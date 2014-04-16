using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;

/// <summary>
///JsonRequestURLBuilder 的摘要说明
/// </summary>
public class JsonRequestURLBuilder
{
    public static string mapiUrl = ConfigurationManager.AppSettings["mapiUrl"];//mapi URL地址
    public static string lmApiUrl = ConfigurationManager.AppSettings["lmapiUrl"];//lmapi URL地址

    public JsonRequestURLBuilder()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    
    
    //获取查询反馈信息的URL.
    public static string getSearchAdviceUrl()
    {
        string url = lmApiUrl + CommonFunction.GetUrlFromXml("MapiURL", "AdviceSearchUrl");
        return url;    
    }


    public static string getSearchAdviceJsonUrl()
    {
        string url = lmApiUrl + CommonFunction.GetUrlFromXml("MapiURL", "AdviceSearchJsonUrl");
        return url;    
    }


    //根据电话号码，查询验证码，生产新的验证码
    public static string QueryUserSign()
    {
        string url = mapiUrl + CommonFunction.GetUrlFromXml("MapiURL", "QueryUserSignUrl");
        return url;
    }


    ////得到单房型的酒店列表信息
    //public static string queryHotels(string user, string key, string cityId, string checkInDate, string checkOutDate)
    //{
    //    string url = mapiUrl + "/mapi/rest/lm/hotelbase/{0}/{1}/100.json?user={2}&key={3}";
    //    //return string.Format("http://mapi.huitongke.com/mapi/rest/lm/hotelbase/{0}/{1}/100.json?user={2}&key={3}", cityId, checkInDate, user, key);
    //    return string.Format(url, cityId, checkInDate, user, key);
    //}


   

}