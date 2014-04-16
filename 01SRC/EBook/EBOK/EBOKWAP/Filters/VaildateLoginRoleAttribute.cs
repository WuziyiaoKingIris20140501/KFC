using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using System.Security.Principal;
using System.Web.Security;

namespace EBOK.Filters
{
    public class VaildateLoginRoleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        //如果未登录
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    string redirectOnSuccess = filterContext.HttpContext.Request.RawUrl;
            //    string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
            //    string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
            //    //filterContext.HttpContext.Response.Redirect(loginUrl, true);

            //    filterContext.Result = new RedirectResult(loginUrl);
            //}
            //else
            //{
            //    string logUserAccount = UserSession.Current.UserAccount;
            //    if (string.IsNullOrEmpty(logUserAccount))
            //    {
            //        if (filterContext.HttpContext.Request.Cookies["LoginUserAccount"] != null && filterContext.HttpContext.Request.Cookies["LoginDspName"] != null)
            //        {
            //            string strUserAccount = filterContext.HttpContext.Request.Cookies["LoginUserAccount"].Value;
            //            string strUserDspName = filterContext.HttpContext.Request.Cookies["LoginDspName"].Value;

            //            if (!string.IsNullOrEmpty(strUserAccount))
            //            {
            //                CommonFunction comFun = new CommonFunction();
            //                comFun.setSesssionAndCookies(strUserAccount, HttpUtility.UrlDecode(strUserDspName));
            //            }
            //        }
            //    }
            //    //判断是否存在角色
            //    // 角色包含的可以访问的页面权限是否有权利访问该页面  无权限就跳转到默认无权限页面
            //    //filterContext.Result = new RedirectResult("/Account/warning");
            //}
        }
    }
}
