namespace cn.tdr.policeequipment.web.Models
{
    using System.Linq;

    /// <summary>
    /// 警力分布模型
    /// </summary>
    public class DispatchModel
    {
        /// <summary>
        /// 分组数
        /// </summary>
        public int groupCount
        {
            get { return groups.Count(); }
        }

        /// <summary>
        /// 警员数
        /// </summary>
        public int policeCount
        {
            get { return groups.Select(t => t.items.Count()).Sum(); }
        }

        /// <summary>
        /// 分组内容
        /// </summary>
        public DispatchGroupModel[] groups { get; set; }
    }

    /// <summary>
    /// 警力分布分组模型
    /// </summary>
    public class DispatchGroupModel
    {
        public string name { get; set; }

        public int type { get; set; }

        public GroupItemModel[] items { get; set; }
    }

    /// <summary>
    /// 警力分布警力模型
    /// </summary>
    public class GroupItemModel
    {
        public string name { get; set; }

        public string orgName { get; set; }

        public Pointer site { get; set; }
    }
}