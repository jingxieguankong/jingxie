namespace cn.tdr.policeequipment.web.Models
{
    using data.entity;
    using Newtonsoft.Json.Linq;

    public class TableOrgHeaderModel:TableTreeHeaderModel
    {
        private static TableOrgHeaderModel _header;

        public static TableOrgHeaderModel Header
        {
            get { return _header = _header ?? new TableOrgHeaderModel(); }
        }

        private TableOrgHeaderModel() { }

        public TableCellModel name => new TableCellModel { field = "name", title = "机构名称" };

        public TableCellModel code => new TableCellModel { field = "code", title = "机构代码" };
    }

    public class TableOrgDataModel : ConvertJson<Organization, TableOrgHeaderModel>
    {
        private static TableOrgDataModel _m;

        public static TableOrgDataModel Model
        {
            get { return _m = _m ?? new TableOrgDataModel(); }
        }

        private TableOrgDataModel() { }

        public override JObject GetJson(Organization data, TableOrgHeaderModel header)
        {
            var json = new JObject();
            json[header.id.field] = data.Id;
            json[header.code.field] = data.Code;
            json[header.name.field] = data.Name;
            json[header.parentId.field] = data.Pid;
            return json;
        }
    }
}