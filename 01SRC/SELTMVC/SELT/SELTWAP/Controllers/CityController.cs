using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


using HotelVp.SELT.Domain.Biz;
using HotelVp.SELT.Domain.Entity;
using System.IO;
using System.Collections;

using SELTWAP.Filters;

namespace SELTWAP.Controllers
{
    [VaildateLoginRole]
    public class CityController : Controller
    {
        //
        // GET: /City/

        public ActionResult Index()
        {
            ViewBag.Message = "Your City page.";
            //_cityEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            //_cityEntity.LogMessages.Userid = UserSession.Current.UserAccount;
            //_cityEntity.LogMessages.Username = UserSession.Current.UserDspName;
            //_cityEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
            CityEntity _cityEntity = new CityEntity();
            _cityEntity.CityDBEntity = new List<CityDBEntity>();
            CityDBEntity cityDBEntity = new CityDBEntity();

            //cityDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
            //cityDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
            //cityDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
            //cityDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

            _cityEntity.CityDBEntity.Add(cityDBEntity);
            ViewBag.CityList = CityInfoBP.Select(_cityEntity).QueryResult;


            return View();
        }

        //
        // GET: /City/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /City/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /City/Create

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
        // GET: /City/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /City/Edit/5

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
        // GET: /City/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /City/Delete/5

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



        [HttpPost]
        public string GetCityList(string citynm, string startdt, string enddt, string online)
        {
            //ViewBag
            //System.Threading.Thread.Sleep(5000);
            CityEntity _cityEntity = new CityEntity();
            _cityEntity.CityDBEntity = new List<CityDBEntity>();
            CityDBEntity cityDBEntity = new CityDBEntity();

            //cityDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
            //cityDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
            //cityDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
            //cityDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

            _cityEntity.CityDBEntity.Add(cityDBEntity);
            System.Data.DataSet dsResult = CityInfoBP.Select(_cityEntity).QueryResult;
            var list = JsonConvert.SerializeObject(dsResult.Tables[0], new DataTableConverter());
            return list;
        }

        public ActionResult AutoGetCityList(string query)
        {
            CityEntity _cityEntity = new CityEntity();
            _cityEntity.CityDBEntity = new List<CityDBEntity>();
            CityDBEntity cityDBEntity = new CityDBEntity();
            cityDBEntity.Name_CN = query;
            //cityDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
            //cityDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
            //cityDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
            //cityDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

            _cityEntity.CityDBEntity.Add(cityDBEntity);
            DataSet dsResult = CityInfoBP.CommonSelect(_cityEntity).QueryResult;

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

            //var list = JsonConvert.SerializeObject(dsResult.Tables[0], new DataTableConverter());
            //var users = (from u in dsResult.Tables[0].Rows
            //             where u.EmailAddress.Contains(query)
            //             orderby u.EmailAddress // optional
            //             select u.EmailAddress).Distinct().ToArray();

            return Json(alCity, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TagSearch(string term)
        {
            // Get Tags from database
            string[] tags = { "ASP.NET", "WebForms", "MVC", "jQuery", "ActionResult", "MangoDB", "Java", "Windows" };
            return this.Json(tags.Where(t => t.StartsWith(term)), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public string AjxGetCityList(string citynm, string startdt, string enddt, string online)
        {
            //ViewBag
            CityEntity _cityEntity = new CityEntity();
            _cityEntity.CityDBEntity = new List<CityDBEntity>();
            CityDBEntity cityDBEntity = new CityDBEntity();

            //cityDBEntity.Name_CN = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Name_CN"].ToString())) ? null : ViewState["Name_CN"].ToString();
            //cityDBEntity.OnlineStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OnlineStatus"].ToString())) ? null : ViewState["OnlineStatus"].ToString();
            //cityDBEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
            //cityDBEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();

            _cityEntity.CityDBEntity.Add(cityDBEntity);
            System.Data.DataSet dsResult = CityInfoBP.Select(_cityEntity).QueryResult;

            using(var sw = new StringWriter())
            {
                ViewData.Model = dsResult;
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "../Partial/_cityListInfo");
                var viewContent = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContent, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult Seq()
        {
            ViewBag.Message = "Your City page.";

            return View();
        }
    }
}
