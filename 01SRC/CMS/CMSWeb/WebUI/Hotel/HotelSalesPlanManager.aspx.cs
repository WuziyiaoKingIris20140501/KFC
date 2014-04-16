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

public partial class HotelSalesPlanManager : BasePage
{
    CommonEntity _commonEntity = new CommonEntity();
    //private String folder;
    //private String url;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type=hotel"))
        {
            BindDropDownList();
            ReSetControlVal();
            if ("LMBAR".Equals(ddpPriceType.SelectedValue))
            {
                ddpGuaid.SelectedValue = "PP";
                ddpCxlid.SelectedValue = "PT100";
            }
            else
            {
                ddpGuaid.SelectedValue = "RH04";
                ddpCxlid.SelectedValue = "NP24";
            }

            if (!String.IsNullOrEmpty(Request.QueryString["eh"]))
            {
                SetDvEffHour99();
            }

            if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() == "AddOrEdit")
            {
                string type = Request.QueryString["type"].ToString();
                string hotel = "[" + Request.QueryString["Hid"].ToString() + "]" + Request.QueryString["hName"].ToString();
                string roomName = Request.QueryString["radiaoName"].ToString();
                string priceCode = Request.QueryString["priceCode"].ToString();

                wctHotel.AutoResult = hotel;
                this.hidHotelID.Value = Request.QueryString["Hid"].ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "setsalesroomKeys", "SetHotel('" + hotel + "');", true);//设置酒店

