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
    public abstract class HotelInfoDA
    {
        public static HotelInfoEntity CommonHotelGroupSelect(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_common_hotelgrouplist", false);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity CommonCitySelect(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_common_citylist", false);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity CommonProvincialSelect(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_common_provinciallist", false);
            return hotelInfoEntity;
        }

        public static DataSet GetAddRoomList()
        {
            return HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_common_roomlist", true);
        }

        public static int HotelSave(HotelInfoEntity hotelInfoEntity)
        {
            if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelInfoEntity.LogMessages == null)
            {
                return 0;
            }

            string strSQLString = "";
            if (CheckInsert(hotelInfoEntity) > 0)
            {
                strSQLString = "t_lm_b_hotelinfo_update";
            }
            else
            {
                strSQLString = "t_lm_b_hotelinfo_insert";
            }

            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("HOTELNM",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("STARRATING",OracleType.VarChar),
                                    new OracleParameter("DIAMONDRATING",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("OPENDATE",OracleType.VarChar),
                                    new OracleParameter("REPAIRDATE",OracleType.VarChar),
                                    new OracleParameter("ADDRESS",OracleType.VarChar),
                                    new OracleParameter("WEBSITE",OracleType.VarChar),
                                    //new OracleParameter("PHONE",OracleType.VarChar),
                                    //new OracleParameter("FAX",OracleType.VarChar),
                                    //new OracleParameter("CONTACTPER",OracleType.VarChar),
                                    //new OracleParameter("CONTACTEMAIL",OracleType.VarChar),
                                    new OracleParameter("SIMPLEDESCZH",OracleType.VarChar),
                                    new OracleParameter("DESCZH",OracleType.VarChar),
                                    new OracleParameter("EVALUATION",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("AUTOTRUST",OracleType.VarChar),
                                    new OracleParameter("FOGSTATUS",OracleType.VarChar),
                                    new OracleParameter("HOTELNMEN",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.HotelID;
            lmParm[1].Value = dbParm.Name_CN;
            lmParm[2].Value = dbParm.HotelGroup;
            lmParm[3].Value = dbParm.StarRating;
            lmParm[4].Value = dbParm.DiamondRating;
            lmParm[5].Value = dbParm.City;
            lmParm[6].Value = dbParm.OpenDate;
            lmParm[7].Value = dbParm.RepairDate;
            if (String.IsNullOrEmpty(dbParm.AddRess))
            {
                lmParm[8].Value = DBNull.Value;
            }
            else
            {
                lmParm[8].Value = dbParm.AddRess;
            }

            if (String.IsNullOrEmpty(dbParm.WebSite))
            {
                lmParm[9].Value = DBNull.Value;
            }
            else
            {
                lmParm[9].Value = dbParm.WebSite;
            }

            //if (String.IsNullOrEmpty(dbParm.Phone))
            //{
            //    lmParm[10].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[10].Value = dbParm.Phone;
            //}

            //if (String.IsNullOrEmpty(dbParm.Fax))
            //{
            //    lmParm[11].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[11].Value = dbParm.Fax;
            //}

            //if (String.IsNullOrEmpty(dbParm.ContactPer))
            //{
            //    lmParm[12].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[12].Value = dbParm.ContactPer;
            //}

            //if (String.IsNullOrEmpty(dbParm.ContactEmail))
            //{
            //    lmParm[13].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[13].Value = dbParm.ContactEmail;
            //}

            if (String.IsNullOrEmpty(dbParm.SimpleDescZh))
            {
                lmParm[10].Value = DBNull.Value;
            }
            else
            {
                lmParm[10].Value = dbParm.SimpleDescZh;
            }

            if (String.IsNullOrEmpty(dbParm.DescZh))
            {
                lmParm[11].Value = DBNull.Value;
            }
            else
            {
                lmParm[11].Value = dbParm.DescZh;
            }

            if (String.IsNullOrEmpty(dbParm.Evaluation))
            {
                lmParm[12].Value = DBNull.Value;
            }
            else
            {
                lmParm[12].Value = dbParm.Evaluation;
            }

            if (String.IsNullOrEmpty(dbParm.Status))
            {
                lmParm[13].Value = DBNull.Value;
            }
            else
            {
                lmParm[13].Value = dbParm.Status;
            }

            lmParm[14].Value = dbParm.AutoTrust;

            lmParm[15].Value = dbParm.FogStatus;

            if (String.IsNullOrEmpty(dbParm.Name_EN))
            {
                lmParm[16].Value = DBNull.Value;
            }
            else
            {
                lmParm[16].Value = dbParm.Name_EN;
            }

            lmParm[17].Value = dbParm.Longitude;

            lmParm[18].Value = dbParm.Latitude;

            DbManager.ExecuteSql("HotelInfo", strSQLString, lmParm);

            return 1;
        }

        public static HotelInfoEntity UpdateHotelInfo(HotelInfoEntity hotelInfoEntity)
        {
            if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
            {
                hotelInfoEntity.Result = 0;
                return hotelInfoEntity;
            }

            if (hotelInfoEntity.LogMessages == null)
            {
                hotelInfoEntity.Result = 0;
                return hotelInfoEntity;
            }

            if (CheckSupCity(hotelInfoEntity) <= 0)
            {
                hotelInfoEntity.Result = 3;
                return hotelInfoEntity;
            }

            List<CommandInfo> sqlList = new List<CommandInfo>();
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            CommandInfo InsertLmbPropInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELNM",OracleType.VarChar),
                                    new OracleParameter("HOTELEN",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("STARRATING",OracleType.VarChar),
                                    new OracleParameter("DIAMONDRATING",OracleType.VarChar),
                                    new OracleParameter("ADDRESS",OracleType.VarChar),
                                    new OracleParameter("FAX",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar),
                                    new OracleParameter("CONTACTEMAIL",OracleType.VarChar),
                                    new OracleParameter("OPENDATE",OracleType.VarChar),
                                    new OracleParameter("REPAIRDATE",OracleType.VarChar),
                                    new OracleParameter("SIMPLEDESCZH",OracleType.VarChar),
                                    new OracleParameter("DESCZH",OracleType.VarChar),
                                    new OracleParameter("STATUSID",OracleType.VarChar),
                                    new OracleParameter("HOTELPN",OracleType.VarChar),
                                    new OracleParameter("TOTALROOMS",OracleType.VarChar),
                                    new OracleParameter("HOTELJP",OracleType.VarChar),
                                    new OracleParameter("ZIP",OracleType.VarChar),
                                    new OracleParameter("PRICELOW",OracleType.VarChar),
                                    new OracleParameter("CONTACTPER",OracleType.VarChar),
                                    new OracleParameter("CONTACTPHONE",OracleType.VarChar),
                                    new OracleParameter("PHONE",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.Int32),
                                    new OracleParameter("EVALUATION",OracleType.VarChar),
                                    new OracleParameter("HOTELREMARK",OracleType.VarChar),
                                    new OracleParameter("WEBSITE",OracleType.VarChar),
                                    new OracleParameter("FOGSTATUS",OracleType.VarChar),
                                    new OracleParameter("BDLONGITUDE",OracleType.VarChar),
                                    new OracleParameter("BDLATITUDE",OracleType.VarChar),
                                    new OracleParameter("GROUPCODE",OracleType.VarChar),
                                    new OracleParameter("KEYWORDS",OracleType.VarChar),
                                    new OracleParameter("ISMYHOTEL",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.Name_CN;
            lmParm[1].Value = dbParm.Name_EN;
            lmParm[2].Value = dbParm.City;
            lmParm[3].Value = dbParm.StarRating.Split('|')[0].ToString().Trim();
            lmParm[4].Value = dbParm.StarRating.Split('|')[1].ToString().Trim();
            lmParm[5].Value = dbParm.AddRess;
            lmParm[6].Value = dbParm.Fax;
            lmParm[7].Value = dbParm.Longitude;
            lmParm[8].Value = dbParm.Latitude;
            lmParm[9].Value = dbParm.ContactEmail;
            lmParm[10].Value = dbParm.OpenDate;
            lmParm[11].Value = dbParm.RepairDate;
            lmParm[12].Value = dbParm.SimpleDescZh;
            lmParm[13].Value = dbParm.DescZh;
            lmParm[14].Value = dbParm.Status;
            lmParm[15].Value = dbParm.HotelPN;
            lmParm[16].Value = dbParm.TotalRooms;
            lmParm[17].Value = dbParm.HotelJP;
            lmParm[18].Value = dbParm.Zip;
            lmParm[19].Value = dbParm.PriceLow;
            lmParm[20].Value = dbParm.ContactPer;
            lmParm[21].Value = dbParm.ContactPhone;
            lmParm[22].Value = dbParm.Phone;
            lmParm[23].Value = dbParm.HotelID;
            lmParm[24].Value = dbParm.Evaluation;
            //lmParm[25].Value = dbParm.HotelRemark;
            if (String.IsNullOrEmpty(dbParm.Remark))
            {
                lmParm[25].Value = DBNull.Value;
            }
            else
            {
                lmParm[25].Value = dbParm.Remark;
            }
           
            lmParm[26].Value = dbParm.WebSite;
            lmParm[27].Value = dbParm.FogStatus;

            lmParm[28].Value = dbParm.BDLongitude;
            lmParm[29].Value = dbParm.BDLatitude;
            lmParm[30].Value = dbParm.HotelGroup;
            lmParm[31].Value = dbParm.KeyWords;
            lmParm[32].Value = dbParm.IsMyHotel;
            //lmParm[9].Value = dbParm.Bussiness;
            //DbManager.ExecuteSql("HotelInfo", "t_lm_b_suphotelinfo_insert", lmParm);

            InsertLmbPropInfo.SqlName = "HotelInfo";
            InsertLmbPropInfo.SqlId = "t_lm_b_createhotelinfo_update";
            InsertLmbPropInfo.Parameters = lmParm;
            sqlList.Add(InsertLmbPropInfo);

            //CommandInfo InsertLmbBussInfo = new CommandInfo();
            //OracleParameter[] lmBussParm ={
            //                        new OracleParameter("HOTELID",OracleType.VarChar)
            //                    };

            //lmBussParm[0].Value = lmParm[23].Value;

            //InsertLmbBussInfo.SqlName = "HotelInfo";
            //InsertLmbBussInfo.SqlId = "t_lm_b_fvphotelbussinfo_insert";
            //InsertLmbBussInfo.Parameters = lmBussParm;
            //sqlList.Add(InsertLmbBussInfo);

            //if (!String.IsNullOrEmpty(dbParm.Bussiness))
            //{
            //    string strAreaID = "";
            //    string[] strBusList = dbParm.Bussiness.Split(',');
            //    foreach (string par in strBusList)
            //    {
            //        strAreaID = (par.IndexOf("]") >= 0) ? par.Substring((par.IndexOf('[') + 1), (par.IndexOf(']') - 1)) : "";
            //        if (String.IsNullOrEmpty(strAreaID))
            //        {
            //            continue;
            //        }
            //        CommandInfo InsertLmbAreaInfo = new CommandInfo();
            //        OracleParameter[] lmAreaParm ={
            //                        new OracleParameter("HOTELID",OracleType.VarChar),
            //                        new OracleParameter("AREAID",OracleType.VarChar)
            //                    };

            //        lmAreaParm[0].Value = lmParm[23].Value;
            //        lmAreaParm[1].Value = strAreaID;

            //        InsertLmbAreaInfo.SqlName = "HotelInfo";
            //        InsertLmbAreaInfo.SqlId = "t_lm_b_suphotelareainfo_insert";
            //        InsertLmbAreaInfo.Parameters = lmAreaParm;
            //        sqlList.Add(InsertLmbAreaInfo);
            //    }
            //}

            DbManager.ExecuteSqlTran(sqlList);

            hotelInfoEntity.Result = 1;
            hotelInfoEntity.ErrorMSG = lmParm[23].Value.ToString();
            return hotelInfoEntity;
        }

        public static HotelInfoEntity CreateHotelInfo(HotelInfoEntity hotelInfoEntity)
        {
            if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
            {
                hotelInfoEntity.Result = 0;
                return hotelInfoEntity;
            }

            if (hotelInfoEntity.LogMessages == null)
            {
                hotelInfoEntity.Result = 0;
                return hotelInfoEntity;
            }

            if (CheckSupInsert(hotelInfoEntity) > 0)
            {
                hotelInfoEntity.Result = 2;
                return hotelInfoEntity;
            }

            if (CheckSupCity(hotelInfoEntity) <= 0)
            {
                hotelInfoEntity.Result = 3;
                return hotelInfoEntity;
            }

            List<CommandInfo> sqlList = new List<CommandInfo>();
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            CommandInfo InsertLmbPropInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELNM",OracleType.VarChar),
                                    new OracleParameter("HOTELEN",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("STARRATING",OracleType.VarChar),
                                    new OracleParameter("DIAMONDRATING",OracleType.VarChar),
                                    new OracleParameter("ADDRESS",OracleType.VarChar),
                                    new OracleParameter("FAX",OracleType.VarChar),
                                     new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar),
                                    new OracleParameter("CONTACTEMAIL",OracleType.VarChar),
                                    new OracleParameter("OPENDATE",OracleType.VarChar),
                                    new OracleParameter("REPAIRDATE",OracleType.VarChar),
                                    new OracleParameter("SIMPLEDESCZH",OracleType.VarChar),
                                    new OracleParameter("DESCZH",OracleType.VarChar),
                                    new OracleParameter("STATUSID",OracleType.VarChar),
                                    new OracleParameter("HOTELPN",OracleType.VarChar),
                                    new OracleParameter("TOTALROOMS",OracleType.VarChar),
                                    new OracleParameter("HOTELJP",OracleType.VarChar),
                                    new OracleParameter("ZIP",OracleType.VarChar),
                                    new OracleParameter("PRICELOW",OracleType.VarChar),
                                    new OracleParameter("CONTACTPER",OracleType.VarChar),
                                    new OracleParameter("CONTACTPHONE",OracleType.VarChar),
                                    new OracleParameter("PHONE",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.Int32),
                                    new OracleParameter("EVALUATION",OracleType.VarChar),
                                    new OracleParameter("HOTELREMARK",OracleType.VarChar),
                                    new OracleParameter("WEBSITE",OracleType.VarChar),
                                    new OracleParameter("BDLONGITUDE",OracleType.VarChar),
                                    new OracleParameter("BDLATITUDE",OracleType.VarChar),
                                    new OracleParameter("KEYWORDS",OracleType.VarChar),
                                    new OracleParameter("HOTELGROUP",OracleType.VarChar),
                                    new OracleParameter("ISMYHOTEL",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.Name_CN;
            lmParm[1].Value = dbParm.Name_EN;
            lmParm[2].Value = dbParm.City;
            lmParm[3].Value = dbParm.StarRating.Split('|')[0].ToString().Trim();
            lmParm[4].Value = dbParm.StarRating.Split('|')[1].ToString().Trim();
            lmParm[5].Value = dbParm.AddRess;
            lmParm[6].Value = dbParm.Fax;
            lmParm[7].Value = dbParm.Longitude;
            lmParm[8].Value = dbParm.Latitude;
            lmParm[9].Value = dbParm.ContactEmail;
            lmParm[10].Value = dbParm.OpenDate;
            lmParm[11].Value = dbParm.RepairDate;
            lmParm[12].Value = dbParm.SimpleDescZh;
            lmParm[13].Value = dbParm.DescZh;
            lmParm[14].Value = dbParm.Status;
            lmParm[15].Value = dbParm.HotelPN;
            lmParm[16].Value = dbParm.TotalRooms;
            lmParm[17].Value = dbParm.HotelJP;
            lmParm[18].Value = dbParm.Zip;
            lmParm[19].Value = dbParm.PriceLow;
            lmParm[20].Value = dbParm.ContactPer;
            lmParm[21].Value = dbParm.ContactPhone;
            lmParm[22].Value = dbParm.Phone;
            lmParm[23].Value = getMaxIDfromSeq("T_LM_B_PROP_SEQ");
            lmParm[24].Value = dbParm.Evaluation;
            //lmParm[25].Value = dbParm.HotelRemark;
            if (string.IsNullOrEmpty(dbParm.HotelRemark))
            {
                lmParm[25].Value = DBNull.Value;
            }else
            {
                lmParm[25].Value = dbParm.HotelRemark;
            }
            
            lmParm[26].Value = dbParm.WebSite;
            lmParm[27].Value = dbParm.BDLongitude;
            lmParm[28].Value = dbParm.BDLatitude;
            lmParm[29].Value = dbParm.KeyWords;
            lmParm[30].Value = dbParm.HotelGroup;
            lmParm[31].Value = dbParm.IsMyHotel;
            //lmParm[9].Value = dbParm.Bussiness;
            //DbManager.ExecuteSql("HotelInfo", "t_lm_b_suphotelinfo_insert", lmParm);

            InsertLmbPropInfo.SqlName = "HotelInfo";
            InsertLmbPropInfo.SqlId = "t_lm_b_createhotelinfo_insert";
            InsertLmbPropInfo.Parameters = lmParm;
            sqlList.Add(InsertLmbPropInfo);

            CommandInfo InsertLmbBussInfo = new CommandInfo();
            OracleParameter[] lmBussParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };

            lmBussParm[0].Value = lmParm[23].Value;

            InsertLmbBussInfo.SqlName = "HotelInfo";
            InsertLmbBussInfo.SqlId = "t_lm_b_fvphotelbussinfo_insert";
            InsertLmbBussInfo.Parameters = lmBussParm;
            sqlList.Add(InsertLmbBussInfo);

            if (!String.IsNullOrEmpty(dbParm.Bussiness))
            {
                string strAreaID = "";
                string[] strBusList = dbParm.Bussiness.Split(',');
                foreach (string par in strBusList)
                {
                    strAreaID = (par.IndexOf("]") >= 0) ? par.Substring((par.IndexOf('[') + 1), (par.IndexOf(']') - 1)) : "";
                    if (String.IsNullOrEmpty(strAreaID))
                    {
                        continue;
                    }
                    CommandInfo InsertLmbAreaInfo = new CommandInfo();
                    OracleParameter[] lmAreaParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("AREAID",OracleType.VarChar)
                                };

                    lmAreaParm[0].Value = lmParm[23].Value;
                    lmAreaParm[1].Value = strAreaID;

                    InsertLmbAreaInfo.SqlName = "HotelInfo";
                    InsertLmbAreaInfo.SqlId = "t_lm_b_suphotelareainfo_insert";
                    InsertLmbAreaInfo.Parameters = lmAreaParm;
                    sqlList.Add(InsertLmbAreaInfo);
                }
            }

            DbManager.ExecuteSqlTran(sqlList);

            hotelInfoEntity.Result = 1;
            hotelInfoEntity.ErrorMSG = lmParm[23].Value.ToString();
            return hotelInfoEntity;
        }

        public static HotelInfoEntity SupHotelSave(HotelInfoEntity hotelInfoEntity)
        {
            if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
            {
                hotelInfoEntity.Result = 0;
                return hotelInfoEntity;
            }

            if (hotelInfoEntity.LogMessages == null)
            {
                hotelInfoEntity.Result = 0;
                return hotelInfoEntity;
            }

            if (CheckSupInsert(hotelInfoEntity) > 0)
            {
                hotelInfoEntity.Result = 2;
                return hotelInfoEntity;
            }

            if (CheckSupCity(hotelInfoEntity) <= 0)
            {
                hotelInfoEntity.Result = 3;
                return hotelInfoEntity;
            }

            List<CommandInfo> sqlList = new List<CommandInfo>();
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            CommandInfo InsertLmbPropInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELNM",OracleType.VarChar),
                                    new OracleParameter("HOTELEN",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("STARRATING",OracleType.VarChar),
                                    new OracleParameter("DIAMONDRATING",OracleType.VarChar),
                                    new OracleParameter("ADDRESS",OracleType.VarChar),
                                    new OracleParameter("FAX",OracleType.VarChar),
                                     new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar),
                                    new OracleParameter("CONTACTEMAIL",OracleType.VarChar),
                                    new OracleParameter("OPENDATE",OracleType.VarChar),
                                    new OracleParameter("REPAIRDATE",OracleType.VarChar),
                                    new OracleParameter("SIMPLEDESCZH",OracleType.VarChar),
                                    new OracleParameter("DESCZH",OracleType.VarChar),
                                    new OracleParameter("STATUSID",OracleType.VarChar),
                                    new OracleParameter("HOTELPN",OracleType.VarChar),
                                    new OracleParameter("TOTALROOMS",OracleType.VarChar),
                                    new OracleParameter("HOTELJP",OracleType.VarChar),
                                    new OracleParameter("ZIP",OracleType.VarChar),
                                    new OracleParameter("PRICELOW",OracleType.VarChar),
                                    new OracleParameter("CONTACTPER",OracleType.VarChar),
                                    new OracleParameter("CONTACTPHONE",OracleType.VarChar),
                                    new OracleParameter("PHONE",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.Int32)
                                };

            lmParm[0].Value = dbParm.Name_CN;
            lmParm[1].Value = dbParm.Name_EN;
            lmParm[2].Value = dbParm.City;
            lmParm[3].Value = dbParm.StarRating.Split('|')[0].ToString().Trim();
            lmParm[4].Value = dbParm.StarRating.Split('|')[1].ToString().Trim();
            lmParm[5].Value = dbParm.AddRess;
            lmParm[6].Value = dbParm.Fax;
            lmParm[7].Value = dbParm.Longitude;
            lmParm[8].Value = dbParm.Latitude;
            lmParm[9].Value = dbParm.ContactEmail;
            lmParm[10].Value = dbParm.OpenDate;
            lmParm[11].Value = dbParm.RepairDate;
            lmParm[12].Value = dbParm.SimpleDescZh;
            lmParm[13].Value = dbParm.DescZh;
            lmParm[14].Value = dbParm.Status;
            lmParm[15].Value = dbParm.HotelPN;
            lmParm[16].Value = dbParm.TotalRooms;
            lmParm[17].Value = dbParm.HotelJP;
            lmParm[18].Value = dbParm.Zip;
            lmParm[19].Value = dbParm.PriceLow;
            lmParm[20].Value = dbParm.ContactPer;
            lmParm[21].Value = dbParm.ContactPhone;
            lmParm[22].Value = dbParm.Phone;
            lmParm[23].Value = getMaxIDfromSeq("T_LM_B_PROP_SEQ");

            //lmParm[9].Value = dbParm.Bussiness;
            //DbManager.ExecuteSql("HotelInfo", "t_lm_b_suphotelinfo_insert", lmParm);

            InsertLmbPropInfo.SqlName = "HotelInfo";
            InsertLmbPropInfo.SqlId = "t_lm_b_suphotelinfo_insert";
            InsertLmbPropInfo.Parameters = lmParm;
            sqlList.Add(InsertLmbPropInfo);

            if (!String.IsNullOrEmpty(dbParm.Bussiness))
            {
                string strAreaID = "";
                string[] strBusList = dbParm.Bussiness.Split(',');
                foreach (string par in strBusList)
                {
                    strAreaID = (par.IndexOf("]") >= 0) ? par.Substring((par.IndexOf('[') + 1), (par.IndexOf(']') - 1)) : "";
                    if (String.IsNullOrEmpty(strAreaID))
                    {
                        continue;
                    }
                    CommandInfo InsertLmbAreaInfo = new CommandInfo();
                    OracleParameter[] lmAreaParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("AREAID",OracleType.VarChar)
                                };

                    lmAreaParm[0].Value = lmParm[23].Value;
                    lmAreaParm[1].Value = strAreaID;

                    InsertLmbAreaInfo.SqlName = "HotelInfo";
                    InsertLmbAreaInfo.SqlId = "t_lm_b_suphotelareainfo_insert";
                    InsertLmbAreaInfo.Parameters = lmAreaParm;
                    sqlList.Add(InsertLmbAreaInfo);
                }
            }

            DbManager.ExecuteSqlTran(sqlList);

            hotelInfoEntity.Result = 1;
            hotelInfoEntity.ErrorMSG = lmParm[23].Value.ToString();
            return hotelInfoEntity;
        }

        public static HotelInfoEntity BindHotelList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelinfo_bind", false, parm);
            return hotelInfoEntity;
        }

        public static int UpdateHotelSalesUserAccount(string HotelID, string UserAccount)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("USERCODE",OracleType.VarChar)
                                };
            parm[0].Value = HotelID;
            parm[1].Value = UserAccount;
            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("HotelInfo", "t_lm_b_hotelinfo_sales_update", parm);
        }

        public static HotelInfoEntity ChkLMPropHotelList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelinfo_prop_chk", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity ChkLMPropHotelExam(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelinfo_prop_exam_chk", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity ReadFogHotelInfo(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelinfo_fog", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetShwoHisPlanInfoList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("EffectDate",OracleType.VarChar),
                                    new OracleParameter("PriceCode",OracleType.VarChar),
                                    new OracleParameter("RoomCode",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.EffectDate;
            parm[2].Value = dbParm.PriceCode;
            parm[3].Value = dbParm.RoomCode;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_his_plan_list", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetHotelPlanInfoList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("EffectDate",OracleType.VarChar),
                                    new OracleParameter("CityID",OracleType.VarChar),
                                    new OracleParameter("AreaID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.EffectDate;
            parm[2].Value = dbParm.City;
            parm[3].Value = dbParm.AreaID;

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("HotelInfo", "GetHotelPlanInfoList");
            DataSet dsResult = DbManager.Query(SqlString, parm, (hotelInfoEntity.PageCurrent - 1) * hotelInfoEntity.PageSize, hotelInfoEntity.PageSize, false);
            DataSet dsRemark = new DataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    dsResult.Tables[0].Rows[i]["STAR"] = SetStarValue(dsResult.Tables[0].Rows[i]["diamond_rating"].ToString(), dsResult.Tables[0].Rows[i]["star_rating"].ToString());
                    dsResult.Tables[0].Rows[i]["EFFDT"] = SetEfHourValue(dsResult.Tables[0].Rows[i]["effect_hour"].ToString());
                    dsRemark = SetRemarkValue(dsResult.Tables[0].Rows[i]["HOTELID"].ToString());
                    if (dsRemark.Tables.Count > 0 && dsRemark.Tables[0].Rows.Count > 0)
                    {
                        dsResult.Tables[0].Rows[i]["ROOMCL"] = dsRemark.Tables[0].Rows[0]["Create_User"].ToString();
                        dsResult.Tables[0].Rows[i]["REMARK"] = dsRemark.Tables[0].Rows[0]["Remark"].ToString();
                    }
                }
            }

            hotelInfoEntity.QueryResult = dsResult;
            return hotelInfoEntity;
        }

        private static string SetStarValue(string DiaHotel, string Star)
        {
            string val = string.Empty;
            if ("1".Equals(DiaHotel))
            {
                val = "准";
            }

            if (!chkNum(Star) || (chkNum(Star) && int.Parse(Star) < 3))
            {
                val = "经济";
            }
            else
            {
                val = val + Star;
            }

            return val;
        }
        private static bool chkNum(string parm)
        {
            try
            {
                int.Parse(parm);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static string SetEfHourValue(string EfHour)
        {
            string val = string.Empty;
            int iIndex = EfHour.LastIndexOf("0");
            if (iIndex == 11)
            {
                val = "12点";
            }
            else if (iIndex == 13)
            {
                val = "14点";
            }
            else if (iIndex == 15)
            {
                val = "16点";
            }
            else if (iIndex == 17)
            {
                val = "18点";
            }
            return val;
        }

        private static DataSet SetRemarkValue(string hotelID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetConsultRoomHistoryByHotelID");
            cmd.SetParameterValue("@HOTELID", hotelID);
            return cmd.ExecuteDataSet();
        }
        public static HotelInfoEntity GetHotelPlanInfoCount(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("EffectDate",OracleType.VarChar),
                                    new OracleParameter("CityID",OracleType.VarChar),
                                    new OracleParameter("AreaID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.EffectDate;
            parm[2].Value = dbParm.City;
            parm[3].Value = dbParm.AreaID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "GetHotelPlanInfoCount", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity CountHotelOnlineLb(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar),
                                    new OracleParameter("EffectDate",OracleType.VarChar),
                                    new OracleParameter("CityID",OracleType.VarChar),
                                    new OracleParameter("AreaID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.EffectDate;
            parm[2].Value = dbParm.City;
            parm[3].Value = dbParm.AreaID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "CountHotelOnlineLb", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity SelectHotelInfoEX(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HotelID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_hotel_ex_type_byhotelid", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetSalesManager(HotelInfoEntity hotelInfoEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerHistory");
            cmd.SetParameterValue("@HotelID", hotelInfoEntity.HotelInfoDBEntity[0].HotelID);
            hotelInfoEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelInfoEntity;
        }

        //public static HotelInfoEntity SelectTypeDetail(HotelInfoEntity hotelInfoEntity)
        //{
        //    OracleParameter[] parm ={
        //                            new OracleParameter("ID",OracleType.VarChar)
        //                        };
        //    HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
        //    parm[0].Value = dbParm.ID;

        //    hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotel_facilities_detal", parm);
        //    return hotelInfoEntity;
        //}


        //public static HotelInfoEntity ServiceTypeSelect(HotelInfoEntity hotelInfoEntity)
        //{
        //    hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotel_service");
        //    return hotelInfoEntity;
        //}



        //public static HotelInfoEntity FacilitiesTypeSelect(HotelInfoEntity hotelInfoEntity)
        //{
        //    hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotel_facilities");
        //    return hotelInfoEntity;
        //}

        public static int CheckInsert(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelinfo_insertcheck", false, parm);

            if (hotelInfoEntity.QueryResult.Tables.Count > 0 && hotelInfoEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckSupInsert(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELNM",OracleType.VarChar),
                                    new OracleParameter("TEL",OracleType.VarChar),
                                    new OracleParameter("FAX",OracleType.VarChar),
                                    new OracleParameter("ADDRESS",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.Name_CN;
            parm[1].Value = dbParm.Phone;
            parm[2].Value = dbParm.Fax;
            parm[3].Value = dbParm.AddRess;
            parm[4].Value = dbParm.Longitude;
            parm[5].Value = dbParm.Latitude;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelinfo_supinsertcheck", false, parm);

            if (hotelInfoEntity.QueryResult.Tables.Count > 0 && hotelInfoEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckSupCity(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.City;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelinfo_supcitycheck", false, parm);

            if (hotelInfoEntity.QueryResult.Tables.Count > 0 && hotelInfoEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        //public static int Insert(HotelInfoEntity hotelInfoEntity)
        //{
        //    if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
        //    {
        //        return 0;
        //    }

        //    if (hotelInfoEntity.LogMessages == null)
        //    {
        //        return 0;
        //    }

        //    if (CheckInsert(hotelInfoEntity) > 0)
        //    {
        //        return 2;
        //    }

        //    HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

        //    CommandInfo InsertLmPaymentInfo = new CommandInfo();
        //    OracleParameter[] lmParm ={
        //                            new OracleParameter("ID",OracleType.Number),
        //                            new OracleParameter("NAMECN",OracleType.VarChar),
        //                            new OracleParameter("TYPE",OracleType.VarChar),
        //                            new OracleParameter("CODE",OracleType.VarChar),
        //                        };

        //    lmParm[0].Value = getMaxIDfromSeq("t_lm_b_facilities_SEQ");
        //    lmParm[1].Value = dbParm.Name_CN;
        //    if (dbParm.Type.Equals("0"))
        //    {
        //        lmParm[2].Value = "S";
        //    }
        //    else
        //    {
        //        lmParm[2].Value = "F";
        //    }
        //    lmParm[3].Value = 'P' + lmParm[0].Value.ToString().PadLeft(4, '0');
        //    DbManager.ExecuteSql("HotelInfo", "t_lm_b_hotel_facilities_insert", lmParm);

        //    return 1;
        //}


        //public static int HotelInsert(HotelInfoEntity hotelInfoEntity)
        //{
        //    if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
        //    {
        //        return 0;
        //    }

        //    if (hotelInfoEntity.LogMessages == null)
        //    {
        //        return 0;
        //    }

        //    HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

        //    List<CommandInfo> sqlList = new List<CommandInfo>();

        //    CommandInfo UpdateCsHotelInfo = new CommandInfo();
        //    OracleParameter[] lmParm ={
        //                            new OracleParameter("HOTELID",OracleType.VarChar)                         
        //                        };
        //    lmParm[0].Value = dbParm.HotelID;
        //    UpdateCsHotelInfo.SqlName = "HotelInfo";
        //    UpdateCsHotelInfo.SqlId = "t_lm_b_hotel_service_save_update";
        //    UpdateCsHotelInfo.Parameters = lmParm;
        //    sqlList.Add(UpdateCsHotelInfo);



        //    //OracleParameter[] lmParm ={
        //    //                        new OracleParameter("ID",OracleType.Number),
        //    //                        new OracleParameter("NAMECN",OracleType.VarChar)                               
        //    //                    };

        //    //lmParm[0].Value = getMaxIDfromSeq("t_lm_b_facilities_hotel_SEQ");
        //    //lmParm[1].Value = dbParm.Name_CN;
        //    //DbManager.ExecuteSql("HotelInfo", "t_lm_b_hotel_service_save", lmParm);
        //    DbManager.ExecuteSqlTran(sqlList);
        //    return 1;
        //}

        //public static int Update(HotelInfoEntity hotelInfoEntity)
        //{
        //    if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
        //    {
        //        return 0;
        //    }

        //    if (hotelInfoEntity.LogMessages == null)
        //    {
        //        return 0;
        //    }

        //    if (CheckUpdate(hotelInfoEntity) > 0)
        //    {
        //        return 2;
        //    }

        //    HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
        //    OracleParameter[] parm ={
        //                            new OracleParameter("ID",OracleType.Number),
        //                            new OracleParameter("NAMECN",OracleType.VarChar),
        //                            new OracleParameter("ONLINESTATUS",OracleType.VarChar)

        //                        };
        //    parm[0].Value = dbParm.ID;
        //    parm[1].Value = dbParm.Name_CN;
        //    parm[2].Value = dbParm.Status;

        //    DbManager.ExecuteSql("HotelInfo", "t_lm_b_hotel_facilities_update", parm);
        //    return 1;
        //}

        //public static int CheckUpdate(HotelInfoEntity hotelInfoEntity)
        //{
        //    OracleParameter[] parm ={
        //                            new OracleParameter("ID",OracleType.Number),
        //                            new OracleParameter("CHKNAME",OracleType.VarChar),
        //                            new OracleParameter("TYPE",OracleType.VarChar)                                
        //                        };

        //    HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

        //    parm[0].Value = int.Parse(dbParm.ID.ToString());
        //    parm[1].Value = dbParm.Name_CN;
        //   if (dbParm.Type.Equals("0"))
        //    {
        //        parm[2].Value = "S";
        //    }
        //    else
        //    {
        //        parm[2].Value = "F";
        //    }

        //    hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotel_facilities_update_check", parm);
        //    if (hotelInfoEntity.QueryResult.Tables.Count > 0 && hotelInfoEntity.QueryResult.Tables[0].Rows.Count > 0)
        //    {
        //        return 1;
        //    }
        //    return 0;
        //}

        public static int UpdateHotelSalesList(HotelInfoEntity hotelInfoEntity)
        {
            if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelInfoEntity.LogMessages == null)
            {
                return 0;
            }

            if (!CheckUpdate(hotelInfoEntity))
            {
                return 2;
            }

            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] lmParm ={
                                   new OracleParameter("PHONE",OracleType.VarChar),
                                    new OracleParameter("FAX",OracleType.VarChar),
                                    new OracleParameter("CONTACTPER",OracleType.VarChar),
                                    new OracleParameter("CONTACTEMAIL",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                    //,
                                    //new OracleParameter("USERCODE",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.Phone))
            {
                lmParm[0].Value = DBNull.Value;
            }
            else
            {
                lmParm[0].Value = dbParm.Phone;
            }

            if (String.IsNullOrEmpty(dbParm.Fax))
            {
                lmParm[1].Value = DBNull.Value;
            }
            else
            {
                lmParm[1].Value = dbParm.Fax;
            }

            if (String.IsNullOrEmpty(dbParm.ContactPer))
            {
                lmParm[2].Value = DBNull.Value;
            }
            else
            {
                lmParm[2].Value = dbParm.ContactPer;
            }

            if (String.IsNullOrEmpty(dbParm.ContactEmail))
            {
                lmParm[3].Value = DBNull.Value;
            }
            else
            {
                lmParm[3].Value = dbParm.ContactEmail;
            }

            lmParm[4].Value = dbParm.HotelID;

            //if (String.IsNullOrEmpty(dbParm.SalesID))
            //{
            //    lmParm[5].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[5].Value = dbParm.SalesID;
            //}
            DbManager.ExecuteSql("HotelInfo", "t_lm_b_hotel_sales_update", lmParm);
            InsertSalesAndHistory(hotelInfoEntity);
            UpdateLMSalesHistory(hotelInfoEntity);
            SaveRoomRatherList(hotelInfoEntity);
            return 1;
        }

        public static HotelInfoEntity SaveRoomRatherList(HotelInfoEntity hotelInfoEntity)
        {
            if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
            {
                return hotelInfoEntity;
            }

            if (hotelInfoEntity.LogMessages == null)
            {
                return hotelInfoEntity;
            }

            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            List<CommandInfo> cmdList = new List<CommandInfo>();
            string strSql = XmlSqlAnalyze.GotSqlTextFromXml("HotelInfo", "t_room_rather_list_save");

            DataTable dtResult = dbParm.RoomRather;
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                CommandInfo cminfo = new CommandInfo();
                cminfo.CommandText = strSql;
                OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCODE",OracleType.VarChar),
                                    new OracleParameter("DTYPE",OracleType.VarChar),
                                    new OracleParameter("DVALE",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar)
                                };

                parm[0].Value = dbParm.HotelID;
                parm[1].Value = dtResult.Rows[i]["ROOMCD"].ToString();
                parm[2].Value = dtResult.Rows[i]["DISCOUNTCD"].ToString();
                parm[3].Value = dtResult.Rows[i]["DISCOUNTXT"].ToString();
                parm[4].Value = hotelInfoEntity.LogMessages.Username;
                cminfo.Parameters = parm;
                cmdList.Add(cminfo);
            }

            DbHelperOra.ExecuteSqlTran(cmdList);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity SaveHotelRoomsList(HotelInfoEntity hotelInfoEntity)
        {
            if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
            {
                hotelInfoEntity.Result = 0;
                return hotelInfoEntity;
            }

            if (hotelInfoEntity.LogMessages == null)
            {
                hotelInfoEntity.Result = 0;
                return hotelInfoEntity;
            }

            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if ("1".Equals(dbParm.RoomACT) && !CheckRoomSave(hotelInfoEntity))
            {
                hotelInfoEntity.Result = 2;
                return hotelInfoEntity;
            }

            if (!CheckBedTypeSave(hotelInfoEntity))
            {
                hotelInfoEntity.Result = 3;
                return hotelInfoEntity;
            }

            if (!"1".Equals(dbParm.RoomACT) && CheckRoomNameCG(hotelInfoEntity))
            {
                hotelInfoEntity.HotelInfoDBEntity[0].RoomNMCG = "1";
            }

            string strSql = ("1".Equals(dbParm.RoomACT)) ? "t_room_list_insert" : "t_room_list_update";
            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCODE",OracleType.VarChar),
                                    new OracleParameter("ROOMNAME",OracleType.VarChar),
                                    new OracleParameter("BEDTYPE",OracleType.VarChar),
                                    new OracleParameter("MAXGUEST",OracleType.VarChar),
                                    new OracleParameter("ISWINDOW",OracleType.VarChar),
                                    new OracleParameter("GUESTTYPE",OracleType.VarChar),
                                    new OracleParameter("RNETWORK",OracleType.VarChar),
                                    new OracleParameter("ROOMAREA",OracleType.VarChar),
                                    new OracleParameter("ISNOSMOKE",OracleType.VarChar),
                                    new OracleParameter("RSTATUS",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.HotelID;
            lmParm[1].Value = dbParm.RoomCode;
            lmParm[2].Value = dbParm.RoomNM;
            lmParm[3].Value = dbParm.BedType;

            if (String.IsNullOrEmpty(dbParm.RoomPer))
            {
                lmParm[4].Value = DBNull.Value;
            }
            else
            {
                lmParm[4].Value = dbParm.RoomPer;
            }

            lmParm[5].Value = dbParm.RoomWindow;
            lmParm[6].Value = dbParm.GuesType;
            lmParm[7].Value = dbParm.RoomWlan;
            lmParm[8].Value = dbParm.RoomArea;
            lmParm[9].Value = dbParm.RoomSmoke;
            lmParm[10].Value = dbParm.RoomStatus;
            lmParm[11].Value = hotelInfoEntity.LogMessages.Username;

            DbManager.ExecuteSql("HotelInfo", strSql, lmParm);
            InsertRoomSaveHistory(hotelInfoEntity);
            hotelInfoEntity.Result = 1;
            return hotelInfoEntity;
        }

        public static int InsertRoomSaveHistory(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertRoomSaveHistory");
            cmd.SetParameterValue("@HOTELID", dbParm.HotelID);
            cmd.SetParameterValue("@ROOMCODE", dbParm.RoomCode);
            cmd.SetParameterValue("@ROOMNAME", dbParm.RoomNM);
            cmd.SetParameterValue("@BEDTYPE", dbParm.BedType);
            cmd.SetParameterValue("@MAXGUEST", dbParm.RoomPer);
            cmd.SetParameterValue("@ISWINDOW", dbParm.RoomWindow);
            cmd.SetParameterValue("@GUESTTYPE", dbParm.GuesType);
            cmd.SetParameterValue("@RNETWORK", dbParm.RoomWlan);
            cmd.SetParameterValue("@ROOMAREA", dbParm.RoomArea);
            cmd.SetParameterValue("@ISNOSMOKE", dbParm.RoomSmoke);
            cmd.SetParameterValue("@RSTATUS", dbParm.RoomStatus);
            cmd.SetParameterValue("@CREATEUSER", hotelInfoEntity.LogMessages.Username);
            return cmd.ExecuteNonQuery();
        }

        public static HotelInfoEntity GetSalesManagerSettingHistory(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerSettingHistory");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            hotelInfoEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetHotelRoomHistoryList(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetHotelRoomHistoryList");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@RoomCode", dbParm.RoomCode);
            hotelInfoEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetHotelRoomList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_room_list_select", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetHotelRatherList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_room_rather_list_select", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetHotelPrRoomList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_price_code_list_select", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity BindUpdateRoomData(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;
            dlmParm[1].Value = dbParm.RoomCode;

            hotelInfoEntity.QueryResult = DbManager.Query("HotelInfo", "t_room_inser_check", false, dlmParm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity BindUpdatePrRoomData(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("PRICECD",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;
            dlmParm[1].Value = dbParm.PriceCode;

            hotelInfoEntity.QueryResult = DbManager.Query("HotelInfo", "t_price_room_list", false, dlmParm);
            return hotelInfoEntity;
        }

        public static int InsertSalesAndHistory(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertSalesAndHistory");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@UserCode", dbParm.SalesID);
            cmd.SetParameterValue("@StartDTime", dbParm.SalesStartDT);
            cmd.SetParameterValue("@EndDTime", dbParm.SalesEndDT);
            cmd.SetParameterValue("@CreateUser", hotelInfoEntity.LogMessages.Username);
            cmd.SetParameterValue("@Fax", dbParm.Fax);
            cmd.SetParameterValue("@Per", dbParm.ContactPer);
            cmd.SetParameterValue("@Tel", dbParm.Phone);
            cmd.SetParameterValue("@Email", dbParm.ContactEmail);
            return cmd.ExecuteNonQuery(); ;
        }

        public static int InsertBalanceHistory(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            string[] RoomList = dbParm.HRoomList.Split(',');

            DateTime InDateFrom = DateTime.Parse(dbParm.InDateFrom);
            DateTime InDateTo = DateTime.Parse(dbParm.InDateTo);
            //dbParm.InDateFrom  dbParm.InDateTo
            while (InDateFrom <= InDateTo)
            {
                foreach (string room in RoomList)
                {
                    if (String.IsNullOrEmpty(room.Trim()))
                    {
                        continue;
                    }

                    DataCommand cmd = DataCommandManager.GetDataCommand("InsertBalanceHistory");
                    cmd.SetParameterValue("@HotelID", dbParm.HotelID);
                    cmd.SetParameterValue("@PriceCode", dbParm.PriceCode);
                    cmd.SetParameterValue("@RoomCode", room);
                    cmd.SetParameterValue("@InDate", InDateFrom.ToShortDateString());
                    cmd.SetParameterValue("@BalType", dbParm.BalType);
                    cmd.SetParameterValue("@BalValue", dbParm.BalValue);
                    cmd.SetParameterValue("@CreateUser", hotelInfoEntity.LogMessages.Username);
                    cmd.ExecuteNonQuery();
                }

                InDateFrom = InDateFrom.AddDays(1);
            }
            return 1;
        }

        public static int UpdateLMSalesHistory(HotelInfoEntity hotelInfoEntity)
        {
            if (hotelInfoEntity.HotelInfoDBEntity.Count == 0)
            {
                return 0;
            }

            if (hotelInfoEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckHistoryUpdate(hotelInfoEntity))
            {
                DeleteLMSalesHistory(hotelInfoEntity);
            }

            InsertLMSalesHistory(hotelInfoEntity);
            return 1;
        }

        public static int InsertLMSalesHistory(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            lmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                lmParm[1].Value = DBNull.Value;
            }
            else
            {
                lmParm[1].Value = dbParm.SalesID;
            }

            DbManager.ExecuteSql("HotelInfo", "t_lm_sales_history_save", lmParm);
            return 1;
        }

        public static int DeleteLMSalesHistory(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                dlmParm[1].Value = DBNull.Value;
            }
            else
            {
                dlmParm[1].Value = dbParm.SalesID;
            }

            DbManager.ExecuteSql("HotelInfo", "t_lm_sales_history_delete", dlmParm);
            return 1;
        }

        public static bool CheckUpdate(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CheckUpdateSalesID");
            cmd.SetParameterValue("@UserCode", dbParm.SalesID);
            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SalesRoleID"])) ? "5" : ConfigurationManager.AppSettings["SalesRoleID"].ToString().Trim();
            cmd.SetParameterValue("@RoleID", RoleID);
            DataSet dsResult = cmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckRoomSave(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;
            dlmParm[1].Value = dbParm.RoomCode;

            DataSet dsResult = DbManager.Query("HotelInfo", "t_room_inser_check", false, dlmParm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public static bool CheckBedTypeSave(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("BEDCD",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.BedType;

            DataSet dsResult = DbManager.Query("HotelInfo", "t_room_bed_save_check", false, dlmParm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckRoomNameCG(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;
            dlmParm[1].Value = dbParm.RoomCode;

            DataSet dsResult = DbManager.Query("HotelInfo", "t_room_inser_check", false, dlmParm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !dsResult.Tables[0].Rows[0]["ROOMNM"].ToString().Equals(dbParm.RoomNM))
            {
                return true;
            }

            return false;
        }

        public static bool chkInserBed(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("BEDCD",OracleType.VarChar),
                                    new OracleParameter("BEDNM",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.BedCode;
            dlmParm[1].Value = dbParm.BedName;

            DataSet dsResult = DbManager.Query("HotelInfo", "t_room_bed_inser_check", false, dlmParm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public static bool CheckHistoryUpdate(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                dlmParm[1].Value = DBNull.Value;
            }
            else
            {
                dlmParm[1].Value = dbParm.SalesID;
            }
            DataSet dsResult = DbManager.Query("HotelInfo", "t_lm_sales_history_select", false, dlmParm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static HotelInfoEntity GetBalanceRoomHistory(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar),
                                    new OracleParameter("STARTDT",OracleType.VarChar),
                                    new OracleParameter("ENDDT",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.HRoomList))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HRoomList;
            }

            if (String.IsNullOrEmpty(dbParm.InDateFrom))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.InDateFrom;
            }

            if (String.IsNullOrEmpty(dbParm.InDateTo))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.InDateTo;
            }

            DataSet dsResult = new DataSet();
            DataSet dsRoomList = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_roomlist", true, parm);
            DataSet dsDataList = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_select", true, parm);
            int iLmCount = 0;
            string Cols = string.Empty;
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("EFFECTDT");
            foreach (DataRow drCol in dsRoomList.Tables[0].Rows)
            {
                if ("LMBAR".Equals(drCol["rate_code"].ToString().ToUpper()))
                {
                    iLmCount = iLmCount + 1;
                }

                dsResult.Tables[0].Columns.Add(drCol["rate_code"].ToString().ToUpper() + "-" + drCol["room_type_code"].ToString().ToUpper());
                Cols = Cols + GetColsNameByRoomTypeCode(dbParm.HotelID, drCol["room_type_code"].ToString()) + ",";
            }

            string strDate = string.Empty;
            foreach (DataRow drVal in dsDataList.Tables[0].Rows)
            {
                if (!strDate.Equals(drVal["EFFECTDATE"].ToString()))
                {
                    strDate = drVal["EFFECTDATE"].ToString();
                    DataRow[] drList = dsDataList.Tables[0].Select("EFFECTDATE='" + strDate + "'");

                    if (drList.Count() == 0)
                    {
                        continue;
                    }

                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["EFFECTDT"] = strDate;
                    string strColNM = string.Empty;
                    foreach (DataRow drTemp in drList)
                    {
                        strColNM = drTemp["rate_code"].ToString().ToUpper() + "-" + drTemp["room_type_code"].ToString().ToUpper();
                        drRow[strColNM] = ("42".Equals(drTemp["commision_mode"].ToString())) ? drTemp["commision"].ToString() + "%" : drTemp["commision"].ToString() + "元";
                    }
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }

            dsResult.Tables[0].Columns["EFFECTDT"].ColumnName = "日期/房型";
            hotelInfoEntity.LMCount = iLmCount;
            hotelInfoEntity.LM2Count = dsResult.Tables[0].Columns.Count - iLmCount - 1;
            hotelInfoEntity.Cols = Cols.Trim(',');
            hotelInfoEntity.QueryResult = dsResult;
            return hotelInfoEntity;
        }

        public static string GetColsNameByRoomTypeCode(string HotelID, string room_type_code)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar)
                                };
            parm[0].Value = HotelID;
            parm[1].Value = room_type_code;

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balanceroom_roomnm", true, parm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["name_zh"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static HotelInfoEntity ExportBalanceRoomHistory(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar),
                                    new OracleParameter("STARTDT",OracleType.VarChar),
                                    new OracleParameter("ENDDT",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.HRoomList))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HRoomList;
            }

            if (String.IsNullOrEmpty(dbParm.InDateFrom))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.InDateFrom;
            }

            if (String.IsNullOrEmpty(dbParm.InDateTo))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.InDateTo;
            }

            DataSet dsResult = new DataSet();
            DataSet dsRoomList = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_roomlist", true, parm);
            DataSet dsDataList = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_select", true, parm);

            System.Collections.Hashtable htRoomNm = new System.Collections.Hashtable();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("EFFECTDT");
            foreach (DataRow drCol in dsRoomList.Tables[0].Rows)
            {
                dsResult.Tables[0].Columns.Add(drCol["rate_code"].ToString().ToUpper() + "-" + drCol["room_type_code"].ToString().ToUpper());

                if (!htRoomNm.ContainsKey(drCol["rate_code"].ToString().ToUpper() + "-" + drCol["room_type_code"].ToString().ToUpper()))
                {
                    htRoomNm.Add(drCol["rate_code"].ToString().ToUpper() + "-" + drCol["room_type_code"].ToString().ToUpper(), GetColsNameByRoomTypeCode(dbParm.HotelID, drCol["room_type_code"].ToString()));
                }
            }

            string strDate = string.Empty;
            foreach (DataRow drVal in dsDataList.Tables[0].Rows)
            {
                if (!strDate.Equals(drVal["EFFECTDATE"].ToString()))
                {
                    strDate = drVal["EFFECTDATE"].ToString();
                    DataRow[] drList = dsDataList.Tables[0].Select("EFFECTDATE='" + strDate + "'");

                    if (drList.Count() == 0)
                    {
                        continue;
                    }

                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["EFFECTDT"] = strDate;
                    string strColNM = string.Empty;
                    foreach (DataRow drTemp in drList)
                    {
                        strColNM = drTemp["rate_code"].ToString().ToUpper() + "-" + drTemp["room_type_code"].ToString().ToUpper();
                        drRow[strColNM] = ("42".Equals(drTemp["commision_mode"].ToString())) ? drTemp["commision"].ToString() + "%" : drTemp["commision"].ToString() + "元";
                    }
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }

            dsResult.Tables[0].Columns["EFFECTDT"].ColumnName = "日期/房型";

            for (int i = 1; i < dsResult.Tables[0].Columns.Count; i++)
            {
                dsResult.Tables[0].Columns[i].ColumnName = dsResult.Tables[0].Columns[i].ColumnName.Substring(0, dsResult.Tables[0].Columns[i].ColumnName.IndexOf('-') + 1) + htRoomNm[dsResult.Tables[0].Columns[i].ColumnName].ToString();
            }

            hotelInfoEntity.QueryResult = dsResult;
            return hotelInfoEntity;
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

        public static HotelInfoEntity GetTagInfoAERA(HotelInfoEntity hotelInfoEntity)
        {

            OracleParameter[] parm ={
                                    new OracleParameter("HVPHOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hvp_area_select", false, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity BedTypeListSelect(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("KEYWORD",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (!String.IsNullOrEmpty(dbParm.KeyWord))
            {
                parm[0].Value = dbParm.KeyWord;
            }
            else
            {
                parm[0].Value = DBNull.Value;
            }

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_room_bed_select", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                Hashtable alNm = GetBedTagNm();
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    dsResult.Tables[0].Rows[i]["BEDTG"] = SetBedTagNm(dsResult.Tables[0].Rows[i]["bed_tag"].ToString(), alNm);
                }
            }

            hotelInfoEntity.QueryResult = dsResult;
            return hotelInfoEntity;
        }

        private static string SetBedTagNm(string bedtag, Hashtable alNm)
        {
            string strResult = "";
            for (int i = 0; i < bedtag.Length; i++)
            {
                if ("1".Equals(bedtag.Substring(i, 1)))
                {
                    strResult = strResult + alNm[i.ToString()] + ",";
                }
            }
            return strResult.TrimEnd(',');
        }

        private static Hashtable GetBedTagNm()
        {
            Hashtable alNm = new Hashtable();
            DataSet dsNm = CommonDA.GetConfigList("bedtype");

            if (dsNm.Tables.Count > 0 && dsNm.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsNm.Tables[0].Rows.Count; i++)
                {
                    alNm.Add(dsNm.Tables[0].Rows[i]["Key"].ToString(), dsNm.Tables[0].Rows[i]["Value"].ToString());
                }
            }
            return alNm;
        }

        public static HotelInfoEntity BedTypeListDetail(HotelInfoEntity hotelInfoEntity)
        {

            OracleParameter[] parm ={
                                    new OracleParameter("BEDCD",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.BedCode;

            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_room_bed_detail", false, parm);
            return hotelInfoEntity;
        }

        public static int InsertBedType(HotelInfoEntity hotelInfoEntity)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            if ("0".Equals(dbParm.Type) && !chkInserBed(hotelInfoEntity))
            {
                return 2;
            }

            OracleParameter[] lmParm ={
                                    new OracleParameter("BEDCD",OracleType.VarChar),
                                    new OracleParameter("BEDNM",OracleType.VarChar),
                                    new OracleParameter("BEDTG",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.BedCode;
            lmParm[1].Value = dbParm.BedName;
            lmParm[2].Value = dbParm.BedTag;
            lmParm[3].Value = hotelInfoEntity.LogMessages.Username;

            string strSQL = "t_lm_bed_isert";
            if ("1".Equals(dbParm.Type))
            {
                strSQL = "t_lm_bed_update";
            }

            DbManager.ExecuteSql("HotelInfo", strSQL, lmParm);
            return 1;
        }

        public static HotelInfoEntity GetBalanceRoomList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("PRICECD",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.PriceCode;
            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_priceroomlist", true, parm);

            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetBalanceRoomListByHotel(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;
            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_priceroomlist_byhotelid", true, parm);

            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetBalanceRoomListByHotelAndPriceCode(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("PRICECODE",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.PriceCode;
            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_byhotelandpricecode", true, parm);

            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetBalHotelRoomList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            parm[0].Value = dbParm.HotelID;
            hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balanceroom_proomlist", true, parm);

            return hotelInfoEntity;
        }

        public static APPContentEntity GetConsultRoomHistoryList(APPContentEntity APPContentEntity)
        {
            APPContentDBEntity dbParm = (APPContentEntity.APPContentDBEntity.Count > 0) ? APPContentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetConsultRoomHistoryList");
            cmd.SetParameterValue("@CityID", dbParm.CityID);
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@PriceCode", dbParm.PriceCode);
            cmd.SetParameterValue("@RoomCode", dbParm.RoomCode);
            cmd.SetParameterValue("@PlanDate", dbParm.PlanDTime);
            APPContentEntity.QueryResult = cmd.ExecuteDataSet();

            return APPContentEntity;
        }

        public static HotelInfoEntity GetConsultRoomHotelRoomList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("SALES",OracleType.VarChar),
                                     new OracleParameter("EXTIME",OracleType.VarChar) 
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.SalesID; ;
            }

            if (String.IsNullOrEmpty(dbParm.EffectDate))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.EffectDate; ;
            }

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("HotelInfo", "t_lm_b_hotelconsulting_roomtable");

            //if (!String.IsNullOrEmpty(dbParm.Type))
            //{
            //    SqlString = SqlString + dbParm.Type;
            //}
            hotelInfoEntity.QueryResult = DbHelperOra.Query(SqlString, true, parm);
            return hotelInfoEntity;

            //hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelconsulting_roomtable", true, parm);
            //return hotelInfoEntity;
        }

        public static HotelInfoEntity GetConsultRoomHotelRoomListByAll(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.QueryResult = DbManager.Query("HotelInfo", "t_lm_b_hotelconsulting_roomtable_byall", true);
            return hotelInfoEntity;

            //hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelconsulting_roomtable", true, parm);
            //return hotelInfoEntity;
        }

        public static HotelInfoEntity GetOrderApproveList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("InDateFrom",OracleType.VarChar),
                                    new OracleParameter("InDateTo",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.SalesID;
            }

            if (String.IsNullOrEmpty(dbParm.InDateFrom))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.InDateFrom;
            }

            if (String.IsNullOrEmpty(dbParm.InDateTo))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.InDateTo;
            }

            if (String.IsNullOrEmpty(dbParm.AuditStatus))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.AuditStatus;
            }

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("HotelInfo", "t_lm_b_hotelorder_approvetable");
            hotelInfoEntity.QueryResult = DbHelperOra.Query(SqlString, true, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetOrderFaxApproveList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("SALES",OracleType.VarChar),
                                    new OracleParameter("OutSDate",OracleType.VarChar),
                                    new OracleParameter("OutEDate",OracleType.VarChar),
                                    new OracleParameter("FaxStatus",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.SalesID;
            }

            if (String.IsNullOrEmpty(dbParm.OutStartDate))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.OutStartDate;
            }

            if (String.IsNullOrEmpty(dbParm.OutEndDate))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.OutEndDate;
            }

            if (String.IsNullOrEmpty(dbParm.FaxStatus))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.FaxStatus;
            }

            if (String.IsNullOrEmpty(dbParm.AuditStatus))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.AuditStatus;
            }

            DataSet dsHLResult = DbManager.Query("HotelInfo", "t_lm_b_hotelorder_fax_approvetable", true, parm);

            //if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dbParm.ADStatsBack))
            //{
            //    for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
            //    {
            //        if (!chkDApproveData(hotelInfoEntity, dsResult.Tables[0].Rows[i]))
            //        {
            //            dsResult.Tables[0].Rows.RemoveAt(i);
            //        }
            //    }
            //}

            DataSet dsODResult = DbManager.Query("HotelInfo", "t_lm_b_hotelorder_fax_approvetable_order", true, parm);
            DataSet dsResult = ClearDApproveData(hotelInfoEntity, dsHLResult, dsODResult);

            hotelInfoEntity.QueryResult = dsResult;
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetConsultRoomHotelRoomListByProp(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("EXTIME",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID; ;

            }
            if (String.IsNullOrEmpty(dbParm.EffectDate))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.EffectDate; ;
            }
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("HotelInfo", "t_lm_b_hotelconsulting_roomtable_byprop");

            if (!String.IsNullOrEmpty(dbParm.Type))
            {
                SqlString = SqlString + " order by " + dbParm.Type;
            }

            hotelInfoEntity.QueryResult = DbHelperOra.Query(SqlString, true, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetConsultRoomHotelRoomListByHotel(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TRADEAREAID",OracleType.VarChar),
                                    //new OracleParameter("NOTPROP",OracleType.VarChar),
                                    new OracleParameter("EXTIME",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID; ;
            }
            if (String.IsNullOrEmpty(dbParm.City))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.City; ;
            }
            if (String.IsNullOrEmpty(dbParm.Bussiness))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.Bussiness; ;
            }
            //if (String.IsNullOrEmpty(dbParm.BalValue))//过滤锦江酒店  
            //{
            //    parm[3].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[3].Value = dbParm.BalValue; ;
            //}
            if (String.IsNullOrEmpty(dbParm.EffectDate))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.EffectDate; ;
            }
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("HotelInfo", "t_lm_b_hotelconsulting_roomtable_byhotel");

            if (!String.IsNullOrEmpty(dbParm.Type))
            {
                SqlString = SqlString + " order by " + dbParm.Type;
            }

            hotelInfoEntity.QueryResult = DbHelperOra.Query(SqlString, true, parm);
            return hotelInfoEntity;
            //hotelInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_hotelconsulting_roomtable_byhotel", true, parm);

            //return hotelInfoEntity;
        }

        public static HotelInfoEntity GetOrderApproveListByHotel(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TRADEAREAID",OracleType.VarChar),
                                    new OracleParameter("InDateFrom",OracleType.VarChar),
                                    new OracleParameter("InDateTo",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar)
                                    };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID; ;
            }
            if (String.IsNullOrEmpty(dbParm.City))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.City; ;
            }
            if (String.IsNullOrEmpty(dbParm.Bussiness))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.Bussiness;
            }

            if (String.IsNullOrEmpty(dbParm.InDateFrom))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.InDateFrom;
            }

            if (String.IsNullOrEmpty(dbParm.InDateTo))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.InDateTo;
            }

            if (String.IsNullOrEmpty(dbParm.AuditStatus))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.AuditStatus;
            }

            if (String.IsNullOrEmpty(dbParm.OrderID))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.OrderID;
            }

            hotelInfoEntity.QueryResult = DbManager.Query("HotelInfo", "t_lm_b_order_approvetable_byhotel", true, parm);
            return hotelInfoEntity;
        }

        public static HotelInfoEntity GetOrderApproveHotelFaxList(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TRADEAREAID",OracleType.VarChar),
                                    new OracleParameter("OutSDate",OracleType.VarChar),
                                    new OracleParameter("OutEDate",OracleType.VarChar),
                                    new OracleParameter("FaxNum",OracleType.VarChar),
                                    new OracleParameter("FaxStatus",OracleType.VarChar),
                                    new OracleParameter("AuditStatus",OracleType.VarChar),
                                    new OracleParameter("OrderID",OracleType.VarChar)
                                    };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID; ;
            }
            if (String.IsNullOrEmpty(dbParm.City))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.City; ;
            }
            if (String.IsNullOrEmpty(dbParm.Bussiness))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.Bussiness;
            }

            if (String.IsNullOrEmpty(dbParm.OutStartDate))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.OutStartDate;
            }

            if (String.IsNullOrEmpty(dbParm.OutEndDate))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.OutEndDate;
            }

            if (String.IsNullOrEmpty(dbParm.FaxNum))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.FaxNum;
            }

            if (String.IsNullOrEmpty(dbParm.FaxStatus))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.FaxStatus;
            }

            if (String.IsNullOrEmpty(dbParm.AuditStatus))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.AuditStatus;
            }

            if (String.IsNullOrEmpty(dbParm.OrderID))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = dbParm.OrderID;
            }

            DataSet dsHLResult = DbManager.Query("HotelInfo", "t_lm_b_order_approve_fax_byhotel", true, parm);

            DataSet dsODResult = DbManager.Query("HotelInfo", "t_lm_b_order_approve_fax_byhotel_order", true, parm);

            //if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dbParm.ADStatsBack))
            //{
            //    for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
            //    {
            //        if (!chkDApproveData(hotelInfoEntity, dsResult.Tables[0].Rows[i]))
            //        {
            //            dsResult.Tables[0].Rows.RemoveAt(i);
            //        }
            //    }
            //}



            DataSet dsResult = ClearDApproveData(hotelInfoEntity, dsHLResult, dsODResult);


            hotelInfoEntity.QueryResult = dsResult;
            return hotelInfoEntity;
        }

        public static DataSet ClearDApproveData(HotelInfoEntity hotelInfoEntity, DataSet dsHotel, DataSet dsOrder)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
            if (dsHotel.Tables.Count > 0 && dsHotel.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dbParm.ADStatsBack))
            {
                for (int i = dsHotel.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    if (!RefushDApproveData(hotelInfoEntity, dsHotel.Tables[0].Rows[i], dsOrder))
                    {
                        dsHotel.Tables[0].Rows.RemoveAt(i);
                    }
                }
            }
            return dsHotel;
        }

        public static bool RefushDApproveData(HotelInfoEntity hotelInfoEntity, DataRow drRow, DataSet dsOrder)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            string LDStatus = string.Empty;
            string NSStatus = string.Empty;
            string LDdbAppr = string.Empty;
            string NSdbAppr = string.Empty;
            string ADStatsBack = (String.IsNullOrEmpty(dbParm.ADStatsBack)) ? "" : dbParm.ADStatsBack;

            if (ADStatsBack.Contains("1") || ADStatsBack.Contains("3"))
            {
                LDStatus = "离店";
            }

            if (ADStatsBack.Contains("2") || ADStatsBack.Contains("4"))
            {
                NSStatus = "No-Show";
            }

            if (ADStatsBack.Contains("1"))
            {
                LDdbAppr = "0";
            }
            else if (ADStatsBack.Contains("3"))
            {
                LDdbAppr = "1";
            }

            if (ADStatsBack.Contains("2"))
            {
                NSdbAppr = "0";
            }
            else if (ADStatsBack.Contains("4"))
            {
                NSdbAppr = "1";
            }

            DataRow[] drList = dsOrder.Tables[0].Select("prop='" + drRow["prop"].ToString() + "'");
            int iCount = drList.Length;
            for (int i = drList.Length - 1; i >= 0; i--)
            {
                if ("未审核".Equals(drList[i]["ORDERST"].ToString()))
                {
                    continue;
                }

                if (!String.IsNullOrEmpty(ADStatsBack))
                {
                    DataCommand cmd = DataCommandManager.GetDataCommand("ChkApproveOrderList");
                    cmd.SetParameterValue("@ORDERID", drList[i]["ORDERID"].ToString());
                    DataSet dsResult = cmd.ExecuteDataSet();

                    if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
                    {
                        iCount = iCount - 1;
                    }
                    else
                    {
                        if ("离店".Equals(dsResult.Tables[0].Rows[0]["OD_STATUS"].ToString()))
                        {
                            if (!(!String.IsNullOrEmpty(LDStatus) && LDdbAppr.Equals(dsResult.Tables[0].Rows[0]["ISDBAPPROVE"].ToString())))
                            {
                                iCount = iCount - 1;
                            }
                        }
                        else if ("No-Show".Equals(dsResult.Tables[0].Rows[0]["OD_STATUS"].ToString()))
                        {
                            if (!(!String.IsNullOrEmpty(NSStatus) && NSdbAppr.Equals(dsResult.Tables[0].Rows[0]["ISDBAPPROVE"].ToString())))
                            {
                                iCount = iCount - 1;
                            }
                        }
                        else
                        {
                            iCount = iCount - 1;
                        }
                    }
                }
            }

            if (iCount > 0)
            {
                drRow["ordercount"] = iCount.ToString();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool chkDApproveData(HotelInfoEntity hotelInfoEntity, DataRow drRow)
        {
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            string LDStatus = string.Empty;
            string NSStatus = string.Empty;
            string LDdbAppr = string.Empty;
            string NSdbAppr = string.Empty;
            string ADStatsBack = (String.IsNullOrEmpty(dbParm.ADStatsBack)) ? "" : dbParm.ADStatsBack;

            if (ADStatsBack.Contains("1") || ADStatsBack.Contains("3"))
            {
                LDStatus = "离店";
            }

            if (ADStatsBack.Contains("2") || ADStatsBack.Contains("4"))
            {
                NSStatus = "No-Show";
            }

            if (ADStatsBack.Contains("1"))
            {
                LDdbAppr = "0";
            }
            else if (ADStatsBack.Contains("3"))
            {
                LDdbAppr = "1";
            }

            if (ADStatsBack.Contains("2"))
            {
                NSdbAppr = "0";
            }
            else if (ADStatsBack.Contains("4"))
            {
                NSdbAppr = "1";
            }

            OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
            _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
            OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
            orderinfoEntity.HotelID = drRow["prop"].ToString();
            orderinfoEntity.OutDate = dbParm.OutDate;
            orderinfoEntity.OrderID = dbParm.OrderID;
            orderinfoEntity.AuditStatus = dbParm.AuditStatus;
            orderinfoEntity.ADStatsBack = dbParm.ADStatsBack;
            _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);

           DataSet dsOrders = OrderInfoDA.BindOrderApproveFaxList(_orderInfoEntity).QueryResult;
           DataSet dsResult = new DataSet();
           for (int i = dsOrders.Tables[0].Rows.Count - 1; i >= 0; i--)
           {
               if ("未审核".Equals(dsOrders.Tables[0].Rows[i]["ORDERST"].ToString()))
               {
                    continue;
               }

               if (!String.IsNullOrEmpty(ADStatsBack))
               {
                   DataCommand cmd = DataCommandManager.GetDataCommand("ChkApproveOrderList");
                   cmd.SetParameterValue("@ORDERID", dsOrders.Tables[0].Rows[i]["ORDERID"].ToString());
                   dsResult = cmd.ExecuteDataSet();

                   if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
                   {
                       dsOrders.Tables[0].Rows.RemoveAt(i);
                   }
                   else
                   {
                       if ("离店".Equals(dsResult.Tables[0].Rows[0]["OD_STATUS"].ToString()))
                       {
                           if (!(!String.IsNullOrEmpty(LDStatus) && LDdbAppr.Equals(dsResult.Tables[0].Rows[0]["ISDBAPPROVE"].ToString())))
                           {
                               dsOrders.Tables[0].Rows.RemoveAt(i);
                           }
                       }
                       else if ("No-Show".Equals(dsResult.Tables[0].Rows[0]["OD_STATUS"].ToString()))
                       {
                           if (!(!String.IsNullOrEmpty(NSStatus) && NSdbAppr.Equals(dsResult.Tables[0].Rows[0]["ISDBAPPROVE"].ToString())))
                           {
                               dsOrders.Tables[0].Rows.RemoveAt(i);
                           }
                       }
                       else
                       {
                           dsOrders.Tables[0].Rows.RemoveAt(i);
                       }
                   }
               }
           }

           if (dsOrders.Tables.Count > 0 && dsOrders.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public static string GetFaxHotelListColr(string HotelID, HotelInfoEntity hotelInfoEntity)
        //{
        //    OracleParameter[] parm ={
        //                            new OracleParameter("HOTELID",OracleType.VarChar),
        //                            new OracleParameter("OutDate",OracleType.VarChar),
        //                            new OracleParameter("FaxNum",OracleType.VarChar),
        //                            new OracleParameter("FaxStatus",OracleType.VarChar),
        //                            new OracleParameter("AuditStatus",OracleType.VarChar),
        //                            new OracleParameter("OrderID",OracleType.VarChar),
        //                            };
        //    HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

        //    parm[0].Value = HotelID;
        //    if (String.IsNullOrEmpty(dbParm.OutDate))
        //    {
        //        parm[1].Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        parm[1].Value = dbParm.OutDate;
        //    }

        //    if (String.IsNullOrEmpty(dbParm.FaxNum))
        //    {
        //        parm[2].Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        parm[2].Value = dbParm.FaxNum;
        //    }

        //    if (String.IsNullOrEmpty(dbParm.FaxStatus))
        //    {
        //        parm[3].Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        parm[3].Value = dbParm.FaxStatus;
        //    }

        //    if (String.IsNullOrEmpty(dbParm.AuditStatus))
        //    {
        //        parm[4].Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        parm[4].Value = dbParm.AuditStatus;
        //    }

        //    if (String.IsNullOrEmpty(dbParm.OrderID))
        //    {
        //        parm[5].Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        parm[5].Value = dbParm.OrderID;
        //    }

        //    DataSet dsResult = DbManager.Query("OrderInfo", "t_lm_orderappromginfo_fax_select", true, parm);

        //    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        //    {
                
        //    }
        //    else
        //    {
        //        return "0";
        //    }
        //}
        

        public static APPContentEntity GetHasChangedConsultRoomList(APPContentEntity APPContentEntity)
        {
            APPContentDBEntity dbParm = (APPContentEntity.APPContentDBEntity.Count > 0) ? APPContentEntity.APPContentDBEntity[0] : new APPContentDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetHasChangedConsultRoomList");
            cmd.SetParameterValue("@CreateUser", dbParm.CreateUser);
            APPContentEntity.QueryResult = cmd.ExecuteDataSet();

            return APPContentEntity;
        }

        public static HotelInfoEntity CheckApproveUser(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("SALES",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["OrderAproID"])) ? "11" : ConfigurationManager.AppSettings["OrderAproID"].ToString().Trim();
            DataCommand cmd = DataCommandManager.GetDataCommand("CheckApproveUser");
            cmd.SetParameterValue("@UserID", dbParm.SalesID);
            cmd.SetParameterValue("@RoleID", RoleID);
            hotelInfoEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelInfoEntity;
        }

        public static HotelInfoEntity CheckApproveUserBandHotel(HotelInfoEntity hotelInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("SALES",OracleType.VarChar)
                                };
            HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.SalesID;
            }

            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("HotelInfo", "CheckApproveUserBandHotel");
            hotelInfoEntity.QueryResult = DbHelperOra.Query(SqlString, true, parm);
            return hotelInfoEntity;
        }
    }
}
