namespace cn.tdr.policeequipment.web.Models
{
    using data.entity;
    using Newtonsoft.Json.Linq;

    public class TableMenuHeaderModel:TableTreeHeaderModel
    {
        private static TableMenuHeaderModel _header;

        public static TableMenuHeaderModel Header
        {
            get { return _header = _header ?? new TableMenuHeaderModel(); }
        }

        private TableMenuHeaderModel() { }

        public TableCellModel title => new TableCellModel { field="title", title="菜单名称" };

        public TableCellModel src => new TableCellModel { field = "src", title = "跳转地址" };

        public TableCellModel order => new TableCellModel { field="order", title="序列" };

        public TableCellModel remmark => new TableCellModel { field = "remark", title = "备注" };
    }

    public class TableMenuDataModel : ConvertJson<Menu, TableMenuHeaderModel>
    {
        private static TableMenuDataModel _m;

        public static TableMenuDataModel Model
        {
            get { return _m = _m ?? new TableMenuDataModel(); }
        }

        private TableMenuDataModel() { }

        public override JObject GetJson(Menu data, TableMenuHeaderModel header)
        {
            var json = new JObject();
            json[header.id.field] = data.Id;
            json[header.title.field] = data.Title;
            json[header.src.field] = data.Src;
            json[header.order.field] = data.Order;
            json[header.remmark.field] = data.Remarks;
            json[header.parentId.field] = data.Pid;
            return json;
        }
    }
}