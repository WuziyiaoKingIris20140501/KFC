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
    public abstract class SeltBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.SeltBP  Method: ";

        public static SeltEntity ReviewSelect(SeltEntity seltEntity)
        {
            seltEntity.LogMessages.MsgType = MessageType.INFO;
            seltEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect";
            LoggerHelper.LogWriter(seltEntity.LogMessages);

            try
            {
                return SeltDA.ReviewSelect(seltEntity);
            }
            catch (Exception ex)
            {
                seltEntity.LogMessages.MsgType = MessageType.ERROR;
                seltEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(seltEntity.LogMessages);
                throw ex;
            }
        }

        public static SeltEntity ReLoadDetialInfo(SeltEntity seltEntity)
        {
            seltEntity.LogMessages.MsgType = MessageType.INFO;
            seltEntity.LogMessages.Content = _nameSpaceClass + "ReLoadDetialInfo";
            LoggerHelper.LogWriter(seltEntity.LogMessages);

            try
            {
                return SeltDA.ReLoadDetialInfo(seltEntity);
            }
            catch (Exception ex)
            {
                seltEntity.LogMessages.MsgType = MessageType.ERROR;
                seltEntity.LogMessages.Content = _nameSpaceClass + "ReLoadDetialInfo  Error: " + ex.Message;
                LoggerHelper.LogWriter(seltEntity.LogMessages);
                throw ex;
            }
        }

        public static SeltEntity ReviewSelectCount(SeltEntity seltEntity)
        {
            seltEntity.LogMessages.MsgType = MessageType.INFO;
            seltEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectCount";
            LoggerHelper.LogWriter(seltEntity.LogMessages);

            try
            {
                return SeltDA.ReviewSelectCount(seltEntity);
            }
            catch (Exception ex)
            {
                seltEntity.LogMessages.MsgType = MessageType.ERROR;
                seltEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(seltEntity.LogMessages);
                throw ex;
            }
        }

        public static SeltEntity SaveSettlementInfo(SeltEntity seltEntity)
        {
            seltEntity.LogMessages.MsgType = MessageType.INFO;
            seltEntity.LogMessages.Content = _nameSpaceClass + "SaveSettlementInfo";
            LoggerHelper.LogWriter(seltEntity.LogMessages);

            try
            {
                return SeltDA.SaveSettlementInfo(seltEntity);
            }
            catch (Exception ex)
            {
                seltEntity.LogMessages.MsgType = MessageType.ERROR;
                seltEntity.LogMessages.Content = _nameSpaceClass + "SaveSettlementInfo  Error: " + ex.Message;
                LoggerHelper.LogWriter(seltEntity.LogMessages);
                throw ex;
            }
        }

    }
}