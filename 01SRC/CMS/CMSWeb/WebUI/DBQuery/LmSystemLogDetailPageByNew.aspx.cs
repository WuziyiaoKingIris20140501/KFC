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
using HotelVp.CMS.Domain.Entity.Order;

public partial class WebUI_DBQuery_LmSystemOrderDetailPage : System.Web.UI.Page
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    OrderRefundEntity _OrderRefundEntity = new OrderRefundEntity();
    HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();
    OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
    string FogOrderNum = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string EventLMID = string.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                EventLMID = Request.QueryString["ID"].ToString().Trim();

            }
            else if (!String.IsNullOrEmpty(Request.QueryString["FOGID"]))
            {
                EventLMID = GetEventLMOrderID(Request.QueryString["FOGID"].ToString().Trim());

                if (String.IsNullOrEmpty(EventLMID.Trim()))
                {
                    detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                    return;
                }
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            }

            hidEventLMID.Value = EventLMID;
            BindViewCSSystemLogMain();
        }
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
        _lmSystemLogEntity.EventID = hidEventLMID.Value;

        DataSet dsMainResult = LmSystemLogBP.UserMainListSelect(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }
        hidHotelID.Value = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();

        lmlfog_order_num.Text = dsMainResult.Tables[0].Rows[0]["fog_order_num"].ToString();//订单ID
        lmlcreate_time.Text = dsMainResult.Tables[0].Rows[0]["create_time"].ToString();//创建时间
        lmlhotel_id.Text = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();//酒店        
        lmlhotel_name.Text = dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();
        lmlHotelSales.Text = " (销售人员:" + dsMainResult.Tables[0].Rows[0]["sales_account"].ToString() + ")";//酒店销售
        lmlcity_id.Text = dsMainResult.Tables[0].Rows[0]["city_id"].ToString();//城市
        lmlroom_type_name.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString();//房型
        lmlbook_room_num.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString();//预定间数
        lmlprice_code.Text = dsMainResult.Tables[0].Rows[0]["price_code"].ToString();//价格代码
        lmlbook_total_price.Text = dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString() + "元";//预订总价格
        lmlbook_price.Text = "(" + dsMainResult.Tables[0].Rows[0]["book_price"].ToString() + "元/间夜)";//单价
        string book_status = dsMainResult.Tables[0].Rows[0]["book_status"].ToString();
        string book_status_other = dsMainResult.Tables[0].Rows[0]["book_status_other"].ToString();
        if (dsMainResult.Tables[0].Rows[0]["priceCode"].ToString() == "LMBAR")//只有预付单显示
        {
            //0 新建 /1 入paycenter成功/2 失败/3超时/4取消单/5成功单/6 CC确认/7 CC取消/8入住中 已完成/9 离店/10NS（入住中和离店状态是根据离店时间自动变化的）
            lblBook_status_other.Text = dsMainResult.Tables[0].Rows[0]["BOOKSTATUS"].ToString().Replace("0", "新建").Replace("1", "入paycenter成功").Replace("2", "失败").Replace("3", "超时").Replace("4", "取消单").Replace("5", "成功单").Replace("6", "CC确认").Replace("7", "CC取消").Replace("8", "入住中(已完成)").Replace("9", "离店").Replace("10", "NO-SHOW");

            this.spanPayStatusOrMethod.Style.Add("display", "block");//显示
            this.lblPayStatusOrMethod.Style.Add("display", "block");//显示
            //this.btnRefund.Style.Add("display", "block");//显示
            if (dsMainResult.Tables[0].Rows[0]["paystatus"].ToString() == "10")
            {
                lblPayStatusOrMethod.InnerHtml = dsMainResult.Tables[0].Rows[0]["pay_status"].ToString() + "(" + dsMainResult.Tables[0].Rows[0]["pay_method"].ToString() + ")&nbsp; <input type=\"button\" runat=\"server\" id=\"btnRefund\" value=\"退款\" class=\"btn primary\" onclick=\"invokeOpenDiv()\" />"; //支付状态
            }
            else
            {
                lblPayStatusOrMethod.InnerHtml = dsMainResult.Tables[0].Rows[0]["pay_status"].ToString() + "(" + dsMainResult.Tables[0].Rows[0]["pay_method"].ToString() + ")"; //支付状态
            }
        }
        else
        {
            lblBook_status_other.Text = dsMainResult.Tables[0].Rows[0]["BookStatusOther"].ToString().Replace("0", "新建").Replace("1", "预订成功等待确认").Replace("2", "新建入fog失败").Replace("3", "用户取消").Replace("4", "可入住已确认").Replace("5", "NO-SHOW").Replace("6", "已完成（超过入住时间）").Replace("7", "审核中").Replace("8", "离店").Replace("9", "CC取消");

            this.spanPayStatusOrMethod.Style.Add("display", "none");//显示
            this.lblPayStatusOrMethod.Style.Add("display", "none");//显示
            //this.btnRefund.Style.Add("display", "none");//显示
        }

        lmluser_id.Text = dsMainResult.Tables[0].Rows[0]["user_id"].ToString();//用户ID
        lmllogin_mobile.Text = dsMainResult.Tables[0].Rows[0]["login_mobile"].ToString();//登录手机号
        lmlin_date.Text = dsMainResult.Tables[0].Rows[0]["in_date"].ToString();//入住日期
        lmlout_date.Text = dsMainResult.Tables[0].Rows[0]["out_date"].ToString();//离店日期
        lmlguest_names.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();//入住人
        lmlcontact_tel.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();//联系人方式


        lblOrder_channel.Text = dsMainResult.Tables[0].Rows[0]["order_channel"].ToString(); ; //订单渠道
        lmlapp_platform.Text = dsMainResult.Tables[0].Rows[0]["app_platform"].ToString();//订单平台
        lmisgua_name.Text = dsMainResult.Tables[0].Rows[0]["isguanm"].ToString();//担保
        lmlticket_amount.Text = dsMainResult.Tables[0].Rows[0]["ticket_amount"].ToString();//券金额
        lmlbreakfast_num.Text = dsMainResult.Tables[0].Rows[0]["breakfast_num"].ToString();//早餐
        lmlis_network.Text = dsMainResult.Tables[0].Rows[0]["is_network"].ToString();//wifi

        if (dsMainResult.Tables[0].Rows[0]["book_status_other"].ToString() == "8")
        {
            string roomID = GetRoomNumber(dsMainResult.Tables[0].Rows[0]["fog_order_num"].ToString());
            if (!string.IsNullOrEmpty(roomID))
            {
                lblRoomNumber.Text = "(房间号:" + roomID + ")";//房号   sql取    
            }
        }

        FogOrderNum = dsMainResult.Tables[0].Rows[0]["FOG_ORDER_NUM"].ToString();
        this.HidFogOrderNum.Value = FogOrderNum;
        GetOperateHis(dsMainResult.Tables[0].Rows[0]["order_num"].ToString());// GetOperateHis(FogOrderNum);
        GetShwoHisPlanInfoList(dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString(), dsMainResult.Tables[0].Rows[0]["create_time"].ToString(), dsMainResult.Tables[0].Rows[0]["priceCode"].ToString(), dsMainResult.Tables[0].Rows[0]["room_type_code"].ToString());
        //GetPayHis(FogOrderNum);

        //给Div赋值
        lblRefundMethod.Text = dsMainResult.Tables[0].Rows[0]["pay_method"].ToString();//退款方式
        HidPayMethod.Value = dsMainResult.Tables[0].Rows[0]["paymethod"].ToString();
        lblRefundMobile.Text = dsMainResult.Tables[0].Rows[0]["login_mobile"].ToString() == "" ? dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString() : dsMainResult.Tables[0].Rows[0]["login_mobile"].ToString();//退款手机号
        lblRefundName.Text = UserSession.Current.UserAccount; //退款人
        lblRefundAmount.Text = dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString();//退款金额
    }

    protected void gridViewCSSystemLogDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSSystemLogDetail.PageIndex = e.NewPageIndex;
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

    private string GetRoomNumber(string orderNum)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.FOG_ORDER_NUM = orderNum;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetCheckAppNSOrders(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult != null && dtResult.Rows.Count > 0 ? dtResult.Rows[0]["ROOM_ID"].ToString() : "";
    }

    public void GetShwoHisPlanInfoList(string strHotelID, string effectDate, string priceCode, string roomCode)
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        hotelInfoDBEntity.EffectDate = DateTime.Parse(effectDate).ToShortDateString().Replace("/", "-");
        hotelInfoDBEntity.PriceCode = priceCode;
        hotelInfoDBEntity.RoomCode = roomCode;

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

        DataSet dsResult = HotelInfoBP.GetShwoHisPlanInfoList(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            dvHisPlanInfoList.InnerHtml = "无酒店计划历史，请确认！";
            return;
        }

        StringBuilder sbString = new StringBuilder();
        string strTemp = string.Empty;
        string strMsg = string.Empty; //修改计划
        string strMeno = string.Empty;
        sbString.Append("<table cellspacing='0' cellpadding='0' width='100%' style='border:1px solid #D5D5D5'><tr class='GView_HeaderCSS' style='border-collapse:collapse'><td width='30%' style='white-space:nowrap; border: solid #D5D5D5 1px;'>操作人</td><td width='40%' style='white-space:nowrap; border: solid #D5D5D5 1px;'>操作时间</td><td width='30%' style='white-space:nowrap; border: solid #D5D5D5 1px;'>操作内容</td></tr>");

        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
        {
            strMsg = dsResult.Tables[0].Rows[i]["ope_msg"].ToString();
            strMeno = dsResult.Tables[0].Rows[i]["opememo"].ToString();

            if ("修改计划".Equals(strMeno))
            {
                if (strMsg.Contains("status") && "F".Equals(strMsg.Substring(strMsg.IndexOf("status") + 7, 1).Trim().ToUpper()))
                {
                    strMeno = "操作下线";
                }

                if (strMsg.Contains("isRoomful") && "1".Equals(strMsg.Substring(strMsg.IndexOf("isRoomful") + 10, 1).Trim().ToUpper()))
                {
                    strMeno = ("操作下线".Equals(strMeno)) ? "满房&下线" : "标记满房";
                }
            }

            strTemp = strTemp + "<tr id='" + "trHis" + i.ToString() + "' class='GView_ItemCSS' style='border-collapse:collapse' onclick=DvChangeEvent('" + "dvHis" + i.ToString() + "','" + "trHis" + i.ToString() + "')><td style='border:1px solid #D5D5D5'><font color='#3A599C'>" + dsResult.Tables[0].Rows[i]["operator"].ToString().PadRight(25, ' ') + "</font></td><td style='border:1px solid #D5D5D5'><font color='#3A599C'>" + dsResult.Tables[0].Rows[i]["opetime"].ToString() + "</font></td><td style='border:1px solid #D5D5D5'><font color='#3A599C'>" + strMeno + "</font></td></tr>";
            strTemp = strTemp + "<tr id='" + "dvHis" + i.ToString() + "' style='display:none;border:1px solid #D5D5D5;border-collapse:collapse;'><td colspan='3' style='border:1px solid #D5D5D5;' align='left'>" + dsResult.Tables[0].Rows[i]["ope_msg"].ToString() + "</td></tr>";
            sbString.Append(strTemp);
            strTemp = "";
            strMeno = "";
        }

        sbString.Append("</table>");
        dvHisPlanInfoList.InnerHtml = sbString.ToString();
    }

    public void GetOperateHis(string OrderNum)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("MEMO");//订单操作
        dtResult.Columns.Add("MSG");//订单状态
        dtResult.Columns.Add("OPERATOR");//操作人 
        dtResult.Columns.Add("EVENTTIME");//操作时间 
        dtResult.Columns.Add("REMARK");//操作备注
        dtResult.Columns.Add("operateresult");//操作结果
        DataRow row;

        DataTable Dsresult = BindViewCSSystemLogDetailORACLE(OrderNum);
        for (int i = 0; i < Dsresult.Rows.Count; i++)
        {
            row = dtResult.NewRow();
            row["MEMO"] = Dsresult.Rows[i]["MEMO"].ToString();
            row["MSG"] = Dsresult.Rows[i]["MSG"].ToString().Split(';');
            row["OPERATOR"] = Dsresult.Rows[i]["OPERATOR"].ToString();
            row["EVENTTIME"] = Dsresult.Rows[i]["EVENTTIME"].ToString();
            row["REMARK"] = Dsresult.Rows[i]["EVENTTYPE"].ToString();
            dtResult.Rows.Add(row);
        }

        dtResult.DefaultView.Sort = "EVENTTIME DESC ";
        dtResult = dtResult.DefaultView.ToTable();
        gridViewCSSystemLogDetail.DataSource = dtResult;
        gridViewCSSystemLogDetail.DataKeyNames = new string[] { "EVENTTIME" };//主键
        gridViewCSSystemLogDetail.DataBind();
    }
    private DataTable BindViewCSSystemLogDetailORACLE(string OrderNum)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = OrderNum;

        DataSet dsDetailResult = LmSystemLogBP.GetOperateHis(_lmSystemLogEntity).QueryResult;
        return dsDetailResult.Tables[0];
    }

    public void GetSQLOperateHis(string OrderNum)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("MEMO");//订单操作
        dtResult.Columns.Add("MSG");//订单状态
        dtResult.Columns.Add("OPERATOR");//操作人 
        dtResult.Columns.Add("EVENTTIME");//操作时间 
        dtResult.Columns.Add("REMARK");//操作备注
        dtResult.Columns.Add("operateresult");//操作结果
        DataRow row;

        DataTable SQLDsresult = BindViewCSSystemLogDetailSQL(OrderNum);
        for (int i = 0; i < SQLDsresult.Rows.Count; i++)
        {
            row = dtResult.NewRow();
            row["MEMO"] = SQLDsresult.Rows[i]["EVENT_TYPE"].ToString();
            row["MSG"] = SQLDsresult.Rows[i]["OD_STATUS"].ToString().Split(';');
            row["OPERATOR"] = SQLDsresult.Rows[i]["EVENT_USER"].ToString();
            row["EVENTTIME"] = SQLDsresult.Rows[i]["EVENT_TIME"].ToString();
            row["REMARK"] = SQLDsresult.Rows[i]["REMARK"].ToString();
            row["operateresult"] = SQLDsresult.Rows[i]["OperateResult"].ToString();
            dtResult.Rows.Add(row);
        }

        dtResult.DefaultView.Sort = "EVENTTIME DESC ";
        dtResult = dtResult.DefaultView.ToTable();
        gridViewCSSystemLogDetail.DataSource = dtResult;
        gridViewCSSystemLogDetail.DataKeyNames = new string[] { "EVENTTIME" };//主键
        gridViewCSSystemLogDetail.DataBind();
    }

    private DataTable BindViewCSSystemLogDetailSQL(string FogOrderNum)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.MemoKey = FogOrderNum;

        DataSet dsDetailResult = LmSystemLogBP.OrderActionHis(_lmSystemLogEntity).QueryResult;
        return dsDetailResult.Tables[0];
    }

    protected void btnDivRenewPlan_Click(object sender, EventArgs e)
    {
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _commonEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _commonEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        _OrderRefundEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _OrderRefundEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _OrderRefundEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _OrderRefundEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _OrderRefundEntity.OrderRefundDBEntity = new List<OrderRefundDBEntity>();
        OrderRefundDBEntity orderRefundDBEntity = new OrderRefundDBEntity();

        orderRefundDBEntity.Obj_id = FogOrderNum == "" ? this.HidFogOrderNum.Value : FogOrderNum;
        orderRefundDBEntity.Amount = lblRefundAmount.Text;//退款金额
        orderRefundDBEntity.Operators = lblRefundName.Text;//退款人
        orderRefundDBEntity.Remark = txtRefundRemark.InnerText;//退款备注
        //orderRefundDBEntity.Type = lblRefundMethod.Text;//退款方式
        orderRefundDBEntity.Type = HidPayMethod.Value;//退款方式
        orderRefundDBEntity.Create_time = System.DateTime.Now.ToString();
        orderRefundDBEntity.Sn = lblRefundEntryNumber.Text;//流水号
        orderRefundDBEntity.Refund_account = lblRefundAccount.Text; //退款账号
        orderRefundDBEntity.Refund_time = this.lblRefundTime.Value;//退款时间

        _OrderRefundEntity.OrderRefundDBEntity.Add(orderRefundDBEntity);
        _OrderRefundEntity = OrderInfoBP.saveRefund(_OrderRefundEntity);

        commonDBEntity.Event_Type = "订单操作-退款";
        commonDBEntity.Event_ID = FogOrderNum == "" ? this.HidFogOrderNum.Value : FogOrderNum;
        string conTent = "退款金额:" + lblRefundAmount.Text + ",退款人:" + lblRefundName.Text + ",退款备注:" + txtRefundRemark.InnerText + ",退款方式:" + lblRefundMethod.Text + ",流水号:" + lblRefundEntryNumber.Text + ",退款账号:" + lblRefundAccount.Text + ",退款时间" + lblRefundTime.Value;
        commonDBEntity.Event_Content = conTent;

        commonDBEntity.Event_Result = "MSG:" + _OrderRefundEntity.ErrorMSG + ",Result:" + _OrderRefundEntity.Result.ToString();
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        ScriptManager.RegisterStartupScript(this.UpdatePanel10, this.GetType(), "keyclosebtn", "BtnCompleteStyle();", true);

        ScriptManager.RegisterStartupScript(this.UpdatePanel10, this.GetType(), "keyclosediv", "invokeCloseDiv();", true);
    }
}