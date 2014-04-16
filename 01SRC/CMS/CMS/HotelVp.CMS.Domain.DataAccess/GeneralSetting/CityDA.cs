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
    public abstract class CityDA
    {
        public static CityEntity SelectFogToCity(CityEntity cityEntity)
        {
            //改参数
            OracleParameter[] parm ={
                                    new OracleParameter("name_zh",OracleType.VarChar),
                                    new OracleParameter("name_en",OracleType.VarChar),
                                    new OracleParameter("pinyin_short",OracleType.VarChar)
                                };
            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();

            if (String.IsNullOrEmpty(dbParm.Name_CN))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.Name_CN;
            }


            if (String.IsNullOrEmpty(dbParm.Name_EN))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.Name_EN;
            }

            if (String.IsNullOrEmpty(dbParm.PinyinS))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.PinyinS;
            }

            cityEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("City", "fog_t_city", false, parm);
            return cityEntity;
        }


        public static CityEntity CommonSelect(CityEntity cityEntity)
        {
            cityEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("City", "t_fc_city", false);
            return cityEntity;
        }

        public static CityEntity Select(CityEntity cityEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYNAME",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("StartDTime",OracleType.VarChar),
                                    new OracleParameter("EndDTime",OracleType.VarChar)
                                };
            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();

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

            cityEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("City", "t_lm_b_city", false, parm);
            return cityEntity;
        }

        public static CityEntity MainSelect(CityEntity cityEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                };
            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();
            parm[0].Value = dbParm.CityID;
            cityEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("City", "t_lm_b_city_main", false, parm);
            return cityEntity;
        }

        public static CityEntity FOGMainSelect(CityEntity cityEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                };
            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();
            parm[0].Value = dbParm.CityID;
            cityEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("City", "t_lm_b_city_main_fog", false, parm);
            return cityEntity;
        }

        public static int CheckInsert(CityEntity cityEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                };
            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();
            parm[0].Value = dbParm.CityID;
            cityEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("City", "t_lm_b_city_sigle", false, parm);

            if (cityEntity.QueryResult.Tables.Count > 0 && cityEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static DataSet CheckCity(CityEntity cityEntity)
        {
              OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                };
            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();
            parm[0].Value = dbParm.CityID;
            return  HotelVp.Common.DBUtility.DbManager.Query("City", "t_fc_city_sigle",false, parm);
        }


        public static int Insert(CityEntity cityEntity)
        {
            if (cityEntity.CityDBEntity.Count == 0)
            {
                return 0;
            }

            if (cityEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(cityEntity) > 0)
            {
                return 2;
            }

            DataSet dsCity = CheckCity(cityEntity);
            if (dsCity.Tables.Count == 0 || dsCity.Tables[0].Rows.Count == 0)
            {
                return 3;
            }

            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();

            //List<CommandInfo> sqlList = new List<CommandInfo>();
            //CommandInfo InsertLmCityInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                    //,                                    
                                    //new OracleParameter("Name_CN",OracleType.VarChar),
                                    //new OracleParameter("Name_EN",OracleType.VarChar),
                                    //new OracleParameter("Area_ID",OracleType.VarChar)     
                                };

            lmParm[0].Value = getMaxIDfromSeq("T_LM_B_CITY_SEQ");
            lmParm[1].Value = dbParm.CityID;
            //lmParm[2].Value = dsCity.Tables[0].Rows[0]["name_zh"].ToString();
            //lmParm[3].Value = dsCity.Tables[0].Rows[0]["name_en"].ToString();
            //lmParm[4].Value = dsCity.Tables[0].Rows[0]["areaid"].ToString();
            //InsertLmCityInfo.SqlName = "City";
            //InsertLmCityInfo.SqlId = "t_lm_b_city_insert";
            //InsertLmCityInfo.Parameters = lmParm;

            DbManager.ExecuteSql("City", "t_lm_b_city_insert", lmParm);

            //CommandInfo InsertCsCityInfo = new CommandInfo();

            //OracleParameter[] csParm ={
            //                        new OracleParameter("ID",OracleType.Number),
            //                        new OracleParameter("CITYID",OracleType.VarChar),                                    
            //                        new OracleParameter("ONLINESTATUS",OracleType.VarChar)                                 
            //                    };

            //csParm[0].Value = getMaxIDfromSeq("T_CS_CITY_SEQ");
            //csParm[1].Value = lmParm[0].Value;
            //csParm[2].Value = "0";
            //InsertCsCityInfo.SqlName = "City";
            //InsertCsCityInfo.SqlId = "t_cs_city_insert";
            //InsertCsCityInfo.Parameters = csParm;

            //sqlList.Add(InsertLmCityInfo);
            //sqlList.Add(InsertCsCityInfo);
            //DbManager.ExecuteSqlTran(sqlList);

            //DataCommand cmd = DataCommandManager.GetDataCommand("InsertCityList");
            //foreach (CityDBEntity dbParm in cityEntity.CityDBEntity)
            //{
            //    cmd.SetParameterValue("@ChannelID", dbParm.ChannelID);
            //    cmd.SetParameterValue("@NameCN", dbParm.Name_CN);
            //    cmd.SetParameterValue("@NameEN", PinyinHelper.GetPinyin(dbParm.Name_CN));
            //    cmd.SetParameterValue("@OnlineStatus", dbParm.OnlineStatus);
            //    cmd.SetParameterValue("@Remark", dbParm.Remark);
            //    cmd.SetParameterValue("@CreateUser", (cityEntity.LogMessages != null) ? cityEntity.LogMessages.Userid : "");
            //    cmd.SetParameterValue("@UpdateUser", (cityEntity.LogMessages != null) ? cityEntity.LogMessages.Userid : "");
            //    cmd.ExecuteNonQuery();
            //}
            
            return 1;
        }

        public static int Update(CityEntity cityEntity)
        {
            if (cityEntity.CityDBEntity.Count == 0)
            {
                return 0;
            }

            if (cityEntity.LogMessages == null)
            {
                return 0;
            }

            OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),  
                                    new OracleParameter("ELCITYID",OracleType.VarChar),  
                                    new OracleParameter("NAMECN",OracleType.VarChar),
                                    new OracleParameter("SEQ",OracleType.VarChar),
                                    new OracleParameter("PINYIN",OracleType.VarChar),
                                    new OracleParameter("PINYINSHORT",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar),
                                    new OracleParameter("ISHOT",OracleType.VarChar),
                                    new OracleParameter("SALEHOUR",OracleType.VarChar)
                                };

            CityDBEntity dbParm = (cityEntity.CityDBEntity.Count > 0) ? cityEntity.CityDBEntity[0] : new CityDBEntity();

            parm[0].Value = dbParm.CityID;
            parm[1].Value = dbParm.ElCityID;
            parm[2].Value = dbParm.Name_CN;
            parm[3].Value = dbParm.SEQ;
            parm[4].Value = dbParm.Pinyin;
            parm[5].Value = dbParm.PinyinS;
            parm[6].Value = dbParm.Longitude;
            parm[7].Value = dbParm.Latitude;
            parm[8].Value = dbParm.OnlineStatus;
            parm[9].Value = dbParm.CityType;
            parm[10].Value = dbParm.IsHot;
            parm[11].Value = dbParm.SaleHour;
            DbManager.ExecuteSql("City", "t_lm_b_city_update", parm);
            //DataCommand cmd = DataCommandManager.GetDataCommand("UpdateCityList");
            //foreach (CityDBEntity dbParm in cityEntity.CityDBEntity)
            //{
            //    cmd.SetParameterValue("@ChannelNo", dbParm.ChannelNo);
            //    cmd.SetParameterValue("@ChannelID", dbParm.ChannelID);
            //    cmd.SetParameterValue("@NameCN", dbParm.Name_CN);
            //    cmd.SetParameterValue("@NameEN", PinyinHelper.GetPinyin(dbParm.Name_CN));
            //    cmd.SetParameterValue("@OnlineStatus", dbParm.OnlineStatus);
            //    cmd.SetParameterValue("@Remark", dbParm.Remark);
            //    cmd.SetParameterValue("@UpdateUser", (cityEntity.LogMessages != null) ? cityEntity.LogMessages.Userid : "");
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