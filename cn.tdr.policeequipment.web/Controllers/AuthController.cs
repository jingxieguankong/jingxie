namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using System.Web.Routing;
    using module.models;

    /// <summary>
    /// 需要授权访问基础控制器
    /// </summary>
    public abstract class AuthController : BasicController
    {        
        // 1
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            var returnUrl = Request.RawUrl;
            if (null == CurrentUser)
            {
                var url = (new UrlHelper(filterContext.RequestContext)).Action("Index", "Signin", new { returnUrl = returnUrl });
                var redirect = $"<script type='text/javascript'>top.location.href = '{url}';</script>";
                filterContext.Result = new ContentResult { Content = redirect };
            }
        }

        // 2 action 执行之前
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        // 3 action 执行结束
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        // 4 requestion 请求结束
        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            base.OnAuthenticationChallenge(filterContext);
        }
    }
}