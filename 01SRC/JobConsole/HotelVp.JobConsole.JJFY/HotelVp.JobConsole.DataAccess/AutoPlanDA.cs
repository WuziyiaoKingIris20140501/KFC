using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using MySql.Data.MySqlClient;
//using HotelVp.Common;
//using HotelVp.Common.DBUtility;
//using HotelVp.Common.Utilities;
//using HotelVp.Common.DataAccess;
//using HotelVp.Common.Configuration;
using HotelVp.Jjzx.DBUtility;
using JJZX.JobConsole.Entity;
using HotelVp.Common.DBUtility;

namespace JJZX.JobConsole.DataAccess
{
    public abstract class AutoPlanDA
    {
        public static DataSet GetOrderYesterDayList()
        {
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("AutoPlan", "GetOrderYesterDayList");
            // 取得前一天入住的会员 通过手机APP预订入住的 充值的是预订人电话
            return DbHelperMySQL.Query(SqlString);
        }

        public static DataSet GetOrderYFComfirnList()
        {
            // 取得所有充值状态未同步回来的数据进行充值结果的同步
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("AutoPlan", "GetOrderYFComfirnList");
            return DbHelperMySQL.Query(SqlString);
        }

        public static int InsertYFOrderList(AutoHotelPlanDBEntity dbParm, Hashtable hsResult, string MsgRes)
        {
            string SendResult = hsResult.ContainsKey("result") ?  hsResult["result"].ToString() : "";
            string Pro = hsResult.ContainsKey("pro") ?  hsResult["pro"].ToString() : "";
            string Yys = hsResult.ContainsKey("yys") ?  hsResult["yys"].ToString() : "";
            string Code = hsResult.ContainsKey("code") ?  hsResult["code"].ToString() : "";
            string Price = hsResult.ContainsKey("price") ? hsResult["price"].ToString() : "";

            //订单号、预订人、入住门店、订单状态、预订人手机号、预订入住时间、预订离店时间、充值话费金额、充值状态、充值时间、运营商、入主渠道、平台
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("AutoPlan", "InsertYFOrderList");
            MySqlParameter[] cmdParms ={
                                    new MySqlParameter("RESVID", MySqlDbType.VarChar),
                                    new MySqlParameter("NAME", MySqlDbType.VarChar),
                                    new MySqlParameter("MOBILE", MySqlDbType.VarChar),
                                    new MySqlParameter("UNIT_ID", MySqlDbType.VarChar),
                                    new MySqlParameter("ORDER_STATUS", MySqlDbType.VarChar),
                                    new MySqlParameter("ARRD", MySqlDbType.VarChar),
                                    new MySqlParameter("DEPD", MySqlDbType.VarChar),
                                    new MySqlParameter("CHANNEL", MySqlDbType.VarChar),
                                    new MySqlParameter("PLATFORM_CODE", MySqlDbType.VarChar),
                                    new MySqlParameter("SRESULT", MySqlDbType.VarChar),
                                    new MySqlParameter("PRO", MySqlDbType.VarChar),
                                    new MySqlParameter("YYS", MySqlDbType.VarChar),
                                    new MySqlParameter("CODE", MySqlDbType.VarChar),
                                    new MySqlParameter("PRICE", MySqlDbType.VarChar),
                                    new MySqlParameter("MONEY", MySqlDbType.VarChar),
                                    new MySqlParameter("GUEST_ID", MySqlDbType.VarChar),
                                    new MySqlParameter("MSGRES", MySqlDbType.VarChar)
                                };

            cmdParms[0].Value = dbParm.OrderId;
            cmdParms[1].Value = dbParm.NAME;
            cmdParms[2].Value = dbParm.MobileNum;
            cmdParms[3].Value = dbParm.UNIT_ID;
            cmdParms[4].Value = dbParm.ORDER_STATUS;
            cmdParms[5].Value = dbParm.ARRD;
            cmdParms[6].Value = dbParm.DEPD;
            cmdParms[7].Value = dbParm.CHANNEL;
            cmdParms[8].Value = dbParm.PLATFORM_CODE;
            cmdParms[9].Value =SendResult;
            cmdParms[10].Value = Pro;
            cmdParms[11].Value = Yys;
            cmdParms[12].Value = Code;
            cmdParms[13].Value = Price;
            cmdParms[14].Value = dbParm.Money;
            cmdParms[15].Value = dbParm.GUEST_ID;
            cmdParms[16].Value = MsgRes;
            return DbHelperMySQL.ExecuteSql(SqlString, cmdParms);
        }

        public static int UpdateYFOrderStatus(string OrderID, Hashtable hsResult, string MsgRes)
        {
            string GetResult = hsResult.ContainsKey("result") ?  hsResult["result"].ToString() : "";
            string GetStatus = hsResult.ContainsKey("state") ? hsResult["state"].ToString() : "";

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("AutoPlan", "UpdateYFOrderStatus");
            MySqlParameter[] cmdParms ={
                                    new MySqlParameter("RESVID", MySqlDbType.VarChar),
                                    new MySqlParameter("GSTATUS", MySqlDbType.VarChar),
                                    new MySqlParameter("GRESULT", MySqlDbType.VarChar),
                                    new MySqlParameter("MSGRES", MySqlDbType.VarChar)
                                };

            cmdParms[0].Value = OrderID;
            cmdParms[1].Value = GetStatus;
            cmdParms[2].Value = GetResult;
            cmdParms[3].Value = MsgRes;
            return DbHelperMySQL.ExecuteSql(SqlString, cmdParms);
        }
    }
}