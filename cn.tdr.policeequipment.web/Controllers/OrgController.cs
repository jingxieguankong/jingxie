namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using enumerates;
    using module;

    public class OrgController : AuthController
    {
        // GET: Org
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableOrgHeaderModel.Header);
        }

        public JObject GetData()
        {
            var m = new OrgModule(CurrentUser);
            var items = m.FetchAll().ToArray();
            if (!CurrentUser.IsSupperAdministrator)
            {
                var root = items.First(t => t.Id == CurrentUser.Organization.Id);
                root.Pid = null;
                var childs = items.Where(t => t.Pid == CurrentUser.Organization.Id);
                var list = new List<data.entity.Organization>();
                list.AddRange(items);
                list.RemoveAll(t => t.Id == CurrentUser.Organization.Id);
                list.Add(root);
                items = list.ToArray();
                list.Clear();
                list = null;
            }
            var json = TableOrgDataModel.Model.GetJson(items, TableOrgHeaderModel.Header);
            return json;
        }

        [HttpGet]
        public JsonResult Tree()
        {
            var m = new OrgModule(CurrentUser);
            var items = m.FetchAll().ToArray();
            var data = 
                items
                .Where(t => string.IsNullOrWhiteSpace(t.Pid) || t.Pid == "0" || t.Id == CurrentUser.Organization.Id)
                .Select(t => GetTree(t, items))
                .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private ComboTreeModel GetTree(data.entity.Organization parent, IEnumerable<data.entity.Organization> orgs)
        {
            var tree = new ComboTreeModel
            {
                id = parent.Id,
                text = parent.Name,
                children = orgs.Where(t => t.Pid == parent.Id).Select(t => GetTree(t, orgs)).ToArray()
            };
            return tree;
        }

        public JsonResult FormSubmit(string id, string name, string code, string parentId)
        {
            var flag = false;
            var mod = new OrgModule(CurrentUser);
            var m = new data.entity.Organization
            {
                Code = code,
                IsDel = (short)DeleteStatus.No,
                Layer = 0,
                Name = name,
                Pid = parentId
            };

            if (string.IsNullOrWhiteSpace(id))
            {
                flag = mod.Add(m);
            }
            else
            {
                m.Id = id;
                flag = mod.Modify(m, t => t.Id == id);
            }
            return Json(new { code = 0, msg = "Ok", data = flag }, "text/json", System.Text.Encoding.UTF8);
        }

        public JsonResult Remove(string id)
        {
            var m = new OrgModule(CurrentUser);
            var flag = m.Remove(t => t.Id == id);
            return Json(new { code = 0, msg = "Ok", data = flag });
        }
    }
}