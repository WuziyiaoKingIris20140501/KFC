using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class DbSearchEntity : BaseEntity
    {
        private List<DbSearchDBEntity> dbsearchdbEntity;
        public List<DbSearchDBEntity> DbSearchDBEntity
        {
            get { return dbsearchdbEntity; }
            set { dbsearchdbEntity = value; }
        }
    }

    public class DbSearchDBEntity
    {
        private string tableId;
        public string TableID
        {
            get { return tableId; }
            set { tableId = value; }
        }

        private string sqlContent;
        public string SqlContent
        {
            get { return sqlContent; }
            set { sqlContent = value; }
        }
    }
}