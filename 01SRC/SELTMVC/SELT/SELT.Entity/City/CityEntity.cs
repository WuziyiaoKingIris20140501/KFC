using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.SELT.Domain.Entity
{
    public class CityEntity : BaseEntity
    {
        private List<CityDBEntity> citydbentity;
        public List<CityDBEntity> CityDBEntity
        {
            get { return citydbentity; }
            set { citydbentity = value; }
        }
    }

    public class CityDBEntity
    {
        private string cityNo;
        public string CityNo
        {
            get { return cityNo; }
            set { cityNo = value; }
        }

        private string cityID;
        public string CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        private string name_CN;
        public string Name_CN
        {
            get { return name_CN; }
            set { name_CN = value; }
        }

        private string name_EN;
        public string Name_EN
        {
            get { return name_EN; }
            set { name_EN = value; }
        }

        private string areaID;
        public string AreaID
        {
            get { return areaID; }
            set { areaID = value; }
        }

        private string onlineStatus;
        public string OnlineStatus
        {
            get { return onlineStatus; }
            set { onlineStatus = value; }
        }

        private string seQ;
        public string SEQ
        {
            get { return seQ; }
            set { seQ = value; }
        }

        private string pinyin;
        public string Pinyin
        {
            get { return pinyin; }
            set { pinyin = value; }
        }

        private string pinyinS;
        public string PinyinS
        {
            get { return pinyinS; }
            set { pinyinS = value; }
        }

        private string longitude;
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        private string latitude;
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string createUser;
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

        private string createDTime;
        public string CreateDTime
        {
            get { return createDTime; }
            set { createDTime = value; }
        }

        private string updateUser;
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        private string updateDTime;
        public string UpdateDTime
        {
            get { return updateDTime; }
            set { updateDTime = value; }
        }

        private string startDTime;
        public string StartDTime
        {
            get { return startDTime; }
            set { startDTime = value; }
        }

        private string endDTime;
        public string EndDTime
        {
            get { return endDTime; }
            set { endDTime = value; }
        }

        private string cityType;
        public string CityType
        {
            get { return cityType; }
            set { cityType = value; }
        }

        private string elCityID;
        public string ElCityID
        {
            get { return elCityID; }
            set { elCityID = value; }
        }

        private string isHot;
        public string IsHot
        {
            get { return isHot; }
            set { isHot = value; }
        }

        private string saleHour;
        public string SaleHour
        {
            get { return saleHour; }
            set { saleHour = value; }
        }
    }
}
