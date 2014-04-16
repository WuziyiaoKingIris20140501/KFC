using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelVp.Common.Utilities.String
{
    /// <summary>
    /// 正则表达式相关方法集合
    /// </summary>
    public static class RegularHelper
    {
        private const string m_NumberPattm = @"^[-+]?(0{1}|(([1-9]){1}[0-9]{0,6}))?$";
        private const string m_NumberPattmTemplate = @"^[-+]?(0{1}|(([1-9]){1}[0-9]{0,digit}))?$";
        private const string m_DecimalPattm = @"^[-+]?(0{1}|(([1-9]){1}[0-9]{0,6}))(\.[0-9]{0,2})?$";
        private const string m_DecimalPattmTemplate = @"^[-+]?(0{1}|(([1-9]){1}[0-9]{0,intDigit}))(\.[0-9]{0,decimalDigit})?$";
        private const string m_EmailPattm = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        private const string m_StringPattm="^\\w+$";
        private const string m_URLPattm = @"^http(s)?://([\w-]+\.?)+[\w-]+(\:\d+)?(/[\w- ./?%&=]*)?$";
        private const string m_GUIDPattm = @"^[A-Fa-f0-9]{8}(-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}$";

        private static Dictionary<string, Regex> m_RegexList = new Dictionary<string, Regex>();

        /// <summary>
        /// 根据传入的字符串和提供的正则表达式进行验证
        /// </summary>
        /// <param name="input">需要进行验证的字符串</param>
        /// <param name="pattem">验证时使用的正则表达式</param>
        /// <param name="options">正则表达式选项</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsMatch(string input, string pattem, RegexOptions options)
        {
            Regex reg = null;
            if (m_RegexList.ContainsKey(pattem))
            {
                reg = m_RegexList[pattem];
            }
            else
            {
                reg = new Regex(pattem, options);
                m_RegexList.Add(pattem, reg);
            }
            return reg.IsMatch(input);
        }

        /// <summary>
        /// 根据传入的字符串和提供的正则表达式进行验证
        /// </summary>
        /// <param name="input">需要进行验证的字符串</param>
        /// <param name="pattem">验证时使用的正则表达式</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsMatch(string input, string pattem)
        {
            return IsMatch(input, pattem, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// 检查输入的字符串是否是整数，默认7位数
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsNumber(string input)
        {
            bool result=false;
            result = IsMatch(input, m_NumberPattm);
            return result; 
        }

        /// <summary>
        /// 指定数字长度，进行整数验证
        /// <remarks>如果digit为空或者digit小于等于0，将自动默认使用7位数字验证</remarks>
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="digit">整数最大位数</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsNumber(string input, int digit)
        {
            bool result = false;
            string pattmTemplate = string.Empty;
            if ( digit > 0)
            {
                pattmTemplate = m_NumberPattmTemplate.Replace("digit",Convert.ToString(digit-1));
            }
            else
            {
                pattmTemplate = m_NumberPattm;
            }
            result = IsMatch(input, pattmTemplate);
            return result; 
        }

        /// <summary>
        /// 检查输入的字符串是否包含小数格式，默认7位整数，2位小数
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsDecimal(string input)
        {
            bool result = false;
            result = IsMatch(input, m_DecimalPattm);
            return result;
        }

        /// <summary>
        /// 指定小数位数，对输入字符串进行验证
        /// <remarks>此时整数部分，默认使用最大7位，小数根据使用者指定产生。如果指定的小数位数小于等于0，那么将采用默认的2位格式验证</remarks>
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="decimalDigit">小数最大位数</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsDecimal(string input,int decimalDigit)
        {
            bool result = false;
            string pattmTemplate = string.Empty;
            if ( decimalDigit > 0)
            {
                pattmTemplate = m_DecimalPattmTemplate.Replace("decimalDigit", decimalDigit.ToString());
                pattmTemplate = pattmTemplate.Replace("intDigit", "6");
            }
            else
            {
                pattmTemplate = m_DecimalPattm;
            }
            result = IsMatch(input, pattmTemplate);
            return result; 
        }

        /// <summary>
        /// 指定整数位数和小数位数,进行数字验证
        /// <remarks>如果指定的整数位数小于0，那么将使用默认的7位整数格式
        ///          如果指定的小数位数小于0，那么将使用默认的2位小数格式</remarks>
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="intDigit">整数位数</param>
        /// <param name="decimalDigit">小数位数</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsDecimal(string input,int intDigit, int decimalDigit)
        {
            bool result = false;
            string pattmTemplate = string.Empty;
            if ( intDigit > 0)
            {
                pattmTemplate = m_DecimalPattmTemplate.Replace("intDigit", Convert.ToString(intDigit-1));
                
            }
            else
            {
                pattmTemplate = pattmTemplate.Replace("intDigit", "6");
            }
            if ( decimalDigit > 0)
            {
                pattmTemplate = pattmTemplate.Replace("decimalDigit", decimalDigit.ToString());
               
            }
            else
            {
                pattmTemplate = pattmTemplate.Replace("decimalDigit", "2");
            }
            result = IsMatch(input, pattmTemplate);
            return result;
        }
        /// <summary>
        /// 验证输入字符串是否EMail地址
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsEmailAddress(string input)
        {
            bool result = false;
            result = IsMatch(input, m_EmailPattm);
            return result;
        }

        /// <summary>
        /// 验证输入字符串是否是26个字母+数字+下划线
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsCheck(string input)
        {
            bool result = false;
            result = IsMatch(input, m_StringPattm);
            return result;
        }

        /// <summary>
        /// 验证输入字符串是否是URL
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsURL(string input)
        {
            return IsMatch(input, m_URLPattm);
        }

        /// <summary>
        /// 验证输入字符串是否是GUID值
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>匹配通过返回true，否则返回false</returns>
        public static bool IsGuid(string input)
        {
            return IsMatch(input, m_GUIDPattm);
        }
    }
}
