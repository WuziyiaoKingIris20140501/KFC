using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Data.OracleClient;
using System.Net;
using System.Net.Mail;
using System.Threading;

using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.JobConsole.Entity;
using HotelVp.JobConsole.DataAccess;
using HotelVp.JobConsole.ServiceAdapter;


namespace HotelVp.JobConsole.Biz
{
    public abstract class AutoHotelPlanBP
    {
        //static string _nameSpaceClass = "HotelVp.JobConsole.Biz.AutoHotelPlanBP  Method: ";
        public static void AutoHotelPlaning()
        {
            DateTime dtBegin = new DateTime();
            dtBegin = System.DateTime.Now;

            AutoHotelPlanEntity _autohotelplanEntity = new AutoHotelPlanEntity();
            CommonEntity _commonEntity = new CommonEntity();
            _autohotelplanEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _autohotelplanEntity.LogMessages.Userid = "JOB System";
            _autohotelplanEntity.LogMessages.Username = "JOB System";
            _autohotelplanEntity.AutoHotelPlanDBEntity = new List<AutoHotelPlanDBEntity>();
            AutoHotelPlanDBEntity appcontentDBEntity = new AutoHotelPlanDBEntity();
            _autohotelplanEntity.AutoHotelPlanDBEntity.Add(appcontentDBEntity);
            Console.WriteLine("酒店销售计划JOB自动运行开始");
            try
            {
                int iCount = AutoSelect(_autohotelplanEntity);
                Console.WriteLine("酒店销售计划JOB自动运行 执行记录数：" + iCount.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("JOB Error:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
            }
            //Console.WriteLine("发送邮件开始");
            //bool bResult = SendMailExpress(dsResult);
            //string strResult = "失败";
            //if (bResult)
            //{
            //    strResult = "成功";
            //}
            //else
            //{
            //    Thread.Sleep(5000);
            //}

            //Console.WriteLine("发送邮件结束 结果：" + strResult);

            DateTime dtEnd = new DateTime();
            dtEnd = System.DateTime.Now;

            Console.WriteLine(dtEnd - dtBegin);
        }

        private static int AutoSelect(AutoHotelPlanEntity autogotelplanEntity)
        {
            int iMaxLenth = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLength"]) ? 200 : int.Parse(ConfigurationManager.AppSettings["MaxLength"].ToString());
            DataSet dsResult = new DataSet();
            dsResult = AutoHotelPlanDA.AutoListSelect(autogotelplanEntity);
            int iCount = 0;
            string PlanID=string.Empty;
            string Status=string.Empty;
            string Action=string.Empty;
            string Result=string.Empty;
            string Username = string.Empty;
            //string strToDay = DateTime.Now.ToShortDateString();
            //int strToWeek = (int)DateTime.Now.DayOfWeek + 1;
            //string strDayTemp = string.Empty;
            string strWeekTemp = string.Empty;
            string strTypeNm = string.Empty;
            string strTypeTime = string.Empty;
            string ChkResult = string.Empty;

            ArrayList alHotelList = new ArrayList();
            Hashtable htErrList = new Hashtable();
            Hashtable htErr = new Hashtable();

            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                if (!"0".Equals(drRow["Type"].ToString().Trim()))
                {
                    ChkResult = CheckApplySalesRoomService(drRow, drRow["Type"].ToString().Trim());
                    if (!String.IsNullOrEmpty(ChkResult))
                    {
                        htErrList.Add(drRow["HPID"].ToString().Trim(), ChkResult);
                    }
                    else
                    {
                        alHotelList.Add(drRow);
                    }
                }
                else
                {
                    alHotelList.Add(drRow);
                }

                iCount = iCount + 1;
                if (iCount == iMaxLenth)
                {
                    htErr = ApplySalesRoomServiceList(alHotelList);
                    foreach (System.Collections.DictionaryEntry item in htErr)
                    {
                        if (!htErrList.ContainsKey(item.Key.ToString()))
                        {
                            htErrList.Add(item.Key.ToString(), item.Value.ToString());
                        }
                    }
                    iCount = 0;
                }
            }

            if (iCount > 0)
            {
                htErr = ApplySalesRoomServiceList(alHotelList);
                foreach (System.Collections.DictionaryEntry item in htErr)
                {
                    if (!htErrList.ContainsKey(item.Key.ToString()))
                    {
                        htErrList.Add(item.Key.ToString(), item.Value.ToString());
                    }
                }
            }

            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                if ("0".Equals(drRow["Type"].ToString().Trim()))
                {
                    strTypeNm = "立即保存";
                    strTypeTime = drRow["Plan_DTime"].ToString().Trim();
                    PlanID = drRow["HPID"].ToString().Trim();
                    Status = "2";
                    Action = "1";
                    //Result = ApplySalesRoomService(drRow, drRow["Type"].ToString().Trim());
                    Result = htErrList[drRow["HPID"].ToString().Trim()].ToString();
                    Username = drRow["Create_User"].ToString().Trim();
                    AutoHotelPlanDA.UpdateSalesPlanEventStatus(PlanID, Status, Action, Result, Username);
                    AutoHotelPlanDA.UpdateSalesPlanEventJobStatus(drRow["JID"].ToString().Trim(), Action, Result, Username);
                    iCount = iCount + 1;
                }
                else if ("1".Equals(drRow["Type"].ToString().Trim()))
                {
                    strTypeNm = "定时保存";
                    strTypeTime = drRow["Plan_DTime"].ToString().Trim();
                    PlanID = drRow["HPID"].ToString().Trim();
                    Status = "2";
                    Action = "1";
                    //Result = ApplySalesRoomService(drRow, drRow["Type"].ToString().Trim());
                    Result = htErrList[drRow["HPID"].ToString().Trim()].ToString();
                    Username = drRow["Create_User"].ToString().Trim();
                    AutoHotelPlanDA.UpdateSalesPlanEventStatus(PlanID, Status, Action, Result, Username);
                    AutoHotelPlanDA.UpdateSalesPlanEventJobStatus(drRow["JID"].ToString().Trim(), Action, Result, Username);
                    iCount = iCount + 1;
                }
                else if ("2".Equals(drRow["Type"].ToString().Trim()))
                {
                    strTypeNm = "每日自动更新";
                    strTypeTime = drRow["PlanTime"].ToString().Trim();
                    //strDayTemp = drRow["EndDtime"].ToString().Trim();
                    strWeekTemp = drRow["Week_List"].ToString().Trim();

                    Status = (DateTime.Parse(drRow["JPDTime"].ToString().Trim()) == DateTime.Parse(drRow["PlanPlanEnd"].ToString().Trim() + " " + drRow["PlanTime"].ToString().Trim())) ? "2" : "1";
                    PlanID = drRow["HPID"].ToString().Trim();

                    //Console.WriteLine("Action:" + drRow["Action"].ToString().Trim());
                    //Console.WriteLine("HPID:" + drRow["HPID"].ToString().Trim());
                    //Console.WriteLine(htErrList[drRow["HPID"].ToString().Trim()].ToString());

                    Action = ChkNumerVal(drRow["Action"].ToString().Trim()).ToString();
                    //Result = ApplySalesRoomService(drRow, drRow["Type"].ToString().Trim());
                    Result = htErrList[drRow["HPID"].ToString().Trim()].ToString();
                    Username = drRow["Create_User"].ToString().Trim();
                    AutoHotelPlanDA.UpdateSalesPlanEventStatus(PlanID, Status, Action, Result, Username);
                    AutoHotelPlanDA.UpdateSalesPlanEventJobStatus(drRow["JID"].ToString().Trim(), "1", Result, Username);
                    iCount = iCount + 1;
                }

                CommonEntity _commonEntity = new CommonEntity();
                _commonEntity.LogMessages = autogotelplanEntity.LogMessages;
                _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
                CommonDBEntity commonDBEntity = new CommonDBEntity();

                commonDBEntity.Event_Type = "酒店销售计划-JOB";
                commonDBEntity.Event_ID = drRow["HPID"].ToString().Trim();
                string conTent = "销售计划 运行一次 - 计划ID：{0} 更新方式：{1} 定时执行时间：{2} 定时开始日期：{3} 定时结束日期：{4} 星期详情：{5} 修改时间：{6} 修改人：{7}";
                conTent = string.Format(conTent, drRow["HPID"].ToString().Trim(), strTypeNm, strTypeTime, drRow["StartDtime"].ToString().Trim(), drRow["EndDtime"].ToString().Trim(), strWeekTemp, drRow["Update_Time"].ToString().Trim(), drRow["Update_User"].ToString().Trim());
                commonDBEntity.Event_Content = conTent;
                commonDBEntity.Event_Result = Result;
                _commonEntity.CommonDBEntity.Add(commonDBEntity);
                CommonBP.InsertEventHistory(_commonEntity);
            }
            return dsResult.Tables[0].Rows.Count;
        }

