using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class UserGroupDA
    {
        public static UserGroupEntity CommonSelect(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("City", "t_fc_city", false);
            return userGroupEntity;
        }

        public static UserGroupEntity GetRegChannelList(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_regchnnel_list", false);
            return userGroupEntity;
        }

        public static UserGroupEntity GetPlatFormList(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_platForm_list", false);
            return userGroupEntity;
        }

        public static UserGroupEntity CreateUserSelect(UserGroupEntity userGroupEntity)
        {
            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_select", false);
            return userGroupEntity;
        }

        public static UserGroupEntity ReviewSelect(UserGroupEntity userGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERGROUPNAME",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar),
                                    new OracleParameter("StartCount",OracleType.VarChar),
                                    new OracleParameter("EndCount",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserGroupNM))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserGroupNM;
            }

            if (String.IsNullOrEmpty(dbParm.StartDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.StartDTime;
            }

            if (String.IsNullOrEmpty(dbParm.EndDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.EndDTime;
            }

            if (String.IsNullOrEmpty(dbParm.StartCount))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.StartCount;
            }

            if (String.IsNullOrEmpty(dbParm.EndCount))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.EndCount;
            }

            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_review_select",true, parm);
            return userGroupEntity;
        }

        public static UserGroupEntity UserMainListSelect(UserGroupEntity userGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERGROUPID",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserGroupID;
            }

            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_main_select",true, parm);
            return userGroupEntity;
        }

        public static UserGroupEntity UserDetailListSelect(UserGroupEntity userGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERGROUPID",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserGroupID;
            }

            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_detail_select",false, parm);
            return userGroupEntity;
        }

        public static UserGroupEntity UserDetailListQuery(UserGroupEntity userGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERGROUPID",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserGroupID;
            }

            userGroupEntity.QueryResult = DbManager.Query("UserGroup", "t_lm_usergroup_detail_query", parm, (userGroupEntity.PageCurrent - 1) * userGroupEntity.PageSize, userGroupEntity.PageSize, true);
                //HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_detail_query", parm);
            return userGroupEntity;
        }

        public static UserGroupEntity ExportUserDetailList(UserGroupEntity userGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERGROUPID",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserGroupID;
            }

            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_detail_query",true, parm);
            return userGroupEntity;
        }

        public static UserGroupEntity UserDetailListCount(UserGroupEntity userGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERGROUPID",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserGroupID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserGroupID;
            }

            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_detail_count",true, parm);
            return userGroupEntity;
        }

        public static int CheckInsert(UserGroupEntity userGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERGROUPNM",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();
            parm[0].Value = dbParm.UserGroupNM;
            userGroupEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup",false, parm);

            if (userGroupEntity.QueryResult.Tables.Count > 0 && userGroupEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckConsultInsert(UserEntity userEntity)
        {
            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar),
                                    new OracleParameter("RTYPE",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TAGID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.RType))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RType;
            }

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.TagID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.TagID;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.SalesID;
            }

            string strSql = "t_lm_consult_room_insert_city_check";

            if ("1".Equals(dbParm.RType))
            {
                strSql = "t_lm_consult_room_insert_tag_check";
            }
            else if ("2".Equals(dbParm.RType))
            {
                strSql = "t_lm_consult_room_insert_hotel_check";
            }
            else if ("3".Equals(dbParm.RType))
            {
                strSql = "t_lm_consult_room_insert_sales_check";
            }


            userEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("User", strSql, false, parm);

            if (userEntity.QueryResult.Tables.Count > 0 && userEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckConsultPorderInsert(UserEntity userEntity)
        {
            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar),
                                    new OracleParameter("RTYPE",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TAGID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.RType))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RType;
            }

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.TagID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.TagID;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.HotelID;
            }

            string strSql = "t_lm_consult_porder_insert_city_check";

            if ("1".Equals(dbParm.RType))
            {
                strSql = "t_lm_consult_porder_insert_tag_check";
            }
            else if ("2".Equals(dbParm.RType))
            {
                strSql = "t_lm_consult_porder_insert_hotel_check";
            }


            userEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("User", strSql, false, parm);

            if (userEntity.QueryResult.Tables.Count > 0 && userEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static DataSet GetHistoryInsert(UserGroupEntity userGroupEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERGROUPNM",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();
            parm[0].Value = dbParm.UserGroupNM;
            
            return HotelVp.Common.DBUtility.DbManager.Query("UserGroup", "t_lm_usergroup_historylist",false, parm);
        }

        public static DataSet CheckCity(UserGroupEntity userGroupEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                };
            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();
            parm[0].Value = dbParm.UserGroupID;
            return  HotelVp.Common.DBUtility.DbManager.Query("City", "t_fc_city_sigle",false, parm);
        }


        public static int Insert(UserGroupEntity userGroupEntity)
        {
            if (userGroupEntity.UserGroupDBEntity.Count == 0)
            {
                return 0;
            }

            if (userGroupEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(userGroupEntity) > 0)
            {
                return 2;
            }

            UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            string manualAdd = dbParm.ManualAdd.Replace("'", "").Replace("，",",").Replace("\r\n", "");
            string[] manualList = manualAdd.Split(',');
            string errorString = "";
            string complString = "";
            ArrayList complList = new ArrayList();
            ArrayList tempList = new ArrayList();

            int MaxLength = 300;
            int iLength = 0;

            for (int imanual = 0; imanual <= manualList.Count() - 1; imanual++)
            {
                if (IsMobileNumber(manualList[imanual].ToString().Trim()) && manualList[imanual].ToString().Trim().Length == 11)
                {
                    if (!tempList.Contains(manualList[imanual].ToString().Trim()))
                    {
                        tempList.Add(manualList[imanual].ToString().Trim());
                        complString = complString + manualList[imanual].ToString().Trim() + ",";
                        iLength = iLength + 1;

                        if (iLength == MaxLength)
                        {
                            iLength = 0;
                            complString = complString.Substring(0, complString.Length - 1);
                            complList.Add(complString);
                            complString = "";
                        }
                    }
                }
                else
                {
                    errorString = errorString + manualList[imanual].ToString().Trim() + ",";
                }
            }

            if (iLength < MaxLength)
            {
                complString = (complString.Length > 0) ? complString.Substring(0, complString.Length - 1) : complString;
                complList.Add(complString);
            }

            errorString = (errorString.Length > 0) ? errorString.Substring(0, errorString.Length - 1) : errorString.Trim();
            //complList = (complList.Length > 0) ? complList.Substring(0, complList.Length - 1) : complList.Trim();
            userGroupEntity.UserGroupDBEntity[0].ErrManualAdd = errorString;
            userGroupEntity.UserGroupDBEntity[0].ComplManualAdd = complList;

            //if (CheckInsert(userGroupEntity) > 0)
            //{
            //    return 2;
            //}

            //DataSet dsCity = CheckCity(userGroupEntity);
            //if (dsCity.Tables.Count == 0 || dsCity.Tables[0].Rows.Count == 0)
            //{
            //    return 3;
            //}

            //UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            List<CommandInfo> sqlList = new List<CommandInfo>();
            CommandInfo InsertLmUserGroupInfo = new CommandInfo();
            OracleParameter[] lmUserGroupParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("USERGROUPNAME",OracleType.VarChar),
                                    new OracleParameter("REGISTSTART",OracleType.VarChar),
                                    new OracleParameter("REGISTEND",OracleType.VarChar),
                                    new OracleParameter("LOGINSTART",OracleType.VarChar),
                                    new OracleParameter("LOGINEND",OracleType.VarChar),
                                    new OracleParameter("SUBMITORDERFROM",OracleType.Int32),
                                    new OracleParameter("SUBMITORDERTO",OracleType.Int32),
                                    new OracleParameter("COMPLETEORDERFROM",OracleType.Int32),
                                    new OracleParameter("COMPLETEORDERTO",OracleType.Int32),
                                    new OracleParameter("LASTORDERSTART",OracleType.VarChar),
                                    new OracleParameter("LASTORDEREND",OracleType.VarChar),
                                    new OracleParameter("MANUALADD",OracleType.VarChar)    
                                };

            lmUserGroupParm[0].Value = getMaxIDfromSeq("T_LM_USERGROUP_SEQ");
            lmUserGroupParm[1].Value = dbParm.UserGroupNM;

            if (String.IsNullOrEmpty(dbParm.RegistStart))
            {
                lmUserGroupParm[2].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[2].Value = DateTime.Parse(dbParm.RegistStart);
                lmUserGroupParm[2].Value = dbParm.RegistStart;
            }

            if (String.IsNullOrEmpty(dbParm.RegistEnd))
            {
                lmUserGroupParm[3].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[3].Value = DateTime.Parse(dbParm.RegistEnd);
                lmUserGroupParm[3].Value = dbParm.RegistEnd;
            }

            if (String.IsNullOrEmpty(dbParm.LoginStart))
            {
                lmUserGroupParm[4].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[4].Value = DateTime.Parse(dbParm.LoginStart);
                lmUserGroupParm[4].Value = dbParm.LoginStart;
            }

            if (String.IsNullOrEmpty(dbParm.LoginEnd))
            {
                lmUserGroupParm[5].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[5].Value = DateTime.Parse(dbParm.LoginEnd);
                lmUserGroupParm[5].Value = dbParm.LoginEnd;
            }

            if (String.IsNullOrEmpty(dbParm.SubmitOrderFrom))
            {
                lmUserGroupParm[6].Value = DBNull.Value;
            }
            else
            {
                lmUserGroupParm[6].Value = Int32.Parse(dbParm.SubmitOrderFrom);
            }

            if (String.IsNullOrEmpty(dbParm.SubmitOrderTo))
            {
                lmUserGroupParm[7].Value = DBNull.Value;
            }
            else
            {
                lmUserGroupParm[7].Value = Int32.Parse(dbParm.SubmitOrderTo);
            }

            if (String.IsNullOrEmpty(dbParm.CompleteOrderFrom))
            {
                lmUserGroupParm[8].Value = DBNull.Value;
            }
            else
            {
                lmUserGroupParm[8].Value = Int32.Parse(dbParm.CompleteOrderFrom);
            }

            if (String.IsNullOrEmpty(dbParm.CompleteOrderTo))
            {
                lmUserGroupParm[9].Value = DBNull.Value;
            }
            else
            {
                lmUserGroupParm[9].Value = Int32.Parse(dbParm.CompleteOrderTo);
            }

            if (String.IsNullOrEmpty(dbParm.LastOrderStart))
            {
                lmUserGroupParm[10].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[10].Value = DateTime.Parse(dbParm.LastOrderStart);
                lmUserGroupParm[10].Value = dbParm.LastOrderStart;
            }

            if (String.IsNullOrEmpty(dbParm.LastOrderEnd))
            {
                lmUserGroupParm[11].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[11].Value = DateTime.Parse(dbParm.LastOrderEnd);
                lmUserGroupParm[11].Value = dbParm.LastOrderEnd;
            }

            lmUserGroupParm[12].Value = dbParm.ManualAdd;

            InsertLmUserGroupInfo.SqlName = "UserGroup";
            InsertLmUserGroupInfo.SqlId = "t_lm_usergroup_insert";
            InsertLmUserGroupInfo.Parameters = lmUserGroupParm;
            sqlList.Add(InsertLmUserGroupInfo);

            string[] RegChannelList = dbParm.RegChannelList.Trim().Replace("'", "").Split(',');
            string RegChannelString = "";
            if (RegChannelList.Length > 0)
            {
                foreach (string RegCrlString in RegChannelList)
                {
                    if (RegCrlString.Trim().Length > 0)
                    {
                        RegChannelString = RegChannelString + RegCrlString.Substring(RegCrlString.IndexOf('_') + 1) + ",";
                    }
                }

                //RegChannelString = RegChannelString.Substring(0, RegChannelString.Length - 1);

                CommandInfo InsertlmUGROUPRCLInfo = new CommandInfo();

                OracleParameter[] lmRclParm ={
                                    new OracleParameter("USERGROUPID_RCL",OracleType.VarChar),                                    
                                    new OracleParameter("REGCHANELCODE",OracleType.VarChar)                                 
                                };

                lmRclParm[0].Value = lmUserGroupParm[0].Value;
                lmRclParm[1].Value = RegChannelString;
                InsertlmUGROUPRCLInfo.SqlName = "UserGroup";
                InsertlmUGROUPRCLInfo.SqlId = "t_lm_usergroup_rechanel_insert";
                InsertlmUGROUPRCLInfo.Parameters = lmRclParm;
                sqlList.Add(InsertlmUGROUPRCLInfo);
            }

            string PlatformList = dbParm.PlatformList.Trim().Replace("'", "");
            if (PlatformList.Length > 0)
            {
                CommandInfo InsertlmUGROUPPTMInfo = new CommandInfo();

                OracleParameter[] lmPtmParm ={
                                    new OracleParameter("USERGROUPID_PTM",OracleType.VarChar),                                    
                                    new OracleParameter("PLATFORMCODE",OracleType.VarChar)                                 
                                };

                lmPtmParm[0].Value = lmUserGroupParm[0].Value;
                lmPtmParm[1].Value = PlatformList;
                InsertlmUGROUPPTMInfo.SqlName = "UserGroup";
                InsertlmUGROUPPTMInfo.SqlId = "t_lm_usergroup_platform_insert";
                InsertlmUGROUPPTMInfo.Parameters = lmPtmParm;
                sqlList.Add(InsertlmUGROUPPTMInfo);
            }

            CommandInfo InsertLmUserListInfo = new CommandInfo();
            OracleParameter[] lmUserListParm ={
                                    new OracleParameter("USERGROUPID",OracleType.Int32),
                                    new OracleParameter("REGISTSTART",OracleType.VarChar),
                                    new OracleParameter("REGISTEND",OracleType.VarChar),
                                    new OracleParameter("LOGINSTART",OracleType.VarChar),
                                    new OracleParameter("LOGINEND",OracleType.VarChar),
                                    new OracleParameter("SUBMITORDERFROM",OracleType.Int32),
                                    new OracleParameter("SUBMITORDERTO",OracleType.Int32),
                                    new OracleParameter("COMPLETEORDERFROM",OracleType.Int32),
                                    new OracleParameter("COMPLETEORDERTO",OracleType.Int32),
                                    new OracleParameter("LASTORDERSTART",OracleType.VarChar),
                                    new OracleParameter("LASTORDEREND",OracleType.VarChar),
                                    new OracleParameter("REGCHANNELLIST",OracleType.VarChar),
                                    new OracleParameter("PLATFORMLIST",OracleType.VarChar)
                                };

            lmUserListParm[0].Value = lmUserGroupParm[0].Value;

            if (String.IsNullOrEmpty(dbParm.RegistStart))
            {
                lmUserListParm[1].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[2].Value = DateTime.Parse(dbParm.RegistStart);
                lmUserListParm[1].Value = dbParm.RegistStart;
            }

            if (String.IsNullOrEmpty(dbParm.RegistEnd))
            {
                lmUserListParm[2].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[3].Value = DateTime.Parse(dbParm.RegistEnd);
                lmUserListParm[2].Value = dbParm.RegistEnd;
            }

            if (String.IsNullOrEmpty(dbParm.LoginStart))
            {
                lmUserListParm[3].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[4].Value = DateTime.Parse(dbParm.LoginStart);
                lmUserListParm[3].Value = dbParm.LoginStart;
            }

            if (String.IsNullOrEmpty(dbParm.LoginEnd))
            {
                lmUserListParm[4].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[5].Value = DateTime.Parse(dbParm.LoginEnd);
                lmUserListParm[4].Value = dbParm.LoginEnd;
            }

            if (String.IsNullOrEmpty(dbParm.SubmitOrderFrom))
            {
                lmUserListParm[5].Value = DBNull.Value;
            }
            else
            {
                lmUserListParm[5].Value = Int32.Parse(dbParm.SubmitOrderFrom);
            }

            if (String.IsNullOrEmpty(dbParm.SubmitOrderTo))
            {
                lmUserListParm[6].Value = DBNull.Value;
            }
            else
            {
                lmUserListParm[6].Value = Int32.Parse(dbParm.SubmitOrderTo);
            }

            if (String.IsNullOrEmpty(dbParm.CompleteOrderFrom))
            {
                lmUserListParm[7].Value = DBNull.Value;
            }
            else
            {
                lmUserListParm[7].Value = Int32.Parse(dbParm.CompleteOrderFrom);
            }

            if (String.IsNullOrEmpty(dbParm.CompleteOrderTo))
            {
                lmUserListParm[8].Value = DBNull.Value;
            }
            else
            {
                lmUserListParm[8].Value = Int32.Parse(dbParm.CompleteOrderTo);
            }

            if (String.IsNullOrEmpty(dbParm.LastOrderStart))
            {
                lmUserListParm[9].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[10].Value = DateTime.Parse(dbParm.LastOrderStart);
                lmUserListParm[9].Value = dbParm.LastOrderStart;
            }

            if (String.IsNullOrEmpty(dbParm.LastOrderEnd))
            {
                lmUserListParm[10].Value = DBNull.Value;
            }
            else
            {
                //lmUserGroupParm[11].Value = DateTime.Parse(dbParm.LastOrderEnd);
                lmUserListParm[10].Value = dbParm.LastOrderEnd;
            }

            if (String.IsNullOrEmpty(RegChannelString))
            {
                lmUserListParm[11].Value = DBNull.Value;
            }
            else
            {
                lmUserListParm[11].Value = (RegChannelString.Length > 1) ? RegChannelString.Substring(0, RegChannelString.Length -1) : "";
            }

            if (String.IsNullOrEmpty(PlatformList))
            {
                lmUserListParm[12].Value = DBNull.Value;
            }
            else
            {
                lmUserListParm[12].Value = (PlatformList.Length > 1) ? PlatformList.Substring(0, PlatformList.Length - 1) : "";
            }

            InsertLmUserListInfo.SqlName = "UserGroup";
            InsertLmUserListInfo.SqlId = "t_lm_usergroup_ult_insert";
            InsertLmUserListInfo.Parameters = lmUserListParm;
            sqlList.Add(InsertLmUserListInfo);


            if (userGroupEntity.UserGroupDBEntity[0].ComplManualAdd.Count > 0)
            {
                foreach (string strManual in userGroupEntity.UserGroupDBEntity[0].ComplManualAdd)
                {
                    if (String.IsNullOrEmpty(strManual))
                    {
                        continue;
                    }
                    CommandInfo InsertLmManualUserListInfo = new CommandInfo();
                    OracleParameter[] lmManualUserParm ={
                                    new OracleParameter("USERGROUPID",OracleType.Int32),
                                    new OracleParameter("MANUALADD",OracleType.VarChar)    
                                };

                    lmManualUserParm[0].Value = lmUserGroupParm[0].Value;
                    lmManualUserParm[1].Value = strManual;

                    InsertLmManualUserListInfo.SqlName = "UserGroup";
                    InsertLmManualUserListInfo.SqlId = "t_lm_usergroup_ult_manual_insert";
                    InsertLmManualUserListInfo.Parameters = lmManualUserParm;
                    sqlList.Add(InsertLmManualUserListInfo);
                }
            }

            DbManager.ExecuteSqlTran(sqlList);

            if (userGroupEntity.UserGroupDBEntity[0].ErrManualAdd.Length > 0)
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }

        public static int InsertConsultRoomUser(UserEntity userEntity)
        {
            if (userEntity.UserDBEntity.Count == 0)
            {
                return 0;
            }

            if (userEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckConsultInsert(userEntity) > 0)
            {
                return 2;
            }

            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar),
                                    new OracleParameter("RTYPE",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TAGID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("TCID",OracleType.Int32),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.RType))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RType;
            }

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.TagID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.TagID;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.HotelID;
            }

            parm[5].Value = userEntity.LogMessages.Username;

            parm[6].Value = getMaxIDfromSeq("T_LM_CONSULT_ROOM_SEQ");

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.SalesID;
            }

            HotelVp.Common.DBUtility.DbManager.ExecuteSql("User", "t_lm_consult_room_user_insert", parm);//t_lm_consult_room_seq.nextval

            if (dbParm.ALDelHT.Length > 0)
            {
                List<CommandInfo> sqlList = new List<CommandInfo>();
                string[] alHotel = dbParm.ALDelHT.Split(',');
                foreach (string strHotel in alHotel)
                {
                    if (String.IsNullOrEmpty(strHotel.Trim()))
                    {
                        continue;
                    }
                    CommandInfo InsertHotelExInfo = new CommandInfo();
                    OracleParameter[] lmHotelExParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("TLCRID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar)
                                };
                    lmHotelExParm[0].Value = getMaxIDfromSeq("T_LM_CONSULT_ROOM_HOTEL_EX_SEQ");
                    lmHotelExParm[1].Value = parm[6].Value;
                    lmHotelExParm[2].Value = strHotel;
                    lmHotelExParm[3].Value = userEntity.LogMessages.Username;

                    InsertHotelExInfo.SqlName = "User";
                    InsertHotelExInfo.SqlId = "t_lm_consult_room_hotel_insert";
                    InsertHotelExInfo.Parameters = lmHotelExParm;
                    sqlList.Add(InsertHotelExInfo);
                }
                DbManager.ExecuteSqlTran(sqlList);
            }
            return 1;
        }

        public static int InsertConsultPOrderUser(UserEntity userEntity)
        {
            if (userEntity.UserDBEntity.Count == 0)
            {
                return 0;
            }

            if (userEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckConsultPorderInsert(userEntity) > 0)
            {
                return 2;
            }

            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar),
                                    new OracleParameter("RTYPE",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TAGID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("TCID",OracleType.Int32)
                                };

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.RType))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RType;
            }

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.TagID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.TagID;
            }

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.HotelID;
            }

            parm[5].Value = userEntity.LogMessages.Username;
            parm[6].Value = getMaxIDfromSeq("T_LM_CONSULT_ROOM_SEQ");

            HotelVp.Common.DBUtility.DbManager.ExecuteSql("User", "t_lm_consult_porder_user_insert", parm);//t_lm_consult_room_seq.nextval
            if (!String.IsNullOrEmpty(dbParm.ALDelHT))
            {
                if (dbParm.ALDelHT.Length > 0)
                {
                    List<CommandInfo> sqlList = new List<CommandInfo>();
                    string[] alHotel = dbParm.ALDelHT.Split(',');
                    foreach (string strHotel in alHotel)
                    {
                        if (String.IsNullOrEmpty(strHotel.Trim()))
                        {
                            continue;
                        }
                        CommandInfo InsertHotelExInfo = new CommandInfo();
                        OracleParameter[] lmHotelExParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("TLCRID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar)
                                };
                        lmHotelExParm[0].Value = getMaxIDfromSeq("T_LM_CONSULT_ROOM_HOTEL_EX_SEQ");
                        lmHotelExParm[1].Value = parm[6].Value;
                        lmHotelExParm[2].Value = strHotel;
                        lmHotelExParm[3].Value = userEntity.LogMessages.Username;

                        InsertHotelExInfo.SqlName = "User";
                        InsertHotelExInfo.SqlId = "t_lm_consult_room_hotel_insert";
                        InsertHotelExInfo.Parameters = lmHotelExParm;
                        sqlList.Add(InsertHotelExInfo);
                    }
                    DbManager.ExecuteSqlTran(sqlList);
                }
            }
            return 1;
        }

        public static int DeleteConsultRoomUser(UserEntity userEntity)
        {
            if (userEntity.UserDBEntity.Count == 0)
            {
                return 0;
            }

            if (userEntity.LogMessages == null)
            {
                return 0;
            }

            //if (CheckConsultDel(userEntity) > 0)
            //{
            //    return 2;
            //}

            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("CONID",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.UserNo))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserNo;
            }

            HotelVp.Common.DBUtility.DbManager.ExecuteSql("User", "t_lm_consult_room_user_del", parm);
            return 1;
        }

        private static string AppendString(string param)
        {
            string result = "";
            string[] temp = param.Split(',');

            foreach (string strTemp in temp)
            {
                result = (!String.IsNullOrEmpty(strTemp)) ? result + "'" + strTemp + "'," : result;
            }

            result = (result.Length > 1) ? result.Substring(0, result.Length - 1) : result;

            return result;
        }

        private static bool IsMobileNumber(string str_telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"(1[3,4,5,8][0-9])\d{8}$");
        }

        public static int Update(UserGroupEntity userGroupEntity)
        {
            if (userGroupEntity.UserGroupDBEntity.Count == 0)
            {
                return 0;
            }

            if (userGroupEntity.LogMessages == null)
            {
                return 0;
            }

            //OracleParameter[] parm ={
            //                        new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                 
            //                    };

            //UserGroupDBEntity dbParm = (userGroupEntity.UserGroupDBEntity.Count > 0) ? userGroupEntity.UserGroupDBEntity[0] : new UserGroupDBEntity();

            //parm[0].Value = dbParm.UserGroupNo;
            ////parm[1].Value = dbParm.OnlineStatus;
            //DbManager.ExecuteSql("City", "t_cs_city_update", parm);
            //DataCommand cmd = DataCommandManager.GetDataCommand("UpdateCityList");
            //foreach (UserGroupDBEntity dbParm in userGroupEntity.UserGroupDBEntity)
            //{
            //    cmd.SetParameterValue("@ChannelNo", dbParm.ChannelNo);
            //    cmd.SetParameterValue("@ChannelID", dbParm.ChannelID);
            //    cmd.SetParameterValue("@NameCN", dbParm.Name_CN);
            //    cmd.SetParameterValue("@NameEN", PinyinHelper.GetPinyin(dbParm.Name_CN));
            //    cmd.SetParameterValue("@OnlineStatus", dbParm.OnlineStatus);
            //    cmd.SetParameterValue("@Remark", dbParm.Remark);
            //    cmd.SetParameterValue("@UpdateUser", (userGroupEntity.LogMessages != null) ? userGroupEntity.LogMessages.Userid : "");
            //    cmd.ExecuteNonQuery();
            //}
            return 1;
        }

        //通过sequence查询得到下一个ID值,数据库为Oracle
        public static int getMaxIDfromSeq(string sequencename)
        {
            int seqID = 1;
            string sql = "select " + sequencename + ".nextval from dual";
            object obj = DbHelperOra.GetSingle(sql,false);
            if (obj != null)
            {
                seqID = Convert.ToInt32(obj);
            }
            return seqID;
        }
    }
}