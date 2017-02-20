using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cn.tdr.policeequipment.module.test
{
    [TestClass]
    public class MyModuleTest
    {
        [TestMethod]
        public void Constructor()
        {
            var module = new AuthModule();
            Assert.IsNotNull(module.Repository);
        }
    }
}
