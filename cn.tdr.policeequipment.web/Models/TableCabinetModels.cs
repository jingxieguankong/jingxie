namespace cn.tdr.policeequipment.web.Models
{
    using module.models;
    using Newtonsoft.Json.Linq;
    using data.entity;

    public class TableCabinetHeaderModel : TableHeaderModel
    {
        private TableCabinetHeaderModel() { }

        public static readonly TableCabinetHeaderModel Header = new TableCabinetHeaderModel();

        public TableCellModel name => new TableCellModel { field = "name", title = "名称" };

        public TableCellModel site => new TableCellModel { field = "siteId", title = "基站" };

        public TableCellModel gps => new TableCellModel { field = "gps", title = "位置 ( GPS )" };

        public TableCellModel lon => new TableCellModel { field = "lon" };

        public TableCellModel lat => new TableCellModel { field = "lat" };

        public TableCellModel org => new TableCellModel { field = "org", title = "组织机构" };

        public TableCellModel officer => new TableCellModel { field = "officer", title = "负责人" };
    }

    public class TableCabinetDataModel : ConvertJson<CabinetModel, TableCabinetHeaderModel>
    {
        public override JObject GetJson(CabinetModel data, TableCabinetHeaderModel header)
        {
            var json = new JObject();
            json[header.gps.field] = $"{data.cabinet.Lon},{data.cabinet.Lat}";
            json[header.lat.field] = data.cabinet.Lat;
            json[header.lon.field] = data.cabinet.Lon;
            json[header.id.field] = data.cabinet.Id;
            json[header.name.field] = data.cabinet.Name;
            json[header.officer.field] = GetOfficer(data.officer);
            json[header.org.field] = GetOrg(data.org);
            json[header.site.field] = data.cabinet.StationId;

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
            return json;
        }

        private TableCabinetDataModel() { }


        public static readonly TableCabinetDataModel Model = new TableCabinetDataModel();
    }
}