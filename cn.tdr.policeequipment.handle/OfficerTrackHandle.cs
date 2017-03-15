namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class OfficerTrackHandle : Handle<OfficerMoveTrail>
    {
        public OfficerTrackHandle(IRepository repository) : base(repository)
        {
        }
    }
}
