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
    public abstract class UserElementDA
    {
        public static int Insert(UserElementEntity ueEntity)
        {
            if (ueEntity.UserElementDBEntity.Count == 0)
            {
                return 0;
            }

            //if (roleEntity.LogMessages == null)
            //{
            //    return 0;
            //}

            //if (CheckInsert(roleEntity) > 0)
            //{
            //    return 2;
            //}

            UserElementDBEntity dbParm = (ueEntity.UserElementDBEntity.Count > 0) ? ueEntity.UserElementDBEntity[0] : new UserElementDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("InsertToUserElement");

            cmd.SetParameterValue("@Role_ID", dbParm.RoleID);
            cmd.SetParameterValue("@User_Account", dbParm.UserAccount);
            cmd.SetParameterValue("@UE_Type", dbParm.UEType);
            cmd.SetParameterValue("@UE_Creator", dbParm.UECreator);

            int intCount = cmd.ExecuteNonQuery();

            return intCount;
        }

        public static int Delete(UserElementEntity ueEntity)
        {
            try
            {
                UserElementDBEntity dbParm = (ueEntity.UserElementDBEntity.Count > 0) ? ueEntity.UserElementDBEntity[0] : new UserElementDBEntity();
                DataCommand cmd = DataCommandManager.GetDataCommand("DeleteUserElementByUserAccount");//删除相应的UserAccount在Role中的信息。
                cmd.SetParameterValue("@Role_ID", dbParm.RoleID);
                cmd.SetParameterValue("@User_Account", dbParm.UserAccount);
                int intCount = cmd.ExecuteNonQuery();
                return intCount;
            }
            catch
            {
                return 0;
            }

        }

        public static int Update(UserElementEntity ueEntity)
        {
            try
            {
                UserElementDBEntity dbParm = (ueEntity.UserElementDBEntity.Count > 0) ? ueEntity.UserElementDBEntity[0] : new UserElementDBEntity();
                DataCommand cmd = DataCommandManager.GetDataCommand("UpdateUserElementByUserAccount");//删除相应的UserAccount在Role中的信息。
                cmd.SetParameterValue("@Role_ID", dbParm.RoleID);
                cmd.SetParameterValue("@User_Account", dbParm.UserAccount);
                int intCount = cmd.ExecuteNonQuery();
                return intCount;
            }
            catch
            {
                return 0;
            }

        }

        //选择在当前角色的用户
        //public static DataSet SelectInRole()
        //{
        //    DataCommand cmd = DataCommandManager.GetDataCommand("SelectUserInRoleList");
        //    DataSet ds = cmd.ExecuteDataSet();
        //    return ds;
        //}

        //public static DataSet SelectNotInRole(string roleID, string searchText)
        //{
        //    DataCommand cmd = DataCommandManager.GetDataCommand("SelectUserNotInRoleList");
        //    cmd.SetParameterValue("@roleid", roleID);
        //    cmd.SetParameterValue("@SearchText", searchText);
        //    DataSet ds = cmd.ExecuteDataSet();
        //    return ds;
        //}

        public static UserElementEntity SelectInRole(UserElementEntity ueEntity)
        {
            UserElementDBEntity dbParm = (ueEntity.UserElementDBEntity.Count > 0) ? ueEntity.UserElementDBEntity[0] : new UserElementDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("SelectUserInRoleList");
            cmd.SetParameterValue("@roleid", dbParm.RoleID);
            ueEntity.QueryResult = cmd.ExecuteDataSet();
            return ueEntity;
        }

        public static UserElementEntity SelectNotInRole(UserElementEntity ueEntity,string searchText)
        {
            UserElementDBEntity dbParm = (ueEntity.UserElementDBEntity.Count > 0) ? ueEntity.UserElementDBEntity[0] : new UserElementDBEntity();

            DataCommand cmd = DataCommandManager.GetDataCommand("SelectUserNotInRoleList");
            cmd.SetParameterValue("@roleid", dbParm.RoleID);
            cmd.SetParameterValue("@SearchText", searchText);
           
            ueEntity.QueryResult = cmd.ExecuteDataSet();
            return ueEntity;
        }


    }
}
