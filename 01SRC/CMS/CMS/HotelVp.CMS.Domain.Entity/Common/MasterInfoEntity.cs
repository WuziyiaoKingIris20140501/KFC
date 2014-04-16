using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class MasterInfoEntity : BaseEntity
    {
        private List<MasterInfoDBEntity> masterInfodbentity;
        public List<MasterInfoDBEntity> MasterInfoDBEntity
        {
            get { return masterInfodbentity; }
            set { masterInfodbentity = value; }
        }
    }

    public class MasterInfoDBEntity
    {
        private string userNo;
        public string UserNo
        {
            get { return userNo; }
            set { userNo = value; }
        }

        private string today;
        public string Today
        {
            get { return today; }
            set { today = value; }
        }

        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private string regChannelID;
        public string RegChannelID
        {
            get { return regChannelID; }
            set { regChannelID = value; }
        }

        private string platformID;
        public string PlatformID
        {
            get { return platformID; }
            set { platformID = value; }
        }

        private string registStart;
        public string RegistStart
        {
            get { return registStart; }
            set { registStart = value; }
        }

        private string registEnd;
        public string RegistEnd
        {
            get { return registEnd; }
            set { registEnd = value; }
        }

        private string orderCount;
        public string OrderCount
        {
            get { return orderCount; }
            set { orderCount = value; }
        }

        private string orderSuccCount;
        public string OrderSuccCount
        {
            get { return orderSuccCount; }
            set { orderSuccCount = value; }
        }
    }
}