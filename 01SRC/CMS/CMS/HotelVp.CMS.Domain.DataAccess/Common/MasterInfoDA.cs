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
    public abstract class MasterInfoDA
    {
        public static MasterInfoEntity CommonSelect(MasterInfoEntity masterInfoEntity)
        {
            DataSet dsResult = new DataSet();

            MasterInfoDBEntity dbParm = (masterInfoEntity.MasterInfoDBEntity.Count > 0) ? masterInfoEntity.MasterInfoDBEntity[0] : new MasterInfoDBEntity();
            string sumSql = string.Empty;
            string listSql = string.Empty;
            string userSql = string.Empty;
            string roomSql = string.Empty;
            string allSql = string.Empty;

            string allticSql = string.Empty;
            string ordticSql = string.Empty;
            string roomticSql = string.Empty;

            if (!String.IsNullOrEmpty(dbParm.Today) && "1".Equals(dbParm.Today))
            {
                sumSql = "t_lm_master_order_sum_info_today";
                listSql = "t_lm_master_order_list_info_today";
                userSql = "t_lm_master_user_info_today";
                roomSql = "t_lm_master_in_room_info_today";
                allSql = "t_lm_master_order_all_info_today";

                allticSql = "t_lm_master_ticket_info_today";
                ordticSql = "t_lm_order_ticket_info_today";
                roomticSql = "t_lm_order_ticket_in_room_info_today";
            }
            else
            {
                sumSql = "t_lm_master_order_sum_info";
                listSql = "t_lm_master_order_list_info";
                userSql = "t_lm_master_user_info";
                roomSql = "t_lm_master_in_room_info";
                allSql = "t_lm_master_order_all_info";

                allticSql = "t_lm_master_ticket_info";
                ordticSql = "t_lm_order_ticket_info";
                roomticSql = "t_lm_order_ticket_in_room_info";
            }
            
            DataTable dtOrderSum = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", sumSql, true).Tables[0].Copy();
            dtOrderSum.TableName = "OrderSum";

            DataTable dtOrderList = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", listSql, true).Tables[0].Copy();
            dtOrderList.TableName = "OrderList";

            DataTable dtUser = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", userSql, true).Tables[0].Copy();
            dtUser.TableName = "UserCount";

            DataTable dtProc = new DataTable();// HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", "t_lm_master_proc_info").Tables[0].Copy();
            dtProc.TableName = "ProcCount";

            DataTable dtInRoom = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", roomSql, true).Tables[0].Copy();
            dtInRoom.TableName = "InRoomCount";

            DataTable dtOrderAll = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", allSql, true).Tables[0].Copy();
            dtOrderAll.TableName = "OrderAll";

            DataTable dtAllTic = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", allticSql, true).Tables[0].Copy();
            dtAllTic.TableName = "AllTic";

            DataTable dtOrderTic = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", ordticSql, true).Tables[0].Copy();
            dtOrderTic.TableName = "OrderTic";

            DataTable dtRoomTic = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", roomticSql, true).Tables[0].Copy();
            dtRoomTic.TableName = "RoomTic";

            dsResult.Tables.Add(dtOrderSum);
            dsResult.Tables.Add(dtOrderList);
            dsResult.Tables.Add(dtUser);
            dsResult.Tables.Add(dtProc);
            dsResult.Tables.Add(dtInRoom);
            dsResult.Tables.Add(dtOrderAll);

            dsResult.Tables.Add(dtAllTic);
            dsResult.Tables.Add(dtOrderTic);
            dsResult.Tables.Add(dtRoomTic);

            masterInfoEntity.QueryResult = dsResult;
            return masterInfoEntity;
        }

        /// <summary>
        /// 订单、渠道统计
        /// </summary>
        /// <param name="masterInfoEntity"></param>
        /// <returns></returns>
        public static MasterInfoEntity CommonSelectOrderChannelData(MasterInfoEntity masterInfoEntity)
        {
            DataSet dsResult = new DataSet();
            OracleParameter[] parm ={
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar)
                                };

            MasterInfoDBEntity dbParm = (masterInfoEntity.MasterInfoDBEntity.Count > 0) ? masterInfoEntity.MasterInfoDBEntity[0] : new MasterInfoDBEntity();
            string sumChannelSql = "t_lm_orderchannel_days_info";

            if (String.IsNullOrEmpty(dbParm.RegistStart))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.RegistStart.Replace("/","-");
            }


            if (String.IsNullOrEmpty(dbParm.RegistEnd))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RegistEnd.Replace("/", "-");
            }

            DataTable dtOrderSum = HotelVp.Common.DBUtility.DbManager.Query("MasterPageInfo", sumChannelSql, true, parm).Tables[0].Copy();
            dtOrderSum.TableName = "OrderAll";

            dsResult.Tables.Add(dtOrderSum);
            masterInfoEntity.QueryResult = dsResult;

            return masterInfoEntity;
        }

        /// <summary>
        ///用户统计
        /// </summary>
        /// <param name="masterInfoEntity"></param>
        /// <returns></returns>
        public static MasterInfoEntity CommonSelectUser(MasterInfoEntity masterInfoEntity)
        {
            DataSet dsResult = new DataSet();

            OracleParameter[] parm ={
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar)
                                };

            MasterInfoDBEntity dbParm = (masterInfoEntity.MasterInfoDBEntity.Count > 0) ? masterInfoEntity.MasterInfoDBEntity[0] : new MasterInfoDBEntity();
            string sumUserSql = "t_lm_master_user_info_today";
            
            if (String.IsNullOrEmpty(dbParm.RegistStart))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.RegistStart;
            }


            if (String.IsNullOrEmpty(dbParm.RegistEnd))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RegistEnd;
            }

            DataTable dtUser = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", sumUserSql, true, parm).Tables[0].Copy();
            dtUser.TableName = "UserCount";

            dsResult.Tables.Add(dtUser);

            masterInfoEntity.QueryResult = dsResult;

            return masterInfoEntity;
        }

        public static MasterInfoEntity CommonSelectUserNew(MasterInfoEntity masterInfoEntity)
        {
            DataSet dsResult = new DataSet();

            OracleParameter[] parm ={
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar)
                                };

            MasterInfoDBEntity dbParm = (masterInfoEntity.MasterInfoDBEntity.Count > 0) ? masterInfoEntity.MasterInfoDBEntity[0] : new MasterInfoDBEntity();
            string sumUserSql = "t_lm_master_user_info_today_new";

            if (String.IsNullOrEmpty(dbParm.RegistStart))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.RegistStart;
            }


            if (String.IsNullOrEmpty(dbParm.RegistEnd))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RegistEnd;
            }

            DataTable dtUser = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", sumUserSql, true, parm).Tables[0].Copy();
            dtUser.TableName = "UserCount";

            dsResult.Tables.Add(dtUser);

            masterInfoEntity.QueryResult = dsResult;

            return masterInfoEntity;
        }

        /// <summary>
        /// 返回当日登陆用户数
        /// </summary>
        /// <param name="masterInfoEntity"></param>
        /// <returns></returns>
        public static MasterInfoEntity CommonSelectTodayLoginUser(MasterInfoEntity masterInfoEntity)
        {
            DataSet dsResult = new DataSet();

            OracleParameter[] parm ={
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar)
                                };

            MasterInfoDBEntity dbParm = (masterInfoEntity.MasterInfoDBEntity.Count > 0) ? masterInfoEntity.MasterInfoDBEntity[0] : new MasterInfoDBEntity();
            string sumUserSql = "t_lm_master_user_today_login_info";

            if (String.IsNullOrEmpty(dbParm.RegistStart))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.RegistStart;
            }


            if (String.IsNullOrEmpty(dbParm.RegistEnd))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RegistEnd;
            }

            DataTable dtUser = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", sumUserSql, true, parm).Tables[0].Copy();
            dtUser.TableName = "TodayLoginUserData";

            dsResult.Tables.Add(dtUser);

            masterInfoEntity.QueryResult = dsResult;

            return masterInfoEntity;
        }

        public static MasterInfoEntity CommonSelectTodayLoginUserNew(MasterInfoEntity masterInfoEntity)
        {
            DataSet dsResult = new DataSet();

            OracleParameter[] parm ={
                                    new OracleParameter("StartDate",OracleType.VarChar),
                                    new OracleParameter("EndDate",OracleType.VarChar)
                                };

            MasterInfoDBEntity dbParm = (masterInfoEntity.MasterInfoDBEntity.Count > 0) ? masterInfoEntity.MasterInfoDBEntity[0] : new MasterInfoDBEntity();
            string sumUserSql = "t_lm_master_user_today_login_info_new";

            if (String.IsNullOrEmpty(dbParm.RegistStart))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.RegistStart;
            }


            if (String.IsNullOrEmpty(dbParm.RegistEnd))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RegistEnd;
            }

            DataTable dtUser = HotelVp.Common.DBUtility.DbManager.Query("MasterInfo", sumUserSql, true, parm).Tables[0].Copy();
            dtUser.TableName = "TodayLoginUserDataNew";

            dsResult.Tables.Add(dtUser);

            masterInfoEntity.QueryResult = dsResult;

            return masterInfoEntity;
        }
    }
}