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

    public class EquipmentModule : MyModule
    {
        // 即将过期时间间隔
        private static readonly int PreExpiredInterval = -15;
        private static readonly short LowPowerFlag = 10;

        public EquipmentModule(UserInfo user) : base(user)
        {
            Handler = new EquipmentHandle(Repository);
        }

        public EquipmentHandle Handler { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Handler.Dispose();
            Handler = null;
        }

        public IEnumerable<Equipment> FeatchAll(string orgId, string cateId, string stgId)
        {
            var items = new Equipment[0];
            if (!string.IsNullOrWhiteSpace(orgId) && !string.IsNullOrWhiteSpace(cateId) && !string.IsNullOrWhiteSpace(stgId))
            {
                var noDel = (short)DeleteStatus.No;
                items = Handler.All(t => t.IsDel == noDel && t.OrgId == orgId && t.CateId == cateId && t.LibId == stgId)
                    .Where(t => t.OfficerId == null).Take(20).ToArray();
            }
            return items;
        }

        public IEnumerable<EquipmentModel> Page(string orgId, string storageId, string cabinetId,
            string tagCode, string factorCode, short dataType, int page, int size, out int count)
        {
            using (var eqtHandler = new EquipmentHandle(Repository))
            using (var cateHandler = new EqtTypeHandle(Repository))
            using (var stgHandler = new StorageHandle(Repository))
            using (var cabHandler = new CabinetHandle(Repository))
            using (var orgHandler = new OrganizationHandle(Repository))
            using (var offHandler = new OfficerHandle(Repository))
            {
                var noDel = (short)DeleteStatus.No;
                var query =
                    from eqt in eqtHandler.All(t => t.IsDel == noDel)
                    join org in orgHandler.All(t => t.IsDel == noDel) on eqt.OrgId equals org.Id
                    join cate in cateHandler.All(t => t.IsDel == noDel) on eqt.CateId equals cate.Id
                    join stg in stgHandler.All(t => t.IsDel == noDel) on eqt.LibId equals stg.Id
                    join cabitem in cabHandler.All(t => t.IsDel == noDel) on eqt.CabId equals cabitem.Id into cabs
                    from cab in cabs.DefaultIfEmpty(new Cabinet { })
                    join offitem in offHandler.All(t => t.IsDel == noDel) on eqt.OfficerId equals offitem.Id into offs
                    from off in offs.DefaultIfEmpty(new Officer { })
                    select new { eqt = eqt, org = org, cate = cate, stg = stg, cab = cab, officer = off };

                if (!string.IsNullOrWhiteSpace(storageId))
                {
                    query = query.Where(t => t.eqt.LibId == storageId);
                }

                if (!string.IsNullOrWhiteSpace(cabinetId))
                {
                    query = query.Where(t => t.eqt.CabId == cabinetId);
                }

                if (!string.IsNullOrWhiteSpace(tagCode))
                {
                    query = query.Where(t => t.eqt.TagId.Contains(tagCode));
                }

                if (!string.IsNullOrWhiteSpace(factorCode))
                {
                    query = query.Where(t => t.eqt.FactorCode.Contains(factorCode));
                }

                if (!string.IsNullOrWhiteSpace(orgId))
                {
                    query = query.Where(t => t.eqt.OrgId == orgId);
                }

                if (string.IsNullOrWhiteSpace(orgId) && !User.IsSupperAdministrator)
                {
                    orgId = User.Organization.Id;
                    query = query.Where(t => t.eqt.OrgId == orgId);
                }

                var dispatchStatus = 0;
                // 布控中
                if (dataType == (short)QueryEqtDataType.Dispatching)
                {
                    dispatchStatus = (short)DispatchedStatus.Doing;
                    query = query.Where(t => t.eqt.Dispatched == dispatchStatus);
                }

                // 撤控
                if (dataType == (short)QueryEqtDataType.Dispatched)
                {
                    dispatchStatus = (short)DispatchedStatus.Done;
                    query = query.Where(t => t.eqt.Dispatched == dispatchStatus);
                }

                var now = DateTime.Now.ToUnixTime();
                // 已过期
                if (dataType == (short)QueryEqtDataType.Expired)
                {
                    query = query.Where(t => t.eqt.ExpiredTime <= now);
                }

                // 即将过期
                if (dataType == (short)QueryEqtDataType.PreExpired)
                {
                    var preDate = DateTime.Now.AddDays(PreExpiredInterval).ToUnixTime();
                    query = query.Where(t => t.eqt.ExpiredTime >= preDate && t.eqt.ExpiredTime < now);
                }

                // 尚未过期
                if (dataType == (short)QueryEqtDataType.NoExpired)
                {
                    query = query.Where(t => t.eqt.ExpiredTime >= now);
                }

                // 绑定警员
                if (dataType == (short)QueryEqtDataType.Bound)
                {
                    query = query.Where(t => t.eqt.OfficerId != null);
                }

                // 已损坏
                if (dataType == (short)QueryEqtDataType.Destoried)
                {
                    var sta = (short)EquipmentStatus.Destory;
                    query = query.Where(t => t.eqt.Status == sta);
                }

                // 低电量
                if (dataType == (short)QueryEqtDataType.LowPower)
                {
                    query = query.Where(t => t.eqt.Power <= LowPowerFlag);
                }

                count = query.Count();
                var skipCount = (page - 1) * size;
                var items =
                    query.OrderByDescending(t => t.eqt.InputTime).Skip(skipCount).Take(size).ToArray()
                    .Select(t => new EquipmentModel
                    {
                        cabinet = t.cab,
                        category = t.cate,
                        equipment = t.eqt,
                        officer = t.officer,
                        org = t.org,
                        storage = t.stg
                    });
                return items;
            }
        }

        public bool Add(Equipment eqt)
        {
            return
                null != Handler.Add(eqt, true);
        }

        public bool Modify(Equipment eqt, Expression<Func<Equipment, bool>> predicate)
        {
            var items =
                Handler.ModifyAny(
                    m =>
                    {
                        m.CateId = eqt.CateId;
                        m.ChangeTime = eqt.ChangeTime;
                        m.ExpiredTime = eqt.ExpiredTime;
                        m.FactorCode = eqt.FactorCode;
                        m.FactorTime = eqt.FactorTime;
                        m.Factory = eqt.Factory;
                        m.LibId = eqt.LibId;
                        m.Model = eqt.Model;
                        m.OrgId = eqt.OrgId;
                        m.PurchaseTime = eqt.PurchaseTime;
                        m.TagId = eqt.TagId;
                        return m;
                    }, predicate, true);

            return 0 < items.Count();
        }

        public bool Remove(string id)
        {
            var items = Handler.RemoveAny(t => t.Id == id, true);
            return
                0 < items.Count();
        }
    }
}
