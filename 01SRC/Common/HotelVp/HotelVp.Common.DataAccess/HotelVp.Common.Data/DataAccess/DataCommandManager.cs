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
using System.Data;
using HotelVp.Common.DataConfiguration;
using HotelVp.Common.Utilities;

namespace HotelVp.Common.DataAccess
{

	using DataCommandHashtable = Dictionary<string, DataCommand>;
    

	public static class DataCommandManager
	{

		#region fields 
		private const string EventCategory = "DataCommandManager";

		private const int FILE_CHANGE_NOTIFICATION_INTERVAL = 500;
		private static FileSystemChangeEventHandler s_FileChangeHandler;
		private static Object s_CommandSyncObject;
		private static Object s_CommandFileListSyncObject;

		private static FileSystemWatcher s_Watcher;
		private static DataCommandHashtable s_DataCommands;
		private static string s_DataFileFolder;
        private static ConfigIntanceBase _commonConfigInstance;

		/// <summary>
		/// records datacommand file and command list relationship
		/// key: file name
		/// value: list of datacommand names
		/// </summary>
		private static Dictionary<string, IList<string>> s_FileCommands;
        private static Dictionary<string, dataOperationsDataCommand> s_dataConfigurationData;
		#endregion

		static DataCommandManager()
		{
            _commonConfigInstance = DbInstanceManager.CreateDbInance();
			s_FileChangeHandler = new FileSystemChangeEventHandler(FILE_CHANGE_NOTIFICATION_INTERVAL);
			s_FileChangeHandler.ActualHandler += new FileSystemEventHandler(Watcher_Changed);

			s_DataFileFolder = Path.GetDirectoryName(_commonConfigInstance.DataCommandFileListConfigFile);

			s_CommandSyncObject = new object();
			s_CommandFileListSyncObject = new object();

            if (s_DataFileFolder == null
                || !Directory.Exists(s_DataFileFolder))
            {
                return;
            }

			s_Watcher = new FileSystemWatcher(s_DataFileFolder);
			s_Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
			s_Watcher.Changed += new FileSystemEventHandler(s_FileChangeHandler.ChangeEventHandler);
			s_Watcher.EnableRaisingEvents = true;

			UpdateAllCommandFiles();
		}

		/// <summary>
		/// invoked when a file change occurs.
		/// Note:
		///		1. one change at a time.
		///		2. if the inventory file changes then all the datacommands are reloaded.
		///		3. this function is thread safe.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			// check datacommand list file
			if (string.Compare(e.FullPath, _commonConfigInstance.DataCommandFileListConfigFile, true) == 0)
			{
				// reload all datacommands
				UpdateAllCommandFiles();
				return;
			}

			// check data command file
			lock (s_CommandFileListSyncObject)
			{
				foreach (string file in s_FileCommands.Keys)
				{
					if (string.Compare(file, e.FullPath, true) == 0)
					{
						UpdateCommandFile(file);
						// only one file is watched at a time. 
						// if break is not used here, s_FileCommands is changed in UpdateCommandFile and an exception
						// will be thrown in the next iteration.
						break;
					}
				}
			}
		}

