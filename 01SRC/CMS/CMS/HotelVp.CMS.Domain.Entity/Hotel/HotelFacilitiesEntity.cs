using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class HotelFacilitiesEntity : BaseEntity
    {
        private List<HotelFacilitiesDBEntity> hotelfacilitiesdbentity;
        public List<HotelFacilitiesDBEntity> HotelFacilitiesDBEntity
        {
            get { return hotelfacilitiesdbentity; }
            set { hotelfacilitiesdbentity = value; }
        }
    }

    public class HotelFacilitiesDBEntity
    {
        private string hotelId;
        public string HotelID
        {
            get { return hotelId; }
            set { hotelId = value; }
        }

        private ArrayList aryFalList;
        public ArrayList AryFalLisT
        {
            get { return aryFalList; }
            set { aryFalList = value; }
        }

        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
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

        private string fTID;
        public string FTID
        {
            get { return fTID; }
            set { fTID = value; }
        }

        private string fTCode;
        public string FTCode
        {
            get { return fTCode; }
            set { fTCode = value; }
        }

        private string fTName;
        public string FTName
        {
            get { return fTName; }
            set { fTName = value; }
        }

        private string fTSeq;
        public string FTSeq
        {
            get { return fTSeq; }
            set { fTSeq = value; }
        }

        private Hashtable fTSeqList;
        public Hashtable FTSeqList
        {
            get { return fTSeqList; }
            set { fTSeqList = value; }
        }
    }
}