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

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class LmOrderLogPage : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();
    //private string orderXPaymentCode="";
    //private string orderInTheNight = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type=hotel") && !this.Page.Request.QueryString.ToString().Contains("Type=city"))
        {
            #region
            InitControlData();
            ViewState["Rests"] = "";
            ViewState["orderXPaymentCode"] = "";
            ViewState["orderInTheNight"] = "";

            ViewState["OrderID"] = "";
            ViewState["Mobile"] = "";
            ViewState["CityID"] = "";
            ViewState["GroupID"] = "";
            ViewState["HotelID"] = "";
            ViewState["StartDTime"] = "";
            ViewState["EndDTime"] = "";
            ViewState["InStart"] = "";
            ViewState["InEnd"] = "";
            ViewState["OutStart"] = "";
            ViewState["OutEnd"] = "";
            ViewState["PayCode"] = "";
            ViewState["HotelComfirm"] = "";
            ViewState["BookStatus"] = "";
            ViewState["PayStatus"] = "";
            ViewState["Aprove"] = "";
            ViewState["Ticket"] = "";
            ViewState["PlatForm"] = "";
            ViewState["Sales"] = "";
            ViewState["TicketType"] = "";
            ViewState["TicketData"] = "";
            ViewState["TicketPcode"] = "";
            ViewState["DashPopStatus"] = "";
            ViewState["DashInStart"] = "";
            ViewState["DashInEnd"] = "";
            ViewState["DashStartDTime"] = "";
            ViewState["DashEndDTime"] = "";
            ViewState["OutTest"] = "";
            ViewState["packagename"] = "";
            ViewState["amountfrom"] = "";
            ViewState["amountto"] = "";
            ViewState["pickfromdate"] = "";
            ViewState["picktodate"] = "";
            ViewState["tickettime"] = "";
            ViewState["OrderChannel"] = "";
            ViewState["OrderTypeStatus"] = "";
            ViewState["OrderBookStatus"] = "";
            ViewState["OrderBookStatusOther"] = "";
            ViewState["RestPriceCode"] = "";
            ViewState["RestBookStatus"] = "";
            ViewState["IsReserve"] = "";
            ViewState["GuestName"] = "";

            ViewState["OutCC"] = "";
            ViewState["OutUC"] = "";
            ViewState["OutFailOrder"] = "";
            #endregion

            #region DashBorad Link   Edit Jason.yu  注释
            //if (!String.IsNullOrEmpty(Request.QueryString["PC"]))
            //{
            //    ViewState["DashInStart"] = Request.QueryString["DT"].ToString() + " 00:00:00";
            //    ViewState["DashInEnd"] = Request.QueryString["DT"].ToString() + " 23:59:59";
            //    ViewState["DashStartDTime"] = Request.QueryString["DT"].ToString() + " 04:00:00";
            //    ViewState["DashEndDTime"] = GetDateTimeQuery(Request.QueryString["DT"].ToString());
            //    ViewState["OutTest"] = "1";
            //    ViewState["DashPopStatus"] = "1";

            //    ViewState["OrderChannel"] = Request.QueryString["OC"].ToString();

            //    string strPayCode = Request.QueryString["PC"].ToString();
            //    if ("ALL".Equals(strPayCode))
            //    {
            //        if (!String.IsNullOrEmpty(Request.QueryString["PF"]) && "1".Equals(Request.QueryString["PF"].ToString().Trim()))
            //        {
            //            ViewState["DashInStart"] = "";
            //            ViewState["DashInEnd"] = "";
            //            ViewState["DashStartDTime"] = "";
            //            ViewState["DashEndDTime"] = "";

            //            ViewState["StartDTime"] = Request.QueryString["DT"].ToString() + " 04:00:00";
            //            ViewState["EndDTime"] = GetDateTimeQuery(Request.QueryString["DT"].ToString());
            //            ViewState["DashPopStatus"] = "";
            //        }

            //        ViewState["PayCode"] = "";
            //    }
            //    else if ("BARC".Equals(strPayCode) || "BAR".Equals(strPayCode))
            //    {
            //        //string strDateType = Request.QueryString["DY"].ToString();
            //        ViewState["PayCode"] = "BAR,BARB,";
            //        if (!String.IsNullOrEmpty(Request.QueryString["PF"]))
            //        {
            //            ViewState["PlatForm"] = Request.QueryString["PF"].ToString();
            //        }
            //    }
            //    else
            //    {
            //        ViewState["PayCode"] = strPayCode + ",";
            //        if (!String.IsNullOrEmpty(Request.QueryString["PF"]))
            //        {
            //            ViewState["PlatForm"] = Request.QueryString["PF"].ToString();
            //        }
            //    }

            //    if ("BARC".Equals(strPayCode))
            //    {
            //        ViewState["InStart"] = Request.QueryString["DT"].ToString() + " 00:00:00";
            //        ViewState["InEnd"] = Request.QueryString["DT"].ToString() + " 23:59:59";
            //    }

            //    if (!String.IsNullOrEmpty(Request.QueryString["TK"]))
            //    {
            //        ViewState["Ticket"] = Request.QueryString["TK"].ToString();
            //    }
            //}
            #endregion

            #region 优惠券 Link
            if (!String.IsNullOrEmpty(Request.QueryString["TYPE"]))
            {
                ViewState["RestBookStatus"] = "10,";
                ViewState["TicketType"] = Request.QueryString["TYPE"].ToString();
                ViewState["TicketData"] = (!String.IsNullOrEmpty(Request.QueryString["DATA"])) ? Request.QueryString["DATA"].ToString() : "";

                string strPayCode = (!String.IsNullOrEmpty(Request.QueryString["PCOD"])) ? Request.QueryString["PCOD"].ToString().Trim() : "";
                if (String.IsNullOrEmpty(strPayCode))
                {
                    ViewState["TicketPcode"] = "";
                }
                else if ("BARB".Equals(strPayCode))
                {
                    ViewState["TicketPcode"] = "BAR,BARB,";
                }
                else
                {
                    ViewState["TicketPcode"] = strPayCode + ",";
                }

                string pkn = HttpUtility.UrlDecode(Request.QueryString["pknm"], Encoding.GetEncoding("GB2312"));
                string atk = HttpUtility.UrlDecode(Request.QueryString["atk"], Encoding.GetEncoding("GB2312"));
                string att = HttpUtility.UrlDecode(Request.QueryString["att"], Encoding.GetEncoding("GB2312"));
                string pkf = HttpUtility.UrlDecode(Request.QueryString["pkf"], Encoding.GetEncoding("GB2312"));
                string pkt = HttpUtility.UrlDecode(Request.QueryString["pkt"], Encoding.GetEncoding("GB2312"));
                string tkt = HttpUtility.UrlDecode(Request.QueryString["tkt"], Encoding.GetEncoding("GB2312"));

                ViewState["packagename"] = pkn;//优惠券礼包名
                ViewState["amountfrom"] = atk;
                ViewState["amountto"] = att;
                ViewState["pickfromdate"] = pkf;
                ViewState["picktodate"] = pkt;
                ViewState["tickettime"] = tkt;
            }
            #endregion

            #region 0 订单  1 渠道
            if (!String.IsNullOrEmpty(Request.QueryString["State"]))
            {
                string State = Request.QueryString["State"].ToString();
                ViewState["OutTest"] = "1";
                if (State == "0")
                {
                    ViewState["RestBookStatus"] = "10,";

                    #region 总订单  （数）
                    string strPayCode = Request.QueryString["PC"] == null ? "" : Request.QueryString["PC"].ToString();
                    if ("ALL".Equals(strPayCode))
                    {
                        ViewState["PayCode"] = "LMBAR,LMBAR2,BAR,BARB,";
                    }
                    else if ("LMBAR".Equals(strPayCode))
                    {
                        ViewState["PayCode"] = "LMBAR,";
                    }
                    else if ("LMBAR2".Equals(strPayCode))
                    {
                        ViewState["PayCode"] = "LMBAR2,";
                    }
                    else
                    {
                        ViewState["PayCode"] = strPayCode + ",";
                    }
                    #endregion

                    #region  当晚入住
                    if (!String.IsNullOrEmpty(Request.QueryString["PF"]))
                    {
                        ViewState["orderInTheNight"] = Request.QueryString["PF"].ToString();
                        if (ViewState["orderInTheNight"].ToString() == "CKIN")
                        {
                            ViewState["PayCode"] = "BAR,BARB,";
                        }
                    }
                    #endregion

                    #region  总订单 (劵)
                    if (!String.IsNullOrEmpty(Request.QueryString["TK"]))
                    {
                        ViewState["Ticket"] = Request.QueryString["TK"].ToString();
                    }
                    #endregion

                    #region  确认  未确认订单劵  cc取消单  用户取消单  其他  OC 在订单统计中 不同意义
                    if (!string.IsNullOrEmpty(Request.QueryString["OC"]))
                    {
                        string str = Request.QueryString["OC"].ToString();
                        if ("LMBAR".Equals(strPayCode))
                        {
                            ViewState["OrderBookStatus"] = str;
                        }
                        else if ("ALL".Equals(strPayCode))
                        {
                            string[] strStatus = str.Split('|');
                            ViewState["OrderBookStatus"] = strStatus[1].ToString() + ",";
                            ViewState["OrderBookStatusOther"] = strStatus[0].ToString() + ",";
                            if (ViewState["orderInTheNight"].ToString() == "SUM")
                            {
                                ViewState["orderXPaymentCode"] = "QuFen";
                            }
                        }
                        else
                        {
                            ViewState["OrderBookStatusOther"] = str + ",";
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 渠道   应用平台
                    string strPayCode = Request.QueryString["PC"] == null ? "" : Request.QueryString["PC"].ToString();
                    string[] payCode = strPayCode.Split(','); //HOTELVP    IOS
                    if ("Rests".Equals(payCode[0].ToString()))
                    {
                        ViewState["Rests"] = "Rests";
                        //ViewState["OrderChannel"] = "HOTELVP,QUNAR,116114,MOJI,GETAROOM,HOTELVPPRO,";
                        ViewState["OrderChannel"] = "HOTELVP,GETAROOM,QUNAR,116114,MOJI,HOTELVPPRO,HOTELVPMAP,";
                        ViewState["RestPriceCode"] = "LMBAR";
                    }
                    else if ("ALL".Equals(payCode[0].ToString()))
                    {
                        //需要改动sql    查询字段  需改为  上面一直   以逗号分隔     订单渠道  应用平台
                        //ViewState["OrderChannel"] = "HOTELVP,QUNAR,116114,MOJI,GETAROOM,HOTELVPPRO,";
                        ViewState["OrderChannel"] = "HOTELVP,GETAROOM,QUNAR,116114,MOJI,HOTELVPPRO,HOTELVPMAP,";
                        ViewState["PlatForm"] = "IOS,ANDROID,WP,WAP,";
                        ViewState["RestPriceCode"] = "LMBAR";
                    }
                    else if ("HOTELVP".Equals(payCode[0].ToString()))
                    {
                        #region
                        strPayCode = payCode[1].ToString();
                        if ("IOS".Equals(strPayCode))
                        {
                            ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                            ViewState["PlatForm"] = payCode[1].ToString() + ",";
                            ViewState["RestPriceCode"] = "LMBAR";
                        }
                        else if ("ANDROID".Equals(strPayCode))
                        {
                            ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                            ViewState["PlatForm"] = payCode[1].ToString() + ",";
                            ViewState["RestPriceCode"] = "LMBAR";
                        }
                        else if ("WP".Equals(strPayCode))
                        {
                            ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                            ViewState["PlatForm"] = payCode[1].ToString() + ",";
                            ViewState["RestPriceCode"] = "LMBAR";
                        }
                        else
                        {
                            //WAP
                            ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                            ViewState["PlatForm"] = payCode[1].ToString() + ",";
                            ViewState["RestPriceCode"] = "LMBAR";
                        }
                        #endregion
                    }
                    else
                    {
                        ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                        ViewState["RestPriceCode"] = "LMBAR";
                    }
                    #endregion

                    #region   (劵)
                    if (!String.IsNullOrEmpty(Request.QueryString["TK"]))
                    {
                        ViewState["Ticket"] = Request.QueryString["TK"].ToString();
                    }
                    #endregion

                    #region  确认 未确认订单   cc取消单  用户取消单   其他  通过OC传值
                    if (!string.IsNullOrEmpty(Request.QueryString["OC"]))
                    {
                        string str = Request.QueryString["OC"].ToString();
                        ViewState["OrderBookStatusOther"] = str + ",";
                        if (!string.IsNullOrEmpty(ViewState["RestPriceCode"].ToString()))
                        {
                            if (str.Contains("4,5,6,7,8"))
                            {
                                ViewState["RestBookStatus"] = "5,";
                            }
                            else if (str.Contains("1"))
                            {
                                ViewState["RestBookStatus"] = "1,";
                            }
                            else if (str.Contains("9"))
                            {
                                ViewState["RestBookStatus"] = "9,";
                            }
                            else if (str.Contains("3"))
                            {
                                ViewState["RestBookStatus"] = "4,";
                            }
                            else if (str.Contains("2"))
                            {
                                ViewState["RestBookStatus"] = "2,3,";
                            }
                        }
                    }
                    #endregion
                }

                #region 设置时间段  根据DT
                if (!String.IsNullOrEmpty(Request.QueryString["DTStart"]) && !String.IsNullOrEmpty(Request.QueryString["DTEnd"]))
                {
                    string strStart = Request.QueryString["DTStart"].ToString().Replace("/", "-") + " 04:00:00";
                    string strEnd = Request.QueryString["DTEnd"].ToString().Replace("/", "-") + " 03:59:59";
                    //strEnd = Convert.ToDateTime(strEnd).AddDays(1).ToString();
                    ViewState["StartDTime"] = strStart;
                    ViewState["EndDTime"] = strEnd;
                    dpCreateStart.Value = strStart;
                    dpCreateEnd.Value = strEnd;
                }
                else
                {
                    string strStart = DateTime.Now.ToShortDateString().Replace("/", "-") + " 04:00:00";
                    //string strEnd = DateTime.Now.AddDays(1).ToShortDateString().Replace("/", "-") + " 03:59:59";
                    string strEnd = DateTime.Now.ToShortDateString().Replace("/", "-") + " 03:59:59";
                    ViewState["StartDTime"] = strStart;
                    ViewState["EndDTime"] = strEnd;
                    dpCreateStart.Value = strStart;
                    dpCreateEnd.Value = strEnd;
                }
                #endregion
            }
            #endregion

            #region  Edit Jason.yu  注释
            if (String.IsNullOrEmpty(Request.QueryString["TYPE"]) && String.IsNullOrEmpty(Request.QueryString["State"]))
            {
                ViewState["RestBookStatus"] = "10,";
                string strStart = DateTime.Now.ToShortDateString().Replace("/", "-") + " 00:00:00";
                string strEnd = DateTime.Now.ToShortDateString().Replace("/", "-") + " 23:59:59";
                ViewState["StartDTime"] = strStart;
                ViewState["EndDTime"] = strEnd;
                dpCreateStart.Value = strStart;
                dpCreateEnd.Value = strEnd;
            }
            #endregion



            // 正确的属性设置方法
            gridViewCSReviewLmSystemLogList.Attributes.Add("SortExpression", "CREATETIME");
            gridViewCSReviewLmSystemLogList.Attributes.Add("SortDirection", "DESC");

            BindReviewLmSystemLogListGrid();

            /*king*/
            //BindReviewLmSystemLogListGrid(1, gridViewCSReviewLmSystemLogList.PageSize);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
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
            return dtTime.AddDays(1).ToShortDateString() + " 04:00:00";
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

        DataTable dtIsReserve = GetISRESERVEDataTable();
        ddpIsReserve.DataSource = dtIsReserve;
        ddpIsReserve.DataTextField = "ISRESERVE_STATUS_TEXT";
        ddpIsReserve.DataValueField = "ISRESERVE_STATUS";
        ddpIsReserve.DataBind();
        ddpIsReserve.SelectedIndex = 0;

        GetPlatFormDataTable();
        GetSalesDataTable();
        GetpOrderChannel();
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
                    dr["PAY_CODE"] = "BAR,BARB";
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

    private DataTable GetISRESERVEDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn PayStatus = new DataColumn("ISRESERVE_STATUS");
        DataColumn PayStatusText = new DataColumn("ISRESERVE_STATUS_TEXT");

        dt.Columns.Add(PayStatus);
        dt.Columns.Add(PayStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["ISRESERVE_STATUS"] = "";
        dr0["ISRESERVE_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);
        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ISRESERVE_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["ISRESERVE_STATUS_TEXT"] = "即时确认单";
                    break;
                case "1":
                    dr["ISRESERVE_STATUS_TEXT"] = "非即时确认单";
                    break;
                default:
                    dr["ISRESERVE_STATUS_TEXT"] = "未知状态";
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

    private void GetpOrderChannel()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsOrderChannel = LmSystemLogBP.OrderChannelSelect(_lmSystemLogEntity).QueryResult;

        DataRow drTempcity = dsOrderChannel.Tables[0].NewRow();
        drTempcity["CHANNELCODE"] = DBNull.Value;
        drTempcity["CHANNELMNAME"] = "不限制";
        dsOrderChannel.Tables[0].Rows.InsertAt(drTempcity, 0);

        //DataRow drTempother = dsPlatForm.Tables[0].NewRow();
        //drTempother["CHANNELCODE"] = "-999999";
        //drTempother["CHANNELMNAME"] = "其他";
        //dsPlatForm.Tables[0].Rows.Add(drTempother);

        ddpOrderChannel.DataTextField = "CHANNELMNAME";
        ddpOrderChannel.DataValueField = "CHANNELCODE";
        ddpOrderChannel.DataSource = dsOrderChannel;
        ddpOrderChannel.DataBind();
        ddpOrderChannel.SelectedIndex = 0;
    }

    private void GetSalesDataTable()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsSalesManager = LmSystemLogBP.SalesManagerSelect(_lmSystemLogEntity).QueryResult;

        DataRow drTemp = dsSalesManager.Tables[0].NewRow();
        drTemp["SALESID"] = DBNull.Value;
        drTemp["SALESNAME"] = "不限制";
        dsSalesManager.Tables[0].Rows.InsertAt(drTemp, 0);

        ddpSalesManager.DataTextField = "SALESNAME";
        ddpSalesManager.DataValueField = "SALESID";
        ddpSalesManager.DataSource = dsSalesManager;
        ddpSalesManager.DataBind();
        ddpSalesManager.SelectedIndex = 0;
    }


    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewLmSystemLogListGrid();
    }

    private int CountLmSystemLog()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        _lmSystemLogEntity.GroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GroupID"].ToString())) ? null : ViewState["GroupID"].ToString();
        _lmSystemLogEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
        _lmSystemLogEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
        _lmSystemLogEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
        _lmSystemLogEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
        _lmSystemLogEntity.HotelComfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();
        _lmSystemLogEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
        _lmSystemLogEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
        _lmSystemLogEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
        _lmSystemLogEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();
        _lmSystemLogEntity.OutStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStart"].ToString())) ? null : ViewState["OutStart"].ToString();
        _lmSystemLogEntity.OutEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEnd"].ToString())) ? null : ViewState["OutEnd"].ToString();
        _lmSystemLogEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
        _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString();
        _lmSystemLogEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString();
        _lmSystemLogEntity.OutCC = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutCC"].ToString())) ? null : ViewState["OutCC"].ToString();
        _lmSystemLogEntity.OutUC = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutUC"].ToString())) ? null : ViewState["OutUC"].ToString();
        _lmSystemLogEntity.OutFailOrder = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutFailOrder"].ToString())) ? null : ViewState["OutFailOrder"].ToString();

        _lmSystemLogEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
        _lmSystemLogEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
        _lmSystemLogEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();

        _lmSystemLogEntity.DashPopStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashPopStatus"].ToString())) ? null : ViewState["DashPopStatus"].ToString();
        _lmSystemLogEntity.DashInStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInStart"].ToString())) ? null : ViewState["DashInStart"].ToString();
        _lmSystemLogEntity.DashInEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInEnd"].ToString())) ? null : ViewState["DashInEnd"].ToString();
        _lmSystemLogEntity.DashStartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashStartDTime"].ToString())) ? null : ViewState["DashStartDTime"].ToString();
        _lmSystemLogEntity.DashEndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashEndDTime"].ToString())) ? null : ViewState["DashEndDTime"].ToString();

        _lmSystemLogEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        _lmSystemLogEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        _lmSystemLogEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        _lmSystemLogEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        _lmSystemLogEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        _lmSystemLogEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();
        _lmSystemLogEntity.OrderChannel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderChannel"].ToString())) ? null : ViewState["OrderChannel"].ToString();
        _lmSystemLogEntity.OrderTypeStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderTypeStatus"].ToString())) ? null : ViewState["OrderTypeStatus"].ToString();
        _lmSystemLogEntity.OrderBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderBookStatus"].ToString())) ? null : ViewState["OrderBookStatus"].ToString();
        _lmSystemLogEntity.OrderBookStatusOther = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderBookStatusOther"].ToString())) ? null : ViewState["OrderBookStatusOther"].ToString();
        _lmSystemLogEntity.RestPriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestPriceCode"].ToString())) ? null : ViewState["RestPriceCode"].ToString();
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();

        _lmSystemLogEntity.IsReserve = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["IsReserve"].ToString())) ? null : ViewState["IsReserve"].ToString();

        _lmSystemLogEntity.GuestName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GuestName"].ToString())) ? null : ViewState["GuestName"].ToString();

        DataSet dsResult = new DataSet();
        //if (orderXPaymentCode != "" && orderXPaymentCode == "QuFen")
        if (ViewState["orderXPaymentCode"].ToString() != "" && ViewState["orderXPaymentCode"].ToString() == "QuFen")
        {
            dsResult = LmSystemLogBP.order_XPayment_ReviewLmOrderLogSelectCount(_lmSystemLogEntity).QueryResult;
        }
        //else if (orderInTheNight != "" && orderInTheNight == "CKIN")
        else if (ViewState["orderInTheNight"].ToString() != "" && ViewState["orderInTheNight"].ToString() == "CKIN")
        {
            dsResult = LmSystemLogBP.order_InTheNight_ReviewLmOrderLogSelectCount(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["Rests"].ToString() != "" && ViewState["Rests"].ToString() == "Rests")
        {
            dsResult = LmSystemLogBP.ReviewLmOrderLogSelectCountByRests(_lmSystemLogEntity).QueryResult;//其他  排除已存在的渠道
        }
        else
        {
            dsResult = LmSystemLogBP.ReviewLmOrderLogSelectCount(_lmSystemLogEntity).QueryResult;
        }

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
        }

        return 0;
    }

    private void BindReviewLmSystemLogListGrid()
    {
        //messageContent.InnerHtml = "";

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        _lmSystemLogEntity.GroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GroupID"].ToString())) ? null : ViewState["GroupID"].ToString();
        _lmSystemLogEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
        _lmSystemLogEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
        _lmSystemLogEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
        _lmSystemLogEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
        _lmSystemLogEntity.HotelComfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();
        _lmSystemLogEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
        _lmSystemLogEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
        _lmSystemLogEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
        _lmSystemLogEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();
        _lmSystemLogEntity.OutStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStart"].ToString())) ? null : ViewState["OutStart"].ToString();
        _lmSystemLogEntity.OutEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEnd"].ToString())) ? null : ViewState["OutEnd"].ToString();
        _lmSystemLogEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
        _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString();
        _lmSystemLogEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString();
        _lmSystemLogEntity.OutCC = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutCC"].ToString())) ? null : ViewState["OutCC"].ToString();
        _lmSystemLogEntity.OutUC = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutUC"].ToString())) ? null : ViewState["OutUC"].ToString();
        _lmSystemLogEntity.OutFailOrder = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutFailOrder"].ToString())) ? null : ViewState["OutFailOrder"].ToString();

        _lmSystemLogEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
        _lmSystemLogEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
        _lmSystemLogEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();
        _lmSystemLogEntity.DashPopStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashPopStatus"].ToString())) ? null : ViewState["DashPopStatus"].ToString();
        _lmSystemLogEntity.DashInStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInStart"].ToString())) ? null : ViewState["DashInStart"].ToString();
        _lmSystemLogEntity.DashInEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInEnd"].ToString())) ? null : ViewState["DashInEnd"].ToString();
        _lmSystemLogEntity.DashStartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashStartDTime"].ToString())) ? null : ViewState["DashStartDTime"].ToString();
        _lmSystemLogEntity.DashEndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashEndDTime"].ToString())) ? null : ViewState["DashEndDTime"].ToString();

        _lmSystemLogEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        _lmSystemLogEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        _lmSystemLogEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        _lmSystemLogEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        _lmSystemLogEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        _lmSystemLogEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();

        _lmSystemLogEntity.OrderChannel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderChannel"].ToString())) ? null : ViewState["OrderChannel"].ToString();
        _lmSystemLogEntity.OrderTypeStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderTypeStatus"].ToString())) ? null : ViewState["OrderTypeStatus"].ToString();
        _lmSystemLogEntity.OrderBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderBookStatus"].ToString())) ? null : ViewState["OrderBookStatus"].ToString();
        _lmSystemLogEntity.OrderBookStatusOther = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderBookStatusOther"].ToString())) ? null : ViewState["OrderBookStatusOther"].ToString();
        _lmSystemLogEntity.RestPriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestPriceCode"].ToString())) ? null : ViewState["RestPriceCode"].ToString();
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();
        _lmSystemLogEntity.IsReserve = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["IsReserve"].ToString())) ? null : ViewState["IsReserve"].ToString();
        _lmSystemLogEntity.GuestName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GuestName"].ToString())) ? null : ViewState["GuestName"].ToString();

        _lmSystemLogEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _lmSystemLogEntity.PageSize = gridViewCSReviewLmSystemLogList.PageSize;
        _lmSystemLogEntity.SortField = gridViewCSReviewLmSystemLogList.Attributes["SortExpression"].ToString();
        _lmSystemLogEntity.SortType = gridViewCSReviewLmSystemLogList.Attributes["SortDirection"].ToString();
        DataSet dsResult = new DataSet();
        //if (orderXPaymentCode != "" && orderXPaymentCode == "QuFen")
        if (ViewState["orderXPaymentCode"].ToString() != "" && ViewState["orderXPaymentCode"].ToString() == "QuFen")
        {
            dsResult = LmSystemLogBP.order_XPayment_ReviewLmOrderLogSelect(_lmSystemLogEntity).QueryResult;
        }
        //else if (orderInTheNight != "" && orderInTheNight == "CKIN")
        else if (ViewState["orderInTheNight"].ToString() != "" && ViewState["orderInTheNight"].ToString() == "CKIN")
        {
            dsResult = LmSystemLogBP.order_InTheNight_ReviewLmOrderLogSelect(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["Rests"].ToString() != "" && ViewState["Rests"].ToString() == "Rests")
        {
            dsResult = LmSystemLogBP.ReviewLmOrderLogSelectByRests(_lmSystemLogEntity).QueryResult;//其他  排除已存在的渠道
        }
        else
        {
            dsResult = LmSystemLogBP.ReviewLmOrderLogSelect(_lmSystemLogEntity).QueryResult;
        }

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                if (dsResult.Tables[0].Rows[i]["LMID"].ToString() == "17373")
                {
                    string lmid = dsResult.Tables[0].Rows[i]["LMID"].ToString();
                }

                if (dsResult.Tables[0].Rows[i]["LOGINMOBILE"].ToString() == "" && (dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "HOTELVP" || dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "HOTELVPPRO" || dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "GETAROOM"))
                {
                    dsResult.Tables[0].Rows[i]["LOGINMOBILE"] = "游客";
                }
                else if (dsResult.Tables[0].Rows[i]["LOGINMOBILE"].ToString() == "" && dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "QUNAR")
                {
                    dsResult.Tables[0].Rows[i]["LOGINMOBILE"] = "QUNAR";
                }

                dsResult.Tables[0].Rows[i]["HOTELNM"] = (dsResult.Tables[0].Rows[i]["HOTELNM"].ToString().Length > 10) ? dsResult.Tables[0].Rows[i]["HOTELNM"].ToString().Substring(0, 9) + "…" : dsResult.Tables[0].Rows[i]["HOTELNM"].ToString();
            }
        }

        gridViewCSReviewLmSystemLogList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "FOGORDERID" };//主键
        gridViewCSReviewLmSystemLogList.DataBind();

        AspNetPager1.PageSize = gridViewCSReviewLmSystemLogList.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
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
        for (int i = 0; i <= gridViewCSReviewLmSystemLogList.Rows.Count; i++)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //模板列 
                //((HyperLink)e.Row.Cells[3].FindControl("HyperLink1")).NavigateUrl = "~/WebUI/UserGroup/UserDetailPage.aspx?ID=" + e.Row.Cells[0].Text + "&TYPE=1&A=" + e.Row.Cells[0].Text.Trim();
                ((HyperLink)e.Row.Cells[2].FindControl("hpp")).NavigateUrl = "~/WebUI/UserGroup/UserDetailPage.aspx?UserID=" + ((HyperLink)e.Row.Cells[2].FindControl("hpp")).Text + "&TYPE=2&ID=" + ((HyperLink)e.Row.Cells[17].FindControl("HyperLink1")).Text;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
        ViewState["Rests"] = "";
        ViewState["BookStatus"] = "";
        ViewState["PlatForm"] = "";
        ViewState["OrderChannel"] = "";

        ViewState["OrderID"] = strOrderList;
        string strHotel = hidHotel.Value.ToString().Trim();
        string strCity = hidCity.Value.ToString().Trim();
        string strGroup = hidGroup.Value.ToString().Trim();
        ViewState["HotelID"] = (strHotel.IndexOf(']') > 0) ? strHotel.Substring(0, strHotel.IndexOf(']')).Trim('[').Trim(']') : strHotel;
        ViewState["CityID"] = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity;
        ViewState["GroupID"] = (strGroup.IndexOf(']') > 0) ? strGroup.Substring(0, strGroup.IndexOf(']')).Trim('[').Trim(']') : strGroup;
        //chkPayCode
        ViewState["PayCode"] = hidCommonList.Value != "" ? hidCommonList.Value + "," : hidCommonList.Value;
        ViewState["BookStatus"] = ddpBookStatus.SelectedValue.ToString().Trim();
        //ViewState["OrderBookStatus"] = ddpBookStatus.SelectedValue.ToString().Trim() != "" ? ddpBookStatus.SelectedValue.ToString().Trim() + "," : ddpBookStatus.SelectedValue.ToString().Trim();
        ViewState["PayStatus"] = ddpPayStatus.SelectedValue.ToString().Trim();
        ViewState["Aprove"] = ddpAprove.SelectedValue.ToString().Trim();
        ViewState["Ticket"] = ddpTicket.SelectedValue.ToString().Trim();
        ViewState["HotelComfirm"] = ddpHotelComfirm.SelectedValue.ToString().Trim();

        ViewState["IsReserve"] = ddpIsReserve.SelectedValue.ToString().Trim();

        ViewState["StartDTime"] = dpCreateStart.Value;
        ViewState["EndDTime"] = dpCreateEnd.Value;
        ViewState["Mobile"] = txtMobile.Value.Trim();
        ViewState["InStart"] = dpInStart.Value;
        ViewState["InEnd"] = dpInEnd.Value;

        ViewState["OutStart"] = dpOutStart.Value;
        ViewState["OutEnd"] = dpOutEnd.Value;

        ViewState["PlatForm"] = ddpPlatForm.SelectedValue.Trim() != "" ? ddpPlatForm.SelectedValue.Trim() + "," : ddpPlatForm.SelectedValue.Trim();
        ViewState["Sales"] = hidSalesCommonList.Value.Trim();
        ViewState["OutTest"] = (chkOutTest.Checked) ? "1" : "";

        ViewState["OutCC"] = (chkOutCC.Checked) ? "1" : "";
        ViewState["OutUC"] = (chkOutUC.Checked) ? "1" : "";

        ViewState["OutFailOrder"] = (chkFailOrder.Checked) ? "1" : "";

        ViewState["TicketType"] = "";
        ViewState["TicketData"] = "";
        ViewState["TicketPcode"] = "";
        ViewState["DashPopStatus"] = "";

        ViewState["DashInStart"] = "";
        ViewState["DashInEnd"] = "";
        ViewState["DashStartDTime"] = "";
        ViewState["DashEndDTime"] = "";

        ViewState["packagename"] = "";
        ViewState["amountfrom"] = "";
        ViewState["amountto"] = "";
        ViewState["pickfromdate"] = "";
        ViewState["picktodate"] = "";
        ViewState["tickettime"] = "";
        ViewState["OrderTypeStatus"] = "";
        //ViewState["OrderBookStatus"] = "";
        ViewState["OrderBookStatusOther"] = "";

        ViewState["OrderChannel"] = ddpOrderChannel.SelectedValue.ToString().Trim() != "" ? ddpOrderChannel.SelectedValue.ToString().Trim() + "," : ddpOrderChannel.SelectedValue.ToString().Trim();

        ViewState["GuestName"] = txtGuestNM.Value.Trim();

        AspNetPager1.CurrentPageIndex = 1;
        BindReviewLmSystemLogListGrid();
    }

    //<summary>
    //GridView排序事件
    //</summary>
    protected void gridViewCSReviewLmSystemLogList_Sorting(object sender, GridViewSortEventArgs e)
    {
        // 从事件参数获取排序数据列
        string sortExpression = e.SortExpression.ToString();

        // 假定为排序方向为“顺序”
        string sortDirection = "ASC";

        // “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
        if (sortExpression == gridViewCSReviewLmSystemLogList.Attributes["SortExpression"])
        {
            //获得下一次的排序状态
            sortDirection = (gridViewCSReviewLmSystemLogList.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
        }

        // 重新设定GridView排序数据列及排序方向
        gridViewCSReviewLmSystemLogList.Attributes["SortExpression"] = sortExpression;
        gridViewCSReviewLmSystemLogList.Attributes["SortDirection"] = sortDirection;

        BindReviewLmSystemLogListGrid();
        //BindReviewLmSystemLogListGrid(AspNetPager1.CurrentPageIndex , gridViewCSReviewLmSystemLogList.PageSize);
    }

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString(); ;
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString(); ;
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        _lmSystemLogEntity.GroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GroupID"].ToString())) ? null : ViewState["GroupID"].ToString();
        _lmSystemLogEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
        _lmSystemLogEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
        _lmSystemLogEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
        _lmSystemLogEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
        _lmSystemLogEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
        _lmSystemLogEntity.HotelComfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();
        _lmSystemLogEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
        _lmSystemLogEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
        _lmSystemLogEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();
        _lmSystemLogEntity.OutStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStart"].ToString())) ? null : ViewState["OutStart"].ToString();
        _lmSystemLogEntity.OutEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEnd"].ToString())) ? null : ViewState["OutEnd"].ToString();
        _lmSystemLogEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
        _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString();
        _lmSystemLogEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString();
        _lmSystemLogEntity.OutCC = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutCC"].ToString())) ? null : ViewState["OutCC"].ToString();
        _lmSystemLogEntity.OutUC = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutUC"].ToString())) ? null : ViewState["OutUC"].ToString();
        _lmSystemLogEntity.OutFailOrder = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutFailOrder"].ToString())) ? null : ViewState["OutFailOrder"].ToString();

        _lmSystemLogEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
        _lmSystemLogEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
        _lmSystemLogEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();
        _lmSystemLogEntity.DashPopStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashPopStatus"].ToString())) ? null : ViewState["DashPopStatus"].ToString();
        _lmSystemLogEntity.DashInStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInStart"].ToString())) ? null : ViewState["DashInStart"].ToString();
        _lmSystemLogEntity.DashInEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInEnd"].ToString())) ? null : ViewState["DashInEnd"].ToString();
        _lmSystemLogEntity.DashStartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashStartDTime"].ToString())) ? null : ViewState["DashStartDTime"].ToString();
        _lmSystemLogEntity.DashEndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashEndDTime"].ToString())) ? null : ViewState["DashEndDTime"].ToString();

        _lmSystemLogEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        _lmSystemLogEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        _lmSystemLogEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        _lmSystemLogEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        _lmSystemLogEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        _lmSystemLogEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();
        _lmSystemLogEntity.OrderChannel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderChannel"].ToString())) ? null : ViewState["OrderChannel"].ToString();
        _lmSystemLogEntity.OrderTypeStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderTypeStatus"].ToString())) ? null : ViewState["OrderTypeStatus"].ToString();
        _lmSystemLogEntity.OrderBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderBookStatus"].ToString())) ? null : ViewState["OrderBookStatus"].ToString();
        _lmSystemLogEntity.OrderBookStatusOther = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderBookStatusOther"].ToString())) ? null : ViewState["OrderBookStatusOther"].ToString();
        _lmSystemLogEntity.RestPriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestPriceCode"].ToString())) ? null : ViewState["RestPriceCode"].ToString();
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();
        _lmSystemLogEntity.IsReserve = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["IsReserve"].ToString())) ? null : ViewState["IsReserve"].ToString();
        _lmSystemLogEntity.GuestName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GuestName"].ToString())) ? null : ViewState["GuestName"].ToString();

        _lmSystemLogEntity.SortField = gridViewCSReviewLmSystemLogList.Attributes["SortExpression"].ToString();
        _lmSystemLogEntity.SortType = gridViewCSReviewLmSystemLogList.Attributes["SortDirection"].ToString();

        DataSet dsResult = new DataSet();
        //if (orderXPaymentCode != "" && orderXPaymentCode == "QuFen")
        if (ViewState["orderXPaymentCode"].ToString() != "" && ViewState["orderXPaymentCode"].ToString() == "QuFen")
        {
            //dsResult = LmSystemLogBP.order_XPayment_ReviewLmOrderLogSelectCount(_lmSystemLogEntity).QueryResult;
            dsResult = LmSystemLogBP.order_XPayment_ReviewLmOrderLogExport(_lmSystemLogEntity).QueryResult;
        }
        //else if (orderInTheNight != "" && orderInTheNight == "CKIN")
        else if (ViewState["orderInTheNight"].ToString() != "" && ViewState["orderInTheNight"].ToString() == "CKIN")
        {
            //dsResult = LmSystemLogBP.order_InTheNight_ReviewLmOrderLogSelectCount(_lmSystemLogEntity).QueryResult;
            dsResult = LmSystemLogBP.order_InTheNight_ReviewLmOrderLogExport(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["Rests"].ToString() != "" && ViewState["Rests"].ToString() == "Rests")
        {
            dsResult = LmSystemLogBP.ExportLmOrderSelectByRests(_lmSystemLogEntity).QueryResult;//其他  排除已存在的渠道
        }
        else
        {
            dsResult = LmSystemLogBP.ExportLmOrderSelect(_lmSystemLogEntity).QueryResult;
        }

        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                if (dsResult.Tables[0].Rows[i]["LOGINMOBILE"].ToString() == "" && (dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "HOTELVP" || dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "HOTELVPPRO" || dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "GETAROOM"))
                {
                    dsResult.Tables[0].Rows[i]["LOGINMOBILE"] = "游客";
                }
                else if (dsResult.Tables[0].Rows[i]["LOGINMOBILE"].ToString() == "" && dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "QUNAR")
                {
                    dsResult.Tables[0].Rows[i]["LOGINMOBILE"] = "QUNAR";
                }
            }
        }

        CommonFunction.ExportExcelForLM(dsResult);
    }
}