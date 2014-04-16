using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.IO;
using System.Web;

using HotelVp.Common.Json.Linq;
using HotelVp.JobConsole.DataAccess;

namespace HotelVp.JobConsole.ServiceAdapter
{
    public abstract class AutoFaxSA
    {
        public static int QueryFaxList()
        {
            int iCount = 0;
            /////////////////////////////////////接收传真///////////////////////////////////
            FaxService fax = new FaxService();

            string xml = ToServiceXML.getQueryResultForRecvTaskXMLstr(); //拼装xml数据
            string QueryResultForRecvTaskBack = "";
            //string QueryResultForRecvTaskBack1 = "";
            string filePath = ConfigurationManager.AppSettings["UploadFile"].ToString() + "\\";
            while (!String.IsNullOrEmpty(QueryResultForRecvTaskBack = fax.QueryResultForRecvTask(xml)))
            {
                iCount++;
                ; //开始远程调用
                //QueryResultForRecvTaskBack1 = QueryResultForRecvTaskBack.Replace(">", ">\r\n");
                //tb_XML.Text = QueryResultForRecvTaskBack1;

                ///////////////////////////////////解析反馈结果（只接收一个传真）///////////////////////////////////////

                XmlDocument m_XmlDoc = new XmlDocument();
                try
                {
                    m_XmlDoc.LoadXml(QueryResultForRecvTaskBack);
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(m_XmlDoc.NameTable);
                    XmlNodeList nodeList = m_XmlDoc.ChildNodes;
                    XmlNode node = nodeList.Item(1);
                    string ErrorFlag = node.FirstChild.SelectSingleNode("Header").SelectSingleNode("ErrorFlag").InnerText;
                    string ReturnMessage = node.FirstChild.SelectSingleNode("Header").SelectSingleNode("ReturnMessage").InnerText;
                    string ServerTaskID = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("ServerTaskID").InnerText;
                    string FaxNumber = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("FaxNumber").InnerText;
                    string CallingNumber = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("CallingNumber").InnerText;
                    string FaxSID = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("FaxSID").InnerText;
                    string Result = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("Result").InnerText;
                    string StartTime = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("StartTime").InnerText;
                    string CostTime = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("CostTime").InnerText;
                    string NumOfPages = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("NumOfPages").InnerText;
                    string RecvFileName = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("RecvFileName").InnerText;
                    string FileSize = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("FileSize").InnerText;
                    string BillingFee = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("RecvFaxResult").SelectSingleNode("BillingFee").InnerText;
                    string fileName = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("Document").Attributes.GetNamedItem("FileName").Value;
                    string ContentType = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("Document").Attributes.GetNamedItem("ContentType").Value;
                    string EncodingType = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("Document").Attributes.GetNamedItem("EncodingType").Value;
                    string DocumentExtension = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("Document").Attributes.GetNamedItem("DocumentExtension").Value;
                    string filestr = node.FirstChild.SelectSingleNode("QueryResultForRecvTaskResponse").SelectSingleNode("QueryResultForRecvTaskResult").SelectSingleNode("Document").InnerText;
                    string fileUrl = "";
                    filestr = filestr == null ? "" : filestr.Trim();
                    if (!filestr.Equals(""))
                    {
                        ToBase64.SaveDecodingToFile(filestr, filePath + fileName);
                        //file = "文件存放在：" + "d:\\35fax\\" + fileName;
                        fileUrl = UploadFile(filePath + fileName);
                    }

                    AutoReciveFaxDA.SaveAutoReciveFax(fileUrl, "CMSJOB", ServerTaskID, CallingNumber);
                    //string queryResultS = "";
                    //queryResultS = queryResultS + "ErrorFlag :" + ErrorFlag + "\r\n" + "ReturnMessage:" + ReturnMessage + "\r\n" + "ServerTaskID :" + ServerTaskID + "\r\n" +
                    //    "FaxNumber :" + FaxNumber + "\r\n" + "CallingNumber :" + CallingNumber + "\r\n" + "FaxSID :" + FaxSID + "\r\n" + "Result :" + Result + "\r\n" +
                    //    "StartTime :" + StartTime + "\r\n" + "CostTime :" + CostTime + "\r\n" + "NumOfPages :" + NumOfPages + "\r\n" + "RecvFileName :" + RecvFileName + "\r\n" +
                    //    "FileSize :" + FileSize + "\r\n" + "BillingFee :" + BillingFee + "\r\n" + file + "\r\n";
                    //tb_QueryResultForRecvTask.Text = queryResultS;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                }
            }

            return iCount;
        }


        public static string UploadFile(string file)
        {

            //string uploadPath = HttpContext.Current.Server.MapPath(context.Request.Params["folder"]) + "\\";

            if (file != null)
            {
                //if (!Directory.Exists(uploadPath))
                //{
                //    Directory.CreateDirectory(uploadPath);
                //}
                //string fullname = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                //file.SaveAs(string.Format("{0}\\{1}", uploadPath, fullname));

                //string extname = Path.GetExtension(file.FileName);
                //string filename = fullname;// file.FileName;
                FileStream localfile = File.Open(file, FileMode.Open);
                string extname = Path.GetExtension(file);
                //HttpPostedFile postFile = new HttpPostedFile();

                byte[] bit = PhotoToBinary(localfile);

                CallWebPage call = new CallWebPage();
                string imgServer = System.Configuration.ConfigurationManager.AppSettings["ImageServer"].ToString();
                string strJson = call.CallWebByURL(imgServer + "?imageType=" + extname.Replace(".", "") + "&imageKind=4&apiVersion=2.2", bit, "POST");//
                if (!string.IsNullOrEmpty(strJson))
                {
                    JObject o = JObject.Parse(strJson);
                    string picUrl = o.SelectToken("result").SelectToken("url").ToString().Trim('"');
                    string HtpPathBak = o.SelectToken("result").SelectToken("urlBak").ToString().Trim('"');
                    return picUrl;
                    //try
                    //{
                    //    //ImageEntity _imageEntity = new ImageEntity();
                    //    //_imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                    //    //_imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                    //    //_imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
                    //    //_imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

                    //    //_imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                    //    //ImageDBEntity imageDBEntity = new ImageDBEntity();
                    //    //imageDBEntity.HotelID = hotelID;//酒店ID 
                    //    //imageDBEntity.Resolution = filename;//图片名
                    //    //imageDBEntity.Source = extname;//后缀名 
                    //    //imageDBEntity.HtpPath = picUrl;//图片路径 
                    //    //imageDBEntity.HtpPathBak = HtpPathBak;//备份路径
                    //    //_imageEntity.ImageDBEntity.Add(imageDBEntity);
                    //    //ImageBP.InsertImage(_imageEntity);
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.Write(ex.Message.ToString());
                    //}
                }
                else
                {
                    return "";
                }
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            }
            else
            {
                return "";
            }
        }


        public static byte[] PhotoToBinary(FileStream file)
        {
            int mlength = int.Parse(file.Length.ToString());

            System.IO.Stream StreamObject = file;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();


            byte[] temp = new byte[mlength];
            StreamObject.Read(temp, 0, mlength);
            ms.Write(temp, 0, mlength);

            StreamObject.Close();
            byte[] content = ms.ToArray();
            return content;
        }

        public static byte[] PhotoToBinary(HttpPostedFile file)
        {
            HttpPostedFile UpFile = file;
            int mlength = UpFile.ContentLength;

            System.IO.Stream StreamObject = UpFile.InputStream;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();


            byte[] temp = new byte[mlength];
            StreamObject.Read(temp, 0, mlength);
            ms.Write(temp, 0, mlength);

            StreamObject.Close();
            byte[] content = ms.ToArray();
            return content;
        }

    }
}
