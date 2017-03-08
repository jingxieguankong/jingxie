namespace cn.tdr.policeequipment.handle
{
    using interfaces;
    using data.entity;

    public class StorageHandle : Handle<Storage>
    {
        public StorageHandle(IRepository repository) : base(repository)
        {
        }
    }
}
