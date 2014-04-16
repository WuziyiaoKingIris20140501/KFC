/*****************************************************************
 * Copyright (C) hotelvp Corporation. All rights reserved.
 * 
 * Author:   
 * Create Date:  09/05/2006 08:11:35
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;

namespace HotelVp.Common.DataAccess
{
	/// <summary>
	/// Logs critical information for diagnostics and performance improvement.
	/// </summary>
	internal static class DataAccessLogger
	{
        private const string LOG_CATEGORY_NAME = "HotelVp.Common.DataAccess";

		#region event ids
		private const int LoadDatabaseFile = 1;
		private const int LoadCommandInventoryFile = 2;
		private const int LoadCommandFile = 3;
		private const int DBFileChanged = 10;
		private const int EXECUTION_ERROR = 20;
		#endregion

		//[Conditional("TRACE")]
		public static void LogDatabaseFileChanged(FileSystemEventArgs arg)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("File name: " + arg.FullPath + System.Environment.NewLine);
			sb.Append("Change type: " + arg.ChangeType.ToString());
			//LogEvent(DBFileChanged, sb.ToString());
		}

		public static Exception LogDatabaseFileLoaded(string fileName,Exception sourceException)
		{
            
			string msg = "Database config file loaded: " + fileName;
            Exception result = new Exception(msg,sourceException);
            return result;
			//LogEvent(LoadDatabaseFile, msg);
		}

		//[Conditional("TRACE")]
        public static Exception LogDatabaseCommandFileLoaded(String fileName,Exception sourceException)
		{
			string msg = "Data command file loaded: " + fileName;
            Exception result = new Exception(msg,sourceException);
            return result;
			//LogEvent(LoadCommandFile, msg);
		}

		//[Conditional("TRACE")]
        public static Exception LogDataCommandInventoryFileLoaded(string fileName, int count, Exception sourceException)
		{
			string msg = "Data command inventory file loaded: " + fileName + ". " + count.ToString() + " command file(s) found.";
            Exception result = new Exception(msg, sourceException);
            return result;
			//LogEvent(LoadCommandInventoryFile, msg);
		}
		//[Conditional("TRACE")]
		public static Exception LogExecutionError(DbCommand cmd, Exception ex)
		{
             
           
			StringBuilder sb = new StringBuilder();
			sb.Append("DataCommand Execution error, command text:");
			sb.Append(System.Environment.NewLine);
			sb.Append(cmd.CommandText);
            sb.Append(System.Environment.NewLine);
            if (cmd != null)
            {
                sb.Append("command parameters inforamtion:");
                sb.Append(System.Environment.NewLine);

                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    sb.AppendFormat("parameters name:{0}, parameters value:{1}, parameters type:{2}", cmd.Parameters[i].ParameterName, cmd.Parameters[i].Value, cmd.Parameters[i].DbType);
                    sb.Append(System.Environment.NewLine);
                }
            }
            sb.Append(System.Environment.NewLine);
			sb.Append("Exception: ");
			sb.Append(ex.Message);
            Exception result = new Exception(sb.ToString(), ex);
            return result;
			//LogEvent(EXECUTION_ERROR, sb.ToString());
		}

		private static void LogEvent(int eventId, string message)
		{
			//Logger.LogEvent(LOG_CATEGORY_NAME, eventId, message);
		}
	}
}
