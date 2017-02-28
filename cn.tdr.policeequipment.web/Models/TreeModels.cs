namespace cn.tdr.policeequipment.web.Models
{
    public class ComboTreeModel
    {
        public string id { get; set; }

        public string text { get; set; }

        public ComboTreeModel[] children { get; set; }
    }
}