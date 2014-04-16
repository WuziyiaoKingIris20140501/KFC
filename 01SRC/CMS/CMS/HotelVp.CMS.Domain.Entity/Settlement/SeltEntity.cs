using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace HotelVp.CMS.Domain.Entity
{
    public class SeltEntity : BaseEntity
    {
        private List<SeltDBEntity> seltdbentity;
        public List<SeltDBEntity> SeltDBEntity
        {
            get { return seltdbentity; }
            set { seltdbentity = value; }
        }
    }

    public class SeltDBEntity
    {
        private string seltNo;
        public string SeltNo
        {
            get { return seltNo; }
            set { seltNo = value; }
        }

        private string seltID;
        public string SeltID
        {
            get { return seltID; }
            set { seltID = value; }
        }

        private string unitNm;
        public string UnitNm
        {
            get { return unitNm; }
            set { unitNm = value; }
        }

        private string invoiceNm;
        public string InvoiceNm
        {
            get { return invoiceNm; }
            set { invoiceNm = value; }
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

        private string term;
        public string Term
        {
            get { return term; }
            set { term = value; }
        }

        private string termStDt;
        public string TermStDt
        {
            get { return termStDt; }
            set { termStDt = value; }
        }

        private string tax;
        public string Tax
        {
            get { return tax; }
            set { tax = value; }
        }

        private string per;
        public string Per
        {
            get { return per; }
            set { per = value; }
        }

        private string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private string fax;
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        private string sales;
        public string Sales
        {
            get { return sales; }
            set { sales = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string billItem;
        public string BillItem
        {
            get { return billItem; }
            set { billItem = value; }
        }

        private string taxNo;
        public string TaxNo
        {
            get { return taxNo; }
            set { taxNo = value; }
        }

        private string payNo;
        public string PayNo
        {
            get { return payNo; }
            set { payNo = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private DataTable dthotellist;
        public DataTable dtHotelList
        {
            get { return dthotellist; }
            set { dthotellist = value; }
        }
    }
}