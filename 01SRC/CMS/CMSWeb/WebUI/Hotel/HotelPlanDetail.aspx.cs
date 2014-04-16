using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class WebUI_Hotel_HotelPlanDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string taskid = Request.QueryString["taskid"].ToString();
            string planid = Request.QueryString["planid"].ToString();
            this.hidplanid.Value = planid;

            //绑定列表
            BindTaskList(planid);

            //=============动态增加按钮到前台 
            DateList.DataSource = getDataTable(planid);
            DateList.DataBind();
        
            Button btn0 = (Button)DateList.Items[0].FindControl("btnTaskSearch");
            btn0.Style.Add("background-color", "orange");
            btn0.Style.Add("color", "blue");
            if (getProcStatus(btn0.Text) == true)
            {
                this.divButton.Style.Add("display", "none");
            }

            //==============绑定详细内容
            BindExamDetailInfo(taskid);
        }
    }
    // get 
    public DataTable GetExamDetailInfo(string taskid)
    {
        DataSet ds = new DataSet();
        ds = DbHelperOra.Query("select t.ID,t.TASK_ID,t.OLD_CONTENT.GETSTRINGVAL() as OLD_CONTENT,t.NEW_CONTENT.GETSTRINGVAL() as NEW_CONTENT  from  T_LM_PROC_EXAM_DETAIL t where t.TASK_ID=" + taskid, false);
        return ds.Tables[0];
    }
    
    private void BindExamDetailInfo(string taskid)
    {
        DataTable dt = GetExamDetailInfo(taskid);

        StringBuilder sb1 = new StringBuilder();
        sb1.Append("<table style='width:100%'><tr><td class='tdcell' style='width:30%'>字段名</td><td class='tdcell' style='width:70%'>字段值</td></tr>");
        StringBuilder sb2 = new StringBuilder();
        sb2.Append("<table  style='width:100%'><tr><td class='tdcell' style='width:30%'>字段名</td><td class='tdcell' style='width:70%'>字段值</td></tr>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            CommonFunction comFun = new CommonFunction();
            DataSet ds_old = CommonFunction.getDataFromStringXml(dt.Rows[i]["OLD_CONTENT"].ToString());

            for (int j = 0; j < ds_old.Tables[0].Rows.Count; j++)
            {
                string temp = "<tr><td class='tdcell'>" + ds_old.Tables[0].Rows[j][0].ToString() + "</td><td class='tdcell'>" + ds_old.Tables[0].Rows[j][1].ToString() + "</td></tr>";
                sb1.Append(temp);

                if ("hotelid".Equals(ds_old.Tables[0].Rows[j][0].ToString().ToLower()))
                {
                    SetHotelNameVal(ds_old.Tables[0].Rows[j][1].ToString());
                }
            }

            DataSet ds_new = CommonFunction.getDataFromStringXml(dt.Rows[i]["NEW_CONTENT"].ToString());

            for (int k = 0; k < ds_new.Tables[0].Rows.Count; k++)
            {
                string temp2 = "<tr><td class='tdcell'>" + ds_new.Tables[0].Rows[k][0].ToString() + "</td><td class='tdcell'>" + ds_new.Tables[0].Rows[k][1].ToString() + "</td></tr>";
                sb2.Append(temp2);
            }
            sb1.Append("</table>");
            sb2.Append("</table>");

            //this.divOldContent.InnerHtml = sb1.ToString();
            //this.divNewContent.InnerHtml = sb2.ToString();

          
            //手动创建DataTable表
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("OldColName");
            dtNew.Columns.Add("OldColValue");
            dtNew.Columns.Add("NewColName");
            dtNew.Columns.Add("NewColValue");
            for (int m = 0; m < ds_old.Tables[0].Rows.Count; m++)
            {
                DataRow dr = dtNew.NewRow();
                dr["OldColName"] = ds_old.Tables[0].Rows[m][0].ToString();
                dr["OldColValue"] = ds_old.Tables[0].Rows[m][1].ToString();
                dr["NewColName"] = ds_new.Tables[0].Rows[m][0].ToString();
                dr["NewColValue"] = ds_new.Tables[0].Rows[m][1].ToString();
                dtNew.Rows.Add(dr);
            }
            gridViewData.DataSource = dtNew;
            gridViewData.DataBind();

        }
    
    }

    private void SetHotelNameVal(string hotelId)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hotelId;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.ChkLMPropHotelExam(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            lbHotelID.Text = dsResult.Tables[0].Rows[0]["PROP"].ToString();
            lbHotelNM.Text = dsResult.Tables[0].Rows[0]["PROP_NAME_ZH"].ToString();
        }
    }
    
    //绑定指定Taskid的列表
    private void BindTaskList(string planid)
    {        
        string sql = "select distinct t.TASK_ID,t.TASK_NAME,t.TASK_CODE,t.TASK_STATUS,t.TASK_APPROVEREJECT,t.TASK_CURPROCUSER,t.TASK_CREATETIME,t.TASK_UPDATETIME,t.TASK_CREATEBY from t_lm_proc_exam t ";
        sql += " where t.TASK_ID in (select TASK_ID from t_lm_proc_exam_detail where REFID ='" + planid + "') order by t.TASK_ID desc";

        DataSet ds = DbHelperOra.Query(sql, false);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sb.Append(ds.Tables[0].Rows[i]["TASK_ID"].ToString());
            sb.Append(",");
        }
      
        string taskArray = sb.ToString().Trim(',');
        ViewState["taskidArray"] = taskArray;      

        //==========================================================
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
            dt.Rows.Add(dr);
        }

        this.gridViewHotelPlanExam.DataSource = dt;
        
        //========================================================

       // this.gridViewHotelPlanExam.DataSource = ds.Tables[0];  
        this.gridViewHotelPlanExam.DataBind();

    }

    //得taskid
    private DataTable getDataTable(string planid)
    {
        DataTable dt = new DataTable();
        string strTaskid = ViewState["taskidArray"].ToString();
        string[] arrTaskID = strTaskid.Split(',');
        try
        {
            DataColumn ID_dc = new DataColumn("id");
            dt.Columns.Add(ID_dc);

            int intTotal = arrTaskID.Length;
            for (int i = 0; i <= intTotal; i++)
            {
                DataRow row1 = dt.NewRow();
                row1["id"] = arrTaskID[i];               
                dt.Rows.Add(row1);
            }
            return dt;
        }
        catch
        {
            return dt;
        }
    }

    /// <summary>
    /// 查询该任务是否已经处理0-未处理；1-已处理
    /// </summary>
    /// <param name="taskid"></param>
    /// <returns></returns>
    private bool getProcStatus(string taskid)
    {
        string sql = "select TASK_STATUS from t_lm_proc_exam where TASK_ID=" + taskid;
        object obj = DbHelperOra.GetSingle(sql, false);

        //TASK_STATUS =0表示未处理；1-表示已经处理
        if (obj == DBNull.Value || obj == null)
        {
            return false;
        }
        else if (obj.ToString() == "0")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    protected void DateList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string command = e.CommandName;
        if (command == "search")
        {
            string id = e.CommandArgument.ToString();//获取其参数的方式得到其ID,根据这个ID来写插入方法
            //ViewState["effectdate"] = id;

            BindExamDetailInfo(id);

            //这时不知道怎么写了, 
            //想法是在这里调用Button的事件,然后在buuton事件里写插入数据的方法. 
        }
    }

    //绑定数据时，控制下拉框的选择无效
    protected void gridViewData_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        //比较供应商状态和Hotelvp状态是否相同。不同则增加高亮颜色。              
        int iCount = this.gridViewData.Rows.Count;
        for (int i = 0; i < iCount; i++)
        {
            //5-供应商状态,6--hotelvp状态
            string  oldValue = this.gridViewData.Rows[i].Cells[1].Text.Trim();
            string newValue = this.gridViewData.Rows[i].Cells[3].Text.Trim();
            if (oldValue != newValue)
            {
                this.gridViewData.Rows[i].Cells[0].Style.Add("background-color", "red");
            }
        }


    }

    //保留原值
    protected void btnRetain_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> list = new List<string>();

            CommonFunction comFun = new CommonFunction();
            string strTaskid = ViewState["taskidArray"].ToString();
            string[] arrTaskID = strTaskid.Split(',');

            //保留原值
            int count = this.gridViewData.Rows.Count;
            string sql = string.Empty;
            for (int i = 0; i < count; i++)
            {
                string strColName = this.gridViewData.Rows[i].Cells[0].Text;
                string dataType = comFun.getDataType("T_LM_PLAN", strColName);

                string strColValue = this.gridViewData.Rows[i].Cells[1].Text;
                if (i == (count - 1))
                {
                    if (dataType == "NUMBER" || dataType == "LONG")
                    {
                        sql += strColName + "=" + strColValue + "";
                    }
                    else if (dataType == "DATE")
                    {
                        sql += strColName + "=  to_date('" + strColValue + "', 'yyyy-mm-dd hh24:mi:ss.ff')";                       
                    }
                    else if (dataType.IndexOf("TIMESTAMP")>0)
                    {
                        sql += strColName + "=  to_timestamp('" + strColValue + "', 'yyyy-mm-dd hh24:mi:ss.ff')";                      
                    }
                    else
                    {
                        sql += strColName + "='" + strColValue + "'";
                    }
                }
                else
                {
                    if (dataType == "NUMBER" || dataType == "LONG")
                    {
                        sql += strColName + "=" + strColValue + ",";
                    }
                    else if (dataType == "DATE")
                    {
                        sql += strColName + "= to_date('" + strColValue + "', 'yyyy-mm-dd hh24:mi:ss.ff')" + ",";
                    }
                    else if (dataType.IndexOf("TIMESTAMP") > 0)
                    {
                        sql += strColName + "=  to_timestamp('" + strColValue + "', 'yyyy-mm-dd hh24:mi:ss.ff')" + ",";
                    }
                    else
                    {
                        sql += strColName + "='" + strColValue + "',";
                    } 
                }

            }
            sql.Trim(',');

            sql = "update t_lm_plan set " + sql + " where ID = " + this.hidplanid.Value.Trim();
            list.Add(sql);

            string strTaskID = arrTaskID[0];//第一个taskid的值
            string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
            string sqlTask = "update t_lm_proc_exam set TASK_STATUS=1,TASK_APPROVEREJECT=0,TASK_CURPROCUSER='" + UserSession.Current.UserAccount + "',TASK_UPDATETIME=to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') where TASK_ID=" + strTaskID;
            list.Add(sqlTask);

            if (arrTaskID.Length > 1)
            {
                //修改别的taskid的值。
                for (int j = 1; j < arrTaskID.Length; j++)
                {
                    string strOtherTaskID = arrTaskID[j];
                    string sqlTask0 = "update t_lm_proc_exam set TASK_STATUS=1,TASK_APPROVEREJECT=2,TASK_CURPROCUSER='" + UserSession.Current.UserAccount + "',TASK_UPDATETIME=to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') where TASK_ID=" + strOtherTaskID;
                    list.Add(sqlTask0);
                }
            }

            DbHelperOra.ExecuteSqlTran(list);
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('保留原值成功！');", true);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key0", "<script>alert('保留原值成功！');if(window.opener!=null){window.opener.href=window.opener.href;}window.opener = null; window.close();</script>");
 
        }
        catch(Exception ex)
        {
            string msg = ex.Message;
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('保留原值失败！');", true);
           // ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('保留原值成功！');if(window.opener.window.opener!=null){window.opener.window.opener.location.href=window.opener.window.opener.location.href;}window.opener.close();self.close();</script>");
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "<script>alert('保留原值失败！');if(window.opener!=null){window.opener.href=window.opener.href;}window.opener = null; window.close();</script>");
 
        }

    }

    //使用新值
    protected void btnUseNew_Click(object sender, EventArgs e)
    {
        try
        {
            //使用新值
            List<string> list = new List<string>();

            string strTaskid = ViewState["taskidArray"].ToString();
            string[] arrTaskID = strTaskid.Split(',');
            string strTaskID = arrTaskID[0];//第一个taskid的值

            string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
            string sqlTask = "update t_lm_proc_exam set TASK_STATUS=1,TASK_APPROVEREJECT=1,TASK_CURPROCUSER='" + UserSession.Current.UserAccount + "',TASK_UPDATETIME=to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') where TASK_ID=" + strTaskID;
            list.Add(sqlTask);
            //
            if (arrTaskID.Length > 1)
            {
                //修改别的taskid的值
                for (int j = 1; j < arrTaskID.Length; j++)
                {
                    string strOtherTaskID = arrTaskID[j];
                    string sqlTask0 = "update t_lm_proc_exam set TASK_STATUS=1,TASK_APPROVEREJECT=2,TASK_CURPROCUSER='" + UserSession.Current.UserAccount + "',TASK_UPDATETIME=to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') where TASK_ID=" + strOtherTaskID;
                    list.Add(sqlTask0);
                }
            }
            DbHelperOra.ExecuteSqlTran(list);
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('使用新值成功！');", true);
            //ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('使用新值成功！');if(window.opener!=null){window.opener.location.href=window.opener.location.href;}window.opener = null; window.close();</script>");
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key1", "<script>alert('使用新值成功！');if(window.opener!=null){window.opener.href=window.opener.href;}window.opener = null; window.close();</script>");
        }
        catch
        {
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('使用新值失败！');", true);
            this.Page.ClientScript.RegisterStartupScript(Page.GetType(), "key2", "<script>alert('使用新值失败！');if(window.opener!=null){window.opener.href=window.opener.href;}window.opener = null; window.close();</script>");
 
        }
    }

    protected void btnTaskSearch_Click(object sender, EventArgs e)
    {
        int iCount = DateList.Items.Count;
        for (int i = 0; i < iCount; i++)
        {
            Button btn0 = (Button)DateList.Items[i].FindControl("btnTaskSearch");
            btn0.Style.Clear();
        }

        Button btn = (Button)sender;
        string getSelBtnTxt = btn.Text;
        string[] arrTaskid = ViewState["taskidArray"].ToString().Split(',');

        if (getSelBtnTxt != arrTaskid[0])
        {
            this.divButton.Style.Add("display", "none");
        }
        else
        {
            if (getProcStatus(arrTaskid[0]) == true)
            {
                this.divButton.Style.Add("display", "none");
            }
            else
            {
                this.divButton.Style.Add("display", "block");
            }
        }
       // string getItemValue = DateList.Items[0].DataItem.ToString();
        btn.Style.Add("background-color", "yellow");
        btn.Style.Add("color", "blue");
    }
}