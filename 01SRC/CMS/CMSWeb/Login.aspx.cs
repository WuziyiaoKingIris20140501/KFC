using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;

using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;
using HotelVp.CMS.Domain.Process.Common;
using System.Collections;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["RememberMe"] != null)
            {
                string UserName = HttpUtility.UrlDecode(Request.Cookies["RememberMe"].Value, System.Text.Encoding.GetEncoding("gb2312"));

                if (!String.IsNullOrEmpty(UserName))
                {
                    this.txtUserID.Text = UserName;
                    chkRemember.Checked = true;
                }
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.Application["Administrator"].ToString().Contains(this.txtUserID.Text.Trim().ToLower()))
        {
            Login_KFCUser();
        }
        else
        {
            string strLoginType = ConfigurationManager.AppSettings["LoginType"].ToString();
            if ("1".Equals(strLoginType))
            {
                Login_ADUser();
            }
            else
            {
                Login_KFCUser();
            }
        }
    }

    private void Login_ADUser()
    {
        string userid = this.txtUserID.Text.Trim().ToLower();//登录人账户
        string pwd = this.txtPwd.Text.Trim();//登录人密码

        if (String.IsNullOrEmpty(userid) || String.IsNullOrEmpty(pwd))
        {
            this.lblRegMsgPopup.Text = "用户名或密码错误，请从新输入！";
            return;
        }

        string domain = ConfigurationManager.AppSettings["LdapAuthenticationDomain"].ToString();
        LdapAuthentication ladAuthBP = new LdapAuthentication();

        if (ladAuthBP.IsAuthenticated(domain, userid, pwd) && ladAuthBP.GetStatus())
        {
            Hashtable userInfo = ladAuthBP.GetUserInfo();
            string userDspName = (userInfo.Count > 0) ? userInfo["cn"].ToString() : "";
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, "LoginCookieInfo", DateTime.Now, DateTime.Now.AddMinutes(60), false, userid); // User data 
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket); //加密 
            //   存入Cookie 
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = authTicket.Expiration;
            Response.Cookies.Add(authCookie);

            if (chkRemember.Checked)//再写入cookie
            {
                if (Request.Cookies["RememberMe"] == null || String.IsNullOrEmpty(Response.Cookies["RememberMe"].Value))
                {
                    Response.Cookies["RememberMe"].Value = HttpUtility.UrlEncode(userid, System.Text.Encoding.GetEncoding("gb2312"));
                    Response.Cookies["RememberMe"].Expires = DateTime.Now.AddMonths(1);
                }
            }
            else
            {
                if (Response.Cookies["RememberMe"] != null) Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);//删除
            }
            CommonFunction comFun = new CommonFunction();
            comFun.setSesssionAndCookies(userid, userDspName, ladAuthBP.GetGroups());

            this.Response.Redirect("~/Default.aspx");
        }

        this.lblRegMsgPopup.Text = "用户名或密码错误，请从新输入！";
        return;
    }

    private void Login_KFCUser()
    {
        //string strUserAccount = this.txtUserID.Text.Trim();//登录人账户
        //string strPwd = this.txtPwd.Text.Trim();//登录人密码
        //CommonFunction comFun = new CommonFunction();

        //strPwd = comFun.setMD5Password(strPwd);//md5加密

        //string resultDspName = comFun.checkLogin(strUserAccount, strPwd);//登录人显示名
        //if (!string.IsNullOrEmpty(resultDspName))
        //{           
        //    comFun.setSesssionAndCookies(strUserAccount,resultDspName); 
        //    this.Response.Redirect("~/Default.aspx");
        //}
        //else
        //{           
        //    this.lblRegMsgPopup.Text = "登录失败!";
        //    return;
        //}

        string strUserAccount = this.txtUserID.Text.Trim();//登录人账户
        string strPwd = this.txtPwd.Text.Trim();//登录人密码
        CommonFunction comFun = new CommonFunction();
        strPwd = comFun.setMD5Password(strPwd);//md5加密

        string strTemp = comFun.checkLogin(strUserAccount, strPwd);
        string resultDspName = strTemp.Split(',')[1];//登录人显示名
        strUserAccount = strTemp.Split(',')[0];
        if (!string.IsNullOrEmpty(resultDspName))
        {
            //System.Web.Security.FormsAuthentication.SetAuthCookie(strUserAccount, false);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, "LoginCookieInfo", DateTime.Now, DateTime.Now.AddMinutes(60), false, strUserAccount); // User data 
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket); //加密 
            //   存入Cookie 
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = authTicket.Expiration;
            Response.Cookies.Add(authCookie);

            if (chkRemember.Checked)//再写入cookie
            {
                if (Request.Cookies["RememberMe"] == null || String.IsNullOrEmpty(Response.Cookies["RememberMe"].Value))
                {
                    Response.Cookies["RememberMe"].Value = HttpUtility.UrlEncode(strUserAccount, System.Text.Encoding.GetEncoding("gb2312"));
                    Response.Cookies["RememberMe"].Expires = DateTime.Now.AddMonths(1);
                }
            }
            else
            {
                if (Response.Cookies["RememberMe"] != null) Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);//删除
            }

            comFun.setSesssionAndCookies(strUserAccount, resultDspName, "");

            this.Response.Redirect("~/Default.aspx");
            //if (Request.QueryString.ToString().Contains("ReturnUrl") && !String.IsNullOrEmpty(Request.QueryString["ReturnUrl"].ToString()))
            //{
            //    this.Response.Redirect(Request.QueryString["ReturnUrl"].ToString());
            //}
            //else
            //{
            //    this.Response.Redirect("~/Default.aspx");
            //}
        }
        else
        {
            this.lblRegMsgPopup.Text = "登录失败!";
            return;
        }
    }
    /// <summary>
    /// check login information is true，返回user display name.
    /// </summary>
    /// <param name="userAccount"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    //private string checkLogin(string userAccount,string pwd)
    //{
    //    string getDspName = string.Empty;
    //    try
    //    {
    //        DataCommand cmd = DataCommandManager.GetDataCommand("UserLoginByAccount");
    //        cmd.SetParameterValue("@USER_Account", userAccount);
    //        cmd.SetParameterValue("@User_Pwd", pwd);
    //        System.Data.DataSet dsResult = cmd.ExecuteDataSet();
    //        DataTable dtUser = dsResult.Tables[0];
    //        if (dtUser.Rows.Count > 0)
    //        {
    //            getDspName = dtUser.Rows[0]["User_DspName"].ToString();//用户登录名
    //            return getDspName;
    //        }
    //        else
    //        {
    //            return getDspName;
    //        }
    //    }
    //    catch
    //    {
    //        return getDspName;
    //    }
    
    //}
}