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
using HotelVp.CMS.Domain.DataAccess;

namespace HotelVp.CMS.Domain.ServiceAdapter
{
    public abstract class APPContentV2SA
    {
        public static APPContentEntity CommonSelect(APPContentEntity appcontentEntity)
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("cityid"));
            dsResult.Tables[0].Columns.Add(new DataColumn("cityNM"));
            try
            {
                string strUserCode = (appcontentEntity.APPContentDBEntity.Count > 0 && !String.IsNullOrEmpty(appcontentEntity.APPContentDBEntity[0].PlatForm)) ? appcontentEntity.APPContentDBEntity[0].PlatForm : "IOS";
                string url = JsonRequestURLBuilder.getSearchCityUrlV3(strUserCode);//JsonRequestURLBuilder.getSearchCityUrlV2(strUserCode);
                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url, "");

                //解析json数据
                JObject o = JObject.Parse(strJson);
                //JArray jsa = (JArray)o.SelectToken("result");
                string oCityList = o.SelectToken("result").ToString();
                JArray jsa = (JArray)JsonConvert.DeserializeObject(oCityList);

                for (int i = 0; i < jsa.Count; i++)
                {
                    JObject jso = (JObject)jsa[i];
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["cityid"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "cityid").Trim('"');
                    drRow["cityNM"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "namecn").Trim('"') + "[" + JsonRequestURLBuilder.GetJsonStringValue(jso, "nameen").Trim('"') + "]";
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }
            catch
            {
                
            }
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }
        public static string PostSignKey(string body)
        {
            try
            {
                string MD5Key = ConfigurationManager.AppSettings["SMD5Key"].ToString();
                string signKey = FormsAuthentication.HashPasswordForStoringInConfigFile(body + MD5Key, "MD5");
                return signKey;
            }
            catch
            {
                return "";
            }
        }
        public static APPContentEntity HotelListSelect(APPContentEntity appcontentEntity)
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELNM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("TRADEAREA"));
            try
            {
                APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
                string url = JsonRequestURLBuilder.getSearchHotelListUrlV3(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID); //JsonRequestURLBuilder.getSearchHotelListUrlV2(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID);

                CallWebPage callWebPage = new CallWebPage();
                string DataString = "{\"cityId\":\"" + dbParm.CityID + "\"," + "\"type\":\"" + "1" + "\"}";
                url = url + "&signKey=" + PostSignKey(DataString);
                string strJson = callWebPage.CallWebByURL(url, DataString);

                //解析json数据
                JObject o = JObject.Parse(strJson);
                //JObject oHotelBase = (JObject)o.SelectToken("result");
                string oHotells = o.SelectToken("result").ToString();

                JObject oh = JObject.Parse(oHotells);
                string oHotelDetail = oh.SelectToken("hotellist").ToString();

                //酒店列表信息
                int index = oHotelDetail.IndexOf("[");

                string strHotelid = string.Empty;
                string strHotelname = string.Empty;
                string strTradearea = string.Empty;

                if (index == 0)
                {
                    //Array 
                    JArray jsa = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                    for (int i = 0; i < jsa.Count; i++)
                    {
                        JObject jso = (JObject)jsa[i];

                        strHotelid = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelid").Trim('"');
                        strHotelname = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelname").Trim('"');
                        strTradearea = JsonRequestURLBuilder.GetJsonStringValue(jso, "tradearea").Trim('"');

                        DataRow drRow = dsResult.Tables[0].NewRow();
                        drRow["HOTELID"] = strHotelid;
                        drRow["HOTELNM"] = strHotelname;
                        drRow["TRADEAREA"] = strTradearea;
                        dsResult.Tables[0].Rows.Add(drRow);
                    }
                }
                else
                {
                    JObject jsoObj = (JObject)JsonConvert.DeserializeObject(oHotelDetail);

                    strHotelid = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotelid").Trim('"');
                    strHotelname = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotelname").Trim('"');
                    strTradearea = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "tradearea").Trim('"');
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["HOTELID"] = strHotelid;
                    drRow["HOTELNM"] = strHotelname;
                    drRow["TRADEAREA"] = strTradearea;
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }
            catch (Exception ex)
            {
            }

            appcontentEntity.QueryResult = dsResult;

            return appcontentEntity;
        }

        public static APPContentEntity Select(APPContentEntity appcontentEntity)
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELNM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("PICTURE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("TAGNM"));
            dsResult.Tables[0].Columns.Add(new DataColumn("STARRATE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("LOWPRICE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("LONGITUDE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("LATITUDE"));

            try
            {
                APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
                //string url = JsonRequestURLBuilder.getSearchHotelListUrlV2(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID);

                //CallWebPage callWebPage = new CallWebPage();
                //string strJson = callWebPage.CallWebByURL(url, "");

                string url = JsonRequestURLBuilder.getSearchHotelListUrlV3(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID);

                CallWebPage callWebPage = new CallWebPage();
                string DataString = "{\"cityId\":\"" + dbParm.CityID + "\"," + "\"type\":\"" + "1" + "\"}";
                url = url + "&signKey=" + PostSignKey(DataString);
                string strJson = callWebPage.CallWebByURL(url, DataString);

                //解析json数据
                JObject o = JObject.Parse(strJson);
                //JObject oHotelBase = (JObject)o.SelectToken("result");
                string oHotells = o.SelectToken("result").ToString();

                JObject oh = JObject.Parse(oHotells);
                string oHotelDetail = oh.SelectToken("hotellist").ToString();

                //酒店列表信息
                int index = oHotelDetail.IndexOf("[");

                string strHotelid = string.Empty;
                string strHotelname = string.Empty;
                string strPicture = string.Empty;
                string strTradearea = string.Empty;
                string strStarcode = string.Empty;
                string strTwoprice = string.Empty;
                string strLongitude = string.Empty;
                string strLatitude = string.Empty;
                string strLowestprice = string.Empty;
                //decimal decTwoprice = 0;
                //decimal decPriceTemp = 0;

                if (index == 0)
                {
                    //Array 
                    JArray jsa = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                    for (int i = 0; i < jsa.Count; i++)
                    {
                        JObject jso = (JObject)jsa[i];

                        strHotelid = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelid").Trim('"');
                        strHotelname = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelnamezh").Trim('"');
                        strPicture = JsonRequestURLBuilder.GetJsonStringValue(jso, "picture").Trim('"');
                        strTradearea = JsonRequestURLBuilder.GetJsonStringValue(jso, "tradearea").Trim('"');
                        strStarcode = JsonRequestURLBuilder.GetJsonStringValue(jso, "starcode").Trim('"') + "[" + JsonRequestURLBuilder.GetJsonStringValue(jso, "stardesc").Trim('"') + "]";
                        strLongitude = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotellongitude").Trim('"');
                        strLatitude = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotellatitude").Trim('"');
                        strLowestprice = JsonRequestURLBuilder.GetJsonStringValue(jso, "lowestprice").Trim('"');
                        //string oRoomTypeCode = jso.SelectToken("planpromotionlist").ToString();

                        //int index0 = oRoomTypeCode.IndexOf("[");
                        ////List<HotelRoomTypeCode> liRoomType = new List<HotelRoomTypeCode>();

                        //decTwoprice = 0;
                        //decPriceTemp = 0;
                        //if (index0 == 0)
                        //{
                        //    JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                        //    for (int j = 0; j < jsaRoomType.Count; j++)
                        //    {
                        //        JObject jsoRoomType = (JObject)jsaRoomType[j];
                        //        decPriceTemp = (!String.IsNullOrEmpty(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"'))) ? decimal.Parse(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"')) : 0;

                        //        if (j == 0)
                        //        {
                        //            decTwoprice = decPriceTemp;
                        //        }
                        //        else if (j > 0 && decPriceTemp < decTwoprice)
                        //        {
                        //            decTwoprice = decPriceTemp;
                        //        }

                        //        //HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "hotelid"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "ratecode"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomname"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomnum"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "thirdpartprice"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice"));
                        //        //liRoomType.Add(saRoomType);
                        //    }
                        //}
                        //else
                        //{
                        //    JObject jsoObjRoomType = (JObject)JsonConvert.DeserializeObject(oRoomTypeCode);
                        //    decTwoprice = (!String.IsNullOrEmpty(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"'))) ? decimal.Parse(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"')) : 0;
                        //    //HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "hotelid"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "ratecode"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomcode"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomname"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomnum"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "thirdpartprice"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice"));
                        //    //liRoomType.Add(saRoomType);
                        //}

                        DataRow drRow = dsResult.Tables[0].NewRow();
                        drRow["HOTELID"] = strHotelid;
                        drRow["HOTELNM"] = strHotelname;
                        drRow["PICTURE"] = strPicture;
                        drRow["TAGNM"] = strTradearea;
                        drRow["STARRATE"] = strStarcode;
                        drRow["LOWPRICE"] = strLowestprice;
                        drRow["LONGITUDE"] = strLongitude;
                        drRow["LATITUDE"] = strLatitude;
                        dsResult.Tables[0].Rows.Add(drRow);
                    }
                }
                else
                {
                    JObject jsoObj = (JObject)JsonConvert.DeserializeObject(oHotelDetail);

                    strHotelid = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotelid").Trim('"');
                    strHotelname = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotelname").Trim('"');
                    strPicture = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "picture").Trim('"');
                    strTradearea = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "tradearea").Trim('"');
                    strStarcode = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "starcode").Trim('"') + "[" + JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "stardesc").Trim('"') + "]";
                    strLongitude = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotellongitude").Trim('"');
                    strLatitude = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotellatitude").Trim('"');
                    strLowestprice = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "lowestprice").Trim('"');
                    //string oRoomTypeCode = jsoObj.SelectToken("planpromotionlist").ToString();
                    //int index0 = oRoomTypeCode.IndexOf("[");
                    ////List<HotelRoomTypeCode> liRoomType = new List<HotelRoomTypeCode>();

                    //decTwoprice = 0;
                    //decPriceTemp = 0;
                    //if (index0 == 0)
                    //{
                    //    JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                    //    for (int j = 0; j < jsaRoomType.Count; j++)
                    //    {
                    //        JObject jsoRoomType = (JObject)jsaRoomType[j];
                    //        decPriceTemp = (!String.IsNullOrEmpty(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"'))) ? decimal.Parse(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"')) : 0;

                    //        if (j == 0)
                    //        {
                    //            decTwoprice = decPriceTemp;
                    //        }
                    //        else if (j > 0 && decPriceTemp < decTwoprice)
                    //        {
                    //            decTwoprice = decPriceTemp;
                    //        }
                    //        //HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "hotelid"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "ratecode"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomname"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomnum"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "thirdpartprice"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice"));
                    //        //liRoomType.Add(saRoomType);
                    //    }
                    //}
                    //else
                    //{
                    //    JObject jsoObjRoomType = (JObject)JsonConvert.DeserializeObject(oRoomTypeCode);
                    //    decTwoprice = (!String.IsNullOrEmpty(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"'))) ? decimal.Parse(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"')) : 0;
                    //    //HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "hotelid"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "ratecode"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomcode"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomname"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomnum"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "thirdpartprice"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice"));
                    //    //liRoomType.Add(saRoomType);
                    //}

                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["HOTELID"] = strHotelid;
                    drRow["HOTELNM"] = strHotelname;
                    drRow["PICTURE"] = strPicture;
                    drRow["TAGNM"] = strTradearea;
                    drRow["STARRATE"] = strStarcode;
                    drRow["LOWPRICE"] = strLowestprice;
                    drRow["LONGITUDE"] = strLongitude;
                    drRow["LATITUDE"] = strLatitude;
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }
            catch (Exception ex)
            {
            }

            appcontentEntity.QueryResult = dsResult;

            return appcontentEntity;
        }

        public static APPContentEntity HotelDetailListSelect(APPContentEntity appcontentEntity)
        {
            DataSet dsHotelMain = new DataSet();
            dsHotelMain.Tables.Add(new DataTable());
            dsHotelMain.Tables[0].Columns.Add("HOTELID");
            dsHotelMain.Tables[0].Columns.Add("HOTELNM");
            dsHotelMain.Tables[0].Columns.Add("HOTELGROUP");
            dsHotelMain.Tables[0].Columns.Add("ADDRESS");
            dsHotelMain.Tables[0].Columns.Add("LONGITUDE");
            dsHotelMain.Tables[0].Columns.Add("LATITUDE");
            dsHotelMain.Tables[0].Columns.Add("HOTELDES");
            dsHotelMain.Tables[0].Columns.Add("HOTELAPPR");
            dsHotelMain.Tables[0].Columns.Add("HOTELSERVICE");
            dsHotelMain.Tables[0].Columns.Add("BUSSES");
            dsHotelMain.Tables[0].Columns.Add("CUSTOMTEL");
            dsHotelMain.Tables[0].Columns.Add("TRADEAREA");
            dsHotelMain.Tables[0].Columns.Add("PRODESC");
            dsHotelMain.Tables[0].Columns.Add("PROCONTENT");
            dsHotelMain.Tables[0].Columns.Add("HTPPATH");

            DataRow drRow = dsHotelMain.Tables[0].NewRow();

            ArrayList ayHotelImage = new ArrayList();

            DataSet dsHotelRoom = new DataSet();
            dsHotelRoom.Tables.Add(new DataTable());
            dsHotelRoom.Tables[0].Columns.Add("ROOMID");
            dsHotelRoom.Tables[0].Columns.Add("ROOMNM");
            dsHotelRoom.Tables[0].Columns.Add("ROOMCODE");
            dsHotelRoom.Tables[0].Columns.Add("BEDNM");
            dsHotelRoom.Tables[0].Columns.Add("BOOKTYPE");
            dsHotelRoom.Tables[0].Columns.Add("BREAKFAST");
            dsHotelRoom.Tables[0].Columns.Add("FTPLINE");
            dsHotelRoom.Tables[0].Columns.Add("NETPRICE");
            dsHotelRoom.Tables[0].Columns.Add("VPPRICE");
            dsHotelRoom.Tables[0].Columns.Add("PROTITLE");
            dsHotelRoom.Tables[0].Columns.Add("PROCONT");

            //DataSet dsHotelFtType = new DataSet();

            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            string phoneUser = ConfigurationManager.AppSettings["phoneUser"];    //全局User
            string phoneKey = ConfigurationManager.AppSettings["phoneKey"];      //全局Key
            string checkInDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            // 酒店详情取得
            string HotelsByIDUrl = JsonRequestURLBuilder.queryHotelsByIDV2(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID);
            string strHotelMain = CommonCallWebUrl(HotelsByIDUrl);

            //解析json数据
            JObject oHotelMain = JObject.Parse(strHotelMain);
            //JObject oHotelBase = (JObject)o.SelectToken("result");
            string osHotelMain = oHotelMain.SelectToken("result").ToString();

            JObject ohls = JObject.Parse(osHotelMain);
            string oHotelsDetail = ohls.SelectToken("hotellist").ToString();

            //Array 
            JArray jsaHotels = (JArray)JsonConvert.DeserializeObject(oHotelsDetail);
            string strHotelid = "";
            for (int i = 0; i < jsaHotels.Count; i++)
            {
                JObject jsoHotels = (JObject)jsaHotels[i];

                strHotelid = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotelid").Trim('"');
                if (!dbParm.HotelID.Equals(strHotelid))
                {
                    continue;
                }

                drRow["HOTELID"] = strHotelid;
                drRow["HOTELNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotelname").Trim('"');
                drRow["HOTELGROUP"] = "";
                drRow["ADDRESS"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hoteladdr").Trim('"');
                drRow["LONGITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotellongitude").Trim('"');
                drRow["LATITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotellatitude").Trim('"');
                drRow["HOTELDES"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "simpledesc").Trim('"');
                drRow["TRADEAREA"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "tradearea").Trim('"');

                JObject jsoHotelsPro = (JObject)((JArray)JsonConvert.DeserializeObject(JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotelpromotionlist")))[0];
                drRow["PRODESC"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "prodesc").Trim('"');
                drRow["PROCONTENT"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "procontent").Trim('"');
                drRow["HTPPATH"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "htppath").Trim('"');

                string oRoomTypeCode = jsoHotels.SelectToken("planpromotionlist").ToString();
                int index0 = oRoomTypeCode.IndexOf("[");
                //List<HotelRoomTypeCode> liRoomType = new List<HotelRoomTypeCode>();
                if (index0 == 0)
                {
                    JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                    for (int j = 0; j < jsaRoomType.Count; j++)
                    {
                        DataRow drRoom = dsHotelRoom.Tables[0].NewRow();
                        JObject jsoRoomType = (JObject)jsaRoomType[j];
                        drRoom["ROOMID"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomtypecode").Trim('"');
                        drRoom["ROOMNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomtypename").Trim('"');
                        drRoom["ROOMCODE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomtypecode").Trim('"');
                        drRoom["BEDNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "bedname").Trim('"');
                        drRoom["BOOKTYPE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "ratecode").Trim('"');
                        drRoom["BREAKFAST"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "breakfastnum").Trim('"');
                        drRoom["FTPLINE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "isnetwork").Trim('"');
                        drRoom["NETPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "thirdprice").Trim('"');
                        drRoom["VPPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"');

                        drRoom["PROTITLE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "prodesc").Trim('"');
                        drRoom["PROCONT"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "procontent").Trim('"');
                        dsHotelRoom.Tables[0].Rows.Add(drRoom);
                    }
                }
                else
                {
                    JObject jsoObjRoomType = (JObject)JsonConvert.DeserializeObject(oRoomTypeCode);
                    DataRow drRoom = dsHotelRoom.Tables[0].NewRow();
                    drRoom["ROOMID"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomtypecode").Trim('"');
                    drRoom["ROOMNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomtypename").Trim('"');
                    drRoom["ROOMCODE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomtypecode").Trim('"');
                    drRoom["BEDNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "bedname").Trim('"');
                    drRoom["BOOKTYPE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "ratecode").Trim('"');
                    drRoom["BREAKFAST"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "breakfastnum").Trim('"');
                    drRoom["FTPLINE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "isnetwork").Trim('"');
                    drRoom["NETPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "thirdprice").Trim('"');
                    drRoom["VPPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"');

                    drRoom["PROTITLE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "prodesc").Trim('"');
                    drRoom["PROCONT"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "procontent").Trim('"');
                    dsHotelRoom.Tables[0].Rows.Add(drRoom);
                }
            }

            // 酒店服务设施目的地取得
            string HotelAmenityUrl = JsonRequestURLBuilder.queryHotelAmenityByIDV2(dbParm.PlatForm, dbParm.HotelID);
            string strHotelAmenity = CommonCallWebUrl(HotelAmenityUrl);

            //解析json数据
            //JObject o = JObject.Parse(strJson);
            ////JObject oHotelBase = (JObject)o.SelectToken("result");
            //string oHotells = o.SelectToken("result").ToString();

            //JObject oh = JObject.Parse(oHotells);
            //string oHotelDetail = oh.SelectToken("hotellist").ToString();

            JObject oHotelBaseAmenity = JObject.Parse(strHotelAmenity);
            string strResAmenity = oHotelBaseAmenity.SelectToken("result").ToString();

            JObject oHotelAmenity = JObject.Parse(strResAmenity);

            string serviceDescZh = "";//酒店服务
            string businessDescZh = "";//商务设施
            string ApprDescZh = "";//小贴士
            //string FtDescZh = "";//目的地
            string oHotelDetail = (oHotelAmenity.SelectToken("hotelservice") != null) ? oHotelAmenity.SelectToken("hotelservice").ToString() : "";
            if (!String.IsNullOrEmpty(oHotelDetail))
            {
                //Array 
                JArray jsaA = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                for (int i = 0; i < jsaA.Count; i++)
                {
                    JObject jsoA = (JObject)jsaA[i];
                    serviceDescZh = serviceDescZh + JsonRequestURLBuilder.GetJsonStringValue(jsoA, "namezh").Trim('"') + " | ";
                }
                serviceDescZh = (serviceDescZh.Length > 0) ? serviceDescZh.Substring(0, serviceDescZh.Length - 3) : serviceDescZh;
            }
            drRow["HOTELSERVICE"] = serviceDescZh;

            oHotelDetail = (oHotelAmenity.SelectToken("businessamenity") != null) ? oHotelAmenity.SelectToken("businessamenity").ToString() : "";
            if (!String.IsNullOrEmpty(oHotelDetail))
            {
                //Array 
                JArray jsaS = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                for (int i = 0; i < jsaS.Count; i++)
                {
                    JObject jsoS = (JObject)jsaS[i];
                    businessDescZh = businessDescZh + JsonRequestURLBuilder.GetJsonStringValue(jsoS, "namezh").Trim('"') + " | ";
                }
                businessDescZh = (businessDescZh.Length > 0) ? businessDescZh.Substring(0, businessDescZh.Length - 3) : businessDescZh;
            }
            drRow["BUSSES"] = businessDescZh;

            oHotelDetail = (oHotelAmenity.SelectToken("hotelappr") != null) ? oHotelAmenity.SelectToken("hotelappr").ToString() : "";
            if (!String.IsNullOrEmpty(oHotelDetail))
            {
                //Array 
                JArray jsaR = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                for (int i = 0; i < jsaR.Count; i++)
                {
                    JObject jsoR = (JObject)jsaR[i];
                    ApprDescZh = ApprDescZh + JsonRequestURLBuilder.GetJsonStringValue(jsoR, "simpleapprzh").Trim('"') + ",";
                }
                ApprDescZh = ApprDescZh.Trim(',');
            }
            drRow["HOTELAPPR"] = ApprDescZh;

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

            // 客服电话信息取得
            string HotelServiceTelUrl = JsonRequestURLBuilder.queryHotelServiceTelV2(dbParm.PlatForm);
            string strHotelServiceTel = CommonCallWebUrl(HotelServiceTelUrl);
            string HotelServiceTel = "";
            JObject otel = JObject.Parse(strHotelServiceTel);
            string strbasetel = (otel.SelectToken("result") != null) ? otel.SelectToken("result").ToString() : "";
            if (!String.IsNullOrEmpty(strbasetel))
            {
                JObject obaseSys = JObject.Parse(strbasetel);
                string strsysinfo = (obaseSys.SelectToken("sysinfo") != null) ? obaseSys.SelectToken("sysinfo").ToString().Trim('"') : "";
                JObject obasetel = JObject.Parse(strsysinfo);
                HotelServiceTel = (obasetel.SelectToken("servicetel") != null) ? obasetel.SelectToken("servicetel").ToString().Trim('"') : "";
            }
            drRow["CUSTOMTEL"] = HotelServiceTel;
            dsHotelMain.Tables[0].Rows.Add(drRow);

            appcontentEntity.APPContentDBEntity[0].HotelMain = dsHotelMain;
            appcontentEntity.APPContentDBEntity[0].HotelImage = ayHotelImage;
            appcontentEntity.APPContentDBEntity[0].HotelRoom = dsHotelRoom;
            //appcontentEntity.APPContentDBEntity[0].HotelFtType = dsHotelFtType;
            return appcontentEntity;
        }

        public static APPContentEntity HotelFtDetailListSelectV2(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataSet dsResult = new DataSet();

            // 酒店服务设施目的地取得
            string FtType = dbParm.FtType;
            //string HotelAmenityUrl = JsonRequestURLBuilder.queryHotelAmenityByIDV2(dbParm.PlatForm, dbParm.HotelID);
            //string strHotelAmenity = CommonCallWebUrl(HotelAmenityUrl);

            ////解析json数据
            //JObject oHotelAmenity = JObject.Parse(strHotelAmenity);
            //JObject oHotelBaseAmenity = (JObject)oHotelAmenity.SelectToken("result");
            string HotelsByIDUrl = JsonRequestURLBuilder.queryHotelsByIDV3(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID, dbParm.HotelID);
            string strHotelMain = CommonCallWebUrl(HotelsByIDUrl);

            //解析json数据
            JObject oHotelMain = JObject.Parse(strHotelMain);

            //string oHotelDetail = oHotelBaseAmenity.SelectToken("destinations").ToString();
            //Array 
            //JArray jsaA = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("FTNAME");
            dsResult.Tables[0].Columns.Add("FTADDRESS");
            dsResult.Tables[0].Columns.Add("LONGITUDE");
            dsResult.Tables[0].Columns.Add("LATITUDE");

            string oRoomTypeCode = oHotelMain.SelectToken("result").SelectToken("destinations").ToString();
            int index0 = oRoomTypeCode.IndexOf("[");
            //List<HotelRoomTypeCode> liRoomType = new List<HotelRoomTypeCode>();
            if (index0 == 0)
            {
                JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                for (int j = 0; j < jsaRoomType.Count; j++)
                {
                    JObject jsoA = (JObject)jsaRoomType[j];
                    if (FtType.Equals(JsonRequestURLBuilder.GetJsonStringValue(jsoA, "typeid").Trim('"')))
                    {
                        DataRow drRow = dsResult.Tables[0].NewRow();
                        drRow["FTNAME"] = JsonRequestURLBuilder.GetJsonStringValue(jsoA, "namecn").Trim('"');
                        drRow["FTADDRESS"] = JsonRequestURLBuilder.GetJsonStringValue(jsoA, "addresscn").Trim('"');
                        drRow["LONGITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoA, "longitude").Trim('"');
                        drRow["LATITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoA, "latitude").Trim('"');
                        dsResult.Tables[0].Rows.Add(drRow);
                    }
                }
            }
            else
            {
                JObject jsoObj = (JObject)JsonConvert.DeserializeObject(oRoomTypeCode);
                if (FtType.Equals(JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "typeid").Trim('"')))
                {
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["FTNAME"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "namecn").Trim('"');
                    drRow["FTADDRESS"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "addresscn").Trim('"');
                    drRow["LONGITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "longitude").Trim('"');
                    drRow["LATITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "latitude").Trim('"');
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }

            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity HotelFtDetailListSelect(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataSet dsResult = new DataSet();

            // 酒店服务设施目的地取得
            string FtType = dbParm.FtType;
            string HotelAmenityUrl = JsonRequestURLBuilder.queryHotelAmenityByIDV2(dbParm.PlatForm, dbParm.HotelID);
            string strHotelAmenity = CommonCallWebUrl(HotelAmenityUrl);

            //解析json数据
            JObject oHotelAmenity = JObject.Parse(strHotelAmenity);
            JObject oHotelBaseAmenity = (JObject)oHotelAmenity.SelectToken("result");

            string oHotelDetail = oHotelBaseAmenity.SelectToken("destinations").ToString();
            //Array 
            JArray jsaA = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("FTNAME");
            dsResult.Tables[0].Columns.Add("FTADDRESS");
            dsResult.Tables[0].Columns.Add("LONGITUDE");
            dsResult.Tables[0].Columns.Add("LATITUDE");

            for (int i = 0; i < jsaA.Count; i++)
            {
                JObject jsoA = (JObject)jsaA[i];
                if (FtType.Equals(JsonRequestURLBuilder.GetJsonStringValue(jsoA, "typeid").Trim('"')))
                {
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["FTNAME"] = JsonRequestURLBuilder.GetJsonStringValue(jsoA, "namecn").Trim('"');
                    drRow["FTADDRESS"] = JsonRequestURLBuilder.GetJsonStringValue(jsoA, "addresscn").Trim('"');
                    drRow["LONGITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoA, "longitude").Trim('"');
                    drRow["LATITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoA, "latitude").Trim('"');
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }

            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity ApplyUnFullRoom(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            //Hashtable alRoom = QueryFullRoomList(appcontentEntity);
            string[] strRoomList = dbParm.RoomList.Split(',');
            string HotelID = dbParm.HotelID.Substring((dbParm.HotelID.IndexOf('[') + 1), (dbParm.HotelID.IndexOf(']') - 1));
            string RoomCode = "";
            //dbParm.HotelID, RoomCode, dbParm.PriceCode, dbParm.StartDTime, dbParm.EndDTime
            //{"roomTypeName":"单人房","roomTypeCode":"123","status":false,"isReserve":”0”}
            bool bFlag = true;
            string msg = string.Empty;
            foreach (string strRoomCode in strRoomList)
            {
                if (String.IsNullOrEmpty(strRoomCode))
                {
                    continue;
                }

                //if (alRoom.Count == 0 || !alRoom.ContainsKey(strRoomCode))
                //{
                //    msg = msg + strRoomCode + ",";
                //    bFlag = false;
                //}

                RoomCode = RoomCode + "{\"roomTypeName\":\"" + APPContentDA.HotelRoomNM(HotelID, strRoomCode) + "\"," + "\"roomTypeCode\":\"" + strRoomCode + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "},";
            }

            //if (!bFlag)
            //{
            //    appcontentEntity.ErrorMSG = msg;
            //    appcontentEntity.Result = 2;
            //    return appcontentEntity;
            //}

            RoomCode = (RoomCode.Length > 0) ? RoomCode.Substring(0, RoomCode.Length - 1) : RoomCode;
            string DataString = "";
            DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"guaid\":\"" + dbParm.Note1 + "\"," + "\"cxlid\":\"" + dbParm.Note2 + "\"," + "\"offsetunit\":\"" + dbParm.Offsetunit + "\",";

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
                appcontentEntity.Result = 1;
            }
            else
            {
                appcontentEntity.Result = 2;
            }

            return appcontentEntity;
        }

        public static APPContentEntity CreateSalesPlan(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
      
            string HotelID = dbParm.HotelID.Substring((dbParm.HotelID.IndexOf('[') + 1), (dbParm.HotelID.IndexOf(']') - 1));
            string RoomCode = "";

            string msg = string.Empty;
            RoomCode = RoomCode + "{\"roomTypeName\":\"" + dbParm.RoomName + "\"," + "\"roomTypeCode\":\"" + dbParm.RoomCode + "\"," + "\"status\":" + dbParm.RoomStatus + ",\"isReserve\":\"" + dbParm.IsReserve + "\"" + "}";
            string DataString = "";
            DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"whatDay\":\"" + dbParm.WeekList + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"guaid\":\"" + dbParm.Note1 + "\"," + "\"cxlid\":\"" + dbParm.Note2 + "\"," + "\"offsetunit\":\"" + dbParm.Offsetunit + "\"," + "\"effectHour\":\"" + dbParm.EffHour + "\",";

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
            DataString = (String.IsNullOrEmpty(dbParm.NetPrice)) ? DataString : DataString + "\"thirdPrice\":" + ConverDouble(dbParm.NetPrice) + ",";
            DataString = (String.IsNullOrEmpty(dbParm.Supplier)) ? DataString : DataString + "\"source\":\"" + dbParm.Supplier + "\",";
            DataString = DataString + "\"updateUser\":\"" + dbParm.UpdateUser + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.applyHotelFullRoomV2();

            string strHotelFullRoom = CommonCallWebUrl(HotelFullRoomUrl + DataString);

            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "result").Trim('"')))
            {
                appcontentEntity.Result = 1;
                appcontentEntity.ErrorMSG = "保存成功！";
            }
            else
            {
                appcontentEntity.Result = 2;
                appcontentEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
            }

            return appcontentEntity;
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

        public static APPContentEntity ApplyFullRoom(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            Hashtable alRoom = QueryFullRoomList(appcontentEntity);
            string[] strRoomList = dbParm.RoomList.Split(',');
            string HotelID = dbParm.HotelID.Substring((dbParm.HotelID.IndexOf('[') + 1), (dbParm.HotelID.IndexOf(']') - 1));
            string RoomCode = "";
            //dbParm.HotelID, RoomCode, dbParm.PriceCode, dbParm.StartDTime, dbParm.EndDTime
            //{"roomTypeName":"单人房","roomTypeCode":"123","status":false,"isReserve":”0”}
            bool bFlag = true;
            string msg = string.Empty;
            foreach (string strRoomCode in strRoomList)
            {
                if (String.IsNullOrEmpty(strRoomCode))
                {
                    continue;
                }

                if (alRoom.Count == 0 || !alRoom.ContainsKey(strRoomCode))
                {
                    msg = msg + strRoomCode + ",";
                    bFlag = false;
                }

                RoomCode = RoomCode + "{\"roomTypeName\":\"" + APPContentDA.HotelRoomNM(HotelID, strRoomCode) + "\"," + "\"roomTypeCode\":\"" + strRoomCode + "\"," + "\"status\":" + "true" + ",\"isReserve\":\"" + "0" + "\"" + "},";
            }

            if (!bFlag)
            {
                appcontentEntity.ErrorMSG = msg;
                appcontentEntity.Result = 2;
                return appcontentEntity;
            }

            RoomCode = (RoomCode.Length > 0) ? RoomCode.Substring(0, RoomCode.Length - 1) : RoomCode;
            string DataString = "{\"moneyType\":\"" + "CHY" + "\"," + "\"hotelId\":\"" + HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"roomNum\":" + "0" + "," + "\"whatDay\":\"" + "1,2,3,4,5,6,7" + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"lmroom\":[" + RoomCode + "]," + "\"updateUser\":\"" + dbParm.UpdateUser + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.applyHotelFullRoomV2();

            string strHotelFullRoom = CommonCallWebUrl(HotelFullRoomUrl + DataString);

            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "result").Trim('"')))
            {
                appcontentEntity.Result = 1;
            }
            else
            {
                string ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "code").Trim('"');
                appcontentEntity.ErrorMSG = ("1001".Equals(ErrorMSG)) ? "酒店满房标记失败   指定的房型在该时间区间内存在无销售计划，请确认！" : "";
                appcontentEntity.Result = 3;
            }

            return appcontentEntity;
        }

        public static Hashtable QueryFullRoomList(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            Hashtable alRoom = new Hashtable();
            try
            {
                string url = JsonRequestURLBuilder.queryHotelFullRoomV2();
                string HotelID = dbParm.HotelID.Substring((dbParm.HotelID.IndexOf('[') + 1), (dbParm.HotelID.IndexOf(']') - 1));
                string DataString = "{\"hotelId\":\"" + HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime +"\"," + "\"platformCode\":\"" + "CMS" + "\"}";
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

        public static APPContentEntity QueryFullRoom(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELROOMCODE"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELROOMNM"));
            try
            {
                string url = JsonRequestURLBuilder.queryHotelFullRoomV2();

                string DataString = "{\"hotelId\":\"" + dbParm.HotelID + "\"," + "\"rateCode\":\"" + dbParm.PriceCode + "\"," + "\"beginDate\":\"" + dbParm.StartDTime + "\"," + "\"endDate\":\"" + dbParm.EndDTime + "\"," + "\"platformCode\":\"" + "CMS" + "\"}";
                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url + DataString, "");
                ArrayList alRoom = new ArrayList();
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
                    roomtypecode= JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypecode").Trim('"');
                    roomtypename = JsonRequestURLBuilder.GetJsonStringValue(jso, "roomtypename").Trim('"');

                    if (String.IsNullOrEmpty(roomtypecode) || alRoom.Contains(roomtypecode))
                    {
                        continue;
                    }

                    alRoom.Add(roomtypecode);
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["HOTELROOMCODE"] = roomtypecode;
                    drRow["HOTELROOMNM"] = roomtypename;
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }
            catch
            {

            }
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
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

        public static APPContentEntity HotelDetailListSelectV2(APPContentEntity appcontentEntity)
        {
            DataSet dsHotelMain = new DataSet();
            dsHotelMain.Tables.Add(new DataTable());
            dsHotelMain.Tables[0].Columns.Add("HOTELID");
            dsHotelMain.Tables[0].Columns.Add("HOTELNM");
            dsHotelMain.Tables[0].Columns.Add("HOTELGROUP");
            dsHotelMain.Tables[0].Columns.Add("ADDRESS");
            dsHotelMain.Tables[0].Columns.Add("LONGITUDE");
            dsHotelMain.Tables[0].Columns.Add("LATITUDE");
            dsHotelMain.Tables[0].Columns.Add("HOTELDES");
            dsHotelMain.Tables[0].Columns.Add("HOTELAPPR");
            dsHotelMain.Tables[0].Columns.Add("HOTELSERVICE");
            dsHotelMain.Tables[0].Columns.Add("BUSSES");
            dsHotelMain.Tables[0].Columns.Add("CUSTOMTEL");
            dsHotelMain.Tables[0].Columns.Add("TRADEAREA");
            dsHotelMain.Tables[0].Columns.Add("PRODESC");
            dsHotelMain.Tables[0].Columns.Add("PROCONTENT");
            dsHotelMain.Tables[0].Columns.Add("HTPPATH");

            DataRow drRow = dsHotelMain.Tables[0].NewRow();

            ArrayList ayHotelImage = new ArrayList();

            DataSet dsHotelRoom = new DataSet();
            dsHotelRoom.Tables.Add(new DataTable());
            dsHotelRoom.Tables[0].Columns.Add("ROOMID");
            dsHotelRoom.Tables[0].Columns.Add("ROOMNM");
            dsHotelRoom.Tables[0].Columns.Add("ROOMCODE");
            dsHotelRoom.Tables[0].Columns.Add("BEDNM");
            dsHotelRoom.Tables[0].Columns.Add("BOOKTYPE");
            dsHotelRoom.Tables[0].Columns.Add("BREAKFAST");
            dsHotelRoom.Tables[0].Columns.Add("FTPLINE");
            dsHotelRoom.Tables[0].Columns.Add("NETPRICE");
            dsHotelRoom.Tables[0].Columns.Add("VPPRICE");
            dsHotelRoom.Tables[0].Columns.Add("PROTITLE");
            dsHotelRoom.Tables[0].Columns.Add("PROCONT");

            //DataSet dsHotelFtType = new DataSet();

            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            string phoneUser = ConfigurationManager.AppSettings["phoneUser"];    //全局User
            string phoneKey = ConfigurationManager.AppSettings["phoneKey"];      //全局Key
            string checkInDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            // 酒店详情取得
            string HotelsByIDUrl = JsonRequestURLBuilder.queryHotelsByIDV3(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID, dbParm.HotelID);
            string strHotelMain = CommonCallWebUrl(HotelsByIDUrl);

            //解析json数据
            JObject oHotelMain = JObject.Parse(strHotelMain);
            //JObject oHotelBase = (JObject)o.SelectToken("result");
            //string osHotelMain = oHotelMain.SelectToken("result").ToString();

            //JObject ohls = JObject.Parse(osHotelMain);
            string oHotelsDetail = oHotelMain.SelectToken("result").SelectToken("hotelInfo").ToString();

            //Array 
            //JArray jsaHotels = (JArray)JsonConvert.DeserializeObject(oHotelsDetail);
            string strHotelid = "";
            if (oHotelsDetail.Length > 0)
            {
                JArray jsaHotels = (JArray)JsonConvert.DeserializeObject("[" + oHotelsDetail + "]");
                JObject jsoHotels = (JObject)jsaHotels[0];

                strHotelid = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotelid").Trim('"');
                drRow["HOTELID"] = strHotelid;
                drRow["HOTELNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotelname").Trim('"');
                drRow["HOTELGROUP"] = "";
                drRow["ADDRESS"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hoteladdr").Trim('"');
                drRow["LONGITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotellongitude").Trim('"');
                drRow["LATITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "hotellatitude").Trim('"');
                drRow["HOTELDES"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "simpledesc").Trim('"');
                //drRow["TRADEAREA"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "tradearea").Trim('"');

                string oHotelspromotion = oHotelMain.SelectToken("result").SelectToken("hotelPromotion").ToString();
                JObject jsoHotelsPro = (JObject)((JArray)JsonConvert.DeserializeObject("[" + oHotelspromotion+"]"))[0];
                drRow["PRODESC"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "prodesc").Trim('"');
                drRow["PROCONTENT"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "procontent").Trim('"');
                drRow["HTPPATH"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "httppath").Trim('"');


                string oRoomTypeCode = oHotelMain.SelectToken("result").SelectToken("roomPlans").ToString();
                int index0 = oRoomTypeCode.IndexOf("[");
                //List<HotelRoomTypeCode> liRoomType = new List<HotelRoomTypeCode>();
                if (index0 == 0)
                {
                    JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                    for (int j = 0; j < jsaRoomType.Count; j++)
                    {
                        DataRow drRoom = dsHotelRoom.Tables[0].NewRow();
                        JObject jsoRoomType = (JObject)jsaRoomType[j];
                        drRoom["ROOMID"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"');
                        drRoom["ROOMNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomname").Trim('"');
                        drRoom["ROOMCODE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"');
                        drRoom["BEDNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "bedname").Trim('"');
                        drRoom["BOOKTYPE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "pricecode").Trim('"');
                        drRoom["BREAKFAST"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "breakfastnum").Trim('"');
                        drRoom["FTPLINE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "hasnet").Trim('"');
                        drRoom["NETPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "thirdprice").Trim('"');
                        drRoom["VPPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "price").Trim('"');

                        //drRoom["PROTITLE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "prodesc").Trim('"');
                        //drRoom["PROCONT"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "procontent").Trim('"');
                        dsHotelRoom.Tables[0].Rows.Add(drRoom);
                    }
                }
                else
                {
                    JObject jsoObjRoomType = (JObject)JsonConvert.DeserializeObject(oRoomTypeCode);
                    DataRow drRoom = dsHotelRoom.Tables[0].NewRow();
                    drRoom["ROOMID"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomcode").Trim('"');
                    drRoom["ROOMNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomname").Trim('"');
                    drRoom["ROOMCODE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomcode").Trim('"');
                    drRoom["BEDNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "bedname").Trim('"');
                    drRoom["BOOKTYPE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "pricecode").Trim('"');
                    drRoom["BREAKFAST"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "breakfastnum").Trim('"');
                    drRoom["FTPLINE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "hasnet").Trim('"');
                    drRoom["NETPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "thirdprice").Trim('"');
                    drRoom["VPPRICE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "price").Trim('"');

                    //drRoom["PROTITLE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "prodesc").Trim('"');
                    //drRoom["PROCONT"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "procontent").Trim('"');
                    dsHotelRoom.Tables[0].Rows.Add(drRoom);
                }
            }

            // 酒店服务设施目的地取得
            //string HotelAmenityUrl = JsonRequestURLBuilder.queryHotelAmenityByIDV2(dbParm.PlatForm, dbParm.HotelID);
            //string strHotelAmenity = CommonCallWebUrl(HotelAmenityUrl);

            //解析json数据
            //JObject o = JObject.Parse(strJson);
            ////JObject oHotelBase = (JObject)o.SelectToken("result");
            //string oHotells = o.SelectToken("result").ToString();

            //JObject oh = JObject.Parse(oHotells);
            //string oHotelDetail = oh.SelectToken("hotellist").ToString();

            //JObject oHotelBaseAmenity = JObject.Parse(strHotelAmenity);
            //string strResAmenity = oHotelBaseAmenity.SelectToken("result").ToString();

            //JObject oHotelAmenity = JObject.Parse(strResAmenity);

            string serviceDescZh = "";//酒店服务
            string businessDescZh = "";//商务设施
            string ApprDescZh = "";//小贴士
            //string FtDescZh = "";//目的地
            string oHotelDetail = oHotelMain.SelectToken("result").SelectToken("hotelService").ToString();//(oHotelAmenity.SelectToken("hotelservice") != null) ? oHotelAmenity.SelectToken("hotelservice").ToString() : "";
            if (!String.IsNullOrEmpty(oHotelDetail))
            {
                //Array 
                JArray jsaA = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                for (int i = 0; i < jsaA.Count; i++)
                {
                    JObject jsoA = (JObject)jsaA[i];
                    serviceDescZh = serviceDescZh + JsonRequestURLBuilder.GetJsonStringValue(jsoA, "namezh").Trim('"') + " | ";
                }
                serviceDescZh = (serviceDescZh.Length > 0) ? serviceDescZh.Substring(0, serviceDescZh.Length - 3) : serviceDescZh;
            }
            drRow["HOTELSERVICE"] = serviceDescZh;

            oHotelDetail = oHotelMain.SelectToken("result").SelectToken("businessAmenity").ToString();
            if (!String.IsNullOrEmpty(oHotelDetail))
            {
                //Array 
                JArray jsaS = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                for (int i = 0; i < jsaS.Count; i++)
                {
                    JObject jsoS = (JObject)jsaS[i];
                    businessDescZh = businessDescZh + JsonRequestURLBuilder.GetJsonStringValue(jsoS, "namezh").Trim('"') + " | ";
                }
                businessDescZh = (businessDescZh.Length > 0) ? businessDescZh.Substring(0, businessDescZh.Length - 3) : businessDescZh;
            }
            drRow["BUSSES"] = businessDescZh;

            oHotelDetail = oHotelMain.SelectToken("result").SelectToken("hotelAppr").ToString();
            if (!String.IsNullOrEmpty(oHotelDetail))
            {
                //Array 
                JArray jsaR = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                for (int i = 0; i < jsaR.Count; i++)
                {
                    JObject jsoR = (JObject)jsaR[i];
                    ApprDescZh = ApprDescZh + JsonRequestURLBuilder.GetJsonStringValue(jsoR, "simpleapprzh").Trim('"') + ",";
                }
                ApprDescZh = ApprDescZh.Trim(',');
            }
            drRow["HOTELAPPR"] = ApprDescZh;

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

            // 客服电话信息取得
            string HotelServiceTelUrl = JsonRequestURLBuilder.queryHotelServiceTelV2(dbParm.PlatForm);
            string strHotelServiceTel = CommonCallWebUrl(HotelServiceTelUrl);
            string HotelServiceTel = "";
            JObject otel = JObject.Parse(strHotelServiceTel);
            string strbasetel = (otel.SelectToken("result") != null) ? otel.SelectToken("result").ToString() : "";
            if (!String.IsNullOrEmpty(strbasetel))
            {
                JObject obaseSys = JObject.Parse(strbasetel);
                string strsysinfo = (obaseSys.SelectToken("sysinfo") != null) ? obaseSys.SelectToken("sysinfo").ToString().Trim('"') : "";
                JObject obasetel = JObject.Parse(strsysinfo);
                HotelServiceTel = (obasetel.SelectToken("servicetel") != null) ? obasetel.SelectToken("servicetel").ToString().Trim('"') : "";
            }
            drRow["CUSTOMTEL"] = HotelServiceTel;
            dsHotelMain.Tables[0].Rows.Add(drRow);

            appcontentEntity.APPContentDBEntity[0].HotelMain = dsHotelMain;
            appcontentEntity.APPContentDBEntity[0].HotelImage = ayHotelImage;
            appcontentEntity.APPContentDBEntity[0].HotelRoom = dsHotelRoom;
            //appcontentEntity.APPContentDBEntity[0].HotelFtType = dsHotelFtType;
            return appcontentEntity;
        }

    }
}