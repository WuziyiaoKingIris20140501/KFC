using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelVp.Common.Utilities.String
{
    /// <summary>
    /// �ṩ�ַ�������뷨����
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
        /// ����ϵͳ�ṩ�Ƚ��ַ���ֻ��һ���ո�ʱ������Ϊ�Ƚϵ��ַ�����Ϊ�ա�
        /// �÷����Ƕ�ϵͳ������һ�����䣬�������ַ�������ֻ��һ���ո�ʱ����֤�ַ���Ϊ�գ�
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
        /// �Ƚ�2���ַ��������Ƿ����,���ִ�Сд��
        /// <remarks>2���ַ���ת��ΪСд�ַ����бȽ�</remarks>
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns>����ȣ���ΪTrue����֮ΪFalse</returns>
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
        /// ����һ������ֵ��ָ�������ַ����Ƿ���ȣ������ִ�Сд��
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns>����ȣ���ΪTrue����֮ΪFalse��</returns>
        public static bool AreEqualIgnoreCase(string input1, string input2)
        {
            return (input1 == input2) || StringComparer.OrdinalIgnoreCase.Compare(input1, input2) == 0;
        }

        /// <summary>
        /// ȥ���ַ�����ǰ��ո񡣵��ַ���Ϊnullʱ������null
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
