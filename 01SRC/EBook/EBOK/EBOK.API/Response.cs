using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.EBOK.Domain.API
{
    public class Response<T>
    {
        public string message { get; set; }

        public T result { get; set; }

        public int code { get; set; }

        public string header { get; set; }

        public Foot foot { get; set; }

        public Page page { get; set; }

        public bool timeOut { get; set; } // 连接超时
    }
}
