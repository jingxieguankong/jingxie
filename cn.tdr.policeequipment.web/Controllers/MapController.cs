namespace cn.tdr.policeequipment.web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using common;
    using module;
    using cn.tdr.policeequipment.web.Models;

    // 地图控制器
    public class MapController : AuthController
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DrawPoint()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Attendance(string pattern)
        {
            var module = new MapModule(CurrentUser);
            var items = module.AttendanceSelect(pattern, DateTime.Now.Date.AddDays(-7), DateTime.Now);
            var data = items.Select(t => new AttendanceGroupModel
            {
                etime = t.atd.ETime,
                length = t.atd.TimeLength,
                name = t.officer.Name,
                stime = t.atd.STime,
                items = t.tracks.Select(x => new AttendanceItemModel
                {
                    name = x.station.SiteId,
                    time = x.track.UpTime
                }).ToArray()
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Rectangle(double x1, double y1, double x2, double y2)
        {
            var module = new MapModule(CurrentUser);
            var items = module.RectangleSelect(x1, y1, x2, y2);
            var data = new Models.DispatchModel
            {
                groups = items.GroupBy(t => t.ptp).Select(t => new DispatchGroupModel
                {
                    items = t.Select(x => new GroupItemModel
                    {
                        name = x.officer.Name,
                        orgName = x.org.Name,
                        site = new Pointer { x = x.station.Lon, y = x.station.Lat }
                    }).ToArray(),
                    name = t.Key.Name
                }).ToArray()
            };
            return Json(new { code = 0, msg = "Ok", data = data });
        }

        [HttpPost]
        public JsonResult Circle(double x, double y, double r)
        {
            return Json(TestData());
        }

        [HttpPost]
        public JsonResult Irregular(Models.Pointer[] data)
        {
            return Json(TestData());
        }

        public object TestData()
        {
            return new { code = 0, msg = "Ok", data = TestDispatchData() };
        }

        private Models.DispatchModel TestDispatchData()
        {
            return new Models.DispatchModel
            {
                groups = new Models.DispatchGroupModel[] {
                    new Models.DispatchGroupModel {
                         name="{ 警种类型名称 }",
                         items = TestTestDispatchGroupItemData()
                    },
                    new Models.DispatchGroupModel {
                         name="{ 警种类型名称 }",
                         items = TestTestDispatchGroupItemData()
                    }
                }
            };
        }

        private Models.GroupItemModel[] TestTestDispatchGroupItemData()
        {
            int count = (new Random(Guid.NewGuid().GetHashCode())).Next(1, 10);
            const int cv = 100;
            var list = new System.Collections.Generic.List<Models.GroupItemModel>();
            for (int i = 0; i < count; i++)
            {
                var rand = new Random(Guid.NewGuid().GetHashCode());
                
                list.Add(new Models.GroupItemModel
                {
                    name = "{ 警员姓名 }",
                    orgName = "{ 组织机构名称 }",
                    site = new Models.Pointer
                    {
                        x = Math.Round(rand.NextDouble() * cv, 8),
                        y = Math.Round(rand.NextDouble() * cv, 8)
                    }
                });
            }
            return list.ToArray();
        }
    }
}