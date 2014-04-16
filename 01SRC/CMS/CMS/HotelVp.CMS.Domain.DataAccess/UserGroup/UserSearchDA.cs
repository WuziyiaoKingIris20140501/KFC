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
    public abstract class UserSearchDA
    {
        public static UserEntity CommonSelect(UserEntity userEntity)
        {
            userEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("City", "t_fc_city", true);
            return userEntity;
        }

        public static UserEntity GetRegChannelList(UserEntity userEntity)
        {
            userEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("User", "t_lm_regchnnel_list", true);
            return userEntity;
        }

        public static UserEntity GetPlatFormList(UserEntity userEntity)
        {
            userEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("User", "t_lm_platForm_list", true);
            return userEntity;
        }

        public static UserEntity CreateUserSelect(UserEntity userEntity)
        {
            userEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("User", "t_lm_usergroup_select", true);
            return userEntity;
        }

        public static UserEntity ReviewSelectCount(UserEntity userEntity)
        {
            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();
            string SqlString = "t_lm_user_review_count";
            if (String.IsNullOrEmpty(dbParm.TicketType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("UserID",OracleType.VarChar),
                                    new OracleParameter("RegistStart",OracleType.VarChar),
                                    new OracleParameter("RegistEnd",OracleType.VarChar),
                                    new OracleParameter("RegChannelID",OracleType.VarChar),
                                    new OracleParameter("PlatformID",OracleType.VarChar),
                                    new OracleParameter("OrderFrom",OracleType.VarChar),
                                    new OracleParameter("OrderTo",OracleType.VarChar),
                                    new OracleParameter("OrderSucFrom",OracleType.VarChar),
                                    new OracleParameter("OrderSucTo",OracleType.VarChar),
                                    new OracleParameter("LoginStart",OracleType.VarChar),
                                    new OracleParameter("LoginEnd",OracleType.VarChar),
                                    new OracleParameter("LoginSizeStart",OracleType.VarChar),
                                    new OracleParameter("LoginSizeEnd",OracleType.VarChar)
                                };


                if (String.IsNullOrEmpty(dbParm.UserID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.UserID;
                    SqlString = "t_lm_user_review_user_count";
                }

                if (String.IsNullOrEmpty(dbParm.RegistStart))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.RegistStart;
                }

                if (String.IsNullOrEmpty(dbParm.RegistEnd))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.RegistEnd;
                }

                if (String.IsNullOrEmpty(dbParm.RegChannelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.RegChannelID;
                }

                if (String.IsNullOrEmpty(dbParm.PlatformID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.PlatformID;
                }

                if (String.IsNullOrEmpty(dbParm.OrderFrom))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = dbParm.OrderFrom;
                }

                if (String.IsNullOrEmpty(dbParm.OrderTo))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.OrderTo;
                }

                if (String.IsNullOrEmpty(dbParm.OrderSucFrom))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = dbParm.OrderSucFrom;
                }

                if (String.IsNullOrEmpty(dbParm.OrderSucTo))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = dbParm.OrderSucTo;
                }

                if (String.IsNullOrEmpty(dbParm.LoginStart))
                {
                    parm[9].Value = DBNull.Value;
                }
                else
                {
                    parm[9].Value = dbParm.LoginStart;
                }

                if (String.IsNullOrEmpty(dbParm.LoginEnd))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = dbParm.LoginEnd;
                }

                if (String.IsNullOrEmpty(dbParm.LoginSizeStart))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = dbParm.LoginSizeStart;
                }

                if (String.IsNullOrEmpty(dbParm.LoginSizeEnd))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = dbParm.LoginSizeEnd;
                }

                if (String.IsNullOrEmpty(dbParm.OrderFrom) && String.IsNullOrEmpty(dbParm.OrderTo) && String.IsNullOrEmpty(dbParm.OrderSucFrom) && String.IsNullOrEmpty(dbParm.OrderSucTo))
                {
                    SqlString = ("t_lm_user_review_count".Equals(SqlString)) ? "t_lm_user_review_no_order_count" : "t_lm_user_review_user_no_order_count";
                }
                userEntity.QueryResult = DbManager.Query("User", SqlString , true, parm);
            }
            else
            {
                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(dbParm.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.PackageName;
                }

                if (String.IsNullOrEmpty(dbParm.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.AmountFrom;
                }

                if (String.IsNullOrEmpty(dbParm.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.AmountTo;
                }

                if (String.IsNullOrEmpty(dbParm.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.PickfromDate;
                }

                if (String.IsNullOrEmpty(dbParm.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.PicktoDate;
                }

                if (String.IsNullOrEmpty(dbParm.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = dbParm.TicketType;
                }

                if (String.IsNullOrEmpty(dbParm.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.TicketTime;
                }

                //if (String.IsNullOrEmpty(dbParm.TicketType))
                //{
                //    parm[0].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[0].Value = dbParm.TicketType;
                //}

                string strSql = "";
                if ("1".Equals(dbParm.TicketData))
                {
                    strSql = "t_lm_user_review_ticketype_ord_count"; 
                }
                else
                {
                    strSql = "t_lm_user_review_ticketype_all_count";
                }
                userEntity.QueryResult = DbManager.Query("User", strSql, true, parm);
            }
            return userEntity;
        }

        public static UserEntity ExportReviewSelect(UserEntity userEntity)
        {
            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();
            string SqlString = "t_lm_user_review_select";
            if (String.IsNullOrEmpty(dbParm.TicketType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("UserID",OracleType.VarChar),
                                    new OracleParameter("RegistStart",OracleType.VarChar),
                                    new OracleParameter("RegistEnd",OracleType.VarChar),
                                    new OracleParameter("RegChannelID",OracleType.VarChar),
                                    new OracleParameter("PlatformID",OracleType.VarChar),
                                    new OracleParameter("OrderFrom",OracleType.VarChar),
                                    new OracleParameter("OrderTo",OracleType.VarChar),
                                    new OracleParameter("OrderSucFrom",OracleType.VarChar),
                                    new OracleParameter("OrderSucTo",OracleType.VarChar),
                                    new OracleParameter("LoginStart",OracleType.VarChar),
                                    new OracleParameter("LoginEnd",OracleType.VarChar),
                                    new OracleParameter("LoginSizeStart",OracleType.VarChar),
                                    new OracleParameter("LoginSizeEnd",OracleType.VarChar)
                                };


                if (String.IsNullOrEmpty(dbParm.UserID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.UserID;
                    SqlString = "t_lm_user_review_user_select";
                }

                if (String.IsNullOrEmpty(dbParm.RegistStart))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.RegistStart;
                }

                if (String.IsNullOrEmpty(dbParm.RegistEnd))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.RegistEnd;
                }

                if (String.IsNullOrEmpty(dbParm.RegChannelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.RegChannelID;
                }

                if (String.IsNullOrEmpty(dbParm.PlatformID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.PlatformID;
                }

                if (String.IsNullOrEmpty(dbParm.OrderFrom))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = dbParm.OrderFrom;
                }

                if (String.IsNullOrEmpty(dbParm.OrderTo))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.OrderTo;
                }

                if (String.IsNullOrEmpty(dbParm.OrderSucFrom))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = dbParm.OrderSucFrom;
                }

                if (String.IsNullOrEmpty(dbParm.OrderSucTo))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = dbParm.OrderSucTo;
                }

                if (String.IsNullOrEmpty(dbParm.LoginStart))
                {
                    parm[9].Value = DBNull.Value;
                }
                else
                {
                    parm[9].Value = dbParm.LoginStart;
                }

                if (String.IsNullOrEmpty(dbParm.LoginEnd))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = dbParm.LoginEnd;
                }

                if (String.IsNullOrEmpty(dbParm.LoginSizeStart))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = dbParm.LoginSizeStart;
                }

                if (String.IsNullOrEmpty(dbParm.LoginSizeEnd))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = dbParm.LoginSizeEnd;
                }
                userEntity.QueryResult = DbManager.Query("User", SqlString, true, parm);
            }
            else
            {
                //OracleParameter[] parm ={
                //                    new OracleParameter("PACKAGETYPE",OracleType.VarChar)
                //                };


                //if (String.IsNullOrEmpty(dbParm.TicketType))
                //{
                //    parm[0].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[0].Value = dbParm.TicketType;
                //}
                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(dbParm.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.PackageName;
                }

                if (String.IsNullOrEmpty(dbParm.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.AmountFrom;
                }

                if (String.IsNullOrEmpty(dbParm.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.AmountTo;
                }

                if (String.IsNullOrEmpty(dbParm.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.PickfromDate;
                }

                if (String.IsNullOrEmpty(dbParm.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.PicktoDate;
                }

                if (String.IsNullOrEmpty(dbParm.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = dbParm.TicketType;
                }

                if (String.IsNullOrEmpty(dbParm.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.TicketTime;
                }

                string strSql = "";
                if ("1".Equals(dbParm.TicketData))
                {
                    strSql = "t_lm_user_review_ticketype_ord";
                }
                else
                {
                    strSql = "t_lm_user_review_ticketype_all";
                }
                userEntity.QueryResult = DbManager.Query("User", strSql, true, parm);
            }
            return userEntity;
        }

        public static UserEntity ReviewSelect(UserEntity userEntity)
        {
            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();
            string SqlString = "t_lm_user_review_select";
            if (String.IsNullOrEmpty(dbParm.TicketType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("UserID",OracleType.VarChar),
                                    new OracleParameter("RegistStart",OracleType.VarChar),
                                    new OracleParameter("RegistEnd",OracleType.VarChar),
                                    new OracleParameter("RegChannelID",OracleType.VarChar),
                                    new OracleParameter("PlatformID",OracleType.VarChar),
                                     new OracleParameter("OrderFrom",OracleType.VarChar),
                                    new OracleParameter("OrderTo",OracleType.VarChar),
                                    new OracleParameter("OrderSucFrom",OracleType.VarChar),
                                    new OracleParameter("OrderSucTo",OracleType.VarChar),
                                    new OracleParameter("LoginStart",OracleType.VarChar),
                                    new OracleParameter("LoginEnd",OracleType.VarChar),
                                    new OracleParameter("LoginSizeStart",OracleType.VarChar),
                                    new OracleParameter("LoginSizeEnd",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.UserID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.UserID;
                    SqlString = "t_lm_user_review_user_select";
                }

                if (String.IsNullOrEmpty(dbParm.RegistStart))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.RegistStart;
                }

                if (String.IsNullOrEmpty(dbParm.RegistEnd))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.RegistEnd;
                }

                if (String.IsNullOrEmpty(dbParm.RegChannelID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.RegChannelID;
                }

                if (String.IsNullOrEmpty(dbParm.PlatformID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.PlatformID;
                }

                if (String.IsNullOrEmpty(dbParm.OrderFrom))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = dbParm.OrderFrom;
                }

                if (String.IsNullOrEmpty(dbParm.OrderTo))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.OrderTo;
                }

                if (String.IsNullOrEmpty(dbParm.OrderSucFrom))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = dbParm.OrderSucFrom;
                }

                if (String.IsNullOrEmpty(dbParm.OrderSucTo))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = dbParm.OrderSucTo;
                }

                if (String.IsNullOrEmpty(dbParm.LoginStart))
                {
                    parm[9].Value = DBNull.Value;
                }
                else
                {
                    parm[9].Value = dbParm.LoginStart;
                }

                if (String.IsNullOrEmpty(dbParm.LoginEnd))
                {
                    parm[10].Value = DBNull.Value;
                }
                else
                {
                    parm[10].Value = dbParm.LoginEnd;
                }

                if (String.IsNullOrEmpty(dbParm.LoginSizeStart))
                {
                    parm[11].Value = DBNull.Value;
                }
                else
                {
                    parm[11].Value = dbParm.LoginSizeStart;
                }

                if (String.IsNullOrEmpty(dbParm.LoginSizeEnd))
                {
                    parm[12].Value = DBNull.Value;
                }
                else
                {
                    parm[12].Value = dbParm.LoginSizeEnd;
                }

                if (String.IsNullOrEmpty(dbParm.OrderFrom) && String.IsNullOrEmpty(dbParm.OrderTo) && String.IsNullOrEmpty(dbParm.OrderSucFrom) && String.IsNullOrEmpty(dbParm.OrderSucTo))
                {
                    SqlString = ("t_lm_user_review_select".Equals(SqlString)) ?  "t_lm_user_review_select_no_order": "t_lm_user_review_user_select_no_order";
                }
                userEntity.QueryResult = DbManager.Query("User", SqlString, parm, (userEntity.PageCurrent - 1) * userEntity.PageSize, userEntity.PageSize, true);
            }
            else
            {
                //OracleParameter[] parm ={
                //                    new OracleParameter("PACKAGETYPE",OracleType.VarChar)
                //                };


                //if (String.IsNullOrEmpty(dbParm.TicketType))
                //{
                //    parm[0].Value = DBNull.Value;
                //}
                //else
                //{
                //    parm[0].Value = dbParm.TicketType;
                //}
                OracleParameter[] parm ={
                                     new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(dbParm.PackageName))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.PackageName;
                }

                if (String.IsNullOrEmpty(dbParm.AmountFrom))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.AmountFrom;
                }

                if (String.IsNullOrEmpty(dbParm.AmountTo))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.AmountTo;
                }

                if (String.IsNullOrEmpty(dbParm.PickfromDate))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.PickfromDate;
                }

                if (String.IsNullOrEmpty(dbParm.PicktoDate))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.PicktoDate;
                }

                if (String.IsNullOrEmpty(dbParm.TicketType))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = dbParm.TicketType;
                }

                if (String.IsNullOrEmpty(dbParm.TicketTime))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.TicketTime;
                }


                string strSql = "";
                if ("1".Equals(dbParm.TicketData))
                {
                    strSql = "t_lm_user_review_ticketype_ord";
                }
                else
                {
                    strSql = "t_lm_user_review_ticketype_all";
                }
                userEntity.QueryResult = DbManager.Query("User", strSql, parm, (userEntity.PageCurrent - 1) * userEntity.PageSize, userEntity.PageSize, true);
            }
            return userEntity;
        }

        public static UserEntity UserMainListSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            paymentEntity.QueryResult = DbManager.Query("User", "t_lm_user_main_select", true, parm);
            return paymentEntity;
        }

        public static UserEntity ReviewConsultRoomUserSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            DataSet dsResult = DbManager.Query("User", "t_lm_consult_room_user_main_select", true, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && dsResult.Tables[0].Select("CONSULTTYPE='销售'").Length > 0)
            {
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    if ("销售".Equals(dsResult.Tables[0].Rows[i]["CONSULTTYPE"].ToString().Trim()))
                    {
                        dsResult.Tables[0].Rows[i]["CONSULTVAL"] = GetSalesName(dsResult.Tables[0].Rows[i]["CONSULTVAL"].ToString().Trim());
                    }
                }
            }

            paymentEntity.QueryResult = dsResult;
            return paymentEntity;
        }

        public static string GetSalesName(string userAccount)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCmsSysUsersInfo");
            cmd.SetParameterValue("@UserAccount", userAccount);
            DataSet dsResult = cmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return "[" + userAccount + "]" + dsResult.Tables[0].Rows[0]["User_DspName"].ToString().Trim();
            }
            else
            {
                return userAccount;
            }
        }

        public static UserEntity ReviewConsultPOrderUserSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            paymentEntity.QueryResult = DbManager.Query("User", "t_lm_consult_porder_user_main_select", true, parm);
            return paymentEntity;
        }

        public static UserEntity PreConsultRoomUserSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TAGID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            string strSql = "";

            if ("0".Equals(dbParm.RType))
            {
                parm[0].Value = dbParm.CityID;
                parm[1].Value = DBNull.Value;
                parm[2].Value = DBNull.Value;
                parm[3].Value = DBNull.Value;
                strSql = "t_lm_consult_room_user_pre_city";
            }
            else if ("1".Equals(dbParm.RType))
            {
                parm[0].Value = DBNull.Value;
                parm[1].Value = dbParm.TagID;
                parm[2].Value = DBNull.Value;
                parm[3].Value = DBNull.Value;
                strSql = "t_lm_consult_room_user_pre_tag";
            }
            else if ("2".Equals(dbParm.RType))
            {
                parm[0].Value = DBNull.Value;
                parm[1].Value = DBNull.Value;
                parm[2].Value = dbParm.HotelID;
                parm[3].Value = DBNull.Value;
                strSql = "t_lm_consult_room_user_pre_hotel";
            }
            else if ("3".Equals(dbParm.RType))
            {
                parm[0].Value = DBNull.Value;
                parm[1].Value = DBNull.Value;
                parm[2].Value = DBNull.Value;
                parm[3].Value = dbParm.SalesID;
                strSql = "t_lm_consult_room_user_pre_sales";
            }

            paymentEntity.QueryResult = DbManager.Query("User", strSql, true, parm);
            return paymentEntity;
        }

        public static UserEntity PreConsultPOrderUserSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TAGID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            string strSql = "";

            if ("0".Equals(dbParm.RType))
            {
                parm[0].Value = dbParm.CityID;
                parm[1].Value = DBNull.Value;
                parm[2].Value = DBNull.Value;
                strSql = "t_lm_consult_porder_user_pre_city";
            }
            else if ("1".Equals(dbParm.RType))
            {
                parm[0].Value = DBNull.Value;
                parm[1].Value = dbParm.TagID;
                parm[2].Value = DBNull.Value;
                strSql = "t_lm_consult_porder_user_pre_tag";
            }
            else if ("2".Equals(dbParm.RType))
            {
                parm[0].Value = DBNull.Value;
                parm[1].Value = DBNull.Value;
                parm[2].Value = dbParm.HotelID;
                strSql = "t_lm_consult_porder_user_pre_hotel";
            }

            paymentEntity.QueryResult = DbManager.Query("User", strSql, true, parm);
            return paymentEntity;
        }

        public static UserEntity ReviewConsultRoomUserDetail(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("KEYID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserNo))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserNo;
            }

            paymentEntity.QueryResult = DbManager.Query("User", "t_lm_consult_room_user_detail_select", true, parm);
            return paymentEntity;
        }

        public static UserEntity ReviewConsultPOrderUserDetail(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("KEYID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserNo))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserNo;
            }

            paymentEntity.QueryResult = DbManager.Query("User", "t_lm_consult_porder_user_detail_select", true, parm);
            return paymentEntity;
        }

        public static UserEntity GetUserIDFromMobile(UserEntity userEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("MOBILE",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (userEntity.UserDBEntity.Count > 0) ? userEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.LoginMobile))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.LoginMobile;
            }

            userEntity.QueryResult = DbManager.Query("User", "t_lm_user_select_userid", true, parm);
            return userEntity;
        }

        public static UserEntity UserCashMainListSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            paymentEntity.QueryResult = DbManager.Query("User", "t_lm_user_cash_main_select", true, parm);
            return paymentEntity;
        }

        public static UserEntity UserDetailListSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            paymentEntity.QueryResult = DbManager.Query("User", "t_lm_user_detail_select", true, parm);
            return paymentEntity;
        }

        public static UserEntity UserCashDetailListSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();

            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            paymentEntity.QueryResult = DbManager.Query("User", "t_lm_user_cash_detail_select", true, parm);
            return paymentEntity;
        }

        public static UserEntity UserCashPopListSelect(UserEntity paymentEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar),
                                    new OracleParameter("PKEY",OracleType.VarChar)
                                };
            UserDBEntity dbParm = (paymentEntity.UserDBEntity.Count > 0) ? paymentEntity.UserDBEntity[0] : new UserDBEntity();
            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.Pkey))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.Pkey;
            }
            string strSql = "";
            if ("0".Equals(dbParm.SelectType))
            {
                strSql = "t_lm_user_cash_detail_appl_select";
            }
            else
            {
                strSql = "t_lm_user_cash_detail_back_select";
            }
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(DbManager.Query("User", strSql,true, parm).Tables[0].Copy());
            dsResult.Tables[0].TableName = "MASTER";

            OracleParameter[] detailParm ={
                                    new OracleParameter("USERID",OracleType.VarChar),
                                    new OracleParameter("PKEY",OracleType.VarChar)
                                };
            if (String.IsNullOrEmpty(dbParm.UserID))
            {
                detailParm[0].Value = DBNull.Value;
            }
            else
            {
                detailParm[0].Value = dbParm.UserID;
            }

            if (String.IsNullOrEmpty(dbParm.Pkey))
            {
                detailParm[1].Value = DBNull.Value;
            }
            else
            {
                detailParm[1].Value = dbParm.Pkey;
            }
            string strDetailSql = "";
            if ("0".Equals(dbParm.SelectType))
            {
                strDetailSql = "t_lm_user_cash_appl_detail_select";
            }
            else
            {
                strDetailSql = "t_lm_user_cash_back_detail_select";
            }
            dsResult.Tables.Add(DbManager.Query("User", strDetailSql,true, detailParm).Tables[0].Copy());
            dsResult.Tables[1].TableName = "DETAIL";

            paymentEntity.QueryResult = dsResult;
            return paymentEntity;
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