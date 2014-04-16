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

public partial class APPContentSearch : BasePage
{
    APPContentEntity _appcontentEntity = new APPContentEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlOnlinebind();
            BindCityDDL();
            BindCityListGrid();
        }
        else
        {
            messageContent.InnerHtml = "";
        }
    }

    public void ddlOnlinebind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "ONLINETYPE";
            dsResult.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";

            ddpTypeList.DataTextField = "ONLINEDIS";
            ddpTypeList.DataValueField = "ONLINETYPE";
            ddpTypeList.DataSource = dsResult;
            ddpTypeList.DataBind();
        }
       
        int itime = int.Parse(DateTime.Now.Hour.ToString());
        if ((18 <= itime && itime <= 23) || (0 <= itime && itime <= 4))
        {
            ddpTypeList.SelectedValue = "1";
            ViewState["TYPE"] = "1";
        }
        else
        {
            ddpTypeList.SelectedValue = "0";
            ViewState["TYPE"] = "0";
        }
    }

    private void BindCityDDL()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

         DataSet dsVerResult = GetServiceVer();
         ddpServiceVer.DataSource = dsVerResult;
         ddpServiceVer.DataTextField = "verNM";
         ddpServiceVer.DataValueField = "verid";         
         ddpServiceVer.DataBind();

         _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
         _appcontentEntity.APPContentDBEntity.Add(new APPContentDBEntity());
         _appcontentEntity.APPContentDBEntity[0].SerVer = "2";
        
        ddpServiceVer.SelectedValue = "2";

         BandCityDdpList();
        //DataSet dsResult = APPContentBP.CommonSelect(_appcontentEntity).QueryResult;
        //ddpCityList.DataTextField = "cityNM";
        //ddpCityList.DataValueField = "cityid";
        //ddpCityList.DataSource = dsResult;
        //ddpCityList.DataBind();

        DataSet dsPlatResult = APPContentBP.CommonPlatSelect(_appcontentEntity).QueryResult;
        ddpPlatform.DataTextField = "platformname";
        ddpPlatform.DataValueField = "platformcode";
        ddpPlatform.DataSource = dsPlatResult;
        ddpPlatform.DataBind();

        
        ddpCityList.SelectedValue = "Shanghai";
        ddpPlatform.SelectedValue = "IOS";

        ViewState["CITYID"] = ddpCityList.SelectedValue;
        ViewState["PLATFORM"] = ddpPlatform.SelectedValue;
        ViewState["SERVER"] = ddpServiceVer.SelectedValue;
    }

    private void BandCityDdpList()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        _appcontentEntity.APPContentDBEntity.Add(new APPContentDBEntity());
        _appcontentEntity.APPContentDBEntity[0].SerVer = ddpServiceVer.SelectedValue;
        _appcontentEntity.APPContentDBEntity[0].PlatForm = ddpPlatform.SelectedValue;
        DataSet dsResult = APPContentBP.CommonSelect(_appcontentEntity).QueryResult;
        ddpCityList.DataSource = null;
        ddpCityList.DataSource = dsResult;
        ddpCityList.DataTextField = "cityNM";
        ddpCityList.DataValueField = "cityid";        
        ddpCityList.DataBind();
    }

    private DataSet GetServiceVer()
    {
        DataSet dsResult = new DataSet();
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("verid");
        dtResult.Columns.Add("verNM");

        for (int i = 1; i < 3;i++ )
        {
            DataRow drRow = dtResult.NewRow();
            drRow["verid"] = i.ToString();
            drRow["verNM"] = i.ToString() + ".0";
            dtResult.Rows.Add(drRow);
        }
        dsResult.Tables.Add(dtResult);
        return dsResult;
    }

    private void BindCityListGrid()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CITYID"].ToString())) ? null : ViewState["CITYID"].ToString();
        appcontentDBEntity.TypeID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TYPE"].ToString())) ? null : ViewState["TYPE"].ToString();
        appcontentDBEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PLATFORM"].ToString())) ? null : ViewState["PLATFORM"].ToString();
        appcontentDBEntity.SerVer = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SERVER"].ToString())) ? null : ViewState["SERVER"].ToString();
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        DataSet dsResult = APPContentBP.Select(_appcontentEntity).QueryResult;
        gridViewCSAPPContenList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSAPPContenList.DataKeyNames = new string[] { "HOTELID" };//主键
        gridViewCSAPPContenList.DataBind();
        string strContent = String.Format(GetLocalResourceObject("SelectCount").ToString(), dsResult.Tables[0].Rows.Count.ToString());
        messageContent.InnerHtml = strContent;


        for (int i = 0; i < gridViewCSAPPContenList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridViewCSAPPContenList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                for (int j = 0; j < gridViewCSAPPContenList.Rows[i].Cells.Count - 1; j++)
                {
                    switch (j)
                    {
                        case 1:

                            break;
                        case 2:
                            Image iHotelPic = (Image)gridViewCSAPPContenList.Rows[i].Cells[j].FindControl("imgHotelPic");
                            if (String.IsNullOrEmpty(iHotelPic.ImageUrl))
                            {
                                gridViewCSAPPContenList.Rows[i].Cells[j].Attributes.Add("bgcolor", "#FF6666");
                            }
                            break;
                        case 5:
                            if (String.IsNullOrEmpty(gridViewCSAPPContenList.Rows[i].Cells[j].Text) || decimal.Parse(gridViewCSAPPContenList.Rows[i].Cells[j].Text) < 10)
                            {
                                gridViewCSAPPContenList.Rows[i].Cells[j].Attributes.Add("bgcolor", "#FF6666");
                            }
                            break;
                        default:
                            if (String.IsNullOrEmpty(gridViewCSAPPContenList.Rows[i].Cells[j].Text) || String.IsNullOrEmpty(gridViewCSAPPContenList.Rows[i].Cells[j].Text.Replace("&nbsp;", "").Trim()))
                            {
                                gridViewCSAPPContenList.Rows[i].Cells[j].Attributes.Add("bgcolor", "#FF6666");
                            }
                            break;
                    }
                }
            }
        }
    }

    protected void gridViewCSAPPContenList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSAPPContenList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbHotelNm = (Label)e.Row.FindControl("lbGRHotelNm");
            if (lbHotelNm != null && String.IsNullOrEmpty(lbHotelNm.Text))
            {
                e.Row.Cells[1].Attributes.Add("bgcolor", "#FF6666");
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["CITYID"] = ddpCityList.SelectedValue;
        ViewState["TYPE"] = ddpTypeList.SelectedValue;
        ViewState["PLATFORM"] = ddpPlatform.SelectedValue;
        ViewState["SERVER"] = ddpServiceVer.SelectedValue;
        BindCityListGrid();
    }

    protected void gridViewCSAPPContenList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSAPPContenList.PageIndex = e.NewPageIndex;
        BindCityListGrid();
    }

    protected void ddpServiceVer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BandCityDdpList();
    }
}