using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace HotelVp.CMS.Domain.Entity.Promotion
{
    public class PromotionTypeEntity : BaseEntity
    {
        private List<PromotionTypeDBEntity> promotiontypedbentity;
        public List<PromotionTypeDBEntity> PromotiontypeDBEntity
        {
            get { return promotiontypedbentity; }
            set { promotiontypedbentity = value; }
        }
    }

    public class PromotionTypeDBEntity
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private string seq;
        public string Seq
        {
            get { return seq; }
            set { seq = value; }
        }
        

    }
}
