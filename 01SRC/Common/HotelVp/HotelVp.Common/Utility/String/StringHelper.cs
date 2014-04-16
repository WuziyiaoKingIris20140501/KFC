using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelVp.Common.Utilities.String
{
    /// <summary>
    /// 提供字符串相关想法集合
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSQL"></param>
        /// <returns></returns>
        [Obsolete("These helper methods have been merged in to string, please use string.MakeSafeSql() to instead")]
        public static string MakeSafeSql(string inputSQL)
        {
            string s = inputSQL;
            s = inputSQL.Replace("'", "''");
            s = s.Replace("[", "[[]");
            s = s.Replace("%", "[%]");
            s = s.Replace("_", "[_]");
            return s;
        }

        /// <summary>
        /// 由于系统提供比较字符串只有一个空格时，会认为比较的字符串不为空。
        /// 该方法是对系统方法的一个补充，即传入字符串有且只有一个空格时，验证字符串为空；
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(string input)
        {
            if (input == null)
                return true;
            if (input.Trim().Length == 0 )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 比较2个字符串对象是否相等,区分大小写。
        /// <remarks>2个字符串转换为小写字符进行比较</remarks>
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns>若相等，则为True；反之为False</returns>
        public static bool AreEqual(string input1, string input2)
        {
            if (input1 == null && input2 == null)
            {
                return true;
            }
            if (input1 == null || input2 == null)
            {
                return false;
            }
            if (input1.ToLower().Trim() == input2.ToLower().Trim())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回一个布尔值，指定两个字符串是否相等，不区分大小写。
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns>若相等，则为True；反之为False。</returns>
        public static bool AreEqualIgnoreCase(string input1, string input2)
        {
            return (input1 == input2) || StringComparer.OrdinalIgnoreCase.Compare(input1, input2) == 0;
        }

        /// <summary>
        /// 去掉字符串的前后空格。当字符串为null时，返回null
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimString(string s)
        {
            return s == null ? null : s.Trim();
        }

        public static string EscapeSqlText(this string s)
        {
            string str = s;
            return str.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
        }

        public static bool EqualIgnoreCase(string str1, string str2)
        {
            return string.Equals(str1.ToUpper(), str2.ToUpper());
        }
    }
}
