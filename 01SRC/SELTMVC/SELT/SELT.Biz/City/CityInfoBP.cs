using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.SELT.Domain.DA;
using HotelVp.SELT.Domain.Entity;

namespace HotelVp.SELT.Domain.Biz
{
    public abstract class CityInfoBP
    {
        public static CityEntity Select(CityEntity cityEntity)
        {
            //cityEntity.LogMessages.MsgType = MessageType.INFO;
            //cityEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.Select(cityEntity);
            }
            catch (Exception ex)
            {
                //cityEntity.LogMessages.MsgType = MessageType.ERROR;
                //cityEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }

        public static CityEntity CommonSelect(CityEntity cityEntity)
        {
            //cityEntity.LogMessages.MsgType = MessageType.INFO;
            //cityEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.CommonSelect(cityEntity);
            }
            catch (Exception ex)
            {
                //cityEntity.LogMessages.MsgType = MessageType.ERROR;
                //cityEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }



        public static DataSet GetFaxUnknow()
        {
            //cityEntity.LogMessages.MsgType = MessageType.INFO;
            //cityEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return CityDA.GetFaxUnknow();
            }
            catch (Exception ex)
            {
                //cityEntity.LogMessages.MsgType = MessageType.ERROR;
                //cityEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }
    }
}
