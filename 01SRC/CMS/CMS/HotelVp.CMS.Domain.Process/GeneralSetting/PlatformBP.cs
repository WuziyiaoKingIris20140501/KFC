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
    public abstract class PlatformBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.PlatformBP  Method: ";

        public static PlatformEntity Select(PlatformEntity platformEntity)
        {
            platformEntity.LogMessages.MsgType = MessageType.INFO;
            platformEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(platformEntity.LogMessages);

            try
            {
                return PlatformDA.Select(platformEntity);
            }
            catch (Exception ex)
            {
                platformEntity.LogMessages.MsgType = MessageType.ERROR;
                platformEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(platformEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(PlatformEntity platformEntity)
        {
            platformEntity.LogMessages.MsgType = MessageType.INFO;
            platformEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(platformEntity.LogMessages);

            try
            {
                return PlatformDA.Insert(platformEntity);
            }
            catch (Exception ex)
            {
                platformEntity.LogMessages.MsgType = MessageType.ERROR;
                platformEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(platformEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(PlatformEntity platformEntity)
        {
            platformEntity.LogMessages.MsgType = MessageType.INFO;
            platformEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(platformEntity.LogMessages);

            try
            {
                return PlatformDA.Update(platformEntity);
            }
            catch (Exception ex)
            {
                platformEntity.LogMessages.MsgType = MessageType.ERROR;
                platformEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(platformEntity.LogMessages);
                throw ex;
            }
        }
    }
}