namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class OfficerModel
    {
        public Officer officer { get; set; }

        public User user { get; set; }

        public Organization org { get; set; }

        public PoliceType ptp { get; set; }
    }
}
