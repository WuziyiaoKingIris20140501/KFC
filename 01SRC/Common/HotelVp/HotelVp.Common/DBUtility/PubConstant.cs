using System;
using System.Configuration;
namespace HotelVp.Common.DBUtility
{
    
    public class PubConstant
    {        
        /// <summary>
        /// ��ȡ�����ַ���
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
        /// ��ȡ�����ַ���
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
        /// �õ�web.config������������ݿ������ַ�����
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
        /// ��ȡ�����ַ���
        /// </summary>
        public static string PageSizeString
        {
            get
            {
                return ConfigurationManager.AppSettings["PageCountString"];
            }
        }

        /// <summary>
        /// ��ȡ��ҳ�����ַ���
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
