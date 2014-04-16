using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class Security_Roles_AddRole : BasePage
{
    RoleEntity _roleEntity = new RoleEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (String.IsNullOrEmpty(txtRoleName.Text.ToString().Trim()))
        {           
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('角色名称不能为空！');", true);
            return;            
        }

        _roleEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _roleEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _roleEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _roleEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _roleEntity.RoleDBEntity = new List<RoleDBEntity>();
        RoleDBEntity roleDBEntity = new RoleDBEntity();
        roleDBEntity.RoleName = txtRoleName.Text.Trim();
        roleDBEntity.RoleCreator = UserSession.Current.UserAccount;
        roleDBEntity.UpdateTime = DateTime.Now.ToString();
        roleDBEntity.CreateTime = DateTime.Now.ToString();


        _roleEntity.RoleDBEntity.Add(roleDBEntity);
        int iResult = RoleBP.Insert(_roleEntity);

        _commonEntity.LogMessages = _roleEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "";
        commonDBEntity.Event_ID = "";



        //string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        //conTent = string.Format(conTent,txtRoleName.Text.Trim());
        //commonDBEntity.Event_Content = conTent;

        if (iResult == 1)//插入成功
        {
            commonDBEntity.Event_Result = Resources.MyGlobal.InsertSuccessText;
            messageContent.InnerHtml = Resources.MyGlobal.InsertSuccessText;
        }
        else if (iResult == 2)//表示该名称已经存在
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("PromptRoleExist").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("PromptRoleExist").ToString();
        }
        else//表示失败
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("PromptAddRoleFaild").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("PromptAddRoleFaild").ToString();
        }

        //_commonEntity.CommonDBEntity.Add(commonDBEntity);
        //CommonBP.InsertEventHistory(_commonEntity);



    }
}