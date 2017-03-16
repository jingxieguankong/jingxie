namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class StationHandle : Handle<Station>
    {
        public StationHandle(IRepository repository) : base(repository)
        {
        }
    }
}
