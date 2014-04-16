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
using System.Data.SqlClient;


public partial class NewSite : System.Web.UI.MasterPage
{
    private DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable dt = new DataTable();
            if (HttpContext.Current.Application["Administrator"].ToString().Contains(UserSession.Current.UserAccount.ToLower()))
            {
                dt = UserPermission.GetModulesByLevel(ModuleLevel.All);
            }
            else
            {
                dt = new UserPermission(UserSession.Current.UserAccount).GetModulesByLevelWithRight(ModuleLevel.All);
            }

            InitTree(this.MenuTreeView.Nodes, "0",dt); //递归生成树。

            if (Session["NodeIndex"] != null)
            {
                int index = Convert.ToInt32(Session["NodeIndex"]);
                this.MenuTreeView.Nodes[index].Expanded = true;
            }
            else
            {
                this.MenuTreeView.Nodes[0].Expanded = true;
            }
        }

    }
   
    private void InitTree(TreeNodeCollection Nds, string parentId,DataTable dt)
    {
        DataView dv = new DataView();
        TreeNode tmpNd;
        string intId;

        dv.Table = dt;
        dv.RowFilter = "Parent_MenuId='" + parentId + "'";
        foreach (DataRowView drv in dv)
        {
            tmpNd = new TreeNode();
            //tmpNd.Target = "frm_right";
            tmpNd.Text = drv["Menu_Name"].ToString();
            tmpNd.NavigateUrl = drv["Menu_Url"].ToString();
            tmpNd.Value = drv["Menu_ID"].ToString();
            Nds.Add(tmpNd);

            intId = drv["Menu_ID"].ToString();
            InitTree(tmpNd.ChildNodes, intId, dt);
        }
    }

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        CommonFunction comFun = new CommonFunction();
        comFun.clearSessionAndCookies();
        Response.Redirect("~/Login.aspx");
    }

    protected void lbtnUpdatePwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Security/Permissions/ModifyPwd.aspx");
    }

    protected void MenuTreeView_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        int i = 0;
        if (e.Node.Parent == null)
        {
            i = this.MenuTreeView.Nodes.IndexOf(e.Node);         
        }
        else
        {
            i = this.MenuTreeView.Nodes.IndexOf(e.Node.Parent);          
        }       

        Session["NodeIndex"] = i.ToString();


        TreeNodeCollection ts = null;
        if (e.Node.Parent == null)
        {
            ts = ((TreeView)sender).Nodes;
        }
        else
            ts = e.Node.Parent.ChildNodes;
        foreach (TreeNode node in ts)
        {
            if (node != e.Node)
            {
                node.Collapse();
            }
        } 
    }

}
