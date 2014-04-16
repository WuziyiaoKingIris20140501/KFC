using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.ServiceAdapter;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class TicketInfoBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.TicketInfoBP  Method: ";
        //public static TicketInfoEntity CommonHotelGroupSelect(TicketInfoEntity ticketInfoEntity)
        //{
        //    ticketInfoEntity.LogMessages.MsgType = MessageType.INFO;
        //    ticketInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
        //    LoggerHelper.LogWriter(ticketInfoEntity.LogMessages);

        //    try
        //    {
        //        return TicketInfoDA.CommonHotelGroupSelect(ticketInfoEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        ticketInfoEntity.LogMessages.MsgType = MessageType.ERROR;
        //        ticketInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
        //        LoggerHelper.LogWriter(ticketInfoEntity.LogMessages);
        //        throw ex;
        //    }
        //}

        public static TicketInfoEntity BindTicketInfoList(TicketInfoEntity ticketInfoEntity)
        {
            ticketInfoEntity.LogMessages.MsgType = MessageType.INFO;
            ticketInfoEntity.LogMessages.Content = _nameSpaceClass + "BindTicketInfoList";
            LoggerHelper.LogWriter(ticketInfoEntity.LogMessages);

            try
            {
                return TicketInfoDA.BindTicketInfoList(ticketInfoEntity);
            }
            catch (Exception ex)
            {
                ticketInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                ticketInfoEntity.LogMessages.Content = _nameSpaceClass + "BindTicketInfoList  Error: " + ex.Message;
                LoggerHelper.LogWriter(ticketInfoEntity.LogMessages);
                throw ex;
            }
        }

        //public static int HotelSave(TicketInfoEntity ticketInfoEntity)
        //{
        //    ticketInfoEntity.LogMessages.MsgType = MessageType.INFO;
        //    ticketInfoEntity.LogMessages.Content = _nameSpaceClass + "HotelSave";
        //    LoggerHelper.LogWriter(ticketInfoEntity.LogMessages);

        //    try
        //    {
        //        return TicketInfoDA.HotelSave(ticketInfoEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        ticketInfoEntity.LogMessages.MsgType = MessageType.ERROR;
        //        ticketInfoEntity.LogMessages.Content = _nameSpaceClass + "HotelSave  Error: " + ex.Message;
        //        LoggerHelper.LogWriter(ticketInfoEntity.LogMessages);
        //        throw ex;
        //    }
        //}
    }
}