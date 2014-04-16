using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.DataAccess;
using HotelVp.CMS.Domain.Entity.Order;

namespace HotelVp.CMS.Domain.ServiceAdapter
{
    public abstract class OrderInfoSA
    {
        public static LmSystemLogEntity SendMsgToLMSales(LmSystemLogEntity dbParm)
        {
            string DataString = "";
            DataString = DataString + "{\"method\":\"save\",\"data\":{\"syscode\":\"" + "CMS" + "\",\"reqid\":\"" + dbParm.SalesTel + "\",\"bizcode\":\"" + "CMS-LM联系人短信通知" + "\",";
            DataString = DataString + "\"cnfnum\":\"" + dbParm.EventID + "\",\"mobiles\":\"" + dbParm.SalesTel + "\",\"msg\":\"" + dbParm.SendMSG + "\",";
            DataString = DataString + "\"sign\":\"" + PostSignKey("CMS" + dbParm.SalesTel) + "\"},\"version\":\"v1.0\"}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.applySendMsgV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelFullRoom = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);
            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);
            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"')))
            {
                dbParm.Result = 1;
                dbParm.ErrorMSG = "发送成功！";
            }
            else
            {
                dbParm.Result = 2;
                dbParm.ErrorMSG = "发送失败！" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
            }

            return dbParm;
        }

        public static int SaveOrderOperation(LmSystemLogEntity dbParm)
        {
            string DataString = "";
            DataString = DataString + "{\"orderNum\":\"" + dbParm.FogOrderID + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"opeSource\":\"" + "CMS" + "\"}";

            string SaveOrderOperationUrl = JsonRequestURLBuilder.saveOrderOperationV2();

            if ("5".Equals(dbParm.OrderBookStatus) || "7".Equals(dbParm.OrderBookStatus) || "8".Equals(dbParm.OrderBookStatus))
            {
                SaveOrderOperationUrl = JsonRequestURLBuilder.saveAuditOrderOperationV2();
            }

            CallWebPage callWebPage = new CallWebPage();
            string strSaveOrderOperation = callWebPage.CallWebByURL(SaveOrderOperationUrl, DataString);
            return 1;
        }

