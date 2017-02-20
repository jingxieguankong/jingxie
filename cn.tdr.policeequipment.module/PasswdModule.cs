namespace cn.tdr.policeequipment.module
{
    using common.secret;

    public abstract class PasswdModule:Module
    {
        // 加密
        protected string EscapePassword(string userId, string passwd)
        {
            return userId.ToMd5(passwd);
        }
    }
}
