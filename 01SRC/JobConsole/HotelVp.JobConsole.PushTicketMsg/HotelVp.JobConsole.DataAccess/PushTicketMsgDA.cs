using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;


namespace HotelVp.JobConsole.DataAccess
{
    public abstract class PushTicketMsgDA
    {
        public static DataSet GetPushTicketMsgList(int i)
        {
            if (i == 0)
            {
                return HotelVp.Common.DBUtility.DbManager.Query("PushTicketMsg", "t_lm_ticket_user_msg_list_0", false);
            }
            else
            {
                return HotelVp.Common.DBUtility.DbManager.Query("PushTicketMsg", "t_lm_ticket_user_msg_list_1", false);
            }
        }
    }
}