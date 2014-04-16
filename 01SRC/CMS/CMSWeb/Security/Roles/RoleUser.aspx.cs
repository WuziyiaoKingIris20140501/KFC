using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;

using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;


//using Maticsoft.DBUtility;

public partial class Security_Roles_RoleUser : BasePage
{
    protected string roleName = "";
    protected string roleid = "";

    UserElementEntity _ueEntity = new UserElementEntity();
    CommonEntity _commonEntity = new CommonEntity();    
    protected void Page_Load(object sender, EventArgs e)
    {
        roleName = Server.UrlDecode(Request.QueryString.Get("rolename"));
        roleid = Request.QueryString.Get("roleid");
        Literal2.Text = roleName;

        if (!IsPostBack)
        {
            this.btnBack.Value = Resources.MyGlobal.BackText;
            this.btnBack2.Value = Resources.MyGlobal.BackText;

            //ViewState["OrderDirection"] = "asc";
            //ViewState["OrderByField"] = "USER_Account";
        
            BindGridView1(); 
            GridviewControl.ResetGridView(this.SmartGridView1);
        
            BindGridView2();
            GridviewControl.ResetGridView(this.SmartGridView2);

        }        
        
    }

    private void BindGridHandler(object sender, EventArgs e)
    {
        BindGridView1();
    }
    private void BindGridHandler2(object sender, EventArgs e)
    {
        BindGridView2();
    }

    protected void SmartGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.SmartGridView1.PageIndex = e.NewPageIndex;
        BindGridView1();
    }

    protected void SmartGridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.SmartGridView2.PageIndex = e.NewPageIndex;
        BindGridView2();
    } 

    //显示在该角色中的用户
    private void BindGridView1()
    {
        //string Sql = " SELECT *  FROM CMS_SYS_USERS where [User_Active]=1 and User_Account in ( SELECT User_Account FROM  CMS_SYS_USER_ELEMENT WHERE role_id  = " + roleid + ")";
        //DataTable dtUser =  DbHelperSQL.Query(Sql).Tables[0];
        //GridviewControl.GridViewDataBind(this.SmartGridView1, dtUser);              

        _ueEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _ueEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _ueEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _ueEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _ueEntity.UserElementDBEntity = new List<UserElementDBEntity>();
        UserElementDBEntity ueDBEntity = new UserElementDBEntity();

        ueDBEntity.RoleID = roleid;

        //cityDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
        //cityDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
        //cityDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        //cityDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

        _ueEntity.UserElementDBEntity.Add(ueDBEntity);      
        DataSet dsResult = UserElementBP.SelectInRole(_ueEntity).QueryResult;
        DataTable dtUser = dsResult.Tables[0];
        GridviewControl.GridViewDataBind(this.SmartGridView1, dtUser); 
    }

    //显示不在该角色中的用户
    private void BindGridView2()
    {
        string account = "";
        string dspname = "";

        string getStr = string.Empty;        
        account = CommonFunction.StringFilter(txtAccount.Text.Trim());
        dspname = CommonFunction.StringFilter(txtDspName.Text.Trim());

        if (account.Length > 0 || dspname.Length > 0)
        {
            if (account.Length > 0)
            {
                getStr += string.Format(" and USER_Account like '%{0}%' ", account);
            }

            if (dspname.Length > 0)
            {
                getStr += string.Format(" and USER_DspName like '%{0}%' ", dspname);
            }
        }
        
        //string Sql = " SELECT *  FROM CMS_SYS_USERS where [User_Active]=1 and User_Account not in ( SELECT User_Account FROM  CMS_SYS_USER_ELEMENT WHERE role_id  = " + roleid + ")" + getStr;
        //DataTable dtUser = DbHelperSQL.Query(Sql).Tables[0];
        //GridviewControl.GridViewDataBind(this.SmartGridView2, dtUser);

        _ueEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _ueEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _ueEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _ueEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _ueEntity.UserElementDBEntity = new List<UserElementDBEntity>();
        UserElementDBEntity ueDBEntity = new UserElementDBEntity();

        ueDBEntity.RoleID = roleid;

        _ueEntity.UserElementDBEntity.Add(ueDBEntity);
        DataSet dsResult = UserElementBP.SelectNotInRole(_ueEntity,getStr).QueryResult;
        DataTable dtUser = dsResult.Tables[0];
        GridviewControl.GridViewDataBind(this.SmartGridView2, dtUser);
    }
    
    protected void Delete_Click(object sender, EventArgs e)
    {
        _ueEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _ueEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _ueEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _ueEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        string usersList = "";
        string roleId = Request.QueryString["roleid"];
        for (int i = 0; i < this.SmartGridView1.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)this.SmartGridView1.Rows[i].FindControl("checkitem1");
            if (ck.Checked == true)
            {
                string strUser = this.SmartGridView1.Rows[i].Cells[1].Text.Trim();
                usersList += strUser + ",";
            }
        }
        if (usersList != "")
        {
            usersList = usersList.TrimEnd(',');
            string[] iTempDel = usersList.Split(',');
            for (int i = 0; i < iTempDel.Length; i++)
            {               
                //string sql = "delete from CMS_SYS_USER_ELEMENT where role_id=" + int.Parse(roleId) + " and User_Account='" + iTempDel[i].ToString() + "'";
                //DbHelperSQL.ExecuteSql(sql);

                _ueEntity.UserElementDBEntity = new List<UserElementDBEntity>();
                UserElementDBEntity ueDBEntity = new UserElementDBEntity();
                ueDBEntity.RoleID = roleid;
                ueDBEntity.UserAccount = iTempDel[i].ToString();
                _ueEntity.UserElementDBEntity.Add(ueDBEntity);
                int intResult = UserElementBP.Delete(_ueEntity);                
            }
        }

        BindGridView1();
        BindGridView2();     
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        _ueEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _ueEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _ueEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _ueEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        string usersList = "";
        string roleId = Request.QueryString["roleid"];
        for (int i = 0; i < this.SmartGridView2.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)this.SmartGridView2.Rows[i].FindControl("checkitem2");
            if (ck.Checked == true)
            {
                string strUser = this.SmartGridView2.Rows[i].Cells[1].Text.Trim();
                usersList += strUser + ",";
            }
        }
        if (usersList != "")
        {
            usersList = usersList.TrimEnd(',');

            string[] usersArry = usersList.Split(',');
            for (int i = 0; i < usersArry.Length; i++)
            {
                //string sql = "insert into  [CMS_SYS_USER_ELEMENT]([Role_ID],[User_Account],[UE_Type],[UE_Creator]) values ("+ roleId + ",'" + usersArry[i].ToString() + "',0,'" + UserSession.Current.UserAccount + "')";                
                //DbHelperSQL.ExecuteSql(sql);

                _ueEntity.UserElementDBEntity = new List<UserElementDBEntity>();
                UserElementDBEntity ueDBEntity = new UserElementDBEntity();
                ueDBEntity.RoleID = roleId;
                ueDBEntity.UserAccount = usersArry[i].ToString();
                ueDBEntity.UEType = "0";
                ueDBEntity.UECreator = UserSession.Current.UserAccount;
               
                _ueEntity.UserElementDBEntity.Add(ueDBEntity);
                int intResult = UserElementBP.Insert(_ueEntity);  

            }
        }
        BindGridView1();
        BindGridView2();  
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView2();

    } 
   
}