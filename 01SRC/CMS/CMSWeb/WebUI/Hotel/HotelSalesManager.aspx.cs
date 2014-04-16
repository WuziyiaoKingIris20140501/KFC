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

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class HotelSalesManager : BasePage
{
    APPContentEntity _appcontentEntity = new APPContentEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SalesUser"] = "";
            //ViewState["HotelNm"] = "";
            BindDataListGrid();
        }
        else
        {
            messageContent.InnerHtml = "";
        }
    }

    private void BindDataListGrid()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.UserCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SalesUser"].ToString())) ? null : ViewState["SalesUser"].ToString();
        //appcontentDBEntity.HotelNM = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelNm"].ToString())) ? null : ViewState["HotelNm"].ToString();

        _appcontentEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _appcontentEntity.PageSize = gridViewCSAPPContenList.PageSize;

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        _appcontentEntity = APPContentBP.SalesMangeListSelect(_appcontentEntity);

        DataSet dsResult = _appcontentEntity.QueryResult;
        gridViewCSAPPContenList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSAPPContenList.DataKeyNames = new string[] { "USERACCOUNT" };//主键
        gridViewCSAPPContenList.DataBind();

        AspNetPager1.PageSize = gridViewCSAPPContenList.PageSize;
        AspNetPager1.RecordCount = _appcontentEntity.TotalCount;
    }

    protected void gridViewCSAPPContenList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSAPPContenList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }

    protected void btnCLose_Click(object sender, EventArgs e)
    {
        WebAutoComplete.AutoResult = "";
        detailMessageContent.InnerHtml = "";
        BindDataListGrid();

        //messageContent.InnerHtml = GetLocalResourceObject("SearchSuccess").ToString();
        //dvSearch.Style.Add("display", "");
        //dvSearchUn.Style.Add("display", "none");
        //btnPopDiv.Enabled = true;
        //DateTime dtNow = DateTime.Now;
        //lbToDay.Text = "审查结果（" + dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + "）";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        myGridView.PageIndex = 0;
        SalesDetailManager();
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindDataListGrid();
    }

    private void SalesDetailManager()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.UserCode = hidUserAccount.Value.Trim();
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.SalesMangeDetialSelect(_appcontentEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            lbDspName.Text = dsResult.Tables[0].Rows[0]["USERNM"].ToString();
            lbAccount.Text = dsResult.Tables[0].Rows[0]["USERACCOUNT"].ToString();
            lbSaleManager.Text = dsResult.Tables[0].Rows[0]["SALESMANAGER"].ToString();
            lbTel.Text = dsResult.Tables[0].Rows[0]["TEL"].ToString();
            lbHotelSum.Text = dsResult.Tables[0].Rows[0]["HOTELSUM"].ToString();
            PopReseachData();
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UserIDError").ToString();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        if (String.IsNullOrEmpty(WebAutoComplete.AutoResult) || !WebAutoComplete.AutoResult.Trim().Equals(hidHotelID.Value.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("HotelIDError").ToString();
            PopReseachData();
            return;
        }

        if (String.IsNullOrEmpty(dpStart.Value) || String.IsNullOrEmpty(dpEnd.Value))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("DTimeError").ToString();
            PopReseachData();
            return;
        }

        string strHotelNM = WebAutoComplete.AutoResult.ToString();
        string strHotelID = strHotelNM.Substring((strHotelNM.IndexOf('[') + 1), (strHotelNM.IndexOf(']') - 1));
        //string strTypeID = ddpAppIgnore.SelectedValue;
        //string strTypeNM = ddpAppIgnore.SelectedItem.ToString();

        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.StartDTime = dpStart.Value;
        appcontentDBEntity.EndDTime = dpEnd.Value;
        appcontentDBEntity.UserCode = hidUserAccount.Value.Trim();

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        APPContentEntity appcontentRest  = APPContentBP.InsertSalesMangeGrid(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店销售管理-添加";
        commonDBEntity.Event_ID = strHotelID + "-" + hidUserAccount.Value.Trim();
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, strHotelNM, hidUserAccount.Value.Trim());
        commonDBEntity.Event_Content = conTent;

        if (appcontentRest.Result == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            WebAutoComplete.AutoResult = "";
            dpStart.Value = "";
            dpEnd.Value = "";
        }
        else if (appcontentRest.Result == 2)
        {
            commonDBEntity.Event_Result = string.Format(GetLocalResourceObject("InsertError").ToString(), appcontentRest.ErrorMSG);
            detailMessageContent.InnerHtml = string.Format(GetLocalResourceObject("InsertError").ToString(), appcontentRest.ErrorMSG);
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        PopReseachData();
        SalesDetailManager();
    }

    private void PopReseachData()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.UserCode = hidUserAccount.Value.Trim();
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        DataSet dsResult = APPContentBP.SalesPopGridSelect(_appcontentEntity).QueryResult;
        myGridView.DataSource = dsResult.Tables[0].DefaultView;
        myGridView.DataKeyNames = new string[] { "HOTELID" };//主键
        myGridView.DataBind();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist('" + WebAutoComplete.AutoResult + "');", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["SalesUser"] = txtSalesUser.Text.Trim();
        //ViewState["HotelNm"] = txtHotelNm.Text.Trim();
        messageContent.InnerHtml = "";
        //gridViewCSAPPContenList.PageIndex = 0;
        AspNetPager1.CurrentPageIndex = 1;
        BindDataListGrid();
    }

    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.myGridView.PageIndex = e.NewPageIndex;
        PopReseachData();
    }

    //protected void gridViewCSAPPContenList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    this.gridViewCSAPPContenList.PageIndex = e.NewPageIndex;
    //    BindDataListGrid();
    //}

    protected void myGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strHotelID = myGridView.Rows[e.RowIndex].Cells[0].Text.ToString();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.UserCode = hidUserAccount.Value.Trim();

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.DeleteSalesManagerGrid(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店销售管理-删除";
        commonDBEntity.Event_ID = strHotelID + "-" + hidUserAccount.Value.Trim();
        string conTent = GetLocalResourceObject("EventDeleteMessage").ToString();
        conTent = string.Format(conTent, strHotelID, hidUserAccount.Value.Trim());
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("DeleteSuccess").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("DeleteSuccess").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("DeleteError").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("DeleteError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        PopReseachData();
    }
}