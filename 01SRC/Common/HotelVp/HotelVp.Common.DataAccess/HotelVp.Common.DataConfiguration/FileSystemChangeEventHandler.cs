using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace HotelVp.Common.DataConfiguration
{
    /// <summary>
    /// 文件监控时间
    /// </summary>
    public class FileSystemChangeEventHandler
    {

        private class FileChangeEventArg
        {
            private object m_Sender;
            private FileSystemEventArgs m_Argument;

            public FileChangeEventArg(Object sender, FileSystemEventArgs arg)
            {
                m_Sender = sender;
                m_Argument = arg;
            }

            public Object Sender
            {
                get { return m_Sender; }
            }
            public FileSystemEventArgs Argument
            {
                get { return m_Argument; }
            }
        }

        private Object m_SyncObject;

        private Dictionary<string, Timer> m_Timers;
        private int m_Timeout;

        public event FileSystemEventHandler ActualHandler;

        private FileSystemChangeEventHandler()
        {
            m_SyncObject = new object();
            m_Timers = new Dictionary<string, Timer>(new CaseInsensitiveStringEqualityComparer());
        }

        public FileSystemChangeEventHandler(int timeout)
            : this()
        {
            m_Timeout = timeout;
        }

        /// <summary>
        /// 文件改动时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeEventHandler(Object sender, FileSystemEventArgs e)
        {
            lock (m_SyncObject)
            {
                
                Timer t;

                // disable the existing timer
                if (m_Timers.ContainsKey(e.FullPath))
                {
                    t = m_Timers[e.FullPath];
                    t.Change(Timeout.Infinite, Timeout.Infinite);
                    t.Dispose();
                }

                // add a new timer
                if (ActualHandler != null)
                {
                    t = new Timer(TimerCallback, new FileChangeEventArg(sender, e), m_Timeout, Timeout.Infinite);
                    m_Timers[e.FullPath] = t;
                }
            }
        }

        private void TimerCallback(Object state)
        {
            FileChangeEventArg arg = state as FileChangeEventArg;
            // LogActualHandleFileChange(arg);
            ActualHandler(arg.Sender, arg.Argument);
        }
    }
}
