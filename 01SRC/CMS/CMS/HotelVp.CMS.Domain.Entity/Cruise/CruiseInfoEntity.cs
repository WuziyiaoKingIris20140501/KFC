using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HotelVp.CMS.Domain.Entity
{
    public class CruiseInfoEntity : BaseEntity
    {
        private List<CruiseInfoDBEntity> cruiseinfodbEntity;
        public List<CruiseInfoDBEntity> CruiseInfoDBEntity
        {
            get { return cruiseinfodbEntity; }
            set { cruiseinfodbEntity = value; }
        }

    }

    public class CruiseInfoDBEntity
    {
        private string cruiseid;
        public string CruiseID
        {
            get { return cruiseid; }
            set { cruiseid = value; }
        }

        private string shipnm;
        public string ShipNM
        {
            get { return shipnm; }
            set { shipnm = value; }
        }
        private string destination;
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        private string days;
        public string Days
        {
            get { return days; }
            set { days = value; }
        }
        private string port;
        public string Port
        {
            get { return port; }
            set { port = value; }
        }



        private string planid;
        public string PlanID
        {
            get { return planid; }
            set { planid = value; }
        }
        private string plandtime;
        public string PlanDTime
        {
            get { return plandtime; }
            set { plandtime = value; }
        }
        private string plannumer;
        public string PlanNumer
        {
            get { return plannumer; }
            set { plannumer = value; }
        }
        private string oplannumer;
        public string OPlanNumer
        {
            get { return oplannumer; }
            set { oplannumer = value; }
        }


        private string action;
        public string Action
        {
            get { return action; }
            set { action = value; }
        }

        private string boatid;
        public string BoatID
        {
            get { return boatid; }
            set { boatid = value; }
        }

        private string createstart;
        public string CreateStart
        {
            get { return createstart; }
            set { createstart = value; }
        }    
    }
}