using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Web.Security;

namespace HotelVp.EBOK.Domain.API
{

    internal class Request
    {
        internal static bool Flag = false;

        //internal static void Log(string type, string path, string url, string body)
        //{
        //    try
        //    {
        //        if (!System.IO.File.Exists(path))
        //        {
        //            FileStream fs = System.IO.File.Create(path);
        //        }
        //        using (StreamWriter sw = System.IO.File.AppendText(path))
        //        {
        //            sw.WriteLine("---------------------------------------------------------");
        //            // Arbitrary objects can also be written to the file.
        //            sw.Write("API Request " + type + ":" + url);
        //            sw.WriteLine(DateTime.Now);
        //            if (!string.IsNullOrEmpty(body))
        //                sw.WriteLine("body:" + body);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        //internal static string Get(string clientCode, string url, string args, string apiVersion)
        //{
        //    HttpWebRequest request = null;
        //    try
        //    {
        //        if (Flag == false)
        //        {
        //            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
        //            Flag = true;
        //        }
        //        if (string.IsNullOrEmpty(clientCode))
        //            clientCode = "HOTELVP";
        //        var rqUrl = url;
        //        rqUrl += string.Format("?useCode={0}&useCodeVersion={1}&clientCode={2}&apiVersion={3}", Setting.UseCode, Setting.UseCodeVersion, clientCode, apiVersion);
        //        rqUrl += string.IsNullOrEmpty(args) ? "" : "&" + args;
        //        request = (HttpWebRequest)WebRequest.Create(rqUrl);
        //        request.Method = "GET";
        //        request.Timeout = 60000;
        //        request.ContentType = "application/json";
        //        //request.Headers.Add("Accept-Encoding", "gzip");
        //        //request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
        //        //request.AutomaticDecompression = DecompressionMethods.GZip;
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            return response.GetResponseStream().ToString();
        //            //return Unzip(response.GetResponseStream());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //    finally
        //    {
        //        if (request != null)
        //            request.Abort();
        //    }
        //}

        internal static string Post(string url, string body, string args = "")
        {
            //if (Flag == false)
            //{
            //    System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            //    Flag = true;
            //}
            Stream dataStream = null;
            HttpWebRequest request = null;
            try
            {
                var key = Setting.SignKey;
                var signKey = FormsAuthentication.HashPasswordForStoringInConfigFile(body + key, "MD5");
                var rqUrl = string.Format("{0}?useCode={1}&useCodeVersion={2}&clientCode={3}&apiVersion={4}&signKey={5}", url, Setting.UseCode, Setting.UseCodeVersion, Setting.ClientCode,Setting.ApiVersion, signKey);
                if (!string.IsNullOrEmpty(args))
                    rqUrl += "&" + args;

                request = (HttpWebRequest)WebRequest.Create(rqUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                //request.Headers.Add("Accept-Encoding", "gzip");
                request.Timeout = 60000;
                byte[] byteArray = Encoding.UTF8.GetBytes(body); ;
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.GetResponseStream().ToString();
                    //return Unzip(response.GetResponseStream());
                }

            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                if (dataStream != null)
                    dataStream.Close();
                if (request != null)
                    request.Abort();
            }
        }


        internal static string CallWebByPost(string url, string body, string args = "")
        {
            //if (Flag == false)
            //{
            //    System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            //    Flag = true;
            //}
            Encoding Encod = System.Text.Encoding.UTF8;
            string rStr = "";
            System.Net.WebRequest req = null;
            System.Net.WebResponse resp = null;
            System.IO.Stream os = null;
            System.IO.StreamReader sr = null;

            try
            {
                var key = Setting.SignKey;
                var signKey = FormsAuthentication.HashPasswordForStoringInConfigFile(body + key, "MD5");
                var rqUrl = string.Format("{0}?useCode={1}&useCodeVersion={2}&clientCode={3}&apiVersion={4}&signKey={5}", url, Setting.UseCode, Setting.UseCodeVersion, Setting.ClientCode, Setting.ApiVersion, signKey);
                if (!string.IsNullOrEmpty(args))
                    rqUrl += "&" + args;

                //创建连接            
                req = System.Net.WebRequest.Create(rqUrl);

                req.ContentType = "application/json;charset=utf-8";
                req.Method = "POST";
                if (Encod == null)
                {
                    Encod = System.Text.Encoding.UTF8;
                }
                byte[] bytes = Encod.GetBytes(body);
                req.ContentLength = bytes.Length;

                os = req.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);
                os.Close();

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

        ///// <summary>
        ///// GZip 解压
        ///// </summary>
        ///// <param name="stream"></param>
        ///// <returns></returns>
        //internal static string Unzip(Stream stream)
        //{
        //    StringBuilder r = new StringBuilder();
        //    using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress, true))
        //    {
        //        byte[] d = new byte[40960];
        //        int i = gZipStream.Read(d, 0, 40960);
        //        while (i > 0)
        //        {
        //            r.Append(Encoding.UTF8.GetString(d, 0, i));
        //            i = gZipStream.Read(d, 0, 40960);
        //        }
        //    }
        //    return r.ToString();
        //}
    }
}
