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

public partial class PlatformSearchPage : BasePage
{
    PlatformEntity _platformEntity = new PlatformEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindChannelDDL();
            BindPlatformListGrid();

            chkUnTime.Checked = true;
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetchkRegistUnTime();", true);
        }

        messageContent.InnerHtml = "";
    }

    public DataSet ddlOnlinebind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("ONLINESTATUS");
        dtResult.Columns.Add("ONLINEDIS");
        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
            dsResult.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";
        }
        return dsResult;
    }


    //private void BindChannelDDL()
    //{
    //    _channelEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _channelEntity.LogMessages.Userid = "test";
    //    _channelEntity.LogMessages.Username = "test";

    //    DataSet dsResult = PlatformBP.CommonSelect(_channelEntity).QueryResult;
    //    ddpChannelList.DataTextField = "DISPLAYNM";
    //    ddpChannelList.DataValueField = "ID";
    //    ddpChannelList.DataSource = dsResult;
    //    ddpChannelList.DataBind();
    //}

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAddPlatform();
        BindPlatformListGrid();
    }

    //清除控件中的数据
    private void clearValue()
    {
    }

    //发放渠道
    private void BindPlatformListGrid()
    {
        _platformEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _platformEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _platformEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _platformEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _platformEntity.PlatformDBEntity = new List<PlatformDBEntity>();
        PlatformDBEntity platformDBEntity = new PlatformDBEntity();

        platformDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
        platformDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
        platformDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        platformDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        //platformDBEntity.Name_CN = txtSelPlatformName.Value;

        ////if (chkAll.Checked)
        ////{
        ////    platformDBEntity.OnlineStatus = null;
        ////}
        ////else if (chkOnL.Checked && chkOff.Checked)
        ////{
        ////    platformDBEntity.OnlineStatus = null;
        ////}
        ////else if (chkOff.Checked)
        ////{
        ////    platformDBEntity.OnlineStatus = "0";
        ////}
        ////else if (chkOnL.Checked)
        ////{
        ////    platformDBEntity.OnlineStatus = "1";
        ////}
        ////else
        ////{
        ////    platformDBEntity.OnlineStatus = null;
        ////}

        //if (rdbAll.Checked)
        //{
        //    platformDBEntity.OnlineStatus = null;
        //}
        //else if (rdbOnL.Checked)
        //{
        //    platformDBEntity.OnlineStatus = "1";
        //}
        //else if (rdbOff.Checked)
        //{
        //    platformDBEntity.OnlineStatus = "0";
        //}
        //else
        //{
        //    platformDBEntity.OnlineStatus = null;
        //}

        //if (chkUnTime.Checked)
        //{
        //    platformDBEntity.StartDTime = null;
        //    platformDBEntity.EndDTime = null;
        //}
        //else
        //{
        //    platformDBEntity.StartDTime = dpStart.Value;
        //    platformDBEntity.EndDTime = dpEnd.Value;
        //}

        _platformEntity.PlatformDBEntity.Add(platformDBEntity);

        DataSet dsResult = PlatformBP.Select(_platformEntity).QueryResult;

        gridViewCSPlatformList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSPlatformList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSPlatformList.DataBind();

        DropDownList ddl;
        for (int i = 0; i <= gridViewCSPlatformList.Rows.Count - 1; i++)
        {
            DataRowView drvtemp = dsResult.Tables[0].DefaultView[i];
            ddl = (DropDownList)gridViewCSPlatformList.Rows[i].FindControl("ddlOnline");
            ddl.SelectedValue = drvtemp["ONLINESTATUS"].ToString();
        }
    }

    protected void gridViewCSPlatformList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSPlatformList.Rows.Count; i++)
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["Name_CN"] = txtSelPlatformName.Value;

        if (rdbAll.Checked)
        {
           ViewState["OnlineStatus"] = "";
        }
        else if (rdbOnL.Checked)
        {
           ViewState["OnlineStatus"] = "1";
        }
        else if (rdbOff.Checked)
        {
            ViewState["OnlineStatus"] = "0";
        }
        else
        {
           ViewState["OnlineStatus"] = "";
        }

        if (chkUnTime.Checked)
        {
            ViewState["StartDTime"] = "";
            ViewState["EndDTime"] = "";
        }
        else
        {
            ViewState["StartDTime"] = dpStart.Value;
            ViewState["EndDTime"] = dpEnd.Value;
        }

        gridViewCSPlatformList.EditIndex = -1;
        BindPlatformListGrid();
    }

    protected void gridViewCSPlatformList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridViewCSPlatformList.EditIndex = e.NewEditIndex;
        BindPlatformListGrid();
        ((DropDownList)gridViewCSPlatformList.Rows[e.NewEditIndex].Cells[7].FindControl("ddlOnline")).Enabled = true;
    }

    protected void gridViewCSPlatformList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string platformNo = gridViewCSPlatformList.DataKeys[e.RowIndex].Value.ToString();
        string platformID = gridViewCSPlatformList.Rows[e.RowIndex].Cells[3].Text;
        string nameCN = ((TextBox)(gridViewCSPlatformList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        string onlineStatus = ((DropDownList)gridViewCSPlatformList.Rows[e.RowIndex].Cells[7].FindControl("ddlOnline")).SelectedValue;
        if (String.IsNullOrEmpty(nameCN))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }
        if (nameCN.Length > 32)
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
            return;
        }
        if (!btnUpdatePlatform(platformNo, platformID, nameCN, onlineStatus))
        {
            return;
        }
        gridViewCSPlatformList.EditIndex = -1;
        BindPlatformListGrid();
    }

    protected void gridViewCSPlatformList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridViewCSPlatformList.EditIndex = -1;
        BindPlatformListGrid();
    }

    public void btnAddPlatform()
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtPlatformName.Value.ToString().Trim()) || String.IsNullOrEmpty(txtPlatformID.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        _platformEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _platformEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _platformEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _platformEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _platformEntity.PlatformDBEntity = new List<PlatformDBEntity>();
        PlatformDBEntity platformDBEntity = new PlatformDBEntity();
        platformDBEntity.Name_CN = txtPlatformName.Value;
        platformDBEntity.PlatformID = txtPlatformID.Value;
        _platformEntity.PlatformDBEntity.Add(platformDBEntity);
        int iResult = PlatformBP.Insert(_platformEntity);

        _commonEntity.LogMessages = _platformEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "应用平台管理-添加";
        commonDBEntity.Event_ID = txtPlatformID.Value;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtPlatformID.Value, txtPlatformName.Value);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    public bool btnUpdatePlatform(string platformNo, string platformID, string nameCN, string onlineStatus)
    {
        messageContent.InnerHtml = "";

        _platformEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _platformEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _platformEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _platformEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _platformEntity.PlatformDBEntity = new List<PlatformDBEntity>();
        PlatformDBEntity platformDBEntity = new PlatformDBEntity();
        platformDBEntity.PlatformNo = platformNo;
        platformDBEntity.PlatformID = platformID;
        platformDBEntity.Name_CN = nameCN;
        platformDBEntity.OnlineStatus = onlineStatus;
        _platformEntity.PlatformDBEntity.Add(platformDBEntity);
        int iResult = PlatformBP.Update(_platformEntity);

        _commonEntity.LogMessages = _platformEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "应用平台管理-修改";
        commonDBEntity.Event_ID = platformID;

        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, platformDBEntity.PlatformID, platformDBEntity.Name_CN, platformDBEntity.OnlineStatus);
        commonDBEntity.Event_Content = conTent;
        bool returnValue = true;
        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else if(iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError2").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
            returnValue = false;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
            returnValue = false;
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        return returnValue;
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    //((DropDownList)gridViewCSChannelList.Rows[2].Cells[0].FindControl("ddlOnline")).SelectedValue

    //    messageContent.InnerHtml = "";
    //    _channelEntity.ChannelDBEntity = new List<ChannelDBEntity>();
    //    ChannelDBEntity channelDBEntity = new ChannelDBEntity();
         

    //    _channelEntity.ChannelDBEntity.Add(channelDBEntity);

    //    //int iResult = PlatformBP.Insert(channelEntity);

    //    //if (iResult == 1)
    //    //{
    //    //    messageContent.InnerHtml = "渠道保存成功！";
    //    //    return;
    //    //}

    //    //messageContent.InnerHtml = "渠道添加失败！";
    //}
}