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

public partial class HotelGroupDetail : BasePage
{
    HotelGroupEntity _hotelGroupEntity = new HotelGroupEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string HotelGroupID = Request.QueryString["ID"].ToString();
                string strType = String.IsNullOrEmpty(Request.QueryString["TYPE"]) ? "" : Request.QueryString["TYPE"].ToString();

                hidGroupNo.Value = HotelGroupID;
                hiddenType.Value = strType;
                BindHotelGroupDetail(HotelGroupID, strType);
                lbHotelGroupTitle.Text = "编辑酒店集团";
                this.Page.Title = "编辑酒店集团";
            }
            else if (String.IsNullOrEmpty(Request.QueryString["ID"]) && !String.IsNullOrEmpty(Request.QueryString["TYPE"]) && "0".Equals(Request.QueryString["TYPE"].ToString()))
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                //btnDelete.Visible = false;
                btnUpdate.Visible = false;
                lbHotelGroupTitle.Text = "编辑酒店集团";
                this.Page.Title = "编辑酒店集团";
            }
            else
            {
                lbHotelGroupTitle.Text = "添加酒店集团";
                this.Page.Title = "添加酒店集团";
            }

            ddlTypebind();
            ddlOnlinebind();
        }
        else
        {
            detailMessageContent.InnerHtml = "";
        }
    }

    public void ddlTypebind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("BandType").ToString());
        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "BANDTYPEKEY";
            dsResult.Tables[0].Columns["Value"].ColumnName = "BANDTYPENM";

            ddpGroupTypeList.DataTextField = "BANDTYPENM";
            ddpGroupTypeList.DataValueField = "BANDTYPEKEY";
            ddpGroupTypeList.DataSource = dsResult;
            ddpGroupTypeList.DataBind();
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

    private void BindHotelGroupDetail(string HotelGroupID, string strType)
    {
        _hotelGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelGroupEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelGroupEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelGroupEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelGroupEntity.HotelGroupDBEntity = new List<HotelGroupDBEntity>();
        HotelGroupDBEntity hotelGroupDBEntity = new HotelGroupDBEntity();
        hotelGroupDBEntity.HotelGroupID = HotelGroupID;
        _hotelGroupEntity.HotelGroupDBEntity.Add(hotelGroupDBEntity);
 
        DataSet dsMainResult = HotelGroupBP.Select(_hotelGroupEntity).QueryResult;
        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            txtHotelGroupNM.Value = dsMainResult.Tables[0].Rows[0]["GROUPNM"].ToString();
            txtHotelGroupCode.Value = dsMainResult.Tables[0].Rows[0]["GROUPCODE"].ToString();
            ddpGroupTypeList.SelectedValue = dsMainResult.Tables[0].Rows[0]["BANDTYPE"].ToString();
            ddpStatusList.SelectedValue = dsMainResult.Tables[0].Rows[0]["STATUS"].ToString();
            txtDescribe.Text = dsMainResult.Tables[0].Rows[0]["GROUPDESC"].ToString();
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //btnDelete.Visible = false;
            btnUpdate.Visible = false;
        }
    }

    public void btnUpdateChannel(string actionType)
    {
        _hotelGroupEntity.HotelGroupDBEntity = new List<HotelGroupDBEntity>();

        _hotelGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelGroupEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelGroupEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelGroupEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelGroupEntity.HotelGroupDBEntity = new List<HotelGroupDBEntity>();

        HotelGroupDBEntity hotelGroupDBEntity = new HotelGroupDBEntity();

        hotelGroupDBEntity.HotelGroupID = hidGroupNo.Value;
        hotelGroupDBEntity.Name_CN = txtHotelGroupNM.Value.Trim();
        hotelGroupDBEntity.HotelGroupCode = txtHotelGroupCode.Value.Trim();
        hotelGroupDBEntity.Description = txtDescribe.Text.Trim();
        hotelGroupDBEntity.BandType = ddpGroupTypeList.SelectedValue;
        hotelGroupDBEntity.OnlineStatus = ddpStatusList.SelectedValue;
        hotelGroupDBEntity.Type = actionType;
        _hotelGroupEntity.HotelGroupDBEntity.Add(hotelGroupDBEntity);

        int iResult = "2".Equals(actionType) ? HotelGroupBP.Insert(_hotelGroupEntity) : HotelGroupBP.Update(_hotelGroupEntity);

        _commonEntity.LogMessages = _hotelGroupEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店集团管理-修改";
        commonDBEntity.Event_ID = hidGroupNo.Value;

        //detailMessageContent.InnerHtml = "支付方式更新成功！";
        string conTent = "";
        if (String.IsNullOrEmpty(hidGroupNo.Value))
        {
            conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        }
        else
        {
            conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        }

        conTent = string.Format(conTent, txtHotelGroupNM.Value, txtHotelGroupCode.Value, ddpGroupTypeList.SelectedValue);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
            if (String.IsNullOrEmpty(hidGroupNo.Value))
            {
                commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            }
            else
            {
                commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            }
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

    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    detailMessageContent.InnerHtml = "";
    //    btnUpdateChannel("0");
    //}

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtHotelGroupNM.Value.ToString().Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString(); 
            return;
        }

        if (String.IsNullOrEmpty(txtHotelGroupCode.Value.ToString().Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError4").ToString(); 
            return;
        }

        if (txtHotelGroupNM.Value.ToString().Trim().Length > 32)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError3").ToString();
            return;
        }

        if (txtDescribe.Text.ToString().Trim().Length > 200)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError5").ToString();
            return;
        }

        if (String.IsNullOrEmpty(hidGroupNo.Value))
        {
            btnUpdateChannel("2");
        }
        else
        {
            btnUpdateChannel("1");
        }
    }
}