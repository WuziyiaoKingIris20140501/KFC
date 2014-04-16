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
    public abstract class UserSearchSA
    {
        public static UserEntity getSignByPhoneForCC(UserEntity userEntity)
        {
            string DataString = string.Empty;
            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();

            DataString = "{\"mobile\":\"" + dbParm.LoginMobile + "\"," + "\"isUpdate\":\"" + "1" + "\"}";

            string SaveCashBackRequestUrl = JsonRequestURLBuilder.getSignByPhoneForCCV2();// +PostSignKey(DataString);
            CallWebPage callWebPage = new CallWebPage();
            string strSaveCashBack = callWebPage.CallWebByURL(SaveCashBackRequestUrl, DataString);
            JObject oSaveCashBack = JObject.Parse(strSaveCashBack);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oSaveCashBack, "message").Trim('"')))
            {
                userEntity.Result = 1;
                userEntity.UserDBEntity[0].SignKey = JsonRequestURLBuilder.GetJsonStringValue(oSaveCashBack, "result").Trim('"');
                //string oIDs = oSaveCashBack.SelectToken("result").ToString();
                //JObject oID = JObject.Parse(oIDs);
                //userEntity.UserDBEntity[0].SignKey = oID.SelectToken("sn").ToString().Trim('"');
            }
            else
            {
                string ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oSaveCashBack, "message").Trim('"');
                userEntity.ErrorMSG = ErrorMSG;
                userEntity.Result = 2;
            }
            return userEntity;
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