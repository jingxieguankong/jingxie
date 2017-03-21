namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using enumerates;
    using module;

    public class MenuController : AuthController
    {
        // GET: Menu
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableMenuHeaderModel.Header);
        }

        [HttpGet]
        public JObject GetData()
        {
            var m = new MenuModule(CurrentUser);
            var items = m.Query().OrderByDescending(t => t.Order);
            var json = TableMenuDataModel.Model.GetJson(items, TableMenuHeaderModel.Header);
            return json;
        }

        [HttpGet]
        public JsonResult MenuTree()
        {
            var m = new MenuModule(CurrentUser);
            var items = m.Query().ToArray();
            var data = items.Where(t => string.IsNullOrWhiteSpace(t.Pid) || t.Pid == "0").Select(t => new ComboTreeModel
            {
                id = t.Id,
                text = t.Title,
                children = new ComboTreeModel[0]
                //children = items.Where(x => x.Pid == t.Id).Select(x => new ComboTreeModel
                //{
                //    id = x.Id,
                //    text = x.Title,
                //    children = new ComboTreeModel[0]
                //}).ToArray()
            }).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FormSubmit(string id, string title, string src, short order, string parentId, string remark)
        {
            var flag = false;
            var mod = new MenuModule(CurrentUser);
            var m = new data.entity.Menu
            {
                IsDel = (short)DeleteStatus.No,
                Order = order,
                Pid = parentId,
                Remarks = remark,
                Src = src,
                Title = title
            };
            if (string.IsNullOrWhiteSpace(id))
            {
                // insert action
                flag = mod.Add(m);
            }
            else
            {
                // upgrade action
                m.Id = id;
                flag = mod.Modify(m);
            }

            return Json(new { code = 0, msg = "Ok", data = flag }, "text/html");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var mod = new MenuModule(CurrentUser);
            var flag = mod.Remove(id);
            return Json(new { code = 0, msg = "Ok", data = flag });
        }
    }
}