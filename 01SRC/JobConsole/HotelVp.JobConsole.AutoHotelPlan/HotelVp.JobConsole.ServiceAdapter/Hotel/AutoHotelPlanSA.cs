using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;
using HotelVp.JobConsole.Entity;
using HotelVp.JobConsole.DataAccess;

namespace HotelVp.JobConsole.ServiceAdapter
{
    public abstract class AutoHotelPlanSA
    {
        public static AutoHotelPlanEntity ApplySalesPlan(AutoHotelPlanEntity autohotelplanEntity)
        {
            AutoHotelPlanDBEntity dbParm = (autohotelplanEntity.AutoHotelPlanDBEntity.Count > 0) ? autohotelplanEntity.AutoHotelPlanDBEntity[0] : new AutoHotelPlanDBEntity();
            Hashtable alRoom = QueryFullRoomList(autohotelplanEntity);
            string HotelID = dbParm.HotelID;//dbParm.HotelID.Substring((dbParm.HotelID.IndexOf('[') + 1), (dbParm.HotelID.IndexOf(']') - 1));
            string RoomCode = "";
            if (!"0".Equals(dbParm.TypeID) && !alRoom.ContainsKey(dbParm.RoomCode))
            {
                autohotelplanEntity.ErrorMSG = "保存失败！该酒店无房型销售 请确认！酒店ID：" + dbParm.HotelID + "  房型：[" + dbParm.RoomCode + "]" + dbParm.RoomName;
                autohotelplanEntity.Result = 2;
                return autohotelplanEntity;
            }

            RoomCode = RoomCode + "{\"roomTypeName\":\"" + dbParm.RoomName + "\"," + "\"roomTypeCode\":\"" + dbParm.RoomCode + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}";
            string DataString = "";
            DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime.Replace("/", "-") + "\"," + "\"endDate\":\"" + dbParm.EndDTime.Replace("/", "-") + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"guaid\":\"" + dbParm.Note1 + "\"," + "\"cxlid\":\"" + dbParm.Note2 + "\"," + "\"offsetunit\":\"" + dbParm.Offsetunit + "\"," + "\"effectHour\":\"" + dbParm.EffHour + "\",";

            DataString = (String.IsNullOrEmpty(dbParm.TwoPrice)) ? DataString : DataString + "\"twoPrice\":" + ConverDouble(dbParm.TwoPrice) + ",";
            DataString = ("true".Equals(dbParm.RoomStatus) && dbParm.RoomCount.Length > 0) ? DataString + "\"roomNum\":" + dbParm.RoomCount + "," : DataString;
            DataString = (String.IsNullOrEmpty(dbParm.Offsetval)) ? DataString : DataString + "\"offsetval\":" + ConverDouble(dbParm.Offsetval) + ",";
            DataString = (String.IsNullOrEmpty(dbParm.BreakfastNum)) ? DataString : DataString + "\"breakfastNum\":" + dbParm.BreakfastNum + ",";
            DataString = (String.IsNullOrEmpty(dbParm.BreakPrice)) ? DataString : DataString + "\"eachBreakfastPrice\":" + ConverDouble(dbParm.BreakPrice) + ",";
            DataString = (String.IsNullOrEmpty(dbParm.IsNetwork)) ? DataString : DataString + "\"isNetwork\":" + dbParm.IsNetwork + ",";
            DataString = (String.IsNullOrEmpty(dbParm.OnePrice)) ? DataString : DataString + "\"onePrice\":" + ConverDouble(dbParm.OnePrice) + ",";
            DataString = (String.IsNullOrEmpty(dbParm.ThreePrice)) ? DataString : DataString + "\"threePrice\":" + ConverDouble(dbParm.ThreePrice) + ",";
            DataString = (String.IsNullOrEmpty(dbParm.FourPrice)) ? DataString : DataString + "\"fourPrice\":" + ConverDouble(dbParm.FourPrice) + ",";
            DataString = (String.IsNullOrEmpty(dbParm.BedPrice)) ? DataString : DataString + "\"attnPrice\":" + ConverDouble(dbParm.BedPrice) + ",";

            DataString = DataString + "\"updateUser\":\"" + dbParm.UpdateUser + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.applyHotelFullRoomV2();

