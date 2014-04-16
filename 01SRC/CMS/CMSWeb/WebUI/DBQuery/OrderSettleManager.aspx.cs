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
using System.IO;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

public partial class OrderSettleManager : BasePage
{
    APPContentEntity _appcontentEntity = new APPContentEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SalesUser"] = "";
            ViewState["HotelID"] = "";
            //BindDataListGrid();

            chkMonth3.Checked = true;
            chkMonth4.Checked = true;
            chkMonth5.Checked = true;
            chkMonth6.Checked = true;
            chkMonth7.Checked = true;
            chkMonth8.Checked = true;
            chkMonth9.Checked = true;
            chkMonth10.Checked = true;
            chkMonth11.Checked = true;
            chkMonth12.Checked = true;
        }
        else
        {
            messageContent.InnerHtml = "";
        }
    }

    private void BindDataListGrid()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.UserCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SalesUser"].ToString())) ? null : ViewState["SalesUser"].ToString();
        appcontentDBEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        appcontentDBEntity.Months = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Months"].ToString())) ? null : ViewState["Months"].ToString();

        _appcontentEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _appcontentEntity.PageSize = gridViewCSAPPContenList.PageSize;

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        _appcontentEntity = APPContentBP.OrderSettleMangeListSelect(_appcontentEntity);

        DataSet dsResult = _appcontentEntity.QueryResult;
        gridViewCSAPPContenList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSAPPContenList.DataKeyNames = new string[] { "HOTELID", "MONTHS" };//主键
        gridViewCSAPPContenList.DataBind();

        AspNetPager1.PageSize = gridViewCSAPPContenList.PageSize;
        AspNetPager1.RecordCount = GetCommonInfoCount();// _appcontentEntity.TotalCount;

        string months = string.Empty;
        //执行循环，保证每条数据都可以更新
        for (int i = 0; i < gridViewCSAPPContenList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridViewCSAPPContenList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk3 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom3"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk4 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom4"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk5 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom5"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk6 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom6"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk7 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom7"));


                System.Web.UI.HtmlControls.HtmlInputCheckBox chk8 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom8"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk9 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom9"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk10 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom10"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk11 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom11"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk12 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkCom12"));



                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom3 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom3"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom4 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom4"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom5 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom5"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom6 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom6"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom7 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom7"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom8 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom8"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom9 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom9"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom10 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom10"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom11 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom11"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkCom12 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkCom12"));


                months = gridViewCSAPPContenList.DataKeys[i][1].ToString();
                if (months.Contains("3"))
                {
                    chk3.Checked = true;
                }
                    
                if (appcontentDBEntity.Months.Contains("3"))
                {
                    dvchkCom3.Style.Add("display", "");
                }
                else
                {
                    dvchkCom3.Style.Add("display", "none");
                }

                if (months.Contains("4"))
                {
                    chk4.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("4"))
                {
                    dvchkCom4.Style.Add("display", "");
                }
                else
                {
                    dvchkCom4.Style.Add("display", "none");
                }

                if (months.Contains("5"))
                {
                    chk5.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("5"))
                {
                    dvchkCom5.Style.Add("display", "");
                }
                else
                {
                    dvchkCom5.Style.Add("display", "none");
                }

                if (months.Contains("6"))
                {
                    chk6.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("6"))
                {
                    dvchkCom6.Style.Add("display", "");
                }
                else
                {
                    dvchkCom6.Style.Add("display", "none");
                }

                if (months.Contains("7"))
                {
                    chk7.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("7"))
                {
                    dvchkCom7.Style.Add("display", "");
                }
                else
                {
                    dvchkCom7.Style.Add("display", "none");
                }

                if (months.Contains("8"))
                {
                    chk8.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("8"))
                {
                    dvchkCom8.Style.Add("display", "");
                }
                else
                {
                    dvchkCom8.Style.Add("display", "none");
                }

                if (months.Contains("9"))
                {
                    chk9.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("9"))
                {
                    dvchkCom9.Style.Add("display", "");
                }
                else
                {
                    dvchkCom9.Style.Add("display", "none");
                }

                if (months.Contains("10"))
                {
                    chk10.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("10"))
                {
                    dvchkCom10.Style.Add("display", "");
                }
                else
                {
                    dvchkCom10.Style.Add("display", "none");
                }

                if (months.Contains("11"))
                {
                    chk11.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("11"))
                {
                    dvchkCom11.Style.Add("display", "");
                }
                else
                {
                    dvchkCom11.Style.Add("display", "none");
                }

                if (months.Contains("12"))
                {
                    chk12.Checked = true;
                }

                if (appcontentDBEntity.Months.Contains("12"))
                {
                    dvchkCom12.Style.Add("display", "");
                }
                else
                {
                    dvchkCom12.Style.Add("display", "none");
                }

                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL3 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList3"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL4 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList4"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL5 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList5"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL6 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList6"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL7 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList7"));


                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL8 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList8"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL9 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList9"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL10 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList10"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL11 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList11"));
                System.Web.UI.HtmlControls.HtmlInputCheckBox chkL12 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("chkList12"));


                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL3 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList3"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL4 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList4"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL5 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList5"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL6 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList6"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL7 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList7"));


                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL8 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList8"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL9 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList9"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL10 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList10"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL11 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList11"));
                System.Web.UI.HtmlControls.HtmlGenericControl dvchkL12 = ((System.Web.UI.HtmlControls.HtmlGenericControl)gridViewCSAPPContenList.Rows[i].Cells[5].FindControl("dvchkList12"));

                if (appcontentDBEntity.Months.Contains("3"))
                {
                    dvchkL3.Style.Add("display","");
                    chkL3.Checked = true;
                }
                else
                {
                    dvchkL3.Style.Add("display", "none");
                    chkL3.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("4"))
                {
                    dvchkL4.Style.Add("display", "");
                    chkL4.Checked = true;
                }
                else
                {
                    dvchkL4.Style.Add("display", "none");
                    chkL4.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("5"))
                {
                    dvchkL5.Style.Add("display", "");
                    chkL5.Checked = true;
                }
                else
                {
                    dvchkL5.Style.Add("display", "none");
                    chkL5.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("6"))
                {
                    dvchkL6.Style.Add("display", "");
                    chkL6.Checked = true;
                }
                else
                {
                    dvchkL6.Style.Add("display", "none");
                    chkL6.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("7"))
                {
                    dvchkL7.Style.Add("display", "");
                    chkL7.Checked = true;
                }
                else
                {
                    dvchkL7.Style.Add("display", "none");
                    chkL7.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("8"))
                {
                    dvchkL8.Style.Add("display", "");
                    chkL8.Checked = true;
                }
                else
                {
                    dvchkL8.Style.Add("display", "none");
                    chkL8.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("9"))
                {
                    dvchkL9.Style.Add("display", "");
                    chkL9.Checked = true;
                }
                else
                {
                    dvchkL9.Style.Add("display", "none");
                    chkL9.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("10"))
                {
                    dvchkL10.Style.Add("display", "");
                    chkL10.Checked = true;
                }
                else
                {
                    dvchkL10.Style.Add("display", "none");
                    chkL10.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("11"))
                {
                    dvchkL11.Style.Add("display", "");
                    chkL11.Checked = true;
                }
                else
                {
                    dvchkL11.Style.Add("display", "none");
                    chkL11.Checked = false;
                }

                if (appcontentDBEntity.Months.Contains("12"))
                {
                    dvchkL12.Style.Add("display", "");
                    chkL12.Checked = true;
                }
                else
                {
                    dvchkL12.Style.Add("display", "none");
                    chkL12.Checked = false;
                }
            }
        }

        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetWebAutoControl()", true);
    }

    private int GetCommonInfoCount()
    {
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.UserCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["SalesUser"].ToString())) ? null : ViewState["SalesUser"].ToString();
        appcontentDBEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
        appcontentDBEntity.Months = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Months"].ToString())) ? null : ViewState["Months"].ToString();

        _appcontentEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _appcontentEntity.PageSize = gridViewCSAPPContenList.PageSize;

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = APPContentBP.OrderSettleMangeListCount(_appcontentEntity).QueryResult;
        return dsResult.Tables[0].Rows.Count;
    }

    protected void gridViewCSAPPContenList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();
        
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindDataListGrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strHotel = hidHotelID.Value;
        string strSales = hidSalesID.Value;
        string months = string.Empty;
        if (chkMonth3.Checked)
        {
            months = "3,";
        }

        if (chkMonth4.Checked)
        {
            months = months + "4,";
        }

        if (chkMonth5.Checked)
        {
            months = months + "5,";
        }

        if (chkMonth6.Checked)
        {
            months = months + "6,";
        }

        if (chkMonth7.Checked)
        {
            months = months + "7,";
        }

        if (chkMonth8.Checked)
        {
            months = months + "8,";
        }

        if (chkMonth9.Checked)
        {
            months = months + "9,";
        }

        if (chkMonth10.Checked)
        {
            months = months + "10,";
        }

        if (chkMonth11.Checked)
        {
            months = months + "11,";
        }

        if (chkMonth12.Checked)
        {
            months = months + "12,";
        }

        months = months.TrimEnd(',');

        if (String.IsNullOrEmpty(months))
        {
            messageContent.InnerText = "请选择月份";
            return;
        }

        if (!String.IsNullOrEmpty(strHotel) && (!strHotel.Contains("[") || !strHotel.Contains("]")))
        {
            messageContent.InnerText = "酒店不正确，请选择";
            return;
        }

        if (!String.IsNullOrEmpty(strSales) && (!strSales.Contains("[") || !strSales.Contains("]")))
        {
            messageContent.InnerText = "销售不正确，请选择";
            return;
        }

        strHotel = (strHotel.Length > 0) ? strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1)) : "";
        strSales = (strSales.Length > 0) ? strSales.Substring((strSales.IndexOf('[') + 1), (strSales.IndexOf(']') - 1)) : "";

        ViewState["SalesUser"] = strSales;
        ViewState["HotelID"] = strHotel;
        ViewState["Months"] = months;
        messageContent.InnerHtml = "";
        //gridViewCSAPPContenList.PageIndex = 0;
        AspNetPager1.CurrentPageIndex = 1;
        BindDataListGrid();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int EditIndex = -1;
        //执行循环，保证每条数据都可以更新
        for (int i = 0; i < gridViewCSAPPContenList.Rows.Count; i++)
        {
            if (hidSelHotelID.Value.Equals(gridViewCSAPPContenList.Rows[i].Cells[0].Text.ToString()))
            {
                EditIndex = i;
            }
        }

        string months = string.Empty;
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk3 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList3"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk4 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList4"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk5 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList5"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk6 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList6"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk7 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList7"));


        System.Web.UI.HtmlControls.HtmlInputCheckBox chk8 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList8"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk9 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList9"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk10 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList10"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk11 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList11"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk12 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkList12"));


        if (chk3.Checked)
        {
            months = months + "3,";
        }

        if (chk4.Checked)
        {
            months = months + "4,";
        }

        if (chk5.Checked)
        {
            months = months + "5,";
        }

        if (chk6.Checked)
        {
            months = months + "6,";
        }

        if (chk7.Checked)
        {
            months = months + "7,";
        }

        if (chk8.Checked)
        {
            months = months + "8,";
        }

        if (chk9.Checked)
        {
            months = months + "9,";
        }

        if (chk10.Checked)
        {
            months = months + "10,";
        }

        if (chk11.Checked)
        {
            months = months + "11,";
        }

        if (chk12.Checked)
        {
            months = months + "12,";
        }

        months = months.TrimEnd(',');
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = hidSelHotelID.Value;
        appcontentDBEntity.Months = months;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
         DataSet dsResult = APPContentBP.ExportOrderSettleMangeList(_appcontentEntity).QueryResult;
         //CommonFunction.ExportExcelForLM(dsResult);

        if (dsResult.Tables[0].Rows.Count == 0)
        {
            messageContent.InnerText = "酒店ID：" + hidSelHotelID.Value + "  " + months + "月无订单记录，请从新选择导出月份！";
            return;
        }

        string path = this.Server.MapPath("~\\SettleExecl\\") + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
        if (!Directory.Exists(this.Server.MapPath("~\\SettleExecl\\")))
        {
            Directory.CreateDirectory(this.Server.MapPath("~\\SettleExecl\\"));
        }

        ExportDatasetToExcel(dsResult, path, months);
        string Redirectpath = "~\\SettleExecl\\" + path.Substring(path.LastIndexOf("\\") + 1);
        Response.Redirect(Redirectpath);
        File.Delete(path);
    }

    /// <summary>  
    /// 将DataSet数据集转换HSSFworkbook对象，并保存为Stream流  
    /// </summary>  
    /// <param name="ds"></param> 
    /// <returns>返回数据流Stream对象</returns>
    public static MemoryStream ExportDatasetToExcel(DataSet ds, string fileName, string months)
    {
        #region
        //Hashtable ht = new Hashtable();
        //ht.Add("orderNum", "订单号码");
        //ht.Add("createTime", "创建日期");
        //ht.Add("cityId", "订单城市");
        //ht.Add("hotelName", "酒店名称");
        //ht.Add("guestName", "入住人名称");
        //ht.Add("inDate", "入住日期");
        //ht.Add("outDate", "离店日期");
        //ht.Add("bookTotalPrice", "房间单价");
        //ht.Add("rooms", "间数");
        //ht.Add("bookPrice", "总价");
        //ht.Add("bookStatusOther", "订单状态");
        //ht.Add("commission", "佣金");
        //ht.Add("orderChannel", "渠道");
        #endregion

        try
        {
            //文件流对象  
            MemoryStream stream = new MemoryStream();
            //打开Excel对象  
            HSSFWorkbook workbook = new HSSFWorkbook();


            //Excel的Sheet对象  
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");

            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(1, 10 * 256);
            sheet.SetColumnWidth(2, 12 * 256);
            sheet.SetColumnWidth(3, 12 * 256);
            sheet.SetColumnWidth(4, 12 * 256);
            sheet.SetColumnWidth(5, 5 * 256);
            sheet.SetColumnWidth(6, 9 * 256);
            sheet.SetColumnWidth(7, 9 * 256);
            sheet.SetColumnWidth(8, 9 * 256);
            sheet.SetColumnWidth(9, 8 * 256);


            sheet.SetMargin(MarginType.RightMargin, (double)0.1);
            sheet.SetMargin(MarginType.TopMargin, (double)0.1);
            sheet.SetMargin(MarginType.LeftMargin, (double)0.1);
            sheet.SetMargin(MarginType.BottomMargin, (double)0.1);
            sheet.PrintSetup.PaperSize = 9;
            sheet.PrintSetup.FitWidth = 800;
            sheet.PrintSetup.FitHeight = 1000;

            //set date format  
            //CellStyle cellStyleDate = workbook.CreateCellStyle();
            ICellStyle cellStyleDate = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
            //使用NPOI操作Excel表  

            //合并消息头 
            NPOI.SS.UserModel.IRow rowHeader = sheet.CreateRow(0);
            NPOI.SS.UserModel.ICell cellHeader = rowHeader.CreateCell(0);
            cellHeader.SetCellValue("今夜酒店特价 HotelVP.com 佣金结算对账单");
            cellStyleDate.Alignment = HorizontalAlignment.CENTER;
            cellStyleDate.VerticalAlignment = VerticalAlignment.CENTER;
            IFont font = workbook.CreateFont();
            font.FontHeight = 20 * 20;
            cellStyleDate.SetFont(font);
            cellHeader.CellStyle = cellStyleDate;
            sheet.AddMergedRegion(new CellRangeAddress(0, 3, 0, 9));


            //添加Excel头信息内容
            //分销渠道：去哪儿
            NPOI.SS.UserModel.ICell cellKey;
            NPOI.SS.UserModel.ICell cellValue;

            IFont font12 = workbook.CreateFont();
            font12.FontHeightInPoints = 10;
            font12.FontName = "微软雅黑";
            //font12.FontHeight = 20 * 20;

            NPOI.SS.UserModel.IRow channelRow = sheet.CreateRow(7);
            channelRow.Height = 20 * 20;
            
            cellKey = channelRow.CreateCell(0);
            cellKey.CellStyle.SetFont(font12);
            //cellKey.CellStyle.FillPattern = FillPatternType.
            cellKey.SetCellValue("To:");
            cellValue = channelRow.CreateCell(1);
            cellValue.CellStyle.SetFont(font12);
            //cellValue.SetCellValue("去哪儿");
            cellValue.SetCellValue(ds.Tables[0].Rows[0]["酒店名称"].ToString());

            cellKey = channelRow.CreateCell(4);
            cellKey.SetCellValue("From:");
            cellValue = channelRow.CreateCell(5);
            //cellValue.SetCellValue("去哪儿");
            cellValue.SetCellValue("结算部");


            //报表日期段：xxxx
            NPOI.SS.UserModel.IRow reportDateRow = sheet.CreateRow(8);
            cellKey = reportDateRow.CreateCell(0);
            cellKey.SetCellValue("Tel:");
            cellValue = reportDateRow.CreateCell(1);
            //cellValue.SetCellValue("2012/12/01--2012/12/13");
            cellValue.SetCellValue(ds.Tables[0].Rows[0]["酒店电话"].ToString());

            cellKey = reportDateRow.CreateCell(4);
            cellKey.SetCellValue("Tel:");
            cellValue = reportDateRow.CreateCell(5);
            //cellValue.SetCellValue("2012/12/01--2012/12/13");
            cellValue.SetCellValue("021-52186026、52186036");

            //报表生成日期：yyyy-mm-dd hh-mm-ss
            NPOI.SS.UserModel.IRow reportCreateDateRow = sheet.CreateRow(9);
            cellKey = reportCreateDateRow.CreateCell(0);
            cellKey.SetCellValue("Fax:");
            cellValue = reportCreateDateRow.CreateCell(1);
            cellValue.SetCellValue(ds.Tables[0].Rows[0]["酒店传真"].ToString());

            cellKey = reportCreateDateRow.CreateCell(4);
            cellKey.SetCellValue("Fax:");
            cellValue = reportCreateDateRow.CreateCell(5);
            cellValue.SetCellValue("021-52186011");

            //结算总价
            NPOI.SS.UserModel.IRow SettlementPriceRow = sheet.CreateRow(10);
            cellKey = SettlementPriceRow.CreateCell(0);
            cellKey.SetCellValue("Id:");
            cellValue = SettlementPriceRow.CreateCell(1);
            //cellValue.SetCellValue("111122233");
            cellValue.SetCellValue(ds.Tables[0].Rows[0]["酒店ID"].ToString());

            cellKey = SettlementPriceRow.CreateCell(4);
            cellKey.SetCellValue("Email:");
            cellValue = SettlementPriceRow.CreateCell(5);
            //cellValue.SetCellValue("111122233");
            cellValue.SetCellValue("billing@hotelvp.com");


            //结算总价
            NPOI.SS.UserModel.IRow remarkRow = sheet.CreateRow(12);
            cellKey = remarkRow.CreateCell(0);
            cellKey.SetCellValue("您好！以下为2013年" + months + "月佣金结算对账单，烦请贵酒店相关负责人员核对以下数据");

            NPOI.SS.UserModel.IRow remark1Row = sheet.CreateRow(13);
            cellKey = remark1Row.CreateCell(0);
            cellKey.SetCellValue("若无误请于收到对账单二日内签字盖章确认回传，若有疑问，请及时告知，非常感谢！");

            //结算总价
            NPOI.SS.UserModel.IRow totalPrice1Row = sheet.CreateRow(16);
            cellKey = totalPrice1Row.CreateCell(0);
            cellKey.SetCellValue("总间夜数:");
            NPOI.SS.UserModel.ICell cellValue1;
            cellValue1 = totalPrice1Row.CreateCell(1);
            //cellValue1.CellStyle.Alignment = HorizontalAlignment.CENTER;
            cellValue1.SetCellValue(ds.Tables[0].Compute("SUM(间夜)", "").ToString());
            cellValue = totalPrice1Row.CreateCell(2);
            cellValue.SetCellValue("间夜");

            cellValue = totalPrice1Row.CreateCell(4);
            cellValue.SetCellValue("签字确认:");
            cellValue = totalPrice1Row.CreateCell(5);
            cellValue.SetCellValue("");

            NPOI.SS.UserModel.IRow totalPrice2Row = sheet.CreateRow(17);
            cellKey = totalPrice2Row.CreateCell(0);
            cellKey.SetCellValue("佣金合计:");
            
            NPOI.SS.UserModel.ICell cellValue2;
            cellValue2 = totalPrice2Row.CreateCell(1);
            //cellValue2.CellStyle.Alignment = HorizontalAlignment.CENTER;
            cellValue2.SetCellValue(ds.Tables[0].Compute("SUM(佣金)", "").ToString());
            cellValue = totalPrice2Row.CreateCell(2);
            cellValue.SetCellValue("人民币（元）");

            cellValue = totalPrice2Row.CreateCell(4);
            cellValue.SetCellValue("发票抬头:");
            cellValue = totalPrice2Row.CreateCell(5);
            cellValue.SetCellValue("");

            NPOI.SS.UserModel.IRow totalPrice3Row = sheet.CreateRow(18);
            cellKey = totalPrice3Row.CreateCell(0);
            
            cellKey.SetCellValue("前期未付佣金:");
            cellValue = totalPrice3Row.CreateCell(1);
            cellValue.SetCellValue("");
            cellValue = totalPrice3Row.CreateCell(2);
            cellValue.SetCellValue("人民币（元）");


            NPOI.SS.UserModel.IRow row = sheet.CreateRow(21);
            int count = 0;
            for (int i = 4; i < ds.Tables[0].Columns.Count; i++) //生成sheet第一行列名 
            {
                NPOI.SS.UserModel.ICell cell = row.CreateCell(count++);
                cell.SetCellValue(ds.Tables[0].Columns[i].ColumnName.ToString());
            }
            //将数据导入到excel表中  
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 22);
                count = 0;
                for (int j = 4; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (ds.Tables[0].Columns[j].ColumnName != null)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = ds.Tables[0].Rows[i][j].GetType();

                        if (type == typeof(int) || type == typeof(Int16)
                            || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)ds.Tables[0].Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)ds.Tables[0].Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)ds.Tables[0].Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)ds.Tables[0].Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(ds.Tables[0].Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }

            NPOI.SS.UserModel.IRow totalInfo1Row = sheet.CreateRow(21 + ds.Tables[0].Rows.Count + 1);
            cellValue = totalInfo1Row.CreateCell(0);
            cellValue.SetCellValue("Total：");
            cellValue = totalInfo1Row.CreateCell(5);
            cellValue.SetCellValue(ds.Tables[0].Compute("SUM(间夜)", "").ToString());
            cellValue = totalInfo1Row.CreateCell(6);
            cellValue.SetCellValue(ds.Tables[0].Compute("SUM(卖价)", "").ToString());
            cellValue = totalInfo1Row.CreateCell(7);
            cellValue.SetCellValue(ds.Tables[0].Compute("SUM(底价)", "").ToString());
            cellValue = totalInfo1Row.CreateCell(8);
            cellValue.SetCellValue(ds.Tables[0].Compute("SUM(佣金)", "").ToString());

            //结算总价
            NPOI.SS.UserModel.IRow companyinfoRow = sheet.CreateRow(21 + ds.Tables[0].Rows.Count + 5);
            cellKey = companyinfoRow.CreateCell(0);
            cellKey.SetCellValue("我司汇款资料");

            NPOI.SS.UserModel.IRow companyinfo1Row = sheet.CreateRow(21 + ds.Tables[0].Rows.Count + 6);
            cellValue = companyinfo1Row.CreateCell(0);
            cellValue.SetCellValue("银行账号：");
            cellValue = companyinfo1Row.CreateCell(1);
            cellValue.SetCellValue("31001515100050024076");

            NPOI.SS.UserModel.IRow companyinfo2Row = sheet.CreateRow(21 + ds.Tables[0].Rows.Count + 7);
            cellValue = companyinfo2Row.CreateCell(0);
            cellValue.SetCellValue("开户银行：");
            cellValue = companyinfo2Row.CreateCell(1);
            cellValue.SetCellValue("中国建设银行股份有限公司上海长宁支行");

            NPOI.SS.UserModel.IRow companyinfo3Row = sheet.CreateRow(21 + ds.Tables[0].Rows.Count + 8);
            cellValue = companyinfo3Row.CreateCell(0);
            cellValue.SetCellValue("公司全称：");
            cellValue = companyinfo3Row.CreateCell(1);
            cellValue.SetCellValue("上海千栈网络信息科技有限公司");


            //保存excel文档 
            sheet.ForceFormulaRecalculation = true;
            using (Stream filstream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(filstream);
            }

            workbook.Dispose();

            return stream;
        }
        catch (Exception ex)
        {
            return new MemoryStream();
        }
    }

    protected void btnTag_Click(object sender, EventArgs e)
    {
        int EditIndex = -1;
        //执行循环，保证每条数据都可以更新
        for (int i = 0; i < gridViewCSAPPContenList.Rows.Count; i++)
        {
            if (hidSelHotelID.Value.Equals(gridViewCSAPPContenList.Rows[i].Cells[0].Text.ToString()))
            {
                EditIndex = i;
                break;
            }
        }

        string months = string.Empty;
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk3 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom3"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk4 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom4"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk5 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom5"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk6 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom6"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk7 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom7"));


        System.Web.UI.HtmlControls.HtmlInputCheckBox chk8 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom8"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk9 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom9"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk10 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom10"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk11 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom11"));
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk12 = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)gridViewCSAPPContenList.Rows[EditIndex].Cells[5].FindControl("chkCom12"));


        if (chk3.Checked)
        {
            months = months + "3,";
        }

        if (chk4.Checked)
        {
            months = months + "4,";
        }

        if (chk5.Checked)
        {
            months = months + "5,";
        }

        if (chk6.Checked)
        {
            months = months + "6,";
        }

        if (chk7.Checked)
        {
            months = months + "7,";
        }

        if (chk8.Checked)
        {
            months = months + "8,";
        }

        if (chk9.Checked)
        {
            months = months + "9,";
        }

        if (chk10.Checked)
        {
            months = months + "10,";
        }

        if (chk11.Checked)
        {
            months = months + "11,";
        }

        if (chk12.Checked)
        {
            months = months + "12,";
        }

        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = hidSelHotelID.Value;
        appcontentDBEntity.Months = months;

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        int iResult = APPContentBP.SaveOrderSettleMangeList(_appcontentEntity).Result;
        messageContent.InnerText = "标记保存成功！";
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle()", true);
    }
}
