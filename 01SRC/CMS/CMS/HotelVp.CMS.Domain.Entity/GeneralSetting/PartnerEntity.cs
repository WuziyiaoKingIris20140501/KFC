using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class PartnerEntity : BaseEntity
    {
        private List<PartnerDBEntity> partnerdbentity;
        public List<PartnerDBEntity> PartnerDBEntity
        {
            get { return partnerdbentity; }
            set { partnerdbentity = value; }
        }
    }

    public class PartnerDBEntity
    {
        private string sysID;
        public string SysID
        {
            get { return sysID; }
            set { sysID = value; }
        }

        private string wapLink;
         public string WapLink
        {
            get { return wapLink; }
            set { wapLink = value; }
        }

        private string partnerID;
        public string PartnerID
        {
            get { return partnerID; }
            set { partnerID = value; }
        }

        private string partnerLink;
        public string PartnerLink
        {
            get { return partnerLink; }
            set { partnerLink = value; }
        }

        private string partnerTitle;
        public string PartnerTitle
        {
            get { return partnerTitle; }
            set { partnerTitle = value; }
        }

        private string partnerCost;
        public string PartnerCost
        {
            get { return partnerCost; }
            set { partnerCost = value; }
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
    }
}