using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;
using System.Collections;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class FacilitiesTypeSearchPage : BasePage
{
    HotelFacilitiesEntity _hotelfacilitiesEntity = new HotelFacilitiesEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            messageContent.InnerHtml = "";
            BindFtListGrid();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAddFt();
        BindFtListGrid();

        if (gridViewCSList.Rows.Count > 0)
        {
            btnModifySeq.Visible = true;
            dvUpdateSeq.Style.Add("display", "none");
        }
    }

    protected void btnModifySeq_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        if (gridViewCSList.Rows.Count > 0)
        {
            btnModifySeq.Visible = false;
            dvUpdateSeq.Style.Add("display", "");

            //gridViewCSList.Columns[3].Visible = false;
            //gridViewCSList.Columns[4].Visible = true;

            for (int i = 0; i < gridViewCSList.Rows.Count; i++)
            {
                //首先判断是否是数据行
                if (gridViewCSList.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    TextBox tbBox = (TextBox)gridViewCSList.Rows[i].FindControl("txtSeqList");
                    tbBox.Enabled = true;
                }
            }
        }        
    }

    protected void btnUpdateSeq_Click(object sender, EventArgs e)
    {
        Hashtable htList = new Hashtable();

        for (int i = 0; i < gridViewCSList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridViewCSList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                string FtID = gridViewCSList.Rows[i].Cells[0].Text.ToString();
                TextBox tbBox = (TextBox)gridViewCSList.Rows[i].FindControl("txtSeqList");

                if (!checkNum(tbBox.Text.Trim()))
                {
                    messageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString();
                    return;
                }

                htList.Add(FtID, tbBox.Text);
            }
        }

        Hashtable hNew = new Hashtable();
        foreach (System.Collections.DictionaryEntry key in htList)
        {
            if (!hNew.ContainsValue(key.Value)) hNew.Add(key.Key, key.Value);
        }
        
        if (hNew.Values.Count != htList.Values.Count)
        {
            messageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
            return;
        }


        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();

        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();

        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        hotelFacilitiesDBEntity.FTSeqList = htList;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);

        int iResult = HotelFacilitiesBP.FtTypeSeqListUpdate(_hotelfacilitiesEntity);
        _commonEntity.LogMessages = _hotelfacilitiesEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "服务设施类别管理-排序修改";
        commonDBEntity.Event_ID = "";
        string conTent = "";
        conTent = GetLocalResourceObject("EventUpdateMessageSeq").ToString();
        //conTent = string.Format(conTent, txtFtCode.Value, txtFtName.Value, lbFtSeq.Text, ddpStatusList.SelectedValue);
        commonDBEntity.Event_Type = "";
        commonDBEntity.Event_ID = "";
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccessSeq").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccessSeq").ToString();
            BindFtListGrid();
        }
        else
        {
            messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
        }

        foreach (System.Collections.DictionaryEntry key in htList)
        {
            conTent = string.Format(conTent, key.Key, key.Value);
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }
    }

    private bool checkNum(string param)
    {
        bool bReturn = true;

        if (String.IsNullOrEmpty(param))
        {
            return false;
        }

        try
        {
            return IsVali(param);
        }
        catch
        {
            bReturn = false;
        }

        return bReturn;
    }

    public bool IsVali(string str)
    {
        bool flog = false;
        string strPatern = @"^([1-9]\d*)$";
        Regex reg = new Regex(strPatern);
        if (reg.IsMatch(str))
        {
            flog = true;
        }
        return flog;
    }

    protected void btnUpdateCal_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        btnModifySeq.Visible = true;
        dvUpdateSeq.Style.Add("display", "none");
        BindFtListGrid();
    }

    //清除控件中的数据
    private void clearValue()
    {
    }

    public void BindFtListGrid()
    {
        //messageContent.InnerHtml = "";
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelfacilitiesDBEntity = new HotelFacilitiesDBEntity();
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelfacilitiesDBEntity);


        DataSet dsResult = HotelFacilitiesBP.FtTypeSelect(_hotelfacilitiesEntity).QueryResult;

        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "FTID" };//主键
        gridViewCSList.DataBind();

        //DropDownList ddl;
        //for (int i = 0; i <= gridViewCSPaymentList.Rows.Count - 1; i++)
        //{
        //    DataRowView drvtemp = dsResult.Tables[0].DefaultView[i];
        //    ddl = (DropDownList)gridViewCSPaymentList.Rows[i].FindControl("ddlOnline");
        //    ddl.SelectedValue = drvtemp["ONLINESTATUS"].ToString();
        //}

        if (!String.IsNullOrEmpty(refushFlag.Value))
        {
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            refushFlag.Value = "";
        }

        lbFtSeq.Text = GetFtTypeMaxSeq();

        if (gridViewCSList.Rows.Count > 0)
        {
            btnModifySeq.Visible = true;
            dvUpdateSeq.Style.Add("display", "none");
            //gridViewCSList.Columns[3].Visible = true;
            //gridViewCSList.Columns[4].Visible = false;

            for (int i = 0; i < gridViewCSList.Rows.Count; i++)
            {
                //首先判断是否是数据行
                if (gridViewCSList.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    TextBox tbBox = (TextBox)gridViewCSList.Rows[i].FindControl("txtSeqList");
                    tbBox.Enabled = false;
                }
            }
        }
    }

    private string GetFtTypeMaxSeq()
    {
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelfacilitiesDBEntity = new HotelFacilitiesDBEntity();

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelfacilitiesDBEntity);

        DataSet dsResult = HotelFacilitiesBP.GetFtTypeMaxSeq(_hotelfacilitiesEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            return dsResult.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            return "1";
        }
    }

    protected void gridViewCSList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        BindFtListGrid();
    }

    protected void gridViewCSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSList.PageIndex = e.NewPageIndex;
        messageContent.InnerHtml = "";
        BindFtListGrid();
    }

    public void btnAddFt()
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtFtName.Value.ToString().Trim()) || String.IsNullOrEmpty(txtFtCode.Value.ToString().Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelfacilitiesDBEntity = new HotelFacilitiesDBEntity();
        hotelfacilitiesDBEntity.FTName = txtFtName.Value.Trim();
        hotelfacilitiesDBEntity.FTCode = txtFtCode.Value.Trim();
        hotelfacilitiesDBEntity.FTSeq = lbFtSeq.Text;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelfacilitiesDBEntity);
        int iResult = HotelFacilitiesBP.FtTypeInsert(_hotelfacilitiesEntity);

        _commonEntity.LogMessages = _hotelfacilitiesEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "服务设施类别管理-添加";
        commonDBEntity.Event_ID = "";

        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtFtCode.Value, txtFtName.Value);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
        }

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
}