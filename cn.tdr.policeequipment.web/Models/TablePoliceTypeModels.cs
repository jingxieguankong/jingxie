namespace cn.tdr.policeequipment.web.Models
{
    using module.models;
    using Newtonsoft.Json.Linq;

    public class TablePoliceTypeHeaderModel: TableGroupHeaderModel
    {
        public TableCellModel category => new TableCellModel { field = "categoryName", title = "警械类型" };

        public TableCellModel num => new TableCellModel { field="num", title="装备数量" };

        public TableCellModel ispk => new TableCellModel { field="ispk", title="是否主装备" };

        public TableCellModel isrq => new TableCellModel { field = "isrq", title = "是否必备" };

        public TableCellModel categoryId => new TableCellModel { field= "categoryId" };

        public TableCellModel ptp => new TableCellModel { field="ptp" };

        private TablePoliceTypeHeaderModel() { }

        private static TablePoliceTypeHeaderModel _h;

        public static TablePoliceTypeHeaderModel Header
        {
            get { return _h = _h ?? new TablePoliceTypeHeaderModel(); }
        }
    }

    public class TablePoliceTypeDataModel : ConvertJson<PoliceTypeStandardEquipment, TablePoliceTypeHeaderModel>
    {
        public override JObject GetJson(PoliceTypeStandardEquipment data, TablePoliceTypeHeaderModel header)
        {
            var json = new JObject();
            json[header.id.field] = data.equipment.Id;
            json[header.ispk.field] = data.equipment.IsPrimary;
            json[header.isrq.field] = data.equipment.IsRequire;
            json[header.num.field] = data.equipment.Num;
            json[header.category.field] = data.category.Name;
            json[header.categoryId.field] = data.category.Id;

            var orgJson = GetOrg(data.org);
            var ptpJson = GetPoliceType(data.type, orgJson);
            json[header.ptp.field] = ptpJson;            
            json[header.groupId.field] = data.type.Id;

            return json;
        }

        private JObject GetPoliceType(data.entity.PoliceType type, JObject org)
        {
            var json = new JObject();
            json["id"] = type.Id;
            json["name"] = type.Name;
            json["org"] = org;
            return json;
        }

        private JObject GetOrg(data.entity.Organization org)
        {
            var json = new JObject();
            json["id"] = org.Id;
            json["name"] = org.Name;
            return json;
        }

        private TablePoliceTypeDataModel() { }

        private static TablePoliceTypeDataModel _m;

        public static TablePoliceTypeDataModel Model
        {
            get { return _m = _m ?? new TablePoliceTypeDataModel(); }
        }
    }
}