namespace cn.tdr.policeequipment.module
{
    using System.Collections.Generic;
    using System.Linq;
    using enumerates;
    using handle;
    using models;

    public class OfficerTrackModule : MyModule
    {
        public OfficerTrackModule(UserInfo user) : base(user)
        {
            Handler = new OfficerTrackHandle(Repository);
        }

        public OfficerTrackHandle Handler { get; private set; }
        
        public IEnumerable<OfficerTrackModel> Page(string pattern, int page, int size, out int count)
        {
            var orcHandler = new OfficerHandle(Repository);
            var orgHandler = new OrganizationHandle(Repository);
            var trkHandler = new OfficerTrackHandle(Repository);
            var noDel = (short)DeleteStatus.No;
            var query =
                from trk in trkHandler.All(null)
                join ocr in orcHandler.All(t => t.IsDel == noDel) on trk.OfficerId equals ocr.Id
                join org in orgHandler.All(t => t.IsDel == noDel) on ocr.OrgId equals org.Id
                select new { track = trk, officer = ocr, org = org };

            if (!string.IsNullOrWhiteSpace(pattern))
            {
                query = query.Where(t => t.officer.Name.Contains(pattern) || t.officer.IdentyCode.Contains(pattern));
            }

            if (!User.IsSupperAdministrator)
            {
                var orgId = User.Organization.Id;
                query = query.Where(t => t.officer.OrgId == orgId);
            }

            count = query.Count();
            var skipCount = (page - 1) * size;
            var items = query.OrderByDescending(t => t.track.UpTime).Skip(skipCount).Take(size).ToArray()
                .Select(t => new OfficerTrackModel
                {
                    officer = t.officer,
                    org = t.org,
                    track = t.track
                });
            return items;
        }
    }
}
