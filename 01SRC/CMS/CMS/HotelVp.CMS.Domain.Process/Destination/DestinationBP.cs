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
    public abstract class DestinationBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.DestinationBP  Method: ";


        public static DestinationEntity DestinationListSelect(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "DestinationListSelect";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.DestinationListSelect(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "DestinationListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        public static DestinationEntity CommonTypeSelect(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "CommonTypeSelect";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.CommonTypeSelect(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "CommonTypeSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        public static DestinationEntity TypeSelect(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "TypeSelect";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.TypeSelect(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "TypeSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        public static DestinationEntity CommonTypeSelectSigle(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "CommonTypeSelectSigle";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.CommonTypeSelectSigle(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "CommonTypeSelectSigle  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        public static DestinationEntity DestinationTypeDetail(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "DestinationTypeDetail";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.DestinationTypeDetail(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "DestinationTypeDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        public static DestinationEntity CitySelect(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "CitySelect";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.CitySelect(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "CitySelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

       

        public static DestinationEntity Select(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.Select(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        //public static DestinationEntity PlatFormSelect(DestinationEntity destinationEntity)
        //{
        //    destinationEntity.LogMessages.MsgType = MessageType.INFO;
        //    destinationEntity.LogMessages.Content = _nameSpaceClass + "PlatFormSelect";
        //    LoggerHelper.LogWriter(destinationEntity.LogMessages);

        //    try
        //    {
        //        return DestinationDA.PlatFormSelect(destinationEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        destinationEntity.LogMessages.MsgType = MessageType.ERROR;
        //        destinationEntity.LogMessages.Content = _nameSpaceClass + "PlatFormSelect  Error: " + ex.Message;
        //        throw ex;
        //    }
        //}

        public static int DestinationInsert(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "DestinationInsert";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                destinationEntity = DestinationDA.DestinationInsert(destinationEntity);
                if (destinationEntity.Result == 1)
                {
                    DestinationDA.DestinationUpdateBatchHotel(destinationEntity);
                }
                return destinationEntity.Result;
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "DestinationInsert  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        public static int DestinationUpdate(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "DestinationUpdate";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                int iResult = DestinationDA.DestinationUpdate(destinationEntity);
                if (iResult != 1)
                {
                    return iResult;
                }

                DestinationDA.DestinationUpdateBatchHotel(destinationEntity);
                return iResult;
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "DestinationUpdate  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.Insert(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(DestinationEntity destinationEntity)
        {
            destinationEntity.LogMessages.MsgType = MessageType.INFO;
            destinationEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(destinationEntity.LogMessages);

            try
            {
                return DestinationDA.Update(destinationEntity);
            }
            catch (Exception ex)
            {
                destinationEntity.LogMessages.MsgType = MessageType.ERROR;
                destinationEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(destinationEntity.LogMessages);
                throw ex;
            }
        }
    }
}