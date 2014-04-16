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
    public abstract class InvoiceDA
    {
        public static InvoiceEntity InvoiceListSelect(InvoiceEntity invoiceEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar),
                                    new OracleParameter("CNFNUM",OracleType.VarChar),
                                    new OracleParameter("SENDCODE",OracleType.VarChar),
                                    new OracleParameter("INVSTATUS",OracleType.VarChar),
                                    new OracleParameter("APPLYCHANEL",OracleType.VarChar),
                                    new OracleParameter("APPLYBEGINDATE",OracleType.VarChar),
                                    new OracleParameter("APPLYENDDATE",OracleType.VarChar),
                                    new OracleParameter("SENDBEGINDATE",OracleType.VarChar),
                                    new OracleParameter("SENDENDDATE",OracleType.VarChar),
                                };
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();

            if (String.IsNullOrEmpty(dbParm.USERID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.USERID;
            }

            if (String.IsNullOrEmpty(dbParm.CNFNUM))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.CNFNUM;
            }

            if (String.IsNullOrEmpty(dbParm.SENDCODE))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.SENDCODE;
            }

            if (String.IsNullOrEmpty(dbParm.Status))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Status;
            }

            if (String.IsNullOrEmpty(dbParm.APPLYCHANEL))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.APPLYCHANEL;
            }

            if (String.IsNullOrEmpty(dbParm.APPLYBEGINDATE))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.APPLYBEGINDATE;
            }

            if (String.IsNullOrEmpty(dbParm.APPLYENDDATE))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.APPLYENDDATE;
            }

            if (String.IsNullOrEmpty(dbParm.SENDBEGINDATE))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.SENDBEGINDATE;
            }

            if (String.IsNullOrEmpty(dbParm.SENDENDDATE))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = dbParm.SENDENDDATE;
            }

            string strSql = "";
            if ("0".Equals(dbParm.SelectType))
            {
                strSql = "t_lm_Invoice_all_top";
            }
            else
            {
                strSql = "t_lm_Invoice_all";
            }
            invoiceEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Invoice", strSql,false, parm);
            return invoiceEntity;
        }

        public static InvoiceEntity InvoiceListExcelSelect(InvoiceEntity invoiceEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar),
                                    new OracleParameter("CNFNUM",OracleType.VarChar),
                                    new OracleParameter("SENDCODE",OracleType.VarChar),
                                    new OracleParameter("INVSTATUS",OracleType.VarChar),
                                    new OracleParameter("APPLYCHANEL",OracleType.VarChar),
                                    new OracleParameter("APPLYBEGINDATE",OracleType.VarChar),
                                    new OracleParameter("APPLYENDDATE",OracleType.VarChar),
                                    new OracleParameter("SENDBEGINDATE",OracleType.VarChar),
                                    new OracleParameter("SENDENDDATE",OracleType.VarChar),
                                };
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();

            if (String.IsNullOrEmpty(dbParm.USERID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.USERID;
            }

            if (String.IsNullOrEmpty(dbParm.CNFNUM))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.CNFNUM;
            }

            if (String.IsNullOrEmpty(dbParm.SENDCODE))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.SENDCODE;
            }

            if (String.IsNullOrEmpty(dbParm.Status))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Status;
            }

            if (String.IsNullOrEmpty(dbParm.APPLYCHANEL))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.APPLYCHANEL;
            }

            if (String.IsNullOrEmpty(dbParm.APPLYBEGINDATE))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.APPLYBEGINDATE;
            }

            if (String.IsNullOrEmpty(dbParm.APPLYENDDATE))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.APPLYENDDATE;
            }

            if (String.IsNullOrEmpty(dbParm.SENDBEGINDATE))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.SENDBEGINDATE;
            }

            if (String.IsNullOrEmpty(dbParm.SENDENDDATE))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = dbParm.SENDENDDATE;
            }

            string strSql = "";
            if ("0".Equals(dbParm.SelectType))
            {
                strSql = "t_lm_Invoice_all_excel_top";
            }
            else
            {
                strSql = "t_lm_Invoice_all_excel";
            }

            invoiceEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Invoice", strSql, false, parm);
            return invoiceEntity;
        }

        public static InvoiceEntity InvoiceDetailSelect(InvoiceEntity invoiceEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();

            if (String.IsNullOrEmpty(dbParm.InvoiceID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.InvoiceID;
            }

            invoiceEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Invoice", "t_lm_Invoice_detail", false, parm);
            return invoiceEntity;
        }

        public static InvoiceEntity CommonTypeSelect(InvoiceEntity invoiceEntity)
        {
            invoiceEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Invoice", "t_lm_b_Invoice_type", false);
            return invoiceEntity;
        }

        public static int InvoiceUpdate(InvoiceEntity invoiceEntity)
        {
            if (invoiceEntity.InvoiceDBEntity.Count == 0)
            {
                return 0;
            }

            if (invoiceEntity.LogMessages == null)
            {
                return 0;
            }

            
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            string onStatus = dbParm.OnlineStatus;
            string ActionType = dbParm.ActionType.ToString();
            if ("0".Equals(ActionType))
            {
                string strSQL = "";
                if ("3".Equals(onStatus))
                {
                    onStatus = "2";
                    strSQL = "t_lm_b_Invoice_update_back_send";
                }
                else if ("2".Equals(onStatus))
                {
                    onStatus = "1";
                    strSQL = "t_lm_b_Invoice_update_back_invoice";
                }

                OracleParameter[] lmBackParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("OPERATOR",OracleType.VarChar),
                                    new OracleParameter("REMARK",OracleType.VarChar)
                                    
                                };

                lmBackParm[0].Value = dbParm.InvoiceID;
                lmBackParm[1].Value = onStatus;
                lmBackParm[2].Value = invoiceEntity.LogMessages.Username;
                lmBackParm[3].Value = dbParm.Remark;
                DbManager.ExecuteSql("Invoice", strSQL, lmBackParm);
            }
            else
            {
                if ("1".Equals(onStatus))
                {
                    if (!CheckInvoiceUpdate(invoiceEntity))
                    {
                        return 2;
                    }

                    OracleParameter[] lmSaveParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("INVOICENUM",OracleType.VarChar),
                                    new OracleParameter("OPERATOR",OracleType.VarChar),
                                    new OracleParameter("REMARK",OracleType.VarChar)
                                };

                    lmSaveParm[0].Value = dbParm.InvoiceID;
                    lmSaveParm[1].Value = dbParm.INVOICENUM;
                    lmSaveParm[2].Value = invoiceEntity.LogMessages.Username;
                    lmSaveParm[3].Value = dbParm.Remark;
                    DbManager.ExecuteSql("Invoice", "t_lm_b_Invoice_update_invoicenum", lmSaveParm);
                }
                else
                {
                    OracleParameter[] lmSendParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("SENDCODE",OracleType.VarChar),
                                    new OracleParameter("SENDNAME",OracleType.VarChar),
                                    new OracleParameter("OPERATOR",OracleType.VarChar),
                                    new OracleParameter("REMARK",OracleType.VarChar)
                                };

                    lmSendParm[0].Value = dbParm.InvoiceID;
                    lmSendParm[1].Value = dbParm.SENDCODE;
                    lmSendParm[2].Value = dbParm.SENDNAME;
                    lmSendParm[3].Value = invoiceEntity.LogMessages.Username;
                    lmSendParm[4].Value = dbParm.Remark;
                    DbManager.ExecuteSql("Invoice", "t_lm_b_Invoice_update_send", lmSendParm);
                }
            }
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

        private static bool CheckInvoiceUpdate(InvoiceEntity invoiceEntity)
        {
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            OracleParameter[] lmchkParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("INVOICENUM",OracleType.VarChar)
                                };

            lmchkParm[0].Value = dbParm.InvoiceID;
            lmchkParm[1].Value = dbParm.INVOICENUM;
            DataSet dsResult = DbManager.Query("Invoice", "t_lm_b_Invoice_update_invoicenum_check", false, lmchkParm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}