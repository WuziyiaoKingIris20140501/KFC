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

namespace HotelVp.CMS.Domain.DataAccess.GeneralSetting
{
    public abstract class OrderSearchDA
    {
        public static LmSystemLogEntity ReviewOrderListSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("AREAID",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAME",OracleType.VarChar),
                                    new OracleParameter("CREATESTATUS",OracleType.VarChar),
                                    new OracleParameter("USERCANCEL",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYMETHOD",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar)
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

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.InStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.CityID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AreaID))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.AreaID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[9].Value = DBNull.Value;
                }
                else
                {
                    parm[9].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Sales;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.HotelID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.Mobile;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.CreateStatus))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.CreateStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.UserCancel))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    parm[15].Value = lmSystemLogEntity.UserCancel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PriceCode))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.PriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AffirmStatus))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.AffirmStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.Aprove;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.HotelComfirm;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.RestPriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.PayStatus;
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", "t_lm_order_search_list");
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY TLO." + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }
                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
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

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", strSqlName);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }

                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
                lmSystemLogEntity.QueryResult = dsResult;
            }

            return lmSystemLogEntity;
        }
        public static LmSystemLogEntity ReviewOrderListSelectBak(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("AREAID",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAME",OracleType.VarChar),
                                    new OracleParameter("CREATESTATUS",OracleType.VarChar),
                                    new OracleParameter("USERCANCEL",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUS",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar)
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

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.InStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.CityID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AreaID))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.AreaID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[9].Value = DBNull.Value;
                }
                else
                {
                    parm[9].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Sales;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.HotelID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.Mobile;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.CreateStatus))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.CreateStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.UserCancel))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    parm[15].Value = lmSystemLogEntity.UserCancel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PriceCode))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.PriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AffirmStatus))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.AffirmStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.Aprove;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.HotelComfirm;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.RestPriceCode;
                } 
                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", "t_lm_order_search_list");
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY TLO." + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }
                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
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

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", strSqlName);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }

                DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
                lmSystemLogEntity.QueryResult = dsResult;
            }

            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReviewOrderListSelectCount(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("AREAID",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAME",OracleType.VarChar),
                                    new OracleParameter("CREATESTATUS",OracleType.VarChar),
                                    new OracleParameter("USERCANCEL",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYMETHOD",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar)
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

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.InStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.CityID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AreaID))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.AreaID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[9].Value = DBNull.Value;
                }
                else
                {
                    parm[9].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Sales;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.HotelID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.Mobile;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.CreateStatus))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.CreateStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.UserCancel))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    parm[15].Value = lmSystemLogEntity.UserCancel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PriceCode))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.PriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AffirmStatus))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.AffirmStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.Aprove;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.HotelComfirm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.RestPriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.PayStatus;
                }
                //string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", "t_lm_order_search_list_count");

                //DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
                DataSet dsResult = DbManager.Query("OrderSearch", "t_lm_order_search_list_count", true, parm);
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

                DataSet dsResult = DbManager.Query("OrderSearch", strSql, true, parm);
                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ExportOrderListSelect(LmSystemLogEntity lmSystemLogEntity)
        {
            if (String.IsNullOrEmpty(lmSystemLogEntity.TicketType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("ORDERID",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("INSTART",OracleType.VarChar),
                                    new OracleParameter("INEND",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("AREAID",OracleType.VarChar),
                                    new OracleParameter("OUTSTART",OracleType.VarChar),
                                    new OracleParameter("OUTEND",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LOGINMOBILE",OracleType.VarChar),
                                    new OracleParameter("GUESTNAME",OracleType.VarChar),
                                    new OracleParameter("CREATESTATUS",OracleType.VarChar),
                                    new OracleParameter("USERCANCEL",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUSOTHER",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("PLATFORM",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("APROVE",OracleType.VarChar),
                                    new OracleParameter("OUTTEST",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUS",OracleType.VarChar),
                                    new OracleParameter("PAYMETHOD",OracleType.VarChar),
                                    new OracleParameter("PAYSTATUS",OracleType.VarChar)
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

                if (String.IsNullOrEmpty(lmSystemLogEntity.GroupID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = lmSystemLogEntity.GroupID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.InStart))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = lmSystemLogEntity.InStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.InEnd))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = lmSystemLogEntity.InEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.CityID))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = lmSystemLogEntity.CityID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AreaID))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = lmSystemLogEntity.AreaID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutStart))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = lmSystemLogEntity.OutStart;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutEnd))
                {
                    parm[9].Value = DBNull.Value;
                }
                else
                {
                    parm[9].Value = lmSystemLogEntity.OutEnd;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Sales))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = lmSystemLogEntity.Sales;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelID))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = lmSystemLogEntity.HotelID;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Mobile))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = lmSystemLogEntity.Mobile;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.GuestName))
                {
                    parm[13].Value = DBNull.Value;
                }
                else
                {
                    parm[13].Value = lmSystemLogEntity.GuestName;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.CreateStatus))
                {
                    parm[14].Value = DBNull.Value;
                }
                else
                {
                    parm[14].Value = lmSystemLogEntity.CreateStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.UserCancel))
                {
                    parm[15].Value = DBNull.Value;
                }
                else
                {
                    parm[15].Value = lmSystemLogEntity.UserCancel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PriceCode))
                {
                    parm[16].Value = DBNull.Value;
                }
                else
                {
                    parm[16].Value = lmSystemLogEntity.PriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.AffirmStatus))
                {
                    parm[17].Value = DBNull.Value;
                }
                else
                {
                    parm[17].Value = lmSystemLogEntity.AffirmStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
                {
                    parm[18].Value = DBNull.Value;
                }
                else
                {
                    parm[18].Value = lmSystemLogEntity.Ticket;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.PlatForm))
                {
                    parm[19].Value = DBNull.Value;
                }
                else
                {
                    parm[19].Value = lmSystemLogEntity.PlatForm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
                {
                    parm[20].Value = DBNull.Value;
                }
                else
                {
                    parm[20].Value = lmSystemLogEntity.OrderChannel;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.Aprove))
                {
                    parm[21].Value = DBNull.Value;
                }
                else
                {
                    parm[21].Value = lmSystemLogEntity.Aprove;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.OutTest))
                {
                    parm[22].Value = DBNull.Value;
                }
                else
                {
                    parm[22].Value = lmSystemLogEntity.OutTest;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
                {
                    parm[23].Value = DBNull.Value;
                }
                else
                {
                    parm[23].Value = lmSystemLogEntity.RestBookStatus;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
                {
                    parm[24].Value = DBNull.Value;
                }
                else
                {
                    parm[24].Value = lmSystemLogEntity.HotelComfirm;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
                {
                    parm[25].Value = DBNull.Value;
                }
                else
                {
                    parm[25].Value = lmSystemLogEntity.RestPriceCode;
                }

                if (String.IsNullOrEmpty(lmSystemLogEntity.BookStatus))
                {
                    parm[26].Value = DBNull.Value;
                }
                else
                {
                    parm[26].Value = lmSystemLogEntity.BookStatus;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayCode))
                {
                    parm[27].Value = DBNull.Value;
                }
                else
                {
                    parm[27].Value = lmSystemLogEntity.PayCode;
                }
                if (String.IsNullOrEmpty(lmSystemLogEntity.PayStatus))
                {
                    parm[28].Value = DBNull.Value;
                }
                else
                {
                    parm[28].Value = lmSystemLogEntity.PayStatus;
                }

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", "t_lm_order_search_list");
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY TLO." + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }
                DataSet dsResult = DbHelperOra.Query(SqlString, true, parm);

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

                string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", strSqlName);
                if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
                {
                    SqlString = SqlString + " ORDER BY " + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
                }

                DataSet dsResult = DbHelperOra.Query(SqlString, true, parm);
                lmSystemLogEntity.QueryResult = dsResult;
            }
            return lmSystemLogEntity;
        }

        public static LmSystemLogEntity ReviewLmOrderLogSelectByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUS",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar)
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

            if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.OrderChannel;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.PriceCode))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = lmSystemLogEntity.PriceCode;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.AffirmStatus))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = lmSystemLogEntity.AffirmStatus;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = lmSystemLogEntity.RestBookStatus;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = lmSystemLogEntity.RestPriceCode;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = lmSystemLogEntity.Ticket;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = lmSystemLogEntity.HotelComfirm;
            }

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("OrderSearch", "ReviewLmOrderLogSelectByRests");
            if (!String.IsNullOrEmpty(lmSystemLogEntity.SortField))
            {
                SqlString = SqlString + " ORDER BY TLO." + lmSystemLogEntity.SortField + " " + lmSystemLogEntity.SortType;
            }
            DataSet dsResult = DbManager.Query(SqlString, parm, (lmSystemLogEntity.PageCurrent - 1) * lmSystemLogEntity.PageSize, lmSystemLogEntity.PageSize, true);
            lmSystemLogEntity.QueryResult = dsResult;

            return lmSystemLogEntity;
        }


        public static LmSystemLogEntity ReviewLmOrderLogSelectCountByRests(LmSystemLogEntity lmSystemLogEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("ORDERCHANNEL",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar),
                                    new OracleParameter("AFFIRMSTATUS",OracleType.VarChar),
                                    new OracleParameter("RESTBOOKSTATUS",OracleType.VarChar),
                                    new OracleParameter("RESTPRICECODE",OracleType.VarChar),
                                    new OracleParameter("TICKET",OracleType.VarChar),
                                    new OracleParameter("HOTELCOMFIRM",OracleType.VarChar)
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

            if (String.IsNullOrEmpty(lmSystemLogEntity.OrderChannel))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = lmSystemLogEntity.OrderChannel;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.PriceCode))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = lmSystemLogEntity.PriceCode;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.AffirmStatus))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = lmSystemLogEntity.AffirmStatus;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.RestBookStatus))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = lmSystemLogEntity.RestBookStatus;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.RestPriceCode))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = lmSystemLogEntity.RestPriceCode;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.Ticket))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = lmSystemLogEntity.Ticket;
            }

            if (String.IsNullOrEmpty(lmSystemLogEntity.HotelComfirm))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = lmSystemLogEntity.HotelComfirm;
            }

            DataSet dsResult = DbManager.Query("OrderSearch", "ReviewLmOrderLogSelectCountByRests", true, parm);
            lmSystemLogEntity.QueryResult = dsResult;

            return lmSystemLogEntity;
        }
    }
}
