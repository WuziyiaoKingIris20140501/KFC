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
    public abstract class RegChannelDA
    {
        public static RegChannelEntity Select(RegChannelEntity regChannelEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("REGCHANELNAME",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            RegChannelDBEntity dbParm = (regChannelEntity.RegChannelDBEntity.Count > 0) ? regChannelEntity.RegChannelDBEntity[0] : new RegChannelDBEntity();

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

            regChannelEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("RegChannel", "t_lm_b_regchanel", false, parm);
            return regChannelEntity;
        }

        public static int CheckInsert(RegChannelEntity regChannelEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("REGCHANELCODE",OracleType.VarChar),
                                    new OracleParameter("REGCHANELNM",OracleType.VarChar)
                                };
            RegChannelDBEntity dbParm = (regChannelEntity.RegChannelDBEntity.Count > 0) ? regChannelEntity.RegChannelDBEntity[0] : new RegChannelDBEntity();
            parm[0].Value = dbParm.RegChannelID;
            parm[1].Value = dbParm.Name_CN;
            regChannelEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("RegChannel", "t_lm_b_regchanel_sigle", false, parm);

            if (regChannelEntity.QueryResult.Tables.Count > 0 && regChannelEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckUpdate(RegChannelEntity regChannelEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("REGCHANELCODE",OracleType.VarChar),
                                    new OracleParameter("REGCHANELNM",OracleType.VarChar)
                                };
            RegChannelDBEntity dbParm = (regChannelEntity.RegChannelDBEntity.Count > 0) ? regChannelEntity.RegChannelDBEntity[0] : new RegChannelDBEntity();
            parm[0].Value = dbParm.RegChannelID;
            parm[1].Value = dbParm.Name_CN;
            regChannelEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("RegChannel", "t_lm_b_regchanel_updatesigle", false, parm);

            if (regChannelEntity.QueryResult.Tables.Count > 0 && regChannelEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int Insert(RegChannelEntity regChannelEntity)
        {
            if (regChannelEntity.RegChannelDBEntity.Count == 0)
            {
                return 0;
            }

            if (regChannelEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(regChannelEntity) > 0)
            {
                return 2;
            }

            RegChannelDBEntity dbParm = (regChannelEntity.RegChannelDBEntity.Count > 0) ? regChannelEntity.RegChannelDBEntity[0] : new RegChannelDBEntity();

            //List<CommandInfo> sqlList = new List<CommandInfo>();
            //CommandInfo InsertLmRegChannelInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("REGCHANELCODE",OracleType.VarChar),                                    
                                    new OracleParameter("REGCHANELNM",OracleType.VarChar)                                 
                                };

            lmParm[0].Value = getMaxIDfromSeq("T_LM_B_REGCHANEL_SEQ");
            lmParm[1].Value = dbParm.RegChannelID;
            lmParm[2].Value = dbParm.Name_CN;
            DbManager.ExecuteSql("RegChannel", "t_lm_b_regchanel_insert", lmParm);
            //InsertLmRegChannelInfo.SqlName = "RegChannel";
            //InsertLmRegChannelInfo.SqlId = "t_lm_regchanel_insert";
            //InsertLmRegChannelInfo.Parameters = lmParm;

            //CommandInfo InsertCsRegChannelInfo = new CommandInfo();

            //OracleParameter[] csParm ={
            //                        new OracleParameter("ID",OracleType.Number),
            //                        new OracleParameter("REGCHANELID",OracleType.VarChar),                                    
            //                          new OracleParameter("ONLINESTATUS",OracleType.VarChar)                                 
            //                    };

            //csParm[0].Value = getMaxIDfromSeq("T_CS_REGCHANEL_SEQ");
            //csParm[1].Value = lmParm[0].Value;
            //csParm[2].Value = "0";
            //InsertCsRegChannelInfo.SqlName = "RegChannel";
            //InsertCsRegChannelInfo.SqlId = "t_cs_regchanel_insert";
            //InsertCsRegChannelInfo.Parameters = csParm;

            //sqlList.Add(InsertLmRegChannelInfo);
            //sqlList.Add(InsertCsRegChannelInfo);
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

        public static int Update(RegChannelEntity regChannelEntity)
        {
            if (regChannelEntity.RegChannelDBEntity.Count == 0)
            {
                return 0;
            }

            if (regChannelEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckUpdate(regChannelEntity) > 0)
            {
                return 2;
            }

            RegChannelDBEntity dbParm = (regChannelEntity.RegChannelDBEntity.Count > 0) ? regChannelEntity.RegChannelDBEntity[0] : new RegChannelDBEntity();

            //List<CommandInfo> sqlList = new List<CommandInfo>();
            //CommandInfo UpdateLmRegChannelInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.VarChar),                             
                                    new OracleParameter("REGCHANELNM",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.RegChannelID;
            lmParm[1].Value = dbParm.Name_CN;
            lmParm[2].Value = dbParm.OnlineStatus;
            DbManager.ExecuteSql("RegChannel", "t_lm_b_regchanel_update", lmParm);
            //UpdateLmRegChannelInfo.SqlName = "RegChannel";
            //UpdateLmRegChannelInfo.SqlId = "t_lm_regchanel_update";
            //UpdateLmRegChannelInfo.Parameters = lmParm;

            //CommandInfo UpdateCsRegChannelInfo = new CommandInfo();
            //OracleParameter[] csParm ={
            //                       new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)                    
            //                    };

            //csParm[0].Value = dbParm.RegChannelNo;
            //csParm[1].Value = dbParm.OnlineStatus;


            //UpdateCsRegChannelInfo.SqlName = "RegChannel";
            //UpdateCsRegChannelInfo.SqlId = "t_cs_regchanel_update";
            //UpdateCsRegChannelInfo.Parameters = csParm;

            //sqlList.Add(UpdateLmRegChannelInfo);
            //sqlList.Add(UpdateCsRegChannelInfo);
            //DbManager.ExecuteSqlTran(sqlList);

            //OracleParameter[] parm ={
            //                        new OracleParameter("ID",OracleType.Number),                       
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                 
            //                    };

            //RegChannelDBEntity dbParm = (regChannelEntity.RegChannelDBEntity.Count > 0) ? regChannelEntity.RegChannelDBEntity[0] : new RegChannelDBEntity();

            //parm[0].Value = dbParm.RegChannelNo;
            //parm[1].Value = dbParm.OnlineStatus;
            //DbManager.ExecuteSql("RegChannel", "t_cs_regchanel_update", parm);
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