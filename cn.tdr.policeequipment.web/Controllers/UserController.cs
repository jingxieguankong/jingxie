namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using data.entity;
    using enumerates;
    using module;
    using module.models;

    public class UserController : AuthController
    {
        // GET: User
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableUserHeaderModel.Header);
        }

        // Get: /User/GetData?{orgId=&roleId=&page=1&rows=20}
        public JObject GetData(string queryOrgId, string queryRoleId, int page, int rows)
        {
            var module = new UserModule(CurrentUser);
            var count = 0;
            var items = module.Page(queryOrgId, queryRoleId, page, rows, out count);
            var json = TableUserDataModel.Model.GetJson(items, count, TableUserHeaderModel.Header);
            return json;
        }

        [HttpGet]
        public JsonResult CategoryTree()
        {
            var data = AccountCategoryHelper.Items.Where(t => t.Category != AccountCategory.None).Select(
                t => new ComboTreeModel
                {
                    id = $"{t.Value}",
                    text = t.Summary,
                    children = new ComboTreeModel[0]
                }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FormSubmit(string id, string roleId, string name, short category)
        {
            var data = false;
            var module = new UserModule(CurrentUser);
            var user = new User
            {
                Account = name,
                Category = category,
                RoleId = roleId
            };
            if (string.IsNullOrWhiteSpace(id))
            {
                data = module.Add(user);
            }
            else
            {
                user.Id = id;
                data = module.Modify(user, t => t.Id == id);
            }

            return Json(new { code = 0, msg = "Ok", data = data }, "text/html");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var module = new UserModule(CurrentUser);
            var data = module.Remove(id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}