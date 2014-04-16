using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace HotelVp.JobConsole.Biz
{
    public class ExcelManager
    {
        private string filePath = "";
        public ExcelManager(string excel_path)
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
            filePath = excel_path;
        }
        /// <summary> 
        /// 将指定的Dataset导出到Excel文件 
        /// </summary> 
        /// <param name="dt"></param> 
        /// <returns></returns> 
        public bool ExportToExcel(System.Data.DataSet ds, string ReportName)
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("数据集为空");
            }
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlbook = xlapp.Workbooks.Add(true);
            Worksheet xlsheet = (Worksheet)xlbook.Worksheets[1];
            //Range range = xlsheet.get_Range(xlapp.Cells[1, 1], xlapp.Cells[1, ds.Tables[0].Columns.Count]);
            //range.MergeCells = true;
            xlapp.ActiveCell.FormulaR1C1 = ReportName;
            xlapp.ActiveCell.Font.Size = 20;
            xlapp.ActiveCell.Font.Bold = true;
            xlapp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            int colIndex = 0;
            int RowIndex = 2;
            //开始写入每列的标题 
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                colIndex++;
                xlsheet.Cells[RowIndex, colIndex] = dc.Caption;
            }
            //开始写入内容 
            int RowCount = ds.Tables[0].Rows.Count;//行数 
            for (int i = 0; i < RowCount; i++)
            {
                RowIndex++;
                int ColCount = ds.Tables[0].Columns.Count;//列数 
                for (colIndex = 1; colIndex <= ColCount; colIndex++)
                {
                    xlsheet.Cells[RowIndex, colIndex] = ds.Tables[0].Rows[i][colIndex - 1];//dg[i, colIndex - 1]; 
                    xlsheet.Cells.ColumnWidth = ds.Tables[0].Rows[i][colIndex - 1].ToString().Length;
                }
            }

            xlbook.Saved = true;
            xlbook.SaveCopyAs(filePath);
            xlapp.Quit();
            GC.Collect();
            return true;
        }

        public bool ExportToExcelOF(System.Data.DataSet ds, string ReportName)
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("数据集为空");
            }
            string FileName = filePath;

            //System.Data.DataTable dt = new System.Data.DataTable(); 
            FileStream objFileStream;
            StreamWriter objStreamWriter;
            string strLine = "";
            objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
            objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);

            strLine = ReportName;
            objStreamWriter.WriteLine(strLine);
            strLine = "";

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                strLine = strLine + ds.Tables[0].Columns[i].ColumnName.ToString() + "          " + Convert.ToChar(9);
            }
            objStreamWriter.WriteLine(strLine);
            strLine = "";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strLine = strLine + (i + 1) + Convert.ToChar(9);
                for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                {
                    strLine = strLine + ds.Tables[0].Rows[i][j].ToString() + Convert.ToChar(9);
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";
            }
            objStreamWriter.Close();
            objFileStream.Close();

            //Microsoft.Office.Interop.Excel._Application xlapp = new ApplicationClass(); 
            //Workbook xlbook = xlapp.Workbooks.Add(true); 
            //Worksheet xlsheet = (Worksheet)xlbook.Worksheets[1]; 
            //Range range = xlsheet.get_Range(xlapp.Cells[1, 1], xlapp.Cells[1, ds.Tables[0].Columns.Count]); 
            //range.EntireColumn.AutoFit(); 
            //xlapp.Quit(); 
            return true;
        }
    }
}