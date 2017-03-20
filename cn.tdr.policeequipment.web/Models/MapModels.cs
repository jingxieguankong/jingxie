namespace cn.tdr.policeequipment.web.Models
{
    public class AttendanceGroupModel
    {
        public string id { get; set; }

        /// <summary>
        /// 警员名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 本次出勤开始时间
        /// </summary>
        public long stime { get; set; }

        /// <summary>
        /// 本次出勤结束时间
        /// </summary>
        public long etime { get; set; }

        /// <summary>
        /// 本次出勤持续时间长度
        /// </summary>
        public long length { get; set; }

        /// <summary>
        /// 本次出勤经过的基站
        /// </summary>
        public AttendanceItemModel[] items { get; set; }
    }

    public class AttendanceItemModel
    {
        /// <summary>
        /// 基站名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 基站 GPS 纬度
        /// </summary>
        public double lat { get; set; }

        /// <summary>
        /// 基站 GPS 经度
        /// </summary>
        public double lon { get; set; }

        /// <summary>
        /// 进入基站时间
        /// </summary>
        public long time { get; set; }
    }
}