using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.JobConsole.Entity;

namespace HotelVp.JobConsole.DataAccess
{
    public abstract class CommonDA
    {
        public static CommonEntity GetEventHistoryList(CommonEntity commonEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetEventHistoryList");

            CommonDBEntity dbParm = (commonEntity.CommonDBEntity.Count > 0) ? commonEntity.CommonDBEntity[0] : new CommonDBEntity();
            cmd.SetParameterValue("@USERID", dbParm.UserID);
            cmd.SetParameterValue("@USERNAME", dbParm.UserName);
            cmd.SetParameterValue("@IPADDRESS", dbParm.IpAddress);
            cmd.SetParameterValue("@EVENTTYPE", dbParm.Event_Type);
            cmd.SetParameterValue("@EVENTID", dbParm.Event_ID);
            cmd.SetParameterValue("@EVENTCONTENT", dbParm.Event_Content);
            cmd.SetParameterValue("@EVENTRESULT", dbParm.Event_Result);
            cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);

            cmd.SetParameterValue("@PageCurrent", commonEntity.PageCurrent);
            cmd.SetParameterValue("@PageSize", commonEntity.PageSize);
            cmd.SetParameterValue("@SortField", commonEntity.SortField);
            cmd.SetParameterValue("@SortType", commonEntity.SortType);
            commonEntity.QueryResult = cmd.ExecuteDataSet();
            commonEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            return commonEntity;
        }

        public static int InsertEventHistory(CommonEntity CommonEntity)
        {
            if (CommonEntity.CommonDBEntity.Count == 0)
            {
                return 0;
            }
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertEventHistory");
            foreach (CommonDBEntity dbParm in CommonEntity.CommonDBEntity)
            {
                cmd.SetParameterValue("@USERID", (CommonEntity.LogMessages != null) ? CommonEntity.LogMessages.Userid : "");
                cmd.SetParameterValue("@USERNAME", (CommonEntity.LogMessages != null) ? CommonEntity.LogMessages.Username : "");
                cmd.SetParameterValue("@IPADDRESS", (CommonEntity.LogMessages != null) ? CommonEntity.LogMessages.IpAddress : "");
                cmd.SetParameterValue("@EVENTTYPE", dbParm.Event_Type);
                cmd.SetParameterValue("@EVENTID", dbParm.Event_ID);
                cmd.SetParameterValue("@EVENTCONTENT", dbParm.Event_Content);
                cmd.SetParameterValue("@EVENTRESULT", dbParm.Event_Result);
                cmd.ExecuteNonQuery();
            }
            return 1;
        }

        public static int InsertEventHistory(string content)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertEventHistoryContent");

            cmd.SetParameterValue("@EVENTCONTENT", content);
            return cmd.ExecuteNonQuery();
        }

        public static int InsertEventHistoryError(string taskID,string content)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertEventHistoryContentError");
            cmd.SetParameterValue("@EVENTTYPE", "发送Push信息-JOB异常");
            cmd.SetParameterValue("@EVENTID", taskID);
            cmd.SetParameterValue("@EVENTCONTENT", content);
            return cmd.ExecuteNonQuery();
        }

        public static DataSet GetConfigList(string Typetring)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectConfigList");
            cmd.SetParameterValue("@Type", Typetring);
            return cmd.ExecuteDataSet();
        }
    }
}