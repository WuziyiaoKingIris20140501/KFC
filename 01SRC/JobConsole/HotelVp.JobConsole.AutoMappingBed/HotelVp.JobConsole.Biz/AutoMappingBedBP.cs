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
using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;


namespace HotelVp.JobConsole.Biz
{
    public abstract class AutoMappingBedBP
    {
        //static string _nameSpaceClass = "HotelVp.JobConsole.Biz.AutoOrderSynchronizing  Method: ";

        public static void AutoMappingBeding()
        {
            DateTime dtBegin = new DateTime();
            dtBegin = System.DateTime.Now;

            AutoMappingBedEntity _hotelcomparisonEntity = new AutoMappingBedEntity();
            CommonEntity _commonEntity = new CommonEntity();
            _hotelcomparisonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _hotelcomparisonEntity.LogMessages.Userid = "JOB System";
            _hotelcomparisonEntity.LogMessages.Username = "JOB System";
            _hotelcomparisonEntity.AutoHotelComparisonDBEntity = new List<AutoHotelComparisonDBEntity>();
            AutoHotelComparisonDBEntity hotelcomparisondbentity = new AutoHotelComparisonDBEntity();
            _hotelcomparisonEntity.AutoHotelComparisonDBEntity.Add(hotelcomparisondbentity);

            Console.WriteLine("ELMappingRoomBedJOB自动运行开始");

            int iCount = AutoMappingBeding(_hotelcomparisonEntity);

            Console.WriteLine("ELMappingRoomBedJOB自动运行 执行记录数：" + iCount.ToString());
            DateTime dtEnd = new DateTime();
            dtEnd = System.DateTime.Now;

            Console.WriteLine(dtEnd - dtBegin);
        }

        private static int AutoMappingBeding(AutoMappingBedEntity hotelcomparisonEntity)
        {
            CommonEntity _commonEntity = new CommonEntity();
            _commonEntity.LogMessages = new Common.Logger.LogMessage();

            IPHostEntry hostInfo = Dns.GetHostByName(Dns.GetHostName());
            IPAddress[] address = hostInfo.AddressList;
            string m_IP = address[0].ToString();

            _commonEntity.LogMessages.IpAddress = m_IP;
            _commonEntity.LogMessages.Computername = Dns.GetHostName();
            _commonEntity.LogMessages.Userid = "JOB System";
            _commonEntity.LogMessages.Username = "JOB System";
            _commonEntity.CommonDBEntity = new List<CommonDBEntity>();

            DataSet dsResult = AutoMappingBedDA.AutoHotelRoomMappingList(hotelcomparisonEntity);

            DataTable dtMapping = new DataTable();
            dtMapping.Columns.Add("SOURCE");
            dtMapping.Columns.Add("HOTELID");
            dtMapping.Columns.Add("ROOMCD");
            dtMapping.Columns.Add("BED");
            string ELBed = string.Empty;
            string strSQL = XmlSqlAnalyze.GotSqlTextFromXml("AutoMappingBed", "SaveHotelRoomBed");
            int iCount = 0;
            int MaxLength = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLength"].ToString())) ? 1000 : int.Parse(ConfigurationManager.AppSettings["MaxLength"].ToString());
            List<CommandInfo> cmdList = new List<CommandInfo>();

            DataRow[] drTemp;
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                drTemp = dtMapping.Select("SOURCE='" + dsResult.Tables[0].Rows[i]["MPType"].ToString().Trim() + "' and HOTELID='" + dsResult.Tables[0].Rows[i]["Mapping_Hotel"].ToString().Trim() + "'" + " and ROOMCD = '" + dsResult.Tables[0].Rows[i]["Mapping_Room"].ToString().Trim() + "'");
                if (drTemp.Count() <= 0)
                {
                    Console.WriteLine(dsResult.Tables[0].Rows[i]["Mapping_Hotel"].ToString().Trim() + " - " + dsResult.Tables[0].Rows[i]["Mapping_Room"].ToString().Trim() + " 艺龙接口调用开始");
                    GetMappingPrice(dsResult.Tables[0].Rows[i]["MPType"].ToString().Trim(), dsResult.Tables[0].Rows[i]["Mapping_Hotel"].ToString().Trim(), dsResult.Tables[0].Rows[i]["Mapping_Room"].ToString().Trim(), ref dtMapping);
                    Console.WriteLine(dsResult.Tables[0].Rows[i]["Mapping_Hotel"].ToString().Trim() + " - " + dsResult.Tables[0].Rows[i]["Mapping_Room"].ToString().Trim() + " 艺龙接口调用结束");
                    Console.WriteLine(dtMapping.Rows.Count);

                    drTemp = dtMapping.Select("SOURCE='" + dsResult.Tables[0].Rows[i]["MPType"].ToString().Trim() + "' and HOTELID='" + dsResult.Tables[0].Rows[i]["Mapping_Hotel"].ToString().Trim() + "'" + " and ROOMCD = '" + dsResult.Tables[0].Rows[i]["Mapping_Room"].ToString().Trim() + "'");
                    if (drTemp.Count() <= 0)
                    {
                        Console.WriteLine("无");
                        continue;
                    }
                    else
                    {
                        ELBed = drTemp[0]["BED"].ToString();
                    }
                }
                else
                {
                    Console.WriteLine(dtMapping.Rows.Count);
                    ELBed = drTemp[0]["BED"].ToString();
                }

                if (String.IsNullOrEmpty(ELBed))
                {
                    continue;
                }

                dsResult.Tables[0].Rows[i]["ROOMBED"] = ELBed;

                CommandInfo cminfo = new CommandInfo();
                cminfo.CommandText = strSQL;
                OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                   new OracleParameter("ROOMCODE",OracleType.VarChar),
                                   new OracleParameter("ROOMAREA",OracleType.VarChar),
                                   new OracleParameter("UPDATEUSER",OracleType.VarChar)
                                };
                lmParm[0].Value = dsResult.Tables[0].Rows[i]["Hotel_ID"].ToString().Trim();
                lmParm[1].Value = dsResult.Tables[0].Rows[i]["Room_Code"].ToString().Trim();
                lmParm[2].Value = dsResult.Tables[0].Rows[i]["ROOMBED"].ToString().Trim();
                lmParm[3].Value = "CMS JOB";

