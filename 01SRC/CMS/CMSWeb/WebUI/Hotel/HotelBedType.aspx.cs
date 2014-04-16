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

public partial class HotelBedType : BasePage
{
    HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBedTagList();
            ViewState["KEYWORD"] = "";
            BindBedListGrid();
            messageContent.InnerHtml = "";
        }
    }

    private void BindBedTagList()
    {
        DataSet dsBed = CommonBP.GetConfigList(GetLocalResourceObject("BedType").ToString());
        if (dsBed.Tables.Count > 0)
        {
            dsBed.Tables[0].Columns["Key"].ColumnName = "BedKEY";
            dsBed.Tables[0].Columns["Value"].ColumnName = "BedVAL";

            chkBedTag.DataTextField = "BedVAL";
            chkBedTag.DataValueField = "BedKEY";
            chkBedTag.DataSource = dsBed;
            chkBedTag.DataBind();
        }
    }

    private void BindBedListGrid()
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity HotelInfoDBEntity = new HotelInfoDBEntity();

        HotelInfoDBEntity.KeyWord = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["KEYWORD"].ToString())) ? null : ViewState["KEYWORD"].ToString();
        _hotelinfoEntity.HotelInfoDBEntity.Add(HotelInfoDBEntity);

        DataSet dsResult = HotelInfoBP.BedTypeListSelect(_hotelinfoEntity).QueryResult;

        myGridView.DataSource = dsResult.Tables[0].DefaultView;
        myGridView.DataKeyNames = new string[] { "BEDCD" };//主键
        myGridView.DataBind();
    }

    protected void myGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= myGridView.Rows.Count; i++)
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity HotelInfoDBEntity = new HotelInfoDBEntity();

        if (String.IsNullOrEmpty(txtBedCode.Text.Trim()) || String.IsNullOrEmpty(txtBedNM.Text.Trim()))
        {
            detailMessageContent.InnerHtml = GetLocalResourceObject("InsertError1").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenlist()", true);
        }

        HotelInfoDBEntity.BedCode = txtBedCode.Text.Trim();
        HotelInfoDBEntity.BedName = txtBedNM.Text.Trim();

        string bedtag = "";
        for (int i = 0; i < chkBedTag.Items.Count; i++)
        {
            if (chkBedTag.Items[i].Selected)
            {
                bedtag = bedtag + "1";
            }
            else
            {
                bedtag = bedtag + "0";
            }
        }

        HotelInfoDBEntity.BedTag = bedtag.PadRight(10, '0');
        HotelInfoDBEntity.Type = hidAddType.Value.Trim();

        _hotelinfoEntity.HotelInfoDBEntity.Add(HotelInfoDBEntity);
        int iResult = HotelInfoBP.InsertBedType(_hotelinfoEntity);

        _commonEntity.LogMessages = _hotelinfoEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "床型管理-保存";
        commonDBEntity.Event_ID = HotelInfoDBEntity.BedCode;
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        conTent = string.Format(conTent, HotelInfoDBEntity.BedCode, HotelInfoDBEntity.BedName, HotelInfoDBEntity.BedTag);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            BindBedListGrid();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeCloselist()", true);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertError").ToString();
            detailMessageContent.InnerHtml = GetLocalResourceObject("InsertError").ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenlist()", true);
        }
        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ShowBedCotentVal();
    }

    protected void btnAddBed_Click(object sender, EventArgs e)
    {
        ShowBedCotentVal();
    }

    private void ShowBedCotentVal()
    {
        messageContent.InnerHtml = "";
        detailMessageContent.InnerHtml = "";
        if ("1".Equals(hidAddType.Value.Trim()))
        {
            _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
            _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
            HotelInfoDBEntity HotelInfoDBEntity = new HotelInfoDBEntity();

            HotelInfoDBEntity.KeyWord = hidBedCode.Value.Trim();
            _hotelinfoEntity.HotelInfoDBEntity.Add(HotelInfoDBEntity);
            DataSet dsResult = HotelInfoBP.BedTypeListSelect(_hotelinfoEntity).QueryResult;

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                txtBedCode.Text = dsResult.Tables[0].Rows[0]["BEDCD"].ToString();
                txtBedNM.Text = dsResult.Tables[0].Rows[0]["BEDNM"].ToString();
                string bedTag = dsResult.Tables[0].Rows[0]["bed_tag"].ToString();
                foreach (ListItem lt in chkBedTag.Items)
                {
                    lt.Selected = false;
                }

                for (int i = 0; i < chkBedTag.Items.Count; i++)
                {
                    if (bedTag.Length > 0 && "1".Equals(bedTag.Substring(i, 1)))
                    {
                        chkBedTag.Items[i].Selected = true;
                    }
                }
            }
            txtBedCode.Enabled = false;
        }
        else
        {
            txtBedCode.Enabled = true;
            txtBedCode.Text = "";
            txtBedNM.Text = "";
            foreach (ListItem lt in chkBedTag.Items)
            {
                lt.Selected = false;
            }
        }
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "invokeOpenlist()", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["KEYWORD"] = txtKeyWord.Text.Trim();
        messageContent.InnerHtml = "";
        BindBedListGrid();
    }

    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.myGridView.PageIndex = e.NewPageIndex;
        BindBedListGrid();
    } 
}