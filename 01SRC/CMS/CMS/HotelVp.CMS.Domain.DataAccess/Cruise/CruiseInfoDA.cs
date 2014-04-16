using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
using System.Collections;

//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class CruiseInfoDA
    {
        public static DataSet GetCruiseShipPlanInfo()
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCruiseShipPlanInfo");
            return cmd.ExecuteDataSet();
        }

        public static DataSet GetCruiseBoadPlanInfo(string shipID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCruiseBoadPlanInfo");
            cmd.SetParameterValue("@ShipID", shipID);
            return cmd.ExecuteDataSet();
        }
        public static DataSet GetBoatRoomInfo(string BoatID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCruiseBoatRoomPlanInfo");
            cmd.SetParameterValue("@BoatID", BoatID);
            DataSet dsBoat = cmd.ExecuteDataSet();
            return dsBoat;
        }


        public static DataSet GetCruiseRoomPlanInfo(string BoatID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCruiseBoatRoomPlanInfo");
            cmd.SetParameterValue("@BoatID", BoatID);
            DataSet dsBoat = cmd.ExecuteDataSet();

            DataCommand roomCmd = DataCommandManager.GetDataCommand("GetCruiseRoomPlanInfo");
            roomCmd.SetParameterValue("@BoatID", BoatID);
            DataSet dsRoom = roomCmd.ExecuteDataSet();


            DataSet dsResult = new DataSet();

            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("ShipPlanID");
            dsResult.Tables[0].Columns.Add("出船日");
            for (int i = 0; i < dsBoat.Tables[0].Rows.Count; i++)
            {
                dsResult.Tables[0].Columns.Add(dsBoat.Tables[0].Rows[i]["ShipRoomNM"].ToString());
            }

            foreach (DataRow drRow in dsRoom.Tables[0].Rows)
            {
                DataRow dr = dsResult.Tables[0].NewRow();
                dr["ShipPlanID"] = drRow["ShipPlanID"].ToString();
                dr["出船日"] = DateTime.Parse(drRow["PlanDT"].ToString()).Month.ToString() + DateTime.Parse(drRow["PlanDT"].ToString()).Day.ToString().PadLeft(2,'0'); 

                string[] plannum = drRow["PlanNum"].ToString().Split(',');
                for (int j = 0; j < plannum.Length; j++)
                {
                    dr[j + 2] = plannum[j].ToString();
                }

                dsResult.Tables[0].Rows.Add(dr);
            }
            return dsResult;
        }

        public static DataSet GetCruiseDataInfo(string CruiseID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCruiseDataInfo");
            cmd.SetParameterValue("@CruiseID", CruiseID);
            return cmd.ExecuteDataSet();
        }

        public static DataSet GetCruiseDataPlanInfo(string CruiseID, string SDtime, string EDtime)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCruiseDataPlanInfo");
            CruiseID = CruiseID.Substring((CruiseID.IndexOf('[') + 1), (CruiseID.IndexOf(']') - 1));
            cmd.SetParameterValue("@CruiseID", CruiseID);
            cmd.SetParameterValue("@SDtime", SDtime);
            cmd.SetParameterValue("@EDtime", EDtime);
            return cmd.ExecuteDataSet();
        }

        public static CruiseInfoEntity SaveCruiseInfo(CruiseInfoEntity cruiseInfoEntity)
        {
            CruiseInfoDBEntity dbParm = (cruiseInfoEntity.CruiseInfoDBEntity.Count > 0) ? cruiseInfoEntity.CruiseInfoDBEntity[0] : new CruiseInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.CruiseID))
            {
                DataCommand cmd = DataCommandManager.GetDataCommand("CCruiseDataInfo");
                cmd.SetParameterValue("@ShipNM", dbParm.ShipNM);
                cmd.SetParameterValue("@Destination", dbParm.Destination);
                cmd.SetParameterValue("@Days", dbParm.Days);
                cmd.SetParameterValue("@Port", dbParm.Port);
                cmd.SetParameterValue("@CUser", cruiseInfoEntity.LogMessages.Username);
                cmd.ExecuteNonQuery();
            }
            else
            {
                DataCommand cmd = DataCommandManager.GetDataCommand("UCruiseDataInfo");
                string CruiseID = dbParm.CruiseID.Substring((dbParm.CruiseID.IndexOf('[') + 1), (dbParm.CruiseID.IndexOf(']') - 1));

                cmd.SetParameterValue("@CruiseID", CruiseID);
                cmd.SetParameterValue("@ShipNM", dbParm.ShipNM);
                cmd.SetParameterValue("@Destination", dbParm.Destination);
                cmd.SetParameterValue("@Days", dbParm.Days);
                cmd.SetParameterValue("@Port", dbParm.Port);
                cmd.SetParameterValue("@UUser", cruiseInfoEntity.LogMessages.Username);
                cmd.ExecuteNonQuery();
            }
            cruiseInfoEntity.Result = 1;
            return cruiseInfoEntity;
        }

        public static CruiseInfoEntity SaveCruisePlanInfo(CruiseInfoEntity cruiseInfoEntity)
        {
            //CruiseInfoDBEntity dbParm = (cruiseInfoEntity.CruiseInfoDBEntity.Count > 0) ? cruiseInfoEntity.CruiseInfoDBEntity[0] : new CruiseInfoDBEntity();

            //if (String.IsNullOrEmpty(dbParm.PlanID))
            //{
            //    DataCommand cmd = DataCommandManager.GetDataCommand("CCruisePlanInfo");
            //    cmd.SetParameterValue("@RouteID", dbParm.CruiseID);
            //    cmd.SetParameterValue("@PlanDTime", dbParm.PlanDTime);
            //    cmd.SetParameterValue("@PlanNumer", dbParm.PlanNumer);
            //    cmd.SetParameterValue("@CUser", cruiseInfoEntity.LogMessages.Username);
            //    cmd.ExecuteNonQuery();
            //    cruiseInfoEntity.CruiseInfoDBEntity[0].PlanID = cmd.GetParameterValue("@PlanID").ToString();
            //}
            //else
            //{
            //    DataCommand cmd = DataCommandManager.GetDataCommand("UCruisePlanInfo");

            //    cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            //    cmd.SetParameterValue("@PlanNumer", dbParm.PlanNumer);
            //    cmd.SetParameterValue("@UUser", cruiseInfoEntity.LogMessages.Username);
            //    cmd.ExecuteNonQuery();
            //}
            //cruiseInfoEntity.Result = 1;
            //return cruiseInfoEntity;


            CruiseInfoDBEntity dbParm = (cruiseInfoEntity.CruiseInfoDBEntity.Count > 0) ? cruiseInfoEntity.CruiseInfoDBEntity[0] : new CruiseInfoDBEntity();


            DataCommand icmd = DataCommandManager.GetDataCommand("GetShipPlanInfo");
            icmd.SetParameterValue("@ShipPlanID", dbParm.PlanID.Split('-')[0].ToString());
            DataSet dsinfo = icmd.ExecuteDataSet();

            if (dsinfo.Tables[0].Rows.Count == 0)
            {
                cruiseInfoEntity.Result = 0;
                return cruiseInfoEntity;
            }

            string strNum = dsinfo.Tables[0].Rows[0]["PlanNum"].ToString();

            string[] numlist = strNum.Split(',');
            int i = int.Parse(dbParm.PlanID.Split('-')[1].ToString());
            numlist[i] = dbParm.PlanNumer;
            strNum = "";
            for (int j = 0; j < numlist.Count(); j++ )
            {
                strNum = strNum + numlist[j].ToString() + ",";
            }
            strNum = strNum.TrimEnd(',');

            DataCommand cmd = DataCommandManager.GetDataCommand("UShipPlanInfo");
            cmd.SetParameterValue("@ShipPlanID", dbParm.PlanID.Split('-')[0].ToString());
            cmd.SetParameterValue("@PlanNum", strNum);
            cmd.SetParameterValue("@UUser", cruiseInfoEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();

            cruiseInfoEntity.Result = 1;
            return cruiseInfoEntity;
        }


        public static CruiseInfoEntity SaveCruisePlanList(CruiseInfoEntity cruiseInfoEntity)
        {
            CruiseInfoDBEntity dbParm = (cruiseInfoEntity.CruiseInfoDBEntity.Count > 0) ? cruiseInfoEntity.CruiseInfoDBEntity[0] : new CruiseInfoDBEntity();

            if ("0".Equals(dbParm.Action))
            {
                DataCommand cmd = DataCommandManager.GetDataCommand("CShipPlanInfo");

                cmd.SetParameterValue("@BoatID", dbParm.BoatID);
                cmd.SetParameterValue("@PlanDT", dbParm.CreateStart);
                cmd.SetParameterValue("@PlanNum", dbParm.PlanNumer.TrimEnd(','));
                cmd.SetParameterValue("@CUser", cruiseInfoEntity.LogMessages.Username);
                cmd.ExecuteNonQuery();
                cruiseInfoEntity.CruiseInfoDBEntity[0].PlanID = cmd.GetParameterValue("@PlanID").ToString();

                string[] strRooms = dbParm.PlanNumer.TrimEnd(',').Split(',');
                for (int i =0;i < strRooms.Count(); i++)
                {
                    CommonEntity _commonEntity = new CommonEntity();
                    _commonEntity.LogMessages = cruiseInfoEntity.LogMessages;
                    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
                    CommonDBEntity commonDBEntity = new CommonDBEntity();

                    commonDBEntity.Event_Type = "游轮库存-保存";
                    commonDBEntity.Event_ID = cruiseInfoEntity.CruiseInfoDBEntity[0].PlanID + "-" + i.ToString();
                    string conTent = "库存保存 - 初始化库存:{0}";

                    conTent = string.Format(conTent, strRooms[i].ToString());
                    commonDBEntity.Event_Content = conTent;
                    _commonEntity.CommonDBEntity.Add(commonDBEntity);
                    CommonDA.InsertEventHistory(_commonEntity);
                }
            }
            else
            {
                //DataCommand cmd = DataCommandManager.GetDataCommand("UCruisePlanInfo");

                //cmd.SetParameterValue("@PlanID", dbParm.PlanID);
                //cmd.SetParameterValue("@PlanNumer", dbParm.PlanNumer);
                //cmd.SetParameterValue("@UUser", cruiseInfoEntity.LogMessages.Username);
                //cmd.ExecuteNonQuery();
            }
            cruiseInfoEntity.Result = 1;
            return cruiseInfoEntity;
        }

        public static DataSet ReviewRoutePlanHistory(string CruiseID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("ReviewRoutePlanHistory");
            cmd.SetParameterValue("@PlanID", CruiseID);
            return cmd.ExecuteDataSet();
        }
    }
}
