using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.Configuration;

using HotelVp.Common.DBUtility;

public partial class WebUI_Ticket_BatchCreateTicket : BasePage
{
    public static string selectLabel = string.Empty;
    public static string clearLabel = string.Empty;
    public static string PromptTicketCodeNoEmpty = string.Empty;
    public static string PromptPackageCannotUse = string.Empty;
    public static string PromptTicketCountIsOne = string.Empty;
    public static string CodeIsNotExist = string.Empty;
    public static string TicketHavingFinish = string.Empty;
    public static string CodeUseCompleted = string.Empty;
    public static string PromptCreateTicketSuccess = string.Empty;
    public static string PromptCreateTicketFaild = string.Empty;
    public static string PromptNoUseRule = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        selectLabel = Resources.MyGlobal.SelectText;
        clearLabel = Resources.MyGlobal.ClearText;
        PromptTicketCodeNoEmpty = GetLocalResourceObject("PromptTicketCodeNoEmpty").ToString();
        PromptPackageCannotUse = GetLocalResourceObject("PromptPackageCannotUse").ToString();
        PromptTicketCountIsOne = GetLocalResourceObject("PromptTicketCountIsOne").ToString();
        CodeIsNotExist = GetLocalResourceObject("CodeIsNotExist").ToString();
        TicketHavingFinish = GetLocalResourceObject("TicketHavingFinish").ToString();
        CodeUseCompleted = GetLocalResourceObject("CodeUseCompleted").ToString();
        PromptCreateTicketSuccess = GetLocalResourceObject("PromptCreateTicketSuccess").ToString();
        PromptCreateTicketFaild = GetLocalResourceObject("PromptCreateTicketFaild").ToString();
        PromptNoUseRule = GetLocalResourceObject("PromptNoUseRule").ToString();

