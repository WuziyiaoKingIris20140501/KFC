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
    public abstract class PartnerBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.PartnerBP  Method: ";

        public static PartnerEntity Select(PartnerEntity partnerEntity)
        {
            partnerEntity.LogMessages.MsgType = MessageType.INFO;
            partnerEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(partnerEntity.LogMessages);

            try
            {
                return PartnerDA.Select(partnerEntity);
            }
            catch (Exception ex)
            {
                partnerEntity.LogMessages.MsgType = MessageType.ERROR;
                partnerEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(partnerEntity.LogMessages);
                throw ex;
            }
        }

        public static PartnerEntity ChartSelect(PartnerEntity partnerEntity)
        {
            partnerEntity.LogMessages.MsgType = MessageType.INFO;
            partnerEntity.LogMessages.Content = _nameSpaceClass + "ChartSelect";
            LoggerHelper.LogWriter(partnerEntity.LogMessages);

            try
            {
                return PartnerDA.ChartSelect(partnerEntity);
            }
            catch (Exception ex)
            {
                partnerEntity.LogMessages.MsgType = MessageType.ERROR;
                partnerEntity.LogMessages.Content = _nameSpaceClass + "ChartSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(partnerEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(PartnerEntity partnerEntity)
        {
            partnerEntity.LogMessages.MsgType = MessageType.INFO;
            partnerEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(partnerEntity.LogMessages);

            try
            {
                return PartnerDA.Insert(partnerEntity);
            }
            catch (Exception ex)
            {
                partnerEntity.LogMessages.MsgType = MessageType.ERROR;
                partnerEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(partnerEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(PartnerEntity partnerEntity)
        {
            partnerEntity.LogMessages.MsgType = MessageType.INFO;
            partnerEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(partnerEntity.LogMessages);
            
            try
            {
                return PartnerDA.Update(partnerEntity);
            }
            catch (Exception ex)
            {
                partnerEntity.LogMessages.MsgType = MessageType.ERROR;
                partnerEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(partnerEntity.LogMessages);
                throw ex;
            } 
        }
    }
}