using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.ServiceAdapter;
using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class ReportCenterBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.LmSystemLogBP  Method: ";

        public static ReportCenterEntity GeneralDataReportSelect(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "GeneralDataReportSelect";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.GeneralDataReportSelect(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "GeneralDataReportSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity GeneralDataReportSelectList(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "GeneralDataReportSelectList";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.GeneralDataReportSelectList(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "GeneralDataReportSelectList  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity QunaerDataReportSelect(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "QunaerDataReportSelect";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.QunaerDataReportSelect(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "QunaerDataReportSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity CCCancelDataReportSelect(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "CCCancelDataReportSelect";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.CCCancelDataReportSelect(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "CCCancelDataReportSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity PodinnsDataReportSelect(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "PodinnsDataReportSelect";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.PodinnsDataReportSelect(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "PodinnsDataReportSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity DuplicateOrderDataReportSelect(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "DuplicateOrderDataReportSelect";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.DuplicateOrderDataReportSelect(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "DuplicateOrderDataReportSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity FRCancelDataReportSelect(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "FRCancelDataReportSelect";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.FRCancelDataReportSelect(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "FRCancelDataReportSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity CohortAnalysisReportSelect(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "CohortAnalysisReportSelect";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.CohortAnalysisReportSelect(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "CohortAnalysisReportSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity HotelSalesDataReportSelect(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "HotelSalesDataReportSelect";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.HotelSalesDataReportSelect(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "HotelSalesDataReportSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        public static ReportCenterEntity OrderSearchDataReport(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "OrderSearchDataReport";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.OrderSearchDataReport(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "OrderSearchDataReport  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }

        #region   订单操作报表
        public static ReportCenterEntity GetOracleOrderList(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "OrderSearchDataReport";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.GetOracleOrderList(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "OrderSearchDataReport  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }
        public static ReportCenterEntity GetSqlOrderList(ReportCenterEntity ReportCenterEntity)
        {
            ReportCenterEntity.LogMessages.MsgType = MessageType.INFO;
            ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "OrderSearchDataReport";
            LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);

            try
            {
                return ReportCenterDA.GetSqlOrderList(ReportCenterEntity);
            }
            catch (Exception ex)
            {
                ReportCenterEntity.LogMessages.MsgType = MessageType.ERROR;
                ReportCenterEntity.LogMessages.Content = _nameSpaceClass + "OrderSearchDataReport  Error: " + ex.Message;
                LoggerHelper.LogWriter(ReportCenterEntity.LogMessages);
                throw ex;
            }
        }
        #endregion
    }
}