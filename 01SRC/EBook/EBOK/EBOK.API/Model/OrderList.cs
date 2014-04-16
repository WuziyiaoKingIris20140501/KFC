using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.EBOK.Domain.API.Model
{
    public class OrderList
    {
        public string roomTypeCode { get; set; }
        public string createTime { get; set; }
        public string status { get; set; }
        public string bookRoomNum { get; set; }
        public string cityId { get; set; }
        public string inDate { get; set; }
        public string fogOrderNum { get; set; }
        public string bookTotalPrice { get; set; }
        public string memo1 { get; set; }
        public string outDate { get; set; }
        public string orderNum { get; set; }
        public string contactTel { get; set; }
        public string loginmobile { get; set; }
        public string hotelId { get; set; }
        public string bookRemark { get; set; }
        public string guestNames { get; set; }
        public string hotelName { get; set; }
        public string contactName { get; set; }
        public string bookPrice { get; set; }
        public string roomTypeName { get; set; }
        public string statusDesc { get; set; }
        public string breakfastNum { get; set; }
        public string isNetwork { get; set; }
        public string isReserve { get; set; }
        public string priceCode { get; set; }
        public string confirmUser { get; set; }
        public string approveUser { get; set; }
        public string bookStatusOther { get; set; }
        public string orderChannel { get; set; } // 订单来源
        public string bookRemarkOpe { get; set; }
    }
}
