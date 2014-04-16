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
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class PaymentDA
    {
        public static PaymentEntity CommonSelect(PaymentEntity paymentEntity)
        {
            paymentEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Payment", "t_lm_b_platform", false);
            return paymentEntity;
        }

        public static PaymentEntity PlatFormSelect(PaymentEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PAYMENTID",OracleType.VarChar)
                                };
            PaymentDBEntity dbParm = (paymentEntity.PaymentDBEntity.Count > 0) ? paymentEntity.PaymentDBEntity[0] : new PaymentDBEntity();

            if (String.IsNullOrEmpty(dbParm.PaymentNo))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.PaymentNo;
            }

            paymentEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Payment", "t_lm_b_payment_all", false, parm);
            return paymentEntity;
        }

        public static PaymentEntity Select(PaymentEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PAYMENTNAME",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            PaymentDBEntity dbParm = (paymentEntity.PaymentDBEntity.Count > 0) ? paymentEntity.PaymentDBEntity[0] : new PaymentDBEntity();

            if (String.IsNullOrEmpty(dbParm.PaymentNo))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.PaymentNo;
            }

            if (String.IsNullOrEmpty(dbParm.Name_CN))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.Name_CN;
            }
            

            //if (String.IsNullOrEmpty(dbParm.OnlineStatus))
            //{
            //    parm[1].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[1].Value = dbParm.OnlineStatus;
            //}

            if (String.IsNullOrEmpty(dbParm.StartDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.StartDTime;
            }

            if (String.IsNullOrEmpty(dbParm.EndDTime))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.EndDTime;
            }

            paymentEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Payment", "t_lm_b_payment", false, parm);
            return paymentEntity;
        }

        public static int CheckInsert(PaymentEntity paymentEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("PAYMENTCODE",OracleType.VarChar),
                                    new OracleParameter("PAYMENTNM",OracleType.VarChar)
                                };
            PaymentDBEntity dbParm = (paymentEntity.PaymentDBEntity.Count > 0) ? paymentEntity.PaymentDBEntity[0] : new PaymentDBEntity();
            parm[0].Value = dbParm.PaymentID;
            parm[1].Value = dbParm.Name_CN;
            paymentEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Payment", "t_lm_b_payment_sigle", false, parm);

            if (paymentEntity.QueryResult.Tables.Count > 0 && paymentEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckUpdate(PaymentEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PAYMENTNM",OracleType.VarChar)                                 
                                };

            PaymentDBEntity dbParm = (paymentEntity.PaymentDBEntity.Count > 0) ? paymentEntity.PaymentDBEntity[0] : new PaymentDBEntity();

            parm[0].Value = int.Parse(dbParm.PaymentNo.ToString());
            parm[1].Value = dbParm.Name_CN;



            paymentEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Payment", "t_lm_b_payment_updatesigle", false, parm);

            if (paymentEntity.QueryResult.Tables.Count > 0 && paymentEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }
        public static int Insert(PaymentEntity paymentEntity)
        {
            if (paymentEntity.PaymentDBEntity.Count == 0)
            {
                return 0;
            }

            if (paymentEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(paymentEntity) > 0)
            {
                return 2;
            }

            PaymentDBEntity dbParm = (paymentEntity.PaymentDBEntity.Count > 0) ? paymentEntity.PaymentDBEntity[0] : new PaymentDBEntity();

            List<CommandInfo> sqlList = new List<CommandInfo>();
            CommandInfo InsertLmPaymentInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PAYMENTCODE",OracleType.VarChar),                                    
                                    new OracleParameter("PAYMENTNM",OracleType.VarChar)                                 
                                };

            lmParm[0].Value = getMaxIDfromSeq("T_LM_B_PAYMENT_SEQ");
            lmParm[1].Value = dbParm.PaymentID;
            lmParm[2].Value = dbParm.Name_CN;
            InsertLmPaymentInfo.SqlName = "Payment";
            InsertLmPaymentInfo.SqlId = "t_lm_b_payment_insert";
            InsertLmPaymentInfo.Parameters = lmParm;
            sqlList.Add(InsertLmPaymentInfo);

            PaymentEntity platformList = CommonSelect(paymentEntity);

            if (platformList.QueryResult.Tables.Count > 0 && platformList.QueryResult.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drRow in platformList.QueryResult.Tables[0].Rows)
                {
                    CommandInfo InsertCsPaymentInfo = new CommandInfo();
                    OracleParameter[] csParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PAYMENTID",OracleType.VarChar),
                                    new OracleParameter("PLATFORM_ID",OracleType.VarChar),  
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)                                 
                                };

                    csParm[0].Value = getMaxIDfromSeq("T_LM_B_PAYMENT_PLAT_SEQ");
                    csParm[1].Value = dbParm.PaymentID;
                    csParm[2].Value = drRow["PlatformCODE"].ToString();
                    csParm[3].Value = "0";
                    InsertCsPaymentInfo.SqlName = "Payment";
                    InsertCsPaymentInfo.SqlId = "t_lm_b_payment_plat_insert";
                    InsertCsPaymentInfo.Parameters = csParm;
                    sqlList.Add(InsertCsPaymentInfo);
                }
            }

            DbManager.ExecuteSqlTran(sqlList);

            //DataCommand cmd = DataCommandManager.GetDataCommand("InsertCityList");
            //foreach (PaymentDBEntity dbParm in paymentEntity.PaymentDBEntity)
            //{
            //    cmd.SetParameterValue("@ChannelID", dbParm.ChannelID);
            //    cmd.SetParameterValue("@NameCN", dbParm.Name_CN);
            //    cmd.SetParameterValue("@NameEN", PinyinHelper.GetPinyin(dbParm.Name_CN));
            //    cmd.SetParameterValue("@OnlineStatus", dbParm.OnlineStatus);
            //    cmd.SetParameterValue("@Remark", dbParm.Remark);
            //    cmd.SetParameterValue("@CreateUser", (paymentEntity.LogMessages != null) ? paymentEntity.LogMessages.Userid : "");
            //    cmd.SetParameterValue("@UpdateUser", (paymentEntity.LogMessages != null) ? paymentEntity.LogMessages.Userid : "");
            //    cmd.ExecuteNonQuery();
            //}
            
            return 1;
        }

        public static int Update(PaymentEntity paymentEntity)
        {
            if (paymentEntity.PaymentDBEntity.Count == 0)
            {
                return 0;
            }

            if (paymentEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckUpdate(paymentEntity) > 0)
            {
                return 2;
            }

            List<CommandInfo> sqlList = new List<CommandInfo>();

            bool iflag = true;
            string PaymentID = "";
            foreach(PaymentDBEntity paymentDBEntity in paymentEntity.PaymentDBEntity)
            {
                if (iflag)
                {
                    PaymentEntity commonEntity = new PaymentEntity();
                    commonEntity.PaymentDBEntity = new List<PaymentDBEntity>();
                    PaymentDBEntity commonpaycodeDBEntity = new PaymentDBEntity();
                    commonpaycodeDBEntity.PaymentNo = paymentDBEntity.PaymentNo.ToString();
                    commonEntity.PaymentDBEntity.Add(commonpaycodeDBEntity);
                    DataSet dsResult = Select(commonEntity).QueryResult;
                    PaymentID = (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0) ? dsResult.Tables[0].Rows[0]["paymentcode"].ToString() : "";
                    iflag = false;
                }
                if (paymentDBEntity.UpdatType.Equals("1"))
                {
                    CommandInfo InsertCsMainPaymentInfo = new CommandInfo();
                    OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PAYMENTNM",OracleType.VarChar)                                 
                                };

                    lmParm[0].Value = int.Parse(paymentDBEntity.PaymentNo.ToString());
                    lmParm[1].Value = paymentDBEntity.Name_CN;

                    InsertCsMainPaymentInfo.SqlName = "Payment";
                    InsertCsMainPaymentInfo.SqlId = "t_lm_b_payment_update";
                    InsertCsMainPaymentInfo.Parameters = lmParm;
                    sqlList.Add(InsertCsMainPaymentInfo);
                }
                else
                {
                    CommandInfo InsertCsPaymentInfo = new CommandInfo();
                    OracleParameter[] csParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PAYMENTID",OracleType.VarChar),
                                    new OracleParameter("PLATFORM_ID",OracleType.VarChar),  
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)                                 
                                };

                    csParm[0].Value = (string.IsNullOrEmpty(paymentDBEntity.PaymentNo)) ? getMaxIDfromSeq("T_LM_B_PAYMENT_PLAT_SEQ") : int.Parse(paymentDBEntity.PaymentNo.ToString());
                    csParm[1].Value = PaymentID;
                    csParm[2].Value = paymentDBEntity.PaymFormID;
                    csParm[3].Value = paymentDBEntity.OnlineStatus;
                    InsertCsPaymentInfo.SqlName = "Payment";
                    InsertCsPaymentInfo.SqlId = "t_lm_b_payment_plat_update";
                    InsertCsPaymentInfo.Parameters = csParm;
                    sqlList.Add(InsertCsPaymentInfo);
                }
            }            

            DbManager.ExecuteSqlTran(sqlList);

            //OracleParameter[] parm ={
            //                        new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                 
            //                    };

            //PaymentDBEntity dbParm = (paymentEntity.PaymentDBEntity.Count > 0) ? paymentEntity.PaymentDBEntity[0] : new PaymentDBEntity();

            //parm[0].Value = dbParm.PaymentID;
            //parm[1].Value = dbParm.OnlineStatus;
            //DbManager.ExecuteSql("Payment", "t_cs_payment_update", parm);
            //DataCommand cmd = DataCommandManager.GetDataCommand("UpdateCityList");
            //foreach (PaymentDBEntity dbParm in paymentEntity.PaymentDBEntity)
            //{
            //    cmd.SetParameterValue("@ChannelNo", dbParm.ChannelNo);
            //    cmd.SetParameterValue("@ChannelID", dbParm.ChannelID);
            //    cmd.SetParameterValue("@NameCN", dbParm.Name_CN);
            //    cmd.SetParameterValue("@NameEN", PinyinHelper.GetPinyin(dbParm.Name_CN));
            //    cmd.SetParameterValue("@OnlineStatus", dbParm.OnlineStatus);
            //    cmd.SetParameterValue("@Remark", dbParm.Remark);
            //    cmd.SetParameterValue("@UpdateUser", (paymentEntity.LogMessages != null) ? paymentEntity.LogMessages.Userid : "");
            //    cmd.ExecuteNonQuery();
            //}
            return 1;
        }

        //通过sequence查询得到下一个ID值,数据库为Oracle
        public static int getMaxIDfromSeq(string sequencename)
        {
            int seqID = 1;
            string sql = "select " + sequencename + ".nextval from dual";
            object obj = DbHelperOra.GetSingle(sql, false);
            if (obj != null)
            {
                seqID = Convert.ToInt32(obj);
            }
            return seqID;
        }
    }
}