		private static string DataCommandListFileName
		{
			get { return Path.GetFileName(_commonConfigInstance.DataCommandFileListConfigFile); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m_FileNamePattern"></param>
		/// <exception cref="DataCommandFileLoadException">m_FileNamePattern does not exist or contains invalid information</exception>
		private static void UpdateCommandFile(string fileName)
		{
			IList<string> commandNames;
			if (s_FileCommands.ContainsKey(fileName))
			{
				commandNames = s_FileCommands[fileName];
			}
			else
			{
				commandNames = null;
			}

			lock (s_CommandSyncObject)
			{
				// copy from existing hashtable
				DataCommandHashtable newCommands = new DataCommandHashtable(s_DataCommands);

				// remove existing data commands
				if (commandNames != null)
				{
					foreach (string commandName in commandNames)
					{
						newCommands.Remove(commandName);
					}
				}

				// load from file and add to commands
                dataOperations commands = Utilities.SerializationUtility.LoadFromXml<dataOperations>(fileName);
				if (commands == null)
				{
					throw new DataCommandFileLoadException(fileName);
				}
				if (commands.dataCommand != null && commands.dataCommand.Length > 0)
				{
					foreach (dataOperationsDataCommand cmd in commands.dataCommand)
					{
                        try
                        {
                            newCommands.Add(cmd.name, cmd.GetDataCommand());
                            if (s_dataConfigurationData.ContainsKey(cmd.name))
                            {
                                s_dataConfigurationData[cmd.name] = cmd;
                            }
                            else
                            {
                                s_dataConfigurationData.Add(cmd.name, cmd);
                            }
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("Command:" + cmd.name + " has exists.", ex);
                        }
					}
					s_DataCommands = newCommands;
				}

				// update file-command list relationship
				s_FileCommands[fileName] = commands.GetCommandNames();
				//DataAccessLogger.LogDatabaseCommandFileLoaded(fileName);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="DataCommandFileNotSpecifiedException"> if the datacommand file list 
		/// configuration file does not contain any valid file name.
		/// </exception>
		private static void UpdateAllCommandFiles()
		{
			lock (s_CommandFileListSyncObject)
			{
				// reload file list content
				ConfigDataCommandFileList fileList =
                    Utilities.SerializationUtility.LoadFromXml<ConfigDataCommandFileList>(_commonConfigInstance.DataCommandFileListConfigFile);
                if (fileList != null && fileList.FileList != null)
                {
                    //throw new DataCommandFileNotSpecifiedException();


                    //DataAccessLogger.LogDataCommandInventoryFileLoaded(DataAccessSetting.DataCommandFileListConfigFile, 
                    //    fileList.FileList.Length);

                    // clear file-command name relationship
                    s_FileCommands = new Dictionary<string, IList<string>>();

                    // clear commands
                    s_DataCommands = new DataCommandHashtable();

                    s_dataConfigurationData = new Dictionary<string, dataOperationsDataCommand>();

                    // update each datacommand file
                    foreach (ConfigDataCommandFileList.DataCommandFile commandFile in fileList.FileList)
                    {
                        string directory = Path.GetPathRoot(commandFile.FileName);
                        string fileName = string.Empty;
                        if (directory == string.Empty || directory.StartsWith("\\"))
                        {
                            fileName = Path.Combine(s_DataFileFolder, commandFile.FileName);
                        }
                        else
                        {
                            fileName = commandFile.FileName;
                        }
                        UpdateCommandFile(fileName);
                    }
                }
			}
		}


		/// <summary>
		/// Get DataCommand corresponding to the given command name.
		/// </summary>
		/// <param name="name">Name of the DataCommand </param>
		/// <returns>DataCommand</returns>
		/// <exception cref="KeyNotFoundException">the specified DataCommand does not exist.</exception>
		public static DataCommand GetDataCommand(string name)
		{
			// Logger.LogSystemInfo(EventCategory, "Retrieving datacommand: " + name);
			return s_DataCommands[name].Clone() as DataCommand;
		}

		public static CustomDataCommand CreateCustomDataCommand(string databaseAliasName)
		{
            return new CustomDataCommand(databaseAliasName);
		}

        public static CustomDataCommand CreateCustomDataCommand(string databaseAliasName, CommandType commandType)
		{
            return new CustomDataCommand(databaseAliasName, commandType);
		}

        public static CustomDataCommand CreateCustomDataCommand(string databaseAliasName, CommandType commandType, string commandText)
		{
            return new CustomDataCommand(databaseAliasName, commandType, commandText);
		}

        public static CustomDataCommand CreateCustomDataCommandFromConfig(string sqlNameInConfig)
        {
            dataOperationsDataCommand configData = s_dataConfigurationData[sqlNameInConfig];
            if (configData == null)
            {
                throw new System.ApplicationException("Can not find any configuration match the input name in SQL configuration files.");
            }
            CommandType commandType = CommandType.Text;
            switch (configData.commandType)
            {
                case dataOperationsDataCommandCommandType.StoredProcedure:
                    commandType = CommandType.StoredProcedure;
                    break;
                case dataOperationsDataCommandCommandType.TableDirect:
                    commandType = CommandType.TableDirect;
                    break;
                case dataOperationsDataCommandCommandType.Text:
                    commandType = CommandType.Text;
                    break;
            }
            CustomDataCommand result = new CustomDataCommand(configData.database, commandType, configData.commandText);
            return result;
        }
	}
}
