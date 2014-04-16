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

namespace JJZX.JobConsole.ServiceAdapter
{
/// <summary>
///JsonRequestURLBuilder 的摘要说明
/// </summary>
    public class JsonRequestURLBuilder
    {
        public static string apiUrl = ConfigurationManager.AppSettings["FYApiUrl"];//mapi URL地址

        public JsonRequestURLBuilder()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string applyFYCZ(string agentid, string mobilenum, string money, string orderid, string sign)
        {
            string url = apiUrl + string.Format("refer.aspx?agentid={0}&mobilenum={1}&money={2}&orderid={3}&sign={4}", agentid, mobilenum, money, orderid, sign);
            return url;
        }

        public static string applyFYCX(string agentid, string orderid, string sign)
        {
            string url = apiUrl + string.Format("query.aspx?agentid={0}&orderid={1}&sign={2}", agentid, orderid, sign);
            return url;
        }

        public static string applyFYYE(string agentid, string sign)
        {
            string url = apiUrl + string.Format("balance.aspx?agentid={0}&&sign={1}", agentid, sign);
            return url;
        }
    }
}