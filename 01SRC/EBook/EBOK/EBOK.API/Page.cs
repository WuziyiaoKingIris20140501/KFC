using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.EBOK.Domain.API
{
    public class Page
    {
        public int count { get; set; }

        public int pageSize { get; set; }

        public bool hasPre { get; set; }

        public bool hasNext { get; set; }

        public int currPage { get; set; }

        public int totalPage { get; set; }
    }
}
