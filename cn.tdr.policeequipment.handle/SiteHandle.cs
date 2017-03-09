namespace cn.tdr.policeequipment.handle
{
    using interfaces;
    using data.entity;

    public class SiteHandle : Handle<Station>
    {
        public SiteHandle(IRepository repository) : base(repository)
        {
        }
    }
}
