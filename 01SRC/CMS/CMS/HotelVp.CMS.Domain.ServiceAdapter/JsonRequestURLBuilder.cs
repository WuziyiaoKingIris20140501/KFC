using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml;

using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;

namespace HotelVp.CMS.Domain.ServiceAdapter
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
        public static string SearchapiV2 = ConfigurationManager.AppSettings["searchapiV2"];//SearchapiV2 URL地址

        public JsonRequestURLBuilder()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string SaveCashBackRequestV2()
        {
            string url = lmApiUrlV2 + GetUrlFromXml("SaveCashBackRequestV2");// "/show/getSupCity.json?clientCode=&useCode=IOS";
            return url;
        }

        public static string getSignByPhoneForCCV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("getSignByPhoneForCCV2");// "/management/cms/getUserCaptcha.json";
            return url;
        }

        //获取城市信息的URL.
        public static string getSearchCityUrl()
        {
            string url = lmApiUrl + GetUrlFromXml("SearchCityUrl");// "/show/getSupCity.json?clientCode=&useCode=IOS";
            return url;
        }

        public static string getSearchCityUrlV2(string strUserCode)
        {
            string baseUrl = GetUrlFromXml("SearchCityUrlV2");// "/show/getSupCity.json?clientCode=&useCode=IOS";
            string url = lmApiUrlV2 + String.Format(baseUrl, strUserCode);
            return url;
        }

        public static string getSearchCityUrlV3(string strUserCode)
        {
            string baseUrl = GetUrlFromXml("SearchCityUrlV3");// "/show/getSupCity.json?clientCode=&useCode=IOS";
            string url = lmApiUrlV2 + String.Format(baseUrl, strUserCode);
            return url;
        }

        //获取查询酒店列表信息的URL.
        public static string getSearchHotelListUrl(string CityID, string PlatForm, string TypeID)
        {
            string baseUrl = GetUrlFromXml("SearchHotelListUrl");// "/show/hotel-list-v2.json?sysSource={0}&imgSize=200&cityId={1}&version=0&isTest=true&isToday={2}";
            string url = lmApiUrl + String.Format(baseUrl, PlatForm, CityID, ("1".Equals(TypeID) ? "true" : "false"));
            return url;
        }

        public static string getSearchHotelListUrlV2(string CityID, string PlatForm, string TypeID)
        {
            string baseUrl = GetUrlFromXml("getSearchHotelListUrlV2");
            string url = lmApiUrlV2 + String.Format(baseUrl, PlatForm, CityID, ("1".Equals(TypeID) ? "true" : "false"));
            return url;
        }

        public static string getSearchHotelListUrlV3(string CityID, string PlatForm, string TypeID)
        {
            string baseUrl = GetUrlFromXml("getSearchHotelListUrlV3");
            string url = SearchapiV2 + String.Format(baseUrl, PlatForm);
            return url;
        }

        //获取酒店详细信息的URL.
        public static string queryHotelsByID(string CityID, string PlatForm, string TypeID)
        {
            string url = lmApiUrl + GetUrlFromXml("SearchHotelsByIDUrl");//mapiUrl + "/mapi/rest/lm/hotelbaseHotelid/{0}/{1}/100/{2}.json?user={3}&key={4}";
            return string.Format(url, PlatForm, CityID, ("1".Equals(TypeID) ? "true" : "false"));
        }

        public static string queryHotelsByIDV2(string CityID, string PlatForm, string TypeID)
        {
            string url = lmApiUrlV2 + GetUrlFromXml("SearchHotelsByIDUrlV2");//mapiUrl + "/mapi/rest/lm/hotelbaseHotelid/{0}/{1}/100/{2}.json?user={3}&key={4}";
            return string.Format(url, PlatForm, CityID, ("1".Equals(TypeID) ? "true" : "false"));
        }

        public static string queryHotelsBySUPIDV2(string HotelID, string InDate, string OutDate)
        {
            string url = lmApiUrlV2 + GetUrlFromXml("SearchHotelsBySUPIDUrlV2");//mapiUrl + "/mapi/rest/lm/hotelbaseHotelid/{0}/{1}/100/{2}.json?user={3}&key={4}";
            return string.Format(url, HotelID, InDate, OutDate);
        }

        public static string queryHotelsByIDV3(string CityID, string PlatForm, string TypeID, string HotelID)
        {
            string url = lmApiUrlV2 + GetUrlFromXml("SearchHotelsByIDUrlV3");//mapiUrl + "/mapi/rest/lm/hotelbaseHotelid/{0}/{1}/100/{2}.json?user={3}&key={4}";
            return string.Format(url, PlatForm, HotelID);
        }

        //获取酒店服务设施目的地信息的URL.
        public static string queryHotelAmenityByID(string hotelID)
        {
            string url = lmApiUrl + GetUrlFromXml("SearchHotelAmenityUrl");//mapiUrl + "/show/propAmenityV2.html?prop={0}";
            return string.Format(url, hotelID);
        }

        public static string queryHotelAmenityByIDV2(string userCode, string hotelID)
        {
            string url = lmApiUrlV2 + GetUrlFromXml("SearchHotelAmenityUrlV2");//mapiUrl + "/show/propAmenityV2.html?prop={0}";
            return string.Format(url, userCode, hotelID);
        }

        //获取酒店图片信息的URL.
        public static string queryHotelImages(string hotelID, string imgSize)
        {
            string url = mapiUrl + GetUrlFromXml("SearchHotelImagesUrl"); //return string.Format("/mapi/rest/hotel/image/{0}/{1}.json", hotelID, imgSize);
            return string.Format(url, hotelID, imgSize);
        }

        //获取酒店图片信息的URL Service 2.0.
        public static string queryHotelImagesV2(string hotelID, string imgSize)
        {
            string url = lmApiUrlV2 + GetUrlFromXml("SearchHotelImagesUrlV2");
            return string.Format(url, hotelID, imgSize);
        }

        //获取客服电话信息的URL.
        public static string queryHotelServiceTel()
        {
            string url = lmApiUrl + GetUrlFromXml("SearchServiceTelUrl"); //"/mapi/rest/hotel/image/{0}/{1}.json";//sysinfoCity.html?clientCode=HOTELVP&useCode=IOS
            return string.Format(url);
        }

        //获取客服电话信息的URL.
        public static string queryHotelServiceTelV2(string platform)
        {
            string url = lmApiUrlV2 + GetUrlFromXml("SearchServiceTelUrlV2");
            return string.Format(url, platform);
        }

        public static string applyHotelFullRoomV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("ApplyHotelFullRoomV2");
            return url;
        }

        public static string applyHotelFullRoomByUpdatePlan()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("RenewPlanFullRoomByUpdatePlan");
            return url;
        }

        /// <summary>
        /// 房态控制 --  批量更新计划的接口  type:1 满房、2 关房、3 开房
        /// </summary>
        /// <returns></returns>
        public static string BatchUpdatePlan()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("BatchUpdatePlan");
            return url;
        }

        /// <summary>
        /// 退款接口(预付)
        /// </summary>
        /// <returns></returns>
        public static string saveRefund()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("saveRefund");
            return url;
        }


        public static string queryHotelFullRoomV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("queryHotelFullRoomV2");
            return url;
        }

        public static string queryHotelBalRoomV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("queryHotelBalRoomV2");
            return url;
        }

        public static string applyHotelBalRoomV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("applyHotelBalRoomV2");
            return url;
        }

        public static string saveOrderOperationV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("saveOrderOperationV2");
            return url;
        }

        public static string saveAuditOrderOperationV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("saveAuditOrderOperationV2");
            return url;
        }

        public static string sendFaxSeviceV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("sendFaxSeviceV2");
            return url;
        }

        public static string sendBindFaxSeviceV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("sendBindFaxSeviceV2");
            return url;
        }

        public static string applyHotelRoomPlanV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("applyHotelRoomPlanV2");
            return url;
        }

        public static string applyHotelPrRoomPlanV2()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("applyHotelPrRoomPlanV2");
            return url;
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

        public static string applySendMsgV2()
        {
            string url = lmMsgApiUrlV2 + "/sms.json";
            return url;
        }

        /// <summary>
        /// 支付宝充值
        /// </summary>
        /// <returns></returns>
        public static string autoPay()
        {
            string url = lmScApiUrlV2 + GetUrlFromXml("autoPayV2");
            return url;
        }

        /// <summary>
        /// 更新订单最终状态接口
        /// </summary>
        /// <returns></returns>
        public static string IssueOrderV2()
        {
            string url = lmScApiUrlV2 + "/management/cms/updateIssueOrder.json";
            return url;
        }

        /// <summary>
        /// 查询订单最终状态接口
        /// </summary>
        /// <returns></returns>
        public static string orderQueryByOrderNumV2()
        {
            string url = lmScApiUrlV2 + "/management/cms/orderQueryByOrderNum.json";
            return url;
        }

        /// <summary>
        /// eBooking 用户查询
        /// </summary>
        /// <returns></returns>
        public static string eBookingUserQuery()
        {
            string url = lmScApiUrlV2 + "/management/userQuery.json";
            //string url = "http://10.10.0.72/v2/management/userQuery.json";
            return url;
        }

        /// <summary>
        /// eBooking 新增用户
        /// </summary>
        /// <returns></returns>
        public static string eBookingUserSave()
        {
            string url = lmScApiUrlV2 + "/management/userSave.json";
            //string url = "http://10.10.0.72/v2/management/userSave.json";
            return url;
        }

        /// <summary>
        /// eBooking 修改用户
        /// </summary>
        /// <returns></returns>
        public static string eBookingUserUpdate()
        {
            string url = lmScApiUrlV2 + "/management/userUpdate.json";
            //string url = "http://10.10.0.72/v2/management/userUpdate.json";
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
    }
}
