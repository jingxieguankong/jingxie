namespace cn.tdr.policeequipment.enumerates
{
    /// <summary>
    /// 登录状态
    /// </summary>
    public enum AccountLoginStatus : short
    {
        Success = 0,
        UserNoExist = -1,
        PasswordError = -2,
        ExceptionAccount = -3,
        LockedAccount = -4,
        Error = -5,
    }
}
