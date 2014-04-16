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

public partial class HotelGroupSearchPage : BasePage
{
    HotelGroupEntity _hotelGroupEntity = new HotelGroupEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGroupListGrid();
        }
    }

    public void BindGroupListGrid()
    {
        messageContent.InnerHtml = "";
        _hotelGroupEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelGroupEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelGroupEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelGroupEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelGroupEntity.HotelGroupDBEntity = new List<HotelGroupDBEntity>();
        HotelGroupDBEntity hotelGroupDBEntity = new HotelGroupDBEntity();
        hotelGroupDBEntity.HotelGroupID = "";
        _hotelGroupEntity.HotelGroupDBEntity.Add(hotelGroupDBEntity);

        DataSet dsResult = HotelGroupBP.Select(_hotelGroupEntity).QueryResult;

        gridViewCSHGroupList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSHGroupList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSHGroupList.DataBind();

        if (!String.IsNullOrEmpty(refushFlag.Value))
        {
            if ("1".Equals(hiddenType.Value.Trim()))
            {
                messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            }
            else
            {
                messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            }
            
            refushFlag.Value = "";
            hiddenType.Value = "";
        }
    }

    protected void gridViewCSHGroupList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSHGroupList.Rows.Count; i++)
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGroupListGrid();
    }

    protected void gridViewCSHGroupList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridViewCSHGroupList.EditIndex = e.NewEditIndex;
        BindGroupListGrid();
    }

    protected void gridViewCSHGroupList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gridViewCSHGroupList.EditIndex = -1;
        BindGroupListGrid();
    }

    protected void gridViewCSHGroupList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridViewCSHGroupList.EditIndex = -1;
        BindGroupListGrid();
    }

    protected void gridViewCSHGroupList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridViewCSHGroupList.PageIndex = e.NewPageIndex;
        BindGroupListGrid();
    }
}