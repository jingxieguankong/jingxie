using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.tdr.common.secret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.tdr.common.secret.Test
{
    [TestClass()]
    public class Md5HelperTests
    {
        [TestMethod()]
        public void ToMd5Test()
        {
            var userId = "admin";
            var passwd = "111";
            var sec = userId.ToMd5(passwd);
            Assert.AreNotEqual<string>(passwd, sec);
        }
    }
}