using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data;
using System.Text;

//using HotelVp.Common;
//using HotelVp.CMS.Domain.Resource;
using HotelVp.Common.Utilities;
//using HotelVp.Common.DataAccess;
//using HotelVp.CMS.Domain.DataAccess;
//using HotelVp.Common.Configuration;
using HotelVp.Common.DBUtility;
//using System.Resources;
//using System.IO;


public partial class Ticket_TicketRule : BasePage
{
    //public static Ticket ticketRs = new Ticket();
    //public static GlobalResource gr = new GlobalResource();

    public static string SelectButtonLabel = string.Empty;
    public static string ClearButtonLabel = string.Empty;
    public static string NoLimitText = string.Empty;
    public static string AddRuleSuccess = string.Empty;
    public static string AddRuleFaild = string.Empty;
    public static string RuleNameCanNotEmpty = string.Empty;
    public static string StartDateCanNotEmpty = string.Empty;
    public static string EndDateCanNotEmpty = string.Empty;
    public static string EndDateGreaterThanStartDate = string.Empty;
    public static string PromptOrderAmtMustNum = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        SelectButtonLabel = Resources.MyGlobal.SelectText;
        ClearButtonLabel = Resources.MyGlobal.ClearText;
        NoLimitText = Resources.MyGlobal.NoLimitText;

        AddRuleSuccess = GetLocalResourceObject("AddRuleSuccess").ToString();
        AddRuleFaild = GetLocalResourceObject("AddRuleFaild").ToString();

        RuleNameCanNotEmpty = GetLocalResourceObject("RuleNameCanNotEmpty").ToString();
        StartDateCanNotEmpty = GetLocalResourceObject("StartDateCanNotEmpty").ToString();
        EndDateCanNotEmpty = GetLocalResourceObject("EndDateCanNotEmpty").ToString();
        EndDateGreaterThanStartDate = GetLocalResourceObject("EndDateGreaterThanStartDate").ToString();
        PromptOrderAmtMustNum = GetLocalResourceObject("PromptOrderAmtMustNum").ToString(); 

