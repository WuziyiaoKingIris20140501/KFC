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
using HotelVp.CMS.Domain.Entity;
using System.Configuration;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
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
            string IsWriteLog = ConfigurationManager.AppSettings["IsWriteLog"].ToString();
            if ("0".Equals(IsWriteLog))
            {
                return 1;
            }

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

        public static DataSet GetConfigList(string TypeString)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectConfigList");
            cmd.SetParameterValue("@Type", TypeString);
            return cmd.ExecuteDataSet();
        }

        public static DataSet GetConfigListForSort(string TypeString)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectConfigListForSort");
            cmd.SetParameterValue("@Type", TypeString);
            return cmd.ExecuteDataSet();
        }

        public static DataSet GetConfigVale(string TypeString, string KeyString)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectConfigValue");
            cmd.SetParameterValue("@Type", TypeString);
            cmd.SetParameterValue("@Key", KeyString);
            return cmd.ExecuteDataSet();
        }

        public static int InsertEventHistory(string content)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertEventHistoryContent");

            cmd.SetParameterValue("@EVENTCONTENT", content);
            return cmd.ExecuteNonQuery();
        }

        public static int InsertOrderActionHistory(OrderInfoEntity OrderEntity)
        {
            if (OrderEntity.OrderInfoDBEntity.Count == 0)
            {
                return 0;
            }

            OrderInfoDBEntity dbParm = (OrderEntity.OrderInfoDBEntity.Count > 0) ? OrderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertOrderActionHistory");
            
            cmd.SetParameterValue("@FGID", dbParm.ORDER_NUM);
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            cmd.SetParameterValue("@ACTIONID", dbParm.ActionID);
            cmd.SetParameterValue("@ODSTATUS", dbParm.OdStatus);
            cmd.SetParameterValue("@REMARK", dbParm.REMARK);
            cmd.SetParameterValue("@CANNEL", dbParm.CANNEL);
            cmd.SetParameterValue("@USERID", (OrderEntity.LogMessages != null) ? OrderEntity.LogMessages.Userid : "");
            cmd.ExecuteNonQuery();
            return 1;
        }

        public static int InsertOrderActionHistoryList(OrderInfoEntity OrderEntity)
        {
            if (OrderEntity.OrderInfoDBEntity.Count == 0)
            {
                return 0;
            }

            OrderInfoDBEntity dbParm = (OrderEntity.OrderInfoDBEntity.Count > 0) ? OrderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertOrderActionHistoryList");
            
            cmd.SetParameterValue("@FGID", dbParm.ORDER_NUM);
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            cmd.SetParameterValue("@ACTIONID", dbParm.ActionID);
            cmd.SetParameterValue("@ODSTATUS", dbParm.OdStatus);
            cmd.SetParameterValue("@REMARK", dbParm.REMARK);
            cmd.SetParameterValue("@CANNEL", dbParm.CANNEL);
            cmd.SetParameterValue("@INROOMID", dbParm.InRoomID);
            cmd.SetParameterValue("@APPROVEID", dbParm.ApproveId);
            cmd.SetParameterValue("@USERID", (OrderEntity.LogMessages != null) ? OrderEntity.LogMessages.Userid : "");
            cmd.SetParameterValue("@ISDBAPPROVE", dbParm.IsDbApprove);
            cmd.ExecuteNonQuery();
            return 1;
        }
         
        public static int InsertConsultRoomHistory(APPContentEntity APPContentEntity)
        {
            string IsWriteLog = ConfigurationManager.AppSettings["IsWriteLog"].ToString();
            if ("0".Equals(IsWriteLog))
            {
                return 1;
            }

            if (APPContentEntity.APPContentDBEntity.Count == 0)
            {
                return 0;
            }
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertConsultRoomHistory");
            foreach (APPContentDBEntity dbParm in APPContentEntity.APPContentDBEntity)
            {
                cmd.SetParameterValue("@CityID", dbParm.CityID);
                cmd.SetParameterValue("@HotelID", dbParm.HotelID );
                cmd.SetParameterValue("@PlanDate", dbParm.PlanTime);
                cmd.SetParameterValue("@PriceCode", dbParm.PriceCode);
                cmd.SetParameterValue("@RoomCode", dbParm.RoomCode);
                cmd.SetParameterValue("@TwoPrice", dbParm.TwoPrice);
                cmd.SetParameterValue("@RoomNum", dbParm.RoomCount);
                cmd.SetParameterValue("@Status", dbParm.PlanStatus);
                cmd.SetParameterValue("@Remark", dbParm.Remark);
                cmd.SetParameterValue("@IsReserve", dbParm.IsReserve);
                cmd.SetParameterValue("@Create_User", dbParm.CreateUser);
                 
                cmd.ExecuteNonQuery();
            }
            return 1;
        }
    }
}
