using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

public partial class UserGroupDetailPage : BasePage
{
    UserGroupEntity _userGroupEntity = new UserGroupEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string UserGroupID = Request.QueryString["ID"].ToString().Trim();
                hidUserGroupID.Value = UserGroupID;
                ////BindChannelDDL();
                BindUserGroupMainListDetail();
                AspNetPager1.CurrentPageIndex = 1;
                BindViewCSUserGroupListDetail();
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            }
        }
        //messageContent.InnerHtml = "";
    }

    //public DataSet ddlOnlinebind()
    //{
    //    DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
    //    DataTable dtResult = new DataTable();
    //    dtResult.Columns.Add("ONLINESTATUS");
    //    dtResult.Columns.Add("ONLINEDIS");
    //    if (dsResult.Tables.Count > 0)
    //    {
    //        dsResult.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
    //        dsResult.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";
    //    }
    //    return dsResult;
    //}

    //private void BindRegChannelDDL()
    //{
    //    _userGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _userGroupEntity.LogMessages.Userid = "test";
    //    _userGroupEntity.LogMessages.Username = "test";

    //    DataSet dsResult = UserGroupBP.GetRegChannelList(_userGroupEntity).QueryResult;
    //    ddpRegChannelList.DataTextField = "REGCHANELNM";
    //    ddpRegChannelList.DataValueField = "REGCHANELCODE";
    //    ddpRegChannelList.DataSource = dsResult;
    //    ddpRegChannelList.DataBind();

    //    DataSet dsPlatFormResult = UserGroupBP.GetPlatFormList(_userGroupEntity).QueryResult;
    //    chkPlatformList.DataTextField = "PLATFORMNM";
    //    chkPlatformList.DataValueField = "PLATFORMCODE";
    //    chkPlatformList.DataSource = dsPlatFormResult;
    //    chkPlatformList.DataBind();
    //}

    //protected void btnAddUserGroup_Click(object sender, EventArgs e)
    //{
    //    //btnAddChannel();
    //    //BindChanelListGrid();
    //    string RergChannleList = hidRegChannelList.Value;

    //    foreach (ListItem li in chkPlatformList.Items)
    //    {
    //        if (li.Selected)
    //        {
                
    //        }
    //    }


    //}

    //[WebMethod]
    //public static string HelloWorld(string parm)
    //{
        
    //    AddUserGroupList();
    //    return "123--->456";
    //}

    //private static string AddUserGroupList()
    //{
       
    //    return "";
    //}

    //[System.Web.Services.WebMethod]
    //public static string GetDataTable()
    //{
    //    return DataTable2Json(CreateDataTable());
    //}

    //public static System.Data.DataTable CreateDataTable()
    //{
    //    System.Data.DataTable dataTable1 = new System.Data.DataTable("BlogUser");
    //    System.Data.DataRow dr;
    //    dataTable1.Columns.Add(new System.Data.DataColumn("UserId", typeof(System.Int32)));
    //    dataTable1.Columns.Add(new System.Data.DataColumn("UserName", typeof(System.String)));
    //    dataTable1.PrimaryKey = new System.Data.DataColumn[] { dataTable1.Columns["UserId"] };

    //    for (int i = 0; i < 8; i++)
    //    {
    //        dr = dataTable1.NewRow();
    //        dr[0] = i;
    //        dr[1] = "【孟子E章】" + i.ToString() + " 前端传递的参数的值分别是:";
    //        dataTable1.Rows.Add(dr);
    //    }
    //    return dataTable1;
    //}

    //public static string DataTable2Json(System.Data.DataTable dt)
    //{
    //    StringBuilder jsonBuilder = new StringBuilder();
    //    jsonBuilder.Append("{\"");
    //    jsonBuilder.Append(dt.TableName.ToString());
    //    jsonBuilder.Append("\":[");
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        jsonBuilder.Append("{");
    //        for (int j = 0; j < dt.Columns.Count; j++)
    //        {
    //            jsonBuilder.Append("\"");
    //            jsonBuilder.Append(dt.Columns[j].ColumnName);
    //            jsonBuilder.Append("\":\"");
    //            jsonBuilder.Append(dt.Rows[i][j].ToString());
    //            jsonBuilder.Append("\",");
    //        }
    //        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
    //        jsonBuilder.Append("},");
    //    }
    //    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
    //    jsonBuilder.Append("]");
    //    jsonBuilder.Append("}");
    //    return jsonBuilder.ToString();
    //}  


    //[WebMethod]
    //public static void dddddddaaaaaa()
    //{
    //    string RergChannleList = "";
    //}

    //protected void btnAddRegChannel_Click(object sender, EventArgs e)
    //{
        //LinkButton lkbtn = new LinkButton();

        //lkbtn.Text = ddpRegChannelList.SelectedItem.ToString();
        //lkbtn.ID = "lkbtn_" + ddpRegChannelList.SelectedValue.ToString();
        //lkbtn.Click
        //dvRegChannelList.Controls.Remove();
        //dvRegChannelList.Controls.Add(lkbtn);
        //btnAddChannel();
        //BindChanelListGrid();
    //}

    ////清除控件中的数据
    //private void clearValue()
    //{
    //}

    private void BindUserGroupMainListDetail()
    {
        _userGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userGroupEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userGroupEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userGroupEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userGroupEntity.UserGroupDBEntity = new List<UserGroupDBEntity>();
        UserGroupDBEntity userGroupDBEntity = new UserGroupDBEntity();

        userGroupDBEntity.UserGroupID = hidUserGroupID.Value;

        _userGroupEntity.UserGroupDBEntity.Add(userGroupDBEntity);

        DataSet dsMainResult = UserGroupBP.UserMainListSelect(_userGroupEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        lbUserGroupNM.Text = dsMainResult.Tables[0].Rows[0]["USERGROUPNAME"].ToString();
        lbRegChannelList.Text = dsMainResult.Tables[0].Rows[0]["REGCHANELNM"].ToString();
        lbPlatformList.Text = dsMainResult.Tables[0].Rows[0]["PLATFORMNM"].ToString();
        lbRegistDTime.Text = dsMainResult.Tables[0].Rows[0]["REGISTTIME"].ToString();
        lbLoginDTime.Text = dsMainResult.Tables[0].Rows[0]["LOGINTIME"].ToString();
        lbSubmitOrder.Text = dsMainResult.Tables[0].Rows[0]["SUBMITORDER"].ToString();
        lbCompleteOrder.Text = dsMainResult.Tables[0].Rows[0]["COMPLETEORDER"].ToString();
        lbLastOrder.Text = dsMainResult.Tables[0].Rows[0]["LASTORDER"].ToString();
        lbManualCount.Text = dsMainResult.Tables[0].Rows[0]["MANUALCOUNT"].ToString();
        txtManualAdd.Text = dsMainResult.Tables[0].Rows[0]["MANUALADD"].ToString();
        lbUserCount.Text = dsMainResult.Tables[0].Rows[0]["AUTOCOUNT"].ToString();
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindViewCSUserGroupListDetail();
    }

    private void BindViewCSUserGroupListDetail()
    {
        _userGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userGroupEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userGroupEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userGroupEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userGroupEntity.UserGroupDBEntity = new List<UserGroupDBEntity>();
        UserGroupDBEntity userGroupDBEntity = new UserGroupDBEntity();

        userGroupDBEntity.UserGroupID = hidUserGroupID.Value;
        _userGroupEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _userGroupEntity.PageSize = gridViewCSUserGroupListDetail.PageSize;
        _userGroupEntity.UserGroupDBEntity.Add(userGroupDBEntity);

        DataSet dsDetailResult = UserGroupBP.UserDetailListQuery(_userGroupEntity).QueryResult;

        gridViewCSUserGroupListDetail.DataSource = dsDetailResult.Tables[0].DefaultView;
        gridViewCSUserGroupListDetail.DataKeyNames = new string[] { "loginmobile" };//主键
        gridViewCSUserGroupListDetail.DataBind();

        AspNetPager1.PageSize = gridViewCSUserGroupListDetail.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();
    }


    private int CountLmSystemLog()
    {
        _userGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userGroupEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userGroupEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userGroupEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userGroupEntity.UserGroupDBEntity = new List<UserGroupDBEntity>();
        UserGroupDBEntity userGroupDBEntity = new UserGroupDBEntity();

        userGroupDBEntity.UserGroupID = hidUserGroupID.Value;
        _userGroupEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _userGroupEntity.PageSize = gridViewCSUserGroupListDetail.PageSize;
        _userGroupEntity.UserGroupDBEntity.Add(userGroupDBEntity);

        DataSet dsResult = UserGroupBP.UserDetailListCount(_userGroupEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
        }

        return 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        _userGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userGroupEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userGroupEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userGroupEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userGroupEntity.UserGroupDBEntity = new List<UserGroupDBEntity>();
        UserGroupDBEntity userGroupDBEntity = new UserGroupDBEntity();
        userGroupDBEntity.UserGroupID = hidUserGroupID.Value;
        _userGroupEntity.UserGroupDBEntity.Add(userGroupDBEntity);

        DataSet dsResult = new DataSet();
        dsResult = UserGroupBP.ExportUserDetailList(_userGroupEntity).QueryResult;
        CommonFunction.ExportExcelForLM(dsResult);
    }

    protected void gridViewCSUserGroupListDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    BindChanelListGrid();
    //}

    protected void gridViewCSUserGroupListDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
    //    gridViewCSChannelList.EditIndex = e.NewEditIndex;
    //    BindChanelListGrid();
    //    ((DropDownList)gridViewCSChannelList.Rows[e.NewEditIndex].Cells[7].FindControl("ddlOnline")).Enabled = true;
    }

    protected void gridViewCSUserGroupListDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    //    string channelNo = gridViewCSChannelList.DataKeys[e.RowIndex].Value.ToString();
    //    string channelID = gridViewCSChannelList.Rows[e.RowIndex].Cells[3].Text;
    //    string nameCN = ((TextBox)(gridViewCSChannelList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
    //    string onlineStatus = ((DropDownList)gridViewCSChannelList.Rows[e.RowIndex].Cells[7].FindControl("ddlOnline")).SelectedValue;
    //    if (String.IsNullOrEmpty(nameCN))
    //    {
    //        messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
    //        return;
    //    }
    //    btnUpdateChannel(channelNo, channelID, nameCN, onlineStatus);
    //    gridViewCSChannelList.EditIndex = -1;
    //    BindChanelListGrid();
    }

    protected void gridViewCSUserGroupListDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    //    gridViewCSChannelList.EditIndex = -1;
    //    BindChanelListGrid();
    }

    protected void gridViewCSUserGroupListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSUserGroupListDetail.PageIndex = e.NewPageIndex;
        BindViewCSUserGroupListDetail();
    }

    //public void btnAddChannel()
    //{
    //    messageContent.InnerHtml = "";

    //    if (String.IsNullOrEmpty(txtChannelName.Value.ToString().Trim()) || String.IsNullOrEmpty(txtChannelID.Value.ToString().Trim()))
    //    {
    //        messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
    //        return;
    //    }

    //    _userGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _userGroupEntity.LogMessages.Userid = "test";
    //    _userGroupEntity.LogMessages.Username = "test";

    //    _userGroupEntity.ChannelDBEntity = new List<ChannelDBEntity>();
    //    ChannelDBEntity userGroupDBEntity = new ChannelDBEntity();
    //    userGroupDBEntity.Name_CN = txtChannelName.Value;
    //    userGroupDBEntity.ChannelID = txtChannelID.Value;
    //    _userGroupEntity.ChannelDBEntity.Add(userGroupDBEntity);
    //    int iResult = ChannelBP.Insert(_userGroupEntity);

    //    _commonEntity.LogMessages = _userGroupEntity.LogMessages;
    //    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
    //    CommonDBEntity commonDBEntity = new CommonDBEntity();

    //    commonDBEntity.Event_Type = "";
    //    commonDBEntity.Event_ID = "";



    //    string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
    //    conTent = string.Format(conTent, txtChannelID.Value, txtChannelName.Value);
    //    commonDBEntity.Event_Content = conTent;

    //    if (iResult == 1)
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
    //    }
    //    else if (iResult == 2)
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
    //    }
    //    else
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
    //    }

    //    _commonEntity.CommonDBEntity.Add(commonDBEntity);
    //    CommonBP.InsertEventHistory(_commonEntity);
    //}

    //public void btnUpdateChannel(string channelNo, string channelID, string nameCN, string onlineStatus)
    //{
    //    messageContent.InnerHtml = "";

    //    _userGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _userGroupEntity.LogMessages.Userid = "test";
    //    _userGroupEntity.LogMessages.Username = "test";

    //    _userGroupEntity.ChannelDBEntity = new List<ChannelDBEntity>();
    //    ChannelDBEntity userGroupDBEntity = new ChannelDBEntity();
    //    userGroupDBEntity.ChannelNo = channelNo;
    //    userGroupDBEntity.ChannelID = channelID;
    //    userGroupDBEntity.Name_CN = nameCN;
    //    userGroupDBEntity.OnlineStatus = onlineStatus;
    //    _userGroupEntity.ChannelDBEntity.Add(userGroupDBEntity);
    //    int iResult = ChannelBP.Update(_userGroupEntity);

    //    _commonEntity.LogMessages = _userGroupEntity.LogMessages;
    //    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
    //    CommonDBEntity commonDBEntity = new CommonDBEntity();

    //    commonDBEntity.Event_Type = "";
    //    commonDBEntity.Event_ID = "";

    //    string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
    //    conTent = string.Format(conTent, userGroupDBEntity.ChannelID, userGroupDBEntity.Name_CN, userGroupDBEntity.OnlineStatus);
    //    commonDBEntity.Event_Content = conTent;
        
    //    if (iResult == 1)
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
    //    }
    //    else
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
    //    }

    //    _commonEntity.CommonDBEntity.Add(commonDBEntity);
    //    CommonBP.InsertEventHistory(_commonEntity);
    //}

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    //((DropDownList)gridViewCSChannelList.Rows[2].Cells[0].FindControl("ddlOnline")).SelectedValue

    //    messageContent.InnerHtml = "";
    //    _userGroupEntity.ChannelDBEntity = new List<ChannelDBEntity>();
    //    ChannelDBEntity userGroupDBEntity = new ChannelDBEntity();
         

    //    _userGroupEntity.ChannelDBEntity.Add(userGroupDBEntity);

    //    //int iResult = ChannelBP.Insert(channelEntity);

    //    //if (iResult == 1)
    //    //{
    //    //    messageContent.InnerHtml = "渠道保存成功！";
    //    //    return;
    //    //}

    //    //messageContent.InnerHtml = "渠道添加失败！";
    //}
}