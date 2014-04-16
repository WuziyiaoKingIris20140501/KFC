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

using HotelVp.Common.DBUtility;
using HotelVp.JobConsole.Entity;
using HotelVp.JobConsole.DataAccess;

namespace HotelVp.JobConsole.Biz
{
    public abstract class PinyingBP
    {
        public static void PinyingSetting()
        {
            DateTime dtBegin = new DateTime();
            dtBegin = System.DateTime.Now;
            DataSet dsHotel = PinyingDA.GetHotelPinyingList();

            if (dsHotel.Tables.Count == 0 || dsHotel.Tables[0].Rows.Count == 0)
            {
                return;
            }

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

            string strResult = "";
            string strContent = "";
            string strTemp = "酒店目的地信息管理--酒店ID：{0} 全拼：{1} 短拼：{2}";
            string strSQL = XmlSqlAnalyze.GotSqlTextFromXml("Pinying", "t_lm_b_pinying_hotel_save");
            Hashtable htList = new Hashtable();
            int iCount = 0;
            int MaxLength = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLength"].ToString())) ? 1000 : int.Parse(ConfigurationManager.AppSettings["MaxLength"].ToString());
            List<CommandInfo> cmdList = new List<CommandInfo>();

            ChineseCode chineseCode = new ChineseCode();
            for (int i = 0; i <= dsHotel.Tables[0].Rows.Count - 1; i++)
            {
                if (String.IsNullOrEmpty(dsHotel.Tables[0].Rows[i]["HOTELID"].ToString()))
                {
                    continue;
                }

                //dsHotel.Tables[0].Rows[i]["LPINYIN"] = HotelVp.Common.Utilities.PinyinHelper.GetPinyin(dsHotel.Tables[0].Rows[i]["PROPNAME"].ToString().Trim());
                //dsHotel.Tables[0].Rows[i]["SPINYIN"] = HotelVp.Common.Utilities.PinyinHelper.GetShortPinyin(dsHotel.Tables[0].Rows[i]["PROPNAME"].ToString().Trim());

                try
                {
                    dsHotel.Tables[0].Rows[i]["LPINYIN"] = String.IsNullOrEmpty(dsHotel.Tables[0].Rows[i]["PROPNAME"].ToString().Trim()) ? "" : chineseCode.GetSpell(dsHotel.Tables[0].Rows[i]["PROPNAME"].ToString().Trim());
                    dsHotel.Tables[0].Rows[i]["SPINYIN"] = String.IsNullOrEmpty(dsHotel.Tables[0].Rows[i]["PROPNAME"].ToString().Trim()) ? "" : chineseCode.IndexCode(dsHotel.Tables[0].Rows[i]["PROPNAME"].ToString().Trim()).ToLower();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                CommandInfo cminfo = new CommandInfo();
                cminfo.CommandText = strSQL;
                OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LPINYIN",OracleType.VarChar),
                                    new OracleParameter("SPINYIN",OracleType.VarChar)
                                };

                lmParm[0].Value = dsHotel.Tables[0].Rows[i]["HOTELID"].ToString();
                lmParm[1].Value = dsHotel.Tables[0].Rows[i]["LPINYIN"].ToString();
                lmParm[2].Value = dsHotel.Tables[0].Rows[i]["SPINYIN"].ToString();
                cminfo.Parameters = lmParm;
                cmdList.Add(cminfo);
                iCount = iCount + 1;
                if (MaxLength == iCount)
                {
                    try
                    {
                        PinyingDA.SavePinyingCommonList(cmdList);
                        strResult = "成功";
                    }
                    catch
                    {
                        strResult = "失败";
                    }

                    foreach (CommandInfo tempinfo in cmdList)
                    {
                        OracleParameter[] tempParm = (OracleParameter[])tempinfo.Parameters;
                        strContent = string.Format(strTemp, tempParm[0].Value.ToString(), tempParm[1].Value.ToString(), tempParm[2].Value.ToString());
                        
                        CommonDBEntity commonDBEntity = new CommonDBEntity();
                        commonDBEntity.Event_Type = "JOB-酒店拼音设置";
                        commonDBEntity.Event_Content = strContent;
                        commonDBEntity.Event_Result = strResult;
                        commonDBEntity.Event_ID = tempParm[0].Value.ToString();
                        commonDBEntity.UserID = "JOB System";
                        commonDBEntity.UserID = "JOB System";

                        _commonEntity.LogMessages.Event_id = tempParm[0].Value.ToString();
                        _commonEntity.CommonDBEntity.Add(commonDBEntity);
                        CommonBP.InsertEventHistory(_commonEntity);
                        _commonEntity.CommonDBEntity.Clear();
                        Console.WriteLine(strContent + "设置结果：" + strResult);
                    }

                    iCount = 0;
                    cmdList.Clear();
                }
            }

            if (iCount > 0)
            {
                try
                {
                    PinyingDA.SavePinyingCommonList(cmdList);
                    strResult = "成功";
                }
                catch
                {
                    strResult = "失败";
                }

                foreach (CommandInfo tempinfo in cmdList)
                {
                    OracleParameter[] tempParm = (OracleParameter[])tempinfo.Parameters;
                    strContent = string.Format(strTemp, tempParm[0].Value.ToString(), tempParm[1].Value.ToString(), tempParm[2].Value.ToString());
                    
                    CommonDBEntity commDBEntity = new CommonDBEntity();
                    commDBEntity.Event_Type = "JOB-酒店拼音设置";
                    commDBEntity.Event_Content = strContent;
                    commDBEntity.Event_Result = strResult;
                    commDBEntity.Event_ID = tempParm[0].Value.ToString();

                    _commonEntity.LogMessages.Event_id = tempParm[0].Value.ToString();
                    _commonEntity.CommonDBEntity.Add(commDBEntity);
                    CommonBP.InsertEventHistory(_commonEntity);
                    _commonEntity.CommonDBEntity.Clear();
                    Console.WriteLine(strContent + "设置结果：" + strResult);
                }
            }

            DateTime dtEnd = new DateTime();
            dtEnd = System.DateTime.Now;

            Console.WriteLine(dtEnd - dtBegin);
        }
    }
}
