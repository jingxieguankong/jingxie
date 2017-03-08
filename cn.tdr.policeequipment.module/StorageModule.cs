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

    public class StorageModule : MyModule
    {
        public StorageModule(UserInfo user) : base(user)
        {
        }

        public bool Add(Storage storage)
        {
            return false;
        }

        public bool Modify(Storage storage, Expression<Func<Storage, bool>> predicate)
        {
            return false;
        }

        public bool Remove(Expression<Func<Storage, bool>> predicate)
        {
            return false;
        }
        
        public IEnumerable<StorageModel> Page(string orgId, string name, int page, int size, out int count)
        {
            using (var storageHandler = new StorageHandle(Repository))
            using (var orgHandler = new OrganizationHandle(Repository))
            using (var officerHandler = new OfficerHandle(Repository))
            using (var siteHandler = new SiteHandle(Repository))
            {
                var noDel = (short)DeleteStatus.No;
                var query =
                    from stg in storageHandler.All(t => t.IsDel == noDel)
                    join org in orgHandler.All(t => t.IsDel == noDel) on stg.OrgId equals org.Id
                    join officer in officerHandler.All(t => t.IsDel == noDel) on stg.OfficerId equals officer.Id into officers
                    from off in officers.DefaultIfEmpty(new Officer { })
                    join site in siteHandler.All(t => t.IsDel == noDel) on stg.StationId equals site.Id into sites
                    from sta in sites.DefaultIfEmpty(new Station { })
                    select new { stg = stg, org = org, officer = off, sta = sta };

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(t => t.stg.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(orgId))
                {
                    query = query.Where(t => t.stg.OrgId == orgId);
                }

                if (string.IsNullOrWhiteSpace(orgId) && !User.IsSupperAdministrator)
                {
                    var code = User.Organization.Code;
                    query = query.Where(t => t.org.Code.StartsWith(code));
                }

                count = query.Count();
                var skipCount = (page - 1) * size;
                var items = query.OrderBy(t => t.sta.OrgId).Skip(skipCount).Take(size).ToArray()
                    .Select(t => new StorageModel
                    {
                        officer = t.officer,
                        org = t.org,
                        storage = t.stg
                    });
                return items;
            }
        }
    }
}
