using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class APPContentDA
    {
        public static APPContentEntity CommonSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.QueryResult = DbManager.Query("APPContent", "t_lm_b_city", false);
            return appcontentEntity;
        }

        public static APPContentEntity CommonPlatSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.QueryResult = DbManager.Query("APPContent", "t_lm_b_platform", false);
            return appcontentEntity;
        }

        public static APPContentEntity SalesMangeDetialSelect(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("SalesMangeDetialSelect");
            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                cmd.SetParameterValue("@UserCode", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@UserCode", dbParm.UserCode);
            }
            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SalesRoleID"])) ? "5" : ConfigurationManager.AppSettings["SalesRoleID"].ToString().Trim();
            cmd.SetParameterValue("@RoleID", RoleID);
            DataSet dsResult = cmd.ExecuteDataSet();
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity SalesMangeListSelectCount(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("SalesMangeListSelectCount");
            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                cmd.SetParameterValue("@UserCode", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@UserCode", dbParm.UserCode);
            }

            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SalesRoleID"])) ? "5" : ConfigurationManager.AppSettings["SalesRoleID"].ToString().Trim();
            cmd.SetParameterValue("@RoleID", RoleID);
            //if (!String.IsNullOrEmpty(dbParm.HotelNM))
            //{
            //    if (String.IsNullOrEmpty(dbParm.AccountList))
            //    {
            //        cmd.SetParameterValue("@AccountList", DBNull.Value);
            //    }
            //    else
            //    {
            //        cmd.SetParameterValue("@AccountList", dbParm.AccountList);
            //    }
            //}
            DataSet dsResult = cmd.ExecuteDataSet();
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity SalesMangeListSelect(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("SalesMangeListSelect");
            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                cmd.SetParameterValue("@UserCode", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@UserCode", dbParm.UserCode);
            }

            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SalesRoleID"])) ? "5" : ConfigurationManager.AppSettings["SalesRoleID"].ToString().Trim();
            cmd.SetParameterValue("@RoleID", RoleID);
            cmd.SetParameterValue("@PageCurrent", appcontentEntity.PageCurrent - 1);
            cmd.SetParameterValue("@PageSize", appcontentEntity.PageSize);

            DataSet dsResult = cmd.ExecuteDataSet();
            appcontentEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity OrderSettleMangeListSelect(APPContentEntity appcontentEntity)
        {
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", "t_lm_order_settle_search_list");
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("USERCODE",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.UserCode;
            }

            string whereString = string.Empty;
            string months = dbParm.Months;
            if (months.Contains("3"))
            {
                whereString = whereString + "(dt.out_date >= TO_DATE('2013-03-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-04-01','yyyy-mm-dd'))";
            }

            if (months.Contains("4"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-04-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-05-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-04-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-05-01','yyyy-mm-dd'))";
            }

            if (months.Contains("5"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-05-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-06-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-05-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-06-01','yyyy-mm-dd'))";
            }

            if (months.Contains("6"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-06-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-07-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-06-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-07-01','yyyy-mm-dd'))";
            }

            if (months.Contains("7"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-07-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-08-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-07-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-08-01','yyyy-mm-dd'))";
            }

            if (months.Contains("8"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-08-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-09-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-08-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-09-01','yyyy-mm-dd'))";
            }

            if (months.Contains("9"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-09-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-10-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-09-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-10-01','yyyy-mm-dd'))";
            }

            if (months.Contains("10"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-10-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-11-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-10-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-11-01','yyyy-mm-dd'))";
            }

            if (months.Contains("11"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-11-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-12-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-11-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-12-01','yyyy-mm-dd'))";
            }

            if (months.Contains("12"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-12-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2014-01-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-12-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2014-01-01','yyyy-mm-dd'))";
            }

            SqlString = whereString.Length > 0 ? SqlString + "AND (" + whereString + ")" : SqlString;
            SqlString = SqlString + "group by dt.hotel_id,bp.prop_name_zh,tsh.sales_account,sett.months  order by SUM(dt.COMS) desc";

            DataSet dsResult = DbManager.Query(SqlString, parm, (appcontentEntity.PageCurrent - 1) * appcontentEntity.PageSize, appcontentEntity.PageSize, true);
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity ExportOrderSettleMangeList(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("USERCODE",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", "t_lm_order_settle_search_export");
            string whereString = string.Empty;
            string months = dbParm.Months;
            if (months.Contains("3"))
            {
                whereString = whereString + "(dt.out_date >= TO_DATE('2013-03-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-04-01','yyyy-mm-dd'))";
            }

            if (months.Contains("4"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-04-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-05-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-04-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-05-01','yyyy-mm-dd'))";
            }

            if (months.Contains("5"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-05-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-06-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-05-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-06-01','yyyy-mm-dd'))";
            }

            if (months.Contains("6"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-06-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-07-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-06-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-07-01','yyyy-mm-dd'))";
            }

            if (months.Contains("7"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-07-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-08-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-07-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-08-01','yyyy-mm-dd'))";
            }

            if (months.Contains("8"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-08-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-09-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-08-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-09-01','yyyy-mm-dd'))";
            }

            if (months.Contains("9"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-09-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-10-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-09-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-10-01','yyyy-mm-dd'))";
            }

            if (months.Contains("10"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-10-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-11-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-10-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-11-01','yyyy-mm-dd'))";
            }

            if (months.Contains("11"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-11-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-12-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-11-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-12-01','yyyy-mm-dd'))";
            }

            if (months.Contains("12"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-12-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2014-01-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-12-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2014-01-01','yyyy-mm-dd'))";
            }
            SqlString = whereString.Length > 0 ? SqlString + "AND (" + whereString + ")" : SqlString;
            SqlString = SqlString + "order by dt.out_date desc";
            DataSet dsResult = DbManager.Query(SqlString, parm, 0, 10000000, true);
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity OrderSettleMangeListCount(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("USERCODE",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.UserCode;
            }

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", "t_lm_order_settle_search_list");

            string whereString = string.Empty;
            string months = dbParm.Months;
            if (months.Contains("3"))
            {
                whereString = whereString + "(dt.out_date >= TO_DATE('2013-03-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-04-01','yyyy-mm-dd'))";
            }

            if (months.Contains("4"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-04-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-05-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-04-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-05-01','yyyy-mm-dd'))";
            }

            if (months.Contains("5"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-05-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-06-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-05-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-06-01','yyyy-mm-dd'))";
            }

            if (months.Contains("6"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-06-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-07-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-06-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-07-01','yyyy-mm-dd'))";
            }

            if (months.Contains("7"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-07-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-08-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-07-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-08-01','yyyy-mm-dd'))";
            }

            if (months.Contains("8"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-08-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-09-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-08-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-09-01','yyyy-mm-dd'))";
            }

            if (months.Contains("9"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-09-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-10-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-09-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-10-01','yyyy-mm-dd'))";
            }

            if (months.Contains("10"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-10-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-11-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-10-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-11-01','yyyy-mm-dd'))";
            }

            if (months.Contains("11"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-11-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-12-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-11-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2013-12-01','yyyy-mm-dd'))";
            }

            if (months.Contains("12"))
            {
                whereString = whereString.Length > 0 ? whereString + " OR (dt.out_date >= TO_DATE('2013-12-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2014-01-01','yyyy-mm-dd'))" : whereString + " (dt.out_date >= TO_DATE('2013-12-01','yyyy-mm-dd') AND dt.out_date < TO_DATE('2014-01-01','yyyy-mm-dd'))";
            }
            SqlString = whereString.Length > 0 ? SqlString + "AND (" + whereString + ")" : SqlString;
            SqlString = SqlString + "group by dt.hotel_id,bp.prop_name_zh,tsh.sales_account,sett.months  order by SUM(dt.COMS) desc";

            DataSet dsResult = DbManager.Query(SqlString, parm, 0, 10000000, true);
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity SaveOrderSettleMangeList(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("MONTHS",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.Months;
            parm[2].Value = appcontentEntity.LogMessages.Username;
            DbManager.ExecuteSql("OrderSearch", "t_lm_order_settle_save", parm);
            appcontentEntity.Result = 1;
            return appcontentEntity;
        }


        public static APPContentEntity SalesPopGridSelect(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SalesMangeHotelListSelect");
            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                cmd.SetParameterValue("@UserCode", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@UserCode", dbParm.UserCode);
            }
            DataSet dsHotelList = cmd.ExecuteDataSet();
            DataSet dsResult = new DataSet();

            if (dsHotelList.Tables.Count > 0 && dsHotelList.Tables[0].Rows.Count > 0)
            {
                string strHotelID = string.Empty;
                foreach (DataRow drRow in dsHotelList.Tables[0].Rows)
                {
                    strHotelID = strHotelID + drRow["HOTELID"].ToString() + ",";
                }
                OracleParameter[] parm ={
                                    new OracleParameter("PROPS",OracleType.VarChar)
                                };
                parm[0].Value = strHotelID;
                dsResult = DbManager.Query("APPContent", "t_lm_b_prop_infos_select", false, parm);

                for (int i = 0; i < dsHotelList.Tables[0].Rows.Count; i++)
                {
                    DataRow[] drTemp = dsResult.Tables[0].Select("PROP = '" + dsHotelList.Tables[0].Rows[i]["HOTELID"].ToString() + "'");
                    dsHotelList.Tables[0].Rows[i]["HOTELNM"] = (drTemp.Count() > 0) ? drTemp[0]["PROPNM"].ToString() : "";
                    dsHotelList.Tables[0].Rows[i]["STARNM"] = (drTemp.Count() > 0) ? SetStarValue(drTemp[0]["DIAHOTEL"].ToString(), drTemp[0]["STAR"].ToString()) : "";
                    dsHotelList.Tables[0].Rows[i]["AREANM"] = (drTemp.Count() > 0) ? drTemp[0]["AREANM"].ToString() : "";
                }
            }

            appcontentEntity.QueryResult = dsHotelList;
            return appcontentEntity;
        }

        private static string SetStarValue(string DiaHotel, string Star)
        {
            string val = string.Empty;
            if ("1".Equals(DiaHotel))
            {
                val = "准";
            }

            if (!chkNum(Star) || (chkNum(Star) && int.Parse(Star) < 3))
            {
                val = "经济";
            }
            else
            {
                val = val + Star;
            }

            return val;
        }
        private static bool chkNum(string parm)
        {
            try
            {
                int.Parse(parm);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DataSet HotelLinkSelect(APPContentEntity appcontentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PROP",OracleType.VarChar)
                                };
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            parm[0].Value = dbParm.HotelID;

            return DbManager.Query("APPContent", "t_lm_b_prop_link", true, parm);
        }

        public static string HotelRoomNM(string hotelId, string roomCode)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMID",OracleType.VarChar)
                                };

            parm[0].Value = hotelId;
            parm[1].Value = roomCode;

            DataSet dsResult = DbManager.Query("APPContent", "t_lm_hotelroom_name", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["HOTELROOMNM"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static ArrayList GetHotelIgnore(string HotelID)
        {
            ArrayList alIgnore = new ArrayList();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetHotelIgnore");
            cmd.SetParameterValue("@HotelID", HotelID);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drRow in dsResult.Tables[0].Rows)
                {
                    if (!alIgnore.Contains(drRow[0].ToString().Trim()))
                    {
                        alIgnore.Add(drRow[0].ToString());
                    }
                }
            }
            return alIgnore;
        }

        public static ArrayList GetHotelCompare(string HotelID)
        {
            ArrayList alIgnore = new ArrayList();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetHotelCompare");
            cmd.SetParameterValue("@HotelID", HotelID);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drRow in dsResult.Tables[0].Rows)
                {
                    if (!alIgnore.Contains(drRow[0].ToString().Trim()))
                    {
                        alIgnore.Add(drRow[0].ToString());
                    }
                }
            }
            return alIgnore;
        }

        public static APPContentEntity GetHotelFogList(APPContentEntity appcontentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PROP",OracleType.VarChar)
                                };
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HotelID;
            }

            appcontentEntity.QueryResult = DbManager.Query("APPContent", "t_lm_b_prop_fog_compare", false, parm);
            return appcontentEntity;
        }

        public static APPContentEntity PopGridSelect(APPContentEntity appcontentEntity)
        {
            DataCommand cmdHotel = DataCommandManager.GetDataCommand("GetHotelIgnoreList");
            DataSet dsHotelResult = cmdHotel.ExecuteDataSet();
            string strHotelLIst = "";
            foreach (DataRow drRow in dsHotelResult.Tables[0].Rows)
            {
                strHotelLIst = strHotelLIst + drRow[0].ToString() + ",";
            }

            DataCommand cmd = DataCommandManager.GetDataCommand("GetHotelPopIgnore");
            DataSet dsResult = cmd.ExecuteDataSet();

            if (strHotelLIst.Length > 0)
            {
                DataSet dsHotelNM = HotelLinkSelect(strHotelLIst);

                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < dsHotelNM.Tables[0].Rows.Count; j++)
                    {
                        if (dsResult.Tables[0].Rows[i]["HOTELID"].ToString().Trim().Equals(dsHotelNM.Tables[0].Rows[j]["PROP"].ToString().Trim()))
                        {
                            dsResult.Tables[0].Rows[i]["HOTELNM"] = dsHotelNM.Tables[0].Rows[j]["PROPNM"].ToString().Trim();
                        }
                    }
                }
            }

            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static APPContentEntity PopHotelGridSelect(APPContentEntity appcontentEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetHotelPopCompare");
            DataSet dsResult = cmd.ExecuteDataSet();

            DataCommand cmdHotel = DataCommandManager.GetDataCommand("GetHotelCompareList");
            DataSet dsHotelResult = cmdHotel.ExecuteDataSet();
            string strHotelLIst = "";
            int iCount = 0;
            int iMaxCount = 200;
            foreach (DataRow drRow in dsHotelResult.Tables[0].Rows)
            {
                strHotelLIst = strHotelLIst + drRow[0].ToString() + ",";
                iCount = iCount + 1;
                if (iCount == iMaxCount && strHotelLIst.Length > 0)
                {
                    DataSet dsHotelNM = HotelLinkSelect(strHotelLIst);
                    for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < dsHotelNM.Tables[0].Rows.Count; j++)
                        {
                            if (dsResult.Tables[0].Rows[i]["HOTELID"].ToString().Trim().Equals(dsHotelNM.Tables[0].Rows[j]["PROP"].ToString().Trim()))
                            {
                                dsResult.Tables[0].Rows[i]["HOTELNM"] = dsHotelNM.Tables[0].Rows[j]["PROPNM"].ToString().Trim();
                            }
                        }
                    }
                    strHotelLIst = "";
                    iCount = 0;
                }
            }

            if (strHotelLIst.Length > 0)
            {
                DataSet dsHotelNM = HotelLinkSelect(strHotelLIst);

                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < dsHotelNM.Tables[0].Rows.Count; j++)
                    {
                        if (dsResult.Tables[0].Rows[i]["HOTELID"].ToString().Trim().Equals(dsHotelNM.Tables[0].Rows[j]["PROP"].ToString().Trim()))
                        {
                            dsResult.Tables[0].Rows[i]["HOTELNM"] = dsHotelNM.Tables[0].Rows[j]["PROPNM"].ToString().Trim();
                        }
                    }
                }
            }
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static DataSet HotelLinkSelect(string PROPS)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PROPS",OracleType.VarChar)
                                };
            parm[0].Value = PROPS;

            return DbManager.Query("APPContent", "t_lm_b_prop_names_select", false, parm);
        }

        public static APPContentEntity GetFullRoomHistoryList(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("STARTDT",OracleType.VarChar),
                                    new OracleParameter("ENDDT",OracleType.VarChar)
                                };
            string hotelID = dbParm.HotelID.Substring((dbParm.HotelID.IndexOf('[') + 1), (dbParm.HotelID.IndexOf(']') - 1));
            parm[0].Value = hotelID;
            parm[1].Value = dbParm.StartDTime;
            parm[2].Value = dbParm.EndDTime;

            DataSet dsResult = new DataSet();
            DataSet dsTemp = DbManager.Query("APPContent", "t_lm_b_plan_history_select", false, parm);

            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("EVENT");
            dsResult.Tables[0].Columns.Add("EVENTTM");
            dsResult.Tables[0].Columns.Add("EFFECTDATE");
            dsResult.Tables[0].Columns.Add("ROOMNM");
            dsResult.Tables[0].Columns.Add("PRICECODE");
            dsResult.Tables[0].Columns.Add("STATUSDIS");
            dsResult.Tables[0].Columns.Add("GUAID");
            dsResult.Tables[0].Columns.Add("CXLID");
            dsResult.Tables[0].Columns.Add("ROOMNUM");
            dsResult.Tables[0].Columns.Add("ISRESERVE");
            dsResult.Tables[0].Columns.Add("HOLDROOMNUM");
            dsResult.Tables[0].Columns.Add("TWOPRICE");
            dsResult.Tables[0].Columns.Add("BREAKFASTNUM");
            dsResult.Tables[0].Columns.Add("ISNETWORK");
            dsResult.Tables[0].Columns.Add("OFFSETVAL");
            dsResult.Tables[0].Columns.Add("OFFSETUNIT");


            if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drTemp in dsTemp.Tables[0].Rows)
                {
                    string[] strTemp = drTemp["ope_msg"].ToString().Trim('{').Trim('}').Split(',');
                    DataRow drRow = dsResult.Tables[0].NewRow();
                    foreach (string strValList in strTemp)
                    {
                        string[] strVal = strValList.Split('=');
                        if (strVal.Count() > 0)
                        {
                            switch (strVal[0].Trim())
                            {
                                case "effectDate":
                                    drRow["EFFECTDATE"] = strVal[1].ToString().Trim();
                                    break;

                                case "roomTypeCode":
                                    drRow["ROOMNM"] = drRow["ROOMNM"].ToString() + "[" + strVal[1].ToString().Trim() + "]";
                                    break;

                                case "roomTypeName":
                                    drRow["ROOMNM"] = drRow["ROOMNM"].ToString() + strVal[1].ToString().Trim();
                                    break;

                                case "rateCode":
                                    drRow["PRICECODE"] = strVal[1].ToString().Trim();
                                    break;

                                case "status":
                                    drRow["STATUSDIS"] = "true".Equals(strVal[1].ToString().Trim()) ? "可用" : "关闭";
                                    break;

                                case "guaid":
                                    drRow["GUAID"] = strVal[1].ToString().Trim();
                                    break;

                                case "cxlid":
                                    drRow["CXLID"] = strVal[1].ToString().Trim();
                                    break;

                                case "roomNum":
                                    drRow["ROOMNUM"] = strVal[1].ToString().Trim();
                                    break;

                                case "isReserve":
                                    drRow["ISRESERVE"] = "0".Equals(strVal[1].ToString().Trim()) ? "非保留" : "保留";
                                    break;

                                case "holdRoomNum":
                                    drRow["HOLDROOMNUM"] = strVal[1].ToString().Trim();
                                    break;

                                case "twoPrice":
                                    drRow["TWOPRICE"] = strVal[1].ToString().Trim();
                                    break;

                                case "breakfastNum":
                                    drRow["BREAKFASTNUM"] = strVal[1].ToString().Trim();
                                    break;

                                case "isNetwork":
                                    drRow["ISNETWORK"] = "0".Equals(strVal[1].ToString().Trim()) ? "无" : "有";
                                    break;

                                case "offsetval":
                                    drRow["OFFSETVAL"] = strVal[1].ToString().Trim();
                                    break;

                                case "offsetunit":
                                    drRow["OFFSETUNIT"] = "0".Equals(strVal[1].ToString().Trim()) ? "整数" : "百分数";
                                    break;

                                default:
                                    break;
                            }

                        }
                    }

                    drRow["EVENT"] = drTemp["ope_memo"].ToString();
                    drRow["EVENTTM"] = drTemp["ope_time"].ToString();
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }

            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }

        public static int InsertHotelIgnoreGrid(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            if (!CheckInsert(appcontentEntity))
            {
                return 2;
            }

            DataCommand cmd = DataCommandManager.GetDataCommand("InsertHotelIgnoreGrid");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@TypeID", dbParm.TypeID);
            cmd.SetParameterValue("@UserID", appcontentEntity.LogMessages.Username);
            return cmd.ExecuteNonQuery();
        }

        public static APPContentEntity InsertSalesMangeGrid(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            string Error = CheckSalesInsert(appcontentEntity);
            if (!String.IsNullOrEmpty(Error))
            {
                appcontentEntity.Result = 2;
                appcontentEntity.ErrorMSG = Error;
                return appcontentEntity;
            }

            DataCommand cmd = DataCommandManager.GetDataCommand("InsertSalesMangeGrid");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@UserCode", dbParm.UserCode);
            cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);
            cmd.SetParameterValue("@CreateUser", appcontentEntity.LogMessages.Username);

            DataSet dsHotelInfo = GetBindHotelInfo(dbParm.HotelID);
            if (dsHotelInfo.Tables.Count > 0 && dsHotelInfo.Tables[0].Rows.Count > 0)
            {
                cmd.SetParameterValue("@Fax", dsHotelInfo.Tables[0].Rows[0]["FAX"].ToString());
                cmd.SetParameterValue("@Per", dsHotelInfo.Tables[0].Rows[0]["CONTACTPER"].ToString());
                cmd.SetParameterValue("@Tel", dsHotelInfo.Tables[0].Rows[0]["PHONE"].ToString());
                cmd.SetParameterValue("@Email", dsHotelInfo.Tables[0].Rows[0]["CONTACTEMAIL"].ToString());
            }
            else
            {
                cmd.SetParameterValue("@Fax", DBNull.Value);
                cmd.SetParameterValue("@Per", DBNull.Value);
                cmd.SetParameterValue("@Tel", DBNull.Value);
                cmd.SetParameterValue("@Email", DBNull.Value);
            }

            cmd.ExecuteNonQuery();
            //HotelInfoDA.UpdateHotelSalesUserAccount(dbParm.HotelID, dbParm.UserCode);
            UpdateLMSalesHistory(appcontentEntity);
            appcontentEntity.Result = 1;
            return appcontentEntity;
        }

        public static int UpdateLMSalesHistory(APPContentEntity appcontentEntity)
        {
            if (appcontentEntity.APPContentDBEntity.Count == 0)
            {
                return 0;
            }

            if (appcontentEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckHistoryUpdate(appcontentEntity))
            {
                DeleteLMSalesHistory(appcontentEntity);
            }

            InsertLMSalesHistory(appcontentEntity);
            return 1;
        }

        public static bool CheckHistoryUpdate(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                dlmParm[1].Value = DBNull.Value;
            }
            else
            {
                dlmParm[1].Value = dbParm.UserCode;
            }
            DataSet dsResult = DbManager.Query("HotelInfo", "t_lm_sales_history_select", false, dlmParm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static int InsertLMSalesHistory(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            lmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                lmParm[1].Value = DBNull.Value;
            }
            else
            {
                lmParm[1].Value = dbParm.UserCode;
            }

            DbManager.ExecuteSql("HotelInfo", "t_lm_sales_history_save", lmParm);
            return 1;
        }

        public static int DeleteLMSalesHistory(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.UserCode))
            {
                dlmParm[1].Value = DBNull.Value;
            }
            else
            {
                dlmParm[1].Value = dbParm.UserCode;
            }

            DbManager.ExecuteSql("HotelInfo", "t_lm_sales_history_delete", dlmParm);
            return 1;
        }

        public static DataSet GetBindHotelInfo(string HotelID)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            parm[0].Value = HotelID;
            return HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelinfo_bind", false, parm);
        }

        //public static APPContentEntity ReviewSalesPlanCount(APPContentEntity appcontentEntity)
        //{
        //    APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
        //    DataCommand cmd = DataCommandManager.GetDataCommand("ReviewSalesPlanCount");
        //    cmd.SetParameterValue("@HotelID", dbParm.HotelID);
        //    cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
        //    cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);
        //    appcontentEntity.QueryResult = cmd.ExecuteDataSet();
        //    return appcontentEntity;
        //}

        public static APPContentEntity ReviewSalesPlanDetail(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ReviewSalesPlanDetail");
            cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            appcontentEntity.QueryResult = cmd.ExecuteDataSet();
            return appcontentEntity;
        }

        public static APPContentEntity ChkHotelLowLimitPrice(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;
            appcontentEntity.QueryResult = DbManager.Query("HotelInfo", "t_fog_low_limit_select", false, dlmParm);
            return appcontentEntity;
        }

        public static DataSet ReviewSalesPlanDetailHistory(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ReviewSalesPlanDetailHistory");
            cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            return cmd.ExecuteDataSet();
        }

        public static APPContentEntity UpdateSalesPlanStatus(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanDetailStatus");
            cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            cmd.SetParameterValue("@Status", dbParm.PlanStatus);
            cmd.SetParameterValue("@Update_User", appcontentEntity.LogMessages.Username);
            appcontentEntity.Result = cmd.ExecuteNonQuery();
            return appcontentEntity;
        }

        public static APPContentEntity UpdateSalesPlanManager(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanManager");

            cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            cmd.SetParameterValue("@Status", dbParm.PlanStatus);
            cmd.SetParameterValue("@Type", dbParm.SaveType);
            if ("0".Equals(dbParm.SaveType.Trim()))
            {
                cmd.SetParameterValue("@Plan_Time", DBNull.Value);
                cmd.SetParameterValue("@Plan_DTime", DBNull.Value);
                cmd.SetParameterValue("@Start_Dtime", DBNull.Value);
                cmd.SetParameterValue("@End_Dtime", DBNull.Value);
                cmd.SetParameterValue("@Week_List", DBNull.Value);
            }
            else if ("1".Equals(dbParm.SaveType.Trim()))
            {
                cmd.SetParameterValue("@Plan_Time", DBNull.Value);
                cmd.SetParameterValue("@Plan_DTime", dbParm.PlanDTime);
                cmd.SetParameterValue("@Start_Dtime", DBNull.Value);
                cmd.SetParameterValue("@End_Dtime", DBNull.Value);
                cmd.SetParameterValue("@Week_List", DBNull.Value);
            }
            else if ("2".Equals(dbParm.SaveType.Trim()))
            {
                cmd.SetParameterValue("@Plan_Time", dbParm.PlanTime);
                cmd.SetParameterValue("@Plan_DTime", DBNull.Value);
                cmd.SetParameterValue("@Start_Dtime", dbParm.PlanStart);
                cmd.SetParameterValue("@End_Dtime", dbParm.PlanEnd);
                cmd.SetParameterValue("@Week_List", dbParm.PlanWeek);
            }
            cmd.SetParameterValue("@Update_User", appcontentEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            return appcontentEntity;
        }

        public static int UpdateSalesPlanEventDetail(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanEventDetail");
            cmd.SetParameterValue("@HPID", dbParm.PlanID);
            cmd.SetParameterValue("@Start_Dtime", dbParm.StartDTime);
            cmd.SetParameterValue("@End_Dtime", dbParm.EndDTime);
            cmd.SetParameterValue("@EffHour", dbParm.EffHour);
            cmd.SetParameterValue("@Week_List", dbParm.WeekList);
            cmd.SetParameterValue("@HOTEL_ID", dbParm.HotelID);
            cmd.SetParameterValue("@RATE_CODE", dbParm.PriceCode);
            cmd.SetParameterValue("@MONEY_TYPE", "CHY");
            cmd.SetParameterValue("@GUAID", dbParm.Note1);
            cmd.SetParameterValue("@CXLID", dbParm.Note2);
            cmd.SetParameterValue("@ROOM_TYPE_NAME", dbParm.RoomName);
            cmd.SetParameterValue("@ROOM_TYPE_CODE", dbParm.RoomCode);
            cmd.SetParameterValue("@STATUS", dbParm.RoomStatus);

            if (String.IsNullOrEmpty(dbParm.RoomCount))
            {
                cmd.SetParameterValue("@ROOM_NUM", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@ROOM_NUM", dbParm.RoomCount);
            }
            cmd.SetParameterValue("@IS_RESERVE", dbParm.IsReserve);

            if (String.IsNullOrEmpty(dbParm.OnePrice))
            {
                cmd.SetParameterValue("@ONE_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@ONE_PRICE", dbParm.OnePrice);
            }

            if (String.IsNullOrEmpty(dbParm.TwoPrice))
            {
                cmd.SetParameterValue("@TWO_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@TWO_PRICE", dbParm.TwoPrice);
            }

            if (String.IsNullOrEmpty(dbParm.ThreePrice))
            {
                cmd.SetParameterValue("@THREE_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@THREE_PRICE", dbParm.ThreePrice);
            }

            if (String.IsNullOrEmpty(dbParm.FourPrice))
            {
                cmd.SetParameterValue("@FOUR_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@FOUR_PRICE", dbParm.FourPrice);
            }

            if (String.IsNullOrEmpty(dbParm.BedPrice))
            {
                cmd.SetParameterValue("@ATTN_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@ATTN_PRICE", dbParm.BedPrice);
            }

            if (String.IsNullOrEmpty(dbParm.BreakfastNum))
            {
                cmd.SetParameterValue("@BREAKFAST_NUM", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@BREAKFAST_NUM", dbParm.BreakfastNum);
            }

            if (String.IsNullOrEmpty(dbParm.BreakPrice))
            {
                cmd.SetParameterValue("@EACH_BREAKFAST_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EACH_BREAKFAST_PRICE", dbParm.BreakPrice);
            }

            cmd.SetParameterValue("@IS_NETWORK", dbParm.IsNetwork);

            if (String.IsNullOrEmpty(dbParm.Offsetval))
            {
                cmd.SetParameterValue("@OFFSETVAL", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@OFFSETVAL", dbParm.Offsetval);
            }

            cmd.SetParameterValue("@OFFSETUNIT", dbParm.Offsetunit);
            cmd.SetParameterValue("@Update_User", appcontentEntity.LogMessages.Username);

            if (String.IsNullOrEmpty(dbParm.NetPrice))
            {
                cmd.SetParameterValue("@NETPRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@NETPRICE", dbParm.NetPrice);
            }

            cmd.SetParameterValue("@SOURCE", dbParm.Supplier);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public static APPContentEntity ReviewSalesPlan(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ReviewSalesPlan");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);
            cmd.SetParameterValue("@PageCurrent", appcontentEntity.PageCurrent - 1);
            cmd.SetParameterValue("@PageSize", appcontentEntity.PageSize);

            appcontentEntity.QueryResult = cmd.ExecuteDataSet();
            appcontentEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            return appcontentEntity;
        }

        public static int InsertHotelCompareGrid(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            if (!CheckInsert(appcontentEntity))
            {
                return 2;
            }

            DataCommand cmd = DataCommandManager.GetDataCommand("InsertHotelCompareGrid");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@TypeID", dbParm.TypeID);
            cmd.SetParameterValue("@UserID", appcontentEntity.LogMessages.Username);
            return cmd.ExecuteNonQuery();
        }

        public static int InsertHotelCompareGridBatch(APPContentEntity appcontentEntity)
        {
            foreach (APPContentDBEntity dbParm in appcontentEntity.APPContentDBEntity)
            {
                DataCommand cmd = DataCommandManager.GetDataCommand("InsertHotelCompareGrid");
                cmd.SetParameterValue("@HotelID", dbParm.HotelID);
                cmd.SetParameterValue("@TypeID", dbParm.TypeID);
                cmd.SetParameterValue("@UserID", appcontentEntity.LogMessages.Username);
                cmd.ExecuteNonQuery();
            }
            return 1;
        }

        public static int UpdateHotelCompareGridBatch(APPContentEntity appcontentEntity)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            string strSql = "";
            foreach (APPContentDBEntity dbParm in appcontentEntity.APPContentDBEntity)
            {
                CommandInfo UpdateHubsInfo = new CommandInfo();
                OracleParameter[] lmInfoParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
                lmInfoParm[0].Value = dbParm.HotelID;
                UpdateHubsInfo.SqlName = "APPContent";
                switch (dbParm.TypeID)
                {
                    case "HOTELNMZH":
                        strSql = "t_lm_b_prop_update_nm";
                        break;
                    case "HOTELNMEN":
                        strSql = "t_lm_b_prop_update_nmen";
                        break;
                    case "FOGSTATUS":
                        strSql = "t_lm_b_prop_update_status";
                        break;
                    case "CITY":
                        strSql = "t_lm_b_prop_update_city";
                        break;
                    case "DIAMOND":
                        strSql = "t_lm_b_prop_update_diamond";
                        break;
                    case "STAR":
                        strSql = "t_lm_b_prop_update_star";
                        break;
                    case "OPENDT":
                        strSql = "t_lm_b_prop_update_open";
                        break;
                    case "RENOVATIONDT":
                        strSql = "t_lm_b_prop_update_ren";
                        break;
                    case "TRADEAREA":
                        strSql = "t_lm_b_prop_update_tra";
                        break;
                    case "ADDRESS":
                        strSql = "t_lm_b_prop_update_address";
                        break;
                    case "WEBSITE":
                        strSql = "t_lm_b_prop_update_web";
                        break;
                    case "LINKTEL":
                        strSql = "t_lm_b_prop_update_tel";
                        break;
                    case "LINKFAX":
                        strSql = "t_lm_b_prop_update_fax";
                        break;
                    case "LINKMAN":
                        strSql = "t_lm_b_prop_update_man";
                        break;
                    case "LINKMAIL":
                        strSql = "t_lm_b_prop_update_mail";
                        break;
                    case "LONGITUDE":
                        strSql = "t_lm_b_prop_update_long";
                        break;
                    case "LATITUDE":
                        strSql = "t_lm_b_prop_update_lati";
                        break;
                    case "DESCZH":
                        strSql = "t_lm_b_prop_update_desc";
                        break;
                    default:
                        strSql = "";
                        break;
                }

                UpdateHubsInfo.SqlId = strSql;
                UpdateHubsInfo.Parameters = lmInfoParm;
                sqlList.Add(UpdateHubsInfo);
            }

            DbManager.ExecuteSqlTran(sqlList);
            return 1;
        }

        public static string CheckSalesInsert(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("CheckInsertSalesManager");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["USERNM"].ToString();
            }
            return "";
        }

        public static bool CheckInsert(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("CheckInsertHotelIgnore");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@TypeID", dbParm.TypeID);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public static int DeleteHotelIgnoreGrid(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("DeleteHotelIgnoreGrid");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@TypeID", dbParm.TypeID);
            cmd.SetParameterValue("@UserID", appcontentEntity.LogMessages.Username);
            return cmd.ExecuteNonQuery();
        }

        public static int DeleteSalesManagerGrid(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("DeleteSalesManagerGrid");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@UserCode", dbParm.UserCode);
            cmd.SetParameterValue("@CreateUser", appcontentEntity.LogMessages.Username);

            DataSet dsHotelInfo = GetBindHotelInfo(dbParm.HotelID);
            if (dsHotelInfo.Tables.Count > 0 && dsHotelInfo.Tables[0].Rows.Count > 0)
            {
                cmd.SetParameterValue("@Fax", dsHotelInfo.Tables[0].Rows[0]["FAX"].ToString());
                cmd.SetParameterValue("@Per", dsHotelInfo.Tables[0].Rows[0]["CONTACTPER"].ToString());
                cmd.SetParameterValue("@Tel", dsHotelInfo.Tables[0].Rows[0]["PHONE"].ToString());
                cmd.SetParameterValue("@Email", dsHotelInfo.Tables[0].Rows[0]["CONTACTEMAIL"].ToString());
            }
            else
            {
                cmd.SetParameterValue("@Fax", DBNull.Value);
                cmd.SetParameterValue("@Per", DBNull.Value);
                cmd.SetParameterValue("@Tel", DBNull.Value);
                cmd.SetParameterValue("@Email", DBNull.Value);
            }
            cmd.ExecuteNonQuery();
            DeleteLMSalesHistory(appcontentEntity);
            //HotelInfoDA.UpdateHotelSalesUserAccount(dbParm.HotelID, "");
            return 1;
        }

        public static int DeleteHotelCompareGrid(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("DeleteHotelCompareGrid");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@TypeID", dbParm.TypeID);
            cmd.SetParameterValue("@UserID", appcontentEntity.LogMessages.Username);
            return cmd.ExecuteNonQuery();
        }

        public static int CreateSalesPlanEvent(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CreateSalesPlanEvent");

            cmd.SetParameterValue("@Type", dbParm.SaveType);
            if ("0".Equals(dbParm.SaveType.Trim()))
            {
                cmd.SetParameterValue("@Plan_Time", DBNull.Value);
                cmd.SetParameterValue("@Plan_DTime", DBNull.Value);
                cmd.SetParameterValue("@Start_Dtime", DBNull.Value);
                cmd.SetParameterValue("@End_Dtime", DBNull.Value);
                cmd.SetParameterValue("@Week_List", DBNull.Value);
                cmd.SetParameterValue("@CStatus", "2");
                cmd.SetParameterValue("@CAction", "1");
            }
            else if ("1".Equals(dbParm.SaveType.Trim()))
            {
                cmd.SetParameterValue("@Plan_Time", DBNull.Value);
                cmd.SetParameterValue("@Plan_DTime", dbParm.PlanDTime);
                cmd.SetParameterValue("@Start_Dtime", DBNull.Value);
                cmd.SetParameterValue("@End_Dtime", DBNull.Value);
                cmd.SetParameterValue("@Week_List", DBNull.Value);
                cmd.SetParameterValue("@CStatus", "1");
                cmd.SetParameterValue("@CAction", "0");
            }
            else if ("2".Equals(dbParm.SaveType.Trim()))
            {
                cmd.SetParameterValue("@Plan_Time", dbParm.PlanTime);
                cmd.SetParameterValue("@Plan_DTime", DBNull.Value);
                cmd.SetParameterValue("@Start_Dtime", dbParm.PlanStart);
                cmd.SetParameterValue("@End_Dtime", dbParm.PlanEnd);
                cmd.SetParameterValue("@Week_List", dbParm.PlanWeek);
                cmd.SetParameterValue("@CStatus", "1");
                cmd.SetParameterValue("@CAction", "0");
            }
            cmd.SetParameterValue("@Create_User", appcontentEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            int planID = (int)cmd.GetParameterValue("@PlanID");
            //CreateSalesPlanEventDetail(appcontentEntity, planID);
            return planID;
        }

        public static int CreateSalesPlanEventJobList(APPContentEntity appcontentEntity, int planID)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CreateSalesPlanEventJobList");

            cmd.SetParameterValue("@PlanID", planID);
            cmd.SetParameterValue("@Type", dbParm.SaveType);
            cmd.SetParameterValue("@Create_User", appcontentEntity.LogMessages.Username);

            if ("0".Equals(dbParm.SaveType.Trim()))
            {
                cmd.SetParameterValue("@Plan_DTime", DateTime.Now.ToString());
                cmd.SetParameterValue("@CStatus", "1");
                cmd.SetParameterValue("@CAction", "1");
                cmd.ExecuteNonQuery();
            }
            else if ("1".Equals(dbParm.SaveType.Trim()))
            {
                cmd.SetParameterValue("@Plan_DTime", dbParm.PlanDTime);
                cmd.SetParameterValue("@CStatus", "1");
                cmd.SetParameterValue("@CAction", "0");
                cmd.ExecuteNonQuery();
            }
            else if ("2".Equals(dbParm.SaveType.Trim()))
            {
                DateTime dtStart = DateTime.Parse(dbParm.PlanStart);
                DateTime dtEnd = DateTime.Parse(dbParm.PlanEnd);
                string strWeekList = dbParm.PlanWeek;
                string strDtTemp = string.Empty;
                while (dtStart <= dtEnd)
                {
                    int strToWeek = (int)dtStart.DayOfWeek + 1;
                    if (strWeekList.IndexOf(strToWeek.ToString()) >= 0)
                    {
                        strDtTemp = dtStart.ToShortDateString() + " " + dbParm.PlanTime;
                        cmd.SetParameterValue("@Plan_DTime", strDtTemp);
                        cmd.SetParameterValue("@CStatus", "1");
                        cmd.SetParameterValue("@CAction", "0");
                        cmd.ExecuteNonQuery();
                    }
                    dtStart = dtStart.AddDays(1);
                }
            }
            return 1;
        }

        public static DataSet CheckSalesPlanEventJobAction(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CheckSalesPlanEventJobAction");

            cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            cmd.SetParameterValue("@ActionStart", dbParm.PlanStart + " 00:00:00");
            cmd.SetParameterValue("@ActionEnd", dbParm.PlanStart + " 23:59:59");
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult;
        }

        public static int UpdateSalesPlanEventJobList(APPContentEntity appcontentEntity, string strStatus)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanEventJobList");

            cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            cmd.SetParameterValue("@Status", strStatus);
            cmd.SetParameterValue("@Update_User", appcontentEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public static int UpdateSalesPlanEventJobStatus(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanEventJobStatus");

            cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            cmd.SetParameterValue("@ActResult", appcontentEntity.ErrorMSG);
            cmd.SetParameterValue("@Update_User", appcontentEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public static int UpdateSalesPlanEventJobListForTime(APPContentEntity appcontentEntity, string strStatus)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanEventJobListForTime");

            cmd.SetParameterValue("@PlanID", dbParm.PlanID);
            cmd.SetParameterValue("@Status", strStatus);
            cmd.SetParameterValue("@ActionStart", dbParm.PlanStart + " 00:00:00");
            cmd.SetParameterValue("@Update_User", appcontentEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public static int UpdateSalesPlanEventStatus(APPContentEntity appcontentEntity, int planID)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSalesPlanEventStatus");
            cmd.SetParameterValue("@HPID", planID);
            cmd.SetParameterValue("@Result", appcontentEntity.ErrorMSG);
            cmd.SetParameterValue("@Update_User", appcontentEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public static int CreateSalesPlanEventDetail(APPContentEntity appcontentEntity, int planID)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CreateSalesPlanEventDetail");
            cmd.SetParameterValue("@HPID", planID);
            cmd.SetParameterValue("@Start_Dtime", dbParm.StartDTime);
            cmd.SetParameterValue("@End_Dtime", dbParm.EndDTime);
            cmd.SetParameterValue("@EffHour", dbParm.EffHour);
            cmd.SetParameterValue("@Week_List", dbParm.WeekList);
            string HotelID = dbParm.HotelID.Substring((dbParm.HotelID.IndexOf('[') + 1), (dbParm.HotelID.IndexOf(']') - 1));
            cmd.SetParameterValue("@HOTEL_ID", HotelID);
            cmd.SetParameterValue("@RATE_CODE", dbParm.PriceCode);
            cmd.SetParameterValue("@MONEY_TYPE", "CHY");
            cmd.SetParameterValue("@GUAID", dbParm.Note1);
            cmd.SetParameterValue("@CXLID", dbParm.Note2);
            cmd.SetParameterValue("@ROOM_TYPE_NAME", dbParm.RoomName);
            cmd.SetParameterValue("@ROOM_TYPE_CODE", dbParm.RoomCode);
            cmd.SetParameterValue("@STATUS", dbParm.RoomStatus);

            if (String.IsNullOrEmpty(dbParm.RoomCount))
            {
                cmd.SetParameterValue("@ROOM_NUM", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@ROOM_NUM", dbParm.RoomCount);
            }
            cmd.SetParameterValue("@IS_RESERVE", dbParm.IsReserve);

            if (String.IsNullOrEmpty(dbParm.OnePrice))
            {
                cmd.SetParameterValue("@ONE_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@ONE_PRICE", dbParm.OnePrice);
            }

            if (String.IsNullOrEmpty(dbParm.TwoPrice))
            {
                cmd.SetParameterValue("@TWO_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@TWO_PRICE", dbParm.TwoPrice);
            }

            if (String.IsNullOrEmpty(dbParm.ThreePrice))
            {
                cmd.SetParameterValue("@THREE_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@THREE_PRICE", dbParm.ThreePrice);
            }

            if (String.IsNullOrEmpty(dbParm.FourPrice))
            {
                cmd.SetParameterValue("@FOUR_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@FOUR_PRICE", dbParm.FourPrice);
            }

            if (String.IsNullOrEmpty(dbParm.BedPrice))
            {
                cmd.SetParameterValue("@ATTN_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@ATTN_PRICE", dbParm.BedPrice);
            }

            if (String.IsNullOrEmpty(dbParm.BreakfastNum))
            {
                cmd.SetParameterValue("@BREAKFAST_NUM", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@BREAKFAST_NUM", dbParm.BreakfastNum);
            }

            if (String.IsNullOrEmpty(dbParm.BreakPrice))
            {
                cmd.SetParameterValue("@EACH_BREAKFAST_PRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EACH_BREAKFAST_PRICE", dbParm.BreakPrice);
            }

            cmd.SetParameterValue("@IS_NETWORK", dbParm.IsNetwork);

            if (String.IsNullOrEmpty(dbParm.Offsetval))
            {
                cmd.SetParameterValue("@OFFSETVAL", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@OFFSETVAL", dbParm.Offsetval);
            }

            cmd.SetParameterValue("@OFFSETUNIT", dbParm.Offsetunit);
            cmd.SetParameterValue("@Create_User", appcontentEntity.LogMessages.Username);

            if (String.IsNullOrEmpty(dbParm.NetPrice))
            {
                cmd.SetParameterValue("@NETPRICE", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@NETPRICE", dbParm.NetPrice);
            }

            cmd.SetParameterValue("@SUPPLIER", dbParm.Supplier);

            cmd.ExecuteNonQuery();
            return 1;
        }

        public static bool GetHotelSales(string hotelId)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerHistory");
            cmd.SetParameterValue("@HotelID", hotelId);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static APPContentEntity GetCoreHotelGroupDetail(APPContentEntity appcontentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HGROUPID",OracleType.VarChar),
                                    new OracleParameter("GTYPE",OracleType.VarChar),
                                    new OracleParameter("CUSER",OracleType.VarChar)
                                };
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            if (String.IsNullOrEmpty(dbParm.HGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HGroupID;
            }

            if (String.IsNullOrEmpty(dbParm.GType))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.GType;
            }

            if (String.IsNullOrEmpty(dbParm.Cuser))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.Cuser;
            }
            
            appcontentEntity.QueryResult = DbManager.Query("APPContent", "t_lm_b_hotelGroup_select", false, parm);
            return appcontentEntity;
        }

        public static APPContentEntity BindHotelListGrid(APPContentEntity appcontentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HGROUPID",OracleType.VarChar),
                                    new OracleParameter("DTime",OracleType.VarChar),
                                    new OracleParameter("GTYPE",OracleType.VarChar),
                                    new OracleParameter("CUSER",OracleType.VarChar)
                                };
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            if (String.IsNullOrEmpty(dbParm.HGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HGroupID;
            }

            if (String.IsNullOrEmpty(dbParm.DTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.DTime;
            }

            if (String.IsNullOrEmpty(dbParm.GType))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.GType;
            }

            if (String.IsNullOrEmpty(dbParm.Cuser))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Cuser;
            }
            DataSet dsResult = DbManager.Query("APPContent", "t_lm_b_hotelGroup_list_select", false, parm);
            string strRcn = string.Empty;
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++ )
                {
                    strRcn = String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["RCN"].ToString()) ? "0" : dsResult.Tables[0].Rows[i]["RCN"].ToString();
                    if (int.Parse(strRcn) > 0 && "active".Equals(dsResult.Tables[0].Rows[i]["FOGSTATUS"].ToString()) && "1".Equals(dsResult.Tables[0].Rows[i]["ONLINESTATUS"].ToString()))
                    {
                        dsResult.Tables[0].Rows[i]["ONLINES"] = "是";
                    }
                    else
                    {
                        dsResult.Tables[0].Rows[i]["ONLINES"] = "否";
                    }

                    dsResult.Tables[0].Rows[i]["HVPMG"] = GetHotelSalesInfo(dsResult.Tables[0].Rows[i]["HOTELID"].ToString());
                }
            }

            appcontentEntity.QueryResult =dsResult;
            return appcontentEntity;
        }

        public static string GetHotelSalesInfo(string hotelId)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerHistory");
            cmd.SetParameterValue("@HotelID", hotelId);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["User_DspName"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static bool ChkInsertHotelGroup(string hGroupId, string hotelId, string gtype)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HGROUPID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("GTYPE",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(hGroupId))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = hGroupId;
            }

            if (String.IsNullOrEmpty(hotelId))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = hotelId;
            }

            if (String.IsNullOrEmpty(gtype))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = gtype;
            }

            DataSet dsResult = DbManager.Query("APPContent", "t_lm_b_hotelGroup_check", false, parm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ChkHotelExist(string hotelId)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(hotelId))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = hotelId;
            }

            DataSet dsResult = DbManager.Query("APPContent", "t_lm_b_hotel_check", false, parm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int InsertHotelGroupList(APPContentEntity appcontentEntity)
        {
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            if (!ChkHotelExist(dbParm.HotelID))
            {
                return 3;
            }

            if (ChkInsertHotelGroup(dbParm.HGroupID, dbParm.HotelID, dbParm.GType))
            {
                return 2;
            }

            OracleParameter[] parm ={
                                    new OracleParameter("HGROUPID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("GTYPE",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.HGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HGroupID;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(appcontentEntity.LogMessages.Username))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = appcontentEntity.LogMessages.Username;
            }

            if (String.IsNullOrEmpty(dbParm.GType))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.GType;
            }

            return DbManager.ExecuteSql("APPContent", "t_lm_b_hotelGroup_list_add", parm);
        }

        public static int DeteleHotelGroupList(APPContentEntity appcontentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HGROUPID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("GTYPE",OracleType.VarChar)
                                };
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();

            if (String.IsNullOrEmpty(dbParm.HGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HGroupID;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(appcontentEntity.LogMessages.Username))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = appcontentEntity.LogMessages.Username;
            }

            if (String.IsNullOrEmpty(dbParm.GType))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.GType;
            }

            return DbManager.ExecuteSql("APPContent", "t_lm_b_hotelGroup_list_del", parm);
        }

        public static APPContentEntity SelectPropByPic(APPContentEntity appcontentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PROP",OracleType.VarChar)
                                };
            APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            parm[0].Value = dbParm.HotelID;

            DataSet dsResult = DbManager.Query("APPContent", "t_lm_b_prop_selectbypic", true, parm);
            appcontentEntity.QueryResult = dsResult;
            return appcontentEntity;
        }
    }
}