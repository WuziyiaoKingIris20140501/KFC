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

public partial class DBSearchPage : BasePage
{
    DbSearchEntity _dbSearchEntity = new DbSearchEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gridViewCSDBResultList.Style.Add("display", "none");
            InitTree();
        }
    }

    private void InitTree()
    {
        DataSet dsSearch = GetTreeViewTable();

        if (dsSearch.Tables.Count == 0 || dsSearch.Tables[0].Rows.Count == 0)
        {
            return;
        }
       
        tvTableList.ShowCheckBoxes = TreeNodeTypes.None;
        foreach (DataRow dr in dsSearch.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["Cols"].ToString();
            node.Value = dr["Cols"].ToString();
            node.Expanded = false;
            tvTableList.Nodes.Add(node);
        }



        DataSet dsSqlSearch = GetSqlTreeViewTable();

        if (dsSqlSearch.Tables.Count == 0 || dsSqlSearch.Tables[0].Rows.Count == 0)
        {
            return;
        }

        TreeViewSql.ShowCheckBoxes = TreeNodeTypes.None;
        foreach (DataRow dr in dsSqlSearch.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["Cols"].ToString();
            node.Value = dr["Cols"].ToString();
            node.Expanded = false;
            TreeViewSql.Nodes.Add(node);
        }
    }

    private DataSet GetTreeViewTable()
    {
        DataTable dtResult = new DataTable();
        messageContent.InnerHtml = "";
        _dbSearchEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _dbSearchEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _dbSearchEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _dbSearchEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _dbSearchEntity.DbSearchDBEntity = new List<DbSearchDBEntity>();
        DbSearchDBEntity dbSearchDBEntity = new DbSearchDBEntity();
        _dbSearchEntity.DbSearchDBEntity.Add(dbSearchDBEntity);
        return DbSearchBP.MenuSelect(_dbSearchEntity).QueryResult;
    }

    private DataSet GetSqlTreeViewTable()
    {
        DataTable dtResult = new DataTable();
        messageContent.InnerHtml = "";
        _dbSearchEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _dbSearchEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _dbSearchEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _dbSearchEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _dbSearchEntity.DbSearchDBEntity = new List<DbSearchDBEntity>();
        DbSearchDBEntity dbSearchDBEntity = new DbSearchDBEntity();
        _dbSearchEntity.DbSearchDBEntity.Add(dbSearchDBEntity);
        return DbSearchBP.SqlMenuSelect(_dbSearchEntity).QueryResult;
    }

    public void BindGroupListGrid()
    {
        messageContent.InnerHtml = "";
        _dbSearchEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _dbSearchEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _dbSearchEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _dbSearchEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _dbSearchEntity.DbSearchDBEntity = new List<DbSearchDBEntity>();
        DbSearchDBEntity dbSearchDBEntity = new DbSearchDBEntity();
        dbSearchDBEntity.SqlContent = txtManualAdd.Text.Trim();
        _dbSearchEntity.DbSearchDBEntity.Add(dbSearchDBEntity);

        DataSet dsResult = new DataSet();

        try
        {
            dsResult = DbSearchBP.ItemSelect(_dbSearchEntity).QueryResult;
            gridViewCSDBResultList.DataSource = dsResult.Tables[0].DefaultView;
            //gridViewCSDBSearchList.DataKeyNames = new string[] { "ID" };//主键
            gridViewCSDBResultList.DataBind();
        }
        catch
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        }

      

        //if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
        //}
        //else
        //{
        //    messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
        //}
    }

    //导出Excel文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";
        _dbSearchEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _dbSearchEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _dbSearchEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _dbSearchEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _dbSearchEntity.DbSearchDBEntity = new List<DbSearchDBEntity>();
        DbSearchDBEntity dbSearchDBEntity = new DbSearchDBEntity();
        dbSearchDBEntity.SqlContent = txtManualAdd.Text.Trim();
        _dbSearchEntity.DbSearchDBEntity.Add(dbSearchDBEntity);

        DataSet dsResult = new DataSet();

        try
        {
            dsResult = DbSearchBP.ItemSelect(_dbSearchEntity).QueryResult;
            gridViewCSDBResultList.DataSource = dsResult.Tables[0].DefaultView;
            //gridViewCSDBSearchList.DataKeyNames = new string[] { "ID" };//主键
            gridViewCSDBResultList.DataBind();
        }
        catch
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            return;
        }

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        CommonFunction.ExportExcelForLM(dsResult);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gridViewCSDBSearchList.DataSource = null;
        gridViewCSDBSearchList.DataBind();
        gridViewCSDBResultList.DataSource = null;
        gridViewCSDBSearchList.Style.Add("display","none");
        gridViewCSDBResultList.Style.Add("display", "");
        BindGroupListGrid();
    }

    protected void gridViewCSDBResultList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridViewCSDBResultList.PageIndex = e.NewPageIndex;
        BindGroupListGrid();
    }

    protected void gridViewCSDBSearchList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridViewCSDBSearchList.PageIndex = e.NewPageIndex;
        GetTableColumsList();
    }

    protected void tvTableList_SelectedNodeChanged(object sender, EventArgs e)
    {
        gridViewCSDBResultList.DataSource = null;
        gridViewCSDBResultList.DataBind();
        gridViewCSDBSearchList.DataSource = null;
        gridViewCSDBSearchList.Style.Add("display", "");
        gridViewCSDBResultList.Style.Add("display", "none");
        GetTableColumsList();
        txtManualAdd.Text = String.Format(GetLocalResourceObject("SQLString").ToString(), tvTableList.SelectedValue);
    }

    private void GetTableColumsList()
    {
        messageContent.InnerHtml = "";
        _dbSearchEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _dbSearchEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _dbSearchEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _dbSearchEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _dbSearchEntity.DbSearchDBEntity = new List<DbSearchDBEntity>();
        DbSearchDBEntity dbSearchDBEntity = new DbSearchDBEntity();
        dbSearchDBEntity.TableID = tvTableList.SelectedValue;
        _dbSearchEntity.DbSearchDBEntity.Add(dbSearchDBEntity);

        DataSet dsResult = DbSearchBP.TableColumsSelect(_dbSearchEntity).QueryResult;

        //GridviewControl.GridViewDataBind(gridViewCSDBSearchList, dsResult.Tables[0]);

        gridViewCSDBSearchList.DataSource = dsResult.Tables[0].DefaultView;
        //gridViewCSDBSearchList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSDBSearchList.DataBind();
    }

    protected void rdbtnlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(rdbtnlist.SelectedValue))
        {
            return;
        }

        string rdbtnVal = rdbtnlist.SelectedValue;
        switch (rdbtnVal)
        {
            case "order":
                txtManualAdd.Text = GetLocalResourceObject("orderSQLString").ToString();
                break;
            case "hotel":
                txtManualAdd.Text = GetLocalResourceObject("hotelSQLString").ToString();
                break;
            case "user":
                txtManualAdd.Text = GetLocalResourceObject("userSQLString").ToString();
                break;
            //case "pay":
            //    txtManualAdd.Text = GetLocalResourceObject("paySQLString").ToString();
            //    break;
            default:
                txtManualAdd.Text = "";
                break;
        }

        gridViewCSDBSearchList.DataSource = null;
        gridViewCSDBSearchList.DataBind();
        gridViewCSDBResultList.DataSource = null;
        gridViewCSDBSearchList.Style.Add("display", "none");
        gridViewCSDBResultList.Style.Add("display", "");
        BindGroupListGrid();
    }

    protected void btnSqlSearch_Click(object sender, EventArgs e)
    {
        gridViewSql.DataSource = null;
        gridViewSql.DataBind();
        BindSqlGroupListGrid();
    }

    protected void btnSqlExport_Click(object sender, EventArgs e)
    {
        messageSqlContent.InnerHtml = "";
        _dbSearchEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _dbSearchEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _dbSearchEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _dbSearchEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _dbSearchEntity.DbSearchDBEntity = new List<DbSearchDBEntity>();
        DbSearchDBEntity dbSearchDBEntity = new DbSearchDBEntity();
        dbSearchDBEntity.SqlContent = txtSql.Text.Trim();
        _dbSearchEntity.DbSearchDBEntity.Add(dbSearchDBEntity);

        DataSet dsResult = new DataSet();

        try
        {
            dsResult = DbSearchBP.ItemSqlSelect(_dbSearchEntity).QueryResult;
            gridViewSql.DataSource = dsResult.Tables[0].DefaultView;
            gridViewSql.DataBind();
        }
        catch
        {
            messageSqlContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        }

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            messageSqlContent.InnerHtml = GetLocalResourceObject("WarningMessage").ToString();
            return;
        }

        CommonFunction.ExportExcelForLM(dsResult);
    }

    protected void gridViewSqlDBSearchList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridViewSql.PageIndex = e.NewPageIndex;
        BindSqlGroupListGrid();
    }

    protected void tvSqlTableList_SelectedNodeChanged(object sender, EventArgs e)
    {
        gridViewSql.DataSource = null;
        gridViewSql.DataBind();
        //GetSqlTableColumsList();
        txtSql.Text = String.Format(GetLocalResourceObject("SQLsqlString").ToString(), TreeViewSql.SelectedValue);
    }

    private void GetSqlTableColumsList()
    {
        messageContent.InnerHtml = "";
        _dbSearchEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _dbSearchEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _dbSearchEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _dbSearchEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _dbSearchEntity.DbSearchDBEntity = new List<DbSearchDBEntity>();
        DbSearchDBEntity dbSearchDBEntity = new DbSearchDBEntity();
        dbSearchDBEntity.TableID = TreeViewSql.SelectedValue;
        _dbSearchEntity.DbSearchDBEntity.Add(dbSearchDBEntity);

        //DataSet dsResult = DbSearchBP.SqlTableColumsSelect(_dbSearchEntity).QueryResult;

        //gridViewSql.DataSource = dsResult.Tables[0].DefaultView;
        //gridViewSql.DataBind();
    }

    public void BindSqlGroupListGrid()
    {
        messageSqlContent.InnerHtml = "";
        _dbSearchEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _dbSearchEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _dbSearchEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _dbSearchEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _dbSearchEntity.DbSearchDBEntity = new List<DbSearchDBEntity>();
        DbSearchDBEntity dbSearchDBEntity = new DbSearchDBEntity();
        dbSearchDBEntity.SqlContent = txtSql.Text.Trim();
        _dbSearchEntity.DbSearchDBEntity.Add(dbSearchDBEntity);

        DataSet dsResult = new DataSet();

        try
        {
            dsResult = DbSearchBP.ItemSqlSelect(_dbSearchEntity).QueryResult;
            gridViewSql.DataSource = dsResult.Tables[0].DefaultView;
            gridViewSql.DataBind();
        }
        catch
        {
            messageSqlContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
        }

    }
}