using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web.Security;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;
using HotelVp.CMS.Domain.Entity;

namespace HotelVp.CMS.Domain.ServiceAdapter
{
    public abstract class CashBackSA
    {
        public static CashBackEntity BindHotelImagesList(CashBackEntity cashBackEntity)
        {
            ArrayList ayHotelImage = new ArrayList();

            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();

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
            //cashBackEntity.CashBackDBEntity[0].HotelImage = ayHotelImage;
            //return cashBackEntity;

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
            cashBackEntity.CashBackDBEntity[0].HotelImage = ayHotelImage;
            return cashBackEntity;
        }

        public static CashBackEntity GetBalanceRoomList(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
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

            cashBackEntity.QueryResult = dsResult;
            return cashBackEntity;
        }

        public static CashBackEntity SetBalanceRoomList(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
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
                cashBackEntity.Result = 1;
            }
            else
            {
                string ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelBalRoom, "message").Trim('"');
                cashBackEntity.ErrorMSG = ErrorMSG;
                cashBackEntity.Result = 2;
            }
            return cashBackEntity;
        }



        public static CashBackEntity SaveCashBackRequest(CashBackEntity cashBackEntity)
        {
            string DataString = string.Empty;
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();

            //if ("0".Equals(dbParm.BackCashType))
            //{
            //    DataString = "{\"loginMobile\":\"" + dbParm.UserID + "\"," + "\"amount\":" + dbParm.BackCashAmount + "," + "\"bankBranch\":\"" + dbParm.BankBranch + "\"," + "\"bankCardNumber\":\"" + dbParm.BankCardNumber + "\"," + "\"bankCardOwner\":\"" + dbParm.BankOwner + "\"}";
            //}
            //else
            //{
            //    DataString = "{\"loginMobile\":\"" + dbParm.UserID + "\"," + "\"amount\":" + dbParm.BackCashAmount + "," + "\"bankBranch\":\"" + "999999" + "\"," + "\"bankCardNumber\":\"" + "999999" + "\"," + "\"bankCardOwner\":\"" + "999999" + "\"}";
            //}

            if ("0".Equals(dbParm.BackCashType))
            {
                DataString = "{\"loginMobile\":\"" + dbParm.UserID + "\"," + "\"amount\":" + dbParm.BackCashAmount + "," + "\"applyType\":" + "1" + "," + "\"bankBranch\":\"" + dbParm.BankBranch + "\"," + "\"bankCardNumber\":\"" + dbParm.BankCardNumber + "\"," + "\"bankCardOwner\":\"" + dbParm.BankOwner + "\"}";
            }
            else if ("1".Equals(dbParm.BackCashType))
            {
                DataString = "{\"loginMobile\":\"" + dbParm.UserID + "\"," + "\"amount\":" + dbParm.BackCashAmount + "," + "\"applyType\":" + "3" + "," + "\"rechargePhoneNumber\":\"" + dbParm.BackTel + "\"}";
            }
            else
            {
                DataString = "{\"loginMobile\":\"" + dbParm.UserID + "\"," + "\"amount\":" + dbParm.BackCashAmount + "," + "\"applyType\":" + "2" + "," + "\"alipayAccount\":\"" + dbParm.BackBao + "\"}";
            }

            string SaveCashBackRequestUrl = JsonRequestURLBuilder.SaveCashBackRequestV2() + PostSignKey(DataString);
            CallWebPage callWebPage = new CallWebPage();
            string strSaveCashBack = callWebPage.CallWebByURL(SaveCashBackRequestUrl, DataString);
            JObject oSaveCashBack = JObject.Parse(strSaveCashBack);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oSaveCashBack, "message").Trim('"')))
            {
                cashBackEntity.Result = 1;
                string oIDs = oSaveCashBack.SelectToken("result").ToString();
                JObject oID = JObject.Parse(oIDs);
                cashBackEntity.CashBackDBEntity[0].ID = oID.SelectToken("sn").ToString().Trim('"');
            }
            else
            {
                string ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oSaveCashBack, "message").Trim('"');
                cashBackEntity.ErrorMSG = ErrorMSG;
                cashBackEntity.Result = 2;
            }
            return cashBackEntity;
        }

        /// <summary>
        /// 支付宝充值接口
        /// </summary>
        /// <returns></returns>
        public static CashBackEntity autoPay(CashBackEntity cashBackEntity)
        {
            string DataString = string.Empty;
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            if (dbParm.BackCashType == "3")//手机充值
            {
                DataString = "{\"loginMobile\":\"" + dbParm.LoginMobile + "\",\"amount\":" + dbParm.BackCashAmount + ",\"applyType\":\"" + dbParm.BackCashType + "\",\"rechargePhoneNumber\":\"" + dbParm.Phone + "\",\"sn\":\"" + dbParm.Sn + "\",\"Remark\":\"" + dbParm.Remark + "\",\"operater\":\"" + dbParm.CreateUser + "\"}";
            }
            else if (dbParm.BackCashType == "2")//支付宝返还
            {
                DataString = "{\"loginMobile\":\"" + dbParm.LoginMobile + "\",\"amount\":" + dbParm.BackCashAmount + ",\"applyType\":\"" + dbParm.BackCashType + "\",\"alipayAccount\":\"" + dbParm.AlipayAccount + "\",\"userName\":\"" + dbParm.AlipayName + "\",\"sn\":\"" + dbParm.Sn + "\",\"Remark\":\"" + dbParm.Remark + "\",\"operater\":\"" + dbParm.CreateUser + "\"}";
            }
            string SaveCashBackRequestUrl = JsonRequestURLBuilder.autoPay();
            CallWebPage callWebPage = new CallWebPage();
            string strSaveCashBack = callWebPage.CallWebByURL(SaveCashBackRequestUrl, DataString);
            JObject oSaveCashBack = JObject.Parse(strSaveCashBack);

            if ("200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oSaveCashBack, "code").Trim('"')))
            {
                //成功
                cashBackEntity.Result = 1;
                cashBackEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oSaveCashBack, "message").Trim('"');
            }
            else
            {
                cashBackEntity.Result = 2;
                cashBackEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oSaveCashBack, "message").Trim('"');
                
            }
            return cashBackEntity;
        }


        public static string PostSignKey(string body)
        {
            try
            {
                string MD5Key = ConfigurationManager.AppSettings["MD5Key"].ToString();                                                     
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