using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class DestinationEntity : BaseEntity
    {
        private List<DestinationDBEntity> destinationdbentity;
        public List<DestinationDBEntity> DestinationDBEntity
        {
            get { return destinationdbentity; }
            set { destinationdbentity = value; }
        }
    }

    public class DestinationDBEntity
    {
        private string destinationNo;
        public string DestinationNo
        {
            get { return destinationNo; }
            set { destinationNo = value; }
        }

        private string destinationID;
        public string DestinationID
        {
            get { return destinationID; }
            set { destinationID = value; }
        }

        private string parentsID;
        public string ParentsID
        {
            get { return parentsID; }
            set { parentsID = value; }
        }

        private string addRess;
        public string AddRess
        {
            get { return addRess; }
            set { addRess = value; }
        }

        private string cityID;
        public string CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        private string destinationTypeID;
        public string DestinationTypeID
        {
            get { return destinationTypeID; }
            set { destinationTypeID = value; }
        }

        private string telST;
        public string TelST
        {
            get { return telST; }
            set { telST = value; }
        }

        private string telLG;
        public string TelLG
        {
            get { return telLG; }
            set { telLG = value; }
        }

        private string latitude;
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        private string longitude;
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
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