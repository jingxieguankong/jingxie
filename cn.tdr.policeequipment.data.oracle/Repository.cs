namespace cn.tdr.policeequipment.data.oracle
{
    public class EfRepository : repository.EntityFrameworkRepository
    {
        public EfRepository(string connectionStringName, string owner)
            : base(new DbContext(connectionStringName, owner))
        {
        }
    }
}
