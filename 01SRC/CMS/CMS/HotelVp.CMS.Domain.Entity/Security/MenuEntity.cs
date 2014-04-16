using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
   
    public class MenuEntity : BaseEntity
    {
        private List<MenuDBEntity> menudbentity;
        public List<MenuDBEntity> MenuDBEntity
        {
            get { return menudbentity; }
            set { menudbentity = value; }
        }
    }

    public class MenuDBEntity
    {
        private int menu_ID;
        public int Menu_ID
        {
            get { return menu_ID; }
            set { menu_ID = value; }
        }

        private int parent_MenuId;
        public int Parent_MenuId
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
	
        private string menu_Url;
        public string Menu_Url
        {
            get { return menu_Url; }
            set { menu_Url = value; }
        }   
	
        private string menu_Target=string.Empty;
        public string Menu_Target
        {
            get { return menu_Target; }
            set { menu_Target = value; }
        }   
	
        private int menu_Show;
        public int Menu_Show
        {
            get { return menu_Show; }
            set { menu_Show = value; }
        }   

        private int menu_OrderID;
        public int Menu_OrderID
        {
            get { return menu_OrderID; }
            set { menu_OrderID = value; }
        }

        private int menu_Type;
        public int Menu_Type
        {
            get { return menu_Type; }
            set { menu_Type = value; }
        }

        private int menu_Level;
        public int Menu_Level
        {
            get { return menu_Level; }
            set { menu_Level = value; }
        }

        private int menu_Limit;
        public int Menu_Limit
        {
            get { return menu_Limit; }
            set { menu_Limit = value; }
        }


        private string create_Time;
        public string Create_Time
        {
            get { return create_Time; }
            set { create_Time = value; }
        }

        private string update_Time;
        public string Update_Time
        {
            get { return update_Time; }
            set { update_Time = value; }
        }

        private string menu_Creator;
        public string Menu_Creator
        {
            get { return menu_Creator; }
            set { menu_Creator = value; }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; }
        }

    }
}
