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

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using HotelVp.Common.Utilities;

public partial class OrderConfirmInfoPrint : System.Web.UI.Page
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            string orderID = Request.QueryString["ID"] == null ? "" : Request.QueryString["ID"].ToString();
            
            BindViewCSSystemLogMain(orderID);
            
            //Image1.ImageUrl = "code128.aspx?num=" + orderID;
            Response.Write(" <script> window.print(); </script> ");
        }
    }


    private void BindViewCSSystemLogMain(string orderID)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = orderID;

        DataSet dsMainResult = LmSystemLogBP.OrderComfirmByPrint(_lmSystemLogEntity).QueryResult;

        ViewState["order_num"] = dsMainResult.Tables[0].Rows[0]["fog_order_num"].ToString();

        lblOrderNumTip.Text = orderID;//订单号
        lblOrderNum.Text = orderID;//订单号
        lblHotelName.Text = dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();//预定酒店
        lblHotelID.Text = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();//预定酒店ID
        lblHTel.Text = GetHotelEXInfo(dsMainResult.Tables[0].Rows[0]["ODLINKTEL"].ToString());
        lblHFax.Text = GetHotelEXInfo(dsMainResult.Tables[0].Rows[0]["ODLINKFAX"].ToString());
        lblSystemDate.Text = System.DateTime.Now.ToString().Replace("/", "-");//首发时间
        lblCustomerName.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();//客人姓名
        lblCustomerTel.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();

        lblRoomTypeName.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString(); //房型名称
        lblPriceCount.Text = dsMainResult.Tables[0].Rows[0]["BOOK_TOTAL_PRICE"].ToString();  //总价
        lblDetailPriceCount.Text = dsMainResult.Tables[0].Rows[0]["BOOK_TOTAL_PRICE"].ToString();  //总价
        lblInDate.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString();//入住时间
        lblOutDate.Text = dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString(); //离店日期
        lblRoomNum.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString(); //房间数量

        lblInDayNum.Text = GetInDaysNum(dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString(), dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString()); //入住人数

        if (!"LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()))
        {
            lblPriceCode.Text = "前台现付(返佣)";
        }
        else
        {
            lblPriceCode.Text = "本单客人已经付款，请勿向用户重复收款（本单和‘今夜酒店特价’结算）";
        }


        lblCusRemark.Text = "";
        lblOrderRemark.Text = "";

        lblFullDT.Text = System.DateTime.Now.ToShortDateString().Replace("/", "-");//首发时间


        if ("1".Equals(dsMainResult.Tables[0].Rows[0]["IS_GUA"].ToString()))
        {
            lblIsGUA.Text = "本单已经担保";
        }
        else
        {
            lblIsGUA.Text = "本单非担保";
        }
        lblDetialInOutDT.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString() + " 至 " + dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString();
        lblDetialTwoPrice.Text = dsMainResult.Tables[0].Rows[0]["book_price"].ToString();
        lblDetialTotDays.Text = lblInDayNum.Text;
        lblDetialBreaks.Text = dsMainResult.Tables[0].Rows[0]["breakfast_num"].ToString();

        BindViewCSSystemLogDetail(dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString(), dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper());
    }

    private string GetHotelEXInfo(string strParam)
    {
        string strResult = "";
        if (String.IsNullOrEmpty(strParam))
        {
            return strResult;
        }

        int iHour = DateTime.Now.Hour;
        string[] strList = strParam.Split('|');
        if (strList.Length >= iHour + 1)
        {
            strResult = strList[iHour].ToString();
        }

        return strResult;
    }

    private string GetInDaysNum(string strStartDT, string strEndDT)
    {
        string strResult = "";
        try
        {
            DateTime startDT = DateTime.Parse(strStartDT);
            DateTime EndDT = DateTime.Parse(strEndDT);
            System.TimeSpan ND= EndDT - startDT;
            strResult = ND.Days.ToString();
        }
        catch
        {
            strResult = "";
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

    private void BindViewCSSystemLogDetail(string HotelID, string PriceCode)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.HotelID = HotelID;
        _lmSystemLogEntity.PriceCode = "LMBAR2";

        DataSet dsDetailResult = LmSystemLogBP.OrderComfirmPlanByPrint(_lmSystemLogEntity).QueryResult;

        if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 0)
        {
            string strBody = "[ ]{0}-{1}元/晚&nbsp; &nbsp;";
            StringBuilder sbRooms = new StringBuilder();

            for (int i = 0; i < dsDetailResult.Tables[0].Rows.Count; i++ )
            {
                sbRooms.Append(String.Format(strBody, dsDetailResult.Tables[0].Rows[i]["ROOMNM"].ToString(), dsDetailResult.Tables[0].Rows[i]["TWOPRICE"].ToString()));
                if ((i+1)%3 == 0)
                {
                    sbRooms.Append("<br/><span style='margin-left: 5px;'/>");
                }
            }

            spRoom.InnerHtml = sbRooms.ToString();

            //foreach (DataRow dr in dsDetailResult.Tables[0].Rows)
            //{
            //    sbRooms.Append(String.Format(strBody, dr["ROOMNM"].ToString(), dr["TWOPRICE"].ToString()));
            //}
            //spRoom.InnerHtml = sbRooms.ToString();

        }
        else
        {
            spRoom.InnerHtml = "";
        }
    }
}