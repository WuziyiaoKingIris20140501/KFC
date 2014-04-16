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
    public abstract class CityBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.CityBP  Method: ";

        public static CityEntity CommonSelectFogToCity(CityEntity cityEntity)
        {
            cityEntity.LogMessages.MsgType = MessageType.INFO;
            cityEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectFogToCity";
            LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.SelectFogToCity(cityEntity);
            }
            catch (Exception ex)
            {
                cityEntity.LogMessages.MsgType = MessageType.ERROR;
                cityEntity.LogMessages.Content = _nameSpaceClass + "CommonSelectFogToCity  Error: " + ex.Message;
                LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }



        public static CityEntity CommonSelect(CityEntity cityEntity)
        {
            cityEntity.LogMessages.MsgType = MessageType.INFO;
            cityEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.CommonSelect(cityEntity);
            }
            catch (Exception ex)
            {
                cityEntity.LogMessages.MsgType = MessageType.ERROR;
                cityEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }

        public static CityEntity Select(CityEntity cityEntity)
        {
            cityEntity.LogMessages.MsgType = MessageType.INFO;
            cityEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.Select(cityEntity);
            }
            catch (Exception ex)
            {
                cityEntity.LogMessages.MsgType = MessageType.ERROR;
                cityEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }

        public static CityEntity MainSelect(CityEntity cityEntity)
        {
            cityEntity.LogMessages.MsgType = MessageType.INFO;
            cityEntity.LogMessages.Content = _nameSpaceClass + "MainSelect";
            LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.MainSelect(cityEntity);
            }
            catch (Exception ex)
            {
                cityEntity.LogMessages.MsgType = MessageType.ERROR;
                cityEntity.LogMessages.Content = _nameSpaceClass + "MainSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }

        public static CityEntity FOGMainSelect(CityEntity cityEntity)
        {
            cityEntity.LogMessages.MsgType = MessageType.INFO;
            cityEntity.LogMessages.Content = _nameSpaceClass + "FOGMainSelect";
            LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.FOGMainSelect(cityEntity);
            }
            catch (Exception ex)
            {
                cityEntity.LogMessages.MsgType = MessageType.ERROR;
                cityEntity.LogMessages.Content = _nameSpaceClass + "FOGMainSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(CityEntity cityEntity)
        {
            cityEntity.LogMessages.MsgType = MessageType.INFO;
            cityEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.Insert(cityEntity);
            }
            catch (Exception ex)
            {
                cityEntity.LogMessages.MsgType = MessageType.ERROR;
                cityEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(CityEntity cityEntity)
        {
            cityEntity.LogMessages.MsgType = MessageType.INFO;
            cityEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.Update(cityEntity);
            }
            catch (Exception ex)
            {
                cityEntity.LogMessages.MsgType = MessageType.ERROR;
                cityEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }
    }
}