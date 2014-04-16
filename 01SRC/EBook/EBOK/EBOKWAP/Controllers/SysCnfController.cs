using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using HotelVp.EBOK.Domain.Biz;
using EBOK.Filters;

namespace EBOK.Controllers
{
    [VaildateLoginRole]
    public class SysCnfController : Controller
    {
        //
        // GET: /City/

        public ActionResult Index()
        {
            ViewBag.Message = "Your OrderCm page.";
            ViewBag.HotelNM = (Request.QueryString["hnm"] != null) ? HttpUtility.UrlDecode(Request.QueryString["hnm"].ToString(), Encoding.GetEncoding("utf-8")) : "";
            return View();
        }

        public ActionResult List()
        {
            ViewBag.Message = "Your OrderCm page.";
            return View();
        }

        public ActionResult Review()
        {
            ViewBag.Message = "Your OrderCm page.";
            return View();
        }
      
        public ActionResult Seq()
        {
            ViewBag.Message = "Your City page.";

            return View();
        }

        [HttpPost]
        public string AjxGetIndexMainList(string mac, string hid)
        {
            using (var sw = new StringWriter())
            {
                ViewData.Model = SysCnfBP.GetIndexMainList(mac, hid).result;
                //ViewBag.TRoomCT = (ViewData.Model as List<HotelVp.EBOK.Domain.API.Model.RoomList>).Count;

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_sysCnfIndexInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public JsonResult AjxDeleteUserInfo(string userId, string userName, string opeuserid, string mac, string hid)
        {
            var result = SysCnfBP.DeleteUserInfo(userId, userName, opeuserid, mac, hid);
            return Json(result);
        }

        [HttpPost]
        public JsonResult AjxSaveUserInfo(string userId, string userName, string tel, string status, string remark, string opeuserid, string action, string mac, string hid)
        {
            var result = SysCnfBP.SaveUserInfo(userId, userName, tel, status, remark, opeuserid, action, mac, hid);
            return Json(result);
        }
        
    }
}
