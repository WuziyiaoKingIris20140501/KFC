using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using HotelVp.Common.DataAccess;

public partial class Security_Permissions_ModifyPwd : BasePage
{

   public string  PromptInpuAccount = string.Empty;  //请输入账户！
   public string  PromptInputOldPwd= string.Empty;	//请输入旧的密码！
   public string  PromptInputNewPwd= string.Empty;	//请输入新密码！
   public string  PromptInputConfirmPwd= string.Empty;	//请输入确认密码！
   public string  PromptPwdNotConsistent= string.Empty;	//新密码和确认密码输入不一致！
   public string  PromptOldPwdError= string.Empty;	//输入的旧密码不正确！
   public string  PromptUpdatePwdSuccess= string.Empty;	//密码修改成功！
   public string  PromptUpdatePwdFaild= string.Empty;	//密码修改错误！


    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            this.txtUserID.Text = UserSession.Current.UserAccount;           
        }
        PromptInpuAccount = GetLocalResourceObject("PromptInpuAccount").ToString();
        PromptInputOldPwd = GetLocalResourceObject("PromptInputOldPwd").ToString(); ;
        PromptInputNewPwd = GetLocalResourceObject("PromptInputNewPwd").ToString();
        PromptInputConfirmPwd = GetLocalResourceObject("PromptInputConfirmPwd").ToString();
        PromptPwdNotConsistent = GetLocalResourceObject("PromptPwdNotConsistent").ToString();
        PromptOldPwdError = GetLocalResourceObject("PromptOldPwdError").ToString();
        PromptUpdatePwdSuccess = GetLocalResourceObject("PromptUpdatePwdSuccess").ToString();
        PromptUpdatePwdFaild = GetLocalResourceObject("PromptUpdatePwdFaild").ToString();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
       string userid = this.txtUserID.Text;
       string oldPassword = this.txtOldPassword.Text;        

       CommonFunction comFun = new CommonFunction();
       oldPassword = comFun.setMD5Password(oldPassword);

       string resultDspName = comFun.checkLogin(userid, oldPassword);//登录人显示名
       if (string.IsNullOrEmpty(resultDspName) == true)
       {
           this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "oldpassword", "alert('" + PromptOldPwdError + "');", true);
            return;
       }

       string newPassword = this.txtNewPassword.Text.Trim();
       string confimrPassword = this.txtConfirmPassword.Text.Trim();

       newPassword = comFun.setMD5Password(newPassword);

       int i = updatePassword(userid, newPassword);
       if (i == 1)
       {
           this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "success", "alert('" + PromptUpdatePwdSuccess + "');", true);
       }
       else
       {
           this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "fail", "alert('" + PromptUpdatePwdFaild + "');", true);
           return;
       }
    }

    private int updatePassword(string userAccount, string pwd)
    {
        int result = 0;
        try
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateCmsUserPasswordByID");
            cmd.SetParameterValue("@USER_Account", userAccount);
            cmd.SetParameterValue("@User_Pwd", pwd);
            result = cmd.ExecuteNonQuery();
            return result;
        }
        catch
        {
            return result;
        }

    }

}