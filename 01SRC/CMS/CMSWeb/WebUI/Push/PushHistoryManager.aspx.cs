using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.IO;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.OleDb;

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
using HotelVp.Common.Utilities;

public partial class PushHistoryManager : BasePage
{
    PushEntity _pushEntity = new PushEntity();
    CommonEntity _commonEntity = new CommonEntity();
    string _strFilePath = string.Empty;
    string _strPushProtoType = string.Empty;
    int _iAllCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["PushTitle"] = "";
            ViewState["PushContent"] = "";
            ViewState["SendStart"] = "";
            ViewState["SendEnd"] = "";
            BindReviewLmListGrid();
            /*king*/
            //BindReviewLmSystemLogListGrid(1, gridViewCSReviewLmSystemLogList.PageSize);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
        //messageContent.InnerHtml = "";
    }

    private string GetDateTimeQuery(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return "";
        }

        try
        {
            DateTime dtTime = DateTime.Parse(param);
            return dtTime.AddDays(1).ToShortDateString() + " 04:00:00";
        }
        catch
        {
            return "";
        }
    }

    //protected void AspNetPager1_PageChanged(object src, EventArgs e)
    //{
    //    BindReviewLmSystemLogListGrid();
    //}

    //private int CountLmSystemLog()
    //{
    //    _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
    //    _pushEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
    //    _pushEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
    //    _pushEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
    //    _pushEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
    //    _pushEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
    //    _pushEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
    //    _pushEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
    //    _pushEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
    //    _pushEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
    //    _pushEntity.HotelComfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();
    //    _pushEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
    //    _pushEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
    //    _pushEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
    //    _pushEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();
    //    _pushEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
    //    _pushEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString();
    //    _pushEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString(); 

    //    _pushEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
    //    _pushEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
    //    _pushEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();

    //    _pushEntity.DashPopStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashPopStatus"].ToString())) ? null : ViewState["DashPopStatus"].ToString();
    //    _pushEntity.DashInStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInStart"].ToString())) ? null : ViewState["DashInStart"].ToString();
    //    _pushEntity.DashInEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInEnd"].ToString())) ? null : ViewState["DashInEnd"].ToString();
    //    _pushEntity.DashStartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashStartDTime"].ToString())) ? null : ViewState["DashStartDTime"].ToString();
    //    _pushEntity.DashEndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashEndDTime"].ToString())) ? null : ViewState["DashEndDTime"].ToString();
    
    //    DataSet dsResult = PushBP.ReviewLmOrderLogSelectCount(_pushEntity).QueryResult;

    //    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
    //    {
    //        return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
    //    }

    //    return 0;
    //}

    private void BindReviewLmListGrid()
    {
        //messageContent.InnerHtml = "";

        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        //_pushEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();


        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();

        pushEntity.Title = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PushTitle"].ToString())) ? null : ViewState["PushTitle"].ToString();
        pushEntity.Content = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PushContent"].ToString())) ? null : ViewState["PushContent"].ToString();
        pushEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SendStart"].ToString())) ? null : ViewState["SendStart"].ToString();
        pushEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SendEnd"].ToString())) ? null : ViewState["SendEnd"].ToString();
        _pushEntity.PushDBEntity.Add(pushEntity);
        //_pushEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _pushEntity.PageSize = gridViewCSReviewList.PageSize;
        //_pushEntity.SortField = gridViewCSReviewList.Attributes["SortExpression"].ToString();
        //_pushEntity.SortType = gridViewCSReviewList.Attributes["SortDirection"].ToString();
        DataSet dsResult = PushBP.PushHistoryListSelect(_pushEntity).QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "TID" };//主键
        gridViewCSReviewList.DataBind();

        //AspNetPager1.PageSize = gridViewCSReviewList.PageSize;
        //AspNetPager1.RecordCount = CountLmSystemLog();
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
        ViewState["PushTitle"] = txtPushTitle.Text.Trim();
        ViewState["PushContent"] = txtPushContent.Text.Trim();
        ViewState["SendStart"] = dpSendStart.Value.Trim();
        ViewState["SendEnd"] = dpSendEnd.Value.Trim();

        BindReviewLmListGrid();
    }

    protected void gridViewCSReviewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSReviewList.PageIndex = e.NewPageIndex;
        BindReviewLmListGrid();
    } 

     //<summary>
     //GridView排序事件
     //</summary>
    //protected void gridViewCSReviewLmSystemLogList_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    //// 从事件参数获取排序数据列
    //    //string sortExpression = e.SortExpression.ToString();

    //    //// 假定为排序方向为“顺序”
    //    //string sortDirection = "ASC";

    //    //// “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
    //    //if (sortExpression == gridViewCSReviewList.Attributes["SortExpression"])
    //    //{
    //    //    //获得下一次的排序状态
    //    //    sortDirection = (gridViewCSReviewList.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
    //    //}

    //    //// 重新设定GridView排序数据列及排序方向
    //    //gridViewCSReviewList.Attributes["SortExpression"] = sortExpression;
    //    //gridViewCSReviewList.Attributes["SortDirection"] = sortDirection;

    //    BindReviewLmListGrid();
    //    //BindReviewLmSystemLogListGrid(AspNetPager1.CurrentPageIndex , gridViewCSReviewLmSystemLogList.PageSize);
    //}
}