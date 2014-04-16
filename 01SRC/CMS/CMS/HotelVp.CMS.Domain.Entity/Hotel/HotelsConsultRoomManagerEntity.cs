using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class HotelsConsultRoomManagerEntity : BaseEntity
    {
        private List<HotelsConsultRoomManagerDBEntity> hotelsConsultRoomManagerDBentity;
        public List<HotelsConsultRoomManagerDBEntity> HotelsConsultRoomManagerDBEntity
        {
            get { return hotelsConsultRoomManagerDBentity; }
            set { hotelsConsultRoomManagerDBentity = value; }
        }
    }

    public class HotelsConsultRoomManagerDBEntity
    {
        private string keyID;
        public string KeyID
        {
            get { return keyID; }
            set { keyID = value; }
        }

        private string hotelId;
        public string HotelId
        {
            get { return hotelId; }
            set { hotelId = value; }
        }

        private string hotelName;
        public string HotelName
        {
            get { return hotelName; }
            set { hotelName = value; }
        }

        private string cityID;
        public string CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        private string cityName;
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        private string checkUserName;
        public string CheckUserName
        {
            get { return checkUserName; }
            set { checkUserName = value; }
        }

        private DateTime checkTime;
        public DateTime CheckTime
        {
            get { return checkTime; }
            set { checkTime = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string lmCount;
        public string LMCount
        {
            get { return lmCount; }
            set { lmCount = value; }
        }

        private string sDate;
        public string SDate
        {
            get { return sDate; }
            set { sDate = value; }
        }

        private string eDate;
        public string EDate
        {
            get { return eDate; }
            set { eDate = value; }
        }


    }
}
