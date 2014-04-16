using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;


namespace HotelVp.CMS.Domain.DataAccess
{
     public abstract class MenuDA
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="menuEntity"></param>
        /// <returns></returns>
        public static MenuEntity SelectMenuList(MenuEntity menuEntity)
        {
            MenuDBEntity dbParm = (menuEntity.MenuDBEntity.Count > 0) ? menuEntity.MenuDBEntity[0] : new MenuDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectMenuList");//查询
            cmd.SetParameterValue("@SearchText", dbParm.SearchText);
            menuEntity.QueryResult = cmd.ExecuteDataSet();
            return menuEntity;
        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="menuEntity"></param>
        /// <returns></returns>
        public static int Insert(MenuEntity menuEntity)
        {

            MenuDBEntity dbParm = (menuEntity.MenuDBEntity.Count > 0) ? menuEntity.MenuDBEntity[0] : new MenuDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("InsertMenu");

           cmd.SetParameterValue("@Parent_MenuId",dbParm.Parent_MenuId);
           cmd.SetParameterValue("@Menu_Name",dbParm.Menu_Name);
           cmd.SetParameterValue("@Menu_Url",dbParm.Menu_Url);
           cmd.SetParameterValue("@Menu_Target",dbParm.Menu_Target);
           cmd.SetParameterValue("@Menu_Show",dbParm.Menu_Show);
           cmd.SetParameterValue("@Menu_OrderID",dbParm.Menu_OrderID);
           cmd.SetParameterValue("@Menu_Type",dbParm.Menu_Type);
           cmd.SetParameterValue("@Menu_Limit",dbParm.Menu_Limit);           
           cmd.SetParameterValue("@Menu_Level",dbParm.Menu_Level);
           cmd.SetParameterValue("@Menu_Creator", dbParm.Menu_Creator);
           int intCount = cmd.ExecuteNonQuery();
           return intCount;
        }

        //修改菜单信息
        public static int UpdateMenuByMenuID(MenuEntity menuEntity)
        {
           // MenuDBEntity dbParm2 = (menuEntity.MenuDBEntity.Count > 0) ? menuEntity.MenuDBEntity[0] : new MenuDBEntity();
            MenuDBEntity dbParm2 = menuEntity.MenuDBEntity[0];
            DataCommand cmd2 = DataCommandManager.GetDataCommand("UpdateMenuByMenuID");
           
            cmd2.SetParameterValue("@Parent_MenuId", dbParm2.Parent_MenuId);
            cmd2.SetParameterValue("@Menu_Name",dbParm2.Menu_Name);
            cmd2.SetParameterValue("@Menu_Url",dbParm2.Menu_Url); 
            cmd2.SetParameterValue("@Menu_Target",dbParm2.Menu_Target);
            cmd2.SetParameterValue("@Menu_Show",dbParm2.Menu_Show); 
            cmd2.SetParameterValue("@Menu_OrderID" ,dbParm2.Menu_OrderID);          
            cmd2.SetParameterValue("@Menu_Level" ,dbParm2.Menu_Level);
            cmd2.SetParameterValue("@Menu_Limit" ,dbParm2.Menu_Limit);
            cmd2.SetParameterValue("@Update_Time" ,dbParm2.Update_Time); 
            cmd2.SetParameterValue("@Menu_ID", dbParm2.Menu_ID);

            int intCount = cmd2.ExecuteNonQuery();
            return intCount;           
        
        }
        //修改菜单是否限制
        public static int UpdateMenuLimitByMenuID(MenuEntity menuEntity)
        {
            MenuDBEntity dbParm = (menuEntity.MenuDBEntity.Count > 0) ? menuEntity.MenuDBEntity[0] : new MenuDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateMenuLimit");
            cmd.SetParameterValue("@Menu_Limit", dbParm.Menu_Limit);
            cmd.SetParameterValue("@Menu_ID", dbParm.Menu_ID);
            int intCount = cmd.ExecuteNonQuery();
            return intCount;       
        }

        //修改菜单是否显示
        public static int UpdateMenuDisplayByMenuID(MenuEntity menuEntity)
        {
            MenuDBEntity dbParm = (menuEntity.MenuDBEntity.Count > 0) ? menuEntity.MenuDBEntity[0] : new MenuDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateMenuDisplay");
            cmd.SetParameterValue("@Menu_Show", dbParm.Menu_Show);
            cmd.SetParameterValue("@Menu_ID", dbParm.Menu_ID);
            int intCount = cmd.ExecuteNonQuery();
            return intCount; 
        
        }

        //删除
        public static int DeleteMenuByMenuID(MenuEntity menuEntity)
        {
            MenuDBEntity dbParm = (menuEntity.MenuDBEntity.Count > 0) ? menuEntity.MenuDBEntity[0] : new MenuDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("DeleteMenuByMenuID");           
            cmd.SetParameterValue("@Menu_ID", dbParm.Menu_ID);
            int intCount = cmd.ExecuteNonQuery();
            return intCount;         
        }


        //返回第一级菜单。
        public static DataSet getFristMenu()
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectFirstMenuList");
            DataSet ds = cmd.ExecuteDataSet();
            return ds;        
        }

    }
}
