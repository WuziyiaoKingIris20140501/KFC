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

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Text.RegularExpressions;

public partial class ReviewHotelRoomPlan : BasePage
{
    public DataTable RoomList = new DataTable();
    public DataTable LmbarPlanList = new DataTable();
    public DataTable Lmbar2PlanList = new DataTable();
    public DataTable TimeList = new DataTable();
    HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        wctHotel.Attributes.Add("onfocus", "javascript:document.getElementById('wctHotel').innerHTML=''");
        wctCity.Attributes.Add("onfocus", "javascript:document.getElementById('wctCity').innerHTML=''");
        wcthvpInventoryControl.Attributes.Add("onfocus", "javascript:document.getElementById('wcthvpInventoryControl').innerHTML=''");
        wcthvpTagInfo.Attributes.Add("onfocus", "javascript:document.getElementById('wcthvpTagInfo').innerHTML=''");
        if (!IsPostBack)
        {
            if (DateTime.Now.Hour <= 4 && DateTime.Now.Hour >= 0)
            {
                this.planStartDate.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(6).ToShortDateString().Replace("/", "-");
            }
            else
            {
                this.planStartDate.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(7).ToShortDateString().Replace("/", "-");
            }
            this.lmbarH.Visible = false;
            this.lmbar2H.Visible = false;
            ViewState["hotelID"] = "";
            ViewState["StartDT"] = this.planStartDate.Value;
            ViewState["EndDT"] = this.planEndDate.Value;
            this.DivhotelDetails.Visible = false;
            this.DivPlanDetails.Visible = false;
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        #region   酒店
        string strHotel = wctHotel.AutoResult.ToString();
        if (!string.IsNullOrEmpty(strHotel))
        {
            if (!string.IsNullOrEmpty(strHotel))
            {
                if (!strHotel.Trim().Contains("[") || !strHotel.Trim().Contains("]"))
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "hisInfo", "AlterHotelError();", true);
                    return;
                }
            }
        }
        #endregion

        #region   城市
        string strCity = wctCity.AutoResult.ToString();
        if (!string.IsNullOrEmpty(strCity))
        {
            if (!string.IsNullOrEmpty(strCity))
            {
                if (!strCity.Trim().Contains("[") || !strCity.Trim().Contains("]"))
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "hisInfo", "AlterCityError();", true);
                    return;
                }
            }
        }
        #endregion

        #region   销售
        string strSales = wcthvpInventoryControl.AutoResult.ToString();
        if (!string.IsNullOrEmpty(strSales))
        {
            if (!string.IsNullOrEmpty(strSales))
            {
                if (!strSales.Trim().Contains("[") || !strSales.Trim().Contains("]"))
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "hisInfo", "AlterSalesError();", true);
                    return;
                }
            }
        }
        #endregion

        #region   商圈
        string strArea = wcthvpTagInfo.AutoResult.ToString();
        if (!string.IsNullOrEmpty(strArea))
        {
            if (!string.IsNullOrEmpty(strArea))
            {
                if (!strArea.Trim().Contains("[") || !strArea.Trim().Contains("]"))
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "hisInfo", "AlterAreaError();", true);
                    return;
                }
            }
        }
        #endregion

        DataTable dtResult = new DataTable();
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelID = strHotel == "" ? "" : strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));//酒店ID
        hotelInfoDBEntity.City = strCity == "" ? "" : strCity.Substring((strCity.IndexOf('[') + 1), (strCity.IndexOf(']') - 1)); //"";//城市ID 
        hotelInfoDBEntity.Bussiness = strArea == "" ? "" : strArea.Substring((strArea.IndexOf('[') + 1), (strArea.IndexOf(']') - 1));//"";//商圈ID
        hotelInfoDBEntity.SalesID = strSales == "" ? "" : strSales.Substring((strSales.IndexOf('[') + 1), (strSales.IndexOf(']') - 1));//"";//销售

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        dtResult = HotelPlanInfoBP.GetHotelList(_hotelinfoEntity).QueryResult.Tables[0];

        this.gridHotelList.DataSource = dtResult.DefaultView;
        this.gridHotelList.DataBind();
        this.UpdatePanel2.Update();
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "hisInfo", "BtnCompleteStyle();", true);

        //默认触发第一个酒店的Click事件
        operandNum.InnerHtml = dtResult.Rows.Count.ToString();
        if (dtResult.Rows.Count > 0)
        {
            this.hidSelectHotelID.Value = gridHotelList.DataKeys[0].Values[1].ToString();
            this.hidSelectHotelName.Value = gridHotelList.DataKeys[0].Values[2].ToString();
            hidSelectIndex.Value = "0";
            btnSingleHotel_Click(null, null);
        }
        else
        {
            this.DivhotelDetails.Visible = false;
            this.DivPlanDetails.Visible = false;

            this.lmbarH.Visible = false;
            this.lmbar2H.Visible = false;


            UpdatePanel1.Update();
        }
    }

    protected void btnSingleHotel_Click(object sender, EventArgs e)
    {
        this.DivhotelDetails.Visible = true;
        this.DivPlanDetails.Visible = true;

        this.lmbarH.Visible = true;
        this.lmbar2H.Visible = true;

        RoomList.Clear();
        LmbarPlanList.Clear();
        TimeList.Clear();

        string strHotel = this.hidSelectHotelID.Value;
        if (String.IsNullOrEmpty(strHotel))
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "hisInfo", "BtnCompleteStyle();", true);
            return;
        }
        lblHotelID.Text = strHotel;
        lblhotelName.Text = lblHotelID.Text + " - " + this.hidSelectHotelName.Value;
        HidHotelID.Value = lblHotelID.Text;
        BindData();
        ViewState["hotelID"] = this.lblHotelID.Text;
        ViewState["StartDT"] = this.planStartDate.Value;
        ViewState["EndDT"] = this.planEndDate.Value;
        lblHotelDetails.Text = "     --   [" + lblHotelID.Text + "]" + this.hidSelectHotelName.Value;

        ////判断维护权限   酒店销售  助理  主管
        //List<string> list = new List<string>();
        //list.Add("Selina.liang");
        //list.Add("Sally.Qian");
        //list.Add("judy.wang");
        //list.Add("daniel.yuan");
        //list.Add("Cecilia.Ren");
        //list.Add("colin.han");
        //if (UserSession.Current.UserAccount == LinkMan.Text || UserSession.Current.UserDspName == LinkMan.Text || list.Contains(UserSession.Current.UserAccount))
        //{
        //    btnAddSalesPlan.Style["Display"] = "Block"; //显示
        //}
        //else
        //{
        //    btnAddSalesPlan.Style["Display"] = "none"; //显示
        //}

        for (int i = 0; i < gridHotelList.Rows.Count; i++)
        {
            //首先判断是否是数据行
            if (gridHotelList.Rows[i].RowType == DataControlRowType.DataRow)
            {
                gridHotelList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }
        }
        int sIndex = int.Parse(hidSelectIndex.Value == "" ? "0" : hidSelectIndex.Value);
        gridHotelList.Rows[sIndex].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
        UpdatePanel1.Update();
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "hisInfo", "BtnCompleteStyle();", true);
    }


    public void BindDateSelect()
    {
        //this.DivhotelDetails.Visible = true;
        //this.DivPlanDetails.Visible = true;

        //this.lmbarH.Visible = true;
        //this.lmbar2H.Visible = true;

        //RoomList.Clear();
        //LmbarPlanList.Clear();
        //TimeList.Clear();

        //if (String.IsNullOrEmpty(strHotel))
        //{
        //    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "hisInfo", "BtnCompleteStyle();", true);
        //    return;
        //}
        //lblHotelID.Text = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
        //lblhotelName.Text = lblHotelID.Text + " - " + strHotel.Substring(strHotel.IndexOf(']') + 1);
        //HidHotelID.Value = lblHotelID.Text;
        //BindData();
        //ViewState["hotelID"] = this.lblHotelID.Text;
        //ViewState["StartDT"] = this.planStartDate.Value;
        //ViewState["EndDT"] = this.planEndDate.Value;
        ////lblDivHotelName.Text = lblhotelName.Text;
        //lblHotelDetails.Text = "     --   [" + lblHotelID.Text + "]" + strHotel.Substring(strHotel.IndexOf(']') + 1);
        //UpdatePanel1.Update();
        //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "hisInfo", "BtnCompleteStyle();", true);
    }

    public void BindData()
    {
        GetBasicLMInfo(lblHotelID.Text);
        GetBasicSalesInfo(lblHotelID.Text);
        DataSet roomResult = GetBalanceRoomList(lblHotelID.Text);
        if (roomResult != null)
        {
            RoomList = roomResult.Tables[0];
            DataRow dr1 = RoomList.NewRow();
            dr1["ROOMCODE"] = "";
            dr1["ROOMNM"] = "";
            RoomList.Rows.InsertAt(dr1, 0);
        }
        DataSet LmbarPlanResult = GetBindLmbarPlanList(this.planStartDate.Value, this.planEndDate.Value, lblHotelID.Text, "LMBAR");
        if (LmbarPlanResult != null)
        {
            LmbarPlanList = LmbarPlanResult.Tables[0];
        }
        DataSet Lmbar2PlanResult = GetBindLmbarPlanList(this.planStartDate.Value, this.planEndDate.Value, lblHotelID.Text, "LMBAR2");
        if (Lmbar2PlanResult != null)
        {
            Lmbar2PlanList = Lmbar2PlanResult.Tables[0];
        }
        GetDate(this.planStartDate.Value, this.planEndDate.Value);

        //BindBalanceRoomList(lblHotelID.Text);


    }

    protected void btnShwoInfo_Click(object sender, EventArgs e)
    {
        GetShwoHisPlanInfoList(HidHotelID.Value.Trim(), HiddenEffectDate.Value.Trim(), HiddenPriceCode.Value.Trim(), HiddenRoomCode.Value.Trim());
    }

    public void GetShwoHisPlanInfoList(string strHotelID, string effectDate, string priceCode, string roomCode)
    {
        if (String.IsNullOrEmpty(strHotelID) && String.IsNullOrEmpty(effectDate) && String.IsNullOrEmpty(priceCode) && String.IsNullOrEmpty(roomCode))
        {
            RefushBindData();
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "hisInfo", "BtnCompleteStyle();", true);
            return;
        }

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        hotelInfoDBEntity.EffectDate = effectDate;
        hotelInfoDBEntity.PriceCode = priceCode;
        hotelInfoDBEntity.RoomCode = roomCode;

        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);

        DataSet dsResult = HotelInfoBP.GetShwoHisPlanInfoList(_hotelinfoEntity).QueryResult;

        if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
        {
            dvHisPlanInfoList.InnerHtml = "无酒店计划历史，请确认！";
            RefushBindData();
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "hisInfo", "invokeOpen2();", true);
            return;
        }

        StringBuilder sbString = new StringBuilder();
        string strTemp = string.Empty;
        string strMsg = string.Empty; //修改计划
        string strMeno = string.Empty;
        sbString.Append("<table cellspacing='0' cellpadding='0' width='100%' style='border:1px solid #D5D5D5'><tr class='GView_HeaderCSS' style='border-collapse:collapse'><td width='30%' style='white-space:nowrap; border: solid #D5D5D5 1px;'>操作人</td><td width='40%' style='white-space:nowrap; border: solid #D5D5D5 1px;'>操作时间</td><td width='30%' style='white-space:nowrap; border: solid #D5D5D5 1px;'>操作内容</td></tr>");

        for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
        {
            strMsg = dsResult.Tables[0].Rows[i]["ope_msg"].ToString();
            strMeno = dsResult.Tables[0].Rows[i]["opememo"].ToString();

            if ("修改计划".Equals(strMeno))
            {
                if (strMsg.Contains("status") && "F".Equals(strMsg.Substring(strMsg.IndexOf("status") + 7, 1).Trim().ToUpper()))
                {
                    strMeno = "操作下线";
                }

                if (strMsg.Contains("isRoomful") && "1".Equals(strMsg.Substring(strMsg.IndexOf("isRoomful") + 10, 1).Trim().ToUpper()))
                {
                    strMeno = ("操作下线".Equals(strMeno)) ? "满房&下线" : "标记满房";
                }
            }

            strTemp = strTemp + "<tr id='" + "trHis" + i.ToString() + "' class='GView_ItemCSS' style='border-collapse:collapse' onclick=DvChangeEvent('" + "dvHis" + i.ToString() + "','" + "trHis" + i.ToString() + "')><td style='border:1px solid #D5D5D5'><font color='#3A599C'>" + dsResult.Tables[0].Rows[i]["operator"].ToString().PadRight(25, ' ') + "</font></td><td style='border:1px solid #D5D5D5'><font color='#3A599C'>" + dsResult.Tables[0].Rows[i]["opetime"].ToString() + "</font></td><td style='border:1px solid #D5D5D5'><font color='#3A599C'>" + strMeno + "</font></td></tr>";
            strTemp = strTemp + "<tr id='" + "dvHis" + i.ToString() + "' style='display:none;border:1px solid #D5D5D5;border-collapse:collapse;'><td colspan='3' style='border:1px solid #D5D5D5;' align='left'>" + dsResult.Tables[0].Rows[i]["ope_msg"].ToString() + "</td></tr>";
            sbString.Append(strTemp);
            strTemp = "";
            strMeno = "";
        }

        sbString.Append("</table>");
        dvHisPlanInfoList.InnerHtml = sbString.ToString();
        RefushBindData();
        ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "hisInfo", "invokeOpen2();", true);
    }

    public void RefushBindData()
    {
        string strHotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["hotelID"].ToString())) ? null : ViewState["hotelID"].ToString();
        string strStartDT = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDT"].ToString())) ? null : ViewState["StartDT"].ToString();
        string strEndDT = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDT"].ToString())) ? null : ViewState["EndDT"].ToString();

        if (String.IsNullOrEmpty(strHotelID.Trim()) || String.IsNullOrEmpty(strStartDT.Trim()) || String.IsNullOrEmpty(strEndDT.Trim()))
        {
            return;
        }

        GetBasicLMInfo(strHotelID);
        GetBasicSalesInfo(strHotelID);
        DataSet roomResult = GetBalanceRoomList(strHotelID);

        if (roomResult != null)
        {
            RoomList = roomResult.Tables[0];
            DataRow dr1 = RoomList.NewRow();
            dr1["ROOMCODE"] = "";
            dr1["ROOMNM"] = "";
            RoomList.Rows.InsertAt(dr1, 0);
        }
        DataSet LmbarPlanResult = GetBindLmbarPlanList(strStartDT, strEndDT, strHotelID, "LMBAR");
        if (LmbarPlanResult != null)
        {
            LmbarPlanList = LmbarPlanResult.Tables[0];
        }
        DataSet Lmbar2PlanResult = GetBindLmbarPlanList(strStartDT, strEndDT, strHotelID, "LMBAR2");
        if (Lmbar2PlanResult != null)
        {
            Lmbar2PlanList = Lmbar2PlanResult.Tables[0];
        }
        GetDate(strStartDT, strEndDT);
    }


    /// <summary>
    /// 酒店信息-LM联系人
    /// </summary>
    public void GetBasicLMInfo(string strHotelID)
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.BindHotelList(_hotelinfoEntity).QueryResult;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            ContactTel.Text = dsResult.Tables[0].Rows[0]["PHONE"].ToString();
            ContactMan.Text = dsResult.Tables[0].Rows[0]["CONTACTPER"].ToString();
        }
        else
        {
            ContactTel.Text = "";
            ContactMan.Text = "";
        }
    }

    /// <summary>
    /// 酒店信息-销售联系人
    /// </summary>
    /// <param name="strHotelID"></param>
    public void GetBasicSalesInfo(string strHotelID)
    {
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
            LinkMan.Text = dsResult.Tables[0].Rows[0]["DISPNM"].ToString().Replace('/', '-');
            LinkTel.Text = dsResult.Tables[0].Rows[0]["User_Tel"].ToString().Replace('/', '-');
        }
        else
        {
            LinkMan.Text = "";
            LinkTel.Text = "";
        }
    }

    /// <summary>
    /// 自动拼取时间段
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    public void GetDate(string startDate, string endDate)
    {
        TimeList.Columns.Add(new DataColumn("time"));
        TimeList.Columns.Add(new DataColumn("timeMD"));
        DataRow drRow = TimeList.NewRow();
        drRow["time"] = "";
        drRow["timeMD"] = "";
        TimeList.Rows.Add(drRow);
        TimeSpan ts = DateTime.Parse(endDate) - DateTime.Parse(startDate);
        int days = ts.Days;
        for (int i = 0; i <= days; i++)
        {
            drRow = TimeList.NewRow();
            drRow["time"] = DateTime.Parse(startDate).AddDays(i).ToShortDateString();
            drRow["timeMD"] = DateTime.Parse(startDate).AddDays(i).Month.ToString() + "-" + DateTime.Parse(startDate).AddDays(i).Day.ToString();
            TimeList.Rows.Add(drRow);
        }
    }

    /// <summary>
    /// 根据HotelID得到所有房型
    /// </summary>
    /// <param name="strHotelID"></param>
    private DataSet GetBalanceRoomList(string strHotelID)
    {
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        //return HotelInfoBP.GetBalanceRoomList(_hotelinfoEntity).QueryResult;
        return HotelInfoBP.GetBalHotelRoomList(_hotelinfoEntity).QueryResult;
    }

    /// <summary>
    /// 根据时间段  HotelID 取计划
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="strHotelID"></param>
    /// <returns></returns>
    public DataSet GetBindLmbarPlanList(string startDate, string endDate, string strHotelID, string priceCode)
    {
        #region
        //string sql = string.Empty;
        //sql = "SELECT ID,EFFECT_DATE,SEASON,MONEY_TYPE,HOTEL_ID,ROOM_TYPE_NAME,ROOM_TYPE_CODE,STATUS,ROOM_NUM,GMT_CREATED,CREATOR,ONE_PRICE,TWO_PRICE,THREE_PRICE,FOUR_PRICE,ATTN_PRICE,BREAKFAST_NUM,EACH_BREAKFAST_PRICE,IS_NETWORK,GMT_MODIFIED,MODIFIER,IS_DELETED,HOLD_ROOM_NUM,RATE_CODE,GUAID,CXLID,OFFSETVAL,OFFSETUNIT,LMPRICE,THIRDPRICE,LMSTATUS,IS_RESERVE,HOTELVP_STATUS,APP_STATUS,IS_ROOMFUL FROM T_LM_PLAN " +
        //      " WHERE EFFECT_DATE BETWEEN to_date(:startDate,'yyyy-mm-dd') AND to_date(:endDate,'yyyy-mm-dd') and hotel_ID=:strHotelID";
        //OracleParameter[] parm ={
        //                            new OracleParameter("startDate",OracleType.VarChar), 
        //                            new OracleParameter("endDate",OracleType.VarChar),     
        //                            new OracleParameter("strHotelID",OracleType.VarChar)                                    
        //                        };
        //if (String.IsNullOrEmpty(startDate))
        //{
        //    parm[0].Value = DBNull.Value;
        //}
        //else
        //{
        //    parm[0].Value = startDate;
        //}

        //if (String.IsNullOrEmpty(endDate))
        //{
        //    parm[1].Value = DBNull.Value;
        //}
        //else
        //{
        //    parm[1].Value = endDate;
        //}

        //if (String.IsNullOrEmpty(strHotelID))
        //{
        //    parm[2].Value = DBNull.Value;
        //}
        //else
        //{
        //    parm[2].Value = strHotelID;
        //}

        //DataSet ds = DbHelperOra.Query(sql, false, parm);
        //return ds;
        #endregion

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

        DataSet dsResult = HotelInfoBP.GetPlanList(_hotelinfoEntity).QueryResult;

        return dsResult;
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
            e.Row.Attributes.Add("OnClick", "ClickEvent('" + gridHotelList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[1].ToString() + "','" + gridHotelList.DataKeys[e.Row.RowIndex].Values[2].ToString() + "','" + e.Row.RowIndex.ToString() + "')");
        }
    }
}