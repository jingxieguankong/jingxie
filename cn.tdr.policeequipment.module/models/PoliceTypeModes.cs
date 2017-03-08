namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class PoliceTypeStandardEquipment
    {
        public PoliceType type { get; set; }

        public EqtCategory category { get; set; }

        public Organization org { get; set; }

        public StandardEquipment equipment { get; set; }
    }
}
