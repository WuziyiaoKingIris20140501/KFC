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
    public abstract class AutoMappingBedDA
    {
        public static int InsertOrderConfirmData(DataRow drRow)
        {
            OracleParameter[] lmParm ={
                                   new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("FOGORDERNUM",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(drRow["id"].ToString().Trim()))
            {
                lmParm[0].Value = DBNull.Value;
            }
            else
            {
                lmParm[0].Value = drRow["id"].ToString().Trim();
            }

            if (String.IsNullOrEmpty(drRow["fog_order_num"].ToString().Trim()))
            {
                lmParm[1].Value = DBNull.Value;
            }
            else
            {
                lmParm[1].Value = drRow["fog_order_num"].ToString().Trim();
            }

            lmParm[2].Value = "System";


            return DbManager.ExecuteSql("AutoMappingBed", "t_lm_order_cof_insert", lmParm);
        }

        public static DataSet AutoListSelectOrder(AutoMappingBedEntity ordersynchronizingEntity)
        {
            AutoHotelComparisonDBEntity dbParm = (ordersynchronizingEntity.AutoHotelComparisonDBEntity.Count > 0) ? ordersynchronizingEntity.AutoHotelComparisonDBEntity[0] : new AutoHotelComparisonDBEntity();
            
            OracleParameter[] lmParm ={
                                   new OracleParameter("ORDEID",OracleType.VarChar)
                                };

            if ("0".Equals(dbParm.SaveType))
            {
                lmParm[0].Value = DBNull.Value;
            }
            else
            {
                lmParm[0].Value = GetMaxOrderID();
            }

            DataSet dsResult = DbManager.Query("AutoMappingBed", "AutoSelectOrderList", false, lmParm);
            return dsResult;
        }

        public static string GetMaxOrderID()
        {
            DataSet dsResult = DbManager.Query("AutoMappingBed", "AutoGetMaxOrderID", false);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static DataSet GetMailToList(string strUserID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetMailToList");
            cmd.SetParameterValue("@UserID", strUserID);
            return cmd.ExecuteDataSet();
        }

        public static int SaveOrderSynchronizeCommonList(List<CommandInfo> cmdList)
        {
            DbHelperOra.ExecuteSqlTran(cmdList);
            return 1;
        }


        public static DataSet AutoHotelRoomMappingList(AutoMappingBedEntity ordersynchronizingEntity)
        {
            DataSet dsResult = DbManager.Query("AutoMappingBed", "AutoHotelRoomMappingList", false);
            return dsResult;
        }


        public static int SaveHotelRoomBed(DataRow drRow)
        {
            OracleParameter[] lmParm ={
                                   new OracleParameter("HOTELID",OracleType.VarChar),
                                   new OracleParameter("ROOMCODE",OracleType.VarChar),
                                   new OracleParameter("ROOMAREA",OracleType.VarChar),
                                   new OracleParameter("UPDATEUSER",OracleType.VarChar)
                                };

            lmParm[0].Value = drRow["Hotel_ID"].ToString().Trim();
            lmParm[1].Value = drRow["Room_Code"].ToString().Trim();
            lmParm[2].Value = drRow["ROOMBED"].ToString().Trim();
            lmParm[3].Value = "CMS JOB";
            return DbManager.ExecuteSql("AutoMappingBed", "SaveHotelRoomBed", lmParm);
        }

        public static int UpdateOverDateData()
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateOverDateData");
            return cmd.ExecuteNonQuery();
        }

        public static int SaveHotelRoomBedList(List<CommandInfo> cmdList)
        {
            DbHelperOra.ExecuteSqlTran(cmdList);
            return 1;
        }

        public static int SaveHotelComparison(DataRow drRow)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SaveHotelComparison");

            cmd.SetParameterValue("@Hotel_ID", drRow["Hotel_ID"].ToString().Trim());
            cmd.SetParameterValue("@Hotel_Name", drRow["Hotel_Name"].ToString().Trim());
            cmd.SetParameterValue("@Room_Code", drRow["Room_Code"].ToString().Trim());
            cmd.SetParameterValue("@Room_Name", drRow["Room_Name"].ToString().Trim());
            cmd.SetParameterValue("@Mapping_Hotel", drRow["Mapping_Hotel"].ToString().Trim());
            cmd.SetParameterValue("@Mapping_Room", drRow["Mapping_Room"].ToString().Trim());
            cmd.SetParameterValue("@City_ID", drRow["City_ID"].ToString().Trim());
            cmd.SetParameterValue("@MPType", drRow["MPType"].ToString().Trim());
            cmd.SetParameterValue("@DType", drRow["DType"].ToString().Trim());
            cmd.SetParameterValue("@DValue", drRow["DValue"].ToString().Trim());
            cmd.SetParameterValue("@Two_Price", drRow["Two_Price"].ToString().Trim());
            cmd.SetParameterValue("@Mapping_Price", drRow["Mapping_Price"].ToString().Trim());
            cmd.SetParameterValue("@Act_Price", drRow["Act_Price"].ToString().Trim());
            cmd.SetParameterValue("@CreateUser", "CMS JOB");

            Console.Write("CMS DB  Insert: HOTELID:" + drRow["Hotel_ID"].ToString().Trim());

            return cmd.ExecuteNonQuery();
        }

        public static DataSet GetMailDataList()
        {
            DataSet dsResult = new DataSet();

            DataTable dtMaster = new DataTable();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetMailDataList");
            dtMaster = cmd.ExecuteDataSet().Tables[0].Copy();
            dtMaster.TableName = "Master";

            DataTable dtDetail = new DataTable();
            dtDetail = DbManager.Query("AutoMappingBed", "GetMailBodyList", false).Tables[0].Copy();
            dtDetail.TableName = "Detail";


            DataTable dtTotal = new DataTable();
            DataCommand totalCmd = DataCommandManager.GetDataCommand("GetMailDataTotalList");
            dtTotal = totalCmd.ExecuteDataSet().Tables[0].Copy();
            dtTotal.TableName = "Total";

            dsResult.Tables.Add(dtMaster);
            dsResult.Tables.Add(dtDetail);
            dsResult.Tables.Add(dtTotal);
            return dsResult;
        }
    }
}