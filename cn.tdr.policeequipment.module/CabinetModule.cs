namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using data.entity;
    using handle;
    using models;
    using enumerates;

    public class CabinetModule : MyModule
    {
        public CabinetModule(UserInfo user) : base(user)
        {
            Handler = new CabinetHandle(Repository);
        }

        public bool Add(Cabinet cabinet)
        {
            return
                null != Handler.Add(cabinet, true);
        }

        public bool Modify(Cabinet cabinet, Expression<Func<Cabinet, bool>> predicate)
        {
            var items = Handler.ModifyAny(
                m => {
                    m.IsDel = cabinet.IsDel;
                    m.Lat = cabinet.Lat;
                    m.Lon = cabinet.Lon;
                    m.Name = cabinet.Name;
                    m.OrgId = cabinet.OrgId;
                    return m;
                }, predicate, true);

            return
                0 < items.Count();
        }

        public bool Remove(Expression<Func<Cabinet, bool>> predicate)
        {
            var items = Handler.RemoveAny(predicate, true);
            return
                0 < items.Count();
        }

        public IEnumerable<CabinetModel> Page(string orgId, string name, int page, int size, out int count)
        {
            using (var cabinetHandler = new CabinetHandle(Repository))
            using (var orgHandler = new OrganizationHandle(Repository))
            using (var officerHandler = new OfficerHandle(Repository))
            using (var siteHandler = new SiteHandle(Repository))
            {
                var noDel = (short)DeleteStatus.No;
                var query =
                    from cab in cabinetHandler.All(t => t.IsDel == noDel)
                    join org in orgHandler.All(t => t.IsDel == noDel) on cab.OrgId equals org.Id
                    join officer in officerHandler.All(t => t.IsDel == noDel) on cab.OfficerId equals officer.Id into officers
                    from off in officers.DefaultIfEmpty(new Officer { })
                    join site in siteHandler.All(t => t.IsDel == noDel) on cab.StationId equals site.Id into sites
                    from sta in sites.DefaultIfEmpty(new Station { })
                    select new { cab = cab, org = org, officer = off, sta = sta };

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(t => t.cab.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(orgId))
                {
                    query = query.Where(t => t.cab.OrgId == orgId);
                }

                if (string.IsNullOrWhiteSpace(orgId) && !User.IsSupperAdministrator)
                {
                    var code = User.Organization.Code;
                    query = query.Where(t => t.org.Code.StartsWith(code));
                }

                count = query.Count();
                var skipCount = (page - 1) * size;
                var items = query.OrderBy(t => t.sta.OrgId).Skip(skipCount).Take(size).ToArray()
                    .Select(t => new CabinetModel
                    {
                        officer = t.officer,
                        org = t.org,
                        cabinet = t.cab
                    });
                return items;
            }
        }

        public CabinetHandle Handler { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Handler.Dispose();
            Handler = null;
        }
    }
}
