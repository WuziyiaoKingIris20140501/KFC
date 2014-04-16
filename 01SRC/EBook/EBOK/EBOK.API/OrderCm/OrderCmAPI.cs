using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.EBOK.Domain.API.Model;

namespace HotelVp.EBOK.Domain.API
{
    public static class OrderCmAPI
    {
        // 今日待确认订单信息
        public static Response<List<OrderList>> GetIndexMainList(string mac, string hId, string beginDate, string endDate, string orderstatus)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"status\":\"{4}\",\"priceCode\":\"LMBAR2\",\"pageSize\":10000", mac, hId, beginDate, endDate, orderstatus);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "order/orderList.json", body);
                return JsonUtility.Deserialize < Response<List<OrderList>>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<List<OrderList>>() { timeOut = true };
            }
            catch(Exception ex)
            {
                return new Response<List<OrderList>>() { timeOut = true };
            }
        }

        public static Response<List<OrderList>> GetIndexHisMainList(string mac, string hId, string beginDate, string endDate, string beginSDate, string endSDate, string orderstatus)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"status\":\"{4}\",\"priceCode\":\"LMBAR2\",\"pageSize\":10000,\"updateBeginDate\":\"{5}\",\"updateEndDate\":\"{6}\"", mac, hId, beginDate, endDate, orderstatus, beginSDate, endSDate);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "order/orderList.json", body);
                return JsonUtility.Deserialize < Response<List<OrderList>>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<List<OrderList>>() { timeOut = true };
            }
            catch(Exception ex)
            {
                return new Response<List<OrderList>>() { timeOut = true };
            }
        }

        public static Response<List<OrderList>> GetOrderQueryList(string mac, string hId, string beginDate, string endDate, string cusid)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"guestNames\":\"{4}\",\"pageSize\":10000", mac, hId, beginDate, endDate, cusid);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "order/orderList.json", body);
                return JsonUtility.Deserialize < Response<List<OrderList>>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<List<OrderList>>() { timeOut = true };
            }
            catch(Exception ex)
            {
                return new Response<List<OrderList>>() { timeOut = true };
            }
        }

        public static Response<List<OrderList>> AjxGetOrderQueryPageList(string mac, string hId, string beginDate, string endDate, string cusid, int pageNum, int pageSize)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"guestNames\":\"{4}\",\"pageNum\":{5},\"pageSize\":{6}", mac, hId, beginDate, endDate, cusid, pageNum, pageSize);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "order/orderList.json", body);
                return JsonUtility.Deserialize < Response<List<OrderList>>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<List<OrderList>>() { timeOut = true };
            }
            catch(Exception ex)
            {
                return new Response<List<OrderList>>() { timeOut = true };
            }
        }

        public static int GetIndexOrderCount(string mac, string hId, string beginDate, string endDate, string orderstatus)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"priceCode\":\"LMBAR2\",\"status\":\"{4}\"", mac, hId, beginDate, endDate, orderstatus);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "order/orderCount.json", body);
                OrderInfo orderinfo = JsonUtility.Deserialize<Response<OrderInfo>>(r).result;
                return (String.IsNullOrEmpty(orderinfo.count) ? 0 : int.Parse(orderinfo.count));
            }
            catch (TimeoutException)
            {
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static int GetIndexHisOrderCount(string mac, string hId, string beginDate, string endDate, string beginSDate, string endSDate, string orderstatus)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"priceCode\":\"LMBAR2\",\"status\":\"{4}\",\"updateBeginDate\":\"{5}\",\"updateEndDate\":\"{6}\"", mac, hId, beginDate, endDate, orderstatus, beginSDate, endSDate);
                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "order/orderCount.json", body);
                OrderInfo orderinfo = JsonUtility.Deserialize<Response<OrderInfo>>(r).result;
                return (String.IsNullOrEmpty(orderinfo.count) ? 0 : int.Parse(orderinfo.count));
            }
            catch (TimeoutException)
            {
                return 0;
            }
            catch
            {
                return 0;
            }
        }


        public static Response<object> SaveConfirmOrderInfo(string orderid, string comfirmid, string remark, string actionType, string opeuserid, string mac, string hid)
        {
            try
            {
                var body = "";
                var url = "\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"fogOrderNum\":\"{2}\",\"status\":\"{3}\",\"operator\":\"{4}\",\"confirmNum\":\"{5}\"";
                if ("4".Equals(actionType)) // 确认可入住
                {
                    body = string.Format(url, mac, hid, orderid, actionType, opeuserid, comfirmid);
                }
                else if ("9".Equals(actionType)) // 满房取消
                {
                    url = "\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"fogOrderNum\":\"{2}\",\"status\":\"{3}\",\"operator\":\"{4}\",\"cancleReason\":\"CRC01\"";
                    body = string.Format(url, mac, hid, orderid, actionType, opeuserid);
                }
                else if ("8".Equals(actionType)) // 离店
                {
                    url = "\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"fogOrderNum\":\"{2}\",\"status\":\"{3}\",\"operator\":\"{4}\",\"inroomNum\":\"{5}\"";
                    body = string.Format(url, mac, hid, orderid, actionType, opeuserid, comfirmid);
                }
                else if ("5".Equals(actionType)) // No-Show
                {
                    url = "\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"fogOrderNum\":\"{2}\",\"status\":\"{3}\",\"operator\":\"{4}\"";
                    body = string.Format(url, mac, hid, orderid, actionType, opeuserid);
                }

                if (!String.IsNullOrEmpty(remark))
                {
                    body = body + ",\"bookRemark\":\"" + remark + "\"";
                }

                body = "{" + body + "}";
                var r = Request.CallWebByPost(Setting.Host + "order/operateOrderStatus.json", body);
                return JsonUtility.Deserialize<Response<object>>(r);
            }
            catch (TimeoutException)
            {
                return new Response<object>() { timeOut = true };
            }
            catch (Exception ex)
            {
                return new Response<object>() { timeOut = true };
            }
        }
    }
}
