using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class UserGroupEntity : BaseEntity
    {
        private List<UserGroupDBEntity> usergroupdbentity;
        public List<UserGroupDBEntity> UserGroupDBEntity
        {
            get { return usergroupdbentity; }
            set { usergroupdbentity = value; }
        }
    }

    public class UserGroupDBEntity
    {
        private string userGroupNo;
        public string UserGroupNo
        {
            get { return userGroupNo; }
            set { userGroupNo = value; }
        }

        private string userGroupID;
        public string UserGroupID
        {
            get { return userGroupID; }
            set { userGroupID = value; }
        }

        private string userGroupNM;
        public string UserGroupNM
        {
            get { return userGroupNM; }
            set { userGroupNM = value; }
        }

        private string regChannelList;
        public string RegChannelList
        {
            get { return regChannelList; }
            set { regChannelList = value; }
        }


        private string platformList;
        public string PlatformList
        {
            get { return platformList; }
            set { platformList = value; }
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

        private string loginStart;
        public string LoginStart
        {
            get { return loginStart; }
            set { loginStart = value; }
        }

        private string loginEnd;
        public string LoginEnd
        {
            get { return loginEnd; }
            set { loginEnd = value; }
        }

        private string submitOrderFrom;
        public string SubmitOrderFrom
        {
            get { return submitOrderFrom; }
            set { submitOrderFrom = value; }
        }

        private string submitOrderTo;
        public string SubmitOrderTo
        {
            get { return submitOrderTo; }
            set { submitOrderTo = value; }
        }

        private string completeOrderFrom;
        public string CompleteOrderFrom
        {
            get { return completeOrderFrom; }
            set { completeOrderFrom = value; }
        }

        private string completeOrderTo;
        public string CompleteOrderTo
        {
            get { return completeOrderTo; }
            set { completeOrderTo = value; }
        }

        private string lastOrderStart;
        public string LastOrderStart
        {
            get { return lastOrderStart; }
            set { lastOrderStart = value; }
        }

        private string lastOrderEnd;
        public string LastOrderEnd
        {
            get { return lastOrderEnd; }
            set { lastOrderEnd = value; }
        }

        private string manualAdd;
        public string ManualAdd
        {
            get { return manualAdd; }
            set { manualAdd = value; }
        }

        private ArrayList complManualAdd;
        public ArrayList ComplManualAdd
        {
            get { return complManualAdd; }
            set { complManualAdd = value; }
        }

        private string errManualAdd;
        public string ErrManualAdd
        {
            get { return errManualAdd; }
            set { errManualAdd = value; }
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

        private string startCount;
        public string StartCount
        {
            get { return startCount; }
            set { startCount = value; }
        }

        private string endCount;
        public string EndCount
        {
            get { return endCount; }
            set { endCount = value; }
        }

    }
}