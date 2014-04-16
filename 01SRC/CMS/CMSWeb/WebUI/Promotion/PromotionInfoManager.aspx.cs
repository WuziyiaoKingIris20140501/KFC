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

public partial class PromotionInfoManager : BasePage
{
    PromotionEntity _promotionEntity = new PromotionEntity();
    CommonEntity _commonEntity = new CommonEntity();
    //private String folder;
    //private String url;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindDropDownList();
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetDataTable();", true);
            //BindUserGroupListGrid();

            //for   (int   i   =   0;   i   <   10;   i++) 
            //    { 
            //            Button   btn   =   new   Button(); 
            //            btn.Text   =   "Button "   +   i.ToString(); 
            //            btn.ID   =   "btn "   +   i.ToString();
            //            btn.OnClientClick = "SetRbtTypeList('"+i.ToString()+"')";
            //            dvUserGroupList.Controls.Add(btn); 
            //    } 

            ReSetControlVal();
            BindPromotionType();
            lbCommonNM.Text = "链接URL：";
        }
        //messageContent.InnerHtml = "";


        //image_src.Value = "4ea02506-7b06-4ebe-ba6e-d68aa4203732.png";
    }

    private void ReSetControlVal()
    {
        //dvUserGroup.FindControl("wctUserGroup"). .Attributes.Add("disabled", "disabled");
        wctCity.CTLDISPLAY = "0";
        wctHotel.CTLDISPLAY = "0";
        wctHotelGroup.CTLDISPLAY = "0";
        wctUserGroup.CTLDISPLAY = "1";
        chkAllUserGroup.Attributes.Add("checked", "checked");


        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetBtnStyleList('1', '1')", true);
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

    ////发放渠道
    private void BindUserGroupListGrid()
    {
        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();


        //DataSet dsResult = PromotionBP.CreateUserSelect(_promotionEntity).QueryResult;

        //gridViewCSCreateUserGroupList.DataSource = dsResult.Tables[0].DefaultView;
        //gridViewCSCreateUserGroupList.DataKeyNames = new string[] { "ID" };//主键
        //gridViewCSCreateUserGroupList.DataBind();        
    }

    private bool checkNum(string param)
    {
        bool bReturn = true;

        if (String.IsNullOrEmpty(param))
        {
            return bReturn;
        }

        try
        {
            return IsVali(param);
        }
        catch 
        {
            bReturn = false;
        }

        return bReturn;
    }

    public bool IsVali(string str)
    {
        bool flog = false;
        string strPatern = @"^([0-9]\d*)$";
        Regex reg = new Regex(strPatern);
        if (reg.IsMatch(str))
        {
            flog = true;
        }
        return flog;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

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
            messageContent.InnerHtml = errMsg;
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
       // promotionEntity.Imageid = "1";//image_src  本地路径||服务器路径||IMAGEID
        promotionEntity.Imageid = txtImgFilePath.Value.Trim();
        promotionEntity.RateCode = ddpPriceType.SelectedValue;
        promotionEntity.Promethodid = ddpPromotionType.SelectedValue;
        promotionEntity.LinkUrl = ("0".Equals(hidCommonType.Value)) ? txtLinkUrl.Text.Trim() : "";
        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        _promotionEntity = PromotionBP.Insert(_promotionEntity);
        int iResult =_promotionEntity.Result;

        _commonEntity.LogMessages = _promotionEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "新建促销信息-添加";
        commonDBEntity.Event_ID = txtPromotionTitle.Value.ToString().Trim();
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtPromotionTitle.Value.ToString().Trim(), ddpPriorityList.SelectedValue, dpKeepStart.Value, dpKeepEnd.Value, hidCommonType.Value,hidHotelID.Value + hidCommonList.Value, hidUserGroupList.Value, txtPromDescZh.Text.ToString().Trim(), "1");//image_src  本地路径||服务器路径||IMAGEID
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString(); 
            hidCommonList.Value = "";
            hidUserGroupList.Value = "";
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error6").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error6").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error9").ToString();
            messageContent.InnerHtml = string.Format(GetLocalResourceObject("Error9").ToString(), _promotionEntity.ErrorMSG);
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error7").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        UpdatePanel6.Update();
    }

    protected void btnCannel_Click(object sender, EventArgs e)
    { 
        
    }

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //ProgressBar1.Value = 50;
        messageContent.InnerHtml = "";

        //if (String.IsNullOrEmpty(txtUserGroupNM.Value.ToString().Trim()) || String.IsNullOrEmpty(txtUserGroupNM.Value.ToString().Trim()))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
        //    //UpdatePanel2.Update();
        //    return;
        //}

        //if (!checkNum(txtSubmitOrderFrom.Value.ToString().Trim()) || !checkNum(txtSubmitOrderTo.Value.ToString().Trim()) || !checkNum(txtCompleteOrderFrom.Value.ToString().Trim()) || !checkNum(txtCompleteOrderTo.Value.ToString().Trim()))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
        //    return;
        //}


        //if ((!String.IsNullOrEmpty(txtSubmitOrderFrom.Value.ToString().Trim()) && !String.IsNullOrEmpty(txtSubmitOrderTo.Value.ToString().Trim())) && (int.Parse(txtSubmitOrderFrom.Value.ToString().Trim()) > int.Parse(txtSubmitOrderTo.Value.ToString().Trim())))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("Error6").ToString();
        //    return;
        //}

        //if ((!String.IsNullOrEmpty(txtCompleteOrderFrom.Value.ToString().Trim()) && !String.IsNullOrEmpty(txtCompleteOrderTo.Value.ToString().Trim())) && (int.Parse(txtCompleteOrderFrom.Value.ToString().Trim()) > int.Parse(txtCompleteOrderTo.Value.ToString().Trim())))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
        //    return;
        //}

        //_promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;

        //_promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        //PromotionDBEntity promotionEntity = new PromotionDBEntity();

        //promotionEntity.UserGroupNM = txtUserGroupNM.Value.ToString().Trim();

        //promotionEntity.RegChannelList = hidRegChannelList.Value.ToString().Trim();
        //promotionEntity.PlatformList = hidPlatformList.Value.ToString().Trim();

        //promotionEntity.RegistStart = dpRegistStart.Value;
        //promotionEntity.RegistEnd = dpRegistEnd.Value;

        //promotionEntity.LoginStart = dpLoginStart.Value;
        //promotionEntity.LoginEnd = dpLoginEnd.Value;

        //promotionEntity.SubmitOrderFrom = txtSubmitOrderFrom.Value.ToString().Trim();
        //promotionEntity.SubmitOrderTo = txtSubmitOrderTo.Value.ToString().Trim();

        //promotionEntity.CompleteOrderFrom = txtCompleteOrderFrom.Value.ToString().Trim();
        //promotionEntity.CompleteOrderTo = txtCompleteOrderTo.Value.ToString().Trim();

        //promotionEntity.LastOrderStart = dpLastOrderStart.Value;
        //promotionEntity.LastOrderEnd = dpLastOrderEnd.Value;

        //promotionEntity.ManualAdd = txtManualAdd.Text.ToString().Trim();

        //_promotionEntity.PromotionDBEntity.Add(promotionEntity);
        //int iResult = PromotionBP.Insert(_promotionEntity);

        //_commonEntity.LogMessages = _promotionEntity.LogMessages;
        //_commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        //CommonDBEntity commonDBEntity = new CommonDBEntity();

        //commonDBEntity.Event_Type = "";
        //commonDBEntity.Event_ID = "";
        //string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        //conTent = string.Format(conTent, txtUserGroupNM.Value, "");
        //commonDBEntity.Event_Content = conTent;

        //if (iResult == 1)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        //}
        //else if (iResult == 2)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        //}
        //else if (iResult == 3)
        //{
        //    string errTent = GetLocalResourceObject("Error5").ToString();
        //    errTent = string.Format(errTent, (_promotionEntity.PromotionDBEntity.Count > 0 ) ? _promotionEntity.PromotionDBEntity[0].ErrManualAdd : "");

        //    commonDBEntity.Event_Result = errTent;
        //    messageContent.InnerHtml = errTent;
        //}
        //else
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
        //}

        //_commonEntity.CommonDBEntity.Add(commonDBEntity);

        //int maxLength = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLengthCount"]) ? 500 : int.Parse(ConfigurationManager.AppSettings["MaxLengthCount"].ToString());
        //if (iResult == 1 || iResult == 3)
        //{
        //    DataSet dsHistory = PromotionBP.GetHistoryInsert(_promotionEntity);

        //    if (dsHistory.Tables.Count > 0 && dsHistory.Tables[0].Rows.Count > 0)
        //    {
        //        int iCount = 0;
        //        string phoneString = "";
        //        foreach (DataRow drRow in dsHistory.Tables[0].Rows)
        //        {
        //            phoneString = phoneString + drRow[0].ToString() + ",";
        //            iCount = iCount + 1;
        //            if (iCount == maxLength)
        //            {
        //                phoneString = phoneString.Substring(0, phoneString.Length - 1);
        //                conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        //                conTent = string.Format(conTent, txtUserGroupNM.Value, phoneString);
        //                _commonEntity.CommonDBEntity[0].Event_Content = conTent;
        //                CommonBP.InsertEventHistory(_commonEntity);
        //                phoneString = "";
        //                iCount = 0;
        //            }

        //        }

        //        if (iCount > 0)
        //        {
        //            phoneString = phoneString.Substring(0, phoneString.Length - 1);
        //            conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        //            conTent = string.Format(conTent, txtUserGroupNM.Value, phoneString);
        //            _commonEntity.CommonDBEntity[0].Event_Content = conTent;
        //            CommonBP.InsertEventHistory(_commonEntity);
        //        }
        //    }
        //    else
        //    {
        //        conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        //        conTent = string.Format(conTent, txtUserGroupNM.Value, "");
        //        _commonEntity.CommonDBEntity[0].Event_Content = conTent;
        //        CommonBP.InsertEventHistory(_commonEntity);
        //    }
        //}
        //else
        //{
        //    //_commonEntity.CommonDBEntity.Add(commonDBEntity);
        //    CommonBP.InsertEventHistory(_commonEntity);
        //}

        //hidRegChannelList.Value = "";
        //hidPlatformList.Value = "";
        //BindUserGroupListGrid();
        //UpdatePanel1.Update();
        //ProgressBar1.Value = 100;
    }

    protected void rbtTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetRbtTypeList("+"yes"+")", true);
        //this.Page.ClientScript.Regist
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

        ddpPromotionType.DataTextField = "NAME";
        ddpPromotionType.DataValueField = "ID";
        ddpPromotionType.DataSource = _promotionTypeEntity.QueryResult.Tables[0];
        ddpPromotionType.DataBind();
    }

}