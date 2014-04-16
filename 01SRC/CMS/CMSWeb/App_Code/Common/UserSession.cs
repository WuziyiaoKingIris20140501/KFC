using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;

/// <summary>
/// UserSession 的摘要说明
/// </summary>
[Serializable]
public class UserSession
{   
    private string _loginPwd = string.Empty; 
    private string _userDspName = string.Empty;
    private string _roleCode = string.Empty;
    private string _userHRID = string.Empty;
    private string _userAccount = string.Empty;
    private string _userIP = string.Empty;
    private string _userHostName = string.Empty;
    private string _userGroups = string.Empty;

    /// <summary>
    /// 用户账号
    /// </summary>
    public string UserAccount
    {
        get
        {
            return _userAccount;
        }
        set
        {
            _userAccount = value;
        }
    }

    public string UserGroups
    {
        get
        {
            return _userGroups;
        }
        set
        {
            _userGroups = value;
        }
    } 

    /// <summary>
    /// 登录密码
    /// </summary>
    public string LoginPwd
    {
        get
        {
            return _loginPwd;
        }
        set
        {
            _loginPwd = value;
        }
    }

    /// <summary>
    /// HR ID
    /// </summary>
    public string UserHRID
    {
        get
        {
            return _userHRID;
        }
        set
        {
            _userHRID = value;
        }
    }

    /// <summary>
    /// user name
    /// </summary>
    public string UserDspName
    {
        get
        {
            return _userDspName;
        }
        set
        {
            _userDspName = value;
        }
    }

     
    //角色代码
    public string RoleCode
    {
        get
        {
            return _roleCode;
        }
        set
        {
            _roleCode = value;
        }
    }

    //用户IP地址
    public string UserIP
    {
        get
        {
            return _userIP;
        }
        set
        {
            _userIP = value;
        }
    }

    //用户的主机名
    public string UserHostName
    {
        get
        {
            return _userHostName;
        }
        set
        {
            _userHostName = value;
        }
    }
    

    public UserSession()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //s

    }

    public static UserSession Current
    {
        get
        {
            return (UserSession)HttpContext.Current.Session["LoginSession"];
        }
    }

}


