using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;

public partial class Security_Users_AddCmsUser : BasePage
{
    protected string type;
    protected string p_UserAccount;
    protected void Page_Load(object sender, EventArgs e)
    {
        string getType =Request.QueryString["type"];
        this.type = getType;
        if (!string.IsNullOrEmpty(Request.QueryString["account"]))
        {
            this.p_UserAccount = Server.UrlDecode(Request.QueryString["account"].ToString());           
        }

        this.type = Request.QueryString["type"].ToString();

        //this.txtBirthday.Attributes.Add("readOnly", "true");
        //this.txtBirthday.Attributes.Add("style", "vertical-align:middle");
        //this.txtJoinData.Attributes.Add("readOnly", "true");
        //this.txtJoinData.Attributes.Add("style", "vertical-align:middle");
        hidActionType.Value = type;
        if (!IsPostBack)
        {
            switch (this.type)
            {
                case "add":
                     tdRoL.Style.Add("display", "");
                    tdRoV.Style.Add("display", "");
                    break;
                case "update":
                    this.LoadOldInfo(this.p_UserAccount);
                    break;               
            }

            BindDDp();
        }
    }

    private void BindDDp()
    {
        DataCommand cmd = DataCommandManager.GetDataCommand("SelectRolesList");
        DataSet dsResult = cmd.ExecuteDataSet();

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            DataRow dr0 = dsResult.Tables[0].NewRow();
            dr0["Role_Name"] = DBNull.Value;
            dr0["Role_ID"] = DBNull.Value;
            dsResult.Tables[0].Rows.InsertAt(dr0, 0);
        }

        ddpRole.DataTextField = "Role_Name";
        ddpRole.DataValueField = "Role_ID";
        ddpRole.DataSource = dsResult;
        ddpRole.DataBind();
        
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        CommonFunction comFun = new CommonFunction();
        string strUserAccount = CommonFunction.StringFilter(this.txtUserAccount.Text.Trim());
        string strUserName = CommonFunction.StringFilter(this.txtUserName.Text.Trim());
        string strPwd = CommonFunction.StringFilter(this.txtPwd.Text.Trim());
        strPwd =  comFun.setMD5Password(strPwd);

        string strEmail = CommonFunction.StringFilter(this.txtEmail.Text.Trim());
        string strHRID = CommonFunction.StringFilter(this.txtHRID.Text.Trim());
        string strTel = CommonFunction.StringFilter(this.txtTel.Text.Trim());
        string strTitle = "";// CommonFunction.StringFilter(this.txtTitle.Text.Trim());
        string strUserManager = CommonFunction.StringFilter(this.txtUserManager.Text.Trim());

        if (String.IsNullOrEmpty(strTel))
        {
            
        }

