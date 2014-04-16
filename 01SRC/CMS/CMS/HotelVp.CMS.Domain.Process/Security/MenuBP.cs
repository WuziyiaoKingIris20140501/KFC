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
    public abstract class MenuBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.MenuBP  Method: ";
        
        //新增
        public static int Insert(MenuEntity menuEntity)
        {
            menuEntity.LogMessages.MsgType = MessageType.INFO;
            menuEntity.LogMessages.Content = _nameSpaceClass + "Insert";
            LoggerHelper.LogWriter(menuEntity.LogMessages);

            try
            {
                return MenuDA.Insert(menuEntity);   
            }
            catch (Exception ex)
            {
                menuEntity.LogMessages.MsgType = MessageType.ERROR;
                menuEntity.LogMessages.Content = _nameSpaceClass + "Insert  Error: " + ex.Message;
                LoggerHelper.LogWriter(menuEntity.LogMessages);
                throw ex;
            }
        }

        public static MenuEntity SelectMenuList(MenuEntity menuEntity)
        {
            menuEntity.LogMessages.MsgType = MessageType.INFO;
            menuEntity.LogMessages.Content = _nameSpaceClass + "select";
            LoggerHelper.LogWriter(menuEntity.LogMessages);
            try
            {
                return MenuDA.SelectMenuList(menuEntity);   
            }
            catch (Exception ex)
            {
                menuEntity.LogMessages.MsgType = MessageType.ERROR;
                menuEntity.LogMessages.Content = _nameSpaceClass + "select  Error: " + ex.Message;
                LoggerHelper.LogWriter(menuEntity.LogMessages);
                throw ex;
            }
         
   
        }

        public static int UpdateMenuByMenuID(MenuEntity menuEntity)
        {
            menuEntity.LogMessages.MsgType = MessageType.INFO;
            menuEntity.LogMessages.Content = _nameSpaceClass + "update menu by id";
            LoggerHelper.LogWriter(menuEntity.LogMessages);
             try
            {
                return MenuDA.UpdateMenuByMenuID(menuEntity);   
            }
            catch (Exception ex)
            {
                menuEntity.LogMessages.MsgType = MessageType.ERROR;
                menuEntity.LogMessages.Content = _nameSpaceClass + "update menu  Error: " + ex.Message;
                LoggerHelper.LogWriter(menuEntity.LogMessages);
                throw ex;
            }
        
        }


        public static int UpdateMenuLimitByMenuID(MenuEntity menuEntity)
        {
            menuEntity.LogMessages.MsgType = MessageType.INFO;
            menuEntity.LogMessages.Content = _nameSpaceClass + "update menu limit";
            LoggerHelper.LogWriter(menuEntity.LogMessages);
             try
            {
                return MenuDA.UpdateMenuLimitByMenuID(menuEntity);   
            }
            catch (Exception ex)
            {
                menuEntity.LogMessages.MsgType = MessageType.ERROR;
                menuEntity.LogMessages.Content = _nameSpaceClass + "update menu limit  Error: " + ex.Message;
                LoggerHelper.LogWriter(menuEntity.LogMessages);
                throw ex;
            }
        
        }

        public static int UpdateMenuDisplayByMenuID(MenuEntity menuEntity)
        {
            menuEntity.LogMessages.MsgType = MessageType.INFO;
            menuEntity.LogMessages.Content = _nameSpaceClass + "update menu display";
            LoggerHelper.LogWriter(menuEntity.LogMessages);
             try
            {
                return MenuDA.UpdateMenuDisplayByMenuID(menuEntity);   
            }
            catch (Exception ex)
            {
                menuEntity.LogMessages.MsgType = MessageType.ERROR;
                menuEntity.LogMessages.Content = _nameSpaceClass + "update menu display  Error: " + ex.Message;
                LoggerHelper.LogWriter(menuEntity.LogMessages);
                throw ex;
            }
        
        }


        public static int DeleteMenuByMenuID(MenuEntity menuEntity)
        {
            menuEntity.LogMessages.MsgType = MessageType.INFO;
            menuEntity.LogMessages.Content = _nameSpaceClass + "delete menu";
            LoggerHelper.LogWriter(menuEntity.LogMessages);
            try
            {
                return MenuDA.DeleteMenuByMenuID(menuEntity);
            }
            catch (Exception ex)
            {
                menuEntity.LogMessages.MsgType = MessageType.ERROR;
                menuEntity.LogMessages.Content = _nameSpaceClass + "delete menu Error: " + ex.Message;
                LoggerHelper.LogWriter(menuEntity.LogMessages);
                throw ex;
            }
        
        }


        public static DataSet getFristMenu()
        {          
            try
            {
                return MenuDA.getFristMenu();
            }
            catch (Exception ex)
            {               
                //throw ex;
                return null;
            }
        
        }

    }
}
