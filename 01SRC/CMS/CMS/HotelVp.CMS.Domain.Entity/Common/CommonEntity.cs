using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class CommonEntity : BaseEntity
    {
        private List<CommonDBEntity> commondbentity;
        public List<CommonDBEntity> CommonDBEntity
        {
            get { return commondbentity; }
            set { commondbentity = value; }
        }
    }

    public class CommonDBEntity
    {
        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string ipAddress;
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
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

        private string eventType;
        public string Event_Type
        {
            get { return eventType; }
            set { eventType = value; }
        }

        private string eventID;
        public string Event_ID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        private string eventContent;
        public string Event_Content
        {
            get { return eventContent; }
            set { eventContent = value; }
        }

        private string eventResult;
        public string Event_Result
        {
            get { return eventResult; }
            set { eventResult = value; }
        }
    }
}