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
    public abstract class HotelPlanInfoBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.HotelInfoBP  Method: ";

        public static HotelInfoEntity GetHotelList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelPlanInfoDA.GetHotelList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

    }
}
