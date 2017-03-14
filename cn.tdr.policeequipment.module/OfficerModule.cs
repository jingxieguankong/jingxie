﻿namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using data.entity;
    using enumerates;
    using handle;
    using models;

    public class OfficerModule : MyModule
    {
        public OfficerModule(UserInfo user) : base(user)
        {
            Handler = new OfficerHandle(Repository);
        }

        public OfficerHandle Handler { get; private set; }

        public IEnumerable<OfficerModel> Page(string orgId, string ptId, string name, string code, int page, int size, out int count)
        {
            using (var officerHandler = new OfficerHandle(Repository))
            using (var userHandler = new UserHandle(Repository))
            using (var orgHandler = new OrganizationHandle(Repository))
            using (var ptpHandler = new PoliceTypeHandle(Repository))
            {
                var noDel = (short)DeleteStatus.No;
                var query =
                    from officer in officerHandler.All(t => t.IsDel == noDel)
                    join useritem in userHandler.All(t => t.IsDel == noDel) on officer.UserId equals useritem.Id into usrs
                    from usr in usrs.DefaultIfEmpty(new User { })
                    join org in orgHandler.All(t => t.IsDel == noDel) on officer.OrgId equals org.Id
                    join ptp in ptpHandler.All(t => t.IsDel == noDel) on officer.PtId equals ptp.Id
                    select new { officer = officer, usr = usr, org = org, ptp = ptp };

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(t => t.officer.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(code))
                {
                    query = query.Where(t => t.officer.IdentyCode.Contains(code));
                }

                if (!string.IsNullOrWhiteSpace(ptId))
                {
                    query = query.Where(t => t.officer.PtId == ptId);
                }

                if (!string.IsNullOrWhiteSpace(orgId))
                {
                    query = query.Where(t => t.officer.OrgId == orgId);
                }

                if (string.IsNullOrWhiteSpace(orgId) && !User.IsSupperAdministrator)
                {
                    orgId = User.Organization.Id;
                    query = query.Where(t => t.officer.OrgId == orgId);
                }

                count = query.Count();
                var skipCount = (page - 1) * size;
                var items =
                    query.OrderByDescending(t => t.officer.SignupDate).Skip(skipCount).Take(size).ToArray()
                    .Select(t => new OfficerModel
                    {
                        officer = t.officer,
                        org = t.org,
                        ptp = t.ptp,
                        user = t.usr
                    });
                return items;
            }
        }

        public bool Add(Officer officer)
        {
            return
                null != Handler.Add(officer, true);
        }

        public bool Modify(Officer officer, Expression<Func<Officer, bool>> predicate)
        {
            var count =
                Handler.ModifyAny(
                    m =>
                    {
                        m.AtrUrl = officer.AtrUrl;
                        m.IdentyCode = officer.IdentyCode;
                        m.Name = officer.Name;
                        m.OrgId = officer.OrgId;
                        m.PtId = officer.PtId;
                        m.Sex = officer.Sex;
                        m.Tel = officer.Tel;
                        return m;
                    }, predicate, true).Count();
            return
                0 < count;
        }

        public bool Remove(Expression<Func<Officer, bool>> predicate)
        {
            var count =
                Handler.RemoveAny(predicate, true).Count();
            return
                0 < count;
        }

        public BindEqtResultStatus BindEqt(string officerId, string eqtId, string cabId, string ptpId, string cateId)
        {
            // 获取当前警员已绑定的相关装备
            // 获取当前警员警种绑定当前装备类型的最大
            using (var stdHandler = new StandardEquipmentHandle(Repository))
            using (var eqtHandler = new EquipmentHandle(Repository))
            {
                var noDel = (short)DeleteStatus.No;
                var std = stdHandler.All(t => t.IsDel == noDel && t.PtId == ptpId && t.CateId == cateId).FirstOrDefault();
                if (std == null)
                {
                    return BindEqtResultStatus.Error;
                }

                var maxCount = std.Num;
                var currentCount = eqtHandler.All(t => t.IsDel == noDel && t.OfficerId == officerId && t.CateId == cateId).Count();
                if (currentCount >= maxCount)
                {
                    return BindEqtResultStatus.Repeate;
                }

                // 绑定
                var items = eqtHandler.ModifyAny(m =>
                {
                    m.OfficerId = officerId;
                    if (!string.IsNullOrWhiteSpace(cabId))
                    {
                        m.CabId = cabId;
                    }
                    return m;
                }, t => t.Id == eqtId, true);

                if (0 < items.Count())
                {
                    return BindEqtResultStatus.Success;
                }

                return BindEqtResultStatus.Failed;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Handler.Dispose();
            Handler = null;
        }
    }

    public enum BindEqtResultStatus:short
    {
        Error = -3,
        Repeate = -2,
        Failed = -1,
        Success = 0,
    }
}
