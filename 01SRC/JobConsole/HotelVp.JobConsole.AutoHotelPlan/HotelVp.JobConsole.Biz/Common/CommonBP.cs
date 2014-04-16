using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.JobConsole.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.JobConsole.Entity;

namespace HotelVp.JobConsole.Biz
{
    public abstract class CommonBP
    {
        static string _nameSpaceClass = "HotelVp.JobConsole.Process.CommonBP  Method: ";

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

    }
}