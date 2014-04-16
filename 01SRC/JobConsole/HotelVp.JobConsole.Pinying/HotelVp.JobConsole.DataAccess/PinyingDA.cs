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
    public abstract class PinyingDA
    {
        public static DataSet GetHotelPinyingList()
        {
            DataSet dsResult = new DataSet();
            dsResult = HotelVp.Common.DBUtility.DbManager.Query("Pinying", "t_lm_b_pinying_hotel", false);
            return dsResult;
        }

        public static int SaveDestinationList(string strSQL, DataRow drRow)
        {
            OracleParameter[] lmParm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("DESTINATIONID",OracleType.VarChar),
                                    new OracleParameter("TYPEID",OracleType.VarChar),
                                    new OracleParameter("DISTANCE",OracleType.VarChar)
                                };

            lmParm[0].Value = drRow["CITYID"].ToString();
            lmParm[1].Value = drRow["HOTELID"].ToString();
            lmParm[2].Value = drRow["DESTINATIONID"].ToString();
            lmParm[3].Value = drRow["TYPEID"].ToString();
            lmParm[4].Value = drRow["DISTANCE"].ToString();

            DbHelperOra.ExecuteSql(strSQL, lmParm);

            return 1;
        }

        public static int SavePinyingCommonList(List<CommandInfo> cmdList)
        {
            DbHelperOra.ExecuteSqlTran(cmdList);
            return 1;
        }
    }
}
