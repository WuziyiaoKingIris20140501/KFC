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

public partial class APPContentDetail : BasePage
{
    APPContentEntity _appcontentEntity = new APPContentEntity();

    CommonEntity _commonEntity = new CommonEntity();
    public string arrData = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string HotelID = Request.QueryString["ID"].ToString().Trim();
                string CITYID = Request.QueryString["CITY"].ToString().Trim();
                string PLATID = Request.QueryString["PLAT"].ToString().Trim();
                string TYPEID = Request.QueryString["TYPE"].ToString().Trim();
                string VERID = Request.QueryString["VER"].ToString().Trim();

                hidHotelID.Value = HotelID;
                hidCITYID.Value = CITYID;
                hidPLATID.Value = PLATID;
                hidTYPEID.Value = TYPEID;
                hidVERID.Value = VERID;
                BindHotelMainListDetail();
            }
            else
            {
                MessageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            }
        }
        //messageContent.InnerHtml = "";
    }

    private void BindHotelMainListDetail()
    {
        MessageContent.InnerHtml = "";
        string messageContent = "";
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity aPPContentDBEntity = new APPContentDBEntity();
        aPPContentDBEntity.HotelID = hidHotelID.Value;
        aPPContentDBEntity.CityID = hidCITYID.Value;
        aPPContentDBEntity.PlatForm = hidPLATID.Value;
        aPPContentDBEntity.TypeID = hidTYPEID.Value;
        aPPContentDBEntity.SerVer = hidVERID.Value;
        _appcontentEntity.APPContentDBEntity.Add(aPPContentDBEntity);

        _appcontentEntity = APPContentBP.HotelDetailListSelect(_appcontentEntity);

        DataSet dsHotelMain = _appcontentEntity.APPContentDBEntity[0].HotelMain;
        ArrayList ayHotelImage = _appcontentEntity.APPContentDBEntity[0].HotelImage;
        DataSet dsHotelRoom = _appcontentEntity.APPContentDBEntity[0].HotelRoom;
        DataSet dsHotelFtType = _appcontentEntity.APPContentDBEntity[0].HotelFtType;

        if (dsHotelMain.Tables.Count > 0 && dsHotelMain.Tables[0].Rows.Count > 0)
        {
            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELID"].ToString().Trim()))
            {
                lbHotelID.Text = dsHotelMain.Tables[0].Rows[0]["HOTELID"].ToString();
            }
            else
            {
                lbHotelID.Text = "";
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelID").ToString() + "<br/>";
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELNM"].ToString().Trim()))
            {
                lbHotelNM.Text = dsHotelMain.Tables[0].Rows[0]["HOTELNM"].ToString();
            }
            else
            {
                lbHotelNM.Text = "";
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelNM").ToString() + "<br/>";
            }

            lbHotelGroup.Text = dsHotelMain.Tables[0].Rows[0]["HOTELGROUP"].ToString();

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["ADDRESS"].ToString().Trim()))
            {
                lbAddress.Text = dsHotelMain.Tables[0].Rows[0]["ADDRESS"].ToString();
            }
            else
            {
                lbAddress.Text = "";
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelAddress").ToString() + "<br/>";
            }

            bool bFlag = true;
            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["LONGITUDE"].ToString().Trim()) && ChkTudeValue(dsHotelMain.Tables[0].Rows[0]["LONGITUDE"].ToString().Trim()))
            {
                lbLongitude.Text = dsHotelMain.Tables[0].Rows[0]["LONGITUDE"].ToString();
            }
            else
            {
                lbLongitude.Text = dsHotelMain.Tables[0].Rows[0]["LONGITUDE"].ToString();
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelTUDE").ToString() + "<br/>";
                bFlag = false;
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["LATITUDE"].ToString().Trim()) && ChkTudeValue(dsHotelMain.Tables[0].Rows[0]["LATITUDE"].ToString().Trim()))
            {
                lbLatitude.Text = dsHotelMain.Tables[0].Rows[0]["LATITUDE"].ToString();
            }
            else
            {
                lbLatitude.Text = dsHotelMain.Tables[0].Rows[0]["LATITUDE"].ToString();
                messageContent = (bFlag) ? messageContent + GetLocalResourceObject("ErrorMsgHotelTUDE").ToString() + "<br/>" : messageContent;
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELDES"].ToString().Trim()))
            {
                lbHotelDes.Text = dsHotelMain.Tables[0].Rows[0]["HOTELDES"].ToString();
            }
            else
            {
                lbHotelDes.Text = "";
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelDesc").ToString() + "<br/>";
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELAPPR"].ToString().Trim()))
            {
                lbHotelAppr.Text = SetlbHotelAppr(dsHotelMain.Tables[0].Rows[0]["HOTELAPPR"].ToString());

                if (!ChkApprValue(dsHotelMain.Tables[0].Rows[0]["HOTELAPPR"].ToString().Trim()))
                {
                    messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelAppr").ToString() + "<br/>";
                }
            }
            else
            {
                lbHotelAppr.Text = "";
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelAppr").ToString() + "<br/>";
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELSERVICE"].ToString().Trim()))
            {
                lbHotelService.Text = dsHotelMain.Tables[0].Rows[0]["HOTELSERVICE"].ToString();
            }
            else
            {
                lbHotelService.Text = "";
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelService").ToString() + "<br/>";
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["BUSSES"].ToString().Trim()))
            {
                lbBusses.Text = dsHotelMain.Tables[0].Rows[0]["BUSSES"].ToString();
            }
            else
            {
                lbBusses.Text = "";
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelBussiness").ToString() + "<br/>";
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["CUSTOMTEL"].ToString().Trim()))
            {
                lbCustomTel.Text = dsHotelMain.Tables[0].Rows[0]["CUSTOMTEL"].ToString();
            }
            else
            {
                lbCustomTel.Text = "";
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelSerTel").ToString() + "<br/>";
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["PRODESC"].ToString().Trim()))
            {
                lbProDes.Text = dsHotelMain.Tables[0].Rows[0]["PRODESC"].ToString();
            }
            else
            {
                lbProDes.Text = "";
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["PROCONTENT"].ToString().Trim()))
            {
                lbProCont.Text = dsHotelMain.Tables[0].Rows[0]["PROCONTENT"].ToString();
            }
            else
            {
                lbProCont.Text = "";
            }

            if (!String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HTPPATH"].ToString().Trim()))
            {
                ProImageReview.InnerHtml = "<img src='" + dsHotelMain.Tables[0].Rows[0]["HTPPATH"].ToString() + "'style='width:310px;height:109px;' />";
            }
            else
            {
                ProImageReview.InnerHtml = "";
            }
        }
        else
        {
            lbHotelID.Text = "";
            lbHotelNM.Text = "";
            lbAddress.Text = "";
            lbLongitude.Text = "";
            lbLatitude.Text = "";
            lbHotelDes.Text = "";
            lbHotelAppr.Text = "";
            lbHotelService.Text = "";
            lbBusses.Text = "";
            lbCustomTel.Text = "";
            messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelError").ToString() + "<br/>";
        }

        if (ayHotelImage.Count > 0)
        {
            PreViewImage(ayHotelImage);

            if (!ChkHotelImagePath(ayHotelImage))
            {
                messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelImage").ToString() + "<br/>";
            }
        }
        else
        {
            ImageReview.InnerHtml = "";
            messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelImage").ToString() + "<br/>";
        }

        gridViewCSAPPContenRoomList.DataSource = dsHotelRoom.Tables[0].DefaultView;
        gridViewCSAPPContenRoomList.DataKeyNames = new string[] { "ROOMID" };//主键
        gridViewCSAPPContenRoomList.DataBind();

        if (dsHotelRoom.Tables.Count > 0 && dsHotelRoom.Tables[0].Rows.Count > 0)
        {
            string strROOMNM = "";
            string strROOMCODE = "";
            string strBEDNM = "";
            string strNETPRICE = "";
            string strVPPRICE = "";
            for (int i = 0; i < dsHotelRoom.Tables[0].Rows.Count; i++)
            {
                if (String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["ROOMNM"].ToString()) && String.IsNullOrEmpty(strROOMNM))
                {
                    strROOMNM = GetLocalResourceObject("ErrorMsgHotelRoomNM").ToString() + "<br/>";
                }

                if (String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["ROOMCODE"].ToString()) && String.IsNullOrEmpty(strROOMCODE))
                {
                    strROOMCODE = GetLocalResourceObject("ErrorMsgHotelRoomCode").ToString() + "<br/>";
                }

                if (String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["BEDNM"].ToString()) && String.IsNullOrEmpty(strBEDNM))
                {
                    strBEDNM = GetLocalResourceObject("ErrorMsgHotelBedNM").ToString() + "<br/>";
                }

                if (String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["NETPRICE"].ToString()) && String.IsNullOrEmpty(strNETPRICE))
                {
                    strNETPRICE = GetLocalResourceObject("ErrorMsgHotelNetPrice").ToString() + "<br/>";
                }

                if (String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["VPPRICE"].ToString()) && String.IsNullOrEmpty(strVPPRICE))
                {
                    strVPPRICE = GetLocalResourceObject("ErrorMsgHotelTNPrice").ToString() + "<br/>";
                }
            }

            messageContent = messageContent + strROOMNM + strROOMCODE + strBEDNM + strNETPRICE + strVPPRICE;
        }
        else
        {
            messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelRoomError").ToString() + "<br/>";
        }

        BindViewCSHotelFtListDetail(lbHotelNM.Text, lbAddress.Text, lbLongitude.Text, lbLatitude.Text);
        MessageContent.InnerHtml = messageContent + "<br/>";
    }

    private bool ChkHotelImagePath(ArrayList alImagePath)
    {
        System.Net.HttpWebResponse result = null;
        System.Net.HttpWebRequest req = null;
        System.Net.ServicePointManager.DefaultConnectionLimit = 200;
        foreach (string strPath in alImagePath)
        {
            try
            {
                System.GC.Collect();
                req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strPath);
                req.ContentType = "application/x-www-form-urlencoded";
                req.Timeout = 5000;
                req.KeepAlive = false;
                result = (System.Net.HttpWebResponse)(req.GetResponse());

                if (result != null)
                {
                    result.Close();
                }
                if (req != null)
                {
                    req.Abort();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        return true;
    }

    private bool ChkTudeValue(string param)
    {
        try
        {
            if (decimal.Parse(param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private bool ChkApprValue(string param)
    {
        if (param.IndexOf('，') >= 0)
        {
            return false;
        }

        if (param.EndsWith(","))
        {
            return false;
        }

        return true;
    }

    private string SetlbHotelAppr(string param)
    {
        string result = "";

        if (String.IsNullOrEmpty(param))
        {
            return result;
        }

        string[] apprList = param.Split(',');

        if (apprList.Length == 0)
        {
            result = param;
        }
        else
        {
            for (int i = 0; i < apprList.Length; i++ )
            {
                result = result + apprList[i].ToString().PadRight(20, ' ') + "&nbsp;&nbsp;&nbsp;&nbsp;" + (((i + 1) % 2 == 0) ? "<br/><br/>" : "");
                //result = result + apprList[i].ToString().PadRight(20, ' ') + "&nbsp;&nbsp;&nbsp;&nbsp;";
            }
        }

        if (String.IsNullOrEmpty(result))
        {
            
        }

        return result;
    }

    private void BindViewCSHotelFtListDetail(string HotelNM , string Address , string Longitude , string Latitude)
    {
        APPContentEntity appcontentEntity = new APPContentEntity();
        appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity aPPContentDBEntity = new APPContentDBEntity();
        aPPContentDBEntity.HotelID = hidHotelID.Value;
        aPPContentDBEntity.FtType = ddpFacilitiesList.SelectedValue;
        aPPContentDBEntity.PlatForm = hidPLATID.Value;
        aPPContentDBEntity.SerVer = hidVERID.Value;
        appcontentEntity.APPContentDBEntity.Add(aPPContentDBEntity);

        DataSet dsDetailResult = APPContentBP.HotelFtDetailListSelect(appcontentEntity).QueryResult;
        if (dsDetailResult.Tables.Count == 0 || dsDetailResult.Tables[0].Rows.Count == 0)
        {
            lbxFacilitiesList.DataSource = null;
            lbxFacilitiesList.DataBind();
            //arrData = "[]";
            //hidArrData.Value = "[]";
            //return;
        }
        lbxFacilitiesList.DataSource = dsDetailResult.Tables[0].DefaultView;
        lbxFacilitiesList.DataTextField = "FTNAME";
        lbxFacilitiesList.DataValueField = "FTNAME";
        lbxFacilitiesList.DataBind();

        getLatLngList(dsDetailResult, HotelNM , Address , Longitude , Latitude);
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "initialize();", true);
    }

    private void getLatLngList(DataSet dsResult, string HotelNM, string Address, string Longitude, string Latitude)
    {
        string latitudetemp = string.Empty;//经度
        string longitudetemp = string.Empty;//维度
        string ftname = string.Empty;//目的地名
        string ftaddress = string.Empty;//目的地地址
        string temp = string.Empty;
        StringBuilder sb = new StringBuilder();
        DataTable dt = dsResult.Tables[0];

        temp = "['" + HotelNM + " （地址：" + Address + "）'," + Latitude + "," + Longitude + "," + 0 + "]";
        sb.Append(temp);
        sb.Append(',');

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            latitudetemp = dt.Rows[i]["LATITUDE"].ToString();//经度
            longitudetemp = dt.Rows[i]["LONGITUDE"].ToString();//维度
            ftname = dt.Rows[i]["FTNAME"].ToString(); //目的地名
            ftaddress = dt.Rows[i]["FTADDRESS"].ToString(); //目的地地址

            if (String.IsNullOrEmpty(ftaddress))
            {
                temp = "['" + ftname + "'," + latitudetemp + "," + longitudetemp + "," + (i+1) + "]";
            }
            else
            {
                temp = "['" + ftname + " （地址：" + ftaddress + "）'," + latitudetemp + "," + longitudetemp + "," + (i + 1) + "]";
            }
            sb.Append(temp);
            sb.Append(',');
        }
        arrData = "[" + sb.ToString().Trim(',') + "]";
        //hidArrData.Value = "[" + sb.ToString().Trim(',') + "]";
    }

    private void PreViewImage(ArrayList ayHotelImage)
    {
        //String preViewImage = "";
        //for (int i = 0; i < ayHotelImage.Count; i++)
        //{
        //    preViewImage += "<img src='" + ayHotelImage[i] + "'/>&nbsp;&nbsp;";
        //}
        //ImageReview.InnerHtml = preViewImage;

        string preViewImage = string.Empty;
        string strWidth = string.Empty;
        string strHeight = string.Empty;
        string strTemp = string.Empty;
        for (int i = 0; i < ayHotelImage.Count; i++)
        {
            try
            {
                strTemp = ayHotelImage[i].ToString().Substring(ayHotelImage[i].ToString().LastIndexOf("/") + 1);
                strWidth = strTemp.Substring(strTemp.IndexOf("_") + 1, strTemp.LastIndexOf("_") - strTemp.IndexOf("_") - 1);
                strHeight = strTemp.Substring(strTemp.LastIndexOf("_") + 1, strTemp.IndexOf(".") - strTemp.LastIndexOf("_") - 1);

                strWidth = (chkWidthHeight(strWidth)) ? (double.Parse(strWidth) * 0.5).ToString() : strWidth;
                strHeight = (chkWidthHeight(strHeight)) ? (double.Parse(strHeight) * 0.5).ToString() : strHeight;
            }
            catch
            {
                strWidth = "200";
                strHeight = "150";
            }
            preViewImage += "&nbsp;&nbsp;<img src='" + ayHotelImage[i] + "' style='width:" + strWidth + "px;height:" + strHeight + "px' />";
        }

        ImageReview.InnerHtml = preViewImage;
    }

    private bool chkWidthHeight(string param)
    {
        try
        {
            decimal.Parse(param);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.CityID = hidCITYID.Value;
        appcontentDBEntity.TypeID = hidTYPEID.Value;
        appcontentDBEntity.PlatForm = hidPLATID.Value;
        appcontentDBEntity.SerVer = hidVERID.Value;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.Select(_appcontentEntity).QueryResult;
        string HotelID = hidHotelID.Value;
        for (int i = 0; i <= dsResult.Tables[0].Rows.Count - 1; i++)
        {
            if (hidHotelID.Value.Equals(dsResult.Tables[0].Rows[i]["HOTELID"].ToString()) && i != 0)
            {
                HotelID = dsResult.Tables[0].Rows[i - 1]["HOTELID"].ToString();
                break;
            }
        }
        
        hidHotelID.Value = HotelID;
        BindHotelMainListDetail();
    }

    protected void bntNext_Click(object sender, EventArgs e)
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.CityID = hidCITYID.Value;
        appcontentDBEntity.TypeID = hidTYPEID.Value;
        appcontentDBEntity.PlatForm = hidPLATID.Value;
        appcontentDBEntity.SerVer = hidVERID.Value;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.Select(_appcontentEntity).QueryResult;
        string HotelID = hidHotelID.Value;
        for (int i = 0; i <= dsResult.Tables[0].Rows.Count - 1; i++)
        {
            if (hidHotelID.Value.Equals(dsResult.Tables[0].Rows[i]["HOTELID"].ToString()) && i != dsResult.Tables[0].Rows.Count - 1)
            {
                HotelID = dsResult.Tables[0].Rows[i + 1]["HOTELID"].ToString();
                break;
            }
        }
        
        hidHotelID.Value = HotelID;
        BindHotelMainListDetail();
    }
    protected void ddpFacilitiesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindViewCSHotelFtListDetail(lbHotelNM.Text, lbAddress.Text, lbLongitude.Text, lbLatitude.Text);
    }
}