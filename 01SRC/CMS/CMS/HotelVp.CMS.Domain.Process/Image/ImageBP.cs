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
using HotelVp.CMS.Domain.ServiceAdapter;

namespace HotelVp.CMS.Domain.Process
{
    public abstract class ImageBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.ImageBP  Method: ";

        public static DataTable GetImageByHotelID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "GetImageByHotelID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.GetImageByHotelID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "GetImageByHotelID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }


        public static int InsertImage(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "InsertImage";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.InsertImage(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "InsertImage  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeleteImage(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "DeleteImage";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.deleteImage(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "DeleteImage  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }


        public static int TSupImageCheckInsert(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "TSupImageCheckInsert";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.CheckInsert(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "TSupImageCheckInsert  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }

        public static int UpdateTRoomByHotelID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "UpdateTRoomByHotelID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.UpdateTRoomByHotelID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "UpdateTRoomByHotelID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetSupImageByHotelID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "GetSupImageByHotelID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.GetSupImageByHotelID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "GetSupImageByHotelID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }

        public static DataTable SelectTRoomImageIDByHotelID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "SelectTRoomImageIDByHotelID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.SelectTRoomImageIDByHotelID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "SelectTRoomImageIDByHotelID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }

        public static int RenewSupCoverImageByID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "RenewSupCoverImageByID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.RenewSupCoverImageByID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "RenewSupCoverImageByID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }

        public static int RenewSupImageByID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "RenewSupImageByID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.RenewSupImageByID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "RenewSupImageByID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }

        public static int DeleteSupImageByID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "DeleteSupImageByID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                //return ImageDA.DeleteSupImageByID(ImageEntity);
                int iresult = ImageDA.DeleteSupImageByID(ImageEntity);
                if (iresult > 0)
                {
                    ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
                    HotelInfoSA.RefushHotelList(dbParm.HotelID);
                }
                return iresult;
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "DeleteSupImageByID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetSupImageByID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "GetSupImageByID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.GetSupImageByID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "GetSupImageByID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }
        public static int UpdateSupImageDetailsByID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "UpdateSupImageDetailsByID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.UpdateSupImageDetailsByID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "UpdateSupImageDetailsByID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }
        public static int UpdateSupImageDetailsRepeatByID(ImageEntity ImageEntity)
        {
            ImageEntity.LogMessages.MsgType = MessageType.INFO;
            ImageEntity.LogMessages.Content = _nameSpaceClass + "UpdateSupImageDetailsRepeatByID";
            LoggerHelper.LogWriter(ImageEntity.LogMessages);

            try
            {
                return ImageDA.UpdateSupImageDetailsRepeatByID(ImageEntity);
            }
            catch (Exception ex)
            {
                ImageEntity.LogMessages.MsgType = MessageType.ERROR;
                ImageEntity.LogMessages.Content = _nameSpaceClass + "UpdateSupImageDetailsRepeatByID  Error: " + ex.Message;
                LoggerHelper.LogWriter(ImageEntity.LogMessages);
                throw ex;
            }
        }
        
        
    }
}