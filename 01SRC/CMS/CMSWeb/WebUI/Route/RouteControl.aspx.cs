using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.Web.Services;
using System.Text.RegularExpressions;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class RouteControl : BasePage
{
    CruiseInfoEntity _cruiseinfoEntity = new CruiseInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        //wctHotel.CityName = hidCityName.Value;

        if (!IsPostBack)
        {
        }
    }

    #region 编辑酒店信息
    
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(hidCruiseID.Value))
        {
            string strCruise = hidCruiseID.Value;
            MessageContent.InnerHtml = "";
            if (!strCruise.Contains("[") || !strCruise.Contains("]") || String.IsNullOrEmpty(strCruise))
            {
                MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
                return;
            }

            strCruise = strCruise.Substring((strCruise.IndexOf('[') + 1), (strCruise.IndexOf(']') - 1));
            if (String.IsNullOrEmpty(strCruise))
            {
                MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
                return;
            }


            DataSet dsResult = CruiseInfoBP.GetCruiseDataInfo(strCruise);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                ReSetControlVal(dsResult);
            }
            else
            {
                MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
            }
        }
        else
        {
            MessageContent.InnerHtml = GetLocalResourceObject("SelectError").ToString();
        }
    }

    private void ReSetControlVal(DataSet dsResult)
    {
        lbCruiseNM.Text = hidCruiseID.Value.ToString();
        txtShipNM.Text = dsResult.Tables[0].Rows[0]["ShipNM"].ToString().Trim();
        txtDestination.Text = dsResult.Tables[0].Rows[0]["Destination"].ToString().Trim();
        txtDays.Text = dsResult.Tables[0].Rows[0]["Days"].ToString().Trim();
        txtPort.Text = dsResult.Tables[0].Rows[0]["Port"].ToString().Trim();
    }

    protected void btnCreateHL_Click(object sender, EventArgs e)
    {
        CreateCr();
    }

    #endregion


    #region 新建航线信息

    public void  CreateCr()
    {
        MessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtShipNM.Text.Trim()) || String.IsNullOrEmpty(txtDestination.Text.Trim()) || String.IsNullOrEmpty(txtDays.Text.Trim()) || String.IsNullOrEmpty(txtPort.Text.Trim()))
        {
            MessageContent.InnerHtml = "航线基础信息保存 *号位必填字段，请修改！";
            return;
        }


        _cruiseinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cruiseinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cruiseinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cruiseinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _cruiseinfoEntity.CruiseInfoDBEntity = new List<CruiseInfoDBEntity>();
        CruiseInfoDBEntity cruiseInfoDBEntity = new CruiseInfoDBEntity();

        cruiseInfoDBEntity.CruiseID = hidCruiseID.Value.ToString();
        cruiseInfoDBEntity.ShipNM = txtShipNM.Text.Trim();
        cruiseInfoDBEntity.Destination = txtDestination.Text.Trim();
        cruiseInfoDBEntity.Days = txtDays.Text.Trim();
        cruiseInfoDBEntity.Port = txtPort.Text.Trim();

        _cruiseinfoEntity.CruiseInfoDBEntity.Add(cruiseInfoDBEntity);
        _cruiseinfoEntity = CruiseInfoBP.SaveCruiseInfo(_cruiseinfoEntity);
        int iResult = _cruiseinfoEntity.Result;
        string HotelID = _cruiseinfoEntity.ErrorMSG;
        _commonEntity.LogMessages = _cruiseinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "航线基础信息-保存";
        commonDBEntity.Event_ID = HotelID;
        string conTent = GetLocalResourceObject("EventSaveMessage").ToString();

        conTent = string.Format(conTent, HotelID, cruiseInfoDBEntity.ShipNM, cruiseInfoDBEntity.Destination, cruiseInfoDBEntity.Days, cruiseInfoDBEntity.Port);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = string.Format(GetLocalResourceObject("SaveSuccess").ToString(), HotelID);
            MessageContent.InnerHtml = string.Format(GetLocalResourceObject("SaveSuccess").ToString(), HotelID);
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("SaveError").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("SaveError").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    #endregion

}
