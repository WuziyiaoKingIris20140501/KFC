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
    public abstract class DbSearchDA
    {
         public static DbSearchEntity ItemSelect(DbSearchEntity dbSearchEntity)
        {
           
            DbSearchDBEntity dbParm = (dbSearchEntity.DbSearchDBEntity.Count > 0) ? dbSearchEntity.DbSearchDBEntity[0] : new DbSearchDBEntity();

            if (String.IsNullOrEmpty(dbParm.SqlContent))
            {
                return dbSearchEntity;
            }

            dbSearchEntity.QueryResult = HotelVp.Common.DBUtility.DbHelperOra.Query(dbParm.SqlContent,true);
            return dbSearchEntity;
        }

         public static DbSearchEntity ItemSqlSelect(DbSearchEntity dbSearchEntity)
        {
           
            DbSearchDBEntity dbParm = (dbSearchEntity.DbSearchDBEntity.Count > 0) ? dbSearchEntity.DbSearchDBEntity[0] : new DbSearchDBEntity();

            if (String.IsNullOrEmpty(dbParm.SqlContent))
            {
                return dbSearchEntity;
            }

            DataCommand cmd = DataCommandManager.CreateCustomDataCommand("CMS", CommandType.Text, dbParm.SqlContent);
            dbSearchEntity.QueryResult = cmd.ExecuteDataSet();
            return dbSearchEntity;
        }

         public static DbSearchEntity MenuSelect(DbSearchEntity dbSearchEntity)
        {
            dbSearchEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("DbSearch", "t_lm_b_dbsearch_table", true);
            return dbSearchEntity;
        }

         public static DbSearchEntity MenuSelectFromCMS(DbSearchEntity dbSearchEntity)
         {
             DataCommand cmd = DataCommandManager.GetDataCommand("SelectLmdbStructLog");
             dbSearchEntity.QueryResult = cmd.ExecuteDataSet();
             return dbSearchEntity;
         }

         public static DbSearchEntity SqlMenuSelectFromCMS(DbSearchEntity dbSearchEntity)
         {
             DataCommand cmd = DataCommandManager.GetDataCommand("SelectSqlMenuSelectFromCMS");
             dbSearchEntity.QueryResult = cmd.ExecuteDataSet();
             return dbSearchEntity;
         }

         public static DbSearchEntity TableColumsSelect(DbSearchEntity dbSearchEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TABLENM",OracleType.VarChar)
                                };
            DbSearchDBEntity dbParm = (dbSearchEntity.DbSearchDBEntity.Count > 0) ? dbSearchEntity.DbSearchDBEntity[0] : new DbSearchDBEntity();

            if (String.IsNullOrEmpty(dbParm.TableID))
            {
                return dbSearchEntity;
            }
            else
            {
                parm[0].Value = dbParm.TableID;
            }
          
            //string strSQL = HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("DbSearch", "t_lm_b_dbsearch_tablecolums");
            //strSQL = String.Format(strSQL, dbParm.TableID);
            //dbSearchEntity.QueryResult = HotelVp.Common.DBUtility.DbHelperOra.Query(strSQL);
            dbSearchEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("DbSearch", "t_lm_b_dbsearch_tablecolums",true, parm);
            return dbSearchEntity;
        }

         public static DbSearchEntity TableSqlColumsSelect(DbSearchEntity dbSearchEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TABLENM",OracleType.VarChar)
                                };
            DbSearchDBEntity dbParm = (dbSearchEntity.DbSearchDBEntity.Count > 0) ? dbSearchEntity.DbSearchDBEntity[0] : new DbSearchDBEntity();

            if (String.IsNullOrEmpty(dbParm.TableID))
            {
                return dbSearchEntity;
            }
            else
            {
                parm[0].Value = dbParm.TableID;
            }
          
            //string strSQL = HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("DbSearch", "t_lm_b_dbsearch_tablecolums");
            //strSQL = String.Format(strSQL, dbParm.TableID);
            //dbSearchEntity.QueryResult = HotelVp.Common.DBUtility.DbHelperOra.Query(strSQL);
            dbSearchEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("DbSearch", "t_lm_b_dbsearch_tablecolums",true, parm);
            return dbSearchEntity;
        }
    }
}