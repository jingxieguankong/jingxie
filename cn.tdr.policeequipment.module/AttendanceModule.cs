namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using enumerates;
    using handle;
    using models;

    public class AttendanceModule : MyModule
    {
        public AttendanceModule(UserInfo user) : base(user)
        {
            Handler = new OfficerAttendanceHandle(Repository);
        }

        public OfficerAttendanceHandle Handler { get; private set; }

        public IEnumerable<AttendanceModel> Page(string pattern, int page, int size, out int count)
        {
            var officerHandler = new OfficerHandle(Repository);
            var atdHandler = new OfficerAttendanceHandle(Repository);
            var orgHandler = new OrganizationHandle(Repository);
            var noDel = (short)DeleteStatus.No;
            var query =
                from atd in atdHandler.All(null)
                join ocr in officerHandler.All(t => t.IsDel == noDel) on atd.OfficerId equals ocr.Id
                join org in orgHandler.All(t => t.IsDel == noDel) on ocr.OrgId equals org.Id
                select new { officer = ocr, attendance = atd, org = org };

            if (!string.IsNullOrWhiteSpace(pattern))
            {
                query = query.Where(t => t.officer.Name.Contains(pattern) || t.officer.IdentyCode.Contains(pattern));
            }

            if (!User.IsSupperAdministrator)
            {
                var orgId = User.Organization.Id;
                query = query.Where(t => t.officer.OrgId == orgId);
            }

            count = query.Count();
            var skipCount = (page - 1) * size;
            var items = query.OrderByDescending(t => t.attendance.STime).Skip(skipCount).Take(size).ToArray()
                .Select(t => new AttendanceModel
                {
                    attendance = t.attendance,
                    officer = t.officer,
                    org = t.org
                });
            return items;
        }
    }
}
