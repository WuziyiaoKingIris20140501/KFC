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

public partial class WebUI_CashBack_CashChangeSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetEmptyDataTable();

            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string UserID = (Request.QueryString["ID"] != null && !String.IsNullOrEmpty(Request.QueryString["ID"])) ? Request.QueryString["ID"].ToString() : "";
                string strType = (Request.QueryString["TYPE"] != null && !String.IsNullOrEmpty(Request.QueryString["TYPE"])) ? Request.QueryString["TYPE"].ToString() : "";

                ViewState["UserID"] = UserID;
                ViewState["StartCreateDate"] = "";
                ViewState["EndCreateDate"] = "";
                ViewState["SourceType"] = "";
                ViewState["Status"] = ("0".Equals(strType)) ? "" : "0";
                ViewState["BackType"] = strType;
                AspNetPager1.CurrentPageIndex = 1;
                BindLToCash();
            }
        }
    }

    private void SetEmptyDataTable()
    {       
        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn SOURCE_TYPE_dc = new DataColumn("SOURCE_TYPE");
        DataColumn SOURCE_CONTENT_dc = new DataColumn("SOURCE_CONTENT");
        DataColumn USER_ID_dc = new DataColumn("USER_ID");
        DataColumn SOURCE_AMOUNT_dc = new DataColumn("SOURCE_AMOUNT");
        DataColumn CREATE_TIME_dc = new DataColumn("CREATE_TIME");
        DataColumn STATUS_dc = new DataColumn("STATUS");
        DataColumn SELTYPE_dc = new DataColumn("SELTYPE");

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(SOURCE_TYPE_dc);
        dt.Columns.Add(SOURCE_CONTENT_dc);
        dt.Columns.Add(USER_ID_dc);
        dt.Columns.Add(SOURCE_AMOUNT_dc);
        dt.Columns.Add(CREATE_TIME_dc);
        dt.Columns.Add(STATUS_dc);
        dt.Columns.Add(SELTYPE_dc); 
 
        GridviewControl.GridViewDataBind(this.gridViewCash, dt);
    }

    //protected void gridViewCash_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    this.gridViewCash.PageIndex = e.NewPageIndex;
    //    BindLToCash();
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string UserID = this.txtUserID.Text;
        string StartCreateDate = this.dtStartCreateDate.Value;
        string EndCreateDate = this.dtEndCreateDate.Value;
        string SourceType = this.ddlSourceType.SelectedValue;
        string Status = this.ddlProcessStatus.SelectedValue;

        ViewState["UserID"] = UserID;
        ViewState["StartCreateDate"] = StartCreateDate;
        ViewState["EndCreateDate"] = EndCreateDate;
        ViewState["SourceType"] = SourceType;
        ViewState["Status"] = Status;
        ViewState["BackType"] = "";
        AspNetPager1.CurrentPageIndex = 1;
        BindLToCash();
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindLToCash();
    }
    
    #region 绑定数据
    public DataTable exportDataTable()
    {
        DataTable getDataTable = getCashLogCount().Tables[0];

        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn SOURCE_TYPE_dc = new DataColumn("SOURCE_TYPE");
        DataColumn SOURCE_CONTENT_dc = new DataColumn("SOURCE_CONTENT");
        DataColumn USER_ID_dc = new DataColumn("USER_ID");
        DataColumn SOURCE_AMOUNT_dc = new DataColumn("SOURCE_AMOUNT");
        DataColumn CREATE_TIME_dc = new DataColumn("CREATE_TIME");
        DataColumn STATUS_dc = new DataColumn("STATUS");
        DataColumn SELTYPE_dc = new DataColumn("SELTYPE");
        

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(SOURCE_TYPE_dc);
        dt.Columns.Add(SOURCE_CONTENT_dc);
        dt.Columns.Add(USER_ID_dc);
        dt.Columns.Add(SOURCE_AMOUNT_dc);
        dt.Columns.Add(CREATE_TIME_dc);
        dt.Columns.Add(STATUS_dc);
        dt.Columns.Add(SELTYPE_dc); 

        for (int i = 0; i < getDataTable.Rows.Count; i++)
        {
           // PKEY,CHREASON,CHAMOUNT,CHDTIME,CHTYPE,USER_ID 
            DataRow dr = dt.NewRow();
            string strPKEY = getDataTable.Rows[i]["PKEY"].ToString();
            dr["ID"] = strPKEY;            
            string source_type = getDataTable.Rows[i]["CHREASON"].ToString();           
            string strSouceType = string.Empty;
            string sourceContent = "";
            switch (source_type)
            {
                case "2":
                    strSouceType = "订单返现";
                    sourceContent = "订单返现" + "-" + strPKEY;                   
                    break;
                case "1":
                    strSouceType = "返现券返现";
                    sourceContent ="返现券返现"+ "-" + strPKEY;;
                    break;
                case "3":
                    if (String.IsNullOrEmpty(getDataTable.Rows[i]["SOURCECHANNEL"].ToString()))
                    {
                        strSouceType = "用户提现";
                        sourceContent = "用户提现" + "-" + strPKEY;
                    }
                    else
                    {
                        strSouceType = "CMS提现";
                        sourceContent = "CMS提现" + "-" + strPKEY;
                    }
                    break;
                default:
                    strSouceType = "其它";
                    sourceContent ="其它"+ "-" + strPKEY;;
                    break;
            }

            dr["SOURCE_TYPE"] = strSouceType;//变动类型
            //dr["SOURCE_CONTENT"] = getDataTable.Rows[i]["SOURCE_CONTENT"];
            dr["SOURCE_CONTENT"] = sourceContent;
            dr["USER_ID"] = getDataTable.Rows[i]["USER_ID"];
            dr["SOURCE_AMOUNT"] = getDataTable.Rows[i]["CHAMOUNT"];
            dr["CREATE_TIME"] = getDataTable.Rows[i]["CHDTIME"];

            string strSelType = getDataTable.Rows[i]["SELTYPE"].ToString();//0--表示账号订单关联的信息；1-表示取提现表的信息
            string strStatus = getDataTable.Rows[i]["CHTYPE"].ToString();//状态值。0-表示
            string strTempStatus = string.Empty;
            if (strSelType == "0")
            { 
                switch (strStatus)
                {
                    case "0":
                        strTempStatus = "已提交";
                        break;
                    case "1":
                        strTempStatus = "已审核";
                        break;
                    case "2":
                        strTempStatus = "已失败";
                        break;
                    case "3":
                        strTempStatus = "已成功";
                        break;
                    default:
                        strTempStatus = "无";
                        break;
                }
            }
            
            if (strSelType == "1")
            {
                switch (strStatus)
                {
                    case "0":
                        strTempStatus = "已提交";
                        break;
                    case "1":
                        strTempStatus = "已审核";
                        break;
                    case "2":
                        strTempStatus = "已失败";
                        break;
                    case "3":
                        strTempStatus = "已成功";
                        break;
                    default:
                         strTempStatus = "无";
                        break;

                }
            }            
            dr["STATUS"] = strTempStatus;
            dr["SELTYPE"] = getDataTable.Rows[i]["SELTYPE"];
         
            dt.Rows.Add(dr);
        }
        return dt;
    }
    public DataTable createDataTable()
    {
        DataTable getDataTable = getCashData().Tables[0];

        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn SOURCE_TYPE_dc = new DataColumn("SOURCE_TYPE");
        DataColumn SOURCE_CONTENT_dc = new DataColumn("SOURCE_CONTENT");
        DataColumn USER_ID_dc = new DataColumn("USER_ID");
        DataColumn SOURCE_AMOUNT_dc = new DataColumn("SOURCE_AMOUNT");
        DataColumn CREATE_TIME_dc = new DataColumn("CREATE_TIME");
        DataColumn STATUS_dc = new DataColumn("STATUS");
        DataColumn SELTYPE_dc = new DataColumn("SELTYPE");
        

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(SOURCE_TYPE_dc);
        dt.Columns.Add(SOURCE_CONTENT_dc);
        dt.Columns.Add(USER_ID_dc);
        dt.Columns.Add(SOURCE_AMOUNT_dc);
        dt.Columns.Add(CREATE_TIME_dc);
        dt.Columns.Add(STATUS_dc);
        dt.Columns.Add(SELTYPE_dc); 

        for (int i = 0; i < getDataTable.Rows.Count; i++)
        {
           // PKEY,CHREASON,CHAMOUNT,CHDTIME,CHTYPE,USER_ID 
            DataRow dr = dt.NewRow();
            string strPKEY = getDataTable.Rows[i]["PKEY"].ToString();
            dr["ID"] = strPKEY;            
            string source_type = getDataTable.Rows[i]["CHREASON"].ToString();           
            string strSouceType = string.Empty;
            string sourceContent = "";
            switch (source_type)
            {
                case "2":
                    strSouceType = "订单返现";
                    sourceContent = "订单返现" + "-" + strPKEY;                   
                    break;
                case "1":
                    strSouceType = "返现券返现";
                    sourceContent ="返现券返现"+ "-" + strPKEY;;
                    break;
                case "3":
                    if (String.IsNullOrEmpty(getDataTable.Rows[i]["SOURCECHANNEL"].ToString()))
                    {
                        strSouceType = "用户提现";
                        sourceContent = "用户提现" + "-" + strPKEY;
                    }
                    else
                    {
                        strSouceType = "CMS提现";
                        sourceContent = "CMS提现" + "-" + strPKEY;
                    }
                    break;
                default:
                    strSouceType = "其它";
                    sourceContent ="其它"+ "-" + strPKEY;;
                    break;
            }

            dr["SOURCE_TYPE"] = strSouceType;//变动类型
            //dr["SOURCE_CONTENT"] = getDataTable.Rows[i]["SOURCE_CONTENT"];
            dr["SOURCE_CONTENT"] = sourceContent;
            dr["USER_ID"] = getDataTable.Rows[i]["USER_ID"];
            dr["SOURCE_AMOUNT"] = getDataTable.Rows[i]["CHAMOUNT"];
            dr["CREATE_TIME"] = getDataTable.Rows[i]["CHDTIME"];

            string strSelType = getDataTable.Rows[i]["SELTYPE"].ToString();//0--表示账号订单关联的信息；1-表示取提现表的信息
            string strStatus = getDataTable.Rows[i]["CHTYPE"].ToString();//状态值。0-表示
            string strTempStatus = string.Empty;
            if (strSelType == "0")
            { 
                switch (strStatus)
                {
                    case "0":
                        strTempStatus = "已提交";
                        break;
                    case "1":
                        strTempStatus = "已审核";
                        break;
                    case "2":
                        strTempStatus = "已失败";
                        break;
                    case "3":
                        strTempStatus = "已成功";
                        break;
                    default:
                        strTempStatus = "无";
                        break;
                }
            }
            
            if (strSelType == "1")
            {
                switch (strStatus)
                {
                    case "0":
                        strTempStatus = "已提交";
                        break;
                    case "1":
                        strTempStatus = "已审核";
                        break;
                    case "2":
                        strTempStatus = "已失败";
                        break;
                    case "3":
                        strTempStatus = "已成功";
                        break;
                    default:
                         strTempStatus = "无";
                        break;

                }
            }            
            dr["STATUS"] = strTempStatus;
            dr["SELTYPE"] = getDataTable.Rows[i]["SELTYPE"];
         
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void BindLToCash()
    {
        DataTable dt = createDataTable();
        GridviewControl.GridViewDataBind(this.gridViewCash, dt);

        AspNetPager1.PageSize = gridViewCash.PageSize;
        DataSet dsResult = getCashLogCount();
        AspNetPager1.RecordCount = (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0) ? dsResult.Tables[0].Rows.Count : 0;
    }

    private DataSet getCashData()
    {
        string sql = string.Empty;
        //sql += " SELECT  PKEY,CHREASON,CHAMOUNT,CHDTIME,CHTYPE,USER_ID,SELTYPE from V_LM_CASH_USER where 1=1 ";

        sql += "select * from (select TO_CHAR(order_num) AS PKEY,TO_CHAR(BACK_TYPE) AS CHREASON,AMOUNT AS CHAMOUNT,    update_time AS CHDTIME,    TO_CHAR(status) AS CHTYPE，    USER_ID,    '1' AS SELTYPE, SOURCE_CHANNEL AS SOURCECHANNEL     from T_LM_CASH where TYPE=0    union all    select TO_CHAR(id) AS PKEY,    '3' AS CHREASON,    AMOUNT AS CHAMOUNT,    update_time AS CHDTIME,    TO_CHAR(status) AS CHTYPE,    USER_ID,    '0' AS SELTYPE , SOURCE_CHANNEL AS SOURCECHANNEL   from  T_LM_CASH where TYPE =1 )";
        sql += " where ((:user_id is NULL) or (USER_ID=:user_id))";
        sql += " and ((:startCreateDate IS NULL) OR (CHDTIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss')))";
        sql += " and ((:endCreateDate IS NULL) OR (CHDTIME <= to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss')))";
        sql += " and ((:sourceType is NULL) or (CHREASON=:sourceType) OR (:sourceType='3' AND SELTYPE='0'))";
        sql += " and ((:status is NULL) or (CHTYPE=:status) OR (:BackType = '0' and CHTYPE IN ('0','1')))";
        sql += " and ((:BackType is NULL) or (SELTYPE=:BackType))";
        sql += "  order by CHDTIME desc";

        string userid = ViewState["UserID"].ToString();
        string startCreateDate = ViewState["StartCreateDate"].ToString();
        string endCreateDate = ViewState["EndCreateDate"].ToString();
        string sourceType = ViewState["SourceType"].ToString();
        string status = ViewState["Status"].ToString();
        string BackType = ViewState["BackType"].ToString(); 
        OracleParameter[] parm ={
                                    new OracleParameter("user_id",OracleType.VarChar),
                                    new OracleParameter("startCreateDate",OracleType.VarChar),
                                    new OracleParameter("endCreateDate",OracleType.VarChar),
                                    new OracleParameter("sourceType",OracleType.VarChar),
                                    new OracleParameter("status",OracleType.VarChar),
                                    new OracleParameter("BackType",OracleType.VarChar)
                                };

        if (String.IsNullOrEmpty(userid))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = userid;
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

        if (String.IsNullOrEmpty(sourceType))
        {
            parm[3].Value = DBNull.Value;
        }
        else
        {
            parm[3].Value = sourceType;
        }

        if (String.IsNullOrEmpty(status))
        {
            parm[4].Value = DBNull.Value;
        }
        else
        {
            parm[4].Value = status;
        }

        if (String.IsNullOrEmpty(BackType))
        {
            parm[5].Value = DBNull.Value;
        }
        else
        {
            parm[5].Value = BackType;
        }
        //DataSet ds = DbHelperOra.Query(sql, false, parm);
        DataSet ds = DbManager.Query(sql, parm, (AspNetPager1.CurrentPageIndex - 1) * gridViewCash.PageSize, gridViewCash.PageSize, true);
        return ds;
    }

    private DataSet getCashLogCount()
    {
        string sql = string.Empty;
        //sql += " SELECT  PKEY,CHREASON,CHAMOUNT,CHDTIME,CHTYPE,USER_ID,SELTYPE from V_LM_CASH_USER where 1=1 ";

        sql += "select * from (select TO_CHAR(order_num) AS PKEY,TO_CHAR(BACK_TYPE) AS CHREASON,AMOUNT AS CHAMOUNT,    update_time AS CHDTIME,    TO_CHAR(status) AS CHTYPE，    USER_ID,    '1' AS SELTYPE , SOURCE_CHANNEL AS SOURCECHANNEL    from T_LM_CASH where TYPE=0    union all    select TO_CHAR(id) AS PKEY,    '3' AS CHREASON,    AMOUNT AS CHAMOUNT,    update_time AS CHDTIME,    TO_CHAR(status) AS CHTYPE,    USER_ID,    '0' AS SELTYPE , SOURCE_CHANNEL AS SOURCECHANNEL   from  T_LM_CASH where TYPE = 1 )";
        sql += " where ((:user_id is NULL) or (USER_ID=:user_id))";
        sql += " and ((:startCreateDate IS NULL) OR (CHDTIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss')))";
        sql += " and ((:endCreateDate IS NULL) OR (CHDTIME <= to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss')))";
        sql += " and ((:sourceType is NULL) or (CHREASON=:sourceType) OR (:sourceType='3' AND SELTYPE=1))";
        sql += " and ((:status is NULL) or (CHTYPE=:status) OR (:BackType = '0' and CHTYPE IN ('0','1')))";
        sql += " and ((:BackType is NULL) or (SELTYPE=:BackType))";
        sql += "  order by CHDTIME desc";

        string userid = ViewState["UserID"].ToString();
        string startCreateDate = ViewState["StartCreateDate"].ToString();
        string endCreateDate = ViewState["EndCreateDate"].ToString();
        string sourceType = ViewState["SourceType"].ToString();
        string status = ViewState["Status"].ToString();
        string BackType = ViewState["BackType"].ToString();
        OracleParameter[] parm ={
                                    new OracleParameter("user_id",OracleType.VarChar),
                                    new OracleParameter("startCreateDate",OracleType.VarChar),
                                    new OracleParameter("endCreateDate",OracleType.VarChar),
                                    new OracleParameter("sourceType",OracleType.VarChar),
                                    new OracleParameter("status",OracleType.VarChar),
                                    new OracleParameter("BackType",OracleType.VarChar)
                                };

        if (String.IsNullOrEmpty(userid))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = userid;
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

        if (String.IsNullOrEmpty(sourceType))
        {
            parm[3].Value = DBNull.Value;
        }
        else
        {
            parm[3].Value = sourceType;
        }

        if (String.IsNullOrEmpty(status))
        {
            parm[4].Value = DBNull.Value;
        }
        else
        {
            parm[4].Value = status;
        }

        if (String.IsNullOrEmpty(BackType))
        {
            parm[5].Value = DBNull.Value;
        }
        else
        {
            parm[5].Value = BackType;
        }
        DataSet ds = DbHelperOra.Query(sql, false, parm);
        //DataSet ds = DbManager.Query(sql, parm, (AspNetPager1.CurrentPageIndex - 1) * gridViewCash.PageSize, gridViewCash.PageSize, true);
        return ds;
    }

    #endregion



    protected void gridViewCash_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            string userid = lb.Text;//User_ID的值
            string seltype = e.CommandArgument.ToString();//SELTYPE的值，1-表示t_lm_cash_cashback，0-表示t_lm_cash_tocash_appl_detail中的信息

            ViewState["userid_sel"] = userid;
            ViewState["seltype_sel"] = seltype;
            DataTable dt = getTable(userid, seltype);
            myGridView.DataSource = dt;
            myGridView.DataBind();

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpen2();", true);
        }
    }




    #region  绑定弹出和影藏的层
    private DataTable getTable(string userid, string seltype)
    {
        DataTable dt = new DataTable();

        string sql = string.Empty;

        if (seltype == "1")
        {
            sql += "select USER_ID,ORDER_NUM,AMOUNT AS SOURCE_AMOUNT,BACK_TYPE AS SOURCE_TYPE,STATUS,CREATE_TIME, BACK_TICKET_USER_CODE AS TICKET_USER_CODE,SOURCE_CHANNEL AS SOURCECHANNEL from T_LM_CASH where TYPE=0";
            //sql = "select USER_ID,ORDER_NUM,SOURCE_AMOUNT,SOURCE_TYPE,STATUS,CREATE_TIME,TICKET_USER_CODE from T_LM_CASH_CASHBACK_DETAIL where 1=1";
            sql += " and USER_ID='" + userid + "'";
            sql += " order by CREATE_TIME desc";

            //============================================================
            DataSet ds = DbHelperOra.Query(sql, false);
            DataTable getDataTable = ds.Tables[0];
            DataColumn User_ID_dc = new DataColumn("USER_ID");
            DataColumn ORDER_NUM_dc = new DataColumn("ORDER_NUM");
            DataColumn SOURCE_AMOUNT_dc = new DataColumn("SOURCE_AMOUNT");
            DataColumn SOURCE_TYPE_dc = new DataColumn("SOURCE_TYPE");
            DataColumn STATUS_dc = new DataColumn("STATUS");
            DataColumn CREATE_TIME_dc = new DataColumn("CREATE_TIME");
            DataColumn TICKET_USER_CODE_dc = new DataColumn("TICKET_USER_CODE");

            User_ID_dc.ColumnName = "用户ID";
            ORDER_NUM_dc.ColumnName = "订单编号";
            SOURCE_AMOUNT_dc.ColumnName = "变动金额";
            SOURCE_TYPE_dc.ColumnName = "变动类型";
            STATUS_dc.ColumnName = "状态";
            CREATE_TIME_dc.ColumnName = "创建时间";
            TICKET_USER_CODE_dc.ColumnName = "优惠券号";


            dt.Columns.Add(User_ID_dc);
            dt.Columns.Add(ORDER_NUM_dc);
            dt.Columns.Add(SOURCE_AMOUNT_dc);
            dt.Columns.Add(SOURCE_TYPE_dc);
            dt.Columns.Add(STATUS_dc);
            dt.Columns.Add(CREATE_TIME_dc);
            dt.Columns.Add(TICKET_USER_CODE_dc);

            for (int i = 0; i < getDataTable.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["用户ID"] = getDataTable.Rows[i]["USER_ID"];
                dr["订单编号"] = getDataTable.Rows[i]["ORDER_NUM"];
                dr["变动金额"] = getDataTable.Rows[i]["SOURCE_AMOUNT"];

                string source_type = getDataTable.Rows[i]["SOURCE_TYPE"].ToString();
                string strSouceType = string.Empty;
                switch (source_type)
                {
                    case "1":
                        strSouceType = "订单返现";
                        break;
                    case "2":
                        strSouceType = "返现券返现";
                        break;
                    case "3":
                        if (String.IsNullOrEmpty(getDataTable.Rows[i]["SOURCECHANNEL"].ToString()))
                        {
                            strSouceType = "用户提现";
                        }
                        else
                        {
                            strSouceType = "CMS提现";
                        }
                        break;
                    default:
                        strSouceType = "其它";
                        break;
                }
                dr["变动类型"] = strSouceType;//变动类型,0-为默认值，1-订单返现；2-返现券返现
                string strStatus = getDataTable.Rows[i]["STATUS"].ToString();//状态值。0-表示
                string strTempStatus = string.Empty;
                switch (strStatus)
                {
                    case "0":
                        strTempStatus = "已提交";
                        break;
                    case "1":
                        strTempStatus = "已审核";
                        break;
                    case "2":
                        strTempStatus = "已失败";
                        break;
                    case "3":
                        strTempStatus = "已成功";
                        break;
                    default:
                        strTempStatus = "无";
                        break;
                }
                dr["状态"] = strTempStatus;
                dr["创建时间"] = getDataTable.Rows[i]["CREATE_TIME"];
                dr["优惠券号"] = getDataTable.Rows[i]["TICKET_USER_CODE"];
                dt.Rows.Add(dr);
            }
        }

        if (seltype == "0")//提现申请
        {
            //sql = "select USER_ID,PICK_CASH_AMOUNT,PROCESS_USERID,CASH_WAY,APPLICATE_STATUS,PROCESS_STATUS,APPLICATE_TIME,PROCESS_TIME from T_LM_CASH_TOCASH_APPL where 1=1";
            //sql += " and USER_ID='" + userid + "'";
            //sql += " order by APPLICATE_TIME desc";                                

            //sql = "select a.USER_ID,a.PICK_CASH_AMOUNT,d.HANDLE_STATUS,d.HANDLE_REMARK,d.PAY_MODE,d.HANDLE_TIME,d.HANDLER";
            //sql += " from T_LM_CASH_TOCASH_APPL a,T_LM_CASH_TOCASH_APPL_DETAIL d where a.USER_ID = d.USER_ID";
            //sql += " and d.USER_ID='" + userid + "'";
            //sql += " order by d.HANDLE_TIME desc";

            sql = " select a.USER_ID,a.AMOUNT AS PICK_CASH_AMOUNT,a.STATUS AS HANDLE_STATUS,a.PROCESS_REMARK AS HANDLE_REMARK, a.APPLY_TYPE AS PAY_MODE,a.UPDATE_TIME AS HANDLE_TIME,a.PROCESS_USERID AS HANDLER, SOURCE_CHANNEL AS SOURCECHANNEL from T_LM_CASH a where a.type IN (1,2)";
            sql += " and a.USER_ID='" + userid + "'";
            sql += " order by a.UPDATE_TIME desc";

            //============================================================
            DataSet ds = DbHelperOra.Query(sql, false);
            DataTable getDataTable = ds.Tables[0];

            DataColumn User_ID_dc = new DataColumn("USER_ID");
            DataColumn PICK_CASH_AMOUNT_dc = new DataColumn("PICK_CASH_AMOUNT");
            DataColumn HANDLE_STATUS_dc = new DataColumn("HANDLE_STATUS");
            DataColumn HANDLE_REMARK_dc = new DataColumn("HANDLE_REMARK");
            DataColumn PAY_MODE_dc = new DataColumn("PAY_MODE");
            DataColumn HANDLE_TIME_dc = new DataColumn("HANDLE_TIME");
            DataColumn HANDLER_dc = new DataColumn("HANDLER");

            User_ID_dc.ColumnName = "用户ID";
            PICK_CASH_AMOUNT_dc.ColumnName = "申请金额";
            HANDLE_STATUS_dc.ColumnName = "处理状态";
            HANDLE_REMARK_dc.ColumnName = "备注";
            PAY_MODE_dc.ColumnName = "支付方式";
            HANDLE_TIME_dc.ColumnName = "处理时间";
            HANDLER_dc.ColumnName = "处理人";

            dt.Columns.Add(User_ID_dc);
            dt.Columns.Add(PICK_CASH_AMOUNT_dc);
            dt.Columns.Add(HANDLE_STATUS_dc);
            dt.Columns.Add(HANDLE_REMARK_dc);
            dt.Columns.Add(PAY_MODE_dc);
            dt.Columns.Add(HANDLE_TIME_dc);
            dt.Columns.Add(HANDLER_dc);

            for (int i = 0; i < getDataTable.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["用户ID"] = getDataTable.Rows[i]["USER_ID"];
                dr["申请金额"] = getDataTable.Rows[i]["PICK_CASH_AMOUNT"];
                //0-已提交；1-已审核；2-已成功；3-已失败
                string handleStatus = getDataTable.Rows[i]["HANDLE_STATUS"].ToString().Trim();//0-默认值；1-现金返还；2-支付宝返还；3-手机充值 
                switch (handleStatus)
                {
                    case "0":
                        dr["处理状态"] = "已提交";
                        break;
                    case "1":
                        dr["处理状态"] = "已审核";
                        break;
                    case "2":
                        dr["处理状态"] = "已失败";
                        break;
                    case "3":
                        dr["处理状态"] = "已成功";
                        break;
                    default:
                        dr["处理状态"] = "没有选择";
                        break;
                }

                dr["备注"] = getDataTable.Rows[i]["HANDLE_REMARK"];
                //提现用--申请提现方式：1-现金返还；2-支付宝返还；3-手机充值
                string payMode = getDataTable.Rows[i]["PAY_MODE"].ToString().Trim();//0-已提交；1-已审核；2-已成功；3-已失败
                switch (payMode)
                {
                    case "1":
                        dr["支付方式"] = "现金返还";
                        break;
                    case "2":
                        dr["支付方式"] = "支付宝返还";
                        break;
                    case "3":
                        dr["支付方式"] = "手机充值";
                        break;
                    default:
                        dr["支付方式"] = "没有选择";
                        break;
                }

                dr["处理时间"] = getDataTable.Rows[i]["HANDLE_TIME"];
                dr["处理人"] = getDataTable.Rows[i]["HANDLER"];
                dt.Rows.Add(dr);
            }
        }
        return dt;        
    }

    private void bindDetail(string userid, string seltype)
    {
        DataTable dt = getTable(userid, seltype);
        myGridView.DataSource = dt;
        myGridView.DataBind();
    
    }
    #endregion


    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    { 
        string userid = ViewState["userid_sel"].ToString();
        string seltype= ViewState["seltype_sel"].ToString();

        myGridView.PageIndex = e.NewPageIndex;
        bindDetail(userid, seltype);

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpen2();", true);
    }


    //导出Excel文档
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = exportDataTable();// createDataTable();
            DataSet ds0 = new DataSet("changesearch");
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
           // return;
        }
    }
    protected void gridViewCash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ("已失败".Equals(e.Row.Cells[7].Text))
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Gray;
            }
            else if (e.Row.Cells[2].Text.Contains("返现"))
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
            }
            else if (e.Row.Cells[2].Text.Contains("提现"))
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}