        private static int ChkNumerVal(string param)
        {
            if (String.IsNullOrEmpty(param))
            {
                return 1;
            }

            try
            {
                return int.Parse(param) + 1;
            }
            catch
            {
                return 1;
            }
        }

        private static string CheckApplySalesRoomService(DataRow drRow, string Type)
        {
            AutoHotelPlanEntity _autohotelplanEntity = new AutoHotelPlanEntity();
            _autohotelplanEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _autohotelplanEntity.LogMessages.Userid = "JOB System";
            _autohotelplanEntity.LogMessages.Username = drRow["Create_User"].ToString().Trim();
            _autohotelplanEntity.AutoHotelPlanDBEntity = new List<AutoHotelPlanDBEntity>();
            AutoHotelPlanDBEntity appcontentDBEntity = new AutoHotelPlanDBEntity();

            appcontentDBEntity.HotelID = drRow["HOTEL_ID"].ToString().Trim();
            appcontentDBEntity.PriceCode = drRow["RATE_CODE"].ToString().Trim();
            appcontentDBEntity.RoomCode = drRow["ROOM_TYPE_CODE"].ToString().Trim();
            appcontentDBEntity.RoomName = drRow["ROOM_TYPE_NAME"].ToString().Trim();
            appcontentDBEntity.StartDTime = drRow["StartDtime"].ToString().Trim();
            appcontentDBEntity.EndDTime = drRow["EndDtime"].ToString().Trim();
            appcontentDBEntity.WeekList = drRow["Week_List"].ToString().Trim();
            appcontentDBEntity.Note1 = drRow["GUAID"].ToString().Trim();
            appcontentDBEntity.Note2 = drRow["CXLID"].ToString().Trim();
            appcontentDBEntity.OnePrice = drRow["ONE_PRICE"].ToString().Trim();
            appcontentDBEntity.TwoPrice = drRow["TWO_PRICE"].ToString().Trim();
            appcontentDBEntity.ThreePrice = drRow["THREE_PRICE"].ToString().Trim();
            appcontentDBEntity.FourPrice = drRow["FOUR_PRICE"].ToString().Trim();
            appcontentDBEntity.BedPrice = drRow["ATTN_PRICE"].ToString().Trim();
            appcontentDBEntity.BreakfastNum = drRow["BREAKFAST_NUM"].ToString().Trim();
            appcontentDBEntity.BreakPrice = drRow["EACH_BREAKFAST_PRICE"].ToString().Trim();
            appcontentDBEntity.IsNetwork = drRow["IS_NETWORK"].ToString().Trim();
            appcontentDBEntity.Offsetval = drRow["OFFSETVAL"].ToString().Trim();
            appcontentDBEntity.Offsetunit = drRow["OFFSETUNIT"].ToString().Trim();
            appcontentDBEntity.RoomStatus = drRow["STATUS"].ToString().Trim();
            appcontentDBEntity.RoomCount = drRow["ROOM_NUM"].ToString().Trim();
            appcontentDBEntity.IsReserve = drRow["IS_RESERVE"].ToString().Trim();
            appcontentDBEntity.UpdateUser = drRow["Create_User"].ToString().Trim();
            appcontentDBEntity.TypeID = Type;

            _autohotelplanEntity.AutoHotelPlanDBEntity.Add(appcontentDBEntity);
            _autohotelplanEntity = AutoHotelPlanSA.CheckSalesHotelPlan(_autohotelplanEntity);

            return _autohotelplanEntity.ErrorMSG;
        }

