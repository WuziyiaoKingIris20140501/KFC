using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.EBOK.Domain.Entity;
using HotelVp.EBOK.Domain.DA;
using System.Data;
using System.Data.OleDb;

namespace HotelVp.EBOK.Domain.Biz
{
    public abstract class SettleInfoBP
    {
        /// <summary>
        /// 批量导入收款记录
        /// </summary>
        /// <param name="settleEntity"></param>
        /// <returns></returns>
        public static SettleEntity ImportCollectList(SettleEntity settleEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                //if (String.IsNullOrEmpty(PhoneFlUpload.FileName))
                //{
                //    return "";
                //}

                //StringBuilder sbPhonelist = new StringBuilder();
                //string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(PhoneFlUpload.FileName);// OrderFlUpload.FileName.Substring(OrderFlUpload.FileName.IndexOf('.'));          //获取文件名
                //string folder = Server.MapPath("../Excel");
                //if (!Directory.Exists(folder))
                //    Directory.CreateDirectory(folder);

                //PhoneFlUpload.SaveAs(Server.MapPath("../Excel" + "\\" + fileName));      //上传文件到Excel文件夹下

                SettleDBEntity dbParm = (settleEntity.SettleDBEntity.Count > 0) ? settleEntity.SettleDBEntity[0] : new SettleDBEntity();
                DataTable dtImport = new DataTable();
                if ("0".Equals(dbParm.ImportType))
                {
                    dtImport = LoadExcelToDataTable(System.Web.HttpContext.Current.Server.MapPath(@"~\SettleUploadExcel\" + dbParm.UploadFileName));
                    if (dtImport.Rows.Count < 10)
                    {
                        settleEntity.Result = 0;
                        return settleEntity;
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        dtImport.Rows.RemoveAt(i);
                    }
                    dtImport.AcceptChanges();
                }
                else
                {
                    for (int i=0; i < 14; i++)
                    {
                        dtImport.Columns.Add("col" + i.ToString());
                    }
                    DataRow drRow = dtImport.NewRow();
                    drRow[5] = dbParm.IntoAmount;
                    drRow[8] = dbParm.PayName;
                    drRow[10] = dbParm.Summary;
                    drRow[11] = dbParm.Remark;
                    dtImport.Rows.Add(drRow);
                }
                
                settleEntity.SettleDBEntity[0].TotalUploadCount = (dtImport.Rows.Count - 9).ToString();
                settleEntity.SettleDBEntity[0].UpLoadList = dtImport;
                return SettleDA.ImportCollectList(settleEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        public static DataTable LoadExcelToDataTable(string filename)
        {
            DataTable dtResult = new DataTable();
            //连接字符串  
            string sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=Excel 12.0;";

            //string sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + @";Extended Properties=""Excel 12.0;HDR=YES;""";

            //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filename + ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            OleDbConnection myConn = new OleDbConnection(sConnectionString);
            myConn.Open();
            DataTable sheetNames = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string sheetName = (sheetNames.Rows.Count > 0) ? sheetNames.Rows[0][2].ToString() : "";

            if (String.IsNullOrEmpty(sheetName))
            {
                return dtResult;
            }
            string strCom = " SELECT * FROM [" + sheetName + "]";

            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
            myCommand.Fill(dtResult);
            myConn.Close();
            return dtResult;
        }

        ///// <summary>
        ///// 手动添加收款记录
        ///// </summary>
        ///// <param name="settleEntity"></param>
        ///// <returns></returns>
        //public static SettleEntity AddCollectItem(SettleEntity settleEntity)
        //{
        //    //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
        //    //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
        //    //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

        //    try
        //    {
        //        return SettleDA.AddCollectItem(settleEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
        //        //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
        //        //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 异步调用 自动筛选 匹配账单
        /// </summary>
        /// <param name="settleEntity"></param>
        /// <returns></returns>
        public static SettleEntity ApproveAutoCompleteCollectList(SettleEntity settleEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return SettleDA.ApproveAutoCompleteCollectList(settleEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 确认(调整导入)收款记录
        /// </summary>
        /// <param name="settleEntity"></param>
        /// <returns></returns>
        public static SettleEntity ApproveCollectList(SettleEntity settleEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return SettleDA.ApproveCollectList(settleEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 上传收款记录取得
        /// </summary>
        /// <param name="settleEntity"></param>
        /// <returns></returns>
        public static SettleEntity GetImportCollectList(SettleEntity settleEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return SettleDA.GetImportCollectList(settleEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }

        /// <summary>
        /// 回款销账记录取得
        /// </summary>
        /// <param name="settleEntity"></param>
        /// <returns></returns>
        public static SettleEntity GetApproveCollectList(SettleEntity settleEntity)
        {
            //liquidateEntity.LogMessages.MsgType = MessageType.INFO;
            //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select";
            //LoggerHelper.LogWriter(liquidateEntity.LogMessages);

            try
            {
                return SettleDA.GetApproveCollectList(settleEntity);
            }
            catch (Exception ex)
            {
                //liquidateEntity.LogMessages.MsgType = MessageType.ERROR;
                //liquidateEntity.LogMessages.Content = _nameSpaceClass + "Select  Error: " + ex.Message;
                //LoggerHelper.LogWriter(liquidateEntity.LogMessages);
                throw ex;
            }
        }
    }
}
