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

public partial class ConsultRoomUserPage : BasePage
{
    UserEntity _userEntity = new UserEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbUserHotelNum.Text = "0";
            lbAddTotal.Text = "0";
            BindDdpList();
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "btnLoad", "BtnCompleteStyle()");
        }
        
    }

    private void BindDdpList()
    {
        DataTable dtBalType = GetTypeData();
        ddpConrultType.DataSource = dtBalType;
        ddpConrultType.DataTextField = "TYPE_TEXT";
        ddpConrultType.DataValueField = "TYPE_STATUS";
        ddpConrultType.DataBind();
        ddpConrultType.SelectedIndex = 2;
    }

    private DataTable GetTypeData()
    {
        DataTable dt = new DataTable();
        DataColumn TypeStatus = new DataColumn("TYPE_STATUS");
        DataColumn TypeStatusText = new DataColumn("TYPE_TEXT");
        dt.Columns.Add(TypeStatus);
        dt.Columns.Add(TypeStatusText);

        for (int i = 0; i < 3; i++)
        {
            DataRow dr = dt.NewRow();
            dr["TYPE_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["TYPE_TEXT"] = "城市";
                    break;
                case "1":
                    dr["TYPE_TEXT"] = "商圈";
                    break;
                case "2":
                    dr["TYPE_TEXT"] = "酒店";
                    break;
                default:
                    dr["TYPE_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void btnReLoad_Click(object sender, EventArgs e)
    {
        BindReviewUserListGrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        tbAll.Style.Add("display", "");

        if (String.IsNullOrEmpty(hidSelecUserID.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if (!hidSelecUserID.Value.Trim().Contains("[") || !hidSelecUserID.Value.Trim().Contains("]"))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error6").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        hidUserID.Value = hidSelecUserID.Value;
        BindReviewUserListGrid();
        roomDetailGridView.DataSource = null;
        roomDetailGridView.DataBind();
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
    }

    private void BindReviewUserListGrid()
    {
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();
        usergroupEntity.UserID = hidUserID.Value.Trim().Substring((hidUserID.Value.Trim().IndexOf('[') + 1), (hidUserID.Value.Trim().IndexOf(']') - 1));
        _userEntity.UserDBEntity.Add(usergroupEntity);
        DataSet dsResult = UserSearchBP.ReviewConsultRoomUserSelect(_userEntity).QueryResult;

        gridViewCSReviewUserList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewUserList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSReviewUserList.DataBind();

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            lbUserHotelNum.Text = dsResult.Tables[0].Compute("sum(HTSUM)", "true").ToString(); 
        }
        else
        {
            lbUserHotelNum.Text = "0";
        }
    }

    protected void addHotelView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        messageContent.InnerHtml = "";
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("Content");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        string HotelID = "";
        string CityID = "";
        string TagID = "";

        if (String.IsNullOrEmpty(hidAddAutoVal.Value.Trim()))
        {
            if ("0".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
            }
            else if ("1".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error8").ToString();
            }
            else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error9").ToString();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if (!hidAddAutoVal.Value.Trim().Contains("[") || !hidAddAutoVal.Value.Trim().Contains("]"))
        {
            if ("0".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error10").ToString();
            }
            else if ("1".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error11").ToString();
            }
            else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error12").ToString();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if ("0".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            CityID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }
        else if ("1".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            TagID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }
        else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            HotelID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }

        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();
        //usergroupEntity.UserID = hidUserID.Value.Trim().Substring((hidUserID.Value.Trim().IndexOf('[') + 1), (hidUserID.Value.Trim().IndexOf(']') - 1));
        usergroupEntity.HotelID = HotelID;
        usergroupEntity.CityID = CityID;
        usergroupEntity.TagID = TagID;
        usergroupEntity.RType = ddpConrultType.SelectedValue.Trim();
        _userEntity.UserDBEntity.Add(usergroupEntity);

        DataSet dsResult = UserSearchBP.PreConsultRoomUserSelect(_userEntity).QueryResult;

        addHotelView.DataSource = dsResult.Tables[0].DefaultView;
        addHotelView.DataKeyNames = new string[] { "HOTELID" };//主键
        addHotelView.DataBind();

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            lbAddTotal.Text = dsResult.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lbAddTotal.Text = "0";
        }

        dvBtnSty.Style.Add("display", "");
        dvBtnOK.Style.Add("display", "none");

        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "updateScript", "BtnOKCompleteStyle();", true);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        string HotelID = "";
        string CityID = "";
        string TagID = "";

        if (String.IsNullOrEmpty(hidUserID.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error14").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if (String.IsNullOrEmpty(hidAddAutoVal.Value.Trim()))
        {
            if ("0".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
            }
            else if ("1".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error8").ToString();
            }
            else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error9").ToString();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if (!hidAddAutoVal.Value.Trim().Contains("[") || !hidAddAutoVal.Value.Trim().Contains("]"))
        {
            if ("0".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error10").ToString();
            }
            else if ("1".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error11").ToString();
            }
            else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error12").ToString();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if ("0".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            CityID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }
        else if ("1".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            //CityID = hidAddAutoVal2.Value.Trim().Substring((hidAddAutoVal2.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal2.Value.Trim().IndexOf(']') - 1));
            TagID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }
        else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            HotelID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }

        if ("2".Equals(ddpConrultType.SelectedValue.Trim()) && hidALDelHT.Value.Contains(HotelID))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error13").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();
        usergroupEntity.UserID = hidUserID.Value.Trim().Substring((hidUserID.Value.Trim().IndexOf('[') + 1), (hidUserID.Value.Trim().IndexOf(']') - 1));
        usergroupEntity.HotelID = HotelID;
        usergroupEntity.CityID = CityID;
        usergroupEntity.TagID = TagID;
        usergroupEntity.RType = ddpConrultType.SelectedValue.Trim();
        usergroupEntity.ALDelHT = hidALDelHT.Value.ToString();
        _userEntity.UserDBEntity.Add(usergroupEntity);
        
        int iResult = UserGroupBP.InsertConsultRoomUser(_userEntity);

        _commonEntity.LogMessages = _userEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "巡房条件-保存";
        commonDBEntity.Event_ID = hidUserID.Value.ToString().Trim();
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        string conval = "";
        if ("0".Equals(usergroupEntity.RType))
        {
            conval = usergroupEntity.CityID;
        }
        else if ("1".Equals(usergroupEntity.RType))
        {
            conval = usergroupEntity.CityID + "-" + usergroupEntity.TagID;
        }
        else if ("2".Equals(usergroupEntity.RType))
        {
            conval = usergroupEntity.HotelID;
        }
        conTent = string.Format(conTent, usergroupEntity.UserID, usergroupEntity.RType, conval);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();

            dvBtnSty.Style.Add("display", "none");
            dvBtnOK.Style.Add("display", "");
            lbAddTotal.Text = "0";
            hidALDelHT.Value = "";
            hidAddDelHTID.Value = "";
            hidALDelTP.Value = "";
            addHotelView.DataSource = null;
            addHotelView.DataBind();
            //BindReviewUserListGrid();
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "BtnADDCompleteStyle();", true);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
        }
        else if (iResult == 3)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error3").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error4").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void btnCanel_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        
    }

    protected void btnRefush_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();

        usergroupEntity.UserNo = hidSelecID.Value.Trim();
        _userEntity.UserDBEntity.Add(usergroupEntity);
        DataSet dsResult = UserSearchBP.ReviewConsultRoomUserDetail(_userEntity).QueryResult;

        roomDetailGridView.DataSource = dsResult.Tables[0].DefaultView;
        roomDetailGridView.DataKeyNames = new string[] { "HEXID" };//主键
        roomDetailGridView.DataBind();
        string strTemp = "";
        for (int i = 0; i < roomDetailGridView.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (roomDetailGridView.Rows[i].RowType == DataControlRowType.DataRow)
            {
                strTemp = roomDetailGridView.DataKeys[i].Values[0].ToString().Trim();
                if (String.IsNullOrEmpty(strTemp))
                {
                    continue;
                }
                roomDetailGridView.Rows[i].Cells[0].Attributes.Add("bgcolor", "#FF6666");
            }
        }

    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        string strID = hidConID.Value.ToString();
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();
        usergroupEntity.UserNo = strID;
        _userEntity.UserDBEntity.Add(usergroupEntity);

        int iResult = UserGroupBP.DeleteConsultRoomUser(_userEntity);

        _commonEntity.LogMessages = _userEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "巡房条件-删除";
        commonDBEntity.Event_ID = hidUserID.Value.ToString().Trim();
        string conTent = GetLocalResourceObject("EventDeleteMessage").ToString();
        conTent = string.Format(conTent, usergroupEntity.UserNo);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("DelSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("DelSuccess").ToString();
            //BindReviewUserListGrid();

            if (hidConID.Value.Equals(hidSelecID.Value))
            {
                roomDetailGridView.DataSource = null;
                roomDetailGridView.DataBind();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "BtnDelCompleteStyle();", true);
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error5").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error5").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void btnAddDel_Click(object sender, EventArgs e)
    {
        string strID = hidAddDelHTID.Value.ToString();
        string strTpID = hidALDelTP.Value.ToString();
        string strtemp = "";

        DataTable dtResult = GetYujiDataSource().Tables[0];
        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            strtemp = dtResult.Rows[i]["HOTELID"].ToString();
            if (!strID.Equals(strtemp))
            {
                if (hidALDelHT.Value.Contains(strtemp))
                {
                    dtResult.Rows[i]["CTL"] = "恢复";
                }
            }
            else
            {
                if ("删除".Equals(strTpID))
                {
                    dtResult.Rows[i]["CTL"] = "恢复";
                    hidALDelHT.Value = hidALDelHT.Value + strtemp + ",";
                }
                else
                {
                    dtResult.Rows[i]["CTL"] = "删除";
                    hidALDelHT.Value = hidALDelHT.Value.Replace(strtemp + ",", "");
                }
            }
        }

        addHotelView.DataSource = dtResult;
        addHotelView.DataKeyNames = new string[] { "HOTELID" };//主键
        addHotelView.DataBind();

        string strAllID = hidALDelHT.Value.ToString();
        for (int i = 0; i < addHotelView.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (addHotelView.Rows[i].RowType == DataControlRowType.DataRow)
            {
                strtemp = addHotelView.DataKeys[i].Values[0].ToString();
                if (!strAllID.Contains(strtemp))
                {
                    continue;
                }

                addHotelView.Rows[i].Cells[0].Attributes.Add("bgcolor", "#FF6666");
                //((HtmlAnchor)addHotelView.Rows[i].Cells[2].FindControl("addRoomPopupArea")).InnerHtml = "恢复";
                //((DataBoundLiteralControl)addHotelView.Rows[i].Cells[2].Controls[0]).Text.Replace("删除", "恢复");
            }
        }

        if (dtResult.Rows.Count > 0)
        {
            int iRemove = (hidALDelHT.Value.Length > 0) ? hidALDelHT.Value.Split(',').Count()-1 : 0;
            lbAddTotal.Text = (dtResult.Rows.Count - iRemove).ToString();
        }
        else
        {
            lbAddTotal.Text = "0";
        }

        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
    }

    private DataSet GetYujiDataSource()
    {
        messageContent.InnerHtml = "";
        string HotelID = "";
        string CityID = "";
        string TagID = "";

        if ("0".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            CityID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }
        else if ("1".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            TagID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }
        else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            HotelID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
        }

        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();
        usergroupEntity.UserID = hidUserID.Value.Trim().Substring((hidUserID.Value.Trim().IndexOf('[') + 1), (hidUserID.Value.Trim().IndexOf(']') - 1));
        usergroupEntity.HotelID = HotelID;
        usergroupEntity.CityID = CityID;
        usergroupEntity.TagID = TagID;
        usergroupEntity.RType = ddpConrultType.SelectedValue.Trim();
        _userEntity.UserDBEntity.Add(usergroupEntity);
        return UserSearchBP.PreConsultRoomUserSelect(_userEntity).QueryResult;
    }

    //protected void gridViewCSReviewUserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string strID = gridViewCSReviewUserList.DataKeys[e.RowIndex].Value.ToString();

    //    _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

    //    _userEntity.UserDBEntity = new List<UserDBEntity>();
    //    UserDBEntity usergroupEntity = new UserDBEntity();
    //    usergroupEntity.UserNo = strID;
    //    _userEntity.UserDBEntity.Add(usergroupEntity);

    //    int iResult = UserGroupBP.DeleteConsultRoomUser(_userEntity);

    //    _commonEntity.LogMessages = _userEntity.LogMessages;
    //    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
    //    CommonDBEntity commonDBEntity = new CommonDBEntity();

    //    commonDBEntity.Event_Type = "巡房条件-删除";
    //    commonDBEntity.Event_ID = hidUserID.Value.ToString().Trim();
    //    string conTent = GetLocalResourceObject("EventDeleteMessage").ToString();
    //    conTent = string.Format(conTent, usergroupEntity.UserNo);
    //    commonDBEntity.Event_Content = conTent;

    //    if (iResult == 1)
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("DelSuccess").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("DelSuccess").ToString();
    //        BindReviewUserListGrid();
    //        //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnDelCompleteStyle();", true);
    //    }
    //    else
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("Error5").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("Error5").ToString();
    //    }
    //    _commonEntity.CommonDBEntity.Add(commonDBEntity);
    //    CommonBP.InsertEventHistory(_commonEntity);
    //}
}