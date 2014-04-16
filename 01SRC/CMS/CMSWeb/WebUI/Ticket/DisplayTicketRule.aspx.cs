using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using HotelVp.Common;
using HotelVp.Common.DBUtility;
using System.Data;

public partial class Ticket_DisplayTicketRule : BasePage
{
    public static string closeText = Resources.MyGlobal.CloseText;
    public static string noLimitText = Resources.MyGlobal.NoLimitText;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCityDDL();
            //==========================
            BindClientListBox();
            BindPlatFormListBox();
            BindSaleChannel();

            string ticketRuleCode = Request.QueryString["TICKETRULECODE"];
            string sql = "select * from T_LM_TICKET_RULE where TICKETRULECODE='" + ticketRuleCode + "'";
            DataTable dt = DbHelperOra.Query(sql, false).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ticketrulename = dt.Rows[i]["ticketrulename"].ToString();
                string startdate = dt.Rows[i]["startdate"].ToString();
                string enddate = dt.Rows[i]["enddate"].ToString();
                string starttime = dt.Rows[i]["starttime"].ToString();
                string endtime = dt.Rows[i]["endtime"].ToString();
                string hotelid = dt.Rows[i]["hotelid"].ToString();
                string cityid = dt.Rows[i]["cityid"].ToString();
                string ticketruledesc = dt.Rows[i]["ticketruledesc"].ToString();
                string chanelcode = dt.Rows[i]["chanelcode"].ToString();
                string pricecode = dt.Rows[i]["PRICE_CODE"].ToString();
                string groupcode = dt.Rows[i]["usergroupid"].ToString();
                string salechannel = dt.Rows[i]["clientcode"].ToString();

                string usecode = dt.Rows[i]["usecode"].ToString();
                string hotelname = dt.Rows[i]["hotelname"].ToString();
                string ordamt = dt.Rows[i]["ordamt"].ToString();


                //===给前台控件赋值
                this.txtRuleName.Text = ticketrulename;//规则名称
                this.fromDate.Text = startdate;
                this.endDate.Text = enddate;
                this.txtStartTime.Text = starttime;
                this.txtEndTime.Text = endtime;
                this.txtOrdAmt.Text = ordamt;
                this.txtHotelName.Value = hotelname;
                this.txthotelid.Value = hotelid;
                this.txtRuleDesc.Text = ticketruledesc;
                this.cityid.SelectedValue = cityid;
                
                //用户组
                BindListBox(LBUserGroup,groupcode);

                //使用的终端平台
                BindListBox(LBUseCode, usecode); 
               
                //绑定销售渠道
                BindListBox(LBSaleChannel, salechannel);

                //绑定价格代码
                BindListBox(LBPriceCode, pricecode); 
            }
        }
    }

    private void BindListBox(ListBox LB,string strCode )
    {
        //LB.Items.Clear();//先清空所有数据
        string[] arrCode = strCode.Split(','); //分割成数组
        for (int j = 0; j < arrCode.Length; j++)
        {
            //ListItem li = new ListItem(arrCode[j], arrCode[j]);      
            //LB.Items.Add(li);           
            for (int i = 0; i < LB.Items.Count; i++)
            {
                if (LB.Items[i].Value == arrCode[j])
                {
                    LB.Items[i].Selected = true;
                    break;
                }                
            }
            //LB.SelectedValue = arrCode[j];
        }
    }  


    private void BindCityDDL()
    {
       
        string sql = " select  city_id, name_cn from t_lm_city";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];
        DataRow row1 = myTable.NewRow();
        row1["city_id"] = "";
        row1["name_cn"] = noLimitText;//不限制
        myTable.Rows.InsertAt(row1, 0);
        //myTable.Rows.Add(row1);
        cityid.DataTextField = "name_cn";
        cityid.DataValueField = "city_id";
        cityid.DataSource = myTable;
        cityid.DataBind();
    }



    //====绑定初始值，即默认值==========
    //领用券用户组
    private void BindClientListBox()
    {
        string sql = "select ID, USERGROUP_NAME from T_LM_USERGROUP";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["USERGROUP_NAME"] = noLimitText;
        row1["ID"] = DBNull.Value;
        myTable.Rows.InsertAt(row1, 0);

        LBUserGroup.DataTextField = "USERGROUP_NAME";
        LBUserGroup.DataValueField = "ID";
        LBUserGroup.DataSource = myTable.DefaultView;
        LBUserGroup.DataBind();

    }

    //使用平台,原始用的是usecode
    private void BindPlatFormListBox()
    {
        string sql = "select PLATFORM_CODE, PLATFORM_NAME from T_LM_B_PLATFORM where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["PLATFORM_NAME"] = noLimitText;
        row1["PLATFORM_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);

        LBUseCode.DataTextField = "PLATFORM_NAME";
        LBUseCode.DataValueField = "PLATFORM_CODE";
        LBUseCode.DataSource = myTable.DefaultView;
        LBUseCode.DataBind();

    }
    //绑定销售渠道
    private void BindSaleChannel()
    {
        string sql = "select CHANEL_CODE, CHANEL_NAME from T_LM_B_CHANEL where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];
        DataRow row1 = myTable.NewRow();
        row1["CHANEL_NAME"] = noLimitText;
        row1["CHANEL_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);
        LBSaleChannel.DataTextField = "CHANEL_NAME";
        LBSaleChannel.DataValueField = "CHANEL_CODE";
        LBSaleChannel.DataSource = myTable.DefaultView;
        LBSaleChannel.DataBind();
    }
}