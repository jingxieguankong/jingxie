using Microsoft.VisualStudio.TestTools.UnitTesting;
using cn.tdr.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.tdr.common.Test
{
    [TestClass()]
    public class DateTimeHelperTests
    {
        [TestMethod()]
        public void ToUnixTimeTest()
        {
            var timestamp = DateTime.Now.ToUnixTime();
        }
    }
}