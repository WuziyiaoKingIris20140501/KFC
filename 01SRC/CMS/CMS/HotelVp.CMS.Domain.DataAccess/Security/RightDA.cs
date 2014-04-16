using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public class RightDA
    {
        #region setRight.aspx中使用
        /// <summary>
        /// 得到RoleId 和 RoleName的列表
        /// </summary>
        /// <returns></returns>
        public  DataSet getRoleList()
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectActiveRoleList");
            DataSet ds= cmd.ExecuteDataSet();
            return ds;
        }

        /// <summary>
        /// 获取User_Account 和User_DspName列表
        /// </summary>
        /// <returns></returns>
        public DataSet getUserList()
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectActiveUserList");
            DataSet ds = cmd.ExecuteDataSet();
            return ds;
        }

        /// <summary>
        /// 根据menu_level和menu_name查询得到menuID
        /// </summary>
        /// <param name="Menu_Level"></param>
        /// <param name="Menu_Name"></param>
        /// <returns></returns>
        public object getMenuIDByLevelAndName(string Menu_Level, string Menu_Name)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectMenuIDByMenuLevelAndMenuName");
            cmd.SetParameterValue("@Menu_Level",Menu_Level);
            cmd.SetParameterValue("@Menu_Name", Menu_Name);
            object objMenuID = cmd.ExecuteScalar();
            return objMenuID;        
        }

        /// <summary>
        /// 根据module_id查询满足条件的列表
        /// </summary>
        /// <param name="Module_ID"></param>
        /// <returns></returns>
        public DataSet getRightListByModuleID(int Module_ID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectRightByModuleID");
            cmd.SetParameterValue("@Module_ID", Module_ID);
            DataSet ds = cmd.ExecuteDataSet();
            return ds;         
        }

        /// <summary>
        /// 根据Menu_Level和Menu_Name查询得到满足条件的权利列表
        /// </summary>
        /// <param name="Menu_Level"></param>
        /// <param name="Menu_Name"></param>
        /// <returns></returns>
        public DataSet getMenuByLevelAndName(string Menu_Level, string Menu_Name)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectMenuByLevelAndName");
            cmd.SetParameterValue("@Menu_Level", Menu_Level);
            cmd.SetParameterValue("@Menu_Name", Menu_Name);
            DataSet ds = cmd.ExecuteDataSet();
            return ds;         
        }

        /// <summary>
        /// 根据Module_ID删除已经设置的权限
        /// </summary>
        /// <param name="Module_ID"></param>
        /// <returns></returns>
        public int deletePermissionByModuleID(string Module_ID)
        {
            int rtnResult = 0;
            DataCommand cmd = DataCommandManager.GetDataCommand("DeletePermissionByModuleID");
            cmd.SetParameterValue("@Module_ID", Module_ID);
            rtnResult = cmd.ExecuteNonQuery();
            return rtnResult;

        }
        #endregion

        #region permission使用

        /// <summary>
        /// 查询可以查看的菜单列表,根据用户账户
        /// </summary>
        /// <param name="SearchText"></param>
        /// <param name="UserAccount"></param>
        /// <returns></returns>
        public DataSet SelectModulesRightListByUserAccount(string SearchText ,string UserAccount)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectModulesRightListByUserAccount");
            cmd.SetParameterValue("@SearchText", SearchText);
            cmd.SetParameterValue("@USER_ID", UserAccount);
            DataSet ds = cmd.ExecuteDataSet();
            return ds;
        
        }

        public DataSet SelectModulesRightListByUserADAccount(string SearchText)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectModulesRightListByUserADAccount");
            cmd.SetParameterValue("@SearchText", SearchText);
            DataSet ds = cmd.ExecuteDataSet();
            return ds;
        
        }

        /// <summary>
        /// 根据菜单级别查询可以显示的菜单列表
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public DataSet SelectModulesByLevel(string SearchText)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectModulesByLevel");
           
            cmd.SetParameterValue("@SearchText", SearchText);
            DataSet ds = cmd.ExecuteDataSet();
            return ds;
        
        }

        public DataSet SelectModulesByAdmin(string SearchText)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectModulesByAdmin");

            string AdminMenuID = ConfigurationManager.AppSettings["AdminMenuID"];

            SearchText = " and Menu_ID=" + AdminMenuID + " or Parent_MenuId=" + AdminMenuID;

            
            cmd.SetParameterValue("@SearchText", SearchText);
            DataSet ds = cmd.ExecuteDataSet();
            return ds;
        
        }

        /// <summary>
        /// 查询所有设置了进行权限设置的菜单
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public DataSet SelectModulesWithLimit()
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectModulesWithLimit");             
            DataSet ds = cmd.ExecuteDataSet();
            return ds;
        
        }
        


        #endregion


        #region right_user使用
        public int InsertRightForUser(PermissionDBEntity permissionDBEntity)
        {
            int rtnResult = 0;
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertRightForUser");
            cmd.SetParameterValue("@USER_ID", permissionDBEntity.USER_ID);
            cmd.SetParameterValue("@Permission_Type", permissionDBEntity.Permission_Type);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);
            cmd.SetParameterValue("@Module_Type", permissionDBEntity.Module_Type);
            cmd.SetParameterValue("@Module_Right", permissionDBEntity.Module_Right);
            cmd.SetParameterValue("@Creator", permissionDBEntity.Creator);
            rtnResult = cmd.ExecuteNonQuery();
            return rtnResult;        
        }

        public int UpdateRightForUser(PermissionDBEntity permissionDBEntity)
        {
            int rtnResult = 0;
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateRightForUser");
            cmd.SetParameterValue("@Module_Right", permissionDBEntity.Module_Right);
            cmd.SetParameterValue("@UpdatedBy", permissionDBEntity.UpdatedBy);
            cmd.SetParameterValue("@Update_Time", permissionDBEntity.Update_Time);
            cmd.SetParameterValue("@USER_ID", permissionDBEntity.USER_ID);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);            
            rtnResult = cmd.ExecuteNonQuery();
            return rtnResult;    
        
        }


        public int UpdateRightForUserModuleTypeEqual1(PermissionDBEntity permissionDBEntity)
        {
            int rtnResult = 0;
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateRightForUserModuleTypeEqual1");
            cmd.SetParameterValue("@Module_Right", permissionDBEntity.Module_Right);
            cmd.SetParameterValue("@UpdatedBy", permissionDBEntity.UpdatedBy);
            cmd.SetParameterValue("@Update_Time", permissionDBEntity.Update_Time);
            cmd.SetParameterValue("@USER_ID", permissionDBEntity.USER_ID);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);            
            rtnResult = cmd.ExecuteNonQuery();
            return rtnResult;    
        
        }



        public object SelectMenuIDByMenuIDInParentMenuID(PermissionDBEntity permissionDBEntity)
        {        
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectMenuIDByMenuIDInParentMenuID");
            cmd.SetParameterValue("@Menu_ID", permissionDBEntity.Menu_ID);
            object objResult = cmd.ExecuteScalar();
            return objResult;    
        
        }

        public object SelectCountByUserIDAndModuleID(PermissionDBEntity permissionDBEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectCountByUserIDAndModuleID");
            cmd.SetParameterValue("@USER_ID", permissionDBEntity.USER_ID);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);
            object objResult = cmd.ExecuteScalar();
            return objResult;
        }

        public object SelectMenuIDByParentMenuID(PermissionDBEntity permissionDBEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectMenuIDByParentMenuID");
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);
            object objResult = cmd.ExecuteScalar();
            return objResult;             
        }

        public object SelectRightByUserAccoutAndModuleID(PermissionDBEntity permissionDBEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectRightByUserAccoutAndModuleID");
            cmd.SetParameterValue("@USER_ID", permissionDBEntity.USER_ID);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);
            object objResult = cmd.ExecuteScalar();
            return objResult;  
        }
        


        #endregion


        #region right_role的使用
        public int InsertRightForRole(PermissionDBEntity permissionDBEntity)
        {
            int rtnResult = 0;
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertRightForRole");
            cmd.SetParameterValue("@Permission_Code", permissionDBEntity.Permission_Code);
            cmd.SetParameterValue("@Permission_Type", permissionDBEntity.Permission_Type);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);
            cmd.SetParameterValue("@Module_Type", permissionDBEntity.Module_Type);
            cmd.SetParameterValue("@Module_Right", permissionDBEntity.Module_Right);
            cmd.SetParameterValue("@Creator", permissionDBEntity.Creator);
            rtnResult = cmd.ExecuteNonQuery();
            return rtnResult;        
        }


        public int UpdateRightForRole(PermissionDBEntity permissionDBEntity)
        {
            int rtnResult = 0;
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateRightForRole");
            cmd.SetParameterValue("@Module_Right", permissionDBEntity.Module_Right);
            cmd.SetParameterValue("@Creator", permissionDBEntity.Creator);
            cmd.SetParameterValue("@Update_Time", permissionDBEntity.Update_Time);
            cmd.SetParameterValue("@Role_ID", permissionDBEntity.Role_ID);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);            
            rtnResult = cmd.ExecuteNonQuery();
            return rtnResult;        
        }

        public int UpdateRightForRoleAndModuleTypeEqual1(PermissionDBEntity permissionDBEntity)
        {         
            int rtnResult = 0;
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateRightForRoleAndModuleTypeEqual1");
            cmd.SetParameterValue("@Module_Right", permissionDBEntity.Module_Right);
            cmd.SetParameterValue("@UpdatedBy", permissionDBEntity.UpdatedBy);
            cmd.SetParameterValue("@Update_Time", permissionDBEntity.Update_Time);
            cmd.SetParameterValue("@Role_ID", permissionDBEntity.Role_ID);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);            
            rtnResult = cmd.ExecuteNonQuery();
            return rtnResult;        
        }


        public int SelectCountByRoleIDAndModuleID(PermissionDBEntity permissionDBEntity)
        {            
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectCountByRoleIDAndModuleID");
            cmd.SetParameterValue("@Role_ID", permissionDBEntity.Role_ID);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);
            object rtnResult = cmd.ExecuteScalar();
            return Convert.ToInt32(rtnResult);
        }

        public object SelectRightByRoleIDAndModuleID(PermissionDBEntity permissionDBEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectRightByRoleIDAndModuleID");
            cmd.SetParameterValue("@Role_ID", permissionDBEntity.Role_ID);
            cmd.SetParameterValue("@Module_ID", permissionDBEntity.Module_ID);
            object rtnResult = cmd.ExecuteScalar();
            return rtnResult;        
        }


        #endregion

    }
}
