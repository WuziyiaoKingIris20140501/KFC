using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Collections;


namespace HotelVp.Common.DataConfiguration
{
    public class CommonDataInstance : ConfigIntanceBase
    {
        private string _commonConfigurationFile = DataAccessSetting.CommonConfigurationListFile;
        private IDictionary<string,string> _configurationFileList = null;
        private string _dbConfigFile = string.Empty;
        private string _dataCommandFileListConfigFile = DataAccessSetting.DataCommandFileListConfigFile;


        public CommonDataInstance()
        {
            this.GetCommonConfigurationFile();

            if (this._configurationFileList.ContainsKey(DataAccessSetting.DatabaseConfigName.ToUpper()))
                this._dbConfigFile = this._configurationFileList[DataAccessSetting.DatabaseConfigName.ToUpper()].Trim();
            if (this._configurationFileList.ContainsKey(DataAccessSetting.DataCommandFileConfigName.ToUpper()))
                this._dataCommandFileListConfigFile = this._configurationFileList[DataAccessSetting.DataCommandFileConfigName.ToUpper()].Trim();
        }


        private void GetCommonConfigurationFile()
        {
            ConfigurationFileList list = HotelVp.Common.Utilities.SerializationUtility.LoadFromXml<ConfigurationFileList>(this._commonConfigurationFile);
            string commonDirectoryName = Path.GetDirectoryName(this._commonConfigurationFile);
            string filePath = string.Empty;

            if (list != null && list.ConfigurationList != null && list.ConfigurationList.Count != 0)
            {
                this._configurationFileList = new Dictionary<string, string>(list.ConfigurationList.Count);
                foreach (ConfigurationFile sub in list.ConfigurationList)
                {
                    filePath = Path.Combine(commonDirectoryName, sub.Path.Trim());
                    if(sub.IsAbsolute)
                        filePath = sub.Path.Trim();
                    this._configurationFileList.Add(sub.Key.ToUpper(), filePath);
                }
            }
        }


        /// <summary>
        /// Database.config's full path
        /// </summary>
        public override string DatabaseConfigFile
        {
            get { return this._dbConfigFile; }
        }


        /// <summary>
        /// DbCommandFiles.config's full path
        /// </summary>
        public override string DataCommandFileListConfigFile
        {
            get { return this._dataCommandFileListConfigFile; }
        }


        public IDictionary<string, string> ConfigurationFileList
        {
            get { return this._configurationFileList; }
        }
       
    }
}
