namespace cn.tdr.policeequipment.web.Models
{
    using Newtonsoft.Json.Linq;
    using data.entity;
    using enumerates;
    using module.models;

    public class TableOfficerHeaderModel : TableHeaderModel
    {
        public TableCellModel name => new TableCellModel { field = "name", title = "姓名" };

        public TableCellModel sex => new TableCellModel { field = "sex", title = "性别" };

        public TableCellModel code => new TableCellModel { field = "code", title = "警号" };

        public TableCellModel avatar => new TableCellModel { field = "avatarsrc", title = "头像" };

        public TableCellModel digital => new TableCellModel { field = "isbinddigital", title = "数字证书" };

        public TableCellModel tel => new TableCellModel { field="tel", title="手机" };

        public TableCellModel signup => new TableCellModel { field="signup", title="注册时间" };

        public TableCellModel account => new TableCellModel { field = "acc" };

        public TableCellModel org => new TableCellModel { field="org" };

        public TableCellModel ptp => new TableCellModel { field = "ptp" };

        private TableOfficerHeaderModel() { }

        private static TableOfficerHeaderModel _h;

        public static TableOfficerHeaderModel Header
        {
            get { return _h = _h ?? new TableOfficerHeaderModel(); }
        }
    }

    public class TableOfficerDataModel : ConvertJson<OfficerModel, TableOfficerHeaderModel>
    {
        public override JObject GetJson(OfficerModel data, TableOfficerHeaderModel header)
        {
            var json = new JObject();
            json[header.account.field] = GetAcc(data.user);
            json[header.avatar.field] = data.officer.AtrUrl;
            json[header.code.field] = data.officer.IdentyCode;
            json[header.digital.field] = string.IsNullOrWhiteSpace(data.officer.DigitalID) ? 0 : 1;
            json[header.id.field] = data.officer.Id;
            json[header.name.field] = data.officer.Name;
            json[header.org.field] = GetOrg(data.org);
            json[header.ptp.field] = GetPtp(data.ptp);
            json[header.sex.field] = data.officer.Sex;
            json[header.signup.field] = data.officer.SignupDate;
            json[header.tel.field] = data.officer.Tel;
            return json;
        }

        private JObject GetOrg(Organization org)
        {
            var json = new JObject();
            json["id"] = org.Id;
            json["name"] = org.Name;
            return json;
        }

        private JObject GetPtp(PoliceType ptp)
        {
            var json = new JObject();
            json["id"] = ptp.Id;
            json["name"] = ptp.Name;
            return json;
        }

        private JObject GetAcc(User usr)
        {
            var json = new JObject();
            json["id"] = usr.Id;
            json["cate"] = usr.Category;
            json["online"] = usr.SigninStatus == (short)AccountSigninStatus.Online ? 1 : 0;
            json["status"] = usr.Status;
            return json;
        }

        private TableOfficerDataModel() { }

        private static TableOfficerDataModel _m;

        public static TableOfficerDataModel Model
        {
            get { return _m = _m ?? new TableOfficerDataModel(); }
        }
    }
}