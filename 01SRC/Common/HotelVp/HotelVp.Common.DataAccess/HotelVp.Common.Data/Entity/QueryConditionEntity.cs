using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Entity
{
    public class QueryConditionEntity<T> where T : class, new()
    {
        public QueryConditionEntity()
        {
            Condition = new T();
            PagingInfo = new PagingInfoEntity();
        }

        public T Condition { get; set; }
        public PagingInfoEntity PagingInfo { get; set; }
    }
}
