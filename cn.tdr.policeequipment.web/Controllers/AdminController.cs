namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using module;
    using module.models;

    public class AdminController : AuthController
    {
        // GET: Admin
        public ActionResult Index()
        {
            var menus = GetMenus();
            return View(menus);
        }

        public Models.MenuItemModel[] GetMenus()
        {
            using (var module = new MenuModule(CurrentUser))
            {
                var items = module.MyMenus();
                var menus = items.Where(t => t.Pid == null || t.Pid == "0").Select(t => new Models.MenuItemModel
                {
                    src = t.Src,
                    title = t.Title,
                    items = items.Where(x => x.Pid == t.Id).Select(x => new Models.MenuItemModel
                    {
                        src = t.Src,
                        title = t.Title,
                        items = new Models.MenuItemModel[0]
                    }).ToArray()
                }).ToArray();
                return menus;
            }
        }

        // 登出系统
        public ActionResult SignOut()
        {
            return View();
        }
    }
}