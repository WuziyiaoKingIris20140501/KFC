using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.SELT.Domain.Entity;
using HotelVp.SELT.Domain.DA;
using HotelVp.SELT.Domain.API;

namespace HotelVp.SELT.Domain.Biz
{
    public abstract class LiquidateInfoBP
    {
        public static LiquidateEntity CommonSelect(LiquidateEntity liquidateEntity)
        {
            //cityEntity.LogMessages.MsgType = MessageType.INFO;
            //cityEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(cityEntity.LogMessages);

            try
            {
                return LiquidateDA.CommonSelect(liquidateEntity);
            }
            catch (Exception ex)
            {
                //cityEntity.LogMessages.MsgType = MessageType.ERROR;
                //cityEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(cityEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 确认清算列表取得
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity GetLiquidateList(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
            try
            {
                return LiquidateDA.GetLiquidateList(liquidateEntity);
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
        /// 确认清算明细取得
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity GetLiquidateDetail(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                // 结算单位取得
                // 本月订单列表
                // 历史遗漏订单列表
                // 历史遗漏款项列表
                return LiquidateDA.GetLiquidateDetail(liquidateEntity);
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
        /// 保存清算报表
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity SaveLiquidate(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateDA.SaveLiquidate(liquidateEntity);
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
        /// 确认清算报表
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity ApproveLiquidate(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateDA.ApproveLiquidate(liquidateEntity);
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
        /// 增加清算调整项
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity AddLiquidateAdjust(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateDA.AddLiquidateAdjust(liquidateEntity);
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
        /// 修改清算项
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity ModifyLiquidateAdjust(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateDA.ModifyLiquidateAdjust(liquidateEntity);
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
        /// 转下月清算项
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity MoveNextMonthAdjust(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateDA.MoveNextMonthAdjust(liquidateEntity);
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
        /// 回当月清算项
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity MoveBackMonthAdjust(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateDA.MoveBackMonthAdjust(liquidateEntity);
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
        /// 打印清算
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity PrintLiquidateList(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateSA.PrintLiquidateList(liquidateEntity);
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
        /// 传真清算
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity FaxLiquidateList(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateSA.FaxLiquidateList(liquidateEntity);
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
        /// 导出Excel
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity ExportLiquidateList(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateDA.ExportLiquidateList(liquidateEntity);
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
        /// 调整订单Issue单
        /// </summary>
        /// <param name="liquidateEntity"></param>
        /// <returns></returns>
        public static LiquidateEntity AddLiquidateIssue(LiquidateEntity liquidateEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return LiquidateDA.AddLiquidateIssue(liquidateEntity);
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