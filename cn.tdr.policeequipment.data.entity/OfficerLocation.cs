﻿namespace cn.tdr.policeequipment.data.entity
{
    /// <summary>
    /// 
    /// </summary>
    public class OfficerLocation: Entity
    {
         
        /// <summary>
        /// 关联警员主键，标识被布控的警员，同时标识警员布控
        /// </summary>
        public string OfficerId{ get; set; }

        /// <summary>
        /// 关联警械ID，标识布控的警械
        /// </summary>
        public string EquipId { get; set; }

        /// <summary>
        /// 描述当前标签在哪一个位置的基站范围内
        /// </summary>
        public string SiteId{ get; set; }
         
        /// <summary>
        /// 描述当前是进入或者离开当前基站。1：进入；2：离开。
        /// </summary>
        public short Status{ get; set; }
         
        /// <summary>
        /// 描述基站上传标签的时间，标签的运动位置的排序应该以当前字段为准
        /// </summary>
        public long UpTime{ get; set; }
    }
}