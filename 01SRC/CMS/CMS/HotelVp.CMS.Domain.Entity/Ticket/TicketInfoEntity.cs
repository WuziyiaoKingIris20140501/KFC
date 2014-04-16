using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class TicketInfoEntity : BaseEntity
    {
        private List<TicketInfoDBEntity>ticketinfodbentity;
        public List<TicketInfoDBEntity> TicketInfoDBEntity
        {
            get { return ticketinfodbentity; }
            set { ticketinfodbentity = value; }
        }
    }

    public class TicketInfoDBEntity
    {
        private string packagename;
        public string PackageName
        {
            get { return packagename; }
            set { packagename = value; }
        }

        private string amountfrom;
        public string AmountFrom
        {
            get { return amountfrom; }
            set { amountfrom = value; }
        }

        private string amountto;
        public string AmountTo
        {
            get { return amountto; }
            set { amountto = value; }
        }

        private string pickfromdate;
        public string Pickfromdate
        {
            get { return pickfromdate; }
            set { pickfromdate = value; }
        }

        private string picktodate;
        public string Picktodate
        {
            get { return picktodate; }
            set { picktodate = value; }
        }

        private string packagetype;
        public string PackageType
        {
            get { return packagetype; }
            set { packagetype = value; }
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

        private string tickettime;
        public string TicketTime
        {
            get { return tickettime; }
            set { tickettime = value; }
        }
    }
}