                cminfo.Parameters = lmParm;
                cmdList.Add(cminfo);
                iCount = iCount + 1;

                if (MaxLength == iCount)
                {
                    try
                    {
                        AutoMappingBedDA.SaveHotelRoomBedList(cmdList);
                        Console.Write("成功");
                        iCount = 0;
                        cmdList.Clear();
                    }
                    catch
                    {
                        Console.Write("失败");
                    }
                }


                //AutoMappingBedDA.SaveHotelRoomBed(dsResult.Tables[0].Rows[i]);
            }

            if (iCount > 0)
            {
                try
                {
                    AutoMappingBedDA.SaveHotelRoomBedList(cmdList);
                    Console.Write("成功");
                    iCount = 0;
                    cmdList.Clear();
                }
                catch
                {
                    Console.Write("失败");
                }
            }
            //AutoMappingBedDA.UpdateOverDateData();

            //if ("1".Equals(hotelcomparisonEntity.AutoHotelComparisonDBEntity[0].SaveType))
            //{
            //    DataSet dsMailData = AutoMappingBedDA.GetMailDataList();

            //    string strMailBody = "";
            //    string strMailSubject = "今日{0}家酒店价格过高 " + DateTime.Now.ToShortDateString();

            //    if (dsMailData.Tables.Count > 0 && dsMailData.Tables["Master"].Rows.Count > 0)
            //    {
            //        strMailSubject = String.Format(strMailSubject, dsMailData.Tables["Master"].Rows[0]["BHLID"].ToString());
            //    }
            //    else
            //    {
            //        strMailSubject = String.Format(strMailSubject, "0");
            //    }

            //    strMailBody = SetMailBodyData(dsMailData);

            //    SendMailExpress(strMailBody, strMailSubject);
            //}

