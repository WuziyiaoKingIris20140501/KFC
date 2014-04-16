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
using JJZX.JobConsole.Entity;
using JJZX.JobConsole.DataAccess;
using System.Web.Security;
using System.Xml;
using System.Security.Cryptography;

namespace JJZX.JobConsole.ServiceAdapter
{
    public abstract class AutoHotelPlanSA
    {
        public static int ApplyFYCZInterface(AutoPlanEntity autohotelplanEntity)
        {
            AutoHotelPlanDBEntity dbParm = (autohotelplanEntity.AutoHotelPlanDBEntity.Count > 0) ? autohotelplanEntity.AutoHotelPlanDBEntity[0] : new AutoHotelPlanDBEntity();

            string StrBody = string.Format("agentid={0}&mobilenum={1}&money={2}&orderid={3}&key=", dbParm.AgentId, dbParm.MobileNum, dbParm.Money, dbParm.OrderId);
            string sign = MD5Make(StrBody);
            string FYCZUrl = JsonRequestURLBuilder.applyFYCZ(dbParm.AgentId, dbParm.MobileNum, dbParm.Money, dbParm.OrderId, sign);
            int iMax = 5;
            Hashtable hsResult = new Hashtable();
            while(iMax > 0){

                string strResult = CommonCallWebUrl(FYCZUrl, "");
                hsResult = LoadXml(strResult);
                string result = hsResult.ContainsKey("result") ? hsResult["result"].ToString() : "";
                if ("1005".Equals(result))
                {
                    iMax = iMax - 1;
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }
               
                AutoPlanDA.InsertYFOrderList(dbParm, hsResult, "");
                return 1;
            }

            AutoPlanDA.InsertYFOrderList(dbParm, hsResult, "");
            return 1;
        }


        public static string MsgSendInterface(AutoPlanEntity autohotelplanEntity)
        {
            AutoHotelPlanDBEntity dbParm = (autohotelplanEntity.AutoHotelPlanDBEntity.Count > 0) ? autohotelplanEntity.AutoHotelPlanDBEntity[0] : new AutoHotelPlanDBEntity();
            string MsgContent = ConfigurationManager.AppSettings["MsgContent"].ToString();
            string SignName = FormsAuthentication.HashPasswordForStoringInConfigFile(dbParm.MobileNum + ConfigurationManager.AppSettings["JJMD5"].ToString(), "MD5").ToString().ToLower();
            string body = "{" + string.Format("\"mobile\":\"{0}\",\"content\":\"{1}\",\"signName\":\"{2}\"", dbParm.MobileNum, MsgContent, SignName) + "}";
            string MsgUrl = ConfigurationManager.AppSettings["MsgUrl"].ToString() + "channel=hotelvp&platForm=android&platFormVersion=1.4&signKey=" + FormsAuthentication.HashPasswordForStoringInConfigFile(body + ConfigurationManager.AppSettings["JYMD5"].ToString(), "MD5");
            string strResult = CommonCallWebUrl(MsgUrl, body);
            return JsonUtility.Deserialize<Response<MsgInfo>>(strResult).result.ReturnVal;
        }


        public static int ApplyFYCXInterface(AutoPlanEntity autohotelplanEntity)
        {
            AutoHotelPlanDBEntity dbParm = (autohotelplanEntity.AutoHotelPlanDBEntity.Count > 0) ? autohotelplanEntity.AutoHotelPlanDBEntity[0] : new AutoHotelPlanDBEntity();

            string StrBody = string.Format("agentid={0}&orderid={1}&key=", dbParm.AgentId, dbParm.OrderId);
            string sign = MD5Make(StrBody);
            string FYCXUrl = JsonRequestURLBuilder.applyFYCX(dbParm.AgentId, dbParm.OrderId, sign);
            string strResult = CommonCallWebUrl(FYCXUrl, "");
            Hashtable hsResult = LoadXml(strResult);
            string state = hsResult.ContainsKey("state") ? hsResult["state"].ToString() : "";
            string MsgRes = "";
            if ("3".Equals(state))
            {
                MsgRes = MsgSendInterface(autohotelplanEntity);
            }
            AutoPlanDA.UpdateYFOrderStatus(dbParm.OrderId, hsResult, MsgRes);
            return 1;
        }

        public static Hashtable ApplyFYYEInterface()
        {
            string agentid = ConfigurationManager.AppSettings["AgentID"].ToString();
            string sign = MD5Make("agentid=" + agentid + "&key=");

            string FYYEUrl = JsonRequestURLBuilder.applyFYYE(agentid, sign);
            string strResult = CommonCallWebUrl(FYYEUrl, "");

            Hashtable hsResult = LoadXml(strResult);
            return hsResult;
        }

        public static string MD5Make(string body)
        {
            string MD5Key = ConfigurationManager.AppSettings["FYMD5"].ToString();
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;
            //此处GB2312 编码请不要修改 否则可能会发生密匙加密错误的情况.
            inputBye = Encoding.GetEncoding("GB2312").GetBytes(body + MD5Key);
            outputBye = m5.ComputeHash(inputBye);
            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToLower();
            return retStr.Substring(0, 32);
        }

        //public static string PostSignKey(string body)
        //{
        //    try
        //    {
        //        string MD5Key = ConfigurationManager.AppSettings["FYMD5"].ToString();
        //        string signKey = FormsAuthentication.HashPasswordForStoringInConfigFile(body + MD5Key, "MD5").ToLower();
        //        return signKey;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}

        private static Hashtable LoadXml(string strXml)
        {
            Hashtable hsResult = new Hashtable();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);
            XmlNode list = (XmlNode)doc.GetElementsByTagName("Suprsinfo")[0];//取第一个子节点

            foreach (XmlNode node in list)
            {
                hsResult.Add(node.Name, node.InnerXml.ToString());
            }
            return hsResult;
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

        public static string CommonCallWebUrl(string strUrl, string body)
        {
            string strJson = string.Empty;
            try
            {
                CallWebPage callWebPage = new CallWebPage();
                strJson = callWebPage.CallWebByURL(strUrl, body);
            }
            catch
            {

            }
            return strJson;
        }
    }
}