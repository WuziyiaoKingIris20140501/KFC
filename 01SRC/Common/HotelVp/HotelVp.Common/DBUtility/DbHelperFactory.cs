using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
 
namespace  HotelVp.Common.DBUtility
{
    //�ⲿ���ã�ֱ��ʹ������ķ��������ڷ��ز�ͬ��ֵ
    public class DbHelperFactory
    {
        public static DataSet Query(string SQLString)
        {
            DataSet ds = new DataSet();
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    ds = DbHelperSQL.Query(SQLString);
                    break;
                case "2":
                    DataTable dtblInfo = null;
                    SQLString = SQLString.Replace("getdate()", "SYSDATE");
                    string[] sarr = SQLString.Split(';');
                    if (sarr.Length > 1)
                    {
                        for (int i = 0; i < sarr.Length; i++)
                        {
                            if (sarr[i].Length > 0)
                            {
                              ds.Tables.Add(  DbHelperOra.Query(sarr[i]).Tables[0].Copy());
                            }
                        }
                    }
                    else
                    {
                        ds = DbHelperOra.Query(SQLString);
                    }
                  
                    break;
                default:
                    ds = DbHelperSQL.Query(SQLString);
                    break;
            }
            return ds;
        }
        public static int ExecuteSql(string SQLString)
        {
            int itemp=0;
            
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    itemp = DbHelperSQL.ExecuteSql(SQLString);
                    break; 
                case "2":
                    SQLString = SQLString.Replace("getdate()", "SYSDATE");
                    string []sarr  =SQLString.Split(';');
                    if (sarr.Length > 1)
                    {
                        for (int i = 0; i < sarr.Length; i++)
                        {
                            if (sarr[i].Length > 0)
                            {
                                DbHelperOra.ExecuteSql(sarr[i]);
                            }
                        }
                    }
                    else
                    {
                        itemp = DbHelperOra.ExecuteSql(SQLString);
                    }
                    break;
                default:
                    itemp = DbHelperSQL.ExecuteSql(SQLString);
                    break;
            }
            return itemp;
        }

        public static object GetSingle(string SQLString)
        {
            object itemp;
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    itemp = DbHelperSQL.GetSingle(SQLString);
                    break;
                case "2":
                    itemp = DbHelperOra.GetSingle(SQLString);
                    break;
                default:
                    itemp = DbHelperSQL.GetSingle(SQLString);
                    break;
            }
            return itemp;
        }
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            int itemp;
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    itemp = DbHelperSQL.ExecuteSqlTran(SQLStringList);
                    break;
                case "2":
                    itemp = DbHelperOra.ExecuteSqlTran(SQLStringList);
                    break;
                default:
                    itemp = DbHelperSQL.ExecuteSqlTran(SQLStringList);
                    break;
            }
            return itemp;
        }
        public static int GetMaxID(string FieldName, string TableName)
        {
            int itemp;
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    itemp = DbHelperSQL.GetMaxID(FieldName, TableName);
                    break;
                case "2":
                    itemp = DbHelperOra.GetMaxID(FieldName, TableName);
                    break;
                default:
                    itemp = DbHelperSQL.GetMaxID(FieldName, TableName);
                    break;
            }
            return itemp;
        }
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            DataSet itemp = new DataSet();
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    itemp = DbHelperSQL.RunProcedure(storedProcName, parameters, tableName);
                    break;
                case "2":
                    itemp = DbHelperOra.RunProcedure(storedProcName, parameters, tableName);
                    break;
                default:
                    itemp = DbHelperSQL.RunProcedure(storedProcName, parameters, tableName);
                    break;
            }
            return itemp;
        }



        #region cs add 2009-09-09

        public static void ExecuteSqlTran(IDbTransaction transaction, IDbConnection connection, string strsql)
        {

            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    DbHelperSQL.ExecuteSqlTran((SqlTransaction)transaction, (SqlConnection)connection, strsql);
                    break;
                case "2":
                    strsql = strsql.Replace("getdate()", "SYSDATE");
                    string[] sarr = strsql.Split(';');
                    if (sarr.Length > 1)
                    {
                        for (int i = 0; i < sarr.Length; i++)
                        {
                            if (sarr[i].Length > 0)
                            {

                                DbHelperOra.ExecuteSqlTran((OracleTransaction)transaction, (OracleConnection)connection, sarr[i]);
                            }
                        }
                    }
                    else
                    {
                        DbHelperOra.ExecuteSqlTran((OracleTransaction)transaction, (OracleConnection)connection, strsql);
                    }


                    
                    break;
                default:
                    DbHelperSQL.ExecuteSqlTran((SqlTransaction)transaction, (SqlConnection)connection, strsql);
                    break;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="transaction">�����������</param>
        /// <param name="connection">�������Ӷ���</param>
        public static void BeginTran(out IDbTransaction transaction, out IDbConnection connection)
        {
            SqlTransaction sqlTransaction=null;
            SqlConnection sqlConnection=null;
            OracleTransaction oracleTransaction;
            OracleConnection oracleConnection;
            transaction = null;
            connection = null;

            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    sqlTransaction = (SqlTransaction)transaction;
                    sqlConnection = (SqlConnection)connection;

                    DbHelperSQL.BeginTran(out sqlTransaction, out sqlConnection);
                    transaction = sqlTransaction;
                    connection = sqlConnection;

                    break;
                case "2":
                    oracleTransaction = (OracleTransaction)transaction;
                    oracleConnection = (OracleConnection)connection;
                    DbHelperOra.BeginTran(out oracleTransaction, out oracleConnection);
                    transaction = oracleTransaction;
                    connection = oracleConnection;

                    break;
                default:
                    sqlTransaction = (SqlTransaction)transaction;
                    sqlConnection = (SqlConnection)connection;

                    DbHelperSQL.BeginTran(out sqlTransaction, out sqlConnection);
                    transaction = sqlTransaction;
                    connection = sqlConnection;
                    break;
            }

        }

        /// <summary>
        /// �ύ����
        /// </summary>
        /// <param name="transaction">�������</param>
        /// <param name="connection">���Ӷ���</param>
        public static void Commit( IDbTransaction transaction,  IDbConnection connection)
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    DbHelperSQL.Commit((SqlTransaction)transaction, (SqlConnection)connection);
                    break;
                case "2":
                    DbHelperOra.Commit((OracleTransaction)transaction, (OracleConnection)connection);
                    break;
                default:
                    DbHelperSQL.Commit((SqlTransaction)transaction, (SqlConnection)connection);
                    break;
            }
        }

        public static void Rollback(IDbTransaction transaction, IDbConnection connection)
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    DbHelperSQL.Rollback((SqlTransaction)transaction, (SqlConnection)connection);
                    break;
                case "2":
                    DbHelperOra.Rollback((OracleTransaction)transaction, (OracleConnection)connection);
                    break;
                default:
                    DbHelperSQL.Rollback((SqlTransaction)transaction, (SqlConnection)connection);
                    break;
            }
        }


        /// <summary>
        /// �õ��������
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction GetTransaction()
        {
            IDbTransaction objTransaction;
            objTransaction = null;
         
            //switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            //{
            //    case "1":
            //        objTransaction =new SqlTransaction() ;
            //        break;
            //    case "2":
            //         objTransaction =new OracleTransaction() ;
            //        break;
            //    default:
            //        objTransaction =new SqlTransaction() ;
            //        break;
            //}
            return objTransaction;
        }
        /// <summary>
        /// �õ����Ӷ���
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetConnection()
        {
            IDbConnection objConnection;
            objConnection = null;

            //switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            //{
            //    case "1":
            //        objTransaction =new SqlTransaction() ;
            //        break;
            //    case "2":
            //         objTransaction =new OracleTransaction() ;
            //        break;
            //    default:
            //        objTransaction =new SqlTransaction() ;
            //        break;
            //}
            return objConnection;
        }

           /// <summary>
        /// ִ��SQL��䣬����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
        public static string ExecuteSqlwithOutput(string SQLString)
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["DataBaseType"])
            {
                case "1":
                    return DbHelperSQL.ExecuteSqlwithOutput(SQLString); 
                    break;
                case "2":
                    return DbHelperOra.ExecuteSqlwithOutput(SQLString);
                    break;
                default:
                    return DbHelperSQL.ExecuteSqlwithOutput(SQLString);
                    break;
            }


        }
        #endregion


        public static void RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            //SqlDataReader returnReader;
            //return returnReader;
        }
    }
}
