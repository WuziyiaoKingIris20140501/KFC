using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelVp.Common.DBUtility;
using System.Data;


public partial class Common_sel_package : System.Web.UI.Page
{
    public string FormType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //多语言      
        if (!IsPostBack)
        {
            //排序            
            BindGridView();
        }

        if (Request.QueryString["FormType"] != "")
        {
            FormType = Request.QueryString["FormType"];
        }

    }

    void BindGridView()
    {
        string strSql = "select distinct id,packagecode,usercnt,startdate,enddate,packagename from t_lm_ticket_package where 1=1 ";
        if (this.txtPackageName.Text != "")
        {
            strSql += " and packagename like '%" + CommonFunction.StringFilter(this.txtPackageName.Text.Trim()) + "%'";
        }

        if (this.txtPackageCode.Text != "")
        {
            strSql += " and packagecode=  '" + CommonFunction.StringFilter(this.txtPackageCode.Text.Trim()) + "'";
        }

        strSql += " order by ID desc";

        DataSet ds = DbHelperOra.Query(strSql, false);        
        DataView view = ds.Tables[0].DefaultView;
        this.myGridView.DataSource = view;
        this.myGridView.DataBind();

    }

    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        myGridView.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
}