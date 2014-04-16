using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Entity
{
    [Serializable]
    public class QueryResultList<T> where T : class, new()
    {
        public List<T> ResultList { get; set; }

        public PagingInfoEntity PagingInfo { get; set; }
    }
}
