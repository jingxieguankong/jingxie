namespace cn.tdr.policeequipment.web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Newtonsoft.Json.Linq;
    using Models;
    using common;
    using data.entity;
    using enumerates;
    using module;

    public class OfficerController : AuthController
    {
        // GET: Officer
        public ActionResult Index(int w, int h)
        {
            ViewBag.ViewWidth = w;
            ViewBag.ViewHeight = h;
            return View(TableOfficerHeaderModel.Header);
        }
        
        public JObject GetData(string orgId, string ptId, string name, string code, int page, int rows)
        {
            var count = 0;
            var module = new OfficerModule(CurrentUser);
            var items = module.Page(orgId, ptId, name, code, page, rows, out count);
            var json = TableOfficerDataModel.Model.GetJson(items, count, TableOfficerHeaderModel.Header);
            return json;
        }

        [HttpPost]
        public JsonResult FormSubmit(string id, string orgId, string ptpId, string name, string code, string tel, short sex)
        {
            var officer = new Officer
            {
                OrgId = orgId,
                PtId = ptpId,
                Name = name,
                IdentyCode = code,
                IsDel = (short)DeleteStatus.No,
                Sex = sex,
                Tel = tel,
                SignupDate = DateTime.Now.ToUnixTime()
            };
            var module = new OfficerModule(CurrentUser);
            var data = false;
            var emptyId = string.IsNullOrWhiteSpace(id);
            if (emptyId)
            {
                data = module.Add(officer);
            }

            if (!emptyId)
            {
                officer.Id = id;
                data = module.Modify(officer, t => t.Id == id);
            }

            return Json(new { code = 0, msg = "Ok", data = data }, "text/html");
        }

        [HttpPost]
        public JsonResult BindEquipmentFormSubmit(string bdEqtId, string bdOfficerId, string bdCabId, string bdPtpId, string bdCateId)
        {
            var module = new OfficerModule(CurrentUser);
            var status = module.BindEqt(bdOfficerId, bdEqtId, bdCabId, bdPtpId, bdCateId);
            var data = false;
            var code = (short)status;
            var msg = "Ok";
            if (status == BindEqtResultStatus.Error)
            {
                msg = "发生错误 。";
            }
            if (status == BindEqtResultStatus.Failed)
            {
                msg = "绑定失败，请重试 。";
            }
            if (status == BindEqtResultStatus.Repeate)
            {
                msg = "重复绑定 。";
            }
            if (status == BindEqtResultStatus.Success)
            {
                msg = "成功 。";
                data = true;
            }

            return Json(new { code = code, msg = msg, data = data }, "text/html");
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {
            var module = new OfficerModule(CurrentUser);
            var data = module.Remove(t => t.Id == id);
            return Json(new { code = 0, msg = "Ok", data = data });
        }
    }
}