using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.DataConfiguration
{
    public class NormalDataInstance : ConfigIntanceBase
    {
        private string _dbConfigFile = DataAccessSetting.DatabaseConfigFile;
        private string _dataCommandFileListConfigFile = DataAccessSetting.DataCommandFileListConfigFile;

        public override string DatabaseConfigFile
        {
            get
            {
                return this._dbConfigFile;
            }
        }


        public override string DataCommandFileListConfigFile
        {
            get
            {
                return this._dataCommandFileListConfigFile;
            }
        }
    }
}
