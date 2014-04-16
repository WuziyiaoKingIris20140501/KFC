using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{  
    public class UserElementEntity: BaseEntity
    {
        private List<UserElementDBEntity> userElementDBEntity;
        public List<UserElementDBEntity> UserElementDBEntity
        {
            get { return userElementDBEntity; }
            set { userElementDBEntity = value; }
        }
    }


    public class UserElementDBEntity
    {     
        private string roleID;
        public string RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        private string userAccount;
        public string UserAccount
        {
            get { return userAccount; }
            set { userAccount = value; }
        }

        private string ueType;
        public string UEType
        {
            get { return ueType; }
            set { ueType = value; }
        }

        private string createTime;
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

         private string updateTime;
        public string UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        private string ueCreator;
        public string UECreator
        {
            get { return ueCreator; }
            set { ueCreator = value; }
        }
    
    }
}
