using System;
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
using HotelVp.SELT.Domain.Entity;

namespace HotelVp.SELT.Domain.DA
{
    public abstract class WriteOffDA
    {
        public static WriteOffEntity GetOutstandingBalanceList(WriteOffEntity writeoffEntity)
        {
            WriteOffDBEntity dbParm = (writeoffEntity.WriteOffDBEntity.Count > 0) ? writeoffEntity.WriteOffDBEntity[0] : new WriteOffDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetOutstandingBalanceList");
            cmd.SetParameterValue("@ImpDealTimeST", dbParm.ImpDealTimeST);
            cmd.SetParameterValue("@ImpDealTimeDT", dbParm.ImpDealTimeDT);
            cmd.SetParameterValue("@ImpAmountST", dbParm.ImpAmountST);
            cmd.SetParameterValue("@ImpAmountDT", dbParm.ImpAmountDT);
            cmd.SetParameterValue("@ImpUser", dbParm.ImpUser);
            cmd.SetParameterValue("@ImpPayName", dbParm.ImpPayName);
            cmd.SetParameterValue("@ImpPayAccount", dbParm.ImpPayAccount);
            cmd.SetParameterValue("@ImpMatch", dbParm.ImpMatch);

            cmd.SetParameterValue("@PageCurrent", writeoffEntity.PageCurrent);
            cmd.SetParameterValue("@PageSize", writeoffEntity.PageSize);
            cmd.SetParameterValue("@SortField", writeoffEntity.SortField);
            cmd.SetParameterValue("@SortType", writeoffEntity.SortType);

            writeoffEntity.WriteOffDBEntity[0].DtWriteoff = cmd.ExecuteDataSet().Tables[0].Copy();
            writeoffEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            return writeoffEntity;
        }

        public static WriteOffEntity GetOutstandingBalanceUnitList(WriteOffEntity writeoffEntity)
        {
            DataCommand unitCmd = DataCommandManager.GetDataCommand("GetOutstandingBalanceUnitList");
            //unitCmd.SetParameterValue("@", dbParm.);
            //unitCmd.SetParameterValue("@", dbParm.);
            //unitCmd.SetParameterValue("@", dbParm.);
            //unitCmd.SetParameterValue("@", dbParm.);
            //unitCmd.SetParameterValue("@", dbParm.);

            //writeoffEntity.  = unitCmd.ExecuteDataSet();
            return writeoffEntity;
        }

        public static WriteOffEntity GetOutstandingSettleUnitList(WriteOffEntity writeoffEntity)
        {
            WriteOffDBEntity dbParm = (writeoffEntity.WriteOffDBEntity.Count > 0) ? writeoffEntity.WriteOffDBEntity[0] : new WriteOffDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetOutstandingSettleUnitList");

            cmd.SetParameterValue("@UnitName", dbParm.UnitName);
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@HotelGroup", dbParm.HotelGroup);
            cmd.SetParameterValue("@SlAmountST", dbParm.SlAmountST);
            cmd.SetParameterValue("@SlAmountDT", dbParm.SlAmountDT);
            cmd.SetParameterValue("@SlPayNo", dbParm.SlPayNo);

            cmd.SetParameterValue("@PageCurrent", writeoffEntity.PageCurrent);
            cmd.SetParameterValue("@PageSize", writeoffEntity.PageSize);
            cmd.SetParameterValue("@SortField", writeoffEntity.SortField);
            cmd.SetParameterValue("@SortType", writeoffEntity.SortType);

            writeoffEntity.WriteOffDBEntity[0].DtSlUnitList = cmd.ExecuteDataSet().Tables[0].Copy();
            writeoffEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");

            return writeoffEntity;
        }

        //public static WriteOffEntity ConfirmCollectHis(WriteOffEntity writeoffEntity)
        //{
        //    WriteOffDBEntity dbParm = (writeoffEntity.WriteOffDBEntity.Count > 0) ? writeoffEntity.WriteOffDBEntity[0] : new WriteOffDBEntity();
        //    // 回款销账状态更新 -> 1   0:导入 1:销账
        //    DataCommand ofCmd = DataCommandManager.GetDataCommand("ConfirmWriteOffHis");
        //    ofCmd.SetParameterValue("@WRITEOFFID", dbParm.WriteOffID);
        //    ofCmd.SetParameterValue("@OPEUSER", writeoffEntity.LogMessages.Userid);
        //    ofCmd.ExecuteNonQuery();

        //    // 更新结算单  回款销账ID
        //    DataCommand unitCmd = DataCommandManager.GetDataCommand("ConfirmCollectHis");
        //    unitCmd.SetParameterValue("@SLID", dbParm.SLID);
        //    unitCmd.SetParameterValue("@WRITEOFFID", dbParm.WriteOffID);
        //    unitCmd.SetParameterValue("@OPEUSER", writeoffEntity.LogMessages.Userid);

        //    writeoffEntity.Result = unitCmd.ExecuteNonQuery();
        //    return writeoffEntity;
        //}

        public static WriteOffEntity GetInvoiceHisList(WriteOffEntity writeoffEntity)
        {
            WriteOffDBEntity dbParm = (writeoffEntity.WriteOffDBEntity.Count > 0) ? writeoffEntity.WriteOffDBEntity[0] : new WriteOffDBEntity();
            DataCommand unitCmd = DataCommandManager.GetDataCommand("GetInvoiceHisList");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            writeoffEntity.WriteOffDBEntity[0].DtInvoiceHis = unitCmd.ExecuteDataSet().Tables[0].Copy();
            return writeoffEntity;
        }

