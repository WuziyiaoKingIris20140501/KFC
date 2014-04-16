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
    public abstract class GGBDTudeBP
    {
        public static void BDTudeConvertSetting()
        {
            string strResult = "";
            string strSQL = XmlSqlAnalyze.GotSqlTextFromXml("GGBDTude", "t_lm_b_hotel_save");
            int iCount = 0;
            int MaxLength = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLength"].ToString())) ? 1000 : int.Parse(ConfigurationManager.AppSettings["MaxLength"].ToString());
            List<CommandInfo> cmdList = new List<CommandInfo>();
            string[] Los = new string[2];
            DataSet dsHotel = GGBDTudeDA.GetHotelList();
            for (int i = 0; i <= dsHotel.Tables[0].Rows.Count; i++)
            {
                // 城市-销售人员名-酒店名前四个字母
                try
                {
                    Los = GGBDCovertHelper.GGBDCovert(dsHotel.Tables[0].Rows[i]["longitude"].ToString(), dsHotel.Tables[0].Rows[i]["latitude"].ToString());
                    CommandInfo cminfo = new CommandInfo();
                    cminfo.CommandText = strSQL;
                    OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar)
                                };
                    parm[0].Value = dsHotel.Tables[0].Rows[i]["prop"].ToString();
                    parm[1].Value = Los[0];
                    parm[2].Value = Los[1];

                    cminfo.Parameters = parm;
                    cmdList.Add(cminfo);
                    iCount = iCount + 1;

                    if (MaxLength == iCount)
                    {
                        try
                        {
                            GGBDTudeDA.SaveHotelTudeCommonList(cmdList);
                            iCount = 0;
                            cmdList.Clear();
                            strResult = "成功";
                        }
                        catch
                        {
                            strResult = "失败";
                        }
                    }
                    //GGBDTudeDA.SaveHotelTude(dsHotel.Tables[0].Rows[i]["prop"].ToString(), GGBDCovert.GGBDCovert(dsHotel.Tables[0].Rows[i]["longitude"].ToString(), dsHotel.Tables[0].Rows[i]["latitude"].ToString()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (iCount > 0)
            {
                try
                {
                    GGBDTudeDA.SaveHotelTudeCommonList(cmdList);
                    strResult = "成功";
                }
                catch
                {
                    strResult = "失败";
                }
            }

            Console.WriteLine(strResult);
        }
    }
}
