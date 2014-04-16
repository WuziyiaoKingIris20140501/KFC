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
    public abstract class AutoHotelPlanDA
    {
        public static DataSet AutoListSelect(AutoHotelPlanEntity autohotelplanEntity)
        {
            AutoHotelPlanDBEntity dbParm = (autohotelplanEntity.AutoHotelPlanDBEntity.Count > 0) ? autohotelplanEntity.AutoHotelPlanDBEntity[0] : new AutoHotelPlanDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("AutoListSelect");
            return cmd.ExecuteDataSet(); ;
        }

        public static string HotelRoomNM(string hotelId, string roomCode)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMID",OracleType.VarChar)
                                };

            parm[0].Value = hotelId;
            parm[1].Value = roomCode;

            DataSet dsResult = DbManager.Query("AutoHotelPlan", "t_lm_hotelroom_name", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["HOTELROOMNM"].ToString();
            }
            else
            {
                return "";
            }
        }

         public static DataSet GetRoomInfo(string strCurrtDate, string HotelID, string RoomCode)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CurrtDate",OracleType.VarChar),
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("RoomCode",OracleType.VarChar),
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar)
                                };

            parm[0].Value = strCurrtDate;
            parm[1].Value = HotelID;
            parm[2].Value = RoomCode;

            DateTime dtNow = DateTime.Now;
             string strStartDT = "";
             if (dtNow.Hour >= 8)
             {
                 strStartDT = dtNow.ToShortDateString() + " 08:00:00";
             }
             else if (dtNow.Hour >= 4 && dtNow.Hour <= 8)
             {
                 strStartDT = dtNow.ToShortDateString() + " 04:00:00";
             }
             else if (dtNow.Hour >= 0 && dtNow.Hour < 4)
             {
                 strStartDT = dtNow.AddDays(-1).ToShortDateString() + " 08:00:00";
             }

             parm[3].Value = strStartDT;
             parm[4].Value = dtNow.ToString();

             //Console.WriteLine("sql start CurrtDate:" + parm[0].Value +"HotelID:" + parm[1].Value + "RoomCode:" + parm[2].Value + "StartDate:"+parm[3].Value + "EndDate:" + parm[4].Value);

            return DbManager.Query("AutoHotelPlan", "t_lm_hotelroom_info_less", true, parm);//"t_lm_hotelroom_info"
        }

        public static int UpdateSalesPlanEventStatus(string PlanID, string Status, string Action, string Result, string Username)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanEventStatus");
            cmd.SetParameterValue("@HPID", PlanID);
            cmd.SetParameterValue("@Status", Status);
            //cmd.SetParameterValue("@Action", Action);
            cmd.SetParameterValue("@Result", Result);
            cmd.SetParameterValue("@Update_User", Username);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public static int UpdateSalesPlanEventJobStatus(string JobID, string Action, string Result, string Username)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanEventJobStatus");
            cmd.SetParameterValue("@JID", JobID);
            cmd.SetParameterValue("@Action", Action);
            cmd.SetParameterValue("@Result", Result);
            cmd.SetParameterValue("@Update_User", Username);
            cmd.ExecuteNonQuery();
            return 1;
        }
    }
}