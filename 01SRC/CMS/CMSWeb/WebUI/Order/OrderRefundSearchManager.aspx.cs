using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Process;
using System.Data;

public partial class WebUI_Order_OrderRefundSearchManager : System.Web.UI.Page
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSalesDataTable();

            ViewState["hotelID"] = "";
            ViewState["salesID"] = "";
            ViewState["CreateStart"] = "";
            ViewState["EndStart"] = "";
            ViewState["orderNum"] = "";
            ViewState["strPayStatus"] = "";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(this.hidSelecHotel.Value.Trim()))
        {
            if (!hidSelecHotel.Value.Trim().Contains("[") || !hidSelecHotel.Value.Trim().Contains("]"))
            {
                messageContent.InnerHtml = "查询失败，选择酒店不合法，请修改！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }
        }


        string hotelID = !string.IsNullOrEmpty(this.hidSelecHotel.Value) ? this.hidSelecHotel.Value.Substring((this.hidSelecHotel.Value.IndexOf('[') + 1), (this.hidSelecHotel.Value.IndexOf(']') - 1)) : "";//酒店
        string salesID = ddpSalesManager.SelectedValue;//销售
        string CreateStart = dpCreateStart.Value;//创建开始时间
        string EndStart = dpCreateEnd.Value;//创建结束时间
        string orderNum = this.txtFogaOrderNum.Text;//订单编号
        string strPayStatus = "";//支付状态
        if (this.chkPayStatusSus.Checked == true)//支付成功
        {
            strPayStatus += "1,";//支付成功1         待支付0,2,3,8,9        已退款7
        }
        if (this.chkPayStatusBackPay.Checked == true)
        {
            strPayStatus += "0,2,3,8,9,";//支付成功1         待支付0,2,3,8,9     已退款7
        }
        if (this.chkPayStatusRebate.Checked == true)
        {
            strPayStatus += "7,";//支付成功1         待支付0,2,3,8,9     已退款7
        }
        ViewState["hotelID"] = hotelID;
        ViewState["salesID"] = salesID;
        ViewState["CreateStart"] = CreateStart;
        ViewState["EndStart"] = EndStart;
        ViewState["orderNum"] = orderNum;
        ViewState["strPayStatus"] = strPayStatus;
        BindGroupListGrid(hotelID, salesID, CreateStart, EndStart, orderNum, strPayStatus);
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "keys", "BtnCompleteStyle();", true);
    }


    private void BindGroupListGrid(string hotelID, string sales, string startDate, string endDate, string orderNum, string payStatus)
    {
        DataTable dtResult = GetRefundOrderList(hotelID, sales, startDate, endDate, orderNum, payStatus);
        gridViewCSReviewList.DataSource = dtResult;
        gridViewCSReviewList.DataKeyNames = new string[] { };//主键
        gridViewCSReviewList.DataBind();
    }

    protected void gridViewCSReviewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridViewCSReviewList.PageIndex = e.NewPageIndex;
        BindGroupListGrid(ViewState["hotelID"].ToString(), ViewState["salesID"].ToString(), ViewState["CreateStart"].ToString(), ViewState["EndStart"].ToString(), ViewState["orderNum"].ToString(), ViewState["strPayStatus"].ToString());
    }

    /// <summary>
    /// 酒店销售人员
    /// </summary>
    private void GetSalesDataTable()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsSalesManager = LmSystemLogBP.SalesManagerSelect(_lmSystemLogEntity).QueryResult;

        DataRow drTemp = dsSalesManager.Tables[0].NewRow();
        drTemp["SALESID"] = DBNull.Value;
        drTemp["User_Account"] = DBNull.Value;
        drTemp["SALESNAME"] = "不限制";
        dsSalesManager.Tables[0].Rows.InsertAt(drTemp, 0);

        ddpSalesManager.DataTextField = "SALESNAME";
        ddpSalesManager.DataValueField = "User_Account";
        ddpSalesManager.DataSource = dsSalesManager;
        ddpSalesManager.DataBind();
        ddpSalesManager.SelectedIndex = 0;
    }

    public DataTable GetRefundOrderList(string hotelID, string sales, string startDate, string endDate, string orderNum, string payStatus)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.HOTEL_ID = hotelID;
        orderinfoEntity.UserID = sales;
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        orderinfoEntity.FOG_ORDER_NUM = orderNum;
        orderinfoEntity.PAY_STATUS = payStatus;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetRefundOrderList(_orderInfoEntity).QueryResult.Tables[0];
        return dtResult;
    }
}