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
using HotelVp.CMS.Domain.Process.GeneralSetting;
using System.Configuration;

public partial class WebUI_DBQuery_LmOrderLogPage : System.Web.UI.Page
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type=hotel") && !this.Page.Request.QueryString.ToString().Contains("Type=city"))
        {
            GetSalesDataTable();//酒店销售人员
            GetPayCodeDataTable();//价格代码(订单类型)
            GetAffirmStatus();//订单确认状态
            GetPlatForm();//应用平台
            GetOrderChannel();//订单渠道
            GetAprove();//审核状态
            BindCHK();//绑定默认值

            #region
            ViewState["orderXPaymentCode"] = "";
            ViewState["orderInTheNight"] = "";
            ViewState["Rests"] = "";


            ViewState["StartDTime"] = "";//下单开始时间
            ViewState["EndDTime"] = "";//下单结束时间
            ViewState["GroupID"] = "";//集团
            ViewState["InStart"] = "";//入住开始日期
            ViewState["InEnd"] = "";//入住结束日期
            ViewState["CityID"] = "";//城市
            ViewState["Business"] = "";//商圈
            ViewState["OutStart"] = "";//离店开始时间
            ViewState["OutEnd"] = "";//离店结束时间
            ViewState["Sales"] = "";//销售
            ViewState["HotelID"] = "";//酒店
            ViewState["Mobile"] = "";//登录手机
            ViewState["OrderID"] = "";//订单ID
            ViewState["GuestName"] = "";//入住人


            ViewState["BookStatusOtherCreate"] = "";//创建状态
            ViewState["UserCancel"] = "";//用户取消
            ViewState["PriceCode"] = "";//价格代码
            ViewState["BookStatusOtherAffirm"] = "";//确认状态
            ViewState["Timeout"] = "";//确认超时
            ViewState["Ticket"] = "";//含返现券
            ViewState["PlatForm"] = "";//订单平台
            ViewState["OrderChannel"] = "";//订单渠道
            ViewState["Aprove"] = "";//审核状态


            ViewState["OutTest"] = "";
            ViewState["RestBookStatus"] = "";
            ViewState["orderInTheNight"] = "";
            ViewState["HotelComfirm"] = "";


            ViewState["TicketType"] = "";//优惠券类型
            ViewState["TicketData"] = "";
            ViewState["TicketPcode"] = "";
            ViewState["packagename"] = "";//优惠券礼包名
            ViewState["amountfrom"] = "";
            ViewState["amountto"] = "";
            ViewState["pickfromdate"] = "";
            ViewState["picktodate"] = "";
            ViewState["tickettime"] = "";
            ViewState["RestPriceCode"] = "";

            ViewState["BookStatus"] = "";
            ViewState["PayCode"] = "";
            ViewState["PayStatus"] = "";

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
                    //ViewState["RestBookStatus"] = "10,";

                    #region 总订单  （数）
                    string strPayCode = Request.QueryString["PC"] == null ? "" : Request.QueryString["PC"].ToString();
                    if ("ALL".Equals(strPayCode))
                    {
                        ViewState["PriceCode"] = "LMBAR,LMBAR2,BAR,BARB,";
                    }
                    else if ("LMBAR".Equals(strPayCode))
                    {
                        ViewState["PriceCode"] = "LMBAR,";
                    }
                    else if ("LMBAR2".Equals(strPayCode))
                    {
                        ViewState["PriceCode"] = "LMBAR2,";
                    }
                    else
                    {
                        ViewState["PriceCode"] = strPayCode + ",";
                    }
                    #endregion

                    #region  当晚入住
                    if (!String.IsNullOrEmpty(Request.QueryString["PF"]))
                    {
                        ViewState["orderInTheNight"] = Request.QueryString["PF"].ToString();
                        if (ViewState["orderInTheNight"].ToString() == "CKIN")
                        {
                            ViewState["PriceCode"] = "BAR,BARB,";
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
                            ViewState["RestBookStatus"] = str;
                        }
                        else if ("ALL".Equals(strPayCode))
                        {
                            string[] strStatus = str.Split('|');
                            ViewState["RestBookStatus"] = strStatus[1].ToString() + ",";
                            ViewState["BookStatusOtherAffirm"] = strStatus[0].ToString() + ",";
                            if (ViewState["orderInTheNight"].ToString() == "SUM")
                            {
                                ViewState["orderXPaymentCode"] = "QuFen";
                            }
                        }
                        else
                        {
                            ViewState["BookStatusOtherAffirm"] = str + ",";
                            if (str.Contains("4,5,6,7,8"))
                            {
                                ViewState["HotelComfirm"] = "1";
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 渠道   应用平台
                    string strPayCode = Request.QueryString["PC"] == null ? "" : Request.QueryString["PC"].ToString();
                    string[] payCode = strPayCode.Split(','); //HOTELVP    IOS
                    ViewState["RestPriceCode"] = "LMBAR";
                    if ("Rests".Equals(payCode[0].ToString()))
                    {
                        ViewState["Rests"] = "Rests";
                        ViewState["OrderChannel"] = "HOTELVP,GETAROOM,QUNAR,116114,MOJI,HOTELVPPRO,HOTELVPMAP,";
                        ViewState["PriceCode"] = "LMBAR,LMBAR2,BAR,BARB,";
                    }
                    else if ("ALL".Equals(payCode[0].ToString()))
                    {
                        //需要改动sql    查询字段  需改为  上面一直   以逗号分隔     订单渠道  应用平台
                        ViewState["OrderChannel"] = "HOTELVP,GETAROOM,QUNAR,116114,MOJI,HOTELVPPRO,HOTELVPMAP,";
                        ViewState["PlatForm"] = "IOS,ANDROID,WP,WAP,";
                        ViewState["PriceCode"] = "LMBAR,LMBAR2,BAR,BARB,";
                    }
                    else if ("HOTELVP".Equals(payCode[0].ToString()))
                    {
                        #region
                        strPayCode = payCode[1].ToString();
                        if ("IOS".Equals(strPayCode))
                        {
                            ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                            ViewState["PlatForm"] = payCode[1].ToString() + ",";
                            ViewState["PriceCode"] = "LMBAR,LMBAR2,BAR,BARB,";
                        }
                        else if ("ANDROID".Equals(strPayCode))
                        {
                            ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                            ViewState["PlatForm"] = payCode[1].ToString() + ",";
                            ViewState["PriceCode"] = "LMBAR,LMBAR2,BAR,BARB,";
                        }
                        else if ("WP".Equals(strPayCode))
                        {
                            ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                            ViewState["PlatForm"] = payCode[1].ToString() + ",";
                            ViewState["PriceCode"] = "LMBAR,LMBAR2,BAR,BARB,";
                        }
                        else
                        {
                            //WAP
                            ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                            ViewState["PlatForm"] = payCode[1].ToString() + ",";
                            ViewState["PriceCode"] = "LMBAR,LMBAR2,BAR,BARB,";
                        }
                        #endregion
                    }
                    else
                    {
                        ViewState["OrderChannel"] = payCode[0].ToString() + ",";
                        ViewState["PriceCode"] = "LMBAR,LMBAR2,BAR,BARB,";
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
                        ViewState["BookStatusOtherAffirm"] = str + ",";
                        if (!string.IsNullOrEmpty(ViewState["PriceCode"].ToString()))
                        {
                            if (str.Contains("4,5,6,7,8"))
                            {
                                ViewState["RestBookStatus"] = "5,";
                                ViewState["BookStatusOtherAffirm"] = "4,5,6,7,8,";
                                ViewState["HotelComfirm"] = "1";
                            }
                            else if (str.Contains("1"))
                            {
                                ViewState["RestBookStatus"] = "1,";
                                ViewState["BookStatusOtherAffirm"] = "1,";
                            }
                            else if (str.Contains("9"))
                            {
                                ViewState["RestBookStatus"] = "9,";
                                ViewState["BookStatusOtherAffirm"] = "9,";
                            }
                            else if (str.Contains("3"))
                            {
                                ViewState["RestBookStatus"] = "4,";
                                ViewState["BookStatusOtherAffirm"] = "3,";
                            }
                            else if (str.Contains("2"))
                            {
                                ViewState["RestBookStatus"] = "2,3,";
                                ViewState["BookStatusOtherAffirm"] = "2,0,";
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
                    ViewState["StartDTime"] = strStart;
                    ViewState["EndDTime"] = strEnd;
                    dpCreateStart.Value = strStart;
                    dpCreateEnd.Value = strEnd;
                }
                else
                {
                    string strStart = DateTime.Now.ToShortDateString().Replace("/", "-") + " 04:00:00";
                    string strEnd = DateTime.Now.ToShortDateString().Replace("/", "-") + " 03:59:59";
                    ViewState["StartDTime"] = strStart;
                    ViewState["EndDTime"] = strEnd;
                    dpCreateStart.Value = strStart;
                    dpCreateEnd.Value = strEnd;
                }
                #endregion
            }
            #endregion

            #region  Edit Jason.yu
            if (String.IsNullOrEmpty(Request.QueryString["TYPE"]) && String.IsNullOrEmpty(Request.QueryString["State"]))
            {
                ViewState["RestBookStatus"] = "10,";
                string strStart = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-") + " 04:00:00";
                string strEnd = DateTime.Now.ToShortDateString().Replace("/", "-") + " 03:59:59";
                ViewState["StartDTime"] = strStart;
                ViewState["EndDTime"] = strEnd;
                dpCreateStart.Value = strStart;
                dpCreateEnd.Value = strEnd;
            }
            #endregion


            // 正确的属性设置方法
            gridViewCSReviewLmSystemLogList.Attributes.Add("SortExpression", "CREATE_TIME");
            gridViewCSReviewLmSystemLogList.Attributes.Add("SortDirection", "DESC");

            BindReviewLmSystemLogListGrid();

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);


        }
    }

    //搜索()
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
        ViewState["StartDTime"] = dpCreateStart.Value;//下单开始时间
        ViewState["EndDTime"] = dpCreateEnd.Value;//下单结束时间
        string strGroup = hidGroup.Value.ToString().Trim();
        ViewState["GroupID"] = (strGroup.IndexOf(']') > 0) ? strGroup.Substring(0, strGroup.IndexOf(']')).Trim('[').Trim(']') : strGroup; //集团
        ViewState["InStart"] = dpInStart.Value;//入住开始日期
        ViewState["InEnd"] = dpInEnd.Value;//入住结束日期 
        string strCity = hidCity.Value.ToString().Trim();
        ViewState["CityID"] = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity; //城市
        string strTagInfo = hidTagInfo.Value.ToString().Trim();
        ViewState["Business"] = (strTagInfo.IndexOf(']') > 0) ? strTagInfo.Substring(0, strTagInfo.IndexOf(']')).Trim('[').Trim(']') : strCity;//商圈
        ViewState["OutStart"] = dpOutStart.Value;//离店开始时间
        ViewState["OutEnd"] = dpOutEnd.Value;//离店结束时间
        ViewState["Sales"] = ddpSalesManager.SelectedValue; //销售
        string strHotel = hidHotel.Value.ToString().Trim();
        ViewState["HotelID"] = (strHotel.IndexOf(']') > 0) ? strHotel.Substring(0, strHotel.IndexOf(']')).Trim('[').Trim(']') : strHotel;//酒店
        ViewState["Mobile"] = txtMobile.Value.Trim();//登录手机
        ViewState["OrderID"] = strOrderList;//订单ID
        ViewState["GuestName"] = txtGuestNM.Value.Trim();//入住人

        if ((this.chkSucceed.Checked == true && this.chkFail.Checked == true) || (this.chkSucceed.Checked == false && this.chkFail.Checked == false))
        {
            ViewState["BookStatusOtherCreate"] = "";//创建状态   1成功 0失败
        }
        else if (this.chkSucceed.Checked == true && this.chkFail.Checked == false)
        {
            ViewState["BookStatusOtherCreate"] = "1";//创建状态   1成功 0失败
        }
        else
        {
            ViewState["BookStatusOtherCreate"] = "0";//创建状态   1成功 0失败
        }

        if ((this.chkUserCancelY.Checked == true && this.chkUserCancelN.Checked == true) || (this.chkUserCancelY.Checked == false && this.chkUserCancelN.Checked == false))
        {
            ViewState["UserCancel"] = "";//用户取消   1 是  0否
        }
        else if (this.chkUserCancelY.Checked == true && this.chkUserCancelN.Checked == false)
        {
            ViewState["UserCancel"] = "1";//用户取消   1 是  0否
        }
        else
        {
            ViewState["UserCancel"] = "0";//用户取消   1 是  0否
        }

        ViewState["PriceCode"] = hidCommonList.Value != "" ? hidCommonList.Value : ""; //价格代码
        ViewState["BookStatusOtherAffirm"] = this.ddlAffirmStatus.SelectedValue;//确认状态 
        ViewState["BookStatus"] = ConvertStr(this.ddlAffirmStatus.SelectedValue);//根据Book_status_other或者Book_status
        if ((this.chkTimeOutY.Checked == true && this.chkTimeOutN.Checked == true) || (this.chkTimeOutY.Checked == false && this.chkTimeOutN.Checked == false))
        {
            ViewState["Timeout"] = "";//确认超时   1 是  0否
        }
        else if (this.chkTimeOutY.Checked == true && this.chkTimeOutN.Checked == false)
        {
            ViewState["Timeout"] = "1";//确认超时   1 是  0否
        }
        else
        {
            ViewState["Timeout"] = "0";//确认超时   1 是  0否
        }



        if (this.chkTicketY.Checked == true && this.chkTicketN.Checked == false)
        {
            ViewState["Ticket"] = "1";//含返现券    1是  2 否
        }
        else if (this.chkTicketY.Checked == false && this.chkTicketN.Checked == true)
        {
            ViewState["Ticket"] = "0";//含返现券    1是  2 否
        }

        ViewState["PlatForm"] = ddpPlatForm.SelectedValue;//订单平台
        ViewState["OrderChannel"] = ddpOrderChannel.SelectedValue;//订单渠道
        ViewState["Aprove"] = ddpAprove.SelectedValue;//审核状态


        //支付方式
        if (this.chkPayMethodAlipay.Checked == true && this.chkPayMethodUnionPay.Checked == false)
        {
            ViewState["PayCode"] = "1,";//支付宝1   银联 3
        }
        else if (this.chkPayMethodAlipay.Checked == false && this.chkPayMethodUnionPay.Checked == true)
        {
            ViewState["PayCode"] = "3,";//支付宝1   银联 3
        }
        else if (this.chkPayMethodAlipay.Checked == true && this.chkPayMethodUnionPay.Checked == true)
        {
            ViewState["PayCode"] = "1,3,";//支付宝1   银联 3
        }
        else
        {
            ViewState["PayCode"] = "";
        }
        //支付状态 
        string strPayStatus = "";
        if (this.chkPayStatusSus.Checked == true)//支付成功
        {
            strPayStatus += "1,";//支付成功1         待支付0,2,3,8,9        已退款7
        }
        if (this.chkPayStatusBackPay.Checked == true)
        {
            strPayStatus += "0,2,3,8,9,";//支付成功1         待支付0,2,3,8,9     已退款7
        }
        if (this.chkPayStatusRebate.Checked == true)
        {
            strPayStatus += "7,";//支付成功1         待支付0,2,3,8,9     已退款7
        }
        ViewState["PayStatus"] = strPayStatus;


        ViewState["OutTest"] = "";
        ViewState["RestBookStatus"] = "";
        ViewState["orderInTheNight"] = "";

        AspNetPager1.CurrentPageIndex = 1;
        BindReviewLmSystemLogListGrid();
    }

    private string ConvertStr(string str)
    {
        string returnStr = "";
        switch (str)
        {
            case "4,5,6,7,8,":
                returnStr = "6,8,9,10,";
                break;
            case "9,":
                returnStr = "7,";
                break;
            case "1,":
                returnStr = "5,";
                break;
            default:
                break;
        }
        return returnStr;
    }

    private void BindReviewLmSystemLogListGrid()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString();
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();

        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();//下单开始时间
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();//下单结束时间
        _lmSystemLogEntity.GroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GroupID"].ToString())) ? null : ViewState["GroupID"].ToString();//集团

        _lmSystemLogEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();//入住开始日期
        _lmSystemLogEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();//入住结束日期
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();  //城市

        _lmSystemLogEntity.TagInfo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Business"].ToString())) ? null : ViewState["Business"].ToString();//商圈
        _lmSystemLogEntity.OutStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStart"].ToString())) ? null : ViewState["OutStart"].ToString();//离店开始时间
        _lmSystemLogEntity.OutEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEnd"].ToString())) ? null : ViewState["OutEnd"].ToString();//离店结束时间

        _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString(); //销售
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();//酒店
        _lmSystemLogEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();//登录手机

        _lmSystemLogEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();//订单ID
        _lmSystemLogEntity.GuestName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GuestName"].ToString())) ? null : ViewState["GuestName"].ToString();//入住人
        _lmSystemLogEntity.CreateStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatusOtherCreate"].ToString())) ? null : ViewState["BookStatusOtherCreate"].ToString();//创建状态   成功 失败

        _lmSystemLogEntity.UserCancel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UserCancel"].ToString())) ? null : ViewState["UserCancel"].ToString();//用户取消   1 是  2 否
        _lmSystemLogEntity.PriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PriceCode"].ToString())) ? null : ViewState["PriceCode"].ToString(); //价格代码
        _lmSystemLogEntity.AffirmStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatusOtherAffirm"].ToString())) ? null : ViewState["BookStatusOtherAffirm"].ToString();//确认状态

        _lmSystemLogEntity.Timeout = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Timeout"].ToString())) ? null : ViewState["Timeout"].ToString();//确认超时   1是  2 否
        _lmSystemLogEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();//含返现券    1是  2 否
        _lmSystemLogEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();//订单平台

        _lmSystemLogEntity.OrderChannel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderChannel"].ToString())) ? null : ViewState["OrderChannel"].ToString();//订单渠道
        _lmSystemLogEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();//审核状态 
        _lmSystemLogEntity.HotelComfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();//酒店是否已确认

        _lmSystemLogEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();//LMBAR状态
        _lmSystemLogEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();//支付方式
        _lmSystemLogEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();//支付状态

        #region  优惠券信息
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();
        _lmSystemLogEntity.RestPriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestPriceCode"].ToString())) ? null : ViewState["RestPriceCode"].ToString();

        _lmSystemLogEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
        _lmSystemLogEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
        _lmSystemLogEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();

        _lmSystemLogEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        _lmSystemLogEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        _lmSystemLogEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        _lmSystemLogEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        _lmSystemLogEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        _lmSystemLogEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();
        #endregion

        _lmSystemLogEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _lmSystemLogEntity.PageSize = gridViewCSReviewLmSystemLogList.PageSize;
        _lmSystemLogEntity.SortField = gridViewCSReviewLmSystemLogList.Attributes["SortExpression"].ToString();
        _lmSystemLogEntity.SortType = gridViewCSReviewLmSystemLogList.Attributes["SortDirection"].ToString();


        DataSet dsResult = new DataSet();
        if (ViewState["orderXPaymentCode"].ToString() != "" && ViewState["orderXPaymentCode"].ToString() == "QuFen")
        {
            //dsResult = LmSystemLogBP.order_XPayment_ReviewLmOrderLogSelect(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["orderInTheNight"].ToString() != "" && ViewState["orderInTheNight"].ToString() == "CKIN")
        {
            //dsResult = LmSystemLogBP.order_InTheNight_ReviewLmOrderLogSelect(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["Rests"].ToString() != "" && ViewState["Rests"].ToString() == "Rests")
        {
            //dsResult = LmSystemLogBP.ReviewLmOrderLogSelectByRests(_lmSystemLogEntity).QueryResult;//其他  排除已存在的渠道
            dsResult = OrderSearchBP.ReviewLmOrderLogSelectByRests(_lmSystemLogEntity).QueryResult;
        }
        else
        {
            //dsResult = LmSystemLogBP.ReviewLmOrderLogSelect(_lmSystemLogEntity).QueryResult;
            dsResult = OrderSearchBP.ReviewOrderListSelect(_lmSystemLogEntity).QueryResult;
        }

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            //for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                if (dsResult.Tables[0].Rows[i]["login_mobile"].ToString() == "" && (dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "HOTELVP" || dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "HOTELVPPRO" || dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "GETAROOM"))
                {
                    dsResult.Tables[0].Rows[i]["login_mobile"] = "游客";
                }
                else if (dsResult.Tables[0].Rows[i]["login_mobile"].ToString() == "" && dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "QUNAR")
                {
                    dsResult.Tables[0].Rows[i]["login_mobile"] = "QUNAR";
                }

                dsResult.Tables[0].Rows[i]["hotel_name"] = (dsResult.Tables[0].Rows[i]["hotel_name"].ToString().Length > 10) ? dsResult.Tables[0].Rows[i]["hotel_name"].ToString().Substring(0, 9) + "…" : dsResult.Tables[0].Rows[i]["hotel_name"].ToString();



                if (!string.IsNullOrEmpty(ViewState["Timeout"].ToString()))
                {
                    if (!string.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()))
                    {
                        if (ViewState["Timeout"].ToString() == "1")//只取超时订单
                        {
                            if (int.Parse(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()) <= 30)
                            {
                                dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                            }
                        }
                        else//只取没有超时的订单
                        {
                            if (int.Parse(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()) > 30)
                            {
                                dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                            }
                        }
                    }
                    else
                    {
                        dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                    }
                }
            }
        }

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            gridViewCSReviewLmSystemLogList.DataSource = dsResult.Tables[0].DefaultView;
            gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "FOG_ORDER_NUM" };//主键
            gridViewCSReviewLmSystemLogList.DataBind();
        }
        else
        {
            gridViewCSReviewLmSystemLogList.DataSource = BackTable();
            gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "FOG_ORDER_NUM" };//主键
            gridViewCSReviewLmSystemLogList.DataBind();
        }

        AspNetPager1.PageSize = gridViewCSReviewLmSystemLogList.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();


        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    private DataSet BackTable()
    {
        DataSet dsResult = new DataSet();
        dsResult.Tables.Add(new DataTable());
        dsResult.Tables[0].Columns.Add("LMID");
        dsResult.Tables[0].Columns.Add("FOG_ORDER_NUM");
        dsResult.Tables[0].Columns.Add("FOGORDERNUM");
        dsResult.Tables[0].Columns.Add("login_mobile");
        dsResult.Tables[0].Columns.Add("create_time");
        dsResult.Tables[0].Columns.Add("hotel_name");
        dsResult.Tables[0].Columns.Add("name_cn");
        dsResult.Tables[0].Columns.Add("room_type_name");
        dsResult.Tables[0].Columns.Add("book_room_num");
        dsResult.Tables[0].Columns.Add("price_code");
        dsResult.Tables[0].Columns.Add("book_price");
        dsResult.Tables[0].Columns.Add("in_date");
        dsResult.Tables[0].Columns.Add("out_date");
        dsResult.Tables[0].Columns.Add("guest_names");
        dsResult.Tables[0].Columns.Add("FOGRESVSTATUS");
        dsResult.Tables[0].Columns.Add("FOGAUDITSTATUS");
        dsResult.Tables[0].Columns.Add("sales_account");
        dsResult.Tables[0].Columns.Add("confirmTime");
        dsResult.Tables[0].Columns.Add("ticket_amount");
        dsResult.Tables[0].Columns.Add("order_channel");
        dsResult.Tables[0].Columns.Add("app_platform");
        dsResult.Tables[0].Columns.Add("UserID");
        dsResult.Tables[0].Columns.Add("User_ID");


        DataRow row = dsResult.Tables[0].NewRow();

        row["LMID"] = "";
        row["FOG_ORDER_NUM"] = "";
        row["FOGORDERNUM"] = "";
        row["login_mobile"] = "";
        row["create_time"] = "";
        row["hotel_name"] = "";
        row["name_cn"] = "";
        row["room_type_name"] = "";
        row["book_room_num"] = "";
        row["price_code"] = "";
        row["book_price"] = "";
        row["in_date"] = "";
        row["out_date"] = "";
        row["guest_names"] = "";
        row["FOGRESVSTATUS"] = "";
        row["FOGAUDITSTATUS"] = "";
        row["sales_account"] = "";
        row["confirmTime"] = "";
        row["ticket_amount"] = "";
        row["order_channel"] = "";
        row["app_platform"] = "";
        row["UserID"] = "";
        row["User_ID"] = "";


        dsResult.Tables[0].Rows.Add(row);
        return dsResult;
    }

    private int CountLmSystemLog()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _lmSystemLogEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString();
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();

        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();//下单开始时间
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();//下单结束时间
        _lmSystemLogEntity.GroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GroupID"].ToString())) ? null : ViewState["GroupID"].ToString();//集团

        _lmSystemLogEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();//入住开始日期
        _lmSystemLogEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();//入住结束日期
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();  //城市

        _lmSystemLogEntity.TagInfo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Business"].ToString())) ? null : ViewState["Business"].ToString();//商圈
        _lmSystemLogEntity.OutStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStart"].ToString())) ? null : ViewState["OutStart"].ToString();//离店开始时间
        _lmSystemLogEntity.OutEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEnd"].ToString())) ? null : ViewState["OutEnd"].ToString();//离店结束时间

        _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString(); //销售
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();//酒店
        _lmSystemLogEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();//登录手机

        _lmSystemLogEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();//订单ID
        _lmSystemLogEntity.GuestName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GuestName"].ToString())) ? null : ViewState["GuestName"].ToString();//入住人
        _lmSystemLogEntity.CreateStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatusOtherCreate"].ToString())) ? null : ViewState["BookStatusOtherCreate"].ToString();//创建状态   成功 失败

        _lmSystemLogEntity.UserCancel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UserCancel"].ToString())) ? null : ViewState["UserCancel"].ToString();//用户取消   1 是  2 否
        _lmSystemLogEntity.PriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PriceCode"].ToString())) ? null : ViewState["PriceCode"].ToString(); //价格代码
        _lmSystemLogEntity.AffirmStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatusOtherAffirm"].ToString())) ? null : ViewState["BookStatusOtherAffirm"].ToString();//确认状态

        _lmSystemLogEntity.Timeout = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Timeout"].ToString())) ? null : ViewState["Timeout"].ToString();//确认超时   1是  2 否
        _lmSystemLogEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();//含返现券    1是  2 否
        _lmSystemLogEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();//订单平台

        _lmSystemLogEntity.OrderChannel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderChannel"].ToString())) ? null : ViewState["OrderChannel"].ToString();//订单渠道
        _lmSystemLogEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();//审核状态 

        _lmSystemLogEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();//LMBAR状态
        _lmSystemLogEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();//支付方式
        _lmSystemLogEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();//支付状态

        #region  优惠券信息
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();

        _lmSystemLogEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
        _lmSystemLogEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
        _lmSystemLogEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();

        _lmSystemLogEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        _lmSystemLogEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        _lmSystemLogEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        _lmSystemLogEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        _lmSystemLogEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        _lmSystemLogEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();
        #endregion


        _lmSystemLogEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _lmSystemLogEntity.PageSize = gridViewCSReviewLmSystemLogList.PageSize;
        _lmSystemLogEntity.SortField = gridViewCSReviewLmSystemLogList.Attributes["SortExpression"].ToString();
        _lmSystemLogEntity.SortType = gridViewCSReviewLmSystemLogList.Attributes["SortDirection"].ToString();

        DataSet dsResult = new DataSet();
        if (ViewState["orderXPaymentCode"].ToString() != "" && ViewState["orderXPaymentCode"].ToString() == "QuFen")
        {
            //dsResult = LmSystemLogBP.order_XPayment_ReviewLmOrderLogSelectCount(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["orderInTheNight"].ToString() != "" && ViewState["orderInTheNight"].ToString() == "CKIN")
        {
            //dsResult = LmSystemLogBP.order_InTheNight_ReviewLmOrderLogSelectCount(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["Rests"].ToString() != "" && ViewState["Rests"].ToString() == "Rests")
        {
            dsResult = OrderSearchBP.ReviewLmOrderLogSelectCountByRests(_lmSystemLogEntity).QueryResult;//其他  排除已存在的渠道
        }
        else
        {
            dsResult = OrderSearchBP.ReviewOrderListSelectCount(_lmSystemLogEntity).QueryResult;
        }

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            //for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(ViewState["Timeout"].ToString()))
                {
                    if (!string.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()))
                    {
                        if (ViewState["Timeout"].ToString() == "1")//只取超时订单
                        {
                            if (int.Parse(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()) <= 30)
                            {
                                dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                            }
                        }
                        else//只取没有超时的订单
                        {
                            if (int.Parse(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()) > 30)
                            {
                                dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                            }
                        }
                    }
                    else
                    {
                        dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                    }
                }
            }

            int iSum = int.Parse(ConfigurationManager.AppSettings["iSum"].ToString());
            return int.Parse((dsResult.Tables[0].Rows.Count * iSum).ToString());
        }

        return 0;
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

        _lmSystemLogEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString();
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();

        _lmSystemLogEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();//下单开始时间
        _lmSystemLogEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();//下单结束时间
        _lmSystemLogEntity.GroupID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GroupID"].ToString())) ? null : ViewState["GroupID"].ToString();//集团

        _lmSystemLogEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();//入住开始日期
        _lmSystemLogEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();//入住结束日期
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();  //城市

        _lmSystemLogEntity.TagInfo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Business"].ToString())) ? null : ViewState["Business"].ToString();//商圈
        _lmSystemLogEntity.OutStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutStart"].ToString())) ? null : ViewState["OutStart"].ToString();//离店开始时间
        _lmSystemLogEntity.OutEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutEnd"].ToString())) ? null : ViewState["OutEnd"].ToString();//离店结束时间

        _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString(); //销售
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();//酒店
        _lmSystemLogEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();//登录手机

        _lmSystemLogEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();//订单ID
        _lmSystemLogEntity.GuestName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["GuestName"].ToString())) ? null : ViewState["GuestName"].ToString();//入住人
        _lmSystemLogEntity.CreateStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatusOtherCreate"].ToString())) ? null : ViewState["BookStatusOtherCreate"].ToString();//创建状态   成功 失败

        _lmSystemLogEntity.UserCancel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UserCancel"].ToString())) ? null : ViewState["UserCancel"].ToString();//用户取消   1 是  2 否
        _lmSystemLogEntity.PriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PriceCode"].ToString())) ? null : ViewState["PriceCode"].ToString(); //价格代码
        _lmSystemLogEntity.AffirmStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatusOtherAffirm"].ToString())) ? null : ViewState["BookStatusOtherAffirm"].ToString();//确认状态

        _lmSystemLogEntity.Timeout = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Timeout"].ToString())) ? null : ViewState["Timeout"].ToString();//确认超时   1是  2 否
        _lmSystemLogEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();//含返现券    1是  2 否
        _lmSystemLogEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();//订单平台

        _lmSystemLogEntity.OrderChannel = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderChannel"].ToString())) ? null : ViewState["OrderChannel"].ToString();//订单渠道
        _lmSystemLogEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();//审核状态 
        _lmSystemLogEntity.HotelComfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();//酒店是否已确认

        _lmSystemLogEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();//LMBAR状态
        _lmSystemLogEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();//支付方式
        _lmSystemLogEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();//支付状态

        #region  优惠券信息
        _lmSystemLogEntity.RestBookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestBookStatus"].ToString())) ? null : ViewState["RestBookStatus"].ToString();
        _lmSystemLogEntity.RestPriceCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RestPriceCode"].ToString())) ? null : ViewState["RestPriceCode"].ToString();

        _lmSystemLogEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
        _lmSystemLogEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
        _lmSystemLogEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();

        _lmSystemLogEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        _lmSystemLogEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        _lmSystemLogEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        _lmSystemLogEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        _lmSystemLogEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        _lmSystemLogEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();
        #endregion


        _lmSystemLogEntity.SortField = gridViewCSReviewLmSystemLogList.Attributes["SortExpression"].ToString();
        _lmSystemLogEntity.SortType = gridViewCSReviewLmSystemLogList.Attributes["SortDirection"].ToString();

        DataSet dsResult = new DataSet();
        if (ViewState["orderXPaymentCode"].ToString() != "" && ViewState["orderXPaymentCode"].ToString() == "QuFen")
        {
            //dsResult = LmSystemLogBP.order_XPayment_ReviewLmOrderLogExport(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["orderInTheNight"].ToString() != "" && ViewState["orderInTheNight"].ToString() == "CKIN")
        {
            //dsResult = LmSystemLogBP.order_InTheNight_ReviewLmOrderLogExport(_lmSystemLogEntity).QueryResult;
        }
        else if (ViewState["Rests"].ToString() != "" && ViewState["Rests"].ToString() == "Rests")
        {
            //dsResult = LmSystemLogBP.ExportLmOrderSelectByRests(_lmSystemLogEntity).QueryResult;//其他  排除已存在的渠道
        }
        else
        {
            dsResult = OrderSearchBP.ExportOrderListSelect(_lmSystemLogEntity).QueryResult;
        }

        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            //for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                if (dsResult.Tables[0].Rows[i]["LOGIN_MOBILE"].ToString() == "" && (dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "HOTELVP" || dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "HOTELVPPRO" || dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "GETAROOM"))
                {
                    dsResult.Tables[0].Rows[i]["LOGIN_MOBILE"] = "游客";
                }
                else if (dsResult.Tables[0].Rows[i]["LOGIN_MOBILE"].ToString() == "" && dsResult.Tables[0].Rows[i]["ORDER_CHANNEL"].ToString() == "QUNAR")
                {
                    dsResult.Tables[0].Rows[i]["LOGIN_MOBILE"] = "QUNAR";
                }

                if (!string.IsNullOrEmpty(ViewState["Timeout"].ToString()))
                {
                    if (!string.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()))
                    {
                        if (ViewState["Timeout"].ToString() == "1")//只取超时订单
                        {
                            if (int.Parse(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()) <= 30)
                            {
                                dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                            }
                        }
                        else//只取没有超时的订单
                        {
                            if (int.Parse(dsResult.Tables[0].Rows[i]["confirmTime"].ToString()) > 30)
                            {
                                dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                            }
                        }
                    }
                    else
                    {
                        dsResult.Tables[0].Rows.Remove(dsResult.Tables[0].Rows[i]);
                    }
                }
            }
        }

        CommonFunction.ExportExcelForLM(dsResult);
    }

    //翻页
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewLmSystemLogListGrid();
    }

    protected void gridViewCSReviewLmSystemLogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i <= gridViewCSReviewLmSystemLogList.Rows.Count; i++)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //模板列 
                ((HyperLink)e.Row.Cells[1].FindControl("hpp")).NavigateUrl = "~/WebUI/UserGroup/UserDetailPage.aspx?UserID=" + ((HyperLink)e.Row.Cells[1].FindControl("hpp")).Text + "&TYPE=2&ID=" + ((HyperLink)e.Row.Cells[17].FindControl("HyperLink1")).Text;
            }
        }
    }

    #region  基础数据
    /// <summary>
    /// 酒店销售人员
    /// </summary>
    private void GetSalesDataTable()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsSalesManager = LmSystemLogBP.SalesManagerSelect(_lmSystemLogEntity).QueryResult;

        DataRow drTemp = dsSalesManager.Tables[0].NewRow();
        drTemp["SALESID"] = DBNull.Value;
        drTemp["User_Account"] = DBNull.Value;
        drTemp["SALESNAME"] = "不限制";
        dsSalesManager.Tables[0].Rows.InsertAt(drTemp, 0);

        ddpSalesManager.DataTextField = "SALESNAME";
        ddpSalesManager.DataValueField = "User_Account";
        ddpSalesManager.DataSource = dsSalesManager;
        ddpSalesManager.DataBind();
        ddpSalesManager.SelectedIndex = 0;
    }

    /// <summary>
    /// 价格代码(订单类型)
    /// </summary>
    /// <returns></returns>
    private void GetPayCodeDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("PAY_CODE");
        DataColumn BookStatusText = new DataColumn("PAY_CODE_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

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
        chkPayCode.DataSource = dt;
        chkPayCode.DataTextField = "PAY_CODE_TEXT";
        chkPayCode.DataValueField = "PAY_CODE";
        chkPayCode.DataBind();
    }

    /// <summary>
    /// 订单确认状态
    /// </summary>
    private void GetAffirmStatus()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("BOOK_STATUS_OTHER_TEXT");
        DataColumn BookStatusText = new DataColumn("BOOK_STATUS_OTHER_CODE");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["BOOK_STATUS_OTHER_CODE"] = "4,5,6,7,8,";
                    dr["BOOK_STATUS_OTHER_TEXT"] = "CC确认";
                    break;
                case "1":
                    dr["BOOK_STATUS_OTHER_CODE"] = "9,";
                    dr["BOOK_STATUS_OTHER_TEXT"] = "CC取消";
                    break;
                case "2":
                    dr["BOOK_STATUS_OTHER_CODE"] = "1";
                    dr["BOOK_STATUS_OTHER_TEXT"] = "待确认";
                    break;
                default:
                    break;
            }
            dt.Rows.Add(dr);
        }
        DataRow drTemp = dt.NewRow();
        drTemp["BOOK_STATUS_OTHER_CODE"] = DBNull.Value;
        drTemp["BOOK_STATUS_OTHER_TEXT"] = "不限制";
        dt.Rows.InsertAt(drTemp, 0);

        ddlAffirmStatus.DataTextField = "BOOK_STATUS_OTHER_TEXT";
        ddlAffirmStatus.DataValueField = "BOOK_STATUS_OTHER_CODE";
        ddlAffirmStatus.DataSource = dt;
        ddlAffirmStatus.DataBind();
        ddlAffirmStatus.SelectedIndex = 0;
    }

    /// <summary>
    /// 应用平台
    /// </summary>
    private void GetPlatForm()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("PLATFORM_TEXT");
        DataColumn BookStatusText = new DataColumn("PLATFORM_CODE");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["PLATFORM_CODE"] = "IOS";
                    dr["PLATFORM_TEXT"] = "IOS";
                    break;
                case "1":
                    dr["PLATFORM_CODE"] = "ANDROID";
                    dr["PLATFORM_TEXT"] = "Android";
                    break;
                case "2":
                    dr["PLATFORM_CODE"] = "WP7,WIN8,WAP,";
                    dr["PLATFORM_TEXT"] = "其他";
                    break;
                default:
                    break;
            }
            dt.Rows.Add(dr);
        }
        DataRow drTemp = dt.NewRow();
        drTemp["PLATFORM_CODE"] = DBNull.Value;
        drTemp["PLATFORM_TEXT"] = "不限制";
        dt.Rows.InsertAt(drTemp, 0);

        ddpPlatForm.DataTextField = "PLATFORM_TEXT";
        ddpPlatForm.DataValueField = "PLATFORM_CODE";
        ddpPlatForm.DataSource = dt;
        ddpPlatForm.DataBind();
        ddpPlatForm.SelectedIndex = 0;
    }

    /// <summary>
    /// 订单渠道
    /// </summary>
    private void GetOrderChannel()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("ORDERCHANNEL_TEXT");
        DataColumn BookStatusText = new DataColumn("ORDERCHANNEL_CODE");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["ORDERCHANNEL_CODE"] = "HOTELVP";
                    dr["ORDERCHANNEL_TEXT"] = "HOTELVP";
                    break;
                case "1":
                    dr["ORDERCHANNEL_CODE"] = "QUNAR";
                    dr["ORDERCHANNEL_TEXT"] = "QUNAR";
                    break;
                case "2":
                    dr["ORDERCHANNEL_CODE"] = "";
                    dr["ORDERCHANNEL_TEXT"] = "其他";
                    break;
                default:
                    break;
            }
            dt.Rows.Add(dr);
        }
        DataRow drTemp = dt.NewRow();
        drTemp["ORDERCHANNEL_CODE"] = DBNull.Value;
        drTemp["ORDERCHANNEL_TEXT"] = "不限制";
        dt.Rows.InsertAt(drTemp, 0);

        ddpOrderChannel.DataTextField = "ORDERCHANNEL_TEXT";
        ddpOrderChannel.DataValueField = "ORDERCHANNEL_CODE";
        ddpOrderChannel.DataSource = dt;
        ddpOrderChannel.DataBind();
        ddpOrderChannel.SelectedIndex = 0;
    }

    /// <summary>
    /// 审核状态
    /// </summary>
    private void GetAprove()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("APROVE_TEXT");
        DataColumn BookStatusText = new DataColumn("APROVE_CODE");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            switch (i.ToString())
            {
                case "0":
                    dr["APROVE_CODE"] = "7"; //null 7 代表待审核
                    dr["APROVE_TEXT"] = "待审核";
                    break;
                case "1":
                    dr["APROVE_CODE"] = "8";
                    dr["APROVE_TEXT"] = "离店";
                    break;
                case "2":
                    dr["APROVE_CODE"] = "5";
                    dr["APROVE_TEXT"] = "NS-SHOW";
                    break;
                default:
                    break;
            }
            dt.Rows.Add(dr);
        }
        DataRow drTemp = dt.NewRow();
        drTemp["APROVE_CODE"] = DBNull.Value;
        drTemp["APROVE_TEXT"] = "不限制";
        dt.Rows.InsertAt(drTemp, 0);

        ddpAprove.DataTextField = "APROVE_TEXT";
        ddpAprove.DataValueField = "APROVE_CODE";
        ddpAprove.DataSource = dt;
        ddpAprove.DataBind();
        ddpAprove.SelectedIndex = 0;
    }

    /// <summary>
    /// 绑定默认值
    /// </summary>
    private void BindCHK()
    {
        this.chkSucceed.Checked = true;
        this.chkUserCancelN.Checked = true;

        for (int i = 0; i < chkPayCode.Items.Count; i++)
        {
            if (!chkPayCode.Items[i].Selected)
            {
                chkPayCode.Items[i].Selected = true;
            }
        }

        this.chkTimeOutN.Checked = true;
        this.chkTimeOutY.Checked = true;

        this.chkTicketY.Checked = true;
        this.chkTicketN.Checked = true;
    }
    #endregion
}