using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OracleClient;
using HotelVp.Common.DBUtility;
using System.Data;
using System.Collections;

public partial class Ticket_BatchTicketPackage : BasePage
{
    public static string addLabel = Resources.MyGlobal.AddText;
    public static string selectLabel = Resources.MyGlobal.SelectText;
    public static string clearLabel = Resources.MyGlobal.ClearText;

    public static string promptDelete = string.Empty;
    public static string promptTicketAmountIsEmpty = string.Empty;
    public static string promptTicketAmountIsNaN = string.Empty;
    public static string promptAllAmountNoEmpty = string.Empty;
    public static string promptTicketCountError = string.Empty;
    public static string promptTicketAmountError = string.Empty;
    public static string promptAmountError = string.Empty;
    public static string promptTicketNameIsEmpty = string.Empty;
    public static string promptAllAmountIsNaN = string.Empty;
    public static string promptStartDateIsEmpty = string.Empty;
    public static string promptEndDateIsEmpty = string.Empty;
    public static string promptIncludeTicketIsEmpty = string.Empty;
    public static string promptAddSuccess = string.Empty;
    public static string promptAddFaild = string.Empty;
    public static string noLimit = string.Empty;
    public static string promptTicketCountAmountIsNan = string.Empty;
    public static string amountBigRemain = string.Empty;
    public static string includeAmountNotEqualAllAmount = string.Empty;
    public static string confirmSave = string.Empty;

    public static string promptUserCountIsEmpty = string.Empty;
    public static string customernumberErrorMsg = string.Empty;
    public static string promptUserCountIsNaN = string.Empty;

    public static string promptUserRepCountIsEmpty = string.Empty;
    public static string promptUserRepCountIsNaN = string.Empty; 

    protected void Page_Load(object sender, EventArgs e)
    {
        promptDelete = GetLocalResourceObject("PromptDelete").ToString();
        promptTicketAmountIsEmpty = GetLocalResourceObject("PromptTicketAmountIsEmpty").ToString();
        promptTicketAmountIsNaN = GetLocalResourceObject("PromptTicketAmountIsNaN").ToString();
        promptAllAmountNoEmpty = GetLocalResourceObject("PromptAllAmountNoEmpty").ToString();
        promptTicketCountError = GetLocalResourceObject("PromptTicketCountError").ToString();

        promptTicketAmountError = GetLocalResourceObject("PromptTicketAmountError").ToString();
        promptAmountError = GetLocalResourceObject("PromptAmountError").ToString();
        promptTicketNameIsEmpty = GetLocalResourceObject("PromptTicketNameIsEmpty").ToString();
        promptAllAmountIsNaN = GetLocalResourceObject("PromptAllAmountIsNaN").ToString();

        promptStartDateIsEmpty = GetLocalResourceObject("PromptStartDateIsEmpty").ToString();
        promptEndDateIsEmpty = GetLocalResourceObject("PromptEndDateIsEmpty").ToString();
        promptIncludeTicketIsEmpty = GetLocalResourceObject("PromptIncludeTicketIsEmpty").ToString();

        promptAddSuccess = GetLocalResourceObject("PromptIncludeTicketIsEmpty").ToString();
        promptAddFaild = GetLocalResourceObject("PromptAddFaild").ToString();
        noLimit = GetLocalResourceObject("NoLimit").ToString();
        promptTicketCountAmountIsNan = GetLocalResourceObject("PromptTicketCountAmountIsNan").ToString();
        amountBigRemain = GetLocalResourceObject("AmountBigRemain").ToString();
        includeAmountNotEqualAllAmount = GetLocalResourceObject("IncludeAmountNotEqualAllAmount").ToString();
        confirmSave = GetLocalResourceObject("ConfirmSave").ToString();

        customernumberErrorMsg = GetLocalResourceObject("customernumberErrorMsg").ToString();
        promptUserCountIsEmpty = GetLocalResourceObject("promptUserCountIsEmpty").ToString();
        promptUserCountIsNaN = GetLocalResourceObject("promptUserCountIsNaN").ToString();

        promptUserRepCountIsEmpty = GetLocalResourceObject("promptUserRepCountIsEmpty").ToString();
        promptUserRepCountIsNaN = GetLocalResourceObject("promptUserRepCountIsNaN").ToString();


        if (!IsPostBack)
        {       
            BindChanelListBox();
            BindUserGroupListBox();
            BindPlatFormListBox();
            BindGridView();

            SetControlVal();
        }
    }

