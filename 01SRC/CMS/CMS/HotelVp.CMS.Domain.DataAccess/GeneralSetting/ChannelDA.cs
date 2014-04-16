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
    public abstract class ChannelDA
    {
        public static ChannelEntity CommonSelect(ChannelEntity channelEntity)
        {
            channelEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Channel", "t_lm_b_chanel", false);
            return channelEntity;
        }

        public static ChannelEntity Select(ChannelEntity channelEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CHANELNAME",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            ChannelDBEntity dbParm = (channelEntity.ChannelDBEntity.Count > 0) ? channelEntity.ChannelDBEntity[0] : new ChannelDBEntity();

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

            channelEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Channel", "t_lm_b_chanel_list", false, parm);
            return channelEntity;
        }

        public static int CheckInsert(ChannelEntity channelEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("CHANELCODE",OracleType.VarChar),
                                    new OracleParameter("CHANELNM",OracleType.VarChar)
                                };
            ChannelDBEntity dbParm = (channelEntity.ChannelDBEntity.Count > 0) ? channelEntity.ChannelDBEntity[0] : new ChannelDBEntity();
            parm[0].Value = dbParm.ChannelID;
            parm[1].Value = dbParm.Name_CN;
            channelEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Channel", "t_lm_b_chanel_sigle", false, parm);

            if (channelEntity.QueryResult.Tables.Count > 0 && channelEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckUpdate(ChannelEntity channelEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("CHANELCODE",OracleType.VarChar),
                                    new OracleParameter("CHANELNM",OracleType.VarChar)
                                };
            ChannelDBEntity dbParm = (channelEntity.ChannelDBEntity.Count > 0) ? channelEntity.ChannelDBEntity[0] : new ChannelDBEntity();
            parm[0].Value = dbParm.ChannelID;
            parm[1].Value = dbParm.Name_CN;
            channelEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Channel", "t_lm_b_chanel_updatesigle",false , parm);

            if (channelEntity.QueryResult.Tables.Count > 0 && channelEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int Insert(ChannelEntity channelEntity)
        {
            if (channelEntity.ChannelDBEntity.Count == 0)
            {
                return 0;
            }

            if (channelEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(channelEntity) > 0)
            {
                return 2;
            }

            ChannelDBEntity dbParm = (channelEntity.ChannelDBEntity.Count > 0) ? channelEntity.ChannelDBEntity[0] : new ChannelDBEntity();

            //List<CommandInfo> sqlList = new List<CommandInfo>();
            //CommandInfo InsertLmChannelInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("CHANELCODE",OracleType.VarChar),                                    
                                    new OracleParameter("CHANELNM",OracleType.VarChar)
                                };

            lmParm[0].Value = getMaxIDfromSeq("T_LM_B_CHANEL_SEQ");
            lmParm[1].Value = dbParm.ChannelID;
            lmParm[2].Value = dbParm.Name_CN;
            //InsertLmChannelInfo.SqlName = "Channel";
            //InsertLmChannelInfo.SqlId = "t_lm_chanel_insert";
            //InsertLmChannelInfo.Parameters = lmParm;

            DbManager.ExecuteSql("Channel", "t_lm_b_chanel_insert", lmParm);

            //CommandInfo InsertCsChannelInfo = new CommandInfo();

            //OracleParameter[] csParm ={
            //                        new OracleParameter("ID",OracleType.Number),
            //                        new OracleParameter("CHANELID",OracleType.VarChar),                                    
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)                                 
            //                    };

            //csParm[0].Value = getMaxIDfromSeq("T_CS_CHANEL_SEQ");
            //csParm[1].Value = lmParm[0].Value;
            //csParm[2].Value = "0";
            //InsertCsChannelInfo.SqlName = "Channel";
            //InsertCsChannelInfo.SqlId = "t_cs_chanel_insert";
            //InsertCsChannelInfo.Parameters = csParm;

            //sqlList.Add(InsertLmChannelInfo);
            //sqlList.Add(InsertCsChannelInfo);
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

        public static int Update(ChannelEntity channelEntity)
        {
            if (channelEntity.ChannelDBEntity.Count == 0)
            {
                return 0;
            }

            if (channelEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckUpdate(channelEntity) > 0)
            {
                return 2;
            }


            ChannelDBEntity dbParm = (channelEntity.ChannelDBEntity.Count > 0) ? channelEntity.ChannelDBEntity[0] : new ChannelDBEntity();

            //List<CommandInfo> sqlList = new List<CommandInfo>();
            //CommandInfo UpdateLmChannelInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("CHANELCODE",OracleType.VarChar),                             
                                    new OracleParameter("CHANELNM",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.ChannelID;
            lmParm[1].Value = dbParm.Name_CN;
            lmParm[2].Value = dbParm.OnlineStatus;
            DbManager.ExecuteSql("Channel", "t_lm_b_chanel_update", lmParm);

            //UpdateLmChannelInfo.SqlName = "Channel";
            //UpdateLmChannelInfo.SqlId = "t_lm_chanel_update";
            //UpdateLmChannelInfo.Parameters = lmParm;

            //CommandInfo UpdateCsChannelInfo = new CommandInfo();
            //OracleParameter[] csParm ={
            //                        new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)                             
            //                    };

            //csParm[0].Value = dbParm.ChannelNo;
            //csParm[1].Value = dbParm.OnlineStatus;
 

            //UpdateCsChannelInfo.SqlName = "Channel";
            //UpdateCsChannelInfo.SqlId = "t_cs_chanel_update";
            //UpdateCsChannelInfo.Parameters = csParm;

            //sqlList.Add(UpdateLmChannelInfo);
            //sqlList.Add(UpdateCsChannelInfo);
            //DbManager.ExecuteSqlTran(sqlList);



            //OracleParameter[] parm ={
            //                        new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                 
            //                    };

      
            //parm[0].Value = dbParm.ChannelNo;
            //parm[1].Value = dbParm.OnlineStatus;
            //DbManager.ExecuteSql("Channel", "t_cs_chanel_update", parm);
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