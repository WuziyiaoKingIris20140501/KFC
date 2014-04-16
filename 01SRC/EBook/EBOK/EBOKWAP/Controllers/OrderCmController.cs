using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using EBOK.Filters;
using HotelVp.EBOK.Domain.Biz;
using HotelVp.EBOK.Domain.API;
using HotelVp.EBOK.Domain.API.Model;


namespace EBOK.Controllers
{
    [VaildateLoginRole]
    public class OrderCmController : Controller
    {
        #region 页面初始化
        public ActionResult Index()
        {
            ViewBag.Message = "今日待处理订单";
            ViewBag.MAC = (Request.QueryString["mac"] != null) ? Request.QueryString["mac"].ToString() : "";
            ViewBag.HotelID = (Request.QueryString["hid"] != null) ? Request.QueryString["hid"].ToString() : "";
            ViewBag.HotelNM = (Request.QueryString["hnm"] != null) ? HttpUtility.UrlDecode(Request.QueryString["hnm"].ToString(), Encoding.GetEncoding("utf-8")) : "";
            return View();
        }

         [HttpPost]
        public string AjxGetSpanCount(string mac, string hid)
        {
            string beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            // 订单确认
            string OcOffToday = OrderCmBP.GetIndexOrderCount(mac, hid, beginDate, endDate, "0,1,3").ToString();

            beginDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            endDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            // 订单审核
            string AcOffToday = OrderCmBP.GetIndexOrderCount(mac, hid, beginDate, endDate, "4,6,7").ToString();

            return OcOffToday + "," + AcOffToday;
        }

        public ActionResult List()
        {
            ViewBag.Message = "Your OrderCm page.";
            return View();
        }

        public ActionResult Review()
        {
            ViewBag.Message = "今日待审核订单";
            ViewBag.HotelNM = (Request.QueryString["hnm"] != null) ? HttpUtility.UrlDecode(Request.QueryString["hnm"].ToString(), Encoding.GetEncoding("utf-8")) : "";
            return View();
        }

        public ActionResult Seq()
        {
            ViewBag.Message = "Your City page.";

            return View();
        }

        public ActionResult Approve()
        {
            ViewBag.Message = "今日待审核订单";
            ViewBag.HotelNM = (Request.QueryString["hnm"] != null) ? HttpUtility.UrlDecode(Request.QueryString["hnm"].ToString(), Encoding.GetEncoding("utf-8")) : "";
            return View();
        }
        #endregion

