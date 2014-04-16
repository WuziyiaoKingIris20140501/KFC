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

public partial class DestinationTypeSearch : BasePage
{
    DestinationEntity _destinationEntity = new DestinationEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            messageContent.InnerHtml = "";
            BindTypeDDL();
            BindDestinationTypeListGrid();
        }
    }

    private void BindTypeDDL()
    {
        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsResult = DestinationBP.CommonTypeSelect(_destinationEntity).QueryResult;
        ddpTypeList.DataTextField = "TYPENM";
        ddpTypeList.DataValueField = "ID";
        ddpTypeList.DataSource = dsResult;
        ddpTypeList.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        btnAddDestinationType();
        BindDestinationTypeListGrid();
    }

    //清除控件中的数据
    private void clearValue()
    {
    }

    //发放渠道
    public void BindDestinationTypeListGrid()
    {
        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
        DestinationDBEntity destinationDBEntity = new DestinationDBEntity();

        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);

        DataSet dsResult = DestinationBP.Select(_destinationEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSList.DataBind();

        if (!String.IsNullOrEmpty(refushFlag.Value))
        {
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            refushFlag.Value = "";
        }
    }

    protected void gridViewCSList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSList.Rows.Count; i++)
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
        BindDestinationTypeListGrid();
    }

    protected void gridViewCSList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridViewCSList.EditIndex = e.NewEditIndex;
        BindDestinationTypeListGrid();
        //((DropDownList)gridViewCSList.Rows[e.NewEditIndex].Cells[7].FindControl("ddlOnline")).Enabled = true;
    }

    protected void gridViewCSList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string channelNo = gridViewCSList.DataKeys[e.RowIndex].Value.ToString();
        //string channelID = gridViewCSList.Rows[e.RowIndex].Cells[3].Text;
        //string nameCN = ((TextBox)(gridViewCSList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        ////string onlineStatus = ((DropDownList)gridViewCSList.Rows[e.RowIndex].Cells[7].FindControl("ddlOnline")).SelectedValue;
        //if (String.IsNullOrEmpty(nameCN))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
        //    return;
        //}
        //btnUpdateChannel(channelNo, channelID, nameCN, "");
        gridViewCSList.EditIndex = -1;
        BindDestinationTypeListGrid();
    }

    protected void gridViewCSList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridViewCSList.EditIndex = -1;
        BindDestinationTypeListGrid();
    }

    protected void gridViewCSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSList.PageIndex = e.NewPageIndex;
        BindDestinationTypeListGrid();
    }

    public void btnAddDestinationType()
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtDestinationTypeNM.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
        DestinationDBEntity destinationDBEntity = new DestinationDBEntity();

        destinationDBEntity.Name_CN = txtDestinationTypeNM.Value.Trim();
        destinationDBEntity.ParentsID = ddpTypeList.SelectedValue;

        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);
        int iResult = DestinationBP.Insert(_destinationEntity);

        _commonEntity.LogMessages = _destinationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "目的地类别管理-添加";
        commonDBEntity.Event_ID = "";

        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        //conTent = string.Format(conTent, txtPaymentID.Value, txtPaymentName.Value);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            BindTypeDDL();
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

    //public void btnUpdateChannel(string paymentNo, string paymentID, string nameCN, string onlineStatus)
    //{
    //    messageContent.InnerHtml = "";

    //    _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _destinationEntity.LogMessages.Userid = "test";
    //    _destinationEntity.LogMessages.Username = "test";

    //    _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
    //    DestinationDBEntity destinationDBEntity = new DestinationDBEntity();
    //    destinationDBEntity.PaymentNo = paymentNo;
    //    destinationDBEntity.PaymentID = paymentID;
    //    destinationDBEntity.Name_CN = nameCN;
    //    destinationDBEntity.OnlineStatus = onlineStatus;
    //    _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);
    //    int iResult = DestinationBP.Update(_destinationEntity);

    //    _commonEntity.LogMessages = _destinationEntity.LogMessages;
    //    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
    //    CommonDBEntity commonDBEntity = new CommonDBEntity();

    //    commonDBEntity.Event_Type = "";
    //    commonDBEntity.Event_ID = "";

    //    string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
    //    conTent = string.Format(conTent, destinationDBEntity.PaymentID, destinationDBEntity.Name_CN, destinationDBEntity.OnlineStatus);
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
}