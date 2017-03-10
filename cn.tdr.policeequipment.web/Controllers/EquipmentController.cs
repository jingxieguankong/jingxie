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

    public class EquipmentController : AuthController
    {
        // GET: Equipment
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableEquipmentHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData(string orgId, string storageId, string cabinetId,
            string tagCode, string factorCode, short dataType, int page, int rows)
        {
            var count = 0;
            var module = new EquipmentModule(CurrentUser);
            var items = module.Page(orgId, storageId, cabinetId, tagCode, factorCode, dataType,
                page, rows, out count);
            var json = TableEquipmentDataModel.Model.GetJson(items, count, TableEquipmentHeaderModel.Header);
            return json;
        }

        [HttpPost]
        public JsonResult FormSubmit()
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