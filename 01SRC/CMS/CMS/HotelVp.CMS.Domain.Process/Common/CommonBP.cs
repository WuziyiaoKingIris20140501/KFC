using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class CommonBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.CommonBP  Method: ";

        public static int InsertEventHistory(CommonEntity commonEntity)
        {
            commonEntity.LogMessages.MsgType = MessageType.INFO;
            commonEntity.LogMessages.Content = _nameSpaceClass + "InsertEventHistory";
            LoggerHelper.LogWriter(commonEntity.LogMessages);

            try
            {
                return CommonDA.InsertEventHistory(commonEntity);
            }
            catch (Exception ex)
            {
                commonEntity.LogMessages.MsgType = MessageType.ERROR;
                commonEntity.LogMessages.Content = _nameSpaceClass + "InsertEventHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(commonEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertOrderActionHistory(OrderInfoEntity OrderEntity)
        {
            OrderEntity.LogMessages.MsgType = MessageType.INFO;
            OrderEntity.LogMessages.Content = _nameSpaceClass + "InsertOrderActionHistory";
            LoggerHelper.LogWriter(OrderEntity.LogMessages);

            try
            {
                return CommonDA.InsertOrderActionHistory(OrderEntity);
            }
            catch (Exception ex)
            {
                OrderEntity.LogMessages.MsgType = MessageType.ERROR;
                OrderEntity.LogMessages.Content = _nameSpaceClass + "InsertOrderActionHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(OrderEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertOrderActionHistoryList(OrderInfoEntity OrderEntity)
        {
            OrderEntity.LogMessages.MsgType = MessageType.INFO;
            OrderEntity.LogMessages.Content = _nameSpaceClass + "InsertOrderActionHistoryList";
            LoggerHelper.LogWriter(OrderEntity.LogMessages);

            try
            {
                return CommonDA.InsertOrderActionHistoryList(OrderEntity);
            }
            catch (Exception ex)
            {
                OrderEntity.LogMessages.MsgType = MessageType.ERROR;
                OrderEntity.LogMessages.Content = _nameSpaceClass + "InsertOrderActionHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(OrderEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetConfigList(string Typetring)
        {
            try
            {
                return CommonDA.GetConfigList(Typetring);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetConfigListForSort(string Typetring)
        {
            try
            {
                return CommonDA.GetConfigListForSort(Typetring);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet GetConfigVale(string TypeString, string KeyString)
        {
            try
            {
                return CommonDA.GetConfigVale(TypeString, KeyString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CommonEntity GetEventHistoryList(CommonEntity commonEntity)
        {
            commonEntity.LogMessages.MsgType = MessageType.INFO;
            commonEntity.LogMessages.Content = _nameSpaceClass + "GetEventHistoryList";
            LoggerHelper.LogWriter(commonEntity.LogMessages);

            try
            {
                return CommonDA.GetEventHistoryList(commonEntity);
            }
            catch (Exception ex)
            {
                commonEntity.LogMessages.MsgType = MessageType.ERROR;
                commonEntity.LogMessages.Content = _nameSpaceClass + "GetEventHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(commonEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertConsultRoomHistory(APPContentEntity APPContentEntity)
        {
            APPContentEntity.LogMessages.MsgType = MessageType.INFO;
            APPContentEntity.LogMessages.Content = _nameSpaceClass + "InsertConsultRoomHistory";
            LoggerHelper.LogWriter(APPContentEntity.LogMessages);

            try
            {
                return CommonDA.InsertConsultRoomHistory(APPContentEntity);
            }
            catch (Exception ex)
            {
                APPContentEntity.LogMessages.MsgType = MessageType.ERROR;
                APPContentEntity.LogMessages.Content = _nameSpaceClass + "InsertConsultRoomHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(APPContentEntity.LogMessages);
                throw ex;
            }
        }

    }
}