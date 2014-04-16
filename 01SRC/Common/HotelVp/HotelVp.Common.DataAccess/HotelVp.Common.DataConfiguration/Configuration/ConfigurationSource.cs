using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace HotelVp.Common.Configuration
{
    public interface IConfigurationSource
    {
        T GetSection<T>(string sectionName) where T : ConfigurationSection;
    }

    public class LocalConfigurationSource : IConfigurationSource
    {
        public T GetSection<T>(string sectionName) where T : ConfigurationSection
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }
    }

    public class GlobalConfigurationSource : IConfigurationSource
    {
        private static ExeConfigurationFileMap fileMap;
        private string configurationFilepath;

        private GlobalConfigurationSource(string configurationFilepath)
        {
            if (String.IsNullOrEmpty(configurationFilepath))
            {
                throw new ArgumentException("configurationFilepath");
            }

            this.configurationFilepath = RootConfigurationFilePath(configurationFilepath);

            if (!File.Exists(this.configurationFilepath))
            {
                throw new FileNotFoundException(String.Format("The configuration file {0} could not be found.", this.configurationFilepath));
            }
        }

        public static GlobalConfigurationSource Create()
        {
            GlobalSettingsSection section = (GlobalSettingsSection)ConfigurationManager.GetSection("cms/globalSettings");

            if (section == null)
            {
                throw new ConfigurationErrorsException("The global configuration is null, please check your config file, make sure tha cms/globalSettings is exists.");
            }

            return new GlobalConfigurationSource(section.FilePath);
        }

        public T GetSection<T>(string sectionName) where T : ConfigurationSection
        {
            if (fileMap == null)
            {
                fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = RootConfigurationFilePath(configurationFilepath);
            }

            object section = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None).GetSection(sectionName);

            return (T)section;
        }

        private static string RootConfigurationFilePath(string configurationFile)
        {
            string rootedConfigurationFile = (string)configurationFile.Clone();

            if (!Path.IsPathRooted(rootedConfigurationFile))
            {
                rootedConfigurationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rootedConfigurationFile);
            }

            return rootedConfigurationFile;
        }
    }
}