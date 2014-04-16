using System;
using System.Configuration;
using System.Collections.Specialized;

namespace HotelVp.Common.Configuration
{
    public static class CmsConfig
    {
        public static CacheManagerSection CacheManager
        {
            get
            {
                string elementName = "cacheManager";

                GlobalConfigurationSource globalSource = GlobalConfigurationSource.Create();
                return globalSource.GetSection<CacheManagerSection>(elementName);
            }
        }

        public static GlobalLogManagerSection GlobalLogManager
        {
            get
            {
                string elementName = "logManager";

                GlobalConfigurationSource globalSource = GlobalConfigurationSource.Create();

                return globalSource.GetSection<GlobalLogManagerSection>(elementName);
            }
        }

        public static LocalLogManagerSection LocalLogManager
        {
            get
            {
                string elementName = "cms/logSettings";

                LocalConfigurationSource localSource = new LocalConfigurationSource();

                return localSource.GetSection<LocalLogManagerSection>(elementName);
            }
        }

        public static DataAccessManagerSection LocalDataAccessManager
        {
            get
            {
                string elementName = "cms/dataAccessSettings";

                LocalConfigurationSource localSource = new LocalConfigurationSource();

                return localSource.GetSection<DataAccessManagerSection>(elementName);
            }
        }


        public static NameValueCollection GlobalAppSettings
        {
            get
            {
                NameValueCollection result = new NameValueCollection ();
                GlobalConfigurationSource globalSource = GlobalConfigurationSource.Create();
                AppSettingsSection section =  globalSource.GetSection<AppSettingsSection>("appSettings");
                if(section != null)
                {
                    foreach (string key in section.Settings.AllKeys)
                    {
                        result.Add(key, section.Settings[key].Value);
                    }    
                }
                return result;
            }
        }
    }
}