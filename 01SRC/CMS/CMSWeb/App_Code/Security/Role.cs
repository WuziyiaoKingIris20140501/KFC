using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// Role 的摘要说明
/// </summary>
[DataObject(true)]
public class Role
{
	public Role()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    ///  得到所有角色
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <returns></returns>
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    static public List<RoleData> GetRoles()
    {
        RoleData r = null;
        List<RoleData> roleList = new List<RoleData>();
        string[] ary = Roles.GetAllRoles();

        foreach (string s in ary)
        {
            r = new RoleData();
            r.RoleName = s;

            roleList.Add(r);
        }

        return roleList;
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="roleName">角色名称</param>
    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    static public void DeleteRole(string roleName)
    {
        MembershipUserCollection muc = Membership.GetAllUsers();
        string[] allUserNames = new string[1];

        foreach (MembershipUser mu in muc)
        {
            if (Roles.IsUserInRole(mu.UserName, roleName))
            {
                allUserNames[0] = mu.UserName;
                Roles.RemoveUsersFromRole(allUserNames, roleName);
            }
        }
        Roles.DeleteRole(roleName);
    }
}

/// <summary>
/// 角色的实体类
/// </summary>
public class RoleData
{
    protected string _roleName;

    /// <summary>
    /// 角色名称 关键字
    /// </summary>
    [DataObjectField(true)]
    public string RoleName
    {
        get { return this._roleName; }
        set { this._roleName = value; }
    }
}
