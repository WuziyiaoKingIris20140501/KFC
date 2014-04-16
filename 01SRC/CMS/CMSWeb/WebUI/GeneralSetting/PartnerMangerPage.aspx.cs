using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.Configuration;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Web.UI.DataVisualization.Charting;

public partial class PartnerMangerPage : BasePage
{
    PartnerEntity _partnerEntity = new PartnerEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidRbtType.Value = "1440";
            BindPartnerListGrid();
        }
        messageContent.InnerHtml = "";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAddPartner();
        BindPartnerListGrid();
    }

    //清除控件中的数据
    private void clearValue()
    {
    }

    //发放渠道
    private void BindPartnerListGrid()
    {
        _partnerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _partnerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _partnerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _partnerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _partnerEntity.PartnerDBEntity = new List<PartnerDBEntity>();
        PartnerDBEntity partnerDBEntity = new PartnerDBEntity();

        partnerDBEntity.PartnerID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PartnerID"].ToString())) ? null : ViewState["PartnerID"].ToString();
        partnerDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        partnerDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        partnerDBEntity.WapLink = String.IsNullOrEmpty(ConfigurationManager.AppSettings["WapLink"]) ? "" : ConfigurationManager.AppSettings["WapLink"].ToString();
        _partnerEntity.PartnerDBEntity.Add(partnerDBEntity);
        DataSet dsResult = PartnerBP.Select(_partnerEntity).QueryResult;

        gridViewCSPartnerList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSPartnerList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSPartnerList.DataBind();
    }

    protected void gridViewCSPartnerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSPartnerList.Rows.Count; i++)
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(txtDelPartnerID.Value.ToString().Trim()) || String.IsNullOrEmpty(txtDelPartnerLink.Value.ToString().Trim()))
        {

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist('" + hidSysID.Value + "','" + txtDelPartnerID.Value.ToString() + "','" + txtDelPartnerLink.Value.ToString() + "','" + txtDelRemark.Text.ToString().Trim() + "','" + txtDelPartnerTitle.Value.ToString().Trim() + "','" + txtDelCost.Value.ToString().Trim() + "','" + hidPartnerct.Value.ToString().Trim() + "','" + hidAvgpt.Value.Trim() + "','" + hidWapLink.Value.Trim() + "');", true);
            detailMessageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        if (!String.IsNullOrEmpty(txtDelRemark.Text.ToString().Trim()) && txtDelRemark.Text.ToString().Trim().Length > 500)
        {

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist('" + hidSysID.Value + "','" + txtDelPartnerID.Value.ToString() + "','" + txtDelPartnerLink.Value.ToString() + "','" + txtDelRemark.Text.ToString().Trim() + "','" + txtDelPartnerTitle.Value.ToString().Trim() + "','" + txtDelCost.Value.ToString().Trim() + "','" + hidPartnerct.Value.ToString().Trim() + "','" + hidAvgpt.Value.Trim() + "','" + hidWapLink.Value.Trim() + "');", true);
            detailMessageContent.InnerHtml = GetLocalResourceObject("Error6").ToString();
            return;
        }

        if (!String.IsNullOrEmpty(txtDelCost.Value.ToString().Trim()) && !checkNum(txtDelCost.Value.ToString().Trim()))
        {

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist('" + hidSysID.Value + "','" + txtDelPartnerID.Value.ToString() + "','" + txtDelPartnerLink.Value.ToString() + "','" + txtDelRemark.Text.ToString().Trim() + "','" + txtDelPartnerTitle.Value.ToString().Trim() + "','" + txtDelCost.Value.ToString().Trim() + "','" + hidPartnerct.Value.ToString().Trim() + "','" + hidAvgpt.Value.Trim() + "','" + hidWapLink.Value.Trim() + "');", true);
            detailMessageContent.InnerHtml = GetLocalResourceObject("Error8").ToString();
            return;
        }

        _partnerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _partnerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _partnerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _partnerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _partnerEntity.PartnerDBEntity = new List<PartnerDBEntity>();
        PartnerDBEntity partnerDBEntity = new PartnerDBEntity();
        partnerDBEntity.SysID = hidSysID.Value;
        partnerDBEntity.PartnerID = txtDelPartnerID.Value.Trim();
        partnerDBEntity.PartnerLink = txtDelPartnerLink.Value.Trim();
        partnerDBEntity.Remark = txtDelRemark.Text.Trim().Replace("\r\n", "");
        partnerDBEntity.PartnerTitle = txtDelPartnerTitle.Value.Trim();
        partnerDBEntity.PartnerCost = txtDelCost.Value.Trim();
        _partnerEntity.PartnerDBEntity.Add(partnerDBEntity);
        int iResult = PartnerBP.Update(_partnerEntity);

        _commonEntity.LogMessages = _partnerEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "合作渠道链接管理-修改";
        commonDBEntity.Event_ID = txtDelPartnerID.Value.Trim();
        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, txtSelPartnerID.Value, txtPartnerLink.Value);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError2").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist('" + hidSysID.Value + "','" + txtDelPartnerID.Value.ToString() + "','" + txtDelPartnerLink.Value.ToString() + "','" + txtDelRemark.Text.ToString().Trim() + "','" + txtDelPartnerTitle.Value.ToString().Trim() + "','" + txtDelCost.Value.ToString().Trim() + "','" + hidPartnerct.Value.ToString().Trim() + "','" + hidAvgpt.Value.Trim() + "','" + hidWapLink.Value.Trim() + "');", true);
    }

    protected void btnChart_Click(object sender, EventArgs e)
    {
        _partnerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _partnerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _partnerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _partnerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _partnerEntity.PartnerDBEntity = new List<PartnerDBEntity>();
        PartnerDBEntity partnerDBEntity = new PartnerDBEntity();

        partnerDBEntity.SysID = hidSysID.Value.ToString().Trim();
        partnerDBEntity.StartDTime = dpChartStart.Value;
        partnerDBEntity.EndDTime = dpChartEnd.Value;
        _partnerEntity.PartnerDBEntity.Add(partnerDBEntity);
        DataSet dsResult = PartnerBP.ChartSelect(_partnerEntity).QueryResult;
        DataTable dtResult = GetChartDataTable(dsResult);


        //Chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Hours; //按小时
        //Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1.0; //间隔定为 1

        //设置图表的数据源
        Chart1.DataSource = dtResult;

        //设置图表Y轴对应项
        Chart1.Series[0].YValueMembers = "Volume";

        //设置图表X轴对应项
        Chart1.Series[0].XValueMember = "Date";
        //Chart1.Series[0].ChartType = SeriesChartType.SplineArea;
        //Chart1.Series[0].IsValueShownAsLabel = true;

        ////Chart1.Series[0].ChartType = SeriesChartType.Bubble;
        ////Chart1.Series[0].MarkerStyle = MarkerStyle.Square;

        //Chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
        ////Chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 2;
        ////Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
        ////Chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;

        Chart1.ChartAreas[0].AxisX.Interval = 1;
        Chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
        //Chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;


        //Chart1.Series[0].ChartType = SeriesChartType.Bar;
        // Set series point width
        Chart1.Series[0]["PointWidth"] = "0.6";

        // Show data points labels
        Chart1.Series[0].IsValueShownAsLabel = true;

        // Set data points label style
        Chart1.Series[0]["BarLabelStyle"] = "Center";

        // Show as 3D
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

        // Draw as 3D Cylinder
        //Chart1.Series[0]["DrawingStyle"] = "Cylinder";

        //Chart1.BackColor = System.Drawing.Color.LemonChiffon;

        //绑定数据
        Chart1.DataBind();

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist('" + hidSysID.Value + "','" + txtDelPartnerID.Value.ToString() + "','" + txtDelPartnerLink.Value.ToString() + "','" + txtDelRemark.Text.ToString().Trim() + "','" + txtDelPartnerTitle.Value.ToString().Trim() + "','" + txtDelCost.Value.ToString().Trim() + "','" + hidPartnerct.Value.ToString().Trim() + "','" + hidAvgpt.Value.Trim() + "','" + hidWapLink.Value.Trim() + "');", true);
    }

    private DataTable GetChartDataTable(DataSet dsParm)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("Date");
        dtResult.Columns.Add("Volume");
        string RbtType = hidRbtType.Value.ToString();

        if (dsParm.Tables.Count == 0 || dsParm.Tables[0].Rows.Count == 0)
        {
            return dtResult;
        }

        DateTime dtFinalEnd = DateTime.Parse(dsParm.Tables[0].Rows[dsParm.Tables[0].Rows.Count - 1][0].ToString());     
        DateTime dtStart = (String.IsNullOrEmpty(dpChartStart.Value)) ? DateTime.Parse(dsParm.Tables[0].Rows[0][0].ToString()) : DateTime.Parse(dpChartStart.Value);//DateTime.Parse((DateTime.Parse(dsParm.Tables[0].Rows[0][0].ToString()).ToShortDateString() + " 00:00:00"));
        DateTime dtEnd = dtStart.AddMinutes(double.Parse(RbtType));
        int iCount = 0;
        string condition = "";
        while (dtEnd <= dtFinalEnd)
        {
            condition = "DATETIME >='" + dtStart.ToString() + "' and DATETIME <='" + dtEnd.ToString() + "'";
            iCount = dsParm.Tables[0].Select(condition).Count();

            DataRow drRow = dtResult.NewRow();
            if (double.Parse(RbtType) > 60)
            {
                drRow["Date"] = dtStart.ToShortDateString();
            }
            else
            {
                drRow["Date"] = dtStart;
            }

            drRow["Volume"] = iCount;
            dtResult.Rows.Add(drRow);

            dtStart = dtEnd;
            dtEnd = dtStart.AddMinutes(double.Parse(RbtType));
        }

        condition = "DATETIME >='" + dtStart.ToString() + "' and DATETIME <='" + dtEnd.ToString() + "'";
        DataRow drFinalRow = dtResult.NewRow();
        if (double.Parse(RbtType) > 60)
        {
            drFinalRow["Date"] = dtStart.ToShortDateString();
        }
        else
        {
            drFinalRow["Date"] = dtStart;
        }

        iCount = dsParm.Tables[0].Select(condition).Count();
        drFinalRow["Volume"] = iCount;
        dtResult.Rows.Add(drFinalRow);
        
        
        //DateTime dtStart = DateTime.Parse(dsParm.Tables[0].Rows[0][0].ToString());
        //DateTime dtEnd = dtStart.AddMinutes(double.Parse(RbtType));
        //int iCount = 0;
        //foreach (DataRow drTemp in dsParm.Tables[0].Rows)
        //{
        //    if (!String.IsNullOrEmpty(drTemp[0].ToString()) && DateTime.Parse(drTemp[0].ToString()) > dtEnd)
        //    {
        //        DataRow drRow = dtResult.NewRow();
        //        if (double.Parse(RbtType) > 60)
        //        {
        //            drRow["Date"] = dtStart.ToShortDateString();
        //        }
        //        else
        //        {
        //            drRow["Date"] = dtStart;
        //        }

        //        drRow["Volume"] = iCount;
        //        dtResult.Rows.Add(drRow);

        //        iCount = 1;
        //        dtStart = DateTime.Parse(drTemp[0].ToString());
        //        dtEnd = dtStart.AddMinutes(double.Parse(RbtType));
        //    }
        //    else
        //    {
        //        iCount = iCount + 1;
        //    }
        //}

        //if (iCount > 0)
        //{
        //    DataRow drRow = dtResult.NewRow();
        //    if (double.Parse(RbtType) > 60)
        //    {
        //        drRow["Date"] =  dtStart.ToShortDateString();
        //    }
        //    else
        //    {
        //        drRow["Date"] = dtStart;
        //    }

        //    drRow["Volume"] = iCount;
        //    dtResult.Rows.Add(drRow);
        //}

        return dtResult;
    }

    protected void btnCLose_Click(object sender, EventArgs e)
    {
        gridViewCSPartnerList.EditIndex = -1;
        BindPartnerListGrid();
        detailMessageContent.InnerHtml = "";
        messageContent.InnerHtml = "";
        hidPartnerct.Value = "";
        hidAvgpt.Value = "";
        dpChartStart.Value = "";
        dpChartEnd.Value = "";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["PartnerID"] = txtSelPartnerID.Value.Trim();
        if (chkUnTime.Checked)
        {
            ViewState["StartDTime"] = "";
            ViewState["EndDTime"] = "";
        }
        else
        {
            ViewState["StartDTime"] = dpStart.Value;
            ViewState["EndDTime"] = dpEnd.Value;
        }

        gridViewCSPartnerList.EditIndex = -1;
        BindPartnerListGrid();
    }

    protected void gridViewCSPartnerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSPartnerList.PageIndex = e.NewPageIndex;
        BindPartnerListGrid();
    }

    private bool checkNum(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return true;
        }

        try
        {
             decimal.Parse(param);

        }
        catch
        {
            return false;
        }

        return true;
    }

    public void btnAddPartner()
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtPartnerID.Value.ToString().Trim()) || String.IsNullOrEmpty(txtPartnerLink.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        if (!String.IsNullOrEmpty(txtRemark.Text.ToString().Trim()) && txtRemark.Text.ToString().Trim().Length > 500)
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error5").ToString();
            return;
        }

        if (!String.IsNullOrEmpty(txtCost.Value.ToString().Trim()) && !checkNum(txtCost.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
            return;
        }

        _partnerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _partnerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _partnerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _partnerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _partnerEntity.PartnerDBEntity = new List<PartnerDBEntity>();
        PartnerDBEntity partnerDBEntity = new PartnerDBEntity();
        partnerDBEntity.PartnerID = txtPartnerID.Value.Trim();
        partnerDBEntity.PartnerTitle = txtPartnerTitle.Value.Trim();
        partnerDBEntity.PartnerCost = txtCost.Value.Trim();
        partnerDBEntity.PartnerLink = txtPartnerLink.Value.Trim();
        partnerDBEntity.Remark = txtRemark.Text.Trim().Replace("\r\n", "");
        _partnerEntity.PartnerDBEntity.Add(partnerDBEntity);
        int iResult = PartnerBP.Insert(_partnerEntity);

        _commonEntity.LogMessages = _partnerEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "合作渠道链接管理-添加";
        commonDBEntity.Event_ID = txtPartnerID.Value.Trim();
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtSelPartnerID.Value, txtPartnerLink.Value);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
}