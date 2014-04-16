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
    public abstract class DestinationDA
    {
        public static DestinationEntity DestinationTypeDetail(DestinationEntity destinationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();
            parm[0].Value = dbParm.DestinationID;
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_type_detail",false, parm);
            return destinationEntity;
        }

        public static DestinationEntity CommonTypeSelectSigle(DestinationEntity destinationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();
            parm[0].Value = dbParm.DestinationID;
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_type_common", false, parm);
            return destinationEntity;
        }

        public static DestinationEntity DestinationListSelect(DestinationEntity destinationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TYPEID",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                };
            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();

            if (String.IsNullOrEmpty(dbParm.DestinationID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.DestinationID;
            }

            if (String.IsNullOrEmpty(dbParm.CityID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.CityID;
            }

            if (String.IsNullOrEmpty(dbParm.DestinationTypeID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.DestinationTypeID;
            }

            if (String.IsNullOrEmpty(dbParm.Status))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Status;
            }

            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_all", false, parm);
            return destinationEntity;
        }

        public static DestinationEntity CommonTypeSelect(DestinationEntity destinationEntity)
        {
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_type", false);
            return destinationEntity;
        }

        public static DestinationEntity TypeSelect(DestinationEntity destinationEntity)
        {
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_type_union", false);
            return destinationEntity;
        }

        public static int CheckInsert(DestinationEntity destinationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TYPENM",OracleType.VarChar)
                                };
            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();
            parm[0].Value = dbParm.Name_CN;
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_type_sigle", false, parm);

            if (destinationEntity.QueryResult.Tables.Count > 0 && destinationEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckDestinationUpdate(DestinationEntity destinationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar),
                                    new OracleParameter("DESNM",OracleType.VarChar)
                                };
            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();
            parm[0].Value = dbParm.DestinationID;
            parm[1].Value = dbParm.Name_CN;
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_update_sigle", false, parm);

            if (destinationEntity.QueryResult.Tables.Count > 0 && destinationEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckDestinationCityInsert(DestinationEntity destinationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar)
                                };
            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();
            parm[0].Value = dbParm.CityID;
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_city_check", false, parm);

            if (destinationEntity.QueryResult.Tables.Count > 0 && destinationEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckDestinationInsert(DestinationEntity destinationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("DESNM",OracleType.VarChar)
                                };
            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();
            parm[0].Value = dbParm.Name_CN;
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_sigle", false, parm);

            if (destinationEntity.QueryResult.Tables.Count > 0 && destinationEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int DestinationUpdateBatchHotel(DestinationEntity destinationEntity)
        {
            if (destinationEntity.DestinationDBEntity.Count == 0)
            {
                return 0;
            }

            if (destinationEntity.LogMessages == null)
            {
                return 0;
            }

            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();

            OracleParameter[] lmhotelParm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("DESTINATIONID",OracleType.VarChar)
                                };
            lmhotelParm[0].Value = dbParm.CityID;
            lmhotelParm[1].Value = dbParm.DestinationID;
            DataSet dsCity = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_hotel_batchlist", false, lmhotelParm);


            int MaxLength = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLength"].ToString())) ? 1000 : int.Parse(ConfigurationManager.AppSettings["MaxLength"].ToString());
            List<CommandInfo> cmdList = new List<CommandInfo>();
           
            string strSQL = XmlSqlAnalyze.GotSqlTextFromXml("Destination", "t_lm_b_destination_hotel_save");
            int iCount = 0;

            for (int i = 0; i <= dsCity.Tables[0].Rows.Count - 1; i++)
            {
                if (String.IsNullOrEmpty(dsCity.Tables[0].Rows[i]["DESTINATIONID"].ToString()))
                {
                    continue;
                }

                dsCity.Tables[0].Rows[i]["DISTANCE"] = GetTotalDistance(dsCity.Tables[0].Rows[i]["HLLATITUDE"].ToString(), dsCity.Tables[0].Rows[i]["HLLONGITUDE"].ToString(), dsCity.Tables[0].Rows[i]["DTLATITUDE"].ToString(), dsCity.Tables[0].Rows[i]["DTLONGITUDE"].ToString());

                CommandInfo cminfo = new CommandInfo();
                cminfo.CommandText = strSQL;
                OracleParameter[] lmParm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("DESTINATIONID",OracleType.VarChar),
                                    new OracleParameter("TYPEID",OracleType.VarChar),
                                    new OracleParameter("DISTANCE",OracleType.VarChar)
                                };

                lmParm[0].Value = dsCity.Tables[0].Rows[i]["CITYID"].ToString();
                lmParm[1].Value = dsCity.Tables[0].Rows[i]["HOTELID"].ToString();
                lmParm[2].Value = dsCity.Tables[0].Rows[i]["DESTINATIONID"].ToString();
                lmParm[3].Value = dsCity.Tables[0].Rows[i]["TYPEID"].ToString();
                lmParm[4].Value = dsCity.Tables[0].Rows[i]["DISTANCE"].ToString();
                cminfo.Parameters = lmParm;
                cmdList.Add(cminfo);
                iCount = iCount + 1;
                if (MaxLength == iCount)
                {
                    try
                    {
                        DbHelperOra.ExecuteSqlTran(cmdList);
                    }
                    catch
                    {
                    }

                    iCount = 0;
                    cmdList.Clear();
                }
            }

            if (iCount > 0)
            {
                try
                {
                    DbHelperOra.ExecuteSqlTran(cmdList);
                }
                catch
                {
                }
            }

            return 1;
        }

        private const double EARTH_RADIUS = 6378.137; //地球半径
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public static double GetTotalDistance(string strlat1, string strlng1, string strlat2, string strlng2)
        {
            double horizontal = GetDistance(strlat1, strlng1, strlat1, strlng2);
            double vertical = GetDistance(strlat1, strlng2, strlat2, strlng2);
            double result = horizontal + vertical;

            if (result == 0d)
            {
                return 0.1d;
            }
            else
            {
                return result;
            }
        }

        public static double GetDistance(string strlat1, string strlng1, string strlat2, string strlng2)
        {
            double lat1 = (String.IsNullOrEmpty(strlat1)) ? 0d : double.Parse(strlat1);
            double lng1 = (String.IsNullOrEmpty(strlng1)) ? 0d : double.Parse(strlng1);
            double lat2 = (String.IsNullOrEmpty(strlat2)) ? 0d : double.Parse(strlat2);
            double lng2 = (String.IsNullOrEmpty(strlng2)) ? 0d : double.Parse(strlng2);

            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round((Math.Round(s * 10000) / 10000), 1);
            return s;
        }

        public static int DestinationUpdate(DestinationEntity destinationEntity)
        {
            if (destinationEntity.DestinationDBEntity.Count == 0)
            {
                return 0;
            }

            if (destinationEntity.LogMessages == null)
            {
                return 0;
            }

            //if (CheckDestinationUpdate(destinationEntity) > 0)
            //{
            //    return 2;
            //}

            if (CheckDestinationCityInsert(destinationEntity) == 0)
            {
                return 3;
            }

            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();

            CommandInfo InsertLmPaymentInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TYPEID",OracleType.VarChar),
                                    new OracleParameter("NAMECN",OracleType.VarChar),
                                    new OracleParameter("ADDRESSCN",OracleType.VarChar),
                                    new OracleParameter("TELST",OracleType.VarChar),
                                    new OracleParameter("TELLG",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.DestinationID;
            lmParm[1].Value = dbParm.CityID;
            lmParm[2].Value = dbParm.DestinationTypeID;
            lmParm[3].Value = dbParm.Name_CN;
            lmParm[4].Value = dbParm.AddRess;
            lmParm[5].Value = dbParm.TelST;
            lmParm[6].Value = dbParm.TelLG;
            lmParm[7].Value = dbParm.Latitude;
            lmParm[8].Value = dbParm.Longitude;
            lmParm[9].Value = dbParm.OnlineStatus;
            DbManager.ExecuteSql("Destination", "t_lm_b_destination_update", lmParm);
            return 1;
        }

        public static DestinationEntity DestinationInsert(DestinationEntity destinationEntity)
        {
            if (destinationEntity.DestinationDBEntity.Count == 0)
            {
                destinationEntity.Result = 0;
                return destinationEntity;
            }

            if (destinationEntity.LogMessages == null)
            {
                destinationEntity.Result = 0;
                return destinationEntity;
            }

            //if (CheckDestinationInsert(destinationEntity) > 0)
            //{
            //    return 2;
            //}

            if (CheckDestinationCityInsert(destinationEntity) == 0)
            {
                destinationEntity.Result = 3;
                return destinationEntity;
            }

            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();

            CommandInfo InsertLmPaymentInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TYPEID",OracleType.VarChar),
                                    new OracleParameter("NAMECN",OracleType.VarChar),
                                    new OracleParameter("ADDRESSCN",OracleType.VarChar),
                                    new OracleParameter("TELST",OracleType.VarChar),
                                    new OracleParameter("TELLG",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar)
                                };

            lmParm[0].Value = getMaxIDfromSeq("T_LM_B_DESTINATION_SEQ");
            lmParm[1].Value = dbParm.CityID;
            lmParm[2].Value = dbParm.DestinationTypeID;
            lmParm[3].Value = dbParm.Name_CN;
            lmParm[4].Value = dbParm.AddRess;
            lmParm[5].Value = dbParm.TelST;
            lmParm[6].Value = dbParm.TelLG;
            lmParm[7].Value = dbParm.Latitude;
            lmParm[8].Value = dbParm.Longitude;
            destinationEntity.DestinationDBEntity[0].DestinationID = lmParm[0].Value.ToString();
            DbManager.ExecuteSql("Destination", "t_lm_b_destination_insert", lmParm);

            destinationEntity.Result = 1;
            return destinationEntity;
        }

        public static int Insert(DestinationEntity destinationEntity)
        {
            if (destinationEntity.DestinationDBEntity.Count == 0)
            {
                return 0;
            }

            if (destinationEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(destinationEntity) > 0)
            {
                return 2;
            }

            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();

            CommandInfo InsertLmPaymentInfo = new CommandInfo();
            OracleParameter[] lmParm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PARENTSID",OracleType.VarChar),
                                    new OracleParameter("NAMECN",OracleType.VarChar)                               
                                };

            lmParm[0].Value = getMaxIDfromSeq("T_LM_B_DESTINATION_TYPE_SEQ");
            lmParm[1].Value = dbParm.ParentsID;
            lmParm[2].Value = dbParm.Name_CN;
            DbManager.ExecuteSql("Destination", "t_lm_b_destination_type_insert", lmParm);

            if (!String.IsNullOrEmpty(dbParm.ParentsID))
            {
                UpdateFlag(dbParm.ParentsID, "0");
            }

            return 1;
        }

        public static int UpdateFlag(string destinationID, string showFlag)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("SHOWFlAG",OracleType.VarChar)

                                };
            parm[0].Value = destinationID;
            parm[1].Value = showFlag;
            DbManager.ExecuteSql("Destination", "t_lm_b_destination_type_update_flag", parm);

            return 1;
        }

        public static DestinationEntity Select(DestinationEntity destinationEntity)
        {
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_type_all", false);
            return destinationEntity;
        }

        public static DataSet GetDestinationParentsID(string destinationID, string parentsID)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            parm[0].Value = destinationID;
            //parm[1].Value = parentsID;
            return HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_type_parents",false, parm);
        }

        public static int Update(DestinationEntity destinationEntity)
        {
            if (destinationEntity.DestinationDBEntity.Count == 0)
            {
                return 0;
            }

            if (destinationEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckUpdate(destinationEntity) > 0)
            {
                return 2;
            }

            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();
            DataSet dsParents = GetDestinationParentsID(dbParm.DestinationID, dbParm.ParentsID);
            string oldParentsID = (dsParents.Tables[0].Rows.Count > 0) ? dsParents.Tables[0].Rows[0]["PARENTSID"].ToString() : "";
            int iCount = (dsParents.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsParents.Tables[0].Rows[0]["ICOUNT"].ToString())) ? int.Parse(dsParents.Tables[0].Rows[0]["ICOUNT"].ToString()) : 0;

            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("PARENTSID",OracleType.VarChar),
                                    new OracleParameter("NAMECN",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar)

                                };
            parm[0].Value = dbParm.DestinationID;
            parm[1].Value = dbParm.ParentsID;
            parm[2].Value = dbParm.Name_CN;
            parm[3].Value = dbParm.OnlineStatus;
            DbManager.ExecuteSql("Destination", "t_lm_b_destination_type_update", parm);

            if (!String.IsNullOrEmpty(dbParm.ParentsID))
            {
                UpdateFlag(dbParm.ParentsID, "0");
            }

            if (!oldParentsID.Equals("0") && !oldParentsID.Equals(dbParm.ParentsID) && iCount == 0)
            {
                UpdateFlag(oldParentsID, "1");
            }

            return 1;
        }

        public static int CheckUpdate(DestinationEntity destinationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Number),
                                    new OracleParameter("NAMECN",OracleType.VarChar)                                 
                                };

            DestinationDBEntity dbParm = (destinationEntity.DestinationDBEntity.Count > 0) ? destinationEntity.DestinationDBEntity[0] : new DestinationDBEntity();

            parm[0].Value = int.Parse(dbParm.DestinationID.ToString());
            parm[1].Value = dbParm.Name_CN;

            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_lm_b_destination_type_updatesigle", false, parm);
            if (destinationEntity.QueryResult.Tables.Count > 0 && destinationEntity.QueryResult.Tables[0].Rows.Count > 0)
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

        public static DestinationEntity CitySelect(DestinationEntity destinationEntity)
        {
            destinationEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Destination", "t_fc_city", false);
            return destinationEntity;
        }       
    }
}