namespace cn.tdr.policeequipment.web.Models
{
    using module.models;
    using Newtonsoft.Json.Linq;

    public class TablePoliceTypeHeaderModel: TableGroupHeaderModel
    {
        public TableCellModel org => new TableCellModel { field="orgName", title="组织机构" };

        public TableCellModel ptp => new TableCellModel { field="ptpName", title="警种类型" };

        public TableCellModel category => new TableCellModel { field = "categoryName", title = "警械类型" };

        public TableCellModel num => new TableCellModel { field="num", title="装备数量" };

        public TableCellModel ispk => new TableCellModel { field="ispk", title="是否主装备" };

        public TableCellModel isrq => new TableCellModel { field = "isrq", title = "是否必备" };

        public TableCellModel orgId => new TableCellModel { field="orgId" };

        public TableCellModel ptpId => new TableCellModel { field="ptpId" };

        public TableCellModel categoryId => new TableCellModel { field= "categoryId" };

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
            json[header.org.field] = data.org.Name;
            json[header.orgId.field] = data.org.Id;
            json[header.ptp.field] = data.type.Name;
            json[header.ptpId.field] = data.type.Id;
            json[header.groupId.field] = data.type.Id;

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