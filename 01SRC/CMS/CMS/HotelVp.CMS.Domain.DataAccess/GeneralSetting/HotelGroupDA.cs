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
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class HotelGroupDA
    {
         public static HotelGroupEntity Select(HotelGroupEntity hotelGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number)
                                };
            HotelGroupDBEntity dbParm = (hotelGroupEntity.HotelGroupDBEntity.Count > 0) ? hotelGroupEntity.HotelGroupDBEntity[0] : new HotelGroupDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelGroupID;
            }

            hotelGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelGroup", "t_lm_b_hotelgroup", false, parm);
            return hotelGroupEntity;
        }

        public static int CheckInsert(HotelGroupEntity hotelGroupEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("GROUPCODE",OracleType.VarChar),
                                    new OracleParameter("GROUPNM",OracleType.VarChar)
                                };
            HotelGroupDBEntity dbParm = (hotelGroupEntity.HotelGroupDBEntity.Count > 0) ? hotelGroupEntity.HotelGroupDBEntity[0] : new HotelGroupDBEntity();
            parm[0].Value = dbParm.HotelGroupCode;
            parm[1].Value = dbParm.Name_CN;
            hotelGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelGroup", "t_lm_b_hotelgroup_sigle", false, parm);

            if (hotelGroupEntity.QueryResult.Tables.Count > 0 && hotelGroupEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckUpdate(HotelGroupEntity hotelGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("GROUPNM",OracleType.VarChar)                                 
                                };

            HotelGroupDBEntity dbParm = (hotelGroupEntity.HotelGroupDBEntity.Count > 0) ? hotelGroupEntity.HotelGroupDBEntity[0] : new HotelGroupDBEntity();

            parm[0].Value = int.Parse(dbParm.HotelGroupID.ToString());
            parm[1].Value = dbParm.Name_CN;

            hotelGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelGroup", "t_lm_b_hotelgroup_updatesigle", false, parm);

            if (hotelGroupEntity.QueryResult.Tables.Count > 0 && hotelGroupEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }
        public static int Insert(HotelGroupEntity hotelGroupEntity)
        {
            if (hotelGroupEntity.HotelGroupDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelGroupEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(hotelGroupEntity) > 0)
            {
                return 2;
            }

            HotelGroupDBEntity dbParm = (hotelGroupEntity.HotelGroupDBEntity.Count > 0) ? hotelGroupEntity.HotelGroupDBEntity[0] : new HotelGroupDBEntity();

            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),                       
                                    new OracleParameter("GROUPCODE",OracleType.VarChar),                       
                                    new OracleParameter("NAMEZH",OracleType.VarChar),                       
                                    new OracleParameter("DESCRIPTIONZH",OracleType.VarChar),                       
                                    new OracleParameter("BANDTYPE",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                };
            parm[0].Value = getMaxIDfromSeq("t_lm_b_hotelgroup_seq");
            parm[1].Value = dbParm.HotelGroupCode;
            parm[2].Value = dbParm.Name_CN;
            parm[3].Value = dbParm.Description;
            parm[4].Value = dbParm.BandType;
            parm[5].Value = dbParm.OnlineStatus;
            DbManager.ExecuteSql("HotelGroup", "t_lm_b_hotelgroup_insert", parm);
            return 1;
        }

        public static int Update(HotelGroupEntity hotelGroupEntity)
        {
            if (hotelGroupEntity.HotelGroupDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelGroupEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckUpdate(hotelGroupEntity) > 0)
            {
                return 2;
            }

            HotelGroupDBEntity dbParm = (hotelGroupEntity.HotelGroupDBEntity.Count > 0) ? hotelGroupEntity.HotelGroupDBEntity[0] : new HotelGroupDBEntity();

            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),                       
                                    new OracleParameter("GROUPCODE",OracleType.VarChar),                       
                                    new OracleParameter("NAMEZH",OracleType.VarChar),                       
                                    new OracleParameter("DESCRIPTIONZH",OracleType.VarChar),                       
                                    new OracleParameter("BANDTYPE",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                };

            parm[0].Value = dbParm.HotelGroupID;
            parm[1].Value = dbParm.HotelGroupCode;
            parm[2].Value = dbParm.Name_CN;
            parm[3].Value = dbParm.Description;
            parm[4].Value = dbParm.BandType;
            parm[5].Value = dbParm.OnlineStatus;

            DbManager.ExecuteSql("HotelGroup", "t_lm_b_hotelgroup_update", parm);
            //if (!String.IsNullOrEmpty(dbParm.Type) && "1".Equals(dbParm.Type))
            //{
            //    OracleParameter[] parm ={
            //                        new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("GROUPCODE",OracleType.VarChar),                       
            //                        new OracleParameter("NAMEZH",OracleType.VarChar),                       
            //                        new OracleParameter("DESCRIPTIONZH",OracleType.VarChar),                       
            //                        new OracleParameter("BANDTYPE",OracleType.VarChar)
            //                    };

            //    parm[0].Value = dbParm.HotelGroupID;
            //    parm[1].Value = dbParm.HotelGroupCode;
            //    parm[2].Value = dbParm.Name_CN;
            //    parm[3].Value = dbParm.Description;
            //    parm[4].Value = dbParm.BandType;

            //    DbManager.ExecuteSql("HotelGroup", "t_lm_b_hotelgroup_update", parm);
            //}
            //else
            //{
            //    OracleParameter[] parm ={
            //                            new OracleParameter("ID",OracleType.Number)                                 
            //                        };

            //    parm[0].Value = dbParm.HotelGroupID;
            //    DbManager.ExecuteSql("HotelGroup", "t_lm_b_hotelgroup_delete", parm);
            //}

            return 1;
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