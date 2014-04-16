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

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class FacilitiesHoteSetting : BasePage
{
    HotelFacilitiesEntity _hotelfacilitiesEntity = new HotelFacilitiesEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAllFTList();
            //BindServiceList();
            //BindFacilitiesList();
        }
    }

    private void BindAllFTList()
    {
        MessageContent.InnerHtml = "";
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);

        DataSet dsResult = HotelFacilitiesBP.ServiceAllFTSelect(_hotelfacilitiesEntity).QueryResult;

        string strTypeSeq = "";
        DataSet dsDDP = GetDataItem();
        Panel pl = new Panel();
        CheckBoxList chk1 = new CheckBoxList();

        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
        {
            if (!strTypeSeq.Equals(dsResult.Tables[0].Rows[i]["TYPESEQ"].ToString()))
            {
                if (!String.IsNullOrEmpty(strTypeSeq))
                {
                    chk1.DataTextField = "FACILITIESNM";
                    chk1.DataValueField = "FACILITIESCODE";
                    chk1.DataSource = dsDDP;
                    chk1.DataBind();
                    pl.Controls.Add(chk1);
                    dvChkList.Controls.Add(pl);
                }
                
                pl = new Panel();
                pl.GroupingText = dsResult.Tables[0].Rows[i]["TYPENAME"].ToString();
                pl.CssClass = "";
                pl.Style.Add("width", "100%");

                chk1 = new CheckBoxList();
                chk1.RepeatDirection = RepeatDirection.Vertical;
                chk1.RepeatColumns = 8;
                chk1.RepeatLayout = RepeatLayout.Table;
                chk1.CellSpacing = 10;

                dsDDP.Tables[0].Rows.Clear();
                strTypeSeq = dsResult.Tables[0].Rows[i]["TYPESEQ"].ToString();
            }

            DataRow dr = dsDDP.Tables[0].NewRow();
            dr["FACILITIESNM"] = dsResult.Tables[0].Rows[i]["FACILITIESNM"].ToString();
            dr["FACILITIESCODE"] = dsResult.Tables[0].Rows[i]["FACILITIESCODE"].ToString();
            dsDDP.Tables[0].Rows.Add(dr);
        }

        chk1.DataTextField = "FACILITIESNM";
        chk1.DataValueField = "FACILITIESCODE";
        chk1.DataSource = dsDDP;
        chk1.DataBind();
        pl.Controls.Add(chk1);
        dvChkList.Controls.Add(pl);
    }

    private DataSet GetDataItem()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add(new DataTable());
        ds.Tables[0].Columns.Add("FACILITIESNM");
        ds.Tables[0].Columns.Add("FACILITIESCODE");
        return ds;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string strHotel = WebAutoComplete.AutoResult.ToString();
        if (String.IsNullOrEmpty(strHotel))
        {
            return;
        }

        lbHotelNM.Text = strHotel.Substring(strHotel.IndexOf(']') + 1);
        hidHotelID.Value = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
        BindAllFTList();
        BindHotelList();
        //UpdatePanel1.Update();
    }

    public void BindHotelList()
    {
        MessageContent.InnerHtml = "";
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);
        hotelFacilitiesDBEntity.HotelID = hidHotelID.Value;
        DataSet dsResult = HotelFacilitiesBP.BindHotelList(_hotelfacilitiesEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            return;
        }

        ArrayList chkList = new ArrayList();

        foreach (DataRow drRow in dsResult.Tables[0].Rows)
        {
            chkList.Add(drRow["FACILITIESCODE"].ToString());
        }


        for (int i = 0; i < dvChkList.Controls.Count; i++)
        {
            if (dvChkList.Controls[i].Controls.Count > 0)
            {
                CheckBoxList chbList = (CheckBoxList)dvChkList.Controls[i].Controls[0];
                foreach (ListItem li in chbList.Items)
                {
                    if (chkList.Contains(li.Value))
                    {
                        li.Selected = true;
                    }
                }
            }
        }
        UpdatePanel3.Update();
         //foreach (ListItem li in chkServiceList.Items)
         //{
         //    if (chkList.Contains(li.Value))
         //    {
         //       li.Selected = true;
         //    }
         //}

         //foreach (ListItem li in chkFacilitiesList.Items)
         //{
         //    if (chkList.Contains(li.Value))
         //    {
         //        li.Selected = true;
         //    }
         //}

        //UpdatePanel1.Update();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnAddData();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        //BindServiceList();
        //BindFacilitiesList();
        BindAllFTList();
        BindHotelList();
    }

    public void BindServiceList()
    {
        MessageContent.InnerHtml = "";
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);

        DataSet dsResult = HotelFacilitiesBP.ServiceTypeSelect(_hotelfacilitiesEntity).QueryResult;

        //chkServiceList.DataTextField = "FACILITIESNM";
        //chkServiceList.DataValueField = "FACILITIESCODE";
        //chkServiceList.DataSource = dsResult;
        //chkServiceList.DataBind();
    }

    public void BindFacilitiesList()
    {
        MessageContent.InnerHtml = "";
        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);

        DataSet dsResult = HotelFacilitiesBP.FacilitiesTypeSelect(_hotelfacilitiesEntity).QueryResult;
        //chkFacilitiesList.DataTextField = "FACILITIESNM";
        //chkFacilitiesList.DataValueField = "FACILITIESCODE";
        //chkFacilitiesList.DataSource = dsResult;
        //chkFacilitiesList.DataBind();
    }

    public void btnAddData()
    {
        MessageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(hidHotelID.Value))
        {
            MessageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            UpdatePanel4.Update();
            return;
        }

        _hotelfacilitiesEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelfacilitiesEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelfacilitiesEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelfacilitiesEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity = new List<HotelFacilitiesDBEntity>();
        HotelFacilitiesDBEntity hotelFacilitiesDBEntity = new HotelFacilitiesDBEntity();
       
        ArrayList chkList = new ArrayList();

        string[] FTlist = hidFTList.Value.Split(',');

        foreach (string li in FTlist)
        {
            if (!String.IsNullOrEmpty(li))
            {
                chkList.Add(li + " ");
            }
        }


        //foreach (ListItem li in chkServiceList.Items)
        //{
        //    if (li.Selected)
        //    {
        //        chkList.Add(li.Value + " ");
        //    }
        //}

        //foreach (ListItem li in chkFacilitiesList.Items)
        //{
        //    if (li.Selected)
        //    {
        //        chkList.Add(li.Value + " ");
        //    }
        //}
        hotelFacilitiesDBEntity.AryFalLisT = chkList;
        hotelFacilitiesDBEntity.HotelID = hidHotelID.Value;

        _hotelfacilitiesEntity.HotelFacilitiesDBEntity.Add(hotelFacilitiesDBEntity);
        int iResult = HotelFacilitiesBP.HotelInsert(_hotelfacilitiesEntity);

        _commonEntity.LogMessages = _hotelfacilitiesEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "酒店服务设施设置-添加";
        commonDBEntity.Event_ID = hidHotelID.Value;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();

        string[] strDataFields = (string[])chkList.ToArray(typeof(string));
        string arlist = string.Concat(strDataFields);

        conTent = string.Format(conTent, hidHotelID.Value, lbHotelNM.Text, arlist);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
            MessageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        UpdatePanel4.Update();
    }
}