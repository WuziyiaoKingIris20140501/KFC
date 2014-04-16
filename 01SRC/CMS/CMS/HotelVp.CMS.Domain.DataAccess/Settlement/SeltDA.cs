using System;
using System.Collections;
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
    public abstract class SeltDA
    {
        public static SeltEntity ReviewSelectCount(SeltEntity seltEntity)
        {
            SeltDBEntity dbParm = (seltEntity.SeltDBEntity.Count > 0) ? seltEntity.SeltDBEntity[0] : new SeltDBEntity();
            OracleParameter[] parm ={
                                     new OracleParameter("UnitName",OracleType.VarChar), 
                                    new OracleParameter("CityID",OracleType.VarChar),
                                    new OracleParameter("InvoiceName",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.UnitNm))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UnitNm;
            }

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.InvoiceNm))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.InvoiceNm;
            }

            seltEntity.QueryResult = DbManager.Query("Settlement", "t_lm_settlement_unit_count", true, parm);
            return seltEntity;
        }

        public static SeltEntity ReviewSelect(SeltEntity seltEntity)
        {
            SeltDBEntity dbParm = (seltEntity.SeltDBEntity.Count > 0) ? seltEntity.SeltDBEntity[0] : new SeltDBEntity();
            OracleParameter[] parm ={
                                     new OracleParameter("UnitName",OracleType.VarChar), 
                                    new OracleParameter("CityID",OracleType.VarChar),
                                    new OracleParameter("InvoiceName",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.UnitNm))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UnitNm;
            }

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.InvoiceNm))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.InvoiceNm;
            }

            seltEntity.QueryResult = DbManager.Query("Settlement", "t_lm_settlement_unit_select", parm, (seltEntity.PageCurrent - 1) * seltEntity.PageSize, seltEntity.PageSize, true);
            
            return seltEntity;
        }

        public static SeltEntity ReLoadDetialInfo(SeltEntity seltEntity)
        {
            SeltDBEntity dbParm = (seltEntity.SeltDBEntity.Count > 0) ? seltEntity.SeltDBEntity[0] : new SeltDBEntity();
            OracleParameter[] parm ={
                                     new OracleParameter("UnitID",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.SeltID;
            DataSet dsResult = DbManager.Query("Settlement", "t_lm_settlement_unit_detail_select", true, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                dsResult.Tables[0].Rows[0]["settlement_sales_nm"] = UserSearchDA.GetSalesName(dsResult.Tables[0].Rows[0]["settlement_sales"].ToString().Trim());
            }

            seltEntity.QueryResult = dsResult;


            OracleParameter[] lmparm ={
                                     new OracleParameter("UnitID",OracleType.VarChar)
                                };
            lmparm[0].Value = dbParm.SeltID;
            
            DataTable dtHotelList = DbManager.Query("Settlement", "t_lm_settlement_unit_detail_hotel_select", true, lmparm).Tables[0];

            for (int i = 0; i < dtHotelList.Rows.Count; i++)
            {
                dtHotelList.Rows[i]["ODTYPE"] = ((dtHotelList.Rows[i]["PRICECD"].ToString().Trim().Substring(0, 1) == "1") ? "预付 " : "") + ((dtHotelList.Rows[i]["PRICECD"].ToString().Trim().Substring(1, 1) == "1") ? "现付" : "");
            }

            seltEntity.SeltDBEntity[0].dtHotelList = dtHotelList;
            return seltEntity;
        }

        public static SeltEntity SaveSettlementInfo(SeltEntity seltEntity)
        {
            SeltDBEntity dbParm = (seltEntity.SeltDBEntity.Count > 0) ? seltEntity.SeltDBEntity[0] : new SeltDBEntity();

            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo cminfo = new CommandInfo();
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar), 
                                    new OracleParameter("UNIT_NAME",OracleType.VarChar),
                                    new OracleParameter("INVOICE_NAME",OracleType.VarChar), 
                                    new OracleParameter("SETTLEMENT_TERM",OracleType.VarChar),
                                    new OracleParameter("HOTEL_TAX",OracleType.VarChar), 
                                    new OracleParameter("SETTLEMENT_ADDRESS",OracleType.VarChar),
                                    new OracleParameter("SETTLEMENT_PER",OracleType.VarChar), 
                                    new OracleParameter("SETTLEMENT_TEL",OracleType.VarChar),
                                    new OracleParameter("SETTLEMENT_FAX",OracleType.VarChar), 
                                    new OracleParameter("SETTLEMENT_SALES",OracleType.VarChar),
                                    new OracleParameter("BILL_ITEM",OracleType.VarChar), 
                                    new OracleParameter("HOTEL_TAXNO",OracleType.VarChar),
                                    new OracleParameter("HOTEL_PAYNO",OracleType.VarChar), 
                                    new OracleParameter("STATUS",OracleType.VarChar), 
                                    new OracleParameter("CREATE_USER",OracleType.VarChar), 
                                    new OracleParameter("TERM_STDT",OracleType.VarChar)
                                };

            string strSQL = "";
            if (String.IsNullOrEmpty(dbParm.SeltID))
            {
                parm[0].Value = getMaxIDfromSeq("T_LM_SETTLEMENT_UNIT_SEQ");
                strSQL = "t_lm_settlement_unit_insert";
            }
            else
            {
                parm[0].Value = dbParm.SeltID;
                strSQL = "t_lm_settlement_unit_update";
            }

            if (String.IsNullOrEmpty(dbParm.UnitNm))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.UnitNm;
            }

            if (String.IsNullOrEmpty(dbParm.InvoiceNm))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.InvoiceNm;
            }

            if (String.IsNullOrEmpty(dbParm.Term))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Term;
            }

            if (String.IsNullOrEmpty(dbParm.Tax))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.Tax;
            }

            if (String.IsNullOrEmpty(dbParm.Address))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.Address;
            }

            if (String.IsNullOrEmpty(dbParm.Per))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.Per;
            }

            if (String.IsNullOrEmpty(dbParm.Tel))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.Tel;
            }

            if (String.IsNullOrEmpty(dbParm.Fax))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = dbParm.Fax;
            }

            if (String.IsNullOrEmpty(dbParm.Sales))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = dbParm.Sales;
            }

            if (String.IsNullOrEmpty(dbParm.BillItem))
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = dbParm.BillItem;
            }

            if (String.IsNullOrEmpty(dbParm.TaxNo))
            {
                parm[11].Value = DBNull.Value;
            }
            else
            {
                parm[11].Value = dbParm.TaxNo;
            }

            if (String.IsNullOrEmpty(dbParm.PayNo))
            {
                parm[12].Value = DBNull.Value;
            }
            else
            {
                parm[12].Value = dbParm.PayNo;
            }

            parm[13].Value = dbParm.Status;
            parm[14].Value = seltEntity.LogMessages.Username;
            parm[15].Value = dbParm.TermStDt;

            cminfo.CommandText = XmlSqlAnalyze.GotSqlTextFromXml("Settlement", strSQL);
            cminfo.Parameters = parm;
            cmdList.Add(cminfo);




            CommandInfo cmUpinfo = new CommandInfo();
            OracleParameter[] upparm ={
                                    new OracleParameter("UNIT_ID",OracleType.VarChar), 
                                    new OracleParameter("CREATE_USER",OracleType.VarChar)
                                };

            upparm[0].Value = parm[0].Value;
            upparm[1].Value = seltEntity.LogMessages.Username;

            cmUpinfo.CommandText = XmlSqlAnalyze.GotSqlTextFromXml("Settlement", "t_lm_settlement_unit_hotel_update");
            cmUpinfo.Parameters = upparm;
            cmdList.Add(cmUpinfo);





            DataTable dtHotel = dbParm.dtHotelList;
            string strHotelSQL = XmlSqlAnalyze.GotSqlTextFromXml("Settlement", "t_lm_settlement_unit_hotel_save");
            for (int i = 0; i <= dtHotel.Rows.Count - 1; i++)
            {
                CommandInfo cmhinfo = new CommandInfo();
                cmhinfo.CommandText = strHotelSQL;
                OracleParameter[] lmParm ={
                                    new OracleParameter("UNIT_ID",OracleType.VarChar),
                                    new OracleParameter("HOTEL_ID",OracleType.VarChar),
                                    new OracleParameter("PRICE_CODE",OracleType.VarChar),
                                    new OracleParameter("VENDOR",OracleType.VarChar),
                                    new OracleParameter("STATUS",OracleType.VarChar),
                                    new OracleParameter("CREATE_USER",OracleType.VarChar)
                                };

                lmParm[0].Value = parm[0].Value;
                lmParm[1].Value = dtHotel.Rows[i]["HOTELID"].ToString();
                lmParm[2].Value = dtHotel.Rows[i]["PRICECD"].ToString();
                lmParm[3].Value = dtHotel.Rows[i]["VENDOR"].ToString();
                lmParm[4].Value = "1";
                lmParm[5].Value = seltEntity.LogMessages.Username;
                cmhinfo.Parameters = lmParm;
                cmdList.Add(cmhinfo);
            }

            DbHelperOra.ExecuteSqlTran(cmdList);
            seltEntity.Result = 1;
            
            return seltEntity;
        }

        private static string AppendString(string param)
        {
            string result = "";
            string[] temp = param.Split(',');

            foreach (string strTemp in temp)
            {
                result = (!String.IsNullOrEmpty(strTemp)) ? result + "'" + strTemp + "'," : result;
            }

            result = (result.Length > 1) ? result.Substring(0, result.Length - 1) : result;

            return result;
        }

        private static bool IsMobileNumber(string str_telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"(1[3,4,5,8][0-9])\d{8}$");
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