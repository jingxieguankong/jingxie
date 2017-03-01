namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;

    public class UserController : AuthController
    {
        // GET: User
        public ActionResult Index(int w, int h)
        {
            return View();
        }
    }
}