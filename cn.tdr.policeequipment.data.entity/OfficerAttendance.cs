namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class OfficerAttendance: Entity
    {
         
        /// <summary>
        /// 关联警员主键，标识被布控的警员，同时标识警员布控
        /// </summary>
        public string OfficerId{ get; set; }
         
        /// <summary>
        /// 本次出勤开始时间
        /// </summary>
        public long STime{ get; set; }
         
        /// <summary>
        /// 本次出勤返回时间
        /// </summary>
        public long ETime{ get; set; }
         
        /// <summary>
        /// 本次出勤总时长
        /// </summary>
        public long TimeLength{ get; set; }
    }
}