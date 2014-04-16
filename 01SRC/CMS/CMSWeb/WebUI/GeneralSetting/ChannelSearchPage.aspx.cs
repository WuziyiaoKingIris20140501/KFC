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

public partial class ChannelSearchPage : BasePage
{
    ChannelEntity _channelEntity = new ChannelEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindChannelDDL();
            BindChanelListGrid();

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

    //    DataSet dsResult = ChannelBP.CommonSelect(_channelEntity).QueryResult;
    //    ddpChannelList.DataTextField = "DISPLAYNM";
    //    ddpChannelList.DataValueField = "ID";
    //    ddpChannelList.DataSource = dsResult;
    //    ddpChannelList.DataBind();
    //}

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAddChannel();
        BindChanelListGrid();
    }

    //清除控件中的数据
    private void clearValue()
    {
    }

    //发放渠道
    private void BindChanelListGrid()
    {
        _channelEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _channelEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _channelEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _channelEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _channelEntity.ChannelDBEntity = new List<ChannelDBEntity>();
        ChannelDBEntity channelDBEntity = new ChannelDBEntity();

        channelDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
        channelDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
        channelDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        channelDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

        //channelDBEntity.Name_CN = txtSelChannelName.Value;

        ////if (chkAll.Checked)
        ////{
        ////    channelDBEntity.OnlineStatus = null;
        ////}
        ////else if (chkOnL.Checked && chkOff.Checked)
        ////{
        ////    channelDBEntity.OnlineStatus = null;
        ////}
        ////else if (chkOff.Checked)
        ////{
        ////    channelDBEntity.OnlineStatus = "0";
        ////}
        ////else if (chkOnL.Checked)
        ////{
        ////    channelDBEntity.OnlineStatus = "1";
        ////}
        ////else
        ////{
        ////    channelDBEntity.OnlineStatus = null;
        ////}

        //if (rdbAll.Checked)
        //{
        //    channelDBEntity.OnlineStatus = null;
        //}
        //else if (rdbOnL.Checked)
        //{
        //    channelDBEntity.OnlineStatus = "1";
        //}
        //else if (rdbOff.Checked)
        //{
        //    channelDBEntity.OnlineStatus = "0";
        //}
        //else
        //{
        //    channelDBEntity.OnlineStatus = null;
        //}

        //if (chkUnTime.Checked)
        //{
        //    channelDBEntity.StartDTime = null;
        //    channelDBEntity.EndDTime = null;
        //}
        //else
        //{
        //    channelDBEntity.StartDTime = dpStart.Value;
        //    channelDBEntity.EndDTime = dpEnd.Value;
        //}

        _channelEntity.ChannelDBEntity.Add(channelDBEntity);

        DataSet dsResult = ChannelBP.Select(_channelEntity).QueryResult;

        gridViewCSChannelList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSChannelList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSChannelList.DataBind();

        DropDownList ddl;
        for (int i = 0; i <= gridViewCSChannelList.Rows.Count - 1; i++)
        {
            DataRowView drvtemp = dsResult.Tables[0].DefaultView[i];
            ddl = (DropDownList)gridViewCSChannelList.Rows[i].FindControl("ddlOnline");
            ddl.SelectedValue = drvtemp["ONLINESTATUS"].ToString();
        }
    }

    protected void gridViewCSChannelList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSChannelList.Rows.Count; i++)
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
        ViewState["Name_CN"] = txtSelChannelName.Value.Trim();

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
        gridViewCSChannelList.EditIndex = -1;
        BindChanelListGrid();
    }

    protected void gridViewCSChannelList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridViewCSChannelList.EditIndex = e.NewEditIndex;
        BindChanelListGrid();
        ((DropDownList)gridViewCSChannelList.Rows[e.NewEditIndex].Cells[6].FindControl("ddlOnline")).Enabled = true;
    }

    protected void gridViewCSChannelList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string channelNo = gridViewCSChannelList.DataKeys[e.RowIndex].Value.ToString();
        string channelID = gridViewCSChannelList.Rows[e.RowIndex].Cells[2].Text;
        string nameCN = ((TextBox)(gridViewCSChannelList.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        string onlineStatus = ((DropDownList)gridViewCSChannelList.Rows[e.RowIndex].Cells[6].FindControl("ddlOnline")).SelectedValue;
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
        if (!btnUpdateChannel(channelNo, channelID, nameCN, onlineStatus))
        {
            return;
        }
        gridViewCSChannelList.EditIndex = -1;
        BindChanelListGrid();
    }

    protected void gridViewCSChannelList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridViewCSChannelList.EditIndex = -1;
        BindChanelListGrid();
    }

    public void btnAddChannel()
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtChannelName.Value.ToString().Trim()) || String.IsNullOrEmpty(txtChannelID.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        _channelEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _channelEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _channelEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _channelEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _channelEntity.ChannelDBEntity = new List<ChannelDBEntity>();
        ChannelDBEntity channelDBEntity = new ChannelDBEntity();
        channelDBEntity.Name_CN = txtChannelName.Value;
        channelDBEntity.ChannelID = txtChannelID.Value;
        _channelEntity.ChannelDBEntity.Add(channelDBEntity);
        int iResult = ChannelBP.Insert(_channelEntity);

        _commonEntity.LogMessages = _channelEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "渠道管理-添加";
        commonDBEntity.Event_ID = "";

        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtChannelID.Value, txtChannelName.Value);
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

    public bool btnUpdateChannel(string channelNo, string channelID, string nameCN, string onlineStatus)
    {
        messageContent.InnerHtml = "";

        _channelEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _channelEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _channelEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _channelEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _channelEntity.ChannelDBEntity = new List<ChannelDBEntity>();
        ChannelDBEntity channelDBEntity = new ChannelDBEntity();
        channelDBEntity.ChannelNo = channelNo;
        channelDBEntity.ChannelID = channelID;
        channelDBEntity.Name_CN = nameCN;
        channelDBEntity.OnlineStatus = onlineStatus;
        _channelEntity.ChannelDBEntity.Add(channelDBEntity);
        int iResult = ChannelBP.Update(_channelEntity);

        _commonEntity.LogMessages = _channelEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "渠道管理-修改";
        commonDBEntity.Event_ID = channelID;

        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, channelDBEntity.ChannelID, channelDBEntity.Name_CN, channelDBEntity.OnlineStatus);
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