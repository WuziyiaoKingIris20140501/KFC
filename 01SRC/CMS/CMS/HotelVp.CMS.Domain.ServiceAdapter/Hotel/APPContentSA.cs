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

namespace HotelVp.CMS.Domain.ServiceAdapter
{
    public abstract class APPContentSA
    {
        public static APPContentEntity CommonSelect(APPContentEntity appcontentEntity)
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("cityid"));
            dsResult.Tables[0].Columns.Add(new DataColumn("cityNM"));
            try
            {
                string url = JsonRequestURLBuilder.getSearchCityUrl();
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

        public static APPContentEntity HotelListSelect(APPContentEntity appcontentEntity)
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELNM"));
            //dsResult.Tables[0].Columns.Add(new DataColumn("PICTURE"));
            //dsResult.Tables[0].Columns.Add(new DataColumn("TAGNM"));
            //dsResult.Tables[0].Columns.Add(new DataColumn("STARRATE"));
            //dsResult.Tables[0].Columns.Add(new DataColumn("LOWPRICE"));
            //dsResult.Tables[0].Columns.Add(new DataColumn("LONGITUDE"));
            //dsResult.Tables[0].Columns.Add(new DataColumn("LATITUDE"));

            try
            {
                APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
                string url = JsonRequestURLBuilder.getSearchHotelListUrl(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID);

                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url, "");

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
                //string strPicture = string.Empty;
                //string strTradearea = string.Empty;
                //string strStarcode = string.Empty;
                //string strTwoprice = string.Empty;
                //string strLongitude = string.Empty;
                //string strLatitude = string.Empty;
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
                        strHotelname = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelname").Trim('"');
                        //strPicture = JsonRequestURLBuilder.GetJsonStringValue(jso, "picture").Trim('"');
                        //strTradearea = JsonRequestURLBuilder.GetJsonStringValue(jso, "tradearea").Trim('"');
                        //strStarcode = JsonRequestURLBuilder.GetJsonStringValue(jso, "starcode").Trim('"') + "[" + JsonRequestURLBuilder.GetJsonStringValue(jso, "stardesc").Trim('"') + "]";
                        //strLongitude = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotellongitude").Trim('"');
                        //strLatitude = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotellatitude").Trim('"');

                        //string oRoomTypeCode = jso.SelectToken("listlmplanpromotion").ToString();

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
                        //drRow["PICTURE"] = strPicture;
                        //drRow["TAGNM"] = strTradearea;
                        //drRow["STARRATE"] = strStarcode;
                        //drRow["LOWPRICE"] = decTwoprice;
                        //drRow["LONGITUDE"] = strLongitude;
                        //drRow["LATITUDE"] = strLatitude;
                        dsResult.Tables[0].Rows.Add(drRow);
                    }
                }
                else
                {
                    JObject jsoObj = (JObject)JsonConvert.DeserializeObject(oHotelDetail);

                    strHotelid = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotelid").Trim('"');
                    strHotelname = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotelname").Trim('"');
                    //strPicture = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "picture").Trim('"');
                    //strTradearea = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "tradearea").Trim('"');
                    //strStarcode = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "starcode").Trim('"') + "[" + JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "stardesc").Trim('"') + "]";
                    //strLongitude = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotellongitude").Trim('"');
                    //strLatitude = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotellatitude").Trim('"');

                    //string oRoomTypeCode = jsoObj.SelectToken("listlmplanpromotion").ToString();
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
                    //drRow["PICTURE"] = strPicture;
                    //drRow["TAGNM"] = strTradearea;
                    //drRow["STARRATE"] = strStarcode;
                    //drRow["LOWPRICE"] = decTwoprice;
                    //drRow["LONGITUDE"] = strLongitude;
                    //drRow["LATITUDE"] = strLatitude;
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
                string url = JsonRequestURLBuilder.getSearchHotelListUrl(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID);

                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(url, "");

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
                decimal decTwoprice = 0;
                decimal decPriceTemp = 0;

                if (index == 0)
                {
                    //Array 
                    JArray jsa = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                    for (int i = 0; i < jsa.Count; i++)
                    {
                        JObject jso = (JObject)jsa[i];

                        strHotelid = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelid").Trim('"');
                        strHotelname = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelname").Trim('"');
                        strPicture = JsonRequestURLBuilder.GetJsonStringValue(jso, "picture").Trim('"');
                        strTradearea = JsonRequestURLBuilder.GetJsonStringValue(jso, "tradearea").Trim('"');
                        strStarcode = JsonRequestURLBuilder.GetJsonStringValue(jso, "starcode").Trim('"') + "[" + JsonRequestURLBuilder.GetJsonStringValue(jso, "stardesc").Trim('"') + "]";
                        strLongitude = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotellongitude").Trim('"');
                        strLatitude = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotellatitude").Trim('"');

                        string oRoomTypeCode = jso.SelectToken("listlmplanpromotion").ToString();

                        int index0 = oRoomTypeCode.IndexOf("[");
                        //List<HotelRoomTypeCode> liRoomType = new List<HotelRoomTypeCode>();

                        decTwoprice = 0;
                        decPriceTemp = 0;
                        if (index0 == 0)
                        {
                            JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                            for (int j = 0; j < jsaRoomType.Count; j++)
                            {
                                JObject jsoRoomType = (JObject)jsaRoomType[j];
                                decPriceTemp = (!String.IsNullOrEmpty(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"'))) ? decimal.Parse(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"')) : 0;

                                if (j == 0)
                                {
                                    decTwoprice = decPriceTemp;
                                }
                                else if (j > 0 && decPriceTemp < decTwoprice)
                                {
                                    decTwoprice = decPriceTemp;
                                }

                                //HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "hotelid"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "ratecode"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomname"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomnum"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "thirdpartprice"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice"));
                                //liRoomType.Add(saRoomType);
                            }
                        }
                        else
                        {
                            JObject jsoObjRoomType = (JObject)JsonConvert.DeserializeObject(oRoomTypeCode);
                            decTwoprice = (!String.IsNullOrEmpty(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"'))) ? decimal.Parse(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"')) : 0;
                            //HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "hotelid"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "ratecode"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomcode"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomname"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomnum"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "thirdpartprice"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice"));
                            //liRoomType.Add(saRoomType);
                        }

                        DataRow drRow = dsResult.Tables[0].NewRow();
                        drRow["HOTELID"] = strHotelid;
                        drRow["HOTELNM"] = strHotelname;
                        drRow["PICTURE"] = strPicture;
                        drRow["TAGNM"] = strTradearea;
                        drRow["STARRATE"] = strStarcode;
                        drRow["LOWPRICE"] = decTwoprice;
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

                    string oRoomTypeCode = jsoObj.SelectToken("listlmplanpromotion").ToString();
                    int index0 = oRoomTypeCode.IndexOf("[");
                    //List<HotelRoomTypeCode> liRoomType = new List<HotelRoomTypeCode>();

                    decTwoprice = 0;
                    decPriceTemp = 0;
                    if (index0 == 0)
                    {
                        JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                        for (int j = 0; j < jsaRoomType.Count; j++)
                        {
                            JObject jsoRoomType = (JObject)jsaRoomType[j];
                            decPriceTemp = (!String.IsNullOrEmpty(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"'))) ? decimal.Parse(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"')) : 0;

                            if (j == 0)
                            {
                                decTwoprice = decPriceTemp;
                            }
                            else if (j > 0 && decPriceTemp < decTwoprice)
                            {
                                decTwoprice = decPriceTemp;
                            }
                            //HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "hotelid"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "ratecode"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomname"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomnum"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "thirdpartprice"), JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice"));
                            //liRoomType.Add(saRoomType);
                        }
                    }
                    else
                    {
                        JObject jsoObjRoomType = (JObject)JsonConvert.DeserializeObject(oRoomTypeCode);
                        decTwoprice = (!String.IsNullOrEmpty(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"'))) ? decimal.Parse(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice").Trim('"')) : 0;
                        //HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "hotelid"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "ratecode"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomcode"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomname"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "roomnum"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "thirdpartprice"), JsonRequestURLBuilder.GetJsonStringValue(jsoObjRoomType, "twoprice"));
                        //liRoomType.Add(saRoomType);
                    }

                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["HOTELID"] = strHotelid;
                    drRow["HOTELNM"] = strHotelname;
                    drRow["PICTURE"] = strPicture;
                    drRow["TAGNM"] = strTradearea;
                    drRow["STARRATE"] = strStarcode;
                    drRow["LOWPRICE"] = decTwoprice;
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
            string HotelsByIDUrl = JsonRequestURLBuilder.queryHotelsByID(dbParm.CityID, dbParm.PlatForm, dbParm.TypeID);
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

                JObject jsoHotelsPro = (JObject)((JArray)JsonConvert.DeserializeObject(JsonRequestURLBuilder.GetJsonStringValue(jsoHotels, "listhotelpromotionrs")))[0];
                drRow["PRODESC"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "prodesc").Trim('"');
                drRow["PROCONTENT"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "procontent").Trim('"');
                drRow["HTPPATH"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelsPro, "htppath").Trim('"');

                string oRoomTypeCode = jsoHotels.SelectToken("listlmplanpromotion").ToString();
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

            //JObject oHotelMain = JObject.Parse(strHotelMain);
            //string oHotelDetail = oHotelMain.SelectToken("HotelBaseInfoRS[0].hotelBaseInfoExt").ToString();
            //JObject jsoObj = (JObject)JsonConvert.DeserializeObject(oHotelDetail);

            //drRow["HOTELID"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotelid").Trim('"');
            //drRow["HOTELNM"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hotelname").Trim('"');
            //drRow["HOTELGROUP"] = "";
            //drRow["ADDRESS"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "hoteladdr").Trim('"');
            //drRow["LONGITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "longitude").Trim('"');
            //drRow["LATITUDE"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "latitude").Trim('"');
            //drRow["HOTELDES"] = JsonRequestURLBuilder.GetJsonStringValue(jsoObj, "simpledesc").Trim('"');

            //string oRoomTypeCode = jsoObj.SelectToken("lstroomtypecode").ToString();

            //JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
            //for (int j = 0; j < jsaRoomType.Count; j++)
            //{
            //    JObject jsoRoomType = (JObject)jsaRoomType[j];
            //    HotelRoomTypeCode saRoomType = new HotelRoomTypeCode(
            //        JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "hotelid").Trim('"'),
            //        JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "ratecode").Trim('"'),
            //        JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"'),
            //        JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomname").Trim('"'),
            //        JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomnum").Trim('"'),
            //        JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "thirdpartprice").Trim('"'),
            //        JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "twoprice").Trim('"'));
            //    liRoomType.Add(saRoomType);
            //}


            // 酒店服务设施目的地取得
            string HotelAmenityUrl = JsonRequestURLBuilder.queryHotelAmenityByID(dbParm.HotelID);
            string strHotelAmenity = CommonCallWebUrl(HotelAmenityUrl);

            //解析json数据
            JObject oHotelAmenity = JObject.Parse(strHotelAmenity);

            //string oAmenitys = oHotelAmenity.SelectToken("result").ToString();

            //JObject oA = JObject.Parse(oAmenitys);

            string serviceDescZh = "";//酒店服务
            string businessDescZh = "";//商务设施
            string ApprDescZh = "";//小贴士
            //string FtDescZh = "";//目的地
            string oHotelDetail = (oHotelAmenity.SelectToken("result[0].hotelService") != null) ? oHotelAmenity.SelectToken("result[0].hotelService").ToString() : "";
            if (!String.IsNullOrEmpty(oHotelDetail))
            {
                //Array 
                JArray jsaA = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                for (int i = 0; i < jsaA.Count; i++)
                {
                    JObject jsoA = (JObject)jsaA[i];
                    serviceDescZh = serviceDescZh + JsonRequestURLBuilder.GetJsonStringValue(jsoA, "desczh").Trim('"') + " | ";
                }
                serviceDescZh = (serviceDescZh.Length > 0) ? serviceDescZh.Substring(0, serviceDescZh.Length - 3) : serviceDescZh;
            }
            drRow["HOTELSERVICE"] = serviceDescZh;

            oHotelDetail = (oHotelAmenity.SelectToken("result[0].businessAmenity") != null ) ? oHotelAmenity.SelectToken("result[0].businessAmenity").ToString() : "";
            if (!String.IsNullOrEmpty(oHotelDetail))
            {
                //Array 
                JArray jsaS = (JArray)JsonConvert.DeserializeObject(oHotelDetail);

                for (int i = 0; i < jsaS.Count; i++)
                {
                    JObject jsoS = (JObject)jsaS[i];
                    businessDescZh = businessDescZh + JsonRequestURLBuilder.GetJsonStringValue(jsoS, "desczh").Trim('"') + " | ";
                }
                businessDescZh = (businessDescZh.Length > 0) ? businessDescZh.Substring(0, businessDescZh.Length - 3) : businessDescZh;
            }
            drRow["BUSSES"] = businessDescZh;

            oHotelDetail = (oHotelAmenity.SelectToken("result[0].hotelAppr") != null) ? oHotelAmenity.SelectToken("result[0].hotelAppr").ToString() : "";
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
            string HotelImagesUrl = JsonRequestURLBuilder.queryHotelImages(dbParm.HotelID, "200");
            string strHotelImages = CommonCallWebUrl(HotelImagesUrl);
            JObject oimage = JObject.Parse(strHotelImages);
            JObject oHotelImages = (JObject)oimage.SelectToken("HotelImageRS[0]");

            string oHotelImagesDetail = (oimage.SelectToken("HotelImageRS[0].gallery")!= null) ? oimage.SelectToken("HotelImageRS[0].gallery").ToString() : "";
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
            string HotelServiceTelUrl = JsonRequestURLBuilder.queryHotelServiceTel();
            string strHotelServiceTel = CommonCallWebUrl(HotelServiceTelUrl);

            JObject otel = JObject.Parse(strHotelServiceTel);
            string HotelServiceTel = (otel.SelectToken("result[0].serviceTel") != null) ? otel.SelectToken("result[0].serviceTel").ToString().Trim('"') : "";

            drRow["CUSTOMTEL"] = HotelServiceTel;
            dsHotelMain.Tables[0].Rows.Add(drRow);

            appcontentEntity.APPContentDBEntity[0].HotelMain = dsHotelMain;
            appcontentEntity.APPContentDBEntity[0].HotelImage = ayHotelImage;
            appcontentEntity.APPContentDBEntity[0].HotelRoom = dsHotelRoom;
            //appcontentEntity.APPContentDBEntity[0].HotelFtType = dsHotelFtType;
            return appcontentEntity;
        }

        public static APPContentEntity HotelFtDetailListSelect(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataSet dsResult = new DataSet();

            // 酒店服务设施目的地取得
            string FtType = dbParm.FtType;
            string HotelAmenityUrl = JsonRequestURLBuilder.queryHotelAmenityByID(dbParm.HotelID);
            string strHotelAmenity = CommonCallWebUrl(HotelAmenityUrl);

            //解析json数据
            JObject oHotelAmenity = JObject.Parse(strHotelAmenity);

            string oHotelDetail = oHotelAmenity.SelectToken("result[0].destService").ToString();
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