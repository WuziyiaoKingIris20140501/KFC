using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;

namespace HotelVp.Common.DataConfiguration
{
    public class DbInstanceManager
    {
        public static ConfigIntanceBase CreateDbInance()
        {
            string commonConfigString = (DataAccessSetting.CommonConfigurationListFile == null) ? string.Empty : DataAccessSetting.CommonConfigurationListFile.Trim();
            ConfigIntanceBase dbIntance = null;

            if (commonConfigString.Length > 0)
                dbIntance = new CommonDataInstance();
            else
                dbIntance = new NormalDataInstance();

            return dbIntance;
        }
        /// <summary>
        /// 指定连接字符串别名，得到连接字符串
        /// </summary>
        /// <param name="alisaName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string alisaName)
        {
            string result = string.Empty;
            string commonConfigString = (DataAccessSetting.CommonConfigurationListFile == null) ? string.Empty : DataAccessSetting.CommonConfigurationListFile.Trim();
            ConfigIntanceBase dbIntance = null;

            if (commonConfigString.Length > 0)
                dbIntance = new CommonDataInstance();
            else
                dbIntance = new NormalDataInstance();
            IList<DatabaseInstance> dbList = dbIntance.GetIntanceList();
            foreach (DatabaseInstance item in dbList)
            {
                if (item.Name.Trim() == alisaName.Trim())
                {
                    result = item.ConnectionString;
                }
            }
            return result;
        }
    }
}
