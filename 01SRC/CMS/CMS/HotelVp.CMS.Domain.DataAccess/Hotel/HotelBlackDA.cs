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
using HotelVp.CMS.Domain.Entity.Hotel;

namespace HotelVp.CMS.Domain.DataAccess.Hotel
{
    public abstract class HotelBlackDA
    {
        public static HotelBlackEntity GetHotelBlackListByCount(HotelBlackEntity hotelblackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("ISBLACK",OracleType.VarChar)
                                };
            HotelBlackDBEntity dbParm = (hotelblackEntity.HotelBlackDBEntity.Count > 0) ? hotelblackEntity.HotelBlackDBEntity[0] : new HotelBlackDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelId))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelId; ;
            }
            if (String.IsNullOrEmpty(dbParm.Source))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.Source; ;
            }
            if (String.IsNullOrEmpty(dbParm.IsBlack))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.IsBlack; ;
            }
            hotelblackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelBlackList", "t_lm_hotel_blacklist_search_bycount", true, parm);

            return hotelblackEntity;
        }


        public static HotelBlackEntity GetHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("ISBLACK",OracleType.VarChar)
                                };
            HotelBlackDBEntity dbParm = (hotelblackEntity.HotelBlackDBEntity.Count > 0) ? hotelblackEntity.HotelBlackDBEntity[0] : new HotelBlackDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelId))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelId; ;
            }
            if (String.IsNullOrEmpty(dbParm.Source))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.Source; ;
            }
            if (String.IsNullOrEmpty(dbParm.IsBlack))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.IsBlack; ;
            }
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("HotelBlackList", "t_lm_hotel_blacklist_search");
            //hotelblackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelBlackList", "t_lm_hotel_blacklist_search", true, parm);
            hotelblackEntity.QueryResult = DbManager.Query(SqlString, parm, (hotelblackEntity.PageCurrent - 1) * hotelblackEntity.PageSize, hotelblackEntity.PageSize, true);

            return hotelblackEntity;
        }

        public static int InsertHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            OracleParameter[] parm ={
                                        new OracleParameter("ID",OracleType.Number),
                                        new OracleParameter("HOTELID",OracleType.VarChar),
                                        new OracleParameter("ISBLACK",OracleType.VarChar),
                                        new OracleParameter("SOURCE",OracleType.VarChar),
                                        new OracleParameter("CREATEUSER",OracleType.VarChar)
                                };
            HotelBlackDBEntity dbParm = (hotelblackEntity.HotelBlackDBEntity.Count > 0) ? hotelblackEntity.HotelBlackDBEntity[0] : new HotelBlackDBEntity();

            parm[0].Value = getMaxIDfromSeq("t_lm_hotel_blacklist_SEQ");

            //:ID,:HOTELID,:ISBLACK,:SOURCE,sysdate,:CREATEUSER
            if (String.IsNullOrEmpty(dbParm.HotelId))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HotelId;
            }
            if (String.IsNullOrEmpty(dbParm.IsBlack))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.IsBlack;
            }
            if (String.IsNullOrEmpty(dbParm.Source))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Source;
            }
            if (String.IsNullOrEmpty(dbParm.CreateUser))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.CreateUser;
            }

            DbManager.ExecuteSql("HotelBlackList", "t_lm_hotel_blacklist_insert", parm);
            return 1;
        }

        public static int DeleteHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            OracleParameter[] parm ={
                                        new OracleParameter("ID",OracleType.Number)
                                };
            HotelBlackDBEntity dbParm = (hotelblackEntity.HotelBlackDBEntity.Count > 0) ? hotelblackEntity.HotelBlackDBEntity[0] : new HotelBlackDBEntity();

            if (String.IsNullOrEmpty(dbParm.ID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.ID;
            }
            DbManager.ExecuteSql("HotelBlackList", "t_lm_hotel_blacklist_delete", parm);
            return 1;
        }


        public static int UpdateHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            OracleParameter[] parm ={
                                        new OracleParameter("ID",OracleType.Number),
                                        new OracleParameter("ISBLACK",OracleType.VarChar)
                                };
            HotelBlackDBEntity dbParm = (hotelblackEntity.HotelBlackDBEntity.Count > 0) ? hotelblackEntity.HotelBlackDBEntity[0] : new HotelBlackDBEntity();

            if (String.IsNullOrEmpty(dbParm.ID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.ID;
            }

            if (String.IsNullOrEmpty(dbParm.IsBlack))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.IsBlack;
            }

            DbManager.ExecuteSql("HotelBlackList", "t_lm_hotel_blacklist_update", parm);
            return 1;
        }

        public static int UpdateHotelBlackListByExist(HotelBlackEntity hotelblackEntity)
        {
            OracleParameter[] parm ={
                                        new OracleParameter("ISBLACK",OracleType.VarChar),
                                        new OracleParameter("HOTELID",OracleType.VarChar),
                                        new OracleParameter("SOURCE",OracleType.VarChar),
                                        new OracleParameter("UPDATEUSER",OracleType.VarChar)                                        
                                };
            HotelBlackDBEntity dbParm = (hotelblackEntity.HotelBlackDBEntity.Count > 0) ? hotelblackEntity.HotelBlackDBEntity[0] : new HotelBlackDBEntity();

            if (String.IsNullOrEmpty(dbParm.IsBlack))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.IsBlack;
            }

            if (String.IsNullOrEmpty(dbParm.HotelId))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HotelId;
            }

            if (String.IsNullOrEmpty(dbParm.Source))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.Source;
            }

            if (String.IsNullOrEmpty(dbParm.UpdateUser))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.UpdateUser;
            }

            DbManager.ExecuteSql("HotelBlackList", "t_lm_hotel_blacklist_update_exist", parm);
            return 1;
        }



        public static HotelBlackEntity RepeatHotelBlackList(HotelBlackEntity hotelblackEntity)
        {
            OracleParameter[] parm ={
                                        new OracleParameter("HOTELID",OracleType.VarChar),
                                        new OracleParameter("SOURCE",OracleType.VarChar)
                                };
            HotelBlackDBEntity dbParm = (hotelblackEntity.HotelBlackDBEntity.Count > 0) ? hotelblackEntity.HotelBlackDBEntity[0] : new HotelBlackDBEntity();

            parm[0].Value = dbParm.HotelId;
            parm[1].Value = dbParm.Source;

            hotelblackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelBlackList", "t_lm_hotel_blacklist_repeat", true, parm);

            return hotelblackEntity;
        }



        //通过sequence查询得到下一个ID值,数据库为Oracle
        public static int getMaxIDfromSeq(string sequencename)
        {
            int seqID = 1;
            string sql = "select " + sequencename + ".nextval from dual";
            object obj = DbHelperOra.GetSingle(sql, false);
            if (obj != null)
            {
                seqID = Convert.ToInt32(obj);
            }
            return seqID;
        }
    }
}
