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
    public abstract class SysCnfBP
    {
        /// <summary>
        /// 今日房态取得
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static Response<List<OpeUser>> GetIndexMainList(string mac, string hId)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return SysCnfAPI.GetIndexMainList(mac, hId);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }


        public static Response<object> DeleteUserInfo(string userId, string userName, string opeuserid, string mac, string hid)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return SysCnfAPI.DeleteUserInfo(userId, userName, opeuserid, mac, hid);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        public static Response<object> SaveUserInfo(string userId, string userName, string tel, string status, string remark, string opeuserid, string action, string mac, string hid)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return SysCnfAPI.SaveUserInfo(userId, userName, tel, status, remark, opeuserid, action, mac, hid);
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
