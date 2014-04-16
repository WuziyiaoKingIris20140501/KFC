using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Xml;
using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;
using System.Text.RegularExpressions;


/// <summary>
///CommonFunction 的摘要说明
/// </summary>
public class CommonFunction 
{
	public CommonFunction()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static string strAdmin = "admin";

    public string setMD5Password(string strPassword)
    {
        string strMD5Password = "";
        if (strPassword != "")
        {
            strMD5Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strPassword, "MD5");
        }
        return strMD5Password;
    }
    
    //get JsonStringValue
    public string GetJsonStringValue( HotelVp.Common.Json.Linq.JObject jso, string key)
    {
        try
        {
            return jso[key] == null ? string.Empty : jso[key].ToString();
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 回到历史页面
    /// </summary>
    /// <param name="value">-1/1</param>
    public static void GoHistory(int value,System.Web.UI.Page page)
    {
        #region
        string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
        if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "GoHistory"))
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "GoHistory", string.Format(js, value));
        }
        #endregion
    }
      
    public bool IsMobileNumber(string str_telephone)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"(1[3,4,5,8][0-9])\d{8}$");
    }

    //通过sequence查询得到下一个ID值,数据库为Oracle
    public int getMaxIDfromSeq(string sequencename)
    {
        int seqID = 1;
        string sql = "select " + sequencename + ".nextval from dual";
        object obj = DbHelperOra.GetSingle(sql, false);
        if (obj != null)
        {
            seqID = Convert.ToInt32(obj);
        }
        return seqID;

    }


    //得到一个随机数
    public string GetRandNumString(int length)
    {
        string str = "1234567890";
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            builder.Append(str[new Random(Guid.NewGuid().GetHashCode()).Next(0, 9)]);
        }
        return builder.ToString();
    }


    //把输入框中有单引号字符的替换为空。
    public static string StringFilter(string strString)
    {
        return strString.Replace("'", "");
    }

    /// <summary>
    ///  根据文件名，和ID查询得到的值。
    /// </summary>
    /// <param name="FileName">文件名,不包含后缀名</param>
    /// <param name="TagID">mapiID的对应值</param>
    /// <returns></returns>
    public static string GetUrlFromXml(string FileName,string TagID)
    {
        string XmlTagName = "mapi";//  <mapi mapiID="AdviceSearch">/advice.html/json/xml</mapi>
        string XmlTagID = "lmApiID";//

        string xmlString = String.Empty;      
        //string path = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["SqlPath"].ToString() + SqlName + ConfigurationManager.AppSettings["SqlType"].ToString();
        string path = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["LocalXmlPath"].ToString() + FileName +".xml";

        XmlDocument doc = new XmlDocument();//创建一个新的xmldocumt 对象
        doc.Load(path);//加载xml文件
        
        //string SqlTagName = ConfigurationManager.AppSettings["SqlTagName"].ToString();
        //string SqlAttributes = ConfigurationManager.AppSettings["SqlAttributes"].ToString();

        XmlNodeList list = doc.DocumentElement.GetElementsByTagName(XmlTagName);
        foreach (XmlNode node in list)
        {
            if (node.Attributes[XmlTagID].Value.Equals(TagID))           
            {
                xmlString = node.InnerText;
                break;
            }
        }
        return xmlString;
    }

    /// <summary>
    /// 读取本地xml文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static DataSet getDataFromXml(string fileName)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(HttpContext.Current.Server.MapPath(fileName));
        return ds;        
    }

    /// <summary>
    /// 根据路径把Xml文件加载DataSet中
    /// </summary>
    /// <param name="RemoteUrl"></param>
    /// <returns></returns>
    public static DataSet getDataFromRemoteXml(string RemoteUrl)
    {
        DataSet ds = new DataSet();        
        ds.ReadXml(RemoteUrl);
        return ds;
    }

    /// <summary>
    /// 把XML格式的内容，通过流的方式读取进来，然后载入DataSet中。
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DataSet getDataFromStringXml(string strXml)
    {
        if (!string.IsNullOrEmpty(strXml))
        {
            System.IO.StringReader reader = new System.IO.StringReader(strXml);
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            return ds;
        }
        else
        {
            return null;
        }
    }
    
   
    /// <summary>
    /// 保存Cookies and Session包括用户账户、用户显示名、用户IP、用户机器名
    /// </summary>
    /// <param name="userAccount">用户账户</param>
    /// <param name="userDspName">用户名称</param>
    public void setSesssionAndCookies(string userAccount,string userDspName, string userGroups)
    {
        UserSession.Current.UserAccount = userAccount;
        UserSession.Current.UserDspName = userDspName;
        UserSession.Current.UserGroups = userGroups;
        //string strIP = GetClientIP();

        UserSession.Current.UserIP = IPAddress();
       // UserSession.Current.UserHostName = GetHostName(strIP);

        UserSession.Current.UserHostName = "Hotelvp Office";

        HttpCookie HCaccount = new HttpCookie("LoginUserAccount", userAccount);
        HttpCookie HCdspname = new HttpCookie("LoginDspName", HttpUtility.UrlEncode(userDspName));
        HttpCookie HGroups = new HttpCookie("LoginUserGroups", HttpUtility.UrlEncode(userGroups));

        HttpContext.Current.Response.AppendCookie(HCaccount);
        HttpContext.Current.Response.AppendCookie(HCdspname);
        HttpContext.Current.Response.AppendCookie(HGroups);

        HCaccount.Expires = System.DateTime.Now.AddMonths(1);
        HCdspname.Expires = System.DateTime.Now.AddMonths(1);
        HGroups.Expires = System.DateTime.Now.AddMonths(1);
    }

    /// <summary>
    /// 清空Session和Cookies
    /// </summary>
    public void clearSessionAndCookies()
    {
        UserSession.Current.UserAccount = "";
        HttpContext.Current.Response.Cookies["LoginUserAccount"].Value = "";

        UserSession.Current.UserDspName = "";
        HttpContext.Current.Response.Cookies["LoginDspName"].Value = "";

        UserSession.Current.UserIP = "";
        UserSession.Current.UserHostName = "";

        UserSession.Current.UserGroups = "";
        HttpContext.Current.Response.Cookies["LoginUserGroups"].Value = "";
    }

    /// <summary>
    /// check login information is true，返回user display name.
    /// </summary>
    /// <param name="userAccount"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public string checkLogin(string userAccount, string pwd)
    {
        string getDspName = string.Empty;
        try
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UserLoginByAccount");
            cmd.SetParameterValue("@USER_Account", userAccount);
            cmd.SetParameterValue("@User_Pwd", pwd);
            System.Data.DataSet dsResult = cmd.ExecuteDataSet();
            DataTable dtUser = dsResult.Tables[0];
            if (dtUser.Rows.Count > 0)
            {
                getDspName = dtUser.Rows[0]["User_Account"].ToString() + "," + dtUser.Rows[0]["User_DspName"].ToString();//用户登录名
                return getDspName;
            }
            else
            {
                return getDspName + ",";
            }
        }
        catch
        {
            return getDspName + ",";
        }

    }

    public string GetClientIP()
    {
        //可以透过代理服务器
        string userIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (userIP == null || userIP == "")
        {
            //没有代理服务器,如果有代理服务器获取的是代理服务器的IP
            userIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return userIP;
    }

    public string IPAddress()
    {
        string result = String.Empty;
        result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (result != null && result != String.Empty)
        {
            //可能有代理 
            if (result.IndexOf(".") == -1)     //没有“.”肯定是非IPv4格式 
                result = null;
            else
            {
                if (result.IndexOf(",") != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。 
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];     //找到不是内网的地址 
                        }
                    }
                }
                else if (IsIPAddress(result)) //代理即是IP格式 
                    return result;
                else
                    result = null;     //代理中的内容 非IP，取IP 
            }
        }

        string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        if (null == result || result == String.Empty)
        result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (result == null || result == String.Empty)
            result = HttpContext.Current.Request.UserHostAddress;
        return result;
    }

    public bool IsIPAddress(string str1)
    {
        if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
        string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
        Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
        return regex.IsMatch(str1);
    }

    public string GetHostName(string strClientIP)
    {
        System.Net.IPHostEntry ihe = System.Net.Dns.GetHostEntry(strClientIP);    //根据ip对象创建主机对象

        return ihe.HostName;
    }


    #region 导出Excel文件

    public static void DataSet2Excel(DataSet ds)
    {
        int maxRow = ds.Tables[0].Rows.Count;
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";//设置导出文件的名称

        //DataView dv = new DataView(ds.Tables[0]);//将DataSet转换成DataView
        string fileURL = string.Empty;
        HttpContext curContext = System.Web.HttpContext.Current;
        curContext.Response.ContentType = "application/vnd.ms-excel";
        curContext.Response.ContentEncoding = System.Text.Encoding.Default;
        curContext.Response.AppendHeader("Content-Disposition", ("attachment;filename=" + fileName));
        curContext.Response.Charset = "";

        curContext.Response.Write(BuildExportHTML(ds.Tables[0]));
        curContext.Response.Flush();
        curContext.Response.End();
    }

    public static void ExportExcelForLM(DataSet ds)
    {
        int maxRow = ds.Tables[0].Rows.Count;
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";//设置导出文件的名称

        //DataView dv = new DataView(ds.Tables[0]);//将DataSet转换成DataView
        string fileURL = string.Empty;
        HttpContext curContext = System.Web.HttpContext.Current;
        curContext.Response.ContentType = "application/vnd.ms-excel";
        curContext.Response.ContentEncoding = System.Text.Encoding.Default;
        curContext.Response.AppendHeader("Content-Disposition", ("attachment;filename=" + fileName));
        curContext.Response.Charset = "";

        curContext.Response.Write(BuildExportHTMLFORLM(ds.Tables[0]));
        curContext.Response.Flush();
        curContext.Response.End();
    }

    public static void ExportExcelForLMList(DataSet ds)
    {
        int maxRow = ds.Tables[0].Rows.Count;
        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";//设置导出文件的名称

        //DataView dv = new DataView(ds.Tables[0]);//将DataSet转换成DataView
        string fileURL = string.Empty;
        HttpContext curContext = System.Web.HttpContext.Current;
        curContext.Response.ContentType = "application/vnd.ms-excel";
        curContext.Response.ContentEncoding = System.Text.Encoding.Default;
        curContext.Response.AppendHeader("Content-Disposition", ("attachment;filename=" + fileName));
        curContext.Response.Charset = "";

        curContext.Response.Write(BuildExportHTMLFORLMList(ds));
        curContext.Response.Flush();
        curContext.Response.End();
    }

    //替换Xml中的特殊字符
    private static string ReplaceString(string str)
    {
        string strValue = str;
        if (strValue != "")
        {
            strValue = strValue.Replace("&", "&amp;");
            strValue = strValue.Replace("<", "&lt;");
            strValue = strValue.Replace(">", "&gt;");
        }

        return strValue;
    }

    private static string BuildExportHTMLFORLM(System.Data.DataTable dt)
    {
        string result = string.Empty;
        int readCnt = dt.Rows.Count;
        int colCount = dt.Columns.Count;

        int pagerecords = 65500;
        result = "<?xml version=\"1.0\"  encoding=\"gb2312\"?>";
        result += "<?mso-application progid=\"Excel.Sheet\"?>";
        result += "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ";
        result += "xmlns:o=\"urn:schemas-microsoft-com:office:office\" ";
        result += "xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ";
        result += "xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" ";
        result += "xmlns:html=\"http://www.w3.org/TR/REC-html40\"> ";
        //以下两部分是可选的
        //result += "<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">";
        //result += "<Author>User</Author>";
        //result += "<LastAuthor>User</LastAuthor>";
        //result += "<Created>2009-03-20T02:15:12Z</Created>";
        //result += "<Company>Microsoft</Company>";
        //result += "<Version>12.00</Version>";
        //result += "</DocumentProperties>";

        //result += "<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">";
        //result += "<WindowHeight>7815</WindowHeight>";
        //result += "<WindowWidth>14880</WindowWidth>";
        //result += "<WindowTopX>240</WindowTopX>";
        //result += "<WindowTopY>75</WindowTopY>";
        //result += "<ProtectStructure>False</ProtectStructure>";
        //result += "<ProtectWindows>False</ProtectWindows>";
        //result += "</ExcelWorkbook>";

        string strTitleRow = "";
        strTitleRow = "<Row  ss:AutoFitHeight='0'>";
        for (int j = 0; j < colCount; j++)
        {
            strTitleRow += "<Cell><Data ss:Type=\"String\">" + dt.Columns[j].ColumnName + "</Data></Cell>";
        }
        strTitleRow += "</Row>";

        StringBuilder strRows = new StringBuilder();
        int page = 1;
        int cnt = 0;
        int sheetcolnum = 0;

        for (int i = 0; i < readCnt; i++)
        {
            strRows.Append("<Row  ss:AutoFitHeight=\"0\">");
            for (int j = 0; j < colCount; j++)
            {

                if (dt.Columns[j].DataType.Name == "DateTime" || dt.Columns[j].DataType.Name == "SmallDateTime")
                {
                    if (dt.Rows[i][j].ToString() != string.Empty)
                    {
                        strRows.Append("<Cell><Data ss:Type=\"String\">" + Convert.ToDateTime(dt.Rows[i][j].ToString()) + "</Data></Cell>");
                    }
                    else
                        strRows.Append("<Cell><Data ss:Type=\"String\"></Data></Cell>");
                }
                else if (dt.Columns[j].DataType.Name == "Decimal")
                {
                    if (dt.Rows[i][j].ToString() != string.Empty)
                    {
                        strRows.Append("<Cell><Data ss:Type=\"Number\">" + Convert.ToDecimal(dt.Rows[i][j].ToString()) + "</Data></Cell>");
                    }
                    else
                    strRows.Append("<Cell><Data ss:Type=\"Number\"></Data></Cell>");
                }
                else
                {
                    strRows.Append("<Cell><Data ss:Type=\"String\">" + ReplaceString(dt.Rows[i][j].ToString().Trim()) + "</Data></Cell>");
                }
            }
            strRows.Append("</Row>");
            cnt++;
            if (cnt >= pagerecords)
            {
                sheetcolnum = cnt + 1;
                result += "<Worksheet ss:Name=\"Sheet" + page.ToString() + "\"><Table  ss:ExpandedColumnCount=\"" + colCount.ToString() + "\" ss:ExpandedRowCount=\"" + sheetcolnum.ToString() + "\" x:FullColumns=\"1\"  x:FullRows=\"1\" ss:DefaultColumnWidth=\"104\" ss:DefaultRowHeight=\"13.5\">" + strTitleRow.ToString() + strRows.ToString() + "</Table></Worksheet>";
                strRows.Remove(0, strRows.Length);
                cnt = 1;
                page++;

            }
        }
        sheetcolnum = cnt + 1;
        result = result + "<Worksheet ss:Name='Sheet" + page.ToString() + "'><Table  ss:ExpandedColumnCount='" + colCount.ToString() + "' ss:ExpandedRowCount='" + sheetcolnum.ToString() + "' x:FullColumns='1'  x:FullRows='1' ss:DefaultColumnWidth='104' ss:DefaultRowHeight='13.5'>" + strTitleRow.ToString() + strRows.ToString() + "</Table></Worksheet></Workbook>";
        return result;
    }

    private static string BuildExportHTMLFORLMList(System.Data.DataSet ds)
    {
        string result = string.Empty;

        DataTable dtTotal = ds.Tables["Master"];
        DataTable dt = ds.Tables["Detail"];

        int readCnt = dt.Rows.Count;
        int colCount = dt.Columns.Count;

        int pagerecords = 65500;
        result = "<?xml version=\"1.0\"  encoding=\"gb2312\"?>";
        result += "<?mso-application progid=\"Excel.Sheet\"?>";
        result += "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ";
        result += "xmlns:o=\"urn:schemas-microsoft-com:office:office\" ";
        result += "xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ";
        result += "xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" ";
        result += "xmlns:html=\"http://www.w3.org/TR/REC-html40\"> ";

        string strTitleRow = "";
        StringBuilder strTotalRows = new StringBuilder();

        strTotalRows.Append("<Row  ss:AutoFitHeight='0'><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\">HVP价格过高</Data></Cell><Cell><Data ss:Type=\"String\">HVP价格过低</Data></Cell><Cell><Data ss:Type=\"String\">HVP价格正常</Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell></Row>");

        if (dtTotal.Rows.Count > 0)
        {
            strTotalRows.Append("<Row  ss:AutoFitHeight='0'><Cell><Data ss:Type=\"String\">酒店数</Data></Cell><Cell><Data ss:Type=\"String\">" + dtTotal.Rows[0]["BHLID"].ToString() + "</Data></Cell><Cell><Data ss:Type=\"String\">" + dtTotal.Rows[0]["LHLID"].ToString() + "</Data></Cell><Cell><Data ss:Type=\"String\">" + dtTotal.Rows[0]["DHLID"].ToString() + "</Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell></Row>");
            strTotalRows.Append("<Row  ss:AutoFitHeight='0'><Cell><Data ss:Type=\"String\">房间数</Data></Cell><Cell><Data ss:Type=\"String\">" + dtTotal.Rows[0]["BRMCD"].ToString() + "</Data></Cell><Cell><Data ss:Type=\"String\">" + dtTotal.Rows[0]["LRMCD"].ToString() + "</Data></Cell><Cell><Data ss:Type=\"String\">" + dtTotal.Rows[0]["DRMCD"].ToString() + "</Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell></Row>");
        }
        else
        {
            strTotalRows.Append("<Row  ss:AutoFitHeight='0'><Cell><Data ss:Type=\"String\">酒店数</Data></Cell><Cell><Data ss:Type=\"String\">0</Data></Cell><Cell><Data ss:Type=\"String\">0</Data></Cell><Cell><Data ss:Type=\"String\">0</Data></Cell></Row><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell>");
            strTotalRows.Append("<Row  ss:AutoFitHeight='0'><Cell><Data ss:Type=\"String\">房间数</Data></Cell><Cell><Data ss:Type=\"String\">0</Data></Cell><Cell><Data ss:Type=\"String\">0</Data></Cell><Cell><Data ss:Type=\"String\">0</Data></Cell></Row><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell>");
        }

        strTotalRows.Append("<Row  ss:AutoFitHeight=\"0\"><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell></Row>");
        strTotalRows.Append("<Row  ss:AutoFitHeight=\"0\"><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell></Row>");
        strTotalRows.Append("<Row  ss:AutoFitHeight=\"0\"><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell><Cell><Data ss:Type=\"String\"></Data></Cell></Row>");
        

        strTitleRow = "<Row  ss:AutoFitHeight='0'>";

        for (int j = 0; j < colCount; j++)
        {
            strTitleRow += "<Cell><Data ss:Type=\"String\">" + dt.Columns[j].ColumnName + "</Data></Cell>";
        }
        strTitleRow += "</Row>";

        StringBuilder strRows = new StringBuilder();
        
        int page = 1;
        int cnt = 0;
        int sheetcolnum = 0;

        for (int i = 0; i < readCnt; i++)
        {
            strRows.Append("<Row  ss:AutoFitHeight=\"0\">");
            for (int j = 0; j < colCount; j++)
            {

                if (dt.Columns[j].DataType.Name == "DateTime" || dt.Columns[j].DataType.Name == "SmallDateTime")
                {
                    if (dt.Rows[i][j].ToString() != string.Empty)
                    {
                        strRows.Append("<Cell><Data ss:Type=\"String\">" + Convert.ToDateTime(dt.Rows[i][j].ToString()) + "</Data></Cell>");
                    }
                    else
                        strRows.Append("<Cell><Data ss:Type=\"String\"></Data></Cell>");
                }
                else if (dt.Columns[j].DataType.Name == "Decimal")
                {
                    if (dt.Rows[i][j].ToString() != string.Empty)
                    {
                        strRows.Append("<Cell><Data ss:Type=\"Number\">" + Convert.ToDecimal(dt.Rows[i][j].ToString()) + "</Data></Cell>");
                    }
                    else
                        strRows.Append("<Cell><Data ss:Type=\"Number\"></Data></Cell>");
                }
                else
                {
                    strRows.Append("<Cell><Data ss:Type=\"String\">" + ReplaceString(dt.Rows[i][j].ToString().Trim()) + "</Data></Cell>");
                }
            }
            strRows.Append("</Row>");
            cnt++;
            if (cnt >= pagerecords)
            {
                sheetcolnum = cnt + 1;
                result += "<Worksheet ss:Name=\"Sheet" + page.ToString() + "\"><Table  ss:ExpandedColumnCount=\"" + colCount.ToString() + "\" ss:ExpandedRowCount=\"" + sheetcolnum.ToString() + "\" x:FullColumns=\"1\"  x:FullRows=\"1\" ss:DefaultColumnWidth=\"104\" ss:DefaultRowHeight=\"13.5\">" + strTotalRows.ToString() + strTitleRow.ToString() + strRows.ToString() + "</Table></Worksheet>";
                strRows.Remove(0, strRows.Length);
                cnt = 1;
                page++;

            }
        }

        

        sheetcolnum = cnt + 1;
        result = result + "<Worksheet ss:Name='Sheet" + page.ToString() + "'><Table  ss:ExpandedColumnCount='" + colCount.ToString() + "' ss:ExpandedRowCount='" + (sheetcolnum+ 6).ToString() + "' x:FullColumns='1'  x:FullRows='1' ss:DefaultColumnWidth='104' ss:DefaultRowHeight='13.5'>" + strTotalRows.ToString() + strTitleRow.ToString() + strRows.ToString() + "</Table></Worksheet></Workbook>";
        return result;
    }


    private static string BuildExportHTML(System.Data.DataTable dt)
    {
        string result = string.Empty;
        int readCnt = dt.Rows.Count;
        int colCount = dt.Columns.Count;

        int pagerecords = 65500;
        result = "<?xml version=\"1.0\"  encoding=\"gb2312\"?>";
        result += "<?mso-application progid=\"Excel.Sheet\"?>";
        result += "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ";
        result += "xmlns:o=\"urn:schemas-microsoft-com:office:office\" ";
        result += "xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ";
        result += "xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" ";
        result += "xmlns:html=\"http://www.w3.org/TR/REC-html40\"> ";
        //以下两部分是可选的
        //result += "<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">";
        //result += "<Author>User</Author>";
        //result += "<LastAuthor>User</LastAuthor>";
        //result += "<Created>2009-03-20T02:15:12Z</Created>";
        //result += "<Company>Microsoft</Company>";
        //result += "<Version>12.00</Version>";
        //result += "</DocumentProperties>";

        //result += "<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">";
        //result += "<WindowHeight>7815</WindowHeight>";
        //result += "<WindowWidth>14880</WindowWidth>";
        //result += "<WindowTopX>240</WindowTopX>";
        //result += "<WindowTopY>75</WindowTopY>";
        //result += "<ProtectStructure>False</ProtectStructure>";
        //result += "<ProtectWindows>False</ProtectWindows>";
        //result += "</ExcelWorkbook>";

        string strTitleRow = "";
        strTitleRow = "<Row  ss:AutoFitHeight='0'>";
        for (int j = 0; j < colCount; j++)
        {
            strTitleRow += "<Cell><Data ss:Type=\"String\">" + dt.Columns[j].ColumnName + "</Data></Cell>";
        }
        strTitleRow += "</Row>";

        StringBuilder strRows = new StringBuilder();
        int page = 1;
        int cnt = 0;
        int sheetcolnum = 0;

        for (int i = 0; i < readCnt; i++)
        {
            strRows.Append("<Row  ss:AutoFitHeight=\"0\">");
            for (int j = 0; j < colCount; j++)
            {

                if (dt.Columns[j].DataType.Name == "DateTime" || dt.Columns[j].DataType.Name == "SmallDateTime")
                {
                    if (dt.Rows[i][j].ToString() != string.Empty)
                    {
                        strRows.Append("<Cell><Data ss:Type=\"String\">" + Convert.ToDateTime(dt.Rows[i][j].ToString()).ToString("yyyy年MM月dd日") + "</Data></Cell>");
                    }
                    else
                        strRows.Append("<Cell><Data ss:Type=\"String\"></Data></Cell>");
                }
                else if (dt.Columns[j].DataType.Name == "Decimal")
                {
                    if (dt.Rows[i][j].ToString() != string.Empty)
                    {
                        strRows.Append("<Cell><Data ss:Type=\"Number\">" + Convert.ToDecimal(dt.Rows[i][j].ToString()) + "</Data></Cell>");
                    }
                    else
                        strRows.Append("<Cell><Data ss:Type=\"Number\"></Data></Cell>");
                }
                else
                {
                    strRows.Append("<Cell><Data ss:Type=\"String\">" + ReplaceString(dt.Rows[i][j].ToString().Trim()) + "</Data></Cell>");
                }
            }
            strRows.Append("</Row>");
            cnt++;
            if (cnt >= pagerecords)
            {
                sheetcolnum = cnt + 1;
                result += "<Worksheet ss:Name=\"Sheet" + page.ToString() + "\"><Table  ss:ExpandedColumnCount=\"" + colCount.ToString() + "\" ss:ExpandedRowCount=\"" + sheetcolnum.ToString() + "\" x:FullColumns=\"1\"  x:FullRows=\"1\" ss:DefaultColumnWidth=\"104\" ss:DefaultRowHeight=\"13.5\">" + strTitleRow.ToString() + strRows.ToString() + "</Table></Worksheet>";
                strRows.Remove(0, strRows.Length);
                cnt = 1;
                page++;

            }
        }
        sheetcolnum = cnt + 1;
        result = result + "<Worksheet ss:Name='Sheet" + page.ToString() + "'><Table  ss:ExpandedColumnCount='" + colCount.ToString() + "' ss:ExpandedRowCount='" + sheetcolnum.ToString() + "' x:FullColumns='1'  x:FullRows='1' ss:DefaultColumnWidth='104' ss:DefaultRowHeight='13.5'>" + strTitleRow.ToString() + strRows.ToString() + "</Table></Worksheet></Workbook>";
        return result;
    }

    #endregion


    /// <summary>
    /// 根据表面和列名查找列的类型
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public string getDataType(string tableName,string columnName)
    {
        string sql = "select A.DATA_TYPE  from ALL_TAB_COLUMNS A WHERE A.TABLE_NAME= '" + tableName + "' AND  A.COLUMN_NAME ='" + columnName + "'";
        Object obj = DbHelperOra.GetSingle(sql, false);
        if (obj != DBNull.Value && obj != null)
        {
            return obj.ToString().ToUpper();
        }
        else
        {
            return string.Empty;
        }
    }

}