/*****************************************************************
 * Copyright (C) hotelvp Corporation. All rights reserved.
 * 
 * Author:   
 * Create Date:  08/26/2006
 * Usage:
 *
 * RevisionHistory
 * Date           Author               Description
 *               Add CommonConfigurationListFile property
 * 
*****************************************************************/

using System;
using System.Configuration;
using System.IO;
using HotelVp.Common.Configuration;

namespace HotelVp.Common.DataConfiguration
{
    /// <summary>
    /// provide basic runtime environment settings for data access component.
    /// </summary>
    internal static class DataAccessSetting
    {
        private static string s_DatabaseConfigFile;
        private static string s_DataCommandFileListConfigFile;
        private static string _commonConfigurationListFile;

        private static bool m_IsCryption = true;


        internal static readonly string DatabaseConfigName = "DatabaseListFile";
        internal static readonly string DataCommandFileConfigName = "DataCommandFile";
        private static readonly string _commonConfigFilePathName = "CommonConfigFilePath";
        private static readonly string m_IsCryptionConfigName = "IsCryption";
        static DataAccessSetting()
        {
            if (CmsConfig.GlobalAppSettings[DatabaseConfigName] != null)
            {
                s_DatabaseConfigFile = Utilities.GetFileFullPath(CmsConfig.GlobalAppSettings[DatabaseConfigName]);
            }
            if (CmsConfig.LocalDataAccessManager != null && !string.IsNullOrEmpty(CmsConfig.LocalDataAccessManager.DataCommandFile))
            {
                s_DataCommandFileListConfigFile = Utilities.GetFileFullPath(CmsConfig.LocalDataAccessManager.DataCommandFile);
            }
            DataAccessSetting._commonConfigurationListFile = Utilities.GetConfigurationFullPath(DataAccessSetting._commonConfigFilePathName);
            string result = Utilities.GetConfigurationValue(m_IsCryptionConfigName);
            if (result != null && result != "")
            {
                m_IsCryption = bool.Parse(result);
            }
        }

        /// <summary>
        /// get the configuration file for database settings
        /// </summary>
        public static string DatabaseConfigFile
        {
            get { return s_DatabaseConfigFile; }
        }

        /// <summary>
        /// get the configuration file that contains the list of files for datacommand
        /// </summary>
        public static string DataCommandFileListConfigFile
        {
            get { return s_DataCommandFileListConfigFile; }
        }


        /// <summary>
        /// Get the configuration file that contains the list of configuration
        /// </summary>
        public static string CommonConfigurationListFile
        {
            get
            {
                return DataAccessSetting._commonConfigurationListFile;
            }
        }

        /// <summary>
        /// 是否进行加密解密
        /// </summary>
        public static bool IsCryption
        {
            get { return DataAccessSetting.m_IsCryption; }
        }
    }
}
