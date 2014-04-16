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
    public abstract class AutoMsgCancelOrdDA
    {
        public static DataSet AutoListSelect(AutoMsgCancelOrdEntity automsgcancelordEntity)
        {
            AutoMsgCancelOrdDBEntity dbParm = (automsgcancelordEntity.AutoMsgCancelOrdDBEntity.Count > 0) ? automsgcancelordEntity.AutoMsgCancelOrdDBEntity[0] : new AutoMsgCancelOrdDBEntity();
            DataSet dsResult = new DataSet();
            OracleParameter[] parm ={
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar)
                                };
            string strStartDT = "";
            string strEndDT = "";

            if ("1".Equals(dbParm.TypeID))
            {
                strStartDT = DateTime.Now.ToShortDateString() + " 04:00:00";
                strEndDT = DateTime.Now.ToShortDateString() + " 16:00:00";
            }
            else
            {
                strStartDT = SetParamDtime(-79);
                strEndDT = SetParamDtime(-69);
            }

            parm[0].Value = strStartDT;
            parm[1].Value = strEndDT;

            dsResult = DbManager.Query("AutoMsgCancelOrd", "AutoListSelect", true, parm);
            return dsResult;
        }

        private static string SetParamDtime(int AddMin)
        {
            string strResult = string.Empty;
            string strDtimeNow = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":00";
            strResult = DateTime.Parse(strDtimeNow).AddMinutes(AddMin).ToString();
            return strResult;
        }
    }
}