        public static string SynOrderOperationList(LmSystemLogEntity dbParm)
        {
            string DataString = "";
            DataString = DataString + "{\"orderNum\":\"" + dbParm.FogOrderID + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"opeSource\":\"" + "CMS" + "\"}";

            string SaveOrderOperationUrl = JsonRequestURLBuilder.saveOrderOperationV2();

            CallWebPage callWebPage = new CallWebPage();
            string strSaveOrderOperation = callWebPage.CallWebByURL(SaveOrderOperationUrl, DataString);

            JObject oSaveOrderOperation = JObject.Parse(strSaveOrderOperation);
            if ("200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oSaveOrderOperation, "code").Trim('"')))
            {
                return "";
            }
            else
            {
                return JsonRequestURLBuilder.GetJsonStringValue(oSaveOrderOperation, "message").Trim('"');
            }
        }

        public static string SaveOrderOperationList(LmSystemLogEntity dbParm)
        {
            string DataString = "";
            DataString = DataString + "{\"orderNum\":\"" + dbParm.FogOrderID + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"opeSource\":\"" + "CMS" + "\"}";

            string SaveOrderOperationUrl = JsonRequestURLBuilder.saveOrderOperationV2();

            if ("5".Equals(dbParm.OrderBookStatus) || "7".Equals(dbParm.OrderBookStatus) || "8".Equals(dbParm.OrderBookStatus))
            {
                SaveOrderOperationUrl = JsonRequestURLBuilder.saveAuditOrderOperationV2();
            }

            CallWebPage callWebPage = new CallWebPage();
            string strSaveOrderOperation = callWebPage.CallWebByURL(SaveOrderOperationUrl, DataString);

            JObject oSaveOrderOperation = JObject.Parse(strSaveOrderOperation);
            if ("200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oSaveOrderOperation, "code").Trim('"')))
            {
                return "";
            }
            else
            {
                return JsonRequestURLBuilder.GetJsonStringValue(oSaveOrderOperation, "message").Trim('"');
            }
        }

        public static string SaveOrderOperationDoubleApprove(LmSystemLogEntity dbParm)
        {
            string DataString = "";
            if (dbParm.OrderBookStatus == "5")
            {
                DataString = "{\"orderNum\":\"" + dbParm.FogOrderID + "\",\"status\":\"" + dbParm.OrderBookStatus + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"remark\":\"" + dbParm.BookRemark + "\",\"cancelReason\":\"" + dbParm.CanelReson + "\"}";
            }
            else
            {
                DataString = "{\"orderNum\":\"" + dbParm.FogOrderID + "\",\"status\":\"" + dbParm.OrderBookStatus + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"remark\":\"" + dbParm.BookRemark + "\"}";
            }
            string HotelPrRoomPlanUrl = JsonRequestURLBuilder.IssueOrderV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelPrRoomPlan = callWebPage.CallWebByURL(HotelPrRoomPlanUrl, DataString);
            JObject oHotelPrRoomPlan = JObject.Parse(strHotelPrRoomPlan);

            if ("200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "code").Trim('"')))
            {
                return "";
            }
            else
            {
                if ("更改的订单状态不可与库中相同".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"')))
                {
                    return "";
                }
                else
                {
                    return JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"');
                }
            }
        }

        public static LmSystemLogEntity SendFaxService(LmSystemLogEntity dbParm)
        {
            string DataString = "";

            //{"faxType":"","objectId":"","operator":"","remark":"","faxContent":"","hotelList":[{"hotelId":"fasdfasfas","orderList":["0010023","12312312"]}]}
            DataString = DataString + "{\"faxType\":\"" + dbParm.SendFaxType + "\",\"objectId\":\"" + dbParm.ObjectID + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"remark\":\"" + dbParm.BookRemark + "\",\"hotelList\":[{\"hotelId\":\"" + dbParm.HotelID + "\",\"orderList\":[" + dbParm.FogOrderID + "]}]}";
            string SendFaxSeviceUrl = JsonRequestURLBuilder.sendFaxSeviceV2();

            CallWebPage callWebPage = new CallWebPage();
            string strSendFaxSevice = callWebPage.CallWebByURL(SendFaxSeviceUrl, DataString);
            //JObject oSaveOrderOperation = JObject.Parse(strSaveOrderOperation);
            //if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oSaveOrderOperation, "message").Trim('"')))
            //{
            //    return 1;
            //}
            //else
            //{
            //    return 0;
            //}

            dbParm.Result = 1;
            return dbParm;
        }

        public static int SaveOrderConfirmPlanMail(LmSystemLogEntity dbParm, DataSet dsOrderInfo)
        {
            string iCType = "";
            if ("LMBAR".Equals(dsOrderInfo.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()))
            {
                if ("4".Equals(dbParm.OrderBookStatus))
                {
                    iCType = dbParm.CanelReson;
                }
            }
            else
            {
                if ("9".Equals(dbParm.OrderBookStatus))
                {
                    iCType = dbParm.CanelReson;
                }
            }

            if (String.IsNullOrEmpty(iCType))
            {
                return 0;
            }

            //订单操作备注必填， 如果用户选择了‘取消订单’ 且取消原因是‘满房’ 则自动调用房控的更新计划接口， 更新该房型当天的计划， 标记为满房， 备注信息同本页面上的‘操作备注’
            //如果订单取消原因 in (‘变价’)，则自动调用房控的更新计划接口， 更新该房型最近7天的计划， 当天包括未来7天标记为下线， 备注信息同本页面上的‘操作备注’，同时调用短信发送接口， 给酒店对应的销售人员发送短信， 内容为‘xxx酒店xxx房型由于’变价‘原因，销售计划已被订单确认员xxx标记下线一周，请及时处理’并发送邮件给sales.all, 标题为’xxx酒店xxx房型由于‘变价’原因下线’，正文为‘‘xxx酒店xxx房型由于’变价‘原因，今日销售计划已被订单确认员xxx标记下线一周，请及时处理’  BR KFC’
            //如果订单取消原因 in (‘终止合作’，‘无协议’)，则自动调用房控的更新计划接口， 更新该酒店最近7天的计划， 当天包括未来7天标记为下线， 备注信息同本页面上的‘操作备注’，同时调用短信发送接口， 给酒店对应的销售人员发送短信， 内容为‘xxx酒店由于’无协议‘原因，销售计划已被订单确认员xxx标记下线一周，请及时处理’并发送邮件给sales.all, 标题为’xxx酒店由于‘无协议’原因下线一周’，正文为‘‘xxx酒店由于’变价‘原因，今日销售计划已被订单确认员xxx标记下线一周，请及时处理’  BR KFC’
            string DataString = "";
            string SaveOrderOperationUrlPlan = "";
            string strMailTo = ConfigurationManager.AppSettings["mailOrderConfirm"].ToString();
            string strMailSubject = "";
            string strMailBody = "";
            string strMsg = "";
            DateTime dtNow = (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 4) ? DateTime.Now.AddDays(-1) : DateTime.Now;

            if ("CRC01".Equals(iCType)) // 满房 //type:1 满房、2 关房、3 开房
            {
                DataString = "{\"hotelId\":\"" + dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim() + "\",\"whatDay\":\"1,2,3,4,5,6,7\",\"beginDate\":\"" + dtNow.ToShortDateString().Replace("/", "-") + "\",\"endDate\":\"" + dtNow.ToShortDateString().Replace("/", "-") + "\",\"operator\":\"" + dbParm.LogMessages.Userid + "\",\"type\":\"" + "1" + "\"}";
                SaveOrderOperationUrlPlan = JsonRequestURLBuilder.BatchUpdatePlan();
                //DataString = DataString + "{\"orderNum\":\"" + dbParm.FogOrderID + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"opeSource\":\"" + "CMS" + "\"}";
                //SaveOrderOperationUrlPlan = JsonRequestURLBuilder.saveOrderOperationV2();
            }
            else if ("CRC06".Equals(iCType)) // 酒店变价 
            {
                //DataString = DataString + "{\"orderNum\":\"" + dbParm.FogOrderID + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"opeSource\":\"" + "CMS" + "\"}";
                //SaveOrderOperationUrlPlan = JsonRequestURLBuilder.saveOrderOperationV2();

                DataString = "{\"hotelId\":\"" + dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim() + "\",\"whatDay\":\"1,2,3,4,5,6,7\",\"beginDate\":\"" + dtNow.ToShortDateString().Replace("/", "-") + "\",\"endDate\":\"" + dtNow.AddDays(7).ToShortDateString().Replace("/", "-") + "\",\"operator\":\"" + dbParm.LogMessages.Userid + "\",\"type\":\"" + "2" + "\",\"priceCodeRoom\":[{\"priceCode\":\"LMBAR2\",\"roomCode\":\"" + dsOrderInfo.Tables[0].Rows[0]["room_type_code"].ToString().Trim() + "\"}]}";
                SaveOrderOperationUrlPlan = JsonRequestURLBuilder.BatchUpdatePlan();

                strMsg = String.Format("{0} {1} 由于‘酒店变价’原因，销售计划已被订单确认员{2}标记下线一周，请及时处理！", "[" + dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim() + "]" + dsOrderInfo.Tables[0].Rows[0]["hotel_name"].ToString().Trim(), "[" + dsOrderInfo.Tables[0].Rows[0]["room_type_code"].ToString().Trim() + "]" + dsOrderInfo.Tables[0].Rows[0]["room_type_name"].ToString().Trim(), dbParm.LogMessages.Username);
                OrderConfirmSendMsgToLMSales(dbParm, dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim(), strMsg);
                strMailSubject = String.Format("{0} {1} 由于‘{2}’原因下线一周", "[" + dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim() + "]" + dsOrderInfo.Tables[0].Rows[0]["hotel_name"].ToString().Trim(), "[" + dsOrderInfo.Tables[0].Rows[0]["room_type_code"].ToString().Trim() + "]" + dsOrderInfo.Tables[0].Rows[0]["room_type_name"].ToString().Trim(), "酒店变价");
                strMailBody = "<div style='font-size:16;font-family:Microsoft YaHei;'>Hi ALL <br/><br/>" + strMsg + "<br/><br/>BR KFC</div>";
                SendMailProcess(strMailTo, strMailSubject, strMailBody);
            }
            else if ("CRH10".Equals(iCType) || "CRH07".Equals(iCType)) // 终止合作  无协议
            {
                //DataString = DataString + "{\"orderNum\":\"" + dbParm.FogOrderID + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"opeSource\":\"" + "CMS" + "\"}";
                //SaveOrderOperationUrlPlan = JsonRequestURLBuilder.saveOrderOperationV2();
                DataString = "{\"hotelId\":\"" + dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim() + "\",\"whatDay\":\"1,2,3,4,5,6,7\",\"beginDate\":\"" + dtNow.ToShortDateString().Replace("/", "-") + "\",\"endDate\":\"" + dtNow.AddDays(7).ToShortDateString().Replace("/", "-") + "\",\"operator\":\"" + dbParm.LogMessages.Userid + "\",\"type\":\"" + "2" + "\"}";
                SaveOrderOperationUrlPlan = JsonRequestURLBuilder.BatchUpdatePlan();

                strMsg = String.Format("{0} 由于‘{1}’原因，销售计划已被订单确认员{2}标记下线一周，请及时处理！", "[" + dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim() + "]" + dsOrderInfo.Tables[0].Rows[0]["hotel_name"].ToString().Trim(), ("CRH10".Equals(iCType) ? "终止合作" : "无协议"), dbParm.LogMessages.Username);
                OrderConfirmSendMsgToLMSales(dbParm, dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim(), strMsg);
                strMailSubject = String.Format("{0} 由于‘{1}’原因下线一周", "[" + dsOrderInfo.Tables[0].Rows[0]["hotel_id"].ToString().Trim() + "]" + dsOrderInfo.Tables[0].Rows[0]["hotel_name"].ToString().Trim(), ("CRH10".Equals(iCType) ? "终止合作" : "无协议"));
                strMailBody = "<div style='font-size:16;font-family:Microsoft YaHei;'>Hi ALL <br/><br/>" + strMsg + "<br/><br/>BR KFC</div>";
                SendMailProcess(strMailTo, strMailSubject, strMailBody);
            }
            else
            {
                return 0;
            }

            CallWebPage callWebPage = new CallWebPage();
            string strSaveOrderOperation = callWebPage.CallWebByURL(SaveOrderOperationUrlPlan, DataString);
            return 1;
        }

        public static LmSystemLogEntity OrderConfirmSendMsgToLMSales(LmSystemLogEntity dbParm, string strHotelID, string strSendMSG)
        {
            DataSet dsSales = LmSystemLogDA.GetSalesLMHotelInfo(strHotelID);

            if (dsSales.Tables.Count == 0 || dsSales.Tables[0].Rows.Count == 0 || String.IsNullOrEmpty(dsSales.Tables[0].Rows[0]["User_Tel"].ToString()))
            {
                return dbParm;
            }

            string DataString = "";
            DataString = DataString + "{\"method\":\"save\",\"data\":{\"syscode\":\"" + "CMS" + "\",\"reqid\":\"" + dsSales.Tables[0].Rows[0]["User_Tel"].ToString() + "\",\"bizcode\":\"" + "CMS-订单确认短信通知" + "\",";
            DataString = DataString + "\"cnfnum\":\"" + dbParm.FogOrderID + "\",\"mobiles\":\"" + dsSales.Tables[0].Rows[0]["User_Tel"].ToString() + "\",\"msg\":\"" + strSendMSG + "\",";
            DataString = DataString + "\"sign\":\"" + PostSignKey("CMS" + dsSales.Tables[0].Rows[0]["User_Tel"].ToString()) + "\"},\"version\":\"v1.0\"}";

            string HotelFullRoomUrl = JsonRequestURLBuilder.applySendMsgV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelFullRoom = callWebPage.CallWebByURL(HotelFullRoomUrl, DataString);
            JObject oHotelFullRoom = JObject.Parse(strHotelFullRoom);
            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"')))
            {
                dbParm.Result = 1;
                dbParm.ErrorMSG = "发送成功！";
            }
            else
            {
                dbParm.Result = 2;
                dbParm.ErrorMSG = "发送失败！" + JsonRequestURLBuilder.GetJsonStringValue(oHotelFullRoom, "message").Trim('"');
            }

            return dbParm;
        }

        public static bool SendMailProcess(string strMailTo, string strMailSubject, string strMailBody)
        {
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
            string mailTo = strMailTo;
            string mailHost = ConfigurationManager.AppSettings["mailHost"].ToString();
            string mailPort = ConfigurationManager.AppSettings["mailPort"].ToString();
            string mailPass = ConfigurationManager.AppSettings["mailPass"].ToString();
            string mailSubject = strMailSubject;
            string mailBody = strMailBody;
            MailMessage msg = new MailMessage();
            msg.To.Add(mailTo);
            msg.From = new MailAddress(mailFrom, mailFrom, System.Text.Encoding.UTF8);

            DateTime dtNow = DateTime.Now;
            msg.Subject = mailSubject;//邮件标题 
            msg.SubjectEncoding = Encoding.UTF8;//Encoding.Default;//邮件标题编码 
            msg.Body = msg.Body + mailBody;// ConverBodyData(title, mailBody);

            msg.BodyEncoding = Encoding.UTF8; //Encoding.Default;//邮件内容编码 
            msg.IsBodyHtml = true;//是否是HTML邮件 
            //msg.Priority = MailPriority.High;//邮件优先级 

            SmtpClient client = new SmtpClient();
            client.Host = mailHost;
            client.Port = int.Parse(mailPort);
            client.Credentials = new NetworkCredential(mailFrom, mailPass);
            //object userState = msg;
            try
            {
                //client.SendAsync(msg, userState);
                client.Send(msg);
                return true;// 发送成功
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送邮件 异常：" + ex.Message);
                return false; // 发送失败
            }
        }

        public static string PostSignKey(string body)
        {
            try
            {
                string MD5Key = ConfigurationManager.AppSettings["MSGMD5Key"].ToString();
                string signKey = FormsAuthentication.HashPasswordForStoringInConfigFile(body + MD5Key, "MD5");
                return signKey;
            }
            catch
            {
                return "";
            }
        }

        public static string CommonCallWebUrl(string strUrl)
        {
            string strJson = string.Empty;
            try
            {
                CallWebPage callWebPage = new CallWebPage();
                strJson = callWebPage.CallWebByURL(strUrl, "");
            }
            catch
            {

            }
            return strJson;
        }

        /// <summary>
        /// 查询最终订单状态（供应商）
        /// </summary>
        /// <returns></returns>
        public static OrderInfoEntity orderQueryByOrderNum(OrderInfoEntity OrderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (OrderInfoEntity.OrderInfoDBEntity.Count > 0) ? OrderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();

            string DataString = "{\"orderNum\":\"" + dbParm.FOG_ORDER_NUM + "\"}";

            string HotelPrRoomPlanUrl = JsonRequestURLBuilder.orderQueryByOrderNumV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelPrRoomPlan = callWebPage.CallWebByURL(HotelPrRoomPlanUrl, DataString);
            JObject oHotelPrRoomPlan = JObject.Parse(strHotelPrRoomPlan);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"')))
            {

                OrderInfoEntity.ErrorMSG = oHotelPrRoomPlan.SelectToken("result").SelectToken("statusDesc").ToString().Trim('"') + "(" + oHotelPrRoomPlan.SelectToken("result").SelectToken("statusOriginal").ToString().Trim('"') + ")";//最终状态（状态描述） 
                OrderInfoEntity.Result = 1;
            }
            else
            {
                OrderInfoEntity.ErrorMSG = "";
                OrderInfoEntity.Result = 2;
            }

            return OrderInfoEntity;
        }

        /// <summary>
        /// 更新订单最终状态接口
        /// </summary>
        /// <returns></returns>
        public static OrderInfoEntity updateIssueOrder(OrderInfoEntity OrderInfoEntity)
        {
            OrderInfoDBEntity dbParm = (OrderInfoEntity.OrderInfoDBEntity.Count > 0) ? OrderInfoEntity.OrderInfoDBEntity[0] : new OrderInfoDBEntity();
            string DataString = "";
            if (dbParm.BOOK_STATUS_OTHER == "9")
            {
                DataString = "{\"orderNum\":\"" + dbParm.FOG_ORDER_NUM + "\",\"status\":\"" + dbParm.BOOK_STATUS_OTHER + "\",\"operator\":\"" + dbParm.USER_ID + "\",\"remark\":\"" + dbParm.REMARK + "\",\"cancelReason\":\"" + dbParm.CanelReson + "\",\"isCoupon\":\"" + dbParm.TICKET_AMOUNT + "\"}";
            }
            else
            {
                DataString = "{\"orderNum\":\"" + dbParm.FOG_ORDER_NUM + "\",\"status\":\"" + dbParm.BOOK_STATUS_OTHER + "\",\"operator\":\"" + dbParm.USER_ID + "\",\"remark\":\"" + dbParm.REMARK + "\",\"isCoupon\":\"" + dbParm.TICKET_AMOUNT + "\"}";
            }
            string HotelPrRoomPlanUrl = JsonRequestURLBuilder.IssueOrderV2();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelPrRoomPlan = callWebPage.CallWebByURL(HotelPrRoomPlanUrl, DataString);
            JObject oHotelPrRoomPlan = JObject.Parse(strHotelPrRoomPlan);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"')) && "200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "code").Trim('"')))
            {

                OrderInfoEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"');
                OrderInfoEntity.Result = 1;
            }
            else
            {
                OrderInfoEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"') + JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "code").Trim('"');
                OrderInfoEntity.Result = 2;
            }

            return OrderInfoEntity;
        }


        public static LmSystemLogEntity SaveBindReciveFax(LmSystemLogEntity dbParm)
        {
            string DataString = "";

            //{"faxType":"","objectId":"","operator":"","remark":"","faxContent":"","hotelList":[{"hotelId":"fasdfasfas","orderList":["0010023","12312312"]}]}
            DataString = DataString + "{\"barCode\":\"" + dbParm.BarCode + "\",\"unknownId\":\"" + dbParm.FaxID + "\",\"operator\":\"" + dbParm.LogMessages.Username + "\",\"remark\":\"" + dbParm.BookRemark + "\"}";
            string SendFaxSeviceUrl = JsonRequestURLBuilder.sendBindFaxSeviceV2();

            CallWebPage callWebPage = new CallWebPage();
            string strSendFaxSevice = callWebPage.CallWebByURL(SendFaxSeviceUrl, DataString);
            //JObject oSaveOrderOperation = JObject.Parse(strSaveOrderOperation);
            //if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oSaveOrderOperation, "message").Trim('"')))
            //{
            //    return 1;
            //}
            //else
            //{
            //    return 0;
            //}

            dbParm.Result = 1;
            return dbParm;
        }

        /// <summary>
        /// 预付退款接口
        /// </summary>
        /// <param name="OrderRefundEntity"></param>
        /// <returns></returns>
        public static OrderRefundEntity saveRefund(OrderRefundEntity OrderRefundEntity)
        {
            OrderRefundDBEntity dbParm = (OrderRefundEntity.OrderRefundDBEntity.Count > 0) ? OrderRefundEntity.OrderRefundDBEntity[0] : new OrderRefundDBEntity();

            string DataString = "{\"OrderNum\":\"" + dbParm.Obj_id + "\",\"amount\":" + dbParm.Amount + ",\"operator\":\"" + dbParm.Operators + "\",\"remark\":\"" + dbParm.Remark + "\",\"sn\":\"" + dbParm.Sn + "\",\"refundAccount\":\"" + dbParm.Refund_account + "\",\"refundTime\":\"" + dbParm.Refund_time + "\",\"type\":\"" + dbParm.Type + "\"}";

            string HotelPrRoomPlanUrl = JsonRequestURLBuilder.saveRefund();
            CallWebPage callWebPage = new CallWebPage();
            string strHotelPrRoomPlan = callWebPage.CallWebByURL(HotelPrRoomPlanUrl, DataString);
            JObject oHotelPrRoomPlan = JObject.Parse(strHotelPrRoomPlan);

            if ("success".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"')) && "200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "code").Trim('"')))
            {
                OrderRefundEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"');
                OrderRefundEntity.Result = 1;
            }
            else
            {
                OrderRefundEntity.ErrorMSG = JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "message").Trim('"') + JsonRequestURLBuilder.GetJsonStringValue(oHotelPrRoomPlan, "code").Trim('"');
                OrderRefundEntity.Result = -1;
            }

            return OrderRefundEntity;
        }
    }
}