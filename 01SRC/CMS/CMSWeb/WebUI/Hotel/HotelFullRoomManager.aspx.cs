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

public partial class HotelFullRoomManager : BasePage
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
            SetDateTimePacker();
            //ViewState["HotelID"] = hidHotelID.Value;
            //ViewState["CommonList"] = hidCommonList.Value;
            //ViewState["PriceCode"] = ddpPriceType.SelectedValue;
            //ViewState["StartDTime"] = dpKeepStart.Value;
            //ViewState["EndDTime"] = dpKeepEnd.Value;
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
        //dvUserGroup.FindControl("wctUserGroup"). .Attributes.Add("disabled", "disabled");
        //wctCity.CTLDISPLAY = "0";
        //wctHotel.CTLDISPLAY = "0";
        //wctHotelGroup.CTLDISPLAY = "0";
        //wctUserGroup.CTLDISPLAY = "1";
        //chkAllUserGroup.Attributes.Add("checked", "checked");


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
        //DataSet dsOnlineStatus = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
        //if (dsOnlineStatus.Tables.Count > 0)
        //{
        //    dsOnlineStatus.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
        //    dsOnlineStatus.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";

        //    ddpStatusList.DataTextField = "ONLINEDIS";
        //    ddpStatusList.DataValueField = "ONLINESTATUS";
        //    ddpStatusList.DataSource = dsOnlineStatus;
        //    ddpStatusList.DataBind();
        //}

        //DataSet dsPriorityType = CommonBP.GetConfigList(GetLocalResourceObject("PriorityType").ToString());
        //if (dsPriorityType.Tables.Count > 0)
        //{
        //    dsPriorityType.Tables[0].Columns["Key"].ColumnName = "PRIORITYKEY";
        //    dsPriorityType.Tables[0].Columns["Value"].ColumnName = "PRIORITYVALUE";

        //    ddpPriorityList.DataTextField = "PRIORITYVALUE";
        //    ddpPriorityList.DataValueField = "PRIORITYKEY";
        //    ddpPriorityList.DataSource = dsPriorityType;
        //    ddpPriorityList.DataBind();
        //}

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

    protected void btnRest_Click(object sender, EventArgs e)
    {
        SetDateTimePacker();
        //UpdatePanel4.Update();
        messageContent.InnerHtml = "";
        //UpdatePanel6.Update();
        //UpdatePanel1.Update();
        ddpPriceType.SelectedIndex = 0;
        chkHotelRoomList.DataSource = null;
        chkHotelRoomList.DataBind();
        lbHotel.Text = "";
        dvAutoComplete.Style.Add("display", "");
        dvlbHotel.Style.Add("display", "none");
        dvRoomList.Style.Add("display", "none");
        dvSelectHotel.Style.Add("display", "");
        dvLbTime.Style.Add("display", "none");
        dvKeepTime.Style.Add("display", "");
        dvHistory.Style.Add("display", "none");
        gridViewCSList.DataSource = null;
        gridViewCSList.DataBind();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DtControlStyle(false);", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(lbHotel.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (String.IsNullOrEmpty(hidKeepStart.Value.Trim()) || String.IsNullOrEmpty(hidKeepEnd.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
            //UpdatePanel6.Update();
            return;
        }

        if (String.IsNullOrEmpty(hidCommonList.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
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


        appcontentDBEntity.HotelID = lbHotel.Text.Trim();
        appcontentDBEntity.RoomList = hidCommonList.Value.ToString().Trim();
        appcontentDBEntity.PriceCode = ddpPriceType.SelectedValue.Trim();
        appcontentDBEntity.StartDTime = dpKeepStart.Value.ToString().Trim();
        appcontentDBEntity.EndDTime = dpKeepEnd.Value.ToString().Trim();
        appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        _appcontentEntity = APPContentBP.ApplyFullRoom(_appcontentEntity);
        int iResult = _appcontentEntity.Result;
        _commonEntity.LogMessages = _appcontentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店标记满房-保存";
        commonDBEntity.Event_ID = lbHotel.Text.Trim();

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
        conTent = string.Format(conTent, lbHotel.Text.Trim(), msgCommon, dpKeepStart.Value, dpKeepEnd.Value, ddpPriceType.SelectedValue);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            BindHistoryListGrid();
            //hidCommonList.Value = "";
            //hidHotelID.Value = "";
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = _appcontentEntity.ErrorMSG;
            messageContent.InnerHtml = _appcontentEntity.ErrorMSG;
        }
        else
        {
            string errormsg = "";
            string alList = _appcontentEntity.ErrorMSG;
            string[] strerrList = alList.Split(',');

            foreach (string strRoom in strerrList)
            {
                foreach (ListItem lt in chkHotelRoomList.Items)
                {
                    if (!String.IsNullOrEmpty(strRoom) && lt.Value.Equals(strRoom))
                    {
                        errormsg = errormsg + lt.Text + "[" + strRoom + "]" + ",";
                    }
                }
            }

            errormsg = (errormsg.Length > 0) ? errormsg.Substring(0, errormsg.Length - 1) : errormsg;
            conTent = GetLocalResourceObject("Error8").ToString();
            conTent = string.Format(conTent, errormsg);
            commonDBEntity.Event_Result = conTent;
            messageContent.InnerHtml = conTent;
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        //UpdatePanel6.Update();
    }

    protected void btnCannel_Click(object sender, EventArgs e)
    { 
        
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

        if (String.IsNullOrEmpty(dpKeepStart.Value.Trim()) || String.IsNullOrEmpty(dpKeepEnd.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
            //UpdatePanel6.Update();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetHotelControlVal('" + hidHotelID.Value + "');", true);
            return;
        }

        hidKeepStart.Value = dpKeepStart.Value.Trim();
        hidKeepEnd.Value = dpKeepEnd.Value.Trim();
        hidHotelID.Value = wctHotel.AutoResult;
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

        dvRoomList.Style.Add("display", "");
        dvAutoComplete.Style.Add("display", "none");
        lbHotel.Text = wctHotel.AutoResult;
        dvlbHotel.Style.Add("display", "");
        dvSelectHotel.Style.Add("display", "none");

        lbStart.Text = dpKeepStart.Value.Trim();
        lbEnd.Text = dpKeepEnd.Value.Trim();
        dvLbTime.Style.Add("display", "");
        dvKeepTime.Style.Add("display", "none");
        //UpdatePanel1.Update();
        //UpdatePanel4.Update();
        //UpdatePanel8.Update();
        //UpdatePanel6.Update();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DtControlStyle(true);", true);
        BindHistoryListGrid();
    }


    protected void gridViewCSList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSList.Rows.Count; i++)
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

    protected void gridViewCSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSList.PageIndex = e.NewPageIndex;
        BindHistoryListGrid();
    }

    private void BindHistoryListGrid()
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = lbHotel.Text.Trim();
        appcontentDBEntity.StartDTime = hidKeepStart.Value.Trim();
        appcontentDBEntity.EndDTime = hidKeepEnd.Value.Trim();
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        DataSet dsResult = APPContentBP.GetFullRoomHistoryList(_appcontentEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "EVENT" };//主键
        gridViewCSList.DataBind();
        dvHistory.Style.Add("display", "");
    }
}