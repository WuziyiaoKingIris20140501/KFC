using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;
using System.Collections;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class FRCancelDataWeekly : BasePage
{
    ReportCenterEntity _reportCenterEntity = new ReportCenterEntity();
    CommonEntity _commonEntity = new CommonEntity();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["StartDTime"] = "";
            ViewState["EndDTime"] = "";
            BindViewCSReviewGridList();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
    }

    private string GetDateTimeQuery(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return "";
        }

        try
        {
            DateTime dtTime = DateTime.Parse(param);
            return dtTime.AddDays(1).ToShortDateString() + " 04:00:00";
        }
        catch
        {
            return "";
        }
    }

    private void BindViewCSReviewGridList()
    {
        //messageContent.InnerHtml = "";
        _reportCenterEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _reportCenterEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _reportCenterEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _reportCenterEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _reportCenterEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        _reportCenterEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        DataSet dsResult = ReportCenterBP.FRCancelDataReportSelect(_reportCenterEntity).QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "d1" };//主键
        gridViewCSReviewList.DataBind();

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void gridViewCSReviewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                //第一行表头
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].Width = 20;
                tcHeader[0].Attributes.Add("rowspan", "2"); //跨Row
                tcHeader[0].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[0].Text = "日期";
                tcHeader[0].CssClass = "NoBreak";
                //tcHeader[0].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Width = 15;
                tcHeader[1].Attributes.Add("rowspan", "2"); //跨Row
                tcHeader[1].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[1].Text = "满房订单";
                tcHeader[1].CssClass = "NoBreak";
                //tcHeader[1].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Width = 10;
                tcHeader[2].Attributes.Add("rowspan", "2"); //跨Row
                tcHeader[2].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[2].Text = "满房取消订单占比";//满房取消订单占比
                tcHeader[2].CssClass = "NoBreak";
                //tcHeader[2].Wrap = true;

                tcHeader.Add(new TableHeaderCell());
                //tcHeader[3].Width = 550;
                tcHeader[3].Attributes.Add("colspan", "4");
                tcHeader[3].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[3].Text = "BAR_满房订单";

                tcHeader.Add(new TableHeaderCell());
                //tcHeader[4].Width = 550;
                tcHeader[4].Attributes.Add("colspan", "6");
                tcHeader[4].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[4].Text = "LM_满房订单";

                tcHeader.Add(new TableHeaderCell());
                //tcHeader[5].Width = 550;
                tcHeader[5].Attributes.Add("colspan", "6");
                tcHeader[5].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[5].Text = "即时LM_满房订单</th></tr><tr  class='GView_HeaderCSS'>";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Width = 10;
                tcHeader[6].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[6].Text = "总数量";
                tcHeader[6].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Width = 10;
                tcHeader[7].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[7].Text = "占满房单比";
                tcHeader[7].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Width = 10;
                tcHeader[8].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[8].Text = "即时订单数";
                tcHeader[8].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[9].Width = 10;
                tcHeader[9].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[9].Text = "占BAR满房单比";
                tcHeader[9].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[10].Width = 10;
                tcHeader[10].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[10].Text = "数量";
                tcHeader[10].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[11].Width = 10;
                tcHeader[11].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[11].Text = "占满房单比";
                tcHeader[11].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Width = 10;
                tcHeader[12].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[12].Text = "HVP销售";
                tcHeader[12].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[13].Width = 10;
                tcHeader[13].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[13].Text = "占LM满房单比";
                tcHeader[13].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[14].Width = 10;
                tcHeader[14].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[14].Text = "hubs1销售";
                tcHeader[14].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[15].Width = 10;
                tcHeader[15].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[15].Text = "占LM满房单比";
                tcHeader[15].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[16].Width = 10;
                tcHeader[16].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[16].Text = "数量";
                tcHeader[16].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[17].Width = 10;
                tcHeader[17].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[17].Text = "占满房单比";
                tcHeader[17].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[18].Width = 10;
                tcHeader[18].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[18].Text = "HVP销售";
                tcHeader[18].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[19].Width = 10;
                tcHeader[19].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[19].Text = "占LM满房单比";
                tcHeader[19].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[20].Width = 10;
                tcHeader[20].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[20].Text = "hubs1销售";
                tcHeader[20].Wrap = false;

                tcHeader.Add(new TableHeaderCell());
                tcHeader[21].Width = 10;
                tcHeader[21].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[21].Text = "占LM满房单比";
                tcHeader[21].Wrap = false;

                break;
        }

         if ((e.Row.RowType == DataControlRowType.Header) || (e.Row.RowType == DataControlRowType.DataRow))
        {
            //设置gridview头和体不自动换行
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].CssClass = "NoBreak";
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
            }
        }

    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["StartDTime"] = dpCreateStart.Value;
        ViewState["EndDTime"] = dpCreateEnd.Value;
        BindViewCSReviewGridList();
    }

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ReportCenterEntity _reportCenterEntity = new ReportCenterEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _reportCenterEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _reportCenterEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _reportCenterEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _reportCenterEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _reportCenterEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        _reportCenterEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        DataSet dsResult = ReportCenterBP.FRCancelDataReportSelect(_reportCenterEntity).QueryResult;
       
        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count ==0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }
        CommonFunction.ExportExcelForLM(dsResult);
    }
}