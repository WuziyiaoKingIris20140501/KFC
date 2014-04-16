using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelVp.Common.DBUtility;
using System.Data;


public partial class Com_sel_city : System.Web.UI.Page
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
        //string strSql = "select distinct CITY_ID,NAME_CN from T_LM_CITY where 1=1 ";
        string strSql = "select distinct CITY_ID,NAME_CN from T_LM_B_CITY where STATUS=1 ";
         
        if (this.txtCityID.Text != "")
        {
            strSql += " and CITY_ID ='" + CommonFunction.StringFilter(this.txtCityID.Text.Trim()) + "'";            
        }

        if (this.txtCityName.Text != "")
        {
            strSql += " and NAME_CN like '%" + CommonFunction.StringFilter(this.txtCityName.Text.Trim()) + "%'";
        }

        DataSet ds = DbHelperOra.Query(strSql, false);

        DataView view = ds.Tables[0].DefaultView;
        // view.Sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        this.myGridView.DataSource = view;
        this.myGridView.DataBind();
 
    }
   
    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        myGridView.PageIndex = e.NewPageIndex;
        BindGridView();
        //getRows();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
        //getRows();
    }
       
}