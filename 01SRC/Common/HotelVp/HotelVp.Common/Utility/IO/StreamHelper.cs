using System;
using System.IO;
using HotelVp.Common.Utilities.String;

namespace HotelVp.Common.Utilities.IO
{
    /// <summary>
    /// Utility for help operation Stream.
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        /// Create a memory stream from input byte array.
        /// </summary>
        /// <param name="inputData">Input data based on byte array.</param>
        /// <returns>A memory stream created from input data. If input data is null or empty, return null.</returns>
        public static MemoryStream CreateMemoryStreamFromBytes(byte[] inputData)
        {
            if (inputData == null || inputData.Length == 0)
            {
                return null;
            }
            MemoryStream result = new MemoryStream(inputData, false);
            return result;
        }

        /// <summary>
        /// Read the data based on byte array from a specific file which path is from input.
        /// </summary>
        /// <param name="localFilePath">File path you want read.</param>
        /// <returns>The file data based on byte array of input file. If input file path not exists, return null.</returns>
        public static byte[] ReadBytesFromFile(string localFilePath)
        {
            if (StringHelper.IsNullOrEmpty(localFilePath)
                || !File.Exists(localFilePath))
            {
                return null;
            }
            FileStream fs = new FileStream(localFilePath,
                FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] result = new byte[fs.Length];
            int bufferSize = Convert.ToInt32((long)65536 > fs.Length ? fs.Length : (long)65536);
            int offset = 0;
            int readCount = 0;
            using (fs as IDisposable)
            {
                while ((readCount = fs.Read(result, offset, bufferSize)) > 0)
                {
                    offset += readCount;
                    bufferSize = bufferSize > result.Length - offset ?
                        result.Length - offset : bufferSize;
                }
            }
            return result;
        }
    }
}
