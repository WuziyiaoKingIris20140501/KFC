using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data;

using HotelVp.EBOK.Domain.API;
using HotelVp.EBOK.Domain.API.Model;

namespace HotelVp.EBOK.Domain.Biz
{
    public abstract class PDReportBP
    {
        public static DataSet AjxGetPDReportQueryList(string mac, string hId, string beginDate, string endDate)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                List<OrderList> orderlist = PDReportAPI.AjxGetPDReportQueryList(mac, hId, beginDate, endDate).result;
                DataSet dsResult = new DataSet();

                dsResult.Tables.Add(new DataTable());
                dsResult.Tables[0].Columns.Add("ROOMCD");
                dsResult.Tables[0].Columns.Add("ODZS");
                dsResult.Tables[0].Columns.Add("ODRZ");
                dsResult.Tables[0].Columns.Add("ODNS");
                dsResult.Tables[0].Columns.Add("ODQT");
                dsResult.Tables[0].Columns.Add("ODLD");
                dsResult.Tables[0].Columns.Add("ODJE");

                // 总订单  总确认可住单  总待确认单  总离店单  总No-Show单  总取消单

                // 房型名称  订单数  确认可住单  待确认单  离店单  No-Show单  取消单
                // 房型1      XX         XX         XX         XX       XX       XX
                // 房型2      XX         XX         XX         XX       XX       XX
                // 房型3      XX         XX         XX         XX       XX       XX
                // 房型4      XX         XX         XX         XX       XX       XX
                string strRoom = string.Empty;
                foreach (OrderList order in orderlist)
                {
                    if ("2".Equals(order.bookStatusOther))
                    {
                        continue;
                    }
                    strRoom = "[" + order.roomTypeCode + "]" + order.roomTypeName;
                    int iRow = 0;
                    if (dsResult.Tables[0].Select("ROOMCD='" + strRoom + "'").Count() == 0)
                    {
                        DataRow dr = dsResult.Tables[0].NewRow();
                        dr["ROOMCD"] = strRoom;
                        dr["ODZS"] = 0;
                        dr["ODRZ"] = 0;
                        dr["ODNS"] = 0;
                        dr["ODQT"] = 0;
                        dr["ODLD"] = 0;
                        dr["ODJE"] = 0;
                        dsResult.Tables[0].Rows.Add(dr);
                    }

                    for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                    {
                        if (strRoom.Equals(dsResult.Tables[0].Rows[i]["ROOMCD"].ToString()))
                        {
                            iRow = i;
                            break;
                        }
                    }

                    if (!"3".Equals(order.bookStatusOther))
                    {
                        dsResult.Tables[0].Rows[iRow]["ODZS"] = int.Parse(dsResult.Tables[0].Rows[iRow]["ODZS"].ToString()) + 1;
                    }

                    if ("0".Equals(order.bookStatusOther) || "1".Equals(order.bookStatusOther)) // 待确认
                    {
                        dsResult.Tables[0].Rows[iRow]["ODRZ"] = int.Parse(dsResult.Tables[0].Rows[iRow]["ODRZ"].ToString()) + 1;
                    }
                    else if ("4".Equals(order.bookStatusOther) || "6".Equals(order.bookStatusOther) || "7".Equals(order.bookStatusOther)) // 可住
                    {
                        dsResult.Tables[0].Rows[iRow]["ODQT"] = int.Parse(dsResult.Tables[0].Rows[iRow]["ODQT"].ToString()) + 1;
                    }
                    else if ("8".Equals(order.bookStatusOther)) // 离店
                    {
                        dsResult.Tables[0].Rows[iRow]["ODLD"] = int.Parse(dsResult.Tables[0].Rows[iRow]["ODLD"].ToString()) + 1;
                        dsResult.Tables[0].Rows[iRow]["ODJE"] = decimal.Parse(dsResult.Tables[0].Rows[iRow]["ODJE"].ToString()) + decimal.Parse(order.bookTotalPrice);
                    }
                    else if ("5".Equals(order.bookStatusOther)) // No-Show
                    {
                        dsResult.Tables[0].Rows[iRow]["ODNS"] = int.Parse(dsResult.Tables[0].Rows[iRow]["ODNS"].ToString()) + 1;
                    }
                    else if ("9".Equals(order.bookStatusOther)) // 取消单"3".Equals(order.bookStatusOther) ||
                    {
                        dsResult.Tables[0].Rows[iRow]["ODQT"] = int.Parse(dsResult.Tables[0].Rows[iRow]["ODQT"].ToString()) + 1;
                    }
                }

                return dsResult;
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }
    }
}
