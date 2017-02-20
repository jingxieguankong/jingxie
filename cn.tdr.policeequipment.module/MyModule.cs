namespace cn.tdr.policeequipment.module
{
    using System;
    using models;

    /// <summary>
    /// 用户处理模块
    /// </summary>
    public abstract class MyModule : PasswdModule
    {
        protected MyModule(UserInfo user)
            : base()
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            User = user;
        }

        /// <summary>
        /// 用户
        /// </summary>
        public UserInfo User { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            User = null;
        }
    }
}
