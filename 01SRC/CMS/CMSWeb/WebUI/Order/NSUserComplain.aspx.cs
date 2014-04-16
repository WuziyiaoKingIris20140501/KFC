using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Text;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process.Order;
using HotelVp.CMS.Domain.Entity.Order;
using System.Configuration;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Process;

public partial class WebUI_Order_NSUserComplain : System.Web.UI.Page
{
    OrderFeedbackEntity _orderFeedbackEntity = new OrderFeedbackEntity();
    OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
    public static LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrderNum"] = "";
            ViewState["CreateTimeStart"] = "";
            ViewState["CreateTimeEnd"] = "";
            ViewState["UpdateTimeStart"] = "";
            ViewState["UpdateTimeEnd"] = "";
            ViewState["IsProcess"] = "";

            BindchkProcess();
            BandDDpList();
            SettingControlVal();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["OrderNum"] = txtOrderNum.Text;
        ViewState["CreateTimeStart"] = orderCreateStart.Value.Replace("/", "-");
        ViewState["CreateTimeEnd"] = orderCreateEnd.Value.Replace("/", "-");
        ViewState["UpdateTimeStart"] = orderUpdateStart.Value.Replace("/", "-");
        ViewState["UpdateTimeEnd"] = orderUpdateEnd.Value.Replace("/", "-");
        ViewState["IsProcess"] = this.rdoProcess.SelectedValue.Trim();


        gridViewCSList.EditIndex = -1;
        BindPartnerListGrid();

