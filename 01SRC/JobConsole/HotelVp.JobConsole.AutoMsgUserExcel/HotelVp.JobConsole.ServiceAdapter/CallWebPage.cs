using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using HotelVp.Common.Json;
using HotelVp.Common.Json.Serialization;
using System.Net;
using System.Collections;

namespace HotelVp.JobConsole.ServiceAdapter
{
/// <summary>
///CallWebPage 的摘要说明
/// </summary>
    public class CallWebPage
    {
        public CallWebPage()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 访问URL地址
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="postDataStr">参数</param>
        /// <param name="Encod">编码方式</param>
        /// <returns></returns>
        public string CallWebByURL(string url, string postDataStr)
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