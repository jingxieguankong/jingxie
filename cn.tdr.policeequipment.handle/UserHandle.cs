namespace cn.tdr.policeequipment.handle
{
    using data.entity;
    using interfaces;

    public class UserHandle : Handle<User>
    {
        public UserHandle(IRepository repository) 
            : base(repository)
        {
        }
    }
}
