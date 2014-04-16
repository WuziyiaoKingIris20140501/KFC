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

public partial class ReviewPromotionInfo : BasePage
{
    PromotionEntity _promotionEntity = new PromotionEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPromotioningList();
            BindPromotionMsgListGrid();

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent()", true);
        }
    }

    private void BindPromotioningList()
    {
        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();
        _promotionEntity.PromotionDBEntity.Add(promotionEntity);

         DataSet dsResult = PromotionBP.PromotioningSelect(_promotionEntity).QueryResult;
         gridViewCSPromotioningList.DataSource = dsResult.Tables[0].DefaultView;
         gridViewCSPromotioningList.DataKeyNames = new string[] { "ID" };//主键
         gridViewCSPromotioningList.DataBind();
    }
        
   
    private void BindPromotionMsgListGrid()
    {
        _promotionEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _promotionEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _promotionEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _promotionEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _promotionEntity.PromotionDBEntity = new List<PromotionDBEntity>();
        PromotionDBEntity promotionEntity = new PromotionDBEntity();

        promotionEntity.Title = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["ProTitle"].ToString())) ? null : ViewState["ProTitle"].ToString();
        promotionEntity.StartBeginDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartBeginDTime"].ToString())) ? null : ViewState["StartBeginDTime"].ToString();
        promotionEntity.StartEndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartEndDTime"].ToString())) ? null : ViewState["StartEndDTime"].ToString();
        promotionEntity.EndBeginDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndBeginDTime"].ToString())) ? null : ViewState["EndBeginDTime"].ToString();
        promotionEntity.EndEndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndEndDTime"].ToString())) ? null : ViewState["EndEndDTime"].ToString();
      
        _promotionEntity.PromotionDBEntity.Add(promotionEntity);
        DataSet dsResult = PromotionBP.PromotionMsgSelect(_promotionEntity).QueryResult;

        gridViewCSPromotionMsgList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSPromotionMsgList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSPromotionMsgList.DataBind();
    }

    protected void gridViewCSPromotioningList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gridViewCSPromotionMsgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["ProTitle"] = txtProTitle.Value.Trim();

        if (chkStartUnTime.Checked)
        {
            ViewState["StartBeginDTime"] = "";
            ViewState["StartEndDTime"] = "";
        }
        else
        {
            ViewState["StartBeginDTime"] = dpStartBeginDate.Value;
            ViewState["StartEndDTime"] = dpStartEndDate.Value;
        }

        if (chkEndUnTime.Checked)
        {
            ViewState["EndBeginDTime"] = "";
            ViewState["EndEndDTime"] = "";
        }
        else
        {
            ViewState["EndBeginDTime"] = dpEndBeginDate.Value;
            ViewState["EndEndDTime"] = dpEndEndDate.Value;
        }

        BindPromotionMsgListGrid();
        BindPromotioningList();
    }

    protected void gridViewCSPromotionMsgList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSPromotionMsgList.PageIndex = e.NewPageIndex;
        BindPromotionMsgListGrid();
    }

    protected void gridViewCSPromotioningList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSPromotioningList.PageIndex = e.NewPageIndex;
        BindPromotioningList();
    } 
}