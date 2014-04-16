using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

namespace HotelVp.Common.DBUtility
{
    /// <summary>
    /// Copyright (C) 2004-2008 LiTianPing 
    /// 数据访问基础类(基于Oracle)
    /// 可以用户可以修改满足自己项目的需要。


    /// </summary>
    public abstract class DbHelperOra
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
        public static string connectionString = PubConstant.ConnectionString;
        public static string queryConnectionString = PubConstant.QueryConnectionString;
        public DbHelperOra()
        {
        }

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数


        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (Exception E)
                    {
                        throw E;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。


        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (Exception E)
                {
                    tx.Rollback();
                    throw E;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。


        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception E)
                {
                    tx.Rollback();
                    throw E;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。


        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(SQLString, connection);
                System.Data.OracleClient.OracleParameter myParameter = new System.Data.OracleClient.OracleParameter("@content", OracleType.NVarChar);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (Exception E)
                {
                    throw E;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(strSQL, connection);
                System.Data.OracleClient.OracleParameter myParameter = new System.Data.OracleClient.OracleParameter("@fs", OracleType.LongRaw);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (Exception E)
                {
                    throw E;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        public static bool Exists(string strSql, bool SqlType)
        {
            object obj = DbHelperOra.GetSingle(strSql, SqlType);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, bool SqlType)
        {
            string strConnection = SqlType ? queryConnectionString : connectionString;
            using (OracleConnection connection = new OracleConnection(strConnection))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(string strSQL, bool SqlType)
        {
            string strConnection = SqlType ? queryConnectionString : connectionString;
            OracleConnection connection = new OracleConnection(strConnection);
            OracleCommand cmd = new OracleCommand(strSQL, connection);
            try
            {
                connection.Open();
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, bool SqlType)
        {
            string strConnection = SqlType ? queryConnectionString : connectionString;
            using (OracleConnection connection = new OracleConnection(strConnection))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询SQL</param>
        /// <param name="StartRecord">开始记录数</param>
        /// <param name="MaxRecord">取得数据数</param>
        /// <returns></returns>
        public static DataSet Query(string SQLString, int StartRecord, int MaxRecord, bool SqlType)
        {
            string strConnection = SqlType ? queryConnectionString : connectionString;
            using (OracleConnection connection = new OracleConnection(strConnection))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                    command.Fill(ds, StartRecord, MaxRecord, "ds");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }
        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数


        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (Exception E)
                    {
                        throw E;
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();
                        
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。


        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OracleParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    OracleCommand cmd = new OracleCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            OracleParameter[] cmdParms = (OracleParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        public static bool Exists(string strSql, bool SqlType, params OracleParameter[] cmdParms)
        {
            object obj = DbHelperOra.GetSingle(strSql, SqlType, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, bool SqlType, params OracleParameter[] cmdParms)
        {
            string strConnection = SqlType ? queryConnectionString : connectionString;
            using (OracleConnection connection = new OracleConnection(strConnection))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();

                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(string SQLString, bool SqlType, params OracleParameter[] cmdParms)
        {
            string strConnection = SqlType ? queryConnectionString : connectionString;
            OracleConnection connection = new OracleConnection(strConnection);
            OracleCommand cmd = new OracleCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();

            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, bool SqlType, params OracleParameter[] cmdParms)
        {
            string strConnection = SqlType ? queryConnectionString : connectionString;
            using (OracleConnection connection = new OracleConnection(strConnection))
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();

                    }
                    return ds;
                }
            }
        }

                /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, int StartRecord, int MaxRecord, bool SqlType, params OracleParameter[] cmdParms)
        {
            string strConnection = SqlType ? queryConnectionString : connectionString;
            using (OracleConnection connection = new OracleConnection(strConnection))
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds,StartRecord, MaxRecord, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();

                    }
                    return ds;
                }
            }
        }

        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="connection">连接对象</param>
        /// <param name="strsql">sql语句</param>
        /// <remarks>
        /// cs add
        /// </remarks>
        public static void ExecuteSqlTran(OracleTransaction transaction, OracleConnection connection, string strsql)
        {

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.CommandText = strsql;
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 打开事务
        /// </summary>
        /// <param name="transaction">返回事务对象</param>
        /// <param name="connection">返回连接对象</param>
        public static void BeginTran(out OracleTransaction transaction, out OracleConnection connection)
        {
            connection = new OracleConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="connection">连接对象</param>
        public static void Commit(OracleTransaction transaction, OracleConnection connection)
        {
            transaction.Commit();
            connection.Close();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="connection">连接对象</param>
        public static void Rollback(OracleTransaction transaction, OracleConnection connection)
        {
            transaction.Rollback();
            connection.Close();
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数


        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static string ExecuteSqlwithOutput(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    { 
                        // PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        cmd.Parameters.Add(new OracleParameter("@FolderPath", OracleType.VarChar, 800));
                        cmd.Parameters["@FolderPath"].Direction = ParameterDirection.Output;
                        cmd.CommandText = SQLString;
                          
                        connection.Open();
                        cmd.Connection = connection;

                        cmd.ExecuteNonQuery();
                        string strReturn = cmd.Parameters["@FolderPath"].Value.ToString();
                        cmd.Parameters.Clear();
                        connection.Close();
                        return strReturn;
                    }
                    catch (Exception  e)
                    {   
                        throw e;
                    }
                }
            }
        }

     

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程 返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            OracleDataReader returnReader;
            connection.Open();
            OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                OracleDataAdapter sqlDA = new OracleDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


        /// <summary>
        /// 构建 OracleCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand</returns>
        private static OracleCommand BuildQueryCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OracleCommand command = new OracleCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                int result;
                connection.Open();
                OracleCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 OracleCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand 对象实例</returns>
        private static OracleCommand BuildIntCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new OracleParameter("ReturnValue",
                OracleType.Int32, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        public static int GetMaxID(string FieldName, string TableName, bool SqlType)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql, SqlType);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static int ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> cmdList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    OracleCommand cmd = new OracleCommand();
                    try
                    {
                        int count = 0;
                        //循环
                        foreach (CommandInfo myDE in cmdList)
                        {
                            string cmdText = myDE.CommandText;
                            OracleParameter[] cmdParms = (OracleParameter[])myDE.Parameters;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            //if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                            //{
                            //    trans.Rollback();
                            //    return 0;
                            //}
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return count;
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

    }
}
