using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HotelVp.CMS.Domain.Entity
{
    public class ImageEntity : BaseEntity
    {
        private List<ImageDBEntity> imagedbentity;
        public List<ImageDBEntity> ImageDBEntity
        {
            get { return imagedbentity; }
            set { imagedbentity = value; }
        }
    }

    public class ImageDBEntity
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string imgType;
        public string ImgType
        {
            get { return imgType; }
            set { imgType = value; }
        }

        private string htpPath;
        public string HtpPath
        {
            get { return htpPath; }
            set { htpPath = value; }
        }

        private string dnsPath;
        public string DnsPath
        {
            get { return dnsPath; }
            set { dnsPath = value; }
        }

        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        private string isCover;
        public string IsCover
        {
            get { return isCover; }
            set { isCover = value; }
        }

        private string hotelID;
        public string HotelID
        {
            get { return hotelID; }
            set { hotelID = value; }
        }

        private string extend;
        public string Extend
        {
            get { return extend; }
            set { extend = value; }
        }

        private string isProcess;
        public string IsProcess
        {
            get { return isProcess; }
            set { isProcess = value; }
        }

        private string resolution;
        public string Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        private string roomTypeCode;
        public string RoomTypeCode
        {
            get { return roomTypeCode; }
            set { roomTypeCode = value; }
        }


        private DateTime createTime;
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }


        private DateTime updateTime;
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        private int seq;
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        private string htpPathBak;
        public string HtpPathBak
        {
            get { return htpPathBak; }
            set { htpPathBak = value; }
        }
    }
}