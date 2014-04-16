using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SELTWAP.Filters;

namespace SELTWAP.Controllers
{
    [VaildateLoginRole]
    public class WriteOffController : Controller
    {
        //
        // GET: /WriteOff/

        public ActionResult Index()
        {
            ViewBag.MenuID = "#menu_settle";
            ViewBag.MItemID = "#item_writeoff";
            ViewBag.SiteMapp = "Home <i class='icon-chevron-right'></i> 清结算管理 <i class='icon-chevron-right'></i> 回款销账";
            return View("WriteOff");
        }

        //
        // GET: /WriteOff/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WriteOff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WriteOff/Create

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
        // GET: /WriteOff/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WriteOff/Edit/5

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
        // GET: /WriteOff/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WriteOff/Delete/5

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
