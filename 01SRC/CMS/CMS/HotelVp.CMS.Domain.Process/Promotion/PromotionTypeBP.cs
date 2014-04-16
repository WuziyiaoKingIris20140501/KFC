using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.CMS.Domain.DataAccess.Promotion;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Entity.Promotion;

namespace HotelVp.CMS.Domain.Process.Promotion
{
    public abstract class PromotionTypeBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.PromotionTypeBP  Method: ";

        public static PromotionTypeEntity CommonSelect(PromotionTypeEntity promotionTypeEntity)
        {
            promotionTypeEntity.LogMessages.MsgType = MessageType.INFO;
            promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);

            try
            {
                return PromotionTypeDA.CommonSelect(promotionTypeEntity);
            }
            catch (Exception ex)
            {
                promotionTypeEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(PromotionTypeEntity promotionTypeEntity)
        {
            promotionTypeEntity.LogMessages.MsgType = MessageType.INFO;
            promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);

            try
            {
                return PromotionTypeDA.Insert(promotionTypeEntity);
            }
            catch (Exception ex)
            {
                promotionTypeEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(PromotionTypeEntity promotionTypeEntity)
        {
            promotionTypeEntity.LogMessages.MsgType = MessageType.INFO;
            promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);

            try
            {
                return PromotionTypeDA.Update(promotionTypeEntity);
            }
            catch (Exception ex)
            {
                promotionTypeEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);
                throw ex;
            }
        }

        public static int Delete(PromotionTypeEntity promotionTypeEntity)
        {
            promotionTypeEntity.LogMessages.MsgType = MessageType.INFO;
            promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "Delete";
            LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);

            try
            {
                return PromotionTypeDA.Delete(promotionTypeEntity);
            }
            catch (Exception ex)
            {
                promotionTypeEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "Delete  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);
                throw ex;
            }
        }

        //获取最大排序号
        public static int GetMaxSeq(PromotionTypeEntity promotionTypeEntity)
        {
            promotionTypeEntity.LogMessages.MsgType = MessageType.INFO;
            promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "GetMaxSeq";
            LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);

            try
            {
                return PromotionTypeDA.GetMaxSeq();
            }
            catch (Exception ex)
            {
                promotionTypeEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionTypeEntity.LogMessages.Content = _nameSpaceClass + "GetMaxSeq  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionTypeEntity.LogMessages);
                throw ex;
            }
        }

    }
}
