namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using data.entity;
    using enumerates;
    using handle;
    using models;

    // 地图业务模块
    public class MapModule : MyModule
    {
        public MapModule(UserInfo user) : base(user)
        {
        }

        // 矩形选择
        public IEnumerable<OfficerDispatchQueryModel> RectangleSelect(double x1, double y1, double x2, double y2)
        {
            var lcHandler = new OfficerLocationHandle(Repository);
            var ocHandler = new OfficerHandle(Repository);
            var orgHandler = new OrganizationHandle(Repository);
            var ptHandler = new PoliceTypeHandle(Repository);
            var stHandler = new StationHandle(Repository);

            var noDel = (short)DeleteStatus.No;
            var query =
                from lc in lcHandler.All(null)
                join oc in ocHandler.All(t => t.IsDel == noDel) on lc.OfficerId equals oc.Id
                join org in orgHandler.All(t => t.IsDel == noDel) on oc.OrgId equals org.Id
                join ptp in ptHandler.All(t => t.IsDel == noDel) on oc.PtId equals ptp.Id
                join sts in stHandler.All(t => t.IsDel == noDel) on lc.SiteId equals sts.SiteId into stas
                from st in stas.DefaultIfEmpty(new Station { })
                select new { location = lc, officer = oc, org = org, ptp = ptp, station = st };
            
            // 此处进一步查询


            var items = query.ToArray()
                .Select(t => new OfficerDispatchQueryModel
                {
                    location = t.location,
                    officer = t.officer,
                    org = t.org,
                    ptp = t.ptp,
                    station = t.station
                });
            return items;
        }
    }
}
