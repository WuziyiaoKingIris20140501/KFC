using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Services;

public partial class RouteStockII : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindShipsList();
        }
    }
    private void bindShipsList()
    {
        DataSet dsResult = CruiseInfoBP.GetCruiseShipPlanInfo();
        rdlShips.DataSource = dsResult;
        rdlShips.DataTextField = "TEXT";
        rdlShips.DataValueField = "VALUE";
        rdlShips.DataBind();
        rdlShips.SelectedIndex = 0;

        hidShipNM.Value = rdlShips.SelectedItem.Text;


        string shipID = rdlShips.SelectedValue;
        DataSet dsBoat = CruiseInfoBP.GetCruiseBoadPlanInfo(shipID);
        rdlBoats.DataSource = dsBoat;
        rdlBoats.DataTextField = "TEXT";
        rdlBoats.DataValueField = "VALUE";
        rdlBoats.DataBind();
        rdlBoats.SelectedIndex = 0;

        hidBoatNM.Value = rdlBoats.SelectedItem.Text;
    }
    #region  查询操作
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        lbCruiseNM.Text = hidCruiseID.Value.ToString();
       
    }

    [WebMethod]
    public static string GetDataByJson(string CruiseID, string SDtime, string EDtime)
    {
        StringBuilder sbResult = new StringBuilder();

        DataSet dsResult = CruiseInfoBP.GetCruiseDataPlanInfo(CruiseID, SDtime, EDtime);

        sbResult.Append("<table class=\"Tb_BodyCSS2\" style=\"border:1px #d5d5d5 solid; padding:1px;width:98%\">");
        sbResult.Append("<tr>");

        DateTime dtS = DateTime.Parse(SDtime);
        DateTime dtE = DateTime.Parse(EDtime);

        for (int i = 0; i < 14; i++)
        {
            if (dtS.AddDays(i).DayOfWeek == DayOfWeek.Friday)
            {
                sbResult.Append("<td>");
                sbResult.Append("星期五"); 
            }
            else if (dtS.AddDays(i).DayOfWeek == DayOfWeek.Monday)
            {
                sbResult.Append("<td>");
                sbResult.Append("星期一"); 
            }
            else if (dtS.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
            {
                sbResult.Append("<td style=\"background-color: #CDEBFF;\">");
                sbResult.Append("星期六"); 
            }
            else if (dtS.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
            {
                sbResult.Append("<td style=\"background-color: #CDEBFF;\">");
                sbResult.Append("星期日"); 
            }
            else if (dtS.AddDays(i).DayOfWeek == DayOfWeek.Thursday)
            {
                sbResult.Append("<td>");
                sbResult.Append("星期四"); 
            }
            else if (dtS.AddDays(i).DayOfWeek == DayOfWeek.Tuesday)
            {
                sbResult.Append("<td>");
                sbResult.Append("星期二"); 
            }
            else if (dtS.AddDays(i).DayOfWeek == DayOfWeek.Wednesday)
            {
                sbResult.Append("<td>");
                sbResult.Append("星期三");
            }

            sbResult.Append("</td>");
        }
        sbResult.Append("</tr><tr>");



        string strCruiseID = CruiseID.Substring((CruiseID.IndexOf('[') + 1), (CruiseID.IndexOf(']') - 1));
        // planid, routeid, plannum, plandt
        int iNum = 1;

        string planid = "";
        string plannum = "";

        while (dtS <= dtE)
        {
            if (dsResult.Tables[0].Select("PlanDTime='" + dtS.ToString("yyyyMMdd") + "'").Length > 0)
            {
                planid = dsResult.Tables[0].Select("PlanDTime='" + dtS.ToString("yyyyMMdd") + "'")[0]["PlanID"].ToString();
                plannum = dsResult.Tables[0].Select("PlanDTime='" + dtS.ToString("yyyyMMdd") + "'")[0]["PlanNumer"].ToString();
            }
            else
            {
                planid = "";
                plannum = "";
            }

            if (!String.IsNullOrEmpty(plannum) && int.Parse(plannum) == 0)
            {
                sbResult.Append("<td style=\"background-color: #E6B9B6;cursor:pointer\" onclick=\"OpenClick('" + planid + "','" + strCruiseID + "','" + plannum + "','" + dtS.ToShortDateString() + "')\">");
            }
            else if (dtS.DayOfWeek == DayOfWeek.Sunday || dtS.DayOfWeek == DayOfWeek.Saturday)
            {
                sbResult.Append("<td style=\"background-color: #CDEBFF;cursor:pointer\" onclick=\"OpenClick('" + planid + "','" + strCruiseID + "','" + plannum + "','" + dtS.ToShortDateString() + "')\">");
            }
            else
            {
                sbResult.Append("<td style=\"cursor:pointer\" onclick=\"OpenClick('" + planid + "','" + strCruiseID + "','" + plannum + "','" + dtS.ToShortDateString() + "')\">");
            }

            if (!String.IsNullOrEmpty(planid))
            {
                sbResult.Append(dtS.Month.ToString() + dtS.Day.ToString().PadLeft(2,'0'));
            }

            if (!String.IsNullOrEmpty(plannum))
            {
                sbResult.Append("<br/>" + plannum.ToString() + "间");
            }

            sbResult.Append("</td>");

            dtS = dtS.AddDays(1);

            if (iNum > 0 && iNum % 14 == 0)
            {
                sbResult.Append("</tr><tr>");
            }
            iNum = iNum + 1;
        }

        if ((iNum - 1) % 14 == 0)
        {
            string temp = sbResult.ToString().Substring(0, sbResult.ToString().Length - 4);
            sbResult.Clear();
            sbResult.Append(temp);
        }
        else
        {
            while ((iNum - 1) % 14 != 0)
            {
                sbResult.Append("<td>");
                sbResult.Append("</td>");
                iNum = iNum + 1;
            }
        }
        sbResult.Append("</tr>");
        sbResult.Append("</table>");

        return sbResult.ToString();
    }

    [WebMethod]
    public static string SetMemoVal(string strKey)
    {
        StringBuilder sbMemo = new StringBuilder();
        DataSet dsResult = CruiseInfoBP.ReviewRoutePlanHistory(strKey);
        //strMeno = String.Format("<table style='width:97%;background-color:#DCDCDC' cellpadding='3'><tr><td align='left'><b><font size='3px'>{0}<font /></b></td><td></td><td></td><td align='right' style='margin-right:10px;'>{1} 操作于：{2}</td></tr><tr><td align='left' style='width:100px'>入住房号：</td><td align='left' style='width:100px'>{3}</td><td align='right' style='width:100px'>确认号：</td><td align='left' style='width:300px'>{4}</td></tr><tr><td align='left'>备注信息：</td><td colspan='3' align='left'>{5}</td></tr></table><br />", drRow["OD_STATUS"].ToString(), drRow["EVENTUSER"].ToString(), drRow["EVENTTIME"].ToString(), drRow["ROOM_ID"].ToString(), drRow["APPROVE_ID"].ToString(), drRow["REMARK"].ToString());
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            sbMemo.Append("<table class=\"GView_BodyCSS\" style=\"border:1px #d5d5d5 solid; padding:1px;width:98%\">");
            sbMemo.Append("<tr><td align=\"center\" style=\"background-color:#DDDDDD\">操作人</td><td align=\"center\" style=\"background-color:#DDDDDD\">操作日期</td><td align=\"center\" style=\"background-color:#DDDDDD\">操作库存</td></tr>");
            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                sbMemo.Append("<tr><td>" + drRow["USERNAME"].ToString() + "</td><td>" + drRow["CREATEDATE"].ToString() + "</td><td>" + drRow["EVENT_CONTENT"].ToString() + "</td></tr>");
            }
            sbMemo.Append("</table>");
        }

        return sbMemo.ToString();
    }

    [WebMethod]
    public static string SaveCruisePlan(string PlanID, string PlanNumer, string OPlanNumer)
    {
        CruiseInfoEntity _cruiseinfoEntity = new CruiseInfoEntity();
        CommonEntity _commonEntity = new CommonEntity();
        _cruiseinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cruiseinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cruiseinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cruiseinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _cruiseinfoEntity.CruiseInfoDBEntity = new List<CruiseInfoDBEntity>();
        CruiseInfoDBEntity cruiseInfoDBEntity = new CruiseInfoDBEntity();

        cruiseInfoDBEntity.PlanID = PlanID;
        cruiseInfoDBEntity.PlanNumer = PlanNumer;
        cruiseInfoDBEntity.OPlanNumer = OPlanNumer;

        _cruiseinfoEntity.CruiseInfoDBEntity.Add(cruiseInfoDBEntity);
        _cruiseinfoEntity = CruiseInfoBP.SaveCruisePlanInfo(_cruiseinfoEntity);
        int iResult = _cruiseinfoEntity.Result;
        _commonEntity.LogMessages = _cruiseinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "游轮库存-保存";
        commonDBEntity.Event_ID = cruiseInfoDBEntity.PlanID;
        string conTent = "库存保存 - 原库存：{0}  修改后库存:{1}";

        conTent = string.Format(conTent, OPlanNumer, PlanNumer);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = "库存保存成功！"; ;
            //MessageContent.InnerHtml = string.Format(GetLocalResourceObject("SaveSuccess").ToString(), HotelID);
        }
        else
        {
            //commonDBEntity.Event_Result = GetLocalResourceObject("SaveError").ToString();
            //MessageContent.InnerHtml = GetLocalResourceObject("SaveError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        return "库存保存成功！";
    }
    
    #endregion
    protected void rdlShips_SelectedIndexChanged(object sender, EventArgs e)
    {
        string shipID = rdlShips.SelectedValue;
        DataSet dsBoat = CruiseInfoBP.GetCruiseBoadPlanInfo(shipID);
        rdlBoats.DataSource = dsBoat;
        rdlBoats.DataTextField = "TEXT";
        rdlBoats.DataValueField = "VALUE";
        rdlBoats.DataBind();
        rdlBoats.SelectedIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        hidShipNM.Value = rdlShips.SelectedItem.Text;
        hidBoatNM.Value = rdlBoats.SelectedItem.Text;
        hidBoatID.Value = rdlBoats.SelectedValue;

        DataSet dsResult =  CruiseInfoBP.GetCruiseRoomPlanInfo(rdlBoats.SelectedValue);
        DataSet dsBoat = CruiseInfoBP.GetBoatRoomInfo(rdlBoats.SelectedValue);
        string strBoatNM = string.Empty;
        foreach (DataRow dr in dsBoat.Tables[0].Rows)
        {
            strBoatNM = strBoatNM + dr["ShipRoomNM"].ToString() + ",";
        }
        strBoatNM = strBoatNM.TrimEnd(',');
        hidBoatNMList.Value = strBoatNM;

        StringBuilder sbMemo = new StringBuilder();
        sbMemo.Append("<table class=\"GView_BodyCSS\" style=\"border:1px #d5d5d5 solid; padding:1px;width:98%\">");

        sbMemo.Append("<tr class=\"GView_HeaderCSS\">");
        for (int i = 1; i < dsResult.Tables[0].Columns.Count; i++)
        {
            sbMemo.Append("<th align=\"center\">" + dsResult.Tables[0].Columns[i].ColumnName + "</th>");
        }
        sbMemo.Append("</tr>");
        string strplanid = string.Empty;
        string strroomnm = string.Empty;
        string strroomnum = string.Empty;
        for (int j = 0; j < dsResult.Tables[0].Rows.Count; j++)
        {

            sbMemo.Append("<tr class=\"GView_ItemCSS\">");
            sbMemo.Append("<td align=\"center\">" + dsResult.Tables[0].Rows[j][1].ToString() + "</td>");
            for (int h = 2; h < dsResult.Tables[0].Columns.Count; h++)
            {
                strplanid = dsResult.Tables[0].Rows[j][0].ToString() + "-" + (h - 2).ToString();
                strroomnm = dsResult.Tables[0].Columns[h].ColumnName;
                strroomnum = dsResult.Tables[0].Rows[j][h].ToString();
                sbMemo.Append("<td align=\"center\" style=\"cursor:pointer\" onclick=\"OpenClick('" + strplanid + "','" + strroomnm + "','" + strroomnum + "')\">" + dsResult.Tables[0].Rows[j][h].ToString() + "</td>");
            }
            sbMemo.Append("</tr>");
        }
        sbMemo.Append("</table>");
        hidContent.Value = sbMemo.ToString();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BindTable();", true);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }



    [WebMethod]
    public static string CrCruisePlan(string Action, string BoatID, string CreateStart, string PlanNumer)
    {
        CruiseInfoEntity _cruiseinfoEntity = new CruiseInfoEntity();
        CommonEntity _commonEntity = new CommonEntity();
        _cruiseinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cruiseinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cruiseinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cruiseinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _cruiseinfoEntity.CruiseInfoDBEntity = new List<CruiseInfoDBEntity>();
        CruiseInfoDBEntity cruiseInfoDBEntity = new CruiseInfoDBEntity();


        cruiseInfoDBEntity.Action = Action;
        cruiseInfoDBEntity.BoatID = BoatID;
        cruiseInfoDBEntity.CreateStart = CreateStart;
        cruiseInfoDBEntity.PlanNumer = PlanNumer;

        _cruiseinfoEntity.CruiseInfoDBEntity.Add(cruiseInfoDBEntity);
        _cruiseinfoEntity = CruiseInfoBP.SaveCruisePlanList(_cruiseinfoEntity);
        int iResult = _cruiseinfoEntity.Result;
        //_commonEntity.LogMessages = _cruiseinfoEntity.LogMessages;
        //_commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        //CommonDBEntity commonDBEntity = new CommonDBEntity();

        //commonDBEntity.Event_Type = "游轮库存-保存";
        //commonDBEntity.Event_ID = cruiseInfoDBEntity.PlanID;
        //string conTent = "库存保存 - 原库存：{0}  修改后库存:{1}";

        //conTent = string.Format(conTent, PlanNumer);
        //commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            //commonDBEntity.Event_Result = "库存保存成功！"; ;
            //MessageContent.InnerHtml = string.Format(GetLocalResourceObject("SaveSuccess").ToString(), HotelID);
        }
        else
        {
            //commonDBEntity.Event_Result = GetLocalResourceObject("SaveError").ToString();
            //MessageContent.InnerHtml = GetLocalResourceObject("SaveError").ToString();
        }
        //_commonEntity.CommonDBEntity.Add(commonDBEntity);
        //CommonBP.InsertEventHistory(_commonEntity);
        return "库存保存成功！";
    }
}