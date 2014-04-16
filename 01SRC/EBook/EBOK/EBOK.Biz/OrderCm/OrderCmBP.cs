using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using HotelVp.EBOK.Domain.API;
using HotelVp.EBOK.Domain.API.Model;

namespace HotelVp.EBOK.Domain.Biz
{
    public abstract class OrderCmBP
    {
        /// <summary>
        /// 待处理/已处理 订单取得
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="orderstatus"></param>
        /// <returns></returns>
        public static Response<List<OrderList>> GetIndexMainList(string mac, string hId, string beginDate, string endDate, string orderstatus)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return OrderCmAPI.GetIndexMainList(mac, hId, beginDate, endDate, orderstatus);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        public static Response<List<OrderList>> GetIndexHisMainList(string mac, string hId, string beginDate, string endDate, string beginSDate, string endSDate, string orderstatus)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return OrderCmAPI.GetIndexHisMainList(mac, hId, beginDate, endDate, beginSDate, endSDate, orderstatus);
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
        /// 待处理/已处理 订单数量统计
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="hId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="orderstatus"></param>
        /// <returns></returns>
        public static int GetIndexOrderCount(string mac, string hId, string beginDate, string endDate, string orderstatus)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return OrderCmAPI.GetIndexOrderCount(mac, hId, beginDate, endDate, orderstatus);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        public static int GetIndexHisOrderCount(string mac, string hId, string beginDate, string endDate, string beginSDate, string endSDate, string orderstatus)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return OrderCmAPI.GetIndexHisOrderCount(mac, hId, beginDate, endDate, beginSDate, endSDate, orderstatus);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        public static Response<object> SaveConfirmOrderInfo(string orderid, string comfirmid, string remark, string actionType, string opeuserid, string mac, string hid)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return OrderCmAPI.SaveConfirmOrderInfo(orderid, comfirmid, remark, actionType, opeuserid, mac, hid);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        public static Response<List<OrderList>> GetOrderQueryList(string mac, string hId, string beginDate, string endDate, string cusid)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return OrderCmAPI.GetOrderQueryList(mac, hId, beginDate, endDate, cusid);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        public static Response<List<OrderList>> AjxGetOrderQueryPageList(string mac, string hId, string beginDate, string endDate, string cusid,int pageNum, int pageSize)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return OrderCmAPI.AjxGetOrderQueryPageList(mac, hId, beginDate, endDate, cusid, pageNum, pageSize);
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
