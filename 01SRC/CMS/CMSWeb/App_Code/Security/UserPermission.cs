using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.CMS.Domain.Process;
//using HotelVp.CMS.Domain.Resource;


using Maticsoft.DBUtility;
using System.Data.SqlClient;


 public enum ModuleLevel
    {
        All,
        First,
        Second,
        Third,
        Fourth,
        Fifth
    }

/// <summary>
///UserPermission 的摘要说明
/// </summary>
public class UserPermission
{
    //
		//TODO: 在此处添加构造函数逻辑
		//

        private string _userid = string.Empty; 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        public UserPermission(string userid)
        {
            _userid = userid;
        }

        /// <summary>
        /// 根据模块级别获取当前用户能够访问的模块
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        
//        public List<SYS_Module> GetModules(ModuleLevel level)
//        {
//            List<SYS_Module> modules = new List<SYS_Module>();
//            string tempWhere = string.Empty;
//            if (level != ModuleLevel.All)
//            {
//                tempWhere = " and b.ObjectLevel=" + ((int)level).ToString();
//            }
//            string strSql = @"select distinct a.ModuleID as ID,a.CompetenceType,a.ObjectRight,b.ParentNodeId,b.TextCH,b.TextEN,b.TextTW,b.TextJP,b.Target,c.PageLink as Url,b.ObjectLevel from (
//                                --查询用户帐号下的权限
//                                select ModuleID,CompetenceType,ModuleType,ObjectRight from SYS_PERMISSION_Competence a,SYS_BPS_USERS b where a.USER_ID=b.USER_Account
//                                and ModuleType=1 and a.CompetenceType=1 and a.USER_ID='" + _userid + @"'
//                                union 
//                                --查询用户所有角色的权限
//                                select ModuleID,CompetenceType,ModuleType,ObjectRight from SYS_PERMISSION_Competence a,
//                                (select distinct Role_ID from VW_RoleUser where UserID='" + _userid + @"') b 
//                                where a.CompetenceCode=b.Role_ID and a.CompetenceType=3 and a.ModuleType=1
//                                union
//                                --查询用户所有机构的权限
//                                select ModuleID,CompetenceType,ModuleType,ObjectRight from SYS_PERMISSION_Competence a,
//                                (select distinct GroupID from VW_GPUser where UserID='" + _userid + @"') b 
//                                where a.CompetenceCode=b.GroupID  and a.ModuleType=1 and a.CompetenceType=2) a,SYS_BPS_MENU b left join SYS_PERMISSION_MODULES c
//                                on b.PageID=c.PageID where a.ModuleID=b.ID and b.Show=0 " + tempWhere;

//            DataSet ds = DbHelper.Query(strSql);
//            foreach (DataRow dr in ds.Tables[0].Rows)
//            {
//                modules.Add(new SYS_Module(dr["ModuleCode"].ToString(), dr["ModuleName"].ToString(), dr["PageLink"].ToString(), int.Parse(dr["ModuleLevel"].ToString()), dr["ParentModule"].ToString()));
//            }
//            return modules;
//        }

        public DataTable GetModulesByADWithRight(ModuleLevel level)
        {
            string strUserGroups = UserSession.Current.UserGroups;
            string tempWhere = string.Empty;
            if (!String.IsNullOrEmpty(strUserGroups))
            {
                string[] strGroups = strUserGroups.Split(new char[]{'|'}, StringSplitOptions.RemoveEmptyEntries);
                string strWhere = string.Empty;
                foreach (string strGroup in strGroups)
                {
                    strWhere = strWhere + "'" + strGroup + "',";
                }
                strWhere = strWhere.TrimEnd(',');
                tempWhere = " and Role_Name IN (" + strWhere + ")";
            }
            else
            {
                tempWhere = " and Role_Name='nothing'";
            }

            RightDA rightDA = new RightDA();
            DataSet ds = rightDA.SelectModulesRightListByUserADAccount(tempWhere);
            DataTable dt = ds.Tables[0];
            return dt;

//            string strSql = @"select distinct a.Module_ID as Menu_ID,a.Module_Right,b.Parent_MenuId,b.Menu_Name,b.Menu_Target,b.Menu_OrderID,b.Menu_Url,b.Menu_Level from (
//            select Module_ID,Module_Type,Module_Right from CMS_SYS_PERMISSION a,
//            (select distinct Role_ID from [CMS_SYS_ROLES] where IS_AD = '1' " + tempWhere + @") b 
//            where a.Permission_Code=b.Role_ID and a.Module_Type=1 and a.Permission_Type=3) a,CMS_SYS_MENU b where a.Module_ID=b.Menu_ID and b.Menu_Show=1 and b.Menu_Limit=1
//            union 
//            select b.Menu_ID,1,b.Parent_MenuId,b.Menu_Name,b.Menu_Target,b.Menu_OrderID,b.Menu_Url,b.Menu_Level
//            from CMS_SYS_MENU b  where b.Menu_Limit=0 and b.Menu_Show=1  order by b.Menu_OrderID";

//            DataSet ds = Query(strSql);
//            DataTable dt = ds.Tables[0];
//            return dt;
        }

