using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HotelVp.EBOK.Domain.API.Model
{
    public class RoomPlan
    {
        public DataSet planResult { get; set; }
        public List<string> Roomls { get; set; }

        public int lm2Cols { get; set; }
        public int lmCols { get; set; }
    }
}
