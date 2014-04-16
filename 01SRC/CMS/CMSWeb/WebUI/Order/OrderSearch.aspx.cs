using System;
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

public partial class WebUI_Order_OrderSearch : System.Web.UI.Page
{
    OrderInfoEntity _orderinfoEntity = new OrderInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    public static string noLimit = string.Empty;
    public static string strOnlineLabel = string.Empty;
    public static string strOfflineLabel = string.Empty;
    public static string ResetText = string.Empty;
    public static string PromptHotelIDIsNaN = string.Empty;

    public static string lblSelectDate = string.Empty;
    public static string PromptStartDateMustGreaterThanEnd = string.Empty;
    public static string PromptHotelIDMustIsNumber = string.Empty;
    public static string PromptOrderIDMustIsNumber = string.Empty;
    public static string lblReset = string.Empty;

    Oder order = new Oder();
    protected void Page_Load(object sender, EventArgs e)
    {
        noLimit = Resources.MyGlobal.NoLimitText;
        ResetText = Resources.MyGlobal.ResetText;

        lblSelectDate = GetLocalResourceObject("lblSelectDate").ToString();
        PromptStartDateMustGreaterThanEnd = GetLocalResourceObject("PromptStartDateMustGreaterThanEnd").ToString();
        PromptHotelIDMustIsNumber = GetLocalResourceObject("PromptHotelIDMustIsNumber").ToString();
        PromptOrderIDMustIsNumber = GetLocalResourceObject("PromptOrderIDMustIsNumber").ToString();
        lblReset = GetLocalResourceObject("lblReset").ToString();

        if (!Page.IsPostBack)
        {          
            bindCityDDL();
            bindBookStatusList();
            bindPayStatusList();           
                                
            SetEmptyDataTable();

            string strToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            this.dtStartTime.Value = strToday;
            this.dtEndTime.Value = strToday;
        }
    }

    private DataSet getHotelData()
    {
        //string sql = string.Empty;
        //if (ViewState["condition"] != null)
        //{
        //    sql = "select FOG_ORDER_NUM,CITY_ID,HOTEL_ID,HOTEL_NAME,ROOM_TYPE_NAME,IN_DATE,GUEST_NAMES,CONTACT_NAME,CONTACT_TEL,BOOK_STATUS,PAY_STATUS,BOOK_SOURCE,BOOK_PRICE,BOOK_ROOM_NUM,BOOK_TOTAL_PRICE,TICKET_AMOUNT,TICKET_USERCODE,TICKET_AMOUNT,TICKET_COUNT,CREATE_TIME,LOGIN_MOBILE  from T_LM_ORDER where 1=1 " + ViewState["condition"].ToString();
        //}
        //else
        //{
        //    sql = "select FOG_ORDER_NUM,CITY_ID,HOTEL_ID,HOTEL_NAME,ROOM_TYPE_NAME,IN_DATE,GUEST_NAMES,CONTACT_NAME,CONTACT_TEL,BOOK_STATUS,PAY_STATUS,BOOK_SOURCE,BOOK_PRICE,BOOK_ROOM_NUM,BOOK_TOTAL_PRICE,TICKET_AMOUNT,TICKET_USERCODE,TICKET_AMOUNT,TICKET_COUNT,CREATE_TIME,LOGIN_MOBILE	from T_LM_ORDER";
        //}

        //DataSet ds = HotelVp.Common.DBUtility.DbHelperOra.Query(sql);

        //return ds;


        _orderinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderinfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderInfoDBEntity = new OrderInfoDBEntity();

        orderInfoDBEntity.HOTEL_ID = ViewState["HOTEL_ID"].ToString();
        orderInfoDBEntity.HOTEL_NAME = ViewState["HOTEL_NAME"].ToString();
        orderInfoDBEntity.CITY_ID = ViewState["CITY_ID"].ToString();
        orderInfoDBEntity.StartDate = ViewState["StartDate"].ToString();
        orderInfoDBEntity.EndDate = ViewState["EndDate"].ToString();
        orderInfoDBEntity.BOOK_STATUS = ViewState["BOOK_STATUS"].ToString();
        orderInfoDBEntity.PAY_STATUS = ViewState["PAY_STATUS"].ToString();
        orderInfoDBEntity.FOG_ORDER_NUM = ViewState["FOG_ORDER_NUM"].ToString();

        _orderinfoEntity.OrderInfoDBEntity.Add(orderInfoDBEntity);
        DataSet dsResult = OrderInfoBP.BindOrderList(_orderinfoEntity).QueryResult;

        return dsResult;
    }
    
