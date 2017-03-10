namespace cn.tdr.policeequipment.handle
{
    using interfaces;
    using data.entity;

    public class EquipmentHandle : Handle<Equipment>
    {
        public EquipmentHandle(IRepository repository) : base(repository)
        {
        }
    }
}
