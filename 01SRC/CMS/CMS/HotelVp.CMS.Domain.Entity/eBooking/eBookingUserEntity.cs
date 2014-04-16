using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity.eBooking
{
    public class eBookingUserEntity : BaseEntity
    {
        private List<eBookingUserDBEntity> ebookinguserdbentity;
        public List<eBookingUserDBEntity> eBookingUserDBEntity
        {
            get { return ebookinguserdbentity; }
            set { ebookinguserdbentity = value; }
        }
    }

    public class eBookingUserDBEntity
    {
        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private string hotelId;
        public string HotelId
        {
            get { return hotelId; }
            set { hotelId = value; }
        }

        private string operatorId;
        public string OperatorId
        {
            get { return operatorId; }
            set { operatorId = value; }
        }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int pageNum;
        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value; }
        }

    }

}
