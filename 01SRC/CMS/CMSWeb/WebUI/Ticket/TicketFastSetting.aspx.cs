using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OracleClient;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Data;
using System.Collections;

public partial class Ticket_TicketFastSetting : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绑定销售渠道 
            BindSaleChannel();
            //绑定发放渠道 
            BindChanelListBox();
            //绑定使用平台
            BindPlatFormListBox();
            //绑定领用平台 
            BindUsePlatFormListBox();
            //绑定价格代码 
            LBPriceCode.Items[0].Selected = true;

            SetControlVal();

            BindPackageGridView();//绑定列表
        }
    }

    //绑定销售渠道
    private void BindSaleChannel()
    {
        string sql = "select CHANEL_CODE, CHANEL_NAME from T_LM_B_CHANEL where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];
        DataRow row1 = myTable.NewRow();
        row1["CHANEL_NAME"] = "无限制";
        row1["CHANEL_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);
        LBSaleChannel.DataTextField = "CHANEL_NAME";
        LBSaleChannel.DataValueField = "CHANEL_CODE";
        LBSaleChannel.DataSource = myTable.DefaultView;
        LBSaleChannel.DataBind();
        LBSaleChannel.SelectedIndex = 0;
    }

    //绑定发放渠道 
    private void BindChanelListBox()
    {
        string sql = "select CHANEL_CODE, CHANEL_NAME from T_LM_B_CHANEL where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["CHANEL_NAME"] = "无限制";
        row1["CHANEL_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);

        LBSaleChanel.DataTextField = "CHANEL_NAME";
        LBSaleChanel.DataValueField = "CHANEL_CODE";
        LBSaleChanel.DataSource = myTable.DefaultView;
        LBSaleChanel.DataBind();
        LBSaleChanel.SelectedIndex = 0;
    }

    //使用平台 
    private void BindPlatFormListBox()
    {
        string sql = "select PLATFORM_CODE, PLATFORM_NAME from T_LM_B_PLATFORM where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["PLATFORM_NAME"] = "无限制";
        row1["PLATFORM_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);

        LBUsePlatForm.DataTextField = "PLATFORM_NAME";
        LBUsePlatForm.DataValueField = "PLATFORM_CODE";
        LBUsePlatForm.DataSource = myTable.DefaultView;
        LBUsePlatForm.DataBind();
        LBUsePlatForm.SelectedIndex = 0;
    }

    //使用平台
    private void BindUsePlatFormListBox()
    {

        string sql = "select PLATFORM_CODE, PLATFORM_NAME from T_LM_B_PLATFORM where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["PLATFORM_NAME"] = "无限制";
        row1["PLATFORM_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);

        LBUsePlatFormListBox.DataTextField = "PLATFORM_NAME";
        LBUsePlatFormListBox.DataValueField = "PLATFORM_CODE";
        LBUsePlatFormListBox.DataSource = myTable.DefaultView;
        LBUsePlatFormListBox.DataBind();
        LBUsePlatFormListBox.SelectedIndex = 0;
    }

    //新增
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string ticketRuleCode = AddTicketRule();//新增规则
        if (ticketRuleCode != "")
        {
            string ticketCode = AddTicketPackage();//新增礼包
            if (ticketRuleCode != "" && ticketCode != "")
            {
                BindTicketByUseRule(ticketRuleCode, ticketCode); //绑定规则和礼包        
                clearValue();//清除页面内容
            }
        }
        BindPackageGridView();//重新绑定Gridview
    }

    //新增规则
    public string AddTicketRule()
    {
        StringBuilder sql = new StringBuilder();
        //Oracle sql 语法
        sql.AppendLine("INSERT INTO T_LM_TICKET_RULE(ID,TICKETRULECODE,CREATETIME,ORDAMT,STARTTIME,ENDTIME,STARTDATE,ENDDATE,HOTELID,CITYID,USEFLG,TICKETRULEDESC,TICKETRULENAME,CLIENTCODE,USECODE,HOTELNAME,USERGROUPID,PRICE_CODE) VALUES ( ");
        sql.AppendLine(":ID,:TICKETRULECODE,:CREATETIME,:ORDAMT,:STARTTIME,:ENDTIME,:STARTDATE,:ENDDATE,:HOTELID,:CITYID,:USEFLG,:TICKETRULEDESC,:TICKETRULENAME,:CLIENTCODE,:USECODE,:HOTELNAME,:USERGROUPID,:PRICECODE) ");
        OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("TICKETRULECODE",OracleType.VarChar),                                    
                                    new OracleParameter("CREATETIME",OracleType.DateTime),
                                    new OracleParameter("ORDAMT",OracleType.Int32),
                                    new OracleParameter("STARTTIME",OracleType.VarChar),
                                    new OracleParameter("ENDTIME",OracleType.VarChar),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("USEFLG",OracleType.VarChar),
                                    new OracleParameter("TICKETRULEDESC",OracleType.NVarChar),
                                    new OracleParameter("TICKETRULENAME",OracleType.NVarChar),
                                    new OracleParameter("CLIENTCODE",OracleType.NVarChar),//销售渠道
                                    new OracleParameter("USECODE",OracleType.NVarChar),
                                    new OracleParameter("HOTELNAME",OracleType.NVarChar),
                                    new OracleParameter("USERGROUPID",OracleType.VarChar),//用户组
                                    new OracleParameter("PRICECODE",OracleType.VarChar),
                                };

        string strTicketCode = "10000";
        string strTicketName = this.txtRuleName.Text;//规则名称       
        string strFromDate = this.fromDate.Value;//开始日期
        string strEndDate = this.endDate.Value;//结束日期

        string strStartTime = "";// this.txtStartTime.Text;//么用 开始日期
        string strEndTime = "";//this.txtEndTime.Text;//么用 结束日期
        string strOrdAmt = this.txtOrdAmt.Text;//最低订单金额
        if (strOrdAmt == "")
        {
            strOrdAmt = "0";
        }
        string strHotelID = "";// this.txthotelid.Value;//么用
        string strCityID = "";// this.cityid.SelectedValue;//么用
        string strRuleDesc = this.txtRuleDesc.Text;//优惠券描述(展示在客户端)
        string strHotelName = "";// this.txtHotelName.Value;//么用

        string useGroup = "";     //使用用户组  么用

        //销售渠道
        string saleChannel = "";//销售渠道  HOTELVP  设置基本规则 
        for (int i = 0; i < LBSaleChannel.Items.Count; i++)
        {
            if (LBSaleChannel.Items[i].Selected == true)
            {
                saleChannel = saleChannel + LBSaleChannel.Items[i].Value + ",";
            }
        }
        saleChannel = saleChannel.Trim().Trim(',');

        string useCode = "";//使用平台   ANDROID    设置基本规则 
        for (int i = 0; i < LBUsePlatForm.Items.Count; i++)
        {
            if (LBUsePlatForm.Items[i].Selected == true)
            {
                useCode = useCode + LBUsePlatForm.Items[i].Value + ",";
            }
        }
        useCode = useCode.Trim().Trim(',');

        //价格代码
        string priceCode = "";//价格代码   LMBAR2   设置基本规则
        for (int i = 0; i < LBPriceCode.Items.Count; i++)
        {
            if (LBPriceCode.Items[i].Selected == true)
            {
                priceCode = priceCode + LBPriceCode.Items[i].Value + ",";
            }
        }
        priceCode = priceCode.Trim().Trim(',');

        CommonFunction comFun = new CommonFunction();
        parm[0].Value = comFun.getMaxIDfromSeq("T_LM_TICKET_RULE_SEQ");
        int ticketrulecode = DbHelperOra.GetMaxID("TICKETRULECODE", "T_LM_TICKET_RULE", false);
        if (ticketrulecode == 1)
        {
            strTicketCode = "10000";
        }
        else
        {
            strTicketCode = ticketrulecode.ToString();
        }

        parm[1].Value = strTicketCode;//10504
        parm[2].Value = System.DateTime.Now;
        parm[3].Value = strOrdAmt;//121
        parm[4].Value = strStartTime;//null
        parm[5].Value = strEndTime;//null
        parm[6].Value = strFromDate;//开始日期
        parm[7].Value = strEndDate;//结束日期
        parm[8].Value = strHotelID;
        parm[9].Value = strCityID;
        parm[10].Value = "1";
        parm[11].Value = strRuleDesc;//优惠券描述(展示在客户端)
        parm[12].Value = strTicketName;//规则名称    
        parm[13].Value = saleChannel;//销售渠道
        parm[14].Value = useCode;//使用平台
        parm[15].Value = strHotelName;
        parm[16].Value = useGroup;
        parm[17].Value = priceCode;//价格代码
        try
        {
            DbHelperOra.ExecuteSql(sql.ToString(), parm);
            //重新绑定Gridview
            //BindGridView();
            // this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + AddRuleSuccess + "');", true);
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpen();", true);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('添加成功');", true);
            return strTicketCode;
        }
        catch (Exception ex)
        {
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + AddRuleFaild + "');", true);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('新增失败');", true);
            return "";
            //ex.ToString();
        }
    }

    //新增礼包
    public string AddTicketPackage()
    {
        string packageName = this.txtRuleName.Text.Replace("'", ""); //this.txtPackageName.Text.Replace("'", "");//领用券名称
        string allAmount = this.txtAcount.Text.Replace("'", ""); //this.txtAllAmount.Value.Replace("'", ""); ;//总金额

        //string fromDate = this.FromDate.Text.Replace("'", "");
        //string endDate = this.EndDate.Text.Replace("'", "");

        string fromDate = this.recipientFormDate.Value.Replace("'", "");//最早可领用日期
        string endDate = this.recipientEndDate.Value.Replace("'", "");//最晚可领用日期

        string userCount = this.txtUserCount.Text.Replace("'", ""); //可被用户领用数，如果为空，则记录为0   不同用户可领用次数：
        if (string.IsNullOrEmpty(userCount))
        {
            userCount = "0";
        }

        string userRepCount = this.txtUserRepCount.Text.Replace("'", ""); //可被用户领用数，如果为空，则记录为1  同一用户可领用次数：
        if (string.IsNullOrEmpty(userRepCount))
        {
            userRepCount = "1";
        }

        string saleChannel = ""; //发放渠道  QUNAR    优惠券礼包管理  
        for (int i = 0; i < LBSaleChanel.Items.Count; i++)
        {
            if (LBSaleChanel.Items[i].Selected == true)
            {
                saleChannel = saleChannel + LBSaleChanel.Items[i].Value + ",";
            }
        }
        saleChannel = saleChannel.Trim().Trim(',').Replace("'", "");

        //string useClient = this.LBUseFlatForm.SelectedValue;//使用平台
        string userGroup = "";         //领用用户组  么用 

        string useCode = "";//用户平台   ANDROID   优惠券礼包管理 
        for (int i = 0; i < LBUsePlatFormListBox.Items.Count; i++)
        {
            if (LBUsePlatFormListBox.Items[i].Selected == true)
            {
                useCode = useCode + LBUsePlatFormListBox.Items[i].Value + ",";
            }
        }
        useCode = useCode.Trim().Trim(',').Replace("'", "");


        string cityID = "";// this.txtCityID.Value;//城市ID
        string packageType = this.hidPackageType.Value;//优惠券类型 
        //通过变量的方式，加入数据表中
        StringBuilder sql = new StringBuilder();
        CommonFunction comFun = new CommonFunction();

        string strPackageCode = string.Empty;
        if (!chkCustomNumber.Checked)
        {
            strPackageCode = getPackageCode(10);//一个10位的随机数      
            if (strPackageCode == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('领用券Code的位数已经使用完，不能生成新的Code了！');changeRemainValue();", true);
                return "";
            }
        }
        else
        {
            if (!chkCustomIsNum(txtCustomNumber.Text.Trim()))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('自定义优惠券号码必须为10位数字！');changeRemainValue();", true);
                return "";
            }

            if (!chkCutomerNum(txtCustomNumber.Text))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('礼包号码与历史重复，请返回修改！');changeRemainValue()", true);
                return "";
            }
            else
            {
                strPackageCode = txtCustomNumber.Text.Trim();
            }
        }
        //=============主表=========================
        string status = "1";
        int packageID = comFun.getMaxIDfromSeq("T_LM_TICKET_PACKAGE_SEQ");

        sql.AppendLine("INSERT INTO T_LM_TICKET_PACKAGE(ID,STATUS,PACKAGENAME,PACKAGECODE,STARTDATE,ENDDATE,USERCNT,CREATETIME,AMOUNT,CLIENTCODE,USERGROUPID,USECODE,CITYID, SINGLE_USERCNT, PACKAGETYPE) VALUES  ");
        sql.AppendLine("（" + packageID + ",'" + status + "','" + packageName + "','" + strPackageCode + "','" + fromDate + "','" + endDate + "'," + userCount + ",to_date( '" + System.DateTime.Now + "' , 'YYYY-MM-DD HH24:MI:SS' )," + allAmount + ",'" + saleChannel + "','" + userGroup + "','" + useCode + "','" + cityID + "'," + userRepCount + ",'" + packageType + "') ");

        //to_date ( '2007-12-20 18:31:34' , 'YYYY-MM-DD HH24:MI:SS' ) 
        //执行   一张券 任意金额 
        List<String> list = new List<string>();
        list.Add(sql.ToString());

        //==================把子表信息加入到表 T_LM_TICKET
        //string[] lblNumber = Request.Form.GetValues("lblNumber"); //1次 
        //string[] lblAmount = Request.Form.GetValues("lblAmount"); //20元
        string lblNumber = "1";
        string lblAmount = this.txtAcount.Text.Replace("'", "");

        int ticketCodeFlag = 0;
        string strTicketCode = "";
        StringBuilder sql_ticket = new StringBuilder();
        for (int i = 0; i < 1; i++)
        {
            sql_ticket.AppendLine("INSERT INTO T_LM_TICKET(ID,STATUS,TICKETCODE,TICKETAMT,CREATETIME,PACKAGECODE,TICKETCNT) VALUES ");
            int intGetID = comFun.getMaxIDfromSeq("T_LM_TICKET_SEQ");//ID的值

            string strStatus = "1";
            //string strTicketCode = comFun.GetRandNumString(13);   //ticket的code,10位数的随机数
            strTicketCode = getTicketCode(13);   //ticket的code,10位数的随机数
            if (strTicketCode == "")
            {

                ticketCodeFlag = 1;
                break;
            }

            DateTime CREATETIME = System.DateTime.Now;
            string IntTicketCnt = lblNumber;
            string IntTicketAmt = lblAmount;

            //加入值每个字段的值
            sql_ticket.AppendLine("( " + intGetID + ",'" + strStatus + "','" + strTicketCode + "'," + IntTicketAmt + ",to_date('" + CREATETIME + "' , 'YYYY-MM-DD HH24:MI:SS'),'" + strPackageCode + "'," + IntTicketCnt + ")");

            //add to list
            list.Add(sql_ticket.ToString());
            sql_ticket.Clear();
        }


        if (ticketCodeFlag == 1)//表示在产生Ticket code的时候，已经没法产生可用的Ticket Code了。
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('内含Ticket Code的位数已经使用完，无法生成新的Code了！');changeRemainValue()", true);
            return "";
        }
        //=================================================
        try
        {
            DbHelperOra.ExecuteSqlTran(list);
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('领用券新增成功！');", true); 
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('添加成功');", true);
            return strTicketCode;
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('领用券新增失败！');changeRemainValue()", true);
            //ex.ToString();
            return "";
        }
    }

    //绑定规则和礼包        
    public bool BindTicketByUseRule(string ticketRuleCode, string ticketCode)
    {
        //修改数据内容
        List<string> li = new List<string>();
        //=======判断RuleCode 和TicketCode中的规则是否有冲突=======
        int checkFlag = 1;//默认是没有冲突
        for (int k = 0; k < 1; k++)
        {
            Hashtable ht = hsDateAndPackage(ticketRuleCode, ticketCode);
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
            for (int i = 0; i < 1; i++)
            {
                string tempTicketCode = ticketCode;
                string sql = "UPDATE T_LM_TICKET SET TICKETRULECODE ='" + ticketRuleCode + "' WHERE TICKETCODE='" + tempTicketCode + "'";
                li.Add(sql);
            }
            if (li.Count > 0)
            {
                try
                {
                    DbHelperOra.ExecuteSqlTran(li);
                    //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptSettingSuccess + "');", true);
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('绑定成功');", true);
                    return true;
                }
                catch
                {
                    //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptSettingFaild + "');", true);
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('绑定失败');", true);
                }
            }
        }
        return false;
    }

    //清除控件中的数据
    private void clearValue()
    {
        this.txtRuleName.Text = "";
        this.fromDate.Value = "";
        this.endDate.Value = "";
        this.txtOrdAmt.Text = "";
        this.txtRuleDesc.Text = "";

        LBUsePlatForm.ClearSelection();
        LBUsePlatForm.Items[0].Selected = true;
        LBSaleChannel.ClearSelection();
        LBSaleChannel.Items[0].Selected = true;
        LBPriceCode.ClearSelection();
        LBPriceCode.Items[0].Selected = true;


        this.txtCustomNumber.Text = "";
        this.recipientFormDate.Value = "";
        this.recipientEndDate.Value = "";
        this.txtAcount.Text = "";
        this.txtUserCount.Text = "";
        this.txtUserRepCount.Text = "";

        this.LBUsePlatFormListBox.ClearSelection();
        LBUsePlatFormListBox.Items[0].Selected = true;
        this.LBSaleChanel.ClearSelection();
        LBSaleChanel.Items[0].Selected = true;

        chkCustomNumber.Checked = false;
        txtCustomNumber.Text = "";
        txtCustomNumber.Enabled = false;
        lbCustomNumMsg.Text = "";
        txtUserRepCount.Text = "1";
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

    private void SetControlVal()
    {
        chkCustomNumber.Checked = false;
        txtCustomNumber.Text = "";
        txtCustomNumber.Enabled = false;
        txtUserRepCount.Text = "1";
        hidPackageType.Value = "2";
    }

    //判断领用规则和使用规则是否有冲突
    private Hashtable hsDateAndPackage(string ruleCode, string ticketCode)
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
        if ((arrRule.Length == 1 && string.IsNullOrEmpty(arrRule[0])) || (arrPackage.Length == 1 && string.IsNullOrEmpty(arrPackage[0])))
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

    private void BindPackageGridView()
    {
        string sql = "select * from t_lm_ticket_package order by ID desc  ";
        DataSet ds = DbHelperOra.Query(sql, false);
        this.gridViewPackage.DataSource = ds.Tables[0].DefaultView;
        this.gridViewPackage.DataBind();
    }

    protected void gridViewPackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=t");
        } 
    }

    protected void gridViewPackage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewPackage.PageIndex = e.NewPageIndex;
        BindPackageGridView();
    }
}