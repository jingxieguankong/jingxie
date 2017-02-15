namespace cn.tdr.policeequipment.interfaces
{
    /// <summary>
    /// 作业单元接口
    /// </summary>
    public interface IUnitWork : System.IDisposable
    {
        /// <summary>
        /// 提交，并返回提交结果代码
        /// </summary>
        /// <returns>
        /// 结果代码
        /// <para>0：标识成功；</para>
        /// <para>小于 0：标识对应的错误代码；</para>
        /// </returns>
        int Commit();
    }
}
