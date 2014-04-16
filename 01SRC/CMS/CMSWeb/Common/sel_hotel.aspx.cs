using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelVp.Common.DBUtility;
using System.Data;

public partial class Com_sel_hotel : System.Web.UI.Page
{
    public string FormType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //多语言      
        if (!IsPostBack)
        {
            BindCityDDL();        
            BindGridView(); 
        }
       //Rows = Convert.ToInt32(ViewState["rows"]);
        if (Request.QueryString["FormType"] != "")
        {
            FormType = Request.QueryString["FormType"];
        }

    }
      
    void BindGridView()
    {
        string strSql = "select distinct t.hotel_id,t.name,f.cityid from t_lm_hotel t left join FOG_T_PROP f on t.hotel_id=f.prop where 1=1 ";

        if (this.cityid.SelectedValue != "")
        {
            strSql += " and f.cityid ='" + CommonFunction.StringFilter(this.cityid.SelectedValue.Trim()) + "'";
        }

        if (this.txtHotelName.Text != "")
        {
            strSql += " and t.name like '%" + CommonFunction.StringFilter(this.txtHotelName.Text.Trim()) + "%'";
        }
        if (this.txtHotelID.Text != "")
        {
            strSql += " and t.hotel_id ='" + CommonFunction.StringFilter(this.txtHotelID.Text.Trim()) + "'";
        }


        DataSet ds = DbHelperOra.Query(strSql, false);
        
        DataView view = ds.Tables[0].DefaultView;
       // view.Sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        this.myGridView.DataSource = view;
        this.myGridView.DataBind();

       // HeadRowAddImg(ViewState["SortOrder"].ToString(), this.myGridView, ViewState["OrderDire"].ToString());//增加排序图片
    }

    //绑定城市列表
    private void BindCityDDL()
    {
        string sql = " select  city_id, name_cn from t_lm_city";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];       
        
        DataRow row1 = myTable.NewRow();
        row1["city_id"] = "";
        row1["name_cn"] = "不限制";
        myTable.Rows.InsertAt(row1, 0);

        //myTable.Rows.Add(row1);
        cityid.DataTextField = "name_cn";
        cityid.DataValueField = "city_id";
        cityid.DataSource = myTable;
        cityid.DataBind();


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