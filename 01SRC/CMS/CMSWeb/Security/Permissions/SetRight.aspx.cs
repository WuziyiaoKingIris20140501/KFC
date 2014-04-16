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

using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;


//using Maticsoft.DBUtility;

public partial class Security_Permissions_SetRight : BasePage
{
     public string RightSettingTitle= string.Empty;	//权限设置
     public string PromptSelectModuleName= string.Empty;	//请选择模块名称！
     public string ModuleNameLabel= string.Empty;	//模块名称：
     public string OrgLabel= string.Empty;	//组织结构：
     public string RoleLabel= string.Empty;	//角色：
     public string UserListLabel= string.Empty;	//用户列表：
     public string PromptSuccess= string.Empty;	//保存成功！
     public string PromptFail= string.Empty;	//保存失败！

     RightDA rightDA = new RightDA();
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.btnSave.Text = STR_SAVE;
        //this.listBoxFrom1.Attributes.Add("onDblClick", "dbchkdeptleft()");
        //this.listBoxTo1.Attributes.Add("onDblClick", "dbchkdeptright()");
        this.listBoxFrom2.Attributes.Add("onDblClick", "dbchkroleleft()");
        this.listBoxTo2.Attributes.Add("onDblClick", "dbchkroleright()");

        this.listBoxFrom3.Attributes.Add("onDblClick", "dbchkuserleft()");
        this.listBoxTo3.Attributes.Add("onDblClick", "dbchkuserright()");