            return dsResult.Tables[0].Rows.Count;
        }

        public static string SetMailBodyData(DataSet dsData)
        {
            StringBuilder strMailBody = new StringBuilder();
            string strTitle = DateTime.Now.ToShortDateString() + "，共比较{0}个酒店，{1}个房型，未比较{2}个酒店（{3}个房型）。";
            decimal decALHL = 0;
            decimal decALRM = 0;

            decimal decBJHL = 0;
            decimal decBJRM = 0;

            if (dsData.Tables.Count > 1 && dsData.Tables["Detail"].Rows.Count > 0)
            {
                decALHL = decimal.Parse(dsData.Tables["Detail"].Rows[0]["Hotel_ID"].ToString());
                decALRM = decimal.Parse(dsData.Tables["Detail"].Rows[0]["Room_Code"].ToString());
            }

            if (dsData.Tables.Count > 0 && dsData.Tables["Master"].Rows.Count > 0)
            {
                decBJHL = decimal.Parse(dsData.Tables["Master"].Rows[0]["ALHLID"].ToString());
                decBJRM = decimal.Parse(dsData.Tables["Master"].Rows[0]["ALRMCD"].ToString());
            }

            strTitle = String.Format(strTitle, decBJHL.ToString(), decBJRM.ToString(), (decALHL - decBJHL).ToString(), (decALRM - decBJRM).ToString());

            strMailBody.Append("<div style='font-size:16;font-family:Microsoft YaHei;'>");
            strMailBody.Append("<div style='font-weight:bold'>");
            strMailBody.Append(strTitle);
            strMailBody.Append("</div>");
            strMailBody.Append("<br/>");

            strMailBody.Append("<table style='font-size:14;font-family:Microsoft YaHei;width:70%' border='1'><tr style='background-color:#E9E9E9;font-weight:bold;'><td style='width:10%'></td><td align='center' style='width:30%'>HVP价格高于签约价</td><td align='center' style='width:30%'>HVP价格低于签约价</td><td align='center' style='width:30%'>HVP价格等于签约价</td></tr><tr style='background-color:#E9E9E9;'><td align='center' style='font-weight:bold;'>酒店数</td>");

            if (dsData.Tables.Count > 0 && dsData.Tables["Master"].Rows.Count > 0)
            {
                strMailBody.Append("<td align='center'>" + dsData.Tables["Master"].Rows[0]["BHLID"].ToString() + "</td><td align='center'>" + dsData.Tables["Master"].Rows[0]["LHLID"].ToString() + "</td><td align='center'>" + dsData.Tables["Master"].Rows[0]["DHLID"].ToString() + "</td></tr>");
                strMailBody.Append("<tr style='background-color:#E9E9E9;'><td align='center' style='font-weight:bold;'>房间数</td><td align='center'>" + dsData.Tables["Master"].Rows[0]["BRMCD"].ToString() + "</td><td align='center'>" + dsData.Tables["Master"].Rows[0]["LRMCD"].ToString() + "</td><td align='center'>" + dsData.Tables["Master"].Rows[0]["DRMCD"].ToString() + "</td>");
            }
            else
            {
                strMailBody.Append("<td align='center'>0</td><td align='center'>0</td><td align='center'>0</td></tr>");
                strMailBody.Append("<tr style='background-color:#E9E9E9;'><td align='center' style='font-weight:bold;'>房间数</td><td align='center'>0</td><td align='center'>0</td><td align='center'>0</td>");
            }

            strMailBody.Append("</tr></table>");
            strMailBody.Append("<br/>");


            strMailBody.Append("<table style='font-size:14;font-family:Microsoft YaHei;width:100%' border='1'><tr style='background-color:#f6f6f6;font-weight:bold;'><td align='center' style='width:10%'>酒店ID</td><td align='center' style='width:20%'>酒店名称</td><td align='center' style='width:10%'>房型名称</td><td align='center' style='width:10%'>折扣方式</td><td align='center' style='width:10%'>折扣</td><td align='center' style='width:10%'>今日LM价格</td><td align='center' style='width:10%'>今日艺龙价格</td><td align='center' style='width:10%'>实际比较价格</td><td align='center' style='width:10%'>酒店销售</td></tr>");

            if (dsData.Tables.Count > 2 && dsData.Tables["Total"].Rows.Count > 0)
            {
                for (int i = 0; i < dsData.Tables["Total"].Rows.Count; i++ )
                {
                    strMailBody.Append("<tr>");
                    strMailBody.Append("<td align='center'>" + dsData.Tables["Total"].Rows[i]["Hotel_ID"].ToString() + "</td>");
                    strMailBody.Append("<td align='center'>" + dsData.Tables["Total"].Rows[i]["Hotel_Name"].ToString() + "</td>");
                    strMailBody.Append("<td align='center'>" + dsData.Tables["Total"].Rows[i]["Room_Name"].ToString() + "</td>");
                    strMailBody.Append("<td align='center'>" + (("1".Equals(dsData.Tables["Total"].Rows[i]["DType"].ToString())) ? "固定折扣" : "固定价格") + "</td>");
                    strMailBody.Append("<td align='center'>" + dsData.Tables["Total"].Rows[i]["DValue"].ToString() + "</td>");
                    strMailBody.Append("<td align='center'>" + dsData.Tables["Total"].Rows[i]["Two_Price"].ToString() + "</td>");
                    strMailBody.Append("<td align='center'>" + dsData.Tables["Total"].Rows[i]["Mapping_Price"].ToString() + "</td>");
                    strMailBody.Append("<td align='center'>" + dsData.Tables["Total"].Rows[i]["Act_Price"].ToString() + "</td>");
                    strMailBody.Append("<td align='center'>" + dsData.Tables["Total"].Rows[i]["User_DspName"].ToString() + "</td>");
                    strMailBody.Append("</tr>");
                }
            }
            strMailBody.Append("</table>");

            strMailBody.Append("</div>");
            return strMailBody.ToString();
        }

        public static void GetMappingPrice(string MPType, string Mapping_Hotel, string Mapping_Room, ref DataTable dtMapping)
        {
            if ("ELONG".Equals(MPType))
            {
                string HotelsByIDUrl = JsonRequestURLBuilder.queryHotelsBySUPIDV2("EL" + Mapping_Hotel, DateTime.Now.AddDays(1).ToShortDateString().Replace("/", "-"), DateTime.Now.AddDays(2).ToShortDateString().Replace("/", "-"));
                string strHotelMain = CommonCallWebUrl(HotelsByIDUrl);

                if (strHotelMain.Length > 0)
                {
                    JObject oHotelMain = JObject.Parse(strHotelMain);
                    if (!"200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelMain, "code").Trim('"')))
                    {
                        HotelsByIDUrl = JsonRequestURLBuilder.queryHotelsBySUPIDV2("EL" + Mapping_Hotel, DateTime.Now.AddDays(7).ToShortDateString().Replace("/", "-"), DateTime.Now.AddDays(8).ToShortDateString().Replace("/", "-"));
                        strHotelMain = CommonCallWebUrl(HotelsByIDUrl);
                        oHotelMain = JObject.Parse(strHotelMain);
                        if (!"200".Equals(JsonRequestURLBuilder.GetJsonStringValue(oHotelMain, "code").Trim('"')))
                        {
                            return;
                        }
                    }

                    string oRoomTypeCode = oHotelMain.SelectToken("result").SelectToken("roomPlans").ToString();
                    int index0 = oRoomTypeCode.IndexOf("[");
                    string strRoomCD = string.Empty;
                    if (index0 == 0)
                    {
                        JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                        for (int j = 0; j < jsaRoomType.Count; j++)
                        {
                            JObject jsoRoomType = (JObject)jsaRoomType[j];
                            DataRow drRoom = dtMapping.NewRow();
                            drRoom["SOURCE"] = MPType;
                            drRoom["HOTELID"] = Mapping_Hotel; 
                            strRoomCD =  JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"');
                            strRoomCD = (strRoomCD.Contains("-")) ? strRoomCD.Split('-')[0].ToString().Trim() : strRoomCD;
                            drRoom["ROOMCD"] = strRoomCD;
                            drRoom["BED"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomarea").Trim('"').Replace("M²", "平米");
                            dtMapping.Rows.Add(drRoom);
                        }
                    }
                    else
                    {
                        JArray jsaRoomType = (JArray)JsonConvert.DeserializeObject(oRoomTypeCode);
                        for (int j = 0; j < jsaRoomType.Count; j++)
                        {
                            JObject jsoRoomType = (JObject)jsaRoomType[j];
                            DataRow drRoom = dtMapping.NewRow();
                            drRoom["SOURCE"] = MPType;
                            drRoom["HOTELID"] = Mapping_Hotel;
                            strRoomCD = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomcode").Trim('"');
                            strRoomCD = (strRoomCD.Contains("-")) ? strRoomCD.Split('-')[0].ToString().Trim() : strRoomCD;
                            drRoom["ROOMCD"] = strRoomCD;
                            drRoom["BED"] = JsonRequestURLBuilder.GetJsonStringValue(jsoRoomType, "roomarea").Trim('"').Replace("M²", "平米");
                            dtMapping.Rows.Add(drRoom);
                        }
                    }
                }
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

        public static string GetActPrice(string DType, string DValue, string Mapping_Price)
        {
            string result = "";
            if ("1".Equals(DType))
            {
                if (!String.IsNullOrEmpty(Mapping_Price) && IsVildNum(Mapping_Price) && IsVildNum(DValue))
                {
                    result = Math.Round(decimal.Parse(Mapping_Price) * decimal.Parse(DValue), 2).ToString();
                }
            }
            else if ("2".Equals(DType))
            {
                result = DValue;
            }

            return result;
        }

        public static bool IsVildNum(string DValue)
        {
            try
            {
                decimal.Parse(DValue);
                return true;
            }
            catch {
                return false;
            }
        }

        private static string GetMailTo(string strUserID)
        {
            DataSet dsResult = AutoMappingBedDA.GetMailToList(strUserID);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["User_Email"].ToString().Trim()))
            {
                return dsResult.Tables[0].Rows[0]["User_Email"].ToString().Trim();
            }
            else
            {
                return "";
            }
        }

        private static bool SendMailExpress(string strMailBody,string strMailSubject)
        {
            string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
            string mailTo = ConfigurationManager.AppSettings["mailTo"].ToString();
            string mailSTo = ConfigurationManager.AppSettings["mailSTo"].ToString();
            string mailCC = ConfigurationManager.AppSettings["mailCC"].ToString();
            string mailHost = ConfigurationManager.AppSettings["mailHost"].ToString();
            string mailPort = ConfigurationManager.AppSettings["mailPort"].ToString();
            string mailPass = ConfigurationManager.AppSettings["mailPass"].ToString();
            string mailSubject = strMailSubject;//ConfigurationManager.AppSettings["mailSubject"].ToString();
            //string mailBody = ConfigurationManager.AppSettings["mailBody"].ToString();
            
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