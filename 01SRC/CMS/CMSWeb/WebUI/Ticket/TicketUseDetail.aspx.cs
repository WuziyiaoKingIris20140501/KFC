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

public partial class WebUI_Ticket_TicketUseDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string packagecode = Request.QueryString["packagecode"];
            ViewState["PackageCode"] = packagecode;

            //动态增加按钮到前台 
            DateList.DataSource = getTicketTab(packagecode);
            DateList.DataBind();
                      
            Button btn0 = (Button)DateList.Items[0].FindControl("btnDateSearch");
            btn0.Style.Add("background-color", "orange");
            btn0.Style.Add("color", "blue");

            //绑定领用券规则
            BindPackRule(packagecode);

            //绑定使用规则

            object objRuleCode = ViewState["rulecode"];
            if (objRuleCode != null)
            {
                string[] arr = objRuleCode.ToString().Split(',');
                string strRuleCode = arr[0];
                BindUseRule(strRuleCode);
                
                string strTicketCode = arr[1];
                hidTicketCode.Value = strTicketCode;

                // 正确的属性设置方法
                gridViewTicketUseInfo.Attributes.Add("SortExpression", "CREATETIME");
                gridViewTicketUseInfo.Attributes.Add("SortDirection", "ASC");

                BindTicketUserCode(1);

                //绑定统计结果
                setCalcValue(strTicketCode);
            }

        }
    }

    private void BindPackRule(string packagecode)
    {
        DataTable dt = getPackageRule(packagecode);
        this.DetailsViewPackRule.DataSource = dt;
        this.DetailsViewPackRule.DataBind();
    }
    
    private void BindUseRule(string ticketRuleCode)
    {
        DataTable dt =  getTicketRule(ticketRuleCode);
        //绑定领用券规则
        this.DetailsViewUseRule.DataSource = dt;
        this.DetailsViewUseRule.DataBind();
    }

    private void BindTicketUserCode(int startRecord)
    {
        string ticketcode = hidTicketCode.Value;
        DataTable dt = getTicketUserCode(ticketcode, startRecord, gridViewTicketUseInfo.PageSize);
        //绑定领用券规则
        this.gridViewTicketUseInfo.DataSource = dt;
        this.gridViewTicketUseInfo.DataBind();
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindTicketUserCode(AspNetPager1.CurrentPageIndex);
    }

    //优惠券使用规则
    private DataTable getTicketRule(string ticketRuleCode)
    {
        string sql = "select TICKETRULECODE,TICKETRULENAME,STARTDATE,ENDDATE,STARTTIME,ENDTIME,ORDAMT,CITYID,HOTELID,TICKETRULEDESC from t_lm_ticket_rule where  ticketrulecode='" + ticketRuleCode+"'";
        DataSet ds = DbHelperOra.Query(sql, false);
       
        DataTable dt0 = ds.Tables[0];
        //手动创建DataTable表
        DataTable dt = new DataTable();
        dt.Columns.Add("TICKETRULECODE");
        dt.Columns.Add("TICKETRULENAME");
        dt.Columns.Add("STARTDATE_ENDDATE");
        dt.Columns.Add("STARTTIME_ENDTIME");
        dt.Columns.Add("ORDAMT");
        dt.Columns.Add("CITYID");
        dt.Columns.Add("HOTELID");
        dt.Columns.Add("TICKETRULEDESC");       

        for (int i = 0; i < dt0.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();

            dr["TICKETRULECODE"] = dt0.Rows[i]["TICKETRULECODE"].ToString();
            dr["TICKETRULENAME"] = dt0.Rows[i]["TICKETRULENAME"].ToString();
            dr["STARTDATE_ENDDATE"] = dt0.Rows[i]["STARTDATE"].ToString() + "——" + dt0.Rows[i]["ENDDATE"].ToString();
            dr["STARTTIME_ENDTIME"] = dt0.Rows[i]["STARTTIME"].ToString() + "——" + dt0.Rows[i]["ENDTIME"].ToString(); ;
            dr["ORDAMT"] = dt0.Rows[i]["ORDAMT"].ToString();
            dr["CITYID"] = dt0.Rows[i]["CITYID"].ToString(); 
            dr["HOTELID"] = dt0.Rows[i]["HOTELID"].ToString();
            dr["TICKETRULEDESC"] = dt0.Rows[i]["TICKETRULEDESC"].ToString();           
            dt.Rows.Add(dr);
        }
        return dt;         

    }
    
    //获取领用券规则
    private DataTable getPackageRule(string packageCode)
    {
        string sql = "select p.ID, p.PACKAGECODE,p.PACKAGENAME,p.STARTDATE,p.ENDDATE,p.AMOUNT,p.USERCNT,p.CITYID,p.USERGROUPID,t.TicketCount,";
        sql += "CASE WHEN p.PACKAGETYPE= '0' THEN '运营' when p.PACKAGETYPE= '1' then '市场' when p.PACKAGETYPE= '2' then '其他' ELSE '' END AS PACKAGETYNM ";
        sql += " from t_lm_ticket_package p ";
        sql += " left Join ";
        sql += " (select Count(TICKETCNT) as TicketCount,PACKAGECODE from t_lm_ticket group by PACKAGECODE) t";
        sql += " on p.PACKAGECODE = t.PACKAGECODE where 1=1 ";
        sql += " and p.PACKAGECODE ='" + packageCode + "'";

        DataSet ds = DbHelperOra.Query(sql, false);
        DataTable dt0 = ds.Tables[0];
                
        //手动创建DataTable表
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("PACKAGECODE");
        dt.Columns.Add("PACKAGENAME");
        dt.Columns.Add("STARTDATE_ENDDATE");
        dt.Columns.Add("AMOUNT");
        dt.Columns.Add("TicketCount");
        dt.Columns.Add("USERCNT");
        dt.Columns.Add("CITYID");
        dt.Columns.Add("USERGROUPID");
        dt.Columns.Add("PACKAGETYNM");

        for (int i = 0; i < dt0.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = dt0.Rows[i]["ID"].ToString();
            dr["PACKAGECODE"] = dt0.Rows[i]["PACKAGECODE"].ToString();
            dr["PACKAGENAME"] = dt0.Rows[i]["PACKAGENAME"].ToString();
            dr["STARTDATE_ENDDATE"] = dt0.Rows[i]["STARTDATE"].ToString() + "——" + dt0.Rows[i]["ENDDATE"].ToString(); ;
            dr["AMOUNT"] = dt0.Rows[i]["AMOUNT"].ToString();
            dr["TicketCount"] = dt0.Rows[i]["TicketCount"].ToString();
            dr["USERCNT"] = dt0.Rows[i]["USERCNT"].ToString();
            dr["CITYID"] = dt0.Rows[i]["CITYID"].ToString();
            dr["USERGROUPID"] = dt0.Rows[i]["USERGROUPID"].ToString();
            dr["PACKAGETYNM"] = dt0.Rows[i]["PACKAGETYNM"].ToString();
            dt.Rows.Add(dr);
        }
        return dt; 
    }

    #region 绑定Tab中的功能
    protected void DateList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string command = e.CommandName;
        if (command == "search")
        {
            string ticketrulecode = e.CommandArgument.ToString();//获取其参数的方式得到其ID,根据这个ID来写插入方法
            ViewState["rulecode"] = ticketrulecode;

            //BindUseRule(ticketrulecode);           
            if (!string.IsNullOrEmpty(ticketrulecode))
            {
                string[] arr = ticketrulecode.ToString().Split(',');
                string strRuleCode = arr[0];
                BindUseRule(strRuleCode);
                AspNetPager1.CurrentPageIndex = 1;
                string strTicketCode = arr[1];
                hidTicketCode.Value = strTicketCode;
                BindTicketUserCode(1);
                UpdatePanel1.Update();

                //绑定统计结果
                setCalcValue(strTicketCode);
            }
        }
    }

    private DataTable getTicketTab(string packageCode)
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = "select TICKETCODE,TICKETAMT,TICKETRULECODE from t_lm_ticket where PACKAGECODE ='" + packageCode + "'";
            DataTable dt0 = DbHelperOra.Query(sql, false).Tables[0];

            ViewState["TicketDataTable"] = dt0;

            DataColumn ID_dc = new DataColumn("amount");
            dt.Columns.Add(ID_dc);
            DataColumn RULECODE_dc = new DataColumn("rulecode");
            dt.Columns.Add(RULECODE_dc);

            DataColumn TICKETCODE_dc = new DataColumn("ticketcode");
            dt.Columns.Add(TICKETCODE_dc);
            


            for (int i = 0; i <dt0.Rows.Count; i++)
            {
                //ViewState["rulecode"] = dt0.Rows[0]["TICKETRULECODE"].ToString();//第一个金额的值
                string strAmount = dt0.Rows[i]["TICKETAMT"].ToString();
                string strRuleCode = dt0.Rows[i]["TICKETRULECODE"].ToString();
                string ticketcode = dt0.Rows[i]["TICKETCODE"].ToString(); 
                DataRow row1 = dt.NewRow();
                row1["amount"] = strAmount;
                row1["rulecode"] = strRuleCode + "," + ticketcode;

                ViewState["rulecode"] = strRuleCode + "," + ticketcode;

                dt.Rows.Add(row1);
            }
            
            return dt;
        }
        catch
        {
            return dt;
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
    #endregion

    protected void gridViewTicketUseInfo_Sorting(object sender, GridViewSortEventArgs e)
    {
        // 从事件参数获取排序数据列
        string sortExpression = e.SortExpression.ToString();

        // 假定为排序方向为“顺序”
        string sortDirection = "ASC";

        // “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
        if (sortExpression == gridViewTicketUseInfo.Attributes["SortExpression"])
        {
            //获得下一次的排序状态
            sortDirection = (gridViewTicketUseInfo.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
        }

        // 重新设定GridView排序数据列及排序方向
        gridViewTicketUseInfo.Attributes["SortExpression"] = sortExpression;
        gridViewTicketUseInfo.Attributes["SortDirection"] = sortDirection;

        BindTicketUserCode(AspNetPager1.CurrentPageIndex);
        //BindReviewLmSystemLogListGrid(AspNetPager1.CurrentPageIndex , gridViewCSReviewLmSystemLogList.PageSize);
    }
    
    private DataTable getTicketUserCode(string ticketcode, int startRecord, int maxRecord)
    {
        string sql = "select * from (select u.ID,u.TICKETCODE,u.USERID,u.TICKETUSERCODE,u.STATUS,u.TICKETAMT,u.CREATETIME,u.PACKAGECODE,u.FLAG,s.CREATETIME UseTime,s.CNFNUM from T_LM_TICKET_USER u ,T_LM_TICKET_SUB s where u.TICKETCODE ='" + ticketcode + "'";
        sql += " and u.TICKETUSERCODE = s.TICKETUSERCODE(+)) ";

        if (!String.IsNullOrEmpty(gridViewTicketUseInfo.Attributes["SortExpression"]) && !String.IsNullOrEmpty(gridViewTicketUseInfo.Attributes["SortDirection"]))
        {
            sql += " ORDER BY " + gridViewTicketUseInfo.Attributes["SortExpression"] + " " + gridViewTicketUseInfo.Attributes["SortDirection"];
        }

        string sqlCount = "select count(u.ID) from T_LM_TICKET_USER u ,T_LM_TICKET_SUB s where u.TICKETCODE ='" + ticketcode + "'";
        sqlCount += " and u.TICKETUSERCODE = s.TICKETUSERCODE(+)";

        DataSet dsCount = DbHelperOra.Query(sqlCount, false);

        if (dsCount.Tables.Count > 0 && dsCount.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsCount.Tables[0].Rows[0][0].ToString()))
        {
            AspNetPager1.RecordCount = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }
        AspNetPager1.PageSize = maxRecord;
        DataSet ds = DbManager.Query(sql, (startRecord - 1) * maxRecord, maxRecord, false);

        DataTable dt0 = ds.Tables[0];
        //手动创建DataTable表
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TICKETCODE");
        dt.Columns.Add("USERID");
        dt.Columns.Add("TICKETUSERCODE");
        dt.Columns.Add("STATUS");
        dt.Columns.Add("TICKETAMT");
        dt.Columns.Add("CREATETIME");        
        dt.Columns.Add("PACKAGECODE");
        dt.Columns.Add("FLAG");
        dt.Columns.Add("UseTime");
        dt.Columns.Add("CNFNUM");        

        for (int i = 0; i < dt0.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = dt0.Rows[i]["ID"].ToString();
            dr["TICKETCODE"] = dt0.Rows[i]["TICKETCODE"].ToString();
            dr["USERID"] = dt0.Rows[i]["USERID"].ToString();
            dr["TICKETUSERCODE"] = dt0.Rows[i]["TICKETUSERCODE"].ToString();
            
            //0-有效,1-无效,2-过期,3-已使用
            string strStatus = dt0.Rows[i]["STATUS"].ToString();
            switch (strStatus)
            {
                case "0" :
                    dr["STATUS"] = "有效";
                    break;
                case "1":
                    dr["STATUS"] = "无效";
                    break;
                case "2":
                    dr["STATUS"] = "过期";
                    break;
                case "3":
                    dr["STATUS"] = "已使用";
                    break;
                default:
                    dr["STATUS"] = "有效";
                    break;            
            }
            //dr["STATUS"] = dt0.Rows[i]["STATUS"].ToString();


            dr["TICKETAMT"] = dt0.Rows[i]["TICKETAMT"].ToString();
            dr["PACKAGECODE"] = dt0.Rows[i]["PACKAGECODE"].ToString();
            dr["CREATETIME"] = dt0.Rows[i]["CREATETIME"].ToString();

            string strFlag = dt0.Rows[i]["FLAG"].ToString().Trim();
            switch (strFlag)
            {
                case "0":
                    dr["FLAG"] = "前台领用";
                    break;
                case "1":
                    dr["FLAG"] = "CMS后台批量生成";
                    break;
                case "2":
                    dr["FLAG"] = "CMS手动发券给用户";
                    break;                
                default:
                    dr["FLAG"] = "后台批量生成";
                    break;
            }
            //dr["FLAG"] = dt0.Rows[i]["FLAG"].ToString();

            dr["UseTime"] = dt0.Rows[i]["UseTime"].ToString();
            dr["CNFNUM"] = dt0.Rows[i]["CNFNUM"].ToString();
            dt.Rows.Add(dr);
        }
        return dt;    
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string ticketcode = hidTicketCode.Value;
        DataSet dsResult = ExportTicketUserCode(ticketcode);
        CommonFunction.ExportExcelForLM(dsResult);
    }

    private DataSet ExportTicketUserCode(string ticketcode)
    {
        string sql = "select * from (select u.ID,u.TICKETCODE,u.USERID,u.TICKETUSERCODE,u.STATUS,u.TICKETAMT,u.CREATETIME,u.PACKAGECODE,u.FLAG,s.CREATETIME UseTime,s.CNFNUM from T_LM_TICKET_USER u ,T_LM_TICKET_SUB s where u.TICKETCODE ='" + ticketcode + "'";
        sql += " and u.TICKETUSERCODE = s.TICKETUSERCODE(+)) ";
        if (!String.IsNullOrEmpty(gridViewTicketUseInfo.Attributes["SortExpression"]) && !String.IsNullOrEmpty(gridViewTicketUseInfo.Attributes["SortDirection"]))
        {
            sql += " ORDER BY " + gridViewTicketUseInfo.Attributes["SortExpression"] + " " + gridViewTicketUseInfo.Attributes["SortDirection"];
        }
        DataSet ds = DbHelperOra.Query(sql, false);
        DataTable dt0 = ds.Tables[0];
        //手动创建DataTable表
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TICKETCODE");
        dt.Columns.Add("USERID");
        dt.Columns.Add("TICKETUSERCODE");
        dt.Columns.Add("STATUS");
        dt.Columns.Add("TICKETAMT");
        dt.Columns.Add("CREATETIME");
        dt.Columns.Add("PACKAGECODE");
        dt.Columns.Add("FLAG");
        dt.Columns.Add("UseTime");
        dt.Columns.Add("CNFNUM");

        for (int i = 0; i < dt0.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = dt0.Rows[i]["ID"].ToString();
            dr["TICKETCODE"] = dt0.Rows[i]["TICKETCODE"].ToString();
            dr["USERID"] = dt0.Rows[i]["USERID"].ToString();
            dr["TICKETUSERCODE"] = dt0.Rows[i]["TICKETUSERCODE"].ToString();

            //0-有效,1-无效,2-过期,3-已使用
            string strStatus = dt0.Rows[i]["STATUS"].ToString();
            switch (strStatus)
            {
                case "0":
                    dr["STATUS"] = "有效";
                    break;
                case "1":
                    dr["STATUS"] = "无效";
                    break;
                case "2":
                    dr["STATUS"] = "过期";
                    break;
                case "3":
                    dr["STATUS"] = "已使用";
                    break;
                default:
                    dr["STATUS"] = "有效";
                    break;
            }
            //dr["STATUS"] = dt0.Rows[i]["STATUS"].ToString();


            dr["TICKETAMT"] = dt0.Rows[i]["TICKETAMT"].ToString();
            dr["PACKAGECODE"] = dt0.Rows[i]["PACKAGECODE"].ToString();
            dr["CREATETIME"] = dt0.Rows[i]["CREATETIME"].ToString();

            string strFlag = dt0.Rows[i]["FLAG"].ToString().Trim();
            switch (strFlag)
            {
                case "0":
                    dr["FLAG"] = "前台领用";
                    break;
                case "1":
                    dr["FLAG"] = "CMS后台批量生成";
                    break;
                case "2":
                    dr["FLAG"] = "CMS手动发券给用户";
                    break;
                default:
                    dr["FLAG"] = "后台批量生成";
                    break;
            }
            //dr["FLAG"] = dt0.Rows[i]["FLAG"].ToString();

            dr["UseTime"] = dt0.Rows[i]["UseTime"].ToString();
            dr["CNFNUM"] = dt0.Rows[i]["CNFNUM"].ToString();
            dt.Rows.Add(dr);
        }
        return ds;
    }
    
    //翻页程序
    //protected void gridViewTicketUseInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    this.gridViewTicketUseInfo.PageIndex = e.NewPageIndex;

    //    object objRuleCode = ViewState["rulecode"];
    //    if (objRuleCode != null)
    //    {
    //        string[] arr = objRuleCode.ToString().Split(','); 
    //        string strTicketCode = arr[1];
    //        BindTicketUserCode(strTicketCode);
    //    }       
    //}

    /// <summary>
    /// 计算统计结果
    /// </summary>
    private void setCalcValue(string ticketcode)
    {
        //总领用用户数       
        string sqlPick = "select Count(USERID) pickAllCount from t_lm_ticket_user where TICKETCODE = '" + ticketcode + "'";
        object objPickAllCount = DbHelperOra.GetSingle(sqlPick, false);      
        this.lblAllPickUser.Text = objPickAllCount.ToString();

        //总使用用户数 
        int intAllUseCount = 0;
        //string sqlAllTicket = " select TICKETUSERCODE from t_lm_ticket_user where TICKETCODE = '" + ticketcode + "'";
        //DataSet ds = DbHelperOra.Query(sqlAllTicket);

        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    string ticketusecode = ds.Tables[0].Rows[i]["TICKETUSERCODE"].ToString();
        //    string sqlAllUseCount = " select Count(distinct USERID) from t_lm_ticket_sub where TICKETUSERCODE ='" + ticketusecode + "'";
        //    object objAllUseCount = DbHelperOra.GetSingle(sqlAllUseCount);
        //    intAllUseCount += Convert.ToInt32(objAllUseCount);        
        //}

        string sqlAllUseCount = " select Count(distinct tlts.USERID) from t_lm_ticket_user tltu inner join t_lm_ticket_sub tlts on tltu.ticketusercode = tlts.ticketusercode where tltu.TICKETCODE = '" + ticketcode + "'";
        object objAllUseCount = DbHelperOra.GetSingle(sqlAllUseCount, false);
        intAllUseCount += Convert.ToInt32(objAllUseCount);
        this.lblAllUseUser.Text = intAllUseCount.ToString();
      

        //产生订单数
        int intAllOrderCount = 0;
        //for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        //{
        //    string ticketusecode1 = ds.Tables[0].Rows[j]["TICKETUSERCODE"].ToString();
        //    string sqlAllOrderCount = " select Count(distinct USERID) from t_lm_ticket_sub where TICKETUSERCODE ='" + ticketusecode1 + "' and STATUS=0";

        //    object objAllOrderCount = DbHelperOra.GetSingle(sqlAllOrderCount);
        //    intAllOrderCount += Convert.ToInt32(objAllOrderCount);           
        //}
        string sqlAllOrderCount = " select Count(distinct tlts.USERID) from t_lm_ticket_user tltu inner join t_lm_ticket_sub tlts on tltu.ticketusercode = tlts.ticketusercode where tlts.STATUS=0 AND tltu.TICKETCODE = '" + ticketcode + "'";

        object objAllOrderCount = DbHelperOra.GetSingle(sqlAllOrderCount, false);
        intAllOrderCount += Convert.ToInt32(objAllOrderCount);
        this.lblAllOrderCount.Text = intAllOrderCount.ToString();


        //有效订单数
        int intAllEffectiveOrderCount = 0;
        //for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
        //{
        //    string ticketusecode2 = ds.Tables[0].Rows[k]["TICKETUSERCODE"].ToString();
        //    string sqlAllEffectiveOrderCount = "select count(USERID) from ((select  USERID,CNFNUM from t_lm_ticket_sub sub where TICKETUSERCODE ='" + ticketusecode2 + "' and STATUS=0) s left join ";
        //    sqlAllEffectiveOrderCount += "(select  FOG_ORDER_NUM from t_lm_order where PAY_STATUS=1 and BOOK_STATUS=5) o  on s.CNFNUM = o.FOG_ORDER_NUM)";

        //    object objAllEffectiveOrderCount = DbHelperOra.GetSingle(sqlAllEffectiveOrderCount);
        //    intAllEffectiveOrderCount += Convert.ToInt32(objAllEffectiveOrderCount);                  
        //}
        string sqlAllEffectiveOrderCount = "select Count(distinct tlts.USERID) from t_lm_ticket_user tltu inner join t_lm_ticket_sub tlts on tltu.ticketusercode = tlts.ticketusercode inner join t_lm_order tlo on tlts.CNFNUM = tlo.fog_order_num where tlts.STATUS=0 and tlo.PAY_STATUS=1 and tlo.BOOK_STATUS=5 AND tltu.TICKETCODE = '" + ticketcode + "'";
        object objAllEffectiveOrderCount = DbHelperOra.GetSingle(sqlAllEffectiveOrderCount, false);
        intAllEffectiveOrderCount += Convert.ToInt32(objAllEffectiveOrderCount);
        this.lblAllEffectiveOrderCount.Text = intAllEffectiveOrderCount.ToString();   
    
    }
}