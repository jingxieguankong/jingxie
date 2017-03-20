namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using common;
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
            var xmin = x1;
            var xmax = x2;
            if (x1 > x2)
            {
                xmax = x1;
                xmin = x2;
            }

            var ymin = y1;
            var ymax = y2;
            if (y1 > y2)
            {
                ymin = y2;
                ymax = y1;
            }
            query = query.Where(t => t.station.Lon >= xmin && t.station.Lon <= xmax && t.station.Lat >= ymin && t.station.Lat <= ymax);
            if (!User.IsSupperAdministrator)
            {
                var orgId = User.Organization.Id;
                query = query.Where(t => t.org.Id == orgId);
            }

            var items = query.OrderByDescending(t => t.location.UpTime).ToArray()
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

        public IEnumerable<OfficerAttendanceQueryModel> AttendanceSelect(string pattern, DateTime stime, DateTime etime)
        {
            var ocrHandler = new OfficerHandle(Repository);
            var orgHandler = new OrganizationHandle(Repository);
            var ptHandler = new PoliceTypeHandle(Repository);
            var atdHandler = new OfficerAttendanceHandle(Repository);

            var noDel = (short)DeleteStatus.No;
            var query =
                from atd in atdHandler.All(null)
                join ocr in ocrHandler.All(t => t.IsDel == noDel) on atd.OfficerId equals ocr.Id
                join org in orgHandler.All(t => t.IsDel == noDel) on ocr.OrgId equals org.Id
                join ptp in ptHandler.All(t => t.IsDel == noDel) on ocr.PtId equals ptp.Id
                select new { atd = atd, ocr = ocr, org = org, ptp = ptp };

            if (!string.IsNullOrWhiteSpace(pattern))
            {
                query = query.Where(t => t.ocr.Name.Contains(pattern) || t.ocr.IdentyCode.Contains(pattern));
            }

            if (!User.IsSupperAdministrator)
            {
                var orgId = User.Organization.Id;
                query = query.Where(t => t.org.Id == orgId);
            }

            var items = query.OrderBy(t => t.ocr.Id).OrderByDescending(t => t.atd.STime).ToArray();
            if (0 == items.Count())
            {
                return new OfficerAttendanceQueryModel[0];
            }


            var sttime = items.Select(t => t.atd.STime).Min();
            var ettime = DateTime.Now.ToUnixTime();
            if (!items.Any(t => t.atd.ETime == 0L))
            {
                ettime = items.Select(t => t.atd.ETime).Max();
            }
            var ids = items.Select(t => t.atd.OfficerId).Distinct().ToArray();
            var tracks = OfficerTracks(sttime, ettime, ids);
            var data = items.Select(t => {
                var m = new OfficerAttendanceQueryModel
                {
                    atd = t.atd,
                    officer = t.ocr,
                    org = t.org,
                    ptp = t.ptp
                };

                var sdtime = t.atd.STime;
                var edtime = ettime;
                if (t.atd.ETime != 0L)
                {
                    edtime = t.atd.ETime;
                }
                m.tracks = tracks.Where(x => x.track.OfficerId == t.atd.OfficerId && x.track.UpTime >= sdtime && x.track.UpTime <= edtime);

                return m;
            });

            return data;
        }

        public IEnumerable<OfficerAttendanceTrackModel> OfficerTracks(long stime, long etime,
            params string[] officerIds)
        {
            var trackHandler = new OfficerTrackHandle(Repository);
            var stHandler = new StationHandle(Repository);
            var ids = officerIds.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();

            var noDel = (short)DeleteStatus.No;
            var query =
                from track in trackHandler.All(null)
                join sta in stHandler.All(t => t.IsDel == noDel) on track.SiteId equals sta.SiteId into stas
                from site in stas.DefaultIfEmpty(new Station { })
                select new { track = track, site = site };
            
            query = query.Where(t => t.track.UpTime >= stime && t.track.UpTime <= etime);
            if (ids.Length > 0)
            {
                query = query.Where(t => ids.Any(x => t.track.OfficerId == x));
            }

            var items =
                query.OrderBy(t => t.track.OfficerId).OrderByDescending(t => t.track.UpTime).ToArray()
                .Select(t => new OfficerAttendanceTrackModel { station = t.site, track = t.track });

            return items;
        }
    }
}
