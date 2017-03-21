namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;

    public class MyController : AuthController
    {
        // GET: My
        public ActionResult Index(int w, int h)
        {
            return View();
        }

        public ActionResult Passwd(int w, int h, string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }

        public ActionResult Inf(int w, int h)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Modify(string oldPwd, string newPwd)
        {
            var msg = "Ok";
            var data = false;
            return Json(new { code = 0, msg = msg, data = data }, "text/html");
        }
    }
}