namespace cn.tdr.policeequipment.data.oracle
{
    using System.Data.Entity;
    using cn.tdr.policeequipment.data.entity;

    public class DbContext:System.Data.Entity.DbContext
    {
        private readonly string Owner = string.Empty;
    
        public DbContext(string connectionStringName, string owner)
            :base($"name={connectionStringName}")
        {
            if (string.IsNullOrWhiteSpace(owner))
            {
                throw new System.ArgumentNullException(nameof(owner));
            }
            Owner = owner;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema($"{Owner.ToUpper()}");
            modelBuilder.Entity<AllopatryEquipmentPosition>().ToTable("tb_AllopatryEquipmentPosition");
            modelBuilder.Entity<Cabinet>().ToTable("tb_Cabinet");
            modelBuilder.Entity<DispatchEquipmentPosition>().ToTable("tb_DispatchEquipmentPosition");
            modelBuilder.Entity<EqtCategory>().ToTable("tb_EqtCategory");
            modelBuilder.Entity<Equipment>().ToTable("tb_Equipment");
            modelBuilder.Entity<EquipmentAllopatryExcept>().ToTable("tb_EquipmentAllopatryExcept");
            modelBuilder.Entity<EquipmentDispatch>().ToTable("tb_EquipmentDispatch");
            modelBuilder.Entity<Hit>().ToTable("tb_Hit");
            modelBuilder.Entity<Officer>().ToTable("tb_Officer");
            modelBuilder.Entity<Organization>().ToTable("tb_Organization");
            modelBuilder.Entity<PoliceType>().ToTable("tb_PoliceType");
            modelBuilder.Entity<StandardEquipment>().ToTable("tb_StandardEquipment");
            modelBuilder.Entity<Station>().ToTable("tb_Station");
            modelBuilder.Entity<StockAlert>().ToTable("tb_StockAlert");
            modelBuilder.Entity<Storage>().ToTable("tb_Storage");
            modelBuilder.Entity<TagMoveTrail>().ToTable("tb_TagMoveTrail");
            modelBuilder.Entity<ActionLog>().ToTable("tb_sys_ActionLog");
            modelBuilder.Entity<Feature>().ToTable("tb_sys_Feature");
            modelBuilder.Entity<Menu>().ToTable("tb_sys_Menu");
            modelBuilder.Entity<Role>().ToTable("tb_sys_Role");
            modelBuilder.Entity<User>().ToTable("tb_sys_User");
        }
        
        public virtual DbSet<AllopatryEquipmentPosition> AllopatryEquipmentPositionItems { get; set; }
        public virtual DbSet<Cabinet> CabinetItems { get; set; }
        public virtual DbSet<DispatchEquipmentPosition> DispatchEquipmentPositionItems { get; set; }
        public virtual DbSet<EqtCategory> EqtCategoryItems { get; set; }
        public virtual DbSet<Equipment> EquipmentItems { get; set; }
        public virtual DbSet<EquipmentAllopatryExcept> EquipmentAllopatryExceptItems { get; set; }
        public virtual DbSet<EquipmentDispatch> EquipmentDispatchItems { get; set; }
        public virtual DbSet<Hit> HitItems { get; set; }
        public virtual DbSet<Officer> OfficerItems { get; set; }
        public virtual DbSet<Organization> OrganizationItems { get; set; }
        public virtual DbSet<PoliceType> PoliceTypeItems { get; set; }
        public virtual DbSet<StandardEquipment> StandardEquipmentItems { get; set; }
        public virtual DbSet<Station> StationItems { get; set; }
        public virtual DbSet<StockAlert> StockAlertItems { get; set; }
        public virtual DbSet<Storage> StorageItems { get; set; }
        public virtual DbSet<TagMoveTrail> TagMoveTrailItems { get; set; }
        public virtual DbSet<ActionLog> ActionLogItems { get; set; }
        public virtual DbSet<Feature> FeatureItems { get; set; }
        public virtual DbSet<Menu> MenuItems { get; set; }
        public virtual DbSet<Role> RoleItems { get; set; }
        public virtual DbSet<User> UserItems { get; set; }
    }
}
