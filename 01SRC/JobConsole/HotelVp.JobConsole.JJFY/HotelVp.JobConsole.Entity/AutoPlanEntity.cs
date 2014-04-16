using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJZX.JobConsole.Entity
{
    public class AutoPlanEntity : BaseEntity
    {
        private List<AutoHotelPlanDBEntity> autohotelplandbentity;
        public List<AutoHotelPlanDBEntity> AutoHotelPlanDBEntity
        {
            get { return autohotelplandbentity; }
            set { autohotelplandbentity = value; }
        }
    }

    public class AutoHotelPlanDBEntity
    {
        private string agentid;
        public string AgentId
        {
            get { return agentid; }
            set { agentid = value; }
        }

        private string mobilenum;
        public string MobileNum
        {
            get { return mobilenum; }
            set { mobilenum = value; }
        }

        private string money;
        public string Money
        {
            get { return money; }
            set { money = value; }
        }

        private string orderid;
        public string OrderId
        {
            get { return orderid; }
            set { orderid = value; }
        }
            
        private string name;
        public string NAME
        {
            get { return name; }
            set { name = value; }
        }
        
        private string unit_id;
        public string UNIT_ID
        {
            get { return unit_id; }
            set { unit_id = value; }
        }
            
        private string order_status;
        public string ORDER_STATUS
        {
            get { return order_status; }
            set { order_status = value; }
        }
            
        private string arrd;
        public string ARRD
        {
            get { return arrd; }
            set { arrd = value; }
        }
            
        private string depd;
        public string DEPD
        {
            get { return depd; }
            set { depd = value; }
        }
            
        private string channel;
        public string CHANNEL
        {
            get { return channel; }
            set { channel = value; }
        }

        private string platform_code;
        public string PLATFORM_CODE
        {
            get { return platform_code; }
            set { platform_code = value; }
        }

        private string guest_id;
        public string GUEST_ID
        {
            get { return guest_id; }
            set { guest_id = value; }
        }

        private string msg_id;
        public string MSG_ID
        {
            get { return msg_id; }
            set { msg_id = value; }
        }

        private string msg_res;
        public string MSG_RES
        {
            get { return msg_res; }
            set { msg_res = value; }
        }
    }
}