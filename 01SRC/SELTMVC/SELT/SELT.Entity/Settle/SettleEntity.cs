using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HotelVp.SELT.Domain.Entity
{
    public class SettleEntity : BaseEntity
    {
        private List<SettleDBEntity> settledbentity;
        public List<SettleDBEntity> SettleDBEntity
        {
            get { return settledbentity; }
            set { settledbentity = value; }
        }
    }

    public class SettleDBEntity
    {
        #region 收款上传导入详情
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

        /// <summary>
        /// 上传文件名
        /// </summary>
        private string uploadFileName;
        public string UploadFileName
        {
            get { return uploadFileName; }
            set { uploadFileName = value; }
        }

        /// <summary>
        /// 流水生成时间
        /// </summary>
        private string serialNumDate;
        public string SerialNumDate
        {
          get { return serialNumDate; }
          set { serialNumDate = value; }
        }

        /// <summary>
        /// 上传总记录数
        /// </summary>
        private string totalUploadCount;
        public string TotalUploadCount
        {
            get { return totalUploadCount; }
            set { totalUploadCount = value; }
        }

        /// <summary>
        /// 上传总金额
        /// </summary>
        private string totalUploadAmount;
        public string TotalUploadAmount
        {
            get { return totalUploadAmount; }
            set { totalUploadAmount = value; }
        }

        /// <summary>
        /// 上传备注
        /// </summary>
        private string upLoadRemark;
        public string UpLoadRemark
        {
            get { return upLoadRemark; }
            set { upLoadRemark = value; }
        }

        /// <summary>
        /// 导入上传信息类型 - 0：初次导入/1：调整导入信息
        /// </summary>
        private string importActionType;
        public string ImportActionType
        {
            get { return importActionType; }
            set { importActionType = value; }
        }

        /// <summary>
        /// 导入收款列表
        /// </summary>
        private DataTable importList;
        public DataTable ImportList
        {
            get { return importList; }
            set { importList = value; }
        }

        /// <summary>
        /// 上传收款列表
        /// </summary>
        private DataTable upLoadList;
        public DataTable UpLoadList
        {
            get { return upLoadList; }
            set { upLoadList = value; }
        }

        /// <summary>
        /// 导入方式 - 批量导入 手动导入
        /// </summary>
        private string importType;
        public string ImportType
        {
            get { return importType; }
            set { importType = value; }
        }

        /// <summary>
        /// 添加收款类型 - 银行流水 支票 汇票 现金
        /// </summary>
        private string paymentType;
        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

        /// <summary>
        /// 交易时间
        /// </summary>
        private string dealTime;
        public string DealTime
        {
          get { return dealTime; }
          set { dealTime = value; }
        }

        /// <summary>
        /// 对方户名
        /// </summary>
        private string payName;
        public string PayName
        {
            get { return payName; }
            set { payName = value; }
        }

        /// <summary>
        /// 对方账号
        /// </summary>
        private string payAccount;
        public string PayAccount
        {
          get { return payAccount; }
          set { payAccount = value; }
        }

        /// <summary>
        /// 转入金额
        /// </summary>
        private string intoAmount;
        public string IntoAmount
        {
          get { return intoAmount; }
          set { intoAmount = value; }
        }

        /// <summary>
        /// 账户明细编号-交易流水号
        /// </summary>
        private string detailAerialNum;
        public string DetailAerialNum
        {
          get { return detailAerialNum; }
          set { detailAerialNum = value; }
        }
        
        /// <summary>
        /// 摘要
        /// </summary>
        private string summary;
        public string Summary
        {
          get { return summary; }
          set { summary = value; }
        }
        
        /// <summary>
        /// 备注
        /// </summary>
        private string remark;
        public string Remark
        {
          get { return remark; }
          set { remark = value; }
        }
        #endregion

        #region 上传收款查询条件
        /// <summary>
        /// 添加时间开始
        /// </summary>
        private string uploadStartDT;
        public string UploadStartDT
        {
            get { return uploadStartDT; }
            set { uploadStartDT = value; }
        }

        /// <summary>
        /// 添加时间结束
        /// </summary>
        private string uploadEndDT;
        public string UploadEndDT
        {
            get { return uploadEndDT; }
            set { uploadEndDT = value; }
        }

        /// <summary>
        /// 金额开始
        /// </summary>
        private string uploadAmountStart;
        public string UploadAmountStart
        {
            get { return uploadAmountStart; }
            set { uploadAmountStart = value; }
        }

        /// <summary>
        /// 金额结束
        /// </summary>
        private string uploadamountEnd;
        public string UploadAmountEnd
        {
            get { return uploadamountEnd; }
            set { uploadamountEnd = value; }
        }

        /// <summary>
        /// 添加人
        /// </summary>
        private string uploadUser;
        public string UploadUser
        {
            get { return uploadUser; }
            set { uploadUser = value; }
        }
        #endregion
    }
}
