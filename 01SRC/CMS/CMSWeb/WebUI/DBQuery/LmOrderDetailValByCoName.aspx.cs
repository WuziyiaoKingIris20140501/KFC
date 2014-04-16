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

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class LmOrderDetailValByCoName : Page
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
        {
            string EventLMID = GetEventLMOrderID(Request.QueryString["id"].ToString().Trim());
            string Colname = String.IsNullOrEmpty(Request.QueryString["fild"]) ? "" : Request.QueryString["fild"].ToString().Trim();

            if (String.IsNullOrEmpty(EventLMID) || String.IsNullOrEmpty(Colname))
            {
                Response.Clear();
                Response.Charset = "utf-8";
                Response.Buffer = true;
                this.EnableViewState = false;
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.ContentType = "text/plain";
                Response.Write("查无此值");
                Response.Flush();
                //Response.Close();
                Response.End();
                return;
            }

            hidEventLMID.Value = EventLMID;
            hidColumnID.Value = Colname;
            BindViewCSSystemLogMain();
        }
        else
        {
            Response.Clear();
            Response.Charset = "utf-8";
            Response.Buffer = true;
            this.EnableViewState = false;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/plain";
            Response.Write("查无此值");
            Response.Flush();
            //Response.Close();
            Response.End();
            return;
        }
    }

    private string GetEventLMOrderID(string orderID)
    {
        if (String.IsNullOrEmpty(orderID.Trim()))
        {
             return "";
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = "GetOrderField";// UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = "GetOrderField";//UserSession.Current.UserDspName;
        _lmSystemLogEntity.EventID = orderID;

        DataSet dsMainResult = LmSystemLogBP.GetEventLMOrderID(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            return "";
        }

        return dsMainResult.Tables[0].Rows[0][0].ToString();
    }

    private void BindViewCSSystemLogMain()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = "GetOrderField";// UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = "GetOrderField";//UserSession.Current.UserDspName;
        _lmSystemLogEntity.EventID = hidEventLMID.Value;

        DataSet dsMainResult = LmSystemLogBP.UserMainListSelect(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            Response.Clear();
            Response.Charset = "utf-8";
            Response.Buffer = true;
            this.EnableViewState = false;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/plain";
            Response.Write("查无此值");
            Response.Flush();
            //Response.Close();
            Response.End();
            return;
        }

        string strVal = "";
        bool bVal = false;
        for (int i=0; i < dsMainResult.Tables[0].Columns.Count -1; i ++)
        {
            if (hidColumnID.Value.ToString().ToLower().Equals(dsMainResult.Tables[0].Columns[i].ColumnName.ToString().ToLower()))
            {
                bVal = true;
                strVal = dsMainResult.Tables[0].Rows[0][i].ToString();
            }
        }

        Response.Clear();
        Response.Charset = "utf-8";
        Response.Buffer = true;
        this.EnableViewState = false;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "text/plain";
        Response.Write((bVal) ? strVal : "查无此值");
        Response.Flush();
        Response.End();
    }
}