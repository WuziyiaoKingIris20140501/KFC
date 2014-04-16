using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Web.Services;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Text.RegularExpressions;


public partial class OrderApprovedFax : BasePage
{
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type=hotel") && !this.Page.Request.QueryString.ToString().Contains("Type=city") && !this.Page.Request.QueryString.ToString().Contains("Type=tag") && !this.Page.Request.QueryString.ToString().Contains("Type=orderApro"))
        {
            OutStartDate.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
            OutEndDate.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");

            //OutDateTime.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
            chkListStar0.Checked = true;
            chkListStar1.Checked = false;
            chkListStar2.Checked = false;
            //chkListStar3.Checked = false;
            chkListStar4.Checked = false;
            ddpSort.SelectedIndex = 0;
            ViewState["HotelID"] = "";
            dvGridMessage.InnerHtml = "";
            this.gridHotelList.ShowHeader = false;
            CheckApproveUser(UserSession.Current.UserAccount, 0);
            BandDDpList();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "setsalesroomKeys", "SetSalesRoom('" + UserSession.Current.UserAccount + "');", true);
        }
    }

    private void BandDDpList()
    {
        DataTable dtSortType = GetCannelTypeData();
        ddpNoShow.DataSource = dtSortType;
        ddpNoShow.DataTextField = "SORT_TEXT";
        ddpNoShow.DataValueField = "SORT_STATUS";
        ddpNoShow.DataBind();
        ddpNoShow.SelectedIndex = 0;
    }

    private DataTable GetCannelTypeData()
    {
        DataTable dt = new DataTable();
        DataColumn SortStatus = new DataColumn("SORT_STATUS");
        DataColumn SortStatusText = new DataColumn("SORT_TEXT");
        dt.Columns.Add(SortStatus);
        dt.Columns.Add(SortStatusText);


        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["SORT_STATUS"] = "行程变更";
                    dr["SORT_TEXT"] = "行程变更";
                    break;
                //case "1":
                //    dr["SORT_STATUS"] = "超时到店，酒店满房";
                //    dr["SORT_TEXT"] = "超时到店，酒店满房";
                //    break;
                case "1":
                    dr["SORT_STATUS"] = "客人入住，但未按协议价入住";
                    dr["SORT_TEXT"] = "客人入住，但未按协议价入住";
                    break;
                //case "3":
                //    dr["SORT_STATUS"] = "客人抵达酒店，但未入住";
                //    dr["SORT_TEXT"] = "客人抵达酒店，但未入住";
                //    break;
                //case "4":
                //    dr["SORT_STATUS"] = "客人未收到确认sms或延时";
                //    dr["SORT_TEXT"] = "客人未收到确认sms或延时";
                //    break;
                //case "5":
                //    dr["SORT_STATUS"] = "联系不到客人";
                //    dr["SORT_TEXT"] = "联系不到客人";
                //    break;
                case "2":
                    dr["SORT_STATUS"] = "重复预订";
                    dr["SORT_TEXT"] = "重复预订";
                    break;
                //case "7":
                //    dr["SORT_STATUS"] = "客人未找到酒店";
                //    dr["SORT_TEXT"] = "客人未找到酒店";
                //    break;
                //case "8":
                //    dr["SORT_STATUS"] = "客人反馈未有预订";
                //    dr["SORT_TEXT"] = "客人反馈未有预订";
                //    break;
                //case "9":
                //    dr["SORT_STATUS"] = "保留时间内到店，酒店满房";
                //    dr["SORT_TEXT"] = "保留时间内到店，酒店满房";
                //    break;
                //case "10":
                //    dr["SORT_STATUS"] = "其他";
                //    dr["SORT_TEXT"] = "其他";
                //    break;
                //default:
                //    dr["SORT_TEXT"] = "未知状态";
                //    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        ViewState["OutStartDate"] = OutStartDate.Value;
        ViewState["OutEndDate"] = OutEndDate.Value;
        //ViewState["OutDateTime"] = OutDateTime.Value;

        ViewState["OrderID"] = txtOrderID.Text.Trim();
        ViewState["AuditStatus"] = GetAuditStatsList();
        ViewState["ADStatsBack"] = GetAuditStatsBk();
        ViewState["SortID"] = ddpSort.SelectedValue;
        ViewState["FaxNum"] = txtFaxNum.Text.Trim();
        ViewState["FaxStatus"] = hidFaxStatus.Value.Trim();
        ViewState["HotelID"] = "";
        rdOrderVerifyTimeDay.Checked = false;
        rdOrderVerifyTimeNight.Checked = false;
        rdOrderVerifyTypeFax.Checked = false;
        rdOrderVerifyTypeTel.Checked = false;

        txtOrderVerifyLinkMan.Text = "";
        txtOrderVerifyLinkTel.Text = "";
        txtOrderVerifyLinkFax.Text = "";
        txtOrderVerifyRemark.Value = "";
        lbHotelNM.Text = "";
        getHotelLists();
        if (gridHotelList.DataSource != null && gridHotelList.Rows.Count > 0)
        {
            HidSelIndex.Value = "";
            gridHotelList.SelectedIndex = -1;
        }
        dvGridMessage.InnerHtml = "请选择酒店";
    }

    public bool CheckApproveUser(string UserID, int iType)
    {
        messageContent.InnerHtml = "";
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.SalesID = UserID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.CheckApproveUser(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml = "该用户非订单审核人员，请确认！";
            return false;
        }

        if (iType == 1)
        {
            dsResult = HotelInfoBP.CheckApproveUserBandHotel(_hotelinfoEntity).QueryResult;
            if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
            {
                messageContent.InnerHtml = "该订单审核人员未绑定酒店，请确认！";
                return false;
            }
        }

        return true;
    }

    private string GetAuditStatsBk()
    {
        string strResult = "";
        //if (chkListStar1.Checked && !chkListStar3.Checked)
        //{
        //    strResult = strResult + "1,";
        //}

        if (chkListStar2.Checked && !chkListStar4.Checked)
        {
            strResult = strResult + "2,";
        }

        //if (chkListStar3.Checked && !chkListStar1.Checked)
        //{
        //    strResult = strResult + "3,";
        //}

        if (chkListStar4.Checked && !chkListStar2.Checked)
        {
            strResult = strResult + "4,";
        }
        return strResult.TrimEnd(',');
    }

    private string GetAuditStatsList()
    {
        string strResult = "";
        if (chkListStar0.Checked && (!chkListStar1.Checked) && (!chkListStar2.Checked && !chkListStar4.Checked))
        {
            strResult = "1";
        }
        else if (!chkListStar0.Checked && (chkListStar1.Checked) && (!chkListStar2.Checked && !chkListStar4.Checked))
        {
            strResult = "2";
        }
        else if (!chkListStar0.Checked && (!chkListStar1.Checked) && (chkListStar2.Checked || chkListStar4.Checked))
        {
            strResult = "3";
        }
        else if (chkListStar0.Checked && (!chkListStar1.Checked) && (chkListStar2.Checked || chkListStar4.Checked))
        {
            strResult = "4";
        }
        else if (chkListStar0.Checked && (chkListStar1.Checked) && (!chkListStar2.Checked && !chkListStar4.Checked))
        {
            strResult = "5";
        }
        else if (!chkListStar0.Checked && (chkListStar1.Checked) && (chkListStar2.Checked || chkListStar4.Checked))
        {
            strResult = "6";
        }
        else if (chkListStar0.Checked && (chkListStar1.Checked) && (chkListStar2.Checked || chkListStar4.Checked))
        {
            strResult = "7";
        }
        else{
            strResult = "";
        }
        return strResult;
    }

    public void getHotelLists()
    {
        messageContent.InnerHtml = "";

        DataTable dtResult = new DataTable();
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        string strOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        string strfaxNum = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxNum"].ToString())) ? null : ViewState["FaxNum"].ToString();

        if (!String.IsNullOrEmpty(strOrderID) || !String.IsNullOrEmpty(strfaxNum))
        {
            //hotelInfoDBEntity.Type = ddpSort.SelectedValue;
            hotelInfoDBEntity.OrderID = strOrderID;
            hotelInfoDBEntity.FaxNum = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxNum"].ToString())) ? null : ViewState["FaxNum"].ToString();

            //hotelInfoDBEntity.OutStartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            //hotelInfoDBEntity.OutEndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();

            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetOrderApproveHotelFaxList(_hotelinfoEntity).QueryResult.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                messageContent.InnerHtml = "无订单审核，请确认！";
            }
        }
        else if (!String.IsNullOrEmpty(hidSelectCity.Value) || !String.IsNullOrEmpty(hidSelectHotel.Value) || !String.IsNullOrEmpty(hidSelectBussiness.Value))
        {
            if (!String.IsNullOrEmpty(hidSelectHotel.Value))
            {
                if (!hidSelectHotel.Value.Trim().Contains("[") || !hidSelectHotel.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择酒店不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }
            if (!String.IsNullOrEmpty(hidSelectCity.Value))
            {
                if (!hidSelectCity.Value.Trim().Contains("[") || !hidSelectCity.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择城市不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }
            if (!String.IsNullOrEmpty(hidSelectBussiness.Value))
            {
                if (!hidSelectBussiness.Value.Trim().Contains("[") || !hidSelectBussiness.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择商圈不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }

            string OutStartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            string OutEndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();
            if (String.IsNullOrEmpty(OutStartDate) && String.IsNullOrEmpty(OutEndDate))
            {
                messageContent.InnerHtml = "查询失败，请选择离店日期！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }
            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setsalesroomKeysOne", "SetSalesRoom('')", true);
            hotelInfoDBEntity.City = hidSelectCity.Value == "" ? "" : hidSelectCity.Value.Substring((hidSelectCity.Value.IndexOf('[') + 1), (hidSelectCity.Value.IndexOf(']') - 1)); //"";//城市ID 
            hotelInfoDBEntity.HotelID = hidSelectHotel.Value == "" ? "" : hidSelectHotel.Value.Substring((hidSelectHotel.Value.IndexOf('[') + 1), (hidSelectHotel.Value.IndexOf(']') - 1));//"";//酒店ID
            hotelInfoDBEntity.Bussiness = hidSelectBussiness.Value == "" ? "" : hidSelectBussiness.Value.Substring((hidSelectBussiness.Value.IndexOf('[') + 1), (hidSelectBussiness.Value.IndexOf(']') - 1));//"";//商圈ID
            //hotelInfoDBEntity.Type = ddpSort.SelectedValue;
            //hotelInfoDBEntity.InDateFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            //hotelInfoDBEntity.InDateTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();

            hotelInfoDBEntity.OutStartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            hotelInfoDBEntity.OutEndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();

            //hotelInfoDBEntity.OutDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutDateTime"].ToString())) ? null : ViewState["OutDateTime"].ToString();
            hotelInfoDBEntity.OrderID = strOrderID;
            hotelInfoDBEntity.AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
            hotelInfoDBEntity.ADStatsBack = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ADStatsBack"].ToString())) ? null : ViewState["ADStatsBack"].ToString();

            hotelInfoDBEntity.FaxNum = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxNum"].ToString())) ? null : ViewState["FaxNum"].ToString();
            hotelInfoDBEntity.FaxStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxStatus"].ToString())) ? null : ViewState["FaxStatus"].ToString();

            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetOrderApproveHotelFaxList(_hotelinfoEntity).QueryResult.Tables[0];

            if (dtResult.Rows.Count == 0)
            {
                messageContent.InnerHtml = "无订单审核，请确认！";
            }
        }
        else
        {
            if (this.hidSelectSalesID.Value != UserSession.Current.UserAccount)
            {
                if (String.IsNullOrEmpty(this.hidSelectSalesID.Value.Trim()))
                {
                    messageContent.InnerHtml = "查询失败，选择用户不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }

                if (!hidSelectSalesID.Value.Trim().Contains("[") || !hidSelectSalesID.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择用户不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }

            string OutStartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            string OutEndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();
            if (String.IsNullOrEmpty(OutStartDate) && String.IsNullOrEmpty(OutEndDate))
            {
                messageContent.InnerHtml = "查询失败，请选择离店日期！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setsalesroomKeysOne", "SetSalesRoom('" + this.hidSelectSalesID.Value + "')", true);
            hotelInfoDBEntity.SalesID = this.hidSelectSalesID.Value == UserSession.Current.UserAccount ? UserSession.Current.UserAccount : this.hidSelectSalesID.Value.Substring((this.hidSelectSalesID.Value.IndexOf('[') + 1), (this.hidSelectSalesID.Value.IndexOf(']') - 1));//"";//房控人员

            if (!CheckApproveUser(hotelInfoDBEntity.SalesID, 1))
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }

            //hotelInfoDBEntity.Type = ddpSort.SelectedValue;
            //hotelInfoDBEntity.InDateFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            //hotelInfoDBEntity.InDateTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();

            hotelInfoDBEntity.OrderID = strOrderID;

            hotelInfoDBEntity.OutStartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            hotelInfoDBEntity.OutEndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();

            //hotelInfoDBEntity.OutDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutDateTime"].ToString())) ? null : ViewState["OutDateTime"].ToString();
            hotelInfoDBEntity.AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
            hotelInfoDBEntity.ADStatsBack = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ADStatsBack"].ToString())) ? null : ViewState["ADStatsBack"].ToString();
            hotelInfoDBEntity.FaxStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxStatus"].ToString())) ? null : ViewState["FaxStatus"].ToString();
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetOrderFaxApproveList(_hotelinfoEntity).QueryResult.Tables[0];//得到当天所有 有计划  的酒店 

            if (dtResult.Rows.Count == 0)
            {
                messageContent.InnerHtml = "该审核人员绑定酒店无订单审核，请确认！";
            }
        }

        dtResult.DefaultView.Sort = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SortID"].ToString())) ? "" : ViewState["SortID"].ToString();
        this.gridHotelList.DataSource = dtResult.DefaultView;
        this.gridHotelList.DataBind();

        //int UADCUN = 0;
        //int ADCUN = 0;
        ////int BCUN = 0;
        //for (int i = 0; i < gridHotelList.Rows.Count; i++)
        //{
        //    //首先判断是否是数据行
        //    if (gridHotelList.Rows[i].RowType == DataControlRowType.DataRow)
        //    {
        //        UADCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[4].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[4].ToString());
        //        ADCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[5].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[5].ToString());
        //        //BCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[6].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[6].ToString());

        //        if (UADCUN == ADCUN)
        //        {
        //            gridHotelList.Rows[i].Cells[0].Attributes.Add("bgcolor", "#70A88C");
        //            gridHotelList.Rows[i].Cells[1].Attributes.Add("bgcolor", "#70A88C");

        //            //gridHotelList.Rows[i].Attributes.Add("bgcolor", "#70A88C");
        //        }
        //        //else if (BCUN > 0)
        //        //{
        //        //    gridHotelList.Rows[i].Cells[0].Attributes.Add("bgcolor", "#94FB92");
        //        //    gridHotelList.Rows[i].Cells[1].Attributes.Add("bgcolor", "#94FB92");

        //        //    //gridHotelList.Rows[i].Attributes.Add("bgcolor", "#94FB92");
        //        //}
        //    }
        //}

        gridViewCSList.DataSource = null;
        gridViewCSList.DataBind();

        this.UpdatePanel1.Update();

        string strOrderCount = dtResult.Rows.Count > 0 ? dtResult.Compute("SUM(ordercount)", "").ToString() : "0";

        this.countNum.InnerText = dtResult.Rows.Count.ToString() + "/" + strOrderCount;
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "keys", "BtnCompleteStyle();", true);
    }

    protected void gridHotelList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int UADCUN = 0;
            int ADCUN = 0;
            int BCUN = 0;

            UADCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[e.Row.RowIndex].Values[4].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[e.Row.RowIndex].Values[4].ToString());
            ADCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[e.Row.RowIndex].Values[5].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[e.Row.RowIndex].Values[5].ToString());
            BCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[e.Row.RowIndex].Values[6].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[e.Row.RowIndex].Values[6].ToString());

            //if (UADCUN == ADCUN)
            //{
            //    e.Row.Cells[0].Attributes.Add("bgcolor", "#70A88C");
            //    e.Row.Cells[1].Attributes.Add("bgcolor", "#70A88C");
            //}
            //else if (BCUN > 0)
            //{
            //    e.Row.Cells[0].Attributes.Add("bgcolor", "#94FB92");
            //    e.Row.Cells[1].Attributes.Add("bgcolor", "#94FB92");
            //}

            //e.Row.Cells[0].Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';c=this.parentNode.parentNode.childNodes[" + e.Row.RowIndex + "].childNodes[1].style.backgroundColor;this.parentNode.parentNode.childNodes[" + e.Row.RowIndex + "].childNodes[1].style.backgroundColor='#ebebce';");
            //e.Row.Cells[0].Attributes.Add("onMouseOut", "this.style.backgroundColor=t;this.parentNode.parentNode.childNodes[" + e.Row.RowIndex + "].childNodes[1].style.backgroundColor=c;");

            //e.Row.Cells[1].Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';c=this.parentNode.parentNode.childNodes[" + e.Row.RowIndex + "].childNodes[0].style.backgroundColor;this.parentNode.parentNode.childNodes[" + e.Row.RowIndex + "].childNodes[0].style.backgroundColor='#ebebce';");
            //e.Row.Cells[1].Attributes.Add("onMouseOut", "this.style.backgroundColor=t;this.parentNode.parentNode.childNodes[" + e.Row.RowIndex + "].childNodes[0].style.backgroundColor=c;");


            if (UADCUN == ADCUN)
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
            }
            else if (BCUN > 0)
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#94FB92");
            }

            e.Row.Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=t;");

            e.Row.Attributes.Add("OnClick", "ClickEvent('" + e.Row.RowIndex + "')");
        }
    }

    private DataSet GetConsultRoomHistoryList()
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.CreateUser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = HotelInfoBP.GetHasChangedConsultRoomList(_appcontentEntity).QueryResult;
        return dsResult;
    }

    public void GetBasicOrderAprInfo(string strHotelID)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.SelectHotelInfoEX(_hotelinfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            //日/夜审
            if (dsResult.Tables[0].Rows[0]["Ex_Mode"].ToString() == "1") { this.rdOrderVerifyTypeFax.Checked = true; this.rdOrderVerifyTypeTel.Checked = false; }
            else { this.rdOrderVerifyTypeFax.Checked = false; this.rdOrderVerifyTypeTel.Checked = true; }
            //审核方式
            if (dsResult.Tables[0].Rows[0]["Ex_Time"].ToString() == "000000000111111111100000") { this.rdOrderVerifyTimeDay.Checked = true; this.rdOrderVerifyTimeNight.Checked = false; }
            else { this.rdOrderVerifyTimeDay.Checked = false; this.rdOrderVerifyTimeNight.Checked = true; }

            if (dsResult.Tables[0].Rows[0]["Ex_Time"].ToString() == "000000000111111111100000")
            {
                //审核联系人
                this.txtOrderVerifyLinkMan.Text = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["LINKMAN"].ToString())) ? "" : dsResult.Tables[0].Rows[0]["LINKMAN"].ToString().Split('|')[10].ToString();
                //审核联系电话
                this.txtOrderVerifyLinkTel.Text = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["LINKTEL"].ToString())) ? "" : dsResult.Tables[0].Rows[0]["LINKTEL"].ToString().Split('|')[10].ToString();
                //审核联系传真
                this.txtOrderVerifyLinkFax.Text = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["LINKFAX"].ToString())) ? "" : dsResult.Tables[0].Rows[0]["LINKFAX"].ToString().Split('|')[10].ToString();
            }
            else
            {
                //审核联系人
                this.txtOrderVerifyLinkMan.Text = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["LINKMAN"].ToString())) ? "" : dsResult.Tables[0].Rows[0]["LINKMAN"].ToString().Split('|')[0].ToString();
                //审核联系电话
                this.txtOrderVerifyLinkTel.Text = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["LINKTEL"].ToString())) ? "" : dsResult.Tables[0].Rows[0]["LINKTEL"].ToString().Split('|')[0].ToString();
                //审核联系传真
                this.txtOrderVerifyLinkFax.Text = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["LINKFAX"].ToString())) ? "" : dsResult.Tables[0].Rows[0]["LINKFAX"].ToString().Split('|')[0].ToString();
            }
            //审核备注
            this.txtOrderVerifyRemark.InnerHtml = dsResult.Tables[0].Rows[0]["REMARK"].ToString();
        }
        else
        {
            rdOrderVerifyTimeDay.Checked = false;
            rdOrderVerifyTimeNight.Checked = false;
            rdOrderVerifyTypeFax.Checked = false;
            rdOrderVerifyTypeTel.Checked = false;

            txtOrderVerifyLinkMan.Text = "";
            txtOrderVerifyLinkTel.Text = "";
            txtOrderVerifyLinkFax.Text = "";
            txtOrderVerifyRemark.Value = "";
        }
    }

    private DataTable GetSalesManagerList()
    {
        WebAutoCompleteBP webBP = new WebAutoCompleteBP();
        DataTable dtSales = webBP.GetWebCompleteList("sales", "", "").Tables[0];
        return dtSales;
    }

    protected void btnSendFax_Click(object sender, EventArgs e)
    {
        string HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        if (String.IsNullOrEmpty(HotelID))
        {
            messageContent.InnerHtml = "发送传真失败，请选择酒店列表内酒店信息！";
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "grLayout();", true);
            return;
        }

        if (gridViewCSList.Rows.Count == 0)
        {
            messageContent.InnerHtml = "发送传真失败，选择酒店无审核订单信息，请修改！";
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "grLayout();", true);
            return;
        }

        ArrayList alList = new ArrayList();
        for (int i = 0; i < this.gridViewCSList.Rows.Count; i++)
        {
            System.Web.UI.HtmlControls.HtmlInputCheckBox ck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSList.Rows[i].FindControl("chkItems");
            if (ck.Checked == true)
            {
                alList.Add(gridViewCSList.DataKeys[i].Value.ToString());
            }
        }

        if (alList.Count == 0)
        {
            messageContent.InnerHtml = "发送传真失败，请选择订单列表需要发送传真的订单！";
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "grLayout();", true);
            return;
        }

        //StringBuilder sbOrderList = new StringBuilder();
        //for (int i = 0; i < gridViewCSList.Rows.Count; i++)
        //{
        //    sbOrderList.Append("\"");
        //    sbOrderList.Append(gridViewCSList.DataKeys[i].Values[0].ToString());
        //    sbOrderList.Append("\",");
        //}

        StringBuilder sbOrderList = new StringBuilder();
        foreach (string strSN in alList)
        {
            sbOrderList.Append("\"");
            sbOrderList.Append(strSN);
            sbOrderList.Append("\",");
        }

        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        
        _lmSystemLogEntity.HotelID = HotelID;
        _lmSystemLogEntity.FogOrderID = sbOrderList.ToString().TrimEnd(',');
        _lmSystemLogEntity.SendFaxType = "4";
        _lmSystemLogEntity.BookRemark = "";
        _lmSystemLogEntity.ObjectID = HotelID;
        int iResult = LmSystemLogBP.SendFaxService(_lmSystemLogEntity).Result;

        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单审核-发送传真";
        commonDBEntity.Event_ID = hidOrderID.Value;
        string conTent = GetLocalResourceObject("EventSendFaxMessage").ToString();

        conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, sbOrderList.ToString().TrimEnd(','));
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SendFaxSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("SendFaxSuccess").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SendFaxError").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("SendFaxError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "BtnCompleteStyle();", true);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();

        if (String.IsNullOrEmpty(HotelID))
        {
            messageContent.InnerHtml = "打印审核单失败，请选择酒店列表内酒店信息！";
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "grLayout();", true);
            return ;
        }

        //string StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
        //string EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();

        //string OutDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutDateTime"].ToString())) ? null : ViewState["OutDateTime"].ToString();
        //string AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
        //string OrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();

        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "openPrintPage('" + HotelID + "','" + OutDate + "','" + AuditStatus + "','" + OrderID + "')", true);

        ArrayList alList = new ArrayList();
        for (int i = 0; i < this.gridViewCSList.Rows.Count; i++)
        {
            System.Web.UI.HtmlControls.HtmlInputCheckBox ck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSList.Rows[i].FindControl("chkItems");
            if (ck.Checked == true)
            {
                alList.Add(gridViewCSList.DataKeys[i].Value.ToString());
            }
        }

        if (alList.Count == 0)
        {
            messageContent.InnerHtml = "打印审核单失败，请选择订单列表需要打印的订单！";
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "grLayout();", true);
            return ;
        }

        Session["OrderFaxList"] = alList;

        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "openFaxPrintPage('" + HotelID + "')", true);
    }

    protected void btnPreFax_Click(object sender, EventArgs e)
    {
        string HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();

        if (String.IsNullOrEmpty(HotelID))
        {
            messageContent.InnerHtml = "查看相关传真失败，请选择酒店列表内酒店信息！";
            return ;
        }

        string OutStartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
        string OutEndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();

        //string OutDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutDateTime"].ToString())) ? null : ViewState["OutDateTime"].ToString();
        string FaxStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxStatus"].ToString())) ? null : ViewState["FaxStatus"].ToString();

        string OrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString(); ;
        string FaxNum = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxNum"].ToString())) ? null : ViewState["FaxNum"].ToString();

        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "OpenPreviewFaxList('" + HotelID + "', '" + OutStartDate + "', '" + OutEndDate + "', '" + FaxStatus + "', '" + OrderID + "', '" + FaxNum + "')", true);
    }

    protected void btnUpdateLink_Click(object sender, EventArgs e)
    {
        HotelInfoEXEntity _hotelinfoEXEntity = new HotelInfoEXEntity();

        _hotelinfoEXEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEXEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEXEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEXEntity.LogMessages.IpAddress = UserSession.Current.UserIP;


        _hotelinfoEXEntity.HotelInfoEXDBEntity = new List<HotelInfoEXDBEntity>();
        HotelInfoEXDBEntity hotelInfoEXDBEntity = new HotelInfoEXDBEntity();

        string hotelId = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();//酒店ID

        if (String.IsNullOrEmpty(hotelId))
        {
            messageContent.InnerHtml = "酒店联系信息保存失败，请选择酒店！";
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "grLayout();", true);
            return;
        }

        if (!chkFaxVaild(txtOrderVerifyLinkFax.Text.Trim()))
        {
            messageContent.InnerHtml = "审核联系传真只能输入数字和'+'，请修改！";
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "grLayout();", true);
            return;
        }


        //日间9点-18点   夜间  19点-8点
        string OrderVerifyTime = rdOrderVerifyTimeDay.Checked == true ? "000000000111111111100000" : "111111111000000000011111";//日/夜审
        string OrderVerifyType = rdOrderVerifyTypeFax.Checked == true ? "1" : "2";//审核方式

        string rdOrderVerifyLinkMan = txtOrderVerifyLinkMan.Text;//审核联系人
        string rdOrderVerifyLinkTel = txtOrderVerifyLinkTel.Text;//审核联系电话
        string rdOrderVerifyLinkFax = txtOrderVerifyLinkFax.Text;//审核联系传真
        string rdOrderVerifyRemark = this.txtOrderVerifyRemark.Value;//审核备注
        hotelInfoEXDBEntity.HotelID = hotelId;
        hotelInfoEXDBEntity.Type = "3";
        StringBuilder OrderVerifyLinkMan = new StringBuilder();
        for (int i = 0; i < OrderVerifyTime.Length; i++)
        {
            if (OrderVerifyTime[i].ToString() == "1")
            {
                OrderVerifyLinkMan.Append(txtOrderVerifyLinkMan.Text + "|");
            }
            else
            {
                OrderVerifyLinkMan.Append("|");
            }
        }

        StringBuilder OrderVerifyLinkTel = new StringBuilder();
        for (int i = 0; i < OrderVerifyTime.Length; i++)
        {
            if (OrderVerifyTime[i].ToString() == "1")
            {
                OrderVerifyLinkTel.Append(txtOrderVerifyLinkTel.Text + "|");
            }
            else
            {
                OrderVerifyLinkTel.Append("|");
            }
        }

        StringBuilder OrderVerifyLinkFax = new StringBuilder();
        for (int i = 0; i < OrderVerifyTime.Length; i++)
        {
            if (OrderVerifyTime[i].ToString() == "1")
            {
                OrderVerifyLinkFax.Append(txtOrderVerifyLinkFax.Text + "|");
            }
            else
            {
                OrderVerifyLinkFax.Append("|");
            }
        }

        hotelInfoEXDBEntity.LinkMan = (OrderVerifyLinkMan.ToString().Length > 0) ? OrderVerifyLinkMan.ToString().Substring(0, OrderVerifyLinkMan.Length - 1) : OrderVerifyLinkMan.ToString();
        hotelInfoEXDBEntity.LinkTel = (OrderVerifyLinkTel.ToString().Length > 0) ? OrderVerifyLinkTel.ToString().Substring(0, OrderVerifyLinkTel.Length - 1) : OrderVerifyLinkTel.ToString();
        hotelInfoEXDBEntity.LinkFax = (OrderVerifyLinkFax.ToString().Length > 0) ? OrderVerifyLinkFax.ToString().Substring(0, OrderVerifyLinkFax.Length - 1) : OrderVerifyLinkFax.ToString();
        hotelInfoEXDBEntity.Remark = rdOrderVerifyRemark;
        hotelInfoEXDBEntity.ExTime = OrderVerifyTime;
        hotelInfoEXDBEntity.ExMode = OrderVerifyType;
        hotelInfoEXDBEntity.Status = "1";
        hotelInfoEXDBEntity.CreateUser = UserSession.Current.UserDspName;
        hotelInfoEXDBEntity.UpdateUser = UserSession.Current.UserDspName;
        _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);

        HotelInfoEXBP.SaveHotelInfoEX(_hotelinfoEXEntity);
        HotelInfoEXBP.InsertHotelEXHistory(_hotelinfoEXEntity);
        _hotelinfoEXEntity.HotelInfoEXDBEntity.Clear();
        messageContent.InnerHtml = "酒店联系信息保存成功！";
        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "BtnCompleteStyle();", true);
    }

    private bool chkFaxVaild(string arg)
    {
        if (String.IsNullOrEmpty(arg))
        {
            return true;
        }

        string strPatern = @"^[\d+|\+]+$";
        Regex reg = new Regex(strPatern);
        if (reg.IsMatch(arg))
        {
            return true;
        }
        return false;
    }

    private void BindOrderConfirmListGrid()
    {
        messageContent.InnerHtml = "";
        OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        string strOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        string strfaxNum = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxNum"].ToString())) ? null : ViewState["FaxNum"].ToString();

        if (!String.IsNullOrEmpty(strOrderID) || !String.IsNullOrEmpty(strfaxNum))
        {
            orderinfoEntity.OrderID = strOrderID;
            orderinfoEntity.FaxNum = strfaxNum;
            orderinfoEntity.SqlType = "1";
        }
        else if (!String.IsNullOrEmpty(hidSelectCity.Value) || !String.IsNullOrEmpty(hidSelectHotel.Value) || !String.IsNullOrEmpty(hidSelectBussiness.Value))
        {
            orderinfoEntity.City = hidSelectCity.Value == "" ? "" : hidSelectCity.Value.Substring((hidSelectCity.Value.IndexOf('[') + 1), (hidSelectCity.Value.IndexOf(']') - 1)); //"";//城市ID 
            //orderinfoEntity.HotelID = hidSelectHotel.Value == "" ? "" : hidSelectHotel.Value.Substring((hidSelectHotel.Value.IndexOf('[') + 1), (hidSelectHotel.Value.IndexOf(']') - 1));//"";//酒店ID
            orderinfoEntity.Bussiness = hidSelectBussiness.Value == "" ? "" : hidSelectBussiness.Value.Substring((hidSelectBussiness.Value.IndexOf('[') + 1), (hidSelectBussiness.Value.IndexOf(']') - 1));//"";//商圈ID
            orderinfoEntity.StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            orderinfoEntity.EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();
            orderinfoEntity.OrderID = strOrderID;
            orderinfoEntity.AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
            orderinfoEntity.ADStatsBack = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ADStatsBack"].ToString())) ? null : ViewState["ADStatsBack"].ToString();
            orderinfoEntity.FaxNum = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxNum"].ToString())) ? null : ViewState["FaxNum"].ToString();
            //orderinfoEntity.FaxStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxStatus"].ToString())) ? null : ViewState["FaxStatus"].ToString();
            orderinfoEntity.SqlType = "2";
        }
        else
        {
            orderinfoEntity.OrderID = strOrderID;
            orderinfoEntity.StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            orderinfoEntity.EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();
            orderinfoEntity.AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
            orderinfoEntity.ADStatsBack = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ADStatsBack"].ToString())) ? null : ViewState["ADStatsBack"].ToString();
            //orderinfoEntity.FaxStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxStatus"].ToString())) ? null : ViewState["FaxStatus"].ToString();
            orderinfoEntity.SqlType = "3";
        }


        //orderinfoEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        //orderinfoEntity.StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
        //orderinfoEntity.EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();
        //orderinfoEntity.OrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        //orderinfoEntity.AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
        //orderinfoEntity.ADStatsBack = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ADStatsBack"].ToString())) ? null : ViewState["ADStatsBack"].ToString();

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderInfoBP.BindOrderApproveFaxList(_orderInfoEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "ORDERID" };//主键
        gridViewCSList.DataBind();

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            dvGridMessage.InnerHtml = "无审核订单";
        }
        else
        {
            dvGridMessage.InnerHtml = "";
        }
        gridViewCSList.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");

        GetBasicOrderAprInfo(orderinfoEntity.HotelID);
        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "BtnCompleteStyle();", true);
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    private void ResetOrderConfirmListGrid()
    {
        OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        orderinfoEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        string strOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        string strfaxNum = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxNum"].ToString())) ? null : ViewState["FaxNum"].ToString();

        if (!String.IsNullOrEmpty(strOrderID) || !String.IsNullOrEmpty(strfaxNum))
        {
            orderinfoEntity.OrderID = strOrderID;
            orderinfoEntity.FaxNum = strfaxNum;
            orderinfoEntity.SqlType = "1";
        }
        else if (!String.IsNullOrEmpty(hidSelectCity.Value) || !String.IsNullOrEmpty(hidSelectHotel.Value) || !String.IsNullOrEmpty(hidSelectBussiness.Value))
        {
            orderinfoEntity.City = hidSelectCity.Value == "" ? "" : hidSelectCity.Value.Substring((hidSelectCity.Value.IndexOf('[') + 1), (hidSelectCity.Value.IndexOf(']') - 1)); //"";//城市ID 
            //orderinfoEntity.HotelID = hidSelectHotel.Value == "" ? "" : hidSelectHotel.Value.Substring((hidSelectHotel.Value.IndexOf('[') + 1), (hidSelectHotel.Value.IndexOf(']') - 1));//"";//酒店ID
            orderinfoEntity.Bussiness = hidSelectBussiness.Value == "" ? "" : hidSelectBussiness.Value.Substring((hidSelectBussiness.Value.IndexOf('[') + 1), (hidSelectBussiness.Value.IndexOf(']') - 1));//"";//商圈ID
            orderinfoEntity.StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            orderinfoEntity.EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();
            orderinfoEntity.OrderID = strOrderID;
            orderinfoEntity.AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
            orderinfoEntity.ADStatsBack = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ADStatsBack"].ToString())) ? null : ViewState["ADStatsBack"].ToString();
            orderinfoEntity.FaxNum = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxNum"].ToString())) ? null : ViewState["FaxNum"].ToString();
            //orderinfoEntity.FaxStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxStatus"].ToString())) ? null : ViewState["FaxStatus"].ToString();
            orderinfoEntity.SqlType = "2";
        }
        else
        {
            orderinfoEntity.OrderID = strOrderID;
            orderinfoEntity.StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
            orderinfoEntity.EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();
            orderinfoEntity.AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
            orderinfoEntity.ADStatsBack = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ADStatsBack"].ToString())) ? null : ViewState["ADStatsBack"].ToString();
            //orderinfoEntity.FaxStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["FaxStatus"].ToString())) ? null : ViewState["FaxStatus"].ToString();
            orderinfoEntity.SqlType = "3";
        }


        //orderinfoEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        //orderinfoEntity.StartDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStartDate"].ToString())) ? null : ViewState["OutStartDate"].ToString();
        //orderinfoEntity.EndDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEndDate"].ToString())) ? null : ViewState["OutEndDate"].ToString();
        //orderinfoEntity.OrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        //orderinfoEntity.AuditStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["AuditStatus"].ToString())) ? null : ViewState["AuditStatus"].ToString();
        //orderinfoEntity.ADStatsBack = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ADStatsBack"].ToString())) ? null : ViewState["ADStatsBack"].ToString();

        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderInfoBP.BindOrderApproveFaxList(_orderInfoEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            dvGridMessage.InnerHtml = "无审核订单";
        }
        else
        {
            dvGridMessage.InnerHtml = "";
            if (dsResult.Tables[0].Select("ORDERST='未审核'").Length == 0)
            {
                for (int i = 0; i < gridHotelList.Rows.Count; i++)
                {
                    //首先判断是否是数据行
                    if (gridHotelList.Rows[i].RowType == DataControlRowType.DataRow && gridHotelList.DataKeys[i].Values[0].ToString().Equals(orderinfoEntity.HotelID))
                    {
                        gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");

                        gridHotelList.Rows[i].Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';");
                        gridHotelList.Rows[i].Attributes.Add("onMouseOut", "this.style.backgroundColor=t;");
                        //gridHotelList.Rows[i].Cells[0].Attributes.Add("bgcolor", "#70A88C");
                        //gridHotelList.Rows[i].Cells[1].Attributes.Add("bgcolor", "#70A88C");

                        //gridHotelList.Rows[i].Cells[0].Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';c=this.parentNode.parentNode.childNodes[" + i + "].childNodes[1].style.backgroundColor;this.parentNode.parentNode.childNodes[" + i + "].childNodes[1].style.backgroundColor='#ebebce';");
                        //gridHotelList.Rows[i].Cells[0].Attributes.Add("onMouseOut", "this.style.backgroundColor=t;this.parentNode.parentNode.childNodes[" + i + "].childNodes[1].style.backgroundColor=c;");

                        //gridHotelList.Rows[i].Cells[1].Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';c=this.parentNode.parentNode.childNodes[" + i + "].childNodes[0].style.backgroundColor;this.parentNode.parentNode.childNodes[" + i + "].childNodes[0].style.backgroundColor='#ebebce';");
                        //gridHotelList.Rows[i].Cells[1].Attributes.Add("onMouseOut", "this.style.backgroundColor=t;this.parentNode.parentNode.childNodes[" + i + "].childNodes[0].style.backgroundColor=c;");
                    }
                }
            }
        }
        gridViewCSList.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "ORDERID" };//主键
        gridViewCSList.DataBind();
    }

    protected void gridViewCSList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        txtActionID.Text = "";
        txtBOOK_REMARK.Text = "";
        string strOrderID = e.CommandArgument.ToString();
        //string strActionID = "";
        trAction.Style.Add("display", "none");
        trNs.Style.Add("display", "none");
        if (e.CommandName == "quest")
        {
            //CreateIssue(strOrderID);
            return;
        }
        else if (e.CommandName == "leave")
        {
            lbAproLable.Text = "已离店";
            trAction.Style.Add("display", "");
            hidActionType.Value = "8";
            strOrderID = e.CommandArgument.ToString().Contains("_") ? e.CommandArgument.ToString().Split('_')[0].ToString() : e.CommandArgument.ToString();
            //strActionID = e.CommandArgument.ToString().Contains("_") ? e.CommandArgument.ToString().Split('_')[1].ToString() : e.CommandArgument.ToString();
            //UpdateAction(strOrderID, "8");
        }
        else if (e.CommandName == "noshow")
        {
            lbAproLable.Text = "No-Show";
            hidActionType.Value = "5";
            trNs.Style.Add("display", "");
            //UpdateAction(strOrderID, "5");
        }
        else if (e.CommandName == "remark")
        {
            lbAproLable.Text = "备注";
            hidActionType.Value = "";
            trAction.Style.Add("display", "none");
            trNs.Style.Add("display", "none");
            //hidLogKey.Value = strOrderID;
            //hidOrderID.Value = (strOrderID.Split('-').Length > 0) ? strOrderID.Split('-')[1].ToString() : strOrderID;
            //lbMemo1.Text = SetMemoVal(strOrderID);
            //ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "setScript", "invokeOpenlist()", true);
        }

        hidOrderID.Value = strOrderID;
        //lbMemo1.Text = SetMemoVal(strOrderID);
        txtBOOK_REMARK.Text = "";
        txtActionID.Text = "";// strActionID;
        txtInRoomID.Text = "";
        ddpNoShow.SelectedIndex = 0;
        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    protected void gridViewCSList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int j = 0; j < e.Row.Cells.Count; j++)
            {
                e.Row.Cells[j].Attributes.Add("style", e.Row.Cells[j].Attributes["style"] + "top: expression(this.offsetParent.scrollTop); z-index: 666;");
            }
        }
    }

    [WebMethod]
    public static string SetMemoVal(string strKey)
    {
        StringBuilder sbMemo = new StringBuilder();
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.MemoKey = strKey;

        DataSet dsResult = LmSystemLogBP.GetOrderActionHisList(_lmSystemLogEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                if ("订单确认".Equals(drRow["EVENT_TYPE"].ToString()))
                {
                    if (String.IsNullOrEmpty(drRow["CANNEL"].ToString()) && !"取消单".Equals(drRow["OD_STATUS"].ToString()))
                    {
                        sbMemo.Append(GetMenoHtml(0, drRow));
                    }
                    else
                    {
                        sbMemo.Append(GetMenoHtml(1, drRow));
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(drRow["CANNEL"].ToString()))
                    {
                        sbMemo.Append(GetMenoHtml(2, drRow));
                    }
                    else
                    {
                        sbMemo.Append(GetMenoHtml(3, drRow));
                    }
                }
            }
        }

        return sbMemo.ToString();
    }

    private static string GetMenoHtml(int iType, DataRow drRow)
    {
        string strMeno = "";

        switch (iType)
        {
            case 0:
                strMeno = String.Format("<table style='width:97%;background-color:#DCDCDC' cellpadding='3'><tr><td align='left'><b><font size='3px'>{0}<font /></b></td><td></td><td></td><td align='right' style='margin-right:10px;'>{1} 操作于：{2}</td></tr><tr><td align='left' style='width:100px'>确认号：</td><td align='left' style='width:100px'>{3}</td><td align='right' style='width:100px'></td><td align='left' style='width:300px'></td></tr><tr><td align='left'>备注信息：</td><td colspan='3' align='left'>{4}</td></tr></table><br />", drRow["OD_STATUS"].ToString(), drRow["EVENTUSER"].ToString(), drRow["EVENTTIME"].ToString(), drRow["ACTION_ID"].ToString(), drRow["REMARK"].ToString());
                break;
            case 1://<td align='right' style='width:100px'></td><td align='left' style='width:200px'></td>
                strMeno = String.Format("<table style='width:97%;background-color:#DCDCDC' cellpadding='3'><tr><td align='left'><b><font size='3px'>{0}<font /></b></td><td></td><td></td><td align='right' style='margin-right:10px;'>{1} 操作于：{2}</td></tr><tr><td align='left' style='width:100px'>取消原因：</td><td align='left' style='width:500px' colspan='3'>{3}</td></tr><tr><td align='left'>备注信息：</td><td colspan='3' align='left'>{4}</td></tr></table><br />", drRow["OD_STATUS"].ToString(), drRow["EVENTUSER"].ToString(), drRow["EVENTTIME"].ToString(), GetCancleReason(drRow["CANNEL"].ToString()), drRow["REMARK"].ToString());
                break;
            case 2:
                strMeno = String.Format("<table style='width:97%;background-color:#DCDCDC' cellpadding='3'><tr><td align='left'><b><font size='3px'>{0}<font /></b></td><td></td><td></td><td align='right' style='margin-right:10px;'>{1} 操作于：{2}</td></tr><tr><td align='left' style='width:100px'>入住房号：</td><td align='left' style='width:100px'>{3}</td><td align='right' style='width:100px'>确认号：</td><td align='left' style='width:300px'>{4}</td></tr><tr><td align='left'>备注信息：</td><td colspan='3' align='left'>{5}</td></tr></table><br />", drRow["OD_STATUS"].ToString(), drRow["EVENTUSER"].ToString(), drRow["EVENTTIME"].ToString(), drRow["ROOM_ID"].ToString(), drRow["APPROVE_ID"].ToString(), drRow["REMARK"].ToString());
                break;
            case 3://<td align='right' style='width:100px'></td><td align='left' style='width:200px'></td>
                strMeno = String.Format("<table style='width:97%;background-color:#DCDCDC' cellpadding='3'><tr><td align='left' style='width:120px;'><b><font size='3px'>{0}<font /></b></td><td></td><td></td><td align='right' style='margin-right:10px;'>{1} 操作于：{2}</td></tr><tr><td align='left' style='width:100px'>NS原因：</td><td align='left' style='width:500px' colspan='3'>{3}</td></tr><tr><td align='left'>备注信息：</td><td colspan='3' align='left'>{4}</td></tr></table><br />", drRow["OD_STATUS"].ToString() + (("1".Equals(drRow["ISDBAPPROVE"].ToString())) ? "(复)" : ""), drRow["EVENTUSER"].ToString(), drRow["EVENTTIME"].ToString(), drRow["CANNEL"].ToString(), drRow["REMARK"].ToString());
                break;
            default:
                strMeno = "";
                break;
        }

        return strMeno;
    }

    private static string GetCancleReason(string reasonCode)
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

        reasonName = htReason.ContainsKey(reasonCode) ? htReason[reasonCode].ToString() : reasonCode;
        return reasonName;
    }

    private string SetActionTypeVal(string strType)
    {
        if ("5".Equals(strType))
        {
            return "No-Show";
        }
        else if ("8".Equals(strType))
        {
            return "离店";
        }
        else
        {
            return "备注";
        }
    }

    protected void btnLoadOrderList_Click(object sender, EventArgs e)
    {
        gridHotelList.SelectedIndex = int.Parse(this.HidSelIndex.Value);
        string PROP = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[0].ToString();

        string PROPNM = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[3].ToString();
        lbHotelNM.Text = "[" + PROP + "]" + PROPNM;

        ViewState["HotelID"] = PROP;
        BindOrderConfirmListGrid();
        string sjs = "GetResultFromServer();";

        int UADCUN = 0;
        int ADCUN = 0;
        int BCUN = 0;
        for (int i = 0; i < gridHotelList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridHotelList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                UADCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[4].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[4].ToString());
                ADCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[5].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[5].ToString());
                BCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[6].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[6].ToString());

                if (UADCUN == ADCUN)
                {
                    gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
                else if (BCUN > 0)
                {
                    gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#94FB92");
                }
                else
                {
                    gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                }
                gridHotelList.Rows[i].Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';");
                gridHotelList.Rows[i].Attributes.Add("onMouseOut", "this.style.backgroundColor=t;");
            }
        }

        gridHotelList.Rows[gridHotelList.SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
        ScriptManager.RegisterClientScriptBlock(this.gridHotelList, this.GetType(), "", sjs, true);
    }

    protected void btnAddRemark_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtBOOK_REMARK.Text.Trim()) && (StringUtility.Text_Length(txtBOOK_REMARK.Text.ToString().Trim()) > 250))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorRemark").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        if ("8".Equals(hidActionType.Value) && String.IsNullOrEmpty(txtInRoomID.Text.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorAction").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "setScript", "invokeOpenList()", true);
            return;
        }

        if (String.IsNullOrEmpty(hidActionType.Value))
        {
            if (String.IsNullOrEmpty(txtBOOK_REMARK.Text.Trim()))
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("EmpryRemark").ToString();
                ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "setScript", "invokeOpenList()", true);
                return;
            }

            OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
            _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
            OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
            orderinfoEntity.EventType = "订单审核";
            orderinfoEntity.ORDER_NUM = hidOrderID.Value;
            orderinfoEntity.OdStatus = SetActionTypeVal(hidActionType.Value);
            orderinfoEntity.REMARK = txtBOOK_REMARK.Text.Trim();
            orderinfoEntity.CANNEL = "";
            orderinfoEntity.ApproveId = "";
            orderinfoEntity.InRoomID = "";
            orderinfoEntity.IsDbApprove = "0";
            _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
            CommonBP.InsertOrderActionHistoryList(_orderInfoEntity);
            messageContent.InnerHtml = GetLocalResourceObject("RemarkSuccess").ToString();
        }
        else
        {
            LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
            _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            _lmSystemLogEntity.FogOrderID = hidOrderID.Value;
            _lmSystemLogEntity.OrderBookStatus = hidActionType.Value;
            _lmSystemLogEntity.CanelReson = ddpNoShow.SelectedValue;
            _lmSystemLogEntity.BookRemark = txtBOOK_REMARK.Text.Trim();
            _lmSystemLogEntity.ApproveId = txtActionID.Text.Trim();
            _lmSystemLogEntity.InRoomID = txtInRoomID.Text.Trim();
            _lmSystemLogEntity.IsDbApprove = "0";// (chkDbApprove.Checked) ? "1" : "0";
            _lmSystemLogEntity = LmSystemLogBP.SaveOrderOpeRemarkList(_lmSystemLogEntity);
            int iResult = _lmSystemLogEntity.Result;
            _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            commonDBEntity.Event_Type = "订单审核-保存";
            commonDBEntity.Event_ID = hidOrderID.Value;
            string conTent = GetLocalResourceObject("EventSaveMessage").ToString();

            conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.OrderBookStatus, _lmSystemLogEntity.BookRemark, _lmSystemLogEntity.InRoomID, _lmSystemLogEntity.ApproveId, _lmSystemLogEntity.CanelReson);
            commonDBEntity.Event_Content = conTent;

            if (iResult == 1)
            {
                OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
                _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
                _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

                _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
                OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
                orderinfoEntity.EventType = "订单审核";
                orderinfoEntity.ORDER_NUM = hidOrderID.Value;
                orderinfoEntity.OdStatus = SetActionTypeVal(hidActionType.Value);
                orderinfoEntity.REMARK = txtBOOK_REMARK.Text.Trim();
                orderinfoEntity.CANNEL = ("8".Equals(hidActionType.Value)) ? "" : ddpNoShow.SelectedValue;
                orderinfoEntity.ApproveId = ("5".Equals(hidActionType.Value)) ? "" : txtActionID.Text.Trim();
                orderinfoEntity.InRoomID = ("5".Equals(hidActionType.Value)) ? "" : txtInRoomID.Text.Trim();
                orderinfoEntity.IsDbApprove = "0";//(chkDbApprove.Checked) ? "1" : "0";
                _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
                CommonBP.InsertOrderActionHistoryList(_orderInfoEntity);

                string strMsg = "";
                if ("5".Equals(hidActionType.Value))
                {
                    strMsg = GetLocalResourceObject("UpdateSuccess12").ToString();
                }
                else if ("8".Equals(hidActionType.Value))
                {
                    strMsg = GetLocalResourceObject("UpdateSuccess11").ToString();
                }

                commonDBEntity.Event_Result = strMsg;
                messageContent.InnerHtml = strMsg;
                ResetOrderConfirmListGrid();
            }
            else if (iResult == 0)
            {
                commonDBEntity.Event_Result = GetLocalResourceObject("WarningMessage").ToString();
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            }
            else if (iResult == 2)
            {
                commonDBEntity.Event_Result = _lmSystemLogEntity.ErrorMSG;
                detailMessageContent.InnerHtml = _lmSystemLogEntity.ErrorMSG;
                ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "setScript", "invokeOpenList()", true);
                return;
            }
            else
            {
                commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
                messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
            }
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }

        gridHotelList.SelectedIndex = int.Parse(this.HidSelIndex.Value);
        string PROP = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[0].ToString();

        int UADCUN = 0;
        int ADCUN = 0;
        int BCUN = 0;
        for (int i = 0; i < gridHotelList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridHotelList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                UADCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[4].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[4].ToString());
                ADCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[5].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[5].ToString());
                BCUN = (String.IsNullOrEmpty(gridHotelList.DataKeys[i].Values[6].ToString())) ? 0 : int.Parse(gridHotelList.DataKeys[i].Values[6].ToString());

                if (UADCUN == ADCUN)
                {
                    gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
                else if (BCUN > 0)
                {
                    gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#94FB92");
                }
                gridHotelList.Rows[i].Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';");
                gridHotelList.Rows[i].Attributes.Add("onMouseOut", "this.style.backgroundColor=t;");
            }
        }

        gridHotelList.Rows[gridHotelList.SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");

        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "setScript", "invokeCloseList()", true);
    }

    protected void btnReLoad_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        if (gridHotelList.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        this.HidSelIndex.Value = String.IsNullOrEmpty(HidSelIndex.Value) ? "-1" : HidSelIndex.Value;

        string selectIndex = this.HidSelIndex.Value;//当前选中酒店 
        string HidLastOrNextByHotel = this.HidLastOrNextByHotel.Value;//上一个 还是 下一个 
        if (HidLastOrNextByHotel == "1")
        {
            this.HidScrollValue.Value = "30";
        }
        else
        {
            this.HidScrollValue.Value = "-30";
        }
        int Index = int.Parse(selectIndex) + (int.Parse(HidLastOrNextByHotel));

        if (Index != -1 && Index < gridHotelList.Rows.Count)
        {
            this.HidSelIndex.Value = (Index == -2) ? "0" : Index.ToString();
            btnLoadOrderList_Click(null, null);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyalert", "alert('" + HidLastOrNextByHotel.Replace("-1", "无上一个！").Replace("1", "无下一个！") + "');BtnCompleteStyle();", true);
        }
    }
}