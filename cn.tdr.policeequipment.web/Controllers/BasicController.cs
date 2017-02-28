namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;
    using module.models;

    /// <summary>
    /// 自定义基础控制器，其它控制器都应该从该控制器派生
    /// </summary>
    public abstract class BasicController : Controller
    {
        // 用户 session_key
        private static readonly string UserSessionKey = "MyUser";
        // 用户
        private UserInfo _user;
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public UserInfo CurrentUser
        {
            get { return _user = _user ?? Session[UserSessionKey] as UserInfo; }
            private set { Session[UserSessionKey] = _user = value; }
        }

        protected virtual void CacheCurrentUser(UserInfo user)
        {
            if (null != user)
            {
                CurrentUser = user;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Title = "警械智能管控系统";
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //filterContext.ExceptionHandled = true; // 标识异常已经处理
            // 其它异常相关处理

            // 继续执行系统定义处理
            base.OnException(filterContext);
        }
    }
}