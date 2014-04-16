using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


using HotelVp.EBOK.Domain.Biz;
using System.IO;
using System.Collections;

using EBOK.Filters;
using HotelVp.EBOK.Domain.API.Model;
using HotelVp.EBOK.Domain.API;

namespace EBOK.Controllers
{
    [VaildateLoginRole]
    public class PDReportController : Controller
    {
        //
        // GET: /City/

        public ActionResult Index()
        {
            ViewBag.Message = "Your OrderCm page.";
            ViewBag.HotelNM = (Request.QueryString["hnm"] != null) ? HttpUtility.UrlDecode(Request.QueryString["hnm"].ToString(), Encoding.GetEncoding("utf-8")) : "";

            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");

            ViewBag.FromDayDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.ToDayDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

            DateTime dt = DateTime.Now;
            ViewBag.STDate = dt.AddDays(1 - dt.Day).ToString("yyyy-MM-dd");

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
        public string AjxGetOrderQueryList(string mac, string hid, string sdate)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            if ("Yesdate".Equals(sdate))
            {
                beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                endDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            else if ("SysMonth".Equals(sdate))
            {
                beginDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            }
            else if (!"Sysdate".Equals(sdate) && !"SysMonth".Equals(sdate) && !"Yesdate".Equals(sdate) && !"_".Equals(sdate))
            {
                beginDate = (sdate.Split('_').Count() > 0) ? sdate.Split('_')[0] : "";
                endDate = (sdate.Split('_').Count() > 0) ? sdate.Split('_')[1] : "";
            }

            using (var sw = new StringWriter())
            {
                ViewData.Model = PDReportBP.AjxGetPDReportQueryList(mac, hid, beginDate, endDate);
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_pbReportListInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public string AjxGetOrderDTPicker(string prenext, string sdate)
        {
            ViewBag.STDate = DateTime.Parse(sdate).AddMonths(int.Parse(prenext)).ToString("yyyy-MM-dd");
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_orderCmDTPicker");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
