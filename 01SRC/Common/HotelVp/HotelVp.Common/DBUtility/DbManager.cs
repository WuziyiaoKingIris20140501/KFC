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
        /// ִ��SQL
        /// </summary>
        /// <param name="SqlName">XML�ļ���</param>
        /// <param name="SqlId">ִ��SQL�ű�ID</param>
        /// <returns></returns>
        public static int ExecuteSql(string SqlName, string SqlId)
        {
            return DbHelperOra.ExecuteSql(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId));
        }

        /// <summary>
        /// ִ�д��в���SQL
        /// </summary>
        /// <param name="SqlName">XML�ļ���</param>
        /// <param name="SqlId">ִ��SQL�ű�ID</param>
        /// <param name="cmdParms">��������</param>
        /// <returns></returns>
        public static int ExecuteSql(string SqlName, string SqlId, object[] cmdParms)
        {
            return DbHelperOra.ExecuteSql(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), (OracleParameter[])cmdParms);
        }

        /// <summary>
        /// ִ�д��в���SQL �ж��Ƿ����
        /// </summary>
        /// <param name="SqlName">XML�ļ���</param>
        /// <param name="SqlId">ִ��SQL�ű�ID</param>
        /// <param name="cmdParms">��������</param>
        /// <returns></returns>
        public static bool Exists(string SqlName, string SqlId, bool SqlType, object[] cmdParms)
        {
            return DbHelperOra.Exists(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), SqlType, (OracleParameter[])cmdParms);
        }

        /// <summary>
        /// ִ��SQL��ѯ���
        /// </summary>
        ///<param name="SqlName">XML�ļ���</param>
        /// <param name="SqlId">ִ��SQL�ű�ID</param>
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
        /// ִ�д��в���SQL��ѯ���
        /// </summary>
        ///<param name="SqlName">XML�ļ���</param>
        /// <param name="SqlId">ִ��SQL�ű�ID</param>
        /// <param name="cmdParms">��������</param>
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
        /// ִ�ж�SQL�Ľű�
        /// </summary>
        /// <param name="cmdList">��ű�����</param>
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
        /// ִ�д��в���Procedure
        /// </summary>
        /// <param name="SqlName">XML�ļ���</param>
        /// <param name="SqlId">ִ��SQL�ű�ID</param>
        /// <param name="cmdParms">��������</param>
        /// <param name="tableName">����</param>
        /// <returns></returns>
        public static DataSet RunProcedure(string SqlName, string SqlId, object[] cmdParms, string tableName)
        {
            return DbHelperOra.RunProcedure(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), (OracleParameter[])cmdParms, tableName);
        }

        /// <summary>
        /// ִ�д��в���Procedure
        /// </summary>
        /// <param name="SqlName">XML�ļ���</param>
        /// <param name="SqlId">ִ��SQL�ű�ID</param>
        /// <param name="cmdParms">��������</param>
        /// <returns></returns>
        public static OracleDataReader RunProcedure(string SqlName, string SqlId, object[] cmdParms)
        {
            return DbHelperOra.RunProcedure(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), (OracleParameter[])cmdParms);
        }

        /// <summary>
        /// ִ�д��в���Procedure
        /// </summary>
        /// <param name="SqlName">XML�ļ���</param>
        /// <param name="SqlId">ִ��SQL�ű�ID</param>
        /// <param name="cmdParms">��������</param>
        /// <param name="rowsAffected">����Ӱ������</param>
        /// <returns></returns>
        public static int RunProcedure(string SqlName, string SqlId, object[] cmdParms, out int rowsAffected)
        {
            return DbHelperOra.RunProcedure(XmlSqlAnalyze.GotSqlTextFromXml(SqlName, SqlId), (OracleParameter[])cmdParms, out rowsAffected);
        }
    }
}