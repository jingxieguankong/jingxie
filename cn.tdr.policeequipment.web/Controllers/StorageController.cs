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
            var stg = new Storage
            {
                IsDel = (short)DeleteStatus.No,
                Lat = lat,
                Lon = lon,
                Name = name,
                OrgId = orgId
            };
            var data = false;
            var module = new StorageModule(CurrentUser);
            if (string.IsNullOrWhiteSpace(id))
            {
                data = module.Add(stg);
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                stg.Id = id;
                data = module.Modify(stg, t => t.Id == id);
            }
            return Json(new { code = 0, msg = "Ok", data = data }, "text/json");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var data = false;
            var module = new StorageModule(CurrentUser);
            data = module.Remove(t => t.Id == id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}