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
using HotelVp.EBOK.Domain.API.Model;

namespace EBOK.Controllers
{
    [VaildateLoginRole]
    public class RoomCmController : Controller
    {
        //
        // GET: /City/

        public ActionResult Index()
        {
            ViewBag.Message = "Your RoomCm page.";
            ViewBag.HotelNM = (Request.QueryString["hnm"] != null) ? HttpUtility.UrlDecode(Request.QueryString["hnm"].ToString(), Encoding.GetEncoding("utf-8")) : "";
            return View();
        }

        public ActionResult List()
        {
            ViewBag.Message = "Your RoomCm page.";
            return View();
        }

        public ActionResult Review()
        {
            ViewBag.Message = "Your RoomCm page.";
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
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            using (var sw = new StringWriter())
            {
                ViewData.Model = RoomCmBP.GetIndexMainList(mac, hid, beginDate, endDate).result;
                ViewBag.TRoomCT = (ViewData.Model as List<HotelVp.EBOK.Domain.API.Model.RoomList>).Count;

                ViewBag.ToDayDate = DateTime.Now.ToString("yyyy-MM-dd");
                ViewData.Add("CnUser", RoomCmBP.GetOpeUserList(mac, hid));
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_roomCmIndexInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public string AjxGetReviewRoomMainList(string mac, string hid)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            using (var sw = new StringWriter())
            {
                ViewData.Model = RoomCmBP.GetRoomMainList(mac, hid, beginDate, endDate).result;
                ViewBag.TRoomCT = (ViewData.Model as List<HotelVp.EBOK.Domain.API.Model.RoomList>).Count;
                ViewData.Add("CnUser", RoomCmBP.GetOpeUserList(mac, hid));

                ViewBag.FromDayDate = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.ToDayDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_roomCmReviewInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public JsonResult AjxSaveRoomInfo(string saveDate, string saveTime, string roomcd, string roomnum, string roombref, string roomWifi, string opeuserid, string mac, string hid)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (!"Sysdate".Equals(saveDate))
            {
                beginDate = (saveDate.Split('_').Count() > 0) ? saveDate.Split('_')[0] : "";
                endDate = (saveDate.Split('_').Count() > 0) ? saveDate.Split('_')[1] : "";
            }
            //"btn_roomcm_review_roomcd_价格代码_房型CODE"
            //roomcd = roomcd.Substring(25);//价格代码_房型CODE
            //string rateCode = (roomcd.Split('_').Count() > 0) ? roomcd.Split('_')[0] : "";
            //string roomTypeCode = (roomcd.Split('_').Count() > 0) ? roomcd.Split('_')[1] : "";

            var r = RoomCmBP.SaveRoomInfo(beginDate, endDate, "LMBAR2", roomcd, roomnum, roombref, roomWifi, opeuserid, mac, hid);
            return Json(r);
        }

        [HttpPost]
        public string AjxGetListRoomMainList(string mac, string hid)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            using (var sw = new StringWriter())
            {
                //ViewData.Model = RoomCmBP.GetRoomMainList(mac, hid, beginDate, endDate).result;

                ViewBag.FromDayDate = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.ToDayDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_roomCmListInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public string AjxGetRoomPlanListMainList(string mac, string hid, string sdate)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (!"Sysdate".Equals(sdate))
            {
                beginDate = (sdate.Split('_').Count() > 0) ? sdate.Split('_')[0] : "";
                endDate = (sdate.Split('_').Count() > 0) ? sdate.Split('_')[1] : "";
            }

            using (var sw = new StringWriter())
            {
                RoomPlan roomplan = RoomCmBP.GetRoomPlanListMainList(mac, hid, beginDate, endDate);
                //ViewBag.Roomls = roomplan.Roomls;
                ViewData.Model = roomplan.planResult;
                ViewBag.lm2Cols = roomplan.lm2Cols;
                ViewBag.lmCols = roomplan.lmCols;
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_roomCmPlanListInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public JsonResult AjxSaveRoomStatusInfo(string ratecode, string roomcd, string roomnum, string status, string twoprice, string opeuserid, string mac, string hid)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var r = RoomCmBP.SaveRoomStatusInfo(ratecode, roomcd, roomnum, status, twoprice, opeuserid, mac, hid);
            return Json(r);
        }
    }
}
