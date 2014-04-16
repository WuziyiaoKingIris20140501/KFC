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

public partial class SettlementUnitPage : BasePage
{
    SeltEntity _seltEntity = new SeltEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type=city") && !this.Page.Request.QueryString.ToString().Contains("Type=hotel") && !this.Page.Request.QueryString.ToString().Contains("Type=sales"))
        {
            AspNetPager1.AlwaysShow = false;
            ViewState["UnitName"] = "";
            ViewState["CityID"] = "";
            ViewState["InvoiceName"] = "";
            BandDDpList();
        }
    }

    private void BandDDpList()
    {
        DataTable dtTermType = GetTermData();
        ddpterm.DataSource = dtTermType;
        ddpterm.DataTextField = "TERM_TEXT";
        ddpterm.DataValueField = "TERM_STATUS";
        ddpterm.DataBind();
        ddpterm.SelectedIndex = 0;

        ddptermStDt.DataSource = dtTermType;
        ddptermStDt.DataTextField = "TERM_TEXT";
        ddptermStDt.DataValueField = "TERM_STATUS";
        ddptermStDt.DataBind();
        ddptermStDt.SelectedIndex = 0;
    }

    private DataTable GetTermData()
    {
        DataTable dt = new DataTable();
        DataColumn TermStatus = new DataColumn("TERM_STATUS");
        DataColumn TermStatusText = new DataColumn("TERM_TEXT");
        dt.Columns.Add(TermStatus);
        dt.Columns.Add(TermStatusText);
        for (int i = 1; i < 29; i++)
        {
            DataRow dr = dt.NewRow();
            dr["TERM_STATUS"] = i.ToString();
            dr["TERM_TEXT"] = i.ToString();
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewUserListGrid();
    }

    private void BindReviewUserListGrid()
    {
        _seltEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _seltEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _seltEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _seltEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _seltEntity.SeltDBEntity = new List<SeltDBEntity>();
        SeltDBEntity seltEntity = new SeltDBEntity();

        seltEntity.UnitNm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UnitName"].ToString())) ? null : ViewState["UnitName"].ToString();
        seltEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        seltEntity.InvoiceNm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InvoiceName"].ToString())) ? null : ViewState["InvoiceName"].ToString();

        _seltEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _seltEntity.PageSize = gridViewCSReviewUserList.PageSize;
        _seltEntity.SeltDBEntity.Add(seltEntity);
        DataSet dsResult = SeltBP.ReviewSelect(_seltEntity).QueryResult;
       
        gridViewCSReviewUserList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewUserList.DataKeyNames = new string[] { "UNITID" };//主键
        gridViewCSReviewUserList.DataBind();

        AspNetPager1.PageSize = gridViewCSReviewUserList.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetAClickEvent();", true);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "SetAClickEvent()", true);  
    }

    private int CountLmSystemLog()
    {
        _seltEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _seltEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _seltEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _seltEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _seltEntity.SeltDBEntity = new List<SeltDBEntity>();
        SeltDBEntity seltEntity = new SeltDBEntity();

        seltEntity.UnitNm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UnitName"].ToString())) ? null : ViewState["UnitName"].ToString();
        seltEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        seltEntity.InvoiceNm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InvoiceName"].ToString())) ? null : ViewState["InvoiceName"].ToString();

        _seltEntity.SeltDBEntity.Add(seltEntity);
        DataSet dsResult = SeltBP.ReviewSelectCount(_seltEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
        }

        return 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        ViewState["UnitName"] = txtUnitName.Value.Trim();
        string strCity = hidCity.Value.ToString().Trim();
        ViewState["CityID"] = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity;
        ViewState["InvoiceName"] = txtInvoiceName.Value.Trim();

        AspNetPager1.AlwaysShow = true;
        AspNetPager1.CurrentPageIndex = 1;
        BindReviewUserListGrid();
    }

    protected void btnHotelADD_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        string strHotel = hidSelectHotel.Value.ToString().Trim();
        if (!strHotel.Contains("[") || !strHotel.Contains("]"))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorHotel").ToString();
            //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "BtnHotelErrStyle();", true);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnHotelErrStyle();", true);
            return;
        }

        if (chkLMBAR.Checked == false && chkLMBAR2.Checked == false)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorPriceCode").ToString();
            //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "BtnHotelErrStyle();", true);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnHotelErrStyle();", true);
            return;
        }

        string hotelID = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
        string hotelNM = strHotel.Substring((strHotel.IndexOf(']') + 1));
        string priceCD = (((chkLMBAR.Checked) ? "1" : "0") + ((chkLMBAR2.Checked) ? "1" : "0")).PadRight(20, '0');
        string priceNM = ((chkLMBAR.Checked) ? "预付 " : "") + ((chkLMBAR2.Checked) ? "现付" : "");
        string vendorCD = ((chkALL.Checked) ? "1" : "0") .PadRight(20, '0');

        DataTable dtResult;
        dtResult = new DataTable();
        dtResult.Columns.Add("HOTELID");
        dtResult.Columns.Add("HOTELNM");
        dtResult.Columns.Add("ODTYPE");
        dtResult.Columns.Add("PRICECD");
        dtResult.Columns.Add("VENDOR");
        dtResult.Columns.Add("ACTYPE");

        if (gridViewHotelList.Rows.Count > 0)
        {
            for (int i = 0; i < gridViewHotelList.Rows.Count; i++)
            {
                if (hotelID.Equals(gridViewHotelList.DataKeys[i].Values[0].ToString()))
                {
                    detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorHotelAdd").ToString();
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnHotelErrStyle();", true);
                    return;
                }

                DataRow drRow = dtResult.NewRow();
                drRow["HOTELID"] = gridViewHotelList.DataKeys[i].Values[0].ToString();
                drRow["HOTELNM"] = gridViewHotelList.DataKeys[i].Values[1].ToString();
                drRow["ODTYPE"] = gridViewHotelList.DataKeys[i].Values[2].ToString();
                drRow["PRICECD"] = gridViewHotelList.DataKeys[i].Values[3].ToString();
                drRow["VENDOR"] = gridViewHotelList.DataKeys[i].Values[4].ToString();
                drRow["ACTYPE"] = gridViewHotelList.DataKeys[i].Values[5].ToString();

                dtResult.Rows.Add(drRow);
            }
        }

        DataRow drNewRow = dtResult.NewRow();
        drNewRow["HOTELID"] = hotelID;
        drNewRow["HOTELNM"] = hotelNM;
        drNewRow["ODTYPE"] = priceNM;
        drNewRow["PRICECD"] = priceCD;
        drNewRow["VENDOR"] = vendorCD;
        drNewRow["ACTYPE"] = "1";

        dtResult.Rows.Add(drNewRow);

        gridViewHotelList.DataSource = dtResult;
        gridViewHotelList.DataKeyNames = new string[] { "HOTELID","HOTELNM", "ODTYPE", "PRICECD", "VENDOR", "ACTYPE"};//主键
        gridViewHotelList.DataBind();
        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "invokeOpenList();", true);
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenList();", true);
    }

    protected void btnDeletePopupArea_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        string strHotel = hidDelHotelID.Value.ToString().Trim();

        DataTable dtResult;
        dtResult = new DataTable();
        dtResult.Columns.Add("HOTELID");
        dtResult.Columns.Add("HOTELNM");
        dtResult.Columns.Add("ODTYPE");
        dtResult.Columns.Add("PRICECD");
        dtResult.Columns.Add("VENDOR");
        dtResult.Columns.Add("ACTYPE");

        if (gridViewHotelList.Rows.Count > 0)
        {
            for (int i = 0; i < gridViewHotelList.Rows.Count; i++)
            {
                if (strHotel.Equals(gridViewHotelList.DataKeys[i].Values[0].ToString()))
                {
                    continue;
                }
                DataRow drRow = dtResult.NewRow();
                drRow["HOTELID"] = gridViewHotelList.DataKeys[i].Values[0].ToString();
                drRow["HOTELNM"] = gridViewHotelList.DataKeys[i].Values[1].ToString();
                drRow["ODTYPE"] = gridViewHotelList.DataKeys[i].Values[2].ToString();
                drRow["PRICECD"] = gridViewHotelList.DataKeys[i].Values[3].ToString();
                drRow["VENDOR"] = gridViewHotelList.DataKeys[i].Values[4].ToString();
                drRow["ACTYPE"] = gridViewHotelList.DataKeys[i].Values[5].ToString();

                dtResult.Rows.Add(drRow);
            }
        }

        gridViewHotelList.DataSource = dtResult;
        gridViewHotelList.DataKeyNames = new string[] { "HOTELID", "HOTELNM", "ODTYPE", "PRICECD", "VENDOR", "ACTYPE" };//主键
        gridViewHotelList.DataBind();
        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "invokeOpenList();", true);
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenList();", true);
    }

    protected void btnReLoad_Click(object sender, EventArgs e)
    {
        dvUnitMessageContent.InnerHtml = "";
        MessageContent.InnerHtml = "";
        if ("1".Equals(hidActionType.Value.Trim()))
        {
            txt_UnitNm.Text = "";
            txt_InvoiceNm.Text = "";
            ddpterm.SelectedIndex = 0;
            ddptermStDt.SelectedIndex = 0;
            txt_tax.Text = "";
            txt_Per.Text = "";
            txt_Tel.Text = "";
            txt_fax.Text = "";
            //txt_sales.Text = "";
            txt_address.Text = "";

            txt_billItem.Text = "";
            txt_taxno.Text = "";
            txt_payno.Text = "";
            rdbOnL.Checked = true;

            hidSales.Value = "";
            gridViewHotelList.DataSource = null;
            gridViewHotelList.DataBind();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenList();", true);
            //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        _seltEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _seltEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _seltEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _seltEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _seltEntity.SeltDBEntity = new List<SeltDBEntity>();
        SeltDBEntity seltEntity = new SeltDBEntity();

        seltEntity.SeltID = hidUnitID.Value.Trim();
        _seltEntity.SeltDBEntity.Add(seltEntity);
        _seltEntity = SeltBP.ReLoadDetialInfo(_seltEntity);

        DataSet dsResult = _seltEntity.QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            txt_UnitNm.Text = dsResult.Tables[0].Rows[0]["unit_name"].ToString().Trim();
            txt_InvoiceNm.Text = dsResult.Tables[0].Rows[0]["invoice_name"].ToString().Trim();
            ddpterm.SelectedValue = dsResult.Tables[0].Rows[0]["settlement_term"].ToString().Trim();
            ddptermStDt.SelectedValue = dsResult.Tables[0].Rows[0]["term_stdt"].ToString().Trim();
            txt_tax.Text = dsResult.Tables[0].Rows[0]["hotel_tax"].ToString().Trim();
            txt_Per.Text = dsResult.Tables[0].Rows[0]["settlement_per"].ToString().Trim();
            txt_Tel.Text = dsResult.Tables[0].Rows[0]["settlement_tel"].ToString().Trim();
            txt_fax.Text = dsResult.Tables[0].Rows[0]["settlement_fax"].ToString().Trim();
            //txt_sales.Text = dsResult.Tables[0].Rows[0]["settlement_sales"].ToString().Trim();
            txt_address.Text = dsResult.Tables[0].Rows[0]["settlement_address"].ToString().Trim();

            txt_billItem.Text = dsResult.Tables[0].Rows[0]["bill_item"].ToString().Trim();
            txt_taxno.Text = dsResult.Tables[0].Rows[0]["hotel_taxno"].ToString().Trim();
            txt_payno.Text = dsResult.Tables[0].Rows[0]["hotel_payno"].ToString().Trim();
            rdbOnL.Checked = (dsResult.Tables[0].Rows[0]["status"].ToString() == "1") ? true : false;

            hidSales.Value = dsResult.Tables[0].Rows[0]["settlement_sales_nm"].ToString().Trim();
        }

        DataTable dtResult = _seltEntity.SeltDBEntity[0].dtHotelList;

        gridViewHotelList.DataSource = dtResult;
        gridViewHotelList.DataKeyNames = new string[] { "HOTELID", "HOTELNM", "ODTYPE", "PRICECD", "VENDOR", "ACTYPE" };//主键
        gridViewHotelList.DataBind();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenList();", true);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strErrMsg = string.Empty;
        bool bFlag = true;

        if (String.IsNullOrEmpty(txt_UnitNm.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave1").ToString() + "&nbsp;";
            bFlag = false;
        }
        if (String.IsNullOrEmpty(txt_InvoiceNm.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave2").ToString() + "&nbsp;";
            bFlag = false;
        }
        if (String.IsNullOrEmpty(txt_tax.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave3").ToString() + "&nbsp;";
            bFlag = false;
        }
        else
        {
            if (!ChkBalVal(txt_tax.Text.Trim()))
            {
                strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave13").ToString() + "&nbsp;";
                bFlag = false;
            }
        }
        if (String.IsNullOrEmpty(txt_Per.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave4").ToString() + "&nbsp;";
            bFlag = false;
        }
        if (String.IsNullOrEmpty(txt_Tel.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave5").ToString() + "&nbsp;";
            bFlag = false;
        }
        if (String.IsNullOrEmpty(txt_fax.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave6").ToString() + "&nbsp;";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(hidSales.Value.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave7").ToString() + "&nbsp;";
            bFlag = false;
        }
        else
        {
            if (!hidSales.Value.Trim().Contains("[") || !hidSales.Value.Trim().Contains("]"))
            {
                strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave14").ToString() + "&nbsp;";
                bFlag = false;
            }
        }


        if (String.IsNullOrEmpty(txt_address.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave8").ToString() + "&nbsp;";
            bFlag = false;
        }
        if (String.IsNullOrEmpty(txt_billItem.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave9").ToString() + "&nbsp;";
            bFlag = false;
        }
        if (String.IsNullOrEmpty(txt_taxno.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave10").ToString() + "&nbsp;";
            bFlag = false;
        }
        if (String.IsNullOrEmpty(txt_payno.Text.Trim()))
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave11").ToString() + "&nbsp;";
            bFlag = false;
        }
        if (gridViewHotelList.Rows.Count == 0)
        {
            strErrMsg = strErrMsg + GetLocalResourceObject("ErrorSave12").ToString() + "&nbsp;";
            bFlag = false;
        }

        if (!bFlag)
        {
            dvUnitMessageContent.InnerHtml = strErrMsg;
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenList();", true);
            return;
        }

        DataTable dtResult;
        dtResult = new DataTable();
        dtResult.Columns.Add("HOTELID");
        dtResult.Columns.Add("HOTELNM");
        dtResult.Columns.Add("ODTYPE");
        dtResult.Columns.Add("PRICECD");
        dtResult.Columns.Add("VENDOR");
        dtResult.Columns.Add("ACTYPE");
        string strHotelList = string.Empty;
        if (gridViewHotelList.Rows.Count > 0)
        {
            for (int i = 0; i < gridViewHotelList.Rows.Count; i++)
            {
                strHotelList = strHotelList + gridViewHotelList.DataKeys[i].Values[0].ToString() + ",";
                DataRow drRow = dtResult.NewRow();
                drRow["HOTELID"] = gridViewHotelList.DataKeys[i].Values[0].ToString();
                drRow["HOTELNM"] = gridViewHotelList.DataKeys[i].Values[1].ToString();
                drRow["ODTYPE"] = gridViewHotelList.DataKeys[i].Values[2].ToString();
                drRow["PRICECD"] = gridViewHotelList.DataKeys[i].Values[3].ToString();
                drRow["VENDOR"] = gridViewHotelList.DataKeys[i].Values[4].ToString();
                drRow["ACTYPE"] = gridViewHotelList.DataKeys[i].Values[5].ToString();

                dtResult.Rows.Add(drRow);
            }
        }
        strHotelList = strHotelList.TrimEnd(',');
        //check value
        _seltEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _seltEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _seltEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _seltEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _seltEntity.SeltDBEntity = new List<SeltDBEntity>();
        SeltDBEntity seltEntity = new SeltDBEntity();

        seltEntity.SeltID = hidUnitID.Value.Trim();
        seltEntity.UnitNm = txt_UnitNm.Text.Trim();
        seltEntity.InvoiceNm = txt_InvoiceNm.Text.Trim();
        seltEntity.Term = ddpterm.SelectedValue.Trim();
        seltEntity.TermStDt = ddptermStDt.SelectedValue.Trim();
        seltEntity.Tax = txt_tax.Text.Trim();
        seltEntity.Per = txt_Per.Text.Trim();
        seltEntity.Tel = txt_Tel.Text.Trim();
        seltEntity.Fax = txt_fax.Text.Trim();

        string salesID = hidSales.Value.Trim();
        seltEntity.Sales = (salesID.IndexOf(']') > 0) ? salesID.Substring(0, salesID.IndexOf(']')).Trim('[').Trim(']') : salesID;  //txt_sales.Text.Trim();
        seltEntity.Address = txt_address.Text.Trim();

        seltEntity.BillItem = txt_billItem.Text.Trim();
        seltEntity.TaxNo = txt_taxno.Text.Trim();
        seltEntity.PayNo = txt_payno.Text.Trim();
        seltEntity.Status = (rdbOnL.Checked) ? "1" : "0";

        seltEntity.dtHotelList = dtResult;
        _seltEntity.SeltDBEntity.Add(seltEntity);

        _seltEntity = SeltBP.SaveSettlementInfo(_seltEntity);

        int iResult = _seltEntity.Result;
        _commonEntity.LogMessages = _seltEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店结算单位-保存";
        commonDBEntity.Event_ID = seltEntity.UnitNm;
        string conTent = GetLocalResourceObject("EventSaveMessage").ToString();

        conTent = string.Format(conTent, seltEntity.UnitNm, seltEntity.InvoiceNm, seltEntity.Term, seltEntity.TermStDt, seltEntity.Tax, seltEntity.Per, seltEntity.Tel, seltEntity.Fax, seltEntity.Sales, seltEntity.Address, seltEntity.BillItem, seltEntity.TaxNo, seltEntity.PayNo, seltEntity.Status, strHotelList);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("CreateSuccess").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("CreateSuccess").ToString();

            AspNetPager1.AlwaysShow = true;
            AspNetPager1.CurrentPageIndex = 1;
            BindReviewUserListGrid();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("CreateError").ToString();
            dvUnitMessageContent.InnerHtml = GetLocalResourceObject("CreateError").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenList();", true);
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    private bool ChkBalVal(string param)
    {
        bool bReturn = true;
        if (String.IsNullOrEmpty(param))
        {
            return false;
        }

        if (!ChkDouble(param) || decimal.Parse(param) < 0 || 100 < decimal.Parse(param))
        {
            return false;
        }

        return bReturn;
    }

    private bool ChkDouble(string param)
    {
        bool bReturn = true;

        if (String.IsNullOrEmpty(param))
        {
            return true;
        }

        try
        {
            return IsVald(param);
        }
        catch
        {
            bReturn = false;
        }

        return bReturn;
    }

    private bool IsVald(string str)
    {
        bool flog = false;
        //string strPatern = @"^([0-9]\d*)$";  /^(?:\d+(?:\.\d{2})?|\.\d{2})$/

        string strPatern = @"^(?:\d+(?:\.\d{0,2})?|\.\d{0,2})$";
        Regex reg = new Regex(strPatern);
        if (reg.IsMatch(str))
        {
            flog = true;
        }
        return flog;
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
        string strPatern = @"^([0-9]\d*)$";
        Regex reg = new Regex(strPatern);
        if (reg.IsMatch(str))
        {
            flog = true;
        }
        return flog;
    }

}