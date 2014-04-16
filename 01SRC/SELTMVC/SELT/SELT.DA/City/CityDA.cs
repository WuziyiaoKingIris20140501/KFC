using System;
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
using HotelVp.SELT.Domain.Entity;

namespace HotelVp.SELT.Domain.DA
{
    public abstract class CityDA
    {
        public static CityEntity Select(CityEntity cityEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYNAME",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();

            if (String.IsNullOrEmpty(dbParm.Name_CN))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.Name_CN;
            }


            if (String.IsNullOrEmpty(dbParm.OnlineStatus))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.OnlineStatus;
            }

            if (String.IsNullOrEmpty(dbParm.StartDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.StartDTime;
            }

            if (String.IsNullOrEmpty(dbParm.EndDTime))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.EndDTime;
            }

            cityEntity.QueryResult = DbManager.Query("City", "t_lm_b_city", true, parm);
            return cityEntity;
        }

        public static CityEntity CommonSelect(CityEntity cityEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PARAMETERS",OracleType.VarChar)
                                };
            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();

            if (String.IsNullOrEmpty(dbParm.Name_CN))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.Name_CN;
            }

            cityEntity.QueryResult = DbManager.Query("WebAutoComplete", "t_b_auto_city", true, parm);
            return cityEntity;
        }

        public static DataSet GetFaxUnknow()
        {
            return DbManager.Query("City", "t_b_fax_unknow", true);
        }
    }
}
