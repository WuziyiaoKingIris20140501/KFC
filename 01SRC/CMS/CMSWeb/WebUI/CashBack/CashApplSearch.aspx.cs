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

public partial class WebUI_CashBack_CashApplSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetEmptyDataTable();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string phoneNumber = this.txtPhoneNumber.Text;
        string startCreateDate = this.dtStartCreateDate.Value;
        string endCreateDate = this.dtEndCreateDate.Value;
        string startProcessDate = this.dtStartProcessDate.Value;
        string endProcessDate = this.dtEndProcessDate.Value;
        string applicateMode = this.ddlAppMode.SelectedValue;
        string processStatus = this.ddlProcessStatus.SelectedValue;

        ViewState["phoneNumber"] = phoneNumber;
        ViewState["startCreateDate"] = startCreateDate;
        ViewState["endCreateDate"] = endCreateDate;
        ViewState["startProcessDate"] = startProcessDate;
        ViewState["endProcessDate"] = endProcessDate;
        ViewState["applicateMode"] = applicateMode;
        ViewState["processStatus"] = processStatus;

        BindLToCash();
    }

    private void SetEmptyDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn User_ID_dc = new DataColumn("USER_ID");
        DataColumn Pick_Cash_Amount_dc = new DataColumn("PICK_CASH_AMOUNT");
        DataColumn Cash_Way_dc = new DataColumn("CASH_WAY");
        DataColumn Applicate_Time_dc = new DataColumn("APPLICATE_TIME");
        DataColumn PROCESS_STATUS_dc = new DataColumn("PROCESS_STATUS");
        DataColumn Process_UserID_dc = new DataColumn("PROCESS_USERID");
        DataColumn STATUS_dc = new DataColumn("PROCESS_TIME");

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(User_ID_dc);
        dt.Columns.Add(Pick_Cash_Amount_dc);
        dt.Columns.Add(Cash_Way_dc);
        dt.Columns.Add(Applicate_Time_dc);
        dt.Columns.Add(PROCESS_STATUS_dc);
        dt.Columns.Add(Process_UserID_dc);
        dt.Columns.Add(STATUS_dc);
        GridviewControl.GridViewDataBind(this.gridViewCash, dt);
    }

    public DataTable createDataTable()
    {
        DataTable getDataTable = getCashData().Tables[0];
        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn User_ID_dc = new DataColumn("USER_ID");
        DataColumn Pick_Cash_Amount_dc = new DataColumn("PICK_CASH_AMOUNT");
        DataColumn Cash_Way_dc = new DataColumn("CASH_WAY");
        DataColumn Applicate_Time_dc = new DataColumn("APPLICATE_TIME");
        DataColumn PROCESS_STATUS_dc = new DataColumn("PROCESS_STATUS");
        DataColumn Process_UserID_dc = new DataColumn("PROCESS_USERID");
        DataColumn STATUS_dc = new DataColumn("PROCESS_TIME");

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(User_ID_dc);
        dt.Columns.Add(Pick_Cash_Amount_dc);
        dt.Columns.Add(Cash_Way_dc);
        dt.Columns.Add(Applicate_Time_dc);
        dt.Columns.Add(PROCESS_STATUS_dc);
        dt.Columns.Add(Process_UserID_dc);
        dt.Columns.Add(STATUS_dc);

        for (int i = 0; i < getDataTable.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = getDataTable.Rows[i]["ID"];
            dr["USER_ID"] = getDataTable.Rows[i]["USER_ID"];
            dr["PICK_CASH_AMOUNT"] = getDataTable.Rows[i]["PICK_CASH_AMOUNT"];

            string cashway = getDataTable.Rows[i]["CASH_WAY"].ToString().Trim();//提现用--申请提现方式：1-现金返还；2-支付宝返还；3-手机充值
            switch (cashway)
            {
                case "1":
                    dr["CASH_WAY"] = "现金返还";
                    break;
                case "2":
                    dr["CASH_WAY"] = "支付宝返还";
                    break;
                case "3":
                    dr["CASH_WAY"] = "手机充值";
                    break;
                default:
                    dr["CASH_WAY"] = "没有选择";
                    break;
            }

            dr["APPLICATE_TIME"] = getDataTable.Rows[i]["APPLICATE_TIME"];
            string processStatus = getDataTable.Rows[i]["PROCESS_STATUS"].ToString().Trim();//0-已提交；1-已审核；2-已成功；3-已失败
            switch (processStatus)
            {
                case "0":
                    dr["PROCESS_STATUS"] = "已提交";
                    break;
                case "1":
                    dr["PROCESS_STATUS"] = "已审核";
                    break;
                case "3":
                    dr["PROCESS_STATUS"] = "已成功";
                    break;
                case "2":
                    dr["PROCESS_STATUS"] = "已失败";
                    break;
                default:
                    dr["PROCESS_STATUS"] = "没有选择";
                    break;
            }

            dr["PROCESS_USERID"] = getDataTable.Rows[i]["PROCESS_USERID"];
            dr["PROCESS_TIME"] = getDataTable.Rows[i]["PROCESS_TIME"];
            dt.Rows.Add(dr);
        }
        return dt;

    }

    private void BindLToCash()
    {
        DataTable dt = createDataTable();
        GridviewControl.GridViewDataBind(this.gridViewCash, dt);
    }

    private DataSet getCashData()
    {
        string sql = string.Empty;

        //sql = "select ID,SN,USER_ID,PICK_CASH_AMOUNT,PROCESS_USERID,CASH_WAY,APPLICATE_STATUS,PROCESS_STATUS,APPLICATE_TIME,PROCESS_TIME from T_LM_CASH_TOCASH_APPL where 1=1";
        //sql += " and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber))";
        //sql += " and ((:startCreateDate IS NULL) OR (APPLICATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:endCreateDate IS NULL) OR (APPLICATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') ))";
        //sql += " and ((:startProcessDate IS NULL) OR (PROCESS_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:endProcessDate IS NULL) OR (PROCESS_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:applicateMode is NULL) or (APPLICATE_STATUS=:applicateMode))";
        //sql += " and ((:processStatus is NULL) or (PROCESS_STATUS=:processStatus))";
        //sql += " order by APPLICATE_TIME desc";

        sql = "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,APPLY_TYPE AS CASH_WAY,STATUS AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME from t_lm_cash where TYPE=1 and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and ((:startProcessDate IS NULL) OR (UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endProcessDate IS NULL) OR (UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:applicateMode is NULL) or (APPLY_TYPE=:applicateMode)) and ((:processStatus is NULL) or (STATUS=:processStatus)) order by CREATE_TIME desc";

        string phoneNumber = ViewState["phoneNumber"].ToString();
        string startCreateDate = ViewState["startCreateDate"].ToString();
        string endCreateDate = ViewState["endCreateDate"].ToString();
        string startProcessDate = ViewState["startProcessDate"].ToString();
        string endProcessDate = ViewState["endProcessDate"].ToString();
        string applicateMode = ViewState["applicateMode"].ToString();
        string processStatus = ViewState["processStatus"].ToString();

        OracleParameter[] parm ={
                                    new OracleParameter("phoneNumber",OracleType.VarChar), 
                                    new OracleParameter("startCreateDate",OracleType.VarChar),     
                                    new OracleParameter("endCreateDate",OracleType.VarChar),
                                    new OracleParameter("startProcessDate",OracleType.VarChar),
                                    new OracleParameter("endProcessDate",OracleType.VarChar),
                                    new OracleParameter("applicateMode",OracleType.VarChar),
                                    new OracleParameter("processStatus",OracleType.VarChar) 
                                };

        if (String.IsNullOrEmpty(phoneNumber))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = phoneNumber;
        }

        if (String.IsNullOrEmpty(startCreateDate))
        {
            parm[1].Value = DBNull.Value;
        }
        else
        {
            parm[1].Value = startCreateDate;
        }

        if (String.IsNullOrEmpty(endCreateDate))
        {
            parm[2].Value = DBNull.Value;
        }
        else
        {
            parm[2].Value = endCreateDate;
        }

        if (String.IsNullOrEmpty(startProcessDate))
        {
            parm[3].Value = DBNull.Value;
        }
        else
        {
            parm[3].Value = startProcessDate;
        }

        if (String.IsNullOrEmpty(endProcessDate))
        {
            parm[4].Value = DBNull.Value;
        }
        else
        {
            parm[4].Value = endProcessDate;
        }

        if (String.IsNullOrEmpty(applicateMode))
        {
            parm[5].Value = DBNull.Value;
        }
        else
        {
            parm[5].Value = applicateMode;
        }

        if (String.IsNullOrEmpty(processStatus))
        {
            parm[6].Value = DBNull.Value;
        }
        else
        {
            parm[6].Value = processStatus;
        }

        DataSet ds = DbHelperOra.Query(sql, false, parm);
        return ds;
    }

    protected void gridViewCash_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCash.PageIndex = e.NewPageIndex;
        BindLToCash();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = createDataTable();
            DataSet ds0 = new DataSet("cashsearch");
            ds0.Tables.Add(dt);

            if (ds0 == null)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
                return;

            }
            if (ds0.Tables[0].Rows.Count <= 0)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
                return;
            }

            CommonFunction.ExportExcelForLM(ds0);
        }
        catch
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
            return;
        }
    }
}