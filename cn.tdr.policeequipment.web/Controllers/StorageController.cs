namespace cn.tdr.policeequipment.web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using common;
    using data.entity;
    using enumerates;
    using module;

    public class StorageController : AuthController
    {
        // GET: Storage
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableStorageHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData(string orgId, string name, int page, int rows)
        {
            var module = new StorageModule(CurrentUser);
            var count = 0;
            var items = module.Page(orgId, name, page, rows, out count);
            var json = TableStorageDataModel.Model.GetJson(items, count, TableStorageHeaderModel.Header);
            return json;
        }

        [HttpPost]
        public JsonResult FormSubmit(string id, string orgId, string name, double lon, double lat)
        {
            var data = false;
            return Json(new { code = 0, msg = "Ok", data = data }, "text/json");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var data = false;
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}