using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess.GeneralSetting;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity.GeneralSetting;

namespace HotelVp.CMS.Domain.Process.GeneralSetting
{
    public abstract class TagInfoBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.GeneralSetting.TagInfoBP  Method: ";

        public static TagInfoEntity TagInfoSearch(TagInfoEntity tagInfoEntity)
        {
            tagInfoEntity.LogMessages.MsgType = MessageType.INFO;
            tagInfoEntity.LogMessages.Content = _nameSpaceClass + "TagInfoSearch";
            LoggerHelper.LogWriter(tagInfoEntity.LogMessages);

            try
            {
                return TagInfoDA.TagInfoSearch(tagInfoEntity);
            }
            catch (Exception ex)
            {
                tagInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                tagInfoEntity.LogMessages.Content = _nameSpaceClass + "TagInfoSearch  Error: " + ex.Message;
                LoggerHelper.LogWriter(tagInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static TagInfoEntity TagInfoSearchCount(TagInfoEntity tagInfoEntity)
        {
            tagInfoEntity.LogMessages.MsgType = MessageType.INFO;
            tagInfoEntity.LogMessages.Content = _nameSpaceClass + "TagInfoSearchCount";
            LoggerHelper.LogWriter(tagInfoEntity.LogMessages);

            try
            {
                return TagInfoDA.TagInfoSearchCount(tagInfoEntity);
            }
            catch (Exception ex)
            {
                tagInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                tagInfoEntity.LogMessages.Content = _nameSpaceClass + "TagInfoSearchCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(tagInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static int TagInfoInsert(TagInfoEntity tagInfoEntity)
        {
            tagInfoEntity.LogMessages.MsgType = MessageType.INFO;
            tagInfoEntity.LogMessages.Content = _nameSpaceClass + "TagInfoInsert";
            LoggerHelper.LogWriter(tagInfoEntity.LogMessages);

            try
            {
                return TagInfoDA.TagInfoInsert(tagInfoEntity);
            }
            catch (Exception ex)
            {
                tagInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                tagInfoEntity.LogMessages.Content = _nameSpaceClass + "TagInfoInsert  Error: " + ex.Message;
                LoggerHelper.LogWriter(tagInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static int TagInfoUpdate(TagInfoEntity tagInfoEntity)
        {
            tagInfoEntity.LogMessages.MsgType = MessageType.INFO;
            tagInfoEntity.LogMessages.Content = _nameSpaceClass + "TagInfoUpdate";
            LoggerHelper.LogWriter(tagInfoEntity.LogMessages);

            try
            {
                return TagInfoDA.TagInfoUpdate(tagInfoEntity);
            }
            catch (Exception ex)
            {
                tagInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                tagInfoEntity.LogMessages.Content = _nameSpaceClass + "TagInfoUpdate  Error: " + ex.Message;
                LoggerHelper.LogWriter(tagInfoEntity.LogMessages);
                throw ex;
            }
        }
    }
}
