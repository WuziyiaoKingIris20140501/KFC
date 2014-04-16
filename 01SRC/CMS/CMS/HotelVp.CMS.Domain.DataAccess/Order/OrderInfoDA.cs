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
using System.Collections;
using HotelVp.CMS.Domain.Entity.Order;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class OrderInfoDA
    {
        public static OrderInfoEntity BindOrderList(OrderInfoEntity orderEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTEL_ID",OracleType.VarChar),
                                    new OracleParameter("HOTEL_NAME",OracleType.VarChar),
                                    new OracleParameter("CITY_ID",OracleType.VarChar),                                  
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar),
                                    new OracleParameter("BOOK_STATUS",OracleType.VarChar),
                                    new OracleParameter("PAY_STATUS",OracleType.VarChar),
                                    new OracleParameter("FOG_ORDER_NUM",OracleType.VarChar)
                                };

            OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.HOTEL_ID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HOTEL_ID;
            }


            if (String.IsNullOrEmpty(dbParm.HOTEL_NAME))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HOTEL_NAME;
            }

            if (String.IsNullOrEmpty(dbParm.CITY_ID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.CITY_ID;
            }

            if (String.IsNullOrEmpty(dbParm.StartDate))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.StartDate;
            }

            if (String.IsNullOrEmpty(dbParm.EndDate))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.EndDate;
            }

            if (String.IsNullOrEmpty(dbParm.BOOK_STATUS))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.BOOK_STATUS;
            }

            if (String.IsNullOrEmpty(dbParm.PAY_STATUS))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.PAY_STATUS;
            }

            if (String.IsNullOrEmpty(dbParm.FOG_ORDER_NUM))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.FOG_ORDER_NUM;
            }


            orderEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_orderinfo_select", true, parm);
            return orderEntity;
        }

        public static OrderInfoEntity BindOrderConfirmList(OrderInfoEntity orderEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CityID",OracleType.VarChar),
                                    new OracleParameter("CStatus",OracleType.VarChar),
                                    new OracleParameter("FStatus",OracleType.VarChar),
                                    new OracleParameter("HotelConfirm",OracleType.VarChar),
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("UserID",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("FollowUp",OracleType.VarChar),
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar),
                                    new OracleParameter("FaxSNumber",OracleType.VarChar),
                                    new OracleParameter("FaxENumber",OracleType.VarChar),
                                    new OracleParameter("BStatusOther",OracleType.VarChar),
                                    new OracleParameter("PriceCode",OracleType.VarChar),
                                    new OracleParameter("BStatus",OracleType.VarChar)
                                };

            OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.CStatus))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = ("3".Equals(dbParm.CStatus)) ? "" : dbParm.CStatus;
            }

            if (String.IsNullOrEmpty(dbParm.FStatus))
            {
                parm[2].Value = DBNull.Value;
                parm[10].Value = DBNull.Value;
                parm[11].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.FStatus;
                parm[10].Value = DBNull.Value;
                parm[11].Value = DBNull.Value;
                //if ("4".Equals(dbParm.FStatus))
                //{
                //    parm[2].Value = "1";
                //    parm[10].Value = "2";
                //    parm[11].Value = DBNull.Value;
                //}
                //else if ("1".Equals(dbParm.FStatus))
                //{
                //    parm[2].Value = dbParm.FStatus;
                //    parm[10].Value = "0";
                //    parm[11].Value = "1";
                //}
                //else
                //{
                //    parm[2].Value = dbParm.FStatus;
                //    parm[10].Value = DBNull.Value;
                //    parm[11].Value = DBNull.Value;
                //}
            }

            if (String.IsNullOrEmpty(dbParm.HotelConfirm))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.HotelConfirm;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.OrderID))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.OrderID;
            }

            if ("3".Equals(dbParm.CStatus))
            {
                parm[7].Value = "1";
            }
            else
            {
                parm[7].Value = DBNull.Value;
            }

            if (String.IsNullOrEmpty(dbParm.StartDate))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = dbParm.StartDate;
            }

            if (String.IsNullOrEmpty(dbParm.EndDate))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = dbParm.EndDate;
            }

            if (String.IsNullOrEmpty(dbParm.BOOK_STATUS_OTHER))
            {
                parm[12].Value = DBNull.Value;
            }
            else
            {
                parm[12].Value = dbParm.BOOK_STATUS_OTHER;
            }

            if (String.IsNullOrEmpty(dbParm.PRICE_CODE))
            {
                parm[13].Value = DBNull.Value;
            }
            else
            {
                parm[13].Value = dbParm.PRICE_CODE;
            }

            if (String.IsNullOrEmpty(dbParm.BOOK_STATUS))
            {
                parm[14].Value = DBNull.Value;
            }
            else
            {
                parm[14].Value = dbParm.BOOK_STATUS;
            }

            //string SQLString = ("1".Equals(dbParm.SType)) ? HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderconfirminfo_select_init") : HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderconfirminfo_select");
            string SQLString = ("1".Equals(dbParm.SType)) ? HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderconfirminfo_select_init_all") : HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderconfirminfo_select_all");

            if (String.IsNullOrEmpty(dbParm.SortID))
            {
                SQLString = SQLString + "order by t.price_code asc,t.create_time asc, toc.fax_status asc";
            }
            else
            {
                SQLString = SQLString + "order by t.price_code asc,toc.order_id " + dbParm.SortID;
            }

            orderEntity.QueryResult = DbHelperOra.Query(SQLString, false, parm);//HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_orderconfirminfo_select",true, parm);
            return orderEntity;
        }

        public static OrderInfoEntity ChkOrderLock(OrderInfoEntity orderEntity)
        {
            //OracleParameter[] parm ={
            //                        new OracleParameter("ORDERID",OracleType.VarChar)
            //                    };

            //OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();
            //if (String.IsNullOrEmpty(dbParm.OrderID))
            //{
            //    parm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[0].Value = dbParm.OrderID;
            //}

            //orderEntity.QueryResult = DbManager.Query("OrderInfo", "t_lm_orderappromginfo_chkOrderLock", false, parm);
            //return orderEntity;


            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };

            OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();
            if (String.IsNullOrEmpty(dbParm.OrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.OrderID;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_check", true, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                if ("2".Equals(dsResult.Tables[0].Rows[0]["status"].ToString().Trim()) && !orderEntity.LogMessages.Userid.Equals(dsResult.Tables[0].Rows[0]["ope_user"].ToString().Trim()))
                {
                    orderEntity.Result = 1;
                }
                else
                {
                    orderEntity.Result = 0;
                }
            }
            else
            {
                orderEntity.Result = 0;
            }
            return orderEntity;
        }

        public static OrderInfoEntity BindOrderConfirmManagerList(OrderInfoEntity orderEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CityID",OracleType.VarChar),
                                    new OracleParameter("FGStatus",OracleType.VarChar),
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("UserID",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar)
                                };

            OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.FGStatus))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.FGStatus;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.OrderID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.OrderID;
            }

            if (String.IsNullOrEmpty(dbParm.StartDate))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.StartDate;
            }

            if (String.IsNullOrEmpty(dbParm.EndDate))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.EndDate;
            }

            string SQLString = HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderconfirmmginfo_select");
            SQLString = SQLString + "order by t.fog_order_num " + dbParm.SortID + ", t.hotel_name";

            DataSet dsResult = DbHelperOra.Query(SQLString, false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["HVPNM"].ToString()))
                    {
                        dsResult.Tables[0].Rows[i]["HVPNM"] = GetSalesNUm(dsResult.Tables[0].Rows[i]["HVPNM"].ToString());
                    }
                }
            }

            orderEntity.QueryResult = dsResult;
            return orderEntity;
        }

        public static OrderInfoEntity BindOrderApproveList(OrderInfoEntity orderEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar)
                                };

            OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }
            if (String.IsNullOrEmpty(dbParm.StartDate))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.StartDate;
            }

            if (String.IsNullOrEmpty(dbParm.EndDate))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.EndDate;
            }

            if (String.IsNullOrEmpty(dbParm.OrderID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.OrderID;
            }

            if (String.IsNullOrEmpty(dbParm.AuditStatus))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.AuditStatus;
            }
            string SQLString = HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderappromginfo_select");
            SQLString = SQLString + "order by t.fog_order_num " + dbParm.SortID + ", t.hotel_name";

            DataSet dsResult = DbHelperOra.Query(SQLString, false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["HVPNM"].ToString()))
                    {
                        dsResult.Tables[0].Rows[i]["HVPNM"] = GetSalesNUm(dsResult.Tables[0].Rows[i]["HVPNM"].ToString());
                    }
                }
            }

            orderEntity.QueryResult = dsResult;
            return orderEntity;
        }

        public static OrderInfoEntity BindOrderApproveFaxList(OrderInfoEntity orderEntity)
        {
            OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();
            DataSet dsResult = new DataSet();
            if ("1".Equals(dbParm.SqlType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("FaxNum",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.OrderID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.OrderID;
                }

                if (String.IsNullOrEmpty(dbParm.FaxNum))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.FaxNum;
                }

                string SQLString = HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderappromginfo_fax_select_order");
                SQLString = SQLString + "order by t.fog_order_num " + dbParm.SortID + ", t.hotel_name";

                dsResult = DbHelperOra.Query(SQLString, false, parm);
            }
            else if ("2".Equals(dbParm.SqlType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("OutSDate",OracleType.VarChar),
                                    new OracleParameter("OutEDate",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TRADEAREAID",OracleType.VarChar),
                                    new OracleParameter("FaxNum",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.HotelID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.HotelID;
                }

                if (String.IsNullOrEmpty(dbParm.StartDate))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.StartDate;
                }

                if (String.IsNullOrEmpty(dbParm.EndDate))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.EndDate;
                }

                if (String.IsNullOrEmpty(dbParm.OrderID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.OrderID;
                }

                if (String.IsNullOrEmpty(dbParm.AuditStatus))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.AuditStatus;
                }


                if (String.IsNullOrEmpty(dbParm.City))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = dbParm.City;
                }

                if (String.IsNullOrEmpty(dbParm.Bussiness))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.Bussiness;
                }

                if (String.IsNullOrEmpty(dbParm.FaxNum))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = dbParm.FaxNum;
                }

                string SQLString = HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderappromginfo_fax_select_hotel");
                SQLString = SQLString + "order by t.fog_order_num " + dbParm.SortID + ", t.hotel_name";

                dsResult = DbHelperOra.Query(SQLString, false, parm);
            }
            else
            {
                OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("OutSDate",OracleType.VarChar),
                                    new OracleParameter("OutEDate",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(dbParm.HotelID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.HotelID;
                }

                if (String.IsNullOrEmpty(dbParm.StartDate))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.StartDate;
                }

                if (String.IsNullOrEmpty(dbParm.EndDate))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.EndDate;
                }

                if (String.IsNullOrEmpty(dbParm.OrderID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.OrderID;
                }

                if (String.IsNullOrEmpty(dbParm.AuditStatus))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.AuditStatus;
                }
                string SQLString = HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("OrderInfo", "t_lm_orderappromginfo_fax_select");
                SQLString = SQLString + "order by t.fog_order_num " + dbParm.SortID + ", t.hotel_name";

                dsResult = DbHelperOra.Query(SQLString, false, parm);
            }

            #region 过滤复审
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                string LDStatus = string.Empty;
                string NSStatus = string.Empty;
                string LDdbAppr = string.Empty;
                string NSdbAppr = string.Empty;
                string ADStatsBack = (String.IsNullOrEmpty(dbParm.ADStatsBack)) ? "" : dbParm.ADStatsBack;

                if (ADStatsBack.Contains("1") || ADStatsBack.Contains("3"))
                {
                    LDStatus = "离店";
                }

                if (ADStatsBack.Contains("2") || ADStatsBack.Contains("4"))
                {
                    NSStatus = "No-Show";
                }

                if (ADStatsBack.Contains("1"))
                {
                    LDdbAppr = "0";
                }
                else if (ADStatsBack.Contains("3"))
                {
                    LDdbAppr = "1";
                }

                if (ADStatsBack.Contains("2"))
                {
                    NSdbAppr = "0";
                }
                else if (ADStatsBack.Contains("4"))
                {
                    NSdbAppr = "1";
                }
                DataSet dsOrders = new DataSet();
                for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    if ("未审核".Equals(dsResult.Tables[0].Rows[i]["ORDERST"].ToString()))
                    {
                        if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["HVPNM"].ToString()))
                        {
                            dsResult.Tables[0].Rows[i]["HVPNM"] = GetSalesNUm(dsResult.Tables[0].Rows[i]["HVPNM"].ToString());
                        }
                        continue;
                    }

                    if (!String.IsNullOrEmpty(ADStatsBack))
                    {
                        DataCommand cmd = DataCommandManager.GetDataCommand("ChkApproveOrderList");
                        cmd.SetParameterValue("@ORDERID", dsResult.Tables[0].Rows[i]["ORDERID"].ToString());
                        dsOrders = cmd.ExecuteDataSet();

                        if (dsOrders.Tables.Count == 0 || dsOrders.Tables[0].Rows.Count == 0)
                        {
                            dsResult.Tables[0].Rows.RemoveAt(i);
                            continue;
                        }
                        else
                        {
                            if ("离店".Equals(dsOrders.Tables[0].Rows[0]["OD_STATUS"].ToString()))
                            {
                                if (!(!String.IsNullOrEmpty(LDStatus) && LDdbAppr.Equals(dsOrders.Tables[0].Rows[0]["ISDBAPPROVE"].ToString())))
                                {
                                    dsResult.Tables[0].Rows.RemoveAt(i);
                                    continue;
                                }
                            }
                            else if ("No-Show".Equals(dsOrders.Tables[0].Rows[0]["OD_STATUS"].ToString()))
                            {
                                if (!(!String.IsNullOrEmpty(NSStatus) && NSdbAppr.Equals(dsOrders.Tables[0].Rows[0]["ISDBAPPROVE"].ToString())))
                                {
                                    dsResult.Tables[0].Rows.RemoveAt(i);
                                    continue;
                                }
                            }
                            else
                            {
                                dsResult.Tables[0].Rows.RemoveAt(i);
                                continue;
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["HVPNM"].ToString()))
                    {
                        dsResult.Tables[0].Rows[i]["HVPNM"] = GetSalesNUm(dsResult.Tables[0].Rows[i]["HVPNM"].ToString());
                    }
                }
            }
            #endregion

            orderEntity.QueryResult = dsResult;
            return orderEntity;
        }

        public static OrderInfoEntity BindOrderApproveFaxPrint(OrderInfoEntity orderEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("OrderList",OracleType.VarChar)
                                };

            OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }

            ArrayList alList = new ArrayList();
            alList = dbParm.OrderList;
            string strList = "";
            foreach (string strSN in alList)
            {
                strList = strList + strSN + ",";
            }
            if (alList.Count == 0)
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = strList;
            }

            DataSet dsResult = DbManager.Query("OrderInfo", "t_lm_orderappromginfo_fax_print_select", false, parm);
            dsResult.Tables[0].TableName = "PRINT";


            OracleParameter[] htparm ={
                                    new OracleParameter("HotelID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                htparm[0].Value = DBNull.Value;
            }
            else
            {
                htparm[0].Value = dbParm.HotelID;
            }

            DataTable dtHotelEx = new DataTable();
            dtHotelEx = DbManager.Query("OrderInfo", "t_lm_hotelex_print_select", false, htparm).Tables[0].Copy();
            dtHotelEx.TableName = "HLEX";

            DataTable dtUsersInfo = new DataTable();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCmsSysUsersInfo");
            cmd.SetParameterValue("@UserAccount", orderEntity.LogMessages.Userid);
            dtUsersInfo = cmd.ExecuteDataSet().Tables[0].Copy();
            dtUsersInfo.TableName = "UsersInfo";

            dsResult.Tables.Add(dtHotelEx);
            dsResult.Tables.Add(dtUsersInfo);
            orderEntity.QueryResult = dsResult;
            return orderEntity;
        }


        public static OrderInfoEntity BindOrderApprovePrint(OrderInfoEntity orderEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar)
                                };

            OrderInfoDBEntity dbParm = (orderEntity.OrderInfoDBEntity.Count > 0) ? orderEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }
            if (String.IsNullOrEmpty(dbParm.StartDate))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.StartDate;
            }

            if (String.IsNullOrEmpty(dbParm.EndDate))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.EndDate;
            }

            if (String.IsNullOrEmpty(dbParm.OrderID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.OrderID;
            }

            if (String.IsNullOrEmpty(dbParm.AuditStatus))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.AuditStatus;
            }

            DataSet dsResult = DbManager.Query("OrderInfo", "t_lm_orderappromginfo_print_select", false, parm);
            dsResult.Tables[0].TableName = "PRINT";


            OracleParameter[] htparm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                htparm[0].Value = DBNull.Value;
            }
            else
            {
                htparm[0].Value = dbParm.HotelID;
            }

            DataTable dtHotelEx = new DataTable();
            dtHotelEx = DbManager.Query("OrderInfo", "t_lm_hotelex_print_select", false, htparm).Tables[0].Copy();
            dtHotelEx.TableName = "HLEX";

            DataTable dtUsersInfo = new DataTable();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCmsSysUsersInfo");
            cmd.SetParameterValue("@UserAccount", orderEntity.LogMessages.Userid);
            dtUsersInfo = cmd.ExecuteDataSet().Tables[0].Copy();
            dtUsersInfo.TableName = "UsersInfo";

            dsResult.Tables.Add(dtHotelEx);
            dsResult.Tables.Add(dtUsersInfo);
            orderEntity.QueryResult = dsResult;
            return orderEntity;
        }

        private static string GetSalesNUm(string strSales)
        {
            string strResutl = "";

            DataCommand cmd = DataCommandManager.GetDataCommand("GetMailToList");
            cmd.SetParameterValue("@UserID", strSales);
            DataSet dsTemp = cmd.ExecuteDataSet();

            if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsTemp.Tables[0].Rows[0]["User_Tel"].ToString()))
            {
                strResutl = strSales + " - " + dsTemp.Tables[0].Rows[0]["User_Tel"].ToString();
            }
            else
            {
                strResutl = strSales;
            }

            return strResutl;
        }

        public static OrderInfoEntity CommonCitySelect(OrderInfoEntity orderInfoEntity)
        {
            orderInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_b_citylist", true);
            return orderInfoEntity;

        }

        public static OrderInfoEntity UnLockOrderConfirm(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            OracleParameter[] lmparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.OrderID))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = dbParm.OrderID;
            }

            lmparm[1].Value = orderInfoEntity.LogMessages.Userid;

            orderInfoEntity.Result = HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_unlock", lmparm);
            return orderInfoEntity;

        }

        public static OrderInfoEntity UnLockHotelOrderConfirm(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            OracleParameter[] lmparm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.HOTEL_ID))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = dbParm.HOTEL_ID;
            }

            lmparm[1].Value = orderInfoEntity.LogMessages.Userid;

            orderInfoEntity.Result = HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_hotel_unlock", lmparm);
            return orderInfoEntity;

        }

        public static LmSystemLogEntity OrderOperationMemoSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("OrderOperationMemoSelect");
            cmd.SetParameterValue("@EVENTID", lmSystemLogEntity.MemoKey);
            lmSystemLogEntity.QueryResult = cmd.ExecuteDataSet();

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderActionHis(LmSystemLogEntity lmSystemLogEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("OrderActionHis");
            cmd.SetParameterValue("@FGID", lmSystemLogEntity.MemoKey);
            lmSystemLogEntity.QueryResult = cmd.ExecuteDataSet();

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity GetOrderActionHisList(LmSystemLogEntity lmSystemLogEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetOrderActionHisList");
            cmd.SetParameterValue("@FGID", lmSystemLogEntity.MemoKey);
            lmSystemLogEntity.QueryResult = cmd.ExecuteDataSet();

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderOperationSalesSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerHistory");
            cmd.SetParameterValue("@HotelID", lmSystemLogEntity.HotelID);
            lmSystemLogEntity.QueryResult = cmd.ExecuteDataSet();

            return lmSystemLogEntity;
        }

        public static OrderInfoEntity OrderOperateStatus(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            OracleParameter[] lmparm ={
                                    new OracleParameter("FOGORDERNUM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.FOG_ORDER_NUM))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = dbParm.FOG_ORDER_NUM;
            }


            orderInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_by_operatestatus", true, lmparm);
            return orderInfoEntity;

        }

        public static OrderInfoEntity InsertOrderActionHisList(OrderInfoEntity orderInfoEntity, string Result)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("InsertOrderActionHisList");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            cmd.SetParameterValue("@FOGORDERNUM", dbParm.FOG_ORDER_NUM);
            cmd.SetParameterValue("@ACTIONID", dbParm.ActionID);
            cmd.SetParameterValue("@STATUS", dbParm.BOOK_STATUS);
            cmd.SetParameterValue("@REMARK", dbParm.REMARK);
            cmd.SetParameterValue("@CANNEL", dbParm.CanelReson);
            cmd.SetParameterValue("@EVENTUSER", dbParm.USER_ID);
            cmd.SetParameterValue("@APPROVEID", dbParm.ApproveId);
            cmd.SetParameterValue("@ROOMID", dbParm.ROOM_TYPE_CODE);
            cmd.SetParameterValue("@OPERATERESULT", Result);
            cmd.SetParameterValue("@ISDBAPPROVE", dbParm.IsDbApprove);

            orderInfoEntity.Result = cmd.ExecuteNonQuery();

            return orderInfoEntity;
        }

        public static LmSystemLogEntity GetHCorderList(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CONTTEL",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.HotelID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.ContactTel))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.ContactTel;
            }
            parm[3].Value = lmSystemLogEntity.LogMessages.Userid;
            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_comfirm_hcorder_list", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                OracleParameter[] lkparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    lkparm[0].Value = DBNull.Value;
                }
                else
                {
                    lkparm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    lkparm[1].Value = DBNull.Value;
                }
                else
                {
                    lkparm[1].Value = lmSystemLogEntity.HotelID;
                }
                lkparm[2].Value = lmSystemLogEntity.LogMessages.Userid;
                HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_hotel_lock", lkparm);

                for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    if ("0".Equals(dsResult.Tables[0].Rows[i]["CHTYPE"].ToString()) && dsResult.Tables[0].Select("CHTYPE='1' AND ORDERID='" + dsResult.Tables[0].Rows[i]["ORDERID"].ToString() + "'").Length > 0)
                    {
                        dsResult.Tables[0].Rows.RemoveAt(i);
                    }
                }
            }

            decimal iDay = 0;
            decimal iNum = 0;
            decimal iPrice = 0;
            decimal decPrice = 0;
            string strDay = string.Empty;

            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                strDay = SetOrderDaysVal(dsResult.Tables[0].Rows[i]["in_date"].ToString(), dsResult.Tables[0].Rows[i]["out_date"].ToString());
                iDay = decimal.Parse(strDay);
                iNum = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["book_room_num"].ToString())) ? 1 : decimal.Parse(dsResult.Tables[0].Rows[i]["book_room_num"].ToString());
                iPrice = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["book_total_price"].ToString())) ? 0 : decimal.Parse(dsResult.Tables[0].Rows[i]["book_total_price"].ToString());
                decPrice = Math.Round((iPrice / iDay) / iNum, 1);

                dsResult.Tables[0].Rows[i]["INODATE"] = strDay + "晚（" + dsResult.Tables[0].Rows[i]["in_date_nm"].ToString() + " 至 " + dsResult.Tables[0].Rows[i]["out_date_nm"].ToString() + "）";
                dsResult.Tables[0].Rows[i]["PRICES"] = dsResult.Tables[0].Rows[i]["book_total_price"].ToString() + "元" + "（" + decPrice.ToString() + "元/间夜）";
                dsResult.Tables[0].Rows[i]["HOTELNM"] = dsResult.Tables[0].Rows[i]["hotelid"].ToString() + (dsResult.Tables[0].Rows[i]["hotel_name"].ToString().Trim().Length > 12 ? dsResult.Tables[0].Rows[i]["hotel_name"].ToString().Trim().Substring(0, 12) + "..." : dsResult.Tables[0].Rows[i]["hotel_name"].ToString().Trim());
            }

            lmSystemLogEntity.QueryResult = dsResult;
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity GetHCorderViewList(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CONTTEL",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.HotelID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.ContactTel))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.ContactTel;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_comfirm_hcorder_list_view", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    if ("0".Equals(dsResult.Tables[0].Rows[i]["CHTYPE"].ToString()) && dsResult.Tables[0].Select("CHTYPE='1' AND ORDERID='" + dsResult.Tables[0].Rows[i]["ORDERID"].ToString() + "'").Length > 0)
                    {
                        dsResult.Tables[0].Rows.RemoveAt(i);
                    }
                }
            }

            decimal iDay = 0;
            decimal iNum = 0;
            decimal iPrice = 0;
            decimal decPrice = 0;
            string strDay = string.Empty;

            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                strDay = SetOrderDaysVal(dsResult.Tables[0].Rows[i]["in_date"].ToString(), dsResult.Tables[0].Rows[i]["out_date"].ToString());
                iDay = decimal.Parse(strDay);
                iNum = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["book_room_num"].ToString())) ? 1 : decimal.Parse(dsResult.Tables[0].Rows[i]["book_room_num"].ToString());
                iPrice = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["book_total_price"].ToString())) ? 0 : decimal.Parse(dsResult.Tables[0].Rows[i]["book_total_price"].ToString());
                decPrice = Math.Round((iPrice / iDay) / iNum, 1);

                dsResult.Tables[0].Rows[i]["INODATE"] = strDay + "晚（" + dsResult.Tables[0].Rows[i]["in_date_nm"].ToString() + " 至 " + dsResult.Tables[0].Rows[i]["out_date_nm"].ToString() + "）";
                dsResult.Tables[0].Rows[i]["PRICES"] = dsResult.Tables[0].Rows[i]["book_total_price"].ToString() + "元" + "（" + decPrice.ToString() + "元/间夜）";
                dsResult.Tables[0].Rows[i]["HOTELNM"] = dsResult.Tables[0].Rows[i]["hotelid"].ToString() + (dsResult.Tables[0].Rows[i]["hotel_name"].ToString().Trim().Length > 12 ? dsResult.Tables[0].Rows[i]["hotel_name"].ToString().Trim().Substring(0, 12) + "..." : dsResult.Tables[0].Rows[i]["hotel_name"].ToString().Trim());
            }

            lmSystemLogEntity.QueryResult = dsResult;
            return lmSystemLogEntity;
        }

        private static string SetOrderDaysVal(string strInDate, string strOutDate)
        {
            if (String.IsNullOrEmpty(strInDate.Trim()) || String.IsNullOrEmpty(strOutDate.Trim()))
            {
                return "1";
            }

            try
            {
                DateTime dtInDate = Convert.ToDateTime(strInDate);
                DateTime dtOutDate = Convert.ToDateTime(strOutDate);
                TimeSpan tsDays = dtOutDate - dtInDate;
                return tsDays.Days.ToString();
            }
            catch
            {
                return "1";
            }
        }

        #region  订单审核报表
        public static OrderInfoEntity OrderApprovedReport(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            OracleParameter[] lmparm ={
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.StartDate))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = dbParm.StartDate;
            }

            if (String.IsNullOrEmpty(dbParm.EndDate))
            {
                lmparm[1].Value = DBNull.Value;
            }
            else
            {
                lmparm[1].Value = dbParm.EndDate;
            }

            if (String.IsNullOrEmpty(dbParm.CITY_ID))
            {
                lmparm[2].Value = DBNull.Value;
            }
            else
            {
                lmparm[2].Value = dbParm.CITY_ID;
            }

            orderInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_approved_report", true, lmparm);
            return orderInfoEntity;
        }

        public static OrderInfoEntity OrderApprovedByCount(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            OracleParameter[] lmparm ={
                                    new OracleParameter("FOGORDERNUM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.FOG_ORDER_NUM))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = dbParm.FOG_ORDER_NUM;
            }

            orderInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_by_order", true, lmparm);
            return orderInfoEntity;
        }

        public static OrderInfoEntity OrderApprovedByOrderDetails(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            OracleParameter[] lmparm ={
                                    new OracleParameter("FOGORDERNUM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.FOG_ORDER_NUM))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = dbParm.FOG_ORDER_NUM;
            }

            orderInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_by_order_details", true, lmparm);
            return orderInfoEntity;
        }


        public static OrderInfoEntity GetApprovedOrderList(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetApprovedOrderList");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            if (string.IsNullOrEmpty(dbParm.CONTACT_NAME))
            {
                cmd.SetParameterValue("@EVENTUSER", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EVENTUSER", dbParm.CONTACT_NAME);
            }
            cmd.SetParameterValue("@StartDate", dbParm.StartDate);
            cmd.SetParameterValue("@EndDate", dbParm.EndDate);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }


        public static OrderInfoEntity GetApprovedOrderListByCheck(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetApprovedOrderListByCheck");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            if (string.IsNullOrEmpty(dbParm.CONTACT_NAME))
            {
                cmd.SetParameterValue("@EVENTUSER", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EVENTUSER", dbParm.CONTACT_NAME);
            }
            cmd.SetParameterValue("@StartDate", dbParm.StartDate);
            cmd.SetParameterValue("@EndDate", dbParm.EndDate);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }

        public static OrderInfoEntity GetOrderListNum(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetOrderListNum");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            cmd.SetParameterValue("@StartDate", dbParm.StartDate);
            cmd.SetParameterValue("@EndDate", dbParm.EndDate);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }

        public static OrderInfoEntity GetApprovedUserListByCheck(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetApprovedUserListByCheck");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            if (string.IsNullOrEmpty(dbParm.CONTACT_NAME))
            {
                cmd.SetParameterValue("@EVENTUSER", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EVENTUSER", dbParm.CONTACT_NAME);
            }
            cmd.SetParameterValue("@StartDate", dbParm.StartDate);
            cmd.SetParameterValue("@EndDate", dbParm.EndDate);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }

        #region   订单审核 临时数据   初审：初审订单  初审NS订单   复审：复审订单  复审离店单
        //获取某个人的初审订单数   临时加
        public static OrderInfoEntity GetFirstAppOrders(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetFirstAppOrders");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            if (string.IsNullOrEmpty(dbParm.CONTACT_NAME))
            {
                cmd.SetParameterValue("@EVENTUSER", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EVENTUSER", dbParm.CONTACT_NAME);
            }
            cmd.SetParameterValue("@StartDate", dbParm.StartDate);
            cmd.SetParameterValue("@EndDate", dbParm.EndDate);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }

        //获取某个人的初审No-Show订单数   临时加
        public static OrderInfoEntity GetFirstAppNSOrders(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetFirstAppNSOrders");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            if (string.IsNullOrEmpty(dbParm.CONTACT_NAME))
            {
                cmd.SetParameterValue("@EVENTUSER", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EVENTUSER", dbParm.CONTACT_NAME);
            }
            cmd.SetParameterValue("@StartDate", dbParm.StartDate);
            cmd.SetParameterValue("@EndDate", dbParm.EndDate);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }

        //获取某个人的复审订单数   临时加
        public static OrderInfoEntity GetCheckAppOrders(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetCheckAppOrders");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            if (string.IsNullOrEmpty(dbParm.CONTACT_NAME))
            {
                cmd.SetParameterValue("@EVENTUSER", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EVENTUSER", dbParm.CONTACT_NAME);
            }
            cmd.SetParameterValue("@StartDate", dbParm.StartDate);
            cmd.SetParameterValue("@EndDate", dbParm.EndDate);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }

        //获取某个人的复审No-Show订单数   临时加
        public static OrderInfoEntity GetCheckAppNSOrders(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetCheckAppNSOrders");
            cmd.SetParameterValue("@EVENTTYPE", dbParm.EventType);
            if (string.IsNullOrEmpty(dbParm.CONTACT_NAME))
            {
                cmd.SetParameterValue("@EVENTUSER", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EVENTUSER", dbParm.CONTACT_NAME);
            }
            cmd.SetParameterValue("@StartDate", dbParm.StartDate);
            cmd.SetParameterValue("@EndDate", dbParm.EndDate);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }
        #endregion

        #endregion


        /// <summary>
        /// 订单详情页  离店状态 获取房间号
        /// </summary>
        /// <param name="orderInfoEntity"></param>
        /// <returns></returns>
        public static OrderInfoEntity GetRoomNumber(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetRoomNumber");
            cmd.SetParameterValue("@FOGID", dbParm.EventType);
            orderInfoEntity.QueryResult = cmd.ExecuteDataSet();

            return orderInfoEntity;
        }

        /// <summary>
        /// 退款列表
        /// </summary>
        /// <param name="orderInfoEntity"></param>
        /// <returns></returns>
        public static OrderInfoEntity GetRefundOrderList(OrderInfoEntity orderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (orderInfoEntity.OrderInfoDBEntity.Count > 0) ? orderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            OracleParameter[] lmparm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("FOGORDERNUM",OracleType.VarChar),
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.HOTEL_ID))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = dbParm.HOTEL_ID;
            }

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                lmparm[1].Value = DBNull.Value;
            }
            else
            {
                lmparm[1].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.FOG_ORDER_NUM))
            {
                lmparm[2].Value = DBNull.Value;
            }
            else
            {
                lmparm[2].Value = dbParm.FOG_ORDER_NUM;
            }

            if (String.IsNullOrEmpty(dbParm.StartDate))
            {
                lmparm[3].Value = DBNull.Value;
            }
            else
            {
                lmparm[3].Value = dbParm.StartDate;
            }

            if (String.IsNullOrEmpty(dbParm.EndDate))
            {
                lmparm[4].Value = DBNull.Value;
            }
            else
            {
                lmparm[4].Value = dbParm.EndDate;
            }

            if (String.IsNullOrEmpty(dbParm.PAY_STATUS))
            {
                lmparm[5].Value = DBNull.Value;
            }
            else
            {
                lmparm[5].Value = dbParm.PAY_STATUS;
            }
            orderInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_by_order_refund", true, lmparm);
            return orderInfoEntity;

        }

    }
}
