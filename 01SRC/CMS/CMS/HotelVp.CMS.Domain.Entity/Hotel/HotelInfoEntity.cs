using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HotelVp.CMS.Domain.Entity
{
    public class HotelInfoEntity : BaseEntity
    {
        private List<HotelInfoDBEntity> hotelinfodbentity;
        public List<HotelInfoDBEntity> HotelInfoDBEntity
        {
            get { return hotelinfodbentity; }
            set { hotelinfodbentity = value; }
        }

        private int lmCount;
        public int LMCount
        {
            get { return lmCount; }
            set { lmCount = value; }
        }

        private int lm2Count;
        public int LM2Count
        {
            get { return lm2Count; }
            set { lm2Count = value; }
        }

        private string cols;
        public string Cols
        {
            get { return cols; }
            set { cols = value; }
        }
    }

    public class HotelInfoDBEntity
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string roomNMCG;
        public string RoomNMCG
        {
            get { return roomNMCG; }
            set { roomNMCG = value; }
        }

        private string bedCode;
        public string BedCode
        {
            get { return bedCode; }
            set { bedCode = value; }
        }

        private string bedName;
        public string BedName
        {
            get { return bedName; }
            set { bedName = value; }
        }

        private string bedTag;
        public string BedTag
        {
            get { return bedTag; }
            set { bedTag = value; }
        }

        private string zip;
        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        private string keyWord;
        public string KeyWord
        {
            get { return keyWord; }
            set { keyWord = value; }
        }

        private string hotelId;
        public string HotelID
        {
            get { return hotelId; }
            set { hotelId = value; }
        }

        private string areaID;
        public string AreaID
        {
            get { return areaID; }
            set { areaID = value; }
        }

        private string hotelPN;
        public string HotelPN
        {
            get { return hotelPN; }
            set { hotelPN = value; }
        }

        private string priceLow;
        public string PriceLow
        {
            get { return priceLow; }
            set { priceLow = value; }
        }

        private string hotelJP;
        public string HotelJP
        {
            get { return hotelJP; }
            set { hotelJP = value; }
        }

        private string totalRooms;
        public string TotalRooms
        {
            get { return totalRooms; }
            set { totalRooms = value; }
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

        private string contactPhone;
        public string ContactPhone
        {
            get { return contactPhone; }
            set { contactPhone = value; }
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

        private string hotelRemark;
        public string HotelRemark
        {
            get { return hotelRemark; }
            set { hotelRemark = value; }
        }

        private string keywords;
        public string KeyWords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        private string hServe;
        public string HServe
        {
            get { return hServe; }
            set { hServe = value; }
        }

        private string hFacility;
        public string HFacility
        {
            get { return hFacility; }
            set { hFacility = value; }
        }

        private string bussiness;
        public string Bussiness
        {
            get { return bussiness; }
            set { bussiness = value; }
        }

        private string administration;
        public string Administration
        {
            get { return administration; }
            set { administration = value; }
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

        private string outdate;
        public string OutDate
        {
            get { return outdate; }
            set { outdate = value; }
        }

        private string outstartdate;
        public string OutStartDate
        {
            get { return outstartdate; }
            set { outstartdate = value; }
        }

        private string ouendtdate;
        public string OutEndDate
        {
            get { return ouendtdate; }
            set { ouendtdate = value; }
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

        private string bdlongitude;
        public string BDLongitude
        {
            get { return bdlongitude; }
            set { bdlongitude = value; }
        }

        private string bdlatitude;
        public string BDLatitude
        {
            get { return bdlatitude; }
            set { bdlatitude = value; }
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

        private DataTable roomRather;
        public DataTable RoomRather
        {
            get { return roomRather; }
            set { roomRather = value; }
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

        private string orderID;
        public string OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        private string auditStatus;
        public string AuditStatus
        {
            get { return auditStatus; }
            set { auditStatus = value; }
        }

        private string adstatsback;
        public string ADStatsBack
        {
            get { return adstatsback; }
            set { adstatsback = value; }
        }

        private string faxnum;
        public string FaxNum
        {
            get { return faxnum; }
            set { faxnum = value; }
        }

        private string faxstatus;
        public string FaxStatus
        {
            get { return faxstatus; }
            set { faxstatus = value; }
        }

        private string statusList;
        public string StatusList
        {
            get { return statusList; }
            set { statusList = value; }
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

        private string isPushFog;
        public string IsPushFog
        {
            get { return isPushFog; }
            set { isPushFog = value; }
        }

        private string effectDate;
        public string EffectDate
        {
            get { return effectDate; }
            set { effectDate = value; }
        }

        private string roomCode;
        public string RoomCode
        {
            get { return roomCode; }
            set { roomCode = value; }
        }

        private string roomNM;
        public string RoomNM
        {
            get { return roomNM; }
            set { roomNM = value; }
        }

        private string roomACT;
        public string RoomACT
        {
            get { return roomACT; }
            set { roomACT = value; }
        }

        private string prRoomACT;
        public string PRRoomACT
        {
            get { return prRoomACT; }
            set { prRoomACT = value; }
        }

        private string prStatus;
        public string PRStatus
        {
            get { return prStatus; }
            set { prStatus = value; }
        }

        private string prRooms;
        public string PRRoomS
        {
            get { return prRooms; }
            set { prRooms = value; }
        }

        private string upPlan;
        public string UPPlan
        {
            get { return upPlan; }
            set { upPlan = value; }
        }

        private string roomStatus;
        public string RoomStatus
        {
            get { return roomStatus; }
            set { roomStatus = value; }
        }

        private string bedType;
        public string BedType
        {
            get { return bedType; }
            set { bedType = value; }
        }

        private string roomPer;
        public string RoomPer
        {
            get { return roomPer; }
            set { roomPer = value; }
        }

        private string roomArea;
        public string RoomArea
        {
            get { return roomArea; }
            set { roomArea = value; }
        }

        private string roomWlan;
        public string RoomWlan
        {
            get { return roomWlan; }
            set { roomWlan = value; }
        }

        private string guesType;
        public string GuesType
        {
            get { return guesType; }
            set { guesType = value; }
        }

        private string roomWindow;
        public string RoomWindow
        {
            get { return roomWindow; }
            set { roomWindow = value; }
        }

        private string roomSmoke;
        public string RoomSmoke
        {
            get { return roomSmoke; }
            set { roomSmoke = value; }
        }

        private long image_ID;
        public long Image_ID
        {
            get { return image_ID; }
            set { image_ID = value; }
        }

        private string isMyHotel;
        public string IsMyHotel
        {
            get { return isMyHotel; }
            set { isMyHotel = value; }
        }
    }
}