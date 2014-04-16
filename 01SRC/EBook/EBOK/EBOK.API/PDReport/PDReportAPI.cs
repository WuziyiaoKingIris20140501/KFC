using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.EBOK.Domain.API.Model;

namespace HotelVp.EBOK.Domain.API
{
    public static class PDReportAPI
    {
        public static Response<List<OrderList>> AjxGetPDReportQueryList(string mac, string hId, string beginDate, string endDate)
        {
            try
            {
                var body = string.Format("\"mac\":\"{0}\",\"hotelId\":\"{1}\",\"beginDate\":\"{2}\",\"endDate\":\"{3}\",\"pageSize\":1000000,\"priceCode\":\"LMBAR2\"", mac, hId, beginDate, endDate);
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
    }
}
