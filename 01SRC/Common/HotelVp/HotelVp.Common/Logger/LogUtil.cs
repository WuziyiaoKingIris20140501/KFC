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
using HotelVp.Common.DBUtility;

namespace HotelVp.Common.Logger
{
    /// <summary>
    /// �ṩ��ͬ�����������ֵ�úϲ�����
    /// </summary>
    public class LogUtil
    {
        public LogUtil()
        { }
        private static LogMessage message = null;
        private static ILog _log;
        public static ILog log
        {
            get
            {
                if (_log == null)
                {
                    string strWebConfig = ConfigurationManager.AppSettings["WebLogConfig"].ToString();
                    //�������ļ��ж�ȡLogger����   
                    _log = log4net.LogManager.GetLogger(strWebConfig);
                }
                return _log;
            }
        }
        public static void debug()
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }
        public static void error()
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }
        public static void fatal()
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
        public static void info()
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }
        public static void warn()
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        /// <summary>   
        /// ��Ҫд��־�ĵط����ô˷���   
        /// </summary>   
        /// <param name="userID">�Զ����ֶ�</param>   
        /// <param name="UserName">�Զ����ֶ�</param>   
        /// <param name="level">�Զ��弶��</param>   
        public static void SaveMessage(LogMessage messageInfo)
        {
            message = messageInfo;//�õ��ֶθ�����   
            switch (message.MsgType) { case MessageType.INFO: info(); break; case MessageType.WARN: warn(); break; case MessageType.ERROR: error(); break; case MessageType.FATAL: fatal(); break; case MessageType.DEBUG: debug(); break; default: break; }
        }
    }
}