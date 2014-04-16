using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections;

using SELTWAP.Filters;

using HotelVp.SELT.Domain.Biz;
using HotelVp.SELT.Domain.Entity;
using System.IO;
using System.Text;

namespace SELTWAP.Controllers
{
    [VaildateLoginRole]
    public class SettlementController : Controller
    {
        //
        // GET: /Settlement/
        public ActionResult Index()
        {
            ViewBag.MenuID = "#menu_settle";
            ViewBag.MItemID = "#item_settle";
            ViewBag.SiteMapp = "Home <i class='icon-chevron-right'></i> 清结算管理 <i class='icon-chevron-right'></i> 账单管理";
            return View("Settlement");
        }

        public ActionResult AutoGetUnitList(string query)
        {
            LiquidateEntity _liquidateEntity = new LiquidateEntity();
            _liquidateEntity.LiquidateDBEntity = new List<LiquidateDBEntity>();
            LiquidateDBEntity liquidateDBEntity = new LiquidateDBEntity();
            liquidateDBEntity.AutoQuery = query;

            _liquidateEntity.LiquidateDBEntity.Add(liquidateDBEntity);
            DataSet dsResult = LiquidateInfoBP.CommonSelect(_liquidateEntity).QueryResult;

            query = query.Replace(" ", "");
            if (query.Length > 1)
            {
                int op = query.LastIndexOf(",");
                query = query.Substring(op + 1);
            }

            ArrayList alCity = new ArrayList();

            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++ )
            {
                alCity.Add(dsResult.Tables[0].Rows[i][0].ToString().Trim());
            }
            return Json(alCity, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string AjxGetSettleMainList(string month, string unitID, string city, string unitcharge, string orderid, string billid, string saveuser, string slstatus)
        {
            ViewBag.QRMonth = month;
            ViewBag.QRUnitID = unitID;
            ViewBag.QRCity = city;
            ViewBag.QRUnitcharge = unitcharge;
            ViewBag.QROrderid = orderid;
            ViewBag.QRBillid = billid;
            ViewBag.QRSaveuser = saveuser;
            ViewBag.QRSlstatus = slstatus;
            return ActionAjxSettleMainList(1);
        }

        [HttpPost]
        public string ActionAjxSettleMainList(int pagecurrent)
        {
            LiquidateEntity _liquidateEntity = new LiquidateEntity();
            _liquidateEntity.LiquidateDBEntity = new List<LiquidateDBEntity>();
            LiquidateDBEntity liquidateDBEntity = new LiquidateDBEntity();

            liquidateDBEntity.UnitID = ViewBag.QRUnitID;
            liquidateDBEntity.CityID = ViewBag.QRCity;
            liquidateDBEntity.UnitCharge = ViewBag.QRUnitcharge;
            liquidateDBEntity.SaveUser = ViewBag.QRSaveuser;
            liquidateDBEntity.OrderID = ViewBag.QROrderid;
            liquidateDBEntity.SlMonth = ViewBag.QRMonth;
            liquidateDBEntity.SlStatus = ViewBag.QRSlstatus;
            liquidateDBEntity.BillID = ViewBag.QRBillid;

            //liquidateDBEntity.UnitName = "";
            //liquidateDBEntity.HotelID = "";
            //liquidateDBEntity.HotelGroup = "";

            _liquidateEntity.LiquidateDBEntity.Add(liquidateDBEntity);
            _liquidateEntity.PageCurrent = pagecurrent - 1;
            //_liquidateEntity.PageSize = ViewBag.PageSize;

            _liquidateEntity = LiquidateInfoBP.GetLiquidateList(_liquidateEntity);
            DataSet dsResult = _liquidateEntity.QueryResult;

            using (var sw = new StringWriter())
            {
                ViewBag.Pagers = PagerControl.Pagers(pagecurrent, "fn_settle_main_research({0})", _liquidateEntity.TotalCount, _liquidateEntity.PageSize, _liquidateEntity.PageSplit);
                ViewData.Model = dsResult;
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_settleMainListInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
