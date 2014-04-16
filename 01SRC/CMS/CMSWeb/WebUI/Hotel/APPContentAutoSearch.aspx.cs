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

public partial class APPContentAutoSearch : BasePage
{
    APPContentEntity _appcontentEntity = new APPContentEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlOnlinebind();
            BindCityDDL();
            //BindCityListGrid();
            lbToDay.Text= "审查结果";
        }
        else
        {
            messageContent.InnerHtml = "";
        }
    }

    public void ddlOnlinebind()
    {
        //DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
        //if (dsResult.Tables.Count > 0)
        //{
        //    dsResult.Tables[0].Columns["Key"].ColumnName = "ONLINETYPE";
        //    dsResult.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";

        //    ddpTypeList.DataTextField = "ONLINEDIS";
        //    ddpTypeList.DataValueField = "ONLINETYPE";
        //    ddpTypeList.DataSource = dsResult;
        //    ddpTypeList.DataBind();
        //}
       
        //int itime = int.Parse(DateTime.Now.Hour.ToString());
        //if ((18 <= itime && itime <= 23) || (0 <= itime && itime <= 4))
        //{
        //    ddpTypeList.SelectedValue = "1";
        //    ViewState["TYPE"] = "1";
        //}
        //else
        //{
        //    ddpTypeList.SelectedValue = "0";
        //    ViewState["TYPE"] = "0";
        //}
    }

    private void BindCityDDL()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsVerResult = GetServiceVer();
        ddpServiceVer.DataTextField = "verNM";
        ddpServiceVer.DataValueField = "verid";
        ddpServiceVer.DataSource = dsVerResult;
        ddpServiceVer.DataBind();
        ddpServiceVer.SelectedValue = "2";

        BandCityDdpList();
        //_appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        //_appcontentEntity.APPContentDBEntity.Add(new APPContentDBEntity());
        //_appcontentEntity.APPContentDBEntity[0].SerVer = "2";

        //DataSet dsResult = APPContentBP.CommonSelect(_appcontentEntity).QueryResult;
        //DataRow drTempcity = dsResult.Tables[0].NewRow();
        //drTempcity["cityid"] = DBNull.Value;
        //drTempcity["cityNM"] = "所有城市";
        //dsResult.Tables[0].Rows.InsertAt(drTempcity, 0);

        //ddpCityList.DataTextField = "cityNM";
        //ddpCityList.DataValueField = "cityid";
        //ddpCityList.DataSource = dsResult;
        //ddpCityList.DataBind();

        //DataSet dsPlatResult = APPContentBP.CommonPlatSelect(_appcontentEntity).QueryResult;
        //ddpPlatform.DataTextField = "platformname";
        //ddpPlatform.DataValueField = "platformcode";
        //ddpPlatform.DataSource = dsPlatResult;
        //ddpPlatform.DataBind();

        //ddpCityList.SelectedValue = "Shanghai";
        //ddpPlatform.SelectedValue = "IOS";

        //ViewState["CITYID"] = ddpCityList.SelectedValue;
        //ViewState["PLATFORM"] = ddpPlatform.SelectedValue;

        DataSet dsAppIgnore = CommonBP.GetConfigList(GetLocalResourceObject("AppIgnore").ToString());
        DataTable dtAppIgnore = new DataTable();
        dtAppIgnore.Columns.Add("APPIGKEY");
        dtAppIgnore.Columns.Add("APPIGVAL");
        if (dsAppIgnore.Tables.Count > 0)
        {
            dsAppIgnore.Tables[0].Columns["Key"].ColumnName = "APPIGKEY";
            dsAppIgnore.Tables[0].Columns["Value"].ColumnName = "APPIGVAL";

            ddpAppIgnore.DataTextField = "APPIGVAL";
            ddpAppIgnore.DataValueField = "APPIGKEY";
            ddpAppIgnore.DataSource = dsAppIgnore;
            ddpAppIgnore.DataBind();
        }
        
    }

    private DataSet GetServiceVer()
    {
        DataSet dsResult = new DataSet();
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("verid");
        dtResult.Columns.Add("verNM");

        //for (int i = 1; i < 3; i++)
        //{
        //    DataRow drRow = dtResult.NewRow();
        //    drRow["verid"] = i.ToString();
        //    drRow["verNM"] = i.ToString() + ".0";
        //    dtResult.Rows.Add(drRow);
        //}
        DataRow drRow = dtResult.NewRow();
        drRow["verid"] = "2";
        drRow["verNM"] = "2.0";
        dtResult.Rows.Add(drRow);
        dsResult.Tables.Add(dtResult);
        return dsResult;
    }

    private void BindCityListGrid()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CITYID"].ToString())) ? null : ViewState["CITYID"].ToString();
        appcontentDBEntity.CityNM = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CITYNM"].ToString())) ? null : ViewState["CITYNM"].ToString();
        appcontentDBEntity.SerVer = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SERVER"].ToString())) ? null : ViewState["SERVER"].ToString();

        //appcontentDBEntity.TypeID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TYPE"].ToString())) ? null : ViewState["TYPE"].ToString();
        //appcontentDBEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PLATFORM"].ToString())) ? null : ViewState["PLATFORM"].ToString();

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        DataSet dsResult = APPContentBP.AutoSelect(_appcontentEntity).QueryResult;

        gridViewCSAPPContenList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSAPPContenList.DataKeyNames = new string[] { "TYPEID" };//主键
        gridViewCSAPPContenList.DataBind();
        //string strContent = String.Format(GetLocalResourceObject("SelectCount").ToString(), dsResult.Tables[0].Rows.Count.ToString());
        //messageContent.InnerHtml = strContent;
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

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lbHotelNm = (Label)e.Row.FindControl("lbGRHotelNm");
        //    if (lbHotelNm != null && String.IsNullOrEmpty(lbHotelNm.Text))
        //    {
        //        e.Row.Cells[1].Attributes.Add("bgcolor", "#FF6666");
        //    }
        //}
    }

    protected void btnCLose_Click(object sender, EventArgs e)
    {
        WebAutoComplete.AutoResult = "";
        ddpAppIgnore.SelectedIndex = 0;

        BindCityListGrid();
        messageContent.InnerHtml = GetLocalResourceObject("SearchSuccess").ToString();
        dvSearch.Style.Add("display", "");
        dvSearchUn.Style.Add("display", "none");
        btnPopDiv.Enabled = true;
        DateTime dtNow = DateTime.Now;
        lbToDay.Text = "审查结果（" + dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + "）";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        if (String.IsNullOrEmpty(WebAutoComplete.AutoResult))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("HotelIDError").ToString();
            PopReseachData();
            return;
        }

        string strHotelNM = WebAutoComplete.AutoResult.ToString();
        string strHotelID = strHotelNM.Substring((strHotelNM.IndexOf('[') + 1), (strHotelNM.IndexOf(']') - 1));
        string strTypeID = ddpAppIgnore.SelectedValue;
        string strTypeNM = ddpAppIgnore.SelectedItem.ToString();

        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.TypeID = strTypeID;

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.InsertHotelIgnoreGrid(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "APP自动审核-配置免检项目-添加";
        commonDBEntity.Event_ID = strHotelID;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, strHotelNM, strTypeID, strTypeNM);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertError").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("InsertError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        PopReseachData();
    }

    protected void btnPopSearch_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        PopReseachData();
    }

    private void PopReseachData()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.PopGridSelect(_appcontentEntity).QueryResult;

        myGridView.DataSource = dsResult.Tables[0].DefaultView;
        myGridView.DataKeyNames = new string[] { "TYPEID" };//主键
        myGridView.DataBind();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist('" + WebAutoComplete.AutoResult + "');", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["CITYID"] = ddpCityList.SelectedValue;
        ViewState["CITYNM"] = ddpCityList.SelectedItem.ToString();
        ViewState["SERVER"] = ddpServiceVer.SelectedValue;
        //ViewState["TYPE"] = ddpTypeList.SelectedValue;
        //ViewState["PLATFORM"] = ddpPlatform.SelectedValue;

        messageContent.InnerHtml = "";
        //btnSearch.Text = "正在审查请稍后";
        //btnSearch.Enabled = false;

        BindCityListGrid();

        messageContent.InnerHtml = GetLocalResourceObject("SearchSuccess").ToString();
        //btnSearch.Text = "开始审查";
        //btnDiv.Disabled = false;

        dvSearch.Style.Add("display", "");
        dvSearchUn.Style.Add("display", "none");
        btnPopDiv.Enabled = true;
        DateTime dtNow = DateTime.Now;
        lbToDay.Text = "审查结果（" + dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + "）";
    }

    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.myGridView.PageIndex = e.NewPageIndex;
        PopReseachData();
    } 

    //protected void gridViewCSAPPContenList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    this.gridViewCSAPPContenList.PageIndex = e.NewPageIndex;
    //    BindCityListGrid();
    //}

    protected void myGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strHotelID = myGridView.Rows[e.RowIndex].Cells[0].Text.ToString();
        string strTypeID = myGridView.DataKeys[e.RowIndex].Value.ToString();

        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.TypeID = strTypeID;

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.DeleteHotelIgnoreGrid(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "APP自动审核-配置免检项目-删除";
        commonDBEntity.Event_ID = strHotelID;
        string conTent = GetLocalResourceObject("EventDeleteMessage").ToString();
        conTent = string.Format(conTent, strHotelID, strTypeID);
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

    protected void gridViewCSAPPContenList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strHotelNM = gridViewCSAPPContenList.Rows[e.RowIndex].Cells[0].Text.ToString();
        string strHotelID = gridViewCSAPPContenList.Rows[e.RowIndex].Cells[1].Text.ToString();
        string strTypeID = gridViewCSAPPContenList.DataKeys[e.RowIndex].Value.ToString();

        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.TypeID = strTypeID;

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.InsertHotelIgnoreGrid(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "APP自动审核-配置免检项目-添加忽略";
        commonDBEntity.Event_ID = strHotelID;
        string conTent = GetLocalResourceObject("EventInsertMessage2").ToString();
        conTent = string.Format(conTent, strHotelNM, strTypeID);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            BindCityListGrid();
            DateTime dtNow = DateTime.Now;
            lbToDay.Text = "审查结果（" + dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + "）";
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertError").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
    protected void ddpServiceVer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BandCityDdpList();
    }

    private void BandCityDdpList()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        _appcontentEntity.APPContentDBEntity.Add(new APPContentDBEntity());
        _appcontentEntity.APPContentDBEntity[0].SerVer = ddpServiceVer.SelectedValue;
        _appcontentEntity.APPContentDBEntity[0].PlatForm = "IOS";
        DataSet dsResult = APPContentBP.CommonSelect(_appcontentEntity).QueryResult;
        DataRow drTempcity = dsResult.Tables[0].NewRow();
        drTempcity["cityid"] = DBNull.Value;
        drTempcity["cityNM"] = "所有城市";
        dsResult.Tables[0].Rows.InsertAt(drTempcity, 0);
        ddpCityList.DataSource = null;
        ddpCityList.DataTextField = "cityNM";
        ddpCityList.DataValueField = "cityid";
        ddpCityList.DataSource = dsResult;
        ddpCityList.DataBind();
    }
}