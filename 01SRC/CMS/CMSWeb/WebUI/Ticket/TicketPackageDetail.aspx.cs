using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using HotelVp.Common.DBUtility;
using System.Data;


public partial class Ticket_TicketPackageDetail : BasePage
{
    public static string closeText = Resources.MyGlobal.CloseText;  
    public static string noLimit = Resources.MyGlobal.NoLimitText;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindChanelListBox();
            BindPlatFormListBox();
            BindUserGroupListBox();

            string PackageCode = Request.QueryString["PACKAGECODE"];
            string sql = "select * from T_LM_TICKET_PACKAGE where PACKAGECODE ='" + PackageCode + "'";
            DataTable dt = DbHelperOra.Query(sql, false).Tables[0];

            //select id, syssource, status, packagecode, startdate, enddate, usercnt, updatetime, createtime, amount, field1, field2, field3, field4, chanelcode, clientcode, usecode, packagename, cityid from t_lm_ticket_package



            //return ds;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.txtPackageName.Text = dt.Rows[i]["packagename"].ToString();
                this.txtAllAmount.Value = dt.Rows[i]["amount"].ToString();
                this.FromDate.Text = dt.Rows[i]["startdate"].ToString();
                this.EndDate.Text = dt.Rows[i]["enddate"].ToString();
                this.txtUserCount.Text = dt.Rows[i]["usercnt"].ToString();
                this.txtUserRepCount.Text = dt.Rows[i]["SINGLE_USERCNT"].ToString();
                this.txtCityID.Value = dt.Rows[i]["cityid"].ToString();

                if ("0".Equals(dt.Rows[i]["PACKAGETYPE"].ToString()))
                {
                    lbPackageType.Text = "运营";
                }
                else if ("1".Equals(dt.Rows[i]["PACKAGETYPE"].ToString()))
                {
                    lbPackageType.Text = "市场";
                }
                else if ("2".Equals(dt.Rows[i]["PACKAGETYPE"].ToString()))
                {
                    lbPackageType.Text = "其他";
                }
                else
                {
                    lbPackageType.Text = "";
                }

                BindTicketGridView(PackageCode);

                string groupid = dt.Rows[i]["USERGROUPID"].ToString();
                string salechannel = dt.Rows[i]["clientcode"].ToString();
                string usecode = dt.Rows[i]["usecode"].ToString();

                //用户组
                BindListBox(this.LBUserGroup, groupid);

                //使用的终端平台
                BindListBox(this.LBUseCode, usecode);

                //可以使用的渠道
                BindListBox(this.LBSaleChannel, salechannel);
            }
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


    #region 绑定类别名称到ListBox
    //发放渠道
    private void BindChanelListBox()
    {
        string sql = "select CHANEL_CODE, CHANEL_NAME from T_LM_B_CHANEL where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["CHANEL_NAME"] = noLimit;
        row1["CHANEL_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);

        LBSaleChannel.DataTextField = "CHANEL_NAME";
        LBSaleChannel.DataValueField = "CHANEL_CODE";
        LBSaleChannel.DataSource = myTable.DefaultView;
        LBSaleChannel.DataBind();
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

        LBUseCode.DataTextField = "PLATFORM_NAME";
        LBUseCode.DataValueField = "PLATFORM_CODE";
        LBUseCode.DataSource = myTable.DefaultView;
        LBUseCode.DataBind();

    }

    //领用券用户组
    private void BindUserGroupListBox()
    {
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


    private void BindTicketGridView(string packagecode)
    {
        string sql = "select * from t_lm_ticket where packagecode='" + packagecode + "' order by ID";
        DataSet ds = DbHelperOra.Query(sql, false);
        this.gridViewTicket.DataSource = ds.Tables[0].DefaultView;
        this.gridViewTicket.DataBind();
    }
}