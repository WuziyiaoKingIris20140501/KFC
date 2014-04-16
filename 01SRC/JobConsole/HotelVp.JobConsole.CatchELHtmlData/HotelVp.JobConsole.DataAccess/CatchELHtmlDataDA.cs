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
    public abstract class CatchELHtmlDataDA
    {
        public static DataSet GetSalesManagerList(string HotelID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerList");
            cmd.SetParameterValue("@HotelID", HotelID);

            return cmd.ExecuteDataSet(); ;
        }

        public static DataSet GetHotelName(string HotelID)
        {
            DataSet dsResult = new DataSet();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HotelID",OracleType.VarChar)
                                };

            lmParm[0].Value = HotelID;
            dsResult = HotelVp.Common.DBUtility.DbManager.Query("CatchELHtmlData", "t_lm_b_hotel_name", true, lmParm);

            return dsResult;
        }

        public static DataSet GetCityName(string HotelID)
        {
            DataSet dsResult = new DataSet();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HotelID",OracleType.VarChar)
                                };

            lmParm[0].Value = HotelID;
            dsResult = HotelVp.Common.DBUtility.DbManager.Query("CatchELHtmlData", "t_lm_b_city_name", true, lmParm);

            return dsResult;
        }
    }
}