        PromptSuccess = GetLocalResourceObject("PromptSuccess").ToString();
        PromptFail = GetLocalResourceObject("PromptFail").ToString();
        if (!IsPostBack)
        {
            //BindDept(listBoxFrom1);
            BindRole(listBoxFrom2);
            BindUser(listBoxFrom3);       

            if (Request.QueryString["mname"] != null)
            {
                txtModuleName.Text = Server.UrlDecode(Request.QueryString["mname"].ToString());
            }

            BindRight();
        }

    }

    private void BindRole(ListBox listBox)
    {
        //string strSql = "SELECT Role_ID ,Role_Name FROM CMS_SYS_ROLES where Role_Active=1";
        //DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
      
        DataTable dt = rightDA.getRoleList().Tables[0];

        listBox.DataSource = dt;
        listBox.DataTextField = "Role_Name";
        listBox.DataValueField = "Role_ID";
        listBox.DataBind();
    }   

    //绑定用户列表
    private void BindUser(ListBox listBox)
    {
        //string strSql = "SELECT User_Account ,User_DspName FROM CMS_SYS_USERS where User_Active=1";
        //DataTable dt = DbHelperSQL.Query(strSql).Tables[0];

        DataTable dt = rightDA.getUserList().Tables[0];
        listBox.DataSource = dt;
        listBox.DataTextField = "User_DspName";
        listBox.DataValueField = "User_Account";
        listBox.DataBind();
    
    }


    #region 组织架构
    ///// <summary>
    ///// 添加部门
    ///// </summary>
    ///// <param name="listBox"></param>
    //private void BindDept(ListBox listBox)
    //{
    //    string strSql = GetDeptlistSql();
    //    DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
    //    if (dt != null)
    //    {

    //        #region 非递归
    //        //DataRow[] dr1 = dt.Select("Group_ParentID=0");
    //        //for (int i = 0; i < dr1.Length; i++)
    //        //{

    //        //    ListItem item = new ListItem();
    //        //    item.Text = dr1[i][0].ToString();
    //        //    item.Value = dr1[i][1].ToString();
    //        //    listBox.Items.Add(item);

    //        //    DataRow[] dr2 = dt.Select("Group_ParentID=" + item.Value);
    //        //    for (int j = 0; j < dr2.Length; j++)
    //        //    {
    //        //        ListItem item1 = new ListItem();
    //        //        item1.Text = "--" + dr2[j][0].ToString();
    //        //        item1.Value = dr2[j][1].ToString();
    //        //        listBox.Items.Add(item1);

    //        //        DataRow[] dr3 = dt.Select("Group_ParentID=" + item1.Value);
    //        //        for (int k = 0; k < dr3.Length; k++)
    //        //        {
    //        //            ListItem item2 = new ListItem();
    //        //            item2.Text = "-- --" + dr3[k][0].ToString();
    //        //            item2.Value = dr3[k][1].ToString();
    //        //            listBox.Items.Add(item2);

    //        //            DataRow[] dr4 = dt.Select("Group_ParentID=" + item2.Value);
    //        //            for (int g = 0; g < dr4.Length; g++)
    //        //            {
    //        //                ListItem item3 = new ListItem();
    //        //                item3.Text = "-- -- --" + dr4[k][0].ToString();
    //        //                item3.Value = dr4[k][1].ToString();
    //        //                listBox.Items.Add(item3);

    //        //                DataRow[] dr5=dt.Select ("Group_ParentID="+item3 .Value );
    //        //                for (int m = 0; m < dr5.Length; m++)
    //        //                {
    //        //                    ListItem item4 = new ListItem();
    //        //                    item4.Text = "-- -- -- --" + dr5[k][0].ToString();
    //        //                    item4.Value = dr5[k][1].ToString();
    //        //                    listBox.Items.Add(item4);                            
    //        //                }
    //        //            }
    //        //        }
    //        //    }
    //        //}
    //        #endregion

    //        #region 递归
    //        BindDept2(listBox, dt, null, 0);
    //        #endregion
    //    }
    //}


    ///// <summary>
    ///// 递归添加部门
    ///// </summary>
    ///// <param name="listBox">listbox</param>
    ///// <param name="dt">数据源</param>
    ///// <param name="item">部门项</param>
    ///// <param name="Level">部门level</param>
    //private void BindDept2(ListBox listBox, DataTable dt, ListItem item, int Level)
    //{

    //    DataRow[] drs = null;
    //    if (listBox.Items.Count == 0)
    //    {
    //        drs = dt.Select("Group_ParentID=0");
    //    }
    //    else
    //    {
    //        drs = dt.Select("Group_ParentID=" + item.Value);
    //    }

    //    string strLine = string.Empty;
    //    for (int k = 0; k < Level; k++)
    //    {
    //        strLine += "--";
    //    }

    //    int intLevel = Level + 1;
    //    for (int g = 0; g < drs.Length; g++)
    //    {
    //        ListItem item1 = new ListItem();
    //        item1.Text = strLine + drs[g][0].ToString();
    //        item1.Value = drs[g][1].ToString();
    //        listBox.Items.Add(item1);

    //        BindDept2(listBox, dt, item1, intLevel);
    //    }

    //}

    ///// <summary>
    ///// 返回部门Sql
    ///// </summary>
    ///// <returns>Sql</returns>
    //private string GetDeptlistSql()
    //{
    //    string strSql = " Select ";
    //    strSql += " Distinct GROUP_Name,";
    //    strSql += " GROUP_ID, ";
    //    strSql += " GROUP_PARENTID";
    //    strSql += " From ";
    //    strSql += " SYS_BPS_GROUPS ";
    //    strSql += " Order by GROUP_ID";
    //    return strSql;
    //}

    #endregion

    /// <summary>
    /// 初始时，显示模块已设置的权限
    /// </summary>
    
    //定义两个公共变量给客户端控件用户账号和用户名
    public string selectedUserName = string.Empty;
    public string selectedUserAccount = string.Empty;

    private void BindRight()
    {
        if (Request.QueryString["mlevel"] == null)
        {
            return;
        }

        if (Request.QueryString["mnid"] == null)
        {
            return;
        }

        string strModuleLevel = Request.QueryString["mlevel"].ToString();
        string strModuleName1 = Request.QueryString["mname"].ToString();

        //string strSql = " Select Menu_ID from CMS_SYS_MENU Where Menu_Level=" + strModuleLevel + " and Menu_Name='" + strModuleName1 + "'";
        //object returnVal = DbHelperSQL.GetSingle(strSql);

        object returnVal = Request.QueryString["mnid"].ToString();//rightDA.getMenuIDByLevelAndName(strModuleLevel, strModuleName1);
     

        int strModuleId = -1;
        if (returnVal != null)
        {
            strModuleId = Convert.ToInt32(returnVal.ToString());

            //strSql = BindRightSql(strModuleId.ToString());
            //DataTable dt = DbHelperSQL.Query(strSql).Tables[0];

            DataTable dt = rightDA.getRightListByModuleID(strModuleId).Tables[0];

            if (dt != null)
            {
                //User
                DataRow[] drUser = dt.Select("MTYPE=1");
                for (int i = 0; i < drUser.Length; i++)
                {
                    //if (txtUserName.Value.Trim() == string.Empty)
                    //{
                    //    txtUserName.Value += drUser[i]["User_DspName"].ToString().Trim();
                    //    txtUserAccount.Value += drUser[i]["MID"].ToString().Trim();
                    //}
                    //else
                    //{
                    //    txtUserName.Value += "," + drUser[i]["User_DspName"].ToString().Trim();
                    //    txtUserAccount.Value += "," + drUser[i]["MID"].ToString().Trim();
                    //}

                    ListItem item = new ListItem();
                    item.Text = drUser[i]["User_DspName"].ToString().Trim();
                    item.Value = drUser[i]["MID"].ToString().Trim();
                    listBoxTo3.Items.Add(item);
                }

                //Role
                DataRow[] drRole = dt.Select("MTYPE=3");
                for (int m = 0; m < drRole.Length; m++)
                {

                    ListItem item = new ListItem();
                    item.Text = drRole[m]["User_DspName"].ToString().Trim();
                    item.Value = drRole[m]["MID"].ToString().Trim();
                    listBoxTo2.Items.Add(item);
                }


                ////Dept
                //DataRow[] drDept = dt.Select("MTYPE=2");
                //for (int k = 0; k < drDept.Length; k++)
                //{
                //    for (int gg = 0; gg < listBoxFrom1.Items.Count; gg++)
                //    {
                //        if (listBoxFrom1.Items[gg].Value.Trim() == drDept[k]["MID"].ToString().Trim())
                //        {
                //            ListItem item = new ListItem();
                //            item.Text = listBoxFrom1.Items[gg].Text;
                //            item.Value = listBoxFrom1.Items[gg].Value;
                //            listBoxTo1.Items.Add(item);
                //        }
                //    }
                //}


            }
        }
    }

    /// <summary>
    /// 初始时，显示模块已设置的权限的Sql
    /// </summary>
    /// <param name="strModuleId">模块ID</param>
    /// <returns>Sql</returns>
    private string BindRightSql(string strModuleId)
    {
        string strSql = string.Empty;
        strSql = " Select ";
        strSql += " User_DspName, ";
        strSql += " 1 MTYPE, ";
        strSql += " USER_ACCOUNT MID";
        strSql += " From ";
        strSql += " CMS_SYS_USERS ";
        strSql += " WHERE USER_ACCOUNT IN (SELECT User_ID FROM CMS_SYS_PERMISSION WHERE Module_ID=" + strModuleId + " AND Permission_Type=1)";

        //strSql += " UNION ";
        //strSql += " SELECT ";
        //strSql += " GROUP_NAME,";
        //strSql += " 2 MTYPE,";
        //strSql += " CAST (GROUP_ID AS VARCHAR(30)) MID  ";
        //strSql += " FROM ";
        //strSql += " SYS_BPS_GROUPS ";
        //strSql += " WHERE GROUP_ID IN (SELECT CompetenceCode FROM SYS_PERMISSION_Competence WHERE ModuleID=" + strModuleId + " AND CompetenceType=2) ";

        strSql += " UNION ";
        strSql += " Select ";
        strSql += " Role_Name as User_DspName,";
        strSql += " 3 MTYPE, ";
        strSql += " CAST (ROLE_ID AS VARCHAR(30)) MID ";
        strSql += " FROM ";
        strSql += " CMS_SYS_ROLES ";
        strSql += " WHERE ROLE_ID IN (SELECT Permission_Code FROM CMS_SYS_PERMISSION WHERE Module_ID=" + strModuleId + " AND Permission_Type=3) ";

        return strSql;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string strModuleName = txtModuleName.Text.Trim();
            if (strModuleName == string.Empty)
            {
                return;
            }

            string strModuleLevel = Request.QueryString["mlevel"].ToString();
            string strModuleName1 = Request.QueryString["mname"].ToString();

            string strPid = string.Empty;
            string strModuleId = Request.QueryString["mnid"].ToString();

            //string strSql = " Select * from CMS_SYS_MENU Where Menu_Level=" + strModuleLevel + " and Menu_Name='" + strModuleName1 + "'";
            //DataTable dt = DbHelperSQL.Query(strSql).Tables[0];

            //DataTable dt = rightDA.getMenuByLevelAndName(strModuleLevel, strModuleName1).Tables[0];
            Hashtable hsNew = new Hashtable();
            hsNew.Add("rblPageSet", "1");
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        strModuleId = dt.Rows[0]["Menu_ID"].ToString();
            //    }
            //}

            if (strModuleId == string.Empty)
            {
                return;
            }


            //strSql = "Delete From CMS_SYS_PERMISSION Where Module_ID=" + strModuleId;
            try
            {
                //DbHelperSQL.ExecuteSql(strSql);

                rightDA.deletePermissionByModuleID(strModuleId);
            }
            catch { return; }

            //先取用户        
            //if ( this.txtUserAccount.Value.Trim() != string.Empty)
            //{
            //    string[] strUsers = this.txtUserAccount.Value.Split(',');

            //    for (int k = 0; k < strUsers.Length; k++)
            //    {
            //        Right_User right_User = new Right_User(strUsers[k], strModuleId, strPid, UserSession.Current.UserAccount);
            //        Hashtable hsOld = new Hashtable();
            //        hsOld.Add("rblPageSet", right_User.GetModuleOrPageRight());

            //        right_User.NewRight = hsNew;
            //        right_User.OldRight = hsOld;
            //        right_User.SetNewRight();
            //    }
            //}


            //Response.Write(listBoxTo1.Items.Count);
            //取部门
            //if (listBoxTo1.Items.Count != 0)
            //{
            //    for (int g = 0; g < listBoxTo1.Items.Count; g++)
            //    {
            //        if (listBoxTo1.Items[g].Value.Trim() != string.Empty)
            //        {
            //            Right_Group right_Group = new Right_Group(listBoxTo1.Items[g].Value, strModuleId, strPid, UserSession.Current.LoginAccount);
            //            Hashtable hsOld1 = new Hashtable();
            //            hsOld1.Add("rblPageSet", right_Group.GetModuleOrPageRight());

            //            right_Group.NewRight = hsNew;
            //            right_Group.OldRight = hsOld1;
            //            right_Group.SetNewRight();
            //        }
            //    }

            //}


            //保存角色列表
            if (listBoxTo2.Items.Count != 0)
            {
                for (int z = 0; z < listBoxTo2.Items.Count; z++)
                {
                    if (listBoxTo2.Items[z].Value.Trim() != string.Empty)
                    {
                        RoleRight right_Role = new RoleRight(listBoxTo2.Items[z].Value, strModuleId, strPid, UserSession.Current.UserAccount);

                        Hashtable hsOld2 = new Hashtable();
                        hsOld2.Add("rblPageSet", right_Role.GetModuleOrPageRight());
                        right_Role.NewRight = hsNew;
                        right_Role.OldRight = hsOld2;
                        right_Role.SetNewRight();
                    }
                }
            }

            //保存用户列表
            if (listBoxTo3.Items.Count != 0)
            {
                for (int k = 0; k < listBoxTo3.Items.Count; k++)
                {
                    Right_User right_User = new Right_User(listBoxTo3.Items[k].Value, strModuleId, strPid, UserSession.Current.UserAccount);
                    Hashtable hsOld = new Hashtable();
                    hsOld.Add("rblPageSet", right_User.GetModuleOrPageRight());

                    right_User.NewRight = hsNew;
                    right_User.OldRight = hsOld;
                    right_User.SetNewRight();
                }
            }
        
        //PromptSuccess
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + PromptSuccess + "');", true);
        }
        catch
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('" + PromptSuccess + "');", true);
        }

    }


}
