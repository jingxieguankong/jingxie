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

    public class RoleModule : MyModule
    {
        public RoleModule(UserInfo user) : base(user)
        {
        }

        public bool Add(Role role)
        {
            using (var handler = new RoleHandle(Repository))
            {
                return null != handler.Add(role, true);
            }
        }

        public bool Add(Feature feature)
        {
            using (var handler = new FeatureHandle(Repository))
            {
                return null != handler.Add(feature, true);
            }
        }

        public bool Remove(Expression<Func<Role, bool>> predicate)
        {
            using (var roleHandler = new RoleHandle(Repository))
            using (var featureHandler = new FeatureHandle(Repository))
            {
                var items = roleHandler.RemoveAny(predicate);
                featureHandler.RemoveAny(t => items.Any(x => t.RoleId == x.Id));

                return 0 < Repository.Commit();
            }
        }

        public bool Remove(string featureId)
        {
            using (var handler = new FeatureHandle(Repository))
            {
                return 0 < handler.RemoveAny(t => t.Id == featureId, true).Count();
            }
        }

        public bool Remove(string roleId, string menuId)
        {
            using (var handler = new FeatureHandle(Repository))
            {
                return 0 < handler.RemoveAny(t => t.RoleId == roleId && t.MenuId == menuId, true).Count();
            }
        }

        public IEnumerable<RoleFeatureModel> FeatchAll()
        {
            var noDel = (short)DeleteStatus.No;
            using (var featureHandler = new FeatureHandle(Repository))
            using (var roleHandler = new RoleHandle(Repository))
            using (var menuHandler = new MenuHandle(Repository))
            {
                var query =
                    from role in roleHandler.All(t => t.IsDel == noDel)
                    join featureitem in featureHandler.All(t => t.IsDel == noDel) on role.Id equals featureitem.RoleId into features
                    from feature in features.DefaultIfEmpty(new Feature { })
                    join menuitem in menuHandler.All(t => t.IsDel == noDel) on feature.MenuId equals menuitem.Id into menus
                    from menu in menus.DefaultIfEmpty(new Menu { })
                    select new { feature = feature, role = role, menu = menu };

                if (!User.IsSupperAdministrator)
                {
                    query = query.Where(t => t.role.OrgId == User.Organization.Id);
                }

                var data =
                    query.ToArray().GroupBy(t => t.role).Select(
                        t => new RoleFeatureModel
                        {
                            role = t.Key,
                            menus = t.GroupBy(x => x.menu).Select(x => new RoleMenuFeatureModel
                            {
                                menu = x.Key,
                                role = t.Key,
                                features = Union(x.Select(f => f.feature)).ToArray()
                            }).ToArray()
                        }).ToArray();
                return data;
            }
        }

        private IEnumerable<FeatureModel> Union(IEnumerable<Feature> items)
        {
            var query =
                from cate in FeatureTypeHelper.Items.Where(t => t.FeatureType != FeatureType.None)
                join item in items on cate.Name equals item.ActId into features
                from feature in features.DefaultIfEmpty()
                select new { category = cate, feature = feature };

            var data = query.Select(t => {
                var m = new FeatureModel
                {
                    flag = 0,
                    code = t.category.Name,
                    name = t.category.Summary
                };
                if (null != t.feature)
                {
                    m.flag = 1;
                    m.id = t.feature.Id;
                }
                return m;
            });
            return data;
        }
    }
}