        this.UpdatePanel3.Update();
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
    }

    private void BindPartnerListGrid()
    {
        _orderFeedbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderFeedbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderFeedbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderFeedbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderFeedbackEntity.orderFeedbackDBEntity = new List<OrderFeedbackDBEntity>();
        OrderFeedbackDBEntity orderinfoEntity = new OrderFeedbackDBEntity();

        orderinfoEntity.OrderNum = ViewState["OrderNum"].ToString();
        orderinfoEntity.CreateTimeStart = ViewState["CreateTimeStart"].ToString();
        orderinfoEntity.CreateTimeEnd = ViewState["CreateTimeEnd"].ToString();
        orderinfoEntity.UpdateTimeStart = ViewState["UpdateTimeStart"].ToString();
        orderinfoEntity.UpdateTimeEnd = ViewState["UpdateTimeEnd"].ToString();
        orderinfoEntity.IsProcess = ViewState["IsProcess"].ToString();

        _orderFeedbackEntity.orderFeedbackDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderFeedbackBP.BindOrderFeedBackList(_orderFeedbackEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "ORDER_NUM" };//主键
        gridViewCSList.DataBind();
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
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E9EBF2'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }

    protected void gridViewCSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSList.PageIndex = e.NewPageIndex;
        BindPartnerListGrid();
    }

    private void BindchkProcess()
    {
        DataTable dtchkProcess = new DataTable();
        dtchkProcess.Columns.Add("PROCESS_TEXT");
        dtchkProcess.Columns.Add("PROCESS_STATUS");

        for (int i = 0; i < 3; i++)
        {
            DataRow row = dtchkProcess.NewRow();
            switch (i)
            {
                case 0:
                    row["PROCESS_TEXT"] = "不限制";
                    row["PROCESS_STATUS"] = "-1";
                    break;
                case 1:
                    row["PROCESS_TEXT"] = "已处理";
                    row["PROCESS_STATUS"] = "1";
                    break;
                default:
                    row["PROCESS_TEXT"] = "未处理";
                    row["PROCESS_STATUS"] = "";
                    break;
            }
            dtchkProcess.Rows.Add(row);
        }

        rdoProcess.DataSource = dtchkProcess;
        rdoProcess.DataTextField = "PROCESS_TEXT";
        rdoProcess.DataValueField = "PROCESS_STATUS";
        rdoProcess.DataBind();
        rdoProcess.Items[0].Selected = true;
    }

    protected void btnDetails_Click(object sender, EventArgs e)
    {
        _orderFeedbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderFeedbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderFeedbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderFeedbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderFeedbackEntity.orderFeedbackDBEntity = new List<OrderFeedbackDBEntity>();
        OrderFeedbackDBEntity orderinfoEntity = new OrderFeedbackDBEntity();

        orderinfoEntity.OrderNum = this.hidOrderNum.Value;
        this.lblOrderNum.Text = this.hidOrderNum.Value;

        _orderFeedbackEntity.orderFeedbackDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderFeedbackBP.BindOrderDetailsByOrderNum(_orderFeedbackEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            this.lblHotelName.Text = dsResult.Tables[0].Rows[0]["HOTEL_NAME"].ToString();//预定酒店
            this.lblPriceCode.Text = dsResult.Tables[0].Rows[0]["PRICE_CODE"].ToString();//价格代码
            this.lblHotelLinkMan.Text = dsResult.Tables[0].Rows[0]["sales_account"].ToString();//酒店负责销售 

            this.lblPhone.Text = dsResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString();//预订人号码
            this.lblCreateTime.Text = dsResult.Tables[0].Rows[0]["CREATE_TIME"].ToString();//订单创建日期
            this.lblHotelLinkTel.Text = GetUserTel(dsResult.Tables[0].Rows[0]["sales_account"].ToString());//酒店销售电话 


            this.lblGuestName.Text = dsResult.Tables[0].Rows[0]["GUEST_NAMES"].ToString();//入住人姓名
            this.lblVendor.Text = dsResult.Tables[0].Rows[0]["vendor"].ToString();//酒店供应商
            if (dsResult.Tables[0].Rows[0]["OrderAffirmLinkTel"].ToString() != "" && dsResult.Tables[0].Rows[0]["OrderAffirmLinkTel"].ToString().Split('|').Length >= 23)
            {
                this.lblhotelTel.Text = dsResult.Tables[0].Rows[0]["OrderAffirmLinkTel"].ToString().Split('|')[System.DateTime.Now.Hour];//酒店电话 
            }
            else
            {
                this.lblhotelTel.Text = "";
            }


            this.lblRoomName.Text = dsResult.Tables[0].Rows[0]["ROOM_TYPE_NAME"].ToString();//房型名称
            this.lblBookTotalPrice.Text = dsResult.Tables[0].Rows[0]["BOOK_TOTAL_PRICE"].ToString();//订单总金额
            if (dsResult.Tables[0].Rows[0]["OrderVerifyLinkTel"].ToString() != "" && dsResult.Tables[0].Rows[0]["OrderVerifyLinkTel"].ToString().Split('|').Length >= 23)
            {
                this.lblReviewTel.Text = dsResult.Tables[0].Rows[0]["OrderVerifyLinkTel"].ToString().Split('|')[System.DateTime.Now.Hour];//审核电话  
            }
            else
            {
                this.lblReviewTel.Text = "";
            }

            string IN_DATE = dsResult.Tables[0].Rows[0]["IN_DATE"].ToString() == "" ? "" : DateTime.Parse(dsResult.Tables[0].Rows[0]["IN_DATE"].ToString()).ToShortDateString();
            string OUT_DATE = dsResult.Tables[0].Rows[0]["OUT_DATE"].ToString() == "" ? "" : DateTime.Parse(dsResult.Tables[0].Rows[0]["OUT_DATE"].ToString()).ToShortDateString();
            this.lblInOrOutDate.Text = IN_DATE + "--" + OUT_DATE; //入住 - 离店
            this.lblTicketCode.Text = dsResult.Tables[0].Rows[0]["PROMOTION_AMOUNT"].ToString();//返现券金额


            this.hidBookStatusOther.Value = dsResult.Tables[0].Rows[0]["book_status_other"].ToString();
            this.lblBookStatusOther.Text = dsResult.Tables[0].Rows[0]["book_status_other"].ToString().Replace("0", "新建").Replace("1", "预订成功等待确认").Replace("2", "新建入fog失败").Replace("3", "用户取消").Replace("4", "可入住已确认").Replace("5", "NO-SHOW").Replace("6", "已完成").Replace("7", "审核中").Replace("8", "离店").Replace("9", "CC取消"); //当前HVP状态
            this.lblVendorStatus.Text = GetVenderStatus(this.hidOrderNum.Value);//当前供应商状态
            this.lblNSRoomNum.Text = this.hidContent.Value;//用户申诉房号
            this.txtINRoomNum.Text = this.hidContent.Value;
        }
        else
        {
            this.lblHotelName.Text = "";//预定酒店
            this.lblPriceCode.Text = "";//价格代码
            this.lblHotelLinkMan.Text = "";//酒店负责销售 

            this.lblPhone.Text = "";//预订人号码
            this.lblCreateTime.Text = "";//订单创建日期
            this.lblHotelLinkTel.Text = "";//酒店销售电话 

            this.lblGuestName.Text = "";//入住人姓名
            this.lblVendor.Text = "";//酒店供应商
            this.lblhotelTel.Text = "";//酒店电话 


            this.lblRoomName.Text = "";//房型名称
            this.lblBookTotalPrice.Text = "";//订单总金额
            this.lblReviewTel.Text = "";//审核电话 

            this.lblInOrOutDate.Text = "";//入住 - 离店
            this.lblTicketCode.Text = "";//返现券金额



            this.lblRoomName.Text = "";//当前HVP状态
            this.lblBookTotalPrice.Text = "";//当前供应商状态
            this.lblReviewTel.Text = "";//用户申诉房号
        }

        ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "setScript", "invokeOpenList()", true);
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
    /// 获取酒店销售人员 电话
    /// </summary>
    /// <param name="userAccount"></param>
    /// <returns></returns>
    public string GetUserTel(string userAccount)
    {
        _orderFeedbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderFeedbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderFeedbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderFeedbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderFeedbackEntity.orderFeedbackDBEntity = new List<OrderFeedbackDBEntity>();
        OrderFeedbackDBEntity orderinfoEntity = new OrderFeedbackDBEntity();

        _orderFeedbackEntity.orderFeedbackDBEntity.Add(orderinfoEntity);
        string Result = OrderFeedbackBP.GetUserTel(_orderFeedbackEntity, userAccount);

        return Result;
    }


    /// <summary>
    /// 确认修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnVerifyUpdate_Click(object sender, EventArgs e)
    {
        _orderFeedbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderFeedbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderFeedbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderFeedbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderFeedbackEntity.orderFeedbackDBEntity = new List<OrderFeedbackDBEntity>();
        OrderFeedbackDBEntity orderFeedbackDBEntity = new OrderFeedbackDBEntity();


        //接口 
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        //orderinfoEntity.EventType = "NS订单申诉";
        orderinfoEntity.FOG_ORDER_NUM = this.hidOrderNum.Value;
        orderinfoEntity.BOOK_STATUS = this.dropStatus.Items[dropStatus.SelectedIndex].Text;
        orderinfoEntity.BOOK_STATUS_OTHER = this.dropStatus.Items[dropStatus.SelectedIndex].Value;

        orderinfoEntity.REMARK = this.txtRemark.InnerText;//备注

        if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "8")//离店
        {
            orderinfoEntity.ApproveId = txtAffirmNum.Text;//离店  确认号 
            orderinfoEntity.ROOM_TYPE_CODE = txtINRoomNum.Text;        //离店 房间号 
            orderinfoEntity.EventType = "订单审核";
        }
        else if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "5")//No-Show
        {
            orderinfoEntity.EventType = "订单审核";
            orderinfoEntity.CanelReson = ddpNoShow.SelectedValue;//No-Show 原因
        }
        else if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "3")//用户取消
        {
            orderinfoEntity.EventType = "订单确认";
        }
        else if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "9")//CC取消
        {
            orderinfoEntity.EventType = "订单确认";
            orderinfoEntity.CanelReson = ddpCanelReson.SelectedValue;//CC取消原因 
        }
        else
        {
            orderinfoEntity.EventType = "订单审核";
            orderinfoEntity.BOOK_STATUS = this.hidBookStatusOther.Value;
            orderinfoEntity.BOOK_STATUS_OTHER = this.lblBookStatusOther.Text;
            orderinfoEntity.REMARK = "确认不修改" + this.txtRemark.InnerText;//备注
        }
        orderinfoEntity.IsDbApprove = "1";
        orderinfoEntity.USER_ID = UserSession.Current.UserAccount;

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        _orderInfoEntity = OrderInfoBP.updateIssueOrder(_orderInfoEntity);
        string Result = _orderInfoEntity.Result.ToString();
        string ErrMSG = _orderInfoEntity.ErrorMSG;
        //  sql库 
        int i = OrderInfoBP.InsertOrderActionHisList(_orderInfoEntity, _orderInfoEntity.ErrorMSG).Result;

        if (Result != "1")
        {
            btnDetails_Click(null, null);
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "alertScript", "alert('" + ErrMSG + "!');", true);
            return;
        }
        else
        {
            orderFeedbackDBEntity.OrderNum = this.hidOrderNum.Value;
            orderFeedbackDBEntity.IsProcess = "1";//已处理
            orderFeedbackDBEntity.OperatorZH1 = UserSession.Current.UserAccount;//处理人
            orderFeedbackDBEntity.UpdateTimeStart = System.DateTime.Now.ToString().Replace("/", "-");
            orderFeedbackDBEntity.Content = this.dropStatus.Items[dropStatus.SelectedIndex].Value;
            _orderFeedbackEntity.orderFeedbackDBEntity.Add(orderFeedbackDBEntity);
            OrderFeedbackBP.UpdateOrderFeedBack(_orderFeedbackEntity);

            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "setScript", "invokeCloseList()", true);

            btnSearch_Click(null, null);

            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "alertMsg", "alert('操作成功!');", true);
        }
    }

    /// <summary>
    /// 确认不修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnVerifyNotUpdate_Click(object sender, EventArgs e)
    {
        _orderFeedbackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderFeedbackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderFeedbackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderFeedbackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderFeedbackEntity.orderFeedbackDBEntity = new List<OrderFeedbackDBEntity>();
        OrderFeedbackDBEntity orderFeedbackDBEntity = new OrderFeedbackDBEntity();

        //接口 
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        //orderinfoEntity.EventType = "NS订单申诉";
        orderinfoEntity.FOG_ORDER_NUM = this.hidOrderNum.Value;

        orderinfoEntity.BOOK_STATUS = this.dropStatus.Items[dropStatus.SelectedIndex].Text;
        orderinfoEntity.BOOK_STATUS_OTHER = this.dropStatus.Items[dropStatus.SelectedIndex].Value;

        orderinfoEntity.REMARK = this.txtRemark.InnerText;//备注
        if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "8")//离店
        {
            orderinfoEntity.ApproveId = txtAffirmNum.Text;//离店  确认号 
            orderinfoEntity.ROOM_TYPE_CODE = txtINRoomNum.Text;        //离店 房间号 
            orderinfoEntity.EventType = "订单审核";
        }
        else if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "5")//No-Show
        {
            orderinfoEntity.EventType = "订单审核";
            orderinfoEntity.CanelReson = ddpNoShow.SelectedValue;//No-Show 原因
        }
        else if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "3")//用户取消
        {
            orderinfoEntity.EventType = "订单确认";
        }
        else if (this.dropStatus.Items[dropStatus.SelectedIndex].Value == "9")//CC取消
        {
            orderinfoEntity.EventType = "订单确认";
            orderinfoEntity.CanelReson = ddpCanelReson.SelectedValue;//CC取消原因 
        }
        else
        {
            orderinfoEntity.EventType = "订单审核";
            orderinfoEntity.BOOK_STATUS = this.hidBookStatusOther.Value;
            orderinfoEntity.BOOK_STATUS_OTHER = this.lblBookStatusOther.Text;
            orderinfoEntity.REMARK = "确认不修改" + this.txtRemark.InnerText;//备注
        }
        orderinfoEntity.IsDbApprove = "1";
        orderinfoEntity.USER_ID = UserSession.Current.UserAccount;

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        //string Result = OrderInfoBP.updateIssueOrder(_orderInfoEntity).ErrorMSG;

        orderFeedbackDBEntity.OrderNum = this.hidOrderNum.Value;
        orderFeedbackDBEntity.IsProcess = "1";//已处理
        orderFeedbackDBEntity.OperatorZH1 = UserSession.Current.UserAccount;//处理人
        orderFeedbackDBEntity.UpdateTimeStart = System.DateTime.Now.ToString();
        orderFeedbackDBEntity.Content = this.dropStatus.Items[dropStatus.SelectedIndex].Value;
        _orderFeedbackEntity.orderFeedbackDBEntity.Add(orderFeedbackDBEntity);
        OrderFeedbackBP.UpdateOrderFeedBack(_orderFeedbackEntity);

        //  sql库 
        int i = OrderInfoBP.InsertOrderActionHisList(_orderInfoEntity, "").Result;

        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "setScript", "invokeCloseList()", true);

        btnSearch_Click(null, null);

        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "alertMsg", "alert('操作成功!');", true);
    }

    #region
    /// <summary>
    /// 获取操作历史
    /// </summary>
    /// <param name="FogOrderNum"></param>
    [WebMethod]
    public static string GetOperateHis(string FogOrderNum)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("MEMO");//订单操作
        dtResult.Columns.Add("MSG");//订单状态
        dtResult.Columns.Add("OPERATOR");//操作人 
        dtResult.Columns.Add("EVENTTIME");//操作时间 
        dtResult.Columns.Add("REMARK");//操作备注
        dtResult.Columns.Add("operateresult");//操作结果
        DataRow row;
        string json = "";
        if (!FogOrderNum.Equals("undefined"))
        {
            DataTable SQLDsresult = BindViewCSSystemLogDetailSQL(FogOrderNum);
            for (int i = 0; i < SQLDsresult.Rows.Count; i++)
            {
                row = dtResult.NewRow();
                row["MEMO"] = SQLDsresult.Rows[i]["EVENT_TYPE"].ToString();
                row["MSG"] = SQLDsresult.Rows[i]["OD_STATUS"].ToString().Replace("0", "新建").Replace("1", "预订成功等待确认").Replace("2", "新建入fog失败").Replace("3", "用户取消").Replace("4", "可入住已确认").Replace("5", "NO-SHOW").Replace("6", "已完成").Replace("7", "审核中").Replace("8", "离店").Replace("9", "CC取消"); ;
                row["OPERATOR"] = SQLDsresult.Rows[i]["EVENT_USER"].ToString();
                row["EVENTTIME"] = SQLDsresult.Rows[i]["EVENT_TIME"].ToString();
                row["REMARK"] = SQLDsresult.Rows[i]["REMARK"].ToString();
                row["operateresult"] = SQLDsresult.Rows[i]["operateresult"].ToString();
                dtResult.Rows.Add(row);
            }
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
                row["REMARK"] = "";
                row["operateresult"] = "";
                dtResult.Rows.Add(row);
            }

            dtResult.DefaultView.Sort = "EVENTTIME DESC ";
            dtResult = dtResult.DefaultView.ToTable();

            try
            {
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    json = ToJson(dtResult);
                }
                else
                {
                    json = "{\"d\":\"[]\"}";
                }
            }
            catch (Exception ex)
            {
                json = "{\"d\":\"[]\"}";
            }
        }
        return json;
    }

    private static DataTable BindViewCSSystemLogDetailSQL(string FogOrderNum)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.MemoKey = FogOrderNum;

        DataSet dsDetailResult = LmSystemLogBP.OrderActionHis(_lmSystemLogEntity).QueryResult;
        return dsDetailResult.Tables[0];
    }


    private static DataTable BindViewCSSystemLogDetailORACLE(string FogOrderNum)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = FogOrderNum;

        DataSet dsDetailResult = LmSystemLogBP.GetOperateHis(_lmSystemLogEntity).QueryResult;
        return dsDetailResult.Tables[0];
    }


    #region
    public static string ToJson(DataTable dt)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("[");
        DataRowCollection drc = dt.Rows;
        for (int i = 0; i < drc.Count; i++)
        {
            jsonString.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string strKey = dt.Columns[j].ColumnName;
                string strValue = drc[i][j].ToString();
                Type type = dt.Columns[j].DataType;
                jsonString.Append("\"" + strKey + "\":");
                strValue = StringFormat(strValue, type);
                if (j < dt.Columns.Count - 1)
                {
                    jsonString.Append(strValue + ",");
                }
                else
                {
                    jsonString.Append(strValue);
                }
            }
            jsonString.Append("},");
        }
        jsonString.Remove(jsonString.Length - 1, 1);
        jsonString.Append("]");
        return jsonString.ToString();
    }

    private static string StringFormat(string str, Type type)
    {
        if (type == typeof(string))
        {
            str = String2Json(str);
            str = "\"" + str + "\"";
        }
        else if (type == typeof(DateTime))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(bool))
        {
            str = str.ToLower();
        }
        else if (type != typeof(string) && string.IsNullOrEmpty(str))
        {
            str = "\"" + str + "\"";
        }
        return str;
    }

    private static string String2Json(String s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s.ToCharArray()[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\""); break;
                case '\\':
                    sb.Append("\\\\"); break;
                case '/':
                    sb.Append("\\/"); break;
                case '\b':
                    sb.Append("\\b"); break;
                case '\f':
                    sb.Append("\\f"); break;
                case '\n':
                    sb.Append("\\n"); break;
                case '\r':
                    sb.Append("\\r"); break;
                case '\t':
                    sb.Append("\\t"); break;
                default:
                    sb.Append(c); break;
            }
        }
        return sb.ToString();
    }
    #endregion

    #endregion


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
}
