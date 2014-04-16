using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class PaymentEntity : BaseEntity
    {
        private List<PaymentDBEntity> paymentdbentity;
        public List<PaymentDBEntity> PaymentDBEntity
        {
            get { return paymentdbentity; }
            set { paymentdbentity = value; }
        }
    }

    public class PaymentDBEntity
    {
        private string paymentNo;
        public string PaymentNo
        {
            get { return paymentNo; }
            set { paymentNo = value; }
        }

        private string paymentID;
        public string PaymentID
        {
            get { return paymentID; }
            set { paymentID = value; }
        }

        private string payformID;
        public string PaymFormID
        {
            get { return payformID; }
            set { payformID = value; }
        }

        private string payForm_name_CN;
        public string PayFormName_CN
        {
            get { return payForm_name_CN; }
            set { payForm_name_CN = value; }
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

        private string updatType;
        public string UpdatType
        {
            get { return updatType; }
            set { updatType = value; }
        }
    }
}