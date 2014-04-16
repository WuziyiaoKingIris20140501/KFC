using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class HotelInfoEXEntity : BaseEntity
    {
        private List<HotelInfoEXDBEntity> hotelinfoexdbentity;
        public List<HotelInfoEXDBEntity> HotelInfoEXDBEntity
        {
            get { return hotelinfoexdbentity; }
            set { hotelinfoexdbentity = value; }
        }
    }

    public class HotelInfoEXDBEntity
    {
        private string hotelID;
        public string HotelID
        {
            get { return hotelID; }
            set { hotelID = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string linkMan;
        public string LinkMan
        {
            get { return linkMan; }
            set { linkMan = value; }
        }

        private string linkTel;
        public string LinkTel
        {
            get { return linkTel; }
            set { linkTel = value; }
        }

        private string linkMail;
        public string LinkMail
        {
            get { return linkMail; }
            set { linkMail = value; }
        }

        private string linkFax;
        public string LinkFax
        {
            get { return linkFax; }
            set { linkFax = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string exTime;
        public string ExTime
        {
            get { return exTime; }
            set { exTime = value; }
        }

        private string exMode;
        public string ExMode
        {
            get { return exMode; }
            set { exMode = value; }
        }

        private string createTime;
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string updateTime;
        public string UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        private string createUser;
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

        private string updateUser;
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string opeResult;
        public string OpeResult
        {
            get { return opeResult; }
            set { opeResult = value; }
        }
    }
}
