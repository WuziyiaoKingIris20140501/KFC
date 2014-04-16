using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.ServiceAdapter;
using HotelVp.CMS.Domain.DataAccess.GeneralSetting;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;

namespace HotelVp.CMS.Domain.Process.GeneralSetting
{
    public abstract class OrderSearchBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.OrderSearchBP  Method: ";

        public static LmSystemLogEntity ReviewOrderListSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewOrderListSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderSearchDA.ReviewOrderListSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewOrderListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewOrderListSelectCount(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewOrderListSelectCount";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderSearchDA.ReviewOrderListSelectCount(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewOrderListSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ExportOrderListSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ExportOrderListSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderSearchDA.ExportOrderListSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ExportOrderListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelectByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectByRests";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderSearchDA.ReviewLmOrderLogSelectByRests(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectByRests  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelectCountByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectCountByRests";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderSearchDA.ReviewLmOrderLogSelectCountByRests(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectCountByRests  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }
    }
}
