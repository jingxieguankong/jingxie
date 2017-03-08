namespace cn.tdr.policeequipment.web.Models
{
    using data.entity;
    using Newtonsoft.Json.Linq;

    public class TableEqtTypeHeaderModel:TableTreeHeaderModel
    {
        private static TableEqtTypeHeaderModel _h;

        private TableEqtTypeHeaderModel() { }

        public static TableEqtTypeHeaderModel Header
        {
            get { return _h = _h ?? new TableEqtTypeHeaderModel(); }
        }

        public TableCellModel name => new TableCellModel { field="name", title="名称" };

        public TableCellModel code => new TableCellModel { field = "code", title = "代码" };

        public TableCellModel hits => new TableCellModel { field = "hits", title = "撞击次数" };
    }

    public class TableEqtTypeDataModel : ConvertJson<EqtCategory, TableEqtTypeHeaderModel>
    {
        private static TableEqtTypeDataModel _m;
        private TableEqtTypeDataModel() { }

        public static TableEqtTypeDataModel Model
        {
            get { return _m = _m ?? new TableEqtTypeDataModel(); }
        }

        public override JObject GetJson(EqtCategory data, TableEqtTypeHeaderModel header)
        {
            var json = new JObject();
            json[header.code.field] = data.Code;
            json[header.hits.field] = data.HitCount;
            json[header.id.field] = data.Id;
            json[header.name.field] = data.Name;
            json[header.parentId.field] = data.Pid;

            return json;
        }
    }
}