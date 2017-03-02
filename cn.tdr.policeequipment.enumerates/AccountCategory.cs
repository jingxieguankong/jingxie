namespace cn.tdr.policeequipment.enumerates
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountCategory:short
    {
        [Description("")]
        None = 0,
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        SupperAdministrator = 1,
        /// <summary>
        /// 分支机构管理员
        /// </summary>
        [Description("普通管理员")]
        Administrator = 2,
        /// <summary>
        /// 一般性用户
        /// </summary>
        [Description("普通用户")]
        Normal = 3,
    }

    public class AccountCategoryModel
    {
        public AccountCategoryModel(AccountCategory category)
        {
            Category = category;
            Name = $"{category}";
            Value = (short)category;

            var type = category.GetType();
            var attribute =
                type.GetField(Name)
                .GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            Summary = attribute.Description;
        }

        public AccountCategory Category { get; private set; }

        public string Name { get; private set; }

        public string Summary { get; private set; }

        public short Value { get; private set; }
    }

    public static class AccountCategoryHelper
    {
        public static readonly List<AccountCategoryModel> Items = new List<AccountCategoryModel>();

        static AccountCategoryHelper()
        {
            Items.Add(new AccountCategoryModel(AccountCategory.Administrator));
            Items.Add(new AccountCategoryModel(AccountCategory.None));
            Items.Add(new AccountCategoryModel(AccountCategory.Normal));
            Items.Add(new AccountCategoryModel(AccountCategory.SupperAdministrator));
        }

        public static AccountCategoryModel GetInfo(AccountCategory category)
        {
            return
                Items.FirstOrDefault(t => t.Category == category);
        }
    }
}
