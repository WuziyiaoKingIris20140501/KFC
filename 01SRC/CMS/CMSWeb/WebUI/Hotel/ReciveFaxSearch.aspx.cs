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

public partial class ReciveFaxSearch : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["StartDate"] = "";
            ViewState["EndDate"] = "";

            ViewState["LinkType"] = "";
            ViewState["OrderID"] = "";

            ViewState["BindStartDate"] = "";
            ViewState["BindEndDate"] = "";

            dpCreateStart.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
            dpCreateEnd.Value = DateTime.Now.ToShortDateString().Replace("/", "-");

            dpBindCreateStart.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
            dpBindCreateEnd.Value = DateTime.Now.ToShortDateString().Replace("/", "-");

            dpBindCreateStartVerify.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
            dpBindCreateEndVerify.Value = DateTime.Now.ToShortDateString().Replace("/", "-");

            BindCHDdpList();
        }
    }

    private void BindReviewLmSystemLogListGrid()
    {
        //messageContent.InnerHtml = "";
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDate"].ToString())) ? null : ViewState["StartDate"].ToString();
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDate"].ToString())) ? null : ViewState["EndDate"].ToString();

        _lmSystemLogEntity = LmSystemLogBP.ReciveFaxSelect(_lmSystemLogEntity);
        DataSet dsResult = _lmSystemLogEntity.QueryResult;
        gridViewCSReviewLmSystemLogList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "FAXURL", "FAXID" };//主键
        gridViewCSReviewLmSystemLogList.DataBind();
        UpdatePanel3.Update();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["StartDate"] = (String.IsNullOrEmpty(dpCreateStart.Value.Trim())) ? dpCreateStart.Value.Trim() : dpCreateStart.Value.Trim() + " 00:00:00";
        ViewState["EndDate"] = (String.IsNullOrEmpty(dpCreateEnd.Value.Trim())) ? dpCreateEnd.Value.Trim() : dpCreateEnd.Value.Trim() + " 23:59:59";
        tbData.Style.Add("display", "");
        hidSelectIndex.Value = "";
        messageContent.InnerHtml = "";
        BindReviewLmSystemLogListGrid();
        hidSelectedID.Value = "1";
    }

    protected void btnSet_Click(object sender, EventArgs e)
    {
        hidSelectedID.Value = "1";
        if (String.IsNullOrEmpty(txtUNBarCode.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            return;
        }

        string strIndex = hidSelectIndex.Value.Trim();
        if (String.IsNullOrEmpty(strIndex))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
            return;
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.FaxID = strIndex;
        _lmSystemLogEntity.BarCode = txtUNBarCode.Text.Trim();
        int iResult = LmSystemLogBP.SaveReciveFax(_lmSystemLogEntity);
        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "接收传真绑定-保存";
        commonDBEntity.Event_ID = strIndex;
        string conTent = GetLocalResourceObject("EventSaveMessage").ToString();

        conTent = string.Format(conTent, strIndex, txtUNBarCode.Text.Trim());
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SaveSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("SaveSuccess").ToString();
            hidSelectIndex.Value = "";
            txtUNBarCode.Text = "";
            BindReviewLmSystemLogListGrid();
            UpdatePanel3.Update();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SaveError1").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("SaveError1").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SaveError2").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("SaveError2").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SaveError").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("SaveError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "ReSetImagePreview()", true);
    }

    //protected void gridViewCSReviewLmSystemLogList_SelectedIndexChanged(object sender, EventArgs e) 
    //{
    //    imgPre.Src = gridViewCSReviewLmSystemLogList.DataKeys[gridViewCSReviewLmSystemLogList.SelectedIndex].Values[0].ToString();
    //}

    protected void gridViewCSReviewLmSystemLogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSReviewLmSystemLogList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E9E9E9'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

                e.Row.Attributes.Add("OnClick", "SetImagePreview('" + gridViewCSReviewLmSystemLogList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "','" + gridViewCSReviewLmSystemLogList.DataKeys[e.Row.RowIndex].Values[1].ToString() + "')");
            }
        }
    }

    protected void btnBindSearch_Click(object sender, EventArgs e)
    {
        bindmessageContent.InnerHtml = "";
        ViewState["LinkType"] = ddpLinkType.SelectedValue.Trim();
        ViewState["OrderID"] = txtOrderID.Text.Trim();
        ViewState["BindStartDate"] = (String.IsNullOrEmpty(dpBindCreateStart.Value.Trim())) ? dpBindCreateStart.Value.Trim() : dpBindCreateStart.Value.Trim() + " 00:00:00";
        ViewState["BindEndDate"] = (String.IsNullOrEmpty(dpBindCreateEnd.Value.Trim())) ? dpBindCreateEnd.Value.Trim() : dpBindCreateEnd.Value.Trim() + " 23:59:59";
        tbBindData.Style.Add("display", "");
        hidBindSelectIndex.Value = "";
        BindViewBindListGrid();
        hidSelectedID.Value = "2";
    }

    public void BindCHDdpList()
    {
        DataSet dsStarRating = CommonBP.GetConfigList(GetLocalResourceObject("LinkType").ToString());
        if (dsStarRating.Tables.Count > 0)
        {
            dsStarRating.Tables[0].Columns["Key"].ColumnName = "LINKTYPEKEY";
            dsStarRating.Tables[0].Columns["Value"].ColumnName = "LINKTYPEDIS";

            ddpLinkType.DataTextField = "LINKTYPEDIS";
            ddpLinkType.DataValueField = "LINKTYPEKEY";
            ddpLinkType.DataSource = dsStarRating;
            ddpLinkType.DataBind();

            ddpLinkType.SelectedValue = "0";
        }

        DataSet dsReLinkType = CommonBP.GetConfigList(GetLocalResourceObject("ReLinkType").ToString());
        if (dsReLinkType.Tables.Count > 0)
        {
            dsReLinkType.Tables[0].Columns["Key"].ColumnName = "RELINKTYPEKEY";
            dsReLinkType.Tables[0].Columns["Value"].ColumnName = "RELINKTYPEDIS";

            ddpReLinkType.DataTextField = "RELINKTYPEDIS";
            ddpReLinkType.DataValueField = "RELINKTYPEKEY";
            ddpReLinkType.DataSource = dsReLinkType;
            ddpReLinkType.DataBind();
            ddpReLinkType.SelectedValue = "0";
        }

        //ddpLinkTypeVerify
        DataTable dtLinkTypeVerify = new DataTable();
        dtLinkTypeVerify.Columns.Add("RELINKTYPEDIS");
        dtLinkTypeVerify.Columns.Add("RELINKTYPEKEY");

        DataRow rows = dtLinkTypeVerify.NewRow();
        rows["RELINKTYPEDIS"] = "审核传真";
        rows["RELINKTYPEKEY"] = "4";
        dtLinkTypeVerify.Rows.Add(rows);
        ddpLinkTypeVerify.DataTextField = "RELINKTYPEDIS";
        ddpLinkTypeVerify.DataValueField = "RELINKTYPEKEY";
        ddpLinkTypeVerify.DataSource = dtLinkTypeVerify;
        ddpLinkTypeVerify.DataBind();
        ddpLinkTypeVerify.SelectedValue = "0";
    }

    private void BindViewBindListGrid()
    {
        //messageContent.InnerHtml = "";
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.SendFaxType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LinkType"].ToString())) ? null : ViewState["LinkType"].ToString();
        _lmSystemLogEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();

        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BindStartDate"].ToString())) ? null : ViewState["BindStartDate"].ToString();
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BindEndDate"].ToString())) ? null : ViewState["BindEndDate"].ToString();

        _lmSystemLogEntity = LmSystemLogBP.ReciveBindFaxSelect(_lmSystemLogEntity);
        DataSet dsResult = _lmSystemLogEntity.QueryResult;
        gridViewBindList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewBindList.DataKeyNames = new string[] { "FAXURL", "FAXID" };//主键
        gridViewBindList.DataBind();
        UpdatePanel7.Update();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void gridViewBindList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewBindList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E9E9E9'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

                e.Row.Attributes.Add("OnClick", "SetBindImagePreview('" + gridViewBindList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "','" + gridViewBindList.DataKeys[e.Row.RowIndex].Values[1].ToString() + "')");
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        hidSelectedID.Value = "2";
        if ("0".Equals(ddpReLinkType.SelectedValue.Trim()) && String.IsNullOrEmpty(txtBindOrderID.Text.Trim()))
        {
            bindmessageContent.InnerHtml = GetLocalResourceObject("BindError1").ToString();
            return;
        }

        string strIndex = hidBindSelectIndex.Value.Trim();
        if (String.IsNullOrEmpty(strIndex))
        {
            bindmessageContent.InnerHtml = GetLocalResourceObject("BindError2").ToString();
            return;
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.FaxID = strIndex;
        _lmSystemLogEntity.BarCode = ("0".Equals(ddpReLinkType.SelectedValue.Trim())) ? txtBindOrderID.Text.Trim() : "";
        _lmSystemLogEntity.SendFaxType = ddpReLinkType.SelectedValue.Trim();

        _lmSystemLogEntity = LmSystemLogBP.SaveBindReciveFax(_lmSystemLogEntity);
        int iResult = _lmSystemLogEntity.Result;
        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "接收传真重新绑定-保存";
        commonDBEntity.Event_ID = strIndex;
        string conTent = GetLocalResourceObject("EventBindSaveMessage").ToString();

        conTent = string.Format(conTent, strIndex, ddpReLinkType.SelectedValue.Trim(), txtBindOrderID.Text.Trim());
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("BindSaveSuccess").ToString();
            bindmessageContent.InnerHtml = GetLocalResourceObject("BindSaveSuccess").ToString();
            hidBindSelectIndex.Value = "";
            BindViewBindListGrid();
            UpdatePanel7.Update();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = _lmSystemLogEntity.ErrorMSG;
            bindmessageContent.InnerHtml = _lmSystemLogEntity.ErrorMSG;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("BindSaveError").ToString();
            bindmessageContent.InnerHtml = GetLocalResourceObject("BindSaveError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    #region  订单审核回传查看
    protected void btnBindSearchVerify_Click(object sender, EventArgs e)
    {
        bindmessageContent.InnerHtml = "";
        ViewState["LinkTypeVerify"] = ddpLinkTypeVerify.SelectedValue.Trim();

        ViewState["BindStartDateVerify"] = (String.IsNullOrEmpty(dpBindCreateStartVerify.Value.Trim())) ? dpBindCreateStartVerify.Value.Trim() : dpBindCreateStartVerify.Value.Trim() + " 00:00:00";
        ViewState["BindEndDateVerify"] = (String.IsNullOrEmpty(dpBindCreateEndVerify.Value.Trim())) ? dpBindCreateEndVerify.Value.Trim() : dpBindCreateEndVerify.Value.Trim() + " 23:59:59";
        tbBindDataVerify.Style.Add("display", "");
        hidBindSelectIndex.Value = "";
        BindViewBindListVerifyGrid();
        hidSelectedID.Value = "3";
    }

    private void BindViewBindListVerifyGrid()
    {
        //messageContent.InnerHtml = "";
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.SendFaxType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LinkTypeVerify"].ToString())) ? null : ViewState["LinkTypeVerify"].ToString();

        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BindStartDateVerify"].ToString())) ? null : ViewState["BindStartDateVerify"].ToString();
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BindEndDateVerify"].ToString())) ? null : ViewState["BindEndDateVerify"].ToString();

        _lmSystemLogEntity = LmSystemLogBP.ReciveBindFaxVerifySelect(_lmSystemLogEntity);
        DataSet dsResult = _lmSystemLogEntity.QueryResult;
        gridViewBindListVerify.DataSource = dsResult.Tables[0].DefaultView;
        gridViewBindListVerify.DataKeyNames = new string[] { "FAXURL", "FAXID" };//主键
        gridViewBindListVerify.DataBind();
        UpdatePanel11.Update();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void gridViewBindListVerify_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewBindListVerify.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E9E9E9'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

                e.Row.Attributes.Add("OnClick", "SetBindImageVerifyPreview('" + gridViewBindListVerify.DataKeys[e.Row.RowIndex].Values[0].ToString() + "','" + gridViewBindListVerify.DataKeys[e.Row.RowIndex].Values[1].ToString() + "')");
            }
        }
    }
    #endregion

    protected void btnDeleteFax_Click(object sender, EventArgs e)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.FaxID = hidSelectIndex.Value.Trim();
        int iResult = LmSystemLogBP.DeleteFax(_lmSystemLogEntity);
        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "接收传真绑定-无需处理-保存";
        commonDBEntity.Event_ID = hidSelectIndex.Value.Trim();
        commonDBEntity.Event_Content = "";
        commonDBEntity.Event_Result = "受影响行数：" + iResult;
        messageContent.InnerHtml = "操作成功";
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
}