namespace cn.tdr.policeequipment.web.Models
{
    public abstract class BasicGridPageModel<THeader>
        where THeader : TableHeaderModel
    {
        public string gridId { get; set; } = "x-grid";

        /// <summary>
        /// 头部部分页模型
        /// </summary>
        public GridRowModel<THeader> headerPartialPageModel { get; set; }

        /// <summary>
        /// 头部部分页名称
        /// </summary>
        public string headerPartialPage { get; set; }

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
            get { return headerPartialPageModel.headerModel.id; }
        }

        /// <summary>
        /// 编辑 form 模型
        /// </summary>
        public GridEditFormModel formModel { get; set; }
    }

    public class DataGridPageModel<THeader>:BasicGridPageModel<THeader>
        where THeader : TableHeaderModel
    {
        public string options { get; set; }

        public string toolbarPartialPage { get; set; }
    }

    public class TreeGridPageModel<THeader>: BasicGridPageModel<THeader>
        where THeader:TableHeaderModel
    {

        public bool isAddTop { get; set; } = true;

        public string topAddAction { get; set; }

        /// <summary>
        /// tree 字段
        /// </summary>
        public TableCellModel fieldTree { get; set; }
    }

    public class GridRowModel<THeader>
        where THeader:TableHeaderModel
    {
        public string actionFormart { get; set; } = "fmtOper";

        public THeader headerModel { get; set; }
    }

    public class GridEditFormModel
    {
        public string formId { get; set; } = "x-form-edit";

        public string dlgId { get; set; } = "x-form-dlg";

        public string formTitle { get; set; } = " ";

        public string formAction { get; set; }

        public string formPartialPage { get; set; }

        public string submitAction { get; set; }

        public string cancelAction { get; set; }

        public object formModel { get; set; }
    }
}