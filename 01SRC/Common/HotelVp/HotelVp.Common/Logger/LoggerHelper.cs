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
    /// �ṩ��ͬ�����������ֵ�úϲ�����
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