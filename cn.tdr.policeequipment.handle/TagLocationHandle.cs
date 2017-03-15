namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class TagLocationHandle : Handle<TagLocation>
    {
        public TagLocationHandle(IRepository repository) : base(repository)
        {
        }
    }
}
