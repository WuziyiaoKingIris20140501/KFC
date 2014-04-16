using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace HotelVp.Common.DataConfiguration
{
    public  class Utilities
    {
        #region const
        private const string m_LongDefaultKey = "HotelVp.SHCMS";
       
        /// <summary>
        /// 默认使用的适合于RIJNDAEL算法的InitVector
        /// </summary>
        private const string m_LongDefaultIV = "HotelVp.CMS";
        #endregion 

        #region  serializer
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
                fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
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
        #endregion 

        public static string GetConfigurationFile(string appSection)
        {
            string configFile = appSection;
            configFile = System.Configuration.ConfigurationManager.AppSettings[appSection];
            if (configFile != null)
            {
                if (!Path.IsPathRooted(configFile))
                {

                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile.Replace('/', '\\').TrimStart('\\'));
                }
                else
                {
                    return configFile.Replace('/', '\\').TrimStart('\\');
                }
            }
            else
            {
                return "";
            }
        }

        public static string GetFileFullPath(string fileName)
        {
            if (!Path.IsPathRooted(fileName))
            {

                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName.Replace('/', '\\').TrimStart('\\'));
            }
            else
            {
                return fileName.Replace('/', '\\').TrimStart('\\');
            }
        }


        public static string GetConfigurationFullPath(string appSectionName)
        {
            string configFilePath = null;
            configFilePath = System.Configuration.ConfigurationManager.AppSettings[appSectionName];
            if (configFilePath != null)
                configFilePath = Path.GetFullPath(configFilePath);

            return configFilePath;
        }

        public static string GetConfigurationValue(string appSection)
        {
            string configValue = string.Empty;
            configValue = System.Configuration.ConfigurationManager.AppSettings[appSection];

            return configValue;
        }


        #region cryption

        internal static string Encrypt(string encryptionText)
        {
            string result = string.Empty;

            if (encryptionText.Length > 0)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(encryptionText);
                byte[] inArray = Encrypt(bytes);
                if (inArray.Length > 0)
                {
                    result = Convert.ToBase64String(inArray);
                }
               

            }
            return result;

        }

        internal static string Decrypt(string encryptionText)
        {
            string result = string.Empty;

            if (encryptionText.Length > 0)
            {
                byte[] bytes = Convert.FromBase64String(encryptionText);
                byte[] inArray = Decrypt(bytes);
                if (inArray.Length > 0)
                {
                    result = Encoding.Unicode.GetString(inArray);
                }
             
            }
            return result;

        }

        private static byte[] Encrypt(byte[] bytesData)
        {
            byte[] result = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                ICryptoTransform cryptoServiceProvider = CreateAlgorithm().CreateEncryptor();
                using (CryptoStream stream2 = new CryptoStream(stream, cryptoServiceProvider, CryptoStreamMode.Write))
                {
                    try
                    {
                        stream2.Write(bytesData, 0, bytesData.Length);
                        stream2.FlushFinalBlock();
                        stream2.Close();
                        result = stream.ToArray();
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Error while writing decrypted data to the stream: \n" + exception.Message);
                    }
                }
                stream.Close();
            }
            return result;
        }

        private static byte[] Decrypt(byte[] bytesData)
        {
            byte[] result = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                ICryptoTransform cryptoServiceProvider = CreateAlgorithm().CreateDecryptor();
                using (CryptoStream stream2 = new CryptoStream(stream, cryptoServiceProvider, CryptoStreamMode.Write))
                {
                    try
                    {
                        stream2.Write(bytesData, 0, bytesData.Length);
                        stream2.FlushFinalBlock();
                        stream2.Close();
                        result = stream.ToArray();
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Error while writing encrypted data to the stream: \n" + exception.Message);
                    }
                }
                stream.Close();
            }
            return result;
        }

        private static Rijndael CreateAlgorithm()
        {
            Rijndael rijndael = new RijndaelManaged();
            rijndael.Mode = CipherMode.CBC;
            byte[] key = Encoding.Unicode.GetBytes(m_LongDefaultKey);
            byte[] iv = Encoding.Unicode.GetBytes(m_LongDefaultIV);
            rijndael.Key = key;
            rijndael.IV = iv;
            return rijndael;
        }
        #endregion 
    }
}
