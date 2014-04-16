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
using System.Configuration;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Services;

public partial class WebUI_Hotel_HotelConsultingRoomAsyncTable : System.Web.UI.Page
{
    public bool IsErgodic = false;
    public DataRow[] rowLmbar = null;//LMBAR计划
    public DataRow[] rowLmbar2 = null;//LMBAR2计划
    public DataRow[] drRoomListLMBAR = null;//LMBAR房型
    public DataRow[] drRoomListLMBAR2 = null; //LMBAR2房型
    public DataTable dtTime = new DataTable();//计划时间段


    #region
    public static DataTable lastRowLmbar;//上一个LMBAR计划
    public static DataTable nextRowLmbar;//下一个LMBAR计划

    public static DataTable lastRowLmbar2;//上一个LMBAR2计划
    public static DataTable nextRowLmbar2;//下一个LMBAR2计划

    public static DataTable lastDrRoomListLMBAR;//上一个LMBAR房型
    public static DataTable nextDrRoomListLMBAR;//下一个LMBAR房型

    public static DataTable lastDrRoomListLMBAR2; //上一个LMBAR2房型
    public static DataTable nextDrRoomListLMBAR2; //下一个LMBAR2房型
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindBookStatusList();

            if (Request.QueryString["salesID"] != null)
            {
                string SaleID = Request.QueryString["salesID"].ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "setAlertsalesroomKeys", "SetSalesRoom('" + "[" + SaleID + "]" + SaleID + "');", true);
                this.hidSelectSalesID.Value = "[" + SaleID + "]" + SaleID;
                btnSelect_Click(null, null);
            }

            if (DateTime.Now.Hour <= 4 && DateTime.Now.Hour >= 0)
            {
                this.planStartDate.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(6).ToShortDateString().Replace("/", "-");
            }
            else
            {
                this.planStartDate.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(6).ToShortDateString().Replace("/", "-");
            }
        }
        this.gridHotelList.ShowHeader = false;
        if (String.IsNullOrEmpty(this.hidSelectSalesID.Value.Trim()))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "setsalesroomKeys", "SetSalesRoom('" + UserSession.Current.UserAccount.ToLower() + "');", true);
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "setsalesroomKeys", "SetSalesRoom('" + this.hidSelectSalesID.Value.Trim() + "');", true);
        }
    }

    #region  查询操作
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        getHotelLists();
    }

    public void getHotelLists()
    {
        messageContent.InnerHtml = "";

        DataTable dtResult = new DataTable();
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        if (!string.IsNullOrEmpty(this.hidSelectCity.Value) || !string.IsNullOrEmpty(this.hidSelectHotel.Value) || !string.IsNullOrEmpty(this.hidSelectBussiness.Value))
        {
            #region
            if (!string.IsNullOrEmpty(this.hidSelectHotel.Value))
            {
                if (!hidSelectHotel.Value.Trim().Contains("[") || !hidSelectHotel.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择酒店不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(this.hidSelectCity.Value))
            {
                if (!hidSelectCity.Value.Trim().Contains("[") || !hidSelectCity.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择城市不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(this.hidSelectBussiness.Value))
            {
                if (!hidSelectBussiness.Value.Trim().Contains("[") || !hidSelectBussiness.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择商圈不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }
            #endregion
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setsalesroomKeysOne", "SetSalesRoom('')", true);
            hotelInfoDBEntity.City = this.hidSelectCity.Value == "" ? "" : this.hidSelectCity.Value.Substring((this.hidSelectCity.Value.IndexOf('[') + 1), (this.hidSelectCity.Value.IndexOf(']') - 1)); //"";//城市ID 
            hotelInfoDBEntity.HotelID = this.hidSelectHotel.Value == "" ? "" : this.hidSelectHotel.Value.Substring((this.hidSelectHotel.Value.IndexOf('[') + 1), (this.hidSelectHotel.Value.IndexOf(']') - 1));//"";//酒店ID
            hotelInfoDBEntity.Bussiness = this.hidSelectBussiness.Value == "" ? "" : this.hidSelectBussiness.Value.Substring((this.hidSelectBussiness.Value.IndexOf('[') + 1), (this.hidSelectBussiness.Value.IndexOf(']') - 1));//"";//商圈ID
            hotelInfoDBEntity.Type = DropDownList2.SelectedValue;

            hotelInfoDBEntity.EffectDate = this.radioListBookStatus.SelectedValue.Trim();

            hotelInfoDBEntity.BalValue = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["FilterJinJiangHotels"])) ? "" : ConfigurationManager.AppSettings["FilterJinJiangHotels"].ToString().Trim() + ",";
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetConsultRoomHotelRoomListByHotel(_hotelinfoEntity).QueryResult.Tables[0];

            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                dtResult.Rows[i]["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                dtResult.Rows[i]["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                dtResult.Rows[i]["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();
            }
        }
        else
        {
            #region
            if (this.hidSelectSalesID.Value != UserSession.Current.UserAccount.ToLower())
            {
                if (String.IsNullOrEmpty(this.hidSelectSalesID.Value.Trim()))
                {
                    messageContent.InnerHtml = "查询失败，选择用户不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }

                if (!hidSelectSalesID.Value.Trim().Contains("[") || !hidSelectSalesID.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择用户不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }
            #endregion
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setsalesroomKeysOne", "SetSalesRoom('" + this.hidSelectSalesID.Value + "')", true);
            hotelInfoDBEntity.SalesID = this.hidSelectSalesID.Value == UserSession.Current.UserAccount.ToLower() ? UserSession.Current.UserAccount.ToLower() : this.hidSelectSalesID.Value.Substring((this.hidSelectSalesID.Value.IndexOf('[') + 1), (this.hidSelectSalesID.Value.IndexOf(']') - 1));//"";//房控人员
            hotelInfoDBEntity.Type = DropDownList2.SelectedValue;
            hotelInfoDBEntity.EffectDate = this.radioListBookStatus.SelectedValue.Trim();
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetConsultRoomHotelRoomList(_hotelinfoEntity).QueryResult.Tables[0];//得到当天所有 有计划  的酒店 

            #region 过滤所有计划关闭 且  关闭人为销售人员
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                DataTable dtNewResult = new DataTable();
                dtNewResult.Columns.Add("PROP");
                dtNewResult.Columns.Add("CITYID");
                dtNewResult.Columns.Add("LINKMAN");
                dtNewResult.Columns.Add("LINKTEL");
                dtNewResult.Columns.Add("LINKEMAIL");
                dtNewResult.Columns.Add("SALES_ACCOUNT");
                dtNewResult.Columns.Add("PROP_NAME_ZH");
                dtNewResult.Columns.Add("isplan");
                dtNewResult.Columns.Add("ordercount");
                dtNewResult.Columns.Add("EXLinkMan");
                dtNewResult.Columns.Add("EXLinkTel");
                dtNewResult.Columns.Add("EXRemark");
                dtNewResult.Columns.Add("BackPropName");

                DataTable dtSales = GetSalesManagerList();//所有的销售人员

                string FilterJJHotels = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["FilterJinJiangHotels"])) ? "" : ConfigurationManager.AppSettings["FilterJinJiangHotels"].ToString().Trim();
                string hotelId = "";
                bool IsFlag = false;

                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    IsFlag = false;
                    if (hotelId == "" || hotelId != dtResult.Rows[i]["prop"].ToString())
                    {
                        #region
                        for (int j = 0; j < FilterJJHotels.Split(',').Length; j++)
                        {
                            if (!string.IsNullOrEmpty(FilterJJHotels.Split(',')[j].ToString()))
                            {
                                if (dtResult.Rows[i]["prop"].ToString() == FilterJJHotels.Split(',')[j].ToString())
                                {
                                    IsFlag = true;
                                    break;
                                }
                            }
                        }
                        #endregion
                        if (!IsFlag)
                        {
                            #region
                            hotelId = dtResult.Rows[i]["prop"].ToString();
                            DataRow[] rowsAll = dtResult.Select("prop='" + hotelId + "'");//获取当前酒店所有计划
                            DataRow[] rowsCloseAll = dtResult.Select("prop='" + hotelId + "' and status=0");//获取当前酒店所有已关闭的计划
                            if (rowsAll.Length > 0 && rowsCloseAll.Length > 0)
                            {
                                if (rowsAll.Length == rowsCloseAll.Length)//计划全部关闭  且  关闭人为销售人员
                                {
                                    int count = 0;
                                    for (int j = 0; j < rowsCloseAll.Length; j++)
                                    {
                                        DataRow[] salesRow = dtSales.Select("REVALUE_ALL like '%" + rowsCloseAll[j]["MODIFIER"].ToString() + "%'");
                                        if (salesRow.Length > 0)
                                        {
                                            count++;
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (count != rowsCloseAll.Length)
                                    {
                                        DataRow dr = dtNewResult.NewRow();
                                        dr["PROP"] = hotelId;
                                        dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                        dr["LINKMAN"] = dtResult.Rows[i]["linkman"].ToString();
                                        dr["LINKTEL"] = dtResult.Rows[i]["linktel"].ToString();
                                        dr["LINKEMAIL"] = dtResult.Rows[i]["linkemail"].ToString();
                                        dr["SALES_ACCOUNT"] = dtResult.Rows[i]["sales_account"].ToString();
                                        dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                        dr["isplan"] = dtResult.Rows[i]["isplan"].ToString();
                                        dr["ordercount"] = dtResult.Rows[i]["ordercount"].ToString();
                                        dr["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                                        dr["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "&nbsp;&nbsp;" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                                        dr["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();
                                        dr["BackPropName"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                        dtNewResult.Rows.Add(dr);
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtNewResult.NewRow();
                                    dr["PROP"] = hotelId;
                                    dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                    dr["LINKMAN"] = dtResult.Rows[i]["linkman"].ToString();
                                    dr["LINKTEL"] = dtResult.Rows[i]["linktel"].ToString();
                                    dr["LINKEMAIL"] = dtResult.Rows[i]["linkemail"].ToString();
                                    dr["SALES_ACCOUNT"] = dtResult.Rows[i]["sales_account"].ToString();
                                    dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                    dr["isplan"] = dtResult.Rows[i]["isplan"].ToString();
                                    dr["ordercount"] = dtResult.Rows[i]["ordercount"].ToString();
                                    dr["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                                    dr["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "&nbsp;&nbsp;" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                                    dr["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();
                                    dr["BackPropName"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                    dtNewResult.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = dtNewResult.NewRow();
                                dr["PROP"] = hotelId;
                                dr["CITYID"] = dtResult.Rows[i]["cityid"].ToString();
                                dr["LINKMAN"] = dtResult.Rows[i]["linkman"].ToString();
                                dr["LINKTEL"] = dtResult.Rows[i]["linktel"].ToString();
                                dr["LINKEMAIL"] = dtResult.Rows[i]["linkemail"].ToString();
                                dr["SALES_ACCOUNT"] = dtResult.Rows[i]["sales_account"].ToString();
                                dr["PROP_NAME_ZH"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                dr["isplan"] = dtResult.Rows[i]["isplan"].ToString();
                                dr["ordercount"] = dtResult.Rows[i]["ordercount"].ToString();
                                dr["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                                dr["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "&nbsp;&nbsp;" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                                dr["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();
                                dr["BackPropName"] = dtResult.Rows[i]["PROP_NAME_ZH"].ToString();
                                dtNewResult.Rows.Add(dr);
                            }
                            #endregion
                        }
                    }
                }

                dtNewResult.DefaultView.Sort = DropDownList2.SelectedValue;
                dtResult = dtNewResult.DefaultView.ToTable();
            }
            #endregion
        }
        int operandNum = 0;

        this.gridHotelList.DataSource = dtResult.DefaultView;
        this.gridHotelList.DataBind();

        #region
        DataTable dtConsultResult = GetConsultRoomHistoryList().Tables[0];//获取已询房酒店列表 
        for (int i = 0; i < gridHotelList.Rows.Count; i++)
        {
            for (int j = 0; j < dtConsultResult.Rows.Count; j++)
            {
                if (gridHotelList.DataKeys[i].Values[0].ToString().Trim() == dtConsultResult.Rows[j]["HotelID"].ToString().Trim())
                {
                    if (DateTime.Parse(dtConsultResult.Rows[j]["CreateTime"].ToString()).Hour >= 18)
                    {
                        gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                        ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[i].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                    }
                    else
                    {
                        gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#80c0a0");
                        ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[i].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                    }
                    operandNum += 1;
                }
            }
        }
        #endregion
        this.UpdatePanel3.Update();
        this.operandNum.InnerText = operandNum.ToString();
        this.countNum.InnerText = dtResult.Rows.Count.ToString();
        if (dtResult.Rows.Count > 0)//当查询有值时，自动触发单个酒店详情事件 
        {
            this.HidIsAsync.Value = "true";
            this.HidSelIndex.Value = "0";
            btnSingleHotel_Click(null, null);
        }
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "keys", "BtnCompleteStyle();", true);
    }

    protected void gridHotelList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.HidBrowser.Value == "IE")
            {
                e.Row.Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';c=this.childNodes[1].childNodes[3].style.backgroundColor;this.childNodes[1].childNodes[3].style.backgroundColor='#ebebce';");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=t;this.childNodes[1].childNodes[3].style.backgroundColor=c;");
            }
            else
            {
                e.Row.Attributes.Add("onMouseOver", "t=this.style.backgroundColor;this.style.backgroundColor='#ebebce';c=this.childNodes[2].childNodes[5].style.backgroundColor;this.childNodes[2].childNodes[5].style.backgroundColor='#ebebce'");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=t;this.childNodes[2].childNodes[5].style.backgroundColor=c;");
            }
            int selIndex = e.Row.RowIndex;

            e.Row.Attributes.Add("OnClick", "ClickEvent('" + gridHotelList.DataKeys[e.Row.RowIndex].Values[10].ToString() + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "','" + selIndex + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[1].ToString() + "','true')");
        }
    }

    #endregion

    #region  批量执行   批量关房   批量开房
    /// <summary>
    /// Remark之后 确定 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCloseOrFullRoom_Click(object sender, EventArgs e)
    {
        string strRoom = this.HidCloseOrFullByRoom.Value;//关房CloseRoom  开房OpenRoom   满房 FullRoom
        string strRemark = this.divOperateRoomRemark.Value;//Remark
        if (strRoom == "FullRoom")
        {

            btnPlanFullRoom(strRemark);//满房  FullRoom

        }
        else if (strRoom == "CloseRoom")
        {

            btnPlanCloseRoom(strRemark, "false", true);//关房  CloseRoom  false(status  关房 )

        }
        else if (strRoom == "OpenRoom")
        {

            btnPlanCloseRoom(strRemark, "true", true);//开房  OpenRoom   true(status  关房 )

        }
        else
        {

            btnPlanCloseRoom(strRemark, "", false);//ExecuteRoom   批量执行   （不做操作  只记录当天已查房）

        }
        this.divOperateRoomRemark.Value = "";
    }

    /// <summary>
    /// 计划关房（批量操作 关闭计划）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnPlanCloseRoom(string remark, string status, bool isRenew)
    {
        DataTable dtPlanLMBAR2 = new DataTable();
        DataTable dtPlanLMBAR = new DataTable();
        string hotelId = this.HidPid.Value;//酒店ID
        string dateSE = this.HidMarkFullRoom.Value;//起止日期

        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        CommonEntity _commonEntity = new CommonEntity();
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _commonEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _commonEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        bool IsFlag = false;
        if (!string.IsNullOrEmpty(dateSE))
        {

            DataTable dtPlan = GetBindLmbarPlanList(this.planStartDate.Value, this.planEndDate.Value, this.HidPid.Value, "").Tables[0];//总计划
            dtPlanLMBAR2 = dtPlan.Clone();
            dtPlanLMBAR = dtPlan.Clone();
            DataRow[] drPlanLMBAR2 = dtPlan.Select("RATECODE='LMBAR2'"); //LMBAR2计划
            for (int i = 0; i < drPlanLMBAR2.Length; i++)
            {
                dtPlanLMBAR2.ImportRow(drPlanLMBAR2[i]);
            }
            DataRow[] drPlanLMBAR = dtPlan.Select("RATECODE='LMBAR'"); //LMBAR计划
            for (int i = 0; i < drPlanLMBAR.Length; i++)
            {
                dtPlanLMBAR.ImportRow(drPlanLMBAR[i]);
            }



            string[] datas = dateSE.Split(',');
            for (int i = 0; i < datas.Length; i++)
            {
                if (!string.IsNullOrEmpty(datas[i].ToString()))
                {
                    if (DateTime.Parse(datas[i].ToString()).ToShortDateString() == System.DateTime.Now.ToShortDateString())
                    {
                        IsFlag = true;
                    }

                    string effDate = datas[i].ToString().Replace("/", "-");
                    #region
                    for (int l = 0; l < this.HidLmbar2RoomCode.Value.Split(',').Length; l++)
                    {
                        DataRow[] rowsLmbar2 = dtPlanLMBAR2.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + this.HidLmbar2RoomCode.Value.Split(',')[l].ToString() + "'");
                        for (int j = 0; j < rowsLmbar2.Length; j++)
                        {
                            if (!string.IsNullOrEmpty(rowsLmbar2[j]["RoomNum"].ToString()) && rowsLmbar2[j]["RoomNum"].ToString().ToLower() != "null")
                            {
                                //城市ID
                                appcontentDBEntity.CityID = this.HidCityID.Value;
                                //酒店ID 
                                appcontentDBEntity.HotelID = hotelId;
                                //酒店名称
                                appcontentDBEntity.HotelNM = this.HidPcode.Value;
                                //PlanDate
                                appcontentDBEntity.PlanTime = DateTime.Parse(effDate).ToShortDateString();
                                //价格代码
                                appcontentDBEntity.PriceCode = rowsLmbar2[j]["RATECODE"].ToString();
                                //价格
                                appcontentDBEntity.TwoPrice = rowsLmbar2[j]["TWOPRICE"].ToString();
                                //状态     开启 关闭  
                                //appcontentDBEntity.PlanStatus = rowsLmbar2[j]["STATUS"].ToString();
                                appcontentDBEntity.PlanStatus = status == "" ? rowsLmbar2[j]["STATUS"].ToString() : status;
                                appcontentDBEntity.RoomCount = rowsLmbar2[j]["ROOMNUM"].ToString();
                                appcontentDBEntity.IsReserve = rowsLmbar2[j]["ISRESERVE"].ToString();
                                //房型名称
                                appcontentDBEntity.RoomName = rowsLmbar2[j]["ROOMTYPENAME"].ToString();
                                //房型Code
                                appcontentDBEntity.RoomCode = rowsLmbar2[j]["ROOMTYPECODE"].ToString();

                                appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
                                //备注
                                appcontentDBEntity.Remark = remark;
                                //操作人 
                                appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;

                                _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
                                CommonBP.InsertConsultRoomHistory(_appcontentEntity);
                                _appcontentEntity.APPContentDBEntity.Clear();
                            }
                        }
                    }
                    #endregion
                    #region
                    for (int l = 0; l < this.HidLmbarRoomCode.Value.Split(',').Length; l++)
                    {
                        DataRow[] rowsLmbar = dtPlanLMBAR.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + this.HidLmbarRoomCode.Value.Split(',')[l].ToString() + "'");
                        for (int j = 0; j < rowsLmbar.Length; j++)
                        {
                            if (!string.IsNullOrEmpty(rowsLmbar[j]["ROOMNUM"].ToString()) && rowsLmbar[j]["RoomNum"].ToString().ToLower() != "null")
                            {
                                //城市ID
                                appcontentDBEntity.CityID = this.HidCityID.Value;
                                //酒店ID 
                                appcontentDBEntity.HotelID = hotelId;
                                //酒店名称
                                appcontentDBEntity.HotelNM = this.HidPcode.Value;
                                //PlanDate
                                appcontentDBEntity.PlanTime = DateTime.Parse(effDate).ToShortDateString();
                                //价格代码
                                appcontentDBEntity.PriceCode = rowsLmbar[j]["RATECODE"].ToString();
                                //价格
                                appcontentDBEntity.TwoPrice = rowsLmbar[j]["TWOPRICE"].ToString();
                                //状态     开启 关闭  
                                //appcontentDBEntity.PlanStatus = rowsLmbar[j]["STATUS"].ToString();
                                appcontentDBEntity.PlanStatus = status == "" ? rowsLmbar[j]["STATUS"].ToString() : status;
                                appcontentDBEntity.RoomCount = rowsLmbar[j]["ROOMNUM"].ToString();
                                appcontentDBEntity.IsReserve = rowsLmbar[j]["ISRESERVE"].ToString();
                                //房型名称
                                appcontentDBEntity.RoomName = rowsLmbar[j]["ROOMTYPENAME"].ToString();
                                //房型Code
                                appcontentDBEntity.RoomCode = rowsLmbar[j]["ROOMTYPECODE"].ToString();

                                appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
                                //备注
                                appcontentDBEntity.Remark = remark;
                                //操作人 
                                appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;
                                _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
                                CommonBP.InsertConsultRoomHistory(_appcontentEntity);
                                _appcontentEntity.APPContentDBEntity.Clear();
                            }
                        }
                    }
                    #endregion
                    if (isRenew)
                    {
                        appcontentDBEntity.HotelID = hotelId;
                        appcontentDBEntity.StartDTime = effDate;
                        appcontentDBEntity.EndDTime = effDate;
                        appcontentDBEntity.Lmbar2RoomCode = this.HidLmbar2RoomCode.Value;
                        appcontentDBEntity.LmbarRoomCode = this.HidLmbarRoomCode.Value;
                        appcontentDBEntity.TypeID = status == "true" ? "3" : "2";// "2";//type:1 满房、2 关房、3 开房
                        appcontentDBEntity.UpdateUser = UserSession.Current.UserAccount;
                        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

                        _appcontentEntity = HotelInfoBP.BatchUpdatePlan(_appcontentEntity);

                        _appcontentEntity.APPContentDBEntity.Clear();
                    }
                }
            }
        }
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "keyclosebtn", "BtnCompleteStyle();", true);

        int SelectedIndex = int.Parse(this.HidSelIndex.Value);
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "clickbtnSingle", "ClickEvent('" + this.HidPcode.Value + "','" + this.HidPid.Value + "','" + SelectedIndex + "','" + this.HidCityID.Value + "','false');", true);
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "scrollReset", "GetResultFromServer();", true);
        if (IsFlag)
        {
            if (DateTime.Now.Hour >= 18)
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                {
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
            }
            else
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0"))
                {
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#80c0a0");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
            }
        }
    }

    /// <summary>
    /// 标记满房（批量操作满房）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnPlanFullRoom(string remark)
    {
        DataTable dtPlanLMBAR2 = new DataTable();
        DataTable dtPlanLMBAR = new DataTable();
        string hotelId = this.HidPid.Value;//酒店ID
        string dateSE = this.HidMarkFullRoom.Value;//起止日期

        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        CommonEntity _commonEntity = new CommonEntity();
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _commonEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _commonEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        bool IsFlag = false;
        if (!string.IsNullOrEmpty(dateSE))
        {
            DataTable dtPlan = GetBindLmbarPlanList(this.planStartDate.Value, this.planEndDate.Value, this.HidPid.Value, "").Tables[0];//总计划
            dtPlanLMBAR2 = dtPlan.Clone();
            dtPlanLMBAR = dtPlan.Clone();
            DataRow[] drPlanLMBAR2 = dtPlan.Select("RATECODE='LMBAR2'"); //LMBAR2计划
            for (int i = 0; i < drPlanLMBAR2.Length; i++)
            {
                dtPlanLMBAR2.ImportRow(drPlanLMBAR2[i]);
            }
            DataRow[] drPlanLMBAR = dtPlan.Select("RATECODE='LMBAR'"); //LMBAR计划
            for (int i = 0; i < drPlanLMBAR.Length; i++)
            {
                dtPlanLMBAR.ImportRow(drPlanLMBAR[i]);
            }

            string[] datas = dateSE.Split(',');
            for (int i = 0; i < datas.Length; i++)
            {
                if (!string.IsNullOrEmpty(datas[i].ToString()))
                {
                    if (DateTime.Parse(datas[i].ToString()) == System.DateTime.Now)
                    {
                        IsFlag = true;
                    }
                    string effDate = datas[i].ToString().Replace("/", "-");
                    #region
                    for (int l = 0; l < this.HidLmbar2RoomCode.Value.Split(',').Length; l++)
                    {
                        DataRow[] rowsLmbar2 = dtPlanLMBAR2.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + this.HidLmbar2RoomCode.Value.Split(',')[l].ToString() + "'");
                        for (int j = 0; j < rowsLmbar2.Length; j++)
                        {
                            //城市ID
                            appcontentDBEntity.CityID = this.HidCityID.Value;
                            //酒店ID 
                            appcontentDBEntity.HotelID = hotelId;
                            //酒店名称
                            appcontentDBEntity.HotelNM = this.HidPcode.Value;
                            //PlanDate
                            appcontentDBEntity.PlanTime = DateTime.Parse(effDate).ToShortDateString();
                            //价格代码
                            appcontentDBEntity.PriceCode = rowsLmbar2[j]["RATECODE"].ToString();
                            //价格
                            appcontentDBEntity.TwoPrice = rowsLmbar2[j]["TWOPRICE"].ToString();
                            //状态     开启 关闭  
                            appcontentDBEntity.PlanStatus = rowsLmbar2[j]["STATUS"].ToString();
                            appcontentDBEntity.RoomCount = rowsLmbar2[j]["ROOMNUM"].ToString();
                            appcontentDBEntity.IsReserve = rowsLmbar2[j]["ISRESERVE"].ToString();
                            //房型名称
                            appcontentDBEntity.RoomName = rowsLmbar2[j]["ROOMTYPENAME"].ToString();
                            //房型Code
                            appcontentDBEntity.RoomCode = rowsLmbar2[j]["ROOMTYPECODE"].ToString();

                            appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
                            //备注
                            appcontentDBEntity.Remark = remark;
                            //操作人 
                            appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;

                            _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
                            CommonBP.InsertConsultRoomHistory(_appcontentEntity);
                            _appcontentEntity.APPContentDBEntity.Clear();
                        }
                    }
                    #endregion
                    #region
                    for (int l = 0; l < this.HidLmbarRoomCode.Value.Split(',').Length; l++)
                    {
                        DataRow[] rowsLmbar = dtPlanLMBAR.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + this.HidLmbar2RoomCode.Value.Split(',')[l].ToString() + "'");
                        for (int j = 0; j < rowsLmbar.Length; j++)
                        {
                            //城市ID
                            appcontentDBEntity.CityID = this.HidCityID.Value;
                            //酒店ID 
                            appcontentDBEntity.HotelID = hotelId;
                            //酒店名称
                            appcontentDBEntity.HotelNM = this.HidPcode.Value;
                            //PlanDate
                            appcontentDBEntity.PlanTime = DateTime.Parse(effDate).ToShortDateString();
                            //价格代码
                            appcontentDBEntity.PriceCode = rowsLmbar[j]["RATECODE"].ToString();
                            //价格
                            appcontentDBEntity.TwoPrice = rowsLmbar[j]["TWOPRICE"].ToString();
                            //状态     开启 关闭  
                            appcontentDBEntity.PlanStatus = rowsLmbar[j]["STATUS"].ToString();
                            appcontentDBEntity.RoomCount = rowsLmbar[j]["ROOMNUM"].ToString();
                            appcontentDBEntity.IsReserve = rowsLmbar[j]["ISRESERVE"].ToString();
                            //房型名称
                            appcontentDBEntity.RoomName = rowsLmbar[j]["ROOMTYPENAME"].ToString();
                            //房型Code
                            appcontentDBEntity.RoomCode = rowsLmbar[j]["ROOMTYPECODE"].ToString();

                            appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
                            //备注
                            appcontentDBEntity.Remark = remark;
                            //操作人 
                            appcontentDBEntity.CreateUser = UserSession.Current.UserAccount;
                            _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
                            CommonBP.InsertConsultRoomHistory(_appcontentEntity);
                            _appcontentEntity.APPContentDBEntity.Clear();
                        }
                    }
                    #endregion
                    appcontentDBEntity.HotelID = hotelId;
                    appcontentDBEntity.StartDTime = effDate;
                    appcontentDBEntity.EndDTime = effDate;
                    appcontentDBEntity.Lmbar2RoomCode = this.HidLmbar2RoomCode.Value;
                    appcontentDBEntity.LmbarRoomCode = this.HidLmbar2RoomCode.Value;
                    appcontentDBEntity.TypeID = "1";//type:1 满房、2 关房、3 开房
                    appcontentDBEntity.UpdateUser = UserSession.Current.UserAccount;
                    _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

                    _appcontentEntity = HotelInfoBP.BatchUpdatePlan(_appcontentEntity);

                    _appcontentEntity.APPContentDBEntity.Clear();
                }
            }
        }
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "keybtn4", "BtnCompleteStyle();", true);

        int SelectedIndex = int.Parse(this.HidSelIndex.Value);
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "clickbtnSingle", "ClickEvent('" + this.HidPcode.Value + "','" + this.HidPid.Value + "','" + SelectedIndex + "','" + this.HidCityID.Value + "','false');", true);
        if (IsFlag)
        {
            if (DateTime.Now.Hour >= 18)
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                {
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
            }
            else
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0"))
                {
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#80c0a0");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
            }
        }
    }

    #endregion

    #region  上一个   下一个
    /// <summary>
    /// 控制上一个   下一个酒店 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        string selectIndex = this.HidSelIndex.Value;//当前选中酒店 
        string HidLastOrNextByHotel = this.HidLastOrNextByHotel.Value;//上一个 还是 下一个 
        if (HidLastOrNextByHotel == "1")
        {
            this.HidScrollValue.Value = "30";
        }
        else
        {
            this.HidScrollValue.Value = "-30";
        }
        int Index = int.Parse(selectIndex) + (int.Parse(HidLastOrNextByHotel));
        if (Index != -1 && Index < gridHotelList.Rows.Count)
        {
            this.HidSelIndex.Value = Index.ToString();
            string hotelName = gridHotelList.Rows[Index].Cells[1].Text;
            string hotelID = gridHotelList.DataKeys[Index].Values[0].ToString();
            string cityID = gridHotelList.DataKeys[Index].Values[1].ToString();

            this.HidPcode.Value = hotelName;
            this.HidPid.Value = hotelID;
            this.HidSelIndex.Value = Index.ToString();
            this.HidCityID.Value = cityID;

            btnSingleHotel_Click(null, null);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "keyalert", "alert('" + HidLastOrNextByHotel.Replace("-1", "无上一个！").Replace("1", "无下一个！") + "')", true);
        }
    }

    #endregion

    #region   LM联系人
    /// <summary>
    /// LM  联系人   
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAlertLink_Click(object sender, EventArgs e)
    {
        GetBasicSalesInfo(this.HidPid.Value);
    }

    /// <summary>
    /// 酒店信息-销售联系人
    /// </summary>
    /// <param name="strHotelID"></param>
    public void GetBasicSalesInfo(string strHotelID)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

        DataSet dsResult = HotelInfoBP.GetSalesManager(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            lblContactDetails.Text = "LM酒店负责人:" + dsResult.Tables[0].Rows[0]["DISPNM"].ToString().Replace("null", "") + " &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;    电话:      " + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Replace("null", "");
            this.HidContactDetails.Value = "LM酒店负责人:" + dsResult.Tables[0].Rows[0]["DISPNM"].ToString().Replace("null", "") + "  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;     电话:      " + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Replace("null", "");
            this.HidAlertContactDetails.Value = "LM酒店负责人:" + dsResult.Tables[0].Rows[0]["DISPNM"].ToString().Replace("null", "") + "     电话:      " + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Replace("null", "");
            ScriptManager.RegisterStartupScript(this.UpdatePanel7, this.GetType(), "msgAlertY", "alert('" + this.HidAlertContactDetails.Value + "')", true);

        }
        else
        {
            lblContactDetails.Text = "LM酒店负责人:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;    电话:      ";
            this.HidContactDetails.Value = "LM酒店负责人:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;    电话:      ";
            this.HidAlertContactDetails.Value = "LM酒店负责人:    电话:      ";
            ScriptManager.RegisterStartupScript(this.UpdatePanel7, this.GetType(), "msgAlertN", "alert('" + this.HidAlertContactDetails.Value + "')", true);
        }
    }
    #endregion

    #region   修改计划EX信息
    /// <summary>
    /// 修改备注
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditRemark_Click(object sender, EventArgs e)
    {
        GetHotelExInfo();

        HotelInfoEXEntity _hotelinfoEXEntity = new HotelInfoEXEntity();

        _hotelinfoEXEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEXEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEXEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEXEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEXEntity.HotelInfoEXDBEntity = new List<HotelInfoEXDBEntity>();
        HotelInfoEXDBEntity hotelInfoEXDBEntity = new HotelInfoEXDBEntity();

        StringBuilder QueryRoomLinkMan = new StringBuilder();
        for (int i = 0; i < 23; i++)
        {
            QueryRoomLinkMan.Append(this.HotelEXLinkMan_txt.Text + "|"); //查房联系人
        }

        StringBuilder QueryRoomLinkTel = new StringBuilder();
        for (int i = 0; i < 23; i++)
        {
            QueryRoomLinkTel.Append(this.HotelEXLinkTel_txt.Text + "|");//查房联系电话
        }

        if (this.EXHotelInfo.Value != "0")
        {

            #region
            hotelInfoEXDBEntity.HotelID = this.HidPid.Value;//酒店ID
            hotelInfoEXDBEntity.Type = "1";
            hotelInfoEXDBEntity.LinkMan = QueryRoomLinkMan.ToString().TrimEnd('|');
            hotelInfoEXDBEntity.LinkTel = QueryRoomLinkTel.ToString().TrimEnd('|');
            hotelInfoEXDBEntity.LinkFax = this.EXLinkFax.Value.ToString();
            hotelInfoEXDBEntity.Remark = this.HotelEXLinkRemark_txt.InnerHtml;
            hotelInfoEXDBEntity.ExTime = this.EXExTime.Value.ToString();
            hotelInfoEXDBEntity.ExMode = this.EXExMode.Value.ToString();
            hotelInfoEXDBEntity.Status = "1";
            hotelInfoEXDBEntity.CreateUser = UserSession.Current.UserAccount;
            hotelInfoEXDBEntity.UpdateUser = UserSession.Current.UserAccount;
            _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);
            HotelInfoEXBP.UpdateHotelInfoEXByConsultRoom(_hotelinfoEXEntity);
            HotelInfoEXBP.InsertHotelEXHistory(_hotelinfoEXEntity);
            #endregion

        }
        else
        {

            #region
            hotelInfoEXDBEntity.HotelID = this.HidPid.Value;
            hotelInfoEXDBEntity.Type = "1";
            hotelInfoEXDBEntity.LinkMan = QueryRoomLinkMan.ToString().TrimEnd('|');
            hotelInfoEXDBEntity.LinkTel = QueryRoomLinkTel.ToString().TrimEnd('|');
            hotelInfoEXDBEntity.LinkFax = "";
            hotelInfoEXDBEntity.Remark = this.HotelEXLinkRemark_txt.InnerHtml;
            hotelInfoEXDBEntity.ExTime = "111110000000000000111111";
            hotelInfoEXDBEntity.ExMode = "1";
            hotelInfoEXDBEntity.Status = "1";
            hotelInfoEXDBEntity.CreateUser = UserSession.Current.UserAccount;
            hotelInfoEXDBEntity.UpdateUser = UserSession.Current.UserAccount;
            _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);
            HotelInfoEXBP.InsertHotelInfoEX(_hotelinfoEXEntity);
            HotelInfoEXBP.InsertHotelEXHistory(_hotelinfoEXEntity);
            #endregion

        }

        this.HotelEXLinkMan_span.Text = this.HotelEXLinkMan_txt.Text;
        this.HotelEXLinkMan_txt.Text = this.HotelEXLinkMan_txt.Text;
        this.HotelEXLinkTel_span.Text = this.HotelEXLinkTel_txt.Text;
        this.HotelEXLinkTel_txt.Text = this.HotelEXLinkTel_txt.Text;

        this.HotelEXLinkRemark_span.InnerHtml = this.HotelEXLinkRemark_txt.InnerHtml;
        this.HotelEXLinkRemark_txt.InnerHtml = this.HotelEXLinkRemark_txt.InnerHtml;
        this.btnClearLock.Attributes["style"] = "display:''";
        this.SPANHotelEXLinkRemark.Attributes["style"] = "display:''";
        this.btnEditRemark.Attributes.Add("style", "display:none");
        this.TXTotelEXLinkRemark.Attributes.Add("style", "display:none");
        this.UpdatePanel8.Update();
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "keyEditRemark", "BtnCompleteStyle();", true);
    }
    #endregion

    #region  弹出框 单个房型 更新计划信息
    /// <summary>
    /// 更新计划 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDivRenewPlan_Click(object sender, EventArgs e)
    {

        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        CommonEntity _commonEntity = new CommonEntity();
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _commonEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _commonEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        #region
        //城市ID
        string CityID = this.HidCityID.Value;
        appcontentDBEntity.CityID = CityID;
        //酒店ID 
        string hotelID = this.HidPid.Value;
        appcontentDBEntity.HotelID = hotelID;
        //酒店名称
        string hotelName = this.HidPcode.Value;
        appcontentDBEntity.HotelNM = hotelName;
        //价格代码
        string priceCode = this.HiddenPriceCode.Value;
        appcontentDBEntity.PriceCode = priceCode;
        //价格
        string twoPrice = this.lblDivPrice.Text;
        appcontentDBEntity.TwoPrice = twoPrice;
        //状态     开启 关闭  
        string status = this.dropDivStatusOpen.Checked == true ? "true" : "false";
        appcontentDBEntity.RoomStatus = status;
        appcontentDBEntity.PlanStatus = status;
        if (status == "true")
        {
            //房量
            if (this.txtDivRoomCount.Text.Trim() != "")
            {
                string roomNum = this.txtDivRoomCount.Text;
                appcontentDBEntity.RoomCount = roomNum;
            }
            //是否是保留房
            string isReserve = this.ckDivReserve.Checked == true ? "0" : "1";
            appcontentDBEntity.IsReserve = isReserve;
        }
        else
        {
            appcontentDBEntity.RoomCount = this.HiddenRoomNum.Value;
            appcontentDBEntity.IsReserve = this.HiddenIsReserve.Value;
        }
        //房型名称
        string RoomName = this.HiddenRoomType.Value;
        appcontentDBEntity.RoomName = RoomName;
        //房型Code
        string RoomCode = this.HiddenRoomCode.Value;
        appcontentDBEntity.RoomCode = RoomCode;

        bool IsFlag = false;

        //批量更新日期   开始  结束 
        string divPlanStartDate = this.divPlanStartDate.Value;
        string divPlanEndDate = this.divPlanEndDate.Value;
        if (DateTime.Parse(divPlanStartDate) >= System.DateTime.Now || DateTime.Parse(divPlanEndDate) <= System.DateTime.Now)
        {
            IsFlag = true;
        }
        appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";

        //备注
        string remark = this.txtRemark.Value;
        appcontentDBEntity.Remark = remark;
        //操作人 
        string userName = UserSession.Current.UserAccount;
        appcontentDBEntity.CreateUser = userName;
        appcontentDBEntity.UpdateUser = userName;

        #endregion



        #region
        appcontentDBEntity.StartDTime = divPlanStartDate;
        appcontentDBEntity.EndDTime = divPlanEndDate;
        int DateDiff = calculateDateDiff(divPlanStartDate, divPlanEndDate);

        for (int j = 0; j <= DateDiff; j++)
        {
            appcontentDBEntity.PlanTime = DateTime.Parse(divPlanStartDate).AddDays(j).ToShortDateString();
            _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
            CommonBP.InsertConsultRoomHistory(_appcontentEntity);
            _appcontentEntity.APPContentDBEntity.Clear();
        }

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

        _appcontentEntity = HotelInfoBP.RenewPlanFullRoomByUpdatePlan(_appcontentEntity);

        int SelectedIndex = int.Parse(this.HidSelIndex.Value);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "clickbtnSingle", "ClickEvent('" + this.HidPcode.Value + "','" + this.HidPid.Value + "','" + SelectedIndex + "','" + this.HidCityID.Value + "','false');", true);

        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "scrollReset", "GetResultFromServer();", true);
        if (IsFlag)
        {
            if (DateTime.Now.Hour >= 18)
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                {
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                    //((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
            }
            else
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0"))
                {
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#80c0a0");
                    //((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 计算日期差 
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    private int calculateDateDiff(string startDate, string endDate)
    {
        TimeSpan start = new TimeSpan(DateTime.Parse(startDate).Ticks);
        TimeSpan end = new TimeSpan(DateTime.Parse(endDate).Ticks);
        TimeSpan ts = start.Subtract(end).Duration();
        return int.Parse(ts.Days.ToString());
    }
    #endregion

    #region AJAX异步加载操作历史以及DataTable 转换 JSON
    [WebMethod]
    public static void GetNextOrLastHotelDetails(string JudgeLast, string JudgeNext, string startDate, string endDate, string LastHotelSelectName, string NextHotelSelectName)
    {
        lastRowLmbar = new DataTable(); nextRowLmbar = new DataTable();
        lastRowLmbar2 = new DataTable(); nextRowLmbar2 = new DataTable();
        lastDrRoomListLMBAR = new DataTable(); nextDrRoomListLMBAR = new DataTable();
        lastDrRoomListLMBAR2 = new DataTable(); nextDrRoomListLMBAR2 = new DataTable();
        if (JudgeLast == "Last")
        {
            //上一个 
            #region
            DataTable dtPlan = GetBindLmbarPlanList(startDate, endDate, LastHotelSelectName, "").Tables[0];//总计划
            DataRow[] rowLabar2 = dtPlan.Select("RATECODE='LMBAR2'"); //上一个LMBAR2计划
            lastRowLmbar2 = dtPlan.Clone();
            for (int i = 0; i < rowLabar2.Length; i++)
            {
                lastRowLmbar2.ImportRow(rowLabar2[i]);
            }
            DataRow[] rowLabar = dtPlan.Select("RATECODE='LMBAR'"); //上一个LMBAR计划
            lastRowLmbar = dtPlan.Clone();
            for (int i = 0; i < rowLabar.Length; i++)
            {
                lastRowLmbar.ImportRow(rowLabar[i]);
            }
            #endregion
            #region
            DataTable dtRoomList = BindBalanceRoomList(LastHotelSelectName).Tables[0];//上一个 LMBAR2房型CODE
            DataRow[] roomLmbar2 = dtRoomList.Select("PRICECODE ='LMBAR2'");
            lastDrRoomListLMBAR2 = dtRoomList.Clone();
            for (int i = 0; i < roomLmbar2.Length; i++)
            {
                lastDrRoomListLMBAR2.ImportRow(roomLmbar2[i]);
            }
            DataRow[] roomLmbar = dtRoomList.Select("PRICECODE ='LMBAR'");//上一个 LMBAR房型CODE
            lastDrRoomListLMBAR = dtRoomList.Clone();
            for (int i = 0; i < roomLmbar.Length; i++)
            {
                lastDrRoomListLMBAR.ImportRow(roomLmbar[i]);
            }
            #endregion
        }
        if (JudgeNext == "Next")
        {
            //下一个 
            #region
            DataTable dtPlan = GetBindLmbarPlanList(startDate, endDate, NextHotelSelectName, "").Tables[0];//总计划
            DataRow[] rowLabar2 = dtPlan.Select("RATECODE='LMBAR2'"); //下一个LMBAR2计划
            nextRowLmbar2 = dtPlan.Clone();
            for (int i = 0; i < rowLabar2.Length; i++)
            {
                nextRowLmbar2.ImportRow(rowLabar2[i]);
            }
            DataRow[] rowLabar = dtPlan.Select("RATECODE='LMBAR'"); //下一个LMBAR计划
            nextRowLmbar = dtPlan.Clone();
            for (int i = 0; i < rowLabar.Length; i++)
            {
                nextRowLmbar.ImportRow(rowLabar[i]);
            }
            #endregion
            #region
            DataTable dtRoomList = BindBalanceRoomList(NextHotelSelectName).Tables[0];//下一个 LMBAR2房型CODE
            DataRow[] roomLmbar2 = dtRoomList.Select("PRICECODE ='LMBAR2'");
            nextDrRoomListLMBAR2 = dtRoomList.Clone();
            for (int i = 0; i < roomLmbar2.Length; i++)
            {
                nextDrRoomListLMBAR2.ImportRow(roomLmbar2[i]);
            }
            DataRow[] roomLmbar = dtRoomList.Select("PRICECODE ='LMBAR'");//下一个 LMBAR房型CODE
            nextDrRoomListLMBAR = dtRoomList.Clone();
            for (int i = 0; i < roomLmbar.Length; i++)
            {
                nextDrRoomListLMBAR.ImportRow(roomLmbar[i]);
            }
            #endregion
        }
    }

    /// <summary>
    /// 获取当前计划的HistoryRemark
    /// </summary>
    /// <param name="strHotelID"></param>
    [WebMethod]
    public static string GetHistoryRemarkByJson(string CityID, string HotelID, string PriceCode, string RoomCode, string PlanDTime)
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.CityID = CityID;
        appcontentDBEntity.HotelID = HotelID;
        appcontentDBEntity.PriceCode = PriceCode;
        appcontentDBEntity.RoomCode = RoomCode;
        appcontentDBEntity.PlanDTime = DateTime.Parse(PlanDTime).ToShortDateString();

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        string json = "";
        if (!CityID.Equals("undefined"))
        {
            DataSet dsResult = HotelInfoBP.GetConsultRoomHistoryList(_appcontentEntity).QueryResult;
            try
            {
                if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    json = ToJson(dsResult.Tables[0]);
                }
                else
                {
                    json = "{\"d\":\"[]\"}";
                }
            }
            catch (Exception ex)
            {
                json = "{\"d\":\"[]\"}";
            }
        }
        return json;
    }

    public static string ToJson(DataTable dt)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("[");
        DataRowCollection drc = dt.Rows;
        for (int i = 0; i < drc.Count; i++)
        {
            jsonString.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string strKey = dt.Columns[j].ColumnName;
                string strValue = drc[i][j].ToString();
                Type type = dt.Columns[j].DataType;
                jsonString.Append("\"" + strKey + "\":");
                strValue = StringFormat(strValue, type);
                if (j < dt.Columns.Count - 1)
                {
                    jsonString.Append(strValue + ",");
                }
                else
                {
                    jsonString.Append(strValue);
                }
            }
            jsonString.Append("},");
        }
        jsonString.Remove(jsonString.Length - 1, 1);
        jsonString.Append("]");
        return jsonString.ToString();
    }

    private static string StringFormat(string str, Type type)
    {
        if (type == typeof(string))
        {
            str = String2Json(str);
            str = "\"" + str + "\"";
        }
        else if (type == typeof(DateTime))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(bool))
        {
            str = str.ToLower();
        }
        else if (type != typeof(string) && string.IsNullOrEmpty(str))
        {
            str = "\"" + str + "\"";
        }
        return str;
    }

    private static string String2Json(String s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s.ToCharArray()[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\""); break;
                case '\\':
                    sb.Append("\\\\"); break;
                case '/':
                    sb.Append("\\/"); break;
                case '\b':
                    sb.Append("\\b"); break;
                case '\f':
                    sb.Append("\\f"); break;
                case '\n':
                    sb.Append("\\n"); break;
                case '\r':
                    sb.Append("\\r"); break;
                case '\t':
                    sb.Append("\\t"); break;
                default:
                    sb.Append(c); break;
            }
        }
        return sb.ToString();
    }


    #endregion

    /// <summary>
    /// 获取所有的销售人员--SQL
    /// </summary>
    private DataTable GetSalesManagerList()
    {
        WebAutoCompleteBP webBP = new WebAutoCompleteBP();
        DataTable dtSales = webBP.GetWebCompleteList("sales", "", "").Tables[0];

        return dtSales;
    }

    /// <summary>
    /// 获取当天已更新过的酒店列表--SQL
    /// </summary>
    /// <param name="HotelID"></param>
    /// <returns></returns>
    private DataSet GetConsultRoomHistoryList()
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.CreateUser = UserSession.Current.UserDspName;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        DataSet dsResult = HotelInfoBP.GetHasChangedConsultRoomList(_appcontentEntity).QueryResult;
        return dsResult;
    }

    /// <summary>
    /// 从HotelEx 获取询房信息  --- Oracle
    /// </summary>
    /// <returns></returns>
    public DataRow GetHotelExInfo()
    {
        HotelInfoEXEntity _hotelinfoEXEntity = new HotelInfoEXEntity();

        _hotelinfoEXEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEXEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEXEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEXEntity.LogMessages.IpAddress = UserSession.Current.UserIP;


        _hotelinfoEXEntity.HotelInfoEXDBEntity = new List<HotelInfoEXDBEntity>();
        HotelInfoEXDBEntity hotelInfoEXDBEntity = new HotelInfoEXDBEntity();

        hotelInfoEXDBEntity.HotelID = this.HidPid.Value;//酒店ID
        _hotelinfoEXEntity.HotelInfoEXDBEntity.Add(hotelInfoEXDBEntity);

        DataSet dsResult = HotelInfoEXBP.SelectHotelInfoEX(_hotelinfoEXEntity).QueryResult;


        if (dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count > 0)
        {
            DataRow[] rows = dsResult.Tables[0].Select("type='" + 1 + "'");
            if (rows.Length > 0)
            {
                this.EXHotelInfo.Value = "1";
                this.EXLinkFax.Value = rows[0]["LinkFax"].ToString();
                this.EXExTime.Value = rows[0]["EX_Time"].ToString();
                this.EXExMode.Value = rows[0]["EX_Mode"].ToString();
                return rows[0];
            }
            else
            {
                this.EXHotelInfo.Value = "0";
                this.EXLinkFax.Value = "";
                this.EXExTime.Value = "";
                this.EXExMode.Value = "";
                return null;
            }
        }
        else
        {
            this.EXHotelInfo.Value = "0";
            this.EXLinkFax.Value = "";
            this.EXExTime.Value = "";
            this.EXExMode.Value = "";
            return null;
        }
    }

    /// <summary>
    /// 酒店列表 单个酒店选择  获取酒店信息  拼装计划信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSingleHotel_Click(object sender, EventArgs e)
    {
        judgeLastOrNext(int.Parse(this.HidSelIndex.Value));

        DivLastOrNext.Attributes.Add("style", "float: right; vertical-align: super; width: 100%;display: block;");

        string PROP = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[0].ToString();//prop
        string PROPName = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[6].ToString();//prop
        string LINKMAN = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[2].ToString();//LINKMAN
        string LINKTEL = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[3].ToString();//LINKTEL
        string LINKEMAIL = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[4].ToString();//LINKEMAIL
        string SALES_ACCOUNT = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[5].ToString();//SALES_ACCOUNT

        this.lblHotelInfo.Text = "酒店ID:" + PROP.Replace("null", "") + " &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 酒店名称:      " + PROPName.Replace("null", "");
        this.HidLinkDetails.Value = "酒店联系人:" + LINKMAN.Replace("null", "") + "   &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  电话:      " + LINKTEL.Replace("null", "");

        this.spanHotelInfo.InnerHtml = "[" + PROP.Replace("null", "") + "]&nbsp; - &nbsp;" + gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[10].ToString().Replace("null", "");

        this.HotelEXLinkMan_span.Text = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[7].ToString();
        this.HotelEXLinkMan_txt.Text = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[7].ToString();
        this.HotelEXLinkTel_span.Text = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[8].ToString();
        this.HotelEXLinkTel_txt.Text = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[8].ToString();
        this.HotelEXLinkRemark_txt.InnerHtml = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[9].ToString();
        this.HotelEXLinkRemark_span.InnerHtml = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[9].ToString();

        //string hotelSelectName = this.HidPid.Value;//酒店ID 
        string hotelSelectName = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[0].ToString();
        string startDate = this.planStartDate.Value;//开始时间
        string endDate = this.planEndDate.Value;//结束时间

        contrastTable(hotelSelectName);//判断缓存的DataTable里面是包含该酒店

        if (rowLmbar2 == null || rowLmbar == null)
        {
            DataTable dtPlan = GetBindLmbarPlanList(startDate, endDate, hotelSelectName, "").Tables[0];//总计划
            rowLmbar2 = dtPlan.Select("RATECODE='LMBAR2'"); //LMBAR2计划
            rowLmbar = dtPlan.Select("RATECODE='LMBAR'"); //LMBAR计划
        }

        if (drRoomListLMBAR2 == null || drRoomListLMBAR == null)
        {
            DataTable dtRoomList = BindBalanceRoomList(hotelSelectName).Tables[0];//LMBAR2房型CODE
            drRoomListLMBAR2 = dtRoomList.Select("PRICECODE ='LMBAR2'");
            this.HidLmbar2RoomCode.Value = "";
            for (int i = 0; i < drRoomListLMBAR2.Length; i++)
            {
                this.HidLmbar2RoomCode.Value += drRoomListLMBAR2[i]["ROOMCODE"].ToString() + ",";
            }
            this.HidLmbar2RoomCode.Value = this.HidLmbar2RoomCode.Value.TrimEnd(',');
            drRoomListLMBAR = dtRoomList.Select("PRICECODE ='LMBAR'");
            this.HidLmbarRoomCode.Value = "";
            for (int i = 0; i < drRoomListLMBAR.Length; i++)
            {
                this.HidLmbarRoomCode.Value += drRoomListLMBAR[i]["ROOMCODE"].ToString() + ",";
            }
            this.HidLmbarRoomCode.Value = this.HidLmbarRoomCode.Value.TrimEnd(',');
        }

        dtTime = GetDate(startDate, endDate);
        IsErgodic = true;

        string sjs = "GetResultFromServer();";
        ScriptManager.RegisterClientScriptBlock(this.gridHotelList, this.GetType(), "", sjs, true);

        gridHotelList.SelectedIndex = int.Parse(this.HidSelIndex.Value);

        if (HidSelIndexOld.Value != "")
        {
            if (gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0"))
            {
                ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#ECECEC");
            }
        }
        if (gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0"))
        {
            ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
        }
        this.HidSelIndexOld.Value = this.HidSelIndex.Value;

        this.UpdatePanel3.Update();
        this.UpdatePanel4.Update();
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "keyOvers", "BtnCompleteStyle();", true);
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "alertAsync" + System.DateTime.Now.Millisecond.ToString(), "showA();", true);
        //if (this.HidIsAsync.Value == "true")
        //{
        //    ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "alertAsync" + System.DateTime.Now.Millisecond.ToString(), "showA();", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "alertAsync" + System.DateTime.Now.Millisecond.ToString(), "showA();", true);
        //}
    }

    /// <summary>
    /// 根据时间段  HotelID 取计划  --  接口 
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="strHotelID"></param>
    /// <returns></returns>
    public static DataSet GetBindLmbarPlanList(string startDate, string endDate, string strHotelID, string priceCode)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        hotelInfoDBEntity.SalesStartDT = startDate;
        hotelInfoDBEntity.SalesEndDT = endDate;
        hotelInfoDBEntity.PriceCode = priceCode;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

        DataSet dsResult = HotelInfoBP.GetPlanListByIndiscriminatelyRateCode(_hotelinfoEntity).QueryResult;

        return dsResult;
    }

    /// <summary>
    /// 生成房型列表 -- Oracle
    /// </summary>
    /// <param name="strHotelID"></param>
    public static DataSet BindBalanceRoomList(string strHotelID)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelID = strHotelID;//酒店ID
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetBalanceRoomListByHotel(_hotelinfoEntity).QueryResult;
        return dsResult;
    }

    /// <summary>
    /// 自动拼取时间段  --  业务逻辑 拼装
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    public DataTable GetDate(string startDate, string endDate)
    {
        DataTable TimeList = new DataTable();
        TimeList.Columns.Add(new DataColumn("time"));
        TimeList.Columns.Add(new DataColumn("timeMD"));
        TimeSpan ts = DateTime.Parse(endDate) - DateTime.Parse(startDate);
        int days = ts.Days;
        for (int i = 0; i <= days; i++)
        {
            DataRow drRow = TimeList.NewRow();
            drRow["time"] = DateTime.Parse(startDate).AddDays(i).ToShortDateString();
            drRow["timeMD"] = DateTime.Parse(startDate).AddDays(i).Month.ToString() + "-" + DateTime.Parse(startDate).AddDays(i).Day.ToString();
            TimeList.Rows.Add(drRow);
        }
        return TimeList;
    }

    /// <summary>
    /// 页面加载  绑定RadioButtonList
    /// </summary>
    private void bindBookStatusList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("TEXT");
        dt.Columns.Add("VALUE");
        DataRow drRow = dt.NewRow();
        drRow["TEXT"] = "不限制";
        drRow["VALUE"] = "";
        dt.Rows.Add(drRow);
        drRow = dt.NewRow();
        drRow["TEXT"] = "14点";
        drRow["VALUE"] = "111110000000001111111111";
        dt.Rows.Add(drRow);
        drRow = dt.NewRow();
        drRow["TEXT"] = "18点";
        drRow["VALUE"] = "111110000000000000111111";
        dt.Rows.Add(drRow);
        radioListBookStatus.DataSource = dt;
        radioListBookStatus.DataTextField = "TEXT";
        radioListBookStatus.DataValueField = "VALUE";
        radioListBookStatus.DataBind();
        radioListBookStatus.SelectedIndex = 0;
    }

    /// <summary>
    /// 判断是否有需要查询上一个或者下一个酒店的信息
    /// </summary>
    /// <returns></returns>
    private void judgeLastOrNext(int selectIndex)
    {
        this.HidJudgeLast.Value = "";//上一个 
        this.HidJudgeNext.Value = "";//下一个 
        this.HidLastHotelSelectName.Value = "";
        this.HidNextHotelSelectName.Value = "";
        if (selectIndex == 0)
        {
            //只需要判断下一个是否需要查询
            if (gridHotelList.Rows[selectIndex + 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0") && gridHotelList.Rows[selectIndex + 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
            {
                this.HidJudgeNext.Value = "Next";

                this.HidNextHotelSelectName.Value = gridHotelList.DataKeys[selectIndex + 1].Values[0].ToString();
            }
        }
        else
        {
            //判断上一个  下一个  是否需要查询
            if (selectIndex > 0)
            {
                if (gridHotelList.Rows[selectIndex - 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0") && gridHotelList.Rows[selectIndex - 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                {
                    this.HidJudgeLast.Value = "Last";

                    this.HidLastHotelSelectName.Value = gridHotelList.DataKeys[selectIndex - 1].Values[0].ToString();
                }
            }
            if (selectIndex < gridHotelList.Rows.Count)
            {
                if (gridHotelList.Rows[selectIndex + 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0") && gridHotelList.Rows[selectIndex + 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                {
                    this.HidJudgeNext.Value = "Next";

                    this.HidNextHotelSelectName.Value = gridHotelList.DataKeys[selectIndex + 1].Values[0].ToString();
                }
            }
        }
    }

    /// <summary>
    /// 判断缓存的DataTable里面是包含该酒店
    /// </summary>
    private void contrastTable(string hotelID)
    {
        //判断缓存的DataTable里面是包含该酒店
        //判断是否有房型   是否有计划 
        #region
        if (lastRowLmbar != null && lastRowLmbar.Rows.Count > 0)//上一个LMBAR计划
        {
            rowLmbar = lastRowLmbar.Select("HOTELID='" + hotelID + "'");
        }
        if (lastRowLmbar2 != null && lastRowLmbar2.Rows.Count > 0)//上一个LMBAR2计划
        {
            rowLmbar2 = lastRowLmbar2.Select("HOTELID='" + hotelID + "'");
        }
        if (lastDrRoomListLMBAR != null && lastDrRoomListLMBAR.Rows.Count > 0)//上一个LMBAR房型
        {
            drRoomListLMBAR = lastDrRoomListLMBAR.Select("PRICECODE='LMBAR'");
            this.HidLmbarRoomCode.Value = "";
            for (int i = 0; i < drRoomListLMBAR.Length; i++)
            {
                this.HidLmbarRoomCode.Value += drRoomListLMBAR[i]["ROOMCODE"].ToString() + ",";
            }
            this.HidLmbarRoomCode.Value = this.HidLmbarRoomCode.Value.TrimEnd(',');
        }
        if (lastDrRoomListLMBAR2 != null && lastDrRoomListLMBAR2.Rows.Count > 0)//上一个LMBAR2房型
        {
            drRoomListLMBAR2 = lastDrRoomListLMBAR2.Select("PRICECODE='LMBAR2'");
            this.HidLmbar2RoomCode.Value = "";
            for (int i = 0; i < drRoomListLMBAR2.Length; i++)
            {
                this.HidLmbar2RoomCode.Value += drRoomListLMBAR2[i]["ROOMCODE"].ToString() + ",";
            }
            this.HidLmbar2RoomCode.Value = this.HidLmbar2RoomCode.Value.TrimEnd(',');
        }
        #endregion
        #region
        if (nextRowLmbar != null && nextRowLmbar.Rows.Count > 0)//下一个LMBAR计划
        {
            rowLmbar = nextRowLmbar.Select("HOTELID='" + hotelID + "'");
        }
        if (nextRowLmbar2 != null && nextRowLmbar2.Rows.Count > 0)//下一个LMBAR2计划
        {
            rowLmbar2 = nextRowLmbar2.Select("HOTELID='" + hotelID + "'");
        }
        if (nextDrRoomListLMBAR != null && nextDrRoomListLMBAR.Rows.Count > 0)//下一个LMBAR房型
        {
            drRoomListLMBAR = nextDrRoomListLMBAR.Select("PRICECODE='LMBAR'");
            this.HidLmbarRoomCode.Value = "";
            for (int i = 0; i < drRoomListLMBAR.Length; i++)
            {
                this.HidLmbarRoomCode.Value += drRoomListLMBAR[i]["ROOMCODE"].ToString() + ",";
            }
            this.HidLmbarRoomCode.Value = this.HidLmbarRoomCode.Value.TrimEnd(',');
        }
        if (nextDrRoomListLMBAR2 != null && nextDrRoomListLMBAR2.Rows.Count > 0)//下一个LMBAR2房型
        {
            drRoomListLMBAR2 = nextDrRoomListLMBAR2.Select("PRICECODE='LMBAR2'");
            this.HidLmbar2RoomCode.Value = "";
            for (int i = 0; i < drRoomListLMBAR2.Length; i++)
            {
                this.HidLmbar2RoomCode.Value += drRoomListLMBAR2[i]["ROOMCODE"].ToString() + ",";
            }
            this.HidLmbar2RoomCode.Value = this.HidLmbar2RoomCode.Value.TrimEnd(',');
        }
        #endregion
    }
}