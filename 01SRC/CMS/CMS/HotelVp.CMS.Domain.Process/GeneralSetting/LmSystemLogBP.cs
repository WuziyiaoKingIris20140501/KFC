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
    public abstract class LmSystemLogBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.LmSystemLogBP  Method: ";

        public static LmSystemLogEntity PlatFormSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "PlatFormSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.PlatFormSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "PlatFormSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderChannelSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderChannelSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderChannelSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderChannelSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity SalesManagerSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SalesManagerSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.SalesManagerSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SalesManagerSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        //public static LmSystemLogEntity SalesManagerHotelListSelect(LmSystemLogEntity lmSystemLogEntity)
        //{
        //    lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
        //    lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SalesManagerHotelListSelect";
        //    LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

        //    try
        //    {
        //        return LmSystemLogDA.SalesManagerHotelListSelect(lmSystemLogEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
        //        lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SalesManagerHotelListSelect  Error: " + ex.Message;
        //        LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
        //        throw ex;
        //    }
        //}

        public static LmSystemLogEntity ReviewSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReviewSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity GetOrderInfoData(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetOrderInfoData";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.GetOrderInfoData(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetOrderInfoData  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewSelectByNew(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectByNew";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReviewSelectByNew(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectByNew  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ChartExportLmOrderSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ChartExportLmOrderSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ChartExportLmOrderSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ChartExportLmOrderSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ExportLmOrderSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ExportLmOrderSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ExportLmOrderSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ExportLmOrderSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ExportLmOrderSelectByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ExportLmOrderSelectByRests";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ExportLmOrderSelectByRests(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ExportLmOrderSelectByRests  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewLmOrderSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReviewLmOrderSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReviewLmOrderLogSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelectByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectByRests";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReviewLmOrderLogSelectByRests(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectByRests  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity LmOrderAutoRefreshSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "LmOrderAutoRefreshSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.LmOrderAutoRefreshSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "LmOrderAutoRefreshSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelectCount(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectCount";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReviewLmOrderLogSelectCount(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelectCountByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectCountByRests";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReviewLmOrderLogSelectCountByRests(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewLmOrderLogSelectCountByRests  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReviewSelectByNewCount(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectByNewCount";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReviewSelectByNewCount(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectByNewCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity UserDetailListSelectByNew(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListSelectByNew";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.UserDetailListSelectByNew(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListSelectByNew  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity UserGridViewPlanDetail(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UserGridViewPlanDetail";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.UserGridViewPlanDetail(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UserGridViewPlanDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity UserDetailListSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.UserDetailListSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UserDetailListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static int SaveOrderOpeRemark(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOperation";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                if (LmSystemLogDA.SaveOrderOpeRemark(lmSystemLogEntity) != 1)
                {
                    return 0;
                }

                if (!String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    if (OrderInfoSA.SaveOrderOperation(lmSystemLogEntity) != 1)
                    {
                        return 0;
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOperation  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity SendFaxService(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SendFaxService";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                DataSet dsResult = LmSystemLogDA.OrderOperationSelect(lmSystemLogEntity).QueryResult;
                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    string book_status_other = dsResult.Tables[0].Rows[0]["book_status_other"].ToString();
                    if ("3".Equals(book_status_other) || "9".Equals(book_status_other))
                    {
                        lmSystemLogEntity.SendFaxType = "2";
                    }
                }
                return OrderInfoSA.SendFaxService(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SendFaxService  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity SaveOrderOpeRemarkList(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOpeRemarkList";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                if (LmSystemLogDA.SaveOrderOpeRemarkList(lmSystemLogEntity) != 1)
                {
                    lmSystemLogEntity.Result = 0;
                    return lmSystemLogEntity;
                }

                if ("1".Equals(lmSystemLogEntity.IsDbApprove))
                {
                    lmSystemLogEntity.ErrorMSG = OrderInfoSA.SaveOrderOperationDoubleApprove(lmSystemLogEntity);
                    lmSystemLogEntity.Result = (String.IsNullOrEmpty(lmSystemLogEntity.ErrorMSG)) ? 1 : 2;
                    return lmSystemLogEntity;
                }
                else
                {
                    if (!String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                    {
                        string strMsg = OrderInfoSA.SaveOrderOperationList(lmSystemLogEntity);
                        if (!String.IsNullOrEmpty(strMsg))
                        {
                            lmSystemLogEntity.Result = 2;
                            lmSystemLogEntity.ErrorMSG = strMsg;
                            return lmSystemLogEntity;
                        }
                    }
                }
                lmSystemLogEntity.Result = 1;
                return lmSystemLogEntity;
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOpeRemarkList  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static int SaveOrderOperation(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOperation";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                if (!LmSystemLogDA.ChkOrderOperationLock(lmSystemLogEntity))
                {
                    return 2;
                }

                DataSet dsOrderInfo = LmSystemLogDA.GetSaveOrderOpeRemarkList(lmSystemLogEntity.FogOrderID);
                //if (LmSystemLogDA.SaveOrderOpeRemarkList(lmSystemLogEntity) != 1)
                //{
                //    return 0;
                //}

                if (LmSystemLogDA.SaveOrderOpeRemarkListConfirm(lmSystemLogEntity, dsOrderInfo) != 1)
                {
                    return 0;
                }

                if (OrderInfoSA.SaveOrderOperation(lmSystemLogEntity) != 1)
                {
                    return 0;
                }

                OrderInfoSA.SaveOrderConfirmPlanMail(lmSystemLogEntity, dsOrderInfo);
                return 1;
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOperation  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static string SaveOrderListInterFace(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOperation";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                if (!LmSystemLogDA.ChkOrderOperationLock(lmSystemLogEntity))
                {
                    return "订单：" + lmSystemLogEntity.FogOrderID + " 被其他人锁定。请稍后重试！";
                }

                DataSet dsOrderInfo = LmSystemLogDA.GetSaveOrderOpeRemarkList(lmSystemLogEntity.FogOrderID);
                if (LmSystemLogDA.SaveOrderOpeRemarkListConfirm(lmSystemLogEntity, dsOrderInfo) != 1)
                {
                    return "订单：" + lmSystemLogEntity.FogOrderID + " 信息不存在。请确认！";
                }

                //string strMsg = OrderInfoSA.SaveOrderOperationList(lmSystemLogEntity);
                string strMsg = OrderInfoSA.SynOrderOperationList(lmSystemLogEntity);
                if (!String.IsNullOrEmpty(strMsg))
                {
                    return strMsg;// "订单：" + lmSystemLogEntity.FogOrderID + " " + strMsg;
                }

                OrderInfoSA.SaveOrderConfirmPlanMail(lmSystemLogEntity, dsOrderInfo);
                return "";
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOperation  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static int SaveHotelExRemark(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveHotelExRemark";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                if (!LmSystemLogDA.ChkHotelExRemark(lmSystemLogEntity))
                {
                    return 2;
                }

                return LmSystemLogDA.SaveHotelExRemark(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveHotelExRemark  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static int SaveOrderOperationManager(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOperation";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                if (LmSystemLogDA.SaveOrderOperationManager(lmSystemLogEntity) != 1)
                {
                    return 0;
                }

                if (OrderInfoSA.SaveOrderOperation(lmSystemLogEntity) != 1)
                {
                    return 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderOperation  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderOperationSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderOperationSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderComfirmManagerDetail(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderComfirmManagerDetail";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderComfirmManagerDetail(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderComfirmManagerDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderComfirmManagerDetailRestVal(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderComfirmManagerDetailRestVal";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderComfirmManagerDetailRestVal(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderComfirmManagerDetailRestVal  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ChkOrderOperationSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ChkOrderOperationSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ChkOrderOperationSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ChkOrderOperationSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ChkOrderOperationControl(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ChkOrderOperationControl";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ChkOrderOperationControl(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ChkOrderOperationControl  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ChkOrderConfirmControl(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ChkOrderConfirmControl";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ChkOrderConfirmControl(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ChkOrderConfirmControl  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity UnlockOrderConfirmControl(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UnlockOrderConfirmControl";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.UnlockOrderConfirmControl(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UnlockOrderConfirmControl  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderOperationDetailSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationDetailSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderOperationDetailSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationDetailSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity UserMainListSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UserMainListSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.UserMainListSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "UserMainListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity HotelInfoSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "HotelInfoSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.HotelInfoSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "HotelInfoSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity GetEventLMOrderID(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetEventLMOrderID";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.GetEventLMOrderID(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetEventLMOrderID  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity SendMsgToLMSales(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SendMsgToLMSales";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderInfoSA.SendMsgToLMSales(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SendMsgToLMSales  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderOperationMemoSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationMemoSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderInfoDA.OrderOperationMemoSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationMemoSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderActionHis(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderActionHis";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderInfoDA.OrderActionHis(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderActionHis  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity GetOrderActionHisList(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetOrderActionHisList";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetOrderActionHisList(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetOrderActionHisList  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderOperationSalesSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationSalesSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderInfoDA.OrderOperationSalesSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationSalesSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        #region   Add  区分现付预付总计数据
        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogSelectCount(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelectCount";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_XPayment_ReviewLmOrderLogSelectCount(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogSelectCountForSales(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelectCountForSales";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_XPayment_ReviewLmOrderLogSelectCountForSales(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelectCountForSales  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogExport(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_XPayment_ReviewLmOrderLogExport(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogSelectForSales(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelectForSales";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_XPayment_ReviewLmOrderLogSelectForSales(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelectForSales  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }
        #endregion

        #region   Add  BAR/BARB当晚入住
        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogSelectCount(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelectCount";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_InTheNight_ReviewLmOrderLogSelectCount(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogSelectCountForSales(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelectCountForSales";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_InTheNight_ReviewLmOrderLogSelectCountForSales(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelectCountForSales  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogExport(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_InTheNight_ReviewLmOrderLogExport(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_XPayment_ReviewLmOrderLogSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_XPayment_ReviewLmOrderLogSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_InTheNight_ReviewLmOrderLogSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }


        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogSelectForSales(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelectForSales";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.order_InTheNight_ReviewLmOrderLogSelectForSales(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "order_InTheNight_ReviewLmOrderLogSelectForSales  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }
        #endregion

        public static LmSystemLogEntity OrderOperationSelectByPrint(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationSelectByPrint";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderOperationSelectByPrint(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderOperationSelectByPrint  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderComfirmByPrint(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderComfirmByPrint";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderComfirmByPrint(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderComfirmByPrint  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderComfirmPlanByPrint(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderComfirmPlanByPrint";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderComfirmPlanByPrint(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderComfirmPlanByPrint  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderSurveyDetail(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderSurveyDetail";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderSurveyDetail(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderSurveyDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity OrderSurveyAnalysis(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderSurveyAnalysis";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderSurveyAnalysis(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderSurveyAnalysis  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity GetOperateHis(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetOperateHis";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.GetOperateHis(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetOperateHis  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }


        public static LmSystemLogEntity OrderFaxHis(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderFaxHis";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.OrderFaxHis(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "OrderFaxHis  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity HotelFaxHis(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "HotelFaxHis";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.HotelFaxHis(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "HotelFaxHis  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity HotelComparisonSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "HotelComparisonSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.HotelComparisonSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "HotelComparisonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        //public static LmSystemLogEntity HotelComparisonSelectCount(LmSystemLogEntity lmSystemLogEntity)
        //{
        //    lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
        //    lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "HotelComparisonSelectCount";
        //    LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

        //    try
        //    {
        //        return LmSystemLogDA.HotelComparisonSelectCount(lmSystemLogEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
        //        lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "HotelComparisonSelectCount  Error: " + ex.Message;
        //        LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
        //        throw ex;
        //    }
        //}

        public static LmSystemLogEntity ExportHotelComparisonSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ExportHotelComparisonSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ExportHotelComparisonSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ExportHotelComparisonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReciveFaxSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReciveFaxSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReciveFaxSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReciveFaxSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReciveBindFaxSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReciveBindFaxSelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReciveBindFaxSelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReciveBindFaxSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity ReciveBindFaxVerifySelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReciveBindFaxVerifySelect";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.ReciveBindFaxVerifySelect(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "ReciveBindFaxVerifySelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static int SaveReciveFax(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveReciveFax";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.SaveReciveFax(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveReciveFax  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity SaveBindReciveFax(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveBindReciveFax";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                if ("0".Equals(lmSystemLogEntity.SendFaxType))
                {
                    DataSet dsBarCode = LmSystemLogDA.CheckBarCode(lmSystemLogEntity);
                    if (dsBarCode.Tables.Count == 0 || dsBarCode.Tables[0].Rows.Count == 0)
                    {
                        lmSystemLogEntity.Result = 2;
                        lmSystemLogEntity.ErrorMSG = "接收传真重新绑定保存失败 - 该二维码编号不存在，请修改！";
                        return lmSystemLogEntity;
                    }
                }
                return OrderInfoSA.SaveBindReciveFax(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveBindReciveFax  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity GetHCorderList(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetHCorderList";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetHCorderList(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetHCorderList  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity GetHCorderViewList(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetHCorderViewList";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetHCorderViewList(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "GetHCorderViewList  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static LmSystemLogEntity SaveConfirmOrderList(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveConfirmOrderList";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                if (!LmSystemLogDA.ChkOrderOperationLock(lmSystemLogEntity))
                {
                    lmSystemLogEntity.Result = 2;
                    lmSystemLogEntity.ErrorMSG = "保存失败。订单:" + lmSystemLogEntity.FogOrderID + "已被其他人锁定，请稍后再试！";
                    return lmSystemLogEntity;
                }

                string[] ODList = lmSystemLogEntity.OrderList.TrimEnd(',').Split(',');
                string[] ODDetail;
                string strOdStatus = string.Empty;
                LmSystemLogEntity lmEntity = new LmSystemLogEntity();
                lmEntity.LogMessages = new LogMessage();
                lmEntity.LogMessages = lmSystemLogEntity.LogMessages;
                string strResult = string.Empty;
                string allMSG = string.Empty;
                int iCount = ODList.Length;
                foreach (string tempOD in ODList)
                {
                    strResult = "";
                    ODDetail = tempOD.Split('_');
                    lmEntity.FogOrderID = ODDetail[0].Trim();
                    if (!LmSystemLogDA.ChkOrderOperationLock(lmEntity))
                    {
                        lmSystemLogEntity.ErrorMSG += lmEntity.FogOrderID + ",";
                        iCount = iCount - 1;
                        continue;
                    }

                    if (!"2".Equals(ODDetail[1].Trim()))
                    {
                        lmEntity.OrderBookStatus = ("1".Equals(ODDetail[1].Trim())) ? "6" : "7";//CC确认  CC取消
                        lmEntity.OrderBookStatusOther = ("1".Equals(ODDetail[1].Trim())) ? "4" : "9";
                        lmEntity.CanelReson = ("0".Equals(ODDetail[1].Trim())) ? ODDetail[2].Trim() : "";
                        lmEntity.BookRemark = ODDetail[3].Trim();
                        lmEntity.FollowUp = ("true".Equals(ODDetail[4].Trim())) ? "1" : "0";
                        lmEntity.ActionID = ("1".Equals(ODDetail[1].Trim())) ? ODDetail[2].Trim() : "";
                        strResult = LmSystemLogBP.SaveOrderListInterFace(lmEntity);
                        allMSG += strResult;
                        iCount = (String.IsNullOrEmpty(strResult)) ? iCount : iCount - 1;
                    }

                    if (String.IsNullOrEmpty(strResult.Trim()))
                    {
                        if ("1".Equals(ODDetail[1].Trim()))
                        {
                            strOdStatus = "可入住";
                        }
                        else if ("0".Equals(ODDetail[1].Trim()))
                        {
                            strOdStatus = "CC取消";
                        }
                        else if ("2".Equals(ODDetail[1].Trim()))
                        {
                            strOdStatus = "备注";
                        }

                        OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
                        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                        _orderInfoEntity.LogMessages = lmSystemLogEntity.LogMessages;
                        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
                        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
                        orderinfoEntity.EventType = "订单确认";
                        orderinfoEntity.ORDER_NUM = ODDetail[0].Trim();
                        orderinfoEntity.OdStatus = strOdStatus;
                        orderinfoEntity.REMARK = (!"2".Equals(ODDetail[1].Trim())) ? ODDetail[3].Trim() : ODDetail[2].Trim();
                        orderinfoEntity.ActionID = ("1".Equals(ODDetail[1].Trim())) ? ODDetail[2].Trim() : "";
                        orderinfoEntity.CANNEL = ("0".Equals(ODDetail[1].Trim())) ? ODDetail[2].Trim() : "";
                        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
                        CommonBP.InsertOrderActionHistoryList(_orderInfoEntity);
                    }
                }

                if (!String.IsNullOrEmpty(lmSystemLogEntity.ErrorMSG) || !String.IsNullOrEmpty(allMSG.Trim()))
                {
                    lmSystemLogEntity.Result = 3;
                    lmSystemLogEntity.ErrorMSG = ((iCount > 0) ? "其他订单保存成功。" : "保存失败。") + allMSG.Trim() + ((!String.IsNullOrEmpty(lmSystemLogEntity.ErrorMSG)) ? ("订单：" + lmSystemLogEntity.ErrorMSG.TrimEnd(',') + " 被其他人锁定，请稍后再试！") : "");
                }
                else
                {
                    lmSystemLogEntity.Result = 1;
                }
                return lmSystemLogEntity;
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "SaveConfirmOrderList  Error: " + ex.Message + lmSystemLogEntity.OrderList;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeleteFax(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.LogMessages.MsgType = MessageType.INFO;
            lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "DeleteFax";
            LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);

            try
            {
                return LmSystemLogDA.DeleteFax(lmSystemLogEntity);
            }
            catch (Exception ex)
            {
                lmSystemLogEntity.LogMessages.MsgType = MessageType.ERROR;
                lmSystemLogEntity.LogMessages.Content = _nameSpaceClass + "DeleteFax  Error: " + ex.Message;
                LoggerHelper.LogWriter(lmSystemLogEntity.LogMessages);
                throw ex;
            }
        }
    }
}
