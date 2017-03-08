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

    public class OfficerController : AuthController
    {
        // GET: Officer
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableOfficerHeaderModel.Header);
        }
        
        public JObject GetData(string orgId, string ptId, string name, string code, int page, int rows)
        {
            var count = 0;
            var module = new OfficerModule(CurrentUser);
            var items = module.Page(orgId, ptId, name, code, page, rows, out count);
            var json = TableOfficerDataModel.Model.GetJson(items, count, TableOfficerHeaderModel.Header);
            return json;
        }

        [HttpPost]
        public JsonResult FormSubmit(string id, string orgId, string ptpId, string name, string code, string tel, short sex)
        {
            var officer = new Officer
            {
                OrgId = orgId,
                PtId = ptpId,
                Name = name,
                IdentyCode = code,
                IsDel = (short)DeleteStatus.No,
                Sex = sex,
                Tel = tel,
                SignupDate = DateTime.Now.ToUnixTime()
            };
            var module = new OfficerModule(CurrentUser);
            var data = false;
            var emptyId = string.IsNullOrWhiteSpace(id);
            if (emptyId)
            {
                data = module.Add(officer);
            }

            if (!emptyId)
            {
                officer.Id = id;
                data = module.Modify(officer, t => t.Id == id);
            }

            return Json(new { code = 0, msg = "Ok", data = data }, "text/json");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var module = new OfficerModule(CurrentUser);
            var data = module.Remove(t => t.Id == id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}