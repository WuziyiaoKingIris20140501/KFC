using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

public partial class OrderConfirmInfo : BasePage
{
    OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type="))
        {
            ViewState["CityID"] = "";
            ViewState["SortID"] = "";
            ViewState["CStatus"] = "";
            ViewState["FStatus"] = "";
            ViewState["HotelConfirm"] = "0";
            ViewState["HotelID"] = "";
            ViewState["UserID"] = "";
            ViewState["OrderID"] = "";
            ViewState["StartDate"] = System.DateTime.Now.ToString().Replace("/", "-");//System.DateTime.Now.AddDays(-3).ToShortDateString().Replace("/", "-") + " 04:00:00";
            ViewState["EndDate"] = "";
            ViewState["BStatusOther"] = "1,";
            ViewState["InitFlag"] = "1";
            dpCreateStart.Value = ViewState["StartDate"].ToString();
            BindDropDownList();
            BindOrderConfirmListGrid();
        }
    }

    private void BindDropDownList()
    {
        //DataSet dsCityList = GetCityListData();
        //ddpCityList.DataSource = dsCityList;
        //ddpCityList.DataTextField = "CT_TEXT";
        //ddpCityList.DataValueField = "CT_STATUS";
        //ddpCityList.DataBind();
        //ddpCityList.SelectedIndex = 0;

        DataTable dtSortType = GetSortTypeData();
        ddpSort.DataSource = dtSortType;
        ddpSort.DataTextField = "SORT_TEXT";
        ddpSort.DataValueField = "SORT_STATUS";
        ddpSort.DataBind();
        ddpSort.SelectedIndex = 0;

        DataTable dtControlStatus = GetControlStatusData();
        ddpControlStatus.DataSource = dtControlStatus;
        ddpControlStatus.DataTextField = "CON_TEXT";
        ddpControlStatus.DataValueField = "CON_STATUS";
        ddpControlStatus.DataBind();
        ddpControlStatus.SelectedIndex = 0;

        DataTable dtFaxStatus = GetFaxStatusData();
        ddpFaxStatus.DataSource = dtFaxStatus;
        ddpFaxStatus.DataTextField = "FS_TEXT";
        ddpFaxStatus.DataValueField = "FS_STATUS";
        ddpFaxStatus.DataBind();
        ddpFaxStatus.SelectedIndex = 0;

        DataTable dtHotelConfirm = GetHotelConfirmData();
        ddpHotelConfirm.DataSource = dtHotelConfirm;
        ddpHotelConfirm.DataTextField = "HC_TEXT";
        ddpHotelConfirm.DataValueField = "HC_STATUS";
        ddpHotelConfirm.DataBind();
        ddpHotelConfirm.SelectedValue = "0";

        DataTable dtBStatusOther = GetBStatusOtherData();
        chklBStatusOther.DataSource = dtBStatusOther;
        chklBStatusOther.DataTextField = "BSO_TEXT";
        chklBStatusOther.DataValueField = "BSO_STATUS";
        chklBStatusOther.DataBind();
        //chklBStatusOther.SelectedValue = "1";
        chklBStatusOther.Items[0].Selected = true;
    }

    private DataSet GetCityListData()
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderInfoBP.CommonCitySelect(_orderInfoEntity).QueryResult;

        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["CITY_ID"].ColumnName = "CT_STATUS";
            dsResult.Tables[0].Columns["NAME_CN"].ColumnName = "CT_TEXT";

            DataRow dr0 = dsResult.Tables[0].NewRow();
            dr0["CT_STATUS"] = "";
            dr0["CT_TEXT"] = "不限制";
            dsResult.Tables[0].Rows.InsertAt(dr0, 0);
        }

        return dsResult;
    }

    private DataTable GetSortTypeData()
    {
        DataTable dt = new DataTable();
        DataColumn SortStatus = new DataColumn("SORT_STATUS");
        DataColumn SortStatusText = new DataColumn("SORT_TEXT");
        dt.Columns.Add(SortStatus);
        dt.Columns.Add(SortStatusText);


        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["SORT_STATUS"] = "";
                    dr["SORT_TEXT"] = "默认排序";
                    break;
                case "1":
                    dr["SORT_STATUS"] = "ASC";
                    dr["SORT_TEXT"] = "最老优先";
                    break;
                case "2":
                    dr["SORT_STATUS"] = "DESC";
                    dr["SORT_TEXT"] = "最新优先";
                    break;
                default:
                    dr["SORT_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetControlStatusData()
    {
        DataTable dt = new DataTable();
        DataColumn ConStatus = new DataColumn("CON_STATUS");
        DataColumn ConStatusText = new DataColumn("CON_TEXT");
        dt.Columns.Add(ConStatus);
        dt.Columns.Add(ConStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["CON_STATUS"] = "";
        dr0["CON_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 4; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["CON_STATUS"] = "0";
                    dr["CON_TEXT"] = "待操作";
                    break;
                case "1":
                    dr["CON_STATUS"] = "1";
                    dr["CON_TEXT"] = "可操作";
                    break;
                case "2":
                    dr["CON_STATUS"] = "2";
                    dr["CON_TEXT"] = "操作中";
                    break;
                case "3":
                    dr["CON_STATUS"] = "3";
                    dr["CON_TEXT"] = "需跟进";
                    break;
                default:
                    dr["CON_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetFaxStatusData()
    {
        DataTable dt = new DataTable();
        DataColumn ConStatus = new DataColumn("FS_STATUS");
        DataColumn ConStatusText = new DataColumn("FS_TEXT");
        dt.Columns.Add(ConStatus);
        dt.Columns.Add(ConStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["FS_STATUS"] = "";
        dr0["FS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 5; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["FS_STATUS"] = "0";
                    dr["FS_TEXT"] = "待发送";
                    break;
                case "1":
                    dr["FS_STATUS"] = "1";
                    dr["FS_TEXT"] = "发送中";
                    break;
                case "2":
                    dr["FS_STATUS"] = "2";
                    dr["FS_TEXT"] = "发送成功";
                    break;
                case "3":
                    dr["FS_STATUS"] = "3";
                    dr["FS_TEXT"] = "发送失败";
                    break;
                case "4":
                    dr["FS_STATUS"] = "4";
                    dr["FS_TEXT"] = "重发中";
                    break;
                default:
                    dr["FS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetHotelConfirmData()
    {
        DataTable dt = new DataTable();
        DataColumn ConStatus = new DataColumn("HC_STATUS");
        DataColumn ConStatusText = new DataColumn("HC_TEXT");
        dt.Columns.Add(ConStatus);
        dt.Columns.Add(ConStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["HC_STATUS"] = "";
        dr0["HC_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 5; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["HC_STATUS"] = "0";
                    dr["HC_TEXT"] = "待确认";
                    break;
                case "1":
                    dr["HC_STATUS"] = "1";
                    dr["HC_TEXT"] = "已确认";
                    break;
                case "2":
                    dr["HC_STATUS"] = "2";
                    dr["HC_TEXT"] = "问题单（手工单）";
                    break;
                case "3":
                    dr["HC_STATUS"] = "9";
                    dr["HC_TEXT"] = "酒店反悔";
                    break;
                case "4":
                    dr["HC_STATUS"] = "h";
                    dr["HC_TEXT"] = "HOLD状态";
                    break;
                default:
                    dr["HC_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetBStatusOtherData()
    {
        DataTable dt = new DataTable();
        DataColumn ConStatus = new DataColumn("BSO_STATUS");
        DataColumn ConStatusText = new DataColumn("BSO_TEXT");
        dt.Columns.Add(ConStatus);
        dt.Columns.Add(ConStatusText);

        //DataRow dr0 = dt.NewRow();
        //dr0["BSO_STATUS"] = "";
        //dr0["BSO_TEXT"] = "不限制";
        //dt.Rows.Add(dr0);

        for (int i = 0; i < 7; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["BSO_STATUS"] = "1";
                    dr["BSO_TEXT"] = "新单";
                    break;
                case "1":
                    dr["BSO_STATUS"] = "3";
                    dr["BSO_TEXT"] = "用户取消";
                    break;
                case "2":
                    dr["BSO_STATUS"] = "5";
                    dr["BSO_TEXT"] = "No-Show";
                    break;
                case "3":
                    dr["BSO_STATUS"] = "6";
                    dr["BSO_TEXT"] = "已完成";
                    break;
                case "4":
                    dr["BSO_STATUS"] = "7";
                    dr["BSO_TEXT"] = "入住中";
                    break;
                case "5":
                    dr["BSO_STATUS"] = "8";
                    dr["BSO_TEXT"] = "已离店";
                    break;
                case "6":
                    dr["BSO_STATUS"] = "9";
                    dr["BSO_TEXT"] = "CC取消";
                    break;
                default:
                    dr["BSO_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void BindOrderConfirmListGrid()
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        orderinfoEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        orderinfoEntity.SortID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SortID"].ToString())) ? null : ViewState["SortID"].ToString();
        orderinfoEntity.CStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CStatus"].ToString())) ? null : ViewState["CStatus"].ToString();
        orderinfoEntity.FStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FStatus"].ToString())) ? null : ViewState["FStatus"].ToString();
        orderinfoEntity.HotelConfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelConfirm"].ToString())) ? null : ViewState["HotelConfirm"].ToString();
        orderinfoEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        orderinfoEntity.UserID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UserID"].ToString())) ? null : ViewState["UserID"].ToString();
        orderinfoEntity.OrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        orderinfoEntity.StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDate"].ToString())) ? null : ViewState["StartDate"].ToString();
        orderinfoEntity.EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDate"].ToString())) ? null : ViewState["EndDate"].ToString();
        orderinfoEntity.BOOK_STATUS_OTHER = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BStatusOther"].ToString())) ? null : ViewState["BStatusOther"].ToString();
        orderinfoEntity.SType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InitFlag"].ToString())) ? null : ViewState["InitFlag"].ToString();
        //if (String.IsNullOrEmpty(orderinfoEntity.HotelConfirm) && (String.IsNullOrEmpty(orderinfoEntity.HotelID) && String.IsNullOrEmpty(orderinfoEntity.UserID) && String.IsNullOrEmpty(orderinfoEntity.OrderID)))
        //{
        //    orderinfoEntity.HotelConfirm = "0";
        //    orderinfoEntity.SType = "1";
        //}

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderInfoBP.BindOrderConfirmList(_orderInfoEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "ORDECTIME", "BOOKSTATUS", "REDDIS" };//主键
        gridViewCSList.DataBind();

        string strCreatTime = "";
        string strBookStatus = "";
        string strSysDTime = "";

        DateTime dtTemp = DateTime.Now;
        DateTime dtTempCt = DateTime.Now;

        for (int i = 0; i < gridViewCSList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridViewCSList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                strCreatTime = gridViewCSList.DataKeys[i].Values[0].ToString();
                strBookStatus = gridViewCSList.DataKeys[i].Values[1].ToString();
                strSysDTime = gridViewCSList.DataKeys[i].Values[2].ToString();

                if (String.IsNullOrEmpty(strCreatTime))
                {
                    continue;
                }

                dtTempCt = DateTime.Parse(strCreatTime);
                dtTemp = DateTime.Parse(strSysDTime).AddMinutes(-5);

                if ((dtTemp > dtTempCt) && ("1".Equals(strBookStatus)))
                {
                    gridViewCSList.Rows[i].Cells[0].Attributes.Add("bgcolor", "#FF6666");
                }
            }
        }
        //ViewState["StartDate"] = "";
        //ViewState["EndDate"] = "";
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strCityID = "";
        if (String.IsNullOrEmpty(wctCity.AutoResult) || String.IsNullOrEmpty(hidCityID.Value.Trim()))
        {
            strCityID = "";
        }
        else
        {
            string strCityNM = wctCity.AutoResult.ToString();
            strCityID = strCityNM.Substring((strCityNM.IndexOf('[') + 1), (strCityNM.IndexOf(']') - 1));
        }

        ViewState["CityID"] = strCityID;//ddpCityList.SelectedValue.Trim();
        ViewState["SortID"] = ddpSort.SelectedValue.Trim();
        ViewState["CStatus"] = ddpControlStatus.SelectedValue.Trim();
        ViewState["FStatus"] = ddpFaxStatus.SelectedValue.Trim();
        ViewState["HotelConfirm"] = ddpHotelConfirm.SelectedValue.Trim();
        ViewState["InitFlag"] = "0";

        string strHotelID = "";
        if (String.IsNullOrEmpty(WebAutoComplete.AutoResult) || String.IsNullOrEmpty(hidHotelID.Value.Trim()))
        {
            strHotelID = "";
        }
        else
        {
            string strHotelNM = WebAutoComplete.AutoResult.ToString();
            strHotelID = strHotelNM.Substring((strHotelNM.IndexOf('[') + 1), (strHotelNM.IndexOf(']') - 1));
        }

        ViewState["HotelID"] = strHotelID;
        ViewState["UserID"] = txtUserID.Text.Trim();
        ViewState["OrderID"] = txtOrderID.Text.Trim();
        ViewState["StartDate"] = dpCreateStart.Value.ToString();
        ViewState["EndDate"] = dpCreateEnd.Value.ToString();
        //ViewState["BStatusOther"] = ddpBStatusOther.SelectedValue.Trim();
        ViewState["BStatusOther"] = GetBStatusList();

        BindOrderConfirmListGrid();
    }

    private string GetBStatusList()
    {
        string strResult = "";
        foreach (ListItem lt in chklBStatusOther.Items)
        {
            if (lt.Selected)
            {
                strResult = strResult + lt.Value + ",";
            }
        }
        return strResult;
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindOrderConfirmListGrid();
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(hidOrderID.Value))
        {
            _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
            OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

            orderinfoEntity.OrderID = hidOrderID.Value.Trim();
            _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
            int iResut = OrderInfoBP.UnLockOrderConfirm(_orderInfoEntity).Result;

            _commonEntity.LogMessages = _orderInfoEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单操作-解锁";
            commonDBEntity.Event_ID = hidHotelID.Value;
            string conTent = GetLocalResourceObject("EventUnLockMessage").ToString();

            conTent = string.Format(conTent, hidOrderID.Value.Trim(), _orderInfoEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;

            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }

        BindOrderConfirmListGrid();
    }
}