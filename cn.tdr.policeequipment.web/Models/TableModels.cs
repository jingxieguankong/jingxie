namespace cn.tdr.policeequipment.web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    public class TableCellModel
    {
        public string field { get; set; }

        public string title { get; set; }
    }

    public abstract class TableHeaderModel
    {
        public TableCellModel id => new TableCellModel { field = "id", title = "Id" };
    }

    public abstract class TableGroupHeaderModel:TableHeaderModel
    {
        public TableCellModel groupId => new TableCellModel { field = "groupId", title = groupIdTitle };

        protected virtual string groupIdTitle => "groupId";
    }

    public abstract class TableTreeHeaderModel:TableHeaderModel
    {
        public TableCellModel parentId => new TableCellModel { field = "_parentId", title = ParentIdTitle };

        protected virtual string ParentIdTitle => "ParentId";
    }

    public interface IConvertJson<in T, in THeader>
        where THeader:TableHeaderModel
    {
        JObject GetJson(T data, THeader header);

        JObject GetJson(IEnumerable<T> dataCollection, THeader header);

        JObject GetJson(IEnumerable<T> dataCollection, int dataCount, THeader header);
    }

    public abstract class ConvertJson<T, THeader> : IConvertJson<T, THeader>
        where THeader : TableHeaderModel
    {
        private static readonly string DataFieldName = "rows";
        private static readonly string FooterFieldName = "footer";
        private static readonly string DataCountFieldName = "total";

        public abstract JObject GetJson(T data, THeader header);

        public virtual JObject GetJson(IEnumerable<T> dataCollection, THeader header)
        {
            var arr = dataCollection.Select(t => GetJson(t, header)).ToArray();
            var json = new JObject();
            json[DataFieldName] = new JArray(arr);
            json[FooterFieldName] = new JArray();
            return json;
        }

        public virtual JObject GetJson(IEnumerable<T> dataCollection, int dataCount, THeader header)
        {
            var json = GetJson(dataCollection, header);
            json[DataCountFieldName] = dataCount;
            return json;
        }
    }
}