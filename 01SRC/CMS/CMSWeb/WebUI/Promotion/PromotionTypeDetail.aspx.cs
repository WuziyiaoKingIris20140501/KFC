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


public partial class PromotionTypeDetail : BasePage
{
    PromotionTypeEntity _promotionTypeEntity = new PromotionTypeEntity();
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
            ViewState["id"] = id;
            string name = Request.QueryString["name"] == null ? "" : Request.QueryString["name"].ToString();
            string seq = Request.QueryString["seq"] == null ? "" : Request.QueryString["seq"].ToString();
            BindData(name,seq);
        }
    }

    public void BindData(string name,string seq)
    {
        this.txtPromotionTypeName.Value = name;
        this.txtPromotionTypeSEQ.Value = seq;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        detailMessageContent.InnerHtml = "";

        _promotionTypeEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionTypeEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionTypeEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionTypeEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _promotionTypeEntity.PromotiontypeDBEntity = new List<PromotionTypeDBEntity>();
        PromotionTypeDBEntity promotionTypeDBEntity = new PromotionTypeDBEntity();
        promotionTypeDBEntity.ID = ViewState["id"].ToString();
        promotionTypeDBEntity.Name = this.txtPromotionTypeName.Value;
        promotionTypeDBEntity.Seq = this.txtPromotionTypeSEQ.Value;

        _promotionTypeEntity.PromotiontypeDBEntity.Add(promotionTypeDBEntity);
        int iResult = PromotionTypeBP.Update(_promotionTypeEntity);

        _commonEntity.LogMessages = _promotionTypeEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "促销方式-修改";
        commonDBEntity.Event_ID = ViewState["id"].ToString();

        commonDBEntity.Event_Content = "促销方式-修改 ID:" + ViewState["id"].ToString() +";Name:" + this.txtPromotionTypeName.Value + ";SEQ:" + this.txtPromotionTypeSEQ.Value;
        commonDBEntity.IpAddress = UserSession.Current.UserIP;
        commonDBEntity.UserID = UserSession.Current.UserAccount;
        commonDBEntity.UserName = UserSession.Current.UserDspName;

        if (iResult == 1)
        {
            Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
        }
        else
        {
            detailMessageContent.InnerHtml = "修改失败！";
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }
}