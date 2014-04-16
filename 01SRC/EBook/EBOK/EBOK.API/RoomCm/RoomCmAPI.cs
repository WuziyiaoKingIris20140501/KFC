using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.EBOK.Domain.API.Model;

namespace HotelVp.EBOK.Domain.API
{
    public static class RoomCmAPI
    {
        /// <summary>
        /// 今日计划房型信息
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Response<List<RoomList>> GetIndexMainList(string mac, string hId, string beginDate, string endDate)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\"", mac, hId, beginDate, endDate);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "plan/planList.json", body);
                return JsonUtility.Deserialize<Response<List<RoomList>>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<List<RoomList>>() { timeOut = true };
            }
            catch (Exception ex)
            {
                return new Response<List<RoomList>>() { };
            }
        }

        /// <summary>
        /// 计划房型信息
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Response<List<RoomList>> GetRoomPlanList(string mac, string hId, string beginDate, string endDate, string raceCode)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"rateCode\":\"{4}\",\"pageSize\":1000", mac, hId, beginDate, endDate, raceCode);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "plan/planList.json", body);
                return JsonUtility.Deserialize<Response<List<RoomList>>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<List<RoomList>>() { timeOut = true };
            }
            catch (Exception ex)
            {
                return new Response<List<RoomList>>() { };
            }
        }

        /// <summary>
        /// 酒店操作用户
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetOpeUserList(string mac, string hId)
        {
            Dictionary<string, string> OpeUserList = new Dictionary<string, string>();
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"status\":\"1\"", mac, hId);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "user/userQuery.json", body);
                List<OpeUser> userList = JsonUtility.Deserialize<Response<List<OpeUser>>>(r).result;
                for (int i = 0;i < userList.Count; i++)
                {
                    OpeUserList.Add(userList[i].userId, userList[i].userName);
                }
                return OpeUserList;
            }
            catch (TimeoutException)
            {
                return OpeUserList;
            }
            catch (Exception ex)
            {
                return OpeUserList;
            }
        }

        /// <summary>
        /// 修改房态
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="rateCode"></param>
        /// <param name="roomTypeCode"></param>
        /// <param name="roomnum"></param>
        /// <param name="roombref"></param>
        /// <param name="roomWifi"></param>
        /// <param name="opeuserid"></param>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <returns></returns>
        public static Response<object> SaveRoomInfo(string beginDate, string endDate, string rateCode, string roomTypeCode, string roomnum, string roombref, string roomWifi, string opeuserid, string mac, string hId)
        {
            try
            {
                var body = "";
                var roombody = "";

                var roomlist = roomTypeCode.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
                var roomCode = "";
                if (roomnum == "-1")
                {
                    foreach (string roomcd in roomlist)
                    {
                        roomCode = roomcd.Substring(25);
                        roomCode = (roomCode.Split('_').Count() > 0) ? roomCode.Split('_')[1] : "";
                        roombody += "{\"roomTypeCode\":\"" + roomCode + "\",\"status\":" + "false" + "},";
                    }
                    roombody = "[" + roombody.TrimEnd(',') + "]";
                    body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"updateUser\":\"{4}\",\"whatDay\":\"{5}\",\"rateCode\":\"{6}\",\"lmroom\":{7}", mac, hId, beginDate, endDate, opeuserid, "1,2,3,4,5,6,7", rateCode, roombody);
                }
                else
                {
                    foreach (string roomcd in roomlist)
                    {
                        roomCode = roomcd.Substring(25);
                        roomCode = (roomCode.Split('_').Count() > 0) ? roomCode.Split('_')[1] : "";
                        roombody += "{\"roomTypeCode\":\"" + roomCode + "\",\"status\":" + "true" + "},";
                    }
                    roombody = "[" + roombody.TrimEnd(',') + "]";
                    body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"updateUser\":\"{4}\",\"whatDay\":\"{5}\",\"rateCode\":\"{6}\",\"roomNum\":\"{7}\",\"lmroom\":{8}", mac, hId, beginDate, endDate, opeuserid, "1,2,3,4,5,6,7", rateCode, roomnum, roombody);
                }
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "plan/saveOrUpdatePlan.json", body);
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

        public static Response<object> SaveRoomStatusInfo(string rateCode, string roomTypeCode, string roomnum, string status, string twoprice, string opeuserid, string mac, string hId)
        {
            try
            {
                var beginDate = DateTime.Now.ToString("yyyy-MM-dd");
                var endDate = DateTime.Now.ToString("yyyy-MM-dd");
                var body = "";
                var roombody = "";
                if (status == "-1")
                {
                    roombody = "[{\"roomTypeCode\":\"" + roomTypeCode + "\",\"status\":" + "false" + "}]";
                    body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"updateUser\":\"{4}\",\"whatDay\":\"{5}\",\"rateCode\":\"{6}\",\"lmroom\":{7}", mac, hId, beginDate, endDate, opeuserid, "1,2,3,4,5,6,7", rateCode, roombody);
                }
                else if (status == "0")
                {
                    roombody = "[{\"roomTypeCode\":\"" + roomTypeCode + "\",\"status\":" + "true" + "}]";
                    body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"updateUser\":\"{4}\",\"whatDay\":\"{5}\",\"rateCode\":\"{6}\",\"roomNum\":\"{7}\",\"lmroom\":{8}", mac, hId, beginDate, endDate, opeuserid, "1,2,3,4,5,6,7", rateCode, 0, roombody);
                }
                else if (status == "1")
                {
                    roombody = "[{\"roomTypeCode\":\"" + roomTypeCode + "\",\"status\":" + "true" + "}]";
                    body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"updateUser\":\"{4}\",\"whatDay\":\"{5}\",\"rateCode\":\"{6}\",\"roomNum\":\"{7}\",\"twoPrice\":{8},\"lmroom\":{9}", mac, hId, beginDate, endDate, opeuserid, "1,2,3,4,5,6,7", rateCode, roomnum, twoprice, roombody);
                }
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "plan/saveOrUpdatePlan.json", body);
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
