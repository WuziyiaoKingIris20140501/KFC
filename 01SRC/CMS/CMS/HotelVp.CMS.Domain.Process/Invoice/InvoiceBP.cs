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
    public abstract class InvoiceBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.InvoiceBP  Method: ";

        public static InvoiceEntity InvoiceListSelect(InvoiceEntity invoiceEntity)
        {
            invoiceEntity.LogMessages.MsgType = MessageType.INFO;
            invoiceEntity.LogMessages.Content = _nameSpaceClass + "InvoiceListSelect";
            LoggerHelper.LogWriter(invoiceEntity.LogMessages);

            try
            {
                return InvoiceDA.InvoiceListSelect(invoiceEntity);
            }
            catch (Exception ex)
            {
                invoiceEntity.LogMessages.MsgType = MessageType.ERROR;
                invoiceEntity.LogMessages.Content = _nameSpaceClass + "InvoiceListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(invoiceEntity.LogMessages);
                throw ex;
            }
        }

        public static InvoiceEntity InvoiceListExcelSelect(InvoiceEntity invoiceEntity)
        {
            invoiceEntity.LogMessages.MsgType = MessageType.INFO;
            invoiceEntity.LogMessages.Content = _nameSpaceClass + "InvoiceListExcelSelect";
            LoggerHelper.LogWriter(invoiceEntity.LogMessages);

            try
            {
                return InvoiceDA.InvoiceListExcelSelect(invoiceEntity);
            }
            catch (Exception ex)
            {
                invoiceEntity.LogMessages.MsgType = MessageType.ERROR;
                invoiceEntity.LogMessages.Content = _nameSpaceClass + "InvoiceListExcelSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(invoiceEntity.LogMessages);
                throw ex;
            }
        }

        public static InvoiceEntity CommonTypeSelect(InvoiceEntity invoiceEntity)
        {
            invoiceEntity.LogMessages.MsgType = MessageType.INFO;
            invoiceEntity.LogMessages.Content = _nameSpaceClass + "CommonTypeSelect";
            LoggerHelper.LogWriter(invoiceEntity.LogMessages);

            try
            {
                return InvoiceDA.CommonTypeSelect(invoiceEntity);
            }
            catch (Exception ex)
            {
                invoiceEntity.LogMessages.MsgType = MessageType.ERROR;
                invoiceEntity.LogMessages.Content = _nameSpaceClass + "CommonTypeSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(invoiceEntity.LogMessages);
                throw ex;
            }
        }

        public static InvoiceEntity InvoiceDetailSelect(InvoiceEntity invoiceEntity)
        {
            invoiceEntity.LogMessages.MsgType = MessageType.INFO;
            invoiceEntity.LogMessages.Content = _nameSpaceClass + "InvoiceDetailSelect";
            LoggerHelper.LogWriter(invoiceEntity.LogMessages);

            try
            {
                return InvoiceDA.InvoiceDetailSelect(invoiceEntity);
            }
            catch (Exception ex)
            {
                invoiceEntity.LogMessages.MsgType = MessageType.ERROR;
                invoiceEntity.LogMessages.Content = _nameSpaceClass + "InvoiceDetailSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(invoiceEntity.LogMessages);
                throw ex;
            }
        }

        public static int InvoiceUpdate(InvoiceEntity invoiceEntity)
        {
            invoiceEntity.LogMessages.MsgType = MessageType.INFO;
            invoiceEntity.LogMessages.Content = _nameSpaceClass + "InvoiceUpdate";
            LoggerHelper.LogWriter(invoiceEntity.LogMessages);

            try
            {
                return InvoiceDA.InvoiceUpdate(invoiceEntity);
            }
            catch (Exception ex)
            {
                invoiceEntity.LogMessages.MsgType = MessageType.ERROR;
                invoiceEntity.LogMessages.Content = _nameSpaceClass + "InvoiceUpdate  Error: " + ex.Message;
                LoggerHelper.LogWriter(invoiceEntity.LogMessages);
                throw ex;
            }
        }
    }
}