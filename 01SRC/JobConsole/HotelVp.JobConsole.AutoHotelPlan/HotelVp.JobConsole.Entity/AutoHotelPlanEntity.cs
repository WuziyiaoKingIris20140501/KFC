using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.JobConsole.Entity
{
    public class AutoHotelPlanEntity : BaseEntity
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
        private string hotelID;
        public string HotelID
        {
            get { return hotelID; }
            set { hotelID = value; }
        }

        private string hPID;
        public string HPID
        {
            get { return hPID; }
            set { hPID = value; }
        }

        private string supplier;
        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        private string planID;
        public string PlanID
        {
            get { return planID; }
            set { planID = value; }
        }

        private string isReserve;
        public string IsReserve
        {
            get { return isReserve; }
            set { isReserve = value; }
        }

        private string userCode;
        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }

        private string hotelNM;
        public string HotelNM
        {
            get { return hotelNM; }
            set { hotelNM = value; }
        }

        private string ftType;
        public string FtType
        {
            get { return ftType; }
            set { ftType = value; }
        }

        private DataSet hotelMain;
        public DataSet HotelMain
        {
            get { return hotelMain; }
            set { hotelMain = value; }
        }

        private DataSet hotelRoom;
        public DataSet HotelRoom
        {
            get { return hotelRoom; }
            set { hotelRoom = value; }
        }

        private ArrayList hotelImage;
        public ArrayList HotelImage
        {
            get { return hotelImage; }
            set { hotelImage = value; }
        }

        private DataSet hotelFtType;
        public DataSet HotelFtType
        {
            get { return hotelFtType; }
            set { hotelFtType = value; }
        }

        private DataSet hotelFacilities;
        public DataSet HotelFacilities
        {
            get { return hotelFacilities; }
            set { hotelFacilities = value; }
        }

        private DataSet hotelList;
        public DataSet HotelList
        {
            get { return hotelList; }
            set { hotelList = value; }
        }

        private string cityNo;
        public string CityNo
        {
            get { return cityNo; }
            set { cityNo = value; }
        }

        private string cityID;
        public string CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        private string cityNM;
        public string CityNM
        {
            get { return cityNM; }
            set { cityNM = value; }
        }

        private string typeID;
        public string TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }

        private string platForm;
        public string PlatForm
        {
            get { return platForm; }
            set { platForm = value; }
        }

        private string serVer;
        public string SerVer
        {
            get { return serVer; }
            set { serVer = value; }
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

        private string areaID;
        public string AreaID
        {
            get { return areaID; }
            set { areaID = value; }
        }

        private string onlineStatus;
        public string OnlineStatus
        {
            get { return onlineStatus; }
            set { onlineStatus = value; }
        }

        private string planStatus;
        public string PlanStatus
        {
            get { return planStatus; }
            set { planStatus = value; }
        }

        private string seQ;
        public string SEQ
        {
            get { return seQ; }
            set { seQ = value; }
        }

        private string pinyin;
        public string Pinyin
        {
            get { return pinyin; }
            set { pinyin = value; }
        }

        private string pinyinS;
        public string PinyinS
        {
            get { return pinyinS; }
            set { pinyinS = value; }
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

        private string effHour;
        public string EffHour
        {
            get { return effHour; }
            set { effHour = value; }
        }

        private string roomList;
        public string RoomList
        {
            get { return roomList; }
            set { roomList = value; }
        }

        private string priceCode;
        public string PriceCode
        {
            get { return priceCode; }
            set { priceCode = value; }
        }

        private string roomCount;
        public string RoomCount
        {
            get { return roomCount; }
            set { roomCount = value; }
        }

        private string weekList;
        public string WeekList
        {
            get { return weekList; }
            set { weekList = value; }
        }

        private string note1;
        public string Note1
        {
            get { return note1; }
            set { note1 = value; }
        }

        private string note2;
        public string Note2
        {
            get { return note2; }
            set { note2 = value; }
        }

        private string onePrice;
        public string OnePrice
        {
            get { return onePrice; }
            set { onePrice = value; }
        }

        private string twoPrice;
        public string TwoPrice
        {
            get { return twoPrice; }
            set { twoPrice = value; }
        }

        private string threePrice;
        public string ThreePrice
        {
            get { return threePrice; }
            set { threePrice = value; }
        }

        private string fourPrice;
        public string FourPrice
        {
            get { return fourPrice; }
            set { fourPrice = value; }
        }

        private string bedPrice;
        public string BedPrice
        {
            get { return bedPrice; }
            set { bedPrice = value; }
        }

        private string netPrice;
        public string NetPrice
        {
            get { return netPrice; }
            set { netPrice = value; }
        } 

        private string breakfastNum;
        public string BreakfastNum
        {
            get { return breakfastNum; }
            set { breakfastNum = value; }
        }

        private string breakPrice;
        public string BreakPrice
        {
            get { return breakPrice; }
            set { breakPrice = value; }
        }

        private string isNetwork;
        public string IsNetwork
        {
            get { return isNetwork; }
            set { isNetwork = value; }
        }

        private string roomStatus;
        public string RoomStatus
        {
            get { return roomStatus; }
            set { roomStatus = value; }
        }

        private string offsetval;
        public string Offsetval
        {
            get { return offsetval; }
            set { offsetval = value; }
        }

        private string offsetunit;
        public string Offsetunit
        {
            get { return offsetunit; }
            set { offsetunit = value; }
        }

        private string accountList;
        public string AccountList
        {
            get { return accountList; }
            set { accountList = value; }
        }

        private string saveType;
        public string SaveType
        {
            get { return saveType; }
            set { saveType = value; }
        }

        private string planDTime;
        public string PlanDTime
        {
            get { return planDTime; }
            set { planDTime = value; }
        }

        private string planTime;
        public string PlanTime
        {
            get { return planTime; }
            set { planTime = value; }
        }

        private string planStart;
        public string PlanStart
        {
            get { return planStart; }
            set { planStart = value; }
        }

        private string planEnd;
        public string PlanEnd
        {
            get { return planEnd; }
            set { planEnd = value; }
        }

        private string planWeek;
        public string PlanWeek
        {
            get { return planWeek; }
            set { planWeek = value; }
        }

        private string roomCode;
        public string RoomCode
        {
            get { return roomCode; }
            set { roomCode = value; }
        }

        private string roomName;
        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }
    }
}