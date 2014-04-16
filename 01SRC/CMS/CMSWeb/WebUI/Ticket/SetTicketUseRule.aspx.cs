using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using HotelVp.Common.DBUtility;
using System.Data;
using System.Collections;

public partial class Ticket_SetTicketUseRule : BasePage
{
    public static string PromptSelectTicket = string.Empty;
    public static string PromptSelectRule = string.Empty;
    public static string PromptSettingSuccess = string.Empty;
    public static string PromptSettingFaild = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        PromptSelectTicket = GetLocalResourceObject("PromptSelectTicket").ToString();
        PromptSelectRule = GetLocalResourceObject("PromptSelectRule").ToString();
        PromptSettingSuccess = GetLocalResourceObject("PromptSettingSuccess").ToString();
        PromptSettingFaild = GetLocalResourceObject("PromptSettingFaild").ToString();

        //绑定优惠券列表，也就是Package包。
        if (!IsPostBack)
        {
            BindPackageGridView();
            BindTicketGridView("10000");

            //绑定规则列表
            BindRuleGridView();
        }
    }

    private void BindPackageGridView()
    {
        string sql = "select * from t_lm_ticket_package order by ID desc";
        DataSet ds = DbHelperOra.Query(sql, false);
        this.gridViewPackage.DataSource = ds.Tables[0].DefaultView;
        this.gridViewPackage.DataBind();
    }

    private void BindTicketGridView(string packagecode)
    {
        string sql = "select * from t_lm_ticket where packagecode='" + packagecode + "' order by ID desc";
        DataSet ds = DbHelperOra.Query(sql, false);
        this.gridViewTicket.DataSource = ds.Tables[0].DefaultView;
        this.gridViewTicket.DataBind();
    }

    private void BindRuleGridView()
    {
        string sql = "select * from T_LM_TICKET_RULE order by ID desc";
        DataSet ds = DbHelperOra.Query(sql, false);
        this.gridViewRule.DataSource = ds.Tables[0].DefaultView;
        this.gridViewRule.DataBind();
    }

    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //先判断内含抵用券有没有选中
        string strTicketCode = "";
        for (int i = 0; i < this.gridViewTicket.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)this.gridViewTicket.Rows[i].FindControl("checkticketitem");
            if (ck.Checked == true)
            {
                strTicketCode = strTicketCode + this.gridViewTicket.Rows[i].Cells[2].Text.Trim() + ",";
            }
        }
        strTicketCode = strTicketCode.Trim(',').Trim();

        if (string.IsNullOrEmpty(strTicketCode))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptSelectTicket + "');", true);
            return;
        }

        //先判断规则有没有选中，增加只能选择一个规则，add by agan at 2012-03-20
        string strRuleCode = "";
        for (int j = 0; j < this.gridViewRule.Rows.Count; j++)
        {
            CheckBox ck = (CheckBox)this.gridViewRule.Rows[j].FindControl("checkruleitem");
            if (ck.Checked == true)
            {
                strRuleCode = strRuleCode + this.gridViewRule.Rows[j].Cells[2].Text.Trim() + ",";
            }
        }
        strRuleCode = strRuleCode.Trim(',').Trim();

        if (string.IsNullOrEmpty(strRuleCode))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptSelectRule + "');", true);
            return;
        }
        

        //修改数据内容
        List<string> li = new List<string>();
        string[] arrTicketCode = strTicketCode.Split(',');
        //=======判断RuleCode 和TicketCode中的规则是否有冲突=======
        int checkFlag = 1;//默认是没有冲突
        for (int k = 0; k < arrTicketCode.Length; k++)
        {
            Hashtable ht = hsDateAndPackage(strRuleCode, arrTicketCode[k]);
            bool bRuleResult = Convert.ToBoolean(ht["rule"]);
            bool bPackageResult = Convert.ToBoolean(ht["package"]);
            if (bRuleResult == false)
            {
                //您绑定的优惠券存在使用日期/领用日期冲突，确定要保存么”，确定保存 or 返回检查
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您绑定的优惠券存在使用日期和领用日期冲突，请检查后重新绑定！');</script>");
                checkFlag = 0;
                break;                
            }
            if (bPackageResult == false)
            {
                //您绑定的优惠券存在使用/领用平台冲突，确定要保存么”，确定保存 or 返回检查  
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您绑定的优惠券存在使用平台和领用平台冲突，请检查后重新绑定！');</script>");
                checkFlag = 0;
                break;               
            }
        }



        //执行更新操作
        if (checkFlag == 1)//没有冲突
        {
            for (int i = 0; i < arrTicketCode.Length; i++)
            {
                string tempTicketCode = arrTicketCode[i];
                string sql = "UPDATE T_LM_TICKET SET TICKETRULECODE ='" + strRuleCode + "' WHERE TICKETCODE='" + tempTicketCode + "'";
                li.Add(sql);
            }
            if (li.Count > 0)
            {
                try
                {
                    DbHelperOra.ExecuteSqlTran(li);
                    //当前选中的Packagecode
                    string packCode = this.hidSelectPacgageCode.Value;
                    BindTicketGridView(packCode);

                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptSettingSuccess + "');", true);
                }
                catch
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptSettingFaild + "');", true);
                }
            }
        }
    }

    protected void gridViewPackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {      
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=t");
            int selIndex = e.Row.RowIndex;

            //单击/双击 事件        
            e.Row.Attributes.Add("OnClick", "ClickEvent('" + e.Row.Cells[1].Text + "','" + selIndex + "')");          
        }

        //int i;
        ////执行循环，保证每条数据都可以更新
        //for (i = 0; i < gridViewPackage.Rows.Count; i++)
        //{
        //    //首先判断是否是数据行
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //当鼠标停留时更改背景色
        //        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#00A9FF'");
        //        //当鼠标移开时还原背景色
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");              
        //    }
        //}
    }
   
   

    //选择
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        //根据PackageCode查询该包下的ticket券
        string packCode = this.hidSelectPacgageCode.Value;
        BindTicketGridView(packCode);

        //绑定规则表
        BindRuleGridView(); 

        //设置选中的行的背景颜色
        int selRowIndex = Convert.ToInt32(this.hidSelPackRowIndex.Value);
        this.gridViewPackage.SelectedIndex = selRowIndex;
        //this.gridViewPackage.SelectedRow.Style.Add("backgroundColor", "#ebebce");
        this.gridViewPackage.SelectedRowStyle.BackColor = System.Drawing.Color.Yellow;
    }

    protected void btnSelTicket_Click(object sender, EventArgs e)
    {
        //根据PackageCode查询该包下的ticket券
        string ruleCode = this.hidSelectRuleCode.Value;
        
        //绑定规则表
        BindRuleGridView();        
    }

    //ticket gridview
    protected void gridViewTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=t");
            e.Row.Attributes.CssStyle.Add("cursor", "hand");
            //单击/双击事件
            //e.Row.Attributes.Add("OnClick", "ClickEvent('" + e.Row.Cells[10].FindControl("btnDetial").ClientID + "')");
            e.Row.Attributes.Add("OnClick", "ClickTicketEvent('" + e.Row.Cells[5].Text + "')");
        }
    }

    protected void gridViewPackage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewPackage.PageIndex = e.NewPageIndex;
        BindPackageGridView();
    }

    protected void gridViewRule_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewRule.PageIndex = e.NewPageIndex;
        BindRuleGridView();
    }
  
    //点击复选框的时候执行该事件方法
    protected void checkruleitem_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        string chkuid = chk.UniqueID;
        for (int i = 0; i < gridViewRule.Rows.Count; i++)
        {
            CheckBox ck = (CheckBox)this.gridViewRule.Rows[i].FindControl("checkruleitem");
            if (chkuid != ck.UniqueID)
            {
                ck.Checked = false;
            }

        }
    }

    //判断领用规则和使用规则是否有冲突
    private Hashtable hsDateAndPackage(string ruleCode,string ticketCode)
    {
        Hashtable ht = new Hashtable();
        string ruleStartDate = string.Empty;
        string ruleEndtDate = string.Empty;
        string ruleUseCode = string.Empty;

        string sqlRule = "select STARTDATE,ENDDATE,USECODE from T_LM_TICKET_RULE where TICKETRULECODE='" + ruleCode + "'";
        DataTable dtRule = DbHelperOra.Query(sqlRule, false).Tables[0];
        for (int i = 0; i < dtRule.Rows.Count; i++)
        {
            ruleStartDate = dtRule.Rows[i]["STARTDATE"].ToString();
            ruleEndtDate = dtRule.Rows[i]["ENDDATE"].ToString();
            ruleUseCode = dtRule.Rows[i]["USECODE"].ToString();
        }

        string packStartDate = string.Empty;
        string packEndtDate = string.Empty;
        string packUseCode = string.Empty;

        

        string sqlPackage = "select STARTDATE,ENDDATE,USECODE from v_lm_ticket_package where ticketcode ='" + ticketCode + "'";
        DataTable dtPackage = DbHelperOra.Query(sqlPackage, false).Tables[0];
        for (int j = 0; j < dtPackage.Rows.Count; j++)
        {
            packStartDate = dtPackage.Rows[j]["STARTDATE"].ToString();
            packEndtDate = dtPackage.Rows[j]["ENDDATE"].ToString();
            packUseCode = dtPackage.Rows[j]["USECODE"].ToString();
        }

        //“您绑定的优惠券存在使用日期/领用日期冲突，确定要保存么”，确定保存 or 返回检查
        if (Convert.ToDateTime(ruleEndtDate) < Convert.ToDateTime(packStartDate))//最晚使用日期<最早可领用日期
        {
            ht.Add("rule", false);
        }
        else
        {
            ht.Add("rule", true);
        }

        //判断使用平台是否互相排斥
        string[] arrRule = ruleUseCode.Split(',');
        string[] arrPackage = packUseCode.Split(',');

        int flag = 0;
        for (int k = 0; k < arrRule.Length; k++)
        {
            string tempRule = arrRule[k].Trim();
            for (int q = 0; q < arrPackage.Length; q++)
            {
                string tempPackage = arrPackage[q].Trim();
                if (tempRule == tempPackage)
                {
                    flag = 1;
                    break;
                }
            }
        }

        //规则或礼包中有选择不限制
        if ((arrRule.Length == 1 && string.IsNullOrEmpty(arrRule[0])) || (arrPackage.Length==1 && string.IsNullOrEmpty(arrPackage[0])))
        {
            flag = 1;    
        }

        //如果有一个为空，则赋值为可以使用
        if (flag == 0)
        {
            ht.Add("package", false);
        }
        else
        {
            ht.Add("package", true);
        }
        return ht;    
    }

    //public void Confirm(string strMessage, string strOkPage, string strCancelPage)
    //{
    //    if (strCancelPage == null || strCancelPage.Length == 0)
    //        System.Web.HttpContext.Current.Response.Write(" <script   language=javascript>   if   (confirm( ' " + strMessage + " ')==true){location.href= ' " + strOkPage + " ';} </script> ");
    //    else
    //        System.Web.HttpContext.Current.Response.Write(" <script   language=javascript>   if   (confirm( ' " + strMessage + " ')==true){location.href= ' " + strOkPage + " ';}else{location.href= ' " + strCancelPage + " ';} </script> ");
    //} 

       
}