using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class RoleEntity: BaseEntity
    {
        private List<RoleDBEntity> roledbentity;
        public List<RoleDBEntity> RoleDBEntity
        {
            get { return roledbentity; }
            set { roledbentity = value; }
        }
    }

    public class RoleDBEntity
    { 
        private string roleID;
        public string RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }        
        }

        private string roleDescription;
        public string RoleDescription
        {
            get { return roleDescription; }
            set { roleDescription = value; }
        }

        private string roleActive;
        public string RoleActive
        {
            get { return roleActive; }
            set { roleActive = value; }
        }


        private string createTime;
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string isad;
        public string IsAD
        {
            get { return isad; }
            set { isad = value; }
        }


        private string updateTime;
        public string UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }


        private string roleCreator;
        public string RoleCreator
        {
            get { return roleCreator; }
            set { roleCreator = value; }
        }     
    
    }
}
