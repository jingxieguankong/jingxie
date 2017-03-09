namespace cn.tdr.policeequipment.web.Models
{
    using module.models;
    using Newtonsoft.Json.Linq;
    using data.entity;

    public class TableStorageHeaderModel:TableHeaderModel
    {
        private TableStorageHeaderModel() { }

        public static readonly TableStorageHeaderModel Header = new TableStorageHeaderModel();

        public TableCellModel name => new TableCellModel { field="name", title="名称" };

        public TableCellModel site => new TableCellModel { field="siteId", title="基站" };

        public TableCellModel gps => new TableCellModel { field = "gps", title = "位置" };

        public TableCellModel lon => new TableCellModel { field = "lon" };

        public TableCellModel lat => new TableCellModel { field = "lat" };

        public TableCellModel org => new TableCellModel { field="org", title="组织机构" };

        public TableCellModel officer => new TableCellModel { field = "officer", title = "负责人" };
    }

    public class TableStorageDataModel : ConvertJson<StorageModel, TableStorageHeaderModel>
    {
        public override JObject GetJson(StorageModel data, TableStorageHeaderModel header)
        {
            var json = new JObject();
            json[header.gps.field] = $"{data.storage.Lon},{data.storage.Lat}";
            json[header.id.field] = data.storage.Id;
            json[header.name.field] = data.storage.Name;
            json[header.officer.field] = GetOfficer(data.officer);
            json[header.org.field] = GetOrg(data.org);
            json[header.site.field] = data.storage.StationId;

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

        private TableStorageDataModel() { }

        public static readonly TableStorageDataModel Model = new TableStorageDataModel { };
    }
}