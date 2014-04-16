using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HotelVp.SELT.Domain.Entity
{
    public class InvoiceEntity : BaseEntity
    {
        private List<InvoiceDBEntity> invoicedbentity;
        public List<InvoiceDBEntity> InvoiceDBEntity
        {
            get { return invoicedbentity; }
            set { invoicedbentity = value; }
        }
    }

    public class InvoiceDBEntity
    {
        #region 结算开票列表
        /// <summary>
        /// 结算单位ID
        /// </summary>
        private string slID;
        public string SLID
        {
            get { return slID; }
            set { slID = value; }
        }

        /// <summary>
        /// 结算单位ID
        /// </summary>
        private string unitID;
        public string UnitID
        {
            get { return unitID; }
            set { unitID = value; }
        }

        /// <summary>
        /// 结算单位
        /// </summary>
        private string unitName;
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }

        /// <summary>
        /// 酒店ID
        /// </summary>
        private string hotelID;
        public string HotelID
        {
            get { return hotelID; }
            set { hotelID = value; }
        }

        /// <summary>
        /// 酒店集团
        /// </summary>
        private string hotelGroup;
        public string HotelGroup
        {
            get { return hotelGroup; }
            set { hotelGroup = value; }
        }

        /// <summary>
        /// 城市ID
        /// </summary>
        private string cityID;
        public string CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        /// <summary>
        /// 结算负责人
        /// </summary>
        private string unitCharge;
        public string UnitCharge
        {
            get { return unitCharge; }
            set { unitCharge = value; }
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        private string orderID;
        public string OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        /// <summary>
        /// 结算月
        /// </summary>
        private string slMonth;
        public string SlMonth
        {
            get { return slMonth; }
            set { slMonth = value; }
        }

        /// <summary>
        /// 开票状态
        /// </summary>
        private string invoiceStatus;
        public string InvoiceStatus
        {
            get { return invoiceStatus; }
            set { invoiceStatus = value; }
        }

        /// <summary>
        /// 开票列表
        /// </summary>
        private DataSet invoiceList;
        public DataSet InvoiceList
        {
            get { return invoiceList; }
            set { invoiceList = value; }
        }

        /// <summary>
        /// 导出待开票列表
        /// </summary>
        private DataSet invoiceExportList;
        public DataSet InvoiceExportList
        {
            get { return invoiceExportList; }
            set { invoiceExportList = value; }
        }

        /// <summary>
        /// 导入已开票列表
        /// </summary>
        private DataSet invoiceImportList;
        public DataSet InvoiceImportList
        {
            get { return invoiceImportList; }
            set { invoiceImportList = value; }
        }
        #endregion

        #region 发票详情
        /// <summary>
        /// 发票信息类型
        /// </summary>
        private string invoiceDataType;
        public string InvoiceDataType
        {
            get { return invoiceDataType; }
            set { invoiceDataType = value; }
        }

        /// <summary>
        /// 发票信息类型
        /// </summary>
        private string invoiceID;
        public string InvoiceID
        {
            get { return invoiceID; }
            set { invoiceID = value; }
        }

        /// <summary>
        /// 开票详情
        /// </summary>
        private DataSet invoiceDetail;
        public DataSet InvoiceDetail
        {
            get { return invoiceDetail; }
            set { invoiceDetail = value; }
        }

        /// <summary>
        /// 发票抬头
        /// </summary>
        private string invoiceName;
        public string InvoiceName
        {
            get { return invoiceName; }
            set { invoiceName = value; }
        }

        /// <summary>
        /// 发票项目
        /// </summary>
        private string invoiceProject;
        public string InvoiceProject
        {
            get { return invoiceProject; }
            set { invoiceProject = value; }
        }

        /// <summary>
        /// 发票金额
        /// </summary>
        private string invoiceAmount;
        public string InvoiceAmount
        {
            get { return invoiceAmount; }
            set { invoiceAmount = value; }
        }

        /// <summary>
        /// 邮寄编号
        /// </summary>
        private string zipNum;
        public string ZipNum
        {
            get { return zipNum; }
            set { zipNum = value; }
        }

        /// <summary>
        /// 邮寄地址
        /// </summary>
        private string zipAddress;
        public string ZipAddress
        {
            get { return zipAddress; }
            set { zipAddress = value; }
        }

        /// <summary>
        /// 当前发票号码
        /// </summary>
        private string invoiceNum;
        public string InvoiceNum
        {
            get { return invoiceNum; }
            set { invoiceNum = value; }
        }

        /// <summary>
        /// 是否重开
        /// </summary>
        private string isReOpen;
        public string IsReOpen
        {
            get { return isReOpen; }
            set { isReOpen = value; }
        }

        /// <summary>
        /// 重开票原因
        /// </summary>
        private string reOpenReason;
        public string ReOpenReason
        {
            get { return reOpenReason; }
            set { reOpenReason = value; }
        }

        /// <summary>
        /// 旧发票号码
        /// </summary>
        private string oldZipNum;
        public string OldZipNum
        {
            get { return oldZipNum; }
            set { oldZipNum = value; }
        }
        #endregion
    }
}
