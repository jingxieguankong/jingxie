namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using module;

    public class TrackController : AuthController
    {
        // GET: Track
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableOfficerTrackHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData(string pattern, int page, int rows)
        {
            var count = 0;
            var module = new OfficerTrackModule(CurrentUser);
            var items = module.Page(pattern, page, rows, out count);
            var data = TableOfficerTrackDataModel.Model.GetJson(items, count, TableOfficerTrackHeaderModel.Header);
            return data;
        }
    }
}