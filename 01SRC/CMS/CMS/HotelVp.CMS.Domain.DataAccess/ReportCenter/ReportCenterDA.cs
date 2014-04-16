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
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class ReportCenterDA
    {
        public static ReportCenterEntity GeneralDataReportSelect(ReportCenterEntity reportCenterEntity)
        {
            //OracleParameter[] parm ={
            //                        new OracleParameter("ID",OracleType.VarChar),
            //                        new OracleParameter("StartDTime",OracleType.VarChar),
            //                        new OracleParameter("EndDTime",OracleType.VarChar)
            //                    };

            //if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            //{
            //    parm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[0].Value = lmSystemLogEntity.EventID;
            //}

            //if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            //{
            //    parm[1].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[1].Value = lmSystemLogEntity.StartDTime;
            //}

            //if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            //{
            //    parm[2].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[2].Value = lmSystemLogEntity.EndDTime;
            //}

            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "GeneralDataReport", true);

            return reportCenterEntity;
        }

        public static ReportCenterEntity GeneralDataReportSelectList(ReportCenterEntity reportCenterEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(reportCenterEntity.StartDTime))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = reportCenterEntity.StartDTime;
            }

            if (String.IsNullOrEmpty(reportCenterEntity.EndDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = reportCenterEntity.EndDTime;
            }

            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "GeneralDataReportSelectList", true, parm);

            return reportCenterEntity;
        }

        public static ReportCenterEntity QunaerDataReportSelect(ReportCenterEntity reportCenterEntity)
        {
            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "QunaerDataReport", true);

            return reportCenterEntity;
        }

        public static ReportCenterEntity CCCancelDataReportSelect(ReportCenterEntity reportCenterEntity)
        {
            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "CCCancelDataReport", true);

            return reportCenterEntity;
        }

        public static ReportCenterEntity PodinnsDataReportSelect(ReportCenterEntity reportCenterEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(reportCenterEntity.StartDTime))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = reportCenterEntity.StartDTime;
            }

            if (String.IsNullOrEmpty(reportCenterEntity.EndDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = reportCenterEntity.EndDTime;
            }

            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "PodinnsDataReport", true, parm);

            return reportCenterEntity;
        }

        public static ReportCenterEntity DuplicateOrderDataReportSelect(ReportCenterEntity reportCenterEntity)
        {
            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "DuplicateOrderDataReport", true);

            return reportCenterEntity;
        }

        public static ReportCenterEntity FRCancelDataReportSelect(ReportCenterEntity reportCenterEntity)
        {
            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "FRCancelDataReport", true);

            return reportCenterEntity;
        }

        public static ReportCenterEntity CohortAnalysisReportSelect(ReportCenterEntity reportCenterEntity)
        {
            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "CohortAnalysisReport", true);

            return reportCenterEntity;
        }

        public static ReportCenterEntity HotelSalesDataReportSelect(ReportCenterEntity reportCenterEntity)
        {
            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "HotelSalesDataReport", true);

            return reportCenterEntity;
        }

        public static ReportCenterEntity OrderSearchDataReport(ReportCenterEntity reportCenterEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CREATEDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(reportCenterEntity.StartDTime))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = reportCenterEntity.StartDTime;
            }

            if (String.IsNullOrEmpty(reportCenterEntity.EndDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = reportCenterEntity.EndDTime;
            }

            if (String.IsNullOrEmpty(reportCenterEntity.CityID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = reportCenterEntity.CityID;
            }

            if (String.IsNullOrEmpty(reportCenterEntity.Sales))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = reportCenterEntity.Sales;
            }

            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "OrderSearchDataReport", true, parm);

            return reportCenterEntity;
        }

        #region   订单操作报表
        public static ReportCenterEntity GetOracleOrderList(ReportCenterEntity reportCenterEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(reportCenterEntity.StartDTime))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = reportCenterEntity.StartDTime;
            }

            if (String.IsNullOrEmpty(reportCenterEntity.EndDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = reportCenterEntity.EndDTime;
            }

            if (String.IsNullOrEmpty(reportCenterEntity.PriceCode))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = reportCenterEntity.PriceCode;
            }

            reportCenterEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("ReportCenter", "OrderManageSearchDataReport", true, parm);

            return reportCenterEntity;
        }

        public static ReportCenterEntity GetSqlOrderList(ReportCenterEntity reportCenterEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetOrderManageSearchDataHistory");
            cmd.SetParameterValue("@EVENTUSER", reportCenterEntity.Sales);
            cmd.SetParameterValue("@STARTDATE", reportCenterEntity.StartDTime);
            cmd.SetParameterValue("@ENDDATE", reportCenterEntity.EndDTime);
             
            reportCenterEntity.QueryResult = cmd.ExecuteDataSet();
            return reportCenterEntity;
        }

        #endregion
    }
}