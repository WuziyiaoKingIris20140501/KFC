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
using HotelVp.CMS.Domain.Entity;

using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;

namespace HotelVp.CMS.Domain.ServiceAdapter
{
    public abstract class HotelInfoSA
    {
        public static HotelInfoEntity BindHotelImagesList(HotelInfoEntity hotelInfoEntity)
        {
            ArrayList ayHotelImage = new ArrayList();

            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            string phoneUser = ConfigurationManager.AppSettings["phoneUser"];    //全局User
            string phoneKey = ConfigurationManager.AppSettings["phoneKey"];      //全局Key
            string checkInDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            //// 酒店图片信息取得
            //string HotelImagesUrl = JsonRequestURLBuilder.queryHotelImages(dbParm.HotelID, "200");
            //string strHotelImages = CommonCallWebUrl(HotelImagesUrl);
            //JObject oimage = JObject.Parse(strHotelImages);
            //JObject oHotelImages = (JObject)oimage.SelectToken("HotelImageRS[0]");

            //string oHotelImagesDetail = (oimage.SelectToken("HotelImageRS[0].gallery")!= null) ? oimage.SelectToken("HotelImageRS[0].gallery").ToString() : "";
            //if (!String.IsNullOrEmpty(oHotelImagesDetail))
            //{
            //    int index0 = oHotelImagesDetail.IndexOf("[");
            //    if (index0 == 0)
            //    {
            //        JArray jsa = (JArray)JsonConvert.DeserializeObject(oHotelImagesDetail);
            //        int iCount = 0;
            //        for (int i = 0; i < jsa.Count; i++)
            //        {
            //            JObject jso = (JObject)jsa[i];
            //            ayHotelImage.Add(JsonRequestURLBuilder.GetJsonStringValue(jso, "imageurl").Trim('"'));
            //            iCount = iCount + 1;
            //            if (iCount == 10)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        JObject jsooHotelImagesDetail = (JObject)JsonConvert.DeserializeObject(oHotelImagesDetail);
            //        ayHotelImage.Add(JsonRequestURLBuilder.GetJsonStringValue(jsooHotelImagesDetail, "imageurl").Trim('"'));
            //    }
            //}
            //hotelInfoEntity.HotelInfoDBEntity[0].HotelImage = ayHotelImage;
            //return hotelInfoEntity;

            // 酒店图片信息取得
            string HotelImagesUrl = JsonRequestURLBuilder.queryHotelImagesV2(dbParm.HotelID, "200");
            string strHotelImages = CommonCallWebUrl(HotelImagesUrl);
            JObject oimage = JObject.Parse(strHotelImages);

            string oHotelImagesDetail = (oimage.SelectToken("result") != null) ? oimage.SelectToken("result").ToString() : "";
            if (!String.IsNullOrEmpty(oHotelImagesDetail))
            {
                int index0 = oHotelImagesDetail.IndexOf("[");
                if (index0 == 0)
                {
                    JArray jsa = (JArray)JsonConvert.DeserializeObject(oHotelImagesDetail);
                    int iCount = 0;
                    for (int i = 0; i < jsa.Count; i++)
                    {
                        JObject jso = (JObject)jsa[i];
                        ayHotelImage.Add(JsonRequestURLBuilder.GetJsonStringValue(jso, "imageurl").Trim('"'));
                        iCount = iCount + 1;
                        if (iCount == 10)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    JObject jsooHotelImagesDetail = (JObject)JsonConvert.DeserializeObject(oHotelImagesDetail);
                    ayHotelImage.Add(JsonRequestURLBuilder.GetJsonStringValue(jsooHotelImagesDetail, "imageurl").Trim('"'));
                }
            }
            hotelInfoEntity.HotelInfoDBEntity[0].HotelImage = ayHotelImage;
            return hotelInfoEntity;
        }

        public static string SetBDlonglatTude(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            StringBuilder strBuf = new StringBuilder();

            String from = "2";	//0代表原gps,2代表google
            String to = "4";	//4代表baiDu
            strBuf.Append("from=").Append(from);
            strBuf.Append("&to=").Append(to);
            strBuf.Append("&x=").Append(dbParm.Longitude);
            strBuf.Append("&y=").Append(dbParm.Latitude);
            string strBDUrl = "http://api.map.baidu.com/ag/coord/convert?" + strBuf.ToString();

            string longitude = "";
            string latitude = "";
            string strHotelBDTude = CommonCallWebUrl(strBDUrl);
            if (!String.IsNullOrEmpty(strHotelBDTude))
            {
                JObject oBDTude = JObject.Parse(strHotelBDTude);

                string error = JsonRequestURLBuilder.GetJsonStringValue(oBDTude, "error").Trim('"');
                if ("0".Equals(error))
                {
                    longitude = DecodeBase64("utf-8", JsonRequestURLBuilder.GetJsonStringValue(oBDTude, "x").Trim('"'));
                    latitude = DecodeBase64("utf-8", JsonRequestURLBuilder.GetJsonStringValue(oBDTude, "y").Trim('"'));
                }
            }
            return "[{\"BDLongitude\":\"" + longitude + "\",\"BDLatitude\":\"" + latitude + "\"}]";
        }

        private static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);  //将2进制编码转换为8位无符号整数数组. 
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);  //将指定字节数组中的一个字节序列解码为一个字符串。 
            }
            catch
            {
                decode = code;
            }
            return decode;
        }


        public static HotelInfoEntity GetBalanceRoomList(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMCODE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMNM"));
            try
            {
                string url = JsonRequestURLBuilder.queryHotelBalRoomV2();
                url = url + dbParm.HotelID;
                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url, "roomcode");
                //解析json数据
                JObject o = JObject.Parse(strJson);
                //JArray jsa = (JArray)o.SelectToken("result");
                string oRoomList = o.SelectToken("result").ToString();
                JArray jsa = (JArray)JsonConvert.DeserializeObject(oRoomList);
                string roomtypecode = string.Empty;
                string roomtypename = string.Empty;
                for (int i = 0; i < jsa.Count; i++)
                {
                    JObject jso = (JObject)jsa[i];
                    roomtypecode = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomcode").Trim('"');
                    roomtypename = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomname").Trim('"');

                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["ROOMCODE"] = roomtypecode;
                    drRow["ROOMNM"] = roomtypename;
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }
            catch
            {

            }

            hotelInfoEntity.QueryResult = dsResult;
            return hotelInfoEntity;
        }

        public static HotelInfoEntity SetBalanceRoomList(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            string[] strRoomList = dbParm.HRoomList.Split(',');
            string HotelID = dbParm.HotelID;
            string RoomCode = "";

            string msg = string.Empty;
            foreach (string strRoomCode in strRoomList)
            {
                if (String.IsNullOrEmpty(strRoomCode))
                {
                    continue;
                }

                RoomCode = RoomCode + strRoomCode + "-" + dbParm.PriceCode + ",";
            }
            RoomCode = RoomCode.Trim(',');

            string DataString = "{\"dateType\":\"" + "1" + "\"," + "\"beginDate\":\"" + dbParm.InDateFrom + "\"," + "\"endDate\":\"" + dbParm.InDateTo + "\"," + "\"hotelIds\":\"" + dbParm.HotelID + "\"," + "\"roomCodes\":\"" + RoomCode + "\"," + "\"commisionMode\":\"" + dbParm.BalType + "\"," + "\"commision\":\"" + dbParm.BalValue + "\"," + "\"isPushFog\":\"" + dbParm.IsPushFog + "\"}";

            string HotelBalRoomUrl = JsonRequestURLBuilder.applyHotelBalRoomV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelBalRoom = callWebPage.CallWebByURL(HotelBalRoomUrl, DataString);
            JObject oHotelBalRoom = JObject.Parse(strHotelBalRoom);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelBalRoom, "message").Trim('"')))
            {
                hotelInfoEntity.Result = 1;
            }
            else
            {
                string ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelBalRoom, "message").Trim('"');
                hotelInfoEntity.ErrorMSG = ErrorMSG;
                hotelInfoEntity.Result = 2;
            }
            return hotelInfoEntity;
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

        /// <summary>
        /// 获取计划接口
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static HotelInfoEntity GetPlanList(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            #region
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTMODIFIED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMTYPECODE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GUAID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ONEPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("TWOPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("RATECODE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CREATOR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTCREATED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CXLID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("LMPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("BREAKFASTNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOLDROOMNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISNETWORK"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ATTNPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("OFFSETUNIT"));
            dsResult.Tables[0].Columns.Add(new DataColumn("OFFSETVAL"));
            dsResult.Tables[0].Columns.Add(new DataColumn("THIRDPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("STATUS"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EFFECTDATE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EACHBREAKFASTPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTMODIFIEDSTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CREATORSTR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTCREATEDSTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISDELETED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELVPSTATUS"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MODIFIERSTR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MODIFIER"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISRESERVE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("SEASON"));
            dsResult.Tables[0].Columns.Add(new DataColumn("THREEPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EFFECTDATESTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMTYPENAME"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MONEYTYPE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("FOURPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISROOMFUL"));
            dsResult.Tables[0].Columns.Add(new DataColumn("SOURCE"));
            #endregion
            try
            {
                string url = JsonRequestURLBuilder.queryHotelFullRoomV2();

                string DataString = "{\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"beginDate\":\"" + dbParm.SalesStartDT + "\"," + "\"endDate\":\"" + dbParm.SalesEndDT + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url + DataString, "");
                ArrayList alRoom = new ArrayList();
                //解析json数据
                JObject o = JObject.Parse(strJson);
                string oCityList = o.SelectToken("result").ToString();
                JArray jsa = (JArray)JsonConvert.DeserializeObject(oCityList);
                string roomtypecode = string.Empty;
                string roomtypename = string.Empty;
                for (int i = 0; i < jsa.Count; i++)
                {
                    JObject jso = (JObject)jsa[i];
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    #region
                    drRow["GMTMODIFIED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtmodified").Trim('"');
                    drRow["ROOMTYPECODE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypecode").Trim('"');
                    drRow["GUAID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "guaid").Trim('"');
                    drRow["ONEPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "oneprice").Trim('"');
                    drRow["TWOPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "twoprice").Trim('"');
                    drRow["HOTELID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelid").Trim('"');
                    drRow["RATECODE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "ratecode").Trim('"');
                    drRow["CREATOR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "creator").Trim('"');
                    drRow["ID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "id").Trim('"');
                    drRow["GMTCREATED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtcreated").Trim('"');
                    drRow["CXLID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "cxlid").Trim('"');
                    drRow["LMPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "lmprice").Trim('"');
                    drRow["BREAKFASTNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "breakfastnum").Trim('"');
                    drRow["ROOMNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomnum").Trim('"');
                    drRow["HOLDROOMNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "holdroomnum").Trim('"');
                    drRow["ISNETWORK"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isnetwork").Trim('"');
                    drRow["ATTNPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "attnprice").Trim('"');
                    drRow["OFFSETUNIT"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "offsetunit").Trim('"');
                    drRow["OFFSETVAL"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "offsetval").Trim('"');
                    drRow["THIRDPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "thirdprice").Trim('"');
                    drRow["STATUS"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "status").Trim('"');
                    drRow["EFFECTDATE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "effectdate").Trim('"');
                    drRow["EACHBREAKFASTPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "eachbreakfastprice").Trim('"');
                    drRow["GMTMODIFIEDSTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtmodifiedstring").Trim('"');
                    drRow["CREATORSTR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "creatorstr").Trim('"');
                    drRow["GMTCREATEDSTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtcreatedstring").Trim('"');
                    drRow["ISDELETED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isdeleted").Trim('"');
                    drRow["HOTELVPSTATUS"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelvpstatus").Trim('"');
                    drRow["MODIFIERSTR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "modifierstr").Trim('"');
                    drRow["MODIFIER"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "modifier").Trim('"');
                    drRow["ISRESERVE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isreserve").Trim('"');
                    drRow["SEASON"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "season").Trim('"');
                    drRow["THREEPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "threeprice").Trim('"');
                    drRow["EFFECTDATESTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "effectdatestring").Trim('"');
                    drRow["ROOMTYPENAME"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypename").Trim('"');
                    drRow["MONEYTYPE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "moneytype").Trim('"');
                    drRow["FOURPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "fourprice").Trim('"');
                    drRow["ISROOMFUL"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isroomful").Trim('"');
                    drRow["SOURCE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "source").Trim('"');
                    #endregion
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }
            catch
            {

            }
            hotelInfoEntity.QueryResult = dsResult;

            return hotelInfoEntity;
        }

        /// <summary>
        /// 获取计划接口   不区分价格代码
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static HotelInfoEntity GetPlanListByIndiscriminatelyRateCode(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            #region
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTMODIFIED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMTYPECODE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GUAID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ONEPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("TWOPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("RATECODE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CREATOR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTCREATED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CXLID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("LMPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("BREAKFASTNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOLDROOMNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISNETWORK"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ATTNPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("OFFSETUNIT"));
            dsResult.Tables[0].Columns.Add(new DataColumn("OFFSETVAL"));
            dsResult.Tables[0].Columns.Add(new DataColumn("THIRDPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("STATUS"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EFFECTDATE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EACHBREAKFASTPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTMODIFIEDSTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CREATORSTR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTCREATEDSTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISDELETED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELVPSTATUS"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MODIFIERSTR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MODIFIER"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISRESERVE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("SEASON"));
            dsResult.Tables[0].Columns.Add(new DataColumn("THREEPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EFFECTDATESTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMTYPENAME"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MONEYTYPE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("FOURPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISROOMFUL"));
            #endregion
            try
            {
                string url = JsonRequestURLBuilder.queryHotelFullRoomV2();

                string DataString = "{\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"beginDate\":\"" + dbParm.SalesStartDT + "\"," + "\"endDate\":\"" + dbParm.SalesEndDT + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url + DataString, "");
                ArrayList alRoom = new ArrayList();
                //解析json数据
                JObject o = JObject.Parse(strJson);
                string oCityList = o.SelectToken("result").ToString();
                JArray jsa = (JArray)JsonConvert.DeserializeObject(oCityList);
                string roomtypecode = string.Empty;
                string roomtypename = string.Empty;
                for (int i = 0; i < jsa.Count; i++)
                {
                    JObject jso = (JObject)jsa[i];
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    #region
                    drRow["GMTMODIFIED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtmodified").Trim('"');
                    drRow["ROOMTYPECODE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypecode").Trim('"');
                    drRow["GUAID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "guaid").Trim('"');
                    drRow["ONEPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "oneprice").Trim('"');
                    drRow["TWOPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "twoprice").Trim('"');
                    drRow["HOTELID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelid").Trim('"');
                    drRow["RATECODE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "ratecode").Trim('"');
                    drRow["CREATOR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "creator").Trim('"');
                    drRow["ID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "id").Trim('"');
                    drRow["GMTCREATED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtcreated").Trim('"');
                    drRow["CXLID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "cxlid").Trim('"');
                    drRow["LMPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "lmprice").Trim('"');
                    drRow["BREAKFASTNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "breakfastnum").Trim('"');
                    drRow["ROOMNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomnum").Trim('"');
                    drRow["HOLDROOMNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "holdroomnum").Trim('"');
                    drRow["ISNETWORK"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isnetwork").Trim('"');
                    drRow["ATTNPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "attnprice").Trim('"');
                    drRow["OFFSETUNIT"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "offsetunit").Trim('"');
                    drRow["OFFSETVAL"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "offsetval").Trim('"');
                    drRow["THIRDPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "thirdprice").Trim('"');
                    drRow["STATUS"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "status").Trim('"');
                    drRow["EFFECTDATE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "effectdate").Trim('"');
                    drRow["EACHBREAKFASTPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "eachbreakfastprice").Trim('"');
                    drRow["GMTMODIFIEDSTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtmodifiedstring").Trim('"');
                    drRow["CREATORSTR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "creatorstr").Trim('"');
                    drRow["GMTCREATEDSTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtcreatedstring").Trim('"');
                    drRow["ISDELETED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isdeleted").Trim('"');
                    drRow["HOTELVPSTATUS"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelvpstatus").Trim('"');
                    drRow["MODIFIERSTR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "modifierstr").Trim('"');
                    drRow["MODIFIER"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "modifier").Trim('"');
                    drRow["ISRESERVE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isreserve").Trim('"');
                    drRow["SEASON"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "season").Trim('"');
                    drRow["THREEPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "threeprice").Trim('"');
                    drRow["EFFECTDATESTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "effectdatestring").Trim('"');
                    drRow["ROOMTYPENAME"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypename").Trim('"');
                    drRow["MONEYTYPE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "moneytype").Trim('"');
                    drRow["FOURPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "fourprice").Trim('"');
                    drRow["ISROOMFUL"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isroomful").Trim('"');
                    #endregion
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }
            catch
            {

            }
            hotelInfoEntity.QueryResult = dsResult;

            return hotelInfoEntity;
        }


        /// <summary>
        /// 获取计划接口   区分价格代码
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static HotelInfoEntity GetPlanListByIndiscriminatelyByRateCode(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            #region
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTMODIFIED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMTYPECODE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GUAID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ONEPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("TWOPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("RATECODE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CREATOR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTCREATED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CXLID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("LMPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("BREAKFASTNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOLDROOMNUM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISNETWORK"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ATTNPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("OFFSETUNIT"));
            dsResult.Tables[0].Columns.Add(new DataColumn("OFFSETVAL"));
            dsResult.Tables[0].Columns.Add(new DataColumn("THIRDPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("STATUS"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EFFECTDATE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EACHBREAKFASTPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTMODIFIEDSTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CREATORSTR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("GMTCREATEDSTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISDELETED"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELVPSTATUS"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MODIFIERSTR"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MODIFIER"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISRESERVE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("SEASON"));
            dsResult.Tables[0].Columns.Add(new DataColumn("THREEPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("EFFECTDATESTRING"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ROOMTYPENAME"));
            dsResult.Tables[0].Columns.Add(new DataColumn("MONEYTYPE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("FOURPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("ISROOMFUL"));
            #endregion
            try
            {
                string url = JsonRequestURLBuilder.queryHotelFullRoomV2();

                string DataString = "{\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"beginDate\":\"" + dbParm.SalesStartDT + "\"," + "\"endDate\":\"" + dbParm.SalesEndDT + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url + DataString, "");
                ArrayList alRoom = new ArrayList();
                //解析json数据
                JObject o = JObject.Parse(strJson);
                string oCityList = o.SelectToken("result").ToString();
                JArray jsa = (JArray)JsonConvert.DeserializeObject(oCityList);
                string roomtypecode = string.Empty;
                string roomtypename = string.Empty;
                for (int i = 0; i < jsa.Count; i++)
                {
                    JObject jso = (JObject)jsa[i];
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    #region
                    drRow["GMTMODIFIED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtmodified").Trim('"');
                    drRow["ROOMTYPECODE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypecode").Trim('"');
                    drRow["GUAID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "guaid").Trim('"');
                    drRow["ONEPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "oneprice").Trim('"');
                    drRow["TWOPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "twoprice").Trim('"');
                    drRow["HOTELID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelid").Trim('"');
                    drRow["RATECODE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "ratecode").Trim('"');
                    drRow["CREATOR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "creator").Trim('"');
                    drRow["ID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "id").Trim('"');
                    drRow["GMTCREATED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtcreated").Trim('"');
                    drRow["CXLID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "cxlid").Trim('"');
                    drRow["LMPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "lmprice").Trim('"');
                    drRow["BREAKFASTNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "breakfastnum").Trim('"');
                    drRow["ROOMNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomnum").Trim('"');
                    drRow["HOLDROOMNUM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "holdroomnum").Trim('"');
                    drRow["ISNETWORK"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isnetwork").Trim('"');
                    drRow["ATTNPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "attnprice").Trim('"');
                    drRow["OFFSETUNIT"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "offsetunit").Trim('"');
                    drRow["OFFSETVAL"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "offsetval").Trim('"');
                    drRow["THIRDPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "thirdprice").Trim('"');
                    drRow["STATUS"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "status").Trim('"');
                    drRow["EFFECTDATE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "effectdate").Trim('"');
                    drRow["EACHBREAKFASTPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "eachbreakfastprice").Trim('"');
                    drRow["GMTMODIFIEDSTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtmodifiedstring").Trim('"');
                    drRow["CREATORSTR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "creatorstr").Trim('"');
                    drRow["GMTCREATEDSTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "gmtcreatedstring").Trim('"');
                    drRow["ISDELETED"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isdeleted").Trim('"');
                    drRow["HOTELVPSTATUS"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelvpstatus").Trim('"');
                    drRow["MODIFIERSTR"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "modifierstr").Trim('"');
                    drRow["MODIFIER"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "modifier").Trim('"');
                    drRow["ISRESERVE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isreserve").Trim('"');
                    drRow["SEASON"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "season").Trim('"');
                    drRow["THREEPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "threeprice").Trim('"');
                    drRow["EFFECTDATESTRING"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "effectdatestring").Trim('"');
                    drRow["ROOMTYPENAME"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypename").Trim('"');
                    drRow["MONEYTYPE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "moneytype").Trim('"');
                    drRow["FOURPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "fourprice").Trim('"');
                    drRow["ISROOMFUL"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "isroomful").Trim('"');
                    #endregion
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }
            catch
            {

            }
            hotelInfoEntity.QueryResult = dsResult;

            return hotelInfoEntity;
        }

        /// <summary>
        /// 获取计划接口   不区分价格代码  直接返回Json格式数据(只返回Result的部分数据)
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static HotelInfoEntity GetPlanListByResult(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            try
            {
                string url = JsonRequestURLBuilder.queryHotelFullRoomV2();

                string DataString = "{\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"beginDate\":\"" + dbParm.SalesStartDT + "\"," + "\"endDate\":\"" + dbParm.SalesEndDT + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url + DataString, "");
                ArrayList alRoom = new ArrayList();
                //解析json数据
                JObject o = JObject.Parse(strJson);
                string oCityList = o.SelectToken("result").ToString();
                hotelInfoEntity.ErrorMSG = oCityList;
            }
            catch
            {
                hotelInfoEntity.ErrorMSG = "";
            }
            return hotelInfoEntity;
        }

        /// <summary>
        /// 房态控制 -- 更新计划
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static APPContentEntity RenewPlanFullRoom(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            string RoomCode = "";
            string DataString = "";
            if (dbParm.IsReserve == null && dbParm.RoomCount == null)
            {
                string[] strRoomName = dbParm.RoomName.Split(',');
                string[] strRoomCode = dbParm.RoomCode.Split(',');
                for (int i = 0; i < strRoomCode.Length; i++)
                {
                    //RoomCode = RoomCode + "{\"roomTypeName\":\"" + strRoomName[i].ToString() + "\"," + "\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + "}" + ",";
                    RoomCode = RoomCode + "{\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + "}" + ",";
                }
                RoomCode = RoomCode.TrimEnd(',');

                DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"updateUser\":\"" + dbParm.UpdateUser + "(房控页面)" + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";

            }
            else if (dbParm.IsReserve != null && dbParm.RoomCount == null)
            {
                string[] strRoomName = dbParm.RoomName.Split(',');
                string[] strRoomCode = dbParm.RoomCode.Split(',');
                for (int i = 0; i < strRoomCode.Length; i++)
                {
                    //RoomCode = RoomCode + "{\"roomTypeName\":\"" + strRoomName[i].ToString() + "\"," + "\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}" + ",";
                    RoomCode = RoomCode + "{\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}" + ",";
                }
                RoomCode = RoomCode.TrimEnd(',');

                DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"updateUser\":\"" + dbParm.UpdateUser + "(房控页面)" + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
            }
            else
            {
                string[] strRoomName = dbParm.RoomName.Split(',');
                string[] strRoomCode = dbParm.RoomCode.Split(',');
                for (int i = 0; i < strRoomCode.Length; i++)
                {
                    //RoomCode = RoomCode + "{\"roomTypeName\":\"" + strRoomName[i].ToString() + "\"," + "\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}" + ",";
                    RoomCode = RoomCode + "{\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}" + ",";
                }
                RoomCode = RoomCode.TrimEnd(',');



                DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"roomNum\":\"" + dbParm.RoomCount + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"updateUser\":\"" + dbParm.UpdateUser + "(房控页面)" + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
            }
            string HotelFullRoomUrl = JsonRequestURLBuilder.applyHotelFullRoomV2();

            string strHotelFullRoom = CommonCallWebUrl(HotelFullRoomUrl + DataString);

            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "result").Trim('"')))
            {
                appcontentEntity.Result = 1;
            }
            else
            {
                appcontentEntity.Result = 2;
            }

            return appcontentEntity;
        }

        /// <summary>
        /// 房态控制 -- 更新计划(只更新  不添加)
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static APPContentEntity RenewPlanFullRoomByUpdatePlan(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            string RoomCode = "";
            string DataString = "";
            if (dbParm.IsReserve == null && dbParm.RoomCount == null)
            {
                string[] strRoomName = dbParm.RoomName.Split(',');
                string[] strRoomCode = dbParm.RoomCode.Split(',');
                for (int i = 0; i < strRoomCode.Length; i++)
                {
                    RoomCode = RoomCode + "{\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + "}" + ",";
                }
                RoomCode = RoomCode.TrimEnd(',');

                DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"updateUser\":\"" + dbParm.UpdateUser + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";

            }
            else if (dbParm.IsReserve != null && dbParm.RoomCount == null)
            {
                string[] strRoomName = dbParm.RoomName.Split(',');
                string[] strRoomCode = dbParm.RoomCode.Split(',');
                for (int i = 0; i < strRoomCode.Length; i++)
                {
                    RoomCode = RoomCode + "{\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}" + ",";
                }
                RoomCode = RoomCode.TrimEnd(',');

                DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"updateUser\":\"" + dbParm.UpdateUser + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
            }
            else
            {
                string[] strRoomName = dbParm.RoomName.Split(',');
                string[] strRoomCode = dbParm.RoomCode.Split(',');
                for (int i = 0; i < strRoomCode.Length; i++)
                {
                    RoomCode = RoomCode + "{\"roomTypeCode\":\"" + strRoomCode[i].ToString() + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}" + ",";
                }
                RoomCode = RoomCode.TrimEnd(',');



                DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"roomNum\":\"" + dbParm.RoomCount + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"updateUser\":\"" + dbParm.UpdateUser + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
            }
            string HotelFullRoomUrl = JsonRequestURLBuilder.applyHotelFullRoomByUpdatePlan();

            //string strHotelFullRoom = CommonCallWebUrl(HotelFullRoomUrl + DataString);

            CallWebPage callWebPage = new ServiceAdapter.CallWebPage();
            string strHotelFullRoom = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);

            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"')))
            {
                appcontentEntity.Result = 1;
                appcontentEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
            }
            else
            {
                appcontentEntity.Result = 2;
                appcontentEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
            }

            return appcontentEntity;
        }

        /// <summary>
        /// 房态控制 --  批量更新计划的接口  type:1 满房、2 关房、3 开房
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static APPContentEntity BatchUpdatePlan(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            string DataString = "{\"hotelId\":\"" + dbParm.HotelID + "\",\"whatDay\":\"1,2,3,4,5,6,7\",\"beginDate\":\"" + dbParm.StartDTime + "\",\"endDate\":\"" + dbParm.EndDTime + "\",\"operator\":\"" + dbParm.UpdateUser + "\",\"type\":\"" + dbParm.TypeID + "\",\"priceCodeRoom\":[{\"priceCode\":\"LMBAR\",\"roomCode\":\"" + dbParm.LmbarRoomCode + "\"},{\"priceCode\":\"LMBAR2\",\"roomCode\":\"" + dbParm.Lmbar2RoomCode + "\"}]}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.BatchUpdatePlan();

            CallWebPage callWebPage = new CallWebPage();
            string strHotelFullRoom = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);

            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);

            if ("200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "code").Trim('"')))
            {
                appcontentEntity.Result = 1;
            }
            else
            {
                appcontentEntity.Result = 2;
            }

            return appcontentEntity;
        }

        public static int SaveHotelRoomsPlanList(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            string HotelID = dbParm.HotelID;
            string RoomCode = dbParm.RoomCode;
            string RoomStatus = dbParm.RoomStatus;
            string RoomNM = "";
            if ("1".Equals(dbParm.RoomNMCG))
            {
                RoomNM = dbParm.RoomNM;
            }

            string DataString = "{\"hotelId\":\"" + HotelID + "\"," + "\"roomCode\":\"" + RoomCode + "\"," + "\"status\":\"" + RoomStatus + "\",";

            if ("1".Equals(dbParm.RoomNMCG))
            {
                DataString = DataString + "\"roomName\":\"" + dbParm.RoomNM + "\",";
            }

            DataString = DataString + "\"operator\":\"" + hotelInfoEntity.LogMessages.Username + "\"}";

            string HotelRoomPlanUrl = JsonRequestURLBuilder.applyHotelRoomPlanV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelRoomPlan = callWebPage.CallWebByURL(HotelRoomPlanUrl, DataString);
            JObject oHotelRoomPlan = JObject.Parse(strHotelRoomPlan);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelRoomPlan, "message").Trim('"')))
            {
                return 1;
            }
            else
            {
                return 4;
            }
        }

        public static HotelInfoEntity SaveHotelPrRoomsList(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            string DataString = "{\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"priceCode\":\"" + dbParm.PriceCode + "\"," + "\"roomCode\":\"" + dbParm.PRRoomS + "\"," + "\"status\":\"" + dbParm.PRStatus + "\"," + "\"isUpdatePlan\":\"" + dbParm.UPPlan + "\"," + "\"operateType\":\"" + dbParm.PRRoomACT + "\"," + "\"operator\":\"" + hotelInfoEntity.LogMessages.Username + "\"}";

            string HotelPrRoomPlanUrl = JsonRequestURLBuilder.applyHotelPrRoomPlanV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelPrRoomPlan = callWebPage.CallWebByURL(HotelPrRoomPlanUrl, DataString);
            JObject oHotelPrRoomPlan = JObject.Parse(strHotelPrRoomPlan);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"')))
            {
                hotelInfoEntity.Result = 1;
            }
            else
            {
                string ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"');
                hotelInfoEntity.ErrorMSG = ErrorMSG;
                hotelInfoEntity.Result = 2;
            }

            return hotelInfoEntity;
        }

        public static DataSet GetSupRoomList(DataTable dtHotelist)
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("SOURCES");
            dsResult.Tables[0].Columns.Add("ROOMCD");
            dsResult.Tables[0].Columns.Add("ROOMNM");
            string HotelsByIDUrl = "";
            string strHotelMain = "";
            string oRoomTypeCode = "";
            string strRoomCD = "";
            string strShowPrice = "";
            string strCode = "";
            for (int i = 0; i < dtHotelist.Rows.Count; i++)
            {
                if ("ELONG".Equals(dtHotelist.Rows[i]["SOURCES"].ToString()))
                {
                    HotelsByIDUrl = JsonRequestURLBuilder.queryHotelsBySUPIDV2("EL" + dtHotelist.Rows[i]["SUPID"].ToString(), DateTime.Now.ToShortDateString().Replace("/", "-"), DateTime.Now.AddDays(1).ToShortDateString().Replace("/", "-"));
                    strHotelMain = CommonCallWebUrl(HotelsByIDUrl);

                    if (strHotelMain.Length > 0)
                    {
                        JObject oHotelMain = JObject.Parse(strHotelMain);
                        strCode = JsonRequestURLBuilder.GetJsonStringValue(oHotelMain, "code").Trim('"');
                        if (!"200".Equals(strCode))
                        {
                            HotelsByIDUrl = JsonRequestURLBuilder.queryHotelsBySUPIDV2("EL" + dtHotelist.Rows[i]["SUPID"].ToString(), DateTime.Now.AddDays(1).ToShortDateString().Replace("/", "-"), DateTime.Now.AddDays(2).ToShortDateString().Replace("/", "-"));
                            strHotelMain = CommonCallWebUrl(HotelsByIDUrl);
                            oHotelMain = JObject.Parse(strHotelMain);
                            if (!"200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelMain, "code").Trim('"')))
                            {
                                continue;
                            }
                        }

                        strCode = "";
                        oRoomTypeCode = oHotelMain.SelectToken("result").SelectToken("roomPlans").ToString();
                        int index0 = oRoomTypeCode.IndexOf("[");
                        if (index0 == 0)
                        {
                            JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                            for (int j = 0; j < jsaRoomType.Count; j++)
                            {
                                JObject jsoRoomType = (JObject)jsaRoomType[j];
                                DataRow drRoom = dsResult.Tables[0].NewRow();
                                drRoom["SOURCES"] = dtHotelist.Rows[i]["SOURCES"].ToString();
                                //strRoomCD =  JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"');
                                //strRoomCD = (strRoomCD.Contains("-")) ? strRoomCD.Split('-')[0].ToString().Trim() : strRoomCD;

                                strRoomCD = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"');
                                drRoom["ROOMCD"] = dtHotelist.Rows[i]["SUPID"].ToString() + "_" + strRoomCD.Replace("-", "_");
                                strShowPrice = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "showprice").Trim('"');
                                strShowPrice = (strShowPrice.Contains(".")) ? strShowPrice.Split('.')[0].ToString() : strShowPrice;
                                drRoom["ROOMNM"] = "[" + strRoomCD + "]" + JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomname").Trim('"') + " - " + strShowPrice + "/晚";
                                dsResult.Tables[0].Rows.Add(drRoom);
                            }
                        }
                        else
                        {
                            JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                            for (int j = 0; j < jsaRoomType.Count; j++)
                            {
                                JObject jsoRoomType = (JObject)jsaRoomType[j];
                                DataRow drRoom = dsResult.Tables[0].NewRow();
                                drRoom["SOURCES"] = dtHotelist.Rows[i]["SOURCES"].ToString();
                                //strRoomCD =  JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"');
                                //strRoomCD = (strRoomCD.Contains("-")) ? strRoomCD.Split('-')[0].ToString().Trim() : strRoomCD;

                                strRoomCD = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"');
                                drRoom["ROOMCD"] = dtHotelist.Rows[i]["SUPID"].ToString() + "_" + strRoomCD.Replace("-", "_");
                                strShowPrice = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "showprice").Trim('"');
                                strShowPrice = (strShowPrice.Contains(".")) ? strShowPrice.Split('.')[0].ToString() : strShowPrice;
                                drRoom["ROOMNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomname").Trim('"') + " - " + strShowPrice + "/晚";
                                dsResult.Tables[0].Rows.Add(drRoom);
                            }
                        }
                    }
                }
            }

            return dsResult;
        }

        public static void RefushHotelList(string hotelLists)
        {
            try
            {
                string queueUrl = ConfigurationManager.AppSettings["HRQueueUrl"];
                string queueNm = ConfigurationManager.AppSettings["HRQueueNm"];
                //string strResult = "发送成功";
                //string strRegChanel = ConfigurationManager.AppSettings["MyQueueReg"].ToString();
                //string clientCode = string.Empty;

                //Create the Connection Factory
                IConnectionFactory factory = new ConnectionFactory(queueUrl);
                using (IConnection connection = factory.CreateConnection())
                {
                    //Create the Session  
                    using (ISession session = connection.CreateSession())
                    {
                        IMessageProducer prod = session.CreateProducer(
                        new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(queueNm));
                        ITextMessage msg = prod.CreateTextMessage();
                        msg.NMSDeliveryMode = MsgDeliveryMode.Persistent;
                        msg.Text = "{\"hotelIds\":\"" + hotelLists + "\"}";
                        prod.Send(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                //  Log King
                //CommonDA.InsertEventHistory("Que Push发送异常： Messag：" + ex.Message + "InnerException：" + ex.InnerException + "Source：" + ex.Source + "StackTrace：" + ex.StackTrace);
                //return;
            }
        }

    }
}
