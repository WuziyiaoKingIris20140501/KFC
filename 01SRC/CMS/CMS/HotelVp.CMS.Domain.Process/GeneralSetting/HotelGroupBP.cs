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
    public abstract class HotelGroupBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.HotelGroupBP  Method: ";
        public static HotelGroupEntity Select(HotelGroupEntity hotelGroupEntity)
        {
            hotelGroupEntity.LogMessages.MsgType = MessageType.INFO;
            hotelGroupEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(hotelGroupEntity.LogMessages);

            try
            {
                return HotelGroupDA.Select(hotelGroupEntity);
            }
            catch (Exception ex)
            {
                hotelGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelGroupEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(HotelGroupEntity hotelGroupEntity)
        {
            hotelGroupEntity.LogMessages.MsgType = MessageType.INFO;
            hotelGroupEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(hotelGroupEntity.LogMessages);

            try
            {
                return HotelGroupDA.Insert(hotelGroupEntity);
            }
            catch (Exception ex)
            {
                hotelGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelGroupEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(HotelGroupEntity hotelGroupEntity)
        {
            hotelGroupEntity.LogMessages.MsgType = MessageType.INFO;
            hotelGroupEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(hotelGroupEntity.LogMessages);

            try
            {
                return HotelGroupDA.Update(hotelGroupEntity);
            }
            catch (Exception ex)
            {
                hotelGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelGroupEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelGroupEntity.LogMessages);
                throw ex;
            }
        }
    }
}