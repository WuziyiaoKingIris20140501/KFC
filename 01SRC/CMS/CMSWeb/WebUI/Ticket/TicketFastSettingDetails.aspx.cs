using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using HotelVp.Common.DBUtility;
using System.Data;

public partial class WebUI_Ticket_TicketFastSettingDetails : System.Web.UI.Page
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

            string packagecode = Request.QueryString["packagecode"] == null ? "" : Request.QueryString["packagecode"].ToString();

            //t_lm_ticket   找到  内嵌优惠券   
            BindTicketGridView(packagecode);
            //t_lm_ticket_package   礼包   
            BindDataTicketPackage(packagecode);
            //t_lm_ticket_rule   规则  .
            
        }
    }

    public void BindDataTicketPackage(string PackageCode)
    {
        string sql = "select * from T_LM_TICKET_PACKAGE where PACKAGECODE ='" + PackageCode + "'";
        DataTable dt = DbHelperOra.Query(sql, false).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //this.txtRuleName.Text = dt.Rows[i]["packagename"].ToString();
            this.txtAllAmount.Text = dt.Rows[i]["amount"].ToString();
            this.recipientFormDate.Value = dt.Rows[i]["startdate"].ToString();
            this.recipientEndDate.Value = dt.Rows[i]["enddate"].ToString();
            this.txtUserCount.Text = dt.Rows[i]["usercnt"].ToString();
            this.txtUserRepCount.Text = dt.Rows[i]["SINGLE_USERCNT"].ToString(); 

            if ("0".Equals(dt.Rows[i]["PACKAGETYPE"].ToString()))
            {
                txtPackageType.Text = "运营";
            }
            else if ("1".Equals(dt.Rows[i]["PACKAGETYPE"].ToString()))
            {
                txtPackageType.Text = "市场";
            }
            else if ("2".Equals(dt.Rows[i]["PACKAGETYPE"].ToString()))
            {
                txtPackageType.Text = "其他";
            }
            else
            {
                txtPackageType.Text = "";
            }

            BindTicketGridView(PackageCode);

            string groupid = dt.Rows[i]["USERGROUPID"].ToString();
            string salechannel = dt.Rows[i]["clientcode"].ToString();
            string usecode = dt.Rows[i]["usecode"].ToString();

            ////用户组
            //BindListBox(this.LBUserGroup, groupid);

            //使用的终端平台
            BindListBox(this.LBUsePlatFormListBox, usecode);

            //可以使用的渠道
            BindListBox(this.LBSaleChanel, salechannel);
        }
    }

    private void BindTicketGridView(string packagecode)
    {
        string sql = "select * from t_lm_ticket where packagecode='" + packagecode + "' order by ID";
        DataSet ds = DbHelperOra.Query(sql, false);
        this.gridViewTicket.DataSource = ds.Tables[0].DefaultView;
        this.gridViewTicket.DataBind();

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            BindDataTicketRule(ds.Tables[0].Rows[0]["ticketrulecode"].ToString());
        }
    }

    public void BindDataTicketRule(string ticketRuleCode)
    {
        string sql = "select * from T_LM_TICKET_RULE where TICKETRULECODE='" + ticketRuleCode + "'";
        DataTable dt = DbHelperOra.Query(sql, false).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.txtRuleName.Text = dt.Rows[i]["ticketrulename"].ToString();
            this.fromDate.Value = dt.Rows[i]["startdate"].ToString();
            this.endDate.Value = dt.Rows[i]["enddate"].ToString();
            this.txtOrdAmt.Text = dt.Rows[i]["ordamt"].ToString();
            this.txtRuleDesc.Text = dt.Rows[i]["ticketruledesc"].ToString();
             
            string pricecode = dt.Rows[i]["PRICE_CODE"].ToString(); 
            string salechannel = dt.Rows[i]["clientcode"].ToString();
            string usecode = dt.Rows[i]["usecode"].ToString(); 

              
            //使用的终端平台
            BindListBox(LBUsePlatForm, usecode);

            //绑定销售渠道
            BindListBox(LBSaleChannel, salechannel);

            //绑定价格代码
            BindListBox(LBPriceCode, pricecode);
        }
    }

    /// <summary>
    /// 取值，把选中的
    /// </summary>
    /// <param name="LB"></param>
    /// <param name="strCode"></param>
    private void BindListBox(ListBox LB, string strCode)
    {
        string[] arrCode = strCode.Split(','); //分割成数组
        for (int j = 0; j < arrCode.Length; j++)
        {
            for (int i = 0; i < LB.Items.Count; i++)
            {
                if (LB.Items[i].Value == arrCode[j])
                {
                    LB.Items[i].Selected = true;
                    break;
                }
            }
        }
    }  

    #region  绑定ListBox
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
    }
    #endregion 
}