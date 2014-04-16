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
    public abstract class LmSystemLogDA
    {
        public static LmSystemLogEntity ReviewSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            //OracleParameter[] parm ={
            //                        new OracleParameter("ID",OracleType.VarChar),
            //                        new OracleParameter("StartDTime",OracleType.VarChar),
            //                        new OracleParameter("EndDTime",OracleType.VarChar)
            //                    };

            //if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            //{
            //    parm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[0].Value = lmSystemLogEntity.EventID;
            //}

            //if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            //{
            //    parm[1].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[1].Value = lmSystemLogEntity.StartDTime;
            //}

            //if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            //{
            //    parm[2].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[2].Value = lmSystemLogEntity.EndDTime;
            //}

            //lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_review_select", parm);
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectCmsOrderEventHistory");
            cmd.SetParameterValue("@EVENT_ID", lmSystemLogEntity.EventID);
            cmd.SetParameterValue("@STARTDTIME", lmSystemLogEntity.StartDTime);
            cmd.SetParameterValue("@ENDDTIME", lmSystemLogEntity.EndDTime);
            lmSystemLogEntity.QueryResult = cmd.ExecuteDataSet();
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReviewSelectByNewCount(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PAYCODE2",OracleType.VarChar),
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.EventID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.StartDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.EndDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = lmSystemLogEntity.HotelID;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = lmSystemLogEntity.CityID;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = lmSystemLogEntity.PayCode;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = lmSystemLogEntity.BookStatus;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = lmSystemLogEntity.PayStatus;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = lmSystemLogEntity.PlatForm;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = lmSystemLogEntity.Ticket;
            }

            //LOGINMOBILE
            if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = lmSystemLogEntity.Mobile;
            }

            //InStart
            if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
            {
                parm[11].Value = DBNull.Value;
            }
            else
            {
                parm[11].Value = lmSystemLogEntity.InStart;
            }

            //InEnd
            if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
            {
                parm[12].Value = DBNull.Value;
            }
            else
            {
                parm[12].Value = lmSystemLogEntity.InEnd;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
            {
                parm[13].Value = DBNull.Value;
            }
            else
            {
                if (!"BAR".Equals(lmSystemLogEntity.PayCode))
                {
                    parm[13].Value = lmSystemLogEntity.PayCode;
                }
                else
                {
                    parm[5].Value = "BAR";
                    parm[13].Value = "BARB";
                }
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_usergroup_review_select_new_count", true, parm);
            lmSystemLogEntity.QueryResult = dsResult;
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ChartExportLmOrderSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("ORDERSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.StartDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.EndDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                string strHotel = lmSystemLogEntity.HotelID;
                parm[3].Value = (strHotel.IndexOf(']') > 0) ? strHotel.Substring(0, strHotel.IndexOf(']')).Trim('[').Trim(']') : strHotel;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                string strCity = lmSystemLogEntity.CityID;
                parm[4].Value = (strCity.IndexOf(']') > 0) ? strCity.Substring(0, strCity.IndexOf(']')).Trim('[').Trim(']') : strCity;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = lmSystemLogEntity.PayCode.Replace("/", ",");
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = lmSystemLogEntity.BookStatus;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = lmSystemLogEntity.PayStatus;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = lmSystemLogEntity.Aprove;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = lmSystemLogEntity.HotelComfirm;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = lmSystemLogEntity.Ticket;
            }

            //LOGINMOBILE
            if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
            {
                parm[11].Value = DBNull.Value;
            }
            else
            {
                parm[11].Value = lmSystemLogEntity.Mobile;
            }

            //InStart
            if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
            {
                parm[12].Value = DBNull.Value;
            }
            else
            {
                parm[12].Value = lmSystemLogEntity.InStart;
            }

            //InEnd
            if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
            {
                parm[13].Value = DBNull.Value;
            }
            else
            {
                parm[13].Value = lmSystemLogEntity.InEnd;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
            {
                parm[14].Value = DBNull.Value;
            }
            else
            {
                parm[14].Value = lmSystemLogEntity.PlatForm;
            }

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", "ChartReviewLmOrderLogSelect");
            if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
            {
                SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
            }

            DataSet dsResult = DbHelperOra.Query(SqlString, true, parm);
            string strTimeSpace = lmSystemLogEntity.ShowType;

            lmSystemLogEntity.QueryResult = GetChartDataTable(dsResult, lmSystemLogEntity.ShowType);
            return lmSystemLogEntity;
        }

        private static DataSet GetChartDataTable(DataSet dsParm, string ChartType)
        {
            DataSet dsResult = new DataSet();
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Date");
            dtResult.Columns.Add("Volume");
            dsResult.Tables.Add(dtResult);

            if (dsParm.Tables.Count == 0 || dsParm.Tables[0].Rows.Count == 0)
            {
                return dsResult;
            }

            DateTime dtFinalEnd = DateTime.Parse(dsParm.Tables[0].Rows[dsParm.Tables[0].Rows.Count - 1]["CREATETIME"].ToString());
            DateTime StartTemp = DateTime.Parse(dsParm.Tables[0].Rows[0]["CREATETIME"].ToString());
            string strStart = "";
            if (ChartType == "1")
            {
                strStart = StartTemp.ToShortDateString() + " " + StartTemp.Hour + ":00:00";
            }
            else
            {
                strStart = StartTemp.ToShortDateString() + " 00:00:00";
            }

            DateTime dtStart = DateTime.Parse(strStart);
            DateTime dtEnd = dtStart.AddHours(double.Parse(ChartType));
            int iCount = 0;
            string condition = "";
            while (dtEnd <= dtFinalEnd)
            {
                condition = "CREATETIME >'" + dtStart.ToString() + "' and CREATETIME <='" + dtEnd.ToString() + "'";
                iCount = dsParm.Tables[0].Select(condition).Count();

                DataRow drRow = dtResult.NewRow();
                if (double.Parse(ChartType) > 1)
                {
                    drRow["Date"] = dtStart.ToShortDateString();
                }
                else
                {
                    drRow["Date"] = dtStart;
                }

                drRow["Volume"] = iCount;
                dtResult.Rows.Add(drRow);

                dtStart = dtEnd;
                dtEnd = dtStart.AddHours(double.Parse(ChartType));
            }

            condition = "CREATETIME >='" + dtStart.ToString() + "' and CREATETIME <='" + dtEnd.ToString() + "'";
            DataRow drFinalRow = dtResult.NewRow();
            if (double.Parse(ChartType) > 1)
            {
                drFinalRow["Date"] = dtStart.ToShortDateString();
            }
            else
            {
                drFinalRow["Date"] = dtStart;
            }

            iCount = dsParm.Tables[0].Select(condition).Count();
            drFinalRow["Volume"] = iCount;
            dtResult.Rows.Add(drFinalRow);
            return dsResult;
        }

        public static LmSystemLogEntity ExportLmOrderSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "ReviewLmOrderLogSelect";
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("ORDERSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("DashPopStatus",OracleType.VarChar),
                                    new OracleParameter("DASHSTARTDTIME",OracleType.VarChar),
                                    new OracleParameter("DASHENDDTIME",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("ORDERTYPESTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("ISRESERVE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAMES",OracleType.VarChar),
                                    new OracleParameter("OUTCC",OracleType.VarChar),
                                    new OracleParameter("OUTUC",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("OUTFAILORDER",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.EndDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.HotelID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.CityID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.PayStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.Aprove;
                }
                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[9].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[9].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[9].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Ticket;
                }

                //LOGINMOBILE
                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.Mobile;
                }

                //InStart
                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.InStart;
                }

                //InEnd
                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    strSQLString = "ReviewLmOrderLogSelectForSales";
                    //CommonDA.InsertEventHistory("取得销售人员开始");
                    parm[15].Value = SalesManagerHotelListSelect(lmSystemLogEntity.Sales);
                    //CommonDA.InsertEventHistory("取得销售人员结束");
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashPopStatus))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.DashPopStatus;
                }

                //if (String.IsNullOrEmpty(lmSystemLogEntity.DashInStart))
                //{
                //    parm[17].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[17].Value = lmSystemLogEntity.DashInStart;
                //}

                //if (String.IsNullOrEmpty(lmSystemLogEntity.DashInEnd))
                //{
                //    parm[18].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[18].Value = lmSystemLogEntity.DashInEnd;
                //}

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashStartDTime))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.DashStartDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashEndDTime))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.DashEndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderTypeStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.OrderTypeStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.OrderBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.OrderBookStatusOther;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.RestPriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.IsReserve))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.IsReserve;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[29].Value = DBNull.Value;
                }
                else
                {
                    parm[29].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutCC))
                {
                    parm[30].Value = DBNull.Value;
                }
                else
                {
                    parm[30].Value = lmSystemLogEntity.OutCC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutUC))
                {
                    parm[31].Value = DBNull.Value;
                }
                else
                {
                    parm[31].Value = lmSystemLogEntity.OutUC;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[32].Value = DBNull.Value;
                }
                else
                {
                    parm[32].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutFailOrder))
                {
                    parm[33].Value = DBNull.Value;
                }
                else
                {
                    parm[33].Value = lmSystemLogEntity.OutFailOrder;
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSQLString);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始");
                DataSet dsResult = DbHelperOra.Query(SqlString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史开始");
                lmSystemLogEntity.QueryResult = dsResult;
            }
            else
            {
                //OracleParameter[] parm ={
                //                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                //                    new OracleParameter("PAYCODE",OracleType.VarChar)
                //                };
                //if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                //{
                //    parm[0].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[0].Value = lmSystemLogEntity.TicketType;
                //}

                //if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                //{
                //    parm[1].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[1].Value = lmSystemLogEntity.TicketPayCode;
                //}

                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.PackageName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.AmountFrom;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.AmountTo;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.PickfromDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.PicktoDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.TicketType;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.TicketTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.TicketPayCode;
                }

                string strSqlName = "";
                if ("1".Equals(lmSystemLogEntity.TicketData))
                {
                    strSqlName = "ReviewLmOrderLogTicketOrdData";
                }
                else
                {
                    strSqlName = "ReviewLmOrderLogTicketAllData";
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSqlName);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }

                DataSet dsResult = DbHelperOra.Query(SqlString, true, parm);
                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelectCount(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "ReviewLmOrderLogSelectCount";
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("ORDERSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("DashPopStatus",OracleType.VarChar),
                                    new OracleParameter("DASHSTARTDTIME",OracleType.VarChar),
                                    new OracleParameter("DASHENDDTIME",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("ORDERTYPESTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("ISRESERVE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAMES",OracleType.VarChar),
                                    new OracleParameter("OUTCC",OracleType.VarChar),
                                    new OracleParameter("OUTUC",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("OUTFAILORDER",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.EndDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.HotelID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.CityID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.PayStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.Aprove;
                }
                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[9].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[9].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[9].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Ticket;
                }

                //LOGINMOBILE
                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.Mobile;
                }

                //InStart
                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.InStart;
                }

                //InEnd
                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    //CommonDA.InsertEventHistory("取得销售人员开始-count");
                    parm[15].Value = SalesManagerHotelListSelect(lmSystemLogEntity.Sales);
                    //CommonDA.InsertEventHistory("取得销售人员结束-count");
                    strSQLString = "ReviewLmOrderLogSelectCountForSales";
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashPopStatus))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.DashPopStatus;
                }

                //if (String.IsNullOrEmpty(lmSystemLogEntity.DashInStart))
                //{
                //    parm[17].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[17].Value = lmSystemLogEntity.DashInStart;
                //}

                //if (String.IsNullOrEmpty(lmSystemLogEntity.DashInEnd))
                //{
                //    parm[18].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[18].Value = lmSystemLogEntity.DashInEnd;
                //}

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashStartDTime))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.DashStartDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashEndDTime))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.DashEndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderTypeStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.OrderTypeStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.OrderBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.OrderBookStatusOther;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.RestPriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.IsReserve))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.IsReserve;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[29].Value = DBNull.Value;
                }
                else
                {
                    parm[29].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutCC))
                {
                    parm[30].Value = DBNull.Value;
                }
                else
                {
                    parm[30].Value = lmSystemLogEntity.OutCC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutUC))
                {
                    parm[31].Value = DBNull.Value;
                }
                else
                {
                    parm[31].Value = lmSystemLogEntity.OutUC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[32].Value = DBNull.Value;
                }
                else
                {
                    parm[32].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutFailOrder))
                {
                    parm[33].Value = DBNull.Value;
                }
                else
                {
                    parm[33].Value = lmSystemLogEntity.OutFailOrder;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            else
            {
                //OracleParameter[] parm ={
                //                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                //                    new OracleParameter("PAYCODE",OracleType.VarChar)
                //                };
                //if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                //{
                //    parm[0].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[0].Value = lmSystemLogEntity.TicketType;
                //}

                //if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                //{
                //    parm[1].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[1].Value = lmSystemLogEntity.TicketPayCode;
                //}
                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.PackageName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.AmountFrom;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.AmountTo;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.PickfromDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.PicktoDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.TicketType;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.TicketTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.TicketPayCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.GroupID;
                }
                string strSql = "";
                if ("1".Equals(lmSystemLogEntity.TicketData))
                {
                    strSql = "ReviewLmOrderLogTicketOrdCount";
                }
                else
                {
                    strSql = "ReviewLmOrderLogTicketAllCount";
                }

                DataSet dsResult = DbManager.Query("LmSystemLog", strSql, true, parm);
                lmSystemLogEntity.QueryResult = dsResult;
            }

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "ReviewLmOrderLogSelect";
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("ORDERSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("DashPopStatus",OracleType.VarChar),
                                    new OracleParameter("DASHSTARTDTIME",OracleType.VarChar),
                                    new OracleParameter("DASHENDDTIME",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("ORDERTYPESTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("ISRESERVE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAMES",OracleType.VarChar),
                                    new OracleParameter("OUTCC",OracleType.VarChar),
                                    new OracleParameter("OUTUC",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("OUTFAILORDER",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.EndDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.HotelID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.CityID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.PayStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.Aprove;
                }

                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[9].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[9].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[9].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Ticket;
                }

                //LOGINMOBILE
                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.Mobile;
                }

                //InStart
                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.InStart;
                }

                //InEnd
                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    strSQLString = "ReviewLmOrderLogSelectForSales";
                    //CommonDA.InsertEventHistory("取得销售人员开始");
                    parm[15].Value = SalesManagerHotelListSelect(lmSystemLogEntity.Sales);
                    //CommonDA.InsertEventHistory("取得销售人员结束");
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashPopStatus))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.DashPopStatus;
                }

                //if (String.IsNullOrEmpty(lmSystemLogEntity.DashInStart))
                //{
                //    parm[17].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[17].Value = lmSystemLogEntity.DashInStart;
                //}

                //if (String.IsNullOrEmpty(lmSystemLogEntity.DashInEnd))
                //{
                //    parm[18].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[18].Value = lmSystemLogEntity.DashInEnd;
                //}

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashStartDTime))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.DashStartDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashEndDTime))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.DashEndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderTypeStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.OrderTypeStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.RestPriceCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.IsReserve))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.IsReserve;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[29].Value = DBNull.Value;
                }
                else
                {
                    parm[29].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutCC))
                {
                    parm[30].Value = DBNull.Value;
                }
                else
                {
                    parm[30].Value = lmSystemLogEntity.OutCC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutUC))
                {
                    parm[31].Value = DBNull.Value;
                }
                else
                {
                    parm[31].Value = lmSystemLogEntity.OutUC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[32].Value = DBNull.Value;
                }
                else
                {
                    parm[32].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutFailOrder))
                {
                    parm[33].Value = DBNull.Value;
                }
                else
                {
                    parm[33].Value = lmSystemLogEntity.OutFailOrder;
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSQLString);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始");
                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
                //CommonDA.InsertEventHistory("取得订单历史开始");
                lmSystemLogEntity.QueryResult = dsResult;
            }
            else
            {
                //OracleParameter[] parm ={
                //                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                //                    new OracleParameter("PAYCODE",OracleType.VarChar)
                //                };
                //if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                //{
                //    parm[0].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[0].Value = lmSystemLogEntity.TicketType;
                //}

                //if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                //{
                //    parm[1].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[1].Value = lmSystemLogEntity.TicketPayCode;
                //}

                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.PackageName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.AmountFrom;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.AmountTo;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.PickfromDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.PicktoDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.TicketType;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.TicketTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.TicketPayCode;
                }

                string strSqlName = "";
                if ("1".Equals(lmSystemLogEntity.TicketData))
                {
                    strSqlName = "ReviewLmOrderLogTicketOrdData";
                }
                else
                {
                    strSqlName = "ReviewLmOrderLogTicketAllData";
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSqlName);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }

                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReviewLmOrderSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PAYCODE2",OracleType.VarChar)
                                };//,                                    new OracleParameter("SORTFIELD",OracleType.Int16)
            if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.EventID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.StartDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.EndDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = lmSystemLogEntity.HotelID;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = lmSystemLogEntity.CityID;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = lmSystemLogEntity.PayCode;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = lmSystemLogEntity.BookStatus;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = lmSystemLogEntity.PayStatus;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = lmSystemLogEntity.PlatForm;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = lmSystemLogEntity.Ticket;
            }

            //LOGINMOBILE
            if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = lmSystemLogEntity.Mobile;
            }

            //InStart
            if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
            {
                parm[11].Value = DBNull.Value;
            }
            else
            {
                parm[11].Value = lmSystemLogEntity.InStart;
            }

            //InEnd
            if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
            {
                parm[12].Value = DBNull.Value;
            }
            else
            {
                parm[12].Value = lmSystemLogEntity.InEnd;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
            {
                parm[13].Value = DBNull.Value;
            }
            else
            {
                if (!"BAR".Equals(lmSystemLogEntity.PayCode))
                {
                    parm[13].Value = lmSystemLogEntity.PayCode;
                }
                else
                {
                    parm[5].Value = "BAR";
                    parm[13].Value = "BARB";
                }
            }

            //parm[14].Value = lmSystemLogEntity.SortField;

            //DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_usergroup_review_select_new", parm);
            /*king*/
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", "ReviewLmOrderSelect");

            if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
            {
                SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);

            string strDtTemp = "";
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                strDtTemp = dsResult.Tables[0].Rows[i]["UCCANEL"].ToString();

                if (!String.IsNullOrEmpty(strDtTemp) && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["SCCANEL"].ToString()) && DateTime.Parse(strDtTemp) > DateTime.Parse(dsResult.Tables[0].Rows[i]["SCCANEL"].ToString()))
                {
                    strDtTemp = dsResult.Tables[0].Rows[i]["SCCANEL"].ToString();
                }

                if (!String.IsNullOrEmpty(strDtTemp) && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["CCCANEL"].ToString()) && DateTime.Parse(strDtTemp) > DateTime.Parse(dsResult.Tables[0].Rows[i]["CCCANEL"].ToString()))
                {
                    strDtTemp = dsResult.Tables[0].Rows[i]["CCCANEL"].ToString();
                }

                dsResult.Tables[0].Rows[i]["FOGCANCELR"] = strDtTemp;
                //FOGCANCELR  UCCANEL  SCCANEL  CCCANEL
            }
            lmSystemLogEntity.QueryResult = dsResult;
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReviewSelectByNew(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PAYCODE2",OracleType.VarChar),
                                    new OracleParameter("AUDITSTATUS",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.EventID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.StartDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.EndDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = lmSystemLogEntity.HotelID;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = lmSystemLogEntity.CityID;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = lmSystemLogEntity.PayCode;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = lmSystemLogEntity.BookStatus;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = lmSystemLogEntity.PayStatus;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = lmSystemLogEntity.PlatForm;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = lmSystemLogEntity.Ticket;
            }

            //LOGINMOBILE
            if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = lmSystemLogEntity.Mobile;
            }

            //InStart
            if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
            {
                parm[11].Value = DBNull.Value;
            }
            else
            {
                parm[11].Value = lmSystemLogEntity.InStart;
            }

            //InEnd
            if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
            {
                parm[12].Value = DBNull.Value;
            }
            else
            {
                parm[12].Value = lmSystemLogEntity.InEnd;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
            {
                parm[13].Value = DBNull.Value;
            }
            else
            {
                if (!"BAR".Equals(lmSystemLogEntity.PayCode))
                {
                    parm[13].Value = lmSystemLogEntity.PayCode;
                }
                else
                {
                    parm[5].Value = "BAR";
                    parm[13].Value = "BARB";
                }
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
            {
                parm[14].Value = DBNull.Value;
            }
            else
            {
                parm[14].Value = lmSystemLogEntity.Aprove;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_usergroup_review_select_new", true, parm);
            /*king*/
            //DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_usergroup_review_select_new", parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize);

            string strDtTemp = "";
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                strDtTemp = dsResult.Tables[0].Rows[i]["UCCANEL"].ToString();

                if (!String.IsNullOrEmpty(strDtTemp) && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["SCCANEL"].ToString()) && DateTime.Parse(strDtTemp) > DateTime.Parse(dsResult.Tables[0].Rows[i]["SCCANEL"].ToString()))
                {
                    strDtTemp = dsResult.Tables[0].Rows[i]["SCCANEL"].ToString();
                }

                if (!String.IsNullOrEmpty(strDtTemp) && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["CCCANEL"].ToString()) && DateTime.Parse(strDtTemp) > DateTime.Parse(dsResult.Tables[0].Rows[i]["CCCANEL"].ToString()))
                {
                    strDtTemp = dsResult.Tables[0].Rows[i]["CCCANEL"].ToString();
                }

                dsResult.Tables[0].Rows[i]["FOGCANCELR"] = strDtTemp;
                //FOGCANCELR  UCCANEL  SCCANEL  CCCANEL
            }
            lmSystemLogEntity.QueryResult = dsResult;
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity UserDetailListSelectByNew(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.EventID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_usergroup_detail_select_new", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity UserGridViewPlanDetail(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.EventID;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_order_detail_plan_his", true, parm);

            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                dsResult.Tables[0].Rows[i]["OPEMSG"] = dsResult.Tables[0].Rows[i]["OPEMSG"].ToString().Replace("{", "").Replace("}", "").Replace("isRoomful", "标记满房").Replace("effectHour", "上线时间").Replace("breakfastNum", "早餐数量").Replace("isNetwork", "免费宽带").Replace("status", "上线状态").Replace("source", "供应商").Replace("initRoomNum", "初始房量").Replace("moneyType", "货币类型").Replace("roomNum", "房量").Replace("twoPrice", "价格").Replace("isReserve=0", "保留房=是").Replace("isReserve=1", "保留房=否").Replace("isReserve", "保留房").Replace("订单号", "订单号=");
            }

            lmSystemLogEntity.QueryResult = dsResult;
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity UserMainListSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.EventID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_usergroup_main_select", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderOperationSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderComfirmManagerDetail(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            //lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_detail_select", true, parm);
            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_detail_select_all", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderComfirmManagerDetailRestVal(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_detail_rest_select", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ChkOrderOperationSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select_chk", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderOperationSelectByPrint(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select_byprint", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderComfirmByPrint(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_comfirm_byprint", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderComfirmPlanByPrint(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.HotelID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.PriceCode))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.PriceCode;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_comfirm_plan_byprint", true, parm);
            return lmSystemLogEntity;
        }

        public static bool ChkOrderOperationLock(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_check", true, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                if ("2".Equals(dsResult.Tables[0].Rows[0]["status"].ToString().Trim()) && !lmSystemLogEntity.LogMessages.Userid.Equals(dsResult.Tables[0].Rows[0]["ope_user"].ToString().Trim()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ChkHotelExRemark(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.HotelID;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_hotel_ex_remark_check", true, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static LmSystemLogEntity ChkOrderOperationControl(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_check", true, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                if ("2".Equals(dsResult.Tables[0].Rows[0]["status"].ToString().Trim()) && !lmSystemLogEntity.LogMessages.Userid.Equals(dsResult.Tables[0].Rows[0]["ope_user"].ToString().Trim()))
                {
                    lmSystemLogEntity.ErrorMSG = string.Format("该订单正在由{0}操作中，您将不能更改订单状态。", dsResult.Tables[0].Rows[0]["ope_user"].ToString().Trim());
                }
                else
                {
                    OracleParameter[] lmparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };
                    if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                    {
                        lmparm[0].Value = DBNull.Value;
                    }
                    else
                    {
                        lmparm[0].Value = lmSystemLogEntity.FogOrderID;
                    }

                    lmparm[1].Value = lmSystemLogEntity.LogMessages.Userid;

                    HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_lock", lmparm);
                }
            }

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ChkOrderConfirmControl(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_check", true, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                if ("2".Equals(dsResult.Tables[0].Rows[0]["status"].ToString().Trim()) && !lmSystemLogEntity.LogMessages.Userid.Equals(dsResult.Tables[0].Rows[0]["ope_user"].ToString().Trim()))
                {
                    lmSystemLogEntity.ErrorMSG = dsResult.Tables[0].Rows[0]["ope_user"].ToString().Trim();
                }
                else
                {
                    OracleParameter[] lmparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };
                    if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                    {
                        lmparm[0].Value = DBNull.Value;
                    }
                    else
                    {
                        lmparm[0].Value = lmSystemLogEntity.FogOrderID;
                    }

                    lmparm[1].Value = lmSystemLogEntity.LogMessages.Userid;

                    HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_lock", lmparm);
                }
            }

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity UnlockOrderConfirmControl(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] lmparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("OPEUSER",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            lmparm[1].Value = lmSystemLogEntity.LogMessages.Userid;
            HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_lock", lmparm);
            return lmSystemLogEntity;
        }

        public static int SaveOrderOpeRemark(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] lmparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            DataSet dsOrderInfo = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select", true, lmparm);

            if (dsOrderInfo.Tables.Count == 0 || dsOrderInfo.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            string strPriceCode = dsOrderInfo.Tables[0].Rows[0]["price_code"].ToString().Trim();
            string strBookStatus = dsOrderInfo.Tables[0].Rows[0]["BOOK_STATUS"].ToString().Trim();
            string strBookStatusOther = dsOrderInfo.Tables[0].Rows[0]["book_status_other"].ToString().Trim();
            string strFogresvtype = dsOrderInfo.Tables[0].Rows[0]["fog_resvtype"].ToString().Trim();
            string strFogresvstatus = dsOrderInfo.Tables[0].Rows[0]["fog_resvstatus"].ToString().Trim();
            string strFogauditstatus = dsOrderInfo.Tables[0].Rows[0]["fog_auditstatus"].ToString().Trim();
            string strOrdercanclereason = dsOrderInfo.Tables[0].Rows[0]["ORDER_CANCLE_REASON"].ToString().Trim();
            string strBookremark = dsOrderInfo.Tables[0].Rows[0]["BOOK_REMARK"].ToString().Trim();
            string strPaystatus = dsOrderInfo.Tables[0].Rows[0]["pay_status"].ToString().Trim();
            //string strConfirmnum = dsOrderInfo.Tables[0].Rows[0]["CONFIRM_NUM"].ToString().Trim();

            string strConfirmnum = ("8".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.ActionID : dsOrderInfo.Tables[0].Rows[0]["CONFIRM_NUM"].ToString().Trim();

            strBookremark = String.IsNullOrEmpty(lmSystemLogEntity.BookRemark) ? strBookremark : lmSystemLogEntity.BookRemark;
            if ("LMBAR".Equals(strPriceCode.Trim().ToUpper()))
            {
                strBookStatus = "4";
                strFogresvtype = "c";
                strOrdercanclereason = "";
            }
            else
            {
                switch (lmSystemLogEntity.OrderBookStatus)
                {
                    case "4":
                        strBookStatusOther = "4";
                        strFogresvtype = "n";
                        strFogresvstatus = "1";
                        strOrdercanclereason = "";
                        break;
                    case "5":
                        strBookStatusOther = "5";
                        strFogauditstatus = "5";
                        strOrdercanclereason = "";
                        break;
                    case "7":
                        strBookStatusOther = "7";
                        strFogauditstatus = "7";
                        strOrdercanclereason = "";
                        break;
                    case "8":
                        strBookStatusOther = "8";
                        strFogauditstatus = "8";
                        strOrdercanclereason = "";
                        break;
                    case "9":
                        strBookStatusOther = "9";
                        strFogresvtype = "c";
                        strOrdercanclereason = lmSystemLogEntity.CanelReson;
                        break;
                    default:
                        break;
                }
            }

            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("FOGRESVTYPE",OracleType.VarChar),
                                    new OracleParameter("FOGRESVSTATUS",OracleType.VarChar),
                                    new OracleParameter("FOGAUDITSTATUS",OracleType.VarChar),
                                    new OracleParameter("ORDERCANCLEREASON",OracleType.VarChar),
                                    new OracleParameter("BOOKREMARK",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("CONFIRMNUM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            parm[1].Value = strBookStatus;
            parm[2].Value = strBookStatusOther;
            parm[3].Value = strFogresvtype;
            parm[4].Value = strFogresvstatus;
            parm[5].Value = strFogauditstatus;
            parm[6].Value = strOrdercanclereason;
            parm[7].Value = strBookremark;
            parm[8].Value = lmSystemLogEntity.LogMessages.Username;
            parm[9].Value = strPaystatus;
            parm[10].Value = strConfirmnum;

            HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_operation_save", parm);

            return 1;
            //OracleParameter[] lmparm ={
            //                        new OracleParameter("ORDERID",OracleType.VarChar)
            //                    };
            //if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            //{
            //    lmparm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmparm[0].Value = lmSystemLogEntity.FogOrderID;
            //}

            //DataSet dsOrderInfo = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select", true, lmparm);
            //if (dsOrderInfo.Tables.Count == 0 || dsOrderInfo.Tables[0].Rows.Count == 0)
            //{
            //    return 0;
            //}

            //string strPriceCode = dsOrderInfo.Tables[0].Rows[0]["price_code"].ToString().Trim();
            //string strBookStatus = dsOrderInfo.Tables[0].Rows[0]["BOOK_STATUS"].ToString().Trim();
            //string strBookStatusOther = dsOrderInfo.Tables[0].Rows[0]["book_status_other"].ToString().Trim();
            //string strFogresvtype = dsOrderInfo.Tables[0].Rows[0]["fog_resvtype"].ToString().Trim();
            //string strFogresvstatus = dsOrderInfo.Tables[0].Rows[0]["fog_resvstatus"].ToString().Trim();
            //string strFogauditstatus = dsOrderInfo.Tables[0].Rows[0]["fog_auditstatus"].ToString().Trim();
            //string strOrdercanclereason = dsOrderInfo.Tables[0].Rows[0]["ORDER_CANCLE_REASON"].ToString().Trim();
            //string strBookremark = dsOrderInfo.Tables[0].Rows[0]["BOOK_REMARK"].ToString().Trim();
            //string strPaystatus = dsOrderInfo.Tables[0].Rows[0]["pay_status"].ToString().Trim();
            //strBookremark = lmSystemLogEntity.BookRemark;

            //OracleParameter[] parm ={
            //                        new OracleParameter("ORDERID",OracleType.VarChar),
            //                        new OracleParameter("BOOKSTATUS",OracleType.VarChar),
            //                        new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
            //                        new OracleParameter("FOGRESVTYPE",OracleType.VarChar),
            //                        new OracleParameter("FOGRESVSTATUS",OracleType.VarChar),
            //                        new OracleParameter("FOGAUDITSTATUS",OracleType.VarChar),
            //                        new OracleParameter("ORDERCANCLEREASON",OracleType.VarChar),
            //                        new OracleParameter("BOOKREMARK",OracleType.VarChar),
            //                        new OracleParameter("CREATEUSER",OracleType.VarChar),
            //                        new OracleParameter("PAYSTATUS",OracleType.VarChar)
            //                    };
            //if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            //{
            //    parm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[0].Value = lmSystemLogEntity.FogOrderID;
            //}

            //parm[1].Value = strBookStatus;
            //parm[2].Value = strBookStatusOther;
            //parm[3].Value = strFogresvtype;
            //parm[4].Value = strFogresvstatus;
            //parm[5].Value = strFogauditstatus;
            //parm[6].Value = strOrdercanclereason;
            //parm[7].Value = strBookremark;
            //parm[8].Value = lmSystemLogEntity.LogMessages.Username;
            //parm[9].Value = strPaystatus;
            //HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_operation_save", parm);
            //return 1;
        }

        public static DataSet GetSaveOrderOpeRemarkList(string FogOrderID)
        {
            OracleParameter[] lmparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(FogOrderID))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = FogOrderID;
            }

            return HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select", true, lmparm);
        }

        public static int SaveOrderOpeRemarkListConfirm(LmSystemLogEntity lmSystemLogEntity, DataSet dsResult)
        {
            //OracleParameter[] lmparm ={
            //                        new OracleParameter("ORDERID",OracleType.VarChar)
            //                    };
            //if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            //{
            //    lmparm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmparm[0].Value = lmSystemLogEntity.FogOrderID;
            //}

            //DataSet dsOrderInfo = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select", true, lmparm);

            DataSet dsOrderInfo = dsResult;

            if (dsOrderInfo.Tables.Count == 0 || dsOrderInfo.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            string strPriceCode = dsOrderInfo.Tables[0].Rows[0]["price_code"].ToString().Trim();
            string strBookStatus = dsOrderInfo.Tables[0].Rows[0]["BOOK_STATUS"].ToString().Trim();
            string strBookStatusOther = dsOrderInfo.Tables[0].Rows[0]["book_status_other"].ToString().Trim();
            string strFogresvtype = dsOrderInfo.Tables[0].Rows[0]["fog_resvtype"].ToString().Trim();
            string strFogresvstatus = dsOrderInfo.Tables[0].Rows[0]["fog_resvstatus"].ToString().Trim();
            string strFogauditstatus = dsOrderInfo.Tables[0].Rows[0]["fog_auditstatus"].ToString().Trim();
            string strOrdercanclereason = dsOrderInfo.Tables[0].Rows[0]["ORDER_CANCLE_REASON"].ToString().Trim();
            string strBookremark = dsOrderInfo.Tables[0].Rows[0]["BOOK_REMARK"].ToString().Trim();
            string strPaystatus = dsOrderInfo.Tables[0].Rows[0]["pay_status"].ToString().Trim();
            string strConfirmnum = String.IsNullOrEmpty(lmSystemLogEntity.ActionID) ? dsOrderInfo.Tables[0].Rows[0]["CONFIRM_NUM"].ToString().Trim() : lmSystemLogEntity.ActionID;

            string strApproveId = dsOrderInfo.Tables[0].Rows[0]["APPROVE_NUM"].ToString().Trim();
            string strInRoomnum = dsOrderInfo.Tables[0].Rows[0]["INROOM_NUM"].ToString().Trim();
            string strordernsreason = dsOrderInfo.Tables[0].Rows[0]["ORDER_NS_REASON"].ToString().Trim();
            string strvendor = (String.IsNullOrEmpty(dsOrderInfo.Tables[0].Rows[0]["VENDOR"].ToString().Trim())) ? "HUBS1" : dsOrderInfo.Tables[0].Rows[0]["VENDOR"].ToString().Trim();

            strBookremark = String.IsNullOrEmpty(lmSystemLogEntity.BookRemark) ? strBookremark : lmSystemLogEntity.BookRemark;
            if ("LMBAR".Equals(strPriceCode.Trim().ToUpper()))
            {
                if ("6".Equals(lmSystemLogEntity.OrderBookStatus))//CC确认
                {
                    strBookStatus = "6";
                    strFogresvtype = "n";
                    strFogresvstatus = "1";
                }
                else if ("7".Equals(lmSystemLogEntity.OrderBookStatus))//CC取消
                {
                    strBookStatus = "7";
                    strFogresvtype = "c";
                    strOrdercanclereason = "";
                    strFogresvstatus = "1";
                    strPaystatus = "10";
                }
                //else//备注
                //{
                //    strApproveId = (!"5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.ApproveId : "";
                //    strInRoomnum = (!"5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.InRoomID : "";
                //    strFogauditstatus = lmSystemLogEntity.OrderBookStatus;
                //    strordernsreason = ("5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.CanelReson : "";
                //}
                 

                //if ("4".Equals(lmSystemLogEntity.OrderBookStatus))//CC取消
                //{
                //    strBookStatus = "4";
                //    strFogresvtype = "c";
                //    strOrdercanclereason = "";
                //    strFogresvstatus = "1";
                //}
                //else  //备注
                //{
                //    strApproveId = (!"5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.ApproveId : "";
                //    strInRoomnum = (!"5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.InRoomID : "";
                //    strFogauditstatus = lmSystemLogEntity.OrderBookStatus;
                //    strordernsreason = ("5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.CanelReson : "";
                //}
            }
            else
            {
                switch (lmSystemLogEntity.OrderBookStatusOther)
                {
                    case "4":
                        strBookStatusOther = "4";
                        strFogresvtype = "n";
                        strFogresvstatus = "1";
                        break;
                    case "5":
                        strBookStatusOther = "5";
                        strFogauditstatus = "5";
                        strOrdercanclereason = "";
                        strordernsreason = lmSystemLogEntity.CanelReson;
                        strApproveId = "";
                        strInRoomnum = "";
                        break;
                    case "7":
                        strBookStatusOther = "7";
                        strFogauditstatus = "7";
                        break;
                    case "8":
                        strBookStatusOther = "8";
                        strFogauditstatus = "8";
                        strOrdercanclereason = "";
                        strordernsreason = "";
                        strApproveId = lmSystemLogEntity.ApproveId;
                        strInRoomnum = lmSystemLogEntity.InRoomID;
                        break;
                    case "9":
                        strBookStatusOther = "9";
                        strFogresvtype = "c";
                        strFogresvstatus = "1";
                        strOrdercanclereason = lmSystemLogEntity.CanelReson;
                        break;
                    default:
                        break;
                }
            }

            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("FOGRESVTYPE",OracleType.VarChar),
                                    new OracleParameter("FOGRESVSTATUS",OracleType.VarChar),
                                    new OracleParameter("FOGAUDITSTATUS",OracleType.VarChar),
                                    new OracleParameter("ORDERCANCLEREASON",OracleType.VarChar),
                                    new OracleParameter("BOOKREMARK",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("CONFIRMNUM",OracleType.VarChar),
                                    new OracleParameter("APPROVENUM",OracleType.VarChar),
                                    new OracleParameter("INROOMNUM",OracleType.VarChar),
                                    new OracleParameter("ORDERNSREASON",OracleType.VarChar),
                                    new OracleParameter("VENDOR",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            parm[1].Value = strBookStatus;
            parm[2].Value = strBookStatusOther;
            parm[3].Value = strFogresvtype;
            parm[4].Value = strFogresvstatus;
            parm[5].Value = strFogauditstatus;
            parm[6].Value = strOrdercanclereason;
            parm[7].Value = strBookremark;
            parm[8].Value = lmSystemLogEntity.LogMessages.Username;
            parm[9].Value = strPaystatus;
            parm[10].Value = strConfirmnum;

            parm[11].Value = strApproveId;
            parm[12].Value = strInRoomnum;
            parm[13].Value = strordernsreason;
            parm[14].Value = strvendor;

            HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_operation_appsave", parm);

            if (!String.IsNullOrEmpty(lmSystemLogEntity.FollowUp))
            {
                OracleParameter[] lmsparm ={
                                        new OracleParameter("ORDERID",OracleType.VarChar),
                                        new OracleParameter("FOLLOWUP",OracleType.VarChar),
                                        new OracleParameter("CREATEUSER",OracleType.VarChar)
                                    };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    lmsparm[0].Value = DBNull.Value;
                }
                else
                {
                    lmsparm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                lmsparm[1].Value = lmSystemLogEntity.FollowUp;
                lmsparm[2].Value = lmSystemLogEntity.LogMessages.Userid;

                HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_save", lmsparm);
            }
            return 1;
        }

        public static int SaveOrderOpeRemarkList(LmSystemLogEntity lmSystemLogEntity)
        {
            //OracleParameter[] lmparm ={
            //                        new OracleParameter("ORDERID",OracleType.VarChar)
            //                    };
            //if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            //{
            //    lmparm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmparm[0].Value = lmSystemLogEntity.FogOrderID;
            //}

            //DataSet dsOrderInfo = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select", true, lmparm);

            DataSet dsOrderInfo = GetSaveOrderOpeRemarkList(lmSystemLogEntity.FogOrderID);

            if (dsOrderInfo.Tables.Count == 0 || dsOrderInfo.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            string strPriceCode = dsOrderInfo.Tables[0].Rows[0]["price_code"].ToString().Trim();
            string strBookStatus = dsOrderInfo.Tables[0].Rows[0]["BOOK_STATUS"].ToString().Trim();
            string strBookStatusOther = dsOrderInfo.Tables[0].Rows[0]["book_status_other"].ToString().Trim();
            string strFogresvtype = dsOrderInfo.Tables[0].Rows[0]["fog_resvtype"].ToString().Trim();
            string strFogresvstatus = dsOrderInfo.Tables[0].Rows[0]["fog_resvstatus"].ToString().Trim();
            string strFogauditstatus = dsOrderInfo.Tables[0].Rows[0]["fog_auditstatus"].ToString().Trim();
            string strOrdercanclereason = dsOrderInfo.Tables[0].Rows[0]["ORDER_CANCLE_REASON"].ToString().Trim();
            string strBookremark = dsOrderInfo.Tables[0].Rows[0]["BOOK_REMARK"].ToString().Trim();
            string strPaystatus = dsOrderInfo.Tables[0].Rows[0]["pay_status"].ToString().Trim();
            string strConfirmnum = String.IsNullOrEmpty(lmSystemLogEntity.ActionID) ? dsOrderInfo.Tables[0].Rows[0]["CONFIRM_NUM"].ToString().Trim() : lmSystemLogEntity.ActionID;

            string strApproveId = dsOrderInfo.Tables[0].Rows[0]["APPROVE_NUM"].ToString().Trim();
            string strInRoomnum = dsOrderInfo.Tables[0].Rows[0]["INROOM_NUM"].ToString().Trim();
            string strordernsreason = dsOrderInfo.Tables[0].Rows[0]["ORDER_NS_REASON"].ToString().Trim();
            string strvendor = (String.IsNullOrEmpty(dsOrderInfo.Tables[0].Rows[0]["VENDOR"].ToString().Trim())) ? "HUBS1" : dsOrderInfo.Tables[0].Rows[0]["VENDOR"].ToString().Trim();

            strBookremark = String.IsNullOrEmpty(lmSystemLogEntity.BookRemark) ? strBookremark : lmSystemLogEntity.BookRemark;
            if ("LMBAR".Equals(strPriceCode.Trim().ToUpper()))
            {
                if ("4".Equals(lmSystemLogEntity.OrderBookStatus))
                {
                    strBookStatus = "4";
                    strFogresvtype = "c";
                    strOrdercanclereason = "";
                    strFogresvstatus = "1";
                }
                else
                {
                    strApproveId = (!"5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.ApproveId : "";
                    strInRoomnum = (!"5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.InRoomID : "";
                    strFogauditstatus = lmSystemLogEntity.OrderBookStatus;
                    strordernsreason = ("5".Equals(lmSystemLogEntity.OrderBookStatus)) ? lmSystemLogEntity.CanelReson : "";
                }
            }
            else
            {
                switch (lmSystemLogEntity.OrderBookStatus)
                {
                    case "4":
                        strBookStatusOther = "4";
                        strFogresvtype = "n";
                        strFogresvstatus = "1";
                        break;
                    case "5":
                        strBookStatusOther = "5";
                        strFogauditstatus = "5";
                        strOrdercanclereason = "";
                        strordernsreason = lmSystemLogEntity.CanelReson;
                        strApproveId = "";
                        strInRoomnum = "";
                        break;
                    case "7":
                        strBookStatusOther = "7";
                        strFogauditstatus = "7";
                        break;
                    case "8":
                        strBookStatusOther = "8";
                        strFogauditstatus = "8";
                        strOrdercanclereason = "";
                        strordernsreason = "";
                        strApproveId = lmSystemLogEntity.ApproveId;
                        strInRoomnum = lmSystemLogEntity.InRoomID;
                        break;
                    case "9":
                        strBookStatusOther = "9";
                        strFogresvtype = "c";
                        strFogresvstatus = "1";
                        strOrdercanclereason = lmSystemLogEntity.CanelReson;
                        break;
                    default:
                        break;
                }
            }

            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("FOGRESVTYPE",OracleType.VarChar),
                                    new OracleParameter("FOGRESVSTATUS",OracleType.VarChar),
                                    new OracleParameter("FOGAUDITSTATUS",OracleType.VarChar),
                                    new OracleParameter("ORDERCANCLEREASON",OracleType.VarChar),
                                    new OracleParameter("BOOKREMARK",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("CONFIRMNUM",OracleType.VarChar),
                                    new OracleParameter("APPROVENUM",OracleType.VarChar),
                                    new OracleParameter("INROOMNUM",OracleType.VarChar),
                                    new OracleParameter("ORDERNSREASON",OracleType.VarChar),
                                    new OracleParameter("VENDOR",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            parm[1].Value = strBookStatus;
            parm[2].Value = strBookStatusOther;
            parm[3].Value = strFogresvtype;
            parm[4].Value = strFogresvstatus;
            parm[5].Value = strFogauditstatus;
            parm[6].Value = strOrdercanclereason;
            parm[7].Value = strBookremark;
            parm[8].Value = lmSystemLogEntity.LogMessages.Username;
            parm[9].Value = strPaystatus;
            parm[10].Value = strConfirmnum;

            parm[11].Value = strApproveId;
            parm[12].Value = strInRoomnum;
            parm[13].Value = strordernsreason;
            parm[14].Value = strvendor;

            HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_operation_appsave", parm);

            if (!String.IsNullOrEmpty(lmSystemLogEntity.FollowUp))
            {
                OracleParameter[] lmsparm ={
                                        new OracleParameter("ORDERID",OracleType.VarChar),
                                        new OracleParameter("FOLLOWUP",OracleType.VarChar),
                                        new OracleParameter("CREATEUSER",OracleType.VarChar)
                                    };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    lmsparm[0].Value = DBNull.Value;
                }
                else
                {
                    lmsparm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                lmsparm[1].Value = lmSystemLogEntity.FollowUp;
                lmsparm[2].Value = lmSystemLogEntity.LogMessages.Userid;

                HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_save", lmsparm);
            }
            return 1;
        }

        public static int SaveHotelExRemark(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] lmsparm ={
                                        new OracleParameter("HOTELID",OracleType.VarChar),
                                        new OracleParameter("REMARK",OracleType.VarChar),
                                        new OracleParameter("CREATEUSER",OracleType.VarChar)
                                    };
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                lmsparm[0].Value = DBNull.Value;
            }
            else
            {
                lmsparm[0].Value = lmSystemLogEntity.HotelID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.BookRemark))
            {
                lmsparm[1].Value = DBNull.Value;
            }
            else
            {
                lmsparm[1].Value = lmSystemLogEntity.BookRemark;
            }
            lmsparm[2].Value = lmSystemLogEntity.LogMessages.Userid;
            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_hotel_ex_remark_save", lmsparm);
        }

        public static int SaveOrderOperation(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] lmparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            DataSet dsOrderInfo = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select", true, lmparm);

            if (dsOrderInfo.Tables.Count == 0 || dsOrderInfo.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            string strPriceCode = dsOrderInfo.Tables[0].Rows[0]["price_code"].ToString().Trim();
            string strBookStatus = dsOrderInfo.Tables[0].Rows[0]["BOOK_STATUS"].ToString().Trim();
            string strBookStatusOther = dsOrderInfo.Tables[0].Rows[0]["book_status_other"].ToString().Trim();
            string strFogresvtype = dsOrderInfo.Tables[0].Rows[0]["fog_resvtype"].ToString().Trim();
            string strFogresvstatus = dsOrderInfo.Tables[0].Rows[0]["fog_resvstatus"].ToString().Trim();
            string strFogauditstatus = dsOrderInfo.Tables[0].Rows[0]["fog_auditstatus"].ToString().Trim();
            string strOrdercanclereason = dsOrderInfo.Tables[0].Rows[0]["ORDER_CANCLE_REASON"].ToString().Trim();
            string strBookremark = dsOrderInfo.Tables[0].Rows[0]["BOOK_REMARK"].ToString().Trim();
            string strPaystatus = dsOrderInfo.Tables[0].Rows[0]["pay_status"].ToString().Trim();
            string strConfirmnum = dsOrderInfo.Tables[0].Rows[0]["CONFIRM_NUM"].ToString().Trim();

            strBookremark = String.IsNullOrEmpty(lmSystemLogEntity.BookRemark) ? strBookremark : lmSystemLogEntity.BookRemark;
            if ("LMBAR".Equals(strPriceCode.Trim().ToUpper()))
            {
                strBookStatus = "4";
                strFogresvtype = "c";
                strOrdercanclereason = "";
            }
            else
            {
                switch (lmSystemLogEntity.OrderBookStatus)
                {
                    case "4":
                        strBookStatusOther = "4";
                        strFogresvtype = "n";
                        strFogresvstatus = "1";
                        strOrdercanclereason = "";
                        break;
                    case "5":
                        strBookStatusOther = "5";
                        strFogauditstatus = "5";
                        strOrdercanclereason = "";
                        break;
                    case "7":
                        strBookStatusOther = "7";
                        strFogauditstatus = "7";
                        strOrdercanclereason = "";
                        break;
                    case "8":
                        strBookStatusOther = "8";
                        strFogauditstatus = "8";
                        strOrdercanclereason = "";
                        break;
                    case "9":
                        strBookStatusOther = "9";
                        strFogresvtype = "c";
                        strOrdercanclereason = lmSystemLogEntity.CanelReson;
                        break;
                    default:
                        break;
                }
            }

            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("FOGRESVTYPE",OracleType.VarChar),
                                    new OracleParameter("FOGRESVSTATUS",OracleType.VarChar),
                                    new OracleParameter("FOGAUDITSTATUS",OracleType.VarChar),
                                    new OracleParameter("ORDERCANCLEREASON",OracleType.VarChar),
                                    new OracleParameter("BOOKREMARK",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("CONFIRMNUM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            parm[1].Value = strBookStatus;
            parm[2].Value = strBookStatusOther;
            parm[3].Value = strFogresvtype;
            parm[4].Value = strFogresvstatus;
            parm[5].Value = strFogauditstatus;
            parm[6].Value = strOrdercanclereason;
            parm[7].Value = strBookremark;
            parm[8].Value = lmSystemLogEntity.LogMessages.Username;
            parm[9].Value = strPaystatus;
            parm[10].Value = strConfirmnum;

            HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_operation_save", parm);

            OracleParameter[] lmsparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("FOLLOWUP",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                lmsparm[0].Value = DBNull.Value;
            }
            else
            {
                lmsparm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            lmsparm[1].Value = lmSystemLogEntity.FollowUp;
            lmsparm[2].Value = lmSystemLogEntity.LogMessages.Username;

            HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_cof_save", lmsparm);
            return 1;
        }

        public static int SaveOrderOperationManager(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] lmparm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                lmparm[0].Value = DBNull.Value;
            }
            else
            {
                lmparm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            DataSet dsOrderInfo = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_order_operation_main_select", true, lmparm);

            if (dsOrderInfo.Tables.Count == 0 || dsOrderInfo.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            string strPriceCode = dsOrderInfo.Tables[0].Rows[0]["price_code"].ToString().Trim();
            string strBookStatus = dsOrderInfo.Tables[0].Rows[0]["BOOK_STATUS"].ToString().Trim();
            string strBookStatusOther = dsOrderInfo.Tables[0].Rows[0]["book_status_other"].ToString().Trim();
            string strFogresvtype = dsOrderInfo.Tables[0].Rows[0]["fog_resvtype"].ToString().Trim();
            string strFogresvstatus = dsOrderInfo.Tables[0].Rows[0]["fog_resvstatus"].ToString().Trim();
            string strFogauditstatus = dsOrderInfo.Tables[0].Rows[0]["fog_auditstatus"].ToString().Trim();
            string strOrdercanclereason = dsOrderInfo.Tables[0].Rows[0]["ORDER_CANCLE_REASON"].ToString().Trim();
            string strBookremark = dsOrderInfo.Tables[0].Rows[0]["BOOK_REMARK"].ToString().Trim();
            string strPaystatus = dsOrderInfo.Tables[0].Rows[0]["pay_status"].ToString().Trim();
            string strConfirmnum = dsOrderInfo.Tables[0].Rows[0]["CONFIRM_NUM"].ToString().Trim();

            strBookremark = String.IsNullOrEmpty(lmSystemLogEntity.BookRemark) ? strBookremark : lmSystemLogEntity.BookRemark;
            switch (lmSystemLogEntity.OrderBookStatus)
            {
                case "4":
                    strBookStatusOther = "4";
                    strFogresvtype = "n";
                    strFogresvstatus = "1";
                    strOrdercanclereason = "";
                    break;
                case "5":
                    strBookStatusOther = "5";
                    strFogauditstatus = "5";
                    strOrdercanclereason = "";
                    break;
                case "7":
                    strBookStatusOther = "7";
                    strFogauditstatus = "7";
                    strOrdercanclereason = "";
                    break;
                case "8":
                    strBookStatusOther = "8";
                    strFogauditstatus = "8";
                    strOrdercanclereason = "";
                    break;
                case "9":
                    strBookStatusOther = "9";
                    strFogresvtype = "c";
                    strOrdercanclereason = lmSystemLogEntity.CanelReson;
                    break;
                default:
                    break;
            }

            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("FOGRESVTYPE",OracleType.VarChar),
                                    new OracleParameter("FOGRESVSTATUS",OracleType.VarChar),
                                    new OracleParameter("FOGAUDITSTATUS",OracleType.VarChar),
                                    new OracleParameter("ORDERCANCLEREASON",OracleType.VarChar),
                                    new OracleParameter("BOOKREMARK",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("CONFIRMNUM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            parm[1].Value = strBookStatus;
            parm[2].Value = strBookStatusOther;
            parm[3].Value = strFogresvtype;
            parm[4].Value = strFogresvstatus;
            parm[5].Value = strFogauditstatus;
            parm[6].Value = strOrdercanclereason;
            parm[7].Value = strBookremark;
            parm[8].Value = lmSystemLogEntity.LogMessages.Username;
            parm[9].Value = strPaystatus;
            parm[10].Value = strConfirmnum;
            HotelVp.Common.DBUtility.DbManager.ExecuteSql("OrderInfo", "t_lm_order_operation_save", parm);
            return 1;
        }

        public static LmSystemLogEntity OrderOperationDetailSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            string sqlString = "t_lm_order_operation_detial_select";

            if (lmSystemLogEntity.PriceCode == "LMBAR" || lmSystemLogEntity.PriceCode == "LMBAR2")
            {
                sqlString = "t_lm_order_operation_detial_select_lm";
            }


            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", sqlString, true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity HotelInfoSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.HotelID;
            }

            DataSet dsHotel = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_hotel_info_sales_select", true, parm);

            if (dsHotel.Tables.Count > 0 && dsHotel.Tables[0].Rows.Count > 0)
            {
                DataSet dsSales = GetSalesLMHotelInfo(lmSystemLogEntity.HotelID);
                if (dsSales.Tables.Count > 0 && dsSales.Tables[0].Rows.Count > 0)
                {
                    dsHotel.Tables[0].Rows[0]["SALESPER"] = dsSales.Tables[0].Rows[0]["User_DspName"].ToString();
                    dsHotel.Tables[0].Rows[0]["SALESTEL"] = dsSales.Tables[0].Rows[0]["User_Tel"].ToString();
                }
            }

            lmSystemLogEntity.QueryResult = dsHotel;
            return lmSystemLogEntity;
        }

        public static DataSet GetSalesLMHotelInfo(string HotelID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerHistory");
            cmd.SetParameterValue("@HotelID", HotelID);
            return cmd.ExecuteDataSet();
        }

        public static LmSystemLogEntity GetEventLMOrderID(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.EventID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_usergroup_fogid_select", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity PlatFormSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_b_platform", true);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity OrderChannelSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_b_channel", true);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity UserDetailListSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectCmsOrderDetailEventHistory");
            cmd.SetParameterValue("@EVENT_LM_ID", lmSystemLogEntity.EventID);
            lmSystemLogEntity.QueryResult = cmd.ExecuteDataSet();
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity SalesManagerSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectCmsSalesManagerList");
            lmSystemLogEntity.QueryResult = cmd.ExecuteDataSet();
            return lmSystemLogEntity;
        }

        public static string SalesManagerHotelListSelect(string UserIDList)
        {
            string[] UserList = UserIDList.Split(',');
            string strResult = "";
            foreach (string UserID in UserList)
            {
                if (String.IsNullOrEmpty(UserID))
                {
                    continue;
                }
                DataCommand cmd = DataCommandManager.GetDataCommand("SelectCmsSalesManagerHotelList");
                cmd.SetParameterValue("@UserID", UserID);
                DataSet dsResult = cmd.ExecuteDataSet();
                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drTemp in dsResult.Tables[0].Rows)
                    {
                        strResult = String.IsNullOrEmpty(drTemp["UserAccount"].ToString().Trim()) ? strResult : strResult + drTemp["UserAccount"].ToString().Trim() + ",";
                    }
                }
            }
            strResult = (strResult.Length == 0) ? "0," : strResult;
            return strResult;
        }

        #region   Add  区分现付预付总计数据
        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogSelectCount(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_XPayment_ReviewLmOrderLogSelectCount";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }

                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[7].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[7].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[7].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.GroupID;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogSelectCountForSales(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_XPayment_ReviewLmOrderLogSelectCountForSales";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }


                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogExport(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_XPayment_ReviewLmOrderLogSelect";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }

                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[7].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[7].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[7].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }
                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_XPayment_ReviewLmOrderLogSelect";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }

                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[7].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[7].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[7].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.GroupID;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                //DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSQLString);
                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity order_XPayment_ReviewLmOrderLogSelectForSales(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_XPayment_ReviewLmOrderLogSelectForSales";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }


                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        #endregion

        #region    BAR/BARB当晚入住
        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogSelectCount(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_InTheNight_ReviewLmOrderLogSelectCount";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.GroupID;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogSelectCountForSales(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_InTheNight_ReviewLmOrderLogSelectCountForSales";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }


                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogExport(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_InTheNight_ReviewLmOrderLogSelect";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }


                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_InTheNight_ReviewLmOrderLogSelect";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.GroupID;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                //DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSQLString);
                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity order_InTheNight_ReviewLmOrderLogSelectForSales(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "order_InTheNight_ReviewLmOrderLogSelectForSales";
                OracleParameter[] parm ={
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.EndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }


                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.OutTest;
                }


                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }
        #endregion

        public static LmSystemLogEntity LmOrderAutoRefreshSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("STARTDATE",OracleType.VarChar)
                                };

            parm[0].Value = DateTime.Now.AddDays(-3).ToShortDateString() + " 04:00:00";
            DataSet dsResult = DbManager.Query("LmSystemLog", "LmOrderAutoRefreshSelect", true, parm);
            lmSystemLogEntity.QueryResult = dsResult;
            return lmSystemLogEntity;
        }

        /// <summary>
        /// 订单SurveyDetail
        /// </summary>
        /// <param name="lmSystemLogEntity"></param>
        /// <returns></returns>
        public static LmSystemLogEntity OrderSurveyDetail(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_survey_detail_quick", true, parm);
            return lmSystemLogEntity;
        }

        /// <summary>
        /// 订单SurveyAnalysis
        /// </summary>
        /// <param name="lmSystemLogEntity"></param>
        /// <returns></returns>
        public static LmSystemLogEntity OrderSurveyAnalysis(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CreateDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar),
                                    new OracleParameter("APPPLATFORM",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.StartDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.EndDTime;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.PlatForm;
            }
            if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = lmSystemLogEntity.BookStatus;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_survey_survey", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity GetOrderInfoData(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            DataSet dsHotel = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_order_info_data_select", true, parm);

            if (dsHotel.Tables.Count > 0 && dsHotel.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsHotel.Tables[0].Rows[0]["SALESMG"].ToString()))
            {
                DataCommand cmd = DataCommandManager.GetDataCommand("GetMailToList");
                cmd.SetParameterValue("@UserID", dsHotel.Tables[0].Rows[0]["SALESMG"].ToString());
                DataSet dsSales = cmd.ExecuteDataSet();

                if (dsSales.Tables.Count > 0 && dsSales.Tables[0].Rows.Count > 0)
                {
                    dsHotel.Tables[0].Rows[0]["TEL"] = dsSales.Tables[0].Rows[0]["User_Tel"].ToString();
                    dsHotel.Tables[0].Rows[0]["EMAIL"] = dsSales.Tables[0].Rows[0]["User_Email"].ToString();
                    dsHotel.Tables[0].Rows[0]["SALESNM"] = dsSales.Tables[0].Rows[0]["User_DspName"].ToString();
                }
            }

            lmSystemLogEntity.QueryResult = dsHotel;
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity GetOperateHis(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("FOGORDERNUM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.EventID;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_order_info_data_operatehis", true, parm);
            return lmSystemLogEntity;
        }
        
        public static LmSystemLogEntity OrderFaxHis(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("FOGORDERNUM",OracleType.VarChar),
                                    new OracleParameter("FAXTYPE",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FogOrderID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.SendFaxType))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.SendFaxType;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_order_info_data_faxhis", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity HotelFaxHis(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("OUTSDATE",OracleType.VarChar),
                                    new OracleParameter("OUTEDATE",OracleType.VarChar),
                                    new OracleParameter("FAXSTATUS",OracleType.VarChar),
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("FAXNUM",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.HotelID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.OutStart;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.OutEnd;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.FaxStatus))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = lmSystemLogEntity.FaxStatus;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = lmSystemLogEntity.FogOrderID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.FaxNum))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = lmSystemLogEntity.FaxNum;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_order_info_hotel_faxhis", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity HotelComparisonSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetHotelComparisonTodaySelect");
            cmd.SetParameterValue("@HotelID", lmSystemLogEntity.HotelID);
            cmd.SetParameterValue("@CityID", lmSystemLogEntity.CityID);
            cmd.SetParameterValue("@Sales", lmSystemLogEntity.Sales);

            cmd.SetParameterValue("@DSourceType", lmSystemLogEntity.DSourceType);
            cmd.SetParameterValue("@DSourceData", lmSystemLogEntity.DSourceData);
            cmd.SetParameterValue("@Discount", lmSystemLogEntity.Discount);

            cmd.SetParameterValue("@PageCurrent", lmSystemLogEntity.PageCurrent - 1);
            cmd.SetParameterValue("@PageSize", lmSystemLogEntity.PageSize);

            DataSet dsResult = new DataSet();
            DataTable dtDetail = new DataTable();
            DataTable dtMaster = new DataTable();

            dtDetail = cmd.ExecuteDataSet().Tables[0].Copy();
            dtDetail.TableName = "Detail";

            DataCommand mastCmd = DataCommandManager.GetDataCommand("GetHotelComparisonTodayMasterSelect");
            mastCmd.SetParameterValue("@HotelID", lmSystemLogEntity.HotelID);
            mastCmd.SetParameterValue("@CityID", lmSystemLogEntity.CityID);
            mastCmd.SetParameterValue("@Sales", lmSystemLogEntity.Sales);
            mastCmd.SetParameterValue("@DSourceType", lmSystemLogEntity.DSourceType);
            mastCmd.SetParameterValue("@DSourceData", lmSystemLogEntity.DSourceData);
            mastCmd.SetParameterValue("@Discount", lmSystemLogEntity.Discount);

            dtMaster = mastCmd.ExecuteDataSet().Tables[0].Copy();
            dtMaster.TableName = "Master";


            dsResult.Tables.Add(dtMaster);
            dsResult.Tables.Add(dtDetail);

            lmSystemLogEntity.QueryResult = dsResult;
            lmSystemLogEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");

            return lmSystemLogEntity;
        }

        //public static LmSystemLogEntity HotelComparisonSelectCount(LmSystemLogEntity lmSystemLogEntity)
        //{
        //    OracleParameter[] parm ={
        //                            new OracleParameter("FOGORDERNUM",OracleType.VarChar)
        //                        };
        //    if (String.IsNullOrEmpty(lmSystemLogEntity.EventID))
        //    {
        //        parm[0].Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        parm[0].Value = lmSystemLogEntity.EventID;
        //    }

        //    lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_lm_hotel_comparison_select_count", true, parm);
        //    return lmSystemLogEntity;
        //}

        public static LmSystemLogEntity ExportHotelComparisonSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetHotelComparisonTodayExport");
            cmd.SetParameterValue("@HotelID", lmSystemLogEntity.HotelID);
            cmd.SetParameterValue("@CityID", lmSystemLogEntity.CityID);
            cmd.SetParameterValue("@Sales", lmSystemLogEntity.Sales);
            cmd.SetParameterValue("@DSourceType", lmSystemLogEntity.DSourceType);
            cmd.SetParameterValue("@DSourceData", lmSystemLogEntity.DSourceData);
            cmd.SetParameterValue("@Discount", lmSystemLogEntity.Discount);

            DataSet dsResult = new DataSet();
            DataTable dtDetail = new DataTable();
            DataTable dtMaster = new DataTable();

            dtDetail = cmd.ExecuteDataSet().Tables[0].Copy();
            dtDetail.TableName = "Detail";

            DataCommand mastCmd = DataCommandManager.GetDataCommand("GetHotelComparisonTodayMasterSelect");
            mastCmd.SetParameterValue("@HotelID", lmSystemLogEntity.HotelID);
            mastCmd.SetParameterValue("@CityID", lmSystemLogEntity.CityID);
            mastCmd.SetParameterValue("@Sales", lmSystemLogEntity.Sales);
            mastCmd.SetParameterValue("@DSourceType", lmSystemLogEntity.DSourceType);
            mastCmd.SetParameterValue("@DSourceData", lmSystemLogEntity.DSourceData);
            mastCmd.SetParameterValue("@Discount", lmSystemLogEntity.Discount);
            dtMaster = mastCmd.ExecuteDataSet().Tables[0].Copy();
            dtMaster.TableName = "Master";

            dsResult.Tables.Add(dtMaster);
            dsResult.Tables.Add(dtDetail);
            lmSystemLogEntity.QueryResult = dsResult;

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReciveFaxSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.StartDTime;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.EndDTime;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_fax_unknow_select", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReciveBindFaxSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("LinkType",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(lmSystemLogEntity.SendFaxType))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.SendFaxType;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.FogOrderID;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.StartDTime;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = lmSystemLogEntity.EndDTime;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_fax_unknow_bind_select", true, parm);
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReciveBindFaxVerifySelect(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("LinkType",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(lmSystemLogEntity.SendFaxType))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.SendFaxType;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = lmSystemLogEntity.StartDTime;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.EndDTime;
            }

            lmSystemLogEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_fax_verify_bind_select", true, parm);
            return lmSystemLogEntity;
        }


        public static int SaveReciveFax(LmSystemLogEntity lmSystemLogEntity)
        {
            DataSet dsFaxStaus = CheckFaxStatus(lmSystemLogEntity);
            if (dsFaxStaus.Tables.Count == 0 || dsFaxStaus.Tables[0].Rows.Count == 0)
            {
                return 2;
            }

            DataSet dsBarCode = CheckBarCode(lmSystemLogEntity);
            if (dsBarCode.Tables.Count == 0 || dsBarCode.Tables[0].Rows.Count == 0)
            {
                return 3;
            }

            OracleParameter[] lmparm ={
                                    new OracleParameter("BARCODE",OracleType.VarChar),
                                    new OracleParameter("LINKID",OracleType.VarChar),
                                    new OracleParameter("LINKTYPE",OracleType.VarChar),
                                    new OracleParameter("FAXURL",OracleType.VarChar),
                                    new OracleParameter("BACKFAXURL",OracleType.VarChar),
                                    new OracleParameter("FAXID",OracleType.VarChar),
                                    new OracleParameter("FAXNUM",OracleType.VarChar),
                                    new OracleParameter("UNKOWNID",OracleType.VarChar),
                                    new OracleParameter("OPERATOR",OracleType.VarChar)
                                };
            lmparm[0].Value = dsBarCode.Tables[0].Rows[0]["BARCODE"].ToString();
            lmparm[1].Value = dsBarCode.Tables[0].Rows[0]["LINK_ID"].ToString();
            lmparm[2].Value = dsBarCode.Tables[0].Rows[0]["LINK_TYPE"].ToString();
            lmparm[3].Value = dsFaxStaus.Tables[0].Rows[0]["FAX_URL"].ToString();
            lmparm[4].Value = dsFaxStaus.Tables[0].Rows[0]["FAX_URL"].ToString();
            lmparm[5].Value = dsFaxStaus.Tables[0].Rows[0]["FAX_ID"].ToString();
            lmparm[6].Value = dsFaxStaus.Tables[0].Rows[0]["FAX_NUM"].ToString();
            lmparm[7].Value = lmSystemLogEntity.FaxID;
            lmparm[8].Value = lmSystemLogEntity.LogMessages.Userid;
            HotelVp.Common.DBUtility.DbManager.ExecuteSql("LmSystemLog", "t_fax_data_save", lmparm);

            OracleParameter[] parm ={
                                    new OracleParameter("FAXID",OracleType.VarChar),
                                    new OracleParameter("OPERATOR",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FaxID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FaxID;
            }
            parm[1].Value = lmSystemLogEntity.LogMessages.Userid;
            HotelVp.Common.DBUtility.DbManager.ExecuteSql("LmSystemLog", "t_fax_unknow_status_save", parm);
            return 1;
        }

        public static DataSet CheckFaxStatus(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("FAXID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FaxID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FaxID;
            }

            return HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_fax_unknow_status_check", true, parm);
        }

        public static DataSet CheckBarCode(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("BARCODE",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.BarCode))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.BarCode;
            }
            return HotelVp.Common.DBUtility.DbManager.Query("LmSystemLog", "t_fax_barcode_check", true, parm);
        }


        public static LmSystemLogEntity ReviewLmOrderLogSelectByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "ReviewLmOrderLogSelectByRests";
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("ORDERSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("DashPopStatus",OracleType.VarChar),
                                    new OracleParameter("DASHSTARTDTIME",OracleType.VarChar),
                                    new OracleParameter("DASHENDDTIME",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("ORDERTYPESTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("ISRESERVE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAMES",OracleType.VarChar),
                                    new OracleParameter("OUTCC",OracleType.VarChar),
                                    new OracleParameter("OUTUC",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("OUTFAILORDER",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.EndDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.HotelID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.CityID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.PayStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.Aprove;
                }

                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[9].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[9].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[9].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Ticket;
                }

                //LOGINMOBILE
                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.Mobile;
                }

                //InStart
                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.InStart;
                }

                //InEnd
                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    strSQLString = "ReviewLmOrderLogSelectForSales";
                    //CommonDA.InsertEventHistory("取得销售人员开始");
                    parm[15].Value = SalesManagerHotelListSelect(lmSystemLogEntity.Sales);
                    //CommonDA.InsertEventHistory("取得销售人员结束");
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashPopStatus))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.DashPopStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.DashStartDTime))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.DashStartDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashEndDTime))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.DashEndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderTypeStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.OrderTypeStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.OrderBookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.OrderBookStatusOther;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.RestPriceCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.IsReserve))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.IsReserve;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[29].Value = DBNull.Value;
                }
                else
                {
                    parm[29].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutCC))
                {
                    parm[30].Value = DBNull.Value;
                }
                else
                {
                    parm[30].Value = lmSystemLogEntity.OutCC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutUC))
                {
                    parm[31].Value = DBNull.Value;
                }
                else
                {
                    parm[31].Value = lmSystemLogEntity.OutUC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[32].Value = DBNull.Value;
                }
                else
                {
                    parm[32].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutFailOrder))
                {
                    parm[33].Value = DBNull.Value;
                }
                else
                {
                    parm[33].Value = lmSystemLogEntity.OutFailOrder;
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSQLString);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始");
                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
                //CommonDA.InsertEventHistory("取得订单历史开始");
                lmSystemLogEntity.QueryResult = dsResult;
            }
            else
            {

                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.PackageName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.AmountFrom;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.AmountTo;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.PickfromDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.PicktoDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.TicketType;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.TicketTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.TicketPayCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.GroupID;
                }

                string strSqlName = "";
                if ("1".Equals(lmSystemLogEntity.TicketData))
                {
                    strSqlName = "ReviewLmOrderLogTicketOrdData";
                }
                else
                {
                    strSqlName = "ReviewLmOrderLogTicketAllData";
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSqlName);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }

                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelectCountByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "ReviewLmOrderLogSelectCountByRests";
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("ORDERSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("DashPopStatus",OracleType.VarChar),
                                    new OracleParameter("DASHSTARTDTIME",OracleType.VarChar),
                                    new OracleParameter("DASHENDDTIME",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("ORDERTYPESTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("ISRESERVE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAMES",OracleType.VarChar),
                                    new OracleParameter("OUTCC",OracleType.VarChar),
                                    new OracleParameter("OUTUC",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("OUTFAILORDER",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.EndDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.HotelID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.CityID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.PayStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.Aprove;
                }
                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[9].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[9].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[9].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Ticket;
                }

                //LOGINMOBILE
                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.Mobile;
                }

                //InStart
                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.InStart;
                }

                //InEnd
                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    //CommonDA.InsertEventHistory("取得销售人员开始-count");
                    parm[15].Value = SalesManagerHotelListSelect(lmSystemLogEntity.Sales);
                    //CommonDA.InsertEventHistory("取得销售人员结束-count");
                    strSQLString = "ReviewLmOrderLogSelectCountForSales";
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashPopStatus))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.DashPopStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashStartDTime))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.DashStartDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashEndDTime))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.DashEndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderTypeStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.OrderTypeStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.OrderBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.OrderBookStatusOther;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.RestPriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.IsReserve))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.IsReserve;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[29].Value = DBNull.Value;
                }
                else
                {
                    parm[29].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutCC))
                {
                    parm[30].Value = DBNull.Value;
                }
                else
                {
                    parm[30].Value = lmSystemLogEntity.OutCC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutUC))
                {
                    parm[31].Value = DBNull.Value;
                }
                else
                {
                    parm[31].Value = lmSystemLogEntity.OutUC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[32].Value = DBNull.Value;
                }
                else
                {
                    parm[32].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutFailOrder))
                {
                    parm[33].Value = DBNull.Value;
                }
                else
                {
                    parm[33].Value = lmSystemLogEntity.OutFailOrder;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始-count");
                DataSet dsResult = DbManager.Query("LmSystemLog", strSQLString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史结束-count");

                lmSystemLogEntity.QueryResult = dsResult;
            }
            else
            {
                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.PackageName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.AmountFrom;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.AmountTo;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.PickfromDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.PicktoDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.TicketType;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.TicketTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.TicketPayCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.GroupID;
                }
                string strSql = "";
                if ("1".Equals(lmSystemLogEntity.TicketData))
                {
                    strSql = "ReviewLmOrderLogTicketOrdCount";
                }
                else
                {
                    strSql = "ReviewLmOrderLogTicketAllCount";
                }

                DataSet dsResult = DbManager.Query("LmSystemLog", strSql, true, parm);
                lmSystemLogEntity.QueryResult = dsResult;
            }

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ExportLmOrderSelectByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                string strSQLString = "ReviewLmOrderLogSelectByRests";
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar),
                                    new OracleParameter("ORDERSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("DashPopStatus",OracleType.VarChar),
                                    new OracleParameter("DASHSTARTDTIME",OracleType.VarChar),
                                    new OracleParameter("DASHENDDTIME",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("ORDERTYPESTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("BOOKSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("ISRESERVE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAMES",OracleType.VarChar),
                                    new OracleParameter("OUTCC",OracleType.VarChar),
                                    new OracleParameter("OUTUC",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.FogOrderID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.FogOrderID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.StartDTime))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.StartDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.EndDTime))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.EndDTime;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.HotelID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.CityID;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.PayStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.Aprove;
                }
                if ("1".Equals(lmSystemLogEntity.OutTest) && ("4,5,6,7,8,".Equals(lmSystemLogEntity.OrderBookStatusOther)))
                {
                    parm[9].Value = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                    {
                        parm[9].Value = DBNull.Value;
                    }
                    else
                    {
                        parm[9].Value = lmSystemLogEntity.HotelComfirm;
                    }
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Ticket;
                }

                //LOGINMOBILE
                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.Mobile;
                }

                //InStart
                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.InStart;
                }

                //InEnd
                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    strSQLString = "ReviewLmOrderLogSelectForSales";
                    //CommonDA.InsertEventHistory("取得销售人员开始");
                    parm[15].Value = SalesManagerHotelListSelect(lmSystemLogEntity.Sales);
                    //CommonDA.InsertEventHistory("取得销售人员结束");
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashPopStatus))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.DashPopStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashStartDTime))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.DashStartDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.DashEndDTime))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.DashEndDTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderTypeStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.OrderTypeStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatus))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.OrderBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderBookStatusOther))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.OrderBookStatusOther;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.RestPriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.IsReserve))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.IsReserve;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[29].Value = DBNull.Value;
                }
                else
                {
                    parm[29].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutCC))
                {
                    parm[30].Value = DBNull.Value;
                }
                else
                {
                    parm[30].Value = lmSystemLogEntity.OutCC;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutUC))
                {
                    parm[31].Value = DBNull.Value;
                }
                else
                {
                    parm[31].Value = lmSystemLogEntity.OutUC;
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSQLString);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }
                //CommonDA.InsertEventHistory("取得订单历史开始");
                DataSet dsResult = DbHelperOra.Query(SqlString, true, parm);
                //CommonDA.InsertEventHistory("取得订单历史开始");
                lmSystemLogEntity.QueryResult = dsResult;
            }
            else
            {

                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar),
                                    new OracleParameter("PAYCODE",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(lmSystemLogEntity.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = lmSystemLogEntity.PackageName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = lmSystemLogEntity.AmountFrom;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = lmSystemLogEntity.AmountTo;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.PickfromDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.PicktoDate;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.TicketType;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.TicketTime;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.TicketPayCode))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.TicketPayCode;
                }

                string strSqlName = "";
                if ("1".Equals(lmSystemLogEntity.TicketData))
                {
                    strSqlName = "ReviewLmOrderLogTicketOrdData";
                }
                else
                {
                    strSqlName = "ReviewLmOrderLogTicketAllData";
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("LmSystemLog", strSqlName);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }

                DataSet dsResult = DbHelperOra.Query(SqlString, true, parm);
                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static int DeleteFax(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("FAXID",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(lmSystemLogEntity.FaxID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = lmSystemLogEntity.FaxID;
            }

            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("LmSystemLog", "t_fax_unknow_delete", parm);
        }
    }
}
