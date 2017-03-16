namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using data.entity;
    using enumerates;
    using module;

    public class SiteController : AuthController
    {
        // GET: Site
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableSiteHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData(string orgId, string siteId, int page, int rows)
        {
            var count = 0;
            var module = new StationModule(CurrentUser);
            var items = module.Page(orgId, siteId, page, rows, out count);
            var data = TableSiteDataModel.Model.GetJson(items, count, TableSiteHeaderModel.Header);
            return data;
        }

        [HttpPost]
        public JsonResult FormSubmit(string id, string orgId, string siteId, double lat, double lon)
        {
            var station = new Station
            {
                Category = (short)StationCategory.Normal,
                IsDel = (short)DeleteStatus.No,
                Lat = lat,
                Lon = lon,
                OrgId = orgId,
                SiteId = siteId
            };
            var data = false;
            var isAdd = string.IsNullOrWhiteSpace(id);
            var module = new StationModule(CurrentUser);
            if (isAdd)
            {
                data = module.Add(station);
            }

            if (!isAdd)
            {
                station.Id = id;
                data = module.Modify(station, t => t.Id == id);
            }

            return Json(new { code = 0, msg = "Ok", data = data }, "text/html");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var module = new StationModule(CurrentUser);
            var data = module.Remove(id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}