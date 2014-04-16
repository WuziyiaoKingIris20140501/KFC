using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HotelVp.SELT.Domain.Entity
{
    public class WriteOffEntity : BaseEntity
    {
        private List<WriteOffDBEntity> writeoffdbentity;
        public List<WriteOffDBEntity> WriteOffDBEntity
        {
            get { return writeoffdbentity; }
            set { writeoffdbentity = value; }
        }
    }

    public class WriteOffDBEntity
    {
        /// <summary>
        /// 清结算ID
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
        /// 收款ID
        /// </summary>
        private string collectID;
        public string CollectID
        {
            get { return collectID; }
            set { collectID = value; }
        }

        /// <summary>
        /// 回款销账ID
        /// </summary>
        private string writeoffID;
        public string WriteOffID
        {
            get { return writeoffID; }
            set { writeoffID = value; }
        }

        #region 导入收款查询条件（待销账款项）
        /// <summary>
        /// 付款时间（开始）
        /// </summary>
        private string impDealTimeST;
        public string ImpDealTimeST
        {
            get { return impDealTimeST; }
            set { impDealTimeST = value; }
        }

        /// <summary>
        /// 付款时间（结束）
        /// </summary>
        private string impDealTimeDT;
        public string ImpDealTimeDT
        {
            get { return impDealTimeDT; }
            set { impDealTimeDT = value; }
        }

        /// <summary>
        /// 付款金额（开始）
        /// </summary>
        private string impAmountST;
        public string ImpAmountST
        {
            get { return impAmountST; }
            set { impAmountST = value; }
        }

        /// <summary>
        /// 付款金额（结束）
        /// </summary>
        private string impAmountDT;
        public string ImpAmountDT
        {
            get { return impAmountDT; }
            set { impAmountDT = value; }
        }

        /// <summary>
        /// 添加人
        /// </summary>
        private string impUser;
        public string ImpUser
        {
            get { return impUser; }
            set { impUser = value; }
        }

        /// <summary>
        /// 付款方
        /// </summary>
        private string impPayName;
        public string ImpPayName
        {
          get { return impPayName; }
          set { impPayName = value; }
        }
        
        /// <summary>
        /// 付款账号
        /// </summary>
        private string impPayAccount;
        public string ImpPayAccount
        {
            get { return impPayAccount; }
            set { impPayAccount = value; }
        }

        /// <summary>
        /// 筛选匹配状态
        /// </summary>
        private string impMatch;
        public string ImpMatch
        {
            get { return impMatch; }
            set { impMatch = value; }
        }

        /// <summary>
        /// 待销账列表(匹配)
        /// </summary>
        private DataTable dtWriteoff;
        public DataTable DtWriteoff
        {
          get { return dtWriteoff; }
          set { dtWriteoff = value; }
        }
        #endregion

        #region 结算单位查询
        /// <summary>
        /// 结算单位名称
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
        /// 结算金额（开始）
        /// </summary>
        private string slAmountST;
        public string SlAmountST
        {
            get { return slAmountST; }
            set { slAmountST = value; }
        }

        /// <summary>
        /// 结算金额（结束）
        /// </summary>
        private string slAmountDT;
        public string SlAmountDT
        {
            get { return slAmountDT; }
            set { slAmountDT = value; }
        }

        /// <summary>
        /// 付款账号
        /// </summary>
        private string slPayNo;
        public string SlPayNo
        {
            get { return slPayNo; }
            set { slPayNo = value; }
        }

        /// <summary>
        /// 结算单位列表
        /// </summary>
        private DataTable dtSlUnitList;
        public DataTable DtSlUnitList
        {
            get { return dtSlUnitList; }
            set { dtSlUnitList = value; }
        }
        #endregion

        #region 待销账结算单位账单查询（查询待销账目）
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
        /// 订单号
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
        /// 销账状态
        /// </summary>
        private string settleStatus;
        public string SettleStatus
        {
            get { return settleStatus; }
            set { settleStatus = value; }
        }

        /// <summary>
        /// 查询待销账目列表
        /// </summary>
        private DataTable dtSettleList;
        public DataTable DtSettleList
        {
            get { return dtSettleList; }
            set { dtSettleList = value; }
        }
        #endregion

        #region 结算单位详情
        /// <summary>
        /// 结算单位详情
        /// </summary>
        private DataTable dtUnitDetail;
        public DataTable DtUnitDetail
        {
            get { return dtUnitDetail; }
            set { dtUnitDetail = value; }
        }

        /// <summary>
        /// 发票历史
        /// </summary>
        private DataTable dtInvoiceHis;
        public DataTable DtInvoiceHis
        {
            get { return dtInvoiceHis; }
            set { dtInvoiceHis = value; }
        }

        /// <summary>
        /// 结算备注
        /// </summary>
        private string unitRemark;
        public string UnitRemark
        {
            get { return unitRemark; }
            set { unitRemark = value; }
        }

        /// <summary>
        /// 待销账列表(账单+历史+调整项)
        /// </summary>
        private DataTable dtSettleAllHis;
        public DataTable DtSettleAllHis
        {
            get { return dtSettleAllHis; }
            set { dtSettleAllHis = value; }
        }
        #endregion

        #region 结算调整项
        /// <summary>
        /// 调整项类型
        /// </summary>
        private string settleType;
        public string SettleType
        {
          get { return settleType; }
          set { settleType = value; }
        }

        /// <summary>
        /// 调整项名称
        /// </summary>
        private string adjustName;
        public string AdjustName
        {
          get { return adjustName; }
          set { adjustName = value; }
        }

        /// <summary>
        /// 调整金额
        /// </summary>
        private string adjustAmount;
        public string AdjustAmount
        {
          get { return adjustAmount; }
          set { adjustAmount = value; }
        }

        /// <summary>
        /// 调整项备注
        /// </summary>
        private string adjustRemark;
        public string AdjustRemark
        {
            get { return adjustRemark; }
            set { adjustRemark = value; }
        }
        #endregion
    }
}