        #region 订单确认
        [HttpPost]
        public string AjxGetIndexMainList(string mac, string hid)
        {
            string beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            using (var sw = new StringWriter())
            {
                ViewData.Model = OrderCmBP.GetIndexMainList(mac, hid, beginDate, endDate, "0,1,3").result;
                ViewBag.TOrderCT = (ViewData.Model as List<HotelVp.EBOK.Domain.API.Model.OrderList>).Count;

                string beginSDate = DateTime.Now.ToString("yyyy-MM-dd");
                string endSDate = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.VOrderCT = OrderCmBP.GetIndexHisOrderCount(mac, hid, beginDate, endDate, beginSDate, endSDate, "4,9");

                //ViewBag.VOrderCT = OrderCmBP.GetIndexOrderCount(mac, hid, beginDate, endDate, "4,9");

                ViewData.Add("CnUser", RoomCmBP.GetOpeUserList(mac, hid));

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_orderCmIndexInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public JsonResult AjxConfirmOrderInfo(string orderid, string comfirmid, string actionType, string remark, string opeuserid, string mac, string hid)
        {
            var result = OrderCmBP.SaveConfirmOrderInfo(orderid, comfirmid, remark, actionType, opeuserid, mac, hid);
            return Json(result);
        }

        [HttpPost]
        public string AjxGetOrderReviewList(string mac, string hid)
        {
            string beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            using (var sw = new StringWriter())
            {
                string beginSDate = DateTime.Now.ToString("yyyy-MM-dd");
                string endSDate = DateTime.Now.ToString("yyyy-MM-dd");

                ViewData.Model = OrderCmBP.GetIndexHisMainList(mac, hid, beginDate, endDate, beginSDate, endSDate, "4,9").result;
                ViewBag.VOrderCT = (ViewData.Model as List<HotelVp.EBOK.Domain.API.Model.OrderList>).Count;
                ViewBag.TOrderCT = OrderCmBP.GetIndexOrderCount(mac, hid, beginDate, endDate, "0,1,3");


                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_orderCmReviewInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public string AjxGetOrderQueryInfo(string mac, string hid)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            using (var sw = new StringWriter())
            {
                //ViewData.Model = RoomCmBP.GetRoomMainList(mac, hid, beginDate, endDate).result;

                ViewBag.FromDayDate = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.ToDayDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

                DateTime dt = DateTime.Now;
                ViewBag.STDate = dt.AddDays(1 - dt.Day).ToString("yyyy-MM-dd");

                ViewBag.VOrderCT = OrderCmBP.GetIndexOrderCount(mac, hid, beginDate, endDate, "4,9");
                ViewBag.TOrderCT = OrderCmBP.GetIndexOrderCount(mac, hid, beginDate, endDate, "0,1,3");

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_orderCmQueryInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public string AjxGetOrderQueryList(string mac, string hid, string sdate, string cusid)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (!"Sysdate".Equals(sdate) && !"_".Equals(sdate))
            {
                beginDate = (sdate.Split('_').Count() > 0) ? sdate.Split('_')[0] : "";
                endDate = (sdate.Split('_').Count() > 0) ? sdate.Split('_')[1] : "";
            }

            using (var sw = new StringWriter())
            {
                ViewBag.PageNum = 1;
                ViewBag.PgeSize = 7;
                ViewBag.HasNext = true;
                //OrderCmBP.GetOrderQueryList(mac, hid, beginDate, endDate, cusid).result;

                Response<List<OrderList>> resp = OrderCmBP.AjxGetOrderQueryPageList(mac, hid, beginDate, endDate, cusid, ViewBag.PageNum, ViewBag.PgeSize);
                ViewData.Model = resp.result;
                ViewBag.HasNext = (resp.page.hasNext) ? "1" : "0";
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_orderCmQueryList");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public string AjxGetOrderQueryPageList(string mac, string hid, string sdate, string cusid, int pageNum, int pageSize)
        {
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (!"Sysdate".Equals(sdate) && !"_".Equals(sdate))
            {
                beginDate = (sdate.Split('_').Count() > 0) ? sdate.Split('_')[0] : "";
                endDate = (sdate.Split('_').Count() > 0) ? sdate.Split('_')[1] : "";
            }

            using (var sw = new StringWriter())
            {
                ViewBag.SPageNum = pageNum + 1;
                ViewBag.SPgeSize = pageSize;
                Response<List<OrderList>> resp = OrderCmBP.AjxGetOrderQueryPageList(mac, hid, beginDate, endDate, cusid, pageNum + 1, pageSize);
                ViewData.Model = resp.result;
                ViewBag.SHasNext = (resp.page.hasNext) ? "1" : "0";

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_orderCmQueryPageList");
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
        #endregion

        #region 订单审核
        [HttpPost]
        public string AjxAPPGetIndexMainList(string mac, string hid)
        {
            string beginDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            using (var sw = new StringWriter())
            {
                ViewData.Model = OrderCmBP.GetIndexMainList(mac, hid, beginDate, endDate, "4,6,7").result;
                ViewBag.TOrderCT = (ViewData.Model as List<HotelVp.EBOK.Domain.API.Model.OrderList>).Count;

                string beginSDate = DateTime.Now.ToString("yyyy-MM-dd");
                string endSDate = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.VOrderCT = OrderCmBP.GetIndexHisOrderCount(mac, hid, beginDate, endDate, beginSDate, endSDate, "5,8");
                ViewData.Add("CnUser", RoomCmBP.GetOpeUserList(mac, hid));

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_approveCmIndexInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public JsonResult AjxAPPConfirmOrderInfo(string orderid, string comfirmid, string actionType, string remark, string opeuserid, string mac, string hid)
        {
            var result = OrderCmBP.SaveConfirmOrderInfo(orderid, comfirmid, remark, actionType, opeuserid, mac, hid);
            return Json(result);
        }

        [HttpPost]
        public string AjxAPPGetOrderReviewList(string mac, string hid)
        {
            string beginDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

            string beginSDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endSDate = DateTime.Now.ToString("yyyy-MM-dd");

            using (var sw = new StringWriter())
            {
                ViewData.Model = OrderCmBP.GetIndexHisMainList(mac, hid, beginDate, endDate, beginSDate, endSDate, "5,8").result;
                ViewBag.VOrderCT = (ViewData.Model as List<HotelVp.EBOK.Domain.API.Model.OrderList>).Count;
                ViewBag.TOrderCT = OrderCmBP.GetIndexOrderCount(mac, hid, beginDate, endDate, "4,6,7");

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_approveCmReviewInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public string AjxAPPGetOrderQueryInfo(string mac, string hid)
        {
            string beginDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.ToString("yyyy-MM-dd");

            string beginSDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endSDate = DateTime.Now.ToString("yyyy-MM-dd");

            using (var sw = new StringWriter())
            {
                ViewData.Model = OrderCmBP.GetIndexMainList(mac, hid, beginDate, endDate, "5,9").result;
                ViewBag.VOrderCT = OrderCmBP.GetIndexHisMainList(mac, hid, beginDate, endDate, beginSDate, endSDate, "5,8");
                ViewBag.TOrderCT = OrderCmBP.GetIndexHisMainList(mac, hid, beginDate, endDate, beginSDate, endSDate, "4,6,7");

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_approveCmQueryList");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public string AjxAPPGetOrderQueryList(string mac, string hid, string sdate)
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
                ViewData.Model = OrderCmBP.GetOrderQueryList(mac, hid, beginDate, endDate, "").result;
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_approveCmQueryList");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
    }
}
