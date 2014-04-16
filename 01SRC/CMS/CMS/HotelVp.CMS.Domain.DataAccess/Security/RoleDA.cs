using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class RoleDA
    {
        
        public static int Insert(RoleEntity roleEntity)
        {
            if (roleEntity.RoleDBEntity.Count == 0)
            {
                return 0;
            }

            //if (roleEntity.LogMessages == null)
            //{
            //    return 0;
            //}

            if (CheckInsert(roleEntity) > 0)
            {
                return 2;
            }

            RoleDBEntity dbParm = (roleEntity.RoleDBEntity.Count > 0) ? roleEntity.RoleDBEntity[0] : new RoleDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("InsertRole");

            cmd.SetParameterValue("@Role_Name", dbParm.RoleName);
            cmd.SetParameterValue("@Create_Time", dbParm.CreateTime);
            cmd.SetParameterValue("@Update_Time", dbParm.UpdateTime);
            cmd.SetParameterValue("@Role_Creator", dbParm.RoleCreator);
            cmd.SetParameterValue("@IS_AD", dbParm.IsAD);

            int intCount = cmd.ExecuteNonQuery();

            return intCount;
        }

        public static int CheckInsert(RoleEntity roleEntity)
        {           
            RoleDBEntity dbParm = (roleEntity.RoleDBEntity.Count > 0) ? roleEntity.RoleDBEntity[0] : new RoleDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectByRoleName");
            cmd.SetParameterValue("@Role_Name", dbParm.RoleName);
            cmd.SetParameterValue("@IS_AD", dbParm.IsAD);

            System.Data.DataSet dsResult = cmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public static int Update(RoleEntity roleEntity)
        {
            if (roleEntity.RoleDBEntity.Count == 0)
            {
                return 0;
            }

            //if (roleEntity.LogMessages == null)
            //{
            //    return 0;
            //}

            if (CheckUpdate(roleEntity) > 0)
            {
                return 2;
            }
            
            RoleDBEntity dbParm = (roleEntity.RoleDBEntity.Count > 0) ? roleEntity.RoleDBEntity[0] : new RoleDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateRoleByID");
            cmd.SetParameterValue("@Role_Name", dbParm.RoleName);            
            cmd.SetParameterValue("@Update_Time", dbParm.UpdateTime);
            cmd.SetParameterValue("@Role_ID", dbParm.RoleID);
            cmd.SetParameterValue("@IS_AD", dbParm.IsAD);
            int intCount = cmd.ExecuteNonQuery();
            return intCount;           
        }

        public static int CheckUpdate(RoleEntity roleEntity)
        {

            RoleDBEntity dbParm = (roleEntity.RoleDBEntity.Count > 0) ? roleEntity.RoleDBEntity[0] : new RoleDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("SelectRoleNameByNotEqualID");//判断新改的这个角色名称是否已经存在。
            cmd.SetParameterValue("@Role_Name", dbParm.RoleName);
            cmd.SetParameterValue("@IS_AD", dbParm.IsAD);
            cmd.SetParameterValue("@Role_ID", dbParm.RoleID);
            System.Data.DataSet dsResult = cmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return 1;            
            }           
            return 0;            
        }

        public static int Delete(RoleEntity roleEntity)
        {
            try
            {
                RoleDBEntity dbParm = (roleEntity.RoleDBEntity.Count > 0) ? roleEntity.RoleDBEntity[0] : new RoleDBEntity();
                DataCommand cmd = DataCommandManager.GetDataCommand("DeleteRoleByID");//根据RoleID进行删除            
                cmd.SetParameterValue("@Role_ID", dbParm.RoleID);
                int intCount = cmd.ExecuteNonQuery();
                return intCount;
            }
            catch {
                return 0;
            }
     
        }
    }
}
