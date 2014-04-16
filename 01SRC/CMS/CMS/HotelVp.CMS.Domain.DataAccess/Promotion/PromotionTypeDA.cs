using System;
using System.Collections;
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
using HotelVp.CMS.Domain.Entity.Promotion;

namespace HotelVp.CMS.Domain.DataAccess.Promotion
{
    public abstract class PromotionTypeDA
    {
        public static PromotionTypeEntity CommonSelect(PromotionTypeEntity promotionTypeEntity)
        {
            PromotionTypeDBEntity dbParm = (promotionTypeEntity.PromotiontypeDBEntity.Count > 0) ? promotionTypeEntity.PromotiontypeDBEntity[0] : new PromotionTypeDBEntity();

            OracleParameter[] lmParm ={
                                    new OracleParameter("NAME",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.Name))
            {
                lmParm[0].Value = DBNull.Value;
            }
            else
            {
                lmParm[0].Value = dbParm.Name;
            }
            promotionTypeEntity.QueryResult = DbManager.Query("Promotion", "t_promotion_method", false, lmParm);
            return promotionTypeEntity;
        }

        public static int Insert(PromotionTypeEntity promotionTypeEntity)
        {
            PromotionTypeDBEntity dbParm = (promotionTypeEntity.PromotiontypeDBEntity.Count > 0) ? promotionTypeEntity.PromotiontypeDBEntity[0] : new PromotionTypeDBEntity();

            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("NAME",OracleType.VarChar),
                                    new OracleParameter("SEQ",OracleType.Number)
                                };

            lmParm[0].Value = getMaxIDfromSeq("t_lm_promotion_method_seq");

            if (string.IsNullOrEmpty(dbParm.Name))
            {
                lmParm[1].Value = DBNull.Value;
            }
            else
            {
                lmParm[1].Value = dbParm.Name;
            }
            if (string.IsNullOrEmpty(dbParm.Seq))
            {
                lmParm[2].Value = DBNull.Value;
            }
            else
            {
                lmParm[2].Value = dbParm.Seq;
            }
            return DbManager.ExecuteSql("Promotion", "t_promotion_method_insert", lmParm);
        }

        public static int Update(PromotionTypeEntity promotionTypeEntity)
        {
            PromotionTypeDBEntity dbParm = (promotionTypeEntity.PromotiontypeDBEntity.Count > 0) ? promotionTypeEntity.PromotiontypeDBEntity[0] : new PromotionTypeDBEntity();

            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("NAME",OracleType.VarChar),
                                    new OracleParameter("SEQ",OracleType.Number)
                                };

            lmParm[0].Value = dbParm.ID;
            if (string.IsNullOrEmpty(dbParm.Name))
            {
                lmParm[1].Value = DBNull.Value;
            }
            else
            {
                lmParm[1].Value = dbParm.Name;
            }
            if (string.IsNullOrEmpty(dbParm.Seq))
            {
                lmParm[2].Value = DBNull.Value;
            }
            else
            {
                lmParm[2].Value = dbParm.Seq;
            }
            return DbManager.ExecuteSql("Promotion", "t_promotion_method_update", lmParm);
        }

        public static int Delete(PromotionTypeEntity promotionTypeEntity)
        {
            PromotionTypeDBEntity dbParm = (promotionTypeEntity.PromotiontypeDBEntity.Count > 0) ? promotionTypeEntity.PromotiontypeDBEntity[0] : new PromotionTypeDBEntity();

            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number)
                                };

            lmParm[0].Value = dbParm.ID;
            return DbManager.ExecuteSql("Promotion", "t_promotion_method_delete", lmParm);
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

        //获取最大的Seq
        public static int GetMaxSeq()
        {
            DataSet ds = DbManager.Query("Promotion", "get_t_promotion_method_maxSeq", false);
            if (ds.Tables.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 1;
            }
        }
    }
}
