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
     public abstract class FAQBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.FAQBP  Method: ";

        public static FAQEntity CommonSelect(FAQEntity faqEntity)
        {
            faqEntity.LogMessages.MsgType = MessageType.INFO;
            faqEntity.LogMessages.Content = _nameSpaceClass + " CommonSelect";
            LoggerHelper.LogWriter(faqEntity.LogMessages);

            try
            {
                return  FAQDA.BindFAQList(faqEntity);
            }
            catch (Exception ex)
            {
                faqEntity.LogMessages.MsgType = MessageType.ERROR;
                faqEntity.LogMessages.Content = _nameSpaceClass + " CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(faqEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(FAQEntity faqEntity)
        {
            faqEntity.LogMessages.MsgType = MessageType.INFO;
            faqEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(faqEntity.LogMessages);
            try
            {                
                return FAQDA.Insert(faqEntity);
            }
            catch (Exception ex)
            {
                faqEntity.LogMessages.MsgType = MessageType.ERROR;
                faqEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(faqEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(FAQEntity faqEntity)
        {
            faqEntity.LogMessages.MsgType = MessageType.INFO;
            faqEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(faqEntity.LogMessages);

            try
            {               
                return FAQDA.UpdateFaqByID(faqEntity);
            }
            catch (Exception ex)
            {
                faqEntity.LogMessages.MsgType = MessageType.ERROR;
                faqEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(faqEntity.LogMessages);
                throw ex;
            }
        }

        public static int Delete(FAQEntity faqEntity)
        {
            faqEntity.LogMessages.MsgType = MessageType.INFO;
            faqEntity.LogMessages.Content = _nameSpaceClass + "Delete";
            LoggerHelper.LogWriter(faqEntity.LogMessages);
            try
            {
                return FAQDA.DeleteFaqByID(faqEntity);
            }
            catch (Exception ex)
            {
                faqEntity.LogMessages.MsgType = MessageType.ERROR;
                faqEntity.LogMessages.Content = _nameSpaceClass + "Delete  Error: " + ex.Message;
                LoggerHelper.LogWriter(faqEntity.LogMessages);
                throw ex;
            }
        }
    }
}
