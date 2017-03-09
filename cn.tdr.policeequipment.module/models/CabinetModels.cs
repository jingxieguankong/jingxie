namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class CabinetModel
    {
        public Cabinet cabinet { get; set; }

        public Officer officer { get; set; }

        public Organization org { get; set; }
    }
}
