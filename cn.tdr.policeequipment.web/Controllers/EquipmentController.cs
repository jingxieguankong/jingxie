namespace cn.tdr.policeequipment.web.Controllers
{
    using System;
    using System.Linq;
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
        public JsonResult Tree(string orgId, string cateId, string stgId)
        {
            var module = new EquipmentModule(CurrentUser);
            var items = module.FeatchAll(orgId, cateId, stgId).Select(t => new ComboTreeModel
            {
                children = new ComboTreeModel[0],
                id = t.Id,
                text = t.Model
            });
            return Json(items, JsonRequestBehavior.AllowGet);
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
        public JsonResult FormSubmit(string id, string eqtId, string orgId, string stgId, string tagId,
            string mod, string fac, string facCode, DateTime facDate, int validInterval, DateTime purchaseDate)
        {
            var data = false;
            var expiredDate = facDate.AddDays(validInterval);
            var eqt = new Equipment
            {
                CateId = eqtId,
                ChangeTime = 0,
                Dispatched = (short)DispatchedStatus.None,
                ExpiredTime = expiredDate.ToUnixTime(),
                FactorCode = facCode,
                FactorTime = facDate.ToUnixTime(),
                Factory = fac,
                InputTime = DateTime.Now.ToUnixTime(),
                IsChanged = 0,
                IsDel = (short)DeleteStatus.No,
                IsLost = 0,
                LibId = stgId,
                Model = mod,
                OrgId = orgId,
                PurchaseTime = purchaseDate.ToUnixTime(),
                Status = (short)EquipmentStatus.Normal,
                TagId = tagId
            };
            var module = new EquipmentModule(CurrentUser);
            var isAdd = string.IsNullOrWhiteSpace(id);
            if (isAdd)
            {
                eqt.Power = 100;
                data = module.Add(eqt);
            }

            if (!isAdd)
            {
                eqt.Id = id;
                data = module.Modify(eqt, t => t.Id == id);
            }

            return Json(new { code = 0, msg = "Ok", data = data }, "text/json");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var module = new EquipmentModule(CurrentUser);
            var data = module.Remove(id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}