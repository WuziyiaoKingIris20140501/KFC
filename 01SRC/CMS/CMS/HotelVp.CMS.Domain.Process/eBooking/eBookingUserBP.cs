using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity.eBooking;
using HotelVp.CMS.Domain.ServiceAdapter.eBooking;

namespace HotelVp.CMS.Domain.Process.eBooking
{
    public abstract class eBookingUserBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.eBookingUserBP  Method: ";

        public static eBookingUserEntity eBookingUserQuery(eBookingUserEntity eBookingUserEntity)
        {
            eBookingUserEntity.LogMessages.MsgType = MessageType.INFO;
            eBookingUserEntity.LogMessages.Content = _nameSpaceClass + "eBookingUserQuery";
            LoggerHelper.LogWriter(eBookingUserEntity.LogMessages);

            try
            {
                return eBookingUserSA.eBookingUserQuery(eBookingUserEntity);
            }
            catch (Exception ex)
            {
                eBookingUserEntity.LogMessages.MsgType = MessageType.ERROR;
                eBookingUserEntity.LogMessages.Content = _nameSpaceClass + "eBookingUserQuery  Error: " + ex.Message;
                LoggerHelper.LogWriter(eBookingUserEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet eBookingUserQuery(string userName)
        {
            return eBookingUserSA.eBookingUserQuery(userName);
        }
    }
}
