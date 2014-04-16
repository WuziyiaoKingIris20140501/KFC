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

public partial class HotelFacilitiesSearch : BasePage
{
    HotelFacilitiesEntity _hotelfacilitiesEntity = new HotelFacilitiesEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTypeDDL();
            ViewState["SELFTTYPE"] = ddpSelFtTypeList.SelectedValue;
            BindListGrid();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["SELFTTYPE"] = ddpSelFtTypeList.SelectedValue;
        MessageContent.InnerHtml = "";
        btnModifySeq.Visible = true;
        dvUpdateSeq.Style.Add("display", "none");

        BindListGrid();
    }

    private void BindTypeDDL()
    {
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        DataSet dsResult = HotelFacilitiesBP.CommonFtTypeSelect(_hotelfacilitiesEntity).QueryResult;
        ddpFtTypeList.DataTextField = "TYPENAME";
        ddpFtTypeList.DataValueField = "TYPECODE";
        ddpFtTypeList.DataSource = dsResult;
        ddpFtTypeList.DataBind();

        ddpSelFtTypeList.DataTextField = "TYPENAME";
        ddpSelFtTypeList.DataValueField = "TYPECODE";
        ddpSelFtTypeList.DataSource = dsResult;
        ddpSelFtTypeList.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        btnAddData();
        BindListGrid();
        //refushFlag.Value = "";
        if (gridViewCSServiceList.Rows.Count > 0)
        {
            btnModifySeq.Visible = true;
            dvUpdateSeq.Style.Add("display", "none");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        lbFtSeq.Text = GetFtTypeMaxSeq();
        //refushFlag.Value = "";
        //if (gridViewCSServiceList.Rows.Count > 0)
        //{
        //    btnModifySeq.Visible = true;
        //    dvUpdateSeq.Style.Add("display", "none");
        //}
    }

    protected void btnModifySeq_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        if (gridViewCSServiceList.Rows.Count > 0)
        {
            btnModifySeq.Visible = false;
            dvUpdateSeq.Style.Add("display", "");

            //gridViewCSList.Columns[3].Visible = false;
            //gridViewCSList.Columns[4].Visible = true;

            for (int i = 0; i < gridViewCSServiceList.Rows.Count; i++)
            {
                //首先判断是否是数据行
                if (gridViewCSServiceList.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    TextBox tbBox = (TextBox)gridViewCSServiceList.Rows[i].FindControl("txtSeqList");
                    tbBox.Enabled = true;
                }
            }
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

    protected void btnUpdateSeq_Click(object sender, EventArgs e)
    {
        Hashtable htList = new Hashtable();

        for (int i = 0; i < gridViewCSServiceList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridViewCSServiceList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                string FtID = gridViewCSServiceList.Rows[i].Cells[0].Text.ToString();
                TextBox tbBox = (TextBox)gridViewCSServiceList.Rows[i].FindControl("txtSeqList");

                if (!checkNum(tbBox.Text.Trim()))
                {
                    MessageContent.InnerHtml = GetLocalResourceObject("UpdateError1").ToString();
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
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError2").ToString();
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

        int iResult = HotelFacilitiesBP.FtSeqListUpdate(_hotelfacilitiesEntity);
        _commonEntity.LogMessages = _hotelfacilitiesEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店服务设施管理-修改排序";
        commonDBEntity.Event_ID = "";
        string conTent = "";
        conTent = GetLocalResourceObject("EventUpdateMessageSeq").ToString();
        string strConTemp = "";
        if (iResult == 1)
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccessSeq").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccessSeq").ToString();
            BindListGrid();
        }
        else
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
        }

        foreach (System.Collections.DictionaryEntry key in htList)
        {
            strConTemp = string.Format(conTent, key.Key, key.Value);
            commonDBEntity.Event_Content = strConTemp;
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }
    }

    protected void btnUpdateCal_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        btnModifySeq.Visible = true;
        dvUpdateSeq.Style.Add("display", "none");
        BindListGrid();
    }
 
    public void BindListGrid()
    {
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        hotelFacilitiesDBEntity.Type = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SELFTTYPE"].ToString())) ? null : ViewState["SELFTTYPE"].ToString();
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);

        DataSet dsResult = HotelFacilitiesBP.ServiceTypeSelect(_hotelfacilitiesEntity).QueryResult;

        gridViewCSServiceList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSServiceList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSServiceList.DataBind();

        if (!String.IsNullOrEmpty(refushFlag.Value))
        {
            MessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            refushFlag.Value = "";
        }

        lbFtSeq.Text = GetFtTypeMaxSeq();
        btnModifySeq.Visible = true;
        dvUpdateSeq.Style.Add("display", "none");
    }

    private string GetFtTypeMaxSeq()
    {
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelfacilitiesDBEntity = new HotelFacilitiesDBEntity();
        hotelfacilitiesDBEntity.Type = ddpFtTypeList.SelectedValue;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelfacilitiesDBEntity);

        DataSet dsResult = HotelFacilitiesBP.GetFtHotelMaxSeq(_hotelfacilitiesEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            return dsResult.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            return "1";
        }
    }

    protected void gridViewCSServiceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSServiceList.Rows.Count; i++)
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

    public void btnAddData()
    {
        MessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtServiceName.Value.ToString().Trim()))
        {
            MessageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            return;
        }

        if (String.IsNullOrEmpty(ddpFtTypeList.SelectedValue))
        {
            MessageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
            return;
        }

        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        hotelFacilitiesDBEntity.Name_CN = txtServiceName.Value;
        hotelFacilitiesDBEntity.Type = ddpFtTypeList.SelectedValue;
        hotelFacilitiesDBEntity.FTSeq = lbFtSeq.Text;
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);
        int iResult = HotelFacilitiesBP.Insert(_hotelfacilitiesEntity);

        _commonEntity.LogMessages = _hotelfacilitiesEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店服务设施管理-添加";
        commonDBEntity.Event_ID = txtServiceName.Value;
        string conTent = "";
        conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, txtServiceName.Value, ddpFtTypeList.SelectedValue);

        commonDBEntity.Event_Content = conTent;
        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();

        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void ddpFtTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbFtSeq.Text = GetFtTypeMaxSeq();
    }
}