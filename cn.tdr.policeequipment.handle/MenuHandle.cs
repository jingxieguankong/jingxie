namespace cn.tdr.policeequipment.handle
{
    using interfaces;
    using data.entity;

    public class MenuHandle : Handle<Menu>
    {
        public MenuHandle(IRepository repository)
            : base(repository)
        {
        }
    }
}
