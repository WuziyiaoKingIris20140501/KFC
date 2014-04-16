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

public partial class DestinationTypeDetail : BasePage
{
    DestinationEntity _destinationEntity = new DestinationEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string DestinationID = Request.QueryString["ID"].ToString();
                hidDestinationID.Value = DestinationID;
                ddlOnlinebind();
                BindTypeDDL();
                BindDestinationTypeDetail(DestinationID);
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                btnUpdate.Visible = false;
            }
        }
        else
        {
            detailMessageContent.InnerHtml = "";
        }
    }

    public void ddlOnlinebind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("ONLINESTATUS");
        dtResult.Columns.Add("ONLINEDIS");
        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
            dsResult.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";

            ddpStatusList.DataTextField = "ONLINEDIS";
            ddpStatusList.DataValueField = "ONLINESTATUS";
            ddpStatusList.DataSource = dsResult;
            ddpStatusList.DataBind();
        }
        return;
    }


    private void BindTypeDDL()
    {
        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
        DestinationDBEntity destinationDBEntity = new DestinationDBEntity();
        destinationDBEntity.DestinationID = hidDestinationID.Value;
        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);

        DataSet dsResult = DestinationBP.CommonTypeSelectSigle(_destinationEntity).QueryResult;
        ddpTypeList.DataTextField = "TYPENM";
        ddpTypeList.DataValueField = "ID";
        ddpTypeList.DataSource = dsResult;
        ddpTypeList.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //btnAddChannel();
        //BindPaymentListGrid();
        string destinationID = hidDestinationID.Value;
        string nameCN = txtDestinationName.Value;
        string parentsID = ddpTypeList.SelectedValue;
        string onlineStatus = ddpStatusList.SelectedValue;

        btnUpdateChannel(destinationID, nameCN, parentsID, onlineStatus);
    }

    ////清除控件中的数据
    //private void clearValue()
    //{
    //}

    ////发放渠道
    private void BindDestinationTypeDetail(string DestinationID)
    {
        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
        DestinationDBEntity destinationDBEntity = new DestinationDBEntity();

        destinationDBEntity.DestinationID = DestinationID;

        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);

        DataSet dsMainResult = DestinationBP.DestinationTypeDetail(_destinationEntity).QueryResult;

        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            txtDestinationName.Value = dsMainResult.Tables[0].Rows[0]["TYPENM"].ToString();
            ddpTypeList.SelectedValue = dsMainResult.Tables[0].Rows[0]["PARENTSID"].ToString();
            ddpStatusList.SelectedValue = dsMainResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString();
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
        }
    }

    protected void gridViewCSPaymentDetailList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        //for (int i = 0; i <= gridViewCSPaymentList.Rows.Count; i++)
        //{
        //    //首先判断是否是数据行
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //当鼠标停留时更改背景色
        //        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
        //        //当鼠标移开时还原背景色
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //    }
        //}
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    BindPaymentListGrid();
    //}

    protected void gridViewCSPaymentDetailList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gridViewCSPaymentList.EditIndex = e.NewEditIndex;
        //BindPaymentListGrid();
        //((DropDownList)gridViewCSPaymentList.Rows[e.NewEditIndex].Cells[7].FindControl("ddlOnline")).Enabled = true;
    }

    protected void gridViewCSPaymentDetailList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string channelNo = gridViewCSPaymentList.DataKeys[e.RowIndex].Value.ToString();
        //string channelID = gridViewCSPaymentList.Rows[e.RowIndex].Cells[3].Text;
        //string nameCN = ((TextBox)(gridViewCSPaymentList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        ////string onlineStatus = ((DropDownList)gridViewCSPaymentList.Rows[e.RowIndex].Cells[7].FindControl("ddlOnline")).SelectedValue;
        //if (String.IsNullOrEmpty(nameCN))
        //{
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
        //    return;
        //}
        //btnUpdateChannel(channelNo, channelID, nameCN, "");
        //gridViewCSPaymentList.EditIndex = -1;
        //BindPaymentListGrid();
    }

    protected void gridViewCSPaymentDetailList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //gridViewCSPaymentList.EditIndex = -1;
        //BindPaymentListGrid();
    }

    public void btnUpdateChannel(string destinationID, string nameCN, string parentsID, string onlineStatus)
    {
        detailMessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(nameCN.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString();
            return;
        }

        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
        DestinationDBEntity destinationDBEntity = new DestinationDBEntity();
        destinationDBEntity.DestinationID = destinationID;
        destinationDBEntity.Name_CN = nameCN.Trim();
        destinationDBEntity.ParentsID = parentsID;
        destinationDBEntity.OnlineStatus = onlineStatus;
        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);

        int iResult = DestinationBP.Update(_destinationEntity);

        _commonEntity.LogMessages = _destinationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "目的地类别管理-修改";
        commonDBEntity.Event_ID = destinationID;

        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, destinationDBEntity.ParentsID, destinationDBEntity.Name_CN, destinationDBEntity.OnlineStatus);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError2").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
}