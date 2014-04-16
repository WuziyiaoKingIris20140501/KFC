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

public partial class HotelSupMappingPage : BasePage
{
    ELRelationEntity _elRelationEntity = new ELRelationEntity();
    CommonEntity _commonEntity = new CommonEntity();
    public DataSet dsRoomList;
    public DataSet ddpRoomList;
    public DataSet dsMappingRoomList;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type=hotel") && !this.Page.Request.QueryString.ToString().Contains("Type=sales"))
        {
            AspNetPager1.AlwaysShow = false;
            ViewState["HVPID"] = "";

            //DataSet dsDdp = ddlDDpbind();
            //ddlSupHDDp.DataTextField = "SUPNM";
            //ddlSupHDDp.DataValueField = "SUPID";
            //ddlSupHDDp.DataSource = dsDdp;
            //ddlSupHDDp.DataBind();

            //ddpSupRDDP.DataTextField = "SUPNM";
            //ddpSupRDDP.DataValueField = "SUPID";
            //ddpSupRDDP.DataSource = dsDdp;
            //ddpSupRDDP.DataBind();
        }
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

    public DataSet ddlDDpRoombind()
    {
        return (DataSet)ViewState["ddpRoomList"];//ddpRoomList;
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewListGrid();
    }

    private void BindReviewListGrid()
    {
        //messageContent.InnerHtml = "";
        hidMsg.Value = "";
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

        usergroupEntity.HVPID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HVPID"].ToString())) ? null : ViewState["HVPID"].ToString();
        usergroupEntity.HVPLP = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HVPLP"].ToString())) ? null : ViewState["HVPLP"].ToString();
        usergroupEntity.HVPRP = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HVPRP"].ToString())) ? null : ViewState["HVPRP"].ToString();
        usergroupEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SALES"].ToString())) ? null : ViewState["SALES"].ToString();

        _elRelationEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _elRelationEntity.PageSize = gridViewCSReviewList.PageSize;

        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
        DataSet dsResult = ELRelationBP.ReviewSupHotelMappingSelect(_elRelationEntity).QueryResult;

        gridViewCSReviewList.DataSource = null;
        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] {"HVPID"};
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
        usergroupEntity.HVPRP = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HVPRP"].ToString())) ? null : ViewState["HVPRP"].ToString();

        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
        DataSet dsResult = ELRelationBP.ReviewSupHotelMappingSelectCount(_elRelationEntity).QueryResult;

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

        txtELID = (!String.IsNullOrEmpty(txtSORE) && ("HUBS1".Equals(txtSORE) || "HVP".Equals(txtSORE)) && String.IsNullOrEmpty(txtELID)) ? txtHVPID : txtELID;

        if ((!"HUBS1".Equals(txtSORE) && !"HVP".Equals(txtSORE)) && String.IsNullOrEmpty(txtELID))
        {
            hidMsg.Value = GetLocalResourceObject("Error2").ToString();
            return;
        }

        //if (!String.IsNullOrEmpty(txtELID))
        //{
        //    hidMsg.Value = GetLocalResourceObject("Error1").ToString();
        //    return;
        //}

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
            //messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            gridViewCSReviewList.EditIndex = -1;
            BindReviewListGrid();//重新绑定显示的页面
            gridViewCSReviewList.SelectedIndex = -1;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            hidMsg.Value = GetLocalResourceObject("UpdateError").ToString();
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
            hidMsg.Value = GetLocalResourceObject("Error1").ToString();
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
            //messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            gridViewCSReviewList.EditIndex = -1;
            BindReviewListGrid();//重新绑定显示的页面
            gridViewCSReviewList.SelectedIndex = -1;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            hidMsg.Value = GetLocalResourceObject("UpdateError").ToString();
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

        if (rdbRoomAll.Checked)
        {
            ViewState["HVPRP"] = "";
        }
        else if (rdbRoomOn.Checked)
        {
            ViewState["HVPRP"] = "1";
        }
        else if (rdbRoomOff.Checked)
        {
            ViewState["HVPRP"] = "0";
        }
        else if (rdbRoomOffAll.Checked)
        {
            ViewState["HVPRP"] = "2";
        }

        string strSales = hidSales.Value.ToString().Trim();
        strSales = (strSales.IndexOf(']') > 0) ? strSales.Substring(0, strSales.IndexOf(']')).Trim('[').Trim(']') : strSales;
        ViewState["SALES"] = strSales.Trim();

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