        if (!IsPostBack)
        {
            //绑定列表
            BindGridView();

        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strPackageCode = this.txtPackageCode.Value;
        bool bCanUse = CanUse(strPackageCode);
        if (bCanUse == false)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptPackageCannotUse + "');", true);
            return;//后面就不执行了
        }

        string sqlTicket = HotelVp.Common.DBUtility.XmlSqlAnalyze.GotSqlTextFromXml("Ticket", "t_lm_ticket_user_export");
        OracleParameter[] parmPack = { new OracleParameter("PACKAGECODE", OracleType.VarChar) };
        parmPack[0].Value = strPackageCode;
        DataSet dsResult = DbHelperOra.Query(sqlTicket, false, parmPack);

        CommonFunction.ExportExcelForLM(dsResult);
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        this.btnCreate.Enabled = false;//不能再点 
        

        string strPackageCode = this.txtPackageCode.Value;
        bool bCanUse = CanUse(strPackageCode);
        if (bCanUse == false)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptPackageCannotUse + "');invokeCloseBgDiv();", true);         
            this.btnCreate.Enabled = true;//恢复按钮状态  
            this.divCreateButton.Visible = true;//恢复按钮所在层显示 
            return;//后面就不执行了
        }

        if (getTicketCount(strPackageCode) == false)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptTicketCountIsOne + "');invokeCloseBgDiv();", true);
            this.btnCreate.Enabled = true;//恢复按钮状态   
            this.divCreateButton.Visible = true;//恢复按钮所在层显示
            return;
        }

        bool bUseRule = IsUseRule(strPackageCode);
        if (bUseRule == false)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptNoUseRule + "');invokeCloseBgDiv();", true);
            this.btnCreate.Enabled = true;//恢复按钮状态 
            this.divCreateButton.Visible = true;//恢复按钮所在层显示
            return;//后面就不执行了
        
        }

        //PromptNoUseRule
        try
        {
            string sql = "select USERCNT from t_lm_ticket_package where packagecode =:packagecode";
            OracleParameter[] parmUserCNT = { new OracleParameter("packagecode", OracleType.VarChar) };
            parmUserCNT[0].Value = strPackageCode;
            object objUserCNT = DbHelperOra.GetSingle(sql, false, parmUserCNT);

            if (objUserCNT == null || objUserCNT == DBNull.Value)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + CodeIsNotExist + "');invokeCloseBgDiv();", true);
                this.btnCreate.Enabled = true;//恢复按钮状态     
                this.divCreateButton.Visible = true;//恢复按钮所在层显示
                return;
            }
            else
            {
                int intUserCNT = Convert.ToInt32(objUserCNT);

                //根据packagecode查询，一共已经生产了多少张券，如果有，则需要用这个总量减掉已经生产的总数
                string sqlSelCount = "select  count(*) from T_LM_TICKET_USER where PACKAGECODE=:PACKAGECODE";
                OracleParameter[] parmSelCount = { new OracleParameter("PACKAGECODE", OracleType.VarChar) };
                parmSelCount[0].Value = strPackageCode;
                object objTicketUserCount = DbHelperOra.GetSingle(sqlSelCount, false, parmSelCount);

                int flag = 0;
                if (objTicketUserCount != null && objTicketUserCount != DBNull.Value)
                {
                    int intTicketUserCount = Convert.ToInt32(objTicketUserCount);
                    if (intTicketUserCount >= intUserCNT)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + TicketHavingFinish + "');invokeCloseBgDiv();", true);
                        this.btnCreate.Enabled = true;//恢复按钮状态   
                        this.divCreateButton.Visible = true;//恢复按钮所在层显示
                        return;
                    }
                    else
                    {
                        flag = 1;
                        intUserCNT = intUserCNT - intTicketUserCount;
                    }
                }
                else
                {
                    flag = 1;
                }

                if (flag == 1)//表示可以加入数据到数据库中
                {
                    List<CommandInfo> sqlList = new List<CommandInfo>();
                    string sqlTicket = "select * from T_LM_TICKET where PACKAGECODE=:PACKAGECODE";
                    OracleParameter[] parmPack = { new OracleParameter("PACKAGECODE", OracleType.VarChar) };
                    parmPack[0].Value = strPackageCode;
                    DataTable dt = DbHelperOra.Query(sqlTicket, false, parmPack).Tables[0];
                    int MaxLength = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxTicketLength"].ToString())) ? 1000 : int.Parse(ConfigurationManager.AppSettings["MaxTicketLength"].ToString());
                    int iCount = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string ticketCode = dt.Rows[i]["TICKETCODE"].ToString();
                        string ticketAmt = dt.Rows[i]["TICKETAMT"].ToString();

                        //比较intUserCNT和5000大小，用来判读是循环多少次。
                        //目的用于控制，
                        //if (intUserCNT > 5000)
                        //{
                        //    intUserCNT = 5000;
                        //}


                        for (int j = 0; j < intUserCNT; j++)
                        {
                            StringBuilder sqlInsert = new StringBuilder();
                            //Oracle sql 语法
                            sqlInsert.AppendLine("INSERT INTO T_LM_TICKET_USER(ID,TICKETCODE,TICKETUSERCODE,STATUS,TICKETAMT,PACKAGECODE,FLAG,CREATETIME,UPDATETIME) VALUES ( ");
                            sqlInsert.AppendLine("T_LM_TICKET_USER_SEQ.nextval,:TICKETCODE,:TICKETUSERCODE,:STATUS,:TICKETAMT,:PACKAGECODE,:FLAG,sysdate,sysdate) ");
                            OracleParameter[] parm ={
                                            new OracleParameter("TICKETCODE",OracleType.VarChar),                        
                                            new OracleParameter("TICKETUSERCODE",OracleType.VarChar),
                                            new OracleParameter("STATUS",OracleType.VarChar),
                                            new OracleParameter("TICKETAMT",OracleType.Double),
                                            new OracleParameter("PACKAGECODE",OracleType.VarChar),
                                            new OracleParameter("FLAG",OracleType.Int32)                                    
                                        };


                            //CommonFunction comFun = new CommonFunction();
                            //parm[0].Value = comFun.getMaxIDfromSeq("T_LM_TICKET_USER_SEQ");//ID值  
                            parm[0].Value = ticketCode;

                            //string strTicketUserCode = comFun.GetRandNumString(13);

                            string strTicketUserCode = getTicketCode_New(13);// getTicketCode(13);
                            if (strTicketUserCode == "")
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + CodeUseCompleted + "');invokeCloseBgDiv();", true);
                                this.btnCreate.Enabled = true;//恢复按钮状态 
                                this.divCreateButton.Visible = true;//恢复按钮所在层显示
                                return;
                            }


                            parm[1].Value = strTicketUserCode;
                            parm[2].Value = "0";
                            parm[3].Value = Convert.ToDouble(ticketAmt);
                            parm[4].Value = strPackageCode;
                            parm[5].Value = 1;


                            iCount = iCount + 1;
                            CommandInfo cminfo = new CommandInfo();
                            cminfo.CommandText = sqlInsert.ToString();
                            cminfo.Parameters = parm;
                            sqlList.Add(cminfo);

                            try
                            {
                                if (iCount == MaxLength)
                                {
                                    DbHelperOra.ExecuteSqlTran(sqlList);
                                    sqlList = new List<CommandInfo>();
                                    iCount = 0;
                                }
                            }
                            catch
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptCreateTicketFaild + "');invokeCloseBgDiv();", true);
                                this.btnCreate.Enabled = true;//恢复按钮状态 
                                this.divCreateButton.Visible = true;//恢复按钮所在层显示
                                return;
                            }
                            //try
                            //{
                            //    DbHelperOra.ExecuteSql(sqlInsert.ToString(), parm);
                            //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptCreateTicketSuccess + "');invokeCloseBgDiv();", true);
                            //    this.btnCreate.Enabled = true;//恢复按钮状态 
                            //    this.divCreateButton.Visible = true;//恢复按钮所在层显示
                            //}
                            //catch
                            //{
                            //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptCreateTicketFaild + "');invokeCloseBgDiv();", true);
                            //    this.btnCreate.Enabled = true;//恢复按钮状态
                            //    this.divCreateButton.Visible = true;//恢复按钮所在层显示

                            //}
                        }
                        try
                        {
                            if (iCount > 0)
                            {
                                DbHelperOra.ExecuteSqlTran(sqlList);
                                sqlList = new List<CommandInfo>();
                                iCount = 0;
                            }
                        }
                        catch
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptCreateTicketFaild + "');invokeCloseBgDiv();", true);
                            this.btnCreate.Enabled = true;//恢复按钮状态 
                            this.divCreateButton.Visible = true;//恢复按钮所在层显示
                            return;
                        }
                    }

                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptCreateTicketSuccess + "');invokeCloseBgDiv();", true);
                    this.btnCreate.Enabled = true;//恢复按钮状态
                    this.divCreateButton.Visible = true;//恢复按钮所在层显示
                }
            }
        }//===============
        catch
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptCreateTicketFaild + "');invokeCloseBgDiv();", true);
            this.btnCreate.Enabled = true;//恢复按钮状态     
            this.divCreateButton.Visible = true;//恢复按钮所在层显示
        }


        //执行后需要修改页面中的剩余可以生产券的张数
        btnSearchRest_Click(null, null);

    }

    //判断该券是否在有效的时间和规则内，判断该优惠券包是否可用
    private bool CanUse(string PackageCode)
    {
        string nowDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
        string sql = "select * from t_lm_ticket_package where  packagecode =:packagecode and startdate<=:startdate and enddate>=:enddate";
        OracleParameter[] parmPack = {
                                         new OracleParameter("packagecode", OracleType.VarChar),
                                         new OracleParameter("startdate", OracleType.VarChar),
                                         new OracleParameter("enddate", OracleType.VarChar)
                                     };

        parmPack[0].Value = PackageCode;
        parmPack[1].Value = nowDate;
        parmPack[2].Value = nowDate;
        DataTable dt = DbHelperOra.Query(sql, false, parmPack).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //判断该Package包中的ticket是否关联了规则
    private bool IsUseRule(string PackageCode)
    {
        string sql = "select TICKETRULECODE from t_lm_ticket where  packagecode =:packagecode";
        OracleParameter[] parmRuleCode = {
                                         new OracleParameter("packagecode", OracleType.VarChar)                                  
                                     };
        parmRuleCode[0].Value = PackageCode;
        object objTicketRuleCode = DbHelperOra.GetSingle(sql, false, parmRuleCode);
        if (objTicketRuleCode == null || objTicketRuleCode == DBNull.Value)
        {
            return false;
        }
        else
        {
            return true;
        }
    
    }

    //判断该Package下有几张ticket券
    //只能内含一张ticket，且只能内含一张券
    private bool getTicketCount(string PackageCode)
    {
        string sql = "select Count(ticketcnt) from t_lm_ticket where  packagecode =:packagecode";
        OracleParameter[] parmPack = {
                                         new OracleParameter("packagecode", OracleType.VarChar)                                       
                                     };

        parmPack[0].Value = PackageCode;
        object objTicketCount = DbHelperOra.GetSingle(sql, false, parmPack);
        if (objTicketCount == null || objTicketCount == DBNull.Value)
        {
            return false;
        }
        else
        {
            int intTicketCount = Convert.ToInt32(objTicketCount);
            if (intTicketCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    private string getTicketCode_New(int length)
    {
        string str = "1234567890";
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            builder.Append(str[new Random(Guid.NewGuid().GetHashCode()).Next(0, 9)]);
        }
        return builder.ToString();
    }

    //用于产生新的一个UserCode
    private string getTicketCode(int length)
    {
        CommonFunction comFun = new CommonFunction();
        string newCode = comFun.GetRandNumString(length);
        bool bExist = ExistCode(newCode);
        if (bExist == false)//表示不存在该Code，则可以加入
        {
            return newCode;
        }
        else
        {
            //循环100次，如果还不存在，则提示不能再生成了。
            int flag = 0; //用于标记，循环100次后，是否有形成可用的ticketcode
            for (int i = 0; i < 100; i++)
            {
                newCode = comFun.GetRandNumString(length);
                if (ExistCode(newCode) == false)
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                return newCode;
            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <returns></returns>
    private bool ExistCode(string userTicketCode)
    {
        string sqlExist = "select  ticketusercode from T_LM_TICKET_USER where ticketusercode=:ticketusercode";
        OracleParameter[] parmExist = { new OracleParameter("ticketusercode", OracleType.VarChar) };
        parmExist[0].Value = userTicketCode;
        object objCode = DbHelperOra.GetSingle(sqlExist, false, parmExist);
        if (objCode == null || objCode == DBNull.Value)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //剩余可生产张数。
    /// <summary>
    /// 根据packagecode和总共可以生产的张数，计算剩余的张数
    /// </summary>
    /// <param name="strPackageCode"></param>
    /// <param name="UserCNT"></param>
    /// <returns></returns>
    private int RestCount(string strPackageCode, int UserCNT)
    {
        int restCount = 0;
        string sqlSelCount = "select  count(*) from T_LM_TICKET_USER where PACKAGECODE=:PACKAGECODE";
        OracleParameter[] parmSelCount = { new OracleParameter("PACKAGECODE", OracleType.VarChar) };
        parmSelCount[0].Value = strPackageCode;
        object objTicketUserCount = DbHelperOra.GetSingle(sqlSelCount, false, parmSelCount);
        if (objTicketUserCount == null || objTicketUserCount == DBNull.Value)
        {
            restCount = 0;
        }
        else
        {
            restCount = UserCNT - Convert.ToInt32(objTicketUserCount);
        }
        return restCount;
    
}

    //查询剩余可以生产的抵用券张数
    protected void btnSearchRest_Click(object sender, EventArgs e)
    {
        string packageCode = this.txtPackageCode.Value;
        string strCount = string.Empty;
        if (this.txtTicketCount.Value.Trim() == "")
        {
            strCount = "0";
        }
        else
        {
            strCount = this.txtTicketCount.Value.Trim();
        }
        int userCount = Convert.ToInt32(strCount);
        int restCount = RestCount(packageCode, userCount);
        this.lblRestCount.Text = restCount.ToString();
    }


    #region for searchPachage
    void BindGridView()
    {
        //string strSql = "select distinct id,packagecode,usercnt,startdate,enddate,packagename from t_lm_ticket_package where 1=1 and SINGLE_USERCNT <= 1 ";
        string strSql = "select distinct tpe.id,tpe.packagecode,tpe.usercnt,tpe.startdate,tpe.enddate,tpe.packagename from t_lm_ticket_package tpe inner join (select count(ticketcode) as tCount, packagecode from t_lm_ticket group by packagecode) tcl on tcl.packagecode = tpe.packagecode and tcl.tCount <= 1 where 1=1 and (tpe.SINGLE_USERCNT is null or tpe.SINGLE_USERCNT <= 1)";
        if (this.txtPackageNameSearch.Text != "")
        {
            strSql += " and tpe.packagename like '%" + CommonFunction.StringFilter(this.txtPackageNameSearch.Text) + "%'";
        }

        if (this.txtPackageCodeSearch.Text != "")
        {
            strSql += " and tpe.packagecode=  '" + CommonFunction.StringFilter(this.txtPackageCodeSearch.Text) + "'";
        }

        strSql += " order by tpe.ID desc";

        DataSet ds = DbHelperOra.Query(strSql, false);
        DataView view = ds.Tables[0].DefaultView;
        this.myGridView.DataSource = view;
        this.myGridView.DataBind();

    }

    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        myGridView.PageIndex = e.NewPageIndex;
        BindGridView();

        Page.RegisterStartupScript("ggg", "<script>invokeOpen2(); </script>"); 

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
        Page.RegisterStartupScript("ggg", "<script>invokeOpen2(); </script>"); 
    }

    //点击保存
    //protected void btnOK_Click(object sender, EventArgs e)
    //{
    //    BindGridView();
    //    Page.RegisterStartupScript("ggg", "<script>invokeOpen2(); </script>"); 
    //}

    protected void myGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selIndex = myGridView.SelectedIndex;
        string strPackageCode = myGridView.Rows[selIndex].Cells[1].Text.ToString();
        string strPackageName = myGridView.Rows[selIndex].Cells[2].Text.ToString();

        string strTicketCount = myGridView.Rows[selIndex].Cells[3].Text.ToString();//总共可以生产的张数
        
        int userCount = Convert.ToInt32(strTicketCount);
        int restCount = RestCount(strPackageCode, userCount);

        this.lblRestCount.Text = restCount.ToString();
        this.txtPackageCode.Value = strPackageCode;
        this.txtTicketCount.Value = strTicketCount;

        Page.RegisterStartupScript("ggg", "<script>invokeClose2(); </script>"); 
    }
    #endregion
   
}