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

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class PaymentDetailList : BasePage
{
    PaymentEntity _paymentEntity = new PaymentEntity();
    CommonEntity _commonEntity = new CommonEntity();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string PaymentID = Request.QueryString["ID"].ToString();
                hidPaymentNo.Value = PaymentID;
                //BindChannelDDL();
                BindPaymentDetailListGrid(PaymentID);
            }
            else
            {
                detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                btnUpdate.Visible = false;
            }
        }
        else
        {
            //else
            //{
            //    BindPaymentDetailListGrid(hidPaymentNo.Value);
            //}
            detailMessageContent.InnerHtml = "";
        }

        //messageContent.InnerHtml = "";
    }

    //public DataSet ddlOnlinebind()
    //{
    //    DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
    //    DataTable dtResult = new DataTable();
    //    dtResult.Columns.Add("ONLINESTATUS");
    //    dtResult.Columns.Add("ONLINEDIS");
    //    if (dsResult.Tables.Count > 0)
    //    {
    //        dsResult.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
    //        dsResult.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";
    //    }
    //    return dsResult;
    //}


    ////private void BindChannelDDL()
    ////{
    ////    _paymentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    ////    _paymentEntity.LogMessages.Userid = "test";
    ////    _paymentEntity.LogMessages.Username = "test";

    ////    DataSet dsResult = PaymentBP.CommonSelect(_paymentEntity).QueryResult;
    ////    ddpChannelList.DataTextField = "DISPLAYNM";
    ////    ddpChannelList.DataValueField = "ID";
    ////    ddpChannelList.DataSource = dsResult;
    ////    ddpChannelList.DataBind();
    ////}

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //btnAddChannel();
        //BindPaymentListGrid();
    }

    ////清除控件中的数据
    //private void clearValue()
    //{
    //}

    ////发放渠道
    private void BindPaymentDetailListGrid(string PaymentID)
    {
        _paymentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _paymentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _paymentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _paymentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _paymentEntity.PaymentDBEntity = new List<PaymentDBEntity>();
        PaymentDBEntity paymentDBEntity = new PaymentDBEntity();

        paymentDBEntity.PaymentNo = PaymentID;

    //    //if (chkAll.Checked)
    //    //{
    //    //    paymentDBEntity.OnlineStatus = null;
    //    //}
    //    //else if (chkOnL.Checked && chkOff.Checked)
    //    //{
    //    //    paymentDBEntity.OnlineStatus = null;
    //    //}
    //    //else if (chkOff.Checked)
    //    //{
    //    //    paymentDBEntity.OnlineStatus = "0";
    //    //}
    //    //else if (chkOnL.Checked)
    //    //{
    //    //    paymentDBEntity.OnlineStatus = "1";
    //    //}
    //    //else
    //    //{
    //    //    paymentDBEntity.OnlineStatus = null;
    //    //}

    //    if (chkUnTime.Checked)
    //    {
    //        paymentDBEntity.StartDTime = null;
    //        paymentDBEntity.EndDTime = null;
    //    }
    //    else
    //    {
    //        paymentDBEntity.StartDTime = dpStart.Value;
    //        paymentDBEntity.EndDTime = dpEnd.Value;
    //    }

        _paymentEntity.PaymentDBEntity.Add(paymentDBEntity);

        DataSet dsResult = PaymentBP.PlatFormSelect(_paymentEntity).QueryResult;

        DataSet dsMainResult = PaymentBP.Select(_paymentEntity).QueryResult;
        if (dsMainResult.Tables.Count > 0 && dsMainResult.Tables[0].Rows.Count > 0)
        {
            txtPaymentName.Value = dsMainResult.Tables[0].Rows[0]["paymentnm"].ToString();
            txtPaymentID.Value = dsMainResult.Tables[0].Rows[0]["paymentcode"].ToString();
        }

        gridViewCSPaymentDetailList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSPaymentDetailList.DataKeyNames = new string[] { "PLATMENTID" };//主键
        gridViewCSPaymentDetailList.DataBind();

        CheckBox chk;
        for (int i = 0; i <= gridViewCSPaymentDetailList.Rows.Count - 1; i++)
        {
            DataRowView drvtemp = dsResult.Tables[0].DefaultView[i];
            chk = (CheckBox)gridViewCSPaymentDetailList.Rows[i].FindControl("chkOnline");
            chk.Checked = (drvtemp["ONLINESTATUS"].ToString().Equals("1")) ? true : false;
        }
    }

    protected void gridViewCSPaymentDetailList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        //for (int i = 0; i <= gridViewCSPaymentList.Rows.Count; i++)
        //{
        //    //首先判断是否是数据行
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //当鼠标停留时更改背景色
        //        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
        //        //当鼠标移开时还原背景色
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //    }
        //}
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    BindPaymentListGrid();
    //}

    protected void gridViewCSPaymentDetailList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gridViewCSPaymentList.EditIndex = e.NewEditIndex;
        //BindPaymentListGrid();
        //((DropDownList)gridViewCSPaymentList.Rows[e.NewEditIndex].Cells[7].FindControl("ddlOnline")).Enabled = true;
    }

    protected void gridViewCSPaymentDetailList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string channelNo = gridViewCSPaymentList.DataKeys[e.RowIndex].Value.ToString();
        //string channelID = gridViewCSPaymentList.Rows[e.RowIndex].Cells[3].Text;
        //string nameCN = ((TextBox)(gridViewCSPaymentList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        ////string onlineStatus = ((DropDownList)gridViewCSPaymentList.Rows[e.RowIndex].Cells[7].FindControl("ddlOnline")).SelectedValue;
        //if (String.IsNullOrEmpty(nameCN))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
        //    return;
        //}
        //btnUpdateChannel(channelNo, channelID, nameCN, "");
        //gridViewCSPaymentList.EditIndex = -1;
        //BindPaymentListGrid();
    }

    protected void gridViewCSPaymentDetailList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //gridViewCSPaymentList.EditIndex = -1;
        //BindPaymentListGrid();
    }

    //public void btnAddChannel()
    //{
    //    messageContent.InnerHtml = "";

    //    if (String.IsNullOrEmpty(txtPaymentName.Value) || String.IsNullOrEmpty(txtPaymentID.Value))
    //    {
    //        messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
    //        return;
    //    }

    //    _paymentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _paymentEntity.LogMessages.Userid = "test";
    //    _paymentEntity.LogMessages.Username = "test";

    //    _paymentEntity.PaymentDBEntity = new List<PaymentDBEntity>();
    //    PaymentDBEntity paymentDBEntity = new PaymentDBEntity();
    //    paymentDBEntity.Name_CN = txtPaymentName.Value;
    //    paymentDBEntity.PaymentID = txtPaymentID.Value;
    //    _paymentEntity.PaymentDBEntity.Add(paymentDBEntity);
    //    int iResult = PaymentBP.Insert(_paymentEntity);

    //    _commonEntity.LogMessages = _paymentEntity.LogMessages;
    //    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
    //    CommonDBEntity commonDBEntity = new CommonDBEntity();

    //    commonDBEntity.Event_Type = "";
    //    commonDBEntity.Event_ID = "";



    //    string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
    //    conTent = string.Format(conTent, txtPaymentID.Value, txtPaymentName.Value);
    //    commonDBEntity.Event_Content = conTent;

    //    if (iResult == 1)
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
    //    }
    //    else if (iResult == 2)
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
    //    }
    //    else
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
    //    }

    //    _commonEntity.CommonDBEntity.Add(commonDBEntity);
    //    CommonBP.InsertEventHistory(_commonEntity);
    //}

    public void btnUpdateChannel(string paymentNo, string paymentID, string nameCN, string onlineStatus)
    {
        //messageContent.InnerHtml = "";

        //_paymentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        //_paymentEntity.LogMessages.Userid = "test";
        //_paymentEntity.LogMessages.Username = "test";

        //_paymentEntity.PaymentDBEntity = new List<PaymentDBEntity>();
        //PaymentDBEntity paymentDBEntity = new PaymentDBEntity();
        //paymentDBEntity.PaymentNo = paymentNo;
        //paymentDBEntity.PaymentID = paymentID;
        //paymentDBEntity.Name_CN = nameCN;
        //paymentDBEntity.OnlineStatus = onlineStatus;
        //_paymentEntity.PaymentDBEntity.Add(paymentDBEntity);
        //int iResult = PaymentBP.Update(_paymentEntity);

        //_commonEntity.LogMessages = _paymentEntity.LogMessages;
        //_commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        //CommonDBEntity commonDBEntity = new CommonDBEntity();

        //commonDBEntity.Event_Type = "";
        //commonDBEntity.Event_ID = "";

        //string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        //conTent = string.Format(conTent, paymentDBEntity.PaymentID, paymentDBEntity.Name_CN, paymentDBEntity.OnlineStatus);
        //commonDBEntity.Event_Content = conTent;

        //if (iResult == 1)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        //}
        //else
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        //}

        //_commonEntity.CommonDBEntity.Add(commonDBEntity);
        //CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //((DropDownList)gridViewCSPaymentList.Rows[2].Cells[0].FindControl("ddlOnline")).SelectedValue

        detailMessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtPaymentName.Value.ToString().Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString(); 
            return;
        }

        if (txtPaymentName.Value.ToString().Trim().Length > 32)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError3").ToString();
            return;
        }

        _paymentEntity.PaymentDBEntity = new List<PaymentDBEntity>();

        _paymentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _paymentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _paymentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _paymentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _paymentEntity.PaymentDBEntity = new List<PaymentDBEntity>();

        PaymentDBEntity paymentMainDBEntity = new PaymentDBEntity();
        paymentMainDBEntity.PaymentNo = hidPaymentNo.Value;
        paymentMainDBEntity.Name_CN = txtPaymentName.Value;
        paymentMainDBEntity.UpdatType = "1";
        _paymentEntity.PaymentDBEntity.Add(paymentMainDBEntity);
        string strPayformIDS = "";
        for (int i = 0; i <= gridViewCSPaymentDetailList.Rows.Count - 1; i++)
        {
            PaymentDBEntity paymentDBEntity = new PaymentDBEntity();
            paymentDBEntity.PaymentNo = gridViewCSPaymentDetailList.Rows[i].Cells[0].Text;
            paymentDBEntity.Name_CN = txtPaymentName.Value;
            paymentDBEntity.PaymentID = gridViewCSPaymentDetailList.Rows[i].Cells[2].Text;// gridViewCSPaymentDetailList.DataKeys[i].Value.ToString(); //hidPaymentNo.Value;
            paymentDBEntity.PaymFormID = gridViewCSPaymentDetailList.DataKeys[i].Value.ToString();
            paymentDBEntity.OnlineStatus = (((CheckBox)gridViewCSPaymentDetailList.Rows[i].Cells[7].FindControl("chkOnline")).Checked) ? "1" : "0";
            paymentDBEntity.UpdatType = "0";

            strPayformIDS = strPayformIDS + paymentDBEntity.PaymFormID + "（"+ (paymentDBEntity.OnlineStatus.Equals("1") ? "上线" : "下线") +"） ";

            _paymentEntity.PaymentDBEntity.Add(paymentDBEntity);
        }

        int iResult = PaymentBP.Update(_paymentEntity);
        _commonEntity.LogMessages = _paymentEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        //detailMessageContent.InnerHtml = "支付方式更新成功！";
        string conTent = "";
        conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, hidPaymentNo.Value, txtPaymentName.Value, strPayformIDS);
        commonDBEntity.Event_Type = "支付方式管理-更新";
        commonDBEntity.Event_ID = hidPaymentNo.Value;
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else if (iResult == 2)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError2").ToString();
        }
        else
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
        }

        //Response.Write("<script>window.returnValue=false;window.opener='anyone';window.close();</script>");


        //string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
        //conTent = string.Format(conTent, paymentDBEntity.PaymentID, paymentDBEntity.Name_CN, paymentDBEntity.OnlineStatus);
        //commonDBEntity.Event_Content = conTent;

        //if (iResult == 1)
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        //}
        //else
        //{
        //    commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
        //    messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        //}

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        //messageContent.InnerHtml = "渠道添加失败！";
    }
}