using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class PromotionEntity : BaseEntity
    {
        private List<PromotionDBEntity> promotiondbentity;
        public List<PromotionDBEntity> PromotionDBEntity
        {
            get { return promotiondbentity; }
            set { promotiondbentity = value; }
        }
    }

    public class PromotionDBEntity
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

        private string priority;
        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string imageid;
        public string Imageid
        {
            get { return imageid; }
            set { imageid = value; }
        }

        private string imagepath;
        public string ImagePath
        {
            get { return imagepath; }
            set { imagepath = value; }
        }

        private string hotelID;
        public string HotelID
        {
            get { return hotelID; }
            set { hotelID = value; }
        }

        private string commonList;
        public string CommonList
        {
            get { return commonList; }
            set { commonList = value; }
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

        private string ratecode;
        public string RateCode
        {
            get { return ratecode; }
            set { ratecode = value; }
        }

        private string startBeginDTime;
        public string StartBeginDTime
        {
            get { return startBeginDTime; }
            set { startBeginDTime = value; }
        }

        private string startEndDTime;
        public string StartEndDTime
        {
            get { return startEndDTime; }
            set { startEndDTime = value; }
        }

        private string endBeginDTime;
        public string EndBeginDTime
        {
            get { return endBeginDTime; }
            set { endBeginDTime = value; }
        }

        private string endEndDTime;
        public string EndEndDTime
        {
            get { return endEndDTime; }
            set { endEndDTime = value; }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        private string chkType;
        public string ChkType
        {
            get { return chkType; }
            set { chkType = value; }
        }

        private string promethodid;
        public string Promethodid
        {
            get { return promethodid; }
            set { promethodid = value; }
        }

        private string linkurl;
        public string LinkUrl
        {
            get { return linkurl; }
            set { linkurl = value; }
        }
    }
}