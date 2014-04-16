using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class ELRelationEntity : BaseEntity
    {
        private List<ELRelationDBEntity> elrelationdbentity;
        public List<ELRelationDBEntity> ELRelationDBEntity
        {
            get { return elrelationdbentity; }
            set { elrelationdbentity = value; }
        }
    }

    public class ELRelationDBEntity
    {
        private string sysNo;
        public string SysNo
        {
            get { return sysNo; }
            set { sysNo = value; }
        }

        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        private string supType;
        public string SupType
        {
            get { return supType; }
            set { supType = value; }
        }

        private string inuse;
        public string InUse
        {
            get { return inuse; }
            set { inuse = value; }
        }

        private string osource;
        public string OSource
        {
            get { return osource; }
            set { osource = value; }
        }

        private string osuphid;
                public string OSuphid
        {
            get { return osuphid; }
            set { osuphid = value; }
        }

        private string osupid;
        public string OSupId
        {
            get { return osupid; }
            set { osupid = value; }
        }

        private string hotelnm;
        public string HOTELNM
        {
            get { return hotelnm; }
            set { hotelnm = value; }
        }

        private string hvpID;
        public string HVPID
        {
            get { return hvpID; }
            set { hvpID = value; }
        }

        private string orowid;
        public string ORowID
        {
            get { return orowid; }
            set { orowid = value; }
        }

        private string roomCD;
        public string RoomCD
        {
            get { return roomCD; }
            set { roomCD = value; }
        }

        private string hvpLP;
        public string HVPLP
        {
            get { return hvpLP; }
            set { hvpLP = value; }
        }

        private string hvpRp;
        public string HVPRP
        {
            get { return hvpRp; }
            set { hvpRp = value; }
        }

        private string sales;
        public string Sales
        {
            get { return sales; }
            set { sales = value; }
        }

        private string elongID;
        public string ELongID
        {
            get { return elongID; }
            set { elongID = value; }
        }

        private string platformID;
        public string PlatformID
        {
            get { return platformID; }
            set { platformID = value; }
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

        
        private string orderFrom;
        public string OrderFrom
        {
            get { return orderFrom; }
            set { orderFrom = value; }
        }

        private string orderTo;
        public string OrderTo
        {
            get { return orderTo; }
            set { orderTo = value; }
        }

        private string orderSucFrom;
        public string OrderSucFrom
        {
            get { return orderSucFrom; }
            set { orderSucFrom = value; }
        }

        private string orderSucTo;
        public string OrderSucTo
        {
            get { return orderSucTo; }
            set { orderSucTo = value; }
        }
  
        private string orderCount;
        public string OrderCount
        {
            get { return orderCount; }
            set { orderCount = value; }
        }

        private string orderSuccCount;
        public string OrderSuccCount
        {
            get { return orderSuccCount; }
            set { orderSuccCount = value; }
        }

        private string selectType;
        public string SelectType
        {
            get { return selectType; }
            set { selectType = value; }
        }

        private string pkey;
        public string Pkey
        {
            get { return pkey; }
            set { pkey = value; }
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

        private string loginSizeStart;
        public string LoginSizeStart
        {
            get { return loginSizeStart; }
            set { loginSizeStart = value; }
        }

        private string loginSizeEnd;
        public string LoginSizeEnd
        {
            get { return loginSizeEnd; }
            set { loginSizeEnd = value; }
        }

        private string loginMobile;
        public string LoginMobile
        {
            get { return loginMobile; }
            set { loginMobile = value; }
        }

        private string ticketType;
        public string TicketType
        {
            get { return ticketType; }
            set { ticketType = value; }
        }

        private string ticketData;
        public string TicketData
        {
            get { return ticketData; }
            set { ticketData = value; }
        }

        private string packageName;
        public string PackageName
        {
            get { return packageName; }
            set { packageName = value; }
        }

        private string amountFrom;
        public string AmountFrom
        {
            get { return amountFrom; }
            set { amountFrom = value; }
        }

        private string amountTo;
        public string AmountTo
        {
            get { return amountTo; }
            set { amountTo = value; }
        }

        private string pickfromDate;
        public string PickfromDate
        {
            get { return pickfromDate; }
            set { pickfromDate = value; }
        }

        private string picktoDate;
        public string PicktoDate
        {
            get { return picktoDate; }
            set { picktoDate = value; }
        }

        private string ticketTime;
        public string TicketTime
        {
            get { return ticketTime; }
            set { ticketTime = value; }
        }

        private string signKey;
        public string SignKey
        {
            get { return signKey; }
            set { signKey = value; }
        }
    }
}