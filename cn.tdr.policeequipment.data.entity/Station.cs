namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Station:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 关联组织机构表主键，标识所属组织机构
        /// </summary>
        public string OrgId{ get; set; }
         
        /// <summary>
        /// 绑定的基站ID，每个警械柜都有一个基站与之绑定
        /// </summary>
        public string SiteId{ get; set; }
         
        /// <summary>
        /// GPS 经度
        /// </summary>
        public double Lon{ get; set; }
         
        /// <summary>
        /// GPS 纬度
        /// </summary>
        public double Lat{ get; set; }
         
        /// <summary>
        /// 基站类型。0：一般性基站；1：绑定库房基站；2：门口基站。
        /// </summary>
        public short Category{ get; set; }
    }
}