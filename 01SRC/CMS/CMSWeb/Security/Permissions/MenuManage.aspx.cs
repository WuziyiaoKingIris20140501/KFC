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



public partial class Security_Permissions_MenuManage : BasePage
{
    private string ParentID = ""; 
 
    public int rowIndex = 0;
    public string STR_YES = string.Empty;
    public string STR_NO = string.Empty;

    public static string strClose = Resources.MyGlobal.CloseText;
    public static string strNew = Resources.MyGlobal.NewText;

    MenuEntity _menuEntity = new MenuEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        STR_YES = "是";
        STR_NO = "否";
      
        //this.btnDel.Attributes.Add("onclick", "return CheckSubmit()");
        //this.btnEdit.Attributes.Add("onclick", "return CheckEdit()");
        
        //多语言的绑定
        if (!IsPostBack)
        {                  
            BindGridView();

            BindMenu();

            //string pid = Request.QueryString["ParentNodeId"];
            //if (!string.IsNullOrEmpty(pid))
            //{
            //    string sql = string.Format("select * from CMS_SYS_MENU where Menu_ID = {0}", pid);                
            //    DataTable dt = DbHelperSQL.Query(sql).Tables[0];

            //   // if (Convert.ToInt32(dt.Rows[0]["Menu_Level"]) >= 2) btnNew.Enabled = false;
            //}

        }
    }

    private void BindGridView()
    {
        #region MenuGridView 数据绑定

        _menuEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _menuEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _menuEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _menuEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _menuEntity.MenuDBEntity = new List<MenuDBEntity>();
        MenuDBEntity menuDBEntity = new MenuDBEntity();

        menuDBEntity.SearchText = "";

        //channelDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
        //channelDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
        //channelDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        //channelDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

        _menuEntity.MenuDBEntity.Add(menuDBEntity);
        DataSet dsResult = MenuBP.SelectMenuList(_menuEntity).QueryResult;

        //gridViewCSChannelList.DataSource = dsResult.Tables[0].DefaultView;
        
        //string sql = "select * from CMS_SYS_MENU";
        //if (Request.QueryString["Parent_MenuId"] != null)
        //{
        //    ParentID = Request.QueryString["Parent_MenuId"].ToString();
        //}
        //else
        //{
        //    ParentID = "0";
        //}
        //if (!string.IsNullOrEmpty(ParentID))
        //{
        //    sql += " where Parent_MenuId =" + ParentID;
        //}
        
        //DataTable dtUser = this.page1.GetTableInfo(" and Parent_MenuId=" + ParentID, "SYS_BPS_MENU", "OrderID", strDirection);

        DataTable dtUser = dsResult.Tables[0];
        GridviewControl.GridViewDataBind(this.MenuGridView, dtUser);

        #endregion
    }
    
    //protected void MenuGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    //是否控制权限
    //    if (e.CommandName == "SingleClick")
    //    {
    //        //获取当前行的索引
    //        LinkButton LB = (LinkButton)e.CommandSource;
    //        rowIndex = int.Parse(e.CommandArgument.ToString());
    //        //bool a = true;
    //        //bool b = false;
    //        int a = 1;
    //        int b = 0;
    //        if (LB.Text == "否")
    //        //if (LB.Text == STR_NO)
    //        {
    //            string sql = "update CMS_SYS_MENU set Menu_Limit='" + a + "' where Menu_ID=" + rowIndex;
    //            int t = DbHelperSQL.ExecuteSql(sql);
    //        }
    //        else
    //        {
    //            string sql = "update CMS_SYS_MENU set Menu_Limit='" + b + "' where Menu_ID=" + rowIndex;
    //            int t = DbHelperSQL.ExecuteSql(sql);
    //        }
    //        this.Page.Response.Write("<script>document.location.href=document.location.href;</script>");
    //    }
    //    //是否显示
    //    if (e.CommandName == "DisplayClick")
    //    {
    //        //获取当前行的索引
    //        LinkButton LbDisplay = (LinkButton)e.CommandSource;
    //        rowIndex = int.Parse(e.CommandArgument.ToString());
    //        //0--表示显示；1--表示隐藏
    //        if (LbDisplay.Text == "否")
    //        //if (LbDisplay.Text == STR_NO)
    //        {
    //            string sql = "update CMS_SYS_MENU set Menu_Show=0 where Menu_ID=" + rowIndex;
    //            int t = DbHelperSQL.ExecuteSql(sql);
    //        }
    //        else
    //        {
    //            string sql = "update CMS_SYS_MENU set Menu_Show=1 where Menu_ID=" + rowIndex;
    //            int t = DbHelperSQL.ExecuteSql(sql);
    //        }
    //        this.Page.Response.Write("<script>document.location.href=document.location.href;</script>");
    //    }

    //}
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
       string strMenuName = txtMenuName.Text.Trim();
       string strUrl = txtUrlPath.Text.Trim();
       string parentID= ddlParentID.SelectedValue.Trim();
       string orderID = txtOrderID.Text.Trim();
       string level = ddlLevel.SelectedValue.Trim();
       string limit = this.radioListLimit.SelectedValue;
       string display = this.RadioListDisplay.SelectedValue;

       _menuEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
       _menuEntity.LogMessages.Userid = UserSession.Current.UserAccount;
       _menuEntity.LogMessages.Username = UserSession.Current.UserDspName;
       _menuEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
       _menuEntity.MenuDBEntity = new List<MenuDBEntity>();
       MenuDBEntity menuDBEntity = new MenuDBEntity();

       menuDBEntity.Parent_MenuId = Convert.ToInt32(parentID);
       menuDBEntity.Menu_Name = strMenuName;
       menuDBEntity.Menu_Url = strUrl;
       menuDBEntity.Menu_Target = "";
       menuDBEntity.Menu_Show =Convert.ToInt32(display);//表示不显示
       menuDBEntity.Menu_OrderID = Convert.ToInt32(orderID);
       menuDBEntity.Menu_Type = 0;//表示类别为菜单控制的。
       menuDBEntity.Menu_Limit =Convert.ToInt32(limit); //表示不作限制
       menuDBEntity.Menu_Level = Convert.ToInt32(level);
       menuDBEntity.Menu_Creator = UserSession.Current.UserAccount;
    
       _menuEntity.MenuDBEntity.Add(menuDBEntity);
       MenuBP.Insert(_menuEntity);

       clearInputText();//清空已经输入的信息

       BindGridView();

       BindMenu();
    }

    private void clearInputText()
    {
        txtMenuName.Text = "";
        txtUrlPath.Text = "";        
        this.ddlParentID.Text = "1";
        txtOrderID.Text = "";        
        this.ddlLevel.Text = "1";
        this.radioListLimit.SelectedValue="1";
        this.RadioListDisplay.SelectedValue = "1";    
    }

    protected void MenuGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{          
        //    e.Row.Cells[5].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        //}
    }

    private void BindMenu()
    {
        DataTable myTable = MenuBP.getFristMenu().Tables[0];
        DataRow row1 = myTable.NewRow();
        row1["Menu_Name"] = "根节点";
        row1["Menu_ID"] = "0";
        myTable.Rows.InsertAt(row1, 0);       
        ddlParentID.DataTextField = "Menu_Name";
        ddlParentID.DataValueField = "Menu_ID";
        ddlParentID.DataSource = myTable.DefaultView;
        ddlParentID.DataBind();
    
    }  

    protected void MenuGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        MenuGridView.EditIndex = e.NewEditIndex;
        BindGridView();     
    }

    protected void MenuGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string MenuID = MenuGridView.DataKeys[e.RowIndex].Value.ToString();

        _menuEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _menuEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _menuEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _menuEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        string parentID = ((DropDownList)MenuGridView.Rows[e.RowIndex].FindControl("ddlParentIDEdit")).SelectedValue;
        string menuName = ((TextBox)MenuGridView.Rows[e.RowIndex].FindControl("txtMenuNameEdit")).Text;
        string MenuUrl =((TextBox)MenuGridView.Rows[e.RowIndex].FindControl("txtMenuUrlEdit")).Text;
        string MenuOrderID = ((TextBox)MenuGridView.Rows[e.RowIndex].FindControl("txtMenuOrderIDEdit")).Text;
        string Level = ((DropDownList)MenuGridView.Rows[e.RowIndex].FindControl("ddlLevelEdit")).SelectedValue;
        string limit = ((RadioButtonList)MenuGridView.Rows[e.RowIndex].FindControl("radioListLimitEdit")).SelectedValue;
        string display = ((RadioButtonList)MenuGridView.Rows[e.RowIndex].FindControl("RadioListDisplayEdit")).SelectedValue;

       
        _menuEntity.MenuDBEntity = new List<MenuDBEntity>();
        MenuDBEntity menuDBEntity = new MenuDBEntity();

        menuDBEntity.Menu_ID = Convert.ToInt32(MenuID);
        menuDBEntity.Parent_MenuId = Convert.ToInt32(parentID);
        menuDBEntity.Menu_Name = menuName;
        menuDBEntity.Menu_Url = MenuUrl;
        menuDBEntity.Menu_OrderID = Convert.ToInt32(MenuOrderID);
        menuDBEntity.Menu_Level = Convert.ToInt32(Level);
        menuDBEntity.Menu_Limit = Convert.ToInt32(limit);
        menuDBEntity.Menu_Show = Convert.ToInt32(display);
        menuDBEntity.Update_Time = DateTime.Now.ToString();

        _menuEntity.MenuDBEntity.Add(menuDBEntity);  
  
        int iResult = MenuBP.UpdateMenuByMenuID(_menuEntity);

        if (iResult == 1) //修改成功
        {
            string successText = Resources.MyGlobal.UpdateSuccessText;
            //commonDBEntity.Event_Result = successText;           
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + successText + "');", true);

            MenuGridView.EditIndex = -1;
            BindGridView();//重新绑定显示的页面
        }
        else//表示修改失败
        {
            string strFaild = GetLocalResourceObject("UpdateFaildText").ToString();
            //commonDBEntity.Event_Result = strRoleFaild;            
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "failed", "alert('" + strFaild + "')", true);
        }

        
    }

    protected void MenuGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        MenuGridView.EditIndex = -1;
        BindGridView();
    }

    /// <summary>
    /// 点击删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MenuGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string MenuID = MenuGridView.DataKeys[e.RowIndex].Value.ToString();
        _menuEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _menuEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _menuEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _menuEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _menuEntity.MenuDBEntity = new List<MenuDBEntity>();
        MenuDBEntity menuDBEntity = new MenuDBEntity();

        menuDBEntity.Menu_ID = Convert.ToInt32(MenuID);
        _menuEntity.MenuDBEntity.Add(menuDBEntity);     //增加一条到数据库中

        int iResult = MenuBP.DeleteMenuByMenuID(_menuEntity);


        if (iResult == 1)//删除成功
        {
            string successText = Resources.MyGlobal.DeleteSuccessText;
            //commonDBEntity.Event_Result = successText;           
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + successText + "');", true);

            BindGridView();//重新绑定显示的页面
            BindMenu();    //绑定下拉框中的父状态
        }
        else//表示失败
        {
            string strFaild = GetLocalResourceObject("DeleteFaildText").ToString();
            //commonDBEntity.Event_Result = strRoleFaild;            
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "failed", "alert('" + strFaild + "')", true);
        }
    }
}