using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class PaymentBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.PaymentBP  Method: ";

        public static PaymentEntity CommonSelect(PaymentEntity paymentEntity)
        {
            paymentEntity.LogMessages.MsgType = MessageType.INFO;
            paymentEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(paymentEntity.LogMessages);

            try
            {
                return PaymentDA.CommonSelect(paymentEntity);
            }
            catch (Exception ex)
            {
                paymentEntity.LogMessages.MsgType = MessageType.ERROR;
                paymentEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(paymentEntity.LogMessages);
                throw ex;
            }
        }

        public static PaymentEntity Select(PaymentEntity paymentEntity)
        {
            paymentEntity.LogMessages.MsgType = MessageType.INFO;
            paymentEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(paymentEntity.LogMessages);

            try
            {
                return PaymentDA.Select(paymentEntity);
            }
            catch (Exception ex)
            {
                paymentEntity.LogMessages.MsgType = MessageType.ERROR;
                paymentEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(paymentEntity.LogMessages);
                throw ex;
            }
        }

        public static PaymentEntity PlatFormSelect(PaymentEntity paymentEntity)
        {
            paymentEntity.LogMessages.MsgType = MessageType.INFO;
            paymentEntity.LogMessages.Content = _nameSpaceClass + "PlatFormSelect";
            LoggerHelper.LogWriter(paymentEntity.LogMessages);

            try
            {
                return PaymentDA.PlatFormSelect(paymentEntity);
            }
            catch (Exception ex)
            {
                paymentEntity.LogMessages.MsgType = MessageType.ERROR;
                paymentEntity.LogMessages.Content = _nameSpaceClass + "PlatFormSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(paymentEntity.LogMessages);
                throw ex;
            }
        }

        public static int Insert(PaymentEntity paymentEntity)
        {
            paymentEntity.LogMessages.MsgType = MessageType.INFO;
            paymentEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(paymentEntity.LogMessages);

            try
            {
                return PaymentDA.Insert(paymentEntity);
            }
            catch (Exception ex)
            {
                paymentEntity.LogMessages.MsgType = MessageType.ERROR;
                paymentEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(paymentEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(PaymentEntity paymentEntity)
        {
            paymentEntity.LogMessages.MsgType = MessageType.INFO;
            paymentEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(paymentEntity.LogMessages);

            try
            {
                return PaymentDA.Update(paymentEntity);
            }
            catch (Exception ex)
            {
                paymentEntity.LogMessages.MsgType = MessageType.ERROR;
                paymentEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(paymentEntity.LogMessages);
                throw ex;
            }
        }
    }
}