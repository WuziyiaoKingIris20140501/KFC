using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity.GeneralSetting
{
    //public class TagInfoEntity : BaseEntity
    //{
    //    private List<TagInfoDBEntity> taginfodbentity;
    //    public List<TagInfoDBEntity> TagInfoDBEntity
    //    {
    //        get { return taginfodbentity; }
    //        set { taginfodbentity = value; }
    //    }
    //}

    public class TagInfoEntity : BaseEntity
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string tagName;
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
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

        private string typeID;
        public string TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
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

        private string pinyinLong;
        public string PinyinLong
        {
            get { return pinyinLong; }
            set { pinyinLong = value; }
        }

        private string pinyinShort;
        public string PinyinShort
        {
            get { return pinyinShort; }
            set { pinyinShort = value; }
        }
    }
}
