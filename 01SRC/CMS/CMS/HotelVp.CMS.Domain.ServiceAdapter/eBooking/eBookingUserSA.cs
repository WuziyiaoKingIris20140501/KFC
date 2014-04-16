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
using HotelVp.CMS.Domain.Entity.eBooking;

namespace HotelVp.CMS.Domain.ServiceAdapter.eBooking
{
    public abstract class eBookingUserSA
    {
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

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="eBookingUserEntity"></param>
        /// <returns></returns>
        public static string eBookingUserSave(string loginName, string password, string hotelIds, string remark, string tel, string operatorIds)
        {
            string DataString = "{\"loginName\":\"" + loginName + "\"," + "\"password\":\"" + password + "\"," + "\"hotelIds\":\"" + hotelIds + "\"," + "\"remark\":\"" + remark + "\"," + "\"tel\":\"" + tel + "\"," + "\"operator\":\"" + operatorIds + "\"," + "\"roleId\":\"2\"}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.eBookingUserSave();

            CallWebPage callWebPage = new CallWebPage();
            string strJson = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);

            JObject oHotelFullRoom = JObject.Parse(strJson);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"')))
            {
                return "{\"d\":\"[" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"') + "]\"}";
            }
            else
            {
                return "{\"d\":\"[" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"') + "]\"}";

            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="eBookingUserEntity"></param>
        /// <returns></returns>
        public static string eBookingUserUpdate(string userId, string loginName, string password, string hotelIds, string remark, string tel, string operatorIds)
        {
            string DataString = "{\"userId\":" + userId + "," + "\"loginName\":\"" + loginName + "\"," + "\"password\":\"" + password + "\"," + "\"hotelIds\":\"" + hotelIds + "\"," + "\"remark\":\"" + remark + "\"," + "\"tel\":\"" + tel + "\"," + "\"operator\":\"" + userId + "\"," + "\"roleId\":\"2\"}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.eBookingUserUpdate();

            CallWebPage callWebPage = new CallWebPage();
            string strJson = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);

            JObject oHotelFullRoom = JObject.Parse(strJson);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"')))
            {
                return "{\"d\":\"[" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"') + "]\"}";
            }
            else
            {
                return "{\"d\":\"[" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"') + "]\"}";

            }
        }

        /// <summary>
        /// 查询用户 返回列表 酒店数
        /// </summary>
        /// <param name="eBookingUserEntity"></param>
        /// <returns></returns>
        public static eBookingUserEntity eBookingUserQuery(eBookingUserEntity eBookingUserEntity)
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("USERNAME"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELNAME"));
            dsResult.Tables[0].Columns.Add(new DataColumn("CREATETIME"));
            dsResult.Tables[0].Columns.Add(new DataColumn("RECORDCOUNT"));
            try
            {
                eBookingUserDBEntity dbParm = (eBookingUserEntity.eBookingUserDBEntity.Count > 0) ? eBookingUserEntity.eBookingUserDBEntity[0] : new eBookingUserDBEntity();

                string DataString = "{\"loginName\":\"" + dbParm.LoginName + "\"," + "\"hotelId\":\"" + dbParm.HotelId + "\"," + "\"operator\":\"" + dbParm.OperatorId + "\"," + "\"pageSize\":\"" + dbParm.PageSize + "\"," + "\"pageNum\":\"" + dbParm.PageNum + "\"}";

                string HotelFullRoomUrl = JsonRequestURLBuilder.eBookingUserQuery();

                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);

                //解析json数据
                JObject o = JObject.Parse(strJson);
                if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(o, "message").Trim('"')))
                {
                    string oList = o.SelectToken("result").ToString();
                    JArray jsa = (JArray)JsonConvert.DeserializeObject(oList);

                    string oPage = o.SelectToken("page").SelectToken("count").ToString().Trim('"');
                    
                    //JArray jsa = (JArray)JsonConvert.DeserializeObject(oList);
                    for (int i = 0; i < jsa.Count; i++)
                    {
                        string oHotelInfoList = jsa[i].SelectToken("hotelinfo").ToString();
                        JObject jsoHotelInfoList = (JObject)jsa[i];
                        JArray jsoHotelInfo = (JArray)JsonConvert.DeserializeObject(oHotelInfoList);
                        DataRow drRow = dsResult.Tables[0].NewRow();
                        if (jsoHotelInfo.Count == 1)
                        {
                            JObject jso = (JObject)jsoHotelInfo[0];
                            drRow["USERNAME"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelInfoList, "loginname").Trim('"');
                            drRow["HOTELNAME"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelname").Trim('"');
                            drRow["CREATETIME"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelInfoList, "createtime").Trim('"');
                        }
                        else if (jsoHotelInfo.Count > 1)
                        {
                            drRow["USERNAME"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelInfoList, "loginname").Trim('"');
                            drRow["HOTELNAME"] = jsoHotelInfo.Count + "家";
                            drRow["CREATETIME"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelInfoList, "createtime").Trim('"');
                        }
                        drRow["RECORDCOUNT"] = oPage;
                        dsResult.Tables[0].Rows.Add(drRow);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            eBookingUserEntity.QueryResult = dsResult;
            return eBookingUserEntity;
        }

        /// <summary>
        /// 用户查询 转JSON字符串
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataSet eBookingUserQuery(string userName)
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add(new DataColumn("USERID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("USERNAME"));
            dsResult.Tables[0].Columns.Add(new DataColumn("USERTEL"));
            dsResult.Tables[0].Columns.Add(new DataColumn("USERPWD"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELID"));
            dsResult.Tables[0].Columns.Add(new DataColumn("HOTELNAME"));
            dsResult.Tables[0].Columns.Add(new DataColumn("REMARK"));
            try
            {
                string DataString = "{\"loginName\":\"" + userName + "\"}";

                string HotelFullRoomUrl = JsonRequestURLBuilder.eBookingUserQuery();

                CallWebPage callWebPage = new CallWebPage();
                string strJson = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);
                //解析json数据
                JObject o = JObject.Parse(strJson);
                if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(o, "message").Trim('"')))
                {
                    string oList = o.SelectToken("result").ToString();
                    JArray jsa = (JArray)JsonConvert.DeserializeObject(oList);

                    for (int i = 0; i < jsa.Count; i++)
                    {
                        string oHotelInfoList = jsa[i].SelectToken("hotelinfo").ToString();
                        JObject jsoHotelInfoList = (JObject)jsa[i];
                        JArray jsoHotelInfo = (JArray)JsonConvert.DeserializeObject(oHotelInfoList);
                        for (int j = 0; j < jsoHotelInfo.Count; j++)
                        {
                            DataRow drRow = dsResult.Tables[0].NewRow();
                            JObject jso = (JObject)jsoHotelInfo[j];
                            drRow["USERID"] = jsa[i].SelectToken("userid").ToString().Trim('"');
                            drRow["USERNAME"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelInfoList, "loginname").Trim('"');
                            drRow["USERTEL"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelInfoList, "tel").Trim('"');
                            drRow["USERPWD"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelInfoList, "password").Trim('"');
                            drRow["HOTELID"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelid").Trim('"');
                            drRow["HOTELNAME"] = JsonRequestURLBuilder.GetJsonStringValue(jso, "hotelname").Trim('"');
                            drRow["REMARK"] = JsonRequestURLBuilder.GetJsonStringValue(jsoHotelInfoList, "remark").Trim('"');

                            dsResult.Tables[0].Rows.Add(drRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dsResult;
        }
    }
}
