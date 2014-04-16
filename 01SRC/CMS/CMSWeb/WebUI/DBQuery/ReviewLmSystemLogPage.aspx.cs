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

public partial class ReviewLmSystemLogPage : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrderID"] = "";
            ViewState["StartDTime"] = DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00";
            ViewState["EndDTime"] = DateTime.Now.ToShortDateString() + " 23:59:59";

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);

            // 正确的属性设置方法
            gridViewCSReviewLmSystemLogList.Attributes.Add("SortExpression", "FOGCREATER");
            gridViewCSReviewLmSystemLogList.Attributes.Add("SortDirection", "ASC");

            BindReviewLmSystemLogListGrid();
        }
        //messageContent.InnerHtml = "";
    }

    private void BindReviewLmSystemLogListGrid()
    {
        //messageContent.InnerHtml = "";

        //if (!checkNum(txtOrderID.Value.Trim()) || !checkNum(txtOrderID.Value.Trim()))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("ErrorNum").ToString();
        //    return;
        //}
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.EventID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString(); ;
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString(); ;

        DataSet dsResult = LmSystemLogBP.ReviewSelect(_lmSystemLogEntity).QueryResult;


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

        if (chkCreateUnTime.Checked)
        {
            ViewState["StartDTime"]  = "";
            ViewState["EndDTime"]  = "";
        }
        else
        {
            ViewState["StartDTime"]  = dpCreateStart.Value;
            ViewState["EndDTime"]  = dpCreateEnd.Value;
        }

        BindReviewLmSystemLogListGrid();
        //UpdatePanel2.Update();
    }

    protected void gridViewCSReviewLmSystemLogList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSReviewLmSystemLogList.PageIndex = e.NewPageIndex;
        BindReviewLmSystemLogListGrid();
    }

    /// <summary>
    /// GridView排序事件
    /// </summary>
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

        DataSet dsResult = LmSystemLogBP.ReviewSelect(_lmSystemLogEntity).QueryResult;


        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count ==0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        CommonFunction.ExportExcelForLM(dsResult);
    }
}