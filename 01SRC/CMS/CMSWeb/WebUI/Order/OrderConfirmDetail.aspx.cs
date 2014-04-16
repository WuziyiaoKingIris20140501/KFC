using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Net;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Process.SendFaxByWebTurnPicture;
using HotelVp.CMS.Domain.Entity;
using HotelVp.Common.Utilities;

public partial class OrderConfirmDetail : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["baseDtResult"] = null;
            ViewState["detailDtResult"] = null;
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                hidOrderID.Value = Request.QueryString["ID"].ToString().Trim();
                txtOrderID.Text = hidOrderID.Value;
            }

            detailMessageContent.InnerHtml = "";
            if (String.IsNullOrEmpty(hidOrderID.Value.Trim()))
            {
                hidOrderID.Value = "";
                btnPrint.Visible = false;
                btnSendFax.Visible = false;
                btnSet.Visible = false;
                btnUnlock.Visible = false;
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                return;
            }
            trCanlReson.Style.Add("display", "none");

            //else if (!String.IsNullOrEmpty(Request.QueryString["FOGID"]))
            //{
            //    EventLMID = GetEventLMOrderID(Request.QueryString["FOGID"].ToString().Trim());

            //    if (String.IsNullOrEmpty(EventLMID.Trim()))
            //    {
            //        detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //        return;
            //    }
            //}
            //else
            //{
            //    detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //}

            //hidEventLMID.Value = EventLMID;
            //BindViewCSSystemLogMain();
            //BindViewCSSystemLogDetail();

            tbDetail.Style.Add("display", "none");
            tbControl.Style.Add("display", "none");
            trCanlReson.Style.Add("display", "none");
            detailMessageContent.InnerHtml = "";
            dvErrorInfo.InnerHtml = "";
            BindViewCSSystemLogMain();
        }
        //messageContent.InnerHtml = "";
    }

    private string GetEventLMOrderID(string orderID)
    {
        if (String.IsNullOrEmpty(orderID.Trim()))
        {
            return "";
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = orderID;

        DataSet dsMainResult = LmSystemLogBP.GetEventLMOrderID(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            return "";
        }

        return dsMainResult.Tables[0].Rows[0][0].ToString();
    }

    private void BindViewCSSystemLogMain()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsMainResult = LmSystemLogBP.OrderOperationSelect(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            hidOrderID.Value = "";
            btnPrint.Visible = false;
            btnSendFax.Visible = false;
            btnSet.Visible = false;
            btnUnlock.Visible = false;
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        string strMsg = LmSystemLogBP.ChkOrderOperationControl(_lmSystemLogEntity).ErrorMSG;
        if (!String.IsNullOrEmpty(strMsg))
        {
            //hidOrderID.Value = "";
            btnPrint.Visible = false;
            btnSendFax.Visible = false;
            btnSet.Visible = false;
            //detailMessageContent.InnerHtml = strMsg;
            dvErrorInfo.InnerHtml = strMsg;
            //return;
        }
        else
        {
            _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单操作-锁定";
            commonDBEntity.Event_ID = hidHotelID.Value;
            string conTent = GetLocalResourceObject("EventLockMessage").ToString();

            conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;

            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateLockSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }
        ViewState["baseDtResult"] = dsMainResult.Tables[0];

        btnPrint.Visible = true;
        btnSendFax.Visible = true;
        btnSet.Visible = true;
        btnUnlock.Visible = true;
        lbCREATE_TIME.Text = dsMainResult.Tables[0].Rows[0]["create_time"].ToString();
        lbORDER_CHANNEL.Text = dsMainResult.Tables[0].Rows[0]["ORDER_CHANNEL"].ToString();
        lbPRICE_CODE.Text = dsMainResult.Tables[0].Rows[0]["price_code_nm"].ToString();
        hidPriceCode.Value = dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper();
        lbBOOK_STATUS.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        lbIS_GUA.Text = dsMainResult.Tables[0].Rows[0]["is_gua_nm"].ToString();
        lbRESV_GUA_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_HOLD_TIME"].ToString();
        lbUSER_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["USER_HOLD_TIME"].ToString();
        lbRESV_GUA_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_DESC"].ToString();
        lbRESV_CXL_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_CXL_DESC"].ToString();
        lbPAY_STATUS.Text = dsMainResult.Tables[0].Rows[0]["pay_status_nm"].ToString();
        lbHOTEL_NAME.Text = dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString() + " - " + dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();
        hidIssueNm.Value = "[" + dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString() + "] - [" + dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString() + "] - " + hidOrderID.Value + " - 订单确认问题";
        lbFax.Text = dsMainResult.Tables[0].Rows[0]["linkfax"].ToString();
        lbLINKTEL.Text = dsMainResult.Tables[0].Rows[0]["linktel"].ToString();
        lbGUEST_NAMES.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();
        lbCONTACT_NAME.Text = dsMainResult.Tables[0].Rows[0]["contact_name"].ToString();
        lbCONTACT_TEL.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();
        lbLOGIN_MOBILE.Text = dsMainResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString();
        lbOrderDays.Text = SetOrderDaysVal(dsMainResult.Tables[0].Rows[0]["in_date"].ToString(), dsMainResult.Tables[0].Rows[0]["out_date"].ToString());
        lbIN_DATE.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString();
        lbOUT_DATE.Text = dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString();
        lbROOM_TYPE_NAME.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString();
        lbBOOK_ROOM_NUM.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString();
        lbARRIVE_TIME.Text = dsMainResult.Tables[0].Rows[0]["ARRIVE_TIME"].ToString();
        lbTICKET_USERCODE.Text = dsMainResult.Tables[0].Rows[0]["ticket_usercode"].ToString();
        lbTICKET_PAGENM.Text = dsMainResult.Tables[0].Rows[0]["packagename"].ToString();
        lbTICKET_AMOUNT.Text = dsMainResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        lbBOOK_REMARK.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();
        lbORDER_NUM.Text = dsMainResult.Tables[0].Rows[0]["order_id"].ToString();
        chkFollowUp.Checked = ("1".Equals(dsMainResult.Tables[0].Rows[0]["FOLLOW_UP"].ToString())) ? true : false;
        hidHotelID.Value = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();
        lbMemo1.Text = SetMemoVal(_lmSystemLogEntity.FogOrderID);
        lblSalesMG.Text = SetHotelSalesInfo(dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString());
        BindViewCSSystemLogDetail();
        SetOrderSettingControlVal(dsMainResult.Tables[0].Rows[0]["price_code"].ToString(), dsMainResult.Tables[0].Rows[0]["book_status_other"].ToString(), dsMainResult.Tables[0].Rows[0]["order_cancle_reason"].ToString());

        tbDetail.Style.Add("display", "");
        tbControl.Style.Add("display", "");

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetRefeshVal()", true);
    }


    private string SetMemoVal(string strKey)
    {
        string strResult = "";
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
    //            strTemp = strTemp.Substring(strTemp.IndexOf("备注：")+3, strTemp.IndexOf("取消原因：") - 1 - strTemp.IndexOf("备注：")-3);
    //            if (!String.IsNullOrEmpty(strTemp.Trim()))
    //            {
    //                strResult = strResult + dsResult.Tables[0].Rows[i]["CREATEDATE"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["USERNAME"].ToString().Trim() + "： " + strTemp + "<br/><br/>";
    //            }
    //        }
    //    }

    //    return strResult;
    //}

    private string SetHotelSalesInfo(string HotelID)
    {
        string strResult = "";
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.HotelID = HotelID;

        DataSet dsResult = LmSystemLogBP.OrderOperationSalesSelect(_lmSystemLogEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            strResult = dsResult.Tables[0].Rows[0]["User_DspName"].ToString().Trim() + " - " + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Trim();
        }
        return strResult;
    }

    private string SetOrderDaysVal(string strInDate, string strOutDate)
    {
        if (String.IsNullOrEmpty(strInDate.Trim()) || String.IsNullOrEmpty(strOutDate.Trim()))
        {
            return "1";
        }

        try
        {
            DateTime dtInDate = Convert.ToDateTime(strInDate);
            DateTime dtOutDate = Convert.ToDateTime(strOutDate);
            TimeSpan tsDays = dtOutDate - dtInDate;
            return tsDays.Days.ToString();
        }
        catch
        {
            return "1";
        }
    }

    private void SetOrderSettingControlVal(string strPriceCode, string strVal, string canelval)
    {
        DataTable dtOrderStatus = GetOrderStatusData(strPriceCode.Trim());

        ddpOrderStatus.DataTextField = "ORDERDIS";
        ddpOrderStatus.DataValueField = "ORDERSTATUS";
        ddpOrderStatus.DataSource = dtOrderStatus;
        ddpOrderStatus.DataBind();


        DataTable dtCanelReson = GetCanelResonData();
        ddpCanelReson.DataTextField = "CanelResonDis";
        ddpCanelReson.DataValueField = "CanelReson";
        ddpCanelReson.DataSource = dtCanelReson;
        ddpCanelReson.DataBind();

        if (!"LMBAR".Equals(strPriceCode.Trim().ToUpper()) && ddpOrderStatus.Items.FindByValue(strVal) != null)
        {
            ddpOrderStatus.SelectedValue = strVal;
        }

        if (!"LMBAR".Equals(strPriceCode.Trim().ToUpper()) && "9".Equals(strVal))
        {
            trCanlReson.Style.Add("display", "");
        }
        else
        {
            trCanlReson.Style.Add("display", "none");
        }

        if (!"LMBAR".Equals(strPriceCode.Trim().ToUpper()) && ddpCanelReson.Items.FindByValue(canelval) != null)
        {
            ddpCanelReson.SelectedValue = canelval;
        }
    }

    private DataTable GetOrderStatusData(string strType)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ORDERSTATUS");
        dt.Columns.Add("ORDERDIS");

        if ("LMBAR".Equals(strType.Trim().ToUpper()))
        {
            DataRow dr = dt.NewRow();
            dr["ORDERSTATUS"] = "4";
            dr["ORDERDIS"] = "取消单";
            dt.Rows.Add(dr);
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = dt.NewRow();

                switch (i.ToString())
                {
                    //case "0":
                    //    dr["ORDERSTATUS"] = "3";
                    //    dr["ORDERDIS"] = "用户取消";
                    //    break;
                    case "0":
                        dr["ORDERSTATUS"] = "4";
                        dr["ORDERDIS"] = "可入住";
                        break;
                    //case "1":
                    //    dr["ORDERSTATUS"] = "5";
                    //    dr["ORDERDIS"] = "NoShow";
                    //    break;
                    case "1":
                        dr["ORDERSTATUS"] = "7";
                        dr["ORDERDIS"] = "入住中";
                        break;
                    //case "3":
                    //    dr["ORDERSTATUS"] = "8";
                    //    dr["ORDERDIS"] = "已离店";
                    //    break;
                    case "2":
                        dr["ORDERSTATUS"] = "9";
                        dr["ORDERDIS"] = "CC取消";
                        break;
                    //default:
                    //    dr["ORDERSTATUS"] = "";
                    //    dr["ORDERDIS"] = "";
                    //    break;
                }
                dt.Rows.Add(dr);
            }
        }


        return dt;
    }

    private DataTable GetCanelResonData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CanelReson");
        dt.Columns.Add("CanelResonDis");

        for (int i = 0; i < 8; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["CanelReson"] = "CRC01";
                    dr["CanelResonDis"] = "满房";
                    break;
                case "1":
                    dr["CanelReson"] = "CRH09";
                    dr["CanelResonDis"] = "酒店无人接听/无法接通";
                    break;
                case "2":
                    dr["CanelReson"] = "CRC06";
                    dr["CanelResonDis"] = "酒店变价";
                    break;
                case "3":
                    dr["CanelReson"] = "CRH10";
                    dr["CanelResonDis"] = "终止合作";
                    break;
                case "4":
                    dr["CanelReson"] = "CRH07";
                    dr["CanelResonDis"] = "无协议";
                    break;
                case "5":
                    dr["CanelReson"] = "CRH11";
                    dr["CanelResonDis"] = "不接外宾";
                    break;
                case "6":
                    dr["CanelReson"] = "CRC02";
                    dr["CanelResonDis"] = "重复订单";
                    break;
                case "7":
                    dr["CanelReson"] = "CRG18";
                    dr["CanelResonDis"] = "用户取消";
                    break;
                case "8":
                    dr["CanelReson"] = "CRG99";
                    dr["CanelResonDis"] = "其他";
                    break;
                default:
                    dr["CanelReson"] = "";
                    dr["CanelResonDis"] = "";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void BindViewCSSystemLogDetail()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;
        _lmSystemLogEntity.PriceCode = hidPriceCode.Value;

        DataSet dsDetailResult = LmSystemLogBP.OrderOperationDetailSelect(_lmSystemLogEntity).QueryResult;

        if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 0)
        {
            ViewState["detailDtResult"] = dsDetailResult.Tables[0];
        }

        //if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 1)
        //{
        //    for (int i = 1; i <= dsDetailResult.Tables[0].Rows.Count -1; i++)
        //    {
        //        if (dsDetailResult.Tables[0].Rows[i - 1]["EVENTTIME"] != null && dsDetailResult.Tables[0].Rows[i]["EVENTTIME"] != null)
        //        {
        //            dsDetailResult.Tables[0].Rows[i - 1]["LAG"] = SetTimeLag(dsDetailResult.Tables[0].Rows[i - 1]["EVENTTIME"].ToString(), dsDetailResult.Tables[0].Rows[i]["EVENTTIME"].ToString());
        //        }
        //    }
        //}

        gridViewCSSystemLogDetail.DataSource = dsDetailResult.Tables[0].DefaultView;
        gridViewCSSystemLogDetail.DataKeyNames = new string[] { "INDATE" };//主键
        gridViewCSSystemLogDetail.DataBind();
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

    protected void gridViewCSSystemLogDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ////    //this.gridViewRegion.PageIndex = e.NewPageIndex;
        ////    //BindGridView();

        //    //执行循环，保证每条数据都可以更新
        //    for (int i = 1; i <= gridViewCSSystemLogDetail.Rows.Count; i++)
        //    {
        //        //首先判断是否是数据行
        //        e.Row.
        //    }
    }

    protected void gridViewCSSystemLogDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSSystemLogDetail.PageIndex = e.NewPageIndex;
        BindViewCSSystemLogDetail();
    }

    private string GetCancleReason(string reasonCode)
    {
        string reasonName = string.Empty;
        Hashtable htReason = new Hashtable();
        htReason.Add("CRG18", "LM订单客人手机端取消");
        htReason.Add("CRG17", "预授权失败自动取消");
        htReason.Add("CRC01", "满房");
        htReason.Add("CRC03", "员工差错");
        htReason.Add("CRC04", "蓄水单取消");
        htReason.Add("CRC06", "变价");
        htReason.Add("CRG14", "无法完成担保");
        htReason.Add("CRG06", "无法完成支付");
        htReason.Add("CRG11", "超时未支付");
        htReason.Add("CRG05", "测试订单");
        htReason.Add("CRG01", "行程改变");
        htReason.Add("CRG02", "无法满足特殊需求");
        htReason.Add("CRG03", "其他途径预订");
        htReason.Add("CRG04", "预订内容变更");
        htReason.Add("CRG07", "确认速度不满意");
        htReason.Add("CRG08", "GDS渠道取消");
        htReason.Add("CRG09", "IDS渠道取消");
        htReason.Add("CRG10", "接口渠道取消");
        htReason.Add("CRG13", "设施/位置不满意");
        htReason.Add("CRC02", "重复预订");
        htReason.Add("CRG15", "预订未用抵用券");
        htReason.Add("CRG16", "预订未登录");
        htReason.Add("CRC07", "Jrez渠道取消");
        htReason.Add("CRC05", "骚扰订单");
        htReason.Add("CRG99", "其他");
        htReason.Add("CRH01", "酒店反悔");
        htReason.Add("CRH03", "酒店停业/装修");
        htReason.Add("PGSRQ", "系统自动取消");
        htReason.Add("CRH02", "酒店不确认");
        htReason.Add("CRH05", "无法追加担保");
        htReason.Add("CRH06", "无法追加预付");
        htReason.Add("CRH07", "无协议/协议到期");
        htReason.Add("CRH08", "不可抗力");

        reasonName = htReason.ContainsKey(reasonCode) ? htReason[reasonCode].ToString() : reasonCode;
        return reasonName;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(txtOrderID.Text.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if (!String.IsNullOrEmpty(hidOrderID.Value))
        {
            UnLockOrderConfirm(hidOrderID.Value.Trim());
        }

        trCanlReson.Style.Add("display", "none");
        hidOrderID.Value = txtOrderID.Text.Trim();
        BindViewCSSystemLogMain();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    private void UnLockOrderConfirm(string orderID)
    {
        if (!String.IsNullOrEmpty(orderID))
        {
            OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
            _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
            OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

            orderinfoEntity.OrderID = orderID;
            _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
            int iResut = OrderInfoBP.UnLockOrderConfirm(_orderInfoEntity).Result;

            _commonEntity.LogMessages = _orderInfoEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单操作-解锁";
            commonDBEntity.Event_ID = hidHotelID.Value;
            string conTent = GetLocalResourceObject("EventUnLockOrderMessage").ToString();

            conTent = string.Format(conTent, orderID, _orderInfoEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;

            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateUnLockOrderSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }
    }

    protected void btnSet_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(hidOrderID.Value.Trim()))
        {
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            return;
        }

        if (!String.IsNullOrEmpty(txtBOOK_REMARK.Text.Trim()) && (StringUtility.Text_Length(txtBOOK_REMARK.Text.ToString().Trim()) > 250))
        {
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorRemark").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorRemark").ToString();
            return;
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;
        _lmSystemLogEntity.OrderBookStatus = ddpOrderStatus.SelectedValue.Trim();
        _lmSystemLogEntity.CanelReson = ddpCanelReson.SelectedValue.Trim();
        _lmSystemLogEntity.BookRemark = txtBOOK_REMARK.Text.Trim();
        _lmSystemLogEntity.FollowUp = (chkFollowUp.Checked) ? "1" : "0";

        int iResult = LmSystemLogBP.SaveOrderOperation(_lmSystemLogEntity);

        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单操作-保存";
        commonDBEntity.Event_ID = hidHotelID.Value + "-" + _lmSystemLogEntity.FogOrderID;
        string conTent = GetLocalResourceObject("EventSaveMessage").ToString();

        conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.OrderBookStatus, _lmSystemLogEntity.BookRemark, _lmSystemLogEntity.CanelReson, _lmSystemLogEntity.FollowUp);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateLockErr").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateLockErr").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateLockErr").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
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
        orderinfoEntity.EventType = "订单确认";
        orderinfoEntity.ORDER_NUM = hidOrderID.Value;
        orderinfoEntity.OdStatus = SetActionTypeVal(ddpOrderStatus.SelectedValue.Trim());
        orderinfoEntity.REMARK = txtBOOK_REMARK.Text.Trim();
        orderinfoEntity.ActionID = "";
        orderinfoEntity.CANNEL = ("9".Equals(ddpOrderStatus.SelectedValue.Trim())) ? ddpCanelReson.SelectedValue.Trim() : "";
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        CommonBP.InsertOrderActionHistoryList(_orderInfoEntity);

        lbMemo1.Text = SetMemoVal(_lmSystemLogEntity.FogOrderID);
        RestControlVal();
    }

    private string SetActionTypeVal(string strType)
    {
        if ("LMBAR".Equals(hidPriceCode.Value.ToUpper()))
        {
            return "取消单";
        }
        else
        {
            switch (strType.ToString())
            {
                //case "0":
                //    dr["ORDERSTATUS"] = "3";
                //    dr["ORDERDIS"] = "用户取消";
                //    break;
                case "4":
                    return "可入住";
                //case "1":
                //    dr["ORDERSTATUS"] = "5";
                //    dr["ORDERDIS"] = "NoShow";
                //    break;
                case "7":
                    return "入住中";
                //case "3":
                //    dr["ORDERSTATUS"] = "8";
                //    dr["ORDERDIS"] = "已离店";
                //    break;
                case "9":
                    return "CC取消";
                default:
                    //    dr["ORDERSTATUS"] = "";
                    //    dr["ORDERDIS"] = "";
                    //    break;
                    return "";
            }
        }
    }

    private void RestControlVal()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsMainResult = LmSystemLogBP.OrderOperationSelect(_lmSystemLogEntity).QueryResult;
        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            return;
        }

        lbCREATE_TIME.Text = dsMainResult.Tables[0].Rows[0]["create_time"].ToString();
        lbORDER_CHANNEL.Text = dsMainResult.Tables[0].Rows[0]["ORDER_CHANNEL"].ToString();
        lbPRICE_CODE.Text = dsMainResult.Tables[0].Rows[0]["price_code_nm"].ToString();
        hidPriceCode.Value = dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper();
        lbBOOK_STATUS.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        lbIS_GUA.Text = dsMainResult.Tables[0].Rows[0]["is_gua_nm"].ToString();
        lbRESV_GUA_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_HOLD_TIME"].ToString();
        lbUSER_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["USER_HOLD_TIME"].ToString();
        lbRESV_GUA_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_DESC"].ToString();
        lbRESV_CXL_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_CXL_DESC"].ToString();
        lbPAY_STATUS.Text = dsMainResult.Tables[0].Rows[0]["pay_status_nm"].ToString();
        lbHOTEL_NAME.Text = dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();
        lbLINKTEL.Text = dsMainResult.Tables[0].Rows[0]["linktel"].ToString();
        lbGUEST_NAMES.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();
        lbCONTACT_NAME.Text = dsMainResult.Tables[0].Rows[0]["contact_name"].ToString();
        lbCONTACT_TEL.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();
        lbLOGIN_MOBILE.Text = dsMainResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString();
        lbOrderDays.Text = SetOrderDaysVal(dsMainResult.Tables[0].Rows[0]["in_date"].ToString(), dsMainResult.Tables[0].Rows[0]["out_date"].ToString());
        lbIN_DATE.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString();
        lbOUT_DATE.Text = dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString();
        lbROOM_TYPE_NAME.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString();
        lbBOOK_ROOM_NUM.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString();
        lbARRIVE_TIME.Text = dsMainResult.Tables[0].Rows[0]["ARRIVE_TIME"].ToString();
        lbTICKET_USERCODE.Text = dsMainResult.Tables[0].Rows[0]["ticket_usercode"].ToString();
        lbTICKET_PAGENM.Text = dsMainResult.Tables[0].Rows[0]["packagename"].ToString();
        lbTICKET_AMOUNT.Text = dsMainResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        lbBOOK_REMARK.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();
        lbORDER_NUM.Text = dsMainResult.Tables[0].Rows[0]["order_id"].ToString();
        chkFollowUp.Checked = ("1".Equals(dsMainResult.Tables[0].Rows[0]["FOLLOW_UP"].ToString())) ? true : false;
        lblSalesMG.Text = SetHotelSalesInfo(dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString());
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(hidOrderID.Value.Trim()))
        {
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            return;
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsMainResult = LmSystemLogBP.OrderOperationSelect(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            //hidOrderID.Value = "";
            btnPrint.Visible = false;
            btnSendFax.Visible = false;
            btnSet.Visible = false;
            dvErrorInfo.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        string strMsg = LmSystemLogBP.UnlockOrderConfirmControl(_lmSystemLogEntity).ErrorMSG;
        if (!String.IsNullOrEmpty(strMsg))
        {
            //hidOrderID.Value = "";
            btnPrint.Visible = false;
            btnSendFax.Visible = false;
            btnSet.Visible = false;
            //detailMessageContent.InnerHtml = strMsg;
            dvErrorInfo.InnerHtml = strMsg;
            return;
        }
        else
        {
            _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单操作-强制锁定";
            commonDBEntity.Event_ID = hidHotelID.Value;
            string conTent = GetLocalResourceObject("EventUnLockMessage").ToString();

            conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;

            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }
    }

    protected void ddpOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ("9".Equals(ddpOrderStatus.SelectedValue))
        {
            trCanlReson.Style.Add("display", "");
        }
        else
        {
            trCanlReson.Style.Add("display", "none");
        }
    }

    //发送传真
    protected void btnSendFax_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerText = AssemblyText();
    }

    public static string SendFax(string type, string orderID, string path, string fileName, string ContentType, string ClientTaskID, string FaxNumber, string faxType)
    {
        string sendFaxToServerBack = "";
        try
        {
            FaxService fax = new FaxService();
            fax.Timeout = 1200000;
            string xml = ToServiceXML.getSendFaxToServerXMLStr(path, fileName, ContentType, ClientTaskID, FaxNumber); //拼装xml数据
            sendFaxToServerBack = fax.SendFaxToServer(xml); //开始远程调用
            string sendFaxToServerBack1 = sendFaxToServerBack.Replace(">", ">\r\n");
        }
        catch (Exception ex)
        {
            ex.GetBaseException();
            return "递交失败，请重试！";
        }

        ///////////////////////////////////解析反馈结果///////////////////////////////////////
        XmlDocument m_XmlDoc = new XmlDocument();
        string queryResultS = "";
        try
        {
            m_XmlDoc.LoadXml(sendFaxToServerBack);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(m_XmlDoc.NameTable);
            XmlNodeList nodeList = m_XmlDoc.ChildNodes;
            XmlNode node = nodeList.Item(1);
            string ErrorFlag = node.FirstChild.SelectSingleNode("Header").SelectSingleNode("ErrorFlag").InnerText;
            string ReturnMessage = node.FirstChild.SelectSingleNode("Header").SelectSingleNode("ReturnMessage").InnerText;
            string JobNo = "";
            string JobResult = "";
            string TotalNum = "";
            string ValidNum = "";
            try
            {
                JobNo = node.FirstChild.SelectSingleNode("SendFaxToServerResponse").SelectSingleNode("SendFaxToServerResult").SelectSingleNode("JobNo").InnerText;
                JobResult = node.FirstChild.SelectSingleNode("SendFaxToServerResponse").SelectSingleNode("SendFaxToServerResult").SelectSingleNode("JobResult").InnerText;
                TotalNum = node.FirstChild.SelectSingleNode("SendFaxToServerResponse").SelectSingleNode("SendFaxToServerResult").SelectSingleNode("TotalNum").InnerText;
                ValidNum = node.FirstChild.SelectSingleNode("SendFaxToServerResponse").SelectSingleNode("SendFaxToServerResult").SelectSingleNode("ValidNum").InnerText;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
            }
            queryResultS = queryResultS + "ErrorFlag :" + ErrorFlag + "\r\n" + "ReturnMessage:" + ReturnMessage + "\r\n" + "JobNo :" + JobNo + "\r\n" +
                "JobResult :" + JobResult + "\r\n" + "TotalNum :" + TotalNum + "\r\n" + "ValidNum :" + ValidNum + "\r\n";
            OrderFaxDetialUpdate(type, JobNo, path, ErrorFlag, orderID, ClientTaskID, faxType);
            return ReturnMessage;
        }
        catch (Exception ex)
        {
            ex.GetBaseException();
            return "递交失败，请重试！";
        }
    }

    /// <summary>
    /// 拼装txt
    /// </summary>
    /// <param name="dsResult"></param>
    public string AssemblyText()
    {
        string ClientTaskID = getTicketCode();

        byte[] bytes;
        StringBuilder sbTxt = new StringBuilder();

        //DataTable baseDtResult = ViewState["baseDtResult"] as DataTable;
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataTable baseDtResult = LmSystemLogBP.OrderOperationSelectByPrint(_lmSystemLogEntity).QueryResult.Tables[0];

        #region 根据OrderNo获取详细
        if (baseDtResult != null && baseDtResult.Rows.Count > 0)
        {
            //读取txt模板
            if (!File.Exists(Server.MapPath("Formwork\\SendFax.txt")))
            {
                Console.WriteLine("模板文件不存在，请检查!");
            }
            //替换模板内容 
            string Content = File.ReadAllText(Server.MapPath("Formwork\\SendFax.txt"));

            bytes = Encoding.Default.GetBytes(baseDtResult.Rows[0]["linkfax"].ToString().Trim());
            for (int l = 0; l < 38 - bytes.Length; l++)
            {
                sbTxt.Append(" ");
            }
            Content = Content.Replace("[lblFax]", baseDtResult.Rows[0]["linkfax"].ToString() + sbTxt.ToString());

            Content = Content.Replace("[lblSystemDate]", System.DateTime.Now.ToString());
            Content = Content.Replace("[lblHotelName]", baseDtResult.Rows[0]["hotel_id"].ToString() + "---" + baseDtResult.Rows[0]["hotel_name"].ToString());

            bytes = Encoding.Default.GetBytes(baseDtResult.Rows[0]["fog_order_num"].ToString().Trim());
            sbTxt = new StringBuilder();
            for (int l = 0; l < 33 - bytes.Length; l++)
            {
                sbTxt.Append(" ");
            }
            Content = Content.Replace("[lblOrderNum]", baseDtResult.Rows[0]["fog_order_num"].ToString() + sbTxt.ToString());

            Content = Content.Replace("[lblBookStatus]", "LMBAR".Equals(baseDtResult.Rows[0]["price_code"].ToString().Trim().ToUpper()) ? baseDtResult.Rows[0]["book_status_nm"].ToString() : baseDtResult.Rows[0]["book_status_other_nm"].ToString());
            Content = Content.Replace("[lblCustomerName]", baseDtResult.Rows[0]["guest_names"].ToString());

            bytes = Encoding.Default.GetBytes(baseDtResult.Rows[0]["in_date_nm"].ToString().Trim());
            sbTxt = new StringBuilder();
            for (int l = 0; l < 18 - bytes.Length; l++)
            {
                sbTxt.Append(" ");
            }
            Content = Content.Replace("[lblInDate]", baseDtResult.Rows[0]["in_date_nm"].ToString() + sbTxt.ToString());

            bytes = Encoding.Default.GetBytes(baseDtResult.Rows[0]["out_date_nm"].ToString().Trim());
            sbTxt = new StringBuilder();
            for (int l = 0; l < 20 - bytes.Length; l++)
            {
                sbTxt.Append(" ");
            }
            Content = Content.Replace("[lblOutDate]", baseDtResult.Rows[0]["out_date_nm"].ToString() + sbTxt.ToString());

            Content = Content.Replace("[lblInDay]", SetOrderDaysVal(baseDtResult.Rows[0]["in_date"].ToString(), baseDtResult.Rows[0]["out_date"].ToString()));

            bytes = Encoding.Default.GetBytes(baseDtResult.Rows[0]["ROOM_TYPE_CODE"].ToString().Trim() + "---" + baseDtResult.Rows[0]["room_type_name"].ToString().Trim());
            sbTxt = new StringBuilder();
            for (int l = 0; l < 18 - bytes.Length; l++)
            {
                sbTxt.Append(" ");
            }
            Content = Content.Replace("[lblRoomTypeName]", baseDtResult.Rows[0]["ROOM_TYPE_CODE"].ToString() + "---" + baseDtResult.Rows[0]["room_type_name"].ToString() + sbTxt.ToString());

            bytes = Encoding.Default.GetBytes(baseDtResult.Rows[0]["book_room_num"].ToString().Trim());
            sbTxt = new StringBuilder();
            for (int l = 0; l < 20 - bytes.Length; l++)
            {
                sbTxt.Append(" ");
            }
            Content = Content.Replace("[lblRoomNum]", baseDtResult.Rows[0]["book_room_num"].ToString() + sbTxt.ToString());

            Content = Content.Replace("[lblInPeopleNum]", baseDtResult.Rows[0]["guest_names"].ToString().Split(',').Length.ToString());

            bytes = Encoding.Default.GetBytes(baseDtResult.Rows[0]["ROOM_TYPE_CODE"].ToString().Trim() + "--" + baseDtResult.Rows[0]["price_code_nm"].ToString().Trim());
            sbTxt = new StringBuilder();
            for (int l = 0; l < 18 - bytes.Length; l++)
            {
                sbTxt.Append(" ");
            }
            Content = Content.Replace("[lblPlanName]", baseDtResult.Rows[0]["ROOM_TYPE_CODE"].ToString() + "--" + baseDtResult.Rows[0]["price_code_nm"].ToString() + sbTxt.ToString());

            bytes = Encoding.Default.GetBytes(baseDtResult.Rows[0]["RESV_GUA_HOLD_TIME"].ToString().Trim());
            sbTxt = new StringBuilder();
            for (int l = 0; l < 16 - bytes.Length; l++)
            {
                sbTxt.Append(" ");
            }
            Content = Content.Replace("[lblLasterDate]", baseDtResult.Rows[0]["RESV_GUA_HOLD_TIME"].ToString() + sbTxt.ToString());
            Content = Content.Replace("[lblPayType]", baseDtResult.Rows[0]["price_code_nm"].ToString());
            Content = Content.Replace("[lblPriceCount]", baseDtResult.Rows[0]["BOOK_TOTAL_PRICE"].ToString());
            Content = Content.Replace("[lblGuaDesc]", baseDtResult.Rows[0]["RESV_GUA_DESC"].ToString());
            Content = Content.Replace("[lblCxlDesc]", baseDtResult.Rows[0]["RESV_CXL_DESC"].ToString());
            Content = Content.Replace("[lbBOOK_REMARK]", baseDtResult.Rows[0]["BOOK_REMARK"].ToString());

            //循环日期  早餐  价格 
            StringBuilder sb = new StringBuilder();
            DataTable detailDtResult = ViewState["detailDtResult"] as DataTable;
            if (detailDtResult != null && detailDtResult.Rows.Count > 0)
            {
                for (int j = 0; j < detailDtResult.Rows.Count; j++)
                {
                    bytes = Encoding.Default.GetBytes(detailDtResult.Rows[j]["INDATE"].ToString().Trim());
                    sbTxt = new StringBuilder();
                    for (int l = 0; l < 10 - bytes.Length; l++)
                    {
                        sbTxt.Append(" ");
                    }
                    bytes = Encoding.Default.GetBytes(detailDtResult.Rows[j]["TWOPRICE"].ToString().Trim());
                    StringBuilder priceSbTxt = new StringBuilder();
                    for (int l = 0; l < 8 - bytes.Length; l++)
                    {
                        priceSbTxt.Append(" ");
                    }
                    bytes = Encoding.Default.GetBytes(detailDtResult.Rows[j]["BREAKFAST"].ToString().Trim());
                    StringBuilder fastSbTxt = new StringBuilder();
                    for (int l = 0; l < 4 - bytes.Length; l++)
                    {
                        fastSbTxt.Append(" ");
                    }
                    sb.Append("|  " + detailDtResult.Rows[j]["INDATE"].ToString() + sbTxt.ToString() + "  |    " + detailDtResult.Rows[j]["TWOPRICE"].ToString() + priceSbTxt.ToString() + "|     " + detailDtResult.Rows[j]["BREAKFAST"].ToString() + fastSbTxt.ToString() + " |\r\n  ----------------------------------------");
                }
            }
            Content = Content.Replace("[INDATE]", sb.ToString());

            //重新写入模板  根据OrderNo  创建新文件
            if (File.Exists(Server.MapPath("Formwork\\" + baseDtResult.Rows[0]["fog_order_num"].ToString() + ".txt")))
            {
                File.Delete(Server.MapPath("Formwork\\" + baseDtResult.Rows[0]["fog_order_num"].ToString() + ".txt"));
            }
            File.Create(Server.MapPath("Formwork\\" + baseDtResult.Rows[0]["fog_order_num"].ToString() + ".txt")).Close();
            File.WriteAllText(Server.MapPath("Formwork\\" + baseDtResult.Rows[0]["fog_order_num"].ToString() + ".txt"), Content, System.Text.Encoding.GetEncoding("GB2312"));

            //根据首发时间 判别是第一次发送还是失败重发
            string type = baseDtResult.Rows[0]["fax_fdtime"].ToString() == "" ? "0" : "1";
            string faxType = "";
            if (baseDtResult.Rows[0]["price_code"].ToString().Trim().ToLower() == "lmbar")
            {
                if (baseDtResult.Rows[0]["BOOK_STATUS"].ToString().Trim().Contains("5"))//新单(特价订单已付款)-->成功
                {
                    faxType = "1";
                }
                if (baseDtResult.Rows[0]["BOOK_STATUS"].ToString().Trim().Contains("4"))
                {
                    //全部改为取消单
                    faxType = "2";
                }
            }
            if (baseDtResult.Rows[0]["price_code"].ToString().Trim().ToLower() == "lmbar2")
            {
                if (baseDtResult.Rows[0]["BOOK_STATUS_OTHER"].ToString().Trim().Contains("0") ||
                        baseDtResult.Rows[0]["BOOK_STATUS_OTHER"].ToString().Trim().Contains("1") ||
                        baseDtResult.Rows[0]["BOOK_STATUS_OTHER"].ToString().Trim().Contains("2")) //新单(需前台现付)-->新建  新单(需前台现付)-->预定成功等待确认 新单(需前台现付)-->新建入fog失败  用户取消单-->用户取消 
                {
                    faxType = "2";
                }
                if (baseDtResult.Rows[0]["BOOK_STATUS_OTHER"].ToString().Trim().Contains("3") || baseDtResult.Rows[0]["BOOK_STATUS_OTHER"].ToString().Trim().Contains("9"))
                {
                    //客户取消单   9  取消单
                    faxType = "2";
                }
            }

            string sendFaxToServerBack = SendFax(type, baseDtResult.Rows[0]["order_id"].ToString(), Server.MapPath("Formwork\\" + baseDtResult.Rows[0]["fog_order_num"].ToString() + ".txt"), baseDtResult.Rows[0]["fog_order_num"].ToString() + ".txt", "TXT", ClientTaskID, baseDtResult.Rows[0]["linkfax"].ToString(), faxType);
            return sendFaxToServerBack;
        }
        return "递交失败，请重试！";
        #endregion

    }

    /// <summary>
    /// 生成13位随机数
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    private string getTicketCode()
    {
        string str = "1234567890";
        StringBuilder builder = new StringBuilder();
        bool flag = true;
        while (flag)
        {
            builder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                builder.Append(str[new Random(Guid.NewGuid().GetHashCode()).Next(0, 9)]);
            }
            if (double.Parse(builder.ToString()) < 2147483647)
            {
                flag = false;
            }
        }
        return builder.ToString();
    }

    /// <summary>
    /// 修改Fax信息
    /// </summary>
    /// <returns></returns>
    public static int OrderFaxDetialUpdate(string type, string faxNo, string faxImageUrl, string status, string orderID, string ClientTaskID, string faxType)
    {
        //0   正常发送   1  失败重发
        int result = 0;
        if (type.Contains("0"))
        {
            OracleParameter[] parm ={
                                    new OracleParameter("FAX_SYSTEM",OracleType.VarChar),
                                    new OracleParameter("FAX_NO",OracleType.VarChar),
                                    new OracleParameter("FAX_IMAGE_URL",OracleType.VarChar),
                                    new OracleParameter("FAX_STATUS",OracleType.VarChar),
                                    new OracleParameter("FAX_DTIME",OracleType.VarChar),
                                    new OracleParameter("FAX_FDTIME",OracleType.VarChar),
                                    new OracleParameter("CLIENT_TASK_ID",OracleType.VarChar),
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("FAX_TYPE",OracleType.VarChar)
                                };
            parm[0].Value = "SendFax-CMS";
            if (String.IsNullOrEmpty(faxNo))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = faxNo;
            }
            if (String.IsNullOrEmpty(faxImageUrl))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = faxImageUrl;
            }
            if (String.IsNullOrEmpty(status))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                if (status == "0")
                {
                    parm[3].Value = "1";
                    parm[8].Value = faxType;
                }
                else
                {
                    parm[3].Value = "3";//待重试
                }
            }
            parm[4].Value = System.DateTime.Now.ToString();//成功发送时间
            parm[5].Value = System.DateTime.Now.ToString();//首发时间
            parm[6].Value = ClientTaskID;
            parm[7].Value = orderID;
            result = HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_faxdetial_update", parm);
        }
        else
        {
            OracleParameter[] parm ={
                                    new OracleParameter("FAX_SYSTEM",OracleType.VarChar),
                                    new OracleParameter("FAX_NO",OracleType.VarChar),
                                    new OracleParameter("FAX_IMAGE_URL",OracleType.VarChar),
                                    new OracleParameter("FAX_STATUS",OracleType.VarChar),
                                    new OracleParameter("FAX_DTIME",OracleType.VarChar),
                                     new OracleParameter("CLIENT_TASK_ID",OracleType.VarChar),
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("FAX_TYPE",OracleType.VarChar)
                                };
            parm[0].Value = "SendFax-CMS";
            if (String.IsNullOrEmpty(faxNo))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = faxNo;
            }
            if (String.IsNullOrEmpty(faxImageUrl))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = faxImageUrl;
            }
            if (String.IsNullOrEmpty(status))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                if (status == "0")
                {
                    parm[3].Value = "1";
                    parm[7].Value = faxType;
                }
                else
                {
                    parm[3].Value = "3";//待重试
                }
            }
            parm[4].Value = System.DateTime.Now.ToString();//成功发送时间
            parm[5].Value = ClientTaskID;
            parm[6].Value = orderID;
            result = HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_faxdetial_repeat_update", parm);
        }
        return result;
    }

}