    private void BindGridView()
    {
        DataTable dt = createDataTable();         
        GridviewControl.GridViewDataBind(this.gridViewOrder, dt); 
    }

    public DataTable createDataTable()
    {
        DataTable getDataTable = getHotelData().Tables[0];

        DataTable dt = new DataTable();
        DataColumn FOG_ORDER_NUM_dc = new DataColumn("FOG_ORDER_NUM");
        DataColumn PRICE_CODE_dc = new DataColumn("PRICE_CODE");
        DataColumn CITY_ID_dc = new DataColumn("CITY_ID");
        DataColumn HOTEL_ID_dc = new DataColumn("HOTEL_ID");
        DataColumn HOTEL_NAME_dc = new DataColumn("HOTEL_NAME");
        DataColumn ROOM_TYPE_NAME_dc = new DataColumn("ROOM_TYPE_NAME");
        DataColumn IN_DATE_dc = new DataColumn("IN_DATE");
        DataColumn GUEST_NAMES_dc = new DataColumn("GUEST_NAMES");
        DataColumn CONTACT_TEL_dc = new DataColumn("CONTACT_TEL");
        DataColumn BOOK_SOURCE_dc = new DataColumn("BOOK_SOURCE");
        DataColumn BOOK_TOTAL_PRICE_dc = new DataColumn("BOOK_TOTAL_PRICE");
        DataColumn TICKET_AMOUNT_dc = new DataColumn("TICKET_AMOUNT");
        DataColumn BOOK_STATUS_dc = new DataColumn("BOOK_STATUS");
        DataColumn PAY_STATUS_dc = new DataColumn("PAY_STATUS");
        DataColumn LOGIN_MOBILE_dc = new DataColumn("LOGIN_MOBILE");
        DataColumn PROMOTION_AMOUNT_dc = new DataColumn("PROMOTION_AMOUNT");
        
        dt.Columns.Add(FOG_ORDER_NUM_dc);
        dt.Columns.Add(PRICE_CODE_dc);
        dt.Columns.Add(CITY_ID_dc);
        dt.Columns.Add(HOTEL_ID_dc);
        dt.Columns.Add(HOTEL_NAME_dc);
        dt.Columns.Add(ROOM_TYPE_NAME_dc);
        dt.Columns.Add(IN_DATE_dc);
        dt.Columns.Add(GUEST_NAMES_dc);
        dt.Columns.Add(CONTACT_TEL_dc);
        dt.Columns.Add(BOOK_SOURCE_dc);
        dt.Columns.Add(BOOK_TOTAL_PRICE_dc);
        dt.Columns.Add(TICKET_AMOUNT_dc);
        dt.Columns.Add(BOOK_STATUS_dc);
        dt.Columns.Add(PAY_STATUS_dc);
        dt.Columns.Add(LOGIN_MOBILE_dc);
        dt.Columns.Add(PROMOTION_AMOUNT_dc);

        for (int i = 0; i < getDataTable.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["FOG_ORDER_NUM"] = getDataTable.Rows[i]["FOG_ORDER_NUM"];
            dr["PRICE_CODE"] = getDataTable.Rows[i]["PRICE_CODE"];
            dr["CITY_ID"] = getDataTable.Rows[i]["CITY_ID"];
            dr["HOTEL_ID"] = getDataTable.Rows[i]["HOTEL_ID"];
            dr["HOTEL_NAME"] = getDataTable.Rows[i]["HOTEL_NAME"];
            dr["ROOM_TYPE_NAME"] = getDataTable.Rows[i]["ROOM_TYPE_NAME"];
            dr["IN_DATE"] = string.Format("{0:yyyy-MM-dd}", getDataTable.Rows[i]["IN_DATE"]);
            dr["GUEST_NAMES"] = getDataTable.Rows[i]["GUEST_NAMES"];
            dr["CONTACT_TEL"] = getDataTable.Rows[i]["CONTACT_TEL"];
            dr["BOOK_SOURCE"] = getDataTable.Rows[i]["BOOK_SOURCE"];
            dr["BOOK_TOTAL_PRICE"] = getDataTable.Rows[i]["BOOK_TOTAL_PRICE"];
            dr["TICKET_AMOUNT"] = getDataTable.Rows[i]["TICKET_AMOUNT"];

            string bookStatus = getDataTable.Rows[i]["BOOK_STATUS"].ToString();
            string payStatus = getDataTable.Rows[i]["PAY_STATUS"].ToString();   
        
            //string bookdate = getDataTable.Rows[i]["CREATE_TIME"].ToString();
            dr["BOOK_STATUS"] = order.getBookStatus(bookStatus);
            dr["PAY_STATUS"] = order.getPayStatus(payStatus);
            dr["LOGIN_MOBILE"] = getDataTable.Rows[i]["LOGIN_MOBILE"];
            //dr["STATUS"] = order.getStatusByPayAndBookStatus(bookStatus, payStatus, bookdate);
            dr["PROMOTION_AMOUNT"] = getDataTable.Rows[i]["PROMOTION_AMOUNT"];
            dt.Rows.Add(dr);        
        }
        return dt;
    
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string hotelid = this.txtHotelID.Text.Trim().Replace("'", "");
        string hotelname = this.txtHotelName.Text.Trim().Replace("'", "");
        string cityid = this.ddlCity.SelectedValue.Trim();
        string startDate = this.dtStartTime.Value.Trim();
        string endDate = this.dtEndTime.Value.Trim();
        string bookstatus = this.radioListBookStatus.SelectedValue.Trim();
        string paystatus = this.radioListPayStatus.SelectedValue.Trim();
        string orderid = this.txtOrderID.Text.Replace("'", "").Trim();//订单ID


        ViewState["HOTEL_ID"] = hotelid;
        ViewState["HOTEL_NAME"] = hotelname;
        ViewState["CITY_ID"] = cityid;
        ViewState["StartDate"] = startDate;
        ViewState["EndDate"] = endDate;
        ViewState["BOOK_STATUS"] = bookstatus;
        ViewState["PAY_STATUS"] = paystatus;
        ViewState["FOG_ORDER_NUM"] = orderid;


        //string condtion = string.Empty;
        //if (!string.IsNullOrEmpty(hotelid))
        //{
        //    condtion = condtion + " and HOTEL_ID= " + hotelid;
        //}
        //if (!string.IsNullOrEmpty(hotelname))
        //{
        //    condtion = condtion + " and  HOTEL_NAME like '%" + hotelname + "%'";
        //}
        //if (!string.IsNullOrEmpty(cityid))
        //{
        //    condtion = condtion + " and  CITY_ID = '" + cityid + "'";
        //}

        //if (string.IsNullOrEmpty(startDate) != true && string.IsNullOrEmpty(endDate) != true)
        //{
        //    condtion = condtion + " and  IN_DATE between  to_date('" + startDate + "','yyyy-MM-dd') and to_date('" + endDate + "','yyyy-MM-dd')";
        //}

        //if (!string.IsNullOrEmpty(bookstatus))
        //{
        //    condtion = condtion + " and  BOOK_STATUS = '" + bookstatus + "'";
        //}

        //if (!string.IsNullOrEmpty(paystatus))
        //{
        //    condtion = condtion + " and  PAY_STATUS = '" + paystatus + "'";
        //}

        //if (!string.IsNullOrEmpty(orderid))
        //{
        //    condtion = condtion + " and  FOG_ORDER_NUM = '" + orderid + "'";
        //}
        //ViewState["condition"] = condtion;//把这个查询条件保存下来。

        gridViewOrder.EditIndex = -1;
        BindGridView();//绑定gridview数据。
    }

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            DataTable dt = createDataTable();
            if (dt != null)
            {
                ds.Tables.Add(dt);
            }
            if (dt == null)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('订单数据列表中没有数据，不能导出！');", true);
                return;

            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('订单数据列表中没有数据，不能导出！');", true);
                return;

            }
            CommonFunction.DataSet2Excel(ds);
        }
        catch
        {
            SetEmptyDataTable();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('订单数据列表中没有数据，不能导出！');", true);
            return;
        }
    }
    
    //绑定下来框数据
    private void bindCityDDL()
    {
        //string sql ="select CITY_ID,NAME_CN from t_lm_b_city where STATUS=1";
        //DataTable myTable = DbHelperOra.Query(sql).Tables[0];
        
        DataSet dsCity = OrderInfoBP.CommonCitySelect(_orderinfoEntity).QueryResult;
        DataTable myTable = dsCity.Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["NAME_CN"] = noLimit;
        row1["CITY_ID"] = "";
        myTable.Rows.InsertAt(row1, 0);

        this.ddlCity.DataSource = myTable;
        this.ddlCity.DataTextField = "NAME_CN";
        this.ddlCity.DataValueField = "CITY_ID";
        this.ddlCity.DataBind();
    }
        
    private void bindBookStatusList()
    {
        DataTable dt = order.getBookDataTable();
        radioListBookStatus.DataSource = dt;
        radioListBookStatus.DataTextField = "BOOK_STATUS_TEXT";
        radioListBookStatus.DataValueField = "BOOK_STATUS";
        radioListBookStatus.DataBind();
        radioListBookStatus.SelectedIndex = 0;
    }

    private void bindPayStatusList()
    {
        DataTable dt = order.getPayDataTable();
        radioListPayStatus.DataSource = dt;
        radioListPayStatus.DataTextField = "PAY_STATUS_TEXT";
        radioListPayStatus.DataValueField = "PAY_STATUS";
        radioListPayStatus.DataBind();
        radioListPayStatus.SelectedIndex = 0;

    }
    
    protected void gridViewOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewOrder.PageIndex = e.NewPageIndex;
        BindGridView();

    }

    private void SetEmptyDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn FOG_ORDER_NUM_dc = new DataColumn("FOG_ORDER_NUM");
        DataColumn PRICE_CODE_dc = new DataColumn("PRICE_CODE");
        DataColumn CITY_ID_dc = new DataColumn("CITY_ID");
        DataColumn HOTEL_ID_dc = new DataColumn("HOTEL_ID");
        DataColumn HOTEL_NAME_dc = new DataColumn("HOTEL_NAME");
        DataColumn ROOM_TYPE_NAME_dc = new DataColumn("ROOM_TYPE_NAME");
        DataColumn IN_DATE_dc = new DataColumn("IN_DATE");
        DataColumn GUEST_NAMES_dc = new DataColumn("GUEST_NAMES");
        DataColumn CONTACT_TEL_dc = new DataColumn("CONTACT_TEL");
        DataColumn BOOK_SOURCE_dc = new DataColumn("BOOK_SOURCE");
        DataColumn BOOK_TOTAL_PRICE_dc = new DataColumn("BOOK_TOTAL_PRICE");
        DataColumn TICKET_AMOUNT_dc = new DataColumn("TICKET_AMOUNT");
        DataColumn BOOK_STATUS_dc = new DataColumn("BOOK_STATUS");
        DataColumn PAY_STATUS_dc = new DataColumn("PAY_STATUS");
        DataColumn LOGIN_MOBILE_dc = new DataColumn("LOGIN_MOBILE");
        DataColumn PROMOTION_AMOUNT_dc = new DataColumn("PROMOTION_AMOUNT");

        dt.Columns.Add(FOG_ORDER_NUM_dc);
        dt.Columns.Add(PRICE_CODE_dc);
        dt.Columns.Add(CITY_ID_dc);
        dt.Columns.Add(HOTEL_ID_dc);
        dt.Columns.Add(HOTEL_NAME_dc);
        dt.Columns.Add(ROOM_TYPE_NAME_dc);
        dt.Columns.Add(IN_DATE_dc);
        dt.Columns.Add(GUEST_NAMES_dc);
        dt.Columns.Add(CONTACT_TEL_dc);
        dt.Columns.Add(BOOK_SOURCE_dc);
        dt.Columns.Add(BOOK_TOTAL_PRICE_dc);
        dt.Columns.Add(TICKET_AMOUNT_dc);
        dt.Columns.Add(BOOK_STATUS_dc);
        dt.Columns.Add(PAY_STATUS_dc);
        dt.Columns.Add(LOGIN_MOBILE_dc);
        dt.Columns.Add(PROMOTION_AMOUNT_dc);

        GridviewControl.GridViewDataBind(this.gridViewOrder, dt);
    }
}