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

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class InvoiceManagerSearch : BasePage
{
    InvoiceEntity _invoiceEntity = new InvoiceEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            messageContent.InnerHtml = "";
            ddlOnlinebind();

            if (!String.IsNullOrEmpty(Request.QueryString["CNF"]))
            {
                ViewState["USERID"] = "";
                ViewState["CNFNUM"] = Request.QueryString["CNF"].ToString();
                ViewState["SENDCODE"] = "";
                ViewState["INVSTATUS"] = "";
                ViewState["APPLYCHANEL"] = "";
                ViewState["APPLYBEGINDATE"] = "";
                ViewState["APPLYENDDATE"] = "";
                ViewState["SENDBEGINDATE"] = "";
                ViewState["SENDENDDATE"] = "";
            }

            BindInvoiceListGrid("0");
            hidType.Value = "0";
        }
    }

    public void ddlOnlinebind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("InvoiceStatus").ToString());
        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "INVOICESTATUS";
            dsResult.Tables[0].Columns["Value"].ColumnName = "INVOICEDIS";


            DataRow drTemp = dsResult.Tables[0].NewRow();
            drTemp["INVOICESTATUS"] = DBNull.Value;
            drTemp["INVOICEDIS"] = "不限制";
            dsResult.Tables[0].Rows.InsertAt(drTemp, 0);

            ddpSTATUS.DataTextField = "INVOICEDIS";
            ddpSTATUS.DataValueField = "INVOICESTATUS";
            ddpSTATUS.DataSource = dsResult;
            ddpSTATUS.DataBind();
        }


        DataSet dsApplyResult = CommonBP.GetConfigList(GetLocalResourceObject("ApplyChanel").ToString());
        if (dsApplyResult.Tables.Count > 0)
        {
            dsApplyResult.Tables[0].Columns["Key"].ColumnName = "APPLYCHANEL";
            dsApplyResult.Tables[0].Columns["Value"].ColumnName = "APPLYCHANELDIS";


            DataRow drTemp = dsApplyResult.Tables[0].NewRow();
            drTemp["APPLYCHANEL"] = DBNull.Value;
            drTemp["APPLYCHANELDIS"] = "不限制";
            dsApplyResult.Tables[0].Rows.InsertAt(drTemp, 0);

            ddpAPPLYCHANEL.DataTextField = "APPLYCHANELDIS";
            ddpAPPLYCHANEL.DataValueField = "APPLYCHANEL";
            ddpAPPLYCHANEL.DataSource = dsApplyResult;
            ddpAPPLYCHANEL.DataBind();
        }
    }

    //发放渠道
    public void BindInvoiceListGrid(string Type)
    {
        _invoiceEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _invoiceEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _invoiceEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _invoiceEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _invoiceEntity.InvoiceDBEntity = new List<InvoiceDBEntity>();
        InvoiceDBEntity invoiceDBEntity = new InvoiceDBEntity();
        
        //invoiceDBEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
     
        invoiceDBEntity.USERID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["USERID"].ToString())) ? null : ViewState["USERID"].ToString();
        invoiceDBEntity.CNFNUM = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CNFNUM"].ToString())) ? null : ViewState["CNFNUM"].ToString();
        invoiceDBEntity.SENDCODE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SENDCODE"].ToString())) ? null : ViewState["SENDCODE"].ToString();
        invoiceDBEntity.Status = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["INVSTATUS"].ToString())) ? null : ViewState["INVSTATUS"].ToString();
        invoiceDBEntity.APPLYCHANEL = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["APPLYCHANEL"].ToString())) ? null : ViewState["APPLYCHANEL"].ToString();

        invoiceDBEntity.APPLYBEGINDATE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["APPLYBEGINDATE"].ToString())) ? null : ViewState["APPLYBEGINDATE"].ToString();
        invoiceDBEntity.APPLYENDDATE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["APPLYENDDATE"].ToString())) ? null : ViewState["APPLYENDDATE"].ToString();
        invoiceDBEntity.SENDBEGINDATE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SENDBEGINDATE"].ToString())) ? null : ViewState["SENDBEGINDATE"].ToString();
        invoiceDBEntity.SENDENDDATE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SENDENDDATE"].ToString())) ? null : ViewState["SENDENDDATE"].ToString();
        invoiceDBEntity.SelectType = Type;
        _invoiceEntity.InvoiceDBEntity.Add(invoiceDBEntity);

        DataSet dsResult = InvoiceBP.InvoiceListSelect(_invoiceEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSList.DataBind();

        if (!String.IsNullOrEmpty(refushFlag.Value))
        {
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            refushFlag.Value = "";
        }
    }

    protected void gridViewCSList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        ViewState["USERID"] = txtUSERID.Value.ToString().Trim();
        ViewState["CNFNUM"] = txtCNFNUM.Value.ToString().Trim();
        ViewState["SENDCODE"] = txtSENDCODE.Value.ToString().Trim();
        ViewState["INVSTATUS"] = ddpSTATUS.SelectedValue;
        ViewState["APPLYCHANEL"] = ddpAPPLYCHANEL.SelectedValue;

        ViewState["APPLYBEGINDATE"] = dpApplyBeginDate.Value;
        ViewState["APPLYENDDATE"] = dpApplyEndDate.Value;
        ViewState["SENDBEGINDATE"] = dpSendBeginDate.Value;
        ViewState["SENDENDDATE"] = dpSendEndDate.Value;

        BindInvoiceListGrid("1");
        hidType.Value = "1";
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        _invoiceEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _invoiceEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _invoiceEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _invoiceEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _invoiceEntity.InvoiceDBEntity = new List<InvoiceDBEntity>();
        InvoiceDBEntity invoiceDBEntity = new InvoiceDBEntity();
        
        //invoiceDBEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
     
        invoiceDBEntity.USERID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["USERID"].ToString())) ? null : ViewState["USERID"].ToString();
        invoiceDBEntity.CNFNUM = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CNFNUM"].ToString())) ? null : ViewState["CNFNUM"].ToString();
        invoiceDBEntity.SENDCODE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SENDCODE"].ToString())) ? null : ViewState["SENDCODE"].ToString();
        invoiceDBEntity.Status = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["INVSTATUS"].ToString())) ? null : ViewState["INVSTATUS"].ToString();
        invoiceDBEntity.APPLYCHANEL = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["APPLYCHANEL"].ToString())) ? null : ViewState["APPLYCHANEL"].ToString();

        invoiceDBEntity.APPLYBEGINDATE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["APPLYBEGINDATE"].ToString())) ? null : ViewState["APPLYBEGINDATE"].ToString();
        invoiceDBEntity.APPLYENDDATE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["APPLYENDDATE"].ToString())) ? null : ViewState["APPLYENDDATE"].ToString();
        invoiceDBEntity.SENDBEGINDATE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SENDBEGINDATE"].ToString())) ? null : ViewState["SENDBEGINDATE"].ToString();
        invoiceDBEntity.SENDENDDATE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SENDENDDATE"].ToString())) ? null : ViewState["SENDENDDATE"].ToString();
        invoiceDBEntity.SelectType = hidType.Value;
        _invoiceEntity.InvoiceDBEntity.Add(invoiceDBEntity);

        DataSet dsResult = InvoiceBP.InvoiceListExcelSelect(_invoiceEntity).QueryResult;

        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count == 0)
        {
            return;
        }
        CommonFunction.ExportExcelForLM(dsResult);
    }

    protected void gridViewCSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSList.PageIndex = e.NewPageIndex;
        BindInvoiceListGrid("1");
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
}