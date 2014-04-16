using System;
using System.Collections;
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
    public abstract class HotelInfoEXBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.HotelInfoEXBP  Method: ";

        public static HotelInfoEXEntity SelectHotelInfoEX(HotelInfoEXEntity hotelinfoEXEntity)
        {
            hotelinfoEXEntity.LogMessages.MsgType = MessageType.INFO;
            hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "SelectHotelInfoEX";
            LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);

            try
            {
                return HotelInfoEXDA.SelectHotelInfoEX(hotelinfoEXEntity);
            }
            catch (Exception ex)
            {
                hotelinfoEXEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "SelectHotelInfoEX  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertHotelInfoEX(HotelInfoEXEntity hotelinfoEXEntity)
        {
            hotelinfoEXEntity.LogMessages.MsgType = MessageType.INFO;
            hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelInfoEX";
            LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);

            try
            {
                return HotelInfoEXDA.InsertHotelInfoEX(hotelinfoEXEntity);
            }
            catch (Exception ex)
            {
                hotelinfoEXEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelInfoEX  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);
                throw ex;
            }
        }

        public static int SaveHotelInfoEX(HotelInfoEXEntity hotelinfoEXEntity)
        {
            hotelinfoEXEntity.LogMessages.MsgType = MessageType.INFO;
            hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "SaveHotelInfoEX";
            LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);

            try
            {
                return HotelInfoEXDA.SaveHotelInfoEX(hotelinfoEXEntity);
            }
            catch (Exception ex)
            {
                hotelinfoEXEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "SaveHotelInfoEX  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);
                throw ex;
            }
        }

        public static int UpdateHotelInfoEX(HotelInfoEXEntity hotelinfoEXEntity)
        {
            hotelinfoEXEntity.LogMessages.MsgType = MessageType.INFO;
            hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelInfoEX";
            LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);

            try
            {
                return HotelInfoEXDA.UpdateHotelInfoEX(hotelinfoEXEntity);
            }
            catch (Exception ex)
            {
                hotelinfoEXEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelInfoEX  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);
                throw ex;
            }
        }

        public static int UpdateHotelInfoEXByConsultRoom(HotelInfoEXEntity hotelinfoEXEntity)
        {
            hotelinfoEXEntity.LogMessages.MsgType = MessageType.INFO;
            hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelInfoEXByConsultRoom";
            LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);

            try
            {
                return HotelInfoEXDA.UpdateHotelInfoEXByConsultRoom(hotelinfoEXEntity);
            }
            catch (Exception ex)
            {
                hotelinfoEXEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelInfoEXByConsultRoom  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);
                throw ex;
            }
        }

        public static void InsertHotelEXHistory(HotelInfoEXEntity hotelinfoEXEntity)
        {
            hotelinfoEXEntity.LogMessages.MsgType = MessageType.INFO;
            hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelEXHistory";
            LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);

            try
            {
                HotelInfoEXDA.InsertHotelEXHistory(hotelinfoEXEntity);
            }
            catch (Exception ex)
            {
                hotelinfoEXEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelinfoEXEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelEXHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelinfoEXEntity.LogMessages);
                throw ex;
            }
        }

    }
}