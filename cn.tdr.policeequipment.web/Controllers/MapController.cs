namespace cn.tdr.policeequipment.web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    // 地图控制器
    public class MapController : AnonymousController
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Rectangle(double x1, double y1, double x2, double y2)
        {
            return Json(TestData());
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