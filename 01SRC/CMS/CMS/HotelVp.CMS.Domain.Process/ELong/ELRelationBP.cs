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
using HotelVp.CMS.Domain.ServiceAdapter;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class ELRelationBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.ELRelationBP  Method: ";

        public static ELRelationEntity ReviewSelect(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.ReviewSelect(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity ReviewSupHotelMappingSelect(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupHotelMappingSelect";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.ReviewSupHotelMappingSelect(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupHotelMappingSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity ReviewSupHotelMappingDetail(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupHotelMappingDetail";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.ReviewSupHotelMappingDetail(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupHotelMappingDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity SaSupHotelMappingDetail(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "SaSupHotelMappingDetail";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                DataSet dsResult = new DataSet();
                DataTable dtMaster = new DataTable();
                DataTable dtDetail = new DataTable();

                dtMaster = ELRelationDA.SaSupHotelMappingDetail(ELRelationEntity).Tables[0].Copy();
                dtMaster.TableName = "Master";
                DataSet dstemp = HotelInfoSA.GetSupRoomList(dtMaster);
                dtDetail = dstemp.Tables[0].Copy();
                dtDetail.TableName = "Detail";

                dsResult.Tables.Add(dtMaster);
                dsResult.Tables.Add(dtDetail);
                ELRelationEntity.QueryResult = dsResult;
                return ELRelationEntity;
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "SaSupHotelMappingDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity ReviewSupRoomMappingDetail(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupRoomMappingDetail";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.ReviewSupRoomMappingDetail(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupRoomMappingDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity ReviewSupHotelRoomMappingDetail(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupHotelRoomMappingDetail";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.ReviewSupHotelRoomMappingDetail(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupHotelRoomMappingDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity ReviewSupHotelMappingSelectCount(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupHotelMappingSelectCount";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.ReviewSupHotelMappingSelectCount(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSupHotelMappingSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity ReviewSelectCount(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectCount";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.ReviewSelectCount(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "ReviewSelectCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity UpdateELList(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "UpdateELList";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.UpdateELList(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "UpdateELList  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity UpdateSUPList(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "UpdateSUPList";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.UpdateSUPList(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "UpdateSUPList  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity UpdateSUPRoomList(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "UpdateSUPRoomList";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.UpdateSUPRoomList(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "UpdateSUPRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity HVPAreaInsert(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPAreaInsert";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.HVPAreaInsert(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPAreaInsert  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity HVPAreaDelete(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPAreaDelete";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.HVPAreaDelete(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPAreaDelete  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity HVPAreaSelect(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPAreaSelect";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.HVPAreaSelect(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPAreaSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }

        public static ELRelationEntity HVPHotelSelectCircle(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPHotelSelectCircle";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.HVPHotelSelectCircle(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPHotelSelectCircle  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        } 

        public static ELRelationEntity HVPAreaInsertBase(ELRelationEntity ELRelationEntity)
        {
            ELRelationEntity.LogMessages.MsgType = MessageType.INFO;
            ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPAreaInsertBase";
            LoggerHelper.LogWriter(ELRelationEntity.LogMessages);

            try
            {
                return ELRelationDA.HVPAreaInsertBase(ELRelationEntity);
            }
            catch (Exception ex)
            {
                ELRelationEntity.LogMessages.MsgType = MessageType.ERROR;
                ELRelationEntity.LogMessages.Content = _nameSpaceClass + "HVPAreaInsertBase  Error: " + ex.Message;
                LoggerHelper.LogWriter(ELRelationEntity.LogMessages);
                throw ex;
            }
        }
    }
}