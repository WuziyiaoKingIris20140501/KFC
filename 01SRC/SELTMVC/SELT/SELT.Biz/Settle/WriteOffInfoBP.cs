using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.SELT.Domain.Entity;
using HotelVp.SELT.Domain.DA;

namespace HotelVp.SELT.Domain.Biz
{
    public abstract class WriteOffInfoBP
    {
        /// <summary>
        /// 待销账款项取得
        /// </summary>
        /// <param name="writeoffEntity"></param>
        /// <returns></returns>
        public static WriteOffEntity GetOutstandingBalanceList(WriteOffEntity writeoffEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return WriteOffDA.GetOutstandingBalanceList(writeoffEntity);
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
        /// 查询待销账目取得
        /// </summary>
        /// <param name="writeoffEntity"></param>
        /// <returns></returns>
        public static WriteOffEntity GetOutstandingBalanceUnitList(WriteOffEntity writeoffEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return WriteOffDA.GetOutstandingBalanceUnitList(writeoffEntity);
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
        /// 待结算-结算单位取得
        /// </summary>
        /// <param name="writeoffEntity"></param>
        /// <returns></returns>
        public static WriteOffEntity GetOutstandingSettleUnitList(WriteOffEntity writeoffEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return WriteOffDA.GetOutstandingSettleUnitList(writeoffEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        ///// <summary>
        ///// 确认收款
        ///// </summary>
        ///// <param name="writeoffEntity"></param>
        ///// <returns></returns>
        //public static WriteOffEntity ConfirmCollectHis(WriteOffEntity writeoffEntity)
        //{
        //    //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
        //    //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
        //    //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

        //    try
        //    {
        //        return WriteOffDA.ConfirmCollectHis(writeoffEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
        //        //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
        //        //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 发票历史取得
        /// </summary>
        /// <param name="writeoffEntity"></param>
        /// <returns></returns>
        public static WriteOffEntity GetInvoiceHisList(WriteOffEntity writeoffEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return WriteOffDA.GetInvoiceHisList(writeoffEntity);
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
        /// 增加结算调整项
        /// </summary>
        /// <param name="writeoffEntity"></param>
        /// <returns></returns>
        public static WriteOffEntity AddSettleAdjust(WriteOffEntity writeoffEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return WriteOffDA.AddSettleAdjust(writeoffEntity);
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
        /// 添加结算Issue单
        /// </summary>
        /// <param name="writeoffEntity"></param>
        /// <returns></returns>
        public static WriteOffEntity AddIssueSettle(WriteOffEntity writeoffEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return WriteOffDA.AddIssueSettle(writeoffEntity);
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
        /// 确认销账
        /// </summary>
        /// <param name="writeoffEntity"></param>
        /// <returns></returns>
        public static WriteOffEntity ConfirmSettleList(WriteOffEntity writeoffEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return WriteOffDA.ConfirmSettleList(writeoffEntity);
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
