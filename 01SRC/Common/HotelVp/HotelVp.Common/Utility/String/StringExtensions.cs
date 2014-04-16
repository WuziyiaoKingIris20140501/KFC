using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Utilities.String
{
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSQL"></param>
        /// <returns></returns>
        public static string MakeSafeSql(this string s)
        {
            string t = s;
            t = t.Replace("'", "''");
            t = t.Replace("[", "[[]");
            t = t.Replace("%", "[%]");
            t = t.Replace("_", "[_]");
            return t;
        }

        public static string ReplaceUnsafeSqlParameter(this string s)
        {
            string t = s;
            t = t.Replace("[", "[[]");
            t = t.Replace("%", "[%]");
            t = t.Replace("_", "[_]");
            return t;
        }

        /// <summary>
        /// 比较2个字符串对象是否相等,区分大小写。
        /// <remarks>2个字符串转换为小写字符进行比较</remarks>
        /// </summary>
        /// <param name="compareWith"></param>
        /// <returns>若相等，则为True；反之为False</returns>
        public static bool IsEqual(this string s, string compareWith)
        {
            if (compareWith == null)
            {
                return false;
            }
            if (s.ToLower().Trim() == compareWith.ToLower().Trim())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回一个布尔值，指定两个字符串是否相等，不区分大小写。
        /// </summary>
        /// <param name="compareWith"></param>
        /// <returns>若相等，则为True；反之为False。</returns>
        public static bool IsEqualIgnoreCase(this string s, string compareWith)
        {
            return (s == compareWith) || StringComparer.OrdinalIgnoreCase.Compare(s, compareWith) == 0;
        }

        /// <summary>
        /// 将指定字符串的首字母转换为大写字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UpperFirstChar(this string s)
        {
            if (s.Trim().Length == 0)
            {
                return s;
            }

            string result = string.Empty;
            string[] tmp = s.Split(' ');
            for (int i = 0; i < tmp.Length; i++)
            {
                result += Upper(tmp[i]);
                if (tmp.Length == 1 || i == tmp.Length - 1)
                {
                }
                else
                {
                    result += " ";
                }
            }
            return result;
        }

        private static string Upper(this string s)
        {
            if (s.Length == 0) return s;

            char[] array = s.ToCharArray();
            string result = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                if (i == 0)
                {
                    result += array[i].ToString().ToUpper();
                }
                else
                {
                    result += array[i].ToString().ToLower();
                }
            }
            return result;
        }
    }
}
