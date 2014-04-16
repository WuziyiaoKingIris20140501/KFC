using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;


namespace HotelVp.JobConsole.DataAccess
{
    public abstract class GGBDTudeDA
    {
        public static DataSet GetHotelList()
        {
            return HotelVp.Common.DBUtility.DbManager.Query("GGBDTude", "t_lm_b_hotel_list", true);
        }

        public static void SaveHotelTude(string HotelID, string[] Los)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar)
                                };
            parm[0].Value = HotelID;
            parm[1].Value = Los[0];
            parm[2].Value = Los[1];
            HotelVp.Common.DBUtility.DbManager.ExecuteSql("GGBDTude", "t_lm_b_hotel_save", parm);
        }

        public static int SaveHotelTudeCommonList(List<CommandInfo> cmdList)
        {
            DbHelperOra.ExecuteSqlTran(cmdList);
            return 1;
        }
    }
}
