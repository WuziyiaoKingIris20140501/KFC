using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class CashBackEntity : BaseEntity
    {
        private List<CashBackDBEntity> cashbackdbentity;
        public List<CashBackDBEntity> CashBackDBEntity
        {
            get { return cashbackdbentity; }
            set { cashbackdbentity = value; }
        }
    }

    public class CashBackDBEntity
    {
        private string fogOrderNo;
        public string OrderNo
        {
            get { return fogOrderNo; }
            set { fogOrderNo = value; }
        }

        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private string backCashAmount;
        public string BackCashAmount
        {
            get { return backCashAmount; }
            set { backCashAmount = value; }
        }

        private string backCashType;
        public string BackCashType
        {
            get { return backCashType; }
            set { backCashType = value; }
        }

        private string bankOwner;
        public string BankOwner
        {
            get { return bankOwner; }
            set { bankOwner = value; }
        }

        private string bankName;
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        private string bankBranch;
        public string BankBranch
        {
            get { return bankBranch; }
            set { bankBranch = value; }
        }

        private string bankCardNumber;
        public string BankCardNumber
        {
            get { return bankCardNumber; }
            set { bankCardNumber = value; }
        }

        private string backTel;
        public string BackTel
        {
            get { return backTel; }
            set { backTel = value; }
        }

        private string backBao;
        public string BackBao
        {
            get { return backBao; }
            set { backBao = value; }
        }

        private string backInType;
        public string BackInType
        {
            get { return backInType; }
            set { backInType = value; }
        }


        private string backBaoName;
        public string BackBaoName
        {
            get { return backBaoName; }
            set { backBaoName = value; }
        }





        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string hotelId;
        public string HotelID
        {
            get { return hotelId; }
            set { hotelId = value; }
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

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string fogStatus;
         public string FogStatus
        {
            get { return fogStatus; }
            set { fogStatus = value; }
        }

        private string hotelGroup;
        public string HotelGroup
        {
            get { return hotelGroup; }
            set { hotelGroup = value; }
        }

        private string starRating;
        public string StarRating
        {
            get { return starRating; }
            set { starRating = value; }
        }

        private string diamondRating;
        public string DiamondRating
        {
            get { return diamondRating; }
            set { diamondRating = value; }
        }

        private string provincial;
        public string Provincial
        {
            get { return provincial; }
            set { provincial = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string openDate;
        public string OpenDate
        {
            get { return openDate; }
            set { openDate = value; }
        }

        private string repairDate;
        public string RepairDate
        {
            get { return repairDate; }
            set { repairDate = value; }
        }

        private string addRess;
        public string AddRess
        {
            get { return addRess; }
            set { addRess = value; }
        }

        private string webSite;
        public string WebSite
        {
            get { return webSite; }
            set { webSite = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string fax;
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        private string contactPer;
        public string ContactPer
        {
            get { return contactPer; }
            set { contactPer = value; }
        }

        private string contactEmail;
        public string ContactEmail
        {
            get { return contactEmail; }
            set { contactEmail = value; }
        }

        private string simpleDescZh;
        public string SimpleDescZh
        {
            get { return simpleDescZh; }
            set { simpleDescZh = value; }
        }

        private string descZh;
        public string DescZh
        {
            get { return descZh; }
            set { descZh = value; }
        }

        private string evaluation;
        public string Evaluation
        {
            get { return evaluation; }
            set { evaluation = value; }
        }

        private string autotrust;
        public string AutoTrust
        {
            get { return autotrust; }
            set { autotrust = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
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

        private string longitude;
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        private string latitude;
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        private ArrayList hotelImage;
        public ArrayList HotelImage
        {
            get { return hotelImage; }
            set { hotelImage = value; }
        }

        private string salesID;
        public string SalesID
        {
            get { return salesID; }
            set { salesID = value; }
        }

        private string salesStartDT;
        public string SalesStartDT
        {
            get { return salesStartDT; }
            set { salesStartDT = value; }
        }

        private string salesEndDT;
        public string SalesEndDT
        {
            get { return salesEndDT; }
            set { salesEndDT = value; }
        }

        private string priceCode;
        public string PriceCode
        {
            get { return priceCode; }
            set { priceCode = value; }
        }

        private string roomList;
        public string HRoomList
        {
            get { return roomList; }
            set { roomList = value; }
        }

        private string inDateFrom;
        public string InDateFrom
        {
            get { return inDateFrom; }
            set { inDateFrom = value; }
        }

        private string inDateTo;
        public string InDateTo
        {
            get { return inDateTo; }
            set { inDateTo = value; }
        }

        private string balType;
        public string BalType
        {
            get { return balType; }
            set { balType = value; }
        }

        private string balValue;
        public string BalValue
        {
            get { return balValue; }
            set { balValue = value; }
        }

        private string loginMobile;
        public string LoginMobile
        {
            get { return loginMobile; }
            set { loginMobile = value; }
        }

        private string sn;
        public string Sn
        {
            get { return sn; }
            set { sn = value; }
        }

        private string alipayAccount;
        public string AlipayAccount
        {
            get { return alipayAccount; }
            set { alipayAccount = value; }
        }

        private string alipayName;
        public string AlipayName
        {
            get { return alipayName; }
            set { alipayName = value; }
        }
    }
}