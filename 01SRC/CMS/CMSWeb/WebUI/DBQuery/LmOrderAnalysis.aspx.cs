using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Web.Script.Serialization;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class LmOrderAnalysis : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();
    public string chartData = string.Empty;
    public string chartName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControlData();
            ViewState["OrderID"] = "";
            ViewState["Mobile"] = "";
            ViewState["CityID"] = "";
            ViewState["HotelID"] = "";
            ViewState["StartDTime"] = "";// DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00";
            ViewState["EndDTime"] = "";// DateTime.Now.ToShortDateString() + " 23:59:59";
            ViewState["InStart"] = "";
            ViewState["InEnd"] = "";
            ViewState["PayCode"] = "";
            ViewState["HotelComfirm"] = "";
            ViewState["BookStatus"] = "";
            ViewState["PayStatus"] = "";
            ViewState["Aprove"] = "";
            ViewState["Ticket"] = "";
            ViewState["PlatForm"] = "";

            //if (!String.IsNullOrEmpty(Request.QueryString["PC"]))
            //{
            //    string strPayCode = Request.QueryString["PC"].ToString();

            //    if ("ALL".Equals(strPayCode))
            //    {
            //        ViewState["PayCode"] = "";

            //        ViewState["StartDTime"] = Request.QueryString["DT"].ToString() + " 00:00:00";
            //        ViewState["EndDTime"] = Request.QueryString["DT"].ToString() + " 23:59:59";
            //    }
            //    else if ("BARC".Equals(strPayCode))
            //    {
            //        //string strDateType = Request.QueryString["DY"].ToString();
            //        ViewState["PayCode"] = "BAR,BARB,";
            //        if (!String.IsNullOrEmpty(Request.QueryString["PF"]))
            //        {
            //            ViewState["PlatForm"] = Request.QueryString["PF"].ToString();
            //        }
            //        ViewState["StartDTime"] = Request.QueryString["DT"].ToString() + " 04:00:00";
            //        ViewState["EndDTime"] = GetDateTimeQuery(Request.QueryString["DT"].ToString()) + " 04:00:00";
            //        ViewState["InStart"] = Request.QueryString["DT"].ToString() + " 00:00:00";
            //        ViewState["InEnd"] = Request.QueryString["DT"].ToString() + " 23:59:59";
            //        //if ("0".Equals(strDateType))
            //        //{

            //        //}
            //        //else
            //        //{
            //        //    ViewState["StartDTime"] = Request.QueryString["DT"].ToString() + " 04:00:00";
            //        //    ViewState["EndDTime"] = Request.QueryString["DT"].ToString() + 1 + " 04:00:00";
            //        //    ViewState["InStart"] = Request.QueryString["DT"].ToString() + " 00:00:00";
            //        //    ViewState["InEnd"] = Request.QueryString["DT"].ToString() + " 23:59:59";
            //        //}
            //    }
            //    else
            //    {
            //        ViewState["PayCode"] = strPayCode + ",";
            //        if (!String.IsNullOrEmpty(Request.QueryString["PF"]))
            //        {
            //            ViewState["PlatForm"] = Request.QueryString["PF"].ToString();
            //        }

            //        ViewState["StartDTime"] = Request.QueryString["DT"].ToString() + " 00:00:00";
            //        ViewState["EndDTime"] = Request.QueryString["DT"].ToString() + " 23:59:59";
            //    }

            //    ViewState["SelType"] = "1";
            //}

            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);

            // 正确的属性设置方法
            //gridViewCSReviewLmSystemLogList.Attributes.Add("SortExpression", "CREATETIME");
            //gridViewCSReviewLmSystemLogList.Attributes.Add("SortDirection", "DESC");

            //BindReviewLmSystemLogListGrid();
            /*king*/
            //BindReviewLmSystemLogListGrid(1, gridViewCSReviewLmSystemLogList.PageSize);

            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
        //messageContent.InnerHtml = "";
    }

    private string GetDateTimeQuery(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return "";
        }

        try
        {
            DateTime dtTime = DateTime.Parse(param);
            return dtTime.AddDays(1).ToShortDateString();
        }
        catch
        {
            return "";
        }
    }

    private void InitControlData()
    {
        BindDDpList();
    }

    private void BindDDpList()
    {
        DataTable dtBook = GetBookDataTable();
        ddpBookStatus.DataSource = dtBook;
        ddpBookStatus.DataTextField = "BOOK_STATUS_TEXT";
        ddpBookStatus.DataValueField = "BOOK_STATUS";
        ddpBookStatus.DataBind();
        ddpBookStatus.SelectedIndex = 0;

        DataTable dtPay = GetPayDataTable();
        ddpPayStatus.DataSource = dtPay;
        ddpPayStatus.DataTextField = "PAY_STATUS_TEXT";
        ddpPayStatus.DataValueField = "PAY_STATUS";
        ddpPayStatus.DataBind();
        ddpPayStatus.SelectedIndex = 0;

        DataTable dtPayCode = GetPayCodeDataTable();
        chkPayCode.DataSource = dtPayCode;
        chkPayCode.DataTextField = "PAY_CODE_TEXT";
        chkPayCode.DataValueField = "PAY_CODE";
        chkPayCode.DataBind();

        DataTable dtTicket = GetTicketDataTable();
        ddpTicket.DataSource = dtTicket;
        ddpTicket.DataTextField = "TICKET_CODE_TEXT";
        ddpTicket.DataValueField = "TICKET_CODE";
        ddpTicket.DataBind();
        ddpTicket.SelectedIndex = 0;

        DataTable dtAprove = GetAproveDataTable();
        ddpAprove.DataSource = dtAprove;
        ddpAprove.DataTextField = "APROVE_STATUS_TEXT";
        ddpAprove.DataValueField = "APROVE_STATUS";
        ddpAprove.DataBind();
        ddpAprove.SelectedIndex = 0;

        DataTable dtHotelComfirm = GetHotelComfirmDataTable();
        ddpHotelComfirm.DataSource = dtHotelComfirm;
        ddpHotelComfirm.DataTextField = "COMFIRM_STATUS_TEXT";
        ddpHotelComfirm.DataValueField = "COMFIRM_STATUS";
        ddpHotelComfirm.DataBind();
        ddpHotelComfirm.SelectedIndex = 0;

        GetPlatFormDataTable();

        DataTable dtTimeSpace = GetTimeSpaceDataTable();
        ddpTimeSpace.DataSource = dtTimeSpace;
        ddpTimeSpace.DataTextField = "TIMESPACE_TEXT";
        ddpTimeSpace.DataValueField = "TIMESPACE_STATUS";
        ddpTimeSpace.DataBind();
        ddpTimeSpace.SelectedIndex = 0;
    }

    private DataTable GetHotelComfirmDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("COMFIRM_STATUS");
        DataColumn BookStatusText = new DataColumn("COMFIRM_STATUS_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["COMFIRM_STATUS"] = "";
        dr0["COMFIRM_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            dr["COMFIRM_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["COMFIRM_STATUS_TEXT"] = "待酒店确认";
                    break;
                case "1":
                    dr["COMFIRM_STATUS_TEXT"] = "酒店已确认";
                    break;
                default:
                    dr["COMFIRM_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetAproveDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("APROVE_STATUS");
        DataColumn BookStatusText = new DataColumn("APROVE_STATUS_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["APROVE_STATUS"] = "";
        dr0["APROVE_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 4; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["APROVE_STATUS"] = "0";
                    dr["APROVE_STATUS_TEXT"] = "未审核";
                    break;
                case "1":
                    dr["APROVE_STATUS"] = "7";
                    dr["APROVE_STATUS_TEXT"] = "入住中";
                    break;
                case "2":
                    dr["APROVE_STATUS"] = "8";
                    dr["APROVE_STATUS_TEXT"] = "已离店";
                    break;
                case "3":
                    dr["APROVE_STATUS"] = "5";
                    dr["APROVE_STATUS_TEXT"] = "No-Show";
                    break;
                default:
                    dr["APROVE_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetPayCodeDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("PAY_CODE");
        DataColumn BookStatusText = new DataColumn("PAY_CODE_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        //DataRow dr0 = dt.NewRow();
        //dr0["PAY_CODE"] = "";
        //dr0["PAY_CODE_TEXT"] = "不限制";
        //dt.Rows.Add(dr0);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["PAY_CODE"] = "LMBAR";
                    dr["PAY_CODE_TEXT"] = "LMBAR";
                    break;
                case "1":
                    dr["PAY_CODE"] = "LMBAR2";
                    dr["PAY_CODE_TEXT"] = "LMBAR2";
                    break;
                case "2":
                    dr["PAY_CODE"] = "BAR/BARB";
                    dr["PAY_CODE_TEXT"] = "BAR/BARB";
                    break;
                default:
                    dr["PAY_CODE"] = "";
                    dr["PAY_CODE_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetTicketDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("TICKET_CODE");
        DataColumn BookStatusText = new DataColumn("TICKET_CODE_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["TICKET_CODE"] = "";
        dr0["TICKET_CODE_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            dr["TICKET_CODE"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["TICKET_CODE_TEXT"] = "未使用优惠券";
                    break;
                case "1":
                    dr["TICKET_CODE_TEXT"] = "使用优惠券";
                    break;
                default:
                    dr["BOOK_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetBookDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("BOOK_STATUS");
        DataColumn BookStatusText = new DataColumn("BOOK_STATUS_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["BOOK_STATUS"] = "";
        dr0["BOOK_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 4; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["BOOK_STATUS"] = "n";
                    dr["BOOK_STATUS_TEXT"] = "新单";
                    break;
                case "1":
                    dr["BOOK_STATUS"] = "CC";
                    dr["BOOK_STATUS_TEXT"] = "用户取消单";
                    break;
                case "2":
                    dr["BOOK_STATUS"] = "e";
                    dr["BOOK_STATUS_TEXT"] = "CC修改单";
                    break;
                case "3":
                    dr["BOOK_STATUS"] = "c";
                    dr["BOOK_STATUS_TEXT"] = "CC取消单";
                    break;
                default:
                    dr["BOOK_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetPayDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn PayStatus = new DataColumn("PAY_STATUS");
        DataColumn PayStatusText = new DataColumn("PAY_STATUS_TEXT");

        dt.Columns.Add(PayStatus);
        dt.Columns.Add(PayStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["PAY_STATUS"] = "";
        dr0["PAY_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);
        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            dr["PAY_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["PAY_STATUS_TEXT"] = "未支付成功";
                    break;
                case "1":
                    dr["PAY_STATUS_TEXT"] = "已支付成功";
                    break;
                default:
                    dr["PAY_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void GetPlatFormDataTable()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsPlatForm = LmSystemLogBP.PlatFormSelect(_lmSystemLogEntity).QueryResult;

        DataRow drTempcity = dsPlatForm.Tables[0].NewRow();
        drTempcity["PLATFORMCODE"] = DBNull.Value;
        drTempcity["PLATFORMNAME"] = "不限制";
        dsPlatForm.Tables[0].Rows.InsertAt(drTempcity, 0);

        ddpPlatForm.DataTextField = "PLATFORMNAME";
        ddpPlatForm.DataValueField = "PLATFORMCODE";
        ddpPlatForm.DataSource = dsPlatForm;
        ddpPlatForm.DataBind();
        ddpPlatForm.SelectedIndex = 0;
    }

    private DataTable GetTimeSpaceDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("TIMESPACE_STATUS");
        DataColumn BookStatusText = new DataColumn("TIMESPACE_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["TIMESPACE_STATUS"] = "1";
                    dr["TIMESPACE_TEXT"] = "每小时";
                    break;
                case "1":
                     dr["TIMESPACE_STATUS"] = "24";
                    dr["TIMESPACE_TEXT"] = "每天";
                    break;
                case "2":
                      dr["TIMESPACE_STATUS"] = "168";
                    dr["TIMESPACE_TEXT"] = "每周";
                    break;
                //case "3":
                //      dr["TIMESPACE_STATUS"] = "M";
                //    dr["TIMESPACE_TEXT"] = "每月";
                //    break;
                default:
                    dr["BOOK_STATUS_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void BindReviewLmSystemLogListGrid()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("ID");
        dtResult.Columns.Add("QueryNm");
        dtResult.Columns.Add("OrderID");
        dtResult.Columns.Add("Mobile");
        dtResult.Columns.Add("CityID");
        dtResult.Columns.Add("HotelID");
        dtResult.Columns.Add("StartDTime");
        dtResult.Columns.Add("EndDTime");
        dtResult.Columns.Add("InStart");
        dtResult.Columns.Add("InEnd");
        dtResult.Columns.Add("PayCode");
        dtResult.Columns.Add("BookStatus");
        dtResult.Columns.Add("HotelComfirm");
        dtResult.Columns.Add("PayStatus");
        dtResult.Columns.Add("Aprove");
        dtResult.Columns.Add("Ticket");
        dtResult.Columns.Add("PlatForm");

        for (int i = 0; i < this.gridViewCSReviewLmSystemLogList.Rows.Count; i++)
        {
            DataRow drRow = dtResult.NewRow();
            drRow["ID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[0].Text;
            drRow["QueryNm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[1].Text;
            drRow["OrderID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[2].Text;
            drRow["Mobile"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[3].Text;
            drRow["CityID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[4].Text;
            drRow["HotelID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[5].Text;
            drRow["StartDTime"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[6].Text;
            drRow["EndDTime"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[7].Text;
            drRow["InStart"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[8].Text;
            drRow["InEnd"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[9].Text;
            drRow["PayCode"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[10].Text;
            drRow["BookStatus"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[11].Text;
            drRow["HotelComfirm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[12].Text;
            drRow["PayStatus"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[13].Text;
            drRow["Aprove"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[14].Text;
            drRow["Ticket"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[15].Text;
            drRow["PlatForm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[16].Text;
            dtResult.Rows.Add(drRow);
        }

        DataRow drNewRow = dtResult.NewRow();
        drNewRow["ID"] = gridViewCSReviewLmSystemLogList.Rows.Count + 1;
        drNewRow["QueryNm"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["QueryNm"].ToString())) ? null : ViewState["QueryNm"].ToString();
        drNewRow["OrderID"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        drNewRow["Mobile"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
        drNewRow["CityID"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        drNewRow["HotelID"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        drNewRow["StartDTime"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        drNewRow["EndDTime"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        drNewRow["InStart"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
        drNewRow["InEnd"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();
        drNewRow["PayCode"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
        drNewRow["BookStatus"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
        drNewRow["HotelComfirm"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();
        drNewRow["PayStatus"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
        drNewRow["Aprove"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
        drNewRow["Ticket"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
        drNewRow["PlatForm"] = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
        
        dtResult.Rows.Add(drNewRow);
        gridViewCSReviewLmSystemLogList.DataSource = dtResult.DefaultView;
        gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSReviewLmSystemLogList.DataBind();

        if (dtResult.Rows.Count > 0)
        {
            gridViewCSReviewLmSystemLogList.HeaderRow.Visible = false;
        }
    }

    protected void gridViewCSReviewLmSystemLogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //    //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //    //BindGridView();

        //    //执行循环，保证每条数据都可以更新
        //    for (int i = 0; i <= gridViewCSChannelList.Rows.Count; i++)
        //    {
        //        //首先判断是否是数据行
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            //当鼠标停留时更改背景色
        //            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
        //            //当鼠标移开时还原背景色
        //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //        }
        //    }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        string strRowID = hidRowID.Value;

        for (int i = 0; i < gridViewCSReviewLmSystemLogList.Rows.Count; i++)
        {
            if (!strRowID.Equals(gridViewCSReviewLmSystemLogList.Rows[i].Cells[0].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "")))
            {
                continue;
            }
            txtQueryNm.Value = gridViewCSReviewLmSystemLogList.Rows[i].Cells[1].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            txtOrderID.Value = gridViewCSReviewLmSystemLogList.Rows[i].Cells[2].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            txtMobile.Value = gridViewCSReviewLmSystemLogList.Rows[i].Cells[3].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            wctCity.AutoResult = gridViewCSReviewLmSystemLogList.Rows[i].Cells[4].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            wctHotel.AutoResult = gridViewCSReviewLmSystemLogList.Rows[i].Cells[5].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            dpCreateStart.Value = gridViewCSReviewLmSystemLogList.Rows[i].Cells[6].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            dpCreateEnd.Value = gridViewCSReviewLmSystemLogList.Rows[i].Cells[7].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            dpInStart.Value = gridViewCSReviewLmSystemLogList.Rows[i].Cells[8].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            dpInEnd.Value = gridViewCSReviewLmSystemLogList.Rows[i].Cells[9].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");

            string PayCode = gridViewCSReviewLmSystemLogList.Rows[i].Cells[10].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            string[] payCodeList = PayCode.Split(',');
            ArrayList chkList = new ArrayList();
            foreach (string strTemp in payCodeList)
            {
                if (!String.IsNullOrEmpty(strTemp.Trim()))
                {
                    chkList.Add(strTemp);
                }
            }

            if (chkList.Count == 0)
            {
                chkAllCommon.Checked = true;
                foreach (ListItem li in chkPayCode.Items)
                {
                    li.Enabled = false;
                }
            }
            else
            {
                foreach (ListItem li in chkPayCode.Items)
                {
                    if (chkList.Contains(li.Value))
                    {
                        li.Selected = true;
                    }
                    li.Enabled = true;
                }
                chkAllCommon.Checked = false;
            }
            ddpBookStatus.SelectedValue = gridViewCSReviewLmSystemLogList.Rows[i].Cells[11].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            ddpHotelComfirm.SelectedValue = gridViewCSReviewLmSystemLogList.Rows[i].Cells[12].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            ddpPayStatus.SelectedValue = gridViewCSReviewLmSystemLogList.Rows[i].Cells[13].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            ddpAprove.SelectedValue = gridViewCSReviewLmSystemLogList.Rows[i].Cells[14].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            ddpTicket.SelectedValue = gridViewCSReviewLmSystemLogList.Rows[i].Cells[15].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            ddpPlatForm.SelectedValue = gridViewCSReviewLmSystemLogList.Rows[i].Cells[16].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            dvBtnUpdate.Style.Add("display", "");
            dvBtnInsert.Style.Add("display", "none");
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtQueryNm.Value.Trim()))
        {
            dvBtnUpdate.Style.Add("display", "none");
            dvBtnInsert.Style.Add("display", "");
            messageContent.InnerHtml = GetLocalResourceObject("Warning1Message").ToString();
            return;
        }

        if (String.IsNullOrEmpty(dpCreateStart.Value) || String.IsNullOrEmpty(dpCreateEnd.Value))
        {
            dvBtnUpdate.Style.Add("display", "none");
            dvBtnInsert.Style.Add("display", "");
            messageContent.InnerHtml = GetLocalResourceObject("Error1Message").ToString();
            return;
        }

        string strRowID = hidRowID.Value;
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("ID");
        dtResult.Columns.Add("QueryNm");
        dtResult.Columns.Add("OrderID");
        dtResult.Columns.Add("Mobile");
        dtResult.Columns.Add("CityID");
        dtResult.Columns.Add("HotelID");
        dtResult.Columns.Add("StartDTime");
        dtResult.Columns.Add("EndDTime");
        dtResult.Columns.Add("InStart");
        dtResult.Columns.Add("InEnd");
        dtResult.Columns.Add("PayCode");
        dtResult.Columns.Add("BookStatus");
        dtResult.Columns.Add("HotelComfirm");
        dtResult.Columns.Add("PayStatus");
        dtResult.Columns.Add("Aprove");
        dtResult.Columns.Add("Ticket");
        dtResult.Columns.Add("PlatForm");

        for (int i = 0; i < this.gridViewCSReviewLmSystemLogList.Rows.Count; i++)
        {
            if (strRowID.Equals(gridViewCSReviewLmSystemLogList.Rows[i].Cells[0].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "")))
            {
                DataRow drNewRow = dtResult.NewRow();
                drNewRow["ID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[0].Text;
                string strOrderID = txtOrderID.Value.Trim().Replace('，', ',');
                string strOrderList = "";
                if (strOrderID.IndexOf(',') >= 0)
                {
                    string[] orderList = strOrderID.Split(',');
                    foreach (string strTemp in orderList)
                    {
                        strOrderList = (String.IsNullOrEmpty(strTemp)) ? strOrderList : strOrderList + strTemp + ",";
                    }
                }
                else if (strOrderID.Length > 0)
                {
                    strOrderList = strOrderID + ",";
                }

                ViewState["OrderID"] = strOrderList;
                drNewRow["QueryNm"] = txtQueryNm.Value.Trim();
                drNewRow["OrderID"] = strOrderList;
                drNewRow["Mobile"] = txtMobile.Value.Trim();
                drNewRow["CityID"] = hidCity.Value.ToString().Trim();
                drNewRow["HotelID"] = hidHotel.Value.ToString().Trim();
                drNewRow["StartDTime"] = dpCreateStart.Value;
                drNewRow["EndDTime"] = dpCreateEnd.Value;
                drNewRow["InStart"] = dpInStart.Value;
                drNewRow["InEnd"] = dpInEnd.Value;
                drNewRow["PayCode"] = hidCommonList.Value;
                drNewRow["BookStatus"] = ddpBookStatus.SelectedValue.ToString().Trim();
                drNewRow["HotelComfirm"] = ddpHotelComfirm.SelectedValue.ToString().Trim();
                drNewRow["PayStatus"] = ddpPayStatus.SelectedValue.ToString().Trim();
                drNewRow["Aprove"] = ddpAprove.SelectedValue.ToString().Trim();
                drNewRow["Ticket"] = ddpTicket.SelectedValue.ToString().Trim();
                drNewRow["PlatForm"] = ddpPlatForm.SelectedValue.Trim();

                dtResult.Rows.Add(drNewRow);
                continue;
            }
            DataRow drRow = dtResult.NewRow();
            drRow["ID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[0].Text;
            drRow["QueryNm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[1].Text;
            drRow["OrderID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[2].Text;
            drRow["Mobile"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[3].Text;
            drRow["CityID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[4].Text;
            drRow["HotelID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[5].Text;
            drRow["StartDTime"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[6].Text;
            drRow["EndDTime"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[7].Text;
            drRow["InStart"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[8].Text;
            drRow["InEnd"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[9].Text;
            drRow["PayCode"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[10].Text;
            drRow["BookStatus"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[11].Text;
            drRow["HotelComfirm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[12].Text;
            drRow["PayStatus"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[13].Text;
            drRow["Aprove"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[14].Text;
            drRow["Ticket"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[15].Text;
            drRow["PlatForm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[16].Text;
            dtResult.Rows.Add(drRow);
        }

       
        gridViewCSReviewLmSystemLogList.DataSource = dtResult.DefaultView;
        gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSReviewLmSystemLogList.DataBind();

        if (dtResult.Rows.Count > 0)
        {
            gridViewCSReviewLmSystemLogList.HeaderRow.Visible = false;
        }

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(txtQueryNm.Value.Trim()))
        {
            dvBtnUpdate.Style.Add("display", "none");
            dvBtnInsert.Style.Add("display", "");
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        if (gridViewCSReviewLmSystemLogList.Rows.Count == 10)
        {
            dvBtnUpdate.Style.Add("display", "none");
            dvBtnInsert.Style.Add("display", "");
            messageContent.InnerHtml = GetLocalResourceObject("MaxRowsMessage").ToString();
            return;
        }

        if (String.IsNullOrEmpty(dpCreateStart.Value) || String.IsNullOrEmpty(dpCreateEnd.Value))
        {
            dvBtnUpdate.Style.Add("display", "none");
            dvBtnInsert.Style.Add("display", "");
            messageContent.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            return;
        }

        string strOrderID = txtOrderID.Value.Trim().Replace('，', ',');
        string strOrderList = "";
        if (strOrderID.IndexOf(',') >= 0)
        {
            string[] orderList = strOrderID.Split(',');
            foreach (string strTemp in orderList)
            {
                strOrderList = (String.IsNullOrEmpty(strTemp)) ? strOrderList : strOrderList + strTemp + ",";
            }
        }
        else if (strOrderID.Length > 0)
        {
            strOrderList = strOrderID + ",";
        }

        ViewState["OrderID"] = strOrderList;
        //string strHotel = hidHotel.Value.ToString().Trim(); //wctHotel.AutoResult.ToString().Trim();
        //string strCity = hidCity.Value.ToString().Trim(); //wctCity.AutoResult.ToString().Trim(); 
        //ViewState["HotelID"] = (strHotel.IndexOf(']') > 0) ? strHotel.Substring(0, strHotel.IndexOf(']')).Trim('[').Trim(']') : strHotel;
        //ViewState["CityID"] = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity;

        ViewState["HotelID"] = hidHotel.Value.ToString().Trim();
        ViewState["CityID"] = hidCity.Value.ToString().Trim();

        //chkPayCode
        ViewState["PayCode"] = hidCommonList.Value;
        ViewState["BookStatus"] = ddpBookStatus.SelectedValue.ToString().Trim();
        ViewState["PayStatus"] = ddpPayStatus.SelectedValue.ToString().Trim();
        ViewState["Aprove"] = ddpAprove.SelectedValue.ToString().Trim();
        ViewState["Ticket"] = ddpTicket.SelectedValue.ToString().Trim();
        ViewState["HotelComfirm"] = ddpHotelComfirm.SelectedValue.ToString().Trim();

        ViewState["StartDTime"] = dpCreateStart.Value;
        ViewState["EndDTime"] = dpCreateEnd.Value;
        ViewState["Mobile"] = txtMobile.Value.Trim();
        ViewState["InStart"] = dpInStart.Value;
        ViewState["InEnd"] = dpInEnd.Value;
        ViewState["PlatForm"] = ddpPlatForm.SelectedValue.Trim();
        ViewState["QueryNm"] = txtQueryNm.Value.Trim();

        BindReviewLmSystemLogListGrid();

        txtQueryNm.Value = "";
        wctHotel.AutoResult = "";
        wctCity.AutoResult = "";
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
    }

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gridViewCSReviewLmSystemLogList.Rows.Count == 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("RowsEmptyMessage").ToString();
            return;
        }

        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        chartData = "[";
        chartName = "[";
        DateTime dateTemp;
        for (int i = 0; i < gridViewCSReviewLmSystemLogList.Rows.Count; i++)
        {
            _lmSystemLogEntity.FogOrderID = gridViewCSReviewLmSystemLogList.Rows[i].Cells[2].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.Mobile = gridViewCSReviewLmSystemLogList.Rows[i].Cells[3].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.CityID = gridViewCSReviewLmSystemLogList.Rows[i].Cells[4].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.HotelID = gridViewCSReviewLmSystemLogList.Rows[i].Cells[5].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.StartDTime = gridViewCSReviewLmSystemLogList.Rows[i].Cells[6].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.EndDTime = gridViewCSReviewLmSystemLogList.Rows[i].Cells[7].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.InStart = gridViewCSReviewLmSystemLogList.Rows[i].Cells[8].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.InEnd = gridViewCSReviewLmSystemLogList.Rows[i].Cells[9].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.PayCode = gridViewCSReviewLmSystemLogList.Rows[i].Cells[10].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.BookStatus = gridViewCSReviewLmSystemLogList.Rows[i].Cells[11].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.HotelComfirm = gridViewCSReviewLmSystemLogList.Rows[i].Cells[12].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.PayStatus = gridViewCSReviewLmSystemLogList.Rows[i].Cells[13].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.Aprove = gridViewCSReviewLmSystemLogList.Rows[i].Cells[14].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.Ticket = gridViewCSReviewLmSystemLogList.Rows[i].Cells[15].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.PlatForm = gridViewCSReviewLmSystemLogList.Rows[i].Cells[16].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "");
            _lmSystemLogEntity.ShowType = ddpTimeSpace.SelectedValue.Trim();

            chartName = chartName + "'" + gridViewCSReviewLmSystemLogList.Rows[i].Cells[1].Text.ToString().Trim().Replace("amp;", "").Replace("&nbsp;", "") + "',";

            StringBuilder sb = new StringBuilder();
            string temp = string.Empty;
            DataSet dsResult = LmSystemLogBP.ChartExportLmOrderSelect(_lmSystemLogEntity).QueryResult;
            if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
            {
                dateTemp = DateTime.Parse(_lmSystemLogEntity.StartDTime);
                if (double.Parse(_lmSystemLogEntity.ShowType) > 1)
                {
                    chartData = chartData + "[['" + dateTemp.ToShortDateString() + "', 0]],";
                }
                else
                {
                    chartData = chartData + "[['" + dateTemp.ToString() + "', 0]],";  
                }
                continue;
            }

            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                temp = "['" + drRow["Date"].ToString() + "'," + drRow["Volume"].ToString() + "]";
                sb.Append(temp);
                sb.Append(',');
            }
            chartData = chartData + "["+ sb.ToString().Trim(',') + "],";
        }

        chartData = chartData.Trim(',') + "]";
        chartName = chartName.Trim(',') + "]";
        if (double.Parse(_lmSystemLogEntity.ShowType) > 1)
        {
            hidFormatString.Value = "%#m/%#d/%y";
        }
        else
        {
            hidFormatString.Value = "%Y/%#m/%#d %H:%M:%S";
        }

        //dvBtnUpdate.Style.Add("display", "none");
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DrawingLines();", true);
    }

    protected void gridViewCSReviewLmSystemLogList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       DataTable dtResult = new DataTable();
       dtResult.Columns.Add("ID");
       dtResult.Columns.Add("QueryNm");
       dtResult.Columns.Add("OrderID");
       dtResult.Columns.Add("Mobile");
       dtResult.Columns.Add("CityID");
       dtResult.Columns.Add("HotelID");
       dtResult.Columns.Add("StartDTime");
       dtResult.Columns.Add("EndDTime");
       dtResult.Columns.Add("InStart");
       dtResult.Columns.Add("InEnd");
       dtResult.Columns.Add("PayCode");
       dtResult.Columns.Add("BookStatus");
       dtResult.Columns.Add("HotelComfirm");
       dtResult.Columns.Add("PayStatus");
       dtResult.Columns.Add("Aprove");
       dtResult.Columns.Add("Ticket");
       dtResult.Columns.Add("PlatForm");

       for (int i = 0; i < this.gridViewCSReviewLmSystemLogList.Rows.Count; i++)
       {
           if (i == e.RowIndex)
           {
               continue;
           }

           DataRow drRow = dtResult.NewRow();
           drRow["ID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[0].Text;
           drRow["QueryNm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[1].Text;
           drRow["OrderID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[2].Text;
           drRow["Mobile"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[3].Text;
           drRow["CityID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[4].Text;
           drRow["HotelID"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[5].Text;
           drRow["StartDTime"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[6].Text;
           drRow["EndDTime"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[7].Text;
           drRow["InStart"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[8].Text;
           drRow["InEnd"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[9].Text;
           drRow["PayCode"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[10].Text;
           drRow["BookStatus"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[11].Text;
           drRow["HotelComfirm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[12].Text;
           drRow["PayStatus"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[13].Text;
           drRow["Aprove"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[14].Text;
           drRow["Ticket"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[15].Text;
           drRow["PlatForm"] = this.gridViewCSReviewLmSystemLogList.Rows[i].Cells[16].Text;
           dtResult.Rows.Add(drRow);
       }

       gridViewCSReviewLmSystemLogList.DataSource = dtResult.DefaultView;
       gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "ID" };//主键
       gridViewCSReviewLmSystemLogList.DataBind();

       if (dtResult.Rows.Count > 0)
       {
           gridViewCSReviewLmSystemLogList.HeaderRow.Visible = false;
       }

       dvBtnUpdate.Style.Add("display", "none");
       dvBtnInsert.Style.Add("display", "");
       this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
    }
   
}