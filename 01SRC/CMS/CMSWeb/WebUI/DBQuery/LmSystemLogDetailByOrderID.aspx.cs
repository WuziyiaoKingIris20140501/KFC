using System;
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

public partial class LmSystemLogDetailByOrderID : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string EventLMID = GetEventLMOrderID(Request.QueryString["ID"].ToString().Trim());

                if (String.IsNullOrEmpty(EventLMID.Trim()))
                {
                    detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                    return;
                }

                hidEventLMID.Value = EventLMID;
                ////BindChannelDDL();
                BindViewCSSystemLogMain();
                BindViewCSSystemLogDetail();
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            }
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
        lmlorder_cancle_reason.Text = dsMainResult.Tables[0].Rows[0]["order_cancle_reason"].ToString();
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
    }

    private void BindViewCSSystemLogDetail()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.EventID = hidEventLMID.Value;

        DataSet dsDetailResult = LmSystemLogBP.UserDetailListSelect(_lmSystemLogEntity).QueryResult;

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
        gridViewCSSystemLogDetail.DataKeyNames = new string[] { "nid" };//主键
        gridViewCSSystemLogDetail.DataBind();
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
}