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
using HotelVp.CMS.Domain.ServiceAdapter;
using HotelVp.CMS.Domain.Entity.Order;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class OrderInfoBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.OrderInfoBP  Method: ";
        public static OrderInfoEntity BindOrderList(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderList";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.BindOrderList(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderList  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }


        public static OrderInfoEntity BindOrderConfirmList(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderConfirmList";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.BindOrderConfirmList(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderConfirmList  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity BindOrderConfirmManagerList(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderConfirmManagerList";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.BindOrderConfirmManagerList(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderConfirmManagerList  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity BindOrderApproveList(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderApproveList";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.BindOrderApproveList(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderApproveList  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity BindOrderApproveFaxList(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderApproveFaxList";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.BindOrderApproveFaxList(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderApproveFaxList  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity BindOrderApprovePrint(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderApprovePrint";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.BindOrderApprovePrint(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderApprovePrint  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity BindOrderApproveFaxPrint(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderApproveFaxPrint";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.BindOrderApproveFaxPrint(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "BindOrderApproveFaxPrint  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity UnLockOrderConfirm(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "UnLockOrderConfirm";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.UnLockOrderConfirm(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "UnLockOrderConfirm  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity UnLockHotelOrderConfirm(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "UnLockHotelOrderConfirm";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.UnLockHotelOrderConfirm(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "UnLockHotelOrderConfirm  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity CommonCitySelect(OrderInfoEntity orderInfoEntity)
        {
            try
            {
                return OrderInfoDA.CommonCitySelect(orderInfoEntity);
            }
            catch (Exception ex)
            {
                //orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                //orderInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonCitySelect  Error: " + ex.Message;
                throw ex;
            }
        }

        public static OrderInfoEntity OrderOperateStatus(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderOperateStatus";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.OrderOperateStatus(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderOperateStatus  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 查询最终订单状态（供应商）
        /// </summary>
        /// <returns></returns>
        public static OrderInfoEntity orderQueryByOrderNum(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "orderQueryByOrderNum";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoSA.orderQueryByOrderNum(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "orderQueryByOrderNum  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 更新订单最终状态接口
        /// </summary>
        /// <returns></returns>
        public static OrderInfoEntity updateIssueOrder(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "updateIssueOrder";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoSA.updateIssueOrder(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "updateIssueOrder  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity InsertOrderActionHisList(OrderInfoEntity orderInfoEntity, string Result)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "InsertOrderActionHisList";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.InsertOrderActionHisList(orderInfoEntity, Result);
            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "InsertOrderActionHisList  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity ChkOrderLock(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "chkOrderLock";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.ChkOrderLock(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "chkOrderLock  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        #region 订单审核 报表   临时方法居多，修改订单审核日志表之后清除
        public static OrderInfoEntity OrderApprovedReport(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderApprovedReport";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.OrderApprovedReport(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderApprovedReport  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity OrderApprovedByCount(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderApprovedByCount";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.OrderApprovedByCount(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderApprovedByCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity OrderApprovedByOrderDetails(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderApprovedByOrderDetails";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.OrderApprovedByOrderDetails(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderApprovedByOrderDetails  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }



        public static OrderInfoEntity GetApprovedOrderList(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetApprovedOrderList";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetApprovedOrderList(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetApprovedOrderList  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity GetApprovedOrderListByCheck(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetApprovedOrderListByCheck";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetApprovedOrderListByCheck(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetApprovedOrderListByCheck  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity GetOrderListNum(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderListNum";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetOrderListNum(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderListNum  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity GetApprovedUserListByCheck(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetApprovedUserListByCheck";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetApprovedUserListByCheck(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetApprovedUserListByCheck  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        #region   订单审核 临时数据   初审：初审订单  初审NS订单   复审：复审订单  复审离店单
        public static OrderInfoEntity GetFirstAppOrders(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetFirstAppOrders";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetFirstAppOrders(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetFirstAppOrders  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity GetFirstAppNSOrders(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetFirstAppNSOrders";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetFirstAppNSOrders(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetFirstAppNSOrders  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity GetCheckAppOrders(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetCheckAppOrders";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetCheckAppOrders(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetCheckAppOrders  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static OrderInfoEntity GetCheckAppNSOrders(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetCheckAppNSOrders";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetCheckAppNSOrders(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetCheckAppNSOrders  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// 订单详情页  离店状态 获取房间号
        /// </summary>
        /// <param name="orderInfoEntity"></param>
        /// <returns></returns>
        public static OrderInfoEntity GetRoomNumber(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetRoomNumber";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetRoomNumber(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "GetRoomNumber  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 退款接口
        /// </summary>
        /// <param name="OrderRefundEntity"></param>
        /// <returns></returns>
        public static OrderRefundEntity saveRefund(OrderRefundEntity OrderRefundEntity)
        {
            OrderRefundEntity.LogMessages.MsgType = MessageType.INFO;
            OrderRefundEntity.LogMessages.Content = _nameSpaceClass + "GetRoomNumber";
            LoggerHelper.LogWriter(OrderRefundEntity.LogMessages);

            try
            {
                return OrderInfoSA.saveRefund(OrderRefundEntity);

            }
            catch (Exception ex)
            {
                OrderRefundEntity.LogMessages.MsgType = MessageType.ERROR;
                OrderRefundEntity.LogMessages.Content = _nameSpaceClass + "GetRoomNumber  Error: " + ex.Message;
                LoggerHelper.LogWriter(OrderRefundEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 退款列表
        /// </summary>
        /// <param name="orderInfoEntity"></param>
        /// <returns></returns>
        public static OrderInfoEntity GetRefundOrderList(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.LogMessages.MsgType = MessageType.INFO;
            orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderOperateStatus";
            LoggerHelper.LogWriter(orderInfoEntity.LogMessages);

            try
            {
                return OrderInfoDA.GetRefundOrderList(orderInfoEntity);

            }
            catch (Exception ex)
            {
                orderInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                orderInfoEntity.LogMessages.Content = _nameSpaceClass + "OrderOperateStatus  Error: " + ex.Message;
                LoggerHelper.LogWriter(orderInfoEntity.LogMessages);
                throw ex;
            }
        }
    }
}
