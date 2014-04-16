using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SELTWAP.Filters;

namespace SELTWAP.Controllers
{
    [VaildateLoginRole]
    public class CollectController : Controller
    {
        //
        // GET: /Collect/

        public ActionResult Index()
        {
            ViewBag.MenuID = "#menu_settle";
            ViewBag.MItemID = "#item_collect";
            ViewBag.SiteMapp = "Home <i class='icon-chevron-right'></i> 清结算管理 <i class='icon-chevron-right'></i> 收款记录";
            return View("Collect");
        }

        //
        // GET: /Collect/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Collect/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Collect/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Collect/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Collect/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Collect/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Collect/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
