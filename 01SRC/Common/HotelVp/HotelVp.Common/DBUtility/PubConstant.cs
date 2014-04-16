using System;
using System.Configuration;
namespace HotelVp.Common.DBUtility
{
    
    public class PubConstant
    {        
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {           
            get 
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString; 
            }
        }

        public static string QueryConnectionString
        {
            get
            {
                string _queryConnectionString = ConfigurationManager.AppSettings["QueryConnectionString"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _queryConnectionString = DESEncrypt.Decrypt(_queryConnectionString);
                }
                return _queryConnectionString;
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string SQLConnectionString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionStringSQL"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string PageSizeString
        {
            get
            {
                return ConfigurationManager.AppSettings["PageCountString"];
            }
        }

        /// <summary>
        /// 获取翻页连接字符串
        /// </summary>
        public static string PageSplitString
        {
            get
            {
                return ConfigurationManager.AppSettings["PageSplitString"];
            }
        }
    }
}
