namespace cn.tdr.policeequipment.web.Models
{
    using System.Linq;
    using module.models;
    using enumerates;
    using Newtonsoft.Json.Linq;

    public class TableRoleHeaderModel:TableTreeHeaderModel
    {
        private static TableRoleHeaderModel _header;

        public static TableRoleHeaderModel Header
        {
            get { return _header = _header ?? new TableRoleHeaderModel(); }
        }

        private TableRoleHeaderModel() { }

        public TableCellModel roleName => new TableCellModel { field = "roleName", title = "角色" };

        public TableCellModel roleId => new TableCellModel { field="roleId", title="roleId" };

        public TableCellModel menuName => new TableCellModel { field = "menuName", title = "功能" };

        public TableCellModel menuId => new TableCellModel { field = "menuId", title = "menuId" };

        public TableCellModel status => new TableCellModel { field = "status", title = "状态" };

        /// <summary>
        /// 功能组
        /// </summary>
        public TableCellModel[] features 
            => FeatureTypeHelper.Items.Where(t => t.FeatureType != FeatureType.None).Select(
            t => new TableCellModel { field = t.Name, title = t.Summary }).ToArray();
    }

    public class TableRoleDataModel : ConvertJson<RoleMenuFeatureModel, TableRoleHeaderModel>
    {
        private TableRoleDataModel() { }

        private static TableRoleDataModel _m;
        
        public static TableRoleDataModel Model
        {
            get { return _m = _m ?? new TableRoleDataModel(); }
        }

        public override JObject GetJson(RoleMenuFeatureModel data, TableRoleHeaderModel header)
        {
            var json = new JObject();
            if (null == data.menu)
            {
                json[header.id.field] = data.role.Id;
                json[header.parentId.field] = null;
                json[header.menuName.field] = null;
                json[header.roleName.field] = data.role.Name;
                json[header.menuId.field] = null;
            }

            if (null != data.menu)
            {
                json[header.id.field] = string.Join("_", data.role.Id, data.menu.Id);
                json[header.parentId.field] = data.role.Id;
                json[header.menuName.field] = data.menu.Title;
                json[header.roleName.field] = "";
                json[header.menuId.field] = data.menu.Id;
            }

            foreach (var item in header.features)
            {
                var f = data.features.FirstOrDefault(t => t.code == item.field) ?? new FeatureModel { flag = 0 };
                var obj = new JObject();
                obj[nameof(f.id)] = f.id;
                obj[nameof(f.code)] = f.code;
                obj[nameof(f.name)] = f.name;
                obj[nameof(f.flag)] = f.flag;
                json[item.field] = obj;
            }

            json[header.roleId.field] = data.role.Id;
            json[header.status.field] = data.role.Status;
            return json;
        }
    }
}