        //private DataSet Query(string SQLString)
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringSQL"];
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        DataSet ds = new DataSet();
        //        try
        //        {
        //            connection.Open();
        //            SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
        //            command.Fill(ds, "ds");
        //        }
        //        catch (System.Data.SqlClient.SqlException ex)
        //        {
        //            connection.Close();
        //            throw new Exception(ex.Message);
        //        }
        //        return ds;
        //    }
        //}

        /// <summary>
        /// 根据模块级别获取当前用户能够访问的模块
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns> 
        public DataTable GetModulesByLevelWithRight(ModuleLevel level)
        {
            string tempWhere = string.Empty;
            if (level != ModuleLevel.All)
            {
                tempWhere = " and b.Menu_Level=" + ((int)level).ToString();
            }

//            string strSql = @"select distinct a.Module_ID as Menu_ID,a.Module_Right,b.Parent_MenuId,b.Menu_Name,b.Menu_Target,b.Menu_OrderID,b.Menu_Url,b.Menu_Level from (
//                                --查询用户帐号下的权限
//                                select Module_ID,Module_Type,Module_Right from CMS_SYS_PERMISSION a,CMS_SYS_USERS b where a.USER_ID=b.USER_Account
//                                and Module_Type=1 and a.Permission_Type=1 and a.USER_ID='" + _userid + @"'
//                                union 
//                                --查询用户所有角色的权限
//                                select Module_ID,Module_Type,Module_Right from CMS_SYS_PERMISSION a,
//                                (select distinct Role_ID from CMS_SYS_USER_ELEMENT where User_Account='" + _userid + @"') b 
//                                where a.Permission_Code=b.Role_ID and a.Module_Type=1 and a.Permission_Type=3"+
                             
//                               @" ) a,CMS_SYS_MENU b where a.Module_ID=b.Menu_ID and b.Menu_Show=1 and b.Menu_Limit=1 " + tempWhere + @"
//                                union 
//                                select b.Menu_ID,1,b.Parent_MenuId,b.Menu_Name,b.Menu_Target,b.Menu_OrderID,b.Menu_Url,b.Menu_Level
//                                from CMS_SYS_MENU b  where b.Menu_Limit=0 and b.Menu_Show=1 " + tempWhere;

//            DataSet ds = DbHelperSQL.Query(strSql);
//            DataTable dt = ds.Tables[0];


            RightDA rightDA = new RightDA();
            DataSet ds =rightDA.SelectModulesRightListByUserAccount(tempWhere, _userid);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据模块编号获取该模块下的下级模块
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>       
//        public List<SYS_Module> GetChildModules(string code)
//        {
//            List<SYS_Module> modules = new List<SYS_Module>();
//            string strSql = @"select distinct a.ModuleID as ID,a.CompetenceType,a.ObjectRight,b.ParentNodeId,b.TextCH,b.TextEN,b.TextTW,b.TextJP,b.Target,b.OrderID,c.PageLink as Url,b.ObjectLevel from (
//                                --查询用户帐号下的权限
//                                select ModuleID,CompetenceType,ModuleType,ObjectRight from SYS_PERMISSION_Competence a,SYS_BPS_USERS b where a.USER_ID=b.USER_Account
//                                and ModuleType=1 and a.CompetenceType=1 and a.USER_ID='" + _userid + @"'
//                                union 
//                                --查询用户所有角色的权限
//                                select ModuleID,CompetenceType,ModuleType,ObjectRight from SYS_PERMISSION_Competence a,
//                                (select distinct Role_ID from VW_RoleUser where UserID='" + _userid + @"') b 
//                                where a.CompetenceCode=b.Role_ID and a.CompetenceType=3 and a.ModuleType=1
//                                union
//                                --查询用户所有机构的权限
//                                select ModuleID,CompetenceType,ModuleType,ObjectRight from SYS_PERMISSION_Competence a,
//                                (select distinct GroupID from VW_GPUser where UserID='" + _userid + @"') b 
//                                where a.CompetenceCode=b.GroupID  and a.CompetenceType=2 and a.ModuleType=1) a,SYS_BPS_MENU b left join SYS_PERMISSION_MODULES c
//                                on b.PageID=c.PageID where a.ModuleID=b.ID and b.Show=0 and b.ParentNodeId=" + code;

//            DataSet ds = DbHelper.Query(strSql);
//            foreach (DataRow dr in ds.Tables[0].Rows)
//            {
//                modules.Add(new SYS_Module(dr["ID"].ToString(), dr["TextCH"].ToString(), dr["Url"].ToString(), int.Parse(dr["ObjectLevel"].ToString()), dr["ParentNodeId"].ToString()));
//            }
//            return modules;
//        }

        /// <summary>
        /// 根据模块编号获取该模块下的下级模块
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
//        public DataSet GetChildModulesByID(string code)
//        {
//            string strSql = @"select distinct a.ModuleID as ID,a.Permission_Type,a.Module_Right,b.Parent_MenuId,b.Menu_Name,b.TextEN,b.TextTW,b.TextJP,b.Menu_Target,b.Menu_OrderID,c.PageLink as Url,b.Menu_Level,b.Menu_Level from (
//                                --查询用户帐号下的权限
//                                select ModuleID,Permission_Type,Module_Type,Module_Right from CMS_SYS_PERMISSION a,CMS_SYS_USERS b where a.USER_ID=b.USER_Account
//                                and Module_Type=1 and a.Permission_Type=1 and a.USER_ID='" + _userid + @"'
//                                union 
//                                --查询用户所有角色的权限
//                                select ModuleID,Permission_Type,Module_Type,Module_Right from CMS_SYS_PERMISSION a,
//                                (select distinct Role_ID from VW_RoleUser where UserID='" + _userid + @"') b 
//                                where a.CompetenceCode=b.Role_ID and a.Module_Type=1 and a.Permission_Type=3
//                                union
//                                --查询用户所有机构的权限
//                                select ModuleID,Permission_Type,Module_Type,Module_Right from CMS_SYS_PERMISSION a,
//                                (select distinct GroupID from VW_GPUser where UserID='" + _userid + @"') b 
//                                where a.CompetenceCode=b.GroupID  and a.Module_Type=1 and a.Permission_Type=2) a,CMS_SYS_MENU b left join SYS_PERMISSION_MODULES c
//                                on b.PageID=c.PageID where a.ModuleID=b.ID and b.Show=0 and b.Parent_MenuId=" + code;

//            return DbHelperSQL.Query(strSql);
//        }

    
        /// <summary>
        /// 管理员sa默认全部显示,设置为该菜单显示的。
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static DataTable GetModulesByLevel(ModuleLevel level)
        {
            string tempWhere = string.Empty;
            if (level != ModuleLevel.All)
            {                
                tempWhere = " and Menu_Level=" + ((int)level).ToString();
            }
            //新增左侧图标，并设置在CMS_SYS_MENU表中的MenuName
            //string strSql = @"select Menu_ID,Parent_MenuId ,Menu_Name,Menu_Url,Menu_Target,Menu_OrderID,Menu_Type,Menu_Level,Menu_Limit  from CMS_SYS_MENU where Menu_Show=1" + tempWhere;                            
            //return DbHelperSQL.Query(strSql).Tables[0];

            RightDA rightDA = new RightDA();
            //DataSet ds = rightDA.SelectModulesByLevel(tempWhere);

            DataSet ds = rightDA.SelectModulesByAdmin(tempWhere);

            DataTable dt = ds.Tables[0];
            return dt;
        }

        public static DataTable GetModulesByLevelByAll(ModuleLevel level)
        {
            string tempWhere = string.Empty;
            if (level != ModuleLevel.All)
            {
                tempWhere = " and Menu_Level=" + ((int)level).ToString();
            }
            //新增左侧图标，并设置在CMS_SYS_MENU表中的MenuName
            //string strSql = @"select Menu_ID,Parent_MenuId ,Menu_Name,Menu_Url,Menu_Target,Menu_OrderID,Menu_Type,Menu_Level,Menu_Limit  from CMS_SYS_MENU where Menu_Show=1" + tempWhere;                            
            //return DbHelperSQL.Query(strSql).Tables[0];

            RightDA rightDA = new RightDA();
            DataSet ds = rightDA.SelectModulesByLevel(tempWhere);

            //DataSet ds = rightDA.SelectModulesByAdmin(tempWhere);

            DataTable dt = ds.Tables[0];
            return dt;
        }


        //查询出有设定限制的菜单
        public static DataTable GetModulesWithLimit()
        {         
            //string strSql = @"select Menu_ID,Parent_MenuId ,Menu_Name,Menu_Url,Menu_Target,Menu_OrderID,Menu_Type,Menu_Level,Menu_Limit  from CMS_SYS_MENU where Menu_Show=1 and Menu_Limit=1"; 
            //return DbHelperSQL.Query(strSql).Tables[0];

            RightDA rightDA = new RightDA();
            DataSet ds = rightDA.SelectModulesWithLimit();
            DataTable dt = ds.Tables[0];
            return dt;

        }

}