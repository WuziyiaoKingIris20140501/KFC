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
    public abstract class PromotionDA
    {
        public static PromotionEntity CommonSelect(PromotionEntity promotionEntity)
        {
            promotionEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_fc_city", false);
            return promotionEntity;
        }

        public static PromotionEntity GetCityList(PromotionEntity promotionEntity)
        {
            promotionEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_fc_city", false);
            return promotionEntity;
        }

        public static PromotionEntity GetHotelList(PromotionEntity promotionEntity)
        {
            promotionEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_hotel_list", false);
            return promotionEntity;
        }

        public static PromotionEntity GetHotelGroupList(PromotionEntity promotionEntity)
        {
            promotionEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_hotelgroup_list", false);
            return promotionEntity;
        }

        public static PromotionEntity GetUserGroupList(PromotionEntity promotionEntity)
        {
            promotionEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_usergroup_list", false);
            return promotionEntity;
        }

        public static DataSet GetBalanceRoomList(PromotionEntity promotionEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("PRICECD",OracleType.VarChar)
                                };
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.RateCode;

            return HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_priceroomlist", true, parm);;
        }

        public static DataSet GetHotelRoomList(PromotionEntity promotionEntity)
        {
            //OracleParameter[] parm ={
            //                        new OracleParameter("HOTELID",OracleType.VarChar)
            //                    };
            //PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();

            //if (String.IsNullOrEmpty(dbParm.HotelID))
            //{
            //    parm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[0].Value = dbParm.HotelID;
            //}

            //return HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_hotelroom_list", false, parm);
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("PRICECD",OracleType.VarChar)
                                };
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(dbParm.RateCode))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RateCode;
            }

            return HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_priceroomlist", false, parm);
        }

        public static DataSet GetHotelRoomListAll(PromotionEntity promotionEntity)
        {
            //OracleParameter[] parm ={
            //                        new OracleParameter("HOTELID",OracleType.VarChar)
            //                    };
            //PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();

            //if (String.IsNullOrEmpty(dbParm.HotelID))
            //{
            //    parm[0].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[0].Value = dbParm.HotelID;
            //}

            //return HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_hotelroom_list", false, parm);
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("PRICECD",OracleType.VarChar)
                                };
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();

            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }

            if (String.IsNullOrEmpty(dbParm.RateCode))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RateCode;
            }

            return HotelVp.Common.DBUtility.DbManager.Query("HotelInfo", "t_lm_b_balancerom_priceroomlist_all", false, parm);
        } 

        public static bool CheckInsert(PromotionEntity promotionEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TITLE",OracleType.VarChar)
                                };
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();
            parm[0].Value = dbParm.Title;
            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_promotion_check_insert", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckUpdate(PromotionEntity promotionEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TITLE",OracleType.VarChar),
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();
            parm[0].Value = dbParm.Title;
            parm[1].Value = dbParm.ID;
            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_promotion_check_Update", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static PromotionEntity MainSelect(PromotionEntity promotionEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();
            parm[0].Value = dbParm.ID;
            promotionEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_promotionmsg_main_select", false, parm);
            return promotionEntity;
        }

        public static PromotionEntity DetailSelect(PromotionEntity promotionEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();
            parm[0].Value = dbParm.ID;
            promotionEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_promotionmsg_detail_select", false, parm);
            return promotionEntity;
        }

        private static DataSet CheckErrUpdate(PromotionEntity promotionEntity)
        {
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();
            string SQLString = "";

            OracleParameter[] parm ={
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("PROTYPE",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("COMMONLIST",OracleType.VarChar),
                                    new OracleParameter("PRIORITY",OracleType.VarChar),
                                    new OracleParameter("TITLE",OracleType.VarChar),
                                    new OracleParameter("ID",OracleType.VarChar)
                                };

            parm[0].Value = dbParm.StartDTime;
            parm[1].Value = dbParm.EndDTime;
            parm[2].Value = dbParm.Type;

            if ("0".Equals(dbParm.Type))
            {
                SQLString = "t_lm_promotion_check_err_update_all";
                parm[3].Value = DBNull.Value;
                parm[4].Value = DBNull.Value;
            }
            else if ("1".Equals(dbParm.Type))
            {
                parm[3].Value = DBNull.Value;
                if ("1".Equals(dbParm.ChkType))
                {
                    SQLString = "t_lm_promotion_check_err_update_city_chk";
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    SQLString = "t_lm_promotion_check_err_update_city";
                    parm[4].Value = dbParm.CommonList;
                }
            }
            else if ("2".Equals(dbParm.Type))
            {
                parm[3].Value = DBNull.Value;
                if ("1".Equals(dbParm.ChkType))
                {
                    SQLString = "t_lm_promotion_check_err_update_group_chk";
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    SQLString = "t_lm_promotion_check_err_update_group";
                    parm[4].Value = dbParm.CommonList;
                }
            }
            else if ("3".Equals(dbParm.Type))
            {
                parm[3].Value = DBNull.Value;
                if ("1".Equals(dbParm.ChkType))
                {
                    SQLString = "t_lm_promotion_check_err_update_hotel_chk";
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    SQLString = "t_lm_promotion_check_err_update_hotel";
                    parm[4].Value = dbParm.CommonList;
                }
            }
            else if ("4".Equals(dbParm.Type))
            {
                if ("1".Equals(dbParm.ChkType))
                {
                    SQLString = "t_lm_promotion_check_err_update_room_chk";
                    parm[3].Value = DBNull.Value;
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    SQLString = "t_lm_promotion_check_err_update_room";
                    parm[3].Value = dbParm.HotelID;
                    parm[4].Value = dbParm.CommonList;
                }
            }
            parm[5].Value = dbParm.Priority;
            parm[6].Value = dbParm.Title;
            parm[7].Value = dbParm.ID;
            return HotelVp.Common.DBUtility.DbManager.Query("Promotion", SQLString, false, parm);
        }

        private static DataSet CheckErrInsert(PromotionEntity promotionEntity)
        {
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();
            string SQLString = "";
            
            OracleParameter[] parm ={
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("PROTYPE",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("COMMONLIST",OracleType.VarChar),
                                    new OracleParameter("PRIORITY",OracleType.VarChar),
                                     new OracleParameter("TITLE",OracleType.VarChar),
                                };
            
            parm[0].Value = dbParm.StartDTime;
            parm[1].Value = dbParm.EndDTime;
            parm[2].Value = dbParm.Type;

            if ("0".Equals(dbParm.Type))
            {
                SQLString = "t_lm_promotion_check_err_insert_all";
                parm[3].Value = DBNull.Value;
                parm[4].Value = DBNull.Value;
            }
            else if ("1".Equals(dbParm.Type))
            {
                SQLString = "t_lm_promotion_check_err_insert_city";
                parm[3].Value = DBNull.Value;
                parm[4].Value = dbParm.CommonList;
            }
            else if ("2".Equals(dbParm.Type))
            {
                SQLString = "t_lm_promotion_check_err_insert_group";
                parm[3].Value = DBNull.Value;
                parm[4].Value = dbParm.CommonList;
            }
            else if ("3".Equals(dbParm.Type))
            {
                SQLString = "t_lm_promotion_check_err_insert_hotel";
                parm[3].Value = DBNull.Value;
                parm[4].Value = dbParm.CommonList;
            }
            else if ("4".Equals(dbParm.Type))
            {
                SQLString = "t_lm_promotion_check_err_insert_room";
                  parm[3].Value = dbParm.HotelID;
                  parm[4].Value = dbParm.CommonList;
            }

            parm[5].Value = dbParm.Priority;
            parm[6].Value = dbParm.Title;
            return HotelVp.Common.DBUtility.DbManager.Query("Promotion", SQLString, false, parm);
        }

        public static PromotionEntity Insert(PromotionEntity promotionEntity)
        {
            if (promotionEntity.PromotionDBEntity.Count == 0)
            {
                promotionEntity.Result = 0;
                return promotionEntity;
            }

            if (promotionEntity.LogMessages == null)
            {
                promotionEntity.Result = 0;
                return promotionEntity;
            }

            if (!CheckInsert(promotionEntity))
            {
                promotionEntity.Result = 2;
                return promotionEntity;
            }

            //DataSet dsErr = CheckErrInsert(promotionEntity);
            //if (dsErr.Tables.Count > 0 && dsErr.Tables[0].Rows.Count > 0)
            //{
            //    promotionEntity.Result = 3;
            //    promotionEntity.ErrotMSG = "";
            //    return promotionEntity;
            //}

            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();

            List<CommandInfo> sqlList = new List<CommandInfo>();
            string strImageID = "";
            if (!String.IsNullOrEmpty(dbParm.Imageid))
            {
                CommandInfo InsertLmImagesInfo = new CommandInfo();
                OracleParameter[] lmImagesParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("TITLE",OracleType.VarChar),
                                    new OracleParameter("PATH",OracleType.VarChar)
                                };

                lmImagesParm[0].Value = getMaxIDfromSeq("T_IMAGE_SEQ");
                lmImagesParm[1].Value = DBNull.Value;
                lmImagesParm[2].Value = dbParm.Imageid;

                InsertLmImagesInfo.SqlName = "Promotion";
                InsertLmImagesInfo.SqlId = "t_lm_promotion_t_images_insert";
                InsertLmImagesInfo.Parameters = lmImagesParm;
                sqlList.Add(InsertLmImagesInfo);
                strImageID = lmImagesParm[0].Value.ToString();
            }

            CommandInfo InsertLmPromMsgInfo = new CommandInfo();
            OracleParameter[] lmPromMsgParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("TITLE",OracleType.VarChar),
                                    new OracleParameter("PRIORITY",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("CONTENT",OracleType.VarChar),
                                    new OracleParameter("RATECODE",OracleType.VarChar),
                                    new OracleParameter("IMAGEID",OracleType.VarChar),
                                    new OracleParameter("OPERATEUSER",OracleType.VarChar),
                                    new OracleParameter("PROMETHODID",OracleType.VarChar)
                                };                       

            lmPromMsgParm[0].Value = getMaxIDfromSeq("T_LM_PROMOTIONMSG_SEQ");
            lmPromMsgParm[1].Value = dbParm.Title;
            lmPromMsgParm[2].Value = dbParm.Priority;
            lmPromMsgParm[3].Value = dbParm.StartDTime;
            lmPromMsgParm[4].Value = dbParm.EndDTime;
            lmPromMsgParm[5].Value = dbParm.Content;
            lmPromMsgParm[6].Value = String.IsNullOrEmpty(dbParm.RateCode) ? "" : dbParm.RateCode;
  
            if (String.IsNullOrEmpty(dbParm.Imageid))
            {
                lmPromMsgParm[7].Value = DBNull.Value;
            }
            else
            {
                lmPromMsgParm[7].Value = strImageID;
            }
            lmPromMsgParm[8].Value = (promotionEntity.LogMessages != null) ? promotionEntity.LogMessages.Userid : "";
            lmPromMsgParm[9].Value = dbParm.Promethodid;

            InsertLmPromMsgInfo.SqlName = "Promotion";
            InsertLmPromMsgInfo.SqlId = "t_lm_promotionmsg_insert";
            InsertLmPromMsgInfo.Parameters = lmPromMsgParm;
            sqlList.Add(InsertLmPromMsgInfo);

            string PromotionCommonKey = String.IsNullOrEmpty(ConfigurationManager.AppSettings["PromotionCommonKey"]) ? "000000" : ConfigurationManager.AppSettings["PromotionCommonKey"].ToString();

            CommandInfo InsertLmPromMsgDetailInfo = new CommandInfo();
            OracleParameter[] lmPromMsgDetailParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("PROTYPE",OracleType.VarChar),
                                    new OracleParameter("COMMONLIST",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LINKURL",OracleType.VarChar)
                                };

            lmPromMsgDetailParm[0].Value = lmPromMsgParm[0].Value;
            lmPromMsgDetailParm[1].Value = dbParm.Type;
            
            InsertLmPromMsgDetailInfo.SqlName = "Promotion";

            if ("0".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = DBNull.Value;
                lmPromMsgDetailParm[3].Value = DBNull.Value;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_all_insert";
            }
            else if ("1".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                lmPromMsgDetailParm[3].Value = DBNull.Value;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_city_insert";
            }
            else if ("2".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                lmPromMsgDetailParm[3].Value = DBNull.Value;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_group_insert";
            }
            else if ("3".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                lmPromMsgDetailParm[3].Value = DBNull.Value;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_hotel_insert";
            }
            else if ("4".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                lmPromMsgDetailParm[3].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey : dbParm.HotelID;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_room_insert";
            }

            if (String.IsNullOrEmpty(dbParm.LinkUrl))
            {
                lmPromMsgDetailParm[4].Value = DBNull.Value;
            }
            else
            {
                lmPromMsgDetailParm[4].Value = dbParm.LinkUrl;
            }
            
            InsertLmPromMsgDetailInfo.Parameters = lmPromMsgDetailParm;
            sqlList.Add(InsertLmPromMsgDetailInfo);

            if (dbParm.UserGroupList.Length > 0)
            {
                CommandInfo InsertLmPromMsgUserGroupInfo = new CommandInfo();
                OracleParameter[] lmPromMsgDetailUserGroupParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("PROTYPE",OracleType.VarChar),
                                    new OracleParameter("USERGROUPLIST",OracleType.VarChar),
                                    new OracleParameter("LINKURL",OracleType.VarChar)
                                };
                lmPromMsgDetailUserGroupParm[0].Value = lmPromMsgParm[0].Value;
                lmPromMsgDetailUserGroupParm[1].Value = "5";
                lmPromMsgDetailUserGroupParm[2].Value = dbParm.UserGroupList;
                if (String.IsNullOrEmpty(dbParm.LinkUrl))
                {
                    lmPromMsgDetailUserGroupParm[3].Value = DBNull.Value;
                }
                else
                {
                    lmPromMsgDetailUserGroupParm[3].Value = dbParm.LinkUrl;
                }

                InsertLmPromMsgUserGroupInfo.SqlName = "Promotion";
                InsertLmPromMsgUserGroupInfo.SqlId = "t_lm_promotionmsg_usergroup_insert";
                InsertLmPromMsgUserGroupInfo.Parameters = lmPromMsgDetailUserGroupParm;
                sqlList.Add(InsertLmPromMsgUserGroupInfo);
            }

            DbManager.ExecuteSqlTran(sqlList);
            promotionEntity.Result = 1;
            return promotionEntity;
        }

        public static PromotionEntity PromotioningSelect(PromotionEntity promotionEntity)
        {
            promotionEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Promotion", "t_lm_promotionmsg_ing", false);
            return promotionEntity;
        }

        public static PromotionEntity PromotionMsgSelect(PromotionEntity promotionEntity)
        {
            if (promotionEntity.PromotionDBEntity.Count == 0)
            {
                return promotionEntity;
            }

            if (promotionEntity.LogMessages == null)
            {
                return promotionEntity;
            }

            OracleParameter[] parm ={
                                    new OracleParameter("ProTitle",OracleType.VarChar),                       
                                    new OracleParameter("StartBeginDTime",OracleType.VarChar),                       
                                    new OracleParameter("StartEndDTime",OracleType.VarChar),                       
                                    new OracleParameter("EndBeginDTime",OracleType.VarChar),                       
                                    new OracleParameter("EndEndDTime",OracleType.VarChar)

                                };

            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();

            if (String.IsNullOrEmpty(dbParm.Title))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.Title;
            }

            if (String.IsNullOrEmpty(dbParm.StartBeginDTime))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.StartBeginDTime;
            }

            if (String.IsNullOrEmpty(dbParm.StartEndDTime))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.StartEndDTime;
            }

            if (String.IsNullOrEmpty(dbParm.EndBeginDTime))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.EndBeginDTime;
            }

            if (String.IsNullOrEmpty(dbParm.EndEndDTime))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.EndEndDTime;
            }

            promotionEntity.QueryResult = DbManager.Query("Promotion", "t_lm_promotionmsg_review_select", false, parm);
            return promotionEntity;
        }

        //private static string AppendString(string param)
        //{
        //    string result = "";
        //    string[] temp = param.Split(',');

        //    foreach (string strTemp in temp)
        //    {
        //        result = (!String.IsNullOrEmpty(strTemp)) ? result + "'" + strTemp + "'," : result;
        //    }

        //    result = (result.Length > 1) ? result.Substring(0, result.Length - 1) : result;

        //    return result;
        //}

        //private static bool IsMobileNumber(string str_telephone)
        //{
        //    return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"(1[3,4,5,8][0-9])\d{8}$");
        //}

        public static PromotionEntity Update(PromotionEntity promotionEntity)
        {
            if (promotionEntity.PromotionDBEntity.Count == 0)
            {
                promotionEntity.Result = 0;
                return promotionEntity;
            }

            if (promotionEntity.LogMessages == null)
            {
                promotionEntity.Result = 0;
                return promotionEntity;
            }

            if (!CheckUpdate(promotionEntity))
            {
                promotionEntity.Result = 2;
                return promotionEntity;
            }

            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();

            if ("1".Equals(dbParm.Status))
            {
                DataSet dsErr = CheckErrUpdate(promotionEntity);
                if (dsErr.Tables.Count > 0 && dsErr.Tables[0].Rows.Count > 0)
                {
                    promotionEntity.Result = 3;
                    promotionEntity.ErrorMSG = dsErr.Tables[0].Rows[0]["pro_title"].ToString();
                    return promotionEntity;
                }
            }

            List<CommandInfo> sqlList = new List<CommandInfo>();

            string strImageID = "";
            if (!String.IsNullOrEmpty(dbParm.ImagePath))
            {
                
                CommandInfo InsertLmImagesInfo = new CommandInfo();
                OracleParameter[] lmImagesParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("TITLE",OracleType.VarChar),
                                    new OracleParameter("PATH",OracleType.VarChar)
                                };
                if (String.IsNullOrEmpty(dbParm.Imageid))
                {
                    lmImagesParm[0].Value = getMaxIDfromSeq("T_IMAGE_SEQ");
                }
                else
                {
                    lmImagesParm[0].Value = dbParm.Imageid;
                }
                lmImagesParm[1].Value = DBNull.Value;
                lmImagesParm[2].Value = dbParm.ImagePath;

                InsertLmImagesInfo.SqlName = "Promotion";

                if (String.IsNullOrEmpty(dbParm.Imageid))
                {
                    InsertLmImagesInfo.SqlId = "t_lm_promotion_t_images_insert";
                }
                else
                {
                    InsertLmImagesInfo.SqlId = "t_lm_promotion_t_images_update";
                }
                
                InsertLmImagesInfo.Parameters = lmImagesParm;
                sqlList.Add(InsertLmImagesInfo);
                strImageID = lmImagesParm[0].Value.ToString();
            }

            CommandInfo InsertLmPromMsgInfo = new CommandInfo();
            OracleParameter[] lmPromMsgParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("TITLE",OracleType.VarChar),
                                    new OracleParameter("PRIORITY",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("CONTENT",OracleType.VarChar),
                                    new OracleParameter("IMAGEID",OracleType.VarChar),
                                    new OracleParameter("STATUS",OracleType.VarChar),
                                    new OracleParameter("OPERATEUSER",OracleType.VarChar),
                                    new OracleParameter("RATECODE",OracleType.VarChar),
                                    new OracleParameter("PROMETHODID",OracleType.VarChar)
                                };

            lmPromMsgParm[0].Value = dbParm.ID;
            lmPromMsgParm[1].Value = dbParm.Title;
            lmPromMsgParm[2].Value = dbParm.Priority;
            lmPromMsgParm[3].Value = dbParm.StartDTime;
            lmPromMsgParm[4].Value = dbParm.EndDTime;
            lmPromMsgParm[5].Value = dbParm.Content;

            if (String.IsNullOrEmpty(strImageID))
            {
                lmPromMsgParm[6].Value = DBNull.Value;
            }
            else
            {
                lmPromMsgParm[6].Value = strImageID;
            }
            lmPromMsgParm[7].Value = dbParm.Status;
            lmPromMsgParm[8].Value = (promotionEntity.LogMessages != null) ? promotionEntity.LogMessages.Userid : "";
            lmPromMsgParm[9].Value = String.IsNullOrEmpty(dbParm.RateCode) ? "" : dbParm.RateCode;
            lmPromMsgParm[10].Value = dbParm.Promethodid;

            InsertLmPromMsgInfo.SqlName = "Promotion";
            InsertLmPromMsgInfo.SqlId = "t_lm_promotionmsg_update";
            InsertLmPromMsgInfo.Parameters = lmPromMsgParm;
            sqlList.Add(InsertLmPromMsgInfo);

            CommandInfo UpdateLmPromMsgDetailDelInfo = new CommandInfo();
            OracleParameter[] lmPromMsgDetailDelParm ={
                                    new OracleParameter("ID",OracleType.Int32)
                                };

            lmPromMsgDetailDelParm[0].Value = dbParm.ID;

            UpdateLmPromMsgDetailDelInfo.SqlName = "Promotion";
            UpdateLmPromMsgDetailDelInfo.SqlId = "t_lm_promotionmsg_detail_update";
            UpdateLmPromMsgDetailDelInfo.Parameters = lmPromMsgDetailDelParm;
            sqlList.Add(UpdateLmPromMsgDetailDelInfo);

            string PromotionCommonKey = String.IsNullOrEmpty(ConfigurationManager.AppSettings["PromotionCommonKey"]) ? "000000" : ConfigurationManager.AppSettings["PromotionCommonKey"].ToString();

            CommandInfo InsertLmPromMsgDetailInfo = new CommandInfo();
            OracleParameter[] lmPromMsgDetailParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("PROTYPE",OracleType.VarChar),
                                    new OracleParameter("COMMONLIST",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("LINKURL",OracleType.VarChar)
                                };

            lmPromMsgDetailParm[0].Value = lmPromMsgParm[0].Value;
            lmPromMsgDetailParm[1].Value = dbParm.Type;

            InsertLmPromMsgDetailInfo.SqlName = "Promotion";

            if ("0".Equals(dbParm.Type))
            {
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_all_insert";

                lmPromMsgDetailParm[2].Value = DBNull.Value;
                lmPromMsgDetailParm[3].Value = DBNull.Value;
            }
            else if ("1".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                lmPromMsgDetailParm[3].Value = DBNull.Value;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_city_insert";
            }
            else if ("2".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                lmPromMsgDetailParm[3].Value = DBNull.Value;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_group_insert";
            }
            else if ("3".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                lmPromMsgDetailParm[3].Value = DBNull.Value;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_hotel_insert";
            }
            else if ("4".Equals(dbParm.Type))
            {
                lmPromMsgDetailParm[2].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                lmPromMsgDetailParm[3].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey : dbParm.HotelID;
                InsertLmPromMsgDetailInfo.SqlId = "t_lm_promotionmsg_detail_room_insert";
            }

            if (String.IsNullOrEmpty(dbParm.LinkUrl))
            {
                lmPromMsgDetailParm[4].Value = DBNull.Value;
            }
            else
            {
                lmPromMsgDetailParm[4].Value = dbParm.LinkUrl;
            }

            InsertLmPromMsgDetailInfo.Parameters = lmPromMsgDetailParm;
            sqlList.Add(InsertLmPromMsgDetailInfo);

            if (dbParm.UserGroupList.Length > 0)
            {
                CommandInfo InsertLmPromMsgUserGroupInfo = new CommandInfo();
                OracleParameter[] lmPromMsgDetailUserGroupParm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("PROTYPE",OracleType.VarChar),
                                    new OracleParameter("USERGROUPLIST",OracleType.VarChar),
                                    new OracleParameter("LINKURL",OracleType.VarChar)
                                };
                lmPromMsgDetailUserGroupParm[0].Value = lmPromMsgParm[0].Value;
                lmPromMsgDetailUserGroupParm[1].Value = "5";
                lmPromMsgDetailUserGroupParm[2].Value = dbParm.UserGroupList;
                if (String.IsNullOrEmpty(dbParm.LinkUrl))
                {
                    lmPromMsgDetailUserGroupParm[3].Value = DBNull.Value;
                }
                else
                {
                    lmPromMsgDetailUserGroupParm[3].Value = dbParm.LinkUrl;
                }
                InsertLmPromMsgUserGroupInfo.SqlName = "Promotion";
                InsertLmPromMsgUserGroupInfo.SqlId = "t_lm_promotionmsg_usergroup_insert";
                InsertLmPromMsgUserGroupInfo.Parameters = lmPromMsgDetailUserGroupParm;
                sqlList.Add(InsertLmPromMsgUserGroupInfo);
            }

            DbManager.ExecuteSqlTran(sqlList);
            SetPromotionData(promotionEntity, strImageID);
            promotionEntity.Result = 1;
            return promotionEntity;
        }

        public static int SetPromotionData(PromotionEntity promotionEntity, string strImageID)
        {
            PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();

            List<CommandInfo> sqlList = new List<CommandInfo>();

            CommandInfo UpdateLmPromInfo = new CommandInfo();
            OracleParameter[] lmPromParm ={
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            lmPromParm[0].Value = dbParm.ID;
            UpdateLmPromInfo.SqlName = "Promotion";
            UpdateLmPromInfo.SqlId = "t_lm_promotion_update";
            UpdateLmPromInfo.Parameters = lmPromParm;
            sqlList.Add(UpdateLmPromInfo);

            if ("1".Equals(dbParm.Status))
            {
                string PromotionCommonKey = String.IsNullOrEmpty(ConfigurationManager.AppSettings["PromotionCommonKey"]) ? "000000" : ConfigurationManager.AppSettings["PromotionCommonKey"].ToString();

                CommandInfo InsertLmPromInfo = new CommandInfo();
                OracleParameter[] lmPromInserParm ={
                                    new OracleParameter("ID",OracleType.VarChar),
                                    new OracleParameter("TITLE",OracleType.VarChar),
                                    new OracleParameter("PRIORITY",OracleType.VarChar),
                                    new OracleParameter("STARTDTIME",OracleType.VarChar),
                                    new OracleParameter("ENDDTIME",OracleType.VarChar),
                                    new OracleParameter("CONTENT",OracleType.VarChar),
                                    new OracleParameter("IMAGEID",OracleType.VarChar),
                                    new OracleParameter("STATUS",OracleType.VarChar),
                                    new OracleParameter("OPERATEUSER",OracleType.VarChar),
                                    new OracleParameter("RATECODE",OracleType.VarChar),
                                    new OracleParameter("PROTYPE",OracleType.VarChar),
                                    new OracleParameter("COMMONLIST",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("USERGROUPLIST",OracleType.VarChar),
                                    new OracleParameter("PROMETHODID",OracleType.VarChar),
                                    new OracleParameter("LINKURL",OracleType.VarChar)
                                };
                lmPromInserParm[0].Value = dbParm.ID;
                lmPromInserParm[1].Value = dbParm.Title;
                lmPromInserParm[2].Value = dbParm.Priority;
                lmPromInserParm[3].Value = dbParm.StartDTime;
                lmPromInserParm[4].Value = dbParm.EndDTime;
                lmPromInserParm[5].Value = dbParm.Content;
                lmPromInserParm[6].Value = strImageID;
                lmPromInserParm[7].Value = dbParm.Status;
                lmPromInserParm[8].Value = (promotionEntity.LogMessages != null) ? promotionEntity.LogMessages.Userid : "";
                lmPromInserParm[9].Value = String.IsNullOrEmpty(dbParm.RateCode) ? "" : dbParm.RateCode;

                lmPromInserParm[10].Value = dbParm.Type;

                if ("0".Equals(dbParm.Type))
                {
                    InsertLmPromInfo.SqlId = "t_lm_promotion_detail_all_insert";
                     
                    lmPromInserParm[11].Value = DBNull.Value;
                    lmPromInserParm[12].Value = DBNull.Value;
                    lmPromInserParm[14].Value = dbParm.Promethodid;
                }
                else if ("1".Equals(dbParm.Type))
                {
                    lmPromInserParm[11].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                    lmPromInserParm[12].Value = DBNull.Value;
                    lmPromInserParm[14].Value = dbParm.Promethodid;
                    InsertLmPromInfo.SqlId = "t_lm_promotion_detail_city_insert";
                }
                else if ("2".Equals(dbParm.Type))
                {
                    lmPromInserParm[11].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                    lmPromInserParm[12].Value = DBNull.Value;
                    lmPromInserParm[14].Value = dbParm.Promethodid;
                    InsertLmPromInfo.SqlId = "t_lm_promotion_detail_group_insert";
                }
                else if ("3".Equals(dbParm.Type))
                {
                    lmPromInserParm[11].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                    lmPromInserParm[12].Value = DBNull.Value;
                    lmPromInserParm[14].Value = dbParm.Promethodid;
                    InsertLmPromInfo.SqlId = "t_lm_promotion_detail_hotel_insert";
                }
                else if ("4".Equals(dbParm.Type))
                {
                    lmPromInserParm[11].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey + "," : dbParm.CommonList;
                    lmPromInserParm[12].Value = ("1".Equals(dbParm.ChkType)) ? PromotionCommonKey : dbParm.HotelID;
                    lmPromInserParm[14].Value = dbParm.Promethodid;
                    InsertLmPromInfo.SqlId = "t_lm_promotion_detail_room_insert";
                }

                if (dbParm.UserGroupList.Length > 0)
                {
                    lmPromInserParm[13].Value = dbParm.UserGroupList.Substring(0, dbParm.UserGroupList.Length - 1);
                }
                else
                {
                    lmPromInserParm[13].Value = DBNull.Value;
                }

                if (String.IsNullOrEmpty(dbParm.LinkUrl))
                {
                    lmPromInserParm[15].Value = DBNull.Value;
                }
                else
                {
                    lmPromInserParm[15].Value = dbParm.LinkUrl;
                }
                InsertLmPromInfo.SqlName = "Promotion";
                InsertLmPromInfo.Parameters = lmPromInserParm;
                sqlList.Add(InsertLmPromInfo);
            }

            DbManager.ExecuteSqlTran(sqlList);
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