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
    public abstract class HotelFacilitiesDA
    {
        public static HotelFacilitiesEntity SelectTypeDetail(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            parm[0].Value = dbParm.ID;

            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_detal", false, parm);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity ServiceAllFTSelect(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_hotel_list_all", false);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity CommonFtTypeSelect(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_type_list_all", false);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity GetFtTypeMaxSeq(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_type_seq_max", false);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity GetFtHotelMaxSeq(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TYPE",OracleType.VarChar)
                                };
            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            parm[0].Value = dbParm.Type;

            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_seq_max", false, parm);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity FtDetailSelect(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            parm[0].Value = dbParm.FTID;

            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_type_detal", false, parm);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity GetFtHotelMaxForUpdate(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar)
                                };
            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            parm[0].Value = dbParm.ID;
            parm[1].Value = dbParm.Type;

            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_max_for_update", false, parm);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity BindHotelList(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_hotel", false, parm);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity ServiceTypeSelect(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TYPE",OracleType.VarChar)
                                };
            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();

            if (String.IsNullOrEmpty(dbParm.Type))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.Type;
            }

            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_list_all", false, parm);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity FtTypeSelect(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_type_list", false);
            return hotelfacilitiesEntity;
        }

        public static HotelFacilitiesEntity FacilitiesTypeSelect(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities", false);
            return hotelfacilitiesEntity;
        }

        public static int CheckInsert(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CHKNAME",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar)
                                };
            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            parm[0].Value = dbParm.Name_CN;
            parm[1].Value = dbParm.Type;
            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_check", false, parm);

            if (hotelfacilitiesEntity.QueryResult.Tables.Count > 0 && hotelfacilitiesEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int Insert(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            if (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelfacilitiesEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(hotelfacilitiesEntity) > 0)
            {
                return 2;
            }

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();

            CommandInfo InsertLmPaymentInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("NAMECN",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar),
                                    new OracleParameter("CODE",OracleType.VarChar),
                                    new OracleParameter("SEQ",OracleType.VarChar)
                                };

            lmParm[0].Value = getMaxIDfromSeq("t_lm_b_facilities_SEQ");
            lmParm[1].Value = dbParm.Name_CN;
            lmParm[2].Value = dbParm.Type;
            lmParm[3].Value = 'P' + lmParm[0].Value.ToString().PadLeft(4, '0');
            lmParm[4].Value = dbParm.FTSeq;
            DbManager.ExecuteSql("HotelFacilities", "t_lm_b_hotel_facilities_insert", lmParm);

            return 1;
        }

        public static int CheckFtTypeInsert(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CHKNAME",OracleType.VarChar),
                                    new OracleParameter("CHKCODE",OracleType.VarChar)
                                };
            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            parm[0].Value = dbParm.FTName;
            parm[1].Value = dbParm.FTCode;
            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_type_check", false, parm);

            if (hotelfacilitiesEntity.QueryResult.Tables.Count > 0 && hotelfacilitiesEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int FtTypeInsert(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            if (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelfacilitiesEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckFtTypeInsert(hotelfacilitiesEntity) > 0)
            {
                return 2;
            }

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();

            CommandInfo InsertLmPaymentInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("NAMECN",OracleType.VarChar),
                                    new OracleParameter("CODE",OracleType.VarChar),
                                    new OracleParameter("SEQ",OracleType.VarChar)
                                };

            lmParm[0].Value = getMaxIDfromSeq("t_lm_b_facilities_type_SEQ");
            lmParm[1].Value = dbParm.FTName;
            lmParm[2].Value = dbParm.FTCode;
            lmParm[3].Value = dbParm.FTSeq;
            
            DbManager.ExecuteSql("HotelFacilities", "t_lm_b_hotel_facilities_type_insert", lmParm);

            return 1;
        }


        public static int HotelInsert(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            if (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelfacilitiesEntity.LogMessages == null)
            {
                return 0;
            }

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();

            List<CommandInfo> sqlList = new List<CommandInfo>();

            CommandInfo UpdateCsHotelInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)                         
                                };
            lmParm[0].Value = dbParm.HotelID;
            UpdateCsHotelInfo.SqlName = "HotelFacilities";
            UpdateCsHotelInfo.SqlId = "t_lm_b_hotel_service_save_update";
            UpdateCsHotelInfo.Parameters = lmParm;
            sqlList.Add(UpdateCsHotelInfo);

            foreach (string drRow in dbParm.AryFalLisT)
            {
                CommandInfo InsertCsHotelInfo = new CommandInfo();
                OracleParameter[] csParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("FACILITIESCODE",OracleType.VarChar)                              
                                };

                
                csParm[0].Value = dbParm.HotelID;
                csParm[1].Value = drRow.Trim();

                InsertCsHotelInfo.SqlName = "HotelFacilities";
                InsertCsHotelInfo.SqlId = "t_lm_b_hotel_service_save";
                InsertCsHotelInfo.Parameters = csParm;
                sqlList.Add(InsertCsHotelInfo);
            }

            //OracleParameter[] lmParm ={
            //                        new OracleParameter("ID",OracleType.Number),
            //                        new OracleParameter("NAMECN",OracleType.VarChar)                               
            //                    };

            //lmParm[0].Value = getMaxIDfromSeq("t_lm_b_facilities_hotel_SEQ");
            //lmParm[1].Value = dbParm.Name_CN;
            //DbManager.ExecuteSql("HotelFacilities", "t_lm_b_hotel_service_save", lmParm);
            DbManager.ExecuteSqlTran(sqlList);
            return 1;
        }

        public static int Update(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            if (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelfacilitiesEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckUpdate(hotelfacilitiesEntity) > 0)
            {
                return 2;
            }

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("NAMECN",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar),
                                    new OracleParameter("SEQ",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.ID;
            parm[1].Value = dbParm.Name_CN;
            parm[2].Value = dbParm.OnlineStatus;
            parm[3].Value = dbParm.Type;
            parm[4].Value = dbParm.FTSeq;
            DbManager.ExecuteSql("HotelFacilities", "t_lm_b_hotel_facilities_update", parm);
            return 1;
        }

        public static int CheckUpdate(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("CHKNAME",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar)                                
                                };

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();

            parm[0].Value = int.Parse(dbParm.ID.ToString());
            parm[1].Value = dbParm.Name_CN;
            parm[2].Value = dbParm.Type;

            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_update_check", false, parm);
            if (hotelfacilitiesEntity.QueryResult.Tables.Count > 0 && hotelfacilitiesEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int FtTypeUpdate(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            if (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelfacilitiesEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckFtTypeUpdate(hotelfacilitiesEntity) > 0)
            {
                return 2;
            }

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("NAMECN",OracleType.VarChar),
                                    new OracleParameter("CODE",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)

                                };
            parm[0].Value = dbParm.FTID;
            parm[1].Value = dbParm.FTName;
            parm[2].Value = dbParm.FTCode;
            parm[3].Value = dbParm.Status;
           
            DbManager.ExecuteSql("HotelFacilities", "t_lm_b_hotel_facilities_type_update", parm);
            return 1;
        }

        public static int FtTypeSeqListUpdate(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            if (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelfacilitiesEntity.LogMessages == null)
            {
                return 0;
            }

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();

            List<CommandInfo> sqlList = new List<CommandInfo>();

            Hashtable htList = dbParm.FTSeqList;

            foreach (System.Collections.DictionaryEntry key in htList)
            {
                CommandInfo UpdateCsTypeInfo = new CommandInfo();
                OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("SEQ",OracleType.VarChar)

                                };
                parm[0].Value = key.Key;
                parm[1].Value = key.Value;
                UpdateCsTypeInfo.SqlName = "HotelFacilities";
                UpdateCsTypeInfo.SqlId = "t_lm_b_hotel_facilities_type_seq_update";
                UpdateCsTypeInfo.Parameters = parm;
                sqlList.Add(UpdateCsTypeInfo);
            }

            DbManager.ExecuteSqlTran(sqlList);
            //DbManager.ExecuteSql("HotelFacilities", "t_lm_b_hotel_facilities_type_seq_update", parm);
            return 1;
        }

        public static int FtSeqListUpdate(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            if (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelfacilitiesEntity.LogMessages == null)
            {
                return 0;
            }

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();

            List<CommandInfo> sqlList = new List<CommandInfo>();

            Hashtable htList = dbParm.FTSeqList;

            foreach (System.Collections.DictionaryEntry key in htList)
            {
                CommandInfo UpdateCsTypeInfo = new CommandInfo();
                OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("SEQ",OracleType.VarChar)

                                };
                parm[0].Value = key.Key;
                parm[1].Value = key.Value;
                UpdateCsTypeInfo.SqlName = "HotelFacilities";
                UpdateCsTypeInfo.SqlId = "t_lm_b_hotel_facilities_seq_update";
                UpdateCsTypeInfo.Parameters = parm;
                sqlList.Add(UpdateCsTypeInfo);
            }

            DbManager.ExecuteSqlTran(sqlList);
            //DbManager.ExecuteSql("HotelFacilities", "t_lm_b_hotel_facilities_type_seq_update", parm);
            return 1;
        }

        public static int CheckFtTypeUpdate(HotelFacilitiesEntity hotelfacilitiesEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("CHKNAME",OracleType.VarChar),
                                    new OracleParameter("CHKCODE",OracleType.VarChar)                                
                                };

            HotelFacilitiesDBEntity dbParm = (hotelfacilitiesEntity.HotelFacilitiesDBEntity.Count > 0) ? hotelfacilitiesEntity.HotelFacilitiesDBEntity[0] : new HotelFacilitiesDBEntity();

            parm[0].Value = int.Parse(dbParm.FTID.ToString());
            parm[1].Value = dbParm.FTName;
            parm[2].Value = dbParm.FTCode;

            hotelfacilitiesEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelFacilities", "t_lm_b_hotel_facilities_type_update_check", false, parm);
            if (hotelfacilitiesEntity.QueryResult.Tables.Count > 0 && hotelfacilitiesEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
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