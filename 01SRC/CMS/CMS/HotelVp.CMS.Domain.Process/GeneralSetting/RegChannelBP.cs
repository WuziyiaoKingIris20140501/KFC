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
    public abstract class RegChannelBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.RegChannelBP  Method: ";

        public static RegChannelEntity Select(RegChannelEntity regChannelEntity)
        {
            regChannelEntity.LogMessages.MsgType = MessageType.INFO;
            regChannelEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(regChannelEntity.LogMessages);

            try
            {
                return RegChannelDA.Select(regChannelEntity);
            }
            catch (Exception ex)
            {
                regChannelEntity.LogMessages.MsgType = MessageType.ERROR;
                regChannelEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(regChannelEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(RegChannelEntity regChannelEntity)
        {
            regChannelEntity.LogMessages.MsgType = MessageType.INFO;
            regChannelEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(regChannelEntity.LogMessages);

            try
            {
                return RegChannelDA.Insert(regChannelEntity);
            }
            catch (Exception ex)
            {
                regChannelEntity.LogMessages.MsgType = MessageType.ERROR;
                regChannelEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(regChannelEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(RegChannelEntity regChannelEntity)
        {
            regChannelEntity.LogMessages.MsgType = MessageType.INFO;
            regChannelEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(regChannelEntity.LogMessages);
            
            try
            {
                return RegChannelDA.Update(regChannelEntity);
            }
            catch (Exception ex)
            {
                regChannelEntity.LogMessages.MsgType = MessageType.ERROR;
                regChannelEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(regChannelEntity.LogMessages);
                throw ex;
            } 
        }
    }
}