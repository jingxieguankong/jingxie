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

    public class CabinetController : AuthController
    {
        // GET: Cabinet
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableCabinetHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData(string orgId, string name, int page, int rows)
        {
            var count = 0;
            var module = new CabinetModule(CurrentUser);
            var items = module.Page(orgId, name, page, rows, out count);
            var json = TableCabinetDataModel.Model.GetJson(items, count, TableCabinetHeaderModel.Header);
            return json;
        }

        [HttpPost]
        public JsonResult FormSubmit(string id, string orgId, string name, double lon, double lat)
        {
            var cabinet = new Cabinet
            {
                IsDel = (short)DeleteStatus.No,
                Lat = lat,
                Lon = lon,
                Name = name,
                OrgId = orgId
            };
            var data = false;
            var module = new CabinetModule(CurrentUser);
            if (string.IsNullOrWhiteSpace(id))
            {
                data = module.Add(cabinet);
            }

            if (!string.IsNullOrWhiteSpace(id))
            {
                cabinet.Id = id;
                data = module.Modify(cabinet, t => t.Id == id);
            }

            return Json(new { code = 0, msg = "Ok", data = data }, "text/json");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var data = false;
            var module = new CabinetModule(CurrentUser);
            data = module.Remove(t => t.Id == id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}