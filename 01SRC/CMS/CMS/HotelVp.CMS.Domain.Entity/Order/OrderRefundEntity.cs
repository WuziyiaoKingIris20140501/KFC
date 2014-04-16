using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity.Order
{
    public class OrderRefundEntity : BaseEntity
    {
        private List<OrderRefundDBEntity> orderrefunddbentity;
        public List<OrderRefundDBEntity> OrderRefundDBEntity
        {
            get { return orderrefunddbentity; }
            set { orderrefunddbentity = value; }
        }
    }

    public class OrderRefundDBEntity
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string obj_id;
        public string Obj_id
        {
            get { return obj_id; }
            set { obj_id = value; }
        }
        private string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        private string operators;
        public string Operators
        {
            get { return operators; }
            set { operators = value; }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string create_time;
        public string Create_time
        {
            get { return create_time; }
            set { create_time = value; }
        }
        private string update_time;
        public string Update_time
        {
            get { return update_time; }
            set { update_time = value; }
        }
        private string sn;
        public string Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        private string refund_account;
        public string Refund_account
        {
            get { return refund_account; }
            set { refund_account = value; }
        }
        private string refund_time;
        public string Refund_time
        {
            get { return refund_time; }
            set { refund_time = value; }
        }
    }
}
