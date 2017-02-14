namespace cn.tdr.policeequipment.web.Models
{
    /// <summary>
    /// 菜单项模型
    /// </summary>
    public class MenuItemModel
    {
        public string title { get; set; }

        public string src { get; set; }
        
        public MenuItemModel[] items { get; set; } = new MenuItemModel[0];
    }
}