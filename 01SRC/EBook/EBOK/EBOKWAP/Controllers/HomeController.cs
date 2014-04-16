using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Drawing;
using System.IO;
using System.Net;
using System.Data;
using System.Web.Security;

using HotelVp.EBOK.Domain.Biz;

using EBOK.Filters;

namespace EBOK.Controllers
{
    [VaildateLoginRole]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.MenuID = "#menu_dashbord";
            ViewBag.MItemID = "";
            ViewBag.SiteMapp = "Home <i class='icon-chevron-right'></i> DashBord";
            //LiquidateEntity liquidateEntity = new LiquidateEntity();
            //liquidateEntity.LiquidateDBEntity = new List<LiquidateDBEntity>();
            //liquidateEntity.LiquidateDBEntity.Add(new LiquidateDBEntity());
            //LiquidateInfoBP.GetLiquidateList(liquidateEntity);
            return View();//View("Contact"); 
        }
    }
}
