namespace cn.tdr.policeequipment.handle
{
    using data.entity;
    using interfaces;

    public class RoleHandle : Handle<Role>
    {
        public RoleHandle(IRepository repository) 
            : base(repository)
        {
        }
    }
}
