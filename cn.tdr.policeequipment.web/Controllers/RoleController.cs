namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using data.entity;
    using enumerates;
    using module;
    using module.models;

    public class RoleController : AuthController
    {
        // GET: Role
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableRoleHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData()
        {
            var module = new RoleModule(CurrentUser);
            var list = new List<RoleMenuFeatureModel>();
            var items = module.FeatchAll().ToList();
            items.ForEach(
                t =>
                {
                    list.Add(new RoleMenuFeatureModel
                    {
                        role = t.role,
                        features = new FeatureModel[0],
                        menu = null
                    });
                    list.AddRange(t.menus.Where(x => !string.IsNullOrWhiteSpace(x.menu.Title)));
                });
            var data = TableRoleDataModel.Model.GetJson(list, TableRoleHeaderModel.Header);
            return data;
        }

        [HttpPost]
        public JsonResult RoleFormSubmit(string orgId, string name, string remark)
        {
            var module = new RoleModule(CurrentUser);
            var role = new Role
            {
                IsDel = (short)DeleteStatus.No,
                Name = name,
                OrgId = orgId,
                Remarks = remark,
                Status = (short)AccountStatus.Normal
            };
            var data = module.Add(role);
            return Json(new { code = 0, msg = "Ok", data = data }, "text/html");
        }

        [HttpPost]
        public JsonResult RemoveRole(string id)
        {
            var module = new RoleModule(CurrentUser);
            var data = module.Remove(t => t.Id == id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }

        [HttpPost]
        public JsonResult RemoveFeature(string roleId, string menuId)
        {
            var module = new RoleModule(CurrentUser);
            var data = module.Remove(roleId, menuId);
            return Json(new { code = 0, msg = "Ok", data = data });
        }

        [HttpPost]
        public JsonResult RemoveFeatureItem(string id)
        {
            var module = new RoleModule(CurrentUser);
            var data = module.Remove(id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }

        [HttpPost]
        public JsonResult AddFeatureItem(string roleId, string menuId, string featureId)
        {
            var module = new RoleModule(CurrentUser);
            var ftype = FeatureTypeHelper.Items.First(t => t.Name == featureId);
            var feature = new Feature
            {
                ActId = featureId,
                ActRemark = ftype.Summary,
                IsDel = (short)DeleteStatus.No,
                MenuId = menuId,
                RoleId = roleId,
                Status = 0
            };
            var data = module.Add(feature);
            return Json(new { code = 0, msg = "Ok", data = data }, "text/html");
        }

        [HttpGet]
        public JsonResult MenuTree()
        {
            var module = new MenuModule(CurrentUser);
            var items = module.MyMenus().ToArray();
            var data = items.Where(t => string.IsNullOrWhiteSpace(t.Pid) || t.Pid == "0").Select(
                t => new ComboTreeModel
                {
                    id = t.Id,
                    text = t.Title,
                    children = items.Where(x => x.Pid == t.Id).Select(x => new ComboTreeModel
                    {
                        id = x.Id,
                        text = x.Title,
                        children = new ComboTreeModel[0]
                    }).ToArray()
                }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FeatureTree(string menuId)
        {
            var module = new FeatureModule(CurrentUser);
            var items = module.MyOrgFeatures().ToArray();
            var data = items.Select(t => new ComboTreeModel
            {
                id = t.ActId,
                text = t.ActRemark,
                children = new ComboTreeModel[0]
            }).Distinct().ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Tree(string orgId)
        {
            var module = new RoleModule(CurrentUser);
            var items = module.FeatchAll(orgId);
            var data = items.Select(t => new ComboTreeModel { children = new ComboTreeModel[0], id = t.Id, text = t.Name }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}