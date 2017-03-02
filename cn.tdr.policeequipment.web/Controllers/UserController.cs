namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using enumerates;
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
            var items = new AccountModel[0];
            var json = TableUserDataModel.Model.GetJson(items, 0, TableUserHeaderModel.Header);
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
        public JsonResult FormSubmit()
        {
            return Json(new { code = 0, msg = "Ok", data = false });
        }
    }
}