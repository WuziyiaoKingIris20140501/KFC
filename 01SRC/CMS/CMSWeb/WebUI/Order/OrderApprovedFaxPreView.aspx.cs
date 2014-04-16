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
using HotelVp.Common.Utilities;

public partial class OrderApprovePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string hotelID = Request.QueryString["HID"] == null ? "" :  Server.UrlDecode(Request.QueryString["HID"].ToString());
            string outSDate = Request.QueryString["OTSD"] == null ? "" : Server.UrlDecode(Request.QueryString["OTSD"].ToString());
            string outEDate = Request.QueryString["OTED"] == null ? "" : Server.UrlDecode(Request.QueryString["OTED"].ToString());
            string faxStstus = Request.QueryString["FST"] == null ? "" : Server.UrlDecode(Request.QueryString["FST"].ToString());
            string ordID = Request.QueryString["OID"] == null ? "" : Server.UrlDecode(Request.QueryString["OID"].ToString());
            string faxNum = Request.QueryString["FNU"] == null ? "" : Server.UrlDecode(Request.QueryString["FNU"].ToString());
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetImagePreview('" + hotelID + "','" + outSDate + "','" + outEDate + "','" + faxStstus + "','" + ordID + "','" + faxNum + "');", true);
        }
    }

    [WebMethod]
    public static string SetImagePreviewList(string Hid, string Otsd, string Oted, string Fst, string Oid, string Fnu)
    {
        LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.HotelID = Hid;
        _lmSystemLogEntity.OutStart = Otsd;
        _lmSystemLogEntity.OutEnd = Oted;
        _lmSystemLogEntity.FaxStatus = Fst;

        _lmSystemLogEntity.FogOrderID = Oid;
        _lmSystemLogEntity.FaxNum = Fnu;
        string json = "";
        DataSet dsResult = LmSystemLogBP.HotelFaxHis(_lmSystemLogEntity).QueryResult;
        try
        {
            if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
            {
                json = ToJson(dsResult.Tables[0]);
            }
            else
            {
                json = "[{\"FAXBDT\":\"\",\"FAXNID\":\"\",\"FAXUNID\":\"\",\"FAXID\":\"\",\"FAXDT\":\"\",\"FAXTST\":\"\",\"FAXBST\":\"\",\"FAXTURL\":\"\",\"FAXBURL\":\"\"}]";
            }
        }
        catch (Exception ex)
        {
            json = "[{\"FAXBDT\":\"\",\"FAXNID\":\"\",\"FAXUNID\":\"\",\"FAXID\":\"\",\"FAXDT\":\"\",\"FAXTST\":\"\",\"FAXBST\":\"\",\"FAXTURL\":\"\",\"FAXBURL\":\"\"}]";
        }

        return json;
    }

    public static string ToJson(DataTable dt)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("[");
        DataRowCollection drc = dt.Rows;
        for (int i = 0; i < drc.Count; i++)
        {
            jsonString.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string strKey = dt.Columns[j].ColumnName;
                string strValue = drc[i][j].ToString().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\"", "&quot;");
                Type type = dt.Columns[j].DataType;
                jsonString.Append("\"" + strKey + "\":");
                strValue = StringFormat(strValue, type);
                if (j < dt.Columns.Count - 1)
                {
                    jsonString.Append(strValue + ",");
                }
                else
                {
                    jsonString.Append(strValue);
                }
            }
            jsonString.Append("},");
        }
        jsonString.Remove(jsonString.Length - 1, 1);
        jsonString.Append("]");
        return jsonString.ToString();
    }

    private static string StringFormat(string str, Type type)
    {
        if (type == typeof(string))
        {
            str = String2Json(str);
            str = "\"" + str + "\"";
        }
        else if (type == typeof(DateTime))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(bool))
        {
            str = str.ToLower();
        }
        else if (type != typeof(string) && string.IsNullOrEmpty(str))
        {
            str = "\"" + str + "\"";
        }
        return str;
    }

    private static string String2Json(String s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s.ToCharArray()[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\""); break;
                case '\\':
                    sb.Append("\\\\"); break;
                case '/':
                    sb.Append("\\/"); break;
                case '\b':
                    sb.Append("\\b"); break;
                case '\f':
                    sb.Append("\\f"); break;
                case '\n':
                    sb.Append("\\n"); break;
                case '\r':
                    sb.Append("\\r"); break;
                case '\t':
                    sb.Append("\\t"); break;
                default:
                    sb.Append(c); break;
            }
        }
        return sb.ToString();
    }
}