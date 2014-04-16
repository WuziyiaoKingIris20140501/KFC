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

public partial class FacilitiesTypeDetail : BasePage
{
    HotelFacilitiesEntity _hotelfacilitiesEntity = new HotelFacilitiesEntity();
    CommonEntity _commonEntity = new CommonEntity();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string FtID = Request.QueryString["ID"].ToString();
                hidFtID.Value = FtID;
                ddlOnlinebind();
                BindFtDetailListGrid(FtID);
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
    }

    ////发放渠道
    private void BindFtDetailListGrid(string FtID)
    {
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();

        hotelFacilitiesDBEntity.FTID = FtID;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);

        DataSet dsMainResult = HotelFacilitiesBP.FtDetailSelect(_hotelfacilitiesEntity).QueryResult;
        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            txtFtCode.Value = dsMainResult.Tables[0].Rows[0]["FtCode"].ToString();
            txtFtName.Value = dsMainResult.Tables[0].Rows[0]["FtName"].ToString();
            lbFtSeq.Text = dsMainResult.Tables[0].Rows[0]["FtSeq"].ToString();
            ddpStatusList.SelectedValue = dsMainResult.Tables[0].Rows[0]["FtStatus"].ToString();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //((DropDownList)gridViewCSPaymentList.Rows[2].Cells[0].FindControl("ddlOnline")).SelectedValue

        detailMessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtFtName.Value.ToString().Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString(); 
            return;
        }

        if (String.IsNullOrEmpty(txtFtCode.Value.ToString().Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError11").ToString();
            return;
        }

        //if (txtPaymentName.Value.ToString().Trim().Length > 12)
        //{
        //    detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError3").ToString();
        //    return;
        //}

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();

        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();

        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        hotelFacilitiesDBEntity.FTID = hidFtID.Value;
        hotelFacilitiesDBEntity.FTCode = txtFtCode.Value;
        hotelFacilitiesDBEntity.FTName = txtFtName.Value;
        hotelFacilitiesDBEntity.Status = ddpStatusList.SelectedValue;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);
        
        int iResult = HotelFacilitiesBP.FtTypeUpdate(_hotelfacilitiesEntity);
        _commonEntity.LogMessages = _hotelfacilitiesEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "服务设施类别管理-修改";
        commonDBEntity.Event_ID = hidFtID.Value;

        //detailMessageContent.InnerHtml = "支付方式更新成功！";
        string conTent = "";
        conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtFtCode.Value, txtFtName.Value, lbFtSeq.Text, ddpStatusList.SelectedValue);
        commonDBEntity.Event_Type = "";
        commonDBEntity.Event_ID = "";
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
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
}