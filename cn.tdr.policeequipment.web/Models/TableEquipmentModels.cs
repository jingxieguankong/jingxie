namespace cn.tdr.policeequipment.web.Models
{
    using System;
    using Newtonsoft.Json.Linq;
    using data.entity;
    using module.models;

    public class TableEquipmentHeaderModel:TableHeaderModel
    {
        private TableEquipmentHeaderModel() { }

        public static readonly TableEquipmentHeaderModel Header = new TableEquipmentHeaderModel();

        public TableCellModel mod => new TableCellModel { field= "mod", title= "型号" };

        public TableCellModel factory => new TableCellModel { field = "factory", title = "厂家" };

        public TableCellModel factoryCode => new TableCellModel { field = "factoryCode", title = "出厂编码" };

        public TableCellModel tagId => new TableCellModel { field = "tagId", title = "标签ID" };

        public TableCellModel status => new TableCellModel { field = "status", title = "状态" };

        public TableCellModel power => new TableCellModel { field = "power", title = "电量" };

        public TableCellModel isLost => new TableCellModel { field = "isLost", title = "是否遗失" };

        public TableCellModel dispatched => new TableCellModel { field = "dispatched", title = "布控状态" };

        public TableCellModel isChanged => new TableCellModel { field = "isChanged", title = "是否已更换" };

        public TableCellModel changeDate => new TableCellModel { field = "changeDate", title = "更换时间" };

        public TableCellModel inputDate => new TableCellModel { field = "inputDate", title = "入库时间" };

        public TableCellModel factoryDate => new TableCellModel { field = "factoryDate", title = "生产时间" };

        public TableCellModel expiredDate => new TableCellModel { field = "expiredDate", title = "过期时间" };

        public TableCellModel purchaseDate => new TableCellModel { field = "purchaseDate", title = "采购时间" };

        public TableCellModel org => new TableCellModel { field = "org", title = "组织机构" };

        public TableCellModel category => new TableCellModel { field = "category", title = "警械类型" };

        public TableCellModel storage => new TableCellModel { field = "storage", title = "警械库" };

        public TableCellModel cabinet => new TableCellModel { field = "cabinet", title = "警械柜" };

        public TableCellModel officerId => new TableCellModel { field = "officerId", title = "绑定警员" };
    }

    public class TableEquipmentDataModel : ConvertJson<EquipmentModel, TableEquipmentHeaderModel>
    {
        public override JObject GetJson(EquipmentModel data, TableEquipmentHeaderModel header)
        {
            var json = new JObject();
            json[header.cabinet.field] = GetCabinet(data.cabinet);
            json[header.category.field] = GetCategory(data.category);
            json[header.changeDate.field] = data.equipment.ChangeTime;
            json[header.expiredDate.field] = data.equipment.ExpiredTime;
            json[header.factory.field] = data.equipment.Factory;
            json[header.factoryCode.field] = data.equipment.FactorCode;
            json[header.factoryDate.field] = data.equipment.FactorTime;
            json[header.id.field] = data.equipment.Id;
            json[header.inputDate.field] = data.equipment.InputTime;
            json[header.isChanged.field] = data.equipment.IsChanged;
            json[header.dispatched.field] = data.equipment.Dispatched;
            json[header.isLost.field] = data.equipment.IsLost;
            json[header.mod.field] = data.equipment.Model;
            json[header.officerId.field] = data.equipment.OfficerId;
            json[header.org.field] = GetOrg(data.org);
            json[header.power.field] = data.equipment.Power;
            json[header.purchaseDate.field] = data.equipment.PurchaseTime;
            json[header.status.field] = data.equipment.Status;
            json[header.storage.field] = GetStorage(data.storage);
            json[header.tagId.field] = data.equipment.TagId;

            return json;
        }
        
        private JObject GetOrg(Organization org)
        {
            var json = new JObject();
            json["id"] = org.Id;
            json["name"] = org.Name;
            return json;
        }

        private JObject GetStorage(Storage stg)
        {
            var json = new JObject();
            json["id"] = stg.Id;
            json["name"] = stg.Name;
            return json;
        }

        private JObject GetCabinet(Cabinet cab)
        {
            var json = new JObject();
            json["id"] = cab.Id;
            json["name"] = cab.Name;
            return json;
        }

        private JObject GetCategory(EqtCategory cate)
        {
            var json = new JObject();
            json["id"] = cate.Id;
            json["name"] = cate.Name;
            return json;
        }

        private TableEquipmentDataModel() { }

        public static readonly TableEquipmentDataModel Model = new TableEquipmentDataModel();
    }
}