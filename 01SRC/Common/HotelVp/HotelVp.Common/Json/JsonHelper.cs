#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Text;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;

namespace HotelVp.Common.Json
{
  /// <summary>
  /// 
  /// </summary>
  public static class JsonHelper
  {
    /// <summary>
    /// 访问URL地址
    /// </summary>
    /// <param name="url">URL地址</param>
    /// <param name="postDataStr">参数</param>
    /// <param name="Encod">编码方式</param>
    /// <returns></returns>
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
        catch (Exception ex)
        {
            throw ex;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        return rStr;
    }

    public static string ToJsJson(object item)
    {
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
        using (MemoryStream ms = new MemoryStream())
        {
            serializer.WriteObject(ms, item);
            StringBuilder sb = new StringBuilder();
            sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
            return sb.ToString();
        }

    }

    public static T FromJsonTo<T>(string jsonString)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        {
            T jsonObject = (T)ser.ReadObject(ms);
            return jsonObject;
        }
    }
  }
}