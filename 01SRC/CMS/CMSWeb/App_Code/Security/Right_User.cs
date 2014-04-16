using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using HotelVp.CMS.Domain.DataAccess;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Process;

using Maticsoft.DBUtility;
public class Right_User
{
    private string _userid = string.Empty;
    private string _moduleid = string.Empty;
    private string _pageid = string.Empty;
    private string _operator = string.Empty;

    RightDA rightDA = new RightDA();
    public Right_User(string uid, string mid, string pid, string curuser)
    {
        _userid = uid;
        _moduleid = mid;
        _pageid = pid;
        _operator = curuser;
    }

    /// <summary>
    /// 根据是否有权限更新权限表
    /// </summary>
    /// <param name="haveornot"></param>
    public void SetNewRight()
    {
        DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringSQL"];
        //string getdatestr = " getdate() ";
        string getdatestr = System.DateTime.Now.ToString();
           
        List<string> listSql = new List<string>();
        //ArrayList listSql = new ArrayList();

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
                        //listSql.Add("insert into CMS_SYS_PERMISSION(USER_ID,Permission_Type,Module_ID,Module_Type,Module_Right,Creator) values(N'"
                        //    + _userid + "',1," + temp[0] + ",3," + newright[1] + ",N'" + _operator + "')");

                        pDBEntity.USER_ID = _userid;
                        pDBEntity.Permission_Type = "1";
                        pDBEntity.Module_ID = temp[0];
                        pDBEntity.Module_Type = "3";
                        pDBEntity.Module_Right = newright[1];
                        pDBEntity.Creator = _operator;
                        rightDA.InsertRightForUser(pDBEntity);

                    }
                    else
                    {
                        //listSql.Add("update CMS_SYS_PERMISSION set Module_Right=" + newright[1] + ",UpdatedBy=N'" + _operator + "',Update_Time=" + getdatestr + " where USER_ID=N'" + _userid
                        //    + "' and Permission_Type=1 and Module_ID=" + temp[0] + " and Module_Type=3");

                        pDBEntity.Module_Right = newright[1];
                        pDBEntity.UpdatedBy = _operator;
                        pDBEntity.Update_Time = getdatestr;
                        pDBEntity.USER_ID = _userid;
                        pDBEntity.Permission_Type = "1";
                        pDBEntity.Module_ID = temp[0];
                        pDBEntity.Module_Type = "3";                       
                        rightDA.UpdateRightForUser(pDBEntity);
                    }
                }
                else
                {
                    UseModuleLevel(code, listSql);
                    if (_oldRight[code].ToString() == string.Empty)
                    {
                        //listSql.Add("insert into CMS_SYS_PERMISSION(USER_ID,Permission_Type,Module_ID,Module_Type,Module_Right,Creator) values(N'"
                        //    + _userid + "',1," + _moduleid + ",1," + _newRight[code].ToString() + ",N'" + _operator + "')");

                      

                        pDBEntity.USER_ID = _userid;
                        pDBEntity.Permission_Type = "1";
                        pDBEntity.Module_ID = _moduleid;
                        pDBEntity.Module_Type = "1";
                        pDBEntity.Module_Right = _newRight[code].ToString();
                        pDBEntity.Creator = _operator;
                        rightDA.InsertRightForUser(pDBEntity);

                            
                    }
                    else
                    {
                        //listSql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdatedBy=N'" + _operator + "',Update_Time=" + getdatestr + " where USER_ID=N'" + _userid
                        //    + "' and Permission_Type=1 and Module_ID=" + _moduleid + " and Module_Type=1");

                        pDBEntity.Module_Right = _newRight[code].ToString();
                        pDBEntity.UpdatedBy = _operator;
                        pDBEntity.Update_Time = getdatestr;
                        pDBEntity.USER_ID = _userid;
                        pDBEntity.Permission_Type = "1";
                        pDBEntity.Module_ID = _moduleid;
                        pDBEntity.Module_Type = "1";
                        rightDA.UpdateRightForUserModuleTypeEqual1(pDBEntity);                            
                    }
                }
            }
        }
        
        //DbHelperSQL.ExecuteSqlTran(listSql);
    }

    /// <summary>
    /// 
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
            //strSql = "select Menu_ID from CMS_SYS_MENU where Menu_ID=(select Parent_MenuId from CMS_SYS_MENU where Menu_ID=" + _moduleid + ")";           
            //object id = DbHelperSQL.GetSingle(strSql);

          
            permissionDBEntity.Module_ID = _moduleid;
            object id = rightDA.SelectMenuIDByMenuIDInParentMenuID(permissionDBEntity);

            if (id != null)
            {
                //strSql = "select count(*) from CMS_SYS_PERMISSION where USER_ID=N'" + _userid  + "' and Permission_Type=1 and Module_ID=" + id.ToString() + " and Module_Type=1";
                //object cnt = DbHelperSQL.GetSingle(strSql);

                permissionDBEntity.USER_ID = _userid;
                permissionDBEntity.Module_ID =id.ToString();
                object cnt = rightDA.SelectCountByUserIDAndModuleID(permissionDBEntity);

                if (cnt != null)
                {
                    if (Convert.ToInt32( cnt) == 0)
                    {
                        //sql.Add("insert into CMS_SYS_PERMISSION(USER_ID,Permission_Type,Module_ID,Module_Type,Module_Right,Creator) values(N'"
                        //        + _userid + "',1," + id.ToString() + ",1," + _newRight[code].ToString() + ",N'" + _operator + "')");

                        permissionDBEntity.USER_ID = _userid;
                        permissionDBEntity.Permission_Type = "1";
                        permissionDBEntity.Module_ID = id.ToString();
                        permissionDBEntity.Module_Type = "1";
                        permissionDBEntity.Module_Right = _newRight[code].ToString();
                        permissionDBEntity.Creator = _operator;
                        int i=  rightDA.InsertRightForUser(permissionDBEntity);


                    }
                    else if (_newRight[code].ToString() == "1")
                    {
                        //sql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdatedBy=N'" + _operator + "',Update_Time=" + getdatestr + " where USER_ID=N'" + _userid
                        //        + "' and Permission_Type=1 and Module_ID=" + id.ToString() + " and Module_Type=1");

                        permissionDBEntity.Module_Right = _newRight[code].ToString();
                        permissionDBEntity.UpdatedBy = _operator;
                        permissionDBEntity.Update_Time = getdatestr;
                        permissionDBEntity.USER_ID = _userid;
                        permissionDBEntity.Permission_Type = "1";
                        permissionDBEntity.Module_ID = id.ToString();
                        permissionDBEntity.Module_Type = "1";
                        int i = rightDA.UpdateRightForUserModuleTypeEqual1(permissionDBEntity);

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
                    //strSql = "select Menu_ID from CMS_SYS_MENU where Parent_MenuID=" + _moduleid;
                    //DataSet ds = DbHelperSQL.Query(strSql);
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    sql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdatedBy=N'" + _operator + "',Update_Time=" + getdatestr + " where USER_ID=N'" + _userid
                    //            + "' and Permission_Type=1 and Module_ID=" + dr["Menu_ID"].ToString() + " and Module_Type=1");

                    //}

                    permissionDBEntity.Module_ID = _moduleid;
                    object objMenuID = rightDA.SelectMenuIDByParentMenuID(permissionDBEntity);                   

                    if (objMenuID != null && objMenuID != DBNull.Value)
                    {
                        //sql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdatedBy=N'" + _operator + "',Update_Time=" + getdatestr + " where USER_ID=N'" + _userid
                        //           + "' and Permission_Type=1 and Module_ID=" + dr["Menu_ID"].ToString() + " and Module_Type=1");
                           

                        permissionDBEntity.Module_Right = _newRight[code].ToString();
                        permissionDBEntity.UpdatedBy = _operator;
                        permissionDBEntity.Update_Time = getdatestr;
                        permissionDBEntity.USER_ID = _userid;
                        permissionDBEntity.Permission_Type = "1";
                        permissionDBEntity.Module_ID = objMenuID.ToString();
                        permissionDBEntity.Module_Type = "1";
                        rightDA.UpdateRightForUser(permissionDBEntity);

                    }


                }
            }
            else
            {
                if (_newRight[code].ToString() == "1")
                {
                    //strSql = "select Menu_ID from CMS_SYS_MENU where Menu_ID=(select Parent_MenuID from CMS_SYS_MENU where Menu_Id=" + _moduleid + ")";
                    //object id = DbHelperSQL.GetSingle(strSql);

                    permissionDBEntity.Module_ID = _moduleid;
                    rightDA.SelectMenuIDByParentMenuID(permissionDBEntity);
                    object id = rightDA.SelectMenuIDByMenuIDInParentMenuID(permissionDBEntity);

                    if (id != null)
                    {
                        //sql.Add("update CMS_SYS_PERMISSION set Module_Right=" + _newRight[code].ToString() + ",UpdatedBy=N'" + _operator + "',Update_Time=" + getdatestr + " where USER_ID=N'" + _userid
                        //        + "' and Permission_Type=1 and Module_ID=" + id.ToString() + " and Module_Type=1");

                        permissionDBEntity.Module_Right = _newRight[code].ToString();
                        permissionDBEntity.UpdatedBy = _operator;
                        permissionDBEntity.Update_Time = getdatestr;
                        permissionDBEntity.USER_ID = _userid;
                        permissionDBEntity.Permission_Type = "1";
                        permissionDBEntity.Module_ID = id.ToString();
                        permissionDBEntity.Module_Type = "1";
                        int i = rightDA.UpdateRightForUserModuleTypeEqual1(permissionDBEntity);
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


  
    public string GetModuleOrPageRight()
    {
        //DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringSQL"];
        //string strSql = string.Empty;
        //strSql = "select Module_Right from CMS_SYS_PERMISSION where USER_ID=N'" + _userid + "' and Permission_Type=1 and Module_Type=1 and Module_ID=" + _moduleid;
        //object cnt = DbHelperSQL.GetSingle(strSql);

        PermissionDBEntity permissionDBEntity = new PermissionDBEntity();
        permissionDBEntity.USER_ID = _userid;
        permissionDBEntity.Module_ID = _moduleid;
        object cnt = rightDA.SelectRightByUserAccoutAndModuleID(permissionDBEntity);

        if (cnt == null)
            return string.Empty;
        else
            return cnt.ToString();
    }
}

