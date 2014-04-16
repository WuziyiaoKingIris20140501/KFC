using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity.Order;
using System.Collections;

namespace HotelVp.CMS.Domain.DataAccess.Order
{
    public abstract class OrderFeedbackDA
    {
        public static OrderFeedbackEntity BindOrderFeedBackList(OrderFeedbackEntity OrderFeedbackEntity)
        {
            OrderFeedbackDBEntity dbParm = (OrderFeedbackEntity.orderFeedbackDBEntity.Count > 0) ? OrderFeedbackEntity.orderFeedbackDBEntity[0] : new OrderFeedbackDBEntity();

            string strSQL = XmlSqlAnalyze.GotSqlTextFromXml("OrderFeedback", "t_lm_order_feedback_list");

            if (!String.IsNullOrEmpty(dbParm.OrderNum))
            {
                strSQL += " and lf.ORDER_NUM='" + dbParm.OrderNum + "'";
            }
            else
            {
                if (!String.IsNullOrEmpty(dbParm.CreateTimeStart))
                {
                    strSQL += " and lf.CREATE_TIME BETWEEN TO_DATE('" + dbParm.CreateTimeStart + "', 'yyyy-mm-dd')  AND TO_DATE('" + dbParm.CreateTimeEnd + "', 'yyyy-mm-dd')";
                }
                if (!String.IsNullOrEmpty(dbParm.UpdateTimeStart))
                {
                    strSQL += " and lf.UPDATE_TIME BETWEEN TO_DATE('" + dbParm.UpdateTimeStart + "', 'yyyy-mm-dd')  AND TO_DATE('" + dbParm.UpdateTimeEnd + "', 'yyyy-mm-dd')";
                }
                if (dbParm.IsProcess == "1")
                {
                    strSQL += " and lf.IS_PROCESS='1'";
                }
                else if (dbParm.IsProcess == "")
                {
                    strSQL += " and lf.IS_PROCESS is null ";
                }
            }

            OrderFeedbackEntity.QueryResult = DbHelperOra.Query(strSQL, true);

            return OrderFeedbackEntity;
        }

        public static OrderFeedbackEntity BindOrderDetailsByOrderNum(OrderFeedbackEntity OrderFeedbackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("FOGORDERNUM",OracleType.VarChar)
                                };

            OrderFeedbackDBEntity dbParm = (OrderFeedbackEntity.orderFeedbackDBEntity.Count > 0) ? OrderFeedbackEntity.orderFeedbackDBEntity[0] : new OrderFeedbackDBEntity();

            if (String.IsNullOrEmpty(dbParm.OrderNum))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.OrderNum;
            }

            OrderFeedbackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderFeedback", "t_lm_order_by_nsusercomplain", true, parm);
            return OrderFeedbackEntity;
        }


        public static string GetUserTel(string userAccount)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetUserTel");
            cmd.SetParameterValue("@PARAMETERS", userAccount);

            DataSet dsResult = cmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["User_Tel"].ToString();
            }
            return "";
        }

        public static OrderFeedbackEntity UpdateOrderFeedBack(OrderFeedbackEntity OrderFeedbackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ISPROCESS",OracleType.VarChar),
                                    new OracleParameter("OPERATOR",OracleType.VarChar),
                                    new OracleParameter("ORDERNUM",OracleType.VarChar)
                                };

            OrderFeedbackDBEntity dbParm = (OrderFeedbackEntity.orderFeedbackDBEntity.Count > 0) ? OrderFeedbackEntity.orderFeedbackDBEntity[0] : new OrderFeedbackDBEntity();
            if (String.IsNullOrEmpty(dbParm.IsProcess))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.IsProcess;
            }

            if (String.IsNullOrEmpty(dbParm.OperatorZH1))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.OperatorZH1;
            }

            if (String.IsNullOrEmpty(dbParm.OrderNum))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.OrderNum;
            }

            OrderFeedbackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderFeedback", "update_t_lm_order_feedback", false, parm);
            return OrderFeedbackEntity;
        }


        public static OrderFeedbackEntity UpdateOrderBookStatusOther(OrderFeedbackEntity OrderFeedbackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("ORDERNUM",OracleType.VarChar)
                                };

            OrderFeedbackDBEntity dbParm = (OrderFeedbackEntity.orderFeedbackDBEntity.Count > 0) ? OrderFeedbackEntity.orderFeedbackDBEntity[0] : new OrderFeedbackDBEntity();
            if (String.IsNullOrEmpty(dbParm.Content))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.Content;
            }

            if (String.IsNullOrEmpty(dbParm.OrderNum))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.OrderNum;
            }

            OrderFeedbackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderFeedback", "update_t_lm_order_bookstatusother", false, parm);
            return OrderFeedbackEntity;
        }
    }
}
