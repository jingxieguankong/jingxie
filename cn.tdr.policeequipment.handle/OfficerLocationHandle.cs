namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class OfficerLocationHandle : Handle<OfficerLocation>
    {
        public OfficerLocationHandle(IRepository repository) : base(repository)
        {
        }
    }
}
