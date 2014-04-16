using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;

public partial class Security_Users_UserManage : BasePage
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {                     
            BindGridView();
            GridviewControl.ResetGridView(this.UserGridView);
        }      

        this.btnDelete.Attributes.Add("onclick", "return CheckSubmit()");
        //this.btnAdd.Text = btnNewText;
        //this.btnDelete.Text = btnDelText;
        //this.btnSearch.Text = STR_IMG_SEARCH;
    }

    private void BindGridHandler(object sender, EventArgs e)
    {
        BindGridView();
    }

    private void BindGridView()
    {      
        string getStr = Getwhere();

        DataCommand cmd = DataCommandManager.GetDataCommand("SelectCmsUserList");
        cmd.SetParameterValue("@SearchText", getStr);

        System.Data.DataSet dsResult = cmd.ExecuteDataSet();
        DataTable dtUser = dsResult.Tables[0]; 
        GridviewControl.GridViewDataBind(this.UserGridView, dtUser);
       
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {      
        Response.Redirect("AddCmsUser.aspx?type=add");
    }

    protected void UserGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("OnClick", "window.location.href='AddCmsUser.aspx?type=update&account=" + Server.UrlEncode(e.Row.Cells[1].Text) + "'");
            }

            if (e.Row.Cells[7].Text == "False")
            {
                e.Row.Cells[7].Text = "无效";//STR_DISABLE;
            }
            else if (e.Row.Cells[7].Text == "True")
            {
                e.Row.Cells[7].Text = "有效";//STR_ENABLE2;
            }
        }
    }
    
    //快捷查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.UserGridView.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)this.UserGridView.Rows[i].FindControl("checkitem");
            if (ck.Checked == true)
            {
                string str = this.UserGridView.Rows[i].Cells[1].Text.Trim();
                if (str.Trim() == "admin")
                {
                    Page.RegisterStartupScript("DeleteUser", "<script language='javascript'>alert('不能删除admin账号！')</script>");
                    return;
                }
                // DeletebyuserAccount(this.UserGridView.Rows[i].Cells[1].Text.Trim());
            }
        }
        //BaseUserInfo.GetUsers();
        //this.Page.Response.Write("<script>alert('" + STR_DEL_SUCCESS + "');document.location.href=document.location.href;</script>");
    }

    public string Getwhere()
    {
        string strFilter = string.Empty;
        string strsch_cond = CommonFunction.StringFilter(this.sch_cond.Text.Trim());
        if (!string.IsNullOrEmpty(strsch_cond))
        {
           string strNLike = "'%" + strsch_cond + "%'";

            strFilter = " and (" + " [USER_Account] LIKE " + strNLike + " or [USER_DspName] LIKE  " + strNLike + ")";
        }
        return strFilter;
    }  
    
}