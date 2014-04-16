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

public partial class HotelOnlinePlan : BasePage
{
    HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbInfo.Text = "查询结果";// string.Format("总酒店数:{0}&nbsp;&nbsp;&nbsp;下线酒店:{1}&nbsp;({2}%)&nbsp;&nbsp;&nbsp;上线酒店:{3}&nbsp;({4}%)&nbsp;&nbsp;&nbsp;满房酒店:{5}&nbsp;({6}%)", 0, 0, 0, 0, 0, 0, 0);
            dpEffectDate.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        if (String.IsNullOrEmpty(dpEffectDate.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if (!String.IsNullOrEmpty(hidCityID.Value.Trim()) && (!hidCityID.Value.Trim().Contains("[") || !hidCityID.Value.Trim().Contains("]")))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if (!String.IsNullOrEmpty(hidHotelID.Value.Trim()) && (!hidHotelID.Value.Trim().Contains("[") || !hidHotelID.Value.Trim().Contains("]")))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        if (!String.IsNullOrEmpty(hidAreaID.Value.Trim()) && (!hidAreaID.Value.Trim().Contains("[") || !hidAreaID.Value.Trim().Contains("]")))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error4").ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
            return;
        }

        AspNetPager1.CurrentPageIndex = 1;
        BindReviewUserListGrid();
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
    }

    private void BindReviewUserListGrid()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = (!String.IsNullOrEmpty(hidHotelID.Value.Trim())) ? hidHotelID.Value.Trim().Substring((hidHotelID.Value.Trim().IndexOf('[') + 1), (hidHotelID.Value.Trim().IndexOf(']') - 1)) : hidHotelID.Value.Trim();
        hotelInfoDBEntity.City = (!String.IsNullOrEmpty(hidCityID.Value.Trim())) ? hidCityID.Value.Trim().Substring((hidCityID.Value.Trim().IndexOf('[') + 1), (hidCityID.Value.Trim().IndexOf(']') - 1)) : hidCityID.Value.Trim();
        hotelInfoDBEntity.AreaID = (!String.IsNullOrEmpty(hidAreaID.Value.Trim())) ? hidAreaID.Value.Trim().Substring((hidAreaID.Value.Trim().IndexOf('[') + 1), (hidAreaID.Value.Trim().IndexOf(']') - 1)) : hidAreaID.Value.Trim();
        hotelInfoDBEntity.EffectDate = dpEffectDate.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        _hotelinfoEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _hotelinfoEntity.PageSize = gridViewCSReviewUserList.PageSize;

        DataSet dsResult = HotelInfoBP.GetHotelPlanInfoList(_hotelinfoEntity).QueryResult;
        gridViewCSReviewUserList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewUserList.DataKeyNames = new string[] { "HOTELID" };//主键
        gridViewCSReviewUserList.DataBind();

        AspNetPager1.PageSize = gridViewCSReviewUserList.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();

