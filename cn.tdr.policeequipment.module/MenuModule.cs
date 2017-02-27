namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using data.entity;
    using enumerates;
    using handle;
    using models;

    public class MenuModule:MyModule
    {
        public MenuModule(UserInfo user)
            :base(user)
        {
            Handler = new MenuHandle(Repository);
        }

        public MenuHandle Handler { get; private set; }

        /// <summary>
        /// 我的菜单内容
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Menu> MyMenus()
        {
            var menus = User.Menus;
            if (0 == menus.Count())
            {
                using (var menuHandler = new MenuHandle(Repository))
                using (var featureHandler = new FeatureHandle(Repository))
                {
                    var isDel = (short)DeleteStatus.No;
                    var query = menuHandler.All(t => t.IsDel == isDel);
                    // 非管理员用户，需要根据角色来进行边界数据处理
                    if (!User.IsSupperAdministrator)
                    {
                        var roleId = User.Role.Id;
                        query = query.Join(
                            featureHandler.All(t => t.IsDel == isDel && t.RoleId == roleId),
                            m => m.Id,
                            f => f.MenuId,
                            (m, f) => m);
                    }
                    menus = query.ToArray();
                }
            }
            return menus.Distinct();
        }

        public bool Add(Menu menu)
        {
            return null != Handler.Add(menu, true);
        }

        public bool Modify(Menu menu)
        {
            return null != Handler.Modify(menu, true);
        }

        public bool Remove(params string[] idArray)
        {
            var arr = idArray.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();
            return
               0 < Handler.RemoveAny(t => arr.Any(x => t.Id == x), true).Count();
        }

        public IEnumerable<Menu> Query(Expression<Func<Menu, bool>> predicate = null)
        {
            var noDel = (short)DeleteStatus.No;
            return Handler.All(predicate).Where(t => t.IsDel == noDel);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Handler.Dispose();
            Handler = null;
        }
    }
}
