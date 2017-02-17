namespace cn.tdr.policeequipment.handle
{
    using data.entity;
    using interfaces;

    public class FeatureHandle : Handle<Feature>
    {
        public FeatureHandle(IRepository repository) 
            : base(repository)
        {
        }
    }
}
