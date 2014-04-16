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
using System.Configuration;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class CoreHotelDailyCheck : BasePage
{
    APPContentEntity _appcontentEntity = new APPContentEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbDTime.Text = DateTime.Now.ToShortDateString();
            ViewState["DTime"] = lbDTime.Text;
            lbDTime2.Text = DateTime.Now.ToShortDateString();
            ViewState["DTime2"] = lbDTime2.Text;
            ViewState["HGROUPID"] = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["HGroupID"])) ? "1" : ConfigurationManager.AppSettings["HGroupID"].ToString().Trim();
            hidHotelGroupID.Value = ViewState["HGROUPID"].ToString();
            BindHotelGroupDetail();
            BindHotelGroupDetail2();
        }
        else
        {
            messageContent.InnerHtml = "";
        }
    }

    private void BindHotelGroupDetail()
    {
        hidSelectedID.Value = "0";
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.HGroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HGROUPID"].ToString())) ? null : ViewState["HGROUPID"].ToString();
        appcontentDBEntity.GType = "0";
        appcontentDBEntity.Cuser = "";
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.GetCoreHotelGroupDetail(_appcontentEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml =  GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        //hidHotelGroupID
        lbHotelGroup.Text = dsResult.Tables[0].Rows[0]["HGroupNM"].ToString();
        lbHotelCount.Text = dsResult.Tables[0].Rows[0]["DCOUNT"].ToString();
        lbHotelCrePer.Text = dsResult.Tables[0].Rows[0]["CREATEINFO"].ToString();
        BindHotelListGrid();
    }

    private void BindHotelGroupDetail2()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.HGroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HGROUPID"].ToString())) ? null : ViewState["HGROUPID"].ToString();
        appcontentDBEntity.GType = "1";
        appcontentDBEntity.Cuser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.GetCoreHotelGroupDetail(_appcontentEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent2.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        //hidHotelGroupID
        lbHotelGroup2.Text = dsResult.Tables[0].Rows[0]["HGroupNM"].ToString();
        lbHotelCount2.Text = dsResult.Tables[0].Rows[0]["DCOUNT"].ToString();
        lbHotelCrePer2.Text = dsResult.Tables[0].Rows[0]["CREATEINFO"].ToString();
        BindHotelListGrid2();
    }

    private void RefreshHotelCount(string GType, string Cuser)
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.HGroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HGROUPID"].ToString())) ? null : ViewState["HGROUPID"].ToString();
        appcontentDBEntity.GType = GType;
        appcontentDBEntity.Cuser = Cuser;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.GetCoreHotelGroupDetail(_appcontentEntity).QueryResult;
        string strVal = "";
        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            strVal = "0";
        }
        else
        {
            strVal = dsResult.Tables[0].Rows[0]["DCOUNT"].ToString();
        }

        if ("0".Equals(GType))
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setScript", "SetCountVal('" + strVal + "')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "setScript", "SetCountVal('" + strVal + "')", true);
        }
        
    }

    private void BindHotelListGrid()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HGroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HGROUPID"].ToString())) ? null : ViewState["HGROUPID"].ToString();
        appcontentDBEntity.DTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DTime"].ToString())) ? null : ViewState["DTime"].ToString();
        appcontentDBEntity.GType = "0";
        appcontentDBEntity.Cuser = "";
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.BindHotelListGrid(_appcontentEntity).QueryResult;
        dsResult.Tables[0].DefaultView.Sort = " ONLINES ASC";
        gridViewCSAPPContenList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSAPPContenList.DataKeyNames = new string[] { "HOTELID" };//主键
        gridViewCSAPPContenList.DataBind();

        for (int i = 0; i < gridViewCSAPPContenList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridViewCSAPPContenList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                for (int j = 0; j < gridViewCSAPPContenList.Rows[i].Cells.Count - 1; j++)
                {
                    switch (j)
                    {
                        case 2:
                            if ("否".Equals(gridViewCSAPPContenList.Rows[i].Cells[j].Text))
                            {
                                gridViewCSAPPContenList.Rows[i].Cells[j].Attributes.Add("bgcolor", "#FF6666");
                            }
                            break;
                    }
                }
            }
        }
    }

    private void BindHotelListGrid2()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HGroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HGROUPID"].ToString())) ? null : ViewState["HGROUPID"].ToString();
        appcontentDBEntity.DTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DTime2"].ToString())) ? null : ViewState["DTime2"].ToString();
        appcontentDBEntity.GType = "1";
        appcontentDBEntity.Cuser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.BindHotelListGrid(_appcontentEntity).QueryResult;
        dsResult.Tables[0].DefaultView.Sort = " ONLINES ASC";
        gridViewCSAPPContenList2.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSAPPContenList2.DataKeyNames = new string[] { "HOTELID" };//主键
        gridViewCSAPPContenList2.DataBind();

        for (int i = 0; i < gridViewCSAPPContenList2.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridViewCSAPPContenList2.Rows[i].RowType == DataControlRowType.DataRow)
            {
                for (int j = 0; j < gridViewCSAPPContenList2.Rows[i].Cells.Count - 1; j++)
                {
                    switch (j)
                    {
                        case 2:
                            if ("否".Equals(gridViewCSAPPContenList2.Rows[i].Cells[j].Text))
                            {
                                gridViewCSAPPContenList2.Rows[i].Cells[j].Attributes.Add("bgcolor", "#FF6666");
                            }
                            break;
                    }
                }
            }
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
            }
        }
    }

    protected void gridViewCSAPPContenList2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSAPPContenList2.Rows.Count; i++)
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        if (String.IsNullOrEmpty(WebAutoComplete.AutoResult) || String.IsNullOrEmpty(hidHotelID.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("HotelIDError").ToString();
            return;
        }

        string strHotelNM = WebAutoComplete.AutoResult.ToString();
        string strHotelID = strHotelNM.Substring((strHotelNM.IndexOf('[') + 1), (strHotelNM.IndexOf(']') - 1));

        appcontentDBEntity.HGroupID = hidHotelGroupID.Value;
        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.GType = "0";
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.InsertHotelGroupList(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "核心酒店每日检查-酒店添加";
        commonDBEntity.Event_ID = strHotelID;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, hidHotelGroupID.Value, strHotelID, strHotelNM, "0");
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            BindHotelListGrid();
            RefreshHotelCount("0", "");
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertError").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertError").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertErrorHotel").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertErrorHotel").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        if (String.IsNullOrEmpty(wctHotel.AutoResult) || String.IsNullOrEmpty(hidHotelID2.Value.Trim()))
        {
            messageContent2.InnerHtml = GetLocalResourceObject("HotelIDError").ToString();
            return;
        }

        string strHotelNM = wctHotel.AutoResult.ToString();
        string strHotelID = strHotelNM.Substring((strHotelNM.IndexOf('[') + 1), (strHotelNM.IndexOf(']') - 1));

        appcontentDBEntity.HGroupID = hidHotelGroupID.Value;
        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.GType = "1";
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.InsertHotelGroupList(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "核心酒店每日检查-酒店添加";
        commonDBEntity.Event_ID = strHotelID;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, hidHotelGroupID.Value, strHotelID, strHotelNM, "1");
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent2.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            BindHotelListGrid2();
            RefreshHotelCount("1", UserSession.Current.UserDspName);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertError").ToString();
            messageContent2.InnerHtml = GetLocalResourceObject("InsertError").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertErrorHotel").ToString();
            messageContent2.InnerHtml = GetLocalResourceObject("InsertErrorHotel").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void gridViewCSAPPContenList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strHotelID = gridViewCSAPPContenList.Rows[e.RowIndex].Cells[0].Text.ToString();
        string strHotelNM = gridViewCSAPPContenList.Rows[e.RowIndex].Cells[1].Text.ToString();

        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.HGroupID = hidHotelGroupID.Value;
        appcontentDBEntity.GType = "0";
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.DeteleHotelGroupList(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "核心酒店每日检查-酒店删除";
        commonDBEntity.Event_ID = strHotelID;
        string conTent = GetLocalResourceObject("EventDeteleMessage").ToString();
        conTent = string.Format(conTent, hidHotelGroupID.Value, strHotelID, strHotelNM, "0");
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("DeteleSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("DeteleSuccess").ToString();
            BindHotelListGrid();
            RefreshHotelCount("0", "");
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("DeteleError").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("DeteleError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void gridViewCSAPPContenList2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strHotelID = gridViewCSAPPContenList2.Rows[e.RowIndex].Cells[0].Text.ToString();
        string strHotelNM = gridViewCSAPPContenList2.Rows[e.RowIndex].Cells[1].Text.ToString();

        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = strHotelID;
        appcontentDBEntity.HGroupID = hidHotelGroupID.Value;
        appcontentDBEntity.GType = "1";
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.DeteleHotelGroupList(_appcontentEntity);

        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "核心酒店每日检查-酒店删除";
        commonDBEntity.Event_ID = strHotelID;
        string conTent = GetLocalResourceObject("EventDeteleMessage").ToString();
        conTent = string.Format(conTent, hidHotelGroupID.Value, strHotelID, strHotelNM, "0");
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("DeteleSuccess").ToString();
            messageContent2.InnerHtml = GetLocalResourceObject("DeteleSuccess").ToString();
            BindHotelListGrid2();
            RefreshHotelCount("1", UserSession.Current.UserDspName);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("DeteleError").ToString();
            messageContent2.InnerHtml = GetLocalResourceObject("DeteleError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
}