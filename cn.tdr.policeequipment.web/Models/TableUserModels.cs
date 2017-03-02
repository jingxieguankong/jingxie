namespace cn.tdr.policeequipment.web.Models
{
    using module.models;
    using Newtonsoft.Json.Linq;

    public class TableUserHeaderModel:TableHeaderModel
    {
        private static TableUserHeaderModel _header;

        public static TableUserHeaderModel Header
        {
            get { return _header = _header ?? new TableUserHeaderModel(); }
        }

        private TableUserHeaderModel() { }

        public TableCellModel account => new TableCellModel { field = "account", title = "账户" };

        public TableCellModel role => new TableCellModel { field="role", title="角色" };

        public TableCellModel org => new TableCellModel { field = "org", title = "组织机构" };

        public TableCellModel status => new TableCellModel { field = "status", title = "状态" };

        public TableCellModel signup => new TableCellModel { field="signup", title="注册时间" };

        public TableCellModel category => new TableCellModel { field = "category", title = "账户类型" };
    }

    public class TableUserDataModel : ConvertJson<AccountModel, TableUserHeaderModel>
    {
        private static TableUserDataModel _m;

        public static TableUserDataModel Model
        {
            get { return _m = _m ?? new TableUserDataModel(); }
        }

        private TableUserDataModel() { }

        public override JObject GetJson(AccountModel data, TableUserHeaderModel header)
        {
            var json = new JObject();
            json[header.id.field] = data.user.Id;
            json[header.org.field] = data.org.Name;
            json[header.role.field] = data.role.Name;
            json[header.account.field] = data.user.Account;
            json[header.category.field] = data.user.Category;
            json[header.signup.field] = data.user.SignupDate;
            json[header.status.field] = data.user.Status;
            return json;
        }
    }
}