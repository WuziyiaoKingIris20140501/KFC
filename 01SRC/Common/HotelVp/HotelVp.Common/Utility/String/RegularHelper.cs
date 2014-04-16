using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelVp.Common.Utilities.String
{
    /// <summary>
    /// ������ʽ��ط�������
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
        /// ���ݴ�����ַ������ṩ��������ʽ������֤
        /// </summary>
        /// <param name="input">��Ҫ������֤���ַ���</param>
        /// <param name="pattem">��֤ʱʹ�õ�������ʽ</param>
        /// <param name="options">������ʽѡ��</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
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
        /// ���ݴ�����ַ������ṩ��������ʽ������֤
        /// </summary>
        /// <param name="input">��Ҫ������֤���ַ���</param>
        /// <param name="pattem">��֤ʱʹ�õ�������ʽ</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
        public static bool IsMatch(string input, string pattem)
        {
            return IsMatch(input, pattem, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// ���������ַ����Ƿ���������Ĭ��7λ��
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
        public static bool IsNumber(string input)
        {
            bool result=false;
            result = IsMatch(input, m_NumberPattm);
            return result; 
        }

        /// <summary>
        /// ָ�����ֳ��ȣ�����������֤
        /// <remarks>���digitΪ�ջ���digitС�ڵ���0�����Զ�Ĭ��ʹ��7λ������֤</remarks>
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <param name="digit">�������λ��</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
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
        /// ���������ַ����Ƿ����С����ʽ��Ĭ��7λ������2λС��
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
        public static bool IsDecimal(string input)
        {
            bool result = false;
            result = IsMatch(input, m_DecimalPattm);
            return result;
        }

        /// <summary>
        /// ָ��С��λ�����������ַ���������֤
        /// <remarks>��ʱ�������֣�Ĭ��ʹ�����7λ��С������ʹ����ָ�����������ָ����С��λ��С�ڵ���0����ô������Ĭ�ϵ�2λ��ʽ��֤</remarks>
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <param name="decimalDigit">С�����λ��</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
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
        /// ָ������λ����С��λ��,����������֤
        /// <remarks>���ָ��������λ��С��0����ô��ʹ��Ĭ�ϵ�7λ������ʽ
        ///          ���ָ����С��λ��С��0����ô��ʹ��Ĭ�ϵ�2λС����ʽ</remarks>
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <param name="intDigit">����λ��</param>
        /// <param name="decimalDigit">С��λ��</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
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
        /// ��֤�����ַ����Ƿ�EMail��ַ
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
        public static bool IsEmailAddress(string input)
        {
            bool result = false;
            result = IsMatch(input, m_EmailPattm);
            return result;
        }

        /// <summary>
        /// ��֤�����ַ����Ƿ���26����ĸ+����+�»���
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
        /// ��֤�����ַ����Ƿ���URL
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
        public static bool IsURL(string input)
        {
            return IsMatch(input, m_URLPattm);
        }

        /// <summary>
        /// ��֤�����ַ����Ƿ���GUIDֵ
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <returns>ƥ��ͨ������true�����򷵻�false</returns>
        public static bool IsGuid(string input)
        {
            return IsMatch(input, m_GUIDPattm);
        }
    }
}
