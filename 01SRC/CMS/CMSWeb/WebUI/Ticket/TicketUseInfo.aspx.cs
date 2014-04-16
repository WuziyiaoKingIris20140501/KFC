using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class WebUI_Ticket_TicketUseInfo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["packagename"] = "";
            ViewState["amountfrom"] = "";
            ViewState["amountto"] = "";
            ViewState["pickfromdate"] = "";
            ViewState["picktodate"] = "";
            ViewState["tickettime"] = "";

            InitControlData();

            if (!String.IsNullOrEmpty(Request.QueryString["TYPE"]))
            {
                string strType = Request.QueryString["TYPE"].ToString();
                string pkn = HttpUtility.UrlDecode(Request.QueryString["pknm"], Encoding.GetEncoding("GB2312"));
                string atk = HttpUtility.UrlDecode(Request.QueryString["atk"], Encoding.GetEncoding("GB2312"));
                string att = HttpUtility.UrlDecode(Request.QueryString["att"], Encoding.GetEncoding("GB2312"));
                string pkf = HttpUtility.UrlDecode(Request.QueryString["pkf"], Encoding.GetEncoding("GB2312"));
                string pkt = HttpUtility.UrlDecode(Request.QueryString["pkt"], Encoding.GetEncoding("GB2312"));
                string tkt = HttpUtility.UrlDecode(Request.QueryString["tkt"], Encoding.GetEncoding("GB2312"));
                ViewState["packagetype"] = strType;

                ViewState["packagename"] = pkn;//优惠券礼包名
                ViewState["amountfrom"] = atk;
                ViewState["amountto"] = att;
                ViewState["pickfromdate"] = pkf;
                ViewState["picktodate"] = pkt;
                ViewState["tickettime"] = tkt;

                BindUseTicketInfo();
            }
            else
            {
                ViewState["packagetype"] = "";
                ViewState["packagename"] = "";
                ViewState["amountfrom"] = "";
                ViewState["amountto"] = "";
                ViewState["pickfromdate"] = "";
                ViewState["picktodate"] = "";
                ViewState["tickettime"] = "";
            }
            //SetEmptyDataTable();
            BindGridView();
        }
    }

    private void InitControlData()
    {
        BindDDpList();
    }

    private void BindDDpList()
    {
        DataTable dtTicketTD = GetTicketTimeData();
        ddpTicketTime.DataSource = dtTicketTD;
        ddpTicketTime.DataTextField = "TICKETTD_TEXT";
        ddpTicketTime.DataValueField = "TICKETTD_STATUS";
        ddpTicketTime.DataBind();
        ddpTicketTime.SelectedIndex = 0;
    }

    private DataTable GetTicketTimeData()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("TICKETTD_STATUS");
        DataColumn BookStatusText = new DataColumn("TICKETTD_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["TICKETTD_STATUS"] = "";
        dr0["TICKETTD_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            dr["TICKETTD_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["TICKETTD_TEXT"] = "已过领用期";
                    break;
                //case "1":
                //    dr["TICKETTD_TEXT"] = "已过使用期";
                //    break;
                case "1":
                    dr["TICKETTD_TEXT"] = "当前可领用";
                    break;
                //case "3":
                //    dr["TICKETTD_TEXT"] = "当前可使用";
                //    break;
                default:
                    dr["TICKETTD_TEXT"] = "未知状态";
                    break;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    /// <summary>
    /// 点击查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string packageName =  this.txtPackageName.Text.Trim();
        string amountFrom = this.txtAmountFrom.Text.Trim();
        string amountTo = this.txtAmountTo.Text.Trim();

        string pickFromDate = this.PickFromDate.Value.Trim();
        string pickToDate = this.PickToDate.Value.Trim();

        string ticketTime = ddpTicketTime.SelectedValue.Trim();

        //string useFromDate = this.UseFromDate.Value;
        //string useToDate = this.UseToDate.Value;

        ViewState["packagename"] = packageName;//优惠券礼包名
        ViewState["amountfrom"] = amountFrom;
        ViewState["amountto"] = amountTo;
        ViewState["pickfromdate"] = pickFromDate;
        ViewState["picktodate"] = pickToDate;
        ViewState["packagetype"] = "";

        ViewState["tickettime"] = ticketTime;

        hidPKName.Value = packageName;
        hidATFrom.Value = amountFrom;
        hidATTo.Value = amountTo;
        hidPKFrom.Value = pickFromDate;
        hidPKTo.Value = pickToDate;
        hidTKTime.Value = ticketTime;

        //ViewState["usefromdate"] = useFromDate;
        //ViewState["usetodate"] = useToDate;
        BindUseTicketInfo();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    private void BindUseTicketInfo()
    {
        DataTable dtOrder = getTicketUseInfo(getTicketUseData());
        GridviewControl.GridViewDataBind(this.gridViewTicketUseInfo, dtOrder);

        SetTicketTypeAreaData();
    }

    private void SetTicketTypeAreaData()
    {
        string packageName = ViewState["packagename"].ToString();//优惠券礼包名
        string amountFrom = ViewState["amountfrom"].ToString();
        string amountTo = ViewState["amountto"].ToString();
        string pickFromDate = ViewState["pickfromdate"].ToString();
        string pickToDate = ViewState["picktodate"].ToString();
        string packagetype = ViewState["packagetype"].ToString();
        string tickettime = ViewState["tickettime"].ToString();

        TicketInfoEntity _ticketInfoEntity = new TicketInfoEntity();
        _ticketInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _ticketInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _ticketInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _ticketInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _ticketInfoEntity.TicketInfoDBEntity = new List<TicketInfoDBEntity>();
        TicketInfoDBEntity ticketInfoDBEntity = new TicketInfoDBEntity();
        ticketInfoDBEntity.PackageName = packageName;
        ticketInfoDBEntity.AmountFrom = amountFrom;
        ticketInfoDBEntity.AmountTo = amountTo;
        ticketInfoDBEntity.Pickfromdate = pickFromDate;
        ticketInfoDBEntity.Picktodate = pickToDate;
        ticketInfoDBEntity.PackageType = packagetype;
        ticketInfoDBEntity.TicketTime = tickettime;

        _ticketInfoEntity.TicketInfoDBEntity.Add(ticketInfoDBEntity);
        DataSet dsResult = TicketInfoBP.BindTicketInfoList(_ticketInfoEntity).QueryResult;

        decimal decTpOperate = 0;
        decimal decTpMarket = 0;
        decimal decTpOther = 0;

        decimal decAuOperate = 0;
        decimal decAuMarket = 0;
        decimal decAuOther = 0;

        decimal decOuOperate = 0;
        decimal decOuMarket = 0;
        decimal decOuOther = 0;

        if (dsResult.Tables.Count > 0)
        {
            #region 运营 优惠券
            foreach (DataRow drpg in dsResult.Tables["ALLPG"].Rows)
            {
                if ("0".Equals(drpg["packagetype"].ToString().Trim()))
                {
                    decTpOperate = decTpOperate + ConverDataDec(drpg["ALLPG"].ToString().Trim());
                }
                else if ("1".Equals(drpg["packagetype"].ToString().Trim()))
                {
                    decTpMarket = decTpMarket + ConverDataDec(drpg["ALLPG"].ToString().Trim());
                }
                else
                {
                    decTpOther = decTpOther + ConverDataDec(drpg["ALLPG"].ToString().Trim());
                }
            }
            #endregion

            #region 领取用户总数
            foreach (DataRow drUr in dsResult.Tables["ALLUSER"].Rows)
            {
                if ("0".Equals(drUr["packagetype"].ToString().Trim()))
                {
                    decAuOperate = decAuOperate + ConverDataDec(drUr["ALLUSER"].ToString().Trim());
                }
                else if ("1".Equals(drUr["packagetype"].ToString().Trim()))
                {
                    decAuMarket = decAuMarket + ConverDataDec(drUr["ALLUSER"].ToString().Trim());
                }
                else
                {
                    decAuOther = decAuOther + ConverDataDec(drUr["ALLUSER"].ToString().Trim());
                }
            }
            #endregion

            #region 使用用户总数
            foreach (DataRow drOUr in dsResult.Tables["ORDUSER"].Rows)
            {
                if ("0".Equals(drOUr["packagetype"].ToString().Trim()))
                {
                    decOuOperate = decOuOperate + ConverDataDec(drOUr["ORDUSER"].ToString().Trim());
                }
                else if ("1".Equals(drOUr["packagetype"].ToString().Trim()))
                {
                    decOuMarket = decOuMarket + ConverDataDec(drOUr["ORDUSER"].ToString().Trim());
                }
                else
                {
                    decOuOther = decOuOther + ConverDataDec(drOUr["ORDUSER"].ToString().Trim());
                }
            }
            #endregion

            // 运营
            // 共有优惠券活动
            lbTicketOperate.Text = decTpOperate.ToString();
            // 共领用用户数
            lbOperateAllUser.Text = decAuOperate.ToString();
            // 共使用用户数
            lbOperateUsed.Text = decOuOperate.ToString();

            // 市场
            // 共有优惠券活动
            lbTickeMarket.Text = decTpMarket.ToString();
            // 共领用用户数
            lbMarketAllUser.Text = decAuMarket.ToString();
            // 共使用用户数
            lbMarketUsed.Text = decOuMarket.ToString();

            // 其他
            // 共有优惠券活动
            lbTicketOther.Text = decTpOther.ToString();
            // 共领用用户数
            lbOtherAllUser.Text = decAuOther.ToString();
            // 共使用用户数
            lbOtherUsed.Text = decOuOther.ToString();

            #region 优惠券订单总数
            decimal decOrLm = 0;
            decimal decOrLm2 = 0;
            decimal decOrbarb = 0;

            if (dsResult.Tables["ALLORD"].Rows.Count > 0)
            {
                DataRow[] drListOp = dsResult.Tables["ALLORD"].Select("packagetype='0'");
                foreach (DataRow drOp in drListOp)
                {
                    if ("lmbar".Equals(drOp["price_code"].ToString().Trim().ToLower()))
                    {
                        decOrLm = decOrLm + ConverDataDec(drOp["ALLORD"].ToString().Trim());
                    }
                    else if ("lmbar2".Equals(drOp["price_code"].ToString().Trim().ToLower()))
                    {
                        decOrLm2 = decOrLm2 + ConverDataDec(drOp["ALLORD"].ToString().Trim());
                    }
                    else
                    {
                        decOrbarb = decOrbarb + ConverDataDec(drOp["ALLORD"].ToString().Trim());
                    }
                }

                lbOperateLmOrder.Text = decOrLm.ToString();
                lbOperateLm2Order.Text = decOrLm2.ToString();
                lbOperateBarbOrder.Text = decOrbarb.ToString();

                decOrLm = 0;
                decOrLm2 = 0;
                decOrbarb = 0;

                DataRow[] drListMt = dsResult.Tables["ALLORD"].Select("packagetype='1'");
                foreach (DataRow drMt in drListMt)
                {
                    if ("lmbar".Equals(drMt["price_code"].ToString().Trim().ToLower()))
                    {
                        decOrLm = decOrLm + ConverDataDec(drMt["ALLORD"].ToString().Trim());
                    }
                    else if ("lmbar2".Equals(drMt["price_code"].ToString().Trim().ToLower()))
                    {
                        decOrLm2 = decOrLm2 + ConverDataDec(drMt["ALLORD"].ToString().Trim());
                    }
                    else
                    {
                        decOrbarb = decOrbarb + ConverDataDec(drMt["ALLORD"].ToString().Trim());
                    }
                }

                lbMarketLmOrder.Text = decOrLm.ToString();
                lbMarketLm2Order.Text = decOrLm2.ToString();
                lbMarketBarbOrder.Text = decOrbarb.ToString();

                decOrLm = 0;
                decOrLm2 = 0;
                decOrbarb = 0;

                DataRow[] drListOt = dsResult.Tables["ALLORD"].Select("packagetype='2'");
                foreach (DataRow drOt in drListOt)
                {
                    if ("lmbar".Equals(drOt["price_code"].ToString().Trim().ToLower()))
                    {
                        decOrLm = decOrLm + ConverDataDec(drOt["ALLORD"].ToString().Trim());
                    }
                    else if ("lmbar2".Equals(drOt["price_code"].ToString().Trim().ToLower()))
                    {
                        decOrLm2 = decOrLm2 + ConverDataDec(drOt["ALLORD"].ToString().Trim());
                    }
                    else
                    {
                        decOrbarb = decOrbarb + ConverDataDec(drOt["ALLORD"].ToString().Trim());
                    }
                }

                lbOtherLmOrder.Text = decOrLm.ToString();
                lbOtherLm2Order.Text = decOrLm2.ToString();
                lbOtherBarbOrder.Text = decOrbarb.ToString();
            }
            else
            {
                lbOperateLmOrder.Text = "0";
                lbOperateLm2Order.Text = "0";
                lbOperateBarbOrder.Text = "0";

                lbMarketLmOrder.Text = "0";
                lbMarketLm2Order.Text = "0";
                lbMarketBarbOrder.Text = "0";

                lbOtherLmOrder.Text = "0";
                lbOtherLm2Order.Text = "0";
                lbOtherBarbOrder.Text = "0";
            }
            #endregion

            #region 优惠券成功订单总数
            decimal decSuOrLm = 0;
            decimal decSuOrLm2 = 0;
            decimal decSuOrbarb = 0;

            if (dsResult.Tables["SUCORD"].Rows.Count > 0)
            {
                DataRow[] drSuOp = dsResult.Tables["SUCORD"].Select("packagetype='0'");
                foreach (DataRow drAOr in drSuOp)
                {
                    if ("lmbar".Equals(drAOr["price_code"].ToString().Trim().ToLower()))
                    {
                        decSuOrLm = decSuOrLm + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                    else if ("lmbar2".Equals(drAOr["price_code"].ToString().Trim().ToLower()))
                    {
                        decSuOrLm2 = decSuOrLm2 + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                    else
                    {
                        decSuOrbarb = decSuOrbarb + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                }

                lbOperateLmSuOrder.Text = decSuOrLm.ToString();
                lbOperateLm2SuOrder.Text = decSuOrLm2.ToString();
                lbOperateBarbSuOrder.Text = decSuOrbarb.ToString();

                decSuOrLm = 0;
                decSuOrLm2 = 0;
                decSuOrbarb = 0;

                DataRow[] drSuOr = dsResult.Tables["SUCORD"].Select("packagetype='1'");
                foreach (DataRow drAOr in drSuOr)
                {
                    if ("lmbar".Equals(drAOr["price_code"].ToString().Trim().ToLower()))
                    {
                        decSuOrLm = decSuOrLm + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                    else if ("lmbar2".Equals(drAOr["price_code"].ToString().Trim().ToLower()))
                    {
                        decSuOrLm2 = decSuOrLm2 + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                    else
                    {
                        decSuOrbarb = decSuOrbarb + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                }

                lbMarketLmSuOrder.Text = decSuOrLm.ToString();
                lbMarketLm2SuOrder.Text = decSuOrLm2.ToString();
                lbMarketBarbSuOrder.Text = decSuOrbarb.ToString();

                decSuOrLm = 0;
                decSuOrLm2 = 0;
                decSuOrbarb = 0;

                DataRow[] drSuOt = dsResult.Tables["SUCORD"].Select("packagetype='2'");
                foreach (DataRow drAOr in drSuOt)
                {
                    if ("lmbar".Equals(drAOr["price_code"].ToString().Trim().ToLower()))
                    {
                        decSuOrLm = decSuOrLm + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                    else if ("lmbar2".Equals(drAOr["price_code"].ToString().Trim().ToLower()))
                    {
                        decSuOrLm2 = decSuOrLm2 + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                    else
                    {
                        decSuOrbarb = decSuOrbarb + ConverDataDec(drAOr["SUCORD"].ToString().Trim());
                    }
                }

                lbOtherLmSuOrder.Text = decSuOrLm.ToString();
                lbOtherLm2SuOrder.Text = decSuOrLm2.ToString();
                lbOtherBarbSuOrder.Text = decSuOrbarb.ToString();
            }
            else
            {
                lbOperateLmSuOrder.Text = "0";
                lbOperateLm2SuOrder.Text = "0";
                lbOperateBarbSuOrder.Text = "0";

                lbMarketLmSuOrder.Text = "0";
                lbMarketLm2SuOrder.Text = "0";
                lbMarketBarbSuOrder.Text = "0";

                lbOtherLmSuOrder.Text = "0";
                lbOtherLm2SuOrder.Text = "0";
                lbOtherBarbSuOrder.Text = "0";
            }
            #endregion

        }
        else
        {
            lbTicketOperate.Text = "0";
            lbOperateAllUser.Text = "0";
            lbOperateUsed.Text = "0";
            lbOperateLmOrder.Text = "0";
            lbOperateLm2Order.Text = "0";
            lbOperateBarbOrder.Text = "0";
            lbOperateLmSuOrder.Text = "0";
            lbOperateLm2SuOrder.Text = "0";
            lbOperateBarbSuOrder.Text = "0";

            lbTickeMarket.Text = "0";
            lbMarketAllUser.Text = "0";
            lbMarketUsed.Text = "0";
            lbMarketLmOrder.Text = "0";
            lbMarketLm2Order.Text = "0";
            lbMarketBarbOrder.Text = "0";
            lbMarketLmSuOrder.Text = "0";
            lbMarketLm2SuOrder.Text = "0";
            lbMarketBarbSuOrder.Text = "0";

            lbTicketOther.Text = "0";
            lbOtherAllUser.Text = "0";
            lbOtherUsed.Text = "0";
            lbOtherLmOrder.Text = "0";
            lbOtherLm2Order.Text = "0";
            lbOtherBarbOrder.Text = "0";
            lbOtherLmSuOrder.Text = "0";
            lbOtherLm2SuOrder.Text = "0";
            lbOtherBarbSuOrder.Text = "0";
        }

        // 运营
        // 共有优惠券活动
        //lbTicketOperate.Text = decTpOperate.ToString();
        // 共领用用户数
        //lbOperateAllUser.Text = decAuOperate.ToString();
        // 共使用用户数
        //lbOperateUsed.Text = decOuOperate.ToString();
        //// 总产生订单
        //lbOperateLmOrder.Text = "";
        //lbOperateLm2Order.Text = "";
        //lbOperateBarbOrder.Text = "";
        //// 总产生成功订单
        //lbOperateLmSuOrder.Text = "";
        //lbOperateLm2SuOrder.Text = "";
        //lbOperateBarbSuOrder.Text = "";



        // 市场
        // 共有优惠券活动
        //lbTickeMarket.Text = decTpMarket.ToString();
        //// 共领用用户数
        //lbMarketAllUser.Text = decAuMarket.ToString();
        //// 共使用用户数
        //lbMarketUsed.Text = decOuMarket.ToString();
        //// 总产生订单
        //lbMarketLmOrder.Text = "";
        //lbMarketLm2Order.Text = "";
        //lbMarketBarbOrder.Text = "";
        //// 总产生成功订单
        //lbMarketLmSuOrder.Text = "";
        //lbMarketLm2SuOrder.Text = "";
        //lbMarketBarbSuOrder.Text = "";



        // 其他
        // 共有优惠券活动
        //lbTicketOther.Text = decTpOther.ToString();
        //// 共领用用户数
        //lbOtherAllUser.Text = decAuOther.ToString();
        //// 共使用用户数
        //lbOtherUsed.Text = decOuOther.ToString();
        // 总产生订单
        //lbOtherLmOrder.Text = "";
        //lbOtherLm2Order.Text = "";
        //lbOtherBarbOrder.Text = "";
        //// 总产生成功订单
        //lbOtherLmSuOrder.Text = "";
        //lbOtherLm2SuOrder.Text = "";
        //lbOtherBarbSuOrder.Text = "";
    }

    private decimal ConverDataDec(string param)
    {
        decimal decResult = 0;

        if (String.IsNullOrEmpty(param))
        {
            return decResult;
        }

        try
        {
            decResult = decimal.Parse(param);
        }
        catch
        {
            decResult = 0;
        }
        return decResult;
    }

    private DataSet getTicketUseData()
    {
        string sql = string.Empty;      
        string packageName = ViewState["packagename"].ToString();//优惠券礼包名
        string amountFrom = ViewState["amountfrom"].ToString();
        string amountTo = ViewState["amountto"].ToString();
        string pickFromDate = ViewState["pickfromdate"].ToString();
        string pickToDate = ViewState["picktodate"].ToString();
        //string useFromDate = ViewState["usefromdate"].ToString();
        //string useToDate = ViewState["usetodate"].ToString();
        string packagetype = ViewState["packagetype"].ToString();
        string tickettime = ViewState["tickettime"].ToString();

        sql = "select p.PACKAGECODE,p.PACKAGENAME,p.STARTDATE,p.ENDDATE,p.AMOUNT,p.USERCNT,t.TicketCount from t_lm_ticket_package p ";
        sql += " left Join ";
        sql += " (select Count(TICKETCNT) as TicketCount,PACKAGECODE from t_lm_ticket group by PACKAGECODE) t";
        sql += " on p.PACKAGECODE = t.PACKAGECODE where 1=1 ";
        sql += " and ((:PACKAGENAME IS NULL) OR (p.PACKAGENAME LIKE '%'||:PACKAGENAME||'%'))";
        sql += " and ((:AMOUNTFROM IS NULL) OR (p.AMOUNT >= :AMOUNTFROM))";
        sql += " and ((:AMOUNTTO IS NULL) OR (p.AMOUNT <= :AMOUNTTO))";
        sql += " and ((:STARTDATE IS NULL) OR (p.STARTDATE >= :STARTDATE)) ";//领用开始日期
        sql += " and ((:ENDDATE IS NULL) OR (p.STARTDATE <= :ENDDATE)) ";
        sql += " and ((:PACKAGETYPE IS NULL) OR (p.packagetype = :PACKAGETYPE)) ";
        sql += " and ((:TICKETDT IS NULL) OR (:TICKETDT = '0' AND trunc(sysdate,'dd') > TO_DATE(p.enddate, 'yyyy-mm-dd')) OR (:TICKETDT = '1' AND trunc(sysdate,'dd') <= TO_DATE(p.enddate, 'yyyy-mm-dd') AND trunc(sysdate,'dd') >= TO_DATE(p.startdate, 'yyyy-mm-dd'))) order by p.ID desc";//领用日期       

        OracleParameter[] parm ={
                                    new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),     
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar)
                                };

        if (String.IsNullOrEmpty(packageName))
        {
            parm[0].Value = DBNull.Value;
        }
        else
        {
            parm[0].Value = packageName;
        }

        if (String.IsNullOrEmpty(amountFrom))
        {
            parm[1].Value = DBNull.Value;
        }
        else
        {
            parm[1].Value = Convert.ToInt32(amountFrom);
        }

        if (String.IsNullOrEmpty(amountTo))
        {
            parm[2].Value = DBNull.Value;
        }
        else
        {
            parm[2].Value = Convert.ToInt32(amountTo);
        }

        if (String.IsNullOrEmpty(pickFromDate))
        {
            parm[3].Value = DBNull.Value;
        }
        else
        {
            parm[3].Value = pickFromDate;
        }

        if (String.IsNullOrEmpty(pickToDate))
        {
            parm[4].Value = DBNull.Value;
        }
        else
        {
            parm[4].Value = pickToDate;
        }

        if (String.IsNullOrEmpty(packagetype))
        {
            parm[5].Value = DBNull.Value;
        }
        else
        {
            parm[5].Value = packagetype;
        }

        if (String.IsNullOrEmpty(tickettime))
        {
            parm[6].Value = DBNull.Value;
        }
        else
        {
            parm[6].Value = tickettime;
        }

        DataSet ds = DbHelperOra.Query(sql, false, parm);
        return ds;
    }
    
    //根据得到的DataSet，得出一个手动创建的DataTable
    private DataTable getTicketUseInfo(DataSet ds)
    {    
        DataTable dt0 = ds.Tables[0];
        //手动创建DataTable表
        DataTable dt = new DataTable();
        dt.Columns.Add("PACKAGECODE");
        dt.Columns.Add("PACKAGENAME");
        dt.Columns.Add("STARTDATE_ENDDATE");
        dt.Columns.Add("AMOUNT");
        dt.Columns.Add("TICKETCOUNT");
        dt.Columns.Add("USERCNT");

        for (int i = 0; i < dt0.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["PACKAGECODE"] = dt0.Rows[i]["PACKAGECODE"].ToString();
            dr["PACKAGENAME"] = dt0.Rows[i]["PACKAGENAME"].ToString();
            dr["AMOUNT"] = dt0.Rows[i]["AMOUNT"].ToString();
            string startdate = dt0.Rows[i]["STARTDATE"].ToString();
            string enddate = dt0.Rows[i]["ENDDATE"].ToString();
            dr["STARTDATE_ENDDATE"] = string.Format("{0:yyyy-MM-dd}", startdate) + "——" + string.Format("{0:yyyy-MM-dd}", enddate);

            dr["TICKETCOUNT"] = dt0.Rows[i]["TICKETCOUNT"].ToString();
            dr["USERCNT"] = dt0.Rows[i]["USERCNT"].ToString();

            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void SetEmptyDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn PACKAGECODE_dc = new DataColumn("PACKAGECODE");
        DataColumn PACKAGENAME_dc = new DataColumn("PACKAGENAME");
        DataColumn STARTDATE_ENDDATE_dc = new DataColumn("STARTDATE_ENDDATE");
        DataColumn AMOUNT_dc = new DataColumn("AMOUNT");
        DataColumn TICKETCOUNT_dc = new DataColumn("TICKETCOUNT");
        DataColumn USERCNT_dc = new DataColumn("USERCNT");

        dt.Columns.Add(PACKAGECODE_dc);
        dt.Columns.Add(PACKAGENAME_dc);
        dt.Columns.Add(STARTDATE_ENDDATE_dc);
        dt.Columns.Add(AMOUNT_dc);
        dt.Columns.Add(TICKETCOUNT_dc);
        dt.Columns.Add(USERCNT_dc);
        GridviewControl.GridViewDataBind(this.gridViewTicketUseInfo, dt);
    }

    //翻页
    protected void gridViewTicketUseInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {      
        this.gridViewTicketUseInfo.PageIndex = e.NewPageIndex;
        BindUseTicketInfo();
    }


    #region for searchPachage
    void BindGridView()
    {
        string strSql = "select distinct id,packagecode,usercnt,startdate,enddate,packagename from t_lm_ticket_package where 1=1 ";
        if (this.txtPackageNameSearch.Text != "")
        {
            strSql += " and packagename like '%" + CommonFunction.StringFilter(this.txtPackageNameSearch.Text) + "%'";
        }

        if (this.txtPackageCodeSearch.Text != "")
        {
            strSql += " and packagecode=  '" + CommonFunction.StringFilter(this.txtPackageCodeSearch.Text) + "'";
        }

        //if (!String.IsNullOrEmpty(ViewState["packagetype"].ToString().Trim()))
        //{
        //    strSql += " and packagetype = '" + CommonFunction.StringFilter(ViewState["packagetype"].ToString().Trim()) + "'";
        //}

        strSql += " order by ID desc";

        DataSet ds = DbHelperOra.Query(strSql, false);
        DataView view = ds.Tables[0].DefaultView;
        this.myGridView.DataSource = view;
        this.myGridView.DataBind();

    }

    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        myGridView.PageIndex = e.NewPageIndex;
        BindGridView();
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "ggg", "AddNew()", true);
    }

    protected void btnSearch2_Click(object sender, EventArgs e)
    {
        BindGridView();
        //Page.RegisterStartupScript("ggg", "<script>invokeOpen2(); </script>");
        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "search", "AddNew()", true);
    }
    
    protected void myGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selIndex = myGridView.SelectedIndex;
        string strPackageCode = myGridView.Rows[selIndex].Cells[1].Text.ToString();
        string strPackageName = myGridView.Rows[selIndex].Cells[2].Text.ToString();
        //string strTicketCount = myGridView.Rows[selIndex].Cells[3].Text.ToString();//总共可以生产的张数

        //int userCount = Convert.ToInt32(strTicketCount);
        //int restCount = RestCount(strPackageCode, userCount);

        //this.lblRestCount.Text = restCount.ToString();
        //this.txtPackageCode.Value = strPackageCode;
        //this.txtTicketCount.Value = strTicketCount;
        //Page.RegisterStartupScript("ggg", "<script>invokeClose2(); </script>");
        ViewState["packagetype"] = "";
        this.txtPackageName.Text = strPackageName;//优惠券名称
        btnSearch_Click(null, null);//增加调用查询功能

        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "select", "invokeClose2()", true);
    }
    #endregion
}