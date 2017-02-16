namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Officer:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 管理组织机构表主键，标识警员所属组织机构
        /// </summary>
        public string OrgId{ get; set; }
         
        /// <summary>
        /// 关联警种表主键，标识警员所属的警种类型
        /// </summary>
        public string PtId{ get; set; }
         
        /// <summary>
        /// 关联警械柜表主键。标识警员所属警械柜，NULL 标识该警员没有指明警械柜
        /// </summary>
        public string CabId{ get; set; }
         
        /// <summary>
        /// 警员姓名
        /// </summary>
        public string Name{ get; set; }
         
        /// <summary>
        /// 性别。0：标识 “ 女性 ”；1：标识 ” 男性 “；2：标识 “ 其它 ”
        /// </summary>
        public short Sex{ get; set; }
         
        /// <summary>
        /// 警员编号代码
        /// </summary>
        public string IdentyCode{ get; set; }
         
        /// <summary>
        /// 警员绑定的手机号码。与登录密码结合，支持系统登录
        /// </summary>
        public string Tel{ get; set; }
         
        /// <summary>
        /// 用户头像 URL 地址
        /// </summary>
        public string AtrUrl{ get; set; }
         
        /// <summary>
        /// 绑定的警员数字证书。提供警员登录系统的另一种方式，使用数字证书认证登录，需要校验数字证书的一致性，不适用登录密码。同时在验证之前需要校验账户的状态，在账户处于“ 异常 ”状态时，数据一律无效
        /// </summary>
        public string DigitalID{ get; set; }
         
        /// <summary>
        /// 警员账户创建时间，这是一个精确到秒级的时间戳
        /// </summary>
        public long SignupDate{ get; set; }
         
        /// <summary>
        /// 当前警员绑定的登录账户信息
        /// </summary>
        public string UserId{ get; set; }
    }
}