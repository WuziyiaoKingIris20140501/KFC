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
using System.Text.RegularExpressions;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class DestinationManagerSearch : BasePage
{
    DestinationEntity _destinationEntity = new DestinationEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            messageContent.InnerHtml = "";
            BindTypeCityDDL();
            ddlOnlinebind();
            BindDestinationListGrid();
        }
    }

    private void BindTypeCityDDL()
    {
        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsResult = DestinationBP.TypeSelect(_destinationEntity).QueryResult;
        ddpTypeList.DataTextField = "TYPENM";
        ddpTypeList.DataValueField = "ID";
        ddpTypeList.DataSource = dsResult;
        ddpTypeList.DataBind();

        DataRow drTemp = dsResult.Tables[0].NewRow();
        drTemp["ID"] = DBNull.Value;
        drTemp["TYPENM"] = "不限制";
        dsResult.Tables[0].Rows.InsertAt(drTemp,0);
        ddpSelType.DataTextField = "TYPENM";
        ddpSelType.DataValueField = "ID";
        ddpSelType.DataSource = dsResult;
        ddpSelType.DataBind();


        DataSet dsResultCity = DestinationBP.CitySelect(_destinationEntity).QueryResult;
        ddpCityList.DataTextField = "cityNM";
        ddpCityList.DataValueField = "cityid";
        ddpCityList.DataSource = dsResultCity;
        ddpCityList.DataBind();


        DataRow drTempcity = dsResultCity.Tables[0].NewRow();
        drTempcity["cityid"] = DBNull.Value;
        drTempcity["cityNM"] = "不限制";
        dsResultCity.Tables[0].Rows.InsertAt(drTempcity, 0);

        ddpSelCity.DataTextField = "cityNM";
        ddpSelCity.DataValueField = "cityid";
        ddpSelCity.DataSource = dsResultCity;
        ddpSelCity.DataBind();
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


            DataRow drTemp = dsResult.Tables[0].NewRow();
            drTemp["ONLINESTATUS"] = DBNull.Value;
            drTemp["ONLINEDIS"] = "不限制";
            dsResult.Tables[0].Rows.InsertAt(drTemp, 0);

            ddpStatusList.DataTextField = "ONLINEDIS";
            ddpStatusList.DataValueField = "ONLINESTATUS";
            ddpStatusList.DataSource = dsResult;
            ddpStatusList.DataBind();
        }
        return;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        btnAddDestinationType();
        BindDestinationListGrid();
    }

    //清除控件中的数据
    private void clearValue()
    {
    }

    //发放渠道
    public void BindDestinationListGrid()
    {
        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
        DestinationDBEntity destinationDBEntity = new DestinationDBEntity();
        
        destinationDBEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        destinationDBEntity.DestinationTypeID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TypeID"].ToString())) ? null : ViewState["TypeID"].ToString();
        destinationDBEntity.Status = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Status"].ToString())) ? null : ViewState["Status"].ToString();

        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);

        DataSet dsResult = DestinationBP.DestinationListSelect(_destinationEntity).QueryResult;

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
        //ViewState["CityID"] = ddpSelCity.SelectedValue;
        string strCity = hidSelCity.Value.ToString().Trim();
        ViewState["CityID"] = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity;
        ViewState["TypeID"] = ddpSelType.SelectedValue;
        ViewState["Status"] = ddpStatusList.SelectedValue;

        BindDestinationListGrid();
    }

    protected void btnRefesh_Click(object sender, EventArgs e)
    {
        BindDestinationListGrid();
    }

    protected void gridViewCSList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridViewCSList.EditIndex = e.NewEditIndex;
        BindDestinationListGrid();
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
        BindDestinationListGrid();
    }

    protected void gridViewCSList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridViewCSList.EditIndex = -1;
        BindDestinationListGrid();
    }

    protected void gridViewCSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSList.PageIndex = e.NewPageIndex;
        BindDestinationListGrid();
    }

    private static bool RegexValidate(string regexString, string validateString)
    {
        Regex regex = new Regex(regexString);
        return regex.IsMatch(validateString.Trim());
    }

    private static bool RegexValidateData(string validateString)
    {
        try
        {
            decimal dec = decimal.Parse(validateString);
            return true;
        }
        catch
        {
            return false;
        } 
    }

    public void btnAddDestinationType()
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtDestinationName.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        if ((String.IsNullOrEmpty(txtLatitude.Value.ToString().Trim())) || (String.IsNullOrEmpty(txtLongitude.Value.ToString().Trim())))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
            return;
        }

        if ((!String.IsNullOrEmpty(txtTelST.Value.Trim()) && !RegexValidate("^[0-9]*$", txtTelST.Value.Trim())) || (!String.IsNullOrEmpty(txtTelLG.Value.Trim()) && !RegexValidate("^[0-9]*$", txtTelLG.Value.Trim())))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error5").ToString();
            return;
        }

        if (!RegexValidateData(txtLatitude.Value.ToString().Trim()) || !RegexValidateData(txtLongitude.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error6").ToString();
            return;
        }

        string strCity = hidCity.Value.ToString().Trim();
        strCity = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity;

        if (String.IsNullOrEmpty(strCity))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
            return;
        }

        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
        DestinationDBEntity destinationDBEntity = new DestinationDBEntity();

        destinationDBEntity.Name_CN = txtDestinationName.Value.Trim();
        destinationDBEntity.CityID = strCity;// ddpCityList.SelectedValue;
        destinationDBEntity.DestinationTypeID = ddpTypeList.SelectedValue;
        destinationDBEntity.AddRess = txtAddress.Value.Trim();
        destinationDBEntity.TelST = txtTelST.Value.Trim();
        destinationDBEntity.TelLG = txtTelLG.Value.Trim();
        destinationDBEntity.Latitude = txtLatitude.Value.Trim();
        destinationDBEntity.Longitude = txtLongitude.Value.Trim();

        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);
        int iResult = DestinationBP.DestinationInsert(_destinationEntity);

        _commonEntity.LogMessages = _destinationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "目的地管理-添加";
        commonDBEntity.Event_ID = "";

        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtDestinationName.Value.Trim(), ddpTypeList.SelectedValue, hidCity.Value.ToString().Trim());
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
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error8").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error8").ToString();
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