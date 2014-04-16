using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Resources;
using System.Collections;
using System.Configuration;

namespace HotelVp.Common.Utilities
{
    /// <summary>
    /// 提供相同对象包含属性值得合并方法
    /// </summary>
    public static class ResourceHelper
    {
        public static T ResXResourceReaderHelper<T>(T input, string resxName) where T : new()
        {
            if (resxName == null)
            {
                return input;
            }
            
            T target = new T();

            string path = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ResPath"].ToString() + resxName + ConfigurationManager.AppSettings["ResType"].ToString();

            ResXResourceReader rsxr = new ResXResourceReader(path);

            foreach (DictionaryEntry d in rsxr)
            {
                //promotion 
                if (target.GetType().GetProperty(d.Key.ToString()) != null)
                {
                   target.GetType().GetProperty(d.Key.ToString()).SetValue(target, Convert.ChangeType(d.Value, target.GetType().GetProperty(d.Key.ToString()).PropertyType), null);
                }
            }
            return target;
        }
    }
}