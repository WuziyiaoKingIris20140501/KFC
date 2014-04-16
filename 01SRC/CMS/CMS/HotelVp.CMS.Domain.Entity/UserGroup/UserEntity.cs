using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class UserEntity : BaseEntity
    {
        private List<UserDBEntity> userdbentity;
        public List<UserDBEntity> UserDBEntity
        {
            get { return userdbentity; }
            set { userdbentity = value; }
        }
    }

    public class UserDBEntity
    {
        private string userNo;
        public string UserNo
        {
            get { return userNo; }
            set { userNo = value; }
        }

        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private string regChannelID;
        public string RegChannelID
        {
            get { return regChannelID; }
            set { regChannelID = value; }
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

        private string rType;
        public string RType
        {
            get { return rType; }
            set { rType = value; }
        }

        private string alDelHT;
        public string ALDelHT
        {
            get { return alDelHT; }
            set { alDelHT = value; }
        }

        private string hotelID;
        public string HotelID
        {
            get { return hotelID; }
            set { hotelID = value; }
        }

        private string cityID;
        public string CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        private string tagID;
        public string TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }

        private string salesID;
        public string SalesID
        {
            get { return salesID; }
            set { salesID = value; }
        }
    }
}