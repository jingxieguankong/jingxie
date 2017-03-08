namespace cn.tdr.policeequipment.handle
{
    using data.entity;
    using interfaces;

    public class EqtTypeHandle : Handle<EqtCategory>
    {
        public EqtTypeHandle(IRepository repository) : base(repository)
        {
        }
    }
}