            string strHotelFullRoom = CommonCallWebUrl(HotelFullRoomUrl + DataString);

            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "result").Trim('"')))
            {
                autohotelplanEntity.Result = 1;
                autohotelplanEntity.ErrorMSG = "保存成功！";
            }
            else
            {
                autohotelplanEntity.Result = 2;
                autohotelplanEntity.ErrorMSG = "保存失败！" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
            }

            return autohotelplanEntity;
        }

        public static Hashtable ApplySalesPlanList(AutoHotelPlanEntity autohotelplanEntity)
        {
            Hashtable htErrList = new Hashtable();
            string DataString = string.Empty;
            string RoomCode = string.Empty;

            string strCurrtDate = string.Empty;
            string strStartDate = string.Empty;
            string strEndDate = string.Empty;
            
            int iToWeek = 0;
            string strCurStart = DateTime.Now.ToShortDateString() + " 00:00:00";
            string strCurEnd = DateTime.Now.ToShortDateString() + " 04:00:00";
            DateTime dtNow = DateTime.Now;

            bool bFlag = false;
            string RoomStatus = string.Empty;
            string RoomCount = string.Empty;

            if (DateTime.Parse(strCurStart) <= dtNow && dtNow <= DateTime.Parse(strCurEnd))
            {
                strCurrtDate = DateTime.Now.AddDays(-1).ToShortDateString();
            }
            else
            {
                strCurrtDate = DateTime.Now.ToShortDateString();
            }
            DataSet dsRoom = new DataSet();
            DateTime dtStart;
            DateTime dtEnd;

            foreach (AutoHotelPlanDBEntity dbParm in autohotelplanEntity.AutoHotelPlanDBEntity)
            {
                if ("true".Equals(dbParm.RoomStatus))
                {
                    iToWeek = (int)DateTime.Now.DayOfWeek;
                    iToWeek = (iToWeek == 0) ? 7 : iToWeek;

                    if (DateTime.Parse(dbParm.StartDTime) <= DateTime.Parse(strCurrtDate) && DateTime.Parse(strCurrtDate) <= DateTime.Parse(dbParm.EndDTime) && dbParm.WeekList.IndexOf(iToWeek.ToString()) >= 0)
                    {
                        dsRoom = AutoHotelPlanDA.GetRoomInfo(strCurrtDate, dbParm.HotelID, dbParm.RoomCode);
                        if (dsRoom.Tables.Count > 0 && dsRoom.Tables[0].Rows.Count > 0)
                        {
                            RoomStatus = "1".Equals(dsRoom.Tables[0].Rows[0]["status"].ToString()) ? "true" : "false";
                            RoomCount = dsRoom.Tables[0].Rows[0]["room_num"].ToString();
                            bFlag = true;
                        }
                    }
                }

                if ("2".Equals(dbParm.TypeID))
                {
                    dtStart = DateTime.Parse(dbParm.StartDTime);
                    dtEnd = DateTime.Parse(dbParm.EndDTime);
                    if (dtStart <= (DateTime.Parse(dtNow.ToShortDateString())) && (DateTime.Parse(dtNow.ToShortDateString())) <= dtEnd)
                    {
                        dbParm.StartDTime = dtNow.ToShortDateString();
                        dbParm.EndDTime = dtNow.ToShortDateString();
                    }
                }

                RoomCode = "";
                //RoomCode = RoomCode + "{\"roomTypeName\":\"" + dbParm.RoomName + "\"," + "\"roomTypeCode\":\"" + dbParm.RoomCode + "\"," + "\"status\":" + ((bFlag) ? RoomStatus : dbParm.RoomStatus) + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}";
                RoomCode = RoomCode + "{\"roomTypeName\":\"" + dbParm.RoomName + "\"," + "\"roomTypeCode\":\"" + dbParm.RoomCode + "\"" + ((bFlag) ? "" : (",\"status\":" + dbParm.RoomStatus)) + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}";

                DataString = DataString + "{\"signId\":\"" + dbParm.HPID + "\"," + "\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime.Replace("/", "-") + "\"," + "\"endDate\":\"" + dbParm.EndDTime.Replace("/", "-") + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"guaid\":\"" + dbParm.Note1 + "\"," + "\"cxlid\":\"" + dbParm.Note2 + "\"," + "\"offsetunit\":\"" + dbParm.Offsetunit + "\"," + "\"effectHour\":\"" + dbParm.EffHour + "\",";
                DataString = (String.IsNullOrEmpty(dbParm.TwoPrice)) ? DataString : DataString + "\"twoPrice\":" + ConverDouble(dbParm.TwoPrice) + ",";

                //DataString = (bFlag) ? DataString + "\"roomNum\":" + RoomCount + "," : ("true".Equals(dbParm.RoomStatus) && dbParm.RoomCount.Length > 0) ? DataString + "\"roomNum\":" + dbParm.RoomCount + "," : DataString;
                DataString = (bFlag) ? DataString : ("true".Equals(dbParm.RoomStatus) && dbParm.RoomCount.Length > 0) ? DataString + "\"roomNum\":" + dbParm.RoomCount + "," : DataString;

                DataString = (String.IsNullOrEmpty(dbParm.Offsetval)) ? DataString : DataString + "\"offsetval\":" + ConverDouble(dbParm.Offsetval) + ",";
                DataString = (String.IsNullOrEmpty(dbParm.BreakfastNum)) ? DataString : DataString + "\"breakfastNum\":" + dbParm.BreakfastNum + ",";
                DataString = (String.IsNullOrEmpty(dbParm.BreakPrice)) ? DataString : DataString + "\"eachBreakfastPrice\":" + ConverDouble(dbParm.BreakPrice) + ",";
                DataString = (String.IsNullOrEmpty(dbParm.IsNetwork)) ? DataString : DataString + "\"isNetwork\":" + dbParm.IsNetwork + ",";
                DataString = (String.IsNullOrEmpty(dbParm.OnePrice)) ? DataString : DataString + "\"onePrice\":" + ConverDouble(dbParm.OnePrice) + ",";
                DataString = (String.IsNullOrEmpty(dbParm.ThreePrice)) ? DataString : DataString + "\"threePrice\":" + ConverDouble(dbParm.ThreePrice) + ",";
                DataString = (String.IsNullOrEmpty(dbParm.FourPrice)) ? DataString : DataString + "\"fourPrice\":" + ConverDouble(dbParm.FourPrice) + ",";
                DataString = (String.IsNullOrEmpty(dbParm.BedPrice)) ? DataString : DataString + "\"attnPrice\":" + ConverDouble(dbParm.BedPrice) + ",";
                DataString = (String.IsNullOrEmpty(dbParm.NetPrice)) ? DataString : DataString + "\"thirdPrice\":" + ConverDouble(dbParm.NetPrice) + ",";
                DataString = (String.IsNullOrEmpty(dbParm.Supplier)) ? DataString : DataString + "\"source\":\"" + dbParm.Supplier + "\",";
                DataString = DataString + "\"updateUser\":\"" + dbParm.UpdateUser + "\"" + "},";
                bFlag = false;
            }

            DataString = DataString.Length > 0 ? "{\"planArray\":[" + DataString.Substring(0, DataString.Length - 1) + "]," + "\"platformCode\":\"" + "CMS" + "\"}" : "{\"planArray\":[" + DataString + "]," + "\"platformCode\":\"" + "CMS" + "\"}";
            string HotelFullRoomUrl = JsonRequestURLBuilder.applyHotelFullRoomListV2();
            //string strHotelFullRoom = CommonCallWebUrl(HotelFullRoomUrl + DataString);
            CallWebPage callWebPage = new CallWebPage();
            //string strHotelFullRoom = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);
            try
            {
                //Console.WriteLine("HotelFullRoomUrl:" + HotelFullRoomUrl);
                //Console.WriteLine("DataString:" + DataString);

                var str = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);


                //Console.WriteLine("Result:" + str);

                var rs = JsonRequestURLBuilder.FromJsonTo<ResultBase<List<MsgListV2>>>(str);

                //Console.WriteLine("Result To Json:" + rs.result.Count);

                foreach (MsgListV2 msg in rs.result)
                {
                    //Console.WriteLine("Result To Json msg:" + msg.signId + msg.msg.Trim().ToLower());
                    if (!htErrList.ContainsKey(msg.signId))
                    {
                        htErrList.Add(msg.signId, "success".Equals(msg.msg.Trim().ToLower()) ? "保存成功！" : "保存失败！"+ msg.msg);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Service Error:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
            }
            //JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);
            //if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "result").Trim('"')))
            //{
            //    autohotelplanEntity.Result = 1;
            //    autohotelplanEntity.ErrorMSG = "保存成功！";
            //}
            //else
            //{
            //    autohotelplanEntity.Result = 2;
            //    autohotelplanEntity.ErrorMSG = "保存失败！" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
            //}

            return htErrList;
        }

        public class ResultBase<T>
        {
            public int code { get; set; }

            public string message { get; set; }

            public T result { get; set; }

        }

        public class MsgListV2
        {
            public string signId { get; set; }
            public string msg { get; set; }
        }

        public static AutoHotelPlanEntity CheckSalesHotelPlan(AutoHotelPlanEntity autohotelplanEntity)
        {
            AutoHotelPlanDBEntity dbParm = (autohotelplanEntity.AutoHotelPlanDBEntity.Count > 0) ? autohotelplanEntity.AutoHotelPlanDBEntity[0] : new AutoHotelPlanDBEntity();
            Hashtable alRoom = QueryFullRoomList(autohotelplanEntity);
            if (!alRoom.ContainsKey(dbParm.RoomCode))
            {
                autohotelplanEntity.ErrorMSG = "保存失败！该酒店无房型销售 请确认！酒店ID：" + dbParm.HotelID + "  房型：[" + dbParm.RoomCode + "]" + dbParm.RoomName;
                return autohotelplanEntity;
            }
            else
            {
                autohotelplanEntity.ErrorMSG = "";
            }
            return autohotelplanEntity;
        }

        private static string ConverDouble(string param)
        {
            string strResult = "";

            if (String.IsNullOrEmpty(param))
            {
                return strResult;
            }

            return double.Parse(param).ToString("#.00");
        }

        public static Hashtable QueryFullRoomList(AutoHotelPlanEntity autohotelplanEntity)
        {
            AutoHotelPlanDBEntity dbParm = (autohotelplanEntity.AutoHotelPlanDBEntity.Count > 0) ? autohotelplanEntity.AutoHotelPlanDBEntity[0] : new AutoHotelPlanDBEntity();
            Hashtable alRoom = new Hashtable();
            try
            {
                string url = JsonRequestURLBuilder.queryHotelFullRoomV2();
                string HotelID = dbParm.HotelID;// dbParm.HotelID.Substring((dbParm.HotelID.IndexOf('[') + 1), (dbParm.HotelID.IndexOf(']') - 1));
                string DataString = "{\"hotelId\":\"" + HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"beginDate\":\"" + dbParm.StartDTime.Replace("/", "-") + "\"," + "\"endDate\":\"" + dbParm.EndDTime.Replace("/", "-") + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url + DataString, "");

                //解析json数据
                JObject o = JObject.Parse(strJson);
                //JArray jsa = (JArray)o.SelectToken("result");
                string oCityList = o.SelectToken("result").ToString();
                JArray jsa = (JArray)JsonConvert.DeserializeObject(oCityList);
                string roomtypecode = string.Empty;
                string roomtypename = string.Empty;
                for (int i = 0; i < jsa.Count; i++)
                {
                    JObject jso = (JObject)jsa[i];
                    roomtypecode = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypecode").Trim('"');
                    roomtypename = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypename").Trim('"');

                    if (String.IsNullOrEmpty(roomtypecode) || alRoom.ContainsKey(roomtypecode))
                    {
                        continue;
                    }

                    alRoom.Add(roomtypecode, roomtypename);
                }
            }
            catch
            {

            }
            return alRoom;
        }

        public static string CommonCallWebUrl(string strUrl)
        {
            string strJson = string.Empty;
            try
            {
                CallWebPage callWebPage = new CallWebPage();
                strJson = callWebPage.CallWebByURL(strUrl, "");
            }
            catch
            {

            }
            return strJson;
        }
    }
}