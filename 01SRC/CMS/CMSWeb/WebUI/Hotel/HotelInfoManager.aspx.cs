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
using System.Web.Services;
using System.Text.RegularExpressions;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class HotelInfoManager : BasePage
{
    HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        //wctHotel.CityName = hidCityName.Value;

        if (!IsPostBack)
        {
            //divModify.Style.Add("display", "none");
            divBtnList.Style.Add("display", "none");
            dvSales.Style.Add("display", "none");
            dvBalSearch.Style.Add("display", "none");
            dvBalAdd.Style.Add("display", "none");
            dvbtnRoom.Style.Add("display", "none");
            dvbtnPrRoom.Style.Add("display", "none");
            dvHotelEX.Style.Add("display", "none");
            wctSales.CTLDISPLAY = "1";
            wctUCity.CTLDISPLAY = "1";
            hidModel.Value = "1";
            wctHotelGroupCode.CTLDISPLAY = "1";

            BindDtStatusListRemark();//绑定  酒店下线的原因

            checkedRadio();
            BindDdpList();
            BindCHDdpList();
            SetBtnSearchClick();

            //BindHotelTagIngo();
        }
        ddpStatusList.Attributes.Add("onchange", "selectChange(" + this.ddpStatusList.ClientID + ")");

        ddpOnline.Attributes.Add("onchange", "selectChangeByNewHotel(" + this.ddpOnline.ClientID + ")");

    }

    #region 编辑酒店信息
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
                hidSaveHotelID.Value = dsResult.Tables[0].Rows[0]["REVALUE_ALL"].ToString();
                wctHotel.AutoResult = dsResult.Tables[0].Rows[0]["REVALUE_ALL"].ToString();
                SetSelectHotelControl();
            }
        }
        else
        {
            RestControlEnable(false, 0);
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(hidHotelID.Value))
        {
            string strHotel = hidSaveHotelID.Value;
            MessageContent.InnerHtml = "";
            if (!strHotel.Contains("[") || !strHotel.Contains("]") || String.IsNullOrEmpty(strHotel))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
                return;
            }

            strHotel = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
            if (String.IsNullOrEmpty(strHotel))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
                return;
            }

            _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
            HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
            hotelInfoDBEntity.HotelID = strHotel;
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            DataSet dsResult = HotelInfoBP.ChkLMPropHotelList(_hotelinfoEntity).QueryResult;

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                string reqUrl = "";
                if (!String.IsNullOrEmpty(Request.QueryString["menu"]))
                {
                    reqUrl = "?menu=" + Request.QueryString["menu"].ToString() + "&hid=" + strHotel;
                }
                else
                {
                    reqUrl = "?hid=" + strHotel;
                }
                Server.Transfer("~/WebUI/Hotel/HotelInfoManager.aspx" + reqUrl);
            }
            else
            {
                ReSetControlVal();
                MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
            }
            BindHotelEXInfo();
        }
        else
        {
            SetSelectHotelControl();
        }
    }

    private void ReSetControlVal()
    {
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        //hidSelectedID.Value = "0";
        if ("0".Equals(hidModel.Value))
        {
            dvCRHotel.Style.Add("display", "");
            dvUDHotel.Style.Add("display", "none");
        }
        else
        {
            dvCRHotel.Style.Add("display", "none");
            dvUDHotel.Style.Add("display", "");
        }
        RestBalGridData();
    }

    protected void btnSaveSales_Click(object sender, EventArgs e)
    {
        hidSelectedID.Value = "3";
        MessageContent.InnerHtml = "";
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");

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

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("ROOMCD");
        dtResult.Columns.Add("DISCOUNTCD");
        dtResult.Columns.Add("DISCOUNTXT");
        DropDownList ddl;
        TextBox txtbx;

        for (int i = 0; i <= gridViewRather.Rows.Count - 1; i++)
        {
            DataRow dr = dtResult.NewRow();
            ddl = (DropDownList)gridViewRather.Rows[i].FindControl("ddpDiscount");
            txtbx = (TextBox)gridViewRather.Rows[i].FindControl("txtDiscount");

            if (!chkRoomRatherVal(ddl.SelectedValue, txtbx.Text))
            {
                msgString = msgString + GetLocalResourceObject("UpdateRoomError6").ToString() + "<br/>";
                bFlag = false;
                break;
            }

            dr["ROOMCD"] = gridViewRather.DataKeys[i][0].ToString();
            dr["DISCOUNTCD"] = ddl.SelectedValue;
            dr["DISCOUNTXT"] = ("0".Equals(ddl.SelectedValue)) ? "" : txtbx.Text;
            dtResult.Rows.Add(dr);
        }

        if (!bFlag)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError13").ToString() + "<br/>" + msgString;
            RestBalGridData();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSalesControlVal()", true);
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
        hotelInfoDBEntity.RoomRather = dtResult;// GetRoomEditRatherList();

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

    private bool chkRoomRatherVal(string rtype, string param)
    {
        if ("0".Equals(rtype))
        {
            return true;
        }

        if ("1".Equals(rtype))
        {
            if (String.IsNullOrEmpty(param) || !ChkDouble(param) || decimal.Parse(param) < 0 || 1 < decimal.Parse(param))
            {
                return false;
            }
        }
        else
        {
            if (String.IsNullOrEmpty(param) || !ChkDouble(param) || decimal.Parse(param) < 0 || 9999 < decimal.Parse(param))
            {
                return false;
            }
        }

        return true;
    }

    protected void btnBalSearch_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");

        hidSelectedID.Value = "5";

        ViewState["BalStartDT"] = dpBalStart.Value;
        ViewState["BalEndDT"] = dpBalEnd.Value;
        ViewState["BalRoomCD"] = ddpRoomList.SelectedValue;
        BindBalManagerListGrid();
    }

    protected void btnExportBal_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        hidSelectedID.Value = "5";

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
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        hidSelectedID.Value = "5";

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
        hotelInfoDBEntity.IsPushFog = (chkIsPushFog.Checked) ? "1" : "0";

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
        string strHotel = wctHotel.AutoResult;
        if (String.IsNullOrEmpty(strHotel))
        {
            lbHotelNM.Text = "";
            RestControlValue();
            //chkAutoTrust.Checked = true;
            //divModify.Style.Add("display", "none");
            divBtnList.Style.Add("display", "none");
            dvSales.Style.Add("display", "none");
            dvBalSearch.Style.Add("display", "none");
            dvBalAdd.Style.Add("display", "none");
            dvHotelEX.Style.Add("display", "none");
            dvEvlGrid.Style.Add("display", "none");
            dvLink.Style.Add("display", "none");
            dvbtnRoom.Style.Add("display", "none");
            dvbtnPrRoom.Style.Add("display", "none");
            RestControlEnable(false, 0);
            MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString(); ;
            //ImageReview.InnerHtml = "";
            wctSales.CTLDISPLAY = "1";
            wctUCity.CTLDISPLAY = "1";
            wctHotelGroupCode.CTLDISPLAY = "1";
            if ("1".Equals(hidModel.Value))
            {
                dvCRHotel.Style.Add("display", "none");
                dvUDHotel.Style.Add("display", "");
            }
            else
            {
                dvCRHotel.Style.Add("display", "");
                dvUDHotel.Style.Add("display", "none");
            }
            return;
        }

        hidModel.Value = "1";
        dvCRHotel.Style.Add("display", "none");
        dvUDHotel.Style.Add("display", "");

        wctSales.CTLDISPLAY = "0";
        wctUCity.CTLDISPLAY = "0";
        wctHotelGroupCode.CTLDISPLAY = "0";
        wctHotel.AutoResult = "";
        hidHotelID.Value = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
        lbHotelNM.Text = hidHotelID.Value + " - " + strHotel.Substring(strHotel.IndexOf(']') + 1);

        BindHotelInfo();
        BindHotelEXInfo();
        RestControlEnable(true, 0);
        //BingControlValEnable();
        //divModify.Style.Add("display", "");
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        //ddpStatusListRemark.Attributes.Add("style", "display:''");
        SetEvalGridBtnStyle(true);
        //btnSelect.Enabled = false;

        this.hotelInfoInlineImage.Attributes.Add("src", "HotelInfoInlineImage.aspx?HotelInfoManagerByHotelID=" + hidHotelID.Value);
        this.detailMessageContent.Attributes.Add("style", "display:none");
    }

    private void RestControlValue()
    {
        txtHotelNM.Text = "";
        txtHotelNMEN.Text = "";

        txtUHotelPN.Text = "";
        txtUTotalRooms.Text = "";
        txtUHotelJP.Text = "";
        txtUZip.Text = "";
        txtUPriceLow.Text = "";
        txtUContactNameZh.Text = "";
        txtUHotelFax.Text = "";
        txtUContactPhone.Text = "";
        txtUHotelTel.Text = "";
        txtUContactEmail.Text = "";
        txtURemark.Text = "";

        if (ddpStatusList.Items.Count > 0)
        {
            ddpStatusList.SelectedIndex = 0;
        }

        if (ddpRoomList.Items.Count > 0)
        {
            ddpRoomList.SelectedIndex = 0;
        }

        //if (ddpHotelGroup.Items.Count > 0)
        //{
        //    ddpHotelGroup.SelectedIndex = 0;
        //}

        if (ddpUStarRating.Items.Count > 0)
        {
            ddpUStarRating.SelectedIndex = 0;
        }

        //if (ddpDiamondRating.Items.Count > 0)
        //{
        //    ddpDiamondRating.SelectedIndex = 0;
        //}

        //if (ddpCity.Items.Count > 0)
        //{
        //    ddpCity.SelectedIndex = 0;
        //}

        //ddpProvincial.SelectedIndex=-1;

        dpOpenDate.Value = "";
        dpRepairDate.Value = "";
        txtAddress.Text = "";
        txtWebSite.Text = "";
        txtPhone.Text = "";
        txtFax.Text = "";
        txtContactPer.Text = "";
        txtLongitude.Text = "";
        txtLatitude.Text = "";
        txtBDLongitude.Text = "";
        txtBDLatitude.Text = "";
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
        chkIsPushFog.Checked = false;
        //chkAutoTrust.Checked = true;
        //divModify.Style.Add("display", "none");
        //UpdatePanel1.Update();                                    
    }

    private void RestControlEnable(bool bStatus, int iSelectType)
    {
        txtHotelNM.Enabled = bStatus;
        txtHotelNMEN.Enabled = bStatus;

        txtUHotelPN.Enabled = bStatus;
        txtUTotalRooms.Enabled = bStatus;
        txtUHotelJP.Enabled = bStatus;
        txtUZip.Enabled = bStatus;
        txtUPriceLow.Enabled = bStatus;
        txtUContactNameZh.Enabled = bStatus;
        txtUHotelFax.Enabled = bStatus;
        txtUContactPhone.Enabled = bStatus;
        txtUHotelTel.Enabled = bStatus;
        txtUContactEmail.Enabled = bStatus;
        txtURemark.Enabled = bStatus;


        ddpStatusList.Enabled = bStatus;
        ddpStatusListRemark.Enabled = bStatus;//首次加载  酒店默认下线  禁用原因菜单
        //ddpHotelGroup.Enabled = bStatus;
        ddpUStarRating.Enabled = bStatus;
        //ddpDiamondRating.Enabled = bStatus;
        //ddpProvincial.Enabled = bStatus;
        //ddpCity.Enabled = bStatus;
        dpOpenDate.Disabled = !bStatus;
        dpRepairDate.Disabled = !bStatus;
        txtAddress.Enabled = bStatus;
        txtWebSite.Enabled = bStatus;
        txtPhone.Enabled = bStatus;
        txtFax.Enabled = bStatus;
        txtContactPer.Enabled = bStatus;
        txtLongitude.Enabled = bStatus;
        txtLatitude.Enabled = bStatus;
        txtBDLongitude.Enabled = bStatus;
        txtBDLatitude.Enabled = bStatus;
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
        chkIsPushFog.Enabled = bStatus;

        ddlUpdateIsMyHotel.Enabled = bStatus;

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetChkControlEnable('" + (bStatus ? "" : "disabled") + "'," + iSelectType.ToString() + ")", true);
    }

    private void BingControlValEnable()
    {
        bool bStatus = true;
        int iSelectType = 0;

        txtHotelNM.Enabled = bStatus;
        txtHotelNMEN.Enabled = bStatus;

        txtUHotelPN.Enabled = bStatus;
        txtUTotalRooms.Enabled = bStatus;
        txtUHotelJP.Enabled = bStatus;
        txtUZip.Enabled = bStatus;
        txtUPriceLow.Enabled = bStatus;
        txtUContactNameZh.Enabled = bStatus;
        txtUHotelFax.Enabled = bStatus;
        txtUContactPhone.Enabled = bStatus;
        txtUHotelTel.Enabled = bStatus;
        txtUContactEmail.Enabled = bStatus;
        txtURemark.Enabled = bStatus;


        ddpStatusList.Enabled = bStatus;
        //ddpHotelGroup.Enabled = bStatus;
        ddpUStarRating.Enabled = bStatus;
        //ddpDiamondRating.Enabled = bStatus;
        //ddpProvincial.Enabled = bStatus;
        //ddpCity.Enabled = bStatus;
        dpOpenDate.Disabled = !bStatus;
        dpRepairDate.Disabled = !bStatus;
        txtAddress.Enabled = bStatus;
        txtWebSite.Enabled = bStatus;
        txtPhone.Enabled = bStatus;
        txtFax.Enabled = bStatus;
        txtContactPer.Enabled = bStatus;
        txtLongitude.Enabled = bStatus;
        txtLatitude.Enabled = bStatus;
        txtBDLongitude.Enabled = bStatus;
        txtBDLatitude.Enabled = bStatus;
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
        chkIsPushFog.Enabled = bStatus;
        ddlUpdateIsMyHotel.Enabled = bStatus;
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
        lbFogStatus.Text = dsResult.Tables[0].Rows[0]["FOGSTATUSDIS"].ToString();
        hidFogStatus.Value = dsResult.Tables[0].Rows[0]["FOGSTATUS"].ToString();

        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString()))
        {
            hidOnline.Value = dsResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString();
            ddpStatusList.SelectedValue = dsResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString();
            if (ddpStatusList.SelectedValue == "1")//上线  隐藏原因选项
            {
                this.ddpStatusListRemark.Attributes.Add("style", "display:none");
            }
            else
            {
                this.ddpStatusListRemark.SelectedValue = dsResult.Tables[0].Rows[0]["REMARK"].ToString();
            }
        }

        //if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["GROUPID"].ToString()))
        //{
        //    if (ddpHotelGroup.Items.FindByValue(dsResult.Tables[0].Rows[0]["GROUPID"].ToString().Trim()) != null)
        //    {
        //        ddpHotelGroup.SelectedValue = dsResult.Tables[0].Rows[0]["GROUPID"].ToString().Trim();
        //    }
        //}

        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["STARRATING"].ToString()))
        {
            //ddpUStarRating.SelectedValue = dsResult.Tables[0].Rows[0]["STARRATING"].ToString() + "|" + dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString();

            ddpUStarRating.SelectedIndex = ddpUStarRating.Items.IndexOf(ddpUStarRating.Items.FindByValue(dsResult.Tables[0].Rows[0]["STARRATING"].ToString() + "|" + dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString()));
        }
        //if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString()))
        //{
        //    ddpDiamondRating.SelectedValue = dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString();
        //}
        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["CITYID"].ToString()))
        {
            //ddpCity.SelectedValue = dsResult.Tables[0].Rows[0]["CITYID"].ToString();
            //hidCityName.Value = ddpCity.SelectedValue;
            hidUCityID.Value = dsResult.Tables[0].Rows[0]["CITYNM"].ToString();
            //wctUCity
            //ddpCity.SelectedIndex = ddpCity.Items.IndexOf(ddpCity.Items.FindByValue(dsResult.Tables[0].Rows[0]["CITYID"].ToString()));
        }

        //Group_Code
        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["GROUPID"].ToString()))
        {
            hidHotelGroup.Value = "[" + dsResult.Tables[0].Rows[0]["GROUPID"].ToString() + "]" + dsResult.Tables[0].Rows[0]["NAMEZH"].ToString();
        }

        //ddpProvincial.SelectedIndex=-1;

        dpOpenDate.Value = dsResult.Tables[0].Rows[0]["OPENDATE"].ToString();
        dpRepairDate.Value = dsResult.Tables[0].Rows[0]["REPAIRDATE"].ToString();
        txtAddress.Text = dsResult.Tables[0].Rows[0]["ADDRESS"].ToString();
        txtWebSite.Text = dsResult.Tables[0].Rows[0]["WEBSITE"].ToString();

        txtSimpleDescZh.Text = dsResult.Tables[0].Rows[0]["SIMPLEDESCZH"].ToString();
        txtDescZh.Text = dsResult.Tables[0].Rows[0]["DESCZH"].ToString();
        string strEvaluation = dsResult.Tables[0].Rows[0]["EVALUATION"].ToString();
        //txtEvaluation.Text = dsResult.Tables[0].Rows[0]["EVALUATION"].ToString();
        //chkAutoTrust.Checked = "1".Equals(dsResult.Tables[0].Rows[0]["AUTOTRUST"].ToString()) ? true : false;
        txtLongitude.Text = dsResult.Tables[0].Rows[0]["LONGITUDE"].ToString();
        txtLatitude.Text = dsResult.Tables[0].Rows[0]["LATITUDE"].ToString();

        txtBDLongitude.Text = dsResult.Tables[0].Rows[0]["BDLONGITUDE"].ToString();
        txtBDLatitude.Text = dsResult.Tables[0].Rows[0]["BDLATITUDE"].ToString();

        txtUTotalRooms.Text = dsResult.Tables[0].Rows[0]["TOTAL_ROOMS"].ToString();
        txtUHotelPN.Text = dsResult.Tables[0].Rows[0]["PINYIN_LONG"].ToString();
        txtUHotelJP.Text = dsResult.Tables[0].Rows[0]["PINYIN_SHORT"].ToString();
        txtUPriceLow.Text = dsResult.Tables[0].Rows[0]["LOW_LIMIT"].ToString();
        txtURemark.Text = dsResult.Tables[0].Rows[0]["REMARK"].ToString();
        txtUZip.Text = dsResult.Tables[0].Rows[0]["ZIP"].ToString();

        txtPhone.Text = dsResult.Tables[0].Rows[0]["PHONE"].ToString();
        txtFax.Text = dsResult.Tables[0].Rows[0]["FAX"].ToString();
        txtContactPer.Text = dsResult.Tables[0].Rows[0]["CONTACTPER"].ToString();
        txtContactEmail.Text = dsResult.Tables[0].Rows[0]["CONTACTEMAIL"].ToString();


        txtUContactNameZh.Text = dsResult.Tables[0].Rows[0]["CONTACT_NAME_ZH"].ToString();
        txtUHotelFax.Text = dsResult.Tables[0].Rows[0]["UFAX"].ToString();
        txtUContactPhone.Text = dsResult.Tables[0].Rows[0]["CONTACT_PHONE"].ToString();
        txtUHotelTel.Text = dsResult.Tables[0].Rows[0]["UPHONE"].ToString();
        txtUContactEmail.Text = dsResult.Tables[0].Rows[0]["CONTACT_EMAIL"].ToString();

        hidUKeyWords.Value = dsResult.Tables[0].Rows[0]["KEYWORDS"].ToString();

        ddlUpdateIsMyHotel.SelectedValue = dsResult.Tables[0].Rows[0]["ISMYHOTEL"].ToString() == "" ? "0" : dsResult.Tables[0].Rows[0]["ISMYHOTEL"].ToString();
        //ArrayList ayHotelImage = HotelInfoBP.BindHotelImagesList(_hotelinfoEntity).HotelInfoDBEntity[0].HotelImage;
        //if (ayHotelImage.Count > 0)
        //{
        //    PreViewImage(ayHotelImage);
        //}
        //else
        //{
        //    ImageReview.InnerHtml = "";
        //}

        SetGridEvalList(strEvaluation);
        GetSalesManager(hidHotelID.Value);
        GetBalanceRoomList(hidHotelID.Value);
        BindHotelTagIngo();
        GetHotelRoomList();
        GetHotelPrRoomList();
        GetHotelRatherList();
    }

    private void GetHotelRoomList()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetHotelRoomList(_hotelinfoEntity).QueryResult;

        gridViewRoomList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewRoomList.DataKeyNames = new string[] { "ROOMCD" };//主键
        gridViewRoomList.DataBind();

        chklRooms.DataSource = dsResult.Tables[0].DefaultView;
        chklRooms.DataTextField = "ROOMNM";
        chklRooms.DataValueField = "ROOMCD";
        chklRooms.DataBind();
    }

    private void GetHotelRatherList()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetHotelRatherList(_hotelinfoEntity).QueryResult;

        gridViewRather.DataSource = dsResult.Tables[0].DefaultView;
        gridViewRather.DataKeyNames = new string[] { "ROOMCD", "DISCOUNTCD" };//主键
        gridViewRather.DataBind();
        DropDownList ddl;
        TextBox txtbx;
        for (int i = 0; i <= gridViewRather.Rows.Count - 1; i++)
        {
            ddl = (DropDownList)gridViewRather.Rows[i].FindControl("ddpDiscount");
            ddl.SelectedValue = gridViewRather.DataKeys[i][1].ToString();

            if ("0".Equals(ddl.SelectedValue))
            {
                txtbx = (TextBox)gridViewRather.Rows[i].FindControl("txtDiscount");
                txtbx.Enabled = false;
            }
        }
    }

    private DataTable GetRoomEditRatherList()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("ROOMCD");
        dtResult.Columns.Add("DISCOUNTCD");
        dtResult.Columns.Add("DISCOUNTXT");
        DropDownList ddl;
        TextBox txtbx;

        for (int i = 0; i <= gridViewRather.Rows.Count - 1; i++)
        {
            DataRow dr = dtResult.NewRow();
            ddl = (DropDownList)gridViewRather.Rows[i].FindControl("ddpDiscount");
            txtbx = (TextBox)gridViewRather.Rows[i].FindControl("txtDiscount");

            dr["ROOMCD"] = gridViewRather.DataKeys[i][0].ToString();
            dr["DISCOUNTCD"] = ddl.SelectedValue;
            dr["DISCOUNTXT"] = txtbx.Text;
            dtResult.Rows.Add(dr);
        }

        return dtResult;
    }

    protected void gridViewRather_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
            dpSalesStart.Value = dsResult.Tables[0].Rows[0]["StartDtime"].ToString().Replace('/', '-');
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
        hotelInfoDBEntity.PriceCode = ddpPriceCode.SelectedValue;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetBalanceRoomList(_hotelinfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            chkHotelRoomList.Items.Clear();
            chkHotelRoomList.DataTextField = "ROOMNM";
            chkHotelRoomList.DataValueField = "ROOMCODE";
            chkHotelRoomList.DataSource = dsResult;
            chkHotelRoomList.DataBind();
        }

        GetBalHotelRoomList(hidHotelID.Value);
    }

    private void GetBalHotelRoomList(string strHotelID)
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetBalHotelRoomList(_hotelinfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
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

    protected void ddpPriceCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        hidSelectedID.Value = "5";

        MessageContent.InnerHtml = "";

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.PriceCode = ddpPriceCode.SelectedValue;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetBalanceRoomList(_hotelinfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            chkHotelRoomList.Items.Clear();
            chkHotelRoomList.DataTextField = "ROOMNM";
            chkHotelRoomList.DataValueField = "ROOMCODE";
            chkHotelRoomList.DataSource = dsResult;
            chkHotelRoomList.DataBind();
        }

        RestBalGridData();
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
                    tcHeader[iHead + 1].HorizontalAlign = HorizontalAlign.Center;
                    tcHeader[iHead + 1].Attributes.Add("colspan", hidLM2Count.Value); //跨Column
                    tcHeader[iHead + 1].Text = "LMBAR2</th></tr><tr  class='GView_HeaderCSS'>";
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
                tcHeader[iHead + 2].Width = 5;
                tcHeader[iHead + 2].HorizontalAlign = HorizontalAlign.Center;
                tcHeader[iHead + 2].Text = "&nbsp;&nbsp;日期/房型&nbsp;&nbsp;";
                tcHeader[iHead + 2].Wrap = false;

                for (int i = 0; i < iLMSum; i++)
                {
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[i + iHead + 3].Width = 5;
                    tcHeader[i + iHead + 3].HorizontalAlign = HorizontalAlign.Center;
                    tcHeader[i + iHead + 3].Text = strColList[i];
                    tcHeader[i + iHead + 3].Wrap = false;
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
        dvHotelEX.Style.Add("display", "");
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
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
        lbFogStatus.Text = dsResult.Tables[0].Rows[0]["FOGSTATUSDIS"].ToString();
        hidFogStatus.Value = dsResult.Tables[0].Rows[0]["FOGSTATUS"].ToString();

        //ddpHotelGroup.SelectedValue = dsResult.Tables[0].Rows[0]["GROUPID"].ToString().Trim();

        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["STARRATING"].ToString()))
        {
            //ddpUStarRating.SelectedValue = dsResult.Tables[0].Rows[0]["STARRATING"].ToString()+"|"+dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString();
            ddpUStarRating.SelectedIndex = ddpUStarRating.Items.IndexOf(ddpUStarRating.Items.FindByValue(dsResult.Tables[0].Rows[0]["STARRATING"].ToString() + "|" + dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString()));
        }
        //if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString()))
        //{
        //    ddpDiamondRating.SelectedValue = dsResult.Tables[0].Rows[0]["DIAMONDRATING"].ToString();
        //}
        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["CITYID"].ToString()))
        {
            //ddpCity.SelectedValue = dsResult.Tables[0].Rows[0]["CITYID"].ToString();
            //wctUCity
            hidUCityID.Value = dsResult.Tables[0].Rows[0]["CITYNM"].ToString();
        }

        dpOpenDate.Value = dsResult.Tables[0].Rows[0]["OPENDATE"].ToString();
        dpRepairDate.Value = dsResult.Tables[0].Rows[0]["REPAIRDATE"].ToString();
        txtAddress.Text = dsResult.Tables[0].Rows[0]["ADDRESS"].ToString();
        txtWebSite.Text = dsResult.Tables[0].Rows[0]["WEBSITE"].ToString();
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
        dvHotelEX.Style.Add("display", "");
        RestControlEnable(true, 2);
        SetEvalGridBtnStyle(true);
        dvLink.Style.Add("display", "");
        dvEvlGrid.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
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
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        if (this.ddpStatusList.SelectedValue == "0")
        {
            this.ddpStatusListRemark.Style.Add("display", "");
        }
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
        dvHotelEX.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        dvEvlGrid.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
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

            ddpStatusList.DataTextField = "ONLINEDIS";
            ddpStatusList.DataValueField = "ONLINESTATUS";
            ddpStatusList.DataSource = dsOnlineStatus;
            ddpStatusList.DataBind();
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

        //DataSet dsHotelGroup = HotelInfoBP.CommonHotelGroupSelect(_hotelinfoEntity).QueryResult;
        //ddpHotelGroup.DataTextField = "NAMEZH";
        //ddpHotelGroup.DataValueField = "GROUPCODE";
        //ddpHotelGroup.DataSource = dsHotelGroup;
        //ddpHotelGroup.DataBind();

        //DataSet dsProvincial = HotelInfoBP.CommonProvincialSelect(_hotelinfoEntity).QueryResult;
        //ddpProvincial.DataTextField = "NAMEZH";
        //ddpProvincial.DataValueField = "GROUPCODE";
        //ddpProvincial.DataSource = dsProvincial;
        //ddpProvincial.DataBind();


        //DataSet dsCity = HotelInfoBP.CommonCitySelect(_hotelinfoEntity).QueryResult;
        //ddpCity.DataTextField = "NAMEZH";
        //ddpCity.DataValueField = "CITYID";
        //ddpCity.DataSource = dsCity;
        //ddpCity.DataBind();

        DataSet dsStarRating = CommonBP.GetConfigList(GetLocalResourceObject("StarMeth").ToString());
        if (dsStarRating.Tables.Count > 0)
        {
            dsStarRating.Tables[0].Columns["Key"].ColumnName = "STARKEY";
            dsStarRating.Tables[0].Columns["Value"].ColumnName = "STARDIS";

            ddpUStarRating.DataTextField = "STARDIS";
            ddpUStarRating.DataValueField = "STARKEY";
            ddpUStarRating.DataSource = dsStarRating;
            ddpUStarRating.DataBind();
        }

        //DataSet dsDiamondRating = CommonBP.GetConfigList(GetLocalResourceObject("DiamondRating").ToString());
        //if (dsDiamondRating.Tables.Count > 0)
        //{
        //    dsDiamondRating.Tables[0].Columns["Key"].ColumnName = "DIAMONDKEY";
        //    dsDiamondRating.Tables[0].Columns["Value"].ColumnName = "DIAMONDDIS";

        //    ddpDiamondRating.DataTextField = "DIAMONDDIS";
        //    ddpDiamondRating.DataValueField = "DIAMONDKEY";
        //    ddpDiamondRating.DataSource = dsDiamondRating;
        //    ddpDiamondRating.DataBind();
        //}

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

        DataSet dsRoomNM = CommonBP.GetConfigListForSort(GetLocalResourceObject("RoomNM").ToString()); //HotelInfoBP.GetAddRoomList();// 
        if (dsRoomNM.Tables.Count > 0)
        {
            dsRoomNM.Tables[0].Columns["Value"].ColumnName = "sName";
            dsRoomNM.Tables[0].Columns["Key"].ColumnName = "PK_ID";
            cddpRoomNm.DataSource = dsRoomNM.Tables[0].DefaultView;
            cddpRoomNm.DataTextField = "sName";
            cddpRoomNm.DataValueField = "PK_ID";
            cddpRoomNm.DataBind();
        }


        DataSet dsGuestMax = CommonBP.GetConfigListForSort(GetLocalResourceObject("GuestMax").ToString());
        if (dsGuestMax.Tables.Count > 0)
        {
            dsGuestMax.Tables[0].Columns["Key"].ColumnName = "MGuestKEY";
            dsGuestMax.Tables[0].Columns["Value"].ColumnName = "MGuestDIS";

            ddlRoomPer.DataTextField = "MGuestDIS";
            ddlRoomPer.DataValueField = "MGuestKEY";
            ddlRoomPer.DataSource = dsGuestMax;
            ddlRoomPer.DataBind();
        }

        //DataSet dsWlan = CommonBP.GetConfigListForSort(GetLocalResourceObject("WLAN").ToString());
        //if (dsWlan.Tables.Count > 0)
        //{
        //    dsWlan.Tables[0].Columns["Key"].ColumnName = "WLanKEY";
        //    dsWlan.Tables[0].Columns["Value"].ColumnName = "WLanDIS";

        //    chklWLAN.DataTextField = "WLanDIS";
        //    chklWLAN.DataValueField = "WLanKEY";
        //    chklWLAN.DataSource = dsWlan;
        //    chklWLAN.DataBind();
        //}

        //DataSet dsGuesType = CommonBP.GetConfigListForSort(GetLocalResourceObject("GUESTTYPE").ToString());
        //if (dsGuesType.Tables.Count > 0)
        //{
        //    dsGuesType.Tables[0].Columns["Key"].ColumnName = "GuesTKEY";
        //    dsGuesType.Tables[0].Columns["Value"].ColumnName = "GuesTDIS";

        //    chklGuesType.DataTextField = "GuesTDIS";
        //    chklGuesType.DataValueField = "GuesTKEY";
        //    chklGuesType.DataSource = dsGuesType;
        //    chklGuesType.DataBind();
        //}

        //DataSet dsWindow = CommonBP.GetConfigListForSort(GetLocalResourceObject("WINDOW").ToString());
        //if (dsWindow.Tables.Count > 0)
        //{
        //    dsWindow.Tables[0].Columns["Key"].ColumnName = "WindowKEY";
        //    dsWindow.Tables[0].Columns["Value"].ColumnName = "WindowDIS";

        //    chklWindow.DataTextField = "WindowDIS";
        //    chklWindow.DataValueField = "WindowKEY";
        //    chklWindow.DataSource = dsWindow;
        //    chklWindow.DataBind();
        //}

        //DataSet dsSmoke = CommonBP.GetConfigListForSort(GetLocalResourceObject("SMOKE").ToString());
        //if (dsSmoke.Tables.Count > 0)
        //{
        //    dsSmoke.Tables[0].Columns["Key"].ColumnName = "SmokeKEY";
        //    dsSmoke.Tables[0].Columns["Value"].ColumnName = "SmokeDIS";

        //    chklSmoke.DataTextField = "SmokeDIS";
        //    chklSmoke.DataValueField = "SmokeKEY";
        //    chklSmoke.DataSource = dsSmoke;
        //    chklSmoke.DataBind();
        //}
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

    private bool chkDaveValEmpty()
    {
        //if (String.IsNullOrEmpty(txtPhone.Text.Trim()) || String.IsNullOrEmpty(txtFax.Text.Trim()) || String.IsNullOrEmpty(txtQueryRoomLinkMan.Text.Trim()) || String.IsNullOrEmpty(txtQueryRoomLinkTel.Text.Trim()) || String.IsNullOrEmpty(txtQueryRoomLinkFax.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmDayLinkMan.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmDayLinkTel.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmDayLinkFax.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmNightLinkMan.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmNightLinkTel.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmNightLinkFax.Text.Trim()) || String.IsNullOrEmpty(rdOrderVerifyLinkMan.Text.Trim()) || String.IsNullOrEmpty(rdOrderVerifyLinkTel.Text.Trim()) || String.IsNullOrEmpty(rdOrderVerifyLinkFax.Text.Trim()))
        if (String.IsNullOrEmpty(txtQueryRoomLinkMan.Text.Trim()) || String.IsNullOrEmpty(txtQueryRoomLinkTel.Text.Trim()) || String.IsNullOrEmpty(txtQueryRoomLinkFax.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmDayLinkMan.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmDayLinkTel.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmDayLinkFax.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmNightLinkMan.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmNightLinkTel.Text.Trim()) || String.IsNullOrEmpty(txtOrderAffirmNightLinkFax.Text.Trim()) || String.IsNullOrEmpty(rdOrderVerifyLinkMan.Text.Trim()) || String.IsNullOrEmpty(rdOrderVerifyLinkTel.Text.Trim()) || String.IsNullOrEmpty(rdOrderVerifyLinkFax.Text.Trim()))
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    private bool chkDaveValVaild()
    {
        if (!chkFaxVaild(txtQueryRoomLinkFax.Text.Trim()) || !chkFaxVaild(txtOrderAffirmDayLinkFax.Text.Trim()) || !chkFaxVaild(txtOrderAffirmNightLinkFax.Text.Trim()) || !chkFaxVaild(rdOrderVerifyLinkFax.Text.Trim()))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool chkFaxVaild(string arg)
    {
        if (String.IsNullOrEmpty(arg))
        {
            return true;
        }

        string strPatern = @"^[\d+|\+]+$";
        Regex reg = new Regex(strPatern);
        if (reg.IsMatch(arg))
        {
            return true;
        }
        return false;
    }

    public bool btnAddData()
    {
        MessageContent.InnerHtml = "";
        bool bFlag = true;
        string msgString = "";

        #region add  判断是否已和供应商酒店绑定
        //因为控件在保存的时候 还是会自动查询  所以 只能重新查询  来判断
        ELRelationEntity _ELRelationEntity = new ELRelationEntity();
        _ELRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _ELRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _ELRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _ELRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _ELRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity elrelationDBEntity = new ELRelationDBEntity();
        elrelationDBEntity.HVPID = hidHotelID.Value;
        _ELRelationEntity.ELRelationDBEntity.Add(elrelationDBEntity);
        DataTable ds = ELRelationBP.HVPHotelSelectCircle(_ELRelationEntity).QueryResult.Tables[0];
        if (ds == null || ds.Rows.Count <= 0)
        {
            MessageContent.InnerHtml = "该酒店未绑定供应商酒店ID，请先绑定再设置商圈" + "<br/>";
            //bFlag = false;
            return false;
        }
        #endregion

        #region 编辑酒店各项验证
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

        //酒店下线 必须选择下线原因
        if (this.ddpStatusList.SelectedValue == "0")//下线
        {
            if (String.IsNullOrEmpty(this.ddpStatusListRemark.SelectedValue))
            {
                msgString = msgString + GetLocalResourceObject("UpdateErrorDdpStatusListRemark").ToString() + "<br/>";
                bFlag = false;
            }
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

        if (StringUtility.Text_Length(txtWebSite.Text.ToString().Trim()) > 200)
        {
            msgString = msgString + GetLocalResourceObject("UpdateError5").ToString() + "<br/>";
            bFlag = false;
        }

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

        if (!String.IsNullOrEmpty(txtSimpleDescZh.Text.ToString().Trim()) && (StringUtility.Text_Length(txtSimpleDescZh.Text.ToString().Trim()) > 600))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError8").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtDescZh.Text.ToString().Trim()) && (StringUtility.Text_Length(txtDescZh.Text.ToString().Trim()) > 2000))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError81").ToString() + "<br/>";
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

        if (String.IsNullOrEmpty(txtUHotelPN.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError115").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtUHotelPN.Text.ToString().Trim()) > 1000)
        {
            msgString = msgString + GetLocalResourceObject("CreateError101").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtUTotalRooms.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError102").ToString() + "<br/>";
            bFlag = false;
        }

        if (!ChkNumber(txtUTotalRooms.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError103").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtUPriceLow.Text.ToString().Trim()) || !ChkNumber(txtUPriceLow.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError117").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtUHotelJP.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError116").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtUHotelJP.Text.ToString().Trim()) > 1000)
        {
            msgString = msgString + GetLocalResourceObject("CreateError104").ToString() + "<br/>";
            bFlag = false;
        }

        string strCity = hidUCityID.Value.Trim();

        if (String.IsNullOrEmpty(strCity.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError105").ToString() + "<br/>";
            bFlag = false;
        }
        else if (!strCity.Contains("[") && !strCity.Contains("]"))
        {
            msgString = msgString + GetLocalResourceObject("CreateError118").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtUHotelFax.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError71").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtUHotelFax.Text.ToString().Trim()) && (StringUtility.Text_Length(txtUHotelFax.Text.ToString().Trim()) > 100))
        {
            msgString = msgString + GetLocalResourceObject("CreateError7").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtURemark.Text.ToString().Trim()) && (StringUtility.Text_Length(txtURemark.Text.ToString().Trim()) > 2000))
        {
            msgString = msgString + GetLocalResourceObject("CreateError91").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtUContactPhone.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError110").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtUContactPhone.Text.ToString().Trim()) && (StringUtility.Text_Length(txtUContactPhone.Text.ToString().Trim()) > 30))
        {
            msgString = msgString + GetLocalResourceObject("CreateError111").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtUHotelTel.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError61").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtUHotelTel.Text.ToString().Trim()) && (StringUtility.Text_Length(txtUHotelTel.Text.ToString().Trim()) > 40))
        {
            msgString = msgString + GetLocalResourceObject("CreateError6").ToString() + "<br/>";
            bFlag = false;
        }

        //if (String.IsNullOrEmpty(txtUContactEmail.Text.ToString().Trim()))
        //{
        //    msgString = msgString + GetLocalResourceObject("CreateError113").ToString() + "<br/>";
        //    bFlag = false;
        //}

        if (StringUtility.Text_Length(txtUContactEmail.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("CreateError11").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(hidUKeyWords.Value) && (StringUtility.Text_Length(hidUKeyWords.Value.Trim().TrimEnd(',')) > 300))
        {
            msgString = msgString + GetLocalResourceObject("CreateError10").ToString() + "<br/>";
            bFlag = false;
        }





        if (!hidOnline.Value.Equals(ddpStatusList.SelectedValue) && !chkDaveValEmpty())
        {
            msgString = msgString + GetLocalResourceObject("UpdateError91").ToString() + "<br/>";
            bFlag = false;
        }

        if (!bFlag)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString() + "<br/>" + msgString;
            return false;
        }
        #endregion

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        //hotelInfoDBEntity.ID = hidHotelNo.Value;
        //hotelInfoDBEntity.HotelID = hidHotelID.Value;
        //hotelInfoDBEntity.Name_CN = txtHotelNM.Text.Trim();
        //hotelInfoDBEntity.Name_EN = txtHotelNMEN.Text.Trim();
        //hotelInfoDBEntity.Status = ddpStatusList.SelectedValue;
        //hotelInfoDBEntity.StarRating = ddpUStarRating.SelectedValue.Split(',')[0].ToString();
        //hotelInfoDBEntity.DiamondRating = ddpUStarRating.SelectedValue.Split(',')[1].ToString();
        //hotelInfoDBEntity.City = hidCityID.Value;// ddpCity.SelectedValue;
        //hotelInfoDBEntity.OpenDate = dpOpenDate.Value;
        //hotelInfoDBEntity.RepairDate = dpRepairDate.Value;
        //hotelInfoDBEntity.AddRess = txtAddress.Text.Trim();
        //hotelInfoDBEntity.WebSite = txtWebSite.Text.Trim();
        //hotelInfoDBEntity.Longitude = txtLongitude.Text.Trim();
        //hotelInfoDBEntity.Latitude = txtLatitude.Text.Trim();
        //hotelInfoDBEntity.SimpleDescZh = txtSimpleDescZh.Text.Trim();
        //hotelInfoDBEntity.DescZh = txtDescZh.Text.Trim();
        //hotelInfoDBEntity.Evaluation = Evaluation;
        //hotelInfoDBEntity.AutoTrust = "1";
        //hotelInfoDBEntity.FogStatus = hidFogStatus.Value;

        hotelInfoDBEntity.ID = hidHotelNo.Value;
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.Name_CN = txtHotelNM.Text.Trim();
        hotelInfoDBEntity.Name_EN = txtHotelNMEN.Text.Trim();
        hotelInfoDBEntity.Status = ddpStatusList.SelectedValue;
        if (ddpStatusList.SelectedValue == "0")
        {
            hotelInfoDBEntity.Remark = ddpStatusListRemark.SelectedValue;
        }
        hotelInfoDBEntity.City = (hidUCityID.Value.IndexOf("]") >= 0) ? hidUCityID.Value.Substring((hidUCityID.Value.IndexOf('[') + 1), (hidUCityID.Value.IndexOf(']') - 1)) : "";
        hotelInfoDBEntity.HotelGroup = (hidHotelGroup.Value.IndexOf("]") >= 0) ? hidHotelGroup.Value.Substring((hidHotelGroup.Value.IndexOf('[') + 1), (hidHotelGroup.Value.IndexOf(']') - 1)) : "";//酒店集团
        hotelInfoDBEntity.StarRating = ddpUStarRating.SelectedValue;
        hotelInfoDBEntity.AddRess = txtAddress.Text.Trim();
        hotelInfoDBEntity.Phone = txtUHotelTel.Text.Trim();
        hotelInfoDBEntity.Fax = txtUHotelFax.Text.Trim();
        hotelInfoDBEntity.Longitude = txtLongitude.Text.Trim();
        hotelInfoDBEntity.Latitude = txtLatitude.Text.Trim();
        hotelInfoDBEntity.BDLongitude = txtBDLongitude.Text.Trim();
        hotelInfoDBEntity.BDLatitude = txtBDLatitude.Text.Trim();
        hotelInfoDBEntity.Bussiness = hidBussList.Value.Trim();
        hotelInfoDBEntity.OpenDate = dpOpenDate.Value;
        hotelInfoDBEntity.RepairDate = dpRepairDate.Value;
        hotelInfoDBEntity.SimpleDescZh = txtSimpleDescZh.Text.Trim();
        hotelInfoDBEntity.DescZh = txtDescZh.Text.Trim();
        hotelInfoDBEntity.Status = ddpStatusList.SelectedValue.Trim();
        hotelInfoDBEntity.HotelPN = txtUHotelPN.Text.Trim();
        hotelInfoDBEntity.TotalRooms = txtUTotalRooms.Text.Trim();
        hotelInfoDBEntity.HotelJP = txtUHotelJP.Text.Trim();
        hotelInfoDBEntity.Zip = txtUZip.Text.Trim();
        hotelInfoDBEntity.PriceLow = txtUPriceLow.Text.Trim();
        hotelInfoDBEntity.ContactPer = txtUContactNameZh.Text.Trim();
        hotelInfoDBEntity.ContactPhone = txtUContactPhone.Text.Trim();
        hotelInfoDBEntity.ContactEmail = txtUContactEmail.Text.Trim();
        hotelInfoDBEntity.WebSite = txtWebSite.Text.Trim();
        hotelInfoDBEntity.Evaluation = Evaluation;
        hotelInfoDBEntity.HotelRemark = txtURemark.Text.ToString().Trim();
        hotelInfoDBEntity.FogStatus = hidFogStatus.Value;
        hotelInfoDBEntity.KeyWords = hidUKeyWords.Value.Trim().TrimEnd(',');
        hotelInfoDBEntity.IsMyHotel = ddlUpdateIsMyHotel.SelectedValue;//是否为自签酒店

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        int iResult = HotelInfoBP.UpdateHotelInfo(_hotelinfoEntity).Result;

        _commonEntity.LogMessages = _hotelinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店基础信息-保存";
        commonDBEntity.Event_ID = hidHotelID.Value;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, hidHotelID.Value, hotelInfoDBEntity.Name_CN, hotelInfoDBEntity.Name_EN, hotelInfoDBEntity.City, hotelInfoDBEntity.StarRating, hotelInfoDBEntity.AddRess, hotelInfoDBEntity.Phone, hotelInfoDBEntity.Fax, hotelInfoDBEntity.Longitude, hotelInfoDBEntity.Latitude, hotelInfoDBEntity.Bussiness, hotelInfoDBEntity.OpenDate, hotelInfoDBEntity.RepairDate, hotelInfoDBEntity.SimpleDescZh, hotelInfoDBEntity.DescZh, hotelInfoDBEntity.Status, hotelInfoDBEntity.HotelPN, hotelInfoDBEntity.TotalRooms, hotelInfoDBEntity.HotelJP, hotelInfoDBEntity.Zip, hotelInfoDBEntity.PriceLow, hotelInfoDBEntity.ContactPer, hotelInfoDBEntity.ContactPhone, hotelInfoDBEntity.ContactEmail, hotelInfoDBEntity.WebSite, hotelInfoDBEntity.Evaluation, hotelInfoDBEntity.HotelRemark, hotelInfoDBEntity.BDLongitude, hotelInfoDBEntity.BDLatitude, hotelInfoDBEntity.KeyWords);

        //string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        //conTent = string.Format(conTent, hotelInfoDBEntity.HotelID, hotelInfoDBEntity.Name_CN, hotelInfoDBEntity.Status, hotelInfoDBEntity.HotelGroup, hotelInfoDBEntity.StarRating, hotelInfoDBEntity.DiamondRating, hotelInfoDBEntity.City, hotelInfoDBEntity.OpenDate.ToString(), hotelInfoDBEntity.RepairDate.ToString(), hotelInfoDBEntity.AddRess, hotelInfoDBEntity.WebSite, hotelInfoDBEntity.SimpleDescZh, hotelInfoDBEntity.DescZh, hotelInfoDBEntity.Evaluation);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError331").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError331").ToString();
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
        dvHotelEX.Style.Add("display", "");
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        RestControlEnable(true, 2);
        RestBalGridData();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Page.ClientScript.RegisterStartupScript(typeof(string), "key1", "<script>window.open('HotelBusinessCircle.aspx?type=page&city=" + ddpCity.SelectedValue + "&hotelId=" + hidHotelID.Value + "')</script>");
        Page.ClientScript.RegisterStartupScript(typeof(string), "key1", "<script>window.showModalDialog('HotelBusinessCircle.aspx?type=page&city=" + wctUCity.AutoResult + "&hotelId=" + hidHotelID.Value + "')</script>");
    }

    /// <summary>
    /// 绑定酒店商圈
    /// </summary>
    public void BindHotelTagIngo()
    {
        MessageContent.InnerHtml = "";
        ELRelationEntity _ELRelationEntity = new ELRelationEntity();
        _ELRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _ELRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _ELRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _ELRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _ELRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity elrelationDBEntity = new ELRelationDBEntity();
        elrelationDBEntity.HVPID = hidHotelID.Value;
        _ELRelationEntity.ELRelationDBEntity.Add(elrelationDBEntity);
        DataTable ds = ELRelationBP.HVPHotelSelectCircle(_ELRelationEntity).QueryResult.Tables[0];
        if (ds != null && ds.Rows.Count > 0)
        {
            _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
            HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
            hotelInfoDBEntity.HotelID = hidHotelID.Value;
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

            DataSet dsResult = HotelInfoBP.GetTagInfoAERA(_hotelinfoEntity).QueryResult;
            StringBuilder sb = new StringBuilder();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<span style='background:#DBEAF9;height:15px;margin-right:10px;background-position: right center;background-repeat: no-repeat'>" + dsResult.Tables[0].Rows[i]["REVALUE_ALL"].ToString() + "</span>");
                }
            }
            sb.Append("<input type='button' id='Button1' runat='server' class='btn primary' value='修改' onclick='PopupArea()' />");
            dvUserGroupList.InnerHtml = sb.ToString();
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<span style='color:red'>该酒店未绑定供应商酒店ID，请先绑定再设置商圈</span>");
            dvUserGroupList.InnerHtml = sb.ToString();
        }
    }

    protected void btnSaveRoom_Click(object sender, EventArgs e)
    {
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        hidSelectedID.Value = "1";
        dvSaveRoom.Style.Add("display", "");
        dvRoomGrid.Style.Add("display", "none");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");

        if ("1".Equals(hidRoomACT.Value.Trim()))
        {
            dvRoomCD.Style.Add("display", "");
            lbRoomCD.Style.Add("display", "none");
        }
        else
        {
            dvRoomCD.Style.Add("display", "none");
            lbRoomCD.Style.Add("display", "");
        }


        RestBalGridData();

        MessageContent.InnerHtml = "";
        bool bFlag = true;
        string msgString = "";

        if (String.IsNullOrEmpty(cddpRoomNm.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError100").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(ddtbKeyVal.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError101").ToString() + "<br/>";
            bFlag = false;
        }

        string strBedtype = (hidBedCD.Value.Contains("[") && hidBedCD.Value.Contains("]")) ? hidBedCD.Value.Substring((hidBedCD.Value.IndexOf('[') + 1), (hidBedCD.Value.IndexOf(']') - 1)) : "";
        if (String.IsNullOrEmpty(strBedtype.Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError102").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtRoomArea.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("UpdateError103").ToString() + "<br/>";
            bFlag = false;
        }

        //if (!String.IsNullOrEmpty(txtRoomArea.Text.ToString().Trim()) && !CheckRoomAreNum(txtRoomArea.Text.ToString().Trim()))
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError104").ToString() + "<br/>";
        //    bFlag = false;
        //}

        if (!bFlag)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError131").ToString() + "<br/>" + msgString;
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddRoomStyle('1')", true);
            return;
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.RoomACT = hidRoomACT.Value;
        hotelInfoDBEntity.RoomCode = ("1".Equals(hidRoomACT.Value.Trim())) ? ddtbKeyVal.Text.Trim() : lbRoomCD.Text.Trim();
        hotelInfoDBEntity.RoomNM = cddpRoomNm.Text.Trim();
        hotelInfoDBEntity.RoomStatus = rdlRoomStatus.SelectedValue.Trim();
        hotelInfoDBEntity.BedType = strBedtype;
        hotelInfoDBEntity.RoomPer = ddlRoomPer.SelectedValue.Trim();
        hotelInfoDBEntity.RoomArea = txtRoomArea.Text.Trim();
        hotelInfoDBEntity.RoomWlan = rdlWlan.SelectedValue.Trim();
        hotelInfoDBEntity.GuesType = rdlGuesType.SelectedValue.Trim();
        hotelInfoDBEntity.RoomWindow = rdlWindow.SelectedValue.Trim();
        hotelInfoDBEntity.RoomSmoke = rdlSmoke.SelectedValue.Trim();

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        int iResult = HotelInfoBP.SaveHotelRoomsList(_hotelinfoEntity);


        _commonEntity.LogMessages = _hotelinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店房型管理-保存";
        commonDBEntity.Event_ID = hidHotelID.Value;
        string conTent = GetLocalResourceObject("EventInsertRoomMessage").ToString();

        conTent = string.Format(conTent, hotelInfoDBEntity.HotelID, hotelInfoDBEntity.RoomNM, hotelInfoDBEntity.RoomCode, hotelInfoDBEntity.RoomStatus, hotelInfoDBEntity.BedType, hotelInfoDBEntity.RoomPer, hotelInfoDBEntity.RoomArea, hotelInfoDBEntity.RoomWlan, hotelInfoDBEntity.GuesType, hotelInfoDBEntity.RoomWindow, hotelInfoDBEntity.RoomSmoke);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateRoomSuccess").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateRoomSuccess").ToString();
            GetHotelRoomList();
            dvSaveRoom.Style.Add("display", "none");
            dvRoomGrid.Style.Add("display", "");
            dvRoomCD.Style.Add("display", "");
            lbRoomCD.Style.Add("display", "none");
            dvRoomHis.Style.Add("display", "none");
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateRoomError2").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateRoomError2").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateRoomError4").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateRoomError4").ToString();
        }
        else if (iResult == 4)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateRoomError5").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateRoomError5").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError131").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError131").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void gridViewRoomList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewRoomList.PageIndex = e.NewPageIndex;
        GetHotelRoomList();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSalesControlVal()", true);
    }

    protected void gridViewRoomList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string strHotelID = myGridView.Rows[e.RowIndex].Cells[0].Text.ToString();
        //string strTypeID = myGridView.DataKeys[e.RowIndex].Value.ToString();

        //_appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        //_appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        //_appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        //APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        //appcontentDBEntity.HotelID = strHotelID;
        //appcontentDBEntity.TypeID = strTypeID;

        //_appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        //int iResult = APPContentBP.DeleteHotelIgnoreGrid(_appcontentEntity);

        //_commonEntity.LogMessages = _appcontentEntity.LogMessages;
        //_commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        //CommonDBEntity commonDBEntity = new CommonDBEntity();

        //commonDBEntity.Event_Type = "APP自动审核-配置免检项目-删除";
        //commonDBEntity.Event_ID = strHotelID;
        //string conTent = GetLocalResourceObject("EventDeleteMessage").ToString();
        //conTent = string.Format(conTent, strHotelID, strTypeID);
        //commonDBEntity.Event_Content = conTent;

        //if (iResult == 1)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("DeleteSuccess").ToString();
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("DeleteSuccess").ToString();
        //}
        //else
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("DeleteError").ToString();
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("DeleteError").ToString();
        //}
        //_commonEntity.CommonDBEntity.Add(commonDBEntity);
        //CommonBP.InsertEventHistory(_commonEntity);

        //PopReseachData();
    }

    protected void gridViewRoomHis_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewRoomHis.PageIndex = e.NewPageIndex;
        GetHotelRoomHistoryList();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSalesControlVal()", true);
    }

    private bool CheckRoomAreNum(string parm)
    {
        if (String.IsNullOrEmpty(parm))
        {
            return false;
        }

        try
        {
            if (Decimal.Parse(parm) > 0)
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

    protected void btnLdRoomData_Click(object sender, EventArgs e)
    {
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");

        if ("1".Equals(hidRoomACT.Value.Trim()))
        {
            dvRoomCD.Style.Add("display", "");
            lbRoomCD.Style.Add("display", "none");
        }
        else
        {
            dvRoomCD.Style.Add("display", "none");
            lbRoomCD.Style.Add("display", "");
        }

        hidSelectedID.Value = "1";
        RestBalGridData();

        MessageContent.InnerHtml = "";
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.RoomCode = hidRoomCD.Value;

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.BindUpdateRoomData(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateRoomError3").ToString();
            dvSaveRoom.Style.Add("display", "none");
            dvRoomGrid.Style.Add("display", "");
            dvRoomCD.Style.Add("display", "");
            lbRoomCD.Style.Add("display", "none");
            return;
        }

        ddtbKeyVal.Text = dsResult.Tables[0].Rows[0]["ROOMCD"].ToString();
        cddpRoomNm.Text = dsResult.Tables[0].Rows[0]["ROOMNM"].ToString();
        rdlRoomStatus.SelectedValue = dsResult.Tables[0].Rows[0]["status"].ToString();
        //txtBed.Text = dsResult.Tables[0].Rows[0]["bed_type"].ToString();
        hidBedCD.Value = dsResult.Tables[0].Rows[0]["BEDTYPE"].ToString();
        ddlRoomPer.SelectedValue = dsResult.Tables[0].Rows[0]["max_guest"].ToString();
        txtRoomArea.Text = dsResult.Tables[0].Rows[0]["room_area"].ToString();
        rdlWlan.SelectedValue = dsResult.Tables[0].Rows[0]["network"].ToString();
        rdlGuesType.SelectedValue = dsResult.Tables[0].Rows[0]["guest_type"].ToString();
        rdlWindow.SelectedValue = dsResult.Tables[0].Rows[0]["is_window"].ToString();
        rdlSmoke.SelectedValue = dsResult.Tables[0].Rows[0]["is_nosmoke"].ToString();

        lbRoomCD.Text = dsResult.Tables[0].Rows[0]["ROOMCD"].ToString();

        dvSaveRoom.Style.Add("display", "");
        dvRoomGrid.Style.Add("display", "none");
        dvRoomCD.Style.Add("display", "none");
        lbRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        GetHotelRoomHistoryList();
    }

    private void GetHotelRoomHistoryList()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.RoomCode = hidRoomCD.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetHotelRoomHistoryList(_hotelinfoEntity).QueryResult;

        gridViewRoomHis.DataSource = dsResult.Tables[0].DefaultView;
        gridViewRoomHis.DataKeyNames = new string[] { "ROOMNM" };//主键
        gridViewRoomHis.DataBind();
        dvRoomHis.Style.Add("display", "");
    }

    protected void btnPrRoom_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "");
        dvPrRoomGrid.Style.Add("display", "none");
        hidSelectedID.Value = "4";
        RestBalGridData();
        if ("1".Equals(hidPRRoomACT.Value.Trim()))
        {
            dvPrPlan.Style.Add("display", "none");
            rbtlPriceCD.Enabled = true;
        }
        else
        {
            dvPrPlan.Style.Add("display", "");
            rbtlPriceCD.Enabled = false;
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.PRRoomACT = hidPRRoomACT.Value;
        hotelInfoDBEntity.PriceCode = rbtlPriceCD.SelectedValue;
        hotelInfoDBEntity.PRStatus = rbtlPrSt.SelectedValue;

        string strPriceRooms = "";
        foreach (ListItem chk in chklRooms.Items)
        {
            if (chk.Selected)
            {
                strPriceRooms = strPriceRooms + chk.Value.Trim() + ",";
            }
        }
        strPriceRooms = strPriceRooms.TrimEnd(',');
        hotelInfoDBEntity.PRRoomS = strPriceRooms;
        hotelInfoDBEntity.UPPlan = (chkUpdatePLan.Checked) ? "1" : "0";

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        _hotelinfoEntity = HotelInfoBP.SaveHotelPrRoomsList(_hotelinfoEntity);

        int iResult = _hotelinfoEntity.Result;
        _commonEntity.LogMessages = _hotelinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "价格代码管理-保存";
        commonDBEntity.Event_ID = hidHotelID.Value;
        string conTent = GetLocalResourceObject("EventInsertPrMessage").ToString();

        conTent = string.Format(conTent, hotelInfoDBEntity.HotelID, hotelInfoDBEntity.PriceCode, hotelInfoDBEntity.PRStatus, hotelInfoDBEntity.PRRoomS, hotelInfoDBEntity.UPPlan, hotelInfoDBEntity.PRRoomACT);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            string strSucc = ("1".Equals(hotelInfoDBEntity.UPPlan)) ? GetLocalResourceObject("UpdatePrSuccess").ToString() : GetLocalResourceObject("UpdatePrSuccess1").ToString();
            commonDBEntity.Event_Result = strSucc;
            MessageContent.InnerHtml = strSucc;
            GetHotelPrRoomList();
            dvPrRoom.Style.Add("display", "none");
            dvPrRoomGrid.Style.Add("display", "");
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = _hotelinfoEntity.ErrorMSG;
            MessageContent.InnerHtml = _hotelinfoEntity.ErrorMSG;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdatePrError3").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("UpdatePrError3").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void btnLdPRRoomData_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        hidSelectedID.Value = "4";
        RestBalGridData();

        dvPrRoom.Style.Add("display", "");
        dvPrRoomGrid.Style.Add("display", "none");

        if ("1".Equals(hidPRRoomACT.Value.Trim()))
        {
            dvPrPlan.Style.Add("display", "none");
            rbtlPriceCD.Enabled = true;
        }
        else
        {
            dvPrPlan.Style.Add("display", "");
            rbtlPriceCD.Enabled = false;
        }

        rbtlPriceCD.SelectedValue = hidPRCD.Value.Trim();
        rbtlPrSt.SelectedValue = hidPRST.Value.Trim();
        chkUpdatePLan.Checked = false;

        MessageContent.InnerHtml = "";
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        hotelInfoDBEntity.PriceCode = hidPRCD.Value;

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.BindUpdatePrRoomData(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            foreach (ListItem chk in chklRooms.Items)
            {
                chk.Selected = (dsResult.Tables[0].Select("room_code='" + chk.Value.Trim() + "'").Count() > 0) ? true : false;
            }
        }
        else
        {
            foreach (ListItem chk in chklRooms.Items)
            {
                chk.Selected = false;
            }
        }
    }

    protected void gridViewPrRoomList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewPrRoomList.PageIndex = e.NewPageIndex;
        GetHotelPrRoomList();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSalesControlVal()", true);
    }

    private void GetHotelPrRoomList()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hidHotelID.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetHotelPrRoomList(_hotelinfoEntity).QueryResult;

        gridViewPrRoomList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewPrRoomList.DataKeyNames = new string[] { "PRCD" };//主键
        gridViewPrRoomList.DataBind();
    }

    public DataSet ddlDDpbind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("Discount").ToString());
        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "DISCOUNTCD";
            dsResult.Tables[0].Columns["Value"].ColumnName = "DISCOUNTDIS";
        }
        return dsResult;
    }

    protected void ddpDiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddpDis = (DropDownList)sender;
        TextBox txtbx;
        GridViewRow gvr = (GridViewRow)ddpDis.NamingContainer;
        int iEdit = gvr.RowIndex;

        if (!"0".Equals(ddpDis.SelectedValue))
        {
            txtbx = (TextBox)gridViewRather.Rows[iEdit].FindControl("txtDiscount");
            txtbx.Enabled = true;
        }
        else
        {
            txtbx = (TextBox)gridViewRather.Rows[iEdit].FindControl("txtDiscount");
            txtbx.Text = "";
            txtbx.Enabled = false;
        }

        hidSelectedID.Value = "3";
        MessageContent.InnerHtml = "";
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        RestBalGridData();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetSalesControlVal()", true);
    }
    #endregion

    #region 新建酒店信息
    protected void gridViewCEvaluationList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        MessageContent.InnerHtml = "";
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("Content");

        for (int i = 0; i < this.gridViewCEvaluationList.Rows.Count; i++)
        {
            if (i == e.RowIndex)
            {
                continue;
            }
            TextBox txtBox = (TextBox)gridViewCEvaluationList.Rows[i].FindControl("txtEvalist");
            DataRow drRow = dtResult.NewRow();
            drRow[0] = txtBox.Text;
            dtResult.Rows.Add(drRow);
        }

        gridViewCEvaluationList.DataSource = dtResult.DefaultView;
        gridViewCEvaluationList.DataKeyNames = new string[] { "Content" };//主键
        gridViewCEvaluationList.DataBind();

        if (dtResult.Rows.Count > 0)
        {
            gridViewCEvaluationList.HeaderRow.Visible = false;
        }
        ResetCreateHoteInfo();
    }

    protected void lkBtnCAdd_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("Content");

        for (int i = 0; i < this.gridViewCEvaluationList.Rows.Count; i++)
        {
            TextBox txtBox = (TextBox)gridViewCEvaluationList.Rows[i].FindControl("txtCEvalist");
            DataRow drRow = dtResult.NewRow();
            drRow[0] = txtBox.Text;
            dtResult.Rows.Add(drRow);
        }

        DataRow drEmptyRow = dtResult.NewRow();
        drEmptyRow[0] = "";
        dtResult.Rows.Add(drEmptyRow);

        gridViewCEvaluationList.DataSource = dtResult.DefaultView;
        gridViewCEvaluationList.DataKeyNames = new string[] { "Content" };//主键
        gridViewCEvaluationList.DataBind();

        if (dtResult.Rows.Count > 0)
        {
            gridViewCEvaluationList.HeaderRow.Visible = false;
        }
        ResetCreateHoteInfo();
    }

    protected void txtCHotelNM_TextChanged(object sender, EventArgs e)
    {
        string strHotelNM = txtCHotelNM.Text.Trim();
        string strPinyin = PinyinHelper.GetPinyin(strHotelNM);
        string strShortPinyin = PinyinHelper.GetShortPinyin(strHotelNM);
        txtHotelPN.Text = strPinyin;
        txtHotelJP.Text = strShortPinyin;
        ResetCreateHoteInfo();
    }

    protected void txtHotelNM_TextChanged(object sender, EventArgs e)
    {
        string strHotelNM = txtHotelNM.Text.Trim();
        string strPinyin = PinyinHelper.GetPinyin(strHotelNM);
        string strShortPinyin = PinyinHelper.GetShortPinyin(strHotelNM);
        txtUHotelPN.Text = strPinyin;
        txtUHotelJP.Text = strShortPinyin;

        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        if (this.ddpOnline.SelectedValue == "1")
        {
            this.ddpStatusListRemarkNew.Style.Add("display", "none");
        }
        else
        {
            this.ddpStatusListRemarkNew.Style.Add("display", "");
        }
        hidSelectedID.Value = "0";
        RestBalGridData();
    }

    protected void btnCreateHL_Click(object sender, EventArgs e)
    {
        string strHotel = CreateHotel();
        if (String.IsNullOrEmpty(strHotel))
        {
            return;
        }
        string reqUrl = "";
        if (!String.IsNullOrEmpty(Request.QueryString["menu"]))
        {
            reqUrl = "?menu=" + Request.QueryString["menu"].ToString() + "&hid=" + strHotel;
        }
        else
        {
            reqUrl = "?hid=" + strHotel;
        }
        Server.Transfer("~/WebUI/Hotel/HotelInfoManager.aspx" + reqUrl);
    }

    public void BindCHDdpList()
    {
        DataSet dsOnlineStatus = CommonBP.GetConfigList(GetLocalResourceObject("Online").ToString());
        if (dsOnlineStatus.Tables.Count > 0)
        {
            dsOnlineStatus.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
            dsOnlineStatus.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";

            ddpOnline.DataTextField = "ONLINEDIS";
            ddpOnline.DataValueField = "ONLINESTATUS";
            ddpOnline.DataSource = dsOnlineStatus;
            ddpOnline.DataBind();
        }

        DataSet dsStarRating = CommonBP.GetConfigList(GetLocalResourceObject("StarMeth").ToString());
        if (dsStarRating.Tables.Count > 0)
        {
            dsStarRating.Tables[0].Columns["Key"].ColumnName = "STARKEY";
            dsStarRating.Tables[0].Columns["Value"].ColumnName = "STARDIS";

            ddpCStarRating.DataTextField = "STARDIS";
            ddpCStarRating.DataValueField = "STARKEY";
            ddpCStarRating.DataSource = dsStarRating;
            ddpCStarRating.DataBind();

            ddpCStarRating.SelectedValue = "2|0";
        }
    }

    public string CreateHotel()
    {
        ResetCreateHoteInfo();
        MessageContent.InnerHtml = "";
        bool bFlag = true;
        string msgString = "";

        if (String.IsNullOrEmpty(txtCHotelNM.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError3").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtCHotelNM.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("CreateError2").ToString() + "<br/>";
            bFlag = false;
        }

        //酒店下线 必须选择下线原因
        if (this.ddpOnline.SelectedValue == "0")//下线
        {
            if (String.IsNullOrEmpty(this.ddpStatusListRemarkNew.SelectedValue))
            {
                msgString = msgString + GetLocalResourceObject("UpdateErrorDdpStatusListRemark").ToString() + "<br/>";
                bFlag = false;
            }
        }

        if (StringUtility.Text_Length(txtHotelEN.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("CreateError12").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtHotelPN.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError115").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtHotelPN.Text.ToString().Trim()) > 1000)
        {
            msgString = msgString + GetLocalResourceObject("CreateError101").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtTotalRooms.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError102").ToString() + "<br/>";
            bFlag = false;
        }

        if (!ChkNumber(txtTotalRooms.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError103").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtPriceLow.Text.ToString().Trim()) || !ChkNumber(txtPriceLow.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError117").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtHotelJP.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError116").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtHotelJP.Text.ToString().Trim()) > 1000)
        {
            msgString = msgString + GetLocalResourceObject("CreateError104").ToString() + "<br/>";
            bFlag = false;
        }

        string strCity = hidCityID.Value.Trim();

        if (String.IsNullOrEmpty(strCity.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError105").ToString() + "<br/>";
            bFlag = false;
        }
        else if (!strCity.Contains("[") && !strCity.Contains("]"))
        {
            msgString = msgString + GetLocalResourceObject("CreateError118").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtCAddress.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError106").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtCAddress.Text.ToString().Trim()) > 150)
        {
            msgString = msgString + GetLocalResourceObject("CreateError4").ToString() + "<br/>";
            bFlag = false;
        }

        if ((String.IsNullOrEmpty(txtCLatitude.Text.ToString().Trim())) || (String.IsNullOrEmpty(txtCLongitude.Text.ToString().Trim())))
        {
            msgString = msgString + GetLocalResourceObject("CreateError21").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(hidBussList.Value.Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError107").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtContactNameZh.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError108").ToString() + "<br/>";
            bFlag = false;
        }

        if (StringUtility.Text_Length(txtContactNameZh.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("CreateError109").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtHotelFax.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError71").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtHotelFax.Text.ToString().Trim()) && (StringUtility.Text_Length(txtHotelFax.Text.ToString().Trim()) > 100))
        {
            msgString = msgString + GetLocalResourceObject("CreateError7").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtRemark.Text.ToString().Trim()) && (StringUtility.Text_Length(txtRemark.Text.ToString().Trim()) > 2000))
        {
            msgString = msgString + GetLocalResourceObject("CreateError91").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtContactPhone.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError110").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtContactPhone.Text.ToString().Trim()) && (StringUtility.Text_Length(txtContactPhone.Text.ToString().Trim()) > 30))
        {
            msgString = msgString + GetLocalResourceObject("CreateError111").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtHotelTel.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError61").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtHotelTel.Text.ToString().Trim()) && (StringUtility.Text_Length(txtHotelTel.Text.ToString().Trim()) > 40))
        {
            msgString = msgString + GetLocalResourceObject("CreateError6").ToString() + "<br/>";
            bFlag = false;
        }

        //if (String.IsNullOrEmpty(txtCContactEmail.Text.ToString().Trim()))
        //{
        //    msgString = msgString + GetLocalResourceObject("CreateError113").ToString() + "<br/>";
        //    bFlag = false;
        //}

        if (StringUtility.Text_Length(txtCContactEmail.Text.ToString().Trim()) > 100)
        {
            msgString = msgString + GetLocalResourceObject("CreateError11").ToString() + "<br/>";
            bFlag = false;
        }

        if (String.IsNullOrEmpty(txtCSimpleDescZh.Text.ToString().Trim()))
        {
            msgString = msgString + GetLocalResourceObject("CreateError112").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtCSimpleDescZh.Text.ToString().Trim()) && (StringUtility.Text_Length(txtCSimpleDescZh.Text.ToString().Trim()) > 600))
        {
            msgString = msgString + GetLocalResourceObject("CreateError8").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(txtCDescZh.Text.ToString().Trim()) && (StringUtility.Text_Length(txtCDescZh.Text.ToString().Trim()) > 2000))
        {
            msgString = msgString + GetLocalResourceObject("CreateError114").ToString() + "<br/>";
            bFlag = false;
        }

        //if (!RegexValidateData(txtLatitude.Text.ToString().Trim()) || !RegexValidateData(txtLongitude.Text.ToString().Trim()))
        //{
        //    msgString = msgString + GetLocalResourceObject("UpdateError31").ToString() + "<br/>";
        //    bFlag = false;
        //}

        string Evaluation = string.Empty;
        for (int i = 0; i < this.gridViewCEvaluationList.Rows.Count; i++)
        {
            TextBox txtBox = (TextBox)gridViewCEvaluationList.Rows[i].FindControl("txtCEvalist");
            if (!ChkEvaContent(txtBox.Text.Trim()))//if (txtBox.Text.Trim().Contains(",") || txtBox.Text.Trim().Contains("，"))
            {
                msgString = msgString + GetLocalResourceObject("CreateError32").ToString() + "<br/>";
                bFlag = false;
                break;
            }
            Evaluation = (!string.IsNullOrEmpty(txtBox.Text.Trim())) ? Evaluation + txtBox.Text.Trim() + "," : Evaluation;
        }

        Evaluation = (Evaluation.Length > 0) ? Evaluation.Substring(0, Evaluation.Length - 1) : Evaluation;

        if (!String.IsNullOrEmpty(Evaluation) && (StringUtility.Text_Length(Evaluation) > 1000))
        {
            msgString = msgString + GetLocalResourceObject("CreateError9").ToString() + "<br/>";
            bFlag = false;
        }

        if (!String.IsNullOrEmpty(hidCKeyWords.Value) && (StringUtility.Text_Length(hidCKeyWords.Value.Trim().TrimEnd(',')) > 300))
        {
            msgString = msgString + GetLocalResourceObject("CreateError10").ToString() + "<br/>";
            bFlag = false;
        }

        if (!bFlag)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("CreateError1").ToString() + "<br/>" + msgString;
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle()", true);
            return "";
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelGroup = (hidHotelGroupNew.Value.IndexOf("]") >= 0) ? hidHotelGroupNew.Value.Substring((hidHotelGroupNew.Value.IndexOf('[') + 1), (hidHotelGroupNew.Value.IndexOf(']') - 1)) : "";//酒店集团

        hotelInfoDBEntity.Name_CN = txtCHotelNM.Text.Trim();
        hotelInfoDBEntity.Name_EN = txtHotelEN.Text.Trim();
        hotelInfoDBEntity.City = (strCity.IndexOf("]") >= 0) ? strCity.Substring((strCity.IndexOf('[') + 1), (strCity.IndexOf(']') - 1)) : "";
        //hotelInfoDBEntity.Administration = ddpAdministration.SelectedValue.Trim();
        hotelInfoDBEntity.StarRating = ddpCStarRating.SelectedValue;
        hotelInfoDBEntity.AddRess = txtCAddress.Text.Trim();
        hotelInfoDBEntity.Phone = txtHotelTel.Text.Trim();
        hotelInfoDBEntity.Fax = txtHotelFax.Text.Trim();
        hotelInfoDBEntity.Longitude = txtCLongitude.Text.Trim();
        hotelInfoDBEntity.Latitude = txtCLatitude.Text.Trim();

        hotelInfoDBEntity.BDLongitude = txtCBDLongitude.Text.Trim();
        hotelInfoDBEntity.BDLatitude = txtCBDLatitude.Text.Trim();

        //hotelInfoDBEntity.HotelGroup = ddpHotelGroup.SelectedValue;
        hotelInfoDBEntity.Bussiness = hidBussList.Value.Trim();

        hotelInfoDBEntity.OpenDate = dpCOpenDate.Value;
        hotelInfoDBEntity.RepairDate = dpCRepairDate.Value;
        hotelInfoDBEntity.SimpleDescZh = txtCSimpleDescZh.Text.Trim();
        hotelInfoDBEntity.DescZh = txtCDescZh.Text.Trim();
        //hotelInfoDBEntity.HServe = txtHServe.Text.Trim();
        //hotelInfoDBEntity.HFacility = txtHFacility.Text.Trim();

        hotelInfoDBEntity.Status = ddpOnline.SelectedValue.Trim();
        if (ddpOnline.SelectedValue == "0")
        {
            hotelInfoDBEntity.Remark = ddpStatusListRemarkNew.SelectedValue;
        }
        hotelInfoDBEntity.HotelPN = txtHotelPN.Text.Trim();

        hotelInfoDBEntity.TotalRooms = txtTotalRooms.Text.Trim();
        hotelInfoDBEntity.HotelJP = txtHotelJP.Text.Trim();
        hotelInfoDBEntity.Zip = txtZip.Text.Trim();
        hotelInfoDBEntity.PriceLow = txtPriceLow.Text.Trim();
        hotelInfoDBEntity.ContactPer = txtContactNameZh.Text.Trim();
        hotelInfoDBEntity.ContactPhone = txtContactPhone.Text.Trim();
        hotelInfoDBEntity.ContactEmail = txtCContactEmail.Text.Trim();
        hotelInfoDBEntity.WebSite = txtCWebSite.Text.Trim();
        hotelInfoDBEntity.Evaluation = Evaluation;
        hotelInfoDBEntity.HotelRemark = txtRemark.Text.ToString().Trim();
        hotelInfoDBEntity.KeyWords = hidCKeyWords.Value.Trim().TrimEnd(',');
        //hotelInfoDBEntity.ID = hidHotelNo.Value;
        //hotelInfoDBEntity.HotelID = hidHotelID.Value;
        //hotelInfoDBEntity.Status = ddpStatusList.SelectedValue;
        //hotelInfoDBEntity.Phone = txtPhone.Text.Trim();
        //hotelInfoDBEntity.DiamondRating = ddpDiamondRating.SelectedValue;
        //hotelInfoDBEntity.City = ddpCity.SelectedValue;
        //hotelInfoDBEntity.ContactPer = txtContactPer.Text.Trim();
        //hotelInfoDBEntity.ContactEmail = txtContactEmail.Text.Trim();
        //hotelInfoDBEntity.Evaluation = Evaluation;
        //hotelInfoDBEntity.AutoTrust = (chkAutoTrust.Checked) ? "1" : "0";
        //hotelInfoDBEntity.FogStatus = hidFogStatus.Value;

        hotelInfoDBEntity.IsMyHotel = ddlIsMyHotel.SelectedValue;//是否为自签酒店

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        _hotelinfoEntity = HotelInfoBP.CreateHotelInfo(_hotelinfoEntity);
        int iResult = _hotelinfoEntity.Result;
        string HotelID = _hotelinfoEntity.ErrorMSG;
        _commonEntity.LogMessages = _hotelinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "新建酒店基础信息-保存";
        commonDBEntity.Event_ID = HotelID;
        string conTent = GetLocalResourceObject("EventCreateMessage").ToString();

        conTent = string.Format(conTent, HotelID, hotelInfoDBEntity.Name_CN, hotelInfoDBEntity.Name_EN, hotelInfoDBEntity.City, hotelInfoDBEntity.StarRating, hotelInfoDBEntity.AddRess, hotelInfoDBEntity.Phone, hotelInfoDBEntity.Fax, hotelInfoDBEntity.Longitude, hotelInfoDBEntity.Latitude, hotelInfoDBEntity.Bussiness, hotelInfoDBEntity.OpenDate, hotelInfoDBEntity.RepairDate, hotelInfoDBEntity.SimpleDescZh, hotelInfoDBEntity.DescZh, hotelInfoDBEntity.Status, hotelInfoDBEntity.HotelPN, hotelInfoDBEntity.TotalRooms, hotelInfoDBEntity.HotelJP, hotelInfoDBEntity.Zip, hotelInfoDBEntity.PriceLow, hotelInfoDBEntity.ContactPer, hotelInfoDBEntity.ContactPhone, hotelInfoDBEntity.ContactEmail, hotelInfoDBEntity.WebSite, hotelInfoDBEntity.Evaluation, hotelInfoDBEntity.HotelRemark, hotelInfoDBEntity.BDLongitude, hotelInfoDBEntity.BDLatitude, hotelInfoDBEntity.KeyWords);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = string.Format(GetLocalResourceObject("CreateSuccess").ToString(), HotelID);
            MessageContent.InnerHtml = string.Format(GetLocalResourceObject("CreateSuccess").ToString(), HotelID);
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("CreateError33").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("CreateError33").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("CreateError22").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("CreateError22").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("CreateError1").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("CreateError1").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        if (iResult == 1)
        {
            return HotelID;
        }
        else
        {
            return "";
        }
    }

    private void ResetCreateHoteInfo()
    {
        if (this.ddpOnline.SelectedValue == "1")
        {
            this.ddpStatusListRemarkNew.Style.Add("display", "none");
        }
        else
        {
            this.ddpStatusListRemarkNew.Style.Add("display", "");
        }
        dvCRHotel.Style.Add("display", "");
        dvUDHotel.Style.Add("display", "none");
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ReSetCreateVal()", true);
    }

    [WebMethod]
    public static string SetBDlonglatTude(string Longitude, string Latitude)
    {
        HotelInfoEntity hotelinfoEntity = new HotelInfoEntity();
        hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.Longitude = Longitude;
        hotelInfoDBEntity.Latitude = Latitude;
        hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        return HotelInfoBP.SetBDlonglatTude(hotelinfoEntity);
    }

    #endregion

    #region 酒店执行信息
    public void SaveHotelExecuteInfo()
    {
        MessageContent.InnerHtml = "";
        if (!String.IsNullOrEmpty(hidHotelID.Value))
        {
            string strHotel = hidSaveHotelID.Value;
            MessageContent.InnerHtml = "";
            if (!strHotel.Contains("[") || !strHotel.Contains("]") || String.IsNullOrEmpty(strHotel))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
                return;
            }
            #region
            if (string.IsNullOrEmpty(this.txtQueryRoomLinkMan.Text))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = "房控查询联系方式-查房联系人不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(this.txtQueryRoomLinkTel.Text))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = "房控查询联系方式-查房联系电话不能为空！";
                return;
            }
            #endregion

            #region
            if (string.IsNullOrEmpty(this.txtOrderAffirmDayLinkMan.Text))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = "订单确认方式-日间联系人不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(this.txtOrderAffirmDayLinkTel.Text))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = "订单确认方式-日间联系电话不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(this.txtOrderAffirmNightLinkMan.Text))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = "订单确认方式-夜间联系人不能为空！";
                return;
            }
            if (string.IsNullOrEmpty(this.txtOrderAffirmNightLinkTel.Text))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = "订单确认方式-夜间联系电话不能为空！";
                return;
            }
            #endregion

            #region
            if (string.IsNullOrEmpty(this.rdOrderVerifyLinkMan.Text))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = "订单审核方式-审核联系人不能为空！";
                return;
            }
            if (rdOrderVerifyTypeFax.Checked)
            {
                if (string.IsNullOrEmpty(this.rdOrderVerifyLinkFax.Text))
                {
                    ReSetControlVal();
                    MessageContent.InnerHtml = "订单审核方式-审核联系传真不能为空！";
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.rdOrderVerifyLinkTel.Text))
                {
                    ReSetControlVal();
                    MessageContent.InnerHtml = "订单审核方式-审核联系电话不能为空！";
                    return;
                }
            }
            #endregion

            if (!chkDaveValVaild())
            {
                ReSetControlVal();
                MessageContent.InnerHtml = GetLocalResourceObject("UpdateError911").ToString();
                return;
            }

            try
            {
                DataTable dtResultNum = GetHotelExNum();

                HotelInfoEXEntity _hotelinfoEXEntity = new HotelInfoEXEntity();

                _hotelinfoEXEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                _hotelinfoEXEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                _hotelinfoEXEntity.LogMessages.Username = UserSession.Current.UserDspName;
                _hotelinfoEXEntity.LogMessages.IpAddress = UserSession.Current.UserIP;


                _hotelinfoEXEntity.HotelInfoEXDBEntity = new List<HotelInfoEXDBEntity>();
                HotelInfoEXDBEntity hotelInfoEXDBEntity = new HotelInfoEXDBEntity();

                string hotelId = hidHotelID.Value;//酒店ID

                #region  房控查询联系方式
                string QueryRoomRate = "";//查房频率   1每天:2两天一次:3永不询房
                if (this.rdEveryDay.Checked)
                {
                    QueryRoomRate = "1";
                }
                else if (this.rdTwoDay.Checked)
                {
                    QueryRoomRate = "2";
                }
                else
                {
                    QueryRoomRate = "3";
                }
                string OnLine = this.rdOnLine14.Checked == true ? "111110000000001111111111" : "111110000000000000111111";//上线时间    14点   18点  

                StringBuilder QueryRoomLinkMan = new StringBuilder();
                for (int i = 0; i <= 23; i++)
                {
                    QueryRoomLinkMan.Append(this.txtQueryRoomLinkMan.Text + "|"); //查房联系人   this.txtQueryRoomLinkMan.Text;
                }

                StringBuilder QueryRoomLinkTel = new StringBuilder();
                for (int i = 0; i <= 23; i++)
                {
                    QueryRoomLinkTel.Append(this.txtQueryRoomLinkTel.Text + "|");//查房联系电话  this.txtQueryRoomLinkTel.Text;
                }

                StringBuilder QueryRoomLinkFax = new StringBuilder();
                for (int i = 0; i <= 23; i++)
                {
                    QueryRoomLinkFax.Append(this.txtQueryRoomLinkFax.Text + "|");//查房联系传真  this.txtQueryRoomLinkFax.Text;
                }

                string QueryRoomRemark = this.txtQueryRoomRemark.Value;//查房备注

                hotelInfoEXDBEntity.HotelID = hotelId;
                hotelInfoEXDBEntity.Type = "1";
                //hotelInfoEXDBEntity.LinkMan = QueryRoomLinkMan.ToString().TrimEnd('|');
                //hotelInfoEXDBEntity.LinkTel = QueryRoomLinkTel.ToString().TrimEnd('|');
                //hotelInfoEXDBEntity.LinkFax = QueryRoomLinkFax.ToString().TrimEnd('|');
                hotelInfoEXDBEntity.LinkMan = (QueryRoomLinkMan.ToString().Length > 0) ? QueryRoomLinkMan.ToString().Substring(0, QueryRoomLinkMan.Length - 1) : QueryRoomLinkMan.ToString();
                hotelInfoEXDBEntity.LinkTel = (QueryRoomLinkTel.ToString().Length > 0) ? QueryRoomLinkTel.ToString().Substring(0, QueryRoomLinkTel.Length - 1) : QueryRoomLinkTel.ToString();
                hotelInfoEXDBEntity.LinkFax = (QueryRoomLinkFax.ToString().Length > 0) ? QueryRoomLinkFax.ToString().Substring(0, QueryRoomLinkFax.Length - 1) : QueryRoomLinkFax.ToString();

                hotelInfoEXDBEntity.Remark = QueryRoomRemark;
                hotelInfoEXDBEntity.ExTime = OnLine;
                hotelInfoEXDBEntity.ExMode = QueryRoomRate;
                hotelInfoEXDBEntity.Status = "1";
                hotelInfoEXDBEntity.CreateUser = UserSession.Current.UserDspName;
                hotelInfoEXDBEntity.UpdateUser = UserSession.Current.UserDspName;
                _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);

                DataRow[] dtQueryRoom = dtResultNum.Select("type='" + 1 + "'");
                if (dtQueryRoom.Length > 0)
                {
                    HotelInfoEXBP.UpdateHotelInfoEX(_hotelinfoEXEntity);
                }
                else
                {
                    HotelInfoEXBP.InsertHotelInfoEX(_hotelinfoEXEntity);
                }
                //int result1 = HotelInfoEXBP.InsertHotelInfoEX(_hotelinfoEXEntity);
                //int result1 = HotelInfoEXBP.UpdateHotelInfoEX(_hotelinfoEXEntity);

                HotelInfoEXBP.InsertHotelEXHistory(_hotelinfoEXEntity);
                _hotelinfoEXEntity.HotelInfoEXDBEntity.Clear();
                #endregion

                #region 订单确认方式
                //日间9点-18点   夜间  19点-8点
                StringBuilder OrderAffirmLinkFax = new StringBuilder();
                string OrderAffirmDayLinkFax = this.txtOrderAffirmDayLinkFax.Text;//日间传真
                string OrderAffirmNightLinkFax = this.txtOrderAffirmNightLinkFax.Text;//夜间传真
                for (int i = 0; i <= 23; i++)
                {
                    if (i >= 9 && i <= 18)
                    {
                        OrderAffirmLinkFax.Append(this.txtOrderAffirmDayLinkFax.Text + "|");
                    }
                    else
                    {
                        OrderAffirmLinkFax.Append(this.txtOrderAffirmNightLinkFax.Text + "|");
                    }
                }

                StringBuilder OrderAffirmLinkMan = new StringBuilder();
                string OrderAffirmDayLinkMan = this.txtOrderAffirmDayLinkMan.Text;//日间联系人
                string OrderAffirmNightLinkMan = this.txtOrderAffirmNightLinkMan.Text;//夜间联系人
                for (int i = 0; i <= 23; i++)
                {
                    if (i >= 9 && i <= 18)
                    {
                        OrderAffirmLinkMan.Append(this.txtOrderAffirmDayLinkMan.Text + "|");
                    }
                    else
                    {
                        OrderAffirmLinkMan.Append(this.txtOrderAffirmNightLinkMan.Text + "|");
                    }
                }

                StringBuilder OrderAffirmLinkTel = new StringBuilder();
                string OrderAffirmDayLinkTel = this.txtOrderAffirmDayLinkTel.Text;//日间联系电话
                string OrderAffirmNightLinkTel = this.txtOrderAffirmNightLinkTel.Text;//夜间联系电话
                for (int i = 0; i <= 23; i++)
                {
                    if (i >= 9 && i <= 18)
                    {
                        OrderAffirmLinkTel.Append(this.txtOrderAffirmDayLinkTel.Text + "|");
                    }
                    else
                    {
                        OrderAffirmLinkTel.Append(this.txtOrderAffirmNightLinkTel.Text + "|");
                    }
                }
                string OrderAffirmRemark = this.txtOrderAffirmRemark.Value;//订单确认备注

                hotelInfoEXDBEntity.HotelID = hotelId;
                hotelInfoEXDBEntity.Type = "2";
                //hotelInfoEXDBEntity.LinkMan = OrderAffirmLinkMan.ToString().TrimEnd('|');
                //hotelInfoEXDBEntity.LinkTel = OrderAffirmLinkTel.ToString().TrimEnd('|');
                //hotelInfoEXDBEntity.LinkFax = OrderAffirmLinkFax.ToString().TrimEnd('|');

                hotelInfoEXDBEntity.LinkMan = (OrderAffirmLinkMan.ToString().Length > 0) ? OrderAffirmLinkMan.ToString().Substring(0, OrderAffirmLinkMan.Length - 1) : OrderAffirmLinkMan.ToString();
                hotelInfoEXDBEntity.LinkTel = (OrderAffirmLinkTel.ToString().Length > 0) ? OrderAffirmLinkTel.ToString().Substring(0, OrderAffirmLinkTel.Length - 1) : OrderAffirmLinkTel.ToString();
                hotelInfoEXDBEntity.LinkFax = (OrderAffirmLinkFax.ToString().Length > 0) ? OrderAffirmLinkFax.ToString().Substring(0, OrderAffirmLinkFax.Length - 1) : OrderAffirmLinkFax.ToString();


                hotelInfoEXDBEntity.Remark = QueryRoomRemark;
                hotelInfoEXDBEntity.ExTime = "111111111111111111111111";
                hotelInfoEXDBEntity.ExMode = "";
                hotelInfoEXDBEntity.Status = "1";
                hotelInfoEXDBEntity.CreateUser = UserSession.Current.UserDspName;
                hotelInfoEXDBEntity.UpdateUser = UserSession.Current.UserDspName;
                _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);
                DataRow[] dtOrderAffirm = dtResultNum.Select("type='" + 2 + "'");
                if (dtOrderAffirm.Length > 0)
                {
                    HotelInfoEXBP.UpdateHotelInfoEX(_hotelinfoEXEntity);
                }
                else
                {
                    HotelInfoEXBP.InsertHotelInfoEX(_hotelinfoEXEntity);
                }

                //int result2 = HotelInfoEXBP.InsertHotelInfoEX(_hotelinfoEXEntity);
                //int result2 = HotelInfoEXBP.UpdateHotelInfoEX(_hotelinfoEXEntity);

                HotelInfoEXBP.InsertHotelEXHistory(_hotelinfoEXEntity);
                _hotelinfoEXEntity.HotelInfoEXDBEntity.Clear();
                #endregion

                #region  订单审核方式
                //日间9点-18点   夜间  19点-8点
                string OrderVerifyTime = this.rdOrderVerifyTimeDay.Checked == true ? "000000000111111111100000" : "111111111000000000011111";//日/夜审
                string OrderVerifyType = this.rdOrderVerifyTypeFax.Checked == true ? "1" : "2";//审核方式

                string rdOrderVerifyLinkMan = this.rdOrderVerifyLinkMan.Text;//审核联系人
                string rdOrderVerifyLinkTel = this.rdOrderVerifyLinkTel.Text;//审核联系电话
                string rdOrderVerifyLinkFax = this.rdOrderVerifyLinkFax.Text;//审核联系传真

                string rdOrderVerifyRemark = this.rdOrderVerifyRemark.Value;//审核备注

                hotelInfoEXDBEntity.HotelID = hotelId;
                hotelInfoEXDBEntity.Type = "3";
                StringBuilder OrderVerifyLinkMan = new StringBuilder();
                for (int i = 0; i < OrderVerifyTime.Length; i++)
                {
                    if (OrderVerifyTime[i].ToString() == "1")
                    {
                        OrderVerifyLinkMan.Append(this.rdOrderVerifyLinkMan.Text + "|");
                    }
                    else
                    {
                        OrderVerifyLinkMan.Append("|");
                    }
                }

                StringBuilder OrderVerifyLinkTel = new StringBuilder();
                for (int i = 0; i < OrderVerifyTime.Length; i++)
                {
                    if (OrderVerifyTime[i].ToString() == "1")
                    {
                        OrderVerifyLinkTel.Append(this.rdOrderVerifyLinkTel.Text + "|");
                    }
                    else
                    {
                        OrderVerifyLinkTel.Append("|");
                    }
                }

                StringBuilder OrderVerifyLinkFax = new StringBuilder();
                for (int i = 0; i < OrderVerifyTime.Length; i++)
                {
                    if (OrderVerifyTime[i].ToString() == "1")
                    {
                        OrderVerifyLinkFax.Append(this.rdOrderVerifyLinkFax.Text + "|");
                    }
                    else
                    {
                        OrderVerifyLinkFax.Append("|");
                    }
                }

                //hotelInfoEXDBEntity.LinkMan = OrderVerifyLinkMan.ToString().TrimEnd('|');
                //hotelInfoEXDBEntity.LinkTel = OrderVerifyLinkTel.ToString().TrimEnd('|');
                //hotelInfoEXDBEntity.LinkFax = OrderVerifyLinkFax.ToString().TrimEnd('|');

                hotelInfoEXDBEntity.LinkMan = (OrderVerifyLinkMan.ToString().Length > 0) ? OrderVerifyLinkMan.ToString().Substring(0, OrderVerifyLinkMan.Length - 1) : OrderVerifyLinkMan.ToString();
                hotelInfoEXDBEntity.LinkTel = (OrderVerifyLinkTel.ToString().Length > 0) ? OrderVerifyLinkTel.ToString().Substring(0, OrderVerifyLinkTel.Length - 1) : OrderVerifyLinkTel.ToString();
                hotelInfoEXDBEntity.LinkFax = (OrderVerifyLinkFax.ToString().Length > 0) ? OrderVerifyLinkFax.ToString().Substring(0, OrderVerifyLinkFax.Length - 1) : OrderVerifyLinkFax.ToString();


                hotelInfoEXDBEntity.Remark = rdOrderVerifyRemark;
                hotelInfoEXDBEntity.ExTime = OrderVerifyTime;
                hotelInfoEXDBEntity.ExMode = OrderVerifyType;
                hotelInfoEXDBEntity.Status = "1";
                hotelInfoEXDBEntity.CreateUser = UserSession.Current.UserDspName;
                hotelInfoEXDBEntity.UpdateUser = UserSession.Current.UserDspName;
                _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);
                DataRow[] dtOrderVerify = dtResultNum.Select("type='" + 3 + "'");
                if (dtOrderVerify.Length > 0)
                {
                    HotelInfoEXBP.UpdateHotelInfoEX(_hotelinfoEXEntity);
                }
                else
                {
                    HotelInfoEXBP.InsertHotelInfoEX(_hotelinfoEXEntity);
                }
                //int result3 = HotelInfoEXBP.InsertHotelInfoEX(_hotelinfoEXEntity);
                //int result3 = HotelInfoEXBP.UpdateHotelInfoEX(_hotelinfoEXEntity);

                HotelInfoEXBP.InsertHotelEXHistory(_hotelinfoEXEntity);
                _hotelinfoEXEntity.HotelInfoEXDBEntity.Clear();

                MessageContent.InnerHtml = "酒店执行信息保存成功！";

                #endregion
            }
            catch (Exception ex)
            {
                MessageContent.InnerHtml = "酒店执行信息保存失败！";
            }
        }
        else
        {
            SetSelectHotelControl();
        }
    }

    protected void btnAddHotelEX_Click(object sender, EventArgs e)
    {
        SaveHotelExecuteInfo();
        RestControlEnable(true, 2);
        dvEvlGrid.Style.Add("display", "");
        dvLink.Style.Add("display", "");
        SetEvalGridBtnStyle(true);
        divBtnList.Style.Add("display", "");
        dvSales.Style.Add("display", "");
        dvBalSearch.Style.Add("display", "");
        dvBalAdd.Style.Add("display", "");
        dvHotelEX.Style.Add("display", "");
        dvbtnRoom.Style.Add("display", "");
        dvbtnPrRoom.Style.Add("display", "");
        dvSaveRoom.Style.Add("display", "none");
        dvRoomGrid.Style.Add("display", "");
        dvRoomCD.Style.Add("display", "");
        dvPrRoom.Style.Add("display", "none");
        dvPrRoomGrid.Style.Add("display", "");
        hidSelectedID.Value = "6";
        RestBalGridData();
    }

    public void BindHotelEXInfo()
    {
        if (!String.IsNullOrEmpty(hidHotelID.Value))
        {
            string strHotel = hidSaveHotelID.Value;
            MessageContent.InnerHtml = "";
            if (!strHotel.Contains("[") || !strHotel.Contains("]") || String.IsNullOrEmpty(strHotel))
            {
                ReSetControlVal();
                MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
                return;
            }
            HotelInfoEXEntity _hotelinfoEXEntity = new HotelInfoEXEntity();

            _hotelinfoEXEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _hotelinfoEXEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _hotelinfoEXEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _hotelinfoEXEntity.LogMessages.IpAddress = UserSession.Current.UserIP;


            _hotelinfoEXEntity.HotelInfoEXDBEntity = new List<HotelInfoEXDBEntity>();
            HotelInfoEXDBEntity hotelInfoEXDBEntity = new HotelInfoEXDBEntity();

            hotelInfoEXDBEntity.HotelID = hidHotelID.Value;//酒店ID
            _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);
            DataSet dsResult = HotelInfoEXBP.SelectHotelInfoEX(_hotelinfoEXEntity).QueryResult;

            #region  房控查询联系方式
            DataRow[] dtQueryRoom = dsResult.Tables[0].Select("type='" + 1 + "'");
            if (dtQueryRoom.Length > 0)
            {
                //查房频率
                if (dtQueryRoom[0]["Ex_Mode"].ToString() == "1") { this.rdEveryDay.Checked = true; this.rdTwoDay.Checked = false; this.rdEver.Checked = false; }
                else if (dtQueryRoom[0]["Ex_Mode"].ToString() == "2") { this.rdEveryDay.Checked = false; this.rdTwoDay.Checked = true; this.rdEver.Checked = false; }
                else { this.rdEveryDay.Checked = false; this.rdTwoDay.Checked = false; this.rdEver.Checked = true; }
                //上线时间                                   111110000000000000111111
                if (dtQueryRoom[0]["Ex_Time"].ToString() == "111110000000001111111111") { this.rdOnLine14.Checked = true; this.rdOnLine18.Checked = false; }
                else { this.rdOnLine14.Checked = false; this.rdOnLine18.Checked = true; }
                //查询联系人
                this.txtQueryRoomLinkMan.Text = dtQueryRoom[0]["LINKMAN"].ToString() == "" ? "" : dtQueryRoom[0]["LINKMAN"].ToString().Split('|')[0].ToString();
                //查房联系电话
                this.txtQueryRoomLinkTel.Text = dtQueryRoom[0]["LINKTEL"].ToString() == "" ? "" : dtQueryRoom[0]["LINKTEL"].ToString().Split('|')[0].ToString();
                //查房传真
                this.txtQueryRoomLinkFax.Text = dtQueryRoom[0]["LINKFAX"].ToString() == "" ? "" : dtQueryRoom[0]["LINKFAX"].ToString().Split('|')[0].ToString();
                //查房备注
                this.txtQueryRoomRemark.InnerHtml = dtQueryRoom[0]["REMARK"].ToString();
            }
            #endregion

            #region  订单确认方式
            DataRow[] dtOrderAffirm = dsResult.Tables[0].Select("type='" + 2 + "'");
            if (dtOrderAffirm.Length > 0)
            {
                //日间传真
                this.txtOrderAffirmDayLinkFax.Text = dtOrderAffirm[0]["LINKFAX"].ToString() == "" ? "" : dtOrderAffirm[0]["LINKFAX"].ToString().Split('|')[9].ToString();
                //日间联系人
                this.txtOrderAffirmDayLinkMan.Text = dtOrderAffirm[0]["LINKMAN"].ToString() == "" ? "" : dtOrderAffirm[0]["LINKMAN"].ToString().Split('|')[9].ToString();
                //日间联系电话
                this.txtOrderAffirmDayLinkTel.Text = dtOrderAffirm[0]["LINKTEL"].ToString() == "" ? "" : dtOrderAffirm[0]["LINKTEL"].ToString().Split('|')[9].ToString();

                //夜间传真
                this.txtOrderAffirmNightLinkFax.Text = dtOrderAffirm[0]["LINKFAX"].ToString() == "" ? "" : dtOrderAffirm[0]["LINKFAX"].ToString().Split('|')[19].ToString();
                //夜间联系人
                this.txtOrderAffirmNightLinkMan.Text = dtOrderAffirm[0]["LINKMAN"].ToString() == "" ? "" : dtOrderAffirm[0]["LINKMAN"].ToString().Split('|')[19].ToString();
                //夜间联系电话
                this.txtOrderAffirmNightLinkTel.Text = dtOrderAffirm[0]["LINKTEL"].ToString() == "" ? "" : dtOrderAffirm[0]["LINKTEL"].ToString().Split('|')[19].ToString();

                //订单确认备注
                this.txtOrderAffirmRemark.InnerHtml = dtOrderAffirm[0]["REMARK"].ToString();
            }
            #endregion

            #region  订单审核方式
            DataRow[] dtOrderVerify = dsResult.Tables[0].Select("type='" + 3 + "'");
            if (dtOrderVerify.Length > 0)
            {
                //日/夜审
                if (dtOrderVerify[0]["Ex_Mode"].ToString() == "1") { this.rdOrderVerifyTypeFax.Checked = true; this.rdOrderVerifyTypeTel.Checked = false; }
                else { this.rdOrderVerifyTypeFax.Checked = false; this.rdOrderVerifyTypeTel.Checked = true; }
                //审核方式
                if (dtOrderVerify[0]["Ex_Time"].ToString() == "000000000111111111100000") { this.rdOrderVerifyTimeDay.Checked = true; this.rdOrderVerifyTimeNight.Checked = false; }
                else { this.rdOrderVerifyTimeDay.Checked = false; this.rdOrderVerifyTimeNight.Checked = true; }

                if (dtOrderVerify[0]["Ex_Time"].ToString() == "000000000111111111100000")
                {
                    //审核联系人
                    this.rdOrderVerifyLinkMan.Text = dtOrderVerify[0]["LINKMAN"].ToString() == "" ? "" : dtOrderVerify[0]["LINKMAN"].ToString().Split('|')[10].ToString();
                    //审核联系电话
                    this.rdOrderVerifyLinkTel.Text = dtOrderVerify[0]["LINKTEL"].ToString() == "" ? "" : dtOrderVerify[0]["LINKTEL"].ToString().Split('|')[10].ToString();
                    //审核联系传真
                    this.rdOrderVerifyLinkFax.Text = dtOrderVerify[0]["LINKFAX"].ToString() == "" ? "" : dtOrderVerify[0]["LINKFAX"].ToString().Split('|')[10].ToString();
                    //日间9点-18点   夜间  19点-8点
                    //string OrderVerifyTime = this.rdOrderVerifyTimeDay.Checked == true ? "000000000111111111100000" : "111111111000000000011111";//日/夜审
                }
                else
                {
                    //审核联系人
                    this.rdOrderVerifyLinkMan.Text = dtOrderVerify[0]["LINKMAN"].ToString() == "" ? "" : dtOrderVerify[0]["LINKMAN"].ToString().Split('|')[0].ToString();
                    //审核联系电话
                    this.rdOrderVerifyLinkTel.Text = dtOrderVerify[0]["LINKTEL"].ToString() == "" ? "" : dtOrderVerify[0]["LINKTEL"].ToString().Split('|')[0].ToString();
                    //审核联系传真
                    this.rdOrderVerifyLinkFax.Text = dtOrderVerify[0]["LINKFAX"].ToString() == "" ? "" : dtOrderVerify[0]["LINKFAX"].ToString().Split('|')[0].ToString();
                }
                //审核备注
                this.rdOrderVerifyRemark.InnerHtml = dtOrderVerify[0]["REMARK"].ToString();
            }
            #endregion
        }
    }

    public DataTable GetHotelExNum()
    {
        HotelInfoEXEntity _hotelinfoEXEntity = new HotelInfoEXEntity();

        _hotelinfoEXEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEXEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEXEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEXEntity.LogMessages.IpAddress = UserSession.Current.UserIP;


        _hotelinfoEXEntity.HotelInfoEXDBEntity = new List<HotelInfoEXDBEntity>();
        HotelInfoEXDBEntity hotelInfoEXDBEntity = new HotelInfoEXDBEntity();

        hotelInfoEXDBEntity.HotelID = hidHotelID.Value;//酒店ID
        _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);
        DataSet dsResult = HotelInfoEXBP.SelectHotelInfoEX(_hotelinfoEXEntity).QueryResult;

        return dsResult.Tables[0];
    }

    public void checkedRadio()
    {
        this.rdEveryDay.Checked = true;
        this.rdOnLine18.Checked = true;
        this.rdOrderVerifyTimeDay.Checked = true;
        this.rdOrderVerifyTypeFax.Checked = true;
    }
    #endregion

    /// <summary>
    /// 酒店下线原因
    /// </summary>
    public void BindDtStatusListRemark()
    {
        DataTable dtStatusListRemark = new DataTable();
        dtStatusListRemark.Columns.Add("StatusListRemarkID");
        dtStatusListRemark.Columns.Add("StatusListRemarkName");
        for (int i = 0; i < 8; i++)
        {
            DataRow row = dtStatusListRemark.NewRow();
            switch (i.ToString())
            {
                case "0":
                    row["StatusListRemarkID"] = "";
                    row["StatusListRemarkName"] = "请选择下线原因";
                    break;
                case "1":
                    row["StatusListRemarkID"] = "1";
                    row["StatusListRemarkName"] = "酒店停业\\装修";
                    break;
                case "2":
                    row["StatusListRemarkID"] = "2";
                    row["StatusListRemarkName"] = "合同到期不续签";
                    break;
                case "3":
                    row["StatusListRemarkID"] = "3";
                    row["StatusListRemarkName"] = "合同到期续签审批中";
                    break;
                case "4":
                    row["StatusListRemarkID"] = "4";
                    row["StatusListRemarkName"] = "酒店转业";
                    break;
                case "5":
                    row["StatusListRemarkID"] = "5";
                    row["StatusListRemarkName"] = "阶段性满房";
                    break;
                case "6":
                    row["StatusListRemarkID"] = "6";
                    row["StatusListRemarkName"] = "酒店更换业主";
                    break;
                default:
                    row["StatusListRemarkID"] = "7";
                    row["StatusListRemarkName"] = "合作问题";
                    break;
            }
            dtStatusListRemark.Rows.Add(row);
        }
        ddpStatusListRemark.DataTextField = "StatusListRemarkName";
        ddpStatusListRemark.DataValueField = "StatusListRemarkID";
        ddpStatusListRemark.DataSource = dtStatusListRemark;
        ddpStatusListRemark.DataBind();


        ddpStatusListRemarkNew.DataTextField = "StatusListRemarkName";
        ddpStatusListRemarkNew.DataValueField = "StatusListRemarkID";
        ddpStatusListRemarkNew.DataSource = dtStatusListRemark;
        ddpStatusListRemarkNew.DataBind();
    }
}
