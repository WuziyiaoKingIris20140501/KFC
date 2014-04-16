using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace HotelVp.SELT.Domain.Entity
{
    public class LiquidateEntity : BaseEntity
    {
        private List<LiquidateDBEntity> liquidatedbentity;
        public List<LiquidateDBEntity> LiquidateDBEntity
        {
            get { return liquidatedbentity; }
            set { liquidatedbentity = value; }
        }
    }

    public class LiquidateDBEntity
    {
        #region 确认结算列表
        private string autoquery;
        public string AutoQuery
        {
            get { return autoquery; }
            set { autoquery = value; }
        }

        /********* 确认结算列表 **********/
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
        /// 个人保存
        /// </summary>
        private string saveUser;
        public string SaveUser
        {
            get { return saveUser; }
            set { saveUser = value; }
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
        /// 账单确认状态
        /// </summary>
        private string slStatus;
        public string SlStatus
        {
            get { return slStatus; }
            set { slStatus = value; }
        }

        /// <summary>
        /// 账单号
        /// </summary>
        private string billID;
        public string BillID
        {
            get { return billID; }
            set { billID = value; }
        }

        /// <summary>
        /// 确认结算列表
        /// </summary>
        private DataSet liquidateDetialList;
        public DataSet LiquidateDetialList
        {
            get { return liquidateDetialList; }
            set { liquidateDetialList = value; }
        }
        #endregion

        #region 确认结算明细
        /********* 确认结算明细 **********/
        /// <summary>
        /// 结算备注
        /// </summary>
        private string intoMonth;
        public string IntoMonth
        {
            get { return intoMonth; }
            set { intoMonth = value; }
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
        /// 账单确认人
        /// </summary>
        private string billConfirmUser;
        public string BillConfirmUser
        {
            get { return billConfirmUser; }
            set { billConfirmUser = value; }
        }

        /// <summary>
        /// 结算确认备注
        /// </summary>
        private string billConfirmRemark;
        public string BillConfirmRemark
        {
            get { return billConfirmRemark; }
            set { billConfirmRemark = value; }
        }

        /// <summary>
        /// 结算单位详情
        /// </summary>
        private DataTable dtUnitDetial;
        public DataTable DtUnitDetial
        {
            get { return dtUnitDetial; }
            set { dtUnitDetial = value; }
        }

        /// <summary>
        /// 结算单详情
        /// </summary>
        private DataTable dtSlDetial;
        public DataTable DtSLDetial
        {
            get { return dtSlDetial; }
            set { dtSlDetial = value; }
        }

        /// <summary>
        /// 本月订单
        /// </summary>
        private DataTable dtMonthOrder;
        public DataTable DtMonthOrder
        {
            get { return dtMonthOrder; }
            set { dtMonthOrder = value; }
        }

        /// <summary>
        /// 历史遗漏订单
        /// </summary>
        private DataTable dtHisOrder;
        public DataTable DtHisOrder
        {
            get { return dtHisOrder; }
            set { dtHisOrder = value; }
        }

        /// <summary>
        /// 历史遗漏款项
        /// </summary>
        private DataTable dtHisAmount;
        public DataTable DtHisAmount
        {
            get { return dtHisAmount; }
            set { dtHisAmount = value; }
        }
        #endregion

        #region 调整项
        /********* 调整项 **********/
        /// <summary>
        /// 调整项ID
        /// </summary>
        private string liquidationID;
        public string LiquidationID
        {
            get { return liquidationID; }
            set { liquidationID = value; }
        }

        /// <summary>
        /// 调整项类型
        /// </summary>
        private string liquidationType;
        public string LiquidationType
        {
            get { return liquidationType; }
            set { liquidationType = value; }
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
        /// 订单总价
        /// </summary>
        private string totalAmount;
        public string TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        /// <summary>
        /// 结算金额
        /// </summary>
        private string slAmount;
        public string SlAmount
        {
            get { return slAmount; }
            set { slAmount = value; }
        }

        /// <summary>
        /// 调整项备注
        /// </summary>
        private string liquidationRemark;
        public string LiquidationRemark
        {
            get { return liquidationRemark; }
            set { liquidationRemark = value; }
        }

        /// <summary>
        /// 调整项订单详情
        /// </summary>
        private Hashtable hsLiquidateOrder;
        public Hashtable HsLiquidateOrder
        {
            get { return hsLiquidateOrder; }
            set { hsLiquidateOrder = value; }
        }

        #endregion
    }
}
