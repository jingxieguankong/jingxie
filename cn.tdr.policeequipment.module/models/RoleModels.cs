namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class FeatureModel
    {
        public string id { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        /// <summary>
        /// 状态标记。0：标识无；1：标识有
        /// </summary>
        public short flag { get; set; }
    }

    public class RoleMenuFeatureModel
    {
        public Menu menu { get; set; }

        public Role role { get; set; }

        public FeatureModel[] features { get; set; }
    }

    public class RoleFeatureModel
    {
        public Role role { get; set; }

        public RoleMenuFeatureModel[] menus { get; set; }
    }
}
