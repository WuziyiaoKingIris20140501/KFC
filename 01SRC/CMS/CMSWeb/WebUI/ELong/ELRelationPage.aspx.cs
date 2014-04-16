using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;
using System.Collections;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class ELRelationPage : BasePage
{
    ELRelationEntity _elRelationEntity = new ELRelationEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AspNetPager1.AlwaysShow = false;
            ViewState["HVPID"] = "";
        }
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewListGrid();
    }

    private void BindReviewListGrid()
    {
        //messageContent.InnerHtml = "";
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

        usergroupEntity.HVPID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HVPID"].ToString())) ? null : ViewState["HVPID"].ToString();
        usergroupEntity.HVPLP = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HVPLP"].ToString())) ? null : ViewState["HVPLP"].ToString();

        _elRelationEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _elRelationEntity.PageSize = gridViewCSReviewList.PageSize;

        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
        DataSet dsResult = ELRelationBP.ReviewSelect(_elRelationEntity).QueryResult;

        gridViewCSReviewList.DataSource = null;
        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "HVPID", "SORC","SUPID" };
        gridViewCSReviewList.DataBind();

        //DropDownList ddl;
        //for (int i = 0; i <= gridViewCSReviewList.Rows.Count - 1; i++)
        //{
        //    DataRowView drvtemp = dsResult.Tables[0].DefaultView[i];
        //    ddl = (DropDownList)gridViewCSReviewList.Rows[i].FindControl("ddlSupDDp");
        //    if (!String.IsNullOrEmpty(drvtemp["SUPID"].ToString().Trim()))
        //    {
        //        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(drvtemp["SUPID"].ToString()));
        //    }
        //}

        AspNetPager1.PageSize = gridViewCSReviewList.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();

        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "SetAClickEvent()", true);
    }

    private int CountLmSystemLog()
    {
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

        usergroupEntity.HVPID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HVPID"].ToString())) ? null : ViewState["HVPID"].ToString();
        usergroupEntity.HVPLP = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HVPLP"].ToString())) ? null : ViewState["HVPLP"].ToString();

        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
        DataSet dsResult = ELRelationBP.ReviewSelectCount(_elRelationEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
        }

        return 0;
    }

    protected void gridViewCSReviewList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridViewCSReviewList.EditIndex = e.NewEditIndex;
        BindReviewListGrid();
        string txtSUPID = gridViewCSReviewList.DataKeys[e.NewEditIndex][2].ToString();
        DropDownList ddl = ((DropDownList)gridViewCSReviewList.Rows[e.NewEditIndex].Cells[3].FindControl("ddlSupDDp"));
        if (!String.IsNullOrEmpty(txtSUPID))
        {
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(txtSUPID));
        }
    }

    protected void gridViewCSReviewList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string txtHVPID = gridViewCSReviewList.DataKeys[e.RowIndex][0].ToString();
        string txtSupType = gridViewCSReviewList.DataKeys[e.RowIndex][2].ToString();
        string txtELID = ((TextBox)gridViewCSReviewList.Rows[e.RowIndex].FindControl("txtELIDEdit")).Text;
        string txtSORE = ((DropDownList)gridViewCSReviewList.Rows[e.RowIndex].FindControl("ddlSupDDp")).SelectedValue; ;// gridViewCSReviewList.DataKeys[e.RowIndex][1].ToString();
        //string txtHOTELNM = ((Label)gridViewCSReviewList.Rows[e.RowIndex].FindControl("lblHOTELNM")).Text;

        txtELID = (!String.IsNullOrEmpty(txtSORE) && ("HUBS1".Equals(txtSORE) || "HVP".Equals(txtSORE))) ? txtHVPID : txtELID;

        if ((!"HUBS1".Equals(txtSORE) && !"HVP".Equals(txtSORE)) && String.IsNullOrEmpty(txtELID))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
            return;
        }

        if (!String.IsNullOrEmpty(txtELID) && !System.Text.RegularExpressions.Regex.IsMatch(txtELID, @"^[0-9]*$"))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            return;
        }

        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity ELRelationDBEntity = new ELRelationDBEntity();

        ELRelationDBEntity.HVPID = txtHVPID.Trim();
        ELRelationDBEntity.ELongID = txtELID.Trim();
        ELRelationDBEntity.Source = txtSORE.Trim();
        ELRelationDBEntity.SupType = txtSupType.Trim();
        //ELRelationDBEntity.HOTELNM = txtHOTELNM.Trim();

        _elRelationEntity.ELRelationDBEntity.Add(ELRelationDBEntity);
        int iResult = ELRelationBP.UpdateELList(_elRelationEntity).Result;

        _commonEntity.LogMessages = _elRelationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "ELong酒店管理-保存";
        commonDBEntity.Event_ID = txtHVPID.Trim();
        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();

        conTent = string.Format(conTent, ELRelationDBEntity.HVPID, ELRelationDBEntity.ELongID, ELRelationDBEntity.HOTELNM, ELRelationDBEntity.Source);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            gridViewCSReviewList.EditIndex = -1;
            BindReviewListGrid();//重新绑定显示的页面
            gridViewCSReviewList.SelectedIndex = -1;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void gridViewCSReviewList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridViewCSReviewList.EditIndex = -1;
        messageContent.InnerHtml = "";
        BindReviewListGrid();
    }

    protected void btnEUpdate_Click(object sender, EventArgs e)
    {
        string txtHVPID = gridViewCSReviewList.DataKeys[gridViewCSReviewList.EditIndex][0].ToString();
        string txtELID = txtHVPID;
        string txtSORE = "0";

        if (!String.IsNullOrEmpty(txtELID) && !System.Text.RegularExpressions.Regex.IsMatch(txtELID, @"^[0-9]*$"))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            return;
        }

        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity ELRelationDBEntity = new ELRelationDBEntity();

        ELRelationDBEntity.HVPID = txtHVPID.Trim();
        ELRelationDBEntity.ELongID = txtELID.Trim();
        ELRelationDBEntity.Source = txtSORE.Trim();
        //ELRelationDBEntity.HOTELNM = txtHOTELNM.Trim();

        _elRelationEntity.ELRelationDBEntity.Add(ELRelationDBEntity);
        int iResult = ELRelationBP.UpdateELList(_elRelationEntity).Result;

        _commonEntity.LogMessages = _elRelationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "ELong酒店管理-保存";
        commonDBEntity.Event_ID = txtHVPID.Trim();
        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();

        conTent = string.Format(conTent, ELRelationDBEntity.HVPID, ELRelationDBEntity.ELongID, ELRelationDBEntity.HOTELNM, ELRelationDBEntity.Source);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            gridViewCSReviewList.EditIndex = -1;
            BindReviewListGrid();//重新绑定显示的页面
            gridViewCSReviewList.SelectedIndex = -1;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        string strHotel = hidHotel.Value.ToString().Trim();
        strHotel = (strHotel.IndexOf(']') > 0) ? strHotel.Substring(0, strHotel.IndexOf(']')).Trim('[').Trim(']') : strHotel;
        ViewState["HVPID"] = strHotel.Trim();

        if (rdbAll.Checked)
        {
            ViewState["HVPLP"] = "";
        }
        else if (rdbOn.Checked)
        {
            ViewState["HVPLP"] = "1";
        }
        else if (rdbOff.Checked)
        {
            ViewState["HVPLP"] = "0";
        }

        AspNetPager1.AlwaysShow = true;
        AspNetPager1.CurrentPageIndex = 1;
        BindReviewListGrid();
    }

    private bool ChkNumberQuery(string param)
    {
        if (String.IsNullOrEmpty(param.Trim()))
        {
            return true;
        }

        try
        {
            decimal.Parse(param);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool checkNum(string param)
    {
        bool bReturn = true;

        if (String.IsNullOrEmpty(param))
        {
            return bReturn;
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
        string strPatern = @"^([0-9]\d*)$";
        Regex reg = new Regex(strPatern);
        if (reg.IsMatch(str))
        {
            flog = true;
        }
        return flog;
    }

    protected void gridViewCSReviewUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSReviewList.PageIndex = e.NewPageIndex;
        BindReviewListGrid();
    }

    public DataSet ddlDDpbind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("SupHotelType").ToString());
        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "SUPID";
            dsResult.Tables[0].Columns["Value"].ColumnName = "SUPNM";
        }
        return dsResult;
    }
}