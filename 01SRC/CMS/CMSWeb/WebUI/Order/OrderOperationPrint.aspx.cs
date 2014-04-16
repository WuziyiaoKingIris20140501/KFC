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

public partial class WebUI_Order_OrderOperationPrint : System.Web.UI.Page
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

        DataSet dsMainResult = LmSystemLogBP.OrderOperationSelectByPrint(_lmSystemLogEntity).QueryResult;

        //lblFax.Text = "";传真FAX 
        ViewState["order_num"] = dsMainResult.Tables[0].Rows[0]["fog_order_num"].ToString();
        lblSystemDate.Text = System.DateTime.Now.ToString();//首发时间
        lblHotelName.Text = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString() + "---" + dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();//预定酒店
        lblOrderNum.Text = orderID;//订单号
        lblBookStatus.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        //lblGua.Text = dsMainResult.Tables[0].Rows[0]["is_gua_nm"].ToString();
        lblCustomerName.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();//客人姓名
        lblInDate.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString(); ;//入住时间
        lblOutDate.Text = dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString(); ; //离店日期
        lblInDay.Text = SetOrderDaysVal(dsMainResult.Tables[0].Rows[0]["in_date"].ToString(), dsMainResult.Tables[0].Rows[0]["out_date"].ToString()); ; //入住天数
        lblRoomTypeName.Text = dsMainResult.Tables[0].Rows[0]["ROOM_TYPE_CODE"].ToString() + "---" + dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString(); //房型名称
        lblRoomNum.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString(); //房间数量
        lblInPeopleNum.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString().Split(',').Length.ToString(); //入住人数
        lblPlanName.Text = dsMainResult.Tables[0].Rows[0]["ROOM_TYPE_CODE"].ToString() + "--" + dsMainResult.Tables[0].Rows[0]["price_code_nm"].ToString(); //计划名称 
        lblLasterDate.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_HOLD_TIME"].ToString(); //最晚到店时间(酒店规定)
        lblPayType.Text = dsMainResult.Tables[0].Rows[0]["price_code_nm"].ToString(); //付款方式
        lblPriceCount.Text = dsMainResult.Tables[0].Rows[0]["BOOK_TOTAL_PRICE"].ToString();  //总价
        lbBOOK_REMARK.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();  //备注
        lblGuaDesc.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_DESC"].ToString();
        lblCxlDesc.Text = dsMainResult.Tables[0].Rows[0]["RESV_CXL_DESC"].ToString();

        BindViewCSSystemLogDetail(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper());
        //lbCREATE_TIME.Text = dsMainResult.Tables[0].Rows[0]["create_time"].ToString();

        //lbORDER_CHANNEL.Text = dsMainResult.Tables[0].Rows[0]["ORDER_CHANNEL"].ToString();
        //lbPRICE_CODE.Text = dsMainResult.Tables[0].Rows[0]["price_code_nm"].ToString();
        //lbBOOK_STATUS.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        //lbIS_GUA.Text = dsMainResult.Tables[0].Rows[0]["is_gua_nm"].ToString();
        //lbRESV_GUA_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_HOLD_TIME"].ToString();
        //lbUSER_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["USER_HOLD_TIME"].ToString();
        //lbRESV_GUA_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_DESC"].ToString();
        //lbRESV_CXL_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_CXL_DESC"].ToString();
        //lbPAY_STATUS.Text = dsMainResult.Tables[0].Rows[0]["pay_status_nm"].ToString();
        //lbHOTEL_NAME.Text = dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();
        //lbLINKTEL.Text = dsMainResult.Tables[0].Rows[0]["linktel"].ToString();
        //lbGUEST_NAMES.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();
        //lbCONTACT_NAME.Text = dsMainResult.Tables[0].Rows[0]["contact_name"].ToString();
        //lbCONTACT_TEL.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();
        //lbLOGIN_MOBILE.Text = dsMainResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString();
        //lbOrderDays.Text = SetOrderDaysVal(dsMainResult.Tables[0].Rows[0]["in_date"].ToString(), dsMainResult.Tables[0].Rows[0]["out_date"].ToString());
        //lbIN_DATE.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString();
        //lbOUT_DATE.Text = dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString();
        //lbROOM_TYPE_NAME.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString();
        //lbBOOK_ROOM_NUM.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString();
        //lbARRIVE_TIME.Text = dsMainResult.Tables[0].Rows[0]["ARRIVE_TIME"].ToString();
        //lbTICKET_USERCODE.Text = dsMainResult.Tables[0].Rows[0]["ticket_usercode"].ToString();
        //lbTICKET_PAGENM.Text = dsMainResult.Tables[0].Rows[0]["packagename"].ToString();
        //lbTICKET_AMOUNT.Text = dsMainResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        //lbBOOK_REMARK.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();
        //lbORDER_NUM.Text = dsMainResult.Tables[0].Rows[0]["id"].ToString();
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

    private void BindViewCSSystemLogDetail(string PriceCode)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = ViewState["order_num"].ToString();
        _lmSystemLogEntity.PriceCode = PriceCode;

        DataSet dsDetailResult = LmSystemLogBP.OrderOperationDetailSelect(_lmSystemLogEntity).QueryResult;

        gridViewCSSystemLogDetail.DataSource = dsDetailResult.Tables[0].DefaultView;
        gridViewCSSystemLogDetail.DataKeyNames = new string[] { "INDATE" };//主键
        gridViewCSSystemLogDetail.DataBind();
    }
    protected void gridViewCSSystemLogDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow||e.Row.RowType == DataControlRowType.Footer||e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Attributes.Add("style", "border:Dashed 1px black; border-collapse:collapse");
            e.Row.Cells[1].Attributes.Add("style", "border:Dashed 1px black; border-collapse:collapse");
            e.Row.Cells[2].Attributes.Add("style", "border:Dashed 1px black; border-collapse:collapse");
        }
    }
}