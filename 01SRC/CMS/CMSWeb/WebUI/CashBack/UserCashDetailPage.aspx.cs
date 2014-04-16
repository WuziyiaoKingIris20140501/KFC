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

public partial class UserCashDetailPage : BasePage
{
    UserEntity _userEntity = new UserEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string UserID = Request.QueryString["ID"].ToString().Trim();
                hidUserID.Value = UserID;
                ////BindChannelDDL();
                BindUserMainListDetail();
                BindViewCSUserListDetail();
            }
            else
            {
                messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            }
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

        DataSet dsMainResult = UserSearchBP.UserCashMainListSelect(_userEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        lbUserID.Text = dsMainResult.Tables[0].Rows[0]["ID"].ToString();
        lbLoginMobile.Text = dsMainResult.Tables[0].Rows[0]["LOGINMOBILE"].ToString();
        lbAllCount.Text = dsMainResult.Tables[0].Rows[0]["ALLCOUNT"].ToString();
        lbCompleCount.Text = dsMainResult.Tables[0].Rows[0]["COMPLECOUNT"].ToString();

        lbRegchanel.Text = dsMainResult.Tables[0].Rows[0]["REGCHANELNM"].ToString();
        lbPlatform.Text = dsMainResult.Tables[0].Rows[0]["PLATFORMNM"].ToString();

        lbSignIn.Text = dsMainResult.Tables[0].Rows[0]["CREATETIME"].ToString();
        lbUseCash.Text = dsMainResult.Tables[0].Rows[0]["USECASH"].ToString();
        lbCashApl.Text = dsMainResult.Tables[0].Rows[0]["CASHAPL"].ToString();
        lbCashVer.Text = dsMainResult.Tables[0].Rows[0]["CASHVER"].ToString();
    }

    private void BindViewCSUserListDetail()
    {
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity userGroupDBEntity = new UserDBEntity();

        userGroupDBEntity.UserID = lbLoginMobile.Text.Trim();

        _userEntity.UserDBEntity.Add(userGroupDBEntity);

        DataSet dsDetailResult = UserSearchBP.UserCashDetailListSelect(_userEntity).QueryResult;

        gridViewCSUserListDetail.DataSource = dsDetailResult.Tables[0].DefaultView;
        gridViewCSUserListDetail.DataKeyNames = new string[] { "PKEY", "SELTYPE" };//主键
        gridViewCSUserListDetail.DataBind();
    }

    protected void gridViewCSUserListDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSUserListDetail.Rows.Count; i++)
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

    protected void gridViewCSUserListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSUserListDetail.PageIndex = e.NewPageIndex;
        BindViewCSUserListDetail();
    }

    protected void gridViewCSUserListDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hidPkey.Value = gridViewCSUserListDetail.DataKeys[e.RowIndex][0].ToString();
        hidSelectType.Value = gridViewCSUserListDetail.DataKeys[e.RowIndex][1].ToString();
        BindmyHistoryGridDetail();
    }

    private void BindmyHistoryGridDetail()
    {
        _userEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _userEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _userEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _userEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _userEntity.UserDBEntity = new List<UserDBEntity>();
        UserDBEntity userGroupDBEntity = new UserDBEntity();

        userGroupDBEntity.UserID = lbLoginMobile.Text.Trim();
        userGroupDBEntity.Pkey = hidPkey.Value.Trim();
        userGroupDBEntity.SelectType = hidSelectType.Value.Trim();
        _userEntity.UserDBEntity.Add(userGroupDBEntity);

        DataSet dsDetailResult = UserSearchBP.UserCashPopListSelect(_userEntity).QueryResult;

        myGridView.DataSource = dsDetailResult.Tables["MASTER"].DefaultView;
        myGridView.DataKeyNames = new string[] { "PKEY" };//主键
        myGridView.DataBind();

        myHistoryGridView.DataSource = dsDetailResult.Tables["DETAIL"].DefaultView;
        myHistoryGridView.DataKeyNames = new string[] { "CHDTIME" };//主键
        myHistoryGridView.DataBind();

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "AddNewlist();", true);
    }

    protected void myHistoryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= myHistoryGridView.Rows.Count; i++)
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

    protected void myHistoryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.myHistoryGridView.PageIndex = e.NewPageIndex;
        BindmyHistoryGridDetail();
    }
}