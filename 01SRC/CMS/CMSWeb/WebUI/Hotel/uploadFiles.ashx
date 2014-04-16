<%@ WebHandler Language="C#" Class="uploadFiles" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using HotelVp.CMS.Domain.ServiceAdapter;
using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;
using System.Web.SessionState;
using System.Collections;
using System.Configuration;

using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Process;
using System.Web.SessionState;

/// <summary>
/// uploadFiles 的摘要说明
/// </summary>
public class uploadFiles : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        UploadFile(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void UploadFile(HttpContext context)
    {
        context.Response.CacheControl = "no-cache";

        HttpPostedFile file = context.Request.Files["Filedata"];
        //string uploadPath = HttpContext.Current.Server.MapPath(context.Request.Params["folder"]) + "\\";
        string uploadPath = HttpContext.Current.Server.MapPath("UploadFile") + "\\";

        if (file != null)
        {
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string fullname = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            file.SaveAs(string.Format("{0}\\{1}", uploadPath, fullname));

            string extname = Path.GetExtension(file.FileName);
            string filename = fullname;// file.FileName;

            byte[] bit = PhotoToBinary(file);
            
            string hotelID = "";  //folder
            if (context.Request.Params["hotelID"].IndexOf('A') > 0)
            {
                hotelID = context.Request.Params["hotelID"].Substring(0, context.Request.Params["hotelID"].IndexOf('A'));
            }
            else if (context.Request.Params["hotelID"].Contains("folder"))
            {
                hotelID = context.Request.Params["hotelID"].Substring(0, context.Request.Params["hotelID"].IndexOf('f'));
            }
            else
            {
                hotelID = context.Request.Params["hotelID"];
            }
            string cityId = GetCityByHotelID(hotelID);
            
            CallWebPage call = new CallWebPage();
            string imgServer = System.Configuration.ConfigurationManager.AppSettings["ImageServer"].ToString();
            string strJson = call.CallWebByURL(imgServer + "?imageType=" + extname.Replace(".", "") + "&hotelId=" + hotelID + "&cityId=" + cityId + "&imageKind=1&apiVersion=2.2", bit, "POST");//
            if (!string.IsNullOrEmpty(strJson))
            {
                JObject o = JObject.Parse(strJson);
                string picUrl = o.SelectToken("result").SelectToken("url").ToString().Trim('"');
                string HtpPathBak = o.SelectToken("result").SelectToken("urlBak").ToString().Trim('"');

                try
                {
                    ImageEntity _imageEntity = new ImageEntity();
                    _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                    _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                    _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
                    _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

                    _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                    ImageDBEntity imageDBEntity = new ImageDBEntity();
                    imageDBEntity.HotelID = hotelID;//酒店ID 
                    imageDBEntity.Resolution = filename;//图片名
                    imageDBEntity.Source = extname;//后缀名 
                    imageDBEntity.HtpPath = picUrl;//图片路径 
                    imageDBEntity.HtpPathBak = HtpPathBak;//备份路径
                    _imageEntity.ImageDBEntity.Add(imageDBEntity);
                    ImageBP.InsertImage(_imageEntity);
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message.ToString());
                }
            }
            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            context.Response.Write("1"); 
        }
        else
        {
            context.Response.Write("0");
        }
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

    private static string GetCityByHotelID(string hotelId)
    {
        APPContentEntity _appcontentEntity = new APPContentEntity();
        CommonEntity _commonEntity = new CommonEntity();

        _appcontentEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _appcontentEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _appcontentEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _appcontentEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _appcontentEntity.APPContentDBEntity = new List<APPContentDBEntity>();
        APPContentDBEntity appcontentDBEntity = new APPContentDBEntity();
        appcontentDBEntity.HotelID = hotelId;
        _appcontentEntity.APPContentDBEntity.Add(appcontentDBEntity);
        System.Data.DataSet dsResult = APPContentBP.SelectPropByPic(_appcontentEntity).QueryResult;
        return dsResult.Tables[0].Rows[0]["CITYID"].ToString();
    }
}
 