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
    public abstract class DbSearchBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.DbSearchBP  Method: ";
        public static DbSearchEntity MenuSelect(DbSearchEntity dbsearchEntity)
        {
            dbsearchEntity.LogMessages.MsgType = MessageType.INFO;
            dbsearchEntity.LogMessages.Content = _nameSpaceClass + "MenuSelect";
            LoggerHelper.LogWriter(dbsearchEntity.LogMessages);

            try
            {
                //return DbSearchDA.MenuSelect(dbsearchEntity);
                return DbSearchDA.MenuSelectFromCMS(dbsearchEntity);
            }
            catch (Exception ex)
            {
                dbsearchEntity.LogMessages.MsgType = MessageType.ERROR;
                dbsearchEntity.LogMessages.Content = _nameSpaceClass + "MenuSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(dbsearchEntity.LogMessages);
                throw ex;
            }
        }

        public static DbSearchEntity SqlMenuSelect(DbSearchEntity dbsearchEntity)
        {
            dbsearchEntity.LogMessages.MsgType = MessageType.INFO;
            dbsearchEntity.LogMessages.Content = _nameSpaceClass + "SqlMenuSelect";
            LoggerHelper.LogWriter(dbsearchEntity.LogMessages);

            try
            {
                //return DbSearchDA.MenuSelect(dbsearchEntity);
                return DbSearchDA.SqlMenuSelectFromCMS(dbsearchEntity);
            }
            catch (Exception ex)
            {
                dbsearchEntity.LogMessages.MsgType = MessageType.ERROR;
                dbsearchEntity.LogMessages.Content = _nameSpaceClass + "SqlMenuSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(dbsearchEntity.LogMessages);
                throw ex;
            }
        }

        public static DbSearchEntity ItemSelect(DbSearchEntity dbsearchEntity)
        {
            dbsearchEntity.LogMessages.MsgType = MessageType.INFO;
            dbsearchEntity.LogMessages.Content = _nameSpaceClass + "ItemSelect";
            LoggerHelper.LogWriter(dbsearchEntity.LogMessages);

            try
            {
                return DbSearchDA.ItemSelect(dbsearchEntity);
            }
            catch (Exception ex)
            {
                dbsearchEntity.LogMessages.MsgType = MessageType.ERROR;
                dbsearchEntity.LogMessages.Content = _nameSpaceClass + "ItemSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(dbsearchEntity.LogMessages);
                throw ex;
            }
        }

        public static DbSearchEntity ItemSqlSelect(DbSearchEntity dbsearchEntity)
        {
            dbsearchEntity.LogMessages.MsgType = MessageType.INFO;
            dbsearchEntity.LogMessages.Content = _nameSpaceClass + "ItemSqlSelect";
            LoggerHelper.LogWriter(dbsearchEntity.LogMessages);

            try
            {
                return DbSearchDA.ItemSqlSelect(dbsearchEntity);
            }
            catch (Exception ex)
            {
                dbsearchEntity.LogMessages.MsgType = MessageType.ERROR;
                dbsearchEntity.LogMessages.Content = _nameSpaceClass + "ItemSqlSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(dbsearchEntity.LogMessages);
                throw ex;
            }
        }

        public static DbSearchEntity TableColumsSelect(DbSearchEntity dbsearchEntity)
        {
            dbsearchEntity.LogMessages.MsgType = MessageType.INFO;
            dbsearchEntity.LogMessages.Content = _nameSpaceClass + "TableColumsSelect";
            LoggerHelper.LogWriter(dbsearchEntity.LogMessages);

            try
            {
                return DbSearchDA.TableColumsSelect(dbsearchEntity);
            }
            catch (Exception ex)
            {
                dbsearchEntity.LogMessages.MsgType = MessageType.ERROR;
                dbsearchEntity.LogMessages.Content = _nameSpaceClass + "TableColumsSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(dbsearchEntity.LogMessages);
                throw ex;
            }
        }

        public static DbSearchEntity TableSqlColumsSelect(DbSearchEntity dbsearchEntity)
        {
            dbsearchEntity.LogMessages.MsgType = MessageType.INFO;
            dbsearchEntity.LogMessages.Content = _nameSpaceClass + "TableSqlColumsSelect";
            LoggerHelper.LogWriter(dbsearchEntity.LogMessages);

            try
            {
                return DbSearchDA.TableSqlColumsSelect(dbsearchEntity);
            }
            catch (Exception ex)
            {
                dbsearchEntity.LogMessages.MsgType = MessageType.ERROR;
                dbsearchEntity.LogMessages.Content = _nameSpaceClass + "TableSqlColumsSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(dbsearchEntity.LogMessages);
                throw ex;
            }
        }
    }
}