        try
        {
            switch (this.type)
            {
                case "add":

                    addUser(strUserAccount, strUserName, strEmail, strPwd, strHRID, strTel, strTitle, strUserManager); 
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + GetLocalResourceObject("PromptAddSuccess").ToString() + "');window.location.href='UserManage.aspx'", true);                    

                    break;

                case "update":
                    updateUser(strUserAccount, strUserName, strEmail, strPwd, strHRID, strTel, strTitle, strUserManager);
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + GetLocalResourceObject("PromptUpdateSuccess").ToString() + "');window.location.href='UserManage.aspx'", true);             
                    break;
            }
           // BaseUserInfo.GetUsers();

        }
        catch
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + GetLocalResourceObject("PromptAddFailure").ToString() + "');", true);
            return;
        }        
    }

    protected void back_Click(object sender, EventArgs e)
    {
        string strURL = "UserManage.aspx";
        this.Response.Redirect(strURL);
    }

    private void setEmpty()
    {
        this.txtUserAccount.Text = "";
        this.txtUserName.Text = "";
        this.txtPwd.Text = "";
        this.txtEmail.Text = "";
        this.txtHRID.Text = "";
        this.txtTel.Text = "";
        //this.txtTitle.Text = "";
    
    }

    //增加新用户，执行方法
    private void addUser(string strUserAccount, string strUserName, string strEmail, string strPwd, string strHRID, string strTel, string strTitle, string strUserManager)
    {
        strUserAccount = CommonFunction.StringFilter(strUserAccount);
        strUserName = CommonFunction.StringFilter(strUserName);
        strEmail = CommonFunction.StringFilter(strEmail);

        DataCommand cmd = DataCommandManager.GetDataCommand("InsertCmsUser");
        cmd.SetParameterValue("@User_Account", strUserAccount);
        cmd.SetParameterValue("@User_Pwd", strPwd);
        cmd.SetParameterValue("@User_DspName", strUserName);
        cmd.SetParameterValue("@User_Email", strEmail);
        cmd.SetParameterValue("@User_HRID", strHRID);
        cmd.SetParameterValue("@User_Tel", strTel);
        cmd.SetParameterValue("@User_Title", strTitle);
        cmd.SetParameterValue("@User_Manager", strUserManager);
        int i = cmd.ExecuteNonQuery();


        if (!String.IsNullOrEmpty(ddpRole.SelectedValue))
        {
            UserElementEntity _ueEntity = new UserElementEntity();
            _ueEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _ueEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _ueEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _ueEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            _ueEntity.UserElementDBEntity = new List<UserElementDBEntity>();
            UserElementDBEntity ueDBEntity = new UserElementDBEntity();
            ueDBEntity.RoleID = ddpRole.SelectedValue;
            ueDBEntity.UserAccount = strUserAccount;
            ueDBEntity.UEType = "0";
            ueDBEntity.UECreator = UserSession.Current.UserAccount;

            _ueEntity.UserElementDBEntity.Add(ueDBEntity);
            int intResult = UserElementBP.Insert(_ueEntity);
        }
    }

    //根据Account修改用户，执行方法
    private void updateUser(string strUserAccount, string strUserName, string strEmail, string strPwd, string strHRID, string strTel, string strTitle, string strUserManager)
    {
        if (String.IsNullOrEmpty(strPwd))
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateCmsUserByIDNoPwd");
            cmd.SetParameterValue("@User_Account", strUserAccount);
            cmd.SetParameterValue("@User_DspName", strUserName);
            cmd.SetParameterValue("@User_Email", strEmail);
            cmd.SetParameterValue("@User_HRID", strHRID);
            cmd.SetParameterValue("@User_Tel", strTel);
            cmd.SetParameterValue("@User_Title", strTitle);
            cmd.SetParameterValue("@User_Manager", strUserManager);
            cmd.ExecuteNonQuery();
        }
        else
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateCmsUserByID");
            cmd.SetParameterValue("@User_Account", strUserAccount);
            cmd.SetParameterValue("@User_Pwd", strPwd);
            cmd.SetParameterValue("@User_DspName", strUserName);
            cmd.SetParameterValue("@User_Email", strEmail);
            cmd.SetParameterValue("@User_HRID", strHRID);
            cmd.SetParameterValue("@User_Tel", strTel);
            cmd.SetParameterValue("@User_Title", strTitle);
            cmd.SetParameterValue("@User_Manager", strUserManager);
            cmd.ExecuteNonQuery();
        }


        //UserElementEntity _ueEntity = new UserElementEntity();
        //_ueEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_ueEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_ueEntity.LogMessages.Username = UserSession.Current.UserDspName;
        //_ueEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        //_ueEntity.UserElementDBEntity = new List<UserElementDBEntity>();
        //UserElementDBEntity ueDBEntity = new UserElementDBEntity();
        //ueDBEntity.RoleID = ddpRole.SelectedValue;
        //ueDBEntity.UserAccount = strUserAccount;
        //ueDBEntity.UEType = "0";
        //ueDBEntity.UECreator = UserSession.Current.UserAccount;

        //_ueEntity.UserElementDBEntity.Add(ueDBEntity);
        //int intResult = UserElementBP.Update(_ueEntity);
    }


    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="p_username"></param>
    protected void LoadOldInfo(string UserAccount)
    {
        DataCommand cmd = DataCommandManager.GetDataCommand("SelectCmsUserByID");
        cmd.SetParameterValue("@User_Account", UserAccount);
        DataSet ds = cmd.ExecuteDataSet(); 

        this.txtUserAccount.Text = ds.Tables[0].Rows[0]["USER_Account"].ToString().Trim();
        this.txtUserAccount.Enabled = false;
        this.txtUserName.Text = ds.Tables[0].Rows[0]["User_DspName"].ToString().Trim();

        //this.txtPwd.Text = ds.Tables[0].Rows[0]["User_Pwd"].ToString().Trim();

        this.txtEmail.Text = ds.Tables[0].Rows[0]["User_Email"].ToString().Trim();
        this.txtHRID.Text = ds.Tables[0].Rows[0]["User_HRID"].ToString().Trim();
        this.txtTel.Text = ds.Tables[0].Rows[0]["User_Tel"].ToString().Trim();
        //this.txtTitle.Text = ds.Tables[0].Rows[0]["User_Title"].ToString().Trim();
        this.txtUserManager.Text = ds.Tables[0].Rows[0]["User_SalesManager"].ToString().Trim();
        tdRoL.Style.Add("display", "none");
        tdRoV.Style.Add("display", "none");
    }
}