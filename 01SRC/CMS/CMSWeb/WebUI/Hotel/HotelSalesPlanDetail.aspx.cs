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

public partial class HotelSalesPlanDetail : BasePage
{
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                hidPlanID.Value = Request.QueryString["ID"].ToString();
                BindDropDownList();
                ReSetControlVal();
            }
            else
            {
                messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                btnSave.Visible = false;
            }
        }
    }

    private void ReSetControlVal()
    {
        messageContent.InnerHtml = "";

        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.PlanID = hidPlanID.Value.ToString().Trim();

        appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        DataSet dsResult = APPContentBP.ReviewSalesPlanDetail(_appcontentEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            //lbKeepStart.Text = dsResult.Tables[0].Rows[0]["StartDtime"].ToString().Trim();
            //lbKeepEnd.Text = dsResult.Tables[0].Rows[0]["EndDtime"].ToString().Trim();
            //lbchkWeekList.Text = SetWeekListNm(dsResult.Tables[0].Rows[0]["Week_List"].ToString().Trim());
            lbHotel.Text = SetHotelNM(dsResult.Tables[0].Rows[0]["HOTEL_ID"].ToString().Trim());
            hidHotelID.Value = dsResult.Tables[0].Rows[0]["HOTEL_ID"].ToString().Trim();
            lbRateCode.Text = dsResult.Tables[0].Rows[0]["RATE_CODE"].ToString().Trim();
            hidPriceType.Value = dsResult.Tables[0].Rows[0]["RATE_CODE"].ToString().Trim();
            lbHotelRoomList.Text = dsResult.Tables[0].Rows[0]["ROOM"].ToString().Trim();
            hidHotelRoomList.Value = dsResult.Tables[0].Rows[0]["ROOMCODE"].ToString().Trim();
            //lbRoomStatus.Text = dsResult.Tables[0].Rows[0]["STATUSDIS"].ToString().Trim();
            //lbRoomCount.Text = dsResult.Tables[0].Rows[0]["ROOM_NUM"].ToString().Trim();
            //lbIsReserve.Text = dsResult.Tables[0].Rows[0]["ISRESERVE"].ToString().Trim();
            //lbOnePrice.Text = dsResult.Tables[0].Rows[0]["ONE_PRICE"].ToString().Trim();
            //lbTwoPrice.Text = dsResult.Tables[0].Rows[0]["TWO_PRICE"].ToString().Trim();
            //lbThreePrice.Text = dsResult.Tables[0].Rows[0]["THREE_PRICE"].ToString().Trim();
            //lbFourPrice.Text = dsResult.Tables[0].Rows[0]["FOUR_PRICE"].ToString().Trim();
            //lbBedPrice.Text = dsResult.Tables[0].Rows[0]["ATTN_PRICE"].ToString().Trim();
            //lbBreakfastNum.Text = dsResult.Tables[0].Rows[0]["BREAKFAST_NUM"].ToString().Trim();
            //lbBreakPrice.Text = dsResult.Tables[0].Rows[0]["EACH_BREAKFAST_PRICE"].ToString().Trim();
            //lbIsNetwork.Text = dsResult.Tables[0].Rows[0]["ISNETWORK"].ToString().Trim();
            //lbOffsetval.Text = dsResult.Tables[0].Rows[0]["OFFSETVAL"].ToString().Trim();
            //lbOffsetunit.Text = dsResult.Tables[0].Rows[0]["OFFSETUNITDIS"].ToString().Trim();
            lbSaveType.Text = dsResult.Tables[0].Rows[0]["SAVETYPENM"].ToString().Trim();
            hidSaveType.Value = dsResult.Tables[0].Rows[0]["SAVETYPE"].ToString().Trim();
            ddpStatusList.SelectedValue = dsResult.Tables[0].Rows[0]["PLANSTATUS"].ToString().Trim();
            //lbPlanDTime.Text = dsResult.Tables[0].Rows[0]["PLANDTIME"].ToString().Trim();
            //lbPlanTime.Text = dsResult.Tables[0].Rows[0]["PLANTTIME"].ToString().Trim();
            //lbPlanStart.Text = dsResult.Tables[0].Rows[0]["PLANSTART"].ToString().Trim();
            //lbPlanEnd.Text = dsResult.Tables[0].Rows[0]["PLANEND"].ToString().Trim();
            //lbPlanWeek.Text = SetWeekListNm(dsResult.Tables[0].Rows[0]["PLANWEEK"].ToString().Trim());
            lbPlanStatus.Text = dsResult.Tables[0].Rows[0]["STATUSDIS"].ToString().Trim();


            ddpRoomStatus.SelectedValue = dsResult.Tables[0].Rows[0]["PDSTATUS"].ToString().Trim();
            txtRoomCount.Text = dsResult.Tables[0].Rows[0]["ROOM_NUM"].ToString().Trim();
            ddpIsReserve.SelectedValue = dsResult.Tables[0].Rows[0]["ISRESERVE"].ToString().Trim();
            txtOnePrice.Text = dsResult.Tables[0].Rows[0]["ONE_PRICE"].ToString().Trim();
            txtTwoPrice.Text = dsResult.Tables[0].Rows[0]["TWO_PRICE"].ToString().Trim();
            txtThreePrice.Text = dsResult.Tables[0].Rows[0]["THREE_PRICE"].ToString().Trim();
            txtFourPrice.Text = dsResult.Tables[0].Rows[0]["FOUR_PRICE"].ToString().Trim();
            txtBedPrice.Text = dsResult.Tables[0].Rows[0]["ATTN_PRICE"].ToString().Trim();
            txtNetPrice.Text = dsResult.Tables[0].Rows[0]["NET_PRICE"].ToString().Trim();
            ddpBreakfastNum.SelectedValue = dsResult.Tables[0].Rows[0]["BREAKFAST_NUM"].ToString().Trim();
            txtBreakPrice.Text = dsResult.Tables[0].Rows[0]["EACH_BREAKFAST_PRICE"].ToString().Trim();
            ddpIsNetwork.SelectedValue = dsResult.Tables[0].Rows[0]["ISNETWORK"].ToString().Trim();
            txtOffsetval.Text = dsResult.Tables[0].Rows[0]["OFFSETVAL"].ToString().Trim();
            ddpOffsetunit.SelectedValue = dsResult.Tables[0].Rows[0]["PDOFFSETUNIT"].ToString().Trim();

            ddpSup.SelectedIndex = ddpSup.Items.IndexOf(ddpSup.Items.FindByValue(dsResult.Tables[0].Rows[0]["SOURCE"].ToString().Trim()));

            dpPlanDTime.Value = dsResult.Tables[0].Rows[0]["PLANDTIME"].ToString().Trim();
            dpPlanTime.Value = dsResult.Tables[0].Rows[0]["PLANTTIME"].ToString().Trim();
            dpPlanStart.Value = dsResult.Tables[0].Rows[0]["PLANSTART"].ToString().Trim().Replace("/", "-");
            dpPlanEnd.Value = dsResult.Tables[0].Rows[0]["PLANEND"].ToString().Trim().Replace("/", "-");

            string strWeekList = dsResult.Tables[0].Rows[0]["PLANWEEK"].ToString().Trim();
            string[] weekList = strWeekList.Split(',');
            foreach (ListItem li in chkPlanWeek.Items)
            {
                if (weekList.Contains(li.Value))
                {
                    li.Selected = true;
                }
            }

            dpKeepStart.Value = dsResult.Tables[0].Rows[0]["StartDtime"].ToString().Trim().Replace("/", "-");
            dpKeepEnd.Value = dsResult.Tables[0].Rows[0]["EndDtime"].ToString().Trim().Replace("/", "-");

            GetEffHourStyleVale(dsResult.Tables[0].Rows[0]["EFFECT_HOUR"].ToString().Trim());
            //ddpEffHour.SelectedValue = dsResult.Tables[0].Rows[0]["EFFECT_HOUR"].ToString().Trim();

            string strhWeekList = dsResult.Tables[0].Rows[0]["Week_List"].ToString().Trim();
            string[] hWeekList = strhWeekList.Split(',');
            foreach (ListItem hli in chkWeekList.Items)
            {
                if (hWeekList.Contains(hli.Value))
                {
                    hli.Selected = true;
                }
            }

            //if ("lmbar".Equals(lbRateCode.Text.ToLower()))
            //{
            //    dvlm2.Style.Add("display", "none");
            //    dvlm.Style.Add("display", "");
            //}
            //else
            //{
            //    dvlm2.Style.Add("display", "");
            //    dvlm.Style.Add("display", "none");
            //}

            //lbGuaid.Text = GetSysConfigurationVale("guaid", dsResult.Tables[0].Rows[0]["GUAID"].ToString().Trim());
            //lbCxlid.Text = GetSysConfigurationVale("cxlid", dsResult.Tables[0].Rows[0]["CXLID"].ToString().Trim());

            ddpGuaid.SelectedValue = dsResult.Tables[0].Rows[0]["GUAID"].ToString().Trim();
            ddpCxlid.SelectedValue = dsResult.Tables[0].Rows[0]["CXLID"].ToString().Trim();

            if ("0".Equals(dsResult.Tables[0].Rows[0]["SAVETYPE"].ToString().Trim()))
            {
                dvDTime.Style.Add("display", "none");
                dvEachFor.Style.Add("display", "none");
                dvSaveStyle.Style.Add("display", "none");
            }
            else if ("1".Equals(dsResult.Tables[0].Rows[0]["SAVETYPE"].ToString().Trim()))
            {
                dvDTime.Style.Add("display", "");
                dvEachFor.Style.Add("display", "none");
            }
            else if ("2".Equals(dsResult.Tables[0].Rows[0]["SAVETYPE"].ToString().Trim()))
            {
                dvDTime.Style.Add("display", "none");
                dvEachFor.Style.Add("display", "");
            }

            if ("2".Equals(dsResult.Tables[0].Rows[0]["PLANSTATUS"].ToString().Trim()))
            {
                dvPlanStatus.Style.Add("display", "");
                ddpStatusList.Visible = false;
                btnSave.Visible = false;
            }
            else
            {
                dvPlanStatus.Style.Add("display", "none");
                ddpStatusList.Visible = true;
            }

            if ("0".Equals(hidSaveType.Value))
            {
                dvSaveStyle.Style.Add("display", "none");
            }
            else if ("1".Equals(hidSaveType.Value))
            {
                if (DateTime.Now > DateTime.Parse(dsResult.Tables[0].Rows[0]["PLANDTIME"].ToString().Trim()).AddMinutes(-15))
                {
                    dvSaveStyle.Style.Add("display", "none");
                }
                else
                {
                    dvSaveStyle.Style.Add("display", "");
                }
            }
            else if ("2".Equals(hidSaveType.Value))
            {
                dvSaveStyle.Style.Add("display", "");
            }
            else
            {
                dvSaveStyle.Style.Add("display", "none");
            }
        }
        else
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            btnSave.Visible = false;
        }

        DataSet dsHistory = APPContentBP.ReviewSalesPlanDetailHistory(_appcontentEntity).QueryResult;
        gridViewCSServiceList.DataSource = dsHistory.Tables[0].DefaultView;
        gridViewCSServiceList.DataKeyNames = new string[] { "SAVETYPENM" };//主键
        gridViewCSServiceList.DataBind();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent()", true);
    }

    private void GetEffHourStyleVale(string effHourVal)
    {
        hidEffHour.Value = effHourVal;
        if (ddpEffHour.Items.IndexOf(ddpEffHour.Items.FindByValue(effHourVal)) >= 0)
        {
            ddpEffHour.SelectedIndex = ddpEffHour.Items.IndexOf(ddpEffHour.Items.FindByValue(effHourVal));
        }
        else
        {
            ddpEffHour.SelectedValue = "99";
        }

        ddpEffHour.Enabled = false;

        for (int i = 0; i < effHourVal.Length; i++)
        {
            GetEffHourListVale(i.ToString(), effHourVal.Substring(i, 1));
        }
    }

    private void GetEffHourListVale(string Did, string Dval)
    {
        if ("0".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr0.Style.Add("background-color", "Green");
                dvHr0.Style.Add("color", "White");
            }
            else
            {
                dvHr0.Style.Add("background-color", "White");
                dvHr0.Style.Add("color", "Black");
            }
        }
        else if ("1".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr1.Style.Add("background-color", "Green");
                dvHr1.Style.Add("color", "White");
            }
            else
            {
                dvHr1.Style.Add("background-color", "White");
                dvHr1.Style.Add("color", "Black");
            }
        }
        else if ("2".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr2.Style.Add("background-color", "Green");
                dvHr2.Style.Add("color", "White");
            }
            else
            {
                dvHr2.Style.Add("background-color", "White");
                dvHr2.Style.Add("color", "Black");
            }
        }
        else if ("3".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr3.Style.Add("background-color", "Green");
                dvHr3.Style.Add("color", "White");
            }
            else
            {
                dvHr3.Style.Add("background-color", "White");
                dvHr3.Style.Add("color", "Black");
            }
        }
        else if ("4".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr4.Style.Add("background-color", "Green");
                dvHr4.Style.Add("color", "White");
            }
            else
            {
                dvHr4.Style.Add("background-color", "White");
                dvHr4.Style.Add("color", "Black");
            }
        }
        else if ("5".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr5.Style.Add("background-color", "Green");
                dvHr5.Style.Add("color", "White");
            }
            else
            {
                dvHr5.Style.Add("background-color", "White");
                dvHr5.Style.Add("color", "Black");
            }
        }
        else if ("6".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr6.Style.Add("background-color", "Green");
                dvHr6.Style.Add("color", "White");
            }
            else
            {
                dvHr6.Style.Add("background-color", "White");
                dvHr6.Style.Add("color", "Black");
            }
        }
        else if ("7".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr7.Style.Add("background-color", "Green");
                dvHr7.Style.Add("color", "White");
            }
            else
            {
                dvHr7.Style.Add("background-color", "White");
                dvHr7.Style.Add("color", "Black");
            }
        }
        else if ("8".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr8.Style.Add("background-color", "Green");
                dvHr8.Style.Add("color", "White");
            }
            else
            {
                dvHr8.Style.Add("background-color", "White");
                dvHr8.Style.Add("color", "Black");
            }
        }
        else if ("9".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr9.Style.Add("background-color", "Green");
                dvHr9.Style.Add("color", "White");
            }
            else
            {
                dvHr9.Style.Add("background-color", "White");
                dvHr9.Style.Add("color", "Black");
            }
        }
        else if ("10".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr10.Style.Add("background-color", "Green");
                dvHr10.Style.Add("color", "White");
            }
            else
            {
                dvHr10.Style.Add("background-color", "White");
                dvHr10.Style.Add("color", "Black");
            }
        }
        else if ("11".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr11.Style.Add("background-color", "Green");
                dvHr11.Style.Add("color", "White");
            }
            else
            {
                dvHr11.Style.Add("background-color", "White");
                dvHr11.Style.Add("color", "Black");
            }
        }
        else if ("12".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr12.Style.Add("background-color", "Green");
                dvHr12.Style.Add("color", "White");
            }
            else
            {
                dvHr12.Style.Add("background-color", "White");
                dvHr12.Style.Add("color", "Black");
            }
        }
        else if ("13".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr13.Style.Add("background-color", "Green");
                dvHr13.Style.Add("color", "White");
            }
            else
            {
                dvHr13.Style.Add("background-color", "White");
                dvHr13.Style.Add("color", "Black");
            }
        }
        else if ("14".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr14.Style.Add("background-color", "Green");
                dvHr14.Style.Add("color", "White");
            }
            else
            {
                dvHr14.Style.Add("background-color", "White");
                dvHr14.Style.Add("color", "Black");
            }
        }
        else if ("15".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr15.Style.Add("background-color", "Green");
                dvHr15.Style.Add("color", "White");
            }
            else
            {
                dvHr15.Style.Add("background-color", "White");
                dvHr15.Style.Add("color", "Black");
            }
        }
        else if ("16".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr16.Style.Add("background-color", "Green");
                dvHr16.Style.Add("color", "White");
            }
            else
            {
                dvHr16.Style.Add("background-color", "White");
                dvHr16.Style.Add("color", "Black");
            }
        }
        else if ("17".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr17.Style.Add("background-color", "Green");
                dvHr17.Style.Add("color", "White");
            }
            else
            {
                dvHr17.Style.Add("background-color", "White");
                dvHr17.Style.Add("color", "Black");
            }
        }
        else if ("18".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr18.Style.Add("background-color", "Green");
                dvHr18.Style.Add("color", "White");
            }
            else
            {
                dvHr18.Style.Add("background-color", "White");
                dvHr18.Style.Add("color", "Black");
            }
        }
        else if ("19".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr19.Style.Add("background-color", "Green");
                dvHr19.Style.Add("color", "White");
            }
            else
            {
                dvHr19.Style.Add("background-color", "White");
                dvHr19.Style.Add("color", "Black");
            }
        }
        else if ("20".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr20.Style.Add("background-color", "Green");
                dvHr20.Style.Add("color", "White");
            }
            else
            {
                dvHr20.Style.Add("background-color", "White");
                dvHr20.Style.Add("color", "Black");
            }
        }
        else if ("21".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr21.Style.Add("background-color", "Green");
                dvHr21.Style.Add("color", "White");
            }
            else
            {
                dvHr21.Style.Add("background-color", "White");
                dvHr21.Style.Add("color", "Black");
            }
        }
        else if ("22".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr22.Style.Add("background-color", "Green");
                dvHr22.Style.Add("color", "White");
            }
            else
            {
                dvHr22.Style.Add("background-color", "White");
                dvHr22.Style.Add("color", "Black");
            }
        }
        else if ("23".Equals(Did))
        {
            if ("1".Equals(Dval))
            {
                dvHr23.Style.Add("background-color", "Green");
                dvHr23.Style.Add("color", "White");
            }
            else
            {
                dvHr23.Style.Add("background-color", "White");
                dvHr23.Style.Add("color", "Black");
            }
        }
    }

    private string GetSysConfigurationVale(string strType, string strKey)
    {
        string strVal = string.Empty;

        DataSet dsResult = CommonBP.GetConfigVale(strType, strKey);
        if (dsResult.Tables.Count > 0)
        {
            strVal = dsResult.Tables[0].Rows[0]["Value"].ToString();
        }

        return strVal;
    }

    private string SetHotelNM(string strHotelID)
    {
        string strResult = "";
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.ChkLMPropHotelList(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            strResult = dsResult.Tables[0].Rows[0]["REVALUE_ALL"].ToString();
        }
        return strResult;
    }

    private string SetWeekListNm(string param)
    {
        string[] weekList = param.Split(',');
        string strResult = "";

        foreach (string strTemp in weekList)
        {
            switch (strTemp)
            {
                case "1":
                    strResult = strResult + "星期天  &nbsp;";
                    break;
                case "2":
                    strResult = strResult + "星期一  &nbsp;";
                    break;
                case "3":
                    strResult = strResult + "星期二  &nbsp;";
                    break;
                case "4":
                    strResult = strResult + "星期三  &nbsp;";
                    break;
                case "5":
                    strResult = strResult + "星期四  &nbsp;";
                    break;
                case "6":
                    strResult = strResult + "星期五  &nbsp;";
                    break;
                case "7":
                    strResult = strResult + "星期六  &nbsp;";
                    break;
                default:
                    break;
            }
        }
        return strResult;
    }

    private void BindDropDownList()
    {
        DataSet dsPstatus = CommonBP.GetConfigList(GetLocalResourceObject("PlanStatus").ToString());
        if (dsPstatus.Tables.Count > 0)
        {
            dsPstatus.Tables[0].Rows.RemoveAt(2);

            dsPstatus.Tables[0].Columns["Key"].ColumnName = "StatusEY";
            dsPstatus.Tables[0].Columns["Value"].ColumnName = "StatusVALUE";

            ddpStatusList.DataTextField = "StatusVALUE";
            ddpStatusList.DataValueField = "StatusEY";
            ddpStatusList.DataSource = dsPstatus;
            ddpStatusList.DataBind();
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

        DataSet dsIsReserve = CommonBP.GetConfigList(GetLocalResourceObject("IsReserve").ToString());
        if (dsIsReserve.Tables.Count > 0)
        {
            dsIsReserve.Tables[0].Columns["Key"].ColumnName = "IsReserveKEY";
            dsIsReserve.Tables[0].Columns["Value"].ColumnName = "IsReserveVALUE";

            ddpIsReserve.DataTextField = "IsReserveVALUE";
            ddpIsReserve.DataValueField = "IsReserveKEY";
            ddpIsReserve.DataSource = dsIsReserve;
            ddpIsReserve.DataBind();
        }

        DataSet dsGuaid = CommonBP.GetConfigList(GetLocalResourceObject("GuaId").ToString());
        if (dsGuaid.Tables.Count > 0)
        {
            dsGuaid.Tables[0].Columns["Key"].ColumnName = "GuaIdKEY";
            dsGuaid.Tables[0].Columns["Value"].ColumnName = "GuaIdVALUE";

            ddpGuaid.DataTextField = "GuaIdVALUE";
            ddpGuaid.DataValueField = "GuaIdKEY";
            ddpGuaid.DataSource = dsGuaid;
            ddpGuaid.DataBind();
        }

        DataSet dsCxlid = CommonBP.GetConfigList(GetLocalResourceObject("CxlId").ToString());
        if (dsCxlid.Tables.Count > 0)
        {
            dsCxlid.Tables[0].Columns["Key"].ColumnName = "CxlIdKEY";
            dsCxlid.Tables[0].Columns["Value"].ColumnName = "CxlIdVALUE";

            ddpCxlid.DataTextField = "CxlIdVALUE";
            ddpCxlid.DataValueField = "CxlIdKEY";
            ddpCxlid.DataSource = dsCxlid;
            ddpCxlid.DataBind();
        }

        DataSet dsWeek = CommonBP.GetConfigList(GetLocalResourceObject("WeekType").ToString());
        if (dsWeek.Tables.Count > 0)
        {
            dsWeek.Tables[0].Columns["Key"].ColumnName = "WeekKEY";
            dsWeek.Tables[0].Columns["Value"].ColumnName = "WeekVALUE";

            chkWeekList.DataTextField = "WeekVALUE";
            chkWeekList.DataValueField = "WeekKEY";
            chkWeekList.DataSource = dsWeek;
            chkWeekList.DataBind();

            chkPlanWeek.DataTextField = "WeekVALUE";
            chkPlanWeek.DataValueField = "WeekKEY";
            chkPlanWeek.DataSource = dsWeek;
            chkPlanWeek.DataBind();
        }

        DataSet dsEffHourTypes = CommonBP.GetConfigList(GetLocalResourceObject("EffHour").ToString());
        if (dsEffHourTypes.Tables.Count > 0)
        {
            dsEffHourTypes.Tables[0].Columns["Key"].ColumnName = "EffHourKEY";
            dsEffHourTypes.Tables[0].Columns["Value"].ColumnName = "EffHourVALUE";

            ddpEffHour.DataTextField = "EffHourVALUE";
            ddpEffHour.DataValueField = "EffHourKEY";
            ddpEffHour.DataSource = dsEffHourTypes;
            ddpEffHour.DataBind();

            ddpEffHour.SelectedValue = "111100000000000000111111";
        }

        DataSet dsSup = GetSupCodeData();// CommonBP.GetConfigList(GetLocalResourceObject("SupHotelType").ToString());
        if (dsSup.Tables.Count > 0)
        {
            dsSup.Tables[0].Columns["Key"].ColumnName = "SUPID";
            dsSup.Tables[0].Columns["Value"].ColumnName = "SUPNM";

            DataRow dr0 = dsSup.Tables[0].NewRow();
            dr0["SUPID"] = "";
            dr0["SUPNM"] = "请选择";
            dsSup.Tables[0].Rows.InsertAt(dr0, 0);

            ddpSup.DataTextField = "SUPNM";
            ddpSup.DataValueField = "SUPID";
            ddpSup.DataSource = dsSup;
            ddpSup.DataBind();

            ddpSup.SelectedIndex = 0;
        }
    }

    private DataSet GetSupCodeData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn SupStatus = new DataColumn("Key");
        DataColumn SupStatusText = new DataColumn("Value");
        dt.Columns.Add(SupStatus);
        dt.Columns.Add(SupStatusText);

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["Key"] = "HVP";
                    dr["Value"] = "天海路";
                    break;
                case "1":
                    dr["Key"] = "HUBS1";
                    dr["Value"] = "HUBS1";
                    break;
                //case "1":
                //    dr["Key"] = "HOMEINNS";
                //    dr["Value"] = "如家";
                //    break;
                //case "2":
                //    dr["Key"] = "MOTEL168";
                //    dr["Value"] = "莫泰168";
                //    break;
            }
            dt.Rows.Add(dr);
        }
        ds.Tables.Add(dt);
        return ds;
    }

    private bool ChkPlanDTimeOut(string PlanID)
    {
        bool bResult = true;
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.PlanID = PlanID.Trim();
        appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.ReviewSalesPlanDetail(_appcontentEntity).QueryResult;
        if (DateTime.Now > DateTime.Parse(dsResult.Tables[0].Rows[0]["PLANDTIME"].ToString().Trim()).AddMinutes(-15))
        {
            bResult = false;
        }
        return bResult;
    }

    private bool ChkPlanDTimeModifyThanNow(string SaveType, string PlanID)
    {
        bool bResult = true;

        if ("1".Equals(SaveType) && (DateTime.Parse(DateTime.Parse(dpPlanDTime.Value).ToShortDateString()) < DateTime.Parse(DateTime.Now.ToShortDateString())))
        {
            bResult = false;
        }
        else if ("2".Equals(SaveType) && (DateTime.Parse(DateTime.Parse(dpKeepStart.Value).ToShortDateString()) < DateTime.Parse(DateTime.Now.ToShortDateString())))
        {
            bResult = false;
        }

        return bResult;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(dpKeepStart.Value.Trim()) || String.IsNullOrEmpty(dpKeepEnd.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if (String.IsNullOrEmpty(hidWeekList.Value.Trim(',')))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error11").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if ("2".Equals(hidSaveType.Value.Trim()) && String.IsNullOrEmpty(hidPlanWeekList.Value.Trim(',')))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error16").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if ("2".Equals(hidSaveType.Value.Trim()) && (String.IsNullOrEmpty(dpKeepStart.Value.Trim()) || String.IsNullOrEmpty(dpKeepEnd.Value.Trim()) || String.IsNullOrEmpty(dpPlanTime.Value.Trim())))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error17").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if ("1".Equals(hidSaveType.Value.Trim()) && (String.IsNullOrEmpty(dpPlanDTime.Value.Trim())))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error18").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }
        if (!IsValidNumber(txtOnePrice.Text.Trim()) || !IsValidNumber(txtThreePrice.Text.Trim()) || !IsValidNumber(txtFourPrice.Text.Trim()) || !IsValidNumber(txtBedPrice.Text.Trim()) || !IsValidNumber(txtBreakPrice.Text.Trim()) || !IsValidNumber(txtNetPrice.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error13").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if (!IsValidTwoPrice(txtTwoPrice.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error15").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if (!ChkNumber(txtOffsetval.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error14").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if ("true".Equals(ddpRoomStatus.SelectedValue.Trim().ToLower()) && String.IsNullOrEmpty(txtRoomCount.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if (!ChkNumber(txtRoomCount.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if ("0".Equals(hidSaveType.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("errorUpdate1").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }
        else if ("1".Equals(hidSaveType.Value.Trim()) && !ChkPlanDTimeOut(hidPlanID.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("errorUpdate2").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if (!ChkPlanDTimeModifyThanNow(hidSaveType.Value.Trim(), hidPlanID.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error19").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        if (!ChkLowPrice(txtTwoPrice.Text.Trim()))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if (String.IsNullOrEmpty(ddpSup.SelectedValue.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error21").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        string effHour = "";
        effHour = GetEffHourVal(ddpEffHour.SelectedValue.Trim());

        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.PlanID = hidPlanID.Value.ToString().Trim();
        appcontentDBEntity.PlanStatus = ddpStatusList.SelectedValue.Trim();
        appcontentDBEntity.HotelID = hidHotelID.Value.Trim();

        appcontentDBEntity.PriceCode = hidPriceType.Value.Trim();
        //appcontentDBEntity.RoomList = hidCommonList.Value.ToString().Trim();
        appcontentDBEntity.RoomCode = hidHotelRoomList.Value.Trim();
        appcontentDBEntity.RoomName = lbHotelRoomList.Text.Trim().Substring(0, lbHotelRoomList.Text.Trim().IndexOf('['));
        appcontentDBEntity.StartDTime = dpKeepStart.Value.ToString().Trim();
        appcontentDBEntity.EndDTime = dpKeepEnd.Value.ToString().Trim();
        appcontentDBEntity.EffHour = effHour;
        appcontentDBEntity.WeekList = hidWeekList.Value.Trim(',');

        appcontentDBEntity.Note1 = ddpGuaid.SelectedValue.Trim();
        appcontentDBEntity.Note2 = ddpCxlid.SelectedValue.Trim();

        appcontentDBEntity.OnePrice = txtOnePrice.Text.Trim();
        appcontentDBEntity.TwoPrice = txtTwoPrice.Text.Trim();
        appcontentDBEntity.ThreePrice = txtThreePrice.Text.Trim();
        appcontentDBEntity.FourPrice = txtFourPrice.Text.Trim();
        appcontentDBEntity.BedPrice = txtBedPrice.Text.Trim();
        appcontentDBEntity.NetPrice = txtNetPrice.Text.Trim();
        appcontentDBEntity.BreakfastNum = ddpBreakfastNum.SelectedValue.Trim();
        appcontentDBEntity.BreakPrice = txtBreakPrice.Text.Trim();
        appcontentDBEntity.IsNetwork = ddpIsNetwork.SelectedValue.Trim();
        appcontentDBEntity.Offsetval = txtOffsetval.Text.Trim();
        appcontentDBEntity.Offsetunit = ddpOffsetunit.SelectedValue.Trim();
        appcontentDBEntity.RoomStatus = ddpRoomStatus.SelectedValue.Trim();
        appcontentDBEntity.RoomCount = txtRoomCount.Text.Trim();
        appcontentDBEntity.IsReserve = ddpIsReserve.SelectedValue.Trim();
        appcontentDBEntity.Supplier = ddpSup.SelectedValue.Trim();

        appcontentDBEntity.SaveType = hidSaveType.Value.Trim();
        appcontentDBEntity.PlanDTime = dpPlanDTime.Value.Trim();
        appcontentDBEntity.PlanTime = dpPlanTime.Value.Trim();
        appcontentDBEntity.PlanStart = dpKeepStart.Value.ToString().Trim(); //dpPlanStart.Value.Trim();
        appcontentDBEntity.PlanEnd = dpKeepEnd.Value.ToString().Trim(); //dpPlanEnd.Value.Trim();
        appcontentDBEntity.PlanWeek = hidWeekList.Value.Trim(','); //hidPlanWeekList.Value.Trim(',');

        appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        _appcontentEntity = APPContentBP.UpdateSalesPlanList(_appcontentEntity);
        int iResult = _appcontentEntity.Result;
        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店销售计划-修改";
        commonDBEntity.Event_ID = hidPlanID.Value.ToString().Trim();
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();

        string msgPlanDTime = "";
        string PlanStart = "";
        string PlanWeekList = "";

        if ("1".Equals(hidSaveType.Value.Trim()))
        {
            msgPlanDTime = dpPlanDTime.Value;
        }
        else if ("2".Equals(hidSaveType.Value.Trim()))
        {
            msgPlanDTime = dpPlanTime.Value;
            PlanStart = dpKeepStart.Value + "-" + dpKeepEnd.Value;
            PlanWeekList = hidWeekList.Value.Trim(',');
        }

        //conTent = string.Format(conTent, hidPlanID.Value.ToString().Trim(), ddpStatusList.SelectedValue.Trim());
        conTent = string.Format(conTent, lbHotel.Text, lbHotelRoomList.Text, dpKeepStart.Value, dpKeepEnd.Value, ddpEffHour.SelectedValue.Trim(), hidPriceType.Value, txtRoomCount.Text.Trim(), ddpRoomStatus.SelectedValue.Trim(), hidWeekList.Value.Trim(','), txtOnePrice.Text.Trim(), txtTwoPrice.Text.Trim(), txtThreePrice.Text.Trim(), txtFourPrice.Text.Trim(), txtBedPrice.Text.Trim(), txtNetPrice.Text.Trim(), ddpBreakfastNum.SelectedValue.Trim(), txtBreakPrice.Text.Trim(), ddpIsNetwork.SelectedValue.Trim(), txtOffsetval.Text.Trim(), ddpOffsetunit.SelectedValue.Trim(), ddpIsReserve.SelectedValue.Trim(), hidSaveType.Value.Trim(), msgPlanDTime, PlanStart, PlanWeekList, hidPlanID.Value, ddpStatusList.SelectedItem.Text, ddpSup.SelectedValue);
        commonDBEntity.Event_Content = conTent;
        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = string.Format(GetLocalResourceObject("UpdateSuccess").ToString(), _appcontentEntity.APPContentDBEntity[0].PlanStart);
            messageContent.InnerHtml = string.Format(GetLocalResourceObject("UpdateSuccess").ToString(), _appcontentEntity.APPContentDBEntity[0].PlanStart);
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error8").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error8").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        DataSet dsHistory = APPContentBP.ReviewSalesPlanDetailHistory(_appcontentEntity).QueryResult;
        gridViewCSServiceList.DataSource = dsHistory.Tables[0].DefaultView;
        gridViewCSServiceList.DataKeyNames = new string[] { "SAVETYPENM" };//主键
        gridViewCSServiceList.DataBind();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    private string GetEffHourVal(string param)
    {
        if (!"99".Equals(param))
        {
            return param;
        }
        return hidEffHour.Value.Trim();
    }

    private bool ChkLowPrice(string param)
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.HotelID = hidHotelID.Value.ToString().Trim();
        appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        DataSet dsResult = APPContentBP.ChkHotelLowLimitPrice(_appcontentEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0 || String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString().Trim()))
        {
            return true;
        }

        decimal ilimit = decimal.Parse(dsResult.Tables[0].Rows[0][0].ToString().Trim());

        if (ilimit < 1)
        {
            return true;
        }

        if (decimal.Parse(param) <= ilimit)
        {
            messageContent.InnerHtml = String.Format(GetLocalResourceObject("Error20").ToString(), ilimit);
            return false;
        }

        return true;
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

    protected void gridViewCSServiceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSServiceList.Rows.Count; i++)
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
}