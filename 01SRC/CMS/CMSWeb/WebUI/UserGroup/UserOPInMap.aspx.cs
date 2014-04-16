using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data;
using System.Text;
using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using System.Collections;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Configuration;


public partial class WebUI_UserGroup_UserOPInMap : BasePage
{
    public string hotelname = string.Empty;
    public string latitude = string.Empty;
    public string longitude = string.Empty;
    public string arrData = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //HttpFileCollection files = HttpContext.Current.Request.Files;
        ////状态信息
        //System.Text.StringBuilder strMsg = new System.Text.StringBuilder();
        //for (int ifile = 0; ifile < files.Count; ifile++)
        //{
        //    HttpPostedFile postedfile = files[ifile];
        //    string filename, fileExt;
        //    filename = System.IO.Path.GetFileName(postedfile.FileName);    //获取文件名
        //    fileExt = System.IO.Path.GetExtension(filename);    //获取文件后缀

        //    int MaxAllowUploadFileSize = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["MaxAllowUploadFileSize"]);     //定义允许上传文件大小
        //    if (MaxAllowUploadFileSize == 0) { MaxAllowUploadFileSize = 26500; }
        //    string allowexts = System.Configuration.ConfigurationSettings.AppSettings["AllowUploadFileType"];    //定义允许上传文件类型
        //    if (allowexts == "") { allowexts = "doc|docx|rar|xls|xlsx|txt"; }
        //    Regex allowext = new Regex(allowexts);

        //    if (postedfile.ContentLength < MaxAllowUploadFileSize && allowext.IsMatch(fileExt)) //检查文件大小及扩展名
        //    {
        //        postedfile.SaveAs(Server.MapPath("upload\\" + filename + fileExt));    //upload为与本页面同一目录，可自行修改
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('不允许上传类型" + fileExt + "或文件过大')</script>");
        //    }
        //}
        if (!IsPostBack)
        {
            arrData = "[[0.0]]";
        }
    }

    private DataSet getHotelData()
    {
        DataSet dsResult = new DataSet();
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("LATITUDE");
        dtResult.Columns.Add("LONGITUDE");
        dsResult.Tables.Add(dtResult);

        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(OrderFlUpload.FileName);// OrderFlUpload.FileName.Substring(OrderFlUpload.FileName.IndexOf('.'));          //获取文件名
        string folder = Server.MapPath("../Excel");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        OrderFlUpload.SaveAs(Server.MapPath("../Excel" + "\\" + fileName));      //上传文件到Excel文件夹下
        DataTable dtTemp = LoadExcelToDataTable(Server.MapPath("../Excel" + "\\" + fileName));  //读取excel内容，转成DataTable
        File.Delete(Server.MapPath("../Excel" + "\\" + fileName));                                            //最后删除文件

        foreach (DataRow drTemp in dtTemp.Rows)
        {
            if (drTemp[0].ToString().IndexOf(',') <= 0)
            {
                continue;
            }
            DataRow drRow = dtResult.NewRow();
            drRow["LATITUDE"] = drTemp[0].ToString().Substring(0, drTemp[0].ToString().IndexOf(',') - 1).Trim();
            drRow["LONGITUDE"] = drTemp[0].ToString().Substring(drTemp[0].ToString().IndexOf(',') + 1).Trim();
            dtResult.Rows.Add(drRow);
        }

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(OrderFlUpload.FileName))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString();
            arrData = "[[0.0]]";
            return;
        }

        string fileExt = System.IO.Path.GetExtension(OrderFlUpload.FileName);
        string allowexts = (ConfigurationManager.AppSettings["AllowUploadFileType"] != null) ? ConfigurationManager.AppSettings["AllowUploadFileType"].ToString() : "";    //定义允许上传文件类型
        if (allowexts == "") { allowexts =  ".*xls|.*xlsx";}

        Regex allowext = new Regex(allowexts);

        if (!allowext.IsMatch(fileExt)) //检查文件大小及扩展名
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString();
            arrData = "[[0.0]]";
            return;
        }

       arrData = getLatLngList();
       this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "initialize();", true);
    }

    private string getLatLngList()
    {
        string latitudetemp = string.Empty;//经度
        string longitudetemp = string.Empty;//维度
        string temp = string.Empty;     
        StringBuilder sb = new StringBuilder();
        DataTable dt = getHotelData().Tables[0];
        string tempList = string.Empty;

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                latitudetemp = dt.Rows[i]["LATITUDE"].ToString();//经度
                longitudetemp = dt.Rows[i]["LONGITUDE"].ToString();//维度
                temp = "[" + latitudetemp + "," + longitudetemp + "," + i + "]";
                sb.Append(temp);
                sb.Append(',');
            }
            tempList = "[" + sb.ToString().Trim(',') + "]";
        }
        else
        {
            tempList = "[[0.0]]";
        }
        return tempList; 
    }   
}