namespace cn.tdr.policeequipment.module
{
    using System.Linq;
    using enumerates;
    using handle;
    using models;

    /// <summary>
    /// 身份认证模块
    /// </summary>
    public class AuthModule:PasswdModule
    {
        /// <summary>
        /// 使用用户名和密码获取用户内容
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <param name="passwd">密码</param>
        /// <param name="user">返回的用户信息</param>
        /// <returns></returns>
        public AccountLoginStatus Signin(string userId, string passwd, out UserInfo user)
        {
            var userHandler = new UserHandle(Repository);
            var roleHandler = new RoleHandle(Repository);
            var officerHandler = new OfficerHandle(Repository);
            var orgHandler = new OrganizationHandle(Repository);
            var featureHandler = new FeatureHandle(Repository);
            var menuHandler = new MenuHandle(Repository);
            var isDel = (short)DeleteStatus.No;
            var query =
                from usr in userHandler.All(t => t.Account == userId && t.IsDel == isDel)
                join orgitem in orgHandler.All(t => t.IsDel == isDel) on usr.OrgId equals orgitem.Id into orgarr
                from org in orgarr.DefaultIfEmpty(new data.entity.Organization())
                join officeritem in officerHandler.All(t => t.IsDel == isDel) on usr.Id equals officeritem.UserId into officers
                from officer in officers.DefaultIfEmpty(new data.entity.Officer())
                join roleitem in roleHandler.All(t => t.IsDel == isDel) on usr.RoleId equals roleitem.Id into roles
                from role in roles.DefaultIfEmpty(new data.entity.Role())
                join fitem in featureHandler.All(t => t.IsDel == isDel) on role.Id equals fitem.RoleId into farr
                from feature in farr.DefaultIfEmpty(new data.entity.Feature())
                join mitem in menuHandler.All(t => t.IsDel == isDel) on feature.MenuId equals mitem.Id into marr
                from menu in marr.DefaultIfEmpty(new data.entity.Menu())
                select new { usr = usr, org = org, officer = officer, role = role, feature = feature, menu = menu };

            user =
                query
                .GroupBy(t => new { usr = t.usr, org = t.org, officer = t.officer, role = t.role })
                .ToArray()
                .Select(t => new UserInfo
                {
                    User = t.Key.usr,
                    Officer = t.Key.officer,
                    Organization = t.Key.org,
                    Role = t.Key.role,
                    Features = t.Select(x => x.feature).Where(x => !string.IsNullOrWhiteSpace(x.MenuId)),
                    Menus = t.Select(x => x.menu).Where(x => !string.IsNullOrWhiteSpace(x.Title))
                })
                .FirstOrDefault();

            if (user == null)
            {
                return AccountLoginStatus.UserNoExist;
            }

            if (user.User.Passwd != EscapePassword(userId, passwd))
            {
                return AccountLoginStatus.PasswordError;
            }

            if (user.User.Status == (int)AccountStatus.Exception)
            {
                return AccountLoginStatus.ExceptionAccount;
            }

            if (user.User.Status == (int)AccountStatus.Locked)
            {
                return AccountLoginStatus.LockedAccount;
            }

            user.User.SigninStatus = (short)AccountSigninStatus.Online;
            if (null == userHandler.Modify(user.User, true))
            {
                return AccountLoginStatus.Error;
            }

            return AccountLoginStatus.Success;
        }

        /// <summary>
        /// 用户签出平台
        /// </summary>
        /// <param name="user">正在签出的用户</param>
        /// <returns></returns>
        public bool Singout(UserInfo user)
        {
            var handler = new UserHandle(Repository);
            {
                user.User.SigninStatus = (short)AccountSigninStatus.Offline;
                return null != handler.Modify(user.User, true);
            }
        }
    }
}
