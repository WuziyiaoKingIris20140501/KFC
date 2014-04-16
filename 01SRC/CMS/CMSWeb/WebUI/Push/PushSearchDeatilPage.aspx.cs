using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.IO;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.OleDb;

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

public partial class PushSearchDeatilPage : BasePage
{
    PushEntity _pushEntity = new PushEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                hidMsg.Value = GetLocalResourceObject("LoadMsg").ToString();
                hidTaskID.Value = Request.QueryString["ID"].ToString();
                ViewState["TaskID"] = hidTaskID.Value.Trim();
                ViewState["TelPhone"] = "";
                BindReviewMainInfo();
                AspNetPager1.CurrentPageIndex = 1;
                BindReviewLmListGrid();
                /*king*/
                //BindReviewLmSystemLogListGrid(1, gridViewCSReviewLmSystemLogList.PageSize);
            }
            else
            {
                messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                btnExport.Visible = false;
                btnSearch.Visible = false;
                btnSend.Visible = false;
                //dvBtnBack.Visible = true;
            }
           
        }
        //messageContent.InnerHtml = "";
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewLmListGrid();
    }

    //private int CountLmSystemLog()
    //{
    //    _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
    //    _pushEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
    //    _pushEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
    //    _pushEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
    //    _pushEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
    //    _pushEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
    //    _pushEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
    //    _pushEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
    //    _pushEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
    //    _pushEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
    //    _pushEntity.HotelComfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();
    //    _pushEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
    //    _pushEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
    //    _pushEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
    //    _pushEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();
    //    _pushEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
    //    _pushEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString();
    //    _pushEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString(); 

    //    _pushEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
    //    _pushEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
    //    _pushEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();

    //    _pushEntity.DashPopStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashPopStatus"].ToString())) ? null : ViewState["DashPopStatus"].ToString();
    //    _pushEntity.DashInStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInStart"].ToString())) ? null : ViewState["DashInStart"].ToString();
    //    _pushEntity.DashInEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInEnd"].ToString())) ? null : ViewState["DashInEnd"].ToString();
    //    _pushEntity.DashStartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashStartDTime"].ToString())) ? null : ViewState["DashStartDTime"].ToString();
    //    _pushEntity.DashEndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashEndDTime"].ToString())) ? null : ViewState["DashEndDTime"].ToString();
    
    //    DataSet dsResult = PushBP.ReviewLmOrderLogSelectCount(_pushEntity).QueryResult;

    //    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
    //    {
    //        return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
    //    }

    //    return 0;
    //}

    private void BindReviewLmListGrid()
    {
        //messageContent.InnerHtml = "";

        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TaskID"].ToString())) ? null : ViewState["TaskID"].ToString();
        pushEntity.TelPhone = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TelPhone"].ToString())) ? null : ViewState["TelPhone"].ToString();
        _pushEntity.PushDBEntity.Add(pushEntity);
        _pushEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _pushEntity.PageSize = gridViewCSReviewList.PageSize;
        //_pushEntity.SortField = gridViewCSReviewList.Attributes["SortExpression"].ToString();
        //_pushEntity.SortType = gridViewCSReviewList.Attributes["SortDirection"].ToString();
        DataSet dsResult = PushBP.SelectPushActionHistoryList(_pushEntity).QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "TelPhone" };//主键
        gridViewCSReviewList.DataBind();

        AspNetPager1.PageSize = gridViewCSReviewList.PageSize;
        AspNetPager1.RecordCount = _pushEntity.TotalCount;

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    private void BindReviewMainInfo()
    {
        //messageContent.InnerHtml = "";

        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = hidTaskID.Value.Trim();
        _pushEntity.PushDBEntity.Add(pushEntity);
        DataSet dsResult = PushBP.SelectPushSuccCount(_pushEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            txtPushTitle.Text = dsResult.Tables[0].Rows[0]["Push_Title"].ToString();
            txtPushContent.Text = dsResult.Tables[0].Rows[0]["Push_Content"].ToString();
            hidPushType.Value = dsResult.Tables[0].Rows[0]["Type"].ToString();
            string strType = dsResult.Tables[0].Rows[0]["Type"].ToString();

            if ("0".Equals(strType))
            {
                lbPushUsers.Text = "所有有效用户";
            }
            else if ("1".Equals(strType))
            {
                lbPushUsers.Text = "用户组： ";
            }
            else if ("2".Equals(strType))
            {
                lbPushUsers.Text = "上传Excel列表： ";
            }

            string strStatus = string.Empty;
            switch (dsResult.Tables[0].Rows[0]["Status"].ToString())
            {
                case "0":
                    strStatus = "已保存，未发送";
                    btnSend.Visible = true;
                    break;
                case "1":
                    strStatus = "待发送";
                    btnSend.Visible = false;
                    break;
                case "2":
                    strStatus = "发送中";
                    btnSend.Visible = false;
                    break;
                case "3":
                    strStatus = "已完成";
                    btnSend.Visible = false;
                    break;
                default:
                    strStatus = "";
                    break;
            }

            lbPustStatus.Text = strStatus;
            lbPushUsers.Text = lbPushUsers.Text + dsResult.Tables[0].Rows[0]["Push_ProtoType"].ToString().Trim(',').Trim();
            lbPushAction.Text = GetPushActionDateTime();

            decimal decAllCount = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["All_Count"].ToString())) ? 0 : decimal.Parse(dsResult.Tables[0].Rows[0]["All_Count"].ToString());
            decimal Suc_Count = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["Suc_Count"].ToString())) ? 0 : decimal.Parse(dsResult.Tables[0].Rows[0]["Suc_Count"].ToString());
            decimal Err_Count = ("2".Equals(dsResult.Tables[0].Rows[0]["Status"].ToString()) || "3".Equals(dsResult.Tables[0].Rows[0]["Status"].ToString())) ? decAllCount - Suc_Count : 0;
            hidPushAllNum.Value = decAllCount.ToString();
            lbPushCount.Text = "DeviceToken总数：" + dsResult.Tables[0].Rows[0]["All_Count"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;成功发送：" + dsResult.Tables[0].Rows[0]["Suc_Count"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;发送失败：" + Err_Count.ToString();
        }
        else
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            btnExport.Visible = false;
            btnSearch.Visible = false;
            btnSend.Visible = false;
        }
    }

    private string GetPushActionDateTime()
    {
        //messageContent.InnerHtml = "";

        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = hidTaskID.Value.Trim();
        _pushEntity.PushDBEntity.Add(pushEntity);
        DataSet dsResult = PushBP.SelectActionDateTime(_pushEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            return dsResult.Tables[0].Rows[0]["FACTIONDT"].ToString() + " -- " + dsResult.Tables[0].Rows[0]["LACTIONDT"].ToString();
        }
        else
        {
            return "";
        }
    }

    protected void gridViewCSReviewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    //    //this.gridViewRegion.PageIndex = e.NewPageIndex;
    //    //BindGridView();

    //    //执行循环，保证每条数据都可以更新
    //    for (int i = 0; i <= gridViewCSChannelList.Rows.Count; i++)
    //    {
    //        //首先判断是否是数据行
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //当鼠标停留时更改背景色
    //            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
    //            //当鼠标移开时还原背景色
    //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
    //        }
    //    }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = hidTaskID.Value.Trim();
        pushEntity.Status = "1";
        _pushEntity.PushDBEntity.Add(pushEntity);
        _pushEntity = PushBP.SendPushMsg(_pushEntity);
        int iResult = _pushEntity.Result;

        _commonEntity.LogMessages = _pushEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "发送Push信息-发送";
        commonDBEntity.Event_ID = txtPushTitle.Text.Trim();
        string conTent = GetLocalResourceObject("EventSendMessage").ToString();

        conTent = string.Format(conTent, hidTaskID.Value, txtPushTitle.Text.Trim(), txtPushContent.Text.Trim(), hidPushType.Value.Trim());
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SendSuccess").ToString();
            //messageContent.InnerHtml = GetLocalResourceObject("SendSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error9").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error9").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            return;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error10").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error10").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            return;
        }

        hidMsg.Value = String.Format(GetLocalResourceObject("PushMsg").ToString(), hidPushAllNum.Value); //"Push发送中...0/100";
        hidRemainSecond.Value = (ConfigurationManager.AppSettings["PushRemainSecond"] != null) ? ConfigurationManager.AppSettings["PushRemainSecond"].ToString() : "20";
        this.Page.RegisterStartupScript("remaintimebtn0", "<script>setInterval('RemainTimeBtn()',1000);</script>"); //执行定时执行
    }

    protected void btnRefush_Click(object sender, EventArgs e)
    {
        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = hidTaskID.Value.Trim();
        _pushEntity.PushDBEntity.Add(pushEntity);
        DataSet dsResult = PushBP.SelectPushSuccCount(_pushEntity).QueryResult;

        decimal decPushSucNum = 0;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["Suc_Count"].ToString()))
        {
            decPushSucNum = decimal.Parse(dsResult.Tables[0].Rows[0]["Suc_Count"].ToString().Trim());
        }
        string strStatus = string.Empty;
        switch (dsResult.Tables[0].Rows[0]["Status"].ToString())
        {
            case "0":
                strStatus = "已保存，未发送";
                btnSend.Visible = true;
                break;
            case "1":
                strStatus = "待发送";
                btnSend.Visible = false;
                break;
            case "2":
                strStatus = "发送中";
                btnSend.Visible = false;
                break;
            case "3":
                strStatus = "已完成";
                btnSend.Visible = false;
                break;
            default:
                strStatus = "";
                break;
        }

        lbPustStatus.Text = strStatus;
        decimal decPushAllNum = String.IsNullOrEmpty(hidPushAllNum.Value) ? 0 : decimal.Parse(hidPushAllNum.Value);

        //if (decPushSucNum < decPushAllNum)
        if (!"3".Equals(dsResult.Tables[0].Rows[0]["Status"].ToString()))
        {
            hidMsg.Value = "Push发送中..." + decPushSucNum.ToString() + "/" + hidPushAllNum.Value;
            //lblRemainSecond.Text = "5";
            hidRemainSecond.Value = (ConfigurationManager.AppSettings["PushRemainSecond"] != null) ? ConfigurationManager.AppSettings["PushRemainSecond"].ToString() : "20";
            //this.Page.RegisterStartupScript("remaintimebtn0", "<script>setInterval('RemainTimeBtn()',1000);</script>"); //执行定时执行
        }
        else
        {
            //hidMsg.Value = "数据加载中，请稍等...";
            //hidRemainSecond.Value = "0";
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel2.Update();
            //BindReviewLmListGrid();
            Response.Redirect(Request.Url.ToString());
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TaskID"].ToString())) ? null : ViewState["TaskID"].ToString();
        pushEntity.TelPhone = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TelPhone"].ToString())) ? null : ViewState["TelPhone"].ToString();
        _pushEntity.PushDBEntity.Add(pushEntity);
        DataSet dsResult = PushBP.ExportPushActionHistoryList(_pushEntity).QueryResult;
        CommonFunction.ExportExcelForLM(dsResult);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["TaskID"] = hidTaskID.Value.Trim();
        ViewState["TelPhone"] = txtTelPhone.Text.Trim();
        AspNetPager1.CurrentPageIndex = 1;
        BindReviewLmListGrid();
    }

    //protected void gridViewCSReviewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    this.gridViewCSReviewList.PageIndex = e.NewPageIndex;
    //    BindReviewLmListGrid();
    //} 

     //<summary>
     //GridView排序事件
     //</summary>
    //protected void gridViewCSReviewLmSystemLogList_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    //// 从事件参数获取排序数据列
    //    //string sortExpression = e.SortExpression.ToString();

    //    //// 假定为排序方向为“顺序”
    //    //string sortDirection = "ASC";

    //    //// “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
    //    //if (sortExpression == gridViewCSReviewList.Attributes["SortExpression"])
    //    //{
    //    //    //获得下一次的排序状态
    //    //    sortDirection = (gridViewCSReviewList.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
    //    //}

    //    //// 重新设定GridView排序数据列及排序方向
    //    //gridViewCSReviewList.Attributes["SortExpression"] = sortExpression;
    //    //gridViewCSReviewList.Attributes["SortDirection"] = sortDirection;

    //    BindReviewLmListGrid();
    //    //BindReviewLmSystemLogListGrid(AspNetPager1.CurrentPageIndex , gridViewCSReviewLmSystemLogList.PageSize);
    //}
}