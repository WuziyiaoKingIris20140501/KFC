using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelVp.Common.Json;
//using HotelVp.Common.Json.Converters;
using HotelVp.Common.Json.Linq;
using HotelVp.Common.DBUtility;
using System.Data;

public partial class WebUI_Feedback_UpdateAdviceStatus : BasePage
{
    public static string strClose = Resources.MyGlobal.CloseText;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["TYPE"]))
        {
            string getID = Server.UrlDecode(Request.QueryString["id"]);
            hidAdviceID.Value = getID;
            ViewState["ID"] = getID;

            DataSet dsResult = createDataTable(getID);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                string getTel = dsResult.Tables[0].Rows[0]["tel"].ToString().Trim();
                string getgrade = dsResult.Tables[0].Rows[0]["grade"].ToString().Trim();
                string getusecode = dsResult.Tables[0].Rows[0]["usecode"].ToString().Trim();
                string getcontent = dsResult.Tables[0].Rows[0]["content"].ToString().Trim();
                string getcreateTime = dsResult.Tables[0].Rows[0]["createTime"].ToString().Trim();
                string getstatus = dsResult.Tables[0].Rows[0]["status"].ToString().Trim();
                string getuserver = dsResult.Tables[0].Rows[0]["userver"].ToString().Trim();

                this.txtMobileNumber.Text = getTel;
                this.txtPublishDate.Text = getcreateTime;
                this.txtPlatForm.Text = getusecode;
                this.txtUserVer.Text = getuserver;

                if (getstatus == "0")
                {
                    getstatus = "未处理";
                }
                else
                {
                    getstatus = "已处理";
                }

                this.txtPrcStatus.Text = getstatus;
                this.txtAdviceContent.Text = getcontent.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("“", "&quot;").Replace("“", "&quot;").Replace("‘", "'").Replace(" ", "&nbsp;");
                this.txtScore.Text = getgrade;
            }
        }
        else
        {
            string getID = Server.UrlDecode(Request.QueryString["id"]);
            hidAdviceID.Value = getID;
            ViewState["ID"] = getID;
            string getTel = Server.UrlDecode(Request.QueryString["tel"]);
            string getgrade = Server.UrlDecode(Request.QueryString["grade"]);
            string getusecode = Server.UrlDecode(Request.QueryString["usecode"]);
            string getcontent = Server.UrlDecode(Request.QueryString["content"]);
            string getcreateTime = Server.UrlDecode(Request.QueryString["createTime"]);
            string getstatus = Server.UrlDecode(Request.QueryString["status"]);
            string getuserver = Server.UrlDecode(Request.QueryString["userver"]);

            this.txtMobileNumber.Text = getTel;
            this.txtPublishDate.Text = getcreateTime;
            this.txtPlatForm.Text = getusecode;
            this.txtUserVer.Text = getuserver;

            if (getstatus == "0")
            {
                getstatus = "未处理";
            }
            else
            {
                getstatus = "已处理";
            }

            this.txtPrcStatus.Text = getstatus;
            this.txtAdviceContent.Text = getcontent.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("“", "&quot;").Replace("“", "&quot;").Replace("‘", "'").Replace(" ", "&nbsp;");
            this.txtScore.Text = getgrade;
        }
    }
    //修改为已经处理
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string strID = ViewState["ID"].ToString();

        string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);

        string sql = "update t_lm_advice  set status='1',OPERATE_USER='" + UserSession.Current.UserAccount + "',Update_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') where ID= " + strID;

       int result = DbHelperOra.ExecuteSql(sql);
       if (result == 1)
       {
           this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + GetLocalResourceObject("ProcessSuccessText").ToString() + "');window.opener = null; window.open('', '_self');window.close();", true);
       }
       else
       {
           this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('修改出错！');", true);
       }
    }

    private DataSet createDataTable(string strCond)
    {
        string sql = "    select id, tel, email, title, use_code AS usecode, content, status, grade, to_char(create_time, 'yyyy/mm/dd hh24:mi:ss') AS createTime, client_code, update_time, operate_user, handle_content, use_code_version AS userver from t_lm_advice where id=" + strCond + " order by CREATE_TIME desc";
        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;
    }
}