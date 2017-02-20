namespace cn.tdr.policeequipment.enumerates
{
    /// <summary>
    /// 用户在线状态
    /// </summary>
    public enum AccountSigninStatus : short
    {
        Online = 1,
        NoSignin = 0,
        Offline = -1,
        TimeoutOffline = -2,
    }
}
