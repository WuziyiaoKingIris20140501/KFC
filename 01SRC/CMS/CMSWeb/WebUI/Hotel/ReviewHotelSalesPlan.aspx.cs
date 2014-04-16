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

public partial class ReviewHotelSalesPlan : BasePage
{
    APPContentEntity _appcontentEntity = new APPContentEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["HotelID"] = "";
            ViewState["StartDTime"] = DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00";
            ViewState["EndDTime"] = DateTime.Now.ToShortDateString() + " 23:59:59";
      
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);

            // 正确的属性设置方法
            gridViewCSReviewList.Attributes.Add("SortExpression", "CREATETIME");
            gridViewCSReviewList.Attributes.Add("SortDirection", "DESC");

            BindReviewHotelPlanListGrid();
            /*king*/
            //BindReviewLmSystemLogListGrid(1, gridViewCSReviewLmSystemLogList.PageSize);

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
        //messageContent.InnerHtml = "";
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewHotelPlanListGrid();
    }

    //private int CountLmSystemLog()
    //{
    //    _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
    //    APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

    //    appcontentDBEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
    //    appcontentDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
    //    appcontentDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
    //    _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

    //    DataSet dsResult = APPContentBP.ReviewSalesPlanCount(_appcontentEntity).QueryResult;

    //    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
    //    {
    //        return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
    //    }

    //    return 0;
    //}

    private void BindReviewHotelPlanListGrid()
    {
        //messageContent.InnerHtml = "";

        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        appcontentDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        appcontentDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        _appcontentEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _appcontentEntity.PageSize = gridViewCSReviewList.PageSize;
        _appcontentEntity.SortField = gridViewCSReviewList.Attributes["SortExpression"].ToString();
        _appcontentEntity.SortType = gridViewCSReviewList.Attributes["SortDirection"].ToString();

        _appcontentEntity = APPContentBP.ReviewSalesPlan(_appcontentEntity);
        DataSet dsResult = _appcontentEntity.QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "PLANID" };//主键
        gridViewCSReviewList.DataBind();

        AspNetPager1.PageSize = gridViewCSReviewList.PageSize;
        AspNetPager1.RecordCount = _appcontentEntity.TotalCount;

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
}