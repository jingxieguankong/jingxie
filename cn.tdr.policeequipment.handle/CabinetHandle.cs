namespace cn.tdr.policeequipment.handle
{
    using interfaces;
    using data.entity;

    public class CabinetHandle : Handle<Cabinet>
    {
        public CabinetHandle(IRepository repository) : base(repository)
        {
        }
    }
}
