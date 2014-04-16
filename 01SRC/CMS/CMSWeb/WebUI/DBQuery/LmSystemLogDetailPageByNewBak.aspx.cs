﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class LmSystemLogDetailPageByNew : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();
    string FogOrderNum = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string EventLMID = string.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                EventLMID = Request.QueryString["ID"].ToString().Trim();
              
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["FOGID"]))
            {
                EventLMID = GetEventLMOrderID(Request.QueryString["FOGID"].ToString().Trim());

                if (String.IsNullOrEmpty(EventLMID.Trim()))
                {
                    detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                    return;
                }
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            }

            hidEventLMID.Value = EventLMID;
            BindViewCSSystemLogMain();
            BindViewCSSystemLogDetail();
            BingSurvey(FogOrderNum);
            BindGridViewPlanDetail();
        }
        //messageContent.InnerHtml = "";
    }

    private string GetEventLMOrderID(string orderID)
    {
        if (String.IsNullOrEmpty(orderID.Trim()))
        {
            return "";
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = orderID;

        DataSet dsMainResult = LmSystemLogBP.GetEventLMOrderID(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            return "";
        }

        return dsMainResult.Tables[0].Rows[0][0].ToString();
    }

    private void BindViewCSSystemLogMain()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = hidEventLMID.Value;

        DataSet dsMainResult = LmSystemLogBP.UserMainListSelect(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        lmlid.Text = dsMainResult.Tables[0].Rows[0]["id"].ToString();
        lmlorder_num.Text = dsMainResult.Tables[0].Rows[0]["order_num"].ToString();
        lmlcity_id.Text = dsMainResult.Tables[0].Rows[0]["city_id"].ToString();
        lmlhotel_id.Text = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();
        hidHotelID.Value = dsMainResult.Tables[0].Rows[0]["hotel_id"].ToString();
        lmlhotel_name.Text = dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();
        lmlin_date.Text = dsMainResult.Tables[0].Rows[0]["in_date"].ToString();
        lmlbook_room_num.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString();
        lmlguest_names.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();
        lmlcontact_name.Text = dsMainResult.Tables[0].Rows[0]["contact_name"].ToString();
        lmlcontact_tel.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();
        lmlbook_type.Text = dsMainResult.Tables[0].Rows[0]["book_type"].ToString();
        lmlcreate_time.Text = dsMainResult.Tables[0].Rows[0]["create_time"].ToString();
        lmluser_id.Text = dsMainResult.Tables[0].Rows[0]["user_id"].ToString();
        lmlbook_status.Text = dsMainResult.Tables[0].Rows[0]["book_status"].ToString();
        lmlpay_status.Text = dsMainResult.Tables[0].Rows[0]["pay_status"].ToString();
        lmlhold_time.Text = dsMainResult.Tables[0].Rows[0]["hold_time"].ToString();
        lmlfog_order_num.Text = dsMainResult.Tables[0].Rows[0]["fog_order_num"].ToString();
        lmlout_date.Text = dsMainResult.Tables[0].Rows[0]["out_date"].ToString();
        lmlbook_remark.Text = dsMainResult.Tables[0].Rows[0]["book_remark"].ToString();
        lmlbook_source.Text = dsMainResult.Tables[0].Rows[0]["book_source"].ToString();
        lmlbook_price.Text = dsMainResult.Tables[0].Rows[0]["book_price"].ToString();
        lmlroom_type_code.Text = dsMainResult.Tables[0].Rows[0]["room_type_code"].ToString();
        lmlprice_code.Text = dsMainResult.Tables[0].Rows[0]["price_code"].ToString();
        lmlorder_cancle_reason.Text = GetCancleReason(dsMainResult.Tables[0].Rows[0]["order_cancle_reason"].ToString());
        lmlorder_cancle_time.Text = dsMainResult.Tables[0].Rows[0]["ordercanceltime"].ToString();
        lmlbook_total_price.Text = dsMainResult.Tables[0].Rows[0]["book_total_price"].ToString();
        lmllogin_mobile.Text = dsMainResult.Tables[0].Rows[0]["login_mobile"].ToString();
        lmlovertime.Text = dsMainResult.Tables[0].Rows[0]["overtime"].ToString();
        lmlmemo1.Text = dsMainResult.Tables[0].Rows[0]["memo1"].ToString();
        lmlpro_desc.Text = dsMainResult.Tables[0].Rows[0]["pro_desc"].ToString();
        lmlpro_content.Text = dsMainResult.Tables[0].Rows[0]["pro_content"].ToString();
        lmlis_network.Text = dsMainResult.Tables[0].Rows[0]["is_network"].ToString();
        lmlbreakfast_num.Text = dsMainResult.Tables[0].Rows[0]["breakfast_num"].ToString();
        lmlticket_usercode.Text = dsMainResult.Tables[0].Rows[0]["ticket_usercode"].ToString();
        lmlticket_amount.Text = dsMainResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        lmlticket_count.Text = dsMainResult.Tables[0].Rows[0]["ticket_count"].ToString();
        lmlroom_type_name.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString();
        lmlbookstatusother.Text = dsMainResult.Tables[0].Rows[0]["book_status_other"].ToString();
        lmlbook_person_tel.Text = dsMainResult.Tables[0].Rows[0]["book_person_tel"].ToString();
        lmlpay_method.Text = dsMainResult.Tables[0].Rows[0]["pay_method"].ToString();
        lmlis_reserve.Text = dsMainResult.Tables[0].Rows[0]["is_reserve"].ToString();
        lmlfog_resvtype.Text = dsMainResult.Tables[0].Rows[0]["fog_resvtype"].ToString();
        lmlfog_resvstatus.Text = dsMainResult.Tables[0].Rows[0]["fog_resvstatus"].ToString();
        lmlfog_auditstatus.Text = dsMainResult.Tables[0].Rows[0]["fog_auditstatus"].ToString();
        lmllmbar_status.Text = dsMainResult.Tables[0].Rows[0]["lmbar_status"].ToString();
        lmlpay_methoddesc.Text = dsMainResult.Tables[0].Rows[0]["pay_methoddesc"].ToString();
        lmlupdate_time.Text = dsMainResult.Tables[0].Rows[0]["update_time"].ToString();
        //lmlinvoice_flg.Text = dsMainResult.Tables[0].Rows[0]["invoice_flg"].ToString();
        //lmlinvoice_code.Text = dsMainResult.Tables[0].Rows[0]["invoice_code"].ToString();
        lmlapp_platform.Text = dsMainResult.Tables[0].Rows[0]["app_platform"].ToString();
        lmisgua_name.Text = dsMainResult.Tables[0].Rows[0]["isguanm"].ToString();
        FogOrderNum = dsMainResult.Tables[0].Rows[0]["FOG_ORDER_NUM"].ToString();
    }

    private void BindViewCSSystemLogDetail()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = hidEventLMID.Value;

        DataSet dsDetailResult = LmSystemLogBP.UserDetailListSelectByNew(_lmSystemLogEntity).QueryResult;

        if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 1)
        {
            for (int i = 1; i <= dsDetailResult.Tables[0].Rows.Count -1; i++)
            {
                if (dsDetailResult.Tables[0].Rows[i - 1]["EVENTTIME"] != null && dsDetailResult.Tables[0].Rows[i]["EVENTTIME"] != null)
                {
                    dsDetailResult.Tables[0].Rows[i - 1]["LAG"] = SetTimeLag(dsDetailResult.Tables[0].Rows[i - 1]["EVENTTIME"].ToString(), dsDetailResult.Tables[0].Rows[i]["EVENTTIME"].ToString());
                }
            }
        }

        gridViewCSSystemLogDetail.DataSource = dsDetailResult.Tables[0].DefaultView;
        gridViewCSSystemLogDetail.DataKeyNames = new string[] { "EVENTTIME" };//主键
        gridViewCSSystemLogDetail.DataBind();
    }

    private void BindGridViewPlanDetail()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = hidEventLMID.Value;

        DataSet dsDetailResult = LmSystemLogBP.UserGridViewPlanDetail(_lmSystemLogEntity).QueryResult;
        gridViewPlan.DataSource = dsDetailResult.Tables[0].DefaultView;
        gridViewPlan.DataKeyNames = new string[] { "EVENTTIME" };//主键
        gridViewPlan.DataBind();
    }

    /// <summary>
    /// 订单SurveyDetail
    /// </summary>
    /// <param name="FogOrderNum"></param>
    /// <returns></returns>
    private void BingSurvey(string FogOrderNum)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = FogOrderNum;

        DataSet dsDetailResult = LmSystemLogBP.OrderSurveyDetail(_lmSystemLogEntity).QueryResult;
        if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 0)
        {
            this.lbl_survey_score.Text = dsDetailResult.Tables[0].Rows[0]["SCORE"] == null ? "" : dsDetailResult.Tables[0].Rows[0]["SCORE"].ToString();
            this.lbl_survey_question.Text = dsDetailResult.Tables[0].Rows[dsDetailResult.Tables[0].Rows.Count - 1]["CONTENT"].ToString();
            this.lbl_survey_feedback.Text = dsDetailResult.Tables[0].Rows[dsDetailResult.Tables[0].Rows.Count - 1]["FEEDBACK"].ToString();
            return;
        }
        this.lbl_survey_score.Text = "";
        this.lbl_survey_question.Text = "";
        this.lbl_survey_feedback.Text = "";
    }

    private string SetTimeLag(string strFrom, string strTo)
    {
        string strResult = "";

        if (!CheckDateTimeValue(strFrom, strTo))
        {
            return strResult;
        }

        DateTime dtFrom = DateTime.Parse(strFrom);
        DateTime dtTo = DateTime.Parse(strTo);

        System.TimeSpan ND = dtTo - dtFrom;

        strResult = strResult + ND.Days.ToString() + "天";
        strResult = strResult + ND.Hours.ToString() + "时";
        strResult = strResult + ND.Minutes.ToString() + "分";
        strResult = strResult + ND.Seconds.ToString() + "秒";
        return strResult;
    }

    private bool CheckDateTimeValue(string strFrom, string strTo)
    {
        try
        {
            DateTime.Parse(strFrom);
            DateTime.Parse(strTo);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void gridViewCSSystemLogDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    ////    //this.gridViewRegion.PageIndex = e.NewPageIndex;
    ////    //BindGridView();

    //    //执行循环，保证每条数据都可以更新
    //    for (int i = 1; i <= gridViewCSSystemLogDetail.Rows.Count; i++)
    //    {
    //        //首先判断是否是数据行
    //        e.Row.
    //    }
    }


    protected void gridViewCSSystemLogDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSSystemLogDetail.PageIndex = e.NewPageIndex;
        BindViewCSSystemLogDetail();
    }


    protected void gridViewPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    ////    //this.gridViewRegion.PageIndex = e.NewPageIndex;
    ////    //BindGridView();

    //    //执行循环，保证每条数据都可以更新
    //    for (int i = 1; i <= gridViewCSSystemLogDetail.Rows.Count; i++)
    //    {
    //        //首先判断是否是数据行
    //        e.Row.
    //    }
    }


    protected void gridViewPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewPlan.PageIndex = e.NewPageIndex;
        BindGridViewPlanDetail();
    }


    private string GetCancleReason(string reasonCode)
    {
        string reasonName = string.Empty;
        Hashtable htReason = new Hashtable();
        htReason.Add("CRG18", "LM订单客人手机端取消");
        htReason.Add("CRG17", "预授权失败自动取消");
        htReason.Add("CRC01", "满房");
        htReason.Add("CRC03", "员工差错");
        htReason.Add("CRC04", "蓄水单取消");
        htReason.Add("CRC06", "变价");
        htReason.Add("CRG14", "无法完成担保");
        htReason.Add("CRG06", "无法完成支付");
        htReason.Add("CRG11", "超时未支付");
        htReason.Add("CRG05", "测试订单");
        htReason.Add("CRG01", "行程改变");
        htReason.Add("CRG02", "无法满足特殊需求");
        htReason.Add("CRG03", "其他途径预订");
        htReason.Add("CRG04", "预订内容变更");
        htReason.Add("CRG07", "确认速度不满意");
        htReason.Add("CRG08", "GDS渠道取消");
        htReason.Add("CRG09", "IDS渠道取消");
        htReason.Add("CRG10", "接口渠道取消");
        htReason.Add("CRG13", "设施/位置不满意");
        htReason.Add("CRC02", "重复预订");
        htReason.Add("CRG15", "预订未用抵用券");
        htReason.Add("CRG16", "预订未登录");
        htReason.Add("CRC07", "Jrez渠道取消");
        htReason.Add("CRC05", "骚扰订单");
        htReason.Add("CRG99", "其他");
        htReason.Add("CRH01", "酒店反悔");
        htReason.Add("CRH03", "酒店停业/装修");
        htReason.Add("PGSRQ", "系统自动取消");
        htReason.Add("CRH02", "酒店不确认");
        htReason.Add("CRH05", "无法追加担保");
        htReason.Add("CRH06", "无法追加预付");
        htReason.Add("CRH07", "无协议/协议到期");
        htReason.Add("CRH08", "不可抗力");
        htReason.Add("CRH09", "酒店无人接听/无法接通");

        reasonName = htReason.ContainsKey(reasonCode) ? htReason[reasonCode].ToString() : reasonCode;
        return reasonName;
    }
}