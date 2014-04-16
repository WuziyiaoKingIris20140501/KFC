using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Data;
using System.Text;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class WebUI_Hotel_HotelBusinessCircle : System.Web.UI.Page
{
    public string cityName;
    ELRelationEntity _elRelationEntity = new ELRelationEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["city"] != null)
            {
                string strCity = Request.QueryString["city"].ToString();
                wcthvpTagInfo.CityName = strCity;
                string hotelId = Request.QueryString["hotelId"].ToString();
                bingData(hotelId, wcthvpTagInfo.CityName);
            }
            else
            {
                string query = Request.UrlReferrer.Query;
                string[] strCity = query.Split('&');
                if (strCity.Length > 1)
                {
                    wcthvpTagInfo.CityName = strCity[1].ToString().Substring(5, strCity[1].ToString().Length - 5).ToLower();
                    string hotelId = strCity[2].ToString().Substring(8, strCity[2].ToString().Length - 8);
                    bingData(hotelId, wcthvpTagInfo.CityName);
                }
            }
        }
    }

    public void bingData(string hotelId, string cityName)
    {
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
        ELRelationDBEntity ELRelationDBEntity = new ELRelationDBEntity();

        ELRelationDBEntity.HVPID = hotelId.Trim();
        ELRelationDBEntity.AmountFrom = cityName.Trim();

        _elRelationEntity.ELRelationDBEntity.Add(ELRelationDBEntity);
        DataSet ds = ELRelationBP.HVPAreaSelect(_elRelationEntity).QueryResult;
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<input type='button' id='" + ds.Tables[0].Rows[i]["AREAID"].ToString() + "' style='margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat' onclick='removeClick(this)'  value='[" + ds.Tables[0].Rows[i]["AREAID"].ToString() + "]" + ds.Tables[0].Rows[i]["TagName"].ToString() + "     '>");
            }
            dvUserGroupList.InnerHtml = sb.ToString();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        _elRelationEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _elRelationEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _elRelationEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _elRelationEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        string query = Request.UrlReferrer.Query;
        string[] strCity = query.Split('&');
        string hotelId = strCity[2].ToString().Substring(8, strCity[2].ToString().Length - 8);
        int iResult = 0;

        string[] strId = hidUserGroupList.Value.Trim().Split(',');
        if (hidUserGroupList.Value.Trim().TrimEnd(',').Split(',').Length > 1)
        {
            dvMessageContent.InnerHtml = "酒店商圈关联只能选择一个商圈，请修改！";
            return;
        }

        if (!string.IsNullOrEmpty(hotelId))
        {
            _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
            ELRelationDBEntity ELRelationDBEntity = new ELRelationDBEntity();

            ELRelationDBEntity.HVPID = hotelId.Trim();

            _elRelationEntity.ELRelationDBEntity.Add(ELRelationDBEntity);
            iResult = ELRelationBP.HVPAreaDelete(_elRelationEntity).Result;
        }
        
        _commonEntity.LogMessages = _elRelationEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();
        try
        {
            for (int i = 0; i < strId.Length; i++)
            {
                if (!string.IsNullOrEmpty(strId[i].ToString().Trim()))
                {
                    _elRelationEntity.ELRelationDBEntity = new List<ELRelationDBEntity>();
                    ELRelationDBEntity ELRelationDBEntity = new ELRelationDBEntity();

                    ELRelationDBEntity.HVPID = hotelId.Trim();
                    ELRelationDBEntity.ELongID = strId[i].Trim().Substring(1, strId[i].Trim().IndexOf("]") - 1);
                    _elRelationEntity.ELRelationDBEntity.Add(ELRelationDBEntity);
                    iResult = ELRelationBP.HVPAreaInsert(_elRelationEntity).Result;

                    commonDBEntity.Event_Type = "酒店商圈关联-保存";
                    commonDBEntity.Event_ID = hotelId.Trim();
                    commonDBEntity.Event_Content = "酒店商圈关联操作，HotelID:" + hotelId.Trim() + ",AreaID:" + strId[i].ToString();
                    commonDBEntity.Event_Result = iResult.ToString() == "1" ? "Success" : "Fail";
                    _commonEntity.CommonDBEntity.Add(commonDBEntity);
                     CommonBP.InsertEventHistory(_commonEntity);
                    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
                }
            }
        }
        catch (Exception ex)
        {
            commonDBEntity.Event_Type = "酒店商圈关联-保存1";
            commonDBEntity.Event_ID = hotelId.Trim();
            commonDBEntity.Event_Content = "酒店商圈关联操作，HotelID:" + hotelId + ",AreaID:" + strId;
            commonDBEntity.Event_Result = iResult.ToString() == "1" ? "Success" : "Fail";
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
        }


        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "PageClosed();", true);
    }
}