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
            BandData(hotelID);
            Response.Write(" <script> window.print(); </script> ");
        }
    }

    private void BandData(string hotelID)
    {
        lbFaxDate.Text = DateTime.Now.ToString();
        lbFrom.Text = UserSession.Current.UserDspName;
        OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.HotelID = hotelID;
        orderinfoEntity.OrderList = (Session["OrderFaxList"] != null) ? (ArrayList)(Session["OrderFaxList"]) : new ArrayList();
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataSet dsResult = OrderInfoBP.BindOrderApproveFaxPrint(_orderInfoEntity).QueryResult;

        //gridViewCSList.DataSource = dsResult.Tables["PRINT"].DefaultView;
        //gridViewCSList.DataKeyNames = new string[] { "ORDROOM" };//主键
        //gridViewCSList.DataBind();

        SerOrderList(dsResult);

        if (dsResult.Tables["HLEX"].Rows.Count > 0)
        {
            lbHotelID.Text = String.Format("酒店名称：{0}（ID：{1}）", dsResult.Tables["HLEX"].Rows[0]["prop_name_zh"].ToString(), dsResult.Tables["HLEX"].Rows[0]["hotel_id"].ToString());

            string strTo = dsResult.Tables["HLEX"].Rows[0]["linkman"].ToString();
            string strTelTo = dsResult.Tables["HLEX"].Rows[0]["linktel"].ToString();
            string strFaxTo = dsResult.Tables["HLEX"].Rows[0]["linkfax"].ToString();
            int iHour = DateTime.Now.Hour;

            lbTo.Text = (strTo.Split('|').Length >= iHour) ? strTo.Split('|')[iHour].ToString() : "";
            lbTelTo.Text = (strTelTo.Split('|').Length >= iHour) ? strTelTo.Split('|')[iHour].ToString() : "";
            lbFaxTo.Text = (strFaxTo.Split('|').Length >= iHour) ? strFaxTo.Split('|')[iHour].ToString() : "";
        }
        else
        {
            lbHotelID.Text = "";
            lbTo.Text = "";
            lbTelTo.Text = "";
            lbFaxTo.Text = "";
            //lbTelFrom.Text = "";
            //lbFaxFrom.Text = "";
        }

        //if (dsResult.Tables["UsersInfo"].Rows.Count > 0)
        //{
        //    lbTelFrom.Text = dsResult.Tables["UsersInfo"].Rows[0]["User_Tel"].ToString();
        //}
        //else
        //{
        //    lbTelFrom.Text = "";
        //}
    }

    private void SerOrderList(DataSet dsResult)
    {
        StringBuilder sbOrder = new StringBuilder();
        sbOrder.Append("<table style=\"width: 100%; border: 1px black solid; padding: 1px; border-collapse: collapse;border-spacing: 0;\"><tr><td style=\"width:22%;height:30px;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">订单号</td><td rowspan=\"2\" style=\"width:14%;height:30px;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">客户姓名</td><td rowspan=\"2\" style=\"width:9%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">房价</td><td rowspan=\"2\" style=\"width:9%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">间数</td><td rowspan=\"2\" style=\"width:19%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">入住-离店</td><td rowspan=\"2\" style=\"width:10%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">房号</td><td rowspan=\"2\" style=\"width:17%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">确认号</td></tr><tr><td  align=\"center\" style=\"width:14%;height:30px;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\">房型</td></tr>");

        foreach (DataRow drRow in dsResult.Tables[0].Rows)
        {
            sbOrder.Append("<tr><td style=\"width:22%;height:30px;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;word-break :break-all ; word-wrap:break-word;\" align=\"center\">" + drRow["ORDERID"].ToString().Trim() + drRow["ROOMNM"].ToString().Trim().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("“", "&quot;").Replace("“", "&quot;").Replace("‘", "'").Replace(" ", "&nbsp;") + "</td><td style=\"width:14%;height:30px;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;;word-break :break-all ; word-wrap:break-word;\" align=\"center\">" + drRow["GUESTNM"].ToString().Trim().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("“", "&quot;").Replace("“", "&quot;").Replace("‘", "'").Replace(" ", "&nbsp;") + "</td><td style=\"width:9%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">" + drRow["ROOMPR"].ToString().Trim() + "</td><td style=\"width:9%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">" + drRow["ROOMNUM"].ToString().Trim() + "</td><td style=\"width:19%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">" + drRow["INOUTDT"].ToString().Trim() + "</td><td style=\"width:10%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">" + drRow["ROOMID"].ToString().Trim() + "</td><td style=\"width:17%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;\" align=\"center\">" + drRow["APPRNUM"].ToString().Trim() + "</td></tr>");
        }

        sbOrder.Append("</table>");
        dvOrderList.InnerHtml = sbOrder.ToString();
    }

    protected void gridViewCSList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("style", "word-break :break-all ; word-wrap:break-word");
            e.Row.Cells[1].Attributes.Add("style", "word-break :break-all ; word-wrap:break-word");
        }
    }
}