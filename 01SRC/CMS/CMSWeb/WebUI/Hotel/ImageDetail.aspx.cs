using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;

using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using System.Web.Services;

public partial class ImageDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
            string roomTypeName = Request.QueryString["roomTypeName"] == null ? "" : Request.QueryString["roomTypeName"].ToString();
            this.HidRQRoomCode.Value = roomTypeName;
            BindData(id);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string hotelId = Request.QueryString["hotelId"] == null ? "" : Request.QueryString["hotelId"].ToString();
        string id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
        string picType = this.ddlPicType.Value;
        string roomCode = this.ddlRoomList.SelectedValue;

        ImageEntity _imageEntity = new ImageEntity();
        _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
        ImageDBEntity imageDBEntity = new ImageDBEntity();

        if (this.chkCover.Checked)
        {
            //原来的封面图片会自动变成普通酒店图片
            imageDBEntity.HotelID = HidHotelID.Value;
            _imageEntity.ImageDBEntity.Add(imageDBEntity);
            ImageBP.UpdateSupImageDetailsByID(_imageEntity);// 将原封面图  取消 
            _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
            imageDBEntity = new ImageDBEntity();
            imageDBEntity.IsCover = "1";
        }

        if (this.HidIsRoomType.Value == "1")
        {
            //清除原来房型图和Room的关联
            imageDBEntity.HotelID = hotelId;
            imageDBEntity.RoomTypeCode = this.HidRoomCode.Value;
            _imageEntity.ImageDBEntity.Add(imageDBEntity);
            DataTable dtImageID = ImageBP.SelectTRoomImageIDByHotelID(_imageEntity);
            if (dtImageID.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtImageID.Rows[0]["IMAGE_ID"].ToString()))
                {
                    _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                    imageDBEntity = new ImageDBEntity();
                    imageDBEntity.Id = "";
                    imageDBEntity.HotelID = hotelId;
                    imageDBEntity.RoomTypeCode = this.HidRoomCode.Value;
                    _imageEntity.ImageDBEntity.Add(imageDBEntity);
                    ImageBP.UpdateTRoomByHotelID(_imageEntity);
                    _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                    imageDBEntity = new ImageDBEntity();
                }
            }
        }

        //重新设置图片信息
        imageDBEntity.Id = id;
        imageDBEntity.ImgType = picType;
        imageDBEntity.IsCover = this.chkCover.Checked == true ? "1" : "0";
        _imageEntity.ImageDBEntity.Add(imageDBEntity);
        ImageBP.UpdateSupImageDetailsRepeatByID(_imageEntity);
        if (picType == "1")
        {
            _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
            imageDBEntity = new ImageDBEntity();
            //将原来的房型图片 ImageType改为普通酒店图片   (其他)
            imageDBEntity.HotelID = hotelId;
            imageDBEntity.RoomTypeCode = roomCode;
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
                }
            }

            _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
            imageDBEntity = new ImageDBEntity();
            imageDBEntity.Id = id;
            imageDBEntity.HotelID = HidHotelID.Value;
            imageDBEntity.RoomTypeCode = roomCode;
            _imageEntity.ImageDBEntity.Add(imageDBEntity);
            ImageBP.UpdateTRoomByHotelID(_imageEntity);
        }

        Response.Write("<script>window.returnValue=true;window.opener = null;window.close();</script>");
    }

    private void BindData(string id)
    {
        ImageEntity _imageEntity = new ImageEntity();
        _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
        ImageDBEntity imageDBEntity = new ImageDBEntity();
        imageDBEntity.Id = id;

        _imageEntity.ImageDBEntity.Add(imageDBEntity);
        DataSet dsResult = ImageBP.GetSupImageByID(_imageEntity);
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            string hotelID = dsResult.Tables[0].Rows[0]["HOTEL_ID"].ToString();
            string imgurl = dsResult.Tables[0].Rows[0]["HTP_PATH"].ToString();
            string imgType = dsResult.Tables[0].Rows[0]["IMG_TYPE"].ToString();
            //string roomCode = dsResult.Tables[0].Rows[0]["ROOM_TYPE_CODE"].ToString();
            string roomCode = dsResult.Tables[0].Rows[0]["ROOM_CODE"].ToString();
            string isCover = dsResult.Tables[0].Rows[0]["IS_COVER"].ToString();

            this.HidHotelID.Value = hotelID;
            this.HidRoomCode.Value = roomCode;

            this.ddlPicType.Value = imgType;
            if (imgType == "1")
            {
                this.RoomListDiv.Attributes["style"] = "display:''";
                this.HidIsRoomType.Value = "1";
            }

            if (isCover == "1")
            {
                this.chkCover.Checked = true;
            }

            this.ImgSrc.Src = imgurl.Replace("_100_75", "_400_300");
            this.spanUrl.InnerHtml = imgurl.Replace("_100_75", "");

            GetRoomNameByHotelID(hotelID);
            this.ddlRoomList.SelectedValue = roomCode;
        }
    }

    private void GetRoomNameByHotelID(string hotelId)
    {
        HotelInfoEntity _hotelinfoEntity = new HotelInfoEntity();
        _hotelinfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _hotelinfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _hotelinfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _hotelinfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _hotelinfoEntity.HotelInfoDBEntity = new List<HotelInfoDBEntity>();
        HotelInfoDBEntity hotelInfoDBEntity = new HotelInfoDBEntity();
        hotelInfoDBEntity.HotelID = hotelId;
        _hotelinfoEntity.HotelInfoDBEntity.Add(hotelInfoDBEntity);
        DataSet dsResult = HotelInfoBP.GetHotelRoomList(_hotelinfoEntity).QueryResult;

        DataRow drTempcity = dsResult.Tables[0].NewRow();
        drTempcity["ROOMCD"] = "-1";
        drTempcity["ROOMNM"] = "不限制";
        dsResult.Tables[0].Rows.InsertAt(drTempcity, 0);

        ddlRoomList.DataTextField = "ROOMNM";
        ddlRoomList.DataValueField = "ROOMCD";
        ddlRoomList.DataSource = dsResult;
        ddlRoomList.DataBind();
        ddlRoomList.SelectedIndex = 0;
    }


    [WebMethod]
    public static string IsExits(string roomCode)
    {
        return "1";
    }

}