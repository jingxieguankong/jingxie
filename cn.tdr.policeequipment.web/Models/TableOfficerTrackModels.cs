namespace cn.tdr.policeequipment.web.Models
{
    using data.entity;
    using module.models;
    using Newtonsoft.Json.Linq;

    public class TableOfficerTrackHeaderModel:TableHeaderModel
    {
        private TableOfficerTrackHeaderModel() { }
        
        public static readonly TableOfficerTrackHeaderModel Header = new TableOfficerTrackHeaderModel();

        public TableCellModel current => new TableCellModel { field = "current", title = "当前位置" };

        public TableCellModel previous => new TableCellModel { field = "previous", title = "上一个位置" };

        public TableCellModel itime => new TableCellModel { field = "itime", title = "上报时间" };

        public TableCellModel officer => new TableCellModel { field = "officer", title = "警员" };

        public TableCellModel org => new TableCellModel { field = "org", title = "组织机构" };
    }

    public class TableOfficerTrackDataModel : ConvertJson<OfficerTrackModel, TableOfficerTrackHeaderModel>
    {
        public override JObject GetJson(OfficerTrackModel data, TableOfficerTrackHeaderModel header)
        {
            var json = new JObject();
            json[header.id.field] = data.track.Id;
            json[header.officer.field] = GetOfficer(data.officer);
            json[header.current.field] = data.track.SiteId;
            json[header.previous.field] = data.track.PreSiteId;
            json[header.itime.field] = data.track.UpTime;
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

        private TableOfficerTrackDataModel() { }

        public static readonly TableOfficerTrackDataModel Model = new TableOfficerTrackDataModel();
    }
}