    protected void ChkCustomValue_Click(object sender, EventArgs e)
    {
        if (!chkCustomNumber.Checked)
        {
            txtCustomNumber.Text = "";
            txtCustomNumber.Enabled = false;
            lbCustomNumMsg.Text = "";
        }
        else
        {
            txtCustomNumber.Enabled = true;
        }
    }
    private void SetControlVal()
    {
        chkCustomNumber.Checked = false;
        txtCustomNumber.Text = "";
        txtCustomNumber.Enabled = false;
        txtUserRepCount.Text = "1";
        hidPackageType.Value = "0";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string packageName = this.txtPackageName.Text.Replace("'", "");
        string allAmount = this.txtAllAmount.Value.Replace("'", ""); ;//总金额

        //string fromDate = this.FromDate.Text.Replace("'", "");
        //string endDate = this.EndDate.Text.Replace("'", "");

        string fromDate = this.FromDate.Value.Replace("'", "");
        string endDate = this.EndDate.Value.Replace("'", "");

        string userCount = this.txtUserCount.Text.Replace("'", ""); //可被用户领用数，如果为空，则记录为0
        if (string.IsNullOrEmpty(userCount))
        {
            userCount = "0";
        }

        string userRepCount = this.txtUserRepCount.Text.Replace("'", ""); //可被用户领用数，如果为空，则记录为1
        if (string.IsNullOrEmpty(userRepCount))
        {
            userRepCount = "1";
        }

        string saleChannel = ""; //发放渠道
        for (int i = 0; i < LBSaleChanel.Items.Count; i++)
        {
            if (LBSaleChanel.Items[i].Selected == true)
            {
                saleChannel = saleChannel + LBSaleChanel.Items[i].Value + ",";
            }
        }
        saleChannel = saleChannel.Trim().Trim(',').Replace("'", "");

        //string useClient = this.LBUseFlatForm.SelectedValue;//使用平台
        string userGroup = "";         //领用用户组
        for (int i = 0; i < LBUserGroup.Items.Count; i++)
        {
            if (LBUserGroup.Items[i].Selected == true)
            {
                userGroup = userGroup + LBUserGroup.Items[i].Value + ",";
            }
        }
        userGroup = userGroup.Trim().Trim(',').Replace("'", "");

        string useCode = "";//用户平台
        for (int i = 0; i < LBUsePlatForm.Items.Count; i++)
        {
            if (LBUsePlatForm.Items[i].Selected == true)
            {
                useCode = useCode + LBUsePlatForm.Items[i].Value + ",";
            }
        }
        useCode = useCode.Trim().Trim(',').Replace("'", "");


        string cityID = this.txtCityID.Value;//城市ID
        //通过变量的方式，加入数据表中
        StringBuilder sql = new StringBuilder();
        CommonFunction comFun = new CommonFunction();            

        //string strPackageCode = comFun.GetRandNumString(10);//一个10位的随机数

        string strPackageCode = string.Empty;

        //if (!chkCustomNumber.Checked)
        //{
        //    strPackageCode = getPackageCode(10);//一个10位的随机数      
        //    if (strPackageCode == "")
        //    {
        //        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('领用券Code的位数已经使用完，不能生成新的Code了！');changeRemainValue();", true);
        //        return;
        //    }
        //}
        //else
        //{
        //    if (!chkCustomIsNum(txtCustomNumber.Text.Trim()))
        //    {
        //        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('自定义优惠券号码必须为10位数字！');changeRemainValue();", true);
        //        return;
        //    }

        //    if (!chkCutomerNum(txtCustomNumber.Text))
        //    {
        //        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('礼包号码与历史重复，请返回修改！');changeRemainValue()", true);
        //        return;
        //    }
        //    else
        //    {
        //        strPackageCode = txtCustomNumber.Text.Trim();
        //    }
        //}

        string batchpackageName = "";
        
        string batchNumber = txtBatchPageNumber.Text.Trim();
        string RulesCode = txtRulesCode.Text.Trim();

        if (!chkNumber(batchNumber))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('批量生产礼包数必须为大于0数字！');changeRemainValue();", true);
            return;
        }

