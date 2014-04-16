using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class PermissionEntity : BaseEntity
    {
        private List<PermissionDBEntity> permissiondbentity;
        public List<PermissionDBEntity> PermissionDBEntity
        {
            get { return permissiondbentity; }
            set { permissiondbentity = value; }
        }
    }


    public class PermissionDBEntity
    {
        private string user_ID;
        public string USER_ID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }

        private string permission_Code;
        public string Permission_Code
        {
            get { return permission_Code; }
            set { permission_Code = value; }
        }


       private string permission_Type;
       public string Permission_Type
       {
            get { return permission_Type; }
            set { permission_Type = value; }
       }

       private string module_ID;
       public string Module_ID
       {
           get { return module_ID; }
           set { module_ID = value; }
       }

       private string module_Type;
       public string Module_Type
       {
            get { return module_Type; }
            set { module_Type = value; }
       }

       private string module_Right;
       public string Module_Right
       {
            get { return module_Right; }
            set { module_Right = value; }
       }

       private string creator;
       public string Creator
       {
           get { return creator; }
           set { creator = value; }
       }

       private string updatedBy;
       public string UpdatedBy
       {
           get { return updatedBy; }
           set { updatedBy = value; }
       }

       private string update_Time;
       public string Update_Time
       {
           get { return update_Time; }
           set { update_Time = value; }
       }

       private string menu_ID;
       public string Menu_ID
       {
           get { return menu_ID; }
           set { menu_ID = value; }
       }

       private string parent_MenuId;
       public string Parent_MenuId
       {
           get { return parent_MenuId; }
           set { parent_MenuId = value; }
       }

       private string menu_Name;
       public string Menu_Name
       {
           get { return menu_Name; }
           set { menu_Name = value; }
       }

       private string role_ID;
       public string Role_ID
       {
           get { return role_ID; }
           set { role_ID = value; }
       }

    }
}
