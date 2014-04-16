using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJZX.JobConsole.ServiceAdapter
{
    public class Response<T>
    {
        public string message { get; set; }

        public T result { get; set; }

        public int code { get; set; }

        public string header { get; set; }

        public bool timeOut { get; set; } // 连接超时
    }
}