using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//using HotelVp.Common;
//using HotelVp.Common.DBUtility;
//using HotelVp.Common.Utilities;
//using HotelVp.Common.DataAccess;
//using HotelVp.Common.Configuration;
using JJZX.JobConsole.Entity;

namespace JJZX.JobConsole.DataAccess
{
    public abstract class CommonDA
    {
        public static CommonEntity GetEventHistoryList(CommonEntity commonEntity)
        {
            //DataCommand cmd = DataCommandManager.GetDataCommand("GetEventHistoryList");

            //CommonDBEntity dbParm = (commonEntity.CommonDBEntity.Count > 0) ? commonEntity.CommonDBEntity[0] : new CommonDBEntity();
            //cmd.SetParameterValue("@USERID", dbParm.UserID);
            //cmd.SetParameterValue("@USERNAME", dbParm.UserName);
            //cmd.SetParameterValue("@IPADDRESS", dbParm.IpAddress);
            //cmd.SetParameterValue("@EVENTTYPE", dbParm.Event_Type);
            //cmd.SetParameterValue("@EVENTID", dbParm.Event_ID);
            //cmd.SetParameterValue("@EVENTCONTENT", dbParm.Event_Content);
            //cmd.SetParameterValue("@EVENTRESULT", dbParm.Event_Result);
            //cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            //cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);

            //cmd.SetParameterValue("@PageCurrent", commonEntity.PageCurrent);
            //cmd.SetParameterValue("@PageSize", commonEntity.PageSize);
            //cmd.SetParameterValue("@SortField", commonEntity.SortField);
            //cmd.SetParameterValue("@SortType", commonEntity.SortType);
            //commonEntity.QueryResult = cmd.ExecuteDataSet();
            //commonEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            return commonEntity;
        }

        //public static DataSet GetConfigList(string Typetring)
        //{
        //    DataCommand cmd = DataCommandManager.GetDataCommand("SelectConfigList");
        //    cmd.SetParameterValue("@Type", Typetring);
        //    return cmd.ExecuteDataSet();
        //}
    }
}