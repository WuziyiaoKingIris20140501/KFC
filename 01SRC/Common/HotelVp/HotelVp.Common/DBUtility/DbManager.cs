using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;

namespace HotelVp.Common.DBUtility
{  
    public abstract class DbManager
    {
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="SqlName">XML文件名</param>
        /// <param name="SqlId">执行SQL脚本ID</param>
        /// <returns></returns>
        public static int ExecuteSql(string SqlName, string SqlId)
        {
            return DbHelperOra.ExecuteSql(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId));
        }

        /// <summary>
        /// 执行带有参数SQL
        /// </summary>
        /// <param name="SqlName">XML文件名</param>
        /// <param name="SqlId">执行SQL脚本ID</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns></returns>
        public static int ExecuteSql(string SqlName, string SqlId, object[] cmdParms)
        {
            return DbHelperOra.ExecuteSql(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), (OracleParameter[])cmdParms);
        }

        /// <summary>
        /// 执行带有参数SQL 判读是否存在
        /// </summary>
        /// <param name="SqlName">XML文件名</param>
        /// <param name="SqlId">执行SQL脚本ID</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns></returns>
        public static bool Exists(string SqlName, string SqlId, bool SqlType, object[] cmdParms)
        {
            return DbHelperOra.Exists(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), SqlType, (OracleParameter[])cmdParms);
        }

        /// <summary>
        /// 执行SQL查询语句
        /// </summary>
        ///<param name="SqlName">XML文件名</param>
        /// <param name="SqlId">执行SQL脚本ID</param>
        /// <returns></returns>
        public static DataSet Query(string SqlName, string SqlId, bool SqlType)
        {
            return DbHelperOra.Query(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), SqlType);
        }

        public static DataSet Query(string SqlName, string SqlId, int StartRecord, int MaxRecord, bool SqlType)
        {
            return DbHelperOra.Query(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), StartRecord, MaxRecord, SqlType);
        }

        public static DataSet Query(string SqlString, int StartRecord, int MaxRecord, bool SqlType)
        {
            return DbHelperOra.Query(SqlString, StartRecord, MaxRecord, SqlType);
        }

        /// <summary>
        /// 执行带有参数SQL查询语句
        /// </summary>
        ///<param name="SqlName">XML文件名</param>
        /// <param name="SqlId">执行SQL脚本ID</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns></returns>
        public static DataSet Query(string SqlName, string SqlId, bool SqlType, object[] cmdParms)
        {
            return DbHelperOra.Query(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), SqlType, (OracleParameter[])cmdParms);
        }

        public static DataSet Query(string SqlName, string SqlId, object[] cmdParms, int StartRecord, int MaxRecord, bool SqlType)
        {
            return DbHelperOra.Query(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), StartRecord, MaxRecord, SqlType, (OracleParameter[])cmdParms);
        }

        public static DataSet Query(string SqlString, object[] cmdParms, int StartRecord, int MaxRecord, bool SqlType)
        {
            return DbHelperOra.Query(SqlString, StartRecord, MaxRecord, SqlType, (OracleParameter[])cmdParms);
        }

               /// <summary>
        /// 执行多SQL文脚本
        /// </summary>
        /// <param name="cmdList">多脚本参数</param>
        /// <returns></returns>
        public static int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            foreach (CommandInfo myDE in cmdList)
            {
                myDE.CommandText = XmlSqlAnalyze.GotSqlTextFromXml(myDE.SqlName, myDE.SqlId);
            }
            return DbHelperOra.ExecuteSqlTran(cmdList);
        }

        /// <summary>
        /// 执行带有参数Procedure
        /// </summary>
        /// <param name="SqlName">XML文件名</param>
        /// <param name="SqlId">执行SQL脚本ID</param>
        /// <param name="cmdParms">参数数组</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static DataSet RunProcedure(string SqlName, string SqlId, object[] cmdParms, string tableName)
        {
            return DbHelperOra.RunProcedure(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), (OracleParameter[])cmdParms, tableName);
        }

        /// <summary>
        /// 执行带有参数Procedure
        /// </summary>
        /// <param name="SqlName">XML文件名</param>
        /// <param name="SqlId">执行SQL脚本ID</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns></returns>
        public static OracleDataReader RunProcedure(string SqlName, string SqlId, object[] cmdParms)
        {
            return DbHelperOra.RunProcedure(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), (OracleParameter[])cmdParms);
        }

        /// <summary>
        /// 执行带有参数Procedure
        /// </summary>
        /// <param name="SqlName">XML文件名</param>
        /// <param name="SqlId">执行SQL脚本ID</param>
        /// <param name="cmdParms">参数数组</param>
        /// <param name="rowsAffected">返回影响行数</param>
        /// <returns></returns>
        public static int RunProcedure(string SqlName, string SqlId, object[] cmdParms, out int rowsAffected)
        {
            return DbHelperOra.RunProcedure(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), (OracleParameter[])cmdParms, out rowsAffected);
        }
    }
}