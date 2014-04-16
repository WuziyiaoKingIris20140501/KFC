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
    public abstract class ChannelBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.ChannelBP  Method: ";

        public static ChannelEntity CommonSelect(ChannelEntity channelEntity)
        {
            channelEntity.LogMessages.MsgType = MessageType.INFO;
            channelEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(channelEntity.LogMessages);

            try
            {
                return ChannelDA.CommonSelect(channelEntity);
            }
            catch (Exception ex)
            {
                channelEntity.LogMessages.MsgType = MessageType.ERROR;
                channelEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(channelEntity.LogMessages);
                throw ex;
            }
        }

        public static ChannelEntity Select(ChannelEntity channelEntity)
        {
            channelEntity.LogMessages.MsgType = MessageType.INFO;
            channelEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(channelEntity.LogMessages);

            try
            {
                return ChannelDA.Select(channelEntity);
            }
            catch (Exception ex)
            {
                channelEntity.LogMessages.MsgType = MessageType.ERROR;
                channelEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(channelEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(ChannelEntity channelEntity)
        {
            channelEntity.LogMessages.MsgType = MessageType.INFO;
            channelEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(channelEntity.LogMessages);

            try
            {
                return ChannelDA.Insert(channelEntity);
            }
            catch (Exception ex)
            {
                channelEntity.LogMessages.MsgType = MessageType.ERROR;
                channelEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(channelEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(ChannelEntity channelEntity)
        {
            channelEntity.LogMessages.MsgType = MessageType.INFO;
            channelEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(channelEntity.LogMessages);
            
            try
            {
                return ChannelDA.Update(channelEntity);
            }
            catch (Exception ex)
            {
                channelEntity.LogMessages.MsgType = MessageType.ERROR;
                channelEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(channelEntity.LogMessages);
                throw ex;
            } 
        }
    }
}