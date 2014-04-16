using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.Configuration;
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
using HotelVp.Common.Utilities;


public partial class OrderConfirmManager : BasePage
{
    OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type="))
        {
            hidActionType.Value = "";
            ViewState["CityID"] = "";
            ViewState["SortID"] = "ASC";
            ViewState["FGStatus"] = "";
            ViewState["HotelID"] = "";
            ViewState["UserID"] = "";
            ViewState["OrderID"] = "";
            ViewState["StartDate"] = System.DateTime.Now.AddDays(-7).ToShortDateString().Replace("/", "-");//System.DateTime.Now.AddDays(-3).ToShortDateString().Replace("/", "-") + " 04:00:00";
            ViewState["EndDate"] = System.DateTime.Now.ToShortDateString().Replace("/", "-"); ;
            dpLeaveStart.Value = ViewState["StartDate"].ToString();
            dpLeaveEnd.Value = ViewState["EndDate"].ToString();
            BindDropDownList();
            BindOrderConfirmListGrid();
        }
    }

    private void BindDropDownList()
    {
        //DataSet dsCityList = GetCityListData();
        //ddpCityList.DataSource = dsCityList;
        //ddpCityList.DataTextField = "CT_TEXT";
        //ddpCityList.DataValueField = "CT_STATUS";
        //ddpCityList.DataBind();
        //ddpCityList.SelectedIndex = 0;

        DataTable dtSortType = GetSortTypeData();
        ddpSort.DataSource = dtSortType;
        ddpSort.DataTextField = "SORT_TEXT";
        ddpSort.DataValueField = "SORT_STATUS";
        ddpSort.DataBind();
        ddpSort.SelectedIndex = 0;

        DataTable dtFogAudit = GetFogAuditData();
        ddpFogAuditstatus.DataSource = dtFogAudit;
        ddpFogAuditstatus.DataTextField = "FGAT_TEXT";
        ddpFogAuditstatus.DataValueField = "FGAT_STATUS";
        ddpFogAuditstatus.DataBind();
        ddpFogAuditstatus.SelectedIndex = 0;
    }

    private DataSet GetCityListData()
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderInfoBP.CommonCitySelect(_orderInfoEntity).QueryResult;

        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["CITY_ID"].ColumnName = "CT_STATUS";
            dsResult.Tables[0].Columns["NAME_CN"].ColumnName = "CT_TEXT";

            DataRow dr0 = dsResult.Tables[0].NewRow();
            dr0["CT_STATUS"] = "";
            dr0["CT_TEXT"] = "不限制";
            dsResult.Tables[0].Rows.InsertAt(dr0, 0);
        }

        return dsResult;
    }

    private DataTable GetSortTypeData()
    {
        DataTable dt = new DataTable();
        DataColumn SortStatus = new DataColumn("SORT_STATUS");
        DataColumn SortStatusText = new DataColumn("SORT_TEXT");
        dt.Columns.Add(SortStatus);
        dt.Columns.Add(SortStatusText);


        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["SORT_STATUS"] = "ASC";
                    dr["SORT_TEXT"] = "最老优先";
                    break;
                case "1":
                    dr["SORT_STATUS"] = "DESC";
                    dr["SORT_TEXT"] = "最新优先";
                    break;
                default:
                    dr["SORT_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetFogAuditData()
    {
        DataTable dt = new DataTable();
        DataColumn FGStatus = new DataColumn("FGAT_STATUS");
        DataColumn FGStatusText = new DataColumn("FGAT_TEXT");
        dt.Columns.Add(FGStatus);
        dt.Columns.Add(FGStatusText);


        for (int i = 0; i < 5; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["FGAT_STATUS"] = "";
                    dr["FGAT_TEXT"] = "未审核";
                    break;
                case "1":
                    dr["FGAT_STATUS"] = "5";
                    dr["FGAT_TEXT"] = "NoShow";
                    break;
                case "2":
                    dr["FGAT_STATUS"] = "7";
                    dr["FGAT_TEXT"] = "入住中";
                    break;
                case "3":
                    dr["FGAT_STATUS"] = "8";
                    dr["FGAT_TEXT"] = "离店";
                    break;
                case "4":
                    dr["FGAT_STATUS"] = "9";
                    dr["FGAT_TEXT"] = "不限制";
                    break;
                default:
                    dr["SORT_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void BindOrderConfirmListGrid()
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        orderinfoEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        orderinfoEntity.SortID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SortID"].ToString())) ? null : ViewState["SortID"].ToString();
        orderinfoEntity.FGStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FGStatus"].ToString())) ? null : ViewState["FGStatus"].ToString();
        orderinfoEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        orderinfoEntity.UserID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UserID"].ToString())) ? null : ViewState["UserID"].ToString();
        orderinfoEntity.OrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        orderinfoEntity.StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDate"].ToString())) ? null : ViewState["StartDate"].ToString();
        orderinfoEntity.EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDate"].ToString())) ? null : ViewState["EndDate"].ToString();
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderInfoBP.BindOrderConfirmManagerList(_orderInfoEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "ORDERID" };//主键
        gridViewCSList.DataBind();

        //string strCreatTime = "";
        //string strBookStatus = "";
        //string strSysDTime = "";

        //DateTime dtTemp = DateTime.Now;
        //DateTime dtTempCt = DateTime.Now;

        //for (int i = 0; i < gridViewCSList.Rows.Count; i++)
        //{
        //    //首先判断是否是数据行
        //    if (gridViewCSList.Rows[i].RowType == DataControlRowType.DataRow)
        //    {
        //        strCreatTime = gridViewCSList.DataKeys[i].Values[0].ToString();
        //        strBookStatus = gridViewCSList.DataKeys[i].Values[1].ToString();
        //        strSysDTime = gridViewCSList.DataKeys[i].Values[2].ToString();

        //        if (String.IsNullOrEmpty(strCreatTime))
        //        {
        //            continue;
        //        }

        //        dtTempCt = DateTime.Parse(strCreatTime);
        //        dtTemp = DateTime.Parse(strSysDTime).AddMinutes(-30);

        //        if ((dtTemp > dtTempCt) && ("1".Equals(strBookStatus)))
        //        {
        //            gridViewCSList.Rows[i].Cells[0].Attributes.Add("bgcolor", "#FF6666");
        //        }
        //    }
        //}
        //ViewState["StartDate"] = "";
        //ViewState["EndDate"] = "";
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void gridViewCSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSList.PageIndex = e.NewPageIndex;
        BindOrderConfirmListGrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strCityID = "";
        if (String.IsNullOrEmpty(wctCity.AutoResult) || String.IsNullOrEmpty(hidCityID.Value.Trim()))
        {
            strCityID = "";
        }
        else
        {
            string strCityNM = wctCity.AutoResult.ToString();
            strCityID = strCityNM.Substring((strCityNM.IndexOf('[') + 1), (strCityNM.IndexOf(']') - 1));
        }

        ViewState["CityID"] = strCityID;//ddpCityList.SelectedValue.Trim();
        ViewState["SortID"] = ddpSort.SelectedValue.Trim();
        ViewState["FGStatus"] = ddpFogAuditstatus.SelectedValue.Trim();


        string strHotelID = "";
        if (String.IsNullOrEmpty(WebAutoComplete.AutoResult) || String.IsNullOrEmpty(hidHotelID.Value.Trim()))
        {
            strHotelID = "";
        }
        else
        {
            string strHotelNM = WebAutoComplete.AutoResult.ToString();
            strHotelID = strHotelNM.Substring((strHotelNM.IndexOf('[') + 1), (strHotelNM.IndexOf(']') - 1));
        }

        ViewState["HotelID"] = strHotelID;
        ViewState["UserID"] = txtUserID.Text.Trim();
        ViewState["OrderID"] = txtOrderID.Text.Trim();
        ViewState["StartDate"] = dpLeaveStart.Value.ToString();
        ViewState["EndDate"] = dpLeaveEnd.Value.ToString();
        dvErrorInfo.InnerHtml = "";
        BindOrderConfirmListGrid();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        dvErrorInfo.InnerHtml = "";
        BindOrderConfirmListGrid();
    }

    protected void gridViewCSList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        txtActionID.Text = "";
        txtBOOK_REMARK.Text = "";
        string strOrderID = e.CommandArgument.ToString();
        string strActionID = "";
        trAction.Style.Add("display", "none;");
        if (e.CommandName == "quest")
        {
            CreateIssue(strOrderID);
            return;
        }
        else if (e.CommandName == "leave")
        {
            trAction.Style.Add("display", "");
            hidActionType.Value = "8";
            strOrderID = e.CommandArgument.ToString().Contains("_") ? e.CommandArgument.ToString().Split('_')[0].ToString() : e.CommandArgument.ToString();
            strActionID = e.CommandArgument.ToString().Contains("_") ? e.CommandArgument.ToString().Split('_')[1].ToString() : e.CommandArgument.ToString();
            //UpdateAction(strOrderID, "8");
        }
        else if (e.CommandName == "noshow")
        {
            hidActionType.Value = "5";
            //UpdateAction(strOrderID, "5");
        }
        else if (e.CommandName == "remark")
        {
            hidActionType.Value = "";
            //hidLogKey.Value = strOrderID;
            //hidOrderID.Value = (strOrderID.Split('-').Length > 0) ? strOrderID.Split('-')[1].ToString() : strOrderID;
            //lbMemo1.Text = SetMemoVal(strOrderID);
            //ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "setScript", "invokeOpenlist()", true);
        }

        hidOrderID.Value = strOrderID;
        lbMemo1.Text = SetMemoVal(strOrderID);

        txtBOOK_REMARK.Text = "";
        txtActionID.Text = strActionID;
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "setScript", "invokeOpenlist()", true);
    }

    private string SetMemoVal(string strKey)
    {
        string strResult = "";
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.MemoKey = strKey;

        DataSet dsResult = LmSystemLogBP.OrderActionHis(_lmSystemLogEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            string strTemp = "";
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                strTemp = dsResult.Tables[0].Rows[i]["EVENT_TYPE"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["OD_STATUS"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["EVENT_USER"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["EVENT_TIME"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["REMARK"].ToString().Trim();
                strResult = strResult + strTemp + "<br/><br/>";
            }
        }

        return strResult;
    }

    //private string SetMemoVal(string strKey)
    //{
    //    string strResult = "";
    //    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    //    _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
    //    _lmSystemLogEntity.MemoKey = strKey;

    //    DataSet dsResult = LmSystemLogBP.OrderOperationMemoSelect(_lmSystemLogEntity).QueryResult;

    //    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
    //    {
    //        string strTemp = "";
    //        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
    //        {
    //            strTemp = dsResult.Tables[0].Rows[i]["EVENT_CONTENT"].ToString().Trim();
    //            strTemp = strTemp.Substring(strTemp.IndexOf("备注：") + 3, strTemp.IndexOf("取消原因：") - 1 - strTemp.IndexOf("备注：") - 3);
    //            if (!String.IsNullOrEmpty(strTemp.Trim()))
    //            {
    //                strResult = strResult + dsResult.Tables[0].Rows[i]["CREATEDATE"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["USERNAME"].ToString().Trim() + "： " + strTemp + "<br/><br/>";
    //            }
    //        }
    //    }

    //    return strResult;
    //}

    private void UpdateAction(string orderID, string bookStatus)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        CommonEntity _commonEntity = new CommonEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = orderID;
        _lmSystemLogEntity.OrderBookStatus = bookStatus;

        int iResult = LmSystemLogBP.SaveOrderOperationManager(_lmSystemLogEntity);

        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单审核-保存";
        commonDBEntity.Event_ID = hidHotelID.Value + "-" + _lmSystemLogEntity.FogOrderID;
        string conTent = GetLocalResourceObject("EventSaveMessage").ToString();

        conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.OrderBookStatus);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            BindOrderConfirmListGrid();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateLockErr").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateLockErr").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    private DataSet GetOrderInfoData(string orderID)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        CommonEntity _commonEntity = new CommonEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = orderID;

        return LmSystemLogBP.GetOrderInfoData(_lmSystemLogEntity).QueryResult;
    }

    private void CreateIssue(string orderID)
    {
        CommonEntity _commonEntity = new CommonEntity();
        IssueInfoEntity _issueinfoEntity = new IssueInfoEntity();
        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity issueinfoDBEntity = new IssueInfoDBEntity();

        issueinfoDBEntity.IssueID = "";
        issueinfoDBEntity.ActionType = "0";

        DataSet dsOrderInfo = GetOrderInfoData(orderID);

        if (dsOrderInfo.Tables.Count == 0 || dsOrderInfo.Tables[0].Rows.Count == 0)
        {
            dvErrorInfo.InnerHtml = GetLocalResourceObject("IssueError81").ToString();
            return;
        }

        issueinfoDBEntity.Title = "[" + dsOrderInfo.Tables[0].Rows[0]["HOTELNM"].ToString() + "]订单审核问题";
        issueinfoDBEntity.Priority = "0";
        issueinfoDBEntity.IssueType = "8";
        issueinfoDBEntity.AssignNm = (String.IsNullOrEmpty(dsOrderInfo.Tables[0].Rows[0]["SALESNM"].ToString())) ? ConfigurationManager.AppSettings["DefaultissueNm"].ToString() : dsOrderInfo.Tables[0].Rows[0]["SALESNM"].ToString();
        issueinfoDBEntity.Assignto = (String.IsNullOrEmpty(dsOrderInfo.Tables[0].Rows[0]["SALESMG"].ToString())) ? ConfigurationManager.AppSettings["DefaultissueTo"].ToString() : dsOrderInfo.Tables[0].Rows[0]["SALESMG"].ToString();
        issueinfoDBEntity.Status = "0";
        issueinfoDBEntity.IsIndemnify = "0";
        issueinfoDBEntity.IndemnifyPrice = "";
        issueinfoDBEntity.RelatedType = "0";
        issueinfoDBEntity.RelatedID = orderID;
        issueinfoDBEntity.TicketCode = "";

        issueinfoDBEntity.Remark = "";
        issueinfoDBEntity.HisRemark = "";
        issueinfoDBEntity.UpdateUser = UserSession.Current.UserDspName;
        string dtNow = DateTime.Now.ToString();
        issueinfoDBEntity.TimeDiffTal = "";
        issueinfoDBEntity.TimeSpans = "";
        issueinfoDBEntity.UpdateTime = dtNow;

        issueinfoDBEntity.ChkMsgAssgin = "0";
        issueinfoDBEntity.MsgAssginText = "";
        issueinfoDBEntity.ChkMsgUser = "0";
        issueinfoDBEntity.MsgUserText = "";

        _issueinfoEntity.IssueInfoDBEntity.Add(issueinfoDBEntity);
        _issueinfoEntity = IssueInfoBP.IssueSave(_issueinfoEntity);
        int iResult = _issueinfoEntity.Result;
        string strIssueID = _issueinfoEntity.IssueInfoDBEntity[0].IssueID;
        _commonEntity.LogMessages = _issueinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "Issue问题单-保存";
        commonDBEntity.Event_ID = strIssueID;
        string conTent = GetLocalResourceObject("EventIssueInsertMessage").ToString();
        conTent = string.Format(conTent, strIssueID, issueinfoDBEntity.Title, issueinfoDBEntity.Priority, issueinfoDBEntity.IssueType, issueinfoDBEntity.Assignto, issueinfoDBEntity.Status, issueinfoDBEntity.IsIndemnify, issueinfoDBEntity.IndemnifyPrice, issueinfoDBEntity.TicketCode, issueinfoDBEntity.RelatedType, issueinfoDBEntity.RelatedID, issueinfoDBEntity.Remark, _issueinfoEntity.LogMessages.Username);
        commonDBEntity.Event_Content = conTent;
        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertIssueSuccess").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("InsertIssueSuccess").ToString();
            IssueInfoBP.SendMail(_issueinfoEntity);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("IssueError6").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("IssueError6").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("IssueError8").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("IssueError8").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("IssueError4").ToString() + _issueinfoEntity.ErrorMSG;
            dvErrorInfo.InnerHtml = GetLocalResourceObject("IssueError4").ToString() + _issueinfoEntity.ErrorMSG;
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void btnAddRemark_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtBOOK_REMARK.Text.Trim()) && (StringUtility.Text_Length(txtBOOK_REMARK.Text.ToString().Trim()) > 250))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorRemark").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "setScript", "invokeOpenlist()", true);
            return;
        }

        if ("8".Equals(hidActionType.Value) && String.IsNullOrEmpty(txtActionID.Text.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorAction").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "setScript", "invokeOpenlist()", true);
            return;
        }

        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;
        //_lmSystemLogEntity.LogKey = hidLogKey.Value;
        _lmSystemLogEntity.OrderBookStatus = hidActionType.Value;
        _lmSystemLogEntity.CanelReson = "";
        _lmSystemLogEntity.BookRemark = txtBOOK_REMARK.Text.Trim();
        _lmSystemLogEntity.ActionID = txtActionID.Text.Trim();
        _lmSystemLogEntity.FollowUp = "0";

        int iResult = LmSystemLogBP.SaveOrderOpeRemark(_lmSystemLogEntity);

        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单审核-保存";
        commonDBEntity.Event_ID = hidOrderID.Value;
        string conTent = GetLocalResourceObject("EventSaveMessage1").ToString();

        conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.OrderBookStatus, _lmSystemLogEntity.BookRemark, _lmSystemLogEntity.CanelReson, _lmSystemLogEntity.FollowUp);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            string strMsg = "";
            if ("5".Equals(hidActionType.Value))
            {
                strMsg = GetLocalResourceObject("UpdateSuccess12").ToString();
            }
            else if ("8".Equals(hidActionType.Value))
            {
                strMsg = GetLocalResourceObject("UpdateSuccess11").ToString();
            }
            else if (String.IsNullOrEmpty(hidActionType.Value))
            {
                strMsg = GetLocalResourceObject("UpdateSuccess1").ToString();
            }
            commonDBEntity.Event_Result = strMsg;
            dvErrorInfo.InnerHtml = strMsg;
            BindOrderConfirmListGrid();
        }
        else if (iResult == 0)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("WarningMessage").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError1").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateError1").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.ORDER_NUM = hidOrderID.Value;
        orderinfoEntity.OdStatus = SetActionTypeVal(hidActionType.Value);
        orderinfoEntity.REMARK = txtBOOK_REMARK.Text.Trim();
        orderinfoEntity.ActionID = txtActionID.Text.Trim();
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        CommonBP.InsertOrderActionHistory(_orderInfoEntity);

        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "setScript", "invokeCloselist()", true);
    }

    private string SetActionTypeVal(string strType)
    {
        if ("5".Equals(strType))
        {
            return "No-Show";
        }
        else if ("8".Equals(strType))
        {
            return "离店";
        }
        else
        {
            return "备注";
        }
    }
}