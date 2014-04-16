using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data;
using System.Text;
using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using System.Collections;


public partial class WebUI_Hotel_HotelInMap : BasePage
{
    public string hotelname = string.Empty;
    public string latitude = string.Empty;
    public string longitude = string.Empty;
    public string arrData = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCityDDL();//绑定城市列表
            this.ddlcityid.SelectedValue = "Shanghai";
            this.chkListStar0.Checked = true;
            
            //this.ddlLMBAR.SelectedValue = "LMBAR";

            //hotelname = "这是一个测试酒店";
            //latitude = "31.207589448429228";   //经度
            //longitude = "121.47274017333984"; //纬度  
  
            string strToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            this.fromDate.Value = strToday;
            this.endDate.Value = strToday;
            

            ViewState["FromDate"] = strToday;
            ViewState["EndDate"] = strToday;

            arrData = getLatLngList();
        }
       
    }

    //绑定城市列表
    private void BindCityDDL()
    {
        string sql = " select  city_id, name_cn from T_LM_B_CITY where STATUS=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];
        DataRow row1 = myTable.NewRow();
        row1["city_id"] = "";
        row1["name_cn"] = "不限制";// "不限制";
        myTable.Rows.InsertAt(row1, 0);       
        ddlcityid.DataTextField = "name_cn";
        ddlcityid.DataValueField = "city_id";
        ddlcityid.DataSource = myTable;
        ddlcityid.DataBind();
    }

    private DataSet getHotelData()
    {
        //string sql = string.Empty;
        //if (ViewState["condition"] != null)
        //{
        //    sql = "select PROP,PROP_NAME_ZH,CITYID,STATUS,LATITUDE,LONGITUDE from fog_t_prop prop where prop.IS_LM=1 AND TO_NUMBER(prop.PROP) > 999" + ViewState["condition"].ToString();
        //}
        //else
        //{
        //    ViewState["condition"] = " and CITYID = 'Shanghai'";
        //    //sql = "select PROP,PROP_NAME_ZH,CITYID,STATUS,LATITUDE,LONGITUDE from fog_t_prop prop where prop.IS_LM=1 AND TO_NUMBER(prop.PROP) > 999";
        //    sql = "select PROP,PROP_NAME_ZH,CITYID,STATUS,LATITUDE,LONGITUDE from fog_t_prop prop where prop.IS_LM=1 AND TO_NUMBER(prop.PROP) > 999" + ViewState["condition"].ToString();
        //}

        /************************************/
        //string sqlHotel = "select prop.prop as prop,prop.PROP_NAME_ZH as propName_zh,prop.cityId as cityId,";
        //sqlHotel += "prop.star_rating as starRating,prop.diamond_rating as diamondRating,prop.latitude as latitude,";
        //sqlHotel += "prop.longitude as longitude,city.NAME_ZH as cityNameZh,city.cityId as cityId,city.LONGITUDE as cityLongitude,";
        //sqlHotel += "city.LATITUDE as cityLatitude,";      
        //sqlHotel += "from FOG_T_PROP prop ,FOG_T_CITY city   where prop.cityId = #{cityId}";
        //sqlHotel += "and prop.IS_LM=1 AND prop.STATUS='active' AND TO_NUMBER(prop.PROP) > 999 ";
        //sqlHotel += "and prop.PROP in (select t.hotel_id from t_lm_plan t where t.status='1' and t.effect_date between to_date(#{beginDate},'yyyy-MM-dd')";
        //sqlHotel += "and to_date(#{endDate},'yyyy-MM-dd'))";
        //sqlHotel += "and prop.cityId = city.cityId(+)";
        //DataSet ds = DbHelperOra.Query(sql);

        //===================

        if (ViewState["condition"] == null)
        {
            ViewState["condition"] = " and prop.CITYID = 'Shanghai'";
        }
        
        if (ViewState["PlanCond"] == null)
        {
            //ViewState["PlanCond"] = " and RATE_CODE= 'LMBAR'";        
            ViewState["PlanCond"] = " and 1=1 ";        
        }

        string sqlhotel = "select distinct a.prop,a.PROP_NAME_ZH,a.latitude,a.longitude,p.TWO_PRICE,p.RATE_CODE from ";
        sqlhotel += "(select prop.prop as prop,prop.PROP_NAME_ZH as PROP_NAME_ZH,prop.cityId as cityId,prop.star_rating,prop.latitude as latitude,prop.longitude as longitude from v_t_lm_b_prop prop where prop.STATUS='active' AND TO_NUMBER(prop.PROP) > 999  " + ViewState["condition"].ToString();//查询LM酒店
        sqlhotel += " and prop.prop in (select t.hotel_id from t_lm_plan t where t.status='1' and t.effect_date between to_date(:beginDate,'yyyy-MM-dd') and to_date(:endDate,'yyyy-MM-dd') " + ViewState["PlanCond"].ToString() + ") ) a ,t_lm_plan p";//按计划查询上线的酒店
        sqlhotel +="  where a.prop = p.HOTEL_ID(+) ";
        sqlhotel += " and p.status='1' and p.effect_date between to_date(:beginDate1,'yyyy-MM-dd') and to_date(:endDate1,'yyyy-MM-dd') " + ViewState["PlanCond"].ToString();

        /***********************************/
        OracleParameter[] parm ={                                   
                                    new OracleParameter("beginDate",OracleType.VarChar),
                                    new OracleParameter("endDate",OracleType.VarChar),
                                     new OracleParameter("beginDate1",OracleType.VarChar),
                                    new OracleParameter("endDate1",OracleType.VarChar)
                                };

        parm[0].Value = ViewState["FromDate"].ToString();
        parm[1].Value = ViewState["EndDate"].ToString();
        parm[2].Value = ViewState["FromDate"].ToString();
        parm[3].Value = ViewState["EndDate"].ToString();
        DataSet ds = DbHelperOra.Query(sqlhotel, false, parm);
        return ds;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string cityid = this.ddlcityid.SelectedValue;//选择城市
                        
        //两者之一为4就可以了；如果两者都有的话，就取其中的大的值。
        string star = string.Empty;
        //if (chkListStar0.Checked)
        //{
        //    star = star + chkListStar0.Value + ",";   //不限制        
        //}

        if (chkListStar1.Checked)
        {
            //star = star + chkListStar1.Value + ",";   //经济型        
            star = star  + "0,1,2,";   //经济型        
        }
        if (chkListStar2.Checked)
        {
            star = star + chkListStar2.Value + ",";   //三星        
        }
        if (chkListStar3.Checked)
        {
            star = star + chkListStar3.Value + ",";   //四星        
        }
        if (chkListStar4.Checked)
        {
            star = star + chkListStar4.Value + ",";   //五星        
        }

        //for (int i = 0; i < chkListStar.Items.Count; i++)
        //{
        //    if (chkListStar.Items[i].Selected == true)
        //    {
        //        if (chkListStar.Items[i].Value != "")
        //        {
        //            star = star + chkListStar.Items[i].Value + ",";   //星级。
        //        }
        //    }
        //}

        star = star.Trim(',');

        //this.chkListStar.SelectedValue;
        string lmbar = this.ddlLMBAR.SelectedValue;//预付和现付酒店
        //string priceCondition = this.rbPriceCondition.SelectedValue;//价格条件
        string fromPrice = this.txtFromPrice.Text;//开始价格
        string price = this.txtPrice.Text;//结束价格

        string dtFromDate = this.fromDate.Value;//开始日期
        string dtEndDate = this.endDate.Value;//结束日期

        string condtion = string.Empty;       
        if (!string.IsNullOrEmpty(cityid))
        {
            condtion = condtion + " and  prop.CITYID = '" + cityid + "'";
        }

      //两者之一为4就可以了；如果两者都有的话，就取其中的大的值。
        if (!string.IsNullOrEmpty(star))
        {            
            //condtion = condtion + " and  prop.STAR_RATING >= " + star + "";
            condtion = condtion + " and prop.STAR_RATING  in (" + star + ")";
        }

        ViewState["condition"] = condtion;//把这个查询条件保存下来。


        //(DIAMOND_RATING +STAR_RATING)=5 or DIAMOND_RATING=5 or STAR_RATING=5

        //价格区间
        string planCond = string.Empty;
        if (!string.IsNullOrEmpty(fromPrice))
        {
            planCond = planCond + "  and TWO_PRICE >=" + fromPrice;
        }

        if (!string.IsNullOrEmpty(price))
        {
            planCond = planCond + "  and TWO_PRICE <=" + price;
        }


        if (!string.IsNullOrEmpty(lmbar))
        {
            planCond = planCond + "  and RATE_CODE= '" + lmbar + "'";
        }

        ViewState["PlanCond"] = planCond;
        ViewState["FromDate"] = dtFromDate;
        ViewState["EndDate"] = dtEndDate;

       arrData = getLatLngList();

       this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "initialize();", true);

    }

    private string getLatLngList()
    {
        string latitudetemp = string.Empty;//经度
        string longitudetemp = string.Empty;//维度
        string hotelname = string.Empty;//酒店名称
        string twoprice = string.Empty;//价格
        string temp = string.Empty;     
        StringBuilder sb = new StringBuilder();
        DataTable dt = getHotelData().Tables[0];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            latitudetemp = dt.Rows[i]["LATITUDE"].ToString();//经度
            longitudetemp = dt.Rows[i]["LONGITUDE"].ToString();//维度
            hotelname = dt.Rows[i]["PROP_NAME_ZH"].ToString(); //酒店名
            twoprice = dt.Rows[i]["TWO_PRICE"].ToString(); //价格

            temp = "['" + hotelname + "(价格:" + twoprice + ")'," + latitudetemp + "," + longitudetemp + "," + i + "]";
            sb.Append(temp);
            sb.Append(',');
        }
        string tempList = "[" + sb.ToString().Trim(',') + "]";
        return tempList; 
    }   
}