using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Entity
{
    [Serializable]
    public class PagingInfoEntity
    {
        public int TotalCount { get; set; }

        public int? StartRowIndex { get; set; }

        public int? MaximumRows { get; set; }

        public string SortField { get; set; }
    }
}
