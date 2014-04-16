using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity.Order
{
    public class OrderFeedbackEntity : BaseEntity
    {
        private List<OrderFeedbackDBEntity> orderfeedbackdbentity;
        public List<OrderFeedbackDBEntity> orderFeedbackDBEntity
        {
            get { return orderfeedbackdbentity; }
            set { orderfeedbackdbentity = value; }
        }
    }

    public class OrderFeedbackDBEntity
    {
        private string orderNum;
        public string OrderNum
        {
            get { return orderNum; }
            set { orderNum = value; }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string createTimeStart;
        public string CreateTimeStart
        {
            get { return createTimeStart; }
            set { createTimeStart = value; }
        }

        private string createTimeEnd;
        public string CreateTimeEnd
        {
            get { return createTimeEnd; }
            set { createTimeEnd = value; }
        }

        private string updateTimeStart;
        public string UpdateTimeStart
        {
            get { return updateTimeStart; }
            set { updateTimeStart = value; }
        }

        private string updateTimeEnd;
        public string UpdateTimeEnd
        {
            get { return updateTimeEnd; }
            set { updateTimeEnd = value; }
        }
        
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string OperatorZH;
        public string OperatorZH1
        {
            get { return OperatorZH; }
            set { OperatorZH = value; }
        }

        private string isProcess;
        public string IsProcess
        {
            get { return isProcess; }
            set { isProcess = value; }
        }
    }
}
