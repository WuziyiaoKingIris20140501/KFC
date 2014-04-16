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
using System.Text.RegularExpressions;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class InvoiceManagerDetail : BasePage
{
    InvoiceEntity _invoiceEntity = new InvoiceEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string InvoiceID = Request.QueryString["ID"].ToString();
                hidInvoiceID.Value = InvoiceID;
                //ddlOnlinebind();
                //BindTypeCityDDL();
                BindInvoiceDetail();
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                btnUpdate.Visible = false;
            }
        }
        else
        {
            detailMessageContent.InnerHtml = "";
        }
    }

    //public void ddlOnlinebind()
    //{
    //    DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
    //    DataTable dtResult = new DataTable();
    //    dtResult.Columns.Add("ONLINESTATUS");
    //    dtResult.Columns.Add("ONLINEDIS");
    //    if (dsResult.Tables.Count > 0)
    //    {
    //        dsResult.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
    //        dsResult.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";

    //        ddpStatusList.DataTextField = "ONLINEDIS";
    //        ddpStatusList.DataValueField = "ONLINESTATUS";
    //        ddpStatusList.DataSource = dsResult;
    //        ddpStatusList.DataBind();
    //    }
    //    return;
    //}

    //private void BindTypeCityDDL()
    //{
    //    _invoiceEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _invoiceEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _invoiceEntity.LogMessages.Username = UserSession.Current.UserDspName;

    //    DataSet dsResult = InvoiceBP.TypeSelect(_invoiceEntity).QueryResult;
    //    ddpTypeList.DataTextField = "TYPENM";
    //    ddpTypeList.DataValueField = "ID";
    //    ddpTypeList.DataSource = dsResult;
    //    ddpTypeList.DataBind();


    //    DataSet dsResultCity = InvoiceBP.CitySelect(_invoiceEntity).QueryResult;
    //    ddpCityList.DataTextField = "cityNM";
    //    ddpCityList.DataValueField = "cityid";
    //    ddpCityList.DataSource = dsResultCity;
    //    ddpCityList.DataBind();
    //}

    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    detailMessageContent.InnerHtml = "";
    //    btnAddDestinationType();
    //    BindDestinationListGrid();
    //}

    //清除控件中的数据
    private void clearValue()
    {
    }

    //发放渠道
    public void BindInvoiceDetail()
    {
        _invoiceEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _invoiceEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _invoiceEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _invoiceEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _invoiceEntity.InvoiceDBEntity = new List<InvoiceDBEntity>();
        InvoiceDBEntity invoiceDBEntity = new InvoiceDBEntity();

        invoiceDBEntity.InvoiceID = hidInvoiceID.Value;
        _invoiceEntity.InvoiceDBEntity.Add(invoiceDBEntity);

        DataSet dsMainResult = InvoiceBP.InvoiceDetailSelect(_invoiceEntity).QueryResult;

        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            lbCNFNUM.Text = dsMainResult.Tables[0].Rows[0]["CNFNUM"].ToString();
            lbStatus.Text = dsMainResult.Tables[0].Rows[0]["ONLINEDIS"].ToString();
            lbUSERID.Text = dsMainResult.Tables[0].Rows[0]["USERID"].ToString();
            lbCONTRACTTEL.Text = dsMainResult.Tables[0].Rows[0]["CONTRACTTEL"].ToString();
            lbAPPLYCHANEL.Text = dsMainResult.Tables[0].Rows[0]["APPLYCHANELNM"].ToString();
            lbAPPLYTIME.Text = dsMainResult.Tables[0].Rows[0]["APPLYTIME"].ToString();
            lbRECEIVERNAME.Text = dsMainResult.Tables[0].Rows[0]["RECEIVERNAME"].ToString();
            lbINVOICEHEAD.Text = dsMainResult.Tables[0].Rows[0]["INVOICEHEAD"].ToString();
            lbINVOICEADDR.Text = dsMainResult.Tables[0].Rows[0]["INVOICEADDR"].ToString();
            lbSENDTIME.Text = dsMainResult.Tables[0].Rows[0]["SENDTIME"].ToString();
            lbZIPCODE.Text = dsMainResult.Tables[0].Rows[0]["ZIPCODE"].ToString();
            lbINVOICEAMOUNT.Text = dsMainResult.Tables[0].Rows[0]["INVOICEAMOUNT"].ToString();
            lbSENDNAME.Text = dsMainResult.Tables[0].Rows[0]["SENDNAME"].ToString();
            lbSENDCODE.Text = dsMainResult.Tables[0].Rows[0]["SENDCODE"].ToString();
            lbINVOICENUM.Text = dsMainResult.Tables[0].Rows[0]["INVOICENUM"].ToString();
            lbOPERATOR.Text = dsMainResult.Tables[0].Rows[0]["OPERATOR"].ToString();
            hidOnlineStatus.Value = dsMainResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString();
            lbINVOICEBODY.Text = dsMainResult.Tables[0].Rows[0]["INVOICEBODY"].ToString();
            txtOPERATORMEMO.Text = dsMainResult.Tables[0].Rows[0]["OPERATORMEMO"].ToString();

            if ("1".Equals(hidOnlineStatus.Value))
            {
                txtINVOICENUM.Visible = true;
                btnBack.Visible = false;
            }
            else if ("2".Equals(hidOnlineStatus.Value))
            {
                txtSENDCODE.Visible = true;
                txtSENDNAME.Visible = true;
            }
            else if ("3".Equals(hidOnlineStatus.Value))
            {
                btnBack.Visible = true;
                btnUpdate.Visible = false;
            }
            else
            {
                btnBack.Visible = false;
                btnUpdate.Visible = false;
            }
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
        }

    }

    private static bool RegexValidate(string regexString, string validateString)
    {
        Regex regex = new Regex(regexString);
        return regex.IsMatch(validateString.Trim());
    }

    private static bool RegexValidateData(string validateString)
    {
        try
        {
            decimal dec = decimal.Parse(validateString);
            return true;
        }
        catch
        {
            return false;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";

        SaveAction("0");
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        if ("1".Equals(hidOnlineStatus.Value) && String.IsNullOrEmpty(txtINVOICENUM.Value.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString();
            return;
        }

        if ("2".Equals(hidOnlineStatus.Value) && String.IsNullOrEmpty(txtSENDCODE.Value.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
            return;
        }

        if ("2".Equals(hidOnlineStatus.Value) && String.IsNullOrEmpty(txtSENDNAME.Value.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError3").ToString();
            return;
        }

        if ("1".Equals(hidOnlineStatus.Value) && StringUtility.Text_Length(txtINVOICENUM.Value.Trim()) > 40)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError4").ToString();
            return;
        }

        if ("2".Equals(hidOnlineStatus.Value) && StringUtility.Text_Length(txtSENDCODE.Value.Trim()) > 40)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError5").ToString();
            return;
        }

        if ("2".Equals(hidOnlineStatus.Value) && StringUtility.Text_Length(txtSENDNAME.Value.Trim()) > 200)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError6").ToString();
            return;
        }

        if (StringUtility.Text_Length(txtOPERATORMEMO.Text.Trim()) > 2000)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError8").ToString();
            return;
        }

        SaveAction("1");
    }

    private void SaveAction(string ActionType)
    {
        _invoiceEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _invoiceEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _invoiceEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _invoiceEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _invoiceEntity.InvoiceDBEntity = new List<InvoiceDBEntity>();
        InvoiceDBEntity invoiceDBEntity = new InvoiceDBEntity();
        invoiceDBEntity.InvoiceID = hidInvoiceID.Value;
        invoiceDBEntity.INVOICENUM = txtINVOICENUM.Value.Trim();
        invoiceDBEntity.SENDCODE = txtSENDCODE.Value.Trim();
        invoiceDBEntity.SENDNAME = txtSENDNAME.Value.Trim();
        invoiceDBEntity.OnlineStatus = hidOnlineStatus.Value;
        invoiceDBEntity.Remark = txtOPERATORMEMO.Text;
        invoiceDBEntity.ActionType = ActionType;

        _invoiceEntity.InvoiceDBEntity.Add(invoiceDBEntity);
        int iResult = InvoiceBP.InvoiceUpdate(_invoiceEntity);

        _commonEntity.LogMessages = _invoiceEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "发票管理-保存";
        commonDBEntity.Event_ID = hidInvoiceID.Value;

        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, invoiceDBEntity.InvoiceID, (String.IsNullOrEmpty(txtINVOICENUM.Value.ToString().Trim()) ? lbINVOICENUM.Text : txtINVOICENUM.Value.ToString().Trim()), (String.IsNullOrEmpty(txtSENDNAME.Value.ToString().Trim()) ? lbSENDNAME.Text : txtSENDNAME.Value.ToString().Trim()), (String.IsNullOrEmpty(txtSENDCODE.Value.ToString().Trim()) ? lbSENDCODE.Text : txtSENDCODE.Value.ToString().Trim()));
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError7").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError7").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
}