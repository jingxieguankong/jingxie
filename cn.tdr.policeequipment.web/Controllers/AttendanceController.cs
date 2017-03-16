namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using module;

    public class AttendanceController : AuthController
    {
        // GET: Attendance
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableAttendanceHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData(string pattern, int page, int rows)
        {
            var count = 0;
            var module = new AttendanceModule(CurrentUser);
            var items = module.Page(pattern, page, rows, out count);
            var data = TableAttendanceDataModel.Model.GetJson(items, count, TableAttendanceHeaderModel.Header);
            return data;
        }
    }
}