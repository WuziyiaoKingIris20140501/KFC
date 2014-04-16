using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.IO;
using System.Xml;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Configuration;

public partial class OrderConfirmInfoManager : BasePage
{
    OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();
    public static string strMemo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type=city") && !this.Page.Request.QueryString.ToString().Contains("Type=hotel"))
        {
            ViewState["CityID"] = "";
            ViewState["SortID"] = "";
            ViewState["CStatus"] = "";
            ViewState["FStatus"] = "";
            ViewState["HotelConfirm"] = "0";
            ViewState["HotelID"] = "";
            ViewState["UserID"] = "";
            ViewState["OrderID"] = "";
            ViewState["StartDate"] = (System.DateTime.Now.Hour >= 0 && System.DateTime.Now.Hour < 4) ? System.DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-") + " 04:00:00" : System.DateTime.Now.ToShortDateString().Replace("/", "-") + " 04:00:00";//System.DateTime.Now.AddDays(-3).ToShortDateString().Replace("/", "-") + " 04:00:00";
            ViewState["EndDate"] = "";
            ViewState["BStatusOther"] = "1,";
            ViewState["PriceCode"] = "LMBAR,LMBAR2,";//价格代码
            ViewState["BStatus"] = "5,";//预付状态
            ViewState["InitFlag"] = "1";
            dpCreateStart.Value = System.DateTime.Now.ToShortDateString().Replace("/", "-");
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

        DataTable dtCanelReson = GetCanelResonData();
        ddpCanelReson.DataTextField = "CanelResonDis";
        ddpCanelReson.DataValueField = "CanelReson";
        ddpCanelReson.DataSource = dtCanelReson;
        ddpCanelReson.DataBind();


        DataTable dtPriceCode = GetPriceCodeData();
        chkPriceCode.DataSource = dtPriceCode;
        chkPriceCode.DataTextField = "PriceName";
        chkPriceCode.DataValueField = "PriceCode";
        chkPriceCode.DataBind();
        chkPriceCode.Items[0].Selected = true;
        chkPriceCode.Items[1].Selected = true;
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

    //private DataTable GetFaxStatusData()
    //{
    //    DataTable dt = new DataTable();
    //    DataColumn ConStatus = new DataColumn("FS_STATUS");
    //    DataColumn ConStatusText = new DataColumn("FS_TEXT");
    //    dt.Columns.Add(ConStatus);
    //    dt.Columns.Add(ConStatusText);

    //    DataRow dr0 = dt.NewRow();
    //    dr0["FS_STATUS"] = "";
    //    dr0["FS_TEXT"] = "不限制";
    //    dt.Rows.Add(dr0);

    //    for (int i = 0; i < 5; i++)
    //    {
    //        DataRow dr = dt.NewRow();

    //        switch (i.ToString())
    //        {
    //            case "0":
    //                dr["FS_STATUS"] = "0";
    //                dr["FS_TEXT"] = "待发送";
    //                break;
    //            case "1":
    //                dr["FS_STATUS"] = "1";
    //                dr["FS_TEXT"] = "发送中";
    //                break;
    //            case "2":
    //                dr["FS_STATUS"] = "2";
    //                dr["FS_TEXT"] = "发送成功";
    //                break;
    //            case "3":
    //                dr["FS_STATUS"] = "3";
    //                dr["FS_TEXT"] = "发送失败";
    //                break;
    //            case "4":
    //                dr["FS_STATUS"] = "4";
    //                dr["FS_TEXT"] = "重发中";
    //                break;
    //            default:
    //                dr["FS_TEXT"] = "未知状态";
    //                break;
    //        }
    //        dt.Rows.Add(dr);
    //    }
    //    return dt;
    //}

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

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["FS_STATUS"] = "0";
                    dr["FS_TEXT"] = "待发";
                    break;
                case "1":
                    dr["FS_STATUS"] = "1";
                    dr["FS_TEXT"] = "成功";
                    break;
                case "2":
                    dr["FS_STATUS"] = "2";
                    dr["FS_TEXT"] = "失败";
                    break;
                default:
                    dr["FS_TEXT"] = "未发";
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
                    //dr["BSO_STATUS"] = "7";
                    //dr["BSO_TEXT"] = "入住中";
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
        orderinfoEntity.PRICE_CODE = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PriceCode"].ToString())) ? null : ViewState["PriceCode"].ToString();//价格代码
        orderinfoEntity.BOOK_STATUS = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BStatus"].ToString())) ? null : ViewState["BStatus"].ToString();//预付状态
        orderinfoEntity.SType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InitFlag"].ToString())) ? null : ViewState["InitFlag"].ToString();
        //if (String.IsNullOrEmpty(orderinfoEntity.HotelConfirm) && (String.IsNullOrEmpty(orderinfoEntity.HotelID) && String.IsNullOrEmpty(orderinfoEntity.UserID) && String.IsNullOrEmpty(orderinfoEntity.OrderID)))
        //{
        //    orderinfoEntity.HotelConfirm = "0";
        //    orderinfoEntity.SType = "1";
        //}

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderInfoBP.BindOrderConfirmList(_orderInfoEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "ORDECTIME", "BOOKSTATUS", "REDDIS", "ORDERID" };//主键
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
                dtTemp = DateTime.Parse(strSysDTime).AddMinutes(-15);

                if ((dtTemp > dtTempCt) && ("1".Equals(strBookStatus)))
                {
                    gridViewCSList.Rows[i].Cells[0].Attributes.Add("bgcolor", "#FF6666");
                }
            }
        }
        UpdatePanel2.Update();
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

        ViewState["HotelConfirm"] = ddpHotelConfirm.SelectedValue.Trim();//确认状态

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
        ViewState["StartDate"] = (!String.IsNullOrEmpty(dpCreateStart.Value.ToString())) ? dpCreateStart.Value.ToString() + " 04:00:00" : "";
        ViewState["EndDate"] = (!String.IsNullOrEmpty(dpCreateEnd.Value.ToString())) ? dpCreateEnd.Value.ToString() + " 03:59:59" : "";
        //ViewState["BStatusOther"] = ddpBStatusOther.SelectedValue.Trim();
        ViewState["BStatusOther"] = GetBStatusList();
        ViewState["PriceCode"] = GetPriceCodeList();//获取价格代码
        ViewState["BStatus"] = GetBStatus();//获取预付状态 

        BindOrderConfirmListGrid();
    }

    private string GetPriceCodeList()
    {
        string priceCode = "";
        foreach (ListItem lt in chkPriceCode.Items)
        {
            if (lt.Selected)
            {
                priceCode = priceCode + lt.Value + ",";
            }
        }
        return priceCode;
    }

    private string GetBStatus()
    {
        string strResult = "";
        foreach (ListItem lt in chklBStatusOther.Items)
        {
            if (lt.Selected)
            {
                switch (lt.Value)
                {
                    case "1"://新单
                        strResult = strResult + "5,";
                        break;
                    case "3"://用户取消
                        strResult = strResult + "4,";
                        break;
                    case "5"://No-Show
                        strResult = strResult + "10,";
                        break;
                    case "6"://已完成
                        strResult = strResult + "8,";
                        break;
                    case "8"://已离店
                        strResult = strResult + "9,";
                        break;
                    case "9"://CC取消
                        strResult = strResult + "7,";
                        break;
                    default:
                        break;
                }
            }
        }
        return strResult;
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

    protected void btnlock_Click(object sender, EventArgs e)
    {
        //if (!String.IsNullOrEmpty(hidOrderID.Value))
        //{
        //    _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //    _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //    _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        //    _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        //    _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        //    OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        //    orderinfoEntity.OrderID = hidOrderID.Value.Trim();
        //    _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        //    int iResut = OrderInfoBP.UnLockOrderConfirm(_orderInfoEntity).Result;

        //    _commonEntity.LogMessages = _orderInfoEntity.LogMessages;
        //    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        //    CommonDBEntity commonDBEntity = new CommonDBEntity();

        //    commonDBEntity.Event_Type = "订单确认-解锁";
        //    commonDBEntity.Event_ID = hidHotelID.Value;
        //    string conTent = GetLocalResourceObject("EventUnLockMessage").ToString();

        //    conTent = string.Format(conTent, hidOrderID.Value.Trim(), _orderInfoEntity.LogMessages.Username);
        //    commonDBEntity.Event_Content = conTent;

        //    commonDBEntity.Event_Result = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
        //    _commonEntity.CommonDBEntity.Add(commonDBEntity);
        //    CommonBP.InsertEventHistory(_commonEntity);
        //}

        //BindOrderConfirmListGrid();


        //trCanlReson.Style.Add("display", "none");
        //tbDetail.Style.Add("display", "none");
        //tbControl.Style.Add("display", "none");

        spRekButton.Style.Add("display", "none");
        dvErrorInfo.InnerHtml = "";//GetLocalResourceObject("WarningMessage").ToString();
        imgAlert.Src = "";//"../../Styles/images/err.png";
        //dvImg.Style.Add("margin-left", "100px");
        dvImg.Style.Add("margin-left", "100px");

        BindViewCSSystemLogMain();

        //dvErrorInfo.InnerHtml = "&nbsp;&nbsp;订单操作成功！&nbsp;&nbsp;&nbsp;";
        //imgAlert.Src = "../../Styles/images/suc.png";
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        BindViewOrderMain();
    }

    private void BindViewOrderMain()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsMainResult = LmSystemLogBP.OrderComfirmManagerDetail(_lmSystemLogEntity).QueryResult;
        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            return;
        }

        lbCREATE_TIME2.Text = dsMainResult.Tables[0].Rows[0]["create_time"].ToString();
        lbORDER_CHANNEL2.Text = dsMainResult.Tables[0].Rows[0]["ORDER_CHANNEL"].ToString();
        lbPRICE_CODE2.Text = dsMainResult.Tables[0].Rows[0]["price_code_nm"].ToString();
        //hidPriceCode2.Value = dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper();
        lbBOOK_STATUS2.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        lbIS_GUA2.Text = dsMainResult.Tables[0].Rows[0]["is_gua_nm"].ToString();
        lbPAY_STATUS2.Text = dsMainResult.Tables[0].Rows[0]["pay_status_nm"].ToString();

        PayMethod2.InnerHtml = lbPAY_STATUS.Text == "支付成功" ? "(" + dsMainResult.Tables[0].Rows[0]["pay_method"].ToString() + ")" : "";
        //if (lbPAY_STATUS.Text == "支付成功")
        this.lbPAY_STATUS2.ForeColor = System.Drawing.Color.Red;

        lbHOTEL_NAME2.Text = "[" + dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString() + "] " + dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();
        //hidIssueNm2.Value = "[" + dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString() + "] - [" + dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString() + "] - " + hidOrderID.Value + " - 订单确认问题";

        lbGUEST_NAMES2.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();
        lbCONTACT_TEL2.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();
        lbLOGIN_MOBILE2.Text = dsMainResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString();
        lbOrderDays2.Text = SetOrderDaysVal(dsMainResult.Tables[0].Rows[0]["in_date"].ToString(), dsMainResult.Tables[0].Rows[0]["out_date"].ToString());
        lbIN_DATE2.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString();
        lbOUT_DATE2.Text = dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString();
        lbROOM_TYPE_NAME2.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString();
        lbBOOK_ROOM_NUM2.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString();
        lbARRIVE_TIME2.Text = dsMainResult.Tables[0].Rows[0]["ARRIVE_TIME"].ToString();
        lbTICKET_PAGENM2.Text = dsMainResult.Tables[0].Rows[0]["packagename"].ToString();
        lbTICKET_AMOUNT2.Text = dsMainResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        lbBOOK_REMARK2.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();
        lbORDER_NUM2.Text = dsMainResult.Tables[0].Rows[0]["fog_order_num"].ToString();
        lbVendorNM2.Text = SetVendorVal(dsMainResult.Tables[0].Rows[0]["VENDOR"].ToString());
        lbBreakNet2.Text = (String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["breakfast_num"].ToString()) ? "0" : dsMainResult.Tables[0].Rows[0]["breakfast_num"].ToString()) + "份早餐&nbsp;&nbsp;" + (!String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["is_network"].ToString()) && "1".Equals(dsMainResult.Tables[0].Rows[0]["is_network"].ToString()) ? "免费宽带" : "无宽带");
        //chkFollowUp.Checked = ("1".Equals(dsMainResult.Tables[0].Rows[0]["FOLLOW_UP"].ToString())) ? true : false;
        lbHotel_ex2.Text = SetHotelExInfo(dsMainResult.Tables[0].Rows[0]["HXlinkman"].ToString(), dsMainResult.Tables[0].Rows[0]["HXlinktel"].ToString());
        lbHotel_ex2.Text = (String.IsNullOrEmpty(lbHotel_ex2.Text) && !String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["linkman"].ToString().Trim())) ? dsMainResult.Tables[0].Rows[0]["linkman"].ToString() + "&nbsp;&nbsp;&nbsp;联系电话：" + dsMainResult.Tables[0].Rows[0]["linktel"].ToString() : lbHotel_ex2.Text;
        txtExRemark2.Text = dsMainResult.Tables[0].Rows[0]["HXremark"].ToString();
        if ("成功".Equals(dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString()))
        {
            lbFaxStatus2.ForeColor = System.Drawing.Color.Green;
        }
        else if ("失败".Equals(dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString()))
        {
            lbFaxStatus2.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lbFaxStatus2.ForeColor = System.Drawing.Color.Black;
        }
        lbFaxStatus2.Text = dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString();
        lbFaxStatusBK2.Text = dsMainResult.Tables[0].Rows[0]["REFAXSTATUS"].ToString();
        if ("成功".Equals(dsMainResult.Tables[0].Rows[0]["CAFAXSTATUS"].ToString()))
        {
            lbCancelFax2.ForeColor = System.Drawing.Color.Green;
        }
        else if ("失败".Equals(dsMainResult.Tables[0].Rows[0]["CAFAXSTATUS"].ToString()))
        {
            lbCancelFax2.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lbCancelFax2.ForeColor = System.Drawing.Color.Black;
        }
        lbCancelFax2.Text = dsMainResult.Tables[0].Rows[0]["CAFAXSTATUS"].ToString();

        lbCancelFaxBK2.Text = dsMainResult.Tables[0].Rows[0]["RECAFAXSTATUS"].ToString();
        //txtCofNum.Text = dsMainResult.Tables[0].Rows[0]["CONFIRM_NUM"].ToString();
        hidHotelID.Value = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();
        hidContactTel.Value = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();

        decimal iDay = decimal.Parse(lbOrderDays2.Text);
        decimal iNum = (String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString())) ? 1 : decimal.Parse(dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString());
        decimal iPrice = (String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString())) ? 0 : decimal.Parse(dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString());
        decimal decPrice = Math.Round((iPrice / iDay) / iNum, 1);

        lbTotalPrice2.Text = dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString() + "元" + "（" + decPrice.ToString() + "元/间夜）";
        lblSalesMG2.Text = SetHotelSalesInfo(dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString());
        lbActionUser2.Text = dsMainResult.Tables[0].Rows[0]["OpeUser"].ToString();

        //ddpCanelReson.SelectedIndex = 0;
        //SetOrderSettingControlVal(dsMainResult.Tables[0].Rows[0]["price_code"].ToString(), dsMainResult.Tables[0].Rows[0]["book_status_other"].ToString(), dsMainResult.Tables[0].Rows[0]["order_cancle_reason"].ToString());
        //txtBOOK_REMARK2.Text = "";
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenListViewAj()", true);
    }

    protected void btnUnLockPopupArea_Click(object sender, EventArgs e)
    {
        SetUnHotellock();
        spRekButton.Style.Add("display", "none");
        dvErrorInfo.InnerHtml = "";
        imgAlert.Src = "";
        dvImg.Style.Add("margin-left", "100px");
        BindViewCSSystemLogMain();
    }

    private void SetUnHotellock()
    {
        if (!String.IsNullOrEmpty(hidUnlockHotelID.Value))
        {
            _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
            OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

            orderinfoEntity.HOTEL_ID = hidUnlockHotelID.Value.Trim();
            _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);

            int iResut = OrderInfoBP.UnLockHotelOrderConfirm(_orderInfoEntity).Result;

            _commonEntity.LogMessages = _orderInfoEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单确认-解锁";
            commonDBEntity.Event_ID = hidUnlockHotelID.Value.Trim();
            string conTent = GetLocalResourceObject("EventUnHotelLockMessage").ToString();

            conTent = string.Format(conTent, hidUnlockHotelID.Value.Trim(), _orderInfoEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;

            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            //BindOrderConfirmListGrid();
        }
    }

    private void BindViewCSSystemLogMain()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsMainResult = LmSystemLogBP.OrderComfirmManagerDetail(_lmSystemLogEntity).QueryResult;
        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            hidOrderID.Value = "";
            //btnPrint.Visible = false;
            //btnSendFax.Visible = false;
            //btnSet.Visible = false;
            //btnUnlock.Visible = false;
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "120px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenListAj()", true);
            return;
        }

        string strMsg = LmSystemLogBP.ChkOrderConfirmControl(_lmSystemLogEntity).ErrorMSG;
        if (!String.IsNullOrEmpty(strMsg))
        {
            //hidOrderID.Value = "";
            //btnPrint.Visible = false;
            //btnSendFax.Visible = false;
            //btnSet.Visible = false;
            //detailMessageContent.InnerHtml = strMsg;
            lbActionUser.Text = strMsg;
            dvErrorInfo.InnerHtml = String.Format(GetLocalResourceObject("WarningLockMessage").ToString(), strMsg);
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "70px");
            //return;
        }
        else
        {
            lbActionUser.Text = UserSession.Current.UserAccount;
            _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单确认-锁定";
            commonDBEntity.Event_ID = _lmSystemLogEntity.FogOrderID;
            string conTent = GetLocalResourceObject("EventLockMessage").ToString();

            conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;

            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateLockSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }

        //btnPrint.Visible = true;
        //btnSendFax.Visible = true;
        //btnSet.Visible = true;
        //btnUnlock.Visible = true;

        lbCREATE_TIME.Text = dsMainResult.Tables[0].Rows[0]["create_time"].ToString();
        lbORDER_CHANNEL.Text = dsMainResult.Tables[0].Rows[0]["ORDER_CHANNEL"].ToString();
        lbPRICE_CODE.Text = dsMainResult.Tables[0].Rows[0]["price_code_nm"].ToString();
        hidPriceCode.Value = dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper();
        lbBOOK_STATUS.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        lbIS_GUA.Text = dsMainResult.Tables[0].Rows[0]["is_gua_nm"].ToString();
        //lbRESV_GUA_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_HOLD_TIME"].ToString();
        //lbUSER_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["USER_HOLD_TIME"].ToString();
        //lbRESV_GUA_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_DESC"].ToString();
        //lbRESV_CXL_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_CXL_DESC"].ToString();
        lbPAY_STATUS.Text = dsMainResult.Tables[0].Rows[0]["pay_status_nm"].ToString();

        PayMethod.InnerHtml = lbPAY_STATUS.Text == "支付成功" ? "(" + dsMainResult.Tables[0].Rows[0]["pay_method"].ToString() + ")" : "";
        //if (lbPAY_STATUS.Text == "支付成功")
        this.lbPAY_STATUS.ForeColor = System.Drawing.Color.Red;

        lbHOTEL_NAME.Text = "[" + dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString() + "] " + dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();
        hidIssueNm.Value = "[" + dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString() + "] - [" + dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString() + "] - " + hidOrderID.Value + " - 订单确认问题";
        //lbFax.Text = dsMainResult.Tables[0].Rows[0]["linkfax"].ToString();
        //lbLINKTEL.Text = dsMainResult.Tables[0].Rows[0]["linktel"].ToString();
        lbGUEST_NAMES.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();
        //lbCONTACT_NAME.Text = dsMainResult.Tables[0].Rows[0]["contact_name"].ToString();
        lbCONTACT_TEL.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();
        lbLOGIN_MOBILE.Text = dsMainResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString();
        lbOrderDays.Text = SetOrderDaysVal(dsMainResult.Tables[0].Rows[0]["in_date"].ToString(), dsMainResult.Tables[0].Rows[0]["out_date"].ToString());
        lbIN_DATE.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString();
        lbOUT_DATE.Text = dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString();
        lbROOM_TYPE_NAME.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString();
        lbBOOK_ROOM_NUM.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString();
        lbARRIVE_TIME.Text = dsMainResult.Tables[0].Rows[0]["ARRIVE_TIME"].ToString();
        //lbTICKET_USERCODE.Text = dsMainResult.Tables[0].Rows[0]["ticket_usercode"].ToString();
        lbTICKET_PAGENM.Text = dsMainResult.Tables[0].Rows[0]["packagename"].ToString();
        lbTICKET_AMOUNT.Text = dsMainResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        lbBOOK_REMARK.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();
        lbORDER_NUM.Text = dsMainResult.Tables[0].Rows[0]["fog_order_num"].ToString();
        lbVendorNM.Text = SetVendorVal(dsMainResult.Tables[0].Rows[0]["VENDOR"].ToString());
        lbBreakNet.Text = (String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["breakfast_num"].ToString()) ? "0" : dsMainResult.Tables[0].Rows[0]["breakfast_num"].ToString()) + "份早餐&nbsp;&nbsp;" + (!String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["is_network"].ToString()) && "1".Equals(dsMainResult.Tables[0].Rows[0]["is_network"].ToString()) ? "免费宽带" : "无宽带");
        chkFollowUp.Checked = ("1".Equals(dsMainResult.Tables[0].Rows[0]["FOLLOW_UP"].ToString())) ? true : false;
        lbHotel_ex.Text = SetHotelExInfo(dsMainResult.Tables[0].Rows[0]["HXlinkman"].ToString(), dsMainResult.Tables[0].Rows[0]["HXlinktel"].ToString());
        lbHotel_ex.Text = (String.IsNullOrEmpty(lbHotel_ex.Text) && !String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["linkman"].ToString().Trim())) ? dsMainResult.Tables[0].Rows[0]["linkman"].ToString() + "&nbsp;&nbsp;&nbsp;联系电话：" + dsMainResult.Tables[0].Rows[0]["linktel"].ToString() : lbHotel_ex.Text;
        txtExRemark.Text = dsMainResult.Tables[0].Rows[0]["HXremark"].ToString();

        if ("成功".Equals(dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString()))
        {
            lbFaxStatus.ForeColor = System.Drawing.Color.Green;
        }
        else if ("失败".Equals(dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString()))
        {
            lbFaxStatus.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lbFaxStatus.ForeColor = System.Drawing.Color.Black;
        }
        lbFaxStatus.Text = dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString();

        lbFaxStatusBK.Text = dsMainResult.Tables[0].Rows[0]["REFAXSTATUS"].ToString();

        if ("成功".Equals(dsMainResult.Tables[0].Rows[0]["CAFAXSTATUS"].ToString()))
        {
            lbCancelFax.ForeColor = System.Drawing.Color.Green;
        }
        else if ("失败".Equals(dsMainResult.Tables[0].Rows[0]["CAFAXSTATUS"].ToString()))
        {
            lbCancelFax.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lbCancelFax.ForeColor = System.Drawing.Color.Black;
        }
        lbCancelFax.Text = dsMainResult.Tables[0].Rows[0]["CAFAXSTATUS"].ToString();

        lbCancelFaxBK.Text = dsMainResult.Tables[0].Rows[0]["RECAFAXSTATUS"].ToString();
        txtCofNum.Text = dsMainResult.Tables[0].Rows[0]["CONFIRM_NUM"].ToString();
        hidHotelID.Value = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();
        hidContactTel.Value = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();

        decimal iDay = decimal.Parse(lbOrderDays.Text);
        decimal iNum = (String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString())) ? 1 : decimal.Parse(dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString());
        decimal iPrice = (String.IsNullOrEmpty(dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString())) ? 0 : decimal.Parse(dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString());
        decimal decPrice = Math.Round((iPrice / iDay) / iNum, 1);

        lbTotalPrice.Text = dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString() + "元" + "（" + decPrice.ToString() + "元/间夜）";
        //lbMemoHis.Text = SetMemoVal(_lmSystemLogEntity.FogOrderID);
        lblSalesMG.Text = SetHotelSalesInfo(dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString());
        //BindViewCSSystemLogDetail();
        ddpCanelReson.SelectedIndex = 0;
        SetOrderSettingControlVal(dsMainResult.Tables[0].Rows[0]["price_code"].ToString(), dsMainResult.Tables[0].Rows[0]["book_status_other"].ToString(), dsMainResult.Tables[0].Rows[0]["order_cancle_reason"].ToString());
        txtBOOK_REMARK.Text = "";
        //tbDetail.Style.Add("display", "");
        //tbControl.Style.Add("display", "");
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenListAj()", true);
    }

    [WebMethod]
    public static string SetMemoVal(string strKey, string ReType)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.MemoKey = strKey;
        string json = "";
        if (String.IsNullOrEmpty(strMemo) || "1".Equals(ReType))
        {
            DataSet dsResult = LmSystemLogBP.OrderActionHis(_lmSystemLogEntity).QueryResult;
            try
            {
                if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    json = ToJson(dsResult.Tables[0]);
                }
                else
                {
                    json = "[{\"OD_STATUS\":\"\",\"REMARK\":\"\",\"EVENT_USER\":\"\",\"EVENT_TIME\":\"\"}]";
                }
            }
            catch (Exception ex)
            {
                json = "[{\"OD_STATUS\":\"\",\"REMARK\":\"\",\"EVENT_USER\":\"\",\"EVENT_TIME\":\"\"}]";
            }
            strMemo = json;
        }
        else
        {
            json = strMemo;
        }
        return json;

        //if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        //{
        //    string strTemp = "";
        //    for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
        //    {
        //        strTemp = dsResult.Tables[0].Rows[i]["EVENT_TYPE"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["OD_STATUS"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["EVENT_USER"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["EVENT_TIME"].ToString().Trim() + " " + dsResult.Tables[0].Rows[i]["REMARK"].ToString().Trim();
        //        strResult = strResult + strTemp + "<br/><br/>";
        //    }
        //}

        //lbMemoHis.Text = strResult;
    }

    [WebMethod]
    public static string SetImagePreviewList(string strKey, string ReType)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = strKey;
        _lmSystemLogEntity.SendFaxType = ReType;
        string json = "";
        DataSet dsResult = LmSystemLogBP.OrderFaxHis(_lmSystemLogEntity).QueryResult;
        try
        {
            if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
            {
                json = ToJson(dsResult.Tables[0]);
            }
            else
            {
                json = "[{\"FAXBDT\":\"\",\"FAXNID\":\"\",\"FAXUNID\":\"\",\"FAXID\":\"\",\"FAXDT\":\"\",\"FAXTST\":\"\",\"FAXBST\":\"\",\"FAXTURL\":\"\",\"FAXBURL\":\"\"}]";
            }
        }
        catch (Exception ex)
        {
            json = "[{\"FAXBDT\":\"\",\"FAXNID\":\"\",\"FAXUNID\":\"\",\"FAXID\":\"\",\"FAXDT\":\"\",\"FAXTST\":\"\",\"FAXBST\":\"\",\"FAXTURL\":\"\",\"FAXBURL\":\"\"}]";
        }

        return json;
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
                string strValue = drc[i][j].ToString().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\"", "&quot;");
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

    private string SetHotelExInfo(string HXlinkman, string HXlinktel)
    {
        int iHour = DateTime.Now.Hour;
        string strResult = (!String.IsNullOrEmpty(HXlinkman) && HXlinkman.Split('|').Length == 24) ? HXlinkman.Split('|')[iHour].ToString() : "";
        strResult = strResult + ((!String.IsNullOrEmpty(HXlinktel) && HXlinktel.Split('|').Length == 24) ? "&nbsp;&nbsp;&nbsp;联系电话：" + HXlinktel.Split('|')[iHour].ToString() : "");
        return strResult;
    }

    private string SetVendorVal(string vendor)
    {
        string strVendVal = "";

        switch (vendor)
        {
            case "HUBS1":
                strVendVal = "HUBS1";
                break;
            case "MOTEL168":
                strVendVal = "莫泰168";
                break;
            case "HOMEINNS":
                strVendVal = "如家";
                break;
            case "PODINNS":
                strVendVal = "布丁";
                break;
            case "CTRIP":
                strVendVal = "携程";
                break;
            case "ELONG":
                strVendVal = "艺龙";
                break;
            case "HVP":
                strVendVal = "天海路";
                break;
            default:
                strVendVal = "天海路";
                break;
        }

        return strVendVal;
    }

    private string SetHotelSalesInfo(string HotelID)
    {
        string strResult = "";
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.HotelID = HotelID;

        DataSet dsResult = LmSystemLogBP.OrderOperationSalesSelect(_lmSystemLogEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            strResult = dsResult.Tables[0].Rows[0]["User_DspName"].ToString().Trim() + "&nbsp;&nbsp;&nbsp;" + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Trim();
        }
        return strResult;
    }

    private void SetOrderSettingControlVal(string strPriceCode, string strVal, string canelval)
    {
        rdbCofRe.Checked = false;
        if (!"LMBAR".Equals(strPriceCode.Trim().ToUpper()))
        {
            rdbCofIn.Visible = true;
            rdbCofRe.Checked = true;
            if ("4".Equals(strVal))
            {
                rdbCofIn.Checked = true;
                rdbCofCal.Checked = false;
            }
            else if ("9".Equals(strVal))
            {
                rdbCofIn.Checked = false;
                rdbCofCal.Checked = true;
            }
            else
            {
                rdbCofIn.Checked = false;
                rdbCofCal.Checked = false;
            }
        }
        else
        {
            //rdbCofIn.Visible = false;//原LMBAR不需要进行确认
            rdbCofIn.Checked = false;
            rdbCofCal.Checked = false;
            rdbCofRe.Checked = true;
        }


        if ("LMBAR".Equals(strPriceCode.Trim().ToUpper()))
        {
            trComfIn.Style.Add("display", "none");
            trCanlReson.Style.Add("display", "none");
        }
        else if (!"LMBAR".Equals(strPriceCode.Trim().ToUpper()) && "4".Equals(strVal))
        {
            trComfIn.Style.Add("display", "");
            trCanlReson.Style.Add("display", "none");
        }
        else if (!"LMBAR".Equals(strPriceCode.Trim().ToUpper()) && "9".Equals(strVal))
        {
            trComfIn.Style.Add("display", "none");
            trCanlReson.Style.Add("display", "");
        }
        else
        {
            //trComfIn.Style.Add("display", "none");//LMBAR确认  酒店确认号
            //trCanlReson.Style.Add("display", "none");//LMBAR确认  取消原因
        }

        if (!"LMBAR".Equals(strPriceCode.Trim().ToUpper()) && ddpCanelReson.Items.FindByValue(canelval) != null)
        {
            ddpCanelReson.SelectedValue = canelval;
        }
    }

    private DataTable GetCanelResonData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CanelReson");
        dt.Columns.Add("CanelResonDis");

        for (int i = 0; i < 10; i++)
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
                    dr["CanelReson"] = "OCDISB";
                    dr["CanelResonDis"] = "骚扰订单";
                    break;
                case "9":
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

    private DataTable GetPriceCodeData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PriceCode");
        dt.Columns.Add("PriceName");

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["PriceCode"] = "LMBAR";
                    dr["PriceName"] = "LMBAR";
                    break;
                case "1":
                    dr["PriceCode"] = "LMBAR2";
                    dr["PriceName"] = "LMBAR2";
                    break;
                default:
                    dr["PriceCode"] = "";
                    dr["PriceName"] = "";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
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

    protected void btnSet_Click(object sender, EventArgs e)
    {
        dvErrorInfo.InnerHtml = "";
        imgAlert.Src = "";
        string strOrderBookStatus = string.Empty;

        if (String.IsNullOrEmpty(hidOrderID.Value.Trim()))
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "100px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        if (String.IsNullOrEmpty(txtBOOK_REMARK.Text.Trim()))
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "100px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorEmptyRemark").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        if (!String.IsNullOrEmpty(txtBOOK_REMARK.Text.Trim()) && (StringUtility.Text_Length(txtBOOK_REMARK.Text.ToString().Trim()) > 250))
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "10px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorRemark").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        if (rdbCofRe.Checked)
        {
            trComfIn.Style.Add("display", "none");
            trCanlReson.Style.Add("display", "none");

            imgAlert.Src = "../../Styles/images/suc.png";
            dvImg.Style.Add("margin-left", "100px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateReSuccess").ToString();

            OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
            _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
            OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
            orderinfoEntity.EventType = "订单确认";
            orderinfoEntity.ORDER_NUM = hidOrderID.Value;
            orderinfoEntity.OdStatus = "备注";
            orderinfoEntity.REMARK = txtBOOK_REMARK.Text.Trim();
            orderinfoEntity.ActionID = "";
            orderinfoEntity.CANNEL = "";
            _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
            CommonBP.InsertOrderActionHistoryList(_orderInfoEntity);
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenListAj()", true);
            return;
        }
        else
        {
            if (!"LMBAR".Equals(hidPriceCode.Value.ToUpper()))
            {
                if (rdbCofIn.Checked)
                {
                    strOrderBookStatus = "4";
                }
                else if (rdbCofCal.Checked)
                {
                    strOrderBookStatus = "9";
                }
                else
                {
                    strOrderBookStatus = "4";
                }
            }
            else
            {
                strOrderBookStatus = "4";
            }

            if ("LMBAR".Equals(hidPriceCode.Value.ToUpper()))
            {
                trComfIn.Style.Add("display", "none");
                trCanlReson.Style.Add("display", "");
            }
            else if (!"LMBAR".Equals(hidPriceCode.Value.ToUpper()) && "9".Equals(strOrderBookStatus))
            {
                trComfIn.Style.Add("display", "none");
                trCanlReson.Style.Add("display", "");
            }
            else
            {
                trComfIn.Style.Add("display", "");
                trCanlReson.Style.Add("display", "none");
            }
        }


        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        _lmSystemLogEntity.OrderBookStatus = strOrderBookStatus;
        _lmSystemLogEntity.CanelReson = ddpCanelReson.SelectedValue.Trim();
        _lmSystemLogEntity.BookRemark = txtBOOK_REMARK.Text.Trim();
        _lmSystemLogEntity.FollowUp = (chkFollowUp.Checked) ? "1" : "0";
        _lmSystemLogEntity.ActionID = (!"LMBAR".Equals(hidPriceCode.Value.ToUpper())) ? txtCofNum.Text.Trim() : "";
        int iResult = LmSystemLogBP.SaveOrderOperation(_lmSystemLogEntity);

        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单确认-保存";
        commonDBEntity.Event_ID = hidHotelID.Value + "-" + _lmSystemLogEntity.FogOrderID;
        string conTent = GetLocalResourceObject("EventSaveMessage").ToString();

        conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.OrderBookStatus, _lmSystemLogEntity.BookRemark, _lmSystemLogEntity.CanelReson, _lmSystemLogEntity.FollowUp);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            imgAlert.Src = "../../Styles/images/suc.png";
            dvImg.Style.Add("margin-left", "100px");
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();

            OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
            _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
            OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
            orderinfoEntity.EventType = "订单确认";
            orderinfoEntity.ORDER_NUM = hidOrderID.Value;
            orderinfoEntity.OdStatus = SetActionTypeVal(strOrderBookStatus);
            orderinfoEntity.REMARK = txtBOOK_REMARK.Text.Trim();
            orderinfoEntity.ActionID = (!"LMBAR".Equals(hidPriceCode.Value.ToUpper())) ? txtCofNum.Text.Trim() : "";
            orderinfoEntity.CANNEL = ("9".Equals(strOrderBookStatus) || ("LMBAR".Equals(hidPriceCode.Value.ToUpper()) && "4".Equals(strOrderBookStatus))) ? ddpCanelReson.SelectedValue.Trim() : "";
            _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
            CommonBP.InsertOrderActionHistoryList(_orderInfoEntity);
            RestControlVal();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenListAj()", true);
        }
        else if (iResult == 2)
        {
            imgAlert.Src = "../../Styles/images/err.png";
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateLockErr").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateLockErr").ToString();
            dvImg.Style.Add("margin-left", "30px");
        }
        else
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "100px");
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    private void RestControlVal()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsMainResult = LmSystemLogBP.OrderComfirmManagerDetailRestVal(_lmSystemLogEntity).QueryResult;
        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            return;
        }

        lbBOOK_STATUS.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        lbBOOK_REMARK.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();
        lbFaxStatus.Text = dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString();
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

    protected void btnSetUnlock_Click(object sender, EventArgs e)
    {
        //SetUnlock();
        //hidOrderID.Value = "";

        SetUnHotellock();
        hidOrderID.Value = "";
        hidUnlockHotelID.Value = "";
        hidHotelID.Value = "";
        BindOrderConfirmListGrid();
    }

    private void SetUnlock()
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

            commonDBEntity.Event_Type = "订单确认-解锁";
            commonDBEntity.Event_ID = hidOrderID.Value.Trim();
            string conTent = GetLocalResourceObject("EventUnLockMessage").ToString();

            conTent = string.Format(conTent, hidOrderID.Value.Trim(), _orderInfoEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;

            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            BindOrderConfirmListGrid();
        }
    }

    [WebMethod]
    public static string UnlockOrder(string OrderID)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = OrderID;

        DataSet dsMainResult = LmSystemLogBP.ChkOrderOperationSelect(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            //imgAlert.Src = "../../Styles/images/err.png";
            //dvImg.Style.Add("margin-left", "100px");
            //dvErrorInfo.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
            return "../../Styles/images/err.png" + "|" + "120px" + "|" + "该订单信息不存在，请确认！" + "|";
        }

        string strMsg = LmSystemLogBP.UnlockOrderConfirmControl(_lmSystemLogEntity).ErrorMSG;
        if (!String.IsNullOrEmpty(strMsg))
        {
            //dvErrorInfo.InnerHtml = strMsg;
            //imgAlert.Src = "../../Styles/images/err.png";
            //dvImg.Style.Add("margin-left", "100px");
            return "../../Styles/images/err.png" + "|" + "120px" + "|" + strMsg + "|";
        }
        else
        {
            CommonEntity _commonEntity = new CommonEntity();
            _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单确认-强制锁定";
            commonDBEntity.Event_ID = _lmSystemLogEntity.FogOrderID;
            string conTent = "订单确认强制锁定 - 订单ID：{0}  操作人：{1}";// GetLocalResourceObject("EventUnLockMessage").ToString();

            conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;
            commonDBEntity.Event_Result = "&nbsp;&nbsp;订单确认锁定成功！&nbsp;";// GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            //dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            //imgAlert.Src = "../../Styles/images/suc.png";
            //dvImg.Style.Add("margin-left", "100px");
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);

            return "../../Styles/images/suc.png" + "|" + "120px" + "|" + "订单确认锁定成功！" + "|" + UserSession.Current.UserAccount;

            //lbActionUser.Text = UserSession.Current.UserAccount;
        }
        //return json;
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        dvErrorInfo.InnerHtml = "";

        if (rdbCofRe.Checked)
        {
            trComfIn.Style.Add("display", "none");
            trCanlReson.Style.Add("display", "none");
        }
        else
        {
            string strOrderBookStatus = string.Empty;
            if (!"LMBAR".Equals(hidPriceCode.Value.ToUpper()))
            {
                if (rdbCofIn.Checked)
                {
                    strOrderBookStatus = "4";
                }
                else if (rdbCofCal.Checked)
                {
                    strOrderBookStatus = "9";
                }
                else
                {
                    strOrderBookStatus = "4";
                }
            }
            else
            {
                strOrderBookStatus = "4";
            }

            if ("LMBAR".Equals(hidPriceCode.Value.ToUpper()))
            {
                trComfIn.Style.Add("display", "none");
                trCanlReson.Style.Add("display", "");
            }
            else if (!"LMBAR".Equals(hidPriceCode.Value.ToUpper()) && "9".Equals(strOrderBookStatus))
            {
                trComfIn.Style.Add("display", "none");
                trCanlReson.Style.Add("display", "");
            }
            else
            {
                trComfIn.Style.Add("display", "");
                trCanlReson.Style.Add("display", "none");
            }
        }

        if (String.IsNullOrEmpty(hidOrderID.Value.Trim()))
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "100px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsMainResult = LmSystemLogBP.ChkOrderOperationSelect(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            //hidOrderID.Value = "";
            //btnPrint.Visible = false;
            //btnSendFax.Visible = false;
            //btnSet.Visible = false;
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "100px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        string strMsg = LmSystemLogBP.UnlockOrderConfirmControl(_lmSystemLogEntity).ErrorMSG;
        if (!String.IsNullOrEmpty(strMsg))
        {
            //hidOrderID.Value = "";
            //btnPrint.Visible = false;
            //btnSendFax.Visible = false;
            //btnSet.Visible = false;
            //detailMessageContent.InnerHtml = strMsg;
            dvErrorInfo.InnerHtml = strMsg;
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "100px");
        }
        else
        {
            _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单确认-强制锁定";
            commonDBEntity.Event_ID = _lmSystemLogEntity.FogOrderID;
            string conTent = GetLocalResourceObject("EventUnLockMessage").ToString();

            conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.LogMessages.Username);
            commonDBEntity.Event_Content = conTent;

            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateUnLockSuccess").ToString();
            imgAlert.Src = "../../Styles/images/suc.png";
            dvImg.Style.Add("margin-left", "100px");
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            lbActionUser.Text = UserSession.Current.UserAccount;
        }
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    protected void btnNextOd_Click(object sender, EventArgs e)
    {
        //SetUnlock();
        string strOldOrder = hidOrderID.Value;
        for (int i = 0; i < gridViewCSList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridViewCSList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                if (hidOrderID.Value.Equals(gridViewCSList.DataKeys[i].Values[3].ToString()) && i + 2 <= gridViewCSList.Rows.Count)
                {
                    hidOrderID.Value = gridViewCSList.DataKeys[i + 1].Values[3].ToString();
                    if (ChkOrderLock(hidOrderID.Value))
                    {
                        strOldOrder = hidOrderID.Value;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        //trCanlReson.Style.Add("display", "none");
        //tbDetail.Style.Add("display", "none");
        //tbControl.Style.Add("display", "none");

        spRekButton.Style.Add("display", "none");

        if (strOldOrder.Equals(hidOrderID.Value))
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "120px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("WarningLastOrderMessage").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenListAj()", true);
            return;
        }
        else
        {
            dvErrorInfo.InnerHtml = "";
            imgAlert.Src = "";
            dvImg.Style.Add("margin-left", "120px");
        }

        hidUnlockHotelID.Value = hidHotelID.Value.ToString();
        SetUnHotellock();
        hidUnlockHotelID.Value = "";
        BindViewCSSystemLogMain();
    }

    private bool ChkOrderLock(string strHotelID)
    {
        OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.OrderID = hidOrderID.Value;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        int iResult = OrderInfoBP.ChkOrderLock(_orderInfoEntity).Result;

        if (iResult == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnModifyRemark_Click(object sender, EventArgs e)
    {
        dvErrorInfo.InnerHtml = "";
        imgAlert.Src = "";
        if (rdbCofRe.Checked)
        {
            trComfIn.Style.Add("display", "none");
            trCanlReson.Style.Add("display", "none");
        }
        else
        {
            string strOrderBookStatus = string.Empty;
            if (!"LMBAR".Equals(hidPriceCode.Value.ToUpper()))
            {
                if (rdbCofIn.Checked)
                {
                    strOrderBookStatus = "4";
                }
                else if (rdbCofCal.Checked)
                {
                    strOrderBookStatus = "9";
                }
                else
                {
                    strOrderBookStatus = "4";
                }
            }
            else
            {
                strOrderBookStatus = "4";
            }

            if ("LMBAR".Equals(hidPriceCode.Value.ToUpper()))
            {
                trComfIn.Style.Add("display", "none");
                trCanlReson.Style.Add("display", "");
            }
            else if (!"LMBAR".Equals(hidPriceCode.Value.ToUpper()) && "9".Equals(strOrderBookStatus))
            {
                trComfIn.Style.Add("display", "none");
                trCanlReson.Style.Add("display", "");
            }
            else
            {
                trComfIn.Style.Add("display", "");
                trCanlReson.Style.Add("display", "none");
            }
        }

        if (String.IsNullOrEmpty(hidOrderID.Value.Trim()))
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "100px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        if (!String.IsNullOrEmpty(txtExRemark.Text.Trim()) && (StringUtility.Text_Length(txtExRemark.Text.ToString().Trim()) > 600))
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "10px");
            dvErrorInfo.InnerHtml = GetLocalResourceObject("ErrorExRemark").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.HotelID = hidHotelID.Value;
        _lmSystemLogEntity.BookRemark = txtExRemark.Text.Trim();

        int iResult = LmSystemLogBP.SaveHotelExRemark(_lmSystemLogEntity);

        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单确认-酒店备注保存";
        commonDBEntity.Event_ID = hidHotelID.Value + "-" + _lmSystemLogEntity.FogOrderID;
        string conTent = GetLocalResourceObject("EventExSaveMessage").ToString();

        conTent = string.Format(conTent, _lmSystemLogEntity.HotelID, _lmSystemLogEntity.BookRemark);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            imgAlert.Src = "../../Styles/images/suc.png";
            dvImg.Style.Add("margin-left", "100px");
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateExSuccess").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateExSuccess").ToString();
        }
        else if (iResult == 2)
        {
            imgAlert.Src = "../../Styles/images/err.png";
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateLockExErr").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateLockExErr").ToString();
            dvImg.Style.Add("margin-left", "10px");
        }
        else
        {
            imgAlert.Src = "../../Styles/images/err.png";
            dvImg.Style.Add("margin-left", "10px");
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateExError").ToString();
            dvErrorInfo.InnerHtml = GetLocalResourceObject("UpdateExError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    [WebMethod]
    public static string ModifyRemark(string HotelID, string Remark, string OrderID)
    {
        string strResult = string.Empty;
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();

        if (!String.IsNullOrEmpty(Remark.Trim()) && (StringUtility.Text_Length(Remark.ToString().Trim()) > 600))
        {
            strResult = "../../Styles/images/err.png" + "|" + "10px" + "|" + "操作备注长度不能超过85位中文字符，请修改！";
            return strResult;
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.HotelID = HotelID;
        _lmSystemLogEntity.BookRemark = Remark.Trim();

        int iResult = LmSystemLogBP.SaveHotelExRemark(_lmSystemLogEntity);

        CommonEntity _commonEntity = new CommonEntity();
        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单确认-酒店备注保存";
        commonDBEntity.Event_ID = HotelID + "-" + OrderID;
        string conTent = "订单确认保存 - 酒店ID：{0}  备注：{1} ";

        conTent = string.Format(conTent, _lmSystemLogEntity.HotelID, _lmSystemLogEntity.BookRemark);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = "订单确认-酒店备注保存成功！";
            strResult = "../../Styles/images/suc.png" + "|" + "100px" + "|" + "订单确认-酒店备注保存成功！";
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = "该酒店执行信息不存在，请至酒店基础信息维护！";
            strResult = "../../Styles/images/err.png" + "|" + "10px" + "|" + "该酒店执行信息不存在，请至酒店基础信息维护！";
        }
        else
        {
            commonDBEntity.Event_Result = "订单确认-酒店备注保存失败，请稍微再试！";
            strResult = "../../Styles/images/err.png" + "|" + "10px" + "|" + "订单确认-酒店备注保存失败，请稍微再试！";
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        return strResult;
    }

    protected void btnFindSend_Click(object sender, EventArgs e) { }

    protected void btnFindReturn_Click(object sender, EventArgs e) { }

    protected void btnReturnFax_Click(object sender, EventArgs e) { }

    protected void btnSendFax_Click(object sender, EventArgs e)
    {
        dvErrorInfo.InnerHtml = SendFaxService();
        //imgAlert.Src = "../../Styles/images/err.png";
        dvImg.Style.Add("margin-left", "30px");
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    [WebMethod]
    public static string ReSendFaxOrder(string OrderID, string HotelID)
    {
        string strResult = string.Empty;
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = OrderID;
        _lmSystemLogEntity.HotelID = HotelID;
        _lmSystemLogEntity.SendFaxType = "1";
        _lmSystemLogEntity.ObjectID = OrderID;
        _lmSystemLogEntity = LmSystemLogBP.SendFaxService(_lmSystemLogEntity);

        int iResult = _lmSystemLogEntity.Result;

        CommonEntity _commonEntity = new CommonEntity();
        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单确认-发送传真";
        commonDBEntity.Event_ID = OrderID;
        string conTent = "单确认重发传真 - 酒店ID：{0}  订单ID：{1}";

        conTent = string.Format(conTent, HotelID, OrderID);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = "订单确认重发传真提交成功，发送中！";
            strResult = "../../Styles/images/suc.png" + "|" + "70px" + "|" + "订单确认重发传真提交成功，发送中！";
        }
        else
        {
            commonDBEntity.Event_Result = _lmSystemLogEntity.ErrorMSG;
            strResult = "../../Styles/images/err.png" + "|" + "70px" + "|" + "订单确认重发传真失败，请稍微再试！";
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        return strResult;
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
            DataTable detailDtResult = BindViewCSSystemLogDetail(baseDtResult.Rows[0]["fog_order_num"].ToString(), baseDtResult.Rows[0]["price_code"].ToString());
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
            if ("递交失败，请重试！".Equals(sendFaxToServerBack))
            {
                imgAlert.Src = "../../Styles/images/err.png";
            }
            else
            {
                imgAlert.Src = "../../Styles/images/suc.png";
            }
            return sendFaxToServerBack;
        }

        imgAlert.Src = "../../Styles/images/err.png";
        return "递交失败，请重试！";
        #endregion
    }

    private DataTable BindViewCSSystemLogDetail(string orderID, string priceCode)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = orderID;
        _lmSystemLogEntity.PriceCode = priceCode;

        DataSet dsDetailResult = LmSystemLogBP.OrderOperationDetailSelect(_lmSystemLogEntity).QueryResult;

        if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 0)
        {
            return dsDetailResult.Tables[0];
        }
        return new DataTable();
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

    protected void btnFindCancelFax_Click(object sender, EventArgs e) { }

    protected void btnReturnCalFax_Click(object sender, EventArgs e) { }

    private string SendFaxService()
    {
        string strSendFaxVendor = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SendFaxByVender"].ToString())) ? "0" : ConfigurationManager.AppSettings["SendFaxByVender"].ToString();

        if ("0".Equals(strSendFaxVendor))
        {
            return SendFaxInterface();
        }
        else
        {
            return AssemblyText();
        }
    }

    private string SendFaxInterface()
    {
        string strResult = "";
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;
        _lmSystemLogEntity.HotelID = hidHotelID.Value;
        _lmSystemLogEntity.SendFaxType = "1";
        _lmSystemLogEntity.ObjectID = hidOrderID.Value;
        _lmSystemLogEntity = LmSystemLogBP.SendFaxService(_lmSystemLogEntity);

        int iResult = _lmSystemLogEntity.Result;
        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单确认-发送传真";
        commonDBEntity.Event_ID = hidOrderID.Value;
        string conTent = GetLocalResourceObject("EventSendFaxMessage").ToString();

        conTent = string.Format(conTent, hidHotelID.Value, hidOrderID.Value);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SendSuccess").ToString();
            strResult = GetLocalResourceObject("SendSuccess").ToString();
            imgAlert.Src = "../../Styles/images/suc.png";
        }
        else
        {
            commonDBEntity.Event_Result = _lmSystemLogEntity.ErrorMSG;
            strResult = GetLocalResourceObject("SendError").ToString();//_lmSystemLogEntity.ErrorMSG;
            imgAlert.Src = "../../Styles/images/err.png";
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        return strResult;
    }

    [WebMethod]
    public static string GetHCorderList(string orderID, string hotelID, string contTel)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = orderID;
        _lmSystemLogEntity.HotelID = hotelID;
        _lmSystemLogEntity.ContactTel = contTel;
        string json = "";
        DataSet dsResult = LmSystemLogBP.GetHCorderList(_lmSystemLogEntity).QueryResult;
        try
        {
            if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
            {
                json = ToJson(dsResult.Tables[0]);
            }
            else
            {
                json = "[]";
            }
        }
        catch (Exception ex)
        {
            json = "[]";
        }
        return json;
    }

    [WebMethod]
    public static string RestOrderVal(string OrderID)
    {
        string strResult = string.Empty;
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = OrderID;

        DataSet dsMainResult = LmSystemLogBP.OrderComfirmManagerDetailRestVal(_lmSystemLogEntity).QueryResult;
        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            return strResult;
        }

        strResult += "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString() + "|";
        strResult += dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString() + "|";
        strResult += dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString();

        //lbBOOK_STATUS.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        //lbBOOK_REMARK.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();
        //lbFaxStatus.Text = dsMainResult.Tables[0].Rows[0]["FAXSTATUS"].ToString();
        return strResult;
    }

    [WebMethod]
    public static string SaveOrderList(string OrderList, string OrderID)
    {
        string strResult = string.Empty;
        string[] ODList = OrderList.Split(',');

        if (ODList.Length == 0)
        {
            strResult = "../../Styles/images/err.png" + "|" + "120px" + "|" + "未填写订单确认信息，请修改！";
            return strResult;
        }

        string[] ODDetail;
        string strRemarkTp = string.Empty;
        foreach (string tempOD in ODList)
        {
            if (!String.IsNullOrEmpty(tempOD))
            {
                ODDetail = tempOD.Split('_');
                if (ODDetail[0].Trim() == OrderID)
                {
                    if ("2".Equals(ODDetail[1].Trim()))//备注
                    {
                        strRemarkTp = ODDetail[2].Trim();
                    }
                    else
                    {
                        strRemarkTp = ODDetail[3].Trim();//确认可入住 Or 取消订单
                    }

                    if (String.IsNullOrEmpty(strRemarkTp))//判断操作备注
                    {
                        strResult = "../../Styles/images/err.png" + "|" + "120px" + "|" + "操作备注不能为空，请修改！";
                        return strResult;
                    }

                    if ((StringUtility.Text_Length(strRemarkTp) > 250))//判断操作备注
                    {
                        strResult = "../../Styles/images/err.png" + "|" + "70px" + "|" + "操作备注长度不能超过85位中文字符，请修改！";
                        return strResult;
                    }
                }
                else
                {
                    if ("1".Equals(ODDetail[1].Trim()))
                    {
                        //if (String.IsNullOrEmpty(ODDetail[2].Trim()))
                        //{
                        //    strResult = "../../Styles/images/err.png" + "|" + "70px" + "|" + "本酒店其他订单中存在确认号为空，请修改！";
                        //    return strResult;
                        //}

                        if (!String.IsNullOrEmpty(ODDetail[2].Trim()) && (StringUtility.Text_Length(ODDetail[2].Trim()) > 30))
                        {
                            strResult = "../../Styles/images/err.png" + "|" + "10px" + "|" + "本酒店其他订单中存在确认号超过30位字符，请修改！";
                            return strResult;
                        }
                    }
                    else if ("2".Equals(ODDetail[1].Trim()))
                    {
                        if (String.IsNullOrEmpty(ODDetail[2].Trim()))
                        {
                            strResult = "../../Styles/images/err.png" + "|" + "10px" + "|" + "本酒店其他订单中存在操作备注为空，请修改！";
                            return strResult;
                        }

                        if ((StringUtility.Text_Length(ODDetail[2].Trim()) > 250))
                        {
                            strResult = "../../Styles/images/err.png" + "|" + "10px" + "|" + "本酒店其他订单中存在操作备注超过85位中文字符，请修改！";
                            return strResult;
                        }
                    }
                }
            }
        }

        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = OrderID;
        _lmSystemLogEntity.OrderList = OrderList;

        _lmSystemLogEntity = LmSystemLogBP.SaveConfirmOrderList(_lmSystemLogEntity);
        int iResult = _lmSystemLogEntity.Result;
        string errorMSG = _lmSystemLogEntity.ErrorMSG;

        CommonEntity _commonEntity = new CommonEntity();
        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单确认-保存";
        commonDBEntity.Event_ID = OrderID;
        string conTent = "订单确认保存 - 订单ID：{0}  同酒店订单列表：{1} ";

        conTent = string.Format(conTent, OrderID, OrderList);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = OrderList.TrimEnd(',').Split(',').Length.ToString() + "张订单确认保存成功！";
            strResult = "../../Styles/images/suc.png" + "|" + "120px" + "|" + OrderList.TrimEnd(',').Split(',').Length.ToString() + "张订单确认保存成功！";
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = errorMSG;
            strResult = "../../Styles/images/err.png" + "|" + "120px" + "|" + errorMSG;
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = errorMSG;
            strResult = "../../Styles/images/err.png" + "|" + "1px" + "|" + errorMSG;
        }
        else
        {
            commonDBEntity.Event_Result = "订单确认保存失败，请稍微再试！";
            strResult = "../../Styles/images/err.png" + "|" + "120px" + "|" + "订单确认保存失败，请稍微再试！";
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        return strResult;
    }


    [WebMethod]
    public static string GetHCorderViewList(string orderID, string hotelID, string contTel)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = orderID;
        _lmSystemLogEntity.HotelID = hotelID;
        _lmSystemLogEntity.ContactTel = contTel;
        string json = "";
        DataSet dsResult = LmSystemLogBP.GetHCorderViewList(_lmSystemLogEntity).QueryResult;
        try
        {
            if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
            {
                json = ToJson(dsResult.Tables[0]);
            }
            else
            {
                json = "[]";
            }
        }
        catch (Exception ex)
        {
            json = "[]";
        }
        return json;
    }
}