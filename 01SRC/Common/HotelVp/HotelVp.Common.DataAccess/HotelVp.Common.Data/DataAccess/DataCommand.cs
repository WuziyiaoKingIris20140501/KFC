/*****************************************************************
 * Copyright (C) hotelvp Corporation. All rights reserved.
 * 
 * Author:   
 * Create Date:  08/26/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Data;
using HotelVp.Common.Entity;

namespace HotelVp.Common.DataAccess
{
	
	/// <summary>
	/// 
	/// </summary>
	public class DataCommand : ICloneable
	{
		protected DbCommand m_DbCommand;
		protected string m_DatabaseName;

		#region constructors
		
		internal DataCommand(string databaseName, DbCommand command)
		{
			m_DatabaseName = databaseName;
			m_DbCommand = command;
		}

		private DataCommand()
		{
		}

        public int CommandTimeout
        {
            set
            {
                m_DbCommand.CommandTimeout = value;
            }
            get
            {
                return m_DbCommand.CommandTimeout;
            }
        }

		public object Clone()
		{
			DataCommand cmd = new DataCommand();
			if (m_DbCommand != null)
			{
				if (m_DbCommand is ICloneable)
				{
					cmd.m_DbCommand = ((ICloneable)m_DbCommand).Clone() as DbCommand;
				}
				else
				{
					throw new ApplicationException("A class that implements IClonable is expected.");
				}
			}
			cmd.m_DatabaseName = m_DatabaseName;
			return cmd;
		}

		protected Database ActualDatabase
		{
			// Note: use late binding to reflect the real configuration.
			get { return DatabaseManager.GetDatabase(m_DatabaseName); }
		}
		#endregion

		#region parameters
		/// <summary>
		/// get a parameter value
		/// </summary>
		/// <param name="paramName"></param>
		/// <returns></returns>
		public Object GetParameterValue(string paramName)
		{
			return ActualDatabase.GetParameterValue(m_DbCommand, paramName);
		}
		
		/// <summary>
		/// set a parameter value 
        /// <remarks>当传入值为null时，替换为DBNull</remarks>
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="val"></param>
		public void SetParameterValue(string paramName, Object val)
		{
            
            object value = val;
            if (value == null)
            {
                value = DBNull.Value;
            }
            else if (value.ToString() == default(DateTime).ToString())
            {
                value = DBNull.Value;
            }
            ActualDatabase.SetParameterValue(m_DbCommand, paramName, value);
		}

        /// <summary>
        /// replace a colummn name using a parameter value 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="val"></param>
        public void ReplaceParameterValue(string paramName, string paramValue)
        {
            if (null != m_DbCommand) m_DbCommand.CommandText = m_DbCommand.CommandText.Replace(paramName, paramValue);
        }
		#endregion

		#region execution
		/// <summary>
		/// Executes the scalar.
		/// Throws an exception if an error occurs.
		/// </summary>
		/// <returns></returns>
		public T ExecuteScalar<T>()
		{
			try
			{
                //return (T)ActualDatabase.ExecuteScalar(m_DbCommand);
                return Retry<T>(delegate
                {
                    return (T)ActualDatabase.ExecuteScalar(m_DbCommand);
                });
			}
			catch(Exception ex)
			{
				Exception result= DataAccessLogger.LogExecutionError(m_DbCommand,ex);
                throw result;
			}
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <returns></returns>
        public Object ExecuteScalar()
        {
            try
            {
                //return ActualDatabase.ExecuteScalar(m_DbCommand);
                return Retry<Object>(delegate
                {
                    return ActualDatabase.ExecuteScalar(m_DbCommand);
                });
            }
            catch (Exception ex)
            {
                Exception result = DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw result;
            }
        }

		/// <summary>
		/// returns the number of rows affected.
		/// </summary>
		/// <returns></returns>
		public int ExecuteNonQuery()
		{
			try
			{
                //return ActualDatabase.ExecuteNonQuery(m_DbCommand);
                return Retry<int>(delegate
                {
                    return ActualDatabase.ExecuteNonQuery(m_DbCommand);
                });
			}
			catch (Exception ex)
			{
                Exception result = DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw result;
			}
		}

		/// <summary>
		/// Executes the entity.
		/// Returns null if no entity is returned or the execution failed.
		/// </summary>
		/// <returns></returns>
		public T ExecuteEntity<T>() where T : class, new()
		{
			IDataReader reader = null;
			try
			{
                //reader = ActualDatabase.ExecuteReader(m_DbCommand);
                //if (reader.Read())
                //{
                //    return EntityBuilder.BuildEntity<T>(reader);
                //}
                //else
                //{
                //    return null;
                //}
                return Retry<T>(delegate
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    reader = ActualDatabase.ExecuteReader(m_DbCommand);
                    if (reader.Read())
                    {
                        return EntityBuilder.BuildEntity<T>(reader);
                    }
                    else
                    {
                        return null;
                    }
                });
			}
			catch (Exception ex)
			{
                Exception result = DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw result;
			}
			finally
			{
				if (reader != null)
				{
					reader.Dispose();
				}
			}
		}

		/// <summary>
		/// Executes the entity list.
		/// Returns an empty list if no entity is returned or the execution fails.
		/// </summary>
		/// <returns></returns>
		public List<T> ExecuteEntityList<T>() where T : class, new()
		{
			IDataReader reader = null;
			try
			{
                //reader = ActualDatabase.ExecuteReader(m_DbCommand);
                //List<T> list = new List<T>();
                //while (reader.Read())
                //{
                //    T entity = EntityBuilder.BuildEntity<T>(reader);
                //    list.Add(entity);
                //}
                //return list;
                return Retry<List<T>>(delegate
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    reader = ActualDatabase.ExecuteReader(m_DbCommand);
                    List<T> list = new List<T>();
                    while (reader.Read())
                    {
                        T entity = EntityBuilder.BuildEntity<T>(reader);
                        list.Add(entity);
                    }
                    return list;
                });
			}
			catch (Exception ex)
			{
                Exception result = DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw result;
			}
			finally
			{
				if (reader != null)
				{
					reader.Dispose();
				}
			}
		}

        public K ExecuteEntityCollection<T, K>() where T : class, new() where K : ICollection<T>, new()
        {
            IDataReader reader = null;
            try
            {
                //reader = ActualDatabase.ExecuteReader(m_DbCommand);
                //ICollection<T> list = new K();
                //while (reader.Read())
                //{
                //    T entity = EntityBuilder.BuildEntity<T>(reader);
                //    list.Add(entity);
                //}
                //return (K)list;
                return Retry<K>(delegate
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    reader = ActualDatabase.ExecuteReader(m_DbCommand);
                    ICollection<T> list = new K();
                    while (reader.Read())
                    {
                        T entity = EntityBuilder.BuildEntity<T>(reader);
                        list.Add(entity);
                    }
                    return (K)list;
                });
            }
            catch (Exception ex)
            {
                Exception result = DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw result;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		/// <remarks>Use with caution. Remember to dispose the returned reader.</remarks>
		public IDataReader ExecuteDataReader()
		{
			try
			{
                //return ActualDatabase.ExecuteReader(m_DbCommand);
                return Retry<IDataReader>(delegate
                {
                    return ActualDatabase.ExecuteReader(m_DbCommand);
                });
			}
			catch (Exception ex)
			{
                Exception result = DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw result;
			}
		}

		public DataSet ExecuteDataSet()
		{
			try
			{
                //return ActualDatabase.ExecuteDataSet(m_DbCommand);
                return Retry<DataSet>(delegate
                {
                    return ActualDatabase.ExecuteDataSet(m_DbCommand);
                });
			}
			catch (Exception ex)
			{
                Exception result = DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw result;
			}
		}
		#endregion

        private T Retry<T>(RetryHandler<T> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function can not be null.");
            }

            int retryTimes = 5;
            double intervalSeconds = 0.1;

            // 10054 为下面这个异常的 Number。
            // A transport-level error has occurred when sending the request
            // to the server. (provider: TCP Provider, error: 0 - An existing
            // connection was forcibly closed by the remote host.)
          
            //10053 An established connection was aborted by the software in your host machine. 

            T t = default(T);

            for (int i = 0; i < retryTimes; i++)
            {
                try
                {
                    t = function();
                    break;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 10054 || ex.Number == 10053)
                    {
                        if (i == retryTimes - 1)
                        {
                            throw;
                        }
                        else
                        {
                            SqlConnection.ClearAllPools();
                            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(intervalSeconds));
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return t;
        }

        private delegate T RetryHandler<T>();
	}
}