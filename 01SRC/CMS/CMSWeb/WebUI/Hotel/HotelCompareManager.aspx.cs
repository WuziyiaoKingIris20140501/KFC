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

public partial class HotelCompareManager : BasePage
{
    APPContentEntity _appcontentEntity = new APPContentEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ddlOnlinebind();
            BindDdpList();
            //BindCityListGrid();
            lbToDay.Text= "审查结果";
            dvGridBtn.Style.Add("display", "none");
        }
        else
        {
            messageContent.InnerHtml = "";
        }
    }

    private void BindDdpList()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
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

        DataSet dsAppIgnore = CommonBP.GetConfigList(GetLocalResourceObject("HotelCheck").ToString());
        DataTable dtAppIgnore = new DataTable();
        dtAppIgnore.Columns.Add("HLCKKEY");
        dtAppIgnore.Columns.Add("HLCKVAL");
        if (dsAppIgnore.Tables.Count > 0)
        {
            dsAppIgnore.Tables[0].Columns["Key"].ColumnName = "HLCKKEY";
            dsAppIgnore.Tables[0].Columns["Value"].ColumnName = "HLCKVAL";

            ddpAppIgnore.DataTextField = "HLCKVAL";
            ddpAppIgnore.DataValueField = "HLCKKEY";
            ddpAppIgnore.DataSource = dsAppIgnore;
            ddpAppIgnore.DataBind();
        }
        
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
        appcontentDBEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HOTELID"].ToString())) ? null : ViewState["HOTELID"].ToString();
        //appcontentDBEntity.TypeID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TYPE"].ToString())) ? null : ViewState["TYPE"].ToString();
        //appcontentDBEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PLATFORM"].ToString())) ? null : ViewState["PLATFORM"].ToString();

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        DataSet dsResult = APPContentBP.GetHotelFogList(_appcontentEntity).QueryResult;

        gridViewCSAPPContenList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSAPPContenList.DataKeyNames = new string[] { "TYPEID" };//主键
        gridViewCSAPPContenList.DataBind();
        //string strContent = String.Format(GetLocalResourceObject("SelectCount").ToString(), dsResult.Tables[0].Rows.Count.ToString());
        //messageContent.InnerHtml = strContent;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            dvGridBtn.Style.Add("display", "");
        }
        else
        {
            dvGridBtn.Style.Add("display", "none");
        }
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

                e.Row.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
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

    protected void btnHubVal_Click(object sender, EventArgs e)
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        string strHotelID = "";
        string strHotelNM = "";
        string strTypeID = "";
        for (int i = 0; i < this.gridViewCSAPPContenList.Rows.Count; i++)
        {
            System.Web.UI.HtmlControls.HtmlInputCheckBox ck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].FindControl("chkItems");
            if (ck.Checked == true)
            {
                strHotelID = gridViewCSAPPContenList.Rows[i].Cells[1].Text.ToString();
                strHotelNM = gridViewCSAPPContenList.Rows[i].Cells[2].Text.ToString();
                strTypeID = gridViewCSAPPContenList.DataKeys[i].Value.ToString();
                APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
                appcontentDBEntity.HotelID = strHotelID;
                appcontentDBEntity.HotelNM = strHotelNM;
                appcontentDBEntity.TypeID = strTypeID;
                _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
            }
        }

        if (_appcontentEntity.APPContentDBEntity.Count > 0)
        {
            int iResult = APPContentBP.UpdateHotelCompareGridBatch(_appcontentEntity);
            string Event_Result = "";
            string Event_conTent = GetLocalResourceObject("EventInsertMessage3").ToString();
            _commonEntity.LogMessages = _appcontentEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            string conTent = "";

            if (iResult == 1)
            {
                Event_Result = GetLocalResourceObject("InsertHubs1Success").ToString();
                messageContent.InnerHtml = GetLocalResourceObject("InsertHubs1Success").ToString();
            }
            else
            {
                Event_Result = GetLocalResourceObject("InsertHubs1Error").ToString();
                messageContent.InnerHtml = GetLocalResourceObject("InsertHubs1Error").ToString();
            }

            foreach (APPContentDBEntity dbParm in _appcontentEntity.APPContentDBEntity)
            {
                CommonDBEntity commonDBEntity = new CommonDBEntity();
                commonDBEntity.Event_Type = "酒店数据同步检查-采用Hubs1值";
                commonDBEntity.Event_ID = dbParm.HotelNM;
                commonDBEntity.Event_Result = Event_Result;
                conTent = string.Format(Event_conTent, dbParm.HotelNM, dbParm.TypeID);
                commonDBEntity.Event_Content = conTent;
                _commonEntity.CommonDBEntity.Add(commonDBEntity);
            }

            CommonBP.InsertEventHistory(_commonEntity);
            if (iResult == 1)
            {
                BindCityListGrid();
                DateTime dtNow = DateTime.Now;
                lbToDay.Text = "审查结果（" + dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + "）";
            }
        }
    }

    protected void btnUnCheck_Click(object sender, EventArgs e)
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        string strHotelID = "";
        string strHotelNM = "";
        string strTypeID = "";
        for (int i = 0; i < this.gridViewCSAPPContenList.Rows.Count; i++)
        {
            System.Web.UI.HtmlControls.HtmlInputCheckBox ck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].FindControl("chkItems");
            if (ck.Checked == true)
            {
                strHotelID = gridViewCSAPPContenList.Rows[i].Cells[1].Text.ToString();
                strHotelNM = gridViewCSAPPContenList.Rows[i].Cells[2].Text.ToString();
                strTypeID = gridViewCSAPPContenList.DataKeys[i].Value.ToString();
                APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
                appcontentDBEntity.HotelID = strHotelID;
                appcontentDBEntity.HotelNM = strHotelNM;
                appcontentDBEntity.TypeID = strTypeID;
                _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
            }
        }

        if (_appcontentEntity.APPContentDBEntity.Count > 0)
        {
            int iResult = APPContentBP.InsertHotelCompareGridBatch(_appcontentEntity);
            string Event_Result = "";
            _commonEntity.LogMessages = _appcontentEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            string Event_conTent = GetLocalResourceObject("EventInsertMessage2").ToString();
            string conTent = "";

            if (iResult == 1)
            {
                Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
                messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            }
            else
            {
                Event_Result = GetLocalResourceObject("InsertError2").ToString();
                messageContent.InnerHtml = GetLocalResourceObject("InsertError2").ToString();
            }

            foreach (APPContentDBEntity dbParm in _appcontentEntity.APPContentDBEntity)
            {
                CommonDBEntity commonDBEntity = new CommonDBEntity();
                commonDBEntity.Event_Type = "酒店数据同步检查-标记免审查";
                commonDBEntity.Event_ID = dbParm.HotelNM;
                commonDBEntity.Event_Result = Event_Result;
                conTent = string.Format(Event_conTent, dbParm.HotelNM, dbParm.TypeID);
                commonDBEntity.Event_Content = conTent;
                _commonEntity.CommonDBEntity.Add(commonDBEntity);
            }

            CommonBP.InsertEventHistory(_commonEntity);

            if (iResult == 1)
            {
                BindCityListGrid();
                DateTime dtNow = DateTime.Now;
                lbToDay.Text = "审查结果（" + dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + "）";
            }
        }
    }

    protected void btnCLose_Click(object sender, EventArgs e)
    {
        WebAutoComplete.AutoResult = "";
        ddpAppIgnore.SelectedIndex = 0;

        BindCityListGrid();
        messageContent.InnerHtml = GetLocalResourceObject("SearchSuccess").ToString();
        lbCondition.Text = "审查城市：" + hidCity.Value.ToString().Trim() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审查酒店：" + hidHotel.Value.ToString().Trim();

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
        int iResult = APPContentBP.InsertHotelCompareGrid(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店数据同步检查-配置免检项目-添加";
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
        DataSet dsResult = APPContentBP.PopHotelGridSelect(_appcontentEntity).QueryResult;

        myGridView.DataSource = dsResult.Tables[0].DefaultView;
        myGridView.DataKeyNames = new string[] { "TYPEID" };//主键
        myGridView.DataBind();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist('" + WebAutoComplete.AutoResult + "');", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strHotel = hidHotel.Value.ToString().Trim(); //wctHotel.AutoResult.ToString().Trim();
        string strCity = hidCity.Value.ToString().Trim(); //wctCity.AutoResult.ToString().Trim();
        ViewState["HOTELID"] = (strHotel.IndexOf(']') > 0) ? strHotel.Substring(0, strHotel.IndexOf(']')).Trim('[').Trim(']') : strHotel;
        //(!String.IsNullOrEmpty(strHotel)) ? strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1)) : "";
        ViewState["CITYID"] = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity; 

        //ViewState["TYPE"] = ddpTypeList.SelectedValue;
        //ViewState["PLATFORM"] = ddpPlatform.SelectedValue;

        messageContent.InnerHtml = "";
        //btnSearch.Text = "正在审查请稍后";
        //btnSearch.Enabled = false;

        BindCityListGrid();

        messageContent.InnerHtml = GetLocalResourceObject("SearchSuccess").ToString();
        //btnSearch.Text = "开始审查";
        //btnDiv.Disabled = false;

        lbCondition.Text = "审查城市：" + hidCity.Value.ToString().Trim() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审查酒店：" + hidHotel.Value.ToString().Trim();

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
        int iResult = APPContentBP.DeleteHotelCompareGrid(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店数据同步检查-配置免检项目-删除";
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
        string strHotelID = gridViewCSAPPContenList.Rows[e.RowIndex].Cells[0].Text.ToString();
        string strHotelNM = gridViewCSAPPContenList.Rows[e.RowIndex].Cells[1].Text.ToString();
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
        int iResult = APPContentBP.InsertHotelCompareGrid(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店数据同步检查-配置免检项目-添加忽略";
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
}