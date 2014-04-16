using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Process.Promotion;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Entity.Promotion;

public partial class PromotionInfoDetail : BasePage
{
    PromotionEntity _promotionEntity = new PromotionEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string strID = Request.QueryString["ID"].ToString();
                string strType = Request.QueryString["TYPE"].ToString();
                hiddenId.Value = strID;
                hiddenType.Value = strType;
                
                BindPromotionType();
                BindDropDownList();
                BindContentDetail();
                

                if ("1".Equals(strType))
                {
                    dvBtnUpdateList.Visible = true;
                    //dvBtnBack.Visible = false;
                }
                else
                {
                    dvBtnUpdateList.Visible = false;
                    //dvBtnBack.Visible = true;
                }
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                dvBtnUpdateList.Visible = false;
                //dvBtnBack.Visible = true;
            }
        }
        else
        {
            detailMessageContent.InnerHtml = "";
        }
        
         //if (Page.Master.FindControl("MenuTreeView") != null)
         //{
         //    Page.Master.FindControl("MenuTreeView").Visible = false;
         //}
    }

    private void ReSetControlVal()
    {
        wctCity.CTLDISPLAY = "0";
        wctHotel.CTLDISPLAY = "0";
        wctHotelGroup.CTLDISPLAY = "0";
        wctUserGroup.CTLDISPLAY = "1";
        chkAllUserGroup.Attributes.Add("checked", "checked");
    }

    private void BindDropDownList()
    {
        DataSet dsOnlineStatus = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
        if (dsOnlineStatus.Tables.Count > 0)
        {
            dsOnlineStatus.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
            dsOnlineStatus.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";

            ddpStatusList.DataTextField = "ONLINEDIS";
            ddpStatusList.DataValueField = "ONLINESTATUS";
            ddpStatusList.DataSource = dsOnlineStatus;
            ddpStatusList.DataBind();
        }

        DataSet dsPriorityType = CommonBP.GetConfigList(GetLocalResourceObject("PriorityType").ToString());
        if (dsPriorityType.Tables.Count > 0)
        {
            dsPriorityType.Tables[0].Columns["Key"].ColumnName = "PRIORITYKEY";
            dsPriorityType.Tables[0].Columns["Value"].ColumnName = "PRIORITYVALUE";

            ddpPriorityList.DataTextField = "PRIORITYVALUE";
            ddpPriorityList.DataValueField = "PRIORITYKEY";
            ddpPriorityList.DataSource = dsPriorityType;
            ddpPriorityList.DataBind();
        }

        DataSet dsPriceType = CommonBP.GetConfigList(GetLocalResourceObject("PriceType").ToString());
        if (dsPriceType.Tables.Count > 0)
        {
            dsPriceType.Tables[0].Columns["Key"].ColumnName = "PRICETYPEKEY";
            dsPriceType.Tables[0].Columns["Value"].ColumnName = "PRICETYPEVALUE";

            ddpPriceType.DataTextField = "PRICETYPEVALUE";
            ddpPriceType.DataValueField = "PRICETYPEKEY";
            ddpPriceType.DataSource = dsPriceType;
            ddpPriceType.DataBind();
        }
        //_promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;

        //DataSet dsResult = PromotionBP.GetCityList(_promotionEntity).QueryResult;
        //ddpCityList.DataTextField = "CITYNM";
        //ddpCityList.DataValueField = "CITYID";
        //ddpCityList.DataSource = dsResult;
        //ddpCityList.DataBind();

        //DataSet dsHotelResult = PromotionBP.GetHotelList(_promotionEntity).QueryResult;
        //ddpHotelList.DataTextField = "HOTELNM";
        //ddpHotelList.DataValueField = "HOTELID";
        //ddpHotelList.DataSource = dsHotelResult;
        //ddpHotelList.DataBind();

        //DataSet dsHotelGroupResult = PromotionBP.GetHotelGroupList(_promotionEntity).QueryResult;
        //ddpHotelGroupList.DataTextField = "GROUPNM";
        //ddpHotelGroupList.DataValueField = "GROUPCODE";
        //ddpHotelGroupList.DataSource = dsHotelGroupResult;
        //ddpHotelGroupList.DataBind();

        //DataSet dsUserGroupResult = PromotionBP.GetUserGroupList(_promotionEntity).QueryResult;
        //ddpUserGroupList.DataTextField = "USERGROUPNM";
        //ddpUserGroupList.DataValueField = "USERGROUPID";
        //ddpUserGroupList.DataSource = dsUserGroupResult;
        //ddpUserGroupList.DataBind();

    }

    private void BindContentDetail()
    {
        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();

        promotionEntity.ID = hiddenId.Value;
        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        DataSet dsMainResult = PromotionBP.MainSelect(_promotionEntity).QueryResult;

        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            SetMainControlValue(dsMainResult.Tables[0]);
            DataSet dsDetailResult = PromotionBP.DetailSelect(_promotionEntity).QueryResult;

            if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 0)
            {
                SetDetailControlValue(dsMainResult.Tables[0].Rows[0]["PROTYPE"].ToString(), dsDetailResult.Tables[0].Rows[0]);
            }
        }
        else
        {
            ReSetControlVal();
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            dvBtnUpdateList.Visible = false;
            //dvBtnBack.Visible = true;
        }
    }

    private void SetMainControlValue(DataTable dtResult)
    {
        txtPromotionTitle.Value = dtResult.Rows[0]["PROTITLE"].ToString();
        dpKeepStart.Value = dtResult.Rows[0]["STARTDATE"].ToString();
        dpKeepEnd.Value = dtResult.Rows[0]["ENDDATE"].ToString(); 
        ddpStatusList.SelectedValue = dtResult.Rows[0]["STATUS"].ToString(); 
                //PROTYPE
        ddpPriorityList.SelectedValue = dtResult.Rows[0]["PRIORITY"].ToString(); 
               //     IMAGEID
        txtPromDescZh.Text = dtResult.Rows[0]["CONTENT"].ToString();

        ddpPriceType.SelectedValue = dtResult.Rows[0]["RATECODE"].ToString();
        txtImgFilePath.Value = dtResult.Rows[0]["IMAGEPATH"].ToString();
        hidImageID.Value = dtResult.Rows[0]["IMAGEID"].ToString();

        ddpPromotionType.SelectedValue = dtResult.Rows[0]["PRO_METHOD_ID"].ToString(); 
    }

    private void SetDetailControlValue(string ProType, DataRow drResult)
    {
        string PromotionCommonKey = String.IsNullOrEmpty(ConfigurationManager.AppSettings["PromotionCommonKey"]) ? "000000" : ConfigurationManager.AppSettings["PromotionCommonKey"].ToString();
        switch (ProType)
        {
            case "0":
                hidProType.Value = "0";
                //hidCommonType.Value = "0";
                txtLinkUrl.Text = drResult["LINKURL"].ToString();
                break;
            case "1":
                hidProType.Value = "1";
                //hidCommonType.Value = "1";
                if (PromotionCommonKey.Equals(drResult["CITY_ID"].ToString()))
                {
                    hidChkCommonType.Value = "1";
                }
                else
                {
                    hidProDetail.Value = drResult["cityid"].ToString();
                }
                break;
            case "2":
                hidProType.Value = "2";
                //hidCommonType.Value = "2";
                if (PromotionCommonKey.Equals(drResult["HOTELGROUP_ID"].ToString()))
                {
                    hidChkCommonType.Value = "1";
                }
                else
                {
                    hidProDetail.Value = drResult["hotelgroupid"].ToString();
                }
                break;
            case "3":
                hidProType.Value = "3";
                //hidCommonType.Value = "3";
                if (PromotionCommonKey.Equals(drResult["HOTEL_ID"].ToString()))
                {
                    hidChkCommonType.Value = "1";
                }
                else
                {
                    hidProDetail.Value = drResult["hotelid"].ToString();
                }
                break;
            case "4":
                //hotelid banding
                hidHotelID.Value = drResult["hotelid"].ToString().Split(',')[0].ToString();
                hidProType.Value = "4";
                //hidCommonType.Value = "4";
                if (PromotionCommonKey.Equals(drResult["HOTEL_ID"].ToString()) && PromotionCommonKey.Equals(drResult["ROOM_ID"].ToString()))
                {
                    hidChkCommonType.Value = "1";
                }
                else
                {
                    hidProDetail.Value = drResult["roomid"].ToString();
                    SetChkListVal(hidHotelID.Value, hidProDetail.Value);
                }
                break;
            default: ; break;
        }

        hidProDetailUserGroup.Value = String.IsNullOrEmpty(drResult["USERGROUPID"].ToString()) ? "" : drResult["USERGROUPID"].ToString();

        if (String.IsNullOrEmpty(hidProDetailUserGroup.Value))
        {
            wctUserGroup.CTLDISPLAY = "1";
        }

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "InitlizeData()", true);
    }

    private void SetChkListVal(string HotelID, string chkRoomList)
    {
        if (String.IsNullOrEmpty(HotelID))
        {
            return;
        }

        HotelID = HotelID.Substring((HotelID.IndexOf('[') + 1), (HotelID.IndexOf(']') - 1));

        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();

        promotionEntity.HotelID = HotelID;
        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        DataSet dsResult = PromotionBP.GetHotelRoomList(_promotionEntity);

        chkHotelRoomList.DataTextField = "HOTELROOMNM";
        chkHotelRoomList.DataValueField = "HOTELROOMCODE";
        chkHotelRoomList.DataSource = dsResult;
        chkHotelRoomList.DataBind();

        ArrayList chkList = new ArrayList();
        foreach (string drRow in chkRoomList.Split(','))
        {
            chkList.Add(drRow.Substring((drRow.IndexOf('[') + 1), (drRow.IndexOf(']') - 1)));
        }

        foreach (ListItem li in chkHotelRoomList.Items)
        {
            if (chkList.Contains(li.Value))
            {
                li.Selected = true;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";

        bool bflag = true;
        string errMsg = GetLocalResourceObject("Error").ToString();
        if (String.IsNullOrEmpty(txtPromotionTitle.Value.Trim()))
        {
            errMsg = errMsg +"<br/>"+ GetLocalResourceObject("Error1").ToString();
            bflag=false; 
        }

        if (StringUtility.Text_Length(txtPromotionTitle.Value.ToString().Trim()) > 45)
        {
            errMsg = errMsg + "<br/>" + GetLocalResourceObject("Error8").ToString();
            bflag = false;
        }

        if (String.IsNullOrEmpty(dpKeepStart.Value.Trim()) || String.IsNullOrEmpty(dpKeepEnd.Value.Trim()))
        {
            errMsg = errMsg + "<br/>" + GetLocalResourceObject("Error2").ToString();
            bflag = false; 
        }

        if (!"0".Equals(hidCommonType.Value) && !"1".Equals(hidChkCommonType.Value) && String.IsNullOrEmpty(hidCommonList.Value.Trim()))
        {
            errMsg = errMsg + "<br/>" + GetLocalResourceObject("Error3").ToString();
            bflag = false; 
        }

        if (!chkAllUserGroup.Checked && String.IsNullOrEmpty(hidUserGroupList.Value.Trim()))
        {
            errMsg = errMsg + "<br/>" + GetLocalResourceObject("Error4").ToString();
            bflag = false; 
        }

        if (String.IsNullOrEmpty(txtPromDescZh.Text.Trim()))
        {
            errMsg = errMsg + "<br/>" + GetLocalResourceObject("Error5").ToString();
            bflag = false; 
        }

        if (StringUtility.Text_Length(txtPromDescZh.Text.Trim()) > 540)
        {
            errMsg = errMsg + "<br/>" + GetLocalResourceObject("Error11").ToString();
            bflag = false;
        }

        if (StringUtility.Text_Length(txtImgFilePath.Value.ToString().Trim()) > 1000)
        {
            errMsg = errMsg + "<br/>" + GetLocalResourceObject("Error10").ToString();
            bflag = false;
        }

        if ("0".Equals(hidCommonType.Value) && StringUtility.Text_Length(txtLinkUrl.Text.Trim()) > 500)
        {
            errMsg = errMsg + "<br/>" + GetLocalResourceObject("Error12").ToString();
            bflag = false;
        }

        if (!bflag)
        {
            detailMessageContent.InnerHtml = errMsg;
            UpdatePanel6.Update();
            return;
        }

        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();

        promotionEntity.ID = hiddenId.Value;
        promotionEntity.Title = txtPromotionTitle.Value.ToString().Trim();
        promotionEntity.Priority = ddpPriorityList.SelectedValue;
        promotionEntity.StartDTime = dpKeepStart.Value;
        promotionEntity.EndDTime = dpKeepEnd.Value;
        promotionEntity.Type = hidCommonType.Value;
        promotionEntity.ChkType = hidChkCommonType.Value;
        promotionEntity.CommonList = hidCommonList.Value;
        promotionEntity.HotelID = (hidHotelID.Value.Length > 0) ? hidHotelID.Value.Substring((hidHotelID.Value.IndexOf('[') + 1), (hidHotelID.Value.IndexOf(']') - 1)) : "";
        promotionEntity.UserGroupList = hidUserGroupList.Value;
        promotionEntity.Content = txtPromDescZh.Text.ToString().Trim();
        //promotionEntity.Imageid = "1";//image_src  本地路径||服务器路径||IMAGEID
        promotionEntity.Imageid = hidImageID.Value.Trim();
        promotionEntity.ImagePath = txtImgFilePath.Value.Trim();
        promotionEntity.Status = ddpStatusList.SelectedValue;
        promotionEntity.RateCode = ddpPriceType.SelectedValue;
        promotionEntity.Promethodid =ddpPromotionType.SelectedValue;
        promotionEntity.LinkUrl = ("0".Equals(hidCommonType.Value)) ? txtLinkUrl.Text.Trim() : "";
        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        //int iResult = PromotionBP.Update(_promotionEntity);
        _promotionEntity = PromotionBP.Update(_promotionEntity);
        int iResult = _promotionEntity.Result;

        _commonEntity.LogMessages = _promotionEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "促销信息详细-保存";
        commonDBEntity.Event_ID = txtPromotionTitle.Value.ToString().Trim();
        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        conTent = string.Format(conTent, txtPromotionTitle.Value.ToString().Trim(), ddpPriorityList.SelectedValue, dpKeepStart.Value, dpKeepEnd.Value, hidCommonType.Value,hidHotelID.Value + hidCommonList.Value, hidUserGroupList.Value, txtPromDescZh.Text.ToString().Trim(), "1");//image_src  本地路径||服务器路径||IMAGEID
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString(); 
            hidCommonList.Value = "";
            hidUserGroupList.Value = "";
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error6").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("Error6").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error9").ToString();
            detailMessageContent.InnerHtml = string.Format(GetLocalResourceObject("Error9").ToString(), _promotionEntity.ErrorMSG);
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error7").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        UpdatePanel6.Update();
    }

    //protected void btnCannel_Click(object sender, EventArgs e)
    //{
    //    BindContentDetail();
    //}

    protected void btnSelectHotel_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(wctHotelRoom.AutoResult))
        {
            return;
        }

        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();

        string strHotel = wctHotelRoom.AutoResult.ToString();
        promotionEntity.HotelID = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1)); ;

        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        DataSet dsResult = PromotionBP.GetHotelRoomList(_promotionEntity);

        chkHotelRoomList.DataTextField = "HOTELROOMNM";
        chkHotelRoomList.DataValueField = "HOTELROOMCODE";
        chkHotelRoomList.DataSource = dsResult;
        chkHotelRoomList.DataBind();
        UpdatePanel5.Update();
    }

    public void BindPromotionType()
    {
        PromotionTypeEntity _promotionTypeEntity = new PromotionTypeEntity();
        _promotionTypeEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionTypeEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionTypeEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionTypeEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _promotionTypeEntity.PromotiontypeDBEntity = new List<PromotionTypeDBEntity>();
        PromotionTypeDBEntity promotionTypeDBEntity = new PromotionTypeDBEntity();

        _promotionTypeEntity.PromotiontypeDBEntity.Add(promotionTypeDBEntity);

        _promotionTypeEntity = PromotionTypeBP.CommonSelect(_promotionTypeEntity);
        if (_promotionTypeEntity.QueryResult.Tables[0].Rows.Count > 0)
        {
            ddpPromotionType.DataTextField = "NAME";
            ddpPromotionType.DataValueField = "ID";
            ddpPromotionType.DataSource = _promotionTypeEntity.QueryResult;
            ddpPromotionType.DataBind();
        }
    }
}