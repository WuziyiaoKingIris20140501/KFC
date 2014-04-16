using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HotelVp.Common.Utilities
{
    /// <summary>
    /// 提供相同对象包含属性值得合并方法
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 保存对象类型
        /// </summary>
        private static Dictionary<string, Type> m_TypeList = new Dictionary<string, Type>();

        /// <summary>
        /// 获得合并对象类型，并暂时保存到Dictionary类型中
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
        /// 使用input对象所有的属性值覆盖original对象所有的属性值
        /// </summary>
        /// <exception cref="ArgumentNullException">传入的original对象为null</exception>
        /// <exception cref="ArgumentNullException">传入的input对象为null</exception>
        /// <remarks>使用反射将Input对象的所有属性的值赋给Original对象</remarks>
        /// <typeparam name="T">T 传入的对象类型</typeparam>
        /// <param name="original">原始对象</param>
        /// <param name="input">需要合并的对象</param>
        /// <returns>合并后对象的实例</returns>
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
