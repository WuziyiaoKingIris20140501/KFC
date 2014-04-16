using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using HotelVp.Common.DBUtility;

/// <summary>
///ExamManagement 的摘要说明
/// </summary>
public class ExamManagement
{
	public ExamManagement()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    private string _id = "";
    public string id
    {
        get { return _id; }
        set { _id = value; }
    }

    private string _task_id = "";
    public string take_id
    {
        get { return _task_id; }
        set { _task_id = value; }
    }

    private string _old_content="";
    public string old_content
    {
        get {return _old_content;}
        set {_old_content = value;}    
    }

    private string _new_content = "";
    public string new_content 
    {
        get { return _new_content; }
        set { _new_content = value; }
    }

    // get 
    public void GetExamDetailInfo(string taskid)
    {
        DataSet ds = new DataSet();
        ds = DbHelperOra.Query("select t.ID,t.TASK_ID,t.OLD_CONTENT.GETSTRINGVAL() as OLD_CONTENT,t.NEW_CONTENT.GETSTRINGVAL() as NEW_CONTENT  from  T_LM_PROC_EXAM_DETAIL t where t.TASK_ID=" + taskid, false);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {               
                _id = dt.Rows[i]["ID"].ToString();
                _task_id = dt.Rows[i]["TASK_ID"].ToString();
                _old_content = dt.Rows[i]["OLD_CONTENT"].ToString();
                _new_content = dt.Rows[i]["NEW_CONTENT"].ToString();               
            }
        }
    }

	
}