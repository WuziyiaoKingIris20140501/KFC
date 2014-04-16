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

public partial class ReviewOverTimeOrder : BasePage
{
    IssueInfoEntity _issueinfoEntity = new IssueInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtPrice = GetPriceCodeData();
            ddpPriceCode.DataSource = dtPrice;
            ddpPriceCode.DataTextField = "PRICECD_TEXT";
            ddpPriceCode.DataValueField = "PRICECD_STATUS";
            ddpPriceCode.DataBind();
            ddpPriceCode.SelectedIndex = 0;

            ViewState["HotelID"] = "";
            ViewState["PriceCode"] = "";
            ViewState["StartDTime"] = DateTime.Now.AddDays(-1).ToShortDateString() + " 04:00:00";
            ViewState["EndDTime"] = DateTime.Now.ToShortDateString() + " 04:00:00";
      
            // 正确的属性设置方法
            gridViewCSReviewList.Attributes.Add("SortExpression", "CREATETIME");
            gridViewCSReviewList.Attributes.Add("SortDirection", "DESC");

            BindReviewHotelPlanListGrid();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
    }

    private DataTable GetPriceCodeData()
    {
        DataTable dt = new DataTable();
        DataColumn PStatus = new DataColumn("PRICECD_STATUS");
        DataColumn PText = new DataColumn("PRICECD_TEXT");

        dt.Columns.Add(PStatus);
        dt.Columns.Add(PText);

        DataRow dr0 = dt.NewRow();
        dr0["PRICECD_STATUS"] = "";
        dr0["PRICECD_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        DataRow dr1 = dt.NewRow();
        dr1["PRICECD_STATUS"] = "LMBAR2";
        dr1["PRICECD_TEXT"] = "LMBAR2";
        dt.Rows.Add(dr1);

        DataRow dr2 = dt.NewRow();
        dr2["PRICECD_STATUS"] = "BAR";
        dr2["PRICECD_TEXT"] = "BAR";
        dt.Rows.Add(dr2);

        DataRow dr3 = dt.NewRow();
        dr3["PRICECD_STATUS"] = "BARB";
        dr3["PRICECD_TEXT"] = "BARB";
        dt.Rows.Add(dr3);

        return dt;
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewHotelPlanListGrid();
    }

    private void BindReviewHotelPlanListGrid()
    {
        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity issueinfoEntity = new IssueInfoDBEntity();

        issueinfoEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        issueinfoEntity.PriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PriceCode"].ToString())) ? null : ViewState["PriceCode"].ToString();
        issueinfoEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        issueinfoEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        _issueinfoEntity.IssueInfoDBEntity.Add(issueinfoEntity);

        _issueinfoEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _issueinfoEntity.PageSize = gridViewCSReviewList.PageSize;
        _issueinfoEntity.SortField = gridViewCSReviewList.Attributes["SortExpression"].ToString();
        _issueinfoEntity.SortType = gridViewCSReviewList.Attributes["SortDirection"].ToString();

        _issueinfoEntity = IssueInfoBP.ReviewOverTimeOrderList(_issueinfoEntity);
        DataSet dsResult = _issueinfoEntity.QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "fog_order_num" };//主键
        gridViewCSReviewList.DataBind();

        AspNetPager1.PageSize = gridViewCSReviewList.PageSize;
        AspNetPager1.RecordCount = _issueinfoEntity.TotalCount;

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void gridViewCSReviewList_RowDataBound(object sender, GridViewRowEventArgs e)
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
        string strHotel = hidHotel.Value.ToString().Trim();
        ViewState["HotelID"] = (strHotel.IndexOf(']') > 0) ? strHotel.Substring(0, strHotel.IndexOf(']')).Trim('[').Trim(']') : strHotel;
        ViewState["PriceCode"] = ddpPriceCode.SelectedValue.Trim();
        ViewState["StartDTime"] = dpCreateStart.Value;
        ViewState["EndDTime"] = dpCreateEnd.Value;

        AspNetPager1.CurrentPageIndex = 1;
        BindReviewHotelPlanListGrid();
    }

     //<summary>
     //GridView排序事件
     //</summary>
    protected void gridViewCSReviewList_Sorting(object sender, GridViewSortEventArgs e)
    {
        // 从事件参数获取排序数据列
        string sortExpression = e.SortExpression.ToString();

        // 假定为排序方向为“顺序”
        string sortDirection = "ASC";

        // “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
        if (sortExpression == gridViewCSReviewList.Attributes["SortExpression"])
        {
            //获得下一次的排序状态
            sortDirection = (gridViewCSReviewList.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
        }

        // 重新设定GridView排序数据列及排序方向
        gridViewCSReviewList.Attributes["SortExpression"] = sortExpression;
        gridViewCSReviewList.Attributes["SortDirection"] = sortDirection;
        BindReviewHotelPlanListGrid();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity issueinfoEntity = new IssueInfoDBEntity();

        issueinfoEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        issueinfoEntity.PriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PriceCode"].ToString())) ? null : ViewState["PriceCode"].ToString();
        issueinfoEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        issueinfoEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        _issueinfoEntity.IssueInfoDBEntity.Add(issueinfoEntity);
        _issueinfoEntity.SortField = gridViewCSReviewList.Attributes["SortExpression"].ToString();
        _issueinfoEntity.SortType = gridViewCSReviewList.Attributes["SortDirection"].ToString();

        _issueinfoEntity = IssueInfoBP.ExportOverTimeOrderList(_issueinfoEntity);
        DataSet dsResult = _issueinfoEntity.QueryResult;

        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        dsResult.Tables[0].Columns["fog_order_num"].ColumnName = "订单号";
        dsResult.Tables[0].Columns["login_mobile"].ColumnName = "登陆手机号";
        dsResult.Tables[0].Columns["contact_name"].ColumnName = "联系人";
        dsResult.Tables[0].Columns["hotel_id"].ColumnName = "酒店ID";
        dsResult.Tables[0].Columns["hotel_name"].ColumnName = "酒店名称";
        dsResult.Tables[0].Columns["linktel"].ColumnName = "酒店联系电话";
        dsResult.Tables[0].Columns["price_code"].ColumnName = "价格代码";
        dsResult.Tables[0].Columns["book_status_other_nm"].ColumnName = "订单状态";
        dsResult.Tables[0].Columns["in_date"].ColumnName = "入住时间";
        dsResult.Tables[0].Columns["create_time"].ColumnName = "创建时间";
        dsResult.Tables[0].Columns["update_time"].ColumnName = "确认时间";
        dsResult.Tables[0].Columns["timeD_diff"].ColumnName = "时差";
        dsResult.Tables[0].Columns["book_status_other"].ColumnName = "价格代码CODE";
        dsResult.Tables[0].Columns["cancel_reason"].ColumnName = "取消原因";

        CommonFunction.ExportExcelForLM(dsResult);
    }
}