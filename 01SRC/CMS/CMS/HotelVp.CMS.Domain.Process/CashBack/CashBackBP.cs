using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.ServiceAdapter;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class CashBackBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.CashBack  Method: ";

        public static CashBackEntity BindOrderInfo(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "BindOrderInfo";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackDA.BindOrderInfo(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "BindOrderInfo  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }

        public static CashBackEntity BindOrderInfoByUser(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "BindOrderInfoByUser";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackDA.BindOrderInfoByUser(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "BindOrderInfoByUser  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }


        public static CashBackEntity SaveCashBackRequest(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "SaveCashBackRequest";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();

                if ("0".Equals(dbParm.BackInType))
                {
                    DataSet dsResult = BindOrderInfo(cashBackEntity).QueryResult;

                    if (!"8".Equals(dsResult.Tables[0].Rows[0]["FOG_AUDITSTATUS"].ToString()))
                    {
                        cashBackEntity.Result = 2;
                        cashBackEntity.ErrorMSG = "该订单状态为非离店，不能提交申请，请确认！";
                        return cashBackEntity;
                    }

                    if (!"2".Equals(dsResult.Tables[0].Rows[0]["CashTaskStatus"].ToString()))
                    {
                        cashBackEntity.Result = 2;
                        cashBackEntity.ErrorMSG = "该订单已提交申请，不能重复提交，请确认！";
                        return cashBackEntity;
                    }
                }

                if (!CashBackDA.ChkBackCashVal(cashBackEntity))
                {
                    cashBackEntity.Result = 2;
                    cashBackEntity.ErrorMSG = "申请提现金额不能大于用户可提现余额，请确认！";
                    return cashBackEntity;
                }

                cashBackEntity = CashBackSA.SaveCashBackRequest(cashBackEntity);
                if (cashBackEntity.Result == 1)
                {
                    CashBackDA.UpdateCashBackRequest(cashBackEntity);
                    //CashBackDA.UpdateCashBackHis(cashBackEntity);
                    CashBackDA.InsertCashBackHistory(cashBackEntity);
                }
                return cashBackEntity;
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "SaveCashBackRequest  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }










        public static CashBackEntity BindHotelList(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "BindHotelList";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackDA.BindHotelList(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "BindHotelList  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }
        
        public static int HotelSave(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "HotelSave";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackDA.HotelSave(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "HotelSave  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }

        public static CashBackEntity BindHotelImagesList(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "BindHotelImagesList";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackSA.BindHotelImagesList(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "BindHotelImagesList  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }

        public static int UpdateHotelSalesList(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelSalesList";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackDA.UpdateHotelSalesList(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelSalesList  Error: " + ex.Message;
                throw ex;
            }
        }

        public static CashBackEntity autoPay(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "autoPay";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackSA.autoPay(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "autoPay  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }

        public static CashBackEntity EditAlipayName(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "EditAlipayName";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackDA.EditAlipayName(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "EditAlipayName  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }


        public static CashBackEntity GetCashBackHistoryByEventHistory(CashBackEntity cashBackEntity)
        {
            cashBackEntity.LogMessages.MsgType = MessageType.INFO;
            cashBackEntity.LogMessages.Content = _nameSpaceClass + "GetCashBackHistoryByEventHistory";
            LoggerHelper.LogWriter(cashBackEntity.LogMessages);

            try
            {
                return CashBackDA.GetCashBackHistoryByEventHistory(cashBackEntity);
            }
            catch (Exception ex)
            {
                cashBackEntity.LogMessages.MsgType = MessageType.ERROR;
                cashBackEntity.LogMessages.Content = _nameSpaceClass + "GetCashBackHistoryByEventHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(cashBackEntity.LogMessages);
                throw ex;
            }
        }
    }
}