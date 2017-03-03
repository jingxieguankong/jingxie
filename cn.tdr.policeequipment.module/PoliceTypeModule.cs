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

    public class PoliceTypeModule : MyModule
    {
        public PoliceTypeModule(UserInfo user) : base(user)
        {
            Handler = new PoliceTypeHandle(Repository);
        }

        public PoliceTypeHandle Handler { get; private set; }

        public bool Add(PoliceType ptp)
        {
            return
                null != Handler.Add(ptp, true);
        }

        public bool Modify(PoliceType ptp, Expression<Func<PoliceType, bool>> predicate)
        {
            var count =
                Handler.ModifyAny(
                    m =>
                    {
                        m.Name = ptp.Name;
                        m.OrgId = ptp.OrgId;
                        return m;
                    }, predicate, true).Count();
            return
                0 < count;
        }

        public bool Remove(Expression<Func<PoliceType, bool>> predicate)
        {
            using (var handler = new StandardEquipmentHandle(Repository))
            {
                var ids = Handler.RemoveAny(predicate).Select(t => t.Id).ToArray();
                handler.RemoveAny(t => ids.Any(x => t.PtId == x));

                var count = Repository.Commit();
                return
                    0 < count;
            }
        }

        public IEnumerable<PoliceTypeStandardEquipment> FeatchAll(string orgId)
        {
            using (var cateHandler = new EqtTypeHandle(Repository))
            using (var stdHandler = new StandardEquipmentHandle(Repository))
            using (var orgHandler = new OrganizationHandle(Repository))
            {
                var noDel = (short)DeleteStatus.No;
                var query =
                    from ptp in Handler.All(t => t.IsDel == noDel)
                    join org in orgHandler.All(t => t.IsDel == noDel) on ptp.OrgId equals org.Id
                    join stditem in stdHandler.All(t => t.IsDel == noDel) on ptp.Id equals stditem.PtId into stds
                    from std in stds.DefaultIfEmpty(new StandardEquipment { })
                    join cateitem in cateHandler.All(t => t.IsDel == noDel) on std.CateId equals cateitem.Id into cates
                    from cate in cates.DefaultIfEmpty(new EqtCategory { })
                    select new { ptp = ptp, org = org, std = std, cate = cate };

                if (!User.IsSupperAdministrator)
                {
                    orgId = string.IsNullOrWhiteSpace(orgId) ? User.Organization.Id : orgId;
                    query = query.Where(t => t.ptp.OrgId == orgId);
                }

                if (User.IsSupperAdministrator && !string.IsNullOrWhiteSpace(orgId))
                {
                    query = query.Where(t => t.ptp.OrgId == orgId);
                }

                var items =
                    query.ToArray().Select(t => new PoliceTypeStandardEquipment
                    {
                        category = t.cate,
                        equipment = t.std,
                        org = t.org,
                        type = t.ptp
                    });
                return items;
            }
        }
    }
}
