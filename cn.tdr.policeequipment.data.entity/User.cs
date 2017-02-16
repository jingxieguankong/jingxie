namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class User:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 管理组织机构表主键，标识警员所属组织机构
        /// </summary>
        public string OrgId{ get; set; }
         
        /// <summary>
        /// 登录账户
        /// </summary>
        public string Account{ get; set; }
         
        /// <summary>
        /// 警员登录密码。登录时，需要结合登录账户ID，或者警号，或者手机号码同时进行一致性验证数据有效性。同时在验证之前需要校验账户的状态，在账户处于“ 异常 ”状态时，数据一律无效
        /// </summary>
        public string Passwd{ get; set; }
         
        /// <summary>
        /// 说明警员登录账户的状态。0：正常；-1：异常；-2：已锁定；
        /// </summary>
        public short Status{ get; set; }
         
        /// <summary>
        /// 警员账户登录状态。1：在线；0：未登录；-1：离线；-2：超时离线；
        /// </summary>
        public short SigninStatus{ get; set; }
         
        /// <summary>
        /// 警员账户创建时间，这是一个精确到秒级的时间戳
        /// </summary>
        public long SignupDate{ get; set; }
         
        /// <summary>
        /// 关键系统角色主键，描述用户的角色，用当前字段来确定用户的平台访问功能权限
        /// </summary>
        public string RoleId{ get; set; }
         
        /// <summary>
        /// 系统账户类型。1：超级管理员；2：系统用户。
        /// </summary>
        public short Category{ get; set; }
    }
}