        if (!IsPostBack)
        {    
            //ticketRs = ResourceHelper.ResXResourceReaderHelper(ticketRs, "Ticket");
            //gr = ResourceHelper.ResXResourceReaderHelper(gr, "GlobalResource");

            BindCityDDL();//绑定城市列表
            BindClientListBox();//绑定用户组
            BindPlatFormListBox();//绑定用户平台
            BindSaleChannel();//绑定销售渠道
            BindGridView(); //绑定已经设置的规则列表

            //增加
            //setMessage();

            LBPriceCode.Items[0].Selected = true;
            LBSaleChannel.Items[0].Selected = true;
        }
    }

    //增加到数据库中
    protected void btnAdd_Click(object sender, EventArgs e)
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
        string strFromDate = this.fromDate.Value;
        string strEndDate = this.endDate.Value;

        string strStartTime = this.txtStartTime.Text;
        string strEndTime = this.txtEndTime.Text;
        string strOrdAmt = this.txtOrdAmt.Text;
        if (strOrdAmt == "")
        {
            strOrdAmt = "0";        
        }
        string strHotelID = this.txthotelid.Value;
        string strCityID = this.cityid.SelectedValue;
        string strRuleDesc = this.txtRuleDesc.Text;
        string strHotelName = this.txtHotelName.Value;

        string useGroup = "";     //使用用户组
        for (int i = 0; i < LBUserGroup.Items.Count; i++)
        {
            if (LBUserGroup.Items[i].Selected == true)
            {
                useGroup = useGroup + LBUserGroup.Items[i].Value + ",";
            }
        }
        useGroup = useGroup.Trim().Trim(',');

        string useCode = "";//使用平台
        for (int i = 0; i < LBUsePlatForm.Items.Count; i++)
        {
            if (LBUsePlatForm.Items[i].Selected == true)
            {
                useCode = useCode + LBUsePlatForm.Items[i].Value + ",";
            }
        }
        useCode = useCode.Trim().Trim(',');

        //销售渠道
        string saleChannel = "";//销售渠道
        for (int i = 0; i < LBSaleChannel.Items.Count; i++)
        {
            if (LBSaleChannel.Items[i].Selected == true)
            {
                saleChannel = saleChannel + LBSaleChannel.Items[i].Value + ",";
            }
        }
        saleChannel = saleChannel.Trim().Trim(',');


        //价格代码
        string priceCode = "";//价格代码
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

        parm[1].Value = strTicketCode;
        parm[2].Value = System.DateTime.Now;
        parm[3].Value = strOrdAmt;
        parm[4].Value = strStartTime;
        parm[5].Value = strEndTime;
        parm[6].Value = strFromDate;
        parm[7].Value = strEndDate;
        parm[8].Value = strHotelID;
        parm[9].Value = strCityID;
        parm[10].Value = "1";
        parm[11].Value = strRuleDesc;
        parm[12].Value = strTicketName;
        parm[13].Value = saleChannel;
        parm[14].Value = useCode;
        parm[15].Value = strHotelName;
        parm[16].Value = useGroup;
        parm[17].Value = priceCode;
        try
        {
            DbHelperOra.ExecuteSql(sql.ToString(), parm);            
            //clear the control value
            clearValue();
            //重新绑定Gridview
            BindGridView();
           // this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + AddRuleSuccess + "');", true);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpen();", true);
            
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + AddRuleFaild + "');", true);
            //ex.ToString();
        }


    }

    private DataSet getDs()
    {
        string sql = "select * from T_LM_TICKET_RULE order by ID desc";
        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;

    }

    private void BindGridView()
    {
        this.gridViewRegion.DataSource = getDs().Tables[0].DefaultView;
        this.gridViewRegion.DataBind();
    }

    //领用券用户组
    private void BindClientListBox()
    {
        //string sql = "select clientcode, clientnm from t_lm_client";
        //DataTable myTable = DbHelperOra.Query(sql).Tables[0];

        //DataRow row1 = myTable.NewRow();
        //row1["clientnm"] = NoLimitText;// "不限制";
        //row1["clientcode"] = "";
        //myTable.Rows.InsertAt(row1, 0);

        //LBClientCode.DataTextField = "clientnm";
        //LBClientCode.DataValueField = "clientcode";
        //LBClientCode.DataSource = myTable.DefaultView;
        //LBClientCode.DataBind();

        string sql = "select ID, USERGROUP_NAME from T_LM_USERGROUP";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];

        DataRow row1 = myTable.NewRow();
        row1["USERGROUP_NAME"] = NoLimitText;
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
        row1["PLATFORM_NAME"] = NoLimitText;
        row1["PLATFORM_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);

        LBUsePlatForm.DataTextField = "PLATFORM_NAME";
        LBUsePlatForm.DataValueField = "PLATFORM_CODE";
        LBUsePlatForm.DataSource = myTable.DefaultView;
        LBUsePlatForm.DataBind();
        LBUsePlatForm.SelectedIndex = 0;
    }

    //绑定城市列表
    private void BindCityDDL()
    {
        string sql = " select  city_id, name_cn from T_LM_B_CITY where STATUS=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];
        DataRow row1 = myTable.NewRow();
        row1["city_id"] = "";
        row1["name_cn"] = NoLimitText;// "不限制";
        myTable.Rows.InsertAt(row1, 0);

        //myTable.Rows.Add(row1);
        cityid.DataTextField = "name_cn";
        cityid.DataValueField = "city_id";
        cityid.DataSource = myTable;
        cityid.DataBind();
    }

    //绑定销售渠道
    private void BindSaleChannel()
    {
        string sql = "select CHANEL_CODE, CHANEL_NAME from T_LM_B_CHANEL where status=1";
        DataTable myTable = DbHelperOra.Query(sql, false).Tables[0];
        DataRow row1 = myTable.NewRow();
        row1["CHANEL_NAME"] = NoLimitText;
        row1["CHANEL_CODE"] = "";
        myTable.Rows.InsertAt(row1, 0);
        LBSaleChannel.DataTextField = "CHANEL_NAME";
        LBSaleChannel.DataValueField = "CHANEL_CODE";
        LBSaleChannel.DataSource = myTable.DefaultView;
        LBSaleChannel.DataBind();
        LBSaleChannel.SelectedIndex = 0;
    }

    //点击删除按钮。
    protected void gridViewRegion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strID = gridViewRegion.DataKeys[e.RowIndex].Value.ToString();
        string sql = "delete from T_LM_TICKET_RULE where ID=:ID";
        OracleParameter parm = new OracleParameter("ID", OracleType.Int32);
        parm.Value = strID;
        try
        {
            DbHelperOra.ExecuteSql(sql, parm);

            //再绑定数据
            BindGridView();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    //绑定的时候增加提示属性
    protected void gridViewRegion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //     if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
        //    {
        //        ((LinkButton)e.Row.Cells[10].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除代码为：" + e.Row.Cells[1].Text + "吗?')");
        //    }
        //} 
    }

    //行事件
    protected void gridViewRegion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = 0;
        if (e.CommandName == "SingleClick")
        {
            //获取当前行的索引
            LinkButton LB = (LinkButton)e.CommandSource;
            rowIndex = int.Parse(e.CommandArgument.ToString());
            this.Page.Response.Write("<script>window.open('UpdateTicketRule.aspx?ID=" + rowIndex + "');</script>");

        }

    }

    protected void gridViewRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewRegion.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    //清除控件中的数据
    private void clearValue()
    {
        this.txtRuleName.Text ="";//规则名称       
        this.fromDate.Value = "";
        this.endDate.Value = "";

        this.txtStartTime.Text = "";
        this.txtEndTime.Text = "";
        this.txtOrdAmt.Text = "";


        this.txthotelid.Value = "";
        this.cityid.SelectedValue = "";
        this.txtRuleDesc.Text = "";
        this.txtHotelName.Value = "";

        LBUserGroup.ClearSelection();
        LBUsePlatForm.ClearSelection();
        LBSaleChannel.ClearSelection();

        LBPriceCode.ClearSelection();
        LBPriceCode.Items[0].Selected = true;

    }
}