        public static WriteOffEntity AddSettleAdjust(WriteOffEntity writeoffEntity)
        {
            WriteOffDBEntity dbParm = (writeoffEntity.WriteOffDBEntity.Count > 0) ? writeoffEntity.WriteOffDBEntity[0] : new WriteOffDBEntity();
            DataCommand unitCmd = DataCommandManager.GetDataCommand("AddSettleAdjust");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            unitCmd.SetParameterValue("@SETTLETYPE", dbParm.SettleType);//0:酒店少付 1:HVP多收
            unitCmd.SetParameterValue("@ADJUSTNM", dbParm.AdjustName);// SlMonth + SettleType = 2013年4月HVP多收
            unitCmd.SetParameterValue("@ADJUSTAMOUNT", dbParm.AdjustAmount);
            unitCmd.SetParameterValue("@REMARK", dbParm.AdjustRemark);
            unitCmd.SetParameterValue("@STATUS", "0");
            unitCmd.SetParameterValue("@CREATEUSER", writeoffEntity.LogMessages.Userid);

            writeoffEntity.Result = unitCmd.ExecuteNonQuery();
            return writeoffEntity;
        }

        public static WriteOffEntity AddIssueSettle(WriteOffEntity writeoffEntity)
        {
            DataCommand unitCmd = DataCommandManager.GetDataCommand("AddIssueSettle");
            //unitCmd.SetParameterValue("@", dbParm.);
            //unitCmd.SetParameterValue("@", dbParm.);
            //unitCmd.SetParameterValue("@", dbParm.);
            //unitCmd.SetParameterValue("@", dbParm.);
            //unitCmd.SetParameterValue("@", dbParm.);

            writeoffEntity.Result = unitCmd.ExecuteNonQuery();
            return writeoffEntity;
        }

        public static WriteOffEntity ConfirmSettleList(WriteOffEntity writeoffEntity)
        {
            WriteOffDBEntity dbParm = (writeoffEntity.WriteOffDBEntity.Count > 0) ? writeoffEntity.WriteOffDBEntity[0] : new WriteOffDBEntity();
            // 回款销账状态更新 -> 1   0:导入 1:销账
            DataCommand ofCmd = DataCommandManager.GetDataCommand("ConfirmWriteOffHis");
            ofCmd.SetParameterValue("@WRITEOFFID", dbParm.WriteOffID);
            ofCmd.SetParameterValue("@OPEUSER", writeoffEntity.LogMessages.Userid);
            ofCmd.ExecuteNonQuery();

            foreach (DataRow drRow in dbParm.DtSettleAllHis.Rows)
            {
                if ("0".Equals(drRow["TYPE"].ToString()))
                {
                    if (!String.IsNullOrEmpty(drRow["UNITREMARK"].ToString()))
                    {
                        // 更新结算单  回款销账ID 清结算单更新状态为status='4' 同时更新销账确认信息
                        DataCommand unitCmd = DataCommandManager.GetDataCommand("ConfirmCollectSelHis");
                        unitCmd.SetParameterValue("@SLID", drRow["SLID"].ToString());
                        unitCmd.SetParameterValue("@WRITEOFFID", dbParm.WriteOffID);
                        unitCmd.SetParameterValue("@AMOUNT", drRow["AMOUNT"].ToString());
                        unitCmd.SetParameterValue("@REMARK", drRow["REMARK"].ToString());
                        unitCmd.SetParameterValue("@UNITREMARK", drRow["UNITREMARK"].ToString());
                        unitCmd.SetParameterValue("@OPEUSER", writeoffEntity.LogMessages.Userid);
                        unitCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // 更新结算单  回款销账ID 清结算单更新状态为status='4' 同时更新销账确认信息
                        DataCommand unitCmd = DataCommandManager.GetDataCommand("ConfirmCollectHis");
                        unitCmd.SetParameterValue("@SLID", drRow["SLID"].ToString());
                        unitCmd.SetParameterValue("@WRITEOFFID", dbParm.WriteOffID);
                        unitCmd.SetParameterValue("@AMOUNT", drRow["AMOUNT"].ToString());
                        unitCmd.SetParameterValue("@REMARK", drRow["REMARK"].ToString());
                        unitCmd.SetParameterValue("@OPEUSER", writeoffEntity.LogMessages.Userid);
                        unitCmd.ExecuteNonQuery();
                    }
                   
                }
                else
                {
                    // 更新调整项  回款销账ID 清结算调整项 更新状态为status='1' 同时更新销账确认信息
                    DataCommand unitCmd = DataCommandManager.GetDataCommand("ConfirmItemHis");
                    unitCmd.SetParameterValue("@SETTLEID", drRow["SLID"].ToString());
                    unitCmd.SetParameterValue("@WRITEOFFID", dbParm.WriteOffID);
                    unitCmd.SetParameterValue("@AMOUNT", drRow["AMOUNT"].ToString());
                    unitCmd.SetParameterValue("@REMARK", drRow["REMARK"].ToString());
                    unitCmd.SetParameterValue("@OPEUSER", writeoffEntity.LogMessages.Userid);
                    unitCmd.ExecuteNonQuery();
                }
            }
            writeoffEntity.Result = 1;
            return writeoffEntity;
        }
    }
}
