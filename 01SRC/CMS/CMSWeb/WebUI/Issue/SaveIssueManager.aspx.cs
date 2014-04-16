using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class SaveIssueManager : BasePage
{
    CommonEntity _commonEntity = new CommonEntity();
    IssueInfoEntity _issueinfoEntity = new IssueInfoEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidActionType.Value = "0";
            hidCloseFlag.Value = "1";
            BindDropDownList();
            SetUpdateContrlVal();
            BindGridView();
        }
    }

    private void SetUpdateContrlVal()
    {
        if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            string IssueID = Request.QueryString["ID"].ToString().Trim();
            messageContent.InnerHtml = "";
            _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
            IssueInfoDBEntity issueinfoDBEntity = new IssueInfoDBEntity();
            issueinfoDBEntity.IssueID = IssueID;
            _issueinfoEntity.IssueInfoDBEntity.Add(issueinfoDBEntity);

            DataSet dsAssUser = IssueInfoBP.GetUpdateIssueDetail(_issueinfoEntity).QueryResult;
            if (dsAssUser.Tables.Count > 0 && dsAssUser.Tables[0].Rows.Count > 0)
            {
                hidIssueID.Value = IssueID;
                txtTitle.Text = dsAssUser.Tables[0].Rows[0]["Title"].ToString();
                ddpPriority.SelectedValue = dsAssUser.Tables[0].Rows[0]["Priority"].ToString();

                //chkIssueType
                string[] strType = dsAssUser.Tables[0].Rows[0]["Type"].ToString().Trim().Split(',');
                ArrayList alList = new ArrayList();
                foreach (string strTemp in strType)
                {
                    alList.Add(strTemp);
                }

                foreach (ListItem li in chkIssueType.Items)
                {
                    if (alList.Contains(li.Value))
                    {
                        li.Selected = true;
                    }
                }

                hidAssginTo.Value = dsAssUser.Tables[0].Rows[0]["Assignto"].ToString();
                //ddpAstoList.SelectedValue = dsAssUser.Tables[0].Rows[0]["Assignto"].ToString();
                ddpStatusList.SelectedValue = dsAssUser.Tables[0].Rows[0]["Status"].ToString();
                ddpRelated.SelectedValue = dsAssUser.Tables[0].Rows[0]["RelatedType"].ToString();
                txtIndemnifyPrice.Text = dsAssUser.Tables[0].Rows[0]["IndemnifyPrice"].ToString();
                txtRelatedID.Text = dsAssUser.Tables[0].Rows[0]["RelatedID"].ToString();
                lbIssueHis.Text = dsAssUser.Tables[0].Rows[0]["Remark"].ToString();

                lbIssueID.Text = dsAssUser.Tables[0].Rows[0]["IssueID"].ToString();
                lbIssueCtPer.Text = dsAssUser.Tables[0].Rows[0]["CreateUser"].ToString();
                lbIssueCtDt.Text = dsAssUser.Tables[0].Rows[0]["Create_Time"].ToString();
                txtPackageCode.Text = (String.IsNullOrEmpty(dsAssUser.Tables[0].Rows[0]["TicketCode"].ToString().Trim())) ? "" : dsAssUser.Tables[0].Rows[0]["TicketCode"].ToString() + " - " + dsAssUser.Tables[0].Rows[0]["TicketAmount"].ToString();
                hidIsIndemnify.Value = dsAssUser.Tables[0].Rows[0]["IsIndemnify"].ToString();
                lbTimeDiffTal.Text = dsAssUser.Tables[0].Rows[0]["TimeDiffTal"].ToString();

                lbCtTitle.Text = "修改Issue单";
                hidActionType.Value = "1";
                BindgridViewCSReviewList();

                hidUserID.Value = dsAssUser.Tables[0].Rows[0]["AssignUser"].ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetBtnStyle('1'," + dsAssUser.Tables[0].Rows[0]["IsIndemnify"].ToString() + ",'" + dsAssUser.Tables[0].Rows[0]["RelatedType"].ToString() + "')", true);
                UpdatePanel8.Update();
            }
            else
            {
                btnSave.Visible = false;
                messageContent.InnerHtml = GetLocalResourceObject("Warning").ToString();
            }
        }
        else if (Request.QueryString["RType"] != null && !String.IsNullOrEmpty(Request.QueryString["RType"]))
        {
            ddpRelated.SelectedValue = Request.QueryString["RType"].ToString().Trim();
            txtRelatedID.Text = (Request.QueryString["RID"] != null && !String.IsNullOrEmpty(Request.QueryString["RID"])) ? Request.QueryString["RID"].ToString().Trim() : "";

            string IsuNm = (Request.QueryString["IsuNm"] != null && !String.IsNullOrEmpty(Request.QueryString["IsuNm"])) ? HttpUtility.UrlDecode(Request.QueryString["IsuNm"], Encoding.GetEncoding("GB2312")) : "";
            txtTitle.Text = IsuNm;
            if (!String.IsNullOrEmpty(IsuNm))
            {
                chkIssueType.SelectedValue = "1";
                string strIssue = ConfigurationManager.AppSettings["DefaultOrderConfirmIssue"].ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetUserControl('" + strIssue + "','0')", true);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SerRbtValue('0')", true);
            }
        }
        else if (Request.QueryString["BType"] != null && !String.IsNullOrEmpty(Request.QueryString["BType"]))
        {
            ddpRelated.SelectedValue = Request.QueryString["BType"].ToString().Trim();
            txtRelatedID.Text = (Request.QueryString["RID"] != null && !String.IsNullOrEmpty(Request.QueryString["RID"])) ? Request.QueryString["RID"].ToString().Trim() : "";
            BindOrderIssue(txtRelatedID.Text);
        }
        else
        {
            hidCloseFlag.Value = "0";
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SerRbtValue('0')", true);
        }
    }

    private void BindOrderIssue(string orderID)
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
            return;
        }

        txtTitle.Text = "[" + dsOrderInfo.Tables[0].Rows[0]["HOTELNM"].ToString() + "]订单审核问题";
        ddpPriority.SelectedValue = "2";
        string strIssue = (String.IsNullOrEmpty(dsOrderInfo.Tables[0].Rows[0]["SALESMG"].ToString())) ? ConfigurationManager.AppSettings["DefaultIssue"].ToString() : "["+ dsOrderInfo.Tables[0].Rows[0]["SALESMG"].ToString() +"]"+ dsOrderInfo.Tables[0].Rows[0]["SALESNM"].ToString();
        ddpStatusList.SelectedValue = "0";
        chkIssueType.SelectedValue = "8";
        //ScriptManager.RegisterStartupScript(this.UpdatePanel8, this.GetType(), "setScript", "SetUserControl('" + strIssue + "','0')", true);
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetUserControl('" + strIssue + "','0')", true);
        //UpdatePanel8.Update();
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

    private void BindgridViewCSReviewList()
    {
        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity issueinfoDBEntity = new IssueInfoDBEntity();
        issueinfoDBEntity.IssueID = hidIssueID.Value;
        _issueinfoEntity.IssueInfoDBEntity.Add(issueinfoDBEntity);
        DataSet dsResult = IssueInfoBP.GetIssueHistoryList(_issueinfoEntity).QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        //gridViewCSReviewList.DataKeyNames = new string[] { "CreateTime" };//主键
        gridViewCSReviewList.DataBind();
    }

    private void BindDropDownList()
    {
        hidIsIndemnify.Value = "0";
        DataSet dsPriority = CommonBP.GetConfigList(GetLocalResourceObject("IssPriority").ToString());
        if (dsPriority.Tables.Count > 0)
        {
            dsPriority.Tables[0].Columns["Key"].ColumnName = "PriorityKEY";
            dsPriority.Tables[0].Columns["Value"].ColumnName = "PriorityVALUE";

            ddpPriority.DataTextField = "PriorityVALUE";
            ddpPriority.DataValueField = "PriorityKEY";
            ddpPriority.DataSource = dsPriority;
            ddpPriority.DataBind();
            ddpPriority.SelectedValue = "2";
        }

        DataSet dsIssueType = CommonBP.GetConfigList(GetLocalResourceObject("IssueType").ToString());
        if (dsIssueType.Tables.Count > 0)
        {
            dsIssueType.Tables[0].Columns["Key"].ColumnName = "IssTypeKEY";
            dsIssueType.Tables[0].Columns["Value"].ColumnName = "IssTypeVALUE";

            chkIssueType.DataTextField = "IssTypeVALUE";
            chkIssueType.DataValueField = "IssTypeKEY";
            chkIssueType.DataSource = dsIssueType;
            chkIssueType.DataBind();
        }

        DataSet dsIssueStatus = CommonBP.GetConfigList(GetLocalResourceObject("IssueStatus").ToString());
        if (dsIssueStatus.Tables.Count > 0)
        {
            dsIssueStatus.Tables[0].Columns["Key"].ColumnName = "IssStatusKEY";
            dsIssueStatus.Tables[0].Columns["Value"].ColumnName = "IssStatusVALUE";

            ddpStatusList.DataTextField = "IssStatusVALUE";
            ddpStatusList.DataValueField = "IssStatusKEY";
            ddpStatusList.DataSource = dsIssueStatus;
            ddpStatusList.DataBind();
        }

        DataSet dsIssueRelated = CommonBP.GetConfigList(GetLocalResourceObject("IssueRelated").ToString());
        if (dsIssueRelated.Tables.Count > 0)
        {
            dsIssueRelated.Tables[0].Columns["Key"].ColumnName = "IssRelatedKEY";
            dsIssueRelated.Tables[0].Columns["Value"].ColumnName = "IssRelatedVALUE";

            ddpRelated.DataTextField = "IssRelatedVALUE";
            ddpRelated.DataValueField = "IssRelatedKEY";
            ddpRelated.DataSource = dsIssueRelated;
            ddpRelated.DataBind();
        }
        //SetSalesDataControl();
    }

    private void SetSalesDataControl()
    {
        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity issueinfoDBEntity = new IssueInfoDBEntity();
        _issueinfoEntity.IssueInfoDBEntity.Add(issueinfoDBEntity);

        DataSet dsAssUser = IssueInfoBP.GetCommonUserList(_issueinfoEntity).QueryResult;

        //DataRow drTemp = dsSalesManager.Tables[0].NewRow();
        //drTemp["SALESID"] = DBNull.Value;
        //drTemp["SALESNAME"] = "不限制";
        //dsSalesManager.Tables[0].Rows.InsertAt(drTemp, 0);

        //ddpAstoList.DataTextField = "USERNAME";
        //ddpAstoList.DataValueField = "USERID";
        //ddpAstoList.DataSource = dsAssUser;
        //ddpAstoList.DataBind();
        //ddpAstoList.SelectedIndex = 0;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(txtRelatedID.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        string strUrl = string.Empty;
        string strRelatedType = ddpRelated.SelectedValue.Trim();
        Control cl = new Control();
        switch (strRelatedType)
        {
            case "0":
                strUrl = cl.ResolveClientUrl("~/WebUI/DBQuery/LmSystemLogDetailPageByNew.aspx") + "?FOGID=";
                break;
            case "1":
                strUrl = cl.ResolveClientUrl("~/WebUI/Hotel/HotelInfoManager.aspx") + "?hid=";
                break;
            case "2":
                strUrl = cl.ResolveClientUrl("~/WebUI/Invoice/InvoiceManagerSearch.aspx") + "?CNF=";
                break;
            case "3":
                strUrl = cl.ResolveClientUrl("~/WebUI/UserGroup/UserDetailPage.aspx") + "?TYPE=1&ID=";
                break;
            case "4":
                strUrl = cl.ResolveClientUrl("~/WebUI/CashBack/CashApplProcess.aspx") + "?TYPE=1&ID=";
                break;
            case "5":
                strUrl = cl.ResolveClientUrl("~/WebUI/Feedback/UpdateAdviceStatus.aspx") + "?TYPE=1&id=";
                break;
            default:
                strUrl = "";
                break;
        }

        hidLinkUrl.Value = strUrl + txtRelatedID.Text.Trim();
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "key", "window.open('" + hidLinkUrl.Value + "', null,'left=0,screenX=0,top=0,screenY=0,scrollbars=1,status=yes,toolbar=yes,menubar=yes,location=yes,resizable');", true);


        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "PopupArea();", true);

        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "PopupArea('" + strUrl + txtRelatedID.Text.Trim() + "');", true);
        //Response.Redirect(strUrl + txtRelatedID.Text.Trim());

        //StringBuilder script = new StringBuilder();

        //script.Append("<script type='text/javascript' language='JavaScript' >");
        //script.Append("window.open('" + strUrl + txtRelatedID.Text.Trim() + "')");
        //script.Append("</script>");

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "report", script.ToString(), true);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        string strIssueType = "";
        foreach (ListItem li in chkIssueType.Items)
        {
            if (li.Selected == true)
            {
                strIssueType = strIssueType + li.Value + ",";
            }
        }
        strIssueType = strIssueType.Trim(',');

        if (String.IsNullOrEmpty(txtTitle.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if ("1".Equals(hidIsIndemnify.Value.Trim()))
        {
            if (String.IsNullOrEmpty(txtIndemnifyPrice.Text.Trim()) && String.IsNullOrEmpty(hidPageCode.Value.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
                return;
            }

            if (!String.IsNullOrEmpty(txtIndemnifyPrice.Text.Trim()) && !ChkNumber(txtIndemnifyPrice.Text.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error10").ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
                return;
            }
        }

        if (String.IsNullOrEmpty(txtRelatedID.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if (String.IsNullOrEmpty(strIssueType.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error5").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if (String.IsNullOrEmpty(hidUserID.Value.Trim()) || hidUserID.Value.IndexOf('[') < 0 || hidUserID.Value.IndexOf(']') < 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if ("1".Equals(hidChkAssginTo.Value.Trim()) && String.IsNullOrEmpty(txtAsginText.Text.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error111").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if ("1".Equals(hidChkAssginTo.Value.Trim()) && !String.IsNullOrEmpty(txtAsginText.Text.ToString().Trim()) && (txtAsginText.Text.ToString().Trim().Length > 190))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error11").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if ("1".Equals(hidChkMsgUser.Value.Trim()) && String.IsNullOrEmpty(txtMsgUser.Text.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error121").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if ("1".Equals(hidChkMsgUser.Value.Trim()) && !String.IsNullOrEmpty(txtMsgUser.Text.ToString().Trim()) && (txtMsgUser.Text.ToString().Trim().Length > 190))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error12").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity issueinfoDBEntity = new IssueInfoDBEntity();

        issueinfoDBEntity.IssueID = hidIssueID.Value.Trim();
        issueinfoDBEntity.ActionType = hidActionType.Value.Trim();

        issueinfoDBEntity.Title = txtTitle.Text.Trim();
        issueinfoDBEntity.Priority = ddpPriority.SelectedValue.Trim();
        issueinfoDBEntity.IssueType = strIssueType;
        issueinfoDBEntity.AssignNm = hidUserID.Value.Trim();
        issueinfoDBEntity.Assignto = hidUserID.Value.Substring((hidUserID.Value.IndexOf('[') + 1), (hidUserID.Value.IndexOf(']') - 1));// ddpAstoList.SelectedValue.Trim();
        issueinfoDBEntity.Status = ddpStatusList.SelectedValue.Trim();
        issueinfoDBEntity.IsIndemnify = hidIsIndemnify.Value.Trim();
        issueinfoDBEntity.IndemnifyPrice = ("0".Equals(hidIsIndemnify.Value.Trim()))? "" : txtIndemnifyPrice.Text.Trim();
        issueinfoDBEntity.RelatedType = ddpRelated.SelectedValue.Trim();
        issueinfoDBEntity.RelatedID = txtRelatedID.Text.Trim();
        issueinfoDBEntity.TicketCode = ("0".Equals(hidIsIndemnify.Value.Trim())) ? "" : hidPageCode.Value.Trim();
        //issueinfoDBEntity.Remark = "0".Equals(hidActionType.Value) ? UserSession.Current.UserDspName + "  " + DateTime.Now.ToLongDateString() + "  " + txtRemark.Text.Trim() + "<br/>" : lbIssueHis.Text + UserSession.Current.UserDspName + "  " + DateTime.Now.ToLongDateString() + "  " + txtRemark.Text.Trim() + "<br/>";
        issueinfoDBEntity.Remark = !String.IsNullOrEmpty(txtRemark.Text.Trim()) ? lbIssueHis.Text + UserSession.Current.UserDspName + "  " + DateTime.Now.ToString() + "  " + txtRemark.Text.Trim() + "<br/>" : lbIssueHis.Text;
        issueinfoDBEntity.HisRemark = txtRemark.Text.Trim();
        issueinfoDBEntity.UpdateUser = UserSession.Current.UserDspName;
        string dtNow = DateTime.Now.ToString();
        issueinfoDBEntity.TimeDiffTal = SetTimeLag(lbIssueCtDt.Text, dtNow);
        issueinfoDBEntity.TimeSpans = SetTimeSpanLag(lbIssueCtDt.Text, dtNow);
        issueinfoDBEntity.UpdateTime = dtNow;

        issueinfoDBEntity.ChkMsgAssgin = hidChkAssginTo.Value;
        issueinfoDBEntity.MsgAssginText = ("1".Equals(hidChkAssginTo.Value)) ? txtAsginText.Text.Trim() : "";
        issueinfoDBEntity.ChkMsgUser = ("0".Equals(ddpRelated.SelectedValue.Trim())) ? hidChkMsgUser.Value : "";
        issueinfoDBEntity.MsgUserText = ("1".Equals(hidChkMsgUser.Value) && "0".Equals(ddpRelated.SelectedValue.Trim())) ? txtMsgUser.Text.Trim() : "";

        _issueinfoEntity.IssueInfoDBEntity.Add(issueinfoDBEntity);
        _issueinfoEntity = IssueInfoBP.IssueSave(_issueinfoEntity);
        int iResult = _issueinfoEntity.Result;
        string strIssueID = _issueinfoEntity.IssueInfoDBEntity[0].IssueID;
        _commonEntity.LogMessages = _issueinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "Issue单管理-保存";
        commonDBEntity.Event_ID = strIssueID;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, strIssueID, issueinfoDBEntity.Title, issueinfoDBEntity.Priority, issueinfoDBEntity.IssueType, issueinfoDBEntity.Assignto, issueinfoDBEntity.Status, issueinfoDBEntity.IsIndemnify, issueinfoDBEntity.IndemnifyPrice, issueinfoDBEntity.TicketCode, issueinfoDBEntity.RelatedType, issueinfoDBEntity.RelatedID, issueinfoDBEntity.Remark, _issueinfoEntity.LogMessages.Username);
        commonDBEntity.Event_Content = conTent;
        if (iResult == 1)
        {
            commonDBEntity.Event_Result = ("1".Equals(hidChkAssginTo.Value) || ("1".Equals(hidChkMsgUser.Value) && "0".Equals(ddpRelated.SelectedValue.Trim()))) ? GetLocalResourceObject("InsertSuccess").ToString() : GetLocalResourceObject("Success818").ToString();
            messageContent.InnerHtml = ("1".Equals(hidChkAssginTo.Value) || ("1".Equals(hidChkMsgUser.Value) && "0".Equals(ddpRelated.SelectedValue.Trim()))) ? GetLocalResourceObject("InsertSuccess").ToString() : GetLocalResourceObject("Success818").ToString();
            if ("0".Equals(hidActionType.Value.Trim()) || ("1".Equals(hidActionType.Value.Trim()) && !hidAssginTo.Value.Trim().Equals(issueinfoDBEntity.Assignto)))
            {
                IssueInfoBP.SendMail(_issueinfoEntity);
            }
            BindgridViewCSReviewList();
            hidAssginTo.Value = issueinfoDBEntity.Assignto;
            lbTimeDiffTal.Text = SetTimeLag(lbIssueCtDt.Text, dtNow);
            UpdatePanel9.Update();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error6").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error6").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error8").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error8").ToString();
        }
        else if (iResult == 9)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Success818").ToString() + _issueinfoEntity.IssueInfoDBEntity[0].MsgAssginRs + _issueinfoEntity.IssueInfoDBEntity[0].MsgUserRs;
            messageContent.InnerHtml = GetLocalResourceObject("Success818").ToString() + _issueinfoEntity.IssueInfoDBEntity[0].MsgAssginRs + _issueinfoEntity.IssueInfoDBEntity[0].MsgUserRs;
            if ("0".Equals(hidActionType.Value.Trim()) || ("1".Equals(hidActionType.Value.Trim()) && !hidAssginTo.Value.Trim().Equals(issueinfoDBEntity.Assignto)))
            {
                IssueInfoBP.SendMail(_issueinfoEntity);
            }
            BindgridViewCSReviewList();
            hidAssginTo.Value = issueinfoDBEntity.Assignto;
            lbTimeDiffTal.Text = SetTimeLag(lbIssueCtDt.Text, dtNow);
            UpdatePanel9.Update();
        }
        //else if (iResult == 4)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("Error81").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("Error81").ToString();
        //}
        //else if (iResult == 5)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("Error811").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("Error811").ToString();
        //}
        //else if (iResult == 6 || iResult == 7)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString() +_issueinfoEntity.IssueInfoDBEntity[0].MsgUserRs;
        //    messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString() + _issueinfoEntity.IssueInfoDBEntity[0].MsgUserRs;
        //}
        //else if (iResult == 8)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("Error812").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("Error812").ToString();
        //}
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error4").ToString() + _issueinfoEntity.ErrorMSG;
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString() + _issueinfoEntity.ErrorMSG;
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        if (iResult == 1 || iResult == 9)
        {
            if ("1".Equals(hidCloseFlag.Value))
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "setScript", "BtnUpdateComplete()", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "setScript", "ClearClickEvent()", true);
            }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
        }
        //UpdatePanel6.Update();
    }

    private string SetTimeLag(string strFrom, string strTo)
    {
        string strResult = "";

        if (!CheckDateTimeValue(strFrom, strTo))
        {
            return strResult;
        }

        DateTime dtFrom = DateTime.Parse(strFrom);
        DateTime dtTo = DateTime.Parse(strTo);

        System.TimeSpan ND = dtTo - dtFrom;

        strResult = strResult + ND.Days.ToString() + "天";
        strResult = strResult + ND.Hours.ToString() + "时";
        strResult = strResult + ND.Minutes.ToString() + "分";
        strResult = strResult + ND.Seconds.ToString() + "秒";
        return strResult;
    }

    private string SetTimeSpanLag(string strFrom, string strTo)
    {
        string strResult = "";

        if (!CheckDateTimeValue(strFrom, strTo))
        {
            return strResult;
        }

        DateTime dtFrom = DateTime.Parse(strFrom);
        DateTime dtTo = DateTime.Parse(strTo);

        System.TimeSpan ND = dtTo - dtFrom;
        strResult = ND.TotalMilliseconds.ToString();
        return strResult;
    }

    private bool CheckDateTimeValue(string strFrom, string strTo)
    {
        try
        {
            DateTime.Parse(strFrom);
            DateTime.Parse(strTo);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool IsValidNumber(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return true;
        }

        try
        {
            decimal.Parse(param);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool IsValidTwoPrice(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return true;
        }

        try
        {
            if (decimal.Parse(param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private bool ChkNumber(string param)
    {
        bool bReturn = true;

        if (String.IsNullOrEmpty(param))
        {
            return false;
        }

        try
        {
            return IsVali(param);
        }
        catch
        {
            bReturn = false;
        }

        return bReturn;
    }

    private bool IsVali(string str)
    {
        bool flog = false;
        string strPatern = @"^([1-9]\d*)$";
        Regex reg = new Regex(strPatern);
        if (reg.IsMatch(str))
        {
            flog = true;
        }
        return flog;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
        ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "unReport", "document.getElementById('popupDiv2').style.display = 'block';document.getElementById('bgDiv2').style.display = 'block';document.getElementById('bgDiv2').style.width = document.body.offsetWidth + 'px';document.getElementById('bgDiv2').style.height = screen.height + 'px';", true);
    }

    void BindGridView()
    {
        //string strSql = "select distinct p.id,p.packagecode,p.usercnt,p.startdate,p.enddate,p.packagename,t.ticketrulecode from t_lm_ticket_package p ";
        //strSql += " left join t_lm_ticket t";
        //strSql += " on p.PACKAGECODE = t.PACKAGECODE where 1=1 and p.SINGLE_USERCNT <= 1";
        string strSql = " select distinct p.id,p.packagecode,p.usercnt,p.startdate,p.enddate,p.packagename,t.ticketrulecode,p.AMOUNT from t_lm_ticket_package p inner join (select count(ticketcode) as tCount, packagecode from t_lm_ticket group by packagecode) tcl on p.packagecode = tcl.packagecode and tcl.tCount <= 1 left join t_lm_ticket t on p.PACKAGECODE = t.PACKAGECODE  where 1=1 and (p.SINGLE_USERCNT is null or p.SINGLE_USERCNT <= 1)";
        if (this.txtPackageNameSearch.Text.Trim() != "")
        {
            strSql += " and p.packagename like '%" + CommonFunction.StringFilter(this.txtPackageNameSearch.Text.Trim()) + "%'";
        }

        if (this.txtPackageCodeSearch.Text.Trim() != "")
        {
            strSql += " and p.packagecode=  '" + CommonFunction.StringFilter(this.txtPackageCodeSearch.Text.Trim()) + "'";
        }

        strSql += " order by p.ID desc";

        DataSet ds = DbHelperOra.Query(strSql, false);
        DataView view = ds.Tables[0].DefaultView;
        this.myGridView.DataSource = view;
        this.myGridView.DataBind();
    }

    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        myGridView.PageIndex = e.NewPageIndex;
        BindGridView();
        
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpen2();", true);

        ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "unReport", "document.getElementById('popupDiv2').style.display = 'block';document.getElementById('bgDiv2').style.display = 'block';document.getElementById('bgDiv2').style.width = document.body.offsetWidth + 'px';document.getElementById('bgDiv2').style.height = screen.height + 'px';", true);
    }

    protected void myGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selIndex = myGridView.SelectedIndex;
        string strPackageCode = myGridView.Rows[selIndex].Cells[1].Text.ToString();
        string strPackageName = myGridView.Rows[selIndex].Cells[2].Text.ToString();
        string strTicketCount = myGridView.Rows[selIndex].Cells[3].Text.ToString();//总共可以生产的张数

        string strTicketRuleCode = myGridView.Rows[selIndex].Cells[6].Text.ToString().Trim();//使用规则

        if (string.IsNullOrEmpty(strTicketRuleCode) || strTicketRuleCode == "&nbsp;")
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error9").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "unReport", "document.getElementById('popupDiv2').style.display = 'block';document.getElementById('bgDiv2').style.display = 'block';document.getElementById('bgDiv2').style.width = document.body.offsetWidth + 'px';document.getElementById('bgDiv2').style.height = screen.height + 'px';", true);
        }
        else
        {
            this.txtPackageCode.Text = strPackageCode + " - " + myGridView.Rows[selIndex].Cells[7].Text.ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "unReport", "document.getElementById('Add').value = '清除';", true);
        }
        UpdatePanel7.Update();
    }

    [WebMethod]
    public static string SetRelatedVal(string strKey, string strType)
    {
        IssueInfoEntity issueinfoEntity = new IssueInfoEntity();
        issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity issueinfoDBEntity = new IssueInfoDBEntity();
        issueinfoDBEntity.RevlKey = strKey;
        issueinfoDBEntity.RevlType = strType;
        issueinfoEntity.IssueInfoDBEntity.Add(issueinfoDBEntity);
        DataSet dsResult = IssueInfoBP.GetRevlTypeVal(issueinfoEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["HOTELID"].ToString()))
        {
            return " 酒店:[" + dsResult.Tables[0].Rows[0]["HOTELID"].ToString() + "]" + dsResult.Tables[0].Rows[0]["HOTELNM"].ToString();
        }
        else
        {
            return "";
        }
    }
}