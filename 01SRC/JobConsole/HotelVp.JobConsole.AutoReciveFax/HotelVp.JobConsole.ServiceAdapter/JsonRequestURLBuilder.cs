using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Runtime.Serialization.Json;
using System.Xml;

using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;

namespace HotelVp.JobConsole.ServiceAdapter
{
/// <summary>
///JsonRequestURLBuilder 的摘要说明
/// </summary>
    public class JsonRequestURLBuilder
    {
        public static string mapiUrl = ConfigurationManager.AppSettings["mapiUrl"];//mapi URL地址
        public static string lmApiUrl = ConfigurationManager.AppSettings["lmapiUrl"];//lmapi URL地址

        public static string lmApiUrlV2 = ConfigurationManager.AppSettings["lmApiUrlV2"];//lmapiV2 URL地址
        public static string lmScApiUrlV2 = ConfigurationManager.AppSettings["lmScApiUrlV2"];//lmScApiUrlV2 URL地址

        public static string lmMsgApiUrlV2 = ConfigurationManager.AppSettings["lmMsgApiUrlV2"];//lmScApiUrlV2 URL地址

        public JsonRequestURLBuilder()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string queryHotelsBySUPIDV2(string HotelID, string InDate, string OutDate)
        {
            string url = lmApiUrlV2 + "/content/hotelDetails.json?useCode=IOS&useCodeVersion=2.0&clientCode=HOTELVP&hotelId={0}&imgSize=100&apiVersion=2.7&inDate={1}&outDate={2}";
            return string.Format(url, HotelID, InDate, OutDate);
        }

        public static string applySendMsgV2()
        {
            string url = lmMsgApiUrlV2 + "/sms.json";
            return url;
        }

        public static string applyHotelFullRoomListV2()
        {
            string url = lmScApiUrlV2 + "/management/sc/planBatch.json";
            return url;
        }
        public static string applyHotelFullRoomV2()
        {
            string url = lmScApiUrlV2 + "/management/sc/plan.json?method=saveOrUpdate&data=";//GetUrlFromXml("ApplyHotelFullRoomV2");
            return url;
        }
         public static string queryHotelFullRoomV2()
        {
            string url = lmScApiUrlV2 + "/management/sc/plan.json?method=query&data=";//GetUrlFromXml("queryHotelFullRoomV2");
            return url;
        }

        //获取城市信息的URL.
        public static string getSearchCityUrl()
        {
            string url = lmApiUrl +"/show/getSupCity.json?clientCode=&useCode=IOS";// "/show/getSupCity.json?clientCode=&useCode=IOS";
            return url;
        }

        public static string getSearchCityUrlV2(string PlatForm)
        {
            string baseUrl = "/content/cityList.json?useCode={0}&useCodeVersion=0&clientCode=HOTELVP";// GetUrlFromXml("SearchCityUrlV2");
            string url = lmApiUrlV2 + String.Format(baseUrl, PlatForm);
            return url;
        }

        //获取查询酒店列表信息的URL.
        public static string getSearchHotelListUrl(string CityID, string PlatForm, string TypeID)
        {
            string baseUrl = "/show/hotel-list-v2.json?sysSource={0}&imgSize=200&cityId={1}&version=0&isTest=true&isToday={2}";// "/show/hotel-list-v2.json?sysSource={0}&imgSize=200&cityId={1}&version=0&isTest=true&isToday={2}";
            string url = lmApiUrl + String.Format(baseUrl, PlatForm, CityID, ("1".Equals(TypeID) ? "true" : "false"));
            return url;
        }

        public static string getSearchHotelListUrlV2(string CityID, string PlatForm, string TypeID)
        {
            string baseUrl = "/content/hotelList.json?useCode={0}&useCodeVersion=0&clientCode=HOTELVP&imgSize=200&cityId={1}&hotelListVersion=0&isTest=true&isToday={2}";// GetUrlFromXml("getSearchHotelListUrlV2");
            string url = lmApiUrlV2 + String.Format(baseUrl, PlatForm, CityID, ("1".Equals(TypeID) ? "true" : "false"));
            return url;
        }

        //获取酒店详细信息的URL.
        public static string queryHotelsByID(string CityID, string PlatForm, string TypeID)
        {
            string url = lmApiUrl + "/show/hotel-list-v3.json?sysSource={0}&imgSize=200&cityId={1}&version=0&isTest=true&isToday={2}";//mapiUrl + "/mapi/rest/lm/hotelbaseHotelid/{0}/{1}/100/{2}.json?user={3}&key={4}";
            return string.Format(url, PlatForm, CityID, ("1".Equals(TypeID) ? "true" : "false"));
        }

        public static string queryHotelsByIDV2(string CityID, string PlatForm, string TypeID)
        {
            string url = lmApiUrlV2 + "/content/hotelList.json?useCode={0}&useCodeVersion=0&clientCode=HOTELVP&imgSize=200&cityId={1}&hotelListVersion=0&isTest=true&isToday={2}";// GetUrlFromXml("SearchHotelsByIDUrlV2");//mapiUrl + "/mapi/rest/lm/hotelbaseHotelid/{0}/{1}/100/{2}.json?user={3}&key={4}";
            return string.Format(url, PlatForm, CityID, ("1".Equals(TypeID) ? "true" : "false"));
        }

