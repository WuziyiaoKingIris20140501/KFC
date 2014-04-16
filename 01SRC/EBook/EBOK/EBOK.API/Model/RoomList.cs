using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.EBOK.Domain.API.Model
{
    public class RoomList
    {
        public string roomTypeCode { get; set; }
        public string status { get; set; }
        public string eachBreakfastPrice { get; set; }
        public string effectDate { get; set; }
        public string isRoomful { get; set; }
        public string effectHour { get; set; }
        public string hotelId { get; set; }
        public string twoPrice { get; set; }
        public string rateCode { get; set; }
        public string creator { get; set; }
        public string modifier { get; set; }
        public string isReserve { get; set; }
        public string source { get; set; }
        public string breakfastNum { get; set; }
        public string roomNum { get; set; }
        public string isNetwork { get; set; }
        public string effectDateString { get; set; }
        public string thirdPrice { get; set; }
        public string roomTypeName { get; set; }
    }
}
