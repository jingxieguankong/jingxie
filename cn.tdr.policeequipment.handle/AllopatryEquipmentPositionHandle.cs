namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class AllopatryEquipmentPositionHandle : Handle<AllopatryEquipmentPosition>
    {
        public AllopatryEquipmentPositionHandle(IRepository repository) : base(repository)
        {
        }
    }
}
