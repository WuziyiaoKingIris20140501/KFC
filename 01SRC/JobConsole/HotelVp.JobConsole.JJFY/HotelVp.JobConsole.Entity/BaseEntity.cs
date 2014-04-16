using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Logger;

namespace JJZX.JobConsole.Entity
{
    public class BaseEntity
    {
        private string sortType;
        public string SortType
        {
            get { return sortType; }
            set { sortType = value; }
        }

        private string sortField;
        public string SortField
        {
            get { return sortField; }
            set { sortField = value; }
        }

        private int pageSize;
        public int PageSize
        {
            get {
                if (pageSize == 0)
                {
                    pageSize = (!String.IsNullOrEmpty(PubConstant.PageSizeString)) ? int.Parse(PubConstant.PageSizeString.ToString()) : 0;
                }
                return pageSize; 
            }
            set { pageSize = value; }
        }

        private int pageCurrent;
        public int PageCurrent
        {
            get { return pageCurrent; }
            set { pageCurrent = value; }
        }

        private int totalCount;
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }

        private DataSet dsResult;
        public DataSet QueryResult
        {
            get { return dsResult; }
            set { dsResult = value; }
        }

        private LogMessage logMessage;
        public LogMessage LogMessages
        {
            get { return logMessage; }
            set { logMessage = value; }
        }

        private string errMsg;
        public string ErrorMSG
        {
            get { return errMsg; }
            set { errMsg = value; }
        }

        private int iResult;
        public int Result
        {
            get { return iResult; }
            set { iResult = value; }
        }
    }   
}