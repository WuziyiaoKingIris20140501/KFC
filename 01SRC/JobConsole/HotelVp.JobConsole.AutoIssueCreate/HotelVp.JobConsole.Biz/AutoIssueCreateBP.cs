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
    public abstract class AutoIssueCreateBP
    {
        //static string _nameSpaceClass = "HotelVp.JobConsole.Biz.AutoIssueCreating  Method: ";
        public static void AutoIssueCreating(string ActionType)
        {
            DateTime dtBegin = new DateTime();
            dtBegin = System.DateTime.Now;
            AutoIssueCreateEntity _autoissuecreateEntity = new AutoIssueCreateEntity();
            CommonEntity _commonEntity = new CommonEntity();
            _autoissuecreateEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _autoissuecreateEntity.LogMessages.Userid = "JOB System";
            _autoissuecreateEntity.LogMessages.Username = "JOB System";
            _autoissuecreateEntity.AutoMsgCancelOrdDBEntity = new List<AutoMsgCancelOrdDBEntity>();
            AutoMsgCancelOrdDBEntity appcontentDBEntity = new AutoMsgCancelOrdDBEntity();
            _autoissuecreateEntity.AutoMsgCancelOrdDBEntity.Add(appcontentDBEntity);
            Console.WriteLine("问题订单自动创建Issue单JOB自动运行开始");
            //所有CC取消状态为：满房/变价的自动判定为“酒店问题”Issue单
            //所有LMBAR&LMBAR2的订单处理时长超过30分钟自动判定为“订单问题”Issue单

            int iCount = 0;
            if ("1".Equals(ActionType))
            {
                AutoTodaySelect();
            }
            else if ("2".Equals(ActionType))
            {
                iCount = AutoSelectHotel(_autoissuecreateEntity);
            }
            else
            {
                iCount = AutoSelect(_autoissuecreateEntity);
            }
            Console.WriteLine("问题订单自动创建Issue单JOB自动运行 执行记录数：" + iCount.ToString());
            DateTime dtEnd = new DateTime();
            dtEnd = System.DateTime.Now;

            Console.WriteLine(dtEnd - dtBegin);
        }

        private static void AutoTodaySelect()
        {
            StringBuilder sbString = new StringBuilder();
            DataSet dsResult = new DataSet();
            dsResult = AutoIssueCreateDA.AutoTodaySelect();
            StringBuilder sbResult = new StringBuilder();
            int iCount = 0;
            string strTempUser = string.Empty;
            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                if (!String.IsNullOrEmpty(strTempUser) && !strTempUser.Equals(drRow["Assignto"].ToString().Trim()))
                {
                    sbResult.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold;'>");
                    sbResult.Append("今日共有未close的issue " + iCount.ToString() + "条，具体信息如下：<br/>");
                    sbResult.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:100px' align='center'>IssueID</td><td style='width:150px' align='center'>Title</td><td style='width:80px' align='center'>优先级</td><td style='width:100px' align='center'>类别</td><td style='width:80px' align='center'>状态</td><td style='width:100px' align='center'>关联Item</td><td style='width:150px' align='center'>关联ID</td><td style='width:80px' align='center'>创建人</td><td style='width:150px' align='center'>创建时间</td><td style='width:120px' align='center'>Assign指派</td></tr>" + sbString.ToString().Trim() + "</table><br/><br/>");
                    sbResult.Append("BR<br/>");
                    sbResult.Append("Hotelvp CMS<br/>");
                    sbResult.Append("</div>");
                    SendMailExpress(sbResult.ToString(), "1", DateTime.Now.ToShortDateString() + " 未完成Issue汇总报告", strTempUser);
                    sbString = new StringBuilder();
                    sbResult = new StringBuilder();
                    iCount = 0;
                }
                strTempUser = drRow["Assignto"].ToString().Trim();
                sbString.Append("<tr><td align='center'>" + drRow["IssueID"].ToString() + "</td><td align='center'>" + SetIssueLink(drRow["IssueID"].ToString(), drRow["Title"].ToString()) + "</td><td align='center'>" + drRow["Priority"].ToString() + "</td><td align='center'>" + SetIssueTypeNM(drRow["Type"].ToString()) + "</td><td align='center'>" + drRow["DISStatus"].ToString() + "</td><td align='center'>" + drRow["RelatedTypeNM"].ToString() + "</td><td align='center'>" + drRow["RelatedID"].ToString() + "</td><td align='center'>" + drRow["CreateUser"].ToString() + "</td><td align='center'>" + drRow["Create_Time"].ToString() + "</td><td align='center'>" + drRow["AssigntoNm"].ToString() + "</td></tr>");
                iCount = iCount + 1;
            }

            if (iCount > 0)
            {
                sbResult.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold;'>");
                sbResult.Append("今日共有未close的issue " + iCount.ToString() + "条，具体信息如下：<br/>");
                sbResult.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:100px' align='center'>IssueID</td><td style='width:150px' align='center'>Title</td><td style='width:80px' align='center'>优先级</td><td style='width:100px' align='center'>类别</td><td style='width:80px' align='center'>状态</td><td style='width:100px' align='center'>关联Item</td><td style='width:150px' align='center'>关联ID</td><td style='width:80px' align='center'>创建人</td><td style='width:150px' align='center'>创建时间</td><td style='width:120px' align='center'>Assign指派</td></tr>" + sbString.ToString().Trim() + "</table><br/><br/>");
                sbResult.Append("BR<br/>");
                sbResult.Append("Hotelvp CMS<br/>");
                sbResult.Append("</div>");
                SendMailExpress(sbResult.ToString(), "1", DateTime.Now.ToShortDateString() + " 未完成Issue汇总报告", strTempUser);
            }
        }
        private static string SetIssueTypeNM(string param)
        {
            string strResult = string.Empty;
            string[] strTypeList = param.Split(',');

            foreach (string strTemp in strTypeList)
            {
                switch (strTemp)
                {
                    case "0":
                        strResult = strResult + "酒店问题" + ",";
                        break;
                    case "1":
                        strResult = strResult + "订单状态问题" + ",";
                        break;
                    case "2":
                        strResult = strResult + " 订单支付问题" + ",";
                        break;
                    case "3":
                        strResult = strResult + "订单返现问题" + ",";
                        break;
                    case "4":
                        strResult = strResult + "优惠券问题" + ",";
                        break;
                    case "5":
                        strResult = strResult + "发票问题" + ",";
                        break;
                    case "6":
                        strResult = strResult + " 用户问题" + ",";
                        break;
                    case "7":
                        strResult = strResult + "提现问题" + ",";
                        break;
                    default:
                        strResult = "未知状态";
                        break;
                }
            }

            return strResult.Trim(',');
        }

        private static string SetIssueLink(string issueID, string issueTitle)
        {
            string strUrl = "<a href='" + ConfigurationManager.AppSettings["issueUrl"].ToString() + "'>" + issueTitle + "</a>";
            strUrl = String.Format(strUrl, issueID);
            return strUrl;
        }

        private static int AutoSelectHotel(AutoIssueCreateEntity autogotelplanEntity)
        {
            StringBuilder sbString = new StringBuilder();
            DataSet dsResult = AutoIssueCreateDA.AutoListSelectHotel(autogotelplanEntity);
            StringBuilder sbResult = new StringBuilder();
            string strIssueID = string.Empty;
            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                strIssueID = AutoIssueCreateDA.InsertIssueData(drRow);
                if (!String.IsNullOrEmpty(strIssueID))
                {
                    drRow["IssueID"] = strIssueID;
                    drRow["Title"] = String.Format(drRow["Title"].ToString(), drRow["IssueID"].ToString());
                    AutoIssueCreateDA.UpdateIssueData(drRow);
                    sbString.Append("<tr><td align='center'>" + drRow["IssueID"].ToString() + "</td><td align='center'>" + SetIssueLink(drRow["IssueID"].ToString(), drRow["Title"].ToString()) + "</td><td align='center'>" + drRow["Priority"].ToString() + "</td><td align='center'>" + SetIssueTypeNM(drRow["IssueType"].ToString()) + "</td><td align='center'>" + drRow["DISStatus"].ToString() + "</td><td align='center'>" + drRow["RelatedTypeNM"].ToString() + "</td><td align='center'>" + drRow["RelatedID"].ToString() + "</td><td align='center'>" + drRow["CreateUser"].ToString() + "</td><td align='center'>" + DateTime.Now.ToString() + "</td><td align='center'>" + drRow["AssigntoNm"].ToString() + "</td></tr>");

                    sbResult.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold;'>");
                    //sbResult.Append("Dear " + dsResult.Tables[0].Rows[0]["AssigntoNm"].ToString() + ", <br/><br/>");
                    sbResult.Append("Dear All,<br/><br/>");
                    sbResult.Append("您已经被指派了一个新的Issue，Issue概要：<br/><br/>");
                    sbResult.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:100px' align='center'>IssueID</td><td style='width:150px' align='center'>Title</td><td style='width:80px' align='center'>优先级</td><td style='width:100px' align='center'>类别</td><td style='width:80px' align='center'>状态</td><td style='width:100px' align='center'>关联Item</td><td style='width:150px' align='center'>关联ID</td><td style='width:80px' align='center'>创建人</td><td style='width:150px' align='center'>创建时间</td><td style='width:120px' align='center'>Assign指派</td></tr>" + sbString.ToString().Trim() + "</table><br/><br/>");
                    sbResult.Append("BR<br/>");
                    sbResult.Append("Hotelvp CMS<br/>");
                    sbResult.Append("</div>");

                    SendMailExpress(sbResult.ToString(), "0", "[IssueID:" + drRow["IssueID"].ToString() + "] Newly Assigned Issue Notification", "");

                    sbString = new StringBuilder();
                    sbResult = new StringBuilder();
                }
            }
            return dsResult.Tables[0].Rows.Count;
        }

        private static int AutoSelect(AutoIssueCreateEntity autogotelplanEntity)
        {
            StringBuilder sbString = new StringBuilder();
            DataSet dsResult = AutoIssueCreateDA.AutoListSelect(autogotelplanEntity);
            StringBuilder sbResult = new StringBuilder();
            string strIssueID = string.Empty;
            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                strIssueID = AutoIssueCreateDA.InsertIssueData(drRow);
                if (!String.IsNullOrEmpty(strIssueID))
                {
                    drRow["IssueID"] = strIssueID;
                    drRow["Title"] = String.Format(drRow["Title"].ToString(), drRow["IssueID"].ToString());
                    AutoIssueCreateDA.UpdateIssueData(drRow);
                    sbString.Append("<tr><td align='center'>" + drRow["IssueID"].ToString() + "</td><td align='center'>" + SetIssueLink(drRow["IssueID"].ToString(), drRow["Title"].ToString()) + "</td><td align='center'>" + drRow["Priority"].ToString() + "</td><td align='center'>" + SetIssueTypeNM(drRow["IssueType"].ToString()) + "</td><td align='center'>" + drRow["DISStatus"].ToString() + "</td><td align='center'>" + drRow["RelatedTypeNM"].ToString() + "</td><td align='center'>" + drRow["RelatedID"].ToString() + "</td><td align='center'>" + drRow["CreateUser"].ToString() + "</td><td align='center'>" + DateTime.Now.ToString() + "</td><td align='center'>" + drRow["AssigntoNm"].ToString() + "</td></tr>");

                    sbResult.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold;'>");
                    //sbResult.Append("Dear " + dsResult.Tables[0].Rows[0]["AssigntoNm"].ToString() + ", <br/><br/>");
                    sbResult.Append("Dear All, <br/><br/>");
                    sbResult.Append("您已经被指派了一个新的Issue，Issue概要：<br/><br/>");
                    sbResult.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:100px' align='center'>IssueID</td><td style='width:150px' align='center'>Title</td><td style='width:80px' align='center'>优先级</td><td style='width:100px' align='center'>类别</td><td style='width:80px' align='center'>状态</td><td style='width:100px' align='center'>关联Item</td><td style='width:150px' align='center'>关联ID</td><td style='width:80px' align='center'>创建人</td><td style='width:150px' align='center'>创建时间</td><td style='width:120px' align='center'>Assign指派</td></tr>" + sbString.ToString().Trim() + "</table><br/><br/>");
                    sbResult.Append("BR<br/>");
                    sbResult.Append("Hotelvp CMS<br/>");
                    sbResult.Append("</div>");

                    SendMailExpress(sbResult.ToString(), "0", "[IssueID:" + drRow["IssueID"].ToString() + "] Newly Assigned Issue Notification", "");

                    sbString = new StringBuilder();
                    sbResult = new StringBuilder();
                }
            }
            return dsResult.Tables[0].Rows.Count;
        }

        private static string GetMailTo(string strUserID)
        {
            DataSet dsResult = AutoIssueCreateDA.GetMailToList(strUserID);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["User_Email"].ToString().Trim()))
            {
                return dsResult.Tables[0].Rows[0]["User_Email"].ToString().Trim();
            }
            else
            {
                return "";
            }
        }

        private static bool SendMailExpress(string strMailBody, string strType, string strMailSubject, string strMailTo)
        {
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
            string mailTo = GetMailTo(strMailTo);//ConfigurationManager.AppSettings["mailTo"].ToString();
            string mailSTo = ConfigurationManager.AppSettings["mailSTo"].ToString();
            string mailCC = ConfigurationManager.AppSettings["mailCC"].ToString();
            string mailHost = ConfigurationManager.AppSettings["mailHost"].ToString();
            string mailPort = ConfigurationManager.AppSettings["mailPort"].ToString();
            string mailPass = ConfigurationManager.AppSettings["mailPass"].ToString();
            string mailSubject = strMailSubject;//ConfigurationManager.AppSettings["mailSubject"].ToString();
            string mailBody = ConfigurationManager.AppSettings["mailBody"].ToString();
            
            MailMessage msg = new MailMessage();
            if (!String.IsNullOrEmpty(mailTo))
            {
                msg.To.Add(mailTo);
            }
            else
            {
                msg.To.Add(mailSTo);
            }

            if (!String.IsNullOrEmpty(mailCC))
            {
                msg.CC.Add(mailCC);
            }
            //可以抄送给多人 

            msg.From = new MailAddress(mailFrom, mailFrom, System.Text.Encoding.UTF8);

            DateTime dtNow = DateTime.Now;
            msg.Subject = mailSubject;
            msg.SubjectEncoding = Encoding.UTF8;//Encoding.Default;//邮件标题编码 
            //msg.Body = dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + mailBody;//"07/03/2012 CMS自动审查APP内容状态结果：无错误信息！/CMS中展示的表格";//邮件内容 

            string title = ("0".Equals(strType)) ? dtNow.AddMinutes(-30).ToString() + "-" + dtNow.ToString() + mailBody : dtNow.AddDays(-1).ToShortDateString() + " 04:00:00" + "-" + dtNow.ToShortDateString() + " 04:00:00 汇总" + mailBody;  //dtNow.AddMinutes(-30).ToString() + "-" + dtNow.ToString() + mailBody;
            msg.Body = strMailBody;//msg.Body + ConverBodyData(title, strMailBody);

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

        private static string ConverBodyData(string mailBody, string strMailBody)
        {
            StringBuilder sbString = new StringBuilder();
            sbString.Append("<div style='font-size:14;font-family:Microsoft YaHei;font-weight:bold'>");
            sbString.Append(mailBody);

            if (!String.IsNullOrEmpty(strMailBody))
            {
                sbString.Append("</div>");
                sbString.Append("<br/>");
                //sbString.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:150px' align='center'>城市名称</td><td style='width:100px' align='center'>酒店ID</td><td style='width:300px' align='center'>酒店名称</td><td style='width:400px' align='center'>错误信息</td></tr>");
                //foreach (DataRow drRow in dsParm.Tables[0].Rows)
                //{
                //    sbString.Append("<tr><td>" + drRow["CITYNM"].ToString() + "</td><td>" + drRow["HOTELID"].ToString() + "</td><td>" +drRow["HOTELID"].ToString() + "</td><td>" + drRow["ERRMSG"].ToString() + "</td></tr>");
                //}

                sbString.Append(strMailBody);
                sbString.Append("</table>");
            }
            else
            {
                sbString.Append("无未处理信息！"); 
                sbString.Append("</div>");
            }

            return sbString.ToString();
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