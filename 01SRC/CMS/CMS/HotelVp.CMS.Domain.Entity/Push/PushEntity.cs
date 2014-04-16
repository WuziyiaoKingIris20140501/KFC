using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class PushEntity : BaseEntity
    {
        private List<PushDBEntity> pushdbentity;
        public List<PushDBEntity> PushDBEntity
        {
            get { return pushdbentity; }
            set { pushdbentity = value; }
        }
    }

    public class PushDBEntity
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
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

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string pushtype;
        public string PushType
        {
            get { return pushtype; }
            set { pushtype = value; }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string filepath;
        public string FilePath
        {
            get { return filepath; }
            set { filepath = value; }
        }

        private string ticketpackage;
        public string TicketPackage
        {
            get { return ticketpackage; }
            set { ticketpackage = value; }
        }

        private string ticketamount;
        public string TicketAmount
        {
            get { return ticketamount; }
            set { ticketamount = value; }
        }

        private string userGroupList;
        public string UserGroupList
        {
            get { return userGroupList; }
            set { userGroupList = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        private string wapUrl;
        public string WapUrl
        {
            get { return wapUrl; }
            set { wapUrl = value; }
        }

        private string allCount;
        public string AllCount
        {
            get { return allCount; }
            set { allCount = value; }
        }

        private string pushProtoType;
        public string PushProtoType
        {
            get { return pushProtoType; }
            set { pushProtoType = value; }
        }

        private string telPhone;
        public string TelPhone
        {
            get { return telPhone; }
            set { telPhone = value; }
        }
    }
}