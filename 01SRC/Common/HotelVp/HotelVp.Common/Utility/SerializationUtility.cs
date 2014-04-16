using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace HotelVp.Common.Utilities
{
    public static class SerializationUtility
    {
        public static T LoadFromXml<T>(string fileName)
        {
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return (T)serializer.Deserialize(fs);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        public static void SaveToXml<T>(string fileName, T data)
        {
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                serializer.Serialize(fs, data);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        public static string XmlSerialize<T>(T serialObject)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StringWriter(sb))
            {
                ser.Serialize(writer, serialObject);
                return writer.ToString();
            }
        }

        public static string XmlSerialize(object serialObject)
        {
            StringBuilder sb = new StringBuilder(5000);
            XmlSerializer ser = new XmlSerializer(serialObject.GetType());
            using (TextWriter writer = new StringWriter(sb))
            {
                ser.Serialize(writer, serialObject);
                return writer.ToString();
            }
        }

        public static T XmlDeserialize<T>(string str)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(str))
            {
                return (T)mySerializer.Deserialize(reader);
            }
        }

        public static object XmlDeserialize(string str, Type type)
        {
            XmlSerializer mySerializer = new XmlSerializer(type);
            using (TextReader reader = new StringReader(str))
            {
                return mySerializer.Deserialize(reader);
            }
        }

        public static string BinarySerialize<T>(T serialObject, Encoding encoding)
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
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        public static string BinarySerialize<T>(T serialObject)
        {
            return BinarySerialize<T>(serialObject, Encoding.Default);
        }

        public static string BinarySerialize(object serialObject, Encoding encoding)
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
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        public static string BinarySerialize(object serialObject)
        {
            return BinarySerialize(serialObject, Encoding.Default);
        }

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
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        public static T BinaryDeserialize<T>(string str)
        {
            return BinaryDeserialize<T>(str, Encoding.Default);
        }

        public static object BinaryDeserialize(string str)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                byte[] bytes = Encoding.Default.GetBytes(str);
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return formatter.Deserialize(ms);
            }
        }
    }

    //public static class IOUtility
    //{
    //    public static void SaveToFile(string content, string fileName)
    //    {
    //        FileStream fs = null;
    //        try
    //        {
    //            XmlSerializer serializer = new XmlSerializer(typeof(T));
    //            fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
    //            serializer.Serialize(fs, data);
    //        }
    //        finally
    //        {
    //            if (fs != null)
    //            {
    //                fs.Close();
    //            }
    //        }
    //    }
    //}
}