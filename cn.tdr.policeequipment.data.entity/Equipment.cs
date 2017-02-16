namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Equipment:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 关联组织机构表主键，标识当前警械的归属组织机构
        /// </summary>
        public string OrgId{ get; set; }
         
        /// <summary>
        /// 关联警械库表主键，标识警械装备存放的警械库
        /// </summary>
        public string LibId{ get; set; }
         
        /// <summary>
        /// 关联警械柜的主键，当警械装备被分配为警员，并且当前警员有绑定警械柜时，才有值；否则为 NULL
        /// </summary>
        public string CabId{ get; set; }
         
        /// <summary>
        /// 关联警械类型表主键，标识警械装备的类型
        /// </summary>
        public string CateId{ get; set; }
         
        /// <summary>
        /// 关联警员表的主键，当前警械装备被标识使用时，才有值；否则，为 NULL
        /// </summary>
        public string OfficerId{ get; set; }
         
        /// <summary>
        /// 警械装备的型号
        /// </summary>
        public string Model{ get; set; }
         
        /// <summary>
        /// 警械装备的生产厂家
        /// </summary>
        public string Factory{ get; set; }
         
        /// <summary>
        /// 警械装备的出厂编码
        /// </summary>
        public string FactorCode{ get; set; }
         
        /// <summary>
        /// 与警械装备绑定的标签
        /// </summary>
        public string TagId{ get; set; }
         
        /// <summary>
        /// 警械装备的入库时间
        /// </summary>
        public long InputTime{ get; set; }
         
        /// <summary>
        /// 警械状态。0：标识正常；-1：标识异常；-2：标识损坏；
        /// </summary>
        public short Status{ get; set; }
         
        /// <summary>
        /// 标签电池电量
        /// </summary>
        public short Power{ get; set; }
         
        /// <summary>
        /// 是否遗失。0：未遗失；1：遗失。
        /// </summary>
        public short IsLost{ get; set; }
         
        /// <summary>
        /// 是否已更换。0：未更换，1：已更换。
        /// </summary>
        public short IsChanged{ get; set; }
         
        /// <summary>
        /// 更换时间
        /// </summary>
        public long ChangeTime{ get; set; }
         
        /// <summary>
        /// 生产时间
        /// </summary>
        public long FactorTime{ get; set; }
         
        /// <summary>
        /// 过期时间
        /// </summary>
        public long ExpiredTime{ get; set; }
         
        /// <summary>
        /// 采购时间
        /// </summary>
        public long PurchaseTime{ get; set; }
         
        /// <summary>
        /// 布控状态。0：从未进行过布控，1：正在布控，2：已撤控。
        /// </summary>
        public short Dispatched{ get; set; }
    }
}