using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HotelVp.CMS.Domain.DataAccess.SendFaxByWebTurnPicture;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;

using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace HotelVp.CMS.Domain.Process.SendFaxByWebTurnPicture
{
    public abstract class WebSiteThumbnailBP
    {
        public static Bitmap GetWebSiteThumbnail(string Url, int BrowserWidth, int BrowserHeight, int ThumbnailWidth, int ThumbnailHeight)
        {
            return WebSiteThumbnail.GetWebSiteThumbnail(Url, BrowserWidth, BrowserHeight, ThumbnailWidth, ThumbnailHeight);
        }
    }
}
