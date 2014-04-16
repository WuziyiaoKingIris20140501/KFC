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
    public abstract class ELRelationDA
    {
        public static ELRelationEntity ReviewSelect(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("HVPLP",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            if (String.IsNullOrEmpty(dbParm.HVPLP))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HVPLP;
            }

            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_fx_hvp_hotel_select", parm, (elrelationEntity.PageCurrent - 1) * elrelationEntity.PageSize, elrelationEntity.PageSize, false);
            return elrelationEntity;
        }

        public static ELRelationEntity ReviewSupHotelMappingSelect(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("HVPLP",OracleType.VarChar),
                                    new OracleParameter("HVPRP",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            if (String.IsNullOrEmpty(dbParm.HVPLP))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HVPLP;
            }

            if (String.IsNullOrEmpty(dbParm.HVPRP))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.HVPRP;
            }

            if (String.IsNullOrEmpty(dbParm.Sales))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Sales;
            }


            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_sup_hvp_hotel_select", parm, (elrelationEntity.PageCurrent - 1) * elrelationEntity.PageSize, elrelationEntity.PageSize, false);
            return elrelationEntity;
        }

        public static ELRelationEntity ReviewSupHotelMappingDetail(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_sup_hvp_hotel_detail", false , parm);
            return elrelationEntity;
        }

        public static DataSet SaSupHotelMappingDetail(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            return DbManager.Query("ELRelation", "t_sup_hvp_hotel_detail_sa", false, parm);
        }

        public static ELRelationEntity ReviewSupRoomMappingDetail(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            if (String.IsNullOrEmpty(dbParm.RoomCD))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.RoomCD;
            }

            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_sup_hvp_hotel_mapping_room_detail", false , parm);
            return elrelationEntity;
        }

        public static ELRelationEntity ReviewSupHotelRoomMappingDetail(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_sup_hvp_hotel_room_detail", false , parm);
            return elrelationEntity;
        }

        public static ELRelationEntity ReviewSupHotelMappingSelectCount(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("HVPLP",OracleType.VarChar),
                                    new OracleParameter("HVPRP",OracleType.VarChar),
                                    new OracleParameter("SALES",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }
            if (String.IsNullOrEmpty(dbParm.HVPLP))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HVPLP;
            }

            if (String.IsNullOrEmpty(dbParm.HVPRP))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.HVPRP;
            }

            if (String.IsNullOrEmpty(dbParm.Sales))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Sales;
            }

            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_sup_hvp_hotel_select_count", true, parm);
            return elrelationEntity;
        }

        public static ELRelationEntity ReviewSelectCount(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("HVPLP",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }
            if (String.IsNullOrEmpty(dbParm.HVPLP))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HVPLP;
            }

            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_fx_hvp_hotel_select_count", true, parm);
            return elrelationEntity;
        }

        public static ELRelationEntity UpdateELList(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("ELID",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("OSOURCE",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            if (String.IsNullOrEmpty(dbParm.ELongID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.ELongID;
            }

            if (String.IsNullOrEmpty(dbParm.Source))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.Source;
            }

            if (String.IsNullOrEmpty(dbParm.SupType))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.SupType;
            }
            //if (String.IsNullOrEmpty(dbParm.HOTELNM))
            //{
            //    parm[2].Value = DBNull.Value;
            //}
            //else
            //{
            //    parm[2].Value = dbParm.HOTELNM;
            //}
            DbManager.ExecuteSql("ELRelation", "t_fx_hvp_hotel_update", parm);
            elrelationEntity.Result = 1;
            return elrelationEntity;
        }

        public static bool ChkSUPListInsert(ELRelationEntity elrelationEntity)
        {
            bool bInsert = true;
            DataSet dsResult = new DataSet();
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if ("0".Equals(dbParm.SupType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("OSUPID",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.OSuphid))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.OSuphid;
                }

                dsResult = DbManager.Query("ELRelation", "t_fx_sup_hotel_update_chk", true, parm);
            }
            else
            {
                OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.Source;
                }
                dsResult = DbManager.Query("ELRelation", "t_fx_sup_hotel_create_insert_chk", true, parm);
            }

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                bInsert = false;
            }
            return bInsert;
        }

        public static bool ChkSUPRoomInsert(ELRelationEntity elrelationEntity)
        {
            bool bUpdate = true;
            DataSet dsResult = new DataSet();
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if ("0".Equals(dbParm.SupType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("APPROOM",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("APPHOTELID",OracleType.VarChar),
                                    new OracleParameter("OROWID",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.RoomCD))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.RoomCD;
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(dbParm.ORowID))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.ORowID;
                }

                dsResult = DbManager.Query("ELRelation", "t_fx_sup_hotel_room_update_chk", true, parm);
            }
            else
            {
                OracleParameter[] parm ={
                                    new OracleParameter("APPROOM",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("APPHOTELID",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.RoomCD))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.RoomCD;
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.HVPID;
                }

                dsResult = DbManager.Query("ELRelation", "t_fx_sup_hotel_room_create_chk", true, parm);
            }

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                bUpdate = false;
            }
            return bUpdate;
        }

        public static ELRelationEntity UpdateSUPList(ELRelationEntity elrelationEntity)
        {
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (!ChkSUPListInsert(elrelationEntity))
            {
                elrelationEntity.Result = 2;
                return elrelationEntity;
            }

            if ("0".Equals(dbParm.SupType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("SUPID",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("INUSE",OracleType.VarChar),
                                    new OracleParameter("OSOURCE",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(dbParm.ELongID))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.ELongID;
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.InUse))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.InUse;
                }

                if (String.IsNullOrEmpty(dbParm.OSource))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.OSource;
                }

                DbManager.ExecuteSql("ELRelation", "t_fx_sup_hotel_update", parm);
                if (!dbParm.Source.Equals(dbParm.OSource) || !dbParm.ELongID.Equals(dbParm.OSupId))
                {
                    OracleParameter[] lmparm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("SUPID",OracleType.VarChar),
                                    new OracleParameter("OSOURCE",OracleType.VarChar)
                                };

                    if (String.IsNullOrEmpty(dbParm.HVPID))
                    {
                        lmparm[0].Value = DBNull.Value;
                    }
                    else
                    {
                        lmparm[0].Value = dbParm.HVPID;
                    }

                    if (String.IsNullOrEmpty(dbParm.OSupId))
                    {
                        lmparm[1].Value = DBNull.Value;
                    }
                    else
                    {
                        lmparm[1].Value = dbParm.OSupId;
                    }

                    if (String.IsNullOrEmpty(dbParm.OSource))
                    {
                        lmparm[2].Value = DBNull.Value;
                    }
                    else
                    {
                        lmparm[2].Value = dbParm.OSource;
                    }

                    DbManager.ExecuteSql("ELRelation", "t_fx_sup_hotel_update_for_detail", lmparm);
                }
            }
            else
            {
                OracleParameter[] parm ={
                                    new OracleParameter("HVPID",OracleType.VarChar),
                                    new OracleParameter("SUPID",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("INUSE",OracleType.VarChar)
                                };

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(dbParm.ELongID))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = dbParm.ELongID;
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.InUse))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.InUse;
                }

                DbManager.ExecuteSql("ELRelation", "t_fx_sup_hotel_create", parm);
            }

            elrelationEntity.Result = 1;
            return elrelationEntity;
        }


        public static ELRelationEntity UpdateSUPRoomList(ELRelationEntity elrelationEntity)
        {
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (!ChkSUPRoomInsert(elrelationEntity))
            {
                elrelationEntity.Result = 2;
                return elrelationEntity;
            }

            if ("0".Equals(dbParm.SupType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("APPROOM",OracleType.VarChar),
                                    new OracleParameter("SUPROOM",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("INUSE",OracleType.VarChar),
                                    new OracleParameter("APPHOTELID",OracleType.VarChar),
                                    new OracleParameter("SUPHOTELID",OracleType.VarChar),
                                    new OracleParameter("OSOURCE",OracleType.VarChar),
                                    new OracleParameter("OSUPROOM",OracleType.VarChar),
                                    new OracleParameter("OSUPHOTELID",OracleType.VarChar)
                                };

                string strSupHRID = dbParm.ELongID;
                if (String.IsNullOrEmpty(dbParm.RoomCD))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.RoomCD;
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = strSupHRID.Split('_')[1].ToString();
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.InUse))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.InUse;
                }

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = strSupHRID.Split('_')[0].ToString();
                }

                if (String.IsNullOrEmpty(dbParm.OSource))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.OSource;
                }

                string strOSuphid = dbParm.OSuphid;
                if (String.IsNullOrEmpty(strOSuphid))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = strOSuphid.Split('_')[1].ToString();
                }

                if (String.IsNullOrEmpty(strOSuphid))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = strOSuphid.Split('_')[0].ToString();
                }

                DbManager.ExecuteSql("ELRelation", "t_fx_sup_hotel_room_update", parm);
            }
            else
            {
                OracleParameter[] parm ={
                                    new OracleParameter("APPROOM",OracleType.VarChar),
                                    new OracleParameter("SUPROOM",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("INUSE",OracleType.VarChar),
                                    new OracleParameter("APPHOTELID",OracleType.VarChar),
                                    new OracleParameter("SUPHOTELID",OracleType.VarChar)
                                };

                string strSupHRID = dbParm.ELongID;
                if (String.IsNullOrEmpty(dbParm.RoomCD))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.RoomCD;
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = strSupHRID.Split('_')[1].ToString();
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.InUse))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.InUse;
                }

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = strSupHRID.Split('_')[0].ToString();
                }

                DbManager.ExecuteSql("ELRelation", "t_fx_sup_hotel_room_create", parm);
            }
            UpdateSUPRoomPlanList(elrelationEntity);
            elrelationEntity.Result = 1;
            return elrelationEntity;
        }

        public static ELRelationEntity UpdateSUPRoomPlanList(ELRelationEntity elrelationEntity)
        {
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();
            if ("0".Equals(dbParm.SupType))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("APPROOM",OracleType.VarChar),
                                    new OracleParameter("SUPROOM",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("INUSE",OracleType.VarChar),
                                    new OracleParameter("APPHOTELID",OracleType.VarChar),
                                    new OracleParameter("SUPHOTELID",OracleType.VarChar),
                                    new OracleParameter("OSOURCE",OracleType.VarChar),
                                    new OracleParameter("OSUPROOM",OracleType.VarChar),
                                    new OracleParameter("OSUPHOTELID",OracleType.VarChar),
                                    new OracleParameter("SUPPLANID",OracleType.VarChar)
                                };

                string strSupHRID = dbParm.ELongID;
                if (String.IsNullOrEmpty(dbParm.RoomCD))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.RoomCD;
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = strSupHRID.Split('_')[1].ToString();
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.InUse))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.InUse;
                }

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = strSupHRID.Split('_')[0].ToString();
                }

                if (String.IsNullOrEmpty(dbParm.OSource))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = dbParm.OSource;
                }

                string strOSuphid = dbParm.OSuphid;
                if (String.IsNullOrEmpty(strOSuphid))
                {
                    parm[7].Value = DBNull.Value;
                }
                else
                {
                    parm[7].Value = strOSuphid.Split('_')[1].ToString();
                }

                if (String.IsNullOrEmpty(strOSuphid))
                {
                    parm[8].Value = DBNull.Value;
                }
                else
                {
                    parm[8].Value = strOSuphid.Split('_')[0].ToString();
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[9].Value = DBNull.Value;
                }
                else
                {
                    parm[9].Value = strSupHRID.Split('_')[2].ToString();
                }

                DbManager.ExecuteSql("ELRelation", "t_fx_sup_hotel_room_plan_update", parm);
            }
            else
            {
                OracleParameter[] parm ={
                                    new OracleParameter("APPROOM",OracleType.VarChar),
                                    new OracleParameter("SUPROOM",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("INUSE",OracleType.VarChar),
                                    new OracleParameter("APPHOTELID",OracleType.VarChar),
                                    new OracleParameter("SUPHOTELID",OracleType.VarChar),
                                    new OracleParameter("SUPPLANID",OracleType.VarChar)
                                };

                string strSupHRID = dbParm.ELongID;
                if (String.IsNullOrEmpty(dbParm.RoomCD))
                {
                    parm[0].Value = DBNull.Value;
                }
                else
                {
                    parm[0].Value = dbParm.RoomCD;
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = strSupHRID.Split('_')[1].ToString();
                }

                if (String.IsNullOrEmpty(dbParm.Source))
                {
                    parm[2].Value = DBNull.Value;
                }
                else
                {
                    parm[2].Value = dbParm.Source;
                }

                if (String.IsNullOrEmpty(dbParm.InUse))
                {
                    parm[3].Value = DBNull.Value;
                }
                else
                {
                    parm[3].Value = dbParm.InUse;
                }

                if (String.IsNullOrEmpty(dbParm.HVPID))
                {
                    parm[4].Value = DBNull.Value;
                }
                else
                {
                    parm[4].Value = dbParm.HVPID;
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[5].Value = DBNull.Value;
                }
                else
                {
                    parm[5].Value = strSupHRID.Split('_')[0].ToString();
                }

                if (String.IsNullOrEmpty(strSupHRID))
                {
                    parm[6].Value = DBNull.Value;
                }
                else
                {
                    parm[6].Value = strSupHRID.Split('_')[2].ToString();
                }

                DbManager.ExecuteSql("ELRelation", "t_fx_sup_hotel_room_plan_create", parm);
            }

            elrelationEntity.Result = 1;
            return elrelationEntity;
        }

        //public static ELRelationEntity UserCashDetailListSelect(ELRelationEntity elrelationEntity)
        //{
        //    OracleParameter[] parm ={
        //                            new OracleParameter("USERID",OracleType.VarChar)
        //                        };
        //    ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

        //    if (String.IsNullOrEmpty(dbParm.UserID))
        //    {
        //        parm[0].Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        parm[0].Value = dbParm.UserID;
        //    }

        //    elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_lm_user_cash_detail_select", true, parm);
        //    return elrelationEntity;
        //}

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

        public static ELRelationEntity HVPAreaInsert(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPHOTELID",OracleType.VarChar),
                                    new OracleParameter("AREAID",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            if (String.IsNullOrEmpty(dbParm.ELongID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.ELongID;
            }

            elrelationEntity.Result = DbManager.ExecuteSql("ELRelation", "t_fx_hvp_area_insert", parm);
            return elrelationEntity;
        }

        public static ELRelationEntity HVPAreaDelete(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPHOTELID",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }
             
            elrelationEntity.Result = DbManager.ExecuteSql("ELRelation", "t_fx_hvp_area_delete", parm);
            return elrelationEntity;
        }

        public static ELRelationEntity HVPAreaSelect(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPHOTELID",OracleType.VarChar),
                                    new OracleParameter("CITYNAME",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            if (String.IsNullOrEmpty(dbParm.AmountFrom))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.AmountFrom.ToLower();
            }

            //elrelationEntity.Result = DbManager.ExecuteSql("ELRelation", "t_fx_hvp_area_select", parm);
            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_fx_hvp_area_select", true, parm);
            return elrelationEntity;
        }
        public static ELRelationEntity HVPHotelSelectCircle(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPHOTELID",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }
            elrelationEntity.QueryResult = DbManager.Query("ELRelation", "t_fx_hvp_hotel_select_circle", true, parm);
            return elrelationEntity;
        }
         
        public static ELRelationEntity HVPAreaInsertBase(ELRelationEntity elrelationEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HVPHOTELID",OracleType.VarChar),
                                    new OracleParameter("AREAID",OracleType.VarChar)
                                };
            ELRelationDBEntity dbParm = (elrelationEntity.ELRelationDBEntity.Count > 0) ? elrelationEntity.ELRelationDBEntity[0] : new ELRelationDBEntity();

            if (String.IsNullOrEmpty(dbParm.HVPID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HVPID;
            }

            if (String.IsNullOrEmpty(dbParm.ELongID))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.ELongID;
            }

            elrelationEntity.Result = DbManager.ExecuteSql("ELRelation", "t_fx_hvp_hotel_insertbase", parm);
            return elrelationEntity;
        }
    }
}
