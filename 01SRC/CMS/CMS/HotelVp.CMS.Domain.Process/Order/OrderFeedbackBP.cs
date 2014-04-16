using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess.Order;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity.Order;
using HotelVp.CMS.Domain.ServiceAdapter;

namespace HotelVp.CMS.Domain.Process.Order
{
    public abstract class OrderFeedbackBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.Order  Method: ";
        public static OrderFeedbackEntity BindOrderFeedBackList(OrderFeedbackEntity orderFeedbackEntity)
        {
            orderFeedbackEntity.LogMessages.MsgType = MessageType.INFO;
            orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "BindOrderFeedBackList";
            LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);

            try
            {
                return OrderFeedbackDA.BindOrderFeedBackList(orderFeedbackEntity);
            }
            catch (Exception ex)
            {
                orderFeedbackEntity.LogMessages.MsgType = MessageType.ERROR;
                orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "BindOrderFeedBackList  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);
                throw ex;
            }
        }


        public static OrderFeedbackEntity BindOrderDetailsByOrderNum(OrderFeedbackEntity orderFeedbackEntity)
        {
            orderFeedbackEntity.LogMessages.MsgType = MessageType.INFO;
            orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "BindOrderDetailsByOrderNum";
            LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);

            try
            {
                return OrderFeedbackDA.BindOrderDetailsByOrderNum(orderFeedbackEntity);
            }
            catch (Exception ex)
            {
                orderFeedbackEntity.LogMessages.MsgType = MessageType.ERROR;
                orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "BindOrderDetailsByOrderNum  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);
                throw ex;
            }
        }

        public static string GetUserTel(OrderFeedbackEntity orderFeedbackEntity, string userAccount)
        {
            orderFeedbackEntity.LogMessages.MsgType = MessageType.INFO;
            orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "GetUserTel";
            LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);

            try
            {
                return OrderFeedbackDA.GetUserTel(userAccount);
            }
            catch (Exception ex)
            {
                orderFeedbackEntity.LogMessages.MsgType = MessageType.ERROR;
                orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "GetUserTel  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderFeedbackEntity UpdateOrderFeedBack(OrderFeedbackEntity orderFeedbackEntity)
        {
            orderFeedbackEntity.LogMessages.MsgType = MessageType.INFO;
            orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "UpdateOrderFeedBack";
            LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);

            try
            {
                return OrderFeedbackDA.UpdateOrderFeedBack(orderFeedbackEntity);
            }
            catch (Exception ex)
            {
                orderFeedbackEntity.LogMessages.MsgType = MessageType.ERROR;
                orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "UpdateOrderFeedBack  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderFeedbackEntity UpdateOrderBookStatusOther(OrderFeedbackEntity orderFeedbackEntity)
        {
            orderFeedbackEntity.LogMessages.MsgType = MessageType.INFO;
            orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "UpdateOrderBookStatusOther";
            LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);

            try
            {
                return OrderFeedbackDA.UpdateOrderBookStatusOther(orderFeedbackEntity);
            }
            catch (Exception ex)
            {
                orderFeedbackEntity.LogMessages.MsgType = MessageType.ERROR;
                orderFeedbackEntity.LogMessages.Content = _nameSpaceClass + "UpdateOrderBookStatusOther  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderFeedbackEntity.LogMessages);
                throw ex;
            }
        }

    }
}
