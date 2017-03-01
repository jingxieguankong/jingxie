namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using data.entity;
    using enumerates;
    using handle;
    using models;

    public class OrgModule : MyModule
    {
        public OrgModule(UserInfo user) : base(user)
        {
            Handler = new OrganizationHandle(Repository);
        }

        public OrganizationHandle Handler { get; private set; }

        public bool Add(Organization org)
        {
            return null != Handler.Add(org, true);
        }

        public bool Modify(Organization org, Expression<Func<Organization, bool>> predicate)
        {
            var count = Handler.ModifyAny(
                m =>
                {
                    m.Code = org.Code;
                    m.Name = org.Name;
                    m.Pid = org.Pid;
                    m.Id = org.Id;
                    m.IsDel = org.IsDel;
                    m.Layer = org.Layer;
                    return m;
                }, predicate, true).Count();
            return 0 < count;
        }

        public bool Remove(Expression<Func<Organization, bool>> predicate)
        {
            return 0 < Handler.RemoveAny(predicate, true).Count();
        }

        public IEnumerable<Organization> FetchAll()
        {
            var noDel = (short)DeleteStatus.No;
            var query = Handler.All(t => t.IsDel == noDel);
            if (!User.IsSupperAdministrator)
            {
                query = query.Where(t => t.Code.StartsWith(User.Organization.Code));
            }
            return query.ToArray();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Handler.Dispose();
            Handler = null;
        }
    }
}
