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
    public abstract class UserGroupBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.UserGroupBP  Method: ";

        public static UserGroupEntity CommonSelect(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.CommonSelect(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity GetRegChannelList(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.GetRegChannelList(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity GetPlatFormList(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.GetPlatFormList(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity CreateUserSelect(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "CreateUserSelect";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.CreateUserSelect(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "CreateUserSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity ReviewSelect(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.ReviewSelect(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity UserDetailListCount(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListCount";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.UserDetailListCount(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity UserDetailListQuery(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListQuery";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.UserDetailListQuery(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListQuery  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity ExportUserDetailList(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "ExportUserDetailList";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.ExportUserDetailList(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "ExportUserDetailList  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity UserDetailListSelect(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListSelect";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.UserDetailListSelect(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static UserGroupEntity UserMainListSelect(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "UserMainListSelect";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.UserMainListSelect(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "UserMainListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.Insert(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertConsultRoomUser(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "InsertConsultRoomUser";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserGroupDA.InsertConsultRoomUser(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "InsertConsultRoomUser  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertConsultPOrderUser(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "InsertConsultPOrderUser";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserGroupDA.InsertConsultPOrderUser(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "InsertConsultPOrderUser  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeleteConsultRoomUser(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "DeleteConsultRoomUser";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserGroupDA.DeleteConsultRoomUser(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "DeleteConsultRoomUser  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.Update(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetHistoryInsert(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.LogMessages.MsgType = MessageType.INFO;
            userGroupEntity.LogMessages.Content = _nameSpaceClass + "GetHistoryInsert";
            LoggerHelper.LogWriter(userGroupEntity.LogMessages);

            try
            {
                return UserGroupDA.GetHistoryInsert(userGroupEntity);
            }
            catch (Exception ex)
            {
                userGroupEntity.LogMessages.MsgType = MessageType.ERROR;
                userGroupEntity.LogMessages.Content = _nameSpaceClass + "GetHistoryInsert  Error: " + ex.Message;
                LoggerHelper.LogWriter(userGroupEntity.LogMessages);
                throw ex;
            }
        }
    }
}