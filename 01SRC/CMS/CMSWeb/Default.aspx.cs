using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Configuration;

public partial class _Default : BasePage
{
    DataSet dsResult = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        //1  今天   0  昨天  edit jason.yu 2012.8.10
        if (!IsPostBack)
        {
            this.StartDate.Value = System.DateTime.Now.ToShortDateString().Replace("/", "-");
            this.EndDate.Value = System.DateTime.Now.ToShortDateString().Replace("/", "-");
            string endDate = DateTime.Parse(this.EndDate.Value).AddDays(1).ToShortDateString();
            this.lkOverallYesterDay.ForeColor = Color.Gray;
            ClearChannel();
            //GetResultData(this.StartDate.Value.Trim(), endDate);
            //BindViewChannelDataList();
            //BindViewOrderDataList();

            //BindUser(this.StartDate.Value.Trim(), endDate);
            //BingTodayLoginUser(this.StartDate.Value.Trim(), endDate);
            BindPlanDetail();
            BindVisibleLable();
        }
    }

    public void GetResultData(string StartDate, string EndDate)
    {
        try
        {
            dsResult.Clear();
            MasterInfoEntity _masterInfoEntity = new MasterInfoEntity();
            CommonEntity _commonEntity = new CommonEntity();

            _masterInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _masterInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _masterInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _masterInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            _masterInfoEntity.MasterInfoDBEntity = new List<MasterInfoDBEntity>();
            MasterInfoDBEntity msterInfoDBEntity = new MasterInfoDBEntity();
            _masterInfoEntity.MasterInfoDBEntity.Add(msterInfoDBEntity);
            msterInfoDBEntity.RegistStart = StartDate;
            msterInfoDBEntity.RegistEnd = EndDate;

            dsResult = MasterInfoBP.CommonSelectOrderChannelData(_masterInfoEntity).QueryResult;
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order-GetResultData.txt", "Default-Order异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
    }

    #region
    //昨夜订单统计
    protected void lkOverallYesterDay_Click(object sender, EventArgs e)
    {
        //this.lkOverallToDay.ForeColor = Color.Gray;
        //this.lkOverallYesterDay.ForeColor = Color.Blue;
        //ClearOrder();
        //ClearChannel();
        //GetResultData(this.StartDate.Value.Trim(), this.EndDate.Value.Trim());
        //BindViewChannelDataList();
        //BindViewOrderDataList();
        //BindVisibleLable();
        //BindUser("0");
        //BingTodayLoginUser("0");
    }
    //今夜订单统计
    protected void lkOverallToDay_Click(object sender, EventArgs e)
    {
        //this.lkOverallToDay.ForeColor = Color.Blue;
        //this.lkOverallYesterDay.ForeColor = Color.Gray;
        //ClearOrder();
        //ClearChannel();
        //GetResultData(this.StartDate.Value.Trim(), this.EndDate.Value.Trim());
        //BindViewChannelDataList();
        //BindViewOrderDataList();
        //BindVisibleLable();
        //BindUser("1");
        //BingTodayLoginUser("1");
    }
    #endregion

    //渠道统计
    public void BindViewChannelDataList()
    {
        decimal BookTotalPrice = 0;
        decimal ResvTypePrice = 0;
        try
        {
            #region
            //MasterInfoEntity _masterInfoEntity = new MasterInfoEntity();
            //CommonEntity _commonEntity = new CommonEntity();

            //_masterInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            //_masterInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            //_masterInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            //_masterInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            //_masterInfoEntity.MasterInfoDBEntity = new List<MasterInfoDBEntity>();
            //MasterInfoDBEntity msterInfoDBEntity = new MasterInfoDBEntity();
            //_masterInfoEntity.MasterInfoDBEntity.Add(msterInfoDBEntity);
            //msterInfoDBEntity.Today = strToDay;

            //DataSet dsResult = MasterInfoBP.CommonSelectChannel(_masterInfoEntity).QueryResult;
            #endregion

            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                if (dsResult.Tables["OrderAll"].Rows.Count > 0)
                {
                    //hidChannelDate
                    this.hidStartDate.Value = dsResult.Tables["OrderAll"].Rows[0]["StartDate"].ToString();
                    this.hidEndDate.Value = dsResult.Tables["OrderAll"].Rows[0]["EndDate"].ToString();

                    string strFOGRESVSTATUS = "";
                    for (int i = 0; i < dsResult.Tables["OrderAll"].Rows.Count; i++)
                    {
                        #region 总订单(券(暂不包含))
                        string channelHotelVP = dsResult.Tables["OrderAll"].Rows[i]["ORDER_CHANNEL"].ToString();
                        string channlePlatform = dsResult.Tables["OrderAll"].Rows[i]["APP_PLATFORM"].ToString();
                        string userCode = dsResult.Tables["OrderAll"].Rows[i]["TICKET_USERCODE"].ToString();
                        string channelBookStatusOther = dsResult.Tables["OrderAll"].Rows[i]["BOOK_STATUS_OTHER"].ToString();
                        string channelPriceCode = dsResult.Tables["OrderAll"].Rows[i]["PRICE_CODE"].ToString();
                        string channelBookStatus = dsResult.Tables["OrderAll"].Rows[i]["BOOK_STATUS"].ToString();

                        strFOGRESVSTATUS = dsResult.Tables["OrderAll"].Rows[i]["FOG_RESVSTATUS"].ToString();

                        #region  HOTELVP-IOS
                        if ((channelHotelVP.Trim() == "HOTELVP" && channlePlatform.Contains("IOS")) || (channelHotelVP.Trim() == "HotelVp" && channlePlatform.Contains("IOS")))
                        {
                            //总订单IOS
                            this.lblIOSChannelOrderAll.Text = (int.Parse(this.lblIOSChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblIOSChannelOrderAllCode.Text = "(" + (int.Parse(this.lblIOSChannelOrderAllCode.Text.Substring(1, this.lblIOSChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认,未确认成功单(券)
                            //已确认成功单(券)  
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //IOS  已确认成功单
                                this.lblIOSChannelAffirmOrder.Text = (int.Parse(this.lblIOSChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblIOSChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblIOSChannelAffirmOrderCode.Text.Substring(1, this.lblIOSChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //IOS  未确认成功单
                                this.lblIOSChannelNotAffirmOrder.Text = (int.Parse(this.lblIOSChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblIOSChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblIOSChannelNotAffirmOrderCode.Text.Substring(1, this.lblIOSChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblIOSChannelcc.Text = (int.Parse(this.lblIOSChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblIOSChannelccCode.Text = "(" + (int.Parse(this.lblIOSChannelccCode.Text.Substring(1, this.lblIOSChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                string id = dsResult.Tables["OrderAll"].Rows[i]["ID"].ToString();
                                //用户取消单(券)
                                this.lblIOSChannelOther.Text = (int.Parse(this.lblIOSChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {

                                    this.lblIOSChannelOtherCode.Text = "(" + (int.Parse(this.lblIOSChannelOtherCode.Text.Substring(1, this.lblIOSChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //else if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3")
                            //    || channelBookStatusOther.Contains("0"))
                            else
                            {
                                this.lblIOSrest.Text = (int.Parse(this.lblIOSrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblIOSrestCode.Text = "(" + (int.Parse(this.lblIOSrestCode.Text.Substring(1, this.lblIOSrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region HOTELVP-ANDROID
                        else if ((channelHotelVP.Trim() == "HOTELVP" && channlePlatform.Contains("ANDROID")) || (channelHotelVP.Trim() == "HotelVp" && channlePlatform.Contains("ANDROID")))
                        {
                            //总订单ANDROID
                            this.lblAdrChannelOrderAll.Text = (int.Parse(this.lblAdrChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblAdrChannelOrderAllCode.Text = "(" + (int.Parse(this.lblAdrChannelOrderAllCode.Text.Substring(1, this.lblAdrChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认,未确认成功单(券)
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //ANDROID  已确认成功单
                                this.lblADRChannelAffirmOrder.Text = (int.Parse(this.lblADRChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblADRChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblADRChannelAffirmOrderCode.Text.Substring(1, this.lblADRChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //ANDROID  未确认成功单
                                this.lblADRChannelNotAffirmOrder.Text = (int.Parse(this.lblADRChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblADRChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblADRChannelNotAffirmOrderCode.Text.Substring(1, this.lblADRChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblADRChannelcc.Text = (int.Parse(this.lblADRChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblADRChannelccCode.Text = "(" + (int.Parse(this.lblADRChannelccCode.Text.Substring(1, this.lblADRChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblADRChannelOther.Text = (int.Parse(this.lblADRChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblADRChannelOtherCode.Text = "(" + (int.Parse(this.lblADRChannelOtherCode.Text.Substring(1, this.lblADRChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                            //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                            else
                            {
                                this.lblADRrest.Text = (int.Parse(this.lblADRrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblADRrestCode.Text = "(" + (int.Parse(this.lblADRrestCode.Text.Substring(1, this.lblADRrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion
                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region HOTELVP-WP7
                        else if ((channelHotelVP.Trim() == "HOTELVP" && channlePlatform.Contains("WP")) || (channelHotelVP.Trim() == "HotelVp" && channlePlatform.Contains("WP")))
                        {
                            //总订单WP7
                            this.lblWPChannelOrderAll.Text = (int.Parse(this.lblWPChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblWPChannelOrderAllCode.Text = "(" + (int.Parse(this.lblWPChannelOrderAllCode.Text.Substring(1, this.lblWPChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认,未确认成功单(券)
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //WP7  已确认成功单
                                this.lblWPChannelAffirmOrder.Text = (int.Parse(this.lblWPChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWPChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblWPChannelAffirmOrderCode.Text.Substring(1, this.lblWPChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //WP7  未确认成功单
                                this.lblWPChannelNotAffirmOrder.Text = (int.Parse(this.lblWPChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWPChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblWPChannelNotAffirmOrderCode.Text.Substring(1, this.lblWPChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblWPChannelcc.Text = (int.Parse(this.lblWPChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWPChannelccCode.Text = "(" + (int.Parse(this.lblWPChannelccCode.Text.Substring(1, this.lblWPChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblWPChannelOther.Text = (int.Parse(this.lblWPChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWPChannelOtherCode.Text = "(" + (int.Parse(this.lblWPChannelOtherCode.Text.Substring(1, this.lblWPChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                            //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                            else
                            {
                                this.lblWPrest.Text = (int.Parse(this.lblWPrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWPrestCode.Text = "(" + (int.Parse(this.lblWPrestCode.Text.Substring(1, this.lblWPrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region HOTELVP-W8
                        else if ((channelHotelVP.Trim() == "HOTELVP" && channlePlatform.Contains("WINDOWS8")) || (channelHotelVP.Trim() == "HotelVp" && channlePlatform.Contains("windows8")))
                        {
                            //总订单W8
                            this.lblW8ChannelOrderAll.Text = (int.Parse(this.lblW8ChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblW8ChannelOrderAllCode.Text = "(" + (int.Parse(this.lblW8ChannelOrderAllCode.Text.Substring(1, this.lblW8ChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认,未确认成功单(券)
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //W8  已确认成功单
                                this.lblW8ChannelAffirmOrder.Text = (int.Parse(this.lblW8ChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblW8ChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblW8ChannelAffirmOrderCode.Text.Substring(1, this.lblW8ChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //WP7  未确认成功单
                                this.lblW8ChannelNotAffirmOrder.Text = (int.Parse(this.lblW8ChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblW8ChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblW8ChannelNotAffirmOrderCode.Text.Substring(1, this.lblW8ChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblW8Channelcc.Text = (int.Parse(this.lblWPChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblW8ChannelccCode.Text = "(" + (int.Parse(this.lblW8ChannelccCode.Text.Substring(1, this.lblW8ChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblW8ChannelOther.Text = (int.Parse(this.lblW8ChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblW8ChannelOtherCode.Text = "(" + (int.Parse(this.lblW8ChannelOtherCode.Text.Substring(1, this.lblW8ChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                            //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                            else
                            {
                                this.lblW8rest.Text = (int.Parse(this.lblW8rest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblW8restCode.Text = "(" + (int.Parse(this.lblW8restCode.Text.Substring(1, this.lblW8restCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region HOTEL-WAP
                        else if ((channelHotelVP.Trim() == "HOTELVP" && channlePlatform.Contains("WAP")) || (channelHotelVP.Trim() == "HotelVp" && channlePlatform.Contains("WAP")))
                        {
                            //总订单WP7
                            this.lblWAPChannelOrderAll.Text = (int.Parse(this.lblWAPChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblWAPChannelOrderAllCode.Text = "(" + (int.Parse(this.lblWAPChannelOrderAllCode.Text.Substring(1, this.lblWAPChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认,未确认成功单(券)
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //WP7  已确认成功单
                                this.lblWAPChannelAffirmOrder.Text = (int.Parse(this.lblWAPChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWAPChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblWAPChannelAffirmOrderCode.Text.Substring(1, this.lblWAPChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //WP7  未确认成功单
                                this.lblWAPChannelNotAffirmOrder.Text = (int.Parse(this.lblWAPChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWAPChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblWAPChannelNotAffirmOrderCode.Text.Substring(1, this.lblWAPChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblWAPChannelcc.Text = (int.Parse(this.lblWAPChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWAPChannelccCode.Text = "(" + (int.Parse(this.lblWAPChannelccCode.Text.Substring(1, this.lblWAPChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblWAPChannelOther.Text = (int.Parse(this.lblWAPChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWAPChannelOtherCode.Text = "(" + (int.Parse(this.lblWAPChannelOtherCode.Text.Substring(1, this.lblWAPChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                            //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                            else
                            {
                                this.lblWAPrest.Text = (int.Parse(this.lblWAPrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblWAPrestCode.Text = "(" + (int.Parse(this.lblWAPrestCode.Text.Substring(1, this.lblWAPrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region HOTELVPPRO
                        else if (channelHotelVP.Trim() == "HOTELVPPRO")
                        {
                            //总订单QUNAR
                            this.lblProChannelOrderAll.Text = (int.Parse(this.lblProChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblProChannelOrderAllCode.Text = "(" + (int.Parse(this.lblProChannelOrderAllCode.Text.Substring(1, this.lblProChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认成功单(券(暂不包含))
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //QUNAR  已确认成功单
                                this.lblProChannelAffirmOrder.Text = (int.Parse(this.lblProChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblProChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblProChannelAffirmOrderCode.Text.Substring(1, this.lblProChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //QUNAR  未确认成功单
                                this.lblProChannelNotAffirmOrder.Text = (int.Parse(this.lblProChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblProChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblProChannelNotAffirmOrderCode.Text.Substring(1, this.lblProChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblProChannelcc.Text = (int.Parse(this.lblProChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblProChannelccCode.Text = "(" + (int.Parse(this.lblProChannelccCode.Text.Substring(1, this.lblProChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblProChannelOther.Text = (int.Parse(this.lblProChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblProChannelOtherCode.Text = "(" + (int.Parse(this.lblProChannelOtherCode.Text.Substring(1, this.lblProChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                            //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                            else
                            {
                                this.lblProrest.Text = (int.Parse(this.lblProrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblProrestCode.Text = "(" + (int.Parse(this.lblProrestCode.Text.Substring(1, this.lblProrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region GETAROOM
                        else if (channelHotelVP.Trim() == "GETAROOM")
                        {
                            //总订单QUNAR
                            this.lblGETAROOMOrderAll.Text = (int.Parse(this.lblGETAROOMOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblGETAROOMOrderAllCode.Text = "(" + (int.Parse(this.lblGETAROOMOrderAllCode.Text.Substring(1, this.lblGETAROOMOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认成功单(券(暂不包含))
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //QUNAR  已确认成功单
                                this.lblGETAROOMAffirmOrder.Text = (int.Parse(this.lblGETAROOMAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblGETAROOMAffirmOrderCode.Text = "(" + (int.Parse(this.lblGETAROOMAffirmOrderCode.Text.Substring(1, this.lblGETAROOMAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //QUNAR  未确认成功单
                                this.lblGETAROOMNotAffirmOrder.Text = (int.Parse(this.lblGETAROOMNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblGETAROOMNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblGETAROOMNotAffirmOrderCode.Text.Substring(1, this.lblGETAROOMNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblGETAROOMChannelcc.Text = (int.Parse(this.lblGETAROOMChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblGETAROOMChannelccCode.Text = "(" + (int.Parse(this.lblGETAROOMChannelccCode.Text.Substring(1, this.lblGETAROOMChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblGETAROOMChannelOther.Text = (int.Parse(this.lblGETAROOMChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblGETAROOMChannelOtherCode.Text = "(" + (int.Parse(this.lblGETAROOMChannelOtherCode.Text.Substring(1, this.lblGETAROOMChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                            //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                            else
                            {
                                this.lblGETAROOMrest.Text = (int.Parse(this.lblGETAROOMrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblGETAROOMrestCode.Text = "(" + (int.Parse(this.lblGETAROOMrestCode.Text.Substring(1, this.lblGETAROOMrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region HMBST
                        //if (channelHotelVP.Trim() == "HMBST")
                        //{
                        //    //总订单HMBST
                        //    this.lblHMBSTOrderAll.Text = (int.Parse(this.lblHMBSTOrderAll.Text) + 1).ToString();
                        //    //是否使用劵
                        //    if (userCode != null && userCode != "")
                        //    {
                        //        this.lblHMBSTOrderAllCode.Text = "(" + (int.Parse(this.lblHMBSTOrderAllCode.Text.Substring(1, this.lblHMBSTOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //    }

                        //    #region 已确认成功单(券(暂不包含))
                        //    //已确认成功单(券)
                        //    if (channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                        //    {
                        //        //QUNAR  已确认成功单
                        //        this.lblHMBSTAffirmOrder.Text = (int.Parse(this.lblHMBSTAffirmOrder.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblHMBSTAffirmOrderCode.Text = "(" + (int.Parse(this.lblHMBSTAffirmOrderCode.Text.Substring(1, this.lblHMBSTAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //        if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //        {
                        //            ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //        }
                        //    }
                        //    //未确认成功单(券)
                        //    else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                        //    {
                        //        //QUNAR  未确认成功单
                        //        this.lblHMBSTNotAffirmOrder.Text = (int.Parse(this.lblHMBSTNotAffirmOrder.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblHMBSTNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblHMBSTNotAffirmOrderCode.Text.Substring(1, this.lblHMBSTNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //        if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //        {
                        //            ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //        }
                        //    }
                        //    #endregion

                        //    #region CC取消单(券)
                        //    else if (channelBookStatusOther.Contains("9"))
                        //    {
                        //        //IOS  cc取消单
                        //        this.lblHMBSTChannelcc.Text = (int.Parse(this.lblHMBSTChannelcc.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblHMBSTChannelccCode.Text = "(" + (int.Parse(this.lblHMBSTChannelccCode.Text.Substring(1, this.lblHMBSTChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    #region 用户取消单(券)
                        //    else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                        //    {
                        //        //用户取消单(券)
                        //        this.lblHMBSTChannelOther.Text = (int.Parse(this.lblHMBSTChannelOther.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblHMBSTChannelOtherCode.Text = "(" + (int.Parse(this.lblHMBSTChannelOtherCode.Text.Substring(1, this.lblHMBSTChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    #region  其他
                        //    //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                        //    //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                        //    //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                        //    else
                        //    {
                        //        this.lblHMBSTrest.Text = (int.Parse(this.lblHMBSTrest.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblHMBSTrestCode.Text = "(" + (int.Parse(this.lblHMBSTrestCode.Text.Substring(1, this.lblHMBSTrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //    {
                        //        BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //    }
                        //}
                        #endregion

                        #region QUNar
                        else if (channelHotelVP.Trim() == "QUNAR")
                        {
                            //总订单QUNAR
                            this.lblQUNarChannelOrderAll.Text = (int.Parse(this.lblQUNarChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblQUNarChannelOrderAllCode.Text = "(" + (int.Parse(this.lblQUNarChannelOrderAllCode.Text.Substring(1, this.lblQUNarChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认成功单(券(暂不包含))
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //QUNAR  已确认成功单
                                this.lblQUNarChannelAffirmOrder.Text = (int.Parse(this.lblQUNarChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblQUNarChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblQUNarChannelAffirmOrderCode.Text.Substring(1, this.lblQUNarChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //QUNAR  未确认成功单
                                this.lblQUNarChannelNotAffirmOrder.Text = (int.Parse(this.lblQUNarChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblQUNarChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblQUNarChannelNotAffirmOrderCode.Text.Substring(1, this.lblQUNarChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblQUNarChannelcc.Text = (int.Parse(this.lblQUNarChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblQUNarChannelccCode.Text = "(" + (int.Parse(this.lblQUNarChannelccCode.Text.Substring(1, this.lblQUNarChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblQUNarChannelOther.Text = (int.Parse(this.lblQUNarChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblQUNarChannelOtherCode.Text = "(" + (int.Parse(this.lblQUNarChannelOtherCode.Text.Substring(1, this.lblQUNarChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                            //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                            else
                            {
                                this.lblQUNarrest.Text = (int.Parse(this.lblQUNarrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblQUNarrestCode.Text = "(" + (int.Parse(this.lblQUNarrestCode.Text.Substring(1, this.lblQUNarrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region 116114
                        //if (channelHotelVP.Trim() == "116114")
                        //{
                        //    //总订单116114
                        //    this.lbl11ChannelOrderAll.Text = (int.Parse(this.lbl11ChannelOrderAll.Text) + 1).ToString();
                        //    //是否使用劵
                        //    if (userCode != null && userCode != "")
                        //    {
                        //        this.lbl11ChannelOrderAllCode.Text = "(" + (int.Parse(this.lbl11ChannelOrderAllCode.Text.Substring(1, this.lbl11ChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //    }

                        //    #region 已确认成功单(券(暂不包含))
                        //    if (channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                        //    {
                        //        //116114  已确认成功单
                        //        this.lbl11ChannelAffirmOrder.Text = (int.Parse(this.lbl11ChannelAffirmOrder.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lbl11ChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lbl11ChannelAffirmOrderCode.Text.Substring(1, this.lbl11ChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //        if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //        {
                        //            ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //        }
                        //    }
                        //    //未确认成功单(券)
                        //    else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                        //    {
                        //        //116114  未确认成功单
                        //        this.lbl11ChannelNotAffirmOrder.Text = (int.Parse(this.lbl11ChannelNotAffirmOrder.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lbl11ChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lbl11ChannelNotAffirmOrderCode.Text.Substring(1, this.lbl11ChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //        if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //        {
                        //            ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //        }
                        //    }
                        //    #endregion

                        //    #region CC取消单(券)
                        //    else if (channelBookStatusOther.Contains("9"))
                        //    {
                        //        //IOS  cc取消单
                        //        this.lbl11Channelcc.Text = (int.Parse(this.lbl11Channelcc.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lbl11ChannelccCode.Text = "(" + (int.Parse(this.lbl11ChannelccCode.Text.Substring(1, this.lbl11ChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    #region 用户取消单(券)
                        //    else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                        //    {
                        //        //用户取消单(券)
                        //        this.lbl11ChannelOther.Text = (int.Parse(this.lbl11ChannelOther.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lbl11ChannelOtherCode.Text = "(" + (int.Parse(this.lbl11ChannelOtherCode.Text.Substring(1, this.lbl11ChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    #region  其他
                        //    //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                        //    //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                        //    //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                        //    else
                        //    {
                        //        this.lbl11rest.Text = (int.Parse(this.lbl11rest.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lbl11restCode.Text = "(" + (int.Parse(this.lbl11restCode.Text.Substring(1, this.lbl11restCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //    {
                        //        BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //    }
                        //}
                        #endregion

                        #region MOJI
                        //if (channelHotelVP.Trim() == "MOJI")
                        //{
                        //    //总订单MOJI
                        //    this.lblMJChannelOrderAll.Text = (int.Parse(this.lblMJChannelOrderAll.Text) + 1).ToString();
                        //    //是否使用劵
                        //    if (userCode != null && userCode != "")
                        //    {
                        //        this.lblMJChannelOrderAllCode.Text = "(" + (int.Parse(this.lblMJChannelOrderAllCode.Text.Substring(1, this.lblMJChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //    }

                        //    #region 已确认成功单(券(暂不包含))
                        //    //已确认成功单(券)
                        //    if (channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                        //    {
                        //        //MOJI  已确认成功单
                        //        this.lblMJChannelAffirmOrder.Text = (int.Parse(this.lblMJChannelAffirmOrder.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblMJChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblMJChannelAffirmOrderCode.Text.Substring(1, this.lblMJChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //        if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //        {
                        //            ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //        }
                        //    }
                        //    //未确认成功单(券)
                        //    else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                        //    {
                        //        //MOJI  未确认成功单
                        //        this.lblMJChannelNotAffirmOrder.Text = (int.Parse(this.lblMJChannelNotAffirmOrder.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblMJChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblMJChannelNotAffirmOrderCode.Text.Substring(1, this.lblMJChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //        if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //        {
                        //            ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //        }
                        //    }
                        //    #endregion

                        //    #region CC取消单(券)
                        //    else if (channelBookStatusOther.Contains("9"))
                        //    {
                        //        //IOS  cc取消单
                        //        this.lblMJChannelcc.Text = (int.Parse(this.lblMJChannelcc.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblMJChannelccCode.Text = "(" + (int.Parse(this.lblMJChannelccCode.Text.Substring(1, this.lblMJChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    #region 用户取消单(券)
                        //    else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                        //    {
                        //        //用户取消单(券)
                        //        this.lblMJChannelOther.Text = (int.Parse(this.lblMJChannelOther.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblMJChannelOtherCode.Text = "(" + (int.Parse(this.lblMJChannelOtherCode.Text.Substring(1, this.lblMJChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    #region  其他
                        //    //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                        //    //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                        //    //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                        //    else
                        //    {
                        //        this.lblMJrest.Text = (int.Parse(this.lblMJrest.Text) + 1).ToString();
                        //        //是否使用劵
                        //        if (userCode != null && userCode != "")
                        //        {
                        //            this.lblMJrestCode.Text = "(" + (int.Parse(this.lblMJrestCode.Text.Substring(1, this.lblMJrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                        //        }
                        //    }
                        //    #endregion

                        //    if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                        //    {
                        //        BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                        //    }
                        //}
                        #endregion

                        #region HOTELVPMAP
                        else if (channelHotelVP.Trim() == "HOTELVPMAP")
                        {
                            //总订单QUNAR
                            this.lblHotelvpMapChannelOrderAll.Text = (int.Parse(this.lblHotelvpMapChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblHotelvpMapChannelOrderAllCode.Text = "(" + (int.Parse(this.lblHotelvpMapChannelOrderAllCode.Text.Substring(1, this.lblHotelvpMapChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认成功单(券(暂不包含))
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //QUNAR  已确认成功单
                                this.lblHotelvpMapChannelAffirmOrder.Text = (int.Parse(this.lblHotelvpMapChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblHotelvpMapChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblHotelvpMapChannelAffirmOrderCode.Text.Substring(1, this.lblHotelvpMapChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //QUNAR  未确认成功单
                                this.lblHotelvpMapChannelNotAffirmOrder.Text = (int.Parse(this.lblHotelvpMapChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblHotelvpMapChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblHotelvpMapChannelNotAffirmOrderCode.Text.Substring(1, this.lblHotelvpMapChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblHotelvpMapChannelcc.Text = (int.Parse(this.lblHotelvpMapChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblHotelvpMapChannelccCode.Text = "(" + (int.Parse(this.lblHotelvpMapChannelccCode.Text.Substring(1, this.lblHotelvpMapChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblHotelvpMapChannelOther.Text = (int.Parse(this.lblHotelvpMapChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblHotelvpMapChannelOtherCode.Text = "(" + (int.Parse(this.lblHotelvpMapChannelOtherCode.Text.Substring(1, this.lblHotelvpMapChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            //if (!channelBookStatusOther.Contains("4") || !channelBookStatusOther.Contains("5") || !channelBookStatusOther.Contains("6") || !channelBookStatusOther.Contains("7") || !channelBookStatusOther.Contains("8") || !channelBookStatusOther.Contains("1") || !channelBookStatusOther.Contains("9") || !channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("2")))
                            //if ((channelPriceCode == "LMBAR" && !"145".Contains(channelBookStatus))
                            //    && !channelBookStatusOther.Contains("4") && !channelBookStatusOther.Contains("5") && !channelBookStatusOther.Contains("6") && !channelBookStatusOther.Contains("7") && !channelBookStatusOther.Contains("8") && !channelBookStatusOther.Contains("1") && !channelBookStatusOther.Contains("9") && !channelBookStatusOther.Contains("3"))
                            else
                            {
                                this.lblHotelvpMaprest.Text = (int.Parse(this.lblHotelvpMaprest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblHotelvpMaprestCode.Text = "(" + (int.Parse(this.lblHotelvpMaprestCode.Text.Substring(1, this.lblHotelvpMaprestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region   其他
                        else
                        {
                            //总订单QUNAR
                            this.lblRestsChannelOrderAll.Text = (int.Parse(this.lblRestsChannelOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblRestsChannelOrderAllCode.Text = "(" + (int.Parse(this.lblRestsChannelOrderAllCode.Text.Substring(1, this.lblRestsChannelOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }

                            #region 已确认成功单(券(暂不包含))
                            //已确认成功单(券)
                            if (((channelBookStatusOther.Contains("4") || channelBookStatusOther.Contains("5") || channelBookStatusOther.Contains("6") || channelBookStatusOther.Contains("7") || channelBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS)) || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("5")))
                            {
                                //QUNAR  已确认成功单
                                this.lblRestsChannelAffirmOrder.Text = (int.Parse(this.lblRestsChannelAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblRestsChannelAffirmOrderCode.Text = "(" + (int.Parse(this.lblRestsChannelAffirmOrderCode.Text.Substring(1, this.lblRestsChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            else if (channelBookStatusOther.Contains("1") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("1")))
                            {
                                //QUNAR  未确认成功单
                                this.lblRestsChannelNotAffirmOrder.Text = (int.Parse(this.lblRestsChannelNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblRestsChannelNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblRestsChannelNotAffirmOrderCode.Text.Substring(1, this.lblRestsChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            else if (channelBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblRestsChannelcc.Text = (int.Parse(this.lblRestsChannelcc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblRestsChannelccCode.Text = "(" + (int.Parse(this.lblRestsChannelccCode.Text.Substring(1, this.lblRestsChannelccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            else if (channelBookStatusOther.Contains("3") || (channelPriceCode == "LMBAR" && channelBookStatus.Contains("4")))
                            {
                                //用户取消单(券)
                                this.lblRestsChannelOther.Text = (int.Parse(this.lblRestsChannelOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblRestsChannelOtherCode.Text = "(" + (int.Parse(this.lblRestsChannelOtherCode.Text.Substring(1, this.lblRestsChannelOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            else
                            {
                                this.lblRestsrest.Text = (int.Parse(this.lblRestsrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblRestsrestCode.Text = "(" + (int.Parse(this.lblRestsrestCode.Text.Substring(1, this.lblRestsrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion
                        #endregion
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-查询和分类 异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }

        try
        {

            #region  计算总数
            this.lblChannelOrderAllCount.Text = (int.Parse(this.lblIOSChannelOrderAll.Text) + int.Parse(this.lblAdrChannelOrderAll.Text) + int.Parse(this.lblWPChannelOrderAll.Text) + int.Parse(this.lblW8ChannelOrderAll.Text) + int.Parse(this.lblWAPChannelOrderAll.Text) + int.Parse(this.lblProChannelOrderAll.Text) + int.Parse(this.lblGETAROOMOrderAll.Text) + int.Parse(this.lblHMBSTOrderAll.Text) + int.Parse(this.lblQUNarChannelOrderAll.Text) + int.Parse(this.lbl11ChannelOrderAll.Text) + int.Parse(this.lblMJChannelOrderAll.Text) + int.Parse(this.lblHotelvpMapChannelOrderAll.Text) + int.Parse(this.lblRestsChannelOrderAll.Text)).ToString();//总数
            this.lblChannelOrderAllCodeCount.Text = "(" + (int.Parse(this.lblIOSChannelOrderAllCode.Text.Substring(1, this.lblIOSChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblAdrChannelOrderAllCode.Text.Substring(1, this.lblAdrChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWPChannelOrderAllCode.Text.Substring(1, this.lblWPChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblW8ChannelOrderAllCode.Text.Substring(1, this.lblW8ChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWAPChannelOrderAllCode.Text.Substring(1, this.lblWAPChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblProChannelOrderAllCode.Text.Substring(1, this.lblProChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblGETAROOMOrderAllCode.Text.Substring(1, this.lblGETAROOMOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHMBSTOrderAllCode.Text.Substring(1, this.lblHMBSTOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblQUNarChannelOrderAllCode.Text.Substring(1, this.lblQUNarChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lbl11ChannelOrderAllCode.Text.Substring(1, this.lbl11ChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblMJChannelOrderAllCode.Text.Substring(1, this.lblMJChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHotelvpMapChannelOrderAllCode.Text.Substring(1, this.lblHotelvpMapChannelOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblRestsChannelOrderAllCode.Text.Substring(1, this.lblRestsChannelOrderAllCode.Text.IndexOf(")") - 1))).ToString() + ")";//总券数
            this.lblChannelOrderAllPercent.Text = "100%";
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--AllCount  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }

        try
        {
            this.lblChannelAffirmOrderCount.Text = (int.Parse(this.lblIOSChannelAffirmOrder.Text) + int.Parse(this.lblADRChannelAffirmOrder.Text) + int.Parse(this.lblWPChannelAffirmOrder.Text) + int.Parse(this.lblW8ChannelAffirmOrder.Text) + int.Parse(this.lblWAPChannelAffirmOrder.Text) + int.Parse(this.lblProChannelAffirmOrder.Text) + int.Parse(this.lblGETAROOMAffirmOrder.Text) + int.Parse(this.lblHMBSTAffirmOrder.Text) + int.Parse(this.lblQUNarChannelAffirmOrder.Text) + int.Parse(this.lbl11ChannelAffirmOrder.Text) + int.Parse(this.lblMJChannelAffirmOrder.Text) + int.Parse(this.lblHotelvpMapChannelAffirmOrder.Text) + int.Parse(this.lblRestsChannelAffirmOrder.Text)).ToString();//总确认成功单数
            this.lblChannelAffirmOrderCodeCount.Text = "(" + (int.Parse(this.lblIOSChannelAffirmOrderCode.Text.Substring(1, this.lblIOSChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblADRChannelAffirmOrderCode.Text.Substring(1, this.lblADRChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWPChannelAffirmOrderCode.Text.Substring(1, this.lblWPChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblW8ChannelAffirmOrderCode.Text.Substring(1, this.lblW8ChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWAPChannelAffirmOrderCode.Text.Substring(1, this.lblWAPChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblProChannelAffirmOrderCode.Text.Substring(1, this.lblProChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblGETAROOMAffirmOrderCode.Text.Substring(1, this.lblGETAROOMAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHMBSTAffirmOrderCode.Text.Substring(1, this.lblHMBSTAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblQUNarChannelAffirmOrderCode.Text.Substring(1, this.lblQUNarChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lbl11ChannelAffirmOrderCode.Text.Substring(1, this.lbl11ChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblMJChannelAffirmOrderCode.Text.Substring(1, this.lblMJChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHotelvpMapChannelAffirmOrderCode.Text.Substring(1, this.lblHotelvpMapChannelAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblRestsChannelAffirmOrderCode.Text.Substring(1, this.lblRestsChannelAffirmOrderCode.Text.IndexOf(")") - 1))).ToString() + ")";//总确认成功单总券数
            if (lblChannelAffirmOrderCount.Text == "0" || lblChannelOrderAllCount.Text == "0")
            {
                this.lblChannelAffirmOrderPercent.Text = "0%";
            }
            else
            {
                string AffirmorderPercent = ((decimal.Parse(this.lblChannelAffirmOrderCount.Text) / decimal.Parse(this.lblChannelOrderAllCount.Text)) * 100).ToString();
                this.lblChannelAffirmOrderPercent.Text = (AffirmorderPercent.IndexOf(".") > 0 && AffirmorderPercent.Split('.')[1].Length > 2) ? AffirmorderPercent.Substring(0, AffirmorderPercent.IndexOf(".")) + "." + AffirmorderPercent.Substring(AffirmorderPercent.IndexOf(".") + 1, 2) + "%" : AffirmorderPercent + "%";
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--OrderCount  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }

        try
        {
            this.lblChannelNotAffirmOrderCount.Text = (int.Parse(this.lblIOSChannelNotAffirmOrder.Text) + int.Parse(this.lblADRChannelNotAffirmOrder.Text) + int.Parse(this.lblWPChannelNotAffirmOrder.Text) + int.Parse(this.lblW8ChannelNotAffirmOrder.Text) + int.Parse(this.lblWAPChannelNotAffirmOrder.Text) + int.Parse(this.lblProChannelNotAffirmOrder.Text) + int.Parse(this.lblGETAROOMNotAffirmOrder.Text) + int.Parse(lblHMBSTNotAffirmOrder.Text) + int.Parse(this.lblQUNarChannelNotAffirmOrder.Text) + int.Parse(this.lbl11ChannelNotAffirmOrder.Text) + int.Parse(this.lblMJChannelNotAffirmOrder.Text) + int.Parse(this.lblHotelvpMapChannelNotAffirmOrder.Text) + int.Parse(this.lblRestsChannelNotAffirmOrder.Text)).ToString();//总确认成功单数
            this.lblChannelNotAffirmOrderCodeCount.Text = "(" + (int.Parse(this.lblIOSChannelNotAffirmOrderCode.Text.Substring(1, this.lblIOSChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblADRChannelNotAffirmOrderCode.Text.Substring(1, this.lblADRChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWPChannelNotAffirmOrderCode.Text.Substring(1, this.lblWPChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblW8ChannelNotAffirmOrderCode.Text.Substring(1, this.lblW8ChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWAPChannelNotAffirmOrderCode.Text.Substring(1, this.lblWAPChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblProChannelNotAffirmOrderCode.Text.Substring(1, this.lblProChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblGETAROOMNotAffirmOrderCode.Text.Substring(1, this.lblGETAROOMNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHMBSTNotAffirmOrderCode.Text.Substring(1, this.lblHMBSTNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblQUNarChannelNotAffirmOrderCode.Text.Substring(1, this.lblQUNarChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lbl11ChannelNotAffirmOrderCode.Text.Substring(1, this.lbl11ChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblMJChannelNotAffirmOrderCode.Text.Substring(1, this.lblMJChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHotelvpMapChannelNotAffirmOrderCode.Text.Substring(1, this.lblHotelvpMapChannelNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblRestsChannelNotAffirmOrderCode.Text.Substring(1, this.lblRestsChannelNotAffirmOrderCode.Text.IndexOf(")") - 1))).ToString() + ")";//总确认成功单总券数
            if (lblChannelNotAffirmOrderCount.Text == "0" || lblChannelOrderAllCount.Text == "0")
            {
                this.lblChannelNotAffirmOrderPercent.Text = "0%";
            }
            else
            {
                string NotAffirmOrderPercent = ((decimal.Parse(this.lblChannelNotAffirmOrderCount.Text) / decimal.Parse(this.lblChannelOrderAllCount.Text)) * 100).ToString();
                this.lblChannelNotAffirmOrderPercent.Text = (NotAffirmOrderPercent.IndexOf(".") > 0 && NotAffirmOrderPercent.Split('.')[1].Length > 2) ? NotAffirmOrderPercent.Substring(0, NotAffirmOrderPercent.IndexOf(".")) + "." + NotAffirmOrderPercent.Substring(NotAffirmOrderPercent.IndexOf(".") + 1, 2) + "%" : NotAffirmOrderPercent + "%";
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--NotAffirmOrderCount  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }

        try
        {
            this.lblChannelccCount.Text = (int.Parse(this.lblIOSChannelcc.Text) + int.Parse(this.lblADRChannelcc.Text) + int.Parse(this.lblWPChannelcc.Text) + int.Parse(this.lblW8Channelcc.Text) + int.Parse(this.lblWAPChannelcc.Text) + int.Parse(this.lblProChannelcc.Text) + int.Parse(this.lblGETAROOMChannelcc.Text) + int.Parse(this.lblHMBSTChannelcc.Text) + int.Parse(this.lblQUNarChannelcc.Text) + int.Parse(this.lbl11Channelcc.Text) + int.Parse(this.lblMJChannelcc.Text) + int.Parse(this.lblHotelvpMapChannelcc.Text) + int.Parse(this.lblRestsChannelcc.Text)).ToString();//总确认成功单数
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--ccCount1  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
        try
        {


            this.lblChannelccCodeCount.Text = "(" + (int.Parse(this.lblIOSChannelccCode.Text.Substring(1, this.lblIOSChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblADRChannelccCode.Text.Substring(1, this.lblADRChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWPChannelccCode.Text.Substring(1, this.lblWPChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblW8ChannelccCode.Text.Substring(1, this.lblW8ChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWAPChannelccCode.Text.Substring(1, this.lblWAPChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblProChannelccCode.Text.Substring(1, this.lblProChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblGETAROOMChannelccCode.Text.Substring(1, this.lblGETAROOMChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHMBSTChannelccCode.Text.Substring(1, this.lblHMBSTChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblQUNarChannelccCode.Text.Substring(1, this.lblQUNarChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lbl11ChannelccCode.Text.Substring(1, this.lbl11ChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblMJChannelccCode.Text.Substring(1, this.lblMJChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHotelvpMapChannelccCode.Text.Substring(1, this.lblHotelvpMapChannelccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblRestsChannelccCode.Text.Substring(1, this.lblRestsChannelccCode.Text.IndexOf(")") - 1))).ToString() + ")";//总券数


            //this.lblChannelccCodeCount.Text = "(" + (int.Parse(this.lblIOSChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblIOSChannelccCode.Text.Substring(1, this.lblIOSChannelccCode.Text.IndexOf(")") - 1)) +                                                             int.Parse(this.lblADRChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblADRChannelccCode.Text.Substring(1, this.lblADRChannelccCode.Text.IndexOf(")") - 1)) +                                                             int.Parse(this.lblWPChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblWPChannelccCode.Text.Substring(1, this.lblWPChannelccCode.Text.IndexOf(")") - 1)) + 
            //                                         int.Parse(this.lblW8ChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblW8ChannelccCode.Text.Substring(1, this.lblW8ChannelccCode.Text.IndexOf(")") - 1)) + 
            //                                         int.Parse(this.lblWAPChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblWAPChannelccCode.Text.Substring(1, this.lblWAPChannelccCode.Text.IndexOf(")") - 1)) +                                                             int.Parse(this.lblProChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblProChannelccCode.Text.Substring(1, this.lblProChannelccCode.Text.IndexOf(")") - 1)) +                                                             int.Parse(this.lblGETAROOMChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblGETAROOMChannelccCode.Text.Substring(1, this.lblGETAROOMChannelccCode.Text.IndexOf(")") - 1)) +
            //                                         int.Parse(this.lblGETAROOMChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblHMBSTChannelccCode.Text.Substring(1, this.lblHMBSTChannelccCode.Text.IndexOf(")") - 1)) +
            //                                         int.Parse(this.lblGETAROOMChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblQUNarChannelccCode.Text.Substring(1, this.lblQUNarChannelccCode.Text.IndexOf(")") - 1)) +
            //                                         int.Parse(this.lblGETAROOMChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lbl11ChannelccCode.Text.Substring(1, this.lbl11ChannelccCode.Text.IndexOf(")") - 1)) +
            //                                         int.Parse(this.lblGETAROOMChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblMJChannelccCode.Text.Substring(1, this.lblMJChannelccCode.Text.IndexOf(")") - 1)) +
            //                                         int.Parse(this.lblGETAROOMChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblHotelvpMapChannelccCode.Text.Substring(1, this.lblHotelvpMapChannelccCode.Text.IndexOf(")") - 1)) +
            //                                         int.Parse(this.lblGETAROOMChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblRestsChannelccCode.Text.Substring(1, this.lblRestsChannelccCode.Text.IndexOf(")") - 1))).ToString() + ")";//总券数

            //int.Parse(this.lblIOSChannelccCode.Text.IndexOf(")") > 1 ? "0" : this.lblIOSChannelccCode.Text.Substring(1, this.lblIOSChannelccCode.Text.IndexOf(")") - 1))


        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--ccCount2  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
        try
        {
            if (lblChannelccCount.Text == "0" || lblChannelOrderAllCount.Text == "0")
            {
                this.lblChannelccCodePercent.Text = "0%";
            }
            else
            {
                string ccCodePercent = ((decimal.Parse(this.lblChannelccCount.Text) / decimal.Parse(this.lblChannelOrderAllCount.Text)) * 100).ToString();
                this.lblChannelccCodePercent.Text = (ccCodePercent.IndexOf(".") > 0 && ccCodePercent.Split('.')[1].Length > 2) ? ccCodePercent.Substring(0, ccCodePercent.IndexOf(".")) + "." + ccCodePercent.Substring(ccCodePercent.IndexOf(".") + 1, 2) + "%" : ccCodePercent + "%";
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--ccCount3  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }

        try
        {
            this.lblChannelOtherCount.Text = (int.Parse(this.lblIOSChannelOther.Text) + int.Parse(this.lblADRChannelOther.Text) + int.Parse(this.lblWPChannelOther.Text) + int.Parse(this.lblW8ChannelOther.Text) + int.Parse(this.lblWAPChannelOther.Text) + int.Parse(this.lblProChannelOther.Text) + int.Parse(this.lblGETAROOMChannelOther.Text) + int.Parse(this.lblHMBSTChannelOther.Text) + int.Parse(this.lblQUNarChannelOther.Text) + int.Parse(this.lbl11ChannelOther.Text) + int.Parse(this.lblMJChannelOther.Text) + int.Parse(this.lblHotelvpMapChannelOther.Text) + int.Parse(this.lblRestsChannelOther.Text)).ToString();//总确认成功单数
            this.lblChannelOtherCodeCount.Text = "(" + (int.Parse(this.lblIOSChannelOtherCode.Text.Substring(1, this.lblIOSChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblADRChannelOtherCode.Text.Substring(1, this.lblADRChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWPChannelOtherCode.Text.Substring(1, this.lblWPChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblW8ChannelOtherCode.Text.Substring(1, this.lblW8ChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWAPChannelOtherCode.Text.Substring(1, this.lblWAPChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblProChannelOtherCode.Text.Substring(1, this.lblProChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblGETAROOMChannelOtherCode.Text.Substring(1, this.lblGETAROOMChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHMBSTChannelOtherCode.Text.Substring(1, this.lblHMBSTChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblQUNarChannelOtherCode.Text.Substring(1, this.lblQUNarChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lbl11ChannelOtherCode.Text.Substring(1, this.lbl11ChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblMJChannelOtherCode.Text.Substring(1, this.lblMJChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHotelvpMapChannelOtherCode.Text.Substring(1, this.lblHotelvpMapChannelOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblRestsChannelOtherCode.Text.Substring(1, this.lblRestsChannelOtherCode.Text.IndexOf(")") - 1))).ToString() + ")";//总券数
            if (lblChannelOtherCount.Text == "0" || lblChannelOrderAllCount.Text == "0")
            {
                this.lblChannelOtherPercent.Text = "0%";
            }
            else
            {
                string OtherPercent = ((decimal.Parse(this.lblChannelOtherCount.Text) / decimal.Parse(this.lblChannelOrderAllCount.Text)) * 100).ToString();
                this.lblChannelOtherPercent.Text = (OtherPercent.IndexOf(".") > 0 && OtherPercent.Split('.')[1].Length > 2) ? OtherPercent.Substring(0, OtherPercent.IndexOf(".")) + "." + OtherPercent.Substring(OtherPercent.IndexOf(".") + 1, 2) + "%" : OtherPercent + "%";
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--OtherCount  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
        try
        {
            this.lblrestCount.Text = (int.Parse(this.lblIOSrest.Text) + int.Parse(this.lblADRrest.Text) + int.Parse(this.lblWPrest.Text) + int.Parse(this.lblW8rest.Text) + int.Parse(this.lblWAPrest.Text) + int.Parse(this.lblProrest.Text) + int.Parse(this.lblGETAROOMrest.Text) + int.Parse(this.lblHMBSTrest.Text) + int.Parse(this.lblQUNarrest.Text) + int.Parse(this.lbl11rest.Text) + int.Parse(this.lblMJrest.Text) + int.Parse(this.lblHotelvpMaprest.Text) + int.Parse(this.lblRestsrest.Text)).ToString();//总确认成功单数
            this.lblrestCodeCount.Text = "(" + (int.Parse(this.lblIOSrestCode.Text.Substring(1, this.lblIOSrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblADRrestCode.Text.Substring(1, this.lblADRrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWPrestCode.Text.Substring(1, this.lblWPrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblW8restCode.Text.Substring(1, this.lblW8restCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblWAPrestCode.Text.Substring(1, this.lblWAPrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblProrestCode.Text.Substring(1, this.lblProrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblGETAROOMrestCode.Text.Substring(1, this.lblGETAROOMrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHMBSTrestCode.Text.Substring(1, this.lblHMBSTrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblQUNarrestCode.Text.Substring(1, this.lblQUNarrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lbl11restCode.Text.Substring(1, this.lbl11restCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblMJrestCode.Text.Substring(1, this.lblMJrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblHotelvpMaprestCode.Text.Substring(1, this.lblHotelvpMaprestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblRestsrestCode.Text.Substring(1, this.lblRestsrestCode.Text.IndexOf(")") - 1))).ToString() + ")";//总券数
            if (lblrestCount.Text == "0" || lblChannelOrderAllCount.Text == "0")
            {
                this.lblrestPercent.Text = "0%";
            }
            else
            {
                string restPercent = ((decimal.Parse(this.lblrestCount.Text) / decimal.Parse(this.lblChannelOrderAllCount.Text)) * 100).ToString();
                this.lblrestPercent.Text = (restPercent.IndexOf(".") > 0 && restPercent.Split('.')[1].Length > 2) ? restPercent.Substring(0, restPercent.IndexOf(".")) + "." + restPercent.Substring(restPercent.IndexOf(".") + 1, 2) + "%" : restPercent + "%";
            }
            #endregion
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--restCount  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
        try
        {
            #region
            this.Label2.Text = this.lblChannelOrderAllCount.Text;
            this.Label3.Text = this.lblChannelOrderAllCodeCount.Text;
            if (this.Label2.Text == "0")
            {
                this.Label4.Text = "0";
            }
            else
            {
                this.Label4.Text = ((BookTotalPrice / int.Parse(this.Label2.Text)).ToString().IndexOf(".") > 0 && (BookTotalPrice / int.Parse(this.Label2.Text)).ToString().Split('.')[1].Length > 2) ? (BookTotalPrice / int.Parse(this.Label2.Text)).ToString().Substring(0, (BookTotalPrice / int.Parse(this.Label2.Text)).ToString().IndexOf(".") + 2).ToString() : (BookTotalPrice / int.Parse(this.Label2.Text)).ToString();
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--  Label4.Text 异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
        try
        {
            //this.Label5.Text = (int.Parse(this.lblChannelAffirmOrderCount.Text) + int.Parse(this.lblChannelNotAffirmOrderCount.Text)).ToString();
            this.Label5.Text = this.lblChannelAffirmOrderCount.Text;
            //this.Label6.Text = "(" + (int.Parse(this.lblChannelAffirmOrderCodeCount.Text.Substring(1, this.lblChannelAffirmOrderCodeCount.Text.IndexOf(")") - 1)) + int.Parse(this.lblChannelNotAffirmOrderCodeCount.Text.Substring(1, this.lblChannelNotAffirmOrderCodeCount.Text.IndexOf(")") - 1))).ToString() + ")";
            this.Label6.Text = "(" + this.lblChannelAffirmOrderCodeCount.Text.Substring(1, this.lblChannelAffirmOrderCodeCount.Text.IndexOf(")") - 1) + ")";
            if (this.Label5.Text == "0")
            {
                this.Label9.Text = "0";
            }
            else
            {
                this.Label9.Text = ((ResvTypePrice / int.Parse(this.Label5.Text)).ToString().IndexOf(".") > 0 && (ResvTypePrice / int.Parse(this.Label5.Text)).ToString().Split('.')[1].Length > 2) ? (ResvTypePrice / int.Parse(this.Label5.Text)).ToString().Substring(0, (ResvTypePrice / int.Parse(this.Label5.Text)).ToString().IndexOf(".") + 2) : (ResvTypePrice / int.Parse(this.Label5.Text)).ToString();
            }
            #endregion
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewChannelDataList-统计总数--  Label9.Text 异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
    }

    //订单统计
    public void BindViewOrderDataList()
    {
        decimal BookTotalPrice = 0;
        decimal ResvTypePrice = 0;
        try
        {
            #region
            //MasterInfoEntity _masterInfoEntity = new MasterInfoEntity();
            //CommonEntity _commonEntity = new CommonEntity();

            //_masterInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            //_masterInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            //_masterInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            //_masterInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            //_masterInfoEntity.MasterInfoDBEntity = new List<MasterInfoDBEntity>();
            //MasterInfoDBEntity msterInfoDBEntity = new MasterInfoDBEntity();
            //_masterInfoEntity.MasterInfoDBEntity.Add(msterInfoDBEntity);
            //msterInfoDBEntity.Today = strToDay;

            //DataSet dsResult = MasterInfoBP.CommonSelectOrder(_masterInfoEntity).QueryResult;
            #endregion

            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                if (dsResult.Tables["OrderAll"].Rows.Count > 0)
                {
                    //hidorderDate
                    this.hidOrderStartDate.Value = dsResult.Tables["OrderAll"].Rows[0]["StartDate"].ToString();
                    this.hidOrderEndDate.Value = dsResult.Tables["OrderAll"].Rows[0]["EndDate"].ToString();
                    for (int i = 0; i < dsResult.Tables["OrderAll"].Rows.Count; i++)
                    {
                        #region
                        string PriceCode = dsResult.Tables["OrderAll"].Rows[i]["PRICE_CODE"].ToString();
                        string userCode = dsResult.Tables["OrderAll"].Rows[i]["TICKET_USERCODE"].ToString();
                        string orderResvtype = dsResult.Tables["OrderAll"].Rows[i]["FOG_RESVTYPE"].ToString();
                        string orderResvStatus = dsResult.Tables["OrderAll"].Rows[i]["FOG_RESVSTATUS"].ToString();
                        string orderBookStatus = dsResult.Tables["OrderAll"].Rows[i]["BOOK_STATUS"].ToString();
                        string orderBookStatusOther = dsResult.Tables["OrderAll"].Rows[i]["BOOK_STATUS_OTHER"].ToString();
                        string strFOGRESVSTATUS = dsResult.Tables["OrderAll"].Rows[i]["FOG_RESVSTATUS"].ToString();
                        string CreateDate = null;
                        string InDate = null;
                        if (dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString() != "")
                        {
                            CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                        }
                        if (dsResult.Tables["OrderAll"].Rows[i]["IN_DATE"].ToString() != "")
                        {
                            InDate = dsResult.Tables["OrderAll"].Rows[i]["IN_DATE"].ToString();
                        }
                        #endregion
                        
                        #region LMBAR
                        if (PriceCode != "" && PriceCode == "LMBAR")
                        {
                            #region
                            this.lblLBOrderAll.Text = (int.Parse(this.lblLBOrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblLBOrderAllCode.Text = "(" + (int.Parse(this.lblLBOrderAllCode.Text.Substring(1, this.lblLBOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }
                            #region BAR/BARB当晚入住
                            //if (DateTime.Parse(CreateDate).Hour < 4)
                            //{
                            //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                            //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                            //    {
                            //        this.lblOrderAllCKIN.Text = (int.Parse(this.lblOrderAllCKIN.Text) + 1).ToString();
                            //    }
                            //}
                            //else
                            //{
                            //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                            //    {
                            //        this.lblOrderAllCKIN.Text = (int.Parse(this.lblOrderAllCKIN.Text) + 1).ToString();
                            //    }
                            //}
                            #endregion
                            #endregion

                            #region 已确认,未确认成功单(券)
                            //已确认成功单(券)
                            if (orderBookStatus.Contains("5"))
                            {
                                //已确认成功单
                                this.lblLBOrderAffirm.Text = (int.Parse(this.lblLBOrderAffirm.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLBOrderAffirmCode.Text = "(" + (int.Parse(this.lblLBOrderAffirmCode.Text.Substring(1, this.lblLBOrderAffirmCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblOrderAffirmCKIN.Text = (int.Parse(this.lblOrderAffirmCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblOrderAffirmCKIN.Text = (int.Parse(this.lblOrderAffirmCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion

                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            if (orderBookStatus.Contains("1"))
                            {
                                //未确认成功单
                                this.lblLBOrderNotAffirmOrder.Text = (int.Parse(this.lblLBOrderNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLBOrderNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblLBOrderNotAffirmOrderCode.Text.Substring(1, this.lblLBOrderNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblOrderNotAffirmCKIN.Text = (int.Parse(this.lblOrderNotAffirmCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblOrderNotAffirmCKIN.Text = (int.Parse(this.lblOrderNotAffirmCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion

                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region 用户取消单(券)
                            if (orderBookStatus.Contains("4"))
                            {
                                //用户取消单(券)
                                this.lblLBOrderOther.Text = (int.Parse(this.lblLBOrderOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLBOrderOtherCode.Text = "(" + (int.Parse(this.lblLBOrderOtherCode.Text.Substring(1, this.lblLBOrderOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblOrderOtherCKIN.Text = (int.Parse(this.lblOrderOtherCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblOrderOtherCKIN.Text = (int.Parse(this.lblOrderOtherCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion
                            }
                            #endregion

                            #region  超时取消单
                            if (orderBookStatus.Contains("3"))
                            {
                                this.lblLBOrderTimeOut.Text = (int.Parse(this.lblLBOrderTimeOut.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLBOrderTimeOutCode.Text = "(" + (int.Parse(this.lblLBOrderTimeOutCode.Text.Substring(1, this.lblLBOrderTimeOutCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                            }
                            #endregion

                            #region  其他
                            if (orderBookStatus.Contains("2"))
                            {
                                this.lblLBrest.Text = (int.Parse(this.lblLBrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLBrestCode.Text = "(" + (int.Parse(this.lblLBrestCode.Text.Substring(1, this.lblLBrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblrestCKIN.Text = (int.Parse(this.lblrestCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblrestCKIN.Text = (int.Parse(this.lblrestCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region LMBAR2
                        if (PriceCode != "" && PriceCode == "LMBAR2")
                        {
                            #region
                            this.lblLB2OrderAll.Text = (int.Parse(this.lblLB2OrderAll.Text) + 1).ToString();
                            //是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblLB2OrderAllCode.Text = "(" + (int.Parse(this.lblLB2OrderAllCode.Text.Substring(1, this.lblLB2OrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }
                            #region BAR/BARB当晚入住
                            //if (DateTime.Parse(CreateDate).Hour < 4)
                            //{
                            //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                            //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                            //    {
                            //        this.lblOrderAllCKIN.Text = (int.Parse(this.lblOrderAllCKIN.Text) + 1).ToString();
                            //    }
                            //}
                            //else
                            //{
                            //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                            //    {
                            //        this.lblOrderAllCKIN.Text = (int.Parse(this.lblOrderAllCKIN.Text) + 1).ToString();
                            //    }
                            //}
                            #endregion
                            #endregion

                            #region 已确认,未确认成功单(券)
                            //已确认成功单(券)
                            if ((orderBookStatusOther.Contains("4") || orderBookStatusOther.Contains("5") || orderBookStatusOther.Contains("6") || orderBookStatusOther.Contains("7") || orderBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS))
                            {
                                //已确认成功单
                                this.lblLB2OrderAffirm.Text = (int.Parse(this.lblLB2OrderAffirm.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLB2OrderAffirmCode.Text = "(" + (int.Parse(this.lblLB2OrderAffirmCode.Text.Substring(1, this.lblLB2OrderAffirmCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblOrderAffirmCKIN.Text = (int.Parse(this.lblOrderAffirmCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblOrderAffirmCKIN.Text = (int.Parse(this.lblOrderAffirmCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion

                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            if (orderBookStatusOther.Contains("1"))
                            {
                                //未确认成功单
                                this.lblLB2OrderNotAffirmOrder.Text = (int.Parse(this.lblLB2OrderNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLB2OrderNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblLB2OrderNotAffirmOrderCode.Text.Substring(1, this.lblLB2OrderNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblOrderNotAffirmCKIN.Text = (int.Parse(this.lblOrderNotAffirmCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblOrderNotAffirmCKIN.Text = (int.Parse(this.lblOrderNotAffirmCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion

                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            if (orderBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblLB2Ordercc.Text = (int.Parse(this.lblLB2Ordercc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLB2OrderccCode.Text = "(" + (int.Parse(this.lblLB2OrderccCode.Text.Substring(1, this.lblLB2OrderccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblOrderccCKIN.Text = (int.Parse(this.lblOrderccCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblOrderccCKIN.Text = (int.Parse(this.lblOrderccCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion
                            }
                            #endregion

                            #region 用户取消单(券)
                            if (orderBookStatusOther.Contains("3"))
                            {
                                string di = dsResult.Tables["OrderAll"].Rows[i]["ID"].ToString();
                                //用户取消单(券)
                                this.lblLB2OrderOther.Text = (int.Parse(this.lblLB2OrderOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLB2OrderOtherCode.Text = "(" + (int.Parse(this.lblLB2OrderOtherCode.Text.Substring(1, this.lblLB2OrderOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblOrderOtherCKIN.Text = (int.Parse(this.lblOrderOtherCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblOrderOtherCKIN.Text = (int.Parse(this.lblOrderOtherCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion
                            }
                            #endregion

                            #region  其他
                            //if (orderBookStatusOther.Contains("2") || orderBookStatusOther.Contains("5") || orderBookStatusOther.Contains("7") || orderBookStatusOther.Contains("8") )
                            if (!orderBookStatusOther.Contains("4") && !orderBookStatusOther.Contains("5") && !orderBookStatusOther.Contains("6") && !orderBookStatusOther.Contains("7") && !orderBookStatusOther.Contains("8") && !orderBookStatusOther.Contains("1") && !orderBookStatusOther.Contains("9") && !orderBookStatusOther.Contains("3")
                                || orderBookStatusOther.Contains("0"))
                            {
                                this.lblLB2rest.Text = (int.Parse(this.lblLB2rest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblLB2restCode.Text = "(" + (int.Parse(this.lblLB2restCode.Text.Substring(1, this.lblLB2restCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                //CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                //if (DateTime.Parse(CreateDate).Hour < 4)
                                //{
                                //    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                //    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                //    {
                                //        this.lblrestCKIN.Text = (int.Parse(this.lblrestCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                //    {
                                //        this.lblrestCKIN.Text = (int.Parse(this.lblrestCKIN.Text) + 1).ToString();
                                //    }
                                //}
                                #endregion
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion

                        #region BAR/BARB
                        if (PriceCode != "" && (PriceCode.Trim() == "BAR" || PriceCode.Contains("BARB")))
                        {
                            #region
                            this.lblBBOrderAll.Text = (int.Parse(this.lblBBOrderAll.Text) + 1).ToString();
                            #region 是否使用劵
                            if (userCode != null && userCode != "")
                            {
                                this.lblBBOrderAllCode.Text = "(" + (int.Parse(this.lblBBOrderAllCode.Text.Substring(1, this.lblBBOrderAllCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                            }
                            //BAR/BARB当晚入住
                            if (DateTime.Parse(CreateDate).Hour < 4)
                            {
                                CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                {
                                    this.lblOrderAllCKIN.Text = (int.Parse(this.lblOrderAllCKIN.Text) + 1).ToString();
                                }
                            }
                            else
                            {
                                if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                {
                                    this.lblOrderAllCKIN.Text = (int.Parse(this.lblOrderAllCKIN.Text) + 1).ToString();
                                }
                            }
                            #endregion
                            #endregion

                            #region 已确认,未确认成功单(券)
                            //已确认成功单(券)
                            if ((orderBookStatusOther.Contains("4") || orderBookStatusOther.Contains("5") || orderBookStatusOther.Contains("6") || orderBookStatusOther.Contains("7") || orderBookStatusOther.Contains("8")) && "1".Equals(strFOGRESVSTATUS))
                            {
                                //已确认成功单
                                this.lblBBOrderAffirm.Text = (int.Parse(this.lblBBOrderAffirm.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblBBOrderAffirmCode.Text = "(" + (int.Parse(this.lblBBOrderAffirmCode.Text.Substring(1, this.lblBBOrderAffirmCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                if (DateTime.Parse(CreateDate).Hour < 4)
                                {
                                    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                    {
                                        this.lblOrderAffirmCKIN.Text = (int.Parse(this.lblOrderAffirmCKIN.Text) + 1).ToString();
                                    }
                                }
                                else
                                {
                                    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                    {
                                        this.lblOrderAffirmCKIN.Text = (int.Parse(this.lblOrderAffirmCKIN.Text) + 1).ToString();
                                    }
                                }
                                #endregion

                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            //未确认成功单(券)
                            if (orderBookStatusOther.Contains("1"))
                            {
                                //未确认成功单
                                this.lblBBOrderNotAffirmOrder.Text = (int.Parse(this.lblBBOrderNotAffirmOrder.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblBBOrderNotAffirmOrderCode.Text = "(" + (int.Parse(this.lblBBOrderNotAffirmOrderCode.Text.Substring(1, this.lblBBOrderNotAffirmOrderCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                if (DateTime.Parse(CreateDate).Hour < 4)
                                {
                                    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                    {
                                        this.lblOrderNotAffirmCKIN.Text = (int.Parse(this.lblOrderNotAffirmCKIN.Text) + 1).ToString();
                                    }
                                }
                                else
                                {
                                    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                    {
                                        this.lblOrderNotAffirmCKIN.Text = (int.Parse(this.lblOrderNotAffirmCKIN.Text) + 1).ToString();
                                    }
                                }
                                #endregion

                                if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                                {
                                    ResvTypePrice = ResvTypePrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                                }
                            }
                            #endregion

                            #region CC取消单(券)
                            if (orderBookStatusOther.Contains("9"))
                            {
                                //IOS  cc取消单
                                this.lblBB2Ordercc.Text = (int.Parse(this.lblBB2Ordercc.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblBB2OrderccCode.Text = "(" + (int.Parse(this.lblBB2OrderccCode.Text.Substring(1, this.lblBB2OrderccCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                if (DateTime.Parse(CreateDate).Hour < 4)
                                {
                                    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                    {
                                        this.lblOrderccCKIN.Text = (int.Parse(this.lblOrderccCKIN.Text) + 1).ToString();
                                    }
                                }
                                else
                                {
                                    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                    {
                                        this.lblOrderccCKIN.Text = (int.Parse(this.lblOrderccCKIN.Text) + 1).ToString();
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region 用户取消单(券)
                            if (orderBookStatusOther.Contains("3"))
                            {
                                //用户取消单(券)
                                this.lblBBOrderOther.Text = (int.Parse(this.lblBBOrderOther.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblBBOrderOtherCode.Text = "(" + (int.Parse(this.lblBBOrderOtherCode.Text.Substring(1, this.lblBBOrderOtherCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                if (DateTime.Parse(CreateDate).Hour < 4)
                                {
                                    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                    {
                                        this.lblOrderOtherCKIN.Text = (int.Parse(this.lblOrderOtherCKIN.Text) + 1).ToString();
                                    }
                                }
                                else
                                {
                                    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                    {
                                        this.lblOrderOtherCKIN.Text = (int.Parse(this.lblOrderOtherCKIN.Text) + 1).ToString();
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region  其他
                            //if (orderBookStatusOther.Contains("2") || orderBookStatusOther.Contains("5") || orderBookStatusOther.Contains("7") || orderBookStatusOther.Contains("8") )
                            if (!orderBookStatusOther.Contains("4") && !orderBookStatusOther.Contains("5") && !orderBookStatusOther.Contains("6") && !orderBookStatusOther.Contains("7") && !orderBookStatusOther.Contains("8") && !orderBookStatusOther.Contains("1") && !orderBookStatusOther.Contains("9") && !orderBookStatusOther.Contains("3")
                                || (orderBookStatusOther.Contains("0") && orderBookStatus.Contains("0")))
                            {
                                this.lblBBrest.Text = (int.Parse(this.lblBBrest.Text) + 1).ToString();
                                //是否使用劵
                                if (userCode != null && userCode != "")
                                {
                                    this.lblBBrestCode.Text = "(" + (int.Parse(this.lblBBrestCode.Text.Substring(1, this.lblBBrestCode.Text.IndexOf(")") - 1)) + 1).ToString() + ")";
                                }
                                #region BAR/BARB当晚入住
                                CreateDate = dsResult.Tables["OrderAll"].Rows[i]["CREATE_TIME"].ToString();
                                if (DateTime.Parse(CreateDate).Hour < 4)
                                {
                                    CreateDate = DateTime.Parse(CreateDate).AddDays(-1).ToShortDateString();
                                    if (CreateDate == DateTime.Parse(InDate).ToShortDateString())
                                    {
                                        this.lblrestCKIN.Text = (int.Parse(this.lblrestCKIN.Text) + 1).ToString();
                                    }
                                }
                                else
                                {
                                    if (CreateDate.Substring(0, CreateDate.IndexOf(" ")) == InDate.Substring(0, CreateDate.IndexOf(" ")))
                                    {
                                        this.lblrestCKIN.Text = (int.Parse(this.lblrestCKIN.Text) + 1).ToString();
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            if (dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString().Trim() != "")
                            {
                                BookTotalPrice = BookTotalPrice + decimal.Parse(dsResult.Tables["OrderAll"].Rows[i]["BOOK_TOTAL_PRICE"].ToString());
                            }
                        }
                        #endregion
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewOrderDataList-查询和分类  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }

        try
        {
            #region 计算总计各类订单数、劵数及百分比
            this.lblOrderAllCount.Text = (int.Parse(lblLBOrderAll.Text) + int.Parse(lblLB2OrderAll.Text) + int.Parse(lblBBOrderAll.Text)).ToString();
            this.lblOrderAllCode.Text = "(" + (int.Parse(this.lblLBOrderAllCode.Text.Substring(1, this.lblLBOrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblLB2OrderAllCode.Text.Substring(1, this.lblLB2OrderAllCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblBBOrderAllCode.Text.Substring(1, this.lblBBOrderAllCode.Text.IndexOf(")") - 1))).ToString() + ")";
            this.lblOrderAllCountPercent.Text = "100%";
            this.lblOrderAllCodePercent.Text = "(100%)";

            #region
            this.lblOrderAffirmCount.Text = (int.Parse(lblLBOrderAffirm.Text) + int.Parse(lblLB2OrderAffirm.Text) + int.Parse(lblBBOrderAffirm.Text)).ToString();
            this.lblOrderAffirmCode.Text = "(" + (int.Parse(this.lblLBOrderAffirmCode.Text.Substring(1, this.lblLBOrderAffirmCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblLB2OrderAffirmCode.Text.Substring(1, this.lblLB2OrderAffirmCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblBBOrderAffirmCode.Text.Substring(1, this.lblBBOrderAffirmCode.Text.IndexOf(")") - 1))).ToString() + ")";
            if (lblOrderAffirmCount.Text == "0" || lblOrderAllCount.Text == "0")
            {
                this.lblOrderAffirmPercent.Text = "0%";
            }
            else
            {
                string OrderAffirmPercent = ((decimal.Parse(this.lblOrderAffirmCount.Text) / decimal.Parse(this.lblOrderAllCount.Text)) * 100).ToString();
                this.lblOrderAffirmPercent.Text = (OrderAffirmPercent.IndexOf(".") > 0 && OrderAffirmPercent.Split('.')[1].Length > 2) ? OrderAffirmPercent.Substring(0, OrderAffirmPercent.IndexOf(".")) + "." + OrderAffirmPercent.Substring(OrderAffirmPercent.IndexOf(".") + 1, 2) + "%" : OrderAffirmPercent + "%";
            }
            if (lblOrderAffirmCode.Text == "(0)" || lblOrderAllCode.Text == "(0)")
            {
                this.lblOrderAffirmPercentCode.Text = "(0%)";
            }
            else
            {
                string OrderAffirmPercentCode = ((decimal.Parse(this.lblOrderAffirmCode.Text.Substring(1, this.lblOrderAffirmCode.Text.IndexOf(")") - 1)) / decimal.Parse(this.lblOrderAllCode.Text.Substring(1, this.lblOrderAllCode.Text.IndexOf(")") - 1))) * 100).ToString();
                this.lblOrderAffirmPercentCode.Text = (OrderAffirmPercentCode.IndexOf(".") > 0 && OrderAffirmPercentCode.Split('.')[1].Length > 2) ? "(" + OrderAffirmPercentCode.Substring(0, OrderAffirmPercentCode.IndexOf(".")) + "." + OrderAffirmPercentCode.Substring(OrderAffirmPercentCode.IndexOf(".") + 1, 1) + "%)" : "(" + OrderAffirmPercentCode + "%)";
            }
            #endregion

            #region
            this.lblOrderNotAffirmCount.Text = (int.Parse(lblLBOrderNotAffirmOrder.Text) + int.Parse(lblLB2OrderNotAffirmOrder.Text) + int.Parse(lblBBOrderNotAffirmOrder.Text)).ToString();
            this.lblOrderNotAffirmCode.Text = "(" + (int.Parse(this.lblLBOrderNotAffirmOrderCode.Text.Substring(1, this.lblLBOrderNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblLB2OrderNotAffirmOrderCode.Text.Substring(1, this.lblLB2OrderNotAffirmOrderCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblBBOrderNotAffirmOrderCode.Text.Substring(1, this.lblBBOrderNotAffirmOrderCode.Text.IndexOf(")") - 1))).ToString() + ")";
            if (lblOrderNotAffirmCount.Text == "0" || lblOrderAllCount.Text == "0")
            {
                this.lblOrderNotAffirmPercent.Text = "0%";
            }
            else
            {
                string OrderNotAffirmPercent = ((decimal.Parse(lblOrderNotAffirmCount.Text)) / (decimal.Parse(lblOrderAllCount.Text)) * 100).ToString() + "%";
                this.lblOrderNotAffirmPercent.Text = (OrderNotAffirmPercent.IndexOf(".") > 0 && OrderNotAffirmPercent.Split('.')[1].Length > 2) ? OrderNotAffirmPercent.Substring(0, OrderNotAffirmPercent.IndexOf(".")) + "." + OrderNotAffirmPercent.Substring(OrderNotAffirmPercent.IndexOf(".") + 1, 1) + "%" : OrderNotAffirmPercent + "%";
            }
            if (lblOrderNotAffirmCode.Text == "(0)" || lblOrderAllCode.Text == "(0)")
            {
                this.lblOrderNotAffirmPercentCode.Text = "(0%)";
            }
            else
            {
                string OrderNotAffirmPercentCode = ((decimal.Parse(this.lblOrderNotAffirmCode.Text.Substring(1, this.lblOrderNotAffirmCode.Text.IndexOf(")") - 1)) / decimal.Parse(this.lblOrderAllCode.Text.Substring(1, this.lblOrderAllCode.Text.IndexOf(")") - 1))) * 100).ToString();
                this.lblOrderNotAffirmPercentCode.Text = (OrderNotAffirmPercentCode.IndexOf(".") > 0 && OrderNotAffirmPercentCode.Split('.')[1].Length > 2) ? "(" + OrderNotAffirmPercentCode.Substring(0, OrderNotAffirmPercentCode.IndexOf(".")) + "." + OrderNotAffirmPercentCode.Substring(OrderNotAffirmPercentCode.IndexOf(".") + 1, 1) + "%)" : "(" + OrderNotAffirmPercentCode + "%)";
            }
            #endregion

            #region
            this.lblOrderccCount.Text = (int.Parse(lblLBOrdercc.Text) + int.Parse(lblLB2Ordercc.Text) + int.Parse(lblBB2Ordercc.Text)).ToString();
            this.lblOrderccCode.Text = "(" + (int.Parse(this.lblLBOrderccCode.Text.Substring(1, this.lblLBOrderccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblLB2OrderccCode.Text.Substring(1, this.lblLB2OrderccCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblBB2OrderccCode.Text.Substring(1, this.lblBB2OrderccCode.Text.IndexOf(")") - 1))).ToString() + ")";
            if (lblOrderccCount.Text == "0" || lblOrderAllCount.Text == "0")
            {
                this.lblOrderccCountPercent.Text = "0%";
            }
            else
            {
                string OrderccCountPercent = ((decimal.Parse(this.lblOrderccCount.Text) / decimal.Parse(this.lblOrderAllCount.Text)) * 100).ToString();
                this.lblOrderccCountPercent.Text = (OrderccCountPercent.IndexOf(".") > 0 && OrderccCountPercent.Split('.')[1].Length > 2) ? OrderccCountPercent.Substring(0, OrderccCountPercent.IndexOf(".")) + "." + OrderccCountPercent.Substring(OrderccCountPercent.IndexOf(".") + 1, 1) + "%" : OrderccCountPercent + "%";
            }
            if (lblOrderccCode.Text == "(0)" || lblOrderAllCode.Text == "(0)")
            {
                this.lblOrderccCountPercentCode.Text = "(0%)";
            }
            else
            {
                string OrderccCountPercentCode = ((decimal.Parse(this.lblOrderccCode.Text.Substring(1, this.lblOrderccCode.Text.IndexOf(")") - 1)) / decimal.Parse(this.lblOrderAllCode.Text.Substring(1, this.lblOrderAllCode.Text.IndexOf(")") - 1))) * 100).ToString();
                this.lblOrderccCountPercentCode.Text = (OrderccCountPercentCode.IndexOf(".") > 0 && OrderccCountPercentCode.Split('.')[1].Length > 2) ? "(" + OrderccCountPercentCode.Substring(0, OrderccCountPercentCode.IndexOf(".")) + "." + OrderccCountPercentCode.Substring(OrderccCountPercentCode.IndexOf(".") + 1, 1) + "%)" : "(" + OrderccCountPercentCode + "%)";
            }
            #endregion

            #region
            this.lblOrderOtherCount.Text = (int.Parse(lblLBOrderOther.Text) + int.Parse(lblLB2OrderOther.Text) + int.Parse(lblBBOrderOther.Text)).ToString();
            this.lblOrderOtherCode.Text = "(" + (int.Parse(this.lblLBOrderOtherCode.Text.Substring(1, this.lblLBOrderOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblLB2OrderOtherCode.Text.Substring(1, this.lblLB2OrderOtherCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblBBOrderOtherCode.Text.Substring(1, this.lblBBOrderOtherCode.Text.IndexOf(")") - 1))).ToString() + ")";
            if (lblOrderOtherCount.Text == "0" || lblOrderAllCount.Text == "0")
            {
                this.lblOrderOtherPercent.Text = "0%";
            }
            else
            {
                string OrderOtherPercent = ((decimal.Parse(this.lblOrderOtherCount.Text) / decimal.Parse(this.lblOrderAllCount.Text)) * 100).ToString();
                this.lblOrderOtherPercent.Text = (OrderOtherPercent.IndexOf(".") > 0 && OrderOtherPercent.Split('.')[1].Length > 2) ? OrderOtherPercent.Substring(0, OrderOtherPercent.IndexOf(".")) + "." + OrderOtherPercent.Substring(OrderOtherPercent.IndexOf(".") + 1, 1) + "%" : OrderOtherPercent + "%";
            }
            if (lblOrderOtherCode.Text == "(0)" || lblOrderAllCode.Text == "(0)")
            {
                this.lblOrderOtherCodePercent.Text = "(0%)";
            }
            else
            {
                string OrderOtherCodePercent = ((decimal.Parse(this.lblOrderOtherCode.Text.Substring(1, this.lblOrderOtherCode.Text.IndexOf(")") - 1)) / decimal.Parse(this.lblOrderAllCode.Text.Substring(1, this.lblOrderAllCode.Text.IndexOf(")") - 1))) * 100).ToString();
                this.lblOrderOtherCodePercent.Text = (OrderOtherCodePercent.IndexOf(".") > 0 && OrderOtherCodePercent.Split('.')[1].Length > 2) ? "(" + OrderOtherCodePercent.Substring(0, OrderOtherCodePercent.IndexOf(".")) + "." + OrderOtherCodePercent.Substring(OrderOtherCodePercent.IndexOf(".") + 1, 1) + "%)" : "(" + OrderOtherCodePercent + "%)";
            }
            #endregion

            #region
            this.lblOrderTimeOutCount.Text = (int.Parse(lblLBOrderTimeOut.Text) + int.Parse(lblLB2OrderTimeOut.Text) + int.Parse(lblLBBOrderTimeOut.Text)).ToString();
            this.lblOrderTimeOutCode.Text = "(" + (int.Parse(this.lblLBOrderTimeOutCode.Text.Substring(1, this.lblLBOrderTimeOutCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblLB2OrderTimeOutCode.Text.Substring(1, this.lblLB2OrderTimeOutCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblBBOrderTimeOutCode.Text.Substring(1, this.lblBBOrderTimeOutCode.Text.IndexOf(")") - 1))).ToString() + ")";
            if (lblOrderTimeOutCount.Text == "0" || lblOrderAllCount.Text == "0")
            {
                this.lblOrderTimeOutPercent.Text = "0%";
            }
            else
            {
                string orderTimeOutPercent = ((decimal.Parse(this.lblOrderTimeOutCount.Text) / decimal.Parse(this.lblOrderAllCount.Text)) * 100).ToString();
                this.lblOrderTimeOutPercent.Text = (orderTimeOutPercent.IndexOf(".") > 0 && orderTimeOutPercent.Split('.')[1].Length > 2) ? orderTimeOutPercent.Substring(0, orderTimeOutPercent.IndexOf(".")) + "." + orderTimeOutPercent.Substring(orderTimeOutPercent.IndexOf(".") + 1, 1) + "%" : orderTimeOutPercent + "%";
            }
            if (lblOrderTimeOutCode.Text == "(0)" || lblOrderAllCode.Text == "(0)")
            {
                this.lblOrderTimeOutCodePercent.Text = "(0%)";
            }
            else
            {
                string orderTimeOutCode = ((decimal.Parse(this.lblOrderTimeOutCode.Text.Substring(1, this.lblOrderTimeOutCode.Text.IndexOf(")") - 1)) / decimal.Parse(this.lblOrderAllCode.Text.Substring(1, this.lblOrderAllCode.Text.IndexOf(")") - 1))) * 100).ToString();
                this.lblOrderTimeOutCodePercent.Text = (orderTimeOutCode.IndexOf(".") > 0 && orderTimeOutCode.Split('.')[1].Length > 2) ? "(" + orderTimeOutCode.Substring(0, orderTimeOutCode.IndexOf(".")) + "." + orderTimeOutCode.Substring(orderTimeOutCode.IndexOf(".") + 1, 1) + "%)" : "(" + orderTimeOutCode + "%)";
            }
            #endregion

            #region
            this.lblrestCou.Text = (int.Parse(lblLBrest.Text) + int.Parse(lblLB2rest.Text) + int.Parse(lblBBrest.Text)).ToString();
            this.lblrestCode.Text = "(" + (int.Parse(this.lblLBrestCode.Text.Substring(1, this.lblLBrestCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblLB2restCode.Text.Substring(1, this.lblLB2restCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblBBrestCode.Text.Substring(1, this.lblBBrestCode.Text.IndexOf(")") - 1))).ToString() + ")";
            if (lblrestCou.Text == "0" || lblOrderAllCount.Text == "0")
            {
                this.lblrestCountPercent.Text = "0%";
            }
            else
            {
                string restCountPercent = ((decimal.Parse(this.lblrestCou.Text) / decimal.Parse(this.lblOrderAllCount.Text)) * 100).ToString();
                this.lblrestCountPercent.Text = (restCountPercent.IndexOf(".") > 0 && restCountPercent.Split('.')[1].Length > 2) ? restCountPercent.Substring(0, restCountPercent.IndexOf(".")) + "." + restCountPercent.Substring(restCountPercent.IndexOf(".") + 1, 1) + "%" : restCountPercent + "%";
            }
            if (lblrestCode.Text == "(0)" || lblOrderAllCode.Text == "(0)")
            {
                this.lblrestCodePercent.Text = "(0%)";
            }
            else
            {
                string restCodePercent = ((decimal.Parse(this.lblrestCode.Text.Substring(1, this.lblrestCode.Text.IndexOf(")") - 1)) / decimal.Parse(this.lblOrderAllCode.Text.Substring(1, this.lblOrderAllCode.Text.IndexOf(")") - 1))) * 100).ToString();
                this.lblrestCodePercent.Text = (restCodePercent.IndexOf(".") > 0 && restCodePercent.Split('.')[1].Length > 2) ? "(" + restCodePercent.Substring(0, restCodePercent.IndexOf(".")) + "." + restCodePercent.Substring(restCodePercent.IndexOf(".") + 1, 1) + "%)" : "(" + restCodePercent + "%)";
            }
            #endregion
            #endregion

            #region
            this.Label40.Text = lblOrderAllCount.Text;
            this.Label41.Text = lblOrderAllCode.Text;
            if (this.Label40.Text == "0")
            {
                this.Label42.Text = "0";
            }
            else
            {
                this.Label42.Text = ((BookTotalPrice / int.Parse(this.Label40.Text)).ToString().IndexOf(".") > 2 && (BookTotalPrice / int.Parse(this.Label40.Text)).ToString().Split('.')[1].Length > 3) ? (BookTotalPrice / int.Parse(this.Label40.Text)).ToString().Substring(0, (BookTotalPrice / int.Parse(this.Label40.Text)).ToString().IndexOf(".") + 2).ToString() : (BookTotalPrice / int.Parse(this.Label40.Text)).ToString();
            }

            //this.Label43.Text = (int.Parse(lblOrderAffirmCount.Text) + int.Parse(lblOrderNotAffirmCount.Text)).ToString();
            this.Label43.Text = lblOrderAffirmCount.Text;
            //this.Label44.Text = "(" + (int.Parse(this.lblOrderAffirmCode.Text.Substring(1, this.lblOrderAffirmCode.Text.IndexOf(")") - 1)) + int.Parse(this.lblOrderNotAffirmCode.Text.Substring(1, this.lblOrderNotAffirmCode.Text.IndexOf(")") - 1))).ToString() + ")";
            this.Label44.Text = "(" + this.lblOrderAffirmCode.Text.Substring(1, this.lblOrderAffirmCode.Text.IndexOf(")") - 1) + ")";
            if (this.Label43.Text == "0")
            {
                this.Label7.Text = "0";
            }
            else
            {
                this.Label7.Text = ((ResvTypePrice / int.Parse(this.Label43.Text)).ToString().IndexOf(".") > 2 && (ResvTypePrice / int.Parse(this.Label43.Text)).ToString().Split('.')[1].Length > 3) ? (ResvTypePrice / int.Parse(this.Label43.Text)).ToString().Substring(0, (ResvTypePrice / int.Parse(this.Label43.Text)).ToString().IndexOf(".") + 2) : (ResvTypePrice / int.Parse(this.Label43.Text)).ToString(); 
            }
            #endregion

        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-BindViewOrderDataList-统计  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
    }

    public void ClearOrder()
    {
        try
        {
            this.Label40.Text = this.Label41.Text = this.Label42.Text = this.Label43.Text = this.Label44.Text = this.Label7.Text = "0";
            lblLBOrderAll.Text = lblLB2OrderAll.Text = lblBBOrderAll.Text = lblOrderAllCount.Text = lblOrderAllCountPercent.Text = "0";
            lblLBOrderAllCode.Text = lblLB2OrderAllCode.Text = lblBBOrderAllCode.Text = lblOrderAllCode.Text = lblOrderAllCodePercent.Text = "(0)";
            lblLBOrderAffirm.Text = lblLB2OrderAffirm.Text = lblBBOrderAffirm.Text = lblOrderAffirmCount.Text = lblOrderAffirmPercent.Text = "0";
            lblLBOrderAffirmCode.Text = lblLB2OrderAffirmCode.Text = lblBBOrderAffirmCode.Text = lblOrderAffirmCode.Text = lblOrderAffirmPercentCode.Text = "(0)";
            lblLBOrderNotAffirmOrder.Text = lblLB2OrderNotAffirmOrder.Text = lblBBOrderNotAffirmOrder.Text = lblOrderNotAffirmCount.Text = lblOrderNotAffirmPercent.Text = "0";
            lblLBOrderNotAffirmOrderCode.Text = lblLB2OrderNotAffirmOrderCode.Text = lblBBOrderNotAffirmOrderCode.Text = lblOrderNotAffirmCode.Text = lblOrderNotAffirmPercentCode.Text = "(0)";
            lblLBOrdercc.Text = lblLB2Ordercc.Text = lblBB2Ordercc.Text = lblOrderccCount.Text = lblOrderccCountPercent.Text = "0";
            lblLBOrderccCode.Text = lblLB2OrderccCode.Text = lblBB2OrderccCode.Text = lblOrderccCode.Text = lblOrderccCountPercentCode.Text = "(0)";
            lblLBOrderOther.Text = lblLB2OrderOther.Text = lblBBOrderOther.Text = lblOrderOtherCount.Text = lblOrderOtherPercent.Text = "0";
            lblLBOrderOtherCode.Text = lblLB2OrderOtherCode.Text = lblBBOrderOtherCode.Text = lblOrderOtherCode.Text = lblOrderOtherCodePercent.Text = "(0)";
            lblLBOrderTimeOut.Text = lblLB2OrderTimeOut.Text = lblLBBOrderTimeOut.Text = lblOrderTimeOutCount.Text = lblOrderTimeOutPercent.Text = "0";
            lblLBOrderTimeOutCode.Text = lblLB2OrderTimeOutCode.Text = lblBBOrderTimeOutCode.Text = lblOrderTimeOutCode.Text = lblOrderTimeOutCodePercent.Text = "(0)";
            lblLBrest.Text = lblLB2rest.Text = lblBBrest.Text = lblrestCou.Text = lblrestCountPercent.Text = "0";
            lblLBrestCode.Text = lblLB2restCode.Text = lblBBrestCode.Text = lblrestCode.Text = lblrestCodePercent.Text = "(0)";
            lblOrderAllCKIN.Text = lblOrderAffirmCKIN.Text = lblOrderNotAffirmCKIN.Text = lblOrderccCKIN.Text = lblOrderOtherCKIN.Text = lblrestCKIN.Text = "0";
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-ClearOrder:异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
    }

    /// <summary>
    /// 将所有页面历史数据清0
    /// </summary>
    public void ClearChannel()
    {
        try
        {
            this.Label2.Text = this.Label3.Text = this.Label4.Text = this.Label5.Text = this.Label6.Text = this.Label9.Text = "0";
            lblIOSChannelOrderAll.Text = lblAdrChannelOrderAll.Text = lblWPChannelOrderAll.Text = lblWAPChannelOrderAll.Text = lblProChannelOrderAll.Text = lblQUNarChannelOrderAll.Text = lbl11ChannelOrderAll.Text = lblMJChannelOrderAll.Text = lblChannelOrderAllCount.Text = lblChannelOrderAllPercent.Text = lblGETAROOMOrderAll.Text = lblW8ChannelOrderAll.Text = lblHMBSTOrderAll.Text = "0";
            lblIOSChannelOrderAllCode.Text = lblAdrChannelOrderAllCode.Text = lblWPChannelOrderAllCode.Text = lblWAPChannelOrderAllCode.Text = lblProChannelOrderAllCode.Text = lblQUNarChannelOrderAllCode.Text = lbl11ChannelOrderAllCode.Text = lblMJChannelOrderAllCode.Text = lblChannelOrderAllCodeCount.Text = lblGETAROOMOrderAllCode.Text = lblW8ChannelOrderAllCode.Text = lblHMBSTOrderAllCode.Text = "(0)";
            lblIOSChannelAffirmOrder.Text = lblADRChannelAffirmOrder.Text = lblWPChannelAffirmOrder.Text = lblWAPChannelAffirmOrder.Text = lblProChannelAffirmOrder.Text = lblQUNarChannelAffirmOrder.Text = lbl11ChannelAffirmOrder.Text = lblMJChannelAffirmOrder.Text = lblChannelAffirmOrderCount.Text = lblChannelAffirmOrderPercent.Text = lblGETAROOMAffirmOrder.Text = lblW8ChannelAffirmOrder.Text = lblHMBSTAffirmOrder.Text = "0";
            lblIOSChannelAffirmOrderCode.Text = lblADRChannelAffirmOrderCode.Text = lblWPChannelAffirmOrderCode.Text = lblWAPChannelAffirmOrderCode.Text = lblProChannelAffirmOrderCode.Text = lblQUNarChannelAffirmOrderCode.Text = lbl11ChannelAffirmOrderCode.Text = lblMJChannelAffirmOrderCode.Text = lblChannelAffirmOrderCodeCount.Text = lblGETAROOMAffirmOrderCode.Text = lblW8ChannelAffirmOrderCode.Text = lblHMBSTAffirmOrderCode.Text = "(0)";
            lblIOSChannelNotAffirmOrder.Text = lblADRChannelNotAffirmOrder.Text = lblWPChannelNotAffirmOrder.Text = lblWAPChannelNotAffirmOrder.Text = lblProChannelNotAffirmOrder.Text = lblQUNarChannelNotAffirmOrder.Text = lbl11ChannelNotAffirmOrder.Text = lblMJChannelNotAffirmOrder.Text = lblChannelNotAffirmOrderCount.Text = lblChannelNotAffirmOrderPercent.Text = lblGETAROOMNotAffirmOrder.Text = lblW8ChannelNotAffirmOrder.Text = lblHMBSTNotAffirmOrder.Text = "0";
            lblIOSChannelNotAffirmOrderCode.Text = lblADRChannelNotAffirmOrderCode.Text = lblWPChannelNotAffirmOrderCode.Text = lblWAPChannelNotAffirmOrderCode.Text = lblProChannelNotAffirmOrderCode.Text = lblQUNarChannelNotAffirmOrderCode.Text = lbl11ChannelNotAffirmOrderCode.Text = lblMJChannelNotAffirmOrderCode.Text = lblChannelNotAffirmOrderCodeCount.Text = lblGETAROOMNotAffirmOrderCode.Text = lblW8ChannelNotAffirmOrderCode.Text = lblHMBSTNotAffirmOrderCode.Text = "(0)";
            lblIOSChannelcc.Text = lblADRChannelcc.Text = lblWPChannelcc.Text = lblWAPChannelcc.Text = lblProChannelcc.Text = lblQUNarChannelcc.Text = lbl11Channelcc.Text = lblMJChannelcc.Text = lblChannelccCount.Text = lblChannelccCodePercent.Text = lblGETAROOMChannelcc.Text = lblW8Channelcc.Text = lblHMBSTChannelcc.Text = "0";
            lblIOSChannelccCode.Text = lblADRChannelccCode.Text = lblWPChannelccCode.Text = lblWAPChannelccCode.Text = lblProChannelccCode.Text = lblQUNarChannelccCode.Text = lbl11ChannelccCode.Text = lblMJChannelccCode.Text = lblChannelccCodeCount.Text = lblGETAROOMChannelccCode.Text = lblW8ChannelccCode.Text = lblHMBSTChannelccCode.Text = "(0)";
            lblIOSChannelOther.Text = lblADRChannelOther.Text = lblWPChannelOther.Text = lblWAPChannelOther.Text = lblProChannelOther.Text = lblQUNarChannelOther.Text = lbl11ChannelOther.Text = lblMJChannelOther.Text = lblChannelOtherCount.Text = lblChannelOtherPercent.Text = lblGETAROOMChannelOther.Text = lblW8ChannelOther.Text = lblHMBSTChannelOther.Text = "0";
            lblIOSChannelOtherCode.Text = lblADRChannelOtherCode.Text = lblWPChannelOtherCode.Text = lblWAPChannelOtherCode.Text = lblProChannelOtherCode.Text = lblQUNarChannelOtherCode.Text = lbl11ChannelOtherCode.Text = lblMJChannelOtherCode.Text = lblChannelOtherCodeCount.Text = lblGETAROOMChannelOtherCode.Text = lblW8ChannelOtherCode.Text = lblHMBSTChannelOtherCode.Text = "(0)";
            lblIOSrest.Text = lblADRrest.Text = lblWPrest.Text = lblWAPrest.Text = lblProrest.Text = lblQUNarrest.Text = lbl11rest.Text = lblMJrest.Text = lblrestCount.Text = lblrestPercent.Text = lblGETAROOMrest.Text = lblW8rest.Text = lblHMBSTrest.Text = "0";
            lblIOSrestCode.Text = lblADRrestCode.Text = lblWPrestCode.Text = lblWAPrestCode.Text = lblProrestCode.Text = lblQUNarrestCode.Text = lbl11restCode.Text = lblMJrestCode.Text = lblrestCodeCount.Text = lblGETAROOMrestCode.Text = lblW8restCode.Text = lblHMBSTrestCode.Text = "(0)";

            lblHotelvpMapChannelOrderAll.Text = lblHotelvpMapChannelAffirmOrder.Text = lblHotelvpMapChannelNotAffirmOrder.Text = lblHotelvpMapChannelcc.Text = lblHotelvpMapChannelOther.Text = lblHotelvpMaprest.Text = "0";
            lblHotelvpMapChannelOrderAllCode.Text = lblHotelvpMapChannelAffirmOrderCode.Text = lblHotelvpMapChannelNotAffirmOrderCode.Text = lblHotelvpMapChannelccCode.Text = lblHotelvpMapChannelOtherCode.Text = lblHotelvpMaprestCode.Text = "(0)";

            lblRestsChannelOrderAll.Text = lblRestsChannelAffirmOrder.Text = lblRestsChannelNotAffirmOrder.Text = lblRestsChannelcc.Text = lblRestsChannelOther.Text = lblRestsrest.Text = "0";
            lblRestsChannelOrderAllCode.Text = lblRestsChannelAffirmOrderCode.Text = lblRestsChannelNotAffirmOrderCode.Text = lblRestsChannelccCode.Text = lblRestsChannelOtherCode.Text = lblRestsrestCode.Text = "(0)";
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "-Default-Order-ClearChannel  异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
    }

    public void BindPlanDetail()
    {
        dvPlanDetail.InnerHtml = "<table width='60%'><tr><td style='width:2%'><font color='blue'>酒店上下线计划：</font></td><td style='width:2%'><font color='red'>0 条</font></td><td style='width:2%'><font color='blue'>修改目的地类型：</font></td><td style='width:2%'><font color='red'>0 条</font></td><td style='width:2%'><font color='blue'>修改酒店详情：</font></td><td style='width:2%'><font color='red'>0 条</font></td></tr></table>";
    }

    public void BindUser(string StartDate, string EndDate)
    {
        #region
        MasterInfoEntity _masterInfoEntity = new MasterInfoEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _masterInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _masterInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _masterInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _masterInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _masterInfoEntity.MasterInfoDBEntity = new List<MasterInfoDBEntity>();
        MasterInfoDBEntity msterInfoDBEntity = new MasterInfoDBEntity();
        _masterInfoEntity.MasterInfoDBEntity.Add(msterInfoDBEntity);
        msterInfoDBEntity.RegistStart = StartDate;
        msterInfoDBEntity.RegistEnd = EndDate;

        DataSet dsDetailResult = MasterInfoBP.CommonSelectUser(_masterInfoEntity).QueryResult;

        if (dsDetailResult.Tables["UserCount"].Rows.Count > 0)
        {
            //if ("0".Equals(strToDay))
            //{
            //    lbUserTitle.Text = "昨日用户简报";
            //    lbYesterDate.Text = "昨日（" + dsDetailResult.Tables["UserCount"].Rows[0]["YESTERDATE"].ToString() + "）";
            //}
            //else
            //{
            //    lbUserTitle.Text = "今日用户简报";
            //    lbYesterDate.Text = "今日（" + dsDetailResult.Tables["UserCount"].Rows[0]["YESTERDATE"].ToString() + "）";
            //}
            lbUserTitle.Text = "用户简报";
            lbYesterDate.Text = dsDetailResult.Tables["UserCount"].Rows[0]["StartDate"].ToString() + "--" + dsDetailResult.Tables["UserCount"].Rows[0]["EndDate"].ToString();

            decimal decUserAll = 0;
            decimal decIOSUR = 0;
            decimal decANDUR = 0;
            decimal decWAPUR = 0;
            decimal decWP7UR = 0;
            decimal decW8UR = 0;
            decimal decOther = 0;
            foreach (DataRow drRow in dsDetailResult.Tables["UserCount"].Rows)
            {
                decUserAll = decUserAll + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));

                switch (drRow["COLNMS"].ToString().ToLower())
                {
                    case "ios":
                        decIOSUR = decIOSUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "lm_ios":
                        decIOSUR = decIOSUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "lm_android":
                        decANDUR = decANDUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "android":
                        decANDUR = decANDUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "wap":
                        decWAPUR = decWAPUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "wp":
                        decWP7UR = decWP7UR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "w8":
                        decW8UR = decW8UR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    default:
                        decOther = decOther + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                }
            }

            lbUserAll.Text = decUserAll.ToString("#,##0");
            lbIOSUR.Text = decIOSUR.ToString("#,##0");
            lbANDUR.Text = decANDUR.ToString("#,##0");
            lbWAPUR.Text = decWAPUR.ToString("#,##0");
            lbWP7.Text = decWP7UR.ToString("#,##0");
            lbW8.Text = decW8UR.ToString("#,##0");
            lbOther.Text = decOther.ToString("#,##0");
        }
        else
        {
            //if ("0".Equals(strToDay))
            //{
            //    lbUserTitle.Text = "昨日用户简报";
            //    lbYesterDate.Text = "昨日（）";
            //}
            //else
            //{
            //    lbUserTitle.Text = "今日用户简报";
            //    lbYesterDate.Text = "今日（）";
            //}

            lbUserTitle.Text = "用户简报";
            lbYesterDate.Text = "";

            lbUserAll.Text = "0";
            lbIOSUR.Text = "0";
            lbANDUR.Text = "0";
            lbWAPUR.Text = "0";
            lbWP7.Text = "0";
            lbW8.Text = "0";
            lbOther.Text = "0";
        }
        #endregion

        BindUserNew(StartDate, EndDate);
    }

    public void BindUserNew(string StartDate, string EndDate)
    {
        #region
        MasterInfoEntity _masterInfoEntity = new MasterInfoEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _masterInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _masterInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _masterInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _masterInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _masterInfoEntity.MasterInfoDBEntity = new List<MasterInfoDBEntity>();
        MasterInfoDBEntity msterInfoDBEntity = new MasterInfoDBEntity();
        _masterInfoEntity.MasterInfoDBEntity.Add(msterInfoDBEntity);
        msterInfoDBEntity.RegistStart = StartDate;
        msterInfoDBEntity.RegistEnd = EndDate;

        DataSet dsDetailResult = MasterInfoBP.CommonSelectUserNew(_masterInfoEntity).QueryResult;

        if (dsDetailResult.Tables["UserCount"].Rows.Count > 0)
        {
            decimal decUserAll = 0;
            decimal decUserAllYK = 0;
            decUserAll = ((String.IsNullOrEmpty(dsDetailResult.Tables["UserCount"].Rows[0]["OUSER"].ToString())) ? 0 : decimal.Parse(dsDetailResult.Tables["UserCount"].Rows[0]["OUSER"].ToString()));
            decUserAllYK = ((String.IsNullOrEmpty(dsDetailResult.Tables["UserCount"].Rows[0]["NUSER"].ToString())) ? 0 : decimal.Parse(dsDetailResult.Tables["UserCount"].Rows[0]["NUSER"].ToString()));

            lbUserAll.Text = (decUserAll + decUserAllYK).ToString("#,##0");
            lbXzALLYK.Text = decUserAllYK.ToString("#,##0");
        }
        else
        {
            lbUserAll.Text = "0";
            lbXzALLYK.Text = "0";
        }
        #endregion
    }

    public void BingTodayLoginUser(string StartDate, string EndDate)
    {
        MasterInfoEntity _masterInfoEntity = new MasterInfoEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _masterInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _masterInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _masterInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _masterInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _masterInfoEntity.MasterInfoDBEntity = new List<MasterInfoDBEntity>();
        MasterInfoDBEntity msterInfoDBEntity = new MasterInfoDBEntity();
        _masterInfoEntity.MasterInfoDBEntity.Add(msterInfoDBEntity);
        msterInfoDBEntity.RegistStart = StartDate;
        msterInfoDBEntity.RegistEnd = EndDate;

        DataSet dsTodayLoginResult = MasterInfoBP.CommonSelectTodayLoginUser(_masterInfoEntity).QueryResult;

        if (dsTodayLoginResult.Tables["TodayLoginUserData"].Rows.Count > 0)
        {
            lbYesterDate.Text = "(" + dsTodayLoginResult.Tables["TodayLoginUserData"].Rows[0]["StartDate"].ToString() + "--" + dsTodayLoginResult.Tables["TodayLoginUserData"].Rows[0]["EndDate"].ToString() + ")";

            decimal decLogUserAll = 0;
            decimal decLogIOSUR = 0;
            decimal decLogANDUR = 0;
            decimal decLogWAPUR = 0;
            decimal decLogWP7UR = 0;
            decimal decLogW8UR = 0;
            decimal decLogOther = 0;
            foreach (DataRow drRow in dsTodayLoginResult.Tables["TodayLoginUserData"].Rows)
            {
                decLogUserAll = decLogUserAll + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));

                switch (drRow["use_code"].ToString().ToLower())
                {
                    case "ios":
                        decLogIOSUR = decLogIOSUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "lm_ios":
                        decLogIOSUR = decLogIOSUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "lm_android":
                        decLogANDUR = decLogANDUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "android":
                        decLogANDUR = decLogANDUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "wap":
                        decLogWAPUR = decLogWAPUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "wp":
                        decLogWP7UR = decLogWP7UR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    case "w8":
                        decLogW8UR = decLogW8UR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                    default:
                        decLogOther = decLogOther + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
                        break;
                }
            }

            lbTodayLoginAll.Text = decLogUserAll.ToString("#,##0");
            lblLogIOSUR.Text = decLogIOSUR.ToString("#,##0");
            lbLogANDUR.Text = decLogANDUR.ToString("#,##0");
            lbLogWAPUR.Text = decLogWAPUR.ToString("#,##0");
            lbLogWP7.Text = decLogWP7UR.ToString("#,##0");
            lbLogW8.Text = decLogW8UR.ToString("#,##0");
            lbLogOther.Text = decLogOther.ToString("#,##0");
        }
        else
        {
            lbTodayLoginAll.Text = "0";
            lblLogIOSUR.Text = "0";
            lbLogANDUR.Text = "0";
            lbLogWAPUR.Text = "0";
            lbLogWP7.Text = "0";
            lbLogW8.Text = "0";
            lbLogOther.Text = "0";
        }

        CommonSelectTodayLoginUserNew(StartDate, EndDate);
    }

    public void CommonSelectTodayLoginUserNew(string StartDate, string EndDate)
    {
        MasterInfoEntity _masterInfoEntity = new MasterInfoEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _masterInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _masterInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _masterInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _masterInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _masterInfoEntity.MasterInfoDBEntity = new List<MasterInfoDBEntity>();
        MasterInfoDBEntity msterInfoDBEntity = new MasterInfoDBEntity();
        _masterInfoEntity.MasterInfoDBEntity.Add(msterInfoDBEntity);
        msterInfoDBEntity.RegistStart = StartDate;
        msterInfoDBEntity.RegistEnd = EndDate;

        DataSet dsTodayLoginResult = MasterInfoBP.CommonSelectTodayLoginUserNew(_masterInfoEntity).QueryResult;

        decimal decLogUserAll = 0;
        decimal decLogUserAllYK = 0;
        if (dsTodayLoginResult.Tables["TodayLoginUserDataNew"].Rows.Count > 0)
        {
            decLogUserAll = decLogUserAll + ((String.IsNullOrEmpty(dsTodayLoginResult.Tables["TodayLoginUserDataNew"].Rows[0]["OUSER"].ToString())) ? 0 : decimal.Parse(dsTodayLoginResult.Tables["TodayLoginUserDataNew"].Rows[0]["OUSER"].ToString()));
            decLogUserAllYK = decLogUserAllYK + ((String.IsNullOrEmpty(dsTodayLoginResult.Tables["TodayLoginUserDataNew"].Rows[0]["NUSER"].ToString())) ? 0 : decimal.Parse(dsTodayLoginResult.Tables["TodayLoginUserDataNew"].Rows[0]["NUSER"].ToString()));

            lbTodayLoginAll.Text = (decLogUserAll + decLogUserAllYK).ToString("#,##0");
            lbLgALLYK.Text = decLogUserAllYK.ToString("#,##0");
        }
        else
        {
            lbTodayLoginAll.Text = "0";
            lbLgALLYK.Text = "0";
        }
    }

    public void BindVisibleLable()
    {
        try
        {
            #region
            //string str = "lblLBOrderAllCode,lblLB2OrderAllCode,lblBBOrderAllCode,lblOrderAllCode,lblOrderAllCodePercent,lblLBOrderAffirmCode,lblLB2OrderAffirmCode,lblBBOrderAffirmCode,lblOrderAffirmCode,lblOrderAffirmPercentCode," +
            //         "lblLBOrderNotAffirmOrderCode,lblLB2OrderNotAffirmOrderCode,lblBBOrderNotAffirmOrderCode,lblOrderNotAffirmCode,lblOrderNotAffirmPercentCode,lblLBOrderccCode,lblLB2OrderccCode,lblBB2OrderccCode,lblOrderccCode,lblOrderccCountPercentCode," +
            //         "lblLBOrderOtherCode,lblLB2OrderOtherCode,lblBBOrderOtherCode,lblOrderOtherCode,lblOrderOtherCodePercent,lblLBrestCode,lblLB2restCode,lblBBrestCode,lblrestCode,lblrestCodePercent," +
            //         "lblIOSChannelOrderAllCode,lblAdrChannelOrderAllCode,lblWPChannelOrderAllCode,lblWAPChannelOrderAllCode,lblQUNarChannelOrderAllCode,lbl11ChannelOrderAllCode,lblMJChannelOrderAllCode,lblChannelOrderAllCodeCount,lblIOSChannelAffirmOrderCode,lblADRChannelAffirmOrderCode,lblWPChannelAffirmOrderCode,lblWAPChannelAffirmOrderCode,lblQUNarChannelAffirmOrderCode,lbl11ChannelAffirmOrderCode,lblMJChannelAffirmOrderCode,lblChannelAffirmOrderCodeCount," +
            //         "lblIOSChannelNotAffirmOrderCode,lblADRChannelNotAffirmOrderCode,lblWPChannelNotAffirmOrderCode,lblWAPChannelNotAffirmOrderCode,lblQUNarChannelNotAffirmOrderCode,lbl11ChannelNotAffirmOrderCode,lblMJChannelNotAffirmOrderCode,lblChannelNotAffirmOrderCodeCount,lblIOSChannelccCode,lblADRChannelccCode,lblWPChannelccCode,lblWAPChannelccCode,lblQUNarChannelccCode,lbl11ChannelccCode,lblMJChannelccCode,lblChannelccCodeCount," +
            //         "lblIOSChannelOtherCode,lblADRChannelOtherCode,lblWPChannelOtherCode,lblWAPChannelOtherCode,lblQUNarChannelOtherCode,lbl11ChannelOtherCode,lblMJChannelOtherCode,lblChannelOtherCodeCount,lblIOSrestCode,lblADRrestCode,lblWPrestCode,lblWAPrestCode,lblQUNarrestCode,lbl11restCode,lblMJrestCode,lblrestCodeCount," +
            //         "lblLBOrderTimeOutCode,lblLB2OrderTimeOutCode,lblBBOrderTimeOutCode,lblOrderTimeOutCode," +
            //         "Label3,Label6,Label41,Label44,lblGETAROOMOrderAllCode,lblGETAROOMAffirmOrderCode,lblGETAROOMNotAffirmOrderCode,lblGETAROOMChannelccCode,lblGETAROOMChannelOtherCode,lblGETAROOMrestCode," +
            //         "lblProChannelOrderAllCode,lblProChannelAffirmOrderCode,lblProChannelNotAffirmOrderCode,lblProChannelccCode,lblProChannelOtherCode,lblProrestCode," +
            //         "lblW8ChannelOrderAllCode,lblW8ChannelAffirmOrderCode,lblW8ChannelNotAffirmOrderCode,lblW8ChannelccCode,lblW8ChannelOtherCode,lblW8restCode," +
            //         "lblHMBSTOrderAllCode,lblHMBSTAffirmOrderCode,lblHMBSTNotAffirmOrderCode,lblHMBSTChannelccCode,lblHMBSTChannelOtherCode,lblHMBSTrestCode";
            #endregion

            string str = "Label3,Label6,Label41,Label44," +
                        "lblIOSChannelOrderAllCode,lblIOSChannelAffirmOrderCode,lblIOSChannelNotAffirmOrderCode,lblIOSChannelccCode,lblIOSChannelOtherCode,lblIOSrestCode," +
                        "lblAdrChannelOrderAllCode,lblADRChannelAffirmOrderCode,lblADRChannelNotAffirmOrderCode,lblADRChannelccCode,lblADRChannelOtherCode,lblADRrestCode," +
                        "lblWPChannelOrderAllCode,lblWPChannelAffirmOrderCode,lblWPChannelNotAffirmOrderCode,lblWPChannelccCode,lblWPChannelOtherCode,lblWPrestCode," +
                        "lblW8ChannelOrderAllCode,lblW8ChannelAffirmOrderCode,lblW8ChannelNotAffirmOrderCode,lblW8ChannelccCode,lblW8ChannelOtherCode,lblW8restCode," +
                        "lblWAPChannelOrderAllCode,lblWAPChannelAffirmOrderCode,lblWAPChannelNotAffirmOrderCode,lblWAPChannelccCode,lblWAPChannelOtherCode,lblWAPrestCode," +
                        "lblProChannelOrderAllCode,lblProChannelAffirmOrderCode,lblProChannelNotAffirmOrderCode,lblProChannelccCode,lblProChannelOtherCode,lblProrestCode," +
                        "lblGETAROOMOrderAllCode,lblGETAROOMAffirmOrderCode,lblGETAROOMNotAffirmOrderCode,lblGETAROOMChannelccCode,lblGETAROOMChannelOtherCode,lblGETAROOMrestCode," +
                        "lblHMBSTOrderAllCode,lblHMBSTAffirmOrderCode,lblHMBSTNotAffirmOrderCode,lblHMBSTChannelccCode,lblHMBSTChannelOtherCode,lblHMBSTrestCode," +
                        "lblQUNarChannelOrderAllCode,lblQUNarChannelAffirmOrderCode,lblQUNarChannelNotAffirmOrderCode,lblQUNarChannelccCode,lblQUNarChannelOtherCode,lblQUNarrestCode," +
                        "lbl11ChannelOrderAllCode,lbl11ChannelAffirmOrderCode,lbl11ChannelNotAffirmOrderCode,lbl11ChannelccCode,lbl11ChannelOtherCode,lbl11restCode," +
                        "lblMJChannelOrderAllCode,lblMJChannelAffirmOrderCode,lblMJChannelNotAffirmOrderCode,lblMJChannelccCode,lblMJChannelOtherCode,lblMJrestCode," +
                        "lblChannelOrderAllCodeCount,lblChannelAffirmOrderCodeCount,lblChannelNotAffirmOrderCodeCount,lblChannelccCodeCount,lblChannelOtherCodeCount,lblrestCodeCount," +
                        "lblLBOrderAllCode,lblLBOrderAffirmCode,lblLBOrderNotAffirmOrderCode,lblLBOrderccCode,lblLBOrderOtherCode,lblLBOrderTimeOutCode,lblLBrestCode," +
                        "lblLB2OrderAllCode,lblLB2OrderAffirmCode,lblLB2OrderNotAffirmOrderCode,lblLB2OrderccCode,lblLB2OrderOtherCode,lblLB2OrderTimeOutCode,lblLB2restCode," +
                        "lblBBOrderAllCode,lblBBOrderAffirmCode,lblBBOrderNotAffirmOrderCode,lblBB2OrderccCode,lblBBOrderOtherCode,lblBBOrderTimeOutCode,lblBBrestCode," +
                        "lblOrderAllCode,lblOrderAffirmCode,lblOrderNotAffirmCode,lblOrderccCode,lblOrderOtherCode,lblOrderTimeOutCode,lblrestCode," +
                        "lblOrderAllCodePercent,lblOrderAffirmPercentCode,lblOrderNotAffirmPercentCode,lblOrderccCountPercentCode,lblOrderOtherCodePercent,lblOrderTimeOutCodePercent,lblrestCodePercent," +
                        "lblHotelvpMapChannelOrderAllCode,lblHotelvpMapChannelAffirmOrderCode,lblHotelvpMapChannelNotAffirmOrderCode,lblHotelvpMapChannelccCode,lblHotelvpMapChannelOtherCode,lblHotelvpMaprestCode," +
                        "lblRestsChannelOrderAllCode,lblRestsChannelAffirmOrderCode,lblRestsChannelNotAffirmOrderCode,lblRestsChannelccCode,lblRestsChannelOtherCode,lblRestsrestCode";

            string[] strLbl = str.Split(',');
            for (int i = 0; i < strLbl.Length; i++)
            {
                string a = strLbl[i].ToString();
                Label ctr = right.FindControl(a) as Label;
                if (ctr != null)
                {
                    if (ctr.Text == "(0)" || ctr.Text == "0" || ctr.Text == "(0%)")
                    {
                        string a1 = ctr.Text;
                        ctr.Visible = false;
                    }
                    else
                    {
                        ctr.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order-BindVisibleLable.txt", "Default-Order异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
    }

    #region
    private void BindViewCSUserListDetail(string strToDay)
    {
        MasterInfoEntity _masterInfoEntity = new MasterInfoEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _masterInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _masterInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _masterInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _masterInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _masterInfoEntity.MasterInfoDBEntity = new List<MasterInfoDBEntity>();
        MasterInfoDBEntity msterInfoDBEntity = new MasterInfoDBEntity();
        _masterInfoEntity.MasterInfoDBEntity.Add(msterInfoDBEntity);
        msterInfoDBEntity.Today = strToDay;

        //DataSet dsDetailResult = MasterInfoBP.CommonSelect(_masterInfoEntity).QueryResult;

        //以下代码  注释
        //lbYesterDate2.Text = "";
        //lbYesterDate.Text = "";
        //lbTitle.Text = "昨日订单简报";
        //以下代码  注释
        #region
        //if (dsDetailResult.Tables.Count > 0)
        //{
        //    if (dsDetailResult.Tables["OrderAll"].Rows.Count > 0)
        //    {
        //        decimal decOrderInAll = String.IsNullOrEmpty(dsDetailResult.Tables["OrderAll"].Rows[0][0].ToString().Trim()) ? 0 : decimal.Parse(dsDetailResult.Tables["OrderAll"].Rows[0][0].ToString().Trim());
        //        lbOrderInAll.Text = decOrderInAll.ToString("#,##0");
        //    }
        //    else
        //    {
        //        lbOrderInAll.Text = "0";
        //    }

        //    if (dsDetailResult.Tables["AllTic"].Rows.Count > 0)
        //    {
        //        decimal decSUTicket = String.IsNullOrEmpty(dsDetailResult.Tables["AllTic"].Rows[0]["SUTicket"].ToString().Trim()) ? 0 : decimal.Parse(dsDetailResult.Tables["AllTic"].Rows[0]["SUTicket"].ToString().Trim());
        //        decimal decALTicket = String.IsNullOrEmpty(dsDetailResult.Tables["AllTic"].Rows[0]["ALTicket"].ToString().Trim()) ? 0 : decimal.Parse(dsDetailResult.Tables["AllTic"].Rows[0]["ALTicket"].ToString().Trim());
        //        lbSUTicket.Text = (decSUTicket > 0 ) ? " (" + decSUTicket.ToString("#,##0") + ")" : "";
        //        lbALTicket.Text = (decALTicket > 0 ) ? " (" + decALTicket.ToString("#,##0") + ")" : "";
        //    }
        //    else
        //    {
        //        lbSUTicket.Text = "";
        //        lbALTicket.Text = "";
        //    }

        //    if (dsDetailResult.Tables["OrderTic"].Rows.Count > 0)
        //    {
        //        string ticpricecode = string.Empty;
        //        string ticcolvalue = string.Empty;

        //        decimal decYFTicAll = 0;
        //        decimal decXFTicAll = 0;
        //        decimal decCXFTicAll = 0;

        //        foreach (DataRow drRow in dsDetailResult.Tables["OrderTic"].Rows)
        //        {
        //            ticpricecode = drRow["PRICECODE"].ToString().Trim().ToLower();
        //            ticcolvalue = drRow["COLVALUE"].ToString().Trim();
        //            if ("lmbar".Equals(ticpricecode))
        //            {
        //                decYFTicAll = decYFTicAll + (String.IsNullOrEmpty(ticcolvalue) ? 0 : decimal.Parse(ticcolvalue));
        //            }
        //            else if ("lmbar2".Equals(ticpricecode))
        //            {
        //                decXFTicAll = decXFTicAll + (String.IsNullOrEmpty(ticcolvalue) ? 0 : decimal.Parse(ticcolvalue));
        //            }
        //            else if ("barb".Equals(ticpricecode) || "bar".Equals(ticpricecode))
        //            {
        //                decCXFTicAll = decCXFTicAll + (String.IsNullOrEmpty(ticcolvalue) ? 0 : decimal.Parse(ticcolvalue));
        //            }
        //        }

        //        lbYFTicAll.Text = (decYFTicAll > 0) ? " (" + decYFTicAll.ToString("#,##0") + ")" : "";
        //        lbXFTicAll.Text = (decXFTicAll > 0) ? " (" + decXFTicAll.ToString("#,##0") + ")" : "";
        //        lbCXFTicAll.Text = (decCXFTicAll > 0) ? " (" + decCXFTicAll.ToString("#,##0") + ")" : "";
        //    }
        //    else
        //    {
        //        lbYFTicAll.Text = "";
        //        lbXFTicAll.Text = "";
        //        lbCXFTicAll.Text = "";
        //    }

        //    if (dsDetailResult.Tables["RoomTic"].Rows.Count > 0)
        //    {
        //        decimal decRoomTicAll = String.IsNullOrEmpty(dsDetailResult.Tables["RoomTic"].Rows[0]["COLVALUE"].ToString().Trim()) ? 0 : decimal.Parse(dsDetailResult.Tables["RoomTic"].Rows[0]["COLVALUE"].ToString().Trim());
        //        lbRoomTicAll.Text = (decRoomTicAll > 0) ? " (" + decRoomTicAll.ToString("#,##0") + ")" : "";
        //    }
        //    else
        //    {
        //        lbRoomTicAll.Text = "";
        //    }
        //    #region
        //    decimal decOrderSum = 0;
        //    if (dsDetailResult.Tables["OrderSum"].Rows.Count > 0)
        //    {
        //        if ("0".Equals(strToDay))
        //        {
        //            lbTitle.Text = "昨日订单简报";
        //            lbYesterDate2.Text = "昨日（" + dsDetailResult.Tables["OrderSum"].Rows[0]["YESTERDATE2"].ToString() + "）";
        //            hidDate.Value = dsDetailResult.Tables["OrderSum"].Rows[0]["YESTERDATE2"].ToString();
        //            hidDateType.Value = "0";
        //        }
        //        else
        //        {
        //            lbTitle.Text = "今日订单简报";
        //            lbYesterDate2.Text = "今日（" + dsDetailResult.Tables["OrderSum"].Rows[0]["YESTERDATE2"].ToString() + "）";
        //            hidDate.Value = dsDetailResult.Tables["OrderSum"].Rows[0]["YESTERDATE2"].ToString();
        //            hidDateType.Value = "1";
        //        }
        //        //lbOrderAll.Text = (String.IsNullOrEmpty(dsDetailResult.Tables["OrderSum"].Rows[0]["SUMPRICE"].ToString())) ? "0" : decimal.Parse(dsDetailResult.Tables["OrderSum"].Rows[0]["SUMPRICE"].ToString()).ToString("#,##0");
        //        decOrderSum = (String.IsNullOrEmpty(dsDetailResult.Tables["OrderSum"].Rows[0]["SUMPRICE"].ToString())) ? 0 : decimal.Parse(dsDetailResult.Tables["OrderSum"].Rows[0]["SUMPRICE"].ToString());
        //    }
        //    else
        //    {
        //        if ("0".Equals(strToDay))
        //        {
        //            lbTitle.Text = "昨日订单简报";
        //            lbYesterDate2.Text = "昨日（）";
        //        }
        //        else
        //        {
        //            lbTitle.Text = "今日订单简报";
        //            lbYesterDate2.Text = "今日（）";
        //        }

        //        lbOrderAll.Text = "0";
        //        lbUnvOrderPr.Text = "0";
        //    }
        //    #endregion
        //    decimal decOrderAll = 0;

        //    decimal decYFOrderIOS = 0;
        //    decimal decYFOrderAND = 0;
        //    decimal decYFOrderWAP = 0;
        //    decimal decYFOrderWP7 = 0;
        //    decimal decYFOrderHTK = 0;
        //    decimal decYFOrderQER = 0;

        //    decimal decXFOrderIOS = 0;
        //    decimal decXFOrderAND = 0;
        //    decimal decXFOrderWAP = 0;
        //    decimal decXFOrderWP7 = 0;
        //    decimal decXFOrderHTK = 0;
        //    decimal decXFOrderQER = 0;

        //    decimal decCXFOrderIOS = 0;
        //    decimal decCXFOrderAND = 0;
        //    decimal decCXFOrderWAP = 0;
        //    decimal decCXFOrderWP7 = 0;
        //    decimal decCXFOrderHTK = 0;
        //    decimal decCXFOrderQER = 0;
        //    #region
        //    if (dsDetailResult.Tables["OrderList"].Rows.Count > 0)
        //    {
        //        string pricecode = "";
        //        string booksource = "";
        //        string colvalue = "";
        //        foreach (DataRow drRow in dsDetailResult.Tables["OrderList"].Rows)
        //        {
        //            pricecode = drRow["PRICECODE"].ToString().Trim().ToLower();
        //            booksource = drRow["BOOKSOURCE"].ToString().Trim().ToLower();
        //            colvalue = drRow["COLVALUE"].ToString().Trim().ToLower();
        //            if ("lmbar".Equals(pricecode))
        //            {
        //                if ("lm_ios".Equals(booksource) || "ios".Equals(booksource))
        //                {
        //                    decYFOrderIOS = decYFOrderIOS + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("lm_android".Equals(booksource) || "android".Equals(booksource))
        //                {
        //                    decYFOrderAND = decYFOrderAND + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("lm_web".Equals(booksource) || "lm_116114web".Equals(booksource))
        //                {
        //                    decYFOrderWAP = decYFOrderWAP + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("wp".Equals(booksource))
        //                {
        //                    decYFOrderWP7 = decYFOrderWP7 + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("htk".Equals(booksource))
        //                {
        //                    decYFOrderHTK = decYFOrderHTK + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("qunar".Equals(booksource))
        //                {
        //                    decYFOrderQER = decYFOrderQER + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //            }
        //            else if ("lmbar2".Equals(pricecode))
        //            {
        //                if ("lm_ios".Equals(booksource) || "ios".Equals(booksource))
        //                {
        //                    decXFOrderIOS = decXFOrderIOS + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("lm_android".Equals(booksource) || "android".Equals(booksource))
        //                {
        //                    decXFOrderAND = decXFOrderAND + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("lm_web".Equals(booksource) || "lm_116114web".Equals(booksource))
        //                {
        //                    decXFOrderWAP = decXFOrderWAP + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("wp".Equals(booksource))
        //                {
        //                    decXFOrderWP7 = decXFOrderWP7 + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("htk".Equals(booksource))
        //                {
        //                    decXFOrderHTK = decXFOrderHTK + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("qunar".Equals(booksource))
        //                {
        //                    decXFOrderQER = decXFOrderQER + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //            }
        //            else if ("barb".Equals(pricecode) || "bar".Equals(pricecode))
        //            {
        //                if ("lm_ios".Equals(booksource) || "ios".Equals(booksource))
        //                {
        //                    decCXFOrderIOS = decCXFOrderIOS + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("lm_android".Equals(booksource) || "android".Equals(booksource))
        //                {
        //                    decCXFOrderAND = decCXFOrderAND + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("lm_web".Equals(booksource) || "lm_116114web".Equals(booksource))
        //                {
        //                    decCXFOrderWAP = decCXFOrderWAP + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("wp".Equals(booksource))
        //                {
        //                    decCXFOrderWP7 = decCXFOrderWP7 + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("htk".Equals(booksource))
        //                {
        //                    decCXFOrderHTK = decCXFOrderHTK + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //                else if ("qunar".Equals(booksource))
        //                {
        //                    decCXFOrderQER = decCXFOrderQER + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                }
        //            }
        //        }

        //        lbYFOrderAll.Text = (decYFOrderIOS + decYFOrderAND + decYFOrderWAP + decYFOrderWP7 + decYFOrderHTK + decYFOrderQER).ToString("#,##0");
        //        lbYFOrderIOS.Text = decYFOrderIOS.ToString("#,##0");
        //        lbYFOrderAND.Text = decYFOrderAND.ToString("#,##0");
        //        lbYFOrderWAP.Text = decYFOrderWAP.ToString("#,##0");
        //        lbYFOrderWP7.Text = decYFOrderWP7.ToString("#,##0");
        //        lbYFOrderHTK.Text = decYFOrderHTK.ToString("#,##0");
        //        lbYFOrderQER.Text = decYFOrderQER.ToString("#,##0");

        //        lbXFOrderAll.Text = (decXFOrderIOS + decXFOrderAND + decXFOrderWAP + decXFOrderWP7 + decXFOrderHTK + decXFOrderQER).ToString("#,##0");
        //        lbXFOrderIOS.Text = decXFOrderIOS.ToString("#,##0");
        //        lbXFOrderAND.Text = decXFOrderAND.ToString("#,##0");
        //        lbXFOrderWAP.Text = decXFOrderWAP.ToString("#,##0");
        //        lbXFOrderWP7.Text = decXFOrderWP7.ToString("#,##0");
        //        lbXFOrderHTK.Text = decXFOrderHTK.ToString("#,##0");
        //        lbXFOrderQER.Text = decXFOrderQER.ToString("#,##0");

        //        lbCXFOrderAll.Text = (decCXFOrderIOS + decCXFOrderAND + decCXFOrderWAP + decCXFOrderWP7 + decCXFOrderHTK + decCXFOrderQER).ToString("#,##0");
        //        lbCXFOrderIOS.Text = decCXFOrderIOS.ToString("#,##0");
        //        lbCXFOrderAND.Text = decCXFOrderAND.ToString("#,##0");
        //        lbCXFOrderWAP.Text = decCXFOrderWAP.ToString("#,##0");
        //        lbCXFOrderWP7.Text = decCXFOrderWP7.ToString("#,##0");
        //        lbCXFOrderHTK.Text = decCXFOrderHTK.ToString("#,##0");
        //        lbCXFOrderQER.Text = decCXFOrderQER.ToString("#,##0");
        //    }
        //    else
        //    {
        //        lbYFOrderAll.Text = "0";
        //        lbYFOrderIOS.Text = "0";
        //        lbYFOrderAND.Text = "0";
        //        lbYFOrderWAP.Text = "0";
        //        lbYFOrderWP7.Text = "0";
        //        lbYFOrderHTK.Text = "0";
        //        lbYFOrderQER.Text = "0";

        //        lbXFOrderAll.Text = "0";
        //        lbXFOrderIOS.Text = "0";
        //        lbXFOrderAND.Text = "0";
        //        lbXFOrderWAP.Text = "0";
        //        lbXFOrderWP7.Text = "0";
        //        lbXFOrderHTK.Text = "0";
        //        lbXFOrderQER.Text = "0";

        //        lbCXFOrderAll.Text = "0";
        //        lbCXFOrderIOS.Text = "0";
        //        lbCXFOrderAND.Text = "0";
        //        lbCXFOrderWAP.Text = "0";
        //        lbCXFOrderWP7.Text = "0";
        //        lbCXFOrderHTK.Text = "0";
        //        lbCXFOrderQER.Text = "0";
        //    }
        //    #endregion
        //    SetChartPic();
        //    decOrderAll = decYFOrderIOS + decYFOrderAND + decYFOrderWAP + decYFOrderWP7 + decYFOrderHTK + decYFOrderQER + decXFOrderIOS + decXFOrderAND + decXFOrderWAP + decXFOrderWP7 + decXFOrderHTK + decXFOrderQER + decCXFOrderIOS + decCXFOrderAND + decCXFOrderWAP + decCXFOrderWP7 + decCXFOrderHTK + decCXFOrderQER;
        //    if (decOrderAll == 0)
        //    {
        //        lbOrderAll.Text = "0";
        //        lbUnvOrderPr.Text = "0";
        //    }
        //    else
        //    {
        //        lbOrderAll.Text = decOrderAll.ToString("#,##0");
        //        lbUnvOrderPr.Text = (decOrderSum / decOrderAll).ToString("#,###.##");
        //    }
        //    #region
        //    if (dsDetailResult.Tables["UserCount"].Rows.Count > 0)
        //    {
        //        if ("0".Equals(strToDay))
        //        {
        //            lbUserTitle.Text = "昨日用户简报";
        //            //lbYesterDate2.Text = "昨日（）";
        //            lbYesterDate.Text = "昨日（" + dsDetailResult.Tables["UserCount"].Rows[0]["YESTERDATE"].ToString() + "）";
        //        }
        //        else
        //        {
        //            lbUserTitle.Text = "今日用户简报";
        //            //lbYesterDate2.Text = "昨日（）";
        //            lbYesterDate.Text = "今日（" + dsDetailResult.Tables["UserCount"].Rows[0]["YESTERDATE"].ToString() + "）";
        //        }

        //        decimal decUserAll = 0;
        //        decimal decIOSUR = 0;
        //        decimal decANDUR = 0;
        //        decimal decWAPUR = 0;
        //        decimal decWP7UR = 0;
        //        decimal decOther = 0;
        //        foreach (DataRow drRow in dsDetailResult.Tables["UserCount"].Rows)
        //        {
        //            decUserAll = decUserAll + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));

        //            switch (drRow["COLNMS"].ToString().ToLower())
        //            {
        //                case "ios":
        //                    decIOSUR = decIOSUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                case "lm_ios":
        //                    decIOSUR = decIOSUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                case "lm_android":
        //                    decANDUR = decANDUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                case "android":
        //                    decANDUR = decANDUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                case "wap":
        //                    decWAPUR = decWAPUR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                 case "wp":
        //                    decWP7UR = decWP7UR + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                default:
        //                    decOther = decOther + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //            }
        //        }

        //        lbUserAll.Text = decUserAll.ToString("#,##0");
        //        lbIOSUR.Text = decIOSUR.ToString("#,##0");
        //        lbANDUR.Text = decANDUR.ToString("#,##0");
        //        lbWAPUR.Text = decWAPUR.ToString("#,##0");
        //        lbWP7.Text = decWP7UR.ToString("#,##0");
        //        lbOther.Text = decOther.ToString("#,##0");
        //    }
        //    else
        //    {
        //        if ("0".Equals(strToDay))
        //        {
        //            lbUserTitle.Text = "昨日用户简报";
        //            //lbYesterDate2.Text = "昨日（）";
        //            lbYesterDate.Text = "昨日（）";
        //        }
        //        else
        //        {
        //            lbUserTitle.Text = "今日用户简报";
        //            //lbYesterDate2.Text = "昨日（）";
        //            lbYesterDate.Text = "今日（）";
        //        }

        //        lbUserAll.Text = "0";
        //        lbIOSUR.Text = "0";
        //        lbANDUR.Text = "0";
        //        lbWAPUR.Text = "0";
        //        lbWP7.Text = "0";
        //        lbOther.Text = "0";
        //    }
        //    #endregion
        //    string strPlanDetail = "";
        //    strPlanDetail = strPlanDetail + "<table width='60%'><tr>";

        //    if (dsDetailResult.Tables["ProcCount"].Rows.Count > 0)
        //    {
        //        foreach (DataRow drRow in dsDetailResult.Tables["ProcCount"].Rows)
        //        {
        //            strPlanDetail = strPlanDetail + "<td style='width:2%'><font color='blue'>" + drRow["COLNMS"].ToString() + "：</font></td><td style='width:2%'><font color='red'>" + drRow["COLVALUE"].ToString() + "</font><font color='blue'> 条</font></td>";
        //        }
        //    }
        //    else
        //    {
        //        strPlanDetail = strPlanDetail + "<td style='width:2%'><font color='blue'>酒店上下线计划：</font></td><td style='width:2%'><font color='red'>0 条</font></td><td style='width:2%'><font color='blue'>修改目的地类型：</font></td><td style='width:2%'><font color='red'>0 条</font></td><td style='width:2%'><font color='blue'>修改酒店详情：</font></td><td style='width:2%'><font color='red'>0 条</font></td>";
        //    }

        //    strPlanDetail = strPlanDetail + "</tr></table>";

        //    dvPlanDetail.InnerHtml = strPlanDetail;
        //    //lbPlanOnOff.Text = "";
        //    //lbFtType.Text = "";
        //    //lbHotelInfo.Text = "";


        //    decimal decInRoomAll  = 0;
        //    decimal decInRoomIOS = 0;
        //    decimal decInRoomAND = 0;
        //    decimal decInRoomWAP = 0;

        //    if (dsDetailResult.Tables["InRoomCount"].Rows.Count > 0)
        //    {
        //        foreach (DataRow drRow in dsDetailResult.Tables["InRoomCount"].Rows)
        //        {
        //            decInRoomAll = decInRoomAll + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));

        //            switch (drRow["BOOKSOURCE"].ToString().ToLower())
        //            {
        //                case "ios":
        //                    decInRoomIOS = decInRoomIOS + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                case "lm_ios":
        //                    decInRoomIOS = decInRoomIOS + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                case "lm_android":
        //                    decInRoomAND = decInRoomAND + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                case "android":
        //                    decInRoomAND = decInRoomAND + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                case "wap":
        //                    decInRoomWAP = decInRoomWAP + ((String.IsNullOrEmpty(drRow["COLVALUE"].ToString())) ? 0 : decimal.Parse(drRow["COLVALUE"].ToString()));
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }

        //        lbInRoomAll.Text = decInRoomAll.ToString("#,##0");
        //        lbInRoomIOS.Text = decInRoomIOS.ToString("#,##0");
        //        lbInRoomAND.Text = decInRoomAND.ToString("#,##0");
        //        lbInRoomWAP.Text = decInRoomWAP.ToString("#,##0");
        //    }
        //    else
        //    {
        //        lbInRoomAll.Text = "0";
        //        lbInRoomIOS.Text = "0";
        //        lbInRoomAND.Text = "0";
        //        lbInRoomWAP.Text = "0";
        //    }
        //}
        //else
        //{
        //    lbOrderAll.Text = "0";
        //    lbUnvOrderPr.Text = "0";

        //    lbYFOrderAll.Text = "0";
        //    lbYFOrderIOS.Text = "0";
        //    lbYFOrderAND.Text = "0";
        //    lbYFOrderWAP.Text = "0";

        //    lbXFOrderAll.Text = "0";
        //    lbXFOrderIOS.Text = "0";
        //    lbXFOrderAND.Text = "0";
        //    lbXFOrderWAP.Text = "0";

        //    lbCXFOrderAll.Text = "0";
        //    lbCXFOrderIOS.Text = "0";
        //    lbCXFOrderAND.Text = "0";
        //    lbCXFOrderWAP.Text = "0";

        //    lbUserAll.Text = "0";
        //    lbIOSUR.Text = "0";
        //    lbANDUR.Text = "0";
        //    lbWAPUR.Text = "0";
        //    lbOther.Text = "0";

        //    lbInRoomAll.Text = "0";
        //    lbInRoomIOS.Text = "0";
        //    lbInRoomAND.Text = "0";
        //    lbInRoomWAP.Text = "0";

        //    dvPlanDetail.InnerHtml = "<table width='60%'><tr><td style='width:2%'><font color='blue'>酒店上下线计划：</font></td><td style='width:2%'><font color='red'>0 条</font></td><td style='width:2%'><font color='blue'>修改目的地类型：</font></td><td style='width:2%'><font color='red'>0 条</font></td><td style='width:2%'><font color='blue'>修改酒店详情：</font></td><td style='width:2%'><font color='red'>0 条</font></td></tr></table>";
        //    //lbPlanOnOff.Text = "0";
        //    //lbFtType.Text = "0";
        //    //lbHotelInfo.Text = "0";
        //}
        #endregion
    }

    //注释
    private void SetChartPic()
    {
        //Series seriesPies = new Series("销售情况");
        //seriesPies.ChartType = SeriesChartType.Pie;
        //seriesPies.BorderWidth = 3;
        //seriesPies.ShadowOffset = 2;
        //this.ChartPie.Height = 164;
        //this.ChartPie.Width = 215;
        //this.ChartPie.Series.Clear();
        //this.ChartPie.Series.Add(seriesPies);

        //Title tPie = new Title();
        //this.ChartPie.Titles.Add(tPie);

        ////画饼图
        //if (!String.IsNullOrEmpty(lbYFOrderAll.Text) && decimal.Parse(lbYFOrderAll.Text) > 0)
        //{
        //    seriesPies.Points.AddXY("LM预付" + lbYFOrderAll.Text + "张", lbYFOrderAll.Text);
        //}

        //if (!String.IsNullOrEmpty(lbXFOrderAll.Text) && decimal.Parse(lbXFOrderAll.Text) > 0)
        //{
        //    seriesPies.Points.AddXY("LM现付" + lbXFOrderAll.Text + "张", lbXFOrderAll.Text);
        //}

        //if (!String.IsNullOrEmpty(lbCXFOrderAll.Text) && decimal.Parse(lbCXFOrderAll.Text) > 0)
        //{
        //    seriesPies.Points.AddXY("常规现付" + lbCXFOrderAll.Text + "张", lbCXFOrderAll.Text);
        //}
    }

    #region
    //protected void lkYesterDay_Click(object sender, EventArgs e)
    //{
    //    //1  今天   2  昨天
    //    //BindViewCSUserListDetail("0");
    //}

    //protected void lkToDay_Click(object sender, EventArgs e)
    //{
    //    //1  今天   2  昨天
    //    //BindViewCSUserListDetail("1");

    //}
    #endregion

    #region   渠道统计
    protected void lkChannelYesterDay_Click(object sender, EventArgs e)
    {
        ClearChannel();
        //BindViewChannelDataList("0");
    }

    protected void lkChannelToDay_Click(object sender, EventArgs e)
    {
        ClearChannel();
        //BindViewChannelDataList("1");
    }
    #endregion

    #region   订单统计
    protected void lkOrderYesterDay_Click(object sender, EventArgs e)
    {
        ClearOrder();
        //BindViewOrderDataList("0");
    }

    protected void lkOrderToDay_Click(object sender, EventArgs e)
    {
        ClearOrder();
        //BindViewOrderDataList("1");
    }
    #endregion
    #endregion

    /// <summary>
    /// 搜素日期段的数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSeach_Click(object sender, EventArgs e)
    {
        this.divMain.Attributes["style"] = "display:''";
        try
        {
            if (!System.IO.File.Exists("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt"))
            {
                System.IO.File.Create("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt").Close();
            }
            else
            {
                System.IO.File.WriteAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "", System.Text.Encoding.GetEncoding("GB2312"));
            }
            string StartDate = this.StartDate.Value.Trim();
            string EndDate = this.EndDate.Value.Trim();
            string endDate = DateTime.Parse(this.EndDate.Value).AddDays(1).ToShortDateString();
            ClearOrder();
            ClearChannel();
            GetResultData(StartDate, endDate);
            BindViewChannelDataList();
            BindViewOrderDataList();
            BindVisibleLable();
            try
            {
                BindUser(StartDate, endDate);
                BingTodayLoginUser(StartDate, endDate);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "BindUser AND  BingTodayLoginUser异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
            }

            ReLoadODCount();
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText("C:\\ConsultLog\\" + UserSession.Current.UserAccount + "-Default-Order.txt", "Default-Order异常信息：" + ex.Message.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
        }
    }

    private void ReLoadODCount()
    {
        string str = "lblIOSChannelOrderAll,lblAdrChannelOrderAll,lblWPChannelOrderAll,lblWAPChannelOrderAll,lblProChannelOrderAll,lblQUNarChannelOrderAll,lbl11ChannelOrderAll,lblMJChannelOrderAll,lblChannelOrderAllCount,lblChannelOrderAllPercent,lblGETAROOMOrderAll,lblW8ChannelOrderAll,lblHMBSTOrderAll," +
            "lblIOSChannelOrderAllCode,lblAdrChannelOrderAllCode,lblWPChannelOrderAllCode,lblWAPChannelOrderAllCode,lblProChannelOrderAllCode,lblQUNarChannelOrderAllCode,lbl11ChannelOrderAllCode,lblMJChannelOrderAllCode,lblChannelOrderAllCodeCount,lblGETAROOMOrderAllCode,lblW8ChannelOrderAllCode,lblHMBSTOrderAllCode," +
            "lblIOSChannelAffirmOrder,lblADRChannelAffirmOrder,lblWPChannelAffirmOrder,lblWAPChannelAffirmOrder,lblProChannelAffirmOrder,lblQUNarChannelAffirmOrder,lbl11ChannelAffirmOrder,lblMJChannelAffirmOrder,lblChannelAffirmOrderCount,lblChannelAffirmOrderPercent,lblGETAROOMAffirmOrder,lblW8ChannelAffirmOrder,lblHMBSTAffirmOrder," +
            "lblIOSChannelAffirmOrderCode,lblADRChannelAffirmOrderCode,lblWPChannelAffirmOrderCode,lblWAPChannelAffirmOrderCode,lblProChannelAffirmOrderCode,lblQUNarChannelAffirmOrderCode,lbl11ChannelAffirmOrderCode,lblMJChannelAffirmOrderCode,lblChannelAffirmOrderCodeCount,lblGETAROOMAffirmOrderCode,lblW8ChannelAffirmOrderCode,lblHMBSTAffirmOrderCode," +
            "lblIOSChannelNotAffirmOrder,lblADRChannelNotAffirmOrder,lblWPChannelNotAffirmOrder,lblWAPChannelNotAffirmOrder,lblProChannelNotAffirmOrder,lblQUNarChannelNotAffirmOrder,lbl11ChannelNotAffirmOrder,lblMJChannelNotAffirmOrder,lblChannelNotAffirmOrderCount,lblChannelNotAffirmOrderPercent,lblGETAROOMNotAffirmOrder,lblW8ChannelNotAffirmOrder,lblHMBSTNotAffirmOrder," +
            "lblIOSChannelNotAffirmOrderCode,lblADRChannelNotAffirmOrderCode,lblWPChannelNotAffirmOrderCode,lblWAPChannelNotAffirmOrderCode,lblProChannelNotAffirmOrderCode,lblQUNarChannelNotAffirmOrderCode,lbl11ChannelNotAffirmOrderCode,lblMJChannelNotAffirmOrderCode,lblChannelNotAffirmOrderCodeCount,lblGETAROOMNotAffirmOrderCode,lblW8ChannelNotAffirmOrderCode,lblHMBSTNotAffirmOrderCode," +
            "lblIOSChannelcc,lblADRChannelcc,lblWPChannelcc,lblWAPChannelcc,lblProChannelcc,lblQUNarChannelcc,lbl11Channelcc,lblMJChannelcc,lblChannelccCount,lblChannelccCodePercent,lblGETAROOMChannelcc,lblW8Channelcc,lblHMBSTChannelcc," +
            "lblIOSChannelccCode,lblADRChannelccCode,lblWPChannelccCode,lblWAPChannelccCode,lblProChannelccCode,lblQUNarChannelccCode,lbl11ChannelccCode,lblMJChannelccCode,lblChannelccCodeCount,lblGETAROOMChannelccCode,lblW8ChannelccCode,lblHMBSTChannelccCode," +
            "lblIOSChannelOther,lblADRChannelOther,lblWPChannelOther,lblWAPChannelOther,lblProChannelOther,lblQUNarChannelOther,lbl11ChannelOther,lblMJChannelOther,lblChannelOtherCount,lblChannelOtherPercent,lblGETAROOMChannelOther,lblW8ChannelOther,lblHMBSTChannelOther," +
            "lblIOSChannelOtherCode,lblADRChannelOtherCode,lblWPChannelOtherCode,lblWAPChannelOtherCode,lblProChannelOtherCode,lblQUNarChannelOtherCode,lbl11ChannelOtherCode,lblMJChannelOtherCode,lblChannelOtherCodeCount,lblGETAROOMChannelOtherCode,lblW8ChannelOtherCode,lblHMBSTChannelOtherCode," +
            "lblIOSrest,lblADRrest,lblWPrest,lblWAPrest,lblProrest,lblQUNarrest,lbl11rest,lblMJrest,lblrestCount,lblrestPercent,lblGETAROOMrest,lblW8rest,lblHMBSTrest," +
            "lblIOSrestCode,lblADRrestCode,lblWPrestCode,lblWAPrestCode,lblProrestCode,lblQUNarrestCode,lbl11restCode,lblMJrestCode,lblrestCodeCount,lblGETAROOMrestCode,lblW8restCode,lblHMBSTrestCode," +
            "lblHotelvpMapChannelOrderAll,lblHotelvpMapChannelAffirmOrder,lblHotelvpMapChannelNotAffirmOrder,lblHotelvpMapChannelcc,lblHotelvpMapChannelOther,lblHotelvpMaprest," +
            "lblHotelvpMapChannelOrderAllCode,lblHotelvpMapChannelAffirmOrderCode,lblHotelvpMapChannelNotAffirmOrderCode,lblHotelvpMapChannelccCode,lblHotelvpMapChannelOtherCode,lblHotelvpMaprestCode," +
            "lblRestsChannelOrderAll,lblRestsChannelAffirmOrder,lblRestsChannelNotAffirmOrder,lblRestsChannelcc,lblRestsChannelOther,lblRestsrest," +
            "lblRestsChannelOrderAllCode,lblRestsChannelAffirmOrderCode,lblRestsChannelNotAffirmOrderCode,lblRestsChannelccCode,lblRestsChannelOtherCode,lblRestsrestCode" +
             "Label3,Label6,Label41,Label44," +
                       "lblIOSChannelOrderAllCode,lblIOSChannelAffirmOrderCode,lblIOSChannelNotAffirmOrderCode,lblIOSChannelccCode,lblIOSChannelOtherCode,lblIOSrestCode," +
                       "lblAdrChannelOrderAllCode,lblADRChannelAffirmOrderCode,lblADRChannelNotAffirmOrderCode,lblADRChannelccCode,lblADRChannelOtherCode,lblADRrestCode," +
                       "lblWPChannelOrderAllCode,lblWPChannelAffirmOrderCode,lblWPChannelNotAffirmOrderCode,lblWPChannelccCode,lblWPChannelOtherCode,lblWPrestCode," +
                       "lblW8ChannelOrderAllCode,lblW8ChannelAffirmOrderCode,lblW8ChannelNotAffirmOrderCode,lblW8ChannelccCode,lblW8ChannelOtherCode,lblW8restCode," +
                       "lblWAPChannelOrderAllCode,lblWAPChannelAffirmOrderCode,lblWAPChannelNotAffirmOrderCode,lblWAPChannelccCode,lblWAPChannelOtherCode,lblWAPrestCode," +
                       "lblProChannelOrderAllCode,lblProChannelAffirmOrderCode,lblProChannelNotAffirmOrderCode,lblProChannelccCode,lblProChannelOtherCode,lblProrestCode," +
                       "lblGETAROOMOrderAllCode,lblGETAROOMAffirmOrderCode,lblGETAROOMNotAffirmOrderCode,lblGETAROOMChannelccCode,lblGETAROOMChannelOtherCode,lblGETAROOMrestCode," +
                       "lblHMBSTOrderAllCode,lblHMBSTAffirmOrderCode,lblHMBSTNotAffirmOrderCode,lblHMBSTChannelccCode,lblHMBSTChannelOtherCode,lblHMBSTrestCode," +
                       "lblQUNarChannelOrderAllCode,lblQUNarChannelAffirmOrderCode,lblQUNarChannelNotAffirmOrderCode,lblQUNarChannelccCode,lblQUNarChannelOtherCode,lblQUNarrestCode," +
                       "lbl11ChannelOrderAllCode,lbl11ChannelAffirmOrderCode,lbl11ChannelNotAffirmOrderCode,lbl11ChannelccCode,lbl11ChannelOtherCode,lbl11restCode," +
                       "lblMJChannelOrderAllCode,lblMJChannelAffirmOrderCode,lblMJChannelNotAffirmOrderCode,lblMJChannelccCode,lblMJChannelOtherCode,lblMJrestCode," +
                       "lblChannelOrderAllCodeCount,lblChannelAffirmOrderCodeCount,lblChannelNotAffirmOrderCodeCount,lblChannelccCodeCount,lblChannelOtherCodeCount,lblrestCodeCount," +
                       "lblLBOrderAllCode,lblLBOrderAffirmCode,lblLBOrderNotAffirmOrderCode,lblLBOrderccCode,lblLBOrderOtherCode,lblLBOrderTimeOutCode,lblLBrestCode," +
                       "lblLB2OrderAllCode,lblLB2OrderAffirmCode,lblLB2OrderNotAffirmOrderCode,lblLB2OrderccCode,lblLB2OrderOtherCode,lblLB2OrderTimeOutCode,lblLB2restCode," +
                       "lblBBOrderAllCode,lblBBOrderAffirmCode,lblBBOrderNotAffirmOrderCode,lblBB2OrderccCode,lblBBOrderOtherCode,lblBBOrderTimeOutCode,lblBBrestCode," +
                       "lblOrderAllCode,lblOrderAffirmCode,lblOrderNotAffirmCode,lblOrderccCode,lblOrderOtherCode,lblOrderTimeOutCode,lblrestCode," +
                       "lblOrderAllCodePercent,lblOrderAffirmPercentCode,lblOrderNotAffirmPercentCode,lblOrderccCountPercentCode,lblOrderOtherCodePercent,lblOrderTimeOutCodePercent,lblrestCodePercent," +
                       "lblHotelvpMapChannelOrderAllCode,lblHotelvpMapChannelAffirmOrderCode,lblHotelvpMapChannelNotAffirmOrderCode,lblHotelvpMapChannelccCode,lblHotelvpMapChannelOtherCode,lblHotelvpMaprestCode," +
                       "lblRestsChannelOrderAllCode,lblRestsChannelAffirmOrderCode,lblRestsChannelNotAffirmOrderCode,lblRestsChannelccCode,lblRestsChannelOtherCode,lblRestsrestCode" +
                       "Label40,Label41,Label42,Label43,Label44,Label7," +
"lblLBOrderAll,lblLB2OrderAll,lblBBOrderAll,lblOrderAllCount,lblOrderAllCountPercent," +
"lblLBOrderAllCode,lblLB2OrderAllCode,lblBBOrderAllCode,lblOrderAllCode,lblOrderAllCodePercent," +
"lblLBOrderAffirm,lblLB2OrderAffirm,lblBBOrderAffirm,lblOrderAffirmCount,lblOrderAffirmPercent," +
"lblLBOrderAffirmCode,lblLB2OrderAffirmCode,lblBBOrderAffirmCode,lblOrderAffirmCode,lblOrderAffirmPercentCode," +
"lblLBOrderNotAffirmOrder,lblLB2OrderNotAffirmOrder,lblBBOrderNotAffirmOrder,lblOrderNotAffirmCount,lblOrderNotAffirmPercent," +
"lblLBOrderNotAffirmOrderCode,lblLB2OrderNotAffirmOrderCode,lblBBOrderNotAffirmOrderCode,lblOrderNotAffirmCode,lblOrderNotAffirmPercentCode," +
"lblLBOrdercc,lblLB2Ordercc,lblBB2Ordercc,lblOrderccCount,lblOrderccCountPercent," +
"lblLBOrderccCode,lblLB2OrderccCode,lblBB2OrderccCode,lblOrderccCode,lblOrderccCountPercentCode," +
"lblLBOrderOther,lblLB2OrderOther,lblBBOrderOther,lblOrderOtherCount,lblOrderOtherPercent," +
"lblLBOrderOtherCode,lblLB2OrderOtherCode,lblBBOrderOtherCode,lblOrderOtherCode,lblOrderOtherCodePercent," +
"lblLBOrderTimeOut,lblLB2OrderTimeOut,lblLBBOrderTimeOut,lblOrderTimeOutCount,lblOrderTimeOutPercent," +
"lblLBOrderTimeOutCode,lblLB2OrderTimeOutCode,lblBBOrderTimeOutCode,lblOrderTimeOutCode,lblOrderTimeOutCodePercent," +
"lblLBrest,lblLB2rest,lblBBrest,lblrestCou,lblrestCountPercent," +
"lblLBrestCode,lblLB2restCode,lblBBrestCode,lblrestCode,lblrestCodePercent," +
"lblOrderAllCKIN,lblOrderAffirmCKIN,lblOrderNotAffirmCKIN,lblOrderccCKIN,lblOrderOtherCKIN,lblrestCKIN," +
"Label2,Label3,Label4,Label5,Label6,Label9," +
"lblIOSChannelOrderAll,lblAdrChannelOrderAll,lblWPChannelOrderAll,lblWAPChannelOrderAll,lblProChannelOrderAll,lblQUNarChannelOrderAll,lbl11ChannelOrderAll,lblMJChannelOrderAll,lblChannelOrderAllCount,lblChannelOrderAllPercent,lblGETAROOMOrderAll,lblW8ChannelOrderAll,lblHMBSTOrderAll," +
"lblIOSChannelOrderAllCode,lblAdrChannelOrderAllCode,lblWPChannelOrderAllCode,lblWAPChannelOrderAllCode,lblProChannelOrderAllCode,lblQUNarChannelOrderAllCode,lbl11ChannelOrderAllCode,lblMJChannelOrderAllCode,lblChannelOrderAllCodeCount,lblGETAROOMOrderAllCode,lblW8ChannelOrderAllCode,lblHMBSTOrderAllCode," +
"lblIOSChannelAffirmOrder,lblADRChannelAffirmOrder,lblWPChannelAffirmOrder,lblWAPChannelAffirmOrder,lblProChannelAffirmOrder,lblQUNarChannelAffirmOrder,lbl11ChannelAffirmOrder,lblMJChannelAffirmOrder,lblChannelAffirmOrderCount,lblChannelAffirmOrderPercent,lblGETAROOMAffirmOrder,lblW8ChannelAffirmOrder,lblHMBSTAffirmOrder," +
"lblIOSChannelAffirmOrderCode,lblADRChannelAffirmOrderCode,lblWPChannelAffirmOrderCode,lblWAPChannelAffirmOrderCode,lblProChannelAffirmOrderCode,lblQUNarChannelAffirmOrderCode,lbl11ChannelAffirmOrderCode,lblMJChannelAffirmOrderCode,lblChannelAffirmOrderCodeCount,lblGETAROOMAffirmOrderCode,lblW8ChannelAffirmOrderCode,lblHMBSTAffirmOrderCode," +
"lblIOSChannelNotAffirmOrder,lblADRChannelNotAffirmOrder,lblWPChannelNotAffirmOrder,lblWAPChannelNotAffirmOrder,lblProChannelNotAffirmOrder,lblQUNarChannelNotAffirmOrder,lbl11ChannelNotAffirmOrder,lblMJChannelNotAffirmOrder,lblChannelNotAffirmOrderCount,lblChannelNotAffirmOrderPercent,lblGETAROOMNotAffirmOrder,lblW8ChannelNotAffirmOrder,lblHMBSTNotAffirmOrder," +
"lblIOSChannelNotAffirmOrderCode,lblADRChannelNotAffirmOrderCode,lblWPChannelNotAffirmOrderCode,lblWAPChannelNotAffirmOrderCode,lblProChannelNotAffirmOrderCode,lblQUNarChannelNotAffirmOrderCode,lbl11ChannelNotAffirmOrderCode,lblMJChannelNotAffirmOrderCode,lblChannelNotAffirmOrderCodeCount,lblGETAROOMNotAffirmOrderCode,lblW8ChannelNotAffirmOrderCode,lblHMBSTNotAffirmOrderCode," +
"lblIOSChannelcc,lblADRChannelcc,lblWPChannelcc,lblWAPChannelcc,lblProChannelcc,lblQUNarChannelcc,lbl11Channelcc,lblMJChannelcc,lblChannelccCount,lblChannelccCodePercent,lblGETAROOMChannelcc,lblW8Channelcc,lblHMBSTChannelcc," +
"lblIOSChannelccCode,lblADRChannelccCode,lblWPChannelccCode,lblWAPChannelccCode,lblProChannelccCode,lblQUNarChannelccCode,lbl11ChannelccCode,lblMJChannelccCode,lblChannelccCodeCount,lblGETAROOMChannelccCode,lblW8ChannelccCode,lblHMBSTChannelccCode," +
"lblIOSChannelOther,lblADRChannelOther,lblWPChannelOther,lblWAPChannelOther,lblProChannelOther,lblQUNarChannelOther,lbl11ChannelOther,lblMJChannelOther,lblChannelOtherCount,lblChannelOtherPercent,lblGETAROOMChannelOther,lblW8ChannelOther,lblHMBSTChannelOther," +
"lblIOSChannelOtherCode,lblADRChannelOtherCode,lblWPChannelOtherCode,lblWAPChannelOtherCode,lblProChannelOtherCode,lblQUNarChannelOtherCode,lbl11ChannelOtherCode,lblMJChannelOtherCode,lblChannelOtherCodeCount,lblGETAROOMChannelOtherCode,lblW8ChannelOtherCode,lblHMBSTChannelOtherCode," +
"lblIOSrest,lblADRrest,lblWPrest,lblWAPrest,lblProrest,lblQUNarrest,lbl11rest,lblMJrest,lblrestCount,lblrestPercent,lblGETAROOMrest,lblW8rest,lblHMBSTrest," +
"lblIOSrestCode,lblADRrestCode,lblWPrestCode,lblWAPrestCode,lblProrestCode,lblQUNarrestCode,lbl11restCode,lblMJrestCode,lblrestCodeCount,lblGETAROOMrestCode,lblW8restCode,lblHMBSTrestCode," +
"lblHotelvpMapChannelOrderAll,lblHotelvpMapChannelAffirmOrder,lblHotelvpMapChannelNotAffirmOrder,lblHotelvpMapChannelcc,lblHotelvpMapChannelOther,lblHotelvpMaprest," +
"lblHotelvpMapChannelOrderAllCode,lblHotelvpMapChannelAffirmOrderCode,lblHotelvpMapChannelNotAffirmOrderCode,lblHotelvpMapChannelccCode,lblHotelvpMapChannelOtherCode,lblHotelvpMaprestCode," +
"lblRestsChannelOrderAll,lblRestsChannelAffirmOrder,lblRestsChannelNotAffirmOrder,lblRestsChannelcc,lblRestsChannelOther,lblRestsrest," +
"lblRestsChannelOrderAllCode,lblRestsChannelAffirmOrderCode,lblRestsChannelNotAffirmOrderCode,lblRestsChannelccCode,lblRestsChannelOtherCode,lblRestsrestCode";

        int iSum = int.Parse(ConfigurationManager.AppSettings["iSum"].ToString());
        string[] strLbl = str.Split(',');
        for (int i = 0; i < strLbl.Length; i++)
        {
            string a = strLbl[i].ToString();
            Label ctr = right.FindControl(a) as Label;
            if (ctr != null)
            {
                if (ctr.Text.Contains("(") && !ctr.Text.Contains("%"))
                {
                    ctr.Text = "(" + (int.Parse(ctr.Text.Replace("(", "").Replace(")", "")) * iSum).ToString() + ")";
                }
                else if (ctr.Text.Contains("%"))
                {
                    //ctr.Text = (int.Parse(ctr.Text.Replace("%", "")) * 5).ToString() + "%";
                }
                else
                {
                    ctr.Text = (int.Parse(ctr.Text) * iSum).ToString();
                }
            }
        }



        lbTodayLoginAll.Text = (int.Parse(lbTodayLoginAll.Text) * iSum).ToString("#,##0");
        lblLogIOSUR.Text = (int.Parse(lblLogIOSUR.Text) * iSum).ToString("#,##0");
        lbLogANDUR.Text = (int.Parse(lbLogANDUR.Text) * iSum).ToString("#,##0");
        lbLogWAPUR.Text = (int.Parse(lbLogWAPUR.Text) * iSum).ToString("#,##0");
        lbLogWP7.Text = (int.Parse(lbLogWP7.Text) * iSum).ToString("#,##0");
        lbLogW8.Text = (int.Parse(lbLogW8.Text) * iSum).ToString("#,##0");
        lbLogOther.Text = (int.Parse(lbLogOther.Text) * iSum).ToString("#,##0");



        lbUserAll.Text = (int.Parse(lbUserAll.Text) * iSum).ToString("#,##0");
        lbIOSUR.Text = (int.Parse(lbIOSUR.Text) * iSum).ToString("#,##0");
        lbANDUR.Text = (int.Parse(lbANDUR.Text) * iSum).ToString("#,##0");
        lbWAPUR.Text = (int.Parse(lbWAPUR.Text) * iSum).ToString("#,##0");
        lbWP7.Text = (int.Parse(lbWP7.Text) * iSum).ToString("#,##0");
        lbW8.Text = (int.Parse(lbW8.Text) * iSum).ToString("#,##0");
        lbOther.Text = (int.Parse(lbOther.Text) * iSum).ToString("#,##0");
    }
}