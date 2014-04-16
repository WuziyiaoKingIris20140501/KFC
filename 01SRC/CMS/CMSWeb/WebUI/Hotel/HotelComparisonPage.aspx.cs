using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

public partial class HotelComparisonPage : BasePage
{
    LmSystemLogEntity _lmSystemLogEntity = new LmSystemLogEntity();
    CommonEntity _commonEntity = new CommonEntity();
    //private string orderXPaymentCode = "";
    //private string orderInTheNight = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !this.Page.Request.QueryString.ToString().Contains("Type=hotel") && !this.Page.Request.QueryString.ToString().Contains("Type=city") && !this.Page.Request.QueryString.ToString().Contains("Type=sales"))
        {
            ViewState["CityID"] = "";
            ViewState["HotelID"] = "";
            ViewState["SaleID"] = "";
            ViewState["DSourceType"] = "";
            ViewState["DSourceData"] = "";
            ViewState["Discount"] = "";
            hidDSourceType.Value = "ELONG";
            hidDSourceData.Value = "";
            ddlDDpbind();
        }
    }

    public void ddlDDpbind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("Discount").ToString());
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "DISCOUNTCD";
            dsResult.Tables[0].Columns["Value"].ColumnName = "DISCOUNTDIS";
            dsResult.Tables[0].Rows.RemoveAt(0);

            DataRow dr0 = dsResult.Tables[0].NewRow();
            dr0["DISCOUNTCD"] = "";
            dr0["DISCOUNTDIS"] = "不限制";
            dsResult.Tables[0].Rows.InsertAt(dr0, 0);

            ddpDiscount.DataTextField = "DISCOUNTDIS";
            ddpDiscount.DataValueField = "DISCOUNTCD";
            ddpDiscount.DataSource = dsResult.Tables[0].DefaultView;
            ddpDiscount.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewLmSystemLogListGrid();
    }

    //private int CountLmSystemLog()
    //{
    //    _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

    //    _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
    //    _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
    //    _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SaleID"].ToString())) ? null : ViewState["SaleID"].ToString();

    //    _lmSystemLogEntity.IsReserve = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DSourceType"].ToString())) ? null : ViewState["DSourceType"].ToString();
    //    _lmSystemLogEntity.GuestName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DSourceData"].ToString())) ? null : ViewState["DSourceData"].ToString();

    //    DataSet dsResult = LmSystemLogBP.HotelComparisonSelectCount(_lmSystemLogEntity).QueryResult;
    //    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
    //    {
    //        return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
    //    }

    //    return 0;
    //}

    private void BindReviewLmSystemLogListGrid()
    {
        //messageContent.InnerHtml = "";

        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
       
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SaleID"].ToString())) ? null : ViewState["SaleID"].ToString();

        _lmSystemLogEntity.DSourceType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DSourceType"].ToString())) ? null : ViewState["DSourceType"].ToString();
        _lmSystemLogEntity.DSourceData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DSourceData"].ToString())) ? null : ViewState["DSourceData"].ToString();
        _lmSystemLogEntity.Discount = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Discount"].ToString())) ? null : ViewState["Discount"].ToString();

        _lmSystemLogEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _lmSystemLogEntity.PageSize = gridViewCSReviewLmSystemLogList.PageSize;

        _lmSystemLogEntity = LmSystemLogBP.HotelComparisonSelect(_lmSystemLogEntity);
        DataSet dsResult = _lmSystemLogEntity.QueryResult;
        gridViewCSReviewLmSystemLogList.DataSource = dsResult.Tables["Detail"].DefaultView;
        gridViewCSReviewLmSystemLogList.DataKeyNames = new string[] { "Hotel_ID" };//主键
        gridViewCSReviewLmSystemLogList.DataBind();

        if (dsResult.Tables.Count > 0 && dsResult.Tables["Detail"].Rows.Count > 0)
        {
            lbUpdatTime.Text = dsResult.Tables["Detail"].Rows[dsResult.Tables["Detail"].Rows.Count - 1]["Update_time"].ToString();
        }

        AspNetPager1.PageSize = gridViewCSReviewLmSystemLogList.PageSize;
        AspNetPager1.RecordCount = _lmSystemLogEntity.TotalCount;//CountLmSystemLog();

        if (dsResult.Tables.Count > 0 && dsResult.Tables["Master"].Rows.Count > 0)
        {
            lbBHLID.Text = dsResult.Tables["Master"].Rows[0]["BHLID"].ToString();
            lbLHLID.Text = dsResult.Tables["Master"].Rows[0]["LHLID"].ToString();
            lbDHLID.Text = dsResult.Tables["Master"].Rows[0]["DHLID"].ToString();

            lbBRMCD.Text = dsResult.Tables["Master"].Rows[0]["BRMCD"].ToString();
            lbLRMCD.Text = dsResult.Tables["Master"].Rows[0]["LRMCD"].ToString();
            lbDRMCD.Text = dsResult.Tables["Master"].Rows[0]["DRMCD"].ToString();
        }
        else
        {
            lbBHLID.Text = "0";
            lbLHLID.Text = "0";
            lbDHLID.Text = "0";

            lbBRMCD.Text = "0";
            lbLRMCD.Text = "0";
            lbDRMCD.Text = "0";
        }
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["CityID"] = (hidCity.Value.Trim().Contains("[") && hidCity.Value.Trim().Contains("]")) ? hidCity.Value.Trim().Substring((hidCity.Value.Trim().IndexOf('[') + 1), (hidCity.Value.Trim().IndexOf(']') - 1)) : hidCity.Value.Trim();
        ViewState["HotelID"] = (hidHotel.Value.Trim().Contains("[") && hidHotel.Value.Trim().Contains("]")) ? hidHotel.Value.Trim().Substring((hidHotel.Value.Trim().IndexOf('[') + 1), (hidHotel.Value.Trim().IndexOf(']') - 1)) : hidHotel.Value.Trim();
        ViewState["SaleID"] = (hidSales.Value.Trim().Contains("[") && hidSales.Value.Trim().Contains("]")) ? hidSales.Value.Trim().Substring((hidSales.Value.Trim().IndexOf('[') + 1), (hidSales.Value.Trim().IndexOf(']') - 1)) : hidSales.Value.Trim();


        ViewState["DSourceType"] = hidDSourceType.Value.Trim();
        ViewState["DSourceData"] = hidDSourceData.Value.Trim();

        ViewState["Discount"] = ddpDiscount.SelectedValue;

        AspNetPager1.CurrentPageIndex = 1;
        BindReviewLmSystemLogListGrid();
    }

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        _lmSystemLogEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _lmSystemLogEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _lmSystemLogEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _lmSystemLogEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
       
        _lmSystemLogEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        _lmSystemLogEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
        _lmSystemLogEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SaleID"].ToString())) ? null : ViewState["SaleID"].ToString();

        _lmSystemLogEntity.DSourceType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DSourceType"].ToString())) ? null : ViewState["DSourceType"].ToString();
        _lmSystemLogEntity.DSourceData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DSourceData"].ToString())) ? null : ViewState["DSourceData"].ToString();
        _lmSystemLogEntity.Discount = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Discount"].ToString())) ? null : ViewState["Discount"].ToString();

        DataSet dsResult = LmSystemLogBP.ExportHotelComparisonSelect(_lmSystemLogEntity).QueryResult;


        CommonFunction.ExportExcelForLMList(dsResult);
    }

    protected void gridViewCSReviewLmSystemLogList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSReviewLmSystemLogList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E9E9E9'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }
}