        //获取酒店服务设施目的地信息的URL.
        public static string queryHotelAmenityByID(string hotelID)
        {
            string url = lmApiUrl + "/show/propAmenityV2.html?prop={0}";//mapiUrl + "/show/propAmenityV2.html?prop={0}";
            return string.Format(url, hotelID);
        }

        public static string queryHotelAmenityByIDV2(string userCode, string hotelID)
        {
            string url = lmApiUrlV2 + "/content/hotelAmenityList.json?useCode={0}&useCodeVersion=0&clientCode=HOTELVP&hotelId={1}";// GetUrlFromXml("SearchHotelAmenityUrlV2");//mapiUrl + "/show/propAmenityV2.html?prop={0}";
            return string.Format(url, userCode, hotelID);
        }

        //获取酒店图片信息的URL.
        public static string queryHotelImages(string hotelID, string imgSize)
        {
            string url = mapiUrl + "/rest/hotel/image/{0}/{1}.json"; //return string.Format("/mapi/rest/hotel/image/{0}/{1}.json", hotelID, imgSize);
            return string.Format(url, hotelID, imgSize);
        }

        //获取酒店图片信息的URL Service 2.0.
        public static string queryHotelImagesV2(string hotelID, string PlatForm, string imgSize)
        {
            string url = lmApiUrlV2 + "/content/hotelImagesList.json?useCode={0}&useCodeVersion=0&clientCode=HOTELVP&hotelId={1}&imgSize={2}";// GetUrlFromXml("SearchHotelImagesUrlV2");
            return string.Format(url, PlatForm, hotelID, imgSize);
        }

        //获取客服电话信息的URL.
        public static string queryHotelServiceTel()
        {
            string url = lmApiUrl + "/sysinfoCity.html?clientCode=HOTELVP&useCode=IOS"; //"/mapi/rest/hotel/image/{0}/{1}.json";//sysinfoCity.html?clientCode=HOTELVP&useCode=IOS
            return string.Format(url);
        }

        //获取客服电话信息的URL.
        public static string queryHotelServiceTelV2(string platform)
        {
            string url = lmApiUrlV2 + "/content/sysCityList.json?useCode={0}&useCodeVersion=0&clientCode=HOTELVP";// GetUrlFromXml("SearchServiceTelUrlV2");
            return string.Format(url, platform);
        }

        //获取查询反馈信息的URL.
        public static string getSearchAdviceUrl()
        {
            string url = lmApiUrl + "";// CommonFunction.GetUrlFromXml("MapiURL", "AdviceSearchUrl");
            return url;
        }


        public static string getSearchAdviceJsonUrl()
        {
            string url = lmApiUrl + "";// CommonFunction.GetUrlFromXml("MapiURL", "AdviceSearchJsonUrl");
            return url;
        }


        //根据电话号码，查询验证码，生产新的验证码
        public static string QueryUserSign()
        {
            string url = mapiUrl + "";// CommonFunction.GetUrlFromXml("MapiURL", "QueryUserSignUrl");
            return url;
        }

        //get JsonStringValue
        public static string GetJsonStringValue(JObject jso, string key)
        {
            try
            {
                return jso[key] == null ? string.Empty : jso[key].ToString();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///  根据文件名，和ID查询得到的值。
        /// </summary>
        /// <param name="FileName">文件名,不包含后缀名</param>
        /// <param name="TagID">mapiID的对应值</param>
        /// <returns></returns>
        public static string GetUrlFromXml(string TagID)
        {
            string XmlTagName = "mapi";//  <mapi mapiID="AdviceSearch">/advice.html/json/xml</mapi>
            string XmlTagID = "lmApiID";//

            string xmlString = String.Empty;
            //string path = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["SqlPath"].ToString() + SqlName + ConfigurationManager.AppSettings["SqlType"].ToString();
            string path = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["SerUrlPath"].ToString();

            XmlDocument doc = new XmlDocument();//创建一个新的xmldocumt 对象
            doc.Load(path);//加载xml文件

            //string SqlTagName = ConfigurationManager.AppSettings["SqlTagName"].ToString();
            //string SqlAttributes = ConfigurationManager.AppSettings["SqlAttributes"].ToString();

            XmlNodeList list = doc.DocumentElement.GetElementsByTagName(XmlTagName);
            foreach (XmlNode node in list)
            {
                if (node.Attributes[XmlTagID].Value.Equals(TagID))
                {
                    xmlString = node.InnerText;
                    break;
                }
            }
            return xmlString;
        }

        public static string ToJsJson(object item)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        public static T FromJsonTo<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                T jsonObject = (T)ser.ReadObject(ms);
                return jsonObject;
            }
        }
    }
}