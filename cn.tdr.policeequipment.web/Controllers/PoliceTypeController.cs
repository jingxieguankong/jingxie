namespace cn.tdr.policeequipment.web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using data.entity;
    using enumerates;
    using module;
    using module.models;

    public class PoliceTypeController : AuthController
    {
        // GET: PoliceType
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TablePoliceTypeHeaderModel.Header);
        }

        public JObject GetData(string orgId)
        {
            var module = new PoliceTypeModule(CurrentUser);
            var items = module.FeatchAll(orgId);
            var json = TablePoliceTypeDataModel.Model.GetJson(items, TablePoliceTypeHeaderModel.Header);
            return json;
        }

        [HttpGet]
        public JsonResult Tree(string orgId)
        {
            var module = new PoliceTypeModule(CurrentUser);
            var items = module.FeatchMany(orgId);
            var data = items.Select(t => new ComboTreeModel
            {
                children = new ComboTreeModel[0],
                id = t.Id,
                text = t.Name
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FormSubmit(string id, string orgId, string name)
        {
            var module = new PoliceTypeModule(CurrentUser);
            var ptp = new PoliceType { Name = name, OrgId = orgId };
            var data = false;
            if (string.IsNullOrWhiteSpace(id))
            {
                data = module.Add(ptp);
            }
            else
            {
                ptp.Id = id;
                data = module.Modify(ptp, t => t.Id == id);
            }
            return Json(new { code = 0, msg = "Ok", data = data }, "text/json");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var module = new PoliceTypeModule(CurrentUser);
            var data = module.Remove(t => t.Id == id);

            return Json(new { code = 0, msg = "Ok", data = data });
        }

        [HttpPost]
        public JsonResult AddCateItem(string ptpId, string cateId, short num, short isPk, short isRq)
        {
            var module = new PoliceTypeModule(CurrentUser);
            var item = new StandardEquipment
            {
                CateId = cateId,
                IsDel = (short)DeleteStatus.No,
                IsPrimary = isPk,
                IsRequire = isRq,
                Num = num,
                PtId = ptpId
            };
            var data = module.Add(item);
            return Json(new { code = 0, msg = "Ok", data = data });
        }

        [HttpPost]
        public JsonResult RemoveCateItem(string cateId, string ptpId, short isPk, short isRq)
        {
            var module = new PoliceTypeModule(CurrentUser);
            var data = module.Remove(cateId, ptpId, isPk, isRq);
            return Json(new { code = 0, msg = "Ok", data = true });
        }
    }
}