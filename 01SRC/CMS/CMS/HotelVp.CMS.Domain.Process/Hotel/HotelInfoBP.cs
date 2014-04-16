using System;
using System.Collections;
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
    public abstract class HotelInfoBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.HotelInfoBP  Method: ";

        public static HotelInfoEntity CommonHotelGroupSelect(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.CommonHotelGroupSelect(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonHotelGroupSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity CommonCitySelect(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonCitySelect";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.CommonCitySelect(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonCitySelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity CommonProvincialSelect(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonProvincialSelect";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.CommonProvincialSelect(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CommonProvincialSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        //public static HotelInfoEntity ServiceTypeSelect(HotelInfoEntity hotelInfoEntity)
        //{
        //    hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
        //    hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ServiceTypeSelect";
        //    LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

        //    try
        //    {
        //        return HotelInfoDA.ServiceTypeSelect(hotelInfoEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
        //        hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ServiceTypeSelect  Error: " + ex.Message;
        //        throw ex;
        //    }
        //}

        public static HotelInfoEntity BindHotelList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BindHotelList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.BindHotelList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BindHotelList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetSalesManager(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetSalesManager";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetSalesManager(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetSalesManager  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity SelectHotelInfoEX(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SelectHotelInfoEX";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.SelectHotelInfoEX(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SelectHotelInfoEX  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity ChkLMPropHotelList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ChkLMPropHotelList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.ChkLMPropHotelList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ChkLMPropHotelList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity ChkLMPropHotelExam(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ChkLMPropHotelExam";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.ChkLMPropHotelExam(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ChkLMPropHotelExam  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity ReadFogHotelInfo(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ReadForHotelInfo";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.ReadFogHotelInfo(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ReadForHotelInfo  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static int HotelSave(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "HotelSave";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.HotelSave(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "HotelSave  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity UpdateHotelInfo(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelInfo";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                hotelInfoEntity = HotelInfoDA.UpdateHotelInfo(hotelInfoEntity);

                if (hotelInfoEntity.Result == 1)
                {
                    HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
                    HotelInfoSA.RefushHotelList(dbParm.HotelID);
                }
                return hotelInfoEntity;
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelInfo  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity CreateHotelInfo(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CreateHotelInfo";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                hotelInfoEntity = HotelInfoDA.CreateHotelInfo(hotelInfoEntity);
                if (hotelInfoEntity.Result == 1)
                {
                    HotelInfoSA.RefushHotelList(hotelInfoEntity.ErrorMSG);
                }
                return hotelInfoEntity;
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CreateHotelInfo  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static string SetBDlonglatTude(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SetBDlonglatTude";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoSA.SetBDlonglatTude(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SetBDlonglatTude  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity SupHotelSave(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SupHotelSave";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.SupHotelSave(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SupHotelSave  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity BindHotelImagesList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BindHotelImagesList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoSA.BindHotelImagesList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BindHotelImagesList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity SetBalanceRoomList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SetBalanceRoomList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                //hotelInfoEntity = HotelInfoSA.SetBalanceRoomList(hotelInfoEntity);
                //if (hotelInfoEntity.Result != 1)
                //{
                //    return hotelInfoEntity;
                //}

                //HotelInfoDA.InsertBalanceHistory(hotelInfoEntity);
                //hotelInfoEntity.Result = 1;
                //return hotelInfoEntity;
                return HotelInfoSA.SetBalanceRoomList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SetBalanceRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetBalanceRoomList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetBalanceRoomList(hotelInfoEntity);
                //return HotelInfoSA.GetBalanceRoomList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetBalanceRoomListByHotel(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomListByHotel";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetBalanceRoomListByHotel(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomListByHotel  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetBalanceRoomListByHotelAndPriceCode(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomListByHotelAndPriceCode";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetBalanceRoomListByHotelAndPriceCode(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomListByHotelAndPriceCode  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetBalHotelRoomList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalHotelRoomList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetBalHotelRoomList(hotelInfoEntity);
                //return HotelInfoSA.GetBalanceRoomList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalHotelRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }


        public static HotelInfoEntity GetSalesManagerSettingHistory(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetSalesManagerSettingHistory";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetSalesManagerSettingHistory(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetSalesManagerSettingHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetHotelRoomList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRoomList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetHotelRoomList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetHotelRatherList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRatherList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetHotelRatherList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRatherList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetHotelPrRoomList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelPrRoomList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetHotelPrRoomList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelPrRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetHotelRoomHistoryList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRoomHistoryList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetHotelRoomHistoryList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelRoomHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity BindUpdateRoomData(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BindUpdateRoomData";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.BindUpdateRoomData(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BindUpdateRoomData  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity BindUpdatePrRoomData(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BindUpdatePrRoomData";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.BindUpdatePrRoomData(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BindUpdatePrRoomData  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetBalanceRoomHistory(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomHistory";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetBalanceRoomHistory(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetBalanceRoomHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity ExportBalanceRoomHistory(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ExportBalanceRoomHistory";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.ExportBalanceRoomHistory(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "ExportBalanceRoomHistory  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        //public static HotelInfoEntity FacilitiesTypeSelect(HotelInfoEntity hotelInfoEntity)
        //{
        //    hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
        //    hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "FacilitiesTypeSelect";
        //    LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

        //    try
        //    {
        //        return HotelInfoDA.FacilitiesTypeSelect(hotelInfoEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
        //        hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "FacilitiesTypeSelect  Error: " + ex.Message;
        //        throw ex;
        //    }
        //}

        //public static HotelInfoEntity SelectTypeDetail(HotelInfoEntity hotelInfoEntity)
        //{
        //    hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
        //    hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SelectTypeDetail";
        //    LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

        //    try
        //    {
        //        return HotelInfoDA.SelectTypeDetail(hotelInfoEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
        //        hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SelectTypeDetail  Error: " + ex.Message;
        //        throw ex;
        //    }
        //}

        //public static int Insert(HotelInfoEntity hotelInfoEntity)
        //{
        //    hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
        //    hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "Insert";
        //    LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

        //    try
        //    {
        //        return HotelInfoDA.Insert(hotelInfoEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
        //        hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
        //        throw ex;
        //    }
        //}

        //public static int Update(HotelInfoEntity hotelInfoEntity)
        //{
        //    hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
        //    hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "Update";
        //    LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

        //    try
        //    {
        //        return HotelInfoDA.Update(hotelInfoEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
        //        hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "Update  Error: " + ex.Message;
        //        throw ex;
        //    }
        //}

        public static int UpdateHotelSalesList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelSalesList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.UpdateHotelSalesList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "UpdateHotelSalesList  Error: " + ex.Message;
                throw ex;
            }
        }

        public static int SaveHotelRoomsList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveHotelRoomsList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                //hotelInfoEntity = HotelInfoDA.SaveHotelRoomsList(hotelInfoEntity);
                //int iresult = hotelInfoEntity.Result;
                //if (iresult == 1 && !"1".Equals(hotelInfoEntity.HotelInfoDBEntity[0].RoomACT))
                //{
                //    return HotelInfoSA.SaveHotelRoomsPlanList(hotelInfoEntity);
                //}
                //else
                //{
                //    return iresult;
                //}
                hotelInfoEntity = HotelInfoDA.SaveHotelRoomsList(hotelInfoEntity);
                int iresult = hotelInfoEntity.Result;
                if (iresult == 1 && !"1".Equals(hotelInfoEntity.HotelInfoDBEntity[0].RoomACT))
                {
                    int result = HotelInfoSA.SaveHotelRoomsPlanList(hotelInfoEntity);
                    if (result == 1)
                    {
                        HotelInfoDBEntity dbParm = (hotelInfoEntity.HotelInfoDBEntity.Count > 0) ? hotelInfoEntity.HotelInfoDBEntity[0] : new HotelInfoDBEntity();
                        HotelInfoSA.RefushHotelList(dbParm.HotelID);
                    }
                    return result;
                }
                else
                {
                    return iresult;
                }
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveHotelRoomsList  Error: " + ex.Message;
                throw ex;
            }
        }

        public static HotelInfoEntity SaveHotelPrRoomsList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveHotelPrRoomsList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoSA.SaveHotelPrRoomsList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "SaveHotelPrRoomsList  Error: " + ex.Message;
                throw ex;
            }
        }

        public static HotelInfoEntity GetPlanList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetPlanList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoSA.GetPlanList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetPlanList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetShwoHisPlanInfoList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetShwoHisPlanInfoList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetShwoHisPlanInfoList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetShwoHisPlanInfoList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity RenewPlanFullRoom(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "RenewPlanFullRoom";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return HotelInfoSA.RenewPlanFullRoom(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "RenewPlanFullRoom  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 房态控制 --  更新计划接口 (只更新 不插入)
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static APPContentEntity RenewPlanFullRoomByUpdatePlan(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "RenewPlanFullRoomByUpdatePlan";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return HotelInfoSA.RenewPlanFullRoomByUpdatePlan(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "RenewPlanFullRoomByUpdatePlan  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 房态控制 --  批量更新计划的接口  type:1 满房、2 关房、3 开房
        /// </summary>
        /// <param name="appcontentEntity"></param>
        /// <returns></returns>
        public static APPContentEntity BatchUpdatePlan(APPContentEntity appcontentEntity)
        {
            appcontentEntity.LogMessages.MsgType = MessageType.INFO;
            appcontentEntity.LogMessages.Content = _nameSpaceClass + "BatchUpdatePlan";
            LoggerHelper.LogWriter(appcontentEntity.LogMessages);

            try
            {
                return HotelInfoSA.BatchUpdatePlan(appcontentEntity);
            }
            catch (Exception ex)
            {
                appcontentEntity.LogMessages.MsgType = MessageType.ERROR;
                appcontentEntity.LogMessages.Content = _nameSpaceClass + "BatchUpdatePlan  Error: " + ex.Message;
                LoggerHelper.LogWriter(appcontentEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetTagInfoAERA(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetTagInfoAERA";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetTagInfoAERA(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetTagInfoAERA  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static DataSet GetAddRoomList()
        {
            try
            {
                return HotelInfoDA.GetAddRoomList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static HotelInfoEntity BedTypeListSelect(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BedTypeListSelect";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.BedTypeListSelect(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BedTypeListSelect  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity BedTypeListDetail(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BedTypeListDetail";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.BedTypeListDetail(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "BedTypeListDetail  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static int InsertBedType(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "InsertBedType";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.InsertBedType(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "InsertBedType  Error: " + ex.Message;
                throw ex;
            }
        }

        public static APPContentEntity GetConsultRoomHistoryList(APPContentEntity APPContentEntity)
        {
            APPContentEntity.LogMessages.MsgType = MessageType.INFO;
            APPContentEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHistoryList";
            LoggerHelper.LogWriter(APPContentEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetConsultRoomHistoryList(APPContentEntity);
            }
            catch (Exception ex)
            {
                APPContentEntity.LogMessages.MsgType = MessageType.ERROR;
                APPContentEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(APPContentEntity.LogMessages);
                throw ex;
            }
        }
        public static HotelInfoEntity GetConsultRoomHotelRoomList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHistoryList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetConsultRoomHotelRoomList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHistoryList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetConsultRoomHotelRoomListByAll(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHotelRoomListByAll";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetConsultRoomHotelRoomListByAll(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHotelRoomListByAll  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetOrderApproveList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderApproveList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetOrderApproveList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderApproveList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetOrderFaxApproveList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderFaxApproveList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetOrderFaxApproveList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderFaxApproveList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity CheckApproveUser(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CheckApproveUser";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.CheckApproveUser(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CheckApproveUser  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity CheckApproveUserBandHotel(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CheckApproveUserBandHotel";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.CheckApproveUserBandHotel(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CheckApproveUserBandHotel  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetConsultRoomHotelRoomListByProp(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHotelRoomListByProp";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetConsultRoomHotelRoomListByProp(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHotelRoomListByProp  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetConsultRoomHotelRoomListByHotel(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHotelRoomListByHotel";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetConsultRoomHotelRoomListByHotel(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetConsultRoomHotelRoomListByHotel  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetOrderApproveListByHotel(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderApproveListByHotel";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetOrderApproveListByHotel(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderApproveListByHotel  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetOrderApproveHotelFaxList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderApproveHotelFaxList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetOrderApproveHotelFaxList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetOrderApproveHotelFaxList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static APPContentEntity GetHasChangedConsultRoomList(APPContentEntity APPContentEntity)
        {
            APPContentEntity.LogMessages.MsgType = MessageType.INFO;
            APPContentEntity.LogMessages.Content = _nameSpaceClass + "GetHasChangedConsultRoomList";
            LoggerHelper.LogWriter(APPContentEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetHasChangedConsultRoomList(APPContentEntity);
            }
            catch (Exception ex)
            {
                APPContentEntity.LogMessages.MsgType = MessageType.ERROR;
                APPContentEntity.LogMessages.Content = _nameSpaceClass + "GetHasChangedConsultRoomList  Error: " + ex.Message;
                LoggerHelper.LogWriter(APPContentEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetHotelPlanInfoList(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelPlanInfoList";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetHotelPlanInfoList(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelPlanInfoList  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetHotelPlanInfoCount(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelPlanInfoCount";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.GetHotelPlanInfoCount(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetHotelPlanInfoCount  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity CountHotelOnlineLb(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CountHotelOnlineLb";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoDA.CountHotelOnlineLb(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "CountHotelOnlineLb  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetPlanListByIndiscriminatelyRateCode(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetPlanListByIndiscriminatelyRateCode";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoSA.GetPlanListByIndiscriminatelyRateCode(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetPlanListByIndiscriminatelyRateCode  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetPlanListByIndiscriminatelyByRateCode(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetPlanListByIndiscriminatelyByRateCode";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoSA.GetPlanListByIndiscriminatelyByRateCode(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetPlanListByIndiscriminatelyByRateCode  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

        public static HotelInfoEntity GetPlanListByResult(HotelInfoEntity hotelInfoEntity)
        {
            hotelInfoEntity.LogMessages.MsgType = MessageType.INFO;
            hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetPlanListByResult";
            LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);

            try
            {
                return HotelInfoSA.GetPlanListByResult(hotelInfoEntity);
            }
            catch (Exception ex)
            {
                hotelInfoEntity.LogMessages.MsgType = MessageType.ERROR;
                hotelInfoEntity.LogMessages.Content = _nameSpaceClass + "GetPlanListByResult  Error: " + ex.Message;
                LoggerHelper.LogWriter(hotelInfoEntity.LogMessages);
                throw ex;
            }
        }

    }
}
