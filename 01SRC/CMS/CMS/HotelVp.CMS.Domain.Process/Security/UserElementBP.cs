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
    public abstract class UserElementBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.RoleBP  Method: ";
        //新增
        public static int Insert(UserElementEntity ueEntity)
        {
            ueEntity.LogMessages.MsgType = MessageType.INFO;
            ueEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(ueEntity.LogMessages);

            try
            {
                return UserElementDA.Insert(ueEntity);
            }
            catch (Exception ex)
            {
                ueEntity.LogMessages.MsgType = MessageType.ERROR;
                ueEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(ueEntity.LogMessages);
                throw ex;
            }
        }

        //删除
        public static int Delete(UserElementEntity ueEntity)
        {
            ueEntity.LogMessages.MsgType = MessageType.INFO;
            ueEntity.LogMessages.Content = _nameSpaceClass + "delete";
            LoggerHelper.LogWriter(ueEntity.LogMessages);
            try
            {
                return UserElementDA.Delete(ueEntity);  //UserElementDA.Insert(ueEntity);
            }
            catch (Exception ex)
            {
                ueEntity.LogMessages.MsgType = MessageType.ERROR;
                ueEntity.LogMessages.Content = _nameSpaceClass + "delete  Error: " + ex.Message;
                LoggerHelper.LogWriter(ueEntity.LogMessages);
                throw ex;
            }
        }

        public static int Update(UserElementEntity ueEntity)
        {
            ueEntity.LogMessages.MsgType = MessageType.INFO;
            ueEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(ueEntity.LogMessages);
            try
            {
                return UserElementDA.Update(ueEntity);  //UserElementDA.Insert(ueEntity);
            }
            catch (Exception ex)
            {
                ueEntity.LogMessages.MsgType = MessageType.ERROR;
                ueEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(ueEntity.LogMessages);
                throw ex;
            }
        }


        public static UserElementEntity SelectInRole(UserElementEntity ueEntity)
        {
            ueEntity.LogMessages.MsgType = MessageType.INFO;
            ueEntity.LogMessages.Content = _nameSpaceClass + "Select in role";
            LoggerHelper.LogWriter(ueEntity.LogMessages);

            try
            {
                return UserElementDA.SelectInRole(ueEntity);
            }
            catch (Exception ex)
            {
                ueEntity.LogMessages.MsgType = MessageType.ERROR;
                ueEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                LoggerHelper.LogWriter(ueEntity.LogMessages);
                throw ex;
            }
        }

        public static UserElementEntity SelectNotInRole(UserElementEntity ueEntity,string searchText)
        {
            ueEntity.LogMessages.MsgType = MessageType.INFO;
            ueEntity.LogMessages.Content = _nameSpaceClass + "Select not in role";
            LoggerHelper.LogWriter(ueEntity.LogMessages);

            try
            {
                return UserElementDA.SelectNotInRole(ueEntity, searchText);
            }
            catch (Exception ex)
            {
                ueEntity.LogMessages.MsgType = MessageType.ERROR;
                ueEntity.LogMessages.Content = _nameSpaceClass + "Select not in role Error: " + ex.Message;
                LoggerHelper.LogWriter(ueEntity.LogMessages);
                throw ex;
            }
        }



    }
}