        private static Hashtable ApplySalesRoomServiceList(ArrayList alHotelList)
        {
            AutoHotelPlanEntity _autohotelplanEntity = new AutoHotelPlanEntity();
            _autohotelplanEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _autohotelplanEntity.LogMessages.Userid = "JOB System";
            _autohotelplanEntity.LogMessages.Username = "JOB System";
            _autohotelplanEntity.AutoHotelPlanDBEntity = new List<AutoHotelPlanDBEntity>();

            for (int i = 0; i < alHotelList.Count; i++)
            {
                DataRow drRow = (DataRow)alHotelList[i];
                AutoHotelPlanDBEntity appcontentDBEntity = new AutoHotelPlanDBEntity();
                appcontentDBEntity.HotelID = drRow["HOTEL_ID"].ToString().Trim();
                appcontentDBEntity.PriceCode = drRow["RATE_CODE"].ToString().Trim();
                appcontentDBEntity.RoomCode = drRow["ROOM_TYPE_CODE"].ToString().Trim();
                appcontentDBEntity.RoomName = drRow["ROOM_TYPE_NAME"].ToString().Trim();
                appcontentDBEntity.StartDTime = drRow["StartDtime"].ToString().Trim();
                appcontentDBEntity.EndDTime = drRow["EndDtime"].ToString().Trim();
                appcontentDBEntity.EffHour = drRow["EFFECT_HOUR"].ToString().Trim();
                appcontentDBEntity.WeekList = drRow["Week_List"].ToString().Trim();
                appcontentDBEntity.Note1 = drRow["GUAID"].ToString().Trim();
                appcontentDBEntity.Note2 = drRow["CXLID"].ToString().Trim();
                appcontentDBEntity.OnePrice = drRow["ONE_PRICE"].ToString().Trim();
                appcontentDBEntity.TwoPrice = drRow["TWO_PRICE"].ToString().Trim();
                appcontentDBEntity.ThreePrice = drRow["THREE_PRICE"].ToString().Trim();
                appcontentDBEntity.FourPrice = drRow["FOUR_PRICE"].ToString().Trim();
                appcontentDBEntity.BedPrice = drRow["ATTN_PRICE"].ToString().Trim();
                appcontentDBEntity.NetPrice = drRow["NET_PRICE"].ToString().Trim();
                appcontentDBEntity.BreakfastNum = drRow["BREAKFAST_NUM"].ToString().Trim();
                appcontentDBEntity.BreakPrice = drRow["EACH_BREAKFAST_PRICE"].ToString().Trim();
                appcontentDBEntity.IsNetwork = drRow["IS_NETWORK"].ToString().Trim();
                appcontentDBEntity.Offsetval = drRow["OFFSETVAL"].ToString().Trim();
                appcontentDBEntity.Offsetunit = drRow["OFFSETUNIT"].ToString().Trim();
                appcontentDBEntity.RoomStatus = drRow["STATUS"].ToString().Trim();
                appcontentDBEntity.RoomCount = drRow["ROOM_NUM"].ToString().Trim();
                appcontentDBEntity.IsReserve = drRow["IS_RESERVE"].ToString().Trim();
                appcontentDBEntity.UpdateUser = drRow["Create_User"].ToString().Trim();
                appcontentDBEntity.TypeID = drRow["Type"].ToString().Trim();
                appcontentDBEntity.HPID = drRow["HPID"].ToString().Trim();
                appcontentDBEntity.Supplier = drRow["SOURCE"].ToString().Trim();
                _autohotelplanEntity.AutoHotelPlanDBEntity.Add(appcontentDBEntity);
            }

            Hashtable htResult = new Hashtable();
            htResult = AutoHotelPlanSA.ApplySalesPlanList(_autohotelplanEntity);
            return htResult;
        }

