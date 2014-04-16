using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class ReportCenterEntity : BaseEntity
    {
        private string lmLogID;
        public string LmLogID
        {
            get { return lmLogID; }
            set { lmLogID = value; }
        }

        private string fogOrderID;
        public string FogOrderID
        {
            get { return fogOrderID; }
            set { fogOrderID = value; }
        }

        private string mobile;
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        private string operateName;
        public string OperateName
        {
            get { return operateName; }
            set { operateName = value; }
        }

        private string operateContent;
        public string OperateContent
        {
            get { return operateContent; }
            set { operateContent = value; }
        }

        private string operateResultCode;
        public string OperateResultCode
        {
            get { return operateResultCode; }
            set { operateResultCode = value; }
        }

        private string operateResultMsg;
        public string OperateResultMsg
        {
            get { return operateResultMsg; }
            set { operateResultMsg = value; }
        }

        private string handlers;
        public string Handlers
        {
            get { return handlers; }
            set { handlers = value; }
        }

        private string operateIp;
        public string OperateIp
        {
            get { return operateIp; }
            set { operateIp = value; }
        }

        private string operateTime;
        public string OperateTime
        {
            get { return operateTime; }
            set { operateTime = value; }
        }

        private string eventType;
        public string EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        private string eventID;
        public string EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        private string eventContent;
        public string EventContent
        {
            get { return eventContent; }
            set { eventContent = value; }
        }

        private string eventResult;
        public string EventResult
        {
            get { return eventResult; }
            set { eventResult = value; }
        }

        private DateTime eventTime;
        public DateTime EventTime
        {
            get { return eventTime; }
            set { eventTime = value; }
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

         private string inStart;
        public string InStart
        {
            get { return inStart; }
            set { inStart = value; }
        }

        private string inEnd;
        public string InEnd
        {
            get { return inEnd; }
            set { inEnd = value; }
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

        private string payCode;
        public string PayCode
        {
            get { return payCode; }
            set { payCode = value; }
        }
        
        private string bookStatus;
        public string BookStatus
        {
            get { return bookStatus; }
            set { bookStatus = value; }
        }

        private string payStatus;
        public string PayStatus
        {
            get { return payStatus; }
            set { payStatus = value; }
        }
         
        private string platForm;
        public string PlatForm
        {
            get { return platForm; }
            set { platForm = value; }
        }
        
        private string ticket;
        public string Ticket
        {
            get { return ticket; }
            set { ticket = value; }
        }

        private string priceCode;
        public string PriceCode
        {
            get { return priceCode; }
            set { priceCode = value; }
        }

        private string aprove;
        public string Aprove
        {
            get { return aprove; }
            set { aprove = value; }
        }

        private string hotelComfirm;
        public string HotelComfirm
        {
            get { return hotelComfirm; }
            set { hotelComfirm = value; }
        }

        private string showType;
        public string ShowType
        {
            get { return showType; }
            set { showType = value; }
        }

        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private string sales;
        public string Sales
        {
            get { return sales; }
            set { sales = value; }
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

        private string ticketPcode;
        public string TicketPayCode
        {
            get { return ticketPcode; }
            set { ticketPcode = value; }
        }

        private string dashPopStatus;
        public string DashPopStatus
        {
            get { return dashPopStatus; }
            set { dashPopStatus = value; }
        }

        private string dashStartDTime;
        public string DashStartDTime
        {
            get { return dashStartDTime; }
            set { dashStartDTime = value; }
        }

        private string dashEndDTime;
        public string DashEndDTime
        {
            get { return dashEndDTime; }
            set { dashEndDTime = value; }
        }

        private string dashInStart;
        public string DashInStart
        {
            get { return dashInStart; }
            set { dashInStart = value; }
        }

        private string dashInEnd;
        public string DashInEnd
        {
            get { return dashInEnd; }
            set { dashInEnd = value; }
        }

        private string outTest;
        public string OutTest
        {
            get { return outTest; }
            set { outTest = value; }
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

        private string outStart;
        public string OutStart
        {
            get { return outStart; }
            set { outStart = value; }
        }

        private string outEnd;
        public string OutEnd
        {
            get { return outEnd; }
            set { outEnd = value; }
        }

        private string orderChannel;
        public string OrderChannel
        {
            get { return orderChannel; }
            set { orderChannel = value; }
        }

        private string hotelNM;
        public string HotelNM
        {
            get { return hotelNM; }
            set { hotelNM = value; }
        }

        private string salesTel;
        public string SalesTel
        {
            get { return salesTel; }
            set { salesTel = value; }
        }

        private string ccopr;
        public string Ccopr
        {
            get { return ccopr; }
            set { ccopr = value; }
        }

        private string sendMSG;
        public string SendMSG
        {
            get { return sendMSG; }
            set { sendMSG = value; }
        }

        private string orderTypeStatus;
        public string OrderTypeStatus
        {
            get { return orderTypeStatus; }
            set { orderTypeStatus = value; }
        }

        private string orderBookStatus;
        public string OrderBookStatus
        {
            get { return orderBookStatus; }
            set { orderBookStatus = value; }
        }

        private string orderBookStatusOther;
        public string OrderBookStatusOther
        {
            get { return orderBookStatusOther; }
            set { orderBookStatusOther = value; }
        }

        private string restPriceCode;
        public string RestPriceCode
        {
            get { return restPriceCode; }
            set { restPriceCode = value; }
        }

        private string restBookStatus;
        public string RestBookStatus
        {
            get { return restBookStatus; }
            set { restBookStatus = value; }
        }

        private string isReserve;
        public string IsReserve
        {
            get { return isReserve; }
            set { isReserve = value; }
        }

        private string canelReson;
        public string CanelReson
        {
            get { return canelReson; }
            set { canelReson = value; }
        }

        private string remark;
        public string BookRemark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string followUp;
        public string FollowUp
        {
            get { return followUp; }
            set { followUp = value; }
        }
    }
}