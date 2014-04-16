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
using System.Configuration;

public partial class ConsultRoomUserOrWorkPage : BasePage
{
    UserEntity _userEntity = new UserEntity();
    CommonEntity _commonEntity = new CommonEntity();
    HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
    APPContentEntity _appcontentEntity = new APPContentEntity();

    HotelsConsultRoomManagerEntity _HotelsConsultRoomManagerEntity = new HotelsConsultRoomManagerEntity();

    #region  任务分配
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbUserHotelNum.Text = "0";
            lbAddTotal.Text = "0";
            BindDdpList();
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "btnLoad", "BtnCompleteStyle()");
            this.EndDate.Value = System.DateTime.Now.ToShortDateString().Replace("/", "-");
            this.DivClickValue.Value = "";
            this.txtStartDate.Value = System.DateTime.Now.ToShortDateString().Replace("/", "-");
        }
        if (!System.IO.Directory.Exists("C:\\ConsultLog"))
        {
            System.IO.Directory.CreateDirectory("C:\\ConsultLog");
        }
        if (!System.IO.File.Exists("C:\\ConsultLog\\Ariel.xu.AllHotels-未询.txt"))
        {
            System.IO.File.Create("C:\\ConsultLog\\Ariel.xu.AllHotels-未询.txt").Close();
        }else
        {
            System.IO.File.WriteAllText("C:\\ConsultLog\\Ariel.xu.AllHotels-未询.txt","", System.Text.Encoding.GetEncoding("GB2312"));   
        }
        if (!System.IO.File.Exists("C:\\ConsultLog\\Ariel.xu.AllHotels-已询.txt"))
        {
            System.IO.File.Create("C:\\ConsultLog\\Ariel.xu.AllHotels-已询.txt").Close();
        }
        else
        {
            System.IO.File.WriteAllText("C:\\ConsultLog\\Ariel.xu.AllHotels-已询.txt", "", System.Text.Encoding.GetEncoding("GB2312"));
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

        for (int i = 0; i < 4; i++)
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
                case "3":
                    dr["TYPE_TEXT"] = "销售";
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
        string SalesID = "";

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
            else if ("3".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error91").ToString();
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
            else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error101").ToString();
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
        else if ("3".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            SalesID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
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
        usergroupEntity.SalesID = SalesID;
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
        string SalesID = "";

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
            else if ("3".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error91").ToString();
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
            else if ("2".Equals(ddpConrultType.SelectedValue.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error101").ToString();
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
        else if ("3".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            SalesID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
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
        usergroupEntity.SalesID = SalesID;
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
        else if ("3".Equals(usergroupEntity.RType))
        {
            conval = usergroupEntity.SalesID;
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
            int iRemove = (hidALDelHT.Value.Length > 0) ? hidALDelHT.Value.Split(',').Count() - 1 : 0;
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
        string SalesID = "";

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
        else if ("3".Equals(ddpConrultType.SelectedValue.Trim()))
        {
            SalesID = hidAddAutoVal.Value.Trim().Substring((hidAddAutoVal.Value.Trim().IndexOf('[') + 1), (hidAddAutoVal.Value.Trim().IndexOf(']') - 1));
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
        usergroupEntity.SalesID = SalesID;
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
    #endregion

    #region  新房控工作管理
    /// <summary>
    /// 查询   根据选择的日期  抓取不同数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBalSearch_Click(object sender, EventArgs e)
    {
        string StartDate = this.txtStartDate.Value;
        string EndDate = DateTime.Parse(this.txtStartDate.Value).AddDays(1).ToShortDateString().Replace("/", "-");
        if (DateTime.Parse(StartDate).ToShortDateString() == DateTime.Now.ToShortDateString())
        {
            //当天   从Oracle 抓取实时数据
            DataTable dt18CheckResult = GetCheck18EXDConsultHotelCountLogsByManager(StartDate, EndDate);//获取18点以后询房的酒店数  询房人
            DataTable dtConsultPeopleResult = GetConsultPeopleByManager(StartDate, EndDate);//获取指定日期(每天)所有的询房人员--SQL
            DataTable dtEXDConsultHotelCount = GetEXDConsultHotelCountLogsByManager(StartDate, EndDate);// 获取当天所有询房人员 已询酒店的总数（返回每个人对应的酒店数）
            AssembleConsultRoomPeopleTextByManager(dtConsultPeopleResult, dtEXDConsultHotelCount, dt18CheckResult);
            AssembleAllHotel();
            AssembleAllToDaySelectConsultText(StartDate, EndDate);
        }
        else
        {
            //从SQL库抓取历史操作记录
            DataTable dt18CheckResult = GetCheck18EXDConsultHotelCountLogsByManager(StartDate, EndDate);//获取18点以后询房的酒店数  询房人
            DataTable dtConsultPeopleResult = GetConsultPeopleByManager(StartDate, EndDate);//获取指定日期(每天)所有的询房人员--SQL
            DataTable dtEXDConsultHotelCount = GetEXDConsultHotelCountLogsByManager(StartDate, EndDate);// 获取当天所有询房人员 已询酒店的总数（返回每个人对应的酒店数）
            DataTable dsResult = GetConsultRoomByTimeManagerHistory(StartDate);
            AssembleConsultRoomPeopleTextByManagerHistory(dtConsultPeopleResult, dtEXDConsultHotelCount, dsResult, dt18CheckResult);
            AssembleAllHotel();
            AssembleAllToDaySelectConsultTextByHistory(StartDate, EndDate);
        }
        this.MainDiv.Attributes["style"] = "display:''";
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "update1Script", "BtnCompleteStyle();", true);
    }

    /// <summary>
    /// 点击单个用户的时候  触发 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string StartDate = this.txtStartDate.Value;
        if (this.DivClickValue.Value != "ALL")
        {
            this.btnSearchDetails.Attributes["style"] = "display:''";
            AssembleAllHotel();
            if (DateTime.Parse(StartDate).ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                AssembleToDaySelectConsultText(this.DivClickValue.Value);
            }
            else
            {
                AssembleToDaySelectConsultTextByHistory(this.DivClickValue.Value);//历史操作记录                       
            }
        }
        else
        {
            this.btnSearchDetails.Attributes.Add("style", "display:none");

            string endDate = DateTime.Parse(this.txtStartDate.Value).AddDays(1).ToShortDateString().Replace("/", "-");
            AssembleAllHotel();
            if (DateTime.Parse(StartDate).ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                AssembleAllToDaySelectConsultText(StartDate, endDate);
            }
            else
            {
                AssembleAllToDaySelectConsultTextByHistory(StartDate, endDate); //历史操作记录                       
            }
        }
        string str = "<script type=\"text/javascript\">document.getElementById('" + this.HidDivName.Value + "').style.borderColor = '#21ACFF';BtnBalCompleteStyle();</script>";
        ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", str);
    }

    /// <summary>
    /// 拼装Text (房控员工)
    /// </summary>
    /// <param name="dtConsultPeopleResult">需要数据所有房控人</param>
    /// <param name="dtEXDConsultHotelCount">已询房酒店数</param>
    /// <param name="dt18CheckResult">18点后询房酒店数</param>
    /// 总的酒店数
    /// <returns></returns>
    public void AssembleConsultRoomPeopleTextByManager(DataTable dtConsultPeopleResult, DataTable dtEXDConsultHotelCount, DataTable dt18CheckResult)
    {

        DataTable dtSales = GetSalesManagerLisByManager();
        DataTable dtNewResult = new DataTable();
        dtNewResult.Columns.Add("PROP");
        dtNewResult.Columns.Add("CITYID");
        dtNewResult.Columns.Add("PROP_NAME_ZH");

        string Check18Hotels = "";
        int hotels = 0;
        bool flag = false;
        int EXDHotels = 0;
        //string str = "";


        StringBuilder sb = new StringBuilder();
        if (dtConsultPeopleResult != null && dtConsultPeopleResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtConsultPeopleResult.Rows.Count; i++)//遍历所有询房人员
            {
                dtNewResult = new DataTable();
                dtNewResult.Columns.Add("PROP");
                dtNewResult.Columns.Add("CITYID");
                dtNewResult.Columns.Add("PROP_NAME_ZH");
                Check18Hotels = "";
                hotels = 0;
                flag = false;
                EXDHotels = 0;
                //str = "";
                if (dtEXDConsultHotelCount.Rows.Count > 0)
                {
                    for (int j = 0; j < dtEXDConsultHotelCount.Rows.Count; j++)//遍历所有已询酒店  （人  已询酒店Count）
                    {
                        if (dtConsultPeopleResult.Rows[i]["Create_User"].ToString().Trim().ToLower() == dtEXDConsultHotelCount.Rows[j]["Create_User"].ToString().Trim().ToLower())
                        {
                            //str = dtConsultPeopleResult.Rows[i]["Create_User"].ToString() + ":" + dtEXDConsultHotelCount.Rows[j]["EXD"].ToString();
                            EXDHotels = int.Parse(dtEXDConsultHotelCount.Rows[j]["EXD"] == null || dtEXDConsultHotelCount.Rows[j]["EXD"].ToString() == "" ? "0" : dtEXDConsultHotelCount.Rows[j]["EXD"].ToString());
                            DataRow[] Rows18Check = dt18CheckResult.Select("Create_User='" + dtConsultPeopleResult.Rows[i]["Create_User"].ToString().Trim() + "'");
                            Check18Hotels = Rows18Check == null || Rows18Check.Length <= 0 ? "" : "(" + Rows18Check[0]["EXD"].ToString() + ")";
                            string CheckUserName = dtConsultPeopleResult.Rows[i]["Create_User"].ToString();
                            DataTable dtResult = GetHotelsChargeByManager(CheckUserName);//获取每个人 对应的（酒店 城市 商圈）酒店数

                            for (int l = 0; l < dtResult.Rows.Count; l++)//返回当前人的酒店列表 
                            {
                                string keyID = dtResult.Rows[l]["ID"].ToString();
                                DataTable dtHotelsResult = GetHotelsByKeysByManager(keyID);//根据 商圈 城市 来获取酒店该名下的酒店列表
                                DataTable FinalResult = FilterHotelsByManager(dtHotelsResult, dtSales);
                                for (int k = 0; k < FinalResult.Rows.Count; k++)
                                {
                                    //dtNewResult.Rows.Add(FinalResult.Rows[k]);
                                    dtNewResult.ImportRow(FinalResult.Rows[k]);
                                }
                            }
                            hotels = int.Parse(dtNewResult == null || dtNewResult.Rows.Count <= 0 ? "0" : dtNewResult.Rows.Count.ToString());
                        }

                    }
                }
                //string s = "";
                //for (int l = 0; l < dtNewResult.Rows.Count; l++)
                //{
                //    s += dtNewResult.Rows[l]["PROP"].ToString() + ",";
                //}

                sb.Append("<div id=\"PDiv" + i + "\" style=\"margin: 5px 5px 5px 5px; float: left;cursor:pointer;border:1px solid white;\"  onmousemove=\"javacript:this.style.borderColor='#21ACFF'\" onmouseout=\"javacript:this.style.borderColor='white'\"  onclick=\"divClick('" + dtConsultPeopleResult.Rows[i]["Create_User"].ToString() + "','" + EXDHotels + "','" + hotels + "','PDiv" + i + "')\">");
                if (EXDHotels >= hotels)
                {
                    //title=\"EX:{" + str + "},Hotels:{" + s + "}\"
                    sb.Append("<table style=\"background-color: #9DC996;width:150px;\" >");
                }
                else
                {
                    //title=\"EX:{" + str + "},Hotels:{" + s + "}\"
                    sb.Append("<table style=\"background-color: #E9E9E9;width:150px;\" >");
                }
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<img src=\"../../Styles/images/peopleIco.png\" style=\"width: 25px; height: 28px;\" />");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dtConsultPeopleResult.Rows[i]["Create_User"].ToString());
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan=\"2\" style=\"border-top: 1px solid #D5D5D5; text-align: center;\">");
                sb.Append(EXDHotels + Check18Hotels + "/" + hotels);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
            }
        }
        this.ToDayConsultPeople.InnerHtml = sb.ToString() + AssembleALLTextByManager(dt18CheckResult);
    }

    /// <summary>
    /// 拼装Text (房控员工)---历史记录
    /// </summary>
    /// <param name="dtConsultPeopleResult">需要数据所有房控人</param>
    /// <param name="dtEXDConsultHotelCount">已询房酒店数 </param>
    /// <param name="dtEXDConsultHotelCount">总的酒店数</param>
    /// <param name="dt18CheckResult">18点后询房酒店数</param>
    /// <returns></returns>
    public void AssembleConsultRoomPeopleTextByManagerHistory(DataTable dtConsultPeopleResult, DataTable dtEXDConsultHotelCount, DataTable dtResult, DataTable dt18CheckResult)
    {
        string Check18Hotels = "";
        int hotels = 0;
        int EXDHotels = 0;

        StringBuilder sb = new StringBuilder();
        if (dtConsultPeopleResult != null && dtConsultPeopleResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtConsultPeopleResult.Rows.Count; i++)//遍历所有询房人员
            {
                Check18Hotels = "";
                hotels = 0;
                EXDHotels = 0;
                if (dtEXDConsultHotelCount.Rows.Count > 0)
                {
                    for (int j = 0; j < dtEXDConsultHotelCount.Rows.Count; j++)//遍历所有已询酒店  （人  已询酒店Count）
                    {
                        if (dtConsultPeopleResult.Rows[i]["Create_User"].ToString().Trim().ToLower() == dtEXDConsultHotelCount.Rows[j]["Create_User"].ToString().Trim().ToLower())
                        {
                            EXDHotels = int.Parse(dtEXDConsultHotelCount.Rows[j]["EXD"] == null || dtEXDConsultHotelCount.Rows[j]["EXD"].ToString() == "" ? "0" : dtEXDConsultHotelCount.Rows[j]["EXD"].ToString());
                            string CheckUserName = dtConsultPeopleResult.Rows[i]["Create_User"].ToString();
                            DataRow[] Rows18Check = dt18CheckResult.Select("Create_User='" + CheckUserName + "'");
                            Check18Hotels = Rows18Check == null || Rows18Check.Length <= 0 ? "" : "(" + Rows18Check[0]["EXD"].ToString() + ")";
                            DataRow[] drResult = dtResult.Select("checkUserName='" + CheckUserName + "'"); //返回当前人的酒店列表数 

                            hotels = int.Parse(drResult == null || drResult.Length <= 0 ? "0" : drResult[0]["HTSUM"].ToString()); 
                        }
                    }
                }

                sb.Append("<div id=\"PDiv" + i + "\" style=\"margin: 5px 5px 5px 5px; float: left;cursor:pointer;border:1px solid white;\"  onmousemove=\"javacript:this.style.borderColor='#21ACFF'\" onmouseout=\"javacript:this.style.borderColor='white'\"  onclick=\"divClick('" + dtConsultPeopleResult.Rows[i]["Create_User"].ToString() + "','" + EXDHotels + "','" + hotels + "','PDiv" + i + "')\">");
                if (EXDHotels >= hotels)
                {
                    sb.Append("<table style=\"background-color: #9DC996;width:150px;\">");
                }
                else
                {
                    sb.Append("<table style=\"background-color: #E9E9E9;width:150px;\">");
                }
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<img src=\"../../Styles/images/peopleIco.png\" style=\"width: 25px; height: 28px;\" />");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dtConsultPeopleResult.Rows[i]["Create_User"].ToString());
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan=\"2\" style=\"border-top: 1px solid #D5D5D5; text-align: center;\">");
                sb.Append(EXDHotels + Check18Hotels + "/" + hotels);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
            }
        }
        this.ToDayConsultPeople.InnerHtml = sb.ToString() + AssembleAllCityText(dt18CheckResult);
    }

    /// <summary>
    /// 拼装当日房控员工 -  ALL
    /// </summary>
    /// <returns></returns>
    public string AssembleALLTextByManager(DataTable dt18CheckResult)
    {
        //string s = "";
        int Check18Hotels = 0;
        int LMCount = 0;
        StringBuilder sb = new StringBuilder();
        string StartDate = this.txtStartDate.Value;
        string endDate = DateTime.Parse(this.txtStartDate.Value).AddDays(1).ToShortDateString().Replace("/", "-");
        DataTable dtSales = GetSalesManagerLisByManager();
        DataTable dtEXHotelsResult = GetConsultHotelsByManager(StartDate, endDate);//当天 所有已询房的数据
        DataTable dtConsultResult = GetConsultManagerAllCitysByManager();//LM城市下 所有上线酒店

        DataTable dtFilterCityResult = FilterHotelsByAllCitysManager(dtConsultResult, dtSales);

        DataTable FinalResult = AssembleAllToDaySelectConsultTextByManager(dtFilterCityResult);
        for (int i = 0; i < FinalResult.Rows.Count; i++)
        {
            LMCount += int.Parse(FinalResult.Rows[i]["LMCount"] == null || FinalResult.Rows[i]["LMCount"].ToString() == "" ? "0" : FinalResult.Rows[i]["LMCount"].ToString());
            //s += FinalResult.Rows[i]["PROP"].ToString() + ",";
        }
        this.CompletedConsultHotels.Value = dtEXHotelsResult.Rows.Count.ToString();
        this.SumConsultHotels.Value = LMCount.ToString();
        //计算18点后询房的酒店总数
        for (int i = 0; i < dt18CheckResult.Rows.Count; i++)
        {
            Check18Hotels += int.Parse(dt18CheckResult.Rows[i]["EXD"] == null || dt18CheckResult.Rows[i]["EXD"].ToString() == "" ? "0" : dt18CheckResult.Rows[i]["EXD"].ToString());
        }

        sb.Append("<div id=\"ALLDiv\" style=\"margin: 5px 5px 5px 5px; float: left;cursor:pointer;border:1px solid white;\"  onmousemove=\"javacript:this.style.borderColor='#21ACFF'\" onmouseout=\"javacript:this.style.borderColor='white'\" onclick=\"divClick('ALL','" + this.CompletedConsultHotels.Value + "','" + this.SumConsultHotels.Value + "','ALLDiv')\">");
        if (int.Parse(dtEXHotelsResult.Rows.Count.ToString()) >= LMCount)
        {
            // title=\"" + s + "\"
            sb.Append("<table style=\"background-color: #9DC996;width:150px;\">");
        }
        else
        {
            // title=\"" + s + "\"
            sb.Append("<table style=\"background-color: #E9E9E9;width:150px;\">");
        }
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append("<img src=\"../../Styles/images/peopleIco.png\" style=\"width: 25px; height: 28px;\" />");
        sb.Append("</td>");
        sb.Append("<td>");
        sb.Append("ALL");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan=\"2\" style=\"border-top: 1px solid #D5D5D5; text-align: center;\">");
        string Result = dtEXHotelsResult.Rows.Count.ToString();
        Result += Check18Hotels == 0 ? "" : "(" + Check18Hotels + ")";
        sb.Append(Result + "/" + LMCount.ToString());
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("</div>");
        return sb.ToString();
    }

    /// <summary>
    /// 拼装当日房控员工 -  ALL ---历史记录
    /// </summary>
    /// <returns></returns>
    public string AssembleAllCityText(DataTable dt18CheckResult)
    {
        int Check18Hotels = 0;
        int LMCount = 0;
        StringBuilder sb = new StringBuilder();
        string StartDate = this.txtStartDate.Value;
        string endDate = DateTime.Parse(this.txtStartDate.Value).AddDays(1).ToShortDateString().Replace("/", "-");
        DataTable dtSales = GetSalesManagerLisByManager();
        DataTable dtEXHotelsResult = GetConsultHotelsByManager(StartDate, endDate);//当天 所有已询房的数据
        DataTable dtConsultResult = GetAllCityByTimeManagerHistory(StartDate);//LM城市下 所有上线酒店

        for (int i = 0; i < dtConsultResult.Rows.Count; i++)
        {
            LMCount += int.Parse(dtConsultResult.Rows[i]["LMCount"] == null || dtConsultResult.Rows[i]["LMCount"].ToString() == "" ? "0" : dtConsultResult.Rows[i]["LMCount"].ToString());
        }

        this.CompletedConsultHotels.Value = dtEXHotelsResult.Rows.Count.ToString();
        this.SumConsultHotels.Value = LMCount.ToString();

        //计算18点后询房的酒店总数
        for (int i = 0; i < dt18CheckResult.Rows.Count; i++)
        {
            Check18Hotels += int.Parse(dt18CheckResult.Rows[i]["EXD"] == null || dt18CheckResult.Rows[i]["EXD"].ToString() == "" ? "0" : dt18CheckResult.Rows[i]["EXD"].ToString());
        }

        sb.Append("<div id=\"ALLDiv\" style=\"margin: 5px 5px 5px 5px; float: left;cursor:pointer;border:1px solid white;\"  onmousemove=\"javacript:this.style.borderColor='#21ACFF'\" onmouseout=\"javacript:this.style.borderColor='white'\" onclick=\"divClick('ALL','" + this.CompletedConsultHotels.Value + "','" + this.SumConsultHotels.Value + "','ALLDiv')\">");
        if (int.Parse(dtEXHotelsResult.Rows.Count.ToString()) >= LMCount)
        {
            sb.Append("<table style=\"background-color: #9DC996;width:150px;\">");
        }
        else
        {
            sb.Append("<table style=\"background-color: #E9E9E9;width:150px;\">");
        }
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append("<img src=\"../../Styles/images/peopleIco.png\" style=\"width: 25px; height: 28px;\" />");
        sb.Append("</td>");
        sb.Append("<td>");
        sb.Append("ALL");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan=\"2\" style=\"border-top: 1px solid #D5D5D5; text-align: center;\">");
        string Result = dtEXHotelsResult.Rows.Count.ToString();
        Result += Check18Hotels == 0 ? "" : "(" + Check18Hotels + ")";
        sb.Append(Result + "/" + LMCount.ToString());
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("</div>");
        return sb.ToString();
    }

    /// <summary>
    /// 拼装当日房控查询情况---Text
    /// </summary>
    /// <returns></returns>
    public void AssembleToDaySelectConsultText(string salesID)
    {
        //string s = "";
        int EXDHotels = 0;
        int YetHotel = 0;
        DataTable dtSales = GetSalesManagerLisByManager();

        string StartDate = this.txtStartDate.Value;
        string endDate = DateTime.Parse(this.txtStartDate.Value).AddDays(1).ToShortDateString().Replace("/", "-");

        StringBuilder sb = new StringBuilder();
        sb.Append("<table style=\"width:90%;border-collapse: collapse;\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\">");
        DataTable dtConsultPeopleResult = GetHotelsChargeByManager(salesID);//获取每个人 对应的（酒店 城市 商圈）酒店数
        //DataRow[] drYetConsultRoomResult = GetConsultPeopleByManager(StartDate, endDate).Select("Create_User='" + salesID + "'");//当天 当前人所有已询房的数据
        DataTable dtYetConsultRoomResult = GetConsultHotelsBySales(salesID, StartDate, endDate);//当天 当前人所有已询房的数据
        EXDHotels = int.Parse(dtYetConsultRoomResult == null || dtYetConsultRoomResult.Rows.Count == 0 ? "0" : dtYetConsultRoomResult.Rows.Count.ToString());//已询酒店数

        if (dtConsultPeopleResult != null && dtConsultPeopleResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtConsultPeopleResult.Rows.Count; i++)
            {
                //s = "";
                YetHotel = 0;
                string keyID = dtConsultPeopleResult.Rows[i]["ID"].ToString();
                DataTable dtHotelsResult = GetHotelsByKeysByManager(keyID);//根据 商圈 城市 来获取酒店该名下的酒店列表
                DataTable FinalResult = FilterHotelsByManager(dtHotelsResult, dtSales);
                for (int j = 0; j < FinalResult.Rows.Count; j++)
                {

                    for (int l = 0; l < dtYetConsultRoomResult.Rows.Count; l++)
                    {
                        if (FinalResult.Rows[j]["PROP"].ToString() == dtYetConsultRoomResult.Rows[l]["HotelID"].ToString())
                        {
                            //s += FinalResult.Rows[j]["PROP"].ToString() + ",";
                            YetHotel += 1;
                            break;
                        }
                    }
                }
                sb.Append("<tr style=\"border: 1px solid #666666;line-height:40px;\">");
                if (YetHotel >= int.Parse(dtConsultPeopleResult.Rows[i]["HTSUM"].ToString()))
                {
                    sb.Append("<td style=\"width:150px;text-align:center;border: 1px solid #666666;background-color:#29A778;\">");
                }
                else
                {
                    sb.Append("<td style=\"width:150px;text-align:center;border: 1px solid #666666;\">");
                }
                //+ "EX:" + s
                sb.Append(dtConsultPeopleResult.Rows[i]["CONSULTVAL"].ToString());
                sb.Append("</td>");
                sb.Append("<td style=\"border: 1px solid #666666;\">");
                sb.Append("<div class=\"graph\" style=\"width: 800px;margin-left:10px;margin-right:10px;\">");
                double progressBar = double.Parse(YetHotel.ToString()) / double.Parse(dtConsultPeopleResult.Rows[i]["HTSUM"].ToString());
                if (progressBar > 1.0) { progressBar = 1.0; }
                sb.Append("<strong class=\"bar\" style=\"width: " + progressBar * 100 + "%;\"><span style=\"padding-left: 350px;\">" + YetHotel.ToString() + "/" + dtConsultPeopleResult.Rows[i]["HTSUM"].ToString() + "</span></strong></div>");
                sb.Append("</td>");
                sb.Append("</tr>");
            }
        }
        sb.Append("</table>");
        this.DIVDistributionRules.InnerHtml = sb.ToString();
    }

    /// <summary>
    /// 拼装当日房控查询情况---Text----History
    /// </summary>
    /// <returns></returns>
    public void AssembleToDaySelectConsultTextByHistory(string salesID)
    {
        DataTable dtNewResult = new DataTable();
        dtNewResult.Columns.Add("PROP");
        dtNewResult.Columns.Add("CITYID");
        dtNewResult.Columns.Add("CITYNAME");
        dtNewResult.Columns.Add("ReadyCount");//询过的酒店数
        dtNewResult.Columns.Add("LMCount");//LM酒店数

        bool flag = false;
        DataTable dtSales = GetSalesManagerLisByManager();

        string StartDate = this.txtStartDate.Value;
        string endDate = DateTime.Parse(this.txtStartDate.Value).AddDays(1).ToShortDateString().Replace("/", "-");

        StringBuilder sb = new StringBuilder();

        DataTable dtConsultPeopleResult = GetConsultRoomByTimeAndUserManagerHistory(StartDate, salesID);//获取每个人 对应的酒店列表
        DataTable dtYetConsultRoomResult = GetConsultHotelsBySales(salesID, StartDate, endDate);//当天 当前人所有已询房的数据
        //EXDHotels = int.Parse(dtYetConsultRoomResult == null || dtYetConsultRoomResult.Rows.Count == 0 ? "0" : dtYetConsultRoomResult.Rows.Count.ToString());//已询酒店数

        if (dtConsultPeopleResult != null && dtConsultPeopleResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtConsultPeopleResult.Rows.Count; i++)
            {
                flag = false;
                System.IO.File.AppendAllText("C:\\ConsultLog\\Ariel.xu.AllHotels-未询.txt", dtConsultPeopleResult.Rows[i]["hotelId"].ToString() + "\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                string cityID = dtConsultPeopleResult.Rows[i]["CITYID"].ToString();
                DataRow[] rows = dtNewResult.Select("CITYID='" + cityID + "'");
                if (rows.Length <= 0)//当前城市 暂时还未循环
                {
                    DataRow dr = dtNewResult.NewRow();
                    dr["PROP"] = dtConsultPeopleResult.Rows[i]["hotelId"].ToString();
                    dr["CITYID"] = cityID;
                    dr["CITYNAME"] = dtConsultPeopleResult.Rows[i]["CITYNAME"].ToString();

                    for (int l = 0; l < dtYetConsultRoomResult.Rows.Count; l++)
                    {
                        if (dtConsultPeopleResult.Rows[i]["hotelId"].ToString() == dtYetConsultRoomResult.Rows[l]["HotelID"].ToString())
                        {
                            System.IO.File.AppendAllText("C:\\ConsultLog\\Ariel.xu.AllHotels-已询.txt", dtYetConsultRoomResult.Rows[l]["HotelID"].ToString() + "\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                            flag = true;
                            dr["ReadyCount"] = int.Parse(dr["ReadyCount"] == null || dr["ReadyCount"].ToString() == "" ? "0" : dr["ReadyCount"].ToString()) + 1;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        dr["ReadyCount"] = "0";
                    }
                    dr["LMCount"] = "1";
                    dtNewResult.Rows.Add(dr);
                }
                else
                {
                    string hotelIds = dtConsultPeopleResult.Rows[i]["hotelId"].ToString();

                    for (int l = 0; l < dtYetConsultRoomResult.Rows.Count; l++)
                    {
                        if (hotelIds == dtYetConsultRoomResult.Rows[l]["HotelID"].ToString())
                        {
                            System.IO.File.AppendAllText("C:\\ConsultLog\\Ariel.xu.AllHotels-已询.txt", hotelIds + "\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                            rows[0]["ReadyCount"] = int.Parse(rows[0]["ReadyCount"] == null || rows[0]["ReadyCount"].ToString() == "" ? "0" : rows[0]["ReadyCount"].ToString()) + 1;
                            break;
                        }
                    }
                    rows[0]["LMCount"] = int.Parse(rows[0]["LMCount"].ToString()) + 1;
                }
            }
        }

        sb.Append("<table style=\"width:90%;border-collapse: collapse;\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\">");
        for (int i = 0; i < dtNewResult.Rows.Count; i++)
        {
            sb.Append("<tr style=\"border: 1px solid #666666;line-height:40px;\">");
            if (int.Parse(dtNewResult.Rows[i]["ReadyCount"].ToString()) >= int.Parse(dtNewResult.Rows[i]["LMCount"].ToString()))
            {
                sb.Append("<td style=\"width:150px;text-align:center;border: 1px solid #666666;background-color:#29A778;\">");
            }
            else
            {
                sb.Append("<td style=\"width:150px;text-align:center;border: 1px solid #666666;\">");
            }
            sb.Append(dtNewResult.Rows[i]["CITYNAME"].ToString());
            sb.Append("</td>");
            sb.Append("<td style=\"border: 1px solid #666666;\">");
            sb.Append("<div class=\"graph\" style=\"width: 800px;margin-left:10px;margin-right:10px;\">");
            double progressBar = double.Parse(dtNewResult.Rows[i]["ReadyCount"].ToString()) / double.Parse(dtNewResult.Rows[i]["LMCount"].ToString());
            if (progressBar > 1.0) { progressBar = 1.0; }
            sb.Append("<strong class=\"bar\" style=\"width: " + progressBar * 100 + "%;\"><span style=\"padding-left: 350px;\">" + dtNewResult.Rows[i]["ReadyCount"].ToString() + "/" + dtNewResult.Rows[i]["LMCount"].ToString() + "</span></strong></div>");
            sb.Append("</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");


        this.DIVDistributionRules.InnerHtml = sb.ToString();
    }

    /// <summary>
    /// 拼装当日房控查询情况  ALL   所有酒店情况---Text
    /// </summary>
    /// <returns></returns>
    public void AssembleAllToDaySelectConsultText(string SDate, string EDate)
    {
        DataTable dtSales = GetSalesManagerLisByManager();
        DataTable dtEXHotelsResult = GetConsultHotelsByManager(SDate, EDate);//当天 所有已询房的数据
        DataTable dtConsultResult = GetConsultManagerAllCitysByManager();//LM城市下 所有上线酒店

        DataTable dtFilterCityResult = FilterHotelsByAllCitysManager(dtConsultResult, dtSales);

        //DataTable FinalResult = AssembleAllToDaySelectConsultTextByManager(dtFilterCityResult);

        DataTable dtNewResult = new DataTable();
        dtNewResult.Columns.Add("PROP");
        dtNewResult.Columns.Add("CITYID");
        dtNewResult.Columns.Add("CITYNAME");
        dtNewResult.Columns.Add("ReadyCount");//询过的酒店数
        dtNewResult.Columns.Add("LMCount");//LM酒店数

        StringBuilder sb = new StringBuilder();
        //PROP    CITYID    CITYNAME          PROP_NAME_ZH
        if (dtFilterCityResult != null && dtFilterCityResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtFilterCityResult.Rows.Count; i++)
            {
                string cityID = dtFilterCityResult.Rows[i]["CITYID"].ToString();
                DataRow[] rows = dtNewResult.Select("CITYID='" + cityID + "'");
                if (rows.Length <= 0)//当前城市 暂时还未循环
                {
                    DataRow dr = dtNewResult.NewRow();
                    dr["PROP"] = dtFilterCityResult.Rows[i]["PROP"].ToString();
                    dr["CITYID"] = cityID;
                    dr["CITYNAME"] = dtFilterCityResult.Rows[i]["CITYNAME"].ToString();
                    bool flag = false;
                    for (int j = 0; j < dtEXHotelsResult.Rows.Count; j++)//循环sql 中日志记录的酒店
                    {
                        if (dtFilterCityResult.Rows[i]["PROP"].ToString() == dtEXHotelsResult.Rows[j]["HotelID"].ToString())
                        {
                            flag = true;
                            dr["ReadyCount"] = "1";
                            break;
                        }
                    }
                    if (!flag)
                    {
                        dr["ReadyCount"] = "0";
                    }
                    dr["LMCount"] = "1";
                    dtNewResult.Rows.Add(dr);
                }
                else
                {
                    for (int j = 0; j < dtEXHotelsResult.Rows.Count; j++)//循环sql 中日志记录的酒店
                    {
                        if (dtFilterCityResult.Rows[i]["PROP"].ToString() == dtEXHotelsResult.Rows[j]["HotelID"].ToString())
                        {
                            rows[0]["ReadyCount"] = int.Parse(rows[0]["ReadyCount"].ToString()) + 1;
                            break;
                        }
                    }
                    rows[0]["LMCount"] = int.Parse(rows[0]["LMCount"].ToString()) + 1;
                }
            }
        }

        for (int i = 0; i < dtNewResult.Rows.Count; i++)
        {
            if (i % 2 == 0)
            {
                sb.Append("<table style=\"width:90%;border-collapse: collapse;background-color: #F6F6F6;\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\"  onmousemove=\"javacript:this.style.backgroundColor='#CCCCCC'\" onmouseout=\"javacript:this.style.backgroundColor='#F6F6F6'\">");
            }
            else
            {
                sb.Append("<table style=\"width:90%;border-collapse: collapse;\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\"  onmousemove=\"javacript:this.style.backgroundColor='#CCCCCC'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
            }

            sb.Append("<tr style=\"border: 1px solid #666666;line-height:40px;\">");
            if (int.Parse(dtNewResult.Rows[i]["ReadyCount"].ToString()) >= int.Parse(dtNewResult.Rows[i]["LMCount"].ToString()))
            {
                sb.Append("<td style=\"width:150px;text-align:center;border: 1px solid #666666;background-color: #339966;\">");
            }
            else
            {
                sb.Append("<td style=\"width:150px;text-align:center;border: 1px solid #666666;\">");
            }
            sb.Append(dtNewResult.Rows[i]["CITYNAME"].ToString());
            sb.Append("</td>");
            sb.Append("<td style=\"border: 1px solid #666666;\">");
            sb.Append("<div class=\"graph\" style=\"width: 97%;margin-left:10px;margin-right:10px;\">");
            double percentage = double.Parse(dtNewResult.Rows[i]["ReadyCount"].ToString()) / double.Parse(dtNewResult.Rows[i]["LMCount"].ToString());
            if (percentage > 1.0) { percentage = 1.0; }
            sb.Append("<strong class=\"bar\" style=\"width: " + percentage * 100 + "%;\"><span style=\"padding-left: 350px;\">" + dtNewResult.Rows[i]["ReadyCount"].ToString() + "/" + dtNewResult.Rows[i]["LMCount"].ToString() + "</span></strong></div>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
        }

        this.DIVDistributionRules.InnerHtml = sb.ToString();

    }

    /// <summary>
    /// 拼装当日房控查询情况  ALL   所有酒店情况---Text----History
    /// </summary>
    /// <returns></returns>
    public void AssembleAllToDaySelectConsultTextByHistory(string SDate, string EDate)
    {
        bool flag = false;
        //DataTable dtSales = GetSalesManagerLisByManager();
        DataTable dtEXHotelsResult = GetConsultHotelsByManager(SDate, EDate);//当天 所有已询房的数据
        //DataTable dtConsultResult = GetConsultManagerAllCitysByManager();//LM城市下 所有上线酒店

        //DataTable dtFilterCityResult = FilterHotelsByAllCitysManager(dtConsultResult, dtSales);

        DataTable dtFilterCityResult = GetAllCityByTimeManagerHistory(SDate);


        DataTable dtNewResult = new DataTable();
        dtNewResult.Columns.Add("PROP");
        dtNewResult.Columns.Add("CITYID");
        dtNewResult.Columns.Add("CITYNAME");
        dtNewResult.Columns.Add("ReadyCount");//询过的酒店数
        dtNewResult.Columns.Add("LMCount");//LM酒店数

        StringBuilder sb = new StringBuilder();
        //PROP    CITYID    CITYNAME          PROP_NAME_ZH
        if (dtFilterCityResult != null && dtFilterCityResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtFilterCityResult.Rows.Count; i++)
            {
                flag = false;
                string cityID = dtFilterCityResult.Rows[i]["CITYID"].ToString();
                DataRow[] rows = dtNewResult.Select("CITYID='" + cityID + "'");
                if (rows.Length <= 0)//当前城市 暂时还未循环
                {
                    DataRow dr = dtNewResult.NewRow();
                    dr["PROP"] = dtFilterCityResult.Rows[i]["hotelId"].ToString();
                    dr["CITYID"] = cityID;
                    dr["CITYNAME"] = dtFilterCityResult.Rows[i]["CITYNAME"].ToString();
                    string[] hotelIds = dtFilterCityResult.Rows[i]["hotelId"].ToString().Split(',');
                    for (int j = 0; j < hotelIds.Length; j++)
                    {
                        for (int l = 0; l < dtEXHotelsResult.Rows.Count; l++)
                        {
                            if (hotelIds[j].ToString() == dtEXHotelsResult.Rows[l]["HotelID"].ToString())
                            {
                                flag = true;
                                dr["ReadyCount"] = int.Parse(dr["ReadyCount"] == null || dr["ReadyCount"].ToString() == "" ? "0" : dr["ReadyCount"].ToString()) + 1;
                                break;
                            }
                        }
                    }
                    if (!flag)
                    {
                        dr["ReadyCount"] = "0";
                    }
                    dr["LMCount"] = dtFilterCityResult.Rows[i]["LMCount"].ToString();
                    dtNewResult.Rows.Add(dr);
                }
                else
                {
                    string[] hotelIds = dtFilterCityResult.Rows[i]["hotelId"].ToString().Split(',');
                    for (int j = 0; j < hotelIds.Length; j++)
                    {
                        for (int l = 0; l < dtEXHotelsResult.Rows.Count; l++)
                        {
                            if (hotelIds[j].ToString() == dtEXHotelsResult.Rows[l]["HotelID"].ToString())
                            {
                                rows[0]["ReadyCount"] = int.Parse(rows[0]["ReadyCount"] == null || rows[0]["ReadyCount"].ToString() == "" ? "0" : rows[0]["ReadyCount"].ToString()) + 1;
                                break;
                            }
                        }
                        rows[0]["LMCount"] = int.Parse(rows[0]["LMCount"].ToString()) + 1;
                    }
                }
            }
        }

        for (int i = 0; i < dtNewResult.Rows.Count; i++)
        {
            if (i % 2 == 0)
            {
                sb.Append("<table style=\"width:90%;border-collapse: collapse;background-color: #F6F6F6;\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\"  onmousemove=\"javacript:this.style.backgroundColor='#CCCCCC'\" onmouseout=\"javacript:this.style.backgroundColor='#F6F6F6'\">");
            }
            else
            {
                sb.Append("<table style=\"width:90%;border-collapse: collapse;\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\"  onmousemove=\"javacript:this.style.backgroundColor='#CCCCCC'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
            }

            sb.Append("<tr style=\"border: 1px solid #666666;line-height:40px;\">");
            if (int.Parse(dtNewResult.Rows[i]["ReadyCount"].ToString()) >= int.Parse(dtNewResult.Rows[i]["LMCount"].ToString()))
            {
                sb.Append("<td style=\"width:150px;text-align:center;border: 1px solid #666666;background-color: #339966;\">");
            }
            else
            {
                sb.Append("<td style=\"width:150px;text-align:center;border: 1px solid #666666;\">");
            }
            sb.Append(dtNewResult.Rows[i]["CITYNAME"].ToString());
            sb.Append("</td>");
            sb.Append("<td style=\"border: 1px solid #666666;\">");
            sb.Append("<div class=\"graph\" style=\"width: 97%;margin-left:10px;margin-right:10px;\">");
            double percentage = double.Parse(dtNewResult.Rows[i]["ReadyCount"].ToString()) / double.Parse(dtNewResult.Rows[i]["LMCount"].ToString());
            if (percentage > 1.0) { percentage = 1.0; }
            sb.Append("<strong class=\"bar\" style=\"width: " + percentage * 100 + "%;\"><span style=\"padding-left: 350px;\">" + dtNewResult.Rows[i]["ReadyCount"].ToString() + "/" + dtNewResult.Rows[i]["LMCount"].ToString() + "</span></strong></div>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
        }

        this.DIVDistributionRules.InnerHtml = sb.ToString();

    }

    /// <summary>
    /// 拼装当天所有酒店情况
    /// </summary>
    /// <returns></returns>
    public void AssembleAllHotel()
    {
        StringBuilder sb = new StringBuilder();
        double percentage = double.Parse(this.CompletedConsultHotels.Value == "" ? "0" : this.CompletedConsultHotels.Value) / double.Parse(this.SumConsultHotels.Value == "" ? "0" : this.SumConsultHotels.Value);
        if (percentage > 1.0) { percentage = 1.0; }
        sb.Append("<strong class=\"bar\" style=\"width: " + percentage * 100 + "%;\"><span style=\"padding-left: 400px;\">" + this.CompletedConsultHotels.Value + "/" + this.SumConsultHotels.Value + "</span></strong>");
        this.DivProgressBar.InnerHtml = sb.ToString();
    }

    /// <summary>
    /// 获取指定日期(每天)所有的询房人员--SQL
    /// </summary>
    /// <returns></returns>
    public DataTable GetConsultPeopleByManager(string SDate, string EDate)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.SDate = SDate;
        hotelsConsultRoomManagerDBEntity.EDate = EDate;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetConsultPeopleByManager(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 获取当天所有询房人员 已询酒店的总数（返回每个人对应的酒店数）
    /// </summary>
    /// <returns></returns>
    public DataTable GetEXDConsultHotelCountLogsByManager(string SDate, string EDate)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.SDate = SDate;
        hotelsConsultRoomManagerDBEntity.EDate = EDate;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetEXDConsultHotelCountLogsByManager(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 获取每个人 对应的（酒店 城市 商圈）酒店数
    /// </summary>
    /// <param name="salesID"></param>
    /// <returns></returns>
    public DataTable GetHotelsChargeByManager(string CheckUserName)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.CheckUserName = CheckUserName;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetOracleHotelsByConsultPeopleByManager(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 根据 商圈 城市 来获取酒店该名下的酒店列表
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public DataTable GetHotelsByKeysByManager(string keyID)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.KeyID = keyID;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetHotelsByKeysByManager(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;

    }

    /// <summary>
    /// 获取当天已询酒店列表--All  酒店Count--SQL
    /// </summary>
    /// <returns></returns>
    public DataTable GetConsultHotelsByManager(string SDate, string EDate)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.SDate = SDate;
        hotelsConsultRoomManagerDBEntity.EDate = EDate;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetConsultHotelsByManager(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 获取ALl  所有城市对应的 城市下面的酒店列表--Oracle
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public DataTable GetConsultManagerAllCitysByManager()
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetConsultManagerAllCitysByManager(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;

    }

    /// <summary>
    /// 获取所有的销售人员
    /// </summary>
    private DataTable GetSalesManagerLisByManager()
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtSales = HotelsConsultRoomManagerBP.GetSalesManagerList(_HotelsConsultRoomManagerEntity).Tables[0];

        return dtSales;
    }

    //1.当天无计划的酒店，全部过滤掉；  --SQL文中  inner join (select hotel_id,status,effect_date,Room_type_code,MODIFIER from t_lm_plan where effect_date=to_date('2013-05-28','yyyy-MM-dd')) lp on bp.prop= lp.hotel_id
    //2.当天下线的酒店，全部过滤掉； --SQL文中 bp.type=0 AND bp.STATUS='active' AND bp.ONLINE_STATUS='1' 
    //3.过滤所有非自签酒店 --SQL文中  and bp.ismyhotel=1
    //4.锦江过滤 ID   ConfigurationManager.AppSettings["FilterJinJiangHotels"]
    //5.当选择房控人员时，过滤房控人员下面所有当天计划全部被关闭，并且关闭人全部是销售人员的酒店；  代码过滤    
    public DataTable FilterHotelsByManager(DataTable dtResult, DataTable dtSales)
    {
        DataTable dtNewResult = new DataTable();
        dtNewResult.Columns.Add("PROP");
        dtNewResult.Columns.Add("CITYID");
        dtNewResult.Columns.Add("PROP_NAME_ZH");

        #region
        if (dtResult != null && dtResult.Rows.Count > 0)
        {
            //获取需要过滤的锦江酒店列表
            string FilterJJHotels = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["FilterJinJiangHotels"])) ? "" : ConfigurationManager.AppSettings["FilterJinJiangHotels"].ToString().Trim();
            bool IsFilter = false;
            bool IsFlag = false;
            string hotelId = "";
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                IsFlag = false;
                IsFilter = false;
                if (hotelId == "" || hotelId != dtResult.Rows[i]["prop"].ToString())
                {
                    #region 过滤锦江
                    if (!string.IsNullOrEmpty(FilterJJHotels))
                    {
                        for (int j = 0; j < FilterJJHotels.Split(',').Length; j++)
                        {
                            if (dtResult.Rows[i]["prop"].ToString() == FilterJJHotels.Split(',')[j].ToString())
                            {
                                IsFilter = true;
                                break;
                            }
                        }
                    }
                    #endregion

                    #region
                    if (!IsFilter)
                    {
                        if (dtNewResult != null && dtNewResult.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtNewResult.Rows.Count; j++)
                            {
                                if (dtResult.Rows[i]["prop"].ToString() == dtNewResult.Rows[j]["PROP"].ToString())
                                {
                                    IsFlag = true;
                                    break;
                                }
                            }
                        }
                        if (!IsFlag)
                        {
                            #region
                            hotelId = dtResult.Rows[i]["prop"].ToString();
                            DataRow[] rowsAll = dtResult.Select("prop='" + hotelId + "'");//获取当前酒店所有计划
                            DataRow[] rowsCloseAll = dtResult.Select("prop='" + hotelId + "' and status=0");//获取当前酒店所有已关闭的计划
                            if (rowsAll.Length > 0 && rowsCloseAll.Length > 0)
                            {
                                if (rowsAll.Length == rowsCloseAll.Length)//计划全部关闭  且  关闭人为销售人员
                                {
                                    int count = 0;
                                    for (int j = 0; j < rowsCloseAll.Length; j++)
                                    {
                                        DataRow[] salesRow = dtSales.Select("REVALUE_ALL like '%" + rowsCloseAll[j]["MODIFIER"].ToString() + "%'");
                                        if (salesRow.Length > 0)
                                        {
                                            count++;
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (count != rowsCloseAll.Length)
                                    {
                                        #region
                                        DataRow dr = dtNewResult.NewRow();
                                        dr["PROP"] = hotelId;
                                        dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                        dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                        dtNewResult.Rows.Add(dr);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    #region
                                    DataRow dr = dtNewResult.NewRow();
                                    dr["PROP"] = hotelId;
                                    dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                    dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                    dtNewResult.Rows.Add(dr);
                                    #endregion
                                }
                            }
                            else
                            {
                                #region
                                DataRow dr = dtNewResult.NewRow();
                                dr["PROP"] = hotelId;
                                dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                dtNewResult.Rows.Add(dr);
                                #endregion
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion
        return dtNewResult;
    }

    /// <summary>
    /// 过滤 锦江  以及 重复的酒店 
    /// </summary>
    /// <returns></returns>
    public static DataTable FilterHotelsByAllCitysManager(DataTable dtResult, DataTable dtSales)
    {
        DataTable dtNewResult = new DataTable();
        dtNewResult.Columns.Add("PROP");
        dtNewResult.Columns.Add("CITYID");
        dtNewResult.Columns.Add("CITYNAME");
        dtNewResult.Columns.Add("PROP_NAME_ZH");
        if (dtResult != null && dtResult.Rows.Count > 0)
        {
            //获取需要过滤的锦江酒店列表
            string FilterJJHotels = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["FilterJinJiangHotels"])) ? "" : ConfigurationManager.AppSettings["FilterJinJiangHotels"].ToString().Trim();
            bool IsFilter = false;
            bool IsFlag = false;
            string hotelId = "";
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                IsFlag = false;
                IsFilter = false;
                if (hotelId == "" || hotelId != dtResult.Rows[i]["prop"].ToString())
                {
                    #region 过滤锦江
                    if (!string.IsNullOrEmpty(FilterJJHotels))
                    {
                        for (int j = 0; j < FilterJJHotels.Split(',').Length; j++)
                        {
                            if (dtResult.Rows[i]["prop"].ToString() == FilterJJHotels.Split(',')[j].ToString())
                            {
                                IsFilter = true;
                                break;
                            }
                        }
                    }
                    #endregion

                    #region
                    if (!IsFilter)
                    {
                        if (dtNewResult != null && dtNewResult.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtNewResult.Rows.Count; j++)
                            {
                                if (dtResult.Rows[i]["prop"].ToString() == dtNewResult.Rows[j]["PROP"].ToString())
                                {
                                    IsFlag = true;
                                    break;
                                }
                            }
                        }
                        if (!IsFlag)
                        {
                            #region
                            hotelId = dtResult.Rows[i]["prop"].ToString();
                            DataRow[] rowsAll = dtResult.Select("prop='" + hotelId + "'");//获取当前酒店所有计划
                            DataRow[] rowsCloseAll = dtResult.Select("prop='" + hotelId + "' and status=0");//获取当前酒店所有已关闭的计划
                            if (rowsAll.Length > 0 && rowsCloseAll.Length > 0)
                            {
                                if (rowsAll.Length == rowsCloseAll.Length)//计划全部关闭  且  关闭人为销售人员
                                {
                                    int count = 0;
                                    for (int j = 0; j < rowsCloseAll.Length; j++)
                                    {
                                        DataRow[] salesRow = dtSales.Select("REVALUE_ALL like '%" + rowsCloseAll[j]["MODIFIER"].ToString() + "%'");
                                        if (salesRow.Length > 0)
                                        {
                                            count++;
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (count != rowsCloseAll.Length)
                                    {
                                        #region
                                        DataRow dr = dtNewResult.NewRow();
                                        dr["PROP"] = hotelId;
                                        dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                        dr["CITYNAME"] = dtResult.Rows[i]["name_cn"].ToString();
                                        dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                        dtNewResult.Rows.Add(dr);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    #region
                                    DataRow dr = dtNewResult.NewRow();
                                    dr["PROP"] = hotelId;
                                    dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                    dr["CITYNAME"] = dtResult.Rows[i]["name_cn"].ToString();
                                    dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                    dtNewResult.Rows.Add(dr);
                                    #endregion
                                }
                            }
                            else
                            {
                                #region
                                DataRow dr = dtNewResult.NewRow();
                                dr["PROP"] = hotelId;
                                dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                dr["CITYNAME"] = dtResult.Rows[i]["name_cn"].ToString();
                                dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                dtNewResult.Rows.Add(dr);
                                #endregion
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }
        }
        return dtNewResult;
    }

    /// <summary>
    /// 统计每个城市的酒店总数
    /// </summary>
    /// <param name="dtLMConsultResult"></param>
    public static DataTable AssembleAllToDaySelectConsultTextByManager(DataTable dtLMConsultResult)
    {
        DataTable dtNewResult = new DataTable();
        dtNewResult.Columns.Add("PROP");
        dtNewResult.Columns.Add("CITYID");
        dtNewResult.Columns.Add("CITYNAME");
        dtNewResult.Columns.Add("PROPNAMEZH");
        dtNewResult.Columns.Add("LMCount");//LM酒店数

        StringBuilder sb = new StringBuilder();
        //PROP    CITYID    CITYNAME          PROP_NAME_ZH
        if (dtLMConsultResult != null && dtLMConsultResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtLMConsultResult.Rows.Count; i++)
            {
                string cityID = dtLMConsultResult.Rows[i]["CITYID"].ToString();
                DataRow[] rows = dtNewResult.Select("CITYID='" + cityID + "'");
                if (rows.Length <= 0)//当前城市 暂时还未循环
                {
                    DataRow dr = dtNewResult.NewRow();
                    dr["PROP"] = dtLMConsultResult.Rows[i]["PROP"].ToString();
                    dr["CITYID"] = cityID;
                    dr["CITYNAME"] = dtLMConsultResult.Rows[i]["CITYNAME"].ToString();
                    dr["PROPNAMEZH"] = dtLMConsultResult.Rows[i]["PROP_NAME_ZH"].ToString();
                    dr["LMCount"] = "1";
                    dtNewResult.Rows.Add(dr);
                }
                else
                {
                    rows[0]["PROP"] = rows[0]["PROP"] + "," + dtLMConsultResult.Rows[i]["PROP"].ToString();
                    rows[0]["CITYID"] = cityID;
                    rows[0]["CITYNAME"] = dtLMConsultResult.Rows[i]["CITYNAME"].ToString();
                    rows[0]["PROPNAMEZH"] = rows[0]["PROPNAMEZH"] + "," + dtLMConsultResult.Rows[i]["PROP_NAME_ZH"].ToString();
                    rows[0]["LMCount"] = int.Parse(rows[0]["LMCount"].ToString()) + 1;
                }
            }
        }
        return dtNewResult;
    }

    /// <summary>
    /// 获取历史记录中  每个询房人员对应的酒店数（已询数）
    /// </summary>
    /// <param name="SDate"></param>
    /// <returns></returns>
    public DataTable GetConsultRoomByTimeManagerHistory(string SDate)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.SDate = SDate;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetConsultRoomByTimeManagerHistory(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 获取历史记录中  每个询房人员对应的酒店详细列表（已询酒店列表）
    /// </summary>
    /// <param name="SDate"></param>
    /// <returns></returns>
    public DataTable GetConsultRoomByTimeAndUserManagerHistory(string SDate, string CheckUserName)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.SDate = SDate;
        hotelsConsultRoomManagerDBEntity.CheckUserName = CheckUserName;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetConsultRoomByTimeAndUserManagerHistory(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 获取历史记录中  All  所有城市的酒店列表数
    /// </summary>
    /// <param name="SDate"></param>
    /// <returns></returns>
    public DataTable GetAllCityByTimeManagerHistory(string SDate)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.SDate = SDate;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetAllCityByTimeManagerHistory(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 获取某个房控人员 当天已询酒店列表
    /// </summary>
    /// <returns></returns>
    public DataTable GetConsultHotelsBySales(string salesID, string SDate, string EDate)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.CheckUserName = salesID;
        hotelsConsultRoomManagerDBEntity.SDate = SDate;
        hotelsConsultRoomManagerDBEntity.EDate = EDate;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetConsultHotelsBySales(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }


    /// <summary>
    /// 获取每个人18点后更新的酒店数
    /// </summary>
    /// <returns></returns>
    public DataTable GetCheck18EXDConsultHotelCountLogsByManager(string SDate, string EDate)
    {
        DataTable dtNewResult = new DataTable();
        _HotelsConsultRoomManagerEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _HotelsConsultRoomManagerEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _HotelsConsultRoomManagerEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _HotelsConsultRoomManagerEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity = new List<HotelsConsultRoomManagerDBEntity>();
        HotelsConsultRoomManagerDBEntity hotelsConsultRoomManagerDBEntity = new HotelsConsultRoomManagerDBEntity();
        hotelsConsultRoomManagerDBEntity.SDate = SDate;
        hotelsConsultRoomManagerDBEntity.EDate = EDate;
        _HotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Add(hotelsConsultRoomManagerDBEntity);
        DataTable dtResult = HotelsConsultRoomManagerBP.GetCheck18EXDConsultHotelCountLogsByManager(_HotelsConsultRoomManagerEntity).QueryResult.Tables[0];

        return dtResult;
    }
    #endregion

}