        private static string ApplySalesRoomService(DataRow drRow, string Type)
        {
            AutoHotelPlanEntity _autohotelplanEntity = new AutoHotelPlanEntity();
            _autohotelplanEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _autohotelplanEntity.LogMessages.Userid = "JOB System";
            _autohotelplanEntity.LogMessages.Username = drRow["Create_User"].ToString().Trim();
            _autohotelplanEntity.AutoHotelPlanDBEntity = new List<AutoHotelPlanDBEntity>();
            AutoHotelPlanDBEntity appcontentDBEntity = new AutoHotelPlanDBEntity();

            appcontentDBEntity.HotelID = drRow["HOTEL_ID"].ToString().Trim();
            appcontentDBEntity.PriceCode = drRow["RATE_CODE"].ToString().Trim();
            appcontentDBEntity.RoomCode = drRow["ROOM_TYPE_CODE"].ToString().Trim();
            appcontentDBEntity.RoomName = drRow["ROOM_TYPE_NAME"].ToString().Trim();
            appcontentDBEntity.StartDTime = drRow["StartDtime"].ToString().Trim();
            appcontentDBEntity.EndDTime = drRow["EndDtime"].ToString().Trim();
            appcontentDBEntity.EffHour = drRow["EFFECT_HOUR"].ToString().Trim();
            appcontentDBEntity.WeekList = drRow["Week_List"].ToString().Trim();
            appcontentDBEntity.Note1 = drRow["GUAID"].ToString().Trim();
            appcontentDBEntity.Note2 = drRow["CXLID"].ToString().Trim();
            appcontentDBEntity.OnePrice = drRow["ONE_PRICE"].ToString().Trim();
            appcontentDBEntity.TwoPrice = drRow["TWO_PRICE"].ToString().Trim();
            appcontentDBEntity.ThreePrice = drRow["THREE_PRICE"].ToString().Trim();
            appcontentDBEntity.FourPrice = drRow["FOUR_PRICE"].ToString().Trim();
            appcontentDBEntity.BedPrice = drRow["ATTN_PRICE"].ToString().Trim();
            appcontentDBEntity.BreakfastNum = drRow["BREAKFAST_NUM"].ToString().Trim();
            appcontentDBEntity.BreakPrice = drRow["EACH_BREAKFAST_PRICE"].ToString().Trim();
            appcontentDBEntity.IsNetwork = drRow["IS_NETWORK"].ToString().Trim();
            appcontentDBEntity.Offsetval = drRow["OFFSETVAL"].ToString().Trim();
            appcontentDBEntity.Offsetunit = drRow["OFFSETUNIT"].ToString().Trim();
            appcontentDBEntity.RoomStatus = drRow["STATUS"].ToString().Trim();
            appcontentDBEntity.RoomCount = drRow["ROOM_NUM"].ToString().Trim();
            appcontentDBEntity.IsReserve = drRow["IS_RESERVE"].ToString().Trim();
            appcontentDBEntity.UpdateUser = drRow["Create_User"].ToString().Trim();
            appcontentDBEntity.TypeID = Type;

            _autohotelplanEntity.AutoHotelPlanDBEntity.Add(appcontentDBEntity);
            _autohotelplanEntity = AutoHotelPlanSA.ApplySalesPlan(_autohotelplanEntity);

            return _autohotelplanEntity.ErrorMSG;
        }

