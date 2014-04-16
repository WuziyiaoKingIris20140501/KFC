using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;

namespace HotelVp.CMS.Domain.Process
{
     public abstract class RoleBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.RoleBP  Method: ";

        //public static RegChannelEntity Select(RegChannelEntity regChannelEntity)
        //{
        //    regChannelEntity.LogMessages.MsgType = MessageType.INFO;
        //    regChannelEntity.LogMessages.Content = _nameSpaceClass + "Select";
        //    LoggerHelper.LogWriter(regChannelEntity.LogMessages);

        //    try
        //    {
        //        return RegChannelDA.Select(regChannelEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        regChannelEntity.LogMessages.MsgType = MessageType.ERROR;
        //        regChannelEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
        //        throw ex;
        //    }
        //}

         //新增
        public static int Insert(RoleEntity roleEntity)
        {
            //roleEntity.LogMessages.MsgType = MessageType.INFO;
            //roleEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            //LoggerHelper.LogWriter(roleEntity.LogMessages);

            try
            {
                return RoleDA.Insert(roleEntity);   //RegChannelDA.Insert(regChannelEntity);
            }
            catch (Exception ex)
            {
                //roleEntity.LogMessages.MsgType = MessageType.ERROR;
                //roleEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                throw ex;
            }
        }

         //修改
        public static int Update(RoleEntity roleEntity)
        {
            //roleEntity.LogMessages.MsgType = MessageType.INFO;
            //roleEntity.LogMessages.Content = _nameSpaceClass + "Update";
            //LoggerHelper.LogWriter(roleEntity.LogMessages);
            try
            {
                return RoleDA.Update(roleEntity);   //RegChannelDA.Insert(regChannelEntity);
            }
            catch (Exception ex)
            {
                //roleEntity.LogMessages.MsgType = MessageType.ERROR;
                //roleEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                throw ex;
            }
        }

         //删除
        public static int Delete(RoleEntity roleEntity)
        {
            //roleEntity.LogMessages.MsgType = MessageType.INFO;
            //roleEntity.LogMessages.Content = _nameSpaceClass + "delete";
            //LoggerHelper.LogWriter(roleEntity.LogMessages);
            try
            {
                return RoleDA.Delete(roleEntity);   //RegChannelDA.Insert(regChannelEntity);
            }
            catch (Exception ex)
            {
                //roleEntity.LogMessages.MsgType = MessageType.ERROR;
                //roleEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                throw ex;
            }
        }
    }
}
