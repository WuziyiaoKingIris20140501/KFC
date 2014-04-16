using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Collections;

public partial class WebUI_DBQuery_LmOrderSurveyAnalysis : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    public string chartData = string.Empty;
    public string chartDataNPS = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindchkPlatForm();
            BindchkPayCode();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.messageContent.InnerHtml = "";

        string startDate = StartDate.Value;
        string endDate = EndDate.Value;

        if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
        {
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('请输入需要查询的时间段!');", true);
            this.messageContent.InnerHtml = "请输入需要查询的时间段!";
            return;
        }

        string strPlatForm = "";
        foreach (ListItem lt in chkPlatForm.Items)
        {
            if (lt.Selected)
            {
                strPlatForm = strPlatForm + lt.Text + ",";
            }
        }
        strPlatForm = strPlatForm.Trim(',');

        string strPayCode = "";
        foreach (ListItem lt in chkPayCode.Items)
        {
            if (lt.Selected)
            {
                strPayCode = strPayCode + lt.Value + ",";
            }
        }
        strPayCode = strPayCode.Trim(',');

        ViewState["startDate"] = startDate;
        ViewState["endDate"] = endDate;
        ViewState["PlatForm"] = strPlatForm;
        ViewState["PayCode"] = strPayCode;

        BindChartData(BindChart());
    }

    private void BindChartData(DataSet dsResult)
    {
        string startDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["startDate"].ToString())) ? null : ViewState["startDate"].ToString();
        string endDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["endDate"].ToString())) ? null : ViewState["endDate"].ToString();
        string PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
        string PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();

        Hashtable ht = new Hashtable();
        Dictionary<string, string> Dictionary = new Dictionary<string, string>();
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            //按照Score分数的不同取出不同的List数字
            //9-10
            DataRow[] rows0 = dsResult.Tables[0].Select("score>=9 and score<=10");
            //7-8
            DataRow[] rows1 = dsResult.Tables[0].Select("score>=7 and score<=8");
            //0-6
            DataRow[] rows2 = dsResult.Tables[0].Select("score>=0 and score<=6");

            //得出NPS平均值  ：（9-10分 - 0-6分 ）/0-10分count 总数 
            double NPS = (double.Parse(rows0.Length.ToString()) - double.Parse(rows1.Length.ToString())) / double.Parse(dsResult.Tables[0].Rows.Count.ToString());

            //每天 NPS平均值   select加上  时间   Create_Time
            //获取时间段  HashTable 
            for (int i = 0; i <= (DateTime.Parse(endDate) - DateTime.Parse(startDate)).Days; i++)
            {
                //每一天的总订单数  
                DataRow[] toDayRows = dsResult.Tables[0].Select("create_time like '" + Convert.ToDateTime(startDate).AddDays(i).ToString("yyyy-MM-dd") + "%'");
                //每一天  每个阶段的 不同分数的订单数 
                //Today  9-10
                DataRow[] toDayRows0 = dsResult.Tables[0].Select("create_time like '" + Convert.ToDateTime(startDate).AddDays(i).ToString("yyyy-MM-dd") + "%' and (score>=9 and score<=10)");
                //Today  7-8
                DataRow[] toDayRows1 = dsResult.Tables[0].Select("create_time like '" + Convert.ToDateTime(startDate).AddDays(i).ToString("yyyy-MM-dd") + "%' and (score>=7 and score<=8)");
                //Today  0-6
                DataRow[] toDayRows2 = dsResult.Tables[0].Select("create_time like '" + Convert.ToDateTime(startDate).AddDays(i).ToString("yyyy-MM-dd") + "%' and (score>=0 and score<=6)");

                double toDayNPS = (double.Parse(toDayRows0.Length.ToString()) - double.Parse(toDayRows2.Length.ToString())) / double.Parse(toDayRows.Length.ToString());

                ht.Add(Convert.ToDateTime(startDate).AddDays(i).ToString("yyyy-MM-dd"), toDayNPS.ToString("0.00"));
                Dictionary.Add(Convert.ToDateTime(startDate).AddDays(i).ToString("yyyy-MM-dd"), toDayNPS.ToString("0.00").Replace("非数字", "0.00"));
            }
            AssemblyDateDiv(Dictionary);
            this.NPSDiv.Attributes["style"] = "display:'';padding-left: 20px;";
            lblNPS.Text = NPS.ToString("0.00").Replace("非数字", "0.00");

            this.chartDiv.Attributes["style"] = "display:'';";

            ChartDraw(NPS, Dictionary);
        }
        else
        {
            DateDiv.InnerHtml = "";
            this.NPSDiv.Attributes.Add("style", "display:none;padding-left: 20px;");
            this.chartDiv.Attributes.Add("style", "display:none;");
            this.messageContent.InnerHtml = "该时间段内无订单Survey信息！";
        }

    }

    private DataSet BindChart()
    {
        string startDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["startDate"].ToString())) ? null : ViewState["startDate"].ToString();
        string endDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["endDate"].ToString())) ? null : ViewState["endDate"].ToString();
        string PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
        string PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.StartDTime = startDate;
        _lmSystemLogEntity.EndDTime = endDate;
        _lmSystemLogEntity.PlatForm = PlatForm;
        _lmSystemLogEntity.BookStatus = PayCode;

        DataSet dsResult = LmSystemLogBP.OrderSurveyAnalysis(_lmSystemLogEntity).QueryResult;
        
        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "fog_order_num" };//主键
        gridViewCSList.DataBind();

        return dsResult;
    }

    protected void gridViewCSList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i <= gridViewCSList.Rows.Count; i++)
        {
            //首先判断是否是数据行 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }

    protected void gridViewCSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSList.PageIndex = e.NewPageIndex;
        //BindChart();
        BindChartData(BindChart());
    }

    private void AssemblyDateDiv(Dictionary<string, string> ht)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table border=\"0\" style=\"border-color:white;border-collapse: collapse;padding-top:0px\" cellpadding=\"0\" cellspacing=\"0\" >");
        sb.Append("<tr align=\"center\" style=\"border-collapse:collapse\" >");
        foreach (var item in ht)
        {
            sb.Append("<td style=\"background-color: #CDEBFF; font-style: inherit;width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;\">" + DateTime.Parse(item.Key.ToString()).Month + "-" + DateTime.Parse(item.Key.ToString()).Day + "</td>");
        }
        sb.Append("</tr>");
        sb.Append("<tr align=\"center\">");
        foreach (var item in ht)
        {
            sb.Append("<td style=\"width:100px;white-space:nowrap; border: solid #D5D5D5 1px;\">" + item.Value.ToString() + "</td>");
        }
        sb.Append("</tr>");
        sb.Append("</table>");

        DateDiv.InnerHtml = sb.ToString();
    }

    private void ChartDraw(double NPS, Dictionary<string, string> Dictionary)
    {
        chartData = "[";
        chartDataNPS = "[";


        foreach (var item in Dictionary)
        {
            chartDataNPS = chartDataNPS + "['" + item.Key.ToString() + "', " + NPS.ToString("0.00") + "],";
        }

        foreach (var item in Dictionary)
        {
            chartData = chartData + "['" + item.Key.ToString() + "', " + item.Value.ToString() + "],";
        }
        chartDataNPS = chartDataNPS.Trim(',') + "]";
        chartData = chartData.Trim(',') + "]";

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DrawingLines();", true);
    }

    private void BindchkPlatForm()
    {
        DataTable dsPlatForm = new DataTable();
        DataColumn PLATFORMNAME = new DataColumn("PLATFORMNAME");
        DataColumn PLATFORMCODE = new DataColumn("PLATFORMCODE");
        dsPlatForm.Columns.Add(PLATFORMNAME);
        dsPlatForm.Columns.Add(PLATFORMCODE);

        DataRow dr0 = dsPlatForm.NewRow();
        dr0["PLATFORMNAME"] = "IOS";
        dr0["PLATFORMCODE"] = "IOS";
        dsPlatForm.Rows.Add(dr0);

        DataRow dr1 = dsPlatForm.NewRow();
        dr1["PLATFORMNAME"] = "Android";
        dr1["PLATFORMCODE"] = "ANDROID";
        dsPlatForm.Rows.Add(dr1);

        DataRow dr2 = dsPlatForm.NewRow();
        dr2["PLATFORMNAME"] = "WP";
        dr2["PLATFORMCODE"] = "WP";
        dsPlatForm.Rows.Add(dr2);

        chkPlatForm.DataTextField = "PLATFORMNAME";
        chkPlatForm.DataValueField = "PLATFORMCODE";
        chkPlatForm.DataSource = dsPlatForm;
        chkPlatForm.DataBind();

        foreach (ListItem lt in chkPlatForm.Items)
        {
            lt.Selected = true;
        }
    }

    private void BindchkPayCode()
    {
        DataTable dsPayCode = new DataTable();
        DataColumn BookStatus = new DataColumn("APROVE_STATUS");
        DataColumn BookStatusText = new DataColumn("APROVE_STATUS_TEXT");
        dsPayCode.Columns.Add(BookStatus);
        dsPayCode.Columns.Add(BookStatusText);

        for (int i = 0; i < 4; i++)
        {
            DataRow dr = dsPayCode.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["APROVE_STATUS"] = "6";
                    dr["APROVE_STATUS_TEXT"] = "已完成";
                    break;
                case "1":
                    dr["APROVE_STATUS"] = "8";
                    dr["APROVE_STATUS_TEXT"] = "已离店";
                    break;
                case "2":
                    dr["APROVE_STATUS"] = "5";
                    dr["APROVE_STATUS_TEXT"] = "No-Show";
                    break;
                case "3":
                    dr["APROVE_STATUS"] = "3,9";
                    dr["APROVE_STATUS_TEXT"] = "已取消";
                    break;
                default:
                    dr["APROVE_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dsPayCode.Rows.Add(dr);
        }

        chkPayCode.DataTextField = "APROVE_STATUS_TEXT";
        chkPayCode.DataValueField = "APROVE_STATUS";
        chkPayCode.DataSource = dsPayCode;
        chkPayCode.DataBind();

        foreach (ListItem lt in chkPayCode.Items)
        {
            lt.Selected = true;
        }
    }
}
