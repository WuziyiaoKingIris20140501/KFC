using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;

using HotelVp.JobConsole.Entity;

namespace HotelVp.JobConsole.DataAccess
{
    public abstract class AutoReciveFaxDA
    {
        public static int SaveAutoReciveFax(string strFAX_URL, string strOPERATOR, string strFAX_ID, string strFAX_NUM)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("FAX_URL",OracleType.VarChar),
                                    new OracleParameter("OPERATOR",OracleType.VarChar),
                                    new OracleParameter("FAX_ID",OracleType.VarChar),
                                    new OracleParameter("FAX_NUM",OracleType.VarChar)
                                };
            parm[0].Value = strFAX_URL;
            parm[1].Value = strOPERATOR;
            parm[2].Value = strFAX_ID;
            parm[3].Value = strFAX_NUM;
            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("AutoReciveFax", "t_fax_unknown_save", parm);
        }
    }
}