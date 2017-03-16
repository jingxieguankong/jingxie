namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class EquipmentAllopatryExceptHandle : Handle<EquipmentAllopatryExcept>
    {
        public EquipmentAllopatryExceptHandle(IRepository repository) : base(repository)
        {
        }
    }
}
