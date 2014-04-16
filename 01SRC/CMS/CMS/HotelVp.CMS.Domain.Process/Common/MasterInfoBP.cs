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
    public abstract class MasterInfoBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.MasterInfoBP  Method: ";

        public static MasterInfoEntity CommonSelect(MasterInfoEntity masterInfoEntity)
        {
            masterInfoEntity.LogMessages.MsgType = MessageType.INFO;
            masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(masterInfoEntity.LogMessages);

            try
            {
                return MasterInfoDA.CommonSelect(masterInfoEntity);
            } 
            catch (Exception ex)
            {
                masterInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(masterInfoEntity.LogMessages);
                throw ex;
            }
        }


        public static MasterInfoEntity CommonSelectOrderChannelData(MasterInfoEntity masterInfoEntity)
        {
            masterInfoEntity.LogMessages.MsgType = MessageType.INFO;
            masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectOrderChannelData";
            LoggerHelper.LogWriter(masterInfoEntity.LogMessages);

            try
            {
                return MasterInfoDA.CommonSelectOrderChannelData(masterInfoEntity);
            }
            catch (Exception ex)
            {
                masterInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectOrderChannelData  Error: " + ex.Message;
                LoggerHelper.LogWriter(masterInfoEntity.LogMessages);
                throw ex;
            }
        } 

        public static MasterInfoEntity CommonSelectUser(MasterInfoEntity masterInfoEntity)
        {
            masterInfoEntity.LogMessages.MsgType = MessageType.INFO;
            masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectUser";
            LoggerHelper.LogWriter(masterInfoEntity.LogMessages);

            try
            {
                return MasterInfoDA.CommonSelectUser(masterInfoEntity);
            }
            catch (Exception ex)
            {
                masterInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectUser  Error: " + ex.Message;
                LoggerHelper.LogWriter(masterInfoEntity.LogMessages);
                throw ex;
            }
        }

                public static MasterInfoEntity CommonSelectUserNew(MasterInfoEntity masterInfoEntity)
        {
            masterInfoEntity.LogMessages.MsgType = MessageType.INFO;
            masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectUserNew";
            LoggerHelper.LogWriter(masterInfoEntity.LogMessages);

            try
            {
                return MasterInfoDA.CommonSelectUserNew(masterInfoEntity);
            }
            catch (Exception ex)
            {
                masterInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectUserNew  Error: " + ex.Message;
                LoggerHelper.LogWriter(masterInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static MasterInfoEntity CommonSelectTodayLoginUser(MasterInfoEntity masterInfoEntity)
        {
            masterInfoEntity.LogMessages.MsgType = MessageType.INFO;
            masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectTodayLoginUser";
            LoggerHelper.LogWriter(masterInfoEntity.LogMessages);

            try
            {
                return MasterInfoDA.CommonSelectTodayLoginUser(masterInfoEntity);
            }
            catch (Exception ex)
            {
                masterInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectTodayLoginUser  Error: " + ex.Message;
                LoggerHelper.LogWriter(masterInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static MasterInfoEntity CommonSelectTodayLoginUserNew(MasterInfoEntity masterInfoEntity)
        {
            masterInfoEntity.LogMessages.MsgType = MessageType.INFO;
            masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectTodayLoginUserNew";
            LoggerHelper.LogWriter(masterInfoEntity.LogMessages);

            try
            {
                return MasterInfoDA.CommonSelectTodayLoginUserNew(masterInfoEntity);
            }
            catch (Exception ex)
            {
                masterInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                masterInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectTodayLoginUserNew  Error: " + ex.Message;
                LoggerHelper.LogWriter(masterInfoEntity.LogMessages);
                throw ex;
            }
        }
    }
}