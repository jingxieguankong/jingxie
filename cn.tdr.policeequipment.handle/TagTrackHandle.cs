namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class TagTrackHandle : Handle<TagMoveTrail>
    {
        public TagTrackHandle(IRepository repository) : base(repository)
        {
        }
    }
}
