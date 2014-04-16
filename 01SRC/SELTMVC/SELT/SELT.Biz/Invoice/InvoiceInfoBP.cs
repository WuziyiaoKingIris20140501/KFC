using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.SELT.Domain.Entity;
using HotelVp.SELT.Domain.DA;

namespace HotelVp.SELT.Domain.Biz
{
    public abstract class InvoiceInfoBP
    {
        /// <summary>
        /// 结算开票列表取得
        /// </summary>
        /// <param name="invoiceEntity"></param>
        /// <returns></returns>
        public static InvoiceEntity GetInvoiceList(InvoiceEntity invoiceEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return InvoiceDA.GetInvoiceList(invoiceEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 导出待开票项目
        /// </summary>
        /// <param name="invoiceEntity"></param>
        /// <returns></returns>
        public static InvoiceEntity ExportUnOpenInvoiceList(InvoiceEntity invoiceEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return InvoiceDA.ExportUnOpenInvoiceList(invoiceEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 导入已开票数据
        /// </summary>
        /// <param name="invoiceEntity"></param>
        /// <returns></returns>
        public static InvoiceEntity ImportOpenInvoiceList(InvoiceEntity invoiceEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return InvoiceDA.ImportOpenInvoiceList(invoiceEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 发票详情取得
        /// </summary>
        /// <param name="invoiceEntity"></param>
        /// <returns></returns>
        public static InvoiceEntity GetInvoiceDetail(InvoiceEntity invoiceEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return InvoiceDA.GetInvoiceDetail(invoiceEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 确认开票
        /// </summary>
        /// <param name="invoiceEntity"></param>
        /// <returns></returns>
        public static InvoiceEntity ConfirmInvoiceDetail(InvoiceEntity invoiceEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return InvoiceDA.ConfirmInvoiceDetail(invoiceEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 重新开票
        /// </summary>
        /// <param name="invoiceEntity"></param>
        /// <returns></returns>
        public static InvoiceEntity ReConfirmInvoiceDetail(InvoiceEntity invoiceEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return InvoiceDA.ReConfirmInvoiceDetail(invoiceEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }
    }
}
