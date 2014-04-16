using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Collections;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class WebUI_Hotel_HotelPlanSearch : BasePage
{
    public static string noLimit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            noLimit = Resources.MyGlobal.NoLimitText;
            string strToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            this.dtStartTime.Value = strToday;
            this.dtEndTime.Value = strToday;
            bindCityDDL();

            //BindListHotel();
            GridviewControl.ResetGridView(this.gridViewHotelPlan);
            SetEmptyDataTable();
        }
    }

    //绑定下来框数据
    private void bindCityDDL()
    {
        OrderInfoEntity _orderinfoEntity = new OrderInfoEntity();
        DataSet dsCity = OrderInfoBP.CommonCitySelect(_orderinfoEntity).QueryResult;
        DataTable myTable = dsCity.Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["NAME_CN"] = noLimit;
        row1["CITY_ID"] = "";
        myTable.Rows.InsertAt(row1, 0);

        this.ddlCity.DataSource = myTable;
        this.ddlCity.DataTextField = "NAME_CN";
        this.ddlCity.DataValueField = "CITY_ID";
        this.ddlCity.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string cityID = ddlCity.SelectedValue;
        string lmbar = ddlLMBAR.SelectedValue;        
        string dtStartTime = this.dtStartTime.Value;
        string dtEndTime = this.dtEndTime.Value;

        ViewState["cityid"] = cityID;
        ViewState["lmbar"] = lmbar;
        ViewState["startTime"] = dtStartTime;  //开始时间
        ViewState["endTime"] = dtEndTime;      //结束时间
        ViewState["effectdate"] = dtStartTime;//有效时间，取第一天的。
        
        BindListHotel();
        
        //动态增加按钮到前台 
        DateList.DataSource = getDataTable();
        DateList.DataBind();

        //DateList.Items[0].Style.Add("background-color", "yellow");
        //DateList.Items[0].Style.Add("color", "blue");

        Button btn0 = (Button)DateList.Items[0].FindControl("btnDateSearch");
        btn0.Style.Add("background-color", "orange");
        btn0.Style.Add("color", "blue"); 
    }

    //protected void DateList_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //}

    private DataTable getDataTable()
    {
        DataTable dt = new DataTable();
        try
        {           
            DataColumn ID_dc = new DataColumn("id");
            dt.Columns.Add(ID_dc);

            DateTime dtStartDate = Convert.ToDateTime(ViewState["startTime"]);  //开始时间
            DateTime dtEndDate = Convert.ToDateTime(ViewState["endTime"]);      //结束时间

            TimeSpan sp = dtEndDate.Subtract(dtStartDate);
            int intTotalDays =Convert.ToInt32(Math.Round(sp.TotalDays));
            for (int i = 0; i <= intTotalDays; i++)
            {
                DateTime dtTemp = dtStartDate.AddDays(i);

                DataRow row1 = dt.NewRow();
                row1["id"] = string.Format("{0:yyyy-MM-dd}", dtTemp);
                dt.Rows.Add(row1);
            }
            return dt;
        }
        catch
        {
            return dt; 
        }
    }

    private void BindListHotel()
    {
        DataTable dtOrder = getHotelData().Tables[0];
        GridviewControl.GridViewDataBind(this.gridViewHotelPlan, dtOrder);

        
        DataRow[] drArray = dtOrder.Select(" APP_STATUS='1'");
        DataRow[] drArrayDif = dtOrder.Select(" HOTELVP_STATUS is null or (HOTELVP_STATUS<>STATUS AND HOTELVP_STATUS IS NOT NULL) ");
        DataRow[] drArrayPrice = dtOrder.Select(" two_price is null");

        int AppStatusCount = drArray.Length;
        int difSatus = drArrayDif.Length;
        int difPrice = drArrayPrice.Length;
        //提示有多少家相同和不同的酒店
        this.divSearchResult.InnerHtml = "<table width='100%'><tr><td>共找到<font color=red> " + AppStatusCount.ToString() + " </font>家共同上线酒店计划，<font color=red> " + difSatus + " </font>家状态不同酒店，<font color=red> " + difPrice.ToString() + " </font>家无价格酒店</td></tr></table>";


    }

    private DataSet getHotelData()
    {
        string sql = string.Empty;

        //sql = "select p.ID,p.HOTEL_ID,p.ROOM_TYPE_NAME,p.ROOM_TYPE_CODE,p.ROOM_NUM,p.RATE_CODE,p.STATUS,p.HOTELVP_STATUS,p.APP_STATUS,p.TWO_PRICE,p.RATE_CODE,prop.PROP_NAME_ZH as HOTEL_NAME,prop.AUTO_TRUST,e.UPDATE_TIME,e.OPERATOR from T_LM_PLAN p,T_LM_B_PROP prop,T_LM_Plan_Events e  where 1=1 ";
        //sql += " and ((:EFFECT_DATE IS NULL) OR (p.EFFECT_DATE = to_date(:EFFECT_DATE,'yyyy-MM-dd')))";
        //sql += " and prop.Type=0 AND prop.STATUS='active' AND TO_NUMBER(prop.PROP) > 999 ";
        //sql += " and p.HOTEL_ID = prop.PROP(+) ";
        //sql += " and p.HOTEL_ID = e.PROP(+) ";
        //sql += " and p.ID = e.REFID(+) ";
        //sql += " and ((:CITYID IS NULL) OR  (prop.CITYID = :CITYID)) ";
        //sql += " and ((:RATE_CODE IS NULL) OR (p.RATE_CODE = :RATE_CODE)) order by p.APP_STATUS,p.HOTEL_ID";

        sql = "select distinct p.ID,p.HOTEL_ID,p.ROOM_TYPE_NAME,p.ROOM_TYPE_CODE,p.ROOM_NUM,p.RATE_CODE,p.STATUS,p.HOTELVP_STATUS,p.APP_STATUS,p.TWO_PRICE,p.RATE_CODE,p.EFFECT_DATE,prop.PROP_NAME_ZH as HOTEL_NAME,prop.AUTO_TRUST,p.GMT_CREATED,p.CREATOR from T_LM_PLAN p,T_LM_B_PROP prop  where p.EFFECT_DATE= to_date(:EFFECT_DATE,'yyyy-MM-dd')";
        sql += " and prop.Type =0 AND prop.STATUS='active' AND TO_NUMBER(prop.PROP) > 999 ";
        sql += " and p.HOTEL_ID = prop.PROP(+) ";
        //sql += " and p.HOTEL_ID = e.HOTEL_ID(+) ";
        //sql += " and p.ID = e.REFID(+) ";
        sql += " and ((:CITYID IS NULL) OR  (prop.CITYID = :CITYID)) ";
        sql += " and ((:RATE_CODE IS NULL) OR (p.RATE_CODE = :RATE_CODE)) order by p.APP_STATUS,p.HOTEL_ID"; 


        string cityValue = ViewState["cityid"].ToString();
        string lmbar = ViewState["lmbar"].ToString();      
        string effectDate = ViewState["effectdate"].ToString();

        OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar), 
                                    new OracleParameter("RATE_CODE",OracleType.VarChar),     
                                    new OracleParameter("EFFECT_DATE",OracleType.VarChar)                                    
                                };

        if (String.IsNullOrEmpty(cityValue))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = cityValue;
        }

        if (String.IsNullOrEmpty(lmbar))
        {
            parm[1].Value = DBNull.Value;
        }
        else
        {
            parm[1].Value = lmbar;
        }

        if (String.IsNullOrEmpty(effectDate))
        {
            parm[2].Value = DBNull.Value;
        }
        else
        {
            parm[2].Value = effectDate;
        }

        DataSet ds = DbHelperOra.Query(sql, false, parm);
        //DataSet ds = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_orderinfo_select", parm);
        return ds;


    }

    protected void DateList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string command = e.CommandName;
        if (command == "search")
        {
            string id = e.CommandArgument.ToString();//获取其参数的方式得到其ID,根据这个ID来写插入方法
            ViewState["effectdate"] = id;
            BindListHotel();

           
            //这时不知道怎么写了, 
            //想法是在这里调用Button的事件,然后在buuton事件里写插入数据的方法. 
        } 
    }

    protected void btnDateSearch_Click(object sender, EventArgs e)
    {
        int iCount = DateList.Items.Count;
        for (int i = 0; i < iCount; i++)
        {
            Button btn0 = (Button)DateList.Items[i].FindControl("btnDateSearch");
            btn0.Style.Clear();
        }

        Button btn = (Button)sender;
        btn.Style.Add("background-color", "orange");
        btn.Style.Add("color", "blue"); 

    }
    
    //绑定数据时，触发该方法
    protected void gridViewHotelPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {          
        int iCount = this.gridViewHotelPlan.Rows.Count;
        if (iCount == 1)//只有一行数据，
        {
            string cellText = Server.HtmlDecode(this.gridViewHotelPlan.Rows[0].Cells[0].Text.Trim()).Trim();
            if (cellText == "")
            {
                this.gridViewHotelPlan.Rows[0].Style.Add("background-color", "white");
            }
            else
            {
                //5-供应商状态,6--hotelvp状态
                Label lblStatus = (Label)this.gridViewHotelPlan.Rows[0].Cells[5].FindControl("lblStatus");
                DropDownList getHotelVPStatusDDL = (DropDownList)this.gridViewHotelPlan.Rows[0].Cells[6].FindControl("ddlHotelVPStatus");
                string getStatus = lblStatus.Text;        // "上线";//下线 
                string getStatusValue = "";

                if (getStatus == "上线")
                {
                    getStatusValue = "1";
                }
                else
                {
                    getStatusValue = "0";
                }

                string getHotelVpStatus = getHotelVPStatusDDL.SelectedValue;   //1-上线,0--下线
                if (getStatusValue != getHotelVpStatus)
                {
                    this.gridViewHotelPlan.Rows[0].Style.Add("background-color", "#FF6666");
                }

            }
        }
        else
        {
            for (int i = 0; i < iCount; i++)
            {
                //5-供应商状态,6--hotelvp状态
                Label lblStatus = (Label)this.gridViewHotelPlan.Rows[i].Cells[5].FindControl("lblStatus");
                DropDownList getHotelVPStatusDDL = (DropDownList)this.gridViewHotelPlan.Rows[i].Cells[6].FindControl("ddlHotelVPStatus");
                string getStatus = lblStatus.Text;        // "上线";//下线 
                string getStatusValue = "";

                if (getStatus == "上线")
                {
                    getStatusValue = "1";
                }
                else
                {
                    getStatusValue = "0";
                }

                string getHotelVpStatus = getHotelVPStatusDDL.SelectedValue;   //1-上线,0--下线
                if (getStatusValue != getHotelVpStatus)
                {
                    this.gridViewHotelPlan.Rows[i].Style.Add("background-color", "#FF6666");
                }
            }
        }
    }

    private void SetEmptyDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn HOTEL_ID_dc = new DataColumn("HOTEL_ID");
        DataColumn HOTEL_NAME_dc = new DataColumn("HOTEL_NAME");
        DataColumn ROOM_TYPE_NAME_dc = new DataColumn("ROOM_TYPE_NAME");
        DataColumn ROOM_TYPE_CODE_dc = new DataColumn("ROOM_TYPE_CODE");
        DataColumn TWO_PRICE_dc = new DataColumn("TWO_PRICE");
        DataColumn AUTO_TRUST_dc = new DataColumn("AUTO_TRUST");
        DataColumn STATUS_dc = new DataColumn("STATUS");
        DataColumn HOTELVP_STATUS_dc = new DataColumn("HOTELVP_STATUS");
        DataColumn APP_STATUS_dc = new DataColumn("APP_STATUS");
        DataColumn RATE_CODE_dc = new DataColumn("RATE_CODE");
        DataColumn UPDATE_TIME_dc = new DataColumn("GMT_CREATED");
        DataColumn OPERATOR_dc = new DataColumn("CREATOR");
        DataColumn ROOM_NUM_dc = new DataColumn("ROOM_NUM");

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(HOTEL_ID_dc);
        dt.Columns.Add(HOTEL_NAME_dc);
        dt.Columns.Add(ROOM_TYPE_NAME_dc);
        dt.Columns.Add(ROOM_TYPE_CODE_dc);
        dt.Columns.Add(TWO_PRICE_dc);
        dt.Columns.Add(AUTO_TRUST_dc);
        dt.Columns.Add(STATUS_dc);
        dt.Columns.Add(HOTELVP_STATUS_dc);
        dt.Columns.Add(APP_STATUS_dc);
        dt.Columns.Add(RATE_CODE_dc);
        dt.Columns.Add(UPDATE_TIME_dc);
        dt.Columns.Add(OPERATOR_dc);
        dt.Columns.Add(ROOM_NUM_dc);
        GridviewControl.GridViewDataBind(this.gridViewHotelPlan, dt);
    }

    //翻页程序
    protected void gridViewHotelPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewHotelPlan.PageIndex = e.NewPageIndex;
        BindListHotel();
    }
}