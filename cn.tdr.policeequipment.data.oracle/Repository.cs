namespace cn.tdr.policeequipment.data.oracle
{
    public sealed class EfRepository : repository.EntityFrameworkRepository
    {
        private static readonly string ConfigSectionName = "repository";

        protected override System.Data.Entity.DbContext GetDbContext()
        {
            var cfg = ConfigurationSectionHandler.RepositoryConfig(ConfigSectionName);
            return new DbContext(cfg.ConnectionStringOrName, cfg.Owner);
        }
    }
}
