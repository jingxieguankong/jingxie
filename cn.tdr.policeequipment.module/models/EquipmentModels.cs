namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class EquipmentModel
    {
        public Equipment equipment { get; set; }

        public Officer officer { get; set; }

        public Storage storage { get; set; }

        public Cabinet cabinet { get; set; }

        public Organization org { get; set; }

        public EqtCategory category { get; set; }
    }
}
