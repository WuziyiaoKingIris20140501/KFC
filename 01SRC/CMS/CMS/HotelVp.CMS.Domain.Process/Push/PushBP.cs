using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class PushBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.PushBP  Method: ";

        public static PushEntity GetUserGroupList(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "GetUserGroupList";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.GetUserGroupList(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "GetUserGroupList  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity Insert(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.Insert(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity PushInsert(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "PushInsert";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.PushInsert(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "PushInsert  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity Update(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.Update(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity PushUpdate(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "PushUpdate";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.PushUpdate(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "PushUpdate  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity JSendPushMsg(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "JSendPushMsg";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                PushEntity.Result = 1;
                PushEntity.PushDBEntity[0].Status = "1";
                PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
                PushEntity = PushDA.UpdateSendPushStatus(PushEntity);
                if (2 == PushEntity.Result)
                {
                    return PushEntity;
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                try
                {
                    process.StartInfo.FileName = ConfigurationManager.AppSettings["JPushMsgJob"].ToString();//文件名必须加后缀。 
                    process.StartInfo.Arguments = dbParm.ID;
                    process.Start();
                }
                catch (Exception exU)
                {
                    PushEntity.Result = 3;
                    PushEntity.PushDBEntity[0].Status = "0";
                    PushEntity = PushDA.UpdateSendPushStatus(PushEntity);
                }
                finally
                {
                    if (!process.HasExited)
                    {
                        process.Close();
                        process.Dispose();
                    }
                }
                return PushEntity;
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "JSendPushMsg  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SendPushMsg(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SendPushMsg";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                PushEntity.Result = 1;
                PushEntity.PushDBEntity[0].Status = "1";
                PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
                PushEntity = PushDA.UpdateSendPushStatus(PushEntity);
                if (2 == PushEntity.Result)
                {
                    return PushEntity;
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                try
                {
                    process.StartInfo.FileName = ConfigurationManager.AppSettings["PushMsgJob"].ToString();//文件名必须加后缀。 
                    process.StartInfo.Arguments = dbParm.ID;
                    process.Start();
                }
                catch (Exception exU)
                {
                    PushEntity.Result = 3;
                    PushEntity.PushDBEntity[0].Status = "0";
                    PushEntity = PushDA.UpdateSendPushStatus(PushEntity);
                }
                finally
                {
                    if (!process.HasExited)
                    {
                        process.Close();
                        process.Dispose();
                    }
                }
                return PushEntity;
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SendPushMsg  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SendPushInfoMsg(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SendPushInfoMsg";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                PushEntity.Result = 1;
                PushEntity.PushDBEntity[0].Status = "1";
                PushDBEntity dbParm = (PushEntity.PushDBEntity.Count > 0) ? PushEntity.PushDBEntity[0] : new PushDBEntity();
                PushEntity = PushDA.UpdateSendPushInfoStatus(PushEntity);
                if (2 == PushEntity.Result)
                {
                    return PushEntity;
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                try
                {
                    process.StartInfo.FileName = ConfigurationManager.AppSettings["PushInfoMsgJob"].ToString();//文件名必须加后缀。 
                    process.StartInfo.Arguments = dbParm.ID;
                    process.Start();
                }
                catch (Exception exU)
                {
                    PushEntity.Result = 3;
                    PushEntity.PushDBEntity[0].Status = "0";
                    PushEntity = PushDA.UpdateSendPushInfoStatus(PushEntity);
                }
                finally
                {
                    if (!process.HasExited)
                    {
                        process.Close();
                        process.Dispose();
                    }
                }
                return PushEntity;
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SendPushInfoMsg  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectPushAllUsersCount(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushAllUsersCount";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectPushAllUsersCount(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushAllUsersCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectPushInfoAllUsersCount(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushInfoAllUsersCount";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectPushInfoAllUsersCount(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushInfoAllUsersCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectPushSuccCount(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushSuccCount";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectPushSuccCount(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushSuccCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectPushInfoSuccCount(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushInfoSuccCount";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectPushInfoSuccCount(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushInfoSuccCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectActionDateTime(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectActionDateTime";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectActionDateTime(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectActionDateTime  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectInfoActionDateTime(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectInfoActionDateTime";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectInfoActionDateTime(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectInfoActionDateTime  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectPushUserGroupCount(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushUserGroupCount";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectPushUserGroupCount(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushUserGroupCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectPushInfoUserGroupCount(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushInfoUserGroupCount";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectPushInfoUserGroupCount(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushInfoUserGroupCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity PushHistorySelect(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "PushHistorySelect";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.PushHistorySelect(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "PushHistorySelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity PushInfoHistorySelect(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "PushInfoHistorySelect";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.PushInfoHistorySelect(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "PushInfoHistorySelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity PushHistoryListSelect(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "PushHistoryListSelect";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.PushHistoryListSelect(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "PushHistoryListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity PushInfoHistoryListSelect(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "PushInfoHistoryListSelect";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.PushInfoHistoryListSelect(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "PushInfoHistoryListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectPushActionHistoryList(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushActionHistoryList";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectPushActionHistoryList(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushActionHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity SelectPushInfoActionHistoryList(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushInfoActionHistoryList";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.SelectPushInfoActionHistoryList(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "SelectPushInfoActionHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity ExportPushActionHistoryList(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "ExportPushActionHistoryList";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.ExportPushActionHistoryList(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "ExportPushActionHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }

        public static PushEntity ExportPushInfoActionHistoryList(PushEntity PushEntity)
        {
            PushEntity.LogMessages.MsgType = MessageType.INFO;
            PushEntity.LogMessages.Content = _nameSpaceClass + "ExportPushInfoActionHistoryList";
            LoggerHelper.LogWriter(PushEntity.LogMessages);

            try
            {
                return PushDA.ExportPushInfoActionHistoryList(PushEntity);
            }
            catch (Exception ex)
            {
                PushEntity.LogMessages.MsgType = MessageType.ERROR;
                PushEntity.LogMessages.Content = _nameSpaceClass + "ExportPushInfoActionHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(PushEntity.LogMessages);
                throw ex;
            }
        }
    }
}