        //if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        //{
        //    lbUserHotelNum.Text = dsResult.Tables[0].Compute("sum(HTSUM)", "true").ToString(); 
        //}
        //else
        //{
        //    lbUserHotelNum.Text = "0";
        //}
        CountHotelOnlineLb();
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewUserListGrid();
    }

    private int CountLmSystemLog()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = (!String.IsNullOrEmpty(hidHotelID.Value.Trim())) ? hidHotelID.Value.Trim().Substring((hidHotelID.Value.Trim().IndexOf('[') + 1), (hidHotelID.Value.Trim().IndexOf(']') - 1)) : hidHotelID.Value.Trim();
        hotelInfoDBEntity.City = (!String.IsNullOrEmpty(hidCityID.Value.Trim())) ? hidCityID.Value.Trim().Substring((hidCityID.Value.Trim().IndexOf('[') + 1), (hidCityID.Value.Trim().IndexOf(']') - 1)) : hidCityID.Value.Trim();
        hotelInfoDBEntity.AreaID = (!String.IsNullOrEmpty(hidAreaID.Value.Trim())) ? hidAreaID.Value.Trim().Substring((hidAreaID.Value.Trim().IndexOf('[') + 1), (hidAreaID.Value.Trim().IndexOf(']') - 1)) : hidAreaID.Value.Trim();
        hotelInfoDBEntity.EffectDate = dpEffectDate.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        _hotelinfoEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _hotelinfoEntity.PageSize = gridViewCSReviewUserList.PageSize;

        DataSet dsResult = HotelInfoBP.GetHotelPlanInfoCount(_hotelinfoEntity).QueryResult;
           

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
        }

        return 0;
    }

    private void CountHotelOnlineLb()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = (!String.IsNullOrEmpty(hidHotelID.Value.Trim())) ? hidHotelID.Value.Trim().Substring((hidHotelID.Value.Trim().IndexOf('[') + 1), (hidHotelID.Value.Trim().IndexOf(']') - 1)) : hidHotelID.Value.Trim();
        hotelInfoDBEntity.City = (!String.IsNullOrEmpty(hidCityID.Value.Trim())) ? hidCityID.Value.Trim().Substring((hidCityID.Value.Trim().IndexOf('[') + 1), (hidCityID.Value.Trim().IndexOf(']') - 1)) : hidCityID.Value.Trim();
        hotelInfoDBEntity.AreaID = (!String.IsNullOrEmpty(hidAreaID.Value.Trim())) ? hidAreaID.Value.Trim().Substring((hidAreaID.Value.Trim().IndexOf('[') + 1), (hidAreaID.Value.Trim().IndexOf(']') - 1)) : hidAreaID.Value.Trim();
        hotelInfoDBEntity.EffectDate = dpEffectDate.Value;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        _hotelinfoEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _hotelinfoEntity.PageSize = gridViewCSReviewUserList.PageSize;

        DataSet dsResult = HotelInfoBP.CountHotelOnlineLb(_hotelinfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            decimal dcALBP = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["ALBP"].ToString())) ? 0 : decimal.Parse(dsResult.Tables[0].Rows[0]["ALBP"].ToString());
            decimal dcOLBP = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["OLBP"].ToString())) ? 0 : decimal.Parse(dsResult.Tables[0].Rows[0]["OLBP"].ToString());
            decimal dcRFBP = (String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["RFBP"].ToString())) ? 0 : decimal.Parse(dsResult.Tables[0].Rows[0]["RFBP"].ToString());
            decimal dcFLBP = dcALBP - dcOLBP;


            decimal avgOLBP = (dcALBP == 0) ? 0 : (dcOLBP / dcALBP) * 100;
            decimal avgRFBP = (dcOLBP == 0) ? 0 : (dcRFBP / dcOLBP) * 100;
            decimal avgFLBP = (dcALBP == 0) ? 0 : (dcFLBP / dcALBP) * 100;

            lbInfo.Text = string.Format("总酒店数:{0}&nbsp;&nbsp;&nbsp;下线酒店:{1}&nbsp;({2}%)&nbsp;&nbsp;&nbsp;上线酒店:{3}&nbsp;({4}%)&nbsp;&nbsp;&nbsp;满房酒店:{5}&nbsp;({6}%)", dcALBP, dcFLBP, avgFLBP.ToString("#,##0.##"), dcOLBP, avgOLBP.ToString("#,##0.##"), dcRFBP, avgRFBP.ToString("#,##0.##"));
        }
        else
        {
            lbInfo.Text = string.Format("总酒店数:{0}&nbsp;&nbsp;&nbsp;下线酒店:{1}&nbsp;({2}%)&nbsp;&nbsp;&nbsp;上线酒店:{3}&nbsp;({4}%)&nbsp;&nbsp;&nbsp;满房酒店:{5}&nbsp;({6}%)", 0, 0, 0, 0, 0, 0, 0);
        }
    }

    protected void gridViewCSReviewUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSReviewUserList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#B2DAFD'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }
}