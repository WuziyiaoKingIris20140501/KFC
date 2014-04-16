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

using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;

using HotelVp.Common.DBUtility;
using HotelVp.JobConsole.Entity;
using HotelVp.JobConsole.DataAccess;

namespace HotelVp.JobConsole.Biz
{
    public abstract class PushTicketMsgBP
    {
        public static void PushTicketMsgActioning()
        {
            DateTime dtBegin = new DateTime();
            dtBegin = System.DateTime.Now;
            string content = string.Empty;
            string taskID = string.Empty;
            DataSet dsPushMsg = new DataSet();

            for (int j = 0; j < 2; j++)
            {
                dsPushMsg = PushTicketMsgDA.GetPushTicketMsgList(j);

                if (j == 0)
                {
                    content = GetAppValue("oneContent");
                    taskID = "tic998";
                }
                else
                {
                    content = GetAppValue("twoContent");
                    taskID = "tic999";
                }

                if (dsPushMsg.Tables.Count > 0 && dsPushMsg.Tables[0].Rows.Count > 0)
                {
                    //  Log King
                    //CommonDA.InsertEventHistory("取得Push完成");

                    //  Log King
                    //CommonDA.InsertEventHistory("Que Push开始");
                    // Push Message

                    string strHead = "{\"pushDynamicList\":[";
                    string strFoot = "],\"title\":\"" + "优惠券过期提醒" + "\",\"content\":\"" + content + "\",\"objType\":" + "3" + ",\"objUrl\":\"" + "" + "\",\"pic\":\"\",\"isPopup\":\"\",\"taskId\":\"" + taskID + "\",\"endTime\":\"\"}";

                    try
                    {
                        //PushHelp.PusMsgHelping(dsPushMsg);
                        string queueUrl = ConfigurationManager.AppSettings["MyQueueUrl"];
                        string queueNm = ConfigurationManager.AppSettings["MyQueueNm"];
                        StringBuilder strUserList = new StringBuilder();
                        int iCount = 0;
                        string strResult = "发送成功";
                        string strRegChanel = ConfigurationManager.AppSettings["MyQueueReg"].ToString();
                        string clientCode = string.Empty;

                        //Create the Connection Factory
                        IConnectionFactory factory = new ConnectionFactory(queueUrl);
                        using (IConnection connection = factory.CreateConnection())
                        {
                            //Create the Session  
                            using (ISession session = connection.CreateSession())
                            {
                                IMessageProducer prod = session.CreateProducer(
                                new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(queueNm));
                                ITextMessage msg = prod.CreateTextMessage();
                                msg.NMSDeliveryMode = MsgDeliveryMode.Persistent;

                                for (int i = 0; i < dsPushMsg.Tables[0].Rows.Count; i++)
                                {
                                    //if (!PushMsgDA.CheckPushPlanActionHistory(TaskID, dsPushMsg.Tables[0].Rows[i]["USERID"].ToString().Trim()))
                                    //{
                                    //    continue;
                                    //}

                                    clientCode = dsPushMsg.Tables[0].Rows[i]["CLIENT_CODE"].ToString().Trim();//(!String.IsNullOrEmpty(dsPushMsg.Tables[0].Rows[i]["regchanel_code"].ToString().Trim()) && strRegChanel.Contains(dsPushMsg.Tables[0].Rows[i]["regchanel_code"].ToString().Trim())) ? "HOTELVPPRO" : "HOTELVP";
                                    strUserList.Append("{\"userId\":\"" + dsPushMsg.Tables[0].Rows[i]["USERID"].ToString().Trim() + "\",\"objLinkId\":\"" + dsPushMsg.Tables[0].Rows[i]["ticketusercode"].ToString().Trim() + "\",\"deviceToken\":\"" + dsPushMsg.Tables[0].Rows[i]["DEVICETOKEN"].ToString().Trim() + "\",\"useCode\":\"" + dsPushMsg.Tables[0].Rows[i]["platform_code"].ToString().Trim() + "\",\"useCodeVersion\":\"" + dsPushMsg.Tables[0].Rows[i]["version"].ToString().Trim() + "\",\"clientCode\":\"" + clientCode + "\"},");

                                    iCount++;
                                    //PushMsgDA.InsertPushPlanActionHistory(TaskID, dsPushMsg.Tables[0].Rows[i]["USERID"].ToString().Trim(), dsPushMsg.Tables[0].Rows[i]["DEVICETOKEN"].ToString().Trim(), strResult);
                                    if (iCount == 50)
                                    {
                                        //QueueHelper.SendMessage(strHead + strUserList.ToString().TrimEnd(',') + strFoot);

                                        msg.Text = strHead + strUserList.ToString().TrimEnd(',') + strFoot;
                                        prod.Send(msg);
                                        iCount = 0;
                                        strUserList = new StringBuilder();
                                    }
                                }

                                if (iCount > 0)
                                {
                                    //QueueHelper.SendMessage(strHead + strUserList.ToString() + strFoot);
                                    msg.Text = strHead + strUserList.ToString().TrimEnd(',') + strFoot;
                                    prod.Send(msg);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //  Log King
                        CommonDA.InsertEventHistory("Que Ticket Push发送异常： Messag：" + ex.Message + "InnerException：" + ex.InnerException + "Source：" + ex.Source + "StackTrace：" + ex.StackTrace);
                        return;
                    }
                    //  Log King
                    //CommonDA.InsertEventHistory("Que Push完成");
                }
            }
            //  Log King
            //CommonDA.InsertEventHistory("更新状态 清空数据开始");

            Console.WriteLine("订单记录取得：" + dsPushMsg.Tables[0].Rows.Count.ToString() + "条！");
            //Console.WriteLine("邮件发送开始！");
            //SendMailExpress(dsHotel, ActionType, StartDTime, EndDTime);
            //Console.WriteLine("邮件发送结束！");
            DateTime dtEnd = new DateTime();
            dtEnd = System.DateTime.Now;

            Console.WriteLine(dtEnd - dtBegin);
            Thread.Sleep(5000);
            //Console.WriteLine("");
        }

        public static string GetAppValue(string key)
        {
            return ConfigurationManager.AppSettings[key] == null ? string.Empty : ConfigurationManager.AppSettings[key];
        }
        //private static bool ChkDateTime(string Parm)
        //{
        //    try
        //    {
        //        DateTime.Parse(Parm);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private static bool SendMailExpress(DataSet dsParm, string ActionType, string StartDTime, string EndDTime)
        //{
        //    string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
        //    string mailTo = ConfigurationManager.AppSettings["mailTo"].ToString();
        //    string mailCC = ConfigurationManager.AppSettings["mailCC"].ToString();
        //    string mailHost = ConfigurationManager.AppSettings["mailHost"].ToString();
        //    string mailPort = ConfigurationManager.AppSettings["mailPort"].ToString();
        //    string mailPass = ConfigurationManager.AppSettings["mailPass"].ToString();
        //    string mailSubject = ConfigurationManager.AppSettings["mailSubject"].ToString();
        //    string mailBody = ConfigurationManager.AppSettings["mailBody"].ToString();

        //    MailMessage msg = new MailMessage();
        //    msg.To.Add(mailTo);
        //    if (!String.IsNullOrEmpty(mailCC))
        //    {
        //        msg.CC.Add(mailCC);
        //    }
        //    //可以抄送给多人 

        //    msg.From = new MailAddress(mailFrom, mailFrom, System.Text.Encoding.UTF8);

        //    string dtNow = DateTime.Now.AddDays(-1).ToShortDateString();
        //    //msg.Subject = dtNow + " " + mailSubject;//邮件标题 
        //    msg.SubjectEncoding = Encoding.UTF8;//Encoding.Default;//邮件标题编码 
        //    string title = "";// dtNow + " " + mailBody;
        //    string Subject = "";
        //    if ("0".Equals(ActionType))
        //    {
        //        title = DateTime.Parse(StartDTime).ToShortDateString() + " " + mailBody;
        //        Subject = DateTime.Parse(StartDTime).ToShortDateString() + " " + mailSubject;
        //    }
        //    // 周
        //    else if ("1".Equals(ActionType))
        //    {
        //        title = DateTime.Parse(StartDTime).ToShortDateString() + "--" + DateTime.Parse(EndDTime).AddDays(-1).ToShortDateString() + " 一周" + mailBody;
        //        Subject = DateTime.Parse(StartDTime).ToShortDateString() + "--" + DateTime.Parse(EndDTime).AddDays(-1).ToShortDateString() + " 一周" + mailSubject;
        //    }
        //    // 年
        //    else if ("2".Equals(ActionType))
        //    {
        //        title = DateTime.Parse(StartDTime).Year + "/" + DateTime.Parse(StartDTime).Month + " 月" + mailBody;
        //        Subject = DateTime.Parse(StartDTime).Year + "/" + DateTime.Parse(StartDTime).Month + " 月" + mailSubject;
        //    }

        //    msg.Subject = Subject;//dtNow + " " + mailSubject;//邮件标题 
        //    msg.Body = msg.Body + ConverBodyData(title, dsParm, StartDTime, EndDTime);

        //    msg.BodyEncoding = Encoding.UTF8; //Encoding.Default;//邮件内容编码 
        //    msg.IsBodyHtml = true;//是否是HTML邮件 
        //    //msg.Priority = MailPriority.High;//邮件优先级 

        //    //Attachment attachment = new Attachment("d:\\IT-武必强(King).jpg");//添加附件
        //    //msg.Attachments.Add(attachment);

        //    SmtpClient client = new SmtpClient();
        //    client.Host = mailHost;
        //    client.Port = int.Parse(mailPort);
        //    client.Credentials = new NetworkCredential(mailFrom, mailPass);
        //    //object userState = msg;
        //    try
        //    {
        //        //client.SendAsync(msg, userState);
        //        client.Send(msg);
        //        return true;// 发送成功
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("发送邮件 异常：" + ex.Message);
        //        return false; // 发送失败
        //    }
        //}

        //private static string ConverBodyData(string mailBody, DataSet dsParm, string StartDTime, string EndDTime)
        //{
        //    //ArrayList alCityList = new ArrayList();
        //    StringBuilder sbString = new StringBuilder();
        //    StringBuilder sbTopTenString = new StringBuilder();
        //    StringBuilder sbDashString = new StringBuilder();
        //    sbString.Append("<div style='font-size:16;font-family:Microsoft YaHei;'>");
        //    sbString.Append("<div style='font-weight:bold'>");
        //    sbString.Append(mailBody);
        //    sbString.Append("</div>");

        //    #region 订单汇总列表
        //    if (dsParm.Tables.Count > 3 && dsParm.Tables[3].Rows.Count > 0)
        //    {
        //        decimal decYFOrderIOS = 0;
        //        decimal decYFOrderAND = 0;
        //        decimal decYFOrderWAP = 0;
        //        decimal decYFOrderWP7 = 0;
        //        decimal decYFOrderOther = 0;

        //        decimal decXFOrderIOS = 0;
        //        decimal decXFOrderAND = 0;
        //        decimal decXFOrderWAP = 0;
        //        decimal decXFOrderWP7 = 0;
        //        decimal decXFOrderOther = 0;

        //        decimal decCXFOrderIOS = 0;
        //        decimal decCXFOrderAND = 0;
        //        decimal decCXFOrderWAP = 0;
        //        decimal decCXFOrderWP7 = 0;
        //        decimal decCXFOrderOther = 0;

        //        if (dsParm.Tables[3].Rows.Count > 0)
        //        {
        //            string pricecode = "";
        //            string booksource = "";
        //            string colvalue = "";
        //            foreach (DataRow drRow in dsParm.Tables[3].Rows)
        //            {
        //                pricecode = drRow["PRICECODE"].ToString().Trim().ToLower();
        //                booksource = drRow["BOOKSOURCE"].ToString().Trim().ToLower();
        //                colvalue = drRow["COLVALUE"].ToString().Trim().ToLower();
        //                if ("lmbar".Equals(pricecode))
        //                {
        //                    if ("lm_ios".Equals(booksource) || "ios".Equals(booksource))
        //                    {
        //                        decYFOrderIOS = decYFOrderIOS + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("lm_android".Equals(booksource) || "android".Equals(booksource))
        //                    {
        //                        decYFOrderAND = decYFOrderAND + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("lm_web".Equals(booksource) || "lm_116114web".Equals(booksource))
        //                    {
        //                        decYFOrderWAP = decYFOrderWAP + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("wp".Equals(booksource))
        //                    {
        //                        decYFOrderWP7 = decYFOrderWP7 + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else
        //                    {
        //                        decYFOrderOther = decYFOrderOther + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                }
        //                else if ("lmbar2".Equals(pricecode))
        //                {
        //                    if ("lm_ios".Equals(booksource) || "ios".Equals(booksource))
        //                    {
        //                        decXFOrderIOS = decXFOrderIOS + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("lm_android".Equals(booksource) || "android".Equals(booksource))
        //                    {
        //                        decXFOrderAND = decXFOrderAND + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("lm_web".Equals(booksource) || "lm_116114web".Equals(booksource))
        //                    {
        //                        decXFOrderWAP = decXFOrderWAP + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("wp".Equals(booksource))
        //                    {
        //                        decXFOrderWP7 = decXFOrderWP7 + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else
        //                    {
        //                        decXFOrderOther = decXFOrderOther + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                }
        //                else if ("barb".Equals(pricecode) || "bar".Equals(pricecode))
        //                {
        //                    if ("lm_ios".Equals(booksource) || "ios".Equals(booksource))
        //                    {
        //                        decCXFOrderIOS = decCXFOrderIOS + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("lm_android".Equals(booksource) || "android".Equals(booksource))
        //                    {
        //                        decCXFOrderAND = decCXFOrderAND + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("lm_web".Equals(booksource) || "lm_116114web".Equals(booksource))
        //                    {
        //                        decCXFOrderWAP = decCXFOrderWAP + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else if ("wp".Equals(booksource))
        //                    {
        //                        decCXFOrderWP7 = decCXFOrderWP7 + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                    else
        //                    {
        //                        decCXFOrderOther = decCXFOrderOther + (String.IsNullOrEmpty(colvalue) ? 0 : decimal.Parse(colvalue));
        //                    }
        //                }
        //            }
        //        }

        //        decimal decTicIOS = 0;
        //        decimal decTicAND = 0;
        //        decimal decTicWAP = 0;
        //        decimal decTicWP7 = 0;
        //        decimal decTicOther = 0;

        //        if (dsParm.Tables[4].Rows.Count > 0)
        //        {
        //            string ticbooksource = "";
        //            string ticcolvalue = "";
        //            foreach (DataRow drRow in dsParm.Tables[4].Rows)
        //            {
        //                ticbooksource = drRow["BOOKSOURCE"].ToString().Trim().ToLower();
        //                ticcolvalue = drRow["COLVALUE"].ToString().Trim().ToLower();
        //                if ("lm_ios".Equals(ticbooksource) || "ios".Equals(ticbooksource))
        //                {
        //                    decTicIOS = decTicIOS + (String.IsNullOrEmpty(ticcolvalue) ? 0 : decimal.Parse(ticcolvalue));
        //                }
        //                else if ("lm_android".Equals(ticbooksource) || "android".Equals(ticbooksource))
        //                {
        //                    decTicAND = decTicAND + (String.IsNullOrEmpty(ticcolvalue) ? 0 : decimal.Parse(ticcolvalue));
        //                }
        //                else if ("lm_web".Equals(ticbooksource) || "lm_116114web".Equals(ticbooksource))
        //                {
        //                    decTicWAP = decTicWAP + (String.IsNullOrEmpty(ticcolvalue) ? 0 : decimal.Parse(ticcolvalue));
        //                }
        //                else if ("wp".Equals(ticbooksource))
        //                {
        //                    decTicWP7 = decTicWP7 + (String.IsNullOrEmpty(ticcolvalue) ? 0 : decimal.Parse(ticcolvalue));
        //                }
        //                else
        //                {
        //                    decTicOther = decTicOther + (String.IsNullOrEmpty(ticcolvalue) ? 0 : decimal.Parse(ticcolvalue));
        //                }
        //            }
        //        }

        //        sbDashString.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'>");
        //        sbDashString.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'></td><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>总订单数</td><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>IOS客户端</td><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>Android客户端</td><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>WP7客户端</td><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>WAP</td><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>其他</td></tr>");
        //        sbDashString.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>LM预付</td>" + "<td align='center'>" + (decYFOrderIOS + decYFOrderAND + decYFOrderWAP + decYFOrderWP7 + decYFOrderOther).ToString() + "</td>" + "<td align='center'>" + decYFOrderIOS.ToString() + "</td>" + "<td align='center'>" + decYFOrderAND.ToString() + "</td>" + "<td align='center'>" + decYFOrderWP7.ToString() + "</td>" + "<td align='center'>" + decYFOrderWAP.ToString() + "</td>" + "<td align='center'>" + decYFOrderOther.ToString() + "</td>");
        //        sbDashString.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>LM现付</td>" + "<td align='center'>" + (decXFOrderIOS + decXFOrderAND + decXFOrderWAP + decXFOrderWP7 + decXFOrderOther).ToString() + "</td>" + "<td align='center'>" + decXFOrderIOS.ToString() + "</td>" + "<td align='center'>" + decXFOrderAND.ToString() + "</td>" + "<td align='center'>" + decXFOrderWP7.ToString() + "</td>" + "<td align='center'>" + decXFOrderWAP.ToString() + "</td>" + "<td align='center'>" + decXFOrderOther.ToString() + "</td>");
        //        sbDashString.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>常规现付</td>" + "<td align='center'>" + (decCXFOrderIOS + decCXFOrderAND + decCXFOrderWAP + decCXFOrderWP7 + decCXFOrderOther).ToString() + "</td>" + "<td align='center'>" + decCXFOrderIOS.ToString() + "</td>" + "<td align='center'>" + decCXFOrderAND.ToString() + "</td>" + "<td align='center'>" + decCXFOrderWP7.ToString() + "</td>" + "<td align='center'>" + decCXFOrderWAP.ToString() + "</td>" + "<td align='center'>" + decCXFOrderOther.ToString() + "</td>");
        //        sbDashString.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>总数(券)</td>" + "<td align='center'>" + (decYFOrderIOS + decYFOrderAND + decYFOrderWAP + decYFOrderWP7 + decXFOrderIOS + decYFOrderOther + decXFOrderOther + decCXFOrderOther + decXFOrderAND + decXFOrderWAP + decXFOrderWP7 + decCXFOrderIOS + decCXFOrderAND + decCXFOrderWAP + decCXFOrderWP7).ToString() + ((decTicIOS + decTicAND + decTicWAP + decTicWP7 + decTicOther) > 0 ? "(" + (decTicIOS + decTicAND + decTicWAP + decTicWP7 + decTicOther).ToString() + ")" : "") + "</td>" + "<td align='center'>" + (decYFOrderIOS + decXFOrderIOS + decCXFOrderIOS).ToString() + ((decTicIOS > 0) ? "(" + decTicIOS.ToString() + ")" : "") + "</td>" + "<td align='center'>" + (decYFOrderAND + decXFOrderAND + decCXFOrderAND).ToString() + ((decTicAND > 0) ? ("(" + decTicAND.ToString() + ")") : "") + "</td>" + "<td align='center'>" + (decYFOrderWP7 + decXFOrderWP7 + decCXFOrderWP7).ToString() + ((decTicWP7 > 0) ? ("(" + decTicWP7.ToString() + ")") : "") + "</td>" + "<td align='center'>" + (decYFOrderWAP + decXFOrderWAP + decCXFOrderWAP).ToString() + ((decTicWAP > 0) ? ("(" + decTicWAP.ToString() + ")") : "") + "</td>" + "<td align='center'>" + (decYFOrderOther + decXFOrderOther + decCXFOrderOther).ToString() + ((decTicOther > 0) ? ("(" + decTicOther.ToString() + ")") : "") + "</td>");
        //        sbDashString.Append("</table>");
        //        sbDashString.Append("<br/>");
        //    }
        //    #endregion

        //    #region 城市详细分布
        //    string sPayLm = "<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>LM预付</td>";
        //    string sPayLm2 = "<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>LM现付</td>";
        //    string sPayBar = "<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>常规现付</td>";
        //    string sTotalSum = "<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;width:80px'>总数</td>";

        //    if (dsParm.Tables.Count > 2 && dsParm.Tables[2].Rows.Count > 0)
        //    {
        //        StringBuilder sbCity = new StringBuilder();
        //        StringBuilder sbPayLm = new StringBuilder();
        //        StringBuilder sbPayLm2 = new StringBuilder();
        //        StringBuilder sbTotalSum = new StringBuilder();

        //        StringBuilder sbPayBar = new StringBuilder();

        //        sbCity.Append("<tr style='background-color:#f6f6f6;font-weight:bold;width:80px'><td></td><td align='center' style='width:80px'>总和</td>");

        //        //sbPayLm.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>LM预付</td>");
        //        //sbPayLm2.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>LM现付</td>");
        //        //sbPayBar.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>常规现付</td>");
        //        //sbTotalSum.Append("<tr><td align='center' style='background-color:#f6f6f6;font-weight:bold;'>总数</td>");

        //        string strCity = "";
        //        string strLmBar = "";
        //        string strLmBar2 = "";

        //        string strBar = "";
        //        string strBarB = "";

        //        decimal decLm = 0;
        //        decimal decLm2 = 0;

        //        decimal decBar = 0;
        //        decimal decBarB = 0;

        //        decimal decTotalLm = 0;
        //        decimal decTotalLm2 = 0;

        //        decimal decTotalBar = 0;

        //        foreach (DataRow drRow in dsParm.Tables[2].Rows)
        //        {
        //            //if (!alCityList.Contains(drRow["CITYID"].ToString()))
        //            //{
        //            //    alCityList.Add(drRow["CITYID"].ToString());
        //            //}

        //            if (!strCity.Equals(drRow["CITYID"].ToString()))
        //            {
        //                strCity = drRow["CITYID"].ToString();
        //                sbCity.Append("<td style='width:150px' align='center'>");
        //                sbCity.Append(drRow["CITYNM"].ToString());
        //                sbCity.Append("</td>");

        //                DataRow[] drList = dsParm.Tables[2].Select("CITYID='" + strCity + "'");
        //                strLmBar = "";
        //                strLmBar2 = "";
        //                strBar = "";
        //                strBarB = "";

        //                foreach (DataRow drTemp in drList)
        //                {
        //                    if ("LMBAR".Equals(drTemp["PRICE_CODE"].ToString()))
        //                    {
        //                        strLmBar = drTemp["ORDERSUM"].ToString();
        //                    }
        //                    if ("LMBAR2".Equals(drTemp["PRICE_CODE"].ToString()))
        //                    {
        //                        strLmBar2 = drTemp["ORDERSUM"].ToString();
        //                    }

        //                    if ("BAR".Equals(drTemp["PRICE_CODE"].ToString()))
        //                    {
        //                        strBar = drTemp["ORDERSUM"].ToString();
        //                    }

        //                    if ("BARB".Equals(drTemp["PRICE_CODE"].ToString()))
        //                    {
        //                        strBarB = drTemp["ORDERSUM"].ToString();
        //                    }
        //                }

        //                decLm = (String.IsNullOrEmpty(strLmBar)) ? 0 : decimal.Parse(strLmBar);
        //                decLm2 = (String.IsNullOrEmpty(strLmBar2)) ? 0 : decimal.Parse(strLmBar2);

        //                decBar = (String.IsNullOrEmpty(strBar)) ? 0 : decimal.Parse(strBar);
        //                decBarB = (String.IsNullOrEmpty(strBarB)) ? 0 : decimal.Parse(strBarB);

        //                decTotalLm = decTotalLm + decLm;
        //                decTotalLm2 = decTotalLm2 + decLm2;

        //                decTotalBar = decTotalBar + decBar + decBarB;

        //                strLmBar = String.IsNullOrEmpty(strLmBar) ? "<td align='center'>0</td>" : "<td align='center'>" + strLmBar + "</td>";
        //                strLmBar2 = String.IsNullOrEmpty(strLmBar2) ? "<td align='center'>0</td>" : "<td align='center'>" + strLmBar2 + "</td>";
        //                strBar = String.IsNullOrEmpty(strBar) ? "<td align='center'>0</td>" : "<td align='center'>" + (decBar + decBarB).ToString() + "</td>";

        //                sbPayLm.Append(strLmBar);
        //                sbPayLm2.Append(strLmBar2);

        //                sbPayBar.Append(strBar);

        //                sbTotalSum.Append("<td align='center'>"+ (decLm+decLm2+decBar + decBarB).ToString()+ "</td>");
        //                strLmBar = "";
        //                strLmBar2 = "";
        //                strBar = "";
        //            }
        //        }

        //        //sbString.Append("<br/>");
        //        //sbString.Append("总订单数：");
        //        //sbString.Append((decTotalLm + decTotalLm2 + decTotalBar).ToString());
        //        //sbString.Append("<br/>");
        //        //sbString.Append("LM预付订单总数：");
        //        //sbString.Append(decTotalLm.ToString());
        //        //sbString.Append("<br/>");
        //        //sbString.Append("LM现付订单总数：");
        //        //sbString.Append(decTotalLm2.ToString());
        //        //sbString.Append("<br/>");
        //        //sbString.Append("常规现付订单总数：");
        //        //sbString.Append(decTotalBar.ToString());
        //        //sbString.Append("<br/>");

        //        sbString.Append("</div>");
        //        sbString.Append("<br/>");

        //        sbString.Append(sbDashString.ToString());
        //        sbString.Append("<br/>");
        //        sbString.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'>");
        //        sbCity.Append("</tr>");
        //        sbPayLm.Append("</tr>");
        //        sbPayLm2.Append("</tr>");
        //        sbTotalSum.Append("</tr>");

        //        sbString.Append(sbCity.ToString());
        //        sbString.Append(sPayLm + "<td align='center'>" + decTotalLm.ToString() + "</td>" + sbPayLm.ToString());
        //        sbString.Append(sPayLm2 + "<td align='center'>" + decTotalLm2.ToString() + "</td>" + sbPayLm2.ToString());
        //        sbString.Append(sPayBar + "<td align='center'>" + decTotalBar.ToString() + "</td>" + sbPayBar.ToString());
        //        sbString.Append(sTotalSum + "<td align='center'>" + (decTotalLm + decTotalLm2 + decTotalBar).ToString() + "</td>" + sbTotalSum.ToString());
        //        sbString.Append("</table>");
        //    }
        //    #endregion

        //    #region 酒店排行列表 Top10
        //    StringBuilder sbTopString = new StringBuilder();
        //    StringBuilder sbTopCityString = new StringBuilder();
        //    StringBuilder sbTopHotelTile = new StringBuilder();
        //    DataSet dsHotelTop = new DataSet();

        //    DataSet dsHotelTopSumList = PushMsgDA.GetHotelTopSumList(StartDTime, EndDTime);
        //    int icount = 1;
        //    string strCityID = string.Empty;
        //    StringBuilder sbTop1String = new StringBuilder();
        //    StringBuilder sbTop2String = new StringBuilder();
        //    StringBuilder sbTop3String = new StringBuilder();
        //    StringBuilder sbTop4String = new StringBuilder();
        //    StringBuilder sbTop5String = new StringBuilder();
        //    StringBuilder sbTop6String = new StringBuilder();
        //    StringBuilder sbTop7String = new StringBuilder();
        //    StringBuilder sbTop8String = new StringBuilder();
        //    StringBuilder sbTop9String = new StringBuilder();
        //    StringBuilder sbTop10String = new StringBuilder();
        //    sbTopString.Append("<table style='font-size:14;font-family:Microsoft YaHei;width:100%;table-layout:fixed;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td rowspan='2' align='center' style='width:30px;'>排行</td>");

        //    foreach (DataRow drSum in dsHotelTopSumList.Tables[0].Rows)
        //    {
        //        strCityID = drSum["CITYID"].ToString();
        //        dsHotelTop = PushMsgDA.GetHotelTopList(StartDTime, EndDTime, strCityID);
        //        if (dsHotelTop.Tables.Count > 0 && dsHotelTop.Tables[0].Rows.Count > 0)
        //        {
        //            #region Top10排名
        //            sbTopCityString.Append("<td colspan='4' align='center' style='width:300px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[0]["CITYNM"].ToString() + "</td>");
        //            sbTopHotelTile.Append("<td colspan='2' align='center'style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout:fixed;'><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>酒店ID-酒店名称</td></tr><tr><td align='center' style='word-break: break-all; word-wrap:break-word;'>商圈</td><td align='center' style='word-break: break-all; word-wrap:break-word;'>星级</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>成功单</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>总单</td></tr></table></td>");

        //            if (dsHotelTop.Tables[0].Rows.Count > 0)
        //            {
        //                sbTop1String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[0]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[0]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[0]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[0]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[0]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[0]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[0]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop1String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 1)
        //            {
        //                sbTop2String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[1]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[1]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[1]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[1]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[1]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[1]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[1]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop2String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 2)
        //            {
        //                sbTop3String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[2]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[2]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[2]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[2]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[2]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[2]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[2]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop3String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 3)
        //            {
        //                sbTop4String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[3]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[3]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[3]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[3]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[3]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[3]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[3]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop4String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 4)
        //            {
        //                sbTop5String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[4]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[4]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[4]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[4]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[4]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[4]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[4]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop5String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 5)
        //            {
        //                sbTop6String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[5]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[5]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[5]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[5]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[5]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[5]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[5]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop6String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 6)
        //            {
        //                sbTop7String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[6]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[6]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[6]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[6]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[6]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[6]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[6]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop7String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 7)
        //            {
        //                sbTop8String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[7]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[7]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[7]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[7]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[7]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[7]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[7]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop8String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 8)
        //            {
        //                sbTop9String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[8]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[8]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[8]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[8]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[8]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[8]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[8]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop9String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }

        //            if (dsHotelTop.Tables[0].Rows.Count > 9)
        //            {
        //                sbTop10String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[9]["HOTELID"].ToString().Trim() + "-" + dsHotelTop.Tables[0].Rows[9]["HOTELNM"].ToString().Trim() + "</td></tr><tr><td align='center' style='width:50%'>" + dsHotelTop.Tables[0].Rows[9]["TRADENM"].ToString().Trim() + "</td><td align='center' style='width:50%'>" + SetStarValue(dsHotelTop.Tables[0].Rows[9]["DIAHOTEL"].ToString().Trim(), dsHotelTop.Tables[0].Rows[9]["STAR"].ToString().Trim()) + "</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[9]["OrderSum"].ToString().Trim() + "</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>" + dsHotelTop.Tables[0].Rows[9]["OrderALL"].ToString().Trim() + "</td></tr></table></td>");
        //            }
        //            else
        //            {
        //                sbTop10String.Append("<td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'><table style='font-size:14;font-family:Microsoft YaHei;width:200px;table-layout: fixed;' ><tr><td colspan='2' align='center' style='width:200px;word-break: break-all; word-wrap:break-word;'></td></tr><tr><td align='center'></td><td align='center'>&nbsp;</td></tr></table></td><td colspan='2' align='center'style='width:100px'><table style='font-size:14;font-family:Microsoft YaHei;width:100px;table-layout:fixed;'><tr><td align='center' style='width:60px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td><td align='center' style='width:40px;word-break: break-all; word-wrap:break-word;'>&nbsp;</td></tr></table></td>");
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            continue;
        //        }

        //        icount = icount + 1;
        //        if (icount == 4)
        //        {
        //            //if (sbTopString.ToString().Length == 0)
        //            //{
        //            //    sbString.Append("<br/>");
        //            //    sbString.Append("无酒店订单TOP10排行信息！");
        //            //    sbString.Append("</div>");
        //            //}
        //            //else
        //            if (sbTopCityString.ToString().Length > 0)
        //            {
        //                //sbString.Append("<br/>");
        //                //sbString.Append("<br/>");
        //                //sbString.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold'>");
        //                //sbString.Append("酒店订单TOP10排行列表:");
        //                //sbString.Append("</div>");
        //                //sbString.Append("<br/>");

        //                sbTopTenString.Append(sbTopString.ToString());
        //                sbTopTenString.Append(sbTopCityString.ToString());
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr style='background-color:#f6f6f6;font-weight:bold;'>");
        //                sbTopTenString.Append(sbTopHotelTile.ToString());
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>1</td>");
        //                sbTopTenString.Append(sbTop1String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>2</td>");
        //                sbTopTenString.Append(sbTop2String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>3</td>");
        //                sbTopTenString.Append(sbTop3String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>4</td>");
        //                sbTopTenString.Append(sbTop4String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>5</td>");
        //                sbTopTenString.Append(sbTop5String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>6</td>");
        //                sbTopTenString.Append(sbTop6String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>7</td>");
        //                sbTopTenString.Append(sbTop7String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>8</td>");
        //                sbTopTenString.Append(sbTop8String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>9</td>");
        //                sbTopTenString.Append(sbTop9String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("<tr>");
        //                sbTopTenString.Append("<td align='center'>10</td>");
        //                sbTopTenString.Append(sbTop10String);
        //                sbTopTenString.Append("</tr>");
        //                sbTopTenString.Append("</table>");
        //                sbTopTenString.Append("<br/>");
        //            }
        //            icount = 1;
        //            sbTopCityString.Clear();
        //            sbTopHotelTile.Clear();
        //            sbTop1String.Clear();
        //            sbTop2String.Clear();
        //            sbTop3String.Clear();
        //            sbTop4String.Clear();
        //            sbTop5String.Clear();
        //            sbTop6String.Clear();
        //            sbTop7String.Clear();
        //            sbTop8String.Clear();
        //            sbTop9String.Clear();
        //            sbTop10String.Clear();
        //        }
        //    }

        //    if (sbTopCityString.ToString().Length > 0)
        //    {
        //        //sbString.Append("<br/>");
        //        //sbString.Append("<br/>");
        //        //sbString.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold'>");
        //        //sbString.Append("酒店订单TOP10排行列表:");
        //        //sbString.Append("</div>");
        //        //sbString.Append("<br/>");

        //        sbTopTenString.Append(sbTopString.ToString());
        //        sbTopTenString.Append(sbTopCityString.ToString());
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr style='background-color:#f6f6f6;font-weight:bold;'>");
        //        sbTopTenString.Append(sbTopHotelTile.ToString());
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>1</td>");
        //        sbTopTenString.Append(sbTop1String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>2</td>");
        //        sbTopTenString.Append(sbTop2String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>3</td>");
        //        sbTopTenString.Append(sbTop3String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>4</td>");
        //        sbTopTenString.Append(sbTop4String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>5</td>");
        //        sbTopTenString.Append(sbTop5String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>6</td>");
        //        sbTopTenString.Append(sbTop6String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>7</td>");
        //        sbTopTenString.Append(sbTop7String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>8</td>");
        //        sbTopTenString.Append(sbTop8String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>9</td>");
        //        sbTopTenString.Append(sbTop9String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("<tr>");
        //        sbTopTenString.Append("<td align='center'>10</td>");
        //        sbTopTenString.Append(sbTop10String);
        //        sbTopTenString.Append("</tr>");
        //        sbTopTenString.Append("</table>");
        //        sbTopTenString.Append("<br/>");
        //    }

        //    if (sbTopTenString.ToString().Length == 0)
        //    {
        //        sbString.Append("<br/>");
        //        sbString.Append("无酒店订单TOP10排行信息！");
        //        sbString.Append("</div>");
        //    }
        //    else
        //    {
        //        sbString.Append("<br/>");
        //        sbString.Append("<br/>");
        //        sbString.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold'>");
        //        sbString.Append("酒店订单TOP10排行列表:");
        //        sbString.Append("</div>");
        //        sbString.Append("<br/>");
        //        sbString.Append(sbTopTenString.ToString());
        //    }
        //    #endregion

        //    #region 酒店订单汇总
        //    if (dsParm.Tables.Count > 0 && dsParm.Tables[0].Rows.Count > 0)
        //    {
        //        sbString.Append("</div>");
        //        sbString.Append("<br/>");
        //        sbString.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold'>");
        //        sbString.Append("酒店订单汇总列表:");
        //        sbString.Append("</div>");
        //        sbString.Append("<br/>");
        //        sbString.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:150px' align='center'>城市名称</td><td style='width:150px' align='center'>商圈名称</td><td style='width:100px' align='center'>酒店ID</td><td style='width:200px' align='center'>酒店名称</td><td style='width:120px' align='center'>星级</td><td style='width:120px' align='center'>房型名称</td><td style='width:100px' align='center'>房型单价</td><td style='width:100px' align='center'>订单数</td><td style='width:100px' align='center'>间夜数</td><td style='width:100px' align='center'>订单类型</td><td style='width:100px' align='center'>销售人员</td></tr>");
        //        foreach (DataRow drRow in dsParm.Tables[0].Rows)
        //        {
        //            sbString.Append("<tr><td>" + drRow["CITYNM"].ToString() + "[" + drRow["CITYID"].ToString() + "]" + "</td><td>" + drRow["TRADENM"].ToString() + "</td><td>" + drRow["HOTELID"].ToString() + "</td><td>" + drRow["HOTELNM"].ToString() + "</td><td>" + SetStarValue(drRow["DIAHOTEL"].ToString(), drRow["STAR"].ToString()) + "</td><td>" + drRow["ROOMNM"].ToString() + "</td><td>" + drRow["PriceSum"].ToString() + "</td><td>" + drRow["OrderSum"].ToString() + "</td><td>" + drRow["BRoomSum"].ToString() + "</td><td>" + drRow["PRICENM"].ToString() + "</td><td>" + SetSalesManager(drRow["HOTELID"].ToString()) + "</td></tr>");
        //        }
        //        sbString.Append("</table>");
        //        sbString.Append("<br/><br/>");
        //    }
        //    else
        //    {
        //        sbString.Append("无LM酒店产量信息！");
        //        sbString.Append("</div>");
        //    }
        //    #endregion

        //    #region 具体订单列表
        //    //if (dsParm.Tables.Count > 1 && dsParm.Tables[1].Rows.Count > 0)
        //    //{
        //    //    sbString.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold'>");
        //    //    sbString.Append("具体订单列表:");
        //    //    sbString.Append("</div>");
        //    //    sbString.Append("<br/>");
        //    //    sbString.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:100px' align='center'>订单号</td><td style='width:150px' align='center'>城市名称</td><td style='width:150px' align='center'>商圈名称</td><td style='width:100px' align='center'>酒店ID</td><td style='width:300px' align='center'>酒店名称</td><td style='width:120px' align='center'>星级</td><td style='width:120px' align='center'>房型名称</td><td style='width:100px' align='center'>房型单价</td><td style='width:100px' align='center'>入住日期</td><td style='width:100px' align='center'>入住间夜</td><td style='width:100px' align='center'>订单类型</td></tr>");
        //    //    foreach (DataRow drRow in dsParm.Tables[1].Rows)
        //    //    {
        //    //        sbString.Append("<tr><td>" + drRow["ORDERID"].ToString() + "</td><td>" + drRow["CITYNM"].ToString() + "[" + drRow["CITYID"].ToString() + "]" + "</td><td>" + drRow["TRADENM"].ToString() + "</td><td>" + drRow["HOTELID"].ToString() + "</td><td>" + drRow["HOTELNM"].ToString() + "</td><td>" + SetStarValue(drRow["DIAHOTEL"].ToString(), drRow["STAR"].ToString()) + "</td><td>" + drRow["ROOMNM"].ToString() + "</td><td>" + drRow["PriceSum"].ToString() + "</td><td>" + drRow["INDATE"].ToString() + "</td><td>" + drRow["BRoomSum"].ToString() + "</td><td>" + drRow["PRICENM"].ToString() + "</td></tr>");
        //    //    }
        //    //    sbString.Append("</table>");
        //    //    sbString.Append("<br/><br/>");
        //    //}
        //    //else
        //    //{
        //    //    sbString.Append("无LM酒店产量信息！");
        //    //    sbString.Append("</div>");
        //    //}
        //    #endregion

        //    return sbString.ToString();
        //}

        //private static string SetStarValue(string DiaHotel, string Star)
        //{
        //    string val = string.Empty;
        //    if ("1".Equals(DiaHotel))
        //    {
        //        val = "准";
        //    }

        //    if (!chkNum(Star) || (chkNum(Star) && int.Parse(Star) < 3))
        //    {
        //        val = "经济";
        //    }
        //    else
        //    {
        //        val = val + Star;
        //    }

        //    return val;
        //}

        //private static bool chkNum(string parm)
        //{
        //    try
        //    {
        //        int.Parse(parm);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private static string SetSalesManager(string HotelID)
        //{
        //    DataSet dsSales = PushMsgDA.GetSalesManagerList(HotelID);

        //    if (dsSales.Tables.Count > 0 && dsSales.Tables[0].Rows.Count > 0)
        //    {
        //        return dsSales.Tables[0].Rows[0][0].ToString();
        //    }

        //    return "";
        //}
        //// <summary>
        //// 将DataGridView控件中数据导出到Excel
        //// </summary>
        //// <param name="gridView">DataGridView对象</param>
        //// <param name="isShowExcle">是否显示Excel界面</param>
        //// <returns></returns>
        ////public static bool ExportDataGridview(DataGridView gridView, bool isShowExcle, string path)
        ////{
        ////    bool result = false;
        ////    Microsoft.Office.Interop.Excel.Application excel = null;
        ////    Microsoft.Office.Interop.Excel.Workbook myWorkBook = null;
        ////    try
        ////    {
        ////        if (gridView.Rows.Count == 0)
        ////            return false;
        ////        建立Excel对象
        ////        excel = new Microsoft.Office.Interop.Excel.Application();
        ////        myWorkBook = excel.Application.Workbooks.Add(true);
        ////        excel.Visible = isShowExcle;

        ////        生成字段名称
        ////        for (int i = 0; i < gridView.ColumnCount; i++)
        ////            excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
        ////        填充数据
        ////        for (int i = 0; i < gridView.Rows.Count - 1; i++)
        ////        {
        ////            for (int j = 0; j < gridView.Columns.Count; j++)
        ////            {
        ////                if (gridView[j, i].ValueType == typeof(string))
        ////                    excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
        ////                else
        ////                    excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
        ////            }
        ////        }
        ////        object missing = System.Reflection.Missing.Value;
        ////        excel.ActiveWorkbook.Saved = true;
        ////        excel.ActiveWorkbook.SaveAs(path,
        ////            Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8,
        ////            missing, missing, false,
        ////            false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        ////            missing, missing, missing, missing, missing);
        ////        myWorkBook.Close(missing, missing, missing);
        ////        myWorkBook = null;
        ////        excel.Quit();
        ////        result = true;
        ////    }
        ////    catch (System.Exception ex)
        ////    {
        ////        excel.Quit();
        ////        XException xe = FuncLib.BuildXExcpetion("将DataGridView控件中数据导出到Excel出错", ex);
        ////        xe.AddEnvironmentData("path", path, true);
        ////        xe.WriteLogRecord(false, true, DateTime.Now.ToString("yyyy-MM-dd"));
        ////    }
        ////    return result;
        ////}
    }
}
