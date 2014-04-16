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

using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;
using HotelVp.Common.DBUtility;
using HotelVp.JobConsole.Entity;
using HotelVp.JobConsole.DataAccess;

namespace HotelVp.JobConsole.Biz
{
    public abstract class GGBDCovertHelper
    {
        public static string[] GGBDCovert(string strLong, string strLat)
        {
            StringBuilder strBuf = new StringBuilder();
            string[] los = new string[2];
            String from = "2";	//0代表原gps,2代表google
            String to = "4";	//4代表baiDu
            strBuf.Append("from=").Append(from);
            strBuf.Append("&to=").Append(to);
            strBuf.Append("&x=").Append(strLong);
            strBuf.Append("&y=").Append(strLat);
            string strBDUrl = "http://api.map.baidu.com/ag/coord/convert?" + strBuf.ToString();

            string longitude = "";
            string latitude = "";
            string strHotelBDTude = CommonCallWebUrl(strBDUrl);
            if (!String.IsNullOrEmpty(strHotelBDTude))
            {
                JObject oBDTude = JObject.Parse(strHotelBDTude);

                string error = GetJsonStringValue(oBDTude, "error").Trim('"');
                if ("0".Equals(error))
                {
                    longitude = DecodeBase64("utf-8", GetJsonStringValue(oBDTude, "x").Trim('"'));
                    latitude = DecodeBase64("utf-8", GetJsonStringValue(oBDTude, "y").Trim('"'));
                }
            }
            los[0] = longitude;
            los[1] = latitude;
            return los;
        }

        //get JsonStringValue
        public static string GetJsonStringValue(JObject jso, string key)
        {
            try
            {
                return jso[key] == null ? string.Empty : jso[key].ToString();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);  //将2进制编码转换为8位无符号整数数组. 
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);  //将指定字节数组中的一个字节序列解码为一个字符串。 
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        public static string CommonCallWebUrl(string strUrl)
        {
            string strJson = string.Empty;
            try
            {
                strJson = CallWebByURL(strUrl, "");
            }
            catch
            {

            }
            return strJson;
        }

        public static string CallWebByURL(string url, string postDataStr)
        {
            Encoding Encod = System.Text.Encoding.UTF8;
            string rStr = "";
            System.Net.WebRequest req = null;
            System.Net.WebResponse resp = null;
            System.IO.Stream os = null;
            System.IO.StreamReader sr = null;

            try
            {
                //创建连接            
                req = System.Net.WebRequest.Create(url);

                //设置访问方式和发送的请求数据的内容类型
                if (string.IsNullOrEmpty(postDataStr))
                {
                    req.ContentType = "application/json;charset=utf-8";
                    req.Method = "GET";
                }
                else
                {
                    req.ContentType = "application/json;charset=utf-8";
                    req.Method = "POST";
                    if (Encod == null)
                    {
                        Encod = System.Text.Encoding.UTF8;
                    }
                    byte[] bytes = Encod.GetBytes(postDataStr);
                    req.ContentLength = bytes.Length;

                    os = req.GetRequestStream();
                    os.Write(bytes, 0, bytes.Length);
                    os.Close();
                }

                //读取返回结果GetResponseStream          
                resp = req.GetResponse();
                sr = new System.IO.StreamReader(resp.GetResponseStream(), Encod);
                rStr = sr.ReadToEnd();
            }
            catch (Exception ex1)
            {
                string msg = ex1.Message;
                //LogUtil.Warn("HttpUtil.CallWebPage 异常：" + ex1.Message);
            }
            finally
            {
                try
                {
                    //关闭资源
                    if (os != null)
                    {
                        os.Dispose();
                        os.Close();
                    }
                    if (sr != null)
                    {
                        sr.Dispose();
                        sr.Close();
                    }

                    if (resp != null)
                    {
                        resp.Close();
                    }
                    if (req != null) req = null;
                }
                catch (Exception ex2)
                {
                    //LogUtil.Exception("HttpUtil.CallWebPage 关闭连接异常：" + ex2.Message);
                }
            }
            return rStr;
        }
    }
}
