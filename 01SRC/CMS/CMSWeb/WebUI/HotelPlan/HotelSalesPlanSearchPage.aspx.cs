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

public partial class WebUI_HotelPlan_HotelSalesPlanSearchPage : System.Web.UI.Page
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
            }
            else
            {
                this.planStartDate.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(6).ToShortDateString().Replace("/", "-");

            }
        }
    }

    #region  查询操作
    [WebMethod]
    public static string GetHotelList(string hotelID, string cityID, string areaID, string SalesID)
    {
        try
        {
            DataTable dtResult = GetHotel(hotelID, cityID, areaID, SalesID);
            return ToJson(dtResult);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static DataTable GetHotel(string hotelID, string cityID, string areaID, string SalesID)
    {
        DataTable dtResult = new DataTable();
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelID = hotelID == "" ? "" : hotelID.Substring((hotelID.IndexOf('[') + 1), (hotelID.IndexOf(']') - 1));//酒店ID
        hotelInfoDBEntity.City = cityID == "" ? "" : cityID.Substring((cityID.IndexOf('[') + 1), (cityID.IndexOf(']') - 1)); //"";//城市ID 
        hotelInfoDBEntity.Bussiness = areaID == "" ? "" : areaID.Substring((areaID.IndexOf('[') + 1), (areaID.IndexOf(']') - 1));//"";//商圈ID
        hotelInfoDBEntity.SalesID = SalesID == "" ? "" : SalesID.Substring((SalesID.IndexOf('[') + 1), (SalesID.IndexOf(']') - 1));//"";//销售

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        dtResult = HotelPlanInfoBP.GetHotelList(_hotelinfoEntity).QueryResult.Tables[0];

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


    #region
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

    #region DataTable 转换 JSON
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

}