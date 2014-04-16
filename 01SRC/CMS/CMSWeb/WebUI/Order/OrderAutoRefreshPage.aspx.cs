using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.Configuration;

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

public partial class OrderAutoRefreshPage : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidRefreshWav.Value = (ConfigurationManager.AppSettings["RefreshWav"] != null) ? ConfigurationManager.AppSettings["RefreshWav"].ToString() : "";
            _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            DataSet dsResult = LmSystemLogBP.LmOrderAutoRefreshSelect(_lmSystemLogEntity).QueryResult;

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                hidMaxOrderNo.Value = dsResult.Tables[0].Rows[0]["FOGORDERID"].ToString();
            }

            gridViewCSReviewLmSystemLogList.DataSource = dsResult.Tables[0].DefaultView;
            gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "FOGORDERID" };//主键
            gridViewCSReviewLmSystemLogList.DataBind();

            hidRemainSecond.Value = (ConfigurationManager.AppSettings["RefreshRemainSecond"] != null) ? ConfigurationManager.AppSettings["RefreshRemainSecond"].ToString() : "60";
            this.Page.RegisterStartupScript("remaintimebtn0", "<script>setInterval('RemainTimeBtn()',1000);</script>");
        }
    }

    protected void btnRefush_Click(object sender, EventArgs e)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsResult = LmSystemLogBP.LmOrderAutoRefreshSelect(_lmSystemLogEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !hidMaxOrderNo.Value.Equals(dsResult.Tables[0].Rows[0]["FOGORDERID"].ToString().Trim()))
        {
            hidMaxOrderNo.Value = dsResult.Tables[0].Rows[0]["FOGORDERID"].ToString();
            //bgSound.Attributes["src"] = (ConfigurationManager.AppSettings["RefreshWav"] != null) ? ConfigurationManager.AppSettings["RefreshWav"].ToString() : "http://www.hotelvp.com/Images/Sent.wav";
            hidPlay.Value = "1";
        }
        else
        {
            hidPlay.Value = "0";
            //bgSound.Attributes["src"] = "";
        }
        gridViewCSReviewLmSystemLogList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "FOGORDERID" };//主键
        gridViewCSReviewLmSystemLogList.DataBind();

        hidRemainSecond.Value = (ConfigurationManager.AppSettings["RefreshRemainSecond"] != null) ? ConfigurationManager.AppSettings["RefreshRemainSecond"].ToString() : "60";
        this.Page.RegisterStartupScript("remaintimebtn0", "<script>setInterval('RemainTimeBtn()',1000);</script>");
    }
}