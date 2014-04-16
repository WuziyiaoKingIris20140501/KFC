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

public partial class ReviewIssueManager : BasePage
{
    IssueInfoEntity _issueinfoEntity = new IssueInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList();

            ViewState["Title"] = "";
            ViewState["Status"] = "";
            ViewState["IssueType"] = "";
            ViewState["Assignto"] = "";
            ViewState["RelatedType"] = "";
            ViewState["RelatedID"] = "";
            ViewState["TimeSpans"] = "";

            ViewState["StartDTime"] = DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00";
            ViewState["EndDTime"] = DateTime.Now.ToShortDateString() + " 23:59:59";
      
            // 正确的属性设置方法
            gridViewCSReviewList.Attributes.Add("SortExpression", "CREATETIME");
            gridViewCSReviewList.Attributes.Add("SortDirection", "DESC");

            BindReviewIssueListGrid();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
    }

    private void BindDropDownList()
    {
        DataSet dsIssueType = CommonBP.GetConfigList(GetLocalResourceObject("IssueType").ToString());
        if (dsIssueType.Tables.Count > 0)
        {
            dsIssueType.Tables[0].Columns["Key"].ColumnName = "IssTypeKEY";
            dsIssueType.Tables[0].Columns["Value"].ColumnName = "IssTypeVALUE";

            DataRow drTemp = dsIssueType.Tables[0].NewRow();
            drTemp["IssTypeKEY"] = DBNull.Value;
            drTemp["IssTypeVALUE"] = "不限制";
            dsIssueType.Tables[0].Rows.InsertAt(drTemp, 0);

            ddpIssueType.DataTextField = "IssTypeVALUE";
            ddpIssueType.DataValueField = "IssTypeKEY";
            ddpIssueType.DataSource = dsIssueType;
            ddpIssueType.DataBind();
        }

        DataSet dsIssueStatus = CommonBP.GetConfigList(GetLocalResourceObject("IssueStatus").ToString());
        if (dsIssueStatus.Tables.Count > 0)
        {
            dsIssueStatus.Tables[0].Columns["Key"].ColumnName = "IssStatusKEY";
            dsIssueStatus.Tables[0].Columns["Value"].ColumnName = "IssStatusVALUE";

            DataRow drTemp = dsIssueStatus.Tables[0].NewRow();
            drTemp["IssStatusKEY"] = DBNull.Value;
            drTemp["IssStatusVALUE"] = "不限制";
            dsIssueStatus.Tables[0].Rows.InsertAt(drTemp, 0);

            ddpStatusList.DataTextField = "IssStatusVALUE";
            ddpStatusList.DataValueField = "IssStatusKEY";
            ddpStatusList.DataSource = dsIssueStatus;
            ddpStatusList.DataBind();
        }

        DataSet dsIssueRelated = CommonBP.GetConfigList(GetLocalResourceObject("IssueRelated").ToString());
        if (dsIssueRelated.Tables.Count > 0)
        {
            dsIssueRelated.Tables[0].Columns["Key"].ColumnName = "IssRelatedKEY";
            dsIssueRelated.Tables[0].Columns["Value"].ColumnName = "IssRelatedVALUE";

            DataRow drTemp = dsIssueRelated.Tables[0].NewRow();
            drTemp["IssRelatedKEY"] = DBNull.Value;
            drTemp["IssRelatedVALUE"] = "不限制";
            dsIssueRelated.Tables[0].Rows.InsertAt(drTemp, 0);

            ddpRelated.DataTextField = "IssRelatedVALUE";
            ddpRelated.DataValueField = "IssRelatedKEY";
            ddpRelated.DataSource = dsIssueRelated;
            ddpRelated.DataBind();
        }

        SetSalesDataControl();

        SetACTImeDataControl();
    }

    private void SetACTImeDataControl()
    {
        DataTable dtACTID = GetACTIDData();
        ddpActionTime.DataSource = dtACTID;
        ddpActionTime.DataTextField = "ACTI_TEXT";
        ddpActionTime.DataValueField = "ACTI_STATUS";
        ddpActionTime.DataBind();
        ddpActionTime.SelectedIndex = 0;
    }

    private DataTable GetACTIDData()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("ACTI_STATUS");
        DataColumn BookStatusText = new DataColumn("ACTI_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["ACTI_STATUS"] = "";
        dr0["ACTI_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 1; i < 6; i++)
        {
            DataRow dr = dt.NewRow();
           
            switch (i.ToString())
            {
                case "1":
                    dr["ACTI_STATUS"] = "86400000";
                    dr["ACTI_TEXT"] = "1天";
                    break;
                case "2":
                    dr["ACTI_STATUS"] = "172800000";
                    dr["ACTI_TEXT"] = "2天";
                    break;
                case "3":
                    dr["ACTI_STATUS"] = "259200000";
                    dr["ACTI_TEXT"] = "3天";
                    break;
                case "4":
                    dr["ACTI_STATUS"] = "345600000";
                    dr["ACTI_TEXT"] = "4天";
                    break;
                case "5":
                    dr["ACTI_STATUS"] = "432000000";
                    dr["ACTI_TEXT"] = "5天";
                    break;
                default:
                    dr["ACTI_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void SetSalesDataControl()
    {
        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity issueinfoDBEntity = new IssueInfoDBEntity();
        _issueinfoEntity.IssueInfoDBEntity.Add(issueinfoDBEntity);

        DataSet dsAssUser = IssueInfoBP.GetCommonUserList(_issueinfoEntity).QueryResult;

        DataRow drTemp = dsAssUser.Tables[0].NewRow();
        drTemp["USERID"] = DBNull.Value;
        drTemp["USERNAME"] = "不限制";
        dsAssUser.Tables[0].Rows.InsertAt(drTemp, 0);

        ddpAstoList.DataTextField = "USERNAME";
        ddpAstoList.DataValueField = "USERID";
        ddpAstoList.DataSource = dsAssUser;
        ddpAstoList.DataBind();
        ddpAstoList.SelectedIndex = 0;
    }


    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewIssueListGrid();
    }

    private void BindReviewIssueListGrid()
    {
        _issueinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _issueinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _issueinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _issueinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _issueinfoEntity.IssueInfoDBEntity = new List<IssueInfoDBEntity>();
        IssueInfoDBEntity appcontentDBEntity = new IssueInfoDBEntity();

        appcontentDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        appcontentDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

        appcontentDBEntity.Title = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Title"].ToString())) ? null : ViewState["Title"].ToString();
        appcontentDBEntity.Status = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Status"].ToString())) ? null : ViewState["Status"].ToString();
        appcontentDBEntity.IssueType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["IssueType"].ToString())) ? null : ViewState["IssueType"].ToString();
        appcontentDBEntity.Assignto = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Assignto"].ToString())) ? null : ViewState["Assignto"].ToString();
        appcontentDBEntity.RelatedType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RelatedType"].ToString())) ? null : ViewState["RelatedType"].ToString();
        appcontentDBEntity.RelatedID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RelatedID"].ToString())) ? null : ViewState["RelatedID"].ToString();
        appcontentDBEntity.TimeSpans = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TimeSpans"].ToString())) ? null : ViewState["TimeSpans"].ToString();

        _issueinfoEntity.IssueInfoDBEntity.Add(appcontentDBEntity);
        _issueinfoEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _issueinfoEntity.PageSize = gridViewCSReviewList.PageSize;
        _issueinfoEntity.SortField = gridViewCSReviewList.Attributes["SortExpression"].ToString();
        _issueinfoEntity.SortType = gridViewCSReviewList.Attributes["SortDirection"].ToString();

        _issueinfoEntity = IssueInfoBP.BindIssueList(_issueinfoEntity);
        DataSet dsResult = _issueinfoEntity.QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "IssueID" };//主键
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
        ViewState["Title"] = txtTitle.Text.Trim();
        ViewState["Status"] = ddpStatusList.SelectedValue.Trim();
        ViewState["IssueType"] = ddpIssueType.SelectedValue.Trim();
        ViewState["Assignto"] = ddpAstoList.SelectedValue.Trim();
        ViewState["RelatedType"] = ddpRelated.SelectedValue.Trim();
        ViewState["RelatedID"] = txtRelatedID.Text.Trim();
        ViewState["TimeSpans"] = ddpActionTime.SelectedValue.Trim();
        ViewState["StartDTime"] = dpCreateStart.Value;
        ViewState["EndDTime"] = dpCreateEnd.Value;

        AspNetPager1.CurrentPageIndex = 1;
        BindReviewIssueListGrid();
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
        BindReviewIssueListGrid();
    }
}