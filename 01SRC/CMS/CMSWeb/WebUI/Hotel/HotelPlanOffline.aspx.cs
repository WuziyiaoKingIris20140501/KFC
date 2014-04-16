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

public partial class WebUI_Hotel_HotelPlanOffline : BasePage
{
    public static string noLimit = string.Empty;
    public static string strOnlineLabel = string.Empty;
    public static string strOfflineLabel = string.Empty;
    public static string ResetText = string.Empty;
    public static string PromptHotelIDIsNaN = string.Empty;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        noLimit = Resources.MyGlobal.NoLimitText;
        ResetText = Resources.MyGlobal.ResetText;
        strOnlineLabel = GetLocalResourceObject("OnlineLabel").ToString();
        strOfflineLabel = GetLocalResourceObject("OfflineLabel").ToString();
        PromptHotelIDIsNaN = GetLocalResourceObject("PromptHotelIDIsNaN").ToString();

        if (!Page.IsPostBack)
        {
            bindCityDDL();

            string strToday = string.Format("{0:yyyy-MM-dd}",DateTime.Today);
            string strTwoWeek = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(14));
            string strYesToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-1));

            this.txtYestoday.Text = strYesToday;
            this.txtLastTwoWeek.Text = strTwoWeek;

            this.dtEffectDate.Value = strToday;
            this.dtStartDate.Disabled = true;
            this.dtEndDate.Disabled = true;

            //BindListHotel();
            GridviewControl.ResetGridView(this.gridViewHotelPlan);           
            SetEmptyDataTable();

            //提示有多少家相同和不同的酒店
            //this.divSearchResult.InnerText = "共找到2家共同上线酒店，3家状态不同酒店，3家无价格酒店";
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        string cityID = ddlCity.SelectedValue;
        string lmbar = ddlLMBAR.SelectedValue;
        string dtEffectDate = this.dtEffectDate.Value;

        ViewState["cityid"] = cityID;
        ViewState["lmbar"] = lmbar;
        ViewState["effectDate"] = dtEffectDate;
        BindListHotel();

        this.divSave.Style.Add("display", "none");
        this.divEdit.Style.Add("display", "block");

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

        //sql = "select p.ID,p.HOTEL_ID,p.ROOM_TYPE_NAME,p.ROOM_TYPE_CODE,p.ROOM_NUM,p.RATE_CODE,p.STATUS,p.HOTELVP_STATUS,p.APP_STATUS,p.TWO_PRICE,p.RATE_CODE,p.EFFECT_DATE,prop.PROP_NAME_ZH as HOTEL_NAME,prop.AUTO_TRUST,e.UPDATE_TIME,e.OPERATOR from T_LM_PLAN p,T_LM_B_PROP prop,T_LM_Plan_Events e  where p.EFFECT_DATE= to_date(:EFFECT_DATE,'yyyy-MM-dd')";       
        //sql += " and prop.Type =0 AND prop.STATUS='active' AND TO_NUMBER(prop.PROP) > 999 ";
        //sql += " and p.HOTEL_ID = prop.PROP(+) ";
        //sql += " and p.HOTEL_ID = e.PROP(+) ";
        //sql += " and p.ID = e.REFID(+) ";
        //sql += " and ((:CITYID IS NULL) OR  (prop.CITYID = :CITYID)) ";
        //sql += " and ((:RATE_CODE IS NULL) OR (p.RATE_CODE = :RATE_CODE)) "; 


        sql = "select distinct p.ID,p.HOTEL_ID,p.ROOM_TYPE_NAME,p.ROOM_TYPE_CODE,p.ROOM_NUM,p.RATE_CODE,p.STATUS,p.HOTELVP_STATUS,p.APP_STATUS,p.TWO_PRICE,p.RATE_CODE,p.EFFECT_DATE,prop.PROP_NAME_ZH as HOTEL_NAME,prop.AUTO_TRUST,p.GMT_CREATED,p.CREATOR from T_LM_PLAN p,T_LM_B_PROP prop  where p.EFFECT_DATE= to_date(:EFFECT_DATE,'yyyy-MM-dd')";
        sql += " and prop.Type =0 AND prop.STATUS='active' AND TO_NUMBER(prop.PROP) > 999 ";
        sql += " and p.HOTEL_ID = prop.PROP(+) ";
        //sql += " and p.HOTEL_ID = e.HOTEL_ID(+) ";
        //sql += " and p.ID = e.REFID(+) ";
        sql += " and ((:CITYID IS NULL) OR  (prop.CITYID = :CITYID)) ";
        sql += " and ((:RATE_CODE IS NULL) OR (p.RATE_CODE = :RATE_CODE)) order by p.APP_STATUS,p.HOTEL_ID"; 

  
        string cityValue = ViewState["cityid"].ToString();
        string lmbar = ViewState["lmbar"].ToString();
        string dtEffectDate = ViewState["effectDate"].ToString();

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

        if (String.IsNullOrEmpty(dtEffectDate))
        {
            parm[2].Value = DBNull.Value;
        }
        else
        {
            parm[2].Value = dtEffectDate;
        }

        DataSet ds = DbHelperOra.Query(sql, false, parm);
       //DataSet ds = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_orderinfo_select", parm);
       return ds;


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

    //重置
    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            BindListHotel();
            this.divSave.Style.Add("display", "none");
            this.divEdit.Style.Add("display", "block");

            this.dtStartDate.Disabled = true;
            this.dtEndDate.Disabled = true;

            string strToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            this.dtStartDate.Value = strToday;
            this.dtEndDate.Value = strToday;             
        }
        catch
        {
            Response.Write("<script>alert('重置失败！');</script>");
        }
    }

    //点击保存后进行修改设置。
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string startDate = this.dtStartDate.Value;//开始日期
            string endDate = this.dtEndDate.Value;    //结束日期
            
            List<string> list = new List<string>();
            // List<string> listEvent = new List<string>();
            // List<string> listEventHistory = new List<string>();

            string sql = string.Empty;
            string sqlEvent = string.Empty;
            string sqlEventHistory = string.Empty;
            string sqlPlanHistory = string.Empty;
            DropDownList ddl;
            CommonFunction comFun = new CommonFunction();
            // getMaxIDfromSeq
            for (int i = 0; i < gridViewHotelPlan.Rows.Count; i++)
            {
                string id = gridViewHotelPlan.Rows[i].Cells[0].Text.ToString().Trim();
                string hotelid = gridViewHotelPlan.Rows[i].Cells[1].Text.ToString().Trim();
                string hotelname = gridViewHotelPlan.Rows[i].Cells[2].Text.ToString().Trim();
                string roomtypename = gridViewHotelPlan.Rows[i].Cells[3].Text.ToString().Trim();
                string roomtypecode = gridViewHotelPlan.Rows[i].Cells[4].Text.ToString().Trim();
                string twoprice = gridViewHotelPlan.Rows[i].Cells[5].Text.ToString().Trim();
                string autotrust = ((Label)gridViewHotelPlan.Rows[i].FindControl("lblAutoTrust")).Text.Trim();//是,否
                if (autotrust == "是")
                {
                    autotrust = "1";
                }
                else
                {
                    autotrust = "0";
                }

                string status = ((Label)gridViewHotelPlan.Rows[i].FindControl("lblStatus")).Text.Trim();//上线,下线
                if (status == "上线")
                {
                    status = "1";
                }
                else
                {
                    status = "0";
                }
                string HotelVPStatus = ((DropDownList)gridViewHotelPlan.Rows[i].FindControl("ddlHotelVPStatus")).SelectedValue;
               
                //string appstatus = ((DropDownList)gridViewHotelPlan.Rows[i].FindControl("ddlAPPStatus")).SelectedValue;//上线,下线
                string appstatus = ((Label)gridViewHotelPlan.Rows[i].FindControl("lblAppStatus")).Text.Trim();//上线,下线
                if (appstatus == "上线")
                {
                    appstatus = "1";
                }
                else
                {
                    appstatus = "0";
                }

                string ratecode = gridViewHotelPlan.Rows[i].Cells[10].Text.ToString().Trim();


                //（1）update t_lm_plan中的hotelvpstatus的值。

                string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);

                sql = "update t_lm_plan set hotelvp_status ='" + HotelVPStatus + "',CREATOR='" + UserSession.Current.UserAccount + "' ,GMT_CREATED= to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff')  where (EFFECT_DATE between to_date('" + startDate + "','yyyy-MM-dd') and to_date('" + endDate + "','yyyy-MM-dd')) and hotel_id='" + hotelid + "' and Rate_Code = '" + ratecode + "' and ROOM_TYPE_CODE='" + roomtypecode + "'";               
                list.Add(sql);

                
                
                //(2)把动作表中的信息移动到t_lm_plan_events_history表中。  

                //int newHistoryID = comFun.getMaxIDfromSeq("T_LM_PLAN_EVENTS_HISTORY_SEQ");
                //sqlEventHistory = "insert into t_lm_plan_events_history(ID,REFID,PROP,PROP_NAME_ZH,ROOM_TYPE_NAME,ROOM_TYPE_CODE,RATE_CODE,EFFECT_DATE,STATUS,HOTELVPSTATUS,APPSTATUS,CREATE_TIME,UPDATE_TIME,OPERATOR) select " + newHistoryID + " ,REFID,PROP,PROP_NAME_ZH,ROOM_TYPE_NAME,ROOM_TYPE_CODE,RATE_CODE,EFFECT_DATE,STATUS,HOTELVPSTATUS,APPSTATUS,CREATE_TIME,UPDATE_TIME,OPERATOR from  t_lm_plan_events where REFID=" + id;
                
                //list.Add(sqlEventHistory);

                //(3) 插入信息到t_lm_plan_events表中,先删除，后新增。
                //sqlEvent = "delete from t_lm_plan_events where REFID=" + id;
                //list.Add(sqlEvent);
                //int newid = comFun.getMaxIDfromSeq("T_LM_PLAN_EVENTS_SEQ");
                //string dtNewEffectDate = ViewState["effectDate"].ToString();
                //sqlEvent = "insert into t_lm_plan_events(ID,REFID,PROP,PROP_NAME_ZH,ROOM_TYPE_NAME,ROOM_TYPE_CODE,RATE_CODE,EFFECT_DATE,STATUS,HOTELVPSTATUS,APPSTATUS,OPERATOR) ";
                //sqlEvent += " values (" + newid + "," + id + ",'" + hotelid + "','" + hotelname + "','" + roomtypename + "','" + roomtypecode + "','" + ratecode + "',to_date('" + dtNewEffectDate + "','yyyy-MM-dd'),'" + status + "','" + HotelVPStatus + "','" + appstatus + "','" + UserSession.Current.UserAccount + "')";
               

                //list.Add(sqlEvent);
                //插入数据到
                int h_id = comFun.getMaxIDfromSeq("t_lm_plan_history_seq");//history表中的ID值最大
                sqlPlanHistory = "insert into t_lm_plan_history(ID,REFID,EFFECT_DATE,SEASON,MONEY_TYPE,HOTEL_ID,ROOM_TYPE_NAME,ROOM_TYPE_CODE,STATUS,ROOM_NUM,GMT_CREATED,CREATOR,ONE_PRICE,TWO_PRICE,THREE_PRICE,FOUR_PRICE,ATTN_PRICE,BREAKFAST_NUM,EACH_BREAKFAST_PRICE,IS_NETWORK,GMT_MODIFIED,MODIFIER,IS_DELETED,HOLD_ROOM_NUM,RATE_CODE,GUAID,CXLID,OFFSETVAL,OFFSETUNIT,LMPRICE,THIRDPRICE,LMSTATUS,IS_RESERVE,HOTELVP_STATUS,APP_STATUS,OPERATOR)";
                sqlPlanHistory += " select " + h_id + " ,ID,EFFECT_DATE,SEASON,MONEY_TYPE,HOTEL_ID,ROOM_TYPE_NAME,ROOM_TYPE_CODE,STATUS,ROOM_NUM,GMT_CREATED,CREATOR,ONE_PRICE,TWO_PRICE,THREE_PRICE,FOUR_PRICE,ATTN_PRICE,BREAKFAST_NUM,EACH_BREAKFAST_PRICE,IS_NETWORK,GMT_MODIFIED,MODIFIER,IS_DELETED,HOLD_ROOM_NUM,RATE_CODE,GUAID,CXLID,OFFSETVAL,OFFSETUNIT,LMPRICE,THIRDPRICE,LMSTATUS,IS_RESERVE,'" + HotelVPStatus + "',APP_STATUS,'" + UserSession.Current.UserAccount + "' from  t_lm_plan where ID=" + id;
                list.Add(sqlPlanHistory);
            }

            DbHelperOra.ExecuteSqlTran(list);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('保存成功！');", true);
            BindListHotel();


            this.divSave.Style.Add("display", "none");
            this.divEdit.Style.Add("display", "block");

            this.dtStartDate.Disabled = true;
            this.dtEndDate.Disabled = true;
        }
        catch
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('设置失败！');", true);
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

    //绑定数据时，控制下拉框的选择无效
    protected void gridViewHotelPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList ddl;
        //DropDownList ddl_APPSTATUS;
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当有编辑列时，避免出错，要加的RowState判断 
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ddl = (DropDownList)e.Row.FindControl("ddlHotelVPStatus");
                ddl.Enabled = false;

                //ddl_APPSTATUS = (DropDownList)e.Row.FindControl("ddlAPPStatus");
                //ddl_APPSTATUS.Enabled = false;               
            }
           
        }

        //比较供应商状态和Hotelvp状态是否相同。不同则增加高亮颜色。              
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

    //点击编辑按钮
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string getText = this.gridViewHotelPlan.Rows[0].Cells[0].Text;
        if (getText == Resources.MyGlobal.NoDataText)
        {
            GridviewControl.ResetGridView(this.gridViewHotelPlan);    
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "failed", "alert('请先搜索酒店计划，然后再进行编辑！')", true);
            return;
        }
        else
        {
            DropDownList ddl;
            for (int i = 0; i < gridViewHotelPlan.Rows.Count; i++)
            {
                ddl = (DropDownList)gridViewHotelPlan.Rows[i].FindControl("ddlHotelVPStatus");
                ddl.Enabled = true;
            }
            Button btnSync = (Button)gridViewHotelPlan.HeaderRow.FindControl("btnSyncStatus");
            btnSync.Enabled = true;

            this.divSave.Style.Add("display", "block");
            this.divEdit.Style.Add("display", "none");

            this.dtStartDate.Disabled = false;
            this.dtEndDate.Disabled = false;

            string strYesToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-1));
            string strTwoWeek = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(14));

            string strToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            this.dtStartDate.Value = strToday;
            this.dtEndDate.Value = strToday;             
        }
    }
    
    //点击表格头，进行状态不过
    protected void btnSyncStatus_Click(object sender, EventArgs e)
    {
        DropDownList ddlHotelVPStatus;
        Label lblStatus;
        for (int i = 0; i < gridViewHotelPlan.Rows.Count; i++)
        {
            lblStatus = (Label)gridViewHotelPlan.Rows[i].FindControl("lblStatus"); //供应商状态
            string getStatus = lblStatus.Text;
            if (getStatus == "上线")
            {
                getStatus = "1";
            }
            else
            {
                getStatus = "0";
            }

            ddlHotelVPStatus = (DropDownList)gridViewHotelPlan.Rows[i].FindControl("ddlHotelVPStatus");
            ddlHotelVPStatus.SelectedValue = getStatus;      
        }


    }


    //翻页程序
    protected void gridViewHotelPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewHotelPlan.PageIndex = e.NewPageIndex;
        BindListHotel();
    }
}