namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// 自定义基础控制器，其它控制器都应该从该控制器派生
    /// </summary>
    public abstract class BasicController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true; // 标识异常已经处理
            // 其它异常相关处理

            // 继续执行系统定义处理
            base.OnException(filterContext);
        }
    }
}