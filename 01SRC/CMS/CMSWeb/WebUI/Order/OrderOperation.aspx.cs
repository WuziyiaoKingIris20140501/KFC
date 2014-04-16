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
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Net;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Process.SendFaxByWebTurnPicture;
using HotelVp.CMS.Domain.Entity;
using HotelVp.Common.Utilities;

public partial class OrderOperation : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string EventLMID = string.Empty;
            //if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            //{
            //    EventLMID = Request.QueryString["ID"].ToString().Trim();

            //}
            //else if (!String.IsNullOrEmpty(Request.QueryString["FOGID"]))
            //{
            //    EventLMID = GetEventLMOrderID(Request.QueryString["FOGID"].ToString().Trim());

            //    if (String.IsNullOrEmpty(EventLMID.Trim()))
            //    {
            //        detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //        return;
            //    }
            //}
            //else
            //{
            //    detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            //}

            //hidEventLMID.Value = EventLMID;
            //BindViewCSSystemLogMain();
            //BindViewCSSystemLogDetail();

            tbDetail.Style.Add("display", "none");
            tbControl.Style.Add("display", "none");
            trCanlReson.Style.Add("display", "none");
            detailMessageContent.InnerHtml = "";
        }
        //messageContent.InnerHtml = "";
    }

    private string GetEventLMOrderID(string orderID)
    {
        if (String.IsNullOrEmpty(orderID.Trim()))
        {
            return "";
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
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
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsMainResult = LmSystemLogBP.OrderOperationSelect(_lmSystemLogEntity).QueryResult;

        if (dsMainResult.Tables.Count == 0 || dsMainResult.Tables[0].Rows.Count == 0)
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        lbCREATE_TIME.Text = dsMainResult.Tables[0].Rows[0]["create_time"].ToString();
        lbORDER_CHANNEL.Text = dsMainResult.Tables[0].Rows[0]["ORDER_CHANNEL"].ToString();
        lbPRICE_CODE.Text = dsMainResult.Tables[0].Rows[0]["price_code_nm"].ToString();
        lbBOOK_STATUS.Text = "LMBAR".Equals(dsMainResult.Tables[0].Rows[0]["price_code"].ToString().Trim().ToUpper()) ? dsMainResult.Tables[0].Rows[0]["book_status_nm"].ToString() : dsMainResult.Tables[0].Rows[0]["book_status_other_nm"].ToString();
        lbIS_GUA.Text = dsMainResult.Tables[0].Rows[0]["is_gua_nm"].ToString();
        lbRESV_GUA_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_HOLD_TIME"].ToString();
        lbUSER_HOLD_TIME.Text = dsMainResult.Tables[0].Rows[0]["USER_HOLD_TIME"].ToString();
        lbRESV_GUA_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_GUA_DESC"].ToString();
        lbRESV_CXL_NM.Text = dsMainResult.Tables[0].Rows[0]["RESV_CXL_DESC"].ToString();
        lbPAY_STATUS.Text = dsMainResult.Tables[0].Rows[0]["pay_status_nm"].ToString();
        lbHOTEL_NAME.Text = dsMainResult.Tables[0].Rows[0]["hotel_name"].ToString();
        lbLINKTEL.Text = dsMainResult.Tables[0].Rows[0]["linktel"].ToString();
        lbGUEST_NAMES.Text = dsMainResult.Tables[0].Rows[0]["guest_names"].ToString();
        lbCONTACT_NAME.Text = dsMainResult.Tables[0].Rows[0]["contact_name"].ToString();
        lbCONTACT_TEL.Text = dsMainResult.Tables[0].Rows[0]["contact_tel"].ToString();
        lbLOGIN_MOBILE.Text = dsMainResult.Tables[0].Rows[0]["LOGIN_MOBILE"].ToString();
        lbOrderDays.Text = SetOrderDaysVal(dsMainResult.Tables[0].Rows[0]["in_date"].ToString(), dsMainResult.Tables[0].Rows[0]["out_date"].ToString());
        lbIN_DATE.Text = dsMainResult.Tables[0].Rows[0]["in_date_nm"].ToString();
        lbOUT_DATE.Text = dsMainResult.Tables[0].Rows[0]["out_date_nm"].ToString();
        lbROOM_TYPE_NAME.Text = dsMainResult.Tables[0].Rows[0]["room_type_name"].ToString();
        lbBOOK_ROOM_NUM.Text = dsMainResult.Tables[0].Rows[0]["book_room_num"].ToString();
        lbARRIVE_TIME.Text = dsMainResult.Tables[0].Rows[0]["ARRIVE_TIME"].ToString();
        lbTICKET_USERCODE.Text = dsMainResult.Tables[0].Rows[0]["ticket_usercode"].ToString();
        lbTICKET_PAGENM.Text = dsMainResult.Tables[0].Rows[0]["packagename"].ToString();
        lbTICKET_AMOUNT.Text = dsMainResult.Tables[0].Rows[0]["ticket_amount"].ToString();
        lbBOOK_REMARK.Text = dsMainResult.Tables[0].Rows[0]["BOOK_REMARK"].ToString();
        lbORDER_NUM.Text = dsMainResult.Tables[0].Rows[0]["id"].ToString();

        BindViewCSSystemLogDetail();
        SetOrderSettingControlVal(dsMainResult.Tables[0].Rows[0]["price_code"].ToString());

        tbDetail.Style.Add("display", "");
        tbControl.Style.Add("display", "");
    }

    private string SetOrderDaysVal(string strInDate, string strOutDate)
    {
        if (String.IsNullOrEmpty(strInDate.Trim()) || String.IsNullOrEmpty(strOutDate.Trim()))
        {
            return "1";
        }

        try
        {
            DateTime dtInDate = Convert.ToDateTime(strInDate);
            DateTime dtOutDate = Convert.ToDateTime(strOutDate);
            TimeSpan tsDays = dtOutDate - dtInDate;
            return tsDays.Days.ToString();
        }
        catch
        {
            return "1";
        }
    }

    private void SetOrderSettingControlVal(string strPriceCode)
    {
        DataTable dtOrderStatus = GetOrderStatusData(strPriceCode.Trim());

        ddpOrderStatus.DataTextField = "ORDERDIS";
        ddpOrderStatus.DataValueField = "ORDERSTATUS";
        ddpOrderStatus.DataSource = dtOrderStatus;
        ddpOrderStatus.DataBind();


        DataTable dtCanelReson = GetCanelResonData();
        ddpCanelReson.DataTextField = "CanelResonDis";
        ddpCanelReson.DataValueField = "CanelReson";
        ddpCanelReson.DataSource = dtCanelReson;
        ddpCanelReson.DataBind();


    }

    private DataTable GetOrderStatusData(string strType)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ORDERSTATUS");
        dt.Columns.Add("ORDERDIS");

        if ("LMBAR".Equals(strType.Trim().ToUpper()))
        {
            DataRow dr = dt.NewRow();
            dr["ORDERSTATUS"] = "4";
            dr["ORDERDIS"] = "取消单";
            dt.Rows.Add(dr);
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = dt.NewRow();

                switch (i.ToString())
                {
                    //case "0":
                    //    dr["ORDERSTATUS"] = "3";
                    //    dr["ORDERDIS"] = "用户取消";
                    //    break;
                    case "0":
                        dr["ORDERSTATUS"] = "4";
                        dr["ORDERDIS"] = "可入住";
                        break;
                    case "1":
                        dr["ORDERSTATUS"] = "5";
                        dr["ORDERDIS"] = "NoShow";
                        break;
                    case "2":
                        dr["ORDERSTATUS"] = "7";
                        dr["ORDERDIS"] = "入住中";
                        break;
                    case "3":
                        dr["ORDERSTATUS"] = "8";
                        dr["ORDERDIS"] = "已离店";
                        break;
                    case "4":
                        dr["ORDERSTATUS"] = "9";
                        dr["ORDERDIS"] = "CC取消";
                        break;
                    default:
                        dr["ORDERSTATUS"] = "";
                        dr["ORDERDIS"] = "";
                        break;
                }
                dt.Rows.Add(dr);
            }
        }


        return dt;
    }

    private DataTable GetCanelResonData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CanelReson");
        dt.Columns.Add("CanelResonDis");

        for (int i = 0; i < 4; i++)
        {
            DataRow dr = dt.NewRow();

            switch (i.ToString())
            {
                case "0":
                    dr["CanelReson"] = "CRC01";
                    dr["CanelResonDis"] = "满房";
                    break;
                case "1":
                    dr["CanelReson"] = "CRC06";
                    dr["CanelResonDis"] = "变价";
                    break;
                case "2":
                    dr["CanelReson"] = "CRG01";
                    dr["CanelResonDis"] = "行程改变";
                    break;
                case "3":
                    dr["CanelReson"] = "CRG99";
                    dr["CanelResonDis"] = "其他";
                    break;
                default:
                    dr["CanelReson"] = "";
                    dr["CanelResonDis"] = "";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }


    private void BindViewCSSystemLogDetail()
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;

        DataSet dsDetailResult = LmSystemLogBP.OrderOperationDetailSelect(_lmSystemLogEntity).QueryResult;

        //if (dsDetailResult.Tables.Count > 0 && dsDetailResult.Tables[0].Rows.Count > 1)
        //{
        //    for (int i = 1; i <= dsDetailResult.Tables[0].Rows.Count -1; i++)
        //    {
        //        if (dsDetailResult.Tables[0].Rows[i - 1]["EVENTTIME"] != null && dsDetailResult.Tables[0].Rows[i]["EVENTTIME"] != null)
        //        {
        //            dsDetailResult.Tables[0].Rows[i - 1]["LAG"] = SetTimeLag(dsDetailResult.Tables[0].Rows[i - 1]["EVENTTIME"].ToString(), dsDetailResult.Tables[0].Rows[i]["EVENTTIME"].ToString());
        //        }
        //    }
        //}

        gridViewCSSystemLogDetail.DataSource = dsDetailResult.Tables[0].DefaultView;
        gridViewCSSystemLogDetail.DataKeyNames = new string[] { "INDATE" };//主键
        gridViewCSSystemLogDetail.DataBind();
    }

    private string SetTimeLag(string strFrom, string strTo)
    {
        string strResult = "";

        if (!CheckDateTimeValue(strFrom, strTo))
        {
            return strResult;
        }

        DateTime dtFrom = DateTime.Parse(strFrom);
        DateTime dtTo = DateTime.Parse(strTo);

        System.TimeSpan ND = dtTo - dtFrom;

        strResult = strResult + ND.Days.ToString() + "天";
        strResult = strResult + ND.Hours.ToString() + "时";
        strResult = strResult + ND.Minutes.ToString() + "分";
        strResult = strResult + ND.Seconds.ToString() + "秒";
        return strResult;
    }

    private bool CheckDateTimeValue(string strFrom, string strTo)
    {
        try
        {
            DateTime.Parse(strFrom);
            DateTime.Parse(strTo);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void gridViewCSSystemLogDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ////    //this.gridViewRegion.PageIndex = e.NewPageIndex;
        ////    //BindGridView();

        //    //执行循环，保证每条数据都可以更新
        //    for (int i = 1; i <= gridViewCSSystemLogDetail.Rows.Count; i++)
        //    {
        //        //首先判断是否是数据行
        //        e.Row.
        //    }
    }


    protected void gridViewCSSystemLogDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSSystemLogDetail.PageIndex = e.NewPageIndex;
        BindViewCSSystemLogDetail();
    }

    private string GetCancleReason(string reasonCode)
    {
        string reasonName = string.Empty;
        Hashtable htReason = new Hashtable();
        htReason.Add("CRG18", "LM订单客人手机端取消");
        htReason.Add("CRG17", "预授权失败自动取消");
        htReason.Add("CRC01", "满房");
        htReason.Add("CRC03", "员工差错");
        htReason.Add("CRC04", "蓄水单取消");
        htReason.Add("CRC06", "变价");
        htReason.Add("CRG14", "无法完成担保");
        htReason.Add("CRG06", "无法完成支付");
        htReason.Add("CRG11", "超时未支付");
        htReason.Add("CRG05", "测试订单");
        htReason.Add("CRG01", "行程改变");
        htReason.Add("CRG02", "无法满足特殊需求");
        htReason.Add("CRG03", "其他途径预订");
        htReason.Add("CRG04", "预订内容变更");
        htReason.Add("CRG07", "确认速度不满意");
        htReason.Add("CRG08", "GDS渠道取消");
        htReason.Add("CRG09", "IDS渠道取消");
        htReason.Add("CRG10", "接口渠道取消");
        htReason.Add("CRG13", "设施/位置不满意");
        htReason.Add("CRC02", "重复预订");
        htReason.Add("CRG15", "预订未用抵用券");
        htReason.Add("CRG16", "预订未登录");
        htReason.Add("CRC07", "Jrez渠道取消");
        htReason.Add("CRC05", "骚扰订单");
        htReason.Add("CRG99", "其他");
        htReason.Add("CRH01", "酒店反悔");
        htReason.Add("CRH03", "酒店停业/装修");
        htReason.Add("PGSRQ", "系统自动取消");
        htReason.Add("CRH02", "酒店不确认");
        htReason.Add("CRH05", "无法追加担保");
        htReason.Add("CRH06", "无法追加预付");
        htReason.Add("CRH07", "无协议/协议到期");
        htReason.Add("CRH08", "不可抗力");

        reasonName = htReason.ContainsKey(reasonCode) ? htReason[reasonCode].ToString() : reasonCode;
        return reasonName;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(txtOrderID.Text.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            return;
        }
        trCanlReson.Style.Add("display", "none");
        hidOrderID.Value = txtOrderID.Text.Trim();
        BindViewCSSystemLogMain();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }


    protected void btnSet_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(hidOrderID.Value.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorMessage").ToString();
            return;
        }

        if (!String.IsNullOrEmpty(txtBOOK_REMARK.Text.Trim()) && (StringUtility.Text_Length(txtBOOK_REMARK.Text.ToString().Trim()) > 250))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("ErrorRemark").ToString();
            return;
        }

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _lmSystemLogEntity.FogOrderID = hidOrderID.Value;
        _lmSystemLogEntity.OrderBookStatus = ddpOrderStatus.SelectedValue.Trim();
        _lmSystemLogEntity.CanelReson = ddpCanelReson.SelectedItem.Text.Trim();
        _lmSystemLogEntity.BookRemark = txtBOOK_REMARK.Text.Trim();

        int iResult = LmSystemLogBP.SaveOrderOperation(_lmSystemLogEntity);

        _commonEntity.LogMessages = _lmSystemLogEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "订单操作-保存";
        commonDBEntity.Event_ID = hidHotelID.Value;
        string conTent = GetLocalResourceObject("EventSaveMessage").ToString();

        conTent = string.Format(conTent, _lmSystemLogEntity.FogOrderID, _lmSystemLogEntity.OrderBookStatus, _lmSystemLogEntity.BookRemark, _lmSystemLogEntity.CanelReson);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
    protected void ddpOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ("9".Equals(ddpOrderStatus.SelectedValue))
        {
            trCanlReson.Style.Add("display", "");
        }
        else
        {
            trCanlReson.Style.Add("display", "none");
        }
    }

    //发送传真
    protected void btnSendFax_Click(object sender, EventArgs e)
    {
        string path = GetImageByWeb();

        if (!File.Exists(path + ".jpg"))
        {
            path = GetImageByWeb();
        }
        //detailMessageContent.InnerHtml = "Message:" + path;
        FaxService fax = new FaxService();
        fax.Timeout = 1200000;
        string xml = ""; //ToServiceXML.getSendFaxToServerXMLStr(path + ".jpg"); //拼装xml数据
        string sendFaxToServerBack = fax.SendFaxToServer(xml); //开始远程调用
        string sendFaxToServerBack1 = sendFaxToServerBack.Replace(">", ">\r\n");
        //tb_XML.Text = sendFaxToServerBack1;

        ///////////////////////////////////解析反馈结果///////////////////////////////////////
        XmlDocument m_XmlDoc = new XmlDocument();
        try
        {
            m_XmlDoc.LoadXml(sendFaxToServerBack);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(m_XmlDoc.NameTable);
            XmlNodeList nodeList = m_XmlDoc.ChildNodes;
            XmlNode node = nodeList.Item(1);
            string ErrorFlag = node.FirstChild.SelectSingleNode("Header").SelectSingleNode("ErrorFlag").InnerText;
            string ReturnMessage = node.FirstChild.SelectSingleNode("Header").SelectSingleNode("ReturnMessage").InnerText;
            string JobNo = "";
            string JobResult = "";
            string TotalNum = "";
            string ValidNum = "";
            try
            {
                JobNo = node.FirstChild.SelectSingleNode("SendFaxToServerResponse").SelectSingleNode("SendFaxToServerResult").SelectSingleNode("JobNo").InnerText;
                JobResult = node.FirstChild.SelectSingleNode("SendFaxToServerResponse").SelectSingleNode("SendFaxToServerResult").SelectSingleNode("JobResult").InnerText;
                TotalNum = node.FirstChild.SelectSingleNode("SendFaxToServerResponse").SelectSingleNode("SendFaxToServerResult").SelectSingleNode("TotalNum").InnerText;
                ValidNum = node.FirstChild.SelectSingleNode("SendFaxToServerResponse").SelectSingleNode("SendFaxToServerResult").SelectSingleNode("ValidNum").InnerText;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
            }
            string queryResultS = "";
            queryResultS = queryResultS + "ErrorFlag :" + ErrorFlag + "\r\n" + "ReturnMessage:" + ReturnMessage + "\r\n" + "JobNo :" + JobNo + "\r\n" +
                "JobResult :" + JobResult + "\r\n" + "TotalNum :" + TotalNum + "\r\n" + "ValidNum :" + ValidNum + "\r\n";

            //tb_SendFaxToServer.Text = queryResultS;

            //File.Delete(path);
            detailMessageContent.InnerHtml = "Message:" + ReturnMessage;
        }
        catch (Exception ex)
        {
            ex.GetBaseException();
        }
    }
     
    public string GetImageByWeb()
    {
        //获取当前页面的OrderNo
        string OrderNo = hidOrderID.Value;

        //Order/SendFaxByContent  存放图片
        string fileDir = Server.MapPath("SendFaxByContent");
        if (!Directory.Exists(fileDir))
        {
            Directory.CreateDirectory(fileDir);
        }
        string configPath = ConfigurationManager.AppSettings["GetImageByWeb"];
        string FileName = ConfigurationManager.AppSettings["GetFileName"];
        //configPath = "http://localhost:34067/CMSWeb/WebUI/Order/OrderOperationGetImageByWebPrint.aspx?id=" + OrderNo;
        //configPath = "http://172.16.10.16:8081/WebUI/Order/OrderOperationPrint.aspx?ID="+ OrderNo;
        configPath = configPath + "?id=" + OrderNo;

        var filePath = fileDir + "\\" + OrderNo;
        if (File.Exists(filePath + ".jpg"))
        {
            File.Delete(filePath + ".jpg");
        }
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.EnableRaisingEvents = false;
        process.StartInfo.RedirectStandardOutput = false;
        process.StartInfo.UseShellExecute = false;
        //process.StartInfo.FileName = Environment.CurrentDirectory + "\\webshotcmd.exe";
        process.StartInfo.FileName = FileName;
        process.StartInfo.Arguments = string.Format("/url \"{0}\" /width 760  /height 900 /bwidth 760 /bheight 900 /timeout 90 /waitdoc 2 /waitdocfl 1 /redirectmax 2 -noactivex -ignoreerr /out \"{1}\" ", configPath, filePath + ".jpg");
        process.Start();
        process.WaitForExit(100000);

        if (!process.HasExited)
            process.Kill();

        process.Dispose();

        return filePath;

    }
}