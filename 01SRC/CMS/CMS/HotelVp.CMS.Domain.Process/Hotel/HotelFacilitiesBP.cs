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
    public abstract class HotelFacilitiesBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.HotelFacilitiesBP  Method: ";

        public static HotelFacilitiesEntity ServiceTypeSelect(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "ServiceTypeSelect";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.ServiceTypeSelect(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "ServiceTypeSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity ServiceAllFTSelect(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "ServiceAllFTSelect";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.ServiceAllFTSelect(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "ServiceAllFTSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity CommonFtTypeSelect(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "CommonFtTypeSelect";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.CommonFtTypeSelect(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "CommonFtTypeSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity FtTypeSelect(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtTypeSelect";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.FtTypeSelect(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtTypeSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity GetFtTypeMaxSeq(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "GetFtTypeMaxSeq";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.GetFtTypeMaxSeq(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "GetFtTypeMaxSeq  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity GetFtHotelMaxForUpdate(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "GetFtHotelMaxForUpdate";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.GetFtHotelMaxForUpdate(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "GetFtHotelMaxForUpdate  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity GetFtHotelMaxSeq(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "GetFtHotelMaxSeq";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.GetFtHotelMaxSeq(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "GetFtHotelMaxSeq  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity FtDetailSelect(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtDetailSelect";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.FtDetailSelect(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtDetailSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity BindHotelList(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "BindHotelList";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.BindHotelList(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "BindHotelList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity FacilitiesTypeSelect(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FacilitiesTypeSelect";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.FacilitiesTypeSelect(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FacilitiesTypeSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelFacilitiesEntity SelectTypeDetail(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "SelectTypeDetail";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.SelectTypeDetail(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "SelectTypeDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.Insert(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static int FtTypeInsert(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtTypeInsert";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.FtTypeInsert(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtTypeInsert  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static int HotelInsert(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "HotelInsert";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.HotelInsert(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "HotelInsert  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.Update(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static int FtTypeUpdate(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtTypeUpdate";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.FtTypeUpdate(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtTypeUpdate  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static int FtSeqListUpdate(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtSeqListUpdate";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.FtSeqListUpdate(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtSeqListUpdate  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }

        public static int FtTypeSeqListUpdate(HotelFacilitiesEntity hotelFacilitiesEntity)
        {
            hotelFacilitiesEntity.LogMessages.MsgType = MessageType.INFO;
            hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtTypeSeqListUpdate";
            LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);

            try
            {
                return HotelFacilitiesDA.FtTypeSeqListUpdate(hotelFacilitiesEntity);
            }
            catch (Exception ex)
            {
                hotelFacilitiesEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelFacilitiesEntity.LogMessages.Content = _nameSpaceClass + "FtTypeSeqListUpdate  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelFacilitiesEntity.LogMessages);
                throw ex;
            }
        }
    }
}