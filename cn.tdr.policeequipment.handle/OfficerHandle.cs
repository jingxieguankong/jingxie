namespace cn.tdr.policeequipment.handle
{
    using interfaces;
    using data.entity;

    public class OfficerHandle : Handle<Officer>
    {
        public OfficerHandle(IRepository repository) 
            : base(repository)
        {
        }
    }
}
