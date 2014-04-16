using System;
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
using HotelVp.CMS.Domain.ServiceAdapter;

public partial class WebUI_CashBack_CashApplProcess : System.Web.UI.Page
{
    CommonEntity _commonEntity = new CommonEntity();

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

            string id = Request.QueryString["ID"].ToString();
            if (Request.QueryString["TYPE"] != null && !String.IsNullOrEmpty(Request.QueryString["TYPE"]) && "1".Equals(Request.QueryString["TYPE"].ToString().Trim()))
            {
                id = GetCashSNNUmber(id);
            }

            if (String.IsNullOrEmpty(id))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('该提现申请不存在，请确认！');", true);
                btnOk.Visible = false;
                btnFail.Visible = false;
                return;
            }

            ViewState["ID"] = id;
            setLabelValue(id);
            BindLToCash();

            hidCID.Value = GetCashIDFromSN(id);
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

    //设置标签的值
    private void setLabelValue(string id)
    {
        string sql = "select tlc.ID,tlc.SN,tlc.USER_ID,tlc.AMOUNT AS PICK_CASH_AMOUNT,tlc.PROCESS_USERID,tlc.APPLY_TYPE AS CASH_WAY,tlc.STATUS AS PROCESS_STATUS,tlc.CREATE_TIME AS APPLICATE_TIME,tlc.UPDATE_TIME AS PROCESS_TIME,tlc.BANK_NAME,tlc.BANK_BRANCH,tlc.BANK_CARD_NUMBER,tlc.ALIPAY_ACCOUNT,tlc.RECHARGE_PHONE_NUMBER,tlc.PROCESS_REMARK,tlc.REMARK,tlc.BANK_CARD_OWNER,tlc.TYPE,tlc.source_channel,tlc.IS_PUSH,tlc.ALIPAY_ACCOUNT_NAME,tlp.real_amount AS REALAMOUNT,tlp.charge AS CHARGE,tlc.ope_type AS OPE_TYPE_NM from T_LM_CASH tlc left join t_lm_pay tlp on tlp.sn=tlc.sn where tlc.SN=" + id;
        DataTable dt = DbHelperOra.Query(sql, false).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            #region
            this.HidAlipayName.Value = dt.Rows[i]["ALIPAY_ACCOUNT_NAME"].ToString();
            string cashway = dt.Rows[i]["CASH_WAY"].ToString(); ;//0-默认值；1-现金返还；2-支付宝返还；3-手机充值 
            switch (cashway)
            {
                case "1":
                    this.bankCardDiv.Attributes["style"] = "display:''";
                    AlipayPortDiv.Attributes.Add("style", "display:none");
                    MobilePortDiv.Attributes.Add("style", "display:none");
                    this.lbl_User_ID_bank.Text = dt.Rows[i]["USER_ID"].ToString(); //用户ID
                    this.lbl_cash_way_bank.Text = "现金返还";//申请提现方式
                    this.hidCashWayCode_bank.Value = dt.Rows[i]["CASH_WAY"].ToString();
                    this.lbl_pick_cash_amount_bank.Text = dt.Rows[i]["PICK_CASH_AMOUNT"].ToString();//申请提现金额
                    this.hidSOURCECHANNEL.Value = dt.Rows[i]["source_channel"].ToString();
                    switch (hidSOURCECHANNEL.Value)//提现渠道
                    {
                        case "":
                            this.lbback_channel_bank.Text = "用户提现";
                            break;
                        case "CMS":
                            this.lbback_channel_bank.Text = "CMS提现";
                            break;
                        default:
                            this.lbback_channel_bank.Text = "没有选择";
                            break;
                    }

                    this.lbl_bank_name_bank.Text = dt.Rows[i]["BANK_NAME"].ToString();//用户开户银行
                    this.lbl_bank_branch_bank.Text = dt.Rows[i]["BANK_BRANCH"].ToString();//分行/支行信
                    this.lbl_bank_card_number_bank.Text = (dt.Rows[i]["BANK_CARD_OWNER"].ToString().Trim().Length > 0) ? dt.Rows[i]["BANK_CARD_OWNER"].ToString() + "-" + dt.Rows[i]["BANK_CARD_NUMBER"].ToString() : dt.Rows[i]["BANK_CARD_NUMBER"].ToString();//用户银行卡信息
                    string strOPE_TYPE_NM = "人工";
                    switch (dt.Rows[i]["OPE_TYPE_NM"].ToString())//提现操作方式
                    {
                        case "1":
                            strOPE_TYPE_NM = "APP自动提现";
                            break;
                        case "2":
                            strOPE_TYPE_NM = "KFC自动操作";
                            break;
                        case "3":
                            strOPE_TYPE_NM = "APP自动转KFC手动";
                            break;
                        case "4":
                            strOPE_TYPE_NM = "KFC手动操作受限";
                            break;
                        case "5":
                            strOPE_TYPE_NM = "APP自动接口错误";
                            break;
                        case "6":
                            strOPE_TYPE_NM = "KFC自动接口错误";
                            break;
                        default:
                            strOPE_TYPE_NM = "人工";
                            break;
                    }
                    this.lbl_ope_type_bank.Text = strOPE_TYPE_NM;
                    break;
                case "2":
                    this.AlipayPortDiv.Attributes["style"] = "display:''";
                    bankCardDiv.Attributes.Add("style", "display:none");
                    MobilePortDiv.Attributes.Add("style", "display:none");
                    this.lbl_User_ID_alipay.Text = dt.Rows[i]["USER_ID"].ToString(); //用户ID
                    this.lbl_cash_way_alipay.Text = "支付宝返还";//申请提现方式
                    this.hidCashWayCode_alipay.Value = dt.Rows[i]["CASH_WAY"].ToString();
                    this.lbl_pick_cash_amount_alipay.Text = dt.Rows[i]["PICK_CASH_AMOUNT"].ToString();//申请提现金额
                    this.hidSOURCECHANNEL.Value = dt.Rows[i]["source_channel"].ToString();
                    switch (hidSOURCECHANNEL.Value)//提现渠道
                    {
                        case "":
                            this.lbback_channel_alipay.Text = "用户提现";
                            break;
                        case "CMS":
                            this.lbback_channel_alipay.Text = "CMS提现";
                            break;
                        default:
                            this.lbback_channel_alipay.Text = "没有选择";
                            break;
                    }
                    this.lbl_Alipay_Port.Text = dt.Rows[i]["ALIPAY_ACCOUNT"].ToString(); ;//支付宝账号
                    this.lbl_Alipay_port_name.Text = dt.Rows[i]["ALIPAY_ACCOUNT_NAME"].ToString();//支付宝账户名
                    strOPE_TYPE_NM = "人工";
                    switch (dt.Rows[i]["OPE_TYPE_NM"].ToString())//提现操作方式
                    {
                        case "1":
                            strOPE_TYPE_NM = "APP自动提现";
                            break;
                        case "2":
                            strOPE_TYPE_NM = "KFC自动操作";
                            break;
                        case "3":
                            strOPE_TYPE_NM = "APP自动转KFC手动";
                            break;
                        case "4":
                            strOPE_TYPE_NM = "KFC手动操作受限";
                            break;
                        case "5":
                            strOPE_TYPE_NM = "APP自动接口错误";
                            break;
                        case "6":
                            strOPE_TYPE_NM = "KFC自动接口错误";
                            break;
                        default:
                            strOPE_TYPE_NM = "人工";
                            break;
                    }
                    this.lbl_ope_type_alipay.Text = strOPE_TYPE_NM;
                    break;
                case "3":
                    this.MobilePortDiv.Attributes["style"] = "display:''";
                    bankCardDiv.Attributes.Add("style", "display:none");
                    AlipayPortDiv.Attributes.Add("style", "display:none");
                    this.lbl_User_ID_mobile.Text = dt.Rows[i]["USER_ID"].ToString(); //用户ID
                    this.lbl_cash_way_mobile.Text = "手机充值";//申请提现方式
                    this.hidCashWayCode_mobile.Value = dt.Rows[i]["CASH_WAY"].ToString();
                    this.lbl_pick_cash_amount_mobile.Text = dt.Rows[i]["PICK_CASH_AMOUNT"].ToString();//申请提现金额
                    this.hidSOURCECHANNEL.Value = dt.Rows[i]["source_channel"].ToString();
                    switch (hidSOURCECHANNEL.Value)//提现渠道
                    {
                        case "":
                            this.lbback_channel_mobile.Text = "用户提现";
                            break;
                        case "CMS":
                            this.lbback_channel_mobile.Text = "CMS提现";
                            break;
                        default:
                            this.lbback_channel_mobile.Text = "没有选择";
                            break;
                    }
                    this.lbl_recharge_phone_number_mobile.Text = dt.Rows[i]["RECHARGE_PHONE_NUMBER"].ToString();//用户手机号码
                    strOPE_TYPE_NM = "人工";
                    switch (dt.Rows[i]["OPE_TYPE_NM"].ToString())//提现操作方式
                    {
                        case "1":
                            strOPE_TYPE_NM = "APP自动提现";
                            break;
                        case "2":
                            strOPE_TYPE_NM = "KFC自动操作";
                            break;
                        case "3":
                            strOPE_TYPE_NM = "APP自动转KFC手动";
                            break;
                        case "4":
                            strOPE_TYPE_NM = "KFC手动操作受限";
                            break;
                        case "5":
                            strOPE_TYPE_NM = "APP自动接口错误";
                            break;
                        case "6":
                            strOPE_TYPE_NM = "KFC自动接口错误";
                            break;
                        default:
                            strOPE_TYPE_NM = "人工";
                            break;
                    }
                    this.lbl_ope_type_alipay_mobile.Text = strOPE_TYPE_NM;
                    break;
                default:
                    break;
            }


            this.HidSN.Value = dt.Rows[i]["SN"].ToString();
            this.hidCashType.Value = dt.Rows[i]["TYPE"].ToString();
            #endregion

            //判断当前的处理状态
            string tempProcess = dt.Rows[i]["PROCESS_STATUS"].ToString();
            string okText = string.Empty;
            chkPush.Checked = ("1".Equals(dt.Rows[i]["IS_PUSH"].ToString())) ? true : false;

            switch (cashway)
            {
                case "1":
                    this.HidPort.Value = "";
                    this.HidFlowBtn.Value = "1";
                    //this.lbl_cash_way.Text = "现金返还";
                    this.lblFlowText.Text = "已提交——>已审核——>已操作——>已成功 or 已失败";
                    #region
                    switch (tempProcess)
                    {
                        case "0":
                            this.btnOk.Enabled = true;
                            this.btnOk.Text = "已操作";
                            this.btnFail.Style.Add("display", "none");
                            this.hidProcessStatus.Value = "4";
                            this.tdChkPush.Style.Add("display", "none");
                            break;
                        case "4":
                            this.btnOk.Enabled = true;
                            this.btnOk.Text = "已成功";
                            this.btnFail.Style.Add("display", "block");
                           // this.btnFail.Attributes.Add("style", "display:block");                            
                            this.hidProcessStatus.Value = "3";
                            this.tdChkPush.Style.Add("display", "block");
                            break;
                        case "2":
                            this.btnOk.Style.Add("display", "none");
                            this.btnFail.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "block");
                            chkPush.Enabled = false;
                            break;
                        case "3":
                            this.btnOk.Style.Add("display", "none");
                            this.btnFail.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "block");
                            chkPush.Enabled = false;
                            break;
                        default:
                            this.btnOk.Style.Add("display", "none");
                            this.btnFail.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "none");
                            chkPush.Enabled = false;
                            break;
                    }
                    #endregion
                    break;
                case "2":
                    this.HidPort.Value = "2";
                    this.HidFlowBtn.Value = "2";
                    //this.lbl_cash_way.Text = "支付宝返还";
                    this.lblFlowText.Text = "已提交——>已成功 or 已失败";

                    
                    if (string.IsNullOrEmpty(this.lbl_Alipay_port_name.Text))
                    {
                        this.btnOk.Text = "请补全支付宝账户名";
                        //this.btnOk.Attributes.Add("disabled", "true");
                        this.btnOk.Enabled = false;

                        this.AlipayNameDiv.InnerHtml = "";
                        this.AlipayNameDiv.InnerHtml = "<input type=\"button\" id=\"btnEditAlipayName\" class=\"btn primary\" runat=\"server\" value=\"修改\" onclick=\"EditAlipayName();\" />";
                    }
                    else
                    {
                        this.btnOk.Enabled = true;
                        this.btnOk.Text = "付款";
                       
                    }
                    this.EbankByPort.Style.Add("display", "none");
                    this.btnFail.Style.Add("display", "none");
                    this.hidProcessStatus.Value = "4";
                    this.tdChkPush.Style.Add("display", "none");

                    break;
                case "3":
                    this.HidPort.Value = "3";
                    this.HidFlowBtn.Value = "3";
                    //this.lbl_cash_way.Text = "手机充值";
                    this.lblFlowText.Text = "已提交——>已成功 or 已失败";

                    this.btnOk.Enabled = true;
                    this.btnOk.Text = "付款";
                    this.EbankByPort.Style.Add("display", "none");
                    this.btnFail.Style.Add("display", "none");
                    this.hidProcessStatus.Value = "4";
                    this.tdChkPush.Style.Add("display", "none");

                    break;
                default:
                    this.HidPort.Value = "";
                    this.HidFlowBtn.Value = "1";
                    //this.lbl_cash_way.Text = "没有选择";
                    this.lblFlowText.Text = "已提交——>已审核——>已操作——>已成功 or 已失败";
                    #region
                    switch (tempProcess)
                    {
                        case "0":
                            this.btnOk.Enabled = true;
                            this.btnOk.Text = "已操作";
                            this.btnFail.Style.Add("display", "none");
                            this.hidProcessStatus.Value = "4";
                            this.tdChkPush.Style.Add("display", "none");
                            break;
                        case "4":
                            this.btnOk.Enabled = true;
                            this.btnOk.Text = "已成功";
                            this.btnFail.Style.Add("display", "block");
                            this.hidProcessStatus.Value = "3";
                            this.tdChkPush.Style.Add("display", "block");
                            break;
                        case "2":
                            this.btnOk.Style.Add("display", "none");
                            this.btnFail.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "block");
                            chkPush.Enabled = false;
                            break;
                        case "3":
                            this.btnOk.Style.Add("display", "none");
                            this.btnFail.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "block");
                            chkPush.Enabled = false;
                            break;
                        default:
                            this.btnOk.Style.Add("display", "none");
                            this.btnFail.Style.Add("display", "none");
                            this.tdChkPush.Style.Add("display", "none");
                            chkPush.Enabled = false;
                            break;
                    }
                    #endregion
                    break;
            }


            //this.lbl_pick_cash_amount.Text = dt.Rows[i]["PICK_CASH_AMOUNT"].ToString();
            //this.lbl_bank_name.Text = dt.Rows[i]["BANK_NAME"].ToString();
            //this.lbl_bank_branch.Text = dt.Rows[i]["BANK_BRANCH"].ToString();
            //this.lbl_bank_card_number.Text = (dt.Rows[i]["BANK_CARD_OWNER"].ToString().Trim().Length > 0) ? dt.Rows[i]["BANK_CARD_OWNER"].ToString() + "-" + dt.Rows[i]["BANK_CARD_NUMBER"].ToString() : dt.Rows[i]["BANK_CARD_NUMBER"].ToString();
            //this.lbl_alipay_account.Text = dt.Rows[i]["ALIPAY_ACCOUNT"].ToString();
            //this.lbl_recharge_phone_number.Text = dt.Rows[i]["RECHARGE_PHONE_NUMBER"].ToString();

            //this.ddlProcessStatus.SelectedValue = dt.Rows[i]["PROCESS_STATUS"].ToString();

            //this.hidProcessStatus.Value = tempProcess;            
            //"0"--已提交
            //"1"--已审核
            //"2"--已成功
            //"3"--已失败

            //因为要表示下一个动作，即
            this.Hidlbl_alipay_account_name.Value = dt.Rows[i]["ALIPAY_ACCOUNT_NAME"].ToString();
            this.Hidlbl_realamount.Value = dt.Rows[i]["REALAMOUNT"].ToString();
            this.Hidlbl_charge.Value = dt.Rows[i]["CHARGE"].ToString();



            this.txtRemark.Text = dt.Rows[i]["PROCESS_REMARK"].ToString();
            this.txtUserRemark.Text = dt.Rows[i]["REMARK"].ToString();

            //switch (hidSOURCECHANNEL.Value)
            //{
            //    case "":
            //        this.lbback_channel.Text = "用户提现";
            //        break;
            //    case "CMS":
            //        this.lbback_channel.Text = "CMS提现";
            //        break;
            //    default:
            //        this.lbback_channel.Text = "没有选择";
            //        break;
            //}
        }
        this.lbl_process_userid.Text = UserSession.Current.UserAccount;
    }

    private void SetEmptyDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn HANDLE_STATUS_dc = new DataColumn("HANDLE_STATUS");
        DataColumn HANDLE_TIME_dc = new DataColumn("HANDLE_TIME");
        DataColumn HANDLER_dc = new DataColumn("HANDLER");
        DataColumn HANDLE_REMARK_dc = new DataColumn("HANDLE_REMARK");
        DataColumn HANDLE_PUSH_dc = new DataColumn("HANDLE_PUSH");

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(HANDLE_STATUS_dc);
        dt.Columns.Add(HANDLE_TIME_dc);
        dt.Columns.Add(HANDLER_dc);
        dt.Columns.Add(HANDLE_REMARK_dc);
        dt.Columns.Add(HANDLE_PUSH_dc);
        GridviewControl.GridViewDataBind(this.gridViewCash, dt);
    }

    public DataTable createDataTable()
    {
        
        DataTable dt = new DataTable();
        DataColumn ID_dc = new DataColumn("ID");
        DataColumn HANDLE_STATUS_dc = new DataColumn("HANDLE_STATUS");
        DataColumn HANDLE_TIME_dc = new DataColumn("HANDLE_TIME");
        DataColumn HANDLER_dc = new DataColumn("HANDLER");
        DataColumn HANDLE_REMARK_dc = new DataColumn("HANDLE_REMARK");
        DataColumn HANDLE_PUSH_dc = new DataColumn("HANDLE_PUSH");

        dt.Columns.Add(ID_dc);
        dt.Columns.Add(HANDLE_STATUS_dc);
        dt.Columns.Add(HANDLE_TIME_dc);
        dt.Columns.Add(HANDLER_dc);
        dt.Columns.Add(HANDLE_REMARK_dc);
        dt.Columns.Add(HANDLE_PUSH_dc);

        DataTable dtResultSQL = GetCashBackHistoryByEventHistory();
        if (dtResultSQL != null && dtResultSQL.Rows.Count > 0)
        {
            for (int i = 0; i < dtResultSQL.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = dtResultSQL.Rows[i]["NID"];
                dr["HANDLE_STATUS"] = dtResultSQL.Rows[i]["EVENT_RESULT"];
                dr["HANDLE_TIME"] = dtResultSQL.Rows[i]["CREATEDATE"];
                dr["HANDLER"] = dtResultSQL.Rows[i]["USERID"];
                dr["HANDLE_REMARK"] = dtResultSQL.Rows[i]["EVENT_CONTENT"];
                dr["HANDLE_PUSH"] = "";
                dt.Rows.Add(dr);
            }
        }

        DataTable getDataTable = getCashData().Tables[0];
        for (int i = 0; i < getDataTable.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = getDataTable.Rows[i]["ID"];
            dr["HANDLE_STATUS"] = getDataTable.Rows[i]["HANDLE_STATUS"];
            dr["HANDLE_TIME"] = getDataTable.Rows[i]["HANDLE_TIME"];
            dr["HANDLER"] = getDataTable.Rows[i]["HANDLER"];
            dr["HANDLE_REMARK"] = getDataTable.Rows[i]["HANDLE_REMARK"];

            string applicateStatus = getDataTable.Rows[i]["HANDLE_STATUS"].ToString().Trim();//0-已提交；1-已审核；2-已成功；3-已失败
            switch (applicateStatus)
            {
                case "0":
                    dr["HANDLE_STATUS"] = "已提交";
                    break;
                case "1":
                    dr["HANDLE_STATUS"] = "已审核";
                    break;
                case "3":
                    dr["HANDLE_STATUS"] = "已成功";
                    break;
                case "2":
                    dr["HANDLE_STATUS"] = "已失败";
                    break;
                case "4":
                    dr["HANDLE_STATUS"] = "已操作";
                    break;
                default:
                    dr["HANDLE_STATUS"] = "没有选择";
                    break;
            }

            if ("1".Equals(getDataTable.Rows[i]["HANDLE_PUSH"].ToString()))
            {
                dr["HANDLE_PUSH"] = "是";
            }
            else
            {
                dr["HANDLE_PUSH"] = "否";
            }

            dt.Rows.Add(dr);
        }

        
        return dt;

    }

    private void BindLToCash()
    {
        DataTable dt = createDataTable();
        GridviewControl.GridViewDataBind(this.gridViewCash, dt);
        this.UpdatePanel2.Update();
    }

    private DataSet getCashData()
    {
        string sql = string.Empty;
        //sql = "select ID,SN,REF_APPLICATION_ID,USER_ID,HANDLE_STATUS,HANDLE_RESULT,HANDLE_REMARK,PAY_MODE,HANDLE_TIME,HANDLER from T_LM_CASH_TOCASH_APPL_DETAIL where SN =" + ViewState["ID"].ToString() + " order by HANDLE_TIME desc";
        sql = "select tlch.ID,tlch.SN,tlch.USER_ID,tlch.STATUS AS HANDLE_STATUS,tlch.STATUS AS HANDLE_RESULT,tlch.REMARK AS HANDLE_REMARK,tlc.apply_type AS PAY_MODE,tlch.CREATE_TIME AS HANDLE_TIME,tlch.PROCESS_USERID AS HANDLER,tlch.IS_PUSH AS HANDLE_PUSH from t_lm_cash_his tlch left join T_LM_CASH tlc on tlch.sn=tlc.sn and tlch.type IN (1,2) and tlc.type IN (1,2) where tlch.SN =" + ViewState["ID"].ToString() + " order by tlch.ID desc";

        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;
    }

    private string GetCashSNNUmber(string id)
    {
        string sql = string.Empty;
        //sql = "select ID,SN,REF_APPLICATION_ID,USER_ID,HANDLE_STATUS,HANDLE_RESULT,HANDLE_REMARK,PAY_MODE,HANDLE_TIME,HANDLER from T_LM_CASH_TOCASH_APPL_DETAIL where SN =" + ViewState["ID"].ToString() + " order by HANDLE_TIME desc";
        sql = "select  ID, SN from T_LM_CASH  where ID =" + id;
        DataSet ds = DbHelperOra.Query(sql, false);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["SN"].ToString().Trim();
        }
        else
        {
            return "";
        }
    }

    private string GetCashBackStatus(string CashSN)
    {
        string sql = string.Empty;
        sql = "select * from t_lm_cash where SN='" + CashSN + "'";
        DataSet ds = DbHelperOra.Query(sql, false);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["status"].ToString().Trim();
        }
        else
        {
            return "";
        }
    }



    private string GetCashIDFromSN(string id)
    {
        string sql = string.Empty;
        //sql = "select ID,SN,REF_APPLICATION_ID,USER_ID,HANDLE_STATUS,HANDLE_RESULT,HANDLE_REMARK,PAY_MODE,HANDLE_TIME,HANDLER from T_LM_CASH_TOCASH_APPL_DETAIL where SN =" + ViewState["ID"].ToString() + " order by HANDLE_TIME desc";
        sql = "select  ID, SN from T_LM_CASH  where SN =" + id;
        DataSet ds = DbHelperOra.Query(sql, false);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["ID"].ToString().Trim();
        }
        else
        {
            return "";
        }
    }

    //点击确定按钮
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (this.HidFlowBtn.Value == "1")
        {
            #region
            try
            {
                if (!chkCashStatus(ViewState["ID"].ToString()))
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('该提现申请状态已经更新，请刷新页面！');", true);
                    return;
                }

                if (StringUtility.Text_Length(txtRemark.Text.Trim()) > 180)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('处理备注最多60个中文字，请修改！');", true);
                    return;
                }

                List<string> list = new List<string>();
                //<asp:ListItem Value="0">已提交</asp:ListItem>
                //<asp:ListItem Value="1">已审核</asp:ListItem>
                //<asp:ListItem Value="2">已成功</asp:ListItem>
                //<asp:ListItem Value="3">已失败</asp:ListItem>
                //<asp:ListItem Value="4">已操作</asp:ListItem>

                //string process_status = this.ddlProcessStatus.SelectedValue;

                string process_status = this.hidProcessStatus.Value;   //点击按钮当前处理的状态
                string pickcashamount = this.lbl_pick_cash_amount_bank.Text;//提现金额

                string remark = this.txtRemark.Text;
                string userRemark = this.txtUserRemark.Text;
                string User_ID = this.lbl_User_ID_bank.Text;
                string isPush = (chkPush.Checked) ? "1" : "0";

                CommonFunction comFun = new CommonFunction();
                int id = comFun.getMaxIDfromSeq("T_LM_CASH_HIS_SEQ");//t_lm_cash_tocash_appl_detl_seq
                string cashWayCode = this.hidCashWayCode_bank.Value;

                string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);
                //修改主表信息，保留最新一次的备注信息           
                //string sqlUpdate = "update T_LM_CASH_TOCASH_APPL set PROCESS_STATUS='" + process_status + "',PROCESS_REMARK='" + remark + "',PROCESS_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where id =" + ViewState["ID"].ToString();
                //list.Add(sqlUpdate);

                ////插入一条新信息到详情表中
                //string sqlInsert = "insert into T_LM_CASH_TOCASH_APPL_DETAIL(ID,REF_APPLICATION_ID,USER_ID,HANDLE_STATUS,HANDLE_REMARK,PAY_MODE,HANDLE_TIME,HANDLER) values  ";
                //sqlInsert += "(" + id + ",'" + ViewState["ID"].ToString() + "','" + User_ID + "'," + process_status + ",'" + remark + "','" + cashWayCode + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff'),'" + UserSession.Current.UserAccount + "' )";
                //list.Add(sqlInsert);

                string sqlUpdate = "update T_LM_CASH set STATUS='" + process_status + "',REMARK='" + userRemark + "',PROCESS_REMARK='" + remark + "',UPDATE_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "',IS_PUSH='" + isPush + "'  where (STATUS <> 2 AND STATUS <> 3) AND SN =" + ViewState["ID"].ToString();
                list.Add(sqlUpdate);

                //插入一条新信息到详情表中
                if ("4".Equals(process_status))
                {
                    string sqlInserthis = "insert into t_lm_cash_his (id, sn, user_id, status, remark, process_userid, create_time, type, is_push) values  ";
                    sqlInserthis += "(" + id + ",'" + ViewState["ID"].ToString() + "','" + User_ID + "'," + "1" + ",'" + remark + "','" + UserSession.Current.UserAccount + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff')," + hidCashType.Value + ",'" + isPush + "')";
                    list.Add(sqlInserthis);
                    id = comFun.getMaxIDfromSeq("T_LM_CASH_HIS_SEQ");//t_lm_cash_tocash_appl_detl_seq
                }

                string sqlInsert = "insert into t_lm_cash_his (id, sn, user_id, status, remark, process_userid, create_time, type, is_push) values  ";
                sqlInsert += "(" + id + ",'" + ViewState["ID"].ToString() + "','" + User_ID + "'," + process_status + ",'" + remark + "','" + UserSession.Current.UserAccount + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff')," + hidCashType.Value + ",'" + isPush + "')";
                list.Add(sqlInsert);

                //点击“已审核”
                if (process_status == "3")
                {
                    string sqlCashUser = "update t_lm_cash_user set PULLING_AMOUNT =PULLING_AMOUNT-" + pickcashamount + " where USER_ID='" + User_ID + "'";
                    list.Add(sqlCashUser);
                }
                DbHelperOra.ExecuteSqlTran(list);
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('保存成功！');", true);
                BindLToCash();
                setLabelValue(ViewState["ID"].ToString());

                if ("1".Equals(isPush))
                {
                    PushEntity pushEntity = new PushEntity();
                    pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                    pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                    pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
                    pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

                    pushEntity.PushDBEntity = new List<PushDBEntity>();
                    PushDBEntity pushDBEntity = new PushDBEntity();
                    pushDBEntity.ID = ViewState["ID"].ToString();
                    pushDBEntity.Content = userRemark;
                    pushDBEntity.Type = "6";
                    pushDBEntity.TelPhone = User_ID;
                    pushEntity.PushDBEntity.Add(pushDBEntity);
                    PushInfoSA.SendPush(pushEntity);
                }
            }
            catch
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('保存失败！');", true);
            }
            #endregion
        }
        else
        {
            try
            {
                if (GetCashBackStatus(ViewState["ID"].ToString()) == "0")
                {

                    if (!string.IsNullOrEmpty(this.HidPort.Value))
                    {
                        if (HidPort.Value == "3")
                        {
                            MobilePort_Click(null, null);//手机
                        }
                        else
                        {
                            AlipayPort_Click(null, null);//支付宝
                        }
                    }
                }
                else
                {
                    if (GetCashBackStatus(ViewState["ID"].ToString()) == "2")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "fail", "alert('已失败！');", true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "alertClose", "BtnCompleteStyle();", true);
        BindLToCash();
    }

    private bool chkCashStatus(string strSN)
    {
        string sql = "select sn from T_LM_CASH where (STATUS = 2 OR STATUS = 3) AND SN =" + ViewState["ID"].ToString();
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

    //点击“已失败”按钮
    protected void btnFail_Click(object sender, EventArgs e)
    {
        System.Object lockThis = new System.Object();
        lock (lockThis)
        {
            try
            {
                if (!chkCashStatus(ViewState["ID"].ToString()))
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('该提现申请状态已经更新，请刷新页面！');", true);
                    return;
                }

                if (StringUtility.Text_Length(txtRemark.Text.Trim()) > 180)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('处理备注最多60个中文字，请修改！');", true);
                    return;
                }

                List<string> list = new List<string>();
                //<asp:ListItem Value="0">已提交</asp:ListItem>
                //<asp:ListItem Value="1">已审核</asp:ListItem>
                //<asp:ListItem Value="2">已成功</asp:ListItem>
                //<asp:ListItem Value="3">已失败</asp:ListItem>
                //string process_status = this.ddlProcessStatus.SelectedValue;

                string process_status = "2";   //点击按钮"已失败"
                string pickcashamount = this.lbl_pick_cash_amount_bank.Text;//提现金额

                string remark = this.txtRemark.Text;
                string userRemark = this.txtUserRemark.Text;
                string User_ID = this.lbl_User_ID_bank.Text;//手机号码
                CommonFunction comFun = new CommonFunction();
                int id = comFun.getMaxIDfromSeq("T_LM_CASH_HIS_SEQ");//t_lm_cash_tocash_appl_detl_seq
                string cashWayCode = this.hidCashWayCode_bank.Value;
                string isPush = (chkPush.Checked) ? "1" : "0";
                string strNow = string.Format("{0:yyyy-MM-dd HH:mm:ss}", System.DateTime.Now);

                //修改主表信息，保留最新一次的备注信息
                //string sqlUpdate = "update T_LM_CASH_TOCASH_APPL set PROCESS_STATUS='" + process_status + "',PROCESS_REMARK='" + remark + "',PROCESS_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "'  where SN =" + ViewState["ID"].ToString();
                //list.Add(sqlUpdate);

                ////插入一条新信息到详情表中
                //string sqlInsert = "insert into T_LM_CASH_TOCASH_APPL_DETAIL(ID,REF_APPLICATION_ID,USER_ID,HANDLE_STATUS,HANDLE_REMARK,PAY_MODE,HANDLE_TIME,HANDLER) values  ";
                //sqlInsert += "(" + id + ",'" + ViewState["ID"].ToString() + "','" + User_ID + "'," + process_status + ",'" + remark + "','" + cashWayCode + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff'),'" + UserSession.Current.UserAccount + "' )";
                //list.Add(sqlInsert);

                ////点击“已审核”
                //if (process_status == "3")
                //{
                //    string sqlCashUser = "update t_lm_cash_user set PULLING_AMOUNT =PULLING_AMOUNT-" + pickcashamount + ",CAN_APPLICTAION_AMOUNT=CAN_APPLICTAION_AMOUNT+" + pickcashamount + " where USER_ID='" + User_ID + "'";
                //    list.Add(sqlCashUser);
                //}

                string sqlUpdate = "update T_LM_CASH set STATUS='" + process_status + "',REMARK='" + userRemark + "',PROCESS_REMARK='" + remark + "',UPDATE_TIME =to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff') ,PROCESS_USERID='" + UserSession.Current.UserAccount + "',IS_PUSH='" + isPush + "'  where (STATUS <> 2 AND STATUS <> 3) AND SN =" + ViewState["ID"].ToString();
                list.Add(sqlUpdate);

                //插入一条新信息到详情表中
                string sqlInsert = "insert into t_lm_cash_his (id, sn, user_id, status, remark, process_userid, create_time, type, is_push) values  ";
                sqlInsert += "(" + id + ",'" + ViewState["ID"].ToString() + "','" + User_ID + "'," + process_status + ",'" + remark + "','" + UserSession.Current.UserAccount + "',to_timestamp('" + strNow + "','yyyy-mm-dd hh24:mi:ss.ff')," + hidCashType.Value + ",'" + isPush + "')";
                list.Add(sqlInsert);

                //点击“已失败”
                if (process_status == "2")
                {
                    string sqlCashUser = "update t_lm_cash_user set PULLING_AMOUNT =PULLING_AMOUNT-" + pickcashamount + ",CAN_APPLICTAION_AMOUNT=CAN_APPLICTAION_AMOUNT+" + pickcashamount + " where USER_ID='" + User_ID + "'";
                    list.Add(sqlCashUser);
                }

                DbHelperOra.ExecuteSqlTran(list);
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('保存成功！');", true);
                BindLToCash();
                setLabelValue(ViewState["ID"].ToString());

                if ("1".Equals(isPush))
                {
                    PushEntity pushEntity = new PushEntity();
                    pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                    pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                    pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
                    pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

                    pushEntity.PushDBEntity = new List<PushDBEntity>();
                    PushDBEntity pushDBEntity = new PushDBEntity();
                    pushDBEntity.ID = ViewState["ID"].ToString();
                    pushDBEntity.Content = userRemark;
                    pushDBEntity.TelPhone = User_ID;
                    pushDBEntity.Type = "6";
                    pushDBEntity.Title = "用户提现";
                    pushEntity.PushDBEntity.Add(pushDBEntity);
                    PushInfoSA.SendPush(pushEntity);
                }
            }
            catch
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('保存失败！');", true);
            }
        }
    }

    //翻页
    protected void gridViewCash_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCash.PageIndex = e.NewPageIndex;
        BindLToCash();
    }

    #region 充值操作
    /// <summary>
    /// 网银接口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EbankPort_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 手机充值接口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MobilePort_Click(object sender, EventArgs e)
    {
        CashBackEntity _cashBackEntity = new CashBackEntity();
        _cashBackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cashBackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cashBackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cashBackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _cashBackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        CashBackDBEntity cashBackDBEntity = new CashBackDBEntity();
        cashBackDBEntity.LoginMobile = lbl_User_ID_mobile.Text;//登录号
        cashBackDBEntity.BackCashAmount = lbl_pick_cash_amount_mobile.Text;//充值金额
        cashBackDBEntity.BackCashType = "3";//3:手机充值
        cashBackDBEntity.Phone = lbl_recharge_phone_number_mobile.Text;//手机充值号码
        cashBackDBEntity.Sn = this.HidSN.Value;//SN
        cashBackDBEntity.CreateUser = UserSession.Current.UserAccount;//操作人
        cashBackDBEntity.Remark = this.txtRemark.Text;

        _cashBackEntity.CashBackDBEntity.Add(cashBackDBEntity);
        _cashBackEntity = CashBackBP.autoPay(_cashBackEntity);

        _commonEntity.LogMessages = _cashBackEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();
        commonDBEntity.Event_Type = "用户提现处理--手机充值";
        commonDBEntity.Event_ID = this.HidSN.Value;
        commonDBEntity.Event_Content = "SN:" + this.HidSN.Value + "登录手机号:" + lbl_User_ID_mobile.Text + ",充值金额:" + lbl_pick_cash_amount_mobile.Text + ",充值手机号码:" + lbl_recharge_phone_number_mobile.Text + ",操作人:" + UserSession.Current.UserAccount + ",描述:" + txtRemark.Text;
        if (_cashBackEntity.Result == 1)
        {
            commonDBEntity.Event_Result = "操作成功,提示：" + _cashBackEntity.ErrorMSG;
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "tishi", "alert('操作成功,原因：" + _cashBackEntity.ErrorMSG + "！');", true);
        }
        else
        {
            commonDBEntity.Event_Result = "操作失败,原因：" + _cashBackEntity.ErrorMSG;
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "tishi", "alert('操作失败,原因：" + _cashBackEntity.ErrorMSG + "！');", true);
        }
    }

    /// <summary>
    /// 支付宝
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AlipayPort_Click(object sender, EventArgs e)
    {
        CashBackEntity _cashBackEntity = new CashBackEntity();
        _cashBackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cashBackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cashBackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cashBackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _cashBackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        CashBackDBEntity cashBackDBEntity = new CashBackDBEntity();
        cashBackDBEntity.LoginMobile = lbl_User_ID_alipay.Text;//登录号
        cashBackDBEntity.BackCashAmount = lbl_pick_cash_amount_alipay.Text;//充值金额
        cashBackDBEntity.BackCashType = "2";//2:支付宝返还
        cashBackDBEntity.AlipayAccount = lbl_Alipay_Port.Text;//支付宝账号
        //cashBackDBEntity.AlipayAccount = "530444816@qq.com";
        cashBackDBEntity.AlipayName = this.HidAlipayName.Value;//支付宝姓名
        //cashBackDBEntity.AlipayName = "123";//支付宝姓名
        cashBackDBEntity.Sn = this.HidSN.Value;//SN
        cashBackDBEntity.CreateUser = UserSession.Current.UserAccount;//操作人
        cashBackDBEntity.Remark = this.txtRemark.Text;

        _cashBackEntity.CashBackDBEntity.Add(cashBackDBEntity);
        _cashBackEntity = CashBackBP.autoPay(_cashBackEntity);

        _commonEntity.LogMessages = _cashBackEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();
        commonDBEntity.Event_Type = "用户提现处理--支付宝";
        commonDBEntity.Event_ID = this.HidSN.Value;
        commonDBEntity.Event_Content = "SN:" + this.HidSN.Value + "登录手机号:" + lbl_User_ID_alipay.Text + ",充值金额:" + lbl_pick_cash_amount_alipay.Text + ",支付宝账号:" + lbl_Alipay_Port.Text + ",支付宝姓名：" + HidAlipayName.Value + ",操作人:" + UserSession.Current.UserAccount + ",描述:" + txtRemark.Text;
        if (_cashBackEntity.Result == 1)
        {
            commonDBEntity.Event_Result = "操作成功,提示：" + _cashBackEntity.ErrorMSG;
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "tishi1", "alert('操作成功,原因：" + _cashBackEntity.ErrorMSG + "！');", true);
        }
        else
        {
            commonDBEntity.Event_Result = "操作失败,原因：" + _cashBackEntity.ErrorMSG;
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "tishi1", "alert('操作失败,原因：" + _cashBackEntity.ErrorMSG + "！');", true);
        }

    }

    #endregion

    /// <summary>
    /// 编辑前端可见Remark
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditProcessRemark_Click(object sender, EventArgs e)
    {
        string sql = "update T_LM_CASH set REMARK='" + this.txtUserRemark.Text + "' where SN=" + ViewState["ID"].ToString();
        try
        {
            int i = DbHelperOra.ExecuteSql(sql);
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// 修改支付宝用户名
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditAlipayName_Click(object sender, EventArgs e)
    {
        CashBackEntity _cashBackEntity = new CashBackEntity();
        _cashBackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cashBackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cashBackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cashBackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _cashBackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        CashBackDBEntity cashBackDBEntity = new CashBackDBEntity();
        cashBackDBEntity.AlipayName = this.HidAlipayName.Value;//支付宝姓名
        cashBackDBEntity.Sn = this.HidSN.Value;//SN

        _cashBackEntity.CashBackDBEntity.Add(cashBackDBEntity);
        //_cashBackEntity = CashBackBP.autoPay(_cashBackEntity);
        _cashBackEntity.Result = 1;
        _commonEntity.LogMessages = _cashBackEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();
        commonDBEntity.Event_Type = "用户提现处理--修改支付宝姓名";
        commonDBEntity.Event_ID = this.HidSN.Value;
        commonDBEntity.Event_Content = "处理备注：" + txtRemark.Text + ",支付宝姓名：" + HidAlipayName.Value + ",操作人:" + UserSession.Current.UserAccount;
        if (_cashBackEntity.Result >= 1)
        {
            this.btnOk.Text = "付款";
            this.btnOk.Enabled = true;
            this.UpdatePanel1.Update();
            commonDBEntity.Event_Result = "成功";
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "tishi1", "alert('修改成功！');", true);
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "alertClose", "BtnCompleteStyle();", true);
            BindLToCash();
        }
        else
        {
            commonDBEntity.Event_Result = "失败";
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "tishi1", "alert('修改失败！');", true);
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "alertClose", "BtnCompleteStyle();", true);
            BindLToCash();
        }
    }

    /// <summary>
    /// 获取sql中的操作历史
    /// </summary>
    /// <returns></returns>
    private DataTable GetCashBackHistoryByEventHistory()
    {
        CashBackEntity _cashBackEntity = new CashBackEntity();
        _cashBackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cashBackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cashBackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cashBackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _cashBackEntity.CashBackDBEntity = new List<CashBackDBEntity>();
        CashBackDBEntity cashBackDBEntity = new CashBackDBEntity();
        cashBackDBEntity.Sn = this.HidSN.Value;
        cashBackDBEntity.Type = "用户提现处理--修改支付宝姓名";
        _cashBackEntity.CashBackDBEntity.Add(cashBackDBEntity);
        DataTable dtResult = CashBackBP.GetCashBackHistoryByEventHistory(_cashBackEntity).QueryResult.Tables[0];
        return dtResult;
    }
}