                ddpPriceType.SelectedValue = priceCode;
                GetRoomList();
                ddpHotelRoomList.SelectedValue = roomName;
            }
        }
    }

    private void SetDvEffHour99()
    {
        ddpEffHour.SelectedValue = "000000000000001111000000";
        ddpSup.SelectedValue = "HVP";
        ddpSaveType.SelectedIndex = 0;

        ddpEffHour.Enabled = false;
        ddpSup.Enabled = false;
        ddpSaveType.Enabled = false;

        dvHr0.Style.Add("background-color", "White");
        dvHr0.Style.Add("color", "Black");
        dvHr1.Style.Add("background-color", "White");
        dvHr1.Style.Add("color", "Black");
        dvHr2.Style.Add("background-color", "White");
        dvHr2.Style.Add("color", "Black");
        dvHr3.Style.Add("background-color", "White");
        dvHr3.Style.Add("color", "Black");
        dvHr4.Style.Add("background-color", "White");
        dvHr4.Style.Add("color", "Black");
        dvHr18.Style.Add("background-color", "White");
        dvHr18.Style.Add("color", "Black");
        dvHr19.Style.Add("background-color", "White");
        dvHr19.Style.Add("color", "Black");
        dvHr20.Style.Add("background-color", "White");
        dvHr20.Style.Add("color", "Black");
        dvHr21.Style.Add("background-color", "White");
        dvHr21.Style.Add("color", "Black");
        dvHr22.Style.Add("background-color", "White");
        dvHr22.Style.Add("color", "Black");
        dvHr23.Style.Add("background-color", "White");
        dvHr23.Style.Add("color", "Black");


        dvHr5.Style.Add("background-color", "White");
        dvHr5.Style.Add("color", "Black");
        dvHr6.Style.Add("background-color", "White");
        dvHr6.Style.Add("color", "Black");
        dvHr7.Style.Add("background-color", "White");
        dvHr7.Style.Add("color", "Black");
        dvHr8.Style.Add("background-color", "White");
        dvHr8.Style.Add("color", "Black");
        dvHr9.Style.Add("background-color", "White");
        dvHr9.Style.Add("color", "Black");
        dvHr10.Style.Add("background-color", "White");
        dvHr10.Style.Add("color", "Black");
        dvHr11.Style.Add("background-color", "White");
        dvHr11.Style.Add("color", "Black");
        dvHr12.Style.Add("background-color", "White");
        dvHr12.Style.Add("color", "Black");
        dvHr13.Style.Add("background-color", "White");
        dvHr13.Style.Add("color", "Black");
        dvHr14.Style.Add("background-color", "Green");
        dvHr14.Style.Add("color", "White");
        dvHr15.Style.Add("background-color", "Green");
        dvHr15.Style.Add("color", "White");
        dvHr16.Style.Add("background-color", "Green");
        dvHr16.Style.Add("color", "White");
        dvHr17.Style.Add("background-color", "Green");
        dvHr17.Style.Add("color", "White");

        dvHr0.Attributes.Add("onclick", "");
        dvHr1.Attributes.Add("onclick", "");
        dvHr2.Attributes.Add("onclick", "");
        dvHr3.Attributes.Add("onclick", "");
        dvHr4.Attributes.Add("onclick", "");
        dvHr5.Attributes.Add("onclick", "");
        dvHr6.Attributes.Add("onclick", "");
        dvHr7.Attributes.Add("onclick", "");
        dvHr8.Attributes.Add("onclick", "");
        dvHr9.Attributes.Add("onclick", "");
        dvHr10.Attributes.Add("onclick", "");
        dvHr11.Attributes.Add("onclick", "");
        dvHr12.Attributes.Add("onclick", "");
        dvHr13.Attributes.Add("onclick", "");
        dvHr14.Attributes.Add("onclick", "");
        dvHr15.Attributes.Add("onclick", "");
        dvHr16.Attributes.Add("onclick", "");
        dvHr17.Attributes.Add("onclick", "");
        dvHr18.Attributes.Add("onclick", "");
        dvHr19.Attributes.Add("onclick", "");
        dvHr20.Attributes.Add("onclick", "");
        dvHr21.Attributes.Add("onclick", "");
        dvHr22.Attributes.Add("onclick", "");
        dvHr23.Attributes.Add("onclick", "");
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
        //ddpPriceType.SelectedIndex = 0;
        ddpPriceType.SelectedValue = "LMBAR2";
        ddpIsReserve.SelectedIndex = 0;
        ddpSup.SelectedIndex = 0;
        ddpEffHour.SelectedValue = "111100000000000000111111";

        dvHr0.Style.Add("background-color", "Green");
        dvHr0.Style.Add("color", "White");
        dvHr1.Style.Add("background-color", "Green");
        dvHr1.Style.Add("color", "White");
        dvHr2.Style.Add("background-color", "Green");
        dvHr2.Style.Add("color", "White");
        dvHr3.Style.Add("background-color", "Green");
        dvHr3.Style.Add("color", "White");
        dvHr18.Style.Add("background-color", "Green");
        dvHr18.Style.Add("color", "White");
        dvHr19.Style.Add("background-color", "Green");
        dvHr19.Style.Add("color", "White");
        dvHr20.Style.Add("background-color", "Green");
        dvHr20.Style.Add("color", "White");
        dvHr21.Style.Add("background-color", "Green");
        dvHr21.Style.Add("color", "White");
        dvHr22.Style.Add("background-color", "Green");
        dvHr22.Style.Add("color", "White");
        dvHr23.Style.Add("background-color", "Green");
        dvHr23.Style.Add("color", "White");

        dvHr4.Style.Add("background-color", "White");
        dvHr4.Style.Add("color", "Black");
        dvHr5.Style.Add("background-color", "White");
        dvHr5.Style.Add("color", "Black");
        dvHr6.Style.Add("background-color", "White");
        dvHr6.Style.Add("color", "Black");
        dvHr7.Style.Add("background-color", "White");
        dvHr7.Style.Add("color", "Black");
        dvHr8.Style.Add("background-color", "White");
        dvHr8.Style.Add("color", "Black");
        dvHr9.Style.Add("background-color", "White");
        dvHr9.Style.Add("color", "Black");
        dvHr10.Style.Add("background-color", "White");
        dvHr10.Style.Add("color", "Black");
        dvHr11.Style.Add("background-color", "White");
        dvHr11.Style.Add("color", "Black");
        dvHr12.Style.Add("background-color", "White");
        dvHr12.Style.Add("color", "Black");
        dvHr13.Style.Add("background-color", "White");
        dvHr13.Style.Add("color", "Black");
        dvHr14.Style.Add("background-color", "White");
        dvHr14.Style.Add("color", "Black");
        dvHr15.Style.Add("background-color", "White");
        dvHr15.Style.Add("color", "Black");
        dvHr16.Style.Add("background-color", "White");
        dvHr16.Style.Add("color", "Black");
        dvHr17.Style.Add("background-color", "White");
        dvHr17.Style.Add("color", "Black");

        dvHr0.Attributes.Add("onclick", "");
        dvHr1.Attributes.Add("onclick", "");
        dvHr2.Attributes.Add("onclick", "");
        dvHr3.Attributes.Add("onclick", "");
        dvHr4.Attributes.Add("onclick", "");
        dvHr5.Attributes.Add("onclick", "");
        dvHr6.Attributes.Add("onclick", "");
        dvHr7.Attributes.Add("onclick", "");
        dvHr8.Attributes.Add("onclick", "");
        dvHr9.Attributes.Add("onclick", "");
        dvHr10.Attributes.Add("onclick", "");
        dvHr11.Attributes.Add("onclick", "");
        dvHr12.Attributes.Add("onclick", "");
        dvHr13.Attributes.Add("onclick", "");
        dvHr14.Attributes.Add("onclick", "");
        dvHr15.Attributes.Add("onclick", "");
        dvHr16.Attributes.Add("onclick", "");
        dvHr17.Attributes.Add("onclick", "");
        dvHr18.Attributes.Add("onclick", "");
        dvHr19.Attributes.Add("onclick", "");
        dvHr20.Attributes.Add("onclick", "");
        dvHr21.Attributes.Add("onclick", "");
        dvHr22.Attributes.Add("onclick", "");
        dvHr23.Attributes.Add("onclick", "");


        //dvlm2.StyleAdd("background-color", "Green");.Add("display","none");
        //dvlm.Style.Add("color", "White");Add("display", "");

        dvDTime.Style.Add("display", "none");
        dvEachFor.Style.Add("display", "none");

        txtRoomCount.Text = "";
        txtOnePrice.Text = "";
        txtTwoPrice.Text = "";
        txtThreePrice.Text = "";
        txtFourPrice.Text = "";
        txtBedPrice.Text = "";
        txtNetPrice.Text = "";
        txtBreakPrice.Text = "";
        txtOffsetval.Text = "";
        if (Request.QueryString["type"] == null)
        {
            hidHotelID.Value = "";
            wctHotel.AutoResult = "";
        }
        //if (chkHotelRoomList.Items.Count > 0)
        //{
        //    for (int i = chkHotelRoomList.Items.Count - 1; i >= 0; i--)
        //    {
        //        chkHotelRoomList.Items.RemoveAt(i);
        //    }
        //}
        //ddpHotelRoomList.SelectedIndex = 0;
        ddpSaveType.SelectedIndex = 0;

        dvDTime.Style.Add("display", "none");
        dvEachFor.Style.Add("display", "none");

        dpPlanDTime.Value = "";
        dpPlanTime.Value = "";
        dpPlanStart.Value = "";
        dpPlanEnd.Value = "";

        foreach (ListItem li in chkPlanWeek.Items)
        {
            li.Selected = true;
        }
        SetDdpEmptyList();


        ddpGuaid.SelectedValue = "PP";
        ddpCxlid.SelectedValue = "PT100";

        dvDTime.Style.Add("display", "none");
        dvEachFor.Style.Add("display", "none");

        UpdatePanel1.Update();
        UpdatePanel7.Update();
        UpdatePanel3.Update();
        UpdatePanel2.Update();
        UpdatePanel10.Update();

        if (!String.IsNullOrEmpty(Request.QueryString["eh"]))
        {
            SetDvEffHour99();
        }

        UpdatePanel8.Update();
        //UpdatePanel4.Update();
        //UpdatePanel9.Update();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetEffHourStyle('" + ddpEffHour.SelectedValue + "')", true);
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

            chkPlanWeek.DataTextField = "WeekVALUE";
            chkPlanWeek.DataValueField = "WeekKEY";
            chkPlanWeek.DataSource = dsWeek;
            chkPlanWeek.DataBind();
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

            ddpPriceType.SelectedValue = "LMBAR2";
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

        DataSet dsSaveType = CommonBP.GetConfigList(GetLocalResourceObject("SaveType").ToString());
        if (dsSaveType.Tables.Count > 0)
        {
            dsSaveType.Tables[0].Columns["Key"].ColumnName = "SaveTypeKEY";
            dsSaveType.Tables[0].Columns["Value"].ColumnName = "SaveTypeVALUE";

            ddpSaveType.DataTextField = "SaveTypeVALUE";
            ddpSaveType.DataValueField = "SaveTypeKEY";
            ddpSaveType.DataSource = dsSaveType;
            ddpSaveType.DataBind();

            ddpSaveType.SelectedIndex = 0;
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

            ddpGuaid.SelectedValue = "PP";
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

            ddpCxlid.SelectedValue = "PT100";
        }

        DataSet dsEffHourTypes = CommonBP.GetConfigListForSort(GetLocalResourceObject("EffHour").ToString());
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

        for (int i = 0; i < 1; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["Key"] = "HVP";
                    dr["Value"] = "天海路";
                    break;
                //case "1":
                //    dr["Key"] = "HUBS1";
                //    dr["Value"] = "HUBS1";
                //    break;
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
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if (!wctHotel.AutoResult.Equals(hidHotelID.Value))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error19").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

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

        if ("2".Equals(ddpSaveType.SelectedValue.Trim()) && String.IsNullOrEmpty(hidPlanWeekList.Value.Trim(',')))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error16").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if ("2".Equals(ddpSaveType.SelectedValue.Trim()) && (String.IsNullOrEmpty(dpKeepStart.Value.Trim()) || String.IsNullOrEmpty(dpKeepEnd.Value.Trim()) || String.IsNullOrEmpty(dpPlanTime.Value.Trim())))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error17").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        if ("1".Equals(ddpSaveType.SelectedValue.Trim()) && (String.IsNullOrEmpty(dpPlanDTime.Value.Trim())))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error18").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }

        //if (String.IsNullOrEmpty(hidCommonList.Value.Trim()))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
        //    //UpdatePanel6.Update();
        //    return;
        //}

        if (String.IsNullOrEmpty(ddpHotelRoomList.SelectedValue.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
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

        if ("true".Equals(ddpRoomStatus.SelectedValue.Trim().ToLower()) && String.IsNullOrEmpty(txtTwoPrice.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error12").ToString();
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

        if (!String.IsNullOrEmpty(txtTwoPrice.Text.Trim()) && !ChkLowPrice(txtTwoPrice.Text.Trim()))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel6.Update();
            return;
        }


        string effHour = "";
        effHour = GetEffHourVal(ddpEffHour.SelectedValue.Trim());
        if ("99".Equals(ddpEffHour.SelectedValue.Trim()) && String.IsNullOrEmpty(effHour))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error22").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
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
        //appcontentDBEntity.RoomList = hidCommonList.Value.ToString().Trim();
        appcontentDBEntity.RoomCode = ddpHotelRoomList.SelectedValue.Trim();
        appcontentDBEntity.RoomName = (ddpHotelRoomList.SelectedItem.Text.Trim().Contains("]")) ? ddpHotelRoomList.SelectedItem.Text.Trim().Substring(ddpHotelRoomList.SelectedItem.Text.Trim().IndexOf(']') + 1) : ddpHotelRoomList.SelectedItem.Text.Trim();
        appcontentDBEntity.StartDTime = dpKeepStart.Value.ToString().Trim();
        appcontentDBEntity.EndDTime = dpKeepEnd.Value.ToString().Trim();
        appcontentDBEntity.EffHour = effHour;
        appcontentDBEntity.WeekList = hidWeekList.Value.Trim(',');

        //if ("lmbar".Equals(ddpPriceType.SelectedValue.Trim().ToLower()))
        //{
        //    appcontentDBEntity.Note1 = lbNote1.Text.Trim().Substring(1, lbNote1.Text.Trim().IndexOf('】') -1);
        //    appcontentDBEntity.Note2 = lbNote11.Text.Trim().Substring(1, lbNote11.Text.Trim().IndexOf('】') -1);
        //}
        //else
        //{
        //    appcontentDBEntity.Note1 = lbNote2.Text.Trim().Substring(1, lbNote2.Text.Trim().IndexOf('】') - 1);
        //    appcontentDBEntity.Note2 = lbNote22.Text.Trim().Substring(1, lbNote22.Text.Trim().IndexOf('】') - 1 );
        //}

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

        appcontentDBEntity.SaveType = ddpSaveType.SelectedValue.Trim();
        appcontentDBEntity.PlanDTime = dpPlanDTime.Value.Trim();
        appcontentDBEntity.PlanTime = dpPlanTime.Value.Trim();
        appcontentDBEntity.PlanStart = dpKeepStart.Value.ToString().Trim(); //dpPlanStart.Value.Trim();
        appcontentDBEntity.PlanEnd = dpKeepEnd.Value.ToString().Trim(); //dpPlanEnd.Value.Trim();
        appcontentDBEntity.PlanWeek = hidWeekList.Value.Trim(','); //hidPlanWeekList.Value.Trim(',');

        appcontentDBEntity.Supplier = ddpSup.SelectedValue.Trim();

        appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        _appcontentEntity = APPContentBP.CreateSalesPlan(_appcontentEntity);
        int iResult = _appcontentEntity.Result;
        string strPlanID = _appcontentEntity.APPContentDBEntity[0].PlanID;
        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店销售计划-保存";
        commonDBEntity.Event_ID = strPlanID;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        string msgCommon = string.Empty;
        msgCommon = ddpHotelRoomList.SelectedItem.Text;// +"[" + ddpHotelRoomList.SelectedValue + "]";
        string msgPlanDTime = "";
        string PlanStart = "";
        string PlanWeekList = "";

        if ("1".Equals(ddpSaveType.SelectedValue.Trim()))
        {
            msgPlanDTime = dpPlanDTime.Value;
        }
        else if ("2".Equals(ddpSaveType.SelectedValue.Trim()))
        {
            msgPlanDTime = dpPlanTime.Value;
            PlanStart = dpKeepStart.Value.ToString().Trim() + "-" + dpKeepEnd.Value.ToString().Trim();
            PlanWeekList = hidWeekList.Value.Trim(',');
        }

        conTent = string.Format(conTent, hidHotelID.Value.ToString().Trim(), msgCommon, dpKeepStart.Value, dpKeepEnd.Value, ddpEffHour.SelectedValue.Trim(), ddpPriceType.SelectedValue, txtRoomCount.Text.Trim(), ddpRoomStatus.SelectedValue.Trim(), hidWeekList.Value.Trim(','), txtOnePrice.Text.Trim(), txtTwoPrice.Text.Trim(), txtThreePrice.Text.Trim(), txtFourPrice.Text.Trim(), txtBedPrice.Text.Trim(), txtNetPrice.Text.Trim(), ddpBreakfastNum.SelectedValue.Trim(), txtBreakPrice.Text.Trim(), ddpIsNetwork.SelectedValue.Trim(), txtOffsetval.Text.Trim(), ddpOffsetunit.SelectedValue.Trim(), ddpIsReserve.SelectedValue.Trim(), ddpSaveType.SelectedValue.Trim(), msgPlanDTime, PlanStart, PlanWeekList, strPlanID, ddpSup.SelectedValue);
        commonDBEntity.Event_Content = conTent;
        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error8").ToString() + _appcontentEntity.ErrorMSG;
            messageContent.InnerHtml = GetLocalResourceObject("Error8").ToString() + _appcontentEntity.ErrorMSG;
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
        //UpdatePanel6.Update();
    }

    private string GetEffHourVal(string param)
    {
        if (!"99".Equals(param))
        {
            return param;
        }
        return hidEffHour.Value.Trim();
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

    private bool ChkLowPrice(string param)
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        string HotelID = hidHotelID.Value.ToString().Trim().Substring((hidHotelID.Value.ToString().Trim().IndexOf('[') + 1), (hidHotelID.Value.ToString().Trim().IndexOf(']') - 1));
        appcontentDBEntity.HotelID = HotelID;
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
            SetDdpEmptyList();
            return;
        }

        if (!wctHotel.AutoResult.Equals(hidHotelID.Value))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error10").ToString();
            //UpdatePanel6.Update();
            SetDdpEmptyList();
            return;
        }

        GetRoomList();

        //UpdatePanel6.Update();
    }

    private void GetRoomList()
    {
        PromotionEntity _promotionEntity = new PromotionEntity();
        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();

        string strHotel = wctHotel.AutoResult.ToString();
        if (String.IsNullOrEmpty(strHotel) || !strHotel.Contains("[") || !strHotel.Contains("]"))
        {
            SetDdpEmptyList();
            messageContent.InnerHtml = GetLocalResourceObject("Error9").ToString();
            return;
        }
        promotionEntity.HotelID = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1)); ;
        promotionEntity.RateCode = ddpPriceType.SelectedValue;

        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        DataSet dsResult = PromotionBP.GetHotelRoomListAll(_promotionEntity);

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            ddpHotelRoomList.DataTextField = "ROOMNM";
            ddpHotelRoomList.DataValueField = "ROOMCODE";
            ddpHotelRoomList.DataSource = dsResult;
            ddpHotelRoomList.DataBind();
            //UpdatePanel2.Update();
        }
        else
        {
            SetDdpEmptyList();
            //UpdatePanel2.Update();

            messageContent.InnerHtml = GetLocalResourceObject("Error9").ToString();
        }
    }

    private void SetDdpEmptyList()
    {
        DataSet dsResult = new DataSet();

        dsResult.Tables.Add(new DataTable());
        dsResult.Tables[0].Columns.Add("HOTELROOMCODE");
        dsResult.Tables[0].Columns.Add("HOTELROOMNM");

        ddpHotelRoomList.DataTextField = "HOTELROOMNM";
        ddpHotelRoomList.DataValueField = "HOTELROOMCODE";
        ddpHotelRoomList.DataSource = dsResult;
        ddpHotelRoomList.DataBind();
    }
    //protected void ddpPriceType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddpPriceType.SelectedValue == "LMBAR")
    //    {
    //        dvlm2.Style.Add("display","none");
    //        dvlm.Style.Add("display", "");
    //    }
    //    else
    //    {
    //        dvlm2.Style.Add("display","");
    //        dvlm.Style.Add("display", "none");
    //    }
    //    //UpdatePanel4.Update();
    //}
    protected void ddpPriceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ("LMBAR".Equals(ddpPriceType.SelectedValue))
        {
            ddpGuaid.SelectedValue = "PP";
            ddpCxlid.SelectedValue = "PT100";
        }
        else
        {
            ddpGuaid.SelectedValue = "RH04";
            ddpCxlid.SelectedValue = "NP24";
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

        if (String.IsNullOrEmpty(strHotel) || !strHotel.Contains("[") || !strHotel.Contains("]"))
        {
            return;
        }

        promotionEntity.HotelID = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1)); ;
        promotionEntity.RateCode = ddpPriceType.SelectedValue;

        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        DataSet dsResult = PromotionBP.GetHotelRoomListAll(_promotionEntity);

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            ddpHotelRoomList.DataTextField = "ROOMNM";
            ddpHotelRoomList.DataValueField = "ROOMCODE";
            ddpHotelRoomList.DataSource = dsResult;
            ddpHotelRoomList.DataBind();
            //UpdatePanel2.Update();
        }
    }
}