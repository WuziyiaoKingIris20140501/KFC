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
using HotelVp.CMS.Domain.Entity.Hotel;
using HotelVp.CMS.Domain.Process.Hotel;

public partial class WebUI_Hotel_HotelBlackListManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropChannel();
        }
    }



    /// <summary>
    /// 绑定分销渠道
    /// </summary>
    public void BindDropChannel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ChannelValue");
        dt.Columns.Add("ChannelText");

        DataRow row = dt.NewRow();
        row["ChannelValue"] = "";
        row["ChannelText"] = "请选择";
        dt.Rows.Add(row);

        row = dt.NewRow();
        row["ChannelValue"] = "QUNAR";
        row["ChannelText"] = "去哪儿";
        dt.Rows.Add(row);

        DropChannel.DataSource = dt;
        DropChannel.DataTextField = "ChannelText";
        DropChannel.DataValueField = "ChannelValue";
        DropChannel.DataBind();
        DropChannel.SelectedIndex = 1;

        DropChannelDiv.DataSource = dt;
        DropChannelDiv.DataTextField = "ChannelText";
        DropChannelDiv.DataValueField = "ChannelValue";
        DropChannelDiv.DataBind();
        DropChannelDiv.SelectedIndex = 1;
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        MessageContentDiv.InnerHtml = "";
        string strHotel = this.HidHotelValue.Value;
        if (!string.IsNullOrEmpty(strHotel))
        {
            if (!strHotel.Trim().Contains("[") || !strHotel.Trim().Contains("]"))
            {
                MessageContent.InnerHtml = "酒店选择不合法!";
                return;
            }
            strHotel = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
            ViewState["strHotel"] = strHotel;
        }
        else
        {
            ViewState["strHotel"] = strHotel;
        }
        BindReviewLmSystemLogListGrid();

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "btnSearch", "BtnCompleteStyle();", true);
    }

    protected void gridHotelList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=t;");
        }
    }


    protected void gridHotelList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        MessageContent.InnerHtml = "";
        HotelBlackEntity _hotelblackEntity = new HotelBlackEntity();
        _hotelblackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelblackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelblackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelblackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelblackEntity.HotelBlackDBEntity = new List<HotelBlackDBEntity>();
        HotelBlackDBEntity hotelblackdbEntity = new HotelBlackDBEntity();

        string isBlack = gridHotelList.DataKeys[e.RowIndex].Values[1].ToString();
        hotelblackdbEntity.ID = gridHotelList.DataKeys[e.RowIndex].Values[0].ToString();
        hotelblackdbEntity.IsBlack = isBlack == "黑名单" ? "0" : "1";
        _hotelblackEntity.HotelBlackDBEntity.Add(hotelblackdbEntity);
        //删除  
        //int i = HotelBlackBP.DeleteHotelBlackList(_hotelblackEntity);
        //MessageContent.InnerHtml = i == 1 ? "黑名单酒店删除成功！" : "黑名单酒店删除失败！";
        //修改 
        int i = HotelBlackBP.UpdateHotelBlackList(_hotelblackEntity);
        MessageContent.InnerHtml = i == 1 ? "黑名单酒店处理成功！" : "黑名单酒店处理失败！";
        //btnSearch_Click(null, null);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "btnDeleteE", "BtnCompleteStyle();", true);

        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "keyDeleteClickEvent1", "btnSeachClick()", true);
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindReviewLmSystemLogListGrid();
    }

    public void BindReviewLmSystemLogListGrid()
    {
        HotelBlackEntity _hotelblackEntity = new HotelBlackEntity();
        _hotelblackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelblackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelblackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelblackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelblackEntity.HotelBlackDBEntity = new List<HotelBlackDBEntity>();
        HotelBlackDBEntity hotelblackdbEntity = new HotelBlackDBEntity();

        if (ViewState["strHotel"].ToString() != "") { hotelblackdbEntity.HotelId = ViewState["strHotel"].ToString(); }
        hotelblackdbEntity.Source = this.DropChannel.SelectedValue;

        _hotelblackEntity.HotelBlackDBEntity.Add(hotelblackdbEntity);
        _hotelblackEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _hotelblackEntity.PageSize = gridHotelList.PageSize;

        DataSet dsResult = HotelBlackBP.GetHotelBlackList(_hotelblackEntity).QueryResult;

        this.gridHotelList.DataSource = dsResult.Tables[0].DefaultView;
        this.gridHotelList.DataBind();

        AspNetPager1.PageSize = gridHotelList.PageSize;
        AspNetPager1.RecordCount = CountLmSystemLog();
    }

    public int CountLmSystemLog()
    {
        HotelBlackEntity _hotelblackEntity = new HotelBlackEntity();
        _hotelblackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelblackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelblackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelblackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelblackEntity.HotelBlackDBEntity = new List<HotelBlackDBEntity>();
        HotelBlackDBEntity hotelblackdbEntity = new HotelBlackDBEntity();

        if (ViewState["strHotel"].ToString() != "") { hotelblackdbEntity.HotelId = ViewState["strHotel"].ToString(); }
        hotelblackdbEntity.Source = this.DropChannel.SelectedValue;
        DataSet dsResult = HotelBlackBP.GetHotelBlackListByCount(_hotelblackEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
        {
            return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
        }

        return 0;
    }

    /// <summary>
    /// 添加黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddHotelBlack_Click(object sender, EventArgs e)
    {
        MessageContent.InnerHtml = "";
        string strHotel = wctHotelDiv.AutoResult;
        this.HidHotelValue.Value = strHotel;
        strHotel = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));

        HotelBlackEntity _hotelblackEntity = new HotelBlackEntity();
        _hotelblackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelblackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelblackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelblackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelblackEntity.HotelBlackDBEntity = new List<HotelBlackDBEntity>();
        HotelBlackDBEntity hotelblackdbEntity = new HotelBlackDBEntity();

        hotelblackdbEntity.HotelId = strHotel;
        hotelblackdbEntity.Source = this.DropChannelDiv.SelectedValue;
        _hotelblackEntity.HotelBlackDBEntity.Add(hotelblackdbEntity);
        DataTable dtResult = HotelBlackBP.RepeatHotelBlackList(_hotelblackEntity).QueryResult.Tables[0];
        _hotelblackEntity.HotelBlackDBEntity.Clear();
        if (dtResult != null && dtResult.Rows.Count > 0)
        {
            //MessageContentDiv.InnerHtml = "该酒店已存在!"; 
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "btnAdd2", "invokeOpen3();", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "btnAdd3", "btnConfirm('" + dtResult.Rows[0]["is_black"].ToString().Replace("1", "黑名单").Replace("0", "白名单") + "');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "btnSet", "SetControlValue2();", true);
            return;
        }
        this.HidHotelValue.Value = "";
        hotelblackdbEntity.HotelId = strHotel;
        hotelblackdbEntity.Source = this.DropChannelDiv.SelectedValue;
        hotelblackdbEntity.IsBlack = "1";//0和空为白名单 ; 1=黑名单
        hotelblackdbEntity.CreateUser = UserSession.Current.UserDspName;
        _hotelblackEntity.HotelBlackDBEntity.Add(hotelblackdbEntity);
        int i = HotelBlackBP.InsertHotelBlackList(_hotelblackEntity);
        MessageContent.InnerHtml = i == 1 ? "黑名单酒店添加成功！" : "黑名单酒店添加失败！";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "btnAdd", "BtnCompleteStyle();", true);
        //btnSearch_Click(null, null);
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "keyClickEvent2", "btnSeachClick()", true);
    }

    protected void btnUpdateHotelBlack_Click(object sender, EventArgs e)
    {
        string strHotel = wctHotelDiv.AutoResult;
        this.HidHotelValue.Value = strHotel;
        strHotel = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));

        HotelBlackEntity _hotelblackEntity = new HotelBlackEntity();
        _hotelblackEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelblackEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelblackEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelblackEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelblackEntity.HotelBlackDBEntity = new List<HotelBlackDBEntity>();
        HotelBlackDBEntity hotelblackdbEntity = new HotelBlackDBEntity();
        hotelblackdbEntity.HotelId = strHotel;
        hotelblackdbEntity.Source = this.DropChannelDiv.SelectedValue;
        hotelblackdbEntity.IsBlack = "1";//0和空为白名单 ; 1=黑名单
        hotelblackdbEntity.UpdateUser = UserSession.Current.UserDspName;
        _hotelblackEntity.HotelBlackDBEntity.Add(hotelblackdbEntity);
        int i = HotelBlackBP.UpdateHotelBlackListByExist(_hotelblackEntity);
        MessageContent.InnerHtml = i == 1 ? "黑名单酒店添加成功！" : "黑名单酒店添加失败！";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "btnAdd", "BtnCompleteStyle();", true);
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "keyClickEvent2", "btnSeachClick()", true);
    }
}