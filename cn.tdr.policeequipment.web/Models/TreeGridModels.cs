namespace cn.tdr.policeequipment.web.Models
{
    public class TreeGridPageModel
    {
        /// <summary>
        /// 头部部分页模型
        /// </summary>
        public TableTreeHeaderModel headerPartialPageModel { get; set; }

        /// <summary>
        /// 表格数据源
        /// </summary>
        public string dataAction { get; set; }

        /// <summary>
        /// 表格高度
        /// </summary>
        public int tableHeight { get; set; }

        /// <summary>
        /// id 字段
        /// </summary>
        public virtual TableCellModel fieldId
        {
            get { return headerPartialPageModel.id; }
        }

        /// <summary>
        /// tree 字段
        /// </summary>
        public TableCellModel fieldTree { get; set; }

        /// <summary>
        /// 头部部分页名称
        /// </summary>
        public string headerPartialPage { get; set; }
    }
}