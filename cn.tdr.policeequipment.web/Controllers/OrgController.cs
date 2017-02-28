namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;
    using Models;
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

        public JsonResult GetData()
        {
            return Json(new { });
        }

        public JsonResult Tree()
        {
            return Json(new { });
        }

        public JsonResult FormSubmit(string id, string name, string code, string parentId)
        {
            return Json(new { code = 0, msg = "Ok", data = true }, "text/json", System.Text.Encoding.UTF8);
        }

        public JsonResult Remove(string id)
        {
            return Json(new { code = 0, msg = "Ok", data = true });
        }
    }
}