        if (String.IsNullOrEmpty(RulesCode))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('批量生产礼包关联规则代码必须输入！');changeRemainValue();", true);
            return;
        }

        string packageType = this.hidPackageType.Value;//城市ID

        decimal decCount = decimal.Parse(batchNumber);
        List<String> list = new List<string>();
        for (int j = 0; j < decCount; j++)
        {
            strPackageCode = getPackageCode(10);//一个10位的随机数      
            if (strPackageCode == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('领用券Code的位数已经使用完，不能生成新的Code了！');changeRemainValue();", true);
                return;
            }

            batchpackageName = packageName + (j + 1).ToString();
            //=============主表=========================
            string status = "1";
            int packageID = comFun.getMaxIDfromSeq("T_LM_TICKET_PACKAGE_SEQ");

            sql.AppendLine("INSERT INTO T_LM_TICKET_PACKAGE(ID,STATUS,PACKAGENAME,PACKAGECODE,STARTDATE,ENDDATE,USERCNT,CREATETIME,AMOUNT,CLIENTCODE,USERGROUPID,USECODE,CITYID, SINGLE_USERCNT, PACKAGETYPE) VALUES  ");
            sql.AppendLine("（" + packageID + ",'" + status + "','" + batchpackageName + "','" + strPackageCode + "','" + fromDate + "','" + endDate + "'," + userCount + ",to_date( '" + System.DateTime.Now + "' , 'YYYY-MM-DD HH24:MI:SS' )," + allAmount + ",'" + saleChannel + "','" + userGroup + "','" + useCode + "','" + cityID + "'," + userRepCount + ",'" + packageType + "') ");

            //to_date ( '2007-12-20 18:31:34' , 'YYYY-MM-DD HH24:MI:SS' ) 
            //循环执行

            list.Add(sql.ToString());
            sql.Clear();
            //==================把子表信息加入到表 T_LM_TICKET
            string[] lblNumber = Request.Form.GetValues("lblNumber");
            string[] lblAmount = Request.Form.GetValues("lblAmount");

            int ticketCodeFlag = 0;
            StringBuilder sql_ticket = new StringBuilder();
            for (int i = 0; i < lblNumber.Length; i++)
            {
                sql_ticket.AppendLine("INSERT INTO T_LM_TICKET(ID,STATUS,TICKETCODE,TICKETAMT,CREATETIME,PACKAGECODE,TICKETCNT, TICKETRULECODE) VALUES ");
                int intGetID = comFun.getMaxIDfromSeq("T_LM_TICKET_SEQ");//ID的值

                string strStatus = "1";
                //string strTicketCode = comFun.GetRandNumString(13);   //ticket的code,10位数的随机数
                string strTicketCode = getTicketCode(13);   //ticket的code,10位数的随机数
                if (strTicketCode == "")
                {

                    ticketCodeFlag = 1;
                    break;
                }

                DateTime CREATETIME = System.DateTime.Now;
                string IntTicketCnt = lblNumber[i].Trim().ToString();
                string IntTicketAmt = lblAmount[i].Trim().ToString();

                //加入值每个字段的值
                sql_ticket.AppendLine("( " + intGetID + ",'" + strStatus + "','" + strTicketCode + "'," + IntTicketAmt + ",to_date('" + CREATETIME + "' , 'YYYY-MM-DD HH24:MI:SS'),'" + strPackageCode + "'," + IntTicketCnt + ",'" + RulesCode + "')");

                //add to list
                list.Add(sql_ticket.ToString());
                sql_ticket.Clear();
            }


            if (ticketCodeFlag == 1)//表示在产生Ticket code的时候，已经没法产生可用的Ticket Code了。
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('内含Ticket Code的位数已经使用完，无法生成新的Code了！');changeRemainValue()", true);
                return;
            }
        }
        //=================================================
        try
        {
            DbHelperOra.ExecuteSqlTran(list);
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('领用券新增成功！');", true);

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpen();", true);
            //清除页面内容
            clearValue();
            //bind gridview
            BindGridView();


        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('领用券新增失败！');changeRemainValue()", true);
            //ex.ToString();
        }
    }

    private bool chkNumber(string batchNumber)
    {
        try
        {
            if (decimal.Parse(batchNumber) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private bool chkCutomerNum(string pagecode)
    {
        string sqlString = "select t.packagecode from t_lm_ticket_package t where t.packagecode=:PACKAGECODE";

        OracleParameter[] parm ={
                                    new OracleParameter("PACKAGECODE",OracleType.VarChar)
                                };
        parm[0].Value = pagecode;

        DataSet dsResult = DbHelperOra.Query(sqlString, false, parm);

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void BindGridView()
    {
        this.gridViewRegion.DataSource = getDs().Tables[0].DefaultView;
        this.gridViewRegion.DataBind();
    }

    //清除控件中的数据
    private void clearValue()
    {
        this.txtPackageName.Text = "";
        this.txtAllAmount.Value = "";
        //this.FromDate.Text = "";
        //this.EndDate.Text = "";
        
        this.FromDate.Value = "";
        this.EndDate.Value = "";

        this.txtNumber.Text = "";
        this.txtAmount.Text = "";
        this.lbRemainAmount.Text = "";
        this.txtUserCount.Text = "";

        this.LBUsePlatForm.ClearSelection();
        this.LBUserGroup.ClearSelection();
        this.LBSaleChanel.ClearSelection();
        this.txtCityID.Value = "";

        chkCustomNumber.Checked = false;
        txtCustomNumber.Text = "";
        txtCustomNumber.Enabled = false;
        lbCustomNumMsg.Text = "";
        txtUserRepCount.Text = "1";
    }

    private DataSet getDs()
    {
        string sql = "select * from T_LM_TICKET_PACKAGE order by ID desc";
        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;

    }

    #region 绑定类别名称到ListBox

    //发放渠道
    private void BindChanelListBox()
    {
        //string sql = "select chanelcode, chanelnm from t_lm_chanel";
        string sql = "select CHANEL_CODE, CHANEL_NAME from T_LM_B_CHANEL where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["CHANEL_NAME"] = noLimit;
        row1["CHANEL_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);

        LBSaleChanel.DataTextField = "CHANEL_NAME";
        LBSaleChanel.DataValueField = "CHANEL_CODE";
        LBSaleChanel.DataSource = myTable.DefaultView;
        LBSaleChanel.DataBind();
    }

    //使用平台,原始用的是usecode
    private void BindPlatFormListBox()
    {
        
        string sql = "select PLATFORM_CODE, PLATFORM_NAME from T_LM_B_PLATFORM where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["PLATFORM_NAME"] = noLimit;
        row1["PLATFORM_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);

        LBUsePlatForm.DataTextField = "PLATFORM_NAME";
        LBUsePlatForm.DataValueField = "PLATFORM_CODE";
        LBUsePlatForm.DataSource = myTable.DefaultView;
        LBUsePlatForm.DataBind();
    
    }
    
    //领用券用户组
    private void BindUserGroupListBox()
    {
        //string sql = "select clientcode, clientnm from t_lm_client";
        //DataTable myTable = DbHelperOra.Query(sql).Tables[0];

        //DataRow row1 = myTable.NewRow();
        //row1["clientnm"] = noLimit;
        //row1["clientcode"] = "";
        //myTable.Rows.InsertAt(row1, 0);

        //LBClientCode.DataTextField = "clientnm";
        //LBClientCode.DataValueField = "clientcode";
        //LBClientCode.DataSource = myTable.DefaultView;
        //LBClientCode.DataBind();


        string sql = "select ID, USERGROUP_NAME from T_LM_USERGROUP";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["USERGROUP_NAME"] = noLimit;
        row1["ID"] = DBNull.Value;
        myTable.Rows.InsertAt(row1, 0);

        LBUserGroup.DataTextField = "USERGROUP_NAME";
        LBUserGroup.DataValueField = "ID";
        LBUserGroup.DataSource = myTable.DefaultView;
        LBUserGroup.DataBind();

    }

    #endregion

    protected void gridViewRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewRegion.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    #region 生产PackageCode
    //用于产生新的一个UserCode
    private string getPackageCode(int length)
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
    private bool ExistCode(string packagecode)
    {
        string sqlExist = "select  packagecode from t_lm_ticket_package where packagecode=:packagecode";
        OracleParameter[] parmExist = { new OracleParameter("packagecode", OracleType.VarChar) };
        parmExist[0].Value = packagecode;
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
    #endregion

    #region 生成TicketCode
    //用于产生新的一个UserCode
    private string getTicketCode(int length)
    {
        CommonFunction comFun = new CommonFunction();
        string newCode = comFun.GetRandNumString(length);
        bool bExist = ExistTicketCode(newCode);
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
                if (ExistTicketCode(newCode) == false)
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
    /// 判断是否存在TicketCode
    /// </summary>
    /// <returns></returns>
    private bool ExistTicketCode(string TicketCode)
    {
        string sqlExist = "select  ticketcode from t_lm_ticket where ticketcode=:ticketcode";
        OracleParameter[] parmExist = { new OracleParameter("ticketcode", OracleType.VarChar) };
        parmExist[0].Value = TicketCode;
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
    #endregion

    protected void txtCustomNumber_TextChanged(object sender, EventArgs e)
    {
        if (!chkCustomNumber.Checked)
        {
            return;
        }

        if (!chkCustomIsNum(txtCustomNumber.Text.Trim()))
        {
            lbCustomNumMsg.Text = "自定义优惠券号码必须为10位数字！";
            return;
        }

        if (!chkCutomerNum(txtCustomNumber.Text.Trim()))
        {
            lbCustomNumMsg.Text = "礼包号码与历史重复，请返回修改！";
            return;
        }

        lbCustomNumMsg.Text = "该礼包号码可用！";
    }

    private bool chkCustomIsNum(string pageCode)
    {
        if (pageCode.Length != 10 || !chkNum(pageCode))
        {
            return false;
        }

        return true;
    }

    private bool chkNum(string parm)
    {
        try
        {
            if (decimal.Parse(parm) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
}