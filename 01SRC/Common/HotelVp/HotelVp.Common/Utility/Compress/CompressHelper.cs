using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HotelVp.Common.Utilities.Compress
{
    /// <summary>
    /// �ṩѹ����صķ���
    /// </summary>
    public static class CompressHelper
    {
        /// <summary>
        /// ѹ���ַ���
        /// 
        /// </summary>
        /// <param name="unCompressedString"></param>
        /// <returns></returns>
        public static string CompressString(string unCompressedString)
        {
            byte[] bytData = System.Text.Encoding.UTF8.GetBytes(unCompressedString);
            MemoryStream ms = new MemoryStream();
            using (Stream s = new GZipStream(ms, CompressionMode.Compress))
            {
                s.Write(bytData, 0, bytData.Length);
                s.Close();
            }
            byte[] compressedData = (byte[])ms.ToArray();

            return System.Convert.ToBase64String(compressedData, 0, compressedData.Length);
        }

        /// <summary>
        /// ��ѹ�ַ���
        /// 
        /// </summary>
        /// <param name="unCompressedString"></param>
        /// <returns></returns>
        public static string DecompressString(string unCompressedString)
        {
            System.Text.StringBuilder uncompressedString = new System.Text.StringBuilder();
            byte[] writeData = new byte[4096];

            byte[] bytData = System.Convert.FromBase64String(unCompressedString);
            int totalLength = 0;
            int size = 0;

            using (Stream s = new GZipStream(new MemoryStream(bytData), CompressionMode.Decompress))
            {
                while (true)
                {
                    size = s.Read(writeData, 0, writeData.Length);
                    if (size > 0)
                    {
                        totalLength += size;
                        uncompressedString.Append(System.Text.Encoding.UTF8.GetString(writeData, 0, size));
                    }
                    else
                    {
                        break;
                    }
                }
                s.Close();
            }
            return uncompressedString.ToString();
        }

        /// <summary>
        /// �ṩ�ļ������б��ѹ���󱣴���ļ����ƣ���ָ���ļ�����ѹ��
        /// </summary>
        /// <param name="fileName">��Ҫѹ�����ļ��б�</param>
        /// <param name="compressFileName">ѹ�����ŵ��ļ�����</param>
        /// <returns></returns>
        public static bool CompressFiles(IList<string> fileName, string compressFileName)
        {
            bool result = false;
            try
            {
                ArrayList fileList = new ArrayList();
                List<byte[]> byteList = new List<byte[]>();
                foreach (string item in fileName)
                {
                    if (File.Exists(item))
                    {
                        CompressFileInfo fileInfo = new CompressFileInfo();
                        fileInfo.FileName = Path.GetFileName(item);
                        fileInfo.FileBuffer = File.ReadAllBytes(item);
                        fileList.Add(fileInfo);
                    }
                }

                IFormatter formatter = new BinaryFormatter();
                using (Stream s = new MemoryStream())
                {
                    formatter.Serialize(s, fileList);
                    s.Position = 0;
                    CreateCompressFiles(s, compressFileName);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        /// <summary>
        /// ��ָ����ѹ���ļ����н�ѹ���������ָ��·����
        /// </summary>
        /// <param name="fileName">��Ҫ��ѹ���ļ�ȫ��</param>
        /// <param name="outputPath">��ѹ���ļ����·��</param>
        /// <returns></returns>
        public static bool DecompressFiles(string fileName, string outputPath)
        {
            bool result = false;
            using (Stream source = File.OpenRead(fileName))
            {
                using (Stream destination = new MemoryStream())
                {
                    using (GZipStream input = new GZipStream(source, CompressionMode.Decompress, true))
                    {
                        byte[] bytes = new byte[4096];
                        int n;
                        while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            destination.Write(bytes, 0, n);
                        }
                    }
                    destination.Flush();
                    destination.Position = 0;
                    DeserializeFileInfo(destination, outputPath);
                    result = true;

                }
            }

            return result;
        }

        /// <summary>
        /// ����Ҫѹ�����ļ�������ѹ����Ȼ�󱣴浽ָ�����ļ���
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <param name="compressFileName"></param>
        private static void CreateCompressFiles(Stream sourceStream, string compressFileName)
        {
            using (Stream destination = new FileStream(compressFileName, FileMode.Create, FileAccess.Write))
            {
                using (GZipStream output = new GZipStream(destination, CompressionMode.Compress))
                {
                    byte[] bytes = new byte[4096];
                    int n;
                    while ((n = sourceStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        output.Write(bytes, 0, n);
                        
                    }
                }
            }

        }

        /// <summary>
        /// �����л��ļ���������
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <param name="outputPath"></param>
        private static void DeserializeFileInfo(Stream sourceStream, string outputPath)
        {
            BinaryFormatter b = new BinaryFormatter();
            ArrayList list = (ArrayList)b.Deserialize(sourceStream);

            foreach (CompressFileInfo item in list)
            {
                string newName =Path.Combine(outputPath , item.FileName);
                using (FileStream fs = new FileStream(newName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(item.FileBuffer, 0, item.FileBuffer.Length);
                    fs.Close();
                }
            }
        }

    }
}
