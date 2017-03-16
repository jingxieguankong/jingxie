namespace cn.tdr.policeequipment.web.Models
{
    using System;
    using Newtonsoft.Json.Linq;
    using data.entity;
    using module.models;

    public class TableAttendanceHeaderModel:TableHeaderModel
    {
        private static TableAttendanceHeaderModel _h;

        public static readonly TableAttendanceHeaderModel Header = new TableAttendanceHeaderModel();

        private TableAttendanceHeaderModel() { }

        public TableCellModel stime => new TableCellModel { field="stime", title="开始时间" };

        public TableCellModel etime => new TableCellModel { field="etime", title="结束时间" };

        public TableCellModel officer => new TableCellModel { field = "officer", title="警员" };

        public TableCellModel length => new TableCellModel { field = "lngth", title = "出勤时长" };

        public TableCellModel org => new TableCellModel { field = "org", title = "组织机构" };
    }

    public class TableAttendanceDataModel : ConvertJson<AttendanceModel, TableAttendanceHeaderModel>
    {
        public override JObject GetJson(AttendanceModel data, TableAttendanceHeaderModel header)
        {
            var json = new JObject();
            json[header.etime.field] = data.attendance.ETime;
            json[header.id.field] = data.attendance.Id;
            json[header.length.field] = data.attendance.TimeLength;
            json[header.officer.field] = GetOfficer(data.officer);
            json[header.stime.field] = data.attendance.STime;
            json[header.org.field] = GetOrg(data.org);

            return json;
        }

        private JObject GetOrg(Organization org)
        {
            var json = new JObject();
            json["id"] = org.Id;
            json["name"] = org.Name;
            return json;
        }

        private JObject GetOfficer(Officer officer)
        {
            var json = new JObject();
            json["id"] = officer.Id;
            json["name"] = officer.Name;
            json["code"] = officer.IdentyCode;

            return json;
        }

        private static TableAttendanceDataModel _m;

        public static readonly TableAttendanceDataModel Model = new TableAttendanceDataModel();

        private TableAttendanceDataModel() { }
    }
}