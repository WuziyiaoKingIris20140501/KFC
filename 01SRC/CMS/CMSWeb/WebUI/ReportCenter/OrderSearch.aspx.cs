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


public partial class WebUI_ReportCenter_OrderSearch : BasePage
{
    ReportCenterEntity _reportCenterEntity = new ReportCenterEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        _reportCenterEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _reportCenterEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _reportCenterEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _reportCenterEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _reportCenterEntity.StartDTime = this.dpCreateStart.Value;
        _reportCenterEntity.EndDTime = this.dpCreateEnd.Value;
        if (!string.IsNullOrEmpty(this.HidCityID.Value))
        {
            if (!HidCityID.Value.Trim().Contains("[") || !HidCityID.Value.Trim().Contains("]"))
            {
                messageContent.InnerHtml = "查询失败，选择城市不合法，请修改！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }
        }
        _reportCenterEntity.CityID = this.HidCityID.Value == "" ? "" : this.HidCityID.Value.Substring((this.HidCityID.Value.IndexOf('[') + 1), (this.HidCityID.Value.IndexOf(']') - 1));
        if (!string.IsNullOrEmpty(this.HidSales.Value))
        {
            if (!HidSales.Value.Trim().Contains("[") || !HidSales.Value.Trim().Contains("]"))
            {
                messageContent.InnerHtml = "查询失败，选择城市不合法，请修改！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }
        }
        _reportCenterEntity.Sales = this.HidSales.Value == "" ? "" : this.HidSales.Value.Substring((this.HidSales.Value.IndexOf('[') + 1), (this.HidSales.Value.IndexOf(']') - 1));
        DataSet dsResult = ReportCenterBP.OrderSearchDataReport(_reportCenterEntity).QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "hotel_id" };//主键
        gridViewCSReviewList.DataBind();

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }


    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        _reportCenterEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _reportCenterEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _reportCenterEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _reportCenterEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _reportCenterEntity.StartDTime = this.dpCreateStart.Value;
        _reportCenterEntity.EndDTime = this.dpCreateEnd.Value;
        if (!string.IsNullOrEmpty(this.HidCityID.Value))
        {
            if (!HidCityID.Value.Trim().Contains("[") || !HidCityID.Value.Trim().Contains("]"))
            {
                messageContent.InnerHtml = "查询失败，选择城市不合法，请修改！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }
        }
        _reportCenterEntity.CityID = this.HidCityID.Value == "" ? "" : this.HidCityID.Value.Substring((this.HidCityID.Value.IndexOf('[') + 1), (this.HidCityID.Value.IndexOf(']') - 1));
        if (!string.IsNullOrEmpty(this.HidSales.Value))
        {
            if (!HidSales.Value.Trim().Contains("[") || !HidSales.Value.Trim().Contains("]"))
            {
                messageContent.InnerHtml = "查询失败，选择城市不合法，请修改！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }
        }
        _reportCenterEntity.Sales = this.HidSales.Value == "" ? "" : this.HidSales.Value.Substring((this.HidSales.Value.IndexOf('[') + 1), (this.HidSales.Value.IndexOf(']') - 1));
        DataSet dsResult = ReportCenterBP.OrderSearchDataReport(_reportCenterEntity).QueryResult;

        if (dsResult.Tables.Count == 0 && dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }
        CommonFunction.ExportExcelForLM(dsResult);
    }
}