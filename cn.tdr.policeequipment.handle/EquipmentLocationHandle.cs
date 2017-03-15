namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class EquipmentLocationHandle : Handle<EquipmentLocation>
    {
        public EquipmentLocationHandle(IRepository repository) : base(repository)
        {
        }
    }
}
