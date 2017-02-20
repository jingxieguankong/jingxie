namespace cn.tdr.policeequipment.module.models
{
    using System.Collections.Generic;
    using enumerates;
    using data.entity;

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 绑定的警员信息
        /// </summary>
        public Officer Officer { get; set; }

        /// <summary>
        /// 绑定的角色信息
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// 绑定的组织机构信息
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// 绑定角色的菜单功能信息集合
        /// </summary>
        public IEnumerable<Feature> Features { get; set; }

        /// <summary>
        /// 绑定角色的菜单信息集合
        /// </summary>
        public IEnumerable<Menu> Menus { get; set; }

        /// <summary>
        /// 是否超级管理员用户
        /// </summary>
        public bool IsSupperAdministrator
        {
            get { return User.Category == (short)AccountCategory.SupperAdministrator; }
        }
    }
}
