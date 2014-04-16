using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class PictureInfoManager : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["PicHt"] == null)
        //{
        //    Hashtable ht = new Hashtable();
        //    Session["PicHt"] = ht;

        if (Request.HttpMethod.ToLower() == "post" && Request.Form["sort[0][id]"] != null && Request.Params["type"] != null)
        {
            if (Request.Params["type"].ToString() == "0")
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Request.Form["sort[" + i + "][order][]"] != null)
                    {
                        string[] ids = Request.Form["sort[" + i + "][id]"].ToString().Split(',');
                        string[] orders = Request.Form["sort[" + i + "][order][]"].ToString().Split(',');
                        string[] types = Request.Form["sort[" + i + "][type]"].ToString().Split(',');
                        string[] hotelID = Request.Form["sort[" + i + "][hotelID]"].ToString().Split(',');
                        Renew(ids, orders, types, hotelID);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 1; i++)
                {
                    if (Request.Form["sort[" + i + "][order][]"] != null)
                    {
                        string[] ids = Request.Form["sort[" + i + "][id]"].ToString().Split(',');
                        string[] orders = Request.Form["sort[" + i + "][order][]"].ToString().Split(',');
                        string[] types = Request.Form["sort[" + i + "][type]"].ToString().Split(',');
                        string[] hotelID = Request.Form["sort[" + i + "][hotelID]"].ToString().Split(',');
                        RenewSeq(ids, orders, types, hotelID);
                    }
                }
            }
        }
        if (Request.HttpMethod.ToLower() == "post" && Request.Form["type"] != null && Request.Form["type"] == "DeletePic")
        {
            string id = Request.Form["id"] == null ? "" : Request.Form["id"].ToString();
            DeletePic(id);
        }
        //}
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string strHotel = wctHotel.AutoResult.ToString();
        if (strHotel != "")
        {
            HidHotelID.Value = strHotel.Substring((strHotel.IndexOf('[') + 1), (strHotel.IndexOf(']') - 1));
            lbHotelNM.Text = HidHotelID.Value + " - " + strHotel.Substring(strHotel.IndexOf(']') + 1);

            HidCityID.Value = GetCityByHotelID(HidHotelID.Value);
            //dtCoverPicDiv.Attributes["style"] = "margin: 15px 14px 15px 14px;display:''";
            //dtHotelPicDiv.Attributes["style"] = "margin: 15px 14px 15px 14px;display:''";
            //dtRoomTypePicDiv.Attributes["style"] = "margin: 15px 14px 15px 14px;display:''";
            BindPic("1");
        }
    }

    private string GetCityByHotelID(string hotelId)
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
        DataSet dsResult = APPContentBP.SelectPropByPic(_appcontentEntity).QueryResult;
        return dsResult.Tables[0].Rows[0]["CITYID"].ToString();
    }

    private DataSet GetRoomNameByHotelID(string hotelId)
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
        return dsResult;
    }

    public void BindPic(string type)
    {
        detailMessageContent.InnerHtml = "";
        ImageEntity _imageEntity = new ImageEntity();
        _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
        ImageDBEntity imageDBEntity = new ImageDBEntity();
        //if (type == "1")
        //    imageDBEntity.HotelID = HidHotelID.Value;
        //else
        //    imageDBEntity.HotelID = Session["hotelID"].ToString();

        imageDBEntity.HotelID = HidHotelID.Value;
        //Session["hotelID"] = HidHotelID.Value;
        //ViewState["hotelID"] = HidHotelID.Value;
        _imageEntity.ImageDBEntity.Add(imageDBEntity);
        DataTable dtPic = ImageBP.GetSupImageByHotelID(_imageEntity).Tables[0];


        if (dtPic.Rows.Count > 0)
        {
            string dtCoverPicIds = "";
            StringBuilder sbCoverPic = new StringBuilder();
            //IS_COVER   0:普通图片 1:封面图
            //DataRow[] dtCoverPic = dtPic.Select("IS_COVER='1'");//所有封面图  
            DataRow[] dtCoverPic = dtPic.Select("IS_COVER='1'");//所有封面图  
            sbCoverPic.Append("<ul><li class=\"title\">封面图片</li><li>");
            sbCoverPic.Append("<ul id=\"sortable1\" class=\"connectedSortable\">");

            ////新增
            if (dtCoverPic.Length > 0)
            {
                sbCoverPic.Append("<li class=\"ui-state-default\" id=\"specialID\" runat=\"server\" title=\"-1\" style=\"width: 1px; border-width: 0px; background-color: White; border-color: White; margin-left: 0px; margin-right: 0px; padding: 0px; margin-bottom: 0px;\">");
                sbCoverPic.Append("<div style=\"width: 1px; height: 90px; position: relative; border: 0px;background-repeat: no-repeat;\">");
                sbCoverPic.Append("</div>");
                sbCoverPic.Append("</li>");
            }
            else
            {
                sbCoverPic.Append("<li class=\"ui-state-default\" id=\"specialID\" runat=\"server\" title=\"-1\" style=\"width: 1px; border-width: 0px; background-color: White; border-color: White; margin-left: 200px; margin-right: 0px; padding: 0px; margin-bottom: 0px;\">");
                sbCoverPic.Append("<div style=\"width: 1px; height: 90px; position: relative; border: 0px;background-repeat: no-repeat;\">");
                sbCoverPic.Append("</div>");
                sbCoverPic.Append("</li>");
            }
            //以上新增

            for (int i = 0; i < dtCoverPic.Length; i++)
            {
                dtCoverPicIds += dtCoverPic[i]["ID"].ToString() + ",";
                sbCoverPic.Append("<li class=\"ui-state-default\" title=\"" + dtCoverPic[i]["ID"].ToString() + "\">");
                //sbCoverPic.Append("<img src=\"" + dtCoverPic[i]["HTP_PATH"].ToString() + "\" width=\"120\" height=\"90\" />" + dtCoverPic[i]["ID"].ToString() + "");

                sbCoverPic.Append("<div style=\"width: 120px; height: 90px; position: relative; border: 0px;background-repeat: no-repeat;\">");
                sbCoverPic.Append("<div style=\"position: absolute; top: -5px; right: -5px;border: 0px;\">");
                sbCoverPic.Append("<img src=\"../../Styles/images/delete.png\" style=\"border: 0px;\" onclick=\"deletePic(" + dtCoverPic[i]["ID"].ToString() + ")\" />");
                sbCoverPic.Append("</div><img src=\"" + dtCoverPic[i]["HTP_PATH"].ToString() + "\" width=\"120\" height=\"90\" /></div>");
                sbCoverPic.Append("<span style=\"font-size:small;\">" + dtCoverPic[i]["IMG_TYPE"].ToString().Replace("0", "其他图片").Replace("1", "房型图片").Replace("2", "酒店外观").Replace("3", "促销图片").Replace("4", "酒店大堂").Replace("5", "餐饮娱乐").Replace("6", "酒店图标").Replace("7", "其他图片") + "</span>");
                sbCoverPic.Append("<a onclick=\"updatePic(" + dtCoverPic[i]["ID"].ToString() + "," + dtCoverPic[i]["HOTEL_ID"].ToString() + ")\" style=\"font-size:small;float:right;cursor:pointer\">[编辑]</a></div>");

                sbCoverPic.Append("</li>");
            } 

            sbCoverPic.Append("</ul></li></ul>");
            dtCoverPicDiv.InnerHtml = sbCoverPic.ToString();
            dtCoverPicIds = dtCoverPicIds.TrimEnd(',');
            HidCoverPicIds.Value = dtCoverPicIds;

            //所有酒店图片 
            string dtHotelPicIds = "";
            StringBuilder sbHotelPic = new StringBuilder();
            DataRow[] dtHotelPic = dtPic.Select("IMG_TYPE<>'1' and IS_COVER<>'1'");
            sbHotelPic.Append("<ul><li class=\"title\">酒店图片</li><li>");
            sbHotelPic.Append("<ul id=\"sortable2\" class=\"connectedSortable\">");

            //新增
            sbHotelPic.Append("<li class=\"ui-state-default\" title=\"-2\" style=\"width: 1px; border-width: 0px; background-color: White; border-color: White; margin-left: 0px; margin-right: 0px; padding: 0px; margin-bottom: 0px;\">");
            sbHotelPic.Append("<div style=\"width: 1px; height: 90px; position: relative; border: 0px;background-repeat: no-repeat;\">");
            sbHotelPic.Append("</div>");
            sbHotelPic.Append("</li>");
            //以上新增

            for (int i = 0; i < dtHotelPic.Length; i++)
            {
                dtHotelPicIds += dtHotelPic[i]["ID"].ToString() + ",";
                sbHotelPic.Append("<li class=\"ui-state-default\" title=\"" + dtHotelPic[i]["ID"].ToString() + "\">");
                //sbHotelPic.Append("<img src=\"" + dtHotelPic[i]["HTP_PATH"].ToString() + "\" width=\"120\" height=\"90\" /><br/>" + dtHotelPic[i]["IMG_TYPE"].ToString().Replace("0", "请选择").Replace("1", "房型图片").Replace("2", "酒店外观").Replace("3", "促销图片").Replace("4", "酒店大堂").Replace("5", "餐饮娱乐").Replace("6", "酒店图标").Replace("7", "其他") + "");
                //sbHotelPic.Append("<img src=\"" + dtHotelPic[i]["HTP_PATH"].ToString() + "\" width=\"120\" height=\"90\" /><br/>" + dtHotelPic[i]["ID"].ToString() + "");
                sbHotelPic.Append("<div style=\"width: 120px; height: 90px; position: relative; border: 0px;background-repeat: no-repeat;\" >");
                sbHotelPic.Append("<div style=\"position: absolute; top: -5px; right: -5px;border: 0px;\">");
                sbHotelPic.Append("<img src=\"../../Styles/images/delete.png\" style=\"border: 0px;\" onclick=\"deletePic(" + dtHotelPic[i]["ID"].ToString() + ")\" />");
                sbHotelPic.Append("</div><img src=\"" + dtHotelPic[i]["HTP_PATH"].ToString() + "\" width=\"120\" height=\"90\" />");
                //sbHotelPic.Append(dtHotelPic[i]["IMG_TYPE"].ToString().Replace("0", "其他图片").Replace("1", "房型图片").Replace("2", "酒店外观").Replace("3", "促销图片").Replace("4", "酒店大堂").Replace("5", "餐饮娱乐").Replace("6", "酒店图标").Replace("7", "其他图片"));
                sbHotelPic.Append("<span style=\"font-size:small;\">" + dtHotelPic[i]["IMG_TYPE"].ToString().Replace("0", "其他图片").Replace("1", "房型图片").Replace("2", "酒店外观").Replace("3", "促销图片").Replace("4", "酒店大堂").Replace("5", "餐饮娱乐").Replace("6", "酒店图标").Replace("7", "其他图片") + "</span>");
                sbHotelPic.Append("<a onclick=\"updatePic(" + dtHotelPic[i]["ID"].ToString() + "," + dtHotelPic[i]["HOTEL_ID"].ToString() + ")\" style=\"font-size:small;float:right;cursor:pointer\">[编辑]</a></div>");

                sbHotelPic.Append("</li>");
            }
            sbHotelPic.Append("</ul></li></ul>");
            dtHotelPicDiv.InnerHtml = sbHotelPic.ToString();
            dtHotelPicIds = dtHotelPicIds.TrimEnd(',');
            HidHotelPicIds.Value = dtHotelPicIds;

            //所有房型图片
            //IMG_TYPE   1房型图片、2酒店外观、3促销图片、4酒店大堂、5餐饮娱乐、6酒店图标、7其他
            string roomTypeName = "";
            string dtRoomTypePicIds = "";
            DataSet dsRoomList = GetRoomNameByHotelID(HidHotelID.Value);
            StringBuilder sbRoomTypePic = new StringBuilder();
            //DataRow[] dtRoomTypePic = dtPic.Select("IMG_TYPE='1'");
            DataRow[] dtRoomTypePic = dtPic.Select("IMG_TYPE='1'");
            sbRoomTypePic.Append("<ul><li class=\"title\">房型图片</li><li>");
            sbRoomTypePic.Append("<ul id=\"sortable3\" class=\"\">");
            for (int i = 0; i < dtRoomTypePic.Length; i++)
            {
                dtRoomTypePicIds += dtRoomTypePic[i]["ID"].ToString() + ",";
                sbRoomTypePic.Append("<li class=\"ui-state-default\" title=\"" + dtRoomTypePic[i]["ID"].ToString() + "\">");

                //if (dsRoomList.Tables.Count > 0 && dsRoomList.Tables[0].Rows.Count > 0)
                //{
                //    for (int j = 0; j < dsRoomList.Tables[0].Rows.Count; j++)
                //    {
                //        if (dsRoomList.Tables[0].Rows[j]["ROOMCD"].ToString() == dtRoomTypePic[i]["ROOM_TYPE_CODE"].ToString())
                //        {
                //            roomTypeName = dsRoomList.Tables[0].Rows[j]["ROOMNM"].ToString();
                //        }
                //    }
                //}

                //sbRoomTypePic.Append("<img src=\"" + dtRoomTypePic[i]["HTP_PATH"].ToString() + "\" width=\"120\" height=\"90\" /><br/>" + roomTypeName + "");
                //sbRoomTypePic.Append("<img src=\"" + dtRoomTypePic[i]["HTP_PATH"].ToString() + "\" width=\"120\" height=\"90\" /><br/>" + dtRoomTypePic[i]["ID"].ToString() + "");
                sbRoomTypePic.Append("<div style=\"width: 120px; height: 90px; position: relative; border: 0px;background-repeat: no-repeat;\" >");
                sbRoomTypePic.Append("<div style=\"position: absolute; top: -5px; right: -5px;border: 0px;\">");
                sbRoomTypePic.Append("<img src=\"../../Styles/images/delete.png\" style=\"border: 0px;\" onclick=\"deletePic(" + dtRoomTypePic[i]["ID"].ToString() + ")\" />");
                sbRoomTypePic.Append("</div><img src=\"" + dtRoomTypePic[i]["HTP_PATH"].ToString() + "\" width=\"120\" height=\"90\" />");
                //sbRoomTypePic.Append("<span style=\"font-size:small;margin-top:5px;\">" + roomTypeName + "</span>");
                sbRoomTypePic.Append("<span style=\"font-size:small;margin-top:5px;\">" + dtRoomTypePic[i]["ROOM_NAME"].ToString() + "</span>");
                sbRoomTypePic.Append("<a onclick=\"updatePic(" + dtRoomTypePic[i]["ID"].ToString() + "," + dtRoomTypePic[i]["HOTEL_ID"].ToString() + ")\" style=\"font-size:small;float:right;cursor:pointer\">[编辑]</a></div>");

                sbRoomTypePic.Append("</li>");
                if (!string.IsNullOrEmpty(dtRoomTypePic[i]["ROOM_CODE"].ToString()))
                {
                    roomTypeName = roomTypeName + dtRoomTypePic[i]["ROOM_CODE"].ToString() + ",";
                }
            }
            sbRoomTypePic.Append("</ul></li></ul>");
            dtRoomTypePicDiv.InnerHtml = sbRoomTypePic.ToString();
            dtRoomTypePicIds = dtRoomTypePicIds.TrimEnd(',');
            HidRoomTypePicIds.Value = dtRoomTypePicIds;
            this.HidRoomTypeName.Value = roomTypeName;

            dtCoverPicDiv.Attributes["style"] = "margin: 15px 14px 15px 14px;display:''";
            dtHotelPicDiv.Attributes["style"] = "margin: 15px 14px 15px 14px;display:''";
            dtRoomTypePicDiv.Attributes["style"] = "margin: 15px 14px 15px 14px;display:''";

        }
        else
        {
            dtCoverPicDiv.InnerHtml = "";
            dtCoverPicDiv.Attributes.Add("style", "margin: 15px 14px 15px 14px;display:none");
            dtHotelPicDiv.InnerHtml = "";
            dtHotelPicDiv.Attributes.Add("style", "margin: 15px 14px 15px 14px;display:none");
            dtRoomTypePicDiv.InnerHtml = "";
            dtRoomTypePicDiv.Attributes.Add("style", "margin: 15px 14px 15px 14px;display:none");

            detailMessageContent.InnerHtml = "当前酒店无图片！！！";

        }
    }

    private void Renew(string[] ids, string[] orders, string[] types, string[] hotelID)
    {
        ImageEntity _imageEntity = new ImageEntity();
        _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
        ImageDBEntity imageDBEntity = new ImageDBEntity();

        for (int i = 0; i < orders.Length; i++)
        {
            if (types[0] == "sortable1")
            {
                if (orders[i].ToString() != "-1")
                {
                    imageDBEntity.HotelID = hotelID[0].ToString();
                    _imageEntity.ImageDBEntity.Add(imageDBEntity);
                    ImageBP.UpdateSupImageDetailsByID(_imageEntity);// 将原封面图  取消 

                    _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
                    imageDBEntity = new ImageDBEntity();
                    imageDBEntity.Id = orders[i].ToString();
                    imageDBEntity.IsCover = "1";
                    imageDBEntity.Seq = 0;

                    _imageEntity.ImageDBEntity.Add(imageDBEntity);
                    ImageBP.RenewSupCoverImageByID(_imageEntity);
                    return;
                }
                else
                {
                    imageDBEntity.HotelID = hotelID[0].ToString();
                    _imageEntity.ImageDBEntity.Add(imageDBEntity);
                    ImageBP.UpdateSupImageDetailsByID(_imageEntity);// 将原封面图  取消 
                }
            }
            else
            {
                imageDBEntity.Id = orders[i].ToString();
                imageDBEntity.Seq = i;
                imageDBEntity.IsCover = "0";
                _imageEntity.ImageDBEntity.Add(imageDBEntity);
                ImageBP.RenewSupImageByID(_imageEntity);
            }
        }
    }

    public void RenewSeq(string[] ids, string[] orders, string[] types, string[] hotelID)
    {
        ImageEntity _imageEntity = new ImageEntity();
        _imageEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _imageEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _imageEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _imageEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _imageEntity.ImageDBEntity = new List<ImageDBEntity>();
        ImageDBEntity imageDBEntity = new ImageDBEntity();
        for (int i = 0; i < orders.Length; i++)
        {
            imageDBEntity.Id = orders[i].ToString();
            imageDBEntity.Seq = i;
            imageDBEntity.IsCover = "0";
            _imageEntity.ImageDBEntity.Add(imageDBEntity);
            ImageBP.RenewSupImageByID(_imageEntity);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindPic("0");
    }

    private void DeletePic(string id)
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
        ImageBP.DeleteSupImageByID(_imageEntity);
    }
}