using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //    
    }

    public static DataTable dtUserPage;

    public string SiteAppPath
    {
        get
        {
            //当应用程序为站点根目录下时ApplicationPath==/,而当应用程序有虚拟目录时ApplicationPath==/虚拟目录
            return Request.ApplicationPath.TrimEnd('/');

        }
    }

    protected override void OnInitComplete(EventArgs e)
    {
        //如果不是mobile，则跳到提示下载的页面
        if (!User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Login.aspx");
        }

        CommonFunction comFun = new CommonFunction();
        string logUserAccount = UserSession.Current.UserAccount;
        if (string.IsNullOrEmpty(logUserAccount))
        {
            if (Request.Cookies["LoginUserAccount"] != null && Request.Cookies["LoginDspName"] != null)
            {
                string strUserAccount = Request.Cookies["LoginUserAccount"].Value;
                string strUserDspName = Request.Cookies["LoginDspName"].Value;
                string strUserGroups = (Request.Cookies["LoginUserGroups"] != null) ? Request.Cookies["LoginUserGroups"].Value : "";

                if (!string.IsNullOrEmpty(strUserAccount))
                {
                    comFun.setSesssionAndCookies(strUserAccount, HttpUtility.UrlDecode(strUserDspName), HttpUtility.UrlDecode(strUserGroups));
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        //if (!Request.AppRelativeCurrentExecutionFilePath.ToString().ToLower().Contains("default.aspx"))
        //{
        //    if (dtUserPage != null && dtUserPage.Rows.Count > 0 && dtUserPage.Select("Menu_Url ='" + Request.AppRelativeCurrentExecutionFilePath.ToString() + "'").Length <= 0)
        //    {
        //        Response.Redirect("~/WarningPage.aspx");
        //    }
        //}
    }
}