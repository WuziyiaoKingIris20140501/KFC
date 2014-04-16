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

public partial class QunaerDataWeekly : BasePage
{
    ReportCenterEntity _reportCenterEntity = new ReportCenterEntity();
    CommonEntity _commonEntity = new CommonEntity();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["StartDTime"] = "";
            ViewState["EndDTime"] = "";
            BindViewCSReviewGridList();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
    }

    private string GetDateTimeQuery(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return "";
        }

        try
        {
            DateTime dtTime = DateTime.Parse(param);
            return dtTime.AddDays(1).ToShortDateString() + " 04:00:00";
        }
        catch
        {
            return "";
        }
    }

    private void BindViewCSReviewGridList()
    {
        //messageContent.InnerHtml = "";
        _reportCenterEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _reportCenterEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _reportCenterEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _reportCenterEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _reportCenterEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        _reportCenterEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        DataSet dsResult = ReportCenterBP.QunaerDataReportSelect(_reportCenterEntity).QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "Week" };//主键
        gridViewCSReviewList.DataBind();

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["StartDTime"] = dpCreateStart.Value;
        ViewState["EndDTime"] = dpCreateEnd.Value;
        BindViewCSReviewGridList();
    }

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ReportCenterEntity _reportCenterEntity = new ReportCenterEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _reportCenterEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _reportCenterEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _reportCenterEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _reportCenterEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _reportCenterEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        _reportCenterEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
        DataSet dsResult = ReportCenterBP.QunaerDataReportSelect(_reportCenterEntity).QueryResult;
       
        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count ==0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }
        CommonFunction.ExportExcelForLM(dsResult);
    }
}