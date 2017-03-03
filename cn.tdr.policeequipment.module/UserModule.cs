namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using common;
    using data.entity;
    using enumerates;
    using handle;
    using models;

    public class UserModule : MyModule
    {
        private static readonly string InitPassword = "123456";

        public UserHandle Handler { get; private set; }

        public UserModule(UserInfo user) : base(user)
        {
            Handler = new UserHandle(Repository);
        }

        public bool Add(User user)
        {
            user.Passwd = EscapePassword(user.Account, InitPassword);
            user.SigninStatus = (short)AccountSigninStatus.NoSignin;
            user.SignupDate = DateTime.Now.ToUnixTime();
            user.Status = (short)AccountStatus.Normal;

            var handler = new RoleHandle(Repository);
            var role = handler.First(t => t.Id == user.RoleId);
            user.OrgId = role.OrgId;

            return
                null != Handler.Add(user, true);
        }

        public bool Modify(User user, Expression<Func<User, bool>> predicate)
        {
            var handler = new RoleHandle(Repository);
            var role = handler.First(t => t.Id == user.RoleId);

            var count = Handler.ModifyAny(
                m =>
                {
                    m.OrgId = role.OrgId;
                    m.Account = user.Account;
                    m.Category = user.Category;
                    m.RoleId = user.RoleId;
                    return m;
                }, predicate, true).Count();
            return 0 < count;
        }

        public bool Remove(string id)
        {
            var count = Handler.RemoveAny(t => t.Id == id, true).Count();
            return 0 < count;
        }

        public IEnumerable<AccountModel> Page(string orgId, string roleId, int page, int size, out int count)
        {
            var noDel = (short)DeleteStatus.No;
            using (var userHandler = new UserHandle(Repository))
            using (var roleHandler = new RoleHandle(Repository))
            using (var orgHandler = new OrganizationHandle(Repository))
            {
                var query =
                    from usr in userHandler.All(t => t.IsDel == noDel)
                    join ritem in roleHandler.All(t => t.IsDel == noDel) on usr.RoleId equals ritem.Id into rls
                    from role in rls.DefaultIfEmpty(new Role { })
                    join orgitem in orgHandler.All(t => t.IsDel == noDel) on usr.OrgId equals orgitem.Id into orgs
                    from org in orgs.DefaultIfEmpty(new Organization { }) 
                    select new { usr = usr, role = role, org = org };

                var roleEmp = string.IsNullOrWhiteSpace(roleId);
                if (!roleEmp)
                {
                    query = query.Where(t => t.role.Id == roleId);
                }

                if (roleEmp && !User.IsSupperAdministrator)
                {
                    orgId = string.IsNullOrWhiteSpace(orgId) ? User.Organization.Id : orgId;
                    query = query.Where(t => t.org.Pid == orgId);
                }


                count = query.Count();
                var skipCount = (page - 1) * size;
                return
                    query.OrderBy(t => t.usr.SignupDate).Skip(skipCount).Take(size).ToArray().Select(t => new AccountModel
                    {
                        org = t.org,
                        role = t.role,
                        user = t.usr
                    }).ToArray();
            }

        }
    }
}
