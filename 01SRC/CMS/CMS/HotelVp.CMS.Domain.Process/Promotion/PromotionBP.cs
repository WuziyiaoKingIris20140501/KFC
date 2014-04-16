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
    public abstract class PromotionBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.PromotionBP  Method: ";

        public static PromotionEntity CommonSelect(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.CommonSelect(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "CommonSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity GetCityList(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "GetCityList";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.GetCityList(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "GetCityList  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity GetHotelList(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "GetHotelList";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.GetHotelList(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "GetHotelList  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity GetHotelGroupList(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "GetHotelGroupList";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.GetHotelGroupList(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "GetHotelGroupList  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity GetUserGroupList(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "GetUserGroupList";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.GetUserGroupList(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "GetUserGroupList  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetBalanceRoomList(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomList";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.GetBalanceRoomList(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetHotelRoomList(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRoomList";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.GetHotelRoomList(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetHotelRoomListAll(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRoomListAll";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.GetHotelRoomListAll(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRoomListAll  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity PromotioningSelect(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "PromotioningSelect";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.PromotioningSelect(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "PromotioningSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity PromotionMsgSelect(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "PromotionMsgSelect";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.PromotionMsgSelect(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "PromotionMsgSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity Insert(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.Insert(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity Update(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "Update";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                promotionEntity = PromotionDA.Update(promotionEntity);
                if (promotionEntity.Result == 1)
                {
                    PromotionDBEntity dbParm = (promotionEntity.PromotionDBEntity.Count > 0) ? promotionEntity.PromotionDBEntity[0] : new PromotionDBEntity();
                    if ("3".Equals(dbParm.Type))
                    {
                        HotelInfoSA.RefushHotelList((("1".Equals(dbParm.ChkType)) ? "ALL" : dbParm.CommonList.TrimEnd(',')));
                    }
                }
                return promotionEntity;
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity MainSelect(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "MainSelect";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.MainSelect(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "MainSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }

        public static PromotionEntity DetailSelect(PromotionEntity promotionEntity)
        {
            promotionEntity.LogMessages.MsgType = MessageType.INFO;
            promotionEntity.LogMessages.Content = _nameSpaceClass + "DetailSelect";
            LoggerHelper.LogWriter(promotionEntity.LogMessages);

            try
            {
                return PromotionDA.DetailSelect(promotionEntity);
            }
            catch (Exception ex)
            {
                promotionEntity.LogMessages.MsgType = MessageType.ERROR;
                promotionEntity.LogMessages.Content = _nameSpaceClass + "DetailSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(promotionEntity.LogMessages);
                throw ex;
            }
        }
    }
}