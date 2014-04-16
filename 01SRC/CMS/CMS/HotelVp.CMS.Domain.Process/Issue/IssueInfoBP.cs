using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Mail;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.ServiceAdapter;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class IssueInfoBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.IssueInfoBP  Method: ";

        public static IssueInfoEntity GetCommonUserList(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetCommonUserList";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.GetCommonUserList(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetCommonUserList  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static IssueInfoEntity GetIssueHistoryList(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetIssueHistoryList";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.GetIssueHistoryList(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetIssueHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static IssueInfoEntity GetUpdateIssueDetail(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetUpdateIssueDetail";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.GetUpdateIssueDetail(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetUpdateIssueDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static IssueInfoEntity BindIssueList(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "BindIssueList";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.BindIssueList(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "BindIssueList  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static bool CheckIssueLinkGo(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "CheckIssueLinkGo";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.CheckIssueLinkGo(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "CheckIssueLinkGo  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static IssueInfoEntity IssueSave(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "IssueSave";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                if ("0".Equals(issueInfoEntity.IssueInfoDBEntity[0].ActionType))
                {
                    issueInfoEntity = IssueInfoDA.InsertIssueAndHistory(issueInfoEntity);
                }
                else
                {
                    issueInfoEntity = IssueInfoDA.UpdateIssueAndHistory(issueInfoEntity);
                }

                if (issueInfoEntity.Result != 1)
                {
                    return issueInfoEntity;
                }

                IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
                if ("1".Equals(dbParm.ChkMsgAssgin))
                {
                    DataSet dsResult = IssueInfoDA.GetMailToList(issueInfoEntity).QueryResult;
                    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Trim()))
                    {
                        issueInfoEntity.IssueInfoDBEntity[0].MsgAssginRs = IssueInfoSA.SendMsgToIssue(dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Trim(), "1", dbParm.MsgAssginText);

                        if (issueInfoEntity.IssueInfoDBEntity[0].MsgAssginRs.Length > 0)
                        {
                            issueInfoEntity.Result = 9;
                        }
                        else
                        {
                            issueInfoEntity.IssueInfoDBEntity[0].MsgAssginRs = "Issue指派人短信发送成功！";
                        }
                    }
                    else
                    {
                        issueInfoEntity.Result = 9;
                        issueInfoEntity.IssueInfoDBEntity[0].MsgAssginRs = "Issue指派人电话号码不存在，发送短信失败！";
                    }
                }

                if ("1".Equals(dbParm.ChkMsgUser) && "0".Equals(dbParm.RelatedType))
                {
                    DataSet dsResult = IssueInfoDA.GetOrderUserTel(issueInfoEntity);
                    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString().Trim()))
                    {
                        issueInfoEntity.IssueInfoDBEntity[0].MsgUserRs = IssueInfoSA.SendMsgToIssue(dsResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString().Trim(), "0", dbParm.MsgUserText);

                        if (issueInfoEntity.IssueInfoDBEntity[0].MsgUserRs.Length > 0)
                        {
                            issueInfoEntity.Result = 9;
                        }
                        else
                        {
                            issueInfoEntity.IssueInfoDBEntity[0].MsgUserRs = "订单用户短信发送成功！";
                        }
                    }
                    else
                    {
                        issueInfoEntity.Result = 9;
                        issueInfoEntity.IssueInfoDBEntity[0].MsgUserRs = "订单用户电话号码不存在，发送短信失败！";
                    }
                }

                IssueInfoDA.InsertIssueHistory(issueInfoEntity);
                return issueInfoEntity;
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "IssueSave  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static string GetMailToList(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetMailToList";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                DataSet dsResult = IssueInfoDA.GetMailToList(issueInfoEntity).QueryResult;

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["User_Email"].ToString().Trim()))
                {
                    return dsResult.Tables[0].Rows[0]["User_Email"].ToString().Trim();
                }
                else
                {
                    return ConfigurationManager.AppSettings["issueMailTo"].ToString();
                }
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetMailToList  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static IssueInfoEntity SendMail(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "SendMail";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
                string strMailTo = GetMailToList(issueInfoEntity);
                DataSet dsResult = GetUpdateIssueDetail(issueInfoEntity).QueryResult;

                StringBuilder sbString = new StringBuilder();
                sbString.Append("<div style='font-size:16;font-family:Microsoft YaHei;font-weight:bold;'>");
                sbString.Append("Dear " + dsResult.Tables[0].Rows[0]["AssigntoNm"].ToString() + ", <br/><br/>");
                sbString.Append("您已经被指派了一个新的Issue，Issue概要：<br/><br/>");
                sbString.Append("<table style='font-size:14;font-family:Microsoft YaHei;' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td style='width:100px' align='center'>IssueID</td><td style='width:150px' align='center'>Title</td><td style='width:80px' align='center'>优先级</td><td style='width:100px' align='center'>类别</td><td style='width:80px' align='center'>状态</td><td style='width:100px' align='center'>关联Item</td><td style='width:150px' align='center'>关联ID</td><td style='width:80px' align='center'>创建人</td><td style='width:150px' align='center'>创建时间</td><td style='width:120px' align='center'>Assign指派</td></tr>");
                sbString.Append("<tr><td align='center'>" + dsResult.Tables[0].Rows[0]["IssueID"].ToString() + "</td><td align='center'>" + SetIssueLink(dsResult.Tables[0].Rows[0]["IssueID"].ToString(), dsResult.Tables[0].Rows[0]["Title"].ToString()) + "</td><td align='center'>" + dsResult.Tables[0].Rows[0]["Priority"].ToString() + "</td><td align='center'>" + SetIssueTypeNM(dsResult.Tables[0].Rows[0]["Type"].ToString()) + "</td><td align='center'>" + dsResult.Tables[0].Rows[0]["DISStatus"].ToString() + "</td><td align='center'>" + dsResult.Tables[0].Rows[0]["RelatedTypeNM"].ToString() + "</td><td align='center'>" + dsResult.Tables[0].Rows[0]["RelatedID"].ToString() + "</td><td align='center'>" + dsResult.Tables[0].Rows[0]["CreateUser"].ToString() + "</td><td align='center'>" + dsResult.Tables[0].Rows[0]["Create_Time"].ToString() + "</td><td align='center'>" + dsResult.Tables[0].Rows[0]["AssigntoNm"].ToString() + "</td></tr></table><br/><br/>");
                sbString.Append("BR<br/>");
                sbString.Append("Hotelvp CMS<br/>");
                sbString.Append("</div>");
                SendMailProcess(strMailTo, dsResult.Tables[0].Rows[0]["IssueID"].ToString(), sbString.ToString());
                return issueInfoEntity;
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "SendMail  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
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
            string strUrl = "<a href='"+ ConfigurationManager.AppSettings["issueUrl"].ToString() + "'>" + issueTitle + "</a>";
            strUrl = String.Format(strUrl, issueID);
            return strUrl;
        }

        public static bool SendMailProcess(string strMailTo, string strIssueID,string strMailBody)
        {
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
            string mailTo = strMailTo;
            string mailHost = ConfigurationManager.AppSettings["mailHost"].ToString();
            string mailPort = ConfigurationManager.AppSettings["mailPort"].ToString();
            string mailPass = ConfigurationManager.AppSettings["mailPass"].ToString();
            string mailSubject = String.Format(ConfigurationManager.AppSettings["issueMailSubject"].ToString(), strIssueID);
            string mailBody = strMailBody;
            MailMessage msg = new MailMessage();
            msg.To.Add(mailTo);
            msg.From = new MailAddress(mailFrom, mailFrom, System.Text.Encoding.UTF8);

            DateTime dtNow = DateTime.Now;
            msg.Subject = mailSubject;//邮件标题 
            msg.SubjectEncoding = Encoding.UTF8;//Encoding.Default;//邮件标题编码 
            //msg.Body = dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString() + mailBody;//"07/03/2012 CMS自动审查APP内容状态结果：无错误信息！/CMS中展示的表格";//邮件内容 

            //string title = dtNow.ToLongDateString() + mailBody;  //dtNow.AddMinutes(-30).ToString() + "-" + dtNow.ToString() + mailBody;
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

        public static IssueInfoEntity UpdateIssue(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "UpdateIssue";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.UpdateIssueAndHistory(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "UpdateIssue  Error: " + ex.Message;
                throw ex;
            }
        }

        public static IssueInfoEntity ReviewOverTimeOrderList(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "ReviewOverTimeOrderList";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.ReviewOverTimeOrderList(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "ReviewOverTimeOrderList  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static IssueInfoEntity ExportOverTimeOrderList(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "ExportOverTimeOrderList";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.ExportOverTimeOrderList(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "ExportOverTimeOrderList  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static IssueInfoEntity GetRevlTypeVal(IssueInfoEntity issueInfoEntity)
        {
            issueInfoEntity.LogMessages.MsgType = MessageType.INFO;
            issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetRevlTypeVal";
            LoggerHelper.LogWriter(issueInfoEntity.LogMessages);

            try
            {
                return IssueInfoDA.GetRevlTypeVal(issueInfoEntity);
            }
            catch (Exception ex)
            {
                issueInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                issueInfoEntity.LogMessages.Content = _nameSpaceClass + "GetRevlTypeVal  Error: " + ex.Message;
                LoggerHelper.LogWriter(issueInfoEntity.LogMessages);
                throw ex;
            }
        }
    }
}