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


public partial class Site : System.Web.UI.MasterPage
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
                if (String.IsNullOrEmpty(UserSession.Current.UserAccount))
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                string strLoginType = ConfigurationManager.AppSettings["LoginType"].ToString();
                if ("1".Equals(strLoginType))
                {
                    dt = new UserPermission(UserSession.Current.UserAccount).GetModulesByADWithRight(ModuleLevel.All);
                }
                else
                {
                    dt = new UserPermission(UserSession.Current.UserAccount).GetModulesByLevelWithRight(ModuleLevel.All);
                }
            }

            BasePage.dtUserPage = dt;
            InitTree("0", dt); //递归生成树。

            //if (Session["NodeIndex"] != null)
            //{
            //    int index = Convert.ToInt32(Session["NodeIndex"]);
            //    this.MenuTreeView.Nodes[index].Expanded = true;
            //}
            //else
            //{
            //    this.MenuTreeView.Nodes[0].Expanded = true;
            //}

            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetMenuList(" + "2" + ")", true);

            SiteMapPath1.NodeStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
            SiteMapPath1.PathSeparatorStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#666666");
        }

        if (!String.IsNullOrEmpty(Request.QueryString["menu"]) && chkParm(Request.QueryString["menu"].ToString()))
        {
           hidMenuSpan.Value = Request.QueryString["menu"].ToString();
        }
        else
        {
            hidMenuSpan.Value = "-1";
        }

        if (!String.IsNullOrEmpty(hidChangeMenu.Value))
        {
            if (Request.RawUrl.Contains("hidmenu"))
            {
                Context.RewritePath(Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("hidmenu")) + "hidmenu=" + hidChangeMenu.Value + Request.RawUrl.Substring(Request.RawUrl.IndexOf("hidmenu") + 9));
                //Request.RawUrl = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("hidmenu")) + "hidmenu=" + hidChangeMenu.Value + Request.RawUrl.Substring(Request.RawUrl.IndexOf("hidmenu") + 9);
            }
            else
            {
                Context.RewritePath((Request.RawUrl.Contains("?")) ? Request.RawUrl + "&hidmenu=" + hidChangeMenu.Value : Request.RawUrl + "?hidmenu=" + hidChangeMenu.Value);
                //Request.RawUrl = (Request.RawUrl.Contains("?")) ? Request.RawUrl + "&hidmenu=" + hidChangeMenu.Value : Request.RawUrl + "?hidmenu=" + hidChangeMenu.Value;
            }
        }
        //else
        //{
        //    if (Request.RawUrl.Contains("hidmenu"))
        //    {
        //        Context.RewritePath(Request.RawUrl);
        //    }
        //}

        if (Request.Url.ToString().Contains("hidmenu"))
        {
            hidChangeMenu.Value = Request.Url.ToString().Substring(Request.Url.ToString().IndexOf("hidmenu") + 8, 1);
        }

        //this.Page.ClientScript.RegisterOnSubmitStatement(this.Page.GetType(), "btnLoad", "BtnLoadStyle()");
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "btnLoad", "BtnCompleteStyle()");
    }

    private bool chkParm(string parm)
    {
        try
        {
            int.Parse(parm);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void InitTree(string parentId, DataTable dt)
    {
        //DataView dv = new DataView();
        //TreeNode tmpNd;
        //string intId;

        //dv.Table = dt;
        //dv.RowFilter = "Parent_MenuId='" + parentId + "'";
        //foreach (DataRowView drv in dv)
        //{
        //    tmpNd = new TreeNode();
        //    //tmpNd.Target = "frm_right";
        //    tmpNd.Text = drv["Menu_Name"].ToString();
        //    tmpNd.NavigateUrl = drv["Menu_Url"].ToString();
        //    tmpNd.Value = drv["Menu_ID"].ToString();
        //    Nds.Add(tmpNd);

        //    intId = drv["Menu_ID"].ToString();
        //    InitTree(tmpNd.ChildNodes, intId, dt);
        //}


        System.Text.StringBuilder sbList = new System.Text.StringBuilder();
        //string intId;
        string RowFilter = "Parent_MenuId='" + parentId + "'";
        string RowDetailFilter = "";
        int iSpan = 0;
        foreach (DataRow drv in dt.Select(RowFilter))
        {
            sbList.Append("<h3 style='height:25px;'><a href='#'>");
            sbList.Append(drv["Menu_Name"].ToString());
            sbList.Append("</a></h3>"); 
            //intId = drv["Menu_ID"].ToString();
            sbList.Append("<div style='text-align:left;'>");

            RowDetailFilter = "Parent_MenuId='" + drv["Menu_ID"].ToString() + "'";

            foreach (DataRow drDetailRow in dt.Select(RowDetailFilter))
            {
                sbList.Append("<a href='");

                /**** 正式环境IIS 与 测试环境IIS 配置路径不同导致URL链接需要动态跳转 *****/
                //sbList.Append(Request.ApplicationPath + drDetailRow["Menu_Url"].ToString().Replace("~", ""));
                //sbList.Append("<%=ResolveClientUrl(" + "\"" + drDetailRow["Menu_Url"].ToString() + "\"" + ") %>");

                Control cl = new Control();
                if (drDetailRow["Menu_Url"].ToString().Contains("?"))
                {
                    sbList.Append(cl.ResolveClientUrl(drDetailRow["Menu_Url"].ToString()) + "&menu=" + iSpan.ToString() + "'");
                }
                else
                {
                    sbList.Append(cl.ResolveClientUrl(drDetailRow["Menu_Url"].ToString()) + "?menu=" + iSpan.ToString() + "'");
                }

                sbList.Append(" class='leftnav'>");
                sbList.Append(drDetailRow["Menu_Name"].ToString());
                sbList.Append("</a><br />");
            }

            iSpan = iSpan + 1;
            sbList.Append("</div>");
        }

        if (SiteMapPath1.Provider.CurrentNode == null)
        {
            dvLbMap.Style.Add("display", "");
            dvMapPath.Style.Add("display", "none");
        }
        else {
            dvLbMap.Style.Add("display", "none");
            dvMapPath.Style.Add("display", "");
        }

        //foreach (DataRow drRow in dt.Rows)
        //{
        //    sbList.Append("<h3 style='height:25px;'><a href='#'>");
        //    sbList.Append(drRow["Menu_Name"].ToString());
        //    sbList.Append("</a></h3>");
        //    sbList.Append("<div>");

        //    string sql = "select * from [CMS].[dbo].[CMS_SYS_MENU] where Parent_MenuId=" + drRow["Menu_ID"].ToString();
        //    SqlCommand detailcmd = new SqlCommand(sql, con);
        //    dtMenuList = new DataTable();
        //    SqlDataAdapter detailadapter = new SqlDataAdapter(detailcmd);
        //    detailadapter.Fill(dtMenuList);

        //    foreach (DataRow drDetailRow in dtMenuList.Rows)
        //    {
        //        sbList.Append("<a href='");
        //        sbList.Append(drDetailRow["Menu_Url"].ToString());
        //        sbList.Append("'>");
        //        sbList.Append(drDetailRow["Menu_Name"].ToString());
        //        sbList.Append("</a><br />");
        //    }
        //    sbList.Append("</div>");
        //}

        accordion.InnerHtml = sbList.ToString();
    }

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        //CommonFunction comFun = new CommonFunction();
        //comFun.clearSessionAndCookies();
        //Response.Redirect("~/Login.aspx");
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("~/Login.aspx");
    }

    protected void lbtnUpdatePwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Security/Permissions/ModifyPwd.aspx");
    }

    //protected void MenuTreeView_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    //{
    //    int i = 0;
    //    if (e.Node.Parent == null)
    //    {
    //        i = this.MenuTreeView.Nodes.IndexOf(e.Node);         
    //    }
    //    else
    //    {
    //        i = this.MenuTreeView.Nodes.IndexOf(e.Node.Parent);          
    //    }       

    //    Session["NodeIndex"] = i.ToString();


    //    TreeNodeCollection ts = null;
    //    if (e.Node.Parent == null)
    //    {
    //        ts = ((TreeView)sender).Nodes;
    //    }
    //    else
    //        ts = e.Node.Parent.ChildNodes;
    //    foreach (TreeNode node in ts)
    //    {
    //        if (node != e.Node)
    //        {
    //            node.Collapse();
    //        }
    //    } 
    //}

}