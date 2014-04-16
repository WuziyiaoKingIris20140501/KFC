using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;
using System.Web.Security;

using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Process;
using System.Web.SessionState;
using HotelVp.CMS.Domain.ServiceAdapter;

public partial class WebUI_Hotel_PhotoPatchUploadNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.AspSessID.Value = Request.Cookies[FormsAuthentication.FormsCookieName] == null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value;

            this.HiddenField1.Value = Session.SessionID;

            HidHotelID.Value = Request.QueryString["hotelId"];
            HidCityID.Value = Request.QueryString["cityId"];
        }
    }

    protected void btnOver_Click(object sender, EventArgs e)
    {
        int num = 0;
        string hotelId = Request.QueryString["hotelId"].ToString();
        DataTable dtRoom = GetHotelRoomList(hotelId);
        PicUpload.Attributes.Add("style", "display:none");
        DataTable dtImg = getCMSImage(hotelId);
        if (dtImg != null && dtImg.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style=\"text-align: center;\"><table  border=\"1\"  style=\"width: 500px;height:100%;margin: auto;background: #EDEDED; border: 1 solid  #AAAAAA; border-collapse:collapse;overflow-y:auto;\">");//style="background: #EDEDED; border: 1 solid  #AAAAAA; border-collapse:collapse"
            sb.Append("<tr style=\"height:40px;\">");
            sb.Append("<td style=\"width: 60px;\">封面</td>");
            sb.Append("<td style=\"width: 160px;\">图片</td>");
            sb.Append("<td style=\"width: 410px;\">文件信息</td>");
            sb.Append("</tr>");
            for (int i = 0; i < dtImg.Rows.Count; i++)
            {
                string fileName = dtImg.Rows[i]["imgName"].ToString();//图片名称
                string extName = dtImg.Rows[i]["extName"].ToString();//图片后缀名
                string imgPath = dtImg.Rows[i]["imgPath"].ToString();//图片路径 
                sb.Append("<tr style=\"height:90px;\">");
                sb.Append("<td><input type=\"radio\" id=\"CoverPic" + num + "\" name=\"CoverPic\" border=\"1\" runat=\"server\" /></td>");
                //sb.Append("<td><img id=\"ImgSrc" + num + "\" src=\"" + imgPath.Replace(extName, "_100_75" + extName) + "\"  /></td>");
                sb.Append("<td><img id=\"ImgSrc" + num + "\" src=\"" + imgPath.Replace(extName, extName + "?imageView/1/w/100/h/75") + "\"  /></td>");
                sb.Append("<td>");
                sb.Append("<table width=\"100%\">");
                sb.Append("<tr >");
                sb.Append("<td align=\"left\" colspan=\"2\">文件名:<label id=\"fileName" + num + "\" runat=\"server\">" + fileName + "</label></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td align=\"left\">图片类型:<select id=\"picType" + num + "\" name=\"picType\" runat=\"server\" onchange=\"checkRoomType(this," + num + ")\">");
                sb.Append("<option value=\"-1\">请选择</option>");
                sb.Append("<option value=\"2\">酒店外观</option>");
                sb.Append("<option value=\"1\">房型图片</option>");
                sb.Append("<option value=\"4\">酒店大堂</option>");
                sb.Append("<option value=\"5\">餐饮娱乐</option>");
                sb.Append("<option value=\"6\">酒店图标</option>");
                sb.Append("<option value=\"7\">其他图片</option>");
                sb.Append("</select>");
                sb.Append("</td>");
                sb.Append("<td><div id=\"roomTypeDiv" + num + "\" style=\"display: none\">房型:<select id=\"roomType" + num + "\" runat=\"server\">");
                sb.Append("<option value=\"-1\">请选择</option>");
                for (int j = 0; j < dtRoom.Rows.Count; j++)
                {
                    sb.Append("<option value=\"" + dtRoom.Rows[j]["ROOMCD"].ToString() + "\">" + dtRoom.Rows[j]["ROOMNM"].ToString() + "</option>");
                }
                sb.Append("</select><div>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<input type=\"hidden\" id=\"htpPathBak" + num + "\"  runat=\"server\" value=\"" + dtImg.Rows[i]["urlBak"].ToString() + "\" />");
                num += 1;
            }
            sb.Append("</table></div>");
            upLoadPic.Attributes["style"] = "display: block; margin-left:55px;display:''";
            uploadBtnDiv.Attributes["style"] = "display:''";

            this.upLoadPicDiv.InnerHtml = sb.ToString();

            //Session.Remove("PicList");
            //Session.Remove("PicHt");  

            HiddenNum.Value = num.ToString();
        }
        deleteCMSImage(hotelId);
    }

    private DataTable GetHotelRoomList(string hotelID)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        CommonEntity _commonEntity = new CommonEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hotelID;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetHotelRoomList(_hotelinfoEntity).QueryResult;
        return dsResult.Tables[0];
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ImageEntity _imageEntity = new ImageEntity();
        _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        string hotelId = Request.QueryString["hotelId"].ToString();

        string Answer = this.HiddenAnswer.Value;
        string[] strAnswer = Answer.Split('|');
        for (int i = 0; i < strAnswer.Length; i++)
        {
            if (!string.IsNullOrEmpty(strAnswer[i]))
            {
                _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                ImageDBEntity imageDBEntity = new ImageDBEntity();


                string[] strAnswerChild = strAnswer[i].Split(',');


                string CoverPic = strAnswerChild[0].Split('&')[1].ToString();//封面图
                //http://hotelvp.qiniudn.com/hotels/Shanghai/10003/8603413e9d1fa4a1.jpg?imageView/1/w/100/h/75
                string ImgSrc = strAnswerChild[1].Split('&')[1].ToString();//图片路径
                ImgSrc = ImgSrc.Substring(0,ImgSrc.LastIndexOf("?"));

                string fileName = strAnswerChild[2].Split('&')[1].ToString();//图片名称

                string picTypeValue = strAnswerChild[3].Split('&')[1].ToString();//1房型图片、2酒店外观、3促销图片、4酒店大堂、5餐饮娱乐、6酒店图标、7其他

                string roomTypevalue = strAnswerChild[4].Split('&')[1].ToString();//房型Code

                string htpPathBak = strAnswerChild[5].Split('&')[1].ToString();//房型Code

                //清除酒店现有封面图
                if (CoverPic == "true")
                {
                    imageDBEntity.HotelID = hotelId;
                    _imageEntity.ImageDBEntity.Add(imageDBEntity);
                    ImageBP.UpdateSupImageDetailsByID(_imageEntity);
                    _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                    imageDBEntity = new ImageDBEntity();
                }
                //设置当前图片信息
                imageDBEntity.ImgType = picTypeValue;
                imageDBEntity.HtpPath = ImgSrc;
                imageDBEntity.Source = "HOTELVP";
                imageDBEntity.IsCover = CoverPic == "true" ? "1" : "0";
                imageDBEntity.HotelID = hotelId;
                //imageDBEntity.RoomTypeCode = roomTypevalue;
                imageDBEntity.HtpPathBak = htpPathBak;
                imageDBEntity.Seq = i;
                imageDBEntity.CreateTime = System.DateTime.Now;
                _imageEntity.ImageDBEntity.Add(imageDBEntity);
                int TImageID = ImageBP.TSupImageCheckInsert(_imageEntity);
                //如果为房型图   入T_Room表
                if (picTypeValue == "1")
                {
                    //判断当前酒店 当前房型是否已设置房型图片   
                    _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                    imageDBEntity = new ImageDBEntity();
                    imageDBEntity.HotelID = hotelId;
                    imageDBEntity.RoomTypeCode = roomTypevalue;
                    _imageEntity.ImageDBEntity.Add(imageDBEntity);
                    DataTable dtImageID = ImageBP.SelectTRoomImageIDByHotelID(_imageEntity);
                    if (dtImageID.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtImageID.Rows[0]["IMAGE_ID"].ToString()))
                        {
                            _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                            imageDBEntity = new ImageDBEntity();
                            imageDBEntity.Id = dtImageID.Rows[0]["IMAGE_ID"].ToString();
                            imageDBEntity.ImgType = "7";
                            imageDBEntity.IsCover = "0";
                            _imageEntity.ImageDBEntity.Add(imageDBEntity);
                            ImageBP.UpdateSupImageDetailsRepeatByID(_imageEntity);
                            _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                            imageDBEntity = new ImageDBEntity();
                        }
                    }

                    _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                    imageDBEntity = new ImageDBEntity();
                    imageDBEntity.Id = TImageID.ToString();
                    imageDBEntity.HotelID = hotelId;
                    imageDBEntity.RoomTypeCode = roomTypevalue;
                    _imageEntity.ImageDBEntity.Add(imageDBEntity);
                    ImageBP.UpdateTRoomByHotelID(_imageEntity);
                }


                //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "window.opener.location.reload();window.opener=null;window.close();", true);

            }
        }
        HotelInfoSA.RefushHotelList(hotelId);//MQ 刷新酒店索引信息

        if (Request.Params["flag"] != null && Request.Params["flag"].ToString() == "true")
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "window.opener.document.getElementById('MainContent_btnRefresh').click();;window.opener=null;window.close();", true);
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "window.opener.document.getElementById('btnRefresh').click();;window.opener=null;window.close();", true);
        }
    }

    public DataTable getCMSImage(string hotelID)
    {
        ImageEntity _imageEntity = new ImageEntity();
        _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
        ImageDBEntity imageDBEntity = new ImageDBEntity();
        imageDBEntity.HotelID = hotelID;
        _imageEntity.ImageDBEntity.Add(imageDBEntity);
        return ImageBP.GetImageByHotelID(_imageEntity);
    }

    public int deleteCMSImage(string hotelID)
    {
        ImageEntity _imageEntity = new ImageEntity();
        _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
        ImageDBEntity imageDBEntity = new ImageDBEntity();
        imageDBEntity.HotelID = hotelID;
        _imageEntity.ImageDBEntity.Add(imageDBEntity);
        return ImageBP.DeleteImage(_imageEntity);

    }
}