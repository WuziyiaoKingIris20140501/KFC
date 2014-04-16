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

public partial class DestinationManagerDetail : BasePage
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
                BindTypeCityDDL();
                BindDestinationListGrid();
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


        DataSet dsResultCity = DestinationBP.CitySelect(_destinationEntity).QueryResult;
        ddpCityList.DataTextField = "cityNM";
        ddpCityList.DataValueField = "cityid";
        ddpCityList.DataSource = dsResultCity;
        ddpCityList.DataBind();
    }

    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    detailMessageContent.InnerHtml = "";
    //    btnAddDestinationType();
    //    BindDestinationListGrid();
    //}

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

        destinationDBEntity.DestinationID = hidDestinationID.Value;
        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);

        DataSet dsMainResult = DestinationBP.DestinationListSelect(_destinationEntity).QueryResult;

        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            txtDestinationName.Value = dsMainResult.Tables[0].Rows[0]["DESTINATIONNM"].ToString();
            //ddpCityList.SelectedValue = dsMainResult.Tables[0].Rows[0]["CITY_ID"].ToString();
            //wctCity.AutoResult = dsMainResult.Tables[0].Rows[0]["CITYNAME"].ToString();
            ddpTypeList.SelectedValue = dsMainResult.Tables[0].Rows[0]["TYPE_ID"].ToString();
            txtAddress.Value = dsMainResult.Tables[0].Rows[0]["ADDRESSNM"].ToString();
            txtTelST.Value = dsMainResult.Tables[0].Rows[0]["TEL_ST"].ToString();
            txtTelLG.Value = dsMainResult.Tables[0].Rows[0]["TEL_LG"].ToString();
            txtLatitude.Value = dsMainResult.Tables[0].Rows[0]["LATITUDE"].ToString();
            txtLongitude.Value = dsMainResult.Tables[0].Rows[0]["LONGITUDE"].ToString();
            ddpStatusList.SelectedValue = dsMainResult.Tables[0].Rows[0]["ONLINESTATUS"].ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "InitialValue('" + dsMainResult.Tables[0].Rows[0]["CITYNAME"].ToString() + "');", true);
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
        }

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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtDestinationName.Value.ToString().Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString();
            return;
        }

        if ((String.IsNullOrEmpty(txtLatitude.Value.ToString().Trim())) || (String.IsNullOrEmpty(txtLongitude.Value.ToString().Trim())))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError3").ToString();
            return;
        }

        if ((!String.IsNullOrEmpty(txtTelST.Value.Trim()) && !RegexValidate("^[0-9]*$", txtTelST.Value.Trim())) || (!String.IsNullOrEmpty(txtTelLG.Value.Trim()) && !RegexValidate("^[0-9]*$", txtTelLG.Value.Trim())))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError4").ToString();
            return;
        }

        if (!RegexValidateData(txtLatitude.Value.ToString().Trim()) || !RegexValidateData(txtLongitude.Value.ToString().Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError5").ToString();
            return;
        }

        string strCity = hidCity.Value.ToString().Trim();
        strCity = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity;

        if (String.IsNullOrEmpty(strCity))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
            return;
        }

        _destinationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _destinationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _destinationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _destinationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _destinationEntity.DestinationDBEntity = new List<DestinationDBEntity>();
        DestinationDBEntity destinationDBEntity = new DestinationDBEntity();
        destinationDBEntity.DestinationID = hidDestinationID.Value;
        destinationDBEntity.Name_CN = txtDestinationName.Value.Trim();
        destinationDBEntity.CityID = strCity;// ddpCityList.SelectedValue;
        destinationDBEntity.DestinationTypeID = ddpTypeList.SelectedValue;
        destinationDBEntity.AddRess = txtAddress.Value.Trim();
        destinationDBEntity.TelST = txtTelST.Value.Trim();
        destinationDBEntity.TelLG = txtTelLG.Value.Trim();
        destinationDBEntity.Latitude = txtLatitude.Value.Trim();
        destinationDBEntity.Longitude = txtLongitude.Value.Trim();
        destinationDBEntity.OnlineStatus = ddpStatusList.SelectedValue;

        _destinationEntity.DestinationDBEntity.Add(destinationDBEntity);
        int iResult = DestinationBP.DestinationUpdate(_destinationEntity);

        _commonEntity.LogMessages = _destinationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "目的地管理-修改";
        commonDBEntity.Event_ID = hidDestinationID.Value;

        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, destinationDBEntity.DestinationID, destinationDBEntity.Name_CN, hidCity.Value.ToString().Trim(), ddpTypeList.SelectedValue, txtAddress.Value.Trim(), txtTelST.Value.Trim(), txtTelLG.Value.Trim(), txtLatitude.Value.Trim(), txtLongitude.Value.Trim(), destinationDBEntity.OnlineStatus);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            //Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "PageClosed();", true);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError2").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError6").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError6").ToString();
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