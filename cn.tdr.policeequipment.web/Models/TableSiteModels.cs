namespace cn.tdr.policeequipment.web.Models
{
    using System;
    using module.models;
    using Newtonsoft.Json.Linq;
    using cn.tdr.policeequipment.data.entity;

    public class TableSiteHeaderModel:TableHeaderModel
    {
        private TableSiteHeaderModel() { }

        public static readonly TableSiteHeaderModel Header = new TableSiteHeaderModel();

        public TableCellModel siteId => new TableCellModel { field="siteId", title="基站编号" };

        public TableCellModel org => new TableCellModel { field = "org", title = "组织机构" };

        public TableCellModel category => new TableCellModel { field="category", title="类型" };

        public TableCellModel gps => new TableCellModel { field = "gps", title = "位置 ( GPS )" };

        public TableCellModel orgId => new TableCellModel { field="orgId" };

        public TableCellModel lon => new TableCellModel { field = "lon" };

        public TableCellModel lat => new TableCellModel { field = "lat" };
    }

    public class TableSiteDataModel : ConvertJson<StationModel, TableSiteHeaderModel>
    {
        public override JObject GetJson(StationModel data, TableSiteHeaderModel header)
        {
            var json = new JObject();
            json[header.gps.field] = $"{data.station.Lon},{data.station.Lat}";
            json[header.lat.field] = data.station.Lat;
            json[header.lon.field] = data.station.Lon;
            json[header.id.field] = data.station.Id;
            json[header.org.field] = GetOrg(data.org);
            json[header.orgId.field] = data.org.Id;
            json[header.siteId.field] = data.station.SiteId;
            json[header.category.field] = data.station.Category;

            return json;
        }

        private JObject GetOrg(Organization org)
        {
            var json = new JObject();
            json["id"] = org.Id;
            json["name"] = org.Name;
            return json;
        }

        private TableSiteDataModel() { }

        public static readonly TableSiteDataModel Model = new TableSiteDataModel();
    }
}