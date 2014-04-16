using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
using System.Collections;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class HotelPlanInfoDA
    {
        public static HotelInfoEntity GetHotelList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TRADEAREAID",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar),
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            if (string.IsNullOrEmpty(dbParm.HotelID))
                parm[0].Value = DBNull.Value;
            else
                parm[0].Value = dbParm.HotelID;

            if (string.IsNullOrEmpty(dbParm.City))
                parm[1].Value = DBNull.Value;
            else
                parm[1].Value = dbParm.City;

            if (string.IsNullOrEmpty(dbParm.AreaID))
                parm[2].Value = DBNull.Value;
            else
                parm[2].Value = dbParm.AreaID;

            if (string.IsNullOrEmpty(dbParm.SalesID))
                parm[3].Value = DBNull.Value;
            else
                parm[3].Value = dbParm.SalesID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelPlanInfo", "t_lm_b_hotelplan_search_list", false, parm);
            return hotelInfoEntity;
        }
    }
}