        private static bool SendMailExpress(DataSet dsParm)
        {
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
            string mailTo = ConfigurationManager.AppSettings["mailTo"].ToString();
            string mailCC = ConfigurationManager.AppSettings["mailCC"].ToString();
            string mailHost = ConfigurationManager.AppSettings["mailHost"].ToString();
            string mailPort = ConfigurationManager.AppSettings["mailPort"].ToString();
            string mailPass = ConfigurationManager.AppSettings["mailPass"].ToString();
            string mailSubject = ConfigurationManager.AppSettings["mailSubject"].ToString();
            string mailBody = ConfigurationManager.AppSettings["mailBody"].ToString();
            
            MailMessage msg = new MailMessage();
            msg.To.Add(mailTo);
            if (!String.IsNullOrEmpty(mailCC))
            {
                msg.CC.Add(mailCC);
            }
            //可以抄送给多人 

            msg.From = new MailAddress(mailFrom, mailFrom, System.Text.Encoding.UTF8);

            DateTime dtNow = DateTime.Now;
            msg.Subject = dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + mailSubject;//邮件标题 
            msg.SubjectEncoding = Encoding.UTF8;//Encoding.Default;//邮件标题编码 
            //msg.Body = dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + mailBody;//"07/03/2012 CMS自动审查APP内容状态结果：无错误信息！/CMS中展示的表格";//邮件内容 
            
            string title = dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + mailBody;

            //if (dsParm.Tables.Count > 0 && dsParm.Tables[0].Rows.Count > 0)
            //{
            //    msg.Body = msg.Body + "<br/>";
            //    msg.Body = msg.Body + ConverBodyData(title,dsParm);
            //}
            //else
            //{
            //    msg.Body = msg.Body + "无错误信息！";
            //}

            msg.Body = msg.Body + ConverBodyData(title, dsParm);

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

        private static string ConverBodyData(string mailBody, DataSet dsParm)
        {
            StringBuilder sbString = new StringBuilder();
            sbString.Append("<div style='font-size:14;font-family:Microsoft YaHei;font-weight:bold'>");
            sbString.Append(mailBody);

            if (dsParm.Tables.Count > 0 && dsParm.Tables[0].Rows.Count > 0)
            {
                sbString.Append("</div>");
                sbString.Append("<br/>");
                sbString.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:150px' align='center'>城市名称</td><td style='width:100px' align='center'>酒店ID</td><td style='width:300px' align='center'>酒店名称</td><td style='width:400px' align='center'>错误信息</td></tr>");
                foreach (DataRow drRow in dsParm.Tables[0].Rows)
                {
                    sbString.Append("<tr><td>" + drRow["CITYNM"].ToString() + "</td><td>" + drRow["HOTELID"].ToString() + "</td><td>" + SetHotelLink(drRow["HOTELID"].ToString(), drRow["CITYNM"].ToString(), drRow["HOTELNM"].ToString()) + "</td><td>" + drRow["ERRMSG"].ToString() + "</td></tr>");
                }
                sbString.Append("</table>");
            }
            else
            {
                sbString.Append("无错误信息！"); 
                sbString.Append("</div>");
            }

            return sbString.ToString();
        }

        private static string SetHotelLink(string prop, string city, string propnm)
        {
            string strUrl = "<a href='http://cms/WebUI/Hotel/APPContentDetail.aspx?ID={0}&CITY={1}&PLAT=IOS&TYPE=1'>" + propnm + "</a>";
            string cityid = city.Substring((city.IndexOf('[') + 1), (city.IndexOf(']') - 1) - (city.IndexOf('[')));
            strUrl = String.Format(strUrl, prop, cityid);
            return strUrl;
        }

        public static bool Send(string ReceiveNo, string Subject, string Body)
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.To.Add(new MailAddress(ReceiveNo)); //收件人邮箱
                    message.Subject = Subject;//邮件主题
                    message.Body = Body;  //邮件正文

                    SmtpClient mailClient = new SmtpClient();
                    mailClient.Send(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Send Mail:" + ex.Message);
                return false;
            }
        }

    }
}