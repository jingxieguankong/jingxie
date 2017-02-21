namespace cn.tdr.policeequipment.web.Controllers
{
    using System.Web.Mvc;
    using enumerates;
    using module;
    using module.models;

    public class SigninController : AnonymousController
    {
        public ActionResult Index(string msg, string userId, string passwd)
        {
            ViewBag.ErrorMsg = msg;
            ViewBag.UserId = userId;
            ViewBag.Passwd = passwd;
            return View();
        }

        [HttpPost]
        public ActionResult Valid(string userId, string passwd)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(passwd))
            {
                return RedirectToAction(nameof(Index), new { msg = "用户名和密码不能为空 !", userId=userId, passwd=passwd });
            }

            using (var module = new AuthModule())
            {
                UserInfo user = null;
                var status = module.Signin(userId, passwd, out user);
                if (status == AccountLoginStatus.Success)
                {
                    CacheCurrentUser(user);
                    return RedirectToAction("index", "admin");
                }

                string msg = "登录失败 .";
                if (status == AccountLoginStatus.Error)
                {
                    msg = "发生错误 .";
                }

                if (status == AccountLoginStatus.ExceptionAccount)
                {
                    msg = "账户异常 .";
                }

                if (status == AccountLoginStatus.LockedAccount)
                {
                    msg = "账户已锁定 .";
                }

                if (status == AccountLoginStatus.PasswordError)
                {
                    msg = "密码错误 .";
                }

                if (status == AccountLoginStatus.UserNoExist)
                {
                    msg = "账户不存在 .";
                }

                return RedirectToAction(nameof(Index), new { msg = msg, userId = userId, passwd = passwd });
            }
        }
    }
}