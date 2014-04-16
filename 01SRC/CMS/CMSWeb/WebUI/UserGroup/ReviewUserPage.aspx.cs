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

public partial class ReviewUserPage : BasePage
{
    UserEntity _userEntity = new UserEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["TYPE"]))
            {
                ViewState["UserID"] = "";
                ViewState["RegistStart"] = "";
                ViewState["RegistEnd"] = "";

                ViewState["LoginStart"] = "";
                ViewState["LoginEnd"] = "";

                ViewState["LoginSizeStart"] = "";
                ViewState["LoginSizeEnd"] = "";

                ViewState["RegChannelID"] = "";
                ViewState["PlatformID"] = "";

                ViewState["OrderFrom"] = "";
                ViewState["OrderTo"] = "";
                ViewState["OrderSucFrom"] = "";
                ViewState["OrderSucTo"] = "";

                //ViewState["OrderCount"] = "";
                //ViewState["OrderSucCount"] = "";

                ViewState["Type"] = Request.QueryString["TYPE"].ToString();
                ViewState["Data"] = (String.IsNullOrEmpty(Request.QueryString["DATA"])) ? "" : Request.QueryString["DATA"].ToString();
                string pkn = HttpUtility.UrlDecode(Request.QueryString["pknm"], Encoding.GetEncoding("GB2312"));
                string atk = HttpUtility.UrlDecode(Request.QueryString["atk"], Encoding.GetEncoding("GB2312"));
                string att = HttpUtility.UrlDecode(Request.QueryString["att"], Encoding.GetEncoding("GB2312"));
                string pkf = HttpUtility.UrlDecode(Request.QueryString["pkf"], Encoding.GetEncoding("GB2312"));
                string pkt = HttpUtility.UrlDecode(Request.QueryString["pkt"], Encoding.GetEncoding("GB2312"));
                string tkt = HttpUtility.UrlDecode(Request.QueryString["tkt"], Encoding.GetEncoding("GB2312"));
                ViewState["packagename"] = pkn;//优惠券礼包名
                ViewState["amountfrom"] = atk;
                ViewState["amountto"] = att;
                ViewState["pickfromdate"] = pkf;
                ViewState["picktodate"] = pkt;
                ViewState["tickettime"] = tkt;

                AspNetPager1.AlwaysShow = true;
                BindReviewUserListGrid();
            }
            else
            {
                //hidSelectType.Value = "0";
                //BindReviewUserListGrid();
                AspNetPager1.AlwaysShow = false;

                ViewState["Type"] = "";
                ViewState["Data"] = "";
                ViewState["packagename"] = "";
                ViewState["amountfrom"] = "";
                ViewState["amountto"] = "";
                ViewState["pickfromdate"] = "";
                ViewState["picktodate"] = "";
                ViewState["tickettime"] = "";
            }

            BindALLDropDownList();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetControlEnable()", true);
            //this.Page.ClientScript.RegisterOnSubmitStatement(this.Page.GetType(), "btnLoad", "BtnLoadStyle()");
        }
        else
        {
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "btnLoad", "BtnCompleteStyle()");
        }
        
    }

    private void BindALLDropDownList()
    {
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        DataSet dsResult = UserSearchBP.GetRegChannelList(_userEntity).QueryResult;
        DataRow drTemp = dsResult.Tables[0].NewRow();
        drTemp["REGCHANELCODE"] = DBNull.Value;
        drTemp["REGCHANELNM"] = "不限制";
        dsResult.Tables[0].Rows.InsertAt(drTemp, 0);

        ddpRegChannelList.DataTextField = "REGCHANELNM";
        ddpRegChannelList.DataValueField = "REGCHANELCODE";
        ddpRegChannelList.DataSource = dsResult;
        ddpRegChannelList.DataBind();

        DataSet dsPlatFormResult = UserSearchBP.GetPlatFormList(_userEntity).QueryResult;
        DataRow drPlatFormTemp = dsPlatFormResult.Tables[0].NewRow();
        drPlatFormTemp["PLATFORMCODE"] = DBNull.Value;
        drPlatFormTemp["PLATFORMNM"] = "不限制";
        dsPlatFormResult.Tables[0].Rows.InsertAt(drPlatFormTemp, 0);

        ddpPlatformList.DataTextField = "PLATFORMNM";
        ddpPlatformList.DataValueField = "PLATFORMCODE";
        ddpPlatformList.DataSource = dsPlatFormResult;
        ddpPlatformList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewUserListGrid();
    }

    private void BindReviewUserListGrid()
    {
        //messageContent.InnerHtml = "";

        //if (!checkNum(txtUserCountFrom.Value.Trim()) || !checkNum(txtUserCountTo.Value.Trim()))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("ErrorNum").ToString();
        //    return;
        //}

        //if ((!String.IsNullOrEmpty(txtUserCountFrom.Value.ToString().Trim()) && !String.IsNullOrEmpty(txtUserCountTo.Value.ToString().Trim())) && (int.Parse(txtUserCountFrom.Value.ToString().Trim()) > int.Parse(txtUserCountTo.Value.ToString().Trim())))
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        //    return;
        //}

        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();

        usergroupEntity.UserID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UserID"].ToString())) ? null : ViewState["UserID"].ToString();
        usergroupEntity.RegistStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegistStart"].ToString())) ? null : ViewState["RegistStart"].ToString();
        usergroupEntity.RegistEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegistEnd"].ToString())) ? null : ViewState["RegistEnd"].ToString();
        usergroupEntity.RegChannelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegChannelID"].ToString())) ? null : ViewState["RegChannelID"].ToString();
        usergroupEntity.PlatformID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatformID"].ToString())) ? null : ViewState["PlatformID"].ToString();

        usergroupEntity.OrderFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderFrom"].ToString())) ? null : ViewState["OrderFrom"].ToString();
        usergroupEntity.OrderTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderTo"].ToString())) ? null : ViewState["OrderTo"].ToString();
        usergroupEntity.OrderSucFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucFrom"].ToString())) ? null : ViewState["OrderSucFrom"].ToString();
        usergroupEntity.OrderSucTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucTo"].ToString())) ? null : ViewState["OrderSucTo"].ToString();

        //usergroupEntity.OrderCount = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderCount"].ToString())) ? null : ViewState["OrderCount"].ToString();
        //usergroupEntity.OrderSuccCount = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucCount"].ToString())) ? null : ViewState["OrderSucCount"].ToString();

        usergroupEntity.LoginStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginStart"].ToString())) ? null : ViewState["LoginStart"].ToString();
        usergroupEntity.LoginEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginEnd"].ToString())) ? null : ViewState["LoginEnd"].ToString();

        usergroupEntity.LoginSizeStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginSizeStart"].ToString())) ? null : ViewState["LoginSizeStart"].ToString();
        usergroupEntity.LoginSizeEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginSizeEnd"].ToString())) ? null : ViewState["LoginSizeEnd"].ToString();

        usergroupEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Type"].ToString())) ? null : ViewState["Type"].ToString();
        usergroupEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Data"].ToString())) ? null : ViewState["Data"].ToString();

        usergroupEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        usergroupEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        usergroupEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        usergroupEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        usergroupEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        usergroupEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();


        if (gridViewCSReviewUserList  != null && gridViewCSReviewUserList.Columns.Count > 0 && String.IsNullOrEmpty(usergroupEntity.TicketType) && String.IsNullOrEmpty(usergroupEntity.OrderFrom) && String.IsNullOrEmpty(usergroupEntity.OrderTo) && String.IsNullOrEmpty(usergroupEntity.OrderSucFrom) && String.IsNullOrEmpty(usergroupEntity.OrderSucTo))
        {
            gridViewCSReviewUserList.Columns[7].Visible = false;
            gridViewCSReviewUserList.Columns[8].Visible = false;
        }
        else
        {
            gridViewCSReviewUserList.Columns[7].Visible = true;
            gridViewCSReviewUserList.Columns[8].Visible = true;
        }


        _userEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _userEntity.PageSize = gridViewCSReviewUserList.PageSize;
        //usergroupEntity.SelectType = hidSelectType.Value;
        _userEntity.UserDBEntity.Add(usergroupEntity);
        DataSet dsResult = UserSearchBP.ReviewSelect(_userEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                if (dsResult.Tables[0].Rows[i]["LOGINMOBILE"].ToString().Trim() == "" && dsResult.Tables[0].Rows[i]["THIRD_PARTY_VENDOR"].ToString().Trim() == "")
                {
                    dsResult.Tables[0].Rows[i]["LOGINMOBILE"] = "游客";
                }
                else if (dsResult.Tables[0].Rows[i]["LOGINMOBILE"].ToString().Trim() == "" && dsResult.Tables[0].Rows[i]["THIRD_PARTY_VENDOR"].ToString().Trim() == "QUNAR")
                {
                    dsResult.Tables[0].Rows[i]["LOGINMOBILE"] = "QUNAR";
                }
            }
        }
        gridViewCSReviewUserList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewUserList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSReviewUserList.DataBind();
         

        

        AspNetPager1.PageSize = gridViewCSReviewUserList.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);

        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "setScript", "SetAClickEvent()", true);  
        //lbCount.Text = dsResult.Tables[0].Rows.Count.ToString();
    }

    private int CountLmSystemLog()
    {
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();

        usergroupEntity.UserID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UserID"].ToString())) ? null : ViewState["UserID"].ToString();
        usergroupEntity.RegistStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegistStart"].ToString())) ? null : ViewState["RegistStart"].ToString();
        usergroupEntity.RegistEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegistEnd"].ToString())) ? null : ViewState["RegistEnd"].ToString();
        usergroupEntity.RegChannelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegChannelID"].ToString())) ? null : ViewState["RegChannelID"].ToString();
        usergroupEntity.PlatformID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatformID"].ToString())) ? null : ViewState["PlatformID"].ToString();

        usergroupEntity.OrderFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderFrom"].ToString())) ? null : ViewState["OrderFrom"].ToString();
        usergroupEntity.OrderTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderTo"].ToString())) ? null : ViewState["OrderTo"].ToString();
        usergroupEntity.OrderSucFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucFrom"].ToString())) ? null : ViewState["OrderSucFrom"].ToString();
        usergroupEntity.OrderSucTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucTo"].ToString())) ? null : ViewState["OrderSucTo"].ToString();

        //usergroupEntity.OrderCount = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderCount"].ToString())) ? null : ViewState["OrderCount"].ToString();
        //usergroupEntity.OrderSuccCount = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucCount"].ToString())) ? null : ViewState["OrderSucCount"].ToString();

        usergroupEntity.LoginStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginStart"].ToString())) ? null : ViewState["LoginStart"].ToString();
        usergroupEntity.LoginEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginEnd"].ToString())) ? null : ViewState["LoginEnd"].ToString();

        usergroupEntity.LoginSizeStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginSizeStart"].ToString())) ? null : ViewState["LoginSizeStart"].ToString();
        usergroupEntity.LoginSizeEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginSizeEnd"].ToString())) ? null : ViewState["LoginSizeEnd"].ToString();

        usergroupEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Type"].ToString())) ? null : ViewState["Type"].ToString();
        usergroupEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Data"].ToString())) ? null : ViewState["Data"].ToString();
        usergroupEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        usergroupEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        usergroupEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        usergroupEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        usergroupEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        usergroupEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();

        _userEntity.UserDBEntity.Add(usergroupEntity);
        DataSet dsResult = UserSearchBP.ReviewSelectCount(_userEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
        }

        return 0;
    }

    protected void gridViewCSReviewUserGroupList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    //    //this.gridViewRegion.PageIndex = e.NewPageIndex;
    //    //BindGridView();

    //    //执行循环，保证每条数据都可以更新
    //    for (int i = 0; i <= gridViewCSChannelList.Rows.Count; i++)
    //    {
    //        //首先判断是否是数据行
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //当鼠标停留时更改背景色
    //            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
    //            //当鼠标移开时还原背景色
    //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
    //        }
    //    }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        string strTelphone = txtTelphone.Value.Trim().Replace('，', ',');
        string strTelList = "";
        if (strTelphone.IndexOf(',') >= 0)
        {
            string[] telList = strTelphone.Split(',');
            foreach (string strTemp in telList)
            {
                strTelList = (String.IsNullOrEmpty(strTemp)) ? strTelList : strTelList + strTemp + ",";
            }
        }
        else if (strTelphone.Length > 0)
        {
            strTelList = strTelphone + ",";
        }

        ViewState["UserID"] = strTelList;
        //if (chkCreateUnTime.Checked)
        //{
        //    ViewState["RegistStart"] = "";
        //    ViewState["RegistEnd"] = "";
        //}
        //else
        //{
        //    ViewState["RegistStart"] = dpCreateStart.Value;
        //    ViewState["RegistEnd"] = dpCreateEnd.Value;
        //}

        ViewState["RegistStart"] = dpCreateStart.Value;
        ViewState["RegistEnd"] = dpCreateEnd.Value;

        ViewState["LoginStart"] = dpLoginStart.Value;
        ViewState["LoginEnd"] = dpLoginEnd.Value;

        ViewState["LoginSizeStart"] = dpLoginSizeStart.Value;
        ViewState["LoginSizeEnd"] = dpLoginSizeEnd.Value;

        ViewState["RegChannelID"] = ddpRegChannelList.SelectedValue;
        ViewState["PlatformID"] = ddpPlatformList.SelectedValue;

        //ViewState["OrderCount"] = hidOrdType.Value.Trim();// rdbtnlist.SelectedValue;
        //ViewState["OrderSucCount"] = hidSucOrdType.Value.Trim();// rdbtnSuclist.SelectedValue;

        if ("0".Equals(hidOrdType.Value.Trim()))
        {
             ViewState["OrderFrom"] = "";
             ViewState["OrderTo"] = "";
        }
        else if ("1".Equals(hidOrdType.Value.Trim()))
        {
            if (!ChkNumberQuery(txtOrdFrom.Value.Trim()) || !ChkNumberQuery(txtOrdTo.Value.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
                return;
            }

            ViewState["OrderFrom"] = String.IsNullOrEmpty(txtOrdFrom.Value.Trim()) ? "1" : txtOrdFrom.Value.Trim();
            ViewState["OrderTo"] = String.IsNullOrEmpty(txtOrdTo.Value.Trim()) ? "999999" : txtOrdTo.Value.Trim();
        }
        else if ("2".Equals(hidOrdType.Value.Trim()))
        {
            ViewState["OrderFrom"] = "0";
            ViewState["OrderTo"] = "0";
        }
        else
        {
             ViewState["OrderFrom"] = "";
             ViewState["OrderTo"] = "";
        }

        if ("0".Equals(hidSucOrdType.Value.Trim()))
        {
            ViewState["OrderSucFrom"] = "";
            ViewState["OrderSucTo"] = "";
        }
        else if ("1".Equals(hidSucOrdType.Value.Trim()))
        {
            if (!ChkNumberQuery(txtSucOrdFrom.Value.Trim()) || !ChkNumberQuery(txtSucOrdTo.Value.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
                return;
            }
            ViewState["OrderSucFrom"] = String.IsNullOrEmpty(txtSucOrdFrom.Value.Trim()) ? "1" : txtSucOrdFrom.Value.Trim();
            ViewState["OrderSucTo"] = String.IsNullOrEmpty(txtSucOrdTo.Value.Trim()) ? "999999" : txtSucOrdTo.Value.Trim();
        }
        else if ("2".Equals(hidSucOrdType.Value.Trim()))
        {
            ViewState["OrderSucFrom"] = "0";
            ViewState["OrderSucTo"] = "0";
        }
        else
        {
            ViewState["OrderSucFrom"] = "";
            ViewState["OrderSucTo"] = "";
        }

        ViewState["Type"] = "";
        ViewState["Data"] = "";
        ViewState["packagename"] = "";
        ViewState["amountfrom"] = "";
        ViewState["amountto"] = "";
        ViewState["pickfromdate"] = "";
        ViewState["picktodate"] = "";
        ViewState["tickettime"] = "";

        AspNetPager1.AlwaysShow = true;
        AspNetPager1.CurrentPageIndex = 1;
        //hidSelectType.Value = "1";
        BindReviewUserListGrid();
        //UpdatePanel2.Update();
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

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity usergroupEntity = new UserDBEntity();

        usergroupEntity.UserID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["UserID"].ToString())) ? null : ViewState["UserID"].ToString();
        usergroupEntity.RegistStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegistStart"].ToString())) ? null : ViewState["RegistStart"].ToString();
        usergroupEntity.RegistEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegistEnd"].ToString())) ? null : ViewState["RegistEnd"].ToString();
        usergroupEntity.RegChannelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["RegChannelID"].ToString())) ? null : ViewState["RegChannelID"].ToString();
        usergroupEntity.PlatformID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatformID"].ToString())) ? null : ViewState["PlatformID"].ToString();

        usergroupEntity.OrderFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderFrom"].ToString())) ? null : ViewState["OrderFrom"].ToString();
        usergroupEntity.OrderTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderTo"].ToString())) ? null : ViewState["OrderTo"].ToString();
        usergroupEntity.OrderSucFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucFrom"].ToString())) ? null : ViewState["OrderSucFrom"].ToString();
        usergroupEntity.OrderSucTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucTo"].ToString())) ? null : ViewState["OrderSucTo"].ToString();

        //usergroupEntity.OrderCount = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderCount"].ToString())) ? null : ViewState["OrderCount"].ToString();
        //usergroupEntity.OrderSuccCount = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderSucCount"].ToString())) ? null : ViewState["OrderSucCount"].ToString();

        usergroupEntity.LoginStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginStart"].ToString())) ? null : ViewState["LoginStart"].ToString();
        usergroupEntity.LoginEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginEnd"].ToString())) ? null : ViewState["LoginEnd"].ToString();

        usergroupEntity.LoginSizeStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginSizeStart"].ToString())) ? null : ViewState["LoginSizeStart"].ToString();
        usergroupEntity.LoginSizeEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["LoginSizeEnd"].ToString())) ? null : ViewState["LoginSizeEnd"].ToString();

        usergroupEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Type"].ToString())) ? null : ViewState["Type"].ToString();
        usergroupEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Data"].ToString())) ? null : ViewState["Data"].ToString();
        usergroupEntity.PackageName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["packagename"].ToString())) ? null : ViewState["packagename"].ToString();
        usergroupEntity.AmountFrom = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountfrom"].ToString())) ? null : ViewState["amountfrom"].ToString();
        usergroupEntity.AmountTo = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["amountto"].ToString())) ? null : ViewState["amountto"].ToString();
        usergroupEntity.PickfromDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["pickfromdate"].ToString())) ? null : ViewState["pickfromdate"].ToString();
        usergroupEntity.PicktoDate = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["picktodate"].ToString())) ? null : ViewState["picktodate"].ToString();
        usergroupEntity.TicketTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["tickettime"].ToString())) ? null : ViewState["tickettime"].ToString();

        //usergroupEntity.SelectType = hidSelectType.Value;
        _userEntity.UserDBEntity.Add(usergroupEntity);
        DataSet dsResult = UserSearchBP.ExportReviewSelect(_userEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                if (dsResult.Tables[0].Rows[i]["LOGINMOBILE"].ToString().Trim() == "" && dsResult.Tables[0].Rows[i]["THIRD_PARTY_VENDOR"].ToString().Trim() == "")
                {
                    dsResult.Tables[0].Rows[i]["LOGINMOBILE"] = "游客";
                }
                else if (dsResult.Tables[0].Rows[i]["LOGINMOBILE"].ToString().Trim() == "" && dsResult.Tables[0].Rows[i]["THIRD_PARTY_VENDOR"].ToString().Trim() == "QUNAR")
                {
                    dsResult.Tables[0].Rows[i]["LOGINMOBILE"] = "QUNAR";
                }
            }
        }


        CommonFunction.ExportExcelForLM(dsResult);
        //background.Style.Add("display", "none");
        //progressBar.Style.Add("display", "none");
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
        this.gridViewCSReviewUserList.PageIndex = e.NewPageIndex;
        BindReviewUserListGrid();
    }
}