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

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class WebUI_Order_OrderOperateStatus : System.Web.UI.Page
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BandDDpList();
            SettingControlVal();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "loadhideButton", "hideButton();", true);
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnlock_Click(object sender, EventArgs e)
    {
        string fogOrderNum = this.txtFogOrderNum.Text.Trim();
        ViewState["HidFogOrderNum"] = this.txtFogOrderNum.Text.Trim();
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.FOG_ORDER_NUM = fogOrderNum;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.OrderOperateStatus(_orderInfoEntity).QueryResult.Tables[0];

        //如果是LMBAR的价格代码  直接跳过  提示不能改状态
        if (dtResult != null && dtResult.Rows.Count > 0)
        {
            //if (dtResult.Rows[0]["PRICE_CODE"].ToString().Trim().Equals("LMBAR"))
            //{
            //    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            //    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "alertScript", "alert('预付订单不能修改订单状态!');", true);
            //    return;
            //}

            this.txtHotelName.Text = dtResult.Rows[0]["HOTEL_NAME"].ToString();
            this.txtPriceCode.Text = dtResult.Rows[0]["PRICE_CODE"].ToString();
            spanPriceCode.InnerHtml = dtResult.Rows[0]["PRICE_CODE"].ToString();
            this.txtLoginMobile.Text = dtResult.Rows[0]["LOGIN_MOBILE"].ToString();
            this.txtCreateTime.Text = dtResult.Rows[0]["CREATE_TIME"].ToString();
            this.txtGuestNames.Text = dtResult.Rows[0]["GUEST_NAMES"].ToString();
            this.txtVender.Text = dtResult.Rows[0]["vendor"].ToString();
            this.txtRoomName.Text = dtResult.Rows[0]["ROOM_TYPE_NAME"].ToString();
            this.txtBookTotalPrice.Text = dtResult.Rows[0]["BOOK_TOTAL_PRICE"].ToString();
            string IN_DATE = dtResult.Rows[0]["IN_DATE"].ToString() == "" ? "" : DateTime.Parse(dtResult.Rows[0]["IN_DATE"].ToString()).ToShortDateString();
            string OUT_DATE = dtResult.Rows[0]["OUT_DATE"].ToString() == "" ? "" : DateTime.Parse(dtResult.Rows[0]["OUT_DATE"].ToString()).ToShortDateString();
            this.txtINOrOutDate.Text = IN_DATE + "--" + OUT_DATE;
            this.txtPromotionAmount.Text = dtResult.Rows[0]["PROMOTION_AMOUNT"].ToString();

            if (!dtResult.Rows[0]["PRICE_CODE"].ToString().Trim().Equals("LMBAR"))
            {
                this.lblHVPStatus.Text = ReturnBookStatusOther(dtResult.Rows[0]["book_status_other"].ToString());
            }
            else
            {
                if (dropStatus.Items.FindByValue("18") == null)
                    dropStatus.Items.Add(new ListItem("已完成", "18"));//特别标记
                this.lblHVPStatus.Text = ReturnBookStatus(dtResult.Rows[0]["book_status"].ToString());
            }
            this.lblVendorStatus.Text = GetVenderStatus(fogOrderNum);

            GetOperateHis(dtResult.Rows[0]["order_num"].ToString());

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "loadlowButton", "lowButton();", true);
        }
        else
        {
            this.txtHotelName.Text = "";
            this.txtPriceCode.Text = "";
            this.txtLoginMobile.Text = "";
            this.txtCreateTime.Text = "";
            this.txtGuestNames.Text = "";
            this.txtVender.Text = "";
            this.txtRoomName.Text = "";
            this.txtBookTotalPrice.Text = "";
            this.txtINOrOutDate.Text = "";
            this.txtPromotionAmount.Text = "";
        }
        this.UpdatePanel1.Update();
        //this.UpdatePanel2.Update();
        //this.UpdatePanel3.Update();
        this.UpdatePanel6.Update();


        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
    }

    /// <summary>
    /// 根据订单号获取当前供应商状态
    /// </summary>
    /// <param name="fogOrderNum"></param>
    /// <returns></returns>
    public string GetVenderStatus(string fogOrderNum)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.FOG_ORDER_NUM = fogOrderNum;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        string Result = OrderInfoBP.orderQueryByOrderNum(_orderInfoEntity).ErrorMSG;

        return Result;
    }


    /// <summary>
    /// 确认修改 （修改借口 sql日志） 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        //接口 
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        orderinfoEntity.EventType = "订单状态操作";
        orderinfoEntity.FOG_ORDER_NUM = ViewState["HidFogOrderNum"].ToString();
        orderinfoEntity.ActionID = "";//订单确认ID
        if (txtPriceCode.Text == "LMBAR")
        {
            //<option value="8">离店</option>
            //<option value="5">No-Show</option>
            //<option value="9">CC取消</option>

            this.dropStatus.Items[dropStatus.SelectedIndex].Value.Replace("9", "7").Replace("8", "9").Replace("5", "10").Replace("18", "8");
            if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "9")
                orderinfoEntity.BOOK_STATUS_OTHER = "7";
            if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "8")
                orderinfoEntity.BOOK_STATUS_OTHER = "5";
            if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "5")
                orderinfoEntity.BOOK_STATUS_OTHER = "10";
            if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "18")
                orderinfoEntity.BOOK_STATUS_OTHER = "8";
        }
        else
        {
            orderinfoEntity.BOOK_STATUS_OTHER = this.dropStatus.Items[dropStatus.SelectedIndex].Value;
        }

        orderinfoEntity.TICKET_AMOUNT = this.ddlReturnTicket.Items[ddlReturnTicket.SelectedIndex].Value;
        orderinfoEntity.REMARK = this.txtRemark.InnerText;//备注
        if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "9")
        {
            orderinfoEntity.CanelReson = ddpCanelReson.SelectedValue;//CC取消原因 
        }
        else if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "8")
        {   
            orderinfoEntity.ApproveId = txtAffirmNum.Text;//离店  确认号 
            orderinfoEntity.ROOM_TYPE_CODE = txtINRoomNum.Text;        //离店 房间号 
        }
        else if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "5")
        {
            orderinfoEntity.CanelReson = ddpNoShow.SelectedValue;//No-Show 原因     
        }

        orderinfoEntity.USER_ID = UserSession.Current.UserAccount;

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        string Result = OrderInfoBP.updateIssueOrder(_orderInfoEntity).ErrorMSG;


        //  sql库 
        int i = OrderInfoBP.InsertOrderActionHisList(_orderInfoEntity, Result).Result;

        GetOperateHis(ViewState["HidFogOrderNum"].ToString());

        btnlock_Click(null, null);

        this.dropStatus.SelectedIndex = 0;
        this.txtRemark.InnerHtml = "";

        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "updateScript", "BtnCompleteStyle();", true);

        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "alertScript", "alert('" + Result + "!');", true);
    }

    public void GetOperateHis(string FogOrderNum)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("MEMO");//订单操作
        dtResult.Columns.Add("MSG");//订单状态
        dtResult.Columns.Add("OPERATOR");//操作人 
        dtResult.Columns.Add("EVENTTIME");//操作时间 
        dtResult.Columns.Add("REMARK");//操作备注
        dtResult.Columns.Add("operateresult");//操作结果
        DataRow row;

        //DataTable SQLDsresult = BindViewCSSystemLogDetailSQL(FogOrderNum);
        //for (int i = 0; i < SQLDsresult.Rows.Count; i++)
        //{
        //    row = dtResult.NewRow();
        //    row["MEMO"] = SQLDsresult.Rows[i]["EVENT_TYPE"].ToString();
        //    row["MSG"] = SQLDsresult.Rows[i]["OD_STATUS"].ToString();
        //    row["OPERATOR"] = SQLDsresult.Rows[i]["EVENT_USER"].ToString();
        //    row["EVENTTIME"] = SQLDsresult.Rows[i]["EVENT_TIME"].ToString();
        //    row["REMARK"] = SQLDsresult.Rows[i]["REMARK"].ToString();
        //    row["operateresult"] = SQLDsresult.Rows[i]["operateresult"].ToString();
        //    dtResult.Rows.Add(row);
        //}
        DataTable OracleDsresult = BindViewCSSystemLogDetailORACLE(FogOrderNum);
        for (int i = 0; i < OracleDsresult.Rows.Count; i++)
        {
            row = dtResult.NewRow();
            row["MEMO"] = OracleDsresult.Rows[i]["MEMO"].ToString();
            string[] other = OracleDsresult.Rows[i]["MSG"].ToString().Split(';');
            string bookStatusOther = "";
            for (int j = 0; j < other.Length; j++)
            {
                if (other[j].Contains("bookStatusOther"))
                {
                    bookStatusOther = other[j].Split('=')[1].ToString().Replace("0", "新建").Replace("1", "预订成功等待确认").Replace("2", "新建入fog失败").Replace("3", "用户取消").Replace("4", "可入住已确认").Replace("5", "NO-SHOW").Replace("6", "已完成").Replace("7", "审核中").Replace("8", "离店").Replace("9", "CC取消");
                }
            }
            row["MSG"] = bookStatusOther;
            row["OPERATOR"] = OracleDsresult.Rows[i]["OPERATOR"].ToString();
            row["EVENTTIME"] = OracleDsresult.Rows[i]["EVENTTIME"].ToString();
            row["REMARK"] = OracleDsresult.Rows[i]["MSG"].ToString(); ;
            row["operateresult"] = "";
            dtResult.Rows.Add(row);
        }

        dtResult.DefaultView.Sort = "EVENTTIME DESC ";
        dtResult = dtResult.DefaultView.ToTable();
        gridViewCSSystemLogDetail.DataSource = dtResult;
        gridViewCSSystemLogDetail.DataKeyNames = new string[] { "EVENTTIME" };//主键
        gridViewCSSystemLogDetail.DataBind();
        this.UpdatePanel4.Update();
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


    private DataTable BindViewCSSystemLogDetailORACLE(string FogOrderNum)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = FogOrderNum;

        DataSet dsDetailResult = LmSystemLogBP.GetOperateHis(_lmSystemLogEntity).QueryResult;
        return dsDetailResult.Tables[0];
    }

    #region No-Show
    private void BandDDpList()
    {
        DataTable dtSortType = GetCannelTypeData();
        ddpNoShow.DataSource = dtSortType;
        ddpNoShow.DataTextField = "SORT_TEXT";
        ddpNoShow.DataValueField = "SORT_STATUS";
        ddpNoShow.DataBind();
        ddpNoShow.SelectedIndex = 0;
    }

    private DataTable GetCannelTypeData()
    {
        DataTable dt = new DataTable();
        DataColumn SortStatus = new DataColumn("SORT_STATUS");
        DataColumn SortStatusText = new DataColumn("SORT_TEXT");
        dt.Columns.Add(SortStatus);
        dt.Columns.Add(SortStatusText);

        for (int i = 0; i < 11; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["SORT_STATUS"] = "行程变更";
                    dr["SORT_TEXT"] = "行程变更";
                    break;
                case "1":
                    dr["SORT_STATUS"] = "超时到店，酒店满房";
                    dr["SORT_TEXT"] = "超时到店，酒店满房";
                    break;
                case "2":
                    dr["SORT_STATUS"] = "客人入住，但未按协议价入住";
                    dr["SORT_TEXT"] = "客人入住，但未按协议价入住";
                    break;
                case "3":
                    dr["SORT_STATUS"] = "客人抵达酒店，但未入住";
                    dr["SORT_TEXT"] = "客人抵达酒店，但未入住";
                    break;
                case "4":
                    dr["SORT_STATUS"] = "客人未收到确认sms或延时";
                    dr["SORT_TEXT"] = "客人未收到确认sms或延时";
                    break;
                case "5":
                    dr["SORT_STATUS"] = "联系不到客人";
                    dr["SORT_TEXT"] = "联系不到客人";
                    break;
                case "6":
                    dr["SORT_STATUS"] = "重复预订";
                    dr["SORT_TEXT"] = "重复预订";
                    break;
                case "7":
                    dr["SORT_STATUS"] = "客人未找到酒店";
                    dr["SORT_TEXT"] = "客人未找到酒店";
                    break;
                case "8":
                    dr["SORT_STATUS"] = "客人反馈未有预订";
                    dr["SORT_TEXT"] = "客人反馈未有预订";
                    break;
                case "9":
                    dr["SORT_STATUS"] = "保留时间内到店，酒店满房";
                    dr["SORT_TEXT"] = "保留时间内到店，酒店满房";
                    break;
                case "10":
                    dr["SORT_STATUS"] = "其他";
                    dr["SORT_TEXT"] = "其他";
                    break;
                default:
                    dr["SORT_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
    #endregion

    #region CC取消
    private void SettingControlVal()
    {
        DataTable dtCanelReson = GetCanelResonData();
        ddpCanelReson.DataTextField = "CanelResonDis";
        ddpCanelReson.DataValueField = "CanelReson";
        ddpCanelReson.DataSource = dtCanelReson;
        ddpCanelReson.DataBind();
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
    #endregion


    protected void gridViewCSSystemLogDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public string ReturnBookStatusOther(string str)
    {
        string status = "";
        switch (str)
        {
            case "0":
                status = "新建";
                break;
            case "1":
                status = "预订成功等待确认";
                break;
            case "2":
                status = "新建入fog失败";
                break;
            case "3":
                status = "用户取消";
                break;
            case "4":
                status = "可入住已确认";
                break;
            case "5":
                status = "NO-SHOW";
                break;
            case "6":
                status = "已完成";
                break;
            case "7":
                status = "审核中";
                break;
            case "8":
                status = "离店";
                break;
            default:
                status = "CC取消";
                break;
        }
        return status;
    }

    public string ReturnBookStatus(string str)
    {
        string status = "";
        switch (str)
        {
            case "0":
                status = "新建";
                break;
            case "1":
                status = "入paycenter成功";
                break;
            case "2":
                status = "失败";
                break;
            case "3":
                status = "超时";
                break;
            case "4":
                status = "取消单";
                break;
            case "5":
                status = "成功单";
                break;
            case "6":
                status = "CC确认";
                break;
            case "7":
                status = "CC取消";
                break;
            case "8":
                status = "入住中(已完成)";
                break;
            case "9":
                status = "离店";
                break;
            default:
                status = "NO-SHOW";
                break;
        }
        return status;
    }

}