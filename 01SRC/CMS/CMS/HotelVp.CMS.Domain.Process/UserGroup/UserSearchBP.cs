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
using HotelVp.CMS.Domain.ServiceAdapter;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class UserSearchBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.UserSearchBP  Method: ";

        public static UserEntity CommonSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.CommonSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity GetRegChannelList(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.GetRegChannelList(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity GetPlatFormList(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.GetPlatFormList(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity CreateUserSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "CreateUserSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.CreateUserSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "CreateUserSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity ReviewSelectCount(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectCount";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.ReviewSelectCount(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity GetUserIDFromMobile(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "GetUserIDFromMobile";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.GetUserIDFromMobile(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "GetUserIDFromMobile  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity ReviewSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.ReviewSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity ExportReviewSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "ExportReviewSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.ExportReviewSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "ExportReviewSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity UserDetailListSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.UserDetailListSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity UserCashDetailListSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "UserCashDetailListSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.UserCashDetailListSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "UserCashDetailListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity UserCashPopListSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "UserCashPopListSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.UserCashPopListSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "UserCashPopListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity UserCashMainListSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "UserCashMainListSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.UserCashMainListSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "UserCashMainListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity UserMainListSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "UserMainListSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.UserMainListSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "UserMainListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity getSignByPhoneForCC(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "getSignByPhoneForCC";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchSA.getSignByPhoneForCC(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "getSignByPhoneForCC  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity ReviewConsultRoomUserSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "ReviewConsultRoomUserSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.ReviewConsultRoomUserSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "ReviewConsultRoomUserSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity ReviewConsultPOrderUserSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "ReviewConsultPOrderUserSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.ReviewConsultPOrderUserSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "ReviewConsultPOrderUserSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity PreConsultRoomUserSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "PreConsultRoomUserSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.PreConsultRoomUserSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "PreConsultRoomUserSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity PreConsultPOrderUserSelect(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "PreConsultPOrderUserSelect";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.PreConsultPOrderUserSelect(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "PreConsultPOrderUserSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity ReviewConsultRoomUserDetail(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "ReviewConsultRoomUserDetail";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.ReviewConsultRoomUserDetail(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "ReviewConsultRoomUserDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }

        public static UserEntity ReviewConsultPOrderUserDetail(UserEntity userEntity)
        {
            userEntity.LogMessages.MsgType = MessageType.INFO;
            userEntity.LogMessages.Content = _nameSpaceClass + "ReviewConsultPOrderUserDetail";
            LoggerHelper.LogWriter(userEntity.LogMessages);

            try
            {
                return UserSearchDA.ReviewConsultPOrderUserDetail(userEntity);
            }
            catch (Exception ex)
            {
                userEntity.LogMessages.MsgType = MessageType.ERROR;
                userEntity.LogMessages.Content = _nameSpaceClass + "ReviewConsultPOrderUserDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(userEntity.LogMessages);
                throw ex;
            }
        }
    }
}