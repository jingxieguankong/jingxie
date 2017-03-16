namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;

    public class SiteController : AuthController
    {
        // GET: Site
        public ActionResult Index()
        {
            return View();
        }
    }
}