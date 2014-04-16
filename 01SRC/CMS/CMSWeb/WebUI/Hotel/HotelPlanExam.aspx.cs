using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;


public partial class WebUI_Hotel_HotelPlanExam : BasePage
{
    public static string NoLimitText = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        NoLimitText = Resources.MyGlobal.NoLimitText;
        if (!IsPostBack)
        {     
            BindTaskTypeDDL();
            SetEmptyDataTable();

            //把统计的信息
            divExamCount.InnerHtml = getTaskCount();

            //查询未处理的审核单
            ViewState["TASK_CODE"] = "";
            ViewState["TASK_STATUS"] = "0";//未处理
            ViewState["TASK_APPROVEREJECT"] = "";;
            ViewState["STARTDATE"] = "";
            ViewState["ENDDATE"] = "";            
            BindTaskList();
        }
    }
    
    //任务列表信息
    private void BindTaskTypeDDL()
    {
        DataSet ds = CommonFunction.getDataFromXml("~/Config/TaskType.xml");
        DataTable myTable = ds.Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["code"] = "";
        row1["name"] = NoLimitText;// "不限制";
        myTable.Rows.InsertAt(row1, 0);

        //myTable.Rows.Add(row1);
        ddlExamType.DataTextField = "name";
        ddlExamType.DataValueField = "code";
        ddlExamType.DataSource = myTable;
        ddlExamType.DataBind();
    }

    //private void BindTaskList()
    //{
    //    string sql = "select t.* from t_lm_proc_exam t ";
    //    DataSet ds = DbHelperOra.Query(sql);
    //    this.gridViewHotelPlanExam.DataSource = ds.Tables[0];
    //    this.gridViewHotelPlanExam.DataBind(); 
    //}

    private void BindTaskList()
    {
        DataTable dtTask = getTaskData();
        GridviewControl.GridViewDataBind(this.gridViewHotelPlanExam, dtTask); 
    }

    private DataTable getTaskData()
    {
        string sql = string.Empty;

        string task_code = ViewState["TASK_CODE"].ToString();
        string task_status = ViewState["TASK_STATUS"].ToString();
        string task_approve = ViewState["TASK_APPROVEREJECT"].ToString();
        string startdate = ViewState["STARTDATE"].ToString();
        string enddate = ViewState["ENDDATE"].ToString();

        sql = "select distinct t.TASK_ID,t.TASK_NAME,t.TASK_CODE,t.TASK_STATUS,t.TASK_APPROVEREJECT,t.TASK_CURPROCUSER,t.TASK_CREATETIME,t.TASK_UPDATETIME,t.TASK_CREATEBY,d.CREATE_TIME,d.REFID from t_lm_proc_exam t,t_lm_proc_exam_detail d where 1=1 ";
        sql += " and t.TASK_ID = d.task_id ";
        sql += " and ((:TASK_CODE IS NULL) OR  (t.TASK_CODE = :TASK_CODE))";
        sql += " and ((:TASK_STATUS IS NULL) OR  (t.TASK_STATUS = :TASK_STATUS))";
        sql += " and ((:TASK_APPROVEREJECT IS NULL) OR  (t.TASK_APPROVEREJECT = :TASK_APPROVEREJECT))";
        sql += " and ((:STARTDATE IS NULL) OR  (t.TASK_CREATETIME >= to_timestamp(:STARTDATE,'yyyy-MM-dd hh24:mi:ss.ff')))";
        sql += " and ((:ENDDATE IS NULL) OR  (t.TASK_CREATETIME <= to_timestamp(:ENDDATE,'yyyy-MM-dd hh24:mi:ss.ff')))";
        sql += "  order by t.TASK_CREATETIME desc";
        
        //to_date(:EFFECT_DATE,'yyyy-MM-dd')

        OracleParameter[] parm ={
                                    new OracleParameter("TASK_CODE",OracleType.VarChar), 
                                    new OracleParameter("TASK_STATUS",OracleType.VarChar),     
                                    new OracleParameter("TASK_APPROVEREJECT",OracleType.VarChar),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar)
                                };

        if (String.IsNullOrEmpty(task_code))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = task_code;
        }
        if (String.IsNullOrEmpty(task_status))
        {
            parm[1].Value = DBNull.Value;
        }
        else
        {
            parm[1].Value = task_status;
        }

        if (String.IsNullOrEmpty(task_approve))
        {
            parm[2].Value = DBNull.Value;
        }
        else
        {
            parm[2].Value = task_approve;
        }

        if (String.IsNullOrEmpty(startdate))
        {
            parm[3].Value = DBNull.Value;
        }
        else
        {
            parm[3].Value = startdate;
        }

        if (String.IsNullOrEmpty(enddate))
        {
            parm[4].Value = DBNull.Value;
        }
        else
        {
            DateTime tempEndDate = Convert.ToDateTime(enddate).AddDays(1);
            string strEndDate = string.Format("{0:yyyy-MM-dd}", tempEndDate);
            parm[4].Value = strEndDate;

        }

        DataSet ds = DbHelperOra.Query(sql, false, parm);
        DataTable dt0 = ds.Tables[0];

        //手动创建DataTable表
        DataTable dt = new DataTable();
        dt.Columns.Add("TASK_ID");
        dt.Columns.Add("TASK_NAME");
        dt.Columns.Add("TASK_CREATETIME");
        dt.Columns.Add("TASK_STATUS");        
        dt.Columns.Add("TASK_CREATEBY");
        dt.Columns.Add("TASK_APPROVEREJECT");
        dt.Columns.Add("TASK_CURPROCUSER");
        dt.Columns.Add("TASK_UPDATETIME");
        dt.Columns.Add("REFID");

        for (int i = 0; i < dt0.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();

            dr["TASK_ID"] = dt0.Rows[i]["TASK_ID"].ToString();
            dr["TASK_NAME"] = dt0.Rows[i]["TASK_NAME"].ToString();
            dr["TASK_CREATETIME"] = dt0.Rows[i]["TASK_CREATETIME"].ToString();

            string strTaskStatus = string.Empty;
            if (dt0.Rows[i]["TASK_STATUS"] == DBNull.Value)
            {
                strTaskStatus = "未处理";
            }
            else 
            {
                if (dt0.Rows[i]["TASK_STATUS"].ToString() == "1")
                {
                    strTaskStatus = "已处理";
                }
                else
                {
                    strTaskStatus = "未处理";
                }           
            }

            dr["TASK_STATUS"] = strTaskStatus;
            dr["TASK_CREATEBY"] = dt0.Rows[i]["TASK_CREATEBY"].ToString();

            string strApproveStatus = string.Empty;
            if (dt0.Rows[i]["TASK_APPROVEREJECT"] == DBNull.Value)
            {
                strApproveStatus = "未处理";
            }
            else
            {
                if (dt0.Rows[i]["TASK_APPROVEREJECT"].ToString() == "1")
                {
                    strApproveStatus = "同意";
                }
                else if (dt0.Rows[i]["TASK_APPROVEREJECT"].ToString() == "0")
                {
                    strApproveStatus = "拒绝";
                }
                else
                {
                    strApproveStatus = "已失效";
                }
            }

            dr["TASK_APPROVEREJECT"] = strApproveStatus;
            dr["TASK_CURPROCUSER"] = dt0.Rows[i]["TASK_CURPROCUSER"].ToString();
            dr["TASK_UPDATETIME"] = dt0.Rows[i]["TASK_UPDATETIME"].ToString();
            dr["REFID"] = dt0.Rows[i]["REFID"].ToString();            
            dt.Rows.Add(dr);
        }

        //DataSet ds = HotelVp.Common.DBUtility.DbManager.Query("OrderInfo", "t_lm_orderinfo_select", parm);
        return dt;
    }

    //点击搜索按钮
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string examType = this.ddlExamType.SelectedValue;
        string processStatus= this.ddlProccess.SelectedValue;
        string processResult = this.ddlProcessResult.SelectedValue;
        string startDate = this.dtStartDate.Value;
        string endDate = this.dtEndDate.Value;

        ViewState["TASK_CODE"] = examType;
        ViewState["TASK_STATUS"] = processStatus;
        ViewState["TASK_APPROVEREJECT"] = processResult;
        ViewState["STARTDATE"] = startDate;
        ViewState["ENDDATE"] = endDate;

        BindTaskList(); 
    }

    private void SetEmptyDataTable()
    {        
        DataTable dt = new DataTable();
        DataColumn TASK_ID_dc = new DataColumn("TASK_ID");
        DataColumn TASK_NAME_dc = new DataColumn("TASK_NAME");
        DataColumn TASK_CREATETIME_dc = new DataColumn("TASK_CREATETIME");
        DataColumn TASK_CREATEBY_dc = new DataColumn("TASK_CREATEBY");
        DataColumn TASK_STATUS_dc = new DataColumn("TASK_STATUS");
        DataColumn TASK_APPROVEREJECT_dc = new DataColumn("TASK_APPROVEREJECT");
        DataColumn TASK_CURPROCUSER_dc = new DataColumn("TASK_CURPROCUSER");
        DataColumn TASK_UPDATETIME_dc = new DataColumn("TASK_UPDATETIME");
        DataColumn REFID_dc = new DataColumn("REFID");
        

        dt.Columns.Add(TASK_ID_dc);
        dt.Columns.Add(TASK_NAME_dc);
        dt.Columns.Add(TASK_CREATETIME_dc);
        dt.Columns.Add(TASK_CREATEBY_dc);
        dt.Columns.Add(TASK_STATUS_dc);
        dt.Columns.Add(TASK_APPROVEREJECT_dc);
        dt.Columns.Add(TASK_CURPROCUSER_dc);
        dt.Columns.Add(TASK_UPDATETIME_dc);
        dt.Columns.Add(REFID_dc);

        GridviewControl.GridViewDataBind(this.gridViewHotelPlanExam, dt);
    }

    private string getTaskCount()
    {
        string sql = " select Task_Name,Count(Task_Name) as allCount from t_lm_proc_exam  where  Task_Status=0 group by Task_Name";
        DataSet ds = DbHelperOra.Query(sql, false);
        string strTab = "<table width='100%' style='color:Blue;font-weight:bold'>";
        strTab += "<tr>";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            strTab += "<td>";
            strTab += ds.Tables[0].Rows[i]["Task_Name"].ToString();
            strTab += "：";
            strTab += ds.Tables[0].Rows[i]["allCount"].ToString();
            strTab += "</td>";
        }
        strTab += "</tr>";
        strTab += "</table>";
        return strTab;
    }
}