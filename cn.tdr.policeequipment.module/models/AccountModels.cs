namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class AccountModel
    {
        public User user { get; set; }

        public Role role { get; set; }

        public Organization org { get; set; }
    }
}
