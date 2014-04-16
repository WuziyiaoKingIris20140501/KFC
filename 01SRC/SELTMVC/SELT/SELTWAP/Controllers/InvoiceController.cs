using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SELTWAP.Filters;

namespace SELTWAP.Controllers
{
    [VaildateLoginRole]
    public class InvoiceController : Controller
    {
        //
        // GET: /Invoice/

        public ActionResult Index()
        {
            ViewBag.MenuID = "#menu_settle";
            ViewBag.MItemID = "#item_invoice";
            ViewBag.SiteMapp = "Home <i class='icon-chevron-right'></i> 清结算管理 <i class='icon-chevron-right'></i> 发票管理";
            return View("Invoice");
        }

        //
        // GET: /Invoice/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Invoice/Create

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
        // GET: /Invoice/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Invoice/Edit/5

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
        // GET: /Invoice/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Invoice/Delete/5

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
