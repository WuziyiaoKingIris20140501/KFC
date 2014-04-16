using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;

using HotelVp.JobConsole.Entity;
using System.Data.OleDb;

namespace HotelVp.JobConsole.DataAccess
{
    public abstract class AutoMsgUserExcelDA
    {
        public static DataSet AutoListSelect(string fileName)
        {
            DataSet dsResult = new DataSet();
            string strFilePath = fileName;// ConfigurationManager.AppSettings["UploadFilePath1"].ToString();
            DataTable dtResult = new DataTable();

            if (!System.IO.File.Exists(strFilePath))
            {
                return dsResult;
            }

            dtResult = LoadExcelToDataTable(strFilePath);
            if (dtResult.Rows.Count > 0)
            {
                dtResult.Columns[0].ColumnName = "USERID";
                dtResult.Columns[1].ColumnName = "CONTENT";
            }

            dsResult.Tables.Add(dtResult);
            return dsResult;
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

        private static string SetParamDtime(int AddMin)
        {
            string strResult = string.Empty;
            string strDtimeNow = DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":00";
            strResult = DateTime.Parse(strDtimeNow).AddMinutes(AddMin).ToString();
            return strResult;
        }
    }
}