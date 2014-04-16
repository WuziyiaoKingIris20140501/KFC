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

public partial class ReviewLmSystemLogPageByNew : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControlData();
            ViewState["OrderID"] = "";
            ViewState["HotelID"] = "";
            ViewState["CityID"] = "";
            ViewState["PayCode"] = "";
            ViewState["BookStatus"] = "";
            ViewState["PayStatus"] = "";
            ViewState["PlatForm"] = "";
            ViewState["Ticket"] = "";
            ViewState["Mobile"] = "";
            ViewState["InStart"] = "";
            ViewState["InEnd"] = "";
            ViewState["Aprove"] = "";
            ViewState["StartDTime"] = DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00";
            ViewState["EndDTime"] = DateTime.Now.ToShortDateString() + " 23:59:59";

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);

            // 正确的属性设置方法
            gridViewCSReviewLmSystemLogList.Attributes.Add("SortExpression", "FOGCREATER");
            gridViewCSReviewLmSystemLogList.Attributes.Add("SortDirection", "ASC");

            BindReviewLmSystemLogListGrid();
            /*king*/
            //BindReviewLmSystemLogListGrid(1, gridViewCSReviewLmSystemLogList.PageSize);
            //AspNetPager1.PageSize = gridViewCSReviewLmSystemLogList.PageSize;
            //AspNetPager1.RecordCount = 10000;
        }
        //messageContent.InnerHtml = "";
    }

    private void InitControlData()
    {
        BindPlatFormDDL();
        BindRadioList();
    }

    private void BindPlatFormDDL()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsPlatForm = LmSystemLogBP.PlatFormSelect(_lmSystemLogEntity).QueryResult;

        DataRow drTempcity = dsPlatForm.Tables[0].NewRow();
        drTempcity["PLATFORMCODE"] = DBNull.Value;
        drTempcity["PLATFORMNAME"] = "不限制";
        dsPlatForm.Tables[0].Rows.InsertAt(drTempcity, 0);

        ddpPlatForm.DataTextField = "PLATFORMNAME";
        ddpPlatForm.DataValueField = "PLATFORMCODE";
        ddpPlatForm.DataSource = dsPlatForm;
        ddpPlatForm.DataBind();
    }

    private void BindRadioList()
    {
        DataTable dtBook = GetBookDataTable();
        ddpBookStatus.DataSource = dtBook;
        ddpBookStatus.DataTextField = "BOOK_STATUS_TEXT";
        ddpBookStatus.DataValueField = "BOOK_STATUS";
        ddpBookStatus.DataBind();
        ddpBookStatus.SelectedIndex = 0;

        DataTable dtPay = GetPayDataTable();
        ddpPayStatus.DataSource = dtPay;
        ddpPayStatus.DataTextField = "PAY_STATUS_TEXT";
        ddpPayStatus.DataValueField = "PAY_STATUS";
        ddpPayStatus.DataBind();
        ddpPayStatus.SelectedIndex = 0;

        DataTable dtPayCode = GetPayCodeDataTable();
        ddpPayCode.DataSource = dtPayCode;
        ddpPayCode.DataTextField = "PAY_CODE_TEXT";
        ddpPayCode.DataValueField = "PAY_CODE";
        ddpPayCode.DataBind();
        ddpPayCode.SelectedIndex = 0;

        DataTable dtTicket = GetTicketDataTable();
        ddpTicket.DataSource = dtTicket;
        ddpTicket.DataTextField = "TICKET_CODE_TEXT";
        ddpTicket.DataValueField = "TICKET_CODE";
        ddpTicket.DataBind();
        ddpTicket.SelectedIndex = 0;

        DataTable dtAprove = GetAproveDataTable();
        ddpAprove.DataSource = dtAprove;
        ddpAprove.DataTextField = "APROVE_CODE_TEXT";
        ddpAprove.DataValueField = "APROVE_CODE";
        ddpAprove.DataBind();
        ddpAprove.SelectedIndex = 0;
    }

    private DataTable GetAproveDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("APROVE_CODE");
        DataColumn BookStatusText = new DataColumn("APROVE_CODE_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["APROVE_CODE"] = "";
        dr0["APROVE_CODE_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["APROVE_CODE"] = "5";
                    dr["APROVE_CODE_TEXT"] = "No-Show";
                    break;
                case "1":
                    dr["APROVE_CODE"] = "7";
                    dr["APROVE_CODE_TEXT"] = "入住中";
                    break;
                case "2":
                    dr["APROVE_CODE"] = "8";
                    dr["APROVE_CODE_TEXT"] = "已离店";
                    break;
                default:
                    dr["APROVE_CODE"] = "";
                    dr["APROVE_CODE_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetPayCodeDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("PAY_CODE");
        DataColumn BookStatusText = new DataColumn("PAY_CODE_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["PAY_CODE"] = "";
        dr0["PAY_CODE_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["PAY_CODE"] = "LMBAR";
                    dr["PAY_CODE_TEXT"] = "LMBAR";
                    break;
                case "1":
                    dr["PAY_CODE"] = "LMBAR2";
                    dr["PAY_CODE_TEXT"] = "LMBAR2";
                    break;
                case "2":
                    dr["PAY_CODE"] = "BAR";
                    dr["PAY_CODE_TEXT"] = "BAR/BARB";
                    break;
                default:
                    dr["PAY_CODE"] = "";
                    dr["PAY_CODE_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetTicketDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("TICKET_CODE");
        DataColumn BookStatusText = new DataColumn("TICKET_CODE_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["TICKET_CODE"] = "";
        dr0["TICKET_CODE_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            dr["TICKET_CODE"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["TICKET_CODE_TEXT"] = "未使用优惠券";
                    break;
                case "1":
                    dr["TICKET_CODE_TEXT"] = "使用优惠券";
                    break;
                default:
                    dr["BOOK_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetBookDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("BOOK_STATUS");
        DataColumn BookStatusText = new DataColumn("BOOK_STATUS_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["BOOK_STATUS"] = "";
        dr0["BOOK_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 6; i++)
        {
            DataRow dr = dt.NewRow();
            dr["BOOK_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["BOOK_STATUS_TEXT"] = "新建";
                    break;
                case "1":
                    dr["BOOK_STATUS_TEXT"] = "新建成功";
                    break;
                case "2":
                    dr["BOOK_STATUS_TEXT"] = "新建失败";
                    break;
                case "3":
                    dr["BOOK_STATUS_TEXT"] = "超时";
                    break;
                case "4":
                    dr["BOOK_STATUS_TEXT"] = "订单取消";
                    break;
                case "5":
                    dr["BOOK_STATUS_TEXT"] = "成功";
                    break;
                case "6":
                    dr["BOOK_STATUS_TEXT"] = "已完成";
                    break;
                default:
                    dr["BOOK_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetPayDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn PayStatus = new DataColumn("PAY_STATUS");
        DataColumn PayStatusText = new DataColumn("PAY_STATUS_TEXT");

        dt.Columns.Add(PayStatus);
        dt.Columns.Add(PayStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["PAY_STATUS"] = "";
        dr0["PAY_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);
        for (int i = 0; i < 6; i++)
        {
            DataRow dr = dt.NewRow();
            dr["PAY_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["PAY_STATUS_TEXT"] = "未支付";
                    break;
                case "1":
                    dr["PAY_STATUS_TEXT"] = "支付成功";
                    break;
                case "2":
                    dr["PAY_STATUS_TEXT"] = "等待支付";
                    break;
                case "3":
                    dr["PAY_STATUS_TEXT"] = "支付中";
                    break;
                case "4":
                    dr["PAY_STATUS_TEXT"] = "支付失败";
                    break;
                case "5":
                    dr["PAY_STATUS_TEXT"] = "支付确认中";
                    break;
                case "6":
                    dr["PAY_STATUS_TEXT"] = "异常取消";
                    break;
                default:
                    dr["PAY_STATUS_TEXT"] = "未支付";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    //protected void AspNetPager1_PageChanged(object src, EventArgs e)
    //{
    //    //GridView1.DataSource = SqlHelper.ExecuteReader(CommandType.StoredProcedure, ConfigurationManager.AppSettings["pagedSPName"],
    //    //    new SqlParameter("@startIndex", AspNetPager1.StartRecordIndex),
    //    //    new SqlParameter("@endIndex", AspNetPager1.EndRecordIndex));
    //    //GridView1.DataBind();

    //    BindReviewLmSystemLogListGrid(AspNetPager1.CurrentPageIndex, gridViewCSReviewLmSystemLogList.PageSize);
    //}

    //private void BindReviewLmSystemLogListGrid(int startRecord, int maxRecord)
    private void BindReviewLmSystemLogListGrid()
    {
        //messageContent.InnerHtml = "";

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        _lmSystemLogEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
        _lmSystemLogEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
        _lmSystemLogEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
        _lmSystemLogEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
        _lmSystemLogEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
        _lmSystemLogEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
        _lmSystemLogEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
        _lmSystemLogEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();

        _lmSystemLogEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
        //_lmSystemLogEntity.PageCurrent = startRecord;
        //_lmSystemLogEntity.PageSize = maxRecord;
        DataSet dsResult = LmSystemLogBP.ReviewSelectByNew(_lmSystemLogEntity).QueryResult;


        // 获取GridView排序数据列及排序方向
        string sortExpression = gridViewCSReviewLmSystemLogList.Attributes["SortExpression"];
        string sortDirection = gridViewCSReviewLmSystemLogList.Attributes["SortDirection"];
        // 根据GridView排序数据列及排序方向设置显示的默认数据视图
        if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
        {
            dsResult.Tables[0].DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
        }

        gridViewCSReviewLmSystemLogList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "EVENTID" };//主键
        gridViewCSReviewLmSystemLogList.DataBind();
    }

    protected void gridViewCSReviewLmSystemLogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    //    //this.gridViewRegion.PageIndex = e.NewPageIndex;
    //    //BindGridView();

    //    //执行循环，保证每条数据都可以更新
    //    for (int i = 0; i <= gridViewCSChannelList.Rows.Count; i++)
    //    {
    //        //首先判断是否是数据行
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //当鼠标停留时更改背景色
    //            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
    //            //当鼠标移开时还原背景色
    //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
    //        }
    //    }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["OrderID"] = txtOrderID.Value.Trim();
        string strHotel = hidHotel.Value.ToString().Trim(); //wctHotel.AutoResult.ToString().Trim();
        string strCity = hidCity.Value.ToString().Trim(); //wctCity.AutoResult.ToString().Trim();
        ViewState["HotelID"] = (strHotel.IndexOf(']') > 0) ? strHotel.Substring(0, strHotel.IndexOf(']')).Trim('[').Trim(']') : strHotel;
            //(!String.IsNullOrEmpty(strHotel)) ? strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1)) : "";
        ViewState["CityID"] = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity; 
        //(!String.IsNullOrEmpty(strCity)) ? strCity.Substring((strCity.IndexOf('[') + 1), (strCity.IndexOf(']') - 1)) : "";

        ViewState["PayCode"] = ddpPayCode.SelectedValue.ToString().Trim();
        ViewState["BookStatus"] = ddpBookStatus.SelectedValue.ToString().Trim();
        ViewState["PayStatus"] = ddpPayStatus.SelectedValue.ToString().Trim();
        ViewState["PlatForm"] = ddpPlatForm.SelectedValue.ToString().Trim();
        ViewState["Ticket"] = ddpTicket.SelectedValue.ToString().Trim();

        ViewState["StartDTime"] = dpCreateStart.Value;
        ViewState["EndDTime"] = dpCreateEnd.Value;
        ViewState["Mobile"] = txtMobile.Value.Trim();
        ViewState["InStart"] = dpInStart.Value;
        ViewState["InEnd"] = dpInEnd.Value;
        ViewState["Aprove"] = ddpAprove.SelectedValue;

        BindReviewLmSystemLogListGrid();
        //BindReviewLmSystemLogListGrid(1, gridViewCSReviewLmSystemLogList.PageSize);
        //UpdatePanel2.Update();
    }

    protected void gridViewCSReviewLmSystemLogList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSReviewLmSystemLogList.PageIndex = e.NewPageIndex;
        BindReviewLmSystemLogListGrid();
    }

     //<summary>
     //GridView排序事件
     //</summary>
    protected void gridViewCSReviewLmSystemLogList_Sorting(object sender, GridViewSortEventArgs e)
    {
        // 从事件参数获取排序数据列
        string sortExpression = e.SortExpression.ToString();

        // 假定为排序方向为“顺序”
        string sortDirection = "ASC";

        // “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
        if (sortExpression == gridViewCSReviewLmSystemLogList.Attributes["SortExpression"])
        {
            //获得下一次的排序状态
            sortDirection = (gridViewCSReviewLmSystemLogList.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
        }

        // 重新设定GridView排序数据列及排序方向
        gridViewCSReviewLmSystemLogList.Attributes["SortExpression"] = sortExpression;
        gridViewCSReviewLmSystemLogList.Attributes["SortDirection"] = sortDirection;

        BindReviewLmSystemLogListGrid();
    }

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString(); ;
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString(); ;
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        _lmSystemLogEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
        _lmSystemLogEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
        _lmSystemLogEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
        _lmSystemLogEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
        _lmSystemLogEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
        _lmSystemLogEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
        _lmSystemLogEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
        _lmSystemLogEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();
        _lmSystemLogEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
        DataSet dsResult = LmSystemLogBP.ReviewSelectByNew(_lmSystemLogEntity).QueryResult;
        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count ==0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }
        CommonFunction.ExportExcelForLM(dsResult);
    }
}