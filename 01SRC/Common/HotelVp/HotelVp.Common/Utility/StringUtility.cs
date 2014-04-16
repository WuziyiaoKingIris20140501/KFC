using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Utilities
{
    public static class StringUtility
    {
        public static int Text_Length(string Text)
        {
            int len = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                byte[] byte_len = Encoding.UTF8.GetBytes(Text.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 3;  //如果长度大于1，是中文，占两个字节，+3
                else
                    len += 1;  //如果长度等于1，是英文，占一个字节，+1
            }
            return len;
        }

        public static bool IsNullOrEmpty(string value)
        {
            if (value == null)
            {
                return true;
            }

            return value.Trim().Length == 0;
        }

        public static bool AreTheSame(string str1, string str2)
        {
            return AreTheSame(str1, str2, StringCompareOptions.None);
        }

        public static bool AreTheSame(string str1, string str2, StringCompareOptions options)
        {
            if (str1 == null)
            {
                throw new ArgumentNullException("str1");
            }

            if (str2 == null)
            {
                throw new ArgumentNullException("str2");
            }

            if (str1 == str2)
            {
                return true;
            }

            string str1Post = str1;
            string str2Post = str2;

            if ((StringCompareOptions.IgnoreCase & options) == StringCompareOptions.IgnoreCase)
            {
                str1Post = str1.ToUpper();
                str2Post = str2.ToUpper();
            }

            if ((StringCompareOptions.IgnoreSpace & options) == StringCompareOptions.IgnoreSpace)
            {
                str1Post = str1Post.Trim();
                str2Post = str2Post.Trim();
            }

            return str1Post == str2Post;
        }

        public static bool AreTheSameIgnoreCase(string str1, string str2)
        {
            return (str1 == str2) || StringComparer.OrdinalIgnoreCase.Compare(str1, str2) == 0;
        }

        /// <summary>
        /// Encodes all the characters in the specified System.String into a sequence of bytes.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
        /// <exception cref="System.ArgumentNullException">s is null.</exception>
        public static byte[] ConvertToBytes(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            return ConvertToBytes(s, new ASCIIEncoding());
        }

        /// <summary>
        /// Encodes all the characters in the specified System.String into a sequence of bytes.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
        /// <exception cref="System.ArgumentNullException">s is null.</exception>
        public static byte[] ConvertToBytes(string s, Encoding encoding)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            return encoding.GetBytes(s);
        }

        /// <summary>
        /// Converts a string to its equivalent representation encoded with base 64 digits.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ConvertStringToBase64(string s, Encoding encoding)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            byte[] bytes = encoding.GetBytes(s);

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts a string to its equivalent representation encoded with base 64 digits.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertStringToBase64(string s)
        {
            return ConvertStringToBase64(s, Encoding.Default);
        }

        /// <summary>
        /// Converts a base 64 string to its equivalent representation string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ConvertBase64ToString(string s, Encoding encoding)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            byte[] bytes = Convert.FromBase64String(s);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Converts a base 64 string to its equivalent representation string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertBase64ToString(string s)
        {
            return ConvertBase64ToString(s, Encoding.Default);
        }
    }

    [FlagsAttribute]
    [Serializable]
    public enum StringCompareOptions
    {
        None = 0,
        IgnoreCase = 1,
        IgnoreSpace = 2
    }
}