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
    public abstract class CruiseInfoBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.CruiseInfoBP  Method: ";

        public static DataSet GetCruiseShipPlanInfo()
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.GetCruiseShipPlanInfo();
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetCruiseBoadPlanInfo(string shipID)
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.GetCruiseBoadPlanInfo(shipID);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }


        public static DataSet GetCruiseRoomPlanInfo(string BoatID)
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.GetCruiseRoomPlanInfo(BoatID);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetBoatRoomInfo(string BoatID)
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.GetBoatRoomInfo(BoatID);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }


        public static DataSet GetCruiseDataInfo(string CruiseID)
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.GetCruiseDataInfo(CruiseID);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetCruiseDataPlanInfo(string CruiseID, string SDtime, string EDtime)
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.GetCruiseDataPlanInfo(CruiseID, SDtime, EDtime);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static CruiseInfoEntity SaveCruiseInfo(CruiseInfoEntity cruiseInfoEntity)
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveCruiseInfo";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.SaveCruiseInfo(cruiseInfoEntity);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveCruiseInfo  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet ReviewRoutePlanHistory(string CruiseID)
        {
            try
            {
                return CruiseInfoDA.ReviewRoutePlanHistory(CruiseID);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveCruiseInfo  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static CruiseInfoEntity SaveCruisePlanInfo(CruiseInfoEntity cruiseInfoEntity)
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveCruiseInfo";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.SaveCruisePlanInfo(cruiseInfoEntity);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveCruiseInfo  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static CruiseInfoEntity SaveCruisePlanList(CruiseInfoEntity cruiseInfoEntity)
        {
            //cruiseInfoEntity.LogMessages.MsgType = MessageType.INFO;
            //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveCruiseInfo";
            //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);

            try
            {
                return CruiseInfoDA.SaveCruisePlanList(cruiseInfoEntity);
            }
            catch (Exception ex)
            {
                //cruiseInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //cruiseInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveCruiseInfo  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cruiseInfoEntity.LogMessages);
                throw ex;
            }
        }
    }
}
