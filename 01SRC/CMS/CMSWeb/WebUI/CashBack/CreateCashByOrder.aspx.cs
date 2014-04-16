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

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Text.RegularExpressions;

public partial class CreateCashByOrder : BasePage
{
    CashBackEntity _cashbackEntity = new CashBackEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidCommonType.Value = "0";
            hidInCommonType.Value = "0";

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
    }

    protected void btnSet_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(txtOrderNo.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("SaveError2").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
            return;
        }

        _cashbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cashbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cashbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cashbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _cashbackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        CashBackDBEntity cashbackDBEntity = new CashBackDBEntity();
        cashbackDBEntity.OrderNo = txtOrderNo.Text.Trim();
        _cashbackEntity.CashBackDBEntity.Add(cashbackDBEntity);

        DataSet dsResult = CashBackBP.BindOrderInfo(_cashbackEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
            return;
        }

        if (!"8".Equals(dsResult.Tables[0].Rows[0]["FOG_AUDITSTATUS"].ToString()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessageAudi").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
            return;
        }

        if (!"2".Equals(dsResult.Tables[0].Rows[0]["CashTaskStatus"].ToString()))
        {
            hidCashSN.Value = dsResult.Tables[0].Rows[0]["CashSN"].ToString();
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessageExist").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
            return;
        }

        lbOrderNo.Text = dsResult.Tables[0].Rows[0]["fog_order_num"].ToString();
        lbHotelNM.Text = dsResult.Tables[0].Rows[0]["hotel_name"].ToString();
        lbInDate.Text = dsResult.Tables[0].Rows[0]["INDATE"].ToString();
        lbConPer.Text = dsResult.Tables[0].Rows[0]["contact_name"].ToString();
        lbTiceketCode.Text = dsResult.Tables[0].Rows[0]["ticket_usercode"].ToString();
        lbBackAmount.Text = dsResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        lbBackCashAmount.Text = dsResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        lbOrderAmount.Text = dsResult.Tables[0].Rows[0]["book_total_price"].ToString();

        lbUserID.Text = dsResult.Tables[0].Rows[0]["login_mobile"].ToString();
        lbCanamount.Text = dsResult.Tables[0].Rows[0]["can_applictaion_amount"].ToString();
        lbAuditamount.Text = dsResult.Tables[0].Rows[0]["audit_amount"].ToString();
        lbPullingamount.Text = dsResult.Tables[0].Rows[0]["pulling_amount"].ToString();
        
        //txtHotelNM.Text = dsResult.Tables[0].Rows[0]["HOTELNM"].ToString();
        //txtHotelNMEN.Text = dsResult.Tables[0].Rows[0]["HotelNMEN"].ToString();
        //dpOpenDate.Value = dsResult.Tables[0].Rows[0]["OPENDATE"].ToString();
        //dpRepairDate.Value = dsResult.Tables[0].Rows[0]["REPAIRDATE"].ToString();
        //txtAddress.Text = dsResult.Tables[0].Rows[0]["ADDRESS"].ToString();
        //txtWebSite.Text = dsResult.Tables[0].Rows[0]["WEBSITE"].ToString();
        //txtPhone.Text = dsResult.Tables[0].Rows[0]["PHONE"].ToString();
        //txtFax.Text = dsResult.Tables[0].Rows[0]["FAX"].ToString();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(lbUserID.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("SaveError2").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
            return;
        }

        string strBackCashAmount = (!String.IsNullOrEmpty(lbBackCashAmount.Text.Trim()) && lbBackCashAmount.Text.Trim().IndexOf('.') >= 0) ? lbBackCashAmount.Text.Trim().Substring(0, lbBackCashAmount.Text.Trim().IndexOf('.')) : lbBackCashAmount.Text.Trim();
        if (String.IsNullOrEmpty(lbBackCashAmount.Text.Trim()) || !ChkNumber(strBackCashAmount))
        {
            messageContent.InnerHtml = GetLocalResourceObject("SaveError7").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
            return;
        }

        string errMsg = string.Empty;
        if ("0".Equals(hidCommonType.Value.Trim()) && (String.IsNullOrEmpty(txtBankOwner.Text.Trim()) || String.IsNullOrEmpty(txtBankName.Text.Trim()) || String.IsNullOrEmpty(txtBankBranch.Text.Trim()) || String.IsNullOrEmpty(txtBankCardNumber.Text.Trim())))
        {
            errMsg = GetLocalResourceObject("SaveError3").ToString();
        }
        else if ("1".Equals(hidCommonType.Value.Trim()) && (String.IsNullOrEmpty(txtBackTel.Text.Trim())))
        {
            errMsg = GetLocalResourceObject("SaveError4").ToString();
        }
        else if ("2".Equals(hidCommonType.Value.Trim()) && (String.IsNullOrEmpty(txtBao.Text.Trim())))
        {
            errMsg = GetLocalResourceObject("SaveError5").ToString();
        }
        else if ("2".Equals(hidCommonType.Value.Trim()) && (String.IsNullOrEmpty(txtBaoName.Text.Trim())))
        {
            errMsg = GetLocalResourceObject("SaveError8").ToString();
        }

        if (!String.IsNullOrEmpty(errMsg))
        {
            messageContent.InnerHtml = errMsg;
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
            return;
        }

        _cashbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cashbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cashbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cashbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _cashbackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        CashBackDBEntity cashbackDBEntity = new CashBackDBEntity();
        cashbackDBEntity.UserID = lbUserID.Text.Trim();
        cashbackDBEntity.OrderNo = lbOrderNo.Text.Trim();
        cashbackDBEntity.BackCashAmount = lbBackCashAmount.Text.Trim();
        cashbackDBEntity.BackCashType = hidCommonType.Value.Trim();
        cashbackDBEntity.BankOwner = txtBankOwner.Text.Trim();
        cashbackDBEntity.BankName = txtBankName.Text.Trim();
        cashbackDBEntity.BankBranch = txtBankBranch.Text.Trim();
        cashbackDBEntity.BankCardNumber = txtBankCardNumber.Text.Trim();
        cashbackDBEntity.BackTel = txtBackTel.Text.Trim();
        cashbackDBEntity.BackBao = txtBao.Text.Trim();
        cashbackDBEntity.BackBaoName = txtBaoName.Text.Trim();
        cashbackDBEntity.Remark = txtRemark.Text.Trim();
        cashbackDBEntity.BackInType = "0";
        _cashbackEntity.CashBackDBEntity.Add(cashbackDBEntity);
        _cashbackEntity = CashBackBP.SaveCashBackRequest(_cashbackEntity);
        int iResult = _cashbackEntity.Result;

        _commonEntity.LogMessages = _cashbackEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();
        commonDBEntity.Event_Type = "创建提现申请-保存";
        commonDBEntity.Event_ID = lbUserID.Text.Trim();
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();

        conTent = string.Format(conTent, cashbackDBEntity.UserID, cashbackDBEntity.BackCashAmount, cashbackDBEntity.BackCashType, cashbackDBEntity.BankName, cashbackDBEntity.BankBranch, cashbackDBEntity.BankCardNumber, cashbackDBEntity.BackTel, cashbackDBEntity.BackBao, cashbackDBEntity.Remark, cashbackDBEntity.BackInType);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SaveSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("SaveSuccess").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SaveError1").ToString() + _cashbackEntity.ErrorMSG;
            messageContent.InnerHtml = GetLocalResourceObject("SaveError1").ToString() + _cashbackEntity.ErrorMSG;
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('0');", true);
    }

    protected void btnInSet_Click(object sender, EventArgs e)
    {
        //inMessageContent.InnerHtml = "";

        //if (String.IsNullOrEmpty(txtInUserID.Text.Trim()))
        //{
        //    inMessageContent.InnerHtml = GetLocalResourceObject("SaveError6").ToString();
        //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('1');", true);
        //    return;
        //}

        //_cashbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_cashbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_cashbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        //_cashbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        //_cashbackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        //CashBackDBEntity cashbackDBEntity = new CashBackDBEntity();
        //cashbackDBEntity.UserID = txtInUserID.Text.Trim();
        //_cashbackEntity.CashBackDBEntity.Add(cashbackDBEntity);

        //DataSet dsResult = CashBackBP.BindOrderInfoByUser(_cashbackEntity).QueryResult;

        //if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        //{
        //    inMessageContent.InnerHtml = GetLocalResourceObject("WarningInMessage").ToString();
        //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('1');", true);
        //    return;
        //}

        //lbInUserID.Text = txtInUserID.Text.Trim();
        //lbInCanamount.Text = dsResult.Tables[0].Rows[0]["can_applictaion_amount"].ToString();
        //lbInAuditamount.Text = dsResult.Tables[0].Rows[0]["audit_amount"].ToString();
        //lbInPullingamount.Text = dsResult.Tables[0].Rows[0]["pulling_amount"].ToString();

        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('1');", true);
    }

    protected void btnInSave_Click(object sender, EventArgs e)
    {
        //inMessageContent.InnerHtml = "";
        //if (String.IsNullOrEmpty(lbInUserID.Text.Trim()))
        //{
        //    inMessageContent.InnerHtml = GetLocalResourceObject("SaveError6").ToString();
        //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('1');", true);
        //    return;
        //}

        //if (String.IsNullOrEmpty(txtInBackCashAmount.Text.Trim()) || !ChkNumber(txtInBackCashAmount.Text.Trim()))
        //{
        //    inMessageContent.InnerHtml = GetLocalResourceObject("SaveError7").ToString();
        //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('1');", true);
        //    return;
        //}

        //string errMsg = string.Empty;
        //if ("0".Equals(hidInCommonType.Value.Trim()) && (String.IsNullOrEmpty(txtInBankOwner.Text.Trim()) || String.IsNullOrEmpty(txtInBankName.Text.Trim()) || String.IsNullOrEmpty(txtInBankBranch.Text.Trim()) || String.IsNullOrEmpty(txtInBankCardNumber.Text.Trim())))
        //{
        //    errMsg = GetLocalResourceObject("SaveError3").ToString();
        //}
        //else if ("1".Equals(hidInCommonType.Value.Trim()) && (String.IsNullOrEmpty(txtInBackTel.Text.Trim())))
        //{
        //    errMsg = GetLocalResourceObject("SaveError4").ToString();
        //}
        //else if ("2".Equals(hidInCommonType.Value.Trim()) && (String.IsNullOrEmpty(txtInBao.Text.Trim())))
        //{
        //    errMsg = GetLocalResourceObject("SaveError5").ToString();
        //}

        //if (!String.IsNullOrEmpty(errMsg))
        //{
        //    inMessageContent.InnerHtml = errMsg;
        //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('1');", true);
        //    return;
        //}

        //_cashbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_cashbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_cashbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        //_cashbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        //_cashbackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        //CashBackDBEntity cashbackDBEntity = new CashBackDBEntity();
        //cashbackDBEntity.UserID = lbInUserID.Text.Trim();
        //cashbackDBEntity.BackCashAmount = txtInBackCashAmount.Text.Trim();
        //cashbackDBEntity.BackCashType = hidInCommonType.Value.Trim();
        //cashbackDBEntity.BankOwner = txtInBankOwner.Text.Trim();
        //cashbackDBEntity.BankName = txtInBankName.Text.Trim();
        //cashbackDBEntity.BankBranch = txtInBankBranch.Text.Trim();
        //cashbackDBEntity.BankCardNumber = txtInBankCardNumber.Text.Trim();
        //cashbackDBEntity.BackTel = txtInBackTel.Text.Trim();
        //cashbackDBEntity.BackBao = txtInBao.Text.Trim();
        //cashbackDBEntity.Remark = txtInReMark.Text.Trim();
        //cashbackDBEntity.BackInType = "1";
        //_cashbackEntity.CashBackDBEntity.Add(cashbackDBEntity);
        //_cashbackEntity = CashBackBP.SaveCashBackRequest(_cashbackEntity);
        //int iResult = _cashbackEntity.Result;

        //_commonEntity.LogMessages = _cashbackEntity.LogMessages;
        //_commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        //CommonDBEntity commonDBEntity = new CommonDBEntity();
        //commonDBEntity.Event_Type = "创建提现申请-保存";
        //commonDBEntity.Event_ID = lbInUserID.Text.Trim();
        //string conTent = GetLocalResourceObject("EventInsertMessage").ToString();

        //conTent = string.Format(conTent, cashbackDBEntity.UserID, cashbackDBEntity.BackCashAmount, cashbackDBEntity.BackCashType, cashbackDBEntity.BankName, cashbackDBEntity.BankBranch, cashbackDBEntity.BankCardNumber, cashbackDBEntity.BackTel, cashbackDBEntity.BackBao, cashbackDBEntity.Remark, cashbackDBEntity.BackInType);
        //commonDBEntity.Event_Content = conTent;

        //if (iResult == 1)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("SaveSuccess").ToString();
        //    inMessageContent.InnerHtml = GetLocalResourceObject("SaveSuccess").ToString();
        //}
        //else
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("SaveError1").ToString() + _cashbackEntity.ErrorMSG;
        //    inMessageContent.InnerHtml = GetLocalResourceObject("SaveError1").ToString() + _cashbackEntity.ErrorMSG;
        //}
        //_commonEntity.CommonDBEntity.Add(commonDBEntity);
        //CommonBP.InsertEventHistory(_commonEntity);

        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "RestLoadStyle('1');", true);
    }

    private bool ChkNumber(string param)
    {
        bool bReturn = true;

        if (String.IsNullOrEmpty(param))
        {
            return true;
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

    public void BindHotelInfo()
    {
        //MessageContent.InnerHtml = "";
        //_cashbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_cashbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_cashbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        //_cashbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        //_cashbackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        //CashBackDBEntity cashbackDBEntity = new CashBackDBEntity();
        //cashbackDBEntity.HotelID = hidHotelID.Value;
        //_cashbackEntity.CashBackDBEntity.Add(cashbackDBEntity);
        //DataSet dsResult = HotelInfoBP.BindHotelList(_cashbackEntity).QueryResult;

        //if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        //{
        //    RestControlValue();
        //    chkAutoTrust.Checked = true;
        //    return;
        //}

        //txtHotelNM.Text = dsResult.Tables[0].Rows[0]["HOTELNM"].ToString();
        //txtHotelNMEN.Text = dsResult.Tables[0].Rows[0]["HotelNMEN"].ToString();
        //dpOpenDate.Value = dsResult.Tables[0].Rows[0]["OPENDATE"].ToString();
        //dpRepairDate.Value = dsResult.Tables[0].Rows[0]["REPAIRDATE"].ToString();
        //txtAddress.Text = dsResult.Tables[0].Rows[0]["ADDRESS"].ToString();
        //txtWebSite.Text = dsResult.Tables[0].Rows[0]["WEBSITE"].ToString();
        //txtPhone.Text = dsResult.Tables[0].Rows[0]["PHONE"].ToString();
        //txtFax.Text = dsResult.Tables[0].Rows[0]["FAX"].ToString();
    }
}