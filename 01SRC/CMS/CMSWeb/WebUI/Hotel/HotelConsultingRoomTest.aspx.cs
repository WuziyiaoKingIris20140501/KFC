using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Services;

public partial class WebUI_Hotel_HotelConsultingRoomTest : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //设置计划时间段
            if (DateTime.Now.Hour <= 4 && DateTime.Now.Hour >= 0)
            {
                this.planStartDate.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(6).ToShortDateString().Replace("/", "-");
                this.HidStartDateTime.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
            }
            else
            { 
                this.planStartDate.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(6).ToShortDateString().Replace("/", "-");
                this.HidStartDateTime.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
            }
        }
    }

    [WebMethod]
    public static string GetHotel(string hotelID)
    {
        try
        {
            DataTable dtResult = GetHotelDetails(hotelID);
            return ToJson(dtResult);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static DataTable GetHotelDetails(string hotelID)
    {
        string sql = "select * from t_lm_b_prop bp left join t_lm_hotel_ex he on bp.prop=he.hotel_id where prop=:PROP and he.type=1";
        OracleParameter[] parm ={
                                    new OracleParameter("PROP",OracleType.VarChar)
                                };
        if (String.IsNullOrEmpty(hotelID))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = hotelID;
        }
        DataSet ds = DbHelperOra.Query(sql, false, parm);
        return ds.Tables[0];

    }

    #region  查询操作
    [WebMethod]
    public static string GetHotelList(string hotelID, string cityID, string areaID, string SalesID, string Type, string EffDate)
    {
        try
        {
            DataTable dtResult = GetHotel(hotelID, cityID, areaID, SalesID, Type, EffDate);
            return ToJson(dtResult);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static DataTable GetHotel(string hotelID, string cityID, string areaID, string SalesID, string Type, string EffDate)
    {
        DataTable dtResult = new DataTable();
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        #region  单个查询酒店
        if (!string.IsNullOrEmpty(hotelID))
        {
            hotelInfoDBEntity.HotelID = hotelID == "" ? "" : hotelID.Substring((hotelID.IndexOf('[') + 1), (hotelID.IndexOf(']') - 1));//酒店ID
            hotelInfoDBEntity.Type = Type;//排序方式
            hotelInfoDBEntity.EffectDate = EffDate;//排序方式
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetConsultRoomHotelRoomListByProp(_hotelinfoEntity).QueryResult.Tables[0];
        }
        #endregion

        #region   城市   商圈
        if (!string.IsNullOrEmpty(cityID) || !string.IsNullOrEmpty(areaID))
        {
            hotelInfoDBEntity.City = cityID == "" ? "" : cityID.Substring((cityID.IndexOf('[') + 1), (cityID.IndexOf(']') - 1)); //"";//城市ID 
            hotelInfoDBEntity.Bussiness = areaID == "" ? "" : areaID.Substring((areaID.IndexOf('[') + 1), (areaID.IndexOf(']') - 1));//"";//商圈ID
            hotelInfoDBEntity.Type = Type;//排序方式
            hotelInfoDBEntity.EffectDate = EffDate;//排序方式

            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetConsultRoomHotelRoomListByHotel(_hotelinfoEntity).QueryResult.Tables[0];
        }
        #endregion

        #region  询房用户
        if (!string.IsNullOrEmpty(SalesID))
        {
            hotelInfoDBEntity.SalesID = SalesID == "" ? "" : SalesID.Substring((SalesID.IndexOf('[') + 1), (SalesID.IndexOf(']') - 1));//房控人员
            hotelInfoDBEntity.Type = Type;//排序方式
            hotelInfoDBEntity.EffectDate = EffDate;//排序方式
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetConsultRoomHotelRoomList(_hotelinfoEntity).QueryResult.Tables[0];//得到当天所有 有计划  的酒店 

            #region 过滤所有计划关闭 且  关闭人为销售人员
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                DataTable dtNewResult = new DataTable();
                dtNewResult.Columns.Add("PROP");
                dtNewResult.Columns.Add("CITYID");
                dtNewResult.Columns.Add("NAME_CN");
                dtNewResult.Columns.Add("LINKMAN");
                dtNewResult.Columns.Add("LINKTEL");
                dtNewResult.Columns.Add("LINKEMAIL");
                dtNewResult.Columns.Add("SALES_ACCOUNT");
                dtNewResult.Columns.Add("PROP_NAME_ZH");
                dtNewResult.Columns.Add("ISPLAN");
                dtNewResult.Columns.Add("ORDERCOUNT");
                dtNewResult.Columns.Add("EXLINKMAN");
                dtNewResult.Columns.Add("EXLINKTEL");
                dtNewResult.Columns.Add("EXREMARK");
                dtNewResult.Columns.Add("BACKPROPNAME");
                dtNewResult.Columns.Add("EXMODE");
                dtNewResult.Columns.Add("HASBEENTOHOTELS");
                dtNewResult.Columns.Add("COLOR");


                DataTable dtSales = GetSalesManagerList();//所有的销售人员

                string FilterJJHotels = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["FilterJinJiangHotels"])) ? "" : ConfigurationManager.AppSettings["FilterJinJiangHotels"].ToString().Trim();
                string hotelId = "";
                bool IsFlag = false;

                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    IsFlag = false;
                    if (hotelId == "" || hotelId != dtResult.Rows[i]["prop"].ToString())
                    {
                        #region
                        for (int j = 0; j < FilterJJHotels.Split(',').Length; j++)
                        {
                            if (!string.IsNullOrEmpty(FilterJJHotels.Split(',')[j].ToString()))
                            {
                                if (dtResult.Rows[i]["prop"].ToString() == FilterJJHotels.Split(',')[j].ToString())
                                {
                                    IsFlag = true;
                                    break;
                                }
                            }
                        }
                        #endregion
                        if (!IsFlag)
                        {
                            #region
                            hotelId = dtResult.Rows[i]["prop"].ToString();
                            DataRow[] rowsAll = dtResult.Select("prop='" + hotelId + "'");//获取当前酒店所有计划
                            DataRow[] rowsCloseAll = dtResult.Select("prop='" + hotelId + "' and status=0");//获取当前酒店所有已关闭的计划
                            if (rowsAll.Length > 0 && rowsCloseAll.Length > 0)
                            {
                                if (rowsAll.Length == rowsCloseAll.Length)//计划全部关闭  且  关闭人为销售人员
                                {
                                    int count = 0;
                                    for (int j = 0; j < rowsCloseAll.Length; j++)
                                    {
                                        DataRow[] salesRow = dtSales.Select("REVALUE_ALL like '%" + rowsCloseAll[j]["MODIFIER"].ToString() + "%'");
                                        if (salesRow.Length > 0)
                                        {
                                            count++;
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (count != rowsCloseAll.Length)
                                    {
                                        DataRow dr = dtNewResult.NewRow();
                                        dr["PROP"] = hotelId;
                                        dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                        dr["NAME_CN"] = dtResult.Rows[i]["name_cn"].ToString();
                                        dr["LINKMAN"] = dtResult.Rows[i]["linkman"].ToString();
                                        dr["LINKTEL"] = dtResult.Rows[i]["linktel"].ToString();
                                        dr["LINKEMAIL"] = dtResult.Rows[i]["linkemail"].ToString();
                                        dr["SALES_ACCOUNT"] = dtResult.Rows[i]["sales_account"].ToString();
                                        dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                        dr["isplan"] = dtResult.Rows[i]["isplan"].ToString();
                                        dr["ordercount"] = dtResult.Rows[i]["ordercount"].ToString();
                                        dr["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                                        dr["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "&nbsp;&nbsp;" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                                        dr["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();
                                        dr["BackPropName"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                        dr["EXMODE"] = dtResult.Rows[i]["EXMODE"];
                                        dr["HASBEENTOHOTELS"] = dtResult.Rows[i]["HasBeenToHotels"];
                                        dr["COLOR"] = dtResult.Rows[i]["Color"];
                                        dtNewResult.Rows.Add(dr);
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtNewResult.NewRow();
                                    dr["PROP"] = hotelId;
                                    dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                    dr["NAME_CN"] = dtResult.Rows[i]["name_cn"].ToString();
                                    dr["LINKMAN"] = dtResult.Rows[i]["linkman"].ToString();
                                    dr["LINKTEL"] = dtResult.Rows[i]["linktel"].ToString();
                                    dr["LINKEMAIL"] = dtResult.Rows[i]["linkemail"].ToString();
                                    dr["SALES_ACCOUNT"] = dtResult.Rows[i]["sales_account"].ToString();
                                    dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                    dr["isplan"] = dtResult.Rows[i]["isplan"].ToString();
                                    dr["ordercount"] = dtResult.Rows[i]["ordercount"].ToString();
                                    dr["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                                    dr["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "&nbsp;&nbsp;" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                                    dr["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();
                                    dr["BackPropName"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                    dr["EXMODE"] = dtResult.Rows[i]["EXMODE"];
                                    dr["HASBEENTOHOTELS"] = dtResult.Rows[i]["HasBeenToHotels"];
                                    dr["COLOR"] = dtResult.Rows[i]["Color"];
                                    dtNewResult.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = dtNewResult.NewRow();
                                dr["PROP"] = hotelId;
                                dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                dr["NAME_CN"] = dtResult.Rows[i]["name_cn"].ToString();
                                dr["LINKMAN"] = dtResult.Rows[i]["linkman"].ToString();
                                dr["LINKTEL"] = dtResult.Rows[i]["linktel"].ToString();
                                dr["LINKEMAIL"] = dtResult.Rows[i]["linkemail"].ToString();
                                dr["SALES_ACCOUNT"] = dtResult.Rows[i]["sales_account"].ToString();
                                dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                dr["isplan"] = dtResult.Rows[i]["isplan"].ToString();
                                dr["ordercount"] = dtResult.Rows[i]["ordercount"].ToString();
                                dr["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                                dr["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "&nbsp;&nbsp;" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                                dr["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();
                                dr["BackPropName"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                dr["EXMODE"] = dtResult.Rows[i]["EXMODE"];
                                dr["HASBEENTOHOTELS"] = dtResult.Rows[i]["HasBeenToHotels"];
                                dr["COLOR"] = dtResult.Rows[i]["Color"];
                                dtNewResult.Rows.Add(dr);
                            }
                            #endregion
                        }
                    }
                }
                for (int i = 0; i < dtNewResult.Rows.Count; i++)
                {
                    if (dtNewResult.Rows[i]["EXMODE"].ToString() == "3")
                    {
                        dtNewResult.Rows.Remove(dtNewResult.Rows[i]);
                    }
                }
                dtNewResult.DefaultView.Sort = Type;
                dtResult = dtNewResult.DefaultView.ToTable();
            }
            #endregion
        }
        #endregion
        DataTable dtConsultResult = GetConsultRoomHistoryList().Tables[0];//获取已询房酒店列表 
        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            for (int j = 0; j < dtConsultResult.Rows.Count; j++)
            {
                if (dtResult.Rows[i]["prop"].ToString().Trim() == dtConsultResult.Rows[j]["HotelID"].ToString().Trim())
                {
                    if (DateTime.Parse(dtConsultResult.Rows[j]["PlanDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        if (DateTime.Parse(dtConsultResult.Rows[j]["CreateTime"].ToString()).Hour >= 18)
                        {
                            dtResult.Rows[i]["Color"] = "#FF6666";
                            //gridHotelList.Rows[i].Cells[7].Text = "#CD5C5C";
                            //gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                            //((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[i].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#CD5C5C");
                        }
                        else
                        {
                            dtResult.Rows[i]["Color"] = "#80c0a0";
                            //gridHotelList.Rows[i].Cells[7].Text = "#70A88C";
                            //gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#80c0a0");
                            //((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[i].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                        }
                        dtResult.Rows[i]["HasBeenToHotels"] = "1";
                    }
                }
            }
        }
        return dtResult;
    }

    #endregion

    #region  单个酒店计划信息
    #region 计划信息
    [WebMethod]
    public static string GetHotelPlansByLMBAR(string hotelID, string startDate, string endDate)
    {
        DataTable dtPlan = GetBindLmbarPlanList(startDate, endDate, hotelID, "LMBAR").Tables[0];//LMBAR计划   

        return ToJson(dtPlan);
    }

    [WebMethod]
    public static string GetHotelPlansByLMBAR2(string hotelID, string startDate, string endDate)
    {
        DataTable dtPlan = GetBindLmbarPlanList(startDate, endDate, hotelID, "LMBAR2").Tables[0];//LMBAR计划   

        return ToJson(dtPlan);
    }
    #endregion

    #region 房型信息
    [WebMethod]
    public static string GetHotelRoomsByLMBAR(string hotelID)
    {
        DataTable dtRoomList = BindBalanceRoomList(hotelID, "LMBAR").Tables[0];
        return ToJson(dtRoomList);
    }

    [WebMethod]
    public static string GetHotelRoomsByLMBAR2(string hotelID)
    {
        DataTable dtRoomList = BindBalanceRoomList(hotelID, "LMBAR2").Tables[0];
        return ToJson(dtRoomList);
    }
    #endregion

    #endregion

    #region   时间段
    [WebMethod]
    public static string GetDTime(string startDate, string endDate)
    {
        DataTable TimeList = new DataTable();
        TimeList.Columns.Add(new DataColumn("time"));
        TimeList.Columns.Add(new DataColumn("timeMD"));
        TimeList.Columns.Add(new DataColumn("IsWeek"));
        TimeSpan ts = DateTime.Parse(endDate) - DateTime.Parse(startDate);
        int days = ts.Days;
        for (int i = 0; i <= days; i++)
        {
            DataRow drRow = TimeList.NewRow();
            drRow["time"] = DateTime.Parse(startDate).AddDays(i).ToShortDateString();
            drRow["timeMD"] = DateTime.Parse(startDate).AddDays(i).Month.ToString() + "-" + DateTime.Parse(startDate).AddDays(i).Day.ToString();

            if (DateTime.Parse(startDate).AddDays(i).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(startDate).AddDays(i).DayOfWeek.ToString() == "Sunday")
            {
                drRow["IsWeek"] = "true";
            }
            else
            {
                drRow["IsWeek"] = "false";
            }
            TimeList.Rows.Add(drRow);
        }

        return ToJson(TimeList);
    }
    #endregion

    #region  单个酒店计划信息更新
    [WebMethod]
    public static string RenewPlanBySingleHotel(string hotelID, string CityID, string HotelNM, string PriceCode, string TwoPrice, string Status, string RoomCount, string IsReserve, string RoomName, string RoomCode,
        string StartDTime, string EndDTime, string Remark)
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        CommonEntity _commonEntity = new CommonEntity();
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _commonEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _commonEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        #region
        //城市ID
        appcontentDBEntity.CityID = CityID;
        //酒店ID 
        appcontentDBEntity.HotelID = hotelID;
        //酒店名称
        appcontentDBEntity.HotelNM = HotelNM;
        //价格代码
        appcontentDBEntity.PriceCode = PriceCode;
        //价格
        appcontentDBEntity.TwoPrice = TwoPrice;
        //状态     开启 关闭  
        appcontentDBEntity.RoomStatus = Status;
        appcontentDBEntity.PlanStatus = Status;
        //房量
        appcontentDBEntity.RoomCount = RoomCount;
        //是否是保留房
        appcontentDBEntity.IsReserve = IsReserve;
        //房型名称
        appcontentDBEntity.RoomName = RoomName;
        //房型Code
        appcontentDBEntity.RoomCode = RoomCode;
        //批量更新日期   开始  结束 
        appcontentDBEntity.StartDTime = StartDTime;
        appcontentDBEntity.EndDTime = EndDTime;
        //备注
        appcontentDBEntity.Remark = Remark;
        //操作人  
        appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;
        appcontentDBEntity.UpdateUser = UserSession.Current.UserAccount;

        appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";

        #endregion

        #region

        int DateDiff = calculateDateDiff(appcontentDBEntity.StartDTime, appcontentDBEntity.EndDTime);

        for (int j = 0; j <= DateDiff; j++)
        {
            appcontentDBEntity.PlanTime = DateTime.Parse(appcontentDBEntity.StartDTime).AddDays(j).ToShortDateString();
            _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
            CommonBP.InsertConsultRoomHistory(_appcontentEntity);
            _appcontentEntity.APPContentDBEntity.Clear();
        }

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        _appcontentEntity = HotelInfoBP.RenewPlanFullRoomByUpdatePlan(_appcontentEntity);
        #endregion

        return "{\"d\":{\"message\":\"" + _appcontentEntity.ErrorMSG + "\",\"code\":" + _appcontentEntity.Result + "}}";
    }
    #endregion

    #region   酒店计划批量更新
    [WebMethod]
    public static string RenewPlanMarkRoom(string RoomStatus, string hotelID, string hotelNM, string cityID, string remark, string StartDate, string EndDate, string dateSE, string Lmbar2RoomCode, string LmbarRoomCode)
    {
        string strResult = "{\"message\":\"\",\"code\":\"\"}";
        if (RoomStatus == "FullRoom")
        {
            strResult = btnPlanFullRoom(hotelID, hotelNM, cityID, remark, StartDate, EndDate, dateSE, Lmbar2RoomCode, LmbarRoomCode);//满房  FullRoom
        }
        else if (RoomStatus == "CloseRoom")
        {
            strResult = btnPlanCloseRoom(hotelID, hotelNM, cityID, remark, "false", true, StartDate, EndDate, dateSE, Lmbar2RoomCode, LmbarRoomCode);
            //btnPlanCloseRoom(remark, "false", true);//关房  CloseRoom  false(status  关房 )
        }
        else if (RoomStatus == "OpenRoom")
        {
            //btnPlanCloseRoom(remark, "true", true);//开房  OpenRoom   true(status  关房 )
            strResult = btnPlanCloseRoom(hotelID, hotelNM, cityID, remark, "true", true, StartDate, EndDate, dateSE, Lmbar2RoomCode, LmbarRoomCode);
        }
        else
        {
            //btnPlanCloseRoom(remark, "", false);//ExecuteRoom   批量执行   （不做操作  只记录当天已查房）
            strResult = btnPlanCloseRoom(hotelID, hotelNM, cityID, remark, "", false, StartDate, EndDate, dateSE, Lmbar2RoomCode, LmbarRoomCode);
        }
        return strResult;
    }


    /// <summary>
    /// 计划关房（批量操作 关闭计划）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static string btnPlanCloseRoom(string hotelID, string hotelNM, string cityID, string remark, string status, bool isRenew, string StartDate, string EndDate, string dateSE, string Lmbar2RoomCode, string LmbarRoomCode)
    {
        try
        {
            #region
            APPContentEntity _appcontentEntity = new APPContentEntity();
            _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            CommonEntity _commonEntity = new CommonEntity();
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _commonEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            _commonEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _commonEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
            APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
            #endregion
            bool IsFlag = false;
            if (!string.IsNullOrEmpty(dateSE))
            {
                DataTable dtPlanLMBAR2 = GetBindLmbarPlanList(StartDate, EndDate, hotelID, "LMBAR2").Tables[0];//LMBAR2计划
                DataTable dtPlanLMBAR = GetBindLmbarPlanList(StartDate, EndDate, hotelID, "LMBAR").Tables[0];//LMBAR计划

                string[] datas = dateSE.Split(',');
                for (int i = 0; i < datas.Length; i++)
                {
                    if (!string.IsNullOrEmpty(datas[i].ToString()))
                    {
                        //if (DateTime.Parse(datas[i].ToString()).ToShortDateString() == System.DateTime.Now.ToShortDateString())
                        //{
                        //    IsFlag = true;
                        //} 
                        string effDate = datas[i].ToString().Replace("/", "-");
                        #region
                        for (int l = 0; l < Lmbar2RoomCode.Split('|').Length; l++)
                        {
                            DataRow[] rowsLmbar2 = dtPlanLMBAR2.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + Lmbar2RoomCode.Split('|')[l].ToString() + "'");
                            for (int j = 0; j < rowsLmbar2.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(rowsLmbar2[j]["RoomNum"].ToString()) && rowsLmbar2[j]["RoomNum"].ToString().ToLower() != "null")
                                {
                                    //城市ID
                                    appcontentDBEntity.CityID = cityID;
                                    //酒店ID 
                                    appcontentDBEntity.HotelID = hotelID;
                                    //酒店名称
                                    appcontentDBEntity.HotelNM = hotelNM;
                                    //PlanDate
                                    appcontentDBEntity.PlanTime = DateTime.Parse(effDate).ToShortDateString();
                                    //价格代码
                                    appcontentDBEntity.PriceCode = rowsLmbar2[j]["RATECODE"].ToString();
                                    //价格
                                    appcontentDBEntity.TwoPrice = rowsLmbar2[j]["TWOPRICE"].ToString();
                                    //状态     开启 关闭  
                                    appcontentDBEntity.PlanStatus = status == "" ? rowsLmbar2[j]["STATUS"].ToString() : status;
                                    appcontentDBEntity.RoomCount = rowsLmbar2[j]["ROOMNUM"].ToString();
                                    appcontentDBEntity.IsReserve = rowsLmbar2[j]["ISRESERVE"].ToString();
                                    //房型名称
                                    appcontentDBEntity.RoomName = rowsLmbar2[j]["ROOMTYPENAME"].ToString();
                                    //房型Code
                                    appcontentDBEntity.RoomCode = rowsLmbar2[j]["ROOMTYPECODE"].ToString();

                                    appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
                                    //备注
                                    appcontentDBEntity.Remark = remark;
                                    //操作人 
                                    appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;

                                    _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
                                    CommonBP.InsertConsultRoomHistory(_appcontentEntity);
                                    _appcontentEntity.APPContentDBEntity.Clear();
                                }
                            }
                        }
                        #endregion
                        #region
                        for (int l = 0; l < LmbarRoomCode.Split('|').Length; l++)
                        {
                            DataRow[] rowsLmbar = dtPlanLMBAR.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + LmbarRoomCode.Split('|')[l].ToString() + "'");
                            for (int j = 0; j < rowsLmbar.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(rowsLmbar[j]["ROOMNUM"].ToString()) && rowsLmbar[j]["RoomNum"].ToString().ToLower() != "null")
                                {
                                    //城市ID
                                    appcontentDBEntity.CityID = cityID;
                                    //酒店ID 
                                    appcontentDBEntity.HotelID = hotelID;
                                    //酒店名称
                                    appcontentDBEntity.HotelNM = hotelNM;
                                    //PlanDate
                                    appcontentDBEntity.PlanTime = DateTime.Parse(effDate).ToShortDateString();
                                    //价格代码
                                    appcontentDBEntity.PriceCode = rowsLmbar[j]["RATECODE"].ToString();
                                    //价格
                                    appcontentDBEntity.TwoPrice = rowsLmbar[j]["TWOPRICE"].ToString();
                                    //状态     开启 关闭  
                                    appcontentDBEntity.PlanStatus = status == "" ? rowsLmbar[j]["STATUS"].ToString() : status;
                                    appcontentDBEntity.RoomCount = rowsLmbar[j]["ROOMNUM"].ToString();
                                    appcontentDBEntity.IsReserve = rowsLmbar[j]["ISRESERVE"].ToString();
                                    //房型名称
                                    appcontentDBEntity.RoomName = rowsLmbar[j]["ROOMTYPENAME"].ToString();
                                    //房型Code
                                    appcontentDBEntity.RoomCode = rowsLmbar[j]["ROOMTYPECODE"].ToString();

                                    appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
                                    //备注
                                    appcontentDBEntity.Remark = remark;
                                    //操作人 
                                    appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;
                                    _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
                                    CommonBP.InsertConsultRoomHistory(_appcontentEntity);
                                    _appcontentEntity.APPContentDBEntity.Clear();
                                }
                            }
                        }
                        #endregion
                        if (isRenew)
                        {
                            appcontentDBEntity.HotelID = hotelID;
                            appcontentDBEntity.StartDTime = effDate;
                            appcontentDBEntity.EndDTime = effDate;
                            appcontentDBEntity.Lmbar2RoomCode = Lmbar2RoomCode.Replace("|", ",");
                            appcontentDBEntity.LmbarRoomCode = LmbarRoomCode.Replace("|", ",");
                            appcontentDBEntity.TypeID = status == "true" ? "3" : "2";// "2";//type:1 满房、2 关房、3 开房
                            appcontentDBEntity.UpdateUser = UserSession.Current.UserAccount;
                            _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

                            _appcontentEntity = HotelInfoBP.BatchUpdatePlan(_appcontentEntity);

                            _appcontentEntity.APPContentDBEntity.Clear();
                        }
                    }
                }
            }
            return "{\"message\":\"success\",\"code\":200}";
        }
        catch (Exception ex)
        {
            return "{\"message\":\"fail\",\"code\":-1}";
        }
    }

    /// <summary>
    /// 标记满房（批量操作满房）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static string btnPlanFullRoom(string hotelID, string hotelNM, string cityID, string remark, string StartDate, string EndDate, string dateSE, string Lmbar2RoomCode, string LmbarRoomCode)
    {
        try
        {
            #region
            APPContentEntity _appcontentEntity = new APPContentEntity();
            _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            CommonEntity _commonEntity = new CommonEntity();
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
            _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _commonEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            _commonEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _commonEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            CommonDBEntity commonDBEntity = new CommonDBEntity();

            _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
            APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
            #endregion
            bool IsFlag = false;
            if (!string.IsNullOrEmpty(dateSE))
            {
                DataTable dtPlanLMBAR2 = GetBindLmbarPlanList(StartDate, EndDate, hotelID, "LMBAR2").Tables[0];//LMBAR2计划
                DataTable dtPlanLMBAR = GetBindLmbarPlanList(StartDate, EndDate, hotelID, "LMBAR").Tables[0];//LMBAR计划

                string[] datas = dateSE.Split(',');

                for (int i = 0; i < datas.Length; i++)
                {
                    if (!string.IsNullOrEmpty(datas[i].ToString()))
                    {
                        //if (DateTime.Parse(datas[i].ToString()) == System.DateTime.Now)
                        //{
                        //    IsFlag = true;
                        //}
                        string effDate = datas[i].ToString().Replace("/", "-");
                        #region
                        for (int l = 0; l < Lmbar2RoomCode.Split('|').Length; l++)
                        {
                            DataRow[] rowsLmbar2 = dtPlanLMBAR2.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + Lmbar2RoomCode.Split('|')[l].ToString() + "'");
                            for (int j = 0; j < rowsLmbar2.Length; j++)
                            {
                                //城市ID
                                appcontentDBEntity.CityID = cityID;
                                //酒店ID 
                                appcontentDBEntity.HotelID = hotelID;
                                //酒店名称
                                appcontentDBEntity.HotelNM = hotelNM;
                                //PlanDate
                                appcontentDBEntity.PlanTime = DateTime.Parse(effDate).ToShortDateString();
                                //价格代码
                                appcontentDBEntity.PriceCode = rowsLmbar2[j]["RATECODE"].ToString();
                                //价格
                                appcontentDBEntity.TwoPrice = rowsLmbar2[j]["TWOPRICE"].ToString();
                                //状态     开启 关闭  
                                appcontentDBEntity.PlanStatus = rowsLmbar2[j]["STATUS"].ToString();
                                appcontentDBEntity.RoomCount = rowsLmbar2[j]["ROOMNUM"].ToString();
                                appcontentDBEntity.IsReserve = rowsLmbar2[j]["ISRESERVE"].ToString();
                                //房型名称
                                appcontentDBEntity.RoomName = rowsLmbar2[j]["ROOMTYPENAME"].ToString();
                                //房型Code
                                appcontentDBEntity.RoomCode = rowsLmbar2[j]["ROOMTYPECODE"].ToString();

                                appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
                                //备注
                                appcontentDBEntity.Remark = remark;
                                //操作人 
                                appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;

                                _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
                                CommonBP.InsertConsultRoomHistory(_appcontentEntity);
                                _appcontentEntity.APPContentDBEntity.Clear();
                            }
                        }
                        #endregion
                        #region
                        for (int l = 0; l < LmbarRoomCode.Split('|').Length; l++)
                        {
                            DataRow[] rowsLmbar = dtPlanLMBAR.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + LmbarRoomCode.Split('|')[l].ToString() + "'");
                            for (int j = 0; j < rowsLmbar.Length; j++)
                            {
                                //城市ID
                                appcontentDBEntity.CityID = cityID;
                                //酒店ID 
                                appcontentDBEntity.HotelID = hotelID;
                                //酒店名称
                                appcontentDBEntity.HotelNM = hotelNM;
                                //PlanDate
                                appcontentDBEntity.PlanTime = DateTime.Parse(effDate).ToShortDateString();
                                //价格代码
                                appcontentDBEntity.PriceCode = rowsLmbar[j]["RATECODE"].ToString();
                                //价格
                                appcontentDBEntity.TwoPrice = rowsLmbar[j]["TWOPRICE"].ToString();
                                //状态     开启 关闭  
                                appcontentDBEntity.PlanStatus = rowsLmbar[j]["STATUS"].ToString();
                                appcontentDBEntity.RoomCount = rowsLmbar[j]["ROOMNUM"].ToString();
                                appcontentDBEntity.IsReserve = rowsLmbar[j]["ISRESERVE"].ToString();
                                //房型名称
                                appcontentDBEntity.RoomName = rowsLmbar[j]["ROOMTYPENAME"].ToString();
                                //房型Code
                                appcontentDBEntity.RoomCode = rowsLmbar[j]["ROOMTYPECODE"].ToString();

                                appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
                                //备注
                                appcontentDBEntity.Remark = remark;
                                //操作人 
                                appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;
                                _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
                                CommonBP.InsertConsultRoomHistory(_appcontentEntity);
                                _appcontentEntity.APPContentDBEntity.Clear();
                            }
                        }
                        #endregion

                        appcontentDBEntity.HotelID = hotelID;
                        appcontentDBEntity.StartDTime = effDate;
                        appcontentDBEntity.EndDTime = effDate;
                        appcontentDBEntity.Lmbar2RoomCode = Lmbar2RoomCode.Replace("|", ",");
                        appcontentDBEntity.LmbarRoomCode = Lmbar2RoomCode.Replace("|", ",");
                        appcontentDBEntity.TypeID = "1";//type:1 满房、2 关房、3 开房
                        appcontentDBEntity.UpdateUser = UserSession.Current.UserAccount;
                        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

                        _appcontentEntity = HotelInfoBP.BatchUpdatePlan(_appcontentEntity);
                        _appcontentEntity.APPContentDBEntity.Clear();
                    }
                }
            }
            return "{\"message\":\"success\",\"code\":200}";
        }
        catch (Exception ex)
        {
            return "{\"message\":\"fail\",\"code\":-1}";
        }
    }
    #endregion

    #region AJAX异步加载操作历史以及DataTable 转换 JSON
    /// <summary>
    /// 获取当前计划的HistoryRemark
    /// </summary>
    /// <param name="strHotelID"></param>
    [WebMethod]
    public static string GetHistoryRemarkByJson(string CityID, string HotelID, string PriceCode, string RoomCode, string PlanDTime)
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.CityID = CityID;
        appcontentDBEntity.HotelID = HotelID;
        appcontentDBEntity.PriceCode = PriceCode;
        appcontentDBEntity.RoomCode = RoomCode;
        appcontentDBEntity.PlanDTime = DateTime.Parse(PlanDTime).ToShortDateString();

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        string json = "";
        if (!CityID.Equals("undefined"))
        {
            DataSet dsResult = HotelInfoBP.GetConsultRoomHistoryList(_appcontentEntity).QueryResult;
            try
            {
                if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    json = ToJson(dsResult.Tables[0]);
                }
                else
                {
                    json = "{\"d\":\"[]\"}";
                }
            }
            catch (Exception ex)
            {
                json = "{\"d\":\"[]\"}";
            }
        }
        return json;
    }

    public static string ToJson(DataTable dt)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("[");
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
        }
        jsonString.Append("]");
        return jsonString.ToString();
    }

    private static string StringFormat(string str, Type type)
    {
        if (type == typeof(string))
        {
            str = String2Json(str);
            str = "\"" + str + "\"";
        }
        else if (type == typeof(DateTime))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(bool))
        {
            str = str.ToLower();
        }
        else if (type != typeof(string) && string.IsNullOrEmpty(str))
        {
            str = "\"" + str + "\"";
        }
        return str;
    }

    private static string String2Json(String s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s.ToCharArray()[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\""); break;
                case '\\':
                    sb.Append("\\\\"); break;
                case '/':
                    sb.Append("\\/"); break;
                case '\b':
                    sb.Append("\\b"); break;
                case '\f':
                    sb.Append("\\f"); break;
                case '\n':
                    sb.Append("\\n"); break;
                case '\r':
                    sb.Append("\\r"); break;
                case '\t':
                    sb.Append("\\t"); break;
                default:
                    sb.Append(c); break;
            }
        }
        return sb.ToString();
    }

    #endregion

    #region   页面加载  数据初始化   公共方法
    /// <summary>
    /// 计算日期差 
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    private static int calculateDateDiff(string startDate, string endDate)
    {
        TimeSpan start = new TimeSpan(DateTime.Parse(startDate).Ticks);
        TimeSpan end = new TimeSpan(DateTime.Parse(endDate).Ticks);
        TimeSpan ts = start.Subtract(end).Duration();
        return int.Parse(ts.Days.ToString());
    }

    /// <summary>
    /// 页面加载  绑定RadioButtonList
    /// </summary>
    private void bindBookStatusList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("TEXT");
        dt.Columns.Add("VALUE");
        DataRow drRow = dt.NewRow();
        drRow["TEXT"] = "不限制";
        drRow["VALUE"] = "";
        dt.Rows.Add(drRow);
        drRow = dt.NewRow();
        drRow["TEXT"] = "14点";
        drRow["VALUE"] = "111110000000001111111111";
        dt.Rows.Add(drRow);
        drRow = dt.NewRow();
        drRow["TEXT"] = "18点";
        drRow["VALUE"] = "111110000000000000111111";
        dt.Rows.Add(drRow);
        //radioListBookStatus.DataSource = dt;
        //radioListBookStatus.DataTextField = "TEXT";
        //radioListBookStatus.DataValueField = "VALUE";
        //radioListBookStatus.DataBind();
        //radioListBookStatus.SelectedIndex = 0;
    }

    /// <summary>
    /// 自动拼取时间段  --  业务逻辑 拼装
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    private static DataTable GetDate(string startDate, string endDate)
    {
        DataTable TimeList = new DataTable();
        TimeList.Columns.Add(new DataColumn("time"));
        TimeList.Columns.Add(new DataColumn("timeMD"));
        TimeList.Columns.Add(new DataColumn("IsWeek"));
        TimeSpan ts = DateTime.Parse(endDate) - DateTime.Parse(startDate);
        int days = ts.Days;
        for (int i = 0; i <= days; i++)
        {
            DataRow drRow = TimeList.NewRow();
            drRow["time"] = DateTime.Parse(startDate).AddDays(i).ToShortDateString();
            drRow["timeMD"] = DateTime.Parse(startDate).AddDays(i).Month.ToString() + "-" + DateTime.Parse(startDate).AddDays(i).Day.ToString();

            if (DateTime.Parse(startDate).AddDays(i).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(startDate).AddDays(i).DayOfWeek.ToString() == "Sunday")
            {
                drRow["IsWeek"] = "true";
            }
            else
            {
                drRow["IsWeek"] = "false";
            }
            TimeList.Rows.Add(drRow);
        }
        return TimeList;
    }

    /// <summary>
    /// 根据时间段  HotelID 取计划  --  接口 
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="strHotelID"></param>
    /// <returns></returns>
    private static DataSet GetBindLmbarPlanList(string startDate, string endDate, string strHotelID, string priceCode)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        hotelInfoDBEntity.SalesStartDT = startDate;
        hotelInfoDBEntity.SalesEndDT = endDate;
        hotelInfoDBEntity.PriceCode = priceCode;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

        DataSet dsResult = HotelInfoBP.GetPlanListByIndiscriminatelyByRateCode(_hotelinfoEntity).QueryResult;

        return dsResult;
    }

    /// <summary>
    /// 生成房型列表 -- Oracle
    /// </summary>
    /// <param name="strHotelID"></param>
    private static DataSet BindBalanceRoomList(string strHotelID, string priceCode)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelID = strHotelID;//酒店ID
        hotelInfoDBEntity.PriceCode = priceCode;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetBalanceRoomListByHotelAndPriceCode(_hotelinfoEntity).QueryResult;
        return dsResult;
    }

    /// <summary>
    /// 获取所有的销售人员--SQL
    /// </summary>
    private static DataTable GetSalesManagerList()
    {
        WebAutoCompleteBP webBP = new WebAutoCompleteBP();
        DataTable dtSales = webBP.GetWebCompleteList("sales", "", "").Tables[0];

        return dtSales;
    }

    /// <summary>
    /// 获取当天已询房的酒店列表--SQL
    /// </summary>
    /// <param name="HotelID"></param>
    /// <returns></returns>
    public static DataSet GetConsultRoomHistoryList()
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

    /// <summary>
    /// 从HotelEx 获取询房信息  --- Oracle
    /// </summary>
    /// <returns></returns>
    public static DataRow GetHotelExInfo(string hotelID)
    {
        HotelInfoEXEntity _hotelinfoEXEntity = new HotelInfoEXEntity();

        _hotelinfoEXEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEXEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEXEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEXEntity.LogMessages.IpAddress = UserSession.Current.UserIP;


        _hotelinfoEXEntity.HotelInfoEXDBEntity = new List<HotelInfoEXDBEntity>();
        HotelInfoEXDBEntity hotelInfoEXDBEntity = new HotelInfoEXDBEntity();

        hotelInfoEXDBEntity.HotelID = hotelID;//酒店ID
        _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);

        DataSet dsResult = HotelInfoEXBP.SelectHotelInfoEX(_hotelinfoEXEntity).QueryResult;


        if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
        {
            DataRow[] rows = dsResult.Tables[0].Select("type='" + 1 + "'");
            if (rows.Length > 0)
            {
                return rows[0];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region   修改计划EX信息
    [WebMethod]
    public static string EditEXInfoDetails(string hotelID, string linkman, string linktel, string remark)
    {
        try
        {
            DataRow rows = GetHotelExInfo(hotelID);

            HotelInfoEXEntity _hotelinfoEXEntity = new HotelInfoEXEntity();

            _hotelinfoEXEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _hotelinfoEXEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _hotelinfoEXEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _hotelinfoEXEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

            _hotelinfoEXEntity.HotelInfoEXDBEntity = new List<HotelInfoEXDBEntity>();
            HotelInfoEXDBEntity hotelInfoEXDBEntity = new HotelInfoEXDBEntity();

            StringBuilder QueryRoomLinkMan = new StringBuilder();
            for (int i = 0; i <= 23; i++)
            {
                QueryRoomLinkMan.Append(linkman.TrimStart('(').TrimEnd(')') + "|"); //查房联系人
            }

            StringBuilder QueryRoomLinkTel = new StringBuilder();
            for (int i = 0; i <= 23; i++)
            {
                QueryRoomLinkTel.Append(linktel.TrimStart('(').TrimEnd(')') + "|");//查房联系电话
            }

            if (rows != null)
            {
                #region
                hotelInfoEXDBEntity.HotelID = hotelID;//酒店ID
                hotelInfoEXDBEntity.Type = "1";
                hotelInfoEXDBEntity.LinkMan = QueryRoomLinkMan.ToString().TrimEnd('|');
                hotelInfoEXDBEntity.LinkTel = QueryRoomLinkTel.ToString().TrimEnd('|');
                hotelInfoEXDBEntity.LinkFax = rows["LinkFax"].ToString();
                hotelInfoEXDBEntity.Remark = remark;
                hotelInfoEXDBEntity.ExTime = rows["EX_Time"].ToString();
                hotelInfoEXDBEntity.ExMode = rows["EX_Mode"].ToString();
                hotelInfoEXDBEntity.Status = "1";
                hotelInfoEXDBEntity.CreateUser = UserSession.Current.UserAccount;
                hotelInfoEXDBEntity.UpdateUser = UserSession.Current.UserAccount;
                _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);
                HotelInfoEXBP.UpdateHotelInfoEXByConsultRoom(_hotelinfoEXEntity);
                HotelInfoEXBP.InsertHotelEXHistory(_hotelinfoEXEntity);
                #endregion
            }
            else
            {
                #region
                hotelInfoEXDBEntity.HotelID = hotelID;
                hotelInfoEXDBEntity.Type = "1";
                hotelInfoEXDBEntity.LinkMan = QueryRoomLinkMan.ToString().TrimEnd('|');
                hotelInfoEXDBEntity.LinkTel = QueryRoomLinkTel.ToString().TrimEnd('|');
                hotelInfoEXDBEntity.LinkFax = "";
                hotelInfoEXDBEntity.Remark = remark;
                hotelInfoEXDBEntity.ExTime = "111110000000000000111111";
                hotelInfoEXDBEntity.ExMode = "1";
                hotelInfoEXDBEntity.Status = "1";
                hotelInfoEXDBEntity.CreateUser = UserSession.Current.UserAccount;
                hotelInfoEXDBEntity.UpdateUser = UserSession.Current.UserAccount;
                _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);
                HotelInfoEXBP.InsertHotelInfoEX(_hotelinfoEXEntity);
                HotelInfoEXBP.InsertHotelEXHistory(_hotelinfoEXEntity);
                #endregion
            }

            return "{\"message\":\"success\",\"code\":200}";
        }
        catch (Exception ex)
        {
            return "{\"message\":\"fail\",\"code\":-1}";
        }

    }
    #endregion

    #region   LM联系人
    /// <summary>
    /// 酒店信息-销售联系人
    /// </summary>
    /// <param name="strHotelID"></param>
    [WebMethod]
    public static string GetBasicSalesInfo(string hotelID)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hotelID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

        DataSet dsResult = HotelInfoBP.GetSalesManager(_hotelinfoEntity).QueryResult;
        string result = "";
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            result = "LM酒店负责人:" + dsResult.Tables[0].Rows[0]["DISPNM"].ToString().Replace("null", "") + "     电话:" + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Replace("null", "");
        }
        else
        {
            result = "LM酒店负责人:    电话:      ";
        }
        return "{\"message\":\"" + result + "\",\"code\":200}";
    }
    #endregion
}