namespace cn.tdr.policeequipment.module
{
    using System.Linq;
    using enumerates;
    using handle;
    using models;

    public class FeatureModule : MyModule
    {
        public FeatureModule(UserInfo user) 
            : base(user)
        {
            Handler = new FeatureHandle(Repository);
        }

        /// <summary>
        /// 是否拥有指定菜单功能操作权限
        /// </summary>
        /// <param name="menuId">菜单</param>
        /// <param name="featureType">功能</param>
        /// <returns>true 标识有访问权限，否则，无</returns>
        public bool HasFeature(string menuId, FeatureType featureType)
        {
            //　管理员拥有全部权限
            var isTrue = User.IsSupperAdministrator;

            //  从已提供的数据中查询权限
            var isAllow = (short)FeatureStatus.Normal;
            var actId = featureType.GetInfo().Name;
            if (!isTrue)
            {
                isTrue = User.Features.Any(t => t.MenuId == menuId && t.Status == isAllow && t.ActId == actId);
            }

            // 从数据库中再一次匹配权限
            if (!isTrue)
            {
                var noDel = (short)DeleteStatus.No;
                isTrue = Handler.All(t => t.IsDel == noDel && t.Status == isAllow && t.ActId == actId && t.MenuId == menuId).Any();
            }

            return isTrue;
        }

        /// <summary>
        /// 允许当前用户角色访问指定菜单的功能权限
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="featureType">功能类型</param>
        /// <returns></returns>
        public bool Allow(string menuId, FeatureType featureType)
        {
            var roleId = User.Role.Id;
            var noDel = (short)DeleteStatus.No;
            var ftp = featureType.GetInfo();
            var actId = ftp.Name;
            var feature = Handler.First(t => t.IsDel == noDel && t.MenuId == menuId && t.ActId == actId && t.RoleId == roleId);
            var isTrue = null != feature;
            if (!isTrue)
            {
                feature = new data.entity.Feature
                {
                    ActId = actId,
                    ActRemark = ftp.Summary,
                    IsDel = (short)DeleteStatus.No,
                    MenuId = menuId,
                    RoleId = roleId,
                    Status = (short)FeatureStatus.Normal
                };
                isTrue = null != Handler.Add(feature, true);
            }
            return isTrue;
        }

        /// <summary>
        /// 拒绝当前用户角色访问指定菜单的功能权限
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="featureType">功能类型</param>
        /// <returns></returns>
        public bool Refuse(string menuId, FeatureType featureType)
        {
            var roleId = User.Role.Id;
            var ftp = featureType.GetInfo();
            var actId = ftp.Name;
            var isTrue =
                0 <= Handler.RemoveAny(t => t.ActId == actId && t.MenuId == menuId && t.RoleId == roleId).Count();
            return isTrue;
        }
        
        public FeatureHandle Handler { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
