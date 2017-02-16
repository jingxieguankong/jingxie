namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Storage:LogicDeletableEntity                                                                                                                                         
    {
         
        /// <summary>
        /// 关联组织机构表主键，标识警械库所属组织机构
        /// </summary>
        public string OrgId{ get; set; }
         
        /// <summary>
        /// 警械库名称
        /// </summary>
        public string Name{ get; set; }
         
        /// <summary>
        /// 基站主键
        /// </summary>
        public string StationId{ get; set; }
         
        /// <summary>
        /// GPS 经度
        /// </summary>
        public double Lon{ get; set; }
         
        /// <summary>
        /// GPS 纬度
        /// </summary>
        public double Lat{ get; set; }
         
        /// <summary>
        /// 警械库负责人，关联警员表主键，NULL 或者 “0” 标识未指定负责警员
        /// </summary>
        public string OfficerId{ get; set; }
    }
}