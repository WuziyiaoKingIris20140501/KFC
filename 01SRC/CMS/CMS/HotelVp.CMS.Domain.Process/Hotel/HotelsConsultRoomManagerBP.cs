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
    public abstract class HotelsConsultRoomManagerBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.HotelsConsultRoomManagerBP  Method: ";

        /// <summary>
        /// 获取所有的销售人员（过滤计划需要）
        /// </summary>
        /// <param name="ValueString"></param>
        /// <returns></returns>
        public static DataSet GetSalesManagerList(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetSalesManagerList";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetSalesManagerList();
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetSalesManagerList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }


        public static HotelsConsultRoomManagerEntity GetConsultPeopleByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultPeopleByManager";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetConsultPeopleByManager(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultPeopleByManager  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetOracleHotelsByConsultPeopleByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultPeopleByManager";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetOracleHotelsByConsultPeopleByManager(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultPeopleByManager  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetEXDConsultHotelCountLogsByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetEXDConsultHotelCountLogsByManager";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetEXDConsultHotelCountLogsByManager(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetEXDConsultHotelCountLogsByManager  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetHotelsByKeysByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetHotelsByKeysByManager";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetHotelsByKeysByManager(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetHotelsByKeysByManager  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetConsultHotelsByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultHotelsByManager";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetConsultHotelsByManager(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultHotelsByManager  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetConsultManagerAllCitysByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultHotelsByManager";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetConsultManagerAllCitysByManager(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultHotelsByManager  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetConsultRoomByTimeManagerHistory(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomByTimeManagerHistory";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetConsultRoomByTimeManagerHistory(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomByTimeManagerHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetConsultRoomByTimeAndUserManagerHistory(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomByTimeAndUserManagerHistory";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetConsultRoomByTimeAndUserManagerHistory(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomByTimeAndUserManagerHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetAllCityByTimeManagerHistory(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetAllCityByTimeManagerHistory";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetAllCityByTimeManagerHistory(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetAllCityByTimeManagerHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetConsultHotelsBySales(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetAllCityByTimeManagerHistory";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetConsultHotelsBySales(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetAllCityByTimeManagerHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelsConsultRoomManagerEntity GetCheck18EXDConsultHotelCountLogsByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.INFO;
            hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetCheck18EXDConsultHotelCountLogsByManager";
            LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);

            try
            {
                return HotelsConsultRoomManagerDA.GetCheck18EXDConsultHotelCountLogsByManager(hotelsConsultRoomManagerEntity);
            }
            catch (Exception ex)
            {
                hotelsConsultRoomManagerEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelsConsultRoomManagerEntity.LogMessages.Content = _nameSpaceClass + "GetCheck18EXDConsultHotelCountLogsByManager  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelsConsultRoomManagerEntity.LogMessages);
                throw ex;
            }
        }
    }
}