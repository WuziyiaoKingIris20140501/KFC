using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.EBOK.Domain.API.Model;

namespace HotelVp.EBOK.Domain.API
{
    public static class SysCnfAPI
    {
        /// <summary>
        /// 系统配置 用户取得
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <returns></returns>
        public static Response<List<OpeUser>> GetIndexMainList(string mac, string hId)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\"", mac, hId);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "user/userQuery.json", body);
                return JsonUtility.Deserialize<Response<List<OpeUser>>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<List<OpeUser>>() { timeOut = true };
            }
            catch (Exception ex)
            {
                return new Response<List<OpeUser>>() { };
            }
        }

        public static Response<object> DeleteUserInfo(string userId, string userName, string opeuserid, string mac, string hid)
        {
            try
            {
                var url = "\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"userId\":\"{2}\",\"operator\":\"{3}\"";
                var body = string.Format(url,  mac, hid, userId, opeuserid);

                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "user/userDetele.json", body);
                return JsonUtility.Deserialize<Response<object>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<object>() { timeOut = true };
            }
            catch (Exception ex)
            {
                return new Response<object>() { timeOut = true };
            }
        }

        public static Response<object> SaveUserInfo(string userId, string userName, string tel, string status, string remark, string opeuserid, string action, string mac, string hid)
        {
            try
            {
                var url = "";
                var body = "";
                var host = "user/userUpdate.json";
                if ("1".Equals(action))
                {
                    url = "\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"loginName\":\"{2}\",\"tel\":\"{3}\",\"remark\":\"{4}\",\"operatorId\":\"{5}\",\"status\":\"{6}\"";
                    host = "user/userSave.json";
                    body = string.Format(url, mac, hid, userName, tel, remark, opeuserid, status);
                }
                else
                {
                    url = "\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"userId\":\"{2}\",\"loginName\":\"{3}\",\"tel\":\"{4}\",\"remark\":\"{5}\",\"operator\":\"{6}\",\"status\":\"{7}\"";
                    body = string.Format(url, mac, hid, userId, userName, tel, remark, opeuserid, status);
                }

                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + host, body);
                return JsonUtility.Deserialize<Response<object>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<object>() { timeOut = true };
            }
            catch (Exception ex)
            {
                return new Response<object>() { timeOut = true };
            }
        }
        
    }
}
