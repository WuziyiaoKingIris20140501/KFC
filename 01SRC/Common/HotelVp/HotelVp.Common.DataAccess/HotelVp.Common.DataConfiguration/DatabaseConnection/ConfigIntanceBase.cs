using System;
using System.Collections.Generic;
using System.Text;
using HotelVp.Common.Utilities.Encryption;


namespace HotelVp.Common.DataConfiguration
{
    public abstract class ConfigIntanceBase
    {
        public abstract string DatabaseConfigFile
        {
            get;
        }

        public abstract string DataCommandFileListConfigFile
        {
            get;
        }


        public IList<DatabaseInstance> GetIntanceList()
        {
            IList<DatabaseInstance> dbConnectList = null;
            DatabaseList list = HotelVp.Common.Utilities.SerializationUtility.LoadFromXml<DatabaseList>(this.DatabaseConfigFile);
            if (list != null && list.DatabaseInstances != null && list.DatabaseInstances.Length != 0)
            {
                dbConnectList = new List<DatabaseInstance>(list.DatabaseInstances);
                //Ω¯––Ω‚√‹
                foreach (DatabaseInstance item in dbConnectList)
                {
                    item.ConnectionString = item.ConnectionString;//EncryptionHelper.Decrypt(item.ConnectionString);
                }
            }
           
            return dbConnectList;
        }
    }
}
