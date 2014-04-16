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

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class HotelsConsultRoomManagerDA
    {
        /// <summary>
        /// 获取所有的销售人员（过滤计划需要）
        /// </summary>
        /// <returns></returns>
        public static DataSet GetSalesManagerList()
        {
            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SalesRoleID"])) ? "5" : ConfigurationManager.AppSettings["SalesRoleID"].ToString().Trim();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesMangeListSelect");
            cmd.SetParameterValue("@RoleID", RoleID);
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult;
        }

        /// <summary>
        /// 获取指定日期(每天)所有的询房人员--SQL
        /// </summary>
        /// <param name="hotelInfoEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetConsultPeopleByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetConsultPeopleByManager");
            cmd.SetParameterValue("@StartDate", dbParm.SDate);
            cmd.SetParameterValue("@EndDate", dbParm.EDate);
            hotelsConsultRoomManagerEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 获取每个房控人员 所对应的（酒店 城市 商圈）酒店数
        /// </summary>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetOracleHotelsByConsultPeopleByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CHECKUSERNAME",OracleType.VarChar)
                                };
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();
            if (String.IsNullOrEmpty(dbParm.CheckUserName))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.CheckUserName;
            }

            hotelsConsultRoomManagerEntity.QueryResult = DbManager.Query("HotelsConsultRoomManager", "get_oracle_hotels_byconsultprople", true, parm);
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 获取指定日期(每天)所有人已经询房的酒店列表-SQL
        /// </summary>
        /// <param name="hotelInfoEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetEXDConsultHotelCountLogsByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();
            
            DataCommand cmd = DataCommandManager.GetDataCommand("GetEXDConsultHotelCountLogsByManager");
            cmd.SetParameterValue("@StartDate", dbParm.SDate);
            cmd.SetParameterValue("@EndDate", dbParm.EDate);
            hotelsConsultRoomManagerEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 根据 商圈 城市 来获取酒店该名下的酒店列表
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetHotelsByKeysByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("KEYID",OracleType.VarChar)
                                };
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();
            if (String.IsNullOrEmpty(dbParm.KeyID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.KeyID;
            }

            hotelsConsultRoomManagerEntity.QueryResult = DbManager.Query("HotelsConsultRoomManager", "get_oracle_hotelsdetail_bykeys", true, parm);
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 获取当天已询酒店列表--All  酒店Count
        /// </summary>
        /// <param name="hotelsConsultRoomManagerEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetConsultHotelsByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetConsultHotelsByManager");
            cmd.SetParameterValue("@StartDate", dbParm.SDate);
            cmd.SetParameterValue("@EndDate", dbParm.EDate);
            hotelsConsultRoomManagerEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// All下面 所有的酒店列表
        /// </summary>
        /// <param name="appContentEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetConsultManagerAllCitysByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            hotelsConsultRoomManagerEntity.QueryResult = DbManager.Query("HotelsConsultRoomManager", "get_oracle_consultallcitys_bymanager", true);
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 获取历史记录中  每个询房人员对应的酒店数（已询数）
        /// </summary>
        /// <param name="hotelsConsultRoomManagerEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetConsultRoomByTimeManagerHistory(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetConsultRoomByTimeManagerHistory");
            cmd.SetParameterValue("@CHECKTIME", dbParm.SDate);
            hotelsConsultRoomManagerEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 获取历史记录中  每个询房人员对应的酒店详细（已询酒店列表）
        /// </summary>
        /// <param name="hotelsConsultRoomManagerEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetConsultRoomByTimeAndUserManagerHistory(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetConsultRoomByTimeAndUserManagerHistory");
            cmd.SetParameterValue("@CHECKTIME", dbParm.SDate);
            cmd.SetParameterValue("@CHECKUSERNAME", dbParm.CheckUserName);
            hotelsConsultRoomManagerEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 获取历史记录中  All  所有城市的酒店列表数
        /// </summary>
        /// <param name="hotelsConsultRoomManagerEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetAllCityByTimeManagerHistory(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetAllCityByTimeManagerHistory");
            cmd.SetParameterValue("@CHECKTIME", dbParm.SDate);
            hotelsConsultRoomManagerEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 获取指定询房人员的已询酒店列表-SQL
        /// </summary>
        /// <param name="hotelInfoEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetConsultHotelsBySales(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetConsultHotelsBySales");
            cmd.SetParameterValue("@SalesID", dbParm.CheckUserName);
            cmd.SetParameterValue("@StartDate", dbParm.SDate);
            cmd.SetParameterValue("@EndDate", dbParm.EDate);
            hotelsConsultRoomManagerEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelsConsultRoomManagerEntity;
        }

        /// <summary>
        /// 获取每个人18点后更新的酒店数
        /// </summary>
        /// <param name="hotelInfoEntity"></param>
        /// <returns></returns>
        public static HotelsConsultRoomManagerEntity GetCheck18EXDConsultHotelCountLogsByManager(HotelsConsultRoomManagerEntity hotelsConsultRoomManagerEntity)
        {
            HotelsConsultRoomManagerDBEntity dbParm = (hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity.Count > 0) ? hotelsConsultRoomManagerEntity.HotelsConsultRoomManagerDBEntity[0] : new HotelsConsultRoomManagerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetCheck18EXDConsultHotelCountLogsByManager");
            cmd.SetParameterValue("@StartDate", dbParm.SDate);
            cmd.SetParameterValue("@EndDate", dbParm.EDate);
            hotelsConsultRoomManagerEntity.QueryResult = cmd.ExecuteDataSet();
            return hotelsConsultRoomManagerEntity;
        }
    }
}
