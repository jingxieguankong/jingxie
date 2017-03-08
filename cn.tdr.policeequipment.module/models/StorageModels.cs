namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class StorageModel
    {
        public Storage storage { get; set; }

        public Officer officer { get; set; }

        public Organization org { get; set; }
    }
}
