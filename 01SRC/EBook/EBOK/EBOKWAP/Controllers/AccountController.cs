using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;

using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

using EBOK.Filters;
//using EBOK.Models;

using HotelVp.EBOK.Domain.Biz;
using System.Collections;

namespace EBOK.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            ViewBag.UserName = "";
            if (Request.Cookies["RememberMe"] != null)
            {
                ViewBag.UserName = HttpUtility.UrlDecode(Request.Cookies["RememberMe"].Value, System.Text.Encoding.GetEncoding("gb2312"));
            }
            return View();
        }

        [HttpPost]
        public string Login(string userid, string pwd, string remember)
        {
            ViewBag.LoginErrMsg = "";
            if (String.IsNullOrEmpty(userid) || String.IsNullOrEmpty(pwd))
            {
                return "用户名或密码错误，请从新输入！";
            }

            string domain = ConfigurationManager.AppSettings["LdapAuthenticationDomain"].ToString();
            LdapAuthentication ladAuthBP = new LdapAuthentication();
            ViewBag.ErrorMsg = "";
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

                if ("true".Equals(remember.ToLower()))//再写入cookie
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
                comFun.setSesssionAndCookies(userid, userDspName);
                return "";// RedirectToAction("Index", "Home");
            }

            return "用户名或密码错误，请从新输入！";
        }

        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return View("Login");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
