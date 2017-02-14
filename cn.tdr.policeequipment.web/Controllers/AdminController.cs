namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;

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
            return
                new Models.MenuItemModel[] {
                    new Models.MenuItemModel {
                        title="系统设置",
                        items = new Models.MenuItemModel[] {
                            new Models.MenuItemModel { title="基础设置", src="" },
                            new Models.MenuItemModel { title="组织机构", src="" }
                        }
                    },
                    new Models.MenuItemModel {
                        title = "权限管理",
                        items = new Models.MenuItemModel[] {
                            new Models.MenuItemModel { title="功能菜单", src="" },
                            new Models.MenuItemModel { title="角色功能", src="" },
                            new Models.MenuItemModel { title="用户角色", src="" }
                        }
                    },
                    new Models.MenuItemModel {
                        title = "警员管理",
                        items=new Models.MenuItemModel[] {
                            new Models.MenuItemModel { title="警种", src="" },
                            new Models.MenuItemModel { title="警种装备标准", src="" },
                            new Models.MenuItemModel { title="警员", src="" }
                        }
                    },
                    new Models.MenuItemModel {
                        title="警械管理",
                        items = new Models.MenuItemModel[] {
                            new Models.MenuItemModel { title="警械库", src="" },
                            new Models.MenuItemModel { title="警械柜", src="" },
                            new Models.MenuItemModel { title="警械类型", src="" },
                            new Models.MenuItemModel { title="警械", src="" }
                        }
                    },
                    new Models.MenuItemModel {
                        title="布控管理",
                        items= new Models.MenuItemModel[] {
                            new Models.MenuItemModel { title="正在布控警械", src="" },
                            new Models.MenuItemModel { title="正在布控警员", src="" },
                            new Models.MenuItemModel { title="已撤控警械", src="" },
                            new Models.MenuItemModel { title="已撤控警员", src="" }
                        }
                    },
                    new Models.MenuItemModel {
                        title="统计 & 消息",
                        items=new Models.MenuItemModel[] {
                            new Models.MenuItemModel { title="已分配警械", src="" },
                            new Models.MenuItemModel { title="未分配警械", src="" },
                            new Models.MenuItemModel { title="低电量警械", src="" },
                            new Models.MenuItemModel { title="强撞击警械", src="" },
                            new Models.MenuItemModel { title="已损坏警械", src="" },
                            new Models.MenuItemModel { title="预过期警械", src="" },
                            new Models.MenuItemModel { title="已过期警械", src="" },
                            new Models.MenuItemModel { title="警械使用异常", src="" }
                        }
                    }
                };
        }
    }
}