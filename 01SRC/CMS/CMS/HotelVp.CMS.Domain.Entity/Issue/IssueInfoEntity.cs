using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class IssueInfoEntity : BaseEntity
    {
        private List<IssueInfoDBEntity> issyeinfodbentity;
        public List<IssueInfoDBEntity> IssueInfoDBEntity
        {
            get { return issyeinfodbentity; }
            set { issyeinfodbentity = value; }
        }
    }

    public class IssueInfoDBEntity
    {
        private string issueiD;
        public string IssueID
        {
            get { return issueiD; }
            set { issueiD = value; }
        }

        private string revlkey;
        public string RevlKey
        {
            get { return revlkey; }
            set { revlkey = value; }
        }

        private string revltype;
        public string RevlType
        {
            get { return revltype; }
            set { revltype = value; }
        }

        private string issueCtper;
        public string IssueCtPer
        {
            get { return issueCtper; }
            set { issueCtper = value; }
        }

        private string issueCtdt;
        public string IssueCtDt
        {
            get { return issueCtdt; }
            set { issueCtdt = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string priority;
        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private string issueType;
        public string IssueType
        {
            get { return issueType; }
            set { issueType = value; }
        }

        private string assignto;
        public string Assignto
        {
            get { return assignto; }
            set { assignto = value; }
        }

        private string assignnm;
        public string AssignNm
        {
            get { return assignnm; }
            set { assignnm = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string isIndemnify;
        public string IsIndemnify
        {
            get { return isIndemnify; }
            set { isIndemnify = value; }
        }

        private string indemnifyPrice;
        public string IndemnifyPrice
        {
            get { return indemnifyPrice; }
            set { indemnifyPrice = value; }
        }

        private string ticketCode;
        public string TicketCode
        {
            get { return ticketCode; }
            set { ticketCode = value; }
        }

        private string relatedType;
        public string RelatedType
        {
            get { return relatedType; }
            set { relatedType = value; }
        }

        private string relatedID;
        public string RelatedID
        {
            get { return relatedID; }
            set { relatedID = value; }
        }

        private string timeDiffTal;
        public string TimeDiffTal
        {
            get { return timeDiffTal; }
            set { timeDiffTal = value; }
        }

        private string timeSpans;
        public string TimeSpans
        {
            get { return timeSpans; }
            set { timeSpans = value; }
        }

        private string chkMsgAssgin;
        public string ChkMsgAssgin
        {
            get { return chkMsgAssgin; }
            set { chkMsgAssgin = value; }
        }

        private string msgAssginText;
        public string MsgAssginText
        {
            get { return msgAssginText; }
            set { msgAssginText = value; }
        }

        private string msgAssginRs;
        public string MsgAssginRs
        {
            get { return msgAssginRs; }
            set { msgAssginRs = value; }
        }

        private string chkMsgUser;
        public string ChkMsgUser
        {
            get { return chkMsgUser; }
            set { chkMsgUser = value; }
        }

        private string msgUserText;
        public string MsgUserText
        {
            get { return msgUserText; }
            set { msgUserText = value; }
        }

        private string msgUserRs;
        public string MsgUserRs
        {
            get { return msgUserRs; }
            set { msgUserRs = value; }
        }

        private string updateTime;
        public string UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string hisRemark;
        public string HisRemark
        {
            get { return hisRemark; }
            set { hisRemark = value; }
        }

        private string issueHis;
        public string IssueHis
        {
            get { return issueHis; }
            set { issueHis = value; }
        }

        private string updateUser;
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
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

        private string hotelID;
        public string HotelID
        {
            get { return hotelID; }
            set { hotelID = value; }
        }

        private string priceCode;
        public string PriceCode
        {
            get { return priceCode; }
            set { priceCode = value; }
        }

        private string actionType;
        public string ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }
    }
}