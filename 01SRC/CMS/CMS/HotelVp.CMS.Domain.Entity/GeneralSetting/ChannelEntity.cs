using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class ChannelEntity : BaseEntity
    {
        private List<ChannelDBEntity> channeldbentity;
        public List<ChannelDBEntity> ChannelDBEntity
        {
            get { return channeldbentity; }
            set { channeldbentity = value; }
        }
    }

    public class ChannelDBEntity
    {
        private string channelNo;
        public string ChannelNo
        {
            get { return channelNo; }
            set { channelNo = value; }
        }

        private string channelID;
        public string ChannelID
        {
            get { return channelID; }
            set { channelID = value; }
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