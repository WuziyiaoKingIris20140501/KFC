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

public partial class HotelSaveManager : BasePage
{
    HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //divModify.Style.Add("display", "none");
            divBtnList.Style.Add("display", "none");
            dvSales.Style.Add("display", "none");
            dvBalSearch.Style.Add("display", "none");
            dvBalAdd.Style.Add("display", "none");
            //BindDdpList();
            //RestControlEnable(false, 0);
            //SetBtnSearchClick();
            //wctSales.CTLDISPLAY = "1";

            if (Request.Cookies["CityName"] != null)
            {
                string strCityName = Request.Cookies["CityName"].Value;

                if (!string.IsNullOrEmpty(strCityName))
                {
                    HttpCookie HCdspname = new HttpCookie("CityName", HttpUtility.UrlEncode("beijing"));
                    HttpContext.Current.Response.AppendCookie(HCdspname);
                }

                //wctHotel.extraParams = strCityName;
            }
            else
            {
                HttpCookie HCdspname = new HttpCookie("CityName", HttpUtility.UrlEncode("beijing"));
                HttpContext.Current.Response.AppendCookie(HCdspname);
            }
            
            //wctHotel.extraParams = "dd";
        }
    }

    private void SetBtnSearchClick()
    {
        if (!String.IsNullOrEmpty(Request.QueryString["hid"]))
        {
            string HotelID = Request.QueryString["hid"].ToString().Trim();
            
            MessageContent.InnerHtml = "";
            _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
            HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
            hotelInfoDBEntity.HotelID = HotelID;
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            DataSet dsResult = HotelInfoBP.ChkLMPropHotelList(_hotelinfoEntity).QueryResult;

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                //wctHotel.AutoResult = dsResult.Tables[0].Rows[0]["REVALUE_ALL"].ToString();
                SetSelectHotelControl();
            }
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        SetSelectHotelControl();
    }

    protected void btnSaveSales_Click(object sender, EventArgs e)
    {
        hidSelectedID.Value = "1";
        MessageContent.InnerHtml = "";
        bool bFlag = true;
        string msgString = "";

        if (String.IsNullOrEmpty(wctSales.AutoResult) || !wctSales.AutoResult.Trim().Equals(hidSalesID.Value.Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError14").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(dpSalesStart.Value) || String.IsNullOrEmpty(dpSalesEnd.Value))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError15").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtPhone.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError61").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtFax.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError71").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtPhone.Text.ToString().Trim()) && (StringUtility.Text_Length(txtPhone.Text.ToString().Trim()) > 40))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError6").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtFax.Text.ToString().Trim()) && (StringUtility.Text_Length(txtFax.Text.ToString().Trim()) > 20))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError7").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtContactPer.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("UpdateError10").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtContactEmail.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("UpdateError11").ToString() + "<br/>";
            bFlag = false;
        }

        if (!bFlag)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError13").ToString() + "<br/>" + msgString;
            RestBalGridData();
            return;
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.Name_CN = txtHotelNM.Text.Trim();
        hotelInfoDBEntity.SalesID = hidSalesID.Value.Substring((hidSalesID.Value.IndexOf('[') + 1), (hidSalesID.Value.IndexOf(']') - 1));
        hotelInfoDBEntity.Phone = txtPhone.Text.Trim();
        hotelInfoDBEntity.Fax = txtFax.Text.Trim();
        hotelInfoDBEntity.ContactPer = txtContactPer.Text.Trim();
        hotelInfoDBEntity.ContactEmail = txtContactEmail.Text.Trim();
        hotelInfoDBEntity.SalesStartDT = dpSalesStart.Value;
        hotelInfoDBEntity.SalesEndDT = dpSalesEnd.Value;

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        int iResult = HotelInfoBP.UpdateHotelSalesList(_hotelinfoEntity);

        _commonEntity.LogMessages = _hotelinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店签约信息-保存";
        commonDBEntity.Event_ID = hidHotelID.Value;
        string conTent = GetLocalResourceObject("EventInsertSalesMessage").ToString();

        conTent = string.Format(conTent, hotelInfoDBEntity.HotelID, hotelInfoDBEntity.Name_CN, hotelInfoDBEntity.Fax, hotelInfoDBEntity.Phone, hotelInfoDBEntity.ContactPer, hotelInfoDBEntity.ContactEmail, hotelInfoDBEntity.SalesID, hotelInfoDBEntity.SalesStartDT, hotelInfoDBEntity.SalesEndDT);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSalesSuccess").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateSalesSuccess").ToString();
            BindSalesManagerListGrid();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSalesError2").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateSalesError2").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError13").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError13").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        RestBalGridData();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSalesControlVal()", true);
    }

    protected void btnBalSearch_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        RestControlEnable(true, 2);
        //dvEvlGrid.Style.Add("display", "");
        //dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        hidSelectedID.Value = "2";

        ViewState["BalStartDT"] = dpBalStart.Value;
        ViewState["BalEndDT"] = dpBalEnd.Value;
        ViewState["BalRoomCD"] = ddpRoomList.SelectedValue;
        BindBalManagerListGrid();
    }

    protected void btnExportBal_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        RestControlEnable(true, 2);
        //dvEvlGrid.Style.Add("display", "");
        //dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        hidSelectedID.Value = "2";

        //ViewState["BalStartDT"] = dpBalStart.Value;
        //ViewState["BalEndDT"] = dpBalEnd.Value;
        //ViewState["BalPriceCD"] = ddpBalType.SelectedValue;

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.HRoomList = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BalRoomCD"].ToString())) ? null : ViewState["BalRoomCD"].ToString();
        hotelInfoDBEntity.InDateFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BalStartDT"].ToString())) ? null : ViewState["BalStartDT"].ToString();
        hotelInfoDBEntity.InDateTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BalEndDT"].ToString())) ? null : ViewState["BalEndDT"].ToString();

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.ExportBalanceRoomHistory(_hotelinfoEntity).QueryResult;
        CommonFunction.ExportExcelForLM(dsResult);
        BindBalManagerListGrid();
        //if (gridViewCSBalList.Rows.Count > 0)
        //{
            //DataSet dsResult = (DataSet)gridViewCSBalList.DataSource;
            //ExportGridToExcel(gridViewCSBalList);
        //}
    }

    private void ExportGridToExcel(GridView ctl)
    {
        //string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
        //HttpContext.Current.Response.Charset = "gb2312";
        //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        //HttpContext.Current.Response.AppendHeader("Content-Disposition", ("attachment;filename=" + fileName));
        ////HttpContext.Current.Response.ContentEncoding   =   System.Text.Encoding.GetEncoding("gb2312");     

        //ctl.Page.EnableViewState = false;
        //System.IO.StringWriter tw = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        //ctl.AllowPaging = false;
        //ctl.PageSize = 99999;
        //ctl.RenderControl(hw);
        //ctl.PageSize = 10;
        //ctl.AllowPaging = true;
        //HttpContext.Current.Response.Write(tw.ToString());
        //HttpContext.Current.Response.End();

        //int maxRow = ds.Tables[0].Rows.Count;
        //string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";//设置导出文件的名称

        ////DataView dv = new DataView(ds.Tables[0]);//将DataSet转换成DataView
        //string fileURL = string.Empty;
        //HttpContext curContext = System.Web.HttpContext.Current;
        //curContext.Response.ContentType = "application/vnd.ms-excel";
        //curContext.Response.ContentEncoding = System.Text.Encoding.Default;
        //curContext.Response.AppendHeader("Content-Disposition", ("attachment;filename=" + fileName));
        //curContext.Response.Charset = "";

        //curContext.Response.Write(BuildExportHTMLFORLM(ds.Tables[0]));
        //curContext.Response.Flush();
        //curContext.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }


    protected void btnBalAdd_Click(object sender, EventArgs e)
    {
        RestControlEnable(true, 2);
        //dvEvlGrid.Style.Add("display", "");
        //dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        hidSelectedID.Value = "2";

        MessageContent.InnerHtml = "";
        bool bFlag = true;
        string msgString = "";

        if (String.IsNullOrEmpty(dpInDTStart.Value) || String.IsNullOrEmpty(dpInDTEnd.Value))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError17").ToString() + "<br/>";
            bFlag = false;
        }

        if (!ChkBalVal(txtBalVal.Text.Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError19").ToString() + "<br/>";
            bFlag = false;
        }

        string strHRoomList = "";
        foreach (ListItem lt in chkHotelRoomList.Items)
        {
            if (lt.Selected)
            {
                strHRoomList = strHRoomList + lt.Value + ",";
            }
        }
        strHRoomList = strHRoomList.Trim(',');
        if (String.IsNullOrEmpty(strHRoomList))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError18").ToString() + "<br/>";
            bFlag = false;
        }

        if (!bFlag)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError16").ToString() + "<br/>" + msgString;
            RestBalGridData();
            return;
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;

        hotelInfoDBEntity.PriceCode = ddpPriceCode.SelectedValue.Trim();
        hotelInfoDBEntity.HRoomList = strHRoomList;
        hotelInfoDBEntity.InDateFrom = dpInDTStart.Value.Trim();
        hotelInfoDBEntity.InDateTo = dpInDTEnd.Value.Trim();
        hotelInfoDBEntity.BalType = ddpBalType.SelectedValue.Trim();
        hotelInfoDBEntity.BalValue = txtBalVal.Text.Trim();


        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        _hotelinfoEntity = HotelInfoBP.SetBalanceRoomList(_hotelinfoEntity);
        int iResult = _hotelinfoEntity.Result;
        _commonEntity.LogMessages = _hotelinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店结算信息-保存";
        commonDBEntity.Event_ID = hidHotelID.Value;
        string conTent = GetLocalResourceObject("EventInsertBalMessage").ToString();

        conTent = string.Format(conTent, hotelInfoDBEntity.HotelID, hotelInfoDBEntity.Name_CN, hotelInfoDBEntity.PriceCode, hotelInfoDBEntity.HRoomList, hotelInfoDBEntity.InDateFrom, hotelInfoDBEntity.InDateTo, hotelInfoDBEntity.BalType, hotelInfoDBEntity.BalValue);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateBalSuccess").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateBalSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = _hotelinfoEntity.ErrorMSG;// GetLocalResourceObject("UpdateBalError2").ToString();
            MessageContent.InnerHtml = _hotelinfoEntity.ErrorMSG;//GetLocalResourceObject("UpdateBalError2").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError16").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError16").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        BindBalManagerListGrid();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSalesControlVal()", true);
    }

    private bool ChkBalVal(string param)
    {
        bool bReturn = true;
        if (String.IsNullOrEmpty(param))
        {
            return false;
        }

        if ("42".Equals(ddpBalType.SelectedValue))
        {
            if (!ChkDouble(param) || decimal.Parse(param) < 0 || 100 < decimal.Parse(param))
            {
                return false;
            }
        }
        else
        {
            if (!ChkDouble(param) || decimal.Parse(param) < 0 || 9999 < decimal.Parse(param))
            {
                return false;
            }
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

    private void SetSelectHotelControl()
    {
        //string strHotel = wctHotel.AutoResult.ToString();
        //if (String.IsNullOrEmpty(strHotel))
        //{
        //    //lbHotelNM.Text = "";
        //    RestControlValue();
        //    //chkAutoTrust.Checked = true;
        //    //divModify.Style.Add("display", "none");
        //    divBtnList.Style.Add("display", "none");
        //    dvSales.Style.Add("display", "none");
        //    dvBalSearch.Style.Add("display", "none");
        //    dvBalAdd.Style.Add("display", "none");
        //    //dvEvlGrid.Style.Add("display", "none");
        //    //dvLink.Style.Add("display", "none");
        //    RestControlEnable(false, 0);
        //    MessageContent.InnerHtml = "";
        //    //ImageReview.InnerHtml = "";
        //    wctSales.CTLDISPLAY = "1";
        //    return;
        //}

        wctSales.CTLDISPLAY = "0";
        //wctHotel.AutoResult = "";
        //hidHotelID.Value = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
        //lbHotelNM.Text = hidHotelID.Value + " - " + strHotel.Substring(strHotel.IndexOf(']') + 1);

        BindHotelInfo();
        RestControlEnable(true, 0);
        //BingControlValEnable();
        //divModify.Style.Add("display", "");
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        //dvEvlGrid.Style.Add("display", "");
        //dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        //btnSelect.Enabled = false;
    }

    private void RestControlValue()
    {
        txtHotelNM.Text = "";
        txtHotelNMEN.Text = "";

        //if (ddpStatusList.Items.Count > 0)
        //{
        //    ddpStatusList.SelectedIndex = 0;
        //}

        if (ddpRoomList.Items.Count > 0)
        {
            ddpRoomList.SelectedIndex = 0;
        }

        if (ddpHotelGroup.Items.Count > 0)
        {
            ddpHotelGroup.SelectedIndex = 0;
        }

        if (ddpStarRating.Items.Count > 0)
        {
            ddpStarRating.SelectedIndex = 0;
        }

        //if (ddpDiamondRating.Items.Count > 0)
        //{
        //    ddpDiamondRating.SelectedIndex = 0;
        //}

        if (ddpCity.Items.Count > 0)
        {
            ddpCity.SelectedIndex = 0;
        } 

        //ddpProvincial.SelectedIndex=-1;
        
        dpOpenDate.Value = "";
        dpRepairDate.Value = "";
        txtAddress.Text = "";
        //txtWebSite.Text = "";
        txtPhone.Text = "";
        txtFax.Text = "";
        txtContactPer.Text = "";
        txtLongitude.Text = "";
        txtLatitude.Text = "";
        txtContactEmail.Text = "";
        txtSimpleDescZh.Text = "";
        txtDescZh.Text = "";
        //txtEvaluation.Text = "";
        dpSalesStart.Value = "";
        dpSalesEnd.Value = "";
        dpBalStart.Value = "";
        dpBalEnd.Value = "";

        if (ddpPriceCode.Items.Count > 0)
        {
            ddpPriceCode.SelectedIndex = 0;
        }

        //chkHotelRoomList

        dpInDTStart.Value = "";
        dpInDTEnd.Value = "";
        if (ddpBalType.Items.Count > 0)
        {
            ddpBalType.SelectedIndex = 0;
        }
        txtBalVal.Text = "";

        //chkAutoTrust.Checked = true;
        //divModify.Style.Add("display", "none");
        //UpdatePanel1.Update();                                    
    }

    private void RestControlEnable(bool bStatus, int iSelectType)
    {
        txtHotelNM.Enabled = bStatus;
        txtHotelNMEN.Enabled = bStatus;
        //ddpStatusList.Enabled = bStatus;
        ddpHotelGroup.Enabled = bStatus;
        ddpStarRating.Enabled = bStatus;
        //ddpDiamondRating.Enabled = bStatus;
        //ddpProvincial.Enabled = bStatus;
        ddpCity.Enabled = bStatus;
        dpOpenDate.Disabled = !bStatus;
        dpRepairDate.Disabled = !bStatus;
        txtAddress.Enabled = bStatus;
        //txtWebSite.Enabled = bStatus;
        txtPhone.Enabled = bStatus;
        txtFax.Enabled = bStatus;
        txtContactPer.Enabled = bStatus;
        txtLongitude.Enabled = bStatus;
        txtLatitude.Enabled = bStatus;
        txtContactEmail.Enabled = bStatus;
        txtSimpleDescZh.Enabled = bStatus;
        txtDescZh.Enabled = bStatus;
        //txtEvaluation.Enabled = bStatus;

        dpSalesStart.Disabled = !bStatus;
        dpSalesEnd.Disabled = !bStatus;
        dpBalStart.Disabled = !bStatus;
        dpBalEnd.Disabled = !bStatus;
        ddpRoomList.Enabled = bStatus;

        ddpPriceCode.Enabled = bStatus;
        //chkHotelRoomList
        dpInDTStart.Disabled = !bStatus;
        dpInDTEnd.Disabled = !bStatus;

        ddpBalType.Enabled = bStatus;
        txtBalVal.Enabled = bStatus;

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetChkControlEnable('" + (bStatus ? "" : "disabled") +"',"+ iSelectType.ToString() + ")", true);
    }

    private void BingControlValEnable()
    {
        bool bStatus = true;
        int iSelectType = 0;

        txtHotelNM.Enabled = bStatus;
        txtHotelNMEN.Enabled = bStatus;
        //ddpStatusList.Enabled = bStatus;
        ddpHotelGroup.Enabled = bStatus;
        ddpStarRating.Enabled = bStatus;
        //ddpDiamondRating.Enabled = bStatus;
        //ddpProvincial.Enabled = bStatus;
        ddpCity.Enabled = bStatus;
        dpOpenDate.Disabled = !bStatus;
        dpRepairDate.Disabled = !bStatus;
        txtAddress.Enabled = bStatus;
        //txtWebSite.Enabled = bStatus;
        txtPhone.Enabled = bStatus;
        txtFax.Enabled = bStatus;
        txtContactPer.Enabled = bStatus;
        txtLongitude.Enabled = bStatus;
        txtLatitude.Enabled = bStatus;
        txtContactEmail.Enabled = bStatus;
        txtSimpleDescZh.Enabled = bStatus;
        txtDescZh.Enabled = bStatus;
        //txtEvaluation.Enabled = bStatus;

        dpSalesStart.Disabled = !bStatus;
        dpSalesEnd.Disabled = !bStatus;
        dpBalStart.Disabled = !bStatus;
        dpBalEnd.Disabled = !bStatus;
        ddpRoomList.Enabled = bStatus;

        ddpPriceCode.Enabled = bStatus;
        //chkHotelRoomList
        dpInDTStart.Disabled = !bStatus;
        dpInDTEnd.Disabled = !bStatus;

        ddpBalType.Enabled = bStatus;
        txtBalVal.Enabled = bStatus;

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BingControlValEnable('" + (bStatus ? "" : "disabled") + "'," + iSelectType.ToString() + ")", true);
    }

    public void BindHotelInfo()
    {
        MessageContent.InnerHtml = "";
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.BindHotelList(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            RestControlValue();
            //chkAutoTrust.Checked = true;
            return;
        }

        txtHotelNM.Text = dsResult.Tables[0].Rows[0]["HOTELNM"].ToString();
        txtHotelNMEN.Text = dsResult.Tables[0].Rows[0]["HotelNMEN"].ToString();
        //lbFogStatus.Text = dsResult.Tables[0].Rows[0]["FOGSTATUSDIS"].ToString();
        //hidFogStatus.Value = dsResult.Tables[0].Rows[0]["FOGSTATUS"].ToString();

        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString()))
        {
            //ddpStatusList.SelectedValue = dsResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString();
        }

        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["GROUPID"].ToString()))
        {
            if (ddpHotelGroup.Items.FindByValue(dsResult.Tables[0].Rows[0]["GROUPID"].ToString().Trim()) != null)
            {
                ddpHotelGroup.SelectedValue = dsResult.Tables[0].Rows[0]["GROUPID"].ToString().Trim();
            }
        }

        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["STARRATING"].ToString()))
        {
            ddpStarRating.SelectedValue = dsResult.Tables[0].Rows[0]["STARRATING"].ToString();
        }
        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString()))
        {
            //ddpDiamondRating.SelectedValue = dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString();
        }
        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["CITYID"].ToString()))
        {
            ddpCity.SelectedValue = dsResult.Tables[0].Rows[0]["CITYID"].ToString();
            //ddpCity.SelectedIndex = ddpCity.Items.IndexOf(ddpCity.Items.FindByValue(dsResult.Tables[0].Rows[0]["CITYID"].ToString()));
        }
        //ddpProvincial.SelectedIndex=-1;
        
        dpOpenDate.Value = dsResult.Tables[0].Rows[0]["OPENDATE"].ToString();
        dpRepairDate.Value = dsResult.Tables[0].Rows[0]["REPAIRDATE"].ToString();
        txtAddress.Text = dsResult.Tables[0].Rows[0]["ADDRESS"].ToString();
        //txtWebSite.Text = dsResult.Tables[0].Rows[0]["WEBSITE"].ToString();
        txtPhone.Text = dsResult.Tables[0].Rows[0]["PHONE"].ToString();
        txtFax.Text = dsResult.Tables[0].Rows[0]["FAX"].ToString();
        txtContactPer.Text = dsResult.Tables[0].Rows[0]["CONTACTPER"].ToString();
        txtContactEmail.Text = dsResult.Tables[0].Rows[0]["CONTACTEMAIL"].ToString();
        txtSimpleDescZh.Text = dsResult.Tables[0].Rows[0]["SIMPLEDESCZH"].ToString();
        txtDescZh.Text = dsResult.Tables[0].Rows[0]["DESCZH"].ToString();
        //txtEvaluation.Text = dsResult.Tables[0].Rows[0]["EVALUATION"].ToString();
        //chkAutoTrust.Checked = "1".Equals(dsResult.Tables[0].Rows[0]["AUTOTRUST"].ToString()) ? true : false;
        txtLongitude.Text = dsResult.Tables[0].Rows[0]["LONGITUDE"].ToString();
        txtLatitude.Text = dsResult.Tables[0].Rows[0]["LATITUDE"].ToString();

        ArrayList ayHotelImage = HotelInfoBP.BindHotelImagesList(_hotelinfoEntity).HotelInfoDBEntity[0].HotelImage;
        if (ayHotelImage.Count > 0)
        {
            PreViewImage(ayHotelImage);
        }
        else
        {
            //ImageReview.InnerHtml = "";
        }

        //SetGridEvalList(txtEvaluation.Text);
        GetSalesManager(hidHotelID.Value);
        GetBalanceRoomList(hidHotelID.Value);
    }

    private void GetSalesManager(string strHotelID)
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetSalesManager(_hotelinfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            dpSalesStart.Value = dsResult.Tables[0].Rows[0]["StartDtime"].ToString().Replace('/','-');
            dpSalesEnd.Value = dsResult.Tables[0].Rows[0]["EndDtime"].ToString().Replace('/', '-');
            wctSales.AutoResult = dsResult.Tables[0].Rows[0]["DISPNM"].ToString();
            hidSalesID.Value = dsResult.Tables[0].Rows[0]["DISPNM"].ToString();
        }
        BindSalesManagerListGrid();
    }

    private void GetBalanceRoomList(string strHotelID)
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetBalanceRoomList(_hotelinfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            chkHotelRoomList.DataTextField = "ROOMNM";
            chkHotelRoomList.DataValueField = "ROOMCODE";
            chkHotelRoomList.DataSource = dsResult;
            chkHotelRoomList.DataBind();

            DataRow dr0 = dsResult.Tables[0].NewRow();
            dr0["ROOMCODE"] = "";
            dr0["ROOMNM"] = "不限制";
            dsResult.Tables[0].Rows.InsertAt(dr0, 0);
            ddpRoomList.DataTextField = "ROOMNM";
            ddpRoomList.DataValueField = "ROOMCODE";
            ddpRoomList.DataSource = dsResult.Tables[0].DefaultView;
            ddpRoomList.DataBind();
        }
    } 

    private void BindSalesManagerListGrid()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetSalesManagerSettingHistory(_hotelinfoEntity).QueryResult;

        gridViewCSSalesList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSSalesList.DataKeyNames = new string[] { "SALESNM" };//主键
        gridViewCSSalesList.DataBind();
    }

    protected void gridViewCSSalesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gridViewCSSalesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSSalesList.PageIndex = e.NewPageIndex;
        BindSalesManagerListGrid();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSalesControlVal()", true);
    }

    private void RestBalGridData()
    {
        if (gridViewCSBalList.Rows.Count > 0)
        {
            BindBalManagerListGrid();
        }
    }

    protected void gridViewCSBalList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                int iLMCount = (String.IsNullOrEmpty(hidLMCount.Value)) ? 0 : int.Parse(hidLMCount.Value);
                int iLM2Count = (String.IsNullOrEmpty(hidLM2Count.Value)) ? 0 : int.Parse(hidLM2Count.Value);
                int iLMSum = iLMCount + iLM2Count;
                int iHead = 1;
                string[] strColList = hidColsNM.Value.Split(',');

                //第一行表头
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].Attributes.Add("rowspan", "1"); //跨Row
                tcHeader[0].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[0].Text = "";


                if (iLMCount > 0 && iLM2Count > 0)
                {
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[iHead].HorizontalAlign = HorizontalAlign.Center;
                    tcHeader[iHead].Attributes.Add("colspan", hidLMCount.Value); //跨Column
                    tcHeader[iHead].Text = "LMBAR";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[iHead+1].HorizontalAlign = HorizontalAlign.Center;
                    tcHeader[iHead+1].Attributes.Add("colspan", hidLM2Count.Value); //跨Column
                    tcHeader[iHead+1].Text = "LMBAR2</th></tr><tr  class='GView_HeaderCSS'>";
                }
                else if (iLMCount == 0 && iLM2Count > 0)
                {
                    iHead = 0;
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[iHead + 1].HorizontalAlign = HorizontalAlign.Center;
                    tcHeader[iHead + 1].Attributes.Add("colspan", hidLM2Count.Value); //跨Column
                    tcHeader[iHead + 1].Text = "LMBAR2</th></tr><tr  class='GView_HeaderCSS'>";
                }
                else if (iLMCount > 0 && iLM2Count == 0)
                {
                    iHead = 0;
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[iHead + 1].HorizontalAlign = HorizontalAlign.Center;
                    tcHeader[iHead + 1].Attributes.Add("colspan", hidLMCount.Value); //跨Column
                    tcHeader[iHead + 1].Text = "LMBAR</th></tr><tr  class='GView_HeaderCSS'>";
                }

                //第二行表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[iHead+2].Width = 5;
                tcHeader[iHead+2].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[iHead+2].Text = "&nbsp;&nbsp;日期/房型&nbsp;&nbsp;";
                tcHeader[iHead+2].Wrap = false;

                for (int i = 0; i < iLMSum; i++)
                {
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[i + iHead+3].Width = 5;
                    tcHeader[i + iHead+3].HorizontalAlign = HorizontalAlign.Center;
                    tcHeader[i + iHead+3].Text = strColList[i];
                    tcHeader[i + iHead+3].Wrap = false;
                }
                //tcHeader.Add(new TableHeaderCell());
                //tcHeader[4].Width = 5;
                //tcHeader[4].HorizontalAlign = HorizontalAlign.Center;
                //tcHeader[4].Text = "DDKG";
                //tcHeader.Add(new TableHeaderCell());
                //tcHeader[5].Width = 5;
                //tcHeader[5].HorizontalAlign = HorizontalAlign.Center;
                //tcHeader[5].Text = "DTTW";
                //tcHeader.Add(new TableHeaderCell());
                //tcHeader[6].Width = 5;
                //tcHeader[6].HorizontalAlign = HorizontalAlign.Center;
                //tcHeader[6].Text = "DDKG";
                //tcHeader.Add(new TableHeaderCell());
                //tcHeader[7].Width = 5;
                //tcHeader[7].HorizontalAlign = HorizontalAlign.Center;
                //tcHeader[7].Text = "DTTW";
                break;
        }
    }

    protected void gridViewCSBalList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSBalList.PageIndex = e.NewPageIndex;
        BindBalManagerListGrid();
    }

    private void BindBalManagerListGrid()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;

        hotelInfoDBEntity.HRoomList = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BalRoomCD"].ToString())) ? null : ViewState["BalRoomCD"].ToString();
        hotelInfoDBEntity.InDateFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BalStartDT"].ToString())) ? null : ViewState["BalStartDT"].ToString();
        hotelInfoDBEntity.InDateTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BalEndDT"].ToString())) ? null : ViewState["BalEndDT"].ToString();

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        _hotelinfoEntity = HotelInfoBP.GetBalanceRoomHistory(_hotelinfoEntity);
        DataSet dsResult = _hotelinfoEntity.QueryResult;
        hidLMCount.Value = _hotelinfoEntity.LMCount.ToString();
        hidLM2Count.Value = _hotelinfoEntity.LM2Count.ToString();
        hidColsNM.Value = _hotelinfoEntity.Cols;
        gridViewCSBalList.DataSource = dsResult.Tables[0].DefaultView;
        //gridViewCSBalList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSBalList.DataBind();

        for (int i = 0; i < gridViewCSBalList.Rows.Count; i++)
        {
            for (int j = 0; j < dsResult.Tables[0].Columns.Count; j++)
            {
                gridViewCSBalList.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }

    protected void gridViewCSBalList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //switch (e.Row.RowType)
        //{
        //    case DataControlRowType.Header:
        //        int iLMCount = (String.IsNullOrEmpty(hidLMCount.Value)) ? 0 : int.Parse(hidLMCount.Value);
        //        int iLM2Count = (String.IsNullOrEmpty(hidLM2Count.Value)) ? 0 : int.Parse(hidLM2Count.Value);
        //        int iLMSum = iLMCount + iLM2Count;

        //        string[] strColList = hidColsNM.Value.Split(',');

        //        //第一行表头
        //        TableCellCollection tcHeader = e.Row.Cells;
        //        tcHeader.Clear();
        //        tcHeader.Add(new TableHeaderCell());
        //        tcHeader[0].Attributes.Add("rowspan", "1"); //跨Row
        //        tcHeader[0].HorizontalAlign = HorizontalAlign.Center;
        //        tcHeader[0].Text = "";

        //        tcHeader.Add(new TableHeaderCell());
        //        tcHeader[1].HorizontalAlign = HorizontalAlign.Center;
        //        tcHeader[1].Attributes.Add("colspan", hidLMCount.Value); //跨Column
        //        tcHeader[1].Text = "LMBAR";
        //        tcHeader.Add(new TableHeaderCell());
        //        tcHeader[1].HorizontalAlign = HorizontalAlign.Center;
        //        tcHeader[2].Attributes.Add("colspan", hidLM2Count.Value); //跨Column
        //        tcHeader[2].Text = "LMBAR2</th></tr><tr  class='GView_HeaderCSS'>";

        //        //第二行表头
        //        tcHeader.Add(new TableHeaderCell());
        //        tcHeader[3].Width = 5;
        //        tcHeader[3].HorizontalAlign = HorizontalAlign.Center;
        //        tcHeader[3].Text = "&nbsp;&nbsp;日期/房型&nbsp;&nbsp;";
        //        tcHeader[3].Wrap = false;

        //        for (int i = 0; i < iLMSum; i++)
        //        {
        //            tcHeader.Add(new TableHeaderCell());
        //            tcHeader[i + 4].Width = 5;
        //            tcHeader[i + 4].HorizontalAlign = HorizontalAlign.Center;
        //            tcHeader[i + 4].Text = strColList[i];
        //            tcHeader[i + 4].Wrap = false;
        //        }
        //        //tcHeader.Add(new TableHeaderCell());
        //        //tcHeader[4].Width = 5;
        //        //tcHeader[4].HorizontalAlign = HorizontalAlign.Center;
        //        //tcHeader[4].Text = "DDKG";
        //        //tcHeader.Add(new TableHeaderCell());
        //        //tcHeader[5].Width = 5;
        //        //tcHeader[5].HorizontalAlign = HorizontalAlign.Center;
        //        //tcHeader[5].Text = "DTTW";
        //        //tcHeader.Add(new TableHeaderCell());
        //        //tcHeader[6].Width = 5;
        //        //tcHeader[6].HorizontalAlign = HorizontalAlign.Center;
        //        //tcHeader[6].Text = "DDKG";
        //        //tcHeader.Add(new TableHeaderCell());
        //        //tcHeader[7].Width = 5;
        //        //tcHeader[7].HorizontalAlign = HorizontalAlign.Center;
        //        //tcHeader[7].Text = "DTTW";
        //        break;
        //}
    }

    protected void gridViewEvaluationList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       DataTable dtResult = new DataTable();
       dtResult.Columns.Add("Content");

       for (int i = 0; i < this.gridViewEvaluationList.Rows.Count; i++)
       {
           if (i == e.RowIndex)
           {
               continue;
           }
           TextBox txtBox = (TextBox)gridViewEvaluationList.Rows[i].FindControl("txtEvalist");
           DataRow drRow = dtResult.NewRow();
           drRow[0] = txtBox.Text;
           dtResult.Rows.Add(drRow);
       }

       gridViewEvaluationList.DataSource = dtResult.DefaultView;
       gridViewEvaluationList.DataKeyNames = new string[] { "Content" };//主键
       gridViewEvaluationList.DataBind();

       if (dtResult.Rows.Count > 0)
       {
           gridViewEvaluationList.HeaderRow.Visible = false;
       }

       //divModify.Style.Add("display", "none");
       divBtnList.Style.Add("display", "");
       dvSales.Style.Add("display", "");
       dvBalSearch.Style.Add("display", "");
       dvBalAdd.Style.Add("display", "");
       //dvEvlGrid.Style.Add("display", "");
       //dvLink.Style.Add("display", "");
       RestControlEnable(true, 2);
       RestBalGridData();
    }

    //protected void btnModify_Click(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < this.gridViewEvaluationList.Rows.Count; i++)
    //    {
    //        TextBox txtBox = (TextBox)gridViewEvaluationList.Rows[i].FindControl("txtEvalist");
    //        txtBox.Enabled = true;
    //        gridViewEvaluationList.Rows[i].Cells[1].Visible = true;
    //        //LinkButton lkBtn = (LinkButton)gridViewEvaluationList.Rows[i].FindControl("lkDele");
    //        //lkBtn.Enabled = true;
    //    }

    //    //divModify.Style.Add("display", "none");
    //    divBtnList.Style.Add("display", "");
    //    dvEvlGrid.Style.Add("display", "");
    //    dvLink.Style.Add("display", "");
    //    RestControlEnable(true, 2);
    //}

    private void SetGridEvalList(string Evaluation)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("Content");

        string[] EvalList = Evaluation.Split(',');

        foreach (string temp in EvalList)
        {
            if (String.IsNullOrEmpty(temp.Trim()))
            {
                continue;
            }
            DataRow drRow = dtResult.NewRow();
            drRow[0] = temp;
            dtResult.Rows.Add(drRow);
        }

        gridViewEvaluationList.DataSource = dtResult.DefaultView;
        gridViewEvaluationList.DataKeyNames = new string[] { "Content" };//主键
        gridViewEvaluationList.DataBind();

        if (dtResult.Rows.Count > 0)
        {
            gridViewEvaluationList.HeaderRow.Visible = false;
            for (int i = 0; i < this.gridViewEvaluationList.Rows.Count; i++)
            {
                TextBox txtBox = (TextBox)gridViewEvaluationList.Rows[i].FindControl("txtEvalist");
                txtBox.Enabled = false;

                //LinkButton lkBtn = (LinkButton)gridViewEvaluationList.Rows[i].FindControl("lkDele");
                //lkBtn.Enabled = false;
                gridViewEvaluationList.Rows[i].Cells[1].Visible = false;
            }
        }

    }

    private void PreViewImage(ArrayList ayHotelImage)
    {
        string preViewImage = string.Empty;
        string strWidth = string.Empty;
        string strHeight = string.Empty;
        string strTemp = string.Empty;
        for (int i = 0; i < ayHotelImage.Count; i++)
        {
            try
            {
                strTemp = ayHotelImage[i].ToString().Substring(ayHotelImage[i].ToString().LastIndexOf("/") + 1);
                strWidth = strTemp.Substring(strTemp.IndexOf("_") + 1, strTemp.LastIndexOf("_") - strTemp.IndexOf("_") - 1);
                strHeight = strTemp.Substring(strTemp.LastIndexOf("_") + 1, strTemp.IndexOf(".") - strTemp.LastIndexOf("_") - 1);

                strWidth = (chkWidthHeight(strWidth)) ? (double.Parse(strWidth) * 0.5).ToString() : strWidth;
                strHeight = (chkWidthHeight(strHeight)) ? (double.Parse(strHeight) * 0.5).ToString() : strHeight;
            }
            catch
            {
                strWidth = "200";
                strHeight = "150";
            }
            preViewImage += "&nbsp;&nbsp;<img src='" + ayHotelImage[i] + "' style='width:" + strWidth + "px;height:" + strHeight + "px' />";
        }

        //ImageReview.InnerHtml = preViewImage;
    }

    private bool chkWidthHeight(string param)
    {
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

    protected void btnReadFog_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        DataSet dsResult = HotelInfoBP.ReadFogHotelInfo(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            return;
        }

        txtHotelNM.Text = dsResult.Tables[0].Rows[0]["HOTELNM"].ToString();
        txtHotelNMEN.Text = dsResult.Tables[0].Rows[0]["HotelNMEN"].ToString();
        //ddpStatusList.SelectedValue = dsResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString();
        //ddpHotelGroup.SelectedValue = dsResult.Tables[0].Rows[0]["GROUPID"].ToString();
        //ddpStarRating.SelectedValue = dsResult.Tables[0].Rows[0]["STARRATING"].ToString();
        //ddpDiamondRating.SelectedValue = dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString();
        ////ddpProvincial.SelectedIndex=-1;
        //ddpCity.SelectedValue = dsResult.Tables[0].Rows[0]["CITYID"].ToString();
        //lbFogStatus.Text = dsResult.Tables[0].Rows[0]["FOGSTATUSDIS"].ToString();
        //hidFogStatus.Value = dsResult.Tables[0].Rows[0]["FOGSTATUS"].ToString();

        ddpHotelGroup.SelectedValue = dsResult.Tables[0].Rows[0]["GROUPID"].ToString().Trim();

        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["STARRATING"].ToString()))
        {
            ddpStarRating.SelectedValue = dsResult.Tables[0].Rows[0]["STARRATING"].ToString();
        }
        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString()))
        {
            //ddpDiamondRating.SelectedValue = dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString();
        }
        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["CITYID"].ToString()))
        {
            ddpCity.SelectedValue = dsResult.Tables[0].Rows[0]["CITYID"].ToString();
        }

        dpOpenDate.Value = dsResult.Tables[0].Rows[0]["OPENDATE"].ToString();
        dpRepairDate.Value = dsResult.Tables[0].Rows[0]["REPAIRDATE"].ToString();
        txtAddress.Text = dsResult.Tables[0].Rows[0]["ADDRESS"].ToString();
        //txtWebSite.Text = dsResult.Tables[0].Rows[0]["WEBSITE"].ToString();
        txtPhone.Text = dsResult.Tables[0].Rows[0]["PHONE"].ToString();
        txtFax.Text = dsResult.Tables[0].Rows[0]["FAX"].ToString();
        txtContactPer.Text = dsResult.Tables[0].Rows[0]["CONTACTPER"].ToString();
        txtContactEmail.Text = dsResult.Tables[0].Rows[0]["CONTACTEMAIL"].ToString();

        txtLongitude.Text = dsResult.Tables[0].Rows[0]["LONGITUDE"].ToString();
        txtLatitude.Text = dsResult.Tables[0].Rows[0]["LATITUDE"].ToString();

        txtSimpleDescZh.Text = dsResult.Tables[0].Rows[0]["SIMPLEDESCZH"].ToString();
        txtDescZh.Text = dsResult.Tables[0].Rows[0]["DESCZH"].ToString();
        //txtEvaluation.Text = dsResult.Tables[0].Rows[0]["EVALUATION"].ToString();
        //divModify.Style.Add("display", "none");
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        RestControlEnable(true, 2);
        SetEvalGridBtnStyle(true);
        //dvLink.Style.Add("display", "");
        //dvEvlGrid.Style.Add("display", "");
        RestBalGridData();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSelectEnable('disabled')", true);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnAddData();
        RestControlEnable(true, 2);
        //dvEvlGrid.Style.Add("display", "");
        //dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        hidSelectedID.Value = "0";
        RestBalGridData();
        //if (btnAddData())
        //{
        //    RestControlEnable(false, 1);
        //    SetEvalGridBtnStyle(false);
        //    dvLink.Style.Add("display", "none");
        //    dvEvlGrid.Style.Add("display", "");
        //    //divModify.Style.Add("display", "");
        //    divBtnList.Style.Add("display", "none");
        //    //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSelectEnable('')", true);
        //}
        //else
        //{
        //    RestControlEnable(true, 2);
        //    dvEvlGrid.Style.Add("display", "");
        //    dvLink.Style.Add("display", "");
        //    SetEvalGridBtnStyle(true);
        //    //divModify.Style.Add("display", "none");
        //    divBtnList.Style.Add("display", "");
        //    //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSelectEnable('disabled')", true);
        //}
    }

    private void SetEvalGridBtnStyle(bool bStatus)
    {
        for (int i = 0; i < this.gridViewEvaluationList.Rows.Count; i++)
        {
            TextBox txtBox = (TextBox)gridViewEvaluationList.Rows[i].FindControl("txtEvalist");
            txtBox.Enabled = bStatus;
            gridViewEvaluationList.Rows[i].Cells[1].Visible = bStatus;
            //LinkButton lkBtn = (LinkButton)gridViewEvaluationList.Rows[i].FindControl("lkDele");
            //lkBtn.Enabled = bStatus;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        BindHotelInfo();
        RestControlEnable(true, 2);
        //divModify.Style.Add("display", "none");
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        //dvLink.Style.Add("display", "");
        //dvEvlGrid.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSelectEnable('disabled')", true);
    }

    public void BindDdpList()
    {
        MessageContent.InnerHtml = "";

        DataSet dsOnlineStatus = CommonBP.GetConfigList(GetLocalResourceObject("Online").ToString());
        if (dsOnlineStatus.Tables.Count > 0)
        {
            dsOnlineStatus.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
            dsOnlineStatus.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";

            //ddpStatusList.DataTextField = "ONLINEDIS";
            //ddpStatusList.DataValueField = "ONLINESTATUS";
            //ddpStatusList.DataSource = dsOnlineStatus;
            //ddpStatusList.DataBind();
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

        DataSet dsHotelGroup = HotelInfoBP.CommonHotelGroupSelect(_hotelinfoEntity).QueryResult;
        ddpHotelGroup.DataTextField = "NAMEZH";
        ddpHotelGroup.DataValueField = "GROUPCODE";
        ddpHotelGroup.DataSource = dsHotelGroup;
        ddpHotelGroup.DataBind();

        //DataSet dsProvincial = HotelInfoBP.CommonProvincialSelect(_hotelinfoEntity).QueryResult;
        //ddpProvincial.DataTextField = "NAMEZH";
        //ddpProvincial.DataValueField = "GROUPCODE";
        //ddpProvincial.DataSource = dsProvincial;
        //ddpProvincial.DataBind();

        DataSet dsCity = HotelInfoBP.CommonCitySelect(_hotelinfoEntity).QueryResult;
        ddpCity.DataTextField = "NAMEZH";
        ddpCity.DataValueField = "CITYID";
        ddpCity.DataSource = dsCity;
        ddpCity.DataBind();

        DataSet dsStarRating = CommonBP.GetConfigList(GetLocalResourceObject("StarRating").ToString());
        if (dsStarRating.Tables.Count > 0)
        {
            dsStarRating.Tables[0].Columns["Key"].ColumnName = "STARKEY";
            dsStarRating.Tables[0].Columns["Value"].ColumnName = "STARDIS";

            ddpStarRating.DataTextField = "STARDIS";
            ddpStarRating.DataValueField = "STARKEY";
            ddpStarRating.DataSource = dsStarRating;
            ddpStarRating.DataBind();
        }

        DataSet dsDiamondRating = CommonBP.GetConfigList(GetLocalResourceObject("DiamondRating").ToString());
        if (dsDiamondRating.Tables.Count > 0)
        {
            dsDiamondRating.Tables[0].Columns["Key"].ColumnName = "DIAMONDKEY";
            dsDiamondRating.Tables[0].Columns["Value"].ColumnName = "DIAMONDDIS";

            //ddpDiamondRating.DataTextField = "DIAMONDDIS";
            //ddpDiamondRating.DataValueField = "DIAMONDKEY"; 
            //ddpDiamondRating.DataSource = dsDiamondRating;
            //ddpDiamondRating.DataBind();
        }

        DataTable dtTicketTD = GetPriceCodeData();
        ddpPriceCode.DataSource = dtTicketTD;
        ddpPriceCode.DataTextField = "PRICECD_TEXT";
        ddpPriceCode.DataValueField = "PRICECD_STATUS";
        ddpPriceCode.DataBind();
        ddpPriceCode.SelectedIndex = 0;

        DataTable dtBalType = GetBalTypeData();
        ddpBalType.DataSource = dtBalType;
        ddpBalType.DataTextField = "BALTYPE_TEXT";
        ddpBalType.DataValueField = "BALTYPE_STATUS";
        ddpBalType.DataBind();
        ddpBalType.SelectedIndex = 0;
    }

    private DataTable GetPriceCodeData()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("PRICECD_STATUS");
        DataColumn BookStatusText = new DataColumn("PRICECD_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        //DataRow dr0 = dt.NewRow();
        //dr0["PRICECD_STATUS"] = "";
        //dr0["PRICECD_TEXT"] = "不限制";
        //dt.Rows.Add(dr0);

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            
            switch (i.ToString())
            {
                case "0":
                    dr["PRICECD_STATUS"] = "LMBAR";
                    dr["PRICECD_TEXT"] = "LMBAR";
                    break;
                //case "1":
                //    dr["TICKETTD_TEXT"] = "已过使用期";
                //    break;
                case "1":
                    dr["PRICECD_STATUS"] = "LMBAR2";
                    dr["PRICECD_TEXT"] = "LMBAR2";
                    break;
                //case "3":
                //    dr["TICKETTD_TEXT"] = "当前可使用";
                //    break;
                default:
                    dr["PRICECD_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetBalTypeData()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("BALTYPE_STATUS");
        DataColumn BookStatusText = new DataColumn("BALTYPE_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            
            switch (i.ToString())
            {
                case "0":
                    dr["BALTYPE_STATUS"] = "42";
                    dr["BALTYPE_TEXT"] = "按照订单价格百分比";
                    break;
                //case "1":
                //    dr["TICKETTD_TEXT"] = "已过使用期";
                //    break;
                case "1":
                    dr["BALTYPE_STATUS"] = "25";
                    dr["BALTYPE_TEXT"] = "按照固定金额";
                    break;
                //case "3":
                //    dr["TICKETTD_TEXT"] = "当前可使用";
                //    break;
                default:
                    dr["BALTYPE_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    //private static bool IsMobileNumber(string str_telephone)
    //{
    //    System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");

    //    System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^[1]+[3,5]+\d{9}");

    //    return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"(1[3,4,5,8][0-9])\d{11}$");
    //}

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

    public bool btnAddData()
    {
        MessageContent.InnerHtml = "";
        bool bFlag = true;
        string msgString = "";
        if (String.IsNullOrEmpty(txtHotelNM.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError3").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtHotelNM.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("UpdateError2").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtHotelNMEN.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("UpdateError12").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtAddress.Text.ToString().Trim()) > 150)
        {
            msgString = msgString + GetLocalResourceObject("UpdateError4").ToString() + "<br/>";
            bFlag = false;
        }

        //if (StringUtility.Text_Length(txtWebSite.Text.ToString().Trim()) > 200)
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError5").ToString() + "<br/>";
        //    bFlag = false;
        //}

        //if (String.IsNullOrEmpty(txtPhone.Text.ToString().Trim()))
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError61").ToString() + "<br/>";
        //    bFlag = false;
        //}

        //if (String.IsNullOrEmpty(txtFax.Text.ToString().Trim()))
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError71").ToString() + "<br/>";
        //    bFlag = false;
        //}

        //if (!String.IsNullOrEmpty(txtPhone.Text.ToString().Trim()) && (StringUtility.Text_Length(txtPhone.Text.ToString().Trim()) > 40 ))
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError6").ToString() + "<br/>";
        //    bFlag = false;
        //}

        //if (!String.IsNullOrEmpty(txtFax.Text.ToString().Trim()) && (StringUtility.Text_Length(txtFax.Text.ToString().Trim()) > 20 ))
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError7").ToString() + "<br/>";
        //    bFlag = false;
        //}

        //if (StringUtility.Text_Length(txtContactPer.Text.ToString().Trim()) > 100)
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError10").ToString() + "<br/>";
        //    bFlag = false;
        //}

        //if (StringUtility.Text_Length(txtContactEmail.Text.ToString().Trim()) > 100)
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError11").ToString() + "<br/>";
        //    bFlag = false;
        //}

        if ((String.IsNullOrEmpty(txtLatitude.Text.ToString().Trim())) || (String.IsNullOrEmpty(txtLongitude.Text.ToString().Trim())))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError21").ToString() + "<br/>";
            bFlag = false;
        }

        if (!RegexValidateData(txtLatitude.Text.ToString().Trim()) || !RegexValidateData(txtLongitude.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError31").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtSimpleDescZh.Text.ToString().Trim()) && (StringUtility.Text_Length(txtSimpleDescZh.Text.ToString().Trim()) > 1000))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError8").ToString() + "<br/>";
            bFlag = false;
        }

        string Evaluation = string.Empty;

        for (int i = 0; i < this.gridViewEvaluationList.Rows.Count; i++)
        {
            TextBox txtBox = (TextBox)gridViewEvaluationList.Rows[i].FindControl("txtEvalist");
            if (!ChkEvaContent(txtBox.Text.Trim()))//if (txtBox.Text.Trim().Contains(",") || txtBox.Text.Trim().Contains("，"))
            {
                msgString = msgString + GetLocalResourceObject("UpdateError32").ToString() + "<br/>";
                bFlag = false;
                break;
            }
            Evaluation = (!string.IsNullOrEmpty(txtBox.Text.Trim())) ? Evaluation + txtBox.Text.Trim() + "," : Evaluation;
        }

        Evaluation = (Evaluation.Length > 0) ? Evaluation.Substring(0, Evaluation.Length - 1) : Evaluation;

        if (!String.IsNullOrEmpty(Evaluation) && (StringUtility.Text_Length(Evaluation) > 1000))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError9").ToString() + "<br/>";
            bFlag = false;
        }

        if (!bFlag)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString() + "<br/>" + msgString;
            return false;
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.ID = hidHotelNo.Value;
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.Name_CN = txtHotelNM.Text.Trim();
        hotelInfoDBEntity.Name_EN = txtHotelNMEN.Text.Trim();
        //hotelInfoDBEntity.Status = ddpStatusList.SelectedValue;
        hotelInfoDBEntity.HotelGroup = ddpHotelGroup.SelectedValue;
        hotelInfoDBEntity.StarRating = ddpStarRating.SelectedValue;
        //hotelInfoDBEntity.DiamondRating = ddpDiamondRating.SelectedValue;
        hotelInfoDBEntity.City = ddpCity.SelectedValue;
        hotelInfoDBEntity.OpenDate = dpOpenDate.Value;
        hotelInfoDBEntity.RepairDate = dpRepairDate.Value;
        hotelInfoDBEntity.AddRess = txtAddress.Text.Trim();
        //hotelInfoDBEntity.WebSite = txtWebSite.Text.Trim();
        //hotelInfoDBEntity.Phone = txtPhone.Text.Trim();
        //hotelInfoDBEntity.Fax = txtFax.Text.Trim();
        //hotelInfoDBEntity.ContactPer = txtContactPer.Text.Trim();
        //hotelInfoDBEntity.ContactEmail = txtContactEmail.Text.Trim();

        hotelInfoDBEntity.Longitude = txtLongitude.Text.Trim();
        hotelInfoDBEntity.Latitude = txtLatitude.Text.Trim();

        hotelInfoDBEntity.SimpleDescZh = txtSimpleDescZh.Text.Trim();
        hotelInfoDBEntity.DescZh = txtDescZh.Text.Trim();
        hotelInfoDBEntity.Evaluation = Evaluation;
        //hotelInfoDBEntity.AutoTrust = (chkAutoTrust.Checked) ? "1" : "0";
        //hotelInfoDBEntity.FogStatus = hidFogStatus.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        int iResult = HotelInfoBP.HotelSave(_hotelinfoEntity);

        _commonEntity.LogMessages = _hotelinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店基础信息-保存";
        commonDBEntity.Event_ID = hidHotelID.Value;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();

        conTent = string.Format(conTent, hotelInfoDBEntity.HotelID, hotelInfoDBEntity.Name_CN, hotelInfoDBEntity.Status, hotelInfoDBEntity.HotelGroup, hotelInfoDBEntity.StarRating, hotelInfoDBEntity.DiamondRating, hotelInfoDBEntity.City, hotelInfoDBEntity.OpenDate.ToString(), hotelInfoDBEntity.RepairDate.ToString(), hotelInfoDBEntity.AddRess, hotelInfoDBEntity.WebSite, hotelInfoDBEntity.SimpleDescZh, hotelInfoDBEntity.DescZh, hotelInfoDBEntity.Evaluation);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError1").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        if (iResult == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ChkEvaContent(string parm)
    {
        bool bResult = true;
        Regex seperatorReg = new Regex(@"[~!@#\$%\^&\*\(\)\+=\|\\\}\]\{\[:;<,>\?\/""]+", RegexOptions.IgnorePatternWhitespace);
        Regex seperatorRegUp = new Regex(@"[~！@#\￥%\……&\*\（\）\+=\|\、\}\】\{\【：；《，》\？\、“”]+", RegexOptions.IgnorePatternWhitespace);
        if (seperatorReg.IsMatch(parm) || seperatorRegUp.IsMatch(parm))
        {
            bResult = false;
        }

        return bResult;
    }

    protected void lkBtnAdd_Click(object sender, EventArgs e)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("Content");

        for (int i = 0; i < this.gridViewEvaluationList.Rows.Count; i++)
        {
            TextBox txtBox = (TextBox)gridViewEvaluationList.Rows[i].FindControl("txtEvalist");
            DataRow drRow = dtResult.NewRow();
            drRow[0] = txtBox.Text;
            dtResult.Rows.Add(drRow);
        }

        DataRow drEmptyRow = dtResult.NewRow();
        drEmptyRow[0] = "";
        dtResult.Rows.Add(drEmptyRow);

        gridViewEvaluationList.DataSource = dtResult.DefaultView;
        gridViewEvaluationList.DataKeyNames = new string[] { "Content" };//主键
        gridViewEvaluationList.DataBind();

        if (dtResult.Rows.Count > 0)
        {
            gridViewEvaluationList.HeaderRow.Visible = false;
        }

        hidSelectedID.Value = "0";
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        //dvEvlGrid.Style.Add("display", "");
        //dvLink.Style.Add("display", "");
        RestControlEnable(true, 2);
        RestBalGridData();
    }

    protected void btnAddRoom_Click(object sender, EventArgs e)
    {

    }
}