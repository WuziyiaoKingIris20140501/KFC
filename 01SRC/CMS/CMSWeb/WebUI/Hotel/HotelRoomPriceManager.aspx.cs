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
using HotelVp.CMS.Domain.Entity;

public partial class HotelRoomPriceManager : BasePage
{
    CommonEntity _commonEntity = new CommonEntity();
    //private String folder;
    //private String url;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList();
            ReSetControlVal();
        }
    }

    private void SetDateTimePacker()
    {
        string strToday = string.Empty;
        string strYesToday = string.Empty;
        int hour = DateTime.Now.Hour;
        if ((0 <= hour) && (hour <= 4))
        {
            strToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-1));
            strYesToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-2));
        }
        else
        {
            strToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            strYesToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-1));
        }

        dpKeepStart.Value = strToday;
        dpKeepEnd.Value = strToday;
        txtYestoday.Text = strYesToday;
    }

    private void ReSetControlVal()
    {
        messageContent.InnerHtml = "";
        SetDateTimePacker();
        foreach (ListItem li in chkWeekList.Items)
        {
            li.Selected = true;
        }

        ddpRoomStatus.SelectedValue = "true";
        ddpBreakfastNum.SelectedIndex = 0;
        ddpIsNetwork.SelectedIndex = 0;
        ddpOffsetunit.SelectedIndex = 0;
        ddpPriceType.SelectedIndex = 0;
        ddpIsReserve.SelectedIndex = 0;

        dvlm2.Style.Add("display","none");
        dvlm.Style.Add("display", "");

        txtRoomCount.Text = "";
        txtOnePrice.Text = "";
        txtTwoPrice.Text = "";
        txtThreePrice.Text = "";
        txtFourPrice.Text = "";
        txtBedPrice.Text = "";
        txtBreakPrice.Text = "";
        txtOffsetval.Text = "";
        hidHotelID.Value = "";
        wctHotel.AutoResult = "";

        if (chkHotelRoomList.Items.Count > 0)
        {
            for (int i = chkHotelRoomList.Items.Count - 1; i >= 0; i--)
            {
                chkHotelRoomList.Items.RemoveAt(i);
            }
        }
        UpdatePanel1.Update();
        UpdatePanel7.Update();
        UpdatePanel3.Update();
        UpdatePanel4.Update();
        //UpdatePanel9.Update();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent()", true);
    }

    private void BindDropDownList()
    {
        DataSet dsWeek = CommonBP.GetConfigList(GetLocalResourceObject("WeekType").ToString());
        if (dsWeek.Tables.Count > 0)
        {
            dsWeek.Tables[0].Columns["Key"].ColumnName = "WeekKEY";
            dsWeek.Tables[0].Columns["Value"].ColumnName = "WeekVALUE";

            chkWeekList.DataTextField = "WeekVALUE";
            chkWeekList.DataValueField = "WeekKEY";
            chkWeekList.DataSource = dsWeek;
            chkWeekList.DataBind();
        }

        DataSet dsRoomStatus = CommonBP.GetConfigList(GetLocalResourceObject("RoomStatus").ToString());
        if (dsRoomStatus.Tables.Count > 0)
        {
            dsRoomStatus.Tables[0].Columns["Key"].ColumnName = "RoomStatusKEY";
            dsRoomStatus.Tables[0].Columns["Value"].ColumnName = "RoomStatusVALUE";

            ddpRoomStatus.DataTextField = "RoomStatusVALUE";
            ddpRoomStatus.DataValueField = "RoomStatusKEY";
            ddpRoomStatus.DataSource = dsRoomStatus;
            ddpRoomStatus.DataBind();

            ddpRoomStatus.SelectedValue = "true";
        }

        DataSet dsBreakFastNum = CommonBP.GetConfigList(GetLocalResourceObject("BreakFastNum").ToString());
        if (dsBreakFastNum.Tables.Count > 0)
        {
            dsBreakFastNum.Tables[0].Columns["Key"].ColumnName = "BreakFastNumKEY";
            dsBreakFastNum.Tables[0].Columns["Value"].ColumnName = "BreakFastNumVALUE";

            ddpBreakfastNum.DataTextField = "BreakFastNumVALUE";
            ddpBreakfastNum.DataValueField = "BreakFastNumKEY";
            ddpBreakfastNum.DataSource = dsBreakFastNum;
            ddpBreakfastNum.DataBind();
        }

        DataSet dsIsNetwork = CommonBP.GetConfigList(GetLocalResourceObject("IsNetwork").ToString());
        if (dsIsNetwork.Tables.Count > 0)
        {
            dsIsNetwork.Tables[0].Columns["Key"].ColumnName = "IsNetworkKEY";
            dsIsNetwork.Tables[0].Columns["Value"].ColumnName = "IsNetworkVALUE";

            ddpIsNetwork.DataTextField = "IsNetworkVALUE";
            ddpIsNetwork.DataValueField = "IsNetworkKEY";
            ddpIsNetwork.DataSource = dsIsNetwork;
            ddpIsNetwork.DataBind();
        }

        DataSet dsOffSetunit = CommonBP.GetConfigList(GetLocalResourceObject("OffSetunit").ToString());
        if (dsOffSetunit.Tables.Count > 0)
        {
            dsOffSetunit.Tables[0].Columns["Key"].ColumnName = "OffSetunitKEY";
            dsOffSetunit.Tables[0].Columns["Value"].ColumnName = "OffSetunitVALUE";

            ddpOffsetunit.DataTextField = "OffSetunitVALUE";
            ddpOffsetunit.DataValueField = "OffSetunitKEY";
            ddpOffsetunit.DataSource = dsOffSetunit;
            ddpOffsetunit.DataBind();
        }

        DataSet dsPriceType = CommonBP.GetConfigList(GetLocalResourceObject("PriceType").ToString());
        if (dsPriceType.Tables.Count > 0)
        {
            for (int i = 0; i < dsPriceType.Tables[0].Rows.Count; i++)
            {
                if (String.IsNullOrEmpty(dsPriceType.Tables[0].Rows[i]["Key"].ToString()))
                {
                    dsPriceType.Tables[0].Rows.Remove(dsPriceType.Tables[0].Rows[i]);
                }
            }

            dsPriceType.Tables[0].Columns["Key"].ColumnName = "PRICETYPEKEY";
            dsPriceType.Tables[0].Columns["Value"].ColumnName = "PRICETYPEVALUE";

            ddpPriceType.DataTextField = "PRICETYPEVALUE";
            ddpPriceType.DataValueField = "PRICETYPEKEY";
            ddpPriceType.DataSource = dsPriceType;
            ddpPriceType.DataBind();
        }

        DataSet dsIsReserve = CommonBP.GetConfigList(GetLocalResourceObject("IsReserve").ToString());
        if (dsIsReserve.Tables.Count > 0)
        {
            dsIsReserve.Tables[0].Columns["Key"].ColumnName = "IsReserveKEY";
            dsIsReserve.Tables[0].Columns["Value"].ColumnName = "IsReserveVALUE";

            ddpIsReserve.DataTextField = "IsReserveVALUE";
            ddpIsReserve.DataValueField = "IsReserveKEY";
            ddpIsReserve.DataSource = dsIsReserve;
            ddpIsReserve.DataBind();

            ddpIsReserve.SelectedIndex = 0;
        }
    }

    protected void btnRest_Click(object sender, EventArgs e)
    {
        //SetDateTimePacker();
        //UpdatePanel4.Update();
        //messageContent.InnerHtml = "";
        ReSetControlVal();
        //UpdatePanel6.Update();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(hidHotelID.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (String.IsNullOrEmpty(dpKeepStart.Value.Trim()) || String.IsNullOrEmpty(dpKeepEnd.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (String.IsNullOrEmpty(hidWeekList.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error11").ToString();
            //UpdatePanel6.Update();
            return;
        }

        
        if (String.IsNullOrEmpty(hidCommonList.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            //UpdatePanel6.Update();
            return;
        }


        if ("true".Equals(ddpRoomStatus.SelectedValue.Trim().ToLower()) && String.IsNullOrEmpty(txtTwoPrice.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error12").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (!IsValidNumber(txtOnePrice.Text.Trim()) || !IsValidNumber(txtThreePrice.Text.Trim()) || !IsValidNumber(txtFourPrice.Text.Trim()) || !IsValidNumber(txtBedPrice.Text.Trim()) || !IsValidNumber(txtBreakPrice.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error13").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (!IsValidTwoPrice(txtTwoPrice.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error15").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (!ChkNumber(txtOffsetval.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error14").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if ("true".Equals(ddpRoomStatus.SelectedValue.Trim().ToLower()) && String.IsNullOrEmpty(txtRoomCount.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (!ChkNumber(txtRoomCount.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
            //UpdatePanel6.Update();
            return;
        }

        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = hidHotelID.Value.ToString().Trim();
        appcontentDBEntity.PriceCode = ddpPriceType.SelectedValue.Trim();
        appcontentDBEntity.RoomList = hidCommonList.Value.ToString().Trim();
        appcontentDBEntity.StartDTime = dpKeepStart.Value.ToString().Trim();
        appcontentDBEntity.EndDTime = dpKeepEnd.Value.ToString().Trim();
        appcontentDBEntity.WeekList = hidWeekList.Value.Trim(',');

        if ("lmbar".Equals(ddpPriceType.SelectedValue.Trim().ToLower()))
        {
            appcontentDBEntity.Note1 = lbNote1.Text.Trim().Substring(1, lbNote1.Text.Trim().IndexOf('】') -1);
            appcontentDBEntity.Note2 = lbNote11.Text.Trim().Substring(1, lbNote11.Text.Trim().IndexOf('】') -1);
        }
        else
        {
            appcontentDBEntity.Note1 = lbNote2.Text.Trim().Substring(1, lbNote2.Text.Trim().IndexOf('】') - 1);
            appcontentDBEntity.Note2 = lbNote22.Text.Trim().Substring(1, lbNote22.Text.Trim().IndexOf('】') - 1 );
        }

        appcontentDBEntity.OnePrice = txtOnePrice.Text.Trim();
        appcontentDBEntity.TwoPrice = txtTwoPrice.Text.Trim();
        appcontentDBEntity.ThreePrice = txtThreePrice.Text.Trim();
        appcontentDBEntity.FourPrice = txtFourPrice.Text.Trim();
        appcontentDBEntity.BedPrice = txtBedPrice.Text.Trim();
        appcontentDBEntity.BreakfastNum = ddpBreakfastNum.SelectedValue.Trim();
        appcontentDBEntity.BreakPrice = txtBreakPrice.Text.Trim();
        appcontentDBEntity.IsNetwork = ddpIsNetwork.SelectedValue.Trim();
        appcontentDBEntity.Offsetval = txtOffsetval.Text.Trim();
        appcontentDBEntity.Offsetunit = ddpOffsetunit.SelectedValue.Trim();
        appcontentDBEntity.RoomStatus = ddpRoomStatus.SelectedValue.Trim();
        appcontentDBEntity.RoomCount = txtRoomCount.Text.Trim();
        appcontentDBEntity.IsReserve = ddpIsReserve.SelectedValue.Trim();

        appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        _appcontentEntity = APPContentBP.ApplyUnFullRoom(_appcontentEntity);
        int iResult = _appcontentEntity.Result;
        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店价格计划维护-保存";
        commonDBEntity.Event_ID = hidHotelID.Value.ToString().Trim();
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        
        
        string[] strList = hidCommonList.Value.Split(',');
        string msgCommon = string.Empty;

        foreach (string strRoom in strList)
        {
            foreach (ListItem lt in chkHotelRoomList.Items)
            {
                if (!String.IsNullOrEmpty(strRoom) && lt.Value.Equals(strRoom))
                {
                    msgCommon = msgCommon + lt.Text + "[" + strRoom + "]" + ",";
                }
            }
        }

        msgCommon = (msgCommon.Length > 0) ? msgCommon.Substring(0, msgCommon.Length - 1) : msgCommon;
        conTent = string.Format(conTent, hidHotelID.Value.ToString().Trim(), msgCommon, dpKeepStart.Value, dpKeepEnd.Value, ddpPriceType.SelectedValue, txtRoomCount.Text.Trim(), ddpRoomStatus.SelectedValue.Trim(), hidWeekList.Value.Trim(), txtOnePrice.Text.Trim(), txtTwoPrice.Text.Trim(), txtThreePrice.Text.Trim(), txtFourPrice.Text.Trim(), txtBedPrice.Text.Trim(), ddpBreakfastNum.SelectedValue.Trim(), txtBreakPrice.Text.Trim(), ddpIsNetwork.SelectedValue.Trim(), txtOffsetval.Text.Trim(), ddpOffsetunit.SelectedValue.Trim(), ddpIsReserve.SelectedValue.Trim());
        commonDBEntity.Event_Content = conTent;
        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error8").ToString(); ;
            messageContent.InnerHtml = GetLocalResourceObject("Error8").ToString(); ;
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        //UpdatePanel6.Update();
    }

    private bool IsValidNumber(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return true;
        }

        try
        {
            decimal.Parse(param);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool IsValidTwoPrice(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return true;
        }

        try
        {
            if (decimal.Parse(param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private bool ChkNumber(string param)
    {
        bool bReturn = true;

        if (String.IsNullOrEmpty(param))
        {
            return true;
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

    private bool IsVali(string str)
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

    protected void btnSelectHotel_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(wctHotel.AutoResult) || String.IsNullOrEmpty(hidHotelID.Value))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (!wctHotel.AutoResult.Equals(hidHotelID.Value))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error10").ToString();
            //UpdatePanel6.Update();
            return;
        }

        PromotionEntity _promotionEntity = new PromotionEntity();
        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();

        string strHotel = wctHotel.AutoResult.ToString();
        promotionEntity.HotelID = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1)); ;

        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        DataSet dsResult = PromotionBP.GetHotelRoomList(_promotionEntity);

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            chkHotelRoomList.DataTextField = "HOTELROOMNM";
            chkHotelRoomList.DataValueField = "HOTELROOMCODE";
            chkHotelRoomList.DataSource = dsResult;
            chkHotelRoomList.DataBind();
            //UpdatePanel2.Update();
        }
        else
        {
            chkHotelRoomList.DataSource = dsResult;
            chkHotelRoomList.DataBind();
            //UpdatePanel2.Update();

            messageContent.InnerHtml = GetLocalResourceObject("Error9").ToString();
        }

        //UpdatePanel6.Update();
    }
    protected void ddpPriceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddpPriceType.SelectedValue == "LMBAR")
        {
            dvlm2.Style.Add("display","none");
            dvlm.Style.Add("display", "");
        }
        else
        {
            dvlm2.Style.Add("display","");
            dvlm.Style.Add("display", "none");
        }
        UpdatePanel4.Update();
    }
}