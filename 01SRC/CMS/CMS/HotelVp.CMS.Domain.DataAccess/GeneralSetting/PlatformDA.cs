using System;
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
    public abstract class PlatformDA
    {
        public static PlatformEntity Select(PlatformEntity platformEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PLATFORMNAME",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            PlatformDBEntity dbParm = (platformEntity.PlatformDBEntity.Count > 0) ? platformEntity.PlatformDBEntity[0] : new PlatformDBEntity();

            if (String.IsNullOrEmpty(dbParm.Name_CN))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.Name_CN;
            }
            

            if (String.IsNullOrEmpty(dbParm.OnlineStatus))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.OnlineStatus;
            }

            if (String.IsNullOrEmpty(dbParm.StartDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.StartDTime;
            }

            if (String.IsNullOrEmpty(dbParm.EndDTime))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.EndDTime;
            }

            platformEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Platform", "t_lm_b_platform",false, parm);
            return platformEntity;
        }

        public static int CheckUpdate(PlatformEntity platformEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PLATFORMCODE",OracleType.VarChar),
                                    new OracleParameter("PLATFORMNM",OracleType.VarChar)
                                };
            PlatformDBEntity dbParm = (platformEntity.PlatformDBEntity.Count > 0) ? platformEntity.PlatformDBEntity[0] : new PlatformDBEntity();
            parm[0].Value = dbParm.PlatformID;
            parm[1].Value = dbParm.Name_CN;
            platformEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Platform", "t_lm_b_platform_updatesigle", false, parm);

            if (platformEntity.QueryResult.Tables.Count > 0 && platformEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckInsert(PlatformEntity platformEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("PLATFORMCODE",OracleType.VarChar),
                                    new OracleParameter("PLATFORMNM",OracleType.VarChar)
                                };
            PlatformDBEntity dbParm = (platformEntity.PlatformDBEntity.Count > 0) ? platformEntity.PlatformDBEntity[0] : new PlatformDBEntity();
            parm[0].Value = dbParm.PlatformID;
            parm[1].Value = dbParm.Name_CN;
            platformEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Platform", "t_lm_b_platform_sigle", false, parm);

            if (platformEntity.QueryResult.Tables.Count > 0 && platformEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }
        public static int Insert(PlatformEntity platformEntity)
        {
            if (platformEntity.PlatformDBEntity.Count == 0)
            {
                return 0;
            }

            if (platformEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(platformEntity) > 0)
            {
                return 2;
            }

            PlatformDBEntity dbParm = (platformEntity.PlatformDBEntity.Count > 0) ? platformEntity.PlatformDBEntity[0] : new PlatformDBEntity();
           
            //List<CommandInfo> sqlList = new List<CommandInfo>();
            //CommandInfo InsertLmPlatformInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PLATFORMCODE",OracleType.VarChar),                                    
                                    new OracleParameter("PLATFORMNM",OracleType.VarChar)                                 
                                };

            lmParm[0].Value = getMaxIDfromSeq("T_LM_B_PLATFORM_SEQ");
            lmParm[1].Value = dbParm.PlatformID;
            lmParm[2].Value = dbParm.Name_CN;
            DbManager.ExecuteSql("Platform", "t_lm_b_platform_insert", lmParm);

            //InsertLmPlatformInfo.SqlName = "Platform";
            //InsertLmPlatformInfo.SqlId = "t_lm_platform_insert";
            //InsertLmPlatformInfo.Parameters = lmParm;

            //CommandInfo InsertCSPlatformInfo = new CommandInfo();

            //OracleParameter[] csParm ={
            //                        new OracleParameter("ID",OracleType.Number),
            //                        new OracleParameter("PLATFORMID",OracleType.VarChar),                                    
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)                                 
            //                    };

            //csParm[0].Value = getMaxIDfromSeq("T_CS_PLATFORM_SEQ");
            //csParm[1].Value = lmParm[0].Value;
            //csParm[2].Value = "0";
            //InsertCSPlatformInfo.SqlName = "Platform";
            //InsertCSPlatformInfo.SqlId = "t_cs_platform_insert";
            //InsertCSPlatformInfo.Parameters = csParm;

            //sqlList.Add(InsertLmPlatformInfo);
            //sqlList.Add(InsertCSPlatformInfo);
            //DbManager.ExecuteSqlTran(sqlList);

            //DataCommand cmd = DataCommandManager.GetDataCommand("InsertCityList");
            //foreach (ChannelDBEntity dbParm in channelEntity.ChannelDBEntity)
            //{
            //    cmd.SetParameterValue("@ChannelID", dbParm.ChannelID);
            //    cmd.SetParameterValue("@NameCN", dbParm.Name_CN);
            //    cmd.SetParameterValue("@NameEN", PinyinHelper.GetPinyin(dbParm.Name_CN));
            //    cmd.SetParameterValue("@OnlineStatus", dbParm.OnlineStatus);
            //    cmd.SetParameterValue("@Remark", dbParm.Remark);
            //    cmd.SetParameterValue("@CreateUser", (channelEntity.LogMessages != null) ? channelEntity.LogMessages.Userid : "");
            //    cmd.SetParameterValue("@UpdateUser", (channelEntity.LogMessages != null) ? channelEntity.LogMessages.Userid : "");
            //    cmd.ExecuteNonQuery();
            //}
            
            return 1;
        }

        public static int Update(PlatformEntity platformEntity)
        {
            if (platformEntity.PlatformDBEntity.Count == 0)
            {
                return 0;
            }

            if (platformEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckUpdate(platformEntity) > 0)
            {
                return 2;
            }

            PlatformDBEntity dbParm = (platformEntity.PlatformDBEntity.Count > 0) ? platformEntity.PlatformDBEntity[0] : new PlatformDBEntity();

            //List<CommandInfo> sqlList = new List<CommandInfo>();
            //CommandInfo UpdateLmPlatformInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.VarChar),                             
                                    new OracleParameter("PLATFORMNM",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.PlatformID;
            lmParm[1].Value = dbParm.Name_CN;
            lmParm[2].Value = dbParm.OnlineStatus;
            DbManager.ExecuteSql("Platform", "t_lm_b_platform_update", lmParm);
            //UpdateLmPlatformInfo.SqlName = "Platform";
            //UpdateLmPlatformInfo.SqlId = "t_lm_platform_update";
            //UpdateLmPlatformInfo.Parameters = lmParm;

            //CommandInfo UpdateCsPlatformInfo = new CommandInfo();
            //OracleParameter[] csParm ={
            //                        new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)                             
            //                    };

            //csParm[0].Value = dbParm.PlatformNo;
            //csParm[1].Value = dbParm.OnlineStatus;


            //UpdateCsPlatformInfo.SqlName = "Platform";
            //UpdateCsPlatformInfo.SqlId = "t_cs_platform_update";
            //UpdateCsPlatformInfo.Parameters = csParm;

            //sqlList.Add(UpdateLmPlatformInfo);
            //sqlList.Add(UpdateCsPlatformInfo);
            //DbManager.ExecuteSqlTran(sqlList);

            //OracleParameter[] parm ={
            //                        new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                 
            //                    };

            //PlatformDBEntity dbParm = (platformEntity.PlatformDBEntity.Count > 0) ? platformEntity.PlatformDBEntity[0] : new PlatformDBEntity();
           
            //parm[0].Value = dbParm.PlatformNo;
            //parm[1].Value = dbParm.OnlineStatus;
            //DbManager.ExecuteSql("Platform", "t_cs_platform_update", parm);
            //DataCommand cmd = DataCommandManager.GetDataCommand("UpdateCityList");
            //foreach (ChannelDBEntity dbParm in channelEntity.ChannelDBEntity)
            //{
            //    cmd.SetParameterValue("@ChannelNo", dbParm.ChannelNo);
            //    cmd.SetParameterValue("@ChannelID", dbParm.ChannelID);
            //    cmd.SetParameterValue("@NameCN", dbParm.Name_CN);
            //    cmd.SetParameterValue("@NameEN", PinyinHelper.GetPinyin(dbParm.Name_CN));
            //    cmd.SetParameterValue("@OnlineStatus", dbParm.OnlineStatus);
            //    cmd.SetParameterValue("@Remark", dbParm.Remark);
            //    cmd.SetParameterValue("@UpdateUser", (channelEntity.LogMessages != null) ? channelEntity.LogMessages.Userid : "");
            //    cmd.ExecuteNonQuery();
            //}
            return 1;
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