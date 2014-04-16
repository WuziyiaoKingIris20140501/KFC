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

public partial class Security_Roles_RoleManage : BasePage
{
    public string STR_CONFIRM_DEL = string.Empty;
    public string STR_SEL_DEL = string.Empty;
    public string STR_SEL_MODIFY = string.Empty;
    public string STR_EDITROLE = string.Empty;
    public string STR_EDIT = string.Empty;
    public string STR_RoleNameEmpty = string.Empty;

    RoleEntity _roleEntity = new RoleEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Delete.Attributes.Add("onclick", "return CheckSubmit()");
        
        if (!IsPostBack)
        {
            STR_CONFIRM_DEL = GetLocalResourceObject("PromptConfirmDel").ToString();
            STR_SEL_DEL = GetLocalResourceObject("PromptSelDeleteItem").ToString();
            STR_SEL_MODIFY = GetLocalResourceObject("PromptSelModifyItem").ToString();
            STR_EDITROLE = Resources.MyGlobal.DeleteText;
            STR_EDIT = Resources.MyGlobal.EditText;

            STR_RoleNameEmpty = GetLocalResourceObject("RoleNameIsNotEmpty").ToString();

            BindGridView();
            GridviewControl.ResetGridView(this.RoleGridView);
        }
       
    }
   
    private void BindGridView()
    {
        DataCommand cmd = DataCommandManager.GetDataCommand("SelectRolesList"); 
        System.Data.DataSet dsResult = cmd.ExecuteDataSet();
        DataTable dtRole = dsResult.Tables[0];
        GridviewControl.GridViewDataBind(this.RoleGridView, dtRole);
    }
 
    protected void RoleGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
        }
    }

    protected void RoleGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                #region 每行复选框的全选与取消全选 DataRow部分
                // TableCell里的每个Control
                for (int j = 0; j < e.Row.Cells[i].Controls.Count; j++)
                {
                    if (e.Row.Cells[i].Controls[j] is CheckBox)
                    {
                        CheckBox chk = (CheckBox)e.Row.Cells[i].Controls[j];
                        chk.Attributes.Add("onclick", "yy_ClickCheckItem(this)");
                    }
                }
                #endregion
            }
        }
    }

    /// <summary>
    /// 新增角色页面中的保持按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtRoleName.Text.ToString().Trim()))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + STR_RoleNameEmpty + "');", true);
            return;
        }        

        //_roleEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_roleEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_roleEntity.LogMessages.Username = UserSession.Current.UserDspName;

        //_commonEntity.LogMessages = _roleEntity.LogMessages;
        //_commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        //CommonDBEntity commonDBEntity = new CommonDBEntity();

        //commonDBEntity.Event_Type = "";
        //commonDBEntity.Event_ID = "";


        string updateRoleID = this.txtRoleID.Text.Trim();
        if (string.IsNullOrEmpty(updateRoleID))
        {
            //增加一条到数据库中
            _roleEntity.RoleDBEntity = new List<RoleDBEntity>();
            RoleDBEntity roleDBEntity = new RoleDBEntity();
            roleDBEntity.RoleName = txtRoleName.Text.Trim();
            roleDBEntity.RoleCreator = UserSession.Current.UserAccount;
            roleDBEntity.UpdateTime = DateTime.Now.ToString();
            roleDBEntity.CreateTime = DateTime.Now.ToString();
            roleDBEntity.IsAD = (chkIsAD.Checked) ? "1" : "0";
            _roleEntity.RoleDBEntity.Add(roleDBEntity);
            int iResult = RoleBP.Insert(_roleEntity);

            if (iResult == 1)//插入成功
            {
                string successText = Resources.MyGlobal.InsertSuccessText;
                //commonDBEntity.Event_Result = successText;
                //messageContent.InnerHtml = Resources.MyGlobal.InsertSuccessText;
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + successText + "');", true);

                clearPopupText();

                BindGridView();//重新绑定显示的页面
            }
            else if (iResult == 2)//表示该名称已经存在
            {
                string strRoleExistText = GetLocalResourceObject("PromptRoleExist").ToString();
                //commonDBEntity.Event_Result = strRoleExistText;
                //messageContent.InnerHtml = GetLocalResourceObject("PromptRoleExist").ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + strRoleExistText + "')", true);
            }
            else//表示失败
            {
                string strRoleFaild = GetLocalResourceObject("PromptAddRoleFaild").ToString();
                //commonDBEntity.Event_Result = strRoleFaild;
                //messageContent.InnerHtml = GetLocalResourceObject("PromptAddRoleFaild").ToString();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "failed", "alert('" + strRoleFaild + "')", true);
            }
        }
        else
        {
            //修改数据库中该条信息
            _roleEntity.RoleDBEntity = new List<RoleDBEntity>();
            RoleDBEntity roleDBEntity = new RoleDBEntity();
            
            roleDBEntity.RoleName = txtRoleName.Text.Trim();
            roleDBEntity.RoleID = txtRoleID.Text.Trim();            
            roleDBEntity.UpdateTime = DateTime.Now.ToString();
            roleDBEntity.IsAD = (chkIsAD.Checked) ? "1" : "0";
            _roleEntity.RoleDBEntity.Add(roleDBEntity);           
            int iResult = RoleBP.Update(_roleEntity);

            if (iResult == 1)//修改成功
            {
                string successText = Resources.MyGlobal.UpdateSuccessText;
                //commonDBEntity.Event_Result = successText;
                //messageContent.InnerHtml = Resources.MyGlobal.InsertSuccessText;
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + successText + "');", true);
                clearPopupText();

                BindGridView();//重新绑定显示的页面
            }
            else if (iResult == 2)//表示该名称已经存在
            {
                string strRoleExistText = GetLocalResourceObject("PromptRoleExist").ToString();
                //commonDBEntity.Event_Result = strRoleExistText;
                //messageContent.InnerHtml = GetLocalResourceObject("PromptRoleExist").ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + strRoleExistText + "')", true);
            }
            else//表示失败
            {
                string strRoleFaild = GetLocalResourceObject("PromptUpdateRoleFaild").ToString();
                //commonDBEntity.Event_Result = strRoleFaild;
                //messageContent.InnerHtml = GetLocalResourceObject("PromptAddRoleFaild").ToString();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "failed", "alert('" + strRoleFaild + "')", true);
            }

        }

        //string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        //conTent = string.Format(conTent,txtRoleName.Text.Trim());
        //commonDBEntity.Event_Content = conTent;       

    }

    //删除弹出的
    private void clearPopupText()
    {
        this.txtRoleID.Text = "";
        this.txtRoleName.Text = "";    
    }

    protected void RoleGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string roleid = RoleGridView.DataKeys[e.RowIndex].Value.ToString();
        
        //LogMessage Insert
        //_roleEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_roleEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        //_roleEntity.LogMessages.Username = UserSession.Current.UserDspName;

        //_commonEntity.LogMessages = _roleEntity.LogMessages;
        //_commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        //CommonDBEntity commonDBEntity = new CommonDBEntity();


        //增加一条到数据库中
        _roleEntity.RoleDBEntity = new List<RoleDBEntity>();
        RoleDBEntity roleDBEntity = new RoleDBEntity();
        roleDBEntity.RoleID = roleid; 
        _roleEntity.RoleDBEntity.Add(roleDBEntity);
        int iResult = RoleBP.Delete(_roleEntity);


        if (iResult == 1)//删除成功
        {
            string successText = Resources.MyGlobal.DeleteSuccessText;
            //commonDBEntity.Event_Result = successText;            
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + successText + "');", true);

            BindGridView();//重新绑定显示的页面
        }        
        else//表示失败
        {
            string strRoleFaild = GetLocalResourceObject("DeleteSuccessText").ToString();
            //commonDBEntity.Event_Result = strRoleFaild;            
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "failed", "alert('" + strRoleFaild + "')", true);
        }
    }
}