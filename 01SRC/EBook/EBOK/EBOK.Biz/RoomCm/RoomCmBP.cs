using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Web.Mvc;

using HotelVp.EBOK.Domain.API;
using HotelVp.EBOK.Domain.API.Model;

namespace HotelVp.EBOK.Domain.Biz
{
    public abstract class RoomCmBP
    {
        /// <summary>
        /// 今日房态取得
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Response<List<RoomList>> GetIndexMainList(string mac, string hId, string beginDate, string endDate)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return RoomCmAPI.GetIndexMainList(mac, hId, beginDate, endDate);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 今日计划房型取得
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Response<List<RoomList>> GetRoomMainList(string mac, string hId, string beginDate, string endDate)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return RoomCmAPI.GetIndexMainList(mac, hId, beginDate, endDate);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 计划房型取得
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static RoomPlan GetRoomPlanListMainList(string mac, string hId, string beginDate, string endDate)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                RoomPlan roompl = new RoomPlan();
                DataSet dsResult = new DataSet();
//                List<string> roomls = new List<string>();
                List<RoomList> lm2roomls = RoomCmAPI.GetRoomPlanList(mac, hId, beginDate, endDate, "LMBAR2").result;// 现付
                List<RoomList> lmroomls = RoomCmAPI.GetRoomPlanList(mac, hId, beginDate, endDate, "LMBAR").result;// 预付

                dsResult.Tables.Add(new DataTable());

                dsResult.Tables[0].Columns.Add("PLDATE");
                //dsResult.Tables[0].Columns.Add("RACECD");

                foreach (RoomList plan2 in lm2roomls)
                {
                    if (!dsResult.Tables[0].Columns.Contains(plan2.rateCode + "-" + plan2.roomTypeCode + "-" + plan2.roomTypeName))
                    {
                        dsResult.Tables[0].Columns.Add(plan2.rateCode + "-" + plan2.roomTypeCode + "-" + plan2.roomTypeName);
                    }
                }

                roompl.lm2Cols = dsResult.Tables[0].Columns.Count - 1;

                foreach (RoomList plan in lmroomls)
                {
                    if (!dsResult.Tables[0].Columns.Contains(plan.rateCode + "-" + plan.roomTypeCode + "-" + plan.roomTypeName))
                    {
                        dsResult.Tables[0].Columns.Add(plan.rateCode + "-" + plan.roomTypeCode + "-" + plan.roomTypeName);
                    }
                }

                roompl.lmCols = dsResult.Tables[0].Columns.Count - roompl.lm2Cols - 1;

                DateTime fromDate = DateTime.Parse(beginDate);
                DateTime toDate = DateTime.Parse(endDate);
                while (fromDate <= toDate)
                {
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["PLDATE"] = fromDate.ToString("yyyy-MM-dd");
                    dsResult.Tables[0].Rows.Add(drRow);
                    fromDate = fromDate.AddDays(1);
                }

                DateTime bgTDate = DateTime.Parse(beginDate);
                DateTime edTDate = DateTime.Parse(beginDate);
                int irow = 0;
                foreach (RoomList plan2 in lm2roomls)
                {
                    edTDate = DateTime.Parse(plan2.effectDateString);
                    irow = (edTDate-bgTDate).Days;
                    dsResult.Tables[0].Rows[irow][plan2.rateCode + "-" + plan2.roomTypeCode + "-" + plan2.roomTypeName] = plan2.status +"|"+ plan2.roomNum+"|"+plan2.twoPrice;
                    //dsResult.Tables[0].Rows[irow]["RACECD"] = plan2.rateCode;
                }

                irow = 0;
                edTDate = DateTime.Parse(beginDate);
                foreach (RoomList plan in lmroomls)
                {
                    edTDate = DateTime.Parse(plan.effectDateString);
                    irow = (edTDate - bgTDate).Days;
                    dsResult.Tables[0].Rows[irow][plan.rateCode + "-" + plan.roomTypeCode + "-" + plan.roomTypeName] = plan.status + "|" + plan.roomNum + "|" + plan.twoPrice;
                    //dsResult.Tables[0].Rows[irow]["RACECD"] = plan.rateCode;
                }

                roompl.planResult = dsResult;
//                roompl.Roomls = roomls;
                return roompl;
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 酒店操作用户
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetOpeUserList(string mac, string hId)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return RoomCmAPI.GetOpeUserList(mac, hId);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 酒店计划修改
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Response<object> SaveRoomInfo(string beginDate, string endDate, string rateCode, string roomTypeCode, string roomnum, string roombref, string roomWifi, string opeuserid, string mac, string hid)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return RoomCmAPI.SaveRoomInfo(beginDate, endDate, rateCode, roomTypeCode, roomnum, roombref, roomWifi, opeuserid, mac, hid);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        public static Response<object> SaveRoomStatusInfo(string ratecode, string roomcd, string roomnum, string status, string twoprice, string opeuserid, string mac, string hid)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return RoomCmAPI.SaveRoomStatusInfo(ratecode, roomcd, roomnum, status, twoprice, opeuserid, mac, hid);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }
    }
}
