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

public partial class RegChannelSearchPage : BasePage
{
    RegChannelEntity _regChannelEntity = new RegChannelEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindChannelDDL();
            BindRegChanelListGrid();
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

    //    DataSet dsResult = ChannelBP.CommonSelect(_channelEntity).QueryResult;
    //    ddpChannelList.DataTextField = "DISPLAYNM";
    //    ddpChannelList.DataValueField = "ID";
    //    ddpChannelList.DataSource = dsResult;
    //    ddpChannelList.DataBind();
    //}

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAddRegChannel();
        BindRegChanelListGrid();
    }

    //清除控件中的数据
    private void clearValue()
    {
    }

    //发放渠道
    private void BindRegChanelListGrid()
    {
        _regChannelEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _regChannelEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _regChannelEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _regChannelEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _regChannelEntity.RegChannelDBEntity = new List<RegChannelDBEntity>();
        RegChannelDBEntity regChannelDBEntity = new RegChannelDBEntity();

        regChannelDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
        regChannelDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
        regChannelDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        regChannelDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

        //regChannelDBEntity.Name_CN = txtSelRegChannelName.Value;

        ////if (chkAll.Checked)
        ////{
        ////    regChannelDBEntity.OnlineStatus = null;
        ////}
        ////else if (chkOnL.Checked && chkOff.Checked)
        ////{
        ////    regChannelDBEntity.OnlineStatus = null;
        ////}
        ////else if (chkOff.Checked)
        ////{
        ////    regChannelDBEntity.OnlineStatus = "0";
        ////}
        ////else if (chkOnL.Checked)
        ////{
        ////    regChannelDBEntity.OnlineStatus = "1";
        ////}
        ////else
        ////{
        ////    regChannelDBEntity.OnlineStatus = null;
        ////}

        //if (rdbAll.Checked)
        //{
        //    regChannelDBEntity.OnlineStatus = null;
        //}
        //else if (rdbOnL.Checked)
        //{
        //    regChannelDBEntity.OnlineStatus = "1";
        //}
        //else if (rdbOff.Checked)
        //{
        //    regChannelDBEntity.OnlineStatus = "0";
        //}
        //else
        //{
        //    regChannelDBEntity.OnlineStatus = null;
        //}

        //if (chkUnTime.Checked)
        //{
        //    regChannelDBEntity.StartDTime = null;
        //    regChannelDBEntity.EndDTime = null;
        //}
        //else
        //{
        //    regChannelDBEntity.StartDTime = dpStart.Value;
        //    regChannelDBEntity.EndDTime = dpEnd.Value;
        //}

        _regChannelEntity.RegChannelDBEntity.Add(regChannelDBEntity);

        DataSet dsResult = RegChannelBP.Select(_regChannelEntity).QueryResult;

        gridViewCSRegChannelList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSRegChannelList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSRegChannelList.DataBind();

        DropDownList ddl;
        for (int i = 0; i <= gridViewCSRegChannelList.Rows.Count - 1; i++)
        {
            DataRowView drvtemp = dsResult.Tables[0].DefaultView[i];
            ddl = (DropDownList)gridViewCSRegChannelList.Rows[i].FindControl("ddlOnline");
            ddl.SelectedValue = drvtemp["ONLINESTATUS"].ToString();
        }
    }

    protected void gridViewCSRegChannelList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSRegChannelList.Rows.Count; i++)
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
        ViewState["Name_CN"] = txtSelRegChannelName.Value.Trim();

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

        gridViewCSRegChannelList.EditIndex = -1;
        BindRegChanelListGrid();
    }

    protected void gridViewCSRegChannelList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridViewCSRegChannelList.EditIndex = e.NewEditIndex;
        BindRegChanelListGrid();
        ((DropDownList)gridViewCSRegChannelList.Rows[e.NewEditIndex].Cells[7].FindControl("ddlOnline")).Enabled = true;
    }

    protected void gridViewCSRegChannelList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string regChannelNo = gridViewCSRegChannelList.DataKeys[e.RowIndex].Value.ToString();
        string regChannelID = gridViewCSRegChannelList.Rows[e.RowIndex].Cells[3].Text;
        string nameCN = ((TextBox)(gridViewCSRegChannelList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        string onlineStatus = ((DropDownList)gridViewCSRegChannelList.Rows[e.RowIndex].Cells[7].FindControl("ddlOnline")).SelectedValue;
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
        if (!btnUpdateRegChannel(regChannelNo, regChannelID, nameCN, onlineStatus))
        {
            return;
        }
        gridViewCSRegChannelList.EditIndex = -1;
        BindRegChanelListGrid();
    }

    protected void gridViewCSRegChannelList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridViewCSRegChannelList.EditIndex = -1;
        BindRegChanelListGrid();
    }

    public void btnAddRegChannel()
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtRegChannelName.Value.ToString().Trim()) || String.IsNullOrEmpty(txtRegChannelID.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        _regChannelEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _regChannelEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _regChannelEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _regChannelEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _regChannelEntity.RegChannelDBEntity = new List<RegChannelDBEntity>();
        RegChannelDBEntity regChannelDBEntity = new RegChannelDBEntity();
        regChannelDBEntity.Name_CN = txtRegChannelName.Value;
        regChannelDBEntity.RegChannelID = txtRegChannelID.Value;
        _regChannelEntity.RegChannelDBEntity.Add(regChannelDBEntity);
        int iResult = RegChannelBP.Insert(_regChannelEntity);

        _commonEntity.LogMessages = _regChannelEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "注册渠道管理-添加";
        commonDBEntity.Event_ID = txtRegChannelID.Value;

        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtRegChannelID.Value, txtRegChannelName.Value);
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

    public bool btnUpdateRegChannel(string regChannelNo, string regChannelID, string nameCN, string onlineStatus)
    {
        messageContent.InnerHtml = "";

        _regChannelEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _regChannelEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _regChannelEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _regChannelEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _regChannelEntity.RegChannelDBEntity = new List<RegChannelDBEntity>();
        RegChannelDBEntity regChannelDBEntity = new RegChannelDBEntity();
        regChannelDBEntity.RegChannelNo = regChannelNo;
        regChannelDBEntity.RegChannelID = regChannelID;
        regChannelDBEntity.Name_CN = nameCN;
        regChannelDBEntity.OnlineStatus = onlineStatus;
        _regChannelEntity.RegChannelDBEntity.Add(regChannelDBEntity);
        int iResult = RegChannelBP.Update(_regChannelEntity);

        _commonEntity.LogMessages = _regChannelEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "注册渠道管理-修改";
        commonDBEntity.Event_ID = regChannelID;

        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, regChannelDBEntity.RegChannelID, regChannelDBEntity.Name_CN, regChannelDBEntity.OnlineStatus);
        commonDBEntity.Event_Content = conTent;
        bool returnValue = true;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else if (iResult == 2)
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

    //    //int iResult = ChannelBP.Insert(channelEntity);

    //    //if (iResult == 1)
    //    //{
    //    //    messageContent.InnerHtml = "渠道保存成功！";
    //    //    return;
    //    //}

    //    //messageContent.InnerHtml = "渠道添加失败！";
    //}
}