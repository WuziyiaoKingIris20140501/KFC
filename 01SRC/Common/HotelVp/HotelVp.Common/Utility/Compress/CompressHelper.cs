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
    /// 提供压缩相关的服务
    /// </summary>
    public static class CompressHelper
    {
        /// <summary>
        /// 压缩字符串
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
        /// 解压字符串
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
        /// 提供文件名称列表和压缩后保存的文件名称，对指定文件进行压缩
        /// </summary>
        /// <param name="fileName">需要压缩的文件列表</param>
        /// <param name="compressFileName">压缩后存放的文件名称</param>
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
        /// 将指定的压缩文件进行解压，并输出到指定路径中
        /// </summary>
        /// <param name="fileName">需要解压的文件全名</param>
        /// <param name="outputPath">解压后文件存放路径</param>
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
        /// 将需要压缩的文件流进行压缩，然后保存到指定的文件下
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
        /// 反序列化文件描述对象
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
