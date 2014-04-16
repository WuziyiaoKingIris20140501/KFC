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
    public abstract class AutoMsgUserExcelBP
    {
        //static string _nameSpaceClass = "HotelVp.JobConsole.Biz.AutoMsgCancelOrdBP  Method: ";
        public static void AutoMsgUserExceling()
        {
            DateTime dtBegin = new DateTime();
            dtBegin = System.DateTime.Now;

            Console.WriteLine("用户短信JOB自动运行开始");
            int iCount =  AutoSelect(ConfigurationManager.AppSettings["UploadFilePath2"].ToString());
            //iCount = iCount + AutoSelect(ConfigurationManager.AppSettings["UploadFilePath1"].ToString());

            Console.WriteLine("用户短信JOB自动运行 执行记录数：" + iCount.ToString());
            DateTime dtEnd = new DateTime();
            dtEnd = System.DateTime.Now;

            Console.WriteLine(dtEnd - dtBegin);
        }

        private static int AutoSelect(string fileName)
        {
            DataSet dsResult = new DataSet();
            dsResult = AutoMsgUserExcelDA.AutoListSelect(fileName);
            string Result = string.Empty;
            DateTime startNow = DateTime.Now;
            DateTime endNow = DateTime.Now;
            int iCount = 0;
            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                if (String.IsNullOrEmpty(drRow["USERID"].ToString().Trim()) || String.IsNullOrEmpty(drRow["CONTENT"].ToString().Trim()))
                {
                    continue;
                }
                Result = ApplySendMsgService(drRow);
                iCount++;
                endNow = DateTime.Now;
                if (iCount == 50 && (endNow - startNow).TotalSeconds <= 5)
                {
                    iCount = 0;
                    Thread.Sleep(3000);
                }
            }
            return dsResult.Tables[0].Rows.Count;
        }

         

        private static bool ChkOrderType(string OrderNo, string strOrderType)
        {
            if (String.IsNullOrEmpty(OrderNo) || String.IsNullOrEmpty(strOrderType))
            {
                return true;
            }

            if ("1".Equals(strOrderType))
            {
                if (decimal.Parse(OrderNo) % 2 == 0)
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
                if (decimal.Parse(OrderNo) % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        private static bool ChkAction()
        {
            int startHour = Convert.ToInt32(ConfigurationManager.AppSettings["BeginStartHour"].ToString());
            int endHour = Convert.ToInt32(ConfigurationManager.AppSettings["BeginEndHour"].ToString());
            int startMin = Convert.ToInt32(ConfigurationManager.AppSettings["BeginStartMin"].ToString());
            int endMin = Convert.ToInt32(ConfigurationManager.AppSettings["BeginEndMin"].ToString());

            string StartDTime = DateTime.Now.ToShortDateString() + " " + startHour + ":" + startMin + ":00";
            string EndDTime = DateTime.Now.ToShortDateString() + " " + endHour + ":" + endMin + ":00";
            DateTime dtNow = DateTime.Now;
            if (DateTime.Parse(StartDTime) <= dtNow && dtNow <= DateTime.Parse(EndDTime))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string ApplySendMsgService(DataRow drRow)
        {
            AutoMsgUserExcelEntity _automsgcancelordEntity = new AutoMsgUserExcelEntity();
            _automsgcancelordEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _automsgcancelordEntity.LogMessages.Userid = "JOB System";
            _automsgcancelordEntity.LogMessages.Username = "JOB System";
            _automsgcancelordEntity.AutoMsgCancelOrdDBEntity = new List<AutoMsgCancelOrdDBEntity>();
            AutoMsgCancelOrdDBEntity appcontentDBEntity = new AutoMsgCancelOrdDBEntity();
            appcontentDBEntity.CreateUser = drRow["USERID"].ToString().Trim();
            appcontentDBEntity.Note1 = drRow["CONTENT"].ToString().Trim();
            _automsgcancelordEntity.AutoMsgCancelOrdDBEntity.Add(appcontentDBEntity);
            _automsgcancelordEntity = AutoMsgCancelOrdSA.ApplySendMsgService(_automsgcancelordEntity);

            return _automsgcancelordEntity.ErrorMSG;
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