using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HotelVp.Common.Utilities
{
    /// <summary>
    /// �ṩ��ͬ�����������ֵ�úϲ�����
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// �����������
        /// </summary>
        private static Dictionary<string, Type> m_TypeList = new Dictionary<string, Type>();

        /// <summary>
        /// ��úϲ��������ͣ�����ʱ���浽Dictionary������
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static Type GetTypeInfo(object val)
        {
            string key = val.ToString();
            lock (typeof(ObjectHelper))
            {
                if (!m_TypeList.ContainsKey(key))
                {
                    m_TypeList.Add(key, val.GetType());
                }
            }
            return m_TypeList[key];
        }

        /// <summary>
        /// ʹ��input�������е�����ֵ����original�������е�����ֵ
        /// </summary>
        /// <exception cref="ArgumentNullException">�����original����Ϊnull</exception>
        /// <exception cref="ArgumentNullException">�����input����Ϊnull</exception>
        /// <remarks>ʹ�÷��佫Input������������Ե�ֵ����Original����</remarks>
        /// <typeparam name="T">T ����Ķ�������</typeparam>
        /// <param name="original">ԭʼ����</param>
        /// <param name="input">��Ҫ�ϲ��Ķ���</param>
        /// <returns>�ϲ�������ʵ��</returns>
        public static T OverridePropertyValue<T>(T original, T input) where T : new()
        {
            if (original == null)
            {
                throw new ArgumentNullException("original");
            }
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            T target = new T();
            Type originalTypeInfo = GetTypeInfo(original);
            Type inputTypeInfo = GetTypeInfo(input);
            foreach (PropertyInfo pi in inputTypeInfo.GetProperties())
            {
                object o = pi.GetValue(input, null);
                if (o != null)
                {
                    PropertyInfo p = originalTypeInfo.GetProperty(pi.Name);
                    p.SetValue(target, o, null);
                }
                else
                {
                    PropertyInfo p = inputTypeInfo.GetProperty(pi.Name);
                    p.SetValue(target, p.GetValue(original, null), null);
                }
            }

            return target;
        }
    }
}
