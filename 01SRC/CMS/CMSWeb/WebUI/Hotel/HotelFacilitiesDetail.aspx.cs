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

public partial class HotelFacilitiesDetail : BasePage
{
    HotelFacilitiesEntity _hotelfacilitiesEntity = new HotelFacilitiesEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string strID = Request.QueryString["ID"].ToString();
                hiddenId.Value = strID;
                ddlOnlinebind();
                BindTypeDDL();
                BindContentDetail(strID);
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
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        DataSet dsResult = HotelFacilitiesBP.CommonFtTypeSelect(_hotelfacilitiesEntity).QueryResult;
        ddpFtTypeList.DataTextField = "TYPENAME";
        ddpFtTypeList.DataValueField = "TYPECODE";
        ddpFtTypeList.DataSource = dsResult;
        ddpFtTypeList.DataBind();
    }

    private void BindContentDetail(string strID)
    {
        detailMessageContent.InnerHtml = "";
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);

        hotelFacilitiesDBEntity.ID = strID;

        DataSet dsResult = dsResult = HotelFacilitiesBP.SelectTypeDetail(_hotelfacilitiesEntity).QueryResult;
        
        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            btnUpdate.Visible = false;
            return;
        }

        lbId.Text = dsResult.Tables[0].Rows[0]["id"].ToString();
        txtName.Value = dsResult.Tables[0].Rows[0]["name_zh"].ToString();
        ddpStatusList.SelectedValue = dsResult.Tables[0].Rows[0]["status"].ToString();
        ddpFtTypeList.SelectedValue = dsResult.Tables[0].Rows[0]["TYPE"].ToString();
        lbFtSeq.Text = dsResult.Tables[0].Rows[0]["SEQ"].ToString();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtName.Value.ToString().Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError4").ToString(); 
            return;
        }

        if (txtName.Value.ToString().Trim().Length > 12)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError3").ToString();
            return;
        }

        if (String.IsNullOrEmpty(ddpFtTypeList.SelectedValue))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError5").ToString();
            return;
        }

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();

        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        hotelFacilitiesDBEntity.ID = hiddenId.Value;
        hotelFacilitiesDBEntity.Name_CN = txtName.Value;
        hotelFacilitiesDBEntity.OnlineStatus = ddpStatusList.SelectedValue;
        hotelFacilitiesDBEntity.Type = ddpFtTypeList.SelectedValue;
        hotelFacilitiesDBEntity.FTSeq = lbFtSeq.Text;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);

        int iResult = HotelFacilitiesBP.Update(_hotelfacilitiesEntity);
        _commonEntity.LogMessages = _hotelfacilitiesEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        string conTent = "";
        conTent = GetLocalResourceObject("EventInsertMessage").ToString();

        conTent = string.Format(conTent, txtName.Value, ddpStatusList.SelectedValue);
        commonDBEntity.Event_Type = "酒店设置服务详细-更新";
        commonDBEntity.Event_ID = txtName.Value;
        commonDBEntity.Event_Content = conTent;
        if (iResult == 1)
        {
            Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else if (iResult == 2)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError2").ToString();
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError1").ToString();            
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
    protected void ddpFtTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbFtSeq.Text = GetMaxSeqForUpdate();
    }

    private string GetMaxSeqForUpdate()
    {
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelfacilitiesDBEntity = new HotelFacilitiesDBEntity();

        hotelfacilitiesDBEntity.ID = hiddenId.Value;
        hotelfacilitiesDBEntity.Type = ddpFtTypeList.SelectedValue;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelfacilitiesDBEntity);

        DataSet dsResult = HotelFacilitiesBP.GetFtHotelMaxForUpdate(_hotelfacilitiesEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            return dsResult.Tables[0].Rows[0][0].ToString();
        }

        return GetFtTypeMaxSeq();
    }

    private string GetFtTypeMaxSeq()
    {
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelfacilitiesDBEntity = new HotelFacilitiesDBEntity();
        hotelfacilitiesDBEntity.Type = ddpFtTypeList.SelectedValue;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelfacilitiesDBEntity);

        DataSet dsResult = HotelFacilitiesBP.GetFtHotelMaxSeq(_hotelfacilitiesEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            return dsResult.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            return "1";
        }
    }
}