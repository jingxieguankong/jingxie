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

    public class EqtTypeModule : MyModule
    {
        public EqtTypeModule(UserInfo user) : base(user)
        {
            Handler = new EqtTypeHandle(Repository);
        }

        public EqtTypeHandle Handler { get; private set; }

        public bool Add(EqtCategory cate)
        {
            return
                null != Handler.Add(cate, true);
        }

        public bool Modify(EqtCategory cate, Expression<Func<EqtCategory, bool>> predicate)
        {
            var count =
                Handler.ModifyAny(
                    m =>
                    {
                        m.Code = cate.Code;
                        m.HitCount = cate.HitCount;
                        m.Name = cate.Name;
                        m.Pid = cate.Pid;

                        return m;
                    }, predicate, true).Count();

            return 0 < count;
        }

        public bool Remove(Expression<Func<EqtCategory, bool>> predicate)
        {
            var count = Handler.RemoveAny(predicate, true).Count();
            return 0 < count;
        }

        public IEnumerable<EqtCategory> FeatchAll()
        {
            var noDel = (short)DeleteStatus.No;
            var items = Handler.All(t => t.IsDel == noDel);
            return items;
        }

        public IEnumerable<EqtCategory> FeatchAll(string ptpId)
        {
            using (var stdHandler = new StandardEquipmentHandle(Repository))
            using (var cateHandler = new EqtTypeHandle(Repository))
            {
                var noDel = (short)DeleteStatus.No;
                var query =
                    from std in stdHandler.All(t => t.IsDel == noDel && t.PtId == ptpId)
                    join cate in cateHandler.All(t => t.IsDel == noDel) on std.CateId equals cate.Id
                    select cate;

                return
                    query.ToArray();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Handler.Dispose();
            Handler = null;
        }
    }
}
