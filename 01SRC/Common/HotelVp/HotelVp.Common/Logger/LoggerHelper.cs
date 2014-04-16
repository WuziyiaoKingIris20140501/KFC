using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Resources;
using System.Collections;
using System.Configuration;
using log4net;

namespace HotelVp.Common.Logger
{
    /// <summary>
    /// 提供相同对象包含属性值得合并方法
    /// </summary>
    public static class LoggerHelper
    {
        public static void LogWriter(LogMessage messageInfo)
        {
            log4net.Config.XmlConfigurator.Configure();
            LogUtil.SaveMessage(messageInfo);  
        }
    }
}