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
    public abstract class LiquidateDA
    {
        public static LiquidateEntity CommonSelect(LiquidateEntity liquidateEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PARAMETERS",OracleType.VarChar)
                                };
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();

            if (String.IsNullOrEmpty(dbParm.AutoQuery))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.AutoQuery;
            }

            liquidateEntity.QueryResult = DbManager.Query("WebAutoComplete", "t_b_auto_unit", true, parm);
            return liquidateEntity;
        }


        public static LiquidateEntity GetLiquidateList(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetLiquidateList");

            cmd.SetParameterValue("@UnitID", dbParm.UnitID);
            cmd.SetParameterValue("@UnitName", dbParm.UnitName);
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@HotelGroup", dbParm.HotelGroup);
            cmd.SetParameterValue("@CityID", dbParm.CityID);
            cmd.SetParameterValue("@UnitCharge", dbParm.UnitCharge);
            cmd.SetParameterValue("@SaveUser", dbParm.SaveUser);
            cmd.SetParameterValue("@OrderID", dbParm.OrderID);
            cmd.SetParameterValue("@SlMonth", dbParm.SlMonth);
            cmd.SetParameterValue("@SlStatus", dbParm.SlStatus);
            cmd.SetParameterValue("@BillID", dbParm.BillID);

            cmd.SetParameterValue("@PageCurrent", liquidateEntity.PageCurrent);
            cmd.SetParameterValue("@PageSize", liquidateEntity.PageSize);
            cmd.SetParameterValue("@SortField", liquidateEntity.SortField);
            cmd.SetParameterValue("@SortType", liquidateEntity.SortType);
            liquidateEntity.QueryResult = cmd.ExecuteDataSet();
            liquidateEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            return liquidateEntity;
        }

        public static LiquidateEntity GetLiquidateDetail(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();

            // 结算单详情 + 结算单位详情  结算单位是镜像已经保存在结算单内。
            DataCommand slCmd = DataCommandManager.GetDataCommand("GetSLDetial");
            slCmd.SetParameterValue("@SLID", dbParm.SLID);
            liquidateEntity.LiquidateDBEntity[0].DtSLDetial = slCmd.ExecuteDataSet().Tables[0].Copy();

            //DataCommand unitCmd = DataCommandManager.GetDataCommand("GetLiquidateUnitDetial");
            //unitCmd.SetParameterValue("@SLID", dbParm.UnitID);

            //OracleParameter[] parm ={
            //                         new OracleParameter("UnitID",OracleType.VarChar)
            //                    };
            //parm[0].Value = dbParm.UnitID;
            //DataSet dsResult = DbManager.Query("SettleInfo", "t_lm_settlement_unit_detail_select", true, parm);
            //liquidateEntity.LiquidateDBEntity[0].DtUnitDetial = dsResult.Tables[0].Copy();


            DataCommand monthCmd = DataCommandManager.GetDataCommand("GetLiquidateMonthOrder");
            monthCmd.SetParameterValue("@SLID", dbParm.SLID);
            liquidateEntity.LiquidateDBEntity[0].DtMonthOrder = monthCmd.ExecuteDataSet().Tables[0].Copy();

            DataCommand hisOrderCmd = DataCommandManager.GetDataCommand("GetLiquidateHisOrder");
            hisOrderCmd.SetParameterValue("@SLID", dbParm.SLID);
            hisOrderCmd.SetParameterValue("@UnitID", dbParm.UnitID);
            hisOrderCmd.SetParameterValue("@IntoMonth", dbParm.IntoMonth);
            liquidateEntity.LiquidateDBEntity[0].DtHisOrder = hisOrderCmd.ExecuteDataSet().Tables[0].Copy();

            DataCommand hisAmountCmd = DataCommandManager.GetDataCommand("GetLiquidateHisAmount");
            hisAmountCmd.SetParameterValue("@SLID", dbParm.SLID);
            hisAmountCmd.SetParameterValue("@UnitID", dbParm.UnitID);
            liquidateEntity.LiquidateDBEntity[0].DtHisAmount = hisAmountCmd.ExecuteDataSet().Tables[0].Copy();
            return liquidateEntity;
        }

        public static LiquidateEntity ExportLiquidateList(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();

            DataCommand slCmd = DataCommandManager.GetDataCommand("GetSLDetial");
            slCmd.SetParameterValue("@SLID", dbParm.SLID);
            liquidateEntity.LiquidateDBEntity[0].DtSLDetial = slCmd.ExecuteDataSet().Tables[0].Copy();

            //DataCommand unitCmd = DataCommandManager.GetDataCommand("GetLiquidateUnitDetial");
            //unitCmd.SetParameterValue("@SLID", dbParm.UnitID);

            //OracleParameter[] parm ={
            //                         new OracleParameter("UnitID",OracleType.VarChar)
            //                    };
            //parm[0].Value = dbParm.UnitID;
            //DataSet dsResult = DbManager.Query("SettleInfo", "t_lm_settlement_unit_detail_select", true, parm);
            //liquidateEntity.LiquidateDBEntity[0].DtUnitDetial = dsResult.Tables[0].Copy();


            DataCommand monthCmd = DataCommandManager.GetDataCommand("GetLiquidateMonthOrder");
            monthCmd.SetParameterValue("@SLID", dbParm.SLID);
            liquidateEntity.LiquidateDBEntity[0].DtMonthOrder = monthCmd.ExecuteDataSet().Tables[0].Copy();

            DataCommand hisOrderCmd = DataCommandManager.GetDataCommand("GetLiquidateHisOrder");
            hisOrderCmd.SetParameterValue("@SLID", dbParm.SLID);
            hisOrderCmd.SetParameterValue("@UnitID", dbParm.UnitID);
            hisOrderCmd.SetParameterValue("@IntoMonth", dbParm.IntoMonth);
            liquidateEntity.LiquidateDBEntity[0].DtHisOrder = hisOrderCmd.ExecuteDataSet().Tables[0].Copy();

            DataCommand hisAmountCmd = DataCommandManager.GetDataCommand("GetLiquidateHisAmount");
            hisAmountCmd.SetParameterValue("@SLID", dbParm.SLID);
            hisAmountCmd.SetParameterValue("@UnitID", dbParm.UnitID);
            liquidateEntity.LiquidateDBEntity[0].DtHisAmount = hisAmountCmd.ExecuteDataSet().Tables[0].Copy();
            return liquidateEntity;
        }

        public static LiquidateEntity SaveLiquidate(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();

            DataCommand unitCmd = DataCommandManager.GetDataCommand("SaveLiquidate");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            unitCmd.SetParameterValue("@BillConfirmUser", dbParm.BillConfirmUser);
            unitCmd.SetParameterValue("@BillConfirmRemark", dbParm.BillConfirmRemark);
            unitCmd.SetParameterValue("@UnitRemark", dbParm.UnitRemark);
            unitCmd.SetParameterValue("@OPEUser", liquidateEntity.LogMessages.Userid);
            liquidateEntity.Result = unitCmd.ExecuteNonQuery();

            //OracleParameter[] parm ={
            //                         new OracleParameter("UnitID",OracleType.VarChar),
            //                         new OracleParameter("UnitRemark",OracleType.VarChar),
            //                         new OracleParameter("OPEUser",OracleType.VarChar)
            //                    };
            //parm[0].Value = dbParm.UnitID;
            //parm[1].Value = dbParm.UnitRemark;
            //parm[2].Value = liquidateEntity.LogMessages.Userid;
            //DbManager.ExecuteSql("SettleInfo", "t_lm_settlement_unit_remark_update", parm);

            return liquidateEntity;
        }

        public static LiquidateEntity ApproveLiquidate(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();

            DataCommand unitCmd = DataCommandManager.GetDataCommand("ApproveLiquidate");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            unitCmd.SetParameterValue("@BillConfirmUser", dbParm.BillConfirmUser);
            unitCmd.SetParameterValue("@BillConfirmRemark", dbParm.BillConfirmRemark);
            unitCmd.SetParameterValue("@UnitRemark", dbParm.UnitRemark);
            unitCmd.SetParameterValue("@OPEUser", liquidateEntity.LogMessages.Userid);
            liquidateEntity.Result = unitCmd.ExecuteNonQuery();

            //OracleParameter[] parm ={
            //                         new OracleParameter("UnitID",OracleType.VarChar),
            //                         new OracleParameter("UnitRemark",OracleType.VarChar),
            //                         new OracleParameter("OPEUser",OracleType.VarChar)
            //                    };
            //parm[0].Value = dbParm.UnitID;
            //parm[1].Value = dbParm.UnitRemark;
            //parm[2].Value = liquidateEntity.LogMessages.Userid;
            //DbManager.ExecuteSql("SettleInfo", "t_lm_settlement_unit_remark_update", parm);

            return liquidateEntity;
        }

        public static LiquidateEntity AddLiquidateAdjust(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();
            DataSet dsOrderInfo = new DataSet();
            if (!"1".Equals(dbParm.LiquidationType))
            {
                dsOrderInfo = GetOrderLiquidate(liquidateEntity).QueryResult;
                if (dsOrderInfo.Tables.Count == 0 || dsOrderInfo.Tables[0].Rows.Count == 0)
                {
                    liquidateEntity.Result = 0;
                    return liquidateEntity;
                }
            }
            DataCommand unitCmd = DataCommandManager.GetDataCommand("AddLiquidateAdjust");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            unitCmd.SetParameterValue("@LIQUIDATIONTYPE", dbParm.LiquidationType);
            unitCmd.SetParameterValue("@ADJUSTNM", dbParm.AdjustName);
            unitCmd.SetParameterValue("@SLAMOUNT", dbParm.SlAmount);
            unitCmd.SetParameterValue("@REMARK", dbParm.LiquidationRemark);

            unitCmd.SetParameterValue("@TOTALAMOUNT", (!"1".Equals(dbParm.LiquidationType)) ? dbParm.TotalAmount : "");
            unitCmd.SetParameterValue("@ORDERID", (!"1".Equals(dbParm.LiquidationType)) ? dbParm.OrderID : "");
            unitCmd.SetParameterValue("@CITYID", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["CITYID"].ToString() : "");
            unitCmd.SetParameterValue("@CITYNM", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["CITYNM"].ToString() : "");
            unitCmd.SetParameterValue("@HOTELID", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["HOTELID"].ToString() : "");
            unitCmd.SetParameterValue("@HOTELNM", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["HOTELNM"].ToString() : "");
            unitCmd.SetParameterValue("@GROUPID", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["GROUPID"].ToString() : "");
            unitCmd.SetParameterValue("@GROUPNM", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["GROUPNM"].ToString() : "");
            unitCmd.SetParameterValue("@ROOMID", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["ROOMID"].ToString() : "");
            unitCmd.SetParameterValue("@ROOMNM", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["ROOMNM"].ToString() : "");
            unitCmd.SetParameterValue("@ROOMNUM", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["ROOMNUM"].ToString() : "");
            unitCmd.SetParameterValue("@GUESTNM", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["GUESTNM"].ToString() : "");
            unitCmd.SetParameterValue("@INDATE", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["INDATE"].ToString() : "");
            unitCmd.SetParameterValue("@OUTDATE", (!"1".Equals(dbParm.LiquidationType)) ? dsOrderInfo.Tables[0].Rows[0]["OUTDATE"].ToString() : "");
            
            unitCmd.SetParameterValue("@ISNEXTMONTH", "0");
            unitCmd.SetParameterValue("@INTOMONTH", DBNull.Value);
            unitCmd.SetParameterValue("@NEXTTIMES", 0);
            unitCmd.SetParameterValue("@STATUS", "0");
            unitCmd.SetParameterValue("@CREATEUSER", liquidateEntity.LogMessages.Userid);
            liquidateEntity.Result = unitCmd.ExecuteNonQuery();
            return liquidateEntity;
        }

        public static LiquidateEntity ModifyLiquidateAdjust(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();

            DataCommand unitCmd = DataCommandManager.GetDataCommand("ModifyLiquidateAdjust");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            unitCmd.SetParameterValue("@LIQUIDATIONID", dbParm.HsLiquidateOrder["LIQUIDATIONID"].ToString().Trim());
            unitCmd.SetParameterValue("@HOTELID", dbParm.HsLiquidateOrder["HOTELID"].ToString().Trim());
            unitCmd.SetParameterValue("@HOTELNM", dbParm.HsLiquidateOrder["HOTELNM"].ToString().Trim());
            unitCmd.SetParameterValue("@ROOMID", dbParm.HsLiquidateOrder["ROOMID"].ToString().Trim());
            unitCmd.SetParameterValue("@ROOMNM", dbParm.HsLiquidateOrder["ROOMNM"].ToString().Trim());
            unitCmd.SetParameterValue("@ROOMNUM", dbParm.HsLiquidateOrder["ROOMNUM"].ToString().Trim());
            unitCmd.SetParameterValue("@GUESTNM", dbParm.HsLiquidateOrder["GUESTNM"].ToString().Trim());
            unitCmd.SetParameterValue("@INDATE", dbParm.HsLiquidateOrder["INDATE"].ToString().Trim());
            unitCmd.SetParameterValue("@OUTDATE", dbParm.HsLiquidateOrder["OUTDATE"].ToString().Trim());
            unitCmd.SetParameterValue("@TOTALAMOUNT", dbParm.HsLiquidateOrder["TOTALAMOUNT"].ToString().Trim());
            unitCmd.SetParameterValue("@SLAMOUNT", dbParm.HsLiquidateOrder["SLAMOUNT"].ToString().Trim());
            unitCmd.SetParameterValue("@REMARK", dbParm.HsLiquidateOrder["REMARK"].ToString().Trim());
            unitCmd.SetParameterValue("@OPEUSER", liquidateEntity.LogMessages.Userid);
            liquidateEntity.Result = unitCmd.ExecuteNonQuery();
            return liquidateEntity;
        }

        public static LiquidateEntity MoveNextMonthAdjust(LiquidateEntity liquidateEntity)
        {
            // 首先判断是否转移为0 是：是否转下月设置为1 转入月就是该张账单月+1月 次数为1
            // 首先判断是否转移为1 是：转入月就是该转入月+1月 次数为+1
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();
            DataSet dsResult = GetOrderLiquidateDetail(liquidateEntity);

            if (!"1".Equals(dsResult.Tables[0].Rows[0]["ISNEXTMONTH"].ToString().Trim()))
            {
                DateTime dt = DateTime.Parse(dsResult.Tables[0].Rows[0]["SLMONTH"].ToString().Trim()).AddMonths(1);
                dbParm.HsLiquidateOrder.Add("INTOMONTH", dt.ToString("yyyy-MM"));
                dbParm.HsLiquidateOrder.Add("NEXTTIMES", 1);
            }
            else
            {
                DateTime dt = DateTime.Parse(dsResult.Tables[0].Rows[0]["INTOMONTH"].ToString().Trim()).AddMonths(1);
                dbParm.HsLiquidateOrder.Add("INTOMONTH", dt.ToString("yyyy-MM"));
                dbParm.HsLiquidateOrder.Add("NEXTTIMES", (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["NEXTTIMES"].ToString().Trim()) ? 1 : int.Parse(dsResult.Tables[0].Rows[0]["NEXTTIMES"].ToString().Trim()) + 1));
            }

            // Check转入月未销账
            if (ChkIsOrderMonthApproved(dbParm.UnitID, dbParm.HsLiquidateOrder["INTOMONTH"].ToString()))
            {
                liquidateEntity.Result = 2;
                return liquidateEntity;
            }

            DataCommand unitCmd = DataCommandManager.GetDataCommand("MoveMonthAdjust");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            unitCmd.SetParameterValue("@LIQUIDATIONID", dbParm.HsLiquidateOrder["LIQUIDATIONID"].ToString().Trim());
            unitCmd.SetParameterValue("@ISNEXTMONTH", "1");
            unitCmd.SetParameterValue("@INTOMONTH", dbParm.HsLiquidateOrder["INTOMONTH"].ToString().Trim());
            unitCmd.SetParameterValue("@NEXTTIMES", dbParm.HsLiquidateOrder["NEXTTIMES"].ToString().Trim());
            unitCmd.SetParameterValue("@REMARK", dbParm.HsLiquidateOrder["REMARK"].ToString().Trim());
            unitCmd.SetParameterValue("@OPEUSER", liquidateEntity.LogMessages.Userid);
            liquidateEntity.Result = unitCmd.ExecuteNonQuery();
            return liquidateEntity;
        }

        public static LiquidateEntity MoveBackMonthAdjust(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();
            DataSet dsResult = GetOrderLiquidateDetail(liquidateEntity);
            DateTime dt = DateTime.Parse(dsResult.Tables[0].Rows[0]["INTOMONTH"].ToString().Trim()).AddMonths(-1);
            string strINTOMONTH = dt.ToString("yyyy-MM");

            // Check转入月未销账
            if (ChkIsOrderMonthApproved(dbParm.UnitID, strINTOMONTH))
            {
                liquidateEntity.Result = 2;
                return liquidateEntity;
            }

            // 转回当月需要判断月份是否小于账单月份。如果小于就不让再转回到当月前。
            // 首先判断转入月 - 1月 是否和该账单是同月，如果是：则是否转入更新为0 转入月和次数清空
            // 首先判断转入月 - 1月 与账单月不同：转入月就是该转入月-1月 次数为-1
            
            if (dsResult.Tables[0].Rows[0]["INTOMONTH"].ToString().Trim().Equals(strINTOMONTH))
            {
                dbParm.HsLiquidateOrder.Add("ISNEXTMONTH", "0");
                dbParm.HsLiquidateOrder.Add("INTOMONTH", "");
                dbParm.HsLiquidateOrder.Add("NEXTTIMES", 0);
            }
            else
            {
                dbParm.HsLiquidateOrder.Add("ISNEXTMONTH", "1");
                dbParm.HsLiquidateOrder.Add("INTOMONTH", strINTOMONTH);
                dbParm.HsLiquidateOrder.Add("NEXTTIMES", (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["NEXTTIMES"].ToString().Trim()) ? 1 : int.Parse(dsResult.Tables[0].Rows[0]["NEXTTIMES"].ToString().Trim()) - 1));
            }

            DataCommand unitCmd = DataCommandManager.GetDataCommand("MoveMonthAdjust");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            unitCmd.SetParameterValue("@LIQUIDATIONID", dbParm.HsLiquidateOrder["LIQUIDATIONID"].ToString().Trim());
            unitCmd.SetParameterValue("@ISNEXTMONTH", dbParm.HsLiquidateOrder["ISNEXTMONTH"].ToString().Trim());
            unitCmd.SetParameterValue("@INTOMONTH", dbParm.HsLiquidateOrder["INTOMONTH"].ToString().Trim());
            unitCmd.SetParameterValue("@NEXTTIMES", dbParm.HsLiquidateOrder["NEXTTIMES"].ToString().Trim());
            unitCmd.SetParameterValue("@REMARK", dbParm.HsLiquidateOrder["REMARK"].ToString().Trim());
            unitCmd.SetParameterValue("@OPEUSER", liquidateEntity.LogMessages.Userid);
            liquidateEntity.Result = unitCmd.ExecuteNonQuery();
            return liquidateEntity;
        }

        public static LiquidateEntity GetOrderLiquidate(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();
            OracleParameter[] parm ={
                                     new OracleParameter("OrderID",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.OrderID;
            liquidateEntity.QueryResult = DbManager.Query("SettleInfo", "t_lm_settlement_order_select", true, parm);
            return liquidateEntity;
        }

        public static DataSet GetOrderLiquidateDetail(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();
            DataCommand unitCmd = DataCommandManager.GetDataCommand("GetOrderLiquidateDetail");
            unitCmd.SetParameterValue("@SLID", dbParm.SLID);
            unitCmd.SetParameterValue("@LIQUIDATIONID", dbParm.HsLiquidateOrder["LIQUIDATIONID"].ToString().Trim());
            return unitCmd.ExecuteDataSet();
        }

        public static bool ChkIsOrderMonthApproved(string unitID, string slMonth)
        {
            DataCommand unitCmd = DataCommandManager.GetDataCommand("ChkIsOrderMonthApproved");
            unitCmd.SetParameterValue("@UNITID", unitID);
            unitCmd.SetParameterValue("@SLMONTH", slMonth);
            DataSet dsResult = unitCmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static LiquidateEntity AddLiquidateIssue(LiquidateEntity liquidateEntity)
        {
            LiquidateDBEntity dbParm = (liquidateEntity.LiquidateDBEntity.Count > 0) ? liquidateEntity.LiquidateDBEntity[0] : new LiquidateDBEntity();
            
            return liquidateEntity;
        }
    }
}
