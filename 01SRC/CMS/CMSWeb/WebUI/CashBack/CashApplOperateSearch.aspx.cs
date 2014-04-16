using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class WebUI_CashBack_CashApplOperateSearch : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["menu"]) && chkParm(Request.QueryString["menu"].ToString()))
            {
                hidMenuSpan.Value = Request.QueryString["menu"].ToString();
            }
            else
            {
                hidMenuSpan.Value = "-1";
            }

            string getFlag = Request.QueryString["sendFlag"];
            if (!string.IsNullOrEmpty(getFlag))
            {
                try
                {

                    this.txtPhoneNumber.Text = Session["phoneNumber"].ToString();

                    if (Session["startCreateDate"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["startCreateDate"].ToString()))
                        {
                            string tempCreateStartDate = Session["startCreateDate"].ToString();
                            this.dtStartCreateDate.Value = tempCreateStartDate.Substring(0, tempCreateStartDate.IndexOf(" "));
                        }
                        else
                        {
                            this.dtStartCreateDate.Value = "";
                        }
                    }
                    else
                    {
                        Session["startCreateDate"] = "";
                    }

                    if (Session["endCreateDate"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["endCreateDate"].ToString()))
                        {
                            string tempEndStartDate = Session["endCreateDate"].ToString();
                            this.dtEndCreateDate.Value = tempEndStartDate.Substring(0, tempEndStartDate.IndexOf(" "));
                        }
                        else
                        {
                            this.dtEndCreateDate.Value = "";
                        }
                    }
                    else
                    {
                        Session["endCreateDate"] = "";
                    }

                    if (Session["startProcessDate"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["startProcessDate"].ToString()))
                        {
                            string tempStartProcessDate = Session["startProcessDate"].ToString();
                            this.dtStartProcessDate.Value = tempStartProcessDate.Substring(0, tempStartProcessDate.IndexOf(" "));
                        }
                        else
                        {
                            this.dtStartProcessDate.Value = "";
                        }
                    }
                    else
                    {
                        Session["startProcessDate"] = "";
                    }


                    //this.dtStartProcessDate.Value = Session["startProcessDate"].ToString();
                    if (Session["endProcessDate"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["endProcessDate"].ToString()))
                        {
                            string tempEndProcessDate = Session["endProcessDate"].ToString();
                            this.dtEndProcessDate.Value = tempEndProcessDate.Substring(0, tempEndProcessDate.IndexOf(" "));
                        }
                        else
                        {
                            this.dtEndProcessDate.Value = "";
                        }
                    }
                    else
                    {
                        Session["endProcessDate"] = "";
                    }

                    //this.dtEndProcessDate.Value = Session["endProcessDate"].ToString();

                    if (Session["applicateMode"] != null)
                    {
                        if (string.IsNullOrEmpty(Session["applicateMode"].ToString()))
                        {
                            this.ddlAppMode.SelectedIndex = 0;
                        }
                        else
                        {
                            this.ddlAppMode.SelectedIndex = Convert.ToInt32(Session["applicateMode"]);
                        }
                    }
                    else
                    {
                        Session["applicateMode"] = "";
                    }

                    if (Session["processStatus"] != null)
                    {
                        if (string.IsNullOrEmpty(Session["processStatus"].ToString()))
                        {
                            this.ddlProcessStatus.SelectedIndex = 0;
                        }
                        else
                        {
                            this.ddlProcessStatus.SelectedIndex = Convert.ToInt32(Session["processStatus"]);
                        }
                    }
                    else
                    {
                        Session["processStatus"] = "";
                    }

                    if (Session["SourceChannel"] != null)
                    {
                        if (string.IsNullOrEmpty(Session["SourceChannel"].ToString()))
                        {
                            this.ddlCashCanType.SelectedIndex = 0;
                        }
                        else
                        {
                            this.ddlCashCanType.SelectedIndex = Convert.ToInt32(Session["SourceChannel"]);
                        }
                    }
                    else
                    {
                        Session["SourceChannel"] = "";
                    }

                    if (Session["cashID"] != null)
                    {
                        if (string.IsNullOrEmpty(Session["cashID"].ToString()))
                        {
                            txtCashID.Text = "";
                            Session["cashID"] = "";
                            ViewState["cashID"] = "";
                        }
                        else
                        {
                            ViewState["cashID"] = Session["cashID"];
                            txtCashID.Text = Session["cashID"].ToString();
                        }
                    }
                    else
                    {
                        Session["cashID"] = "";
                        ViewState["cashID"] = "";
                    }

                    ViewState["phoneNumber"] = Session["phoneNumber"];
                    ViewState["startCreateDate"] = Session["startCreateDate"];
                    ViewState["endCreateDate"] = Session["endCreateDate"];
                    ViewState["startProcessDate"] = Session["startProcessDate"];
                    ViewState["endProcessDate"] = Session["endProcessDate"];
                    ViewState["applicateMode"] = Session["applicateMode"];
                    ViewState["processStatus"] = Session["processStatus"];
                    ViewState["CashCanType"] = Session["CashCanType"];
                    ViewState["BackType"] = Session["BackType"];
                    ViewState["SourceChannel"] = Session["SourceChannel"];
                    ViewState["OpeType"] = Session["OpeType"];
                    BindLToCash();
                }
                catch
                {
                }
            }
            else
            {
                SetEmptyDataTable();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["UID"]))
            {
                string UserID = (Request.QueryString["UID"] != null && !String.IsNullOrEmpty(Request.QueryString["UID"])) ? Request.QueryString["UID"].ToString() : "";
                ViewState["phoneNumber"] = UserID;
                ViewState["startCreateDate"] = "";
                ViewState["endCreateDate"] = "";
                ViewState["startProcessDate"] = "";
                ViewState["endProcessDate"] = "";
                ViewState["applicateMode"] = "";
                ViewState["processStatus"] = "";
                ViewState["SourceChannel"] = "";
                ViewState["OpeType"] = "";
                ViewState["BackType"] = "1";
                ViewState["cashID"] = "";

                //赋值给session为从子页面调回的时候用
                Session["phoneNumber"] = ViewState["phoneNumber"];
                Session["startCreateDate"] = ViewState["startCreateDate"];
                Session["endCreateDate"] = ViewState["endCreateDate"];
                Session["startProcessDate"] = ViewState["startProcessDate"];
                Session["endProcessDate"] = ViewState["endProcessDate"];
                Session["applicateMode"] = ViewState["applicateMode"];
                Session["processStatus"] = ViewState["processStatus"];
                Session["SourceChannel"] = ViewState["SourceChannel"];
                Session["OpeType"] = ViewState["OpeType"];
                Session["BackType"] = ViewState["BackType"];
                Session["cashID"] = ViewState["cashID"];

                BindLToCash();
            }

            BindCDropDownListData();
        }
    }

    private void BindCDropDownListData()
    {
        DataSet dsCDropDown = CommonBP.GetConfigList(GetLocalResourceObject("CDropDown").ToString());
        if (dsCDropDown.Tables.Count > 0)
        {
            dsCDropDown.Tables[0].Columns["Value"].ColumnName = "CDropDownText";
            cddpUserRemark.DataSource = dsCDropDown.Tables[0].DefaultView;
            cddpUserRemark.DataTextField = "CDropDownText";
            cddpUserRemark.DataBind();
        }
    }

    private bool chkParm(string parm)
    {
        try
        {
            int.Parse(parm);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        string phoneNumber = this.txtPhoneNumber.Text;
        string startCreateDate = this.dtStartCreateDate.Value;
        string endCreateDate = this.dtEndCreateDate.Value;
        string startProcessDate = this.dtStartProcessDate.Value;
        string endProcessDate = this.dtEndProcessDate.Value;
        string applicateMode = this.ddlAppMode.SelectedValue;
        string processStatus = this.ddlProcessStatus.SelectedValue;
        string sourceChannel = this.ddlCashCanType.SelectedValue;
        string opeType = this.ddlOpeType.SelectedValue;


        string strCashID = txtCashID.Text.Trim().Replace('，', ',');
        string strCashList = "";
        if (strCashID.IndexOf(',') >= 0)
        {
            string[] telList = strCashID.Split(',');
            foreach (string strTemp in telList)
            {
                strCashList = (String.IsNullOrEmpty(strTemp)) ? strCashList : strCashList + strTemp + ",";
            }
        }
        else if (strCashID.Length > 0)
        {
            strCashList = strCashID + ",";
        }

        string cashID = strCashList;

        ViewState["phoneNumber"] = phoneNumber;
        if (string.IsNullOrEmpty(startCreateDate))
        {
            ViewState["startCreateDate"] = startCreateDate;
        }
        else
        {
            ViewState["startCreateDate"] = startCreateDate;// +" 00:00:00";
        }

        if (string.IsNullOrEmpty(endCreateDate))
        {
            ViewState["endCreateDate"] = endCreateDate;
        }
        else
        {
            ViewState["endCreateDate"] = endCreateDate;// +" 23:59:59";
        }


        if (string.IsNullOrEmpty(startProcessDate))
        {
            ViewState["startProcessDate"] = startProcessDate;
        }
        else
        {
            ViewState["startProcessDate"] = startProcessDate;// +" 00:00:00";
        }

        if (string.IsNullOrEmpty(endProcessDate))
        {
            ViewState["endProcessDate"] = endProcessDate;
        }
        else
        {
            ViewState["endProcessDate"] = endProcessDate;// +" 23:59:59"; 
        }

        ViewState["applicateMode"] = applicateMode;
        ViewState["processStatus"] = processStatus;
        //ViewState["CashCanType"] = cashCanType;

        //if (cashCanType == "1") { ViewState["SOURCECHANNEL"] = ""; }//用户提现
        //else if (cashCanType == "2") { ViewState["SOURCECHANNEL"] = "CMS"; } //cms
        //else { ViewState["SOURCECHANNEL"] = "CMS"; }//无限制

        ViewState["SourceChannel"] = sourceChannel;
        ViewState["OpeType"] = opeType;


        ViewState["BackType"] = "";

        if (string.IsNullOrEmpty(cashID))
        {
            ViewState["cashID"] = cashID;
        }
        else
        {
            ViewState["cashID"] = cashID;// +" 23:59:59"; 
        }

        //赋值给session为从子页面调回的时候用
        Session["phoneNumber"] = ViewState["phoneNumber"];
        Session["startCreateDate"] = ViewState["startCreateDate"];
        Session["endCreateDate"] = ViewState["endCreateDate"];
        Session["startProcessDate"] = ViewState["startProcessDate"];
        Session["endProcessDate"] = ViewState["endProcessDate"];
        Session["applicateMode"] = ViewState["applicateMode"];
        Session["processStatus"] = ViewState["processStatus"];
        Session["BackType"] = ViewState["BackType"];
        //Session["CashCanType"] = ViewState["CashCanType"];
        Session["cashID"] = ViewState["cashID"];
        Session["SourceChannel"] = ViewState["SourceChannel"];
        Session["OpeType"] = ViewState["OpeType"];

        AspNetPager1.CurrentPageIndex = 1;
        BindLToCash();

        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindLToCash();
    }

    private void SetEmptyDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn BACK_OWNER_dc = new DataColumn("BACK_OWNER");
        DataColumn SN_dc = new DataColumn("SN");
        DataColumn User_ID_dc = new DataColumn("USER_ID");
        DataColumn Pick_Cash_Amount_dc = new DataColumn("PICK_CASH_AMOUNT");
        DataColumn Cash_Way_dc = new DataColumn("CASH_WAY");
        DataColumn CASHCANTYPE_dc = new DataColumn("CASHCANTYPE");
        DataColumn SOURCECHANNEL_dc = new DataColumn("SOURCECHANNEL");
        DataColumn Applicate_Time_dc = new DataColumn("APPLICATE_TIME");
        DataColumn PROCESS_STATUS_dc = new DataColumn("PROCESS_STATUS");
        DataColumn ISPUSH_dc = new DataColumn("IS_PUSH");
        DataColumn REALAMOUNT_dc = new DataColumn("REALAMOUNT");
        DataColumn CHARGE_dc = new DataColumn("CHARGE");
        DataColumn Process_UserID_dc = new DataColumn("PROCESS_USERID");
        DataColumn STATUS_dc = new DataColumn("PROCESS_TIME");
        DataColumn OPE_TYPE = new DataColumn("OPE_TYPE");

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(BACK_OWNER_dc);
        dt.Columns.Add(SN_dc);
        dt.Columns.Add(User_ID_dc);
        dt.Columns.Add(Pick_Cash_Amount_dc);
        dt.Columns.Add(Cash_Way_dc);
        dt.Columns.Add(CASHCANTYPE_dc);
        dt.Columns.Add(SOURCECHANNEL_dc);
        dt.Columns.Add(Applicate_Time_dc);
        dt.Columns.Add(PROCESS_STATUS_dc);
        dt.Columns.Add(ISPUSH_dc);
        dt.Columns.Add(REALAMOUNT_dc);
        dt.Columns.Add(CHARGE_dc);
        dt.Columns.Add(Process_UserID_dc);
        dt.Columns.Add(STATUS_dc);
        dt.Columns.Add(OPE_TYPE);
        GridviewControl.GridViewDataBind(this.gridViewCash, dt);
    }

    public DataSet ExportDataList()
    {
        DataSet getDataTable = ExportDataLog();

        for (int i = 0; i < getDataTable.Tables[0].Rows.Count; i++)
        {
            string cashway = getDataTable.Tables[0].Rows[i]["CASH_WAY"].ToString().Trim();//提现用--申请提现方式：1-现金返还；2-支付宝返还；3-手机充值
            switch (cashway)
            {
                case "1":
                    getDataTable.Tables[0].Rows[i]["CASH_WAY"] = "现金返还";
                    break;
                case "2":
                    getDataTable.Tables[0].Rows[i]["CASH_WAY"] = "支付宝返还";
                    break;
                case "3":
                    getDataTable.Tables[0].Rows[i]["CASH_WAY"] = "手机充值";
                    break;
                default:
                    getDataTable.Tables[0].Rows[i]["CASH_WAY"] = "没有选择";
                    break;
            }

            string cashcantype = getDataTable.Tables[0].Rows[i]["CASHCANTYPE"].ToString().Trim();//提现用--申请提现方式：0-返现；1-用户提现；2-CMS提现
            switch (cashcantype)
            {
                case "0":
                    getDataTable.Tables[0].Rows[i]["CASHCANTYPE"] = "返现";
                    break;
                case "1":
                    getDataTable.Tables[0].Rows[i]["CASHCANTYPE"] = "提现";
                    break;
                //case "2":
                //    dr["CASHCANTYPE"] = "CMS提现";
                //    break;
                default:
                    getDataTable.Tables[0].Rows[i]["CASHCANTYPE"] = "没有选择";
                    break;
            }

            string casourceshcannel = getDataTable.Tables[0].Rows[i]["CASOURCESHCANNEL"].ToString().Trim();//提现用--申请提现方式：0-返现；""-用户提现；"CMS"-CMS提现
            switch (casourceshcannel)
            {
                case "":
                    getDataTable.Tables[0].Rows[i]["CASOURCESHCANNEL"] = "用户提现";
                    break;
                case "CMS":
                    getDataTable.Tables[0].Rows[i]["CASOURCESHCANNEL"] = "CMS提现";
                    break;
                //case "2":
                //    dr["CASHCANTYPE"] = "";
                //    break;
                default:

                    break;
            }

            string processStatus = getDataTable.Tables[0].Rows[i]["PROCESS_STATUS"].ToString().Trim();//0-已提交；1-已审核；2-已成功；3-已失败
            switch (processStatus)
            {
                case "0":
                    getDataTable.Tables[0].Rows[i]["PROCESS_STATUS"] = (String.IsNullOrEmpty(getDataTable.Tables[0].Rows[i]["TRY_COUNT"].ToString()) || int.Parse(getDataTable.Tables[0].Rows[i]["TRY_COUNT"].ToString()) == 0) ? "已提交-1次" : "已提交-多次";
                    getDataTable.Tables[0].Rows[i]["REALAMOUNT"] = DBNull.Value;
                    getDataTable.Tables[0].Rows[i]["CHARGE"] = DBNull.Value;
                    break;
                case "1":
                    getDataTable.Tables[0].Rows[i]["PROCESS_STATUS"] = "已审核";
                    //getDataTable.Tables[0].Rows[i]["REALAMOUNT"] = "";
                    //getDataTable.Tables[0].Rows[i]["CHARGE"] = "";
                    break;
                case "3":
                    getDataTable.Tables[0].Rows[i]["PROCESS_STATUS"] = "已成功";
                    break;
                case "2":
                    getDataTable.Tables[0].Rows[i]["PROCESS_STATUS"] = "已失败";
                    //getDataTable.Tables[0].Rows[i]["REALAMOUNT"] = "";
                    //getDataTable.Tables[0].Rows[i]["CHARGE"] = "";
                    break;
                case "4":
                    getDataTable.Tables[0].Rows[i]["PROCESS_STATUS"] = "已操作";
                    //getDataTable.Tables[0].Rows[i]["REALAMOUNT"] = "";
                    //getDataTable.Tables[0].Rows[i]["CHARGE"] = "";
                    break;
                default:
                    getDataTable.Tables[0].Rows[i]["PROCESS_STATUS"] = "没有选择";
                    //getDataTable.Tables[0].Rows[i]["REALAMOUNT"] = "";
                    //getDataTable.Tables[0].Rows[i]["CHARGE"] = "";
                    break;
            }
        }
        return getDataTable;

    }

    //public DataTable exportDataTable()
    //{
    //    DataTable getDataTable = CountLmCashLog().Tables[0];
    //    DataTable dt = new DataTable();
    //    DataColumn ID_dc = new DataColumn("ID");
    //    DataColumn SN_dc = new DataColumn("SN");
    //    DataColumn User_ID_dc = new DataColumn("USER_ID");
    //    DataColumn Pick_Cash_Amount_dc = new DataColumn("PICK_CASH_AMOUNT");
    //    DataColumn Cash_Way_dc = new DataColumn("CASH_WAY");
    //    DataColumn CASHCANTYPE_dc = new DataColumn("CASHCANTYPE");
    //    DataColumn SOURCECHANNEL_dc = new DataColumn("SOURCECHANNEL");
    //    DataColumn Applicate_Time_dc = new DataColumn("APPLICATE_TIME");
    //    DataColumn PROCESS_STATUS_dc = new DataColumn("PROCESS_STATUS");
    //    DataColumn Process_UserID_dc = new DataColumn("PROCESS_USERID");
    //    DataColumn STATUS_dc = new DataColumn("PROCESS_TIME");

    //    dt.Columns.Add(ID_dc);
    //    dt.Columns.Add(SN_dc);
    //    dt.Columns.Add(User_ID_dc);
    //    dt.Columns.Add(Pick_Cash_Amount_dc);
    //    dt.Columns.Add(Cash_Way_dc);
    //    dt.Columns.Add(CASHCANTYPE_dc);
    //    dt.Columns.Add(SOURCECHANNEL_dc);
    //    dt.Columns.Add(Applicate_Time_dc);
    //    dt.Columns.Add(PROCESS_STATUS_dc);
    //    dt.Columns.Add(Process_UserID_dc);
    //    dt.Columns.Add(STATUS_dc); 

    //    for (int i = 0; i < getDataTable.Rows.Count; i++)
    //    {
    //        DataRow dr = dt.NewRow();
    //        dr["ID"] = getDataTable.Rows[i]["ID"];
    //        dr["SN"] = getDataTable.Rows[i]["SN"];
    //        dr["USER_ID"] = getDataTable.Rows[i]["USER_ID"];
    //        dr["PICK_CASH_AMOUNT"] = getDataTable.Rows[i]["PICK_CASH_AMOUNT"];

    //        string cashway = getDataTable.Rows[i]["CASH_WAY"].ToString().Trim();//提现用--申请提现方式：1-现金返还；2-支付宝返还；3-手机充值
    //        switch (cashway)
    //        {
    //            case "1":
    //                dr["CASH_WAY"] = "现金返还";
    //                break;
    //            case "2":
    //                dr["CASH_WAY"] = "支付宝返还";
    //                break;
    //            case "3":
    //                dr["CASH_WAY"] = "手机充值";
    //                break;
    //            default:
    //                dr["CASH_WAY"] = "没有选择";
    //                break;
    //        }

    //        string cashcantype = getDataTable.Rows[i]["CASHCANTYPE"].ToString().Trim();//提现用--申请提现方式：0-返现；1-用户提现；2-CMS提现
    //        switch (cashcantype)
    //        {
    //            case "0":
    //                dr["CASHCANTYPE"] = "返现";
    //                break;
    //            case "1":
    //                dr["CASHCANTYPE"] = "提现";
    //                break;
    //            //case "2":
    //            //    dr["CASHCANTYPE"] = "CMS提现";
    //            //    break;
    //            default:
    //                //dr["CASHCANTYPE"] = "没有选择";
    //                break;
    //        }

    //        string casourceshcannel = getDataTable.Rows[i]["CASOURCESHCANNEL"].ToString().Trim();//提现用--申请提现方式：0-返现；""-用户提现；"CMS"-CMS提现
    //        switch (casourceshcannel)
    //        {
    //            case "":
    //                dr["SOURCECHANNEL"] = "用户提现";
    //                break;
    //            case "CMS":
    //                dr["SOURCECHANNEL"] = "CMS提现";
    //                break;
    //            //case "2":
    //            //    dr["CASHCANTYPE"] = "";
    //            //    break;
    //            default:

    //                break;
    //        }

    //        dr["APPLICATE_TIME"] = getDataTable.Rows[i]["APPLICATE_TIME"];
    //        string processStatus = getDataTable.Rows[i]["PROCESS_STATUS"].ToString().Trim();//0-已提交；1-已审核；2-已成功；3-已失败
    //        switch (processStatus)
    //        {
    //            case "0":
    //                dr["PROCESS_STATUS"] = "已提交";
    //                break;
    //            case "1":
    //                dr["PROCESS_STATUS"] = "已审核";
    //                break;
    //            case "3":
    //                dr["PROCESS_STATUS"] = "已成功";
    //                break;
    //            case "2":
    //                dr["PROCESS_STATUS"] = "已失败";
    //                break;
    //            case "4":
    //                dr["PROCESS_STATUS"] = "已操作";
    //                break;
    //            default:
    //                dr["PROCESS_STATUS"] = "没有选择";
    //                break;            
    //        }

    //        dr["PROCESS_USERID"] = getDataTable.Rows[i]["PROCESS_USERID"];
    //        dr["PROCESS_TIME"] = getDataTable.Rows[i]["PROCESS_TIME"];           
    //        dt.Rows.Add(dr);
    //    }
    //    return dt;

    //}

    public DataTable createDataTable()
    {
        DataTable getDataTable = getCashData().Tables[0];
        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn BACK_OWNER_dc = new DataColumn("BACK_OWNER");
        DataColumn SN_dc = new DataColumn("SN");
        DataColumn User_ID_dc = new DataColumn("USER_ID");
        DataColumn Pick_Cash_Amount_dc = new DataColumn("PICK_CASH_AMOUNT");
        DataColumn Cash_Way_dc = new DataColumn("CASH_WAY");
        DataColumn CASHCANTYPE_dc = new DataColumn("CASHCANTYPE");
        DataColumn SOURCECHANNEL_dc = new DataColumn("SOURCECHANNEL");
        DataColumn Applicate_Time_dc = new DataColumn("APPLICATE_TIME");
        DataColumn PROCESS_STATUS_dc = new DataColumn("PROCESS_STATUS");
        DataColumn ISPUSH_dc = new DataColumn("IS_PUSH");
        DataColumn REALAMOUNT_dc = new DataColumn("REALAMOUNT");
        DataColumn CHARGE_dc = new DataColumn("CHARGE");
        DataColumn Process_UserID_dc = new DataColumn("PROCESS_USERID");
        DataColumn STATUS_dc = new DataColumn("PROCESS_TIME");
        DataColumn OPE_TYPE = new DataColumn("OPE_TYPE");

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(BACK_OWNER_dc);
        dt.Columns.Add(SN_dc);
        dt.Columns.Add(User_ID_dc);
        dt.Columns.Add(Pick_Cash_Amount_dc);
        dt.Columns.Add(Cash_Way_dc);
        dt.Columns.Add(CASHCANTYPE_dc);
        dt.Columns.Add(SOURCECHANNEL_dc);
        dt.Columns.Add(Applicate_Time_dc);
        dt.Columns.Add(PROCESS_STATUS_dc);
        dt.Columns.Add(ISPUSH_dc);
        dt.Columns.Add(REALAMOUNT_dc);
        dt.Columns.Add(CHARGE_dc);
        dt.Columns.Add(Process_UserID_dc);
        dt.Columns.Add(STATUS_dc);
        dt.Columns.Add(OPE_TYPE);

        for (int i = 0; i < getDataTable.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = getDataTable.Rows[i]["ID"];
            dr["BACK_OWNER"] = getDataTable.Rows[i]["BACK_OWNER"];
            dr["SN"] = getDataTable.Rows[i]["SN"];
            dr["USER_ID"] = getDataTable.Rows[i]["USER_ID"];
            dr["PICK_CASH_AMOUNT"] = getDataTable.Rows[i]["PICK_CASH_AMOUNT"];
            dr["REALAMOUNT"] = getDataTable.Rows[i]["REALAMOUNT"].ToString();
            dr["CHARGE"] = getDataTable.Rows[i]["CHARGE"].ToString();

            string cashway = getDataTable.Rows[i]["CASH_WAY"].ToString().Trim();//提现用--申请提现方式：1-现金返还；2-支付宝返还；3-手机充值
            switch (cashway)
            {
                case "1":
                    dr["CASH_WAY"] = "现金返还";
                    break;
                case "2":
                    dr["CASH_WAY"] = "支付宝返还";
                    break;
                case "3":
                    dr["CASH_WAY"] = "手机充值";
                    break;
                default:
                    dr["CASH_WAY"] = "没有选择";
                    break;
            }

            string cashcantype = getDataTable.Rows[i]["CASHCANTYPE"].ToString().Trim();//提现用--申请提现方式：0-返现；1-用户提现；2-CMS提现
            switch (cashcantype)
            {
                case "0":
                    dr["CASHCANTYPE"] = "返现";
                    break;
                case "1":
                    dr["CASHCANTYPE"] = "提现";
                    break;
                //case "2":
                //    dr["CASHCANTYPE"] = "CMS提现";
                //    break;
                //default:
                //    dr["CASHCANTYPE"] = "没有选择";
                //    break;
            }

            string casourceshcannel = getDataTable.Rows[i]["CASOURCESHCANNEL"].ToString().Trim();//提现用--申请提现方式：0-返现；""-用户提现；"cms"-CMS提现
            switch (casourceshcannel)
            {
                case "":
                    dr["SOURCECHANNEL"] = "用户提现";
                    break;
                case "CMS":
                    dr["SOURCECHANNEL"] = "CMS提现";
                    break;
                default:
                    break;
            }

            dr["APPLICATE_TIME"] = getDataTable.Rows[i]["APPLICATE_TIME"];
            string processStatus = getDataTable.Rows[i]["PROCESS_STATUS"].ToString().Trim();//0-已提交；1-已审核；2-已成功；3-已失败
            switch (processStatus)
            {
                case "0":
                    //(String.IsNullOrEmpty(getDataTable.Tables[0].Rows[i]["TRY_COUNT"].ToString()) || int.Parse(getDataTable.Tables[0].Rows[i]["TRY_COUNT"].ToString()) == 0) ? "已提交-1次" : "已提交-多次";
                    dr["PROCESS_STATUS"] = (String.IsNullOrEmpty(getDataTable.Rows[i]["TRY_COUNT"].ToString()) || int.Parse(getDataTable.Rows[i]["TRY_COUNT"].ToString()) == 0) ? "已提交-1次" : "已提交-多次";
                    dr["REALAMOUNT"] = DBNull.Value;
                    dr["CHARGE"] = DBNull.Value;
                    break;
                case "1":
                    dr["PROCESS_STATUS"] = "已审核";
                    //dr["REALAMOUNT"] = "";
                    //dr["CHARGE"] = "";
                    break;
                case "3":
                    dr["PROCESS_STATUS"] = "已成功";
                    break;
                case "2":
                    dr["PROCESS_STATUS"] = "已失败";
                    //dr["REALAMOUNT"] = "";
                    //dr["CHARGE"] = "";
                    break;
                case "4":
                    dr["PROCESS_STATUS"] = "已操作";
                    //dr["REALAMOUNT"] = "";
                    //dr["CHARGE"] = "";
                    break;
                default:
                    dr["PROCESS_STATUS"] = "没有选择";
                    //dr["REALAMOUNT"] = "";
                    //dr["CHARGE"] = "";
                    break;
            }

            dr["OPE_TYPE"] = getDataTable.Rows[i]["OPE_TYPE"].ToString().Trim();//是否自动提现

            dr["PROCESS_USERID"] = getDataTable.Rows[i]["PROCESS_USERID"];
            dr["PROCESS_TIME"] = getDataTable.Rows[i]["PROCESS_TIME"];
            dr["IS_PUSH"] = getDataTable.Rows[i]["IS_PUSH"].ToString();

            dt.Rows.Add(dr);
        }
        return dt;

    }

    private void BindLToCash()
    {
        DataTable dt = createDataTable();
        GridviewControl.GridViewDataBind(this.gridViewCash, dt);
        gridViewCash.DataKeyNames = new string[] { "SN" };//主键

        AspNetPager1.PageSize = gridViewCash.PageSize;
        //DataSet dsResult = CountLmCashLog();
        AspNetPager1.RecordCount = CountLmCashLog(); //(dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0) ? dsResult.Tables[0].Rows.Count : 0;
    }

    private DataSet ExportDataLog()
    {
        string sql = string.Empty;
        //sql = sql + "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,TO_CHAR(APPLY_TYPE) AS CASH_WAY,TO_CHAR(STATUS) AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME,";
        //sql = sql + "TO_CHAR(TYPE) AS CASHCANTYPE,TO_CHAR(source_channel) AS CASOURCESHCANNEL , ORDER_NUM,BACK_TYPE,BACK_TICKET_USER_CODE,BANK_NAME,BANK_BRANCH,BANK_CARD_NUMBER,BANK_CARD_OWNER,ALIPAY_ACCOUNT,RECHARGE_PHONE_NUMBER,REMARK,BACK_TICKET_AMOUNT from t_lm_cash where TYPE=1 and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber)) ";
        //sql = sql + "and ((:startCreateDate IS NULL) OR (CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ";
        //sql = sql + "((:endCreateDate IS NULL) OR (CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and ";
        //sql = sql + "((:startProcessDate IS NULL) OR (UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ";
        //sql = sql + "((:endProcessDate IS NULL) OR (UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ";
        //sql = sql + "((:applicateMode is NULL) or (APPLY_TYPE=:applicateMode)) and ((:processStatus is NULL) or (STATUS=:processStatus)) AND ";
        //sql = sql + "((:BackType is NULL) or (:BackType = '1' AND STATUS in (0,1))) and (";
        //sql = sql + " (:cashID IS NULL OR (ID in (SELECT REGEXP_SUBSTR (:cashID, '[^,]+', 1,rownum) FROM DUAL CONNECT BY ROWNUM <=LENGTH (:cashID) - LENGTH (REPLACE (:cashID, ',', '')))))";

        //if (ViewState["SourceChannel"].ToString() == "1")
        //{//用户提现
        //    sql = sql + " and (SOURCE_CHANNEL IS NULL))  order by CREATE_TIME desc  ";
        //}
        //else if (ViewState["SourceChannel"].ToString() == "2")
        //{//cms
        //    sql = sql + " AND (SOURCE_CHANNEL IS NOT NULL AND SOURCE_CHANNEL='CMS')) order by CREATE_TIME desc  ";
        //}
        //else {//无限制
        //    sql = sql + " ) order by CREATE_TIME desc  ";
        //}

        sql = "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,TO_CHAR(APPLY_TYPE) AS CASH_WAY,TO_CHAR(STATUS) AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME,TO_CHAR(TYPE) AS CASHCANTYPE,TO_CHAR(source_channel) AS CASOURCESHCANNEL,  BANK_CARD_OWNER, case when APPLY_TYPE=1 then BANK_CARD_OWNER else GUEST_NAMES end AS BACK_OWNER,ORDER_NUM,BACK_TYPE,BACK_TICKET_USER_CODE,BANK_NAME,BANK_BRANCH,BANK_CARD_NUMBER,ALIPAY_ACCOUNT,RECHARGE_PHONE_NUMBER,REMARK,BACK_TICKET_AMOUNT, case when length(PROCESS_REMARK) > 8 then substr(PROCESS_REMARK, 0, 8) else PROCESS_REMARK end AS IS_PUSH,REALAMOUNT,CHARGE, TRY_COUNT ";
        sql = sql + " from ( select distinct tlc.ID,tlc.SN,tlc.USER_ID,tlc.AMOUNT,tlc.PROCESS_USERID,tlc.APPLY_TYPE,tlc.STATUS,tlc.CREATE_TIME,tlc.UPDATE_TIME ,tlc.TYPE,tlc.source_channel,tlc.BANK_CARD_OWNER,tlo.GUEST_NAMES, tlc.ORDER_NUM,tlc.BACK_TYPE,tlc.BACK_TICKET_USER_CODE,tlc.BANK_NAME,tlc.BANK_BRANCH,tlc.BANK_CARD_NUMBER,tlc.ALIPAY_ACCOUNT,tlc.RECHARGE_PHONE_NUMBER,tlc.REMARK,tlc.BACK_TICKET_AMOUNT,tlc.IS_PUSH,tlp.real_amount AS REALAMOUNT,tlp.charge AS CHARGE, tlc.TRY_COUNT,tlc.PROCESS_REMARK  from t_lm_cash tlc left join t_lm_pay tlp on tlp.sn=tlc.sn left join (select lo.login_mobile,lo.GUEST_NAMES from (select lto.login_mobile,max(lto.create_time) MCreateTime from t_lm_order lto inner join t_lm_cash tc on lto.login_mobile = tc.USER_ID group by lto.login_mobile) tm inner join t_lm_order lo on tm.login_mobile = lo.login_mobile and tm.MCreateTime = lo.create_time) tlo on tlo.login_mobile=tlc.user_id ";
        sql = sql + " where tlc.TYPE=1 and ((:phoneNumber is NULL) or (tlc.USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (tlc.CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (tlc.CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and  ((:startProcessDate IS NULL) OR (tlc.UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and   ((:endProcessDate IS NULL) OR (tlc.UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and    ((:applicateMode is NULL) or (tlc.APPLY_TYPE=:applicateMode)) and     ((:processStatus is NULL) or (:processStatus='5' AND tlc.STATUS='0' and (tlc.TRY_COUNT IS NULL OR tlc.TRY_COUNT=0)) OR (:processStatus='6' AND tlc.STATUS='0' and (tlc.TRY_COUNT IS NOT NULL AND tlc.TRY_COUNT > 0)) OR (tlc.STATUS=:processStatus)) AND     ((:BackType is NULL) or (:BackType = '1' AND tlc.STATUS in (0,1))) and (:cashID IS NULL OR (tlc.ID in (SELECT REGEXP_SUBSTR (:cashID, '[^,]+', 1,rownum) FROM DUAL CONNECT BY ROWNUM <=LENGTH (:cashID) - LENGTH (REPLACE (:cashID, ',', '')))))";

        if (!string.IsNullOrEmpty(ViewState["OpeType"].ToString()))
        {
            if (ViewState["OpeType"].ToString() == "1")//自动
            {
                sql = sql + " and tlc.ope_type='1'";
            }
            else//手动
            {
                sql = sql + " and tlc.ope_type <> '1'";
            }
        }

        if (ViewState["SourceChannel"].ToString() == "1")
        {
            sql = sql + " and (tlc.SOURCE_CHANNEL IS NULL)) order by CREATE_TIME desc  ";
        }
        else if (ViewState["SourceChannel"].ToString() == "2")
        {
            sql = sql + " AND (tlc.SOURCE_CHANNEL IS NOT NULL AND tlc.SOURCE_CHANNEL='CMS')) order by CREATE_TIME desc  ";
        }
        else
        {
            sql = sql + ") order by CREATE_TIME desc  ";
        }

        string phoneNumber = ViewState["phoneNumber"].ToString();
        string startCreateDate = ViewState["startCreateDate"].ToString();
        string endCreateDate = ViewState["endCreateDate"].ToString();
        string startProcessDate = ViewState["startProcessDate"].ToString();
        string endProcessDate = ViewState["endProcessDate"].ToString();
        string applicateMode = ViewState["applicateMode"].ToString();
        string processStatus = ViewState["processStatus"].ToString();
        string BackType = ViewState["BackType"].ToString();
        //string CashCanType = ViewState["CashCanType"].ToString();
        //string CashCanType = ViewState["SOURCECHANNEL"].ToString();
        string cashID = ViewState["cashID"].ToString();

        OracleParameter[] parm ={
                                    new OracleParameter("phoneNumber",OracleType.VarChar), 
                                    new OracleParameter("startCreateDate",OracleType.VarChar),     
                                    new OracleParameter("endCreateDate",OracleType.VarChar),
                                    new OracleParameter("startProcessDate",OracleType.VarChar),
                                    new OracleParameter("endProcessDate",OracleType.VarChar),
                                    new OracleParameter("applicateMode",OracleType.VarChar),
                                    new OracleParameter("processStatus",OracleType.VarChar),
                                    new OracleParameter("BackType",OracleType.VarChar),
                                    new OracleParameter("cashID",OracleType.VarChar)
                                };

        if (String.IsNullOrEmpty(phoneNumber))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = phoneNumber;
        }

        if (String.IsNullOrEmpty(startCreateDate))
        {
            parm[1].Value = DBNull.Value;
        }
        else
        {
            parm[1].Value = startCreateDate;
        }

        if (String.IsNullOrEmpty(endCreateDate))
        {
            parm[2].Value = DBNull.Value;
        }
        else
        {
            parm[2].Value = endCreateDate;
        }

        if (String.IsNullOrEmpty(startProcessDate))
        {
            parm[3].Value = DBNull.Value;
        }
        else
        {
            parm[3].Value = startProcessDate;
        }

        if (String.IsNullOrEmpty(endProcessDate))
        {
            parm[4].Value = DBNull.Value;
        }
        else
        {
            parm[4].Value = endProcessDate;
        }

        if (String.IsNullOrEmpty(applicateMode))
        {
            parm[5].Value = DBNull.Value;
        }
        else
        {
            parm[5].Value = applicateMode;
        }

        if (String.IsNullOrEmpty(processStatus))
        {
            parm[6].Value = DBNull.Value;
        }
        else
        {
            parm[6].Value = processStatus;
        }

        if (String.IsNullOrEmpty(BackType))
        {
            parm[7].Value = DBNull.Value;
        }
        else
        {
            parm[7].Value = BackType;
        }

        //if (String.IsNullOrEmpty(CashCanType))
        //{
        //    parm[8].Value = DBNull.Value;
        //}
        //else
        //{
        //    if (ViewState["CashCanType"].ToString() == "2")
        //    {
        //        parm[8].Value = CashCanType;
        //    }
        //    else {
        //        parm[8].Value = DBNull.Value;
        //    }
        //}

        if (String.IsNullOrEmpty(cashID))
        {
            parm[8].Value = DBNull.Value;
        }
        else
        {
            parm[8].Value = cashID;
        }

        //AspNetPager1.CurrentPageIndex;
        //gridViewCSReviewLmSystemLogList.PageSize;

        DataSet ds = DbHelperOra.Query(sql, true, parm);
        //DataSet ds = DbHelperOra.Query(sql, false, parm);

        return ds;
    }

    private int CountLmCashLog()
    {
        string sql = string.Empty;

        //sql = "select ID,SN,USER_ID,PICK_CASH_AMOUNT,PROCESS_USERID,CASH_WAY,APPLICATE_STATUS,PROCESS_STATUS,APPLICATE_TIME,PROCESS_TIME from T_LM_CASH_TOCASH_APPL where 1=1";
        //sql += " and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber))";
        //sql += " and ((:startCreateDate IS NULL) OR (APPLICATE_TIME >= to_timestamp(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:endCreateDate IS NULL) OR (APPLICATE_TIME <=to_timestamp(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') ))";
        //sql += " and ((:startProcessDate IS NULL) OR (PROCESS_TIME >= to_timestamp(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:endProcessDate IS NULL) OR (PROCESS_TIME <=to_timestamp(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:applicateMode is NULL) or (CASH_WAY=:applicateMode))";
        //sql += " and ((:processStatus is NULL) or (PROCESS_STATUS=:processStatus))";
        //sql += " order by APPLICATE_TIME desc";

        //sql = "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,APPLY_TYPE AS CASH_WAY,STATUS AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME,TYPE AS CASHCANTYPE from t_lm_cash where TYPE=1 and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and ((:startProcessDate IS NULL) OR (UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endProcessDate IS NULL) OR (UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:applicateMode is NULL) or (APPLY_TYPE=:applicateMode)) and ((:processStatus is NULL) or (STATUS=:processStatus)) AND ((:BackType is NULL) or (:BackType = '1' AND STATUS in (0,1))) and ((:CashCanType is NULL) or (TYPE=:CashCanType)) order by CREATE_TIME desc";

        //sql = "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,APPLY_TYPE AS CASH_WAY,STATUS AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME,TYPE AS CASHCANTYPE,source_channel AS CASOURCESHCANNEL from t_lm_cash where TYPE=1 and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and ((:startProcessDate IS NULL) OR (UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endProcessDate IS NULL) OR (UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:applicateMode is NULL) or (APPLY_TYPE=:applicateMode)) and ((:processStatus is NULL) or (STATUS=:processStatus)) AND ((:BackType is NULL) or (:BackType = '1' AND STATUS in (0,1))) and (";
        //sql = sql + " (:cashID IS NULL OR (ID in (SELECT REGEXP_SUBSTR (:cashID, '[^,]+', 1,rownum) FROM DUAL CONNECT BY ROWNUM <=LENGTH (:cashID) - LENGTH (REPLACE (:cashID, ',', '')))))";

        //if (ViewState["SourceChannel"].ToString() == "1")
        //{//用户提现
        //    sql = sql + " and (SOURCE_CHANNEL IS NULL))  order by CREATE_TIME desc  ";
        //}
        //else if (ViewState["SourceChannel"].ToString() == "2")
        //{//cms
        //    sql = sql + " AND (SOURCE_CHANNEL IS NOT NULL AND SOURCE_CHANNEL='CMS')) order by CREATE_TIME desc  ";
        //}
        //else {//无限制
        //    sql = sql + " ) order by CREATE_TIME desc  ";
        //}


        sql = "select count(ID) from ( select tlc.ID,tlc.CREATE_TIME from t_lm_cash tlc ";
        sql = sql + " where tlc.TYPE=1 and ((:phoneNumber is NULL) or (tlc.USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (tlc.CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (tlc.CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and  ((:startProcessDate IS NULL) OR (tlc.UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and   ((:endProcessDate IS NULL) OR (tlc.UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and    ((:applicateMode is NULL) or (tlc.APPLY_TYPE=:applicateMode)) and ((:processStatus is NULL) or (:processStatus='5' AND tlc.STATUS='0' and (tlc.TRY_COUNT IS NULL OR tlc.TRY_COUNT=0)) OR (:processStatus='6' AND tlc.STATUS='0' and (tlc.TRY_COUNT IS NOT NULL AND tlc.TRY_COUNT > 0)) OR (tlc.STATUS=:processStatus)) AND     ((:BackType is NULL) or (:BackType = '1' AND tlc.STATUS in (0,1))) and (:cashID IS NULL OR (tlc.ID in (SELECT REGEXP_SUBSTR (:cashID, '[^,]+', 1,rownum) FROM DUAL CONNECT BY ROWNUM <=LENGTH (:cashID) - LENGTH (REPLACE (:cashID, ',', '')))))";

        if (!string.IsNullOrEmpty(ViewState["OpeType"].ToString()))
        {
            if (ViewState["OpeType"].ToString() == "1")//自动
            {
                sql = sql + " and tlc.ope_type='1'";
            }
            else//手动
            {
                sql = sql + " and tlc.ope_type <> '1'";
            }
        }

        if (ViewState["SourceChannel"].ToString() == "1")
        {
            sql = sql + " and (tlc.SOURCE_CHANNEL IS NULL)) order by CREATE_TIME desc  ";
        }
        else if (ViewState["SourceChannel"].ToString() == "2")
        {
            sql = sql + " AND (tlc.SOURCE_CHANNEL IS NOT NULL AND tlc.SOURCE_CHANNEL='CMS')) order by CREATE_TIME desc  ";
        }
        else
        {
            sql = sql + ") order by CREATE_TIME desc  ";
        }

        string phoneNumber = ViewState["phoneNumber"].ToString();
        string startCreateDate = ViewState["startCreateDate"].ToString();
        string endCreateDate = ViewState["endCreateDate"].ToString();
        string startProcessDate = ViewState["startProcessDate"].ToString();
        string endProcessDate = ViewState["endProcessDate"].ToString();
        string applicateMode = ViewState["applicateMode"].ToString();
        string processStatus = ViewState["processStatus"].ToString();
        string BackType = ViewState["BackType"].ToString();
        //string CashCanType = ViewState["CashCanType"].ToString();
        //string CashCanType = ViewState["SOURCECHANNEL"].ToString();
        string cashID = ViewState["cashID"].ToString();

        OracleParameter[] parm ={
                                    new OracleParameter("phoneNumber",OracleType.VarChar), 
                                    new OracleParameter("startCreateDate",OracleType.VarChar),     
                                    new OracleParameter("endCreateDate",OracleType.VarChar),
                                    new OracleParameter("startProcessDate",OracleType.VarChar),
                                    new OracleParameter("endProcessDate",OracleType.VarChar),
                                    new OracleParameter("applicateMode",OracleType.VarChar),
                                    new OracleParameter("processStatus",OracleType.VarChar),
                                    new OracleParameter("BackType",OracleType.VarChar),
                                    
                                    new OracleParameter("cashID",OracleType.VarChar)
                                };

        if (String.IsNullOrEmpty(phoneNumber))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = phoneNumber;
        }

        if (String.IsNullOrEmpty(startCreateDate))
        {
            parm[1].Value = DBNull.Value;
        }
        else
        {
            parm[1].Value = startCreateDate;
        }

        if (String.IsNullOrEmpty(endCreateDate))
        {
            parm[2].Value = DBNull.Value;
        }
        else
        {
            parm[2].Value = endCreateDate;
        }

        if (String.IsNullOrEmpty(startProcessDate))
        {
            parm[3].Value = DBNull.Value;
        }
        else
        {
            parm[3].Value = startProcessDate;
        }

        if (String.IsNullOrEmpty(endProcessDate))
        {
            parm[4].Value = DBNull.Value;
        }
        else
        {
            parm[4].Value = endProcessDate;
        }

        if (String.IsNullOrEmpty(applicateMode))
        {
            parm[5].Value = DBNull.Value;
        }
        else
        {
            parm[5].Value = applicateMode;
        }

        if (String.IsNullOrEmpty(processStatus))
        {
            parm[6].Value = DBNull.Value;
        }
        else
        {
            parm[6].Value = processStatus;
        }

        if (String.IsNullOrEmpty(BackType))
        {
            parm[7].Value = DBNull.Value;
        }
        else
        {
            parm[7].Value = BackType;
        }

        //if (String.IsNullOrEmpty(CashCanType))
        //{
        //    parm[8].Value = DBNull.Value;
        //}
        //else
        //{
        //    if (ViewState["CashCanType"].ToString() == "2")
        //    {
        //        parm[8].Value = CashCanType;
        //    }
        //    else {
        //        parm[8].Value = DBNull.Value;
        //    }
        //}

        if (String.IsNullOrEmpty(cashID))
        {
            parm[8].Value = DBNull.Value;
        }
        else
        {
            parm[8].Value = cashID;
        }

        //AspNetPager1.CurrentPageIndex;
        //gridViewCSReviewLmSystemLogList.PageSize;

        DataSet ds = DbHelperOra.Query(sql, true, parm);
        //DataSet ds = DbHelperOra.Query(sql, false, parm);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        else
        {
            return 0;
        }
    }

    private DataSet getCashData()
    {
        string sql = string.Empty;

        //sql = "select ID,SN,USER_ID,PICK_CASH_AMOUNT,PROCESS_USERID,CASH_WAY,APPLICATE_STATUS,PROCESS_STATUS,APPLICATE_TIME,PROCESS_TIME from T_LM_CASH_TOCASH_APPL where 1=1";
        //sql += " and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber))";
        //sql += " and ((:startCreateDate IS NULL) OR (APPLICATE_TIME >= to_timestamp(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:endCreateDate IS NULL) OR (APPLICATE_TIME <=to_timestamp(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') ))";
        //sql += " and ((:startProcessDate IS NULL) OR (PROCESS_TIME >= to_timestamp(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:endProcessDate IS NULL) OR (PROCESS_TIME <=to_timestamp(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss')))";
        //sql += " and ((:applicateMode is NULL) or (CASH_WAY=:applicateMode))";
        //sql += " and ((:processStatus is NULL) or (PROCESS_STATUS=:processStatus))";
        //sql += " order by APPLICATE_TIME desc";

        //sql = "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,APPLY_TYPE AS CASH_WAY,STATUS AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME,TYPE AS CASHCANTYPE from t_lm_cash where TYPE=1 and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and ((:startProcessDate IS NULL) OR (UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endProcessDate IS NULL) OR (UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:applicateMode is NULL) or (APPLY_TYPE=:applicateMode)) and ((:processStatus is NULL) or (STATUS=:processStatus)) AND ((:BackType is NULL) or (:BackType = '1' AND STATUS in (0,1))) and ((:CashCanType is NULL) or (TYPE=:CashCanType)) order by CREATE_TIME desc";
        //sql = "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,APPLY_TYPE AS CASH_WAY,STATUS AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME,TYPE AS CASHCANTYPE from t_lm_cash where TYPE=1 and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and ((:startProcessDate IS NULL) OR (UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endProcessDate IS NULL) OR (UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:applicateMode is NULL) or (APPLY_TYPE=:applicateMode)) and ((:processStatus is NULL) or (STATUS=:processStatus)) AND ((:BackType is NULL) or (:BackType = '1' AND STATUS in (0,1))) and ((:CashCanType is NULL) or (SOURCE_CHANNEL=:CashCanType)) order by CREATE_TIME desc";

        // *************2012-12-18 king ****************
        //sql = "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,APPLY_TYPE AS CASH_WAY,STATUS AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME,TYPE AS CASHCANTYPE,source_channel AS CASOURCESHCANNEL,BANK_CARD_OWNER AS BACK_OWNER  from ";
        //sql = sql + " t_lm_cash ";
        //sql = sql + " where TYPE=1 and ((:phoneNumber is NULL) or (USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and ((:startProcessDate IS NULL) OR (UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endProcessDate IS NULL) OR (UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:applicateMode is NULL) or (APPLY_TYPE=:applicateMode)) and ((:processStatus is NULL) or (STATUS=:processStatus)) AND ((:BackType is NULL) or (:BackType = '1' AND STATUS in (0,1))) and ";
        //sql = sql + " (:cashID IS NULL OR (ID in (SELECT REGEXP_SUBSTR (:cashID, '[^,]+', 1,rownum) FROM DUAL CONNECT BY ROWNUM <=LENGTH (:cashID) - LENGTH (REPLACE (:cashID, ',', '')))))";
        ////or (SOURCE_CHANNEL=:CashCanType)) order by CREATE_TIME desc";
        //if (ViewState["SourceChannel"].ToString() == "1")
        //{
        //    sql = sql + " and (SOURCE_CHANNEL IS NULL) order by CREATE_TIME desc  ";
        //}
        //else if (ViewState["SourceChannel"].ToString() == "2")
        //{
        //    sql = sql + " AND (SOURCE_CHANNEL IS NOT NULL AND SOURCE_CHANNEL='CMS') order by CREATE_TIME desc  ";
        //}
        //else
        //{
        //    sql = sql + " order by CREATE_TIME desc  ";
        //}


        sql = "select ID,SN,USER_ID,AMOUNT AS PICK_CASH_AMOUNT,PROCESS_USERID,APPLY_TYPE AS CASH_WAY,STATUS AS PROCESS_STATUS,CREATE_TIME AS APPLICATE_TIME,UPDATE_TIME AS PROCESS_TIME,TYPE AS CASHCANTYPE,source_channel AS CASOURCESHCANNEL,  BANK_CARD_OWNER, case when APPLY_TYPE=1 then BANK_CARD_OWNER else GUEST_NAMES end AS BACK_OWNER, case when length(PROCESS_REMARK) > 8 then substr(PROCESS_REMARK, 0, 8) else PROCESS_REMARK end AS IS_PUSH,REALAMOUNT,CHARGE,TRY_COUNT,CASE WHEN    OPE_TYPE='1' THEN 'app自动' WHEN OPE_TYPE='2' THEN '人工自动' WHEN OPE_TYPE='3' THEN 'app自动受限' WHEN  OPE_TYPE='4' THEN '人工自动受限' WHEN OPE_TYPE='5' THEN 'app自动接口错误'WHEN OPE_TYPE='6' THEN '人工自动接口错误'  WHEN OPE_TYPE='7' THEN '自动转人工' ELSE '纯手工' END  AS OPE_TYPE ";
        sql = sql + " from ( select distinct tlc.ID,tlc.SN,tlc.USER_ID,tlc.AMOUNT,tlc.PROCESS_USERID,tlc.APPLY_TYPE,tlc.STATUS,tlc.CREATE_TIME,tlc.UPDATE_TIME ,tlc.TYPE,tlc.source_channel,tlc.BANK_CARD_OWNER,tlo.GUEST_NAMES, tlc.IS_PUSH,tlp.real_amount AS REALAMOUNT,tlp.charge AS CHARGE,tlc.TRY_COUNT,tlc.ope_type,tlc.PROCESS_REMARK from t_lm_cash tlc left join (select distinct sn,real_amount,charge from t_lm_pay) tlp on tlp.sn=tlc.sn left join (select lo.login_mobile,lo.GUEST_NAMES from (select lto.login_mobile,max(lto.create_time) MCreateTime from t_lm_order lto inner join t_lm_cash tc on lto.login_mobile = tc.USER_ID group by lto.login_mobile) tm inner join t_lm_order lo on tm.login_mobile = lo.login_mobile and tm.MCreateTime = lo.create_time) tlo on tlo.login_mobile=tlc.user_id ";
        sql = sql + " where tlc.TYPE=1 and ((:phoneNumber is NULL) or (tlc.USER_ID=:phoneNumber)) and ((:startCreateDate IS NULL) OR (tlc.CREATE_TIME >= to_date(:startCreateDate, 'yyyy-mm-dd hh24:mi:ss'))) and ((:endCreateDate IS NULL) OR (tlc.CREATE_TIME <=to_date(:endCreateDate, 'yyyy-mm-dd hh24:mi:ss') )) and  ((:startProcessDate IS NULL) OR (tlc.UPDATE_TIME >= to_date(:startProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and   ((:endProcessDate IS NULL) OR (tlc.UPDATE_TIME <=to_date(:endProcessDate, 'yyyy-mm-dd hh24:mi:ss'))) and    ((:applicateMode is NULL) or (tlc.APPLY_TYPE=:applicateMode)) and  ((:processStatus is NULL) or (:processStatus='5' AND tlc.STATUS='0' and (tlc.TRY_COUNT IS NULL OR tlc.TRY_COUNT=0)) OR (:processStatus='6' AND tlc.STATUS='0' and (tlc.TRY_COUNT IS NOT NULL AND tlc.TRY_COUNT > 0)) OR (tlc.STATUS=:processStatus)) AND     ((:BackType is NULL) or (:BackType = '1' AND tlc.STATUS in (0,1))) and (:cashID IS NULL OR (tlc.ID in (SELECT REGEXP_SUBSTR (:cashID, '[^,]+', 1,rownum) FROM DUAL CONNECT BY ROWNUM <=LENGTH (:cashID) - LENGTH (REPLACE (:cashID, ',', '')))))";

        if (!string.IsNullOrEmpty(ViewState["OpeType"].ToString()))
        {
            if (ViewState["OpeType"].ToString() == "1")//自动
            {
                sql = sql + " and tlc.ope_type='1'";
            }
            else//手动
            {
                sql = sql + " and tlc.ope_type <> '1' or tlc.ope_type is null ";
            }
        }


        if (ViewState["SourceChannel"].ToString() == "1")
        {
            sql = sql + " and (tlc.SOURCE_CHANNEL IS NULL)) order by CREATE_TIME desc  ";
        }
        else if (ViewState["SourceChannel"].ToString() == "2")
        {
            sql = sql + " AND (tlc.SOURCE_CHANNEL IS NOT NULL AND tlc.SOURCE_CHANNEL='CMS')) order by CREATE_TIME desc  ";
        }
        else
        {
            sql = sql + ") order by CREATE_TIME desc  ";
        }

        string phoneNumber = ViewState["phoneNumber"].ToString();
        string startCreateDate = ViewState["startCreateDate"].ToString();
        string endCreateDate = ViewState["endCreateDate"].ToString();
        string startProcessDate = ViewState["startProcessDate"].ToString();
        string endProcessDate = ViewState["endProcessDate"].ToString();
        string applicateMode = ViewState["applicateMode"].ToString();
        string processStatus = ViewState["processStatus"].ToString();
        string BackType = ViewState["BackType"].ToString();
        //string CashCanType = ViewState["CashCanType"].ToString();
        //string CashCanType = ViewState["SOURCECHANNEL"].ToString();
        string cashID = ViewState["cashID"].ToString();

        OracleParameter[] parm ={
                                    new OracleParameter("phoneNumber",OracleType.VarChar), 
                                    new OracleParameter("startCreateDate",OracleType.VarChar),     
                                    new OracleParameter("endCreateDate",OracleType.VarChar),
                                    new OracleParameter("startProcessDate",OracleType.VarChar),
                                    new OracleParameter("endProcessDate",OracleType.VarChar),
                                    new OracleParameter("applicateMode",OracleType.VarChar),
                                    new OracleParameter("processStatus",OracleType.VarChar),
                                    new OracleParameter("BackType",OracleType.VarChar),
                                    new OracleParameter("cashID",OracleType.VarChar)
                                };

        if (String.IsNullOrEmpty(phoneNumber))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = phoneNumber;
        }

        if (String.IsNullOrEmpty(startCreateDate))
        {
            parm[1].Value = DBNull.Value;
        }
        else
        {
            parm[1].Value = startCreateDate;
        }

        if (String.IsNullOrEmpty(endCreateDate))
        {
            parm[2].Value = DBNull.Value;
        }
        else
        {
            parm[2].Value = endCreateDate;
        }

        if (String.IsNullOrEmpty(startProcessDate))
        {
            parm[3].Value = DBNull.Value;
        }
        else
        {
            parm[3].Value = startProcessDate;
        }

        if (String.IsNullOrEmpty(endProcessDate))
        {
            parm[4].Value = DBNull.Value;
        }
        else
        {
            parm[4].Value = endProcessDate;
        }

        if (String.IsNullOrEmpty(applicateMode))
        {
            parm[5].Value = DBNull.Value;
        }
        else
        {
            parm[5].Value = applicateMode;
        }

        if (String.IsNullOrEmpty(processStatus))
        {
            parm[6].Value = DBNull.Value;
        }
        else
        {
            parm[6].Value = processStatus;
        }

        if (String.IsNullOrEmpty(BackType))
        {
            parm[7].Value = DBNull.Value;
        }
        else
        {
            parm[7].Value = BackType;
        }

        //if (String.IsNullOrEmpty(CashCanType))
        //{
        //    parm[8].Value = DBNull.Value;
        //}
        //else
        //{
        //    if (ViewState["CashCanType"].ToString() == "2")
        //    {
        //        parm[8].Value = CashCanType;
        //    }
        //    else
        //    {
        //        parm[8].Value = DBNull.Value; 
        //    }
        //}

        if (String.IsNullOrEmpty(cashID))
        {
            parm[8].Value = DBNull.Value;
        }
        else
        {
            parm[8].Value = cashID;
        }
        //AspNetPager1.CurrentPageIndex;
        //gridViewCSReviewLmSystemLogList.PageSize;

        DataSet ds = DbManager.Query(sql, parm, (AspNetPager1.CurrentPageIndex - 1) * gridViewCash.PageSize, gridViewCash.PageSize, true);
        //DataSet ds = DbHelperOra.Query(sql, false, parm);
        return ds;
    }

    //protected void gridViewCash_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    this.gridViewCash.PageIndex = e.NewPageIndex;
    //    BindLToCash();
    //}

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds0 = ExportDataList();//exportDataTable();
            //DataSet ds0 = new DataSet("cashopra");
            //ds0.Tables.Add(dt);

            if (ds0 == null)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
                return;

            }
            if (ds0.Tables[0].Rows.Count <= 0)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
                return;
            }

            CommonFunction.ExportExcelForLM(ds0);
            //StringUtility.WritExcel(ds0.Tables[0], this.Context);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('列表中数据为空，不能导出！');", true);
            return;
        }

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Object lockThis = new System.Object();
        lock (lockThis)
        {
            BatchUpdateCashHis("1");
        }
    }

    protected void btnFail_Click(object sender, EventArgs e)
    {
        System.Object lockThis = new System.Object();
        lock (lockThis)
        {
            BatchUpdateCashHis("0");
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        BatchUpdateCashHis("4");
    }

    private void BatchUpdateCashHis(string strType)
    {
        if (StringUtility.Text_Length(txtRemark.Text.Trim()) > 180)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("BatchUpdateError2").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        ArrayList alList = new ArrayList();
        for (int i = 0; i < this.gridViewCash.Rows.Count; i++)
        {
            System.Web.UI.HtmlControls.HtmlInputCheckBox ck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCash.Rows[i].FindControl("chkItems");
            if (ck.Checked == true)
            {
                alList.Add(gridViewCash.DataKeys[i].Value.ToString());
            }
        }

        DataSet dsTemp = new DataSet();
        if (alList.Count == 0)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("BatchUpdateError").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }

        //if (!chkCashStatusConfirm(alList) && !"1".Equals(hidStatusConfirm.Value.Trim()))
        //{
        //    //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ConfirmSNStatusList('" + strType + "');", true);
        //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "window.confirm('批量处理数据仅操作状态为已操作的申请，是否确定？');", true);
        //    //Response.Write("<script type='text/javascript'>window.confirm('批量处理数据仅操作状态为已操作的申请，是否确定？');</script>");

        //    return;
        //}

        hidStatusConfirm.Value = "";
        foreach (string strSN in alList)
        {
            if ("4".Equals(strType))
            {
                dsTemp = GetModiCashDataInfo(strSN);
            }
            else
            {
                dsTemp = GetCashDataInfo(strSN);
            }

            if (dsTemp.Tables.Count == 0 || dsTemp.Tables[0].Rows.Count == 0)
            {
                continue;
            }

            if ("1".Equals(strType))
            {
                btnOkClick(strSN, dsTemp.Tables[0].Rows[0]["PICK_CASH_AMOUNT"].ToString().Trim(), txtRemark.Text.Trim(), cddpUserRemark.Text.Trim(), dsTemp.Tables[0].Rows[0]["USER_ID"].ToString().Trim(), dsTemp.Tables[0].Rows[0]["CASH_TYPE"].ToString().Trim());
            }
            else if ("0".Equals(strType))
            {
                btnFailClick(strSN, dsTemp.Tables[0].Rows[0]["PICK_CASH_AMOUNT"].ToString().Trim(), txtRemark.Text.Trim(), cddpUserRemark.Text.Trim(), dsTemp.Tables[0].Rows[0]["USER_ID"].ToString().Trim(), dsTemp.Tables[0].Rows[0]["CASH_TYPE"].ToString().Trim());
            }
            else
            {
                btnModiClick(strSN, dsTemp.Tables[0].Rows[0]["PICK_CASH_AMOUNT"].ToString().Trim(), txtRemark.Text.Trim(), cddpUserRemark.Text.Trim(), dsTemp.Tables[0].Rows[0]["USER_ID"].ToString().Trim(), dsTemp.Tables[0].Rows[0]["CASH_TYPE"].ToString().Trim());
            }
        }

        MessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        AspNetPager1.CurrentPageIndex = 1;
        BindLToCash();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    private DataSet GetCashDataInfo(string strSN)
    {
        // king Modify
        string sql = "select USER_ID, amount AS PICK_CASH_AMOUNT, order_num, back_type, back_ticket_user_code, apply_type , bank_name, bank_branch, bank_card_number, bank_card_owner, alipay_account, recharge_phone_number, status, create_time, update_time, process_userid, remark, process_remark, type AS CASH_TYPE, hotel_id, hotel_name, back_ticket_amount, is_delete, source_channel from T_LM_CASH where STATUS = 4 AND SN =" + strSN;
        //string sql = "select USER_ID, amount AS PICK_CASH_AMOUNT, order_num, back_type, back_ticket_user_code, apply_type , bank_name, bank_branch, bank_card_number, bank_card_owner, alipay_account, recharge_phone_number, status, create_time, update_time, process_userid, remark, process_remark, type AS CASH_TYPE, hotel_id, hotel_name, back_ticket_amount, is_delete, source_channel from T_LM_CASH where SN =" + strSN;
        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;
    }

    private DataSet GetModiCashDataInfo(string strSN)
    {
        // king Modify
        string sql = "select USER_ID, amount AS PICK_CASH_AMOUNT, order_num, back_type, back_ticket_user_code, apply_type , bank_name, bank_branch, bank_card_number, bank_card_owner, alipay_account, recharge_phone_number, status, create_time, update_time, process_userid, remark, process_remark, type AS CASH_TYPE, hotel_id, hotel_name, back_ticket_amount, is_delete, source_channel from T_LM_CASH where STATUS = 0 AND SN =" + strSN;
        //string sql = "select USER_ID, amount AS PICK_CASH_AMOUNT, order_num, back_type, back_ticket_user_code, apply_type , bank_name, bank_branch, bank_card_number, bank_card_owner, alipay_account, recharge_phone_number, status, create_time, update_time, process_userid, remark, process_remark, type AS CASH_TYPE, hotel_id, hotel_name, back_ticket_amount, is_delete, source_channel from T_LM_CASH where SN =" + strSN;
        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;
    }

    private bool chkCashStatusConfirm(ArrayList alSNList)
    {
        foreach (string strSN in alSNList)
        {
            if (!chkCashStatus(strSN))
            {
                return false;
            }
        }
        return true;
    }

    private bool chkCashStatus(string strSN)
    {
        string sql = "select sn from T_LM_CASH where (STATUS = 2 OR STATUS = 3) AND SN =" + strSN;
        DataSet ds = DbHelperOra.Query(sql, false);

        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //private bool chkCashStatus(string strSN)
    //{
    //    string sql = "select sn from T_LM_CASH where STATUS = 4 AND SN =" + strSN;
    //    DataSet ds = DbHelperOra.Query(sql, false);

    //    if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
    //    {
    //        // king Modify
    //        return false;
    //        //return true;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}

    private bool chkModiCashStatus(string strSN)
    {
        string sql = "select sn from T_LM_CASH where STATUS = 0 AND SN =" + strSN;
        DataSet ds = DbHelperOra.Query(sql, false);

        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            // king Modify
            return false;
            //return true;
        }
        else
        {
            return true;
        }
    }

    private void btnOkClick(string strID, string pickcashamount, string remark, string userRemark, string User_ID, string cashType)
    {
        try
        {
            if (!chkCashStatus(strID))
            {
                return;
            }

            List<string> list = new List<string>();
            //<asp:ListItem Value="0">已提交</asp:ListItem>
            //<asp:ListItem Value="1">已审核</asp:ListItem>
            //<asp:ListItem Value="2">已成功</asp:ListItem>
            //<asp:ListItem Value="3">已失败</asp:ListItem>
            //<asp:ListItem Value="4">已操作</asp:ListItem>

            //string process_status = this.ddlProcessStatus.SelectedValue;

            string process_status = "3";   //点击按钮当前处理的状态
            //string pickcashamount = this.lbl_pick_cash_amount.Text;//提现金额

            //string remark = this.txtRemark.Text;
            //string userRemark = this.txtUserRemark.Text;
            //string User_ID = this.lbl_User_ID.Text;
            CommonFunction comFun = new CommonFunction();
            int id = comFun.getMaxIDfromSeq("T_LM_CASH_HIS_SEQ");//t_lm_cash_tocash_appl_detl_seq
            //string cashWayCode = this.hidCashWayCode.Value;

            string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
            //修改主表信息，保留最新一次的备注信息           
            //string sqlUpdate = "update T_LM_CASH_TOCASH_APPL set PROCESS_STATUS='" + process_status + "',PROCESS_REMARK='" + remark + "',PROCESS_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where id =" + ViewState["ID"].ToString();
            //list.Add(sqlUpdate);

            ////插入一条新信息到详情表中
            //string sqlInsert = "insert into T_LM_CASH_TOCASH_APPL_DETAIL(ID,REF_APPLICATION_ID,USER_ID,HANDLE_STATUS,HANDLE_REMARK,PAY_MODE,HANDLE_TIME,HANDLER) values  ";
            //sqlInsert += "(" + id + ",'" + ViewState["ID"].ToString() + "','" + User_ID + "'," + process_status + ",'" + remark + "','" + cashWayCode + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff'),'" + UserSession.Current.UserAccount + "' )";
            //list.Add(sqlInsert);

            // king Modify
            string sqlUpdate = "update T_LM_CASH set STATUS='" + process_status + "',REMARK='" + userRemark + "',PROCESS_REMARK='" + remark + "',UPDATE_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where STATUS = 4 AND SN =" + strID;
            //string sqlUpdate = "update T_LM_CASH set STATUS='" + process_status + "',REMARK='" + userRemark + "',PROCESS_REMARK='" + remark + "',UPDATE_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where SN =" + strID;
            list.Add(sqlUpdate);

            string sqlInsert = "insert into t_lm_cash_his (id, sn, user_id, status, remark, process_userid, create_time, type) values  ";
            sqlInsert += "(" + id + ",'" + strID + "','" + User_ID + "'," + process_status + ",'" + remark + "','" + UserSession.Current.UserAccount + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff')," + cashType + ")";
            list.Add(sqlInsert);

            //点击“已审核”
            if (process_status == "3")
            {
                string sqlCashUser = "update t_lm_cash_user set PULLING_AMOUNT =PULLING_AMOUNT-" + pickcashamount + " where USER_ID='" + User_ID + "'";
                list.Add(sqlCashUser);
            }

            DbHelperOra.ExecuteSqlTran(list);
            //MessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        catch
        {
            //MessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }
    }

    private void btnFailClick(string strID, string pickcashamount, string remark, string userRemark, string User_ID, string cashType)
    {
        try
        {
            if (!chkCashStatus(strID))
            {
                return;
            }

            List<string> list = new List<string>();
            string process_status = "2";   //点击按钮"已失败"
            //string pickcashamount = this.lbl_pick_cash_amount.Text;//提现金额
            //string remark = this.txtRemark.Text;
            //string userRemark = this.txtUserRemark.Text;
            //string User_ID = this.lbl_User_ID.Text;//手机号码
            CommonFunction comFun = new CommonFunction();
            int id = comFun.getMaxIDfromSeq("T_LM_CASH_HIS_SEQ");//t_lm_cash_tocash_appl_detl_seq
            //string cashWayCode = this.hidCashWayCode.Value;

            string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);

            // king Modify
            string sqlUpdate = "update T_LM_CASH set STATUS='" + process_status + "',REMARK='" + userRemark + "',PROCESS_REMARK='" + remark + "',UPDATE_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where STATUS = 4 AND SN =" + strID;
            //string sqlUpdate = "update T_LM_CASH set STATUS='" + process_status + "',REMARK='" + userRemark + "',PROCESS_REMARK='" + remark + "',UPDATE_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where SN =" + strID;
            list.Add(sqlUpdate);

            //插入一条新信息到详情表中
            string sqlInsert = "insert into t_lm_cash_his (id, sn, user_id, status, remark, process_userid, create_time, type) values  ";
            sqlInsert += "(" + id + ",'" + strID + "','" + User_ID + "'," + process_status + ",'" + remark + "','" + UserSession.Current.UserAccount + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff')," + cashType + ")";
            list.Add(sqlInsert);

            //点击“已失败”
            if (process_status == "2")
            {
                string sqlCashUser = "update t_lm_cash_user set PULLING_AMOUNT =PULLING_AMOUNT-" + pickcashamount + ",CAN_APPLICTAION_AMOUNT=CAN_APPLICTAION_AMOUNT+" + pickcashamount + " where USER_ID='" + User_ID + "'";
                list.Add(sqlCashUser);
            }

            DbHelperOra.ExecuteSqlTran(list);
            //MessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        catch (Exception ex)
        {
            MessageContent.InnerHtml = ex.Message;
            //MessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }
    }

    private void btnModiClick(string strID, string pickcashamount, string remark, string userRemark, string User_ID, string cashType)
    {
        try
        {
            if (!chkModiCashStatus(strID))
            {
                return;
            }

            List<string> list = new List<string>();
            string process_status = "4";   //点击按钮"已失败"
            //string pickcashamount = this.lbl_pick_cash_amount.Text;//提现金额
            //string remark = this.txtRemark.Text;
            //string userRemark = this.txtUserRemark.Text;
            //string User_ID = this.lbl_User_ID.Text;//手机号码
            CommonFunction comFun = new CommonFunction();
            int id = comFun.getMaxIDfromSeq("T_LM_CASH_HIS_SEQ");//t_lm_cash_tocash_appl_detl_seq
            //string cashWayCode = this.hidCashWayCode.Value;

            string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);

            // king Modify
            string sqlUpdate = "update T_LM_CASH set STATUS='" + process_status + "',REMARK='" + userRemark + "',PROCESS_REMARK='" + remark + "',UPDATE_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where STATUS = 0 AND SN =" + strID;
            //string sqlUpdate = "update T_LM_CASH set STATUS='" + process_status + "',REMARK='" + userRemark + "',PROCESS_REMARK='" + remark + "',UPDATE_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where SN =" + strID;
            list.Add(sqlUpdate);

            //if ("4".Equals(process_status))
            //{
            string sqlInserthis = "insert into t_lm_cash_his (id, sn, user_id, status, remark, process_userid, create_time, type) values  ";
            sqlInserthis += "(" + id + ",'" + strID + "','" + User_ID + "'," + "1" + ",'" + remark + "','" + UserSession.Current.UserAccount + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff')," + cashType + ")";
            list.Add(sqlInserthis);
            id = comFun.getMaxIDfromSeq("T_LM_CASH_HIS_SEQ");//t_lm_cash_tocash_appl_detl_seq
            //}

            //插入一条新信息到详情表中
            string sqlInsert = "insert into t_lm_cash_his (id, sn, user_id, status, remark, process_userid, create_time, type) values  ";
            sqlInsert += "(" + id + ",'" + strID + "','" + User_ID + "'," + process_status + ",'" + remark + "','" + UserSession.Current.UserAccount + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff')," + cashType + ")";
            list.Add(sqlInsert);

            ////点击“已失败”
            //if (process_status == "2")
            //{
            //    string sqlCashUser = "update t_lm_cash_user set PULLING_AMOUNT =PULLING_AMOUNT-" + pickcashamount + ",CAN_APPLICTAION_AMOUNT=CAN_APPLICTAION_AMOUNT+" + pickcashamount + " where USER_ID='" + User_ID + "'";
            //    list.Add(sqlCashUser);
            //}

            DbHelperOra.ExecuteSqlTran(list);
            //MessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        catch (Exception ex)
        {
            MessageContent.InnerHtml = ex.Message;
            //MessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }
    }
}