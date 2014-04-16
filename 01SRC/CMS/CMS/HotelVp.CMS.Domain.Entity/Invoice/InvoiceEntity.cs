using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class InvoiceEntity : BaseEntity
    {
        private List<InvoiceDBEntity> invoicedbentity;
        public List<InvoiceDBEntity> InvoiceDBEntity
        {
            get { return invoicedbentity; }
            set { invoicedbentity = value; }
        }
    }

    public class InvoiceDBEntity
    {
        private string actionType;
        public string ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }

        private string invoiceID;
        public string InvoiceID
        {
            get { return invoiceID; }
            set { invoiceID = value; }
        }

        private string cnfnum;
        public string CNFNUM
        {
            get { return cnfnum; }
            set { cnfnum = value; }
        }

        private string userid;
        public string USERID
        {
            get { return userid; }
            set { userid = value; }
        }

        private string contracttel;
        public string CONTRACTTEL
        {
            get { return contracttel; }
            set { contracttel = value; }
        }

        private string send_name;
        public string SENDNAME
        {
            get { return send_name; }
            set { send_name = value; }
        }

        private string sendcode;
        public string SENDCODE
        {
            get { return sendcode; }
            set { sendcode = value; }
        }

        private string applytime;
        public string APPLYTIME
        {
            get { return applytime; }
            set { applytime = value; }
        }

        private string applychanel;
        public string APPLYCHANEL
        {
            get { return applychanel; }
            set { applychanel = value; }
        }

        private string receivername;
        public string RECEIVERNAME
        {
            get { return receivername; }
            set { receivername = value; }
        }

        private string invoiceaddr;
        public string INVOICEADDR
        {
            get { return invoiceaddr; }
            set { invoiceaddr = value; }
        }

        private string sendtime;
        public string SENDTIME
        {
            get { return sendtime; }
            set { sendtime = value; }
        }

        private string zipcode;
        public string ZIPCODE
        {
            get { return zipcode; }
            set { zipcode = value; }
        }

        private string invoicehead;
        public string INVOICEHEAD
        {
            get { return invoicehead; }
            set { invoicehead = value; }
        }

        private string operatornm;
        public string OPERATOR
        {
            get { return operatornm; }
            set { operatornm = value; }
        }

        private string invoicenum;
        public string INVOICENUM
        {
            get { return invoicenum; }
            set { invoicenum = value; }
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


        private string applybeginDate;
        public string APPLYBEGINDATE
        {
            get { return applybeginDate; }
            set { applybeginDate = value; }
        }
        private string applyendDate;
        public string APPLYENDDATE
        {
            get { return applyendDate; }
            set { applyendDate = value; }
        }

        private string sendbeginDate;
        public string SENDBEGINDATE
        {
            get { return sendbeginDate; }
            set { sendbeginDate = value; }
        }
        private string sendendDate;
        public string SENDENDDATE
        {
            get { return sendendDate; }
            set { sendendDate = value; }
        }

        private string selectType;
        public string SelectType
        {
            get { return selectType; }
            set { selectType = value; }
        }
    }
}