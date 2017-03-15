namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class EquipmentTrackHandle : Handle<EquipmentMoveTrail>
    {
        public EquipmentTrackHandle(IRepository repository) : base(repository)
        {
        }
    }
}
