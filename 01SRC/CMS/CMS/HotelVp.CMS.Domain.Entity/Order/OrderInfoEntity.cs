using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{   
    public class OrderInfoEntity : BaseEntity
    {
        private List<OrderInfoDBEntity> orderinfodbentity;
        public List<OrderInfoDBEntity> OrderInfoDBEntity
        {
            get { return orderinfodbentity; }
            set { orderinfodbentity = value; }
        }
    }
    
    public class OrderInfoDBEntity
    { 
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string stype;
        public string SType
        {
            get { return stype; }
            set { stype = value; }
        }

        private string order_NUM;
        public string ORDER_NUM	
        {
            get { return order_NUM; }
            set { order_NUM = value; }
        }

        private string city_ID;
        public string CITY_ID
        {
            get { return city_ID; }
            set { city_ID = value; }

        }

        private string hotel_ID;
        public string HOTEL_ID
        {

            get { return hotel_ID; }
            set { hotel_ID = value; }
        }

	    private string hotel_NAME;
        public string HOTEL_NAME
        {
            get { return hotel_NAME; }
            set { hotel_NAME = value; }
        }
	
        private string in_DATE;
        public string IN_DATE
        {

            get { return in_DATE; }
            set { in_DATE = value; }

        }

        private string startDate;
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private string endDate;
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }



        private string book_ROOM_NUM;
        public string BOOK_ROOM_NUM
        {
            get { return book_ROOM_NUM; }
            set { book_ROOM_NUM = value; }
        }

        private string guest_NAMES;
        public string GUEST_NAMES
        {
            get { return guest_NAMES; }
            set { guest_NAMES = value; }

        }	

        private string contact_NAME;
        public string CONTACT_NAME
        {
            get { return contact_NAME; }
            set { contact_NAME = value; }
        }

        private string contact_TEL;
        public string CONTACT_TEL
        {
            get { return contact_TEL; }
            set { contact_TEL = value; }
        }
	
        private string book_TYPE;
        public string BOOK_TYPE
        {
            get { return book_TYPE; }
            set { book_TYPE = value; }
        }

        private string create_TIME;
        public string CREATE_TIME
        {

            get { return create_TIME; }
            set { create_TIME = value; }
        }

        private string user_ID;
        public string USER_ID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }
	
        private string book_STATUS;
        public string BOOK_STATUS
        {
            get { return book_STATUS; }
            set { book_STATUS = value; }
        }

	    private string pay_STATUS;
        public string PAY_STATUS
        {
            get { return pay_STATUS; }
            set { pay_STATUS = value; }
        }
	
        private string hold_TIME;
        public string HOLD_TIME
        {
            get { return hold_TIME; }
            set { hold_TIME = value; }
        }

	    private string fog_ORDER_NUM;
        public string FOG_ORDER_NUM
        {
            get { return fog_ORDER_NUM; }
            set { fog_ORDER_NUM = value; }
        }	

        private string out_DATE;
        public string OUT_DATE
        {
            get { return out_DATE; }
            set { out_DATE = value; }
        }
	
        private string book_REMARK;
        public string BOOK_REMARK
        {
            get { return book_REMARK; }
            set { book_REMARK = value; }
        }
	
        private string book_SOURCE;
        public string BOOK_SOURCE
        {
            get { return book_SOURCE; }
            set { book_SOURCE = value; }
        }

	    private string book_PRICE;
        public string BOOK_PRICE
        {
            get { return book_PRICE; }
            set { book_PRICE = value; }
        }

        private string room_TYPE_CODE;
        public string ROOM_TYPE_CODE
        {
            get { return room_TYPE_CODE; }
            set { room_TYPE_CODE = value; }
        }	

        private string price_CODE;
        public string PRICE_CODE
        {
            get { return price_CODE; }
            set { price_CODE = value; }
        }	

        private string order_CANCLE_REASON;
        public string ORDER_CANCLE_REASON
        {
            get { return order_CANCLE_REASON; }
            set { order_CANCLE_REASON = value; }
        }

        private string book_TOTAL_PRICE;
        public string BOOK_TOTAL_PRICE
        {
            get { return book_TOTAL_PRICE; }
            set { book_TOTAL_PRICE = value; }
        }


        private string login_MOBILE;
        public string LOGIN_MOBILE
        {
            get { return login_MOBILE; }
            set { login_MOBILE = value; }
        }

        private string overTIME;
        public string OVERTIME
        {
            get { return overTIME; }
            set { overTIME = value; }
        }

        

        private string memo1;
        public string MEMO1
        {
            get { return memo1; }
            set { memo1 = value; }
        }

        private string pro_DESC;
        public string PRO_DESC
        {
            get { return pro_DESC; }
            set { pro_DESC = value; }
        }

	    private string pro_CONTENT;
        public string PRO_CONTENT
        {
            get { return pro_CONTENT; }
            set { pro_CONTENT = value; }
        }	

        private string is_NETWORK;
        public string IS_NETWORK
        {
            get { return is_NETWORK; }
            set { is_NETWORK = value; }
        }

        private string breakfast_NUM;
        public string BREAKFAST_NUM
        {
            get { return breakfast_NUM; }
            set { breakfast_NUM = value; }
        }

        private string ticket_USERCODE;
        public string TICKET_USERCODE
        {
            get { return ticket_USERCODE; }
            set { ticket_USERCODE = value; }
        }	

        private string ticket_AMOUNT;
        public string TICKET_AMOUNT
        {
            get { return ticket_AMOUNT; }
            set { ticket_AMOUNT = value; }
        }	

        private string ticket_COUNT;
        public string TICKET_COUNT
        {
            get { return ticket_COUNT; }
            set { ticket_COUNT = value; }
        }

	    private string room_TYPE_NAME;
        public string ROOM_TYPE_NAME
        {
            get { return room_TYPE_NAME; }
            set { room_TYPE_NAME = value; }
        }

	    private string book_STATUS_OTHER;
        public string BOOK_STATUS_OTHER
        {
            get { return book_STATUS_OTHER; }
            set { book_STATUS_OTHER = value; }
        }

	    private string book_PERSON_TEL;
        public string BOOK_PERSON_TEL
        {
            get { return book_PERSON_TEL; }
            set { book_PERSON_TEL = value; }
        }	

        private string pay_METHOD;
        public string PAY_METHOD
        {
            get { return pay_METHOD; }
            set { pay_METHOD = value; }
        }	

        private string is_RESERVE;
        public string IS_RESERVE
        {
            get { return is_RESERVE; }
            set { is_RESERVE = value; }
        }	

        private string fog_RESVTYPE;
        public string FOG_RESVTYPE
        {
            get { return fog_RESVTYPE; }
            set { fog_RESVTYPE = value; }
        }

	    private string fog_RESVSTATUS;
        public string FOG_RESVSTATUS
        {
            get { return fog_RESVSTATUS; }
            set { fog_RESVSTATUS = value; }
        }	

        private string fog_AUDITSTATUS;
        public string FOG_AUDITSTATUS
        {
            get { return fog_AUDITSTATUS; }
            set { fog_AUDITSTATUS = value; }
        }	

        private string lmbar_STATUS;
        public string LMBAR_STATUS
        {
            get { return lmbar_STATUS; }
            set { lmbar_STATUS = value; }
        }

	    private string pay_METHODDESC;
        public string PAY_METHODDESC
        {
            get { return pay_METHODDESC; }
            set { pay_METHODDESC = value; }
        }

        private string update_TIME;
        public string UPDATE_TIME 
        {
            get { return update_TIME; }
            set { update_TIME = value; }
        }	


        private string cityID;
        public string CityID 
        {
            get { return cityID; }
            set { cityID = value; }
        }	

        private string sortID;
        public string SortID 
        {
            get { return sortID; }
            set { sortID = value; }
        }

        private string cStatus;
        public string CStatus 
        {
            get { return cStatus; }
            set { cStatus = value; }
        }

        private string fStatus;
        public string FStatus 
        {
            get { return fStatus; }
            set { fStatus = value; }
        }

        private string fgStatus;
        public string FGStatus 
        {
            get { return fgStatus; }
            set { fgStatus = value; }
        }
        
        private string hotelConfirm;
        public string HotelConfirm 
        {
            get { return hotelConfirm; }
            set { hotelConfirm = value; }
        }
        
        private string hotelID;
        public string HotelID 
        {
            get { return hotelID; }
            set { hotelID = value; }
        }

        private string bussiness;
        public string Bussiness 
        {
            get { return bussiness; }
            set { bussiness = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private ArrayList orderlist;
        public ArrayList OrderList 
        {
            get { return orderlist; }
            set { orderlist = value; }
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

        private string userID;
        public string UserID 
        {
            get { return userID; }
            set { userID = value; }
        }

        private string orderID;
        public string OrderID 
        {
            get { return orderID; }
            set { orderID = value; }
        }

        private string faxNum;
        public string FaxNum
        {
            get { return faxNum; }
            set { faxNum = value; }
        }

        private string sqltype;
        public string SqlType
        {
            get { return sqltype; }
            set { sqltype = value; }
        }

        private string faxStatus;
        public string FaxStatus
        {
            get { return faxStatus; }
            set { faxStatus = value; }
        }

        private string outdate;
        public string OutDate
        {
            get { return outdate; }
            set { outdate = value; }
        }

        private string eventType;
        public string EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        private string actionID;
        public string ActionID
        {
            get { return actionID; }
            set { actionID = value; }
        }

        private string approveId;
        public string ApproveId
        {
            get { return approveId; }
            set { approveId = value; }
        }

        private string canelReson;
        public string CanelReson
        {
            get { return canelReson; }
            set { canelReson = value; }
        }

        private string inRoomID;
        public string InRoomID
        {
            get { return inRoomID; }
            set { inRoomID = value; }
        }

        private string isdbapprove;
                public string IsDbApprove
        {
            get { return isdbapprove; }
            set { isdbapprove = value; }
        }

        private string odStatus;
        public string OdStatus
        {
            get { return odStatus; }
            set { odStatus = value; }
        }

        private string remark;
        public string REMARK
        {
            get { return remark; }
            set { remark = value; }
        }

        private string cannel;
        public string CANNEL
        {
            get { return cannel; }
            set { cannel = value; }
        }

        private string operateResult;
        public string OperateResult
        {
            get { return operateResult; }
            set { operateResult = value; }
        }
    }
}
