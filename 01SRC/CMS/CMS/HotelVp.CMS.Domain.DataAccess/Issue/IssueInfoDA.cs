using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
using System.Collections;

//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class IssueInfoDA
    {
        public static IssueInfoEntity GetCommonUserList(IssueInfoEntity issueInfoEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCommonUserList");
            issueInfoEntity.QueryResult = cmd.ExecuteDataSet();
            return issueInfoEntity;
        }

        public static bool CheckIssueLinkGo(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            string strRelatedType = dbParm.RelatedType;
            string strSql = string.Empty;
            switch (strRelatedType)
            {
                case "0":
                    strSql = "t_lm_b_common_order";
                    break;
                case "1":
                    return true;
                    //strSql = "t_lm_b_common_hotel";
                    //break;
                case "2":
                    strSql = "t_lm_b_common_invoice";
                    break;
                case "3":
                    strSql = "t_lm_b_common_user";
                    break;
                case "4":
                    strSql = "t_lm_b_common_cash";
                    break;
                case "5":
                    strSql = "t_lm_b_common_advice";
                    break;
                default:
                    strSql = "";
                    break;
            }

            OracleParameter[] parm ={
                                    new OracleParameter("TID",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.RelatedID;
            DataSet dsResult = DbManager.Query("IssueInfo", strSql, true, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString().Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckIssueAsTo(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CheckIssueAsTo");
            cmd.SetParameterValue("@UserID", dbParm.Assignto);
            DataSet dsResult = cmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static IssueInfoEntity GetMailToList(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetMailToList");
            cmd.SetParameterValue("@UserID", dbParm.Assignto);
            issueInfoEntity.QueryResult = cmd.ExecuteDataSet();
            return issueInfoEntity;
        }

        public static DataSet GetOrderUserTel(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();

            OracleParameter[] parm ={
                                    new OracleParameter("TID",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.RelatedID;
            DataSet dsResult = DbManager.Query("IssueInfo", "t_lm_b_order_user_tel", true, parm);
            return dsResult;
        }

        public static IssueInfoEntity GetUpdateIssueDetail(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetUpdateIssueDetail");
            cmd.SetParameterValue("@IssueID", dbParm.IssueID);
            issueInfoEntity.QueryResult = cmd.ExecuteDataSet();
            return issueInfoEntity;
        }

        public static IssueInfoEntity GetIssueHistoryList(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetIssueHistoryList");
            cmd.SetParameterValue("@IssueID", dbParm.IssueID);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    dsResult.Tables[0].Rows[i]["IssueTypeNM"] = SetIssueTypeNM(dsResult.Tables[0].Rows[i]["IssueType"].ToString().Trim());
                    dsResult.Tables[0].Rows[i]["TicketPage"] = String.IsNullOrEmpty(dsResult.Tables[0].Rows[i]["TicketCode"].ToString().Trim()) ? "" : dsResult.Tables[0].Rows[i]["TicketCode"].ToString().Trim() + "-" + dsResult.Tables[0].Rows[i]["TicketAmount"].ToString().Trim();

                    if (i < dsResult.Tables[0].Rows.Count - 1)
                    {
                        dsResult.Tables[0].Rows[i]["ActionTime"] = SetTimeLag(dsResult.Tables[0].Rows[i + 1]["CreateTime"].ToString().Trim(), dsResult.Tables[0].Rows[i]["CreateTime"].ToString().Trim());
                    }
                }
            }

            issueInfoEntity.QueryResult = dsResult;
            return issueInfoEntity;
        }

        private static string SetTimeLag(string strFrom, string strTo)
        {
            string strResult = "";

            if (!CheckDateTimeValue(strFrom, strTo))
            {
                return strResult;
            }

            DateTime dtFrom = DateTime.Parse(strFrom);
            DateTime dtTo = DateTime.Parse(strTo);

            System.TimeSpan ND = dtTo - dtFrom;

            strResult = strResult + ND.Days.ToString() + "天";
            strResult = strResult + ND.Hours.ToString() + "时";
            strResult = strResult + ND.Minutes.ToString() + "分";
            strResult = strResult + ND.Seconds.ToString() + "秒";
            return strResult;
        }

        private static bool CheckDateTimeValue(string strFrom, string strTo)
        {
            try
            {
                DateTime.Parse(strFrom);
                DateTime.Parse(strTo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IssueInfoEntity BindIssueList(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetBindIssueList");

            cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);

            cmd.SetParameterValue("@Title", String.IsNullOrEmpty(dbParm.Title) ? dbParm.Title : "%" + dbParm.Title + "%");
            cmd.SetParameterValue("@IssueStatus", dbParm.Status);
            cmd.SetParameterValue("@IssueType", String.IsNullOrEmpty(dbParm.IssueType) ? dbParm.IssueType : "%" + dbParm.IssueType + "%");
            cmd.SetParameterValue("@Assignto", dbParm.Assignto);
            cmd.SetParameterValue("@RelatedType", dbParm.RelatedType);
            cmd.SetParameterValue("@RelatedID", dbParm.RelatedID);
            cmd.SetParameterValue("@TimeSpans", dbParm.TimeSpans);

            cmd.SetParameterValue("@PageCurrent", issueInfoEntity.PageCurrent - 1);
            cmd.SetParameterValue("@PageSize", issueInfoEntity.PageSize);

            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++ )
                {
                    dsResult.Tables[0].Rows[i]["IssueTypeNM"] = SetIssueTypeNM(dsResult.Tables[0].Rows[i]["IssueType"].ToString().Trim());
                }
            }

            issueInfoEntity.QueryResult = dsResult;
            issueInfoEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            return issueInfoEntity;
        }

        private static string SetIssueTypeNM(string param)
        {
            string strResult = string.Empty;
            string[] strTypeList = param.Split(',');

            foreach (string strTemp in strTypeList)
            {
                switch (strTemp)
                {
                    case "0":
                        strResult = strResult + "酒店问题" + ",";
                        break;
                    case "1":
                        strResult = strResult + "订单状态问题" + ",";
                        break;
                    case "2":
                        strResult = strResult + " 订单支付问题" + ",";
                        break;
                    case "3":
                        strResult = strResult + "订单返现问题" + ",";
                        break;
                    case "4":
                        strResult = strResult + "优惠券问题" + ",";
                        break;
                    case "5":
                        strResult = strResult + "发票问题" + ",";
                        break;
                    case "6":
                        strResult = strResult + " 用户问题" + ",";
                        break;
                    case "7":
                        strResult = strResult + "提现问题" + ",";
                        break;
                    case "8":
                        strResult = strResult + "订单审核问题" + ",";
                        break;
                    default:
                        strResult = "未知状态";
                        break;
                }
            }

            return strResult.Trim(',');
        }

        public static IssueInfoEntity GetIssueSettingHistory(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetIssueSettingHistory");
            //cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            issueInfoEntity.QueryResult = cmd.ExecuteDataSet();
            return issueInfoEntity;
        }

        public static IssueInfoEntity InsertIssueAndHistory(IssueInfoEntity issueInfoEntity)
        {
            if (!CheckIssueLinkGo(issueInfoEntity))
            {
                issueInfoEntity.Result = 2;
                return issueInfoEntity;
            }

            if (!CheckIssueAsTo(issueInfoEntity))
            {
                issueInfoEntity.Result = 3;
                return issueInfoEntity;
            }

            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertIssueManager");
            cmd.SetParameterValue("@Title", dbParm.Title);
            cmd.SetParameterValue("@Priority", dbParm.Priority);
            cmd.SetParameterValue("@IssueType", dbParm.IssueType);
            cmd.SetParameterValue("@Assignto", dbParm.Assignto);
            cmd.SetParameterValue("@IssueStatus", dbParm.Status);
            cmd.SetParameterValue("@IsIndemnify", dbParm.IsIndemnify);
            cmd.SetParameterValue("@IndemnifyPrice", dbParm.IndemnifyPrice);
            cmd.SetParameterValue("@TicketCode", (String.IsNullOrEmpty(dbParm.TicketCode.Trim())) ? "" : dbParm.TicketCode.Substring(0, dbParm.TicketCode.IndexOf('-')).Trim());
            cmd.SetParameterValue("@TicketAmount", (String.IsNullOrEmpty(dbParm.TicketCode.Trim())) ? "" : dbParm.TicketCode.Substring(dbParm.TicketCode.IndexOf('-') + 1).Trim());
            cmd.SetParameterValue("@RelatedType", dbParm.RelatedType);
            cmd.SetParameterValue("@RelatedID", dbParm.RelatedID);
            cmd.SetParameterValue("@Remark", dbParm.Remark);
            cmd.SetParameterValue("@CreateUser", issueInfoEntity.LogMessages.Userid);
            cmd.SetParameterValue("@CreateTime", dbParm.UpdateTime);
            cmd.ExecuteNonQuery();
            issueInfoEntity.IssueInfoDBEntity[0].IssueID = cmd.GetParameterValue("@IssueID").ToString();

            //InsertIssueHistory(issueInfoEntity);
            issueInfoEntity.Result = 1;
            return issueInfoEntity;
        }

        public static int InsertIssueHistory(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertIssueHistory");
            cmd.SetParameterValue("@IssueID", dbParm.IssueID);
            cmd.SetParameterValue("@Title", dbParm.Title);
            cmd.SetParameterValue("@Priority", dbParm.Priority);
            cmd.SetParameterValue("@IssueType", dbParm.IssueType);
            cmd.SetParameterValue("@Assignto", dbParm.Assignto);
            cmd.SetParameterValue("@IssueStatus", dbParm.Status);
            cmd.SetParameterValue("@IsIndemnify", dbParm.IsIndemnify);
            cmd.SetParameterValue("@IndemnifyPrice", dbParm.IndemnifyPrice);
            cmd.SetParameterValue("@TicketCode", (String.IsNullOrEmpty(dbParm.TicketCode.Trim())) ? "" : dbParm.TicketCode.Substring(0, dbParm.TicketCode.IndexOf('-')).Trim());
            cmd.SetParameterValue("@TicketAmount", (String.IsNullOrEmpty(dbParm.TicketCode.Trim())) ? "" : dbParm.TicketCode.Substring(dbParm.TicketCode.IndexOf('-') + 1).Trim());
            cmd.SetParameterValue("@RelatedType", dbParm.RelatedType);
            cmd.SetParameterValue("@RelatedID", dbParm.RelatedID);
            cmd.SetParameterValue("@Remark", dbParm.HisRemark);
            cmd.SetParameterValue("@CreateUser", issueInfoEntity.LogMessages.Userid);
            cmd.SetParameterValue("@CreateTime", dbParm.UpdateTime);

            cmd.SetParameterValue("@ChkMsgAssgin", dbParm.ChkMsgAssgin);
            cmd.SetParameterValue("@MsgAssginText", dbParm.MsgAssginText);
            cmd.SetParameterValue("@MsgAssginRs", dbParm.MsgAssginRs);
            cmd.SetParameterValue("@ChkMsgUser", dbParm.ChkMsgUser);
            cmd.SetParameterValue("@MsgUserText", dbParm.MsgUserText);
            cmd.SetParameterValue("@MsgUserRs", dbParm.MsgUserRs);
            return cmd.ExecuteNonQuery();
        }

        public static IssueInfoEntity UpdateIssueAndHistory(IssueInfoEntity issueInfoEntity)
        {
            if (!CheckIssueLinkGo(issueInfoEntity))
            {
                issueInfoEntity.Result = 2;
                return issueInfoEntity;
            }

            if (!CheckIssueAsTo(issueInfoEntity))
            {
                issueInfoEntity.Result = 3;
                return issueInfoEntity;
            }

            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateIssueManager");
            cmd.SetParameterValue("@IssueID", dbParm.IssueID);
            cmd.SetParameterValue("@Title", dbParm.Title);
            cmd.SetParameterValue("@Priority", dbParm.Priority);
            cmd.SetParameterValue("@IssueType", dbParm.IssueType);
            cmd.SetParameterValue("@Assignto", dbParm.Assignto);
            cmd.SetParameterValue("@IssueStatus", dbParm.Status);
            cmd.SetParameterValue("@IsIndemnify", dbParm.IsIndemnify);
            cmd.SetParameterValue("@IndemnifyPrice", dbParm.IndemnifyPrice);
            cmd.SetParameterValue("@TicketCode", (String.IsNullOrEmpty(dbParm.TicketCode.Trim())) ? "" : dbParm.TicketCode.Substring(0, dbParm.TicketCode.IndexOf('-')).Trim());
            cmd.SetParameterValue("@TicketAmount", (String.IsNullOrEmpty(dbParm.TicketCode.Trim())) ? "" : dbParm.TicketCode.Substring(dbParm.TicketCode.IndexOf('-') + 1).Trim());
            cmd.SetParameterValue("@RelatedType", dbParm.RelatedType);
            cmd.SetParameterValue("@RelatedID", dbParm.RelatedID);
            cmd.SetParameterValue("@Remark", dbParm.Remark);
            cmd.SetParameterValue("@UpdateUser", issueInfoEntity.LogMessages.Userid);
            cmd.SetParameterValue("@UpdateTime", dbParm.UpdateTime);
            cmd.SetParameterValue("@TimeDiffTal", dbParm.TimeDiffTal);
            cmd.SetParameterValue("@TimeSpans", dbParm.TimeSpans);
            cmd.ExecuteNonQuery();
            //InsertIssueHistory(issueInfoEntity);
            issueInfoEntity.Result = 1;
            return issueInfoEntity;
        }

        //通过sequence查询得到下一个ID值,数据库为Oracle
        public static int getMaxIDfromSeq(string sequencename)
        {
            int seqID = 1;
            string sql = "select " + sequencename + ".nextval from dual";
            object obj = DbHelperOra.GetSingle(sql, false);
            if (obj != null)
            {
                seqID = Convert.ToInt32(obj);
            }
            return seqID;
        }


        public static IssueInfoEntity ReviewOverTimeOrderList(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ReviewOverTimeOrderList");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@PriceCode", dbParm.PriceCode);
            cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);
            cmd.SetParameterValue("@PageCurrent", issueInfoEntity.PageCurrent - 1);
            cmd.SetParameterValue("@PageSize", issueInfoEntity.PageSize);

            DataSet dsResult = cmd.ExecuteDataSet();

            for (int i = 0; i < dsResult.Tables[0].Rows.Count;i++ )
            {
                dsResult.Tables[0].Rows[i]["cancel_reason"] = GetCancleReason(dsResult.Tables[0].Rows[i]["cancel_reason"].ToString());
            }

            issueInfoEntity.QueryResult = dsResult;
            issueInfoEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            return issueInfoEntity;
        }

        public static IssueInfoEntity ExportOverTimeOrderList(IssueInfoEntity issueInfoEntity)
        {
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ExportOverTimeOrderList");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@PriceCode", dbParm.PriceCode);
            cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);

            DataSet dsResult = cmd.ExecuteDataSet();
            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                dsResult.Tables[0].Rows[i]["cancel_reason"] = GetCancleReason(dsResult.Tables[0].Rows[i]["cancel_reason"].ToString());
            }

            issueInfoEntity.QueryResult = dsResult;
            return issueInfoEntity;
        }

        private static string GetCancleReason(string reasonCode)
        {
            string reasonName = string.Empty;
            Hashtable htReason = new Hashtable();
            htReason.Add("CRG18", "LM订单客人手机端取消");
            htReason.Add("CRG17", "预授权失败自动取消");
            htReason.Add("CRC01", "满房");
            htReason.Add("CRC03", "员工差错");
            htReason.Add("CRC04", "蓄水单取消");
            htReason.Add("CRC06", "变价");
            htReason.Add("CRG14", "无法完成担保");
            htReason.Add("CRG06", "无法完成支付");
            htReason.Add("CRG11", "超时未支付");
            htReason.Add("CRG05", "测试订单");
            htReason.Add("CRG01", "行程改变");
            htReason.Add("CRG02", "无法满足特殊需求");
            htReason.Add("CRG03", "其他途径预订");
            htReason.Add("CRG04", "预订内容变更");
            htReason.Add("CRG07", "确认速度不满意");
            htReason.Add("CRG08", "GDS渠道取消");
            htReason.Add("CRG09", "IDS渠道取消");
            htReason.Add("CRG10", "接口渠道取消");
            htReason.Add("CRG13", "设施/位置不满意");
            htReason.Add("CRC02", "重复预订");
            htReason.Add("CRG15", "预订未用抵用券");
            htReason.Add("CRG16", "预订未登录");
            htReason.Add("CRC07", "Jrez渠道取消");
            htReason.Add("CRC05", "骚扰订单");
            htReason.Add("CRG99", "其他");
            htReason.Add("CRH01", "酒店反悔");
            htReason.Add("CRH03", "酒店停业/装修");
            htReason.Add("PGSRQ", "系统自动取消");
            htReason.Add("CRH02", "酒店不确认");
            htReason.Add("CRH05", "无法追加担保");
            htReason.Add("CRH06", "无法追加预付");
            htReason.Add("CRH07", "无协议/协议到期");
            htReason.Add("CRH08", "不可抗力");

            reasonName = htReason.ContainsKey(reasonCode) ? htReason[reasonCode].ToString() : reasonCode;
            return reasonName;
        }

        public static IssueInfoEntity GetRevlTypeVal(IssueInfoEntity issueInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("REKEY",OracleType.VarChar)
                                };
            IssueInfoDBEntity dbParm = (issueInfoEntity.IssueInfoDBEntity.Count > 0) ? issueInfoEntity.IssueInfoDBEntity[0] : new IssueInfoDBEntity();
            parm[0].Value = dbParm.RevlKey;

            string strSql = "t_lm_b_hotelinfo_prop_reval_order";

            if ("1".Equals(dbParm.RevlType))
            {
                strSql = "t_lm_b_hotelinfo_prop_reval_hotel";
            }

            issueInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("IssueInfo", strSql, false, parm);
            return issueInfoEntity;
        }
    }
}