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
    /// �������л���ط���
    /// </summary>
    public static class SerializeHelper
    {
        /// <summary>
        /// ָ��Xml�ļ���������ע������ͷ����л��ɶ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">�ļ����ƣ�����·�����ļ�����</param>
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
        /// ָ�� Stream������ע������ͷ����л��ɶ���
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
        /// ָ�����ó��򼯵���Դ�ļ� ������ע������ͷ����л��ɶ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceName">���ó��򼯵���Դ�ļ���</param>
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
        /// ָ�����������Դ�ļ� ������ע������ͷ����л��ɶ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceName">���ó��������Դ�ļ���</param>
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
        /// ���������л��󣬱��浽ָ�����ļ��У�
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
        /// ָ��Xml�ļ���������ע���WCF Contract���ͷ����л��ɶ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">�ļ����ƣ�����·�����ļ�����</param>
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
        /// ��WCF Contract�������л��󣬱��浽ָ�����ļ��У�
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
        /// ���������л�ΪXml��ʽ���ַ���
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
        /// ���ܶ��󣬲�ͨ��Xml��ʽ���л�����
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
        /// ָ�������л����ͣ�ͨ��Xml���л�����ַ���������Ϊ����ʵ����
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
        /// ����Xml��ʽ���л���ɶ������ȿ�����
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
        /// ͨ�������Ƹ�ʽ������л�����
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
        /// ͨ�������Ƹ�ʽ������л�����
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
        /// ͨ�������Ƹ�ʽ������л�����
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
        /// ͨ�������Ƹ�ʽ������л�����
        /// </summary>
        /// <param name="serialObject"></param>
        /// <returns></returns>
        public static string BinarySerializer(object serialObject)
        {
            return BinarySerializer(serialObject, Encoding.Unicode);
        }

        /// <summary>
        /// �������Ƹ�ʽ���л�����ַ����������л�Ϊ����
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
        /// �������Ƹ�ʽ���л�����ַ����������л�Ϊ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T BinaryDeserialize<T>(string str)
        {
            return BinaryDeserialize<T>(str, Encoding.Unicode);
        }

        /// <summary>
        /// �����л�DataContract��Ϣ����
        /// </summary>
        /// <typeparam name="T">��Ϣ��������</typeparam>
        /// <param name="xmlData">���л�ʱ������xml�ַ���</param>
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
        /// ���л�DataContract��Ϣ����
        /// </summary>
        /// <typeparam name="T">��Ϣ��������</typeparam>
        /// <param name="myObject">��Ϣ����ʵ��</param>
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
        /// ���������л�ΪJson��ʽ
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
        /// ��Json�����л�Ϊ����
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
