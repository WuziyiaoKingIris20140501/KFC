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

public partial class HotelRoomInventoryControl : BasePage
{
    //public DataTable RoomList = new DataTable ();

    public DataTable LMBARRoomList = new DataTable();
    public DataTable LMBAR2RoomList = new DataTable();

    public DataTable LmbarPlanList = new DataTable();
    public DataTable Lmbar2PlanList = new DataTable();
    public DataTable TimeList = new DataTable();
    HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
    CommonEntity _commonEntity = new CommonEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        wctHotel.Attributes.Add("onfocus", "javascript:document.getElementById('wctHotel').innerHTML=''");
        if (!IsPostBack)
        {
            if (DateTime.Now.Hour <= 4 && DateTime.Now.Hour >= 0)
            {
                this.planStartDate.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(6).ToShortDateString().Replace("/", "-");

                this.longPlanStartDate.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
                this.longPlanEndDate.Value = DateTime.Now.AddDays(-1).ToShortDateString().Replace("/", "-");
            }
            else
            {
                this.planStartDate.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
                this.planEndDate.Value = DateTime.Now.AddDays(7).ToShortDateString().Replace("/", "-");

                this.longPlanStartDate.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
                this.longPlanEndDate.Value = DateTime.Now.AddDays(7).ToShortDateString().Replace("/", "-");
            }
            this.lmbarH.Visible = false;
            this.lmbar2H.Visible = false;
            ViewState["hotelID"] = "";
            this.DivhotelDetails.Visible = false;
            this.DivPlanDetails.Visible = false;
            this.DivLongPlanDetails.Visible = false;
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string strHotel = wctHotel.AutoResult.ToString();
        if (string.IsNullOrEmpty(strHotel))
        {
            //Page.RegisterStartupScript(" ", " <script>   alert( '请选择酒店！ ') </script> ");
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "keyalerthotel", "alert('请选择酒店！')", true);
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "keyOvers", "BtnCompleteStyle()", true);
            return;
        }

        this.DivhotelDetails.Visible = true;
        this.DivPlanDetails.Visible = true;
        this.DivLongPlanDetails.Visible = true;

        this.lmbarH.Visible = true;
        this.lmbar2H.Visible = true;

        //RoomList.Clear();
        LmbarPlanList.Clear();
        TimeList.Clear();

        if (String.IsNullOrEmpty(strHotel))
        {
            return;
        }
        lblHotelID.Text = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
        lblhotelName.Text = lblHotelID.Text + " - " + strHotel.Substring(strHotel.IndexOf(']') + 1);

        BindData();

        ViewState["hotelID"] = this.lblHotelID.Text;

        lblDivHotelName.Text = lblhotelName.Text;

        lblHotelDetails.Text = "     --   [" + lblHotelID.Text + "]" + strHotel.Substring(strHotel.IndexOf(']') + 1);

        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.GetType(), "keyOvers", "BtnCompleteStyle()", true);
    }

    public void BindData()
    {
        GetBasicLMInfo(lblHotelID.Text);
        GetBasicSalesInfo(lblHotelID.Text);
        DataSet roomResult = GetBalanceRoomList(lblHotelID.Text);
        //roomResult.Tables[0].Columns["ROOMCD"].ColumnName = "ROOMCODE"; 
        if (roomResult != null)
        {
            LMBARRoomList = roomResult.Tables[0];
            DataRow dr1 = LMBARRoomList.NewRow();
            dr1["ROOMCODE"] = "";
            dr1["ROOMNM"] = "";
            LMBARRoomList.Rows.InsertAt(dr1, 0);

            LMBAR2RoomList = roomResult.Tables[0];
            //DataRow dr2 = LMBAR2RoomList.NewRow();
            //dr2["ROOMCODE"] = "";
            //dr2["ROOMNM"] = "";
            //LMBAR2RoomList.Rows.InsertAt(dr2, 0);
        }
        this.rdLmbar2.Checked = true;
        DataSet LmbarPlanResult = GetBindLmbarPlanList(this.planStartDate.Value, this.planEndDate.Value, lblHotelID.Text, "LMBAR");
        List<string> LmbarRtb = new List<string>();
        if (LmbarPlanResult.Tables.Count > 0 && LmbarPlanResult.Tables[0].Rows.Count > 0)
        {
            for (int j = 0; j < LMBARRoomList.Rows.Count; j++)
            {
                for (int i = 0; i < LmbarPlanResult.Tables[0].Rows.Count; i++)
                {
                    if (LMBARRoomList.Rows[j]["ROOMCODE"].ToString().Trim() == LmbarPlanResult.Tables[0].Rows[i]["ROOMTYPECODE"].ToString().Trim())
                    {
                        if (!LmbarRtb.Contains(LMBARRoomList.Rows[j]["ROOMCODE"].ToString() + "|" + LMBARRoomList.Rows[j]["ROOMNM"].ToString()))
                        {
                            LmbarRtb.Add(LMBARRoomList.Rows[j]["ROOMCODE"].ToString() + "|" + LMBARRoomList.Rows[j]["ROOMNM"].ToString());
                        }
                    }
                }
            }
        }
        LMBARRoomList = new DataTable();
        LMBARRoomList.Columns.Add("ROOMCODE");
        LMBARRoomList.Columns.Add("ROOMNM");
        DataRow dr11 = LMBARRoomList.NewRow();
        dr11["ROOMCODE"] = "";
        dr11["ROOMNM"] = "";
        LMBARRoomList.Rows.InsertAt(dr11, 0);
        for (int i = 0; i < LmbarRtb.Count; i++)
        {
            DataRow dr111 = LMBARRoomList.NewRow();
            dr111["ROOMCODE"] = LmbarRtb[i].ToString().Split('|')[0].ToString();
            dr111["ROOMNM"] = LmbarRtb[i].ToString().Split('|')[1].ToString();
            LMBARRoomList.Rows.Add(dr111);
        }
        LmbarPlanList = LmbarPlanResult.Tables[0];

        DataSet Lmbar2PlanResult = GetBindLmbarPlanList(this.planStartDate.Value, this.planEndDate.Value, lblHotelID.Text, "LMBAR2");
        List<string> Lmbar2Rtb = new List<string>();
        if (Lmbar2PlanResult.Tables.Count > 0 && Lmbar2PlanResult.Tables[0].Rows.Count > 0)
        {
            for (int j = 0; j < LMBAR2RoomList.Rows.Count; j++)
            {
                for (int i = 0; i < Lmbar2PlanResult.Tables[0].Rows.Count; i++)
                {
                    if (LMBAR2RoomList.Rows[j]["ROOMCODE"].ToString().Trim() == Lmbar2PlanResult.Tables[0].Rows[i]["ROOMTYPECODE"].ToString().Trim())
                    {
                        if (!Lmbar2Rtb.Contains(LMBAR2RoomList.Rows[j]["ROOMCODE"].ToString() + "|" + LMBAR2RoomList.Rows[j]["ROOMNM"].ToString()))
                        {
                            Lmbar2Rtb.Add(LMBAR2RoomList.Rows[j]["ROOMCODE"].ToString() + "|" + LMBAR2RoomList.Rows[j]["ROOMNM"].ToString());
                        }
                    }
                }
            }
        }

        LMBAR2RoomList = new DataTable();
        LMBAR2RoomList.Columns.Add("ROOMCODE");
        LMBAR2RoomList.Columns.Add("ROOMNM");
        DataRow dr2 = LMBAR2RoomList.NewRow();
        dr2["ROOMCODE"] = "";
        dr2["ROOMNM"] = "";
        LMBAR2RoomList.Rows.InsertAt(dr2, 0);
        for (int i = 0; i < Lmbar2Rtb.Count; i++)
        {
            DataRow dr22 = LMBAR2RoomList.NewRow();
            dr22["ROOMCODE"] = Lmbar2Rtb[i].ToString().Split('|')[0].ToString();
            dr22["ROOMNM"] = Lmbar2Rtb[i].ToString().Split('|')[1].ToString();
            LMBAR2RoomList.Rows.Add(dr22);
        }
        Lmbar2PlanList = Lmbar2PlanResult.Tables[0];


        GetDate(this.planStartDate.Value, this.planEndDate.Value);

        BindBalanceRoomList(lblHotelID.Text);


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

    /// <summary>
    /// 生成房型列表
    /// </summary>
    /// <param name="strHotelID"></param>
    public void BindBalanceRoomList(string strHotelID)
    {
        chkHotelRoomListLMBAR2.Items.Clear();
        chkHotelRoomListLMBAR.Items.Clear();

        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();

        hotelInfoDBEntity.HotelID = strHotelID;//酒店ID
        hotelInfoDBEntity.PriceCode = "LMBAR2";//价格代码
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResultLMBAR2 = HotelInfoBP.GetBalanceRoomList(_hotelinfoEntity).QueryResult;
        if (dsResultLMBAR2.Tables.Count > 0 && dsResultLMBAR2.Tables[0].Rows.Count > 0)
        {
            chkHotelRoomListLMBAR2.DataTextField = "ROOMNM";
            chkHotelRoomListLMBAR2.DataValueField = "ROOMCODE";
            chkHotelRoomListLMBAR2.DataSource = dsResultLMBAR2;
            chkHotelRoomListLMBAR2.DataBind();
        }

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = strHotelID;//酒店ID
        hotelInfoDBEntity.PriceCode = "LMBAR";//价格代码
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResultLMBAR = HotelInfoBP.GetBalanceRoomList(_hotelinfoEntity).QueryResult;
        if (dsResultLMBAR.Tables.Count > 0 && dsResultLMBAR.Tables[0].Rows.Count > 0)
        {
            chkHotelRoomListLMBAR.DataTextField = "ROOMNM";
            chkHotelRoomListLMBAR.DataValueField = "ROOMCODE";
            chkHotelRoomListLMBAR.DataSource = dsResultLMBAR;
            chkHotelRoomListLMBAR.DataBind();
        }
    }

    /// <summary>
    /// 计划更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRenewPlan_Click(object sender, EventArgs e)
    {
        //根据时间段   酒店ID  取出原有计划
        string priceCode = this.rdLmbar.Checked == true ? "LMBAR" : "LMBAR2";
        DataSet planResult = GetBindLmbarPlanList(longPlanStartDate.Value, longPlanEndDate.Value, ViewState["hotelID"].ToString(), priceCode);
        if (planResult.Tables.Count > 0 && planResult.Tables[0].Rows.Count > 0)
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

            _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
            APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

            appcontentDBEntity.HotelID = ViewState["hotelID"].ToString();

            appcontentDBEntity.PriceCode = priceCode;

            string strHRoomListName = "";
            if (priceCode == "LMBAR2")
            {
                foreach (ListItem lt in chkHotelRoomListLMBAR2.Items)
                {
                    if (lt.Selected)
                    {
                        strHRoomListName = strHRoomListName + lt.Text + ",";
                    }
                }
            }
            else
            {
                foreach (ListItem lt in chkHotelRoomListLMBAR.Items)
                {
                    if (lt.Selected)
                    {
                        strHRoomListName = strHRoomListName + lt.Text + ",";
                    }
                }
            }
            strHRoomListName = strHRoomListName.Trim(',');
            if (strHRoomListName == "")
            {
                //Page.RegisterStartupScript(" ", " <script>   alert( '请选择房型！ '); return false;</script> ");
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "keyalertroom", "alert('请选择房型！')", true);
                return;
            }


            appcontentDBEntity.RoomName = strHRoomListName;

            string strHRoomListCode = "";
            if (priceCode == "LMBAR2")
            {
                foreach (ListItem lt in chkHotelRoomListLMBAR2.Items)
                {
                    if (lt.Selected)
                    {
                        strHRoomListCode = strHRoomListCode + lt.Value + ",";
                    }
                }

            }
            else
            {
                foreach (ListItem lt in chkHotelRoomListLMBAR.Items)
                {
                    if (lt.Selected)
                    {
                        strHRoomListCode = strHRoomListCode + lt.Value + ",";
                    }
                }
            }
            strHRoomListCode = strHRoomListCode.Trim(',');
            appcontentDBEntity.RoomCode = strHRoomListCode;

            appcontentDBEntity.RoomStatus = this.dropStatusOpen.Checked == true ? "true" : "false"; //this.dropStatus.SelectedValue;

            //房量和是否是保留房   应根据状态的开启和关闭  取不同的值
            if (this.dropStatusOpen.Checked)
            {
                if (this.txtRoomCount.Text.Trim() != "")
                {
                    appcontentDBEntity.RoomCount = this.txtRoomCount.Text;
                }
                //appcontentDBEntity.RoomCount = this.txtRoomCount.Text;
                appcontentDBEntity.IsReserve = this.ckReserve.Checked == true ? "0" : "1";
            }

            appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
            //得到 中断的时间段
            List<string> list = new List<string>();
            list.Add(planResult.Tables[0].Rows[0]["EFFECTDATESTRING"].ToString());
            for (int i = 0; i < planResult.Tables[0].Rows.Count; i++)
            {
                if (i != planResult.Tables[0].Rows.Count - 1)
                {
                    string effToDate = planResult.Tables[0].Rows[i + 1]["EFFECTDATESTRING"].ToString();
                    string effYesDate = planResult.Tables[0].Rows[i]["EFFECTDATESTRING"].ToString();
                    TimeSpan effectDate = DateTime.Parse(effToDate) - DateTime.Parse(effYesDate);
                    if (effectDate.Days > 1)
                    {
                        list.Add(effYesDate);
                        list.Add(effToDate);
                    }
                }
                else
                {
                    list.Add(planResult.Tables[0].Rows[i]["EFFECTDATESTRING"].ToString());
                }
            }

            appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;
            CommonDBEntity commonDBEntity = new CommonDBEntity();
            for (int j = 0; j < list.Count; j++)
            {
                appcontentDBEntity.StartDTime = list[j].ToString();
                appcontentDBEntity.EndDTime = list[j + 1].ToString();
                j = j + 1;
                _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);

                commonDBEntity = new CommonDBEntity();
                commonDBEntity.Event_Type = "酒店管理-房控计划批量维护";
                commonDBEntity.Event_ID = ViewState["hotelID"].ToString();
                commonDBEntity.IpAddress = UserSession.Current.UserIP;
                commonDBEntity.UserID = UserSession.Current.UserAccount;
                commonDBEntity.UserName = UserSession.Current.UserDspName;
                commonDBEntity.Event_Content = "房控计划批量更新 - 酒店ID：" + ViewState["hotelID"].ToString() + " 价格代码：" + priceCode + " 房型Code：" + appcontentDBEntity.RoomCode + " 计划开始生效时间：" + appcontentDBEntity.StartDTime + "  计划结束生效时间：" + appcontentDBEntity.EndDTime + " 计划状态：" + appcontentDBEntity.RoomStatus;
                commonDBEntity.Event_Result = "已提交";

                _commonEntity.CommonDBEntity.Add(commonDBEntity);
                //CommonBP.InsertEventHistory(_commonEntity);
                _commonEntity.CommonDBEntity.Clear();

                // _appcontentEntity = HotelInfoBP.RenewPlanFullRoom(_appcontentEntity);
            }
            #region
            //appcontentDBEntity.StartDTime = "";//开始日期
            //appcontentDBEntity.EndDTime = "";//结束日期
            //appcontentDBEntity.MoneyType = "";//"币种: CNY(人民币) USD(美元) HKD(港币)"
            //appcontentDBEntity.HotelID = "";//"酒店ID"
            //appcontentDBEntity.HotelNM = "";//"酒店名称"
            //appcontentDBEntity.RoomName = "";//房型名称
            //appcontentDBEntity.RoomCode = "";//房型代码
            //appcontentDBEntity.RoomStatus = "";//true:打开 false:关闭
            //appcontentDBEntity.RoomCount = "";//房间数量
            //appcontentDBEntity.UpdateDTime = "";//更新时间
            //appcontentDBEntity.UpdateUser = "";//操作人
            //appcontentDBEntity.OnePrice = "";//单人价
            //appcontentDBEntity.TwoPrice = "";//双人价
            //appcontentDBEntity.ThreePrice = "";//三人价
            //appcontentDBEntity.FourPrice = "";//四人价
            //appcontentDBEntity.BedPrice = "";//加床价
            //appcontentDBEntity.BreakfastNum = "";//早餐数量
            //appcontentDBEntity.BreakPrice = "";//每份早餐价格
            //appcontentDBEntity.IsNetwork = "";//ctrue(有宽带)false(无宽带)
            //appcontentDBEntity.PriceCode="";//” LMBAR：预付；LMBAR2：现付”
            //appcontentDBEntity.Offsetval = "";//浮动值
            //appcontentDBEntity.Offsetunit = "";//浮动标志，0：固定值，1：百分比
            //appcontentDBEntity.IsReserve = "";//是否保留房：0：保留房；1：非保留房
            //appcontentDBEntity.WeekList="";//"1,2,3,4,5,6,7(分别对应：星期日，一，二，三，四，五，六)值与值之间有逗号分隔"
            #endregion
        }
        BindData();

        //if (this.dropStatusOpen.Checked)
        //{
        //    ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "unReport", "document.getElementById('managerTxtRoomCount').style.display = 'block';document.getElementById('manegerCkReserve').style.display = 'block';", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.GetType(), "unReport", "document.getElementById('managerTxtRoomCount').style.display = 'none';document.getElementById('manegerCkReserve').style.display = 'none';", true);
        //}

        this.longPlanStartDate.Value = DateTime.Now.ToShortDateString().Replace("/", "-");
        this.longPlanEndDate.Value = DateTime.Now.AddDays(7).ToShortDateString().Replace("/", "-");
        this.rdLmbar2.Checked = true;
        this.rdLmbar.Checked = false;
        this.chkHotelRoomListLMBAR2DIV.Attributes["style"] = "display:''";
        this.chkHotelRoomListLMBARDIV.Attributes.Add("style", "display:none");

        this.dropStatusOpen.Checked = true;
        this.dropStatusClose.Checked = false;
        this.managerTxtRoomCount.Attributes["style"] = "display:''";
        this.manegerCkReserve.Attributes["style"] = "display:''";
    }


    protected void btnDivRenewPlan_Click(object sender, EventArgs e)
    {
        string EffectDate = this.HiddenEffectDate.Value;

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
        commonDBEntity.Event_Type = "酒店管理-房控计划单个维护";
        commonDBEntity.Event_ID = ViewState["hotelID"].ToString();
        commonDBEntity.IpAddress = UserSession.Current.UserIP;
        commonDBEntity.UserID = UserSession.Current.UserAccount;
        commonDBEntity.UserName = UserSession.Current.UserDspName;

        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();

        appcontentDBEntity.HotelID = ViewState["hotelID"].ToString();

        appcontentDBEntity.PriceCode = this.HiddenPriceCode.Value;//价格代码

        appcontentDBEntity.RoomName = this.HiddenRoomType.Value;//房型名称
        appcontentDBEntity.RoomCode = this.HiddenRoomCode.Value;

        appcontentDBEntity.RoomStatus = this.dropDivStatusOpen.Checked == true ? "true" : "false";//this.dropDivStatus.SelectedValue;
        if (this.dropDivStatusOpen.Checked)
        {
            appcontentDBEntity.RoomCount = this.txtDivRoomCount.Text;
            appcontentDBEntity.IsReserve = this.ckDivReserve.Checked == true ? "0" : "1";
        }
        appcontentDBEntity.WeekList = "1,2,3,4,5,6,7";
        appcontentDBEntity.StartDTime = this.HiddenEffectDate.Value;
        appcontentDBEntity.EndDTime = this.HiddenEffectDate.Value;
        appcontentDBEntity.UpdateUser = UserSession.Current.UserDspName;

        commonDBEntity.Event_Content = "房控计划单个更新 - 酒店ID：" + ViewState["hotelID"].ToString() + " 价格代码：" + appcontentDBEntity.PriceCode + " 房型Code：" + appcontentDBEntity.RoomCode + " 计划开始生效时间：" + appcontentDBEntity.StartDTime + "  计划结束生效时间：" + appcontentDBEntity.EndDTime + " 计划状态：" + appcontentDBEntity.RoomStatus;
        commonDBEntity.Event_Result = "已提交";

        _commonEntity.CommonDBEntity.Add(commonDBEntity);
        CommonBP.InsertEventHistory(_commonEntity);

        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        _appcontentEntity = HotelInfoBP.RenewPlanFullRoom(_appcontentEntity);

        BindData();
    }

}