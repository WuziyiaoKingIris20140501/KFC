using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity.Hotel
{
    public class HotelBlackEntity : BaseEntity
    {
        private List<HotelBlackDBEntity> hotelblackdbentity;
        public List<HotelBlackDBEntity> HotelBlackDBEntity
        {
            get { return hotelblackdbentity; }
            set { hotelblackdbentity = value; }
        }
    }

    public class HotelBlackDBEntity
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string hotelid;
        public string HotelId
        {
            get { return hotelid; }
            set { hotelid = value; }
        }
        private string isblack;
        public string IsBlack
        {
            get { return isblack; }
            set { isblack = value; }
        }
        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        private string createtime;
        public string CreateTime
        {
            get { return createtime; }
            set { createtime = value; }
        }
        private string updatetime;
        public string UpdateTime
        {
            get { return updatetime; }
            set { updatetime = value; }
        }
        private string createuser;
        public string CreateUser
        {
            get { return createuser; }
            set { createuser = value; }
        }
        private string updateuser;
        public string UpdateUser
        {
            get { return updateuser; }
            set { updateuser = value; }
        }

    }
}
