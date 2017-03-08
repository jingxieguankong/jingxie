namespace cn.tdr.policeequipment.handle
{
    using interfaces;
    using data.entity;

    public class PoliceTypeHandle : Handle<PoliceType>
    {
        public PoliceTypeHandle(IRepository repository) : base(repository)
        {
        }
    }
}
