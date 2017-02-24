namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;

    public class MenuController : AuthController
    {
        // GET: Menu
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View();
        }

        public JsonResult GetData()
        {
            return Json(new { });
        }
    }
}