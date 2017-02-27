namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using module;

    public class MenuController : AuthController
    {
        // GET: Menu
        public ActionResult Index(int w, int h)
        {
            ViewBag.FormAction = "/Menu/FormSubmit";
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableMenuHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData()
        {
            var m = new MenuModule(CurrentUser);
            var items = m.Query();
            var json = TableMenuDataModel.Model.GetJson(items, TableMenuHeaderModel.Header);
            return json;
        }

        [HttpPost]
        public JsonResult FormSubmit(string title, string src, short order, string parentId, string remark)
        {
            return Json(new { code = 0, msg = "Ok", data = true });
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            return Json(new { code = 0, msg = "Ok", data = true });
        }
    }
}