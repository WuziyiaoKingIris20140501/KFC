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
using HotelVp.JobConsole.Entity;
using HotelVp.JobConsole.DataAccess;

namespace HotelVp.JobConsole.ServiceAdapter
{
    public abstract class AutoMsgCancelOrdSA
    {
        public static AutoMsgCancelOrdEntity ApplySendMsgService(AutoMsgCancelOrdEntity automsgcancelordEntity)
        {
            AutoMsgCancelOrdDBEntity dbParm = (automsgcancelordEntity.AutoMsgCancelOrdDBEntity.Count > 0) ? automsgcancelordEntity.AutoMsgCancelOrdDBEntity[0] : new AutoMsgCancelOrdDBEntity();
            string MsgContent = string.Empty;

            if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToShortDateString() + " 18:00:00"))
            {
                MsgContent = String.Format(ConfigurationManager.AppSettings["MsgContent18after"].ToString(), dbParm.UserNM, dbParm.HotelNM);
            }
            else
            {
                MsgContent = String.Format(ConfigurationManager.AppSettings["MsgContent18befor"].ToString(), dbParm.UserNM, dbParm.HotelNM);
            }

            string DataString = "";
            DataString = DataString + "{\"method\":\"save\",\"data\":{\"syscode\":\"" + "CMS" + "\",\"reqid\":\"" + dbParm.CreateUser +"\",\"bizcode\":\"" + "CMS-JOB-短信提示取消单" +"\",";
            DataString = DataString + "\"cnfnum\":\"" + dbParm.OrderNo + "\",\"mobiles\":\"" + dbParm.CreateUser + "\",\"msg\":\"" + MsgContent + "\",";
            DataString = DataString + "\"sign\":\"" + PostSignKey("CMS" + dbParm.CreateUser) + "\"},\"version\":\"v1.0\"}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.applySendMsgV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelFullRoom = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);
            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);
            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"')))
            {
                automsgcancelordEntity.Result = 1;
                automsgcancelordEntity.ErrorMSG = "保存成功！";
            }
            else
            {
                automsgcancelordEntity.Result = 2;
                automsgcancelordEntity.ErrorMSG = "保存失败！" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
            }

            return automsgcancelordEntity;
        }

        // md5加密

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