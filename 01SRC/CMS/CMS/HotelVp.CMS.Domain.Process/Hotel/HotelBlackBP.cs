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
using HotelVp.CMS.Domain.Entity.Hotel;
using HotelVp.CMS.Domain.DataAccess.Hotel;

namespace HotelVp.CMS.Domain.Process.Hotel
{
    public abstract class HotelBlackBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.HotelBlackBP  Method: ";

        public static HotelBlackEntity GetHotelBlackListByCount(HotelBlackEntity hotelblackEntity)
        {
            hotelblackEntity.LogMessages.MsgType = MessageType.INFO;
            hotelblackEntity.LogMessages.Content = _nameSpaceClass + "GetHotelBlackList";
            LoggerHelper.LogWriter(hotelblackEntity.LogMessages);

            try
            {
                return HotelBlackDA.GetHotelBlackListByCount(hotelblackEntity);
            }
            catch (Exception ex)
            {
                hotelblackEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelblackEntity.LogMessages.Content = _nameSpaceClass + "GetHotelBlackList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelblackEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelBlackEntity GetHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            hotelblackEntity.LogMessages.MsgType = MessageType.INFO;
            hotelblackEntity.LogMessages.Content = _nameSpaceClass + "GetHotelBlackList";
            LoggerHelper.LogWriter(hotelblackEntity.LogMessages);

            try
            {
                return HotelBlackDA.GetHotelBlackList(hotelblackEntity);
            }
            catch (Exception ex)
            {
                hotelblackEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelblackEntity.LogMessages.Content = _nameSpaceClass + "GetHotelBlackList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelblackEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            hotelblackEntity.LogMessages.MsgType = MessageType.INFO;
            hotelblackEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelBlackList";
            LoggerHelper.LogWriter(hotelblackEntity.LogMessages);

            try
            {
                return HotelBlackDA.InsertHotelBlackList(hotelblackEntity);
            }
            catch (Exception ex)
            {
                hotelblackEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelblackEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelBlackList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelblackEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeleteHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            hotelblackEntity.LogMessages.MsgType = MessageType.INFO;
            hotelblackEntity.LogMessages.Content = _nameSpaceClass + "DeleteHotelBlackList";
            LoggerHelper.LogWriter(hotelblackEntity.LogMessages);

            try
            {
                return HotelBlackDA.DeleteHotelBlackList(hotelblackEntity);
            }
            catch (Exception ex)
            {
                hotelblackEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelblackEntity.LogMessages.Content = _nameSpaceClass + "DeleteHotelBlackList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelblackEntity.LogMessages);
                throw ex;
            }
        }

        public static int UpdateHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            hotelblackEntity.LogMessages.MsgType = MessageType.INFO;
            hotelblackEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelBlackList";
            LoggerHelper.LogWriter(hotelblackEntity.LogMessages);

            try
            {
                return HotelBlackDA.UpdateHotelBlackList(hotelblackEntity);
            }
            catch (Exception ex)
            {
                hotelblackEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelblackEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelBlackList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelblackEntity.LogMessages);
                throw ex;
            }
        }

        public static int UpdateHotelBlackListByExist(HotelBlackEntity hotelblackEntity)
        {
            hotelblackEntity.LogMessages.MsgType = MessageType.INFO;
            hotelblackEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelBlackListByExist";
            LoggerHelper.LogWriter(hotelblackEntity.LogMessages);

            try
            {
                return HotelBlackDA.UpdateHotelBlackListByExist(hotelblackEntity);
            }
            catch (Exception ex)
            {
                hotelblackEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelblackEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelBlackListByExist  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelblackEntity.LogMessages);
                throw ex;
            }
        }


        public static HotelBlackEntity RepeatHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            hotelblackEntity.LogMessages.MsgType = MessageType.INFO;
            hotelblackEntity.LogMessages.Content = _nameSpaceClass + "RepeatHotelBlackList";
            LoggerHelper.LogWriter(hotelblackEntity.LogMessages);

            try
            {
                return HotelBlackDA.RepeatHotelBlackList(hotelblackEntity);
            }
            catch (Exception ex)
            {
                hotelblackEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelblackEntity.LogMessages.Content = _nameSpaceClass + "RepeatHotelBlackList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelblackEntity.LogMessages);
                throw ex;
            }
        }
    }
}
