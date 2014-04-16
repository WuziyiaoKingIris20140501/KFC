using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;
using HotelVp.Common.Json.Serialization;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class UserDetailPage : BasePage
{
    UserEntity _userEntity = new UserEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string UserID = "";
            if (!String.IsNullOrEmpty(Request.QueryString["TYPE"]) && "1".Equals(Request.QueryString["TYPE"].ToString()))
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]) && !Request.QueryString["ID"].ToString().Trim().Contains("游客"))
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                    {
                        ViewState["phonenumber"] = Request.QueryString["ID"].ToString().Trim();
                        UserID = GetUserIDFromMobile(Request.QueryString["ID"].ToString().Trim());
                    }
                    else
                    {
                        detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                    }
                }
                //else
                //{
                //    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                //    {
                //        UserID = Request.QueryString["ID"].ToString().Trim();
                //    }
                //    else
                //    {
                //        detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                //    }
                //}
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["TYPE"]) && "2".Equals(Request.QueryString["TYPE"].ToString()))
            {
                if (!String.IsNullOrEmpty(Request.QueryString["UserID"]) && !Request.QueryString["UserID"].ToString().Trim().Contains("游客") && !Request.QueryString["UserID"].ToString().Trim().Contains("QUNAR"))
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["UserID"]))
                    {
                        ViewState["phonenumber"] = Request.QueryString["UserID"].ToString().Trim();
                        UserID = GetUserIDFromMobile(Request.QueryString["UserID"].ToString().Trim());
                    }
                    else
                    {
                        detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                    {
                        UserID = Request.QueryString["ID"].ToString().Trim();
                    }
                    else
                    {
                        detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                    }
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    UserID = Request.QueryString["ID"].ToString().Trim();
                }
                else
                {
                    detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
                }
            }

            hidUserID.Value = UserID;
            ////BindChannelDDL();
            BindUserMainListDetail();
            BindViewCSUserListDetail();

            //
            BindNotUseTicket();
            BindUseTicket();
            // 正确的属性设置方法
            gridViewCSUserListDetail.Attributes.Add("SortExpression", "LMID");
            gridViewCSUserListDetail.Attributes.Add("SortDirection", "DESC");


            //if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            //{
            //    string UserID = "";

            //    //if (!String.IsNullOrEmpty(Request.QueryString["TYPE"]) && "1".Equals(Request.QueryString["TYPE"].ToString()))
            //    //{
            //    //    ViewState["phonenumber"] = Request.QueryString["ID"].ToString().Trim();
            //    //    UserID = GetUserIDFromMobile(Request.QueryString["ID"].ToString().Trim());
            //    //}
            //    //else
            //    //{
            //    //    UserID = Request.QueryString["ID"].ToString().Trim();
            //    //}

            //    if(!String.IsNullOrEmpty(Request.QueryString["TYPE"]) && "1".Equals(Request.QueryString["TYPE"].ToString()))
            //    {
            //        if (!String.IsNullOrEmpty(Request.QueryString["UserID"]) && !Request.QueryString["UserID"].ToString().Trim().Contains("游客"))
            //        {
            //            ViewState["phonenumber"] = Request.QueryString["UserID"].ToString().Trim();
            //            UserID = GetUserIDFromMobile(Request.QueryString["UserID"].ToString().Trim());
            //        }
            //        else
            //        {
            //            UserID = Request.QueryString["ID"].ToString().Trim();
            //        }
            //    }else
            //    {
            //        UserID = Request.QueryString["ID"].ToString().Trim();
            //    }


            //    hidUserID.Value = UserID;
            //    ////BindChannelDDL();
            //    BindUserMainListDetail();
            //    BindViewCSUserListDetail();

            //    //
            //    BindNotUseTicket();
            //    BindUseTicket();
            //    // 正确的属性设置方法
            //    gridViewCSUserListDetail.Attributes.Add("SortExpression", "LMID");
            //    gridViewCSUserListDetail.Attributes.Add("SortDirection", "DESC");
            //}
            //else
            //{
            //    detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //}
        }
        //messageContent.InnerHtml = "";
    }

    private void BindUserMainListDetail()
    {
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity userGroupDBEntity = new UserDBEntity();

        userGroupDBEntity.UserID = hidUserID.Value;

        _userEntity.UserDBEntity.Add(userGroupDBEntity);

        DataSet dsMainResult = UserSearchBP.UserMainListSelect(_userEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        lbUserID.Text = dsMainResult.Tables[0].Rows[0]["ID"].ToString();
        lbLoginMobile.Text = dsMainResult.Tables[0].Rows[0]["LOGINMOBILE"].ToString();

        ViewState["phonenumber"] = dsMainResult.Tables[0].Rows[0]["LOGINMOBILE"].ToString();//手机号码

        lbSignKey.Text = dsMainResult.Tables[0].Rows[0]["SIGN_KEY"].ToString();
        lbSignDate.Text = dsMainResult.Tables[0].Rows[0]["SIGNDATE"].ToString();

        lbCreateDT.Text = dsMainResult.Tables[0].Rows[0]["CREATEDT"].ToString();

        lbAllCount.Text = dsMainResult.Tables[0].Rows[0]["ALLCOUNT"].ToString();
        lbCompleCount.Text = dsMainResult.Tables[0].Rows[0]["COMPLECOUNT"].ToString();

        lbRegchanel.Text = dsMainResult.Tables[0].Rows[0]["REGCHANELNM"].ToString();
        lbDvtoken.Text = dsMainResult.Tables[0].Rows[0]["DVTOKEN"].ToString();
        lbPlatform.Text = dsMainResult.Tables[0].Rows[0]["PLATFORMNM"].ToString();
        lbVrsion.Text = dsMainResult.Tables[0].Rows[0]["VRSION"].ToString();

        lbUserCash.Text = dsMainResult.Tables[0].Rows[0]["USERCASH"].ToString();
    }

    private string GetUserIDFromMobile(string strMobile)
    {
        string strResult = "";

        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity userGroupDBEntity = new UserDBEntity();

        userGroupDBEntity.LoginMobile = strMobile;
        _userEntity.UserDBEntity.Add(userGroupDBEntity);

        DataSet dsDetailResult = UserSearchBP.GetUserIDFromMobile(_userEntity).QueryResult;
        if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 0)
        {
            strResult = dsDetailResult.Tables[0].Rows[0][0].ToString();
        }

        return strResult;
    }

    private void BindViewCSUserListDetail()
    {
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity userGroupDBEntity = new UserDBEntity();

        userGroupDBEntity.UserID = hidUserID.Value;

        _userEntity.UserDBEntity.Add(userGroupDBEntity);

        DataSet dsDetailResult = UserSearchBP.UserDetailListSelect(_userEntity).QueryResult;

        // 获取GridView排序数据列及排序方向
        string sortExpression = gridViewCSUserListDetail.Attributes["SortExpression"];
        string sortDirection = gridViewCSUserListDetail.Attributes["SortDirection"];
        // 根据GridView排序数据列及排序方向设置显示的默认数据视图
        if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
        {
            dsDetailResult.Tables[0].DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
        }

        gridViewCSUserListDetail.DataSource = dsDetailResult.Tables[0].DefaultView;
        gridViewCSUserListDetail.DataKeyNames = new string[] { "LMID" };//主键
        gridViewCSUserListDetail.DataBind();
    }

    protected void gridViewCSUserListDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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
        string getResult = string.Empty;
        //string strMobile = this.lbLoginMobile.Text;
        //getResult = getSignByPhoneForCC(strMobile);

        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity userGroupDBEntity = new UserDBEntity();
        userGroupDBEntity.LoginMobile = this.lbLoginMobile.Text.Trim();

        _userEntity.UserDBEntity.Add(userGroupDBEntity);
        _userEntity = UserSearchBP.getSignByPhoneForCC(_userEntity);
        getResult = _userEntity.UserDBEntity[0].SignKey;
        if (getResult == "")
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
        }
        else
        {
            this.lbSignKey.Text = getResult;
        }
    }

    public string getSignByPhoneForCC(string userPhoneNumber)
    {
        bool boolResult = false;
        try
        {
            string url = JsonRequestURLBuilder.QueryUserSign();

            string postDataStr = "{\"LmLoginRQ\":{\"loginMobile\":\"" + userPhoneNumber + "\"}}";
            CallWebPage callWebPage = new CallWebPage();
            string strJson = callWebPage.CallWebByURL(url, postDataStr);


            //解析json数据
            JObject o = JObject.Parse(strJson);
            JObject oLoginMember = (JObject)o.SelectToken("LoginLmRS[0]");

            string strSign = string.Empty;
            if (oLoginMember.SelectToken("result.success") != null)
            {
                boolResult = (bool)oLoginMember.SelectToken("result.success");
                if (boolResult == true)
                {
                    if (oLoginMember.SelectToken("signKey") != null)
                    {
                        strSign = oLoginMember.SelectToken("signKey").ToString().Trim('"');
                    }
                }
                else
                {
                    if (oLoginMember.SelectToken("result.errormsg") != null)
                    {
                        strSign = oLoginMember.SelectToken("result.errormsg").ToString().Trim('"');
                    }
                }
            }
            return strSign;
        }
        catch
        {
            return "";
        }

    }

    protected void gridViewCSUserListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSUserListDetail.PageIndex = e.NewPageIndex;
        BindViewCSUserListDetail();
    }

    protected void gridViewCSUserListDetail_Sorting(object sender, GridViewSortEventArgs e)
    {
        // 从事件参数获取排序数据列
        string sortExpression = e.SortExpression.ToString();

        // 假定为排序方向为“顺序”
        string sortDirection = "ASC";

        // “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
        if (sortExpression == gridViewCSUserListDetail.Attributes["SortExpression"])
        {
            //获得下一次的排序状态
            sortDirection = (gridViewCSUserListDetail.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
        }

        // 重新设定GridView排序数据列及排序方向
        gridViewCSUserListDetail.Attributes["SortExpression"] = sortExpression;
        gridViewCSUserListDetail.Attributes["SortDirection"] = sortDirection;

        BindViewCSUserListDetail();
    }



    #region 增加优惠券的使用统计功能
    //绑定
    private void BindNotUseTicket()
    {
        DataTable dt = createNotUseTicketDataTable();
        GridviewControl.GridViewDataBind(this.gridViewNotUseTicket, dt);
    }

    //未使用的优惠券
    private DataSet getNotUseTicketData()
    {
        string UserID = ViewState["phonenumber"].ToString();
        string sql = "select PACKAGECODE,PACKAGENAME,TICKETCODE,USERID,TICKETUSERCODE,STATUS,TICKETAMT,FLAG,TICKETRULECODE,STARTDATE,ENDDATE,TICKETRULEDESC from v_lm_ticket_rule where STATUS=0 and userid='" + UserID + "'";
        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;
    }

    public DataTable createNotUseTicketDataTable()
    {
        DataTable getDataTable = getNotUseTicketData().Tables[0];
        DataTable dt = new DataTable();
        DataColumn PACKAGECODE_dc = new DataColumn("PACKAGECODE");
        DataColumn PACKAGENAME_dc = new DataColumn("PACKAGENAME");
        DataColumn TICKETCODE_dc = new DataColumn("TICKETCODE");
        DataColumn USERID_dc = new DataColumn("USERID");
        DataColumn TICKETUSERCODE_dc = new DataColumn("TICKETUSERCODE");
        DataColumn STATUS_dc = new DataColumn("STATUS");
        DataColumn TICKETAMT_dc = new DataColumn("TICKETAMT");
        DataColumn FLAG_dc = new DataColumn("FLAG");
        DataColumn TICKETRULECODE_dc = new DataColumn("TICKETRULECODE");
        DataColumn STARTDATE_dc = new DataColumn("STARTDATE");
        DataColumn ENDDATE_dc = new DataColumn("ENDDATE");
        DataColumn TICKETRULEDESC_dc = new DataColumn("TICKETRULEDESC");

        dt.Columns.Add(PACKAGECODE_dc);
        dt.Columns.Add(PACKAGENAME_dc);
        dt.Columns.Add(TICKETCODE_dc);
        dt.Columns.Add(USERID_dc);
        dt.Columns.Add(TICKETUSERCODE_dc);
        dt.Columns.Add(STATUS_dc);
        dt.Columns.Add(TICKETAMT_dc);
        dt.Columns.Add(FLAG_dc);
        dt.Columns.Add(TICKETRULECODE_dc);
        dt.Columns.Add(STARTDATE_dc);
        dt.Columns.Add(ENDDATE_dc);
        dt.Columns.Add(TICKETRULEDESC_dc);

        for (int i = 0; i < getDataTable.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();

            dr["PACKAGECODE"] = getDataTable.Rows[i]["PACKAGECODE"];
            //dr["PACKAGENAME"] = getDataTable.Rows[i]["PACKAGENAME"];
            string strTempPackage = getDataTable.Rows[i]["PACKAGENAME"].ToString();
            strTempPackage = getDataTable.Rows[i]["PACKAGECODE"].ToString() + "——" + strTempPackage;
            dr["PACKAGENAME"] = strTempPackage;

            dr["TICKETCODE"] = getDataTable.Rows[i]["TICKETCODE"];
            dr["USERID"] = getDataTable.Rows[i]["USERID"];
            dr["TICKETUSERCODE"] = getDataTable.Rows[i]["TICKETUSERCODE"];
            dr["STATUS"] = getDataTable.Rows[i]["STATUS"];//0：有效，1：无效，2：过期，3：已使用
            string status = getDataTable.Rows[i]["STATUS"].ToString().Trim();//0-已提交；1-已审核；2-已成功；3-已失败
            switch (status)
            {
                case "0":
                    dr["STATUS"] = "有效";
                    break;
                case "1":
                    dr["STATUS"] = "无效";
                    break;
                case "2":
                    dr["STATUS"] = "过期";
                    break;
                case "3":
                    dr["STATUS"] = "已使用";
                    break;
                default:
                    dr["STATUS"] = "没有选择";
                    break;
            }
            dr["TICKETAMT"] = getDataTable.Rows[i]["TICKETAMT"];
            dr["FLAG"] = getDataTable.Rows[i]["FLAG"];
            dr["TICKETRULECODE"] = getDataTable.Rows[i]["TICKETRULECODE"];//0：前台领用;1：后台批量生成;2:手动送券给具体用户
            dr["STARTDATE"] = getDataTable.Rows[i]["STARTDATE"];
            dr["ENDDATE"] = getDataTable.Rows[i]["ENDDATE"];
            dr["TICKETRULEDESC"] = getDataTable.Rows[i]["TICKETRULEDESC"];
            dt.Rows.Add(dr);
        }
        return dt;

    }

    protected void gridViewNotUseTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewNotUseTicket.PageIndex = e.NewPageIndex;
        BindNotUseTicket();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "tab0", "switchTab('TabPage1','Tab2')", true);
    }

    //========已经使用的优惠券，包括过期1：无效，2：过期，3：已使用===================
    private void BindUseTicket()
    {
        DataTable dt = createUseTicketDataTable();
        GridviewControl.GridViewDataBind(this.gridViewUseTicket, dt);
    }

    //使用的优惠券
    private DataSet getUseTicketData()
    {
        //string UserID = hidUserID.Value;
        string UserID = ViewState["phonenumber"].ToString();
        string sql = "select v.PACKAGECODE,v.PACKAGENAME,v.TICKETCODE,v.USERID,v.TICKETUSERCODE,v.STATUS,v.TICKETAMT,v.FLAG,v.TICKETRULECODE,v.STARTDATE,v.ENDDATE,v.TICKETRULEDESC,s.CNFNUM from v_lm_ticket_rule v  left join t_lm_ticket_sub s  ";
        sql += " on v.TICKETUSERCODE = s.TICKETUSERCODE where v.status!=0 and v.USERID='" + UserID + "'";
        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;
    }

    protected void gridViewUseTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewUseTicket.PageIndex = e.NewPageIndex;
        BindUseTicket();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "tab", "switchTab('TabPage1','Tab2');", true);
    }

    public DataTable createUseTicketDataTable()
    {
        DataTable getDataTable = getUseTicketData().Tables[0];
        DataTable dt = new DataTable();
        DataColumn PACKAGECODE_dc = new DataColumn("PACKAGECODE");
        DataColumn PACKAGENAME_dc = new DataColumn("PACKAGENAME");
        DataColumn TICKETCODE_dc = new DataColumn("TICKETCODE");
        DataColumn USERID_dc = new DataColumn("USERID");
        DataColumn TICKETUSERCODE_dc = new DataColumn("TICKETUSERCODE");
        DataColumn STATUS_dc = new DataColumn("STATUS");
        DataColumn TICKETAMT_dc = new DataColumn("TICKETAMT");
        DataColumn FLAG_dc = new DataColumn("FLAG");
        DataColumn TICKETRULECODE_dc = new DataColumn("TICKETRULECODE");
        DataColumn STARTDATE_dc = new DataColumn("STARTDATE");
        DataColumn ENDDATE_dc = new DataColumn("ENDDATE");
        DataColumn TICKETRULEDESC_dc = new DataColumn("TICKETRULEDESC");
        DataColumn CNFNUM_dc = new DataColumn("CNFNUM");


        dt.Columns.Add(PACKAGECODE_dc);
        dt.Columns.Add(PACKAGENAME_dc);
        dt.Columns.Add(TICKETCODE_dc);
        dt.Columns.Add(USERID_dc);
        dt.Columns.Add(TICKETUSERCODE_dc);
        dt.Columns.Add(STATUS_dc);
        dt.Columns.Add(TICKETAMT_dc);
        dt.Columns.Add(FLAG_dc);
        dt.Columns.Add(TICKETRULECODE_dc);
        dt.Columns.Add(STARTDATE_dc);
        dt.Columns.Add(ENDDATE_dc);
        dt.Columns.Add(TICKETRULEDESC_dc);
        dt.Columns.Add(CNFNUM_dc);

        for (int i = 0; i < getDataTable.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();

            dr["PACKAGECODE"] = getDataTable.Rows[i]["PACKAGECODE"];
            //dr["PACKAGENAME"] = getDataTable.Rows[i]["PACKAGENAME"];
            string strTempPackage = getDataTable.Rows[i]["PACKAGENAME"].ToString();
            strTempPackage = getDataTable.Rows[i]["PACKAGECODE"].ToString() + "——" + strTempPackage;
            dr["PACKAGENAME"] = strTempPackage;
            dr["TICKETCODE"] = getDataTable.Rows[i]["TICKETCODE"];

            dr["USERID"] = getDataTable.Rows[i]["USERID"];
            dr["TICKETUSERCODE"] = getDataTable.Rows[i]["TICKETUSERCODE"];
            dr["STATUS"] = getDataTable.Rows[i]["STATUS"];//0：有效，1：无效，2：过期，3：已使用
            string status = getDataTable.Rows[i]["STATUS"].ToString().Trim();//0-已提交；1-已审核；2-已成功；3-已失败
            switch (status)
            {
                case "0":
                    dr["STATUS"] = "有效";
                    break;
                case "1":
                    dr["STATUS"] = "无效";
                    break;
                case "2":
                    dr["STATUS"] = "过期";
                    break;
                case "3":
                    dr["STATUS"] = "已使用";
                    break;
                default:
                    dr["STATUS"] = "没有选择";
                    break;
            }
            dr["TICKETAMT"] = getDataTable.Rows[i]["TICKETAMT"];
            dr["FLAG"] = getDataTable.Rows[i]["FLAG"];
            dr["TICKETRULECODE"] = getDataTable.Rows[i]["TICKETRULECODE"];//0：前台领用;1：后台批量生成;2:手动送券给具体用户
            dr["STARTDATE"] = getDataTable.Rows[i]["STARTDATE"];
            dr["ENDDATE"] = getDataTable.Rows[i]["ENDDATE"];
            dr["TICKETRULEDESC"] = getDataTable.Rows[i]["TICKETRULEDESC"];
            dr["CNFNUM"] = getDataTable.Rows[i]["CNFNUM"];
            dt.Rows.Add(dr);
        }
        return dt;

    }

    #endregion
}