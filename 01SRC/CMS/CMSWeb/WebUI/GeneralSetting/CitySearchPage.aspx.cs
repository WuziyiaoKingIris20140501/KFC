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

using HotelVp.Common.DataAccess;
using System.Web.Script.Serialization;


public partial class CitySearchPage : BasePage
{
    DataTable FCName = new DataTable();
    CityEntity _cityEntity = new CityEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindCityDDL();
            //BindCityListGrid();

            chkUnTime.Checked = true;
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetchkRegistUnTime();", true);
        }
        messageContent.InnerHtml = "";
    }

    public DataSet ddlOnlinebind()
    {
        DataSet dsResult = CommonBP.GetConfigList(GetLocalResourceObject("OnlineType").ToString());
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("ONLINESTATUS");
        dtResult.Columns.Add("ONLINEDIS");
        if (dsResult.Tables.Count > 0)
        {
            dsResult.Tables[0].Columns["Key"].ColumnName = "ONLINESTATUS";
            dsResult.Tables[0].Columns["Value"].ColumnName = "ONLINEDIS";
        }
        return dsResult;
    }


    private void BindCityDDL()
    {
        _cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cityEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cityEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        DataSet dsResult = CityBP.CommonSelect(_cityEntity).QueryResult;
        ddpCityList.DataTextField = "cityNM";
        ddpCityList.DataValueField = "cityid";
        ddpCityList.DataSource = dsResult;
        ddpCityList.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAddCity();
        BindCityListGrid();
    }

    //清除控件中的数据
    private void clearValue()
    {
    }

    //发放渠道
    private void BindCityListGrid()
    {
        _cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cityEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cityEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _cityEntity.CityDBEntity = new List<CityDBEntity>();
        CityDBEntity cityDBEntity = new CityDBEntity();

        cityDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
        cityDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
        cityDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
        cityDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

        //cityDBEntity.Name_CN = txtSelChannelName.Value;

        ////if (chkAll.Checked)
        ////{
        ////    cityDBEntity.OnlineStatus = null;
        ////}
        ////else if (chkOnL.Checked && chkOff.Checked)
        ////{
        ////    cityDBEntity.OnlineStatus = null;
        ////}
        ////else if (chkOff.Checked)
        ////{
        ////    cityDBEntity.OnlineStatus = "0";
        ////}
        ////else if (chkOnL.Checked)
        ////{
        ////    cityDBEntity.OnlineStatus = "1";
        ////}
        ////else
        ////{
        ////    cityDBEntity.OnlineStatus = null;
        ////}

        //if (rdbAll.Checked)
        //{
        //    cityDBEntity.OnlineStatus = null;
        //}
        //else if (rdbOnL.Checked)
        //{
        //    cityDBEntity.OnlineStatus = "1";
        //}
        //else if (rdbOff.Checked)
        //{
        //    cityDBEntity.OnlineStatus = "0";
        //}
        //else
        //{
        //    cityDBEntity.OnlineStatus = null;
        //}

        //if (chkUnTime.Checked)
        //{
        //    cityDBEntity.StartDTime = null;
        //    cityDBEntity.EndDTime = null;
        //}
        //else
        //{
        //    cityDBEntity.StartDTime = dpStart.Value;
        //    cityDBEntity.EndDTime = dpEnd.Value;
        //}

        _cityEntity.CityDBEntity.Add(cityDBEntity);

        DataSet dsResult = CityBP.Select(_cityEntity).QueryResult;

        gridViewCSCityList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSCityList.DataKeyNames = new string[] { "ID" };//主键
        gridViewCSCityList.DataBind();

        if (!String.IsNullOrEmpty(refushFlag.Value))
        {
            messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
            refushFlag.Value = "";
        }

        //DropDownList ddl;
        //for (int i = 0; i <= gridViewCSCityList.Rows.Count - 1; i++)
        //{
        //    DataRowView drvtemp = dsResult.Tables[0].DefaultView[i];
        //    ddl = (DropDownList)gridViewCSCityList.Rows[i].FindControl("ddlOnline");
        //    ddl.SelectedValue = drvtemp["ONLINESTATUS"].ToString();
        //}
        string strRtn = string.Empty;
        //城市类型  一共20位，第一位代表LM 第二位代表hubs1 第三位代表艺龙 第四位代表携程 
        for (int i = 0; i <= gridViewCSCityList.Rows.Count - 1; i++)
        {
            string cityTypeText = "";
            Label lblCityTypes = (Label)gridViewCSCityList.Rows[i].FindControl("lblCityTypes");
            if (lblCityTypes.Text.Substring(0, 1) == "1") { cityTypeText += "LM" + ","; }
            if (lblCityTypes.Text.Substring(1, 1) == "1") { cityTypeText += "hubs1" + ","; }
            if (lblCityTypes.Text.Substring(2, 1) == "1") { cityTypeText += "艺龙" + ","; }
            if (lblCityTypes.Text.Substring(3, 1) == "1") { cityTypeText += "携程" + ","; }
            if (cityTypeText != "" && cityTypeText.Length > 0)
            {
                lblCityTypes.Text = cityTypeText.TrimEnd(',');
            }
            else
            {
                lblCityTypes.Text = "";
            }

            Label lblMackData = (Label)gridViewCSCityList.Rows[i].FindControl("lblMackData");
            switch (lblMackData.Text)
            {
                case "111100000011111111111111":
                    strRtn = "10点";
                    break;
                case "111100000000111111111111":
                    strRtn = "12点";
                    break;
                case "111100000000001111111111":
                    strRtn = "14点";
                    break;
                case "111100000000000011111111":
                    strRtn = "16点";
                    break;
                case "111100000000000000111111":
                    strRtn = "18点";
                    break;
                default:
                    strRtn = "未知状态";
                    break;
            }
            lblMackData.Text = strRtn;
        }
    }

    protected void gridViewCSCityList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSCityList.Rows.Count; i++)
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
        this.Button2.Visible = true;
        ViewState["Name_CN"] = txtSelChannelName.Value;

        if (rdbAll.Checked)
        {
            ViewState["OnlineStatus"] = "";
        }
        else if (rdbOnL.Checked)
        {
            ViewState["OnlineStatus"] = "1";
        }
        else if (rdbOff.Checked)
        {
            ViewState["OnlineStatus"] = "0";
        }
        else
        {
            ViewState["OnlineStatus"] = "";
        }

        if (chkUnTime.Checked)
        {
            ViewState["StartDTime"] = "";
            ViewState["EndDTime"] = "";
        }
        else
        {
            ViewState["StartDTime"] = dpStart.Value;
            ViewState["EndDTime"] = dpEnd.Value;
        }

        //ViewState["StartDTime"] = dpStart.Value;
        //ViewState["EndDTime"] = dpEnd.Value;

        gridViewCSCityList.EditIndex = -1;
        BindCityListGrid();
    }

    //protected void gridViewCSCityList_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gridViewCSCityList.EditIndex = e.NewEditIndex;
    //    BindCityListGrid();
    //    ((DropDownList)gridViewCSCityList.Rows[e.NewEditIndex].Cells[7].FindControl("ddlOnline")).Enabled = true;
    //}

    //protected void gridViewCSCityList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    string channelNo = gridViewCSCityList.DataKeys[e.RowIndex].Value.ToString();
    //    string channelID = gridViewCSCityList.Rows[e.RowIndex].Cells[3].Text;
    //    string nameCN = gridViewCSCityList.Rows[e.RowIndex].Cells[2].Text;
    //    string onlineStatus = ((DropDownList)gridViewCSCityList.Rows[e.RowIndex].Cells[7].FindControl("ddlOnline")).SelectedValue;

    //    btnUpdateCity(channelNo, channelID, nameCN, onlineStatus);
    //    gridViewCSCityList.EditIndex = -1;
    //    BindCityListGrid();
    //}

    //protected void gridViewCSCityList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    gridViewCSCityList.EditIndex = -1;
    //    BindCityListGrid();
    //}

    protected void gridViewCSCityList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSCityList.PageIndex = e.NewPageIndex;
        BindCityListGrid();
    } 
    
    public void btnAddCity()
    {
        bool flag = false;
        messageContent.InnerHtml = "";
        for (int i = 0; i < gridView1.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gridView1.Rows[i].Cells[0].FindControl("CheckBox1");
            if (cb.Checked)
            {
                flag = true;
                _cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                _cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                _cityEntity.LogMessages.Username = UserSession.Current.UserDspName;
                _cityEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
                _cityEntity.CityDBEntity = new List<CityDBEntity>();
                CityDBEntity cityDBEntity = new CityDBEntity();
                //cityDBEntity.CityID = ddpCityList.SelectedValue;
                cityDBEntity.CityID = gridView1.Rows[i].Cells[1].Text;
                _cityEntity.CityDBEntity.Add(cityDBEntity);
                int iResult = CityBP.Insert(_cityEntity);

                _commonEntity.LogMessages = _cityEntity.LogMessages;
                _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
                CommonDBEntity commonDBEntity = new CommonDBEntity();

                commonDBEntity.Event_Type = "城市管理-添加";
                commonDBEntity.Event_ID = ddpCityList.SelectedValue;

                string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
                conTent = string.Format(conTent, ddpCityList.SelectedValue, ddpCityList.SelectedItem);
                commonDBEntity.Event_Content = conTent;

                if (iResult == 1)
                {
                    commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
                    messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
                }
                else if (iResult == 2)
                {
                    commonDBEntity.Event_Result = GetLocalResourceObject("Error1").ToString();
                    messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
                }
                else if (iResult == 3)
                {
                    commonDBEntity.Event_Result = GetLocalResourceObject("Error3").ToString();
                    messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString();
                }
                else
                {
                    commonDBEntity.Event_Result = GetLocalResourceObject("Error2").ToString();
                    messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
                }

                _commonEntity.CommonDBEntity.Add(commonDBEntity);
                CommonBP.InsertEventHistory(_commonEntity);
            }
        }
        if (!flag) {
            messageContent.InnerHtml = "请选择需要添加到LM的城市名!";
        }
    }

    //public void btnUpdateCity(string cityNo, string cityID, string nameCN, string onlineStatus)
    //{
    //    messageContent.InnerHtml = "";

    //    _cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _cityEntity.LogMessages.Username = UserSession.Current.UserDspName;

    //    _cityEntity.CityDBEntity = new List<CityDBEntity>();
    //    CityDBEntity cityDBEntity = new CityDBEntity();
    //    cityDBEntity.CityNo = cityNo;
    //    cityDBEntity.CityID = cityID;
    //    cityDBEntity.Name_CN = nameCN;
    //    cityDBEntity.OnlineStatus = onlineStatus;
    //    _cityEntity.CityDBEntity.Add(cityDBEntity);
    //    int iResult = CityBP.Update(_cityEntity);

    //    _commonEntity.LogMessages = _cityEntity.LogMessages;
    //    _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
    //    CommonDBEntity commonDBEntity = new CommonDBEntity();

    //    commonDBEntity.Event_Type = "";
    //    commonDBEntity.Event_ID = "";

    //    string conTent = GetLocalResourceObject("EventUpdateMessage").ToString();
    //    conTent = string.Format(conTent, cityDBEntity.CityID, cityDBEntity.Name_CN, cityDBEntity.OnlineStatus);
    //    commonDBEntity.Event_Content = conTent;

    //    if (iResult == 1)
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("UpdateSuccess").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("UpdateSuccess").ToString();
    //    }
    //    else
    //    {
    //        commonDBEntity.Event_Result = GetLocalResourceObject("UpdateError").ToString();
    //        messageContent.InnerHtml = GetLocalResourceObject("UpdateError").ToString();
    //    }

    //    _commonEntity.CommonDBEntity.Add(commonDBEntity);
    //    CommonBP.InsertEventHistory(_commonEntity);
    //}

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    //((DropDownList)gridViewCSCityList.Rows[2].Cells[0].FindControl("ddlOnline")).SelectedValue

    //    messageContent.InnerHtml = "";
    //    _cityEntity.CityDBEntity = new List<CityDBEntity>();
    //    CityDBEntity cityDBEntity = new CityDBEntity();
         

    //    _cityEntity.CityDBEntity.Add(cityDBEntity);

    //    //int iResult = ChannelBP.Insert(channelEntity);

    //    //if (iResult == 1)
    //    //{
    //    //    messageContent.InnerHtml = "渠道保存成功！";
    //    //    return;
    //    //}

    //    //messageContent.InnerHtml = "渠道添加失败！";
    //}


    protected void Button1_Click(object sender, EventArgs e)
    {
        string cityName = this.txtCityName.Text;
        _cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _cityEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _cityEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _cityEntity.CityDBEntity = new List<CityDBEntity>();
        CityDBEntity cityDBEntity = new CityDBEntity();
        cityDBEntity.Name_CN = cityName;
        cityDBEntity.Name_EN = cityName;
        cityDBEntity.PinyinS = cityName;
        _cityEntity.CityDBEntity.Add(cityDBEntity);
        
        DataSet dsResult = CityBP.CommonSelectFogToCity(_cityEntity).QueryResult;
        gridView1.DataSource = dsResult.Tables[0].DefaultView;
        gridView1.DataKeyNames = new string[] { "cityid" };//主键
        gridView1.DataBind();
    }

    protected void gridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridView1.Rows.Count; i++)
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

    protected void gridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridView1.PageIndex = e.NewPageIndex;
        Button1_Click(null, null);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox tbSeq;
        for (int i = 0; i < gridViewCSCityList.Rows.Count; i++)
        {
            tbSeq = (TextBox)gridViewCSCityList.Rows[i].FindControl("txtSeqRead");
            tbSeq.Enabled = true;
        }

        this.divSaveAdjust.Style.Add("display", "block");
        this.divAdjust.Style.Add("display", "none");
    }

    //进行保存
    protected void btnSaveAdjust_Click(object sender, EventArgs e)
    {
        TextBox tbSeq;
        Label lbID;
        List<string> list = new List<string>();
        string sql = string.Empty;
        List<string> listSeq = new List<string>();
        int updateFlag = 0;
        for (int i = 0; i < gridViewCSCityList.Rows.Count; i++)
        {
            lbID = (Label)gridViewCSCityList.Rows[i].FindControl("lblID");
            string id = lbID.Text;
            tbSeq = (TextBox)gridViewCSCityList.Rows[i].FindControl("txtSeqRead");
            string seq = tbSeq.Text;
            try
            {
                int intSeq = int.Parse(seq);
                if (intSeq < 0)
                {
                    //Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('排序必须输入大于0的非负整数！');", true);
                    ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "alert('排序必须输入大于0的非负整数！')", true);  
                    updateFlag = 0;
                    return;

                }
                else
                {
                    int index = listSeq.IndexOf(seq);
                    if (index >= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "alert('排序有重复的数字，请重新输入数字！')", true);  
                        updateFlag = 0;
                        return;
                    }
                    else
                    {
                        listSeq.Add(seq);
                        sql = "update t_lm_b_city set SEQ =" + seq + " where id=" + id;
                        list.Add(sql);
                        updateFlag = 1;
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "alert('排序必须输入大于0的非负整数！')", true); 
                //Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('排序必须输入大于0的非负整数！');", true);
                updateFlag = 0;
                return;
            }
        }

        try
        {
            if (updateFlag == 1)
            {
                DbHelperOra.ExecuteSqlTran(list);

                //Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('修改成功！');", true);
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "alert('修改成功!')", true); 
                this.divSaveAdjust.Style.Add("display", "none");
                this.divAdjust.Style.Add("display", "block");
                BindCityListGrid();
            }

        }
        catch
        {
            //Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('修改失败！');", true);
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "updateScript", "alert('修改失败!')", true); 
        }

    }

    //取消
    protected void btnCancelAdjust_Click(object sender, EventArgs e)
    {
        this.divSaveAdjust.Style.Add("display", "none");
        this.divAdjust.Style.Add("display", "block");
        BindCityListGrid();
    }
}