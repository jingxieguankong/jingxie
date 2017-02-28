namespace cn.tdr.policeequipment.module
{
    using System.Collections.Generic;
    using System.Linq;
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
