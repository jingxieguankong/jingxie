namespace cn.tdr.policeequipment.handle
{
    using data.entity;
    using interfaces;

    public class OrganizationHandle : Handle<Organization>
    {
        public OrganizationHandle(IRepository repository) 
            : base(repository)
        {
        }
    }
}
