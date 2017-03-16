namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using cn.tdr.policeequipment.module.models;
    using data.entity;
    using enumerates;
    using handle;

    public class StationModule : MyModule
    {
        public StationModule(UserInfo user) : base(user)
        {
            Handler = new StationHandle(Repository);
        }

        public StationHandle Handler { get; private set; }

        public bool Add(Station station)
        {
            return
                null != Handler.Add(station, true);
        }

        public bool Modify(Station station, Expression<Func<Station, bool>> predicate)
        {
            var items = Handler.ModifyAny(
                t => {
                    t.Lat = station.Lat;
                    t.Lon = station.Lon;
                    t.OrgId = station.OrgId;
                    t.SiteId = station.SiteId;
                    return t;
                }, predicate, true);

            return
                0 < items.Count();
        }

        public bool Remove(string id)
        {
            var items = Handler.RemoveAny(t => t.Id == id, true);
            return 0 < items.Count();
        }

        public IEnumerable<StationModel> Page(string orgId, string siteId, int page, int size, out int count)
        {
            var staHandler = new StationHandle(Repository);
            var orgHandler = new OrganizationHandle(Repository);
            var noDel = (short)DeleteStatus.No;
            var query =
                from sta in staHandler.All(t => t.IsDel == noDel)
                join orgitem in orgHandler.All(t => t.IsDel == noDel) on sta.OrgId equals orgitem.Id into orgs
                from org in orgs.DefaultIfEmpty(new Organization { })
                select new { sta = sta, org = org };

            if (!string.IsNullOrWhiteSpace(siteId))
            {
                query = query.Where(t => t.sta.SiteId.Contains(siteId));
            }

            if (string.IsNullOrWhiteSpace(orgId) && !User.IsSupperAdministrator)
            {
                orgId = User.Organization.Id;
            }

            if (!string.IsNullOrEmpty(orgId))
            {
                query = query.Where(t => t.sta.OrgId == orgId);
            }

            count = query.Count();
            var skipCount = (page - 1) * size;
            var items = query.Skip(skipCount).Take(size).ToArray()
                .Select(t => new StationModel
                {
                    org = t.org,
                    station = t.sta
                });
            return items;
        }
    }
}
