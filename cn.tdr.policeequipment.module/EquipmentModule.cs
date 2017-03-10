namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using handle;
    using models;

    public class EquipmentModule : MyModule
    {
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

        public IEnumerable<EquipmentModel> Page(string orgId, string storageId, string cabinetId,
            string tagCode, string factorCode, short dataType, int page, int size, out int count)
        {
            count = 0;
            var items = new EquipmentModel[0];
            return items;
        }
    }
}
