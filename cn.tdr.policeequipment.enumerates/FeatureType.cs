namespace cn.tdr.policeequipment.enumerates
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// 系统功能类型
    /// </summary>
    public enum FeatureType:short
    {
        [Description("暂无")]
        None = 0,
        [Description("添加")]
        Add = 1,
        [Description("移除")]
        Remove = 2,
        [Description("编辑")]
        Edit = 3,
        [Description("查看")]
        Query = 4,
        [Description("上传")]
        Upload = 5,
        [Description("下载")]
        Download = 6,
    }

    public static class FeatureTypeHelper
    {
        private static readonly List<FeatureTypeModel> Items = new List<FeatureTypeModel>();

        static FeatureTypeHelper()
        {
            foreach (var item in Enum.GetValues(typeof(FeatureType)))
            {
                var feature = (FeatureType)item;
                Items.Add(new FeatureTypeModel(feature));
            }
        }

        public static FeatureTypeModel GetInfo(this FeatureType feature)
        {
            return Items.FirstOrDefault(t => t.FeatureType == feature);
        }
    }

    public sealed class FeatureTypeModel
    {
        public FeatureTypeModel(FeatureType featureType)
        {
            FeatureType = featureType;

            var type = featureType.GetType();
            Name = $"{featureType}";

            var attribute = 
                type.GetField(Name)
                .GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            Summary = attribute.Description;
        }

        public FeatureType FeatureType { get; private set; }

        public string Name { get; private set; }

        public string Summary { get; private set; }
    }
}
