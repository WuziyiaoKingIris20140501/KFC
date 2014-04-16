using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class HotelGroupEntity : BaseEntity
    {
        private List<HotelGroupDBEntity> hotelgroupdbEntity;
        public List<HotelGroupDBEntity> HotelGroupDBEntity
        {
            get { return hotelgroupdbEntity; }
            set { hotelgroupdbEntity = value; }
        }
    }

    public class HotelGroupDBEntity
    {
        private string hotelGroupId;
        public string HotelGroupID
        {
            get { return hotelGroupId; }
            set { hotelGroupId = value; }
        }

        private string hotelGroupCode;
        public string HotelGroupCode
        {
            get { return hotelGroupCode; }
            set { hotelGroupCode = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string name_CN;
        public string Name_CN
        {
            get { return name_CN; }
            set { name_CN = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string bandtype;
        public string BandType
        {
            get { return bandtype; }
            set { bandtype = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string onlineStatus;
        public string OnlineStatus
        {
            get { return onlineStatus; }
            set { onlineStatus = value; }
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
    }
}