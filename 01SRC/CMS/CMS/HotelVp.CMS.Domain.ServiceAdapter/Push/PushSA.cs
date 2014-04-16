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
    public abstract class PushInfoSA
    {
        public static PushEntity SendPush(PushEntity pushEntity)
        {
            string DataString = "";
            PushDBEntity dbParm = (pushEntity.PushDBEntity.Count > 0) ? pushEntity.PushDBEntity[0] : new PushDBEntity();

            DataSet dsUserInfo = PushDA.GetUserInfo(dbParm.TelPhone);
            if (dsUserInfo.Tables.Count == 0 || dsUserInfo.Tables[0].Rows.Count == 0)
            {
                pushEntity.Result = 0;
                pushEntity.ErrorMSG = "发送失败！";
                return pushEntity;
            }

            string strRegChanel = dsUserInfo.Tables[0].Rows[0]["CLIENT_CODE"].ToString().Trim();//(!String.IsNullOrEmpty(dsUserInfo.Tables[0].Rows[0]["regchanel_code"].ToString().Trim()) && ConfigurationManager.AppSettings["MyQueueReg"].ToString().Contains(dsUserInfo.Tables[0].Rows[0]["regchanel_code"].ToString())) ? "HOTELVPPRO" : "HOTELVP";
             DataString = "{\"pushDynamicList\":[{\"userId\":\"" + dsUserInfo.Tables[0].Rows[0]["id"].ToString() + "\",\"objLinkId\":\"" + dbParm.ID + "\",\"deviceToken\":\"" + dsUserInfo.Tables[0].Rows[0]["devicetoken"].ToString() + "\",\"useCode\":\"" + dsUserInfo.Tables[0].Rows[0]["platform_code"].ToString() + "\",\"useCodeVersion\":\"" + dsUserInfo.Tables[0].Rows[0]["version"].ToString() + "\",\"clientCode\":\"" + strRegChanel + "\"}],\"title\":\"" + dbParm.Title + "\",\"content\":\"" + dbParm.Content + "\",\"objType\":" + dbParm.Type + ",\"objUrl\":\"\",\"pic\":\"\",\"isPopup\":\"\",\"taskId\":\"" + dbParm.ID + "\",\"endTime\":\"\"}";
            QueueHelper.SendMessage(DataString);
            pushEntity.Result = 1;
            pushEntity.ErrorMSG = "发送成功！";
            return pushEntity;
        }


        public static string PostSignKey(string body)
        {
            try
            {
                string MD5Key = ConfigurationManager.AppSettings["MSGMD5Key"].ToString();
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