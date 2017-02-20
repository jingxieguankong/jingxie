namespace cn.tdr.policeequipment.enumerates
{
    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountCategory:short
    {
        None = 0,
        /// <summary>
        /// 超级管理员
        /// </summary>
        SupperAdministrator = 1,
        /// <summary>
        /// 分支机构管理员
        /// </summary>
        Administrator = 2,
        /// <summary>
        /// 一般性用户
        /// </summary>
        Normal = 3,
    }
}
