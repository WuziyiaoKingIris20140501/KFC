using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.ComponentModel;

/// <summary>
/// User 的摘要说明
/// </summary>
[DataObject(true)]
public class User
{
	public User()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 获得所有用户
    /// </summary>
    /// <returns></returns>
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public MembershipUserCollection GetMembers()
    {
        MembershipUserCollection muc = Membership.GetAllUsers();
        return muc;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <returns></returns>
    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    public void DeleteMember(string username)
    {
        Membership.DeleteUser(username, true);
    }
}
