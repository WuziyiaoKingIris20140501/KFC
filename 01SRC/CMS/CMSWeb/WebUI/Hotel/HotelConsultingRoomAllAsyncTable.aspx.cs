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

public partial class WebUI_Hotel_HotelConsultingRoomAllAsyncTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.HidSelIndexOld.Value = "";
            this.HidIsBackstage.Value = "1";
            this.gridHotelList.ShowHeader = false;

            bindBookStatusList();

            //设置计划时间段
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
            //设置默认的房控人员
            if (String.IsNullOrEmpty(this.hidSelectSalesID.Value.Trim()))
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "setsalesroomKeys", "SetSalesRoom('" + UserSession.Current.UserAccount.ToLower() + "');", true);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "setsalesroomKeys", "SetSalesRoom('" + this.hidSelectSalesID.Value.Trim() + "');", true);
            }
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
        messageContent.InnerHtml = "";

        DataTable dtResult = new DataTable();
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        if (!string.IsNullOrEmpty(this.hidSelectHotel.Value)) {
            if (!string.IsNullOrEmpty(this.hidSelectHotel.Value))
            {
                if (!hidSelectHotel.Value.Trim().Contains("[") || !hidSelectHotel.Value.Trim().Contains("]"))
                {
                    messageContent.InnerHtml = "查询失败，选择酒店不合法，请修改！";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.GetType(), "updateScript", "BtnCompleteStyle();", true);
                    return;
                }
            }
            #region
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setsalesroomKeysOne", "SetSalesRoom('')", true);
            hotelInfoDBEntity.HotelID = this.hidSelectHotel.Value == "" ? "" : this.hidSelectHotel.Value.Substring((this.hidSelectHotel.Value.IndexOf('[') + 1), (this.hidSelectHotel.Value.IndexOf(']') - 1));//"";//酒店ID
            hotelInfoDBEntity.Type = DropDownList2.SelectedValue;

            hotelInfoDBEntity.EffectDate = this.radioListBookStatus.SelectedValue.Trim();

            hotelInfoDBEntity.BalValue = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["FilterJinJiangHotels"])) ? "" : ConfigurationManager.AppSettings["FilterJinJiangHotels"].ToString().Trim() + ",";
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetConsultRoomHotelRoomListByProp(_hotelinfoEntity).QueryResult.Tables[0];

            dtResult.Columns.Add("RowOldColor", typeof(string));
            dtResult.Columns.Add("ColOldColor", typeof(string));
            dtResult.Columns.Add("RowNewColor", typeof(string));
            dtResult.Columns.Add("ColNewColor", typeof(string));

            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                dtResult.Rows[i]["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                dtResult.Rows[i]["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                dtResult.Rows[i]["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();

                if (i % 2 == 0)
                {
                    dtResult.Rows[i]["RowOldColor"] = "#f6f6f6";
                    dtResult.Rows[i]["ColOldColor"] = "#ECECEC";
                    dtResult.Rows[i]["RowNewColor"] = "#f6f6f6";
                    dtResult.Rows[i]["ColNewColor"] = "#ECECEC";
                }
                else
                {
                    dtResult.Rows[i]["RowOldColor"] = "#ffffff";
                    dtResult.Rows[i]["ColOldColor"] = "#ECECEC";
                    dtResult.Rows[i]["RowNewColor"] = "#ffffff";
                    dtResult.Rows[i]["ColNewColor"] = "#ECECEC";
                }
            }
            #endregion
        }
        else if (!string.IsNullOrEmpty(this.hidSelectCity.Value) || !string.IsNullOrEmpty(this.hidSelectBussiness.Value))
        {
            #region
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
            #region
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setsalesroomKeysOne", "SetSalesRoom('')", true);
            hotelInfoDBEntity.City = this.hidSelectCity.Value == "" ? "" : this.hidSelectCity.Value.Substring((this.hidSelectCity.Value.IndexOf('[') + 1), (this.hidSelectCity.Value.IndexOf(']') - 1)); //"";//城市ID 
            hotelInfoDBEntity.HotelID = this.hidSelectHotel.Value == "" ? "" : this.hidSelectHotel.Value.Substring((this.hidSelectHotel.Value.IndexOf('[') + 1), (this.hidSelectHotel.Value.IndexOf(']') - 1));//"";//酒店ID
            hotelInfoDBEntity.Bussiness = this.hidSelectBussiness.Value == "" ? "" : this.hidSelectBussiness.Value.Substring((this.hidSelectBussiness.Value.IndexOf('[') + 1), (this.hidSelectBussiness.Value.IndexOf(']') - 1));//"";//商圈ID
            hotelInfoDBEntity.Type = DropDownList2.SelectedValue;

            hotelInfoDBEntity.EffectDate = this.radioListBookStatus.SelectedValue.Trim();

            hotelInfoDBEntity.BalValue = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["FilterJinJiangHotels"])) ? "" : ConfigurationManager.AppSettings["FilterJinJiangHotels"].ToString().Trim() + ",";
            _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
            dtResult = HotelInfoBP.GetConsultRoomHotelRoomListByHotel(_hotelinfoEntity).QueryResult.Tables[0];

            dtResult.Columns.Add("RowOldColor", typeof(string));
            dtResult.Columns.Add("ColOldColor", typeof(string));
            dtResult.Columns.Add("RowNewColor", typeof(string));
            dtResult.Columns.Add("ColNewColor", typeof(string));

            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                dtResult.Rows[i]["EXLinkMan"] = dtResult.Rows[i]["EXLinkMan"].ToString() == "" ? "" : dtResult.Rows[i]["EXLinkMan"].ToString().Split('|')[0].ToString();
                dtResult.Rows[i]["EXLinkTel"] = dtResult.Rows[i]["EXLinkTel"].ToString() == "" ? "" : "(" + dtResult.Rows[i]["EXLinkTel"].ToString().Split('|')[0].ToString() + ")";
                dtResult.Rows[i]["EXRemark"] = dtResult.Rows[i]["EXRemark"].ToString() == "" ? "" : dtResult.Rows[i]["EXRemark"].ToString().Split('|')[0].ToString();

                if (i % 2 == 0)
                {
                    dtResult.Rows[i]["RowOldColor"] = "#f6f6f6";
                    dtResult.Rows[i]["ColOldColor"] = "#ECECEC";
                    dtResult.Rows[i]["RowNewColor"] = "#f6f6f6";
                    dtResult.Rows[i]["ColNewColor"] = "#ECECEC";
                }
                else
                {
                    dtResult.Rows[i]["RowOldColor"] = "#ffffff";
                    dtResult.Rows[i]["ColOldColor"] = "#ECECEC";
                    dtResult.Rows[i]["RowNewColor"] = "#ffffff";
                    dtResult.Rows[i]["ColNewColor"] = "#ECECEC";
                }
            }
            #endregion
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
                dtNewResult.Columns.Add("EXMODE");
                dtNewResult.Columns.Add("RowOldColor");
                dtNewResult.Columns.Add("ColOldColor");
                dtNewResult.Columns.Add("RowNewColor");
                dtNewResult.Columns.Add("ColNewColor");

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
                                        dr["EXMODE"] = dtResult.Rows[i]["EXMODE"];
                                        if (i % 2 == 0)
                                        {
                                            dr["RowOldColor"] = "#f6f6f6";
                                            dr["ColOldColor"] = "#ECECEC";
                                            dr["RowNewColor"] = "#f6f6f6";
                                            dr["ColNewColor"] = "#ECECEC";
                                        }
                                        else
                                        {
                                            dr["RowOldColor"] = "#ffffff";
                                            dr["ColOldColor"] = "#ECECEC";
                                            dr["RowNewColor"] = "#ffffff";
                                            dr["ColNewColor"] = "#ECECEC";
                                        }
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
                                    dr["EXMODE"] = dtResult.Rows[i]["EXMODE"];
                                    if (i % 2 == 0)
                                    {
                                        dr["RowOldColor"] = "#f6f6f6";
                                        dr["ColOldColor"] = "#ECECEC";
                                        dr["RowNewColor"] = "#f6f6f6";
                                        dr["ColNewColor"] = "#ECECEC";
                                    }
                                    else
                                    {
                                        dr["RowOldColor"] = "#ffffff";
                                        dr["ColOldColor"] = "#ECECEC";
                                        dr["RowNewColor"] = "#ffffff";
                                        dr["ColNewColor"] = "#ECECEC";
                                    }
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
                                dr["EXMODE"] = dtResult.Rows[i]["EXMODE"];
                                if (i % 2 == 0)
                                {
                                    dr["RowOldColor"] = "#f6f6f6";
                                    dr["ColOldColor"] = "#ECECEC";
                                    dr["RowNewColor"] = "#f6f6f6";
                                    dr["ColNewColor"] = "#ECECEC";
                                }
                                else
                                {
                                    dr["RowOldColor"] = "#ffffff";
                                    dr["ColOldColor"] = "#ECECEC";
                                    dr["RowNewColor"] = "#ffffff";
                                    dr["ColNewColor"] = "#ECECEC";
                                }
                                dtNewResult.Rows.Add(dr);
                            }
                            #endregion
                        }
                    }
                }
                for (int i = 0; i < dtNewResult.Rows.Count; i++)
                {
                    if (dtNewResult.Rows[i]["EXMODE"].ToString() == "3")
                    {
                        dtNewResult.Rows.Remove(dtNewResult.Rows[i]);
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
                   // if (DateTime.Parse(dtConsultResult.Rows[j]["CreateTime"].ToString()).Day == System.DateTime.Now.Day)
                    if (DateTime.Parse(dtConsultResult.Rows[j]["PlanDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        if (DateTime.Parse(dtConsultResult.Rows[j]["CreateTime"].ToString()).Hour >= 18)
                        {
                            gridHotelList.Rows[i].Cells[6].Text = "#FF6666";
                            gridHotelList.Rows[i].Cells[7].Text = "#CD5C5C";
                            gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                            ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[i].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#CD5C5C");
                        }
                        else
                        {
                            gridHotelList.Rows[i].Cells[6].Text = "#80c0a0";
                            gridHotelList.Rows[i].Cells[7].Text = "#70A88C";
                            gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#80c0a0");
                            ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[i].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                        }
                        operandNum += 1;
                    }
                }
            }
            this.HiaAllHotelID.Value += gridHotelList.DataKeys[i].Values[0].ToString().Trim() + "&";
        }
        #endregion
        this.UpdatePanel4.Update();
        this.operandNum.InnerText = operandNum.ToString();
        this.countNum.InnerText = dtResult.Rows.Count.ToString();
        if (dtResult.Rows.Count > 0)//当查询有值时，自动触发单个酒店详情事件 
        {
            this.HidIsAsync.Value = "true";
            this.HidSelIndex.Value = "0";
            btnSingleHotel_Click(null, null);
        }
        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keys", "BtnCompleteStyle();", true);
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
            e.Row.Cells[4].Attributes.Add("style", "display:none");
            e.Row.Cells[5].Attributes.Add("style", "display:none");
            e.Row.Cells[6].Attributes.Add("style", "display:none");
            e.Row.Cells[7].Attributes.Add("style", "display:none");
            e.Row.Cells[8].Attributes.Add("style", "display:none");

            int selIndex = e.Row.RowIndex;
            //BackPropName(酒店名称)   PROP(酒店ID)   当前索引   CITYID（城市ID）   EXLinkMan（酒店执行联系人）   EXLinkTel（酒店执行电话）    EXRemark（酒店执行备注）
            e.Row.Attributes.Add("OnClick", "ClickEvent('" + gridHotelList.DataKeys[e.Row.RowIndex].Values[10].ToString() + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "','" + selIndex + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[1].ToString() + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[7].ToString() + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[8].ToString() + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[9].ToString() + "','true','false','true')");
        }
    }

    #endregion

    #region  单个酒店的单机事件
    /// <summary>
    /// 单个酒店的单机事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSingleHotel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.HidSelIndexOld.Value))
        {
            string rowOldColor = gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].Cells[6].Text;
            string colOldColor = gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].Cells[7].Text;

            gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].BackColor = System.Drawing.ColorTranslator.FromHtml(rowOldColor);
            ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml(colOldColor);
        }

        this.HidPcode.Value = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[10].ToString().Replace("null", "");
        this.spanHotelInfo.InnerHtml = "[" + gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[0].ToString().Replace("null", "") + "]&nbsp; - &nbsp;" + gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[10].ToString().Replace("null", "");

        judgeLastOrNext(this.HidSelIndex.Value);

        DivLastOrNext.Attributes.Add("style", "float: right; vertical-align: super; width: 100%;display: block;");

        this.HotelEXLinkMan_span.Text = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[7].ToString().Replace("(", "").Replace(")", "");
        this.HotelEXLinkMan_txt.Text = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[7].ToString().Replace("(", "").Replace(")", "");
        this.HotelEXLinkTel_span.Text = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[8].ToString().Replace("(", "").Replace(")", "");
        this.HotelEXLinkTel_txt.Text = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[8].ToString().Replace("(", "").Replace(")", "");
        this.HotelEXLinkRemark_txt.InnerHtml = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[9].ToString().Replace("(", "").Replace(")", "");
        this.HotelEXLinkRemark_span.InnerHtml = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[9].ToString().Replace("(", "").Replace(")", "");

        this.HidLinkDetails.Value = "酒店联系人:" + HotelEXLinkMan_txt.Text.Replace("null", "") + "   &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  电话:      " + HotelEXLinkTel_txt.Text.Replace("null", "");

        this.HidCityID.Value = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[1].ToString();
        this.HidPid.Value = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[0].ToString();


        string hotelSelectName = gridHotelList.DataKeys[int.Parse(this.HidSelIndex.Value)].Values[0].ToString();
        string startDate = this.planStartDate.Value;//开始时间
        string endDate = this.planEndDate.Value;//结束时间

        #region
        DataTable dtPlan = GetBindLmbarPlanList(startDate, endDate, hotelSelectName).Tables[0];//总计划   
        DataRow[] rowLmbar2 = dtPlan.Select("RATECODE='LMBAR2'"); //LMBAR2计划
        DataTable dtPlanLmbar2 = dtPlan.Clone();
        for (int i = 0; i < rowLmbar2.Length; i++)
        {
            dtPlanLmbar2.ImportRow(rowLmbar2[i]);
        }
        this.HidHotelPlanListLmbar2.Value = ToJson(dtPlanLmbar2);
        DataRow[] rowLmbar = dtPlan.Select("RATECODE='LMBAR'"); //LMBAR计划
        DataTable dtPlanLmbar = dtPlan.Clone();
        for (int i = 0; i < rowLmbar.Length; i++)
        {
            dtPlanLmbar.ImportRow(rowLmbar[i]);
        }
        this.HidHotelPlanListLmbar.Value = ToJson(dtPlanLmbar);

        DataTable dtRoomList = BindBalanceRoomList(hotelSelectName).Tables[0];//LMBAR2房型CODE
        DataRow[] drRoomListLMBAR2 = dtRoomList.Select("PRICECODE ='LMBAR2'");
        this.HidHotelRoomListLMBAR2Code.Value = "";
        for (int i = 0; i < drRoomListLMBAR2.Length; i++)
        {
            this.HidHotelRoomListLMBAR2Code.Value += drRoomListLMBAR2[i]["ROOMCODE"].ToString() + ",";
        }
        this.HidHotelRoomListLMBAR2Code.Value = this.HidHotelRoomListLMBAR2Code.Value.TrimEnd(',');
        DataRow[] drRoomListLMBAR = dtRoomList.Select("PRICECODE ='LMBAR'");
        this.HidHotelRoomListLMBARCode.Value = "";
        for (int i = 0; i < drRoomListLMBAR.Length; i++)
        {
            this.HidHotelRoomListLMBARCode.Value += drRoomListLMBAR[i]["ROOMCODE"].ToString() + ",";
        }
        this.HidHotelRoomListLMBARCode.Value = this.HidHotelRoomListLMBARCode.Value.TrimEnd(',');


        DataTable dtTime = GetDate(startDate, endDate);
        #endregion

        ScriptManager.RegisterClientScriptBlock(this.gridHotelList, this.GetType(), "", "GetResultFromServer();", true);

        gridHotelList.SelectedIndex = -1;
        this.UpdatePanel4.Update();

        AssemblyDiv(rowLmbar2, rowLmbar, drRoomListLMBAR2, drRoomListLMBAR, dtTime);
        this.UpdatePanel5.Update();

        btnClickckHotel_Click(null, null);//设置当前选中酒店颜色  还原上一个酒店的颜色


        if (this.HidSelIndex.Value == "0")
        {
            if (this.HidIsBackstage.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel9, this.GetType(), "alertAsync" + System.DateTime.Now.Millisecond.ToString(), "showA();", true);
            }
        }
        else if (this.HidIsBackstage.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel9, this.GetType(), "alertAsync" + System.DateTime.Now.Millisecond.ToString(), "showA();", true);
        }
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "keyOvers", "BtnCompleteStyle();", true);
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
            //lblContactDetails.Text = "LM酒店负责人:" + dsResult.Tables[0].Rows[0]["DISPNM"].ToString().Replace("null", "") + " &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;    电话:      " + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Replace("null", "");
            //this.HidContactDetails.Value = "LM酒店负责人:" + dsResult.Tables[0].Rows[0]["DISPNM"].ToString().Replace("null", "") + "  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;     电话:      " + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Replace("null", "");
            this.HidAlertContactDetails.Value = "LM酒店负责人:" + dsResult.Tables[0].Rows[0]["DISPNM"].ToString().Replace("null", "") + "     电话:      " + dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Replace("null", "");
            ScriptManager.RegisterStartupScript(this.UpdatePanel7, this.GetType(), "msgAlertY", "alert('" + this.HidAlertContactDetails.Value + "')", true);

        }
        else
        {
            //lblContactDetails.Text = "LM酒店负责人:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;    电话:      ";
            //this.HidContactDetails.Value = "LM酒店负责人:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;    电话:      ";
            this.HidAlertContactDetails.Value = "LM酒店负责人:    电话:      ";
            ScriptManager.RegisterStartupScript(this.UpdatePanel7, this.GetType(), "msgAlertN", "alert('" + this.HidAlertContactDetails.Value + "')", true);
        }
    }
    #endregion

    #region  上一个   下一个
    /// <summary>
    /// 控制上一个   下一个酒店 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnLastOrNextHotel_Click(object sender, EventArgs e)
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

    #region AJAX异步加载操作历史以及DataTable 转换 JSON
    /// <summary>
    /// 获取上一个 或者 下一个酒店的房型 计划信息
    /// </summary>
    /// <param name="JudgeLast"></param>
    /// <param name="JudgeNext"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="LastHotelSelectName"></param>
    /// <param name="NextHotelSelectName"></param>
    [WebMethod]
    public static string GetNextOrLastHotelDetails(string JudgeLast, string JudgeNext, string startDate, string endDate, string LastHotelSelectName, string NextHotelSelectName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{ \"siteLast\": {");
        string LastHotelSelectID = "";
        string LastHotelPlanListLmbar2 = "";
        string LastHotelPlanListLmbar = "";
        string LastHotelRoomListLMBAR2Code = "";
        string LastHotelRoomListLMBARCode = "";

        if (JudgeLast == "Last")
        {
            sb.Append("\"site\":\"Last\",");
            LastHotelSelectID = LastHotelSelectName;

            //上一个 
            #region
            DataTable dtPlan = GetBindLmbarPlanList(startDate, endDate, LastHotelSelectName).Tables[0];//总计划
            DataRow[] rowLabar2 = dtPlan.Select("RATECODE='LMBAR2'"); //上一个LMBAR2计划
            DataTable dtPlanLmbar2 = dtPlan.Clone();
            for (int i = 0; i < rowLabar2.Length; i++)
            {
                dtPlanLmbar2.ImportRow(rowLabar2[i]);
            }
            if (dtPlanLmbar2 != null && dtPlanLmbar2.Rows.Count > 0)
                LastHotelPlanListLmbar2 = ToJson(dtPlanLmbar2);

            DataRow[] rowLabar = dtPlan.Select("RATECODE='LMBAR'"); //上一个LMBAR计划
            DataTable dtPlanLmbar = dtPlan.Clone();
            for (int i = 0; i < rowLabar.Length; i++)
            {
                dtPlanLmbar.ImportRow(rowLabar[i]);
            }
            if (dtPlanLmbar != null && dtPlanLmbar.Rows.Count > 0)
                LastHotelPlanListLmbar = ToJson(dtPlanLmbar);

            #endregion
            #region
            DataTable dtRoomList = BindBalanceRoomList(LastHotelSelectName).Tables[0];//上一个 LMBAR2房型CODE
            DataRow[] roomLmbar2 = dtRoomList.Select("PRICECODE ='LMBAR2'");
            DataTable lastDrRoomListLMBAR2 = dtRoomList.Clone();
            for (int i = 0; i < roomLmbar2.Length; i++)
            {
                lastDrRoomListLMBAR2.ImportRow(roomLmbar2[i]);
            }
            if (lastDrRoomListLMBAR2 != null && lastDrRoomListLMBAR2.Rows.Count > 0)
                LastHotelRoomListLMBAR2Code = ToJson(lastDrRoomListLMBAR2);

            DataRow[] roomLmbar = dtRoomList.Select("PRICECODE ='LMBAR'");//上一个 LMBAR房型CODE
            DataTable lastDrRoomListLMBAR = dtRoomList.Clone();
            for (int i = 0; i < roomLmbar.Length; i++)
            {
                lastDrRoomListLMBAR.ImportRow(roomLmbar[i]);
            }
            if (lastDrRoomListLMBAR != null && lastDrRoomListLMBAR.Rows.Count > 0)
                LastHotelRoomListLMBARCode = ToJson(lastDrRoomListLMBAR);
            #endregion
        }
        sb.Append("\"LastHotelID\":\"" + LastHotelSelectID + "\",");
        if (LastHotelPlanListLmbar2 == "")
            sb.Append("\"LastHotelPlanListLmbar2\":\"" + LastHotelPlanListLmbar2 + "\",");
        else
            sb.Append("\"LastHotelPlanListLmbar2\":" + LastHotelPlanListLmbar2 + ",");
        if (LastHotelPlanListLmbar == "")
            sb.Append("\"LastHotelPlanListLmbar\":\"" + LastHotelPlanListLmbar + "\",");
        else
            sb.Append("\"LastHotelPlanListLmbar\":" + LastHotelPlanListLmbar + ",");
        if (LastHotelRoomListLMBAR2Code == "")
            sb.Append("\"LastHotelRoomListLMBAR2Code\":\"" + LastHotelRoomListLMBAR2Code + "\",");
        else
            sb.Append("\"LastHotelRoomListLMBAR2Code\":" + LastHotelRoomListLMBAR2Code + ",");
        if (LastHotelRoomListLMBARCode == "")
            sb.Append("\"LastHotelRoomListLMBARCode\":\"" + LastHotelRoomListLMBARCode + "\"");
        else
            sb.Append("\"LastHotelRoomListLMBARCode\":" + LastHotelRoomListLMBARCode + "");
        sb.Append("},");
        sb.Append("\"siteNext\": {");

        string NextHotelSelectID = "";
        string NextHotelPlanListLmbar2 = "";
        string NextHotelPlanListLmbar = "";
        string NextHotelRoomListLMBAR2Code = "";
        string NextHotelRoomListLMBARCode = "";
        if (JudgeNext == "Next")
        {
            sb.Append("\"site\":\"Next\",");
            NextHotelSelectID = NextHotelSelectName;

            //下一个 
            #region
            DataTable dtPlan = GetBindLmbarPlanList(startDate, endDate, NextHotelSelectName).Tables[0];//总计划
            DataRow[] rowLabar2 = dtPlan.Select("RATECODE='LMBAR2'"); //下一个LMBAR2计划
            DataTable nextRowLmbar2 = dtPlan.Clone();
            for (int i = 0; i < rowLabar2.Length; i++)
            {
                nextRowLmbar2.ImportRow(rowLabar2[i]);
            }
            if (nextRowLmbar2 != null && nextRowLmbar2.Rows.Count > 0)
                NextHotelPlanListLmbar2 = ToJson(nextRowLmbar2);
            DataRow[] rowLabar = dtPlan.Select("RATECODE='LMBAR'"); //下一个LMBAR计划
            DataTable nextRowLmbar = dtPlan.Clone();
            for (int i = 0; i < rowLabar.Length; i++)
            {
                nextRowLmbar.ImportRow(rowLabar[i]);
            }
            if (nextRowLmbar != null && nextRowLmbar.Rows.Count > 0)
                NextHotelPlanListLmbar = ToJson(nextRowLmbar);
            #endregion
            #region
            DataTable dtRoomList = BindBalanceRoomList(NextHotelSelectName).Tables[0];//下一个 LMBAR2房型CODE
            DataRow[] roomLmbar2 = dtRoomList.Select("PRICECODE ='LMBAR2'");
            DataTable nextDrRoomListLMBAR2 = dtRoomList.Clone();
            for (int i = 0; i < roomLmbar2.Length; i++)
            {
                nextDrRoomListLMBAR2.ImportRow(roomLmbar2[i]);
            }
            if (nextDrRoomListLMBAR2 != null && nextDrRoomListLMBAR2.Rows.Count > 0)
                NextHotelRoomListLMBAR2Code = ToJson(nextDrRoomListLMBAR2);
            DataRow[] roomLmbar = dtRoomList.Select("PRICECODE ='LMBAR'");//下一个 LMBAR房型CODE
            DataTable nextDrRoomListLMBAR = dtRoomList.Clone();
            for (int i = 0; i < roomLmbar.Length; i++)
            {
                nextDrRoomListLMBAR.ImportRow(roomLmbar[i]);
            }
            if (nextDrRoomListLMBAR != null && nextDrRoomListLMBAR.Rows.Count > 0)
                NextHotelRoomListLMBARCode = ToJson(nextDrRoomListLMBAR);
            #endregion
        }
        sb.Append("\"NextHotelID\":\"" + NextHotelSelectID + "\",");
        if (NextHotelPlanListLmbar2 == "")
            sb.Append("\"NextHotelPlanListLmbar2\":\"" + NextHotelPlanListLmbar2 + "\",");
        else
            sb.Append("\"NextHotelPlanListLmbar2\":" + NextHotelPlanListLmbar2 + ",");

        if (NextHotelPlanListLmbar == "")
            sb.Append("\"NextHotelPlanListLmbar\":\"" + NextHotelPlanListLmbar + "\",");
        else
            sb.Append("\"NextHotelPlanListLmbar\":" + NextHotelPlanListLmbar + ",");

        if (NextHotelRoomListLMBAR2Code == "")
            sb.Append("\"NextHotelRoomListLMBAR2Code\":\"" + NextHotelRoomListLMBAR2Code + "\",");
        else
            sb.Append("\"NextHotelRoomListLMBAR2Code\":" + NextHotelRoomListLMBAR2Code + ",");

        if (NextHotelRoomListLMBARCode == "")
            sb.Append("\"NextHotelRoomListLMBARCode\":\"" + NextHotelRoomListLMBARCode + "\"");
        else
            sb.Append("\"NextHotelRoomListLMBARCode\":" + NextHotelRoomListLMBARCode + "");

        DataTable dtTime = GetDate(startDate, endDate);
        sb.Append("},");
        sb.Append("\"dTTime\": {");
        string strTime = ToJson(dtTime);
        sb.Append("\"dTTime\":" + strTime + "");
        sb.Append("}}");

        return sb.ToString().TrimStart('"').TrimEnd('"');
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
        ScriptManager.RegisterStartupScript(this.UpdatePanel11, this.GetType(), "keyinvokeCloseRemark", "invokeCloseRemark();", true);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyclosebtn123", "aaa();", true);
        ScriptManager.RegisterStartupScript(this.UpdatePanel11, this.GetType(), "keyclosebtn", "BtnCompleteStyle();", true);
        this.UpdatePanel5.Update();
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

        #region
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

            DataTable dtPlan = GetBindLmbarPlanList(this.planStartDate.Value, this.planEndDate.Value, this.HidPid.Value).Tables[0];//总计划
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
                    for (int l = 0; l < this.HidLastHotelRoomListLMBAR2.Value.Split(',').Length; l++)
                    {
                        DataRow[] rowsLmbar2 = dtPlanLMBAR2.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + this.HidLastHotelRoomListLMBAR2.Value.Split(',')[l].ToString() + "'");
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
                    for (int l = 0; l < this.HidLastHotelRoomListLMBAR.Value.Split(',').Length; l++)
                    {
                        DataRow[] rowsLmbar = dtPlanLMBAR.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + this.HidLastHotelRoomListLMBAR.Value.Split(',')[l].ToString() + "'");
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
                        appcontentDBEntity.Lmbar2RoomCode = this.HidLastHotelRoomListLMBAR2.Value;
                        appcontentDBEntity.LmbarRoomCode = this.HidLastHotelRoomListLMBAR.Value;
                        appcontentDBEntity.TypeID = status == "true" ? "3" : "2";// "2";//type:1 满房、2 关房、3 开房
                        appcontentDBEntity.UpdateUser = UserSession.Current.UserAccount;
                        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

                        _appcontentEntity = HotelInfoBP.BatchUpdatePlan(_appcontentEntity);

                        _appcontentEntity.APPContentDBEntity.Clear();
                    }
                }
            }
        }
        #endregion

        int SelectedIndex = int.Parse(this.HidSelIndex.Value);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyclosebtn", "BtnCompleteStyle();", true);

        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "clickbtnSingle", "ClickEvent('" + this.HidPcode.Value + "','" + this.HidPid.Value + "','" + SelectedIndex + "','" + this.HidCityID.Value + "','" + this.HidHotelEXLinkMan.Value + "','" + this.HidHotelEXLinkTel.Value + "','" + this.HidHotelEXLinkRemark.Value + "','false','true','false');", true);

        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "scrollReset", "GetResultFromServer();", true);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel11, this.GetType(), "keyclosebtn", "BtnCompleteStyle();", true);
        //btnSelect_Click(null, null);
        btnSingleHotel_Click(null, null);

        if (IsFlag)
        {
            if (DateTime.Now.Hour >= 18)
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                {
                    gridHotelList.Rows[SelectedIndex].Cells[6].Text = "#FF6666";
                    gridHotelList.Rows[SelectedIndex].Cells[7].Text = "#CD5C5C";
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#CD5C5C");
                }
            }
            else
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0"))
                {
                    gridHotelList.Rows[SelectedIndex].Cells[6].Text = "#80c0a0";
                    gridHotelList.Rows[SelectedIndex].Cells[7].Text = "#70A88C";
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

        #region
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
            DataTable dtPlan = GetBindLmbarPlanList(this.planStartDate.Value, this.planEndDate.Value, this.HidPid.Value).Tables[0];//总计划
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
                    //if (DateTime.Parse(datas[i].ToString()) == System.DateTime.Now)
                    if (DateTime.Parse(datas[i].ToString()).ToShortDateString() == System.DateTime.Now.ToShortDateString())
                    {
                        IsFlag = true;
                    }
                    string effDate = datas[i].ToString().Replace("/", "-");
                    #region
                    for (int l = 0; l < this.HidLastHotelRoomListLMBAR2.Value.Split(',').Length; l++)
                    {
                        DataRow[] rowsLmbar2 = dtPlanLMBAR2.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + this.HidLastHotelRoomListLMBAR2.Value.Split(',')[l].ToString() + "'");
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
                    for (int l = 0; l < this.HidLastHotelRoomListLMBAR.Value.Split(',').Length; l++)
                    {
                        DataRow[] rowsLmbar = dtPlanLMBAR.Select("EFFECTDATESTRING='" + DateTime.Parse(effDate).ToString("yyyy-MM-dd") + "' and ROOMTYPECODE='" + this.HidLastHotelRoomListLMBAR.Value.Split(',')[l].ToString() + "'");
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
                    appcontentDBEntity.Lmbar2RoomCode = this.HidLastHotelRoomListLMBAR2.Value;
                    appcontentDBEntity.LmbarRoomCode = this.HidLastHotelRoomListLMBAR.Value;
                    appcontentDBEntity.TypeID = "1";//type:1 满房、2 关房、3 开房
                    appcontentDBEntity.UpdateUser = UserSession.Current.UserAccount;
                    _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

                    _appcontentEntity = HotelInfoBP.BatchUpdatePlan(_appcontentEntity);

                    _appcontentEntity.APPContentDBEntity.Clear();
                }
            }
        }
        #endregion

        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "keyclosebtn", "BtnCompleteStyle();", true);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel11, this.GetType(), "keybtn4", "BtnCompleteStyle();", true);
        //btnSingleHotel_Click(null, null);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "scrollReset", "GetResultFromServer();", true);
        int SelectedIndex = int.Parse(this.HidSelIndex.Value);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "clickbtnSingle", "ClickEvent('" + this.HidPcode.Value + "','" + this.HidPid.Value + "','" + SelectedIndex + "','" + this.HidCityID.Value + "','false');", true);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "clickbtnSingle", "ClickEvent('" + this.HidPcode.Value + "','" + this.HidPid.Value + "','" + SelectedIndex + "','" + this.HidCityID.Value + "','" + this.HidHotelEXLinkMan.Value + "','" + this.HidHotelEXLinkTel.Value + "','" + this.HidHotelEXLinkRemark.Value + "','false','true','false');", true);
        btnSingleHotel_Click(null, null);

        if (IsFlag)
        {
            if (DateTime.Now.Hour >= 18)
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                {
                    gridHotelList.Rows[SelectedIndex].Cells[6].Text = "#FF6666";
                    gridHotelList.Rows[SelectedIndex].Cells[7].Text = "#CD5C5C";
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#CD5C5C");
                }
            }
            else
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0"))
                {
                    gridHotelList.Rows[SelectedIndex].Cells[6].Text = "#80c0a0";
                    gridHotelList.Rows[SelectedIndex].Cells[7].Text = "#70A88C";
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#80c0a0");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
            }
        }
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
        string twoPrice = this.HiddenPrice.Value;
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
        string RoomName = this.HiddenRoomName.Value;
        appcontentDBEntity.RoomName = RoomName;
        //房型Code
        string RoomCode = this.HiddenRoomCode.Value;
        appcontentDBEntity.RoomCode = RoomCode;

        bool IsFlag = false;

        //批量更新日期   开始  结束 
        string divPlanStartDate = this.divPlanStartDate.Value;
        string divPlanEndDate = this.divPlanEndDate.Value;
        //if (DateTime.Parse(divPlanStartDate) >= System.DateTime.Now || DateTime.Parse(divPlanEndDate) <= System.DateTime.Now)
        if (DateTime.Parse(divPlanStartDate) == DateTime.Parse(System.DateTime.Now.ToShortDateString()))
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
        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "clickbtnSingle", "ClickEvent('" + this.HidPcode.Value + "','" + this.HidPid.Value + "','" + SelectedIndex + "','" + this.HidCityID.Value + "','" + this.HidHotelEXLinkMan.Value + "','" + this.HidHotelEXLinkTel.Value + "','" + this.HidHotelEXLinkRemark.Value + "','false','true','false');", true);
        btnSingleHotel_Click(null, null);
        //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "scrollReset", "GetResultFromServer();", true);
        if (IsFlag)
        {
            if (DateTime.Now.Hour >= 18)
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                {
                    gridHotelList.Rows[SelectedIndex].Cells[6].Text = "#FF6666";
                    gridHotelList.Rows[SelectedIndex].Cells[7].Text = "#CD5C5C";
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6666");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#CD5C5C");
                }
            }
            else
            {
                if (gridHotelList.Rows[SelectedIndex].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0"))
                {
                    gridHotelList.Rows[SelectedIndex].Cells[6].Text = "#80c0a0";
                    gridHotelList.Rows[SelectedIndex].Cells[7].Text = "#70A88C";
                    this.operandNum.InnerText = (int.Parse(this.operandNum.InnerText) + 1).ToString();
                    gridHotelList.Rows[SelectedIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#80c0a0");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#70A88C");
                }
            }
        }
        #endregion
        ScriptManager.RegisterStartupScript(this.UpdatePanel10, this.GetType(), "keyinvokeCloseDiv", "invokeCloseDiv();", true);
        ScriptManager.RegisterStartupScript(this.UpdatePanel10, this.GetType(), "keyinvokeCloseDiv1", "BtnCompleteStyle();", true);
        this.UpdatePanel5.Update();
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

    #region   修改计划EX信息
    /// <summary>
    /// 修改备注
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditEXInfo_Click(object sender, EventArgs e)
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
            QueryRoomLinkMan.Append(this.HotelEXLinkMan_txt.Text.TrimStart('(').TrimEnd(')') + "|"); //查房联系人
        }

        StringBuilder QueryRoomLinkTel = new StringBuilder();
        for (int i = 0; i < 23; i++)
        {
            QueryRoomLinkTel.Append(this.HotelEXLinkTel_txt.Text.TrimStart('(').TrimEnd(')') + "|");//查房联系电话
        }

        if (ViewState["EXHotelInfo"].ToString() != "0")
        {
            #region
            hotelInfoEXDBEntity.HotelID = this.HidPid.Value;//酒店ID
            hotelInfoEXDBEntity.Type = "1";
            hotelInfoEXDBEntity.LinkMan = QueryRoomLinkMan.ToString().TrimEnd('|');
            hotelInfoEXDBEntity.LinkTel = QueryRoomLinkTel.ToString().TrimEnd('|');
            hotelInfoEXDBEntity.LinkFax = ViewState["EXLinkFax"].ToString();
            hotelInfoEXDBEntity.Remark = this.HotelEXLinkRemark_txt.InnerHtml;
            hotelInfoEXDBEntity.ExTime = ViewState["EXExTime"].ToString();
            hotelInfoEXDBEntity.ExMode = ViewState["EXExMode"].ToString();
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

    #region   页面加载  数据初始化   公共方法
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
    /// 自动拼取时间段  --  业务逻辑 拼装
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    public static DataTable GetDate(string startDate, string endDate)
    {
        DataTable TimeList = new DataTable();
        TimeList.Columns.Add(new DataColumn("time"));
        TimeList.Columns.Add(new DataColumn("timeMD"));
        TimeList.Columns.Add(new DataColumn("IsWeek"));
        TimeSpan ts = DateTime.Parse(endDate) - DateTime.Parse(startDate);
        int days = ts.Days;
        for (int i = 0; i <= days; i++)
        {
            DataRow drRow = TimeList.NewRow();
            drRow["time"] = DateTime.Parse(startDate).AddDays(i).ToShortDateString();
            drRow["timeMD"] = DateTime.Parse(startDate).AddDays(i).Month.ToString() + "-" + DateTime.Parse(startDate).AddDays(i).Day.ToString();

            if (DateTime.Parse(startDate).AddDays(i).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(startDate).AddDays(i).DayOfWeek.ToString() == "Sunday")
            {
                drRow["IsWeek"] = "true";
            }
            else
            {
                drRow["IsWeek"] = "false";
            }
            TimeList.Rows.Add(drRow);
        }
        return TimeList;
    }

    /// <summary>
    /// 根据时间段  HotelID 取计划  --  接口 
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="strHotelID"></param>
    /// <returns></returns>
    public static DataSet GetBindLmbarPlanList(string startDate, string endDate, string strHotelID)
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
    /// 获取所有的销售人员--SQL
    /// </summary>
    private DataTable GetSalesManagerList()
    {
        WebAutoCompleteBP webBP = new WebAutoCompleteBP();
        DataTable dtSales = webBP.GetWebCompleteList("sales", "", "").Tables[0];

        return dtSales;
    }

    /// <summary>
    /// 获取当天已询房的酒店列表--SQL
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
    /// 判断是否有需要查询上一个或者下一个酒店的信息
    /// </summary>
    /// <returns></returns>
    protected void judgeLastOrNext(string selectID)
    {
        if (!string.IsNullOrEmpty(selectID))
        {
            int selectIndex = int.Parse(selectID);
            this.HidJudgeLast.Value = "";//上一个 
            this.HidJudgeNext.Value = "";//下一个 
            this.HidLastHotelSelectID.Value = "";
            this.HidNextHotelSelectID.Value = "";
            if (selectIndex == 0)
            {
                //只需要判断下一个是否需要查询
                if (gridHotelList.Rows.Count > selectIndex + 1)
                {
                    if (gridHotelList.Rows[selectIndex + 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0") && gridHotelList.Rows[selectIndex + 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                    {
                        this.HidJudgeNext.Value = "Next";

                        this.HidNextHotelSelectID.Value = gridHotelList.DataKeys[selectIndex + 1].Values[0].ToString();
                    }
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

                        this.HidLastHotelSelectID.Value = gridHotelList.DataKeys[selectIndex - 1].Values[0].ToString();
                    }
                }
                if (selectIndex < gridHotelList.Rows.Count - 1)
                {
                    if (gridHotelList.Rows[selectIndex + 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#80c0a0") && gridHotelList.Rows[selectIndex + 1].BackColor != System.Drawing.ColorTranslator.FromHtml("#FF6666"))
                    {
                        this.HidJudgeNext.Value = "Next";

                        this.HidNextHotelSelectID.Value = gridHotelList.DataKeys[selectIndex + 1].Values[0].ToString();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 触发  查询上一个 还是下一个的Click事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnHotelSite_Click(object sender, EventArgs e)
    {
        btnClickckHotel_Click(null, null);
        //this.UpdatePanel8.Update();
        judgeLastOrNext(this.HidSelIndex.Value);
        ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.GetType(), "scrollReset", "GetResultFromServer();", true);
        ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.GetType(), "alertAsync" + System.DateTime.Now.Millisecond.ToString(), "showA();", true);
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
                //this.EXHotelInfo.Value = "1";
                //this.EXLinkFax.Value = rows[0]["LinkFax"].ToString();
                //this.EXExTime.Value = rows[0]["EX_Time"].ToString();
                //this.EXExMode.Value = rows[0]["EX_Mode"].ToString();
                ViewState["EXHotelInfo"] = "1";
                ViewState["EXLinkFax"] = rows[0]["LinkFax"].ToString();
                ViewState["EXExTime"] = rows[0]["EX_Time"].ToString();
                ViewState["EXExMode"] = rows[0]["EX_Mode"].ToString();
                return rows[0];
            }
            else
            {
                //this.EXHotelInfo.Value = "0";
                //this.EXLinkFax.Value = "";
                //this.EXExTime.Value = "";
                //this.EXExMode.Value = "";
                ViewState["EXHotelInfo"] = "0";
                ViewState["EXLinkFax"] = "";
                ViewState["EXExTime"] = "";
                ViewState["EXExMode"] = "";
                return null;
            }
        }
        else
        {
            //this.EXHotelInfo.Value = "0";
            //this.EXLinkFax.Value = "";
            //this.EXExTime.Value = "";
            //this.EXExMode.Value = "";
            ViewState["EXHotelInfo"] = "0";
            ViewState["EXLinkFax"] = "";
            ViewState["EXExTime"] = "";
            ViewState["EXExMode"] = "";
            return null;
        }
    }

    /// <summary>
    /// 拼装酒店计划信息
    /// </summary>
    public void AssemblyDiv(DataRow[] PlanLMBAR2, DataRow[] PlanLMBAR, DataRow[] RoomListLMABAR2, DataRow[] RoomListLMABAR, DataTable dtTime)
    {
        divMain.InnerHtml = "";

        string lmbar2W = "80px";

        string lmbar2TitleWidth = (double.Parse(RoomListLMABAR2.Length.ToString() == "0" ? "1" : RoomListLMABAR2.Length.ToString()) * (double.Parse("80"))).ToString("0.0") + "px";
        string lmbarTitleWidth = (double.Parse(RoomListLMABAR.Length.ToString() == "0" ? "1" : RoomListLMABAR.Length.ToString()) * (double.Parse("80"))).ToString("0.0") + "px";
        string sumWidth = (160.0 + (double.Parse(RoomListLMABAR2.Length.ToString() == "0" ? "1" : RoomListLMABAR2.Length.ToString()) * (double.Parse("80")) + (double.Parse(RoomListLMABAR.Length.ToString() == "0" ? "1" : RoomListLMABAR.Length.ToString()) * (double.Parse("80"))))).ToString("0.0") + "px";

        StringBuilder sb = new StringBuilder();
        sb.Append("<table width=\"" + sumWidth + "\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">");
        sb.Append("<tr align=\"center\">");
        sb.Append("<td rowspan=\"2\" style=\"width: 80px; border: solid #8D8D8D 1px;\">批量操作</td>");
        sb.Append("<td rowspan=\"2\" style=\"width: 80px; border: solid #8D8D8D 1px;\">日期" + "\\" + "房型</td>");
        sb.Append("<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\">LMBAR2</td><td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;\">LMBAR</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");

        #region 循环LMBAR2 房型CODE    最后一个COde   td的style  去掉
        this.HidLastHotelRoomListLMBAR2.Value = "";
        if (RoomListLMABAR2.Length > 0)
        {
            sb.Append("<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" style=\" border: none;\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr align=\"center\">");
            for (int i = 0; i < RoomListLMABAR2.Length; i++)
            {
                if (RoomListLMABAR2.Length - 1 == i)
                {
                    sb.Append("<td style=\"width:" + lmbar2W + ";\">");
                }
                else
                {
                    sb.Append("<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">");
                }
                sb.Append("<span>" + RoomListLMABAR2[i]["ROOMNM"].ToString() + "</span></br><span>" + RoomListLMABAR2[i]["ROOMCODE"].ToString() + "<span>");
                sb.Append("</td>");
                this.HidLastHotelRoomListLMBAR2.Value += RoomListLMABAR2[i]["ROOMCODE"].ToString() + ",";
            }
        }
        else
        {
            sb.Append("<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr align=\"center\">");

            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">");
            sb.Append("");
            sb.Append("</td>");
        }


        sb.Append("</tr> </table></td>");
        #endregion


        #region 循环LMBAR 房型CODE     最后一个COde   td的style  去掉
        this.HidLastHotelRoomListLMBAR.Value = "";
        if (RoomListLMABAR.Length > 0)
        {
            sb.Append("<td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" height=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr align=\"center\">");
            for (int i = 0; i < RoomListLMABAR.Length; i++)
            {
                if (RoomListLMABAR.Length - 1 == i)
                {
                    sb.Append("<td style=\"width:" + lmbar2W + ";padding-top:5px;\">");
                }
                else
                {
                    sb.Append("<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";padding-top:5px;\">");
                }
                sb.Append("<span>" + RoomListLMABAR[i]["ROOMNM"].ToString() + "</span></br><span>" + RoomListLMABAR[i]["ROOMCODE"].ToString() + "<span>");
                sb.Append("</td>");
                this.HidLastHotelRoomListLMBAR.Value += RoomListLMABAR[i]["ROOMCODE"].ToString() + ",";
            }
        }
        else
        {
            sb.Append("<td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;border-collapse:collapse;\"><table width=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr align=\"center\">");
            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">");
            sb.Append("");
            sb.Append("</td>");
        }

        sb.Append("</tr></table></td>");
        #endregion

        sb.Append("</tr>");


        bool IsDayOfWeek = false;
        for (int i = 0; i < dtTime.Rows.Count; i++)
        {
            sb.Append("<tr align=\"center\">");
            #region 日期
            if (DateTime.Parse(dtTime.Rows[i]["time"].ToString()).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(dtTime.Rows[i]["time"].ToString()).DayOfWeek.ToString() == "Sunday")
            {
                IsDayOfWeek = true;
                sb.Append("<td style=\"width: 80px; border: solid #8D8D8D 1px;background-color: #CDEBFF;height:40px;\"><input type=\"checkbox\" id=\"chkMarkFullRoom" + i + "\" name=\"chkMarkFullRoom\" runat=\"server\" value=\"" + dtTime.Rows[i]["time"].ToString() + "\"/></td>");
                sb.Append("<td style=\"width: 80px; border: solid #8D8D8D 1px;background-color: #CDEBFF;height:40px;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">" + dtTime.Rows[i]["time"].ToString() + "</td>");
            }
            else
            {
                IsDayOfWeek = false;
                sb.Append("<td style=\"width: 80px; border: solid #8D8D8D 1px;height:40px;\"><input type=\"checkbox\" id=\"chkMarkFullRoom" + i + "\"  name=\"chkMarkFullRoom\" runat=\"server\" value=\"" + dtTime.Rows[i]["time"].ToString() + "\"/></td>");
                sb.Append("<td style=\"width: 80px; border: solid #8D8D8D 1px;height:40px;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">" + dtTime.Rows[i]["time"].ToString() + "</td>");
            }
            #endregion

            sb.Append("<td width=\"" + lmbar2TitleWidth + "\" > <table width=\"" + lmbar2TitleWidth + "\" style=\"border-collapse: collapse; border: none; \" cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr align=\"center\" > ");

            #region 循环LMBAR2   酒店计划中的房型数量和价格
            if (PlanLMBAR2.Length > 0)
            {
                bool flag = false;
                if (RoomListLMABAR2.Length > 0)
                {
                    for (int j = 0; j < RoomListLMABAR2.Length; j++)
                    {
                        flag = false;
                        for (int k = 0; k < PlanLMBAR2.Length; k++)
                        {
                            if (RoomListLMABAR2[j]["ROOMCODE"].ToString() == PlanLMBAR2[k]["ROOMTYPECODE"].ToString() && DateTime.Parse(PlanLMBAR2[k]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(dtTime.Rows[i]["time"].ToString()))
                            {
                                flag = true;
                                if (PlanLMBAR2[k]["ROOMNUM"].ToString() == "0")
                                {
                                    #region
                                    if (PlanLMBAR2[k]["STATUS"].ToString() == "false")
                                    {
                                        sb.Append("<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">");
                                        sb.Append("<table width=\"100%\" style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR2','" + RoomListLMABAR2[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR2[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR2[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR2[k]["STATUS"].ToString() + "','" + PlanLMBAR2[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR2[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR2[k]["ROOMNUM"].ToString());
                                        if (PlanLMBAR2[k]["ISRESERVE"].ToString() == "0")
                                        {
                                            sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                        }
                                        else
                                        {
                                            sb.Append("</td></tr>");
                                        }
                                        sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                        sb.Append("</td>");
                                    }
                                    else if (PlanLMBAR2[k]["ISROOMFUL"].ToString() == "1")
                                    {
                                        sb.Append("<td style=\"background-color:#E96928;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E96928'\">");
                                        sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR2','" + RoomListLMABAR2[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR2[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR2[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR2[k]["STATUS"].ToString() + "','" + PlanLMBAR2[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR2[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR2[k]["ROOMNUM"].ToString());
                                        if (PlanLMBAR2[k]["ISRESERVE"].ToString() == "0")
                                        {
                                            sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                        }
                                        else
                                        {
                                            sb.Append("</td></tr>");
                                        }
                                        sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                        sb.Append("</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style=\"background-color:#E6B9B6;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E6B9B6'\">");
                                        sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR2','" + RoomListLMABAR2[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR2[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR2[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR2[k]["STATUS"].ToString() + "','" + PlanLMBAR2[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR2[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR2[k]["ROOMNUM"].ToString());
                                        if (PlanLMBAR2[k]["ISRESERVE"].ToString() == "0")
                                        {
                                            sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                        }
                                        else
                                        {
                                            sb.Append("</td></tr>");
                                        }
                                        sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                        sb.Append("</td>");
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region
                                    if (PlanLMBAR2[k]["STATUS"].ToString() == "false")
                                    {
                                        sb.Append("<td style=\"background-color:#999999;border-right: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;border-bottom: solid #8D8D8D 1px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">");
                                        sb.Append("<table width=\"100%\"  style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR2','" + RoomListLMABAR2[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR2[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR2[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR2[k]["STATUS"].ToString() + "','" + PlanLMBAR2[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR2[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR2[k]["ROOMNUM"].ToString());
                                        if (PlanLMBAR2[k]["ISRESERVE"].ToString() == "0")
                                        {
                                            sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                        }
                                        else
                                        {
                                            sb.Append("</td></tr>");
                                        }
                                        sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                        sb.Append("</td>");
                                    }
                                    else
                                    {
                                        if (!IsDayOfWeek)
                                        {
                                            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                                            sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR2','" + RoomListLMABAR2[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR2[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR2[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR2[k]["STATUS"].ToString() + "','" + PlanLMBAR2[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR2[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR2[k]["ROOMNUM"].ToString());
                                            if (PlanLMBAR2[k]["ISRESERVE"].ToString() == "0")
                                            {
                                                sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                            }
                                            else
                                            {
                                                sb.Append("</td></tr>");
                                            }
                                            sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                            sb.Append("</td>");
                                        }
                                        else
                                        {
                                            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                                            sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\"  height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR2','" + RoomListLMABAR2[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR2[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR2[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR2[k]["STATUS"].ToString() + "','" + PlanLMBAR2[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR2[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR2[k]["ROOMNUM"].ToString());
                                            if (PlanLMBAR2[k]["ISRESERVE"].ToString() == "0")
                                            {
                                                sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                            }
                                            else
                                            {
                                                sb.Append("</td></tr>");
                                            }
                                            sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR2[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                            sb.Append("</td>");
                                        }
                                    }
                                    #endregion
                                }
                                break;
                            }
                        }
                        if (!flag)
                        {
                            #region
                            if (!IsDayOfWeek)
                            {
                                sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                                sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                                sb.Append("<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                                sb.Append("</td>");
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    #region
                    if (!IsDayOfWeek)
                    {
                        sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                        sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                        sb.Append("</td>");
                    }
                    else
                    {
                        sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                        sb.Append("<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                        sb.Append("</td>");
                    }
                    #endregion
                }
            }
            else
            {
                #region
                if (!IsDayOfWeek)
                {
                    if (RoomListLMABAR2.Length > 0)
                    {
                        for (int j = 0; j < RoomListLMABAR2.Length; j++)
                        {
                            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                            sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                            sb.Append("</td>");
                        }
                    }
                    else
                    {
                        sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                        sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                        sb.Append("</td>");
                    }
                }
                else
                {
                    if (RoomListLMABAR2.Length > 0)
                    {
                        for (int j = 0; j < RoomListLMABAR2.Length; j++)
                        {
                            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                            sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                            sb.Append("</td>");
                        }
                    }
                    else
                    {
                        sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                        sb.Append("<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                        sb.Append("</td>");
                    }
                }
                #endregion
            }

            sb.Append("</tr>");
            sb.Append("</table></td>");

            sb.Append("<td width=\"" + lmbarTitleWidth + "\" ><table width=\"" + lmbarTitleWidth + "\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr align=\"center\">");

            #endregion

            #region 循环LMBAR   酒店计划中的房型数量和价格
            if (PlanLMBAR.Length > 0)
            {
                bool flag = false;
                if (RoomListLMABAR.Length > 0)
                {
                    for (int j = 0; j < RoomListLMABAR.Length; j++)
                    {
                        flag = false;
                        for (int k = 0; k < PlanLMBAR.Length; k++)
                        {
                            if (RoomListLMABAR[j]["ROOMCODE"].ToString() == PlanLMBAR[k]["ROOMTYPECODE"].ToString() && DateTime.Parse(PlanLMBAR[k]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(dtTime.Rows[i]["time"].ToString()))
                            {
                                flag = true;
                                if (PlanLMBAR[k]["ROOMNUM"].ToString() == "0")
                                {
                                    #region
                                    if (PlanLMBAR[k]["STATUS"].ToString() == "false")
                                    {
                                        sb.Append("<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">");
                                        sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"  onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR','" + RoomListLMABAR[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR[k]["STATUS"].ToString() + "','" + PlanLMBAR[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR[k]["ROOMNUM"].ToString());
                                        if (PlanLMBAR[k]["ISRESERVE"].ToString() == "0")
                                        {
                                            sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                        }
                                        else
                                        {
                                            sb.Append("</td></tr>");
                                        }
                                        sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                        sb.Append("</td>");
                                    }
                                    else if (PlanLMBAR[k]["ISROOMFUL"].ToString() == "1")
                                    {
                                        sb.Append("<td style=\"background-color: #E96928;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E96928'\">");
                                        sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR','" + RoomListLMABAR[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR[k]["STATUS"].ToString() + "','" + PlanLMBAR[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR[k]["ROOMNUM"].ToString());
                                        if (PlanLMBAR[k]["ISRESERVE"].ToString() == "0")
                                        {
                                            sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                        }
                                        else
                                        {
                                            sb.Append("</td></tr>");
                                        }
                                        sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                        sb.Append("</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style=\"background-color: #E6B9B6;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E6B9B6'\">");
                                        sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR','" + RoomListLMABAR[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR[k]["STATUS"].ToString() + "','" + PlanLMBAR[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR[k]["ROOMNUM"].ToString());
                                        if (PlanLMBAR[k]["ISRESERVE"].ToString() == "0")
                                        {
                                            sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                        }
                                        else
                                        {
                                            sb.Append("</td></tr>");
                                        }
                                        sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                        sb.Append("</td>");
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region
                                    if (PlanLMBAR[k]["STATUS"].ToString() == "false")
                                    {
                                        sb.Append("<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">");
                                        sb.Append("<table width=\"100%\"  cellpadding=\"0\" cellspacing=\"0\" style=\"border: none;\"  height=\"40px;\"  onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR','" + RoomListLMABAR[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR[k]["STATUS"].ToString() + "','" + PlanLMBAR[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR[k]["ROOMNUM"].ToString());
                                        if (PlanLMBAR[k]["ISRESERVE"].ToString() == "0")
                                        {
                                            sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                        }
                                        else
                                        {
                                            sb.Append("</td></tr>");
                                        }
                                        sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                        sb.Append("</td>");
                                    }
                                    else
                                    {
                                        if (!IsDayOfWeek)
                                        {
                                            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                                            sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR','" + RoomListLMABAR[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR[k]["STATUS"].ToString() + "','" + PlanLMBAR[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR[k]["ROOMNUM"].ToString());
                                            if (PlanLMBAR[k]["ISRESERVE"].ToString() == "0")
                                            {
                                                sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                            }
                                            else
                                            {
                                                sb.Append("</td></tr>");
                                            }
                                            sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                            sb.Append("</td>");
                                        }
                                        else
                                        {
                                            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                                            sb.Append("<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + this.HidPcode.Value + "','LMBAR','" + RoomListLMABAR[j]["ROOMNM"].ToString() + "','" + RoomListLMABAR[j]["ROOMCODE"].ToString() + "','" + PlanLMBAR[k]["TWOPRICE"].ToString() + "','" + PlanLMBAR[k]["ROOMNUM"].ToString() + "','" + PlanLMBAR[k]["STATUS"].ToString() + "','" + PlanLMBAR[k]["ISRESERVE"].ToString() + "','" + PlanLMBAR[k]["EFFECTDATESTRING"].ToString() + "')\"><tr align=\"center\"><td>" + PlanLMBAR[k]["ROOMNUM"].ToString());
                                            if (PlanLMBAR[k]["ISRESERVE"].ToString() == "0")
                                            {
                                                sb.Append("<span style=\"color: Red\">*</span></td></tr>");
                                            }
                                            else
                                            {
                                                sb.Append("</td></tr>");
                                            }
                                            sb.Append("<tr align=\"center\"><td>￥" + PlanLMBAR[k]["TWOPRICE"].ToString() + "</td></tr></table>");
                                            sb.Append("</td>");
                                        }
                                    }
                                    #endregion
                                }
                                break;
                            }
                        }
                        if (!flag)
                        {
                            #region
                            if (!IsDayOfWeek)
                            {
                                sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                                sb.Append("<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                                sb.Append("<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                                sb.Append("</td>");
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    #region
                    if (!IsDayOfWeek)
                    {
                        sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                        sb.Append("<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                        sb.Append("</td>");
                    }
                    else
                    {
                        sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                        sb.Append("<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                        sb.Append("</td>");
                    }
                    #endregion
                }
            }
            else
            {
                #region
                if (!IsDayOfWeek)
                {
                    if (RoomListLMABAR.Length > 0)
                    {
                        for (int j = 0; j < RoomListLMABAR.Length; j++)
                        {
                            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                            sb.Append("<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                            sb.Append("</td>");
                        }
                    }
                    else
                    {
                        sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">");
                        sb.Append("<table  width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                        sb.Append("</td>");
                    }
                }
                else
                {
                    if (RoomListLMABAR.Length > 0)
                    {
                        for (int j = 0; j < RoomListLMABAR.Length; j++)
                        {
                            sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                            sb.Append("<table  width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                            sb.Append("</td>");
                        }
                    }
                    else
                    {
                        sb.Append("<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">");
                        sb.Append("<table  width=\"100%\"  style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\"  height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>");
                        sb.Append("</td>");
                    }
                }
                #endregion
            }

            sb.Append("</tr>");
            sb.Append("</table></td>");

            #endregion

            sb.Append("</tr>");
        }
        sb.Append("</table>");

        divMain.InnerHtml = sb.ToString();
    }

    /// <summary>
    /// 设置当前选中酒店的颜色  还原  上一个酒店的原有颜色
    /// </summary> 
    protected void btnClickckHotel_Click(object sender, EventArgs e)
    {
        gridHotelList.SelectedIndex = int.Parse(this.HidSelIndex.Value);
        if (this.HidSelIndexOld.Value != this.HidSelIndex.Value)
        {
            //if (!string.IsNullOrEmpty(this.HidSelIndexOld.Value))
            //{
            //    string rowOldColor = gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].Cells[6].Text;//取
            //    string colOldColor = gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].Cells[7].Text;//取
            //    gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].BackColor = System.Drawing.ColorTranslator.FromHtml(rowOldColor);
            //    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndexOld.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml(colOldColor);
            //}
            for (int i = 0; i < gridHotelList.Rows.Count; i++)
            {
                if (gridHotelList.Rows[i].BackColor == System.Drawing.ColorTranslator.FromHtml("#FFCC66"))
                {
                    gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                    ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[i].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#ececec");
                }
                this.UpdatePanel4.Update();
            }
            gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
            ((System.Web.UI.WebControls.WebControl)((Label)gridHotelList.Rows[int.Parse(this.HidSelIndex.Value)].FindControl("Label1"))).BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
            this.UpdatePanel4.Update();
        }
        if (!this.HidSelIndex.Value.Equals(this.HidSelIndexOld.Value))
        {
            this.HidSelIndexOld.Value = this.HidSelIndex.Value;
        }
    }

    /// <summary>
    /// 设置酒店列表的选中项
    /// </summary>
    protected void btnSetHotelSelect_Click()
    {
        //gridHotelList.SelectedIndex = int.Parse(this.HidSelIndex.Value);
    }

    #endregion
}