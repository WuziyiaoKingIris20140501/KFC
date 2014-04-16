using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using HotelVp.Common.DBUtility;
using System.Data;
using System.Collections;

public partial class WebUI_Hotel_OnlineHotel : BasePage
{
    public static string noLimit = string.Empty;
    public static string strOnlineLabel = string.Empty;
    public static string strOfflineLabel = string.Empty;
    public static string ResetText = string.Empty;
    public static string PromptHotelIDIsNaN = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            noLimit = Resources.MyGlobal.NoLimitText;
            ResetText = Resources.MyGlobal.ResetText;
            strOnlineLabel = GetLocalResourceObject("OnlineLabel").ToString();
            strOfflineLabel = GetLocalResourceObject("OfflineLabel").ToString();
            PromptHotelIDIsNaN = GetLocalResourceObject("PromptHotelIDIsNaN").ToString();

            bindCityDDL();
            BindGridView();
        }
    }

    private DataSet getHotelData()
    {   
        string sql = string.Empty;
        if (ViewState["condition"] != null)
        {
            sql = "select PROP,PROP_NAME_ZH,TRADEAREA_ZH,CITYID,ADDRESS1_ZH,STATUS from FOG_t_prop prop where prop.IS_LM=1 AND TO_NUMBER(prop.PROP) > 999" + ViewState["condition"].ToString();
        }
        else
        {
            sql = "select PROP,PROP_NAME_ZH,TRADEAREA_ZH,CITYID,ADDRESS1_ZH,STATUS from FOG_t_prop prop where prop.IS_LM=1 AND TO_NUMBER(prop.PROP) > 999";
        }

        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;    


    }

    private void BindGridView()
    {
        GridviewControl.GridViewDataBind(this.gridViewOnline, getHotelData().Tables[0]);    
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string hotelid = this.txtHotelID.Text.Trim();
        string hotelname = this.txtHotelName.Text.Trim();
        string cityid = this.ddlCity.SelectedValue;
        string status = this.radioListStatus.SelectedValue;

        string condtion = string.Empty;
        if (!string.IsNullOrEmpty(hotelid))
        {
            condtion = condtion + " and PROP= " + hotelid;        
        }
        if (!string.IsNullOrEmpty(hotelname))
        {
            condtion = condtion + " and  PROP_NAME_ZH like '%" + hotelname + "%'";
        }
        if (!string.IsNullOrEmpty(cityid))
        {
            condtion = condtion + " and  CITYID = '" + cityid + "'";
        }

        if (!string.IsNullOrEmpty(status))
        {
            condtion = condtion + " and  status = '" + status + "'";
        }

        ViewState["condition"] = condtion;//把这个查询条件保存下来。

        gridViewOnline.EditIndex = -1;

        BindGridView();//绑定gridview数据。
    }

    //绑定下来框数据
    private void bindCityDDL()
    {
        //string sql ="select CITY_ID,NAME_CN from t_lm_b_city where STATUS=1";        
        string sql = "select CITY_ID,NAME_CN from t_lm_b_city";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["NAME_CN"] = noLimit;
        row1["CITY_ID"] = "";
        myTable.Rows.InsertAt(row1, 0);

        this.ddlCity.DataSource = myTable;
        this.ddlCity.DataTextField="NAME_CN";
        this.ddlCity.DataValueField="CITY_ID";
        this.ddlCity.DataBind();
    
    }

    protected void gridViewOnline_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewOnline.PageIndex = e.NewPageIndex;
        gridViewOnline.EditIndex = -1;
        BindGridView();
    }

    //点击编辑
    protected void gridViewOnline_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridViewOnline.EditIndex = e.NewEditIndex;
        BindGridView();
        ((DropDownList)gridViewOnline.Rows[e.NewEditIndex].Cells[5].FindControl("ddlOnline")).Enabled = true;
    }


    //取消编辑
    protected void gridViewOnline_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridViewOnline.EditIndex = -1;
        BindGridView();
    }


    protected void gridViewOnline_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string hotelid = gridViewOnline.DataKeys[e.RowIndex].Value.ToString();        
        string onlineStatus = ((DropDownList)gridViewOnline.Rows[e.RowIndex].Cells[5].FindControl("ddlOnline")).SelectedValue;
        updateOnlineStatus(hotelid,onlineStatus);
        gridViewOnline.EditIndex = -1;
        BindGridView();
    }

    //修改上线，下线状态
    private void updateOnlineStatus(string hotelid,string status)
    {
        string updateSql = "update FOG_t_prop set STATUS='" + status + "' where  prop=" + hotelid;
        int i = DbHelperOra.ExecuteSql(updateSql);        
    }
}