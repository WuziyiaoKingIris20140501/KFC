using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Diagnostics;

namespace HotelVp.Common.Utilities
{
    /// <summary>
    /// 对象序列化相关方法
    /// </summary>
    public static class SerializeHelper
    {
        /// <summary>
        /// 指定Xml文件名，根据注入的类型反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">文件名称（包含路径和文件名）</param>
        /// <returns></returns>
        public static T LoadFromXml<T>(string fileName)
        {
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return (T)serializer.Deserialize(fs);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 指定 Stream，根据注入的类型反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        public static T LoadFromStream<T>(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }

        /// <summary>
        /// 指定调用程序集的资源文件 ，根据注入的类型反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceName">调用程序集的资源文件名</param>
        /// <returns></returns>
        public static T LoadFromResource<T>(string resourceName)
        {
            using(Stream stream = Assembly.
                GetCallingAssembly().
                GetManifestResourceStream(resourceName))
            {
                Debug.Assert(stream != null);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// 指定调用类的资源文件 ，根据注入的类型反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceName">调用程序类的资源文件名</param>
        /// <returns></returns>
        public static T LoadFromCallingResource<T>(string resourceName)
        {
            StackFrame frame = new StackFrame(1);
            Type callingType = frame.GetMethod().DeclaringType;
            using (Stream stream = Assembly.
                GetCallingAssembly().
                GetManifestResourceStream(callingType, resourceName))
            {
                Debug.Assert(stream != null);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// 将对象序列化后，保存到指定的文件中；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void SaveToXml<T>(string fileName, T data)
        {
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                serializer.Serialize(fs, data);
            }
            catch (Exception e)
            {
                 throw e;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 指定Xml文件名，根据注入的WCF Contract类型反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">文件名称（包含路径和文件名）</param>
        /// <returns></returns>
        public static T LoadContractFromXml<T>(string fileName) where T : class
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(fileName);
                return DataContractDeserializer<T>(reader.ReadToEnd());
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// 将WCF Contract对象序列化后，保存到指定的文件中；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void SaveContractToXml<T>(string fileName, T data) where T : class
        {
            FileStream fs = null;
            try
            {
                string xmlData = DataContractSerializer<T>(data);

                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                byte[] bytes = Encoding.UTF8.GetBytes(xmlData);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 将对象序列化为Xml格式的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialObject"></param>
        /// <returns></returns>
        public static string XmlSerializer<T>(T serialObject)
        {
            StringBuilder sb = new StringBuilder(5000);
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StringWriter(sb))
            {
                ser.Serialize(writer, serialObject);
                return writer.ToString();
            }
        }

        /// <summary>
        /// 接受对象，并通过Xml格式序列化对象
        /// </summary>
        /// <param name="serialObject"></param>
        /// <returns></returns>
        public static string XmlSerializer(object serialObject)
        {
            StringBuilder sb = new StringBuilder(5000);
            XmlSerializer ser = new XmlSerializer(serialObject.GetType());
            using (TextWriter writer = new StringWriter(sb))
            {
                ser.Serialize(writer, serialObject);
                return writer.ToString();
            }
        }

        /// <summary>
        /// 指定反序列化类型，通过Xml序列化后的字符串反序列为对象实例；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string str)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(str))
            {
                return (T)mySerializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// 利用Xml格式序列化完成对象的深度拷贝；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static T DeepClone<T>(T sourceObject)
        {
            T result = default(T);
            if (sourceObject != null)
            {
                string s = XmlSerializer(sourceObject);
                result= XmlDeserialize<T>(s);
            }
            return result;
        }

        /// <summary>
        /// 通过二进制格式完成序列化对象。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialObject"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string BinarySerializer<T>(T serialObject, Encoding encoding)
        {
            string result = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, serialObject);
                    ms.Position = 0;
                    result = encoding.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 通过二进制格式完成序列化对象。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialObject"></param>
        /// <returns></returns>
        public static string BinarySerializer<T>(T serialObject)
        {
            return BinarySerializer<T>(serialObject, Encoding.Unicode);
        }

        public static string CallServiceSerializer<T>(T serialObject)
        {
            return BinarySerializer<T>(serialObject, Encoding.Unicode);
        }

        public static T CallServiceDeserializer<T>(string str)
        {
            return BinaryDeserialize<T>(str);
        }

        /// <summary>
        /// 通过二进制格式完成序列化对象。
        /// </summary>
        /// <param name="serialObject"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string BinarySerializer(object serialObject, Encoding encoding)
        {
            string result = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, serialObject);
                    ms.Position = 0;
                    result = encoding.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 通过二进制格式完成序列化对象。
        /// </summary>
        /// <param name="serialObject"></param>
        /// <returns></returns>
        public static string BinarySerializer(object serialObject)
        {
            return BinarySerializer(serialObject, Encoding.Unicode);
        }

        /// <summary>
        /// 将二进制格式序列化后的字符串，反序列化为对象；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T BinaryDeserialize<T>(string str, Encoding encoding)
        {
            T result = default(T);
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    byte[] bytes = encoding.GetBytes(str);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Position = 0;
                    result = (T)formatter.Deserialize(ms);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 将二进制格式序列化后的字符串，反序列化为对象；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T BinaryDeserialize<T>(string str)
        {
            return BinaryDeserialize<T>(str, Encoding.Unicode);
        }

        /// <summary>
        /// 反序列化DataContract消息对象
        /// </summary>
        /// <typeparam name="T">消息对象类型</typeparam>
        /// <param name="xmlData">序列化时产生的xml字符串</param>
        /// <returns></returns>
        public static T DataContractDeserializer<T>(string xmlData) where T : class
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlData));

            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            T deserializedPerson = (T)ser.ReadObject(reader, true);
            reader.Close();
            stream.Close();

            return deserializedPerson;
        }

        /// <summary>
        /// 序列化DataContract消息对象
        /// </summary>
        /// <typeparam name="T">消息对象类型</typeparam>
        /// <param name="myObject">消息对象实例</param>
        /// <returns></returns>
        public static string DataContractSerializer<T>(T myObject) where T : class
        {
            MemoryStream stream = new MemoryStream();
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            ser.WriteObject(stream, myObject);
            stream.Close();

            return System.Text.UnicodeEncoding.UTF8.GetString(stream.ToArray());
        }

        /// <summary>
        /// 将对象序列化为Json格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string JsonSerializer(object data)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.UTF8.GetString(ms.ToArray());
             }
        }

        /// <summary>
        /// 将Json反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
