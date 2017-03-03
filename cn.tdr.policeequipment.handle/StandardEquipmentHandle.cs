namespace cn.tdr.policeequipment.handle
{
    using interfaces;
    using data.entity;

    public class StandardEquipmentHandle : Handle<StandardEquipment>
    {
        public StandardEquipmentHandle(IRepository repository) : base(repository)
        {
        }
    }
}
