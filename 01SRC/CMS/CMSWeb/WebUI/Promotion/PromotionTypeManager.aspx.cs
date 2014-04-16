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
using HotelVp.CMS.Domain.Process.Promotion;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Entity.Promotion;

public partial class WebUI_Promotion_PromotionTypeManager : BasePage
{
    public DataSet NewDs = new DataSet();
    PromotionTypeEntity _promotionTypeEntity = new PromotionTypeEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.ToLower() == "post" && Request.Form["type"] != null)
        {
            string[] newId = Request.Form["id"].Split(',');
            string[] oldID = Request.Form["order"].Split(',');
            for (int l = 0; l < newId.Length; l++)
            {
                btnUpdateRegChannel(newId[l].Split('|')[0].ToString().ToString(), newId[l].ToString().Split('|')[1].ToString(), l.ToString());
            }
        }

        //if (!IsPostBack)
        //{
        //    BindRegChanelListGrid();
        //}
        BindRegChanelListGrid();
    }

    //现有促销方式
    private void BindRegChanelListGrid()
    {
        _promotionTypeEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionTypeEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionTypeEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionTypeEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _promotionTypeEntity.PromotiontypeDBEntity = new List<PromotionTypeDBEntity>();
        PromotionTypeDBEntity promotionTypeDBEntity = new PromotionTypeDBEntity();

        promotionTypeDBEntity.Name = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name"].ToString())) ? null : ViewState["Name"].ToString();

        _promotionTypeEntity.PromotiontypeDBEntity.Add(promotionTypeDBEntity);

        DataSet dsResult = PromotionTypeBP.CommonSelect(_promotionTypeEntity).QueryResult;
        NewDs = dsResult;
        //gridViewCSRegChannelList.DataSource = dsResult.Tables[0].DefaultView;
        //gridViewCSRegChannelList.DataKeyNames = new string[] { "ID" };//主键
        //gridViewCSRegChannelList.DataBind();
    }

    //protected void gridViewCSRegChannelList_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    //this.gridViewRegion.PageIndex = e.NewPageIndex;
    //    //BindGridView();

    //    //执行循环，保证每条数据都可以更新
    //    for (int i = 0; i <= gridViewCSRegChannelList.Rows.Count; i++)
    //    {
    //        //首先判断是否是数据行
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //当鼠标停留时更改背景色
    //            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
    //            //当鼠标移开时还原背景色
    //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
    //        }
    //    }

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
    //        {
    //            ((LinkButton)e.Row.Cells[4].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"(" + e.Row.Cells[1].Text + ")\"吗?')");
    //        }
    //    }
    //}


    //protected void gridViewCSRegChannelList_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gridViewCSRegChannelList.EditIndex = e.NewEditIndex;
    //    BindRegChanelListGrid();
    //}

    //protected void gridViewCSRegChannelList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    string ID = gridViewCSRegChannelList.DataKeys[e.RowIndex].Value.ToString();
    //    string Name = ((TextBox)(gridViewCSRegChannelList.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
    //    string seq = ((TextBox)(gridViewCSRegChannelList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
    //    if (String.IsNullOrEmpty(Name))
    //    {
    //        messageContent.InnerHtml ="促销名称不能为空!";
    //        return;
    //    }
    //    if (String.IsNullOrEmpty(seq))
    //    {
    //        messageContent.InnerHtml = "促销名称排序不能为空!";
    //        return;
    //    }
    //    if (!btnUpdateRegChannel(ID, Name, seq))
    //    {
    //        return;
    //    }
    //    gridViewCSRegChannelList.EditIndex = -1;
    //    BindRegChanelListGrid();
    //}

    //protected void gridViewCSRegChannelList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    gridViewCSRegChannelList.EditIndex = -1;
    //    BindRegChanelListGrid();
    //}

    //protected void gridViewCSRegChannelList_OnRowDeleting(object sender, GridViewCancelEditEventArgs e)
    //{

    //    string ID = gridViewCSRegChannelList.DataKeys[e.RowIndex].Value.ToString();
    //    BindRegChanelListGrid();
    //}

    public bool btnUpdateRegChannel(string id, string name, string seq)
    {
        messageContent.InnerHtml = "";

        _promotionTypeEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionTypeEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionTypeEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionTypeEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _promotionTypeEntity.PromotiontypeDBEntity = new List<PromotionTypeDBEntity>();
        PromotionTypeDBEntity promotionTypeDBEntity = new PromotionTypeDBEntity();
        promotionTypeDBEntity.ID = id;
        promotionTypeDBEntity.Name = name;
        promotionTypeDBEntity.Seq = seq;

        _promotionTypeEntity.PromotiontypeDBEntity.Add(promotionTypeDBEntity);
        int iResult = PromotionTypeBP.Update(_promotionTypeEntity);

        _commonEntity.LogMessages = _promotionTypeEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "促销方式-修改";
        commonDBEntity.Event_ID = id;

        commonDBEntity.Event_Content = "促销方式-修改 ID:" + id + ";Name:" + name + ";SEQ:" + seq;
        commonDBEntity.IpAddress = UserSession.Current.UserIP;
        commonDBEntity.UserID = UserSession.Current.UserAccount;
        commonDBEntity.UserName = UserSession.Current.UserDspName;
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
        return iResult == 1 ? true : false;

        messageContent.InnerHtml = "修改促销方式成功!";
        BindRegChanelListGrid();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        _promotionTypeEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionTypeEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionTypeEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionTypeEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _promotionTypeEntity.PromotiontypeDBEntity = new List<PromotionTypeDBEntity>();
        PromotionTypeDBEntity promotionTypeDBEntity = new PromotionTypeDBEntity();
        promotionTypeDBEntity.ID = this.hidden1.Value;
        promotionTypeDBEntity.Name = this.divTxtSelPromotionTypeName.Value;
        //promotionTypeDBEntity.Seq = this.hidden2.Value;
        promotionTypeDBEntity.Seq = this.topRd.Checked == true ? GetMaxSeq() : "";

        _promotionTypeEntity.PromotiontypeDBEntity.Add(promotionTypeDBEntity);
        int iResult = PromotionTypeBP.Update(_promotionTypeEntity);

        _commonEntity.LogMessages = _promotionTypeEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "促销方式-修改";
        commonDBEntity.Event_ID = this.hidden1.Value;

        commonDBEntity.Event_Content = "促销方式-修改 ID:" + this.hidden1.Value + ";Name:" + this.divTxtSelPromotionTypeName.Value + ";SEQ:" + this.hidden2.Value; ;
        commonDBEntity.IpAddress = UserSession.Current.UserIP;
        commonDBEntity.UserID = UserSession.Current.UserAccount;
        commonDBEntity.UserName = UserSession.Current.UserDspName;

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        BindRegChanelListGrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["Name"] = txtSelPromotionTypeName.Value.Trim();
        BindRegChanelListGrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string name = this.txtPromotionTypeName.Value.Trim();
        messageContent.InnerHtml = "";
        if (NewDs.Tables.Count > 0 && NewDs.Tables[0].Rows.Count > 0)
        {
            for (int j = 0; j < NewDs.Tables[0].Rows.Count; j++)
            {
                if (NewDs.Tables[0].Rows[j]["Name"].ToString().Trim() == name.Trim())
                {
                    messageContent.InnerHtml = "促销方式名称已存在!";
                    return;
                }
            }
        }

        //string seq = this.txtPromotionTypeSEQ.Value.Trim();

        _promotionTypeEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionTypeEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionTypeEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionTypeEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _promotionTypeEntity.PromotiontypeDBEntity = new List<PromotionTypeDBEntity>();
        PromotionTypeDBEntity promotionTypeDBEntity = new PromotionTypeDBEntity();

        promotionTypeDBEntity.Name = name;

        if (this.StickPromotion.Checked)
        {
            promotionTypeDBEntity.Seq = GetMaxSeq();
        }
        //promotionTypeDBEntity.Seq = GetMaxSeq();//最大Seq

        _promotionTypeEntity.PromotiontypeDBEntity.Add(promotionTypeDBEntity);

        int i = PromotionTypeBP.Insert(_promotionTypeEntity);

        _commonEntity.LogMessages = _promotionTypeEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "促销方式-添加";

        commonDBEntity.Event_Content = "促销方式-添加 Name:" + name + ";SEQ:" + promotionTypeDBEntity.Seq;
        commonDBEntity.IpAddress = UserSession.Current.UserIP;
        commonDBEntity.UserID = UserSession.Current.UserAccount;
        commonDBEntity.UserName = UserSession.Current.UserDspName;
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        messageContent.InnerHtml = "添加促销方式成功!";
        BindRegChanelListGrid();
    }

    //protected void gridViewCSRegChannelList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string ID = gridViewCSRegChannelList.DataKeys[e.RowIndex].Value.ToString();
    //    _promotionTypeEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _promotionTypeEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _promotionTypeEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _promotionTypeEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
    //    _promotionTypeEntity.PromotiontypeDBEntity = new List<PromotionTypeDBEntity>();
    //    PromotionTypeDBEntity promotionTypeDBEntity = new PromotionTypeDBEntity();

    //    promotionTypeDBEntity.ID = ID;

    //    _promotionTypeEntity.PromotiontypeDBEntity.Add(promotionTypeDBEntity);

    //    int i = PromotionTypeBP.Delete(_promotionTypeEntity);

    //    _commonEntity.LogMessages = _promotionTypeEntity.LogMessages;
    //    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
    //    CommonDBEntity commonDBEntity = new CommonDBEntity();

    //    commonDBEntity.Event_Type = "促销方式-删除";
    //    commonDBEntity.Event_ID = ID;
    //    commonDBEntity.IpAddress = UserSession.Current.UserIP;
    //    commonDBEntity.UserID = UserSession.Current.UserAccount;
    //    commonDBEntity.UserName = UserSession.Current.UserDspName;
    //    commonDBEntity.Event_Content = "促销方式-删除 ID:" + ID;

    //    _commonEntity.CommonDBEntity.Add(commonDBEntity);
    //    CommonBP.InsertEventHistory(_commonEntity);

    //    messageContent.InnerHtml = "添加促销删除成功!";
    //    BindRegChanelListGrid();
    //}

    //获取最大排序号

    public string GetMaxSeq()
    {
        _promotionTypeEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionTypeEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionTypeEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionTypeEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _promotionTypeEntity.PromotiontypeDBEntity = new List<PromotionTypeDBEntity>();
        PromotionTypeDBEntity promotionTypeDBEntity = new PromotionTypeDBEntity();
        int i = PromotionTypeBP.GetMaxSeq(_promotionTypeEntity);
        return i.ToString();
    }
}