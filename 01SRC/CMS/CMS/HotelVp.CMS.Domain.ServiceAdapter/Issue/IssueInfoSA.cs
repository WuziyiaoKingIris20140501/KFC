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
using System.Web.Security;

namespace HotelVp.CMS.Domain.ServiceAdapter
{
    public abstract class IssueInfoSA
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

            string DataString = "{\"dateType\":\"" + "1" + "\"," + "\"beginDate\":\"" + dbParm.InDateFrom + "\"," + "\"endDate\":\"" + dbParm.InDateTo + "\"," + "\"hotelIds\":\"" + dbParm.HotelID + "\"," + "\"roomCodes\":\"" + RoomCode + "\"," + "\"commisionMode\":\"" + dbParm.BalType + "\"," + "\"commision\":\"" + dbParm.BalValue + "\"}";

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

        public static string SendMsgToIssue(string TelList, string Type, string Content)
        {
            string strErrResult = "";
            string[] strTelList = { };
            TelList = TelList.Replace("，", ",");
            if (TelList.Contains(","))
            {
                strTelList = TelList.Split(',');
            }
            else
            {
                strTelList = new string[]{ TelList };
            }

            string DataString = "";

            foreach (string strTel in strTelList)
            {
                DataString = DataString + "{\"method\":\"save\",\"data\":{\"syscode\":\"" + "CMS" + "\",\"reqid\":\"" + strTel + "\",\"bizcode\":\"" + ("1".Equals(Type) ? "CMS-Issue单指派人短信通知" : "CMS-Issue单用户短信通知") + "\",";
                //DataString = DataString + "\"cnfnum\":\"" + strTel + "\",\"mobiles\":\"" + strTel + "\",\"msg\":\"" + Content + "\",";
                DataString = DataString + "\"mobiles\":\"" + strTel + "\",\"msg\":\"" + Content + "\",";
                DataString = DataString + "\"sign\":\"" + PostSignKey("CMS" + strTel) + "\"},\"version\":\"v1.0\"}";

                string HotelFullRoomUrl = JsonRequestURLBuilder.applySendMsgV2();
                CallWebPage callWebPage = new CallWebPage();
                string strHotelFullRoom = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);
                JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);
                if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"')))
                {
                    //return "发送成功！";
                }
                else
                {
                    strErrResult = strErrResult + "Tel: " + strTel + "发送失败！" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
                }
            }
            return strErrResult;
        }

        public static string PostSignKey(string body)
        {
            try
            {
                string MD5Key = ConfigurationManager.AppSettings["MSGMD5Key"].ToString();
                string signKey = FormsAuthentication.HashPasswordForStoringInConfigFile(body + MD5Key, "MD5");
                return signKey;
            }
            catch
            {
                return "";
            }
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