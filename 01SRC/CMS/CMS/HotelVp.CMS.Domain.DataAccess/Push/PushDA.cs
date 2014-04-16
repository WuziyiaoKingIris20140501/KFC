using System;
using System.Collections;
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
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class PushDA
    {
        public static PushEntity GetUserGroupList(PushEntity PushEntity)
        {
            PushEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Push", "t_lm_usergroup_list", false);
            return PushEntity;
        }

        public static bool CheckInsert(PushEntity PushEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TITLE",OracleType.VarChar)
                                };
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            parm[0].Value = dbParm.Title;
            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("Push", "t_lm_promotion_check_insert", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckUpdate(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CheckUpdatePushPlanManager");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
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

        public static bool CheckPushUpdate(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CheckUpdatePushInfoPlanManager");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
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

        public static PushEntity SelectPushAllUsersCount(PushEntity PushEntity)
        {
            PushEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Push", "t_lm_push_all_user_count", false);
            return PushEntity;
        }

        public static PushEntity SelectPushInfoAllUsersCount(PushEntity PushEntity)
        {
            PushEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Push", "t_lm_push_info_all_user_count", false);
            return PushEntity;
        }

        public static PushEntity PushHistorySelect(PushEntity PushEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("PushHistorySelect");
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

        public static PushEntity PushInfoHistorySelect(PushEntity PushEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("PushInfoHistorySelect");
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

        public static PushEntity PushHistoryListSelect(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("PushHistoryListSelect");
            if (String.IsNullOrEmpty(dbParm.Title))
            {
                cmd.SetParameterValue("@Title", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@Title", "%" + dbParm.Title + "%");
            }

            if (String.IsNullOrEmpty(dbParm.Content))
            {
                cmd.SetParameterValue("@Content", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@Content", "%" + dbParm.Content + "%");
            }

            if (String.IsNullOrEmpty(dbParm.StartDTime))
            {
                cmd.SetParameterValue("@StartDT", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@StartDT", dbParm.StartDTime);
            }

            if (String.IsNullOrEmpty(dbParm.EndDTime))
            {
                cmd.SetParameterValue("@EndDT", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EndDT", dbParm.EndDTime);
            }
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

        public static PushEntity PushInfoHistoryListSelect(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("PushInfoHistoryListSelect");
            if (String.IsNullOrEmpty(dbParm.Title))
            {
                cmd.SetParameterValue("@Title", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@Title", "%" + dbParm.Title + "%");
            }

            if (String.IsNullOrEmpty(dbParm.Content))
            {
                cmd.SetParameterValue("@Content", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@Content", "%" + dbParm.Content + "%");
            }

            if (String.IsNullOrEmpty(dbParm.StartDTime))
            {
                cmd.SetParameterValue("@StartDT", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@StartDT", dbParm.StartDTime);
            }

            if (String.IsNullOrEmpty(dbParm.EndDTime))
            {
                cmd.SetParameterValue("@EndDT", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EndDT", dbParm.EndDTime);
            }
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

         public static PushEntity SelectPushSuccCount(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectPushSuccCount");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

         public static PushEntity SelectPushInfoSuccCount(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectPushInfoSuccCount");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

         public static PushEntity SelectActionDateTime(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectActionDateTime");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

         public static PushEntity SelectInfoActionDateTime(PushEntity PushEntity)
         {
             PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
             DataCommand cmd = DataCommandManager.GetDataCommand("SelectInfoActionDateTime");
             cmd.SetParameterValue("@TaskID", dbParm.ID);
             PushEntity.QueryResult = cmd.ExecuteDataSet();
             return PushEntity;
         }

        public static PushEntity SelectPushUserGroupCount(PushEntity PushEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("UserGroups",OracleType.VarChar)
                                };
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();

            string strUserGroupList = string.Empty;
            string[] userGroupList = dbParm.UserGroupList.Split(',');

            foreach (string strTemp in userGroupList)
            {
                if(String.IsNullOrEmpty(strTemp.Trim()))
                {
                    continue;
                }
                strUserGroupList = strUserGroupList + strTemp.Trim().Substring(1, strTemp.IndexOf(']') - 1) + ",";
            }
            parm[0].Value = strUserGroupList;
            PushEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Push", "t_lm_push_user_group_count", false, parm);
            return PushEntity;
        }

        public static PushEntity SelectPushInfoUserGroupCount(PushEntity PushEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("UserGroups",OracleType.VarChar)
                                };
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();

            string strUserGroupList = string.Empty;
            string[] userGroupList = dbParm.UserGroupList.Split(',');

            foreach (string strTemp in userGroupList)
            {
                if (String.IsNullOrEmpty(strTemp.Trim()))
                {
                    continue;
                }
                strUserGroupList = strUserGroupList + strTemp.Trim().Substring(1, strTemp.IndexOf(']') - 1) + ",";
            }
            parm[0].Value = strUserGroupList;
            PushEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Push", "t_lm_push_info_user_group_count", false, parm);
            return PushEntity;
        }

        public static PushEntity SelectPushActionHistoryList(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectPushActionHistoryList");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            if (String.IsNullOrEmpty(dbParm.TelPhone))
            {
                cmd.SetParameterValue("@TelPhone", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@TelPhone", dbParm.TelPhone);
            }
            cmd.SetParameterValue("@PageCurrent", PushEntity.PageCurrent - 1);
            cmd.SetParameterValue("@PageSize", PushEntity.PageSize);

            DataSet dsResult = cmd.ExecuteDataSet();
            PushEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            PushEntity.QueryResult = dsResult;
            return PushEntity;
        }

        public static PushEntity SelectPushInfoActionHistoryList(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectPushInfoActionHistoryList");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            if (String.IsNullOrEmpty(dbParm.TelPhone))
            {
                cmd.SetParameterValue("@UserID", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@UserID", dbParm.TelPhone);
            }
            cmd.SetParameterValue("@PageCurrent", PushEntity.PageCurrent - 1);
            cmd.SetParameterValue("@PageSize", PushEntity.PageSize);

            DataSet dsResult = cmd.ExecuteDataSet();
            PushEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");
            PushEntity.QueryResult = dsResult;
            return PushEntity;
        }

        public static PushEntity ExportPushActionHistoryList(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ExportPushActionHistoryList");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            if (String.IsNullOrEmpty(dbParm.TelPhone))
            {
                cmd.SetParameterValue("@TelPhone", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@TelPhone", dbParm.TelPhone);
            }
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

        public static PushEntity ExportPushInfoActionHistoryList(PushEntity PushEntity)
        {
            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ExportPushInfoActionHistoryList");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            if (String.IsNullOrEmpty(dbParm.TelPhone))
            {
                cmd.SetParameterValue("@UserID", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@UserID", dbParm.TelPhone);
            }
            PushEntity.QueryResult = cmd.ExecuteDataSet();
            return PushEntity;
        }

        public static PushEntity Insert(PushEntity PushEntity)
        {
            if (PushEntity.PushDBEntity.Count == 0)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            //if (!CheckInsert(PushEntity))
            //{
            //    PushEntity.Result = 2;
            //    return PushEntity;
            //}

            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertPushPlanManager");
            cmd.SetParameterValue("@PushTYpe", dbParm.Type);
            cmd.SetParameterValue("@PushTitle", dbParm.Title);
            cmd.SetParameterValue("@PushContent", dbParm.Content);
            cmd.SetParameterValue("@PushProtoType", dbParm.PushProtoType);

            if ("0".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", "");
            }
            else if ("1".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", dbParm.UserGroupList);
            }
            else if ("2".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", dbParm.FilePath);
            }

            cmd.SetParameterValue("@Status", "0");
            cmd.SetParameterValue("@All_Count", dbParm.AllCount);
            cmd.SetParameterValue("@CreateUser", PushEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            PushEntity.PushDBEntity[0].ID = cmd.GetParameterValue("@TaskID").ToString();
            PushEntity.Result = 1;
            return PushEntity;
        }

        public static PushEntity PushInsert(PushEntity PushEntity)
        {
            if (PushEntity.PushDBEntity.Count == 0)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            //if (!CheckInsert(PushEntity))
            //{
            //    PushEntity.Result = 2;
            //    return PushEntity;
            //}

            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertPushInfoPlanManager");
            cmd.SetParameterValue("@PushTYpe", dbParm.Type);
            cmd.SetParameterValue("@PushAcTYpe", dbParm.PushType);
            cmd.SetParameterValue("@PushTitle", dbParm.Title);
            cmd.SetParameterValue("@PushContent", dbParm.Content);
            cmd.SetParameterValue("@PushProtoType", dbParm.PushProtoType);
            cmd.SetParameterValue("@WapUrl", dbParm.WapUrl);
            if ("0".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", "");
            }
            else if ("1".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", dbParm.UserGroupList);
            }
            else if ("2".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", dbParm.FilePath);
            }

            cmd.SetParameterValue("@Status", "0");
            cmd.SetParameterValue("@All_Count", dbParm.AllCount);
            cmd.SetParameterValue("@CreateUser", PushEntity.LogMessages.Username);

            cmd.SetParameterValue("@TicketPackage", dbParm.TicketPackage);
            cmd.SetParameterValue("@TicketAmount", dbParm.TicketAmount);

            cmd.ExecuteNonQuery();
            PushEntity.PushDBEntity[0].ID = cmd.GetParameterValue("@TaskID").ToString();
            PushEntity.Result = 1;
            return PushEntity;
        }

        public static PushEntity UpdateSendPushStatus(PushEntity PushEntity)
        {
            if (PushEntity.PushDBEntity.Count == 0)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            if (PushEntity.LogMessages == null)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            if (!CheckUpdate(PushEntity))
            {
                PushEntity.Result = 2;
                return PushEntity;
            }

            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSendPushStatus");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            cmd.SetParameterValue("@Status", dbParm.Status);
            cmd.SetParameterValue("@UpdateUser", PushEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            PushEntity.Result = 1;
            return PushEntity;
        }

        public static PushEntity UpdateSendPushInfoStatus(PushEntity PushEntity)
        {
            if (PushEntity.PushDBEntity.Count == 0)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            if (PushEntity.LogMessages == null)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            if (!CheckPushUpdate(PushEntity))
            {
                PushEntity.Result = 2;
                return PushEntity;
            }

            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSendPushInfoStatus");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            cmd.SetParameterValue("@Status", dbParm.Status);
            cmd.SetParameterValue("@UpdateUser", PushEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();
            PushEntity.Result = 1;
            return PushEntity;
        }

        public static PushEntity Update(PushEntity PushEntity)
        {
            if (PushEntity.PushDBEntity.Count == 0)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            if (PushEntity.LogMessages == null)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            if (!CheckUpdate(PushEntity))
            {
                PushEntity.Result = 2;
                return PushEntity;
            }

            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdatePushPlanManager");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            cmd.SetParameterValue("@PushTYpe", dbParm.Type);
            cmd.SetParameterValue("@PushTitle", dbParm.Title);
            cmd.SetParameterValue("@PushContent", dbParm.Content);
            cmd.SetParameterValue("@PushProtoType", dbParm.PushProtoType);

            if ("0".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", "");
            }
            else if ("1".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", dbParm.UserGroupList);
            }
            else if ("2".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", dbParm.FilePath);
            }
            cmd.SetParameterValue("@All_Count", dbParm.AllCount);
            cmd.SetParameterValue("@UpdateUser", PushEntity.LogMessages.Username);
            cmd.ExecuteNonQuery();

            PushEntity.Result = 1;
            return PushEntity;
        }

        public static PushEntity PushUpdate(PushEntity PushEntity)
        {
            if (PushEntity.PushDBEntity.Count == 0)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            if (PushEntity.LogMessages == null)
            {
                PushEntity.Result = 0;
                return PushEntity;
            }

            if (!CheckPushUpdate(PushEntity))
            {
                PushEntity.Result = 2;
                return PushEntity;
            }

            PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdatePushInfoPlanManager");
            cmd.SetParameterValue("@TaskID", dbParm.ID);
            cmd.SetParameterValue("@PushTYpe", dbParm.Type);
            cmd.SetParameterValue("@PushACTYpe", dbParm.PushType);
            cmd.SetParameterValue("@PushTitle", dbParm.Title);
            cmd.SetParameterValue("@PushContent", dbParm.Content);
            cmd.SetParameterValue("@PushProtoType", dbParm.PushProtoType);
            cmd.SetParameterValue("@WapUrl", dbParm.WapUrl);
            if ("0".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", "");
            }
            else if ("1".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", dbParm.UserGroupList);
            }
            else if ("2".Equals(dbParm.Type))
            {
                cmd.SetParameterValue("@PushUsers", dbParm.FilePath);
            }
            cmd.SetParameterValue("@All_Count", dbParm.AllCount);
            cmd.SetParameterValue("@UpdateUser", PushEntity.LogMessages.Username);

            cmd.SetParameterValue("@TicketPackage", dbParm.TicketPackage);
            cmd.SetParameterValue("@TicketAmount", dbParm.TicketAmount);

            cmd.ExecuteNonQuery();

            PushEntity.Result = 1;
            return PushEntity;
        }

        public static DataSet GetUserInfo(string UserID)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            parm[0].Value = UserID;
            return HotelVp.Common.DBUtility.DbManager.Query("Push", "getuserinfo_by_user", false, parm);
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
    }
}