using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using HotelVp.CMS.Domain.DataAccess;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Process;

using Maticsoft.DBUtility;

public class RoleRight
{
        private string _roleid = string.Empty;
        private string _moduleid = string.Empty;
        private string _pageid = string.Empty;
        private string _operator = string.Empty;

        RightDA rightDA = new RightDA();
        public RoleRight(string gid, string mid, string pid, string curuser)
        {
            _roleid = gid;
            _moduleid = mid;
            _pageid = pid;
            _operator = curuser;
        }

        /// <summary>
        /// 根据是否有权限更新权限表
        /// </summary>        
        public void SetNewRight()
        {
            DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringSQL"];
            //string getdatestr = " getdate() ";
            string getdatestr = System.DateTime.Now.ToString();

            List<string> listSql = new List<string>();

            PermissionDBEntity pDBEntity = new PermissionDBEntity();

            foreach (string code in _oldRight.Keys)
            {
                if (_newRight[code].ToString() != _oldRight[code].ToString())
                {
                    //以~结尾的是控件权限 
                    if (code.EndsWith("~"))
                    {
                        string[] temp = _oldRight[code].ToString().Split(new char[] { '^' });
                        string[] newright = _newRight[code].ToString().Split(new char[] { '^' });
                        if (temp[1] == string.Empty)
                        {
                            //listSql.Add("insert into CMS_SYS_PERMISSION(Permission_Code,Permission_Type,Module_ID,Module_Type,Module_Right,Creator) values("
                            //    + _roleid + ",3," + temp[0] + ",3," + newright[1] + ",N'" + _operator + "')");                           

                            pDBEntity.Permission_Code = _roleid;
                            pDBEntity.Permission_Type ="3";
                            pDBEntity.Module_ID =temp[0];
                            pDBEntity.Module_Type = "3";
                            pDBEntity.Module_Right = newright[1];
                            pDBEntity.Creator = _operator;
                            rightDA.InsertRightForRole(pDBEntity);

                        }
                        else
                        {
                            //listSql.Add("update CMS_SYS_PERMISSION set Module_Right=" + newright[1] + ",Creator=N'" + _operator + "',Update_Time=" + getdatestr + " where Permission_Code=" + _roleid
                            //    + " and Permission_Type=3 and Module_ID=" + temp[0] + " and Module_Type=3");                        

                            pDBEntity.Module_Right = newright[1];
                            pDBEntity.Creator = _operator;
                            pDBEntity.Update_Time = getdatestr;
                            pDBEntity.Role_ID = _roleid;
                            pDBEntity.Permission_Type = "3";
                            pDBEntity.Module_ID = temp[0];
                            pDBEntity.Module_Type = "3";
                            rightDA.UpdateRightForRole(pDBEntity);
                        }
                    }
                    else
                    {
                        UseModuleLevel(code, listSql);
                        if (_oldRight[code].ToString() == string.Empty)
                        {
                            //listSql.Add("insert into CMS_SYS_PERMISSION(Permission_Code,Permission_Type,Module_ID,Module_Type,Module_Right,Creator) values("
                            //    + _roleid + ",3," + _moduleid + ",1," + _newRight[code].ToString() + ",N'" + _operator + "')");

                            pDBEntity.Permission_Code = _roleid;
                            pDBEntity.Permission_Type = "3";
                            pDBEntity.Module_ID = _moduleid;
                            pDBEntity.Module_Type = "1";
                            pDBEntity.Module_Right = _newRight[code].ToString();
                            pDBEntity.Creator = _operator;
                            rightDA.InsertRightForRole(pDBEntity);
                            
                        }
                        else
                        {
                            //listSql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdatedBy=N'" + _operator + "',Update_Time=" + getdatestr + " where Permission_Code=" + _roleid
                            //    + " and Permission_Type=3 and Module_ID=" + _moduleid + " and Module_Type=1");                         

                            pDBEntity.Module_Right = _newRight[code].ToString();
                            pDBEntity.UpdatedBy = _operator;
                            pDBEntity.Update_Time = getdatestr;
                            pDBEntity.Role_ID = _roleid;
                            pDBEntity.Permission_Type = "3";
                            pDBEntity.Module_ID = _moduleid;
                            pDBEntity.Module_Type = "1";
                            rightDA.UpdateRightForRoleAndModuleTypeEqual1(pDBEntity);
                        
                        }
                    }
                }
            }
          
            //DbHelperSQL.ExecuteSqlTran(listSql);
        }

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="code"></param>
       /// <param name="sql"></param>
        private void UseModuleLevel(string code, List<string> sql)
        {
            DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringSQL"];

            //string getdatestr = " getdate() ";
            string getdatestr = System.DateTime.Now.ToString();
            string strSql = string.Empty;
            PermissionDBEntity permissionDBEntity = new PermissionDBEntity();

            if (_oldRight[code].ToString() == string.Empty)
            {
                //strSql = "select Module_ID from CMS_SYS_PERMISSION where Module_ID=(select Parent_MenuId from CMS_SYS_MENU where Menu_ID=" + _moduleid + ")";
                //object id = DbHelperSQL.GetSingle(strSql);

                permissionDBEntity.Module_ID = _moduleid;
                object id = rightDA.SelectMenuIDByMenuIDInParentMenuID(permissionDBEntity);

                if (id != null)
                {
                    //strSql = "select count(*) from CMS_SYS_PERMISSION where Permission_Code=" + _roleid
                    //            + " and Permission_Type=3 and Module_ID=" + id.ToString() + " and Module_Type=1"; 
                    //object cnt = DbHelperSQL.GetSingle(strSql);

                    permissionDBEntity.Role_ID = _roleid;
                    permissionDBEntity.Module_ID = id.ToString();
                    object cnt = rightDA.SelectCountByRoleIDAndModuleID(permissionDBEntity);                   

                    if (cnt != null)
                    {
                        if (cnt.ToString() == "0")
                        {
                            //sql.Add("insert into CMS_SYS_PERMISSION(Permission_Code,Permission_Type,Module_ID,Module_Type,Module_Right,Creator) values("
                            //        + _roleid + ",3," + id.ToString() + ",1," + _newRight[code].ToString() + ",N'" + _operator + "')");

                            permissionDBEntity.Permission_Code = _roleid;
                            permissionDBEntity.Permission_Type = "3";
                            permissionDBEntity.Module_ID = id.ToString();
                            permissionDBEntity.Module_Type = "1";
                            permissionDBEntity.Module_Right = _newRight[code].ToString();
                            permissionDBEntity.Creator = _operator;
                            rightDA.InsertRightForRole(permissionDBEntity);

                        }
                        else
                        {
                            //sql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdatedBy=N'" + _operator + "',Update_Time=" + getdatestr + " where Permission_Code=" + _roleid
                            //        + " and Permission_Type=3 and Module_ID=" + id.ToString() + " and Module_Type=1");

                            
                            permissionDBEntity.Module_Right = _newRight[code].ToString();
                            permissionDBEntity.UpdatedBy = _operator;
                            permissionDBEntity.Update_Time = getdatestr;
                            permissionDBEntity.Role_ID = _roleid;
                            permissionDBEntity.Permission_Type = "3";
                            permissionDBEntity.Module_ID = id.ToString();
                            permissionDBEntity.Module_Type = "1";

                            rightDA.UpdateRightForRoleAndModuleTypeEqual1(permissionDBEntity);
                        }
                    }
                }
            }
            else
            {
                if (_pageid == string.Empty)
                {
                    if (_newRight[code].ToString() == "-1")
                    {
                        //strSql = "select Menu_ID from CMS_SYS_MENU where Parent_MenuId=" + _moduleid;
                        //DataSet ds = DbHelperSQL.Query(strSql);
                        //foreach (DataRow dr in ds.Tables[0].Rows)
                        //{
                        //    sql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdateBy=N'" + _operator + "',Update_Time=" + getdatestr + " where Permission_Code=" + _roleid
                        //            + " and Permission_Type=3 and Module_ID=" + dr["Menu_ID"].ToString() + " and Module_Type=1");                           
                        //}


                        permissionDBEntity.Module_ID = _moduleid;
                        object objMenuID = rightDA.SelectMenuIDByParentMenuID(permissionDBEntity);

                        if (objMenuID != null && objMenuID != DBNull.Value)
                        {
                            
                            //sql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdateBy=N'" + _operator + "',Update_Time=" + getdatestr + " where Permission_Code=" + _roleid
                            //        + " and Permission_Type=3 and Module_ID=" + dr["Menu_ID"].ToString() + " and Module_Type=1"); 
 
                            permissionDBEntity.Module_Right = _newRight[code].ToString();
                            permissionDBEntity.UpdatedBy = _operator;
                            permissionDBEntity.Update_Time = getdatestr;
                            permissionDBEntity.Role_ID = _roleid;
                            permissionDBEntity.Permission_Type = "3";
                            permissionDBEntity.Module_ID = objMenuID.ToString();
                            permissionDBEntity.Module_Type = "1";
                            rightDA.UpdateRightForUser(permissionDBEntity);
                            rightDA.UpdateRightForRoleAndModuleTypeEqual1(permissionDBEntity);
                        }
                    }
                }
                else
                {
                    if (_newRight[code].ToString() == "1")
                    {
                        //strSql = "select Menu_ID from CMS_SYS_MENU where Menu_ID=(select Parent_MenuId from CMS_SYS_MENU where Menu_ID=" + _moduleid + ")";
                        //object id = DbHelperSQL.GetSingle(strSql);

                        permissionDBEntity.Module_ID = _moduleid;
                        rightDA.SelectMenuIDByParentMenuID(permissionDBEntity);
                        object id = rightDA.SelectMenuIDByMenuIDInParentMenuID(permissionDBEntity);


                        if (id != null)
                        {
                            //sql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdateBy=N'" + _operator + "',Update_Time=" + getdatestr + " where Permission_Code=" + _roleid
                            //        + " and Permission_Type=3 and Module_ID=" + id.ToString() + " and Module_Type=1");                           

                            permissionDBEntity.Module_Right = _newRight[code].ToString();
                            permissionDBEntity.UpdatedBy = _operator;
                            permissionDBEntity.Update_Time = getdatestr;
                            permissionDBEntity.Role_ID = _roleid;
                            permissionDBEntity.Permission_Type = "3";
                            permissionDBEntity.Module_ID = id.ToString();
                            permissionDBEntity.Module_Type = "1";
                            int i = rightDA.UpdateRightForRoleAndModuleTypeEqual1(permissionDBEntity);
                        }


                    }
                }
            }
        }

        private Hashtable _oldRight;
        /// <summary>
        /// 旧权限
        /// </summary>
        public Hashtable OldRight
        {
            get { return _oldRight; }
            set { _oldRight = value; }
        }
        private Hashtable _newRight;
        /// <summary>
        /// 新权限
        /// </summary>
        public Hashtable NewRight
        {
            get { return _newRight; }
            set { _newRight = value; }
        }


       /// <summary>
       /// 获取模块或页面的权限
       /// </summary>
       /// <returns></returns>
        public string GetModuleOrPageRight()
        {
            //DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringSQL"];
            //string strSql = string.Empty;            
            //strSql = "select Module_Right from CMS_SYS_PERMISSION where Permission_Code=" + _roleid + " and Permission_Type=3 and Module_Type=1 and Module_ID=" + _moduleid;
            //object cnt = DbHelperSQL.GetSingle(strSql);

            PermissionDBEntity permissionDBEntity = new PermissionDBEntity();
            permissionDBEntity.Role_ID = _roleid;
            permissionDBEntity.Module_ID = _moduleid;
            object cnt = rightDA.SelectRightByRoleIDAndModuleID(permissionDBEntity);


            if (cnt == null)
                return string.Empty;
            else
                return cnt.ToString();
        }
 }

