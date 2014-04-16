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
using HotelVp.CMS.Domain.Process.GeneralSetting;
using HotelVp.CMS.Domain.Entity.GeneralSetting;


public partial class WebUI_GeneralSetting_HotelTagInfoSearchPage : System.Web.UI.Page
{
    TagInfoEntity _tagInfoEntity = new TagInfoEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        string a = "";
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindChanelListGrid();
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
    }

    private void BindChanelListGrid()
    {
        _tagInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _tagInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _tagInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _tagInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _tagInfoEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _tagInfoEntity.PageSize = gridViewTagInfoList.PageSize;

        if (!string.IsNullOrEmpty(HidCityName.Value))
        {
            if (!HidCityName.Value.Trim().Contains("[") || !HidCityName.Value.Trim().Contains("]"))
            {
                messageContent.InnerHtml = "查询失败，选择酒店不合法，请修改！";
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                return;
            }
            _tagInfoEntity.CityID = this.HidCityName.Value.Substring((this.HidCityName.Value.IndexOf('[') + 1), (this.HidCityName.Value.IndexOf(']') - 1));   
        }
        _tagInfoEntity.TagName = txtTagName.Text;

        DataSet dsResult = TagInfoBP.TagInfoSearch(_tagInfoEntity).QueryResult;

        gridViewTagInfoList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewTagInfoList.DataKeyNames = new string[] { "ID" };//主键
        gridViewTagInfoList.DataBind();

        AspNetPager1.PageSize = gridViewTagInfoList.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    private int CountLmSystemLog()
    {
        _tagInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _tagInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _tagInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _tagInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _tagInfoEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _tagInfoEntity.PageSize = gridViewTagInfoList.PageSize;

        _tagInfoEntity.CityID = "";
        _tagInfoEntity.TagName = "";

        DataSet dsResult = TagInfoBP.TagInfoSearchCount(_tagInfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
        }

        return 0;
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindChanelListGrid();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDivAdd_Click(object sender, EventArgs e)
    {
        _tagInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _tagInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _tagInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _tagInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _tagInfoEntity.CityID = this.hidSelectCity.Value.Substring((this.hidSelectCity.Value.IndexOf('[') + 1), (this.hidSelectCity.Value.IndexOf(']') - 1));
        _tagInfoEntity.CityName = this.hidSelectCity.Value.Substring((this.hidSelectCity.Value.IndexOf(']') + 1), (this.hidSelectCity.Value.Length - this.hidSelectCity.Value.IndexOf(']') - 1));
        _tagInfoEntity.TagName = this.txtDviTagName.Text;
        _tagInfoEntity.Longitude = this.txtDivLongitude.Text;
        _tagInfoEntity.Latitude = this.txtDivLatitude.Text;
        _tagInfoEntity.TypeID = this.ddlDivTypeId.SelectedValue;
        _tagInfoEntity.Status = this.ddlDivStatus.SelectedValue;
        _tagInfoEntity.PinyinLong = this.txtDivPinyinLong.Text;
        _tagInfoEntity.PinyinShort = this.txtDivPinyinShort.Text;

        int i = 0;
        if (this.hidStatus.Value != "Edit" && this.hidSelectID.Value == "")
        {
            i = TagInfoBP.TagInfoInsert(_tagInfoEntity);
        }
        else
        {
            _tagInfoEntity.Id = this.hidSelectID.Value;
            i = TagInfoBP.TagInfoUpdate(_tagInfoEntity);
            this.hidSelectID.Value = "";
            this.hidStatus.Value = "";
        }
        BindChanelListGrid();
    }

    protected void gridViewTagInfoList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewTagInfoList.Rows.Count; i++)
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
    }

    protected void gridViewTagInfoList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "updateTagInfo", "invokeOpen();", true);
    }
}