    protected void gridViewCSReviewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("OnClick", "PopupArea('" + Server.UrlEncode(e.Row.Cells[0].Text) + "','"+ Server.UrlEncode(e.Row.Cells[1].Text) + "')");
            }

            //当鼠标停留时更改背景色
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
            //当鼠标移开时还原背景色
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        lbHotelNM.Text = "[" + hidRowID.Value + "]" + Server.UrlDecode(hidHLNM.Value);
        gridViewCSList.EditIndex = -1;
        LoadHotelSupDetail("0");
        LoadRoomSupDetail();

        SetHotelRoomMappingList(hidRowID.Value);
    }

    //[WebMethod]
    //public static void SetHotelRoomMappingList(string HidRowID)
    //{
    //    ddpRoomList = new DataSet();
    //    ddpRoomList.Tables.Add(new DataTable());
    //    ddpRoomList.Tables[0].Columns.Add("SUPID");
    //    ddpRoomList.Tables[0].Columns.Add("SUPNM");

    //    dsMappingRoomList = new DataSet();
    //    dsMappingRoomList.Tables.Add(new DataTable());
    //    dsMappingRoomList.Tables[0].Columns.Add("ROOMCD");
    //    dsMappingRoomList.Tables[0].Columns.Add("ROOMNM");
    //    dsMappingRoomList.Tables[0].Columns.Add("SOURCES");

    //    ELRelationEntity _elRelationEntity = new ELRelationEntity();
    //    _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

    //    _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
    //    ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

    //    usergroupEntity.HVPID = HidRowID;
    //    _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
    //    DataSet dsResult = ELRelationBP.SaSupHotelMappingDetail(_elRelationEntity).QueryResult;

    //    for (int i = 0; i < dsResult.Tables["Master"].Rows.Count; i++)
    //    {
    //        DataRow dr = ddpRoomList.Tables[0].NewRow();
    //        dr["SUPNM"] = dsResult.Tables["Master"].Rows[i]["SUPNM"].ToString();
    //        dr["SUPID"] = dsResult.Tables["Master"].Rows[i]["SOURCES"].ToString();
    //        ddpRoomList.Tables[0].Rows.Add(dr);
    //    }

    //    if (dsResult.Tables.Count > 1 && dsResult.Tables["Detail"].Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dsResult.Tables["Detail"].Rows.Count; i++)
    //        {
    //            DataRow dr = dsMappingRoomList.Tables[0].NewRow();
    //            dr["ROOMCD"] = dsResult.Tables["Detail"].Rows[i]["ROOMCD"].ToString();
    //            dr["ROOMNM"] = dsResult.Tables["Detail"].Rows[i]["ROOMNM"].ToString();
    //            dr["SOURCES"] = dsResult.Tables["Detail"].Rows[i]["SOURCES"].ToString();
    //            dsMappingRoomList.Tables[0].Rows.Add(dr);
    //        }
    //    }
    //}

    public void SetHotelRoomMappingList(string HidRowID)
    {
        ddpRoomList = new DataSet();
        ddpRoomList.Tables.Add(new DataTable());
        ddpRoomList.Tables[0].Columns.Add("SUPID");
        ddpRoomList.Tables[0].Columns.Add("SUPNM");

        dsMappingRoomList = new DataSet();
        dsMappingRoomList.Tables.Add(new DataTable());
        dsMappingRoomList.Tables[0].Columns.Add("ROOMCD");
        dsMappingRoomList.Tables[0].Columns.Add("ROOMNM");
        dsMappingRoomList.Tables[0].Columns.Add("SOURCES");

        ELRelationEntity _elRelationEntity = new ELRelationEntity();
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

        usergroupEntity.HVPID = HidRowID;
        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
        DataSet dsResult = ELRelationBP.SaSupHotelMappingDetail(_elRelationEntity).QueryResult;

        for (int i = 0; i < dsResult.Tables["Master"].Rows.Count; i++)
        {
            DataRow dr = ddpRoomList.Tables[0].NewRow();
            dr["SUPNM"] = dsResult.Tables["Master"].Rows[i]["SUPNM"].ToString();
            dr["SUPID"] = dsResult.Tables["Master"].Rows[i]["SOURCES"].ToString();
            ddpRoomList.Tables[0].Rows.Add(dr);
        }

        if (dsResult.Tables.Count > 1 && dsResult.Tables["Detail"].Rows.Count > 0)
        {
            for (int i = 0; i < dsResult.Tables["Detail"].Rows.Count; i++)
            {
                DataRow dr = dsMappingRoomList.Tables[0].NewRow();
                dr["ROOMCD"] = dsResult.Tables["Detail"].Rows[i]["ROOMCD"].ToString();
                dr["ROOMNM"] = dsResult.Tables["Detail"].Rows[i]["ROOMNM"].ToString();
                dr["SOURCES"] = dsResult.Tables["Detail"].Rows[i]["SOURCES"].ToString();
                dsMappingRoomList.Tables[0].Rows.Add(dr);
            }
        }

        ViewState["ddpRoomList"] = ddpRoomList;
        ViewState["dsMappingRoomList"] = dsMappingRoomList;
    }

    private void LoadRoomSupDetail()
    {
        hidMsg.Value = "";
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

        usergroupEntity.HVPID = hidRowID.Value;
        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
        DataSet dsResult = ELRelationBP.ReviewSupHotelRoomMappingDetail(_elRelationEntity).QueryResult;
        //dsRoomList = dsResult;
        ViewState["dsRoomList"] = dsResult;
        int iCsr = 0;
        HtmlTableRow tr = new HtmlTableRow();
        HtmlTableCell td = new HtmlTableCell();

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                tr = new HtmlTableRow();
                tr.Style.Add("cursor", "pointer");
                tr.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor ='#E4E4E4'");
                tr.Attributes.Add("onmouseout", "this.style.backgroundColor = c");
                td = new HtmlTableCell();
                iCsr = int.Parse(dsResult.Tables[0].Rows[i]["CSR"].ToString());
                td.Attributes.Add("onclick", "LaodRoomInfo('" + dsResult.Tables[0].Rows[i]["room_code"].ToString() + "')");
                td.InnerHtml = "<div style='float:left;'><img src='" + ((iCsr > 0) ? "../../Styles/images/star.png" : "../../Styles/images/hstar.png") + "' alt='' /></div><div style='float:left;margin-top:5px'>&nbsp;" + dsResult.Tables[0].Rows[i]["ROOMNM"].ToString() + (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString()) ? "" : "（" + dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString() + "元/间）") + "</div>";
                tr.Cells.Add(td);
                tbRoomBand.Rows.Add(tr);
            }
        }
        else
        {
            tr = new HtmlTableRow();
            tr.Style.Add("cursor", "pointer");
            td = new HtmlTableCell();
            td.InnerHtml = "该酒店未设置房型";
            tr.Cells.Add(td);
            tbRoomBand.Rows.Add(tr);
        }
        gridViewRoom.EmptyDataText = "";
        gridViewRoom.DataSource = null;
        gridViewRoom.DataBind();
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    private void LoadHotelSupDetail(string strType)
    {
        hidMsg.Value = "";
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

        usergroupEntity.HVPID = hidRowID.Value;
        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
        DataSet dsResult = ELRelationBP.ReviewSupHotelMappingDetail(_elRelationEntity).QueryResult;

        if ("1".Equals(strType))
        {
            DataRow drInsert = dsResult.Tables[0].NewRow();
            drInsert["SUPID"] = "";
            drInsert["INUSE"] = "1";
            drInsert["SORC"] = "供应商：";
            drInsert["SORCID"] = "供应商ID：";
            drInsert["INUSERT"] = "状态：";
            drInsert["SOURCES"] = "ELONG";
            drInsert["SUPNM"] = "艺龙";
            drInsert["INUSERDIS"] = "上线";
            drInsert["RTYPE"] = "1";
            drInsert["OSUPID"] = "";
            dsResult.Tables[0].Rows.Add(drInsert);
        }

        gridViewCSList.DataSource = null;
        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "SUPID", "INUSE", "SOURCES", "RTYPE","OSUPID" };
        gridViewCSList.DataBind();

        UpdatePanel2.Update();
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    protected void btnAddHotelMapping_Click(object sender, EventArgs e)
    {
        //ReFreshgridViewCSList();
        //gridViewCSList.CssClass = "GView_BodyCSS2";
        hidMsg.Value = "";
        int iEdinIndex = 0;

        if (gridViewCSList.Rows.Count > 0)
        {
            iEdinIndex = ("0".Equals(gridViewCSList.DataKeys[gridViewCSList.Rows.Count - 1][3].ToString())) ? gridViewCSList.Rows.Count : gridViewCSList.Rows.Count - 1;
        }

        gridViewCSList.EditIndex = iEdinIndex;
        LoadHotelSupDetail("1");
        //LoadRoomSupDetail("0");
        ReLoadRoomInfoList();
        string chkINUSE = gridViewCSList.DataKeys[gridViewCSList.EditIndex][1].ToString();
        DropDownList ddl = ((DropDownList)gridViewCSList.Rows[gridViewCSList.EditIndex].Cells[3].FindControl("ddlSupDp"));
        CheckBox chk = ((CheckBox)gridViewCSList.Rows[gridViewCSList.EditIndex].Cells[5].FindControl("chkInUse"));
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue("ELONG"));
        chk.Checked = ("1".Equals(chkINUSE)) ? true : false;
        //ScriptManager.RegisterStartupScript(this.UpdatePanel10, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    private void ReFreshgridViewCSList()
    {
        hidMsg.Value = "";
        gridViewCSList.EditIndex = gridViewCSList.Rows.Count + 1;

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("SUPID");
        dtResult.Columns.Add("INUSE");
        dtResult.Columns.Add("SORC");
        dtResult.Columns.Add("SORCID");
        dtResult.Columns.Add("INUSERT");
        dtResult.Columns.Add("SOURCES");
        dtResult.Columns.Add("SUPNM");
        dtResult.Columns.Add("INUSERDIS");
        dtResult.Columns.Add("RTYPE");
        dtResult.Columns.Add("OSUPID");

        for (int i = 0; i < gridViewCSList.Rows.Count; i++)
        {
            DataRow dr = dtResult.NewRow();
            dr["SUPID"] = gridViewCSList.DataKeys[i][0].ToString();
            dr["INUSE"] = gridViewCSList.DataKeys[i][1].ToString();
            dr["SORC"] = "供应商：";
            dr["SORCID"] = "供应商ID：";
            dr["INUSERT"] = "状态：";
            dr["SOURCES"] = gridViewCSList.DataKeys[i][2].ToString();
            dr["SUPNM"] = ((Label)gridViewCSList.Rows[i].Cells[1].FindControl("lblSUPNM")).Text;
            dr["INUSERDIS"] = ((Label)gridViewCSList.Rows[i].Cells[5].FindControl("lbInUse")).Text;
            dr["RTYPE"] = gridViewCSList.DataKeys[i][3].ToString();
            dr["OSUPID"] = gridViewCSList.DataKeys[i][4].ToString();
            dtResult.Rows.Add(dr);
        }

        DataRow drInsert = dtResult.NewRow();
        drInsert["SUPID"] = "";
        drInsert["INUSE"] = "1";
        drInsert["SORC"] = "供应商：";
        drInsert["SORCID"] = "供应商ID：";
        drInsert["INUSERT"] = "状态：";
        drInsert["SOURCES"] = "ELONG";
        drInsert["SUPNM"] = "艺龙";
        drInsert["INUSERDIS"] = "上线";
        drInsert["RTYPE"] = "1";
        drInsert["OSUPID"] = "";
        dtResult.Rows.Add(drInsert);

        gridViewCSList.DataSource = null;
        gridViewCSList.DataSource = dtResult.DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "SUPID", "INUSE", "SOURCES", "RTYPE", "OSUPID" };
        gridViewCSList.DataBind();
    }

    protected void btnAddRoomMapping_Click(object sender, EventArgs e)
    {
        hidMsg.Value = "";
         LoadHotelSupDetail("0");
         LoadRoomInfoList("2", 0);
    }

    protected void btnLaodRoomInfo_Click(object sender, EventArgs e)
    {
        hidMsg.Value = "";
        LoadHotelSupDetail("0");
        LoadRoomInfoList("0", 0);
    }

    private void LoadRoomInfoList(string strType, int iIndex)
    {
        hidMsg.Value = "";
        tbRoomBand.Rows.Clear();
        DataSet dsResult = (DataSet)ViewState["dsRoomList"];//dsRoomList;
        int iCsr = 0;
        HtmlTableRow tr = new HtmlTableRow();
        HtmlTableCell td = new HtmlTableCell();
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                tr = new HtmlTableRow();
                tr.Style.Add("cursor", "pointer");
                td = new HtmlTableCell();
                iCsr = int.Parse(dsResult.Tables[0].Rows[i]["CSR"].ToString());
                if (!hidRoomInfoID.Value.Equals(dsResult.Tables[0].Rows[i]["room_code"].ToString()))
                {
                    tr.Style.Add("background", "#E4E4E4");
                    td.Attributes.Add("onclick", "LaodRoomInfo('" + dsResult.Tables[0].Rows[i]["room_code"].ToString() + "')");
                    td.InnerHtml = "<div style='float:left;'><img src='" + ((iCsr > 0) ? "../../Styles/images/star.png" : "../../Styles/images/hstar.png") + "' alt='' /></div><div style='float:left;margin-top:5px'>&nbsp;" + dsResult.Tables[0].Rows[i]["ROOMNM"].ToString() + (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString()) ? "" : "（" + dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString() + "元/间）") + "</div>";
                }
                else
                {
                    td.Style.Add("background", "#6379B2");
                    td.Style.Add("height", "35px");
                    //td.Attributes.Add("onclick", "LaodRoomInfo('" + dsResult.Tables[0].Rows[i]["room_code"].ToString() + "')");
                    td.InnerHtml = "<div style='float:left;'><img src='" + ((iCsr > 0) ? "../../Styles/images/star.png" : "../../Styles/images/hstar.png") + "' alt='' /></div><div style='float:left;margin-top:6px;color:White;'>&nbsp;" + dsResult.Tables[0].Rows[i]["ROOMNM"].ToString() + (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString()) ? "" : "（" + dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString() + "元/间）") + "</div><div style='float:right;margin-top:2px;margin-right:5px'><input type='button' class='btn primary' id='btnAddRoom' value=' + 添加房型供应商' onclick=btnAddRoomMapping('" + dsResult.Tables[0].Rows[i]["room_code"].ToString() + "') /></div>";
                    
                    tr.Cells.Add(td);
                    tbRoomBand.Rows.Add(tr);

                    tr = new HtmlTableRow();
                    td = new HtmlTableCell();
                    td.ID = "tdGridAdd";

                    gridViewRoom.EmptyDataText = "&nbsp;&nbsp;未绑定供应商房型";
                    if (!"0".Equals(strType))
                    {
                        int iEdinIndex = 0;

                        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
                        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

                        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
                        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

                        usergroupEntity.HVPID = hidRowID.Value;
                        usergroupEntity.RoomCD = hidRoomInfoID.Value;

                        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
                        DataSet dsTemp = ELRelationBP.ReviewSupRoomMappingDetail(_elRelationEntity).QueryResult;

                        if ("2".Equals(strType))
                        {
                            DataRow drInsert = dsTemp.Tables[0].NewRow();
                            drInsert["SUPID"] = "";
                            drInsert["INUSE"] = "1";
                            drInsert["SOURCES"] = "ELONG";
                            drInsert["SUPNM"] = "艺龙";
                            drInsert["INUSERDIS"] = "上线";
                            drInsert["SUPHID"] = "";
                            drInsert["RTYPE"] = "1";
                            drInsert["OROWID"] = "";
                            dsTemp.Tables[0].Rows.Add(drInsert);
                        }

                        if (dsTemp.Tables[0].Rows.Count > 0)
                        {
                            if ("1".Equals(strType))
                            {
                                iEdinIndex = iIndex;
                            }
                            else if ("2".Equals(strType))
                            {
                                iEdinIndex = dsTemp.Tables[0].Rows.Count - 1;
                            }
                            else
                            {
                                iEdinIndex = dsTemp.Tables[0].Rows.Count - 1;//("0".Equals(dsTemp.Tables[0].Rows[dsTemp.Tables[0].Rows.Count - 1]["RTYPE"].ToString())) ?  : dsTemp.Tables[0].Rows.Count;
                            }
                        }

                        gridViewRoom.EditIndex = iEdinIndex;
                        gridViewRoom.DataSource = null;
                        gridViewRoom.DataSource = dsTemp.Tables[0].DefaultView;
                        gridViewRoom.DataKeyNames = new string[] { "SUPID", "INUSE", "SOURCES", "SUPHID", "RTYPE", "OSOURCES", "OROWID" };
                        gridViewRoom.DataBind();

                        string chkINUSE = gridViewRoom.DataKeys[gridViewRoom.EditIndex][1].ToString();
                        DropDownList ddl = ((DropDownList)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[0].FindControl("ddlSupDp"));
                        CheckBox chk = ((CheckBox)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[2].FindControl("chkInUse"));
                        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(gridViewRoom.DataKeys[gridViewRoom.EditIndex][2].ToString()));
                        chk.Checked = ("1".Equals(chkINUSE)) ? true : false;


                        if ("2".Equals(strType))
                        {
                            DropDownList DDRoom = ((DropDownList)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[0].FindControl("DDlRoom"));
                            DDRoom.DataSource = getCommon_cun("ELONG");
                            DDRoom.DataTextField = "ROOMNM";
                            DDRoom.DataValueField = "ROOMCD";
                            DDRoom.DataBind();
                            HtmlGenericControl dvGDP = (HtmlGenericControl)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[0].FindControl("dvGDP");
                            HtmlGenericControl dvGTX = (HtmlGenericControl)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[0].FindControl("dvGTX");
                            dvGDP.Style.Add("display", "");
                            dvGTX.Style.Add("display", "none");
                        }
                        else
                        {
                            DropDownList DDRoom = ((DropDownList)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[0].FindControl("DDlRoom"));
                            DDRoom.DataSource = getCommon_cun(ddl.SelectedValue);
                            DDRoom.DataTextField = "ROOMNM";
                            DDRoom.DataValueField = "ROOMCD";
                            DDRoom.DataBind();
                            string strSupID = gridViewRoom.DataKeys[gridViewRoom.EditIndex][3].ToString();
                            DDRoom.SelectedIndex = DDRoom.Items.IndexOf(DDRoom.Items.FindByValue(strSupID));

                            HtmlGenericControl dvGDP = (HtmlGenericControl)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[0].FindControl("dvGDP");
                            HtmlGenericControl dvGTX = (HtmlGenericControl)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[0].FindControl("dvGTX");
                            TextBox txtRoomIDEdit = (TextBox)gridViewRoom.Rows[gridViewRoom.EditIndex].Cells[0].FindControl("txtRoomIDEdit");
                            if (((DataTable)DDRoom.DataSource).Rows.Count == 0)
                            {
                                dvGDP.Style.Add("display", "none");
                                dvGTX.Style.Add("display", "");
                                string roomID = gridViewRoom.DataKeys[gridViewRoom.EditIndex][0].ToString();
                                txtRoomIDEdit.Text = roomID;
                            }
                            else
                            {
                                dvGDP.Style.Add("display", "");
                                dvGTX.Style.Add("display", "none");
                            }
                        }
                    }
                    else
                    {
                        gridViewRoom.EditIndex = -1;
                        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
                        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

                        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
                        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

                        usergroupEntity.HVPID = hidRowID.Value;
                        usergroupEntity.RoomCD = hidRoomInfoID.Value;

                        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
                        DataSet dsTemp = ELRelationBP.ReviewSupRoomMappingDetail(_elRelationEntity).QueryResult;

                        gridViewRoom.DataSource = null;
                        gridViewRoom.DataSource = dsTemp.Tables[0].DefaultView;
                        gridViewRoom.DataKeyNames = new string[] { "SUPID", "INUSE", "SOURCES", "SUPHID", "RTYPE", "OSOURCES", "OROWID" };
                        gridViewRoom.DataBind();
                    }
                }

                tr.Cells.Add(td);
                tbRoomBand.Rows.Add(tr);
            }
        }
        else
        {
            tr = new HtmlTableRow();
            tr.Style.Add("cursor", "pointer");
            td = new HtmlTableCell();
            td.InnerHtml = "该酒店未设置房型";
            tr.Cells.Add(td);
            tbRoomBand.Rows.Add(tr);
        }

        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    protected void gridViewCSList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        hidMsg.Value = "";
        hidStyle.Value = "";
        gridViewCSList.EditIndex = -1;
        LoadHotelSupDetail("0");
        //LoadRoomSupDetail("0");
        ReLoadRoomInfoList();
        //ReFreshgridViewCSList();
        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    protected void gridViewCSList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hidMsg.Value = "";
        hidStyle.Value = "";
        gridViewCSList.EditIndex = e.NewEditIndex;
        LoadHotelSupDetail(gridViewCSList.DataKeys[e.NewEditIndex][3].ToString());
        //ReFreshgridViewCSList();
        string txtSUPID = gridViewCSList.DataKeys[e.NewEditIndex][2].ToString();
        string chkINUSE = gridViewCSList.DataKeys[e.NewEditIndex][1].ToString();
        DropDownList ddl = ((DropDownList)gridViewCSList.Rows[e.NewEditIndex].Cells[3].FindControl("ddlSupDp"));
        CheckBox chk = ((CheckBox)gridViewCSList.Rows[e.NewEditIndex].Cells[5].FindControl("chkInUse"));
        if (!String.IsNullOrEmpty(txtSUPID))
        {
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(txtSUPID));
        }

        chk.Checked = ("1".Equals(chkINUSE)) ? true : false;
        //LoadRoomSupDetail("0");
        ReLoadRoomInfoList();
        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "invokeOpenList()", true);
    }

    protected void gridViewCSList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        hidMsg.Value = "";
        hidStyle.Value = "";
        string txtHVPID = hidRowID.Value;
        string txtOSupId = gridViewCSList.DataKeys[e.RowIndex][0].ToString();
        string txtSupType = gridViewCSList.DataKeys[e.RowIndex][3].ToString();
        string txtOSource = gridViewCSList.DataKeys[e.RowIndex][2].ToString();
        string txtSUPID = ((TextBox)gridViewCSList.Rows[e.RowIndex].FindControl("txtSUPIDEdit")).Text;
        string txtSORE = ((DropDownList)gridViewCSList.Rows[e.RowIndex].FindControl("ddlSupDp")).SelectedValue;
        string txtInUse = (((CheckBox)gridViewCSList.Rows[e.RowIndex].FindControl("chkInUse")).Checked) ? "1" : "0";
        string txtOsupid = gridViewCSList.DataKeys[e.RowIndex][4].ToString();
        txtSUPID = (!String.IsNullOrEmpty(txtSORE) && ("HUBS1".Equals(txtSORE) || "HVP".Equals(txtSORE)) && String.IsNullOrEmpty(txtSUPID)) ? hidRowID.Value : txtSUPID;

        if ((!"HUBS1".Equals(txtSORE) && !"HVP".Equals(txtSORE)) && String.IsNullOrEmpty(txtSUPID))
        {
            ReLoadRoomInfoList();
            hidMsg.Value = GetLocalResourceObject("Error2").ToString();
            return;
        }

        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity ELRelationDBEntity = new ELRelationDBEntity();

        ELRelationDBEntity.HVPID = txtHVPID.Trim();
        ELRelationDBEntity.ELongID = txtSUPID.Trim();
        ELRelationDBEntity.Source = txtSORE.Trim();
        ELRelationDBEntity.SupType = txtSupType.Trim();
        ELRelationDBEntity.InUse = txtInUse.Trim();
        ELRelationDBEntity.OSource = txtOSource.Trim();
        ELRelationDBEntity.OSuphid = txtOsupid.Trim();
        ELRelationDBEntity.OSupId = txtOSupId.Trim();

        _elRelationEntity.ELRelationDBEntity.Add(ELRelationDBEntity);
        int iResult = ELRelationBP.UpdateSUPList(_elRelationEntity).Result;

        _commonEntity.LogMessages = _elRelationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "供应商酒店绑定-保存";
        commonDBEntity.Event_ID = txtHVPID.Trim();
        string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();

        conTent = string.Format(conTent, ELRelationDBEntity.HVPID, ELRelationDBEntity.ELongID, Server.UrlDecode(hidHLNM.Value), ELRelationDBEntity.Source, ELRelationDBEntity.InUse);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            //messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            gridViewCSList.EditIndex = -1;
            LoadHotelSupDetail("0");
            //LoadRoomSupDetail("0");
            ReLoadRoomInfoList();
            ddpRoomList = new DataSet();
            ddpRoomList.Tables.Add(new DataTable());
            ddpRoomList.Tables[0].Columns.Add("SUPID");
            ddpRoomList.Tables[0].Columns.Add("SUPNM");

            for (int i = 0; i < gridViewCSList.Rows.Count; i++)
            {
                DataRow dr = ddpRoomList.Tables[0].NewRow();
                dr["SUPNM"] = ((Label)gridViewCSList.Rows[i].Cells[1].FindControl("lblSUPNM")).Text;
                dr["SUPID"] = gridViewCSList.DataKeys[i][2].ToString();
                ddpRoomList.Tables[0].Rows.Add(dr);
            }
            ViewState["ddpRoomList"] = ddpRoomList;
            SetHotelRoomMappingList(txtHVPID.Trim());
            if (ELRelationDBEntity.Source.Equals(ELRelationDBEntity.OSource) || ELRelationDBEntity.ELongID.Equals(ELRelationDBEntity.OSupId))
            {
                hidMsg.Value = GetLocalResourceObject("UpdateSuccess").ToString() + " 请注意，修改酒店供应商后，需重新绑定房型！";
            }
        }
        else if (iResult == 2)
        {
            ReLoadRoomInfoList();
            hidMsg.Value = GetLocalResourceObject("Error3").ToString();
            return;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            hidMsg.Value = GetLocalResourceObject("UpdateError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void gridViewRoom_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        hidMsg.Value = "";
        hidStyle.Value = "";
        LoadHotelSupDetail("0");
        LoadRoomInfoList("0", 0);
    }

    protected void gridViewRoom_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hidMsg.Value = "";
        hidStyle.Value = "";
        int iEdit = e.NewEditIndex;
        LoadRoomInfoList("1", iEdit);
    }

    protected void gridViewRoom_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        hidMsg.Value = "";
        hidStyle.Value = "";
        string txtHVPID = hidRowID.Value;
        string txtRoomID = hidRoomInfoID.Value;

        string txtSORE = ((DropDownList)gridViewRoom.Rows[e.RowIndex].FindControl("ddlSupDp")).SelectedValue;
        string txtSUPID = GetSupIDVal(e.RowIndex);
        string txtInUse = (((CheckBox)gridViewRoom.Rows[e.RowIndex].FindControl("chkInUse")).Checked) ? "1" : "0";
        string strRtype = gridViewRoom.DataKeys[e.RowIndex][4].ToString();
        string strOSource = gridViewRoom.DataKeys[e.RowIndex][5].ToString();
        string strOSuphid = gridViewRoom.DataKeys[e.RowIndex][3].ToString();
        string strORowid = gridViewRoom.DataKeys[e.RowIndex][6].ToString();

        if (String.IsNullOrEmpty(txtSUPID.Trim()))
        {
            ReLoadRoomInfoList();
            hidMsg.Value = GetLocalResourceObject("Error5").ToString();
            return;
        }

        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity ELRelationDBEntity = new ELRelationDBEntity();

        ELRelationDBEntity.HVPID = txtHVPID.Trim();
        ELRelationDBEntity.RoomCD = txtRoomID.Trim();
        ELRelationDBEntity.ELongID = txtSUPID.Trim();
        ELRelationDBEntity.Source = txtSORE.Trim();
        ELRelationDBEntity.SupType = strRtype.Trim();
        ELRelationDBEntity.InUse = txtInUse.Trim();
        ELRelationDBEntity.OSource = strOSource.Trim();
        ELRelationDBEntity.OSuphid = strOSuphid.Trim();
        ELRelationDBEntity.ORowID = strORowid.Trim();

        _elRelationEntity.ELRelationDBEntity.Add(ELRelationDBEntity);
        int iResult = ELRelationBP.UpdateSUPRoomList(_elRelationEntity).Result;

        _commonEntity.LogMessages = _elRelationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "供应商房型绑定-保存";
        commonDBEntity.Event_ID = txtHVPID.Trim();
        string conTent = GetLocalResourceObject("EventUpdateRoomMessage").ToString();

        conTent = string.Format(conTent, ELRelationDBEntity.HVPID, ELRelationDBEntity.ELongID, Server.UrlDecode(hidHLNM.Value), ELRelationDBEntity.Source, ELRelationDBEntity.InUse);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateRoomSuccess").ToString();
            //messageContent.InnerHtml = GetLocalResourceObject("UpdateRoomSuccess").ToString();
            gridViewCSList.EditIndex = -1;
            LoadHotelSupDetail("0");
            ReLoadroomSupDetialData();
            LoadRoomInfoList("0", 0);
        }
        else if (iResult == 2)
        {
            ReLoadRoomInfoList();
            hidMsg.Value = GetLocalResourceObject("Error4").ToString();
            return;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateRoomError").ToString();
            hidMsg.Value = GetLocalResourceObject("UpdateRoomError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    private string GetSupIDVal(int rowIndex)
    {
        string strResult = "";
        string txtSORE = ((DropDownList)gridViewRoom.Rows[rowIndex].FindControl("ddlSupDp")).SelectedValue;
        string txtSUPID = ((DropDownList)gridViewRoom.Rows[rowIndex].FindControl("DDlRoom")).SelectedValue;
        string txtRoomIDEdit = ((TextBox)gridViewRoom.Rows[rowIndex].FindControl("txtRoomIDEdit")).Text;

        HtmlGenericControl dvGDP = (HtmlGenericControl)gridViewRoom.Rows[rowIndex].FindControl("dvGDP");
        if (dvGDP.Style["display"] == "none")
        {
            for (int i = 0; i < gridViewCSList.Rows.Count; i++)
            {
                if (txtSORE.Equals(gridViewCSList.DataKeys[i][2].ToString()))
                {
                    strResult = (String.IsNullOrEmpty(txtRoomIDEdit.Trim())) ? "" : gridViewCSList.DataKeys[i][0].ToString() + "_" + txtRoomIDEdit;
                    break;
                }
            }
        }
        else
        {
            strResult = txtSUPID;
        }

        return strResult;
    }

    private void ReLoadroomSupDetialData()
    {
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity usergroupEntity = new ELRelationDBEntity();

        usergroupEntity.HVPID = hidRowID.Value;
        _elRelationEntity.ELRelationDBEntity.Add(usergroupEntity);
        //dsRoomList = ELRelationBP.ReviewSupHotelRoomMappingDetail(_elRelationEntity).QueryResult;
        ViewState["dsRoomList"] = ELRelationBP.ReviewSupHotelRoomMappingDetail(_elRelationEntity).QueryResult;
    }

   protected void btnRefush_Click(object sender, EventArgs e)
   {
       hidStyle.Value = "";
       AspNetPager1.AlwaysShow = true;
       AspNetPager1.CurrentPageIndex = 1;
       BindReviewListGrid();

       gridViewRoom.EmptyDataText = "";
       gridViewRoom.DataSource = null;
       gridViewRoom.DataBind();
   }

   public void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
   {
       hidStyle.Value = "";
       hidMsg.Value = "";
       DropDownList DDHotel = (DropDownList)sender;
       GridViewRow gvr = (GridViewRow)DDHotel.NamingContainer;
       int iEdit = gvr.RowIndex;

       DropDownList DDRoom = (DropDownList)gvr.FindControl("DDlRoom");
       DataTable dtResult = getCommon_cun(DDHotel.SelectedValue);
       DDRoom.DataSource = dtResult;
       DDRoom.DataTextField = "ROOMNM";
       DDRoom.DataValueField = "ROOMCD";
       DDRoom.DataBind();
       string strSupID = gridViewRoom.DataKeys[iEdit][3].ToString();
       DDRoom.SelectedIndex = DDRoom.Items.IndexOf(DDRoom.Items.FindByValue(strSupID));

       //TextBox DDCun = (TextBox)gvr.FindControl("txtSUPIDEdit");
       //DDCun.Text = DDHotel.SelectedValue;
       HtmlGenericControl  dvGDP = (HtmlGenericControl )gvr.FindControl("dvGDP");
       HtmlGenericControl dvGTX = (HtmlGenericControl)gvr.FindControl("dvGTX");
       TextBox txtRoomIDEdit = (TextBox)gvr.FindControl("txtRoomIDEdit");
       if (dtResult.Rows.Count == 0)
       {
           dvGDP.Style.Add("display","none");
           dvGTX.Style.Add("display", "");
           string roomID = gridViewRoom.DataKeys[iEdit][0].ToString();
           txtRoomIDEdit.Text = roomID;
       }
       else
       {
           dvGDP.Style.Add("display", "");
           dvGTX.Style.Add("display", "none");
       }
       ReLoadRoomInfoList();
       //LoadRoomInfoList("1", iEdit);
   }

   private void ReLoadRoomInfoList()
   {
       tbRoomBand.Rows.Clear();
       DataSet dsResult = (DataSet)ViewState["dsRoomList"];//dsRoomList;
       int iCsr = 0;
       HtmlTableRow tr = new HtmlTableRow();
       HtmlTableCell td = new HtmlTableCell();
       if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
       {
           for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
           {
               tr = new HtmlTableRow();
               tr.Style.Add("cursor", "pointer");
               td = new HtmlTableCell();
               iCsr = int.Parse(dsResult.Tables[0].Rows[i]["CSR"].ToString());
               if (!hidRoomInfoID.Value.Equals(dsResult.Tables[0].Rows[i]["room_code"].ToString()))
               {
                   tr.Style.Add("background", "#E4E4E4");
                   td.Attributes.Add("onclick", "LaodRoomInfo('" + dsResult.Tables[0].Rows[i]["room_code"].ToString() + "')");
                   td.InnerHtml = "<div style='float:left;'><img src='" + ((iCsr > 0) ? "../../Styles/images/star.png" : "../../Styles/images/hstar.png") + "' alt='' /></div><div style='float:left;margin-top:5px'>&nbsp;" + dsResult.Tables[0].Rows[i]["ROOMNM"].ToString() + (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString()) ? "" : "（" + dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString() + "元/间）") + "</div>";
               }
               else
               {
                   td.Style.Add("background", "#6379B2");
                   td.Style.Add("height", "35px");
                   //td.Attributes.Add("onclick", "LaodRoomInfo('" + dsResult.Tables[0].Rows[i]["room_code"].ToString() + "')");
                   td.InnerHtml = "<div style='float:left;'><img src='" + ((iCsr > 0) ? "../../Styles/images/star.png" : "../../Styles/images/hstar.png") + "' alt='' /></div><div style='float:left;margin-top:6px;color:White;'>&nbsp;" + dsResult.Tables[0].Rows[i]["ROOMNM"].ToString() + (String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString()) ? "" : "（" + dsResult.Tables[0].Rows[i]["TWOPRICE"].ToString() + "元/间）") + "</div><div style='float:right;margin-top:2px;margin-right:5px'><input type='button' class='btn primary' id='btnAddRoom' value=' + 添加房型供应商' onclick=btnAddRoomMapping('" + dsResult.Tables[0].Rows[i]["room_code"].ToString() + "') /></div>";
                   tr.Cells.Add(td);
                   tbRoomBand.Rows.Add(tr);

                   tr = new HtmlTableRow();
                   td = new HtmlTableCell();
                   td.ID = "tdGridAdd";

                   gridViewRoom.EmptyDataText = "&nbsp;&nbsp;未绑定供应商房型";
               }

               tr.Cells.Add(td);
               tbRoomBand.Rows.Add(tr);
           }
       }
       else
       {
           tr = new HtmlTableRow();
           tr.Style.Add("cursor", "pointer");
           td = new HtmlTableCell();
           td.InnerHtml = "该酒店未设置房型";
           tr.Cells.Add(td);
           tbRoomBand.Rows.Add(tr);
       }

       ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "invokeOpenList()", true);
   }

   private DataTable getCommon_cun(string strSourceID)
   {
       DataTable dsMappingTemp = new DataTable();
       dsMappingTemp.Columns.Add("ROOMCD");
       dsMappingTemp.Columns.Add("ROOMNM");
       dsMappingTemp.Columns.Add("SOURCES");

       DataSet dsMappingRoomList = (DataSet)ViewState["dsMappingRoomList"];
       if (dsMappingRoomList.Tables.Count > 0 && dsMappingRoomList.Tables[0].Rows.Count > 0)
       {
           DataRow[] drList = dsMappingRoomList.Tables[0].Select("SOURCES='" + strSourceID + "'");
           for (int i = 0; i < drList.Length;i++)
           {
               DataRow dr = dsMappingTemp.NewRow();
               dr["ROOMCD"] = drList[i]["ROOMCD"].ToString();
               dr["ROOMNM"] = drList[i]["ROOMNM"].ToString();
               dr["SOURCES"] = drList[i]["SOURCES"].ToString();
               dsMappingTemp.Rows.Add(dr);
           }
       }

       return dsMappingTemp;
   }

   protected void gridViewRoom_RowCreated(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           e.Row.Cells[3].Attributes.Add("onclick", "BtnLoadStyle();");
       }
   }

   protected void gridViewCSList_RowCreated(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           e.Row.Cells[6].Attributes.Add("onclick", "BtnLoadStyle();");
       }
   }
}