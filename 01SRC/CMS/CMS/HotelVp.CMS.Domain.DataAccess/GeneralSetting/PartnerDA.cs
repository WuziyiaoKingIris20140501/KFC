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
    public abstract class PartnerDA
    {
        public static PartnerEntity Select(PartnerEntity partnerEntity)
        {
            PartnerDBEntity dbParm = (partnerEntity.PartnerDBEntity.Count > 0) ? partnerEntity.PartnerDBEntity[0] : new PartnerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetPartnerLinkList");

            if (String.IsNullOrEmpty(dbParm.PartnerID))
            {
                cmd.SetParameterValue("@PartnerID", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@PartnerID", "%" + dbParm.PartnerID + "%");
            }

            if (String.IsNullOrEmpty(dbParm.StartDTime))
            {
                cmd.SetParameterValue("@StartDTime", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            }

            if (String.IsNullOrEmpty(dbParm.EndDTime))
            {
                cmd.SetParameterValue("@EndDTime", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);
            }

            if (String.IsNullOrEmpty(dbParm.WapLink))
            {
                cmd.SetParameterValue("@WapLink", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@WapLink", dbParm.WapLink);
            }

            partnerEntity.QueryResult = cmd.ExecuteDataSet();
            return partnerEntity;
        }

        public static PartnerEntity ChartSelect(PartnerEntity partnerEntity)
        {
            PartnerDBEntity dbParm = (partnerEntity.PartnerDBEntity.Count > 0) ? partnerEntity.PartnerDBEntity[0] : new PartnerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("GetPartnerLinkChartList");
            cmd.SetParameterValue("@SysID", dbParm.SysID);
            if (String.IsNullOrEmpty(dbParm.StartDTime))
            {
                cmd.SetParameterValue("@StartDTime", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@StartDTime", dbParm.StartDTime);
            }

            if (String.IsNullOrEmpty(dbParm.EndDTime))
            {
                cmd.SetParameterValue("@EndDTime", DBNull.Value);
            }
            else
            {
                cmd.SetParameterValue("@EndDTime", dbParm.EndDTime);
            }
            partnerEntity.QueryResult = cmd.ExecuteDataSet();
            return partnerEntity;
        }

        public static int CheckInsert(PartnerEntity partnerEntity)
        {
            PartnerDBEntity dbParm = (partnerEntity.PartnerDBEntity.Count > 0) ? partnerEntity.PartnerDBEntity[0] : new PartnerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("ChkInsertPartnerLink");
            cmd.SetParameterValue("@PartnerID", dbParm.PartnerID);
            cmd.SetParameterValue("@PartnerLink", dbParm.PartnerLink);
            DataSet dsResult = cmd.ExecuteDataSet();

             if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int CheckUpdate(PartnerEntity partnerEntity)
        {
            PartnerDBEntity dbParm = (partnerEntity.PartnerDBEntity.Count > 0) ? partnerEntity.PartnerDBEntity[0] : new PartnerDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("ChkUpdatePartnerLink");
            cmd.SetParameterValue("@SysID", dbParm.SysID);
            cmd.SetParameterValue("@PartnerID", dbParm.PartnerID);
            cmd.SetParameterValue("@PartnerLink", dbParm.PartnerLink);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int Insert(PartnerEntity partnerEntity)
        {
            if (partnerEntity.PartnerDBEntity.Count == 0)
            {
                return 0;
            }

            if (partnerEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckInsert(partnerEntity) > 0)
            {
                return 2;
            }

            PartnerDBEntity dbParm = (partnerEntity.PartnerDBEntity.Count > 0) ? partnerEntity.PartnerDBEntity[0] : new PartnerDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertPartnerLink");
            cmd.SetParameterValue("@PartnerID", dbParm.PartnerID);
            cmd.SetParameterValue("@PartnerLink", dbParm.PartnerLink);
            cmd.SetParameterValue("@PartnerTitle", dbParm.PartnerTitle);
            cmd.SetParameterValue("@Cost", String.IsNullOrEmpty(dbParm.PartnerCost) ? "0" : dbParm.PartnerCost);
            cmd.SetParameterValue("@Remark", dbParm.Remark);
            cmd.SetParameterValue("@UserID", partnerEntity.LogMessages.Username);
            return cmd.ExecuteNonQuery();
        }

        public static int Update(PartnerEntity partnerEntity)
        {
            if (partnerEntity.PartnerDBEntity.Count == 0)
            {
                return 0;
            }

            if (partnerEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckUpdate(partnerEntity) > 0)
            {
                return 2;
            }

            PartnerDBEntity dbParm = (partnerEntity.PartnerDBEntity.Count > 0) ? partnerEntity.PartnerDBEntity[0] : new PartnerDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdatePartnerLink");
            cmd.SetParameterValue("@SysID", dbParm.SysID);
            cmd.SetParameterValue("@PartnerID", dbParm.PartnerID);
            cmd.SetParameterValue("@PartnerLink", dbParm.PartnerLink);
            cmd.SetParameterValue("@Remark", dbParm.Remark);
            cmd.SetParameterValue("@UserID", partnerEntity.LogMessages.Username);
            cmd.SetParameterValue("@PartnerTitle", dbParm.PartnerTitle);
            cmd.SetParameterValue("@Cost", String.IsNullOrEmpty(dbParm.PartnerCost) ? "0" : dbParm.PartnerCost);
            return cmd.ExecuteNonQuery();
        }
    }
}