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

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class CityDetail : BasePage
{
    CityEntity _cityEntity = new CityEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string CityID = Request.QueryString["ID"].ToString();
                hidCityID.Value = CityID;
                BindCityDetail(CityID);
                ddlOnlinebind();
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                btnUpdate.Visible = false;
                btnFogRead.Visible = false;
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
    }

    private void BindCityDetail(string CityID)
    {
        _cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cityEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cityEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _cityEntity.CityDBEntity = new List<CityDBEntity>();
        CityDBEntity cityDBEntity = new CityDBEntity();
        cityDBEntity.CityID = CityID;
        _cityEntity.CityDBEntity.Add(cityDBEntity);
 
        DataSet dsMainResult = CityBP.MainSelect(_cityEntity).QueryResult;
        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            txtCityID.Value = dsMainResult.Tables[0].Rows[0]["city_id"].ToString();
            txtELCityID.Value = dsMainResult.Tables[0].Rows[0]["el_city_id"].ToString();
            txtCityNM.Value = dsMainResult.Tables[0].Rows[0]["name_cn"].ToString();
            txtSEQ.Value = dsMainResult.Tables[0].Rows[0]["seq"].ToString();
            txtPinyin.Value = dsMainResult.Tables[0].Rows[0]["pinyin"].ToString();
            txtPinyin_Short.Value = dsMainResult.Tables[0].Rows[0]["pinyin_short"].ToString();
            txtLongitude.Value = dsMainResult.Tables[0].Rows[0]["longitude"].ToString();
            txtLatitude.Value = dsMainResult.Tables[0].Rows[0]["latitude"].ToString();
            ddpStatusList.SelectedValue = dsMainResult.Tables[0].Rows[0]["STATUS"].ToString();
            if (dsMainResult.Tables[0].Rows[0]["citytypes"].ToString().Trim() != "")
            {
                if (dsMainResult.Tables[0].Rows[0]["citytypes"].ToString().Substring(0, 1) == "1") { this.ckLm.Checked = true; }
                if (dsMainResult.Tables[0].Rows[0]["citytypes"].ToString().Substring(1, 1) == "1") { this.ckHubs1.Checked = true; }
                if (dsMainResult.Tables[0].Rows[0]["citytypes"].ToString().Substring(2, 1) == "1") { this.ckYL.Checked = true; }
                if (dsMainResult.Tables[0].Rows[0]["citytypes"].ToString().Substring(3, 1) == "1") { this.ckXC.Checked = true; }
            }
            //ddpIsHot.SelectedValue = dsMainResult.Tables[0].Rows[0]["IS_HOT"].ToString();
            if (dsMainResult.Tables[0].Rows[0]["is_hot"].ToString().Trim() != "")
            {
                if (dsMainResult.Tables[0].Rows[0]["is_hot"].ToString().Substring(0, 1) == "1") { this.ckLmHotCity.Checked = true; }
                if (dsMainResult.Tables[0].Rows[0]["is_hot"].ToString().Substring(1, 1) == "1") { this.ckHubs1HotCity.Checked = true; }
                if (dsMainResult.Tables[0].Rows[0]["is_hot"].ToString().Substring(2, 1) == "1") { this.ckYLHotCity.Checked = true; }
                if (dsMainResult.Tables[0].Rows[0]["is_hot"].ToString().Substring(3, 1) == "1") { this.ckXCHotCity.Checked = true; }
            }
            if (dsMainResult.Tables[0].Rows[0]["sale_hour"].ToString().Trim() != "")
            { //11110000000 0000000 111111
                this.marketData.SelectedValue = dsMainResult.Tables[0].Rows[0]["sale_hour"].ToString().Trim();
            }
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //btnDelete.Visible = false;
            btnUpdate.Visible = false;
            btnFogRead.Visible = false;
        }
    }

    protected void btnFogRead_Click(object sender, EventArgs e)
    {
        BindFogCityDetail(hidCityID.Value);
    }

    private void BindFogCityDetail(string CityID)
    {
        _cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cityEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cityEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _cityEntity.CityDBEntity = new List<CityDBEntity>();
        CityDBEntity cityDBEntity = new CityDBEntity();
        cityDBEntity.CityID = CityID;
        _cityEntity.CityDBEntity.Add(cityDBEntity);

        DataSet dsMainResult = CityBP.FOGMainSelect(_cityEntity).QueryResult;
        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            txtCityID.Value = dsMainResult.Tables[0].Rows[0]["city_id"].ToString();
            txtCityNM.Value = dsMainResult.Tables[0].Rows[0]["name_cn"].ToString();
            txtSEQ.Value = dsMainResult.Tables[0].Rows[0]["seq"].ToString();
            txtPinyin.Value = dsMainResult.Tables[0].Rows[0]["pinyin"].ToString();
            txtPinyin_Short.Value = dsMainResult.Tables[0].Rows[0]["pinyin_short"].ToString();
            txtLongitude.Value = dsMainResult.Tables[0].Rows[0]["longitude"].ToString();
            txtLatitude.Value = dsMainResult.Tables[0].Rows[0]["latitude"].ToString();
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage1").ToString();
        }
    }

    private bool CheckSeq(string strSeq)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(strSeq, @"^\d+$") || (strSeq.Length > 3))
        {
            return false;
        }

        return true;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        bool bFLag = true;
        string errMsg = GetLocalResourceObject("UpdateError").ToString() + "<br/>";

        if (String.IsNullOrEmpty(txtCityNM.Value.ToString().Trim()))
        {
            errMsg = errMsg + GetLocalResourceObject("Error1").ToString() + "<br/>";
            bFLag = false;
        }
        if (StringUtility.Text_Length(txtCityNM.Value.ToString().Trim()) > 30)
        {
            errMsg = errMsg + GetLocalResourceObject("Error7").ToString() + "<br/>";
            bFLag = false;
        }

        if (String.IsNullOrEmpty(txtPinyin.Value.ToString().Trim()))
        {
            errMsg = errMsg + GetLocalResourceObject("Error2").ToString() + "<br/>";
            bFLag = false;
        }
        if (StringUtility.Text_Length(txtPinyin.Value.ToString().Trim()) > 40)
        {
            errMsg = errMsg + GetLocalResourceObject("Error8").ToString() + "<br/>";
            bFLag = false;
        }

        if (String.IsNullOrEmpty(txtPinyin_Short.Value.ToString().Trim()))
        {
            errMsg = errMsg + GetLocalResourceObject("Error3").ToString() + "<br/>";
            bFLag = false;
        }
        if (StringUtility.Text_Length(txtPinyin_Short.Value.ToString().Trim()) > 40)
        {
            errMsg = errMsg + GetLocalResourceObject("Error9").ToString() + "<br/>";
            bFLag = false;
        }

        //SEQ排序放置搜索界面  此处不做处理     Edit  Jason.yu  2012.08.07
        //if (String.IsNullOrEmpty(txtSEQ.Value.ToString().Trim()))
        //{
        //    errMsg = errMsg + GetLocalResourceObject("Error4").ToString() + "<br/>";
        //    bFLag = false;
        //}

        //if (!CheckSeq(txtSEQ.Value.ToString().Trim()))
        //{
        //    errMsg = errMsg + GetLocalResourceObject("Error10").ToString() + "<br/>";
        //    bFLag = false;
        //}

        if (String.IsNullOrEmpty(txtLongitude.Value.ToString().Trim()))
        {
            errMsg = errMsg + GetLocalResourceObject("Error5").ToString() + "<br/>";
            bFLag = false;
        }

        if (String.IsNullOrEmpty(txtLatitude.Value.ToString().Trim()))
        {
            errMsg = errMsg + GetLocalResourceObject("Error6").ToString() + "<br/>";
            bFLag = false;
        }

        if (!bFLag)
        {
            detailMessageContent.InnerHtml = errMsg;
            return; 
        }
        //if (String.IsNullOrEmpty(txtSEQ.Value.ToString().Trim()))
        //{
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError4").ToString(); 
        //    return;
        //}

        //if (Pinyin.Value.ToString().Trim().Length > 32)
        //{
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError3").ToString();
        //    return;
        //}

        //if (PinyinS.Text.ToString().Trim().Length > 200)
        //{
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError5").ToString();
        //    return;
        //}

        //if (Latitude.Text.ToString().Trim().Length > 200)
        //{
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError5").ToString();
        //    return;
        //}

        //if (Latitude.Text.ToString().Trim().Length > 200)
        //{
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError5").ToString();
        //    return;
        //}

        detailMessageContent.InnerHtml = "";

        _cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cityEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cityEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _cityEntity.CityDBEntity = new List<CityDBEntity>();
        CityDBEntity cityDBEntity = new CityDBEntity();
        cityDBEntity.CityID = txtCityID.Value.Trim();
        cityDBEntity.ElCityID = txtELCityID.Value.Trim();
        cityDBEntity.Name_CN = txtCityNM.Value.Trim();
        cityDBEntity.SEQ = txtSEQ.Value.Trim();
        cityDBEntity.Pinyin = (txtPinyin.Value.Trim().Length > 0) ? txtPinyin.Value.Trim().Substring(0, 1).ToUpper() + txtPinyin.Value.Trim().Substring(1) : txtPinyin.Value.Trim();
        cityDBEntity.PinyinS = txtPinyin_Short.Value.Trim();
        cityDBEntity.Longitude = txtLongitude.Value.Trim();
        cityDBEntity.Latitude = txtLatitude.Value.Trim();
        cityDBEntity.OnlineStatus = ddpStatusList.SelectedValue;
        //cityDBEntity.IsHot = ddpIsHot.SelectedValue;
        string hotCitys = (this.ckLmHotCity.Checked == true ? "1" : "0") + (this.ckHubs1HotCity.Checked == true ? "1" : "0") + (this.ckYLHotCity.Checked == true ? "1" : "0") + (this.ckXCHotCity.Checked == true ? "1" : "0");
        cityDBEntity.IsHot = hotCitys + "0000000000000000";

        //新增 城市类型 修改   edit Jason.yu  2012.8.9
        string cityTypes = (this.ckLm.Checked == true ? "1" : "0") + (this.ckHubs1.Checked == true ? "1" : "0") + (this.ckYL.Checked == true ? "1" : "0") + (this.ckXC.Checked == true ? "1" : "0");
        cityDBEntity.CityType = cityTypes + "0000000000000000";

        //新增 城市销售时间 
        cityDBEntity.SaleHour = this.marketData.SelectedValue;

        _cityEntity.CityDBEntity.Add(cityDBEntity);
        int iResult = CityBP.Update(_cityEntity);

        _commonEntity.LogMessages = _cityEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "城市管理-修改";
        commonDBEntity.Event_ID = txtCityID.Value;

        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, cityDBEntity.CityID, cityDBEntity.Name_CN, cityDBEntity.OnlineStatus, cityDBEntity.SEQ, cityDBEntity.Pinyin, cityDBEntity.PinyinS, cityDBEntity.Longitude, cityDBEntity.Latitude);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            //detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
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