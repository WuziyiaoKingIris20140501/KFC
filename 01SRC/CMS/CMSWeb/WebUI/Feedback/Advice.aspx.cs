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
using System.Collections;
using HotelVp.Common.DBUtility;

public partial class WebUI_Feedback_Advice : BasePage
{
    public static string strProcessedLabel = string.Empty;
    public static string strNotProcessedLabel = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {      
            strProcessedLabel = GetLocalResourceObject("ProcessedLabel").ToString();
            strNotProcessedLabel = GetLocalResourceObject("NotProcessedLabel").ToString();
            if (!IsPostBack)
            {
                string strToday = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                this.dtStartTime.Value = strToday;
                this.dtEndTime.Value = strToday;
            }
       
    }
    //点击查询按钮
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string txtMobile = txtMobileNumber.Text.Trim();
        string strCond = string.Empty;

        if (!string.IsNullOrEmpty(txtMobile))
        {
            strCond = strCond + " and TEL ='" + txtMobile + "'";
        }

        string startTime = this.dtStartTime.Value;//开始时间，格式yyyy-mm-dd
        if (!string.IsNullOrEmpty(startTime))
        {
            strCond = strCond + " and CREATE_TIME >=to_date('" + startTime + "','yyyy-MM-dd')";
        }        

        string endTime = this.dtEndTime.Value;//结束时间,格式yyyy-mm-dd
        if (!string.IsNullOrEmpty(endTime))
        {            
            strCond = strCond + " and CREATE_TIME <=to_date('" + endTime + "','yyyy-MM-dd')";
        }  


        string chkValue = string.Empty;
        for (int i = 0; i < chkListPlatForm.Items.Count; i++)
        {
            if (chkListPlatForm.Items[i].Selected == true)
            {
                chkValue = chkValue + "'" + chkListPlatForm.Items[i].Value + "'" + ",";   //使用平台，因为post数据的时候，需要用单引号括起来。
            }
        }
       chkValue = chkValue.Trim(',');

       if (!string.IsNullOrEmpty(chkValue))
       {
           //strCond = strCond + "\"useCode\":\"" + chkValue + "\"" + ",";
           strCond = strCond + " and USE_CODE in (" + chkValue + ")";
       }  


       string Status = radioListPrcStatus.SelectedValue;//0--未处理，1--已处理
       if (!string.IsNullOrEmpty(Status))
       {        
           strCond = strCond + " and STATUS ='" + Status + "'";
       }

       strCond = strCond.Trim(',');
       ViewState["cond"] = strCond;

       BindGridView(strCond);//绑定数据
    }


    private void BindGridView(string strCond)
    {
        //string sql = "select * from t_lm_advice where 1=1 " + strCond + " order by CREATE_TIME desc";
        //DataSet ds = DbHelperOra.Query(sql);        
        //DataTable dt = ds.Tables[0];

        DataTable dt =  createDataTable(strCond).Tables[0]; 
        GridviewControl.GridViewDataBind(this.gridViewAdvice, dt);
    }

    protected void gridViewAdvice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewAdvice.PageIndex = e.NewPageIndex;
        string cond = string.Empty;
        if (ViewState["cond"] != null)
        {
            cond = ViewState["cond"].ToString();
        }
        BindGridView(cond);
    }


    private DataSet createDataTable(string strCond)
    {

        string sql = "select * from t_lm_advice where 1=1 " + strCond + " order by CREATE_TIME desc";
        DataSet ds = DbHelperOra.Query(sql, false);
       // DataTable dt = ds.Tables[0];
       // return dt;
        return ds;
    }
    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {        
        string cond = string.Empty;
        if (ViewState["cond"] != null)
        {
            cond = ViewState["cond"].ToString();
        }
        DataSet ds0 = createDataTable(cond);        
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
}
