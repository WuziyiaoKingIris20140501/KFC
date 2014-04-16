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


public partial class WebUI_ReportCenter_OrderManageSearch : System.Web.UI.Page
{
    ReportCenterEntity _reportCenterEntity = new ReportCenterEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["startDate"] = "";
            ViewState["endDate"] = "";
            ViewState["salesID"] = "";
            ViewState["priceCode"] = "";
            BindChk();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(this.hidSelecUserID.Value.Trim()))
        {
            if (!hidSelecUserID.Value.Trim().Contains("[") || !hidSelecUserID.Value.Trim().Contains("]"))
            {
                messageContent.InnerHtml = "查询失败，选择人员不合法，请修改！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }
        }



        string startDate = this.dpCreateStart.Value + " 04:00:00";
        ViewState["startDate"] = startDate;
        string endDate = this.dpCreateEnd.Value + " 03:59:59";
        ViewState["endDate"] = endDate;
        string salesID = !string.IsNullOrEmpty(this.hidSelecUserID.Value) ? this.hidSelecUserID.Value.Substring((this.hidSelecUserID.Value.IndexOf('[') + 1), (this.hidSelecUserID.Value.IndexOf(']') - 1)) : "";
        ViewState["salesID"] = salesID;
        string priceCode = "";
        foreach (ListItem lt in chkPriceCode.Items)
        {
            if (lt.Selected)
            {
                priceCode = priceCode + lt.Value + ",";
            }
        }
        priceCode = priceCode.Trim(',');
        ViewState["priceCode"] = priceCode;

        BindGroupListGrid(startDate, endDate, salesID, priceCode);

        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "keys", "BtnCompleteStyle();", true);
    }

    private void BindGroupListGrid(string startDate, string endDate, string salesID, string priceCode)
    {
        DataTable dtOracleResult = GetOracleOrderList(startDate, endDate, priceCode);

        DataTable dtSQLResult = GetSqlOrderList(salesID, startDate, endDate);

        DataTable dtResult = HandleGridView(dtOracleResult, dtSQLResult);
        Session["DataSource"] = dtResult;
        gridViewCSReviewList.DataSource = dtResult;
        gridViewCSReviewList.DataKeyNames = new string[] { };//主键
        gridViewCSReviewList.DataBind();
    }

    public DataTable HandleGridView(DataTable dtOracleResult, DataTable dtSQLResult)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("FOG_ORDER_NUM");//订单号
        dtResult.Columns.Add("VENDOR");//订单来源
        dtResult.Columns.Add("HOTELNAME");//酒店名称
        dtResult.Columns.Add("CITYNAME");//城市名称
        dtResult.Columns.Add("CREATETIME");//预定日期
        dtResult.Columns.Add("INDATE");//入住日期
        dtResult.Columns.Add("ORDERCONFIRM");//订单确认人
        dtResult.Columns.Add("ORDERHANDLE");//订单处理人
        dtResult.Columns.Add("ORDERCONFIRMDATE");//订单确认时长
        dtResult.Columns.Add("FOGRESVSTATUS");//处理状态
        dtResult.Columns.Add("BOOKSTATUSOTHER");//订单状态
        dtResult.Columns.Add("ORDERCANCELREASON");//取消原因

        if (dtOracleResult != null && dtOracleResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtOracleResult.Rows.Count; i++)
            {
                string fogOrderNum = dtOracleResult.Rows[i]["FOG_ORDER_NUM"].ToString();

                DataRow row = dtResult.NewRow();
                row["FOG_ORDER_NUM"] = fogOrderNum; //订单号
                row["VENDOR"] = dtOracleResult.Rows[i]["VENDOR"].ToString(); //订单来源
                row["HOTELNAME"] = dtOracleResult.Rows[i]["prop_name_zh"].ToString(); //酒店名称
                row["CITYNAME"] = dtOracleResult.Rows[i]["name_cn"].ToString();//城市名称
                row["CREATETIME"] = dtOracleResult.Rows[i]["create_time"].ToString();//预定日期
                row["INDATE"] = dtOracleResult.Rows[i]["in_date"].ToString(); //入住日期

                DataRow[] rowsConfirm = dtSQLResult.Select("EVENT_FG_ID='" + fogOrderNum + "' and OD_STATUS='可入住'");
                if (rowsConfirm.Length > 0)
                    row["ORDERCONFIRM"] = rowsConfirm.Length > 0 ? rowsConfirm[rowsConfirm.Length - 1]["EVENT_USER"].ToString() : rowsConfirm[0]["EVENT_USER"].ToString();//订单确认人

                DataRow[] rowsHandle = dtSQLResult.Select("EVENT_FG_ID='" + fogOrderNum + "'");
                if (rowsHandle.Length > 0)
                    row["ORDERHANDLE"] = rowsHandle[0]["EVENT_USER"].ToString();//订单处理人

                row["ORDERCONFIRMDATE"] = dtOracleResult.Rows[i]["confirmTime"].ToString();//订单确认时长
                row["FOGRESVSTATUS"] = dtOracleResult.Rows[i]["fog_resvstatus"].ToString().Replace("0", "待酒店确认").Replace("1", "酒店已确认").Replace("2", "手工单");//处理状态   FOG订单状态  0：待酒店确认 1：酒店已确认  2手工单
                row["BOOKSTATUSOTHER"] = dtOracleResult.Rows[i]["book_status_other"].ToString().Replace("0", "新建").Replace("1", "预订成功等待确认").Replace("2", "新建入fog失败").Replace("3", "用户取消").Replace("4", "可入住已确认").Replace("5", "NO-SHOW").Replace("6", "已完成").Replace("7", "审核中").Replace("8", "离店").Replace("9", "CC取消");//订单状态   0 新建  1  预订成功等待确认 2新建入fog失败    3 用户取消    4 可入住已确认    5 NO-SHOW   6 已完成（超过入住时间）  7 审核中    8 离店     9 CC取消

                DataRow[] rowsCancle = dtSQLResult.Select("EVENT_FG_ID='" + fogOrderNum + "' and OD_STATUS='CC取消'");
                if (rowsCancle.Length > 0)
                    row["ORDERCANCELREASON"] = ConvertStr(rowsCancle.Length > 0 ? rowsCancle[rowsCancle.Length - 1]["CANNEL"].ToString() : rowsCancle[0]["CANNEL"].ToString());//取消原因

                dtResult.Rows.Add(row);
            }
        }

        return dtResult;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(Session["DataSource"] as DataTable);

            if (ds == null)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
                return;

            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
                return;
            }

            CommonFunction.ExportExcelForLM(ds);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
            return;
        }
    }

    protected void gridViewCSReviewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridViewCSReviewList.PageIndex = e.NewPageIndex;
        BindGroupListGrid(ViewState["startDate"].ToString(), ViewState["endDate"].ToString(), ViewState["salesID"].ToString(), ViewState["priceCode"].ToString());
    }

    public string ConvertStr(string str)
    {
        string returnStr = "";
        switch (str)
        {
            case "CRC01":
                returnStr = "满房";
                break;
            case "CRH09":
                returnStr = "酒店无人接听/无法接通";
                break;
            case "CRC06":
                returnStr = "酒店变价";
                break;
            case "CRH10":
                returnStr = "终止合作";
                break;
            case "CRH07":
                returnStr = "无协议";
                break;
            case "CRH11":
                returnStr = "不接外宾";
                break;
            case "CRC02":
                returnStr = "重复订单";
                break;
            case "CRG18":
                returnStr = "用户取消";
                break;
            case "OCDISB":
                returnStr = "骚扰订单";
                break;
            case "CRG99":
                returnStr = "其他";
                break;
            default:
                returnStr = "";
                break;
        }
        return returnStr;
    }

    /// <summary>
    /// 绑定checkBox
    /// </summary>
    public void BindChk()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Name");
        dt.Columns.Add("Value");

        for (int i = 0; i < 2; i++)
        {
            DataRow row = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    row["Name"] = "LMBAR";
                    row["Value"] = "LMBAR";
                    break;
                case "1":
                    row["Name"] = "LMBAR2";
                    row["Value"] = "LMBAR2";
                    break;
                default:
                    break;
            }
            dt.Rows.Add(row);
        }
        this.chkPriceCode.DataSource = dt;
        this.chkPriceCode.DataTextField = "Name";
        this.chkPriceCode.DataValueField = "Value";
        this.chkPriceCode.DataBind();
    }


    /// <summary>
    /// 获取订单列表
    /// </summary>
    /// <returns></returns>
    public DataTable GetOracleOrderList(string startDate, string endDate, string priceCode)
    {
        _reportCenterEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _reportCenterEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _reportCenterEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _reportCenterEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _reportCenterEntity.StartDTime = startDate;
        _reportCenterEntity.EndDTime = DateTime.Parse(endDate).AddDays(1).ToString().Replace("/", "-");
        _reportCenterEntity.PriceCode = priceCode;
        DataTable dtResult = ReportCenterBP.GetOracleOrderList(_reportCenterEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 获取操作日志
    /// </summary>
    /// <returns></returns>
    public DataTable GetSqlOrderList(string salesID, string startDate, string endDate)
    {
        _reportCenterEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _reportCenterEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _reportCenterEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _reportCenterEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _reportCenterEntity.Sales = salesID;
        _reportCenterEntity.StartDTime = startDate;
        _reportCenterEntity.EndDTime = DateTime.Parse(endDate).AddDays(1).AddHours(2).ToString().Replace("/", "-");
        DataTable dtResult = ReportCenterBP.GetSqlOrderList(_reportCenterEntity).QueryResult.Tables[0];

        return dtResult;
    }

}
