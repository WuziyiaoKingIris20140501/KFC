using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;

using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.DataAccess;
using HotelVp.CMS.Domain.ServiceAdapter;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class APPContentBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.APPContentBP  Method: ";

        public static APPContentEntity CommonSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);
            try
            {
                if (appcontentEntity.APPContentDBEntity.Count > 0 && "1".Equals(appcontentEntity.APPContentDBEntity[0].SerVer))
                {
                    return APPContentSA.CommonSelect(appcontentEntity);
                }
                else
                {
                    return APPContentV2SA.CommonSelect(appcontentEntity);
                }
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity PopGridSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "PopGridSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.PopGridSelect(appcontentEntity); //APPContentDA.CommonSelect(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "PopGridSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity ReviewSalesPlanDetail(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "ReviewSalesPlanDetail";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.ReviewSalesPlanDetail(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "ReviewSalesPlanDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity ChkHotelLowLimitPrice(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "ChkHotelLowLimitPrice";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.ChkHotelLowLimitPrice(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "ChkHotelLowLimitPrice  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity ReviewSalesPlanDetailHistory(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "ReviewSalesPlanDetailHistory";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                DataSet dsResult = new DataSet();
                dsResult.Tables.Add(new DataTable());
                dsResult.Tables[0].Columns.Add("SAVETYPENM");
                dsResult.Tables[0].Columns.Add("PLANTIME");
                dsResult.Tables[0].Columns.Add("PLANSTART");
                dsResult.Tables[0].Columns.Add("PLANEND");
                dsResult.Tables[0].Columns.Add("PLANWEEK");
                dsResult.Tables[0].Columns.Add("UPDATETIME");
                dsResult.Tables[0].Columns.Add("UPDATEUSER");

                DataSet dsHistory = APPContentDA.ReviewSalesPlanDetailHistory(appcontentEntity);
                string strContent = string.Empty;
                string strSaveType = string.Empty;
                string strPlanTime = string.Empty;
                string strPlanWeek = string.Empty;
                string strPlanLen = string.Empty;
                foreach (DataRow drRow in dsHistory.Tables[0].Rows)
                {
                    DataRow drIn = dsResult.Tables[0].NewRow();
                    drIn["UPDATEUSER"] = drRow["USERNAME"].ToString();
                    drIn["UPDATETIME"] = drRow["CREATEDATE"].ToString();

                    strContent = drRow["EVENT_CONTENT"].ToString();
                    strSaveType = strContent.Substring(strContent.IndexOf("保存计划类型："), (strContent.IndexOf("保存计划时间：") - strContent.IndexOf("保存计划类型：")));
                    strPlanTime = strContent.Substring(strContent.IndexOf("保存计划时间："), (strContent.IndexOf("保存计划持续时间：") - strContent.IndexOf("保存计划时间：")));
                    strPlanWeek = strContent.Substring(strContent.IndexOf("保存计划持续星期："), (strContent.IndexOf("计划ID：") - strContent.IndexOf("保存计划持续星期：")));
                    strPlanLen = strContent.Substring(strContent.IndexOf("保存计划持续时间："), (strContent.IndexOf("保存计划持续星期：") - strContent.IndexOf("保存计划持续时间：")));

                    strSaveType = strSaveType.Substring(strSaveType.IndexOf("：") + 1).Trim();
                    strPlanLen = strPlanLen.Substring(strPlanLen.IndexOf("：") + 1).Trim();

                    if ("0".Equals(strSaveType))
                    {
                        drIn["SAVETYPENM"] = "立即保存";
                    }
                    else if ("1".Equals(strSaveType))
                    {
                        drIn["SAVETYPENM"] = "定时保存";
                    }
                    else if ("2".Equals(strSaveType))
                    {
                        drIn["SAVETYPENM"] = "每日自动更新";
                    }

                    drIn["PLANTIME"] = strPlanTime.Substring(strPlanTime.IndexOf("：") + 1).Trim();
                    drIn["PLANWEEK"] = strPlanWeek.Substring(strPlanWeek.IndexOf("：") + 1).Trim();
                    drIn["PLANSTART"] = (strPlanLen.Length > 10) ? strPlanLen.Substring(0,10).Trim() : "";
                    drIn["PLANEND"] = (strPlanLen.Length > 10) ? strPlanLen.Substring(11).Trim() : "";
                    dsResult.Tables[0].Rows.Add(drIn);
                }

                appcontentEntity.QueryResult = dsResult;
                return appcontentEntity;
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "ReviewSalesPlanDetailHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity UpdateSalesPlanStatus(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "UpdateSalesPlanStatus";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.UpdateSalesPlanStatus(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "UpdateSalesPlanStatus  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity UpdateSalesPlanList(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "UpdateSalesPlanList";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
                APPContentDA.UpdateSalesPlanManager(appcontentEntity);
                APPContentDA.UpdateSalesPlanEventDetail(appcontentEntity);

                if ("1".Equals(dbParm.SaveType))
                {
                    APPContentDA.UpdateSalesPlanEventJobList(appcontentEntity, "0");
                    if ("1".Equals(dbParm.PlanStatus))
                    {
                        APPContentDA.CreateSalesPlanEventJobList(appcontentEntity, int.Parse(dbParm.PlanID));
                    }
                    appcontentEntity.Result = 1;
                }
                else if ("2".Equals(dbParm.SaveType))
                {
                    if (!ChkPlanJobAction(appcontentEntity))
                    {
                        DateTime dtStart = DateTime.Parse(dbParm.PlanStart);
                        appcontentEntity.APPContentDBEntity[0].PlanStart = dtStart.AddDays(1).ToShortDateString();
                        appcontentEntity.Result = 2;
                    }
                    else
                    {
                        appcontentEntity.Result = 1;
                    }

                    APPContentDA.UpdateSalesPlanEventJobListForTime(appcontentEntity, "0");
                    if ("1".Equals(dbParm.PlanStatus))
                    {
                        APPContentDA.CreateSalesPlanEventJobList(appcontentEntity, int.Parse(dbParm.PlanID));
                    }
                }

                return appcontentEntity;
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "UpdateSalesPlanList  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static bool ChkPlanJobAction(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "ChkPlanJobAction";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);
            bool bResult = true;

            if (DateTime.Parse(appcontentEntity.APPContentDBEntity[0].PlanStart) > DateTime.Parse(DateTime.Now.ToShortDateString()))
            {
                return bResult;
            }

            try
            {
                APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
                DataSet dsResult = APPContentDA.CheckSalesPlanEventJobAction(appcontentEntity);

                if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    bResult = false;
                }

                return bResult;
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "ChkPlanJobAction  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity ReviewSalesPlan(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "ReviewSalesPlan";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.ReviewSalesPlan(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "ReviewSalesPlan  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        //public static APPContentEntity ReviewSalesPlanCount(APPContentEntity appcontentEntity)
        //{
        //    appcontentEntity.LogMessages.MsgType = MessageType.INFO;
        //    appcontentEntity.LogMessages.Content = _nameSpaceClass + "ReviewSalesPlanCount";
        //    LoggerHelper.LogWriter(appcontentEntity.LogMessages);

        //    try
        //    {
        //        return APPContentDA.ReviewSalesPlanCount(appcontentEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
        //        appcontentEntity.LogMessages.Content = _nameSpaceClass + "ReviewSalesPlanCount  Error: " + ex.Message;
        //        LoggerHelper.LogWriter(appcontentEntity.LogMessages);
        //        throw ex;
        //    }
        //}

        public static APPContentEntity PopHotelGridSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "PopHotelGridSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.PopHotelGridSelect(appcontentEntity); //APPContentDA.CommonSelect(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "PopHotelGridSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity GetFullRoomHistoryList(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "GetFullRoomHistoryList";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.GetFullRoomHistoryList(appcontentEntity); //APPContentDA.CommonSelect(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "GetFullRoomHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity GetHotelFogList(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "GetHotelFogList";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                DataSet dsHotel = APPContentDA.GetHotelFogList(appcontentEntity).QueryResult;
                ArrayList alIgnore = new ArrayList();
                DataSet dsRestult = new DataSet();
                dsRestult.Tables.Add(new DataTable());
                dsRestult.Tables[0].Columns.Add("HOTELID");
                dsRestult.Tables[0].Columns.Add("HOTELNM");
                dsRestult.Tables[0].Columns.Add("TYPEID");
                dsRestult.Tables[0].Columns.Add("TYPENM");
                dsRestult.Tables[0].Columns.Add("HVPVAL");
                dsRestult.Tables[0].Columns.Add("HUBVAL");
                string desc = "";
                string desc_f = "";

                foreach (DataRow drHotel in dsHotel.Tables[0].Rows)
                {
                    alIgnore = APPContentDA.GetHotelCompare(drHotel["HOTELID"].ToString());
                    if (!alIgnore.Contains("HOTELNMZH") && !drHotel["HOTELNMZH"].ToString().Trim().Equals(drHotel["HOTELNMZH_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "HOTELNMZH";
                        drErNm["TYPENM"] = "酒店名称";
                        drErNm["HVPVAL"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["HUBVAL"] = drHotel["HOTELNMZH_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("HOTELNMEN") && !drHotel["HOTELNMEN"].ToString().Trim().Equals(drHotel["HOTELNMEN_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "HOTELNMEN";
                        drErNm["TYPENM"] = "酒店英文名称";
                        drErNm["HVPVAL"] = drHotel["HOTELNMEN"].ToString();
                        drErNm["HUBVAL"] = drHotel["HOTELNMEN_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("FOGSTATUS") && !drHotel["FOGSTATUS"].ToString().Trim().Equals(drHotel["FOGSTATUS_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "FOGSTATUS";
                        drErNm["TYPENM"] = "FOG酒店上下线状态";
                        drErNm["HVPVAL"] = drHotel["FOGSTATUS"].ToString();
                        drErNm["HUBVAL"] = drHotel["FOGSTATUS_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("CITY") && !drHotel["CITY"].ToString().Trim().Equals(drHotel["CITY_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "CITY";
                        drErNm["TYPENM"] = "酒店所在城市";
                        drErNm["HVPVAL"] = drHotel["CITY"].ToString();
                        drErNm["HUBVAL"] = drHotel["CITY_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("DIAMOND") && !drHotel["DIAMOND"].ToString().Trim().Equals(drHotel["DIAMOND_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "DIAMOND";
                        drErNm["TYPENM"] = "酒店钻石级";
                        drErNm["HVPVAL"] = drHotel["DIAMOND"].ToString();
                        drErNm["HUBVAL"] = drHotel["DIAMOND_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("STAR") && !drHotel["STAR"].ToString().Trim().Equals(drHotel["STAR_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "STAR";
                        drErNm["TYPENM"] = "酒店星级";
                        drErNm["HVPVAL"] = drHotel["STAR"].ToString();
                        drErNm["HUBVAL"] = drHotel["STAR_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("OPENDT") && !drHotel["OPENDT"].ToString().Trim().Equals(drHotel["OPENDT_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "OPENDT";
                        drErNm["TYPENM"] = "开业日期";
                        drErNm["HVPVAL"] = drHotel["OPENDT"].ToString();
                        drErNm["HUBVAL"] = drHotel["OPENDT_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("RENOVATIONDT") && !drHotel["RENOVATIONDT"].ToString().Trim().Equals(drHotel["RENOVATIONDT_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "RENOVATIONDT";
                        drErNm["TYPENM"] = "开业日期";
                        drErNm["HVPVAL"] = drHotel["RENOVATIONDT"].ToString();
                        drErNm["HUBVAL"] = drHotel["RENOVATIONDT_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("TRADEAREA") && (!drHotel["TRADEAREA"].ToString().Trim().Equals(drHotel["TRADEAREA_F"].ToString().Trim()) || !drHotel["TRADEAREANM"].ToString().Trim().Equals(drHotel["TRADEAREANM_F"].ToString().Trim())))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "TRADEAREA";
                        drErNm["TYPENM"] = "酒店商圈";
                        drErNm["HVPVAL"] = drHotel["TRADEAREANM"].ToString();
                        drErNm["HUBVAL"] = drHotel["TRADEAREANM_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("ADDRESS") && !drHotel["ADDRESS"].ToString().Trim().Equals(drHotel["ADDRESS_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "ADDRESS";
                        drErNm["TYPENM"] = "酒店地址";
                        drErNm["HVPVAL"] = drHotel["ADDRESS"].ToString();
                        drErNm["HUBVAL"] = drHotel["ADDRESS_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("WEBSITE") && !drHotel["WEBSITE"].ToString().Trim().Equals(drHotel["WEBSITE_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "WEBSITE";
                        drErNm["TYPENM"] = "酒店网址";
                        drErNm["HVPVAL"] = drHotel["WEBSITE"].ToString();
                        drErNm["HUBVAL"] = drHotel["WEBSITE_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("LINKTEL") && !drHotel["LINKTEL"].ToString().Trim().Equals(drHotel["LINKTEL_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "LINKTEL";
                        drErNm["TYPENM"] = "预订电话";
                        drErNm["HVPVAL"] = drHotel["LINKTEL"].ToString();
                        drErNm["HUBVAL"] = drHotel["LINKTEL_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("LINKFAX") && !drHotel["LINKFAX"].ToString().Trim().Equals(drHotel["LINKFAX_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "LINKFAX";
                        drErNm["TYPENM"] = "预订传真";
                        drErNm["HVPVAL"] = drHotel["LINKFAX"].ToString();
                        drErNm["HUBVAL"] = drHotel["LINKFAX_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("LINKMAN") && !drHotel["LINKMAN"].ToString().Trim().Equals(drHotel["LINKMAN_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "LINKMAN";
                        drErNm["TYPENM"] = "联系人";
                        drErNm["HVPVAL"] = drHotel["LINKMAN"].ToString();
                        drErNm["HUBVAL"] = drHotel["LINKMAN_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("LINKMAIL") && !drHotel["LINKMAIL"].ToString().Trim().Equals(drHotel["LINKMAIL_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "LINKMAIL";
                        drErNm["TYPENM"] = "联系邮箱";
                        drErNm["HVPVAL"] = drHotel["LINKMAIL"].ToString();
                        drErNm["HUBVAL"] = drHotel["LINKMAIL_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("LONGITUDE") && !drHotel["LONGITUDE"].ToString().Trim().Equals(drHotel["LONGITUDE_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "LONGITUDE";
                        drErNm["TYPENM"] = "经度";
                        drErNm["HVPVAL"] = drHotel["LONGITUDE"].ToString();
                        drErNm["HUBVAL"] = drHotel["LONGITUDE_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    if (!alIgnore.Contains("LATITUDE") && !drHotel["LATITUDE"].ToString().Trim().Equals(drHotel["LATITUDE_F"].ToString().Trim()))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "LATITUDE";
                        drErNm["TYPENM"] = "纬度";
                        drErNm["HVPVAL"] = drHotel["LATITUDE"].ToString();
                        drErNm["HUBVAL"] = drHotel["LATITUDE_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                    desc = (drHotel["DESCZH"].ToString().Trim().Length > 100) ? drHotel["DESCZH"].ToString().Trim().Substring(0, 100) : drHotel["DESCZH"].ToString().Trim();
                    desc_f = (drHotel["DESCZH_F"].ToString().Trim().Length > 100) ? drHotel["DESCZH_F"].ToString().Trim().Substring(0, 100) : drHotel["DESCZH_F"].ToString().Trim();

                    if (!alIgnore.Contains("DESCZH") && !desc.Equals(desc_f))
                    {
                        DataRow drErNm = dsRestult.Tables[0].NewRow();
                        drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                        drErNm["HOTELNM"] = drHotel["HOTELNMZH"].ToString();
                        drErNm["TYPEID"] = "DESCZH";
                        drErNm["TYPENM"] = "酒店详情";
                        drErNm["HVPVAL"] = drHotel["DESCZH"].ToString();
                        drErNm["HUBVAL"] = drHotel["DESCZH_F"].ToString();
                        dsRestult.Tables[0].Rows.Add(drErNm);
                    }

                }

                appcontentEntity.QueryResult = dsRestult;
                return appcontentEntity;

            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "GetHotelFogList  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertHotelIgnoreGrid(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelIgnoreGrid";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.InsertHotelIgnoreGrid(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelIgnoreGrid  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity InsertSalesMangeGrid(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertSalesMangeGrid";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.InsertSalesMangeGrid(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertSalesMangeGrid  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertHotelCompareGrid(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelCompareGrid";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.InsertHotelCompareGrid(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelCompareGrid  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertHotelCompareGridBatch(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelCompareGridBatch";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.InsertHotelCompareGridBatch(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelCompareGridBatch  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int UpdateHotelCompareGridBatch(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelCompareGridBatch";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.UpdateHotelCompareGridBatch(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelCompareGridBatch  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeleteSalesManagerGrid(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "DeleteSalesManagerGrid";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.DeleteSalesManagerGrid(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "DeleteSalesManagerGrid  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeleteHotelIgnoreGrid(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "DeleteHotelIgnoreGrid";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.DeleteHotelIgnoreGrid(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "DeleteHotelIgnoreGrid  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeleteHotelCompareGrid(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "DeleteHotelCompareGrid";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.DeleteHotelCompareGrid(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "DeleteHotelCompareGrid  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }
        
        public static APPContentEntity AutoSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);
            DataSet dsRestult = new DataSet();
            dsRestult.Tables.Add(new DataTable());
            dsRestult.Tables[0].Columns.Add("CITYID");
            dsRestult.Tables[0].Columns.Add("CITYNM");
            dsRestult.Tables[0].Columns.Add("HOTELID");
            dsRestult.Tables[0].Columns.Add("HOTELNM");
            dsRestult.Tables[0].Columns.Add("ERRMSG");
            dsRestult.Tables[0].Columns.Add("TYPEID");

            string strVer = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0].SerVer : "";
            try
            {
                DataSet dsCity = new DataSet();
                if (String.IsNullOrEmpty(appcontentEntity.APPContentDBEntity[0].CityID))
                {
                    if ("1".Equals(strVer))
                    {
                        dsCity = APPContentSA.CommonSelect(appcontentEntity).QueryResult;
                    }
                    else
                    {
                        dsCity = APPContentV2SA.CommonSelect(appcontentEntity).QueryResult;
                    }
                }
                else
                {
                    dsCity.Tables.Add(new DataTable());
                    dsCity.Tables[0].Columns.Add("cityid");
                    dsCity.Tables[0].Columns.Add("cityNM");
                    DataRow drRow = dsCity.Tables[0].NewRow();
                    drRow["cityid"] = appcontentEntity.APPContentDBEntity[0].CityID;
                    drRow["cityNM"] = appcontentEntity.APPContentDBEntity[0].CityNM;
                    dsCity.Tables[0].Rows.Add(drRow);
                }

                APPContentEntity _appcontentEntity = new APPContentEntity();
                _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                _appcontentEntity.LogMessages.Userid = appcontentEntity.LogMessages.Userid;
                _appcontentEntity.LogMessages.Username = appcontentEntity.LogMessages.Username;

                _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
                APPContentDBEntity _appcontentDBEntity = new APPContentDBEntity();
                _appcontentEntity.APPContentDBEntity.Add(_appcontentDBEntity);

                _appcontentEntity.APPContentDBEntity[0].PlatForm = "IOS";
                _appcontentEntity.APPContentDBEntity[0].TypeID = "1";

                DataSet dsHotel = new DataSet();
                DataSet dsHotelMain = new DataSet();
                ArrayList ayHotelImage = new ArrayList();
                DataSet dsHotelRoom = new DataSet();
                DataSet dsHotelFtType = new DataSet();
                ArrayList alIgnore = new ArrayList();

                APPContentEntity dsHotelDetail = new APPContentEntity();

                foreach (DataRow drCity in dsCity.Tables[0].Rows)
                {
                    _appcontentEntity.APPContentDBEntity[0].CityID = drCity["cityid"].ToString();

                    if ("1".Equals(strVer))
                    {
                        dsHotel = APPContentSA.HotelListSelect(_appcontentEntity).QueryResult;
                    }
                    else
                    {
                        dsHotel = APPContentV2SA.HotelListSelect(_appcontentEntity).QueryResult;
                    }

                    foreach (DataRow drHotel in dsHotel.Tables[0].Rows)
                    {
                        _appcontentEntity.APPContentDBEntity[0].HotelID = drHotel[0].ToString();

                        if ("1".Equals(strVer))
                        {
                            dsHotelDetail = APPContentSA.HotelDetailListSelect(_appcontentEntity);
                        }
                        else
                        {
                            dsHotelDetail = APPContentV2SA.HotelDetailListSelectV2(_appcontentEntity);
                        }

                        dsHotelMain = dsHotelDetail.APPContentDBEntity[0].HotelMain;
                        ayHotelImage = dsHotelDetail.APPContentDBEntity[0].HotelImage;
                        dsHotelRoom = dsHotelDetail.APPContentDBEntity[0].HotelRoom;
                        dsHotelFtType = dsHotelDetail.APPContentDBEntity[0].HotelFtType;

                        if (dsHotelMain.Tables.Count > 0 && dsHotelMain.Tables[0].Rows.Count > 0)
                        {
                            alIgnore = APPContentDA.GetHotelIgnore(drHotel[0].ToString());

                            if (!alIgnore.Contains("HOTELNM") && String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELNM"].ToString().Trim()))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少酒店名称";
                                drErNm["TYPEID"] = "HOTELNM";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!alIgnore.Contains("ADDRESS") && String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["ADDRESS"].ToString().Trim()))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少酒店地址";
                                drErNm["TYPEID"] = "ADDRESS";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if ((!alIgnore.Contains("LGLTTUDE")) && ((String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["LONGITUDE"].ToString().Trim()) || String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["LATITUDE"].ToString().Trim())) || (!ChkTudeValue(dsHotelMain.Tables[0].Rows[0]["LONGITUDE"].ToString().Trim()) || !ChkTudeValue(dsHotelMain.Tables[0].Rows[0]["LATITUDE"].ToString().Trim()))))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少酒店经纬度";
                                drErNm["TYPEID"] = "LGLTTUDE";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!alIgnore.Contains("HOTELDES") && String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELDES"].ToString().Trim()))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少酒店概况";
                                drErNm["TYPEID"] = "HOTELDES";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!alIgnore.Contains("HOTELAPPR") && (String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELAPPR"].ToString().Trim()) || (!ChkApprValue(dsHotelMain.Tables[0].Rows[0]["HOTELAPPR"].ToString().Trim()))))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "请检查酒店小贴士";
                                drErNm["TYPEID"] = "HOTELAPPR";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!alIgnore.Contains("HOTELSERVICE") && String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["HOTELSERVICE"].ToString().Trim()))
                           {
                               DataRow drErNm = dsRestult.Tables[0].NewRow();
                               drErNm["CITYID"] = drCity["cityid"].ToString();
                               drErNm["CITYNM"] = drCity["cityNM"].ToString();
                               drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                               drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                               drErNm["ERRMSG"] = "缺少酒店服务信息";
                               drErNm["TYPEID"] = "HOTELSERVICE";
                               dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!alIgnore.Contains("BUSSES") && String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["BUSSES"].ToString().Trim()))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少商务设施信息";
                                drErNm["TYPEID"] = "BUSSES";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!alIgnore.Contains("TRADEAREA") && (String.IsNullOrEmpty(drHotel["TRADEAREA"].ToString().Trim()) || "其它".Equals(drHotel["TRADEAREA"].ToString().Trim())))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少商圈信息";
                                drErNm["TYPEID"] = "TRADEAREA";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!alIgnore.Contains("SALES") && (!APPContentDA.GetHotelSales(drHotel["HOTELID"].ToString())))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少酒店销售人员信息";
                                drErNm["TYPEID"] = "SALES";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }
                            //if (String.IsNullOrEmpty(dsHotelMain.Tables[0].Rows[0]["CUSTOMTEL"].ToString().Trim()))
                            //{
                            //    lbCustomTel.Text = "";
                            //    messageContent = messageContent + GetLocalResourceObject("ErrorMsgHotelSerTel").ToString() + "<br/>";
                            //}
                        }
                        else
                        {
                            DataRow drErNm = dsRestult.Tables[0].NewRow();
                            drErNm["CITYID"] = drCity["cityid"].ToString();
                            drErNm["CITYNM"] = drCity["cityNM"].ToString();
                            drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                            drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                            drErNm["ERRMSG"] = "缺少酒店基础信息";
                            drErNm["TYPEID"] = "";
                            dsRestult.Tables[0].Rows.Add(drErNm);
                        }

                        DataSet dsLink = APPContentDA.HotelLinkSelect(_appcontentEntity);
                        if (dsLink.Tables.Count > 0 && dsLink.Tables[0].Rows.Count > 0)
                        {
                            if (!alIgnore.Contains("LINKTEL") && String.IsNullOrEmpty(dsLink.Tables[0].Rows[0]["LINKTEL"].ToString().Trim()))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少酒店预订电话";
                                drErNm["TYPEID"] = "LINKTEL";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!alIgnore.Contains("LINKFAX") && String.IsNullOrEmpty(dsLink.Tables[0].Rows[0]["LINKFAX"].ToString().Trim()))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = "缺少酒店预订传真";
                                drErNm["TYPEID"] = "LINKFAX";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }
                        }
                        else
                        {
                            DataRow drErNm = dsRestult.Tables[0].NewRow();
                            drErNm["CITYID"] = drCity["cityid"].ToString();
                            drErNm["CITYNM"] = drCity["cityNM"].ToString();
                            drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                            drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                            drErNm["ERRMSG"] = "缺少订单传递方式";
                            drErNm["TYPEID"] = "";
                            dsRestult.Tables[0].Rows.Add(drErNm);
                        }

                        if (!alIgnore.Contains("HTIMAGE") && (ayHotelImage.Count == 0 || !ChkHotelImagePath(ayHotelImage)))
                        {
                            DataRow drErNm = dsRestult.Tables[0].NewRow();
                            drErNm["CITYID"] = drCity["cityid"].ToString();
                            drErNm["CITYNM"] = drCity["cityNM"].ToString();
                            drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                            drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                            drErNm["ERRMSG"] = "请检查酒店图片信息";
                            drErNm["TYPEID"] = "HTIMAGE";
                            dsRestult.Tables[0].Rows.Add(drErNm);
                        }

                        if (dsHotelRoom.Tables.Count > 0 && dsHotelRoom.Tables[0].Rows.Count > 0)
                        {
                            string strROOMNM = "";
                            string strROOMCODE = "";
                            string strBEDNM = "";
                            string strNETPRICE = "";
                            string strVPPRICE = "";
                            for (int i = 0; i < dsHotelRoom.Tables[0].Rows.Count; i++)
                            {
                                if (!alIgnore.Contains("ROOMNM") && String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["ROOMNM"].ToString()) && String.IsNullOrEmpty(strROOMNM))
                                {
                                    strROOMNM = "缺少房型名称";
                                }

                                if (!alIgnore.Contains("ROOMCODE") && String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["ROOMCODE"].ToString()) && String.IsNullOrEmpty(strROOMCODE))
                                {
                                    strROOMCODE = "缺少房型代码";
                                }

                                if (!alIgnore.Contains("BEDNM") && String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["BEDNM"].ToString()) && String.IsNullOrEmpty(strBEDNM))
                                {
                                    strBEDNM = "缺少床型名称";
                                }

                                if (!alIgnore.Contains("NETPRICE") && String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["NETPRICE"].ToString()) && String.IsNullOrEmpty(strNETPRICE))
                                {
                                    strNETPRICE = "缺少网络价";
                                }

                                if (!alIgnore.Contains("VPPRICE") && String.IsNullOrEmpty(strVPPRICE) && (String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["VPPRICE"].ToString()) || (!String.IsNullOrEmpty(dsHotelRoom.Tables[0].Rows[i]["VPPRICE"].ToString()) && decimal.Parse(dsHotelRoom.Tables[0].Rows[i]["VPPRICE"].ToString()) < 10)))
                                {
                                    strVPPRICE = "价格低于10元";
                                }
                            }

                            if (!String.IsNullOrEmpty(strROOMNM))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = strROOMNM;
                                drErNm["TYPEID"] = "ROOMNM";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!String.IsNullOrEmpty(strROOMCODE))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = strROOMCODE;
                                drErNm["TYPEID"] = "ROOMCODE";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!String.IsNullOrEmpty(strBEDNM))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = strBEDNM;
                                drErNm["TYPEID"] = "BEDNM";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!String.IsNullOrEmpty(strNETPRICE))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = strNETPRICE;
                                drErNm["TYPEID"] = "NETPRICE";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }

                            if (!String.IsNullOrEmpty(strVPPRICE))
                            {
                                DataRow drErNm = dsRestult.Tables[0].NewRow();
                                drErNm["CITYID"] = drCity["cityid"].ToString();
                                drErNm["CITYNM"] = drCity["cityNM"].ToString();
                                drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                                drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                                drErNm["ERRMSG"] = strVPPRICE;
                                drErNm["TYPEID"] = "VPPRICE";
                                dsRestult.Tables[0].Rows.Add(drErNm);
                            }
                        }
                        else
                        {
                            DataRow drErNm = dsRestult.Tables[0].NewRow();
                            drErNm["CITYID"] = drCity["cityid"].ToString();
                            drErNm["CITYNM"] = drCity["cityNM"].ToString();
                            drErNm["HOTELID"] = drHotel["HOTELID"].ToString();
                            drErNm["HOTELNM"] = drHotel["HOTELNM"].ToString();
                            drErNm["ERRMSG"] = "缺少酒店房型信息";
                            drErNm["TYPEID"] = "";
                            dsRestult.Tables[0].Rows.Add(drErNm);
                        }
                    }
                }
                appcontentEntity.QueryResult = dsRestult;
                return appcontentEntity;
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                throw ex;
            }
        }

        private static bool ChkHotelImagePath(ArrayList alImagePath)
        {
            System.Net.HttpWebResponse result = null;
            System.Net.HttpWebRequest req = null;
            System.Net.ServicePointManager.DefaultConnectionLimit = 200;
            foreach (string strPath in alImagePath)
            {
                try
                {
                    System.GC.Collect();
                    req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strPath);
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.Timeout = 5000;
                    req.KeepAlive = false;
                    result = (System.Net.HttpWebResponse)(req.GetResponse());

                    if (result != null)
                    {
                        result.Close();
                    }
                    if (req != null)
                    {
                        req.Abort();
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool ChkTudeValue(string param)
        {
            try
            {
                if (decimal.Parse(param) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool ChkApprValue(string param)
        {
            if (param.IndexOf('，') >= 0)
            {
                return false;
            }

            if (param.EndsWith(","))
            {
                return false;
            }

            return true;
        }

        public static APPContentEntity CommonPlatSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "CommonPlatSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.CommonPlatSelect(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "CommonPlatSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity SalesPopGridSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "SalesPopGridSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.SalesPopGridSelect(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "SalesPopGridSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity SalesMangeListSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "SalesMangeListSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.SalesMangeListSelect(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "SalesMangeListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity OrderSettleMangeListSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "OrderSettleMangeListSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.OrderSettleMangeListSelect(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "OrderSettleMangeListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity OrderSettleMangeListCount(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "OrderSettleMangeListCount";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.OrderSettleMangeListCount(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "OrderSettleMangeListCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity SaveOrderSettleMangeList(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderSettleMangeList";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.SaveOrderSettleMangeList(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "SaveOrderSettleMangeList  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity ExportOrderSettleMangeList(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "ExportOrderSettleMangeList";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.ExportOrderSettleMangeList(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "ExportOrderSettleMangeList  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity SalesMangeListSelectCount(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "SalesMangeListSelectCount";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.SalesMangeListSelectCount(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "SalesMangeListSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity SalesMangeDetialSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "SalesMangeDetialSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.SalesMangeDetialSelect(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "SalesMangeDetialSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity Select(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "Select";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                if (appcontentEntity.APPContentDBEntity.Count > 0 && "1".Equals(appcontentEntity.APPContentDBEntity[0].SerVer))
                {
                    return APPContentSA.Select(appcontentEntity);
                }
                else
                {
                    return APPContentV2SA.Select(appcontentEntity);
                }
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity HotelDetailListSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "HotelDetailListSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                if (appcontentEntity.APPContentDBEntity.Count > 0 && "1".Equals(appcontentEntity.APPContentDBEntity[0].SerVer))
                {
                    return APPContentSA.HotelDetailListSelect(appcontentEntity);
                }
                else
                {
                    return APPContentV2SA.HotelDetailListSelectV2(appcontentEntity);
                }
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "HotelDetailListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity HotelFtDetailListSelect(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "HotelFtDetailListSelect";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                if (appcontentEntity.APPContentDBEntity.Count > 0 && "1".Equals(appcontentEntity.APPContentDBEntity[0].SerVer))
                {
                    return APPContentSA.HotelFtDetailListSelect(appcontentEntity);
                }
                else
                {
                    return APPContentV2SA.HotelFtDetailListSelectV2(appcontentEntity);
                }
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "HotelFtDetailListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity ApplyFullRoom(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "ApplyFullRoom";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentV2SA.ApplyFullRoom(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "ApplyFullRoom  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity ApplyUnFullRoom(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "ApplyUnFullRoom";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentV2SA.ApplyUnFullRoom(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "ApplyUnFullRoom  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity CreateSalesPlan(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "CreateSalesPlan";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                APPContentDBEntity dbParm = (appcontentEntity.APPContentDBEntity.Count > 0) ? appcontentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
                using (TransactionScope scope = new TransactionScope())
                {
                    int planID = APPContentDA.CreateSalesPlanEvent(appcontentEntity);
                    appcontentEntity.APPContentDBEntity[0].PlanID = planID.ToString();
                    APPContentDA.CreateSalesPlanEventDetail(appcontentEntity, planID);
                    APPContentDA.CreateSalesPlanEventJobList(appcontentEntity, planID);
                    appcontentEntity.Result = 1;

                    if ("0".Equals(dbParm.SaveType))
                    {
                        appcontentEntity = APPContentV2SA.CreateSalesPlan(appcontentEntity);
                        APPContentDA.UpdateSalesPlanEventJobStatus(appcontentEntity);
                    }

                    scope.Complete();
                }
                return appcontentEntity;
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "CreateSalesPlan  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity QueryFullRoom(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "QueryFullRoom";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentV2SA.QueryFullRoom(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "QueryFullRoom  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity GetCoreHotelGroupDetail(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "GetCoreHotelGroupDetail";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.GetCoreHotelGroupDetail(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "GetCoreHotelGroupDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity BindHotelListGrid(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "BindHotelListGrid";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.BindHotelListGrid(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "BindHotelListGrid  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertHotelGroupList(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelGroupList";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.InsertHotelGroupList(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "InsertHotelGroupList  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeteleHotelGroupList(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "DeteleHotelGroupList";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.DeteleHotelGroupList(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "DeteleHotelGroupList  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity SelectPropByPic(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "SelectPropByPic";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return APPContentDA.SelectPropByPic(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "SelectPropByPic  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }
    }
}