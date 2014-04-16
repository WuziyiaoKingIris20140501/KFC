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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using HotelVp.Common.DataConfiguration;

namespace HotelVp.Common.DataAccess
{
	internal static class DatabaseManager
	{
		private static Dictionary<string, Database> s_DatabaseHashtable;
		private static FileSystemWatcher s_Watcher;
		private static FileSystemChangeEventHandler s_FileChangeHandler;
        private static ConfigIntanceBase _commonConfigInstance;

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		static DatabaseManager()
		{
			s_DatabaseHashtable = new Dictionary<string,Database>();
			s_FileChangeHandler = new FileSystemChangeEventHandler(500);
			s_FileChangeHandler.ActualHandler += new FileSystemEventHandler(OnFileChanged);
            _commonConfigInstance = DbInstanceManager.CreateDbInance();
			
			// set up file system watcher
            string databaseFolder = Path.GetDirectoryName(_commonConfigInstance.DatabaseConfigFile);
            string databaseFile = Path.GetFileName(_commonConfigInstance.DatabaseConfigFile);
			s_Watcher = new FileSystemWatcher(databaseFolder);
			s_Watcher.Filter = databaseFile;
			s_Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
			s_Watcher.Changed += new FileSystemEventHandler(s_FileChangeHandler.ChangeEventHandler);
			s_Watcher.EnableRaisingEvents = true;

			// load database
			s_DatabaseHashtable = LoadDatabaseList();
		}

		private static void OnFileChanged(Object sender, FileSystemEventArgs e)
		{
			//DataAccessLogger.LogDatabaseFileChanged(e);
			s_DatabaseHashtable = LoadDatabaseList();
		}

		private static Dictionary<string, Database> LoadDatabaseList()
		{
            //DatabaseList list = ObjectXmlSerializer.LoadFromXml<DatabaseList>(DataAccessSetting.DatabaseConfigFile);

            IList<DatabaseInstance> list = _commonConfigInstance.GetIntanceList();
			
			if (list == null || list.Count == 0)
			{
                throw DataAccessLogger.LogDatabaseFileLoaded("", null);
			}
			// convert DatabaseList to a hashtable
			Dictionary<string, Database> hashtable = new Dictionary<string, Database>(list.Count);
            //string DbType = System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString();
			foreach (DatabaseInstance instance in list)
			{
                //if (DbType.Equals("2"))
                //{
                //    OracleDatabase db = new OracleDatabase(instance.ConnectionString);
                //    hashtable.Add(instance.Name.ToUpper(), db);
                //}
                //else
                //{
                SqlDatabase db = new SqlDatabase(instance.ConnectionString);
                hashtable.Add(instance.Name.ToUpper(), db);
                //}
				
			}

			//DataAccessLogger.LogDatabaseFileLoaded(DataAccessSetting.DatabaseConfigFile);

			return hashtable;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name">name of the database, case insensitive</param>
		/// <returns></returns>
		/// <exception cref="KeyNotFoundException">The name is not found in the database list.</exception>
		public static Database GetDatabase(string name)
		{
            return s_DatabaseHashtable[name.ToUpper()];
		}

	}
}
