namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using module.models;

    /// <summary>
    /// 需要授权访问基础控制器
    /// </summary>
    public abstract class AuthController : BasicController
    {
        // 用户 session_key
        private static readonly string UserSessionKey = "MyUser";

        private UserInfo _user;
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public UserInfo CurrentUser
        {
            get { return _user = _user ?? Session[UserSessionKey] as UserInfo; }
            private set { Session[UserSessionKey] = _user = value; }
        }

        // 1
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (null == CurrentUser)
            {
                filterContext.Result = RedirectToAction("", "");
            }
        }

        // 2
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        // 3 action 执行之前
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        // 4 action 执行结束
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        // 5
        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            base.OnAuthenticationChallenge(filterContext);
        }
    }
}