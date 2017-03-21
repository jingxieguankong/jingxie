namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using module;
    using data.entity;

    public class EqtTypeController : AuthController
    {
        // GET: EqtType
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableEqtTypeHeaderModel.Header);
        }

        public JObject GetData()
        {
            var module = new EqtTypeModule(CurrentUser);
            var items = module.FeatchAll().ToArray();
            var json = TableEqtTypeDataModel.Model.GetJson(items, TableEqtTypeHeaderModel.Header);
            return json;
        }

        [HttpGet]
        public JsonResult Tree()
        {
            var module = new EqtTypeModule(CurrentUser);
            var items = module.FeatchAll().ToArray();
            var data = items.Where(t => string.IsNullOrWhiteSpace(t.Pid) || t.Pid == "0")
                .Select(t => GetTree(t, items))
                .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private ComboTreeModel GetTree(EqtCategory parent, IEnumerable<EqtCategory> items)
        {
            var tree = new ComboTreeModel
            {
                id = parent.Id,
                text = parent.Name,
                children = items.Where(t => t.Pid == parent.Id).Select(t => GetTree(t, items)).ToArray()
            };
            return tree;
        }

        [HttpGet]
        public JsonResult TreeOnPtp(string ptpId)
        {
            var module = new EqtTypeModule(CurrentUser);
            var items = module.FeatchAll(ptpId).ToArray();
            var data = items.Distinct().Select(t => GetTree(t, items)).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FormSubmit(string id, string name, string code, short hits, string parentId)
        {
            var module = new EqtTypeModule(CurrentUser);
            var cate = new EqtCategory
            {
                Code = code,
                HitCount = hits,
                Name = name,
                Pid = parentId
            };
            var data = false;
            if (string.IsNullOrWhiteSpace(id))
            {
                data = module.Add(cate);
            }
            else
            {
                cate.Id = id;
                data = module.Modify(cate, t => t.Id == id);
            }
            return Json(new { code = 0, msg = "Ok", data = data }, "text/html");
        }

        public JsonResult Remove(string id)
        {
            var module = new EqtTypeModule(CurrentUser);
            var data = module.Remove(t => t.Id == id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}